# J2.4 Sınıf Üyeleri & Nesne Semantiği ⭐

> 📁 `ConsoleApp/01-junior/02-csharp-fundamentals/04-class-members/` · Önkoşul: J2.3 · Sorular: [`interview-questions.md`](interview-questions.md)

OOP ilkelerinin C#'taki somut karşılıkları: property, constructor, eşitlik, kopyalama. Mülakatın "ince detay" soruları buradan gelir.

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

Bir sınıfın **üyeleri** (property, constructor, operatör, indexer, extension…) ve nesnelerin **semantiği** (eşit mi? kopyası nasıl alınır? ne zaman kurulur/bırakılır?). Konu 9 bölümden oluşur (`dotnet run -- J2.4 <numara>`): başlatma sırası, `HashSet` bozulması, mutable key, shallow/deep copy, operator tutarlılığı…

---

## 2. Neden?

- "Neden public field değil property?" ve "Field initializer'lar ile constructor hangi sırayla çalışır?" soruları mülakatın klasik **ince detay** testidir.
- `Equals`/`GetHashCode` hatası `HashSet`/`Dictionary`'de **sessiz** veri bozulması üretir.
- Shallow copy hatası "kopyayı değiştirdim, orijinal bozuldu" üretim hatalarının kaynağıdır.
- Constructor'dan virtual çağrı, yalnızca **belirli koşullarda** patlayan hatalar üretir.

---

## 3. Nasıl?

### 3.1 Property vs field ⭐

[`Products.cs`](Models/Products.cs): `ProductBefore` (public field) → `ProductAfter` (property).

**Public field neden kullanılmaz?**
1. **Doğrulama yapılamaz:** `Price = -5` serbest → invariant korunmaz.
2. **Salt okunur / kısıtlı yazma yapılamaz** (`private set`, `init`).
3. **Sonradan property'ye çevirmek *binary breaking change*'dir:** kaynak kod aynı görünür (`p.Name`) ama IL `ldfld` → `callvirt get_Name` olur; eski derlenmiş tüketici `MissingFieldException` alır. Ayrıca field'a `ref`/`out` geçilebilir, property'ye geçilemez (CS0206). Kütüphane yazarken baştan property (auto-property ücretsizdir).
4. Data binding, serileştirme, EF gibi araçlar çoğunlukla property bekler.

