# J6.2 Dependency Injection Temelleri ⭐

> 📁 `WebApp/01-junior/06-aspnetcore-fundamentals/02-di-basics/` · Önkoşul: J2–J4 · Sorular: [`interview-questions.md`](interview-questions.md)

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

**Dependency Injection (DI)**, bir sınıfın ihtiyaç duyduğu bağımlılıkları (`ILogger`, `DbContext`, `IEmailSender` …) kendisinin **oluşturmak yerine dışarıdan almasıdır** (genelde constructor parametresi olarak). Bağımlılıkları kim oluşturur ve ömrünü kim yönetir? **DI container** (ASP.NET Core'da `Microsoft.Extensions.DependencyInjection`): uygulama başlarken "şu arayüz için şu sınıfı, şu ömürle kullan" kayıtlarını (`IServiceCollection`) alır, `Build()` ile `IServiceProvider` üretir ve nesne grafiğini (A → B → C) ihtiyaç anında kendisi kurar.

**Inversion of Control (IoC)** genel prensiptir: "nesneyi kim oluşturur / akışı kim yönetir" kontrolü sınıftan çerçeveye devredilir (Hollywood prensibi: *"bizi aramayın, biz sizi ararız"*). **DI, IoC'nin en yaygın uygulama biçimidir**; *Dependency Inversion Principle* (SOLID'in "D"si: üst seviye modül somut sınıfa değil soyutlamaya bağlanır) ile birlikte çalışır ama aynı şey değildir. Üçü karıştırılır:

| Kavram | Ne? |
|--------|-----|
| **IoC** | Genel prensip: kontrol akışı/oluşturma sorumluluğu çerçevede |
| **DIP** | Tasarım prensibi: soyutlamaya bağlan (`IEmailSender`), somuta değil (`SmtpEmailSender`) |
| **DI** | Teknik: bağımlılığı dışarıdan ver (constructor/method/property) |
| **DI container** | Aracı: kayıtlardan nesne grafiğini kuran ve ömürlerini yöneten kütüphane |

Konunun kodu [`DiBasicsTopic.cs`](DiBasicsTopic.cs) içindeki üç endpoint'tir: `/lifetimes`, `/greeters`, `/captive`. Servisler [`Services/`](Services/), kasıtlı bozuk örnek [`CaptiveDemo.cs`](CaptiveDemo.cs) dosyasındadır. Derinlemesine (keyed services, decorator, scope yönetimi, container değiştirme, DI'ın iç işleyişi) → M3.

---

## 2. Neden?

`new` ile bağımlılık oluşturmanın bedeli:

```csharp
// ❌ The class decides WHICH implementation it uses and builds it itself
public class OrderService
{
    private readonly SmtpEmailSender _sender = new SmtpEmailSender("smtp.example.com", 25);   // hard-wired

    public void Place(Order order) { /* ... */ _sender.Send(order.CustomerEmail, "Order received"); }
}

// ✅ The class only states WHAT it needs; the container decides the rest
public class OrderService(IEmailSender sender)
{
    public void Place(Order order) { /* ... */ sender.Send(order.CustomerEmail, "Order received"); }
}
```

| Sorun (`new` ile) | DI ile |
|-------------------|--------|
| **Test edilemez:** `OrderService`'i test ederken gerçek SMTP'ye bağlanır | Test'te `IEmailSender` yerine sahte (fake) implementasyon verilir (→ J10) |
| **Sıkı bağlılık:** SMTP'den SendGrid'e geçmek her kullanıcı sınıfı değiştirmeyi gerektirir | Tek yerde (kayıt) değişir |
| **Ömür yönetimi:** kim `Dispose` edecek, kaç tane oluşturulacak, paylaşılacak mı? | Container'a bırakılır (aşağıda: lifetime'lar) |
| **Gizli bağımlılıklar:** sınıfın neye ihtiyacı olduğu imzadan görünmez | Constructor, sınıfın **bağımlılık sözleşmesidir** |
| **Konfigürasyon dağınıklığı:** connection string, host, port her yerde | Bir kez kaydedilir (→ J6.3 Options) |

