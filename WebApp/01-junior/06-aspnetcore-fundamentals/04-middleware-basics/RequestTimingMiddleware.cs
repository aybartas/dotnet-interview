using System.Diagnostics;
using System.Globalization;

namespace Junior.AspNetCoreFundamentals.MiddlewareBasics;

/// <summary>
/// Middleware #1 — measures how long the request takes and puts it in the response as <c>X-Elapsed-Ms</c>.
///
/// THE TRAP: the obvious code is  <c>await next(ctx); ctx.Response.Headers["X-Elapsed-Ms"] = ...;</c>
/// By the time next() returns, the endpoint has usually written the body, so the status line and headers have ALREADY
/// been sent to the client — headers are read-only and the assignment throws / is silently lost (see NaiveTimingMiddleware, /pitfall/naive).
/// The fix: register a callback with <c>Response.OnStarting</c>; the framework runs it right BEFORE the headers are sent.
///
/// Conventional class middleware: constructor takes <see cref="RequestDelegate"/> (the next ring) + singleton-safe dependencies,
/// <c>InvokeAsync</c> takes the <see cref="HttpContext"/> (scoped services may be added as extra InvokeAsync parameters).
/// One instance lives for the whole app, so it must not keep per-request state in fields.
/// </summary>
public sealed class RequestTimingMiddleware(RequestDelegate next)
{
    public const string HeaderName = "X-Elapsed-Ms";

    public async Task InvokeAsync(HttpContext ctx)
    {
        PipelineTrace.Enter(ctx, "Timing", "starts the stopwatch");
        var start = Stopwatch.GetTimestamp();                         // per-request state lives in locals, never in fields

        ctx.Response.OnStarting(() =>
        {
            // Runs when the response is about to start — the last moment headers can still be changed.
            // The value is "time until the headers were sent" (time to first byte), not the time to stream a long body.
            var ms = Stopwatch.GetElapsedTime(start).TotalMilliseconds;
            ctx.Response.Headers[HeaderName] = ms.ToString("F1", CultureInfo.InvariantCulture);
            return Task.CompletedTask;
        });

        await next(ctx);                                              // everything after this line = the response travelling back out
        PipelineTrace.Leave(ctx, "Timing");
    }
}
