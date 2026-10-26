# J6.3 Configuration & Options Temelleri

> 📁 `WebApp/01-junior/06-aspnetcore-fundamentals/03-configuration-basics/` · Önkoşul: J2–J4 · Sorular: [`interview-questions.md`](interview-questions.md)

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

ASP.NET Core'da **configuration**, uygulamanın kod değiştirmeden ortamdan ortama değişen ayarlarını (connection string, port, URL, feature flag, log seviyesi, SMTP bilgileri, API anahtarları) okuma mekanizmasıdır. Ayarlar tek bir yerden değil, **sırayla eklenen birden çok kaynaktan** (provider) gelir; hepsi tek bir **`IConfiguration`** görünümünde birleşir:

```
Kaynak (provider)                     Örnek                              Öncelik
─────────────────────────────────────────────────────────────────────────────────
1. appsettings.json                   "Smtp": { "Port": 587 }            en düşük
2. appsettings.{Environment}.json     appsettings.Production.json           │
3. User Secrets (sadece Development)  dotnet user-secrets set ...           │
4. Environment variables              Smtp__Port=2525                       ▼
5. Command-line arguments             --Smtp:Port=25                     en yüksek
```

**Kural: sonra eklenen kazanır.** Aynı key birden çok kaynakta varsa sonuncunun değeri okunur; diğerleri sessizce ezilir. Bu sıra `WebApplication.CreateBuilder(args)` içinde hazır gelir (bu projede `appsettings*.json` dosyalarını konu klasöründen biz ekliyoruz, bkz. 3.1).

**Options pattern** ise configuration'ın üstüne kurulan tip-güvenli katmandır: `"Smtp"` bölümü bir `SmtpOptions` sınıfına **bind** edilir, DataAnnotations ile **doğrulanır** ve `IOptions<SmtpOptions>` ile DI üzerinden enjekte edilir. Stringly-typed `config["Smtp:Port"]` yerine `options.Value.Port` (bir `int`) kullanırsınız.

Konunun kodu bu klasörde:

| Dosya | İçerik |
|-------|--------|
| [`ConfigurationBasicsTopic.cs`](ConfigurationBasicsTopic.cs) | Host kurulumu, kaynakların eklenmesi, options kaydı, endpoint'ler (`/priority`, `/priority/live`, `/hierarchy`, `/smtp`) |
| [`PriorityDemo.cs`](PriorityDemo.cs) | Aynı key'i 5 kaynaktan veren **izole** `ConfigurationBuilder` — kim kazanıyor? |
| [`SmtpOptions.cs`](SmtpOptions.cs) | Strongly-typed options + DataAnnotations |
| [`appsettings.json`](appsettings.json), [`appsettings.Development.json`](appsettings.Development.json), [`appsettings.Production.json`](appsettings.Production.json) | Demo ayar dosyaları (`Smtp:Password` **bilerek yok**) |

---

## 2. Neden?

- **Aynı build, her ortamda:** Kod bir kez derlenir (J5.3); Development'ta yerel DB'ye, Production'da gerçek DB'ye bağlanması **sadece ayarla** belirlenir. Ayarı koda gömmek her ortam için yeniden build demektir.
- **Secret'lar koddan ve git'ten ayrı yaşar.** Şifre, token, connection string repoya girerse geri alınamaz: git geçmişinde kalır, botlar dakikalar içinde tarar. Katmanlı kaynak modeli, "varsayılan JSON'da, hassas değer ortamdan" ayrımını doğal kılar.
- **Operasyonel esneklik:** Docker/Kubernetes/Azure App Service ayarları **environment variable** olarak verir; bir değeri değiştirmek için imajı yeniden üretmek gerekmez (`Smtp__Port=2525` yeter).
- **Tip güvenliği ve fail-fast:** Options + validation ile yanlış ayar (`Port = 70000`, eksik şifre) **uygulama açılırken** patlar; üretimde ilk istekte, gece 3'te patlamaz.
- **Mülakat:** "Configuration kaynaklarının öncelik sırası?" ve "Secret'ları nerede tutarsın?" junior/mid seviyesinin en sık sorulan ASP.NET Core sorularındandır.

---

## 3. Nasıl?

### 3.1 Kaynaklar ve öncelik: kim kazanır? ([`PriorityDemo.cs`](PriorityDemo.cs))

