# J2.1 Program Anatomisi — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. `Main` metodunun geçerli imzaları nelerdir? Exit code ne anlama gelir?
`void Main()`, `void Main(string[] args)`, `int Main(...)`, `async Task Main(...)` ve `async Task<int> Main(...)`; hepsi `static`'tir, erişim düzeyi önemli değildir, bir assembly'de tek giriş noktası olabilir. Dönüş değeri **exit code**'dur: process bittiğinde işletim sistemine bildirilen sayı; `0` başarı, sıfırdan farklı hata demektir. CI, shell script'leri, Docker ve Kubernetes Job'ları bu koda bakar. Yakalanmayan exception process'i sıfırdan farklı kodla sonlandırır.

### 2. Top-level statements nedir? Derleyici ne üretir?
`Program.cs`'e class ve `Main` yazmadan doğrudan ifadeler yazmaktır (C# 9). Derleyici gizli bir `Program` sınıfı ve `<Main>$` metodu üretir; `args` örtük değişkendir, `return 0;` exit code'u belirler, `await` kullanılırsa `Main` `async Task<int>` olur. Bir projede yalnızca bir dosya top-level statements içerebilir. Reflection ile `Assembly.EntryPoint` incelenirse `<Main>$` görülür.

### 3. ⭐ C#'taki erişim belirleyicilerini ve farklarını anlat.
`private` yalnızca aynı sınıf; `private protected` aynı sınıf + **aynı assembly'deki** türeyenler; `protected` aynı sınıf + türeyenler (başka assembly dahil); `internal` aynı assembly'deki herkes; `protected internal` hem `protected` hem `internal` erişimlerinin **birleşimi** (aynı assembly'deki herkes veya başka assembly'deki türeyenler); `public` herkes. Varsayılanlar: tip → `internal`, üye → `private`.

### 4. ⭐ `protected internal` ile `private protected` farkı nedir?
`protected internal` bir **VEYA**'dır (protected ∪ internal): daha açıktır. `private protected` bir **VE**'dir (protected ∩ internal): yalnızca **aynı assembly içindeki** türeyen sınıflar erişebilir, daha kapalıdır. Örnek: başka assembly'deki türeyen sınıf, `protected internal` üyeye erişir ama `private protected` üyeye erişemez.

### 5. `InternalsVisibleTo` ne işe yarar?
`internal` tipleri/üyeleri belirli "dost" assembly'lere açar; tipik kullanım test projesidir (`<InternalsVisibleTo Include="MyProject.Tests" />`). Public yüzeyi şişirmeden iç mantığı test etmeyi sağlar. Public yapılan her şey geriye dönük uyumluluk sözleşmesine dönüştüğü için, test edebilmek uğruna `public` yapmaktan iyidir.

