# J6.6 Ortamlar & Hata Sayfaları — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. ⭐ ASP.NET Core'da "environment" nedir, nasıl belirlenir? Varsayılanı nedir?
Environment, uygulamanın nerede çalıştığını söyleyen isimdir (`Development`, `Staging`, `Production` ya da özel isimler). `WebApplication.CreateBuilder` ile öncelik sırası: **komut satırı (`--environment`) > `DOTNET_ENVIRONMENT` > `ASPNETCORE_ENVIRONMENT` > varsayılan `Production`**. Hiçbiri set edilmemişse uygulama `Production` olarak açılır; güvenli taraf budur. Visual Studio/`dotnet run` ise `launchSettings.json` profilinden `ASPNETCORE_ENVIRONMENT=Development` verir; `dotnet publish` çıktısında bu dosya yoktur.

### 2. ⭐ `DOTNET_ENVIRONMENT` ve `ASPNETCORE_ENVIRONMENT` ikisi birden set edilirse hangisi kazanır?
`WebApplication.CreateBuilder` (minimal hosting) kullanıyorsanız **`DOTNET_ENVIRONMENT`** kazanır; eski `WebHost` builder'larında `ASPNETCORE_ENVIRONMENT` kazanır. Dokümanda bununla çelişen genel bir cümle de bulunur, bu yüzden pratikte tuzaktır: eski bir `DOTNET_ENVIRONMENT` kalıntısı yüzünden beklenmedik ortamla açılan uygulama klasik bir sorundur. Çözüm: yalnızca birini kullanmak ve açılıştaki `Hosting environment: …` log satırını ya da `/env` benzeri bir endpoint'i kontrol etmek.

### 3. ⭐ Development ve Production'da hata yönetimi neden farklı olmalı?
Development'ta ayrıntı (stack trace, satır numarası, header'lar, route bilgisi) geliştiriciyi hızlandırır. Production'da aynı ayrıntı **bilgi sızıntısıdır (information disclosure)**: dosya yolları, kütüphane sürümleri, SQL/DB sunucu adı, çoğu zaman connection string ya da parola exception mesajına girer ve saldırgana saldırı haritası verir. Bu yüzden Production'da istemciye generic bir yanıt (`ProblemDetails`: `title`, `status`, `traceId`), ayrıntı ise **sunucu log'una** gider.

### 4. ⭐ `UseDeveloperExceptionPage` ile `UseExceptionHandler` farkı nedir? Hangisi nerede kullanılır?
`UseDeveloperExceptionPage` exception'ı **ayrıntılı** gösterir (HTML ya da `Accept`'e göre text/JSON), yalnızca Development için. `UseExceptionHandler("/error")` exception'ı yakalar, **log'lar**, response'u sıfırlar ve isteği `/error` path'iyle pipeline'da **yeniden çalıştırır**; Production/Staging içindir. `WebApplication.CreateBuilder` ile kurulan uygulamada ortam Development ise developer exception page pipeline'ın başına **otomatik eklenir**; elle çağırmak gerekmez. Tipik kod: `if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/error");`.

### 5. `UseExceptionHandler` pipeline'ın neresine konur? Neden?
En başa (ya da başa yakın). Exception, middleware zincirinde yukarı (dışa) doğru kabarır; handler'ın yakalayabilmesi için exception'ı fırlatan middleware'lerin **dışında** olması gerekir. Sonra konulan middleware'lerdeki hatalar yakalanmaz. Genel kural: middleware sırası davranışı belirler (→ J6.4).

### 6. ⭐ `ProblemDetails` nedir? Hangi alanları var?
HTTP API hataları için standart gövde biçimi: **RFC 9457** (RFC 7807'nin yerine geçti), içerik tipi `application/problem+json`. Alanlar: `type` (hata türü URI'si), `title` (kısa, sabit başlık), `status`, `detail` (bu olaya özel açıklama), `instance` ve serbest `extensions` (`traceId`, `errors` …). `builder.Services.AddProblemDetails()` ile `UseExceptionHandler`, `UseStatusCodePages` ve developer exception page bu biçimi yazar; `Results.Problem(...)` ile elle de üretilir. Production'da `detail`'a iç bilgi koymayın. Ayrıntı: → J7.3.

