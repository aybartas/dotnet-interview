# J5.3 `run` vs `build` vs `publish` ⭐

> 📁 `01-junior/05-build-run-basics/03-run-build-publish/` · Önkoşul: J5.2 · Sorular: [`interview-questions.md`](interview-questions.md)

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

`dotnet restore`, `build`, `run`, `publish`, `test`, `pack` farklı işler yapar ve farklı yerlerde kullanılır. En sık karıştırılan üçlü **`run` / `build` / `publish`**'tir: `run` ve `build` **geliştirme**, `publish` **deployment** içindir. Konunun kodu [`Sections/`](Sections/) altında 3 bölümdür (`dotnet run -- 5.3 <numara>`); 2. bölüm **dotnet CLI'ı kendisi çalıştırıp** build ve publish çıktılarını karşılaştırır.

---

## 2. Neden?

- "`dotnet run` ile `dotnet publish` farkı? Production'a hangisiyle çıkılır?" mülakatın favorisidir.
- Yanlış komutla yayın (`dotnet run` ile prod, Debug çıktısıyla deploy) performans, güvenlik ve yapılandırma sorunları getirir.
- CI/CD ve Docker (→ J12) doğrudan `restore → build → test → publish` adımlarına dayanır.

---

## 3. Nasıl?

### 3.1 Komutlar tablosu ([`Section01`](Sections/Section01_CommandsAndLaunchSettings.cs))

| Komut | Ne yapar | Çıktı | Ne zaman |
|-------|----------|-------|----------|
| `dotnet restore` | Bağımlılıkları çözer/indirir | `obj/project.assets.json` | CI'da explicit adım |
| `dotnet build` | Derler (implicit restore) | `bin/<config>/<tfm>/` | Geliştirme, derleme doğrulama |
| `dotnet run` | Build eder **ve çalıştırır** | Çalışan process | **Yalnızca geliştirme** |
| `dotnet watch` | Değişiklikte yeniden çalıştırır / hot reload | — | Geliştirme döngüsü |
| `dotnet publish` | **Dağıtıma hazır** klasör üretir | `bin/<config>/<tfm>/publish/` | **Deployment** |
| `dotnet test` | Testleri derleyip çalıştırır | Test sonuçları | CI |
| `dotnet pack` | NuGet paketi üretir | `.nupkg` | Kütüphane yayını |

- Varsayılan yapılandırma: `build`/`run` → **Debug**; `publish` → **Release** (.NET 8+).
- **`dotnet run` neden production'da kullanılmaz?** (1) **SDK gerektirir** (sunucuda/container'da derleme araçları: büyük image, büyük saldırı yüzeyi); (2) her çalıştırmada build/güncellik kontrolü yapar (yavaş başlangıç, beklenmedik yeniden derleme); (3) kaynak kod ve build araçları sunucuda durur; (4) `launchSettings.json` (geliştirme profili) devreye girer.

### 3.2 `launchSettings.json` yalnızca geliştirme içindir ([`Section01`](Sections/Section01_CommandsAndLaunchSettings.cs), [`Properties/launchSettings.json`](../Properties/launchSettings.json))

```json
{ "profiles": { "J05.BuildRunBasics": { "commandName": "Project",
    "environmentVariables": { "DEMO_ENV": "launchSettings.json'dan geldi" } } } }
```

```bash
dotnet run -- 5.3 1                                           # DEMO_ENV = "launchSettings.json'dan geldi"
dotnet bin/Debug/net10.0/J05.BuildRunBasics.dll 5.3 1         # DEMO_ENV = (tanımsız) — doğrudan çalıştırınca okunmaz
dotnet run --no-launch-profile -- 5.3 1                       # launchSettings'i hiç kullanma
```

- `launchSettings.json`'ı yalnızca **Visual Studio / `dotnet run`** okur; `bin/` ve publish çıktısına **girmez**. (`dotnet run` ilk satırda `Using launch settings from …` yazar.)
- Production ayarları ortam değişkeninden (`ASPNETCORE_ENVIRONMENT`, bağlantı dizesi), `appsettings.<Ortam>.json`'dan veya gizli depodan (Key Vault) gelir (→ J6.4).

### 3.3 🛠 Aynı projeyi `build` ve `publish` ile çıkar ([`Section02`](Sections/Section02_BuildVsPublish.cs))

