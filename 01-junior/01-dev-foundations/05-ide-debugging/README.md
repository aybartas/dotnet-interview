# J1.5 IDE, Debugging & Kod Kalitesi Araçları

> 📁 `01-junior/01-dev-foundations/05-ide-debugging/` · Önkoşul: J1.4 · Pratik: [`src/`](src/) · Çözümler (spoiler): [`SOLUTIONS.md`](SOLUTIONS.md) · Sorular: [`interview-questions.md`](interview-questions.md)

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
| **Conditional** | Binlerce iterasyondan yalnızca *belirli* durumda durmak | `order.Quantity < 0` ([Egzersiz 6](SOLUTIONS.md)) |
| **Hit count** | N'inci geçişte durmak | "7345'inci iterasyonda" |
| **Tracepoint (log point)** | **Durmadan** değer loglamak; kodu değiştirmeden, `Console.WriteLine` eklemeden | Mesaj: `{order.Id}: {order.Quantity}` |
| **Function / Data breakpoint** | Bir metoda girildiğinde / bir alan değiştiğinde durmak | (IDE'ye göre) |

**İnceleme pencereleri:**

| Pencere | İşlevi |
|---------|--------|
| **Locals / Autos** | Mevcut kapsamdaki değişkenler ve değerleri |
| **Watch** | Seçtiğiniz ifadeleri sürekli izler (`i`, `order.Total`, `list.Count`) |
| **Immediate Window** | O an **ifade çalıştırın/değerlendirin**: `percent / 100`, `list.Where(x => x > 1).Count()`; hatta değişkene atama yapıp denemek ([Egzersiz 5](SOLUTIONS.md)) |
| **Call Stack** | "Buraya nasıl geldim?" — çağrı zinciri; üstteki çerçeveyi seçip o noktadaki yerelleri de görebilirsiniz |
| **Make Object ID** | Referansa kalıcı etiket (`{1#}`): iki değişken **aynı nesneyi** mi gösteriyor? ([Egzersiz 8](SOLUTIONS.md)) |
| **Threads / Tasks / Parallel Stacks** | Eşzamanlı kod, bekleyen `Task`'lar, deadlock teşhisi (→ J4, M4) |
| **Breakpoints penceresi** | Hepsini tek yerden aç/kapat, koşulları düzenle |

**Debug ≠ Release:** Debug yapılandırmasında optimizasyon kapalıdır ve ek hata ayıklama bilgisi vardır; Release'de derleyici/JIT kodu yeniden düzenler, değişkenler "optimized away" olur, `Debug.*` ve `[Conditional("DEBUG")]` çağrıları **silinir** ([Egzersiz 9](SOLUTIONS.md)). "Debug'da çalışıyor, Release'de çalışmıyor" gerçek bir sorun sınıfıdır — çoğu zaman yan etkili assert, zamanlama/race ya da optimizasyona duyarlı kod.

### 3.2 Exception ayarları: "first-chance exception"da durmak ⭐

Bir istisna iki aşamada ele alınır:

- **First-chance exception:** İstisna **fırlatıldığı anda**, henüz `catch` aranmadan. Debugger buraya "ilk şans" olarak bildirim alır.
- **Second-chance (unhandled):** Hiçbir `catch` yakalamadı → process çöker.

Varsayılan olarak debugger yalnızca **işlenmeyen** (unhandled) istisnada durur — o noktada çoğu zaman stack zaten çözülmüş ve bağlam kaybolmuştur. Hatanın **fırlatıldığı satırı** görmek için: **Exception Settings** (VS: Ctrl+Alt+E; VS Code: Breakpoints → "Raised Exceptions") → ilgili istisna türünü ( `NullReferenceException`, `InvalidOperationException`…) işaretleyin; debugger istisna **fırlatılırken** durur, **Call Stack**'te zinciri ve **Locals**'ta durumu görürsünüz.

İpucu: **Just My Code** açıkken (varsayılan) framework içi kodu atlarsınız; bir kütüphanenin neden istisna fırlattığını görmek için kapatın. Yakalanıp yutulan (`catch { }`) istisnaları bulmak için de first-chance ayarı bire birdir.

### 3.3 `dotnet watch` ve Hot Reload

```bash
cd src/watch-demo
dotnet watch                # çalıştır + dosya değişikliğini izle
```

- **Hot Reload:** Çalışan uygulamada **kod değişikliğini yeniden başlatmadan** uygular (durum/bellek korunur; denemede sayaç `#13 → #14` devam eder). Metot gövdesi değişiklikleri sorunsuz uygulanır.
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
dotnet_diagnostic.CS8602.severity = error     # tek tek
dotnet_analyzer_diagnostic.category-Style.severity = suggestion   # kategori bazında
csharp_style_namespace_declarations = file_scoped:suggestion      # stil tercihi + önem derecesi
```

Örnek tam dosya: [`src/.editorconfig`](src/.editorconfig) — biçim (girinti, satır sonu), `using`/`var`/namespace stili, **adlandırma kuralları** (interface `I…`, private alan `_camelCase`) ve analyzer önem dereceleri. `.editorconfig` **repoyla birlikte commit'lenir**; IDE'ler, `dotnet format` ve derleyici aynı dosyayı okur → "benim makinemde farklı biçimleniyor" biter.

**Derlemede zorlama:** IDE'deki `IDE####` stil kuralları varsayılanda yalnızca editörde görünür. Derlemede (ve CI'da) de çalışması için:

```xml
<EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
<AnalysisMode>Default</AnalysisMode>   <!-- Default < Recommended < All: açık CA kuralı sayısı artar -->
```

### 3.5 `dotnet format` ve `TreatWarningsAsErrors`

```bash
dotnet format                         # biçim + stil + analyzer düzeltmelerini UYGULAR (dosyaları değiştirir)
dotnet format whitespace              # yalnızca boşluk/girinti/satır sonu
dotnet format style                   # IDE stil kuralları
dotnet format analyzers --severity warn
dotnet format --verify-no-changes     # DEĞİŞTİRMEZ; değişiklik gerekiyorsa sıfırdan farklı çıkış kodu ile BAŞARISIZ olur → CI kapısı
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

`<Nullable>enable</Nullable>` (yeni projelerde varsayılan; bu repoda [`Directory.Build.props`](../../../Directory.Build.props)'ta) ile derleyici **null akışını takip eder**:

```csharp
string  name = "Ayşe";     // asla null olmamalı
string? note = null;       // null olabilir → kullanmadan önce kontrol et
note.Length;               // CS8602: olası null başvuru
```

Bu **derleme zamanı** ipucudur; çalışma zamanında koruma eklemez (`!` ile susturulabilir, reflection/serileştirme ile delinebilir). Ama `NullReferenceException`'ın büyük kısmını **çalışmadan** yakalar.

| Uyarı | Anlamı | Tipik düzeltme |
|-------|--------|----------------|
| `CS8600` | null literal/olası null değeri non-nullable'a atama | Tipi `string?` yap ya da gerçek değer ver |
| `CS8602` | Olası null referansın üyesine erişim | `?.`, `is not null` kontrolü, erken return |
| `CS8603` | Olası null döndürme | Dönüş tipini `T?` yap ya da null olmayan değer döndür |
| `CS8604` | Olası null argüman | Çağırmadan önce kontrol et / parametreyi `T?` yap |
| `CS8618` | Non-nullable alan/özellik constructor sonunda başlatılmadı | Başlat, `required` kullan, ya da `T?` yap |

**Null-forgiving operatörü `!`** ("eminim null değil") uyarıyı susturur, null'ı kaldırmaz ([Egzersiz 2](SOLUTIONS.md)). Yalnızca *kanıtlayabildiğiniz* yerde kullanın. Ek araçlar: `is not null`, `??`, `??=`, `?.`, `ArgumentNullException.ThrowIfNull(x)`, `[NotNullWhen(true)]` gibi nullable öznitelikleri (`TryGetValue(…, out var v)` imzası bu yüzden doğru çalışır).

### 3.7 Pratik

```bash
cd src

# 1) Debugger egzersizleri — 9 bilerek hatalı program
cd debugging-exercises
dotnet run                  # liste
dotnet run -- 6             # 6. egzersiz
dotnet run -- 6 --hint      # ipucu · çözümler: ../../SOLUTIONS.md
# IDE'de: projeyi aç → Program.cs/Exercises'a breakpoint → F5

# 2) Analyzer + nullable
cd ../analyzers-nullable
dotnet build                                    # 18 uyarı (derleme başarılı)
dotnet build -p:TreatWarningsAsErrors=true      # aynı kod → derleme BAŞARISIZ
dotnet build -p:WarningsAsErrors=nullable       # yalnızca nullable uyarıları hata
dotnet format whitespace --verify-no-changes    # biçim kapısı: başarısız (çıkış kodu ≠ 0)
dotnet format                                   # düzelt (dosyaları DEĞİŞTİRİR → sonra git checkout -- . ile geri alın)

# 3) Hot reload
cd ../watch-demo && dotnet watch                # Greeter.cs'i değiştirip kaydedin
```

| Proje | Amaç |
|-------|------|
| [`debugging-exercises/`](src/debugging-exercises/) | 9 egzersiz: döngü hatası, NRE, koleksiyon değişimi, closure, sayısal tuzaklar, **conditional breakpoint**, unutulan await, paylaşılan referans, Debug/Release farkı |
| [`analyzers-nullable/`](src/analyzers-nullable/) | `CS86xx` nullable uyarıları, `CA`/`IDE` analyzer uyarıları, biçim ihlalleri; [`.editorconfig`](src/.editorconfig) ile |
| [`watch-demo/`](src/watch-demo/) | `dotnet watch` + Hot Reload denemesi |
| [`.vscode/`](src/.vscode/) | VS Code için `launch.json` / `tasks.json` örnekleri |

---

## 4. Gerçek dünya senaryosu

**"Sadece 'Zeynep' siparişinde API 500 dönüyor, diğerleri çalışıyor."**

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
