# J2.2 Tip Sistemi & Bellek Modeli ⭐

> 📁 `ConsoleApp/01-junior/02-csharp-fundamentals/02-type-system-memory/` · Önkoşul: J2.1 · Sorular: [`interview-questions.md`](interview-questions.md)

Mülakatın en klasik açılışı: *"Value type ile reference type farkı nedir?"* — ama **"biri stack'te, biri heap'te"** cevabı yetmez.

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

.NET'in **Common Type System**'i (CTS): her değerin bir tipi vardır ve bu tip, değerin **nasıl kopyalandığını**, **nerede yaşadığını** ve **nasıl karşılaştırıldığını** belirler. Bu konu; value/reference ayrımını, `struct`/`class`/`record` seçimini, boxing'i, `string`'in özel durumunu, null'ı, parametre geçirme biçimlerini (`ref/out/in`), tuple'ları ve enum'ları kapsar.

Konu 12 deney içerir (`dotnet run -- J2.2 <number>`) ve ayrıca const gömülmesi deneyi (`dotnet run -- J2.2 const-embedding`, 3.9'da): `const` ile `static readonly` farkını IL'de gösterir; gerçek iki-assembly senaryosu README'deki alıştırmadadır.

---

## 2. Neden?

- Kopyalama semantiğini bilmeyen geliştirici "listeden aldığım nesneyi değiştirdim ama değişmedi" ya da "kopyaladım ama orijinal de değişti" hatalarını üretir.
- Gizli **boxing** ve string birleştirmeleri performans sorunlarının sessiz kaynağıdır.
- `ToUpper()` gibi kültüre duyarlı bir çağrı yalnızca Türkçe işletim sistemli kullanıcılarda hata verebilir — "bende çalışıyor" hatalarının klasiği.
- `record`, `struct`, nullable ve `ref` yapılarını doğru seçmek API tasarımının temelidir.

---

## 3. Nasıl?

### 3.1 Unified type system

Her tip doğrudan veya dolaylı olarak `System.Object`'ten türer — value type'lar dahil (`Int32 → ValueType → Object`). `int` yalnızca `System.Int32`'nin C# takma adıdır (`5.ToString()` bu yüzden çalışır). Bu, her şeyi `object`'e atayabilmeyi sağlar; bedeli value type'lar için **boxing**'dir (3.5). ([`Section01`](Sections/Section01_UnifiedTypeSystem.cs))

### 3.2 Value type vs reference type ⭐

| | Value type | Reference type |
|---|------------|----------------|
| Örnekler | `int`, `double`, `bool`, `char`, `DateTime`, `enum`, `struct`, `record struct`, `ValueTuple` | `class`, `record`, `interface`, `delegate`, `string`, `T[]`, `object` |
| **Atama/geçirme** | **Değer kopyalanır** → iki bağımsız kopya | **Referans kopyalanır** → ikisi aynı nesneyi gösterir |
| Varsayılan `==` | Alanların değeri (`Equals`; `==` yalnızca tanımlıysa) | **Referans eşitliği** (aynı nesne mi?) |
| `null` | Olamaz (`int?` ile olur) | Olabilir |
| Taban | `System.ValueType` | `System.Object` |

**Asıl fark kopyalama semantiğidir.** Yaygın yanılgı: *"value type her zaman stack'tedir."* Doğrusu: **bir değer, onu tutan değişkenin yaşadığı yerdedir.**

| Value type nerede? | Nerede yaşar? |
|--------------------|----------------|
| Yerel değişken (closure'a yakalanmamış) | Stack (veya register) |
| Bir **sınıfın alanı** | Sınıf nesnesiyle birlikte **heap** |
| **Dizi / `List<T>`** elemanı | Dizinin belleği, yani **heap** |
| Lambda/closure'a **yakalanan** yerel değişken | Derleyicinin ürettiği sınıf → **heap** |
| **Boxing**'e uğrayan | Heap'te yeni nesne |

Tersi de doğrudur: *"reference type heap'tedir"* — referansın kendisi (işaretçi) yerel değişkense stack'te durur; heap'te duran nesnedir. (Stack/heap ayrıntısı bir **uygulama detayıdır**; semantiği değil, bellek yerleşimini ve GC baskısını etkiler. Derin hali → S1.3.)

**Metoda geçirme** de aynı kuraldır — her şey varsayılan olarak **değer ile (by value)** geçer. Reference type'ta kopyalanan *referanstır*: nesnenin içeriğini değiştirebilirsiniz ama referansa yeni nesne atamak çağıranı etkilemez (etkilemesi için `ref`, 3.10). ([`Section02`](Sections/Section02_ValueVsReference.cs))

### 3.3 `struct` vs `class` vs `record` vs `record struct` ⭐

| | `class` | `record` (class) | `struct` | `record struct` |
|---|---------|------------------|----------|-----------------|
| Anlam | referans | referans | değer | değer |
| Eşitlik | referans | **değer** (otomatik) | elle `Equals` | **değer** (otomatik) |
| `with` | ✗ | ✓ | ✗ | ✓ |
| Kalıtım | ✓ | ✓ (record'tan) | ✗ | ✗ |
| Varsayılan | `null` | `null` | sıfırlanmış | sıfırlanmış |
| Ne zaman? | Varsayılan seçim; kimlik, davranış, büyük/mutable nesne | Değişmez veri taşıyıcı, DTO, domain value object | Küçük, immutable, kısa ömürlü değer | Küçük değer nesnesi (`Money`, `Point`) |

**Struct ne zaman?** Microsoft önerisi: tek bir mantıksal değeri temsil eder, örnek boyutu **16 byte altı**, **immutable**, sık boxing'e uğramaz. Aksi halde `class`. Struct'ın kopyalama maliyeti büyüdükçe avantajı biter (büyük struct'lar `in` ile geçirilir).

**Mutable struct neden tehlikeli?** Kopyaya yazıp orijinalin değiştiğini sanırsınız ([`Section04`](Sections/Section04_MutableStruct.cs)):

```csharp
var list = new List<Counter> { new() };
list[0].Value++;          // ✗ CS1612 — the indexer returns a COPY; the compiler protects you
var c = list[0]; c.Value++; list[0] = c;   // copy-modify-assign back
```
`foreach` değişkeni kopyadır; `readonly` alandaki struct'ın metodu **savunmacı kopya** üzerinde çalışır (sessizce işe yaramaz); interface üzerinden değiştirmek *kutulanmış kopyayı* değiştirir. Çözüm: **`readonly struct` / `readonly record struct`** — değişiklik gerekirse `with` ile yeni kopya.

### 3.4 Eşitlik özeti

`==` ve `Equals` varsayılanı: reference type → referans eşitliği; struct → alan alan karşılaştırma (reflection tabanlı yavaş varsayılan! kendi `IEquatable<T>`'nizi yazın, → J2.4); `record`/`record struct` değer eşitliğini derleyici üretir; `string` `==`'yi **operator overload** ile değer karşılaştırması yapar — ama `object` tipli iki string `==` ile **referans** karşılaştırır (overload derleme zamanı tipine bağlıdır).

### 3.5 Boxing / unboxing ⭐

**Boxing:** value type'ı bir `object`/interface kutusuna koymak → **heap'te yeni nesne tahsisi + değer kopyası**. **Unboxing:** tip kontrolü + kopyalama; **tam tipe** yapılmalıdır (`(long)(object)42` → `InvalidCastException`).

```csharp
object o = 42;          // boxing
int i = (int)o;         // unboxing
```

**Gizlice olduğu yerler** ([`Section05`](Sections/Section05_Boxing.cs), ölçümlü):
- `object` parametre alan metoda value type geçirmek,
- value type'ı **interface**'e atamak (`IComparable c = 5`; struct → `IShape`),
- non-generic koleksiyonlar (`ArrayList`, `Hashtable`),
- `string.Format("{0}", 5)` / eski `params object[]` metotları,
- enum üzerinde `HasFlag` / `Enum` yardımcıları (eski runtime'larda), `object`-tabanlı karşılaştırmalar.

**Maliyeti:** her boxing = 24 byte civarı tahsis (header + method table + değer) → **GC baskısı** (deneyde 5 milyon eleman: `ArrayList` ~3 Gen0 GC ve ~2,5 kat yavaş, `List<int>` sıfır GC). **Kaçınma:** generic koleksiyonlar (`List<T>`), generic metotlar ve `where T : struct, IInterface` kısıtı (boxing'siz çağrı), `object` yerine generic parametre. Ölçüm kabadır (`Stopwatch`); gerçek benchmark → S2.1 (BenchmarkDotNet).

### 3.6 `string` ⭐

- **Immutable:** her "değişiklik" yeni string üretir. Sonuçları: thread-safe paylaşım, güvenli `Dictionary` anahtarı, interning mümkün, güvenlik; bedeli: döngüde `+=` **O(n²)** (deneyde 20.000 birleştirme ≈ 380 MB tahsis, `StringBuilder` ≈ 0,1 MB). Tek ifadede `a + b + c` için `StringBuilder` gerekmez (derleyici `string.Concat`).
- **Interning:** aynı literal'ler tek nesneyi paylaşır (`string.Intern` ile runtime string'i de havuzlanabilir); ama `ReferenceEquals` ile string karşılaştırmak yanlıştır — daima `==`/`Equals`.
- **`==` neden değer karşılaştırır?** `string` `==` operatörünü overload eder (iki derleme zamanı `string` ise). Referans tipi olduğu halde value-like davranır.
- **Kültür ve "Türkçe I problemi" ⭐:** Türk kültüründe `i` ↔ `İ`, `I` ↔ `ı`. 

```csharp
"title".ToUpper(new CultureInfo("tr-TR"))   // "TİTLE"  (invariant: "TITLE")
"FILE".ToLower(new CultureInfo("tr-TR"))    // "fıle"   → NOT EQUAL to "file"
if (cmd.ToLower() == "file")                // false on Turkish devices! (the typical "breaks only on my/the customer's machine" bug)
```
**Kural:** dil-bağımsız tanımlayıcılar (komut, dosya yolu, URL, header, config anahtarı, enum adı) → `StringComparison.Ordinal`/`OrdinalIgnoreCase`, dönüşümde `ToUpperInvariant()`/`ToLowerInvariant()`. Kullanıcıya gösterilen ve sıralanan metin → kültür duyarlı. Analyzer'lar (CA1304, CA1307, CA1309, CA1310, CA1311) bunu yakalar. Bu repoda kökte `InvariantGlobalization=true` olduğundan, kültür deneyleri için proje bu ayarı `false` yapar (ICU gerekir; Alpine container'da `icu-libs` kurulumu veya invariant mod bilinçli seçilmelidir → J12).

### 3.7 Nullable

- **Nullable value types:** `int?` = `Nullable<int>` — **gerçek bir struct** (`HasValue` + `Value`). `null.Value` → `InvalidOperationException`. Lifted operatörler: `null + 1 = null`, `null < 5 = false` (`null >= 5` de false!). Boxing'de `Nullable<T>` kutulanmaz: `null` → `null` referans, dolu → `T`'nin kutusu.
- **Nullable reference types:** `string?` **yalnızca derleme zamanı** analizidir; çalışma zamanında ayrı bir tip yoktur (`typeof(string?)` bile yazılamaz), yalnızca metadata'da öznitelik olarak tutulur ve **zorlanmaz**. `!` (null-forgiving) uyarıyı susturur, null'ı kaldırmaz (→ J1.5).
- **Operatörler:** `?.` (null ise zinciri kes), `?[]`, `??` (null ise varsayılan), `??=` (null ise ata). `order?.Customer?.Address?.City ?? "no address"` üç seviye kontrolü tek satıra indirir.

### 3.8 `var` vs `dynamic` vs `object`

`var` **derleme zamanı** çıkarımıdır — hâlâ statik tipli ve güvenli (anonim tipler için zorunlu). `object` her şeyi tutar ama üyelere erişmek için cast gerekir, value type'ta boxing yapar. `dynamic` bağlamayı **çalışma zamanına** erteler: derleme güvenliği ve IntelliSense yok, hata `RuntimeBinderException` olarak çalışırken çıkar, ilk çağrıda binder maliyeti vardır → COM/Office interop veya şemasız veri gibi nadir durumlar dışında kaçının. ([`Section08`](Sections/Section08_VarDynamicObject.cs))

### 3.9 `const` vs `readonly` vs `static readonly`

| | `const` | `static readonly` | `readonly` (örnek) |
|---|---------|-------------------|--------------------|
| Atama zamanı | **Derleme zamanı** | Çalışma zamanı, bir kez (static ctor) | Constructor/initializer |
| İzin verilen değer | İlkel, `string`, `enum`, `null` | Her şey | Her şey |
| Bellekte | Yok (IL'e gömülür) | Static alan | Nesne alanı |
| **Çağıran assembly** | Değer **çağıranın IL'ine kopyalanır** | Çalışırken okunur | — |

**Versiyonlama tuzağı** ⭐: Kütüphanede `public const int MaxRetries = 3` → 5 yapıp yalnızca kütüphaneyi dağıtırsanız, onu kullanan **yeniden derlenmemiş** uygulama hâlâ 3'ü kullanır. Aşağıdaki deney bunu kanıtlar. Kural: public ve değişebilir değer → `static readonly`/property; `const` yalnızca gerçekten değişmeyenler için. (Optional parametre varsayılanları da çağırana derlenir → J2.4.) `readonly` ≠ immutable: alan yeniden atanamaz ama `readonly List<int>`'in içeriği değişir.

**Deney — const gömülmesini kanıtlayın.** (a) `ConsoleApp` klasöründe `dotnet run -- J2.2 const-embedding`: iki metot `Config`'e aynı biçimde bakar; IL'de `const` okuyan metot `ldc.i4.3` (3 IL'in **içine yazılı**), `static readonly` okuyan metot `ldsfld` (alanı **çalışırken** okur) içerir. (b) Asıl senaryo (kütüphane güncellenir, uygulama yeniden derlenmez) iki assembly ister; repodan bağımsız, boş bir klasörde:

```bash
mkdir constexperiment && cd constexperiment
dotnet new classlib -n Lib && dotnet new console -n App && dotnet add App reference Lib
printf 'public static class Config\n{\n    public const int MaxRetries = 3;\n    public static readonly int Timeout = 30;\n}\n' > Lib/Class1.cs
echo 'Console.WriteLine($"MaxRetries={Config.MaxRetries}, Timeout={Config.Timeout}");' > App/Program.cs

# 1) build and run with v1                 → MaxRetries=3, Timeout=30
dotnet build -nologo -v q App && dotnet App/bin/Debug/net10.0/App.dll

# 2) make Lib v2, build ONLY Lib, copy the new dll next to App (do NOT REBUILD App!)
sed -i 's/= 3;/= 5;/; s/= 30;/= 60;/' Lib/Class1.cs
dotnet build -nologo -v q Lib && cp Lib/bin/Debug/net10.0/Lib.dll App/bin/Debug/net10.0/

# 3) run again                             → Timeout=60 (read at run time) but MaxRetries is STILL 3 (it was embedded in App's IL)
dotnet App/bin/Debug/net10.0/App.dll

# 4) rebuild App                           → MaxRetries is now 5
dotnet build -nologo -v q App && dotnet App/bin/Debug/net10.0/App.dll
```

### 3.10 `ref`, `out`, `in`

| | Çağıranda önceden atanmış? | Metot yazabilir? | Kopya? |
|---|:---:|:---:|:---:|
| (yok) | ✓ | yerel kopyaya | değer kopyalanır |
| `ref` | ✓ zorunlu | ✓ (okuyabilir de) | **referans** geçer |
| `out` | ✗ gerekmez | ✓ **zorunlu** (her yolda) | referans |
| `in` | ✓ | ✗ salt okunur | referans (kopyasız) |

`out` birden çok değer döndürme ve `TryXxx` deseni için; `in` büyük `readonly struct`'ları kopyalamadan geçirmek için; `ref` local/return dizi elemanına yerinde erişim. `async` metotlarda ve iterator'larda `ref/out/in` parametre kullanılamaz. Modern kodda `out` yerine tuple/record dönmek çoğu zaman daha okunaklıdır. ([`Section10`](Sections/Section10_RefOutIn.cs))

### 3.11 Tuple ve deconstruction

`ValueTuple` (`(int Min, int Max)`) **struct**'tır (eski `System.Tuple<>` class'tı), alanları mutable'dır, isimler yalnızca **derleme zamanı** süsüdür (`Item1`, `Item2` asıl alanlar). `var (min, max) = MinMax(...)` ve `(x, y) = (y, x)` ile ayrıştırma/swap; kendi tipinize `Deconstruct` metodu ekleyebilirsiniz. Özel/yerel çok değerli dönüş için ideal; **public API sınırında veya anlamlı domain kavramı için `record`/`class`** (isimli tip, belgelenebilir, evrilebilir).

### 3.12 `enum` ve `[Flags]`

- `enum` adlandırılmış tamsayı sabitleridir (varsayılan taban `int`). **Her tamsayı enum'a cast edilebilir** — `(OrderStatus)99` exception atmaz, geçersiz değer üretir; `Enum.IsDefined` ile doğrulayın. `Enum.TryParse("99")` da *başarılı* olur (sayısal metin) → yine `IsDefined`. `switch`'te `default` dalında geçersiz değeri **öne çıkarın**.
- `[Flags]`: değerler **2'nin kuvvetleri** (veya bileşimleri); `|` ile birleştir, `& ~X` ile kaldır, `HasFlag` ya da `(v & X) != 0` ile test et; `ToString()` "Read, Write" yazar. `0` için `None` tanımlayın. ([`Section12`](Sections/Section12_Enums.cs))

---

## 4. Gerçek dünya senaryosu

**"Sadece Türkçe Windows kullanan müşterilerde dosya yükleme komutu çalışmıyor."** Kod `if (action.ToLower() == "file")` ile bir komutu karşılaştırıyor; `FILE` Türk kültüründe `fıle` olduğu için hiçbir zaman eşleşmiyor. Geliştirici makineleri `en-US`, müşteri `tr-TR`. Çözüm `StringComparison.OrdinalIgnoreCase`; kalıcı önlem `CA1309/CA1310` analyzer kurallarının hata seviyesine çekilmesi ve kodun `tr-TR` kültüründe de denenmesi (bu konudaki Section06 aynı hatayı üretir).

**"Yüksek hacimli log işleme servisinde GC baskısı."** Profil: metrik değerleri `List<object>`'te tutuluyor, her sayı boxing → saniyede milyonlarca küçük nesne, Gen0 GC patlaması. Çözüm: `List<double>`/generic metot (`ArrayList → List<int>` deneyi); kritik yolda `where T : struct, IMetric` ile boxing'siz çağrı.

**"Fiyat listesini güncelledik ama müşteri uygulaması eski limiti kullanıyor."** Ortak kütüphanedeki `public const decimal`... (`const` çağıranın IL'ine gömülür; kütüphane güncellenmiş ama istemci yeniden derlenmemiştir). `static readonly`/yapılandırma kullanılmalıydı.

---

## 5. Mülakat perspektifi

Sık sorulanlar: Value type vs reference type ⭐ (kopyalama semantiği + "stack/heap" yanılgısı) · `struct` ne zaman? · Boxing nedir, ne zaman olur, maliyeti? · String neden immutable? · `==` string'de neden değer karşılaştırır? · Türkçe I problemi · `const` vs `readonly` · `ref`/`out`/`in` · `var` vs `dynamic` · `int?` ile `string?` farkı.

**İpucu:** "Value type stack'te" derseniz mülakatçı muhtemelen *"sınıf alanı olan struct nerede?"* diye sorar. Cevabı **kopyalama semantiğiyle** başlatın, sonra bellek yerleşiminin değişkenin yaşadığı yere bağlı olduğunu örnekleyin.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| "Value type stack'te, reference type heap'te" | Kopyalama semantiği asıl fark; yerleşim değişkenin yaşadığı yere bağlı |
| Mutable struct kullanmak | `readonly struct`/`record struct`; değişim için `with` |
| Büyük (>16B) mutable struct tercih etmek | `class`/`record` ya da `readonly struct` + `in` |
| `ArrayList`/`object` koleksiyonları | Generic koleksiyonlar |
| Döngüde `string +=` | `StringBuilder` (veya `string.Join`/`Concat`) |
| `ToLower()/ToUpper()` ile tanımlayıcı karşılaştırmak | `StringComparison.OrdinalIgnoreCase` |
| `ReferenceEquals` ile string karşılaştırmak | `==` / `Equals(…, comparison)` |
| `(Enum)sayı` cast'ine güvenmek | `Enum.IsDefined` / `TryParse` + doğrulama |
| `public const` ile değişebilir değer yayınlamak | `static readonly`/property |
| `dynamic`'i "kolay çözüm" saymak | Generic/interface/pattern matching |
| `string?` için çalışma zamanı koruması beklemek | NRT yalnızca derleyici ipucudur; sınırda gerçek doğrulama |
| `readonly` = immutable sanmak | Alan yeniden atanamaz, içerik değişebilir |
| `Nullable<T>.Value`'yu kontrolsüz okumak | `HasValue`, `GetValueOrDefault`, `??` |

---

## 7. Performans & trade-off

- **struct:** kısa ömürlü küçük değerlerde heap tahsisini ve GC baskısını ortadan kaldırır; ama büyüdükçe kopyalama maliyeti artar, boxing'e uğrarsa kazanç tersine döner. Ölçmeden "struct hızlıdır" demeyin.
- **Boxing:** tek boxing ucuzdur (~onlarca ns + 24B); sıcak döngülerde milyonlarca kutu GC'yi tetikler. `ArrayList → List<T>`, interface çağrılarında generic kısıt, `Span<T>` (→ S2).
- **String:** immutability güvenlik/paylaşım getirir, dönüştürme maliyetini siz yönetirsiniz: `StringBuilder`, `string.Create`, `Span<char>`, interpolated string handler'lar (→ S2.3).
- **Interning:** bellek tasarrufu/hız sağlar ama `string.Intern` havuzu ömürlüdür — kullanıcı girdisini interning'lemeyin.
- **`in`/`ref readonly`:** büyük struct'larda kopyayı azaltır, ancak savunmacı kopyaya yol açmaması için struct'ın `readonly` olması gerekir.
- **`record` değer eşitliği:** kullanışlıdır ama alan sayısı kadar karşılaştırma yapar; büyük koleksiyon alanları içeren record'larda eşitlik pahalıdır ve koleksiyon içeriğini karşılaştırmaz (referans karşılaştırır).
- **Kültür duyarlı karşılaştırma** ordinal'dan yavaştır (ICU çağrıları); tanımlayıcılarda ordinal hem doğru hem hızlıdır.

---

## 8. Kaynaklar

- Tip sistemi (C# rehberi): <https://learn.microsoft.com/dotnet/csharp/fundamentals/types/>
- Value types ve reference types: <https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/value-types>
- Struct tasarım yönergeleri: <https://learn.microsoft.com/dotnet/standard/design-guidelines/choosing-between-class-and-struct>
- Boxing ve unboxing: <https://learn.microsoft.com/dotnet/csharp/programming-guide/types/boxing-and-unboxing>
- String'ler için en iyi uygulamalar (kültür/karşılaştırma): <https://learn.microsoft.com/dotnet/standard/base-types/best-practices-strings>
- Türkçe I problemi ve globalization: <https://learn.microsoft.com/dotnet/standard/globalization-localization/globalization>
- Nullable reference types: <https://learn.microsoft.com/dotnet/csharp/nullable-references>
- `ref`, `in`, `out`: <https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/ref>
- Enum tasarım yönergeleri: <https://learn.microsoft.com/dotnet/standard/design-guidelines/enum>
- Jon Skeet — *C# in Depth*, "Parameter passing in C#": <https://jonskeet.uk/csharp/parameters.html>
