# J6.2 Dependency Injection Temelleri — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. ⭐ Dependency Injection nedir? IoC ve Dependency Inversion Principle ile ilişkisi nedir?
**DI:** bir sınıfın ihtiyaç duyduğu bağımlılıkları kendisi `new`'lemek yerine dışarıdan (genelde constructor'dan) almasıdır; bağımlılıkları kuran ve ömrünü yöneten parça **DI container**'dır. **IoC** genel prensiptir: nesne oluşturma/akış kontrolü sınıftan çerçeveye devredilir; DI, IoC'nin en yaygın uygulamasıdır. **DIP** (SOLID'in D'si) ise tasarım kuralıdır: üst seviye kod somut sınıfa değil soyutlamaya (arayüze) bağlanır. DI, DIP'i pratikte mümkün kılan mekanizmadır ama ikisi aynı şey değildir: arayüze bağlanıp yine de içeride `new` yapabilirsiniz (DIP var, DI yok).

### 2. ⭐ Neden bağımlılıkları `new` ile oluşturmayız?
(1) **Test edilebilirlik:** gerçek SMTP/DB yerine sahte implementasyon veremeyiz. (2) **Sıkı bağlılık:** implementasyon değişince tüketen her sınıf değişir. (3) **Ömür yönetimi:** kaç tane oluşturulacağı, paylaşılıp paylaşılmayacağı ve kimin `Dispose` edeceği dağınık kalır. (4) **Gizli bağımlılık:** sınıfın neye ihtiyacı olduğu imzadan görünmez; constructor sınıfın bağımlılık sözleşmesidir. Container bu işleri tek bir yerde (composition root, `Program.cs`) toplar. Not: veri taşıyan basit nesneleri (DTO, `List<T>`, value object) `new`'lemek sorun değildir; DI **davranış sunan servisler** içindir.

### 3. ⭐ Transient, Scoped ve Singleton arasındaki fark nedir? ASP.NET Core'da "scope" ne demek?
**Transient:** her çözümlemede yeni instance. **Scoped:** scope başına bir instance; ASP.NET Core'da her HTTP request için bir scope açılır (`HttpContext.RequestServices` onun provider'ıdır) ve request bitince dispose edilir. **Singleton:** uygulama boyunca tek instance (ilk istendiğinde oluşur, host kapanırken dispose edilir). Örnekler: Singleton → cache, konfigürasyon, thread-safe paylaşılan yardımcılar; Scoped → `DbContext`, request'e özel unit of work/kullanıcı bağlamı; Transient → hafif, durumsuz yardımcılar. Repo'daki `/lifetimes` endpoint'i bunu GUID'le gösterir: aynı request'te transient iki kez farklı, scoped ve singleton aynı; ikinci request'te transient ve scoped yeni, singleton aynı Id.

### 4. ⭐ Singleton bir servisin constructor'ına Scoped servis enjekte edersen ne olur? (captive dependency)
Singleton bir kez oluşturulur ve aldığı scoped nesneyi **sonsuza dek** tutar; scoped servis fiilen singleton'a döner (tüm request'ler paylaşır, request sonunda yenilenmez). Gerçek `DbContext` ile: thread-safe olmayan nesne eşzamanlı kullanılır (`A second operation was started on this context`), change tracker şişer, bayat veri okunur. Development'ta `ValidateScopes = true` olduğundan `InvalidOperationException: Cannot consume scoped service 'X' from singleton 'Y'` fırlar, `ValidateOnBuild` ile bu hata uygulama açılırken yakalanır. Doğrusu: singleton'a `IServiceScopeFactory` enjekte edip işlem başına `CreateScope()` ile kısa ömürlü scope açmak, ya da servisin ömrünü düzeltmek.

