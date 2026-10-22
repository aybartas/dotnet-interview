using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Junior.DevFoundations.HowTheWebWorks;

/// <summary>
/// Writes the incoming HTTP request to the console in the form closest to the RAW text that came over the wire:
///
///     GET /search?q=dotnet HTTP/1.1          ← request line: method + path + query + protocol version
///     Host: localhost:5080                   ← headers
///     User-Agent: curl/8.5.0
///                                            ← blank line: end of the headers
///     {"name":"..."}                         ← body (if any)
///
/// Kestrel has already parsed the request and put it into the HttpContext; we turn it back into "text".
/// HTTP/1.1 really does speak this plain-text format (HTTP/2 and 3 are binary, but the logical model is the same).
/// </summary>
public sealed class RawRequestLoggingMiddleware(RequestDelegate next, ILogger<RawRequestLoggingMiddleware> logger)
{
    private const int MaxBodyBytesToLog = 2048;

    public async Task InvokeAsync(HttpContext ctx)
    {
        var raw = await RenderAsync(ctx.Request);
        ctx.Items["raw-request"] = raw;                       // so endpoints can put it in the response body if they want

        logger.LogInformation("┌── incoming request (remote: {Remote}) ──\n{Raw}\n└──", ctx.Connection.RemoteIpAddress, raw);

        await next(ctx);

        logger.LogInformation("  ↳ response: {Status} {Reason}", ctx.Response.StatusCode,
            ReasonPhrases.GetReasonPhrase(ctx.Response.StatusCode));
    }

    private static async Task<string> RenderAsync(HttpRequest req)
    {
        var sb = new StringBuilder();

        // 1) Request line
        sb.Append(req.Method).Append(' ').Append(req.Path).Append(req.QueryString).Append(' ').AppendLine(req.Protocol);

        // 2) Headers (Kestrel puts Host here too)
        foreach (var (name, values) in req.Headers)
            sb.Append(name).Append(": ").AppendLine(values.ToString());

        // 3) Blank line + body
        sb.AppendLine();
        if (req.ContentLength is > 0 || req.Headers.ContainsKey("Transfer-Encoding"))
        {
            req.EnableBuffering();                              // so the body can be read again later
            using var reader = new StreamReader(req.Body, Encoding.UTF8, leaveOpen: true);
            var buffer = new char[MaxBodyBytesToLog];
            var read = await reader.ReadBlockAsync(buffer, 0, buffer.Length);
            sb.Append(buffer, 0, read);
            if (read == MaxBodyBytesToLog) sb.Append(" …(truncated)");
            req.Body.Position = 0;
        }

        return sb.ToString().TrimEnd();
    }
}
