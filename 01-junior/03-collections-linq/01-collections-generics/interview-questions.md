# J3.1 Collections & Generics — Mülakat Soruları

> Teori: [README.md](README.md) · Pratik: [src/](src/) · Test: [tests/](tests/). ⭐ = çok sık sorulan.

### 1. ⭐ `List<T>` ile `HashSet<T>` farkı nedir? Ne zaman hangisi?
`List<T>` sıralı, tekrara izin veren, indeksle erişilen dinamik dizidir; `Contains` O(n) tarar. `HashSet<T>` tekrarsız kümedir, hash tablosuyla `Add`/`Contains` ortalama O(1) çalışır ama **sıra/indeks garantisi yoktur**, küme işlemleri (`Union`, `Intersect`, `Except`, `IsSubsetOf`) sunar. Sıra veya indeks gerekiyorsa ya da eleman az ve tek seferlik tarama yapılıyorsa `List`; "var mı?" sorusu çok sorulacaksa veya tekrarsızlık gerekiyorsa `HashSet`. Bedeli: bellek, hash hesabı ve elemanın `Equals`/`GetHashCode` sözleşmesine uyması.

### 2. Dictionary nasıl çalışır? Ortalama ve en kötü durum süreleri nedir?
Anahtarın `GetHashCode()` değeri bir kovayı (bucket) belirler; kovadaki zincirde `Equals` ile eşleşme aranır. İyi dağılan hash ile ekleme/arama ortalama O(1)'dir; tüm anahtarlar aynı kovaya düşerse (kötü hash) O(n)'e düşer. Dolulukta kapasite büyür ve yeniden hash'lenir. Sıralama garantisi yoktur. (İç yapının teorisi → D2.)

### 3. ⭐ Bir sınıfı Dictionary anahtarı yapacaksam nelere dikkat etmeliyim?
(1) `Equals` ve `GetHashCode` birlikte override edilir ve tutarlıdır (`a.Equals(b)` ⇒ aynı hash); (2) ikisi de **aynı ve değişmez** alanlardan türer — hash'e giren alan koleksiyondayken değişirse kayıt bulunamaz/silinemez ("hayalet kayıt"); (3) hash iyi dağılmalı (sabit hash O(n) yapar). Pratikte string/int/Guid/enum/`record`/`readonly record struct` kullanılır. Yalnızca `Equals` override edilirse eşit anahtarlar farklı kovaya gidip bulunamaz.

### 4. `Dictionary` indexer ile `TryGetValue` farkı? `TryAdd`, `GetValueOrDefault` ne işe yarar?
Indexer ile eksik anahtar okumak `KeyNotFoundException` fırlatır; `TryGetValue` `bool` döner ve değeri `out` ile verir (tek aramada kontrol + okuma, exception yok). `TryAdd` anahtar zaten varsa `false` döner (indexer atama üzerine yazar, `Add` `ArgumentException` fırlatır). `GetValueOrDefault(key, varsayılan)` eksikse varsayılanı verir.

### 5. `IEnumerable<T>`, `ICollection<T>`, `IList<T>` ve `IReadOnlyList<T>` farkı nedir?
`IEnumerable<T>` yalnızca gezinme sağlar (en az varsayım). `ICollection<T>` sayma ve ekleme/silme/`Contains` ekler (değiştirilebilir). `IList<T>` indeksle okuma/yazma, `Insert`, `RemoveAt`, `IndexOf` ekler. `IReadOnlyCollection<T>`/`IReadOnlyList<T>` sayma ve indeksle **okuma** sunar, değiştirme yok. Parametre olarak en genel arayüz, dönüş olarak değiştirilemez arayüz tercih edilir.

### 6. ⭐ Public API'de neden `List<T>` yerine `IReadOnlyList<T>` döndürülür?
Dahili listeyi dışarı vermek, çağıranın koleksiyonu (`Clear`, `Add`) değiştirip sınıfın invariant'ını bozmasına izin verir. Read-only arayüz encapsulation sağlar, niyeti tipte gösterir ve iç temsili sonradan (List → Array/ImmutableArray) kırıcı olmadan değiştirmeyi mümkün kılar. Not: `IReadOnlyList` bir sözleşmedir, `List<T>`'e cast ile delinebilir; gerçek koruma için `AsReadOnly()`, kopya veya immutable koleksiyon kullanılır.

### 7. `List<T>` kapasitesi nasıl büyür? `new List<T>(capacity)` ne kazandırır?
Dahili dizi dolunca 2 katı büyüklükte yeni dizi ayrılıp elemanlar kopyalanır (0 → 4 → 8 → 16…); bu yüzden `Add` amortize O(1)'dir. Boyut biliniyorsa kapasiteyi vermek ara dizileri ve kopyaları ortadan kaldırır, GC baskısını düşürür (1 milyon int için ~8 MB yerine ~4 MB tahsis). `Capacity ≠ Count`; `Clear` kapasiteyi küçültmez, `TrimExcess` küçültür.

### 8. `Array` ile `List<T>` farkı nedir?
Array sabit boyutludur (boyut oluşturulurken verilir), `Span<T>` ile iyi çalışır, en düşük ek yük; `List<T>` dinamik boyutludur, `Add`/`Remove`/`Insert` sunar ve arkada bir dizi tutar. Boyut sabit ve performans kritikse array; değişkense `List<T>`.

