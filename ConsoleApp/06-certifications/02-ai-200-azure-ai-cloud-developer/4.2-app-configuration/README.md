[← AI-200 İçindekiler](../README.md) · [📝 Quiz](quiz.md) · [← Önceki konu](../4.1-managed-identity-key-vault/README.md)

## 4.2 Azure App Configuration

> **Sınav Alanı:** Secure, monitor, and troubleshoot Azure solutions (%20-25) · **Konu Kodu:** C2.12
>
> 📌 **Resmî hedef (skills measured as of October 7, 2026):** *Secure, monitor, and troubleshoot Azure solutions* — **Azure App Configuration** ile uygulama yapılandırmasını **sakla ve al** (key-value, label, Key Vault referansı, dinamik yenileme, feature flag). Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)'ı kontrol et.

### 1. Ne?

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

### 2. Neden?

- **Sınav:** "Hangisi nerede saklanır?", "yeniden dağıtmadan/restart etmeden yeni ayar", "sentinel key neden", "ortam ayrımı label ile", "yeni özellik kullanıcıların %20'sine" soruları doğrudan bu konudan gelir. Kod tamamlama: `.ConfigureRefresh(...)`, `Register("Sentinel", refreshAll: true)`.
- **Gerçek işte:** `appsettings.{Env}.json` dosyaları imajın/paketin içine gömülür; ayar değişikliği = yeniden build/dağıtım. 20 mikroservisin aynı ayarı farklı yerlerde tutması drift üretir. App Configuration tek kaynak + geçmiş + erişim denetimi + canlı yenileme sunar; feature flag ile **deploy ≠ release** ayrımı sağlanır.
- **Diğer konular:** Kimlik ve secret (→ [C2.11](../4.1-managed-identity-key-vault/README.md)), olay tabanlı yenileme (→ [C2.10](../3.3-event-grid/README.md)), Container Apps ayarları (→ [C2.2](../1.2-container-apps-keda/README.md)). Feature flag stratejisi ve konfigürasyon mimarisi → M9, S8.3.

---

### 3. Nasıl?

#### 3.1 App Configuration vs Key Vault vs diğerleri

- **Ne saklar?**
  - App Configuration: Gizli olmayan ayar, feature flag
  - Key Vault: **Secret**, key, certificate
  - `appsettings.json` / env var: Paketle gelen varsayılanlar / platform ayarı
- **Değişince**
  - App Configuration: Canlı yenileme (restart yok)
  - Key Vault: Yeni **sürüm** (rotation)
  - `appsettings.json` / env var: Yeniden dağıtım/restart
- **Sürüm/geçmiş** — App Configuration: Revision geçmişi, snapshot · Key Vault: Secret/key sürümleri · `appsettings.json` / env var: Git
- **Erişim**
  - App Configuration: RBAC: *App Configuration Data Reader/Owner*
  - Key Vault: RBAC: *Key Vault Secrets User…*
  - `appsettings.json` / env var: —
- **Ortam ayrımı**
  - App Configuration: **Label** (veya ayrı store)
  - Key Vault: Ayrı vault/secret adı
  - `appsettings.json` / env var: Dosya/ortam başına
- **Throttling** — App Configuration: Tier başına istek kotası · Key Vault: İşlem başına istek sınırı · `appsettings.json` / env var: —

Birlikte kullanım: **Key Vault referansı** — App Configuration'daki anahtar, vault'taki secret URI'sine işaret eder; uygulama tek yerden (App Configuration) okur, secret değeri sağlayıcı tarafından vault'tan, uygulamanın kimliğiyle çözülür. Dolayısıyla uygulamanın MI'ı **hem** App Configuration Data Reader **hem** (referans verilen vault'ta) Key Vault Secrets User olmalıdır.

#### 3.2 Anahtar tasarımı, label ve içerik türü

- **Hiyerarşi:** `App:Logging:Level`, `Db:Pool:MaxSize` — .NET'te `:` ayraç, Python/diğerlerinde `/` veya `.` kullanılabilir; sağlayıcı bölüm eşler. Servise özel önek (`Orders:…`) + `Select("Orders:*")` ile her servis yalnızca kendi anahtarlarını yükler; `TrimKeyPrefix("Orders:")` önek'i kırpar.
- **Label'lar:** Aynı anahtar `Dev` ve `Prod` etiketiyle iki kez bulunur. Sağlayıcıda `Select("App:*", labelFilter: "Prod")`. **Birden çok seçici** sırayla uygulanır, **sonraki seçici öncekinin üstüne yazar** → önce etiketsiz ortak değerler, sonra ortama özgü label (`Select("App:*")` sonra `Select("App:*", env)`).
- **Ortam ayırma stratejileri:** (1) label ile tek store (ucuz, basit; RBAC store düzeyinde → Prod'a yanlışlıkla erişim riski), (2) ortam başına **ayrı store** (sıkı yetki ayrımı, ek maliyet). Sınav "en basit" derse label, "izolasyon/yetki ayrımı" derse ayrı store.
- **Key Vault referansı** özel content type taşır: `application/vnd.microsoft.appconfig.keyvaultref+json;charset=utf-8`; değer `{"uri":"https://kv.../secrets/db-conn"}`.
- **Snapshot:** Seçilen key/label filtresinin **değiştirilemez** görüntüsü (composition type: `key` veya `key_label`). Aktif/arşiv durumu ve **saklama süresi** oluşturulurken seçilir. Kullanım: bir sürümü dondurmak, ortam ortam aynı ayar setini yaymak, geri dönmek. Sağlayıcıda `SelectSnapshot("release-1-4")`.
- **Geçmiş/geri alma:** Her değişiklik **revision** olarak tutulur (`az appconfig revision list`); belirli bir revision'daki değeri **restore** edebilirsin (tier'a göre saklama süresi değişir — doğrula). Anahtar/değerleri **kilitleyebilirsin** (`read-only`).

