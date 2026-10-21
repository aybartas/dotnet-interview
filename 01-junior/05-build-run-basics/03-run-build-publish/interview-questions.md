# J5.3 `run` vs `build` vs `publish` — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan. Modül (J5) sorularının tamamı için J5.1 ve J5.2 dosyalarına da bakın.

### 1. ⭐ `dotnet run` ile `dotnet publish` farkı nedir? Production'a hangisiyle çıkılır?
`dotnet run` kodu derleyip (gerekirse) hemen çalıştırır; geliştirme içindir. `dotnet publish` uygulamayı **dağıtıma hazır** bir klasöre (Release, yalnızca çalışmak için gerekenler) çıkarır; **production publish çıktısıyla yapılır**. `run` production'da kullanılmaz: SDK gerektirir (büyük image/saldırı yüzeyi), her çalıştırmada build/güncellik kontrolü yapar (yavaş/beklenmedik), kaynak ve build araçları sunucuda kalır ve `launchSettings.json` (geliştirme profili) devreye girer.

### 2. `dotnet build` ile `dotnet publish` farkı nedir?
`build` derler ve çıktıyı `bin/<config>/<tfm>/` altına koyar (varsayılan Debug); IDE/test/geliştirme içindir. `publish` build'e ek olarak dağıtım birimini hazırlar: varsayılan **Release**, `publish/` klasörü, yalnızca çalışma için gerekenler (web'de `wwwroot`, `web.config` vb.); tek dosya, self-contained, trimming, ReadyToRun, Native AOT gibi seçenekleri yalnızca publish sunar. Basit bir konsol uygulamasında iki çıktı neredeyse aynıdır; farkı yapılandırma ve publish seçenekleri yaratır.

### 3. `dotnet restore`, `build`, `run`, `test`, `pack` ne yapar?
`restore` NuGet bağımlılıklarını çözüp indirir (`obj/project.assets.json`); `build` derler (implicit restore); `run` build edip çalıştırır; `test` testleri derleyip çalıştırır; `pack` NuGet paketi (`.nupkg`) üretir. CI'da `restore` genelde explicit bir adımdır ve sonraki adımlar `--no-restore` ile çalışabilir.

### 4. ⭐ `launchSettings.json` nedir? Production'da kullanılır mı?
Geliştirme sırasında Visual Studio ve `dotnet run` tarafından okunan profil dosyasıdır (URL'ler, ortam değişkenleri, başlatma komutu). Çıktıya/publish'e girmez ve production'da kullanılmaz. Production ayarları ortam değişkenlerinden, `appsettings.<Ortam>.json`'dan veya gizli depodan (Key Vault) gelir.

### 5. `dotnet publish` varsayılan olarak hangi yapılandırmayı kullanır?
Release (.NET 8+); `build` ve `run` varsayılanı Debug'dır. Üretime Debug çıktısı gitmemesi için `publish` bunu varsayılan yapar (optimizasyonlu IL, `DEBUG` yok).

### 6. Framework-dependent, self-contained ve Native AOT yayın farkı nedir?
**Framework-dependent:** küçük çıktı, makinede kurulu .NET gerekir; yamalar runtime güncellemesiyle gelir. **Self-contained:** runtime uygulamayla birlikte gelir, hedefte .NET gerekmez, boyut büyük, yama için yeniden yayın gerekir. **Native AOT:** IL önceden makine koduna derlenir, JIT yoktur; çok hızlı başlangıç ve düşük bellek, ama yansıma/dinamik kod kısıtları vardır. Derin hali → M2.5.

### 7. Sunucuda "You must install or update .NET to run this application" hatası ne demek?
Uygulama framework-dependent yayınlanmış ve hedef makinede `runtimeconfig.json`'da istenen runtime kurulu değil. Çözüm: doğru runtime'ı (SDK değil) kurmak, container'da `aspnet:10.0`/`runtime:10.0` imajı kullanmak ya da self-contained yayınlamak.

### 8. `dotnet run`'a komut satırı argümanı nasıl geçilir?
`dotnet run -- <argümanlar>`: `--`'den sonrakiler uygulamaya (`args`) gider; öncesindekiler `dotnet run`'ın kendi seçenekleridir (`-c Release`, `--no-build`, `--launch-profile`).

### 9. `dotnet publish` çıktısı hangi klasörde oluşur, nasıl değiştirilir?
Varsayılan `bin/<config>/<tfm>/publish/` (RID varsa ek klasör). `-o <klasör>` ile değiştirilir; CI/Docker'da genelde `-o ./out` kullanılır.

### 10. Bir uygulamayı Docker'a koyarken build ve publish nasıl ayrılır?
Çok aşamalı Dockerfile: build aşaması `sdk` imajında `dotnet restore` + `dotnet publish -c Release -o /app`; çalışma aşaması küçük `aspnet`/`runtime` imajına yalnızca publish çıktısını kopyalar (SDK ve kaynak son imaja girmez). Detay → J12.

### 11. `PublishSingleFile` ve `PublishTrimmed` ne yapar?
`PublishSingleFile` uygulama dosyalarını tek çalıştırılabilir dosyaya paketler (dağıtım kolaylığı). `PublishTrimmed` kullanılmayan kodu çıktıdan atarak boyutu küçültür (yansıma kullanan kodu kırabilir; trim analizörlerini uyarılarını ciddiye almak gerekir). İkisi de yalnızca `publish` ile kullanılır.

### 12. Hangi durumda `dotnet watch` kullanılır?
Geliştirme döngüsünde: dosya değişince uygulamayı yeniden çalıştırır veya Hot Reload ile durumu koruyarak değişikliği uygular. Production ile ilgisi yoktur (→ J1.5).
