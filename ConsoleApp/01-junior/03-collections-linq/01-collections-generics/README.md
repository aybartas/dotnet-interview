# J3.1 Collections & Generics

> 📁 `ConsoleApp/01-junior/03-collections-linq/01-collections-generics/` · Önkoşul: J2 · Sorular: [`interview-questions.md`](interview-questions.md)

Veri yapılarının **teorisi ve Big-O ispatları DSA pistinde** (→ D2, D3); burada .NET API'lerinin **doğru kullanımı** var.

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

`System.Collections.Generic` içindeki tip güvenli koleksiyonlar ve bunları yazarken/kullanırken bilinmesi gereken sözleşmeler: arayüz hiyerarşisi, kapasite, enumerator, anahtar sözleşmesi ve generics. Konu 8 bölümden oluşur (`dotnet run -- J3.1 <numara>`).

---

## 2. Neden?

- Yanlış koleksiyon, kod doğru çalışsa bile ölçekte yavaşlık demektir (`List.Contains` döngüsü).
- Yanlış anahtar sözleşmesi `Dictionary`'nin sessizce yalan söylemesine yol açar.
- Public API'de `List<T>` sızdırmak sınıfın kendi durumunu koruyamamasıdır.

---

## 3. Nasıl?

### 3.1 Koleksiyon turu ([`Section01`](Sections/Section01_Tour.cs))

| Tip | Ne? | Not |
|-----|-----|-----|
| `List<T>` | Dinamik dizi | İndeksle O(1); sona ekleme amortize O(1); ortaya ekleme/silme O(n); `Contains` O(n) |
| `Dictionary<K,V>` | Hash tablosu | Anahtarla ortalama O(1); **sıra garantisi yok**; indexer eksik anahtarda exception, `TryGetValue`/`GetValueOrDefault`/`TryAdd` değil |
| `HashSet<T>` | Tekrarsız küme | `Add`/`Contains` O(1); `UnionWith`, `IntersectWith`, `ExceptWith`, `IsSubsetOf` |
| `Queue<T>` | FIFO | İş kuyruğu, BFS |
| `Stack<T>` | LIFO | Undo, ifade ayrıştırma, DFS |
| `LinkedList<T>` | Çift bağlı liste | Bilinen düğümün yanına ekleme/silme O(1); indeks yok; bellek ve cache açısından pahalı — pratikte nadiren doğru seçim |
| `SortedDictionary<K,V>` | Anahtara göre sıralı (kırmızı-siyah ağaç) | O(log n); sıralı gezinme hazır |
| `SortedSet<T>` | Sıralı küme | `GetViewBetween` ile aralık sorgusu, `Min`/`Max` |
| `PriorityQueue<TElement,TPriority>` | Min-heap (.NET 6+) | **En küçük** öncelik önce çıkar; eşit önceliklerde sıra garanti değil |

Çok thread'li erişim için `Concurrent*` (→ M4); değişmez koleksiyonlar için `Immutable*`/`Frozen*` (→ M1).

### 3.2 Arayüz hiyerarşisi ve public API'de read-only arayüz ⭐

```
IEnumerable<T>                    sadece gezinme
  └ IReadOnlyCollection<T>        + Count
      └ IReadOnlyList<T>          + [i] okuma
ICollection<T>                    + Count, Add, Remove, Contains, Clear   (değiştirilebilir)
  └ IList<T>                      + [i] okuma/yazma, Insert, RemoveAt, IndexOf
```
**Kural:** **parametre** olarak en genel arayüz (ihtiyaç duyulan minimum: `IEnumerable<T>`), **dönüş** olarak değiştirilemez arayüz (`IReadOnlyList<T>`, `IReadOnlyDictionary<K,V>`).

**Neden read-only döndürülür?** (1) Encapsulation — sınıf kendi invariant'ını (tekrarsızlık, sıra, doğrulama) tek elden korur; (2) niyet tipte okunur; (3) iç temsili (List → Array/ImmutableArray) sonradan kırıcı olmadan değiştirebilirsiniz.

[`Inventory.cs`](Models/Inventory.cs) dört seviyeyi gösterir:

