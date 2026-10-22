# J4.3 Cancellation Temelleri

> 📁 `ConsoleApp/01-junior/04-async-basics/03-cancellation-basics/` · Önkoşul: J4.2 · Sorular: [`interview-questions.md`](interview-questions.md)

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

**Cancellation (iptal)**, çalışan bir işe "artık sonucuna ihtiyacım yok, bırak" demenin standart .NET yoludur: `CancellationTokenSource` iptali **tetikler**, `CancellationToken` iptali **gözler**. İptal **kooperatiftir**: kimse işi zorla öldürmez (`Thread.Abort` .NET Core'da yoktur); token'ı izleyen kod durur, izlemeyen devam eder. Konunun kodu [`Sections/`](Sections/) altında 4 bölümdür (`dotnet run -- J4.3 <number>`).

---

## 2. Neden?

- Kullanıcı sayfayı kapattığında, istek zaman aşımına uğradığında ya da uygulama kapanırken **boşa çalışan iş** CPU, veritabanı bağlantısı ve thread tüketir.
- Zaman aşımı (`CancelAfter`), yarış (ilk cevap gelince diğerlerini iptal) ve temiz kapanış (graceful shutdown) hep aynı mekanizmayı kullanır.
- ASP.NET Core her istek için bir token sağlar; bunu aşağı geçirmek bir kod inceleme klasiğidir.

---

## 3. Nasıl?

### 3.1 `CancellationTokenSource` / `CancellationToken` — iptal kooperatiftir ⭐ ([`Section01`](Sections/Section01_CooperativeModel.cs))

```csharp
using var cts = new CancellationTokenSource();
var work = DoWorkAsync(cts.Token);     // give the token to the work
cts.Cancel();                          // REQUEST cancellation

async Task DoWorkAsync(CancellationToken ct)
{
    for (var i = 0; i < 20; i++)
    {
        ct.ThrowIfCancellationRequested();   // throw OperationCanceledException if cancellation was requested
        await Task.Delay(50, ct);            // pass it to the API that accepts a token
    }
}
```

Demoda iki işçi: **kooperatif** olan iptalden ~20 ms sonra durur (`Task.IsCanceled = true`); **inatçı** olan token'a bakmadığı için 20 adımın tamamını yapar (~1 sn).

- **CTS** (sahibi iptali isteyen taraf) tek kullanımlıktır: iptal edilen CTS sıfırlanamaz; yeni iş için yeni CTS. `IDisposable`'dır (zamanlayıcı/kayıt tutar) → `using`.
- **Token** küçük bir struct'tır; metotlara serbestçe geçilir. `CancellationToken.None` = iptal edilemez (parametre varsayılanı).
- Bayrağı okumanın yolları: `token.IsCancellationRequested` (bayrağa bak, kendin karar ver) ve `token.ThrowIfCancellationRequested()` (standart: işi kes, yukarı bildir).
- `token.Register(callback)`: iptal anında çalışacak temizlik geri çağrısı; dönen kaydı dispose ederek kaldırılır.

### 3.2 Konvansiyonlar ([`Section02`](Sections/Section02_Conventions.cs))

- **Token metot imzasında SON parametredir**, çoğunlukla varsayılanı vardır ve `cancellationToken` (veya `ct`) adını taşır:

```csharp
Task<string> LoadReportAsync(string name, int pageSize = 100, CancellationToken cancellationToken = default)
```

- İptal edilebilir her metot token alır ve çağırdığı **her async API'ye** geçirir. Token'ı alan nesnenin alanı/özelliği olarak **saklamayın** (istek başına yaşar).
- **`OperationCanceledException`** iptali temsil eder; hata değildir (log seviyesi Information/Debug; 500 üretmeyin). Yakalarken kapsamı daraltın:

```csharp
catch (OperationCanceledException) when (ct.IsCancellationRequested) { /* our own cancellation */ }
```

- **`CancelAfter(timeout)`**: süre dolunca otomatik iptal (zaman aşımı). Kullanıcı iptali ve zaman aşımını birleştirmek için `CancellationTokenSource.CreateLinkedTokenSource(a, b)`; hangisinin tetiklediği her iki CTS'in `IsCancellationRequested`'ına bakılarak ayırt edilir.
- **`HttpClient`**: token iptali de `HttpClient.Timeout` de `TaskCanceledException` (`OperationCanceledException`'ın alt sınıfı) üretir; zaman aşımında `InnerException` `TimeoutException`'dır.

### 3.3 🛠 İptal edilebilir rapor endpoint'i ([`Section03`](Sections/Section03_CancellableEndpoint.cs))

Yerel bir Kestrel API'sinde 10 adımlık (1500 ms) rapor üretimi iki endpoint'te:

```csharp
// Minimal API: a CancellationToken parameter is automatically bound to HttpContext.RequestAborted
app.MapGet("/report", async (CancellationToken requestAborted) =>
    await GenerateReportAsync("/report", requestAborted));

// The version that does not pass the token — even if the client is gone, the work runs to the end
app.MapGet("/report-ignores-token", (CancellationToken requestAborted) =>
    GenerateReportAsync("/report-ignores-token", CancellationToken.None));
```

İstemci 400 ms sonra vazgeçip bağlantıyı kesiyor; sunucu günlüğü:

```
[/report] step 1/10
[/report] step 2/10
[/report] CLIENT DISCONNECTED → RequestAborted fired, report generation STOPPED
[/report-ignores-token] step 1/10 … step 10/10
[/report-ignores-token] report COMPLETED (even though nobody is waiting)
```

- İstemci bağlantıyı kesince Kestrel `HttpContext.RequestAborted` token'ını iptal eder; endpoint/controller action parametresi olarak `CancellationToken` almak yeterlidir.
- Token'ı izlemeyen sürümde terk edilen her istek sunucuya **tam maliyetle** yük bindirir (CPU, DB sorgusu, bağlantı).

### 3.4 Token'ı aşağı geçirmeyi unutmak ([`Section04`](Sections/Section04_ForgottenToken.cs))

100 ms'de iptal edilen 3 aşamalı (900 ms) iş: token'ı her aşamaya geçiren sürüm **115 ms**'de durur; geçirmeyi unutan sürüm **903 ms**'de bitirir (iptal görmezden gelindi). Hata sessizdir — derleme uyarısı vermez, test geçer. `CA2016` ("token'ı ileri geçir") analyzer kuralı bunu yakalar; `.editorconfig` ile `warning` yapın.

Token kabul eden tipik API'ler:

| Alan | Örnekler |
|------|----------|
| `HttpClient` | `GetAsync(url, ct)`, `GetStringAsync(url, ct)`, `SendAsync(request, ct)` |
| EF Core | `ToListAsync(ct)`, `FirstOrDefaultAsync(ct)`, `SaveChangesAsync(ct)` |
| Stream / File | `ReadAsync(buffer, ct)`, `File.ReadAllTextAsync(path, ct)` |
| Task / eşitleme | `Task.Delay(ms, ct)`, `SemaphoreSlim.WaitAsync(ct)`, `Task.Run(work, ct)` |
| Channel / akış | `reader.ReadAsync(ct)`, `IAsyncEnumerable.WithCancellation(ct)` |

CPU döngüsünde token alan API yoksa kendiniz kontrol edin: `ct.ThrowIfCancellationRequested()` (her N iterasyonda bir).

---

## 4. Gerçek dünya senaryosu

**"Kullanıcı rapor ekranını kapatınca veritabanı CPU'su düşmüyor."** Endpoint 30 sn'lik bir sorgu çalıştırıyor; kullanıcılar sabırsızlanıp sayfayı yeniliyor ve her yenileme yeni bir 30 sn'lik sorgu başlatıyor (öncekiler hâlâ çalışıyor). Action'a `CancellationToken ct` eklenip `ToListAsync(ct)` ile geçirilince tarayıcı kapatıldığında sorgu iptal oldu, DB yükü düştü.

**"Dağıtımda eski pod'lar 30 sn kapanmıyor."** Arka plan servisi döngüsünde `stoppingToken`'ı gözlemiyordu; `await Task.Delay(1000, stoppingToken)` ve `while (!stoppingToken.IsCancellationRequested)` ile temiz kapanış sağlandı (→ M12).

**"Üçüncü taraf API'ye çağrılar asılı kalınca tüm istekler yavaşlıyor."** `HttpClient`'a token (zaman aşımı) geçilmediği için çağrılar sonsuz bekliyordu; `CancelAfter(2 sn)` + `HttpClient` token'ı ile sınırlandı, hata kontrollü yönetildi (→ M3 dayanıklılık).

---

## 5. Mülakat perspektifi

Sık sorulanlar: **İptal neden "kooperatif"tir?** · `CancellationToken` neden metodun son parametresi? · `OperationCanceledException` nasıl yönetilir? · `CancelAfter` ve linked token · ASP.NET Core'da istemci kopunca ne olur? · EF Core / `HttpClient` çağrılarına token geçirmeyi unutursak?

**İpucu:** Soruya bir **somut senaryoyla** cevap verin: *"Kullanıcı sayfayı kapattı → Kestrel `RequestAborted`'ı iptal eder → action'daki token'ı `ToListAsync(ct)`'ye geçirdiysem sorgu durur; geçirmediysem DB boşuna çalışır."*

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| Token'ı alıp alt çağrılara geçirmemek | Token alan **her** async çağrıya ver (CA2016) |
| `catch (OperationCanceledException)` ile iptali hata gibi loglamak / 500 dönmek | İptal hata değildir; `when (ct.IsCancellationRequested)` ile filtrele, Information logla |
| Token'ı sınıf alanında saklamak | Metot parametresi olarak ilet (istek başına yaşar) |
| Token parametresini ortada/başta tanımlamak | **Son** parametre, `default` varsayılanıyla |
| CTS'i dispose etmemek / iptalden sonra yeniden kullanmak | `using var cts`; iş başına yeni CTS |
| Uzun CPU döngüsünde iptali hiç kontrol etmemek | `ct.ThrowIfCancellationRequested()` |
| İptal edince kısmi durumu temizlememek (yarım dosya, açık transaction) | `finally`/`Register` ile temizlik; transaction'ı geri al |
| Zaman aşımı için `WhenAny + Delay` kullanıp işi arkada bırakmak | İşe token geçir; `CancelAfter` / `WaitAsync` |
| Zaman aşımı mı kullanıcı iptali mi ayırt etmemek | Linked CTS + `IsCancellationRequested`; `HttpClient`'ta `InnerException is TimeoutException` |

---

## 7. Performans & trade-off

- **Maliyet düşüktür:** `CancellationToken` bir struct'tır; `CancelAfter`/`Register` küçük bir zamanlayıcı/kayıt yaratır (CTS'i dispose edin).
- **İptal gecikmesi:** Kooperatif iptal, işin bir sonraki kontrol noktasına kadar gecikir; sık kontrol edilen kısa adımlar hızlı tepki verir, uzun adımlar yavaş. Demoda token'lı sürüm ~15–20 ms'de durdu.
- **Zorla iptal yoktur:** Hızlı tepki gerekiyorsa işi kontrol noktalarına bölmek gerekir; ayrı process'i öldürmek son çaredir.
- **Sınırlar:** İptal edilen işin yan etkileri (DB'ye yazılmış satır, gönderilmiş e-posta) **geri alınmaz**; iptal güvenli noktalarda yapılmalı, kritik adımlar iptal edilemez bırakılmalıdır (örn. `CancellationToken.None` ile commit).
- Derin konular (kooperatif iptal tasarımı, `CancellationToken` ile kuyruk/kanal tüketimi, graceful shutdown) → M4, M12.

---

## 8. Kaynaklar

- İptal (managed threads): <https://learn.microsoft.com/dotnet/standard/threading/cancellation-in-managed-threads>
- `CancellationTokenSource`: <https://learn.microsoft.com/dotnet/api/system.threading.cancellationtokensource>
- Task iptali: <https://learn.microsoft.com/dotnet/standard/parallel-programming/task-cancellation>
- ASP.NET Core'da `RequestAborted` ve token bağlama: <https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis/parameter-binding>
- CA2016 kuralı: <https://learn.microsoft.com/dotnet/fundamentals/code-analysis/quality-rules/ca2016>
- `HttpClient` zaman aşımı ve iptal: <https://learn.microsoft.com/dotnet/fundamentals/networking/http/httpclient>
- .NET API tasarım yönergeleri — async/iptal: <https://learn.microsoft.com/dotnet/standard/design-guidelines/>
