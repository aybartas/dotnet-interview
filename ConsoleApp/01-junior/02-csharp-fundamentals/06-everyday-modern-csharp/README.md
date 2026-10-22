# J2.6 Günlük Modern C#

> 📁 `ConsoleApp/01-junior/02-csharp-fundamentals/06-everyday-modern-csharp/` · Önkoşul: J2.5 · Sorular: [`interview-questions.md`](interview-questions.md)

2026'da junior bir geliştiricinin her gün yazdığı sözdizimi. Sürüm sürüm tarihçe ve ileri özellikler → M1.1.

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

C# son on yılda "ne yapılacağını" kısa ve deklaratif söyleyen bir dile dönüştü: `record`, pattern matching, `switch` expression, raw string literal, collection expression, primary constructor… Bu konu, yeni kodda **her gün** göreceğiniz özellikleri kapsar.

Kodun merkezi: **aynı sipariş domain modeli iki kez yazıldı** — [`Csharp7Style/Domain.cs`](Csharp7Style/Domain.cs) (elle `Equals`/`GetHashCode`, `switch` statement, `StringBuilder`, blok namespace) ve [`Csharp14Style/Domain.cs`](Csharp14Style/Domain.cs) (record, pattern, raw string, `field`…). [`Section10`](Sections/Section10_SideBySide.cs) iki stilin **aynı davranışı** verdiğini gösterir ve [`LineCounter`](Comparison/LineCounter.cs) ile satırları sayar: **224 → 56 satır (~%75 azalma)**.

---

## 2. Neden?

- Yeni kod tabanı, eğitim materyali ve kütüphane örnekleri modern sözdizimiyle yazılır; okuyamayan geliştirici ekibe katılamaz.
- Daha az boilerplate = daha az hata: elle yazılmış `Equals`/`GetHashCode` sürümleri tutarsızlaşır, `switch` statement'ta unutulan atama sessiz kalır, `required` ile "yarım nesne" derleme hatasına dönüşür.
- Mülakatta "record ile class farkı?" ve "pattern matching nedir?" sıradan sorulardır.

---

## 3. Nasıl?

### 3.1 Records ⭐

```csharp
public record Person(string First, string Last, int Age);        // positional record
var p2 = p1 with { Age = 31 };                                    // non-destructive mutation
var (first, last, age) = p1;                                      // Deconstruct
p1 == p2                                                          // DEĞER eşitliği
```
Derleyici üretir: init-only property'ler, ctor, `Deconstruct`, `Equals`/`GetHashCode`, `ToString` (`Person { First = Ayşe, … }`), `==`/`!=`, `with` desteği.

| | `class` | `record` | `record struct` / `readonly record struct` |
|---|---------|----------|-----------------------------|
| Tip | referans | referans | **değer** |
| Eşitlik | referans | **değer** | **değer** |
| Ne zaman | Kimlik + davranış + mutable durum (entity) | Değişmez veri taşıyıcı, DTO, value object | Küçük değer nesnesi (`Money`, `Point`) |

- **Uyarılar:** (1) positional `record struct` varsayılan **mutable**'dır → `readonly record struct`. (2) Kalıtımda türeyen record farklı tiptir: `Employee` ≠ aynı değerli `Person`. (3) **Koleksiyon alanları referansla karşılaştırılır** (`Basket("b", [1,2]) != Basket("b", [1,2])`) ve `with` **shallow**'dur (→ J2.4). (4) EF Core entity'si için uygun değildir (kimlik/izleme); DTO ve sorgu sonuçları için idealdir (→ J9, M7).

### 3.2 `init` ve `required`

