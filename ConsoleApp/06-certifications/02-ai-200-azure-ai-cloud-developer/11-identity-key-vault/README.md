# C2.11 Managed Identity ve Azure Key Vault

> 📁 `ConsoleApp/06-certifications/02-ai-200-azure-ai-cloud-developer/11-identity-key-vault/` · Sınav alanı: **Secure, monitor, and troubleshoot Azure solutions (%20-25)** · Önkoşul: C1.9 (Entra ID, RBAC), M9 (kimlik), C2.2 (Container Apps) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of October 7, 2026):** *Secure, monitor, and troubleshoot Azure solutions* — **Azure Key Vault ile secret'ları güvenceye al** (**rotation** ve **retrieval** dahil). Managed identity bu hedefin sınavdaki sessiz ön koşuludur: neredeyse her senaryo "secret'sız" bağlanmayı bekler. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)'ı kontrol et.

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

İki parça, tek hedef: **koda ve yapılandırmaya hiç secret koymamak.**

- **Managed identity (MI):** Azure kaynağına (VM, App Service, Functions, Container Apps, AKS pod'u…) Microsoft Entra ID'de **otomatik yönetilen bir servis kimliği** verir. Parola/sertifika yoktur; token'ı platform (IMDS/identity endpoint) üretir ve döndürür. Kimlik Azure RBAC ile yetkilendirilir (→ C1.9).
- **Azure Key Vault:** secret (parola, bağlantı dizesi, API anahtarı), **key** (şifreleme anahtarı; HSM destekli olabilir) ve **certificate** nesnelerini saklayan yönetilen kasa. Erişim denetimli, sürümlü, denetim kaydı (audit log) tutan, soft delete ile korunan bir servistir.

| Kavram | Tek cümle |
|---|---|
| **System-assigned MI** | Kaynağa bağlı; kaynak silinince kimlik de silinir; bir kaynak = bir kimlik |
| **User-assigned MI** | Bağımsız Azure kaynağı; **birden çok kaynağa** atanabilir, yaşam döngüsü ayrı |
| **`DefaultAzureCredential`** | Credential **zinciri**: env → workload identity → managed identity → VS/VS Code/CLI/PowerShell… ilk çalışanı kullanır |
| **Data plane vs control plane** | Vault'u yönetmek (control: `Contributor`) ≠ içindeki secret'ı okumak (data: `Key Vault Secrets User`) |
| **Rotation** | Secret'ın düzenli olarak **yeni sürümle** değiştirilmesi; uygulama yeni sürümü yenilemeyle alır |
| **Soft delete / purge protection** | Silineni kurtarma penceresi / bu pencerede kalıcı silmeyi **engelleme** |

Bu konu sınavın üç sorusunu cevaplar: *Hangi kimlik türü? Hangi rol? Rotation'ı ne tetikler, uygulama yeni değeri nasıl alır?*

---

## 2. Neden?

- **Sınav:** Hedef doğrudan "secret'ları Key Vault ile güvenceye al (rotation + retrieval)" der. Sorular senaryo + kod tamamlama olur: *"En az yetkiyle uygulamanın secret okumasını sağla"*, *"near-expiry olayı ile rotation'ı otomatikleştir"*, `SecretClient`/`DefaultAzureCredential` boşluk doldurma.
- **Gerçek işte:** Sızan bağlantı dizesi en yaygın bulut ihlali nedenlerindendir. Repo'daki, pipeline değişkenindeki, `appsettings.json`'daki secret'lar kopyalanır, eskir, rotasyona girmez. MI + Key Vault ile **saklanacak secret sayısı azalır**, kalan secret tek yerde, denetlenebilir ve döndürülebilir olur.
- **Diğer konularla bağ:** Container Apps secret'ları (→ [C2.2](../02-container-apps/README.md)), Functions bağlantıları (→ [C2.8](../08-azure-functions/README.md)), Cosmos/PostgreSQL/Service Bus'a Entra kimliğiyle bağlanma ve App Configuration'daki Key Vault referansları (→ [C2.12](../12-app-configuration/README.md)) bu konunun üstüne kurulur. Kimlik derinliği → M9, secret yönetimi mimarisi → S6/M9.

---

## 3. Nasıl?

### 3.1 Managed identity türleri

| | **System-assigned** | **User-assigned** |
|---|---|---|
| Oluşturma | Kaynakta açılır (`--system-assigned`) | Ayrı kaynak olarak oluşturulur, kaynağa **bağlanır** |
| Yaşam döngüsü | Kaynakla **birlikte** silinir | Bağımsız; kaynak silinse de kalır |
| Paylaşım | Hayır (kaynağa özel) | **Evet** — aynı kimlik N kaynakta |
| Ne zaman? | Tek kaynak, basit, en az yönetim | Birden çok kaynak aynı yetkiyi paylaşır (ör. 3 Function + 1 API); **yetkiyi dağıtımdan önce hazırlamak** gerekir; kaynak sık yeniden oluşturulur; Key Vault referansı için önceden tanımlı kimlik |
| Tuzak | Kaynağı silip yeniden oluşturunca principal ID değişir → rol atamaları **kaybolur** | Bir kaynağa **birden çok** user-assigned atanabilir → hangisini kullanacağını (`client_id`) belirtmelisin |

> **Çoklu kimlik tuzağı:** Bir kaynakta hem system- hem user-assigned veya birden çok user-assigned varsa `DefaultAzureCredential()` hangisini seçeceğini bilemez; `AZURE_CLIENT_ID` ortam değişkeni veya `ManagedIdentityClientId` ile **belirt**.

**Workload identity federation** (→ M9): MI'nın dışında çalışan iş yükü (AKS pod'u, GitHub Actions, başka bulut) için **federated credential**: dış IdP'nin token'ı Entra token'ıyla değiştirilir; secret yok. AKS'te `WorkloadIdentityCredential` (service account ↔ user-assigned MI), GitHub Actions'ta OIDC ile `azure/login`.

