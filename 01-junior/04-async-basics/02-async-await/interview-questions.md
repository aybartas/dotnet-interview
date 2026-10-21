# J4.2 `async`/`await` Kullanımı — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. ⭐ `async void` neden kullanılmamalı?
`async void` metodun çağıranı bir `Task` alamaz: **await edemez, tamamlanmasını izleyemez, hatasını yakalayamaz**. Exception `await`'ten sonra başka bir thread'de fırlar ve yakalayan olmadığı için process'i çökertir (ASP.NET Core'da tüm sunucu düşer; UI'da uygulama kapanır). Ayrıca test edilemez. Tek meşru yer imzası `void` zorunlu olan **event handler**'lardır; onda da gövde `try/catch` ile sarılır. Diğer her yerde `async Task` kullanılır.

### 2. ⭐ `.Result` (veya `.Wait()`) kullanmanın riski nedir?
Thread'i bloklar. **Deadlock:** SynchronizationContext olan ortamlarda (UI, klasik ASP.NET) `await` sonrası devam kodu bloklanmış thread'e dönmek ister; thread de sonucu beklediği için ikisi birbirini bekler. **Thread pool starvation:** ASP.NET Core'da context olmadığı için klasik deadlock olmaz ama her `.Result` bir havuz thread'ini bloklar; yük altında havuz tükenir. Ayrıca hata `AggregateException` içine sarılır. Çözüm: async all the way.

### 3. ⭐ `Task.WhenAll` içinde bir task hata verirse ne olur?
`WhenAll` **hepsinin bitmesini bekler** (diğerlerini iptal etmez); sonuç Task faulted olur. `await Task.WhenAll(...)` yalnızca **ilk** exception'ı fırlatır; tüm hatalar `task.Exception.InnerExceptions` içindedir. Hepsini loglamak için `WhenAll` task'ını bir değişkende tutup catch'te `Exception.InnerExceptions`'a bakılır.

### 4. `async` bir metot yeni thread açar mı?
Hayır. `async` metot, ilk tamamlanmamış `await`'e kadar **çağıranın thread'inde senkron** çalışır. Thread'i serbest bırakan `await`'tir; iş (I/O) bittiğinde devam kodu bir havuz thread'inde (veya yakalanan context'te) çalışır. CPU işini başka thread'e almak için `Task.Run` gerekir.

### 5. `await` ne yapar?
Task tamamlandıysa senkron devam eder. Tamamlanmadıysa metodun geri kalanını devam kodu (continuation) olarak kaydeder ve çağırana hemen döner; Task bitince devam kodu çalıştırılır. Derleyici bunu bir state machine'e çevirir. Hata varsa `await` onu Task içinden çıkarıp fırlatır.

### 6. Senkron bir uygulamayı async'e nasıl dönüştürürsün?
Bloklayan API'leri async karşılıklarıyla değiştiririm (`ReadAllTextAsync`, `GetStringAsync`), metotları `async Task` yapıp `Async` son eki eklerim, **çağıranları da `await` ederim** (zincir giriş noktasına kadar uzar: async all the way), bağımsız işleri `WhenAll` ile birlikte beklerim ve `CancellationToken` eklerim. Senkron kodu `Task.Run`'a sarmak sahte async'tir: bir thread'i başka bir thread ile değiştirir.

### 7. Async all the way ne demek?
Çağrı zincirinin tamamı (controller'dan veri erişimine kadar) async olmalı ve her seviye `await` etmelidir. Ortada senkron köprü (`.Result`, `.Wait()`) deadlock/starvation riski getirir. Giriş noktaları da async olabilir: `async Task Main`, ASP.NET Core endpoint'leri.

### 8. `Thread.Sleep` ile `await Task.Delay` farkı nedir?
`Thread.Sleep` thread'i bloklar; `await Task.Delay` zamanlayıcı kurup thread'i serbest bırakır. Async kodda `Thread.Sleep` kullanmak async'in kazancını yok eder.

### 9. `Task.WhenAny` ile zaman aşımı nasıl yapılır? Sorun nedir?
`await Task.WhenAny(iş, Task.Delay(timeout))` ile hangisi önce biterse ona bakılır. Sorun: zaman aşımı kazansa bile asıl iş arkada çalışmaya devam eder; gerçek iptal için `CancellationToken` gerekir. Daha okunaklısı `task.WaitAsync(TimeSpan)` (`TimeoutException`) veya `CancellationTokenSource.CancelAfter`.

### 10. `Task` ile `ValueTask` farkı nedir?
`ValueTask<T>` struct'tır; sonuç çoğu zaman senkron hazır olan sıcak yollarda `Task` tahsisini önler. Dezavantajı: yalnızca bir kez `await` edilmeli, `.Result` hemen kullanılmamalı vb. kısıtlar vardır. Varsayılan `Task`; `ValueTask` yalnızca ölçümle gerekçelendiğinde.

### 11. `ConfigureAwait(false)` ne yapar?
`await` sonrası devam kodunun yakalanan SynchronizationContext'e dönmesini engeller (havuz thread'inde devam eder). Kütüphane kodunda context'e dönüş maliyetini ve deadlock riskini azaltır. ASP.NET Core'da context olmadığı için uygulama kodunda genellikle gerekmez (derin konu → M4.2).

### 12. `IAsyncEnumerable<T>` ne işe yarar? `Task<List<T>>`'den farkı?
Veriyi **geldikçe** asenkron üretir/tüketir (`await foreach`). `Task<List<T>>` tüm sonuç bitene kadar bekler ve hepsini bellekte tutar; `IAsyncEnumerable<T>` ilk sonucu hemen işler ve bellek kullanımını düşürür. İptal için üreticide `[EnumeratorCancellation]`, tüketicide `WithCancellation(token)` kullanılır.

### 13. `await using` ve `IAsyncDisposable` ne zaman gerekir?
Kaynağı temizlemenin kendisi asenkron I/O gerektiriyorsa (buffer flush, bağlantı/kanal kapatma). `await using` blok sonunda `DisposeAsync`'i await eder; senkron `Dispose`'a sarmak thread'i bloklar.

### 14. Fire-and-forget (`_ = DoAsync()`) neden sorunlu? Alternatifi nedir?
Task'ın hatası ve tamamlanması kimse tarafından gözlenmez (hata kaybolur, uygulama kapanırken iş yarım kalabilir). Gerçekten arka planda yapılacaksa hatayı loglayan sarmalayıcı kullanılır ya da iş bir kuyruğa/arka plan servisine (`BackgroundService`, outbox) taşınır (→ M12). Derleyici `CS4014` ile uyarır; bu uyarı hata yapılmalıdır.

### 15. Bir `async` metotta `lock` içinde `await` yapılabilir mi?
Hayır (derleme hatası CS1996): `lock` thread'e bağlıdır, `await` sonrası devam başka thread'de olabilir. Async uyumlu karşılığı `SemaphoreSlim(1, 1)` ve `await WaitAsync()` ile kritik bölge oluşturmaktır.
