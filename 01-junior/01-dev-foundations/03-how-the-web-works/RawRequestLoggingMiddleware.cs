using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Junior.DevFoundations.HowTheWebWorks;

/// <summary>
/// Gelen HTTP isteğini, ağdan gelen HAM metne en yakın biçimde konsola yazar:
///
///     GET /search?q=dotnet HTTP/1.1          ← request line: method + path + query + protokol sürümü
///     Host: localhost:5080                   ← header'lar
///     User-Agent: curl/8.5.0
///                                            ← boş satır: header'ların sonu
///     {"name":"..."}                         ← body (varsa)
///
/// Kestrel isteği zaten ayrıştırıp (parse) HttpContext'e koymuştur; biz onu tekrar "metne" çeviriyoruz.
/// HTTP/1.1 gerçekten bu düz metin formatıyla konuşur (HTTP/2 ve 3 ikilik/binary'dir, ama mantıksal model aynıdır).
/// </summary>
public sealed class RawRequestLoggingMiddleware(RequestDelegate next, ILogger<RawRequestLoggingMiddleware> logger)
{
    private const int MaxBodyBytesToLog = 2048;

    public async Task InvokeAsync(HttpContext ctx)
    {
        var raw = await RenderAsync(ctx.Request);
        ctx.Items["raw-request"] = raw;                       // endpoint'ler isterse cevap gövdesine koyabilsin

        logger.LogInformation("┌── gelen istek (uzak: {Remote}) ──\n{Raw}\n└──", ctx.Connection.RemoteIpAddress, raw);

        await next(ctx);

        logger.LogInformation("  ↳ yanıt: {Status} {Reason}", ctx.Response.StatusCode,
            ReasonPhrases.GetReasonPhrase(ctx.Response.StatusCode));
    }

    private static async Task<string> RenderAsync(HttpRequest req)
    {
        var sb = new StringBuilder();

        // 1) Request line
        sb.Append(req.Method).Append(' ').Append(req.Path).Append(req.QueryString).Append(' ').AppendLine(req.Protocol);

        // 2) Header'lar (Kestrel Host'u da buraya koyar)
        foreach (var (name, values) in req.Headers)
            sb.Append(name).Append(": ").AppendLine(values.ToString());

        // 3) Boş satır + body
        sb.AppendLine();
        if (req.ContentLength is > 0 || req.Headers.ContainsKey("Transfer-Encoding"))
        {
            req.EnableBuffering();                              // body'yi sonradan tekrar okuyabilmek için
            using var reader = new StreamReader(req.Body, Encoding.UTF8, leaveOpen: true);
            var buffer = new char[MaxBodyBytesToLog];
            var read = await reader.ReadBlockAsync(buffer, 0, buffer.Length);
            sb.Append(buffer, 0, read);
            if (read == MaxBodyBytesToLog) sb.Append(" …(kesildi)");
            req.Body.Position = 0;
        }

        return sb.ToString().TrimEnd();
    }
}