### 3.2 `DefaultAzureCredential` ve credential seçimi

```csharp
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var vaultUri = new Uri("https://kv-c211-demo.vault.azure.net/");

// Local dev + simple apps: the chain tries env vars, workload identity, managed identity,
// Visual Studio / VS Code / Azure CLI / Azure PowerShell ... in order.
var client = new SecretClient(vaultUri, new DefaultAzureCredential());

// Several user-assigned identities on one resource: say which one
var cred = new DefaultAzureCredential(new DefaultAzureCredentialOptions
{
    ManagedIdentityClientId = "<user-assigned-client-id>"
});

// Production, no ambiguity and no probing latency: use the specific credential
var prod = new SecretClient(vaultUri,
    new ManagedIdentityCredential(ManagedIdentityId.FromUserAssignedClientId("<client-id>")));

// Deterministic "prod = MI, laptop = az login" without the whole chain
var chained = new ChainedTokenCredential(
    new ManagedIdentityCredential(),
    new AzureCliCredential());
```

- Zincirin her adımı denenir; **başarısızlıklar yavaşlık ve belirsizlik** getirir, yanlış bir kimlik (ör. eski bir `AZURE_*` env değişkeni) sessizce seçilebilir. Bu yüzden Microsoft üretimde **belirli credential** (`ManagedIdentityCredential`, `WorkloadIdentityCredential`) veya `ChainedTokenCredential` önerir; `DefaultAzureCredential` geliştirme kolaylığıdır.
- Yerelde aynı kod, **geliştirici kimliğiyle** (`az login` / VS) çalışır; geliştiriciye vault'ta ilgili rol verilir (geliştirme vault'u).

### 3.3 Key Vault nesneleri, sürümler ve izin modeli

| Nesne | Ne? | Not |
|---|---|---|
| **Secret** | Rastgele bayt/metin (≤ 25 KB) | Her `set` **yeni sürüm** üretir; `.../secrets/<ad>` = **en son sürüm**, `.../secrets/<ad>/<version>` = sabit sürüm |
| **Key** | RSA/EC anahtarı; şifrele/imzala/sar işlemleri **vault'ta** yapılır (anahtar dışarı çıkmaz) | **Rotation policy** ile otomatik döndürülebilir; Premium'da HSM destekli |
| **Certificate** | X.509 + yönetilen yaşam döngüsü (yenileme, CA entegrasyonu) | Arkada bir key + bir secret üretir; auto-renew **sertifikalar** için yerleşiktir |

**İzin modeli — Azure RBAC (önerilen) vs access policy (eski):**

