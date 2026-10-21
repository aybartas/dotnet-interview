# J2.2 Tip Sistemi & Bellek Modeli — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. ⭐ Value type ile reference type farkı nedir?
Asıl fark **kopyalama semantiğidir**: value type'ta atama/metoda geçirme **değeri kopyalar** (iki bağımsız kopya), reference type'ta **referansı kopyalar** (ikisi aynı nesneyi gösterir). Buna bağlı olarak: varsayılan `==` value type'ta alan değerlerine, reference type'ta referans eşitliğine bakar; value type `null` olamaz (`int?` ile olur); value type'lar `ValueType`'tan, reference type'lar doğrudan `Object`'ten türer. "Biri stack, biri heap" bir uygulama detayıdır ve çoğu zaman yanlıştır.

### 2. ⭐ "Value type'lar stack'te yaşar" doğru mu?
Hayır, her zaman değil. Değer, **onu tutan değişkenin yaşadığı yerdedir**: yerel değişken ise stack (veya register), bir **sınıfın alanı** ise o nesneyle birlikte heap, **dizi/`List<T>` elemanı** ise dizinin belleğinde (heap), **closure'a yakalanan** değişken ise derleyicinin ürettiği sınıfta (heap), **boxing**'e uğradıysa heap'te yeni bir nesnede. Tersi için: reference type'ın *referansı* stack'te olabilir, nesnenin kendisi heap'tedir.

### 3. ⭐ `struct` ne zaman tercih edilir?
Küçük (tipik olarak 16 byte altı), tek bir mantıksal değeri temsil eden, **immutable**, kısa ömürlü ve değer semantiği istenen tiplerde (`Point`, `Money`, `DateTime` gibi); heap tahsisini ve GC baskısını azaltmak için. Büyük, mutable, sık boxing'e uğrayan ya da kalıtım/kimlik gereken tiplerde `class`. Struct kalıtım alamaz (interface uygulayabilir), `null` olamaz.

### 4. Mutable struct neden tehlikelidir?
Struct kopyalanır; `list[0].Value++` gibi bir çağrı kopyayı değiştirir (derleyici çoğu yerde CS1612 ile engeller), `foreach` değişkeni kopyadır, `readonly` alandaki struct'ın mutating metodu savunmacı kopya üzerinde çalışır, interface'e atanan struct kutulanıp kopyalanır. Sonuç "değiştirdim ama değişmedi" hatalarıdır. Çözüm `readonly struct`/`readonly record struct` ve değişiklik için `with` ile yeni kopya.

### 5. `struct`, `class`, `record` ve `record struct` arasındaki farklar?
`class` referans semantiği, referans eşitliği. `record` (class) referans tipidir ama değer tabanlı eşitlik, `with`, `ToString`, deconstruct derleyici tarafından üretilir. `struct` değer semantiği, eşitlik elle yazılmazsa yavaş reflection tabanlıdır. `record struct` değer semantiği + otomatik değer eşitliği. Veri taşıyan değişmez nesneler için `record`, küçük değer nesneleri için `readonly record struct`, davranış/kimlik için `class`.

### 6. ⭐ Boxing nedir, ne zaman olur, maliyeti nedir?
Value type'ı `object` veya interface tipine dönüştürmek; heap'te yeni bir nesne tahsis edilir ve değer kopyalanır (unboxing ise tip kontrolü + kopya). Gizlice olduğu yerler: `object` parametre, interface'e atama, non-generic koleksiyonlar (`ArrayList`), `string.Format` gibi `params object[]` metotları. Maliyeti: her kutu için ~24 byte tahsis → GC baskısı ve yavaşlama. Kaçınma: generic koleksiyonlar/metotlar ve `where T : struct, IFoo` kısıtı.

### 7. `(long)boxedInt` neden `InvalidCastException` verir?
Unboxing **tam tipe** yapılmalıdır: kutu `int` içeriyorsa `(int)o` yazılır; `long` istenirse önce `(long)(int)o`. Çünkü unboxing bir dönüşüm değil, kutudaki tipin kontrol edilip değerin çıkarılmasıdır.

### 8. ⭐ String neden immutable'dır? Sonuçları nelerdir?
Thread-safe paylaşım (kilit gerekmez), `Dictionary`/`HashSet` anahtarı olarak güvenli (hash değişmez), interning mümkün, güvenlik (yol/bağlantı dizesi sonradan değiştirilemez). Bedeli: her "değişiklik" yeni nesne üretir; döngüde `+=` O(n²) tahsis yapar, bu yüzden döngüde `StringBuilder` kullanılır. Tek ifadedeki `a + b + c` zaten `string.Concat`'e derlenir.

### 9. `string` referans tipi olduğu halde `==` neden değer karşılaştırır?
`string` sınıfı `==` operatörünü overload eder. Overload derleme zamanı tipine bağlıdır: iki operand `string` ise içerik, `object` olarak tutuluyorsa referans karşılaştırılır (`(object)a == (object)b` → false olabilir).

### 10. String interning nedir?
Aynı değerli string literal'lerinin runtime'da tek bir nesneyi paylaşması (intern havuzu). Runtime'da üretilen string'ler otomatik interning'lenmez; `string.Intern` ile havuza alınır. Bellek/hız avantajı sağlar ama `ReferenceEquals` ile string karşılaştırmak yanlıştır; kullanıcı girdisini interning'lemek havuz şişirir.

