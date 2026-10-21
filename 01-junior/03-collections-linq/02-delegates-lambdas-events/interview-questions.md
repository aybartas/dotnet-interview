# J3.2 Delegates, Lambdas & Events — Mülakat Soruları

> Teori: [README.md](README.md) · Pratik: [src/](src/) · Test: [tests/](tests/). ⭐ = çok sık sorulan.

### 1. Delegate nedir? `Func`, `Action` ve `Predicate` farkı?
Delegate, bir metoda (veya metot zincirine) tip güvenli referans tutan nesnedir; imza (parametreler + dönüş tipi) delegate tipiyle belirlenir. `Func<T1,…,TResult>` dönüş değeri olan, `Action<T1,…>` dönüş değeri olmayan (void), `Predicate<T>` `bool` dönen tek parametreli (`Func<T,bool>` ile eşdeğer) hazır generic delegate'lerdir; kendi delegate tipini yazmak nadiren gerekir.

### 2. Method group nedir?
Parantezsiz metot adının (ör. `Console.WriteLine`) imzası uyuşan bir delegate'e dönüştürülmesidir: `words.ForEach(Console.WriteLine)`. Derleyici overload'ı hedef delegate imzasına göre seçer; belirsizlikte lambda ile açıkça yazılır.

### 3. Multicast delegate nedir? Dönüş değeri nasıl davranır?
Bir delegate `+=` ile birden çok metodu zincirleyebilir; çağrıldığında metotlar eklenme sırasıyla çalışır. Dönüş değerli zincirde yalnızca **son** metodun dönüş değeri alınır. `GetInvocationList()` ile tek tek çağrılabilir. Delegate'ler immutable'dır: `+=` yeni bir delegate üretir.

### 4. Lambda expression nedir? Expression lambda ile statement lambda farkı?
`=>` ile yazılan anonim fonksiyondur. Expression lambda gövdesi tek ifadedir (`x => x * x`, değer örtük döner); statement lambda süslü parantez içinde birden çok ifade ve açık `return` içerir. Lambda hedef tipe göre bir delegate'e veya `Expression<TDelegate>`'e dönüşür.

### 5. ⭐ Closure nedir? Döngüde neden sorun çıkarır?
Closure, bir lambda'nın tanımlandığı kapsamdaki dış değişkenleri **değişkenin kendisi olarak** yakalamasıdır (değeri kopyalamaz; lambda çalıştığı anda okur). `for` döngüsünde sayaç değişkeni tektir; `for (var i = 0; i < 3; i++) funcs.Add(() => i);` ile oluşan tüm lambda'lar aynı `i`'yi paylaşır ve döngü bitince `i == 3` olduğu için hepsi `3` döner. Çözüm: her iterasyonda döngü-yerel kopya (`var copy = i;`). `foreach` değişkeni C# 5'ten beri her iterasyonda yenidir, tuzak yoktur.

### 6. Derleyici closure için ne üretir? Maliyeti nedir?
Yakalanan değişkenler heap'te derleyicinin ürettiği bir "display class" nesnesine taşınır ve lambda onun bir metodu olur. Maliyet: her closure için nesne tahsisi (GC baskısı) ve yakalanan değişkenin (büyük nesne olabilir) lambda yaşadığı sürece bellekte kalması. Yakalamayan lambdalar önbelleğe alınır; `static` lambda yakalamayı derleme zamanında yasaklar.

### 7. ⭐ `event` anahtar sözcüğü ile çıplak delegate alanı arasındaki fark nedir?
Public delegate alanında dışarıdaki herkes `= null` ile tüm aboneleri silebilir, atamayla ezebilir, `Invoke` ile sahte olay üretebilir. `event` delegate'i kapsüller: dışarıya yalnızca `+=` ve `-=` açılır; tetikleme ve temizleme yalnızca yayıncı sınıfın içindedir. Bu, yayıncının olayı üzerindeki kontrolünü ve gevşek bağlı yayıncı–abone ilişkisini garanti eder.