| | **Azure RBAC** | **Access policy** |
|---|---|---|
| Yetki kapsamı | Management group → subscription → RG → **vault** → *tek secret* | Yalnızca **vault** düzeyi (tüm secret'lar) |
| Denetim | Azure RBAC + PIM + Azure Policy + atama geçmişi | Vault'a özel, ayrı yönetim |
| Durum | **Önerilen**; yeni vault'larda varsayılan (control plane API `2026-02-01` ve sonrasıyla oluşturulanlarda; mevcut vault'lar değişmez) | Eski model; ileri geçiş için migrasyon önerilir |

Data plane rolleri (en az yetki sırasıyla):

| Rol | Yapabildiği | Tipik sahibi |
|---|---|---|
| **Key Vault Secrets User** | Secret **değerini okur** (get/list) | **Uygulamanın MI'ı** ⭐ |
| **Key Vault Secrets Officer** | Secret oluştur/güncelle/sil (okuma dahil) | Rotation Function'ı, dağıtım hattı |
| **Key Vault Reader** | Vault ve nesne **meta verisi** (değer yok) | Envanter/denetim |
| **Key Vault Crypto User** | Key ile şifrele/çöz/imzala | Şifreleme yapan uygulama |
| **Key Vault Certificates Officer** | Sertifika yönetimi | PKI sahibi |
| **Key Vault Administrator** | Tüm data plane | Yalnızca gerektiğinde (yönetici) |

> **Büyük tuzak:** `Owner`/`Contributor` rolleri **control plane**'dir (vault'u oluşturur/siler/ayarlar); RBAC modelinde **secret değerini okutmaz**. Secret'ı okumak için data plane rolü gerekir. (Eski access policy modelinde vault üzerinde `Contributor` olan biri access policy ekleyip **kendine** veri erişimi verebilirdi; RBAC'ta rol atama yetkisi ayrı olduğu için bu yol kapanır.) Ayrıca RBAC rol atamasının yayılması **birkaç dakika** sürebilir.

### 3.4 Retrieval — secret'ı uygulamaya almanın yolları ⭐

| Yol | Nasıl? | Yenileme | Ne zaman? |
|---|---|---|---|
| **SDK** — `SecretClient.GetSecretAsync` | Kod her ihtiyaçta/başlangıçta çağırır | Sen yönetirsin (önbellek TTL) | Tam kontrol, az sayıda secret |
| **Configuration sağlayıcı** — `AddAzureKeyVault` | Secret'lar `IConfiguration` anahtarlarına dönüşür (`--` → `:`) | `ReloadInterval` ile periyodik | ASP.NET Core/worker; kod değişmeden |
| **App Service / Functions Key Vault referansı** | App setting: `@Microsoft.KeyVault(SecretUri=https://…/secrets/db-conn/)` | Sürümsüz referans platform tarafından **yaklaşık 24 saatte** bir yenilenir (SLA yok); ayar değişimi/restart anında yeniler | Kodsuz; ortam değişkeni bekleyen uygulamalar |
| **Container Apps secret** — `keyvaultref` | `az containerapp secret set --secrets "db=keyvaultref:<uri>,identityref:<id>"`; env var `secretref:db` | Sürümsüz URI'da yeni sürümler yaklaşık **30 dakikada** bir alınır ve ilgili revision restart edilir (dokümana göre — doğrula) | Container Apps (→ C2.2) |
| **App Configuration → Key Vault referansı** | Anahtar App Config'te, değeri vault'ta (→ C2.12) | Sağlayıcının secret yenilemesi | Yapılandırma + secret birlikte |

- **Sürümsüz URI ⇒ en son sürüm**; sürümlü URI ⇒ **sabitlenmiş**, rotation asla etki etmez. Rotation istiyorsan sürümsüz kullan.
- **Önbellek:** Her istekte vault'a gitme (vault **throttling**'i: anahtar ve vault bazında istek sınırı; HTTP 429). Başlangıçta yükle + periyodik yenile.
- `Azure.Extensions.AspNetCore.Configuration.Secrets`:

```csharp
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureKeyVault(
    new Uri(builder.Configuration["KeyVault:Uri"]!),
    new DefaultAzureCredential(),
    new AzureKeyVaultConfigurationOptions
    {
        // re-poll the vault; picks up new secret versions without a restart
        ReloadInterval = TimeSpan.FromMinutes(5)
    });

// secret "Db--ConnectionString" in the vault  ->  config key "Db:ConnectionString"
// IOptionsMonitor<T> / IOptionsSnapshot<T> see the new value; IOptions<T> (singleton) does NOT.
```

