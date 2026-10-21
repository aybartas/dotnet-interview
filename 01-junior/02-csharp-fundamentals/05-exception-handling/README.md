# J2.5 Exception Handling

> 📁 `01-junior/02-csharp-fundamentals/05-exception-handling/` · Önkoşul: J2.4 · Pratik: [`src/`](src/) · Test: [`tests/`](tests/) · Sorular: [`interview-questions.md`](interview-questions.md)

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

**Exception**, normal akışı kesen ve çağrı zincirinde yukarı doğru, bir `catch` bulunana kadar yayılan hata nesnesidir. Hata yönetimi dört soruya cevap verir: **Neyi yakalarım? Nerede? Ne yaparım? Yeniden nasıl fırlatırım?** [`src/ExceptionHandling`](src/ExceptionHandling/) 8 bölümlük bir console uygulaması, [`tests/`](tests/) 23 test içerir.

---

## 2. Neden?

- Yanlış hata yönetimi iki uçta zarar verir: **yutulan hata** (sessiz veri bozulması, bulunamayan bug) ve **kontrolsüz çökme**.
- `throw ex` gibi küçük bir yazım farkı, hatanın **nerede doğduğunu** silerek üretimdeki teşhisi saatler uzatabilir.
- Exception maliyeti ve doğru kullanım alanı mülakatta ve kod incelemede sık gündem olur.

---

## 3. Nasıl?

### 3.1 `try` / `catch` / `finally`, `using`

```csharp
try        { /* riskli iş */ }
catch (FormatException ex)  { /* bu tipe özel */ }          // yukarıdan aşağı İLK eşleşen
catch (Exception ex)        { /* genel (en sona) */ }        // özelden genele yazılır (aksi CS0160)
finally    { /* HER ZAMAN çalışır: temizlik */ }
```
- `finally` hata olsa da olmasa da, hatta `return`'den sonra bile çalışır ([`Section01`](src/ExceptionHandling/Sections/Section01_TryCatchFinally.cs)). İçine `return`/`throw` koymayın: bekleyen exception'ı **yutar veya ezer**.
- **`using` / `await using`** = `IDisposable`/`IAsyncDisposable` için `try/finally` şekerlemesi: exception olsa da kaynak bırakılır, catch'ten **önce** Dispose çalışır.

### 3.2 Exception hiyerarşisi ve custom exception

```
Exception
├─ SystemException ─ NullReferenceException, InvalidCastException, IndexOutOfRangeException, OutOfMemoryException …
│    └─ ArgumentException ─ ArgumentNullException, ArgumentOutOfRangeException
├─ InvalidOperationException ─ ObjectDisposedException
├─ NotSupportedException · NotImplementedException
├─ IOException ─ FileNotFoundException, DirectoryNotFoundException
└─ (kendi domain exception'larınız)
```
**Önce hazır tipi seç:** `ArgumentNullException` (argüman null, `ThrowIfNull`), `ArgumentOutOfRangeException`, `InvalidOperationException` (nesne *bu durumda* bu işlemi yapamaz), `NotSupportedException` (işlem hiç desteklenmiyor).

**Custom exception ne zaman?** (1) Çağıranın exception **tipine göre farklı davranması** gerekiyorsa (`catch (InsufficientFundsException)`), (2) **ek veri** taşıması gerekiyorsa (hesap no, eksik tutar → mesajı ayrıştırmak yerine property). Yalnızca farklı bir **mesaj** için tip üretmeyin. Kurallar: adı `…Exception` ile biter · `System.Exception`'dan türet (`ApplicationException`'dan değil) · standart ctor'lar (mesaj, mesaj+inner) · ek bilgi salt okunur property · hiyerarşi sığ ([`DomainExceptions.cs`](src/ExceptionHandling/Domain/DomainExceptions.cs): `DomainException` kökü → `InsufficientFundsException`, `OrderNotFoundException`, `ValidationFailedException`). Kök tip, üst katmana **tek catch ile** "iş kuralı hatası"nı ayırma imkânı verir (→ HTTP 4xx; beklenmeyen → 500). Altyapı hatasını çevirirken **`InnerException`'ı koru**.