Ek olarak ASP.NET Core DI'ı **çerçevenin kendisi** kullanır: `ILogger<T>`, `IConfiguration`, `DbContext`, `HttpClient` fabrikaları, middleware'ler, endpoint handler parametreleri hep container'dan gelir. DI bilmeden ASP.NET Core yazılmaz.

---

## 3. Nasıl?

### 3.1 Kayıt, çözümleme, constructor injection

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITransientService, TransientService>();   // interface → implementation
builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddSingleton<ISingletonService, SingletonService>();
builder.Services.AddTransient<LifetimeConsumer>();                       // a concrete type can be registered without an interface

var app = builder.Build();      // IServiceCollection is "frozen" into an IServiceProvider here; registering after Build() is not possible
```

```csharp
// Constructor injection (C# 12 primary constructor): the container resolves the parameters and passes them in
public sealed class LifetimeConsumer(ITransientService transient, IScopedService scoped, ISingletonService singleton)
{
    public ITransientService Transient { get; } = transient;
    // ...
}
```

- Container constructor parametrelerini **recursive** çözer (`A` → `B` → `C` grafiğini kendisi kurar); `new` yazan kimse yok.
- Birden çok constructor varsa, çözülebilen parametrelerin **en çoğuna sahip** olanı seçilir; belirsizlikte `InvalidOperationException` fırlar.
- Çözülemeyen bağımlılık (kayıt unutulmuş) → `InvalidOperationException: Unable to resolve service for type '...'` (ilk kullanımda ya da Development'ta başlangıçta).
- **Döngüsel bağımlılık** (A → B → A) → `InvalidOperationException: A circular dependency was detected`. Çözüm: sorumlulukları yeniden böl, genelde tasarım kokusudur.
- Minimal API handler parametreleri de container'dan gelir: `app.MapGet("/x", (LifetimeConsumer consumer) => ...)`.

### 3.2 🛠 Üç lifetime'ı GUID ile görmek: `/lifetimes` ⭐

| Kayıt | Yeni instance ne zaman? | Ömür |
|-------|-------------------------|------|
| `AddTransient` | **Her çözümlemede** (her enjeksiyonda, her `GetService` çağrısında) | İsteyenle birlikte; dispose'u scope sonunda |
| `AddScoped` | **Scope başına bir** — ASP.NET Core'da scope = **bir HTTP request** | Request boyunca |
| `AddSingleton` | **Uygulama boyunca bir** (ilk istendiğinde oluşur) | Host kapanana kadar |

Üç servis aynıdır; her biri constructor'ında `Guid.NewGuid()` üretir ([`Services/LifetimeServices.cs`](Services/LifetimeServices.cs)). Endpoint, aynı request içinde her servisi **iki kere** çözer: (A) `HttpContext.RequestServices`'ten, (B) constructor injection ile gelen `LifetimeConsumer` üzerinden.

```bash
cd WebApp
dotnet run -- J6.2          # http://localhost:5080  (stop: Ctrl+C)

curl http://localhost:5080/lifetimes     # 1st request
curl http://localhost:5080/lifetimes     # 2nd request
```

Gerçek çıktı (iki ardışık request; GUID'lerin ilk 8 karakteri):

```text
Request #1   (environment: Production)

Lifetime    (A) ctx.RequestServices   (B) via constructor   same instance in one request?
----------  ------------------------  --------------------  ------------------------------
Transient   7b072663                  1dd9ae40              no
Scoped      8b63b3f7                  8b63b3f7              yes
Singleton   88d2c536                  88d2c536              yes

Request #2   (environment: Production)