Demo, aynı key'i (`Demo:Message`) **beş kaynaktan** verir ve gerçek varsayılan sırayla üst üste koyar. Sonucun okuyanın makinesine bağlı olmaması için **izole bir `ConfigurationBuilder`** kullanılır; iki katman simüle edilir (çıktıda da yazar): User Secrets (in-memory provider) ve komut satırı (sabit bir `string[]`). Environment variable ise gerçektir: demo kendi process'ine `J63_Demo__Message` değişkenini kendisi koyar ve `AddEnvironmentVariables("J63_")` ile okur.

```bash
cd WebApp
dotnet run -- J6.3                          # http://localhost:5080 (durdurmak: Ctrl+C)
curl -s http://localhost:5080/priority | jq -c '.layers[] | [.order, .source, .providesKey, .valueAfterThisLayer]'
curl -s http://localhost:5080/priority | jq -c .winner
```

```text
[1,"appsettings.json",true,"from appsettings.json"]
[2,"appsettings.Production.json",true,"from appsettings.Production.json"]
[3,"User Secrets (simulated in memory)",false,"from appsettings.Production.json"]   ← atlandı: ortam Development değil
[4,"Environment variable J63_Demo__Message",true,"from environment variable (J63_Demo__Message)"]
[5,"Command line (simulated)",true,"from command line (--Demo:Message=...)"]
{"source":"Command line (simulated)","value":"from command line (--Demo:Message=...)"}
```

`providesKey` o katmanın key'i **tek başına** verip vermediğini, son sütun ise o katman da üste konduktan sonraki **birikmiş** değeri gösterir; en alttaki değer kazanandır.

Aynı endpoint'i `--environment Development` ile çalıştırırsanız 2. katman `appsettings.Development.json` olur ve 3. katman (User Secrets) devreye girer — çünkü **varsayılan olarak yalnızca Development'ta** eklenir. `--environment Staging` verirseniz `appsettings.Staging.json` bulunamaz ve opsiyonel olduğu için **hata vermeden atlanır** (`providesKey` = `false`):

```bash
dotnet run -- J6.3 --environment Development
```

```text
[1,"appsettings.json",true,"from appsettings.json"]
[2,"appsettings.Development.json",true,"from appsettings.Development.json"]
[3,"User Secrets (simulated in memory)",true,"from user-secrets (simulated)"]
[4,"Environment variable J63_Demo__Message",true,"from environment variable (J63_Demo__Message)"]
[5,"Command line (simulated)",true,"from command line (--Demo:Message=...)"]
```

**Gerçek host'un kendi yapılandırması** için `/priority/live` kullanılır: `IConfigurationRoot.Providers` kayıt sırasıyla listelenir ve key'i veren **son** provider kazanan olarak gösterilir (`root[key]` de aynı şekilde sondan başa arar). Kendiniz deneyin:

```bash
dotnet run -- J6.3                                                   # kazanan: appsettings.Production.json
Demo__Message="from real env" dotnet run -- J6.3                     # kazanan: EnvironmentVariablesConfigurationProvider
Demo__Message="from real env" dotnet run -- J6.3 --Demo:Message=hello   # kazanan: CommandLineConfigurationProvider
```

```text
"hello"
"CommandLineConfigurationProvider"
```

Konsolda açılışta yazılan provider listesi de bu yüzden öğreticidir. `WebApplication.CreateBuilder` kendi host-ayar provider'larını (`ASPNETCORE_`/`DOTNET_` önekli env var'lar, content root'taki `appsettings*.json`) önceden ekler; bu projede content root'ta `appsettings.json` olmadığı için bizimkini `AddJsonFile(...)` ile **sonradan** ekleriz. Bir tuzak: sonradan eklenen kazandığı için JSON dosyalarımız, daha önce eklenmiş env var/komut satırı provider'larını **ezerdi**. Gerçek sıra korunsun diye kod, JSON'dan sonra `AddEnvironmentVariables()` ve `AddCommandLine(args)` çağrılarını **tekrarlar** (bkz. `AddConfiguration`).

```text
 5. JsonConfigurationProvider for 'appsettings.json' (Optional)            ← CreateBuilder'ın kendi eklediği (bu projede dosya yok)
 6. JsonConfigurationProvider for 'appsettings.Production.json' (Optional)
 9. EnvironmentVariablesConfigurationProvider
11. JsonConfigurationProvider for 'appsettings.json' (Required)            ← bizim AddJsonFile'larımız
12. JsonConfigurationProvider for 'appsettings.Production.json' (Optional)
13. EnvironmentVariablesConfigurationProvider                              ← sırayı geri kazanmak için tekrar eklendi
14. CommandLineConfigurationProvider
```

