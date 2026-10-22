# J1.5 IDE, Debugging & Kod Kalitesi Araçları

> 📁 `ConsoleApp/01-junior/01-dev-foundations/05-ide-debugging/` · Önkoşul: J1.4 · Çözümler (spoiler): [3.7 sonu](#egzersiz-çözümleri-spoiler) · Sorular: [`interview-questions.md`](interview-questions.md)

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

Bu konu iki şeyi kapsar:

1. **Debugging:** Çalışan kodu durdurup içini incelemek — hatayı tahmin etmek yerine **gözlemlemek**.
2. **Kod kalitesi araçları:** Hataları kod çalışmadan, derleme/IDE aşamasında yakalayan **statik analiz** (Roslyn analyzer'ları, nullable analizi) ve ekip genelinde aynı biçimi zorlayan **`.editorconfig` + `dotnet format`**.

İkisi birbirini tamamlar: analyzer, hatanın bir kısmını **yazarken** yakalar; debugger, kaçanları **çalışırken** bulur.

### 1.1 IDE / editör seçenekleri

| | Visual Studio | JetBrains Rider | VS Code + C# Dev Kit |
|---|---------------|-----------------|----------------------|
| Platform | Windows (macOS sürümü kullanımdan kalktı) | Windows, Linux, macOS | Windows, Linux, macOS |
| Tür | Tam IDE | Tam IDE (ReSharper motoru) | Hafif editör + eklentiler |
| Güçlü yanı | En derin .NET entegrasyonu: profiler, designer'lar, Azure, gelişmiş debugger (IntelliTrace/Snapshot…) | Çok hızlı refactoring/arama, cross-platform, çoklu dil | Hafif, hızlı açılış, tek editörde her dil, uzak/container geliştirme |
| Lisans | Community ücretsiz; Pro/Enterprise ücretli | Ticari (kişisel/eğitim için ücretsiz seçenekler değişebilir) | Ücretsiz (C# Dev Kit lisans koşullarını kontrol edin) |
| Ne zaman? | Windows ağırlıklı kurumsal, WPF/WinForms/Azure | Cross-platform takım, yoğun refactoring | Hafif iş, Docker/Linux, mülakat/canlı kod |

Bunlar araçtır; **CLI hepsinin altındadır** (`dotnet build/test` her yerde aynı) — IDE'den bağımsız çalışabilmek önemlidir (→ J1.4).

---

## 2. Neden?

- **Hata ayıklama günlük işin büyük kısmıdır.** `Console.WriteLine` ile tahmin yürütmek yavaştır, kodu değiştirir, canlıda tehlikelidir. Debugger'la **gerçek durumu** (değişkenler, çağrı zinciri, iş parçacıkları) görürsünüz.
- **Bir hatayı erken yakalamak ucuzdur.** Analyzer uyarısı: saniyeler. Debugger'da bulunan hata: dakikalar. Production'da bulunan: saatler/günler ve itibar kaybı.
- **Takım tutarlılığı:** `.editorconfig` "tab mı boşluk mu", "private alan adı" tartışmalarını bitirir; PR'larda stil yorumları yerine tasarım konuşulur.
- **Nullable analizi `NullReferenceException`'ı** — "milyar dolarlık hata" — çalışmadan yakalar. Mülakatta sorulan temel konudur.

---

## 3. Nasıl?

### 3.1 Debugger temelleri

| Özellik | Kısayol (VS) | Ne yapar? |
|---------|--------------|-----------|
| **Breakpoint** | F9 | O satıra gelindiğinde yürütmeyi durdurur |
| **Step Over** | F10 | Satırı çalıştır, metot çağrısının **içine girme** |
| **Step Into** | F11 | Çağrılan metodun içine gir |
| **Step Out** | Shift+F11 | Mevcut metottan çıkana kadar çalış |
| **Run to Cursor** | Ctrl+F10 | İmlecin olduğu satıra kadar çalış |
| **Continue** | F5 | Sonraki breakpoint'e kadar devam |
| **Set Next Statement** | Ctrl+Shift+F10 | Yürütme noktasını elle taşı (dikkat: durumu tutarsız bırakabilir) |

**Breakpoint çeşitleri** ⭐

| Çeşit | Ne zaman? | Örnek |
|-------|-----------|-------|
| **Normal** | Bir satırın durumunu görmek | — |
| **Conditional** | Binlerce iterasyondan yalnızca *belirli* durumda durmak | `order.Quantity < 0` ([Egzersiz 6](#egzersiz-çözümleri-spoiler)) |
| **Hit count** | N'inci geçişte durmak | "7345'inci iterasyonda" |
| **Tracepoint (log point)** | **Durmadan** değer loglamak; kodu değiştirmeden, `Console.WriteLine` eklemeden | Mesaj: `{order.Id}: {order.Quantity}` |
| **Function / Data breakpoint** | Bir metoda girildiğinde / bir alan değiştiğinde durmak | (IDE'ye göre) |

**İnceleme pencereleri:**

| Pencere | İşlevi |
|---------|--------|
| **Locals / Autos** | Mevcut kapsamdaki değişkenler ve değerleri |
| **Watch** | Seçtiğiniz ifadeleri sürekli izler (`i`, `order.Total`, `list.Count`) |
| **Immediate Window** | O an **ifade çalıştırın/değerlendirin**: `percent / 100`, `list.Where(x => x > 1).Count()`; hatta değişkene atama yapıp denemek ([Egzersiz 5](#egzersiz-çözümleri-spoiler)) |
| **Call Stack** | "Buraya nasıl geldim?" — çağrı zinciri; üstteki çerçeveyi seçip o noktadaki yerelleri de görebilirsiniz |
| **Make Object ID** | Referansa kalıcı etiket (`{1#}`): iki değişken **aynı nesneyi** mi gösteriyor? ([Egzersiz 8](#egzersiz-çözümleri-spoiler)) |
| **Threads / Tasks / Parallel Stacks** | Eşzamanlı kod, bekleyen `Task`'lar, deadlock teşhisi (→ J4, M4) |
| **Breakpoints penceresi** | Hepsini tek yerden aç/kapat, koşulları düzenle |

**Debug ≠ Release:** Debug yapılandırmasında optimizasyon kapalıdır ve ek hata ayıklama bilgisi vardır; Release'de derleyici/JIT kodu yeniden düzenler, değişkenler "optimized away" olur, `Debug.*` ve `[Conditional("DEBUG")]` çağrıları **silinir** ([Egzersiz 9](#egzersiz-çözümleri-spoiler)). "Debug'da çalışıyor, Release'de çalışmıyor" gerçek bir sorun sınıfıdır — çoğu zaman yan etkili assert, zamanlama/race ya da optimizasyona duyarlı kod.

### 3.2 Exception ayarları: "first-chance exception"da durmak ⭐

Bir istisna iki aşamada ele alınır:

- **First-chance exception:** İstisna **fırlatıldığı anda**, henüz `catch` aranmadan. Debugger buraya "ilk şans" olarak bildirim alır.
- **Second-chance (unhandled):** Hiçbir `catch` yakalamadı → process çöker.

Varsayılan olarak debugger yalnızca **işlenmeyen** (unhandled) istisnada durur — o noktada çoğu zaman stack zaten çözülmüş ve bağlam kaybolmuştur. Hatanın **fırlatıldığı satırı** görmek için: **Exception Settings** (VS: Ctrl+Alt+E; VS Code: Breakpoints → "Raised Exceptions") → ilgili istisna türünü ( `NullReferenceException`, `InvalidOperationException`…) işaretleyin; debugger istisna **fırlatılırken** durur, **Call Stack**'te zinciri ve **Locals**'ta durumu görürsünüz.

İpucu: **Just My Code** açıkken (varsayılan) framework içi kodu atlarsınız; bir kütüphanenin neden istisna fırlattığını görmek için kapatın. Yakalanıp yutulan (`catch { }`) istisnaları bulmak için de first-chance ayarı bire birdir.

### 3.3 `dotnet watch` ve Hot Reload

```bash
# In the ConsoleApp folder; runs exercise 3 and watches for file changes
dotnet watch -- J1.5 3       # (same as dotnet watch run -- J1.5 3)
```

- **Hot Reload:** Çalışan uygulamada **kod değişikliğini yeniden başlatmadan** uygular (durum/bellek korunur; çalışan bir web uygulamasında/döngüde değişiklik anında görünür). Metot gövdesi değişiklikleri sorunsuz uygulanır. Denemek için `Exercises/` altındaki bir egzersizin metot gövdesini değiştirip kaydedin.
- **"Rude edit"** (imza değiştirmek, yeni alan/tip eklemek gibi desteklenmeyen değişiklikler): Hot Reload uygulayamaz, yeniden başlatmayı önerir.
- Web projelerinde tarayıcı otomatik yenilenir. `Ctrl+R`: elle yeniden başlat.
- Faydası: UI/API geliştirirken "değiştir → derle → durdur → başlat → aynı sayfaya git" döngüsünü kaldırır. Debugger bağlıyken (IDE'de F5) de Hot Reload çalışır.

### 3.4 Roslyn analyzer'ları ve `.editorconfig`

**Roslyn analyzer**, derleyicinin kod çözümlemesine takılan kural paketleridir; hem **IDE'de** (yazarken, çizgi/ampul) hem **derlemede** çalışır. .NET SDK'da yerleşik gelir (`Microsoft.CodeAnalysis.NetAnalyzers`); ek paketlerle genişler (StyleCop, SonarAnalyzer…).

| Önek | Ne? | Örnek |
|------|-----|-------|
| `CS####` | **Derleyici** uyarı/hataları | `CS8602` (null dereference), `CS4014` (unutulan await) |
| `CA####` | **Kod analizi** (kalite, performans, güvenlik, kullanım) | `CA1822` (üye static olabilir), `CA1861` (sabit diziyi tekrar tekrar üretme) |
| `IDE####` | **Kod stili / refactoring** (IDE kuralları) | `IDE0005` (gereksiz using), `IDE0051` (kullanılmayan private üye), `IDE1006` (adlandırma) |

Her kuralın **önem derecesi (severity)** vardır: `none` < `silent` < `suggestion` < `warning` < `error`. Ayar yeri **`.editorconfig`** — kural bazında:

```ini
[*.cs]
dotnet_diagnostic.CS8602.severity = error     # one by one
dotnet_analyzer_diagnostic.category-Style.severity = suggestion   # by category
csharp_style_namespace_declarations = file_scoped:suggestion      # style preference + severity
```

Örnek tam dosya: [`AnalyzersNullable/.editorconfig`](AnalyzersNullable/.editorconfig) — biçim (girinti, satır sonu), `using`/`var`/namespace stili, **adlandırma kuralları** (interface `I…`, private alan `_camelCase`) ve analyzer önem dereceleri. `.editorconfig` **repoyla birlikte commit'lenir**; IDE'ler, `dotnet format` ve derleyici aynı dosyayı okur → "benim makinemde farklı biçimleniyor" biter.

**Derlemede zorlama:** IDE'deki `IDE####` stil kuralları varsayılanda yalnızca editörde görünür. Derlemede (ve CI'da) de çalışması için:

```xml
<EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
<AnalysisMode>Default</AnalysisMode>   <!-- Default < Recommended < All: the number of enabled CA rules grows -->
```

### 3.5 `dotnet format` ve `TreatWarningsAsErrors`

```bash
dotnet format                         # APPLIES format + style + analyzer fixes (modifies the files)
dotnet format whitespace              # whitespace/indentation/line endings only
dotnet format style                   # IDE style rules
dotnet format analyzers --severity warn
dotnet format --verify-no-changes     # does NOT modify; FAILS with a non-zero exit code if changes are needed → CI gate
```

**Uyarılar ve hatalar:**

| Ayar | Etki |
|------|------|
| `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>` | **Tüm** uyarılar derlemeyi kırar → uyarı birikmez (yeni projede önerilir) |
| `<WarningsAsErrors>nullable</WarningsAsErrors>` | Yalnızca nullable uyarıları hata → **kademeli geçişte pratik orta yol** |
| `<WarningsNotAsErrors>CS0618</WarningsNotAsErrors>` | `TreatWarningsAsErrors` açıkken bazı uyarıları istisna yapmak |
| `<NoWarn>CS1591</NoWarn>` | Bir uyarıyı tamamen sustur (**gerekçesiyle**) |
| `#pragma warning disable CS8602` | Tek bir yerde sustur (en dar kapsam) |

Mantık: **Uyarı = yarım hata.** 500 uyarılı projede 501.'yi kimse fark etmez ("broken windows"). Yeni projede baştan `TreatWarningsAsErrors`; eski projede önce `WarningsAsErrors=nullable`, sonra kural kural sıkılaştırma. Uyarıları susturmak (`#pragma`, `!`, `NoWarn`) borçtur: nedenini yazın.

### 3.6 Nullable referans tipleri: uyarıları okumak ve ciddiye almak

`<Nullable>enable</Nullable>` (yeni projelerde varsayılan; bu repoda `ConsoleApp.csproj`'ta) ile derleyici **null akışını takip eder**:

```csharp
string  name = "Alice";    // must never be null
string? note = null;       // may be null → check before use
note.Length;               // CS8602: possible null dereference
```

Bu **derleme zamanı** ipucudur; çalışma zamanında koruma eklemez (`!` ile susturulabilir, reflection/serileştirme ile delinebilir). Ama `NullReferenceException`'ın büyük kısmını **çalışmadan** yakalar.

| Uyarı | Anlamı | Tipik düzeltme |
|-------|--------|----------------|
| `CS8600` | null literal/olası null değeri non-nullable'a atama | Tipi `string?` yap ya da gerçek değer ver |
| `CS8602` | Olası null referansın üyesine erişim | `?.`, `is not null` kontrolü, erken return |
| `CS8603` | Olası null döndürme | Dönüş tipini `T?` yap ya da null olmayan değer döndür |
| `CS8604` | Olası null argüman | Çağırmadan önce kontrol et / parametreyi `T?` yap |
| `CS8618` | Non-nullable alan/özellik constructor sonunda başlatılmadı | Başlat, `required` kullan, ya da `T?` yap |

**Null-forgiving operatörü `!`** ("eminim null değil") uyarıyı susturur, null'ı kaldırmaz ([Egzersiz 2](#egzersiz-çözümleri-spoiler)). Yalnızca *kanıtlayabildiğiniz* yerde kullanın. Ek araçlar: `is not null`, `??`, `??=`, `?.`, `ArgumentNullException.ThrowIfNull(x)`, `[NotNullWhen(true)]` gibi nullable öznitelikleri (`TryGetValue(…, out var v)` imzası bu yüzden doğru çalışır).

### 3.7 Pratik

Kod `ConsoleApp` projesindedir (`ConsoleApp` klasöründen çalıştırın):

```bash
# 1) Debugger exercises — 9 deliberately buggy programs (Exercises/)
dotnet run -- J1.5                # list
dotnet run -- J1.5 6              # exercise 6
dotnet run -- J1.5 6 --hint       # hint · solutions: "Exercise solutions" below (try it yourself first!)
# In the IDE: make ConsoleApp the startup project, put "J1.5 6" in the Debug arguments → breakpoint in Exercises/ → F5
# Exercise 9 (Debug/Release): dotnet run -c Release -- J1.5 9

# 2) Analyzer + nullable (AnalyzersNullable/) — this folder DELIBERATELY produces warnings
dotnet build                                    # 15 warnings (build succeeds): CS0414, CS86xx, IDE0005/0044/0051/1006, CA1861
dotnet build -p:TreatWarningsAsErrors=true      # same code → build FAILS
dotnet build -p:WarningsAsErrors=nullable       # only nullable warnings become errors
dotnet format whitespace --verify-no-changes    # formatting gate: fails (exit code ≠ 0)
dotnet format                                   # fix (MODIFIES the files → then undo with git checkout -- .)
dotnet run -- J1.5 analyzers                     # run-time behavior of the warning-laden code (GetUserName(2) → null)

# 3) Hot reload
dotnet watch -- J1.5 3                           # change a file under Exercises/ and save
```

| Klasör | Amaç |
|--------|------|
| [`Exercises/`](Exercises/) | 9 egzersiz: döngü hatası, NRE, koleksiyon değişimi, closure, sayısal tuzaklar, **conditional breakpoint**, unutulan await, paylaşılan referans, Debug/Release farkı |
| [`AnalyzersNullable/`](AnalyzersNullable/) | `CS86xx` nullable uyarıları, `CA`/`IDE` analyzer uyarıları, biçim ihlalleri; klasöre özel [`.editorconfig`](AnalyzersNullable/.editorconfig) ile (`ConsoleApp.csproj` → `EnforceCodeStyleInBuild`) |

> `ConsoleApp` projesinde `AnalyzersNullable/` uyarıları **her derlemede** görünür; bunlar bilerek bırakılmıştır.

#### Egzersiz çözümleri (spoiler!)

<details>
<summary>Önce egzersizleri debugger ile kendiniz çözmeye çalışın; sonra açın.</summary>

Her çözümde üç şey var: **kök neden**, **hangi debugger özelliğiyle bulunur**, **düzeltme**.

**1. Döngü: 1'den n'e toplam**
- **Kök neden:** `for (var i = 1; i < n; i++)` — `n` dahil edilmiyor (**off-by-one**). `SumUpTo(1)` döngüye hiç girmez → 0.
- **Debugger ile:** `sum += i` satırına breakpoint → F5/F10 ile ilerleyin; **Watch**'a `i` ve `sum` ekleyin. Son iterasyonda `i == n - 1` olduğunu görürsünüz.
- **Düzeltme:** `i <= n`. (Kalıcı çözüm: sınır değerleri — `n = 0, 1, 10` — için birim testi yazmak, → J10.1.)

**2. NullReferenceException zinciri**
- **Kök neden:** Carol'ın `Address`'i `null`; `CityOf` içinde `c.Address!.City` — `!` operatörü derleyiciye "eminim, null değil" der ve uyarıyı susturur, ama null'ı ortadan kaldırmaz.
- **Debugger ile:** **Exception Settings** (Ctrl+Alt+E) → `System.NullReferenceException`'ı işaretleyin; istisna fırlatıldığı anda durur. **Locals**'ta `c.Address == null` görülür; **Call Stack**'te `ShippingLabel → CityOf` zincirini izlersiniz.
- **Düzeltme:** `c.Address?.City.ToUpperInvariant() ?? "no address"` — `!` yerine gerçek null kontrolü. Genel kural: `!`'i, null olamayacağını kanıtlayabildiğiniz yerlerde kullanın; "uyarıyı sustur" aracı değildir.

**3. foreach sırasında koleksiyonu değiştirmek**
- **Kök neden:** `List<T>` her değişiklikte iç `_version` sayacını artırır; enumerator `MoveNext`'te bunu kontrol eder ve değişmişse `InvalidOperationException` fırlatır. Belirti `foreach`'te çıkar, ama kök neden döngü gövdesindeki `Remove`'dur.
- **Debugger ile:** Exception Settings'te `InvalidOperationException`'ı işaretleyin; **Call Stack**'te `List<T>.Enumerator.MoveNext` görünür; bir önceki çerçeveye geçince hangi `Remove` çağrısından sonra olduğu anlaşılır.
- **Düzeltme:** `orders.RemoveAll(o => o.IsCancelled);` ya da `foreach (var o in orders.ToList())` (kopya üzerinde dön).

**4. Döngüde yakalanan değişken (closure)**
- **Kök neden:** `for` döngüsünde `i` **tek bir değişkendir**; lambda'lar değeri değil **değişkenin kendisini** yakalar. Lambda'lar döngü bittikten sonra çalışınca `i == 3` olduğu için hepsi `30` döner. (`foreach` değişkeni C# 5'ten beri her iterasyonda yenidir; `for` değişkeni değildir.)
- **Debugger ile:** Lambda içine breakpoint → **Locals**'ta `this` altında derleyicinin ürettiği closure sınıfını (`<>c__DisplayClass…`) ve tek bir `i` alanını görürsünüz.
- **Düzeltme:** Döngü içinde kopya al: `var copy = i; actions.Add(() => copy * 10);`.

**5. Tamsayı bölme ve kayan nokta tuzakları**
- **Kök neden:** (a) `percent / 100` → `int / int` = `0` (tamsayı bölme), indirim hiç uygulanmaz. (b) `values.Sum() / values.Length` → `int / int`; sonuç `double`'a sonradan çevrilse bile kesir kaybolmuştur. (c) `0.1 + 0.2 == 0.3` → ikili kayan noktada tam gösterilemeyen değerler; eşitlik karşılaştırması güvenilmez.
- **Debugger ile:** **Immediate window**: `percent / 100` → `0`, `(decimal)percent / 100` → `0.10`, `0.1 + 0.2` → `0.30000000000000004`. İfadeleri çalışırken deneyip doğrulayın.
- **Düzeltme:** `price - price * percent / 100m` (ya da `decimal` tipine çevirin); `values.Average()` / `(double)values.Sum() / values.Length`; para için `decimal`, kayan nokta karşılaştırmasında tolerans (`Math.Abs(a - b) < 1e-9`) kullanın (→ J2.1, J2.2).

**6. 10.000 kayıttan birindeki bozuk veri**
- **Kök neden:** `Id == 7345` olan kaydın `Quantity` değeri `-3` (veri kaynağı hatası).
- **Debugger ile:** Döngüdeki `actual += …` satırına **koşullu breakpoint**: `order.Quantity < 0`. 10.000 iterasyon yerine tam o kayıtta durur. Alternatif: **tracepoint** (sağ tık → "Log a message": `{order.Id}: {order.Quantity}`) — durmadan Output'a yazar, `grep` benzeri filtreyle bakılır. Üçüncü yol: **Hit count** (örn. "7345. iterasyonda dur").
- **Düzeltme:** Hata kaynağı **veri girişidir**: yükleme/doğrulama katmanında `Quantity > 0` kontrolü (veri sınırında doğrula) ve bu kontrolü kapsayan bir birim testi.

**7. Unutulan await**
- **Kök neden:** `SaveAsync` çağrılır ama beklenmez (`await` yok); `Count` kontrolü kayıt işlemi bitmeden yapılır. Sonucu bir değişkene atamak derleyici uyarısını (CS4014) susturur — yani bu hatanın bu biçimi derleyiciden kaçar.
- **Debugger ile:** `Report` satırında **Watch**'a `task.Status` ekleyin → `WaitingForActivation`. **Parallel Tasks / Tasks** penceresi (VS: Debug → Windows) bekleyen task'ı gösterir. F10 ile `await` satırını geçerken kontrolün başka bir yere atlayıp geri döndüğünü gözlemleyin.
- **Düzeltme:** `await repo.SaveAsync("record-1");`. Önleme: `CS4014`'ü uyarı/hata yapmak (`.editorconfig`'te yapıldı), `Task` döndüren çağrıların sonucunu asla yok saymamak (→ J4).

**8. Aynı nesneyi paylaşan iki "farklı" kullanıcı**
- **Kök neden:** `"guest"` rolü için her çağrıda **aynı** `GuestDefaults` listesi döner; `aliceGuest.Add("write")` paylaşılan listeyi değiştirir, Bob da etkilenir (referans tipi + paylaşılan değişken durum).
- **Debugger ile:** **Watch** penceresinde `aliceGuest` → sağ tık → **Make Object ID** → `{1#}`; `bobGuest` için de `{1#}` gelir → aynı nesne.
- **Düzeltme:** Her çağrıda yeni liste ya da değiştirilemez görünüm: `=> [.. GuestDefaults]` veya `IReadOnlyList<string>` döndürmek (tasarım olarak "dışarıya değiştirilebilir iç durum sızdırmamak").

**9. Debug'da doğru, Release'de farklı sonuç**
- **Kök neden:** `Debug.Assert(NextId() > 0)` — `Debug.*` çağrıları `[Conditional("DEBUG")]` ile işaretlidir; **Release derlemesinde çağrı ve argüman ifadeleri tamamen silinir**. İfadenin yan etkisi (`NextId()` sayacı artırır) yalnızca Debug'da gerçekleşir.
- **Debugger ile:** İki yapılandırmayı karşılaştırın: `dotnet run -- J1.5 9` ve `dotnet run -c Release -- J1.5 9`. IDE'de Release'de breakpoint'ler/Locals optimize edilmiş kodda eksik görünebilir.
- **Düzeltme:** Assert/log ifadelerinde **yan etki olmasın**: `var id = NextId(); Debug.Assert(id > 0);`. Prodüksiyonda gerekli doğrulamalar `Debug.Assert` değil gerçek kontrol/istisna olmalı.

</details>

---

## 4. Gerçek dünya senaryosu

**"Sadece 'Carol' siparişinde API 500 dönüyor, diğerleri çalışıyor."**

1. Log: `NullReferenceException at CityOf(...)` — ama hangi referans null? Satırda dört nokta var (`c.Address!.City.ToUpperInvariant()`).
2. Yerelde sorunu yeniden üretin; **Exception Settings**'te `NullReferenceException`'ı işaretleyin, F5. İstisna fırlatıldığı satırda durur; **Locals**: `c.Address == null`.
3. Kök neden: veri kaynağında adres alanı opsiyonel; kodda `!` ile susturulmuş. **Analyzer neden yakalamadı?** `!` operatörü ile uyarıyı bilerek susturduk. Ders: `!` bir borçtur.
4. Düzeltme + regresyon testi (→ J10.1) + `.editorconfig`/`WarningsAsErrors=nullable` ile benzer hataların derlemede yakalanması.

**Ek senaryo — "Yük altında binlerce istekten biri bozuk veriyle patlıyor":** Üretimde debugger bağlamak istemezsiniz; yerelde aynı veriyle koşullu breakpoint/tracepoint ile (Egzersiz 6) suçlu kaydı bulur, **gözlemlenebilirlik** (yapısal log, trace → M13) ile bunun üretimde de yakalanmasını sağlarsınız.

---

## 5. Mülakat perspektifi

Sık sorulanlar ([`interview-questions.md`](interview-questions.md)'de cevaplarıyla):
- Bir hatayı nasıl ayıklarsın? Debugger'ı hangi özellikleriyle kullanırsın?
- Conditional breakpoint / tracepoint ne zaman?
- First-chance exception nedir?
- `NullReferenceException` nedir, nullable reference types ne işe yarar?
- `!` operatörü ne yapar, neden tehlikeli?
- `TreatWarningsAsErrors`'ı açar mısın? Eski bir projede nasıl uygularsın?
- `.editorconfig` ve `dotnet format` ne işe yarar?
- Debug ile Release farkı?
- Hot Reload nedir?

**İpucu:** "Debug yaparım" yerine **yöntemi** anlatın: yeniden üret → hipotez → breakpoint/exception ayarı → gözlemle (Locals/Watch/Call Stack) → düzelt → test ekle. Debugger'ın "gözlem" aracı olduğunu söylemek olgunluk gösterir.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| Hata ayıklamak için kodu `Console.WriteLine` ile doldurup unutmak | Tracepoint (kodu değiştirmez) ya da kalıcı yapısal log |
| Sadece unhandled exception'da durmak | First-chance ayarı → istisna **fırlatıldığı** satırda durun |
| 10.000 iterasyonda F5-F5-F5… | Conditional breakpoint / hit count / tracepoint |
| Release'de bozulan davranışı sadece Debug'da denemek | İki yapılandırmayı da çalıştırın (yan etkili `Debug.Assert`, zamanlama) |
| Uyarıları görmezden gelmek ("derleniyor ya") | `TreatWarningsAsErrors` veya en azından `WarningsAsErrors=nullable` |
| Nullable uyarısını `!` ile geçiştirmek | Gerçek null kontrolü / model düzeltmesi; `!` yalnızca kanıtlı durumlarda |
| `catch (Exception) { }` ile hatayı yutmak | Debugger'da bile görünmez; loglayın veya yeniden fırlatın |
| `.editorconfig` olmadan IDE ayarlarına güvenmek | Kural repoda olmalı, kişinin makinesinde değil |
| `dotnet format`'ı büyük PR'ın içine karıştırmak | Biçim değişikliğini **ayrı commit/PR** yapın (review ve `git blame` temiz kalır) |
| `Debug.Assert` içine yan etkili ifade yazmak | Release'de silinir; önce değişkene alın, sonra assert edin |
| Hot Reload'ı üretimde/kalıcı çözüm saymak | Geliştirme aracıdır; "rude edit"te yeniden başlatma gerekir |
| Breakpoint koyup `async` kodda F10'un "atlamasına" şaşırmak | `await`te kontrol çağırana döner; Tasks/Parallel Stacks penceresi ve devam sonrası satırı izleyin |

---

## 7. Performans & trade-off

- **Debugger'ı durdurmak vs log:** Breakpoint tüm process'i (veya thread'i) durdurur — zamanlama/race hataları kaybolabilir ("Heisenbug"); bu tür hatalarda **tracepoint/log** veya trace daha az müdahalecidir.
- **Üretimde debugger yok:** Attach etmek süreci durdurur ve güvenlik riskidir. Üretimde teşhis: log, metrik, dağıtık trace (M13), dump analizi, `dotnet-counters`/`dotnet-trace` (S2).
- **Analyzer yükü:** `AnalysisMode=All` ve çok sayıda 3. parti analyzer derleme süresini ve uyarı gürültüsünü artırır; kademeli ilerleyin, gürültü kuralı kapatın (gerekçeyle), değerli kuralı `error` yapın.
- **`TreatWarningsAsErrors`:** Yeni projede ucuz ve etkili; eski kod tabanında "büyük patlama" yerine kural kural sıkılaştırma. Yeni SDK/analyzer sürümü yeni uyarı getirip build kırabilir — sürümleri sabitleyin (`global.json`, → J1.4).
- **Biçim kapısı (CI):** `dotnet format --verify-no-changes` hızlı ve ucuzdur; PR'larda stil tartışmasını ortadan kaldırır.
- **Hot Reload:** İterasyon hızını artırır ama tam durum yenilenmediği için bazen "eski kodla çalışıyormuş gibi" yanıltıcı davranış olabilir; şüphede tam yeniden başlatın.

---

## 8. Kaynaklar

- Visual Studio debugger — başlangıç: <https://learn.microsoft.com/visualstudio/debugger/>
- VS Code ile C# hata ayıklama: <https://code.visualstudio.com/docs/csharp/debugging>
- Rider debugger: <https://www.jetbrains.com/help/rider/Debugging_Code.html>
- Exception ayarları ve first-chance: <https://learn.microsoft.com/visualstudio/debugger/managing-exceptions-with-the-debugger>
- `dotnet watch` ve Hot Reload: <https://learn.microsoft.com/aspnet/core/tutorials/dotnet-watch> · <https://learn.microsoft.com/visualstudio/debugger/hot-reload>
- Kod analizi genel bakış (CA/IDE kuralları): <https://learn.microsoft.com/dotnet/fundamentals/code-analysis/overview>
- `.editorconfig` ile .NET kod stili kuralları: <https://learn.microsoft.com/dotnet/fundamentals/code-analysis/code-style-rule-options>
- `dotnet format` aracı: <https://learn.microsoft.com/dotnet/core/tools/dotnet-format>
- Nullable referans tipleri: <https://learn.microsoft.com/dotnet/csharp/nullable-references>
- Derleyici seçenekleri — uyarıları yönetme (`TreatWarningsAsErrors`, `WarningsAsErrors`): <https://learn.microsoft.com/dotnet/csharp/language-reference/compiler-options/errors-warnings>