### 8. `EventHandler<TEventArgs>` deseni nedir?
Standart olay imzasıdır: `void Handler(object? sender, TEventArgs e)`. `sender` olayı yayınlayan nesnedir, `e` (`EventArgs`'tan türeyen sınıf) olay verisini taşır. Yayıncı `protected virtual void OnXxx(XxxEventArgs e) => Xxx?.Invoke(this, e);` biçiminde bir tetikleyici sunar.

### 9. Olay tetiklenirken bir abone exception fırlatırsa ne olur?
Çağrı zinciri kesilir: sonraki aboneler çalışmaz ve exception olayı tetikleyene yayılır. Dayanıklılık gerekiyorsa `GetInvocationList()` ile her aboneyi ayrı çağırıp exception'ları yakalarsınız. Aboneler ayrıca yayıncının thread'inde senkron çalışır; yavaş abone yayıncıyı bloklar.

### 10. ⭐ Event aboneliğinden çıkmazsak ne olur?
Yayıncı, abone nesnesine (delegate'in `Target`'ı) referans tutar. Yayıncı uzun yaşıyorsa (singleton, `static event`) ve kısa ömürlü abone `-=` yapmazsa, abone GC tarafından toplanamaz: memory leak ve hayalet handler'lar (kapatılmış ekranların handler'ları çalışmaya devam eder). Çözüm: `Dispose`/yaşam döngüsü sonunda `-=`, zayıf olay desenleri, `static event`'ten kaçınmak. Teşhis: bellek profilleyici ile "yayıncı → abone" retention path.

### 11. Lambda ile abone olduysam aboneliği nasıl kaldırırım?
`-=` için aynı delegate nesnesi gerekir; yeni yazılan aynı görünümlü lambda farklı bir nesnedir ve aboneliği kaldırmaz. Lambda'yı bir değişkende (`EventHandler h = (s, e) => …;`) tutup hem `+=` hem `-=`'de onu kullanırsınız ya da adlı bir metot kullanırsınız.

### 12. Local function ile lambda arasındaki fark nedir?
Local function adlı bir iç metottur: özyineleme yapabilir, tanımdan önce çağrılabilir, delegate'e dönüştürülmedikçe tahsis üretmez, generic/iterator olabilir, `static` ile yakalamayı yasaklayabilir. Lambda bir delegate (veya expression) değeridir: parametre olarak geçirilir, LINQ'te, olaylarda ve saklamada kullanılır; her zaman delegate nesnesi (ve yakalıyorsa closure) üretir. Tek metotta kullanılan yardımcı mantık için local function, değer olarak taşınacak davranış için lambda.

### 13. `Func<T>` ile `Expression<Func<T>>` farkı nedir? Neden önemli?
`Func<T>` derlenmiş, çalıştırılabilir koddur; `Expression<Func<T>>` aynı lambda'nın ağaç (veri) temsilidir ve incelenip başka bir şeye çevrilebilir. EF Core gibi sağlayıcılar `IQueryable` üzerindeki lambda'ları SQL'e çevirmek için ağaçları kullanır; bu yüzden `IQueryable.Where` `Expression<Func<>>`, `IEnumerable.Where` `Func<>` alır (→ J3.3, J9.4, M1.3).

### 14. Delegate'i strateji deseni için nasıl kullanırsın? Ne zaman interface tercih edersin?
Davranış değişken ve tek metotluksa strateji bir `Func<…>` parametresi/alanı olarak verilir (ör. kuralları `Func<Order, decimal>` listesi olarak tutan indirim motoru); yeni davranış yeni lambda olur ve motor değişmez. Strateji durum taşıyorsa, birden çok metodu varsa, adlandırılıp DI ile kaydedilecekse veya bağımsız test edilecek karmaşıklıktaysa interface/sınıf tercih edilir.

### 15. Null bir delegate'i çağırırsam ne olur? Güvenli yol nedir?
`NullReferenceException`. Olaylarda abone yoksa delegate `null`'dır; bu yüzden `handler?.Invoke(...)` kullanılır (null kontrolü ve çağrı tek adımda, yarış durumuna karşı güvenli yerel kopya mantığıyla).

### 16. Closure çok thread'li kodda neden tehlikelidir?
Yakalanan değişken lambda ile dış kapsam (ve başka lambda'lar/thread'ler) arasında paylaşılır; birden çok thread değiştirirse yarış koşulu oluşur. Değişkeni kopyalayın, immutable tutun veya senkronize edin (→ J4, M4).