Lifetime    (A) ctx.RequestServices   (B) via constructor   same instance in one request?
----------  ------------------------  --------------------  ------------------------------
Transient   66ca5abb                  eb4e5402              no
Scoped      fde18770                  fde18770              yes
Singleton   88d2c536                  88d2c536              yes
```

Nasıl okunur:

| Satır | Aynı request içinde (A vs B) | Request #1 → #2 | Neden |
|-------|------------------------------|-----------------|-------|
| **Transient** | **Farklı** (`7b07…` ≠ `1dd9…`) | Farklı | Her çözümleme yeni nesne; hem (A) hem (B) ayrı instance aldı |
| **Scoped** | **Aynı** (`8b63…` = `8b63…`) | **Farklı** (`8b63…` → `fde1…`) | Request başına bir; request içinde herkes paylaşır, yeni request yeni scope |
| **Singleton** | Aynı | **Aynı** (`88d2…` → `88d2…`) | Uygulama boyunca tek nesne |

> Ezber cümlesi: *Transient her çözümlemede değişir, Scoped her request'te değişir, Singleton hiç değişmez.*

Scoped neden "request başına"? ASP.NET Core her request için bir `IServiceScope` oluşturur (`HttpContext.RequestServices` o scope'un provider'ıdır) ve request bitince `Dispose` eder. Scoped servisin tipik örneği **`DbContext`**: bir request içindeki tüm repository'ler aynı context'i (aynı change tracker/transaction) paylaşır (→ J8, M6).

### 3.3 Lifetime kuralları: kim kimi enjekte edebilir?

Kural: **bir servis, kendisinden kısa ömürlü bir servise bağımlı olmamalıdır** (uzun ömürlü, kısa ömürlüyü "esir alır").

| Tüketen ↓ / Bağımlılık → | Transient | Scoped | Singleton |
|--------------------------|:---------:|:------:|:---------:|
| **Transient** | ✅ | ✅ | ✅ |
| **Scoped** | ✅ | ✅ | ✅ |
| **Singleton** | ⚠️ izinli ama transient fiilen singleton olur | ❌ **captive dependency** | ✅ |

- Singleton → Scoped **hatalıdır** (3.5). Singleton → Transient derleyici/validasyon tarafından engellenmez ama o transient bir kez oluşup uygulama boyunca yaşar; `IDisposable` ise dispose'u uygulama sonuna kalır.
- Scoped → Singleton, Transient → Scoped sorunsuzdur (uzun ömürlüyü kısa ömürlü kullanır).
- **Singleton thread-safe olmak zorundadır:** aynı instance eşzamanlı request'lerce kullanılır; değişebilir (mutable) alanlar kilitsiz yazılmamalıdır (→ J4, M4).

Hangisini seçmeli? Durumsuz (stateless) ve ucuz → Transient; request'e özel durum (kullanıcı, `DbContext`, unit of work) → Scoped; pahalı oluşturulan, paylaşılabilir ve thread-safe (cache, konfigürasyon, `HttpClient` fabrikası) → Singleton.

### 3.4 Çoklu implementasyon: `IEnumerable<IGreeter>`, "son kayıt kazanır", `TryAdd*` ([`Services/Greeters.cs`](Services/Greeters.cs))

Aynı arayüz birden çok kez kaydedilebilir; **ne döneceği, nasıl istediğinize bağlıdır**:

| İstek | Sonuç |
|-------|-------|
| `IEnumerable<IGreeter>` (ya da `GetServices<IGreeter>()`) | **Hepsi**, kayıt sırasıyla |
| `IGreeter` (ya da `GetRequiredService<IGreeter>()`) | Yalnızca **son kayıt** ("last one wins") |

```csharp
services.AddSingleton<IGreeter, EnglishGreeter>();
services.AddSingleton<IGreeter, SpanishGreeter>();
services.AddSingleton<IGreeter, FrenchGreeter>();

services.TryAddSingleton<IGreeter, GermanGreeter>();                                  // skipped: IGreeter already has a registration
services.TryAddEnumerable(ServiceDescriptor.Singleton<IGreeter, SpanishGreeter>());   // skipped: this (service, implementation) pair exists
services.TryAddEnumerable(ServiceDescriptor.Singleton<IGreeter, GermanGreeter>());    // added: new pair

public sealed class GreetingService(IEnumerable<IGreeter> greeters) { /* gets ALL four */ }
```

```bash
curl "http://localhost:5080/greeters?name=Ada"
```

```text
Registration log (startup):
  Add x3 (English, Spanish, French)               → 3 IGreeter registrations
  TryAddSingleton<IGreeter, GermanGreeter>        → skipped (3 registrations)
  TryAddEnumerable<IGreeter, SpanishGreeter>      → skipped (that pair already exists) (3 registrations)
  TryAddEnumerable<IGreeter, GermanGreeter>       → added (new pair) (4 registrations)