### 5. ⭐ `ValidateScopes` ve `ValidateOnBuild` ne yapar? Neden Development'ta açık, Production'da kapalı?
`ValidateScopes`: scoped servisin root provider'dan ya da singleton'dan çözülmesini çalışma anında engeller. `ValidateOnBuild`: provider kurulurken **tüm kayıtları** tarayıp çözülemeyen ya da lifetime kuralını ihlal eden kayıtlar için hata verir (uygulama başlamaz; `AggregateException` içinde `InvalidOperationException`'lar). Varsayılan host (`WebApplication.CreateBuilder`) ikisini yalnızca **Development** ortamında açar: validasyonun (kayıtları tarama + çözümleme başına kontrol) küçük bir maliyeti vardır ve varsayım hataların geliştirmede yakalanmasıdır. Dezavantaj: Production ortamıyla koşan CI/test hatayı kaçırır; `builder.Host.UseDefaultServiceProvider(o => { o.ValidateScopes = true; o.ValidateOnBuild = true; })` ile her ortamda açılabilir. Sınır: factory lambda'larının (`sp => ...`) içindeki çözümlemeleri `ValidateOnBuild` analiz edemez.

### 6. ⭐ Hangi lifetime hangisini enjekte edebilir?
Kural: bir servis kendisinden **kısa ömürlü** bir servise bağımlı olmamalıdır. Transient ve Scoped herkesi alabilir (Transient/Scoped/Singleton). Singleton: Singleton ✅, Transient ⚠️ (derlenir ama o transient uygulama boyunca yaşar; `IDisposable` ise dispose edilmez/ geç edilir), **Scoped ❌**. Scoped → Singleton ve Transient → Scoped sorunsuzdur çünkü uzun ömürlü kısa ömürlüyü kullanıyor, tersi değil.

### 7. Aynı arayüz için birden çok implementasyon kaydedilirse `IGreeter` ve `IEnumerable<IGreeter>` ne döner?
`IEnumerable<IGreeter>` **tüm** kayıtları kayıt sırasıyla verir. Tekil `IGreeter` istenirse **son kayıt** ("last one wins") döner; önceki kayıtlar silinmez, sadece listede kalır. Bu davranış sayesinde kütüphane varsayılanı uygulama kodunda ezilebilir (uygulama sonra kaydeder). Birini belirli bir anahtarla seçmek için .NET 8+ keyed services (`AddKeyedSingleton`, `[FromKeyedServices("key")]`) kullanılır.

### 8. `AddSingleton` ile `TryAddSingleton` ve `TryAddEnumerable` arasındaki fark nedir?
`Add*` her zaman yeni bir kayıt ekler (aynı servis tipinde çoğalır). `TryAdd*` yalnızca o **servis tipi** için hiç kayıt yoksa ekler (kütüphane varsayılanlarında: kullanıcı önceden kaydettiyse dokunma). `TryAddEnumerable` (servis tipi **+ implementasyon tipi**) çifti yoksa ekler; çoklu implementasyon listesine tekrarsız ekleme içindir (`AddXyz()` iki kez çağrılsa da çift kayıt olmaz). Mevcut kaydı değiştirmek için `Replace`, hepsini silmek için `RemoveAll`.

### 9. Constructor injection, method ve property injection'dan neden tercih edilir? Container constructor'ı nasıl seçer?
Constructor injection bağımlılıkları **zorunlu ve görünür** kılar (nesne geçersiz durumda oluşamaz), alanlar `readonly`/değişmez olabilir, test'te `new X(fake)` yeter. Property injection bağımlılığı isteğe bağlı gösterir ve nesne yarım kurulu kalabilir; MS.DI zaten desteklemez. Birden çok public constructor varsa container, **tüm parametreleri çözülebilen en çok parametreli** olanı seçer; belirsizlik varsa `InvalidOperationException`. Minimal API'de handler parametreleri ve middleware `InvokeAsync` parametreleri de aynı mekanizmayla enjekte edilir.

### 10. Service locator nedir, neden anti-pattern sayılır?
Sınıfın içinde `IServiceProvider.GetRequiredService<T>()` ile bağımlılığı elle çözmektir. Sakıncaları: bağımlılıklar constructor'dan görünmez, eksik kayıt **çalışma anında** (ilk çağrıda) patlar, test'te tüm provider'ı kurmak gerekir, sınıf container'a bağımlı hale gelir. Meşru kullanımlar: composition root (`Program.cs`), scope açan altyapı kodu (`IServiceScopeFactory` ile `BackgroundService` içinde), factory'ler/eklenti yükleme gibi dinamik çözümleme.

### 11. ⭐ Container `IDisposable` nesneleri ne zaman dispose eder?
Container, **kendi oluşturduğu** `IDisposable`/`IAsyncDisposable` nesnelerin sahibidir. Transient ve Scoped: onları çözen scope dispose edilince (request sonu); Singleton: root provider dispose edilince (host kapanırken). `AddSingleton<T>(instance)` ile **sizin oluşturup verdiğiniz** instance'ı container dispose etmez; factory (`sp => new X()`) ile oluşturulanları eder. Bu yüzden servis içinde container'dan gelen nesneyi `using`/`Dispose` etmeyin. Tuzak: root provider'dan scope'suz çözülen `IDisposable` transient'ler uygulama sonuna kadar listede tutulur (sızıntı).

### 12. Çözülemeyen bağımlılık ve döngüsel bağımlılıkta ne olur?
Kayıt yoksa: `InvalidOperationException: Unable to resolve service for type 'X' while attempting to activate 'Y'` (`ValidateOnBuild` açıksa açılışta, değilse ilk çözümlemede). A → B → A döngüsünde: `InvalidOperationException: A circular dependency was detected for the service of type '...'`. Döngü genelde sorumlulukların yanlış bölündüğünün işaretidir; çözüm üçüncü bir sınıfa çıkarmak, olay/arayüz ile bağı kırmak ya da bağımlılığı metot parametresine çevirmektir (`Lazy<T>`/`IServiceProvider` ile kaçmak son çaredir).

### 13. Singleton servisler neden thread-safe olmak zorundadır?
Tek instance, eşzamanlı gelen tüm request'lerce (farklı thread'lerde) kullanılır. Değişebilir alan/koleksiyon (örn. `List<T>`, `Dictionary`, sayaç) kilitsiz yazılırsa yarış durumu (race condition) oluşur; çözüm: değişmez (immutable) tasarım, `lock`, `Interlocked`, `ConcurrentDictionary`/`Concurrent*`. Singleton'da kullanıcıya/request'e özel durum tutmak hem thread-safety hem de veri sızıntısı sorunudur (bir kullanıcının verisi diğerine görünür).

