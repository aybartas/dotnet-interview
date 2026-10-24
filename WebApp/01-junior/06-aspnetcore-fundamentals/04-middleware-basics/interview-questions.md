# J6.4 Middleware Pipeline Temelleri — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. ⭐ Middleware nedir? ASP.NET Core pipeline'ı nasıl çalışır?
Middleware, `HttpContext` alan ve zincirde bir sonrakini (`next`) çağırabilen küçük bir bileşendir. Pipeline, **iç içe halkalar** gibi düşünülür: istek dıştan içe, `next()` çağrıları boyunca ilerler; endpoint cevabı üretir; cevap içten dışa, aynı halkalardan **ters sırayla** geri döner. Her middleware `next()`'ten önce request tarafında, sonra response tarafında kod çalıştırabilir. Pipeline `Program.cs`'te `Use...` çağrılarının sırasıyla kurulur; Kestrel isteği alır ve zincirin başına verir (→ J6.1).

### 2. ⭐ Middleware sıralaması neden önemli? Örnekler verin.
Sıra = davranış, çünkü her halka yalnızca kendi **içindekileri** görür/etkiler. Örnekler: (1) `UseExceptionHandler` en başta olmalı; sonra gelenlerin hatalarını yakalar, sonda durursa öncekilerin hatalarını yakalayamaz. (2) `UseAuthentication` → `UseAuthorization`; yetkilendirme `HttpContext.User`'a bakar, kimlik henüz çözülmediyse herkes anonim görünür. (3) `UseCors`, auth'tan önce olmalı; preflight `OPTIONS` isteği kimlik taşımaz. (4) `UseStaticFiles` routing/auth'tan önce, ucuz ve public dosyalar için short-circuit eder. (5) Timing middleware'i yavaş bir middleware'in *dışında* değilse onun süresini ölçmez. Bu repodaki deneyde aynı yedi middleware iki sırayla kuruldu: biri 150+ ms, diğeri ~3 ms ölçtü; birinde exception yakalandı, diğerinde boş 500 döndü.

### 3. ⭐ `Use`, `Run` ve `Map` arasındaki fark nedir?
`Use`: `next` alan, zincire halka ekleyen middleware; `next()` çağırırsa zincir devam eder. `Run`: **terminal** middleware, `next`'i yoktur, zinciri sonlandırır; arkasına yazılan hiçbir şey çalışmaz. `Map`: path prefix'ine göre **dal** açar (prefix `Path`'ten `PathBase`'e taşınır), dal ana pipeline'a dönmez. Ek olarak `MapWhen` (herhangi bir koşulla dal, dönmez) ve `UseWhen` (koşullu dal, dal `next()` çağırırsa ana zincire **geri döner**).

### 4. ⭐ Bir middleware `next()` çağırmazsa ne olur?
Zincir orada kesilir (**short-circuit**): içteki middleware'ler ve endpoint isteği hiç görmez. Middleware cevabı kendisi yazmalıdır, yoksa boş `200` döner. Dıştaki halkalar yine cevabı görür (timing header'ı eklenir, log yazılır). Bilinçli kullanımlar: auth reddi (401/403), CORS preflight, HTTPS redirect, static file, health check. Bilinçsiz `next()` unutmak ise klasik bir bug'dır; `await next()`'i `await`'siz bırakmak da benzer sorun yaratır (cevap tamamlanmadan dönülür).

### 5. ⭐ Tipik bir uygulamada built-in middleware'lerin sırası nedir?
`UseExceptionHandler` (Development'ta developer exception page) → `UseHsts` → `UseHttpsRedirection` → `UseStaticFiles` → `UseRouting` → `UseCors` → `UseAuthentication` → `UseAuthorization` → (custom middleware'ler) → endpoint'ler (`MapControllers`/`MapGet`). Ayrıca rate limiter, response compression/caching, session, request localization gibi middleware'lerin kendi konumları vardır; resmî "Middleware order" sayfasına bakılır. Minimal hosting'de `WebApplication` `UseRouting`'i ve endpoint çalıştırmayı kendisi ekler; `UseRouting()` yalnızca middleware'leri routing'e göre konumlandırmak için elle çağrılır.

