# J2.3 OOP Temelleri ⭐

> 📁 `01-junior/02-csharp-fundamentals/03-oop/` · Önkoşul: J2.2 · Pratik: [`src/`](src/) · Test: [`tests/`](tests/) · Sorular: [`interview-questions.md`](interview-questions.md)

Mülakatların en çok soru üretilen başlığı. "Dört ilkeyi say" değil, **kendi kodundan örnek verebilmek** beklenir.

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

Nesne yönelimli programlama (OOP), yazılımı **veriyi ve davranışı birlikte taşıyan nesneler** üzerinden modellemektir. Amaç; karmaşıklığı yönetmek, değişikliği yerelleştirmek ve kodu test edilebilir kılmaktır. C# bu modeli sınıflar, arayüzler, kalıtım ve polimorfizmle destekler.

Bu konuda kod şu parçalardan oluşur:

| Parça | Ne gösterir? |
|-------|--------------|
| [`src/Oop`](src/Oop/) — 9 bölüm | Dört ilke, polimorfizm türleri, abstract vs interface, `override` vs `new`, casting, ilişkiler, coupling/cohesion, struct ve static |
| [`PaymentRefactoring/`](src/Oop/PaymentRefactoring/) | Ödeme yöntemleri: **kalıtım → strateji → tam composition**, her adımda **aynı testler yeşil** |
| [`Relationships/`](src/Oop/Relationships/) | Aggregation (Team/Employee) ve composition (Order/OrderLine) + **GC ile yaşam süresi kanıtı** |
| [`tests/Oop.Tests`](tests/Oop.Tests/) | 64 test |

---

## 2. Neden?

- Her kod tabanı **değişir**; iyi OOP, değişikliğin yayılma alanını küçültür.
- Mülakatta tanım değil **gerekçe** aranır: "neden composition?", "neden interface?", "neden `new` tehlikeli?"
- DI, test double, domain-driven design, design pattern'ler — hepsi bu temellerin üstüne kurulur (→ J6.2, M11, S5).

---

## 3. Nasıl?

### 3.1 Dört ilke — tanım değil, örnekle ⭐

| İlke | Tek cümle | Kodda ([`Section01`](src/Oop/Sections/Section01_FourPrinciples.cs)) |
|------|-----------|------|
| **Encapsulation** (kapsülleme) | Veriyi ve onu değiştiren davranışı birlikte tut; **geçersiz duruma düşmeyi engelle** | `BankAccount`: `Balance { get; private set; }` + `Withdraw` yetersiz bakiyeyi reddeder → bakiye negatif **olamaz** (invariant). Uygulaması: property → J2.4 |
| **Abstraction** (soyutlama) | "**Ne** yaptığını" açığa çıkar, "**nasıl** yaptığını" sakla | `IReportExporter`: çağıran CSV mi PDF mi bilmez |
| **Inheritance** (kalıtım) | "**is-a**" ilişkisi, ortak davranışı devralma | `SavingsAccount` **is-a** `Account` |
| **Polymorphism** (çok biçimlilik) | Aynı arayüz, farklı davranış | `Shape.Area()`: çağıran somut tipi bilmeden doğru hesap çalışır |

**Kalıtım ne zaman doğru araç, ne zaman tuzak?** Doğru: gerçek bir *is-a* varsa ve taban sınıfın **tüm sözleşmesi** türeyen için de geçerliyse (Liskov Substitution), amaç kavramsal alt-tipleme ise (`Exception` hiyerarşisi, `Stream`). Tuzak: yalnızca **kod tekrar kullanmak** için kalıtım; `Stack : List` gibi *is-a* olmayan türetme; derin hiyerarşiler (→ 3.9).

### 3.2 Polimorfizmin iki türü ⭐

| | Compile-time (static, early binding) | Runtime (dynamic, late binding) |
|---|---------------------------------------|--------------------------------|
| Kim karar verir? | **Derleyici** (argümanların/değişkenin **derleme zamanı tipi**) | **Çalışma zamanı** (nesnenin **gerçek tipi**) |
| Araçlar | **Overloading**, generic'ler, operator overloading | **Overriding**: `virtual`/`abstract` + `override` |
| Mekanizma | Çağrı hedefi IL'e yazılır | **vtable** (sanal metot tablosu) üzerinden dolaylı çağrı |
| Maliyet | Sıfır (inline edilebilir) | 1 ek dolaylama; `sealed`/non-virtual olanlar devirtualize/inline edilebilir |

