# J4.1 Temel Kavramlar — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. ⭐ Concurrency ile parallelism farkı nedir?
**Concurrency**, birden çok işin **aynı zaman diliminde ilerlemesidir** (tek çekirdekte dönüşümlü olabilir; bir iş beklerken diğeri çalışır). **Parallelism** ise birden çok işin **gerçekten aynı anda** çalışmasıdır ve birden çok çekirdek ister. Benzetme: tek aşçının üç yemeği dönüşümlü pişirmesi concurrency, üç aşçının aynı anda pişirmesi parallelism'dir. .NET'te `async/await` I/O için concurrency, `Parallel.For`/PLINQ CPU işi için parallelism sağlar.

### 2. ⭐ CPU-bound bir iş için `async` yeterli mi?
Hayır. `async` işi başka thread'e taşımaz; `await` içermeyen bir `async` metot çağıranın thread'inde senkron çalışır ve onu bloklar. CPU-bound iş için çekirdekler arasında bölme gerekir (`Parallel.For`, PLINQ) ya da UI/arka plan senaryosunda `Task.Run` ile başka bir thread'e alma. ASP.NET Core'da `Task.Run` istek thread'iyle aynı havuzu kullandığı için kazandırmaz; ağır iş arka plan servisine/kuyruğa taşınır (→ M12).

### 3. Process, thread ve task arasındaki fark nedir?
Process, işletim sisteminin izole ettiği çalışan programdır (kendi bellek alanı). Thread, process içindeki yürütme akışıdır; aynı belleği paylaşır ve pahalıdır (~1 MB stack, context switch). Task ise gelecekteki bir işin temsilidir (promise); bir thread üzerinde çalışabilir ya da hiç thread kullanmayabilir (`Task.Delay`, I/O beklemesi, `Task.FromResult`).

### 4. Thread pool nedir, neden var?
Thread yaratmak pahalı olduğu için hazır thread'ler bir havuzda tutulur ve iş geldikçe yeniden kullanılır. `Task.Run`, `Parallel`, ASP.NET Core istekleri ve async devam kodları havuzdan çalışır. Havuz minimum sayıya kadar hemen thread verir; sonrasında yavaş (≈500 ms'de bir) ekler. Bu yüzden havuz thread'lerini bloklamak ani yükte **starvation** yaratır.

### 5. ⭐ Neden bir web sunucusunda thread bloklamak pahalıdır?
Her bloklanan istek bir thread'i bekleterek tutar. Thread sayısı sınırlıdır ve havuz yeni thread'i yavaş ekler; yük arttıkça istekler kuyrukta birikir (thread pool starvation) ve sunucu "CPU düşük ama cevap yok" durumuna gelir. Ayrıca her thread ~1 MB bellek tutar. Non-blocking (`await`) I/O beklerken thread'i serbest bırakır; aynı thread sayısıyla çok daha fazla eşzamanlı istek işlenir.

### 6. CPU-bound ile I/O-bound iş arasındaki fark nedir? Hangisinde ne kullanılır?
CPU-bound işte darboğaz işlemcidir (hesaplama, sıkıştırma); çözüm daha çok çekirdek kullanmaktır (`Parallel`, PLINQ). I/O-bound işte darboğaz beklemedir (ağ, disk, veritabanı); çözüm beklerken thread tutmamaktır (`async/await`). Yanlış eşleştirme (I/O'ya `Parallel.For`, CPU'ya `async`) fayda sağlamaz.

### 7. Aynı 10 bağımsız HTTP çağrısını nasıl yaparsın? Sıralı, `WhenAll` ve `Parallel.For` farkı nedir?
Sıralı `await` toplam süresi çağrıların toplamıdır (10×200 ms = 2 sn). `Task.WhenAll` hepsini aynı anda başlatır, süre en yavaş çağrı kadardır (~200 ms) ve beklerken thread tutmaz. `Parallel.For` CPU işi içindir; içinde bloklayan çağrı (`.Result`) yapılırsa thread'ler uyutulur. Eşzamanlılığı sınırlamak gerekiyorsa async gövdeyi destekleyen `Parallel.ForEachAsync` (veya `SemaphoreSlim`) kullanılır.

### 8. ⭐ Race condition nedir? Bir örnek ve çözüm yolları söyle.
Birden çok thread'in paylaşılan değiştirilebilir state'e kilitsiz eriştiği ve sonucun zamanlamaya bağlı olduğu durumdur. Örnek: `counter++` üç adımdır (oku, artır, yaz); iki thread aynı değeri okursa bir artırma kaybolur. Çözüm sırası: paylaşımı kaldır (thread başına veri/immutable), atomik işlem (`Interlocked`), `lock`, thread-safe koleksiyonlar (`Concurrent*`). Hata "bazen" olduğu için testte yakalanmaz, yük altında patlar.

### 9. `Interlocked` ile `lock` farkı nedir?
`Interlocked` tek bir değişken üzerinde atomik CPU komutlarıyla çalışır (artırma, takas, karşılaştır-ve-değiştir); bloklamaz, çok hızlıdır ama yalnızca basit işlemleri kapsar. `lock` birden çok işlemden oluşan bir kritik bölgeyi korur; rekabet varsa diğer thread'leri bekletir. Basit sayaç/bayrak için `Interlocked`, birden çok alanı tutarlı güncelleme için `lock`.

### 10. `async/await` thread safety sağlar mı?
Hayır. `await` sonrası devam kodu (context yoksa) başka bir havuz thread'inde çalışabilir ve eşzamanlı başka işler aynı paylaşılan state'e dokunabilir. Paylaşılan state yine korunmalıdır (`Interlocked`, `lock` yerine async uyumlu `SemaphoreSlim.WaitAsync`; `lock` içinde `await` yapılamaz).

### 11. `Task.Run` ne zaman kullanılır, ne zaman kullanılmaz?
UI uygulamasında CPU-bound işi UI thread'inden uzaklaştırmak için kullanılır. ASP.NET Core'da istek zaten havuz thread'inde çalıştığı için CPU işini `Task.Run`'a sarmak kazandırmaz; I/O işini `Task.Run` ile sarmak ise (sahte async) bir thread'i yine bloklar. Kural: gerçek async API varsa onu `await` et; `Task.Run` yalnızca CPU işini başka thread'e almak için.

### 12. Thread pool starvation nedir, belirtileri nelerdir?
Havuzdaki tüm thread'ler bloklayan işlerde takılı olduğu için yeni işlerin (hatta bloklayanları serbest bırakacak devam kodlarının) çalıştırılamaması durumudur. Belirti: istek gecikmeleri artar, zaman aşımı olur, ama CPU kullanımı düşüktür. Kök neden çoğunlukla `.Result`/`.Wait()` ve senkron I/O'dur; çözüm async all the way, tanı için `dotnet-counters`/`dotnet-dump` (→ M4.3).

### 13. `Task.Delay` ile `Thread.Sleep` farkı nedir?
`Thread.Sleep` bulunduğu thread'i bloklar; `await Task.Delay` zamanlayıcı kurup thread'i serbest bırakır ve süre dolunca devam kodunu çalıştırır. Async kodda `Thread.Sleep` kullanmak async'in amacını bozar.

### 14. Bir sunucuda 1000 eşzamanlı istek var ve her biri 500 ms DB çağrısı bekliyor. Kaç thread gerekir?
Bloklayan (senkron) kodla ~1000 thread gerekir (bellek ve starvation sorunu). Async kodla bekleme sırasında thread tutulmadığı için birkaç thread yeter; toplam süre ≈ 500 ms'dir. Bu fark async'in web sunucuları için asıl değeridir.
