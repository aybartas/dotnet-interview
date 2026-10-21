# J2.6 Günlük Modern C# — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. ⭐ `record` ile `class` farkı nedir?
`record` (class) yine bir referans tipidir; farkı, derleyicinin **değer tabanlı eşitlik** (`Equals`/`GetHashCode`/`==`), `ToString`, `Deconstruct`, init-only property'ler ve `with` desteğini otomatik üretmesidir. `class`'ta eşitlik varsayılan olarak referanstır ve bunların hepsini elle yazarsınız. Record değişmez veri taşıyıcılar, DTO'lar ve value object'ler için; class kimlik, davranış ve mutable durum taşıyan entity'ler için uygundur. `record struct` aynı özellikleri değer tipinde sunar (`readonly record struct` tercih edilir).

### 2. `with` ifadesi nasıl çalışır? Derin kopya mıdır?
Nesnenin bir kopyasını (derleyicinin ürettiği kopyalama mantığıyla) oluşturur ve belirtilen init property'leri değiştirir; orijinal değişmez. **Shallow**'dur: iç içe referans tipler (iç record, `List<T>`) kopya ile orijinal arasında paylaşılır. İçeriği de izole etmek için iç nesne yeni değerle verilir: `rec with { Address = rec.Address with { City = "X" } }`.

### 3. Record'larda koleksiyon alanı eşitliği nasıl davranır?
Üretilen `Equals` her alanı kendi `Equals`'ıyla karşılaştırır; `List<T>` için bu referans eşitliğidir. Bu yüzden içeriği aynı iki listeyi tutan iki record eşit **olmaz**. Çözüm: `Equals`'ı elle yazıp `SequenceEqual` kullanmak ya da immutable koleksiyon/value object kullanmak.

