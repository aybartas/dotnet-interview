# J2.3 OOP Temelleri — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. ⭐ OOP'un dört ilkesini kendi kodundan örnekle anlat.
- **Encapsulation:** `BankAccount`'ta `Balance { get; private set; }` ve `Withdraw` içinde yetersiz bakiye kontrolü → bakiye dışarıdan keyfi değişemez, negatif olamaz (invariant korunur).
- **Abstraction:** `IReportExporter` — çağıran CSV mi PDF mi bilmez, yalnızca `Export` sözleşmesini bilir; "nasıl"ı saklar.
- **Inheritance:** `SavingsAccount` is-a `Account`; ortak `Deposit` devralınır, faiz hesabı eklenir.
- **Polymorphism:** `Shape.Area()` — `Circle`/`Rectangle` kendi uygulamasını verir, çağıran somut tipi bilmeden doğru sonucu alır.
En iyi örnek gerçek projeden olandır: "ödeme yöntemlerini kalıtımla modelledik, sonra composition'a çevirdik".

### 2. ⭐ Compile-time ve runtime polimorfizm farkı nedir?
Compile-time (static/early binding): hangi metodun çağrılacağına **derleyici** karar verir — overloading, generic'ler, operator overloading; seçim argümanların/değişkenin **derleme zamanı tipine** göre yapılır. Runtime (dynamic/late binding): **nesnenin gerçek tipine** göre çalışma zamanında çözülür — `virtual`/`abstract` + `override`, sanal metot tablosu (vtable) üzerinden. Örnek tuzak: `object o = "x"; Print(o)` overload'ı `object` sürümüne gider.

### 3. ⭐ `abstract class` ile `interface` farkı nedir? C# 8 default implementation sonrası fark ne kaldı?
Abstract class: ortak kod ve **durum** (alan), constructor, `protected`/`private` üyeler taşıyabilir; ancak tek kalıtım vardır. Interface: sözleşme, bir sınıf istediği kadar uygulayabilir. C# 8 ile interface gövdeli üye (default implementation) taşıyabilir, ama hâlâ **instance alan tutamaz, constructor'ı yoktur**; default implementation'a yalnızca interface referansı üzerinden erişilir ve asıl amacı yayınlanmış interface'e üye eklerken uygulayanları kırmamaktır. Pratik: önce interface; ortak kod/durum paylaşımı gerekiyorsa abstract class.

### 4. Abstract sınıf örneklenemiyorsa constructor'ı neden olabilir?
Constructor, türeyen sınıfın constructor'ı tarafından `base(...)` ile çağrılır ve taban sınıfın durumunu başlatır (ör. `Report(string title)`). Abstract sınıfın kendisi `new` ile örneklenemez, ama bir türeyen örnek oluşturulurken taban kısmı onunla kurulur.

### 5. ⭐ `override` ile `new` (method hiding) farkı nedir?
`override` sanal metodu değiştirir; çağrı **nesnenin gerçek tipine** göre çözülür (polimorfizm). `new` taban üyeyi **gizler**; çağrı **değişkenin derleme zamanı tipine** göre çözülür. `Base b = new Derived(); b.Hello()` → override'da `Derived.Hello`, new'de `Base.Hello` çalışır; aynı nesne hangi tipte tutulduğuna göre farklı davranır. `new` yazmak CS0108 uyarısını susturur ve çoğu zaman bir tasarım hatasının işaretidir.

### 6. `sealed` ne işe yarar?
`sealed class` kalıtımı kapatır; `sealed override` bir override'ın daha fazla override edilmesini engeller. Tasarımı koruma (kırılgan taban sınıf riskini azaltma) ve JIT'in çağrıları devirtualize/inline edebilmesi sağlar. Kalıtım için tasarlamadığınız sınıfları `sealed` yapmak iyi bir varsayılandır.

### 7. Explicit interface implementation nedir? Ne zaman gerekir?
Bir interface üyesini `string IBird.Move()` biçiminde, sınıfın public yüzeyine koymadan uygulamaktır; yalnızca o interface referansı üzerinden erişilir. İki interface aynı imzalı üye tanımlıyorsa (diamond benzeri çakışma) her birine farklı davranış vermek için, ya da interface üyesini sınıfın normal API'sinden gizlemek için kullanılır.

### 8. Upcasting ve downcasting nedir? `as`, `is` ve pattern matching farkı?
Upcast (türeyen → taban) güvenli ve örtüktür. Downcast (taban → türeyen) çalışma zamanında doğrulanır: `(Dog)x` başarısızsa `InvalidCastException`, `x as Dog` `null` döner, `x is Dog d` ise kontrol + cast + değişkeni tek adımda yapar. Çok downcast gerekiyorsa davranış `virtual` metoda taşınmalıdır (Open/Closed).

### 9. ⭐ Aggregation ile composition farkı nedir?
İkisi de "has-a"dır ama **sahiplik ve yaşam süresi** farklıdır. Aggregation zayıftır: parça bütünden bağımsız yaşar, dışarıda oluşturulup verilir, başka bütüne taşınabilir (Team–Employee: takım dağılsa çalışan kalır). Composition güçlüdür: parçayı bütün yaratır ve sahiplenir, parça bütünle birlikte yok olur (Order–OrderLine: sipariş iptal olunca satırların anlamı kalmaz). Kodda: composition'da parçanın constructor'ı `internal` ve yalnızca bütün üzerinden üretilir. DDD'de aggregate sınırı bu ayrımdan beslenir.

### 10. Association nedir?
Sahiplik içermeyen "uses-a" ilişkisidir: iki nesne birbirini kullanır ama yaşam süreleri bağımsızdır (Order ↔ Courier). Aggregation ve composition, association'ın sahiplik içeren özel biçimleridir.