Program üç komutu geçici bir klasöre çalıştırıp çıktıları karşılaştırır (SDK gerekir; ~12 sn):

```bash
dotnet build   -c Release -o <A>
dotnet publish -c Release -o <B>                                               # framework-dependent (varsayılan)
dotnet publish -c Release -r <rid> --self-contained false -p:PublishSingleFile=true -o <C>
```

Elle karşılaştırmak için: `dotnet run -- 5.3 compare <klasörA> <klasörB>`.

Gözlem (bu projede, Release):

| Karşılaştırma | Sonuç |
|---------------|-------|
| `build` ↔ `publish` (framework-dependent) | **6 dosya, ~178 KB — dosya listesi ve boyutlar aynı** (`dll`, apphost, `pdb`, `deps.json`, `runtimeconfig.json`, `appsettings.json`) |
| `publish` ↔ `publish` + tek dosya | `dll`, `deps.json` ve `runtimeconfig.json` ayrı dosya olmaktan çıkıp **tek çalıştırılabilir dosyaya gömülür** (3 dosya, ~65 MB: bu SDK tek dosya isteğinde runtime'ı da pakete katmış görünüyor; `-p:SelfContained=true/false`'ı açıkça belirtin) |

**Ders:** Basit bir framework-dependent konsol uygulamasında `build` ve `publish` çıktıları **neredeyse aynıdır**. Farkı yaratan şeyler:

1. **Konum ve varsayılan yapılandırma:** `build` → `bin/Debug/net10.0/` (Debug); `publish` → `bin/Release/net10.0/publish/` (Release, optimizasyonlu).
2. **Publish "dağıtım birimi"dir:** yalnızca çalışmak için gerekenleri toplar (web projelerinde `wwwroot`/statik dosyalar, `web.config`); geliştirme dosyaları (`launchSettings.json`, `obj/`, kaynak) girmez. `build` klasörü IDE/test çalıştırma içindir.
3. **Yalnızca publish'in sunduğu seçenekler:** tek dosya (`PublishSingleFile`), self-contained (`-r` + runtime dahil), trimming (`PublishTrimmed`), ReadyToRun (`PublishReadyToRun`), **Native AOT** (`PublishAot`).

### 3.4 Deployment modellerine giriş ([`Section03`](Sections/Section03_DeploymentModels.cs))

| Model | Runtime nerede? | Boyut | Komut |
|-------|-----------------|-------|-------|
| **Framework-dependent** (varsayılan) | Makinede kurulu .NET | Küçük | `dotnet publish -c Release` |
| **Self-contained** | Uygulamayla birlikte gelir | Büyük (~70+ MB) | `dotnet publish -c Release -r linux-x64 --self-contained` |
| **Native AOT** | Yok: kod önceden native derlenir | Küçük, hızlı başlangıç | `dotnet publish -c Release -r linux-x64 -p:PublishAot=true` |

- **Framework-dependent:** sunucuda/container'da .NET runtime kurulu olmalıdır (Docker'da `aspnet`/`runtime` imajı); güvenlik yamaları runtime güncellemesiyle gelir. Eksikse *"You must install or update .NET…"* hatası.
- **Self-contained:** hedef makinede .NET gerekmez; runtime sürümü uygulamaya kilitlidir (yama için yeniden yayımlamak gerekir). `runtimeconfig.json`'da `includedFrameworks` görünür.
- **Native AOT:** IL, yayın sırasında makine koduna derlenir; JIT yoktur → çok hızlı başlangıç ve düşük bellek; yansıma/dinamik kod kısıtları vardır.
- Seçim: kurumsal sunucu/container → framework-dependent; izole/istemci makinesi → self-contained; serverless/CLI/soğuk başlangıç → AOT. Derin hali → M2.5.

---

## 4. Gerçek dünya senaryosu

**"Deployment script'i sunucuda `dotnet run` çalıştırıyor, her açılışta 40 sn sürüyor ve bazen derleme hatası veriyor."** SDK sunucuda kurulu; her restart kaynağı yeniden derliyor. Pipeline `dotnet publish -c Release -o ./out` çıktısını yayınlayacak, sunucuda yalnızca `dotnet MyApp.dll` (veya apphost) çalıştıracak şekilde değiştirildi: açılış saniyelere düştü, sunucudan SDK ve kaynak kod kalktı.

