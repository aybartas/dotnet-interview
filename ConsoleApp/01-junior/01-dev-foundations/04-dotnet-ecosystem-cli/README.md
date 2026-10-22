# J1.4 .NET Ekosistemi & CLI

> 📁 `ConsoleApp/01-junior/01-dev-foundations/04-dotnet-ecosystem-cli/` · Önkoşul: — · Sorular: [`interview-questions.md`](interview-questions.md)

Hedef platform: **.NET 10 (LTS) / C# 14**. .NET 11 / C# 15'e ait olanlar **(.NET 11)** diye işaretlenir.

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

**.NET**; C#, F# ve VB gibi dillerle uygulama geliştirmek için Microsoft'un açık kaynaklı, platformlar arası (Windows, Linux, macOS) geliştirme platformudur. Üç ana parçadan oluşur:

| Parça | Ne? |
|-------|-----|
| **Diller + derleyici** | C# (Roslyn derleyicisi), F#, VB |
| **Runtime (CLR) + base class library** | Derlenmiş IL kodunu çalıştıran sanal makine (JIT, GC) ve `System.*` kütüphaneleri |
| **Araçlar** | `dotnet` CLI, MSBuild, NuGet, şablonlar, analyzer'lar |

Derleme akışı (derin hali → M2, S1):

```
.cs  ──Roslyn──▶  .dll/.exe (IL + metadata, platformdan bağımsız)  ──CLR (JIT)──▶  makine kodu
```

### 1.1 Tarihçe

| Dönem | Ürün | Not |
|-------|------|-----|
| 2002 | **.NET Framework** | Sadece Windows, kapalı kaynak, makine genelinde kurulu. Bugün bakım modunda (yeni özellik yok); eski kurumsal uygulamalar yaşıyor |
| 2016 | **.NET Core** | Sıfırdan yazıldı: açık kaynak, **cross-platform**, modüler, yüksek performans, side-by-side kurulum, container dostu |
| 2020 | **.NET 5** | Core ve Framework'ün ayrımı kaldırıldı: tek, birleşik "**.NET**" (isimden "Core" düştü, sürüm 5'ten devam) |
| 2021 → | **.NET 6, 7, 8, 9, 10…** | Her yıl Kasım'da yeni sürüm |

**.NET Standard**: Framework, Core ve Xamarin arasında kod paylaşmak için **API sözleşmesiydi** (`netstandard2.0` = "bu API kümesini sunan her runtime'da çalışır"). .NET 5+ tek platform olunca ihtiyaç kalmadı; yeni kütüphaneler doğrudan `net8.0`/`net10.0` (veya geriye uyumluluk için çoklu hedef) hedefler. `netstandard2.0` yalnızca hâlâ Framework'e de hizmet etmesi gereken kütüphanelerde anlamlıdır.

### 1.2 SDK vs Runtime vs Target Framework Moniker ⭐

| Kavram | Ne? | Kimde bulunur? |
|--------|-----|----------------|
| **SDK** | Derlemek/geliştirmek için her şey: `dotnet` CLI, Roslyn, MSBuild, NuGet, şablonlar **+ bir runtime** | Geliştirici makinesi, CI/build ajanı, Docker `sdk` imajı |
| **Runtime** | Derlenmiş uygulamayı **çalıştırmak** için gereken minimum: `Microsoft.NETCore.App` (base runtime), `Microsoft.AspNetCore.App` (web) | Sunucu/production, Docker `aspnet`/`runtime` imajı |
| **TFM** (Target Framework Moniker) | Uygulamanın/kütüphanenin **hangi API setini hedeflediğini** söyleyen etiket: `net10.0`, `net10.0-windows`, `netstandard2.0`, `net48` | `.csproj` içindeki `<TargetFramework>` |

```xml
<TargetFramework>net10.0</TargetFramework>                          <!-- tek hedef -->
<TargetFrameworks>net8.0;net10.0</TargetFrameworks>                 <!-- çoklu hedef (kütüphane yazarları) -->
<!-- net10.0-windows → Windows'a özel API'ler (WPF, WinForms, registry); sadece Windows'ta çalışır -->
```