| Biçim | Anlamı |
|-------|--------|
| `{ get; set; }` | Okunur ve yazılır (auto-property; derleyici `<Name>k__BackingField` üretir, → M2.1) |
| `{ get; }` | Salt okunur (yalnızca ctor/initializer'da atanır) |
| `{ get; init; }` | Yalnızca **nesne oluşturulurken** atanır, sonra salt okunur |
| `{ get; private set; }` | Dışarıdan okunur, **yalnızca sınıf** yazar (durum geçişini metotlar yönetir) |
| `=> expr` (computed) | Depolama yok; her okumada hesaplanır (**ucuz ve yan etkisiz** olmalı) |
| `required` (C# 11) | Nesne oluştururken (object initializer ile) atanması **zorunlu**; "yarım nesne" oluşmaz (CS9035) |
| **`field` keyword** (C# 14) | Backing field'ı elle yazmadan setter'da doğrulama: `set => field = value >= 0 ? value : throw …` (→ M1.1) |

- **Property vs metot:** property ucuz, yan etkisiz, tekrar okunduğunda aynı sonucu veren "durum" gibi davranmalı. DB/ağ çağrısı, uzun hesap, her seferinde farklı sonuç → **metot**.
- **Static property:** tip düzeyinde paylaşılan değer. **`readonly` alan vs `{ get; }`:** alan bellekte bir slot, property bir metot çifti (`get_X`); public API'de property.

### 3.2 Constructor çeşitleri ve başlatma sırası ⭐

| Çeşit | Not |
|-------|-----|
| default | Hiç ctor yazmazsanız derleyici parametresiz public üretir; **bir tane yazarsanız üretilmez** |
| parameterized | — |
| `static` | Tip başına bir kez; parametresiz, erişim belirleyicisiz |
| **`private`** | Singleton / **static factory method** deseni (`Connection.Create(...)`) |
| copy constructor | Aynı tipten parametre alan ctor (özel sözdizimi yok) |
| `this(...)` / `base(...)` | Zincirleme; primary constructor → J2.6 |

**Başlatma sırası** ⭐ — klasik "ince" soru ([`Section02`](Sections/Section02_InitializationOrder.cs) çıktısı):

```
new Derived() →
  1) All fields are set to their default value (0/null)
  2) Field initializers — from the MOST DERIVED class toward the BASE
  3) Constructor BODIES — from the BASE toward the derived
```
Neden initializer'lar "ters"? Derleyici onları constructor'ın **başına**, `base(...)` çağrısından **önce** yerleştirir. Gerçek çıktı: `Derived instance field initializer → Base instance field initializer → Base constructor BODY → Derived constructor BODY`.

**Static başlatma:** her tipin static field initializer + static ctor'u, o tip **ilk kullanılmadan önce bir kez** (thread-safe) çalışır; ikinci `new`'de tekrarlanmaz. Deneyde Base'in static başlatması, Derived'ın instance alanları kurulduktan **sonra**, base ctor'a ilk girişte tetiklenir. Static ctor exception fırlatırsa tip kalıcı olarak `TypeInitializationException` verir (→ J2.1; `beforefieldinit` derinliği → S1.2).

### 3.3 Constructor'dan `virtual` metot çağırmak ⭐ (CA2214)

Base ctor çalışırken türetilmiş sınıfın **ctor gövdesi henüz çalışmamıştır**; override edilen metot **yarım kurulmuş nesneyi** görür ([`VirtualCall.cs`](Models/VirtualCall.cs)):

```
Inside the base ctor:  initializer: from initializer, ctor: null, count: 0      ← whatever the ctor assigns is still at its default!
After the ctor finishes: initializer: from initializer, ctor: from ctor body, count: 42
```
Field initializer ile atananlar *dolu* (taban ctor'dan önce çalıştıkları için), ctor gövdesinde atananlar *boş* → tutarsız ve bulması zor hata. Analyzer **CA2214** bunu yakalar (varsayılan kapalıdır; bu proje [`.editorconfig`](.editorconfig) ile `warning` yapar, `dotnet build --no-incremental` çıktısında görünür). Çözüm: virtual çağrıyı ctor'da yapma; nesne **tamamen kurulduktan sonra** (factory + `Initialize()`; `SafeBase.Create<T>()`).

### 3.4 Eşitlik ve hash ⭐

- **Referans eşitliği vs değer eşitliği:** class varsayılanı referans (`==`, `Equals` = aynı nesne mi?); `ReferenceEquals` her zaman referans; `record` ve `record struct` değer eşitliğini otomatik üretir (→ J2.6).
- **`Equals` + `GetHashCode` birlikte override edilir.** Sözleşme: `a.Equals(b)` ⇒ `a.GetHashCode() == b.GetHashCode()` (tersi şart değil). Hash tabanlı koleksiyonlar önce hash'e bakar, sonra `Equals` çağırır:

```csharp
class BadPoint { override bool Equals(...) => ...; }   // NO GetHashCode
var set = new HashSet<BadPoint> { new(1,2) };
set.Contains(new BadPoint(1,2));   // false! (Equals says equal, but the hash looked in another bucket)
set.Add(new BadPoint(1,2));        // true  → the "same" object was added a second time
```
- **`IEquatable<T>`:** tipli `Equals(T)` — struct'ta **boxing'i önler**, koleksiyonlar onu tercih eder. **`IComparable<T>`** doğal sıralama, **`IComparer<T>`** alternatif sıralama.
- **Mutable key tuzağı** ⭐: `Dictionary`/`HashSet`'e eklendikten sonra hash'i değişen nesne **bulunamaz** (`Contains` false, `Remove` false) ama koleksiyonda **durur** → "hayalet kayıt". Kural: hash'e giren alanlar **değişmez** olmalı (immutable key: `record`, `string`, `int`) (→ J3.1).
- **`ToString()`**: log/hata ayıklama için anlamlı metin.

### 3.5 Extension method'lar ⭐

```csharp
public static class StringExtensions {
    public static string Shout(this string text) => text.ToUpperInvariant() + "!";   // static class + static method + this parameter
}
"hello".Shout();   // compiler: StringExtensions.Shout("hello")  (syntactic sugar for the static call)
```
- Gerçekte tipe **hiçbir şey eklenmez**; null receiver ile bile çalışır (`NullReferenceException` yok).
- **Çözümleme sırası:** (1) **örnek metot HER ZAMAN önce** gelir (aynı imzalı extension asla çağrılmaz — [`Widget`](Extensions/StringExtensions.cs)), (2) çağıran dosyanın `using`'leriyle görünen extension'lar. **`using` yoksa görünmez.**
- **LINQ'in tamamı extension method'dur** (`System.Linq.Enumerable` bir static class). C# 14 *extension members* → M1.1.
- Ne zaman? Sahibi olmadığınız tipe (framework) davranış eklemek, fluent API, interface'e davranış. Ne zaman değil? **Durum** eklemek, davranışı **override** etmek, polimorfizm → inheritance / wrapper / composition.

### 3.6 Operator overloading ve indexer

[`Money`](Models/Money.cs) (`readonly struct`, value object):
- **`==` ve `!=` birlikte** tanımlanır (CS0216); `<`/`>` ve `<=`/`>=` de çift. `==` tanımlıysa **`Equals`/`GetHashCode` aynı mantıkla** override edilmeli (CS0660/CS0661 uyarır) — `a == b` ⇔ `a.Equals(b)` ⇔ aynı hash olmalı.
- `implicit` dönüşüm: kayıp/exception yok (`decimal → Money`); `explicit`: kayıp olabilir (`Money → decimal`: para birimi kaybolur).
- Anlamlı olduğunda kullanın (Money, Vector, `DateTime - DateTime`); `Order + Order` gibi anlamsız overload okunabilirliği bozar.

[`MoneyBag`](Models/MoneyBag.cs) **indexer**: `this[int]` (get/set), `this[string]` (para birimi toplamı), `this[string, int]` (çok parametreli). `Count` + `int` indexer varsa derleyici `bag[^1]` (Index) ve aralık desteğini otomatik sağlar. Indexer derlemede `Item` property'sidir.

### 3.7 Shallow copy vs deep copy ⭐

| Yöntem | Davranış |
|--------|----------|
| `var b = a;` | **Kopya değil**, aynı nesne |
| `object.MemberwiseClone()` | **Shallow:** değer alanları kopyalanır, **iç referanslar paylaşılır** → kopyayı değiştirmek orijinali bozar |
| Copy constructor / elle | **Deep**, açık ve hızlı; yeni alan eklenince güncellemeyi unutma riski |
| JSON round-trip | Deep, yazması kolay; **yavaş**, tip kısıtlı (parametresiz ctor, dairesel referans) |
| `record` + `with` | **Shallow!** İç içe record/koleksiyonda referans paylaşılır → `rec with { Address = rec.Address with { … } }` |

**`ICloneable` neden önerilmez?** `Clone()` `object` döner (cast gerekir) ve shallow mı deep mi olduğunu **sözleşmede belirtmez**; Microsoft public API'de kullanılmamasını önerir. En iyi çözüm çoğu zaman **immutable tasarım** (kopyalama ihtiyacı azalır). ([`Section07`](Sections/Section07_Copying.cs))

### 3.8 Diğer üyeler

- **Optional parametre + named argument:** `Send(to, urgent: true)`. Varsayılan değer **çağıranın IL'ine derlenir** (`const` ile aynı mantık, → J2.2): kütüphanede varsayılanı değiştirip yalnızca kütüphaneyi dağıtırsanız eski tüketici eski değeri kullanır → public API'de overload tercih edin. Optional + overload karışırsa optional'sız overload kazanır (kafa karıştırıcı).
- **Anonim tipler:** derleyicinin ürettiği, adsız, salt okunur, değer eşitlikli sınıf; metot sınırını geçemez → LINQ ara sonuçları; kalıcı veri için `record`/tuple.
- **Nested sınıf:** dış sınıfın `private` üyelerine erişir; yalnızca dış sınıfın iç detayı olan yardımcı tipler için (enumerator, builder, state). Dışarıdan kullanılan/test edilen tipler ayrı dosyada.
- **Yaşam döngüsü ve `IDisposable`'a giriş:** GC yalnızca **belleği** yönetir; dosya/soket/DB bağlantısı gibi yönetilmeyen kaynakları bırakmak sizin işiniz → `using`. `using` (blok veya declaration) `Dispose()`'u exception olsa bile garanti eder; birden çok kaynak **ters sırada** bırakılır. Tam Dispose pattern → M1.5.

---

## 4. Gerçek dünya senaryosu

**"Sipariş kopyalanıp düzenlenince orijinal sipariş de değişiyor."** "Taslak oluştur" özelliği `MemberwiseClone` ile siparişi kopyalıyor; kullanıcı kopyada teslimat adresini değiştirince orijinalin adresi de değişiyor (iç `Address` paylaşılıyor). Çözüm: copy constructor ile deep copy; `ReferenceEquals(original.ShippingAddress, copy.ShippingAddress)` artık `false` olmalı. ([`Section07`](Sections/Section07_Copying.cs))

**"Kullanıcı sepete aynı ürünü iki kez ekleyebiliyor."** `CartItem` yalnızca `Equals` override etmiş; `HashSet<CartItem>` hash farklı olduğu için eşitliği görmüyor ([`BadPoint`](Models/EqualityModels.cs) davranışı). Çözüm: `GetHashCode` + `IEquatable<T>` (ya da `record`).

**"Yalnızca bazı alt sınıflarda NullReferenceException."** Taban sınıf ctor'u virtual `Initialize()` çağırıyor; bir alt sınıf bu metotta ctor gövdesinde atanan alanı kullanıyor → o an `null` ([`DerivedWithState`](Models/VirtualCall.cs)).

---

## 5. Mülakat perspektifi

Sık sorulanlar: Neden public field yerine property? ⭐ · Field initializer ve constructor hangi sırayla? ⭐ · Constructor'dan virtual çağırmak neden tehlikeli? ⭐ · `Equals` override edince neden `GetHashCode`? · Shallow vs deep copy ⭐ · Extension method nasıl yazılır? · `ICloneable` neden önerilmez? · `==` ile `Equals` farkı · Mutable key tuzağı.

**İpucu:** Başlatma sırasını **yazarak/çizerek** anlatın (1-2-3 adım) ve ardından "bu yüzden ctor'da virtual çağrı tehlikeli" ile bağlayın — iki sorunun cevabı tek zincir.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| Public field | Auto-property (en azından `{ get; set; }`), mümkünse `init`/`private set` |
| Property içinde ağ/DB çağrısı yapmak | Metot (`GetXxx()`/`LoadXxxAsync()`) |
| Yalnızca `Equals` override etmek | `Equals` + `GetHashCode` + `IEquatable<T>` (veya `record`) |
| Hash'i mutable alandan türetmek | Immutable alanlar; anahtar nesnesini değiştirmeyin |
| `==` tanımlayıp `Equals`/`GetHashCode`'u unutmak | Tutarlı üçlü; ya da hiç `==` tanımlamayın |
| Constructor'da virtual metot çağırmak | Factory + `Initialize()` ya da `sealed` / composition |
| `MemberwiseClone`'u "kopya" saymak | Shallow! İç referansları da kopyalayın |
| `record with` ile derin kopya beklemek | İç içe record/koleksiyonu da yenileyin veya immutable koleksiyon |
| `ICloneable` kullanmak | Copy constructor / `Clone` yerine açık adlı metot |
| Public API'de optional parametre varsayılanını sonradan değiştirmek | Overload kullanın; veya `null` varsayılan + içeride çözümleyin |
| Extension ile aynı imzalı instance metot beklemek | Instance her zaman kazanır |
| Extension'ı `System.*` namespace'ine koymak | Kendi namespace'iniz; `using` ile açık keşif |
| `IDisposable` nesneyi `using` olmadan bırakmak | `using`/`await using` |

---

## 7. Performans & trade-off

- **`Equals` varsayılanı:** struct için `ValueType.Equals` reflection tabanlı **yavaştır** (ve boxing yapar) → kendi `IEquatable<T>`'nizi yazın; `record struct` bunu üretir.
- **`GetHashCode` kalitesi:** kötü hash çakışma → `Dictionary` O(1) yerine O(n). `HashCode.Combine(...)` iyi varsayılandır.
- **Property erişimi** auto-property'de JIT tarafından inline edilir → field'dan fark yok; "performans için public field" gerekçesi geçersizdir.
- **Deep copy yolları:** copy ctor (hızlı) ≪ JSON round-trip (yavaş, alloc yüklü). Immutable tasarım kopyayı tamamen ortadan kaldırabilir.
- **Optional parametre** çağırana derlendiği için çalışma zamanı maliyeti yoktur; ama versiyonlama maliyeti vardır.
- **Extension method** statik çağrıdır: ek maliyet yok; fakat keşfedilebilirlik/namespace kirliliği maliyeti var.
- **Nesne yaşam süresi:** `using` ucuzdur; unutulan `Dispose` kaynak sızıntısı (handle/soket tükenmesi) üretir.

---

## 8. Kaynaklar

- Properties: <https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/properties>
- `required` members ve `init`: <https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/required> · <https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/init>
- Constructors: <https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/constructors>
- CA2214 — constructor'larda overridable metot çağırmayın: <https://learn.microsoft.com/dotnet/fundamentals/code-analysis/quality-rules/ca2214>
- `Equals` ve `GetHashCode` override rehberi: <https://learn.microsoft.com/dotnet/api/system.object.gethashcode#notes-to-inheritors>
- `IEquatable<T>`: <https://learn.microsoft.com/dotnet/api/system.iequatable-1>
- Extension methods: <https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods>
- Operator overloading ve tasarım yönergeleri: <https://learn.microsoft.com/dotnet/standard/design-guidelines/operator-overloads>
- Indexers: <https://learn.microsoft.com/dotnet/csharp/programming-guide/indexers/>
- ICloneable önerisi: <https://learn.microsoft.com/dotnet/api/system.icloneable#remarks>
- `using` ve `IDisposable`: <https://learn.microsoft.com/dotnet/standard/garbage-collection/using-objects>