### 3.5 Rotation ⭐

**Secret'lar için Key Vault yerleşik otomatik değiştirme yapmaz** (sertifika auto-renew ve **key** rotation policy hariç); secret'ın *değeri* bir servisin parolası/anahtarı olduğundan yeni değeri **hedef serviste de** üretmek gerekir. Kalıp:

```
secret expiry (--expires)  ──►  Key Vault  ──►  Event Grid: Microsoft.KeyVault.SecretNearExpiry
                                                          │  (varsayılan ≈ 30 gün önce)
                                                          ▼
                                    Azure Function (MI = Secrets Officer + hedef serviste yetki)
                                      1. hedef serviste YENİ anahtar üret (ikincil anahtar/ikinci kimlik bilgisi)
                                      2. vault'a YENİ SÜRÜM yaz  (az keyvault secret set / SecretClient.SetSecretAsync)
                                                          ▼
                              Uygulama: sürümsüz referans + yenileme ile yeni sürümü alır
                                                          ▼
                              (eski anahtar bir sonraki turda yenilenir — iki anahtar kalıbı)
```

- **Olaylar:** `Microsoft.KeyVault.SecretNearExpiry`, `SecretExpired`, `SecretNewVersionCreated` (key ve certificate için benzerleri). Abonelik kaynağı **vault**'tur; handler: Function, Logic App, webhook, Service Bus… (→ [C2.10](../10-event-grid/README.md)). Event Grid **at-least-once** teslim eder → rotation handler'ı **idempotent** olmalı.
- **Döngü tuzağı:** Function yeni sürüm yazınca `SecretNewVersionCreated` doğar; yalnızca `SecretNearExpiry`/`SecretExpired`'a abone ol (olay türü filtresi) ya da handler'da kendi yazdığın sürümü yoksay.
- **İki kimlik bilgisi (dual credentials) kalıbı:** Hedef servis iki anahtar sunuyorsa (Storage key1/key2, Service Bus primary/secondary) uygulama bir anahtarı kullanırken **diğerini** döndür, vault'a yaz; uygulama yenileyince geçer; sonraki turda ilkini döndür. Kesintisiz rotation budur.
- **Key rotation policy:** Key'ler için vault'ta **otomatik** döndürme politikası (`az keyvault key rotation-policy update`): süre, `Rotate`/`Notify` eylemi, `expiryTime`. Yeni key sürümü oluşur; key'i kullanan servis (ör. CMK) sürümsüz URI ile yeni sürüme geçer.
- Rotation sonrası **uygulamanın yeni değeri alması** ayrı bir sorudur: SDK önbelleği, `ReloadInterval`, platform referans yenileme aralığı, `IOptions` vs `IOptionsMonitor`. En iyi çözüm **secret'ı hiç kullanmamak**: Entra kimliğiyle bağlanılan servislerde (Cosmos, Service Bus, Storage, PostgreSQL) rotation sorunu ortadan kalkar.

### 3.6 Koruma: soft delete, purge protection, ağ

| Özellik | Davranış |
|---|---|
| **Soft delete** | Silinen vault/nesne **7–90 gün** (varsayılan 90) kurtarılabilir; yeni vault'larda **her zaman açık** (kapatılamaz); süre vault oluşturulurken seçilir, sonra değişmez |
| **Purge protection** | Açıksa soft-deleted nesne retention bitmeden **kimse (Microsoft dahil) kalıcı silemez**; bir kez açılınca **kapatılamaz**; CMK senaryoları için gereklidir |
| **Ağ** | Firewall (IP/VNet kuralı) veya **private endpoint** (`privatelink.vaultcore.azure.net`) + public erişimi kapat; "trusted Microsoft services" istisnası |
| **Denetim** | Diagnostic settings → Log Analytics (`AzureDiagnostics`/`AZKVAuditLogs`); kim hangi secret'ı ne zaman okudu (→ [C2.13](../13-opentelemetry-kql/README.md)) |

### 3.7 Lab — MI ile secret oku, rotation yap, near-expiry olayını işle

Maliyet: Key Vault Standard işlem başına ücretlidir (kuruşlar); Function Consumption/Flex, Event Grid ilk milyon işlem/ay ücretsiz katmana yakındır. **Bütçe alarmı** kurulu olsun (→ C1.11). Emülatör yoktur; vault'u gerçek Azure'da kurarız.

