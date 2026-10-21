# J3.2 Delegates, Lambdas & Events

> 📁 `01-junior/03-collections-linq/02-delegates-lambdas-events/` · Önkoşul: J3.1 · Pratik: [`src/`](src/) · Test: [`tests/`](tests/) · Sorular: [`interview-questions.md`](interview-questions.md)

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

**Delegate**, bir metoda (veya metot zincirine) tip güvenli referans tutan nesnedir; davranışı **değer gibi taşımayı** sağlar. **Lambda**, delegate üreten kısa anonim fonksiyon sözdizimidir. **Event**, delegate üzerine kurulu yayıncı–abone (publish/subscribe) mekanizmasıdır. LINQ (J3.3), `Task.Run`, ASP.NET Core middleware ve DI fabrikaları gibi her yerde karşınıza çıkar. [`src/DelegatesLambdasEvents`](src/DelegatesLambdasEvents/) 7 bölüm, [`tests/`](tests/) 31 test içerir.

---

## 2. Neden?

- "Closure nedir, döngüde neden sorun çıkarır?" mülakatın en sık sorularından biridir ve gerçek bir hata sınıfıdır.
- Çıplak delegate yerine `event` kullanmak, yayıncının kendi durumunu koruması demektir.
- Unutulan abonelik, yönetilen bellek ortamında bile **memory leak**'in en yaygın nedenlerinden biridir.

---

## 3. Nasıl?

### 3.1 Delegate, `Func`/`Action`/`Predicate`, method group, multicast ([`Section01`](src/DelegatesLambdasEvents/Sections/Section01_Delegates.cs))

```csharp
delegate int BinaryOperation(int a, int b);     // delegate TİPİ = imza
BinaryOperation op = Add;                        // method group → delegate dönüşümü
Func<int,int,int> add = Add;                     // girdiler + DÖNÜŞ (son tip parametresi)
Action<string> print = Console.WriteLine;        // dönüş yok
Predicate<int> isEven = n => n % 2 == 0;         // ≡ Func<int,bool>
```
- Kendi delegate tipini nadiren yazarsınız; `Func<>` (dönüşlü) ve `Action<>` (void) yeterlidir.
- **Multicast:** `+=` / `-=` ile birden çok metot zincirlenir; çağrıda **sırayla** çalışır; dönüş değerli zincirde **yalnızca son metodun** dönüşü alınır. `GetInvocationList()` zinciri verir.
- **Delegate'ler immutable'dır:** `+=` yeni bir delegate nesnesi üretir.
- **Null:** abone yoksa delegate `null`'dır → `?.Invoke(...)`.
- Delegate vs interface: tek metotluk davranış için delegate hafiftir; ilişkili birden çok metot/durum için interface.

### 3.2 Lambda ([`Section02`](src/DelegatesLambdasEvents/Sections/Section02_Lambdas.cs))