IEnumerable<IGreeter>  → ALL registrations, in registration order (injected into GreetingService's constructor):
  English  → Hello, Ada!
  Spanish  → Hola, Ada!
  French   → Bonjour, Ada !
  German   → Hallo, Ada!

IGreeter (single)      → the LAST registration wins:
  German   → Hallo, Ada!   (GermanGreeter)
```

| Metot | Ne zaman kullanılır? |
|-------|----------------------|
| `Add*` | Her zaman ekler; aynı servis tipinde ikinci `Add*` öncekini **ezmez**, listeye ekler ve tekil çözümlemede kazanır |
| `TryAdd*` | "Bu **servis tipi** için kayıt yoksa ekle" — kütüphane varsayılanları için: kullanıcının kaydı varsa dokunma |
| `TryAddEnumerable` | "Bu (servis tipi, **implementasyon tipi**) çifti yoksa ekle" — çoklu implementasyon listesine **tekrar etmeden** eklemek için (kütüphane `AddXyz()` metodu iki kez çağrılsa da çift kayıt olmasın) |
| `Replace` / `RemoveAll` | Mevcut kaydı değiştirmek/silmek (örn. testte gerçek servisi sahteyle değiştirme) |

`IEnumerable<T>` desenini ASP.NET Core de kullanır: tüm `IHostedService`, `IValidator<T>`, `IAuthorizationHandler` kayıtları böyle toplanır. "if/switch yerine strateji listesi" için idealdir. (Belirli birini **anahtarla** seçmek için .NET 8+ *keyed services*: `AddKeyedSingleton<IGreeter, EnglishGreeter>("en")` + `[FromKeyedServices("en")]` → M3.)

### 3.5 🛠 Kasıtlı bozuk örnek: captive dependency ⭐ (`/captive`)

**Captive dependency:** uzun ömürlü bir servisin (Singleton), daha kısa ömürlü bir servisi (Scoped) constructor'ına alması. Singleton bir kez oluşturulur → aldığı scoped nesne de **onunla birlikte sonsuza dek yaşar**: "scoped" servis fiilen singleton'a döner.

```csharp
public sealed class ScopedDependency : IDisposable { public Guid Id { get; } = Guid.NewGuid(); /* ... */ }   // think: a DbContext
public sealed class CaptiveSingleton(ScopedDependency scoped) { public ScopedDependency Scoped { get; } = scoped; }   // BUG

services.AddScoped<ScopedDependency>();
services.AddSingleton<CaptiveSingleton>();      // singleton → scoped
```

Gerçek `DbContext` ile sonuçları ciddidir: context thread-safe değildir ama artık tüm request'ler paylaşır (eşzamanlılık hataları), change tracker sonsuza dek büyür (bellek sızıntısı), bayat veri okunur, bağlantı hiç yenilenmez; çoğu zaman da **geliştirmede fark edilmeden** canlıda patlar.

`/captive` endpoint'i **uygulamanın kendi container'ına dokunmaz**; [`CaptiveDemo.cs`](CaptiveDemo.cs) aynı kayıtlarla ayrı `ServiceProvider`'lar kurar:

```bash
curl http://localhost:5080/captive
```

```text
1) ValidateScopes = true
   InvalidOperationException: Cannot consume scoped service 'Junior.AspNetCoreFundamentals.DiBasics.ScopedDependency' from singleton 'Junior.AspNetCoreFundamentals.DiBasics.CaptiveSingleton'.

2) ValidateScopes = true + ValidateOnBuild = true
   AggregateException: Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: Junior.AspNetCoreFundamentals.DiBasics.CaptiveSingleton Lifetime: Singleton ImplementationType: Junior.AspNetCoreFundamentals.DiBasics.CaptiveSingleton': Cannot consume scoped service 'Junior.AspNetCoreFundamentals.DiBasics.ScopedDependency' from singleton 'Junior.AspNetCoreFundamentals.DiBasics.CaptiveSingleton'.)
   inner -> InvalidOperationException: Error while validating the service descriptor '...CaptiveSingleton ...': Cannot consume scoped service '...ScopedDependency' from singleton '...CaptiveSingleton'.

