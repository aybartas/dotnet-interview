# J5.2 `bin/` ve `obj/`

> 📁 `ConsoleApp/01-junior/05-build-run-basics/02-bin-obj/` · Önkoşul: J5.1 · Sorular: [`interview-questions.md`](interview-questions.md)

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

Her .NET projesi derlenince iki klasör oluşur: **`obj/`** (MSBuild'in çalışma masası: restore sonucu ve ara çıktılar) ve **`bin/`** (dağıtılabilir **son çıktı**). İkisi de kaynaktan **üretilebilir** dosyalardır. Konunun kodu [`Sections/`](Sections/) altında 4 bölümdür (`dotnet run -- J5.2 <numara>`); program **kendi** `bin/` ve `obj/` klasörünü listeler ve her dosyayı açıklar.

---

## 2. Neden?

- "`bin` ve `obj` klasörlerinde ne var?" klasik bir mülakat sorusudur.
- Dosyaları tanımak teşhisi hızlandırır: "Neden eski `.dll` çalışıyor?" (→ `dotnet clean`), "Ayar dosyası neden bulunamıyor?" (→ `CopyToOutputDirectory`), "Hangi runtime gerekiyor?" (→ `runtimeconfig.json`).
- Hangi dosyanın commit'lenip hangisinin `.gitignore`'a gireceğini bilmek temel bir hijyendir (→ J1.1).

---

## 3. Nasıl?

### 3.1 🛠 `bin/Debug/net10.0/` içeriği ([`Section01`](Sections/Section01_BinListing.cs))

| Dosya | Ne işe yarar |
|-------|--------------|
| `ConsoleApp.dll` | ★ **Senin kodun** — IL + metadata |
| `ConsoleApp` (Windows'ta `.exe`) | **apphost** — `.dll`'i başlatan native launcher |
| `ConsoleApp.pdb` | Debug sembolleri (IL ↔ kaynak satır eşlemesi; breakpoint ve stack trace'teki satır numaraları bunun sayesinde) |
| `ConsoleApp.deps.json` | Bağımlılık manifesti (`hostpolicy` okur) |
| `ConsoleApp.runtimeconfig.json` | Hangi runtime sürümü, roll-forward, GC/ayarlar (apphost/`hostfxr` okur) |
| `*.dll` (diğerleri) | NuGet/proje bağımlılıkları |
| `appsettings.json` | `CopyToOutputDirectory` ile kopyalanan içerik dosyaları |

Programın çıktısı bu tabloyu kendi klasörüne uygular (boyutlarıyla). Bu projede NuGet paketi olmadığı için başka `.dll` yoktur; paket ekleyince bağımlılık `.dll`'leri de `bin/`'e kopyalanır.

### 3.2 🛠 `runtimeconfig.json` ve `deps.json` ([`Section02`](Sections/Section02_RuntimeConfig.cs))

```json
{
  "runtimeOptions": {
    "tfm": "net10.0",
    "framework": { "name": "Microsoft.NETCore.App", "version": "10.0.0" },
    "configProperties": { "System.Globalization.Invariant": true, "...": "..." }
  }
}
```

- **Framework-dependent** uygulama: runtime uygulamayla **gelmez**; makinede kurulu `Microsoft.NETCore.App 10.0.0` (veya uyumlu roll-forward) aranır. Self-contained'da burada `includedFrameworks` görürdünüz (→ J5.3, M2.5).
- `configProperties`, csproj'taki ayarlardan gelir (örn. csproj'taki `InvariantGlobalization=false` → `System.Globalization.Invariant: false`).
- `deps.json`: `runtimeTarget` ve `libraries` (projenin kendisi + NuGet paketleri). Framework assembly'leri burada listelenmez.
- Eksik/uyumsuz runtime → *"You must install or update .NET to run this application"* / *"Framework 'Microsoft.NETCore.App' was not found"* (→ J1.4).

### 3.3 `obj/` = ara çıktılar ([`Section03`](Sections/Section03_ObjFolder.cs))

| Dosya | Ne? |
|-------|-----|
| `obj/project.assets.json` | ★ **Restore sonucu**: çözülen paketler ve bağımlılık grafı (derleyici ve IDE okur) |
| `obj/*.csproj.nuget.g.props/.targets` | NuGet'in ürettiği MSBuild props/targets (paketlerin eklediği ayarlar) |
| `obj/project.nuget.cache` | Restore önbelleği (değişmediyse restore atlanır) |
| `obj/Debug/net10.0/*.AssemblyInfo.cs` | Derleyiciye giren **üretilmiş kaynak**: assembly öznitelikleri (sürüm, yapılandırma) |
| `obj/Debug/net10.0/*.GlobalUsings.g.cs` | **ImplicitUsings'in gerçek yüzü**: `global using System; … System.Linq; … System.Threading.Tasks;` |
| `obj/Debug/net10.0/*.dll`, `*.pdb` | Derleyicinin yazdığı **ara** assembly; MSBuild sonra `bin/`'e **kopyalar** |
| `obj/Debug/net10.0/apphost` | Projeye özel apphost kopyası |
| `*.FileListAbsolute.txt` | `dotnet clean`'in neyi sileceğini bilmesi için dosya listesi |

Demo `obj/`'daki `.dll` ile `bin/`'deki `.dll`'in **aynı** olduğunu (hash) gösterir: derleyici önce `obj/`'a yazar, MSBuild `bin/`'e kopyalar.

### 3.4 `CopyToOutputDirectory`, `.gitignore`, `dotnet clean` ([`Section04`](Sections/Section04_CopyAndClean.cs))

```xml
<None Include="02-bin-obj/appsettings.json" Link="appsettings.json" CopyToOutputDirectory="PreserveNewest" />
```

| Değer | Anlamı |
|-------|--------|
| `Never` | Kopyalanmaz (varsayılan) |
| `PreserveNewest` | Kaynak daha yeniyse kopyalar (genelde doğrusu) |
| `Always` | Her derlemede kopyalar |

- Web SDK projelerinde `appsettings.json` bunu otomatik alır; kendi içerik dosyanız (şablon, sertifika, SQL betiği) için bu satırı **siz** yazarsınız. Unutulursa çalışma zamanında "dosya bulunamadı" alırsınız.
- **Neden ikisi de `.gitignore`'da?** Üretilebilir çıktıdır; depoya girerse gürültü, merge çakışması, makineye özel yollar ve şişme yaratır. Standart .NET `.gitignore`: `bin/`, `obj/` (+ `.vs/`, `*.user`).
- **`dotnet clean`:** `bin/<config>/` ve `obj/`'daki üretilmiş dosyaları siler (`project.assets.json`'ı silmez). Ne zaman: eski `.dll` çalışıyormuş gibi şüphe, paket/SDK değişimi sonrası tuhaf hatalar, boyut temizliği. Kökten temizlik: `bin/` ve `obj/` klasörlerini silip yeniden `dotnet build`.

---

## 4. Gerçek dünya senaryosu

**"Kodu değiştirdim ama eski davranış çalışıyor."** IDE başka bir yapılandırmanın (Release) çıktısını çalıştırıyor ya da `bin/` eski `.dll` tutuyor. `bin` ve `obj`'u silip yeniden derlemek (veya `dotnet clean`) sorunu çözdü; kök neden çalıştırılan `bin/<config>` klasörünün farklı olmasıydı.

**"CI'da `appsettings.Production.json` bulunamıyor."** Dosya projeye eklenmiş ama `CopyToOutputDirectory` verilmemişti; geliştirici makinesinde kaynak klasörden okunduğu için fark edilmemişti. `PreserveNewest` eklenince `bin/`'e (ve publish çıktısına) kopyalandı.

**"Repo 800 MB."** Biri `bin/` ve `obj/`'u commit'lemişti. `.gitignore` düzeltildi, geçmiş temizlendi (→ J1.1).

---

## 5. Mülakat perspektifi

Sık sorulanlar: **`bin` ve `obj` klasörlerinde ne var?** ⭐ · `.dll` ile `.pdb`, `deps.json` ve `runtimeconfig.json` ne işe yarar? · Neden `.gitignore`'a girerler? · `dotnet clean` ne zaman gerekir? · `appsettings.json` çıktıya nasıl kopyalanır?

**İpucu:** "`obj` ara çıktılar ve restore sonucu (MSBuild'in çalışma masası), `bin` son çıktı" cümlesiyle başlayın, sonra tabloyu sayın: `dll` = kodum, `pdb` = debug sembolleri, `deps.json` = bağımlılıklar, `runtimeconfig.json` = runtime ayarı.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| `bin/` ve `obj/` klasörlerini commit'lemek | `.gitignore` (standart .NET `.gitignore`) |
| `bin/` içindeki dosyayı elle düzenlemek | Bir sonraki derlemede ezilir; kaynağı düzenleyin |
| İçerik dosyasına `CopyToOutputDirectory` vermemek | `PreserveNewest` (veya Web SDK varsayılanı) |
| `obj/`'u silince restore'un da gittiğini unutmak | Normal: bir sonraki `build` restore'u yeniden yapar |
| Yanlış yapılandırma klasöründen çalıştırmak (Debug ↔ Release) | Çalışan `bin/<config>/<tfm>` yolunu doğrulayın |
| `.pdb`'yi yayın paketine koymamak/kaybetmek | Üretim hatalarında satır numarası için saklayın (symbol server) |
| `runtimeconfig.json`'ı elle düzenleyip projeyi eski sanmak | csproj ayarını değiştirin; dosya üretilir |

---

## 7. Performans & trade-off

- **Artımlı derleme:** `obj/` önbellek dosyaları (`*.cache`, `FileListAbsolute.txt`) sayesinde değişmeyen projeler yeniden derlenmez; `obj/` silmek derlemeyi yavaşlatır ama temiz bir başlangıç verir.
- **Boyut:** `bin/` büyüyebilir (çoklu yapılandırma/TFM); CI'da önbelleğe `~/.nuget/packages`'ı alın, `bin/obj`'u değil.
- **`.pdb`:** Küçük maliyetle üretimde okunabilir stack trace (satır numarası) verir; gömülü/ayrı `.pdb` ve symbol server seçenekleri → M2.
- **Temizlik vs hız:** "Sorun olunca sil" refleksi güvenlidir ama alışkanlık haline getirmeyin; kök nedeni (yanlış yapılandırma, eski çıktı) anlayın.

---

## 8. Kaynaklar

- .NET SDK build çıktıları: <https://learn.microsoft.com/dotnet/core/tools/dotnet-build>
- `runtimeconfig.json` ayarları: <https://learn.microsoft.com/dotnet/core/runtime-config/>
- `deps.json` / bağımlılık yükleme: <https://learn.microsoft.com/dotnet/core/dependency-loading/understanding-assemblyloadcontext>
- MSBuild öğeleri (`None`, `CopyToOutputDirectory`): <https://learn.microsoft.com/visualstudio/msbuild/common-msbuild-project-items>
- `dotnet clean`: <https://learn.microsoft.com/dotnet/core/tools/dotnet-clean>
- .NET için `.gitignore`: <https://github.com/github/gitignore/blob/main/VisualStudio.gitignore>
- Implicit using'ler: <https://learn.microsoft.com/dotnet/core/project-sdk/msbuild-props#implicitusings>
