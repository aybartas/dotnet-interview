# J3.3 LINQ Temelleri

> 📁 `ConsoleApp/01-junior/03-collections-linq/03-linq-basics/` · Önkoşul: J3.1, J3.2 · Sorular: [`interview-questions.md`](interview-questions.md)

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

**LINQ (Language Integrated Query)**, her türlü veri kaynağını (bellekteki koleksiyon, veritabanı, XML…) aynı operatör kümesiyle sorgulamayı sağlar. `IEnumerable<T>` üzerindeki LINQ (**LINQ to Objects**) `System.Linq.Enumerable` extension method'ları ve **delegate**'lerdir (→ J3.2); `IQueryable<T>` üzerindeki LINQ ise **expression tree** üretir ve bir sağlayıcı (EF Core gibi) tarafından başka bir dile (SQL) çevrilir. Konu 8 bölüm + 20 rapor alıştırması içerir (`dotnet run -- J3.3 <numara>`).

---

## 2. Neden?

- Döngü + `if` + geçici liste yerine **ne istediğinizi** (deklaratif) söylersiniz; kod kısalır ve niyet okunur.
- EF Core, Dapper sonrası projeksiyonlar, API cevap şekillendirme, test verisi — Junior'ın günlük işi LINQ'tur.
- Yanlış kullanımı sessiz bug (ertelenmiş çalışma) ve performans sorunu (çoklu enumeration, `Count() > 0`, istemci tarafı filtreleme) üretir; mülakatın favori konusudur.

---

## 3. Nasıl?

Çalıştırma (`ConsoleApp` klasöründe): `dotnet run -- J3.3` (bölüm listesi), `dotnet run -- J3.3 2` (tek bölüm), `dotnet run -- J3.3 all` (hepsi).

### 3.1 Query syntax vs method syntax ([`Section01`](Sections/Section01_Syntax.cs))

```csharp
var q1 = from p in products where p.Price > 100 orderby p.Price select p.Name;   // query syntax
var q2 = products.Where(p => p.Price > 100).OrderBy(p => p.Price).Select(p => p.Name); // method syntax
```

