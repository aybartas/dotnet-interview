# J6.4 Middleware Pipeline Temelleri ⭐

> 📁 `WebApp/01-junior/06-aspnetcore-fundamentals/04-middleware-basics/` · Önkoşul: J2–J4 · Sorular: [`interview-questions.md`](interview-questions.md)

## İçindekiler

1. [Ne?](#1-ne)
2. [Neden?](#2-neden)
3. [Nasıl?](#3-nasıl)
4. [Gerçek dünya senaryosu](#4-gerçek-dünya-senaryosu)
5. [Mülakat perspektifi](#5-mülakat-perspektifi)
6. [Yaygın hatalar](#6-yaygın-hatalar)
7. [Performans & trade-off](#7-performans--trade-off)
8. [Kaynaklar](#8-kaynaklar)

**Bu konudaki kod:** [`MiddlewareBasicsTopic.cs`](MiddlewareBasicsTopic.cs) (ana pipeline + `Use`/`Run`/`Map`/`UseWhen`/`MapWhen`) · [`RequestTimingMiddleware.cs`](RequestTimingMiddleware.cs) · [`CorrelationIdMiddleware.cs`](CorrelationIdMiddleware.cs) · [`OrderExperiment.cs`](OrderExperiment.cs) (sıralama deneyi) · [`ExperimentMiddlewares.cs`](ExperimentMiddlewares.cs) (deneyin yardımcı middleware'leri) · [`PipelineTrace.cs`](PipelineTrace.cs) (sırayı gösteren iz) · [`requests.http`](requests.http)

---

## 1. Ne?

**Middleware**, HTTP request/response yolu üzerinde **zincir halinde dizilmiş küçük bileşenlerdir**. Her middleware bir `HttpContext` alır ve iki şeyden birini yapar: işi kendisi bitirip cevabı yazar (**short-circuit**) ya da `next()` çağırıp isteği zincirdeki bir sonraki middleware'e devreder. ASP.NET Core uygulamasının davranışı büyük ölçüde bu zincirin **kendisidir**: HTTPS yönlendirme, static dosyalar, routing, CORS, kimlik doğrulama, yetkilendirme, hata yakalama… hepsi birer middleware'dir (→ J6.1: Kestrel isteği alır, pipeline'a verir).

### Zihinsel model: iç içe halkalar

Pipeline bir "ardışık liste" gibi değil, **iç içe geçmiş halkalar** (soğan) gibi düşünülmelidir. İstek dıştan içe girer, cevap içten dışa çıkar. `await next()` satırı iki dünyayı ayırır:

```
 request ───►  ┌─ A ─────────────────────────────────┐
               │  A: next()'ten ÖNCE (request tarafı)│
               │   ┌─ B ──────────────────────────┐  │
               │   │  B: next()'ten ÖNCE          │  │
               │   │   ┌─ endpoint ────────────┐  │  │
               │   │   │   cevabı üretir       │  │  │
               │   │   └───────────────────────┘  │  │
               │   │  B: next()'ten SONRA         │  │
               │   └──────────────────────────────┘  │
               │  A: next()'ten SONRA (response)     │
               └─────────────────────────────────────┘  ───► response
```

```csharp
app.Use(async (ctx, next) =>
{
    // 1) request tarafı: istek içeri girerken (iç halkalar henüz çalışmadı)
    await next(ctx);
    // 2) response tarafı: iç halkalar bitti, cevap dışarı çıkıyor
});
```

Üç temel sonuç:
- **Sıra = davranış.** Kod `Program.cs`'te hangi sırayla yazılmışsa istek o sırayla içeri girer, **ters sırayla** çıkar.
- **`next()` çağırmayan middleware zinciri keser** (short-circuit): iç halkalar isteği hiç görmez, ama dış halkalar cevabı yine görür.
- Dıştaki halka içtekinin **exception'ını da, süresini de, cevabını da** "görür"; içteki dışta olanı göremez.

---

## 2. Neden?

- **Kesişen ilgiler (cross-cutting concerns)** — loglama, kimlik doğrulama, hata yakalama, CORS, sıkıştırma, correlation ID — her endpoint'te tekrar yazılmaz; pipeline'da **bir kez** tanımlanır.
- **Sıra hataları sessiz ve pahalıdır:** yanlış yerdeki `UseAuthorization` güvenlik açığıdır, yanlış yerdeki exception handler hataları yakalamaz, yanlış yerdeki timing yanlış süre raporlar. Bu yüzden "middleware sıralaması neden önemli?" ASP.NET Core mülakatlarının neredeyse sabit sorusudur.
- Production'da "bu isteğin log'ları hangileri?" sorusunu **correlation ID** cevaplar; "yavaşlık nerede?" sorusunu **timing** header'ı. İkisini de elle yazıp sıralama hatalarını görmek middleware'i gerçekten öğretir.

---

## 3. Nasıl?

### 3.1 Çalıştırma

```bash
cd WebApp
dotnet run -- J6.4          # http://localhost:5080  (Ctrl+C ile durdur)
```

Ana sayfa (`/`) tüm denenebilecek URL'lerin listesini döner. Konsol log'unda scope'lar görünür (`=> CorrelationId=...`).

### 3.2 `Use` vs `Run` vs `Map` (+ `UseWhen` / `MapWhen`)

| Metot | Ne yapar | `next` var mı? | Ana pipeline'a döner mi? |
|-------|----------|----------------|--------------------------|
| `Use(async (ctx, next) => …)` | Zincire bir halka ekler | ✅ | ✅ (zincir devam eder) |
| `Run(async ctx => …)` | **Terminal** middleware: zinciri bitirir | ❌ | — (arkasına eklenen kod hiç çalışmaz) |
| `Map("/yol", branch => …)` | Path prefix'ine göre **dal** açar; prefix `Path`'ten `PathBase`'e taşınır | dal içinde | ❌ (dal kendi içinde biter) |
| `MapWhen(predicate, branch => …)` | Herhangi bir koşula göre dal | dal içinde | ❌ |
| `UseWhen(predicate, branch => …)` | Koşullu dal | dal içinde | ✅ (dal `next()` çağırırsa ana zincire **geri döner**) |

```csharp
app.Map("/map-demo", b => b.Run(ctx =>
    ctx.Response.WriteAsync($"PathBase='{ctx.Request.PathBase}'  Path='{ctx.Request.Path}'")));

app.UseWhen(ctx => ctx.Request.Query.ContainsKey("debug"),
    b => b.Use(async (ctx, next) => { ctx.Response.Headers["X-Debug"] = "…"; await next(); }));
```

```bash
curl -s localhost:5080/map-demo/a/b
# Map branch.  PathBase='/map-demo'  Path='/a/b'  (the main pipeline below never ran)

curl -si "localhost:5080/hello?debug" | grep -i x-debug
# X-Debug: UseWhen branch ran, then rejoined the main pipeline

curl -s "localhost:5080/hello?mapwhen"
# MapWhen branch: the predicate matched, the main pipeline was skipped
```

> **Tuzak — `Map` dalı, kendisinden önce kayıtlı middleware'leri miras alır.** Dal, `Map` çağrısının yapıldığı noktadaki pipeline'dan kesilir. Bu projede deney dalları (`/order-a`, `/order-b`) bu yüzden ana pipeline'dan **önce** kaydedilir; yoksa timing/correlation iki kez çalışırdı.

### 3.3 Short-circuit: `next()` çağrılmazsa

```csharp
app.Use(async (ctx, next) =>
{
    if (ctx.Request.Path == "/blocked")
    {
        ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
        await ctx.Response.WriteAsync("blocked: this middleware did not call next(), so the terminal Run below never ran");
        return;                         // ← no next()
    }
    await next(ctx);
});
```

```text
$ curl -si localhost:5080/blocked
HTTP/1.1 403 Forbidden
X-Correlation-Id: 8f170659f8954a958d2c887972f6eeb7      ← dış halkalar (CorrelationId, Timing) cevabı yine işledi
X-Elapsed-Ms: 0.2

blocked: this middleware did not call next(), so the terminal Run below never ran
```

Short-circuit **kusur değil, araçtır:** authentication reddi (401), CORS preflight cevabı, `UseHttpsRedirection`'ın 307'si, static file cevabı, health check cevabı hep böyle çalışır. Hata, *istemeden* `next()`'i unutmaktır (→ 6. bölüm).

### 3.4 Custom middleware yazmanın üç yolu

| Yol | Ne zaman | Ömür / DI |
|-----|----------|-----------|
| **Inline** — `app.Use(async (ctx, next) => …)` | Küçük, tek seferlik mantık, demo | Lambda; servisler `ctx.RequestServices` ile |
| **Conventional class** — `ctor(RequestDelegate next, …)` + `InvokeAsync(HttpContext)` | **Varsayılan tercih**; yeniden kullanılabilir, test edilebilir | **Uygulama başına tek instance (singleton).** Ctor'a sadece singleton-güvenli servis; scoped servisler `InvokeAsync` parametresi olarak |
| **`IMiddleware`** — `InvokeAsync(HttpContext, RequestDelegate)` | Ctor'da **scoped** servis (örn. `DbContext`) istiyorsan | `IMiddlewareFactory` her istekte DI'dan çözer; **servis olarak kaydedilmesi şart** |

```csharp
// IMiddleware: ctor'a scoped servis girebilir; AddScoped/AddTransient ile kayıt şart, sonra UseMiddleware<T>()
public sealed class AuditMiddleware(AppDbContext db) : IMiddleware
{
    public async Task InvokeAsync(HttpContext ctx, RequestDelegate next) { /* … */ await next(ctx); }
}
builder.Services.AddScoped<AuditMiddleware>();
app.UseMiddleware<AuditMiddleware>();

// Conventional class'ta scoped servis: ctor'a DEĞİL, InvokeAsync parametresine
public async Task InvokeAsync(HttpContext ctx, AppDbContext db) { … }
```

> Conventional middleware singleton olduğu için **per-request state'i field'da tutma**; yerel değişken ya da `HttpContext.Items` kullan. Ctor'a scoped servis enjekte etmek, captive dependency hatasıdır (→ J6.2).

### 3.5 Middleware #1 — `RequestTimingMiddleware` (ve `OnStarting` tuzağı)

[`RequestTimingMiddleware.cs`](RequestTimingMiddleware.cs) süreyi ölçüp `X-Elapsed-Ms` header'ına yazar. Akla gelen ilk kod şudur:

```csharp
await next(ctx);
ctx.Response.Headers["X-Elapsed-Ms"] = …;   // ❌ çoğu zaman çok geç
```

`next()` döndüğünde endpoint genelde body'yi yazmıştır; **status line ve header'lar istemciye gitmeye başlamıştır** (`Response.HasStarted == true`). Header'lar artık salt okunurdur. Gerçek çıktı — `/pitfall/naive` ([`NaiveTimingMiddleware`](ExperimentMiddlewares.cs)):

```text
$ curl -si localhost:5080/pitfall/naive          # X-Elapsed-Ms YOK
HTTP/1.1 200 OK

header set AFTER next() — check the response headers and the server log

# sunucu log'u:
fail: …NaiveTimingMiddleware[0] … Naive timing failed (HasStarted=True): Headers are read-only, response has already started.
```

Doğrusu: header'ı **`Response.OnStarting`** callback'inde yazmak. Framework callback'i header'lar gönderilmeden hemen önce çalıştırır:

```csharp
var start = Stopwatch.GetTimestamp();
ctx.Response.OnStarting(() =>
{
    ctx.Response.Headers["X-Elapsed-Ms"] =
        Stopwatch.GetElapsedTime(start).TotalMilliseconds.ToString("F1", CultureInfo.InvariantCulture);
    return Task.CompletedTask;
});
await next(ctx);
```

```text
$ curl -si localhost:5080/pitfall/ok
HTTP/1.1 200 OK
X-Elapsed-Ms: 0.4
```

Not: bu süre "header'ların gönderildiği ana kadar geçen süre"dir (time-to-first-byte). Büyük bir body'nin stream edilme süresini kapsamaz; onu `finally`/`OnCompleted` ile ölçersin (ama header'a yazamazsın, log'a yazarsın).

### 3.6 Middleware #2 — `CorrelationIdMiddleware`

[`CorrelationIdMiddleware.cs`](CorrelationIdMiddleware.cs) şunları yapar:

1. Gelen `X-Correlation-Id` header'ını okur (API gateway / çağıran servis vermiş olabilir), yoksa yeni üretir. Header kullanıcı girdisi olduğu ve log'a gittiği için **doğrulanır** (`[A-Za-z0-9_-]`, en fazla 64 karakter; aksi halde *log forging*).
2. `HttpContext.Items["CorrelationId"]`'a koyar (aynı isteği işleyen sonraki kod okuyabilsin).
3. Cevap header'ına yazar (yine `OnStarting` ile).
4. **Logger scope** açar: `using (logger.BeginScope("CorrelationId={CorrelationId}", id)) { await next(ctx); }` — scope içinde yazılan **her** log satırı id'yi otomatik taşır.

```text
$ curl -si localhost:5080/hello -H "X-Correlation-Id: demo-42"
HTTP/1.1 200 OK
X-Correlation-Id: demo-42
X-Elapsed-Ms: 3.7

hello from the terminal Run middleware. correlation id: demo-42

# sunucu log'u (ConnectionId/RequestPath scope'ları kısaltıldı):
info: InlineLogger[0] => … => CorrelationId=demo-42 inline Use: before next() — GET /hello
info: InlineLogger[0] => … => CorrelationId=demo-42 inline Use: after next() — status 200
```

### 3.7 Sıralama deneyi: aynı 7 middleware, iki farklı sıra

[`OrderExperiment.cs`](OrderExperiment.cs) iki dal kurar. Aynı yedi middleware'i **farklı sırayla** ekler; cevap gövdesinde middleware'lerin çalışma sırası (iç içe girintiyle) döner:

```text
/order-a  (önerilen)     ExceptionHandler → Timing → CorrelationId → Logging → Slow(150 ms) → Gate → Faulty → endpoint
/order-b  (karıştırılmış) Slow(150 ms) → Gate → Logging → Timing → CorrelationId → Faulty → ExceptionHandler → endpoint
```

- `Slow` her istekte 150 ms bekler; `Gate` `?deny=1` ile **short-circuit** eder (403); `Faulty` `?fail=1` ile **exception fırlatır**; endpoint `?boom=1` ile exception fırlatır.
- Cevap gövdesi, `TraceReportMiddleware` (deney düzeneği, en dış halka) sayesinde "← after" satırlarını da içerir: header'lar gönderildikten sonra gövde değişemeyeceği için bu halka iç body'yi tamponlar, pipeline açılınca raporu yazar.

#### Deney 1 — normal istek: süre ölçümü ve iz

```text
$ curl -si localhost:5080/order-a/hello
HTTP/1.1 200 OK
X-Correlation-Id: b41fc29d75de4d9987e3fe6e9245ae00
X-Elapsed-Ms: 170.4                                       ← Slow'un 150 ms'si DAHİL (Timing, Slow'un dışında)

order-a (recommended order) — GET /order-a/hello
status: 200

→ ExceptionHandler
  → Timing  (starts the stopwatch)
    → CorrelationId  (generated)
      → Logging
        → Slow  (waits 150 ms)
          → Gate
            → Faulty
              · endpoint reached
            ← Faulty
          ← Gate
        ← Slow
      ← Logging
    ← CorrelationId
  ← Timing
← ExceptionHandler
```

```text
$ curl -si localhost:5080/order-b/hello -H "X-Correlation-Id: demo-42"
HTTP/1.1 200 OK
X-Correlation-Id: demo-42
X-Elapsed-Ms: 2.9                                         ← 150 ms nereye gitti? Timing, Slow'un İÇİNDE; Slow'u hiç görmedi

→ Slow  (waits 150 ms)
  → Gate
    → Logging
      → Timing  (starts the stopwatch)
        → CorrelationId  (reused from the request)
          → Faulty
            → ExceptionHandler
              · endpoint reached
            …
```

**Neden?** Kronometre, `Timing` halkasına girildiği anda başlar. `/order-b`'de bu, `Slow` halkasından *sonradır*; yavaş middleware ölçümün dışında kalır. Timing middleware'i "toplam süre" iddia ediyorsa **en dışa yakın** durmalıdır (exception handler'ın hemen içinde).

#### Deney 2 — short-circuit (`?deny=1`): hangi halkalar cevabı görür?

```text
$ curl -si "localhost:5080/order-a/hello?deny=1" -H "X-Correlation-Id: demo-42"
HTTP/1.1 403 Forbidden
X-Correlation-Id: demo-42                                 ← Gate, Timing ve CorrelationId'nin İÇİNDE; ikisi de cevabı işledi
X-Elapsed-Ms: 159.2

$ curl -si "localhost:5080/order-b/hello?deny=1" -H "X-Correlation-Id: demo-42"
HTTP/1.1 403 Forbidden                                    ← header yok: Gate hepsinin DIŞINDA, onlar hiç çalışmadı
Content-Type: text/plain; charset=utf-8

→ Slow  (waits 150 ms)
  → Gate
    · ✗ short-circuit: next() is NOT called, answering 403
  ← Gate
← Slow
```

Sunucu log'u: `/order-a` 403'ü `--> GET /order-a/x` / `<-- 403` olarak **correlation id ile** loglanır; `/order-b` 403'ü **hiç loglanmaz** (Logging, Gate'in içinde kaldı). Reddedilen istekler tam da izlemek istediğin isteklerdir — "kim bana 403 yedirdi?" sorusu `/order-b`'de cevapsız kalır.

#### Deney 3 — exception: handler başta vs sonda

`?fail=1` → `FaultyMiddleware` (bir middleware'de bug):

```text
$ curl -si "localhost:5080/order-a/hello?fail=1"
HTTP/1.1 500 Internal Server Error
X-Correlation-Id: demo-42
X-Elapsed-Ms: 152.1

→ ExceptionHandler
  → Timing … → Faulty
              · ✗ caught InvalidOperationException: "Faulty middleware failed on purpose (?fail=1)"  → answering 500
            ← ExceptionHandler
body written by the pipeline:
  {"error":"internal server error (handled by ExceptionHandlingMiddleware)"}

$ curl -si "localhost:5080/order-b/hello?fail=1"
HTTP/1.1 500 Internal Server Error
Content-Length: 0                                         ← boş 500; ne JSON, ne correlation id
```

`/order-b`'de sunucu log'u Kestrel'den gelir: `fail: Microsoft.AspNetCore.Server.Kestrel[13] … An unhandled exception was thrown by the application. System.InvalidOperationException: Faulty middleware failed on purpose…`. Handler `Faulty`'nin **içinde** (sonra kayıtlı) olduğu için onu yakalayamadı; exception halkalardan **dışarı** doğru yükselir ve yalnızca *dıştaki* try/catch'ler yakalar.

`?boom=1` (exception **endpoint**'ten): iki dalda da yakalanır, çünkü handler endpoint'in dışındadır. Fark iz'de görünür: `/order-b`'de handler en içte olduğu için `← ExceptionHandler`, `← Faulty`, `← CorrelationId`, `← Timing`… satırları normal sırayla tamamlanır; `/order-a`'da handler en dıştadır, exception `Faulty`/`Gate`/`Slow`/`Logging`/`CorrelationId`/`Timing` halkalarının `next()` sonrasındaki kodunu **atlayarak** doğrudan ona ulaşır — iz'de bu halkaların `←` satırları yoktur, `RequestLoggingMiddleware`'in `<-- 500` log satırı da hiç yazılmaz. (`try/finally` kullanmayan her middleware için aynısı geçerlidir.) Buna rağmen `/order-a`'nın 500 cevabında `X-Elapsed-Ms` ve `X-Correlation-Id` vardır: header'lar `OnStarting` ile kayıtlıdır ve handler cevabı yazarken çalışırlar — `OnStarting`'in bir başka avantajı.

#### Deney 4 — log scope: Logging, CorrelationId'den önce mi sonra mı?

```text
# /order-a  (CorrelationId dışta → Logging scope'un İÇİNDE)
info: …RequestLoggingMiddleware[0] => ConnectionId:… => RequestPath:/order-a/x … => CorrelationId=demo-42 --> GET /order-a/x
info: …RequestLoggingMiddleware[0] => ConnectionId:… => RequestPath:/order-a/x … => CorrelationId=demo-42 <-- 200

# /order-b  (Logging dışta → scope henüz açılmadı)
info: …RequestLoggingMiddleware[0] => ConnectionId:… => RequestPath:/order-b/x … --> GET /order-b/x
info: …RequestLoggingMiddleware[0] => ConnectionId:… => RequestPath:/order-b/x … <-- 200
```

Aynı kod, tek fark sıra: `/order-b`'nin log satırları correlation id taşımıyor; log aramasında bu istekle ilişkilendirilemez. (`ExceptionHandler`'ın `/order-b?boom=1` log'u id'yi taşıyor çünkü o `CorrelationId`'nin içinde; `Logging` ise dışında.)

#### Özet tablosu

| Soru | `/order-a` (önerilen) | `/order-b` (karışık) | Neden |
|------|----------------------|----------------------|-------|
| `X-Elapsed-Ms` (normal istek) | ~150–170 | ~3 | Timing, Slow'un dışında / içinde |
| 403 cevabında `X-Correlation-Id` / `X-Elapsed-Ms` | var / var | **yok** | Gate, bu iki halkanın içinde / dışında |
| 403 loglanıyor mu? | evet, id'li | **hayır** | Logging, Gate'in içinde / dışında |
| `Faulty` exception'ı | handler yakalar → JSON 500 | **yakalanmaz** → boş 500 + Kestrel hata log'u | Handler, Faulty'nin dışında / içinde |
| Log satırlarında CorrelationId | var | **yok** | Scope, Logging'i kapsıyor / kapsamıyor |

### 3.8 Sıra neden kritik? Gerçek bir uygulamanın tipik sırası

Genel kural: **dışa yakın olması gerekenler** (hepsini sarmalaması gereken) başta, **isteği reddedebilecekler** (auth, rate limit) endpoint'ten önce, **endpoint'i çalıştıran** en sonda.

```csharp
app.UseExceptionHandler("/error");   // 1) EN BAŞTA: sonrakilerin tüm exception'larını yakalar (Development'ta UseDeveloperExceptionPage) (→ J6.6)
app.UseHsts();                       // 2) (yalnızca production) tarayıcıya "hep HTTPS" der
app.UseHttpsRedirection();           // 3) HTTP → HTTPS yönlendir
app.UseStaticFiles();                // 4) wwwroot dosyaları: routing/auth'a girmeden short-circuit eder (ucuz ve herkese açık)
app.UseRouting();                    // 5) hangi endpoint? — sonraki middleware'ler seçilen endpoint'i görebilir
app.UseCors();                       // 6) UseRouting'ten sonra, UseAuthentication/UseAuthorization'dan ÖNCE
app.UseAuthentication();             // 7) KİM? — kimliği çözer, HttpContext.User'ı doldurur
app.UseAuthorization();              // 8) YAPABİLİR Mİ? — User'a bakar; bu yüzden Authentication'dan SONRA
// custom middleware'ler ihtiyaca göre (genelde Routing/Auth çevresinde)
app.MapControllers();                // 9) endpoint'ler (minimal API'de MapGet/MapPost…)
```

| Kural | Sebep |
|-------|-------|
| `UseExceptionHandler` **ilk** | Yalnızca kendinden *sonra* gelenlerin hatalarını yakalar (Deney 3) |
| `UseAuthentication` → `UseAuthorization` | Yetkilendirme `HttpContext.User`'a bakar; kimlik henüz çözülmediyse herkes "anonim" görünür → hep 401/403 ya da (kötüsü) yanlış karar |
| `UseStaticFiles` routing'ten **önce** | CSS/JS isteği auth/routing maliyetine girmeden cevaplanır |
| `UseCors` auth'tan **önce** | CORS preflight (`OPTIONS`) kimlik taşımaz; auth önce çalışırsa preflight 401 alır, tarayıcı isteği iptal eder |
| `UseHttpsRedirection` erken | Hassas veriyi (cookie/token) taşıyan bir şey çalışmadan önce HTTPS'e yönlendir |
| `UseRouting` ↔ `UseAuthorization` | Authorization, seçilmiş endpoint'in `[Authorize]` metadata'sını okur, bu yüzden routing'ten sonra |

> `WebApplication` (minimal hosting) `UseRouting`'i pipeline'ın başına, endpoint çalıştırmayı sonuna **kendisi** ekler; `UseRouting()`'i elle yalnızca middleware'leri routing'e *göre* konumlandırmak için (CORS/auth'u routing'ten sonra koymak) çağırırsın. Tam sıra listesi ve diğer middleware'ler (rate limiter, response compression/caching, session, request localization) için resmî "Middleware order" sayfasına bak (→ 8. bölüm); derin hali → M3.

---

## 4. Gerçek dünya senaryosu

**Mikroservis ortamında "bu 500'ü kim üretti?"** — Bir sipariş isteği gateway → Orders API → Payments API zincirinden geçiyor. Gateway `X-Correlation-Id` üretir; her servis `CorrelationIdMiddleware` ile **aynı id'yi alır**, log scope'una koyar ve giden `HttpClient` çağrılarına header olarak ekler. Sonuç: log sisteminde (Seq, ELK, Application Insights) `CorrelationId = "…"` ile aratınca **üç servisin satırları tek akışta** görünür. Kullanıcıya dönen cevapta da id olduğu için destek ekibi "şu id ile hata aldım" cümlesiyle doğrudan log'a gider. (Bu mantığın standart hali W3C `traceparent` / OpenTelemetry'dir → M13.4; correlation id onun basit habercisidir.)

Timing header'ı ise Network sekmesinde ya da `curl -i` ile "yavaşlık sunucuda mı, ağda mı?" sorusunu 10 saniyede cevaplar. Sıra bilgisi burada devreye girer: ölçüm halkası exception handler'ın hemen *içinde* durmalı ki hata cevapları da süre ve id taşısın.

---

## 5. Mülakat perspektifi

- ⭐ "**Middleware nedir? Pipeline nasıl çalışır?**" → iç içe halkalar, `next()` öncesi request tarafı / sonrası response tarafı.
- ⭐ "**Middleware sıralaması neden önemli?** Örnek ver." → exception handler ilk; Authentication → Authorization; CORS/static files konumu; deneyden bir örnek (timing + slow).
- "`Use`, `Run`, `Map` farkı?" → tabloyu söyle; `UseWhen` ile `MapWhen`'in farkı (geri dönüş) çıkarsa artı puan.
- "`next()` çağırmazsan ne olur?" → short-circuit; dış halkalar cevabı yine görür.
- "Custom middleware nasıl yazılır? Conventional ile `IMiddleware` farkı?" → singleton vs factory/per-request; scoped servis ctor'da nerede olur.
- Seviye ayrımı: Junior sırayı ve `Use/Run/Map`'i bilir; Mid `IMiddleware`, `HttpContext.Items`, `OnStarting`, endpoint filter ile farkını ve pipeline'ı test etmeyi bilir (→ M3.4, M3.5).
- Tuzak sorular: "Response başladıktan sonra header yazılabilir mi?" (hayır), "Exception handler en sona konursa?" (yakalamaz), "Middleware'de body'yi okursan endpoint ne görür?" (`EnableBuffering` yoksa boş).

---

## 6. Yaygın hatalar

| Hata | Belirti | Çözüm |
|------|---------|-------|
| **`await next()` yazmayı unutmak** (ya da `await`'siz `next()`) | İstek takılır/boş cevap döner; sonraki middleware ve endpoint hiç çalışmaz. Await'siz çağrıda response tamamlanmadan dönülür → race, `ObjectDisposedException` | `await next(ctx);` — short-circuit ise bilinçli ve `return` ile |
| **Response başladıktan sonra header/status yazmak** | `InvalidOperationException: Headers are read-only, response has already started` (ya da değer sessizce kaybolur) | `Response.OnStarting(...)`; yazmadan önce `ctx.Response.HasStarted` kontrolü |
| **Exception handler'ı yanlış yere koymak** | Üstteki middleware'lerin hataları yakalanmaz → boş 500 (Deney 3) | `UseExceptionHandler` **ilk** satır |
| **`UseAuthorization`'ı `UseAuthentication`'dan önce koymak** | `[Authorize]` olan her şey 401; ya da kimlik çözülmeden verilen yanlış karar | Önce Authentication, sonra Authorization |
| **Request body'yi iki kez okumak** | İkinci okuma boş: `Request.Body` ileri-yönlü akıştır | `ctx.Request.EnableBuffering();` oku, sonra `Request.Body.Position = 0;` (örnek: `RawRequestLoggingMiddleware`, → J1.3). Büyük body'de tamponlama bellek maliyetidir |
| **Response body'sini yazdıktan sonra değiştirmeye çalışmak** | Cevap başladıysa yapılamaz; exception handler da yeni cevap yazamaz ("the response has already started") | Body'yi değiştirecek middleware, `Response.Body`'yi bir `MemoryStream` ile değiştirip sonda geri yazar (bkz. `TraceReportMiddleware`) — ama maliyetlidir |
| **Middleware field'ında per-request state tutmak** | Eşzamanlı isteklerde veri karışır (instance singleton) | Yerel değişken ya da `HttpContext.Items` |
| **Ctor'a scoped servis enjekte etmek** (conventional middleware) | Captive dependency / `ValidateScopes` hatası | `InvokeAsync` parametresi ya da `IMiddleware` |
| **`Map` dalının önceden kayıtlı middleware'leri miras aldığını unutmak** | Aynı middleware iki kez çalışır | `Map`'i ilgili middleware'lerden önce çağır |
| **Kullanıcı girdisini (header) doğrulamadan log'a/header'a koymak** | Log forging, header injection | Correlation id'yi doğrula (uzunluk + karakter kümesi) |
| **Her şeyi middleware'e koymak** | Sadece birkaç endpoint'i ilgilendiren mantık tüm istekleri yavaşlatır | Endpoint'e özgü mantık için filter / endpoint metadata (→ M3) |

---

## 7. Performans & trade-off

- Her istek **tüm ilgili halkalardan** geçer; middleware sayısı arttıkça sabit bir maliyet eklenir. Ucuz, sık reddeden middleware'leri (static files, health check, rate limiter) **önce** koymak pahalı işi (auth, DB) hiç çalıştırmadan keser.
- `async` middleware'de `await next()` doğal maliyetli değildir; ama **senkron bloklama** (`.Result`, `Thread.Sleep`) thread pool'u tüketir (→ J4).
- `app.Use(async (ctx, next) => await next())` biçimi her istekte closure tahsis eder; `RequestDelegate` alan `Use(async (ctx, next) => await next(ctx))` overload'ı (.NET 7 ile geldiği hatırlanıyor; sürümü doğrula) ya da class middleware tercih edilir. Hot path'te ölçmeden önemseme, ama bilgin olsun.
- `EnableBuffering` body'yi bellekte (ve belli eşikten sonra diskte) tamponlar: büyük upload'larda pahalıdır; sadece gerekirse aç.
- Response'u tamponlayıp sonra yazmak (bu konudaki `TraceReportMiddleware`) streaming'i bozar ve belleği şişirir; production'da kaçın. Header için `OnStarting`, "bittikten sonra bir şey yap" için `OnCompleted` kullan.
- Class middleware'in `InvokeAsync` metodu startup'ta (pipeline kurulurken) bir kez çözümlenir; her istekte reflection yapılmaz, bu yüzden "reflection yavaş" kaygısı yersizdir. `IMiddleware` ise her istekte `IMiddlewareFactory` üzerinden DI çözümlemesi yapar: biraz daha pahalı ama scoped state için doğru araç.

---

## 8. Kaynaklar

- Microsoft Learn — *ASP.NET Core Middleware* (pipeline diyagramı, `Use`/`Run`/`Map`, **Middleware order** bölümü): https://learn.microsoft.com/aspnet/core/fundamentals/middleware/
- Microsoft Learn — *Write custom ASP.NET Core middleware* (conventional vs `IMiddleware`, `InvokeAsync` parametre DI): https://learn.microsoft.com/aspnet/core/fundamentals/middleware/write
- Microsoft Learn — *Factory-based middleware activation (`IMiddlewareFactory`)*: https://learn.microsoft.com/aspnet/core/fundamentals/middleware/extensibility
- Microsoft Learn — *Handle errors in ASP.NET Core* (`UseExceptionHandler`, "response has already started"): https://learn.microsoft.com/aspnet/core/fundamentals/error-handling
- Bu repoda: J6.1 (Kestrel → pipeline), J6.2 (DI lifetime), J6.5 (logging scope), J6.6 (exception handler & ortamlar), J1.3 (`RawRequestLoggingMiddleware`, `EnableBuffering`), M3.4 (derin middleware), M3.5 (filters).

<details>
<summary>Alıştırmalar ve çözümleri</summary>

1. `/order-a`'da `Faulty` ve `Gate`'in yerini değiştir; `?deny=1&fail=1` ile ne olur? *(Çözüm: ikisi de aynı istekte tetiklenir; sırada önce hangisi varsa o kazanır. `Gate` öndeyse 403 döner ve `Faulty` hiç çalışmaz; `Faulty` öndeyse exception fırlar ve handler'a gider.)*
2. `NaiveTimingMiddleware`'i `HasStarted` kontrolü ile "düzeltmeye" çalış. Kontrolün neden yetmediğini açıkla. *(Çözüm: `next()` döndüğünde `HasStarted` çoğu zaman zaten `true`'dur; kontrol hatayı önlese bile header'ı yazamazsın. Tek çözüm `OnStarting`.)*
3. `/order-b`'de `ExceptionHandlingMiddleware`'i `TraceReportMiddleware`'in hemen arkasına (en dışa) taşı ve `?fail=1` dene. *(Çözüm: JSON 500 gelir; `Timing` ve `CorrelationId` `Faulty`'den önce çalıştıkları ve `OnStarting` kaydettikleri için header'ları da cevapta olur. Logging satırlarında id yine yoktur, çünkü sıra o kısmı değiştirmedi.)*
4. Kendi middleware'ini yaz: `?slow=1` olduğunda `Task.Delay` ekleyen, ya da `X-Powered-By` header'ını silen bir `OnStarting` middleware'i.

</details>