3) No validation (production default)
   scope 1: scoped service Id = fe914cb7   singleton's captured Id = 6b9fb15a
   scope 2: scoped service Id = 3a0692eb   singleton's captured Id = 6b9fb15a
   same singleton instance in both scopes: True
   captured Id the same in both scopes ("scoped" now behaves like a singleton): True
   scope 1's own scoped instance disposed with its scope: True
   captured instance disposed with the scope: False  (it lives until the root provider/app shuts down)

4) Fix: IServiceScopeFactory inside the singleton (validation ON, no error)
   call 1: scoped Id = 76b78bf5
   call 2: scoped Id = 8031a890   (a new scoped instance each call)
```

Çıktıyı yorumlayalım:

1. **`ValidateScopes = true`:** singleton'ı çözmeye çalışınca `InvalidOperationException` ("Cannot consume scoped service … from singleton …"). Hata **yalnızca o yol çalıştığında** yakalanır.
2. **+ `ValidateOnBuild = true`:** `BuildServiceProvider()` / `app.Build()` sırasında **tüm kayıtlar** taranır; bulunan sorunlar tek bir `AggregateException` içinde toplanır (iç hata yine `InvalidOperationException`). Uygulama **başlamaz** → hata deploy'a bile çıkmadan yakalanır.
3. **Validasyon yok:** hiçbir hata yok. İki ayrı scope'tan (iki "request") çözülen singleton **aynı** nesnedir ve içindeki "scoped" servisin Id'si iki scope'ta da `6b9fb15a`'dır; oysa scope'un kendi scoped servisi her seferinde farklıdır (`fe91…`, `3a06…`). Yakalanan nesne scope'la birlikte dispose da edilmez. (Kayıtsız doğrulamada singleton'ın bağımlılığı scope'tan değil **root provider**'dan çözülür; yani ilk request'in nesnesi bile değil, tüm request'lerin paylaştığı ayrı bir "kök" instance'tır. Sonuç aynı: scoped davranış yok.)
4. **Çözüm:** singleton'a scoped servisi değil **`IServiceScopeFactory`** enjekte et ve işlem başına kısa ömürlü bir scope aç (`using var scope = factory.CreateScope();`). Başka çıkış yolları: servisi gerçekten Scoped/Transient yapmak ya da bağımlılığı metot parametresi olarak geçirmek; arka plan işi (`BackgroundService`, singleton) için scope-per-iteration en yaygın kalıptır.

#### Neden Development'ta validasyon açık, Production'da kapalı?

`WebApplication.CreateBuilder` (genel host varsayılanları) service provider'ı **`Development` ortamında** `ValidateScopes = true` ve `ValidateOnBuild = true` ile kurar; diğer ortamlarda (Staging/Production) ikisi de **kapalıdır**. Bu repoda doğrulandı:

```text
# captive registration (singleton → scoped) added to the APP's own container, then:

dotnet run -- J6.2 --environment Development
Unhandled exception. System.AggregateException: Some services are not able to be constructed (Error while validating the service descriptor
'ServiceType: ...CaptiveSingleton Lifetime: Singleton ...': Cannot consume scoped service '...ScopedDependency' from singleton '...CaptiveSingleton'.)
   → the app crashes at startup (app.Build())

dotnet run -- J6.2 --environment Production
   → starts normally and serves requests; the bug stays hidden