### 6. `static class` nedir? static constructor ne zaman çalışır?
`static class` örneklenemez ve yalnızca static üye taşır. Static constructor parametresizdir, elle çağrılamaz; tip ilk kullanılmadan önce runtime tarafından **bir kez** ve thread-safe çalıştırılır (`const`'a erişim tetiklemez). İçinde exception fırlarsa tip kalıcı olarak kullanılamaz hale gelir: her erişimde `TypeInitializationException`. Tam zamanlama (`beforefieldinit`) → S1.2.

### 7. `partial` ne işe yarar? Source generator ile ilişkisi?
Bir tipi/metodu birden çok dosyaya bölmeyi sağlar; amacı elle yazılan kodu üretilmiş koddan ayırmaktır (designer, ORM, generator). `partial` metot, bildirimi bir yerde gövdesi başka yerde olan metottur. Source generator'lar bunu kullanır: ör. `[GeneratedRegex] static partial Regex Digits();` yazarsınız, derleyici gövdeyi derleme sırasında üretir (reflection yok, AOT/trimming dostu).

### 8. Statement ile expression farkı nedir? Expression-bodied member nedir?
Expression bir değer üretir (`a + b`, `x ? y : z`, `switch` expression, atama, `throw` expression); statement bir iş yapar (`if`, `for`, `return`). Gövdesi tek expression olan metot/property'yi `=>` ile yazmaya expression-bodied member denir: `public double Area => Math.PI * r * r;`.

### 9. Scope ve gölgeleme (shadowing) nedir? Definite assignment nedir?
Yerel değişken tanımlandığı en yakın bloğun içinde geçerlidir. Yerel değişken aynı adlı bir **alanı** gölgeleyebilir, ama iç içe iki yerel değişken aynı ada sahip olamaz (CS0136). Definite assignment: bir yerel değişkenin okunmadan önce **her kod yolunda** atanmış olması gerekir; derleyici akış analiziyle denetler (CS0165).

### 10. ⭐ `Parse` ile `TryParse` farkı nedir? Hangisini ne zaman kullanırsın?
`Parse` geçersiz girdide exception (`FormatException`, `OverflowException`, `ArgumentNullException`) fırlatır; `TryParse` `bool` döner ve sonucu `out` ile verir. Kullanıcı/dış girdi gibi hatanın **beklenen** olduğu yerlerde `TryParse`; exception pahalıdır ve kontrol akışı için yanlış araçtır. Girdinin garantili doğru olduğu durumlarda `Parse` kabul edilebilir. Ayrıca kültür verilmelidir: makineler arası veri için `CultureInfo.InvariantCulture`.

### 11. Cast, `as`, `is` ve `Convert` farkları nelerdir?
Explicit cast `(T)x` başarısız olursa `InvalidCastException` fırlatır, sayısal dönüşümde keser/taşabilir. `as` referans/nullable dönüşümü dener, başarısızsa `null` döner. `is` tip testidir (pattern ile `o is string s` kontrol + cast birlikte). `Convert.ToXxx` `null`'ı 0 yapar, format hatasında exception verir ve ondalıkları **banker's rounding** ile yuvarlar (`Convert.ToInt32(2.5) == 2`).

### 12. `(int)3.99` ne döner? Yuvarlama nasıl yapılır?
`3` — cast ondalığı **keser**, yuvarlamaz. Yuvarlamak için `Math.Round(x, MidpointRounding.AwayFromZero)` (ya da varsayılan banker's rounding'i bilinçli kullanmak). Para hesaplarında `decimal` ve açık yuvarlama kuralı kullanılır.

### 13. ⭐ `checked` ve `unchecked` nedir? Varsayılan ne?
Tamsayı aritmetiğinde taşma kontrolüdür. **Varsayılan unchecked**: taşma sessizce sarar (`int.MaxValue + 1 == int.MinValue`). `checked` taşmada `OverflowException` fırlatır; ifade (`checked(a + b)`), blok veya proje geneli (`<CheckForOverflowUnderflow>true`) olarak uygulanır. `decimal` her zaman denetlidir; `float`/`double` taşmada `Infinity` üretir. Kritik hesaplarda (para, stok) ya geniş tip (`long`) ya da `checked` kullanılır.

### 14. ⭐ `&&` ile `&` farkı nedir? Short-circuit neden önemlidir?
`&&` sol taraf `false` ise sağ tarafı hiç değerlendirmez (short-circuit), `&` her iki tarafı da değerlendirir. Fark hem performansı hem güvenliği etkiler: `obj != null && obj.Prop > 0` null-guard'ı sağ tarafın hiç çalışmamasına dayanır; `&` ile yazılırsa `NullReferenceException` alınır. Sağ taraf yan etki taşıyorsa davranış da değişir.

### 15. `typeof`, `GetType()` ve `is` farkı nedir?
`typeof(T)` derleme zamanında bir `Type` nesnesi verir; `obj.GetType()` nesnenin çalışma zamanındaki gerçek tipini verir. `is` kalıtım/arayüz uyumunu da kapsar (`list is IEnumerable<int>` true), `GetType() == typeof(...)` ise tam tip eşleşmesi ister.

### 16. `nameof` neden kullanılır?
Sembolün adını derleme zamanında string olarak verir; yeniden adlandırma (refactor) sırasında otomatik güncellenir, magic string hatalarını önler (`throw new ArgumentNullException(nameof(order))`).

### 17. `#if DEBUG` ile `if (isDebug)` farkı nedir?
`#if` derleme zamanında hangi kodun derlemeye gireceğini belirler; seçilmeyen kod IL'e yazılmaz. `if` ise çalışma zamanında dal seçer, iki dal da derlenir. `[Conditional("DEBUG")]` çağrıları (argümanlarıyla birlikte) Release derlemesinde siler — bu yüzden argümanlarında yan etki olmamalıdır.

### 18. `[CallerMemberName]` ve `[CallerArgumentExpression]` nedir?
Çağıranın bilgisini (metot adı, dosya, satır, argümanın kaynak metni) **derleme zamanında** parametre varsayılan değerine enjekte eden attribute'lardır; reflection/StackTrace maliyeti yoktur. Loglamada ve `INotifyPropertyChanged`'de `[CallerMemberName]`, guard clause'larda `[CallerArgumentExpression]` (örn. `ThrowIfNull(x)` parametre adını böyle bulur) kullanılır.

### 19. XML documentation comment ne işe yarar?
`///` yorumları IntelliSense'te görünür, `GenerateDocumentationFile` ile `.xml` olarak üretilir, Swagger/OpenAPI araçları bunu endpoint açıklamalarına taşır, docfx gibi araçlar dokümantasyon sitesi üretir. Eksik public yorumlar CS1591 uyarısı verir.

### 20. `global using` ve `ImplicitUsings` nedir?
`global using X;` bildirimi projedeki tüm dosyalar için geçerli olur. `<ImplicitUsings>enable</ImplicitUsings>` SDK'nın yaygın namespace'ler için (System, System.Linq, System.IO, System.Collections.Generic, System.Threading, System.Threading.Tasks, System.Net.Http; web projelerinde ek olarak ASP.NET Core) otomatik global using üretmesidir. Dosya başı gürültüsünü azaltır; çakışmalarda alias veya tam nitelikli ad kullanılır.