### 9. `foreach` nasıl çalışır? Neden gezinirken koleksiyonu değiştiremeyiz?
Derleyici `GetEnumerator()`, `MoveNext()`/`Current` ve `Dispose()` çağrılarına çevirir; gezinme durumu enumerator'dadır. `List<T>` gibi koleksiyonlar her değişiklikte iç `_version` sayacını artırır; enumerator `MoveNext`'te bunu kontrol eder ve değişmişse `InvalidOperationException` fırlatır. Çözüm: `RemoveAll`, kopya (`ToList()`) üzerinde gezinmek, ya da sondan başa `for`. `List<T>.Enumerator` struct'tır (doğrudan foreach tahsis etmez); `IEnumerable<T>` üzerinden kutulanır.

### 10. `LinkedList<T>` ne zaman kullanılır? Neden nadiren?
Bilinen bir düğümün önüne/arkasına O(1) ekleme/silme gerektiğinde (LRU cache gibi). Ancak indeksle erişim yoktur, her düğüm ayrı tahsis edilir ve bellekte dağınıktır; cache yerelliği düşük olduğu için pratikte çoğu senaryoda `List<T>` daha hızlıdır. Ölçmeden `LinkedList` seçilmez.

### 11. `Queue`, `Stack` ve `PriorityQueue` farkı nedir?
`Queue<T>` FIFO (`Enqueue`/`Dequeue`; iş kuyruğu, BFS); `Stack<T>` LIFO (`Push`/`Pop`; undo, DFS, ifade ayrıştırma); `PriorityQueue<TElement,TPriority>` en küçük öncelik değerine sahip elemanı önce çıkarır (min-heap, `Enqueue`/`Dequeue` O(log n)); eşit öncelikte sıra garanti değildir. Maksimum öncelik için ters çeviren karşılaştırıcı verilir.

### 12. `SortedDictionary`, `SortedSet` ne zaman kullanılır?
Anahtara/değere göre **sıralı gezinme** veya **aralık sorgusu** (`GetViewBetween`, `Min`/`Max`) gerektiğinde; arama/ekleme O(log n)'dir (kırmızı-siyah ağaç). Sıra gerekmiyorsa `Dictionary`/`HashSet` daha hızlıdır.

### 13. ⭐ Generics neden vardır?
(1) Tip güvenliği derleme zamanında sağlanır (`List<int>`'e string eklenemez); (2) value type'lar için boxing ortadan kalkar (`ArrayList` her int'i kutular) ve JIT her değer tipi için özelleşmiş kod üretir; (3) tek algoritmayı her tip için yazarak kod tekrarı önlenir ve cast gerekmez.

### 14. Generic constraint'ler nelerdir? `where T : class`, `struct`, `new()`, `notnull` ne anlama gelir?
`class` yalnızca referans tipleri (null atanabilir), `struct` null olmayan değer tiplerini, `new()` parametresiz ctor'u (böylece `new T()` yazılabilir), `notnull` null olabilen tip argümanını (ör. `Dictionary` anahtarı), arayüz/taban sınıf kısıtı (`IComparable<T>`) ise o tipin üyelerinin çağrılabilmesini garanti eder. Constraint derleyiciye T hakkında bilgi verir; kısıtsız `T`'de yalnızca `object` üyeleri kullanılır.

### 15. Generic `Repository<T>` yazarken hangi kısıtları koyarsın?
`where T : class, IEntity<TId>` (referans tip ve Id'si olan varlık) ve `where TId : notnull` (Id sözlük anahtarı olacağı için). Böylece aynı sınıf `Product`/`int` ve `Customer`/`Guid` için cast'siz ve tip güvenli çalışır; yanlış varlık eklemek derleme hatasıdır. Dışarıya `IReadOnlyList<T>` döner.

### 16. `IEnumerable<T>` ile `List<T>` döndürmek arasında nasıl seçersin?
Dönüş tipi çağıranın ihtiyacını karşılayan en dar sözleşme olmalıdır: yalnızca gezinme için `IEnumerable<T>`, sayma/indeks gerekiyorsa `IReadOnlyList<T>`. `List<T>` iç temsili sızdırır. Ancak `IEnumerable<T>` ertelenmiş (lazy) olabilir ve birden çok kez gezilirse maliyetli olabilir; sonuç hazırsa `IReadOnlyList<T>`.

### 17. Büyük bir listede çok sayıda "var mı?" sorgusu yapılacak. Ne yaparsın?
Elemanları bir kez `HashSet<T>`'e (veya anahtar-değer gerekiyorsa `Dictionary`) koyarım; her sorgu ortalama O(1) olur. Veri sıralıysa ve aralık sorgusu gerekiyorsa `BinarySearch`/`SortedSet`. Okuma-ağırlıklı ve bir kez kurulan veride `FrozenSet`. Eleman az ise `List` yeterlidir; gerçek veriyle ölçerim.

### 18. `Dictionary`'nin sırasına güvenebilir miyim?
Hayır; sıra tanımsızdır (uygulama ayrıntısı olarak çoğu zaman ekleme sırasına benzer ama silme sonrası değişir). Sıra gerekiyorsa `SortedDictionary`, `OrderBy` ya da `OrderedDictionary<TKey,TValue>` (.NET 9) kullanılır.