### 3.3 Exception filter ⭐

```csharp
catch (HttpFailure ex) when (ex.StatusCode == 404) { … }
```
Filter doğruysa catch devreye girer; yanlışsa exception yakalanmamış gibi yoluna devam eder. Kritik fark: **filter, stack çözülmeden ÖNCE değerlendirilir** (sıra: filter → iç `finally` → catch gövdesi; [`Section03`](src/ExceptionHandling/Sections/Section03_Filters.cs) ve `FlowTests` kanıtlar) → debugger/dump'ta fırlatma noktasının stack'i ve yerel değişkenleri **sağlam** kalır. Tipik kullanım: HTTP status/hata kodu ayrımı, retry'lanabilir hata, `InnerException` tipi, **"logla ama yakalama"** deseni (filter içinde log + `false`). Filter hızlı ve yan etkisiz olmalı; içinde fırlatılan exception yutulur.

### 3.4 `throw` vs `throw ex` ⭐

```csharp
catch (Exception ex) { throw;    }   // ✓ orijinal stack trace KORUNUR
catch (Exception ex) { throw ex; }   // ✗ stack trace burada SIFIRLANIR
```
Deney ([`Section04`](src/ExceptionHandling/Sections/Section04_ThrowVsThrowEx.cs), `RethrowTests`): zincir `Entry → Middle → ThrowSite`.