### 11. ⭐ "Composition over inheritance" ne demek? Neden?
Davranışı kalıtımla devralmak yerine **nesneleri birleştirerek** (içerip delege ederek) kurmak. Nedenleri: fragile base class (taban sınıf değişikliği alt sınıfları sessizce kırar), derin hiyerarşilerin bakım maliyeti, tek kalıtım kısıtı (taksit × fraud gibi iki boyutu birleştirememe ve kodu kopyalama), kalıtımın derleme zamanında sabit olması (composition'da strateji çalışma zamanında değişir), parçaların ayrı test edilebilmesi. Kalıtım gerçek is-a + Liskov uyumu varsa yine doğru araçtır.

### 12. Kalıtımla yazılmış kodu nasıl composition'a refactor edersin?
Önce mevcut davranışı **testlerle sabitlerim**. Sonra değişen boyutu (komisyon hesabı gibi) seçip arkasına interface koyar, alt sınıf yerine **nesne** veririm (strateji). Kalan alt sınıflardaki farkları da küçük nesnelere/kurallara bölerim; hiyerarşiyi sildiğimde yöntem seçimi konfigürasyon/factory olur. **Her adımdan sonra testleri çalıştırırım**; kırmızı olursa adımı geri alırım. (Bu konudaki `PaymentRefactoring` üç adımı aynı testlerle gösterir.)

### 13. Fragile base class problemi nedir?
Taban sınıfın iç davranışındaki masum görünen bir değişikliğin (ör. `Validate` çağrı sırası, bir protected metodun ne zaman çağrıldığı), türeyen sınıfları beklenmedik şekilde bozmasıdır; çünkü türeyen sınıf tabanın uygulama detayına bağımlıdır. Kalıtım "white-box" yeniden kullanımdır, composition "black-box"tır.

### 14. ⭐ Coupling ve cohesion nedir? Nasıl ölçersin?
Coupling, bir sınıfın başkasını ne kadar bildiğidir (hedef: gevşek); cohesion, sınıfın üyelerinin aynı amaca ne kadar hizmet ettiğidir (hedef: yüksek). Ölçüm soruları: "Bu sınıfı değiştirince kaç yer etkileniyor?", "Bunu test etmek için kaç somut şey kurulmalı?" (coupling); "Bu sınıfın kaç farklı değişme nedeni var?" (cohesion, SRP).

### 15. Constructor içinde `new Concrete()` ile bağımlılık yaratmanın maliyeti nedir?
Sınıf somut tipe sıkı bağlanır: test edilemez (gerçek e-posta/DB yan etkisi), gönderim kanalını değiştirmek sınıfı değiştirmeyi gerektirir, bağımlılık imzada görünmez. Çözüm: interface soyutlaması ve constructor injection (Dependency Inversion); testte sahte (fake) uygulama verilir.

### 16. SOLID'i özetle; SRP ve DIP'i örnekle.
Single Responsibility (tek değişme nedeni), Open/Closed (yeni davranış = yeni sınıf), Liskov Substitution (türeyen tabanın yerine sözleşmeyi bozmadan geçer), Interface Segregation (küçük, istemciye özel arayüzler), Dependency Inversion (üst seviye soyutlamaya bağımlı). SRP örneği: ödeme refactor'unda her kural/politika ayrı sınıf. DIP örneği: `OrderService(INotifier)` somut `SmtpSender`'a değil arayüze bağlıdır.

### 17. `struct` kalıtım alabilir mi? Interface uygulayabilir mi?
Struct sınıftan veya başka struct'tan türeyemez (örtük olarak `ValueType`'tan türer, fiilen sealed); interface uygulayabilir. Ancak struct'ı interface referansına atamak boxing yapar; generic kısıt (`where T : IFoo`) ile boxing'siz çağrı yapılabilir.

### 18. `static` sınıf/üye ne zaman uygundur? "Her şeyi static yapma" eleştirisi nedir?
Durumsuz, saf yardımcı fonksiyonlar (`Math`, extension method'lar, factory'ler) için uygundur. Eleştiri: static üyeler imzada görünmeyen gizli bağımlılıktır, mock'lanamaz, paylaşılan durum paralel testleri/thread'leri bozar, polimorfizme katılamaz. `DateTime.Now`, `Random`, dosya sistemi gibi ortam bağımlılıklarını soyutlayıp (`TimeProvider`, `IFileSystem`) enjekte etmek testi deterministik yapar.

### 19. Liskov Substitution Principle'ı ihlal eden bir örnek ver.
`Square : Rectangle` klasik örnektir: `Rectangle`'ın "genişlik ve yüksekliği bağımsız ayarlanır" sözleşmesi `Square`'da bozulur (genişliği değiştirmek yüksekliği de değiştirir), `Rectangle` bekleyen kod `Square` ile yanlış çalışır. Başka örnek: `ReadOnlyCollection`'ı `IList`'in `Add`'ini `NotSupportedException` fırlatarak uygulaması. Çözüm: sözleşmeyi gevşetmek, ortak soyutlama (`IShape`) veya composition.

### 20. Association, aggregation, composition ve inheritance arasında hangisini ne zaman seçersin?
Sadece kullanıyorsam (sahiplik yok) association; parça bağımsız anlam taşıyor ve paylaşılabiliyorsa aggregation; parça bütün olmadan anlamsızsa ve yaşam döngüsü tek yerde yönetilecekse composition; gerçek bir *is-a* ve Liskov uyumu varsa inheritance. Şüphede composition tercih edilir.
