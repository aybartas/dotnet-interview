# J3.3 LINQ Temelleri — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan. Modül (J3) sorularının tamamı için J3.1 ve J3.2 dosyalarına da bakın.

### 1. ⭐ Deferred execution (ertelenmiş çalışma) nedir?
LINQ sorgusu tanımlandığında çalışmaz; yalnızca **ne yapılacağını** tutan bir iterator nesnesi oluşur. Sorgu `foreach`, `ToList()`, `Count()`, `First()` gibi bir şey onu **enumerate** ettiğinde çalışır ve **her enumeration'da baştan** çalışır. Sonuçları: kaynak tanımdan sonra değişirse yeni hali görülür, yakalanan değişkenler enumeration anındaki değerleriyle okunur, yan etkili `Select` enumerate edilmedikçe hiç çalışmaz. Anlık görüntü gerekiyorsa `ToList()`/`ToArray()` ile somutlaştırılır.

### 2. ⭐ Hangi operatörler ertelenmiş, hangileri anında çalışır?
**Ertelenmiş:** `Where`, `Select`, `SelectMany`, `OrderBy`, `GroupBy`, `Join`, `Take`, `Skip`, `Distinct`, `Zip`, `Chunk`. **Anında:** `ToList`/`ToArray`/`ToDictionary`/`ToLookup`/`ToHashSet`, `Count`, `Sum`, `Average`, `Min`/`Max`, `First`/`Single`, `Any`/`All`, `Aggregate`. Ayrıca ertelenmiş operatörler **streaming** (`Where`, `Select`, `Take`: eleman eleman) veya **buffering** (`OrderBy`, `GroupBy`, `Reverse`: tüm kaynağı okur) olabilir.

### 3. Aşağıdaki kod ne yazdırır? `var q = list.Where(x => x > 2); list.Add(10); Console.WriteLine(q.Count());`
Sorgu `Count()` çağrıldığında çalıştığı için `10` eklenmiş hâliyle işlenir; 10 > 2 olduğundan sayıya dahildir. `Where` çağrıldığı anda filtrelenmedi. (Anlık görüntü isteseydik `list.Where(...).ToList()` yazardık.)

### 4. ⭐ `First` ile `Single` farkı nedir? `OrDefault` ne değiştirir?
`First` ilk elemanı döner (boşsa exception); `Single` **tam olarak bir** eleman olmasını ister (0 veya >1 ise exception). `FirstOrDefault`/`SingleOrDefault` boşken exception yerine `default` döner (referans tipte `null`, bu yüzden kontrol gerekir); ama `SingleOrDefault` birden fazla eşleşmede **yine exception** atar. `Single` bir iş kuralını/veri bütünlüğünü iddia eder (ör. e-posta ile kullanıcı); `First` "birkaç olabilir, biri yeter" demektir. Performans: `First` ilk eşleşmede durur, `Single` ikinci eşleşmeyi aramak için diziyi sonuna kadar tarar.

### 5. ⭐ `Any()` ile `Count() > 0` farkı?
`Any()` ilk elemanda durur; `Count()` kaynak `ICollection` değilse tüm diziyi sayar (O(n)). "Eleman var mı?" için `Any()` hem daha hızlı hem niyeti daha açıktır; EF Core'da `EXISTS` olarak çevrilir. Koleksiyon zaten `List`/dizi ise `Count` özelliği O(1)'dir.

### 6. ⭐ Çoklu enumeration problemi nedir?
`IEnumerable<T>` bir parametre olarak alınıp birden çok kez dolaşılırsa (`Any()`, sonra `Count()`, sonra `foreach`) kaynak **her seferinde yeniden çalışır**: iterator tekrar yürür, DB sorgusu tekrar gider, dosya yeniden okunur; kaynak değişkense sonuçlar tutarsız olabilir. Çözüm: bir kez `ToList()`/`ToArray()` ile somutlaştırmak ya da API'de `IReadOnlyCollection<T>`/`IReadOnlyList<T>` istemek. Analyzer kuralı `CA1851`.