### 7. Hata yanıtındaki `traceId` ne işe yarar?
Kullanıcıya iç ayrıntıyı vermeden, olayı log'da bulabilmeniz için **korelasyon kimliğidir**. Kullanıcı/istemci destek talebine id'yi yazar; ekip log/APM aracında (Application Insights, Seq, ELK) bu id ile ilgili isteğin tüm log'larını ve exception'ı bulur. ASP.NET Core `traceId`'yi `Activity.Current.Id` (W3C `00-<trace-id>-<span-id>-00`) ya da `HttpContext.TraceIdentifier` olarak verir.

### 8. `UseStatusCodePages` ne yapar? Exception handler'dan farkı?
Exception olmadan dönen **boş** 4xx/5xx yanıtlara (ör. bilinmeyen path için 404) gövde ekler. `AddProblemDetails()` kayıtlıysa gövde `ProblemDetails` JSON'dur, değilse düz metindir (`Status Code: 404; Not Found`). `UseExceptionHandler` ise *fırlatılan exception*'ları yakalar. `UseStatusCodePagesWithReExecute` URL'yi değiştirmeden başka bir endpoint'e düşer; `...WithRedirects` 302 yapar ve durum kodunu 200'e çevirdiği için API'lerde kullanılmaz.

### 9. ⭐ Staging ortamı ne içindir?
Production'ın **prova** kopyasıdır: aynı yapı ve aynı ayarlar, ama ayrı kaynaklar (DB, queue, domain). Amaç, canlıya çıkmadan deployment'ı, migration'ları ve config'i prod koşullarında denemek (smoke test, UAT). Bu yüzden Staging'in hata davranışı Production gibi olmalı; orada developer exception page açmak provanın anlamını ve güvenliği bozar. Azure App Service'te deployment slot / blue-green stratejisiyle de kullanılır.

### 10. `IHostEnvironment` / `IWebHostEnvironment` neler sunar?
`EnvironmentName`, `ApplicationName` (entry assembly adı), `ContentRootPath` (config dosyalarının, view'ların arandığı kök; `dotnet run`'da proje klasörü) ve `ContentRootFileProvider`. `IWebHostEnvironment` ek olarak `WebRootPath` (`wwwroot`, yoksa `null`) ve `WebRootFileProvider` ekler. `IsDevelopment()`, `IsStaging()`, `IsProduction()`, `IsEnvironment("X")` bunların extension method'larıdır ve büyük/küçük harf duyarsız karşılaştırır.

### 11. ⭐ Ortama göre kodda dallanmak ne zaman doğru, ne zaman yanlış?
Doğru: altyapı/diagnostik farkları: hata sayfası, Swagger/OpenAPI UI'ı açmak, seed data, HSTS/HTTPS yönlendirme ayarları. Yanlış: **iş mantığı** (`IsDevelopment() ? 0 : CalculateDiscount()`), çünkü Dev'de test ettiğiniz kod Prod'da çalışan kod değildir. Ve `Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production"` gibi serpilmiş string karşılaştırmaları (yazım hatası, harf boyutu, tek bir değişiklik noktası yok). Farkı kod dalı değil **configuration/Options** ve DI'da farklı implementasyon kaydı taşısın (→ J6.2, J6.3).

### 12. Ortam, configuration ile nasıl ilişkilidir?
Varsayılan builder `appsettings.json`'dan sonra **`appsettings.{EnvironmentName}.json`** dosyasını yükler ve onun değerleri öncekini ezer; Development'ta ayrıca User Secrets eklenir; üstüne environment variables ve komut satırı gelir (→ J6.3). Linux'ta dosya adı harf duyarlıdır (`appsettings.Development.json`). Ortam adı değişince hem hata sayfası davranışı hem de yüklenen config seti değişir.

### 13. Development'ta DI için ne farklıdır?
Dokümante edilmiş varsayılan: Development ortamında default service provider `ValidateScopes` ve `ValidateOnBuild` ile oluşturulur. Yani "scoped servisi singleton'a enjekte etme" (captive dependency) ve eksik kayıtlar uygulama açılırken ya da çözülürken hata verir. Production'da bu kontroller varsayılan olarak kapalıdır, bu yüzden bu hatalar yalnızca Development'ta görünüp Prod'da sessiz kalabilir (→ J6.2).