| Seviye | Koruma |
|--------|--------|
| `List<T>` döndürmek (`LeakyInventory`) | Yok — çağıran `Clear()` yapabilir |
| `IReadOnlyList<T>` (`ItemsView`) | **Sözleşme** — `List<T>`'e cast edilerek delinir |
| `AsReadOnly()` (`ReadOnlyCollection<T>`) | Gerçek sarmalayıcı — cast edilse de `Add` → `NotSupportedException` |
| Kopya `[.. _items]` (`Snapshot()`) | Anlık görüntü; sonraki değişiklikleri görmez |
| `ImmutableList`/`ImmutableArray` | Gerçek değişmezlik (→ M1) |

Canlı görünüm ile anlık görüntü arasında seçim ayrıca bir tasarım kararıdır.

### 3.3 `Array` vs `List<T>`: kapasite ([`Section03`](Sections/Section03_Capacity.cs))

`List<T>` arkada bir dizi tutar; dolunca **2 katı** yeni dizi ayırıp kopyalar (`0 → 4 → 8 → 16 → 32…`; amortize O(1) `Add`). Boyutu biliyorsanız `new List<T>(capacity)`: 1 milyon `int` eklemede 8,0 MB yerine 3,8 MB tahsis (ara diziler yok). `Capacity ≠ Count`; `Clear()` kapasiteyi küçültmez, `TrimExcess()` küçültür. Array: boyut sabit ve bilinen, performans kritik (`Span<T>`, buffer) durumlar; API dönüşünde ikisi de iç temsili sızdırır.

### 3.4 `foreach` altında `IEnumerator<T>`

`foreach` → `GetEnumerator()`, `MoveNext()`/`Current`, `Dispose()`. Gezinme durumu **enumerator'dadır**, koleksiyonda değil (iki `foreach` bağımsızdır). `List<T>.Enumerator` bir **struct**'tır: doğrudan `foreach` 0 byte tahsis eder, `IEnumerable<T>` olarak tutulursa kutulanır (deneyde 40 B). **Gezinirken koleksiyonu değiştirmek** `InvalidOperationException` verir (iç `_version` kontrolü); çözüm `RemoveAll` veya kopya üzerinde gezinmek. `yield return` ile kendi iterator'ınızı yazarsınız (derleyici state machine üretir; derin hali → M1.4).

### 3.5 Arama süresi ⭐ ([`Section05`](Sections/Section05_LookupBenchmark.cs))

100.000 elemanda 1.000 arama (yarısı yok): `List.Contains` ≈ 14 ms, `BinarySearch` ≈ 1 ms, `HashSet.Contains` ≈ 0,4 ms, `Dictionary.ContainsKey` ≈ 0,3 ms. Sonuç aynı; fark, `List`'in her aramada baştan tarayıp hash yapılarının n'den bağımsız yaklaşık sabit süreli olmasıdır. Küçük n'de (~<20) fark yok; hash yapısı kurma maliyeti (bellek + O(n)) çok sayıda arama ile amorti olur. Ölçüm kabadır (BenchmarkDotNet → S2.1).

### 3.6 Dictionary anahtar sözleşmesi ⭐ ([`Section06`](Sections/Section06_KeyContract.cs))

1. `a.Equals(b)` ⇒ `a.GetHashCode() == b.GetHashCode()`.
2. Hash ve `Equals` **aynı** ve **değişmez** alanlara dayanmalı.
3. Hash iyi dağılmalı.

| İhlal | Sonuç |
|-------|-------|
| Yalnızca `Equals` override (hash farklı) | Eşit anahtar **bulunamaz** (farklı kova) |
| Yalnızca `GetHashCode` override (`Equals` referans) | Aynı kova ama eşit sayılmaz — **bulunamaz** |
| **Sabit hash** (`=> 1`) | Çalışır ama tüm anahtarlar tek kovada → **O(n)** (5.000 arama: 0,3 ms → 131 ms, ~460×) |
| **Mutable key** (hash değişkenden) | `ContainsKey`/`Remove` false, kayıt koleksiyonda "hayalet" kalır (→ J2.4) |