```bash
RG=rg-c211-lab
LOC=westeurope
KV=kv-c211-$RANDOM                       # 3-24 chars, globally unique
az group create --name $RG --location $LOC

# 1) Vault: RBAC model + purge protection (retention 7 days = shortest recovery window)
az keyvault create --resource-group $RG --name $KV --location $LOC \
  --enable-rbac-authorization true --enable-purge-protection true --retention-days 7
KV_ID=$(az keyvault show --name $KV --query id -o tsv)

# 2) Under RBAC even the Owner cannot write secrets: grant yourself Secrets Officer (data plane)
ME=$(az ad signed-in-user show --query id -o tsv)
az role assignment create --assignee-object-id $ME --assignee-principal-type User \
  --role "Key Vault Secrets Officer" --scope $KV_ID
sleep 60                                  # role assignment propagation

# 3) Secret with an expiry date (needed for near-expiry events)
az keyvault secret set --vault-name $KV --name Db--ConnectionString \
  --value "Server=demo;Password=v1" --expires $(date -u -d '+45 days' +%Y-%m-%dT%H:%MZ)

# 4) App identity: user-assigned MI + least-privilege role (Secrets User = read only)
az identity create --resource-group $RG --name id-c211-api
MI_PRINCIPAL=$(az identity show -g $RG -n id-c211-api --query principalId -o tsv)
MI_CLIENT=$(az identity show -g $RG -n id-c211-api --query clientId -o tsv)
MI_ID=$(az identity show -g $RG -n id-c211-api --query id -o tsv)
az role assignment create --assignee-object-id $MI_PRINCIPAL --assignee-principal-type ServicePrincipal \
  --role "Key Vault Secrets User" --scope $KV_ID

# 5) Run the API on Container Apps with the identity; secret comes from Key Vault by reference
#    (image: any API that reads DB_CONN; see C2.2 for the environment/registry parts)
az containerapp create -g $RG -n api-c211 --environment <your-aca-environment> \
  --image <your-image> --user-assigned $MI_ID \
  --env-vars KeyVault__Uri=https://$KV.vault.azure.net/ AZURE_CLIENT_ID=$MI_CLIENT
az containerapp secret set -g $RG -n api-c211 \
  --secrets "db-conn=keyvaultref:https://$KV.vault.azure.net/secrets/Db--ConnectionString,identityref:$MI_ID"
az containerapp update -g $RG -n api-c211 --set-env-vars "DB_CONN=secretref:db-conn"

# 6) Rotation: a new version is just another "set"; unversioned readers see it
az keyvault secret set --vault-name $KV --name Db--ConnectionString \
  --value "Server=demo;Password=v2" --expires $(date -u -d '+90 days' +%Y-%m-%dT%H:%MZ)
az keyvault secret list-versions --vault-name $KV --name Db--ConnectionString \
  --query "[].{version:id, enabled:attributes.enabled, expires:attributes.expires}" -o table

# 7) Event Grid: near-expiry events from the vault -> rotation Function (isolated worker, C2.8)
FUNC_ID=$(az functionapp show -g $RG -n func-c211-rotate --query id -o tsv)
az eventgrid event-subscription create --name sub-near-expiry --source-resource-id $KV_ID \
  --endpoint-type azurefunction --endpoint $FUNC_ID/functions/RotateSecret \
  --included-event-types Microsoft.KeyVault.SecretNearExpiry

# 8) Key rotation policy (keys, not secrets): rotate every 90 days
az keyvault key create --vault-name $KV --name cmk-demo --kty RSA --size 2048
az keyvault key rotation-policy update --vault-name $KV --name cmk-demo \
  --value '{"lifetimeActions":[{"trigger":{"timeAfterCreate":"P90D"},"action":{"type":"Rotate"}}],"attributes":{"expiryTime":"P1Y"}}'

# 9) Cleanup. Purge protection keeps the deleted vault (and its name) for the retention period.
az group delete --name $RG --yes --no-wait
# az keyvault purge --name $KV   # impossible while purge protection is on; the name is blocked until retention ends
```

**C# — API'de okuma (yerelde `az login`, bulutta MI; aynı kod):**