Derleyici query syntax'ı method syntax'a çevirir; **aynı IL**. Method syntax daha yaygındır ve tüm operatörleri kapsar (`Chunk`, `MaxBy`, `CountBy`… query syntax'ta yoktur). Query syntax `join`, birden çok `from` ve `let` içeren sorgularda daha okunaklı olabilir. Takımda tutarlı kalın; [`Reports.RevenueByCategory`](Reports/Reports.cs) query syntax örneğidir.

### 3.2 Deferred vs immediate execution ⭐ ([`Section02`](Sections/Section02_DeferredExecution.cs))

Sorgu **tanımlandığında çalışmaz**; ancak **enumerate** edildiğinde (`foreach`, `ToList()`, `Count()`, `First()`…) çalışır ve **her enumeration'da yeniden** çalışır:

| | Operatörler |
|---|---|
| **Ertelenmiş** (lazy) | `Where`, `Select`, `SelectMany`, `OrderBy`, `GroupBy`, `Join`, `Take`, `Skip`, `Distinct`, `Zip`, `Chunk`… |
| **Anında** (immediate) | `ToList`, `ToArray`, `ToDictionary`, `ToLookup`, `ToHashSet`, `Count`, `Sum`, `Min`/`Max`, `Average`, `First`, `Single`, `Any`, `All`, `Aggregate`… |

Tuzaklar (hepsi Section02'de çıktıyla gösterilir): (1) tanımdan **sonra** kaynağa eleman eklenirse sorgu onu da görür; (2) lambda içinde yakalanan değişken **enumeration anındaki** değerle okunur (closure, → J3.2); (3) yan etkili `Select` enumerate edilmedikçe hiç çalışmaz; (4) `ToList()` bir **anlık görüntü** alır. Ayrıca operatörler **streaming** (eleman eleman akıtır: `Where`, `Select`, `Take`) veya **buffering** (`OrderBy`, `GroupBy`, `Reverse`, `Join`'in iç tarafı; tüm kaynağı okur) olabilir; `Take(3)` ile streaming zincir erken durur.

### 3.3 Temel operatörler ([`Section03`](Sections/Section03_Operators.cs))

- **Filtre/projeksiyon:** `Where`, `Select`, `SelectMany` (iç içe listeyi düzleştirir), `Distinct`/`DistinctBy`, `OfType`.
- **Sıralama:** `OrderBy`/`ThenBy`/`OrderByDescending`, .NET 7+ `Order()`/`OrderDescending()`; sıralama **stabildir**; ikincil anahtar için `ThenBy` (ikinci `OrderBy` ilkini sıfırlar!).
- **Gruplama/birleştirme:** `GroupBy` (`IGrouping<K,T>`), `Join` (inner), `GroupJoin` (left join'in eski yolu), `ToLookup` (anında, bulunmayan anahtara boş döner), `ToDictionary` (yinelenen anahtarda `ArgumentException`).
- **Aggregate:** `Count`, `Sum`, `Average`, `Min`/`Max`, `MinBy`/`MaxBy` (elemanı döner, anahtarı değil), `Any`, `All`, `Aggregate`.
- **Bölme:** `Take`, `Skip`, `TakeWhile`, `SkipWhile`, `Chunk`, `Zip`; sayfalama = `OrderBy` + `Skip((page-1)*size)` + `Take(size)`.
- **Küme:** `Union`, `Intersect`, `Except`/`ExceptBy`, `Concat`.

### 3.4 `First` / `FirstOrDefault` / `Single` / `SingleOrDefault` ⭐ ([`Section04`](Sections/Section04_FirstSingle.cs))

| | 0 eleman | 1 eleman | >1 eleman |
|---|---|---|---|
| `First` | **exception** | döner | ilkini döner |
| `FirstOrDefault` | `default` (referans tipte `null`!) | döner | ilkini döner |
| `Single` | **exception** | döner | **exception** |
| `SingleOrDefault` | `default` | döner | **exception** |

`Single` "tam olarak bir tane **olmalı**" iddiasıdır (iş kuralı/veri bütünlüğünü doğrular); `First` "bir tane yeter" der. `Single` ikinci eşleşmeyi görene kadar tüm diziyi tarar, `First` ilk eşleşmede durur. `FirstOrDefault(defaultValue)` (.NET 6+) özel varsayılan verir; `null` sonucu kontrol edilmelidir.

### 3.5 `Any()` vs `Count() > 0` ve çoklu enumeration ([`Section05`](Sections/Section05_AnyCountMultipleEnumeration.cs))

- `Any()` ilk elemanda durur; `Count()` her şeyi sayar (kaynak `ICollection` ise O(1), değilse O(n)). "Var mı?" için **`Any()`**.
- `IEnumerable<T>` parametresini bir metotta birden çok kez dolaşmak (`if (x.Any()) … x.Count() … foreach`) kaynağı (iterator, DB sorgusu, dosya) **her seferinde yeniden çalıştırır**; analyzer uyarısı `CA1851`. Çözüm: bir kez `ToList()`/`ToArray()` ile somutlaştırın veya parametreyi `IReadOnlyCollection<T>` yapın. `TryGetNonEnumeratedCount` kaynağı dolaşmadan sayıyı bulmayı dener.
- Kaynak bir iterator ise sıra/yan etki bozulabilir; rastgele veya her seferinde değişen kaynaklarda (`Random`, `DateTime.Now`) iki enumeration **farklı** sonuç verir.

### 3.6 Yeni operatörler ([`Section06`](Sections/Section06_NewOperators.cs))

- **(.NET 9)** `CountBy(keySelector)` → `GroupBy(...).Select(g => (g.Key, g.Count()))` yerine, grup nesneleri oluşturmadan; `AggregateBy(key, seed, func)` anahtar başına birikim; `Index()` → `(Index, Item)` çiftleri (`Select((x, i) => …)` yerine).
- **(.NET 10)** `LeftJoin` / `RightJoin` — eşleşmeyen satırı tutan **gerçek** outer join; öncesinde `GroupJoin` + `SelectMany` + `DefaultIfEmpty` gerekiyordu ([`Reports.CustomersWithoutOrders`](Reports/Reports.cs)). Eşleşmeyen tarafın değeri `default` (referans tipte `null`) gelir.

### 3.7 `IEnumerable<T>` vs `IQueryable<T>` ⭐ ([`Section07`](Sections/Section07_EnumerableVsQueryable.cs))

| | `IEnumerable<T>` | `IQueryable<T>` |
|---|---|---|
| Operatör parametresi | `Func<T,bool>` (**delegate**, derlenmiş kod) | `Expression<Func<T,bool>>` (**veri**, ağaç) |
| Nerede çalışır | Bellekte, uygulama sürecinde | Sağlayıcıya çevrilir (EF Core → SQL), kaynakta çalışır |
| Tipik kaynak | `List<T>`, dizi, `yield` | `DbSet<T>`, `AsQueryable()` |

`dbContext.Products.Where(p => p.Price > 100)` → `WHERE Price > 100` SQL'ine çevrilir ve yalnız sonuç gelir. Araya **`AsEnumerable()`/`ToList()`** girerse kalan operatörler bellekte çalışır ve **tüm tablo** çekilir (istemci tarafı filtreleme tuzağı). `Expression<Func<>>` içine her C# metodu yazılamaz (çevrilemeyen çağrı çalışma zamanında hata verir). Ayrıntı: EF Core sorgu çevirisi → J9.4, `IQueryable` ve expression tree derinliği → M1.3.

### 3.8 20 rapor alıştırması ([`Section08`](Sections/Section08_Reports.cs), [`Reports.cs`](Reports/Reports.cs), [`ShopData.cs`](Data/ShopData.cs))

6 müşteri, 10 ürün, 12 sipariş (teslim edilen cirosu **$5,030**, iptal oranı %16,7, hiç satılmamış ürün, hiç sipariş vermemiş müşteri) üzerinde: en çok satan ürün, kategori/müşteri/ay bazlı ciro, sayfalama, `Chunk`, `LeftJoin`, `CountBy`, top-1-per-group… **Önce metodun yorumundaki soruyu okuyup kendi sorgunuzu yazın**, sonra `dotnet run -- J3.3 8` çıktısıyla (sonuçlar veri setinden elle doğrulanmıştır; ciro toplamı $5,030) karşılaştırın.

---

## 4. Gerçek dünya senaryosu

**"Rapor ekranı 20 saniyede açılıyor."** Kod `db.Orders.ToList().Where(o => o.Date > from)` yazıyor: milyonlarca satır belleğe çekilip filtreleniyor. `Where` `ToList()`'ten önceye alınınca SQL'e gidiyor ve 200 ms'ye iniyor.

**"Aynı liste ekranda bazen farklı çıkıyor."** Servis `IEnumerable<Order>` döndürüyor, çağıran `Count()` ve sonra `foreach` yapıyor; kaynak her seferinde veritabanına gidiyor ve iki çağrı arasında veri değişiyor. `ToList()` ile tek seferde somutlaştırınca tutarlı.

**"Yeni eklenen ürün raporda yok."** Sorgu, uygulama açılışında bir listeye bağlı olarak tanımlanıp saklanmış; eski bir anlık görüntü (`ToList()`) kullanılmış. Hangi noktada anlık görüntü alınacağı bilinçli karar olmalıdır.

---

## 5. Mülakat perspektifi

Sık sorulanlar: **Deferred execution nedir, ne tuzaklar üretir?** ⭐ · **`First` vs `Single`** ⭐ · **`IEnumerable` vs `IQueryable`** ⭐ · `Any()` vs `Count() > 0` · Çoklu enumeration · `Select` vs `SelectMany` · `GroupBy` sonucu nedir? · `ToList()` ne zaman çağrılır? · `Join` vs `GroupJoin` · `OrderBy` stabil mi?

**İpucu:** Deferred execution'ı **tanım → kanıt (sayaç/log) → sonuç (anlık görüntü gerekirse ToList, çoklu enumeration)** sırasıyla anlatın; `IQueryable` sorusunda "delegate vs expression tree, SQL'e çevrilme, `AsEnumerable()` ile bellekte çalışma" üçlüsünü söyleyin.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| `ToList()`'i `Where`'den önce çağırmak (EF) | Filtre `IQueryable` üzerinde, `ToList()` en sonda |
| `if (list.Count() > 0)` | `Any()` (veya koleksiyonsa `Count` özelliği) |
| `IEnumerable<T>` parametresini birkaç kez dolaşmak | Somutlaştırın / `IReadOnlyCollection<T>` isteyin |
| Sorgu tanımlayıp kaynağı değiştirmeyi unutmak | Anlık görüntü gerekiyorsa `ToList()` |
| Yan etkili `Select` ile "iş yapmak" | `foreach`; LINQ sorgudur, komut değil |
| `Single()` ile "ilkini al" demek | `First()`; `Single` yalnızca tek olması bir kuraldıysa |
| `FirstOrDefault()` sonucunu null kontrol etmemek | `is null` kontrolü / `?? throw` |
| `OrderBy(a).OrderBy(b)` | `OrderBy(a).ThenBy(b)` |
| `ToDictionary`'de yinelenen anahtarı unutmak | `GroupBy` / `ToLookup` |
| Döngü içinde `list.First(x => …)` (O(n²)) | Önce `ToDictionary`/`ToLookup` |
| Karmaşık sorguyu tek satırda yazmak | Ara değişken/yerel fonksiyon, okunabilirlik |
| EF sorgusunda çevrilemeyen C# metodu çağırmak | Çevrilebilir ifade yaz veya `AsEnumerable()` sonrasına taşı (bilinçli) |

---

## 7. Performans & trade-off

- LINQ to Objects, düz `for` döngüsünden genelde yavaştır (delegate çağrısı, iterator nesneleri, closure tahsisi). **Sıcak yolda** (milyonlarca eleman, sıkı döngü) ölçüp döngüye dönün; geri kalan her yerde okunabilirlik kazanır. Ölçüm → J11.
- **Buffering** operatörler (`OrderBy`, `GroupBy`) belleği O(n) tutar; yalnızca en büyük/küçük için `OrderBy().First()` yerine `MinBy`/`MaxBy` (O(n), tahsissiz) kullanın.
- `Count() > 0` yerine `Any()`; `list.Count()` yerine `list.Count` (zaten `Count()` `ICollection` için optimize edilmiştir, ama niyet açıktır).
- Zincirdeki her ertelenmiş operatör bir iterator nesnesidir; derin zincirler tahsis üretir (→ S2.3).
- **Somutlaştırma (`ToList`) trade-off'u:** tutarlılık + tek çalıştırma vs bellek. Büyük veri için akış (`IAsyncEnumerable`, → J4) veya sayfalama.
- `IQueryable` sorgularında `Select` ile yalnız gereken sütunları çekmek ağ + bellek tasarrufudur (→ J9.4).

---

## 8. Kaynaklar

- LINQ genel bakış: <https://learn.microsoft.com/dotnet/csharp/linq/>
- Standart sorgu operatörleri: <https://learn.microsoft.com/dotnet/csharp/linq/standard-query-operators/>
- Sorgu yürütme (deferred/immediate): <https://learn.microsoft.com/dotnet/csharp/linq/get-started/introduction-to-linq-queries#query-execution>
- Query syntax ve method syntax: <https://learn.microsoft.com/dotnet/csharp/linq/get-started/query-syntax-and-method-syntax-in-linq>
- `Enumerable` sınıfı: <https://learn.microsoft.com/dotnet/api/system.linq.enumerable>
- .NET 9 yenilikleri (`CountBy`, `AggregateBy`, `Index`): <https://learn.microsoft.com/dotnet/core/whats-new/dotnet-9/libraries#linq>
- .NET 10 yenilikleri (`LeftJoin`, `RightJoin`): <https://learn.microsoft.com/dotnet/core/whats-new/dotnet-10/libraries>
- CA1851 (çoklu enumeration): <https://learn.microsoft.com/dotnet/fundamentals/code-analysis/quality-rules/ca1851>
- Expression ağaçları: <https://learn.microsoft.com/dotnet/csharp/advanced-topics/expression-trees/>
