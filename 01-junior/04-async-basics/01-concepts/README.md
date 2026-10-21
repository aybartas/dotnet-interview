# J4.1 Temel Kavramlar

> 📁 `01-junior/04-async-basics/01-concepts/` · Önkoşul: J2, J3 · Sorular: [`interview-questions.md`](interview-questions.md)

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

Async kodu doğru yazmak için önce dört ayrımı netleştirmek gerekir: **process / thread / task**, **concurrency / parallelism**, **CPU-bound / I/O-bound** ve **blocking / non-blocking**. Bunlar karışınca "her şeyi `Task.Run`'a sarayım" ya da "async yaptım ama hâlâ yavaş" gibi hatalar doğar. Bu konunun kodu [`Sections/`](Sections/) altında 6 bölümdür (`dotnet run -- 4.1 <numara>`, J04 modül klasöründe).

---

## 2. Neden?

- Yanlış zihinsel model, yanlış araç seçtirir: CPU işine `async`, I/O işine `Parallel.For` uygulamak ikisi de işe yaramaz.
- Bir web sunucusunun kapasitesi **thread sayısıyla** değil **bekleyen iş sayısıyla** ölçülür; bunu anlamak J4'ün geri kalanının (async/await, iptal) gerekçesidir.
- "Concurrency ile parallelism farkı?" mülakatın en sık sorusudur; thread safety (paylaşılan state) de aynı kavram setinin devamıdır.

---

## 3. Nasıl?

### 3.1 Process vs thread vs task ([`Section01`](Sections/Section01_ProcessThreadTask.cs))

| Kavram | Ne? | Maliyet |
|--------|-----|---------|
| **Process** | İşletim sisteminin izole ettiği çalışan program: kendi bellek alanı, handle'lar | Ağır; başlatması/kapatması pahalı |
| **Thread** | Bir process içindeki yürütme akışı; **aynı belleği paylaşır** | ~1 MB stack + context switch; sayısı sınırlı |
| **Task** | Gelecekte bitecek bir işin **temsili** (promise); `Task<T>` sonucu da taşır | Küçük nesne; çoğu zaman **hiç thread gerektirmez** |

- **Thread pool:** Hazır thread'ler havuzda tutulur ve yeniden kullanılır. `Task.Run` işi havuza atar; `new Thread` her seferinde yeni OS thread'i açar (nadiren gerekir).
- **Task ≠ thread:** `Task.FromResult` hiçbir thread kullanmaz; `Task.Delay` bir zamanlayıcıdır; `TaskCompletionSource` dış bir olayı temsil eder. 1000 `Task.Delay` 1000 thread açmaz (demoda thread sayısı 2 → 4).

### 3.2 Concurrency vs parallelism ⭐ ([`Section02`](Sections/Section02_ConcurrencyVsParallelism.cs))

- **Concurrency (eşzamanlılık):** Birden çok işin **aynı zaman diliminde ilerlemesi**. Tek çekirdek/thread yeter; işler dönüşümlü ilerler (bir iş beklerken diğeri çalışır). Konu **yapı ve yönetimle** ilgilidir.
- **Parallelism (paralellik):** Birden çok işin **gerçekten aynı anda** çalışması. Birden çok çekirdek gerekir. Konu **hızla (throughput)** ilgilidir.

Demoda üç asenkron "aşçı" **tek thread'de** dönüşümlü ilerler (≈300 ms, sıralı olsa ≈900 ms): concurrency var, parallelism yok. `Parallel.For` ile CPU işi çekirdeklere bölününce gerçek paralellik görülür (4 çekirdekte 136 ms → 54 ms).

```
Concurrency (tek thread):   A─ ─B─ ─C─ ─A─ ─B─ ─C─      (dönüşümlü ilerler)
Parallelism (3 çekirdek):   A───────►
                            B───────►                    (aynı anda)
                            C───────►
```

### 3.3 CPU-bound vs I/O-bound ([`Section03`](Sections/Section03_CpuVsIoBound.cs))

| | CPU-bound | I/O-bound |
|---|-----------|-----------|
| Darboğaz | İşlemci (hesap, sıkıştırma, görüntü işleme) | Bekleme (ağ, disk, veritabanı) |
| Çözüm | Daha çok çekirdek: `Parallel.For`, PLINQ, `Task.Run` | Beklerken thread tutmamak: `async`/`await` |
| `async` yardım eder mi? | **Hayır** — iş senkron çalışır, çağıranı bloklar | **Evet** — thread serbest kalır |

