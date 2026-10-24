namespace Junior.AspNetCoreFundamentals.MiddlewareBasics;

/// <summary>
/// THE ORDERING EXPERIMENT. Two branches register exactly the SAME seven middlewares; only the order differs.
///
///   /order-a  ExceptionHandler → Timing → CorrelationId → Logging → Slow → Gate → Faulty → endpoint   (recommended)
///   /order-b  Slow → Gate → Logging → Timing → CorrelationId → Faulty → ExceptionHandler → endpoint   (carelessly shuffled)
///
/// Try on both:  /x    /x?deny=1 (Gate short-circuits)    /x?fail=1 (Faulty middleware throws)    /x?boom=1 (the endpoint throws)
/// Compare: X-Elapsed-Ms (does it include Slow?), X-Correlation-Id on a 403, who catches the exception, the CorrelationId in the console log lines.
/// </summary>
public static class OrderExperiment
{
    public static void Map(WebApplication app)
    {
        app.Map("/order-a", branch =>
        {
            branch.UseMiddleware<TraceReportMiddleware>("order-a (recommended order)");   // harness, outermost
            branch.UseMiddleware<ExceptionHandlingMiddleware>();    // 1st: sees every exception from the rings below it
            branch.UseMiddleware<RequestTimingMiddleware>();        // before Slow: the measurement includes Slow
            branch.UseMiddleware<CorrelationIdMiddleware>();        // before Logging: log lines are inside the scope
            branch.UseMiddleware<RequestLoggingMiddleware>();
            branch.UseMiddleware<SlowMiddleware>();
            branch.UseMiddleware<GateMiddleware>();                 // after Timing/CorrelationId: even a 403 gets both headers
            branch.UseMiddleware<FaultyMiddleware>();
            branch.Run(Endpoint);
        });

        app.Map("/order-b", branch =>
        {
            branch.UseMiddleware<TraceReportMiddleware>("order-b (shuffled order)");
            branch.UseMiddleware<SlowMiddleware>();                 // outside Timing: its delay is invisible to the measurement
            branch.UseMiddleware<GateMiddleware>();                 // before everything useful: a 403 has no headers, no log line, no id
            branch.UseMiddleware<RequestLoggingMiddleware>();       // before CorrelationId: log lines carry no id
            branch.UseMiddleware<RequestTimingMiddleware>();
            branch.UseMiddleware<CorrelationIdMiddleware>();
            branch.UseMiddleware<FaultyMiddleware>();               // before the ExceptionHandler: nobody catches its exception
            branch.UseMiddleware<ExceptionHandlingMiddleware>();    // last: only the endpoint is left to protect
            branch.Run(Endpoint);
        });
    }

    /// <summary>Terminal ring (Run): nothing is called after it.</summary>
    private static async Task Endpoint(HttpContext ctx)
    {
        PipelineTrace.Note(ctx, "endpoint reached");
        if (ctx.Request.Query.ContainsKey("boom")) throw new InvalidOperationException("The endpoint failed on purpose (?boom=1)");
        var id = ctx.Items[CorrelationIdMiddleware.ItemsKey] as string ?? "(none: CorrelationId did not run before the endpoint)";
        await ctx.Response.WriteAsync($"endpoint says hello. correlation id seen by the endpoint: {id}");
    }
}
