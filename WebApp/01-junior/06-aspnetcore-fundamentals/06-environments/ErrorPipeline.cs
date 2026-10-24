using System.Diagnostics;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Junior.AspNetCoreFundamentals.Environments;

/// <summary>
/// The ONE error-handling setup shared by the main host and the two short-lived hosts of <c>/compare</c>,
/// so the Development and Production outputs differ only because of the environment, never because of different code.
/// </summary>
internal static class ErrorPipeline
{
    /// <summary>The message of the demo exception. It deliberately contains a fake connection string: this is what must never reach a client in Production.</summary>
    public const string SecretMessage = "Cannot open database. ConnectionString: Server=db01.internal;Database=Orders;User Id=sa;Password=S3cr3t!";

    public static void AddServices(IServiceCollection services)
    {
        // Registers IProblemDetailsService: UseExceptionHandler and UseStatusCodePages use it to write RFC 9457 bodies (→ J7.3).
        services.AddProblemDetails();
    }

    public static void UseMiddleware(WebApplication app)
    {
        // Development: nothing to add. WebApplication ALREADY inserts UseDeveloperExceptionPage() at the very start of the
        // pipeline when the environment is Development (via a startup filter); calling it again would only duplicate it.
        if (!app.Environment.IsDevelopment())
        {
            // Production, Staging, anything else: catch the exception, re-execute the pipeline at /error, never show details.
            app.UseExceptionHandler("/error");
        }

        // Empty 4xx/5xx responses (e.g. 404 for an unknown path) get a body: with AddProblemDetails it is ProblemDetails JSON.
        app.UseStatusCodePages();
    }

    public static void MapEndpoints(WebApplication app)
    {
        // The deliberately broken endpoint.
        app.MapGet("/boom", IResult () => throw new InvalidOperationException(SecretMessage));

        // The error handler target. Note: no exception message, no stack trace - only a generic title and a trace id.
        // The exception itself is logged by ExceptionHandlerMiddleware on the server side; the client only gets the
        // trace id to quote to support, and the same id finds the log entry.
        app.Map("/error", (HttpContext http) => Results.Problem(
            title: "An unexpected error occurred.",
            statusCode: StatusCodes.Status500InternalServerError,
            extensions: new Dictionary<string, object?> { ["traceId"] = Activity.Current?.Id ?? http.TraceIdentifier }));
    }

    /// <summary>Starts a short-lived in-process host for the given environment on a random free port (same technique as the async-basics LocalApi).</summary>
    public static async Task<(WebApplication App, string BaseUrl)> StartHostAsync(string environmentName)
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions { EnvironmentName = environmentName });
        builder.Logging.ClearProviders();                    // keep the demo output clean
        builder.WebHost.UseUrls("http://127.0.0.1:0");       // 0 = the operating system picks a free port
        AddServices(builder.Services);

        var app = builder.Build();
        UseMiddleware(app);
        MapEndpoints(app);

        await app.StartAsync();
        var url = app.Services.GetRequiredService<IServer>().Features.Get<IServerAddressesFeature>()!.Addresses.First();
        return (app, url);
    }
}
