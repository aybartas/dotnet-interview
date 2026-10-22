# J1.5 IDE, Debugging & Kod Kalitesi — Mülakat Soruları

> Teori: [README.md](README.md) (çözümler README 3.7'de, spoiler). ⭐ = çok sık sorulan.

### 1. ⭐ Bir hatayı nasıl ayıklarsın? Süreci anlat.
Önce hatayı **yeniden üretirim** (en küçük girdi/senaryo). Sonra hipotez kurarım ve debugger ile **gözlemlerim**: şüpheli yere breakpoint koyar, Locals/Watch ile değerleri, Call Stack ile "buraya nasıl geldim"i incelerim; istisna varsa fırlatıldığı noktada dururum (first-chance). Kök nedeni bulunca düzeltir ve aynı hatayı yakalayan bir **test eklerim**. Tahmin yürütüp rastgele değişiklik yapmam; `Console.WriteLine` yerine tracepoint/loglama tercih ederim.

### 2. Step Over, Step Into ve Step Out farkı nedir?
**Step Over (F10):** satırı çalıştırır, metot çağrısının içine girmez. **Step Into (F11):** çağrılan metodun içine girer (framework kodu "Just My Code" ile atlanabilir). **Step Out (Shift+F11):** mevcut metodun sonuna kadar çalışıp çağırana döner. Ek olarak **Run to Cursor** belirli bir satıra kadar çalıştırır.

### 3. ⭐ Conditional breakpoint, hit count ve tracepoint nedir? Ne zaman kullanırsın?
**Conditional breakpoint:** yalnızca ifade doğru olduğunda durur (`order.Quantity < 0`); binlerce iterasyondaki tek bozuk kaydı bulmak için. **Hit count:** N'inci geçişte durur. **Tracepoint (log point):** durmadan, kodu değiştirmeden mesaj yazar (`{order.Id}`); zamanlama hassas kodda veya çok sayıda kaydı izlerken kullanışlıdır, ayrıca `Console.WriteLine` eklemeyi ve sonra silmeyi unutma riskini ortadan kaldırır.

### 4. ⭐ First-chance exception nedir? Neden önemlidir?
Bir istisna fırlatıldığı anda — henüz `catch` aranmadan — debugger'a bildirilir; buna first-chance denir. Yakalanmazsa second-chance (unhandled) olur ve process çöker. Varsayılan debugger yalnızca unhandled istisnada durur; ama o zaman çoğu zaman bağlam kaybolmuştur. Exception Settings'te ilgili türü işaretleyince istisna **fırlatıldığı satırda** durur ve Call Stack/Locals ile kök neden görülür. Yutulan (`catch { }`) istisnaları bulmak için de aynı ayar kullanılır.

### 5. Immediate Window ve Watch farkı nedir?
**Watch**, seçtiğiniz ifadeleri her duruşta otomatik yeniden değerlendirip gösterir. **Immediate Window** ise o an ifade **çalıştırmanıza** izin verir (`list.Where(x => x > 1).Count()`, `percent / 100`, hatta değişkene atama); hipotezleri koda dokunmadan denemek için kullanılır.

### 6. Call Stack penceresi ne işe yarar?
Yürütmenin mevcut noktaya hangi metot zinciriyle geldiğini gösterir. Bir üst çerçeveyi seçince o çağrının yerelleri de incelenebilir. İstisna analizinde "belirti" ile "kök neden"in farklı çerçevelerde olduğu durumlarda (ör. `foreach` içinde `MoveNext` hatası, asıl neden gövdedeki `Remove`) kritiktir.

### 7. Make Object ID ne işe yarar?
Bir referansa kalıcı bir etiket (`{1#}`) verir. İki değişkenin **aynı nesneyi** mi yoksa eşit değerli farklı nesneleri mi gösterdiğini anlamak için kullanılır (ör. iki kullanıcının aynı paylaşılan listeyi kullanması hatası).

### 8. Debug ile Release yapılandırması arasındaki fark nedir? "Debug'da çalışıyor, Release'de çalışmıyor" neden olabilir?
Debug: optimizasyon kapalı, tam hata ayıklama sembolleri, `#if DEBUG` ve `Debug.Assert`/`[Conditional("DEBUG")]` çağrıları aktif. Release: derleyici/JIT optimizasyonları açık (satırlar yeniden düzenlenir, değişkenler "optimized away" olur), `Debug.*` çağrıları ve **argüman ifadeleri derlemeden silinir**. Sorunun tipik nedenleri: `Debug.Assert` içinde yan etkili ifade, optimizasyona duyarlı/zamanlamaya bağlı race condition, `#if DEBUG` ile farklılaşan kod yolları, başlatılmamış durumun Debug'da fark edilmemesi. Her iki yapılandırmayı da test etmek gerekir.

### 9. `dotnet watch` ve Hot Reload nedir?
`dotnet watch` uygulamayı çalıştırır ve dosya değişikliklerini izler. **Hot Reload**, çalışan uygulamaya kod değişikliğini **yeniden başlatmadan** uygular (bellekteki durum korunur); metot gövdesi değişiklikleri desteklenir. İmza değiştirmek gibi "rude edit"lerde uygulayamaz ve yeniden başlatmayı önerir. Geliştirme hızını artırır; üretim için bir yöntem değildir.

### 10. ⭐ `NullReferenceException` nedir, nasıl önlenir?
`null` bir referansın üyesine erişmeye çalışınca çalışma zamanında fırlatılır. Önleme: nullable reference types (`<Nullable>enable</Nullable>`) ile derleme zamanında yakalamak; `?.`, `??`, `is not null`, `ArgumentNullException.ThrowIfNull`, `required` üyeler; API'de "null olabilir mi?"yi tipe yansıtmak (`T?`); `Try` desenleri ve `Option`/boş nesne tasarımı.

### 11. Nullable reference types nedir? `string` ile `string?` farkı?
C# 8+ özelliği: açıkken `string` "null olmamalı", `string?` "null olabilir" demektir ve derleyici null akışını analiz edip `CS8600/8602/8603/8604/8618` gibi uyarılar üretir. Bunlar derleme zamanı ipuçlarıdır; çalışma zamanında ek koruma getirmez (reflection/serileştirme/`!` ile delinebilir). Yeni projelerde varsayılan olarak açıktır.

### 12. ⭐ Null-forgiving operatörü (`!`) ne yapar? Ne zaman kullanılır?
Derleyiciye "bu ifade null değil, emin ol" der ve uyarıyı susturur; çalışma zamanında hiçbir kontrol eklemez, null ise yine `NullReferenceException` alırsınız. Yalnızca null olmadığını **kanıtlayabildiğiniz** yerde (ör. bir önceki satırda doğrulandı ama derleyici akışı çıkaramıyor, ya da test kodu) kullanılır. "Uyarı gitsin" diye serpiştirilirse hata erteleme borcudur.

### 13. `TreatWarningsAsErrors` açar mısın? Eski bir projeye nasıl uygularsın?
Yeni projede evet: uyarı birikmez, kalite sabit kalır. Eski projede tümünü açmak yüzlerce hata üretip ekibi durdurur; bunun yerine kademeli: önce `WarningsAsErrors=nullable` gibi bir kategoriyle başlar, kuralları `.editorconfig`'te tek tek `error`'a çeker, mevcut ihlalleri temizler (gerekirse geçici `NoWarn` ile gerekçeli bastırma), sonra genel bayrağı açarım. Yeni uyarı getiren SDK/analyzer yükseltmeleri için sürümleri sabitlerim (`global.json`).

### 14. Roslyn analyzer nedir? `CS`, `CA` ve `IDE` önekleri ne demek?
Derleyicinin kod modeline takılan, hem IDE'de hem derlemede çalışan statik analiz kurallarıdır. `CS####` derleyici uyarı/hataları (ör. CS8602), `CA####` kod analizi (kalite/performans/güvenlik, ör. CA1822), `IDE####` stil ve refactoring kuralları (ör. IDE0005). Önem dereceleri `none/silent/suggestion/warning/error` olup `.editorconfig` ile kural veya kategori bazında ayarlanır. .NET SDK yerleşik analyzer'larla gelir; ek paketlerle (StyleCop, Sonar) genişletilebilir.

### 15. ⭐ `.editorconfig` nedir, ne işe yarar?
Repoyla birlikte giden, IDE'lerin, `dotnet format`'ın ve derleyicinin okuduğu biçim, stil ve analyzer önem derecesi kural dosyasıdır (girinti, satır sonu, `var` tercihi, adlandırma kuralları, kural bazında severity). Takımdaki herkesin aynı kurallara tabi olmasını sağlar, stil tartışmalarını PR'lardan çıkarır. Klasör ve altını kapsar; `root = true` yukarıya bakmayı durdurur. IDE kurallarının derlemede de zorlanması için `EnforceCodeStyleInBuild` açılır.

### 16. `dotnet format` ne yapar? CI'da nasıl kullanılır?
Kod biçimini, stil kurallarını ve analyzer düzeltmelerini `.editorconfig`'e göre otomatik uygular (`dotnet format`, ya da kapsam için `whitespace`/`style`/`analyzers`). CI'da `dotnet format --verify-no-changes` dosyaları **değiştirmeden** yalnızca denetler; değişiklik gerekiyorsa sıfırdan farklı çıkış koduyla başarısız olur ve PR'ı engelleyen bir kapı olur. Büyük biçim değişikliklerini ayrı commit/PR'a koymak `git blame`'i temiz tutar.

### 17. Visual Studio, Rider ve VS Code arasındaki fark nedir? Hangisini seçersin?
Visual Studio: Windows'a özel, en derin .NET/Azure entegrasyonu, güçlü debugger/profiler. Rider: cross-platform, hızlı refactoring/arama (ReSharper motoru), ticari. VS Code + C# Dev Kit: hafif, cross-platform, Docker/uzak geliştirme ve hızlı düzenleme için. Seçim projeye, platforma ve takım standartlarına göre; asıl önemli olan CLI (`dotnet build/test`) ile IDE'den bağımsız çalışabilmek ve ayarları (`.editorconfig`, `global.json`) repoda tutmaktır.

### 18. `async` kodda debugging neden farklıdır? Unutulan `await` nasıl bulunur?
`await` noktasında kontrol çağırana döner ve devamı sonra (muhtemelen başka thread'de) çalışır; bu yüzden F10 beklenmedik atlıyor gibi görünür. Task'ın durumu `task.Status` ile izlenir; Tasks/Parallel Stacks pencereleri bekleyen task'ları gösterir. Unutulan `await` çoğu zaman yarış durumu gibi belirti verir (sonuç henüz hazır değil); derleyici `CS4014` ile uyarır — bu uyarıyı `warning`/`error` yapmak iyi pratiktir.

### 19. Üretimdeki bir hatayı nasıl incelersin? Debugger bağlar mısın?
Genellikle hayır: bağlanmak süreci durdurur ve güvenlik riski taşır. Önce log, metrik ve dağıtık trace (gözlemlenebilirlik), gerekirse dump alıp (`dotnet-dump`) yerelde analiz ederim; hatayı yerelde aynı veriyle yeniden üretip debugger'la çözer, sonra test eklerim. Gereken gözlemlenebilirliği önceden kodlamak en iyi yoldur (→ M13).

### 20. `#pragma warning disable`, `[SuppressMessage]` ve `NoWarn` farkı nedir?
`#pragma warning disable/restore` belirli bir kod bölgesinde (en dar kapsam), `[SuppressMessage]` bir üye/tip üzerinde, `NoWarn` (veya `.editorconfig`'te `severity = none`) tüm projede uyarıyı kapatır. En dar kapsam tercih edilir ve her bastırma için **gerekçe** yazılır; geniş kapsamlı bastırma borç biriktirir.