| Biçim | Örnek |
|-------|-------|
| **Expression lambda** | `x => x * x` (tek ifade, örtük dönüş) |
| **Statement lambda** | `x => { if (x < 0) return "-"; return "+"; }` (birden çok ifade, `return`) |
| Çok parametre / tipli / discard | `(a, b) => a + b`, `(int a, int b) => …`, `(_, b) => b` |
| Natural type (C# 10) | `var f = (int x) => x + 1;` (derleyici `Func<int,int>` çıkarır) |
| Varsayılan parametre (C# 12) | `(int x, int y = 10) => x + y` |
| `static` lambda | `static x => x * 3` — dış değişkeni **yakalayamaz** (kazara closure yok) |

Lambda hedef tipe göre bir **delegate**'e ya da **`Expression<TDelegate>`**'e dönüşür. `Expression<Func<int,bool>>` kodun **ağaç (veri) temsilidir** (`x > 5` → `GreaterThan`); `IQueryable`/EF Core sorguyu SQL'e çevirmek için bunu kullanır (→ J3.3, J9.4, M1.3).

### 3.3 Closure ve captured variable ⭐ ([`Section03`](src/DelegatesLambdasEvents/Sections/Section03_Closures.cs))

**Closure** = lambda + yakaladığı dış değişkenler. Lambda **değeri değil değişkenin kendisini** yakalar; değeri **çalıştığı anda** okur:

```csharp
var message = "ilk"; Func<string> read = () => message; message = "sonra";
read();   // "sonra"
```
**Klasik tuzak (for döngüsü):**

```csharp
var funcs = new List<Func<int>>();
for (var i = 0; i < 3; i++) funcs.Add(() => i);     // 3 lambda AYNI 'i'yi yakalar
funcs.Select(f => f());                             // 3, 3, 3   (beklenen 0,1,2)
```
- **Çözüm:** döngü-yerel kopya: `var copy = i; funcs.Add(() => copy);` → `0,1,2`.
- **`foreach`'te tuzak yoktur:** C# 5'ten beri `foreach` değişkeni **her iterasyonda yenidir**; `for` değişkeni ise **tektir**.
- **Derleyici ne üretir?** Yakalanan değişkenler heap'teki bir **display class** nesnesine taşınır (`f.Target` → `<>c__DisplayClass…`). Yakalamayan `static` lambda önbelleğe alınmış tek örnektir (ek tahsis yok).
- **Bedel:** her closure bir nesne tahsis eder (deneyde 1000 iterasyon: yakalayan ≈ 64 KB, yakalamayan ≈ 64 B) ve yakalanan değişkenin ömrünü lambda'nınkine uzatır (büyük nesne yakalamak onu bellekte tutar). Çok thread'li kodda paylaşılan yakalanan değişken yarış koşulu üretir.

### 3.4 `event` ⭐ ([`Section04`](src/DelegatesLambdasEvents/Sections/Section04_Events.cs), [`Events.cs`](src/DelegatesLambdasEvents/Models/Events.cs))

**Neden çıplak delegate değil?** `public Action<Order>? OnCreated;` alanında dışarıdaki herkes: `= null` ile **tüm aboneleri siler**, `= tek` ile **ezer**, `?.Invoke(...)` ile **sahte olay üretir**. `event` anahtar sözcüğü delegate'i **kapsüller**: dışarıya yalnızca `+=` / `-=` açılır (CS0070: dışarıdan atama ve tetikleme yok); tetikleme yalnızca yayıncıdadır.

```csharp
public event EventHandler<OrderCreatedEventArgs>? OrderCreated;
protected virtual void OnOrderCreated(OrderCreatedEventArgs e) => OrderCreated?.Invoke(this, e);
```
- **`EventHandler<TEventArgs>`** = `(object? sender, TEventArgs e)`; `EventArgs`'tan türeyen sınıf olayın **verisini** taşır, `sender` yayıncıdır.
- **Observer deseni:** yayıncı abonelerin kim olduğunu bilmez → gevşek bağlılık (→ J2.3).
- **Dikkat:** (1) bir abone exception fırlatırsa **kalan aboneler çalışmaz** (dayanıklılık için `GetInvocationList()` ile tek tek çağırıp yakalayın); (2) aboneler yayıncının thread'inde **senkron** çalışır (yavaş abone yayıncıyı bloklar); (3) `?.Invoke` null kontrolü ve çağrıyı güvenli yapar. Süreçler arası olay = mesajlaşma (→ M12).

### 3.5 Abonelik sızıntısı ⭐ ([`Section05`](src/DelegatesLambdasEvents/Sections/Section05_SubscriptionLeak.cs))

Yayıncı, aboneye **referans tutar** (event = delegate zinciri; her elemanın `Target`'ı abone nesnesi). **Uzun ömürlü yayıncı** (singleton servis, `static event`) + **kısa ömürlü abone** (sayfa, view-model, istek kapsamlı nesne) birleşimi, abone `-=` yapmazsa onu GC'den korur: deneyde `WeakReference` ile kanıtlandı — abonelikten çıkmayan abone GC sonrası **hâlâ yaşıyor**, `Dispose` ile ayrılan toplandı; üstelik sızan abone her `Tick`'te çalışmaya devam eder ("hayalet" davranış).

**Çözümler:** işi bitince `-=` (genelde `IDisposable.Dispose`'ta, `using` ile garanti); zayıf olay desenleri (`WeakReference`/`WeakEventManager`); `static event`'ten kaçınma; lambda ile abone olduysanız `-=` için **aynı delegate nesnesini** saklayın (yeni bir lambda farklı nesnedir). Teşhis: bellek profilleyici (dotMemory, `dotnet-gcdump`) → "yayıncı → abone" retention path (derin hali → M1.6, S2.5).

### 3.6 Local function vs lambda ([`Section06`](src/DelegatesLambdasEvents/Sections/Section06_LocalFunctions.cs))

| | Local function | Lambda |
|---|----------------|--------|
| Ad / özyineleme | Adlı, kendini doğrudan çağırır | Özyineleme için önce delegate değişkeni gerekir |
| Tanımdan önce kullanım | ✓ | ✗ (değişkene atanmadan) |
| Tahsis | Delegate'e dönüştürülmedikçe **yok** (deneyde 0 B) | Her zaman delegate nesnesi (+ closure) |
| Generic / iterator / async | ✓ | Sınırlı |
| Değer olarak taşıma | Ancak delegate'e dönüştürülürse | Doğal |

`static` local function dış değişkeni yakalayamaz. Klasik kullanım: **iterator + erken argüman doğrulaması** — iterator metodunun gövdesi ertelendiği için doğrulama ilk `MoveNext`'te çalışır; doğrulamayı dış metoda, iterator'ı local function'a koyarsanız hata **anında** fırlar. Kural: **delegate/Expression değeri gerektiğinde** (parametre, LINQ, olay) lambda; **yalnızca tek metotta kullanılan yardımcı mantık/özyineleme** için local function.

### 3.7 Lambda ile strateji: indirim motoru ([`DiscountEngine.cs`](src/DelegatesLambdasEvents/Models/DiscountEngine.cs))

`DiscountEngine`, kuralları `Func<Order, decimal>` olarak alır; birleştirme biçimi de bir strateji (`Combiners.Best`, `Combiners.Sum` veya yerinde bir lambda: *en iyi iki indirimin toplamı*). `Rules.Percentage(10)` gibi fabrikalar parametreyi **yakalayan** lambda döndürür; `When`/`Capped` kuralları birleştiren **higher-order function**'lardır. Yeni kural = yeni lambda, motor değişmez (Open/Closed). Karşılaştırma: J2.3'teki `IFeePolicy`/`IPaymentRule` sınıfları aynı fikrin daha törenli hâli. Durum taşıyan, birden çok metodu olan veya DI ile kaydedilecek stratejiler için interface/sınıf; tek metotluk davranış için delegate.

---

## 4. Gerçek dünya senaryosu

**"Test ortamında butonlar rastgele aynı kaydı siliyor."** Liste her satır için buton üretirken `for (var i…) button.Click += () => Delete(items[i])` yazılmış; tüm tıklamalar `i == items.Count` değeriyle çağrılıyor (`IndexOutOfRange`) ya da son kayıt siliniyor. Döngü-yerel kopya (veya `foreach`) ile çözüldü.

**"Sayfayı kapattık ama arka planda hâlâ veri çekiyor, bellek şişiyor."** Sayfa, singleton `NotificationService.Updated` olayına `+=` ile abone oluyor ama `Dispose`'ta `-=` yapmıyor. Her sayfa açılışı bir hayalet abone ekliyor. Çözüm: `Dispose`'ta `-=`, `static event`'ten vazgeçmek.

**"Yeni kampanya kuralı için servisin 3 yerini değiştirmek gerekiyor."** İndirim hesaplaması `if/else` zinciri; kuralları `Func<Order, decimal>` listesine taşıyıp motoru değişmez kıldık; yeni kampanya = tek bir lambda + test.

---

## 5. Mülakat perspektifi

Sık sorulanlar: **Closure nedir, döngüde neden sorun çıkarır?** ⭐ · `event` ile delegate farkı · `Func`/`Action`/`Predicate` · Multicast delegate dönüş değeri · Event abonelikten çıkmazsak ne olur? · Lambda ile local function farkı · `Expression<Func<>>` ile `Func<>` farkı · `EventHandler<T>` deseni.

**İpucu:** Closure sorusunda önce **tanım** ("değişkeni yakalar, değeri değil"), sonra **tuzak** (for → 3,3,3), **çözüm** (kopya / foreach) ve **bedel** (display class tahsisi, ömür uzaması) sırasını izleyin.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| `for` değişkenini lambda'da doğrudan kullanmak | Döngü-yerel kopya |
| Public delegate alanı | `event` |
| `OrderCreated(this, e)` ile null kontrolsüz tetiklemek | `OrderCreated?.Invoke(this, e)` |
| Event aboneliğini hiç kaldırmamak | `-=` / `IDisposable`; `static event`'ten kaçın |
| `+= x => …` ile abone olup sonra `-= x => …` yazmak | Aynı delegate nesnesini değişkende tutun |
| Dönüş değerli multicast'ten tüm dönüşleri beklemek | Yalnızca sonuncu; `GetInvocationList()` ile tek tek çağırın |
| Bir abonenin exception'ının diğerlerini etkilediğini unutmak | Tek tek çağır + yakala |
| Sıcak yolda her seferinde yeni closure üretmek | `static` lambda / önceden oluşturulmuş delegate |
| Lambda içinde büyük nesneyi gereksiz yakalamak | Yalnızca gereken değeri yerel değişkene alın |
| `async void` handler'ın hatasını yakalamayı beklemek | Yalnızca event handler'larda; hata yakalanamaz (→ J4) |
| `Func`'ı interface yerine kullanıp 5 delegate parametre geçmek | İlişkili davranışlar → interface |

---

## 7. Performans & trade-off

- **Delegate çağrısı** sanal çağrıya yakın maliyetli (dolaylı); JIT yakalamayan sabit lambda'yı önbelleğe alır. Sıcak döngüde tahsis eden closure'ı `static` lambda veya local function ile değiştirin.
- **Closure tahsisi:** yakalayan her lambda (ve display class) heap nesnesidir; `LINQ` zincirleri çok sayıda küçük nesne üretir (→ S2.3).
- **Multicast** zincir değiştikçe (`+=`) yeni dizi/nesne üretir; çok sık abone/ayrıl döngüsü pahalıdır.
- **Event vs doğrudan çağrı:** event gevşek bağlılık verir, ama akışı izlemeyi zorlaştırır (kim dinliyor?) ve aboneler senkron çalıştığı için gecikme ekler.
- **Weak event** deseni sızıntıyı önler fakat karmaşıklık ekler.
- **Expression ağaçları** `Compile()` maliyetlidir; sıcak yolda derlenmiş delegate'i önbelleğe alın.

---

## 8. Kaynaklar

- Delegate'ler: <https://learn.microsoft.com/dotnet/csharp/programming-guide/delegates/>
- Lambda ifadeleri: <https://learn.microsoft.com/dotnet/csharp/language-reference/operators/lambda-expressions>
- Closure / yakalanan değişkenler (dış değişken yakalama): <https://learn.microsoft.com/dotnet/csharp/language-reference/operators/lambda-expressions#capture-of-outer-variables-and-variable-scope-in-lambda-expressions>
- Events: <https://learn.microsoft.com/dotnet/csharp/events-overview>
- .NET olay tasarımı deseni (`EventHandler<T>`): <https://learn.microsoft.com/dotnet/standard/events/observer-design-pattern>
- Local functions: <https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/local-functions>
- Expression ağaçları: <https://learn.microsoft.com/dotnet/csharp/advanced-topics/expression-trees/>
- Zayıf olay deseni: <https://learn.microsoft.com/dotnet/desktop/wpf/events/weak-event-patterns>