> Not: Gerçek listede ayrıca 1–4, 7–8 ve 10 numaralı host-ayarı provider'ları (`ASPNETCORE_`/`DOTNET_` önekli env var'lar, `WebApp.settings.json` gibi ek opsiyonel dosyalar, `ChainedConfigurationProvider`) görünür; bu çıktı .NET 10 SDK 10.0.113 ile alındı ve liste sürüme göre küçük farklar gösterebilir. Değişmeyen şey **sıra mantığı**dır: sonra gelen kazanır.

### 3.2 Hiyerarşik key'ler ve env var karşılığı

Configuration, JSON'daki iç içe yapıyı **düz key'lere** çevirir; ayraç `:` karakteridir.

```json
{ "Logging": { "LogLevel": { "Default": "Information" } } }
```
```text
Logging:LogLevel:Default = Information
```

| Kaynak | Yazım |
|--------|-------|
| JSON | `{ "Logging": { "LogLevel": { "Default": "Information" } } }` |
| Kod | `config["Logging:LogLevel:Default"]` |
| Environment variable | `Logging__LogLevel__Default=Warning` (`:` yerine **çift alt çizgi**) |
| Komut satırı | `--Logging:LogLevel:Default=Warning` ya da `--Logging:LogLevel:Default Warning` |
| Dizi elemanı | `Endpoints:1:Name` (env var: `Endpoints__1__Name`) |

Neden `__`? Bazı shell'lerde/işletim sistemlerinde env var adında `:` kullanılamaz (ör. Linux'ta çoğu shell'de export edilemez); `__` her yerde çalışır ve provider tarafından otomatik `:` olur. Key'ler **büyük/küçük harfe duyarsızdır** (`LOGGING:loglevel:DEFAULT` aynı değeri verir).

`/hierarchy` endpoint'i `IConfiguration` API'sinin tamamını tek yerde gösterir ([`ConfigurationBasicsTopic.cs`](ConfigurationBasicsTopic.cs) → `/hierarchy`):

```bash
curl -s http://localhost:5080/hierarchy | jq -c '.keysUnderLogging[], .getValue, .getChildren.endpointsArray, .environmentVariableOverride'
```

```text
{"key":"Logging:LogLevel:Microsoft.AspNetCore","environmentVariable":"Logging__LogLevel__Microsoft.AspNetCore","value":"Warning"}
{"key":"Logging:LogLevel:Default","environmentVariable":"Logging__LogLevel__Default","value":"Information"}
{"port":587,"useSsl":true,"missingWithDefault":30,"missingWithoutDefault":0}
[{"key":"0","name":"primary","url":"https://primary.example.com"},{"key":"1","name":"backup","url":"https://backup.example.com"}]
{"key":"Logging:LogLevel:Default","environmentVariable":"J63_Logging__LogLevel__Default","note":"AddEnvironmentVariables(\"J63_\") strips the prefix, then '__' becomes ':'","valueInJsonOnly":"Information","valueWithEnvironmentVariable":"Warning"}
```

### 3.3 `IConfiguration` API

| API | Ne yapar | Dikkat |
|-----|----------|--------|
| `config["A:B:C"]` | Değeri `string?` olarak okur | Yoksa **`null`** döner, exception yok |
| `config.GetSection("A:B")` | Alt ağaca bir görünüm (`IConfigurationSection`) | Section yoksa bile null dönmez, boş section verir; `GetRequiredSection` yoksa exception fırlatır |
| `config.GetValue<int>("Smtp:Port")` | Tipe çevirerek okur | Yoksa `default(T)` (**0**) — `GetValue("Smtp:Timeout", 30)` ile varsayılan verin |
| `section.Get<SmtpOptions>()` | Bölümü bir nesneye bind eder | Section hiç yoksa **`null`** döner |
| `section.GetChildren()` | Doğrudan alt section'lar | Dizi elemanları `"0"`, `"1"` key'leriyle gelir |
| `config.GetConnectionString("Default")` | `ConnectionStrings:Default` kısayolu | |