`string` anahtarda varsayılan karşılaştırma **ordinal** (büyük/küçük harf duyarlı); farklı davranış için tipi değiştirmeden `IEqualityComparer<T>` verilir (`StringComparer.OrdinalIgnoreCase`). Anahtar olarak `string`, `int`, `Guid`, enum, `record`, `readonly record struct` kullanın.

### 3.7 Generics ([`Section07`](Sections/Section07_Generics.cs))

**Neden var?** (1) **Tip güvenliği** derleme zamanında (`List<int>`'e string eklenemez; `ArrayList`'e eklenir ve çalışırken patlar), (2) **boxing'siz performans** (→ J2.2), (3) **kod tekrarının önlenmesi** (tek algoritma, her tip için; JIT her değer tipi için özelleşmiş kod üretir).

| Constraint | Anlamı | Ne kazandırır? |
|------------|--------|-----------------|
| `where T : class` / `struct` | Referans / null olmayan değer tipi | `null` atama / `T?` = `Nullable<T>` |
| `where T : notnull` | null olabilen tip kabul edilmez | `Dictionary<T,…>` anahtarı |
| `where T : new()` | Parametresiz ctor | `new T()` |
| `where T : IComparable<T>` / `BaseClass` | Arayüz/taban sınıf | O üyeleri çağırabilirsiniz (`CompareTo`) |
| `where T : unmanaged` | Referanssız struct | Unsafe/interop |

Kısıtsız `T` hakkında yalnızca `object` üyeleri bilinir; aşırı kısıtlamak yeniden kullanılabilirliği düşürür. **`InMemoryRepository<T,TId> where T : class, IEntity<TId> where TId : notnull`** taslağı: aynı sınıf `Product`/`int` ve `Customer`/`Guid` için, cast'siz ve tip güvenli (`products.Add(customer)` derlenmez). Variance (`in`/`out`) ve generic math → M1.2.

### 3.8 Hangi koleksiyon ne zaman? ([`Section08`](Sections/Section08_DecisionTable.cs))

| İhtiyaç | Seçim |
|---------|-------|
| Sıralı liste, indeksle (varsayılan) | `List<T>` |
| Sabit boyut, ham hız | `T[]` |
| "Var mı?" / tekrarsız | `HashSet<T>` |
| Anahtar → değer | `Dictionary<K,V>` |
| Anahtarla sıralı gezinme / sıralı + aralık | `SortedDictionary<K,V>` / `SortedSet<T>` |
| FIFO / LIFO / öncelikli | `Queue<T>` / `Stack<T>` / `PriorityQueue<E,P>` |
| Düğüm ortasına sık ekleme (düğüm biliniyor) | `LinkedList<T>` (nadir) |
| Çok thread / değişmez | `Concurrent*` (→ M4) / `Immutable*`, `Frozen*` (→ M1) |

**Kısa kural:** şüphede `List<T>`; anahtarla arıyorsanız `Dictionary`; "var mı?" soruyorsanız `HashSet`.

---

## 4. Gerçek dünya senaryosu

**"Rapor 10 dakika sürüyor."** 200.000 satırlık dosya, her satır için `existingIds.Contains(id)` (`List<int>`) → O(n²) ≈ 40 milyar karşılaştırma. `HashSet<int>` ile dakikalar saniyeye iner; kod tek satır değişti.

**"Dictionary'de olan kullanıcıyı bulamıyoruz."** `UserKey` sınıfı yalnızca `Equals`'ı override etmiş; `GetHashCode` varsayılan (referans tabanlı) → farklı nesneler farklı kovaya gider. Çözüm `record` (veya `Equals` + `GetHashCode`); kalıcı önlem anahtar tiplerini `record` yapmak ve sözleşme için test yazmak (bu konudaki [`Keys.cs`](Models/Keys.cs) tuzakları tek tek gösterir).

**"Müşteri listesini başka bir modül boşalttı."** `public List<Customer> Customers` dışarı açılmış; başka bir servis `Clear()` çağırdı. `IReadOnlyList<Customer>` döndürmek + ekleme için sınıfın kendi metodu (invariant: aynı Id iki kez eklenemez).

---

## 5. Mülakat perspektifi