> **`async` bir metodu başka thread'e taşımaz.** Demoda `async` olarak işaretli ama `await` içermeyen CPU metodu çağıranı tam hesap süresi boyunca bloklar; `Task.Run` ise hemen döner. ASP.NET Core'da CPU işini `Task.Run` ile istek thread'inden **aynı havuza** atmak da kazandırmaz.

### 3.4 Blocking vs non-blocking ([`Section04`](Sections/Section04_BlockingVsNonBlocking.cs))

- **Blocking:** İşin bitmesini beklerken thread **uyur/meşgul bekler** (`Thread.Sleep`, `.Result`, `.Wait()`, senkron dosya/HTTP). Thread başka iş yapamaz.
- **Non-blocking:** Beklerken thread **serbest bırakılır** (`await`); iş bitince devam kodu bir havuz thread'inde çalışır.

Neden web sunucusunda thread bloklamak pahalı?

1. Her thread ~1 MB stack tutar; binlerce thread GB'larca bellek + context switch demektir.
2. Havuz, minimum sayıdan sonra thread'i **yavaş** ekler (≈500 ms'de bir). Ani yükte bloklayan istekler kuyrukta bekler → **thread pool starvation**: CPU %5, ama sunucu cevap vermiyor.
3. Demo: 16 eşzamanlı 250 ms'lik "DB çağrısı" — `Thread.Sleep` ile ≈1000 ms, `await Task.Delay` ile ≈250 ms.

### 3.5 Thread safety'ye giriş ([`Section06`](Sections/Section06_RaceCondition.cs))

Thread'ler belleği paylaşır; **paylaşılan değiştirilebilir state** yarış durumu (race condition) doğurur. `counter++` üç adımdır (oku, artır, yaz): iki thread aynı değeri okursa bir artırma kaybolur.

| Yaklaşım | Sonuç (4 thread × 1M) | Not |
|----------|----------------------|-----|
| `counter++` | ≈2,5M (**yanlış**, her çalıştırmada farklı) | Race condition |
| `Interlocked.Increment` | 4M ✓ | Atomik CPU komutu; basit sayaç/bayrak için |
| `lock` | 4M ✓ | Kritik bölge; yavaş (rekabet varsa) |
| Thread başına yerel toplam + birleştirme | 4M ✓ (en hızlı) | **Paylaşımı ortadan kaldır** |

`async`/`await` thread safety **vermez**: `await` sonrası devam kodu başka thread'de çalışabilir ve aynı state'e dokunabilir. Derin konu (bellek modeli, lock-free) → M4, S3.

### 3.6 🛠 Aynı 10 HTTP çağrısı ([`Section05`](Sections/Section05_TenHttpCalls.cs))

Yerel bir Kestrel API'sine (`LocalApi`, internet gerekmez) 10 çağrı × 200 ms gecikme:

| Yöntem | Süre | Yorum |
|--------|------|-------|
| `for` + `await` (sıralı) | ≈2000 ms | Her çağrı öncekini bekler |
| `Task.WhenAll` | ≈210 ms | Hepsi aynı anda yolda, thread tutulmaz — **I/O için doğru araç** |
| `Parallel.For` + `.GetResult()` | ≈290 ms ama **10 thread uyutuldu** | Yanlış araç: CPU paralelliği içindir; thread'leri bloklar |
| `Parallel.ForEachAsync` (maks. 5) | ≈415 ms | Async gövde + **eşzamanlılık sınırı** (sunucuyu boğmamak için) |

---

## 4. Gerçek dünya senaryosu

**"Canlıda yük artınca API donuyor ama CPU %8."** Servis, her istekte `repository.GetAll().Result` ve `httpClient.GetStringAsync(...).Result` çağırıyor. Düşük yükte sorun yok; yük artınca her istek bir havuz thread'ini bloklayıp bekliyor, havuz saniyede yalnızca 1–2 thread ekleyebiliyor → istekler kuyrukta birikiyor (starvation). Çözüm: zinciri baştan sona `async` yapmak; I/O sırasında thread serbest kalınca aynı sunucu 10× eşzamanlı isteği kaldırıyor.