### 7. ⭐ `IEnumerable<T>` ile `IQueryable<T>` farkı nedir?
`IEnumerable<T>` operatörleri **delegate** (`Func<>`) alır ve bellekte çalıştırır. `IQueryable<T>` operatörleri **expression tree** (`Expression<Func<>>`) alır; sağlayıcı (EF Core) ağacı yorumlayıp SQL'e çevirir ve filtre/sıralama/sayfalama **veritabanında** çalışır. `AsEnumerable()`/`ToList()` araya girerse sonrası bellekte çalışır ve veri önce çekilir; yanlış yerde kullanılırsa tüm tablo belleğe gelir. `IQueryable` içinde çevrilemeyen C# metodu çalışma zamanında hata verir (→ J9.4, M1.3).

### 8. ⭐ `Select` ile `SelectMany` farkı?
`Select` her elemanı **bir** sonuca eşler (1:1, iç içe liste kalır: `IEnumerable<List<T>>`). `SelectMany` her elemanı bir **diziye** eşleyip sonuçları **tek bir diziye düzleştirir** (1:N → `IEnumerable<T>`). Örnek: siparişlerden tüm kalemleri almak için `orders.SelectMany(o => o.Items)`.

### 9. `GroupBy` ne döner? `ToLookup` ile farkı?
`IEnumerable<IGrouping<TKey, T>>` döner; her grup bir `Key` ve elemanlarıdır. `GroupBy` **ertelenmiştir**, `ToLookup` **anında** çalışır ve anahtarla indekslenebilir bir `ILookup` verir (bulunmayan anahtara boş dizi döner, exception atmaz). `ToDictionary` ise anahtar tekrarında `ArgumentException` atar.

### 10. `Join`, `GroupJoin` ve `LeftJoin` farkı nedir?
`Join` inner join'dir (eşleşmeyenler düşer). `GroupJoin` her sol elemana eşleşen sağ elemanlar **koleksiyonu** verir (eşleşme yoksa boş); `SelectMany` + `DefaultIfEmpty` ile left join yapılırdı. **.NET 10** ile gelen `LeftJoin`/`RightJoin` doğrudan outer join'dir; eşleşmeyen tarafın değeri `default` olur. EF Core'da da bu operatörlerle çevrilir.