```csharp
var port    = config.GetValue<int>("Smtp:Port");                    // 587
var timeout = config.GetValue("Smtp:Timeout", 30);                  // key yok → 30
var smtp    = config.GetSection("Smtp").Get<SmtpOptions>();         // tüm bölüm → nesne
foreach (var child in config.GetSection("Endpoints").GetChildren())
    Console.WriteLine($"{child.Key}: {child["Name"]}");             // 0: primary, 1: backup
```

`IConfiguration` her yere serpiştirilirse key'ler string olarak dağılır ve typo derleyiciye yakalanmaz — bu yüzden uygulama kodunda genelde **options** tercih edilir (3.4).

### 3.4 Options pattern'e giriş ([`SmtpOptions.cs`](SmtpOptions.cs))

```csharp
public sealed class SmtpOptions
{
    public const string SectionName = "Smtp";

    [Required]                 public string Host { get; set; } = "";
    [Range(1, 65535)]          public int Port { get; set; } = 25;
                               public bool UseSsl { get; set; }
    [Required, EmailAddress]   public string From { get; set; } = "";
                               public string? Password { get; set; }   // SECRET: appsettings.json'da YOK
}
```

```csharp
builder.Services
    .AddOptions<SmtpOptions>()
    .Bind(builder.Configuration.GetSection("Smtp"))      // "Smtp" bölümü → SmtpOptions
    .ValidateDataAnnotations()                           // [Required], [Range], [EmailAddress]
    .Validate(o => !requirePassword || !string.IsNullOrWhiteSpace(o.Password),
              "Smtp:Password is required ...")           // koşullu, özel kural
    .ValidateOnStart();                                  // ilk kullanımı değil, UYGULAMA AÇILIŞINI beklemeden doğrula

// Doğrulamasız kısa yol:
// builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));
```

Kullanım — `IOptions<T>` constructor'dan ya da minimal API parametresinden enjekte edilir (DI → J6.2):

```csharp
app.MapGet("/smtp", (IOptions<SmtpOptions> options) => options.Value.ToMasked());   // şifre maskeli
```
```bash
curl -s http://localhost:5080/smtp
```
```json
{
  "host": "smtp.example.com",
  "port": 587,
  "useSsl": true,
  "from": "noreply@example.com",
  "password": "(not set)"
}
```

| Arayüz | Ömür | Değişikliği görür mü? | Ne zaman |
|--------|------|------------------------|----------|
| `IOptions<T>` | Singleton | Hayır (bir kez okunur) | Varsayılan tercih — ayarlar çalışırken değişmiyorsa |
| `IOptionsSnapshot<T>` | Scoped | Her **request** başında yeniden okunur | Request başına taze ayar |
| `IOptionsMonitor<T>` | Singleton | Evet — `OnChange` bildirimi | Singleton servislerde canlı ayar |

`reloadOnChange` (JSON dosyası değişince yeniden yükleme), `IOptionsSnapshot`/`IOptionsMonitor`, named options ve `[OptionsValidator]` source generator'ı → **M3**; bu konuda yalnızca `IOptions<T>` + validation kullanıyoruz.

### 3.5 Secret'lar asla `appsettings.json`'a ve git'e girmez ⭐

`appsettings.json` repoya commit edilir; içindeki her şey **herkese açık kabul edilmelidir**. Bu yüzden `Smtp:Password` JSON dosyasında yoktur; değer ortama göre farklı yoldan gelir:

| Ortam | Nerede tutulur | Nasıl okunur |
|-------|----------------|--------------|
| Development | **User Secrets** (kullanıcı profilinde, repo dışında düz metin JSON) | Development'ta otomatik provider |
| CI / container / Kubernetes | **Environment variable** (CI secret store, K8s `Secret`) | `Smtp__Password` |
| Production (Azure) | **Azure Key Vault** (+ Managed Identity ile şifresiz erişim); App Service'te "Application settings"/Key Vault reference | Key Vault configuration provider veya env var olarak enjekte |

**Development — User Secrets.** Bu repoda proje dosyasına dokunmamak için secret store'un id'si koddadır (`dotnet-interview-j6-3`), bu yüzden `--id` ile çalışılır:

