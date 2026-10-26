# J6.5 Logging Temelleri

> 📁 `WebApp/01-junior/06-aspnetcore-fundamentals/05-logging-basics/` · Önkoşul: J2–J4 · Sorular: [`interview-questions.md`](interview-questions.md)

## İçindekiler

1. [Ne?](#1-ne)
2. [Neden?](#2-neden)
3. [Nasıl?](#3-nasıl)
4. [Gerçek dünya senaryosu](#4-gerçek-dünya-senaryosu)
5. [Mülakat perspektifi](#5-mülakat-perspektifi)
6. [Yaygın hatalar](#6-yaygın-hatalar)
7. [Performans & trade-off](#7-performans--trade-off)
8. [Kaynaklar](#8-kaynaklar)

**Bu klasördeki kod:** [`LoggingBasicsTopic.cs`](LoggingBasicsTopic.cs) (host, endpoint'ler) · [`OrderService.cs`](OrderService.cs) (her seviyede log) · [`StructuredLoggingDemo.cs`](StructuredLoggingDemo.cs) (template vs interpolation, scope) · [`ShippingService.cs`](ShippingService.cs) (`[LoggerMessage]`) · [`CapturingLoggerProvider.cs`](CapturingLoggerProvider.cs) (provider'ın gerçekte ne aldığını gösteren in-memory provider) · [`AccountService.cs`](AccountService.cs) + [`LogRedactor.cs`](LogRedactor.cs) (hassas veri) · [`appsettings.json`](appsettings.json) / [`.Development`](appsettings.Development.json) / [`.Production`](appsettings.Production.json)

---

## 1. Ne?

**Logging**, uygulamanın çalışırken "ne yaptığını" kalıcı bir kayda yazmasıdır. Production'da debugger yoktur; bir şey ters gittiğinde elinizdeki tek ipucu loglardır.

ASP.NET Core'da logging, `Microsoft.Extensions.Logging` (MEL) üzerindedir ve üç parçadan oluşur:

```
Kodunuz ──► ILogger<T> ──► ILoggerFactory ──► Filtre kuralları ──► Provider'lar
            (soyutlama)      (kategori+kural)   (seviye / kategori)  Console · Debug · EventSource · (Serilog, OTel ...)
```

- **`ILogger<T>`**: Servise constructor injection ile gelir (J6.2). `T`, **kategori adını** belirler: `ILogger<OrderService>` → kategori `Junior.AspNetCoreFundamentals.LoggingBasics.OrderService` (türün tam adı). Kategori "bu satır kimden geliyor?" demektir ve **filtrelemenin anahtarıdır**.
- **Log seviyesi (`LogLevel`)**: Satırın önemi. Hangi seviyenin yazılacağı konfigürasyondan (`Logging:LogLevel`) gelir, kod değişmeden.
- **Provider**: Satırın nereye yazılacağı. `WebApplication.CreateBuilder` varsayılan olarak **Console, Debug** (debugger çıktı penceresi), **EventSource** ve yalnızca Windows'ta **EventLog** ekler. Dosya, Seq, Elastic, Application Insights için ek provider/kütüphane gerekir.

```csharp
public sealed class OrderService(ILogger<OrderService> logger)
{
    public void PlaceOrder(int orderId, int itemCount) =>
        logger.LogInformation("Order {OrderId} placed with {ItemCount} items", orderId, itemCount);
}
```

> Bu konu **temeli** anlatır. Serilog, OpenTelemetry, dağıtık trace, log aggregation (Seq/ELK/Loki), sampling ve alarmlar → **M13 (Observability)**. Burada üçüncü parti paket kullanılmaz.

---

## 2. Neden?

- **Production teşhisi:** "Dün gece 03:00'te sipariş neden düştü?" sorusunun cevabı loglardadır. İyi log = dakikalar, kötü log = saatler.
- **Seviye ayarı = kod değişmeden gürültü kontrolü:** Geliştirmede `Debug` ile her şeyi görür, production'da `Warning` ile yalnızca sorunları yazarsınız — aynı kod, farklı `appsettings`.
- **Structured logging = sorgulanabilir log:** `User 42 logged in` düz metni yerine `UserId=42` alanı sayesinde "bu kullanıcının son 1 saatteki tüm hataları" tek sorguyla bulunur. Mülakatların en sevdiği logging sorusu budur ⭐.
- **Güvenlik ve uyum:** Loglar dosyalarda, dashboard'larda, yedeklerde ve üçüncü parti SaaS'larda aylarca yaşar ve çok kişi okur. Şifre/token/kişisel veri (PII) loglamak **KVKK/GDPR ihlali** ve güvenlik açığıdır.
- **Maliyet:** Her log satırı CPU, bellek ve I/O harcar; log saklama ve ingestion (Application Insights, Datadog …) **para** demektir. Seviye disiplini bunu kontrol eder.

---

## 3. Nasıl?

### 3.1 Çalıştırma

```bash
cd WebApp
dotnet run -- J6.5                                   # Production (launchSettings yok → varsayılan ortam Production)
dotnet run -- J6.5 --environment Development         # Development
dotnet run -- J6.5 --json                            # JSON console formatter
dotnet run -- J6.5 --json --environment Development  # ikisi birlikte
```

Uygulama `http://localhost:5080` üzerinde dinler; endpoint listesi için `curl http://localhost:5080/`. Sunucu konsolunu **yan pencerede açık tutun**: bu konunun asıl çıktısı orada.

> `--json` bu demonun kendi bayrağıdır. Kod, onu `CreateBuilder`'a vermeden önce `args`'tan ayıklar; aksi halde command-line provider `--json --environment Development` ifadesini `json="--environment"` diye okuyup gerçek argümanı yutardı.

### 3.2 Log seviyeleri — hangisi ne zaman? ⭐

| Seviye | Değer | Ne zaman | Örnek | Production'da? |
|--------|:-----:|----------|-------|----------------|
| `Trace` | 0 | En ince detay; hassas veri içerebilir | `Entering PlaceOrder(1001)` | **Hayır** (varsayılan kapalı olmalı) |
| `Debug` | 1 | Geliştirici teşhisi, akış bilgisi | `Calculating tax for order 1001` | Normalde hayır; sorun anında geçici açılır |
| `Information` | 2 | Normal iş akışının **önemli** olayları | `Order 1001 placed` | Evet (ama makul miktarda) |
| `Warning` | 3 | Beklenmedik ama **uygulama devam ediyor** | `Stock is running low`, retry oldu | Evet |
| `Error` | 4 | Bir işlem/istek **başarısız oldu**, uygulama ayakta | Yakalanan exception, ödeme reddi | Evet; genelde alarm |
| `Critical` | 5 | Uygulama veya kritik bağımlılık **çöktü**, acil müdahale | DB'ye hiç ulaşılamıyor, disk dolu | Evet; hemen alarm |
| `None` | 6 | Hiçbir şey yazma (sadece filtre kuralında anlamlı) | — | — |

Pratik kural: *"Bu satırı gece 3'te bir insanın görmesi gerekiyor mu?"* → `Error`/`Critical`. *"Sorun çıkarsa sebebi anlamama yardım eder mi?"* → `Information`/`Warning`. *"Sadece kodu yazarken lazım"* → `Debug`/`Trace`. Her `catch` bloğu `Error` değildir: beklenen bir durum (kullanıcı yanlış parola girdi) `Warning`/`Information` olabilir.

[`OrderService.cs`](OrderService.cs) her seviyede bir satır yazar:

```csharp
logger.LogTrace("Entering PlaceOrder({OrderId})", orderId);
logger.LogDebug("Calculating tax and discounts for order {OrderId}", orderId);
logger.LogInformation("Order {OrderId} placed with {ItemCount} items", orderId, itemCount);
logger.LogWarning("Stock for order {OrderId} is running low, {Remaining} left", orderId, 3);
logger.LogError("Payment gateway rejected order {OrderId}: {Reason}", orderId, "card_declined");
logger.LogCritical("Order database is unreachable, order {OrderId} cannot be saved", orderId);
```

### 3.3 Seviyeyi `appsettings.json`'dan ayarlamak — ortam bazlı

Üç dosya var; ortam dosyası temel dosyanın üstüne biner (**sonra gelen kazanır**, → J6.3). `Run` bunları açıkça yükler:

```csharp
builder.Configuration
    .AddJsonFile(Path.Combine(folder, "appsettings.json"), optional: false, reloadOnChange: true)
    .AddJsonFile(Path.Combine(folder, $"appsettings.{builder.Environment.EnvironmentName}.json"), optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()   // CreateBuilder bunları zaten eklemişti ama JSON dosyalarımız ÜSTÜNE binerdi;
    .AddCommandLine(args);       // yeniden ekleyerek gerçek sırayı kurarız: JSON < env var < command line
```

| Dosya | `Logging:LogLevel` |
|-------|--------------------|
| `appsettings.json` (temel) | `Default: Information`, `Microsoft.AspNetCore: Warning`, `Microsoft.Hosting.Lifetime: Information` |
| `appsettings.Development.json` | `Default: Debug`, `Microsoft.AspNetCore: Warning` |
| `appsettings.Production.json` | `Default: Warning`, `Microsoft.Hosting.Lifetime: Information`, `Junior.AspNetCoreFundamentals.LoggingBasics: Information` |

**Production** (`dotnet run -- J6.5`, sonra `curl http://localhost:5080/log`) — sunucu konsolu:

```
16:38:33 info: Microsoft.Hosting.Lifetime[14] Now listening on: http://localhost:5080
16:38:33 info: Microsoft.Hosting.Lifetime[0] Application started. Press Ctrl+C to shut down.
16:38:33 info: Microsoft.Hosting.Lifetime[0] Hosting environment: Production
16:38:37 info: Junior.AspNetCoreFundamentals.LoggingBasics.OrderService[0] Order 1001 placed with 2 items
16:38:37 warn: Junior.AspNetCoreFundamentals.LoggingBasics.OrderService[0] Stock for order 1001 is running low, 3 left
16:38:37 fail: Junior.AspNetCoreFundamentals.LoggingBasics.OrderService[0] Payment gateway rejected order 1001: card_declined
16:38:37 crit: Junior.AspNetCoreFundamentals.LoggingBasics.OrderService[0] Order database is unreachable, order 1001 cannot be saved
```

`Trace` ve `Debug` satırları yok; `Information` ve üstü var. **Development** (`--environment Development`):

```
16:38:45 dbug: Microsoft.Extensions.Hosting.Internal.Host[1] Hosting starting
16:38:45 info: Microsoft.Hosting.Lifetime[14] Now listening on: http://localhost:5080
16:38:45 info: Microsoft.Hosting.Lifetime[0] Hosting environment: Development
16:38:49 dbug: Junior.AspNetCoreFundamentals.LoggingBasics.OrderService[0] Calculating tax and discounts for order 1001
16:38:49 info: Junior.AspNetCoreFundamentals.LoggingBasics.OrderService[0] Order 1001 placed with 2 items
16:38:49 warn: Junior.AspNetCoreFundamentals.LoggingBasics.OrderService[0] Stock for order 1001 is running low, 3 left
16:38:49 fail: Junior.AspNetCoreFundamentals.LoggingBasics.OrderService[0] Payment gateway rejected order 1001: card_declined
16:38:49 crit: Junior.AspNetCoreFundamentals.LoggingBasics.OrderService[0] Order database is unreachable, order 1001 cannot be saved
16:38:49 dbug: Junior.AspNetCoreFundamentals.LoggingBasics.OrderService[0] Basket snapshot for order 1001: item-1, item-2
```

Artık `Debug` satırları (framework'ün `Hosting starting` dahil) görünüyor; `Trace` hâlâ yok çünkü `Default` `Debug`. `/log` endpoint'i aynı bilgiyi JSON olarak da döner (`logger.IsEnabled(...)` ile):

```json
{ "environment": "Development", "configuredDefault": "Debug",
  "enabledLevels": { "Trace": false, "Debug": true, "Information": true, "Warning": true, "Error": true, "Critical": true } }
```

**Command-line override** (en yüksek öncelik, ortam fark etmez):

```bash
dotnet run -- J6.5 --Logging:LogLevel:Junior.AspNetCoreFundamentals.LoggingBasics=Trace
```
```
16:39:11 trce: Junior.AspNetCoreFundamentals.LoggingBasics.OrderService[0] Entering PlaceOrder(1001)
16:39:11 dbug: Junior.AspNetCoreFundamentals.LoggingBasics.OrderService[0] Calculating tax and discounts for order 1001
16:39:11 info: ...OrderService[0] Order 1001 placed with 2 items
...
```

Aynı anahtar env var olarak `Logging__LogLevel__Default=Debug` (çift alt çizgi, → J6.3) — container/Kubernetes'te seviye değiştirmenin standart yolu budur; kodu yeniden derlemeye gerek yok.

**Canlı yeniden yükleme:** `reloadOnChange: true` olduğu için uygulama çalışırken `appsettings.Production.json` içinde `"Junior...LoggingBasics": "Information"` → `"Trace"` yapıp kaydederseniz, **restart etmeden** bir sonraki `/log` isteğinde `trce`/`dbug` satırları görünür (denendi: 3 sn içinde devreye girdi). Production'da canlı sorun ayıklarken kullanışlıdır; işiniz bitince geri alın.

### 3.4 Filtre kuralları ve öncelik ⭐

`Logging:LogLevel` altındaki her anahtar bir **kategori öneki**dir (`Default` hariç). Bir kategori için kural şöyle seçilir:

1. Provider'a özel kural varsa (`Logging:Console:LogLevel:...`) genel kuralı **ezer**; yoksa `Logging:LogLevel` kullanılır.
2. Eşleşen kurallar arasından kategori adına **en uzun önek** kazanır (**en spesifik kazanır**); hiçbiri eşleşmezse `Default`.
3. Kural `None` ise o kategori tamamen susar.

`GET /levels` bunu doğrudan gösterir (Production, `curl http://localhost:5080/levels`; çıktı kısaltıldı):

```
rules:  Default=Warning  Microsoft.AspNetCore=Warning  Microsoft.Hosting.Lifetime=Information
        Junior.AspNetCoreFundamentals.LoggingBasics=Information

category                                                     Information?  Warning?
Junior.AspNetCoreFundamentals.LoggingBasics.OrderService     true          true     ← "Junior...LoggingBasics" öneki (Information)
Junior.AspNetCoreFundamentals.LoggingBasics.ShippingService  true          true     ← aynı önek
Microsoft.AspNetCore.Routing.EndpointMiddleware              false         true     ← "Microsoft.AspNetCore" (Warning)
Microsoft.Hosting.Lifetime                                   true          true     ← tam eşleşme (Information)
System.Net.Http.HttpClient                                   false         true     ← hiçbiri → Default (Warning)
```

**Klasik tuzak (denendi):** Production'da `--Logging:LogLevel:Default=Trace` verirseniz `OrderService` **yine** Trace yazmaz — çünkü `Junior.AspNetCoreFundamentals.LoggingBasics = Information` kuralı `Default`'tan daha spesifiktir. Sorun "Default'u düşürdüm ama bizim servis hâlâ susuyor" şeklinde yaşanır; doğru ayar, spesifik kuralı değiştirmektir (yukarıdaki command-line örneği).

Neden `Microsoft.AspNetCore: Warning`? Aksi halde **her istek** için framework 5–10 `Information` satırı (`Request starting`, `Executing endpoint` …) yazar ve kendi loglarınız gürültüde boğulur. `Default: Information` + `Microsoft.AspNetCore: Warning` şablon projedeki varsayılandır.

Seviye değiştirmek için **kod yazmak da mümkündür** (`builder.Logging.SetMinimumLevel(LogLevel.Debug)`, `AddFilter("System", LogLevel.Warning)`), ama konfigürasyon tercih edilir: yeniden derleme/deploy gerektirmez.

### 3.5 JSON console formatter

Varsayılan console formatter insan içindir (`info: Category[0] mesaj`). Log toplayıcılar (Loki, Elastic, Datadog, Azure Monitor container insights) makine okunur satır ister: `--json` bayrağı `builder.Logging.AddJsonConsole(...)` ekler (satır başına bir JSON nesnesi):

```json
{"Timestamp":"2026-10-24T16:40:43.236Z","EventId":0,"LogLevel":"Information",
 "Category":"Junior.AspNetCoreFundamentals.LoggingBasics.OrderService",
 "Message":"Order 1001 placed with 2 items",
 "State":{"OrderId":1001,"ItemCount":2,"{OriginalFormat}":"Order {OrderId} placed with {ItemCount} items"},
 "Scopes":[
   {"Message":"SpanId:821ab57c976ce9e0, TraceId:f93350688383056a77d076e520244222, ParentId:0000000000000000","SpanId":"821ab57c...","TraceId":"f9335068...","ParentId":"0000000000000000"},
   {"Message":"ConnectionId:0HNPHTM5R2D4E","ConnectionId":"0HNPHTM5R2D4E"},
   {"Message":"RequestPath:/scope RequestId:0HNPHTM5R2D4E:00000001","RequestId":"0HNPHTM5R2D4E:00000001","RequestPath":"/scope"},
   {"Message":"System.Collections.Generic.Dictionary`2[System.String,System.Object]","CorrelationId":"abc123"}]}
```

(Gerçek çıktı tek satırdır; okunabilirlik için bölündü ve bazı değerler kısaltıldı.) Dikkat edilecekler:

- `State` içinde **`OrderId` ve `ItemCount` ayrı alanlar** — structured logging'in somut karşılığı. `{OriginalFormat}` ham template'tir: aynı olayın tüm örneklerini **template'e göre gruplayabilirsiniz**.
- `Scopes` içinde ASP.NET Core'un kendi açtığı scope'lar (`TraceId`, `RequestId`, `RequestPath`) ve bizim `CorrelationId`'miz var. Yani framework her satıra "hangi istek?" bilgisini zaten ekliyor; simple formatter'da bunlar `IncludeScopes = true` ile görülür (bu demoda kapalı, çünkü satırları uzatır).
- `IncludeScopes` açık değilse `Scopes` boş gelir. Biçim konfigürasyondan da seçilebilir: `"Logging": { "Console": { "FormatterName": "json" } }`.
- Container'da kural: **stdout'a JSON, toplama işini platform yapsın** (dosya yönetmeyin).

### 3.6 Structured logging — interpolation neden yanlış ⭐

```csharp
logger.LogInformation("User {UserId} logged in from {City}", userId, city);   // ✅ template + değerler
logger.LogInformation($"User {userId} logged in from {city}");                // ❌ string zaten birleştirilmiş
```

Konsolda ikisi de aynı görünür: `User 42 logged in from Izmir`. Fark, **provider'ın aldığı `state`'te** ortaya çıkar. [`CapturingLoggerProvider`](CapturingLoggerProvider.cs) her girdinin `state`'ini (`IReadOnlyList<KeyValuePair<string, object?>>`) saklar; `GET /structured-vs-interpolated` ikisini yazıp provider'ın gördüğünü döner:

```json
{
  "entries": [
    { "message":  "User 42 logged in from Izmir",
      "template": "User {UserId} logged in from {City}",
      "properties": { "UserId": 42, "City": "Izmir" } },

    { "message":  "User 42 logged in from Izmir",
      "template": "User 42 logged in from Izmir",
      "properties": {} }
  ]
}
```

| | `{UserId}` template ✅ | `$"..."` interpolation ❌ |
|---|---|---|
| Template | Sabit: `User {UserId} logged in from {City}` | Her kullanıcı için farklı string |
| Property'ler | `UserId=42`, `City="Izmir"` (tipli, ayrı alan) | **Yok** — sadece düz metin |
| Sorgu | `UserId = 42` / `GROUP BY template` | Metin içinde `LIKE '%User 42%'` (yavaş, kırılgan) |
| Maliyet | Seviye kapalıysa string **hiç üretilmez** | String her zaman üretilir (alloc + format) |
| Analizör | — | CA2254 ("template sabit olmalı") uyarısı |

Detaylar:

- Placeholder isimleri **PascalCase** olur (`{UserId}`, `{orderId}` değil) — bunlar log şemanızın alan adlarıdır.
- Argümanlar **sıraya göre** bağlanır, isme göre değil: `("{A} {B}", b, a)` alanları ters yazar. Yanlış sıra derleyici hatası vermez.
- Biçim belirteci çalışır: `{Total:C}`; ama `{@Order}` (destructuring) ve `{$Type}` **Serilog'a özgüdür**, MEL'de yoktur (→ M13).
- Aynı nesneyi `{Order}` ile geçerseniz `ToString()` çağrılır — kasıtlı olarak alan alan loglayın.

### 3.7 `[LoggerMessage]` — source-generated logging

[`ShippingService.cs`](ShippingService.cs) compile-time üretilen log metodlarını gösterir:

```csharp
public sealed partial class ShippingService      // partial şart
{
    private readonly ILogger<ShippingService> _logger;
    public ShippingService(ILogger<ShippingService> logger) => _logger = logger;

    [LoggerMessage(EventId = 2001, Level = LogLevel.Information, Message = "Order {OrderId} shipped to {City} in {ElapsedMs} ms")]
    private partial void OrderShipped(int orderId, string city, long elapsedMs);

    [LoggerMessage(EventId = 2002, Level = LogLevel.Error, Message = "Shipping failed for order {OrderId}")]
    private partial void ShippingFailed(Exception ex, int orderId);   // Exception parametresi otomatik tanınır
}
```

`GET /source-generated` çıktısı (kısaltıldı) — template ve property'ler `LogInformation` ile aynı yapıda, ayrıca `EventId` (konsolda `[2001]`) ve `Exception` gelir:

```json
{ "entries": [
  { "level": "Information", "message": "Order 1001 shipped to Ankara in 0 ms",
    "template": "Order {OrderId} shipped to {City} in {ElapsedMs} ms",
    "properties": { "OrderId": 1001, "City": "Ankara", "ElapsedMs": 0 }, "exceptionType": null },
  { "level": "Error", "message": "Shipping failed for order 1001",
    "template": "Shipping failed for order {OrderId}",
    "properties": { "OrderId": 1001 }, "exceptionType": "TimeoutException" } ] }
```

Neden? Generator, arka planda `LoggerMessage.Define` benzeri **önbelleğe alınmış delegate** ve `IsEnabled` kontrolü üretir: template her çağrıda yeniden ayrıştırılmaz, değer tipleri **boxing'e girmez**, parametre dizisi (`params object[]`) ayrılmaz. Hot path'lerde (saniyede binlerce çağrı) fark ölçülebilir; ayrıca log şeması (EventId + template) tek yerde tanımlı olur. Günlük kodda `LogInformation` yeterlidir; **performans-kritik veya kütüphane kodunda** `[LoggerMessage]` tercih edin.

### 3.8 Scope — `BeginScope` ve correlation id

Scope, `using` bloğu içinde yazılan **tüm** log satırlarına ortak bir bağlam ekler (parametre taşımadan):

```csharp
using (logger.BeginScope(new Dictionary<string, object> { ["CorrelationId"] = correlationId }))
{
    logger.LogInformation("Handling the request");
    orders.PlaceOrder(orderId: 1001, itemCount: 2);   // BAŞKA bir sınıf, aynı scope
    logger.LogInformation("Request handled");
}
logger.LogInformation("Outside the scope - no CorrelationId here");
```

`curl -H "X-Correlation-Id: abc123" http://localhost:5080/scope` (kısaltıldı):

```json
{
  "correlationId": "abc123",
  "frameworkScopes": [
    "SpanId=83c2cc2c2798a287, TraceId=fb6c2b6521698d8b0fbb14312c46b43e, ParentId=0000000000000000",
    "ConnectionId=0HNPHTLN5HFMG",
    "RequestId=0HNPHTLN5HFMG:00000001, RequestPath=/scope" ],
  "entries": [
    { "category": "...StructuredLoggingDemo", "message": "Handling the request",             "scopes": ["CorrelationId=abc123"] },
    { "category": "...OrderService",          "message": "Order 1001 placed with 2 items",   "scopes": ["CorrelationId=abc123"] },
    { "category": "...StructuredLoggingDemo", "message": "Request handled",                  "scopes": ["CorrelationId=abc123"] },
    { "category": "...StructuredLoggingDemo", "message": "Outside the scope - no CorrelationId here", "scopes": [] }
  ]
}
```

`OrderService` hiçbir şeyden habersiz olduğu halde satırı `CorrelationId`'yi taşıdı; scope bloğundan çıkınca kayboldu. Gerçek hayatta bunu bir **middleware** yapar (header'dan okur/üretir, response'a yazar → J6.4) ve aynı id downstream HTTP çağrılarına da taşınır (→ M13). Scope'ların JSON'a yansıması 3.5'teki çıktıdadır; simple formatter'da `IncludeScopes = true` gerekir.

### 3.9 Exception loglamak ⭐

```csharp
catch (InvalidOperationException ex)
{
    logger.LogError(ex, "Payment failed for order {OrderId}", orderId);   // ✅ exception ilk parametre
}
```

```csharp
logger.LogError($"Payment failed for order {orderId}: {ex}");              // ❌ ex.ToString() mesajın içine gömülür
logger.LogError("Payment failed: " + ex.Message);                          // ❌ stack trace ve iç exception kayboldu
```

`GET /exception` ikisini de yazar ve provider'ın gördüğünü verir:

```json
{ "entries": [
  { "template": "Payment failed for order {OrderId}",
    "properties": { "OrderId": 7 }, "exceptionType": "InvalidOperationException", "exceptionTextInTemplate": false },
  { "template": "Payment failed for order 8: System.InvalidOperationException: Gateway ...",
    "properties": {}, "exceptionType": null, "exceptionTextInTemplate": true } ] }
```

İlkinde exception **ayrı bir nesne**: provider tipini, mesajını, stack trace'ini, `InnerException`'ı alan alan indeksleyebilir, hata dashboard'ları "aynı exception tipi" ile gruplar, template sabit kaldığı için aynı olay gruplanır. İkincisinde stack trace metne gömüldü, `exceptionType` `null`, ve template her sipariş için farklı. Tek `catch`, **tek kez** loglayın: her katmanda `catch → log → throw` aynı hatayı 5 kez yazar.

### 3.10 Şifre, token ve kişisel veri **loglanmaz** ⭐

Loglar dosyada, dashboard'da, yedekte ve üçüncü parti log servisinde yaşar; erişen kişi sayısı üretim veritabanına erişenden çok daha fazladır. Bu yüzden:

| Asla loglanmaz | Maskele / hash'le |
|----------------|-------------------|
| Parola, PIN, OTP | E-posta (`d********@example.com`) |
| Access/refresh token, API key, connection string, session id | Telefon, TC kimlik no, adres (genelde hiç) |
| Kart numarası, CVV (PCI-DSS: en fazla son 4 hane) | Kart: `************1111` |
| Tam request/response body (içinde ne olduğunu bilmezsiniz) | Kullanıcıyı `userId` veya hash parmak izi ile tanımlayın |

[`AccountService.cs`](AccountService.cs) aynı login girişimini iki kez yazar (tüm değerler **SAHTE**) — `GET /sensitive`:

```json
{ "entries": [
  { "message": "LEAKY login attempt: email=demo.user@example.com password=P@ssw0rd-FAKE token=eyFAKE.TOKEN.NOT-REAL card=4111111111111111" },
  { "message": "Redacted login attempt: email=d********@example.com userFingerprint=3c76c823 password=[REDACTED] token=[REDACTED] card=************1111" } ] }
```

[`LogRedactor`](LogRedactor.cs): `MaskEmail`, `MaskCard`, `Fingerprint` (SHA-256'nın ilk 8 hex'i — "aynı kullanıcı" bağlantısını kurar, kim olduğunu söylemez). Önemli nüanslar:

- Parola/token için **maskeleme bile yok, hiç loglamayın**; `[REDACTED]` yer tutucusu bile gereksizse alanı hiç yazmayın.
- Sızıntı çoğu zaman "masum" bir satırdan olur: `logger.LogDebug("Request: {@Body}", body)`, `ex.ToString()` içindeki connection string, URL query'sindeki `?token=...` (hata sayfalarında stack trace/connection string sızıntısı → J6.6).
- **Log injection:** Kullanıcı girdisi satır sonu (`\r\n`) içerip sahte log satırı üretebilir. Structured logging (template + değer) bunu büyük ölçüde azaltır; yine de ham girdiyi birleştirip loglamayın.
- Kurumsal çözüm: framework'ün redaction altyapısı (`Microsoft.Extensions.Compliance.Redaction` ve veri sınıflandırma niteliklerini `[LoggerMessage]` ile birlikte kullanmak) ve saklama süresi politikaları (→ M13, J11). Kod gözden geçirmede "bu alan PII mi?" sorusunu alışkanlık yapın.

### 3.11 Maliyet: seviye kapalıyken ne olur?

```csharp
logger.LogDebug("Basket: {Basket}", DescribeBasket(items));          // DescribeBasket HER ZAMAN çalışır — argüman önce hesaplanır
if (logger.IsEnabled(LogLevel.Debug))                                  // ✅ pahalı hazırlık korunur
    logger.LogDebug("Basket: {Basket}", DescribeBasket(items));
```

- Seviye kapalıysa `LogDebug(...)` mesajı **formatlamaz** ve provider'a göndermez — ucuzdur. Ama **argümanlar çağrıdan önce** değerlendirilir; pahalı bir hesap/serialization varsa `IsEnabled` ile koruyun ([`OrderService.PlaceOrder`](OrderService.cs) sonundaki blok).
- `$"..."` interpolation seviye kapalıyken bile string üretir.
- Ayrıntı için §7.

### 3.12 Serilog ve OpenTelemetry — sadece işaret

Kurumsal projelerde sık görülen ek paketler: **Serilog** (zengin sink'ler, enrichers, `{@Obj}` destructuring; `ILogger` arayüzü aynı kalır, sadece provider değişir), **OpenTelemetry** (log + trace + metric'i ortak `TraceId` ile birleştirir, vendor-nötr export). Bu konuda kullanılmaz; ikisi de MEL'in üstüne oturur, dolayısıyla buradaki `ILogger<T>`, seviye ve template bilgisi doğrudan geçerlidir (→ M13).

---

## 4. Gerçek dünya senaryosu

**"Production'da sipariş düşüyor ama log yok."** Ekip `Default: Warning` ile yayında ve ödeme hataları `Information` ile yazılıyor → hiçbiri görünmüyor. Çözüm süreci:

1. **Kısa vadede:** Deploy etmeden, ortam değişkeniyle spesifik kategoriyi aç: `Logging__LogLevel__MyShop.Payments=Debug` (container restart; ya da dosya/`reloadOnChange` ile anında). `Default`'u değil **ilgili namespace'i** açarsınız — aksi halde framework gürültüsü + maliyet patlar.
2. **Bulma:** Loglar JSON ve `OrderId`, `CorrelationId` alanlı olduğu için log aracında `CorrelationId = "…"` ile tek isteğin tüm satırları (API → servis → ödeme) bir arada görünür. Düz metin (`$"…"`) olsaydı `grep` ile 20 dakikalık dosya taraması gerekirdi.
3. **Kök neden:** `LogError(ex, "Payment failed for order {OrderId}", orderId)` sayesinde stack trace ve iç exception (`SocketException: timeout`) alanları ayrı; dashboard "PaymentGatewayException — son 1 saatte 340" der.
4. **Kalıcı düzeltme:** Ödeme reddi `Warning`, gateway'e ulaşılamaması `Error`, ödeme tablosu erişilemiyor `Critical` olarak yeniden sınıflandırılır; `Error`+ için alarm kurulur. Debug geri kapatılır.
5. **Uyum:** İnceleme sırasında bir `Debug` satırının kart numarasını tam yazdığı fark edilir → maskelenir, geçmiş loglar silinir/saklama politikası uygulanır; olay KVKK sorumlusuna bildirilir.

Bu senaryodaki her adım bu klasörde bir demoya karşılık gelir: seviye+kategori (3.3–3.4), structured (3.6), scope (3.8), exception (3.9), PII (3.10).

---

## 5. Mülakat perspektifi

Sık gelen sorular ([`interview-questions.md`](interview-questions.md)'de cevaplarıyla):

- **"Structured logging nedir, `$"..."` neden yanlış?"** ⭐ → Template + named property'ler provider'a ayrı iletilir; interpolation ile string önceden birleşir, alan kaybolur, gruplama bozulur, seviye kapalıyken bile maliyet ödenir. Demo: `/structured-vs-interpolated`.
- **"Log seviyeleri ve hangisi ne zaman?"** → Tablo 3.2; örneklerle `Warning` ≠ `Error` ayrımını anlatın.
- **"Seviye nasıl ayarlanır, ortama göre nasıl değişir?"** → `Logging:LogLevel` + `appsettings.{Env}.json` + env var / command-line; kod değişmez; kategori öneki ve "en spesifik kazanır".
- **"`ILogger<T>`'deki `T` ne işe yarar?"** → Kategori adı; filtreleme ve log'da "kim yazdı" için.
- **"Exception nasıl loglanır?"** → `LogError(ex, "…")`; `ex.Message` veya `ToString()` ile birleştirmek yanlış.
- **"Ne loglanmaz?"** → Parola, token, kart, PII; neden (KVKK/GDPR, log'ların ömrü ve erişim kitlesi).
- Derinleştirme: `[LoggerMessage]`, `BeginScope`, `IsEnabled`, JSON formatter, Serilog'dan farkı.

**İpucu:** Cevabınızı şu cümleyle kapatın: *"Log, kod değil konfigürasyonla ayarlanabilmeli ve production'da sorgulanabilmeli; bu yüzden structured, doğru seviyede ve hassas veri içermeden yazarım."*

---

## 6. Yaygın hatalar

| Hata | Sonuç | Doğrusu |
|------|-------|---------|
| `LogInformation($"User {id} ...")` | Property yok, gruplanamaz, maliyet her zaman | `LogInformation("User {UserId} ...", id)` |
| `LogError("Failed: " + ex.Message)` / `ex.ToString()` | Stack trace/tip kaybı veya metne gömülü | `LogError(ex, "Failed for {OrderId}", id)` |
| Her şeyi `Information` yazmak | Gürültü, maliyet; gerçek sorun kaybolur | Seviye tablosu (3.2) |
| Her `catch`'te `LogError` + `throw` | Aynı hata N kez loglanır | Yakalayıp ele alan bir yerde logla |
| `Default: Trace/Debug` ile production'a çıkmak | Hacim, maliyet, hassas veri sızıntısı | Spesifik kategoriyi geçici aç |
| Spesifik kural varken `Default`'u değiştirmek | "Seviyeyi düşürdüm ama değişmedi" (3.4) | Kategori önekini/spesifik anahtarı değiştir |
| Parola/token/PII loglamak | KVKK/GDPR ihlali, güvenlik açığı | Loglama / maskele / hash'le (3.10) |
| Tüm request/response body'yi loglamak | PII + hacim | Seçili alanlar; body logging'i bilinçli, geçici aç |
| Pahalı argümanı korumasız `LogDebug`'a vermek | Debug kapalıyken bile CPU harcanır | `IsEnabled` veya `[LoggerMessage]` |
| Log içinde iş mantığı (side-effect) | Seviye değişince davranış değişir | Log yalnızca gözlemler |
| Placeholder isimlerini camelCase/rastgele yazmak | Şema dağınıklığı (`userId`, `UserID`, `user`) | Tutarlı PascalCase isimler |
| Log'u tek kaynak sanıp metric/trace'i atlamak | Büyük resim eksik | Log + metric + trace (→ M13) |

---

## 7. Performans & trade-off

- **Kapalı seviye ucuzdur, ama bedava değildir:** `LogDebug` seviye kapalıyken formatlamaz; fakat çağrı, `params object[]` dizisi ayırır ve değer tipleri box'lanır. **Hot path'te** `[LoggerMessage]` (3.7) bunu sıfırlar.
- **Argüman hesabı log'dan önce:** `DescribeBasket(...)` seviye kapalıyken de çalışır → `IsEnabled` ile koru.
- **I/O baskın maliyet:** Console provider yazmayı kuyruğa alır ve ayrı thread'de yazar; kuyruk dolarsa (varsayılan davranış) çağıran **bekleyebilir** (`ConsoleLoggerOptions.QueueFullMode`/`MaxQueueLength` ile ayarlanır). Saniyede on binlerce satır yazan bir `Debug` kategorisi uygulamayı yavaşlatabilir.
- **Hacim = para:** Application Insights/Datadog gibi servisler GB başına faturalar. Hacmi azaltma sırası: gereksiz kategori seviyesini yükselt (`Microsoft.*: Warning`) → sampling → saklama süresini kısalt.
- **Structured vs okunabilirlik:** JSON makine için harika, insan için zor; çözüm: lokalde simple formatter, production'da JSON (ortam bazlı seçim).
- **Senkron vs asenkron provider:** Dosyaya doğrudan yazan senkron provider isteği bloklar; kuyruklu/asenkron sink (Serilog `WriteTo.Async` vb.) tercih edilir (→ M13).
- **Trade-off:** Ne kadar çok bağlam (scope, property) o kadar iyi teşhis, ama o kadar çok bayt ve olası PII. "Gece 3'te işime yarar mı?" testini kullanın.

---

## 8. Kaynaklar

- Microsoft Learn — *Logging in .NET and ASP.NET Core* (providers, filtering, `appsettings` yapısı, scopes)
- Microsoft Learn — *Compile-time logging source generation* (`[LoggerMessage]`)
- Microsoft Learn — *Console log formatting* (simple / json / systemd formatter ve seçenekleri)
- Microsoft Learn — *High-performance logging* ve *Logging guidance for .NET library authors*
- Microsoft Learn — CA2254 *Template should be a static expression*
- OWASP — *Logging Cheat Sheet* (neyin loglanmayacağı, log injection)
- Message Templates spesifikasyonu (messagetemplates.org) — Serilog/MEL'in ortak template dili
- Bu repoda: DI → J6.2, configuration → J6.3, middleware (correlation id) → J6.4, ortam & hata sayfaları → J6.6, Serilog/OpenTelemetry/log toplama → M13

<details>
<summary>Alıştırmalar ve çözümleri</summary>

1. **Production'da `Debug` satırlarını canlı aç.** `dotnet run -- J6.5` ile başlatın, `/log` çağırın (Trace/Debug yok). Uygulama çalışırken `appsettings.Production.json`'da `"Junior.AspNetCoreFundamentals.LoggingBasics"` değerini `"Debug"` yapıp kaydedin, tekrar `/log` çağırın.
   *Beklenen:* restart olmadan `dbug` satırları görünür (`reloadOnChange`). İşiniz bitince `"Information"`'a geri alın.
2. **Önceliği kendiniz kırın.** `dotnet run -- J6.5 --Logging:LogLevel:Default=Trace` ile başlatıp `/log` ve `/levels` çağırın.
   *Beklenen:* `OrderService` hâlâ Trace yazmaz (spesifik kural `Information`). Sonra `--Logging:LogLevel:Junior.AspNetCoreFundamentals.LoggingBasics=Trace` deneyin → artık yazar.
3. **Provider'a özel kural.** `appsettings.json`'a `"Console": { "LogLevel": { "Default": "Error" } }` ekleyin (`Logging` altına). `/log` çağırın.
   *Beklenen (komut satırıyla `--Logging:Console:LogLevel:Default=Error` olarak denendi):* Console provider yalnızca `fail`/`crit` yazar; `Capture` provider'ı etkilenmez (`/structured-vs-interpolated` hâlâ dolu gelir). `/log` içindeki `enabledLevels` ise `Information: true` göstermeye devam eder, çünkü `IsEnabled` "**herhangi bir** provider yazar mı?" sorusunu yanıtlar.
4. **Environment variable ile seviye.** `Logging__LogLevel__Default=Debug dotnet run -- J6.5 --environment Production` (Linux/macOS) → `curl /levels`; çift alt çizgi `:` yerine geçer.
   *Beklenen:* `configuredRules.Default` `Debug` olur (env var, JSON'u ezer) ama `OrderService` hâlâ `Debug: false` — spesifik `Junior...LoggingBasics` kuralı (3.4).
5. **Redactor yaz.** `LogRedactor`'a telefon numarası için `MaskPhone("+905551234567")` → `+90555***4567` ekleyin; `AccountService.LogRedacted` içinde kullanın.
6. **`[LoggerMessage]`'a seviye parametresi ver.** `OrderShipped` metoduna `LogLevel level` parametresi ekleyin (`[LoggerMessage(EventId = 2003, Message = "...")] partial void X(LogLevel level, ...)`) ve `Ship` içinde `Warning` ile çağırın.

</details>
