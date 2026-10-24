# J6.1 `Program.cs` & Host — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. ⭐ Request geldiğinde ASP.NET Core'da ne olur?
1. İstemci TCP bağlantısı kurar (HTTPS ise TLS el sıkışması da yapılır). 2. **Kestrel** bayt akışını HTTP olarak ayrıştırır ve bir `HttpContext` (request + response + connection) üretir. 3. `HttpContext` **middleware pipeline**'ına girer; her middleware `next()` ile sonrakine devreder. 4. **Routing** (`UseRouting`) path + HTTP metodunu endpoint'lerle eşleştirir ve seçilen endpoint'i context'e yazar. 5. **Endpoint** (controller action ya da `MapGet` delegate'i) çalışır; parametreleri model binding / DI ile çözülür. 6. Response yazılır, middleware'lerden **ters sırada** geri döner ve Kestrel bayt olarak istemciye gönderir. Derinleştirmek için: `UseRouting`'den önceki middleware'ler `GetEndpoint()` ile `null` görür, authorization/CORS routing'den **sonra** gelir çünkü endpoint metadata'sına ihtiyaç duyarlar.

### 2. ⭐ `Program.cs`'te sırayla ne yapılır?
`WebApplication.CreateBuilder(args)` → **builder fazı:** `builder.Services` (DI kayıtları), `builder.Configuration`, `builder.Logging`, `builder.WebHost` → `builder.Build()` (container dondurulur) → **pipeline fazı:** `app.Use…` / `app.Map…` (middleware sırası + endpoint'ler) → `app.Run()`. Kural: servis kaydı `Build()`'den **önce**, middleware ve endpoint tanımı `Build()`'den **sonra**.

### 3. ⭐ `WebApplicationBuilder` ile `WebApplication` farkı nedir?
`WebApplicationBuilder` **kurulum** nesnesidir: servis kaydı, configuration kaynakları, logging provider'ları, URL gibi ayarlar toplanır. `Build()` bunu bir `WebApplication`'a çevirir: o da hem **host** (çalıştırma, yaşam döngüsü, `app.Services`, `app.Configuration`, `app.Logger`) hem de **pipeline builder**'dır (`Use`, `Map*`, `Run`). `Build()`'den sonra `builder.Services`'e kayıt eklemek `InvalidOperationException` verir.

### 4. `WebApplication.CreateBuilder` varsayılan olarak neleri kurar?
Kestrel (IIS arkasındaysa IIS entegrasyonu); content root = çalışma dizini; configuration: `appsettings.json` → `appsettings.{Environment}.json` → User Secrets (Development) → environment variable'lar → komut satırı; host ayarları için `DOTNET_` / `ASPNETCORE_` önekli env var'lar; logging: Console, Debug, EventSource (+ Windows'ta EventLog); DI container (Development'ta scope/bağımlılık doğrulaması açık); `UseRouting`/`UseEndpoints` otomatik ekleme. Bu yüzden boş bir proje bile `appsettings.json` okur ve log yazar.

### 5. ⭐ Kestrel nedir? Neden çoğu zaman bir reverse proxy'nin arkasında çalıştırılır?
Kestrel, ASP.NET Core'un varsayılan, cross-platform, yönetilen kodla yazılmış ve uygulamayla **aynı süreçte** çalışan web sunucusudur (HTTP/1.1, HTTP/2, HTTP/3). Tek başına internete açılabilir (edge server), ama genelde önünde nginx / YARP / IIS / bulut yük dengeleyici durur: **TLS sonlandırma**, yük dengeleme, bir porta birden çok uygulama, WAF/rate limit, sıkıştırma ve statik içerik gibi işleri proxy üstlenir. "Kestrel yavaştır, proxy hızlandırır" yanlıştır — Kestrel çok hızlıdır; proxy hız için değil operasyonel/güvenlik katmanı içindir. Proxy arkasında gerçek istemci IP'si ve şema için `X-Forwarded-*` + `UseForwardedHeaders` gerekir (→ M3).

### 6. Kestrel ile IIS'in ilişkisi nedir? In-process ile out-of-process farkı?
Windows'ta IIS bir reverse proxy/yönetim katmanı olabilir. **In-process:** uygulama IIS worker process'i (`w3wp.exe`) içinde çalışır ve Kestrel yerine **IIS HTTP Server** kullanılır; ekstra ağ atlaması yoktur, daha hızlıdır ve IIS'te varsayılandır. **Out-of-process:** IIS isteği ayrı bir `dotnet` sürecindeki Kestrel'e proxy'ler. Linux/container'da IIS yoktur: Kestrel (+ gerekirse nginx/ingress) kullanılır.

### 7. ⭐ `app.Run()` ile `app.Start()` farkı nedir?
`Run()` host'u başlatır ve **kapanana kadar (Ctrl+C / SIGTERM) bloklar**; `Start()` + `WaitForShutdown()`'a eşdeğerdir. `Start()` host'u başlatıp **hemen döner**; arkasından bloklayan bir şey yoksa `Main` biter ve süreç kapanır. `Start()`/`StartAsync()` daha çok başlangıç sonrası ek iş gerektiğinde ya da testlerde host'u kontrollü açıp kapatmak için kullanılır; async karşılıkları `RunAsync()` / `StartAsync()`.

### 8. ⭐ `launchSettings.json` nedir? Production'a gider mi?
`Properties/launchSettings.json`, yalnızca geliştirme makinesinde `dotnet run` ve Visual Studio/Rider tarafından okunan profil dosyasıdır: `applicationUrl`, `environmentVariables` (tipik olarak `ASPNETCORE_ENVIRONMENT=Development`), `launchBrowser`. `dotnet publish` çıktısına **girmez**, production'da etkisizdir; production ortamını/URL'sini sunucu, container veya App Service ayarları belirler. `dotnet run` varsayılan olarak ilk `Project` profilini kullanır; `--launch-profile` ile seçilir, `--no-launch-profile` ile yok sayılır. Dosya yoksa ortam varsayılan olarak **Production**'dır.

### 9. ⭐ Uygulama hangi port'ta dinleyeceğini nereden bilir? Nasıl değiştirilir?
Kaynaklar: koddaki `builder.WebHost.UseUrls(...)`, `ASPNETCORE_URLS` environment variable'ı, `--urls` komut satırı argümanı, `launchSettings.json` içindeki `applicationUrl` (yalnızca dev), Kestrel configuration (`Kestrel:Endpoints`, `ConfigureKestrel` + `Listen*`). Hiçbiri yoksa Kestrel `http://localhost:5000`'de dinler. .NET 8+ container imajlarında varsayılan port 8080'dir (`ASPNETCORE_HTTP_PORTS`). Dikkat: `localhost` yalnızca loopback'e bağlanır — container/sunucuda dışarıdan erişim için `http://0.0.0.0:8080` / `http://*:8080` gerekir. Pratik not: kodda `UseUrls` yazarsanız env var ve `--urls` ezilebilir (bu repodaki demoda böyle gözlendi); port'u dışarıdan değiştirmek istiyorsanız kodda sabitlemeyin.

### 10. ⭐ HTTPS geliştirme sertifikası nedir? `dotnet dev-certs https` ne yapar?
SDK'nın `localhost` için ürettiği **kendinden imzalı** sertifikadır; Kestrel HTTPS endpoint'i ve tarayıcı güveni için gerekir. `dotnet dev-certs https --check` var/geçerli mi diye bakar, `--trust` sertifikayı güvenilir kök deposuna ekler (Windows/macOS; Linux'ta tek ortak store olmadığı için manuel güvenme gerekir), `--clean` tüm geliştirme sertifikalarını siler, `-ep`/`-p` ile dışa aktarılır (container için). Yalnızca geliştirme içindir; production'da gerçek sertifika (Let's Encrypt, Key Vault, managed certificate) ya da reverse proxy'de TLS sonlandırma kullanılır. Sertifika hatasını `ServerCertificateCustomValidationCallback = true` ile susturmak yerine sertifikayı güvenilir yapın.

### 11. `Startup.cs` ne oldu? Minimal hosting neyi değiştirdi?
.NET Core 1–5'te `Program.cs` host'u kurar, `Startup.ConfigureServices` DI'ı, `Startup.Configure` pipeline'ı tanımlardı (3.0'dan itibaren Generic Host üzerinde). **.NET 6** ile minimal hosting geldi: `WebApplication.CreateBuilder` + top-level statements; `ConfigureServices` → `builder.Services`, `Configure` → `app.Use…`. Mantık aynı, yalnızca iki sınıf tek dosyaya indi. `Startup` sınıfı hâlâ desteklenir; eski projelerde göreceksiniz. Aralarında fark: minimal hosting'de `UseRouting`/`UseEndpoints` otomatik eklenir.

### 12. `UseRouting` çağırmazsam routing çalışır mı? Neden elle çağırılır?
`WebApplication` kullanıyorsanız çalışır: `UseRouting` pipeline'ın **başına**, `UseEndpoints` sonuna otomatik eklenir; yani tüm `app.Use…` middleware'leri routing'den sonra çalışır ve `GetEndpoint()` onlarda dolu gelir. Elle `UseRouting()` yazarsanız onu kendi seçtiğiniz noktaya koyarsınız; öncesindeki middleware'ler endpoint'i görmez (`GetEndpoint()` = `null`), sonrakiler görür. Elle çağırmak, sıranın önem taşıdığı durumlarda (örn. `UseRouting` → `UseAuthentication` → `UseAuthorization` → `UseCors` düzeni) ya da bu konudaki demo gibi pipeline'ı gözlemlemek için gereklidir (→ J6.4).

### 13. 404 ile 405 arasındaki fark routing açısından nedir?
Hiçbir endpoint'in path'i eşleşmezse routing endpoint seçmez ve pipeline sonunda **404** döner (middleware'ler yine de çalışır). Path eşleşir ama HTTP metodu eşleşmezse (`POST /pipeline` ama yalnızca `MapGet` var) endpoint-routing özel bir "metod uyuşmazlığı" endpoint'i seçer ve **405 Method Not Allowed** (`Allow` header'ıyla) döner. Bu demoda ikisi de curl ile görülebilir.

### 14. `builder.Services`, `builder.Configuration` ve `builder.Logging` ne işe yarar? `app.Services` ile fark?
`builder.Services`: DI kayıtları (`AddSingleton/Scoped/Transient`, `AddControllers`, `AddDbContext`…) — `Build()`'den sonra kilitlenir. `builder.Configuration`: `IConfiguration` kaynakları (JSON dosyaları, env var'lar, komut satırı, in-memory); kaynak eklemek için `AddJsonFile`, anahtar okumak için indexer/`GetValue`/`GetSection`. `builder.Logging`: provider'lar ve filtreler (`AddFilter`, `SetMinimumLevel`, `AddJsonConsole`). `app.Services` ise `Build()` sonrası çalışan **kök** `IServiceProvider`'dır; scoped servisi doğrudan ondan çözmek hatalıdır, `CreateScope()` gerekir (→ J6.2).

### 15. Host nedir; ASP.NET Core uygulaması neden "bir console uygulaması"dır?
Host, uygulamanın kaynaklarını (DI, configuration, logging) ve yaşam döngüsünü (başlat, `IHostedService`'leri çalıştır, graceful shutdown) tek nesnede toplar. ASP.NET Core uygulaması gerçekte bir `Main` metodu olan çalıştırılabilir bir programdır; `Main` bir host kurar, host içinde Kestrel dinler. Aynı Generic Host, web olmayan worker service'lerde de (`Host.CreateApplicationBuilder`) kullanılır; web hali bunun üstüne Kestrel + pipeline ekler.

### 16. Dev'de çalışan uygulama container'da/production'da "cevap vermiyor". Nereye bakarsınız?
1. **Başlangıç log'u:** `Now listening on:` (hangi adres/port?), `Hosting environment:`, `Content root path:`. 2. **Bind adresi:** `localhost` yalnızca loopback → container'da `0.0.0.0:8080` / `ASPNETCORE_HTTP_PORTS`. 3. **Port eşleme / firewall / ingress.** 4. **Ortam farkı:** `launchSettings.json` production'a gitmez → `ASPNETCORE_ENVIRONMENT`, secret'lar, bağlantı dizeleri env var ile verilmeli. 5. **HTTPS:** dev sertifikası production'da yok; TLS proxy'de mi sonlanıyor? 6. `address already in use` varsa port çakışması. Sıra: log → adres → ortam → ağ.

### 17. `HttpContext.Items` nedir, ne zaman kullanılır?
Request ömürlü, o isteğe özel anahtar-değer sözlüğüdür; aynı istek içinde middleware'ler ve endpoint arasında veri taşımak için kullanılır (bu konudaki demo aşama listesini burada taşır). Request bitince atılır, thread-safe değildir. İstekler arası veri için değildir (→ cache/session); servislere veri geçirmek için de çoğu zaman scoped bir servis (DI) daha temiz bir çözümdür.

### 18. Graceful shutdown nasıl çalışır?
`app.Run()` Ctrl+C (SIGINT) ve SIGTERM'i yakalar: yeni bağlantı kabulünü durdurur, işlenmekte olan isteklerin bitmesini `HostOptions.ShutdownTimeout` süresince (varsayılan 30 sn) bekler, `IHostedService.StopAsync`'leri çalıştırır, sonra çıkar. Kubernetes'te pod'a SIGTERM gelir; `terminationGracePeriodSeconds` bu süreyle uyumlu olmalıdır. `app.Lifetime.StopApplication()` ile kod içinden de tetiklenebilir.