```

Mantık: validasyon **geliştirici içindir** — hata ilk F5'te patlasın. Production'da varsayılan olarak kapalıdır çünkü `ValidateOnBuild` her kaydı tarar (açılış süresi), `ValidateScopes` her çözümlemede ek kontrol yapar (küçük ama sıfır olmayan maliyet) ve varsayım, bu tür kayıt hatalarının geliştirme/test aşamasında zaten yakalanmış olmasıdır. **Dikkat:** Development'ta test etmeyen/`Production` ortamıyla koşan CI ve entegrasyon testleri hatayı kaçırır. İsterseniz her ortamda açın:

```csharp
builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});
```

Sınırlar: `ValidateOnBuild`, factory lambda'larının (`AddSingleton(sp => ...)`) içinde yaptığınız `GetRequiredService` çağrılarını analiz edemez; bunları ancak `ValidateScopes` çalışma anında yakalar. Yani validasyon bir güvenlik ağıdır, tasarım disiplininin yerini tutmaz.

**Alıştırma:** `DiBasicsTopic.AddServices` sonuna `services.AddScoped<ScopedDependency>(); services.AddSingleton<CaptiveSingleton>();` ekleyip uygulamayı hem `--environment Development` hem de varsayılan (Production) ile başlatın; farkı gözlemleyin, sonra satırları silin.

### 3.6 `IServiceProvider` ve service locator anti-pattern'i

Container'ın kendisi de bir servistir: `IServiceProvider` (ve `IServiceScopeFactory`) enjekte edilebilir. `GetRequiredService<T>()` ile elle çözümlemek **service locator** desenidir:

```csharp
// ❌ Service locator: dependencies are hidden (not visible in the constructor), runtime errors instead of startup errors, hard to test
public class ReportService(IServiceProvider sp)
{
    public void Run() => sp.GetRequiredService<IEmailSender>().Send(/* ... */);
}

// ✅ Explicit dependency
public class ReportService(IEmailSender sender) { public void Run() => sender.Send(/* ... */); }
```

`/lifetimes` endpoint'indeki `ctx.RequestServices.GetRequiredService<...>()` (A sütunu) yalnızca **çözümlemenin container'dan geçtiğini göstermek** içindir; gerçek kodda (B) yolunu kullanın. `IServiceProvider`/`IServiceScopeFactory` enjekte etmek meşru istisnalardır: **composition root** (Program.cs), middleware/`BackgroundService` içinde scope açma, factory sınıfları (3.5'in çözümü), eklenti yükleme gibi dinamik durumlar. `GetService<T>()` kayıt yoksa `null`, `GetRequiredService<T>()` ise exception döner; ikincisini tercih edin.

### 3.7 `IDisposable` sahipliği: container ne zaman dispose eder?

Kural: **Container, kendi oluşturduğu `IDisposable`/`IAsyncDisposable` nesnelerin sahibidir ve dispose eder; sizin oluşturup verdiğiniz nesnelerin sahibi değildir.**

| Kayıt biçimi | Dispose ne zaman? |
|--------------|-------------------|
| **Transient** (container oluşturdu) | Onu çözen **scope** dispose edilince (ASP.NET Core'da request sonu) — tüketen sınıf işini bitirince **değil** |
| **Scoped** | Scope dispose edilince (request sonu) |
| **Singleton** (container oluşturdu) | Root provider dispose edilince = **host kapanırken** |
| `AddSingleton<T>(instance)` — **siz** `new` ile oluşturup verdiniz | Container **dispose etmez**; sahibi sizsiniz |
| Factory (`AddSingleton(sp => new X())`) | Container oluşturduğu için dispose **eder** |

Pratik sonuçlar:

- Servisin içinde `Dispose` çağırmayın / `using` ile sarmayın: container'dan gelen nesneyi başkaları da kullanıyor olabilir (özellikle scoped/singleton).
- **Transient + `IDisposable`** root provider'dan çözülürse (singleton içinden, ya da scope açmadan `app.Services.GetRequiredService<T>()`) dispose'u uygulama sonuna kalır, container hepsini listede tutar → **bellek sızıntısı**. Scope içinde çözün.
- Singleton `IDisposable` ise host kapanırken `Dispose` çalışır; scoped servis bundan sonra kullanılmaz.

### 3.8 Komut satırı ve `requests.http`

```bash
cd WebApp
dotnet run -- J6.2                                 # Production (default: no scope validation in the app's container)
dotnet run -- J6.2 --environment Development       # Development (validation ON)

