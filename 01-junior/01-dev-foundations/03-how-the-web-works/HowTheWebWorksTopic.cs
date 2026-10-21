using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Junior.DevFoundations.HowTheWebWorks;

public static class HowTheWebWorksTopic
{
    public static int Run(string[] args)
    {
        // J1.3 — Web nasıl çalışır? Ham isteği gösteren küçük Kestrel uygulaması.
        //
        //   dotnet run -- 1.3          → http://localhost:5080
        //   dotnet run -- 1.3 --https  → ayrıca https://localhost:5443  (önce:  dotnet dev-certs https --trust)
        //
        // Her istek konsola HAM haliyle yazılır (bkz. RawRequestLoggingMiddleware). Uç noktalar HTTP kavramlarını gösterir.
        // Denemek için: requests.http (VS Code / Rider / VS) ya da README'deki curl komutları

        var builder = WebApplication.CreateBuilder(args);
        var useHttps = args.Contains("--https");

        builder.WebHost.ConfigureKestrel(kestrel =>
        {
            kestrel.ListenLocalhost(5080, o => o.Protocols = HttpProtocols.Http1);   // düz HTTP, HTTP/1.1

            if (useHttps)
            {
                // HTTPS = HTTP + TLS. UseHttps() geliştirme sertifikasını (dotnet dev-certs) kullanır.
                // Http1AndHttp2: TLS handshake sırasında ALPN ile istemciyle HTTP sürümü pazarlığı yapılır ("h2" veya "http/1.1").
                // (HTTP/3 için ek olarak libmsquic ve Http1AndHttp2AndHttp3 gerekir — kavramsal fark README'de.)
                kestrel.ListenLocalhost(5443, o =>
                {
                    o.Protocols = HttpProtocols.Http1AndHttp2;
                    o.UseHttps();
                });
            }
        });

        builder.Services.AddCors();   // endpoint bazlı policy aşağıda (RequireCors)

        var app = builder.Build();

        app.UseMiddleware<RawRequestLoggingMiddleware>();            // CORS'tan ÖNCE: OPTIONS (preflight) isteğini de görmek için
        app.UseCors();

        // HSTS: "bu siteye bundan sonra sadece HTTPS ile gel" der (tarayıcı 1 yıl hatırlar). Yalnızca HTTPS yanıtlarına eklenir.
        app.Use(async (ctx, next) =>
        {
            if (ctx.Request.IsHttps)
                ctx.Response.Headers.StrictTransportSecurity = "max-age=31536000; includeSubDomains";
            await next();
        });

        // ───────────────────────── 1) Ham isteği geri göster ─────────────────────────
        // Eşleşmeyen HER yol ve HER method buraya düşer. İsteği olduğu gibi yanıt olarak döner (curl -v ile birlikte okuyun).
        app.MapFallback(ctx =>
        {
            ctx.Response.ContentType = "text/plain; charset=utf-8";
            var raw = (string)ctx.Items["raw-request"]!;
            return ctx.Response.WriteAsync(
                $"Sunucunun gördüğü istek (protokol: {ctx.Request.Protocol}, şema: {ctx.Request.Scheme}):\n\n{raw}\n");
        });

        // ───────────────────────── 2) Status code aileleri ─────────────────────────
        // GET /status/204, /status/404, /status/503 …
        app.MapGet("/status/{code:int:range(100,599)}", (int code) => Results.StatusCode(code));

        // 3xx yönlendirme: tarayıcı/curl -L, Location header'ını takip eder.
        app.MapGet("/redirect", () => Results.Redirect("/redirect-target"));                         // 302 Found (geçici)
        app.MapGet("/redirect-permanent", () => Results.Redirect("/redirect-target", permanent: true)); // 301 (kalıcı; tarayıcılar önbelleğe alır)
        app.MapGet("/redirect-target", () => "Yönlendirmenin hedefi");

        // ───────────────────────── 3) Method semantiği: safe & idempotent ─────────────────────────
        // GET (safe+idempotent)  PUT (idempotent)  DELETE (idempotent)  POST (NE safe NE idempotent)
        var counter = 0;
        app.MapGet("/counter", () => new { counter });                                  // okur, durumu değiştirmez → safe
        app.MapPost("/counter/increment", () => new { counter = Interlocked.Increment(ref counter) }); // her çağrı sonucu DEĞİŞTİRİR
        app.MapPut("/counter/{value:int}", (int value) =>                                // aynı çağrıyı 10 kez yapmak = 1 kez yapmak
        {
            Interlocked.Exchange(ref counter, value);
            return new { counter };
        });

        // ───────────────────────── 4) Cookie: HTTP stateless, durumu cookie taşır ─────────────────────────
        app.MapGet("/cookie/set", (HttpContext ctx) =>
        {
            // Set-Cookie yanıt header'ıdır; tarayıcı sonraki HER istekte Cookie header'ıyla geri gönderir.
            ctx.Response.Cookies.Append("session", Guid.NewGuid().ToString("N"), new CookieOptions
            {
                HttpOnly = true,                 // JavaScript okuyamaz → XSS'te çalınmasını zorlaştırır
                Secure = false,                  // gerçek hayatta TRUE: sadece HTTPS ile gönderilir (burada http://localhost deneyi için kapalı)
                SameSite = SameSiteMode.Lax,     // başka site kaynaklı isteklerde otomatik gönderimi kısıtlar (CSRF azaltır)
                MaxAge = TimeSpan.FromMinutes(5),
            });
            return "Cookie set edildi. Şimdi /cookie/read — ve cookie jar'sız (curl -c/-b olmadan) farkı deneyin.";
        });
        app.MapGet("/cookie/read", (HttpContext ctx) =>
            ctx.Request.Cookies.TryGetValue("session", out var v)
                ? $"Sunucu seni tanıdı. session = {v}"
                : "Cookie yok → sunucu seni tanımıyor (HTTP stateless: her istek bağımsızdır).");

        // ───────────────────────── 5) Cache: ETag + If-None-Match → 304 ─────────────────────────
        const string resource = "Bu içerik değişmedikçe istemci tekrar indirmez.";
        app.MapGet("/cache/etag", (HttpContext ctx) =>
        {
            var etag = $"\"{Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(resource)))[..16]}\"";
            ctx.Response.Headers.ETag = etag;
            ctx.Response.Headers.CacheControl = "no-cache";           // "kullanmadan önce sunucuya sor (revalidate)", "önbellekleme" demek DEĞİL

            // İstemci elindeki sürümün ETag'ini If-None-Match ile gönderir; aynıysa gövde göndermeden 304 döneriz.
            if (ctx.Request.Headers.IfNoneMatch == etag)
                return Results.StatusCode(StatusCodes.Status304NotModified);

            return Results.Text(resource);
        });

        // ───────────────────────── 6) CORS ─────────────────────────
        // Tarayıcıda farklı origin'den (örn. http://localhost:3000) fetch → önce OPTIONS (preflight), sonra gerçek istek.
        app.MapMethods("/cors/data", ["GET", "POST", "PUT"], () => new { message = "CORS ile erişilebilen veri" })
           .RequireCors(p => p
               .WithOrigins("http://localhost:3000")
               .WithMethods("GET", "POST", "PUT")
               .WithHeaders("Content-Type", "X-Demo")
               .SetPreflightMaxAge(TimeSpan.FromMinutes(10)));

        app.Run();
        return 0;
    }
}
