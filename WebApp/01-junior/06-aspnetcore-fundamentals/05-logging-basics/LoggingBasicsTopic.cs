namespace Junior.AspNetCoreFundamentals.LoggingBasics;

/// <summary>
/// J6.5 Logging basics. Run: <c>dotnet run -- J6.5 [--environment Development] [--json] [--Logging:LogLevel:Default=Trace]</c>
/// Config files of this demo live next to this file (appsettings.json + appsettings.{Environment}.json).
/// </summary>
public static class LoggingBasicsTopic
{
    private const string TopicFolder = "01-junior/06-aspnetcore-fundamentals/05-logging-basics";

    public static int Run(string[] args)
    {
        // "--json" is OUR switch. Remove it before CreateBuilder: the command-line provider would treat
        // "--json --environment Development" as json="--environment" and swallow the real argument.
        var useJson = args.Contains("--json");
        args = [.. args.Where(a => a != "--json")];

        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseUrls("http://localhost:5080");

        // 1) Configuration: base file, then the environment file (later wins). reloadOnChange = edit the JSON while the
        //    app runs and the log levels change WITHOUT a restart (the filter rules are read through IOptionsMonitor).
        var folder = Path.Combine(builder.Environment.ContentRootPath, TopicFolder);
        builder.Configuration
            .AddJsonFile(Path.Combine(folder, "appsettings.json"), optional: false, reloadOnChange: true)
            .AddJsonFile(Path.Combine(folder, $"appsettings.{builder.Environment.EnvironmentName}.json"), optional: true, reloadOnChange: true)
            // CreateBuilder already added env vars + command line BEFORE our JSON files, so our files would override
            // them. Add both again at the end to restore the real order: JSON < env vars < command line.
            .AddEnvironmentVariables()
            .AddCommandLine(args);

        // 2) Providers. CreateBuilder already added Console, Debug, EventSource (+ EventLog on Windows);
        //    here we only choose the console FORMAT. IncludeScopes = true prints BeginScope data too (JSON: "Scopes").
        if (useJson)
        {
            builder.Logging.AddJsonConsole(o =>
            {
                o.IncludeScopes = true;
                o.UseUtcTimestamp = true;
                o.TimestampFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
                o.JsonWriterOptions = new System.Text.Json.JsonWriterOptions { Indented = false };   // one JSON object per line
            });
        }
        else
        {
            builder.Logging.AddSimpleConsole(o =>
            {
                o.SingleLine = true;
                o.IncludeScopes = false;   // the framework adds RequestId/TraceId scopes to every line; use --json to see them
                o.TimestampFormat = "HH:mm:ss ";
            });
        }

        // 3) Our own in-memory provider: it lets the endpoints show what a provider really receives.
        var capture = new CapturingLoggerProvider();
        builder.Logging.AddProvider(capture);

        builder.Services.AddSingleton(capture);
        builder.Services.AddSingleton<OrderService>();
        builder.Services.AddSingleton<ShippingService>();
        builder.Services.AddSingleton<AccountService>();
        builder.Services.AddSingleton<StructuredLoggingDemo>();

        var app = builder.Build();

        app.MapGet("/", () => Results.Text("""
            J6.5 Logging basics
              GET /log                          one line per level + which levels are enabled (watch the server console)
              GET /levels                       IsEnabled matrix for several categories (filter rules and precedence)
              GET /structured-vs-interpolated   {UserId} template vs $"..." - what a provider really receives
              GET /source-generated             [LoggerMessage] source-generated logging
              GET /scope                        BeginScope with a correlation id (header X-Correlation-Id or generated)
              GET /exception                    LogError(ex, ...) vs interpolating ex
              GET /sensitive                    leaking vs redacting secrets and PII (FAKE data)
            """));

        app.MapGet("/log", (OrderService orders, IHostEnvironment env, IConfiguration config) =>
        {
            orders.PlaceOrder(orderId: 1001, itemCount: 2);
            return new
            {
                environment = env.EnvironmentName,
                category = typeof(OrderService).FullName,
                configuredDefault = config["Logging:LogLevel:Default"],
                enabledLevels = orders.EnabledLevels(),
                hint = "Look at the server console: only the enabled levels were written."
            };
        });

        app.MapGet("/levels", (ILoggerFactory factory, IHostEnvironment env, IConfiguration config) =>
        {
            string[] categories =
            [
                "Junior.AspNetCoreFundamentals.LoggingBasics.OrderService",
                "Junior.AspNetCoreFundamentals.LoggingBasics.ShippingService",
                "Microsoft.AspNetCore.Routing.EndpointMiddleware",
                "Microsoft.Hosting.Lifetime",
                "System.Net.Http.HttpClient"
            ];
            return new
            {
                environment = env.EnvironmentName,
                configuredRules = config.GetSection("Logging:LogLevel").GetChildren().ToDictionary(c => c.Key, c => c.Value),
                categories = categories.ToDictionary(c => c, c => OrderService.EnabledLevels(factory.CreateLogger(c)))
            };
        });

        app.MapGet("/structured-vs-interpolated", (StructuredLoggingDemo demo, CapturingLoggerProvider cap) =>
        {
            cap.Clear();
            demo.LogBothWays(userId: 42, city: "Izmir");
            return new { entries = cap.Snapshot().Select(e => new { e.Message, e.Template, e.Properties }) };
        });

        app.MapGet("/source-generated", (ShippingService shipping, CapturingLoggerProvider cap) =>
        {
            cap.Clear();
            shipping.Ship(orderId: 1001, city: "Ankara");
            return new { entries = cap.Snapshot().Select(e => new { e.Level, e.Message, e.Template, e.Properties, e.ExceptionType }) };
        });

        app.MapGet("/scope", (HttpRequest request, StructuredLoggingDemo demo, OrderService orders, CapturingLoggerProvider cap) =>
        {
            var correlationId = request.Headers["X-Correlation-Id"].FirstOrDefault() ?? Guid.NewGuid().ToString("N")[..8];
            cap.Clear();
            demo.LogInsideScope(correlationId, orders);
            var entries = cap.Snapshot();
            return new
            {
                correlationId,
                // ASP.NET Core itself opens scopes per request (TraceId, RequestId, RequestPath ...); shown once, not per entry.
                frameworkScopes = entries.First().Scopes.Where(s => !s.StartsWith("CorrelationId")),
                entries = entries.Select(e => new { e.Category, e.Level, e.Message, scopes = e.Scopes.Where(s => s.StartsWith("CorrelationId")) })
            };
        });

        app.MapGet("/exception", (OrderService orders, CapturingLoggerProvider cap) =>
        {
            cap.Clear();
            orders.FailPaymentCorrectly(7);
            orders.FailPaymentWrongly(8);
            static string Cut(string? s) => s is { Length: > 70 } ? s[..70] + "..." : s ?? "";
            return new
            {
                entries = cap.Snapshot().Select(e => new { template = Cut(e.Template), e.Properties, e.ExceptionType, exceptionTextInTemplate = e.Template?.Contains("   at ") })
            };
        });

        app.MapGet("/sensitive", (AccountService accounts, CapturingLoggerProvider cap) =>
        {
            var attempt = AccountService.FakeAttempt();   // FAKE values only
            cap.Clear();
            accounts.LogLeaky(attempt);
            accounts.LogRedacted(attempt);
            return new { entries = cap.Snapshot().Select(e => new { e.Message }) };
        });

        app.Run();
        return 0;
    }
}