```bash
dotnet user-secrets set "Smtp:Password" "my-dev-password" --id dotnet-interview-j6-3
dotnet user-secrets list --id dotnet-interview-j6-3
# Smtp:Password = my-dev-password
dotnet user-secrets remove "Smtp:Password" --id dotnet-interview-j6-3   # tek key
dotnet user-secrets clear --id dotnet-interview-j6-3                    # hepsi
```

Kendi projenizde: `dotnet user-secrets init` (csproj'a rastgele bir `<UserSecretsId>` yazar), sonra `dotnet user-secrets set "Smtp:Password" "..."`. `WebApplication.CreateBuilder` bu id'yi Development'ta otomatik yükler. Dosya şurada durur (repo dışında):

```text
Windows      : %APPDATA%\Microsoft\UserSecrets\<id>\secrets.json
Linux / macOS: ~/.microsoft/usersecrets/<id>/secrets.json
```

> User Secrets **şifreli değildir**; yalnızca "secret'ı repoya sokma" aracıdır, bir güvenlik sınırı değildir. Production'da kullanılmaz.

**Production — environment variable:**

```bash
export Smtp__Password='s3cret'                 # Linux / macOS (bash, zsh)
docker run -e Smtp__Password=s3cret myapp      # Docker (gerçekte: --env-file veya orchestrator secret'ı)
```
```powershell
$env:Smtp__Password = "s3cret"                 # PowerShell
```
```bat
set Smtp__Password=s3cret
```

**Fail-fast gösterimi** (`--require-password` anahtarı şifreyi zorunlu kılar):

```bash
dotnet run -- J6.3 --require-password          # şifre hiçbir kaynakta yok
```

```text
fail: Microsoft.Extensions.Hosting.Internal.Host[11]
      Hosting failed to start
      Microsoft.Extensions.Options.OptionsValidationException: Smtp:Password is required (--require-password) but no source provided it.

STARTUP FAILED (fail-fast): the configuration is invalid, the app will not start.
  - Smtp:Password is required (--require-password) but no source provided it.

How to fix: set the Smtp__Password environment variable, or: dotnet user-secrets set "Smtp:Password" "..." --id dotnet-interview-j6-3
```

Süreç `1` koduyla çıkar; **tek bir istek bile karşılanmaz**. Şifreyi verince uygulama açılır ve `/smtp` şifreyi maskeler:

```bash
Smtp__Password=s3cret dotnet run -- J6.3 --require-password
curl -s http://localhost:5080/smtp | jq -c .
```
```text
{"host":"smtp.example.com","port":587,"useSsl":true,"from":"noreply@example.com","password":"********"}
```

Aynı doğrulama DataAnnotations kurallarında da çalışır; komut satırı en yüksek öncelikli olduğu için JSON'daki geçerli `587` değerini ezer ve açılış reddedilir:

```bash
dotnet run -- J6.3 --Smtp:Port=70000
```
```text
OptionsValidationException: DataAnnotation validation failed for 'SmtpOptions' members: 'Port' with the error: 'The field Port must be between 1 and 65535.'.
```

**`.gitignore` hatırlatması:** secret içerebilecek dosyalar baştan dışarıda tutulur.

```gitignore
# local / secret configuration
appsettings.*.local.json
secrets.json
.env
*.pfx
```

> Yanlışlıkla commit edilen secret **hemen döndürülür (rotate)**: dosyayı silmek yetmez, git geçmişinde durur. GitHub'ın secret scanning / push protection özelliği bu hataları yakalar.

### 3.6 Alıştırmalar

1. `dotnet run -- J6.3 --environment Development` çalıştırın; `/priority` çıktısında 3. katmanın nasıl aktif olduğunu gözleyin. Sonra `--environment Staging` deneyin: hangi `appsettings.*.json` yüklenir, `Demo:Message` ne olur? (Cevap: dosya yok → opsiyonel olduğu için sessizce atlanır.)
2. `Smtp__Port=2525 dotnet run -- J6.3` ve ardından `... --Smtp:Port=25` ile `/smtp` çıktısını karşılaştırın.
3. `dotnet user-secrets set "Smtp:Password" "x" --id dotnet-interview-j6-3` verin; **Production**'da (`--environment` olmadan) `--require-password` ile açın: neden hâlâ patlar? (User Secrets sadece Development'ta yüklenir.)
4. `SmtpOptions`'a `[Range(1, 60)] public int TimeoutSeconds { get; set; } = 30;` ekleyin ve `--Smtp:TimeoutSeconds=0` ile açılışı bozun.