Tuzak: `object o = "x"; Print(o)` → overload **`object`** sürümüne gider (çalışma zamanı tipi string olsa da); seçim derleme zamanı tipine göredir. `dynamic` seçimi çalışma zamanına erteler ([`Section02`](src/Oop/Sections/Section02_Polymorphism.cs)).

### 3.3 `abstract class` vs `interface` ⭐

| | `abstract class` | `interface` |
|---|------------------|-------------|
| İlişki | "is-a" + **ortak kod/durum** | "can-do" / rol / **sözleşme** |
| Çoklu uygulama | ✗ (tek kalıtım) | ✓ |
| Alan (state) | ✓ | ✗ (instance field yok) |
| Constructor | ✓ | ✗ |
| Erişim düzeyleri | `protected`, `private`, `internal`… | Üyeler varsayılan public |
| Gövdeli üye | ✓ | ✓ **default implementation** (C# 8+) |
| Test (mock) | Zor | Kolay |

**C# 8 default implementation sonrası fark ne kaldı?** İnterface hâlâ **durum tutamaz**, **constructor'ı yoktur**; bir sınıf **tek** abstract class'tan türer ama **istediği kadar** interface uygular; default implementation'a yalnızca **interface referansı** üzerinden erişilir. Default implementation'ın asıl amacı, **yayınlanmış bir interface'e üye eklerken uygulayanları kırmamaktır**, çoklu kalıtımın durumsuz biçimi değildir.

- **`abstract` üye:** gövdesi yok, türeyen **uygulamak zorunda**. Abstract sınıf **örneklenemez ama constructor'ı olabilir** (türeyen sınıf çağırır): `Report(string title)`.
- **Explicit interface implementation:** iki interface'in **aynı imzalı** üyesi çakışırsa her biri ayrı uygulanır (`string IBird.Move()`, `string IRobot.Move()`); yalnızca ilgili interface referansı üzerinden erişilir ([`Section03`](src/Oop/Sections/Section03_AbstractVsInterface.cs)).
- **Pratik kural:** önce interface (bağımlılığı soyutlar, test edilebilir); ortak **kod/durum** paylaşımı gerekiyorsa abstract class.

### 3.4 `virtual`, `override`, `new`, `sealed` ⭐

```csharp
class Base            { public virtual string Hello() => "Base"; }
class ViaOverride : Base { public override string Hello() => "Override"; }
class ViaNew      : Base { public new      string Hello() => "New"; }

Base a = new ViaOverride();  a.Hello();   // "Override"  ← nesnenin GERÇEK tipi (polimorfizm)
Base b = new ViaNew();       b.Hello();   // "Base"      ← DEĞİŞKENİN tipi (method hiding; polimorfizm YOK)
((ViaNew)b).Hello();                      // "New"
```

- **`override`**: sanal metodu **değiştirir**, çağrı runtime tipe göre çözülür.
- **`new`**: taban üyeyi **gizler**; çağrı **değişkenin derleme zamanı tipine** göre çözülür. Aynı nesne hangi tipte tutulduğuna göre farklı davranır → şaşırtıcı hata kaynağı. Yazmazsanız derleyici CS0108 uyarısı verir. Neredeyse hiç istenen şey değildir.
- **`sealed override`**: zinciri burada keser (daha fazla override yok; JIT devirtualize edebilir). `sealed class` kalıtımı tamamen kapatır.
- ([`Section04`](src/Oop/Sections/Section04_NewVsOverride.cs) çıktısı + `BehaviorTests`.)

### 3.5 Upcasting / downcasting

- **Upcast** (türeyen → taban): her zaman güvenli, örtük; nesne değişmez, yalnızca *görünüm* daralır.
- **Downcast** (taban → türeyen): çalışma zamanında doğrulanır → `(Dog)x` başarısızsa `InvalidCastException`, `x as Dog` → `null`, **`x is Dog d`** → kontrol+cast+değişken tek adımda. `switch` + pattern ile tip bazlı dallanma.
- Çok downcast = tasarım kokusu: davranışı **virtual metoda** taşıyın, çağıranın tip kontrolü yapmasını gerektirmeyin (Open/Closed). ([`Section05`](src/Oop/Sections/Section05_Casting.cs))

### 3.6 Nesne ilişkileri — ownership ve yaşam süresi ⭐

| İlişki | Anlam | Yaşam süresi | Örnek |
|--------|-------|--------------|-------|
| **Inheritance** | "is-a" | — | `SavingsAccount : Account` |
| **Association** | "uses-a", sahiplik yok | Bağımsız | `Order` ↔ `Courier` |
| **Aggregation** | zayıf "has-a" | Parça bütünden **uzun yaşayabilir** | `Team` ↔ `Employee` |
| **Composition** | güçlü "has-a" | Parça bütünle **birlikte ölür** | `Order` ↔ `OrderLine` |

Kodda nasıl ayırt edilir ([`Relationships/`](src/Oop/Relationships/)):
- **Aggregation:** parça **dışarıda oluşturulur** ve bütüne verilir (`team.Add(employee)`); bütün dağılınca parça sağ kalır, başka bütüne taşınabilir.
- **Composition:** parçayı **bütün yaratır** (`order.AddLine(...)`, `OrderLine` constructor'ı `internal`); parça tek bir bütüne aittir, bütün iptal edilince parça anlamsızlaşır.

Testler bunu **kanıtlar** ([`RelationshipTests`](tests/Oop.Tests/RelationshipTests.cs)): `WeakReference` + `GC.Collect()` ile — takım toplanınca çalışanlar yaşar; sipariş toplanınca satırlar da toplanır. DDD'de **aggregate** sınırı tam olarak bu ayrımdan beslenir (bütünün tutarlılık sınırı ve yaşam döngüsü, → S5.1).

### 3.7 Coupling & cohesion ⭐

- **Coupling (bağlılık):** bir sınıfın başkasını **ne kadar bildiği**. Hedef: **gevşek**. Ölçmek: *"Bu sınıfı değiştirince kaç yer etkileniyor?"*, *"Bunu test etmek için kaç somut şey ayağa kalkmalı?"*
- **Cohesion (uyum):** bir sınıfın üyelerinin **aynı amaca** ne kadar hizmet ettiği. Hedef: **yüksek**. Ölçmek: *"Bu sınıfın kaç farklı değişme nedeni var?"* (SRP).
- **`new` ile somut tipe bağlanmanın maliyeti:** `new SmtpSender()` sınıfın içinde → test ederken gerçek e-posta gider, kanalı değiştirmek sınıfı değiştirmeyi gerektirir, bağımlılık imzada görünmez. **Interface + constructor injection** bağı gevşetir ([`Section07`](src/Oop/Sections/Section07_CouplingCohesion.cs); → J6.2).
- Düşük uyum örneği: `OrderManager` e-posta + PDF + vergi + veritabanını birlikte yapar → vergi kuralı da şablon da aynı dosyayı değiştirir.

### 3.8 SOLID'e giriş (tamamı → M11.1)

| | Prensip | Bir cümle |
|---|---------|-----------|
| **S** | Single Responsibility | Bir sınıfın değişmek için **tek** nedeni olsun |
| **O** | Open/Closed | Genişlemeye açık, değişikliğe kapalı (yeni davranış = yeni sınıf, mevcut kod değişmez) |
| **L** | Liskov Substitution | Türeyen, tabanın yerine **sözleşmeyi bozmadan** geçebilmeli |
| **I** | Interface Segregation | Büyük arayüz yerine istemciye özel küçük arayüzler |
| **D** | Dependency Inversion | Üst seviye modül somuta değil **soyutlamaya** bağımlı olsun |

Özellikle **SRP** (Adım 3'te her kural/politika ayrı sınıf) ve **DIP** (`OrderService(INotifier)`) bu konunun kodunda görünür.

### 3.9 Composition over inheritance ⭐ — [`PaymentRefactoring/`](src/Oop/PaymentRefactoring/)

Ödeme yöntemleri (kredi kartı, havale, cüzdan) üç adımda refactor edilir. **Her adımda aynı 15 davranış testi** koşar ([`PaymentRefactoringTests`](tests/Oop.Tests/PaymentRefactoringTests.cs): bir *contract* taban test sınıfı, üç adım için üç ince alt sınıf):

| Adım | Yapı | Sorun / kazanım |
|------|------|-----------------|
| **1 — Kalıtım** | `PaymentMethod` → `CreditCard`, `BankTransfer`, `Wallet`; yeni gereksinimler: `InstallmentCreditCard`, `FraudCheckedCreditCard`, **`FraudCheckedInstallmentCreditCard`** | Her yeni "boyut" sınıf sayısını **çarpar** (class explosion); C#'ta çoklu kalıtım yok → fraud mantığı **kopyalanır**; taban sınıf değişince alt sınıflar sessizce etkilenir (**fragile base class**) |
| **2 — Strateji** | Komisyon `IFeePolicy` nesnesine çıkarıldı; `InstallmentCreditCard` sınıfı **silindi** | Küçük, güvenli adım; fraud hâlâ alt sınıfta |
| **3 — Tam composition** | Tek `Payment` = `IFeePolicy` + `IPaymentRule[]` + `IPaymentSource`; "yöntem" = `PaymentFactory` konfigürasyonu | Kalıtım **yok**; fraud+taksit = parçaları birleştirmek; yeni gereksinim = yeni küçük sınıf (**Open/Closed**); her parça tek başına test edilir |

**Neden?** (1) **Fragile base class** — protected davranış sözleşmesi taban ile türeyen arasında gizli bağ kurar; (2) **derin hiyerarşi** bakım maliyeti; (3) **tek kalıtım** kısıtı; (4) kalıtım **derleme zamanında sabit**, composition **çalışma zamanında** değiştirilebilir; (5) parçalar ayrı test edilir. **Kalıtım yine de doğru** olan yer: gerçek *is-a* + Liskov (`Exception`, `Stream`, UI kontrolleri).

**Nasıl refactor edilir?** (1) Önce davranışı **testle sabitle**. (2) Değişen boyutu seç (komisyon), arkasına interface koy, alt sınıf yerine **nesne ver**. (3) Kalan alt sınıflardaki farkları da küçük nesnelere böl. (4) Her adımda testleri çalıştır; kırmızı olursa adımı geri al. (5) Gereksiz hiyerarşiyi sil.

### 3.10 `struct` ve `static` — OOP kısıtları

- **struct:** sınıftan/struct'tan **kalıtım alamaz** (örtük olarak `ValueType`'tan türer, fiilen `sealed`); **interface uygulayabilir** (ama interface'e atama boxing'dir, → J2.2). Polimorfizm yalnızca interface üzerinden.
- **`static` sınıf/üye:** durumsuz, saf yardımcılar (`Math`, extension method, factory) için uygun. "**Her şeyi static yapma**" eleştirisi: static üye imzada görünmeyen **gizli bağımlılıktır**, mock'lanamaz, paylaşılan durum paralel testleri bozar, polimorfizme katılamaz. Zaman/rastgelelik/dosya sistemi → **soyutla ve enjekte et** (`TimeProvider`, `IFileSystem`). ([`Section08`](src/Oop/Sections/Section08_StructAndStatic.cs): `DateTime.Now` vs `TimeProvider`; testte saat sabitlenir.)

---

## 4. Gerçek dünya senaryosu

**"Yeni ödeme yöntemi eklemek her seferinde 4 yeri değiştirmeyi gerektiriyor."** Ekip ödeme modülünü `PaymentMethod` hiyerarşisiyle yazmış. Taksit ve fraud kontrolü geldikçe `FraudCheckedInstallmentCreditCardPayment` gibi sınıflar türüyor, fraud kuralı iki yerde kopya; bir taban sınıf düzeltmesi (örn. `Validate` sırası) alt sınıflardan birini sessizce bozuyor. Çözüm: mevcut davranışı **testle sabitleyip** (15 test senaryosu) kademeli olarak komisyon → kural → kaynak parçalarını nesnelere çıkarmak; sonunda "3D Secure" gereksinimi yeni bir `IPaymentRule` sınıfından ibaret.

**"Sipariş iptalinde satırlar veritabanında kalmış."** `OrderLine` serbestçe oluşturulup başka siparişlere taşınabiliyordu (association gibi modellenmiş). Composition olarak modellemek (satırı sipariş yaratır, tek sipariş sahiplenir, iptal satırları yetim bırakır) yaşam döngüsünü tek yerde toplar — aggregate kökü kavramının ilk adımıdır.

---

## 5. Mülakat perspektifi

Sık sorulanlar: Dört ilkeyi **kendi kodundan** anlat ⭐ · `abstract class` vs `interface` (C# 8 sonrası fark) ⭐ · Compile-time vs runtime polimorfizm ⭐ · `override` vs `new` ⭐ · Aggregation vs composition · Composition over inheritance ⭐ · Coupling/cohesion · SOLID'de SRP ve DIP'i örnekle.

**İpucu:** Tanım yerine **gerçek bir karar hikâyesi** anlatın: "Ödeme yöntemlerini önce kalıtımla modelledik, taksit+fraud gelince sınıf patlaması yaşadık; strateji + kural nesnelerine böldük, testlerimiz her adımda yeşil kaldı." Bu cevap tek başına dört ilke tanımından daha fazla şey söyler.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| Kod tekrar kullanmak için kalıtım | Composition; kalıtım yalnızca gerçek *is-a* + Liskov |
| `Stack : List<T>` gibi *is-a* olmayan türetme | Composition (alan olarak tut, gereken üyeleri aç) |
| `new` ile metot gizlemek | `virtual` + `override`; gizleme neredeyse hiç doğru değil |
| Constructor'da `new Concrete()` ile bağımlılık yaratmak | Interface + constructor injection |
| Her sınıfa interface yazmak ("sadece mock için") | Gerçek bir değişim/test sınırı varsa; aksi halde gereksiz soyutlama |
| God class (her şeyi yapan `Manager`/`Helper`) | SRP; düşük uyumun işareti |
| Çok downcast / `is` zincirleri | Davranışı virtual metoda taşı veya pattern/visitor |
| Public alan / setter ile invariant korumamak | Encapsulation: private setter, doğrulayan metot |
| Her şeyi `static` yapmak | Durumsuz saf fonksiyonlar dışında enjekte et |
| Protected alanları açık bırakmak | Protected = alt sınıflara verilen API; küçük tutun, belgeleyin |
| `abstract class` ile durumsuz sözleşme tanımlamak | Interface |
| Derin (4+ seviye) kalıtım hiyerarşisi | Düzleştir; strateji/dekoratör ile böl |

---

## 7. Performans & trade-off

- **Sanal çağrı** = 1 dolaylama; sıcak döngülerde `sealed`/non-virtual daha ucuzdur ve inline edilebilir. Çoğu iş yükünde fark ölçülemezdir; tasarım netliği önce gelir (→ S1.1).
- **Composition** daha çok nesne ve yönlendirme (delegation) demektir: biraz daha fazla ayrıntı/tahsis; karşılığında esneklik ve test edilebilirlik. Çok küçük parçalara bölmek de okunabilirliği düşürür (parçala ama anlamlı kal).
- **Interface üzerinden çağrı** sanal çağrı maliyetindedir; struct'ı interface'e atamak **boxing** getirir (generic kısıt ile önlenir, → J2.2).
- **Soyutlama bedeli:** her soyutlama bir kavram yükü ekler. "Değişim noktası" belli olmadan soyutlamak (**erken soyutlama**) kodu karmaşıklaştırır; üçüncü tekrarda soyutlayın (rule of three).
- **Kalıtım vs composition bellek:** composition ek nesne referansları taşır; kalıtım alanları aynı nesnede tutar. Çoğu zaman önemsizdir.

---

## 8. Kaynaklar

- C# — nesne yönelimli programlama: <https://learn.microsoft.com/dotnet/csharp/fundamentals/tutorials/oop>
- Polimorfizm: <https://learn.microsoft.com/dotnet/csharp/fundamentals/object-oriented/polymorphism>
- `abstract`, `virtual`, `override`, `new`: <https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/versioning-with-the-override-and-new-keywords>
- Interface'ler ve default implementation: <https://learn.microsoft.com/dotnet/csharp/fundamentals/types/interfaces>
- Explicit interface implementation: <https://learn.microsoft.com/dotnet/csharp/programming-guide/interfaces/explicit-interface-implementation>
- `TimeProvider`: <https://learn.microsoft.com/dotnet/api/system.timeprovider>
- Tasarım yönergeleri — kalıtım ve sınıf tasarımı: <https://learn.microsoft.com/dotnet/standard/design-guidelines/>
- *Design Patterns* (GoF) — "Favor object composition over class inheritance"
- Robert C. Martin — SOLID prensipleri
- Barbara Liskov — Liskov Substitution Principle