- **SDK ≠ hedef:** .NET 10 SDK ile `net8.0` hedefleyen proje derleyebilirsiniz; **çalıştırmak** için o hedefin runtime'ı (8.x) gerekir. SDK her zaman eski runtime'ları hedefleyebilir (geriye uyumlu), tersi olmaz.
- **Roll-forward:** Uygulama varsayılan olarak aynı major sürümün en yeni patch'ine gider (8.0.1 → 8.0.11), ama **yeni bir major'a atlamaz** (`net8.0` uygulaması yalnızca .NET 9 yüklü makinede çalışmaz). `RollForward=Major` ile gevşetilebilir.
- **"Makinemde çalışıyor" nedenlerinden biri:** Production sunucuda hedef runtime kurulu değil → `You must install or update .NET to run this application`. Çözüm: runtime'ı kurmak, **framework-dependent** yerine **self-contained** yayınlamak ya da container kullanmak (→ J5, J12).

### 1.3 Sürüm politikası: LTS vs STS

- Her yıl **Kasım**'da yeni major sürüm çıkar.
- **Çift** numaralılar **LTS** (Long Term Support, **3 yıl**), **tek** numaralılar **STS** (Standard Term Support, **24 ay**).
- **.NET 10 = LTS** (Kasım 2025). **.NET 11 = STS** (Kasım 2026'da bekleniyor).
- Üretim için kural: **LTS'te kal**, STS'yi yeni özellikleri erken denemek ve bir sonraki LTS'e hazırlanmak için kullan. Destek tarihleri değişebilir; güncel tablo: <https://dotnet.microsoft.com/platform/support/policy/dotnet-core>

---

## 2. Neden?

- **Her gün kullanılan araç:** `dotnet new / build / run / test / add package` komutlarını bilmeyen geliştirici IDE olmadan çalışamaz; CI/CD ve Docker'da zaten IDE yoktur — her şey CLI üzerinden döner (→ M14).
- **Doğru hata teşhisi:** "SDK mı runtime mı eksik?", "hangi TFM'i hedefliyoruz?", "paket sürümü neden çakıştı?" soruları yapı taşlarını bilmeyi gerektirir.
- **Sürüm kararları:** Hangi sürümü seçtiğiniz destek süresini, performansı ve kullanabileceğiniz özellikleri belirler.
- **Mülakat:** "SDK ile runtime farkı", "LTS/STS", ".NET Framework ile .NET Core farkı" ısınma soruları.

---

## 3. Nasıl?

### 3.1 Temel `dotnet` komutları

```bash
dotnet --info                       # kurulu SDK/runtime'lar, OS bilgisi
dotnet --list-sdks                  # kurulu SDK'lar
dotnet --list-runtimes              # kurulu runtime'lar
dotnet new list                     # mevcut şablonlar

dotnet new console  -n MyApp        # çalıştırılabilir uygulama
dotnet new classlib -n MyLib        # kütüphane (.dll)
dotnet new webapi   -n MyApi        # ASP.NET Core Web API
dotnet new xunit3   -n MyTests      # xUnit v3 test projesi (şablon: dotnet new install xunit.v3.templates)
dotnet new sln -n MySolution        # solution — .NET 10'da varsayılan biçim .slnx
dotnet new gitignore                # .NET .gitignore  ·  dotnet new editorconfig  ·  dotnet new globaljson

dotnet restore                      # NuGet paketlerini indir (build/run bunu zaten yapar)
dotnet build                        # derle   (-c Release ile Release yapılandırması)
dotnet run                          # derle + çalıştır  (--project yol, -- sonrası uygulamaya argüman)
dotnet test                         # testleri çalıştır
dotnet publish -c Release           # dağıtıma hazır çıktı üret (→ J5)
dotnet clean                        # bin/obj çıktılarını temizle

dotnet add package Serilog          # paket ekle (csproj'a <PackageReference>)
dotnet add reference ../MyLib       # proje referansı ekle
dotnet sln add src/MyApp            # projeyi solution'a ekle
dotnet list package --outdated      # eski paketler  ·  --vulnerable (güvenlik açığı olanlar)  ·  --include-transitive
dotnet tool install -g dotnet-ef    # global araç kurulumu
```

**Template'ler:** `console`, `classlib`, `webapi`, `web`, `worker`, `xunit3`, `mstest`, `nunit`, `blazor`… `dotnet new list` ile görülür; kendi şirket şablonunuzu da yazabilirsiniz (→ S8).

### 3.2 Solution ve proje yapısı

- **Proje (`.csproj`):** Tek bir derleme biriminin tanımı (MSBuild XML'i): hedef framework, paket/proje referansları, derleme ayarları. SDK-style proje **kısadır**: kaynak dosyalar otomatik dahil edilir (`**/*.cs`), listelemek gerekmez.
- **Solution (`.sln` / `.slnx`):** Birden çok projeyi gruplayan **üst konteynır** (IDE'de açmak ve `dotnet build` ile hepsini derlemek için). Kod içermez.
  - `.sln`: eski, GUID dolu, birleşme çakışması çıkaran metin biçimi.
  - **`.slnx` (yeni XML biçimi):** okunabilir, kısa, git dostu; .NET 10 SDK'da `dotnet new sln` varsayılan olarak bunu üretir. Örnek (3.6'daki çözüm):

    ```xml
    <Solution>
      <Folder Name="/src/">
        <Project Path="src/VatCalculator.Core/VatCalculator.Core.csproj" />
        <Project Path="src/VatCalculator.Cli/VatCalculator.Cli.csproj" />
      </Folder>
    </Solution>
    ```
- **Proje referansı** (`<ProjectReference>`): aynı solution'daki başka bir projeyi kullanmak — derleme sırası otomatik belirlenir.

```
VatCalculator/                 (3.6'da CLI ile kurulur)
├── VatCalculator.slnx
├── src/
│   ├── VatCalculator.Core/    ← class library: iş mantığı (başka projeler referans verir)
│   └── VatCalculator.Cli/     ← console: ince giriş katmanı → Core'a ProjectReference
└── tests/
    └── VatCalculator.Core.Tests/   ← xUnit: Core'u test eder
```

**Ortak ayarlar:** `Directory.Build.props` (her projeye uygulanan MSBuild ayarları: TFM, Nullable…) ve `Directory.Packages.props` (**Central Package Management**: paket sürümü tek yerde, `.csproj` içinde sürümsüz `<PackageReference>`). Bu repo bilerek ikisini de **kullanmaz**: yalnızca iki proje var (`ConsoleApp`, `WebApp`), ayarlar doğrudan `.csproj`'larda; çok projeli bir çözümde bu iki dosya devreye girer.

### 3.3 NuGet temelleri

- **NuGet**, .NET'in paket yöneticisidir; varsayılan kaynak [nuget.org](https://www.nuget.org). Özel kaynaklar (Azure Artifacts, GitHub Packages) `nuget.config` ile eklenir.
- Paket referansı: `<PackageReference Include="Serilog" Version="4.2.0" />`. Restore edilen paketler **kullanıcı genelinde** `~/.nuget/packages` önbelleğine iner, projeler arası paylaşılır (`bin/obj` içine kopyalanmaz).
- **Sürüm aralıkları:** `1.2.3` (en az 1.2.3 — NuGet **en düşük uygulanabilir** sürümü seçer), `[1.2.3]` (tam bu), `[1.0,2.0)` (1.0 dahil, 2.0 hariç). Üretimde **sabit sürüm** kullanın; `*` (floating) tekrarlanabilir olmayan derleme demektir.
- **Transitive bağımlılık:** Eklediğiniz paketin bağımlı olduğu paketler de gelir. `dotnet list package --include-transitive` ile görünür (3.6'daki 4. adım: tek `xunit.v3` referansı ondan fazla paket getirir).
  - **Diamond sorunu:** A→C 1.0, B→C 2.0 ise NuGet genelde en yüksek gereken sürümü seçer; uyumsuzluk çıkarsa `NU1107/NU1605` gibi hatalar görülür.
  - Güvenlik: transitive bağımlılıklarda da açık olabilir → `dotnet list package --vulnerable --include-transitive`, NuGet Audit (SDK'da varsayılan) restore sırasında uyarı verir (→ J11.4, S7).
- **Paket yazarı değilseniz** `dotnet pack`/`publish` konusu sonraya (→ M2).

### 3.4 `global.json` ile SDK sürümünü sabitleme

"Bende derleniyor, CI'da farklı SDK yüzünden derlenmiyor" sorununu önler:

```json
{
  "sdk": {
    "version": "10.0.100",
    "rollForward": "latestFeature",
    "allowPrerelease": false
  }
}
```

- `version`: istenen SDK (en düşük); `rollForward: latestFeature`: aynı major.minor içindeki daha yeni feature band'ı (10.0.1xx → 10.0.2xx) ve patch'leri kabul et.
- Dosya bulunduğu klasörün **ve altındakilerin** SDK'sını belirler (yukarı doğru en yakın `global.json` aranır). Karşılayan SDK yoksa `dotnet` komutları hata verir.
- .NET 10'da `global.json` ayrıca `dotnet test` için **test runner** seçimini de taşır: `"test": { "runner": "Microsoft.Testing.Platform" }` (xUnit v3 / Microsoft.Testing.Platform projeleri için gerekli). Örnek: `{ "sdk": { "version": "10.0.100", "rollForward": "latestFeature" }, "test": { "runner": "Microsoft.Testing.Platform" } }` (bu reponun kökündeki `global.json` aynı yapıdadır).

### 3.5 File-based apps (.NET 10) — `dotnet run app.cs`

.NET 10 ile **`.csproj` olmadan tek bir `.cs` dosyası** doğrudan çalıştırılır. Script gibi hafif ama tam C# + NuGet + ASP.NET Core:

```bash
dotnet run vat.cs -- 100 20      # '--' sonrası: uygulamanın args'ı
```

`#:` direktifleri dosyanın **en başında** (kodlardan önce) yazılır:

| Direktif | Karşılığı | Örnek |
|----------|-----------|-------|
| `#:package` | `<PackageReference>` | `#:package Newtonsoft.Json@13.0.3` |
| `#:sdk` | `<Project Sdk="…">` | `#:sdk Microsoft.NET.Sdk.Web` (Minimal API için) |
| `#:property` | `<PropertyGroup>` içindeki MSBuild property'si | `#:property PublishAot=false` |
| `#:project` | `<ProjectReference>` | `#:project ../Lib/Lib.csproj` |

Unix'te ilk satıra `#!/usr/bin/env dotnet` yazıp `chmod +x` ile dosyayı doğrudan çalıştırılabilir bir script yapabilirsiniz.

**Büyüyünce projeye dönüştürme:** `dotnet project convert vat.cs` — `#:` direktiflerini `.csproj`'a çevirip `vat/` klasörü üretir (üretilen csproj'a bakınca varsayılanları da görürsünüz: `OutputType=Exe`, `net10.0`, `ImplicitUsings`, **`PublishAot=true`**, `PackAsTool=true`).

**Üç gotcha** (3.6'daki örneklerde yorumlanmıştır):
1. File-based app'lerde **`PublishAot` varsayılanı açıktır** → yansıma tabanlı JSON serileştirme kapalıdır; anonim nesne döndüren Minimal API endpoint'i 500 verir. AOT'a hazır değilseniz `#:property PublishAot=false` (3.6, `web.cs`).
2. Üst klasörlerde **Central Package Management** (`Directory.Packages.props`) varsa `#:package Paket@sürüm` ile çakışır (`NU1008`); dosyada `#:property ManagePackageVersionsCentrally=false` gerekir (3.6, `with-package.cs`).
3. Üst klasörlerdeki **`Directory.Build.props` file-based app'lere de uygulanır** (ör. `InvariantGlobalization=true` kültür duyarlı kütüphaneleri etkileyebilir).

**Ne zaman hangisi?**

| | File-based app | Console projesi | Solution (çoklu proje) |
|---|----------------|-----------------|------------------------|
| Kullanım | Tek seferlik script, deney, prototip, mülakat canlı kodu, demo | Küçük araç/uygulama | Gerçek uygulama: katmanlar, testler, birden çok host |
| Dosya sayısı | 1 | Çok (tek proje) | Çok (çok proje) |
| Test edilebilirlik | Zor (test projesi referans veremez) | Orta | **İyi** (Core ayrı, test projesi referans verir) |
| Yapılandırma | `#:` direktifleri | `.csproj` | `.csproj` + `.slnx` + `Directory.*.props` |

### 3.6 Pratik: aynı uygulamayı üç şekilde kurmak

Üçü de **aynı işi** yapar: net tutara KDV ekler (`100` + `%20` → `120,00`). Hepsini repodan **bağımsız, boş bir klasörde** deneyin: üst klasörlerde Central Package Management (`Directory.Packages.props`) olan bir repoda `dotnet add package` onu değiştirirdi.

**(a) File-based app** — dosya sayısı 1, csproj yok. `vat.cs`:

```csharp
#:property Nullable=enable
using System.Globalization;

// Top-level statements: derleyici gizli bir Program sınıfı + Main üretir (→ J2.1). args = "--" sonrakiler.
if (args.Length < 1 || !decimal.TryParse(args[0], CultureInfo.InvariantCulture, out var net))
{
    Console.Error.WriteLine("Kullanım: dotnet run vat.cs -- <net tutar> [KDV yüzdesi=20]");
    return 1;
}

var rate = args.Length > 1 && decimal.TryParse(args[1], CultureInfo.InvariantCulture, out var r) ? r : 20m;
Console.WriteLine($"{net:N2} + %{rate:N0} KDV = {VatMath.WithVat(net, rate):N2}");
return 0;

// File-based app'te tipler dosyanın SONUNDA (top-level statement'lardan sonra) tanımlanır.
static class VatMath
{
    public static decimal WithVat(decimal net, decimal ratePercent)
        => Math.Round(net * (1 + ratePercent / 100m), 2, MidpointRounding.AwayFromZero);
}
```

```bash
dotnet run vat.cs -- 100 20      # 100,00 + %20 KDV = 120,00
```

`web.cs` — aynı hesap Minimal API olarak (`dotnet run web.cs` → `http://localhost:5090/vat?net=100&rate=20`):

```csharp
#:sdk Microsoft.NET.Sdk.Web
#:property PublishAot=false      // gotcha 1: varsayılan AOT, anonim nesne JSON'unu bozar

var app = WebApplication.CreateBuilder(args).Build();
app.MapGet("/vat", (decimal net, decimal? rate) =>
{
    var r = rate ?? 20m;
    return new { net, rate = r, total = Math.Round(net * (1 + r / 100m), 2, MidpointRounding.AwayFromZero) };
});
app.Run("http://localhost:5090");
```

`with-package.cs` — `#:package` ile NuGet (`dotnet run with-package.cs`):

```csharp
#:package Newtonsoft.Json@13.0.3
#:property ManagePackageVersionsCentrally=false   // gotcha 2: üst klasörde CPM varsa NU1008
using Newtonsoft.Json;

Console.WriteLine(JsonConvert.SerializeObject(new { Net = 100m, Rate = 20m, Total = 120m }, Formatting.Indented));
```

**(b) Console projesi** — artık bir `.csproj` var: derleme ayarları, paket referansları, birden çok dosya, IDE desteği.

```bash
dotnet new console -n VatCalculator.Console
cd VatCalculator.Console
# Program.cs'e (a)'daki kodu yapıştırın (#: satırları hariç), sonra:
dotnet run -- 100 20
```

**(c) Solution + class library + test projesi** — yalnızca CLI ile, sıfırdan. `VatMath` ayrı bir class library'ye taşınır (test edilebilir), console ince bir giriş katmanıdır:

```bash
dotnet --list-sdks && dotnet --list-runtimes                       # hangi SDK/runtime kurulu?

dotnet new sln -n VatCalculator                                    # .NET 10'da varsayılan: .slnx
dotnet new classlib -n VatCalculator.Core  -o src/VatCalculator.Core
dotnet new console  -n VatCalculator.Cli   -o src/VatCalculator.Cli
dotnet new classlib -n VatCalculator.Tests -o tests/VatCalculator.Tests   # test projesi: classlib + xunit paketi

dotnet sln add src/VatCalculator.Core src/VatCalculator.Cli tests/VatCalculator.Tests
dotnet add src/VatCalculator.Cli reference src/VatCalculator.Core          # ProjectReference
dotnet add tests/VatCalculator.Tests reference src/VatCalculator.Core

dotnet add tests/VatCalculator.Tests package xunit.v3             # <PackageReference> yazılır
dotnet list tests/VatCalculator.Tests package --include-transitive | head -20   # tek paket → onlarca transitive

# xUnit v3 testleri çalıştırılabilir (Exe) projedir; .NET 10'da `dotnet test`'in yeni modu için global.json:
sed -i 's#<PropertyGroup>#<PropertyGroup>\n    <OutputType>Exe</OutputType>#' tests/VatCalculator.Tests/VatCalculator.Tests.csproj
echo '{ "test": { "runner": "Microsoft.Testing.Platform" } }' > global.json

dotnet build && dotnet test && dotnet run --project src/VatCalculator.Cli
cat src/VatCalculator.Cli/VatCalculator.Cli.csproj                 # şablonun ürettiği çıplak csproj
```

`Core`'daki metot doğrulamalı olur (`ArgumentOutOfRangeException.ThrowIfNegative(net)`); Cli yalnızca girdi/çıktıdan sorumludur.

---

## 4. Gerçek dünya senaryosu

**"Lokalde çalışıyor, CI'da `NETSDK1045: The current .NET SDK does not support targeting .NET 10.0`."**

1. Projeyi `net10.0` hedefleyecek şekilde güncellediniz; kendi makinenizde .NET 10 SDK var.
2. CI ajanı eski bir imaj kullanıyor ve .NET 8 SDK'sı kurulu → **SDK, hedef framework'ten eski** olduğu için derleyemiyor.
3. Teşhis: CI günlüğünde `dotnet --info`. Çözüm: ajanda SDK'yı kurmak (`actions/setup-dotnet`), ve tekrar yaşanmasın diye repoya **`global.json`** koymak → hem geliştirici hem CI aynı SDK bandını kullanır.
4. Üretimde ise aynı uygulamayı çalıştıran sunucuda **runtime** (SDK değil) eksik olabilir: `Framework: 'Microsoft.AspNetCore.App', version '10.0.0' was not found`. Docker'da `mcr.microsoft.com/dotnet/aspnet:10.0` (runtime) imajı ile `sdk:10.0` imajının farkı tam olarak budur (→ J12).

**Ek senaryo — "Prototip büyüdü":** Mülakat öncesi 30 satırlık `scratch.cs` ile bir fikri denediniz; sonra servise dönüşecek. `dotnet project convert scratch.cs` → proje, ardından Core'u ayrı class library'ye alıp test ekleme (c) yapısı. Üç yapı, bir uygulamanın **olgunlaşma basamaklarıdır**.

---

## 5. Mülakat perspektifi

Sık sorulanlar ([`interview-questions.md`](interview-questions.md)'de cevaplarıyla):
- **SDK ile runtime farkı nedir?** ⭐
- **TFM nedir?** `net10.0` ile `netstandard2.0` ne demek?
- .NET Framework, .NET Core ve .NET (5+) farkı; .NET Standard neden artık gerekmiyor?
- LTS ile STS farkı; hangisini production'da seçersin?
- `.sln` ile `.csproj` farkı; `.slnx` neden geldi?
- Transitive bağımlılık nedir? Paket sürümü çakışması nasıl çözülür?
- `global.json` ne işe yarar? Central Package Management nedir?
- `dotnet run app.cs` (file-based apps) ne işe yarar?

**İpucu:** "SDK build, runtime run" tek cümlesini söyleyip **örnek verin**: "CI'da SDK, production imajında sadece runtime olur."

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| SDK ile runtime'ı karıştırmak ("SDK kurdum, sunucuda da yeter") | Sunucuda yalnızca **runtime** yeterli; SDK build içindir. Container'da çok aşamalı build (→ J12) |
| Projeyi yeni TFM'e güncelleyip CI/Docker imajını güncellememek | `global.json` + CI'da `setup-dotnet` + imaj etiketini birlikte güncelleyin |
| Üretimde STS sürüme geçip desteğin 24 ayda bittiğini unutmak | Üretim = LTS (mevcut: .NET 10); STS'yi bilinçli seçin |
| `bin/` ve `obj/` klasörlerini commit'lemek | `.gitignore` (→ J1.1) |
| Kayan sürüm (`Version="*"`) ile paket eklemek | Tekrarlanabilir olmayan build. Sabit sürüm veya Central Package Management |
| Paket sürümlerini her `.csproj`'a ayrı ayrı yazmak | Çok projede sürüm kayması. `Directory.Packages.props` (CPM) |
| Transitive bağımlılıkta güvenlik açığını görmezden gelmek | `dotnet list package --vulnerable --include-transitive`, NuGet Audit uyarılarını ciddiye alın |
| File-based app'e `Paket@sürüm` yazıp CPM çakışmasını anlamamak | `NU1008`: bu dosyada `#:property ManagePackageVersionsCentrally=false` (bkz. 3.5) |
| File-based Minimal API'de 500 (JSON metadata hatası) | `PublishAot` varsayılan açık → `#:property PublishAot=false` ya da source-generated JSON |
| `dotnet test`'e MSBuild bayrağı eklemek (ör. `-nologo`) | Microsoft.Testing.Platform modunda argümanlar test uygulamasına iletilir; tanımsız bayrak `Handshake failures` verir |
| `dotnet add package` ile Central Package Management'ı beklenmedik şekilde değiştirmek | CPM'de sürüm `Directory.Packages.props`'ta yaşar; CLI orayı düzenler. Tümünü kasıtlı yapın |
| Uygulama framework'ünden (`net10.0`) eski bir makinede çalıştırmayı denemek | Roll-forward major'a atlamaz; ya runtime kurun ya self-contained yayınlayın |

---

## 7. Performans & trade-off

- **Build süresi:** Büyük solution'larda gereksiz proje referansı zinciri her değişiklikte yeniden derlemeyi tetikler; `dotnet build --no-restore`, artımlı derleme ve katmanlı projeler (kararlı Core ayrı) yardımcı olur.
- **Çoklu hedef (`TargetFrameworks`)** kütüphane tüketicilerine uyumluluk sağlar ama derleme/test matrisini katlar; sadece gerçekten desteklenecek hedefleri yazın.
- **`netstandard2.0` vs doğrudan `net10.0`:** Standard geniş uyumluluk, modern API'leri (span, yeni BCL) kaybettirir; modern hedef performans/özellik kazandırır.
- **Self-contained vs framework-dependent:** Self-contained her şeyi paketler (büyük çıktı, runtime kurulumu gerekmez, güvenlik yamalarını siz taşırsınız); framework-dependent küçük ve ortak runtime'ı kullanır ama sunucuda runtime ister (→ J5).
- **File-based app derlemesi** ilk çalıştırmada derler ve önbelleğe alır; her çalıştırmada proje gibi artımlı çalışır — ağır iş yükleri için proje daha uygundur.
- **Central Package Management** küçük bir kurulum maliyetiyle büyük repolarda sürüm kayması ve birleştirme çakışmalarını ortadan kaldırır.

---

## 8. Kaynaklar

- .NET indirme ve destek politikası: <https://dotnet.microsoft.com/platform/support/policy/dotnet-core>
- .NET CLI genel bakış: <https://learn.microsoft.com/dotnet/core/tools/>
- `.slnx` solution biçimi: <https://learn.microsoft.com/visualstudio/ide/solutions-and-projects-in-visual-studio>
- `global.json` genel bakış: <https://learn.microsoft.com/dotnet/core/tools/global-json>
- File-based apps (`dotnet run file.cs`): <https://learn.microsoft.com/dotnet/core/sdk/file-based-apps>
- NuGet — Central Package Management: <https://learn.microsoft.com/nuget/consume-packages/central-package-management>
- NuGet — sürüm aralıkları ve çözümleme: <https://learn.microsoft.com/nuget/concepts/package-versioning>
- .NET Standard'ın geleceği: <https://learn.microsoft.com/dotnet/standard/net-standard>
- Hedef framework'ler (TFM) listesi: <https://learn.microsoft.com/dotnet/standard/frameworks>
- .NET 10'da neler yeni: <https://learn.microsoft.com/dotnet/core/whats-new/dotnet-10/overview>