```csharp
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(_ => new SecretClient(
    new Uri(builder.Configuration["KeyVault:Uri"]!),
    new DefaultAzureCredential()));            // AZURE_CLIENT_ID picks the user-assigned identity

var app = builder.Build();

app.MapGet("/db-version", async (SecretClient secrets, CancellationToken ct) =>
{
    // latest version; Value is never logged
    KeyVaultSecret s = await secrets.GetSecretAsync("Db--ConnectionString", cancellationToken: ct);
    return Results.Ok(new { s.Name, version = s.Properties.Version, expires = s.Properties.ExpiresOn });
});

app.Run();
```

**C# — Rotation Function (isolated worker, Event Grid trigger):**

```csharp
using Azure.Identity;
using Azure.Messaging.EventGrid;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Functions.Worker;

public sealed class RotateSecret(ILogger<RotateSecret> log)
{
    private static readonly SecretClient Vault =
        new(new Uri(Environment.GetEnvironmentVariable("KeyVault__Uri")!), new DefaultAzureCredential());

    [Function("RotateSecret")]
    public async Task Run([EventGridTrigger] EventGridEvent ev, CancellationToken ct)
    {
        // ev.EventType == "Microsoft.KeyVault.SecretNearExpiry"; ev.Subject == secret name
        var name = ev.Subject;
        log.LogInformation("Rotating {Secret}", name);

        string newValue = await CreateNewCredentialInTargetServiceAsync(name, ct); // idempotent!
        var s = new KeyVaultSecret(name, newValue);
        s.Properties.ExpiresOn = DateTimeOffset.UtcNow.AddDays(90);
        await Vault.SetSecretAsync(s, ct);      // new version; consumers refresh and pick it up
    }

    private static Task<string> CreateNewCredentialInTargetServiceAsync(string name, CancellationToken ct)
        => Task.FromResult(Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(32)));
}
```

**Python eşdeğeri (`azure-identity`, `azure-keyvault-secrets`):**

```python
from datetime import datetime, timedelta, timezone
from azure.identity import DefaultAzureCredential, ManagedIdentityCredential
from azure.keyvault.secrets import SecretClient

vault_url = "https://kv-c211-demo.vault.azure.net/"

# local: az login; cloud: managed identity. For a specific user-assigned identity:
credential = DefaultAzureCredential(managed_identity_client_id="<client-id>")
# production, explicit: credential = ManagedIdentityCredential(client_id="<client-id>")

client = SecretClient(vault_url=vault_url, credential=credential)

secret = client.get_secret("Db--ConnectionString")           # latest version
print(secret.name, secret.properties.version, secret.properties.expires_on)

# rotation: setting the same name creates a NEW version
client.set_secret(
    "Db--ConnectionString",
    "Server=demo;Password=v3",
    expires_on=datetime.now(timezone.utc) + timedelta(days=90),
)
for p in client.list_properties_of_secret_versions("Db--ConnectionString"):
    print(p.version, p.enabled, p.expires_on)
```

> **Alıştırma.** Uygulama `Key Vault Contributor` rolüyle `GetSecretAsync` çağırıyor ve 403 alıyor. (a) Neden? (b) En az yetkili çözüm ne?
>
> <details><summary>Çözüm</summary>
>
> (a) `Key Vault Contributor` **control plane** rolüdür (vault yönetimi); data plane'de secret okuma izni içermez. (b) Uygulamanın MI'ına vault (veya tek secret) kapsamında **Key Vault Secrets User** ata; `Contributor` kaldırılabilir. Rol yayılımı için birkaç dakika bekle.
> </details>

---

## 4. Gerçek dünya senaryosu

Bir sipariş platformunun API'si (Container Apps), 3 worker Function'ı ve PostgreSQL + Service Bus + Cosmos bağımlılığı var:

