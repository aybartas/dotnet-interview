# J2.1 Program Anatomisi

> 📁 `ConsoleApp/01-junior/02-csharp-fundamentals/01-program-anatomy/` · Önkoşul: J1 · Sorular: [`interview-questions.md`](interview-questions.md)

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

Bir C# programının **iskeleti**: nereden başlar (entry point), kod nasıl düzenlenir (namespace, using, partial), kime ne görünür (erişim belirleyicileri) ve dilin her gün karşınıza çıkan temel mekanikleri (dönüşümler, taşma, operatörler, preprocessor, derleme zamanı bilgisi). Mülakatın ilk teknik turu çoğunlukla buradan başlar; yüzeysel tanım değil **örnekle açıklama** beklenir.

Konu 13 küçük, yorumlu bölümden oluşur ([`Sections/`](Sections/); `dotnet run -- J2.1 <numara>`); [`AccessModifiers/`](AccessModifiers/) erişim belirleyicilerini derleyiciye denetletir (başka assembly'den bakış: 3.3'teki alıştırma).

---

## 2. Neden?

- Doğru erişim düzeyi **API tasarımının** ilk adımıdır: `public` bir kez yayınlandı mı geri alınamaz.
- Sessiz hatalar (integer overflow, kesen cast, kültür duyarlı `Parse`) üretimde **yanlış para hesabı** olarak ortaya çıkar.
- `partial`, `[CallerMemberName]`, `nameof`, `#if` gibi araçlar framework'lerin (ASP.NET Core, EF Core, source generator'lar) **nasıl çalıştığını** anlamanın anahtarıdır.

---

## 3. Nasıl?

### 3.1 Entry point ve top-level statements

Geçerli `Main` imzaları (hepsi `static`; erişim düzeyi önemsiz):

```csharp
static void Main()                       static void Main(string[] args)
static int  Main()                       static int  Main(string[] args)          // dönüş değeri = exit code
static async Task Main()                 static async Task Main(string[] args)
static async Task<int> Main()            static async Task<int> Main(string[] args)
```

