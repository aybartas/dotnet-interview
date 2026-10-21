# J5.2 `bin/` ve `obj/` — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. ⭐ `bin` ve `obj` klasörlerinde ne var?
`obj/` ara çıktılardır: restore sonucu (`project.assets.json`), NuGet'in ürettiği props/targets, derleyiciye giren üretilmiş kaynaklar (`AssemblyInfo.cs`, `GlobalUsings.g.cs`), ara `.dll`/`.pdb` ve artımlı derleme önbellekleri. `bin/<config>/<tfm>/` son çıktıdır: `.dll` (kodun, IL + metadata), apphost (`.exe`), `.pdb` (debug sembolleri), `.deps.json` (bağımlılık manifesti), `.runtimeconfig.json` (hangi runtime), bağımlılık `.dll`'leri ve `CopyToOutputDirectory` ile kopyalanan içerik dosyaları.

### 2. `.pdb` dosyası ne işe yarar? Production'a koyulmalı mı?
IL ile kaynak kod satırları/yerel değişkenler arasındaki eşlemeyi tutar: debugger'da breakpoint ve stack trace'te satır numaraları bunun sayesindedir. Yokluğunda uygulama yine çalışır ama exception'larda satır numarası görünmez. Üretimde genellikle yayınlanır (ya da symbol server'a yüklenir) çünkü küçük maliyetle teşhisi kolaylaştırır.

### 3. `runtimeconfig.json` ve `deps.json` ne işe yarar? Kim okur?
`runtimeconfig.json`: uygulamanın hangi runtime/framework sürümünü (`Microsoft.NETCore.App 10.0.0`), roll-forward ayarını ve GC/globalization gibi runtime ayarlarını söylediği dosyadır; apphost/`hostfxr` okur. `deps.json`: yüklenecek assembly'lerin ve paketlerin manifestosudur; `hostpolicy` bağımlılık yollarını bundan çözer.

### 4. Neden `bin` ve `obj` `.gitignore`'a eklenir?
Kaynaktan üretilebilir (build çıktısı) dosyalardır; depoya girerse repo şişer, makineye özel yollar ve sürekli değişen ikili dosyalar merge çakışması üretir, gereksiz diff gürültüsü olur. Standart .NET `.gitignore` her ikisini de içerir.

### 5. `dotnet clean` ne yapar, ne zaman gerekir?
`bin/<config>/` ve `obj/` altındaki üretilmiş dosyaları siler (`project.assets.json`'ı silmez). Eski çıktının çalıştığından şüphelenince, SDK/paket değişiminden sonra tuhaf derleme hataları olduğunda ya da boyut temizliğinde kullanılır. Kökten temizlik için `bin/` ve `obj/` klasörlerini silip yeniden derlemek yeterlidir (restore dahil).

### 6. `obj/project.assets.json` nedir?
`dotnet restore`'un sonucudur: çözülen tüm paketlerin (transitive dahil), sürümlerinin ve bağımlılık grafının kaydı. Derleyici ve IDE referansları buradan çözer. Paket/sürüm değişince yeniden üretilir.

### 7. Bir içerik dosyasının (ör. `appsettings.json`) çıktı klasörüne kopyalanması için ne yapılır?
`.csproj`'ta `<None Include="dosya" CopyToOutputDirectory="PreserveNewest" />` (veya `<Content ...>`) yazılır. `Never` kopyalamaz (varsayılan), `PreserveNewest` kaynak yeniyse kopyalar, `Always` her derlemede kopyalar. Web SDK projelerinde `appsettings.json` bunu varsayılan olarak alır. Unutulursa çalışma zamanında "dosya bulunamadı" hatası alınır.

### 8. Framework-dependent bir uygulamada `runtimeconfig.json` neyi söyler, self-contained'da fark ne?
Framework-dependent'ta `framework` bölümü (ad + sürüm) makinede kurulu bir runtime gerektiğini söyler; yoksa uygulama başlamaz. Self-contained'da runtime uygulamayla birlikte gelir ve `includedFrameworks` görünür; hedef makinede .NET kurulu olması gerekmez.

### 9. `obj/` klasöründeki `.dll` ile `bin/` klasöründeki `.dll` aynı mı?
Evet, aynı içerik: derleyici önce `obj/`'a yazar, MSBuild sonra `bin/`'e kopyalar. Bu yüzden önce ara çıktı (obj), sonra son çıktı (bin) oluşur.

### 10. `GlobalUsings.g.cs` nedir?
`ImplicitUsings` açıkken SDK'nın üretip derlemeye eklediği dosyadır; `global using System; global using System.Linq; global using System.Threading.Tasks;` gibi her dosyada geçerli olan using'leri içerir. `obj/<config>/<tfm>/` altında görülebilir.

### 11. Aynı projeyi hem Debug hem Release derlersek `bin/` nasıl görünür?
Yapılandırma başına ayrı klasör oluşur: `bin/Debug/net10.0/` ve `bin/Release/net10.0/` (çok TFM'li projede TFM başına da). `obj/` de aynı şekilde yapılandırmaya göre bölünür. Yanlış klasörden çalıştırmak "eski/farklı çıktı çalışıyor" sorunlarının sık nedenidir.