| Sorun | Karar | Neden |
|---|---|---|
| Bağlantı dizeleri `appsettings.Production.json`'da, CI değişkenlerinde | Cosmos/Service Bus/Storage için **Entra kimliği (MI)**, secret yok; PostgreSQL parolası **Key Vault** | Saklanacak secret sayısı en aza iner |
| 3 Function + API aynı vault'tan okuyor | **Tek user-assigned MI**, vault'ta **Secrets User** | Rol ataması bir kez; kaynaklar yeniden oluşturulsa kimlik sabit |
| Secret'ı okuyup rotation yapan hat | Ayrı MI, **Secrets Officer** (yalnızca rotation Function'ında) | Uygulama okur ama yazamaz — en az yetki |
| DB parolası 90 günde bir değişmeli | `--expires` + **Event Grid SecretNearExpiry → Function**; PostgreSQL'de ikinci rol/parola, vault'a yeni sürüm | Kesintisiz iki-kimlik bilgisi kalıbı |
| Yeni parola uygulamaya ne zaman ulaşır? | Sürümsüz referans + `ReloadInterval` (5 dk) + `IOptionsMonitor` | Restart gerektirmez; pinned sürüm kullanılmaz |
| Yanlışlıkla silinen vault | **Soft delete** (90 gün) + **purge protection** | Kurtarılabilir; kötü niyetli kalıcı silme engelli |
| Vault internetten erişilebilir | **Private endpoint** + public erişim kapalı | Secret yalnızca VNet'ten |
| Geliştiriciler yerelde çalışıyor | Dev vault + geliştirici kimliğine **Secrets User**; aynı `DefaultAzureCredential` kodu | Prod'a özgü kod dalı yok |

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → doğru kimlik/rol/mekanizma; kod boşluğu (`new SecretClient(uri, new ____())`); `az` boşluğu (`--enable-rbac-authorization`); rotation akışını sıralama.

| Senaryodaki ifade | Cevap |
|---|---|
| "Parola/anahtar saklamadan servise bağlan", "secret'sız" | **Managed identity** |
| "Birden çok kaynak aynı kimliği/yetkiyi paylaşsın" | **User-assigned** MI |
| "Kaynak silinince kimlik de silinsin, en basit" | **System-assigned** MI |
| "Uygulama yalnızca secret **okuyacak**, en az yetki" | **Key Vault Secrets User** |
| "Secret oluştur/güncelle (rotation, dağıtım)" | **Key Vault Secrets Officer** |
| "`Contributor` var ama secret okunamıyor" | Control plane ≠ data plane → data rolü ver |
| "Secret süresi dolmadan **otomatik tetikle**" | **Event Grid** `SecretNearExpiry` → Function |
| "Key'i otomatik döndür" | Key **rotation policy** |
| "Sertifikayı otomatik yenile" | Certificate **auto-renewal** (policy) |
| "Rotation sonrası uygulama yeni değeri almalı, restart yok" | Sürümsüz referans + periyodik yenileme (`ReloadInterval` / Key Vault referansı / `IOptionsMonitor`) |
| "Silinen vault kurtarılabilsin" | **Soft delete** |
| "Kimse kalıcı silemesin (CMK, uyumluluk)" | **Purge protection** |
| "Vault yalnızca VNet'ten erişilsin" | **Private endpoint** (+ public erişimi kapat) |
| "Pod/GitHub Actions'tan secret'sız Azure erişimi" | **Workload identity federation** |

