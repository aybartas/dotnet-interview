using System.Text.RegularExpressions;

namespace Junior.AspNetCoreFundamentals.MiddlewareBasics;

/// <summary>
/// Middleware #2 — gives every request an ID that ties together the log lines, the response and (later) calls to other services.
///   1. take <c>X-Correlation-Id</c> from the incoming request (a caller or API gateway may already have created one) or generate a new one
///   2. store it in <c>HttpContext.Items</c> (per-request dictionary: endpoints and later middlewares read it from there)
///   3. echo it in the response header (the client can quote it in a bug report)
///   4. open a logger SCOPE: every log line written inside the scope automatically carries the id (the console shows it as "=> CorrelationId=...")
/// </summary>
public sealed partial class CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
{
    public const string HeaderName = "X-Correlation-Id";
    public const string ItemsKey = "CorrelationId";

    public async Task InvokeAsync(HttpContext ctx)
    {
        var incoming = ctx.Request.Headers[HeaderName].ToString();
        var id = IsAcceptable(incoming) ? incoming : Guid.NewGuid().ToString("N");
        PipelineTrace.Enter(ctx, "CorrelationId", incoming == id ? "reused from the request" : "generated");

        ctx.Items[ItemsKey] = id;
        ctx.Response.OnStarting(() =>
        {
            ctx.Response.Headers[HeaderName] = id;                    // OnStarting for the same reason as in RequestTimingMiddleware
            return Task.CompletedTask;
        });

        // Everything that runs inside this using (= all inner middlewares + the endpoint) logs with CorrelationId in its scope.
        // Order matters: a logging middleware registered BEFORE this one runs outside the scope and its lines carry no id.
        using (logger.BeginScope("CorrelationId={CorrelationId}", id))
        {
            await next(ctx);
        }

        PipelineTrace.Leave(ctx, "CorrelationId");
    }

    // The header is user input and ends up in logs: accept only short, boring values (otherwise: log forging via line breaks).
    private static bool IsAcceptable(string value) => value.Length is > 0 and <= 64 && SafeId().IsMatch(value);

    [GeneratedRegex("^[A-Za-z0-9_-]+$")]
    private static partial Regex SafeId();
}
