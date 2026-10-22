# J1.4 .NET Ekosistemi & CLI — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. ⭐ SDK ile Runtime arasındaki fark nedir?
**SDK** uygulamayı *geliştirmek/derlemek* için gereken her şeydir: `dotnet` CLI, Roslyn derleyicisi, MSBuild, NuGet, şablonlar — ve içinde bir runtime da bulunur. **Runtime** derlenmiş uygulamayı *çalıştırmak* için gereken minimum bileşendir (CLR/JIT, GC, base class library; web için ASP.NET Core runtime'ı). Geliştirici makinesi ve CI'da SDK, production sunucu/container'da yalnızca runtime olur (Docker'da `sdk` imajı build için, `aspnet`/`runtime` imajı çalıştırmak için).

### 2. ⭐ Target Framework Moniker (TFM) nedir?
`.csproj` içindeki `<TargetFramework>` değeridir; projenin hangi API kümesini hedeflediğini söyler: `net10.0` (modern, platformlar arası .NET), `net10.0-windows` (Windows'a özel API'ler: WPF, WinForms, registry), `netstandard2.0` (Framework ve .NET arası kütüphane paylaşımı için API sözleşmesi), `net48` (.NET Framework 4.8). Kütüphane yazarları birden çok hedefi `<TargetFrameworks>net8.0;net10.0</TargetFrameworks>` ile yazabilir.

### 3. .NET 10 SDK ile `net8.0` hedefleyen bir proje derleyebilir miyim? Çalıştırmak için ne gerekir?
Derleyebilirim: yeni SDK eski TFM'leri hedefleyebilir (geriye uyumlu). Çalıştırmak için ise **hedefin runtime'ı** (8.x) gerekir; varsayılan roll-forward aynı major'ın daha yeni patch'ine gider ama yeni bir major'a atlamaz (`RollForward` ayarıyla değiştirilebilir). Tersi (eski SDK ile `net10.0` derlemek) mümkün değildir (`NETSDK1045`).

### 4. .NET Framework, .NET Core ve .NET 5+ arasındaki fark nedir?
**.NET Framework** (2002): yalnızca Windows, makine genelinde kurulu, kapalı kaynak; bakım modunda. **.NET Core** (2016): sıfırdan yazılmış, açık kaynak, cross-platform, modüler, daha hızlı, side-by-side sürümler, container dostu. **.NET 5+** (2020 →): Core ve Framework çizgilerinin birleşmiş hali, tek ürün adı ".NET"; her yıl Kasım'da yeni sürüm. Yeni geliştirme .NET 10'da yapılır; .NET Framework yalnızca eski kurumsal uygulamalar için.

### 5. .NET Standard nedir, neden artık gerekmiyor?
Framework, Core ve Xamarin gibi birden çok .NET uygulamasında kod paylaşmak için bir **API sözleşmesiydi** (`netstandard2.0`: "bu API setini sunan her runtime'da çalışır"). .NET 5+ tek, birleşik platform olunca ihtiyaç ortadan kalktı. Yeni kütüphaneler doğrudan `net8.0`/`net10.0` hedefler; `netstandard2.0` yalnızca hâlâ .NET Framework'ü de desteklemesi gereken kütüphanelerde kullanılır.

### 6. ⭐ LTS ile STS arasındaki fark nedir? Production için hangisi?
Her yıl Kasım'da yeni sürüm çıkar. **Çift numaralılar LTS** (3 yıl destek: .NET 8, **.NET 10**), **tek numaralılar STS** (24 ay: .NET 9, .NET 11). Üretimde genellikle **LTS** tercih edilir (daha uzun yama desteği, daha seyrek yükseltme); STS yeni özellikleri erken denemek ve bir sonraki LTS'e hazırlanmak için uygundur. Destek tarihlerini resmî destek politikası sayfasından doğrulamak gerekir.

### 7. `dotnet build`, `dotnet run` ve `dotnet publish` farkı nedir?
`build` derler (`bin/` altına çıktı); `run` gerekirse derleyip çalıştırır (geliştirme için); `publish` uygulamayı dağıtıma hazır, bağımlılıklarıyla birlikte bir klasöre çıkarır (framework-dependent veya self-contained; ayrıntısı → J5.3).