`init`: yalnızca **nesne oluşturulurken** (initializer/ctor/`with`) atanır, sonra salt okunur. `required` (C# 11): oluştururken atanması **zorunlu** (aksi halde CS9035). İkisi birleşince ctor'suz, eksiksiz, değişmez DTO yazılır:

```csharp
public sealed class OrderDto { public required string Id { get; init; } public required decimal Total { get; init; } }
```
`System.Text.Json` ikisini de destekler. Constructor'da `[SetsRequiredMembers]` kontrolü atlar (dikkatli).

### 3.3 Pattern matching ⭐

| Pattern | Örnek |
|---------|-------|
| type | `x is string s` (kontrol + cast + değişken) |
| constant | `x is null`, `x is 0` |
| **relational** (C# 9) | `x is > 5`, `score is >= 0 and < 100` |
| **logical** | `and`, `or`, `not` → `x is not null`, `day is Saturday or Sunday` |
| **property** | `o is { Total: > 1000 }`, iç içe: `o is { Customer.Address.City: "Ankara" }` |
| positional / tuple | `(tier, subtotal) switch { (Gold, > 1000m) => … }` |
| list (C# 11) | `arr is [1, .., 3]`, `[var first, .., var last]` |

Eskiden `as` + null kontrolü veya `(T)` cast + `is` gerekirdi. Null kontrolünde `x is not null`, `!=` operatörü overload edilmiş tiplerde yanıltıcı olabileceğinden tercih edilir. İç içe property pattern'de ara değer `null` ise eşleşme sessizce başarısız olur (patlamaz). ([`Section03`](Sections/Section03_Patterns.cs))

### 3.4 `switch` expression

```csharp
decimal DiscountRate => (Customer.Tier, Subtotal) switch
{
    (CustomerTier.Platinum, > 1000m) => 0.20m,
    (CustomerTier.Platinum, _)       => 0.15m,
    (CustomerTier.Gold, > 1000m)     => 0.15m,
    _                                => 0m,
};
```
Bir **değer** üretir (atanır/döndürülür), `break` yok, `_` varsayılan, `when` guard, `throw` ifadesi. Derleyici **kapsamlılık** uyarısı verir (CS8509); eşleşme yoksa çalışma zamanında `SwitchExpressionException`. **Dal sırası önemlidir**: özelden genele; öncekini gölgeleyen dal derleme hatasıdır.

### 3.5 String interpolation ve raw string literal

- `$"{name,-8}|{price,10:N2}|{date:yyyy-MM-dd}"` — ifade + hizalama + biçim. Makineler arası metin için `FormattableString.Invariant(...)`.
- **Raw string literal** (C# 11): `"""…"""` — içeride **kaçış gerekmez** (JSON, SQL, regex, HTML için ideal); kapanış `"""`'in girintisi içerikten kırpılır. Interpolation: `$$"""…{{ifade}}…"""` — tek `{ }` literal kalır (JSON'a ait).
- Verbatim `@"…"` yerini büyük ölçüde raw string'e bıraktı. **Güvenlik:** kullanıcı girdisini SQL/komut metnine interpolation ile gömmeyin → parametre (→ J8, J11.4). ([`Section05`](Sections/Section05_Strings.cs))

### 3.6 Collection expressions ve spread

```csharp
int[] a = [1, 2, 3];   List<int> l = [1, 2, 3];   ImmutableArray<int> i = [1, 2, 3];   Span<int> s = [1, 2, 3];
int[] merged = [.. first, 3, .. second];           List<string> empty = [];
```
Tek sözdizimi; **hedef tipe göre** oluşur (hedef tip yoksa `var x = [1,2,3]` derlenmez, CS9176). Derleyici hedef tipe uygun en verimli kodu üretir. `..` (spread) birleştirmede `Concat`/`AddRange` yerine; koşullu eleman: `[.. baseRoles, .. isAdmin ? ["admin"] : []]`. Dictionary için kullanılmaz (index initializer).

### 3.7 Primary constructors ⭐

```csharp
public sealed class Greeter(ILogger logger, string prefix) { public string Greet(string n) { logger.Log(n); return $"{prefix}, {n}!"; } }
```
- **class/struct (C# 12):** parametre sınıf gövdesinde görünür ama **property/alan değildir** (yakalanan gizli alan). Dışarı açmak için `public string Prefix { get; } = prefix;`.
- **record:** parametre → **init-only property** + `Deconstruct` + eşitlik (otomatik).
- **Doğrulama:** property başlatıcısında `!string.IsNullOrWhiteSpace(id) ? id : throw …`.
- **Tuzak:** yakalanan parametre **mutable**'dır (`start++` state'i gizler); immutable tutun.
- DI'da en yaygın kullanım: `class OrderService(IOrderRepository repo, ILogger<OrderService> logger)` — boilerplate yok (→ J6.2).

### 3.8 Küçük şekerler

- **File-scoped namespace** (`namespace X;`), **global using** + `ImplicitUsings`, **target-typed `new()`** (`List<int> l = new();`; `var x = new();` derlenmez), **`using` declaration** (`using var r = …;` → kapsam sonunda, ters sırada Dispose), lambda'da varsayılan parametre (C# 12). ([`Section08`](Sections/Section08_SmallSyntax.cs))

### 3.9 C# 14 ile gelen ve günlük kodda görülenler

- **`field` keyword:** backing field'ı elle yazmadan setter doğrulaması:
  ```csharp
  public string Email { get; set => field = value?.Trim().ToLowerInvariant() ?? throw new ArgumentNullException(nameof(value)); } = "";
  ```
  Sınıfınızda zaten `field` adlı bir üye varsa `@field`/`this.field` ile ayırın (derleyici breaking-change uyarısı verir).
- **Null-conditional assignment:** `customer?.Name = "x";` — `customer` null ise atama **ve sağ taraf** hiç değerlendirilmez; `customer?.Count += 1` de çalışır (`++`/`--` değil).
- Extension members, partial constructors/events vb. → M1.1. C# 15 / .NET 11 özellikleri yalnızca **(.NET 11)** işaretiyle anılır.

### 3.10 İki stil yan yana ([`Section10`](Sections/Section10_SideBySide.cs), [`LineCounter`](Comparison/LineCounter.cs))

| Konu | C# 7 tarzı | C# 14 tarzı |
|------|-----------|-------------|
| `Address`, `OrderLine` | elle `Equals`/`GetHashCode`/`ToString`/`WithXxx` | tek satır `record` |
| `Customer.Email` | alan + property + doğrulama | `field` keyword |
| Durum makinesi | `switch` statement + `break` + geçici değişken | `switch` expression + `with` |
| `DiscountRate` | iç içe `if`/`switch` | tuple + relational pattern |
| Makbuz | `StringBuilder` + `string.Format` | interpolated raw string |
| Namespace/using | blok girinti + 6 using | file-scoped + implicit/global |
| Koleksiyon | `new ReadOnlyCollection<>(new List<>(…))` | `ImmutableArray` + `[.. xs]` |
| **Satır** | **224** | **56 (~%75 az)** |

**Okunabilirlik:** yeni stil *ne* olması gerektiğini (deklaratif), eski stil *nasıl* yapılacağını anlatır. Ama kısa ≠ her zaman iyi: ileri pattern ve primary constructor zincirleri deneyimsiz okuyucuya yabancı gelebilir; takım standardı ve okunabilirlik önce gelir. **Dürüst not:** C# 14 sürümünde `Order` için `Equals`'ı yine elle yazmak gerekti (record koleksiyonu referansla karşılaştırır, 3.1).

---

## 4. Gerçek dünya senaryosu

**"Eski servisi modernleştiriyoruz."** 600 satırlık DTO/model katmanı: her sınıfta elle `Equals`/`GetHashCode`/`ToString`. Önce davranışı testlerle sabitleyin (bu konudaki `IOrderFacade` yaklaşımı: iki uygulama aynı senaryodan aynı sonucu verir), sonra DTO'ları `record`/`required init`'e çevirin; EF entity'lerine dokunmayın. Satır sayısı ~%70 düşer, `Equals`/`GetHashCode` tutarsızlığı hata sınıfı ortadan kalkar.

**"Dev ortamda çalışan makbuz üretimde bozuk."** `$"{price:N2}"` geliştirici makinesinde `en-US`, müşteride `tr-TR` kültüründe farklı çıktı verdi; JSON ve makine çıktılarında `FormattableString.Invariant`/`InvariantCulture` kullanın (→ J2.2).

---

## 5. Mülakat perspektifi

Sık sorulanlar: **`record` ile `class` farkı** ⭐ · `with` nasıl çalışır, shallow mı? · `init` vs `set`, `required` ne sağlar? · Pattern matching çeşitleri · `switch` expression'ın statement'tan farkı · Raw string literal ne işe yarar? · Collection expression/spread · **Primary constructor** parametresi property midir? · `field` keyword.

**İpucu:** Modern özelliği anlatırken "eskiden ne yazardım → şimdi ne yazıyorum → hangi hata sınıfı ortadan kalktı" üçlüsünü kullanın.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| Positional `record struct`'ı mutable bırakmak | `readonly record struct` |
| `record` içinde `List<T>` ile değer eşitliği beklemek | Koleksiyon referansla karşılaştırılır; immutable koleksiyon/elle `Equals` |
| `with` ile derin kopya beklemek | Shallow; iç nesneyi de `with` ile yenileyin |
| EF entity'sini `record` yapmak | Entity = class; DTO = record |
| `switch` expression'da dal sırasını karıştırmak | Özelden genele; `_` en sonda |
| Kapsamlı olmayan `switch` expression uyarısını görmezden gelmek | Eksik dalı ekleyin (`SwitchExpressionException` riski) |
| Primary ctor parametresini property sanmak | Class'ta property değildir; açıkça tanımlayın |
| Yakalanan primary ctor parametresini mutate etmek | Alana kopyalayın / immutable tutun |
| Kullanıcı girdisini interpolation ile SQL'e gömmek | Parametreli sorgu |
| `var x = [1,2,3];` / `var x = new();` yazmak | Hedef tip belirtin |
| Her yere ileri pattern zinciri yazmak | Okunabilirlik ve takım standardı |
| `field` adlı mevcut üyeyi unutmak | `@field` / `this.field` |

---

## 7. Performans & trade-off

- **Record değer eşitliği** alan alan karşılaştırır; büyük record'larda ve sıcak `Dictionary` anahtarlarında maliyetlidir. `record struct` + `IEquatable<T>` boxing'i önler.
- **Collection expression** hedef tipe göre en iyi kodu üretir (Span için stack, boyutlu array); `AddRange` döngülerinden eşit veya hızlıdır.
- **Raw string / interpolation:** derleme zamanında çözümlenir; modern interpolated string handler'lar gereksiz tahsisleri azaltır (logger'larda `LoggerMessage` kaynak üreticisi → J6.5).
- **Pattern matching / switch expression** derleyici tarafından optimize edilir (karar ağacı / jump table); elle yazılmış `if` zincirinden yavaş değildir.
- **Primary constructor:** ek maliyet yok; yakalanan parametre gizli alan olur (bellek: bir alan).
- **Sürüm bağımlılığı:** yeni dil özellikleri `LangVersion`/SDK gerektirir; kütüphane çoklu hedefliyorsa (netstandard2.0) bazıları polyfill ister.

---

## 8. Kaynaklar

- C# dil sürümleri ve yenilikler: <https://learn.microsoft.com/dotnet/csharp/whats-new/>
- Records: <https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/record>
- Pattern matching: <https://learn.microsoft.com/dotnet/csharp/fundamentals/functional/pattern-matching>
- `switch` expression: <https://learn.microsoft.com/dotnet/csharp/language-reference/operators/switch-expression>
- Raw string literal'ler: <https://learn.microsoft.com/dotnet/csharp/language-reference/tokens/raw-string>
- Collection expressions: <https://learn.microsoft.com/dotnet/csharp/language-reference/operators/collection-expressions>
- Primary constructors: <https://learn.microsoft.com/dotnet/csharp/whats-new/tutorials/primary-constructors>
- C# 14'te yenilikler (`field`, null-conditional assignment): <https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-14>
- `required` ve `init`: <https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/required>