**"Geliştirmede `https://localhost:5001`'de çalışıyor, container'da farklı portu dinliyor."** `launchSettings.json`'daki `applicationUrl` yalnızca `dotnet run`/VS içindir; container'da `ASPNETCORE_URLS`/`ASPNETCORE_HTTP_PORTS` ortam değişkenleri kullanılır.

**"Sunucuda `You must install or update .NET`."** Uygulama framework-dependent yayınlanmış, sunucuda doğru runtime yok. Runtime kuruldu (ya da `aspnet:10.0` imajı kullanıldı); alternatif self-contained yayındı.

---

## 5. Mülakat perspektifi

Sık sorulanlar: **`dotnet run` ile `dotnet publish` farkı, production'a hangisiyle çıkılır?** ⭐ · `build` ile `publish` farkı · `launchSettings.json` nedir, production'da kullanılır mı? · Framework-dependent / self-contained / Native AOT farkı · Neden `publish` varsayılanı Release?

**İpucu:** Cevabı **gerekçeli** verin: *"`run` geliştirme içindir: SDK ister, her seferinde build kontrolü yapar, kaynak ve araçlar sunucuda kalır. Production'a `publish` çıktısıyla çıkılır: Release derlemesi, yalnızca çalışmak için gerekenler, SDK gerekmez."*

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| Production'da `dotnet run` | `dotnet publish -c Release` çıktısını çalıştır |
| `build` çıktısını (Debug) sunucuya kopyalamak | `publish` (Release) |
| Production ayarlarını `launchSettings.json`'a yazmak | Ortam değişkeni / `appsettings.<Ortam>.json` / gizli depo |
| `publish`'i `-c Debug` ile almak | Varsayılan Release; Debug'ı yalnızca bilinçli teşhis için |
| Self-contained/framework-dependent farkını bilmeden yayınlamak | Hedef ortamı (runtime var mı?) belirleyip seç |
| `dotnet run`'a `--` olmadan argüman geçirmek | `dotnet run -- <uygulama argümanları>` |
| CI'da `restore` ve `build`'i her adımda tekrar etmek | `restore` bir kez; sonra `--no-restore`/`--no-build` ile zincirle |
| Single-file/AOT'de `Assembly.Location` ve yansımaya güvenmek | `AppContext.BaseDirectory`; AOT uyarılarını (IL3000, IL2026) oku |

---

## 7. Performans & trade-off

- **Başlangıç:** `dotnet run` önce build/güncellik kontrolü yapar; `publish` çıktısı doğrudan başlar. Soğuk başlangıç kritikse ReadyToRun/Native AOT.
- **Boyut ↔ bağımlılık:** framework-dependent küçüktür ama runtime kurulumuna bağlıdır; self-contained bağımsızdır ama büyüktür (trimming ile küçülür, kırılganlık riskiyle); AOT en küçük/hızlı başlangıç, en çok kısıt.
- **Güvenlik/bakım:** framework-dependent'ta runtime yamaları sunucu güncellemesiyle gelir; self-contained'da her yamada yeniden yayın gerekir.
- **CI süresi:** `dotnet publish` build + paketleme yapar; `--no-build` ile tekrar derleme önlenebilir. Docker'da çok aşamalı derleme (build imajı `sdk`, çalışma imajı `aspnet`) → J12.

---

## 8. Kaynaklar

- `dotnet publish`: <https://learn.microsoft.com/dotnet/core/tools/dotnet-publish>
- `dotnet build`, `dotnet run`: <https://learn.microsoft.com/dotnet/core/tools/dotnet-build> · <https://learn.microsoft.com/dotnet/core/tools/dotnet-run>
- .NET uygulama yayınlama (deployment modelleri): <https://learn.microsoft.com/dotnet/core/deploying/>
- Tek dosya, trimming, ReadyToRun: <https://learn.microsoft.com/dotnet/core/deploying/single-file/overview>
- Native AOT: <https://learn.microsoft.com/dotnet/core/deploying/native-aot/>
- `launchSettings.json` (ASP.NET Core): <https://learn.microsoft.com/aspnet/core/fundamentals/environments#development-and-launchsettingsjson>
- .NET 8'de `publish` varsayılan Release: <https://learn.microsoft.com/dotnet/core/compatibility/sdk/8.0/dotnet-publish-config>