### 6. `UseAuthentication` ile `UseAuthorization` farkı nedir; yer değiştirirse ne olur?
Authentication "**kimsin?**" sorusunu cevaplar (token/cookie'yi doğrular, `HttpContext.User`'ı doldurur); authorization "**bunu yapabilir misin?**" (policy/role/claim kontrolü, `[Authorize]` metadata'sı). Authorization `User`'a bakar; sıra tersine çevrilirse kimlik henüz çözülmemiş olur, `[Authorize]` olan tüm istekler 401/403 alır. Authorization ayrıca seçilmiş endpoint'in metadata'sını okuduğu için routing'ten sonra gelmelidir.

### 7. ⭐ Custom middleware nasıl yazılır? Üç yolu anlatın.
(1) **Inline:** `app.Use(async (ctx, next) => { …; await next(ctx); … })`: küçük işler için. (2) **Conventional class:** ctor'u `RequestDelegate next` (+ servisler) alır, `Task InvokeAsync(HttpContext ctx)` metodu vardır; `app.UseMiddleware<T>()` ile eklenir; **uygulama başına tek instance**. (3) **`IMiddleware`:** `InvokeAsync(HttpContext, RequestDelegate)` arayüzü; `IMiddlewareFactory` onu her istekte DI'dan çözer, bu yüzden servis olarak **kaydedilmelidir** (`AddScoped/AddTransient`) ve ctor'una scoped servis alabilir. Bu repoda iki conventional middleware yazıldı: `RequestTimingMiddleware` ve `CorrelationIdMiddleware`.

### 8. Conventional middleware ile `IMiddleware` arasındaki DI farkı nedir? Scoped servisi nasıl kullanırım?
Conventional middleware pipeline kurulurken **bir kez** oluşturulur (singleton gibi); ctor'a scoped servis almak captive dependency hatasıdır (`ValidateScopes` Development'ta yakalar, → J6.2). Scoped servisler conventional middleware'de `InvokeAsync(HttpContext ctx, AppDbContext db)` gibi **metot parametresi** olarak alınır (her istekte çözülür). `IMiddleware` ise factory ile istek başına yaratıldığı için ctor'a scoped servis girebilir, bedeli her istekte DI çözümlemesidir. Her iki durumda da per-request state'i middleware field'ında değil, yerel değişkende veya `HttpContext.Items`'ta tutmak gerekir.

### 9. ⭐ Timing middleware'inde süreyi neden `Response.OnStarting` içinde header'a yazıyoruz? `next()` sonrasında yazsak ne olur?
`next()` döndüğünde endpoint çoğunlukla body'yi yazmıştır; **status line ve header'lar gönderilmeye başlamıştır** (`Response.HasStarted == true`). Bu noktada header değiştirmek `InvalidOperationException: Headers are read-only, response has already started` fırlatır ya da etkisiz kalır. `Response.OnStarting(callback)` framework'ün header'lar gönderilmeden hemen önce çağırdığı kancadır; header'ı oraya yazmak güvenlidir. Bu repoda `/pitfall/naive` (header yok, log'da hata) ve `/pitfall/ok` (header var) uçları farkı gösterir. Not: ölçülen süre time-to-first-byte'tır; gövde stream süresi `OnCompleted`/log ile ölçülür.

### 10. ⭐ Exception handler middleware'i en sona koyarsak ne olur?
Sadece kendisinden **sonra** (içteki) halkalardaki hataları yakalar; öndeki middleware'lerin exception'ları yakalanmaz. Bu repodaki deneyde `Faulty` middleware'i handler'dan önce durduğunda sonuç **boş 500** ve Kestrel'in "An unhandled exception was thrown by the application" log'u; handler başta olduğunda JSON 500, correlation id ve timing header'ları vardı. Ek olarak, cevap zaten başladıysa (`HasStarted`) handler yeni bir cevap yazamaz — bu yüzden de header'lar için `OnStarting` önemlidir.

### 11. Correlation ID nedir? Middleware olarak nasıl uygulanır?
Bir isteğin servisler ve log satırları boyunca izlenmesini sağlayan tek bir tanımlayıcıdır. Middleware: gelen `X-Correlation-Id`'yi oku (doğrula) ya da üret; `HttpContext.Items`'a koy; cevap header'ına yaz; `logger.BeginScope("CorrelationId={CorrelationId}", id)` ile **log scope** aç ki iç halkalardaki tüm log'lar id'yi taşısın; giden HTTP çağrılarına da aynı header'ı ekle. Sıra önemli: loglama middleware'i correlation middleware'inden **önce** durursa log satırları scope'un dışında kalır ve id taşımaz. Standart hali W3C Trace Context / OpenTelemetry'dir (→ M13.4).

