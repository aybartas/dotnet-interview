namespace Junior.AspNetCoreFundamentals.Environments;

/// <summary>
/// J6.6 Environments &amp; error pages. Run:
///   dotnet run -- J6.6                                  (no setting anywhere → Production)
///   dotnet run -- J6.6 --environment Development
/// Endpoints: /env (which environment and why), /boom (throws), /error (handler target), /compare (Development vs Production side by side).
/// </summary>
public static class EnvironmentsTopic
{
    public static int Run(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseUrls("http://localhost:5080");
        ErrorPipeline.AddServices(builder.Services);

        var app = builder.Build();
        ErrorPipeline.UseMiddleware(app);
        ErrorPipeline.MapEndpoints(app);

        app.MapGet("/", (IHostEnvironment env) => Results.Text(
            $"J6.6 Environments & error pages - running as '{env.EnvironmentName}'\n\n" +
            "  GET /env      which environment, how it was decided, environment-specific toggles\n" +
            "  GET /boom     throws an exception (watch how the response differs per environment)\n" +
            "  GET /compare  starts a Development and a Production host and calls /boom on both\n" +
            "  GET /nothing  an unknown path → 404 with a ProblemDetails body (UseStatusCodePages)\n"));

        app.MapGet("/env", (IWebHostEnvironment env) =>
        {
            // Precedence with WebApplication.CreateBuilder (verified): command line > DOTNET_ENVIRONMENT > ASPNETCORE_ENVIRONMENT > default Production.
            // (With the older WebHost builders it is the other way round: ASPNETCORE_ENVIRONMENT beats DOTNET_ENVIRONMENT.)
            var fromCommandLine = ReadCommandLineEnvironment(args);
            var aspNetCore = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var dotNet = System.Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            var source =
                fromCommandLine is not null ? "command line (--environment)" :
                dotNet is not null ? "DOTNET_ENVIRONMENT" :
                aspNetCore is not null ? "ASPNETCORE_ENVIRONMENT" :
                "default - nothing is set, so the host falls back to Production";

            return Results.Ok(new
            {
                environmentName = env.EnvironmentName,
                decidedBy = source,
                inputs = new
                {
                    commandLine = fromCommandLine,
                    aspNetCoreEnvironment = aspNetCore,
                    dotNetEnvironment = dotNet,
                },
                checks = new
                {
                    isDevelopment = env.IsDevelopment(),
                    isStaging = env.IsStaging(),
                    isProduction = env.IsProduction(),
                    isEnvironmentCustom = env.IsEnvironment("Custom"),
                },
                host = new
                {
                    env.ApplicationName,
                    env.ContentRootPath,
                },
                behaviour = new
                {
                    errorPages = env.IsDevelopment()
                        ? "developer exception page (added automatically by WebApplication)"
                        : "UseExceptionHandler(\"/error\") → generic ProblemDetails",
                    exceptionDetailsInResponse = env.IsDevelopment(),
                    stagingRehearsal = env.IsStaging() ? "Staging: production-like settings, separate resources" : null,
                },
            });
        });

        // Starts one Development and one Production host (random ports), calls /boom on both, returns both answers, disposes the hosts.
        app.MapGet("/compare", async () =>
        {
            var result = new Dictionary<string, object?>();
            foreach (var environmentName in new[] { Microsoft.Extensions.Hosting.Environments.Development, Microsoft.Extensions.Hosting.Environments.Production })
            {
                var (host, baseUrl) = await ErrorPipeline.StartHostAsync(environmentName);
                try
                {
                    using var client = new HttpClient { BaseAddress = new Uri(baseUrl) };
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    using var response = await client.GetAsync("/boom");
                    var body = await response.Content.ReadAsStringAsync();

                    result[environmentName] = new
                    {
                        status = (int)response.StatusCode,
                        contentType = response.Content.Headers.ContentType?.ToString(),
                        leaksSecret = body.Contains("Password=", StringComparison.Ordinal),
                        leaksStackTrace = body.Contains(" at ", StringComparison.Ordinal) && body.Contains(".cs", StringComparison.Ordinal),
                        bodyLength = body.Length,
                        body = Trim(body, 420),
                    };
                }
                finally
                {
                    await host.StopAsync();
                    await host.DisposeAsync();
                }
            }

            return Results.Json(result, CompareJson);
        });

        app.Run();
        return 0;
    }

    private static readonly System.Text.Json.JsonSerializerOptions CompareJson = new()
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,   // readable output: no \u0022 for quotes
    };

    private static string Trim(string text, int max) => text.Length <= max ? text : text[..max] + $"... [trimmed, {text.Length} chars total]";

    /// <summary>Finds "--environment X" or "--environment=X" in the arguments (the host's command-line provider accepts both forms).</summary>
    private static string? ReadCommandLineEnvironment(string[] args)
    {
        for (var i = 0; i < args.Length; i++)
        {
            if (args[i].StartsWith("--environment=", StringComparison.OrdinalIgnoreCase)) return args[i]["--environment=".Length..];
            if (args[i].Equals("--environment", StringComparison.OrdinalIgnoreCase) && i + 1 < args.Length) return args[i + 1];
        }
        return null;
    }
}
