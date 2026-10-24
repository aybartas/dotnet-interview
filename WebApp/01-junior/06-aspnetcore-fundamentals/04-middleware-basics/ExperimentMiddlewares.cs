using System.Diagnostics;
using System.Globalization;

namespace Junior.AspNetCoreFundamentals.MiddlewareBasics;

// Small middlewares used only by the ordering experiment (OrderExperiment.cs) and the /pitfall branch.
// Each one reports itself to PipelineTrace so the response shows who ran in which order.

/// <summary>
/// Catches exceptions thrown by the rings INSIDE it (= registered after it) and turns them into a 500 JSON answer.
/// It can only catch what happens deeper in the pipeline — which is why it belongs at the very beginning (J6.6 → UseExceptionHandler).
/// </summary>
public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext ctx)
    {
        PipelineTrace.Enter(ctx, "ExceptionHandler");
        try
        {
            await next(ctx);
        }
        catch (Exception ex)
        {
            logger.LogError("Caught {ExceptionType}: {Message}", ex.GetType().Name, ex.Message);
            PipelineTrace.Note(ctx, $"✗ caught {ex.GetType().Name}: \"{ex.Message}\"  → answering 500");
            ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
            ctx.Response.ContentType = "application/json";
            await ctx.Response.WriteAsync("""{"error":"internal server error (handled by ExceptionHandlingMiddleware)"}""");
        }
        PipelineTrace.Leave(ctx, "ExceptionHandler");
    }
}

/// <summary>Logs one line when the request arrives and one when the response leaves. Whether these lines carry the CorrelationId depends only on the ORDER.</summary>
public sealed class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext ctx)
    {
        PipelineTrace.Enter(ctx, "Logging");
        logger.LogInformation("--> {Method} {Path}", ctx.Request.Method, $"{ctx.Request.PathBase}{ctx.Request.Path}");
        await next(ctx);
        logger.LogInformation("<-- {StatusCode}", ctx.Response.StatusCode);
        PipelineTrace.Leave(ctx, "Logging");
    }
}

/// <summary>Deliberately slow (150 ms) — whoever stands outside this ring includes the delay in its measurement, whoever stands inside does not.</summary>
public sealed class SlowMiddleware(RequestDelegate next)
{
    public const int DelayMs = 150;

    public async Task InvokeAsync(HttpContext ctx)
    {
        PipelineTrace.Enter(ctx, "Slow", $"waits {DelayMs} ms");
        await Task.Delay(DelayMs, ctx.RequestAborted);
        await next(ctx);
        PipelineTrace.Leave(ctx, "Slow");
    }
}

/// <summary>
/// SHORT-CIRCUIT: with <c>?deny=1</c> it answers 403 itself and does NOT call next() — the inner rings never see the request.
/// The response still travels back out through the rings that are OUTSIDE the gate, and only through those.
/// </summary>
public sealed class GateMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext ctx)
    {
        PipelineTrace.Enter(ctx, "Gate");
        if (ctx.Request.Query.ContainsKey("deny"))
        {
            PipelineTrace.Note(ctx, "✗ short-circuit: next() is NOT called, answering 403");
            ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
            await ctx.Response.WriteAsync("denied by the Gate middleware");
        }
        else
        {
            await next(ctx);
        }
        PipelineTrace.Leave(ctx, "Gate");
    }
}

/// <summary>A buggy middleware: with <c>?fail=1</c> it throws. Whether anybody catches it depends on where the ExceptionHandler stands.</summary>
public sealed class FaultyMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext ctx)
    {
        PipelineTrace.Enter(ctx, "Faulty");
        if (ctx.Request.Query.ContainsKey("fail"))
            throw new InvalidOperationException("Faulty middleware failed on purpose (?fail=1)");
        await next(ctx);
        PipelineTrace.Leave(ctx, "Faulty");
    }
}

/// <summary>
/// The WRONG way to measure time (used by /pitfall/naive): the header is set after next() — when the response has already started.
/// ASP.NET Core protects you with an exception; the middleware swallows and logs it so the demo keeps running, and the header is simply missing.
/// </summary>
public sealed class NaiveTimingMiddleware(RequestDelegate next, ILogger<NaiveTimingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext ctx)
    {
        var start = Stopwatch.GetTimestamp();
        await next(ctx);
        try
        {
            ctx.Response.Headers["X-Elapsed-Ms"] = Stopwatch.GetElapsedTime(start).TotalMilliseconds.ToString("F1", CultureInfo.InvariantCulture);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogError("Naive timing failed (HasStarted={HasStarted}): {Message}", ctx.Response.HasStarted, ex.Message);
        }
    }
}
