# J6.6 Ortamlar & Hata Sayfaları

> 📁 `WebApp/01-junior/06-aspnetcore-fundamentals/06-environments/` · Önkoşul: J2–J4 · Sorular: [`interview-questions.md`](interview-questions.md)

## İçindekiler

1. [Ne?](#1-ne)
2. [Neden?](#2-neden)
3. [Nasıl?](#3-nasıl)
4. [Gerçek dünya senaryosu](#4-gerçek-dünya-senaryosu)
5. [Mülakat perspektifi](#5-mülakat-perspektifi)
6. [Yaygın hatalar](#6-yaygın-hatalar)
7. [Performans & trade-off](#7-performans--trade-off)
8. [Kaynaklar](#8-kaynaklar)

---

## 1. Ne?

**Ortam (environment)**, aynı kodun *nerede* çalıştığını söyleyen bir isimdir: `Development`, `Staging`, `Production` (ya da kendi verdiğiniz `Test`, `UAT` gibi isimler). ASP.NET Core bu ismi host kurulurken okur ve `IHostEnvironment` / `IWebHostEnvironment` olarak servis eder. Ortama göre değişen şeyler:

- hangi **hata sayfası** gösterilir (ayrıntılı geliştirici sayfası mı, sade/generic bir yanıt mı),
- hangi **configuration dosyaları** yüklenir (`appsettings.{Environment}.json`, User Secrets → J6.3),
- DI doğrulaması (Development'ta `ValidateScopes` + `ValidateOnBuild` açıktır, dokümante edilmiş varsayılan),
- HSTS, detaylı loglama gibi güvenlik ve diagnostik ayarları.

Bu topic'in kodu (`EnvironmentsTopic.cs` + `ErrorPipeline.cs`) tek bir hatayı üç yerde gösterir: `/env` (hangi ortam, neden), `/boom` (patlayan endpoint) ve `/compare` (aynı hatanın Development ve Production'da yan yana görünümü).

| Ortam | Tipik amaç | Hata gösterimi |
|-------|------------|----------------|
| `Development` | Geliştiricinin makinesi | Developer exception page: stack trace, header'lar, endpoint bilgisi |
| `Staging` | Production'ın **prova** kopyası (aynı ayarlar, ayrı kaynaklar) | Production gibi: sade yanıt |
| `Production` | Gerçek kullanıcılar | Sade yanıt (`ProblemDetails`), ayrıntılar yalnızca **log**'da |

> **Varsayılan Production'dır.** Hiçbir yerde ortam ayarlanmamışsa host `Production` olarak başlar (bu repoda `WebApp` için `launchSettings.json` yok; aşağıdaki ilk çıktı bunu doğruluyor). Güvenli taraf budur: unutulan ayar ayrıntılı hata sayfası açmaz.

---

## 2. Neden?

- **Güvenlik:** Development hata sayfası stack trace, kaynak dosya yolları, header'lar ve çoğu zaman exception mesajındaki *connection string / parola / iç sunucu adı* gibi bilgiyi gösterir. Bu, saldırgana kod yapınızın haritasını verir (information disclosure). Production'da bunların kullanıcıya gitmemesi gerekir (OWASP: *Improper Error Handling*).
- **Geliştirici verimliliği:** Geliştirirken tam tersine ayrıntı istersiniz: satır numarası, request header'ları, route değerleri. Aynı kod tabanı iki ihtiyacı da karşılar.
- **Aynı artefakt, farklı ortam:** Build'i bir kez alıp (`dotnet publish`) Dev/Staging/Prod'a **aynen** taşımak istersiniz; farkı kod değil ortam değişkenleri ve config belirler (12-factor).
- **Mülakat:** "Production'da exception detayını neden göstermiyoruz?", "`ASPNETCORE_ENVIRONMENT` ne işe yarar?" junior/mid backend mülakatlarının sık sorularıdır.

---

## 3. Nasıl?

### 3.1 Ortam nasıl belirlenir? ⭐

Üç kaynak vardır; `WebApplication.CreateBuilder(args)` ile **öncelik sırası** (yüksekten düşüğe):

| # | Kaynak | Örnek |
|---|--------|-------|
| 1 | Komut satırı | `dotnet run -- J6.6 --environment Staging` |
| 2 | `DOTNET_ENVIRONMENT` env var | `DOTNET_ENVIRONMENT=Staging` |
| 3 | `ASPNETCORE_ENVIRONMENT` env var | `ASPNETCORE_ENVIRONMENT=Staging` |
| 4 | Hiçbiri | `Production` |

> **Tuzak:** Dokümanda "ASPNETCORE_ENVIRONMENT, DOTNET_ENVIRONMENT'ı ezer" diye genel bir cümle de geçer; bu eski `WebHost` builder'ları için doğrudur. **`WebApplication.CreateBuilder` ile `DOTNET_ENVIRONMENT` kazanır** (dokümanın WebApplication notu; bu topic'te aşağıdaki deneyle doğrulandı). İkisi birden set edilmişse beklenmedik ortamla açılmanın klasik sebebi budur (örn. eski bir `DOTNET_ENVIRONMENT` kalıntısı).

Kendiniz görün: `/env` endpoint'i ortam adını, hangi kaynağın belirlediğini ve ham girdileri döner. Aynı komutu üç farklı şekilde çalıştırdım:

```bash
cd WebApp

# 1) Nothing is set → default
dotnet run -- J6.6
curl http://localhost:5080/env
# {"environmentName":"Production","decidedBy":"default - nothing is set, so the host falls back to Production",
#  "inputs":{"commandLine":null,"aspNetCoreEnvironment":null,"dotNetEnvironment":null}, ...}

# 2) Both env vars are set → DOTNET_ENVIRONMENT wins (WebApplication)
DOTNET_ENVIRONMENT=Staging ASPNETCORE_ENVIRONMENT=development dotnet run -- J6.6
# {"environmentName":"Staging","decidedBy":"DOTNET_ENVIRONMENT", ...,
#  "inputs":{"commandLine":null,"aspNetCoreEnvironment":"development","dotNetEnvironment":"Staging"}}

# 3) The command line beats both
ASPNETCORE_ENVIRONMENT=Development dotnet run -- J6.6 --environment=Production
# {"environmentName":"Production","decidedBy":"command line (--environment)", ...}
```

Notlar:
- Ortam adı **büyük/küçük harf duyarsız karşılaştırılır** (`IsDevelopment()` `OrdinalIgnoreCase` kullanır; `development` çalışır), ama `appsettings.Development.json` dosya adı Linux'ta **büyük/küçük harf duyarlıdır** (→ J6.3).
- `dotnet run` varsayılan olarak `Properties/launchSettings.json` profilini okur ve oradan `ASPNETCORE_ENVIRONMENT=Development` verir (Visual Studio F5 da aynı). `dotnet publish` çıktısında launchSettings **yoktur**; yayınlanan uygulama ortam değişkeni verilmedikçe `Production` çalışır. Bu repodaki `WebApp` bilerek launchSettings içermez: topic'leri `--environment` ile seçersiniz.
- Gerçek dünyada değeri nereden verirsiniz: Docker `-e ASPNETCORE_ENVIRONMENT=Staging`, Kubernetes `env:`, Azure App Service *Application settings*, systemd `Environment=`.

### 3.2 Koddan ortama bakmak

```csharp
var app = builder.Build();

if (app.Environment.IsDevelopment()) { /* Development-only */ }
if (app.Environment.IsStaging())     { /* Staging-only */ }
if (app.Environment.IsProduction())  { /* Production-only */ }
if (app.Environment.IsEnvironment("UAT")) { /* a custom name */ }

// The same object is injectable anywhere:
app.MapGet("/info", (IWebHostEnvironment env) => new
{
    env.EnvironmentName,   // "Production"
    env.ApplicationName,   // "WebApp"  (the entry assembly name)
    env.ContentRootPath,   // where appsettings*.json, views, ... are looked up (the project folder under `dotnet run`)
    env.WebRootPath,       // wwwroot, null if there is no such folder
});
```

- `IHostEnvironment`: `EnvironmentName`, `ApplicationName`, `ContentRootPath`, `ContentRootFileProvider`. `IWebHostEnvironment` bunun üstüne `WebRootPath`, `WebRootFileProvider` ekler.
- `IsDevelopment()` vb. aslında `IHostEnvironment` üzerindeki **extension method**'lardır (`HostEnvironmentEnvExtensions`). Ortam isimleri için `Environments.Development` sabitleri de vardır (`Microsoft.Extensions.Hosting.Environments`). Bu topic'in namespace'i de `...Environments` olduğu için kodda tam adı yazılır (ad çakışması).
- Razor tarafında `<environment include="Development">` tag helper'ı aynı işi view'da yapar.

### 3.3 Aynı hata, iki ortam: `/boom`

`/boom` bilerek patlar ve exception mesajına **sahte bir connection string** koyar (gerçek hayatta DB bağlantı hatalarının mesajı böyle şeyler içerir):

```csharp
app.MapGet("/boom", IResult () => throw new InvalidOperationException(
    "Cannot open database. ConnectionString: Server=db01.internal;Database=Orders;User Id=sa;Password=S3cr3t!"));
```

Hata işleme kodu [`ErrorPipeline.cs`](ErrorPipeline.cs) içinde **tek yerde** durur; hem ana host hem `/compare`'in iki ek host'u aynısını kullanır:

```csharp
services.AddProblemDetails();                    // RFC 9457 writer (→ J7.3)

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/error");           // Production / Staging / ...
// Development: nothing to write - WebApplication already added UseDeveloperExceptionPage() at the start of the pipeline.

app.UseStatusCodePages();                        // empty 4xx/5xx responses get a body (ProblemDetails JSON)

app.Map("/error", (HttpContext http) => Results.Problem(
    title: "An unexpected error occurred.",
    statusCode: 500,
    extensions: new Dictionary<string, object?> { ["traceId"] = Activity.Current?.Id ?? http.TraceIdentifier }));
```

**Development'ta developer exception page'i kendiniz eklemeniz gerekmez:** `WebApplication.CreateBuilder` ile kurulan uygulamada ortam `Development` ise `UseDeveloperExceptionPage()` pipeline'ın en başına otomatik eklenir (aşağıdaki stack trace'te `DeveloperExceptionPageMiddlewareImpl`'in en dışta olması bunu gösterir). Eski `Startup.Configure` modelinde elle yazılırdı (`if (env.IsDevelopment()) app.UseDeveloperExceptionPage();`).

#### Development çıktısı

```bash
dotnet run -- J6.6 --environment Development
curl -i http://localhost:5080/boom
```

```text
HTTP/1.1 500 Internal Server Error
Content-Type: application/problem+json

{"type":"https://tools.ietf.org/html/rfc9110#section-15.6.1","title":"System.InvalidOperationException","status":500,
 "detail":"Cannot open database. ConnectionString: Server=db01.internal;Database=Orders;User Id=sa;Password=S3cr3t!",
 "exception":{"details":"System.InvalidOperationException: Cannot open database. ConnectionString: Server=db01.internal;...Password=S3cr3t!
   at Junior.AspNetCoreFundamentals.Environments.ErrorPipeline.<>c.<MapEndpoints>b__3_0() in /.../06-environments/ErrorPipeline.cs:line 39
   at lambda_method1(Closure, Object, HttpContext)
   at Microsoft.AspNetCore.Routing.EndpointMiddleware.Invoke(HttpContext httpContext)
   at Microsoft.AspNetCore.Diagnostics.StatusCodePagesMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)",
 "headers":{"Accept":["*/*"],"Host":["localhost:5080"],"User-Agent":["curl/8.5.0"]},"path":"/boom","endpoint":"HTTP: GET /boom","routeValues":{},
 "traceId":"00-5941baa91040fc3ad543a77639dbe08b-0d953817bbc60173-00"}
```

(Çıktı okunabilirlik için satırlara bölündü ve dosya yolu kısaltıldı.) Parola, sunucu adı, dosya yolu, satır numarası, header'lar... hepsi istemcide. Dev sayfası **content negotiation** yapar: `Accept: text/html` isteyen tarayıcı renkli HTML sayfa alır (`Content-Type: text/html`); `AddProblemDetails()` kayıtlıysa JSON isteyen istemci yukarıdaki `application/problem+json`'ı alır.

#### Production çıktısı

```bash
dotnet run -- J6.6 --environment Production      # or just: dotnet run -- J6.6
curl -i http://localhost:5080/boom
```

```text
HTTP/1.1 500 Internal Server Error
Content-Type: application/problem+json
Cache-Control: no-cache,no-store

{"type":"https://tools.ietf.org/html/rfc9110#section-15.6.1","title":"An unexpected error occurred.","status":500,
 "traceId":"00-40c08dcc780f34eda5e0afe2828dbadd-a5e73dc0f788c99f-00"}
```

Mesaj yok, stack trace yok, yol yok; yalnızca genel bir başlık ve destek ekibine söylenebilecek bir **`traceId`**. Gerçek ayrıntı **sunucu logunda**:

```text
fail: Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware[1]
      An unhandled exception has occurred while executing the request.
      System.InvalidOperationException: Cannot open database. ConnectionString: Server=db01.internal;...
         at Junior.AspNetCoreFundamentals.Environments.ErrorPipeline.<>c.<MapEndpoints>b__3_0() in ...
         at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(...)
```

Dikkat: **log'un kendisi de hassas veri içerebilir** (burada parola exception mesajında). Log erişimini kısıtlayın ve exception mesajına sır koymayın (→ J6.5).

`UseExceptionHandler("/error")` nasıl çalışır: exception pipeline'dan yukarı kabarır, `ExceptionHandlerMiddleware` yakalar, **log'lar**, response'u sıfırlar, isteğin path'ini `/error`'a çevirip pipeline'ı **yeniden çalıştırır** (re-execute). Response'un gövdesi zaten yazılmaya başlamışsa (header gönderilmişse) durum kodu değiştirilemez; bu yüzden hata sayfası sadece henüz yazılmamış yanıtlarda işe yarar.

#### Yan yana: `/compare`

`/compare` iki kısa ömürlü host başlatır (`new WebApplicationOptions { EnvironmentName = "Development" }` / `"Production"`, rastgele port `http://127.0.0.1:0`, `:0` = işletim sistemi boş port seçer; aynı teknik J4'ün `Shared/LocalApi.cs` dosyasında var), ikisinde `/boom` çağırır, sonucu tek yanıtta verir ve host'ları dispose eder:

```bash
curl http://localhost:5080/compare
```

```json
{
  "Development": {
    "status": 500,
    "contentType": "application/problem+json",
    "leaksSecret": true,
    "leaksStackTrace": true,
    "bodyLength": 1137,
    "body": "{\"type\":\"https://tools.ietf.org/html/rfc9110#section-15.6.1\",\"title\":\"System.InvalidOperationException\",\"status\":500,\"detail\":\"Cannot open database. ConnectionString: Server=db01.internal;Database=Orders;User Id=sa;Password=S3cr3t!\",\"exception\":{\"details\":\"System.InvalidOperationException: Cannot open database. ConnectionString: Server=db01.internal;Database=Orders;User Id=sa;Password=S3cr3t!\\n   at Junior.AspNetCore... [trimmed, 1137 chars total]"
  },
  "Production": {
    "status": 500,
    "contentType": "application/problem+json",
    "leaksSecret": false,
    "leaksStackTrace": false,
    "bodyLength": 149,
    "body": "{\"type\":\"https://tools.ietf.org/html/rfc9110#section-15.6.1\",\"title\":\"An unexpected error occurred.\",\"status\":500,\"traceId\":\"0HNPHTLDAMGFO:00000001\"}"
  }
}
```

Aynı kod, aynı endpoint, aynı exception; fark yalnızca ortam adı: 1137 byte (sır + stack trace) → 149 byte (generic). Bu yanıt hangi ortamdan çağırdığınızdan bağımsızdır (ana host Dev ya da Prod olabilir). Ayrıca `traceId` formatına dikkat: ana host'ta W3C biçimi (`00-…-00`), ek host'ta `0HNPH…:00000001` (Kestrel connection/request id'si). Ek host'larda logging kapalı olduğundan o an bir `Activity` yoktur ve kod `http.TraceIdentifier`'a düşer; bu davranış tamamen normaldir.

### 3.4 `UseStatusCodePages` ve `ProblemDetails`'e giriş

Exception olmadan da hata durumu oluşur: bilinmeyen path → boş `404`. `UseStatusCodePages()` boş 4xx/5xx yanıtlara gövde ekler; `AddProblemDetails()` kayıtlıysa bu gövde **RFC 9457** biçimindedir:

```bash
curl -i http://localhost:5080/nothing
```

```text
HTTP/1.1 404 Not Found
Content-Type: application/problem+json

{"type":"https://tools.ietf.org/html/rfc9110#section-15.5.5","title":"Not Found","status":404,"traceId":"00-37b31ad9b3208685b3cf0227f10fd432-857a645e95373165-00"}
```

`ProblemDetails` alanları: `type` (hata türü URI'si), `title` (kısa, sabit başlık), `status`, `detail` (bu olaya özel açıklama; **Production'da iç bilgi koymayın**), `instance`, ve serbest `extensions` (`traceId`, `errors` ...). İçerik tipi `application/problem+json`. Özel `IExceptionHandler`, `CustomizeProblemDetails`, validation hataları → **J7.3**. Alternatif status-code sayfaları: `UseStatusCodePagesWithReExecute("/error/{0}")` (URL değişmeden başka bir endpoint'e düşer), `UseStatusCodePagesWithRedirects` (302; durum kodunu 200'e çevirdiği için API'de kullanmayın).

### 3.5 Ortam ve configuration (→ J6.3)

`WebApplication.CreateBuilder` configuration kaynaklarını şu sırayla ekler (sonraki öncekini ezer): `appsettings.json` → **`appsettings.{Environment}.json`** → (yalnızca Development'ta) User Secrets → environment variables → komut satırı. Yani ortam adı, hangi ek JSON dosyasının yükleneceğini de seçer; örn. `appsettings.Production.json` connection string'i ve log seviyelerini ezer. Bu topic'in demo'sunda `appsettings.*.json` yok (content root'ta yok; hata sayfası davranışı koddan geliyor).

### 3.6 Staging: Production'ın provası

`Staging`, Production ile **aynı ayarlar ve aynı yapı**, ama **ayrı kaynaklar** (ayrı DB, ayrı queue, ayrı domain) demektir. Amacı canlıya çıkmadan önce deployment'ı, migration'ları, config'i prod koşullarında denemektir. Bu yüzden Staging'in hata davranışı Production gibi olmalıdır: `IsDevelopment()` dışındaki her ortam `UseExceptionHandler("/error")` yoluna girer (kodumuz da böyle). Staging'de developer exception page açmak, "prova"nın anlamını ve güvenliği bozar. Blue/green ve slot (Azure App Service *deployment slot*) stratejileri de bu fikre dayanır.

### 3.7 Yapmayın

```csharp
// BAD: string compare scattered through the code (typos, case, no single place to change)
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production") { ... }
if (env.EnvironmentName == "production") { ... }

// BAD: environment-specific BUSINESS logic - the code you test in Development is not the code that runs in Production
decimal discount = env.IsDevelopment() ? 0m : CalculateDiscount(order);

// GOOD: behaviour differences are configuration, not code branches
public sealed class PaymentOptions { public string Provider { get; init; } = "Fake"; }   // appsettings.Production.json: "Real"
```

Kural: `IsDevelopment()` dallanması **altyapı/diagnostik** içindir (hata sayfası, Swagger UI açma, seed data, HSTS); iş kuralı için **configuration/options** (→ J6.3) ya da DI ile farklı implementasyon kaydı kullanın. Ortamlar arası fark azaldıkça "bende çalışıyordu" hataları azalır.

---

## 4. Gerçek dünya senaryosu

Bir e-ticaret API'si prod'a çıkıyor. Geliştirme sırasında sorunlar developer exception page ile hızlı çözülüyordu. Çıkıştan sonra güvenlik taramasında `GET /api/orders/abc` isteği 500 dönüyor ve gövde şunu içeriyor: tam stack trace, `C:\build\agent\_work\...\OrdersController.cs` yolu, EF Core hata mesajındaki DB sunucu adı ve kullanıcı adı. Sebep: konteynerde `ASPNETCORE_ENVIRONMENT=Development` unutulmuş (Dockerfile'ın ilk `docker run` denemesinden kalma).

Düzeltme: (1) Dağıtım şablonunda ortam değişkeni açıkça `Production`; (2) `UseExceptionHandler("/error")` + `AddProblemDetails()` ile istemciye yalnızca `title` ve `traceId`; (3) kullanıcı "traceId: 00-40c0…" ile destek isterse ekip log aracında (Application Insights/Seq/ELK) o id ile exception'ı bulur; (4) Staging'de aynı hata sayfası davranışı test edilir (deploy öncesi `curl /boom`-benzeri bir smoke test); (5) exception mesajlarına connection string/parola konmaz, log'lar rol bazlı erişime bağlanır; (6) başlangıçta ortam adı bir kez loglanır (`Hosting environment: Production` satırı zaten yazılır) ki yanlış ortam hemen fark edilsin.

---

## 5. Mülakat perspektifi

Beklenen cevaplar (uzun hali [`interview-questions.md`](interview-questions.md)):

- **"Environment nasıl belirlenir, varsayılanı nedir?"** → Komut satırı `--environment`, `DOTNET_ENVIRONMENT`, `ASPNETCORE_ENVIRONMENT` (WebApplication'da bu öncelik); hiçbiri yoksa `Production`.
- **"Development ile Production'da hata yönetimi neden farklı?"** → Dev'de ayrıntı geliştiriciye yardım eder, Prod'da bilgi sızıntısıdır; ayrıntı log'a, istemciye `ProblemDetails` + `traceId`.
- **"`UseDeveloperExceptionPage` nerede çağrılır?"** → `WebApplication` Development'ta otomatik ekler; pipeline'ın en başındadır (middleware sırası: → J6.4). `UseExceptionHandler` de en başa konur ki sonrasındaki her şeyi yakalasın.
- **"Staging nedir?"** → Prod-benzeri prova ortamı.
- **"Ortama göre kod dallanması iyi mi?"** → Diagnostik için evet, iş mantığı için hayır.
- Bonus: `IHostEnvironment.ContentRootPath` vs `WebRootPath`, `appsettings.{Env}.json` ilişkisi, `ProblemDetails` alanları.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| Production'da `ASPNETCORE_ENVIRONMENT=Development` bırakmak | Dağıtım şablonunda ortamı açıkça `Production`/`Staging` yapın; açılışta loglanan `Hosting environment` satırını kontrol edin |
| `UseDeveloperExceptionPage()`'i koşulsuz çağırmak | Yalnızca Development (WebApplication zaten otomatik ekler) |
| Hata yanıtına `ex.Message` / `ex.ToString()` koymak (`catch { return Problem(ex.Message); }`) | Generic `title` + `traceId`; ayrıntı log'a |
| `UseExceptionHandler`'ı pipeline'ın ortasına koymak | En başa: sonrasındaki middleware'lerin exception'ını yakalar (→ J6.4) |
| Her hatada elle `try/catch` + `return StatusCode(500)` | Merkezi exception handler middleware; sadece beklenen hatayı (iş kuralı) yerelde yakalayın |
| `if (env == "Production")` string kontrolleri | `IsProduction()` / `IsEnvironment(...)`; mümkünse hiç dallanmayın, config kullanın |
| İş mantığını ortama göre değiştirmek | Options/DI ile farklı konfigürasyon; kod aynı kalsın |
| `DOTNET_ENVIRONMENT` ile `ASPNETCORE_ENVIRONMENT`'ı birlikte, farklı değerlerle set etmek | Birini seçin; WebApplication'da `DOTNET_ENVIRONMENT` kazanır |
| Linux'ta `appsettings.development.json` adı | Dosya adı `appsettings.Development.json` (ortam adıyla aynı harf boyutu) |
| Hata gövdesi gönderildikten sonra exception sayfası beklemek | Response başladıysa status code değişmez; gövdeyi geç yazmayın/stream'e dikkat |
| Hata mesajına ve log'a parola/connection string yazmak | Hassas veriyi mesajdan ve log'dan çıkarın; gerekirse maskeleyin |

---

## 7. Performans & trade-off

- Ortam kontrolü (`IsDevelopment()`) bir string karşılaştırmasıdır; maliyeti yoktur ve genelde **başlangıçta bir kez** çalışır. Request başına tekrarlamayın.
- Exception **pahalıdır**: fırlatma + stack trace yakalama mikro/milisaniye mertebesindedir ve `ExceptionHandlerMiddleware` isteği yeniden çalıştırır. Beklenen durumlar (kayıt yok, validation) için exception yerine sonuç (`Results.NotFound()`, `Results.ValidationProblem`) döndürün (→ J7.3, J2).
- Development'ta `ValidateScopes`/`ValidateOnBuild` ve ayrıntılı loglama ek yük getirir; Production'ın hızını Development'ta ölçmeyin (benchmark'ı `Release` + `Production` ortamında yapın).
- Trade-off: Staging gerçek kaynaklarla maliyet demektir; ama eksik bir prova, prod'da arıza maliyetinden ucuzdur. Staging'i Production'dan ne kadar uzaklaştırırsanız provanın değeri o kadar düşer.
- Generic hata mesajı kullanıcıyı bilgilendirmez; `traceId` bu açığı kapatır: kullanıcı kodu söyler, siz ayrıntıyı log'dan bulursunuz.

---

## 8. Kaynaklar

- [ASP.NET Core runtime environments](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments)
- [Handle errors in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling)
- [Handle errors in ASP.NET Core APIs (ProblemDetails)](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling-api)
- [Configuration in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/)
- [RFC 9457 — Problem Details for HTTP APIs](https://www.rfc-editor.org/rfc/rfc9457)
- [OWASP — Improper Error Handling](https://owasp.org/www-community/Improper_Error_Handling)
- Bu klasör: [`EnvironmentsTopic.cs`](EnvironmentsTopic.cs) · [`ErrorPipeline.cs`](ErrorPipeline.cs) · [`requests.http`](requests.http)

---

## Alıştırmalar

1. `dotnet run -- J6.6` (ortam yok) çıktısında `Hosting environment` satırını bulun, ardından `--environment Staging` ile `/env` ve `/boom` yanıtlarını karşılaştırın. Staging hangi hata yoluna giriyor?
2. `DOTNET_ENVIRONMENT=Staging ASPNETCORE_ENVIRONMENT=Development` ile başlatın; `/env` hangisini gösteriyor? Neden?
3. `ErrorPipeline.UseMiddleware` içindeki `if (!app.Environment.IsDevelopment())` koşulunu kaldırıp `UseExceptionHandler("/error")`'ı her ortamda çalıştırın. Development'ta ne değişti?
4. `AddProblemDetails()` satırını silin; Development'ta `curl /boom` (varsayılan Accept) ve `curl /nothing` çıktısı nasıl değişiyor? Production'da `/boom` değişiyor mu?
5. `/error` endpoint'ine `IExceptionHandlerFeature` ile exception **tipini** log'layın (mesajı değil). Neden mesajı log'lamamak daha güvenli olabilir?

<details>
<summary>Çözümler</summary>

1. `Production`/`Staging` ikisi de `UseExceptionHandler("/error")` yoluna girer; `/boom` generic `ProblemDetails` döner. Fark yalnızca `environmentName` ve (varsa) `appsettings.Staging.json` olur.
2. `Staging`: WebApplication'da `DOTNET_ENVIRONMENT` kazanır.
3. Developer exception page yine en dıştadır ama `UseExceptionHandler` onun **içinde** kalır ve exception'ı developer sayfası görmeden yakalar; Development'ta da generic `ProblemDetails` (`An unexpected error occurred.` + `traceId`) döner (denendi). Yani Development'ta ayrıntı görmeniz için handler'ın orada olmaması gerekir.
4. `/nothing` (Production da Development da) `Content-Type: text/plain` ile düz `Status Code: 404; Not Found` döner. Development'ta `/boom` artık `application/problem+json` yerine `text/plain` stack trace verir (denendi). `/error` içindeki `Results.Problem` ise servis olmadan da ProblemDetails yazdığı için Production'daki `/boom` yanıtı değişmez.
5. Mesaj kullanıcı girdisi/sır içerebilir; tip + traceId çoğu zaman teşhis için yeterlidir ve loglama maliyeti ile sızıntı riski düşer.

</details>
