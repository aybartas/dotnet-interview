using System.Net;
using Microsoft.AspNetCore.Hosting.Server;

namespace Junior.AspNetCoreFundamentals.ProgramHost;

/// <summary>
/// J6.1 — Program.cs &amp; Host. <see cref="Run"/> is the Program.cs of an "empty" ASP.NET Core web project with EVERY line commented.
/// Run it with:  dotnet run -- J6.1   →  http://localhost:5080  (try /  and  /pipeline).
/// Stop it with Ctrl+C.
/// </summary>
public static class ProgramHostTopic
{
    public static int Run(string[] args)
    {
        // ── 1) BUILDER PHASE: describe what the app needs (services, configuration, logging, web server) ──────────────
        var builder = WebApplication.CreateBuilder(args);                       // Creates the builder; wires up defaults: config sources (appsettings*.json, env vars, command line args), logging (console/debug), DI container, Kestrel. `args` lets --environment/--urls/--Key=value reach the host.
        builder.WebHost.UseUrls("http://localhost:5080");                       // Tells Kestrel which address to listen on (same effect as ASPNETCORE_URLS / --urls); the demo pins it so every topic uses the same port.
        builder.Configuration["Demo:Title"] = "J6.1 Program.cs & Host";         // builder.Configuration = IConfiguration: here we add one in-memory value (read back below via app.Configuration).
        builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);    // builder.Logging = logging setup: hide Microsoft.AspNetCore request chatter below Warning so our own log lines stay readable.
        builder.Services.AddSingleton(TimeProvider.System);                     // builder.Services = DI registrations: a (testable) clock as a singleton; endpoints below receive it as a parameter.

        // ── 2) BUILD: the service collection is frozen into a ServiceProvider; from here on you cannot add services ────
        var app = builder.Build();                                              // Produces the WebApplication: it is both the host (lifetime, DI, config, logging) and the pipeline builder (Use*/Map*).

        // ── 3) PIPELINE PHASE: the order of these lines IS the order requests travel through them ───────────────────
        app.Use(async (context, next) =>                                        // Middleware 1 (inline): runs first for every request, BEFORE routing has picked an endpoint.
        {
            Trace(context, "Kestrel", DescribeKestrel(context));                // Everything Kestrel already knows when the first middleware starts: protocol, remote/local endpoint, server type.
            Trace(context, "middleware 1 (in)", $"before routing, endpoint = {context.GetEndpoint()?.DisplayName ?? "<none yet>"}"); // No endpoint is selected yet because UseRouting() comes later.
            await next(context);                                                // Hand the request to the next middleware; the code after this line runs on the way BACK (response direction).
            app.Logger.LogInformation("pipeline out: middleware 1 (out), status {StatusCode}", context.Response.StatusCode); // The response body is already sent, so the outbound stage can only go to the log, not into the body.
        });                                                                     // End of middleware 1.

        app.UseRouting();                                                       // Routing middleware: matches the request path to an endpoint and stores it (context.GetEndpoint()). Called explicitly so we control where it sits.

        app.Use(async (context, next) =>                                        // Middleware 2 (inline): registered AFTER UseRouting, so it can already see the selected endpoint.
        {
            Trace(context, "routing", $"selected endpoint = {context.GetEndpoint()?.DisplayName ?? "<none>"}"); // The endpoint's display name, e.g. "HTTP: GET /pipeline"; it was chosen by UseRouting above.
            Trace(context, "middleware 2 (in)", "after routing, before the endpoint runs"); // Authorization/CORS-style middleware lives here in real apps (they need the endpoint's metadata).
            await next(context);                                                // Continue to the endpoint (UseEndpoints is added automatically at the end of the pipeline).
            app.Logger.LogInformation("pipeline out: middleware 2 (out), status {StatusCode}", context.Response.StatusCode); // Outbound stage of middleware 2, again only visible in the log.
        });                                                                     // End of middleware 2.

        app.MapGet("/", () =>                                                   // Endpoint: GET / — a plain text hint listing what this demo offers.
            "J6.1 Program.cs & Host\n\nEndpoints:\n  GET /           this hint\n  GET /pipeline   the request journey: Kestrel -> middleware -> routing -> endpoint -> response\n"); // The returned string becomes the response body (200 OK, text/plain).

        app.MapGet("/pipeline", (HttpContext context, TimeProvider clock, IConfiguration config) => // Endpoint: GET /pipeline — the delegate's parameters are resolved from DI / the request (HttpContext, our TimeProvider, IConfiguration).
        {
            Trace(context, "endpoint", $"{config["Demo:Title"]} handler runs, utc = {clock.GetUtcNow():HH:mm:ss}"); // The endpoint is the last stage on the way in; it also proves services were injected.
            Trace(context, "response", "the endpoint returns, the trace is written as the body (outbound stages are in the server log)"); // From here the response travels back out through middleware 2 and 1 to Kestrel.
            return Results.Text(string.Join('\n', Trace(context)) + '\n');      // Results.Text = 200 OK + text/plain body containing all recorded stages in order.
        });                                                                     // End of /pipeline.

        // ── 4) RUN: start Kestrel and block this thread until Ctrl+C / SIGTERM ─────────────────────────────────────────
        app.Run();                                                              // Run() = Start() + wait for shutdown: after this line the app listens on http://localhost:5080 and logs "Now listening on:". Returns on graceful shutdown.
        return 0;                                                               // Exit code 0 = clean exit (the menu in WebApp/Program.cs returns it to the OS).
    }

    // Helper: the trace is a List<string> stored in HttpContext.Items — per-request storage that every middleware and the endpoint can reach.
    private static List<string> Trace(HttpContext context)
    {
        if (!context.Items.TryGetValue(nameof(Trace), out var existing))        // First stage of this request: nothing stored yet.
        {
            existing = new List<string>();                                      // Create the list once per request.
            context.Items[nameof(Trace)] = existing;                            // Items lives exactly as long as the request.
        }

        return (List<string>)existing!;                                         // Same list for every later stage.
    }

    // Helper: append one numbered stage line, e.g. "2. routing  | selected endpoint = HTTP: GET /pipeline".
    private static void Trace(HttpContext context, string stage, string detail)
    {
        var list = Trace(context);                                              // Get (or create) this request's trace.
        list.Add($"{list.Count + 1}. {stage,-18}| {detail}");                   // Number = position in the journey; the stage name is padded so the output lines up.
    }

    // Helper: what Kestrel (the web server) hands to the pipeline for the connection the request arrived on.
    private static string DescribeKestrel(HttpContext context)
    {
        var server = context.RequestServices.GetRequiredService<IServer>();     // IServer is the registered web server; under `dotnet run` it is KestrelServer (the in-process server).
        var remote = new IPEndPoint(context.Connection.RemoteIpAddress ?? IPAddress.None, context.Connection.RemotePort); // The client's address (TCP connection peer).
        var local = new IPEndPoint(context.Connection.LocalIpAddress ?? IPAddress.None, context.Connection.LocalPort);    // The address the connection arrived on (the port Kestrel listens on).
        return $"server = {server.GetType().Name}, {context.Request.Protocol} {context.Request.Method}, remote = {remote}, local = {local}, connection id = {context.Connection.Id}"; // One line: server, HTTP version + method, both endpoints and Kestrel's connection id.
    }
}