### 8. `.sln`/`.slnx` ile `.csproj` arasındaki fark?
`.csproj` tek bir derleme biriminin (proje) MSBuild tanımıdır: hedef framework, referanslar, ayarlar. Solution ise birden çok projeyi gruplayan üst konteynırdır (IDE'de açmak, `dotnet build` ile hepsini derlemek için); kod içermez. `.slnx` yeni XML tabanlı, okunabilir ve git dostu biçimdir (eski `.sln`'in GUID'li, merge conflict üreten formatının yerine); .NET 10 SDK'da `dotnet new sln` varsayılan olarak `.slnx` üretir.

### 9. Proje referansı ile paket referansı arasındaki fark nedir?
`ProjectReference` aynı solution'daki başka bir projeyi kaynak olarak kullanır (derleme sırasını otomatik belirler, kaynak kodla birlikte gelişir). `PackageReference` NuGet'ten indirilen, derlenmiş ve sürümlenmiş bir paketi kullanır. İç modüller için proje referansı, dış kütüphaneler (veya ayrı yayımlanan iç kütüphaneler) için paket referansı.

### 10. Transitive bağımlılık nedir? Sürüm çakışması nasıl yönetilir?
Eklediğim paketin kendisinin bağımlı olduğu paketlerin projeme otomatik gelmesidir. `dotnet list package --include-transitive` ile görülür. Aynı paketin farklı sürümleri istenirse NuGet genellikle istenen **en yüksek** sürümü seçer; uyumsuzluk varsa `NU1107/NU1605` gibi hatalar çıkar. Çözüm: ihtiyaca göre paketi doğrudan referans verip sürümü sabitlemek ya da Central Package Management ile sürümleri tek yerden yönetmek. Transitive paketlerde de güvenlik açığı çıkabileceği için `dotnet list package --vulnerable --include-transitive` kontrolü yapılır.

### 11. Central Package Management (CPM) nedir?
Çözümün kökündeki `Directory.Packages.props` dosyasında `<PackageVersion Include="X" Version="…" />` ile tüm paket sürümlerinin tek yerde tutulması; `.csproj`'larda sürümsüz `<PackageReference Include="X" />` yazılır. Çok projeli repolarda sürüm kayması ve gereksiz merge conflict'lerini önler. (`ManagePackageVersionsCentrally=true`.)

### 12. `global.json` ne işe yarar?
Bulunduğu klasör (ve altı) için kullanılacak **SDK sürümünü sabitler** (`version`, `rollForward`, `allowPrerelease`). Geliştirici makineleri ve CI'ın aynı SDK ile derlemesini sağlar; "bende derleniyor, CI'da derlenmiyor" sorununu azaltır. .NET 10'da ayrıca `dotnet test` için test runner seçimini (`Microsoft.Testing.Platform`) de taşıyabilir.

### 13. File-based app nedir? (`dotnet run app.cs`) Ne zaman kullanılır?
.NET 10 ile `.csproj` olmadan tek bir `.cs` dosyasının doğrudan çalıştırılabilmesidir. `#:package`, `#:sdk`, `#:property`, `#:project` direktifleriyle NuGet, SDK (ör. Web) ve MSBuild ayarları dosyanın başında verilir. Script, prototip, demo ve mülakat canlı kodu için idealdir. Büyüyünce `dotnet project convert app.cs` ile normal projeye dönüştürülür. Birden çok katman/test gereken gerçek uygulamalar için proje/solution yapısı daha uygundur.

### 14. File-based app ile ilgili bilmen gereken tuzaklar var mı?
(1) Varsayılanı `PublishAot=true`'dur; yansıma tabanlı JSON serileştirme (ör. anonim nesne döndüren Minimal API) çalışmaz — AOT'a hazır değilsek `#:property PublishAot=false`. (2) Repo kökünde Central Package Management varsa `#:package Paket@sürüm` ile çakışır (`NU1008`); `ManagePackageVersionsCentrally=false` gerekir. (3) Üst klasörlerdeki `Directory.Build.props` dosyaya da uygulanır.

### 15. Aynı uygulamayı hangi aşamada tek dosyadan solution'a taşırsın?
Dosya birden fazla sorumluluk taşımaya başladığında, birim testi gerektiğinde, birden çok host (CLI + API) aynı mantığı kullanacağında veya takım çalışmaya başladığında: önce `dotnet project convert`, sonra iş mantığını ayrı bir class library'ye (Core), test projesini yanına, ince bir host projesini üzerine koyarım (README 3.6'daki `VatCalculator` çözüm yapısı).

### 16. `dotnet new` ile hangi şablonları kullanırsın?
`console` (uygulama), `classlib` (kütüphane), `webapi` (ASP.NET Core API), `worker` (arka plan servisi), `xunit3`/`mstest`/`nunit` (test), `sln` (solution), ayrıca `gitignore`, `editorconfig`, `globaljson` yardımcı şablonları. `dotnet new list` ile görülür, şirket şablonları da yazılabilir.

### 17. Bir projede `bin/` ve `obj/` ne işe yarar, commit'lenir mi?
`obj/` ara çıktıları ve restore sonuçlarını (`project.assets.json`), `bin/` derleme çıktısını tutar. İkisi de üretilebilir çıktıdır; `.gitignore`'a eklenir, commit'lenmez (→ J5.2).

### 18. "Production sunucuda `Framework 'Microsoft.AspNetCore.App' version '10.0.0' was not found`" hatası ne anlama gelir?
Uygulama framework-dependent yayınlanmış ve sunucuda ASP.NET Core 10 runtime'ı kurulu değil. Çözüm: doğru runtime'ı kurmak, container'da `aspnet:10.0` imajı kullanmak ya da uygulamayı self-contained yayınlamak. (SDK kurmak gerekmez.)
