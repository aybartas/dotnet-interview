# C2.12 Azure App Configuration

> 📁 `ConsoleApp/06-certifications/02-ai-200-azure-ai-cloud-developer/12-app-configuration/` · Sınav alanı: **Secure, monitor, and troubleshoot Azure solutions (%20-25)** · Önkoşul: [C2.11](../11-identity-key-vault/README.md) (managed identity, Key Vault), M9 (kimlik) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of October 7, 2026):** *Secure, monitor, and troubleshoot Azure solutions* — **Azure App Configuration** ile uygulama yapılandırmasını **sakla ve al** (key-value, label, Key Vault referansı, dinamik yenileme, feature flag). Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)'ı kontrol et.

## İçindekiler

1. [Ne?](#1-ne)
2. [Neden?](#2-neden)
3. [Nasıl?](#3-nasıl)
4. [Gerçek dünya senaryosu](#4-gerçek-dünya-senaryosu)
5. [Sınav perspektifi](#5-sınav-perspektifi)
6. [Yaygın hatalar](#6-yaygın-hatalar)
7. [Performans & trade-off](#7-performans--trade-off)
8. [Kaynaklar](#8-kaynaklar)

---

## 1. Ne?

**Azure App Configuration**, uygulama ayarlarını ve **feature flag**'leri uygulama kodundan ve dağıtımdan ayrı, **merkezî** tutan yönetilen servistir. Ayarlar anahtar-değer (key-value) çiftleridir; her çift isteğe bağlı bir **label** ve **content type** taşır, değişiklik geçmişi (revision) tutulur.

| Kavram | Tek cümle |
|---|---|
| **Key-value** | `App:Section:Key` hiyerarşik anahtar + metin değer (≈ `IConfiguration` anahtarı) |
| **Label** | Aynı anahtarın **varyantı** (`Dev`, `Prod`, `v2`…); ortam ayırmanın yolu |
| **Content type** | Değerin türü (`application/json`…); özel türler **Key Vault referansı** ve **feature flag** içindir |
| **Key Vault referansı** | Değer App Configuration'da değil, **Key Vault secret URI**'sidir; secret vault'ta kalır |
| **Sentinel key** | Değiştikçe "bütün yapılandırmayı yenile" sinyali veren tek anahtar |
| **Feature flag** | Kodu yeniden dağıtmadan özelliği aç/kapat; **filtre**lerle kısmi açma |
| **Snapshot** | Seçilen key/label kümesinin **değiştirilemez** anlık görüntüsü |

**En kritik ayrım:** *App Configuration = yapılandırma (gizli olmayan)*; *Key Vault = secret*. Hassas değer asla düz App Configuration değeri olmaz.

---

## 2. Neden?

- **Sınav:** "Hangisi nerede saklanır?", "yeniden dağıtmadan/restart etmeden yeni ayar", "sentinel key neden", "ortam ayrımı label ile", "yeni özellik kullanıcıların %20'sine" soruları doğrudan bu konudan gelir. Kod tamamlama: `.ConfigureRefresh(...)`, `Register("Sentinel", refreshAll: true)`.
- **Gerçek işte:** `appsettings.{Env}.json` dosyaları imajın/paketin içine gömülür; ayar değişikliği = yeniden build/dağıtım. 20 mikroservisin aynı ayarı farklı yerlerde tutması drift üretir. App Configuration tek kaynak + geçmiş + erişim denetimi + canlı yenileme sunar; feature flag ile **deploy ≠ release** ayrımı sağlanır.
- **Diğer konular:** Kimlik ve secret (→ [C2.11](../11-identity-key-vault/README.md)), olay tabanlı yenileme (→ [C2.10](../10-event-grid/README.md)), Container Apps ayarları (→ [C2.2](../02-container-apps/README.md)). Feature flag stratejisi ve konfigürasyon mimarisi → M9, S8.3.

---

## 3. Nasıl?

### 3.1 App Configuration vs Key Vault vs diğerleri

| Soru | **App Configuration** | **Key Vault** | `appsettings.json` / env var |
|---|---|---|---|
| Ne saklar? | Gizli olmayan ayar, feature flag | **Secret**, key, certificate | Paketle gelen varsayılanlar / platform ayarı |
| Değişince | Canlı yenileme (restart yok) | Yeni **sürüm** (rotation) | Yeniden dağıtım/restart |
| Sürüm/geçmiş | Revision geçmişi, snapshot | Secret/key sürümleri | Git |
| Erişim | RBAC: *App Configuration Data Reader/Owner* | RBAC: *Key Vault Secrets User…* | — |
| Ortam ayrımı | **Label** (veya ayrı store) | Ayrı vault/secret adı | Dosya/ortam başına |
| Throttling | Tier başına istek kotası | İşlem başına istek sınırı | — |

Birlikte kullanım: **Key Vault referansı** — App Configuration'daki anahtar, vault'taki secret URI'sine işaret eder; uygulama tek yerden (App Configuration) okur, secret değeri sağlayıcı tarafından vault'tan, uygulamanın kimliğiyle çözülür. Dolayısıyla uygulamanın MI'ı **hem** App Configuration Data Reader **hem** (referans verilen vault'ta) Key Vault Secrets User olmalıdır.

### 3.2 Anahtar tasarımı, label ve içerik türü

- **Hiyerarşi:** `App:Logging:Level`, `Db:Pool:MaxSize` — .NET'te `:` ayraç, Python/diğerlerinde `/` veya `.` kullanılabilir; sağlayıcı bölüm eşler. Servise özel önek (`Orders:…`) + `Select("Orders:*")` ile her servis yalnızca kendi anahtarlarını yükler; `TrimKeyPrefix("Orders:")` önek'i kırpar.
- **Label'lar:** Aynı anahtar `Dev` ve `Prod` etiketiyle iki kez bulunur. Sağlayıcıda `Select("App:*", labelFilter: "Prod")`. **Birden çok seçici** sırayla uygulanır, **sonraki seçici öncekinin üstüne yazar** → önce etiketsiz ortak değerler, sonra ortama özgü label (`Select("App:*")` sonra `Select("App:*", env)`).
- **Ortam ayırma stratejileri:** (1) label ile tek store (ucuz, basit; RBAC store düzeyinde → Prod'a yanlışlıkla erişim riski), (2) ortam başına **ayrı store** (sıkı yetki ayrımı, ek maliyet). Sınav "en basit" derse label, "izolasyon/yetki ayrımı" derse ayrı store.
- **Key Vault referansı** özel content type taşır: `application/vnd.microsoft.appconfig.keyvaultref+json;charset=utf-8`; değer `{"uri":"https://kv.../secrets/db-conn"}`.
- **Snapshot:** Seçilen key/label filtresinin **değiştirilemez** görüntüsü (composition type: `key` veya `key_label`). Aktif/arşiv durumu ve **saklama süresi** oluşturulurken seçilir. Kullanım: bir sürümü dondurmak, ortam ortam aynı ayar setini yaymak, geri dönmek. Sağlayıcıda `SelectSnapshot("release-1-4")`.
- **Geçmiş/geri alma:** Her değişiklik **revision** olarak tutulur (`az appconfig revision list`); belirli bir revision'daki değeri **restore** edebilirsin (tier'a göre saklama süresi değişir — doğrula). Anahtar/değerleri **kilitleyebilirsin** (`read-only`).

### 3.3 Erişim ve güvenlik

| Yol | Ne zaman? |
|---|---|
| **Entra kimliği + RBAC** (önerilen) | `App Configuration Data Reader` (okuma) · `App Configuration Data Owner` (okuma/yazma/silme) |
| Access key / connection string | Eski/basit; secret'a döner → yalnızca gerekmedikçe kullanma, `--disable-local-auth` ile kapatılabilir |
| Private endpoint | Ağ izolasyonu (Standard/Premium; Free'de yok — doğrula) |

- **Reader** uygulamaya, **Owner** yalnızca yönetim/CI hattına. Control plane `Contributor` verisini **okutmaz** (C2.11'deki ilkeyle aynı: control vs data plane).
- Tier'lar (2026 başı itibarıyla Free / Developer / Standard / Premium; kota ve özellikler değişebilir — doğrula): **Free** düşük kota (günlük istek sınırı, aşılınca **HTTP 429**, gece yarısı UTC'ye kadar), tek amaçlı geliştirme/öğrenme; **Standard** üretim (saatlik kota, soft delete, private endpoint, geo-replication seçenekleri); **Premium** kota sınırsızlığı. Dinamik yenileme **polling**'dir → her yenileme isteği kotayı yer; çok instance × kısa aralık = 429 riski.

### 3.4 .NET sağlayıcısı ve dinamik yenileme ⭐

```csharp
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

var endpoint = new Uri(builder.Configuration["AppConfig:Endpoint"]!);     // https://appcs-c212.azconfig.io
var env = builder.Environment.EnvironmentName;                            // "Dev" / "Prod" -> label
var credential = new DefaultAzureCredential();

builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(endpoint, credential)
           // shared defaults first, then environment-specific label overrides them
           .Select("App:*", LabelFilter.Null)
           .Select("App:*", env)
           .Select("Db:*", env)
           // resolve Key Vault references with the same identity
           .ConfigureKeyVault(kv => kv.SetCredential(credential))
           // dynamic refresh: watch ONE sentinel key, reload everything when it changes
           .ConfigureRefresh(refresh => refresh
               .Register("App:Sentinel", env, refreshAll: true)
               .SetRefreshInterval(TimeSpan.FromSeconds(30)))
           // feature flags (own refresh settings; default interval is 30 s)
           .UseFeatureFlags(flags => { flags.Label = env; flags.SetRefreshInterval(TimeSpan.FromSeconds(30)); });
});

builder.Services.AddAzureAppConfiguration();         // services needed by the refresh middleware
builder.Services.AddFeatureManagement();             // Microsoft.FeatureManagement.AspNetCore
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("App"));

var app = builder.Build();
app.UseAzureAppConfiguration();                      // triggers refresh checks per request

app.MapGet("/greeting", (IOptionsSnapshot<AppSettings> s) => s.Value.Greeting);   // IOptionsSnapshot/Monitor!
app.MapGet("/checkout", async (IFeatureManager fm) =>
    await fm.IsEnabledAsync("NewCheckout") ? "new checkout" : "old checkout");
app.Run();

public sealed class AppSettings { public string Greeting { get; set; } = ""; }
```

Yenileme mantığı (sınavın kalbi):

| Konu | Davranış |
|---|---|
| **Poll tabanlı** | Provider, yenileme aralığı dolduktan sonra gelen **ilk istekte** değişikliği kontrol eder; uygulama boştaysa yenileme olmaz |
| **Varsayılan aralık** | Sağlayıcıda yaklaşık **30 sn**; `SetRefreshInterval` ile değişir (çok kısa aralık kotayı yer) |
| **Sentinel key** | Yalnızca bu anahtar izlenir (ucuz: tek istek); değişince `refreshAll: true` **tüm seçili yapılandırmayı** yeniden yükler. Sentinel'siz her anahtarı ayrı izlemek pahalı ve tutarsızdır |
| **Neden en son güncellenir?** | Önce asıl değerleri değiştir, **sentinel'i en son** güncelle → uygulama yarı güncel (tutarsız) set okumaz |
| **`refreshAll: false`** | Yalnızca kayıtlı anahtar yenilenir (ör. sadece sentinel'in kendisi) — klasik yanlış |
| **Tüketici tarafı** | `IOptions<T>` (singleton) yeni değeri **görmez**; `IOptionsSnapshot<T>` (scoped) / `IOptionsMonitor<T>` gerekir; `IConfiguration` doğrudan okunursa güncel |
| **Push (olay)** | Store `Microsoft.AppConfiguration.KeyValueModified` olayını **Event Grid**'e yollar → webhook/Service Bus → uygulama `ProcessPushNotification` ile yenileme ister; aralık beklemeden, ve **kota harcamadan** (polling azalır) |
| **Key Vault secret yenileme** | Referans edilen secret'ın yeni sürümü için sağlayıcıda ayrı bir secret yenileme aralığı (`ConfigureKeyVault(kv => kv.SetSecretRefreshInterval(...))`) gerekir; sentinel secret'ı yenilemez |

> Yeni sürümlerde (provider 8.x) `Register(...)` yerine tüm seçili anahtar-değerleri izleyen `RegisterAll()` de vardır (koleksiyon izleme, sentinel'siz); sınav hâlâ **sentinel** kalıbını sorar — API detayını paket sürümünden doğrula.

### 3.5 Feature flag'ler ⭐

Feature flag, App Configuration'da `.appconfig.featureflag/<ad>` anahtarında özel content type ile saklanan JSON'dur (`enabled` + `conditions.client_filters`). Kod tarafı `Microsoft.FeatureManagement` kütüphanesidir.

| Filtre | Alias | Ne zaman? | Not |
|---|---|---|---|
| **Percentage** | `Microsoft.Percentage` | Yüzde N'i **rastgele** aç (kademeli açma, canary) | **Kullanıcı başına sabit değil**: aynı kullanıcı her istekte farklı sonuç alabilir |
| **TimeWindow** | `Microsoft.TimeWindow` | Başlangıç–bitiş penceresi (kampanya, bakım) | UTC; yalnızca Start / yalnızca End olabilir |
| **Targeting** | `Microsoft.Targeting` | **Kullanıcı/grup** hedefleme + varsayılan yüzde (ring deployment) | Kullanıcıya göre **tutarlı** karar; `ITargetingContextAccessor`/HTTP kullanıcı; `AddFeatureManagement().WithTargeting()` ile kayıt |
| Özel filtre | `IFeatureFilter` | Kendi koşulun (bölge, abonelik planı) | `[FilterAlias("...")]` |

- **Kademeli açma (canary):** Targeting ile önce `Group: Beta` (%100), sonra herkes için varsayılan yüzde %5 → %20 → %100. **Yüzde, kullanıcı tutarlılığı gerekmiyorsa Percentage; "aynı kullanıcı hep aynı deneyimi görsün" → Targeting.**
- Flag'i kodda kullanma: `IFeatureManager.IsEnabledAsync`, `[FeatureGate("NewCheckout")]` (controller/endpoint), `<feature name="…">` (Razor tag helper).
- Flag'ler de **yenilenir** (`UseFeatureFlags` kendi aralığıyla); sentinel gerekmez.
- Python tarafı: `featuremanagement` paketi (`FeatureManager`) + provider `load(..., feature_flag_enabled=True)`.

### 3.6 Python sağlayıcısı

```python
from azure.appconfiguration.provider import load, SettingSelector, WatchKey
from azure.identity import DefaultAzureCredential
from featuremanagement import FeatureManager

endpoint = "https://appcs-c212.azconfig.io"
cred = DefaultAzureCredential()
env = "Prod"

config = load(
    endpoint=endpoint,
    credential=cred,
    selects=[SettingSelector(key_filter="App:*", label_filter="\0"),   # no-label defaults
             SettingSelector(key_filter="App:*", label_filter=env),     # overrides
             SettingSelector(key_filter="Db:*", label_filter=env)],
    keyvault_credential=cred,                       # resolves Key Vault references
    refresh_on=[WatchKey("App:Sentinel", label=env)],   # sentinel
    refresh_interval=30,
    feature_flag_enabled=True,                      # load feature flags too
    feature_flag_refresh_enabled=True,
)

feature_manager = FeatureManager(config)

def handle_request():
    config.refresh()                                # poll-based: call periodically (no middleware in Python)
    print(config["App:Greeting"])
    print("new checkout" if feature_manager.is_enabled("NewCheckout") else "old checkout")
```

> Parametre adları paket sürümüne göre değişebiliyor (`feature_flag_enabled`, `refresh_on`, `refresh_interval`); kurduğun `azure-appconfiguration-provider` sürümünün README'sini kontrol et.

### 3.7 Lab — Dev/Prod label, Key Vault referansı, sentinel ile canlı yenileme, %20 feature flag

Maliyet: **Free** katman yeter (günlük istek kotası var: 30 sn polling × tek instance ≈ 2 880 istek/gün → **kotayı aşabilir**; aralığı 5 dk yap veya bu lab için Standard kullan ve **sonunda sil**). Bütçe alarmı kurulu olsun (→ C1.11). Emülatör yok; community emülatörleri resmî değildir.

```bash
RG=rg-c212-lab
LOC=westeurope
AC=appcs-c212-$RANDOM                       # globally unique
KV=kv-c212-$RANDOM
az group create --name $RG --location $LOC

# 1) Store (Free tier) + Key Vault with one secret (RBAC model, see C2.11)
az appconfig create --resource-group $RG --name $AC --location $LOC --sku Free
az keyvault create -g $RG -n $KV -l $LOC --enable-rbac-authorization true
ME=$(az ad signed-in-user show --query id -o tsv)
az role assignment create --assignee-object-id $ME --assignee-principal-type User \
  --role "App Configuration Data Owner" --scope $(az appconfig show -n $AC -g $RG --query id -o tsv)
az role assignment create --assignee-object-id $ME --assignee-principal-type User \
  --role "Key Vault Secrets Officer" --scope $(az keyvault show -n $KV --query id -o tsv)
sleep 60
az keyvault secret set --vault-name $KV --name db-conn --value "Server=prod-db;Password=s3cret" --output none

# 2) Key-values with labels (Entra auth, no access keys)
az appconfig kv set -n $AC --auth-mode login --key App:Greeting --value "Hello (dev)"  --label Dev  --yes
az appconfig kv set -n $AC --auth-mode login --key App:Greeting --value "Hello (prod)" --label Prod --yes
az appconfig kv set -n $AC --auth-mode login --key App:Sentinel --value 1 --label Prod --yes

# 3) Key Vault reference: the value stays in the vault, only the URI lives in App Configuration
az appconfig kv set-keyvault -n $AC --auth-mode login --key Db:ConnectionString --label Prod \
  --secret-identifier $(az keyvault secret show --vault-name $KV --name db-conn --query id -o tsv | sed 's#/[^/]*$##') --yes
#   (id without the version segment => always the latest secret version)

# 4) Feature flag with the Percentage filter at 20 %
az appconfig feature set -n $AC --auth-mode login --feature NewCheckout --label Prod --yes
az appconfig feature filter add -n $AC --auth-mode login --feature NewCheckout --label Prod \
  --filter-name Microsoft.Percentage --filter-parameters Value=20 --yes
az appconfig feature enable -n $AC --auth-mode login --feature NewCheckout --label Prod --yes

# 5) The app's identity: Data Reader on the store + Secrets User on the vault (least privilege)
#    (MI of the Container App / your own account for local runs)
az role assignment create --assignee-object-id <app-principal-id> --assignee-principal-type ServicePrincipal \
  --role "App Configuration Data Reader" --scope $(az appconfig show -n $AC -g $RG --query id -o tsv)
az role assignment create --assignee-object-id <app-principal-id> --assignee-principal-type ServicePrincipal \
  --role "Key Vault Secrets User" --scope $(az keyvault show -n $KV --query id -o tsv)

# 6) Live refresh without restart: change the value first, bump the sentinel LAST
az appconfig kv set -n $AC --auth-mode login --key App:Greeting --value "Hello v2 (prod)" --label Prod --yes
az appconfig kv set -n $AC --auth-mode login --key App:Sentinel --value 2 --label Prod --yes
#   curl https://<app>/greeting   -> still old until the refresh interval passes AND a request arrives; then "Hello v2 (prod)"

# 7) History + snapshot (freeze the Prod set before a release)
az appconfig revision list -n $AC --auth-mode login --key App:Greeting --label Prod -o table
az appconfig snapshot create -n $AC --auth-mode login --snapshot-name release-1-4 \
  --filters '[{"key":"App:*","label":"Prod"}]' --composition-type key_label    # Standard tier feature — Free may refuse

# 8) Cleanup (both stores have soft delete / purge rules — see C2.11; Free store has no soft delete)
az group delete --name $RG --yes --no-wait
```

Uygulama kodu 3.4 ve 3.6'daki gibidir: `AppConfig:Endpoint` ayarını store URL'sine ver, `ASPNETCORE_ENVIRONMENT=Prod` (label), uygulamayı yerelde `az login` kimliğiyle, bulutta MI ile çalıştır. `/greeting` ve `/checkout` uçlarına tekrar tekrar istek at: `/checkout` isteklerinin yaklaşık beşte biri "new checkout" döner (Percentage filtresi rastgele; kullanıcı başına sabit değil).

**Push ile anında yenileme (isteğe bağlı):**

```csharp
// Event Grid webhook endpoint: App Configuration -> Event Grid -> this endpoint
app.MapPost("/appconfig-event", async (HttpRequest req, IConfigurationRefresherProvider refreshers) =>
{
    // ReadEventAsync (not shown) parses the Event Grid event; the provider validates it and
    // schedules the refresh after a random delay so 200 instances do not hit the store at once
    foreach (var refresher in refreshers.Refreshers)
        if (refresher.TryGetPushNotification(await ReadEventAsync(req), out PushNotification push))
            refresher.ProcessPushNotification(push, TimeSpan.FromSeconds(10));
    return Results.Ok();
});
```

```bash
az eventgrid event-subscription create --name sub-appconfig --source-resource-id $(az appconfig show -n $AC -g $RG --query id -o tsv) \
  --endpoint https://<app>/appconfig-event --included-event-types Microsoft.AppConfiguration.KeyValueModified
```

> **Alıştırma.** Sentinel'i `refreshAll: false` ile kaydettin; `App:Greeting` değişti, sentinel yükseldi, ama uygulama eski karşılamayı yazıyor. Neden, ne yapmalı?
>
> <details><summary>Çözüm</summary>
>
> `refreshAll: false` yalnızca kayıtlı anahtarın (sentinel'in) kendisini yeniler; diğer anahtarlar yeniden yüklenmez. `Register("App:Sentinel", env, refreshAll: true)` yaz. Ayrıca tüketici `IOptions<T>` ise yeni değeri görmez → `IOptionsSnapshot<T>`/`IOptionsMonitor<T>`; ve refresh middleware'inin (`UseAzureAppConfiguration`) eklendiğinden ve aralığın dolduğundan emin ol.
> </details>

---

## 4. Gerçek dünya senaryosu

Bir e-ticaret şirketinin 12 servisi Container Apps'te çalışıyor; ayarlar imajların içinde `appsettings.Production.json`'da:

| İhtiyaç | Karar | Neden |
|---|---|---|
| Servislerin ortak ve ortama özel ayarları | App Configuration, **label** `Dev/Test/Prod`, servis önekli anahtarlar | Tek kaynak; yeniden build yok |
| Prod yetkisi ayrı olmalı (denetim) | Prod için **ayrı store** + ayrı RBAC | Label ile aynı store'da yetki ayrımı yapılamaz |
| DB bağlantı dizesi | Anahtar **Key Vault referansı**; secret vault'ta | Secret App Configuration'da durmaz |
| Ayar değişince restart istenmiyor | **Sentinel** + 30–60 sn aralık + `IOptionsMonitor` | Tutarlı ve ucuz yenileme |
| 200 instance'ın polling'i kotayı yiyor | **Event Grid push** + uzun poll aralığı | İstek sayısı düşer |
| Yeni ödeme akışı | Feature flag, **Targeting**: önce iç çalışanlar, sonra %5→%20→%100 | Kademeli, kullanıcı başına tutarlı |
| Kötü ayar yayına çıktı | **Revision** geçmişinden geri al / **snapshot**'a dön | Hızlı geri dönüş |
| Release öncesi ayarları dondur | Release başına **snapshot** | Değiştirilemez, yeniden üretilebilir |

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Depolama seçimi, yenileme mekanizması ve feature flag filtresi senaryoları; `ConfigureRefresh`/sentinel kod tamamlama; `az appconfig` komut tamamlama.

| Senaryodaki ifade | Cevap |
|---|---|
| "Gizli olmayan ayar/feature flag merkezi" | **App Configuration** |
| "Parola, bağlantı dizesi, API anahtarı" | **Key Vault** (App Configuration'da **Key Vault referansı**) |
| "Dev ve Prod için aynı anahtarın farklı değeri" | **Label** (veya ayrı store) |
| "Yeniden başlatmadan yapılandırmayı yenile" | `ConfigureRefresh` + **sentinel key** |
| "Birden çok anahtar tutarlı güncellensin" | Önce değerler, **sentinel en son**; `refreshAll: true` |
| "Yenileme anında, polling'siz" | **Event Grid** `KeyValueModified` → push yenileme |
| "Yeni özellik kullanıcıların %20'sine" | Feature flag + **Percentage** (tutarlılık gerekiyorsa **Targeting**) |
| "Belirli tarihler arasında açık" | **TimeWindow** filtresi |
| "Belirli kullanıcı/gruplara önce aç" | **Targeting** filtresi |
| "Bir sürümün ayarlarını değiştirilemez dondur" | **Snapshot** |
| "Uygulama yalnızca ayarları okusun" | **App Configuration Data Reader** |
| "Secret'sız erişim" | Managed identity + RBAC (access key değil) |

**Tuzaklar:**
- **Secret'ı App Configuration'a düz değer yazmak** — doğru çözüm Key Vault referansı.
- Sentinel'i `refreshAll: false` ile kaydetmek veya sentinel'i **önce** güncellemek.
- `IOptions<T>` ile "yenilenmiyor" şikâyeti; middleware (`UseAzureAppConfiguration`) eksikliği.
- Yenileme **push değil poll**'dur (Event Grid ile push eklenmedikçe) ve boşta uygulamada tetiklenmez.
- Key Vault referansı çözülmesi için uygulama kimliğinin **vault'ta** da yetkisi olmalı (App Configuration yetkisi yetmez).
- Label ayrımı **güvenlik sınırı değildir** — Prod izolasyonu için ayrı store.
- Percentage filtresi kullanıcı başına **kararlı değildir**.
- Free katman: günlük kota (429), özellik kısıtları.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Bağlantı dizesini App Configuration'a düz değer olarak yazmak | Key Vault referansı |
| Her ayarı ayrı izleyip yüzlerce istek atmak | Tek sentinel + `refreshAll: true` |
| Sentinel'i değerlerden **önce** yükseltmek | Önce değerler, sentinel son |
| `IOptions<T>` ile canlı yenileme beklemek | `IOptionsSnapshot<T>` / `IOptionsMonitor<T>` |
| `app.UseAzureAppConfiguration()` unutmak | Middleware olmadan ASP.NET Core'da refresh tetiklenmez |
| 5 sn polling × 100 instance | Aralığı yükselt / push yenileme; kotayı hesapla |
| Prod ve Dev'i aynı store'da label ile ayırıp yetkiyi aynı vermek | Ayrı store/RBAC |
| Uygulamaya `Data Owner` vermek | Okuma için `Data Reader` |
| Percentage ile "aynı kullanıcıya hep aynı deneyim" beklemek | Targeting |
| Referans verilen vault'a uygulama yetkisi vermemek | Secrets User rolü |
| Label seçicilerinin sırasını yanlış kurmak | Genel → özel (sonraki üstüne yazar) |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Kısa yenileme aralığı | Değişiklik hızlı yansır | İstek kotası/maliyet, 429 |
| **Event Grid push** + uzun poll | Anında ve ucuz yenileme | Endpoint/abonelik, altyapı karmaşıklığı |
| Sentinel (tek anahtar) | Ucuz, tutarlı | Operasyonel disiplin (sentinel'i unutma) |
| Label ile tek store | Basit, ucuz | Zayıf yetki izolasyonu |
| Ortam başına store | Güçlü izolasyon | Daha çok kaynak/maliyet |
| Key Vault referansı | Secret vault'ta kalır | İkinci bağımlılık (vault kesintisi/yetki), ek gecikme |
| Başlangıçta tümünü yükle (`Select("App:*")`) | Tek seferde okuma | Büyük store'da gereksiz anahtarlar; önek/label ile daralt |
| Feature flag'ler | Deploy ≠ release, hızlı kapatma | Flag borcu: eski flag'leri temizle; test kombinasyonları |
| Snapshot | Değiştirilemez, hızlı yükleme | Saklama süresi ve sayısı yönetimi |

---

## 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [What is Azure App Configuration?](https://learn.microsoft.com/en-us/azure/azure-app-configuration/overview) · [Key-values (keys, labels)](https://learn.microsoft.com/en-us/azure/azure-app-configuration/concept-key-value)
- [.NET configuration provider reference](https://learn.microsoft.com/en-us/azure/azure-app-configuration/reference-dotnet-provider) · [Tutorial: dynamic configuration in ASP.NET Core](https://learn.microsoft.com/en-us/azure/azure-app-configuration/enable-dynamic-configuration-aspnet-core) · [Best practices (sentinel key)](https://learn.microsoft.com/en-us/azure/azure-app-configuration/howto-best-practices)
- [Python provider reference](https://learn.microsoft.com/en-us/azure/azure-app-configuration/reference-python-provider)
- [Key Vault references](https://learn.microsoft.com/en-us/azure/azure-app-configuration/use-key-vault-references-dotnet-core) · [Snapshots](https://learn.microsoft.com/en-us/azure/azure-app-configuration/concept-snapshots)
- [Feature management overview](https://learn.microsoft.com/en-us/azure/azure-app-configuration/feature-management-overview) · [.NET feature management reference](https://learn.microsoft.com/en-us/azure/azure-app-configuration/feature-management-dotnet-reference) · [Targeting filter](https://learn.microsoft.com/en-us/azure/azure-app-configuration/howto-targetingfilter-aspnet-core)
- [Push refresh with Event Grid](https://learn.microsoft.com/en-us/azure/azure-app-configuration/concept-app-configuration-event) · [`az appconfig`](https://learn.microsoft.com/en-us/cli/azure/appconfig)
- [Limits / pricing tiers](https://azure.microsoft.com/en-us/pricing/details/app-configuration/) (kota ve katman adları değişebilir — doğrula)

> **Doğrulama notu:** Katman adları (Free / Developer / Standard / Premium), kota rakamları, revision saklama süreleri ve provider API adları (`RegisterAll`, `SetSecretRefreshInterval`, Python parametreleri) sürümle değişir; sınav öncesi güncel sayfaları kontrol et. Sınavda kavramlar (sentinel, label, Key Vault referansı, feature flag filtreleri) esastır.