- Bir assembly'de **tek** entry point olabilir.
- **Exit code:** process bittiğinde işletim sistemine bildirilen sayı. `0` = başarı, sıfırdan farklı = hata. CI, script'ler, Kubernetes `Job`'ları ve Docker bu sayıya bakar. Yakalanmayan exception process'i sıfırdan farklı kodla (Linux'ta genelde 134) sonlandırır.
- **Top-level statements (C# 9):** `Program.cs`'e doğrudan ifadeler yazarsınız; derleyici bir `Program` sınıfı ve `<Main>$` metodu **üretir**. `args` örtük bir değişkendir; `return 3;` exit code'u belirler; `await` kullanırsanız `Main` otomatik `async Task<int>` olur. Bir projede **yalnızca bir dosya** top-level statements içerebilir. ([`Section01`](Sections/Section01_EntryPoint.cs) bunu reflection ile gösterir.)

```csharp
// Program.cs                         // Derleyicinin ürettiği (kavramsal)
Console.WriteLine(args.Length);       // internal class Program {
return 0;                             //     static int <Main>$(string[] args) { Console.WriteLine(args.Length); return 0; } }
```

### 3.2 Namespace ve `using` türleri

| Tür | Örnek | Not |
|-----|-------|-----|
| normal | `using System.Text;` | Tip adlarını kısaltır |
| `static` | `using static System.Math;` | `Sqrt(4)` — tip adı yazmadan static üyeler |
| **alias** | `using Json = System.Text.Json.JsonSerializer;` `using Point = (int X, int Y);` | Çakışan/uzun adlar; C# 12+ her tipe (tuple dahil) alias |
| **global** | `global using System.Diagnostics;` | Projedeki **tüm dosyalarda** geçerli |
| **implicit** | csproj: `<ImplicitUsings>enable</ImplicitUsings>` | SDK'nın `System`, `System.Linq`, `System.IO`, `System.Collections.Generic`, `System.Threading(.Tasks)`, `System.Net.Http` için ürettiği global using'ler (`obj/…GlobalUsings.g.cs`) |

**File-scoped namespace (C# 10):** `namespace X;` — dosyadaki her şey bu namespace'tedir, bir girinti seviyesi kazanılır. Namespace **klasör yapısını yansıtmalıdır** (bu repoda `Junior.CSharpFundamentals.ProgramAnatomy`).

### 3.3 Erişim belirleyicileri — tam tablo ⭐

| Belirleyici | Aynı sınıf | Türeyen (aynı assembly) | Aynı assembly | Türeyen (farklı assembly) | Her yer |
|-------------|:---------:|:----------------------:|:-------------:|:------------------------:|:-------:|
| `private` | ✅ | ❌ | ❌ | ❌ | ❌ |
| `private protected` | ✅ | ✅ | ❌ | ❌ | ❌ |
| `protected` | ✅ | ✅ | ❌ | ✅ | ❌ |
| `internal` | ✅ | ✅ | ✅ | ❌ | ❌ |
| `protected internal` | ✅ | ✅ | ✅ | ✅ | ❌ |
| `public` | ✅ | ✅ | ✅ | ✅ | ✅ |

Akılda tutma: `protected internal` = `protected` **VEYA** `internal` (birleşim, daha açık); `private protected` = `protected` **VE** `internal` (kesişim, daha kapalı).

- **Varsayılanlar:** tip → `internal`, üye → `private`. (İç içe tipte varsayılan `private`.)
- **Tutarlılık kuralı:** public bir metot, daha kısıtlı erişimli bir tipi imzasında kullanamaz (CS0050/CS0051).
- **`InternalsVisibleTo`:** `internal` üyeleri belirli "dost" assembly'lere açar; tipik kullanım **test projesi**. csproj'da `<InternalsVisibleTo Include="Proje.Tests" />`. Tasarım notu: `internal` yalnızca bir uygulama detayını gizler; sırf test edebilmek için her şeyi `public` yapmak yerine bunu kullanın.
- **Pratik kural:** en dar düzeyle başlayın; `private` → `internal` → `public` yönünde ihtiyaç oldukça açın. Kütüphane yazıyorsanız public yüzey bir **sözleşmedir**.

> 🛠 [`AccessModifiers/`](AccessModifiers/): `Vault` (6 düzeyden birer üye) + türeyen/türemeyen "dışarıdaki" sınıflar. `ConsoleApp` klasöründe `dotnet run -- J2.1 access-modifiers` geçerli erişimleri ve reflection ile CLR'ın gördüğü düzeyleri; `dotnet build -p:DefineConstants=SHOW_ERRORS` **derleme hatalarını** (CS0122, CS1540) gösterir.

**Alıştırma — başka assembly'den bakış.** `internal` ve `private protected` farkı ancak **iki assembly** ile gösterilebilir; repodan bağımsız, boş bir klasörde deneyin:

```bash
mkdir erisim && cd erisim
dotnet new classlib -n Lib && dotnet new console -n App && dotnet add App reference Lib
# Lib/Class1.cs → AccessModifiers/Vault.cs'teki Vault sınıfını (namespace'siz) yapıştırın
# App/Program.cs → aşağıdaki iki sınıfı yazıp her üyeye erişmeyi deneyin:
#   class Derived : Vault { void T() { _ = ProtectedMember; _ = ProtectedInternalMember; _ = PrivateProtectedMember; _ = InternalMember; } }
#   class Other   { void T(Vault v) { _ = v.PublicMember; _ = v.ProtectedInternalMember; _ = v.InternalMember; } }
dotnet build App      # hangi satırlar CS0122 / CS0103 / CS1061 verir?
```

<details><summary>Çözüm</summary>

`Derived` (başka assembly, **türeyen**): `ProtectedMember` ✅, `ProtectedInternalMember` ✅, `PublicMember` ✅; `PrivateProtectedMember` ✗ (CS0122: assembly farklı), `InternalMember` ✗ (CS0103: görünmez).
`Other` (başka assembly, **türemeyen**): yalnızca `PublicMember` ✅; `ProtectedInternalMember` ✗ (CS0122: ne aynı assembly ne türeyen), `InternalMember` ✗ (CS1061).
</details>

### 3.4 `static` class, static üye, static constructor

- **`static class`:** örneği oluşturulamaz, yalnızca static üye taşır, `sealed` gibidir (`Math`, `Path`, `Enumerable`). Yardımcı fonksiyonlar ve extension method'lar için.
- **static üye:** tip başına **tek kopya**; tüm çağıranlar aynı durumu paylaşır. Eleştiri: gizli global durum, **test edilebilirliği** düşürür ("her şeyi static yapma" → J2.3).
- **static constructor:** parametresiz, erişim belirleyicisiz, elle çağrılamaz; tip **ilk kullanılmadan önce** runtime tarafından **bir kez** ve thread-safe olarak çalıştırılır. `const`'a erişim tetiklemez. İçinde exception fırlarsa tip kalıcı olarak `TypeInitializationException` verir ([`Section03`](Sections/Section03_Statics.cs)). Zamanlamanın derin hali (`beforefieldinit`) → S1.2.

### 3.5 `partial`

Bir tipi (veya metodu) **birden çok dosyaya** bölmek. Amaç: elle yazılan kodu **üretilmiş koddan** ayırmak (designer, ORM, source generator).

- `partial class` — derleme sırasında dosyalar tek tipte birleşir ([`Invoice.cs`](Sections/Invoice.cs) + [`Invoice.Generated.cs`](Sections/Invoice.Generated.cs)).
- `partial` metot — bildirim ve uygulama ayrı dosyalarda. **Source generator'ların temel mekanizması:** siz `[GeneratedRegex(...)] static partial Regex Digits();` yazarsınız, derleyici **gövdeyi derleme sırasında üretir** (`System.Text.Json` source-gen, `LoggerMessage`, `GeneratedRegex` …) (→ S1.5).
- Tüm parçalar aynı erişim düzeyi/`partial` anahtar sözcüğüyle bildirilmeli; çelişen taban sınıf bildirimleri hata verir.

### 3.6 Statement vs expression, expression-bodied member

- **Expression** değer üretir (`x + 1`, `a ? b : c`, `x switch {…}`, atama, `throw` ifadesi). **Statement** iş yapar (`if`, `for`, `foreach`, `return`, bildirimler) ve genelde değer üretmez.
- **Expression-bodied member (`=>`):** gövdesi tek expression olan metot/property/indexer/operatör: `public double Area => Math.PI * r * r;`.
- `switch` **expression** (C# 8) değer döner, `if/else` zincirinden kısa ve eksik durumu derleyici uyarısıyla yakalar. Throw expression: `value ?? throw new ArgumentNullException(nameof(value))`.

### 3.7 Scope, gölgeleme, definite assignment

- Yerel değişken, tanımlandığı **en yakın `{}` bloğunda** yaşar. `for` değişkeni döngüye aittir.
- **Gölgeleme:** yerel değişken aynı adlı **alanı** gizleyebilir (`this._x` / tip adıyla erişilir); ama iç içe iki **yerel** aynı ada sahip olamaz (CS0136).
- **Definite assignment:** yerel değişken **okunmadan önce her kod yolunda atanmış** olmalı; derleyici akış analizi yapar (CS0165). `out` parametresi çağrıdan dönünce atanmış sayılır; `out var` ve pattern değişkenleri (`is string s`) çevreleyen kapsama sızar.

### 3.8 Tip dönüşümleri ⭐

| Yöntem | Davranış | Başarısızlık |
|--------|----------|--------------|
| **implicit** (`long l = i;`) | Veri kaybı yok → otomatik | Derleme hatası |
| **explicit cast** (`(int)3.99`) | Kaybı kabul ediyorsunuz; **keser, yuvarlamaz** | Taşma sessiz (unchecked) veya `OverflowException`; referans cast'te `InvalidCastException` |
| **`is`** | Tip testi (+ pattern: `o is string s`) | `false` |
| **`as`** | Referans/nullable dönüşüm | **`null` döner**, exception atmaz |
| **`Convert.ToXxx`** | `null`'ı 0 yapar, **banker's rounding** (2.5→2, 3.5→4) | `FormatException` / `OverflowException` |
| **`Parse`** | Metni çevirir | `FormatException`/`ArgumentNullException` (**exception**) |
| **`TryParse`** | Metni çevirir | **`false` döner**, exception yok |

**`Parse` vs `TryParse`:** kullanıcı/dış girdi için **`TryParse`** (hata beklenen bir durumdur, exception pahalıdır ve kontrol akışı için yanlış araçtır, → J2.5). Girdinin garantili doğru olduğu yerde (sabit metin, kendi ürettiğiniz veri) `Parse` kabul edilebilir.

**Kültür tuzağı:** `decimal.Parse("1.234,5")` `tr-TR`'de 1234,5; `Invariant`'ta hata. **Makineler arası veri (JSON, dosya, protokol) → `CultureInfo.InvariantCulture`**, kullanıcıya gösterim/girdi → kullanıcının kültürü (→ J2.2).

### 3.9 `checked` / `unchecked` ⭐

Tamsayı aritmetiğinde taşma **varsayılan olarak `unchecked`**: değer sessizce **sarar** (`int.MaxValue + 1 == int.MinValue`). Exception atmaz; yanlış para/stok hesabı üretir.

```csharp
int a = int.MaxValue;
unchecked(a + 1);     // -2147483648 (varsayılan)
checked(a + 1);       // OverflowException
checked { ... }       // blok içindeki tüm aritmetik/dönüşümler denetlenir
```

- Proje geneli: `<CheckForOverflowUnderflow>true</CheckForOverflowUnderflow>`.
- `decimal` her zaman denetlidir; `float`/`double` taşmada `Infinity` üretir. `Math.Abs(int.MinValue)` `OverflowException` fırlatır.
- Kaçınma yolu: yeterince geniş tip (`long`), kritik hesaplarda `checked`, hash/checksum gibi **bilerek** sarma isteyen yerlerde `unchecked`.

### 3.10 Operatörler

- **Öncelik** (yüksek → düşük, özet): `()` · `x++` · `! ~ ++x` · `* / %` · `+ -` · `<< >>` · `< > <= >= is as` · `== !=` · `&` · `^` · `|` · `&&` · `||` · `??` · `?:` · atama. Şüphede **parantez**.
- **Short-circuit ⭐:** `&&` ve `||` sağ tarafı gerekmedikçe değerlendirmez; mantıksal `&` ve `|` **her iki tarafı da** değerlendirir. Faydası: `text != null && text.Length > 0` null-guard'ı. Yan etkili sağ taraf varsa fark davranışı değiştirir ([`Section09`](Sections/Section09_Operators.cs)).
- Tamsayı bölmesi kesir atar (`7 / 2 == 3`); `%` işareti **sol operandı** izler (`-7 % 3 == -1`).

### 3.11 `nameof`, `typeof`, `sizeof`, `default`

| | Ne zaman değerlenir? | Sonuç |
|---|----------------------|-------|
| `nameof(x)` | Derleme zamanı | Sembolün adı (string); rename-safe, magic string'e karşı |
| `typeof(T)` | Derleme zamanı | `Type` nesnesi (`typeof(List<>)` açık generic) |
| `obj.GetType()` | Çalışma zamanı | Nesnenin **gerçek** tipi (`is` kalıtımı kapsar; `GetType() ==` tam eşleşme) |
| `sizeof(int)` | Derleme zamanı | Yerleşik value type boyutu (byte); kendi struct'ınız için `unsafe`/`Unsafe.SizeOf<T>()` |
| `default(T)` / `default` | Derleme zamanı | Value type → sıfırlanmış, reference type → `null` |

### 3.12 Preprocessor

| Yönerge | Etki |
|---------|------|
| `#if DEBUG` … `#else` … `#endif` | **Koşullu derleme:** seçilmeyen kod IL'e girmez. `DEBUG`, `RELEASE`, `NET10_0_OR_GREATER` gibi semboller SDK'dan gelir; `#define X` (dosya başı) veya `-p:DefineConstants` ile eklenir |
| `#region` / `#endregion` | Yalnızca editörde katlama. Büyük region'lar sınıfı bölme ihtiyacının işaretidir |
| `#nullable enable/disable/restore` | Nullable analizini bölgesel aç/kapat |
| `#pragma warning disable CSxxxx` / `restore` | Uyarıyı **bölgesel** sustur (nedenini yazın) |
| `#error` / `#warning` | Derleme sırasında kendi hata/uyarınızı üretin |

`#if` ≠ `if`: biri **derlemeye** hangi kodun gireceğini, diğeri çalışma zamanında hangi dalın işleyeceğini belirler. `[Conditional("DEBUG")]` metot çağrılarını (argümanlarıyla birlikte) Release'de siler (→ J1.5).

### 3.13 XML documentation ve Caller attribute'ları

- **`///`** yorumları IntelliSense'te görünür; csproj'da `<GenerateDocumentationFile>true` ile `.xml` üretilir; Swashbuckle/OpenAPI bu dosyayı okuyup endpoint açıklamalarına taşır (→ J7.5). Sık etiketler: `<summary>`, `<param>`, `<returns>`, `<exception cref>`, `<remarks>`, `<example>`, `<see cref>`. Eksik yorum uyarısı CS1591 (public yüzeyi olan kütüphanelerde faydalı).
- **`[CallerMemberName]`, `[CallerFilePath]`, `[CallerLineNumber]`:** çağıranın bilgisi **derleme zamanında** parametre varsayılanına yazılır — reflection/`StackTrace` yok, sıfır maliyet. **`[CallerArgumentExpression]`:** argümanın kaynak koddaki **metnini** verir; `ArgumentNullException.ThrowIfNull(x)` `paramName`'i böyle bulur, kendi `Guard` metotlarınızı da yazabilirsiniz ([`Section13`](Sections/Section13_CallerInfo.cs)). `INotifyPropertyChanged` uygulamalarında `[CallerMemberName]` klasiktir.

---

## 4. Gerçek dünya senaryosu

**"Test projesi iç sınıfı göremiyor, ekip her şeyi `public` yapıyor."** Kütüphane yazarı `PriceRules` gibi iç hesaplamaları `internal` tutar; test projesine `InternalsVisibleTo` ile kapıyı açar. Böylece public API yüzeyi küçük kalır, iç kural yine test edilir. Public yapılan her şey artık geriye dönük uyumluluk borcudur.

**"Stok toplamı negatif çıktı."** `int itemCount * int unitPriceInCents` taşar ve **sessizce** negatif sonuç üretir ([`Section08`](Sections/Section08_CheckedUnchecked.cs): 70.000 × 40.000). Çözüm: `long`/`decimal` kullanımı, kritik hesaplarda `checked`, ve sınır değer testi.

**"Sunucuda fiyat 1234,5 yerine 0 geliyor."** `decimal.Parse` geliştirici makinesinde `tr-TR`, sunucuda `en-US` kültürüyle çalışıyor. Makineler arası veri için daima `InvariantCulture` (veya `TryParse`).

---

## 5. Mülakat perspektifi

Sık sorulanlar ([`interview-questions.md`](interview-questions.md)): erişim belirleyicileri tablosu (özellikle `protected internal` vs `private protected`) ⭐ · `Parse` vs `TryParse` ⭐ · `checked`/`unchecked` · top-level statements derleyici ne üretir · `partial` ne için · `&&` vs `&` · `Convert` vs cast · `typeof` vs `GetType`.

İpucu: erişim tablosunu **yazarak** değil, "kimden kime" cümleleriyle anlatın: *"private protected: yalnızca aynı assembly'deki türeyenler."* Sonra `InternalsVisibleTo`'yu test örneğiyle bağlayın.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| Her şeyi `public` yapmak | En dar düzey; `internal` + `InternalsVisibleTo` |
| `protected internal` ile `private protected`'ı karıştırmak | Birleşim (VEYA) vs kesişim (VE) |
| Kullanıcı girdisinde `int.Parse` | `TryParse` (exception beklenen durum için yanlış araç) |
| Para/miktar hesabında `int` ve taşmayı düşünmemek | `decimal`/`long`, kritik yerde `checked` |
| `(int)x` ile yuvarlama beklemek | Keser. `Math.Round` / `Convert` (banker's!) / `MidpointRounding` |
| `Convert.ToInt32(2.5)`'in 3 olacağını varsaymak | Banker's rounding → 2 |
| `&`/`|` ile null-guard yazmak | `&&`/`||` (short-circuit) |
| Kültüre bağlı `Parse`/`ToString` ile makineler arası veri taşımak | `CultureInfo.InvariantCulture` |
| `static` ile gizli global durum biriktirmek | Bağımlılığı enjekte edin (→ J6, M11) |
| `#region` ile uzun sınıfı "düzenli" göstermek | Sınıfı sorumluluklara bölün |
| Uyarıyı gerekçesiz `#pragma` ile susturmak | Neden yazın; en dar kapsam |
| `magic string` ile parametre adı yazmak | `nameof(...)` / `[CallerArgumentExpression]` |

---

## 7. Performans & trade-off

- **`checked`:** taşma denetimi çok küçük maliyetlidir; kritik hesapta güvenlik için değer, sıcak döngüde ölçerek karar verin.
- **Caller attribute'ları** derleme zamanında çözüldüğünden `StackTrace` tabanlı yöntemlere göre **ücretsizdir**.
- **`Parse` + `try/catch` vs `TryParse`:** hatalı girdi sık ise `TryParse` kat kat hızlıdır (exception maliyeti, → J2.5).
- **Source generator (`partial`):** çalışma zamanı reflection maliyetini derleme zamanına taşır (hızlı başlangıç, trimming/AOT dostu) ama derleme süresi/ek kod üretir (→ S1.5).
- **`static` erişim** hızlıdır ama bağımlılığı gizler ve test edilebilirliği düşürür.
- **Implicit/global using:** gürültüyü azaltır, ancak adın nereden geldiğini gizler; çakışmada alias veya tam nitelikli ad kullanın.

---

## 8. Kaynaklar

- C# dil referansı — erişim belirleyicileri: <https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/accessibility-levels>
- Top-level statements: <https://learn.microsoft.com/dotnet/csharp/fundamentals/program-structure/top-level-statements>
- `using` yönergeleri ve global using: <https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/using-directive>
- `checked` ve `unchecked`: <https://learn.microsoft.com/dotnet/csharp/language-reference/statements/checked-and-unchecked>
- Preprocessor yönergeleri: <https://learn.microsoft.com/dotnet/csharp/language-reference/preprocessor-directives>
- XML documentation comments: <https://learn.microsoft.com/dotnet/csharp/language-reference/xmldoc/>
- Caller information attribute'ları: <https://learn.microsoft.com/dotnet/csharp/language-reference/attributes/caller-information>
- `InternalsVisibleTo`: <https://learn.microsoft.com/dotnet/standard/assembly/friend-assemblies>
- Source generator ve `GeneratedRegex`: <https://learn.microsoft.com/dotnet/standard/base-types/regular-expression-source-generators>