| Yeniden fırlatma | Stack trace |
|------------------|-------------|
| `throw;` | `ThrowSite → Middle → Entry → …` (hatanın **doğduğu** yer görünür) |
| `throw ex;` | `Entry → …` — `ThrowSite`/`Middle` **kayboldu**; hata sanki Entry'de doğmuş |
| `throw new X("…", ex)` | Yeni exception + orijinal `InnerException` olarak (**hiçbir bilgi kaybolmaz**) |
| `ExceptionDispatchInfo.Capture(ex).Throw()` | Orijinal stack korunur, "End of stack trace from previous location" ile ayrılır (async altyapısı `await`'te bunu kullanır) |

Analyzer **CA2200** `throw ex`'i yakalar. `throw;` yalnızca catch bloğunda geçerlidir. Hiçbir şey yapmayacaksanız **hiç yakalamayın**.

### 3.5 Exception maliyeti ⭐

Fırlatma pahalıdır: stack trace yakalama + stack çözme + filter/finally işletme. Deney ([`Section05`](src/ExceptionHandling/Sections/Section05_Cost.cs)): 20.000 girdi, yarısı geçersiz → `int.TryParse` ≈ 1 ms, `Parse + catch` ≈ 45 ms (**~50×**). Exception **olmayan** yolda `try` bloğu neredeyse ücretsizdir.

**Kural:** exception **beklenen kontrol akışı** için değil **istisnai** durumlar içindir. Parse için `TryParse`, sözlük için `TryGetValue`, "kayıt yok" için `null`/`Result`/Try deseni. (Gerekçeler: performans, okunabilirlik, debugger'da first-chance gürültüsü, izleme sistemlerinde yanlış alarm.)

### 3.6 Neyi yakalamalı, neyi yakalamamalı?

| Yakala | Yakalama |
|--------|----------|
| **Ne yapacağını bildiğin** kurtarılabilir hata (`FileNotFoundException` → varsayılan yapılandırma) | Kendi **bug**'ın olan hatalar (`NullReferenceException`, `IndexOutOfRange`, `InvalidCast`, çağıranın `ArgumentNullException`'ı) → düzelt, susturma |
| Geçici hata + retry (`HttpRequestException` + `when`) | Ne yapacağını **bilmediğin** hata: yakalayıp loglayıp **yutmak** en kötüsü |
| Katman sınırında **dönüştür** (inner'ı koru) | `OutOfMemoryException` (çoğunlukla), `StackOverflowException` (**yakalanamaz**, process ölür) |
| Temizlik gerekiyorsa `catch { …; throw; }` | **Boş catch** (`catch { }`) |

**`catch (Exception)` ne zaman meşru?** Yalnızca **sınırlarda**: (1) uygulamanın en üst seviyesi / global handler (ASP.NET Core exception middleware → 500 + log), (2) arka plan döngüsü/worker (tek iş hatası tüm servisi öldürmesin), (3) üçüncü parti/plugin çağrı sınırı, (4) `catch + temizlik + throw;`, (5) CLI "son çare" (anlamlı mesaj + doğru exit code). Çağıranın hatası için **guard clause** ile erken ve net reddedin (`ArgumentNullException.ThrowIfNull`).

### 3.7 `Task` içindeki exception'lar ve `AggregateException`'a giriş

- `async` metottaki exception **Task'ın içine** yazılır; metot çağrısı **fırlatmaz**, `await`/`Wait()`/`Result` fırlatır (`task.Status == Faulted`).
- **`await`** orijinal exception'ı **açar** (ilk iç exception); **`.Wait()`/`.Result`** `AggregateException` ile **sarar** (ve deadlock riski vardır → J4).
- **`Task.WhenAll`**: birden çok hata olursa `await` yalnızca **ilkini** fırlatır; **hepsi** `task.Exception.InnerExceptions`'tadır. `AggregateException.Flatten()` iç içe aggregate'leri düzleştirir, `Handle()` bilinenleri işler.
- **İptal:** `OperationCanceledException` hata değil normal akıştır, ayrı yakalanır.
- ⚠ `async void`'deki exception **yakalanamaz** ve süreci çökertebilir (yalnızca event handler'lar için); unutulan (await edilmeyen) Task'taki exception **kaybolur**. Derin hali → J4, M4. ([`Section07`](src/ExceptionHandling/Sections/Section07_TaskExceptions.cs))

### 3.8 Exception vs `Result` (kavramsal giriş)

| | Exception | Result / Try-deseni / null |
|---|-----------|----------------------------|
| Hata türü | Beklenmeyen: bug, ortam, altyapı | **Beklenen** iş sonucu: doğrulama, kayıt yok, bakiye yetersiz |
| Sıklık | Nadir | Normal akışın parçası olabilir |
| Performans | Fırlatma pahalı | Ucuz |
| Çağıranı zorlama | Hayır (unutulabilir) | **Evet** (`Match` ile iki durum ele alınır) |
| Zincirde | Otomatik yayılır | Her seviyede taşıma gerekir |

[`Result<T>`](src/ExceptionHandling/Domain/Result.cs) minimal örnek. İkisi birlikte kullanılır: domain `Result` döner, en dış katman HTTP koduna çevirir; beklenmeyen hata exception olarak kalır. Ayrıntı (ROP, kütüphaneler, tartışma) → M11.6.

---

## 4. Gerçek dünya senaryosu

**"Üretimde NullReferenceException var ama stack trace hep aynı yeri gösteriyor."** Servis katmanında `catch (Exception ex) { _logger.LogError(ex.Message); throw ex; }` yazılmış. Her hata `OrderService.Create` satırında doğmuş gibi görünüyor; gerçek kaynak (repository'deki `null`) kayıp. Düzeltme: `throw;` (veya `new X("…", ex)`), log'a **exception nesnesinin kendisi** verilir (`LogError(ex, "…")`), CA2200 hata seviyesine alınır.

**"Worker bir bozuk mesajda çöküyor."** İş döngüsünde `catch` yok → tek mesaj tüm servisi öldürüyor, mesaj kuyruğa dönüp sonsuz çöküş döngüsü (poison message). Çözüm: iş sınırında `catch (Exception)` + log + dead-letter'a taşıma (meşru geniş catch), döngü devam eder.

**"API'de her 'kullanıcı bulunamadı' 500 dönüyor ve alarmı tetikliyor."** Beklenen durum exception ile modellenmiş. `OrderNotFoundException : DomainException` → middleware 404'e çevirir, alarm yalnızca beklenmeyen exception'larda çalışır; "kayıt yok" çok sıksa `Result`/`null`.

---

## 5. Mülakat perspektifi

Sık sorulanlar: **`throw` vs `throw ex`** ⭐ · `catch (Exception)` ne zaman meşru? · Exception filter nedir? · `finally` ne zaman çalışmaz? · `Parse` vs `TryParse` / exception maliyeti · Custom exception ne zaman? · Exception vs Result · `await` vs `.Wait()` exception farkı · `AggregateException` nedir.

**İpucu:** `throw ex`i anlatırken "stack trace kaybolur" demekle kalmayın: **hangi bilginin kaybolduğunu** ("hatanın doğduğu satır") ve **bunun teşhise etkisini** söyleyin; alternatifleri (`throw;`, sarmalama, `ExceptionDispatchInfo`) sıralayın.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| `throw ex;` | `throw;` |
| Boş `catch { }` / yutma | Logla veya yeniden fırlat; yakalama |
| Kontrol akışı için exception | `TryParse`, `TryGetValue`, `Result` |
| Her yerde `catch (Exception)` | Yalnızca sınırlarda |
| Mesajı string olarak ayrıştırmak | Custom exception + property |
| `ex.Message` loglayıp stack'i atmak | `LogError(ex, "bağlam")` — exception nesnesini ver |
| `finally` içinde `return`/`throw` | Yalnızca temizlik |
| Yeni exception fırlatırken `innerException`'ı atlamak | `new X("…", ex)` |
| `ApplicationException`'dan türetmek | `Exception`'dan (veya anlamlı alt tipten) |
| `NullReferenceException`'ı yakalamak | Null kontrolü / guard clause |
| `async void` içinde hata beklemek | `async Task` döndür, `await` et |
| `.Wait()` / `.Result` ile bloklamak | `await`; aksi halde `AggregateException` + deadlock riski |
| `OperationCanceledException`'ı hata gibi loglamak | İptal normal akıştır |
| Exception'ı mesajıyla `when` filtresinde karşılaştırmak | Tip/hata kodu/property |

---

## 7. Performans & trade-off

- **Fırlatma ≈ binlerce döngü** (stack trace yakalama + iki geçişli stack çözme); derin stack'te daha da pahalı. **Hot path'te** (sıcak döngü, yüksek QPS) exception ile akış kontrolü yapmayın. Try-deseni/Result tercih edin.
- **`try` bloğu** exception yokken ücretsiz denecek kadar ucuzdur — korkup try'ı kaldırmayın; sorun *fırlatma sıklığıdır*.
- **Filter vs catch+rethrow:** filter yeniden fırlatma maliyeti ve stack riski olmadan karar verir.
- **Result:** ucuz ve zorlayıcı ama her katmanda taşıma (map/bind) boilerplate'i getirir; derin çağrı zincirinde exception daha az gürültülüdür.
- **Genel handler maliyeti:** global exception middleware ucuzdur; asıl maliyet yutulan hatanın bedelidir.
- **Loglama:** hata başına tek yerde (sınırda) loglayın; her katmanda logla-ve-fırlat aynı hatayı kez kez yazar (log gürültüsü).

---

## 8. Kaynaklar

- Exception'lar ve exception işleme: <https://learn.microsoft.com/dotnet/csharp/fundamentals/exceptions/>
- Exception kullanım en iyi uygulamaları: <https://learn.microsoft.com/dotnet/standard/exceptions/best-practices-for-exceptions>
- Kendi exception sınıflarınızı oluşturma: <https://learn.microsoft.com/dotnet/standard/exceptions/how-to-create-user-defined-exceptions>
- `when` exception filter'ları: <https://learn.microsoft.com/dotnet/csharp/language-reference/statements/exception-handling-statements#exception-filters>
- CA2200 — stack detaylarını korumak için yeniden fırlatın: <https://learn.microsoft.com/dotnet/fundamentals/code-analysis/quality-rules/ca2200>
- `ExceptionDispatchInfo`: <https://learn.microsoft.com/dotnet/api/system.runtime.exceptionservices.exceptiondispatchinfo>
- Task tabanlı asenkron kalıpta exception: <https://learn.microsoft.com/dotnet/standard/parallel-programming/exception-handling-task-parallel-library>
- Eric Lippert — "Vexing exceptions": <https://ericlippert.com/2008/09/10/vexing-exceptions/>