### 4. `init` ile `set` farkı nedir? `required` ne sağlar?
`set` her zaman atanabilir; `init` yalnızca nesne oluşturulurken (initializer/ctor/`with`) atanabilir, sonra salt okunur olur. `required` (C# 11) oluşturma sırasında üyenin atanmasını derleme zamanında zorunlu kılar (CS9035) — "yarım nesne" oluşmaz. İkisi birlikte ctor yazmadan eksiksiz, değişmez DTO tanımlamayı sağlar.

### 5. ⭐ Pattern matching çeşitlerini say ve örnek ver.
Type (`x is string s`), constant (`x is null`), relational (`x is > 5`), logical (`and`/`or`/`not`), property (`o is { Total: > 1000 }`, iç içe `Customer.Address.City: "Ankara"`), positional/tuple (`(a, b) switch {…}`), list (`arr is [1, .., 3]`), var/discard. Hepsi `is` ve `switch` ile kullanılır; eski `as` + null kontrolü ve çoklu cast'lerin yerini alır. Null kontrolü için `is not null`, `!=` overload'ından etkilenmediği için tercih edilir.

### 6. ⭐ `switch` expression ile `switch` statement arasındaki fark?
Expression bir **değer üretir** (doğrudan atanır/döndürülür), `break` gerektirmez, `=>` ve `_` (varsayılan), `when` guard ve `throw` ifadesi kullanır; derleyici kapsamlılık denetimi yapar (CS8509: eksik dal uyarısı), eşleşme yoksa çalışma zamanında `SwitchExpressionException` fırlar. Statement'ta ise değişkeni önceden tanımlayıp her dalda atar, `break` yazarsınız ve unutulan atama sessiz kalabilir. Dal sırası önemlidir (özelden genele; gölgelenen dal CS8510).

### 7. Raw string literal nedir? Neden kullanılır?
`"""…"""` ile yazılan, içinde kaçış karakteri gerektirmeyen string'dir (C# 11): JSON, SQL, regex, HTML gibi tırnak ve ters bölü içeren metinler için okunabilir ve hatasızdır. Kapanış `"""`'in girintisi içerikten kırpılır. Interpolation için `$$"""…{{ifade}}…"""` kullanılır (tek `{ }` literal kalır). Güvenlik: kullanıcı girdisini SQL'e interpolation ile gömmek SQL injection açığıdır; parametre kullanılmalıdır.

### 8. Collection expression nedir? Spread operatörü ne yapar?
`[1, 2, 3]` sözdizimi (C# 12): dizi, `List<T>`, `ImmutableArray<T>`, `Span<T>`, `IEnumerable<T>` gibi **hedef tipe göre** koleksiyon oluşturur; boş koleksiyon `[]`. `..` (spread) bir koleksiyonun elemanlarını içeri yayar: `[.. first, 3, .. second]`; `Concat`/`AddRange` yerine kullanılır. Hedef tip gerektirir (`var x = [1,2]` derlenmez, CS9176). Dictionary için değil, index initializer kullanılır.

### 9. ⭐ Primary constructor'ın parametresi property midir?
Class/struct primary constructor'ında **hayır**: parametre sınıf gövdesinde kullanılabilen (gerekirse derleyicinin gizli alana yakaladığı) bir parametredir; dışarı açmak için `public string X { get; } = x;` yazılır. **Record**'da ise parametre otomatik olarak init-only property'dir ve eşitliğe/Deconstruct'a katılır. Yakalanan parametre mutable'dır (değiştirilirse state gizlenir), bu yüzden immutable tutulmalıdır. DI'da `class Svc(IRepo repo, ILogger<Svc> log)` ile boilerplate azalır.

### 10. File-scoped namespace, global using, target-typed `new()` ve `using` declaration nedir?
`namespace X;` tüm dosyayı o namespace'e alır, bir girinti seviyesi kazandırır. `global using` bir namespace'i projedeki tüm dosyalara açar; `ImplicitUsings` SDK'nın yaygın namespace'ler için ürettiği global using'lerdir. `new()` tip sol taraftan biliniyorsa tipi tekrarlamaz (`List<int> l = new();`; `var x = new();` derlenmez). `using var r = …;` kapsam sonunda (ters sırada) Dispose çağırır, iç içe `using` bloklarını ortadan kaldırır.

### 11. C# 14 `field` keyword'ü nedir?
Property'nin derleyici tarafından üretilen backing field'ına `field` adıyla erişmeyi sağlar; böylece alan bildirmeden setter'da doğrulama/normalizasyon yazılır: `set => field = value?.Trim() ?? throw …`. Sınıfta zaten `field` adında bir üye varsa anlam değişebilir; `@field`/`this.field` ile ayrılır.

### 12. Null-conditional assignment (`a?.B = x`) nasıl çalışır?
C# 14: `customer?.Name = value;` — `customer` null ise atama yapılmaz ve **sağ taraf da değerlendirilmez**; null değilse normal atanır. Bileşik atama (`+=`) çalışır, `++`/`--` desteklenmez. `if (customer != null) customer.Name = value;` yazımının kısa halidir.

### 13. Eski stil kodu modernleştirirken nasıl ilerlersin?
Önce mevcut davranışı testlerle sabitlerim (iki uygulamanın aynı sözleşme testlerinden geçmesi gibi), sonra küçük adımlarla ilerlerim: DTO'ları `record`/`required init`'e, `switch` statement'ları expression'a, `StringBuilder`/`string.Format` yerine interpolation/raw string'e, koleksiyon başlatmayı collection expression'a çeviririm. EF entity'lerine ve davranışı karmaşık sınıflara aceleyle dokunmam; takım okunabilirlik standardını gözetirim.

### 14. Modern sözdizimi her zaman daha mı iyidir?
Hayır. Kısalık okunabilirlik demek değildir: iç içe ileri pattern'ler ve zincirleme primary constructor/collection expression deneyimsiz okuyucuya yabancı gelebilir; record'un koleksiyon eşitliği ve `with`'in shallow oluşu gibi incelikler gözden kaçabilir. Hedef, aynı davranışı daha az hata yüzeyiyle ifade etmektir; takım standardı, `LangVersion` ve kütüphane uyumluluğu (çoklu hedef) dikkate alınır.

### 15. Aynı domain'i C# 7 ve C# 14 tarzında yazarak ne gözlemledin?
Aynı sipariş modeli ~224 satırdan ~56 satıra indi (%75): `Address`/`OrderLine` elle `Equals`/`GetHashCode`/`ToString` yerine tek satır record, `Customer.Email` alan+property yerine `field`, durum makinesi `switch` statement yerine `switch` expression + `with`, indirim hesabı tuple + relational pattern, makbuz `StringBuilder` yerine raw string, namespace/using gürültüsü file-scoped + global using ile gitti. Her iki sürüm aynı davranış testlerinden geçti; yalnızca `Order.Equals` record'un koleksiyon eşitliği yüzünden elle kaldı.
