# J2.4 Sınıf Üyeleri & Nesne Semantiği — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. ⭐ Neden public field yerine property kullanılır?
(1) **Doğrulama/invariant koruma:** field'a her değer atanabilir, property'nin setter'ı doğrulayabilir. (2) **Erişim hassasiyeti:** salt okunur, `private set`, `init` gibi kısıtlamalar yapılabilir. (3) **Evrim:** sonradan field'ı property'ye çevirmek *binary breaking change*'dir (IL `ldfld` → `callvirt get_X`; eski derlenmiş tüketici `MissingFieldException` alır), baştan property yazmak ücretsizdir (auto-property). (4) Data binding, serileştirme, EF gibi araçlar property bekler. Field'a `ref/out` geçilebilir, property'ye geçilemez — bilinen küçük fark.

### 2. `{ get; set; }`, `{ get; init; }`, `{ get; private set; }` ve `required` farkı nedir?
`set` her yerden yazılır; `init` yalnızca nesne oluşturulurken (initializer/ctor) atanır, sonra salt okunur; `private set` yalnızca sınıfın kendisi yazar (durum geçişini metotlar yönetir); `required` (C# 11) nesne oluştururken o üyenin atanmasını **zorunlu** kılar (aksi halde derleme hatası), "yarım nesne"yi önler. C# 14 `field` keyword'ü, backing field yazmadan setter'da doğrulama yapmayı sağlar.

### 3. Property ile metot arasında nasıl seçim yaparsın?
Property ucuz, yan etkisiz ve tekrar okunduğunda aynı sonucu veren bir durum gibi davranmalıdır. Pahalı iş (DB/ağ çağrısı, uzun hesap), yan etki veya her seferinde farklı sonuç üretiyorsa **metot** olmalıdır (`GetOrders()`). Özellikle debugger'ın property'leri otomatik değerlendirdiği düşünülürse yan etkili property'ler tehlikelidir.

### 4. ⭐ Field initializer'lar ve constructor'lar hangi sırayla çalışır?
`new Derived()` için: (1) alanlar varsayılan değere (0/null) ayarlanır, (2) **field initializer'lar en türetilmiş sınıftan tabana doğru** çalışır, (3) **constructor gövdeleri tabandan türetilmişe doğru** çalışır (`this(...)`/`base(...)` zincirleri önce). Initializer'ların "ters" gibi görünmesinin nedeni, derleyicinin onları ctor'un başına, `base(...)` çağrısından önce yerleştirmesidir. Static başlatma ise her tip için ilk kullanımdan önce bir kez yapılır.

### 5. ⭐ Constructor'dan virtual metot çağırmak neden tehlikelidir?
Taban ctor çalışırken türetilmiş sınıfın ctor gövdesi henüz çalışmamıştır; override edilen metot yarım kurulmuş nesneyi görür. Field initializer ile atanan alanlar dolu (taban ctor'dan önce çalıştıkları için), ctor gövdesinde atananlar `null`/`0` kalır — tutarsız ve yalnızca belirli alt sınıflarda patlayan hatalar. Analyzer **CA2214** yakalar. Çözüm: ctor'da virtual çağırmamak; nesne tamamen kurulduktan sonra (`Initialize()`, factory) çağırmak ya da sınıfı `sealed` yapmak.

### 6. Static constructor ne zaman çalışır? Exception fırlatırsa ne olur?
Tip ilk kullanılmadan önce (ilk static üye erişimi veya örnek oluşturma) runtime tarafından bir kez ve thread-safe çalıştırılır; `const` erişimi tetiklemez. Exception fırlatırsa tip kalıcı olarak kullanılamaz hale gelir: her erişimde `TypeInitializationException` alınır. (Derin hali → S1.2.)

### 7. `private` constructor ne işe yarar?
Nesne oluşturmayı sınıfın kendisine kısıtlar: static factory method (`Connection.Create(...)`) deseni, singleton, yalnızca static üye taşıyan yardımcı sınıflar ve oluşturma kurallarını zorlamak (doğrulama yapıp başarısız olabilen `TryCreate`) için kullanılır.

### 8. ⭐ `Equals` override edince neden `GetHashCode` da override edilmeli?
Sözleşme: `a.Equals(b)` ise `a.GetHashCode() == b.GetHashCode()` olmalıdır. `Dictionary`/`HashSet` önce hash'e bakıp nesneyi bir kovaya koyar, sonra aynı kovada `Equals` çağırır. Yalnızca `Equals` override edilirse eşit nesneler farklı hash üretir: `HashSet.Contains` false döner, aynı mantıksal anahtar iki kez eklenebilir, `Dictionary` anahtarı bulamaz. Hash, eşitlikte kullanılan alanlardan türetilmeli (`HashCode.Combine`).

### 9. `==` ile `Equals` farkı nedir? `ReferenceEquals` nedir?
Class'larda varsayılan olarak ikisi de referans eşitliğidir; `==` operatör overload'ı ile `Equals` sanal metot override'ı birbirinden bağımsızdır (ikisini tutarlı yazmak gerekir). `string` ve `record` değer eşitliği sağlar. `object.ReferenceEquals(a, b)` her zaman referans (aynı nesne mi) kontrolüdür, overload'dan etkilenmez.

### 10. ⭐ Mutable key tuzağı nedir?
`Dictionary`/`HashSet`'e eklendikten sonra hash'i (eşitliğe giren alan) değişen nesne, koleksiyonun eski kovasında kalır: `Contains` ve `Remove` false döner, ama nesne koleksiyonda durur ("hayalet kayıt") ve aynı nesne tekrar eklenebilir. Anahtar olarak immutable tipler (`string`, `int`, `record`, `readonly struct`) kullanılır.

### 11. `IEquatable<T>`, `IComparable<T>` ve `IComparer<T>` ne işe yarar?
`IEquatable<T>` tipli eşitlik sağlar (struct'ta `Equals(object)` yerine boxing'siz `Equals(T)`), koleksiyonlar onu tercih eder. `IComparable<T>` tipin doğal sıralamasını tanımlar (`Sort()` kullanır). `IComparer<T>` tipin dışında alternatif sıralama stratejisidir (azalan sıra, farklı alana göre).

### 12. ⭐ Extension method nasıl yazılır? Çözümleme nasıl yapılır?
Static bir sınıfta, static metodun ilk parametresine `this` yazılır: `public static string Shout(this string s)`. Çağrı `"x".Shout()` biçiminde yapılır ama derleyici bunu `StringExtensions.Shout("x")` statik çağrısına çevirir (tipe gerçekte bir şey eklenmez, null receiver'da `NullReferenceException` olmaz). Çözümleme: önce **örnek metot** (aynı imzalı extension hiç çağrılmaz), sonra çağıran dosyanın `using`'leriyle görünen extension'lar; `using` yoksa görünmez. LINQ'in tamamı extension method'dur.

### 13. Extension method ne zaman, inheritance/wrapper ne zaman tercih edilir?
Sahibi olmadığınız bir tipe (string, `IEnumerable<T>`) durumsuz yardımcı davranış eklemek ve fluent API için extension. Durum eklemek, davranışı override etmek veya polimorfizm gerektiğinde inheritance/composition/wrapper. Aşırı kullanım keşfedilebilirliği düşürür ve namespace'i kirletir.

### 14. Operator overloading yaparken hangi kurallara uyulur?
`==` ve `!=` birlikte (aksi halde CS0216), `<`/`>` ve `<=`/`>=` çift çift tanımlanır; `==` tanımlıysa `Equals(object)`, `GetHashCode` ve tercihen `IEquatable<T>` aynı mantıkla override edilir. `implicit` dönüşüm yalnızca kayıp/exception olmayan durumlarda, aksi halde `explicit`. Operatörler yalnızca doğal anlamı olan tiplerde (Money, Vector) kullanılır.

### 15. Indexer nedir? Birden çok parametre alabilir mi?
`public T this[int index] { get; set; }` ile nesneye dizi/koleksiyon benzeri erişim sağlayan özel property'dir. Parametre tipi `int` olmak zorunda değildir (`this[string key]`), birden çok parametre alabilir (`this[string currency, int nth]`), salt okunur olabilir. `Count` + `int` indexer varsa derleyici `^1` (Index) ve aralık (`1..3`) desteğini otomatik sağlar.

### 16. ⭐ Shallow copy ile deep copy farkı nedir? C#'ta nasıl yapılır?
Shallow copy yalnızca üst düzey alanları kopyalar; iç içe referans tipler **paylaşılır** (kopyayı değiştirmek orijinali bozar). `object.MemberwiseClone()` shallow'dur. Deep copy tüm nesne grafiğini yeniden oluşturur: copy constructor/elle kopyalama (açık ve hızlı), JSON round-trip (kolay ama yavaş ve tip kısıtlı), ya da immutable tasarım ile kopya ihtiyacını azaltmak. `record` ile `with` de shallow'dur: iç içe record/koleksiyonda referans paylaşılır.

### 17. `ICloneable` neden önerilmez?
`Clone()` `object` döner (cast gerekir) ve kopyanın shallow mı deep mi olduğunu sözleşmede belirtmez; kullanıcı davranışı tahmin etmek zorunda kalır. Microsoft public API'de kullanılmamasını önerir. Bunun yerine copy constructor veya açık adlı metot (`DeepCopy()`, `With...`) ya da immutable tasarım tercih edilir.

### 18. Optional parametre kullanırken dikkat edilecek tuzak nedir?
Varsayılan değer metodun içine değil **çağıran assembly'nin IL'ine** derlenir (`const` ile aynı mantık). Kütüphanede varsayılanı değiştirip yalnızca kütüphaneyi yayınlarsanız yeniden derlenmemiş tüketiciler eski değeri kullanmaya devam eder. Public API'de overload'lar daha güvenlidir. Ayrıca optional parametreli bir metot ile optional'sız overload birlikte olunca çözümleme kafa karıştırıcı olur.

### 19. Anonim tip nedir? Ne zaman `record` veya tuple tercih edilir?
Derleyicinin ürettiği, adsız, salt okunur, değer eşitlikli bir sınıftır (`new { Name = "A", Age = 1 }`); metot sınırını geçemez (dönüş tipi olarak yazılamaz), LINQ ara projeksiyonları için uygundur. Metotlar arası taşınacak veya kalıcı veri için `record` (isimli tip), özel/yerel çok değerli dönüş için tuple tercih edilir.

### 20. GC varken neden `IDisposable`'a ihtiyaç var? `using` ne yapar?
GC yalnızca yönetilen belleği toplar ve ne zaman çalışacağı belirsizdir. Dosya tanıtıcısı, soket, DB bağlantısı gibi yönetilmeyen kaynakların deterministik biçimde bırakılması gerekir. `using` (blok veya declaration) `Dispose()`'un exception olsa bile çağrılmasını garanti eder (`try/finally` şekerlemesi); birden çok kaynak tanımlanma sırasının tersiyle bırakılır. Dispose pattern'in tamamı → M1.5.
