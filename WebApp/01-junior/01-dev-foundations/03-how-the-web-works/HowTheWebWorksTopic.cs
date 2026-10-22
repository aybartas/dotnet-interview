using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Junior.DevFoundations.HowTheWebWorks;

/// <summary>
/// J1.3 — How does the web work? A small Kestrel app that shows the raw request.
///
///   dotnet run -- J1.3            → http://localhost:5080
///   dotnet run -- J1.3 --https    → also https://localhost:5443  (first:  dotnet dev-certs https --trust)
///
/// Every request is written to the console in its RAW form (see RawRequestLoggingMiddleware). The endpoints demonstrate HTTP concepts.
/// To try it: requests.http (VS Code / Rider / VS) or the curl commands in the README.
/// </summary>
public static class HowTheWebWorksTopic
{
    public static int Run(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        AddServices(builder, useHttps: args.Contains("--https"));
        var app = builder.Build();
        Map(app);
        app.Run();
        return 0;
    }

    /// <summary>Kestrel ports and the CORS service.</summary>
    private static void AddServices(WebApplicationBuilder builder, bool useHttps)
    {
        builder.WebHost.ConfigureKestrel(kestrel =>
        {
            kestrel.ListenLocalhost(5080, o => o.Protocols = HttpProtocols.Http1);   // plain HTTP, HTTP/1.1

            if (useHttps)
            {
                // HTTPS = HTTP + TLS. UseHttps() uses the development certificate (dotnet dev-certs).
                // Http1AndHttp2: during the TLS handshake the HTTP version is negotiated with the client via ALPN ("h2" or "http/1.1").
                // (HTTP/3 additionally needs libmsquic and Http1AndHttp2AndHttp3 — the conceptual difference is in the README.)
                kestrel.ListenLocalhost(5443, o =>
                {
                    o.Protocols = HttpProtocols.Http1AndHttp2;
                    o.UseHttps();
                });
            }
        });

        builder.Services.AddCors();   // per-endpoint policy below (RequireCors)
    }

    /// <summary>Middleware and endpoints.</summary>
    private static void Map(WebApplication app)
    {
        app.UseMiddleware<RawRequestLoggingMiddleware>();            // BEFORE CORS: so we also see the OPTIONS (preflight) request
        app.UseCors();

        // HSTS: says "from now on, come to this site only over HTTPS" (the browser remembers it for 1 year). Added only to HTTPS responses.
        app.Use(async (ctx, next) =>
        {
            if (ctx.Request.IsHttps)
                ctx.Response.Headers.StrictTransportSecurity = "max-age=31536000; includeSubDomains";
            await next();
        });

        // ───────────────────────── 1) Echo the raw request back ─────────────────────────
        // EVERY unmatched path and EVERY method falls through to here. It returns the request as-is in the response (read it together with curl -v).
        app.MapFallback(ctx =>
        {
            ctx.Response.ContentType = "text/plain; charset=utf-8";
            var raw = (string)ctx.Items["raw-request"]!;
            return ctx.Response.WriteAsync(
                $"The request the server saw (protocol: {ctx.Request.Protocol}, scheme: {ctx.Request.Scheme}):\n\n{raw}\n");
        });

        // ───────────────────────── 2) Status code families ─────────────────────────
        // GET /status/204, /status/404, /status/503 …
        app.MapGet("/status/{code:int:range(100,599)}", (int code) => Results.StatusCode(code));

        // 3xx redirect: the browser / curl -L follows the Location header.
        app.MapGet("/redirect", () => Results.Redirect("/redirect-target"));                         // 302 Found (temporary)
        app.MapGet("/redirect-permanent", () => Results.Redirect("/redirect-target", permanent: true)); // 301 (permanent; browsers cache it)
        app.MapGet("/redirect-target", () => "The redirect target");

        // ───────────────────────── 3) Method semantics: safe & idempotent ─────────────────────────
        // GET (safe+idempotent)  PUT (idempotent)  DELETE (idempotent)  POST (NEITHER safe NOR idempotent)
        var counter = 0;
        app.MapGet("/counter", () => new { counter });                                  // reads, doesn't change state → safe
        app.MapPost("/counter/increment", () => new { counter = Interlocked.Increment(ref counter) }); // every call CHANGES the result
        app.MapPut("/counter/{value:int}", (int value) =>                                // making the same call 10 times = making it once
        {
            Interlocked.Exchange(ref counter, value);
            return new { counter };
        });

        // ───────────────────────── 4) Cookie: HTTP is stateless, the cookie carries the state ─────────────────────────
        app.MapGet("/cookie/set", (HttpContext ctx) =>
        {
            // Set-Cookie is a response header; the browser sends it back in the Cookie header on EVERY following request.
            ctx.Response.Cookies.Append("session", Guid.NewGuid().ToString("N"), new CookieOptions
            {
                HttpOnly = true,                 // JavaScript can't read it → makes it harder to steal via XSS
                Secure = false,                  // in real life TRUE: sent only over HTTPS (off here for the http://localhost experiment)
                SameSite = SameSiteMode.Lax,     // restricts automatic sending on cross-site requests (reduces CSRF)
                MaxAge = TimeSpan.FromMinutes(5),
            });
            return "Cookie set. Now /cookie/read — and try the difference without a cookie jar (without curl -c/-b).";
        });
        app.MapGet("/cookie/read", (HttpContext ctx) =>
            ctx.Request.Cookies.TryGetValue("session", out var v)
                ? $"The server recognized you. session = {v}"
                : "No cookie → the server doesn't recognize you (HTTP is stateless: every request is independent).");

        // ───────────────────────── 5) Cache: ETag + If-None-Match → 304 ─────────────────────────
        const string resource = "The client won't download this content again as long as it doesn't change.";
        app.MapGet("/cache/etag", (HttpContext ctx) =>
        {
            var etag = $"\"{Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(resource)))[..16]}\"";
            ctx.Response.Headers.ETag = etag;
            ctx.Response.Headers.CacheControl = "no-cache";           // means "revalidate with the server before using it", NOT "don't cache"

            // The client sends the ETag of the version it holds in If-None-Match; if it's the same we return 304 without a body.
            if (ctx.Request.Headers.IfNoneMatch == etag)
                return Results.StatusCode(StatusCodes.Status304NotModified);

            return Results.Text(resource);
        });

        // ───────────────────────── 6) CORS ─────────────────────────
        // A fetch from a different origin in the browser (e.g. http://localhost:3000) → first OPTIONS (preflight), then the real request.
        app.MapMethods("/cors/data", ["GET", "POST", "PUT"], () => new { message = "Data accessible via CORS" })
           .RequireCors(p => p
               .WithOrigins("http://localhost:3000")
               .WithMethods("GET", "POST", "PUT")
               .WithHeaders("Content-Type", "X-Demo")
               .SetPreflightMaxAge(TimeSpan.FromMinutes(10)));
    }
}