### 11. ⭐ "Türkçe I problemi" nedir? Nasıl önlenir?
Türk kültüründe `i`'nin büyüğü `İ`, `I`'nın küçüğü `ı`'dır. `"title".ToUpper()` tr-TR'de `"TİTLE"`, `"FILE".ToLower()` `"fıle"` olur; bu yüzden `if (cmd.ToLower() == "file")` yalnızca Türkçe kültürde başarısız olur. Önlem: dil-bağımsız tanımlayıcılarda (komut, dosya yolu, URL, header, config anahtarı) `StringComparison.Ordinal/OrdinalIgnoreCase` ve `ToUpperInvariant()/ToLowerInvariant()` kullanmak; kullanıcıya gösterilen metinde kültür duyarlı karşılaştırma; CA1304/CA1307/CA1309/CA1310 analyzer kurallarını açmak; testleri farklı kültürlerde çalıştırmak.

### 12. `Ordinal` ile kültür duyarlı karşılaştırma arasındaki fark nedir?
Ordinal, karakter kodlarını (UTF-16 kod birimlerini) bayt bayt karşılaştırır: hızlıdır, kültürden bağımsızdır, tanımlayıcılar için doğrudur. Kültür duyarlı karşılaştırma dilin sıralama/eşleme kurallarını (ICU) uygular: kullanıcıya gösterilen sıralama için doğrudur ama daha yavaştır ve sonucu kültüre göre değişir.

### 13. `int?` ile `string?` arasındaki fark nedir?
`int?` gerçek bir tiptir: `Nullable<int>` struct'ı (`HasValue` + `Value`), çalışma zamanında vardır, `null.Value` exception atar. `string?` ise **yalnızca derleme zamanı** nullable analizidir; çalışma zamanında `string` ile aynı tiptir, yalnızca metadata'da öznitelik olarak durur ve zorlanmaz. `!` operatörü analizi susturur ama null'ı kaldırmaz.

### 14. `Nullable<int>` boxing'e uğrarsa ne olur? `null < 5` ne döner?
`null` olan `int?` kutulanınca `null` referans olur; dolu olan, `int`'in kutusu olur (`Nullable<int>` kutulanmaz). Lifted karşılaştırmalar `null` ile `false` döner: `null < 5`, `null >= 5` ikisi de `false`'tur; aritmetik `null + 1` ise `null`'dır.

### 15. `?.`, `??` ve `??=` ne yapar?
`?.` null-conditional: sol taraf null ise zinciri keser ve sonuç null olur (`order?.Customer?.Name`). `??` null-coalescing: sol null ise sağ tarafı verir. `??=` sol taraf null ise atama yapar (lazy init, varsayılan değer). Üçü birlikte iç içe null kontrollerini tek satıra indirir.

### 16. `var`, `object` ve `dynamic` farkı nedir?
`var` derleme zamanı tip çıkarımıdır; değişken statik tipli ve güvenlidir (anonim tipler için zorunlu). `object` her şeyi tutar, ama üyelere erişmek için cast gerekir ve value type'ta boxing olur. `dynamic` üye çözümlemesini çalışma zamanına bırakır: derleme zamanı kontrolü ve IntelliSense yoktur, hatalar `RuntimeBinderException` olarak çalışırken çıkar, ek maliyeti vardır; COM interop ve şemasız veri dışında kaçınılır.

### 17. ⭐ `const`, `readonly` ve `static readonly` farkı nedir? `const` ile ilgili versiyonlama sorunu nedir?
`const` derleme zamanında bilinen ilkel/string/enum/null değerlerdir ve değer kullanıldığı her yere (başka assembly dahil) IL'e **kopyalanır**. `static readonly` çalışma zamanında bir kez atanır (herhangi bir tip olabilir), çağıran değeri çalışırken okur. `readonly` örnek alanı constructor'da atanır. Sorun: kütüphanedeki `public const`'ı değiştirip yalnızca kütüphaneyi dağıtırsanız, yeniden derlenmemiş tüketici eski değeri kullanır. Bu yüzden değişebilir public değerler için `static readonly` veya property kullanılır.

### 18. `ref`, `out` ve `in` farkı nedir?
`ref`: çağıranda önceden atanmış değişkene referans, metot okuyup yazabilir. `out`: çağıranda atanmış olması gerekmez ama metot her yolda atamak zorundadır (TryParse deseni). `in`: referansla geçirir (kopyasız) ama metot değiştiremez; büyük `readonly struct`'lar için. Reference type değerle geçirilince referansın kopyası geçer: nesnenin içeriği değişir ama referansa yeni nesne atamak çağıranı etkilemez; bunun için `ref` gerekir.

### 19. Tuple (`ValueTuple`) nedir? Ne zaman record/class tercih edersin?
`ValueTuple` birden çok değeri hafifçe taşıyan, mutable alanlı bir struct'tır; eleman adları yalnızca derleme zamanı süsüdür (`Item1`, `Item2`). Deconstruction (`var (a, b) = ...`) ile ayrıştırılır. Özel/yerel çok değerli dönüşler için uygundur; public API sınırında ve anlamlı domain kavramlarında isimli tip (`record`) daha okunaklı, belgelenebilir ve evrilebilirdir.

### 20. `enum`'a her tamsayı cast edilebilir mi? `[Flags]` nedir?
Evet, `(Status)99` exception atmaz ve geçersiz bir değer üretir; `Enum.IsDefined` ile doğrulanır. `Enum.TryParse("99")` de başarılı olabilir. `[Flags]` bir enum'u bit alanı olarak kullanmayı sağlar: değerler 2'nin kuvvetleri olur, `|` ile birleştirilir, `& ~X` ile kaldırılır, `HasFlag`/`(v & X) != 0` ile test edilir ve `ToString()` "Read, Write" yazar. `0` değeri için `None` tanımlanır.