curl http://localhost:5080/                        # endpoint list
curl http://localhost:5080/lifetimes               # call twice: which Id changes?
curl "http://localhost:5080/greeters?name=Ada"
curl http://localhost:5080/captive
```

[`requests.http`](requests.http) dosyası aynı istekleri VS Code / Rider / Visual Studio'da gönderir.

---

## 4. Gerçek dünya senaryosu

**"Lokalde sorunsuz, canlıda kullanıcılar birbirinin verisini görüyor / `A second operation was started on this context` hatası."**

1. Bir geliştirici, performans için bir `ProductCache` sınıfını **Singleton** yapar ve constructor'ına `AppDbContext` (Scoped) alır.
2. Development'ta `ValidateScopes`/`ValidateOnBuild` açıktır; uygulama açılışta `Cannot consume scoped service 'AppDbContext' from singleton 'ProductCache'` ile çökerdi. **Ama** geliştirici uygulamayı Development dışı bir ortamda çalıştırıyor (örn. `ASPNETCORE_ENVIRONMENT` ayarsız = Production, ya da Staging'e doğrudan deploy), veya bağımlılık bir `AddSingleton(sp => ...)` factory'sinin içinde gizli → validasyon hiçbir şey yakalamaz.
3. Canlıda `AppDbContext` tüm request'ler tarafından paylaşılır: eşzamanlı request'lerde `InvalidOperationException: A second operation was started on this context`, change tracker büyür (bellek artar), bayat/başka kullanıcının tracked verisi dönebilir.
4. Teşhis: servis kayıtlarını (`Program.cs`) gözden geçir; singleton'ların constructor bağımlılıklarına bak; `ValidateScopes/ValidateOnBuild`'i Production profilinde de aç ve entegrasyon testlerine ekle.
5. Çözüm: `ProductCache` `IServiceScopeFactory` alır, yenileme sırasında `using var scope = factory.CreateScope()` ile kısa ömürlü `DbContext` kullanır (ya da `IDbContextFactory<AppDbContext>` — → M6).

---

## 5. Mülakat perspektifi

Sık sorulanlar ([`interview-questions.md`](interview-questions.md)'de cevaplarıyla):
- **Transient / Scoped / Singleton farkı?** ⭐ (hangisi ne zaman, ASP.NET Core'da scope nedir)
- **Singleton içine Scoped enjekte edersen ne olur?** ⭐ (captive dependency, `ValidateScopes`)
- DI nedir, IoC ve DIP ile farkı; neden `new` kullanmıyoruz?
- Aynı arayüzün birden çok implementasyonu kayıtlıysa ne enjekte edilir? `IEnumerable<T>` ne verir?
- `AddScoped` ile `TryAddScoped` / `TryAddEnumerable` farkı
- Service locator neden anti-pattern?
- Container `IDisposable` nesneleri ne zaman dispose eder?
- `HttpContext.RequestServices` nedir; middleware'e scoped servis nasıl enjekte edilir? (→ J6.4: `InvokeAsync` parametresi)

**İpucu:** "Lifetime'ları" tanım olarak değil **örnekle** anlatın: Singleton = cache/konfigürasyon, Scoped = `DbContext` (request başına), Transient = hafif durumsuz yardımcı. Ardından hemen "singleton scoped'ı tutamaz çünkü …" diye captive dependency'ye bağlayın — mülakatçı genelde bunu bekler.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| Singleton'a Scoped (`DbContext`) enjekte etmek | `IServiceScopeFactory` ile işlem başına scope açın veya servisi Scoped yapın (3.5) |
| Her şeyi Singleton yapmak ("daha hızlı") | State paylaşımı ve thread-safety sorunları; durum taşıyan servis Scoped/Transient olmalı |
| Singleton'da request'e özel durum (kullanıcı, tenant) saklamak | Request verisini Scoped serviste ya da `HttpContext`'te tutun; singleton kullanıcılar arası paylaşılır |
| `new SomeService()` ile container'ı atlamak | Bağımlılığı constructor'dan alın; aksi halde lifetime/dispose/test avantajı gider |
| `IServiceProvider` enjekte edip her yerde `GetService` çağırmak (service locator) | Bağımlılıkları constructor'da açıkça yazın |
| `GetService<T>()` sonucunu null kontrolsüz kullanmak | `GetRequiredService<T>()` kullanın (anlaşılır exception) |
| `Build()` sonrası kayıt eklemeye çalışmak | Tüm kayıtlar `builder.Services` üzerinden `Build()` öncesinde yapılır |
| Container'dan gelen `IDisposable`'ı kendiniz `Dispose`/`using` etmek | Sahibi container; siz yalnızca `new` ile kendiniz oluşturduklarınızı dispose edin |
| Root provider'dan (`app.Services`) scope açmadan Scoped servis çözmek | `using var scope = app.Services.CreateScope();` içinde çözün (Development'ta `ValidateScopes` hata verir) |
| Constructor'a 8+ bağımlılık almak | Sınıfın çok sorumluluğu var (SRP ihlali); böl veya facade kullan — DI sorunu gizler, çözmez |
| Aynı arayüzü ikinci kez `Add*` ile kaydedip eskisinin değiştiğini sanmak | Eskisi **listede kalır**, yenisi sadece tekil çözümlemede kazanır; değiştirmek için `Replace`, korumak için `TryAdd*` |
| Constructor içinde ağır iş/IO yapmak | Constructor yalnızca bağımlılıkları atamalı; iş mantığını metoda koyun (container grafiği kurarken yavaşlar/patlar) |
| Production'da scope validasyonuna güvenip test etmemek | `ValidateScopes`/`ValidateOnBuild`'i test ortamında da açın |

---

## 7. Performans & trade-off

- **Çözümleme maliyeti:** Transient her seferinde `new` + (varsa) dispose takibi demektir; MS DI hızlıdır (derlenmiş çağrı ağaçları), ama sıcak yolda binlerce transient oluşturmak GC baskısı yaratır. Ucuz ve durumsuz sınıf → Transient sorun değil; pahalı oluşturulan (HTTP client handler, regex, serializer ayarları) → Singleton/fabrika.
- **Singleton = paylaşım = thread-safety:** Performans kazancı (bir kez oluştur) karşılığında eşzamanlılık sorumluluğu getirir. Değişmez (immutable) ya da kilitli/concurrent koleksiyonlu olmalı (→ J4, M4).
- **`IDisposable` transient'ler:** Container hepsini scope bitene kadar bir listede tutar → uzun yaşayan scope'ta (ya da root'ta) bellek birikir.
- **Validasyon maliyeti:** `ValidateOnBuild` açılışta tüm kayıtları gezer, `ValidateScopes` çözümlemede küçük kontrol ekler; Development'ta açık, canlıda genelde kapalıdır. Maliyet küçük, kazanç büyük olduğundan CI/test ortamında açık tutmak iyi bir takastır.
- **Constructor injection vs `IServiceProvider`:** Constructor injection açık ve test edilebilir; `IServiceProvider` esnek ama bağımlılıkları gizler → yalnızca scope açma/dinamik çözümleme gibi gerçek ihtiyaçta.
- **Dahili container vs üçüncü parti (Autofac, vb.):** MS.DI bilerek basit tutulmuştur (property injection, decorator, assembly scanning yok); çoğu uygulamaya yeter. Gelişmiş ihtiyaç → Scrutor ile decoration/scanning ya da başka container (→ M3).
- **Keyed services (.NET 8+)** çoklu implementasyonu `if/switch` olmadan seçmenin hafif yoludur; ama anahtarlar string olduğundan derleme zamanı güvenliği azdır.

---

## 8. Kaynaklar

- ASP.NET Core'da Dependency Injection: <https://learn.microsoft.com/aspnet/core/fundamentals/dependency-injection>
- .NET'te Dependency Injection (genel rehber, lifetime'lar, scope validasyonu): <https://learn.microsoft.com/dotnet/core/extensions/dependency-injection>
- Dependency injection guidelines (captive dependency, `IDisposable`, service locator): <https://learn.microsoft.com/dotnet/core/extensions/dependency-injection-guidelines>
- `ServiceProviderOptions` (`ValidateScopes`, `ValidateOnBuild`): <https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.serviceprovideroptions>
- Mark Seemann — *Dependency Injection Principles, Practices, and Patterns* (kitap) ve "Service Locator is an Anti-Pattern": <https://blog.ploeh.dk/2010/02/03/ServiceLocatorisanAnti-Pattern/>
- Martin Fowler — Inversion of Control Containers and the Dependency Injection pattern: <https://martinfowler.com/articles/injection.html>
- Derinleştirme: keyed services, decorator, scope yönetimi, container içi → M3
