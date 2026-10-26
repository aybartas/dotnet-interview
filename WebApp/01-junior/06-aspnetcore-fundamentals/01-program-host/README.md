# J6.1 `Program.cs` & Host

> 📁 `WebApp/01-junior/06-aspnetcore-fundamentals/01-program-host/` · Önkoşul: J2–J4 · Sorular: [`interview-questions.md`](interview-questions.md)

## İçindekiler

1. [Ne?](#1-ne)
2. [Neden?](#2-neden)
3. [Nasıl?](#3-nasıl)
4. [Gerçek dünya senaryosu](#4-gerçek-dünya-senaryosu)
5. [Mülakat perspektifi](#5-mülakat-perspektifi)
6. [Yaygın hatalar](#6-yaygın-hatalar)
7. [Performans & trade-off](#7-performans--trade-off)
8. [Kaynaklar](#8-kaynaklar)

**Kod:** [`ProgramHostTopic.cs`](ProgramHostTopic.cs) (her satırı yorumlanmış `Program.cs`)

---

## 1. Ne?

Bir ASP.NET Core uygulaması, aslında **bir console uygulamasıdır**: `Main` (top-level statements ile `Program.cs`) bir **host** oluşturur, host içinde bir **web sunucusu (Kestrel)** çalışır ve gelen HTTP isteklerini sizin yazdığınız **middleware pipeline**'ına verir. `Program.cs` bu üç şeyin birleştiği yerdir.

**Host** = uygulamanın "yaşam döngüsünü" yöneten nesne. Şunları bir arada tutar:

| Host'un sahip olduğu | Karşılığı |
|----------------------|-----------|
| Dependency Injection container'ı | `builder.Services` → `app.Services` (→ J6.2) |
| Configuration | `builder.Configuration` (→ J6.3) |
| Logging | `builder.Logging` (→ J6.5) |
| Web sunucusu | Kestrel (`IServer`) |
| Yaşam döngüsü | başlat / `Ctrl+C` ile düzgün kapat (graceful shutdown), `IHostedService`'ler |
| Ortam bilgisi | `builder.Environment` / `app.Environment` (→ J6.6) |

`Program.cs`'in iskeleti her ASP.NET Core projesinde aynıdır ve **iki faz**dan oluşur:

```
WebApplication.CreateBuilder(args)      ┐
  builder.Services / Configuration /    │  1) BUILDER FAZI — "uygulamanın neye ihtiyacı var?"
  Logging / WebHost                     ┘     (servis kayıtları, ayarlar, log, URL)
builder.Build()                         ← DI container burada dondurulur (artık servis eklenemez)
  app.Use… / app.Map…                   ┐  2) PIPELINE FAZI — "istek hangi sırayla ne yapsın?"
app.Run()                               ┘     (middleware sırası + endpoint'ler) ve başlat
```

> **Not:** [`ProgramHostTopic.Run`](ProgramHostTopic.cs) bu iskeletin birebir kendisidir; bu repoda `WebApp/Program.cs` yalnızca konu seçen bir menüdür, her konu kendi host'unu kurar.

---

## 2. Neden?

- **Her ASP.NET Core mülakatı buradan başlar:** "`Program.cs`'te ne var?", "`builder.Build()`'den önce/sonra ne yapılır?", "Request geldiğinde ne olur?" ⭐ sorularının cevabı bu konudadır.
- **Hata ayıklamanın başı:** "Neden 5000 portunda açılmıyor?", "Neden Development'ta çalışıp Production'da patlıyor?", "HTTPS sertifika uyarısı neden çıkıyor?" soruları host, `launchSettings.json` ve dev certificate bilgisine dayanır.
- **Sıra = davranış:** Pipeline'da `app.Use…` satırlarının sırası, isteğin hangi aşamadan geçeceğini belirler. Sırayı bilmeden middleware (J6.4), authentication, CORS ya da hata sayfaları (J6.6) doğru kurulamaz.
- **Sonraki her şeyin zemini:** DI (J6.2), configuration (J6.3), logging (J6.5) ve Web API (J7) hep bu `builder` / `app` nesneleri üzerinden yapılandırılır.

---

## 3. Nasıl?

### 3.1 Minimal hosting modeli: `WebApplication.CreateBuilder(args)`

```csharp
var builder = WebApplication.CreateBuilder(args);   // 1) builder fazı
// builder.Services.Add…  builder.Configuration…  builder.Logging…
var app = builder.Build();                           // 2) build
// app.Use…  app.Map…                                // 3) pipeline
app.Run();                                           // 4) çalıştır
```

`CreateBuilder` tek satırda **makul varsayılanları** kurar (Microsoft docs'taki "default builder settings" listesi):

| Alan | Varsayılan olarak ne kurulur |
|------|------------------------------|
| **Web sunucusu** | **Kestrel** (cross-platform, süreç içi); IIS arkasında çalışılıyorsa IIS entegrasyonu |
| **Content root** | Çalışma dizini (`dotnet run`'da proje klasörü); `wwwroot` = statik dosya kökü |
| **Configuration** (sonra gelen kazanır) | `appsettings.json` → `appsettings.{Environment}.json` → User Secrets (yalnızca Development) → environment variable'lar → komut satırı argümanları (`args`) |
| **Host ayarları** | `DOTNET_` ve `ASPNETCORE_` önekli environment variable'lar (ör. `ASPNETCORE_ENVIRONMENT`, `ASPNETCORE_URLS`) |
| **Logging** | Console, Debug, EventSource (ve Windows'ta EventLog) provider'ları; ayar `Logging` bölümünden |
| **DI** | Hazır bir `IServiceProvider`; Development ortamında scope ve bağımlılık doğrulaması açıktır (→ J6.2) |
| **Routing / endpoint** | `UseRouting` ve `UseEndpoints` pipeline'a **otomatik** eklenir (aşağıya bakın) |

Bu yüzden boş bir proje bile `appsettings.json` okur, console'a log yazar ve `app.MapGet(...)` yazar yazmaz Kestrel'de yayın yapar.

**Neden `args` veriyoruz?** Komut satırı da bir configuration kaynağıdır: `dotnet run -- J6.1 --environment Development` içindeki `--environment` host'a böyle ulaşır.

### 3.2 `Program.cs`'in her satırı — kodun kendisi

Konunun asıl çıktısı [`ProgramHostTopic.cs`](ProgramHostTopic.cs) dosyasıdır: boş bir web projesinin `Program.cs`'i, **her satırı yorumlu**. Özet:

```csharp
var builder = WebApplication.CreateBuilder(args);            // defaults: config, logging, DI, Kestrel
builder.WebHost.UseUrls("http://localhost:5080");             // which address Kestrel listens on
builder.Configuration["Demo:Title"] = "J6.1 Program.cs & Host"; // IConfiguration: in-memory value
builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning); // logging setup
builder.Services.AddSingleton(TimeProvider.System);          // DI registration

var app = builder.Build();                                   // container frozen; app = host + pipeline builder

app.Use(async (context, next) => { …; await next(context); … }); // middleware 1 (before routing)
app.UseRouting();                                            // endpoint selection
app.Use(async (context, next) => { …; await next(context); … }); // middleware 2 (after routing)
app.MapGet("/", () => "…");                                  // endpoint
app.MapGet("/pipeline", (HttpContext c, TimeProvider t, IConfiguration cfg) => …); // endpoint with injected parameters

app.Run();                                                   // Start() + wait for Ctrl+C
```

**Çalıştırma:**

```bash
cd WebApp
dotnet run -- J6.1            # → http://localhost:5080 ; stop with Ctrl+C
```

Başlangıç log'u (gerçek çıktı):

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5080
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /…/WebApp
```

`Hosting environment: Production` dikkat çekicidir: bu projede `launchSettings.json` **yok**, dolayısıyla `ASPNETCORE_ENVIRONMENT` set edilmedikçe ortam Production'dır (→ 3.5 ve J6.6). Ortamı değiştirmek için: `dotnet run -- J6.1 --environment Development`.

### 3.3 "Request geldiğinde ne oluyor?" ⭐

Konunun en sık sorulan sorusu. Yolculuk:

```
 client                                                                 server process (dotnet WebApp.dll)
   │  1) TCP connect (+ TLS handshake, HTTPS ise)                          ┌───────────────────────────────┐
   ├──────────────────────────────────────────────────────────────────────▶│ 2) KESTREL                    │
   │  GET /pipeline HTTP/1.1                                                │    bayt akışını HTTP olarak   │
   │                                                                        │    ayrıştırır, HttpContext    │
   │                                                                        │    (Request/Response) üretir  │
   │                                                                        ├───────────────────────────────┤
   │                                                                        │ 3) MIDDLEWARE PIPELINE        │
   │                                                                        │    mw 1 → UseRouting → mw 2   │
   │                                                                        │    (her biri next() çağırır)  │
   │                                                                        ├───────────────────────────────┤
   │                                                                        │ 4) ROUTING: path+method →     │
   │                                                                        │    endpoint seçilir           │
   │                                                                        ├───────────────────────────────┤
   │                                                                        │ 5) ENDPOINT (handler) çalışır │
   │◀───────────────────────────────────────────────────────────────────────┤ 6) response, middleware'lerden│
   │  200 OK + body                                                         │    TERS sırayla geri döner    │
   │                                                                        └───────────────────────────────┘
```

1. **TCP (+TLS):** İstemci porta bağlanır; HTTPS ise TLS el sıkışması Kestrel içinde tamamlanır (→ J1.3).
2. **Kestrel:** Soketten gelen baytları HTTP/1.1, HTTP/2 ya da HTTP/3 olarak ayrıştırır; `HttpContext` (request + response + connection bilgisi) üretir ve pipeline'ın ilk middleware'ine verir.
3. **Middleware pipeline:** İç içe halkalar gibi (→ J6.4): her middleware `await next(context)` ile sonrakine devreder; `next()`'ten **sonra** gelen kod response yönünde, **ters sırada** çalışır.
4. **Routing:** `UseRouting` istek yolunu + HTTP metodunu endpoint'lerle eşleştirir ve seçileni `context.SetEndpoint` ile saklar. `UseRouting`'ten **önceki** middleware'ler endpoint'i henüz göremez (`GetEndpoint()` = `null`); sonrakiler görür (authorization/CORS bu yüzden routing'den sonra gelir).
5. **Endpoint:** `MapGet` ile verdiğiniz delegate çalışır; parametreleri DI'dan ve request'ten çözülür.
6. **Response:** Endpoint'in sonucu yazılır, middleware'lerden geri döner, Kestrel bayt olarak soketten gönderir.

Bunu **gözle görmek** için `/pipeline` endpoint'ine istek atın. Her aşama `HttpContext.Items` içindeki bir listeye sırayla eklenir ve endpoint listeyi döner:

```bash
curl -i http://localhost:5080/pipeline
```

```
HTTP/1.1 200 OK
Content-Length: 525
Content-Type: text/plain; charset=utf-8
Server: Kestrel

1. Kestrel           | server = KestrelServerImpl, HTTP/1.1 GET, remote = 127.0.0.1:55608, local = 127.0.0.1:5080, connection id = 0HNPHTFA01BP6
2. middleware 1 (in) | before routing, endpoint = <none yet>
3. routing           | selected endpoint = HTTP: GET /pipeline
4. middleware 2 (in) | after routing, before the endpoint runs
5. endpoint          | J6.1 Program.cs & Host handler runs, utc = 16:28:25
6. response          | the endpoint returns, the trace is written as the body (outbound stages are in the server log)
```

Okuma rehberi:

- **1:** Sunucu `KestrelServerImpl`; istemci `127.0.0.1:55608` (geçici/ephemeral port), sunucu `5080`; protokol `HTTP/1.1`.
- **2 → 3:** `UseRouting` çalışmadan endpoint `<none yet>`; routing'den sonra `HTTP: GET /pipeline` seçilmiş.
- **Dönüş yönü:** Response gövdesi zaten yazıldığı için "çıkış" aşamaları body'ye girmez; sunucu console'unda **ters sırada** görünür (önce middleware 2, sonra 1):

```
info: WebApp[0]
      pipeline out: middleware 2 (out), status 200
info: WebApp[0]
      pipeline out: middleware 1 (out), status 200
```

Bonus deneyler:

```bash
curl -s --http1.0 http://localhost:5080/pipeline | head -1   # Kestrel satırında HTTP/1.0 GET görünür
curl -s -o /dev/null -w "%{http_code}\n" http://localhost:5080/nope                   # 404 — routing endpoint bulamadı; mw 1 ve mw 2 yine de çalıştı
curl -s -o /dev/null -w "%{http_code}\n" -X POST http://localhost:5080/pipeline       # 405 — path eşleşti, metod eşleşmedi
```

> `app.UseRouting()`'i bu demoda **elle** çağırdık. `WebApplication` çağırmazsanız onu pipeline'ın **en başına** (ve `UseEndpoints`'i en sonuna) kendisi ekler; bu durumda tüm `app.Use…` middleware'leri routing'den sonra çalışır. Sırayı kontrol etmek istediğinizde `UseRouting`'i kendiniz yazarsınız.

### 3.4 Kestrel — web sunucusu

- **Kestrel**, ASP.NET Core'un varsayılan, cross-platform, yönetilen (managed) kodla yazılmış HTTP sunucusudur; uygulamayla **aynı süreçte** çalışır (ayrı Apache/IIS kurulumu gerekmez, `dotnet WebApp.dll` yeter).
- HTTP/1.1, HTTP/2 ve HTTP/3 (QUIC, TLS gerekir) destekler. Düz HTTP (TLS'siz) endpoint'te varsayılan yalnızca HTTP/1.1'dir: `curl --http2-prior-knowledge http://localhost:5080/…` bu demoda cevap vermez; HTTP/2 pratikte HTTPS + ALPN ile pazarlık edilir.
- **Edge sunucu mu, reverse proxy arkası mı?** Kestrel tek başına internete açılabilir, ama genelde önünde bir reverse proxy (nginx, YARP, Azure Front Door / Application Gateway, IIS) durur: TLS sonlandırma, yük dengeleme, sıkıştırma, istek limitleri. Proxy arkasında gerçek istemci IP'si ve şema için `X-Forwarded-*` header'ları ve `UseForwardedHeaders` gerekir (→ M3).
- IIS'te iki model vardır: **in-process** (uygulama `w3wp.exe` içinde, IIS HTTP Server kullanılır) ve **out-of-process** (IIS → Kestrel'e proxy). Detay → M3.
- Kestrel'in iç ayarları (limits, `Listen*`, sertifika, protokoller) `builder.WebHost.ConfigureKestrel(…)` veya `Kestrel:Endpoints` configuration bölümüyle yapılır; junior için `UseUrls` / `ASPNETCORE_URLS` yeterli.

### 3.5 `launchSettings.json`

`Properties/launchSettings.json`, **yalnızca geliştirme makinesinde** `dotnet run` ve Visual Studio/Rider'ın F5'i tarafından okunan "nasıl başlatılsın" dosyasıdır. Environment variable'ları, URL'leri ve tarayıcının açılıp açılmayacağını profil olarak tanımlar. **`dotnet publish` çıktısına girmez** ve production'da hiçbir etkisi yoktur.

Tipik bir ASP.NET Core şablonu:

```json
{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:5001;http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

(Şablonlar genelde rastgele bir port seçer; burada 5000/5001 örnek olarak verildi.)

| Konu | Ne olur |
|------|---------|
| Hangi profil? | `dotnet run` varsayılan olarak dosyadaki **ilk** `"commandName": "Project"` profilini kullanır; `--launch-profile https` ile seçilir, `--no-launch-profile` ile tamamen yok sayılır |
| `applicationUrl` | `ASPNETCORE_URLS` olarak process'e geçirilir; `;` ile birden çok adres |
| `environmentVariables` | Process'e verilir; `ASPNETCORE_ENVIRONMENT=Development` şablonlarda buradan gelir |
| Production'da? | Dosya yayınlanmaz. Production ortamını **sunucu/container/App Service ayarı** belirler |

**Bu repodaki `WebApp` projesinde `launchSettings.json` yoktur** (repo kuralı: konu menüsü + sabit `http://localhost:5080`). Sonuçları:

- `dotnet run` ortam variable'ı vermediği için ortam **Production**'dır (log'da `Hosting environment: Production`).
- Tarayıcı kendiliğinden açılmaz.
- Port'u kod belirler (`UseUrls("http://localhost:5080")`).
- Development davranışını görmek istiyorsanız komut satırından verin: `dotnet run -- J6.1 --environment Development`.

### 3.6 Port'lar, `UseUrls` ve `ASPNETCORE_URLS`

Kestrel hangi adreste dinleyeceğini şu kaynaklardan öğrenir:

| Yöntem | Örnek |
|--------|-------|
| Kodda | `builder.WebHost.UseUrls("http://localhost:5080")` |
| Environment variable | `ASPNETCORE_URLS=http://localhost:5080;https://localhost:5443` |
| Komut satırı | `dotnet run --urls "http://localhost:5080"` |
| `launchSettings.json` | `applicationUrl` (yalnızca dev, yukarıya bakın) |
| Kestrel config | `Kestrel:Endpoints:Http:Url` (`appsettings.json`) veya `ConfigureKestrel` + `Listen*` |

- Hiçbir şey verilmezse Kestrel `http://localhost:5000`'de dinler (HTTPS için varsayılan yoktur; şablonlardaki 5001 `launchSettings`'ten gelir).
- `localhost` yalnızca **loopback**'e bağlanır (dışarıdan erişilemez); container/sunucuda tüm arayüzler için `http://0.0.0.0:8080` ya da `http://*:8080` yazılır. .NET 8+ container imajlarında varsayılan port **8080**'dir (`ASPNETCORE_HTTP_PORTS`).
- Çakışma halinde (`Address already in use`) uygulama başlamaz: `Failed to bind to address http://127.0.0.1:5080: address already in use`. Çözüm: eski süreci kapatmak ya da başka port.
- **Öncelik uyarısı (bu demoda gözlenen):** Koddaki `UseUrls(...)`, `--urls` argümanını ve `ASPNETCORE_URLS`'i **ezdi**: `dotnet run -- J6.1 --urls http://localhost:5090` yine `http://localhost:5080`'de dinledi. Gerçek projede port'un dışarıdan değişebilmesini istiyorsanız kodda `UseUrls` yazmayın. (Ayrıca `Listen*` / `Kestrel:Endpoints` her ikisinden de baskındır.)

### 3.7 HTTPS geliştirme sertifikası (`dotnet dev-certs https`)

HTTPS için sunucuda bir sertifika gerekir. Geliştirmede SDK, `localhost` için **kendinden imzalı bir geliştirme sertifikası** üretir; tarayıcılar güvenilir saymadıkça uyarı verir.

```bash
dotnet dev-certs https --check            # sertifika var mı, geçerli mi?
dotnet dev-certs https --check --trust    # ... ve güvenilir (trusted) mi?
dotnet dev-certs https --trust            # oluştur (yoksa) ve güvenilir kök deposuna ekle  (Windows/macOS)
dotnet dev-certs https --clean            # tüm geliştirme sertifikalarını sil
dotnet dev-certs https -ep ./dev.pfx -p <pwd>   # dışa aktar (örn. container içinde kullanmak için)
```

Gerçek `--check` çıktısı (bu makinede):

```
A valid certificate was found: B00F5E10AE57902AB737407CE8B0D6521D420147 - CN=localhost - Valid from 2026-10-20 18:53:33Z to 2027-10-20 18:53:33Z - IsHttpsDevelopmentCertificate: true - IsExportable: true
Run the command with both --check and --trust options to ensure that the certificate is not only valid but also trusted.
```

- `--trust` Windows ve macOS'ta sertifikayı kullanıcının güvenilir deposuna ekler; **Linux'ta tek bir ortak trust store yoktur**, dağıtıma/tarayıcıya göre manuel güvenme gerekir (docs bunu ayrıca anlatır).
- Sertifika **yalnızca geliştirme içindir**; sunucuya taşımayın. Production'da gerçek bir sertifika kullanılır (Let's Encrypt, Key Vault, App Service managed certificate, reverse proxy'de TLS sonlandırma).
- HTTPS'li bir konu denemek için J1.3: `dotnet run -- J1.3 --https` (→ J1.3, `https://localhost:5443`).
- "Sertifika güvenilir değil" hatası → `--clean`, ardından `--trust`; tarayıcıyı yeniden başlatın.

### 3.8 `app.Run()` ve `app.Start()`

| | `app.Run()` | `app.Start()` |
|---|-------------|---------------|
| Davranış | Host'u başlatır ve **kapanana kadar bloklar** (Ctrl+C / SIGTERM / `app.Lifetime.StopApplication()`) | Host'u başlatır ve **hemen döner**; uygulama arka planda çalışır |
| Eşdeğer | `Start()` + `WaitForShutdown()` | — |
| Async | `RunAsync()` | `StartAsync()` (`await app.StopAsync()` ile durdurulur) |
| Kullanım | Normal `Program.cs`'in son satırı | Başlangıç sonrası ek iş yapmak, testlerde host'u kontrollü açıp kapatmak |

```csharp
app.Start();                           // returns immediately, Kestrel is already listening
Console.WriteLine("started");          // runs; the process would exit here if nothing blocks
app.WaitForShutdown();                 // block until Ctrl+C — this is what Run() does for you
```

`Start()`'tan sonra hiçbir şey bloklamazsa `Main` biter ve süreç kapanır; bu yaygın bir yanlış anlamadır.

### 3.9 Kısa tarihçe: `Startup.cs` → minimal hosting

| Dönem | Yapı |
|-------|------|
| .NET Core 1.x–2.x | `Program.Main` → `WebHost.CreateDefaultBuilder().UseStartup<Startup>()` |
| .NET Core 3.x – .NET 5 | Generic Host: `Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(w => w.UseStartup<Startup>())`; `Startup.ConfigureServices` (DI) + `Startup.Configure` (pipeline) |
| **.NET 6+ (2021)** | **Minimal hosting:** tek dosya, top-level statements, `WebApplication.CreateBuilder`; `ConfigureServices` → `builder.Services`, `Configure` → `app.Use…` |

`Startup.cs` hâlâ desteklenir; eski kod tabanlarında karşınıza çıkar. Eşleşme: `ConfigureServices(IServiceCollection services)` = builder fazı, `Configure(IApplicationBuilder app)` = pipeline fazı. Mantık değişmedi, yalnızca iki sınıf tek dosyaya indi.

---

## 4. Gerçek dünya senaryosu

**"Lokalde çalışıyor, sunucuda 5000 portunda cevap yok / Development'ta çalışıp Production'da patlıyor."**

1. Geliştirici makinesinde F5 → `launchSettings.json` devrede: `ASPNETCORE_ENVIRONMENT=Development`, `applicationUrl=https://localhost:5001`, developer exception page açık, User Secrets okunuyor.
2. Aynı uygulama container'da `dotnet WebApp.dll` ile başlıyor: `launchSettings.json` yok → ortam Production, URL ayarı yok → `http://localhost:5000` (container içinde **loopback**, dışarıdan erişilemez), User Secrets yok → bağlantı dizesi boş.
3. Teşhis: başlangıç log'u (`Now listening on:`, `Hosting environment:`, `Content root path:`) ilk bakılacak yerdir.
4. Çözüm: `ASPNETCORE_URLS=http://0.0.0.0:8080` (ya da `ASPNETCORE_HTTP_PORTS=8080`), `ASPNETCORE_ENVIRONMENT=Production` açıkça, secret'lar environment variable / Key Vault ile; TLS reverse proxy'de sonlanıyorsa uygulama içi HTTPS yönlendirmesi gözden geçirilir.

Aynı mantık mülakatta "bir istek bu host'tan nasıl geçer?" sorusuna cevap verirken de kullanılır: önce host kurulumu (`Program.cs`), sonra çalışma anı (Kestrel → pipeline → endpoint).

---

## 5. Mülakat perspektifi

Sık gelen sorular (cevaplar → [`interview-questions.md`](interview-questions.md)):

- **Request geldiğinde ASP.NET Core'da ne olur?** ⭐ TCP → Kestrel → middleware pipeline → routing → endpoint → response
- `Program.cs`'te sırayla neler yapılır? `builder` ile `app` arasındaki fark nedir?
- `WebApplication.CreateBuilder` varsayılan olarak neleri kurar?
- Kestrel nedir? IIS/nginx ile ilişkisi? Neden reverse proxy arkasında çalıştırılır?
- `Run()` ile `Start()` farkı? `Startup.cs` ne oldu?
- `launchSettings.json` nedir, production'a gider mi?
- Port nasıl ayarlanır? `ASPNETCORE_URLS` ile `UseUrls` ilişkisi?
- HTTPS dev certificate nedir? `dotnet dev-certs https --trust` ne yapar?

**İpucu:** "Request geldiğinde ne olur?" için 6 adımlık cümleyi ezberleyin, sonra *"ben bunu bir `/pipeline` endpoint'iyle gözlemledim"* deyip `GetEndpoint()`'in routing'den önce `null`, sonra dolu olduğunu anlatın — junior'da nadir görülen bir somutluk.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| `builder.Build()`'den sonra `builder.Services.Add…` çağırmak | `InvalidOperationException` (service collection read-only). Tüm kayıtlar `Build()`'den önce |
| `app.Use…` sırasını rastgele yazmak | Sıra = çalışma sırası. Exception handler en başta; `UseRouting` → `UseAuthentication` → `UseAuthorization` → endpoint (→ J6.4) |
| `launchSettings.json`'un production'da da geçerli olduğunu sanmak | Yalnızca dev'de `dotnet run`/IDE okur; yayına girmez. Ortamı sunucu ayarlar |
| `localhost` adresine bağlanıp container dışından erişmeyi beklemek | Container'da `0.0.0.0` / `ASPNETCORE_HTTP_PORTS` kullanın |
| `UseUrls`'i koda gömüp `ASPNETCORE_URLS` ile değiştirmeye çalışmak | Kodda `UseUrls` env var'ı ezer (bu demoda gözlendi); port'u dışarıdan ver |
| `app.Start()` kullanıp sürecin kapanmasına şaşırmak | `Start()` bloklamaz; `WaitForShutdown()` veya `Run()` |
| Dev sertifikasını production'a taşımak | Yalnızca geliştirme içindir; gerçek sertifika kullanın |
| Sertifika uyarısını `ServerCertificateCustomValidationCallback = true` ile susturmak | `dotnet dev-certs https --trust` ile çözün (→ J1.3) |
| Eski port'u tutan süreç yüzünden `address already in use` hatasını kod hatası sanmak | Süreci bulun/kapatın ya da port değiştirin |
| Endpoint'te `GetEndpoint()`'i `UseRouting`'den önceki middleware'de kullanmak | Routing çalışmadan `null` döner; endpoint bilgisi isteyen middleware routing'den sonra olmalı |

---

## 7. Performans & trade-off

- **Kestrel hızlıdır:** Bağımsız benchmark'larda (TechEmpower) üst sıralarda yer alır; bu yüzden "Kestrel yavaş, IIS/nginx hızlandırır" yanlışı yaygındır. Reverse proxy hız için değil, **TLS sonlandırma, yük dengeleme, güvenlik katmanı** için kullanılır.
- **IIS in-process vs out-of-process:** In-process, ekstra ağ atlaması olmadığı için daha hızlıdır (IIS'te varsayılan); out-of-process IIS → Kestrel arasında bir proxy atlaması ekler.
- **Middleware sayısı = her istekte ödenen maliyet:** Pipeline her istekte baştan sona çalışır; gereksiz middleware'i çıkarın, ucuz olanları (statik dosya, health check) erken `short-circuit` edin (→ J6.4).
- **Başlangıç süresi:** Çok sayıda servis kaydı, assembly taraması ve `Build()` içi doğrulama (Development'ta `ValidateOnBuild`) açılışı yavaşlatır; container/serverless'ta soğuk başlangıç (cold start) önemlidir (→ S-seviyesi: ReadyToRun, trimming, AOT).
- **Graceful shutdown:** `Run()` SIGTERM'i yakalar ve işlenmekte olan istekleri bitirmek için `HostOptions.ShutdownTimeout` süresi (varsayılan 30 sn) bekler; Kubernetes'te `terminationGracePeriodSeconds` ile uyumlu olmalıdır (→ M-seviyesi).
- **`HttpContext.Items`** bu demoda aşamaları taşımak için kullanıldı: request ömürlü, ucuz bir sözlük; thread-safe değildir ve request dışında saklanmamalıdır.

---

## 8. Kaynaklar

- ASP.NET Core — Host ve `WebApplication` temelleri (`CreateBuilder` varsayılanları): <https://learn.microsoft.com/aspnet/core/fundamentals/host/web-host> · <https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis/webapplication>
- ASP.NET Core — Uygulama başlangıcı (`Program.cs`): <https://learn.microsoft.com/aspnet/core/fundamentals/startup>
- ASP.NET Core — Kestrel: <https://learn.microsoft.com/aspnet/core/fundamentals/servers/kestrel>
- ASP.NET Core — Kestrel endpoint yapılandırması ve URL öncelikleri: <https://learn.microsoft.com/aspnet/core/fundamentals/servers/kestrel/endpoints>
- ASP.NET Core — Birden çok ortam ve `launchSettings.json`: <https://learn.microsoft.com/aspnet/core/fundamentals/environments>
- ASP.NET Core — HTTPS zorunlu kılma ve geliştirme sertifikası: <https://learn.microsoft.com/aspnet/core/security/enforcing-ssl> · `dotnet dev-certs`: <https://learn.microsoft.com/dotnet/core/tools/dotnet-dev-certs>
- ASP.NET Core — Routing: <https://learn.microsoft.com/aspnet/core/fundamentals/routing>
- Minimal hosting geçiş rehberi (Startup → `WebApplication`): <https://learn.microsoft.com/aspnet/core/migration/50-to-60>
- Pro ASP.NET Core (Adam Freeman) — ilk bölümler
- Bu repoda: [J1.3 Web nasıl çalışır](../../01-dev-foundations/03-how-the-web-works/README.md) · sonraki konular: J6.2 DI, J6.4 Middleware (derin hali → M3)
