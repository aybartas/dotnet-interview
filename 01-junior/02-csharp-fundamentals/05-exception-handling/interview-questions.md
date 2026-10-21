# J2.5 Exception Handling — Mülakat Soruları

> Teori: [README.md](README.md) · Pratik: [src/](src/) · Test: [tests/](tests/). ⭐ = çok sık sorulan.

### 1. ⭐ `throw` ile `throw ex` arasındaki fark nedir?
`throw;` yakalanmış exception'ı **orijinal stack trace'iyle** yeniden fırlatır. `throw ex;` ise exception nesnesini yeniden fırlatıp stack trace'i **catch noktasından başlatır**: hatanın gerçekte doğduğu metot ve satır kaybolur, hata sanki catch bloğunun bulunduğu metotta doğmuş gibi görünür; üretimde teşhisi çok zorlaştırır. CA2200 analyzer'ı bunu yakalar. Bağlam eklemek istiyorsan `throw new X("mesaj", ex)` (orijinal `InnerException` olarak korunur); başka yerden/thread'den orijinal stack'le fırlatmak için `ExceptionDispatchInfo.Capture(ex).Throw()`.

### 2. `try/catch/finally` nasıl çalışır? `finally` her zaman çalışır mı?
Önce `try` çalışır; exception fırlarsa ilk eşleşen `catch` (yukarıdan aşağı, özelden genele) çalışır; `finally` hata olsun olmasın, hatta `return`'den sonra bile çalışır. Çalışmadığı durumlar: process'in aniden ölmesi (`Environment.FailFast`, StackOverflow, güç kesintisi), sonsuz döngü/blok. `finally` içinde `return`/`throw` yazmak bekleyen exception'ı yutar veya ezer; yalnızca temizlik yapılmalı.

### 3. `using` ile `try/finally` arasındaki ilişki nedir?
`using` (blok veya declaration) `IDisposable` için `try { … } finally { x.Dispose(); }` şekerlemesidir; exception olsa bile kaynak bırakılır. `catch` bloğundan **önce** Dispose çalışır (stack çözülürken). Asenkron kaynaklar için `await using` / `IAsyncDisposable`.

### 4. ⭐ Exception filter (`when`) nedir? Neden catch içinde `if` yazmaktan farklıdır?
`catch (X ex) when (koşul)` yalnızca koşul doğruysa catch'i devreye sokar; yanlışsa exception yakalanmamış gibi yoluna devam eder. Filter **stack çözülmeden önce** (iç `finally`'lerden önce) değerlendirilir: debugger/dump'ta fırlatma noktasının stack'i ve yerel değişkenleri korunur, yeniden fırlatma maliyeti ve stack riski yoktur. Kullanım: HTTP status/hata kodu ayrımı, retry'lanabilir hata, `InnerException` tipi, "logla ama yakalama" (filter içinde log + `false`).

### 5. ⭐ `catch (Exception)` ne zaman meşrudur?
Yalnızca **sınırlarda** ve ne yapacağınız belliyse: (1) uygulamanın en üst seviyesi / global handler (ASP.NET Core exception middleware → 500 + log), (2) arka plan döngüsü/worker'da tek bir iş hatası tüm servisi öldürmesin diye (logla, dead-letter, sıradaki işe geç), (3) üçüncü parti/plugin çağrı sınırı, (4) temizlik yapıp `throw;` ile geçirmek, (5) CLI'da kullanıcıya anlamlı mesaj ve doğru exit code. Her yerde genel catch, hataları gizler.

### 6. Hangi exception'ları yakalamamalısın?
Kendi bug'ın olan hatalar (`NullReferenceException`, `IndexOutOfRangeException`, `InvalidCastException`, çağıranın `ArgumentNullException`'ı): bunlar düzeltilmeli, susturulmamalı. Ne yapacağını bilmediğin hatalar: yakalayıp yutmak yerine yukarı bırak. Kurtarılamaz durumlar: `OutOfMemoryException`, `StackOverflowException` (yakalanamaz, process ölür). Ayrıca boş `catch { }` yazma.

### 7. Custom exception ne zaman yazılır? Nasıl yazılır?
(1) Çağıranın exception **tipine göre farklı davranması** gerektiğinde, (2) **ek veri** (hesap no, eksik tutar) taşımak gerektiğinde. Yalnızca farklı bir mesaj için tip üretilmez; hazır tipler (`ArgumentException`, `InvalidOperationException`, …) yeterliyse onlar kullanılır. Kurallar: adı `Exception` ile biter, `Exception`'dan türer (`ApplicationException` değil), standart ctor'ları sağlar (mesaj, mesaj+inner), ek bilgi salt okunur property'dir, hiyerarşi sığ tutulur (bir domain kök exception'ı + birkaç alt tip).

### 8. `InvalidOperationException` ile `ArgumentException` ve `NotSupportedException` arasında nasıl seçim yaparsın?
`ArgumentException` (ve türevleri `ArgumentNullException`, `ArgumentOutOfRangeException`) **parametre geçersiz**; `InvalidOperationException` **nesne bu durumda bu işlemi yapamaz** (ör. kapalı bağlantıda sorgu, boş koleksiyonda `First()`); `NotSupportedException` işlem **hiç desteklenmiyor** (salt okunur koleksiyona `Add`). Doğru tipi seçmek çağıranın hatayı doğru yorumlamasını sağlar.