#### 3.3 Erişim ve güvenlik

| Yol | Ne zaman? |
|---|---|
| **Entra kimliği + RBAC** (önerilen) | `App Configuration Data Reader` (okuma) · `App Configuration Data Owner` (okuma/yazma/silme) |
| Access key / connection string | Eski/basit; secret'a döner → yalnızca gerekmedikçe kullanma, `--disable-local-auth` ile kapatılabilir |
| Private endpoint | Ağ izolasyonu (Standard/Premium; Free'de yok — doğrula) |

- **Reader** uygulamaya, **Owner** yalnızca yönetim/CI hattına. Control plane `Contributor` verisini **okutmaz** (C2.11'deki ilkeyle aynı: control vs data plane).
- Tier'lar (2026 başı itibarıyla Free / Developer / Standard / Premium; kota ve özellikler değişebilir — doğrula): **Free** düşük kota (günlük istek sınırı, aşılınca **HTTP 429**, gece yarısı UTC'ye kadar), tek amaçlı geliştirme/öğrenme; **Standard** üretim (saatlik kota, soft delete, private endpoint, geo-replication seçenekleri); **Premium** kota sınırsızlığı. Dinamik yenileme **polling**'dir → her yenileme isteği kotayı yer; çok instance × kısa aralık = 429 riski.

#### 3.4 .NET sağlayıcısı ve dinamik yenileme ⭐

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

#### 3.5 Feature flag'ler ⭐

Feature flag, App Configuration'da `.appconfig.featureflag/<ad>` anahtarında özel content type ile saklanan JSON'dur (`enabled` + `conditions.client_filters`). Kod tarafı `Microsoft.FeatureManagement` kütüphanesidir.

- **Percentage**
  - Alias: `Microsoft.Percentage`
  - Ne zaman?: Yüzde N'i **rastgele** aç (kademeli açma, canary)
  - Not: **Kullanıcı başına sabit değil**: aynı kullanıcı her istekte farklı sonuç alabilir
- **TimeWindow**
  - Alias: `Microsoft.TimeWindow`
  - Ne zaman?: Başlangıç–bitiş penceresi (kampanya, bakım)
  - Not: UTC; yalnızca Start / yalnızca End olabilir
- **Targeting**
  - Alias: `Microsoft.Targeting`
  - Ne zaman?: **Kullanıcı/grup** hedefleme + varsayılan yüzde (ring deployment)
  - Not: Kullanıcıya göre **tutarlı** karar; `ITargetingContextAccessor`/HTTP kullanıcı; `AddFeatureManagement().WithTargeting()` ile kayıt
- **Özel filtre**
  - Alias: `IFeatureFilter`
  - Ne zaman?: Kendi koşulun (bölge, abonelik planı)
  - Not: `[FilterAlias("...")]`

- **Kademeli açma (canary):** Targeting ile önce `Group: Beta` (%100), sonra herkes için varsayılan yüzde %5 → %20 → %100. **Yüzde, kullanıcı tutarlılığı gerekmiyorsa Percentage; "aynı kullanıcı hep aynı deneyimi görsün" → Targeting.**
- Flag'i kodda kullanma: `IFeatureManager.IsEnabledAsync`, `[FeatureGate("NewCheckout")]` (controller/endpoint), `<feature name="…">` (Razor tag helper).
- Flag'ler de **yenilenir** (`UseFeatureFlags` kendi aralığıyla); sentinel gerekmez.
- Python tarafı: `featuremanagement` paketi (`FeatureManager`) + provider `load(..., feature_flag_enabled=True)`.

#### 3.6 Python sağlayıcısı

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

#### 3.7 Lab — Dev/Prod label, Key Vault referansı, sentinel ile canlı yenileme, %20 feature flag

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

### 4. Gerçek dünya senaryosu

Bir e-ticaret şirketinin 12 servisi Container Apps'te çalışıyor; ayarlar imajların içinde `appsettings.Production.json`'da:

- **Servislerin ortak ve ortama özel ayarları**
  - Karar: App Configuration, **label** `Dev/Test/Prod`, servis önekli anahtarlar
  - Neden: Tek kaynak; yeniden build yok
- **Prod yetkisi ayrı olmalı (denetim)**
  - Karar: Prod için **ayrı store** + ayrı RBAC
  - Neden: Label ile aynı store'da yetki ayrımı yapılamaz
- **DB bağlantı dizesi**
  - Karar: Anahtar **Key Vault referansı**; secret vault'ta
  - Neden: Secret App Configuration'da durmaz
- **Ayar değişince restart istenmiyor**
  - Karar: **Sentinel** + 30–60 sn aralık + `IOptionsMonitor`
  - Neden: Tutarlı ve ucuz yenileme
- **200 instance'ın polling'i kotayı yiyor**
  - Karar: **Event Grid push** + uzun poll aralığı
  - Neden: İstek sayısı düşer
- **Yeni ödeme akışı**
  - Karar: Feature flag, **Targeting**: önce iç çalışanlar, sonra %5→%20→%100
  - Neden: Kademeli, kullanıcı başına tutarlı
- **Kötü ayar yayına çıktı**
  - Karar: **Revision** geçmişinden geri al / **snapshot**'a dön
  - Neden: Hızlı geri dönüş
- **Release öncesi ayarları dondur**
  - Karar: Release başına **snapshot**
  - Neden: Değiştirilemez, yeniden üretilebilir

---

### 5. Sınav perspektifi

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

### 6. Yaygın hatalar

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

### 7. Performans & trade-off

- **Kısa yenileme aralığı** — Kazanç: Değişiklik hızlı yansır · Bedel: İstek kotası/maliyet, 429
- **Event Grid push** + uzun poll
  - Kazanç: Anında ve ucuz yenileme
  - Bedel: Endpoint/abonelik, altyapı karmaşıklığı
- **Sentinel (tek anahtar)**
  - Kazanç: Ucuz, tutarlı
  - Bedel: Operasyonel disiplin (sentinel'i unutma)
- **Label ile tek store** — Kazanç: Basit, ucuz · Bedel: Zayıf yetki izolasyonu
- **Ortam başına store** — Kazanç: Güçlü izolasyon · Bedel: Daha çok kaynak/maliyet
- **Key Vault referansı**
  - Kazanç: Secret vault'ta kalır
  - Bedel: İkinci bağımlılık (vault kesintisi/yetki), ek gecikme
- **Başlangıçta tümünü yükle (`Select("App:*")`)**
  - Kazanç: Tek seferde okuma
  - Bedel: Büyük store'da gereksiz anahtarlar; önek/label ile daralt
- **Feature flag'ler**
  - Kazanç: Deploy ≠ release, hızlı kapatma
  - Bedel: Flag borcu: eski flag'leri temizle; test kombinasyonları
- **Snapshot**
  - Kazanç: Değiştirilemez, hızlı yükleme
  - Bedel: Saklama süresi ve sayısı yönetimi

---

### 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [What is Azure App Configuration?](https://learn.microsoft.com/en-us/azure/azure-app-configuration/overview) · [Key-values (keys, labels)](https://learn.microsoft.com/en-us/azure/azure-app-configuration/concept-key-value)
- [.NET configuration provider reference](https://learn.microsoft.com/en-us/azure/azure-app-configuration/reference-dotnet-provider) · [Tutorial: dynamic configuration in ASP.NET Core](https://learn.microsoft.com/en-us/azure/azure-app-configuration/enable-dynamic-configuration-aspnet-core) · [Best practices (sentinel key)](https://learn.microsoft.com/en-us/azure/azure-app-configuration/howto-best-practices)
- [Python provider reference](https://learn.microsoft.com/en-us/azure/azure-app-configuration/reference-python-provider)
- [Key Vault references](https://learn.microsoft.com/en-us/azure/azure-app-configuration/use-key-vault-references-dotnet-core) · [Snapshots](https://learn.microsoft.com/en-us/azure/azure-app-configuration/concept-snapshots)
- [Feature management overview](https://learn.microsoft.com/en-us/azure/azure-app-configuration/feature-management-overview) · [.NET feature management reference](https://learn.microsoft.com/en-us/azure/azure-app-configuration/feature-management-dotnet-reference) · [Targeting filter](https://learn.microsoft.com/en-us/azure/azure-app-configuration/howto-targetingfilter-aspnet-core)
- [Push refresh with Event Grid](https://learn.microsoft.com/en-us/azure/azure-app-configuration/concept-app-configuration-event) · [`az appconfig`](https://learn.microsoft.com/en-us/cli/azure/appconfig)
- [Limits / pricing tiers](https://azure.microsoft.com/en-us/pricing/details/app-configuration/) (kota ve katman adları değişebilir — doğrula)

> **Doğrulama notu:** Katman adları (Free / Developer / Standard / Premium), kota rakamları, revision saklama süreleri ve provider API adları (`RegisterAll`, `SetSecretRefreshInterval`, Python parametreleri) sürümle değişir; sınav öncesi güncel sayfaları kontrol et. Sınavda kavramlar (sentinel, label, Key Vault referansı, feature flag filtreleri) esastır.

---

➡️ **Sıradaki:** [📝 Bu konunun quiz soruları](quiz.md)