### 12. ⭐ Request body'yi middleware'de okursam endpoint neden boş body görür? Nasıl çözülür?
`Request.Body` varsayılan olarak ileri-yönlü, tek okumalık bir akıştır; middleware okuyunca konum sona gider. Çözüm: okumadan önce `ctx.Request.EnableBuffering()`, sonra okuyup `ctx.Request.Body.Position = 0;` ile başa sarmak (örnek: J1.3'teki `RawRequestLoggingMiddleware`). Bedeli: body bellekte (büyükse geçici dosyada) tamponlanır; büyük upload'larda pahalıdır, sadece gerektiğinde ve boyut sınırıyla kullanılmalıdır. Auth token/parola içeren body'leri log'lamamak da ayrı bir güvenlik gereğidir.

### 13. `app.Map` dalı ile ana pipeline arasındaki ilişki nedir? Dal, öncesinde kayıtlı middleware'leri görür mü?
Evet: dal, `Map` çağrıldığı noktadaki pipeline'dan kesilir, yani `Map`'ten **önce** kayıtlı middleware'ler dal için de çalışır (dala girmeden önce istek onlardan geçer). `Map` dalı ana pipeline'a dönmez; prefix `Path`'ten `PathBase`'e taşınır (`/map-demo/a/b` → PathBase `/map-demo`, Path `/a/b`). Dönmesini istiyorsan `UseWhen` kullanılır. Bu projede deney dalları, ana pipeline'ın timing/correlation middleware'lerini miras almasın diye onlardan önce kaydedilir.

### 14. Bir middleware cevabın body'sini nasıl değiştirebilir? Riskleri nelerdir?
Cevap başladıktan sonra değiştirilemez; bu yüzden middleware `Response.Body`'yi bir `MemoryStream` ile değiştirir, `next()`'i çağırır, tamponu işler ve gerçek stream'e yazar (bu repoda `TraceReportMiddleware` böyle çalışır). Riskleri: bellek kullanımı, streaming/SSE'nin bozulması, `Content-Length` yönetimi, sıkıştırma middleware'leriyle sıralama etkileşimi. Çoğu durumda endpoint/result filter ya da `OnStarting` yeterlidir; tamponlama son çaredir.

### 15. Middleware ile Filter (MVC/endpoint filter) arasındaki fark nedir?
Middleware, **tüm** istekler için, routing'ten bağımsız ve framework'ten bağımsız (`HttpContext` seviyesinde) çalışır; hangi endpoint/action'ın seçildiğini bilmeyebilir. Filter'lar ise MVC/endpoint hattının **içinde**, seçilmiş action ve model binding sonuçlarıyla çalışır (authorization/action/result/exception filter'lar). Kural: tüm trafiği ilgilendiren (correlation id, güvenlik başlıkları, hata yakalama) → middleware; belirli action'lara özgü (validation, response sarmalama) → filter (→ M3.5).

### 16. `Run` ile terminal middleware yazıp arkasına başka middleware eklersem ne olur?
Eklenen middleware'ler **hiçbir zaman çalışmaz**; `Run`'ın `next` parametresi yoktur. Derleyici uyarmaz. Aynı durum, `next()` çağırmayan bir `Use` middleware'inin arkası için de geçerlidir. Bu yüzden `Run`/terminal middleware'ler dalların veya pipeline'ın sonunda olmalıdır.

### 17. Bir middleware'i nasıl test edersiniz?
Birim düzeyinde `DefaultHttpContext` oluşturup `next` yerine kendi `RequestDelegate`'inizi (örn. bir bayrağı set eden lambda) verip `InvokeAsync`'i çağırırsınız; `OnStarting` callback'leri `DefaultHttpContext` ile genellikle kendiliğinden tetiklenmez, bu yüzden header testinde gerçek host gerekir. Entegrasyon düzeyinde `WebApplicationFactory` (veya `TestServer`) ile tüm pipeline sırasıyla test edilir; sıra hataları ancak böyle yakalanır (→ M3.4, J10).