### 9. ⭐ Exception'ın maliyeti nedir? Kontrol akışı için neden kullanılmaz?
Fırlatma pahalıdır (stack trace yakalama, iki geçişli stack çözme, filter/finally işletme): deneyde `Parse + catch` `TryParse`'tan kabaca ~50× yavaştı. `try` bloğu exception yokken neredeyse ücretsizdir; maliyet fırlatma sıklığındadır. Ayrıca beklenen durumları exception ile modellemek okunabilirliği bozar, debugger'da first-chance gürültüsü yaratır ve izleme/alarm sistemlerini yanlış tetikler. Beklenen başarısızlık için `TryParse`/`TryGetValue`/`Result`/`null`; beklenmeyen ihlal için exception.

### 10. Exception mi, `Result` mı döndürmeli?
Beklenmeyen, programcı/ortam kaynaklı ve nadir hatalarda (DB koptu, bug) exception; beklenen iş sonuçlarında (doğrulama, "kayıt yok", yetersiz bakiye) ve sık gerçekleşenlerde `Result`/Try deseni. `Result` imzada başarısızlığı gösterir ve çağıranı ele almaya zorlar, ucuzdur; ama her katmanda taşıma boilerplate'i getirir. Pratikte ikisi birlikte kullanılır: domain `Result` döner, API sınırı HTTP koduna çevirir. (→ M11.6)

### 11. `Parse` ile `TryParse` farkı ve ne zaman hangisi?
`Parse` geçersiz girdide exception fırlatır, `TryParse` `bool` döner. Kullanıcı/dış girdi gibi hatanın **beklendiği** yerlerde `TryParse` (exception maliyetinden ve kontrol akışı kötüye kullanımından kaçınır); girdinin garantili doğru olduğu yerde `Parse` kabul edilebilir.

### 12. `async` metottaki exception nerede yakalanır?
`async` metot çağrıldığında exception **fırlatılmaz**, `Task`'ın içine yazılır (`Faulted`); `await` (veya `Wait()`/`Result`) edildiğinde fırlatılır. `try/catch` içinde `await` etmezseniz yakalanmaz. `async void` metottaki exception ise hiç yakalanamaz ve süreci çökertebilir; bu yüzden `async void` yalnızca event handler için kullanılır.

### 13. `await` ile `.Wait()`/`.Result` exception davranışı farkı nedir?
`await` orijinal exception'ı (ilk iç exception) açıp fırlatır; `.Wait()`/`.Result` ise `AggregateException` içine sarar. Ayrıca `.Wait()`/`.Result` senkron bloklama olduğu için deadlock riski taşır (→ J4); mümkünse her zaman `await`.

### 14. `AggregateException` nedir? `Task.WhenAll` hataları nasıl ele alır?
Birden çok exception'ı tek nesnede toplayan tiptir (`InnerExceptions`). `Task.WhenAll` birden çok task başarısız olursa `await`'te yalnızca **ilk** exception'ı fırlatır; hepsi `task.Exception.InnerExceptions` içindedir. `Flatten()` iç içe aggregate'leri düzleştirir, `Handle()` bilinenleri işleyip kalanları yeniden fırlatır.

### 15. `OperationCanceledException` hata mıdır?
Hayır, iptal normal bir akıştır (`CancellationToken`). Genellikle hata olarak loglanmaz/yeniden denenmez, ayrı yakalanır (`catch (OperationCanceledException)`); `TaskCanceledException` ondan türer.

### 16. Exception'ı loglarken neye dikkat edersin?
`ex.Message` değil **exception nesnesinin kendisini** logla (`logger.LogError(ex, "bağlam")`) ki stack trace ve inner exception'lar kaybolmasın. Aynı hatayı her katmanda logla-ve-fırlat yapma (log gürültüsü); sınırda bir kez logla. Hassas veriyi (parola, token) mesaja koyma.

### 17. `InnerException` ne işe yarar? Ne zaman kullanılır?
Bir hatayı daha üst seviyeli (domain) bir hataya çevirirken orijinal hatayı korumak için: `throw new OrderRepositoryException("Sipariş yüklenemedi", ex)`. Böylece çağıran anlamlı bir tip alır, teşhis eden kişi kök nedene ve orijinal stack'e ulaşır. Orijinali atlamak bilgi kaybıdır.

### 18. `StackOverflowException` yakalanabilir mi?
Hayır; .NET'te process'i sonlandırır. Önlem: özyinelemeyi sınırlamak (derinlik kontrolü), döngüye çevirmek. `OutOfMemoryException` teorik olarak yakalanabilir ama çoğu zaman anlamlı bir kurtarma mümkün değildir.

### 19. Guard clause nedir? Çağıranın hatasını neden yakalamak yerine reddedersin?
Metodun başında geçersiz girdiyi erken ve net reddetme (`ArgumentNullException.ThrowIfNull(x)`, `ArgumentOutOfRangeException.ThrowIfNegative(n)`). Çağıranın hatası (null geçmek) çağıran tarafında düzeltilmesi gereken bir bug'dır; erken ve açık bir hata mesajı ile tespit edilir, daha derinde gizemli `NullReferenceException`'a dönüşmez.

### 20. Global exception handling ASP.NET Core'da nasıl yapılır?
En dış katmanda tek bir noktada: exception handling middleware (`UseExceptionHandler`) veya `IExceptionHandler` + `ProblemDetails`. Domain exception'lar 4xx'e (404/409/422), beklenmeyen exception'lar 500'e çevrilir ve loglanır; kullanıcıya iç ayrıntılar sızdırılmaz. Böylece controller/servis kodunda try/catch yağmuru olmaz. (→ J6.4, J7.3)