**Tuzaklar:**
- RBAC modelinde `Owner`/`Contributor` secret okutmaz; **access policy** modelindeki izinler ise RBAC rollerini **yok sayar** (bir vault iki modeli aynı anda uygulamaz — model vault ayarıdır).
- Sürümlü Key Vault referansı rotation'ı **engeller**.
- Event Grid olayı rotation'ı **yapmaz**, sadece tetikler; yeni değeri üreten sizin koddur.
- `SecretNewVersionCreated`'a abone olup aynı Function'dan yazmak sonsuz döngü doğurur.
- Purge protection açıkken `az keyvault purge` çalışmaz; vault adı retention bitene kadar tekrar kullanılamaz.
- Soft delete artık kapatılamaz; "soft delete'i aç" seçeneği eski sorulardandır — **purge protection** isteğe bağlı olandır.
- Birden çok user-assigned MI varken `client_id` belirtmemek.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Secret'ı `appsettings.json`/pipeline değişkenine koymak | Key Vault + MI; mümkünse hiç secret'sız Entra kimliği |
| `Contributor`/`Owner` verip "yetki verdim" demek | Data plane rolü (Secrets User) gerekir |
| Uygulamaya `Key Vault Administrator` vermek | Okuma için **Secrets User**; en az yetki |
| Pinned sürüm URI'ı kullanıp rotation beklemek | Sürümsüz URI (en son sürüm) |
| `IOptions<T>` ile secret okuyup "yeni değeri almıyor" demek | `IOptionsMonitor<T>`/`IOptionsSnapshot<T>` ve `ReloadInterval` |
| Her istekte `GetSecretAsync` | Önbellekle; vault throttling (429) |
| `DefaultAzureCredential`'a üretimde güvenip yanlış kimlik yakalamak | Belirli credential veya `AZURE_CLIENT_ID`/`ManagedIdentityClientId` |
| System-assigned MI'lı kaynağı silip yeniden oluşturmak | Principal ID değişir; rol atamasını yenile (ya da user-assigned kullan) |
| Rotation'da tek anahtarı değiştirip uygulamayı kırmak | İki kimlik bilgisi kalıbı; önce ikincisini döndür |
| Rol atar atamaz 403 alıp pes etmek | Yayılım birkaç dakika sürebilir |
| Secret'ı log'a yazmak | `Value`'yu asla loglama; telemetri sızıntı yolu olur |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Secret → **MI + Entra kimliği** | Rotation ve sızıntı yüzeyi yok | Hedef servis Entra auth desteklemeli; rol yönetimi |
| System → user-assigned MI | Paylaşım, öngörülebilir yaşam döngüsü | Ek kaynak; `client_id` seçimi; fazla paylaşım = geniş yetki |
| RBAC ↔ access policy | Ayrıntılı kapsam, PIM/Policy, merkezî denetim | Atama sayısı artar; yayılım gecikmesi |
| Kısa `ReloadInterval` | Rotation hızlı yansır | Daha çok vault isteği → throttling/maliyet |
| Platform referansı (App Service/ACA) | Kodsuz | Yenileme gecikmesi (≈ saat/dakika), kontrol sende değil |
| Purge protection | Kötü niyetli/yanlışlıkla kalıcı silmeye karşı | Geri alınamaz; vault adı bloklu kalır, test vault'larında can sıkar |
| Private endpoint | Ağdan erişim kapanır | DNS (`privatelink`) yapılandırması, ek ücret |
| Key Vault Standard ↔ Premium | Premium: HSM destekli key | Daha pahalı; secret için gereksiz |

---

## 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Managed identities for Azure resources](https://learn.microsoft.com/en-us/entra/identity/managed-identities-azure-resources/overview) · [Best practices for managed identities](https://learn.microsoft.com/en-us/entra/identity/managed-identities-azure-resources/managed-identity-best-practice-recommendations)
- [Credential chains in the Azure Identity library for .NET](https://learn.microsoft.com/en-us/dotnet/azure/sdk/authentication/credential-chains) (`DefaultAzureCredential` üretim notları)
- [Azure Key Vault overview](https://learn.microsoft.com/en-us/azure/key-vault/general/overview) · [Grant access with Azure RBAC](https://learn.microsoft.com/en-us/azure/key-vault/general/rbac-guide) · [Plan for Azure RBAC as the default access control model](https://learn.microsoft.com/en-us/azure/key-vault/general/access-control-default)
- [Soft delete](https://learn.microsoft.com/en-us/azure/key-vault/general/soft-delete-overview) · [Recovery and purge protection](https://learn.microsoft.com/en-us/azure/key-vault/general/key-vault-recovery)
- [Key Vault as Event Grid source](https://learn.microsoft.com/en-us/azure/event-grid/event-schema-key-vault) · [Rotation tutorial — resources with two sets of credentials](https://learn.microsoft.com/en-us/azure/key-vault/secrets/tutorial-rotation-dual)
- [Key Vault references (App Service/Functions)](https://learn.microsoft.com/en-us/azure/app-service/app-service-key-vault-references) · [Manage secrets in Azure Container Apps](https://learn.microsoft.com/en-us/azure/container-apps/manage-secrets)
- [Azure Key Vault secret client library for .NET](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/security.keyvault.secrets-readme) · [azure-keyvault-secrets (Python)](https://learn.microsoft.com/en-us/python/api/overview/azure/keyvault-secrets-readme)

> **Doğrulama notu:** "Yeni vault'larda RBAC varsayılanı" Key Vault control plane API `2026-02-01` ile ilişkilendirilir (mevcut vault'lar etkilenmez); platform referans yenileme aralıkları (App Service ≈ 24 saat, Container Apps ≈ 30 dk) dokümantasyonda/Q&A'da değişken ifade edilir — sınav öncesi güncel sayfaları kontrol et.