### 14. Production'da bir 500 hatası aldık, kullanıcıya hiçbir ayrıntı gitmiyor. Nasıl teşhis edersiniz?
Kullanıcıdan/istemciden yanıttaki `traceId`'yi alırım, log/APM aracında bu id ile arama yaparım: exception tipi, mesajı ve stack trace `ExceptionHandlerMiddleware` tarafından log'lanmıştır. Yeniden üretmek için aynı isteği **Staging**'de (prod-benzeri ayarlarla) denerim; gerekirse log seviyesini geçici düşürürüm. Production'da `Development` ortamını açmak çözüm değildir (güvenlik riski).

### 15. Hata yanıtı başladıktan (header gönderildikten) sonra exception olursa ne olur?
`UseExceptionHandler` response'u sıfırlayıp yeni bir yanıt yazabilir ama yalnızca **henüz header'lar gönderilmediyse**. Gövde yazılmaya başlamışsa durum kodu artık değiştirilemez; handler "response has already started" uyarısını log.lar ve exception.ı yeniden fırlatır; sunucu bağlantıyı keser (istemci yarım bir yanıt görür). Bu yüzden büyük yanıtları stream ederken hata yönetimi (önceden doğrulama, buffer) önemlidir.

### 16. Her action'a `try/catch` koymak yerine ne yaparsınız?
Merkezi hata yönetimi: `UseExceptionHandler` (ya da .NET 8+ `IExceptionHandler` implementasyonları) + `AddProblemDetails`. İş kuralı hataları için özel exception tipleri ya da sonuç nesneleri (→ J7.3, M2), `try/catch`'i yalnızca gerçekten kurtarabileceğiniz (retry, fallback) yerde kullanırım. Beklenen durumlar (kayıt yok, validation) için exception fırlatmak hem yavaş hem gürültülüdür; `Results.NotFound()` / `Results.ValidationProblem()` tercih edilir.

### 17. Hata mesajı ve log'larda hassas veri sorunu: ne yaparsınız?
İstemciye: generic `title` + `traceId`. Exception mesajlarına parola/connection string/token koymam; kütüphane (örn. DB sürücüsü) bunu kendisi mesaja koyuyorsa log'a yazmadan önce maskelerim (log enricher/redaction). Log'lara rol bazlı erişim, saklama süresi ve şifreleme uygularım. Sır kaynağı olarak `appsettings` yerine ortam değişkeni/Key Vault/User Secrets kullanırım (→ J6.3, J11).

### 18. "Request geldiğinde ASP.NET Core'da ne olur?" sorusunda ortam ve hata yönetimi nerede devreye girer? *(modül sorusu)*
Kestrel isteği alır, `HttpContext` oluşturur ve middleware pipeline'a verir. Pipeline'ın **en başında** hata middleware'i bulunur: Development'ta developer exception page (otomatik eklenir), diğer ortamlarda `UseExceptionHandler`. Sonra routing, auth, endpoint gelir. Alttan bir exception fırlarsa kabarıp hata middleware'ine ulaşır ve ortama göre ayrıntılı ya da generic yanıt yazılır. Bu yüzden hata middleware'inin sırası kritiktir (→ J6.4).

### 19. "Middleware sıralaması neden önemli?" sorusuna bu topic'ten örnek verin. *(modül sorusu)*
`UseExceptionHandler`'ı pipeline'ın sonuna koyarsanız, kendisinden önceki middleware'lerde ya da endpoint'te oluşan exception'ları yakalayamaz. Aynı şekilde `UseStatusCodePages` response'u üreten middleware'lerin **dışında** olmalıdır ki boş 404/401'leri görüp gövde ekleyebilsin. Doğru sıra: hata middleware'i → (HSTS/HTTPS) → static files → routing → auth → endpoint (→ J6.4).

### 20. Kodda `app.Environment` ile `builder.Environment` aynı mı? Ne zaman hangisi?
Aynı ortam bilgisini taşırlar (`IWebHostEnvironment`). `builder.Environment` **servisleri kaydederken** (build öncesi: ör. "Development'ta fake servis, Production'da gerçek servis kaydı") ve config eklerken; `app.Environment` **pipeline kurarken** (build sonrası) kullanılır. Ortamı build'den sonra değiştiremezsiniz; host kurulurken (komut satırı/env var/`WebApplicationOptions.EnvironmentName`) sabitlenir.
