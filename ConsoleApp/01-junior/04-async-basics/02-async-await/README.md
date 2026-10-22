# J4.2 `async`/`await` Kullanımı

> 📁 `ConsoleApp/01-junior/04-async-basics/02-async-await/` · Önkoşul: J4.1 · Sorular: [`interview-questions.md`](interview-questions.md)

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

`async`/`await`, bekleme gerektiren işleri (ağ, disk, veritabanı) **thread'i bloklamadan** ama **sıralı koda benzeyen** biçimde yazmayı sağlar. Derleyici `async` metodu bir **state machine**'e çevirir; `await` noktasında iş bitmemişse metodun geri kalanı "devam kodu" olarak kaydedilir ve çağıran hemen serbest kalır (iç yapısı → M4.2). Konunun kodu [`Sections/`](Sections/) altında 7 bölümdür (`dotnet run -- J4.2 <numara>`), adım adım dönüşüm örneği [`Conversion/ConversionSteps.cs`](Conversion/ConversionSteps.cs) dosyasındadır.

---

## 2. Neden?

- Junior'dan beklenen async kodu **doğru kullanmak**tır: yanlış kullanım (`async void`, `.Result`) uygulamayı çökertir ya da kilitler.
- Modern .NET API'lerinin (`HttpClient`, EF Core, `Stream`, ASP.NET Core) tamamı async'tir; kütüphaneyi kullanmak async bilmeyi gerektirir.
- Mülakatta `async void`, `.Result`/`.Wait()` ve `WhenAll` davranışı klasik sorulardır.

---

## 3. Nasıl?

### 3.1 `Task`, `Task<T>`, `async` imzası, `await` ([`Section01`](Sections/Section01_TaskBasics.cs))

```csharp
static async Task<int> ComputeAsync(int x)   // dönüş: Task<int>; sonek: Async
{
    await Task.Delay(100);                    // thread bloklanmaz; devam kodu sonra çalışır
    return x * 2;                             // Task<int>'in sonucu
}

var task = ComputeAsync(21);   // başlar (task "sıcak"); ilk await'e kadar ÇAĞIRANIN thread'inde senkron çalışır
var result = await task;       // sonucu bekle (bloklamadan)
```