### 14. `HttpContext.RequestServices` nedir? Middleware'e scoped servis nasıl enjekte edilir?
Request'e özel scope'un `IServiceProvider`'ıdır; burada çözülen scoped servisler request boyunca paylaşılır ve request sonunda dispose edilir. Conventional middleware **singleton gibi** bir kez oluşturulur (constructor'ı uygulama başında çalışır), bu yüzden constructor'a scoped servis alınamaz (captive dependency olurdu); scoped/transient servisler `InvokeAsync(HttpContext context, IScopedService svc)` **parametresi** olarak istenir, çünkü o parametreler her request'te çözülür (→ J6.4).

### 15. `BackgroundService` (singleton) içinde `DbContext` (scoped) nasıl kullanılır?
`BackgroundService` singleton olarak yaşar, bu yüzden `DbContext`'i constructor'a alamayız. Constructor'a `IServiceScopeFactory` alınır; her iterasyonda (mesaj işlerken, timer tikinde) `using var scope = factory.CreateScope(); var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();` ile kısa ömürlü scope açılır; iş bitince scope ve `DbContext` dispose edilir. Alternatif: `IDbContextFactory<AppDbContext>` (→ M6).

### 16. DI container'ı kullanmadan DI yapılabilir mi? ("Pure DI / Poor man's DI")
Evet: nesne grafiğini `Program.cs`'te elle kurmak (`new OrderService(new SmtpEmailSender(...))`) da DI'dır; DI bir **desen**, container bir **araç**tır. Küçük uygulamalarda elle kurmak yeterli ve derleme zamanı güvenli olabilir; uygulama büyüyünce lifetime yönetimi, scope, `IDisposable` takibi, çoklu implementasyon ve çerçeve entegrasyonu (ASP.NET Core, EF Core, `ILogger`) için container pratiktir.