**"Rapor sayfası 40 sorgu atıyor, 8 saniye sürüyor."** Sorgular birbirinden bağımsız ama sıralı `await`'lenmiş. Bağımsız olanlar `Task.WhenAll` ile birlikte başlatılınca süre en yavaş sorgu kadar olur (eşzamanlılık sınırı için `Parallel.ForEachAsync`).

**"Ziyaretçi sayacı 1M ziyarette 700K gösteriyor."** `static int visits; visits++` birden çok istek thread'inden çağrılıyor. `Interlocked.Increment` (veya `ConcurrentDictionary`/metrik kütüphanesi) ile düzeldi.

---

## 5. Mülakat perspektifi

Sık sorulanlar: **Concurrency ile parallelism farkı?** ⭐ · **CPU-bound iş için `async` yeterli mi?** · Thread ile Task farkı · Neden web sunucusunda thread bloklamak kötü? · Race condition nedir, nasıl çözülür? · `Task.Run` ne zaman kullanılır? · Thread pool nedir?

**İpucu:** Concurrency sorusunu bir **benzetmeyle** yanıtlayın ("tek aşçı üç yemeği dönüşümlü pişirir / üç aşçı aynı anda pişirir") ve ardından .NET karşılığını söyleyin: *async/await = concurrency (I/O), Parallel/PLINQ = parallelism (CPU)*.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| I/O işine `Parallel.For` + `.Result` uygulamak | `await Task.WhenAll(...)` / `Parallel.ForEachAsync` |
| "async yaparsam CPU işi hızlanır" | CPU işi → `Parallel`/PLINQ; async yalnızca beklemeyi serbest bırakır |
| ASP.NET Core'da CPU işini `Task.Run`'a sarıp `await` etmek | Kazandırmaz (aynı havuz); gerçekten ağır işi kuyruğa/arka plan servisine taşıyın |
| Bağımsız çağrıları sıralı `await`'lemek | Hepsini başlat → `WhenAll` |
| Sınırsız `WhenAll` (10.000 çağrı) | Eşzamanlılığı sınırla (`Parallel.ForEachAsync`, `SemaphoreSlim`) |
| Paylaşılan sayaç/liste/dictionary'yi kilitsiz güncellemek | `Interlocked`, `lock`, `Concurrent*` koleksiyonlar veya paylaşımı kaldırmak |
| Thread sayısını artırarak yavaşlığı çözmeye çalışmak (`SetMinThreads`) | Kök neden bloklayan çağrıdır; onu async yapın |

---

## 7. Performans & trade-off

- **Thread maliyeti:** ~1 MB stack + context switch; 1000 thread ≈ 1 GB. `async` bekleyen iş başına yalnızca küçük bir state machine nesnesi tutar.
- **Parallel.For ek yükü:** Çok küçük işlerde bölme/birleştirme maliyeti kazancı yer; iş en az mikro-/milisaniye düzeyinde olmalı. Kazanç çekirdek sayısıyla (Amdahl yasası) sınırlıdır.
- **`lock` vs `Interlocked`:** Rekabet varsa `lock` bekletir (demoda ≈6× yavaş); tek sayaç için `Interlocked`; mümkünse paylaşımı kaldır.
- **Eşzamanlılık sınırı:** Sınırsız paralellik bağımlı servisi (DB, API) boğar; sınır koymak sistemi **daha hızlı** yapabilir.
- Ölçüm → J11 (profiling); thread pool iç yapısı ve starvation analizi → M4.3.

---

## 8. Kaynaklar

- Asenkron programlama (genel bakış): <https://learn.microsoft.com/dotnet/csharp/asynchronous-programming/>
- Task-based Asynchronous Pattern (TAP): <https://learn.microsoft.com/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap>
- Managed thread pool: <https://learn.microsoft.com/dotnet/standard/threading/the-managed-thread-pool>
- Parallel programlama (`Parallel`, PLINQ): <https://learn.microsoft.com/dotnet/standard/parallel-programming/>
- `Parallel.ForEachAsync`: <https://learn.microsoft.com/dotnet/api/system.threading.tasks.parallel.foreachasync>
- Thread safety ve `Interlocked`: <https://learn.microsoft.com/dotnet/standard/threading/managed-threading-best-practices>
- Stephen Toub, "How Async/Await Really Works in C#": <https://devblogs.microsoft.com/dotnet/how-async-await-really-works/>