Sık sorulanlar: **`List` ile `HashSet` farkı, ne zaman hangisi?** ⭐ · Dictionary nasıl çalışır? · `Equals`/`GetHashCode` sözleşmesi, mutable key · `IEnumerable` ↔ `ICollection` ↔ `IList` farkı · Neden `IReadOnlyList` döndürülür? · `List` kapasite büyümesi · Generics neden? · Constraint'ler · `Queue` vs `Stack` · `PriorityQueue` hangi sırada çıkarır? · `foreach` içinde silme neden hata verir?

**İpucu:** "HashSet daha hızlı" demeyin; **neden** (hash ile kovaya gitme vs tarama), **ne zaman değil** (küçük n, sıra gerekli) ve **bedeli** (bellek, hash sözleşmesi) söyleyin.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| Döngü içinde `list.Contains` | `HashSet`/`Dictionary` |
| `Dictionary` indexer'ı ile varlık kontrolü | `TryGetValue` / `TryAdd` / `GetValueOrDefault` |
| Yalnızca `Equals` override etmek | `Equals` + `GetHashCode` (veya `record`) |
| Mutable nesneyi anahtar yapmak | Immutable anahtar |
| Public API'de `List<T>` dönmek | `IReadOnlyList<T>`; gerçek koruma için `AsReadOnly()`/kopya |
| `IReadOnlyList`'i "değişmez" sanmak | Sözleşmedir; cast ile delinir |
| Boyutu bilinen listeyi capacity'siz büyütmek | `new List<T>(capacity)` |
| `foreach` içinde koleksiyonu değiştirmek | `RemoveAll` / kopya üzerinde gez |
| `LinkedList`'i "ekleme hızlı" diye seçmek | Önce `List`; ölç |
| `Dictionary` sırasına güvenmek | Sıra garantisi yok → `SortedDictionary` / `OrderBy` |
| `ArrayList`/`Hashtable` kullanmak | Generic karşılıkları |
| Her şeye `where T : class` koymak | Gerçekten gereken constraint |

---

## 7. Performans & trade-off

- **Hash yapıları** hızlı arama için bellek ve hash hesabı öder; küçük koleksiyonlarda `List` + tarama daha hızlı olabilir (cache yerelliği).
- **`List<T>`** bitişik bellek → cache dostu; `LinkedList<T>` düğümleri dağınık, her düğüm ayrı tahsis → genelde yavaş.
- **Kapasite önceden:** tahsis ve kopya azalır; bilinmeyen boyutta fazla büyük kapasite bellek israfıdır.
- **`Dictionary` anahtar tipi:** `string` anahtarın hash'i her aramada hesaplanır; struct anahtarda `IEquatable<T>` boxing'i önler.
- **`IEnumerable<T>` olarak tutmak** struct enumerator'ı kutular (küçük tahsis); sıcak yolda somut tip.
- **Read-only arayüz** sıfır maliyet (sözleşme); `AsReadOnly()` küçük sarmalayıcı nesne; kopya O(n) tahsis. Sık çağrılan getter'larda kopya pahalıdır — canlı görünüm/`ImmutableArray`.
- **`Frozen*` koleksiyonlar** (.NET 8) okuma-ağırlıklı, bir kez kurulan veriler için en hızlı aramayı verir (→ M1, S2).

---

## 8. Kaynaklar

- Koleksiyonlar ve veri yapıları: <https://learn.microsoft.com/dotnet/standard/collections/>
- Hangi koleksiyonu seçmeli: <https://learn.microsoft.com/dotnet/standard/collections/selecting-a-collection-class>
- `Dictionary<TKey,TValue>`: <https://learn.microsoft.com/dotnet/api/system.collections.generic.dictionary-2>
- `GetHashCode` yönergeleri: <https://learn.microsoft.com/dotnet/api/system.object.gethashcode#notes-to-inheritors>
- Generics ve constraint'ler: <https://learn.microsoft.com/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters>
- `PriorityQueue<TElement,TPriority>`: <https://learn.microsoft.com/dotnet/api/system.collections.generic.priorityqueue-2>
- Collection tasarım yönergeleri: <https://learn.microsoft.com/dotnet/standard/design-guidelines/guidelines-for-collections>
- Big-O cheat sheet (teori → D2): <https://www.bigocheatsheet.com>
