namespace Junior.AspNetCoreFundamentals.MiddlewareBasics;

/// <summary>
/// J6.4 — The middleware pipeline.
///
///   dotnet run -- J6.4          → http://localhost:5080
///
/// Contents (try the URLs from README.md):
///   /order-a/...  /order-b/...   the ORDERING EXPERIMENT: same middlewares, different order, different behavior (OrderExperiment.cs)
///   /pitfall/naive  /pitfall/ok  setting a header after next() vs. via Response.OnStarting
///   /map-demo  ?mapwhen  ?debug  Map / MapWhen / UseWhen
///   /  /hello  /blocked  /throw  the main pipeline: Use vs. Run, short-circuit, exception handler placed first
/// </summary>
public static class MiddlewareBasicsTopic
{
    public static int Run(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseUrls("http://localhost:5080");

        // One line per log entry; IncludeScopes shows the logger scope opened by CorrelationIdMiddleware ("=> CorrelationId=...").
        builder.Logging.ClearProviders();
        builder.Logging.AddSimpleConsole(o =>
        {
            o.SingleLine = true;
            o.IncludeScopes = true;
            o.TimestampFormat = "HH:mm:ss.fff ";
        });
        builder.Logging.Configure(o => o.ActivityTrackingOptions = ActivityTrackingOptions.None);   // no SpanId/TraceId scopes: keep the CorrelationId scope easy to spot
        builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);    // hide the framework's own request noise
        builder.Logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);

        var app = builder.Build();
        BuildPipeline(app);
        app.Run();
        return 0;
    }

    private static void BuildPipeline(WebApplication app)
    {
        // ── Branches FIRST ────────────────────────────────────────────────────────────────────────────────────────
        // A Map branch is cut out of the pipeline AT THE POINT where Map is called: it inherits every middleware registered
        // BEFORE it. These branches must start from an empty pipeline (the experiment controls their order completely),
        // so they are registered before any middleware of the main pipeline.
        OrderExperiment.Map(app);

        // /pitfall — the same measurement done wrong and right. Both write a body from the endpoint.
        app.Map("/pitfall/naive", b =>
        {
            b.UseMiddleware<NaiveTimingMiddleware>();
            b.Run(ctx => ctx.Response.WriteAsync("header set AFTER next() — check the response headers and the server log"));
        });
        app.Map("/pitfall/ok", b =>
        {
            b.UseMiddleware<RequestTimingMiddleware>();
            b.Run(ctx => ctx.Response.WriteAsync("header set via Response.OnStarting — X-Elapsed-Ms is there"));
        });

        // Map: path-prefix branch; the prefix moves from Path to PathBase and the branch never rejoins the main pipeline.
        app.Map("/map-demo", b => b.Run(ctx =>
            ctx.Response.WriteAsync($"Map branch.  PathBase='{ctx.Request.PathBase}'  Path='{ctx.Request.Path}'  (the main pipeline below never ran)")));

        // MapWhen: branch on ANY condition (here: a query key). Like Map it does not rejoin.
        app.MapWhen(ctx => ctx.Request.Query.ContainsKey("mapwhen"), b => b.Run(ctx =>
            ctx.Response.WriteAsync("MapWhen branch: the predicate matched, the main pipeline was skipped")));

        // UseWhen: conditional branch that REJOINS the main pipeline after the branch ends (if the branch calls next()).
        app.UseWhen(ctx => ctx.Request.Query.ContainsKey("debug"), b => b.Use(async (ctx, next) =>
        {
            ctx.Response.Headers["X-Debug"] = "UseWhen branch ran, then rejoined the main pipeline";
            await next();
        }));

        // ── Main pipeline ─────────────────────────────────────────────────────────────────────────────────────────
        // 1) Exception handler FIRST: it wraps every ring below it. (The same middleware placed last protects only the endpoint — see /order-b.)
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        // 2) The two custom middlewares.
        app.UseMiddleware<RequestTimingMiddleware>();
        app.UseMiddleware<CorrelationIdMiddleware>();

        // 3) Inline middleware (Use): code before next() = request side, code after next() = response side.
        app.Use(async (ctx, next) =>
        {
            var logger = ctx.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("InlineLogger");
            logger.LogInformation("inline Use: before next() — {Method} {Path}", ctx.Request.Method, ctx.Request.Path);
            await next(ctx);
            logger.LogInformation("inline Use: after next() — status {StatusCode}", ctx.Response.StatusCode);
        });

        // 4) Short-circuit: /blocked never calls next(), so nothing below this ring runs for it.
        //    The rings above (exception handler, timing, correlation id, inline logger) still see the response on its way out.
        app.Use(async (ctx, next) =>
        {
            if (ctx.Request.Path == "/blocked")
            {
                ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                await ctx.Response.WriteAsync("blocked: this middleware did not call next(), so the terminal Run below never ran");
                return;                                             // ← no next()
            }
            await next(ctx);
        });

        // 5) A middleware that throws: only the exception handler at position 1 can turn it into a clean 500.
        app.Use(async (ctx, next) =>
        {
            if (ctx.Request.Path == "/throw") throw new InvalidOperationException("boom from a middleware inside the pipeline");
            await next(ctx);
        });

        // 6) Run: the TERMINAL middleware — it has no next, the pipeline ends here.
        app.Run(async ctx =>
        {
            var id = ctx.Items[CorrelationIdMiddleware.ItemsKey] as string;
            ctx.Response.ContentType = "text/plain; charset=utf-8";
            if (ctx.Request.Path == "/hello")
                await ctx.Response.WriteAsync($"hello from the terminal Run middleware. correlation id: {id}");
            else
                await ctx.Response.WriteAsync("""
                    J6.4 middleware demo. Try:
                      /hello            main pipeline (timing + correlation id + inline logger + terminal Run)
                      /blocked          short-circuit: a middleware that does not call next()
                      /throw            exception from a middleware, caught by the handler registered first
                      /hello?debug      UseWhen branch rejoins the main pipeline (X-Debug header)
                      /hello?mapwhen    MapWhen branch (never rejoins)
                      /map-demo/a/b     Map branch (PathBase / Path)
                      /pitfall/naive    header set after next()  -> missing + error in the log
                      /pitfall/ok       header set via OnStarting
                      /order-a/x  /order-b/x   ordering experiment; add ?deny=1  ?fail=1  ?boom=1
                    """);
        });
    }
}