<details>
<summary>Çözümler</summary>

1. Staging için dosya yoktur; `appsettings.Staging.json` `optional: true` eklendiğinden sessizce atlanır, `Demo:Message` `"from appsettings.json"` kalır (env var/komut satırı katmanları hâlâ üste biner). **Linux'ta dosya adları büyük/küçük harfe duyarlıdır:** `--environment staging` ile aranan dosya `appsettings.staging.json`'dır.
2. Env var 2525'i, komut satırı 25'i verir; komut satırı kazanır.
3. Çünkü User Secrets provider'ı yalnızca Development'ta eklenir; Production'da şifre ancak env var ile verilebilir.
4. `OptionsValidationException: ... 'TimeoutSeconds' ...` ile açılış durur; anotasyon eklemek dışında kod değişmez — kural options sınıfında yaşar.

</details>

---

## 4. Gerçek dünya senaryosu

Bir e-ticaret API'si üç ortamda çalışır:

| | Development | Staging | Production |
|---|---|---|---|
| `Smtp:Host` | `localhost` (MailHog, `appsettings.Development.json`) | `smtp.staging.example.com` | `smtp.example.com` (`appsettings.json`) |
| `Smtp:Password` | User Secrets | Kubernetes Secret → env var | Azure Key Vault (Managed Identity) |
| `Logging:LogLevel:Default` | `Debug` | `Information` | `Warning` (env var ile geçici `Debug`'a çekilebilir) |
| `ConnectionStrings:Default` | `localhost` DB (User Secrets) | Staging DB (env var) | Production DB (Key Vault) |

- **Akış:** CI aynı imajı üretir (J5.3); ortam değişkenleri imajı değil **çalışma anındaki ayarı** belirler. Production'da canlı sorun incelenirken log seviyesi `Logging__LogLevel__Default=Debug` env var'ıyla, kod/deploy değişmeden yükseltilir.
- **Fail-fast:** Yeni bir sürümde `Smtp__Password` unutulursa pod ayağa kalkmaz; Kubernetes rollout durur, eski sürüm hizmet vermeye devam eder. `ValidateOnStart` olmasaydı sorun ilk e-posta gönderiminde, kullanıcıya hata olarak yansırdı.
- **Maskeleme:** `/smtp` benzeri tanılama endpoint'leri şifreyi hiçbir zaman döndürmez; loglara da yazılmaz (→ J6.5).

---

## 5. Mülakat perspektifi

Beklenen cevabın çekirdeği: **kaynaklar + "sonra gelen kazanır" + `__` + secret'lar nerede**.

- ⭐ "Configuration kaynaklarının öncelik sırası nedir?" → `appsettings.json` → `appsettings.{Environment}.json` → User Secrets (yalnızca Development) → environment variables → komut satırı; **sonraki öncekini ezer**.
- ⭐ "Secret'ları nerede saklarsın?" → Repoda asla; Development'ta User Secrets, Production'da environment variable / Key Vault. `appsettings.json` herkese açık kabul edilir.
- "`Logging:LogLevel:Default`'u env var ile nasıl verirsin?" → `Logging__LogLevel__Default`.
- "`IOptions`, `IOptionsSnapshot`, `IOptionsMonitor` farkı?" → Singleton-sabit / Scoped-request başına / Singleton-değişim bildirimli.
- "`ValidateOnStart` ne işe yarar?" → Doğrulama ilk kullanımda değil, host başlarken çalışır; yanlış yapılandırma uygulamayı hiç açmaz (fail-fast).
- Tuzak sorular: `GetValue<int>` key yoksa ne döner (0)? `Get<T>()` section yoksa (null)? `config["x"]` yoksa exception atar mı (hayır, null)?

Tam soru-cevap listesi: [`interview-questions.md`](interview-questions.md).

---

## 6. Yaygın hatalar

| Hata | Sonuç | Doğrusu |
|------|-------|---------|
| Şifreyi/connection string'i `appsettings.json`'a yazıp commit etmek | Secret herkese sızar, git geçmişinden silinemez | User Secrets / env var / Key Vault; sızdıysa **rotate** |
| Production'da User Secrets'a güvenmek | Değer yüklenmez (yalnızca Development), uygulama boş şifreyle çalışır | Production için env var / Key Vault |
| Env var'da `Smtp:Password` yazmak | Linux'ta çoğu shell'de tanımlanamaz | `Smtp__Password` (çift alt çizgi) |
| `appsettings.production.json` (küçük harf) | Linux dosya sistemi büyük/küçük harfe duyarlıdır; dosya bulunmaz, sessizce atlanır | Ortam adıyla birebir aynı yazım: `appsettings.Production.json` |
| Sonradan `AddJsonFile` ekleyip env var'ların ezildiğini fark etmemek | "Environment variable çalışmıyor" | Sırayı kontrol edin; gerekirse env var/komut satırını **sonda** tekrar ekleyin |
| `GetValue<int>("Smtp:Port")` ile eksik key'i 0 almak | Sessiz hata (port 0) | Options + `[Required]`/`[Range]` + `ValidateOnStart` |
| Options'ta yazım hatası (`"Smpt"`) | Bölüm bulunamaz, tüm alanlar varsayılan kalır — hata yok | `SectionName` sabiti, `Get`/`Bind` sonrası validation |
| Options property'sini `private set` / get-only bırakmak | Binder varsayılan olarak yalnızca public setter'lı property'leri doldurur; değer sessizce varsayılan kalır | `{ get; set; }` kullanın |
| Secret'ı log'a veya diagnostic endpoint'e yazmak | Sızıntı | Maskeleyin, hiç yazmayın |
| `IOptions<T>` ile canlı değişiklik beklemek | Değer uygulama ömrü boyunca sabit kalır | `IOptionsMonitor<T>` (→ M3) |

---

## 7. Performans & trade-off

- **Okuma maliyeti önemsizdir:** Configuration açılışta bir kez bellekte düz key/value sözlüğüne yüklenir; `config["x"]` sözlük araması, `IOptions<T>.Value` ise bir kez oluşturulup önbelleklenir. Hot path'te okumak sorun değildir; yine de gereksiz yere `Get<T>()` ile her istekte yeniden bind etmeyin.
- **`reloadOnChange`:** JSON dosyalarını izlemek için file watcher kullanır (`CreateBuilder`'ın appsettings dosyalarında varsayılan açık). Container'larda ayar genelde değişmez imajla birlikte gelir; dosya değişikliği yerine yeni deploy tercih edilir. Canlı değişim gerekiyorsa `IOptionsMonitor<T>` (→ M3).
- **`ValidateOnStart` maliyeti:** Açılışta birkaç yansıma/validation çağrısı; yanlış yapılandırılmış üretim dağıtımını erken yakalama kazancı yanında ihmal edilebilir.
- **Binder ve AOT/trim:** Klasik `Bind`/`Get<T>` reflection kullanır; Native AOT'ta configuration binding source generator'ı gerekir (→ M2.5, M3).
- **Trade-off — katman sayısı:** Çok kaynak esneklik verir ama "değer nereden geldi?" sorusunu zorlaştırır. Çözüm: bu konudaki `/priority/live` gibi tanılama, açılışta provider listesini loglamak ve ayarları mümkün olduğunca az kaynakta tutmak.
- **Trade-off — Key Vault:** Güvenlik ve rotasyon kazanırsınız; ağ bağımlılığı, açılış gecikmesi ve ek maliyet getirir (cache + Managed Identity ile yönetilir).

---

## 8. Kaynaklar

- Microsoft Learn — *Configuration in ASP.NET Core*: https://learn.microsoft.com/aspnet/core/fundamentals/configuration/
- Microsoft Learn — *Options pattern in ASP.NET Core*: https://learn.microsoft.com/aspnet/core/fundamentals/configuration/options
- Microsoft Learn — *Safe storage of app secrets in development in ASP.NET Core*: https://learn.microsoft.com/aspnet/core/security/app-secrets
- Microsoft Learn — *Azure Key Vault configuration provider in ASP.NET Core*: https://learn.microsoft.com/aspnet/core/security/key-vault-configuration
- Microsoft Learn — *Options validation* (`ValidateOnStart`): https://learn.microsoft.com/dotnet/core/extensions/options#options-validation
- The Twelve-Factor App — *III. Config* (ayarlar ortamda): https://12factor.net/config
- Bu repoda: DI temelleri (→ J6.2), Ortamlar (→ J6.6), Logging (→ J6.5), `IOptionsSnapshot`/`IOptionsMonitor`, `reloadOnChange`, named options (→ M3)
