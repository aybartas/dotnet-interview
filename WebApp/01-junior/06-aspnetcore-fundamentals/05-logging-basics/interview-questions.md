# J6.5 Logging Temelleri — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan. Demoyu çalıştırın: `cd WebApp && dotnet run -- J6.5`.

### 1. ⭐ Structured logging nedir? `logger.LogInformation($"User {id} logged in")` neden yanlış?
Structured logging, log satırını **sabit bir message template + adlandırılmış değerler** olarak göndermektir: `LogInformation("User {UserId} logged in", id)`. Provider'a hem template (`User {UserId} logged in`) hem de `UserId=42` gibi tipli property'ler ayrı ayrı gelir; JSON/Seq/Elastic gibi hedefler bunları **sorgulanabilir alan** olarak saklar (`UserId = 42`, template'e göre gruplama). Interpolation (`$"..."`) ise string'i **logger çağrılmadan önce** birleştirir: provider'a düz metin gider, property yoktur, template her kullanıcı için farklıdır (gruplanamaz), ve seviye kapalı olsa bile string üretme maliyeti ödenir. Ayrıca CA2254 analizörü uyarır. Konsolda ikisi de aynı görünür; fark `state`'te ortaya çıkar (demo: `/structured-vs-interpolated`).

### 2. ⭐ Log seviyeleri nelerdir, hangisi ne zaman kullanılır?
`Trace` (en ince detay, hassas veri içerebilir, production'da kapalı), `Debug` (geliştirici teşhisi), `Information` (normal akışın önemli olayları), `Warning` (beklenmedik ama uygulama devam ediyor: düşük stok, retry), `Error` (bir işlem/istek başarısız oldu, uygulama ayakta), `Critical` (uygulama/kritik bağımlılık çöktü, acil müdahale). Ek olarak `None` yalnızca filtrede "sustur" anlamında vardır. Pratik test: gece 3'te bir insanın görmesi gerekiyorsa `Error`/`Critical`. Her `catch` `Error` değildir; beklenen iş durumları (yanlış parola) `Warning`/`Information` olabilir.

### 3. ⭐ `ILogger<T>` nedir, `T` ne işe yarar?
`ILogger<T>`, DI ile enjekte edilen tipli logger'dır. `T`, **kategori adını** belirler (türün tam adı, örn. `MyApp.Services.OrderService`). Kategori iki işe yarar: (1) log satırında "kim yazdı" bilgisi, (2) `Logging:LogLevel` kurallarının eşleştirildiği anahtar — kategori önekine göre seviye ayarlanır (`MyApp.Services: Debug`). `ILoggerFactory.CreateLogger("Ozel.Kategori")` ile elle kategori de verilebilir. Statik sınıflarda `T` olamayacağı için factory kullanılır.

### 4. ⭐ Log seviyesini kod değiştirmeden nasıl ayarlarsınız? Ortama göre nasıl değişir?
`appsettings.json` içindeki `Logging:LogLevel` bölümüyle. `appsettings.{Environment}.json` temel dosyanın üstüne biner (sonra gelen kazanır); environment variable (`Logging__LogLevel__Default=Debug`, çift alt çizgi `:` yerine) ve command-line (`--Logging:LogLevel:Default=Debug`) en yüksek önceliktedir. Böylece Development'ta `Debug`, Production'da `Warning` olur; container'da yeniden build etmeden env var ile değiştirilir. Dosya `reloadOnChange` ile yüklendiyse değişiklik **restart gerektirmeden** devreye girer. Kodla da mümkündür (`SetMinimumLevel`, `AddFilter`) ama konfigürasyon tercih edilir.

### 5. ⭐ Filtre kuralları nasıl çözülür? Aynı kategori için birden çok kural eşleşirse hangisi kazanır?
Önce provider'a özel kural (`Logging:Console:LogLevel:...`) varsa o, yoksa genel `Logging:LogLevel` kullanılır. Eşleşen kurallar arasında **kategori adına en uzun önek** (en spesifik) kazanır; hiçbiri eşleşmezse `Default`. Örnek: `Default: Warning`, `MyApp: Information` ve `MyApp.Payments: Debug` varsa `MyApp.Payments.StripeClient` için `Debug` uygulanır. Klasik tuzak: spesifik kural varken `Default`'u `Trace` yapıp ilgili kategoride değişiklik görmemek.

### 6. ⭐ Exception nasıl loglanmalı?
`logger.LogError(ex, "Payment failed for order {OrderId}", orderId)` — exception **ilk parametre** olarak verilir. Provider tipi, mesajı, stack trace'i ve inner exception'ı ayrı alanlar olarak alır; template sabit kaldığı için aynı hata gruplanır. Yanlışlar: `LogError("Failed: " + ex.Message)` (stack trace kaybolur), `LogError($"...{ex}")` (exception metne gömülür, nesne olarak kaybolur, template her seferinde değişir). Ayrıca aynı exception'ı her katmanda `catch → log → throw` ile tekrar tekrar loglamayın; ele alındığı yerde bir kez loglayın.

### 7. ⭐ Neler loglanmamalı? Neden?
Parola, token/API key, connection string, kart numarası/CVV, OTP gibi **sırlar** hiç loglanmaz; e-posta, telefon, kimlik no, adres gibi **kişisel veri (PII)** maskelenir/hash'lenir ya da hiç yazılmaz. Sebep: loglar dosyada, dashboard'da, yedekte, üçüncü parti SaaS'ta **uzun süre** yaşar ve veritabanından çok daha fazla kişi tarafından okunur; KVKK/GDPR/PCI-DSS ihlali ve saldırgan için hazır bir kimlik hazinesi olur. Tam request/response body loglamak da risklidir. Çözüm: izin verilen alanları açıkça seçmek, maskeleme yardımcıları (`d***@example.com`, `****1111`), kullanıcıyı `userId`/hash ile tanımlamak, saklama süresi politikası.

### 8. `Warning` ile `Error` arasındaki fark nedir?
`Warning`: beklenmedik bir durum oldu ama işlem **başarıyla tamamlandı/uygulama normal çalışıyor** (retry ile geçti, cache miss yerine DB'ye gidildi, stok azaldı). `Error`: bir işlem/istek **başarısız oldu** (ödeme reddedildi, yakalanan exception, kayıt yazılamadı). Yaygın alarm politikası: `Error` ve üstü alarm üretir, `Warning` trend olarak izlenir. Hepsini `Error` yapmak alarm yorgunluğuna yol açar.

### 9. `Critical` ne zaman kullanılır? `Error`'dan farkı?
Tek bir işlemin değil, **uygulamanın veya kritik bir bağımlılığın** çalışamaz hale geldiğinde: veritabanına hiç ulaşılamıyor, disk dolu, zorunlu konfigürasyon yok ve uygulama başlayamıyor. `Critical` anında müdahale (on-call çağrısı) gerektirir; az kullanılmalıdır, aksi halde anlamını kaybeder.

### 10. `BeginScope` nedir, ne zaman kullanılır?
Bir `using` bloğu içinde yazılan **tüm** log girdilerine ortak bağlam (örn. `CorrelationId`, `OrderId`) ekler; bu bilgiyi her metoda parametre olarak taşımak gerekmez, başka sınıflardaki loglar da dahil olur. Scope'lar provider'a `IExternalScopeProvider` ile akar; console'da `IncludeScopes = true`, JSON formatter'da `Scopes` dizisi olarak görünür. ASP.NET Core her istek için zaten `TraceId`, `RequestId`, `RequestPath` scope'ları açar. Tipik kullanım: correlation id middleware'i (→ J6.4) ile bir isteğin bütün satırlarını bağlamak.

### 11. `[LoggerMessage]` (source-generated logging) nedir, neden kullanılır?
Partial metoda `[LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Order {OrderId} shipped")]` yazarsınız, source generator gövdeyi derleme zamanında üretir. Faydaları: template **bir kez** ayrıştırılır, değer tipleri box'lanmaz, `params object[]` ayrılmaz, `IsEnabled` kontrolü yerleşiktir, EventId/şema tek yerde tanımlıdır, template ile parametre uyumsuzlukları derleme zamanında uyarı verir. Hot path ve kütüphane kodunda tercih edilir; sıradan uygulama kodunda `LogInformation` yeterlidir. Sınıf `partial` olmalı, `ILogger` alan/parametre olarak bulunmalıdır; `Exception` parametresi otomatik tanınır.

### 12. Log seviyesi kapalıyken `logger.LogDebug(...)` çağrısı bedava mıdır?
Neredeyse. Mesaj formatlanmaz ve provider'a gönderilmez, ama **argümanlar çağrıdan önce** hesaplanır; `LogDebug("x {Y}", ExpensiveCall())` içinde `ExpensiveCall()` yine çalışır. Ayrıca `params object[]` ayırma ve değer tipi boxing'i oluşur. Korumak için `if (logger.IsEnabled(LogLevel.Debug))` veya `[LoggerMessage]` kullanılır. `$"..."` interpolation ise seviye kapalıyken bile string üretir.

### 13. Varsayılan logging provider'ları nelerdir? Başka bir yere (dosya, Seq) nasıl yazılır?
`WebApplication.CreateBuilder` varsayılan olarak **Console, Debug, EventSource** ve yalnızca Windows'ta **EventLog** provider'larını ekler. Başka hedefler için bir provider/kütüphane eklenir (Serilog sink'leri, Application Insights, OpenTelemetry exporter vb.); kod tarafında `ILogger` arayüzü değişmez, sadece provider değişir. Container'larda önerilen: **stdout'a (JSON) yaz, toplama işini platform yapsın.** Provider'ları `builder.Logging.ClearProviders()` ile temizleyip yenisini ekleyebilirsiniz (→ M13).

### 14. JSON console formatter ne işe yarar? Simple formatter'dan farkı?
Simple formatter insan için tek satırlık metin üretir (`info: Category[0] mesaj`). JSON formatter her girdiyi **tek satırlık JSON nesnesi** olarak yazar (`Timestamp`, `LogLevel`, `Category`, `Message`, `State` = property'ler + `{OriginalFormat}`, `Scopes`, varsa `Exception`). Log toplayıcılar (Loki, Elastic, Datadog, Container Insights) ayrıştırma kuralı yazmadan alanları indeksler. `builder.Logging.AddJsonConsole()` ile veya `Logging:Console:FormatterName=json` ile açılır. Genel pratik: lokalde okunaklı simple, production'da JSON.

### 15. Neden `Microsoft.AspNetCore: Warning` ayarlanır?
Aksi halde ASP.NET Core her istek için `Information` seviyesinde birkaç satır yazar (`Request starting`, `Executing endpoint`, `Executed endpoint`, `Request finished`). Yoğun trafikte bu hem gürültü hem maliyettir ve kendi iş loglarınızı boğar. Şablon proje bu yüzden `Default: Information` + `Microsoft.AspNetCore: Warning` ile gelir; sorun ayıklarken geçici olarak `Information`/`Debug` yapılır.

### 16. Logging, metrics ve tracing farkı nedir? (Kısa)
**Log:** ayrık olaylar ("ne oldu?", ayrıntılı, pahalı). **Metric:** zamana göre sayısal ölçümler ("kaç, ne kadar sürdü?", ucuz, alarm için ideal). **Trace:** bir isteğin servisler arası yolculuğu (span'ler, `TraceId`). Üçü `TraceId`/correlation id ile birbirine bağlanınca "alarm → ilgili trace → o trace'in logları" gezintisi yapılır. Detay: OpenTelemetry (→ M13).

### 17. Log injection nedir, nasıl azaltılır?
Kullanıcı girdisi satır sonu veya özel karakterler içerip log dosyasına **sahte satırlar** ekleyebilir (örn. `\r\n2026-... INFO Admin logged in`) ve incelemeyi yanıltır. Azaltma: structured logging (değer ayrı property olarak tutulur, JSON formatter kaçışlar), ham girdiyi string birleştirerek loglamamak, gerekirse uzunluk sınırlamak/temizlemek. Log'ları okuyan arayüzlerde de çıktıyı encode etmek (log viewer'da XSS) gerekir.

### 18. Log, hata yönetiminin yerini tutar mı? "Hata alırsam loglar, devam ederim" doğru mu?
Hayır. Loglamak hatayı **ele almak** değildir. Beklenen/kurtarılabilir hata (retry, fallback) ele alınıp `Warning` ile loglanabilir; kurtarılamayan hata yukarı fırlatılmalı (veya kullanıcıya uygun yanıt döndürülmeli, → J6.6/J7.3) ve global bir noktada **bir kez** loglanmalıdır. `catch (Exception) { logger.LogError(...); }` ile yutmak sessiz veri bozulması ve yanıltıcı "başarılı" yanıtlar üretir.