| Dönüş tipi | Anlamı |
|------------|--------|
| `Task` | Değer dönmeyen async iş (`void`'in async karşılığı) |
| `Task<T>` | `T` dönen async iş |
| `ValueTask<T>` | Sık **senkron biten** sıcak yollar için tahsisi azaltan alternatif (ölçmeden kullanmayın) |
| `void` | **Kaçının** (→ 3.3) |

- `async` metodu başka bir thread'e **taşımaz**; thread'i serbest bırakan `await`'tir.
- Konsolda/ASP.NET Core'da `await` sonrası devam kodu herhangi bir havuz thread'inde çalışır (T1 → T5); UI/eski ASP.NET'te aynı context'e döner.
- Yalnızca başka bir `Task`'ı aynen döndüren metotta `async` gerekmez (`return inner.Foo();`); ama `try`/`using` içindeyse **`await` şarttır** (yoksa kaynak erken kapanır, hata yakalanmaz).

### 3.2 🛠 Async all the way: adım adım dönüşüm ([`Section02`](Sections/Section02_Conversion.cs))

Aynı iş (dosya oku + iki bağımsız HTTP çağrısı) üç biçimde; tıklar "UI thread'inin ne kadar duyarlı kaldığını" gösterir:

| Adım | Kod | Süre | UI tık | Yorum |
|------|-----|------|--------|-------|
| 1 — Senkron | `File.ReadAllText`, `HttpClient.Send` | ≈308 ms | **1** | UI thread'i bloklandı (donmuş arayüz) |
| 2 — `Task.Run(senkron)` | "sahte async" | ≈307 ms | 15 | UI serbest ama bir **havuz thread'i yine bloklandı**; sunucuda hiçbir kazanç |
| 3 — Async all the way | `ReadAllTextAsync`, `GetStringAsync`, `WhenAll` | ≈160 ms | 8 | Hiçbir thread bloklanmadı **ve** bağımsız çağrılar paralel |

**Reçete:** (1) bloklayan API → async karşılığı, (2) metodu `async Task` yap, (3) **çağıranları da `await` et** (zincir giriş noktasına kadar uzar: controller, `Main`, event handler), (4) bağımsız işleri `WhenAll` ile birlikte bekle, (5) `CancellationToken` ekle (→ J4.3). **Async all the way:** zincirin ortasında senkron köprü (`.Result`, `.Wait()`) bırakmayın.

### 3.3 `async void` neden kötü? ⭐ ([`Section03`](Sections/Section03_AsyncVoid.cs))

```csharp
static async void FailsVoid() { await Task.Delay(100); throw new InvalidOperationException("..."); }

try { FailsVoid(); }          // hatasız DÖNER: exception henüz fırlatılmadı
catch (Exception) { }         // asla çalışmaz — çağıran bir Task alamaz
```

- Hata `await`'ten **sonra**, başka bir thread'de fırlar; yakalayan olmadığı için **yakalanmamış exception → process çöker** (demo bunu ayrı bir alt process'te çalıştırır; çıkış kodu 134, stderr'de `Unhandled exception`).
- Çağıran **await edemez, tamamlanmasını izleyemez**, test edilemez.
- `async Task` ise hatayı Task içinde taşır; `await` eden `try/catch` ile yakalar.
- **Tek meşru yer: event handler** (imza `void` zorunlu). Gövdeyi `try/catch` ile sarın:

```csharp
button.Click += async (s, e) => { try { await SaveAsync(); } catch (Exception ex) { ShowError(ex); } };
```

Fire-and-forget gerekiyorsa: hatayı loglayan sarmalayıcı veya arka plan kuyruğu (→ M12). Analyzer: `VSTHRD100` (Microsoft.VisualStudio.Threading.Analyzers).

### 3.4 `.Result` / `.Wait()` neden tehlikeli? ⭐ ([`Section04`](Sections/Section04_ResultDeadlock.cs))

`Task.Result` ve `.Wait()` thread'i **bloklar**. İki sorun:

1. **Deadlock (SynchronizationContext varsa):** UI thread'i `GetDataAsync().Result` ile bloklanır; `await` sonrası devam kodu UI thread'ine dönmek ister ama thread bloklu → ikisi birbirini bekler. Demoda tek thread'li bir context ile 1,5 sn içinde bitmeyen "DEADLOCK" gösterilir. `ConfigureAwait(false)` kütüphane kodunda bunu önler ama **çözüm değil, kırılgan bir bandajdır** (thread yine bloklu); doğru çözüm `await`.
2. **ASP.NET Core'da context yoktur** → klasik deadlock olmaz, ama her `.Result` bir havuz thread'ini bloklar; yük altında **thread pool starvation** (→ J4.1 §3.4, M4.3).

Ek fark: `.Result`/`.Wait()` hatayı `AggregateException` içine sarar; `await` ve `GetAwaiter().GetResult()` özgün exception'ı verir.

### 3.5 `Task.WhenAll`, `WhenAny`, `WhenEach` ve hata yakalama ([`Section05`](Sections/Section05_WhenAllAny.cs))

- `WhenAll`: hepsini bekler; süre = en yavaşı. Sonuç `T[]` (başarıda).
- **Hata:** `await Task.WhenAll(...)` yalnızca **ilk** exception'ı fırlatır; hepsi `task.Exception.InnerExceptions` içindedir (demoda 2 hata). `WhenAll` diğer task'ları **iptal etmez**.
- `WhenAny`: ilk bitene döner. Zaman aşımı kalıbı `WhenAny(iş, Task.Delay(t))` — ama zaman aşımı kazansa bile **iş arkada sürer** (iptal için token gerekir). Daha temizi: `task.WaitAsync(TimeSpan)` (.NET 6) → `TimeoutException`.
- `Task.WhenEach(tasks)` (**.NET 9**): task'ları **bitiş sırasıyla** `await foreach` ile işler.
- `WhenAll`'a verilen task'lar zaten çalışıyordur (hot); `WhenAll` yalnızca bekler. Sınırsız `WhenAll` (10.000 çağrı) yerine eşzamanlılığı sınırlayın.

### 3.6 `Thread.Sleep` vs `await Task.Delay` ([`Section06`](Sections/Section06_SleepVsDelay.cs))

`Thread.Sleep(400)` thread'i uyutur (UI tık sayısı 1: donmuş arayüz); `await Task.Delay(400)` zamanlayıcı kurar, thread serbest kalır (UI tık sayısı 19). Async kodda `Thread.Sleep` async'in amacını bozar.

### 3.7 `IAsyncEnumerable<T>`, `await foreach`, `await using` ([`Section07`](Sections/Section07_AsyncStreams.cs))

```csharp
static async IAsyncEnumerable<int> ReadPagesAsync([EnumeratorCancellation] CancellationToken ct = default)
{
    for (var page = 1; page <= 5; page++) { await Task.Delay(100, ct); yield return page; }
}

await foreach (var page in ReadPagesAsync().WithCancellation(token)) { /* sayfa geldikçe işle */ }

await using (var connection = new Connection()) { }   // IAsyncDisposable: DisposeAsync await edilir
```

- `IAsyncEnumerable<T>`: veriyi **geldikçe** işler (ilk sonuç 100 ms'de; `Task<List<T>>` ile 500 ms'de hepsi birden). Büyük/yavaş veri, sayfalı API, DB okuyucu için.
- `await using` / `IAsyncDisposable`: `Dispose`'ın kendisi I/O yapıyorsa (flush, bağlantı kapatma); senkron `Dispose`'a sarmak bloklar.
- ASP.NET Core Minimal API'de `IAsyncEnumerable<T>` döndürmek yanıtı akıtır.

---

## 4. Gerçek dünya senaryosu

**"Sipariş servisi sabah 09:00'da kilitleniyor."** Eski bir yardımcı sınıf `GetPriceAsync(...).Result` çağırıyor; sakin saatlerde sorun yok, yoğunlukta her istek bir havuz thread'ini bloklayıp başka bir çağrının sonucunu bekliyor — havuz tükeniyor, devam kodları çalışamıyor, servis donuyor. Çözüm: yardımcı sınıftan controller'a kadar tüm zinciri `async` yapmak (async all the way).

**"Servis bir gece rastgele çöküyor, log'da yalnızca `Unhandled exception`."** `async void Process()` içinde atılan bir exception tüm process'i düşürüyor. `async Task` + çağıranda `await` ve merkezi hata loglama ile düzeldi.

**"Bildirim e-postası bazen hiç gitmiyor."** `_ = SendAsync(...)` (await'siz fire-and-forget) hata verince kimse görmüyor. Hatayı loglayan bir sarmalayıcı veya outbox/kuyruk desenine geçildi.

---

## 5. Mülakat perspektifi

Sık sorulanlar: **`async void` neden kullanılmamalı?** ⭐ · **`.Result` kullanmanın riski ne?** ⭐ · **`Task.WhenAll` içinde bir task hata verirse ne olur?** · `async` bir metot yeni thread açar mı? · `Task.Run` ile async farkı · `ConfigureAwait(false)` ne yapar? · `Task` ile `ValueTask` · `await` ne yapar?

**İpucu:** `async void` sorusunda üç cümle söyleyin: *çağıran yakalayamaz/await edemez, exception process'i çökertir, tek istisna event handler'dır.* `.Result` sorusunda iki risk: *deadlock (context varsa) ve thread pool starvation (yoksa)*.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| `async void` metot | `async Task` (yalnızca event handler `async void` olabilir, gövde try/catch içinde) |
| `.Result` / `.Wait()` / `.GetAwaiter().GetResult()` | `await` — async all the way |
| Task'ı `await` etmeden bırakmak (`_ = DoAsync();`) | `await` et; gerçekten arka plansa hatayı logla / kuyruğa al |
| `Thread.Sleep` async kodda | `await Task.Delay` |
| Sync kodu `Task.Run` ile "async" yapmak (sunucuda) | Gerçek async API'yi kullan |
| Bağımsız çağrıları sıralı `await`'lemek | Başlat → `WhenAll` |
| `await Task.WhenAll` hatasında yalnızca ilk exception'ı loglamak | `task.Exception.InnerExceptions` |
| `async` metotta `lock` içinde `await` | `SemaphoreSlim.WaitAsync` |
| `try`/`using` içinde `await`'siz `return task;` | `await` et (kaynak erken kapanır, hata kaçar) |
| `Async` son eki koymamak, token almamak | `GetUserAsync(..., CancellationToken ct = default)` |

---

## 7. Performans & trade-off

- **State machine maliyeti:** Her `async` metot bir state machine (struct, gerektiğinde heap'e kutulanır) üretir; çok sıcak yollarda tahsis fark edilir → `ValueTask`, gereksiz `async` kaldırma. **Önce ölçün** (→ J11).
- **Async her zaman daha hızlı değildir:** Tek isteğin gecikmesini kısaltmaz; **ölçeklenebilirliği** (eşzamanlı istek sayısı) artırır. Sıralı bağımlı çağrılarda süre aynı kalır.
- **`ConfigureAwait(false)`:** Kütüphane kodunda context'e dönüş maliyetini ve deadlock riskini azaltır; ASP.NET Core uygulama kodunda gerekmez (→ M4.2).
- **`WhenAll` + sınırsız eşzamanlılık** bağımlı servisi boğabilir; sınır koyun.
- **`IAsyncEnumerable<T>`** bellek kullanımını düşürür, ilk sonuç gecikmesini kısaltır; tüm sonuç gerekiyorsa `Task<List<T>>` daha basittir.

---

## 8. Kaynaklar

- Asenkron programlama senaryosu: <https://learn.microsoft.com/dotnet/csharp/asynchronous-programming/async-scenarios>
- `async`/`await` ile TAP: <https://learn.microsoft.com/dotnet/csharp/asynchronous-programming/task-asynchronous-programming-model>
- Async return types: <https://learn.microsoft.com/dotnet/csharp/asynchronous-programming/async-return-types>
- Async akışlar (`IAsyncEnumerable<T>`): <https://learn.microsoft.com/dotnet/csharp/asynchronous-programming/generate-consume-asynchronous-stream>
- `IAsyncDisposable`: <https://learn.microsoft.com/dotnet/standard/garbage-collection/implementing-disposeasync>
- `Task.WhenEach` (.NET 9): <https://learn.microsoft.com/dotnet/api/system.threading.tasks.task.wheneach>
- Async/Await — Best Practices (Stephen Cleary): <https://learn.microsoft.com/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming>
- VS Threading Analyzers: <https://github.com/microsoft/vs-threading>