### 11. ⭐ `List` ile `HashSet` farkı, ne zaman hangisi? (Modül sorusu → J3.1)
`List<T>` sıralı, indekslenebilir, tekrara izin verir; `Contains` O(n). `HashSet<T>` sırasız, tekrarsız, `Contains` ortalama O(1) (`GetHashCode`/`Equals`'a dayanır). Çok `Contains` sorgusu, tekrar elemek ve küme işlemleri (`Union`/`Intersect`) için `HashSet`; sıra/indeks gerektiğinde `List`. LINQ'te `list.Where(x => other.Contains(x))` içinde `other` büyükse `HashSet` olmalı, aksi hâlde O(n²).

### 12. ⭐ Closure nedir, döngüde neden sorun çıkarır? (Modül sorusu → J3.2)
Lambda, dış değişkeni **değeriyle değil değişkenin kendisiyle** yakalar. `for (var i…)` döngüsünde tek bir `i` vardır; lambdalar çalıştırıldığında hepsi son değeri görür. Çözüm: döngü-yerel kopya veya `foreach` (her iterasyonda yeni değişken). LINQ'te de önemlidir: ertelenmiş sorgu yakalanan değişkeni **enumeration anında** okur.

### 13. `OrderBy` stabil midir? `ThenBy` neden gerekir?
Evet, LINQ sıralaması **stabildir** (eşit anahtarlar özgün sıralarını korur). İkinci sıralama ölçütü için `ThenBy`/`ThenByDescending` kullanılır; `.OrderBy(a).OrderBy(b)` yazılırsa ikincisi birincisini **geçersiz kılar** (yalnız `b`'ye göre sıralar).

### 14. `MaxBy` ile `Max` farkı? Sadece en büyüğü almak için `OrderBy().First()` yazılır mı?
`Max(selector)` en büyük **değeri**, `MaxBy(selector)` o değere sahip **elemanı** döner (.NET 6+). `OrderBy(...).First()` tüm dizinin sıralanmasını (O(n log n), buffer) gerektirir; `MaxBy`/`MinBy` tek geçişte O(n) çözer.

### 15. Sayfalama LINQ ile nasıl yapılır? Nelere dikkat edilir?
`query.OrderBy(x => x.Date).Skip((page - 1) * size).Take(size)`. **Mutlaka deterministik bir sıralama** (`OrderBy` + tekilleştirici `ThenBy(Id)`) gerekir, yoksa sayfalar arası tekrar/atlama olur. Çok büyük offset'lerde `Skip` yavaşlar (DB'de keyset/seek sayfalama tercih edilir → J9.4). `Chunk(n)` ise bellekteki diziyi n'li gruplara böler (toplu işlem).

### 16. .NET 9/10'da gelen LINQ yenilikleri nelerdir?
**.NET 9:** `CountBy` (grup nesnesi oluşturmadan sayım), `AggregateBy` (anahtar başına birikim), `Index` (`(Index, Item)` çiftleri). **.NET 10:** `LeftJoin`, `RightJoin`. Öncesinde bunlar `GroupBy().Select(g => (g.Key, g.Count()))`, `Select((x, i) => …)` ve `GroupJoin + DefaultIfEmpty` ile yazılırdı.

### 17. Query syntax ile method syntax farkı nedir? Hangisini kullanırsın?
Derleyici query syntax'ı method syntax'a çevirir; ikisi aynı IL'e iner. Method syntax daha yaygın ve tüm operatörleri kapsar (`Chunk`, `MaxBy`…); query syntax `join`, `let` ve çoklu `from` içeren sorgularda okunaklı olabilir. Takım standardına göre tutarlı kullanırım; çoğunlukla method syntax.

### 18. LINQ'te `ToList()`'i ne zaman çağırırsın, ne zaman çağırmazsın?
**Çağırırım:** sorguyu bir kez çalıştırıp sonucu birden çok kez kullanacaksam, anlık görüntü (tutarlılık) gerekiyorsa, DB bağlantısı/`DbContext` ömrü bitmeden veriyi almam gerekiyorsa. **Çağırmam:** `IQueryable` üzerinde daha filtre/sıralama eklenecekse (önce çevrilmeli), yalnızca ilk eleman/`Any` gerekiyorsa, çok büyük veri akışında (bellek). Kural: filtreyi `ToList()`'ten **önce** yap.

### 19. LINQ her zaman `for` döngüsünden yavaş mıdır? Ne zaman döngüye dönersin?
LINQ to Objects delegate çağrısı, iterator ve closure tahsisleri yüzünden genelde biraz yavaştır ve sıcak yolda fark edilir; ama çoğu uygulamada darboğaz değildir. Önce **ölçerim** (profil/BenchmarkDotNet → J11), sıcak ve büyük döngüde fark çıkarsa `for`/`Span<T>` ile elle yazarım; diğer her yerde okunabilirlik tercih edilir.

### 20. Bir lambda içinde yan etki (ör. log, sayaç artırma) kullanmak neden kötü fikirdir?
Sorgu ertelenmiş ve her enumeration'da yeniden çalıştığı için yan etki **hiç, bir kez veya birden çok kez** çalışabilir; çalışma sırası/sayısı optimizasyonlara (`Take`, kısa devre, `Count` kısayolları) bağlıdır. LINQ **sorgu** içindir; iş yapmak için `foreach` kullanılır.
