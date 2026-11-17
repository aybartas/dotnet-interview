# 🎓 AI-200: Azure AI Cloud Developer Associate — Kapsamlı Çalışma Dokümanı

> **Son Güncelleme:** Ekim 2026 · **Resmî Sınav Hedefleri:** [Skills measured as of October 7, 2026](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
> **Sınav Formatı:** 40-60 soru · 100-120 dakika · Geçme puanı: **700 / 1000** · Soru tipleri: Vaka çalışması, kod/CLI/KQL tamamlama, tek seçim, çoklu seçim, evet/hayır, sürükle-bırak
>
> Bu doküman, AI-200 sınavının 14 konusunun tüm teorik anlatımlarını, mimari şemalarını, SDK/kod örneklerini, KQL/CLI komutlarını, performans/trade-off analizlerini ve 260'a yakın özgün sınav formatında quiz ve deneme sınavı sorusunu (2 vaka çalışması dahil) tek bir kaynakta birleştirmektedir.

---

## 📋 Sınav Alanları ve Ağırlıkları

| Sınav Alanı (Domain) | Ağırlık | Bölümler / Konular |
|---|---|---|
| **Develop containerized solutions on Azure** | %20-25 | Bölüm 1 (C2.1 ACR, C2.2 Container Apps, C2.3 AKS) |
| **Develop AI solutions by using Azure data management services** | **%30-35** | Bölüm 2 (C2.4 Cosmos NoSQL, C2.5 Vector/Change Feed, C2.6 PostgreSQL/pgvector, C2.7 Managed Redis) |
| **Connect to and consume Azure services** | %20-25 | Bölüm 3 (C2.8 Functions, C2.9 Service Bus, C2.10 Event Grid) |
| **Secure, monitor, and troubleshoot Azure solutions** | %20-25 | Bölüm 4 (C2.11 Managed Identity/Key Vault, C2.12 App Configuration, C2.13 OpenTelemetry/KQL) |
| **Deneme Sınavı & Strateji** | 50 Soru | Bölüm 5 (5.1, 5.2 - 2 Vaka Çalışması Dahil, 5.3) |

---

## 📑 Kapsamlı İçindekiler

- **BÖLÜM 1: Develop containerized solutions on Azure (%20-25)**
  - [1.1 Azure Container Registry ve ACR Tasks](#11-azure-container-registry-ve-acr-tasks)
    - [1.1 Quiz Soruları](#-bölüm-11-quiz-soruları)
  - [1.2 Azure Container Apps: Ortam, Revision ve KEDA](#12-azure-container-apps-ortam-revision-ve-keda)
    - [1.2 Quiz Soruları](#-bölüm-12-quiz-soruları)
  - [1.3 AKS'e Manifest ile Dağıtım ve Container Teşhisi](#13-akse-manifest-ile-dağıtım-ve-container-teşhisi)
    - [1.3 Quiz Soruları](#-bölüm-13-quiz-soruları)
- **BÖLÜM 2: Develop AI solutions by using Azure data management services (%30-35)**
  - [2.1 Cosmos DB for NoSQL: SDK, Partitioning, Indexing, Consistency, RU](#21-cosmos-db-for-nosql-sdk-partitioning-indexing-consistency-ru)
    - [2.1 Quiz Soruları](#-bölüm-21-quiz-soruları)
  - [2.2 Cosmos DB ile Vector Search ve Change Feed](#22-cosmos-db-ile-vector-search-ve-change-feed)
    - [2.2 Quiz Soruları](#-bölüm-22-quiz-soruları)
  - [2.3 Azure Database for PostgreSQL ve pgvector](#23-azure-database-for-postgresql-ve-pgvector)
    - [2.3 Quiz Soruları](#-bölüm-23-quiz-soruları)
  - [2.4 Azure Managed Redis: Cache ve Vector Index](#24-azure-managed-redis-cache-ve-vector-index)
    - [2.4 Quiz Soruları](#-bölüm-24-quiz-soruları)
- **BÖLÜM 3: Connect to and consume Azure services (%20-25)**
  - [3.1 Azure Functions: Serverless API, Trigger ve Binding](#31-azure-functions-serverless-api-trigger-ve-binding)
    - [3.1 Quiz Soruları](#-bölüm-31-quiz-soruları)
  - [3.2 Azure Service Bus: Kuyruk, Topic ve Dead-letter](#32-azure-service-bus-kuyruk-topic-ve-dead-letter)
    - [3.2 Quiz Soruları](#-bölüm-32-quiz-soruları)
  - [3.3 Azure Event Grid: Custom Event, Filtre ve Retry](#33-azure-event-grid-custom-event-filtre-ve-retry)
    - [3.3 Quiz Soruları](#-bölüm-33-quiz-soruları)
- **BÖLÜM 4: Secure, monitor, and troubleshoot Azure solutions (%20-25)**
  - [4.1 Managed Identity ve Azure Key Vault](#41-managed-identity-ve-azure-key-vault)
    - [4.1 Quiz Soruları](#-bölüm-41-quiz-soruları)
  - [4.2 Azure App Configuration](#42-azure-app-configuration)
    - [4.2 Quiz Soruları](#-bölüm-42-quiz-soruları)
  - [4.3 OpenTelemetry ile İzleme ve KQL ile Sorun Giderme](#43-opentelemetry-ile-izleme-ve-kql-ile-sorun-giderme)
    - [4.3 Quiz Soruları](#-bölüm-43-quiz-soruları)
- **BÖLÜM 5: AI-200 Deneme Sınavı ve Sınav Stratejisi**
  - [5.0 Kapsamlı AI-200 Deneme Sınavı (50 Soru, 2 Vaka Çalışması) ve Sınav Stratejisi](#50-kapsamlı-ai-200-deneme-sınavı-50-soru-2-vaka-çalışması-ve-sınav-stratejisi)
    - [5.1 Sınav Stratejisi ve Hazırlık Rehberi](#51-sınav-stratejisi-ve-hazırlık-rehberi)
    - [5.2 50 Soruluk AI-200 Deneme Sınavı](#52-50-soruluk-ai-200-deneme-sınavı)
    - [5.3 Puanlama ve Zayıf Alan Analizi](#puan-tablosu)

---

# BÖLÜM 1: Develop containerized solutions on Azure (%20-25)

---

## 1.1 Azure Container Registry ve ACR Tasks

> **Sınav Alanı:** Develop containerized solutions on Azure (%20-25) · **Konu Kodu:** C2.1

### 1. Ne?

**Azure Container Registry (ACR)**, OCI uyumlu container image'larını ve artifact'leri (Helm chart, imza, SBOM…) saklayan **özel, yönetilen registry**'dir. `docker pull/push` ile konuşur; Container Apps, AKS, App Service, Functions ve ACI buradan image çeker. **ACR Tasks** ise registry'nin içindeki **bulut tabanlı build/run motorudur**: yerelde Docker olmadan image derler, kaynak kod veya **base image** değişince otomatik yeniden derler.

| Kavram | Anlamı | Örnek |
|---|---|---|
| **Registry** | Login server'ı olan servis | `myacr.azurecr.io` |
| **Repository** | Aynı image'ın sürümlerini toplayan ad | `todoapi` |
| **Tag** | Manifest'e **değişebilir** işaretçi | `todoapi:1.4.2` |
| **Manifest / digest** | İçeriğin **değişmez** SHA-256 kimliği | `todoapi@sha256:9f2c…` |
| **Layer** | Katman; aynı katman repository'ler arasında paylaşılır | — |

Tam image referansı: `<registry>.azurecr.io/<repository>:<tag>` veya `<registry>.azurecr.io/<repository>@sha256:<digest>`.

```text
git push ─────────────┐
base image update ────┼──▶ ACR Task ──build + push──▶ ACR ──AcrPull──▶ Container Apps / AKS
timer (cron) ─────────┘
```

Bu konu, AI-200'ün *Develop containerized solutions* alanının temelidir: sonraki iki konu ([C2.2](#12-azure-container-apps-ortam-revision-ve-keda), [C2.3](#13-akse-manifest-ile-dağıtım-ve-container-teşhisi)) burada üretilen image'ı çalıştırır.

---

### 2. Neden?

- **Sınav:** SKU ↔ özellik eşleştirmesi, `az acr build` komutu, tag/digest farkı, ACR Tasks tetikleyicileri ve "servis image çekemiyor" senaryolarında doğru rol (`AcrPull`) neredeyse her sınavda vardır; CLI tamamlama sorularına çok uygundur.
- **Gerçek işte:** Public registry'ye (Docker Hub) bağımlılık anonim pull rate limit'ine takılır, üstelik kontrolün dışındadır (→ M14.2). Private registry; ağ izolasyonu (private endpoint), kimlik tabanlı erişim, geo-replication ve güvenlik taraması için gerekir.
- **Tedarik zinciri güvenliği:** `:latest` ile deploy etmek "hangi kod çalışıyor?" sorusunu cevapsız bırakır; digest ile sabitleme ve base image yama zinciri (ACR Tasks) bu sorunu çözer.
- **Geliştirici deneyimi:** CI makinesinde veya Docker'sız bir dev ortamında `az acr build` tek komutla image üretir.

---

### 3. Nasıl?

#### 3.1 SKU'lar ⭐

| Özellik | **Basic** | **Standard** | **Premium** |
|---|---|---|---|
| Dahil depolama (yaklaşık, fiyat sayfası) | ~10 GiB | ~100 GiB | ~500 GiB |
| Programatik yetenekler (push/pull, ACR Tasks, RBAC) | ✔ | ✔ | ✔ |
| Throughput / limitler | düşük | orta | **en yüksek** |
| **Geo-replication** (tek registry, çok bölge) | ✘ | ✘ | ✔ |
| **Private endpoint** (Private Link) | ✘ | ✘ | ✔ |
| Content trust (Docker Content Trust) | ✘ | ✘ | ✔ **(emekli ediliyor, aşağıya bak)** |
| Retention policy (untagged manifest) | ✘ | ✘ | ✔ *(preview)* |
| Customer-managed key, dedicated data endpoint, connected registry | ✘ | ✘ | ✔ |

- SKU'lar **aynı API'yi** sunar; fark depolama, throughput ve **ağ/güvenlik/dağıtım** özelliklerindedir. SKU sonradan yükseltilebilir/düşürülebilir (`az acr update --sku Premium`).
- Dahil kotanın üstü için günlük depolama ücreti alınır; geo-replication'da **her replika bölgesi** ayrıca ücretlendirilir. Tam rakamlar için fiyat sayfasına bak (rakamları ezberleme).
- **Docker Content Trust** Microsoft tarafından kullanımdan kaldırılıyor (2025'te deprecation başladı, **31 Mart 2028'de** tamamen kalkacak; yeni registry'lerde etkinleştirme kapatıldı). İmzalama için **Notary Project / Notation** (anahtar Azure Key Vault'ta) önerilir. Sınav sorusunda "imzalı image / content trust → Premium" kalıbı hâlâ görülebilir; bilginin güncel durumunu da bil.

#### 3.2 Tag stratejisi ve digest ⭐

| Yaklaşım | Örnek | Ne zaman | Risk |
|---|---|---|---|
| **semver** | `:1.4.2` | Sürüm notlu release'ler | Aynı tag yeniden push edilirse içerik değişir |
| **commit SHA** | `:sha-3fa91c2` | CI çıktıları, tam izlenebilirlik | Okunaksız; temizlik gerekir |
| **Build ID** | `:{{.Run.ID}}` (ACR Tasks) | Her build benzersiz | İnsan için anlamsız |
| **`:latest`** | `:latest` | Yalnızca yerel deneme | Üretimde **yasak**: ne çalıştığı belirsiz, geri alma imkânsız |
| **Digest** | `@sha256:…` | **Üretim deploy'u** (immutable) | Güncelleme için yeni digest gerekir |

- **Tag değişebilir, digest değişmez.** Aynı `:1.4.2` tag'i yeniden push edilirse başka bir manifest'e işaret eder; digest ise içeriğin kendisidir. Üretimde "ne test ettiysek tam olarak onu çalıştır" için **digest ile deploy** edilir.
- Birden çok tag aynı digest'e işaret edebilir (`1.4.2`, `1.4`, `sha-3fa91c2`).
- Tag'i korumak için kilitle: `az acr repository update --name $ACR --image todoapi:1.4.2 --write-enabled false` (üzerine yazma/silme kapanır).
- Keşif komutları:

```bash
az acr repository list --name $ACR -o table
az acr repository show-tags --name $ACR --repository todoapi --detail --orderby time_desc -o table
az acr repository show --name $ACR --image todoapi:1.4.2 --query digest -o tsv
az acr manifest list-metadata --registry $ACR --name todoapi -o table   # digest, tags, lastUpdateTime
```

#### 3.3 Kimlik ve yetkilendirme ⭐

| Yöntem | Ne zaman | Not |
|---|---|---|
| **Entra kimliği** (kullanıcı) | Geliştirici `az acr login` | Kısa ömürlü token |
| **Managed identity** + rol | Container Apps, AKS, Functions, App Service'in image çekmesi | Üretimde **önerilen**: secret yok |
| **Service principal** | Kurum dışı CI sistemleri | Secret/sertifika yönetimi gerekir |
| **Repository-scoped token / scope map** | Tek repository'ye sınırlı dış erişim | Registry'ye özel token |
| **Admin user** | Hızlı demo | **Varsayılan kapalı**; tek paylaşılan parola — üretimde kapalı kal |

Yerleşik roller (klasik **RBAC registry** modu):

| Rol | Yetki |
|---|---|
| `AcrPull` | Image **çekme** (servislerin çalışma zamanı kimliği için) |
| `AcrPush` | Çekme + **push** (CI/CD) |
| `AcrDelete` | Image silme |
| `Contributor` / `Owner` | Registry'yi yönetir; **en az yetki ilkesi** gereği image çekmek için gerekmez |

> **Yeni (2026):** Registry'de **ABAC repository permissions** modu etkinleştirilirse `AcrPull/AcrPush/AcrDelete` rolleri **dikkate alınmaz**; bunların yerine *Container Registry Repository Reader / Writer / Contributor* rolleri kullanılır ve koşullarla **repository bazında** daraltılabilir. Bu modda `az aks --attach-acr` da desteklenmez; rol atamasını elle yap. Sınavda klasik cevap hâlâ `AcrPull`'dur, ama senaryoda ABAC geçiyorsa dikkat et.

`az acr login --name $ACR` Docker CLI için token alır; Docker olmayan ortamda `az acr login --name $ACR --expose-token` ham token döndürür.

#### 3.4 ACR Tasks ⭐

| Görev türü | Komut | Ne işe yarar |
|---|---|---|
| **Quick build** | `az acr build` | Kaynağı buluta gönder, orada build + push; **yerel Docker gerekmez** |
| **Quick run** | `az acr run` | Bir container'ı (veya `acr purge` gibi komutu) registry bağlamında çalıştırır |
| **Automatically triggered task** | `az acr task create` | Tetikleyicilerle otomatik build |
| **Multi-step task** | `az acr task create --file task.yaml` / `az acr run --file` | YAML ile build → test → push zinciri |

**Tetikleyiciler (task'a bağlanır):**

| Tetikleyici | Açıklama |
|---|---|
| **Source code update** | Git deposuna commit/pull request (GitHub, Azure DevOps) — erişim için personal access token |
| **Base image update** | Dockerfile'daki `FROM` image'ı güncellenince otomatik yeniden build ⭐ |
| **Timer** | Cron zamanlayıcı (gece build'i, temizlik görevi) |

Base image update ayrıntıları:
- Task'ta **varsayılan olarak açıktır** (`--base-image-trigger-enabled`); tür `--base-image-trigger-type` ile **`Runtime`** (varsayılan, yalnızca son aşama/runtime image) veya `All` seçilir.
- Task'ın bağımlılıkları **ilk build'den sonra** izlenmeye başlar (önce bir kez `az acr task run` ya da commit ile çalışmış olmalı).
- Base image **sabit tag** taşımalıdır (`aspnet:10.0`); her sürümde yeni tag çıkıyorsa tetikleme olmaz.
- Çok aşamalı Dockerfile'da yalnızca **runtime** image izlenir, build-time (SDK) image'ı değil.
- Base image aynı registry'deyse güncelleme **anında** tetikler; public registry'lerdeki (MCR, Docker Hub) image'lar için kontrol periyodiktir (anlık değildir).

**Multi-step task** (`task.yaml`) temel adım türleri: `build`, `push`, `cmd` (container çalıştır). Değişkenler: `$Registry`, `{{.Run.ID}}`, `{{.Values.xyz}}`.

```yaml
# task.yaml — build, smoke-test, then push
version: v1.1.0
steps:
  - build: -t $Registry/todoapi:{{.Run.ID}} -f Dockerfile .
  - cmd: $Registry/todoapi:{{.Run.ID}} --version      # runs the freshly built image
  - push:
      - $Registry/todoapi:{{.Run.ID}}
      - $Registry/todoapi:latest
```

> **Kısıt:** Azure ücretsiz deneme/kredi (free trial, öğrenci) aboneliklerinde ACR Tasks çalıştırmaları **askıya alınmış** olabilir (`TasksOperationsNotAllowed` hatası); çözüm Pay-As-You-Go'ya geçmek veya yerelde `docker build` + `docker push` yapmaktır. Güncel durumu doğrula.

#### 3.5 Bakım ve maliyet

| Araç | Ne yapar | Not |
|---|---|---|
| `az acr repository delete --image …` | Tag/manifest siler | Digest ile silmek tüm tag'leri götürür |
| **`acr purge`** (`az acr run --cmd`) | `--filter`, `--ago`, `--keep`, `--untagged`, `--dry-run` ile toplu temizlik | Timer task'a bağlanıp otomatikleştirilir; OCI manifest'lerini de temizler |
| **Retention policy** (Premium, preview) | Untagged manifest'i N gün sonra siler (0–365; varsayılan 7) | `az acr config retention update --status Enabled --days 30 --type UntaggedManifests`; OCI medya tipli manifest'lerde çalışmaz |
| Tag kilidi (`--write-enabled false`) | Release tag'ini korur | Purge de kilitli tag'i silemez |

Untagged manifest, bir tag'in başka bir image'a taşınmasıyla "yetim" kalır, ama **depolama ücreti** doğurmaya devam eder. Temizlik yapılmazsa registry şişer.

#### 3.6 Lab — Todo API'yi bulutta derle, sürümle, temizle

Maliyet: **Basic SKU** günlük birkaç kuruştur; yine de önce bütçe alarmı kur (→ C1.1). Yerel Docker **gerekmez**. (ACR Tasks free-trial aboneliklerde engelli olabilir — bkz. 3.4.)

`src/TodoApi/Dockerfile` (Todo API, .NET 10):

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "TodoApi.dll"]
```

```bash
RG=rg-ai200-c21
ACR=acrai200$RANDOM                           # must be globally unique, 5-50 lowercase alphanumerics
az group create --name $RG --location westeurope

# 1) Registry (Basic, admin user off)
az acr create --resource-group $RG --name $ACR --sku Basic --admin-enabled false

# 2) Build in the cloud: no local Docker; two tags on one build (semver + commit SHA)
SHA=$(git rev-parse --short HEAD)
az acr build --registry $ACR \
  --image todoapi:1.0.0 --image todoapi:sha-$SHA \
  src/TodoApi

# 3) Inspect tags and note the digest (this is what you deploy in C2.2)
az acr repository show-tags --name $ACR --repository todoapi --detail -o table
DIGEST=$(az acr repository show --name $ACR --image todoapi:1.0.0 --query digest -o tsv)
echo "$ACR.azurecr.io/todoapi@$DIGEST"

# 4) Lock the release tag
az acr repository update --name $ACR --image todoapi:1.0.0 --write-enabled false

# 5) Quick run: execute a container inside ACR (no source needed, hence /dev/null)
az acr run --registry $ACR --cmd '$Registry/todoapi:1.0.0 --version' /dev/null

# 6) Base-image trigger: keep the runtime base image in your own registry, rebuild when it changes
az acr import --name $ACR --source mcr.microsoft.com/dotnet/aspnet:10.0 --image base/aspnet:10.0
# (Dockerfile now uses: ARG REGISTRY_NAME / FROM ${REGISTRY_NAME}/base/aspnet:10.0)
az acr task create --registry $ACR --name todoapi-rebuild \
  --image 'todoapi:{{.Run.ID}}' \
  --context https://github.com/<you>/<repo>.git#main:src/TodoApi \
  --file Dockerfile --arg REGISTRY_NAME=$ACR.azurecr.io \
  --git-access-token $GH_PAT --base-image-trigger-type Runtime
az acr task run --registry $ACR --name todoapi-rebuild      # first run: dependencies get tracked
# Re-import a patched base image under the same tag -> the task fires automatically:
az acr import --name $ACR --source mcr.microsoft.com/dotnet/aspnet:10.0 --image base/aspnet:10.0 --force
az acr task list-runs --registry $ACR -o table

# 7) Cleanup of untagged images: dry run first, then a nightly timer task
az acr run --registry $ACR \
  --cmd "acr purge --filter 'todoapi:.*' --ago 30d --untagged --keep 3 --dry-run" /dev/null
az acr task create --registry $ACR --name nightly-purge \
  --cmd "acr purge --filter 'todoapi:.*' --ago 30d --untagged --keep 3" \
  --schedule "0 1 * * *" --context /dev/null

# 8) Give a runtime identity pull rights (least privilege) — example with a user-assigned identity
PRINCIPAL=$(az identity create -g $RG -n id-todoapi --query principalId -o tsv)
az role assignment create --assignee-object-id $PRINCIPAL --assignee-principal-type ServicePrincipal \
  --role AcrPull --scope $(az acr show --name $ACR --query id -o tsv)

# 9) Cleanup
az group delete --name $RG --yes --no-wait
```

**C# (.NET 10, `Azure.Containers.ContainerRegistry` + `Azure.Identity`) — tag ve digest listeleme:**

```csharp
using Azure.Containers.ContainerRegistry;
using Azure.Identity;

var endpoint = new Uri($"https://{Environment.GetEnvironmentVariable("ACR")}.azurecr.io");
var client = new ContainerRegistryClient(endpoint, new DefaultAzureCredential(),
    new ContainerRegistryClientOptions { Audience = ContainerRegistryAudience.AzureResourceManagerPublicCloud });

ContainerRepository repo = client.GetRepository("todoapi");
await foreach (ArtifactManifestProperties m in
    repo.GetAllManifestPropertiesAsync(ArtifactManifestOrder.LastUpdatedOnDescending))
{
    Console.WriteLine($"{m.Digest}  tags=[{string.Join(", ", m.Tags)}]  updated={m.LastUpdatedOn:u}");
}
```

**Python eşdeğeri (`azure-containerregistry`, `azure-identity`):**

```python
import os
from azure.containerregistry import ContainerRegistryClient, ArtifactTagOrder
from azure.identity import DefaultAzureCredential

endpoint = f"https://{os.environ['ACR']}.azurecr.io"
client = ContainerRegistryClient(endpoint, DefaultAzureCredential(),
                                 audience="https://management.azure.com")

for tag in client.list_tag_properties("todoapi", order_by=ArtifactTagOrder.LAST_UPDATED_ON_DESCENDING):
    print(tag.name, tag.digest, tag.last_updated_on)
```

> **Alıştırma.** `todoapi:1.0.0` tag'ini kilitledikten sonra aynı tag'e yeni bir `az acr build` push etmeye çalışırsan ne olur? Ardından `--image todoapi:1.0.1` ile build edersen? Hangi digest'i production'a verirsin?
>
> <details><summary>Çözüm</summary>
>
> Kilitli (`write-enabled=false`) tag'e push **reddedilir**; build başarısız olur. `1.0.1` yeni bir manifest/digest üretir; `1.0.0` ve digest'i aynen durur (geri alma kolaydır). Production'a, doğrulanmış sürümün `az acr repository show --query digest` çıktısı olan **digest** verilir (`todoapi@sha256:…`).
> </details>

---

### 4. Gerçek dünya senaryosu

Bir e-ticaret ekibi 12 mikroservisi ACR'da tutuyor:

| İhtiyaç | Karar | Neden |
|---|---|---|
| Geliştiricilerin laptop'unda Docker olmayabilir, CI ajanı küçük | `az acr build` | Build bulutta; ajanda Docker gerekmez |
| Production deploy'unda "tam olarak test edilen image" | **Digest ile** deploy (`@sha256:…`) | Tag yeniden yazılsa bile içerik sabit |
| .NET runtime'ında kritik güvenlik yaması çıktı | **Base image trigger'lı ACR Task** | Tüm servis image'ları otomatik yeniden build; sonra pipeline yeniden deploy eder |
| Avrupa + ABD'deki AKS kümeleri aynı image'ı çekiyor | **Premium** + **geo-replication** | Yerel bölgeden hızlı pull, bölge kesintisinde dayanıklılık |
| Registry yalnızca VNet içinden erişilebilmeli | **Premium** + **private endpoint** | Genel erişim kapatılır |
| Container Apps ve AKS image çekecek | Managed identity + **`AcrPull`**, admin user kapalı | Secret yok, en az yetki |
| Registry maliyeti sürekli artıyor | `acr purge` timer task / retention policy | Yetim (untagged) manifest'ler birikmesin |
| Image imzalama gereksinimi | Notation + Key Vault (Content Trust yerine) | Docker Content Trust 2028'de kalkıyor |

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** SKU-özellik eşleştirmesi, senaryodan komut seçme (`az acr build` mi `docker build` mi?), CLI tamamlama (`--registry`, `--image`, `--role AcrPull`), tetikleyici seçimi ve "pod/servis image çekemiyor" kök neden analizi.

| Senaryodaki ifade | Cevap |
|---|---|
| "Yerel Docker kurmadan image derle" | `az acr build` |
| "Registry içinde bir komut/container çalıştır, build yok" | `az acr run` |
| "Base image güncellenince otomatik yeniden build" | ACR Task + **base image update trigger** |
| "Her gece yeniden build / temizlik" | ACR Task + **timer trigger** (cron) |
| "Birden çok bölgeye replikasyon" / "private endpoint" | **Premium** |
| "Servisin image çekmesi, en az yetki" | Managed identity + **`AcrPull`** |
| "CI'ın image push etmesi" | **`AcrPush`** |
| "Üretimde değişmez referans" | **Digest** |
| "Tag'in üzerine yazılmasını engelle" | `--write-enabled false` |
| "Untagged manifest'leri otomatik sil" | Retention policy (Premium) / `acr purge` |

**Tuzaklar:**
- `az acr build` **yerel Docker kullanmaz**; kaynağı buluta yükler (`docker build` + `docker push` ile karıştırma).
- `AcrPull` **image çekme**, `AcrPush` çekme + push; `Contributor` image çekmek için doğru cevap **değildir**.
- Admin user kapalıyken `docker login -u <registry> -p <admin-password>` çalışmaz; doğru yol Entra kimliği.
- Base image trigger **ilk build'den önce** çalışmaz ve base image **sabit tag** gerektirir.
- Tag ≠ digest: "aynı tag aynı içeriktir" yanlış varsayımdır.
- Geo-replication, private endpoint **Premium**'a özgüdür; Basic/Standard'a eklenemez.
- Retention policy yalnızca **untagged** manifest'leri siler; tag'li image'a dokunmaz.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Üretimde `:latest` deploy etmek | Digest veya değişmez semver/SHA tag; geri alma ve denetim için |
| Admin user'ı açıp parolayı pipeline'a koymak | Managed identity/OIDC + `AcrPull`/`AcrPush` |
| Image çekecek servise `Contributor` vermek | Yalnızca `AcrPull` (en az yetki); `Contributor` registry'yi yönetir |
| Rol atamasından hemen sonra "hâlâ pull edemiyor" demek | RBAC yayılımı dakikalar sürebilir; yeniden dene |
| Base image trigger'ın "çalışmadığını" varsaymak | Önce task'ı bir kez çalıştır; sabit tag kullan; yalnızca runtime image izlenir |
| `az acr build` bağlamında `.dockerignore` unutmak | `bin/`, `obj/`, `.git/` buluta yüklenir; build yavaşlar |
| Temizlik yapmamak | `acr purge --untagged` / retention policy; yetim manifest'ler depolama ücreti yazar |
| Private endpoint açıp build'in "çalışmamasına" şaşırmak | Quick task'lar registry'ye genel erişim bekler; kısıtlı registry için dedicated agent pool veya ağ kuralları gerekir (güncel dokümana bak) |
| Docker Content Trust'a yeni yatırım yapmak | Deprecation (31 Mart 2028 kaldırma); Notation kullan |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Basic → Standard → Premium | Daha fazla depolama ve throughput | Daha yüksek sabit ücret |
| Geo-replication | Düşük gecikmeli bölgesel pull, dayanıklılık | Her replika bölgesi ayrı ücretli; push sonrası replikasyon gecikmesi |
| Digest ile deploy | Değişmezlik, tam izlenebilirlik | Okunaksız; güncellemek için pipeline yeni digest'i yazmalı |
| `az acr build` (bulut) vs yerel build | Docker gerekmez, tutarlı ortam, doğrudan registry'ye | Kaynak yükleme süresi, bulut build dakikaları ücretli |
| Base image'ı kendi registry'ne import etmek | Yama zinciri, rate limit'ten bağımsızlık | Import'u güncel tutma sorumluluğu |
| Agresif `acr purge` | Düşük depolama maliyeti | Eski tag'lere geri dönüş imkânı azalır; `--keep` ve `--dry-run` kullan |
| Private endpoint | Ağ izolasyonu | Premium ücreti, DNS (`privatelink.azurecr.io`) ve build/CI erişimi karmaşıklığı |

---

### 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Azure Container Registry service tiers](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-skus) · [Fiyatlandırma](https://azure.microsoft.com/pricing/details/container-registry/)
- [ACR Tasks overview](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-tasks-overview) · [Multi-step tasks](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-tasks-multi-step) · [Base image updates](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-tasks-base-images)
- [Authenticate with ACR](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-authentication) · [ABAC repository permissions](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-rbac-abac-repository-permissions)
- [Retention policy for untagged manifests](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-retention-policy) · [Automatically purge images (`acr purge`)](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-auto-purge)
- [Content trust deprecation](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-content-trust-deprecation)
- [`az acr` CLI reference](https://learn.microsoft.com/en-us/cli/azure/acr) · [azure-containerregistry (Python)](https://pypi.org/project/azure-containerregistry/)


### 📝 Bölüm 1.1 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir ekip registry'yi **yalnızca VNet içinden** erişilebilir yapmak (private endpoint) ve image'ları iki bölgeye replike etmek istiyor. Hangi SKU gerekir?

- A) Basic
- B) Standard
- C) Premium
- D) Standard + geo-replication eklentisi

<details><summary>Cevap</summary>

**C.** Private endpoint ve geo-replication yalnızca **Premium**'dadır. Basic/Standard'a sonradan eklenemez; D diye bir eklenti yoktur, önce SKU Premium'a yükseltilir.
</details>

#### Soru 2: Tek seçim
Geliştiricinin makinesinde Docker yok. Todo API'nin Dockerfile'ından image'ı üretip doğrudan registry'ye göndermek için hangi komut kullanılır?

- A) `docker build` ardından `docker push`
- B) `az acr build --registry myacr --image todoapi:1.0.0 .`
- C) `az acr run --registry myacr --cmd 'docker build .' /dev/null`
- D) `az acr import --name myacr --source ./Dockerfile`

<details><summary>Cevap</summary>

**B.** `az acr build` kaynağı ACR Tasks'a yükler; build ve push bulutta olur, yerel Docker gerekmez. A Docker ister; C'de `docker build` ACR Tasks ortamında çalıştırılacak bir komut değildir; `az acr import` hazır bir image'ı başka registry'den kopyalar, Dockerfile derlemez.
</details>

#### Soru 3: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Aynı tag'e farklı içerikli bir image yeniden push edilebilir; digest ise değişmez. | ☐ | ☐ |
| b) Bir manifest'e birden fazla tag işaret edebilir. | ☐ | ☐ |
| c) `todoapi:1.0.0` tag'i, kilitlenmediği sürece her zaman aynı digest'e işaret eder. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Tag değişebilir bir işaretçidir; üzerine yazılabilir (kilit `--write-enabled false` ile konur). Digest içeriğin SHA-256 kimliğidir. Aynı digest'e `1.0.0`, `1.0` ve `sha-abc123` gibi birden çok tag bağlanabilir. Bu yüzden üretimde digest ile deploy edilir.
</details>

#### Soru 4: Tek seçim
Container Apps uygulamasının managed identity'si ACR'dan image çekecek. **En az yetki** ilkesine göre hangi rol atanmalıdır?

- A) `Owner`
- B) `Contributor`
- C) `AcrPush`
- D) `AcrPull`

<details><summary>Cevap</summary>

**D.** Yalnızca çekme gerekiyor: `AcrPull`. `AcrPush` fazladan yazma yetkisi verir; `Contributor`/`Owner` registry'yi yönetme yetkisidir ve image çekmek için gerekmez. (ABAC repository permissions modunda eşdeğer rol *Container Registry Repository Reader*'dır.)
</details>

#### Soru 5: CLI tamamlama
Bir quick task ile registry bağlamında, kaynak kod olmadan `todoapi:1.0.0` image'ını çalıştırmak istiyorsun. Boşluğu doldur.

```bash
az acr ____ --registry myacr --cmd '$Registry/todoapi:1.0.0 --version' /dev/null
```

- A) `build`
- B) `run`
- C) `task create`
- D) `import`

<details><summary>Cevap</summary>

**B.** `az acr run` bir container'ı (veya `acr purge` gibi bir komutu) registry bağlamında çalıştırır; `/dev/null` "kaynak yok" anlamına gelir. `build` image üretir, `task create` kalıcı task tanımlar, `import` image kopyalar.
</details>

#### Soru 6: Çoklu seçim (iki doğru cevap)
ACR Task için **hangi iki** tetikleyici desteklenir?

- A) Base image güncellemesi
- B) Storage account'a blob yüklenmesi
- C) Zamanlayıcı (cron)
- D) Container Apps'te revision oluşması

<details><summary>Cevap</summary>

**A ve C.** ACR Tasks tetikleyicileri: kaynak kod güncellemesi (commit/PR), base image güncellemesi ve timer. Blob olayı (B) ve revision oluşumu (D) ACR Task tetikleyicisi değildir.
</details>

#### Soru 7: Evet / Hayır
Bir task `FROM mcr.microsoft.com/dotnet/aspnet:10.0` kullanıyor ve base image trigger açık.

| İfade | Evet | Hayır |
|---|---|---|
| a) Task en az bir kez çalıştırılmadan base image bağımlılıkları izlenmez. | ☐ | ☐ |
| b) Base image sürümleri her seferinde yeni, benzersiz bir tag ile yayınlanırsa (ör. `10.0.1`, `10.0.2`) tetikleme güvenilir çalışır. | ☐ | ☐ |
| c) Çok aşamalı Dockerfile'da varsayılan olarak yalnızca runtime image'ın güncellemesi izlenir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Bağımlılıklar ilk build'den sonra bilinir. Tetikleme için base image'ın **sabit bir tag** üzerinden güncellenmesi gerekir; yeni tag çıkarsa task bunu "güncelleme" saymaz. `--base-image-trigger-type` varsayılanı `Runtime`'dır (build-time SDK image'ı izlenmez).
</details>

#### Soru 8: Sıralama
Yeni bir servis için güvenli yayın akışının adımlarını doğru sıraya diz: (1) Digest'i oku ve deploy tanımına yaz, (2) Image'ı `az acr build` ile derle ve semver + SHA tag'le, (3) Registry'yi oluştur (admin user kapalı), (4) Uygulamayı deploy et.

<details><summary>Cevap</summary>

**3 → 2 → 1 → 4.** Önce registry, sonra build; digest ancak image push edildikten sonra bilinir; deploy tanımı digest'le yazıldıktan sonra uygulama çalıştırılır. (Çalışma zamanı kimliğine `AcrPull` ataması, deploy'dan önce herhangi bir noktada yapılabilir.)
</details>

#### Soru 9: Kod tamamlama (YAML)
Multi-step task, image'ı derleyip **test ettikten sonra** push etmeli. Boş bırakılan adım türü hangisidir?

```yaml
version: v1.1.0
steps:
  - build: -t $Registry/todoapi:{{.Run.ID}} -f Dockerfile .
  - ____: $Registry/todoapi:{{.Run.ID}} --selftest
  - push:
      - $Registry/todoapi:{{.Run.ID}}
```

- A) `run`
- B) `cmd`
- C) `test`
- D) `exec`

<details><summary>Cevap</summary>

**B.** Multi-step task'ta `build`, `push` ve `cmd` adım türleri vardır; `cmd` bir container'ı çalıştırır (burada yeni derlenen image). `run`, `test`, `exec` geçerli adım türleri değildir. Adım başarısız olursa sonraki `push` çalışmaz.
</details>

#### Soru 10: Tek seçim
Production'da aşağıdaki deploy referanslarından hangisi **en güvenli ve tekrarlanabilirdir**?

- A) `myacr.azurecr.io/todoapi:latest`
- B) `myacr.azurecr.io/todoapi:1.0`
- C) `myacr.azurecr.io/todoapi@sha256:9f2c…`
- D) `myacr.azurecr.io/todoapi`

<details><summary>Cevap</summary>

**C.** Digest içeriği değişmez biçimde tanımlar. `:latest` ve etiketsiz referans (D, aslında `:latest` demektir) ile `1.0` gibi hareketli tag'ler, aynı referansın zamanla farklı içeriğe işaret etmesine izin verir.
</details>

#### Soru 11: Çoklu seçim (iki doğru cevap)
Registry'de birikmiş **untagged manifest**'leri temizlemek için hangi iki yaklaşım doğrudur?

- A) `acr purge --untagged` komutunu ACR Task (timer) ile zamanlamak
- B) Premium registry'de retention policy etkinleştirmek
- C) Basic registry'de soft-delete'i kapatmak
- D) `az acr update --sku Free`

<details><summary>Cevap</summary>

**A ve B.** `acr purge` (`--filter`, `--ago`, `--keep`, `--untagged`, `--dry-run`) timer task'a bağlanabilir; retention policy (Premium, preview) untagged manifest'leri N gün sonra siler. C ve D uydurmadır (Free SKU yoktur).
</details>

#### Soru 12: CLI tamamlama
`todoapi:1.0.0` image'ının digest'ini yalnızca değer olarak almak istiyorsun.

```bash
az acr repository show --name myacr --image todoapi:1.0.0 --query ____ -o tsv
```

- A) `tag`
- B) `digest`
- C) `sha`
- D) `manifest.id`

<details><summary>Cevap</summary>

**B.** `az acr repository show --image …` çıktısında `digest` alanı bulunur; `--query digest -o tsv` düz metin döndürür. `az acr manifest list-metadata` de digest'leri ve tag'leri listeler.
</details>

#### Soru 13: Tek seçim
Bir GitHub Actions iş akışı, Entra'da tanımlı bir federated credential (OIDC) ile oturum açıp registry'ye image **push** edecek ve başka bir şey yapmayacak. Hangisi doğrudur?

- A) Admin user'ı etkinleştirip parolayı secret olarak saklamak
- B) Federe kimliğe registry kapsamında `AcrPush` atamak
- C) Federe kimliğe abonelik kapsamında `Contributor` atamak
- D) Anonim pull'u açmak

<details><summary>Cevap</summary>

**B.** Parolasız (OIDC) kimlik + yalnızca gereken rol + dar kapsam (registry). A paylaşılan parolaya döner; C gereğinden geniştir ve push için yetmeyebilir (data-plane rolü ister); D push'u hiç etkilemez.
</details>

#### Soru 14: Tek seçim
`az acr build` şu hatayla başarısız oluyor: `TasksOperationsNotAllowed`. Abonelik, **Azure ücretsiz deneme kredisi** kullanıyor. En olası neden ve çözüm hangisidir?

- A) Registry Basic SKU; Premium'a yükseltmek gerekir
- B) ACR Tasks çalıştırmaları bu abonelik türünde kısıtlanmış olabilir; Pay-As-You-Go'ya geçmek veya yerelde `docker build` + `docker push` kullanmak çözümdür
- C) `AcrPull` rolü eksiktir
- D) Dockerfile'da `ENTRYPOINT` yoktur

<details><summary>Cevap</summary>

**B.** Microsoft, kredi/deneme aboneliklerinde ACR Tasks çalıştırmalarını geçici olarak kısıtlayabilmektedir (durumu güncel dokümandan doğrula). SKU, `AcrPull` veya `ENTRYPOINT` bu hata kodunu üretmez; push için gereken rol ise `AcrPush`'tur.
</details>

#### Soru 15: Vaka çalışması
**Contoso Retail** 12 .NET mikroservisini ACR'a koyuyor. Gereksinimler: (1) Production'a giden image'lar tam olarak QA'da test edilen image olmalı; (2) .NET runtime güvenlik yaması çıkınca tüm servis image'ları kendiliğinden yeniden derlenmeli; (3) Avrupa ve ABD'deki AKS kümeleri hızlı pull yapmalı; (4) Registry'ye internetten erişim kapalı olmalı; (5) Pipeline'da kalıcı parola bulunmamalı; (6) Registry maliyeti kontrol altında kalmalı.

**a)** Gereksinim (1) için ne yapılır?
- A) `:latest` tag'iyle deploy
- B) Digest ile deploy (`@sha256:…`)

**b)** Gereksinim (2) için hangi mekanizma?
- A) Her servis için ACR Task + base image update trigger
- B) AKS'te cron job

**c)** Gereksinimler (3) ve (4) için hangi SKU/özellikler?
- A) Standard + Azure Firewall
- B) Premium + geo-replication + private endpoint

**d)** Gereksinim (5) için?
- A) Admin user ve paylaşılan parola
- B) Managed identity / OIDC + `AcrPull` / `AcrPush`

**e)** Gereksinim (6) için?
- A) `acr purge --untagged --ago … --keep …` timer task'ı (veya Premium'da retention policy)
- B) Hiçbir şey; ACR eski image'ları kendi siler

<details><summary>Cevap</summary>

**a) B · b) A · c) B · d) B · e) A.**
(a) Digest değişmezdir. (b) Base image trigger yama zincirini otomatikleştirir (base image sabit tag + ilk build şartı). (c) Geo-replication ve private endpoint Premium'dur; Firewall registry'ye private erişim sağlamaz. (d) Secret'sız kimlik, en az yetki. (e) ACR eski image'ları kendiliğinden silmez; yetim manifest'ler depolama ücreti yazar.
</details>


---

## 1.2 Azure Container Apps: Ortam, Revision ve KEDA

> **Sınav Alanı:** Develop containerized solutions on Azure (%20-25) · **Konu Kodu:** C2.2

### 1. Ne?

**Azure Container Apps (ACA)**, Kubernetes üzerinde çalışan ama Kubernetes'i **gizleyen** serverless container platformudur: HTTP API'ler, arka plan worker'ları, olay işleyiciler ve işler (jobs) için; küme, node, `kubectl` yönetimi yoktur. Yerleşik **KEDA** (olay güdümlü ölçekleme), **Dapr** (isteğe bağlı), **Envoy** tabanlı ingress ve **revision** tabanlı sürümleme sunar.

```text
Container Apps environment  (ortak ağ + log sınırı, Log Analytics)
 ├─ container app "todoapi"      ── ingress (external) ── revisions: todoapi--v1 (90%) · todoapi--v2 (10%)
 ├─ container app "orderworker"  ── ingress yok          ── scale rule: Service Bus queue length (0..10 replica)
 └─ container apps job "nightly" ── cron / olay / manuel tetik
```

| Kavram | Anlamı |
|---|---|
| **Environment** | Uygulamaların paylaştığı güvenlik/ağ/log sınırı (aynı VNet, aynı Log Analytics çalışma alanı) |
| **Container app** | Bir veya daha çok container'ı (template) çalıştıran dağıtım birimi |
| **Revision** | Uygulamanın **değişmez anlık görüntüsü** (image, env var, scale kuralları…) |
| **Replica** | Bir revision'ın çalışan örneği (pod benzeri) |
| **Scale rule** | Replica sayısını belirleyen kural (HTTP, KEDA scaler, CPU/bellek) |
| **Job** | Çalışıp biten iş: zamanlanmış, olay tetiklemeli veya manuel |

---

### 2. Neden?

- **Sınav:** *Implement container-orchestrated solutions* hedefi tam olarak buradadır: revision/trafik senaryoları, KEDA kural seçimi ve `az containerapp` komutları AI-200'ün sevdiği "senaryo + CLI tamamlama" kalıbıdır.
- **Gerçek işte:** AKS'in operasyon yükü (node, upgrade, ağ eklentileri) olmadan, event-driven mikroservisleri **sıfıra kadar ölçekleyerek** ve kullandığın kadar ödeyerek çalıştırırsın. Canary ve geri alma, revision + trafik bölme ile kod yazmadan yapılır.
- **Maliyet:** Boştaki worker'ın sıfır replica'ya inmesi (**scale-to-zero**) ve tüketim bazlı faturalama gece/hafta sonu yükünü neredeyse sıfırlar.

---

### 3. Nasıl?

#### 3.1 Environment ⭐

- **Environment**, uygulamaların aynı sanal ağ ve log hedefini paylaştığı sınırdır; uygulamalar aynı environment içinde **iç DNS** ile birbirine ulaşır. Varsayılan olarak tüm uygulamaların konsol/sistem logları ortak bir **Log Analytics** çalışma alanına gider.
- **Environment türleri:**

| Tür | Plan | Not |
|---|---|---|
| **Workload profiles** (yeni ortamlarda varsayılan) | **Consumption** (tüketim bazlı) **ve Dedicated** (ayrılmış VM profilleri, GPU dahil) | UDR, daha büyük kaynak boyutları; Dedicated'da ortam için sabit yönetim ücreti vardır |
| **Consumption-only** (legacy) | Yalnızca Consumption | Uygulama başına sınırlı CPU/bellek tavanı (yaklaşık 4 vCPU / 8 GiB); ortamın kendisi ücretsiz, sadece uygulamalar faturalanır |

- Yeni ortam oluştururken workload profiles kullan; sınavda "GPU / büyük kaynak / dedicated" geçiyorsa **Dedicated workload profile** cevaptır.
- **Container Apps Jobs** *(bağlam)*: tetikleyici `Schedule` (cron), `Event` (KEDA scaler'ı ile, kuyruğa mesaj gelince) veya `Manual`. Sürekli çalışan servis değil, bitip kapanan iş içindir.
- **Dapr** *(bağlam)*: istenirse sidecar olarak etkinleştirilir (service invocation, pub/sub, state); sınavın ana odağı değildir.

#### 3.2 Uygulama yapılandırması

| Alan | Ne? | Not |
|---|---|---|
| **Ingress** | `external` (internetten) veya `internal` (yalnızca environment/VNet); **target port** = container'ın dinlediği port | Port uyuşmazlığı = 502/timeout; ingress yoksa uygulama yalnızca dahili çağrı/worker'dır |
| **Env var** | Düz değer veya `secretref:` ile secret'a referans | Env var değişikliği **yeni revision** üretir |
| **Secret** | Uygulama düzeyinde saklanan değerler; **Key Vault referansı** + managed identity ile bağlanabilir (→ [C2.11](#41-managed-identity-ve-azure-key-vault)) | Secret değişikliği **yeni revision oluşturmaz**; çalışan revision'ın yeni değeri alması için restart gerekir |
| **Registry kimliği** | `--registry-server` + `--registry-identity` (managed identity) → `AcrPull` | Admin parolası yerine **kimlik tabanlı çekme** |
| **Managed identity** | System- veya user-assigned | Registry, Service Bus, Key Vault, KEDA kimlik doğrulaması için |

> **Registry kimliği ve ilk dağıtım:** system-assigned identity uygulama **oluşturulduktan sonra** doğar; ama image çekmek için identity daha ilk revision'da gerekir. Bu yüzden pratikte **user-assigned identity** (önceden oluşturulup `AcrPull` verilmiş) kullanılır.

#### 3.3 Revision yönetimi ⭐

**Revision** = uygulamanın değişmez bir sürümüdür. Hangi değişikliğin yeni revision ürettiği sınavın klasik tuzağıdır:

| Kapsam | Örnekler | Yeni revision? |
|---|---|---|
| **Revision-scope** (`properties.template`) | Image/tag, container ayarları (CPU, bellek, env var, probe), **scale kuralları**, revision suffix | **Evet** |
| **Application-scope** (`properties.configuration`) | **Secret'lar**, ingress ayarları, **trafik bölme kuralları**, revision mode, registry bilgisi | **Hayır** — tüm revision'lara geçerli |

**Revision mode** ⭐:

| | **Single** (varsayılan) | **Multiple** |
|---|---|---|
| Aktif revision | Tek; yenisi **hazır olunca** (provisioning, replica, startup/readiness probe) trafik ona geçer, eskisi kapatılır | **Birden çok** aktif revision |
| Trafik | %100 etkin revision'a | **Yüzdelerle bölünür** (toplam **%100** olmalı) |
| Kullanım | Sıfır kesintili rolling güncelleme | **Canary / blue-green**, A/B, revision label'ları, geri alma |
| Dikkat | Trafik bölme yok | Aktif revision'lar **kaynak/ücret** tüketir; kullanılmayanları **deactivate** et |
| Olay güdümlü (HTTP olmayan) worker | **Single önerilir** | Birden çok aktif revision kuyruğu paralel tüketir |

- **Revision label:** bir revision'a kalıcı bir isim/URL verir (`canary`, `stable`); label'ı başka bir revision'a **taşıyarak** trafiği değiştirirsin. Bir label aynı anda tek revision'dadır. Trafik kuralı `--revision-weight` (isimle) veya `--label-weight` (label ile) verilir.
- Revision adı: `<app>--<suffix>` (suffix verilmezse rastgele).
- **Geri alma** = trafiği önceki (hâlâ aktif) revision'a döndürmek; kod değil **trafik kuralı** değişikliğidir. Bu yüzden canary süresince eski revision'ı kapatma.

```text
Single mode:    v1 (%100) ──yeni image──▶ v2 hazır olunca v2 (%100), v1 kapanır
Multiple mode:  v1 (%90) + v2 (%10) ──metrikler iyi──▶ v2 (%100) ──▶ v1 deactivate
                                     └──hata artışı──▶ v1 (%100)  (geri alma)
```

#### 3.4 KEDA ile ölçekleme ⭐

- Her container app'in `min-replicas` / `max-replicas` değeri vardır. **`min-replicas 0`** = **scale-to-zero** (hiç replica yok, maliyet yok; ilk istek/mesajda **soğuk başlangıç** gecikmesi). Kural tanımlamazsan uygulama varsayılan olarak HTTP eşzamanlılığına göre ölçeklenir (min 0, max 10, replica başına ~10 eşzamanlı istek — güncel varsayılanları doğrula).
- Birden çok kural varsa replica sayısı, **en çok replica isteyen kurala** göre belirlenir.

| Kural türü | Neye bakar? | Tipik kullanım | Sıfıra inebilir mi? |
|---|---|---|---|
| **HTTP** | Replica başına **eşzamanlı istek** (`concurrentRequests`) | Web API | ✔ (varsayılan) |
| **TCP** | Eşzamanlı TCP bağlantı | TCP servisleri | ✔ |
| **Azure Service Bus** (KEDA `azure-servicebus`) | **Kuyruk/abonelik mesaj sayısı** (`messageCount`) | Sipariş worker'ı | ✔ |
| **Azure Storage Queue** (`azure-queue`) | `queueLength` | Basit kuyruk worker'ı | ✔ |
| **Custom** | Herhangi bir KEDA scaler (Kafka, Event Hubs, Cosmos…) | Özel kaynaklar | ✔ |
| **CPU / bellek** | Kullanım yüzdesi | Hesaplama yoğun iş | ✘ **Sıfıra inemez** (en az 1 replica) |

- **Service Bus kuralı:** `messageCount`, **replica başına hedef mesaj sayısıdır**: 500 mesaj, `messageCount=20` → ~25 replica istenir (max'ı aşamaz). Topic aboneliği için `queueName` yerine `topicName` + `subscriptionName`.
- **Kimlik doğrulama:** (1) **secret** (`--scale-rule-auth connection=<secret-adı>`) veya (2) **managed identity** (kural şemasında `identity: system` ya da user-assigned identity'nin resource ID'si; kimliğe hedefte *Azure Service Bus Data Receiver* gibi rol gerekir). Üretimde secret'sız kimlik tercih edilir.
- **Zamanlama:** özel KEDA kurallarında varsayılan yoklama aralığı **30 sn**, **cooldown 300 sn** (yalnızca son replica'dan sıfıra inerken geçerli) — bu yüzden mesajlar bitince sıfıra inmesi birkaç dakika sürer.
- Scale kuralı değişikliği **revision-scope**'tur → yeni revision. HTTP olmayan (olay güdümlü) worker'lar için **single revision mode** önerilir.

#### 3.5 Lab — Digest ile dağıt, %10 canary, geri al, KEDA ile ölçekle

Maliyet: Consumption planında kullanılan kadar; Service Bus **Basic/Standard** namespace kuruşluk; sıfıra inen worker neredeyse ücretsizdir. Bütçe alarmı kur (→ C1.1). [C2.1](#11-azure-container-registry-ve-acr-tasks) labındaki `$ACR` ve digest'i kullan (aksi halde `az acr import` ile örnek bir image al).

```bash
RG=rg-ai200-c22; LOC=westeurope
ENV=cae-ai200; ID=id-aca-pull
az group create --name $RG --location $LOC
az extension add --name containerapp --upgrade     # CLI extension
az provider register --namespace Microsoft.App      # one-time resource provider registration

# 1) Environment (workload profiles by default; a Log Analytics workspace is created automatically)
az containerapp env create --name $ENV --resource-group $RG --location $LOC

# 2) User-assigned identity with AcrPull (registry from C2.1)
ID_RES=$(az identity create -g $RG -n $ID --query id -o tsv)
ID_PRINCIPAL=$(az identity show -g $RG -n $ID --query principalId -o tsv)
az role assignment create --assignee-object-id $ID_PRINCIPAL --assignee-principal-type ServicePrincipal \
  --role AcrPull --scope $(az acr show --name $ACR --query id -o tsv)

# 3) Deploy BY DIGEST, pull with the managed identity (no admin user)
az containerapp create --name todoapi --resource-group $RG --environment $ENV \
  --image $ACR.azurecr.io/todoapi@$DIGEST \
  --registry-server $ACR.azurecr.io --registry-identity $ID_RES --user-assigned $ID_RES \
  --target-port 8080 --ingress external --min-replicas 1 --max-replicas 5 \
  --revision-suffix v1 --env-vars APP_VERSION=1
FQDN=$(az containerapp show -n todoapi -g $RG --query properties.configuration.ingress.fqdn -o tsv)

# 4) Multiple revision mode, then a new revision (revision-scope change: image + env var) and a 90/10 split
az containerapp revision set-mode --name todoapi --resource-group $RG --mode multiple
az containerapp update --name todoapi --resource-group $RG \
  --image $ACR.azurecr.io/todoapi@$DIGEST_V2 --revision-suffix v2 --set-env-vars APP_VERSION=2
az containerapp ingress traffic set --name todoapi --resource-group $RG \
  --revision-weight todoapi--v1=90 todoapi--v2=10          # weights must sum to 100
az containerapp revision list -n todoapi -g $RG -o table    # both active; see TRAFFIC WEIGHT column

# 5) Observe: ~10% of requests should hit v2 (Portal > Metrics > Requests split by Revision, or logs)
for i in $(seq 1 50); do curl -s https://$FQDN/version; echo; done | sort | uniq -c
az containerapp logs show -n todoapi -g $RG --type console --tail 20

# 6a) Metrics fine -> promote and retire v1
az containerapp ingress traffic set -n todoapi -g $RG --revision-weight todoapi--v2=100
az containerapp revision deactivate -n todoapi -g $RG --revision todoapi--v1
# 6b) ...or problems -> roll back (v1 must still be active)
# az containerapp ingress traffic set -n todoapi -g $RG --revision-weight todoapi--v1=100

# 7) Secrets are application-scope: changing one does NOT create a revision
az containerapp secret set -n todoapi -g $RG --secrets api-key=rotated-value
az containerapp revision restart -n todoapi -g $RG --revision todoapi--v2   # running replicas pick the new value up

# 8) KEDA: queue worker scaling on Service Bus queue length (0..10)
SB=sbai200$RANDOM
az servicebus namespace create -g $RG -n $SB --location $LOC --sku Standard
az servicebus queue create -g $RG --namespace-name $SB -n orders
CONN=$(az servicebus namespace authorization-rule keys list -g $RG --namespace-name $SB \
  -n RootManageSharedAccessKey --query primaryConnectionString -o tsv)

az containerapp create --name orderworker --resource-group $RG --environment $ENV \
  --image $ACR.azurecr.io/orderworker:1.0.0 \
  --registry-server $ACR.azurecr.io --registry-identity $ID_RES --user-assigned $ID_RES \
  --secrets sb-conn="$CONN" --env-vars SB_CONNECTION=secretref:sb-conn \
  --min-replicas 0 --max-replicas 10 \
  --scale-rule-name orders-queue --scale-rule-type azure-servicebus \
  --scale-rule-metadata queueName=orders namespace=$SB messageCount=20 \
  --scale-rule-auth connection=sb-conn

# 9) Send 500 messages (see snippets below), then watch replicas grow and drop back to 0
az servicebus queue show -g $RG --namespace-name $SB -n orders --query countDetails.activeMessageCount
az containerapp replica list -n orderworker -g $RG -o table      # repeat every ~30 s

# 10) Cleanup (also deletes the Service Bus namespace and the environment)
az group delete --name $RG --yes --no-wait
```

**Managed identity ile scale kuralı (YAML, `az containerapp update --yaml`):**

```yaml
properties:
  template:
    scale:
      minReplicas: 0
      maxReplicas: 10
      rules:
        - name: orders-queue
          custom:
            type: azure-servicebus
            metadata:
              queueName: orders
              namespace: sbai200example        # namespace name, without .servicebus.windows.net
              messageCount: "20"
            identity: system                   # or the user-assigned identity's resource ID
```

**C# (.NET 10, `Azure.Messaging.ServiceBus`) — 500 mesaj gönder:**

```csharp
using Azure.Identity;
using Azure.Messaging.ServiceBus;

await using var client = new ServiceBusClient(
    $"{Environment.GetEnvironmentVariable("SB")}.servicebus.windows.net", new DefaultAzureCredential());
await using ServiceBusSender sender = client.CreateSender("orders");

for (int i = 0; i < 5; i++)
{
    using ServiceBusMessageBatch batch = await sender.CreateMessageBatchAsync();
    for (int j = 0; j < 100; j++)
        batch.TryAddMessage(new ServiceBusMessage($"{{\"orderId\":{i * 100 + j}}}"));
    await sender.SendMessagesAsync(batch);
}
Console.WriteLine("Sent 500 messages.");
```

**Python eşdeğeri (`azure-servicebus`, `azure-identity`):**

```python
import os
from azure.identity import DefaultAzureCredential
from azure.servicebus import ServiceBusClient, ServiceBusMessage

with ServiceBusClient(f"{os.environ['SB']}.servicebus.windows.net", DefaultAzureCredential()) as client:
    with client.get_queue_sender("orders") as sender:
        for i in range(5):
            batch = sender.create_message_batch()
            for j in range(100):
                batch.add_message(ServiceBusMessage(f'{{"orderId":{i * 100 + j}}}'))
            sender.send_messages(batch)
print("Sent 500 messages.")
```

> Gönderen kimliğe *Azure Service Bus Data Sender* rolü gerekir. Mesaj işleme (`ServiceBusProcessor`, DLQ) → [C2.9](#32-azure-service-bus-kuyruk-topic-ve-dead-letter).

> **Alıştırma.** `messageCount=20`, `maxReplicas=10`, kuyrukta 500 mesaj var. Kaç replica beklenir? Kuyruk boşalınca ne olur ve ne kadar sürede?
>
> <details><summary>Çözüm</summary>
>
> İstenen replica ≈ ceil(500 / 20) = 25 → **max 10** ile sınırlanır; 10 replica çalışır. Kuyruk boşalınca kural 0 replica ister; ancak **cooldown (varsayılan 300 sn)** dolana kadar son replica kapanmaz, sonra **0'a iner**. Daha hızlı boşaltmak için `maxReplicas` veya replica başına işleme hızı artırılır; sıfıra inmeyi hızlandırmak için cooldown (`cooldownPeriod`) kısaltılır.
> </details>

#### 3.6 Log ve teşhis

- `az containerapp logs show -n APP -g RG --type console|system [--follow]` canlı log; portalda **Log stream**.
- Log Analytics'te iki tablo: **`ContainerAppConsoleLogs_CL`** (stdout/stderr; kolonlar `ContainerAppName_s`, `RevisionName_s`, `ContainerGroupName_g`, `Log_s`) ve **`ContainerAppSystemLogs_CL`** (platform olayları: image çekme, probe, restart). Kolon adları kayıt şemasına göre değişebilir; sorgudan önce `getschema` ile doğrula.

```kusto
ContainerAppConsoleLogs_CL
| where ContainerAppName_s == "todoapi" and TimeGenerated > ago(30m)
| where Log_s contains "error"
| project TimeGenerated, RevisionName_s, Log_s
| order by TimeGenerated desc
```

Container Apps / AKS / App Service karşılaştırması ve AKS teşhisi → [C2.3](#13-akse-manifest-ile-dağıtım-ve-container-teşhisi).

---

### 4. Gerçek dünya senaryosu

Bir lojistik şirketi sipariş API'si ve gece yoğunlaşan bir sipariş-işleme worker'ı çalıştırıyor:

| İhtiyaç | Karar | Neden |
|---|---|---|
| Yeni sürüm riskli; önce küçük bir kesime | **Multiple revision mode** + `%10` canary | Hata artarsa trafik tek komutla v1'e döner |
| Canary'yi QA'nın doğrudan test etmesi | Revision **label**'ı (`canary`) | Kalıcı URL; trafik almadan test edilir |
| Worker kuyruk dolunca hızla büyümeli, boşken sıfır olmalı | **Service Bus KEDA kuralı**, min 0 / max 10 | Mesaj sayısına göre ölçek, boşta maliyet yok |
| API'nin ilk istek gecikmesi kritik | `min-replicas 1` | Scale-to-zero soğuk başlangıcı yok |
| Image'ın tag'i yanlışlıkla değişmesin | **Digest ile** dağıtım | Değişmez referans (→ C2.1) |
| Parola yönetimi istenmiyor | Managed identity ile ACR çekme + Service Bus erişimi | Secret yok |
| Gece 02:00 raporu | **Container Apps Job** (cron) | Sürekli çalışan servis gerekmiyor |
| Takım Kubernetes bilgisi/yönetim kapasitesi sınırlı | ACA (AKS değil) | Küme operasyonu yok |

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryodan revision mode ve trafik kuralı çıkarma, "bu değişiklik yeni revision üretir mi?", hangi KEDA kuralının hangi metriğe bağlanacağı ve `az containerapp` komutunu tamamlama.

| Senaryodaki ifade | Cevap |
|---|---|
| "Yeni sürümü önce %10 kullanıcıya" | **Multiple** revision mode + trafik ağırlığı |
| "Sorun çıkarsa hemen eski sürüme dön" | Trafiği önceki aktif revision'a ver |
| "Revision'a sabit test URL'si" | **Revision label** |
| "Kuyruktaki mesaj sayısına göre ölçek" | KEDA **`azure-servicebus`** (veya `azure-queue`) kuralı |
| "Boşta sıfır maliyet" | `min-replicas 0` (scale-to-zero) |
| "İlk istekte gecikme olmasın" | `min-replicas ≥ 1` |
| "CPU kuralıyla sıfıra ölçekle" | **Mümkün değil** |
| "Secret değiştirdim, yeni revision oluştu mu?" | **Hayır** (application-scope); restart gerekir |
| "Env var / image / scale kuralı değişti" | **Evet**, yeni revision |
| "Image'ı parolasız çek" | Managed identity + **`AcrPull`** + `--registry-identity` |
| "GPU / büyük kaynak" | **Dedicated workload profile** |
| "Zamanlanmış, biten iş" | **Container Apps Job** |

**Tuzaklar:**
- Trafik ağırlıklarının toplamı **%100** olmalıdır; `v1=90 v2=20` geçersizdir.
- **Single** modda trafik bölünemez; canary için önce **multiple** moda geç.
- Scale kuralı bir **revision-scope** değişikliktir (yeni revision), ingress/secret/trafik kuralı **değildir**.
- KEDA `messageCount` **toplam** değil, **replica başına** hedeftir.
- Bir revision'ı trafiksiz bırakmak onu kapatmaz; kapatmak için **deactivate** gerekir, aksi halde ücret/kaynak harcar.
- Scale-to-zero soğuk başlangıç getirir; gecikmeye duyarlı HTTP için min ≥ 1.
- Kural kimlik doğrulaması için kimliğe hedef kaynakta **data-plane rolü** (ör. Service Bus Data Receiver) gerekir.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Canary için single modda kalmak | `az containerapp revision set-mode --mode multiple` |
| Canary sürerken eski revision'ı kapatmak | Geri alma için v1 aktif kalmalı; promote sonrası deactivate et |
| Secret'ı değiştirip uygulamanın yeni değeri "kendiliğinden" göreceğini sanmak | Revision restart (veya yeni revision) gerekir |
| Target port'u yanlış vermek | `--target-port` container'ın dinlediği portla aynı olmalı (ASP.NET Core 8+ varsayılan **8080**) |
| System-assigned identity ile ilk revision'da ACR'dan çekmeye çalışmak | Önceden oluşturulmuş **user-assigned** identity + `AcrPull` |
| Worker'a HTTP ingress ve HTTP scale rule koymak | Kuyruk worker'ı ingress'siz, kuyruk tabanlı KEDA kuralı |
| `max-replicas`'ı unutup maliyet/sınır patlatmak | Her kurala uygun `max` koy; downstream (DB, Service Bus) kapasitesini hesaba kat |
| Aktif ama kullanılmayan çok sayıda revision | Multiple mode'da eski revision'ları deactivate et |
| `:latest` ile dağıtmak | Digest veya benzersiz tag; revision gerçekten değişsin ve izlensin |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Scale-to-zero (min 0) | Boşta maliyet ~0 | **Soğuk başlangıç** gecikmesi (image çekme + uygulama başlatma) |
| `min-replicas ≥ 1` | Sıcak, düşük gecikme | Sürekli (azaltılmış "idle") ücret |
| Düşük `messageCount` | Hızlı boşaltma | Çok replica, daha yüksek maliyet / downstream baskısı |
| Yüksek `messageCount` | Az replica, ucuz | Mesajlar daha uzun bekler |
| Multiple mode + canary | Güvenli yayın, anında geri alma | Eski/yeni revision birlikte çalışır (çift kaynak, şema uyumluluğu ister) |
| Consumption plan | Kullandığın kadar öde | Üst kaynak sınırı; GPU/çok büyük iş yükü için Dedicated |
| Dedicated workload profile | Öngörülebilir kaynak, GPU, büyük boyut | Sabit ortam/instance ücreti |
| ACA vs AKS | Operasyon yükü yok, KEDA/Dapr hazır | Kubernetes API/CRD/özel ağ eklentileri erişimi sınırlı |

---

### 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Azure Container Apps overview](https://learn.microsoft.com/en-us/azure/container-apps/overview) · [Environments](https://learn.microsoft.com/en-us/azure/container-apps/environment) · [Workload profiles](https://learn.microsoft.com/en-us/azure/container-apps/workload-profiles-overview)
- [Revisions](https://learn.microsoft.com/en-us/azure/container-apps/revisions) · [Traffic splitting](https://learn.microsoft.com/en-us/azure/container-apps/traffic-splitting) · [Revision labels](https://learn.microsoft.com/en-us/azure/container-apps/labels)
- [Set scaling rules](https://learn.microsoft.com/en-us/azure/container-apps/scale-app) · [KEDA Azure Service Bus scaler](https://keda.sh/docs/latest/scalers/azure-service-bus/)
- [Managed identities in Container Apps](https://learn.microsoft.com/en-us/azure/container-apps/managed-identity) · [Pull images with managed identity](https://learn.microsoft.com/en-us/azure/container-apps/managed-identity-image-pull)
- [Monitor logs with Log Analytics](https://learn.microsoft.com/en-us/azure/container-apps/log-monitoring) · [Jobs](https://learn.microsoft.com/en-us/azure/container-apps/jobs)
- [`az containerapp` CLI reference](https://learn.microsoft.com/en-us/cli/azure/containerapp)


### 📝 Bölüm 1.2 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir ekip, yeni bir sürümü önce trafiğin %10'una göstermek, sorun yoksa %100'e çıkarmak istiyor. Container app şu an varsayılan revision mode'da. İlk adım ne olmalıdır?

- A) `az containerapp ingress traffic set --revision-weight latest=10`
- B) `az containerapp revision set-mode --mode multiple`
- C) `az containerapp update --min-replicas 0`
- D) `az containerapp secret set`

<details><summary>Cevap</summary>

**B.** Varsayılan mod **single**'dır ve trafik bölmeyi desteklemez. Önce **multiple** moda geçilir; sonra trafik ağırlıkları verilir (A bu adımdan sonra gelir).
</details>

#### Soru 2: Evet / Hayır
Aşağıdaki değişikliklerden hangisi **yeni bir revision** oluşturur?

| Değişiklik | Yeni revision |
|---|---|
| a) Container image'ının değiştirilmesi | ☐ Evet ☐ Hayır |
| b) Secret değerinin güncellenmesi | ☐ Evet ☐ Hayır |
| c) Scale kuralının (ör. `messageCount`) değiştirilmesi | ☐ Evet ☐ Hayır |
| d) Trafik ağırlıklarının değiştirilmesi | ☐ Evet ☐ Hayır |
| e) Environment variable eklenmesi | ☐ Evet ☐ Hayır |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet · d) Hayır · e) Evet.** `template` bölümündeki (revision-scope) değişiklikler — image, env var, kaynaklar, scale kuralları, suffix — yeni revision üretir. Secret, ingress ve trafik bölme kuralları application-scope'tur; tüm revision'lara uygulanır, yeni revision doğurmaz (secret değişince çalışan replica'lar için restart gerekir).
</details>

#### Soru 3: Tek seçim
Multiple revision mode'da `todoapi--v1` %90, `todoapi--v2` %20 verildi. Ne olur?

- A) Trafik 90/20 oranında bölünür
- B) Komut, toplam ağırlık %100 olmadığı için geçersizdir
- C) v2 otomatik %10'a düşürülür
- D) v1 devre dışı kalır

<details><summary>Cevap</summary>

**B.** Trafik kurallarının toplamı **%100** olmalıdır. Sistem değerleri kendiliğinden düzeltmez.
</details>

#### Soru 4: Tek seçim
Bir order worker'ı Service Bus kuyruğundaki mesajlara göre ölçeklenmeli ve kuyruk boşken **hiç replica çalışmamalı**. Hangi yapılandırma doğrudur?

- A) CPU scale rule (%70), `min-replicas 0`
- B) `azure-servicebus` KEDA kuralı, `min-replicas 0`
- C) HTTP concurrency kuralı, `min-replicas 1`
- D) Memory kuralı, `min-replicas 0`

<details><summary>Cevap</summary>

**B.** Kuyruk uzunluğu KEDA `azure-servicebus` scaler'ı ile ölçülür ve scale-to-zero (`min-replicas 0`) mümkündür. **CPU ve bellek kuralları sıfıra inemez** (A, D); HTTP kuralı kuyruğu görmez ve min 1 sıfıra izin vermez (C).
</details>

#### Soru 5: CLI tamamlama
Service Bus kuyruğu `orders` için, **replica başına 20 mesaj** hedefleyen bir kuralı tamamla.

```bash
az containerapp create --name orderworker --resource-group rg --environment cae \
  --image myacr.azurecr.io/orderworker:1.0.0 --min-replicas 0 --max-replicas 10 \
  --secrets sb-conn="$CONN" \
  --scale-rule-name orders-queue --scale-rule-type azure-servicebus \
  --scale-rule-metadata queueName=orders namespace=mysb ____=20 \
  --scale-rule-auth connection=sb-conn
```

- A) `queueLength`
- B) `messageCount`
- C) `concurrentRequests`
- D) `targetValue`

<details><summary>Cevap</summary>

**B.** KEDA Service Bus scaler'ında replica başına hedef mesaj sayısı `messageCount`'tur. `queueLength` Storage Queue scaler'ının, `concurrentRequests` HTTP kuralının parametresidir.
</details>

#### Soru 6: Hesaplama
Kuyrukta 500 mesaj var, `messageCount=20`, `maxReplicas=10`. KEDA kuralı kaç replica'ya ölçekler?

- A) 5
- B) 10
- C) 25
- D) 500

<details><summary>Cevap</summary>

**B.** İstenen ≈ ceil(500/20) = 25; ancak `maxReplicas=10` üst sınırdır, dolayısıyla 10 replica çalışır.
</details>

#### Soru 7: Tek seçim
Bir container app `ACR`'dan image'ı managed identity ile çekecek. **En az yetki** ve ilk revision'da çalışması için hangisi doğrudur?

- A) System-assigned identity'ye `Contributor`, uygulama oluşturulduktan sonra
- B) Önceden oluşturulmuş user-assigned identity'ye registry kapsamında `AcrPull`, uygulama bu identity ve `--registry-identity` ile oluşturulur
- C) Registry'de admin user'ı açıp parolayı env var'a koymak
- D) Identity'ye `AcrPush`

<details><summary>Cevap</summary>

**B.** System-assigned identity uygulama oluşturulduktan sonra var olur; ilk revision'ın image'ı çekmesi için user-assigned identity önceden hazırlanır. `Contributor` (A) ve `AcrPush` (D) gereğinden fazla yetkidir; admin user (C) paylaşılan parolaya dayanır.
</details>

#### Soru 8: Çoklu seçim (iki doğru cevap)
Bir revision label'ı (`canary`) için **hangi iki** ifade doğrudur?

- A) Revision'a kalıcı bir URL verir
- B) Aynı label aynı anda birden çok revision'da bulunabilir
- C) Label'ı başka bir revision'a taşıyarak trafik yönlendirmesi değiştirilebilir
- D) Label, uygulamayı single moda zorlar

<details><summary>Cevap</summary>

**A ve C.** Label revision'a sabit bir URL tanımlar ve `--label-weight` ile trafik kuralında kullanılabilir. Bir label tek revision'dadır (B yanlış); label'lar multiple mod ile kullanışlıdır (D yanlış).
</details>

#### Soru 9: Senaryo — canary sonrası geri alma
`todoapi--v1` (%90) ve `todoapi--v2` (%10) aktif. v2'de 5xx oranı arttı. **En hızlı** ve güvenli eylem hangisidir?

- A) `todoapi--v1`'i deactivate etmek
- B) Trafiği `todoapi--v1=100` olarak ayarlamak
- C) Uygulamayı silip yeniden oluşturmak
- D) `max-replicas`'ı 0 yapmak

<details><summary>Cevap</summary>

**B.** Geri alma = trafik kuralını eski aktif revision'a çevirmektir (`az containerapp ingress traffic set --revision-weight todoapi--v1=100`). A tersine, sağlıklı revision'ı kapatır. C ve D gereksiz yıkıcıdır.
</details>

#### Soru 10: Sıralama
Güvenli bir canary yayınının adımlarını doğru sıraya diz: (1) Trafiği v2'ye %100 ver, (2) Multiple mode'a geç, (3) v2'yi oluştur ve %10 trafik ver, (4) v1'i deactivate et, (5) v2 metriklerini/loglarını izle.

<details><summary>Cevap</summary>

**2 → 3 → 5 → 1 → 4.** Multiple mod; v2 revision'ı ve %10 trafik; izleme; sağlıklıysa %100; en son v1 kapatılır (geri alma imkânı o ana kadar korunur).
</details>

#### Soru 11: Tek seçim
Bir HTTP API'nin sabah ilk isteğinde birkaç saniyelik gecikme yaşanıyor; uygulama gece sıfır replica'ya iniyor. En uygun çözüm?

- A) Revision mode'u multiple yapmak
- B) `min-replicas`'ı 1 yapmak
- C) Scale kuralını CPU'ya çevirmek
- D) Ingress'i internal yapmak

<details><summary>Cevap</summary>

**B.** Gecikme **scale-to-zero soğuk başlangıcıdır**; en az bir sıcak replica tutmak bunu giderir (karşılığında sürekli, azaltılmış idle ücreti). Diğer seçenekler soğuk başlangıcı etkilemez; CPU kuralı ise zaten sıfıra inemez, yani sorunu çözmek yerine maliyeti değiştirir.
</details>

#### Soru 12: Kod tamamlama (YAML)
Scale kuralı **secret yerine managed identity** ile kimlik doğrulamalı. Boşluğu doldur.

```yaml
rules:
  - name: orders-queue
    custom:
      type: azure-servicebus
      metadata:
        queueName: orders
        namespace: mysb
        messageCount: "20"
      ____: system
```

- A) `auth`
- B) `identity`
- C) `credential`
- D) `principal`

<details><summary>Cevap</summary>

**B.** Kural şemasında `auth` (secret referansı) yerine `identity` kullanılır; değeri `system` veya user-assigned identity'nin resource ID'sidir. Kimliğe hedefte *Azure Service Bus Data Receiver* gibi bir rol verilmelidir.
</details>

#### Soru 13: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Consumption-only environment'ta GPU kullanılabilir. | ☐ | ☐ |
| b) Aynı environment'taki uygulamalar aynı Log Analytics çalışma alanına log gönderir (varsayılan). | ☐ | ☐ |
| c) Container Apps Job, cron ile zamanlanabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** GPU ve büyük kaynak boyutları için workload profiles ortamında Dedicated profil gerekir. Environment ortak log hedefini paylaşır; Job'lar `Schedule`, `Event` veya `Manual` tetiklenebilir.
</details>

#### Soru 14: CLI tamamlama
`todoapi--v2` revision'ını kalıcı olarak trafik dışı bırakmak ve kaynak tüketmesini durdurmak için:

```bash
az containerapp revision ____ --name todoapi --resource-group rg --revision todoapi--v2
```

- A) `delete`
- B) `deactivate`
- C) `suspend`
- D) `stop`

<details><summary>Cevap</summary>

**B.** `az containerapp revision deactivate` revision'ı kapatır (replica'ları durur; gerekirse `activate` ile geri açılır). Trafik kuralından çıkarmak tek başına revision'ı kapatmaz.
</details>

#### Soru 15: Tek seçim
Bir geliştirici `az containerapp secret set` ile bir secret'ın değerini güncelledi, ama uygulama hâlâ eski değeri kullanıyor. Neden ve çözüm?

- A) Secret değişikliği yeni revision üretir; eski revision'ın kapanması beklenmeli
- B) Secret application-scope bir değişikliktir ve yeni revision oluşturmaz; çalışan replica'lar için revision restart gerekir
- C) Secret'lar yalnızca Key Vault'tan okunabilir
- D) Değişiklik yalnızca multiple modda geçerlidir

<details><summary>Cevap</summary>

**B.** Secret değişikliği yeni revision doğurmaz; çalışan replica'ların güncel değeri alması için revision restart (veya yeni revision) gerekir.
</details>

#### Soru 16: Tek seçim
Uygulama `8080` portunu dinliyor, ingress `--target-port 80` ile açıldı ve external. Dış istekler hata veriyor; loglar uygulamanın sağlıklı çalıştığını gösteriyor. En olası neden?

- A) Registry kimliği eksik
- B) Target port, container'ın dinlediği portla eşleşmiyor
- C) Scale kuralı yok
- D) Revision mode single

<details><summary>Cevap</summary>

**B.** Ingress'in `target-port`'u container'ın gerçekten dinlediği port olmalıdır. Registry kimliği sorunu image'ın hiç çalışmamasına yol açardı (loglar sağlıklı), diğerleri alakasızdır.
</details>

#### Soru 17: KQL tamamlama
Bir container app'in son 30 dakikadaki konsol loglarından hata içerenleri görmek istiyorsun.

```kusto
____
| where ContainerAppName_s == "todoapi" and TimeGenerated > ago(30m)
| where Log_s contains "error"
| project TimeGenerated, RevisionName_s, Log_s
```

- A) `ContainerAppSystemLogs_CL`
- B) `ContainerAppConsoleLogs_CL`
- C) `AzureActivity`
- D) `AppTraces`

<details><summary>Cevap</summary>

**B.** stdout/stderr çıktısı `ContainerAppConsoleLogs_CL` tablosundadır (`Log_s` mesaj kolonudur). Sistem tablosu platform olaylarını (image çekme, probe, restart) tutar; `AzureActivity` kontrol düzlemi işlemleridir.
</details>

#### Soru 18: Vaka çalışması
**Fabrikam Lojistik**, sipariş API'sini ve sipariş-işleme worker'ını Container Apps'e taşıyor. Gereksinimler: (1) API'nin yeni sürümleri önce %10 trafikle denenmeli ve gerektiğinde saniyeler içinde geri alınabilmeli; (2) worker `orders` kuyruğundaki mesaj sayısına göre ölçeklenmeli, boşken sıfıra inmeli ve Service Bus connection string'i saklanmamalı; (3) image'lar ACR'dan parolasız çekilmeli; (4) API'nin ilk isteği yavaş olmamalı.

**a)** (1) için doğru mod ve eylem?
- A) Single mode, her dağıtımda yeni revision
- B) Multiple mode, yeni revision'a `%10` ağırlık, eski revision aktif

**b)** (2) için doğru ölçekleme yapılandırması?
- A) `azure-servicebus` KEDA kuralı, `messageCount` hedefi, managed identity (`identity`) ile kimlik doğrulama, `min-replicas 0`
- B) CPU kuralı, `min-replicas 0`

**c)** (3) için?
- A) User-assigned identity + registry kapsamında `AcrPull` + `--registry-identity`
- B) ACR admin user

**d)** (4) için?
- A) API'de `min-replicas 1`
- B) API'de `min-replicas 0`

<details><summary>Cevap</summary>

**a) B · b) A · c) A · d) A.**
(a) Trafik bölme yalnızca multiple modda; geri alma, eski revision aktif olduğu sürece trafik kuralı değişikliğidir. (b) Kuyruk uzunluğu KEDA Service Bus scaler'ı ile ölçülür; CPU kuralı sıfıra inemez; `identity` ile connection string saklanmaz. (c) Parolasız ve en az yetkili çekme. (d) Soğuk başlangıcı önlemek için API'de en az bir sıcak replica; worker ise sıfıra inebilir.
</details>


---

## 1.3 AKS'e Manifest ile Dağıtım ve Container Teşhisi

> **Sınav Alanı:** Develop containerized solutions on Azure (%20-25) · **Konu Kodu:** C2.3

### 1. Ne?

**Azure Kubernetes Service (AKS)**, yönetilen Kubernetes'tir: **control plane'i (API server, etcd, scheduler) Azure yönetir**, sen **node pool**'ları (worker VM'leri) ve üzerlerinde çalışan iş yüklerini yönetirsin. Kubernetes'te her şey **bildirimsel manifest**'tir (YAML): "şu image'dan 3 kopya istiyorum" dersin, controller'lar gerçek durumu istenen duruma getirir. Bu konu Kubernetes temellerini (→ M14.3–M14.4) AI-200 sınavının bakış açısıyla birleştirir: **ACR'daki image'ı AKS'e manifest ile dağıtmak ve bozulunca teşhis etmek**.

| Manifest türü | Ne yapar? |
|---|---|
| **Deployment** | N kopya (replica) pod'u ve **rolling update** stratejisini yönetir (ReplicaSet üzerinden) |
| **Service** | Pod'lara sabit sanal IP/DNS: `ClusterIP` (küme içi), `LoadBalancer` (Azure Load Balancer ile dış IP), `NodePort` |
| **Ingress** | Host/yol bazlı HTTP yönlendirme (bir ingress controller gerektirir) |
| **ConfigMap** | Gizli olmayan yapılandırma |
| **Secret** | Gizli veri (base64 **kodlama**, şifreleme değil — varsayılan olarak) |
| **Resource request/limit** | Zamanlama (request) ve tavan (limit) |
| **Probe** | `liveness` (yeniden başlat), `readiness` (trafik al/alma), `startup` (yavaş açılış) |

```text
kubectl apply -f ──▶ API server ──▶ Deployment ──▶ ReplicaSet ──▶ Pod(lar) ◀── Service (selector: app=todoapi)
                                                                     ▲                  ▲
                                                       ConfigMap/Secret (env)        Ingress / LoadBalancer
```

---

### 2. Neden?

- **Sınav:** Hedef "manifest dosyalarıyla dağıt ve yönet" der; sorular genellikle **verilen manifest'teki hatayı bulma** (selector–label uyuşmazlığı, port, image), **hata mesajından kök nedene gitme** (`ImagePullBackOff`, `CrashLoopBackOff`, `OOMKilled`) ve `kubectl` komutu seçme biçimindedir.
- **Gerçek işte:** Kubernetes'te "pod çalışmıyor" sorununun %90'ı dört kalıba iner; sistematik bir teşhis sırası (`get → describe → logs → events`) saatler kazandırır.
- **Karar:** AKS en esnek ama en fazla operasyon yükü olan seçenektir; ne zaman Container Apps veya App Service'in yeterli olduğunu bilmek mimari kararın parçasıdır.

---

### 3. Nasıl?

#### 3.1 AKS, ACR entegrasyonu ve temel kubectl ⭐

| Adım | Komut | Not |
|---|---|---|
| Küme oluştur (ACR bağla) | `az aks create -g RG -n aks --attach-acr $ACR --node-count 2 --generate-ssh-keys` | Kubelet kimliğine registry'de **`AcrPull`** atar |
| Var olan kümeye ACR bağla | `az aks update -g RG -n aks --attach-acr $ACR` | Komutu çalıştıran kullanıcının rol atama yetkisi (Owner/User Access Administrator) gerekir |
| Kubeconfig al | `az aks get-credentials -g RG -n aks` | `~/.kube/config`'e context ekler (Entra entegre kümelerde `kubelogin` gerekebilir) |
| Uygula | `kubectl apply -f k8s/` | Bildirimsel; tekrar çalıştırılabilir (idempotent) |
| Rollout takibi | `kubectl rollout status deploy/todoapi` | Başarı/zaman aşımı |
| Geçmiş / geri al | `kubectl rollout history deploy/todoapi` · `kubectl rollout undo deploy/todoapi [--to-revision=N]` | Önceki ReplicaSet'e döner |
| Ölçekle | `kubectl scale deploy/todoapi --replicas=5` | Elle; otomatik için HPA (→ M14.4) |
| Image güncelle | `kubectl set image deploy/todoapi todoapi=<image>` | Yeni rollout başlatır |

- **AKS ↔ ACR:** `--attach-acr` ile image çekme **kubelet managed identity** üzerinden olur; pod'a `imagePullSecrets` gerekmez. Hata durumunda ilk bakılacak yer: kubelet kimliğinde `AcrPull` var mı, ACR adı/tag doğru mu?
- ABAC repository permissions modundaki registry'lerde `--attach-acr` desteklenmez; *Container Registry Repository Reader* rolünü elle ata (→ C2.1).
- AKS **Free** tier control plane ücretsizdir; ücreti **node VM'leri** oluşturur — laboratuvarın en pahalı kalemi budur, işin bitince kümeyi sil.
- *(Bağlam)* **AKS Automatic**, node ve ağ yönetimini daha çok Azure'a devreden, güvenlik/ölçek varsayılanları hazır bir mod; **Standard** tam kontrol sunar.

#### 3.2 Manifest'ler: Todo API

```yaml
# k8s/00-namespace.yaml
apiVersion: v1
kind: Namespace
metadata:
  name: todo
---
# k8s/10-config.yaml
apiVersion: v1
kind: ConfigMap
metadata:
  name: todoapi-config
  namespace: todo
data:
  ASPNETCORE_ENVIRONMENT: Production
  Logging__LogLevel__Default: Information
---
apiVersion: v1
kind: Secret
metadata:
  name: todoapi-secrets
  namespace: todo
type: Opaque
stringData:                       # plain text here; the API server stores it base64-encoded
  ConnectionStrings__Db: "Host=db;Database=todo;Username=app;Password=change-me"
```

```yaml
# k8s/20-deployment.yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: todoapi
  namespace: todo
spec:
  replicas: 2
  selector:
    matchLabels:
      app: todoapi                # MUST match the pod template labels below
  strategy:
    type: RollingUpdate
    rollingUpdate: { maxSurge: 1, maxUnavailable: 0 }
  template:
    metadata:
      labels:
        app: todoapi
    spec:
      containers:
        - name: todoapi
          image: myacr.azurecr.io/todoapi@sha256:REPLACE_WITH_DIGEST
          ports:
            - containerPort: 8080
          envFrom:
            - configMapRef: { name: todoapi-config }
          env:
            - name: ConnectionStrings__Db
              valueFrom:
                secretKeyRef: { name: todoapi-secrets, key: ConnectionStrings__Db }
          resources:
            requests: { cpu: 100m, memory: 128Mi }
            limits:   { cpu: 500m, memory: 256Mi }
          readinessProbe:
            httpGet: { path: /healthz, port: 8080 }
            initialDelaySeconds: 3
            periodSeconds: 5
          livenessProbe:
            httpGet: { path: /healthz, port: 8080 }
            initialDelaySeconds: 10
            periodSeconds: 10
```

```yaml
# k8s/30-service.yaml
apiVersion: v1
kind: Service
metadata:
  name: todoapi
  namespace: todo
spec:
  type: LoadBalancer              # ClusterIP for in-cluster only; use Ingress for host/path routing
  selector:
    app: todoapi                  # MUST match pod labels, otherwise the Service has no endpoints
  ports:
    - port: 80                    # Service port
      targetPort: 8080            # container port
```

Anahtar kurallar:
- `Deployment.spec.selector.matchLabels` ⊆ `template.metadata.labels`; uyuşmazsa `kubectl apply` **reddedilir** (`selector does not match template labels`). `selector` oluşturulduktan sonra **değiştirilemez**.
- `Service.spec.selector` pod label'larıyla eşleşmezse Service **endpoint'siz** kalır (bağlantı zaman aşımı).
- `targetPort` = container'ın gerçekte dinlediği port (burada 8080); `port` = Service'in dışarıya sunduğu port.
- **Requests** zamanlayıcının node seçerken baktığı garanti; **limits** tavan: bellek limitini aşan container **OOMKilled** olur, CPU limitini aşan **kısılır** (throttle, öldürülmez).
- **Readiness** başarısız olursa pod Service endpoint'lerinden çıkar (yeniden başlatılmaz); **liveness** başarısız olursa container **yeniden başlatılır**.
- **Secret** varsayılan olarak yalnızca base64'tür; gerçek koruma için RBAC, etcd şifreleme ve tercihen **Key Vault'tan Secrets Store CSI driver** (→ [C2.11](#41-managed-identity-ve-azure-key-vault)). Manifest'i Git'e secret değerleriyle koyma.

**Ingress** *(isteğe bağlı)*: `ingressClassName` ile bir controller seçilir.

```yaml
# k8s/40-ingress.yaml
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: todoapi
  namespace: todo
spec:
  ingressClassName: webapprouting.kubernetes.io   # AKS application routing add-on (NGINX-based class)
  rules:
    - host: todo.example.com
      http:
        paths:
          - path: /
            pathType: Prefix
            backend:
              service: { name: todoapi, port: { number: 80 } }
```

> **2026 notu:** Upstream **ingress-nginx** projesi Mart 2026'da bakım sonuna girdi; Microsoft, application routing add-on'undaki NGINX Ingress için **Kasım 2026'ya kadar** kritik güvenlik yamalarını desteklediğini duyurdu ve uzun vadeli yön olarak **Gateway API** (application routing'in Gateway API uygulaması veya **Application Gateway for Containers**) önerdi. Sınav hedefi Ingress kaynağını (ve genel yönlendirme fikrini) sorar; yeni projelerde Gateway API'yi düşün. Güncel durumu doğrula.

#### 3.3 Teşhis ⭐

**Sistematik sıra:** durum → ayrıntı → log → olay.

| Komut | Ne gösterir? |
|---|---|
| `kubectl get pods -n todo -o wide` | `STATUS`, `READY` (0/1), `RESTARTS`, hangi node |
| `kubectl describe pod <pod> -n todo` | **Events** (image çekme, probe, schedule), `State/Last State` (**Reason**, **Exit Code**), env, mount |
| `kubectl logs <pod> -n todo` · `--previous` · `-f` · `-c <container>` | Container logu; **çöken container'ın önceki çalışması için `--previous`** |
| `kubectl get events -n todo --sort-by=.lastTimestamp` | Namespace olayları (zamansal sıra) |
| `kubectl get endpoints todoapi -n todo` | Service'in arkasındaki **hazır pod IP'leri** (boşsa selector/readiness sorunu) |
| `kubectl exec -it <pod> -n todo -- sh` | Container içinde komut/ağ testi (shell yoksa `kubectl debug`) |
| `kubectl top pod -n todo` | CPU/bellek kullanımı (metrics-server ile) |

**Hata kalıbı → kök neden:**

| Gördüğün | Anlamı / olası kök neden | Kanıt | Çözüm |
|---|---|---|---|
| **`ErrImagePull` → `ImagePullBackOff`** | Image çekilemiyor: **yanlış repository/tag/digest**, **registry yetkisi yok** (kubelet kimliğinde `AcrPull` eksik), registry'ye ağ erişimi yok | `describe` → Events: `manifest unknown / not found` (tag yok) veya `unauthorized` / `401` (yetki) | Tag'i/digest'i düzelt; `az aks update --attach-acr`; ağ kuralları |
| **`CrashLoopBackOff`** | Container başlıyor ve **çöküyor**; kubelet artan beklemeyle yeniden başlatıyor. Neden: uygulama hatası, **eksik env var / secret**, yanlış bağlantı dizesi, yanlış komut, liveness probe'un sürekli başarısız olması | `logs --previous` (çökme nedeni), `describe` → `Last State: Terminated`, `Exit Code` | Config/secret'ı düzelt; kodu düzelt; probe'u gevşet |
| **`OOMKilled`** (Exit Code **137**) | Container **bellek limitini** aştı ve çekirdek tarafından öldürüldü | `describe` → `Reason: OOMKilled`; tekrar eden restart | Limit'i artır **veya** bellek sızıntısını düzelt; request/limit'i ölçülen kullanıma göre ayarla |
| **Pod `Running` ama `READY 0/1`** | **Readiness** başarısız: yanlış probe yolu/portu, uygulama hazır değil | `describe` → `Readiness probe failed: … connection refused / 404`; `get endpoints` boş | Probe path/port'u düzelt; `initialDelaySeconds`/startup probe |
| **`Pending`** | Zamanlanamıyor: yetersiz CPU/bellek (request çok yüksek), node selector/taint, PVC | `describe` → `FailedScheduling` (Insufficient cpu) | Request'i düşür, node ekle |
| **Service'e bağlanılamıyor** | Selector–label uyuşmazlığı, `targetPort` yanlış, hazır pod yok | `get endpoints` boş; `describe svc` | Selector/port düzelt |

> `CrashLoopBackOff` bir **belirti**dir, neden değil: asıl nedeni `logs --previous` ve `describe`'ın `Exit Code`'u söyler (1 = uygulama hatası, 137 = OOM/SIGKILL, 139 = segfault).

**Container Apps karşılığı:** `az containerapp logs show --type console|system`, portalda Log stream; Log Analytics'te `ContainerAppConsoleLogs_CL` (uygulama çıktısı) ve `ContainerAppSystemLogs_CL` (image çekme, probe, restart) (→ [C2.2](#12-azure-container-apps-ortam-revision-ve-keda)). AKS'te karşılığı: Container Insights / `ContainerLogV2` tablosu (→ [C2.13](#43-opentelemetry-ile-izleme-ve-kql-ile-sorun-giderme)).

#### 3.4 Container Apps mı, AKS mi, App Service mi? ⭐

| Kriter | **Container Apps** | **AKS** | **App Service for Containers** |
|---|---|---|---|
| Soyutlama | Serverless container; Kubernetes gizli | **Tam Kubernetes** API'si | PaaS web uygulaması (tek/çoklu container) |
| Operasyon yükü | **Düşük** | **Yüksek** (node, upgrade, ağ, güvenlik) | **Çok düşük** |
| Ölçekleme | HTTP + **KEDA** (scale-to-zero), HPA yok | HPA/KEDA/Cluster Autoscaler (**sen** kurarsın) | Plan düzeyinde scale out; sıfıra inmez |
| Özel Kubernetes (CRD, operator, DaemonSet, service mesh seçimi) | ✘ | ✔ | ✘ |
| Kullanım | Mikroservis, event-driven worker, API | Karmaşık/çok takımlı platform, özel ağ/GPU/stateful | Klasik web/API; basit container dağıtımı |
| Maliyet modeli | Kullanım bazlı (tüketim) | Node VM'leri (kullanılmasa da ödenir) | App Service planı (sabit) |

Kural: **Kubernetes API'sine/ekosistemine ihtiyaç yoksa Container Apps; hiç orkestrasyon gerekmiyorsa App Service; tam kontrol gerekiyorsa AKS.** (Tek container, orkestrasyon yok → Container Instances; → C1.5.)

#### 3.5 Lab — Dağıt, bilerek boz, teşhis et, geri al

Maliyet: 2 × `Standard_B2s` node birkaç saatlik laboratuvar için düşük maliyetlidir (güncel fiyatı Pricing Calculator'dan kontrol et); **işin bitince kümeyi sil**. Bütçe alarmı şart (→ C1.1). Yerel alternatif: `kind`/`k3d` kümesi (ACR'dan çekmek için `imagePullSecrets` veya `kind load docker-image` gerekir; bu yüzden bu lab AKS ile anlatılır).

```bash
RG=rg-ai200-c23; LOC=westeurope; AKS=aks-ai200
az group create --name $RG --location $LOC

# 1) Cluster with ACR attached (kubelet identity gets AcrPull). ACR + image come from C2.1.
az aks create --resource-group $RG --name $AKS --node-count 2 --node-vm-size Standard_B2s \
  --generate-ssh-keys --attach-acr $ACR
az aks get-credentials --resource-group $RG --name $AKS --overwrite-existing
kubectl get nodes

# 2) Deploy the good version
sed -e "s|myacr|$ACR|" -e "s|REPLACE_WITH_DIGEST|${DIGEST#sha256:}|" k8s/20-deployment.yaml > /tmp/20-deployment.yaml   # or edit by hand
kubectl apply -f k8s/00-namespace.yaml -f k8s/10-config.yaml
kubectl apply -f /tmp/20-deployment.yaml -f k8s/30-service.yaml
kubectl rollout status deploy/todoapi -n todo
kubectl get svc,pods,endpoints -n todo           # wait for EXTERNAL-IP, then: curl http://<ip>/healthz

# 3) BROKEN #1 — wrong image tag -> ErrImagePull / ImagePullBackOff
kubectl set image deploy/todoapi todoapi=$ACR.azurecr.io/todoapi:9.9.9 -n todo
kubectl get pods -n todo                          # new pod: ImagePullBackOff, old pods still serve (maxUnavailable: 0)
kubectl describe pod -n todo -l app=todoapi | grep -A6 "Events:"      # 'manifest ... not found'
kubectl rollout undo deploy/todoapi -n todo       # back to the previous ReplicaSet
kubectl rollout status deploy/todoapi -n todo

# 4) BROKEN #2 — missing secret key / bad env -> CrashLoopBackOff
kubectl set env deploy/todoapi -n todo ConnectionStrings__Db-        # remove the env var (trailing dash deletes it); assumes the API validates its config at startup
kubectl get pods -n todo -w                       # Error -> CrashLoopBackOff, RESTARTS increasing
POD=$(kubectl get pods -n todo -l app=todoapi --sort-by=.metadata.creationTimestamp -o name | tail -1)   # newest pod = the crashing one
kubectl logs $POD -n todo --previous              # app log: "ConnectionStrings:Db is not configured"
kubectl describe $POD -n todo | grep -E "Reason|Exit Code|Restart Count"
kubectl rollout undo deploy/todoapi -n todo

# 5) BROKEN #3 — wrong readiness port -> Running but 0/1 READY, no endpoints
kubectl patch deploy/todoapi -n todo --type=json \
  -p='[{"op":"replace","path":"/spec/template/spec/containers/0/readinessProbe/httpGet/port","value":8081}]'
kubectl get pods,endpoints -n todo                # READY 0/1, endpoints <none>
kubectl describe pod -n todo -l app=todoapi | grep -i "Readiness probe failed"
kubectl rollout undo deploy/todoapi -n todo

# 6) BROKEN #4 — memory limit too low -> OOMKilled (Exit Code 137)
kubectl set resources deploy/todoapi -n todo --limits=memory=16Mi
kubectl describe pod -n todo -l app=todoapi | grep -E "OOMKilled|Exit Code"
kubectl rollout undo deploy/todoapi -n todo
kubectl rollout history deploy/todoapi -n todo

# 7) Cleanup: delete the whole resource group (cluster + node resource group)
az group delete --name $RG --yes --no-wait
```

> **Alıştırma.** `selector.matchLabels: {app: todoapi}` iken pod şablonu `labels: {app: todo-api}` ise ne olur? Şablon doğru, **Service selector**'ı `app: todo-api` ise ne olur?
>
> <details><summary>Çözüm</summary>
>
> Birincisinde `kubectl apply` Deployment'ı **doğrulamada reddeder** (`selector does not match template labels`); hiçbir pod oluşmaz. İkincisinde Deployment ve pod'lar sağlıklı çalışır ama Service selector'ı hiçbir pod'la eşleşmez: `kubectl get endpoints todoapi` **boş** döner ve istemci bağlantıları zaman aşımına uğrar.
> </details>

---

### 4. Gerçek dünya senaryosu

Bir fintech, Todo/ödeme mikroservislerini AKS'te çalıştırıyor ve cuma akşamı yapılan dağıtımdan sonra alarm geliyor:

| Belirti | Teşhis | Kök neden | Çözüm |
|---|---|---|---|
| `kubectl get pods`: yeni pod'lar `ImagePullBackOff` | `describe` → `manifest ... not found` | CI yanlış tag'i manifest'e yazdı | `rollout undo`; pipeline'da digest kullan (→ C2.1) |
| Pod'lar `CrashLoopBackOff` | `logs --previous`: "Db connection string missing" | Yeni sürüm eklediği `Db__Password` secret'ta yok | Secret'ı ekle; eksik yapılandırmada hızlı fail; `rollout undo` |
| Pod'lar `OOMKilled`, 137 | `describe` → `Reason: OOMKilled` | Limit 256Mi, yeni önbellek ~400Mi | Ölçerek limit'i artır; önbellek boyutunu sınırla |
| Pod `Running` ama trafik gelmiyor | `describe` readiness 404; `get endpoints` boş | Probe `/health`, uygulama `/healthz` | Probe yolunu düzelt |
| Eski sürüm çalışmaya devam ediyor | `maxUnavailable: 0` + bozuk yeni pod | Rolling update eski pod'ları korudu | `rollout undo` ile temizle; kesinti oluşmamıştı |

Ek kararlar: yapılandırma ConfigMap, parola Key Vault + CSI driver; pod başına request/limit zorunlu; üretim image'ları digest ile; rollout `kubectl rollout status` ile pipeline'da doğrulanır, başarısızsa otomatik `rollout undo`.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Kısa bir manifest veya `kubectl` çıktısı verir; hatayı/kök nedeni/komutu seçtirir. Çoğu soru "ne yapılmalı?" yerine **"ilk önce hangi komut?"** veya **"hangi alan yanlış?"** biçimindedir.

| Senaryodaki ifade | Cevap |
|---|---|
| "Pod `ImagePullBackOff`; mesaj `manifest unknown`" | Yanlış tag/digest/repository |
| "Pod `ImagePullBackOff`; mesaj `unauthorized`" | Kubelet kimliğinde **`AcrPull`** yok → `--attach-acr` |
| "Pod sürekli yeniden başlıyor, `Exit Code 1`" | `CrashLoopBackOff` → `kubectl logs --previous` |
| "`Reason: OOMKilled`, Exit Code 137" | **Bellek limiti** aşıldı |
| "Pod Running ama READY 0/1" | **Readiness probe** (yol/port) |
| "Service'e istek zaman aşımı; endpoint yok" | **Selector–label** uyuşmazlığı veya hazır pod yok |
| "Dağıtım sonrası sorun, hemen önceki sürüme dön" | `kubectl rollout undo deployment/<ad>` |
| "Çöken container'ın önceki logu" | `kubectl logs <pod> --previous` |
| "Pod içinde komut çalıştır" | `kubectl exec -it <pod> -- sh` |
| "AKS'in ACR'dan image çekmesi" | `az aks update --attach-acr` (kubelet identity + `AcrPull`) |
| "Kubeconfig'e küme ekle" | `az aks get-credentials` |
| "Kubernetes API/CRD gerekmiyor, scale-to-zero" | **Container Apps** |

**Tuzaklar:**
- `Deployment.selector` ile `template.labels` eşleşmeli; `Service.selector` da pod label'larıyla eşleşmeli — ikisi farklı hatalardır (apply reddi vs endpoint'siz Service).
- `containerPort` **bilgilendiricidir**; trafiği yönlendiren `Service.targetPort` ve uygulamanın gerçek dinlediği porttur.
- **Liveness** ≠ **readiness**: biri yeniden başlatır, öteki trafikten çıkarır.
- Bellek limit aşımı öldürür (**OOMKilled**), CPU limit aşımı yalnızca yavaşlatır (**throttle**).
- `kubectl logs` çöken container için çoğu zaman yeni (boş) instance'ı gösterir; **`--previous`** gerekir.
- `Secret` şifreli değil **base64 kodlu**dur.
- `rollout undo` yalnızca **Deployment** revision geçmişi üzerinden çalışır (`revisionHistoryLimit` ile sınırlı).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| `:latest` tag'i ile dağıtıp `imagePullPolicy` yüzünden sürpriz yaşamak | Digest veya benzersiz tag; politika bilinçli seçilir |
| Request/limit vermemek | Zamanlama ve OOM/throttle öngörülebilir olsun; her container için ver |
| Probe yok / yanlış | Readiness yoksa hazır olmayan pod trafik alır; liveness çok agresifse sağlıklı pod öldürülür |
| `kubectl describe`'a bakmadan yeniden dağıtmak | Events çoğu zaman kök nedeni açıkça yazar |
| Secret'ı düz YAML olarak Git'e koymak | Key Vault + CSI driver / sealed secret / CI'dan enjeksiyon |
| Her şeyi `default` namespace'e koymak | Namespace + RBAC + ResourceQuota ile izole et |
| Sadece 1 replica ve `maxUnavailable` varsayılanı | Kesintisiz güncelleme için ≥2 replica; `maxUnavailable: 0`, `maxSurge: 1` |
| `--attach-acr`'yi unutmak | Kubelet kimliğine `AcrPull` olmadan image çekilemez |
| Basit bir API için AKS seçmek | Container Apps / App Service operasyon maliyetini düşürür |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| AKS vs Container Apps | Tam Kubernetes kontrolü, ekosistem | Node/upgrade/güvenlik operasyonu, boşta node maliyeti |
| Yüksek memory limit | OOM riski azalır | Node'da daha az pod; maliyet |
| Düşük CPU limit | Komşu pod'ları korur | Throttle → gecikme artışı |
| `maxUnavailable: 0, maxSurge: 1` | Kesintisiz güncelleme | Güncelleme sırasında geçici ek kaynak ve daha yavaş rollout |
| Agresif liveness probe | Takılan pod'u hızlı yeniden başlatır | Yavaş açılan uygulamada gereksiz restart döngüsü → **startup probe** kullan |
| `LoadBalancer` Service per app | Basitlik | Her biri ayrı public IP/maliyet; **Ingress/Gateway** ile birleştir |
| Pod başına küçük request | Yoğun paketleme, ucuz | Aşırı taahhüt (overcommit) ve gürültülü komşu riski |

---

### 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [AKS overview](https://learn.microsoft.com/en-us/azure/aks/what-is-aks) · [Deploy an AKS cluster (CLI)](https://learn.microsoft.com/en-us/azure/aks/learn/quick-kubernetes-deploy-cli)
- [Authenticate with ACR from AKS (`--attach-acr`)](https://learn.microsoft.com/en-us/azure/aks/cluster-container-registry-integration)
- [Kubernetes: Deployments](https://kubernetes.io/docs/concepts/workloads/controllers/deployment/) · [Services](https://kubernetes.io/docs/concepts/services-networking/service/) · [Probes](https://kubernetes.io/docs/tasks/configure-pod-container/configure-liveness-readiness-startup-probes/) · [Debug Pods](https://kubernetes.io/docs/tasks/debug/debug-application/debug-pods/)
- [Update on the AKS application routing add-on and Ingress-NGINX](https://blog.aks.azure.com/2025/11/13/ingress-nginx-update) · [Application routing](https://learn.microsoft.com/en-us/azure/aks/app-routing)
- [Choose an Azure container service](https://learn.microsoft.com/en-us/azure/architecture/guide/choose-azure-container-service)
- [Monitor Container Apps logs](https://learn.microsoft.com/en-us/azure/container-apps/log-monitoring)


### 📝 Bölüm 1.3 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Manifest okuma
Aşağıdaki Deployment uygulanıyor.

```yaml
apiVersion: apps/v1
kind: Deployment
metadata: { name: todoapi }
spec:
  replicas: 2
  selector:
    matchLabels:
      app: todoapi
  template:
    metadata:
      labels:
        app: todo-api
    spec:
      containers:
        - name: todoapi
          image: myacr.azurecr.io/todoapi:1.0.0
```

Ne olur?

- A) Pod'lar oluşur; Service'ler çalışmaz
- B) `kubectl apply` selector, şablon label'larıyla eşleşmediği için hata verir
- C) Pod'lar `CrashLoopBackOff` olur
- D) Pod'lar `ImagePullBackOff` olur

<details><summary>Cevap</summary>

**B.** `spec.selector.matchLabels` pod şablonunun label'larıyla eşleşmelidir; burada `todoapi` ≠ `todo-api`, API server Deployment'ı doğrulamada reddeder (`selector does not match template labels`). Hiç pod oluşmaz, dolayısıyla C ve D de mümkün değildir.
</details>

#### Soru 2: Tek seçim
AKS kümesindeki bir pod'un durumu `ImagePullBackOff`; `kubectl describe pod` olayları `Failed to pull image … 401 Unauthorized` diyor. Image adı ve tag doğru. En olası kök neden ve çözüm?

- A) Bellek limiti düşük; limit artırılmalı
- B) Kubelet kimliğinde ACR üzerinde `AcrPull` yok; `az aks update --attach-acr` ile bağlanmalı
- C) Readiness probe yanlış; port düzeltilmeli
- D) Service selector yanlış

<details><summary>Cevap</summary>

**B.** `401/unauthorized` kimlik/yetki sorunudur. AKS image'ı kubelet managed identity ile çeker; ona `AcrPull` verilmelidir (`--attach-acr`). "manifest unknown/not found" olsaydı kök neden yanlış tag/digest olurdu. A, C, D farklı belirtilerin nedenleridir.
</details>

#### Soru 3: Eşleştirme
Belirtileri en olası kök nedenlerle eşleştir.

| Belirti | Kök neden |
|---|---|
| 1) `OOMKilled`, Exit Code 137 | a) Readiness probe yanlış port |
| 2) Pod `Running`, `READY 0/1`, endpoint yok | b) Bellek limiti aşıldı |
| 3) `CrashLoopBackOff`, `logs --previous`'ta "missing configuration" | c) Eksik env var/secret nedeniyle uygulama başlarken çöküyor |
| 4) `ErrImagePull`, `manifest unknown` | d) Registry'de olmayan tag |

<details><summary>Cevap</summary>

**1-b · 2-a · 3-c · 4-d.** Bellek limit aşımı çekirdeğin container'ı SIGKILL (137) ile öldürmesidir; readiness başarısızlığı pod'u endpoint'lerden çıkarır ama yeniden başlatmaz; çökme döngüsünün nedeni uygulama logunda; `manifest unknown` etiketin registry'de bulunmadığını söyler.
</details>

#### Soru 4: Tek seçim
Bir container birkaç saniye çalışıp çöküyor ve pod `CrashLoopBackOff`. Çökmenin **nedenini** görmek için hangi komut en doğrudur?

- A) `kubectl logs <pod>` (her zaman yeterlidir)
- B) `kubectl logs <pod> --previous`
- C) `kubectl scale deploy/x --replicas=0`
- D) `kubectl rollout restart deploy/x`

<details><summary>Cevap</summary>

**B.** Çöken container yeniden başlatıldığı için `kubectl logs` çoğu zaman yeni (henüz log üretmemiş) örneği gösterir; önceki çalışmanın logu `--previous` ile okunur. C ve D teşhis değil, müdahaledir.
</details>

#### Soru 5: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Memory limit aşıldığında container OOMKilled olur. | ☐ | ☐ |
| b) CPU limit aşıldığında container öldürülür. | ☐ | ☐ |
| c) Readiness probe başarısız olursa container yeniden başlatılır. | ☐ | ☐ |
| d) Liveness probe sürekli başarısız olursa container yeniden başlatılır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Hayır · d) Evet.** Bellek sıkıştırılamaz kaynaktır (aşım = öldürülme); CPU limit aşımı yalnızca kısıtlamaya (throttle) yol açar. Readiness başarısızlığı pod'u Service endpoint'lerinden çıkarır, yeniden başlatmaz; liveness başarısızlığı container'ı yeniden başlatır.
</details>

#### Soru 6: kubectl seçimi
Yeni sürüm rollout'ı sonrası hata oranı arttı. **Önceki çalışan sürüme** hızla dönmek için hangi komut kullanılır?

- A) `kubectl delete deploy todoapi`
- B) `kubectl rollout undo deployment/todoapi`
- C) `kubectl scale deployment/todoapi --replicas=0`
- D) `kubectl rollout pause deployment/todoapi`

<details><summary>Cevap</summary>

**B.** `rollout undo` Deployment'ı bir önceki ReplicaSet revision'ına döndürür (`--to-revision=N` ile belirli bir sürüme de). Diğerleri geri alma yapmaz (`pause` rollout'u yalnızca durdurur).
</details>

#### Soru 7: Manifest tamamlama
Service, `app: todoapi` etiketli pod'ların container'ındaki **8080** portuna, dışarıya **80** portundan açılacak.

```yaml
apiVersion: v1
kind: Service
metadata: { name: todoapi }
spec:
  type: LoadBalancer
  selector:
    app: todoapi
  ports:
    - port: 80
      ____: 8080
```

- A) `containerPort`
- B) `targetPort`
- C) `nodePort`
- D) `hostPort`

<details><summary>Cevap</summary>

**B.** `targetPort` trafiğin pod'da gönderileceği porttur; `port` Service'in sunduğu porttur. `containerPort` Pod şablonunda, `nodePort` NodePort türünde node portu için kullanılır.
</details>

#### Soru 8: Tek seçim
Bir Service oluşturuldu, ancak istemciler zaman aşımı alıyor. `kubectl get pods` tüm pod'ları `Running 1/1` gösteriyor. `kubectl get endpoints todoapi` **`<none>`** dönüyor. En olası neden?

- A) Bellek limiti
- B) Service selector'ı pod label'larıyla eşleşmiyor
- C) ACR kimlik hatası
- D) Liveness probe

<details><summary>Cevap</summary>

**B.** Pod'lar sağlıklı ve hazır, ama Service hiçbirini seçmiyor: selector–label uyuşmazlığı. Hazır olmayan pod'lar (readiness) da endpoint'ten çıkardı, ancak burada hepsi 1/1 hazır. ACR ve bellek sorunları pod'u hiç `Running 1/1` bırakmazdı.
</details>

#### Soru 9: CLI tamamlama
Mevcut AKS kümesinin ACR'dan image çekebilmesi için:

```bash
az aks ____ --resource-group rg --name aks --attach-acr myacr
```

- A) `update`
- B) `scale`
- C) `get-credentials`
- D) `nodepool add`

<details><summary>Cevap</summary>

**A.** `az aks update --attach-acr` kubelet kimliğine registry'de `AcrPull` atar (yeni kümede `az aks create --attach-acr`). `get-credentials` kubeconfig yazar, `scale` node sayısını değiştirir.
</details>

#### Soru 10: Çoklu seçim (iki doğru cevap)
Bir pod `Pending` durumunda kalıyor. `describe` olayı: `FailedScheduling: 0/2 nodes are available: 2 Insufficient memory`. **Hangi iki** eylem sorunu giderir?

- A) Container'ın memory **request**'ini düşürmek
- B) Node pool'a node eklemek (veya daha büyük VM)
- C) Liveness probe'u kaldırmak
- D) Service türünü ClusterIP yapmak

<details><summary>Cevap</summary>

**A ve B.** Zamanlayıcı **request** değerlerine bakar; request node'larda boş bellekten büyükse pod yerleşemez. Ya request gerçek ihtiyaca göre düşürülür ya da kapasite artırılır. Probe ve Service türü zamanlamayı etkilemez.
</details>

#### Soru 11: Senaryo
Ekip, bir Todo API'yi Kubernetes API'sine, CRD'lere veya özel bir service mesh'e **ihtiyaç duymadan**, kuyruk mesajına göre sıfıra kadar ölçekleyerek çalıştırmak ve küme operasyonuyla uğraşmamak istiyor. En uygun seçenek hangisidir?

- A) AKS
- B) Azure Container Apps
- C) Azure Virtual Machines
- D) App Service (Windows)

<details><summary>Cevap</summary>

**B.** Container Apps, yerleşik KEDA ile scale-to-zero ve küme yönetimi gerektirmeyen serverless container sunar. AKS bu ihtiyaç için gereğinden fazla operasyon yüküdür; VM ve klasik App Service KEDA tabanlı sıfıra ölçeklemeyi sunmaz.
</details>

#### Soru 12: Manifest okuma (iki doğru cevap)
Aşağıdaki Pod bölümünü incele.

```yaml
containers:
  - name: api
    image: myacr.azurecr.io/api:2.0.0
    ports:
      - containerPort: 8080
    resources:
      limits: { memory: 32Mi }
    readinessProbe:
      httpGet: { path: /healthz, port: 80 }
```

Uygulama 8080 portunu dinliyor ve çalışırken ~120 MiB bellek kullanıyor. **Hangi iki sorun** vardır?

- A) Bellek limiti gerçek kullanımın altında (OOMKilled riski)
- B) Readiness probe yanlış porta bakıyor
- C) `containerPort` yazılması hatadır
- D) `image` alanı tag içeremez

<details><summary>Cevap</summary>

**A ve B.** 32 MiB limit, ~120 MiB kullanımla çatışır: container OOMKilled olur. Probe 80 portunu dener ama uygulama 8080'dedir → readiness başarısız, pod hiç hazır olmaz. `containerPort` geçerli ve bilgilendiricidir; image tag içerebilir.
</details>

#### Soru 13: Tek seçim
Bir ekip dağıtım sonrası pod'ları şöyle gözlemliyor: `STATUS Running`, `READY 0/1`, `RESTARTS 0`. `kubectl describe` çıktısında `Readiness probe failed: Get "http://10.0.0.5:8080/health": … 404`. Uygulamanın sağlık ucu `/healthz`. Çözüm?

- A) Memory limit artırmak
- B) Probe path'ini `/healthz` yapmak
- C) `imagePullPolicy: Always`
- D) `kubectl rollout undo`'yu her zaman kullanmak

<details><summary>Cevap</summary>

**B.** 404, probe'un yanlış yola gittiğini gösterir; pod çalışıyor ama hazır işaretlenmiyor (restart yok, çünkü liveness değil readiness başarısız). `rollout undo` geçici çözümdür, kök nedeni düzeltmez; A ve C alakasızdır.
</details>

#### Soru 14: Sıralama
Bir pod'un çalışmadığı bildirildi. Sistematik teşhis sırasını diz: (1) `kubectl logs <pod> --previous`, (2) `kubectl get pods -o wide`, (3) `kubectl describe pod <pod>`, (4) Kök nedeni gidermek için manifest/yapılandırmayı düzeltip `kubectl apply`.

<details><summary>Cevap</summary>

**2 → 3 → 1 → 4.** Önce durum (`STATUS`, `READY`, `RESTARTS`), sonra `describe` (Events, `Reason`, `Exit Code`), ardından log (çökme nedeni), son olarak düzeltme. Önce müdahale etmek yerine kanıt toplanır.
</details>

#### Soru 15: Vaka çalışması
**Northwind Bank** ödeme API'sini AKS'e dağıttı. Pipeline yeni sürümü `kubectl set image` ile güncelledi. Gözlemler:

- Yeni pod'lar: `ImagePullBackOff`; `describe` olayı: `manifest for myacr.azurecr.io/payments:1.4.1 not found`.
- Eski pod'lar hâlâ çalışıyor ve trafiği karşılıyor (`maxUnavailable: 0`).
- Bir sonraki denemede image doğru ama pod'lar `CrashLoopBackOff`; `kubectl logs --previous`: `Unhandled exception: ConnectionStrings:Db is missing`.
- Üçüncü denemede uygulama ayakta, ancak `READY 0/1` ve Service `endpoints` boş; `describe`: `Readiness probe failed … connection refused (port 8081)`; uygulama 8080'de dinliyor.

**a)** İlk sorunun kök nedeni ve düzeltmesi?
- A) Kubelet'te `AcrPull` yok; rol ver
- B) CI, registry'de bulunmayan bir tag yazdı; doğru tag/digest ile güncelle (veya `rollout undo`)

**b)** İkinci sorunun nedeni?
- A) Bellek limiti
- B) Ortam değişkeni/secret'ta `ConnectionStrings__Db` eksik

**c)** Üçüncü sorunun düzeltmesi?
- A) `readinessProbe.httpGet.port` değerini 8080 yapmak
- B) Replica sayısını artırmak

**d)** Müşteri etkisi neden yaşanmadı?
- A) `RollingUpdate` + `maxUnavailable: 0`: eski pod'lar yenileri hazır olana kadar kapatılmadı
- B) Service tipi LoadBalancer olduğu için

<details><summary>Cevap</summary>

**a) B · b) B · c) A · d) A.**
(a) "not found" tag/manifest'in bulunmadığını söyler; yetki sorunu olsaydı `unauthorized` görülürdü. (b) `logs --previous` doğrudan eksik yapılandırmayı gösteriyor. (c) Probe portu uygulamanın portuyla uyuşmalı; replica artırmak hazır olmayan pod'ları hazır yapmaz. (d) Rolling update, yeni pod'lar readiness'i geçmeden eski pod'ları kapatmaz.
</details>


---

# BÖLÜM 2: Develop AI solutions by using Azure data management services (%30-35)

---

## 2.1 Cosmos DB for NoSQL: SDK, Partitioning, Indexing, Consistency, RU

> **Sınav Alanı:** Develop AI solutions by using Azure data management services (%30-35) · **Konu Kodu:** C2.4

### 1. Ne?

**Azure Cosmos DB for NoSQL**, Microsoft'un tam yönetilen, çok bölgeli, JSON **document** veritabanıdır. Her işlemin bedeli **Request Unit (RU)** cinsinden ölçülür: CPU + bellek + IO'nun tek bir para biriminde soyutlanmış hâli. Bu konunun üç ana ekseni vardır:

| Eksen | Soru | Karar anı |
|---|---|---|
| **Partitioning** | Veri ve yük fiziksel olarak nasıl bölünür? | Container oluşturulurken — **sonradan değiştirilemez** |
| **Indexing** | Hangi yollar indekslenir? Yazma/okuma RU'su ne olur? | Her zaman ayarlanabilir (ama yeniden indeksleme maliyetlidir) |
| **Consistency** | Okuyucu ne kadar "taze" veri görür? | Hesap varsayılanı + istek bazında **gevşetme** |

**Kaynak modeli:** `account → database → container → item`. Container, ölçek ve throughput birimidir; item en çok **2 MB** olabilir (limitler değişebilir — güncel dokümana bak). Her item'ın `id` alanı vardır; **kimlik = (partition key değeri + id)** çiftidir.

```text
Cosmos account (endpoint, consistency default, regions)
 └─ Database
     └─ Container  (partition key path, indexing policy, throughput, TTL)
         ├─ Logical partition  "customer-42"   (aynı partition key değerine sahip tüm item'lar, <= 20 GB)
         │    ├─ item { id: "o-1", customerId: "customer-42", ... }
         │    └─ item { id: "o-2", customerId: "customer-42", ... }
         └─ Logical partition  "customer-77" ...
              (logical partition'lar sistemce physical partition'lara yerleştirilir)
```

Vector arama ve change feed ayrı konudur (→ [C2.5](#22-cosmos-db-ile-vector-search-ve-change-feed)); bu konu temel veri düzlemini ve RU ekonomisini kapsar.

---

### 2. Neden?

- **Sınav:** Bu alan toplam sınavın **%30-35**'idir ve Cosmos DB bunun en büyük parçasıdır. Sorular genelde "şu senaryoda hangi partition key?", "RU neden arttı?", "hangi consistency?", "boşluğu doldur" biçimindedir.
- **Gerçek işte:** Yanlış partition key, **hot partition** (tek physical partition'ın 10.000 RU/s sınırında 429 almaya başlaması) ve cross-partition sorgu faturası demektir. Cosmos'ta *performans sorunu çoğu zaman bir RU sorunudur*, ve RU'yu ölçmeyi bilmeyen ekip tahminle sizing yapar.
- **AI iş yükü:** RAG/ajan uygulamalarında Cosmos; sohbet geçmişi, ajan durumu, doküman metadata'sı ve embedding'ler için kalıcı depodur (→ C2.5, M15.4).

---

### 3. Nasıl?

#### 3.1 Partition key ⭐

İyi partition key üç şeyi sağlar: **yüksek cardinality** (çok sayıda farklı değer), **eşit dağılım** (hem depolama hem istek), **sorgu desenine uyum** (en sık sorgular tek partition'a gitsin).

| Aday (sipariş verisi) | Cardinality | Dağılım | Sorgu uyumu | Karar |
|---|---|---|---|---|
| `/status` (`new/paid/shipped`) | 3 değer | Çok çarpık | "Müşterinin siparişleri" cross-partition | **Kötü** — hot partition |
| `/orderDate` (gün) | Orta | Bugüne yazma yığılır (hot write) | Zaman aralığı sorguları cross-partition | Zayıf |
| `/customerId` | Yüksek | İyi (büyük müşteri hariç) | "Müşterinin siparişleri" = tek partition | **İyi** |
| `/id` | Çok yüksek | Mükemmel | Her sorgu cross-partition (yalnızca point read ucuz) | Yalnızca point-read ağırlıklı iş yükü |
| **Hierarchical:** `/tenantId` → `/userId` → `/sessionId` | Çok yüksek | İyi | Önek (tenant) ile hedefli sorgu | Çok kiracılı büyük tenant'lar için |
| **Synthetic:** `tenantId + "-" + month` | Yüksek | İyi | Hesaplanmış alan | Tek alan yetmiyorsa |

Kurallar (sınavda çıkar):
- Partition key **path'i container oluşturulduktan sonra değiştirilemez**; değiştirmek için yeni container + veri taşıma (ör. change feed ile kopyalama) gerekir. İtem'daki *değer* ise (kural olarak) taşınamaz: item silinip yeniden yazılır.
- **Logical partition** başına depolama üst sınırı vardır (**20 GB**); aşan tenant için hierarchical partition key veya synthetic key.
- **Hierarchical partition key** en çok 3 seviye; sorguda ilk seviye(ler) verilirse sorgu yalnızca ilgili partition'lara gider.
- Partition key değeri **sorguda filtre olarak verilirse** sorgu tek partition'a gider; verilmezse **cross-partition (fan-out)** olur ve RU tüm partition'lara yayılır.

#### 3.2 SDK: `CosmosClient` ve temel işlemler

```csharp
// dotnet add package Microsoft.Azure.Cosmos ; dotnet add package Azure.Identity
using Azure.Identity;
using Microsoft.Azure.Cosmos;

// 1) ONE CosmosClient per application (singleton). It owns connection pools,
//    the partition-key-range cache and the retry policy. Never create it per request.
builder.Services.AddSingleton(_ => new CosmosClient(
    accountEndpoint: builder.Configuration["Cosmos:Endpoint"],
    tokenCredential: new DefaultAzureCredential(),            // Entra ID instead of account keys
    clientOptions: new CosmosClientOptions
    {
        ConnectionMode = ConnectionMode.Direct,                // default in v3; lowest latency
        ApplicationRegion = Regions.WestEurope,
        SerializerOptions = new() { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase },
        // 429 handling: the SDK already retries (default 9 attempts / 30 s total wait).
        MaxRetryAttemptsOnRateLimitedRequests = 9,
        MaxRetryWaitTimeOnRateLimitedRequests = TimeSpan.FromSeconds(30),
    }));

public sealed record Order(string Id, string CustomerId, string Status, DateTime OrderDate, decimal Total);

Container container = client.GetContainer("shop", "orders");           // no network call yet

// 2) Create / upsert
ItemResponse<Order> created = await container.CreateItemAsync(
    new Order("o-1", "customer-42", "paid", DateTime.UtcNow, 99.95m),
    new PartitionKey("customer-42"));
Console.WriteLine($"write RU: {created.RequestCharge}");

// 3) POINT READ: id + partition key. The cheapest operation (1 KB item ~ 1 RU).
ItemResponse<Order> read = await container.ReadItemAsync<Order>("o-1", new PartitionKey("customer-42"));
Console.WriteLine($"point read RU: {read.RequestCharge}");

// 4) Query (parameterized, scoped to ONE partition via PartitionKey in QueryRequestOptions)
var query = new QueryDefinition("SELECT * FROM c WHERE c.status = @s AND c.total > @t")
    .WithParameter("@s", "paid").WithParameter("@t", 50m);

double totalRu = 0;
using FeedIterator<Order> it = container.GetItemQueryIterator<Order>(query,
    requestOptions: new QueryRequestOptions { PartitionKey = new PartitionKey("customer-42"), MaxItemCount = 100 });
while (it.HasMoreResults)
{
    FeedResponse<Order> page = await it.ReadNextAsync();
    totalRu += page.RequestCharge;                              // sum RU over all pages
    foreach (Order o in page) Console.WriteLine(o.Id);
}
Console.WriteLine($"query RU: {totalRu}");

// 5) Transactional batch: atomic, ALL items must share the same partition key (max 100 operations)
TransactionalBatchResponse batch = await container.CreateTransactionalBatch(new PartitionKey("customer-42"))
    .CreateItem(new Order("o-2", "customer-42", "new", DateTime.UtcNow, 10m))
    .ReplaceItem("o-1", new Order("o-1", "customer-42", "shipped", DateTime.UtcNow, 99.95m))
    .ExecuteAsync();
Console.WriteLine($"batch ok={batch.IsSuccessStatusCode} RU={batch.RequestCharge}");

// 6) Bulk: for many independent writes (data loading); the client batches them per partition internally
//    new CosmosClient(..., new CosmosClientOptions { AllowBulkExecution = true });
//    await Task.WhenAll(orders.Select(o => container.UpsertItemAsync(o, new PartitionKey(o.CustomerId))));
```

**Python eşdeğeri** (`pip install azure-cosmos azure-identity`):

```python
from azure.cosmos import CosmosClient, PartitionKey
from azure.identity import DefaultAzureCredential

client = CosmosClient(url=ENDPOINT, credential=DefaultAzureCredential())  # create once, reuse
db = client.create_database_if_not_exists("shop")
container = db.create_container_if_not_exists(id="orders", partition_key=PartitionKey(path="/customerId"))

container.upsert_item({"id": "o-1", "customerId": "customer-42", "status": "paid", "total": 99.95})

item = container.read_item(item="o-1", partition_key="customer-42")          # point read
print("point read RU:", container.client_connection.last_response_headers["x-ms-request-charge"])

rows = container.query_items(
    query="SELECT * FROM c WHERE c.status = @s AND c.total > @t",
    parameters=[{"name": "@s", "value": "paid"}, {"name": "@t", "value": 50}],
    partition_key="customer-42",              # single-partition query; otherwise enable_cross_partition_query=True
)
print([r["id"] for r in rows])
print("query RU:", container.client_connection.last_response_headers["x-ms-request-charge"])
```

**Kimlik doğrulama:** Hesap anahtarı yerine **Entra ID + RBAC** (data-plane rolleri): `Cosmos DB Built-in Data Reader` / `Data Contributor`. Bu roller *Azure RBAC* değil Cosmos'un kendi SQL role assignment'ıdır (→ C2.11):

```bash
az cosmosdb sql role assignment create --account-name $ACC --resource-group $RG \
  --role-definition-name "Cosmos DB Built-in Data Contributor" \
  --principal-id $PRINCIPAL_ID --scope "/"
```

**Mod:** **Direct** (TCP, en düşük gecikme, varsayılan) vs **Gateway** (HTTPS, tek endpoint; güvenlik duvarı/emülatör/proxy kısıtlarında). Bağlantı kısıtlı ortamlarda Gateway'e düşülür.

#### 3.3 Throughput modelleri

| Model | Nasıl çalışır | Ne zaman | Dikkat |
|---|---|---|---|
| **Provisioned — manual** | Sabit RU/s (saatlik faturalanır) | Öngörülebilir, sürekli yük | Fazla ayırırsan boşa ödersin, azsa 429 |
| **Provisioned — autoscale** | Tmax belirlenir; sistem Tmax'ın %10'u ile %100'ü arasında ölçekler | Değişken/zirveli yük | Birim fiyat manual'dan yüksektir; faturalama saatlik kullanılan **en yüksek** RU/s'ye göre |
| **Serverless** | Rezerve throughput yok, **tüketilen RU** kadar öde | Dev/test, seyrek/küçük yük | Tek bölge, container başına throughput/depolama üst sınırları var; sürekli yüksek yük için pahalı |
| **Free tier** | Hesap başına (abonelikte tek) ilk **1000 RU/s + 25 GB** ücretsiz | Öğrenme/laboratuvar | Aboneliğte yalnızca bir hesapta |

- **Database düzeyi (shared) vs container düzeyi (dedicated)** throughput: paylaşılan RU havuzu birçok küçük container için ucuzdur ama bir container havuzu tüketip diğerlerini aç bırakabilir.
- Bir physical partition en çok yaklaşık **10.000 RU/s** ve **50 GB** taşır; toplam RU/s partition sayısına bölündüğü için **hot partition'da container'ın RU/s'si ne olursa olsun 429** alırsın.
- **429 (Request rate too large):** SDK `x-ms-retry-after-ms` kadar bekleyip otomatik yeniden dener; sürekli 429 = ya throughput yetersiz ya hot partition.

#### 3.4 Indexing policy ⭐

Varsayılan: **tüm yolları indeksle** (`/*`), `_etag` hariç; mod `consistent` (yazma ile senkron). Bu "sorgu dostu"dur ama **yazma RU'sunu** artırır (her yol için indeks girişi).

```json
{
  "indexingMode": "consistent",
  "automatic": true,
  "includedPaths": [
    { "path": "/customerId/?" },
    { "path": "/status/?" },
    { "path": "/orderDate/?" }
  ],
  "excludedPaths": [
    { "path": "/*" }
  ],
  "compositeIndexes": [
    [
      { "path": "/customerId", "order": "ascending" },
      { "path": "/orderDate",  "order": "descending" }
    ]
  ]
}
```

- **Dar indeks (allow-list):** `excludedPaths: [/*]` + yalnızca sorguda kullanılan yolları `includedPaths`'e al → yazma RU'su ve depolama düşer. Büyük/okunmayan alanlar (açıklama metni, blob benzeri, **vector alanı**) indekslenmez (→ C2.5).
- **Range index** (varsayılan) eşitlik, aralık, `ORDER BY` (tek alan), `JOIN` için; **spatial** coğrafi sorgular için; **composite** aşağıdaki durumlarda gerekir:
  - **Birden çok alanla `ORDER BY`** (`ORDER BY c.customerId ASC, c.orderDate DESC`) — composite index **yoksa sorgu hata verir**.
  - Birden çok eşitlik + bir aralık filtresinde RU'yu düşürmek için.
- Composite'te **sıra ve yön** önemlidir: sorgudaki `ORDER BY` yönleri indeksle aynı ya da tam ters olmalı.
- `indexingMode: none` yalnızca point read/silme yapılan (sorgusuz) container'larda; `lazy` kullanımdan kalkmıştır — sınavda `consistent` doğru cevaptır.
- Politika değişince arka planda **yeniden indeksleme** yapılır (RU tüketir); sorgu sonuçları bu sırada eksik olabilir.

#### 3.5 Consistency level'lar ⭐

| Seviye | Garanti | Okuma RU'su | Gecikme/kullanılabilirlik | Tipik kullanım |
|---|---|---|---|---|
| **Strong** | Linearizable: her zaman en son commit edilmiş yazma | **~2×** | En yüksek yazma gecikmesi; bölgeler arası mesafe sınırı; çok bölgeli yazma ile birlikte kullanılamaz | Finansal bakiye |
| **Bounded Staleness** | Okuma en fazla *K versiyon* veya *T süre* geride | **~2×** | Strong'a yakın, "sınırlı" gecikme | Skor tabloları, bölgeler arası sıralı veri |
| **Session** ⭐ (**varsayılan**) | Aynı **session** içinde *read-your-writes*, monotonic reads/writes | 1× | Düşük | Kullanıcı bağlamlı uygulamalar — çoğu uygulama |
| **Consistent Prefix** | Yazmalar sırayla görülür, boşluk yok (ama geride olabilir) | 1× | Düşük | Sıralı akış, feed |
| **Eventual** | Sıra garantisi yok, eninde sonunda tutarlı | 1× (en ucuz/en hızlı) | En düşük | Beğeni sayısı, telemetri |

- **Hesap düzeyinde varsayılan** belirlenir; **istek veya client bazında yalnızca gevşetilebilir** (ör. hesap Session iken bir sorgu Eventual; ancak daha güçlüsüne çıkılamaz):

```csharp
var opts = new ItemRequestOptions { ConsistencyLevel = ConsistencyLevel.Eventual };  // can only weaken
var r = await container.ReadItemAsync<Order>("o-1", new PartitionKey("customer-42"), opts);
```

- **Session token tuzağı:** *Read-your-writes* yalnızca **aynı `CosmosClient`/session token** ile geçerlidir. İki farklı sunucu/işlem (ör. API yazar, worker okur) aynı garantiyi isterse session token'ı taşımalıdır (`ItemRequestOptions.SessionToken`), aksi hâlde Session ≈ Eventual gibi davranır.
- CAP/PACELC: seviye yükseldikçe **gecikme ve RU** artar, **kullanılabilirlik** (bölge kaybında) düşebilir.

#### 3.6 RU optimizasyon reçetesi

| Teknik | Etki |
|---|---|
| **Point read** (`ReadItemAsync`) kullan; "id'ye göre sorgu" yazma | ~1 RU vs birkaç RU |
| Sorguya **partition key** ver; cross-partition fan-out'tan kaçın | RU partition sayısı kadar çarpılır |
| **Projection** (`SELECT c.id, c.status`) ve `TOP`/sayfa boyutu | Daha az veri = daha az RU/ağ |
| Indexing policy'yi **daralt**; composite ekle | Yazma RU'su ↓; ORDER BY/çoklu filtre RU'su ↓ |
| Item'ı küçük tut (büyük metni ayrı sakla), gereksiz alan yazma | Okuma+yazma RU'su item boyutuyla orantılı |
| Gevşek consistency (Session/Eventual) | Okuma RU'su ~yarıya iner (Strong/Bounded'a göre) |
| `PartitionKey` ile **bulk** yükle; client'ı **singleton** yap | Bağlantı/DNS/metadata maliyeti tekrarlanmaz |
| TTL (`ttl`) ile eski veriyi otomatik sil | Depolama ve indeks küçülür |

> **RU'yu ölç:** yanıttaki `RequestCharge` / `x-ms-request-charge` başlığı. Sorguda ek olarak `PopulateIndexMetrics = true` ile hangi indeksin kullanıldığı/önerildiği görülür.

#### 3.7 Lab — iki partition key ile RU ölçümü

Bütçe: **free tier** (hesap başına 1000 RU/s + 25 GB ücretsiz; aboneliğin yalnızca bir hesabı) veya **serverless**; yine de bütçe alarmı kur (→ C1.11). Alternatif: **Cosmos DB emulator** (ör. Docker `mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator`; imaj/etiket ve desteklenen özellikler — özellikle vector/change feed — sürüme göre değişir, güncel dokümanı doğrula; emülatör için `ConnectionMode.Gateway` ve emülatörün well-known anahtarı gerekir).

```bash
RG=rg-ai200-c24
ACC=cosmos-c24-$RANDOM            # globally unique
az group create --name $RG --location westeurope

# Account: free tier (or use --capabilities EnableServerless instead of the free-tier flag)
az cosmosdb create --name $ACC --resource-group $RG --kind GlobalDocumentDB \
  --default-consistency-level Session --enable-free-tier true \
  --locations regionName=westeurope failoverPriority=0 isZoneRedundant=False

az cosmosdb sql database create --account-name $ACC --resource-group $RG --name shop

# Same data, two partition keys; share the free 1000 RU/s (400 each)
az cosmosdb sql container create --account-name $ACC --resource-group $RG --database-name shop \
  --name orders_by_customer --partition-key-path "/customerId" --throughput 400
az cosmosdb sql container create --account-name $ACC --resource-group $RG --database-name shop \
  --name orders_by_status   --partition-key-path "/status"     --throughput 400

az cosmosdb show --name $ACC --resource-group $RG --query documentEndpoint -o tsv   # endpoint for the app
```

Ardından C# konsol uygulamasında (3.2'deki `CosmosClient` ile) 2000 sipariş yükleyip her iki container'da şu ölçümleri yap ve tabloya yaz:

```csharp
async Task<double> MeasureAsync(Container c, string label, QueryDefinition q, QueryRequestOptions? o = null)
{
    double ru = 0; int n = 0;
    using var it = c.GetItemQueryIterator<Order>(q, requestOptions: o);
    while (it.HasMoreResults) { var p = await it.ReadNextAsync(); ru += p.RequestCharge; n += p.Count; }
    Console.WriteLine($"{label,-45} items={n,5} RU={ru,8:F2}");
    return ru;
}

// Q1: point read                      -> orders_by_customer
// Q2: customer's orders (single partition)   -> orders_by_customer (PartitionKey set) vs orders_by_status (cross-partition)
// Q3: SELECT * vs SELECT c.id, c.total (projection)
// Q4: ORDER BY customerId, orderDate  -> fails without composite index; add the composite index, re-run
// Q5: narrow indexing policy (3.4) -> compare write RU of 2000 upserts before/after
```

Beklenen sıra (sayılar veri setine göre değişir — önemli olan **oran**): point read ≈ 1 RU ≪ tek partition sorgu < projection'sız cross-partition sorgu. `/status` container'ında üç partition'ın birine yığılan veride 429'lar görülür.

```bash
# Cleanup (also stops any RU charges)
az group delete --name $RG --yes --no-wait
```

---

### 4. Gerçek dünya senaryosu

Bir e-ticaret ekibi sipariş API'sini Cosmos'a taşıyor:

| Gereksinim | Karar | Gerekçe |
|---|---|---|
| Sorguların %90'ı "müşterinin siparişleri" | `/customerId` partition key | Tek partition sorgusu, ucuz |
| Sipariş detay sayfası | `ReadItemAsync(id, customerId)` | Sorgu yerine point read: ~1 RU |
| Kampanya günü 20× trafik | Autoscale (Tmax) | Zirveye göre sabit RU ödememek |
| Müşterinin "son 20 siparişi" | Composite index `(customerId ASC, orderDate DESC)` | Sıralama RU'su düşer |
| Sipariş + stok düşümü aynı anda atomik | Transactional batch (aynı `customerId` altında) | Partition sınırı içinde ACID |
| "Siparişimi verdim, listede görünmüyor" şikayeti | Session consistency + istemci başına tek `CosmosClient` | Read-your-writes, ek RU yok |
| Anahtar sızıntı riski | Entra ID + data-plane RBAC | Hesap anahtarı kodda yok |
| Büyük kurumsal müşteri 20 GB'ı aşıyor | Hierarchical key `/tenantId` → `/orderMonth` | Logical partition sınırı |

---

### 5. Sınav perspektifi

| Senaryodaki ifade | Cevap |
|---|---|
| "Düşük cardinality'li alan (status, country) partition key olmuş, 429'lar" | Hot partition → yüksek cardinality alan (`customerId`, `/id`) veya synthetic/hierarchical key |
| "Partition key'i değiştirmek istiyoruz" | Yeni container + veri kopyala; yerinde değiştirilemez |
| "Kullanıcı kendi yazdığını hemen okuyabilsin, en düşük maliyet" | **Session** |
| "Her zaman en güncel, bölgeler arası" | **Strong** (okuma ~2× RU; multi-region write ile olmaz) |
| "En ucuz/en hızlı okuma, sıra önemli değil" | **Eventual** |
| "Item'ı id'si ve partition key'i ile oku" | `ReadItemAsync` (point read) — sorgu değil |
| "ORDER BY iki alan hata veriyor" | **Composite index** ekle |
| "Yazma RU'su çok yüksek, sorgular yalnızca 3 alanda" | Indexing policy'yi daralt (allow-list) |
| "Gün ortasında yük 10× değişiyor" | **Autoscale**; seyrek/küçük/dev → **serverless** |
| "Birden çok item'ı atomik yaz" | Transactional batch — **aynı partition key** şart |
| "Bağlantı/gecikme sorunları, her istekte `new CosmosClient`" | **Singleton** client |
| "Anahtar yerine kimlik" | `DefaultAzureCredential` + Cosmos data-plane RBAC rolü |

**Tuzaklar:**
- **Strong ≠ Session:** "Kendi yazdığını oku" için Strong gerekmez; Session yeter ve daha ucuzdur.
- Consistency **istekte yalnızca gevşetilir**, güçlendirilemez.
- Transactional batch **cross-partition olamaz**; stored procedure/trigger de tek partition kapsamındadır.
- `SELECT *` + cross-partition + indeksi daraltılmış alan = pahalı tam tarama.
- Partition key ve `id` birlikte benzersizdir: farklı partition'larda aynı `id` olabilir.
- Vector ve change feed soruları ayrı konudur (→ C2.5); burada RU/partition/index/consistency vardır.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| `using var client = new CosmosClient(...)` her istekte | Singleton; her istekte yaratmak socket/bağlantı tükenmesi ve yüksek gecikme |
| Partition key olarak `status`, `country`, `type` | Yüksek cardinality + eşit dağılım + sorgu uyumu |
| Her okumayı `SELECT * FROM c WHERE c.id = ...` ile yapmak | `ReadItemAsync` point read; sorgu 3-10× pahalı olabilir |
| `RequestCharge`'ı yalnızca ilk sayfadan almak | Tüm sayfaların toplamı |
| Varsayılan "hepsini indeksle"yi hiç gözden geçirmemek | Yazma ağırlıklı iş yüklerinde indeksi daralt |
| Account key'i kaynak kodda/`appsettings`'te | Entra ID + RBAC; key kapatılabilir (`disableLocalAuth`) |
| 429'u "bug" saymak | Normal geri basınç; SDK yeniden dener — kök neden RU/hot partition |
| Strong'u "garanti olsun" diye varsayılan yapmak | 2× okuma RU'su + gecikme; çoğu iş yükü Session ile doğrudur |
| Büyük binary/uzun metni item içinde tutmak | 2 MB sınırı ve RU; Blob'da sakla, item'da referans |
| Bölgeler arası yazmada conflict çözümünü düşünmemek | Multi-region write'ta conflict resolution policy (LWW varsayılan) |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Geniş partition key (`/id`) | Mükemmel dağılım, ucuz point read | Her sorgu cross-partition |
| Dar partition key (`/customerId`) | Ucuz tek partition sorgular, transactional batch | Çok büyük tenant → 20 GB / hot partition |
| Manual → Autoscale | Zirveye otomatik uyum, 429 azalır | Birim fiyat yüksek; sürekli düz yükte pahalı |
| Provisioned → Serverless | Boşta maliyet yok | Sürekli yüksekte pahalı; sınırlı ölçek/tek bölge |
| Dar indeks | Yazma RU ↓, depolama ↓ | Yeni sorgu deseni → yeniden indeksleme/pahalı tarama |
| Composite index | ORDER BY / çoklu filtre RU ↓ | Yazma RU ↑, depolama ↑ |
| Strong / Bounded | Taze okuma | ~2× okuma RU'su, yüksek gecikme |
| Session / Eventual | Ucuz, hızlı | Eventual'da sıra/tazelik garantisi yok |
| Direct mod | Düşük gecikme | Geniş port aralığı/ağ gereksinimi; kısıtlı ağda Gateway |

---

### 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Azure Cosmos DB for NoSQL documentation](https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/)
- [Partitioning and horizontal scaling](https://learn.microsoft.com/en-us/azure/cosmos-db/partitioning-overview) · [Hierarchical partition keys](https://learn.microsoft.com/en-us/azure/cosmos-db/hierarchical-partition-keys)
- [Request Units](https://learn.microsoft.com/en-us/azure/cosmos-db/request-units) · [Optimize request cost](https://learn.microsoft.com/en-us/azure/cosmos-db/optimize-cost-reads-writes)
- [Indexing policies](https://learn.microsoft.com/en-us/azure/cosmos-db/index-policy) · [Consistency levels](https://learn.microsoft.com/en-us/azure/cosmos-db/consistency-levels)
- [.NET SDK best practices](https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/best-practice-dotnet) · [Python SDK (`azure-cosmos`)](https://learn.microsoft.com/en-us/python/api/overview/azure/cosmos-readme)
- [Data-plane RBAC](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-connect-role-based-access-control) · [Cosmos DB emulator](https://learn.microsoft.com/en-us/azure/cosmos-db/emulator)
- Sonraki konu: [C2.5 Vector Search ve Change Feed](#22-cosmos-db-ile-vector-search-ve-change-feed)


### 📝 Bölüm 2.1 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir IoT çözümünde cihaz telemetrisi Cosmos DB container'ına yazılıyor. 50.000 cihaz var, sorguların çoğu "belirli bir cihazın son 1 saatlik verisi". Hangi partition key **en uygundur**?

- A) `/deviceType` (5 farklı değer)
- B) `/deviceId`
- C) `/timestamp`
- D) `/region` (3 farklı değer)

<details><summary>Cevap</summary>

**B.** `/deviceId` yüksek cardinality'e sahiptir, yük cihazlara eşit dağılır ve en sık sorgu tek partition'a gider. A ve D düşük cardinality (hot partition); C ise tüm yeni yazmaları "bugünün" partition'ına yığar ve cihaz sorgularını cross-partition yapar.
</details>

#### Soru 2: Tek seçim
Bir container 8 ay önce `/status` partition key'i ile oluşturuldu ve hot partition nedeniyle 429 alıyor. Ekip partition key'i `/customerId` yapmak istiyor. Doğru yaklaşım hangisidir?

- A) `az cosmosdb sql container update --partition-key-path /customerId`
- B) Yeni container'ı `/customerId` ile oluşturup veriyi kopyalamak
- C) Container'ın indexing policy'sini değiştirmek
- D) Autoscale'i açmak, partition key otomatik değişir

<details><summary>Cevap</summary>

**B.** Partition key path'i container oluşturulduktan sonra değiştirilemez; yeni container + veri taşıma (ör. change feed ile kopyalama) gerekir. Autoscale hot partition'ı çözmez; indexing policy dağılımı etkilemez.
</details>

#### Soru 3: Tek seçim
Bir API, sipariş detay sayfası için sipariş `id`'sini ve `customerId` (partition key) değerini zaten biliyor. Hangi çağrı **en düşük RU** maliyetlidir?

- A) `SELECT * FROM c WHERE c.id = @id` sorgusu, `GetItemQueryIterator` ile
- B) `ReadItemAsync<Order>(id, new PartitionKey(customerId))`
- C) `SELECT VALUE COUNT(1) FROM c WHERE c.id = @id`
- D) Cross-partition sorgu + `TOP 1`

<details><summary>Cevap</summary>

**B.** Point read, id + partition key ile doğrudan adreslenir; 1 KB'lık item için yaklaşık 1 RU ile en ucuz okumadır. Sorgular indeks aramasını ve sorgu motorunu devreye sokar; cross-partition olanlar daha da pahalıdır.
</details>

#### Soru 4: Kod tamamlama (C#)
Aşağıdaki kodda tek bir partition içinde, atomik olarak iki işlem yapılmak isteniyor. Boşluğu doldurun.

```csharp
TransactionalBatchResponse r = await container
    .__________(new PartitionKey("customer-42"))
    .CreateItem(order2)
    .ReplaceItem(order1.Id, order1)
    .ExecuteAsync();
```

- A) `CreateBulkOperation`
- B) `CreateTransactionalBatch`
- C) `GetItemQueryIterator`
- D) `CreateTransaction`

<details><summary>Cevap</summary>

**B.** `container.CreateTransactionalBatch(PartitionKey)` — batch'teki tüm işlemler aynı partition key değerini paylaşmalı ve atomik uygulanır (en çok 100 işlem). Diğer adlar SDK'da bu amaçla yoktur; bulk için `AllowBulkExecution` + paralel görevler kullanılır.
</details>

#### Soru 5: Evet / Hayır
Cosmos DB SDK kullanımı hakkında:

| İfade | Evet | Hayır |
|---|---|---|
| a) `CosmosClient` uygulama yaşam süresi boyunca tek örnek (singleton) olarak kullanılmalıdır. | ☐ | ☐ |
| b) Transactional batch, farklı partition key değerlerine sahip item'ları tek atomik işlemde yazabilir. | ☐ | ☐ |
| c) SDK, 429 yanıtlarını varsayılan olarak belirli sayıda otomatik yeniden dener. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Client bağlantı havuzunu, metadata önbelleğini ve retry politikasını taşır; her istekte yaratmak zararlıdır. Batch yalnızca tek logical partition içinde atomiktir. SDK 429'da `retry-after` kadar bekleyip yeniden dener (varsayılan 9 deneme / 30 sn toplam bekleme).
</details>

#### Soru 6: Tek seçim
Bir sosyal uygulamada kullanıcı bir gönderi yazdıktan hemen sonra kendi profil sayfasını yeniler ve gönderiyi **mutlaka** görmelidir. Okuma RU maliyeti mümkün olduğunca düşük olmalı. Hangi consistency level seçilmelidir?

- A) Strong
- B) Bounded Staleness
- C) Session
- D) Eventual

<details><summary>Cevap</summary>

**C.** Session, aynı oturumda read-your-writes garantisi verir ve okuma RU'su Strong/Bounded'ın yarısıdır. Strong gereksiz pahalıdır; Eventual kendi yazdığını görmeyi garanti etmez.
</details>

#### Soru 7: Çoklu seçim
Hangi ifadeler **Strong** consistency için doğrudur? (İki seçenek)

- A) Okuma işlemleri Session'a göre yaklaşık 2× RU tüketir.
- B) En son commit edilmiş yazmayı garanti eder (linearizability).
- C) İstek bazında Strong'a *yükseltilebilir* (hesap Eventual iken).
- D) Varsayılan consistency level'dır.

<details><summary>Cevap</summary>

**A ve B.** C yanlış: istek bazında consistency yalnızca gevşetilebilir, hesap varsayılanından güçlüsüne çıkılamaz. D yanlış: varsayılan Session'dır.
</details>

#### Soru 8: Sıralama
Aşağıdaki consistency level'ları **en güçlü garantiden en zayıfa** sıralayın: Eventual · Session · Strong · Consistent Prefix · Bounded Staleness

<details><summary>Cevap</summary>

**Strong → Bounded Staleness → Session → Consistent Prefix → Eventual.** Güç azaldıkça gecikme ve (Strong/Bounded için) okuma RU'su düşer.
</details>

#### Soru 9: Senaryo
Bir sorgu şu ifadeyi içeriyor ve `The order by query does not have a corresponding composite index` benzeri hata veriyor:

```sql
SELECT c.id, c.total FROM c ORDER BY c.customerId ASC, c.orderDate DESC
```

Ne yapılmalıdır?

- A) Container throughput'unu artırmak
- B) `(customerId ASC, orderDate DESC)` için composite index eklemek
- C) Consistency'yi Eventual yapmak
- D) `indexingMode`'u `none` yapmak

<details><summary>Cevap</summary>

**B.** Birden çok alanla `ORDER BY` composite index gerektirir; sıra ve yönler sorgudakiyle aynı (veya tam ters) olmalıdır. Throughput/consistency bu hatayı çözmez; `none` hiçbir sorguyu çalıştırmaz.
</details>

#### Soru 10: Kod tamamlama (indexing policy)
Ekip, yazma RU'sunu düşürmek için yalnızca `customerId`, `status` ve `orderDate` alanlarını indekslemek istiyor. Boşluğu doldurun:

```json
{
  "indexingMode": "consistent",
  "includedPaths": [
    { "path": "/customerId/?" }, { "path": "/status/?" }, { "path": "/orderDate/?" }
  ],
  "excludedPaths": [ { "path": "__________" } ]
}
```

- A) `/_etag/?`
- B) `/*`
- C) `/customerId/*`
- D) `/?`

<details><summary>Cevap</summary>

**B.** `"/*"` hariç tutulur ve yalnızca `includedPaths`'teki yollar indekslenir (allow-list). Böylece diğer alanlar için indeks girişi yazılmaz ve yazma RU'su düşer.
</details>

#### Soru 11: Çoklu seçim
Bir container'ın **yazma RU maliyetini** düşürmek için hangi iki eylem uygundur?

- A) Indexing policy'yi yalnızca sorgulanan yollarla sınırlamak
- B) Gereksiz büyük alanları (ör. uzun açıklama metni) indeks dışı bırakmak
- C) Consistency'yi Strong yapmak
- D) Her sorguya `SELECT *` yazmak

<details><summary>Cevap</summary>

**A ve B.** Her indekslenen yol yazmada ek RU demektir. Strong okuma maliyetini artırır (yazmayı değil, ve düşürmez); `SELECT *` okuma tarafını etkiler.
</details>

#### Soru 12: CLI tamamlama
Aşağıdaki komut, değişken yüklü bir workload için **autoscale** container oluşturmalıdır (en çok 4000 RU/s'ye çıksın). Boşluğu doldurun:

```bash
az cosmosdb sql container create --account-name $ACC --resource-group $RG \
  --database-name shop --name orders --partition-key-path "/customerId" \
  --__________ 4000
```

- A) `--throughput`
- B) `--max-throughput`
- C) `--autoscale-rus`
- D) `--serverless`

<details><summary>Cevap</summary>

**B.** `--max-throughput` autoscale için Tmax'ı belirler (sistem Tmax'ın %10'u ile %100'ü arasında ölçeklenir). `--throughput` manual provisioned içindir. Serverless container değil **hesap** düzeyinde `EnableServerless` capability'siyle seçilir.
</details>

#### Soru 13: Senaryo
Bir ekip, geliştirme ortamı için seyrek kullanılan küçük bir uygulama yazıyor; trafik günde birkaç yüz istek, çoğu zaman sıfır. En düşük maliyet için hangi throughput modeli en uygundur?

- A) Manual provisioned, 10.000 RU/s
- B) Autoscale, Tmax 10.000 RU/s
- C) Serverless
- D) Database düzeyi shared throughput, 100.000 RU/s

<details><summary>Cevap</summary>

**C.** Serverless'ta rezerve throughput yoktur; yalnızca tüketilen RU ve depolama ödenir. Seyrek/dev-test için idealdir. Diğerleri boşta bile faturalanır (autoscale'de taban %10 Tmax).
</details>

#### Soru 14: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Sorguda partition key filtresi verilmezse sorgu cross-partition olur ve RU genellikle artar. | ☐ | ☐ |
| b) `x-ms-request-charge` başlığı / `RequestCharge` özelliği işlemin RU maliyetini verir. | ☐ | ☐ |
| c) Bir container'daki iki farklı partition key değeri aynı `id`'ye sahip item'lar içeremez. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Benzersizlik `(partition key değeri, id)` çiftidir; farklı partition'larda aynı `id` bulunabilir.
</details>

#### Soru 15: Kod tamamlama (Python)
Aşağıdaki Python kodu, partition key değerini bildiği bir item'ı **en ucuz yolla** okumalıdır. Boşluğu doldurun:

```python
item = container.__________(item="o-1", partition_key="customer-42")
```

- A) `query_items`
- B) `read_item`
- C) `get_item`
- D) `list_items`

<details><summary>Cevap</summary>

**B.** `ContainerProxy.read_item(item, partition_key)` point read'dir. `query_items` sorgu motorunu kullanır; `get_item`/`list_items` `azure-cosmos` API'sinde yoktur.
</details>

#### Soru 16: Tek seçim
Bir uygulama, Cosmos DB'ye hesap anahtarı kullanmadan, **managed identity** ile bağlanacak. Doğru yaklaşım hangisidir?

- A) Azure RBAC'ta kimliğe `Contributor` rolü vermek ve `new CosmosClient(endpoint, new DefaultAzureCredential())` kullanmak
- B) Cosmos DB **data-plane** rolü (ör. *Cosmos DB Built-in Data Contributor*) atamak (`az cosmosdb sql role assignment create`) ve `DefaultAzureCredential` kullanmak
- C) Connection string'i Key Vault yerine environment variable'a koymak
- D) Hesapta firewall'u kapatmak

<details><summary>Cevap</summary>

**B.** Cosmos veri düzlemi, Azure RBAC'tan ayrı kendi SQL role assignment sistemini kullanır. Management-plane `Contributor` rolü veri okuma/yazma yetkisi vermez (A yanlış). C ve D kimlik doğrulama sorununu çözmez.
</details>

#### Soru 17: Sıralama
Aşağıdaki işlemleri tipik **RU maliyeti en düşükten en yükseğe** sıralayın (aynı veri, varsayılan ayarlar): (1) tek partition filtreli sorgu · (2) cross-partition `SELECT *` sorgu · (3) 1 KB'lık item point read

<details><summary>Cevap</summary>

**(3) point read → (1) tek partition sorgu → (2) cross-partition sorgu.** Mutlak değerler veri setine göre değişir; `RequestCharge` ile ölç.
</details>

#### Soru 18: Vaka (case study)
**Senaryo:** *Contoso Shop*, Cosmos DB for NoSQL üzerinde `orders` container'ını `/status` partition key'i ile çalıştırıyor (`new/paid/shipped/cancelled`). Hesap Session consistency'de, container 10.000 RU/s manual. Kampanya günlerinde `paid` partition'ına yazmalar yığılıyor ve API 429 alıyor; Application Insights'ta toplam RU kullanımının yalnızca %20 olduğu görülüyor. En sık sorgu: *"müşterinin son 20 siparişi"*, `SELECT TOP 20 * FROM c WHERE c.customerId = @c ORDER BY c.orderDate DESC` (cross-partition çalışıyor). Siparişin detayı da `SELECT * FROM c WHERE c.id = @id` sorgusuyla alınıyor. Ayrıca farklı bir worker servisi, API'nin yazdığı siparişi hemen okuyup bazen "bulunamadı" alıyor (iki servis ayrı `CosmosClient` kullanıyor).

**Soru A.** 429'ların kök nedeni nedir ve kalıcı çözüm nedir?

**Soru B.** "Son 20 sipariş" ve "detay" sorgularını nasıl iyileştirirsiniz?

**Soru C.** Worker'ın "bulunamadı" sorununun nedeni ve çözümü nedir?

<details><summary>Cevap</summary>

**A.** **Hot partition.** `/status` düşük cardinality'dir; `paid` logical partition'ı tek physical partition'a düşer ve o partition'ın RU/s sınırı (toplam 10.000 RU/s'nin yalnızca bir kısmı) dolar — toplam kullanım %20 olsa bile 429 alınır. Autoscale/RU artırmak sorunu çözmez. Kalıcı çözüm: **yeni container'ı `/customerId`** (yüksek cardinality, sorgu desenine uygun) ile oluşturup veriyi taşımak (partition key yerinde değiştirilemez).

**B.** Yeni key ile "son 20 sipariş" **tek partition sorgusu** olur; `ORDER BY` için `(customerId ASC, orderDate DESC)` **composite index** eklenir (filtre `customerId` eşitlik + sıralama `orderDate`; composite yoksa RU yüksek kalır/çoklu alan ORDER BY hata verir). Detay için sorgu yerine **`ReadItemAsync(id, new PartitionKey(customerId))`** (point read); projection ile gereksiz alanlar çekilmez.

**C.** Session consistency'de read-your-writes garantisi **aynı client/session token** içindir; API ve worker ayrı `CosmosClient` kullandığı için worker yazılanı henüz görmeyebilir. Çözüm: session token'ı API'den worker'a iletmek (`ItemRequestOptions.SessionToken`) — ya da gereksinim gerçekten küresel taze okuma ise (pahalı) Strong/Bounded; ya da worker'ı olay (mesaj) akışına bağlayıp item'ı payload'dan okumak. Strong'u "kolay çözüm" diye seçmek okuma RU'sunu ~2× yapar.
</details>

---

**Puanlama:** 15-18 → hazırsın · 12-14 → README 3.1 (partition key), 3.4 (indexing) ve 3.5 (consistency) tablolarını tekrar et · < 12 → konuyu baştan çalış, 3.7 laboratuvarında `RequestCharge` ölçümlerini kendin yap.


---

## 2.2 Cosmos DB ile Vector Search ve Change Feed

> **Sınav Alanı:** Develop AI solutions by using Azure data management services (%30-35) · **Konu Kodu:** C2.5

### 1. Ne?

İki ayrı yetenek, tek bir kanonik desende buluşur:

| Yetenek | Ne yapar? | Anahtar kavramlar |
|---|---|---|
| **Vector search** | Embedding (kayan noktalı sayı dizisi) saklar ve **anlamsal benzerliğe** göre top-k sonuç döner | vector embedding policy, vector index (`flat` / `quantizedFlat` / `diskANN`), `VectorDistance` |
| **Change feed** | Container'daki item değişikliklerini **sırayla** okunabilir bir akış olarak sunar | change feed processor, lease container, checkpoint |

Kanonik desen (RAG verisini taze tutmak):

```text
 içerik değişir ──► Cosmos container (products)
                        │  change feed
                        ▼
              change feed processor / Functions trigger
                        │  embedding modeli (Azure OpenAI / Foundry) çağır
                        ▼
              item'ı PATCH et: embedding + embeddingSourceHash
                        │
                        ▼
   kullanıcı sorusu ──► embedding ──► SELECT TOP k ... ORDER BY VectorDistance(...)
```

Vector fikrinin kendisi (embedding nedir, cosine/dot/L2, chunking, RAG) → M15.4; burada Cosmos'a özgü yapılandırma ve sorgu vardır.

---

### 2. Neden?

- **Sınav:** "Policy'de boyut uyuşmuyor", "hangi index türü?", "`VectorDistance` sorgusunu tamamla", "change feed silmeleri neden göstermiyor?", "lease container ne işe yarar?" sorularının hepsi bu konudandır.
- **Gerçek işte:** Operasyonel veri (ürün, sipariş, sohbet geçmişi) zaten Cosmos'ta ise **ayrı bir vector veritabanı** işletmek, senkronizasyon, ikinci güvenlik/yedekleme sınırı ve tutarsızlık demektir. Aynı container'da hem metadata hem vector tutmak ve `WHERE` filtresiyle birleştirmek mimariyi sadeleştirir.
- **Tazelik:** RAG'in en sinsi hatası **bayat embedding**'dir (içerik güncellendi, vector eski). Change feed bunu olay güdümlü ve ölçeklenebilir biçimde çözer.

---

### 3. Nasıl?

#### 3.1 Vector embedding policy ve vector indexing policy

İki ayrı politika vardır ve **ikisi de container oluşturulurken verilir**; vector policy sonradan değiştirilemez (özellik 2026 başı itibarıyla yeni container'lar içindir — güncel kısıtları doğrula). Ayrıca hesapta vector search capability'si gerekebilir (`EnableNoSQLVectorSearch`; portal Features sayfası veya CLI — yeni hesaplarda varsayılan olup olmadığını doğrula).

| Politika | Ne tanımlar? | Alanlar |
|---|---|---|
| **Vector embedding policy** | Vector alanının *anlamı* | `path`, `dataType` (`float32`, …), `dimensions`, `distanceFunction` (`cosine`, `dotproduct`, `euclidean`) |
| **Vector indexing policy** (indexing policy içinde `vectorIndexes`) | Vector alanının *nasıl aranacağı* | `path` (embedding policy'deki ile **aynı**), `type` (`flat` / `quantizedFlat` / `diskANN`) |

```json
{
  "vectorEmbeddingPolicy": {
    "vectorEmbeddings": [
      { "path": "/embedding", "dataType": "float32", "distanceFunction": "cosine", "dimensions": 1536 }
    ]
  },
  "indexingPolicy": {
    "indexingMode": "consistent",
    "includedPaths": [ { "path": "/*" } ],
    "excludedPaths": [ { "path": "/embedding/*" } ],
    "vectorIndexes": [ { "path": "/embedding", "type": "diskANN" } ]
  }
}
```

Vector yolu `excludedPaths`'e **eklenir**: vector'ü normal (range) index'e sokmak yazma RU'sunu ve gecikmeyi gereksiz şişirir; arama zaten `vectorIndexes` ile yapılır.

**Index türleri:**

| Tür | Algoritma | Doğruluk | Ölçek / maliyet | Boyut sınırı* | Ne zaman? |
|---|---|---|---|---|---|
| `flat` | Brute-force, tam vector | %100 recall | Küçük veri; yüksek RU/gecikme büyüyünce | ~505 boyuta kadar | Prototip, birkaç bin vector, tam doğruluk şart |
| `quantizedFlat` | Brute-force, **sıkıştırılmış (quantized)** vector | ~%100'e yakın (hafif kayıp) | Orta ölçek (on binlerce vector) | 4096'ya kadar | Orta veri, düşük RU |
| **`diskANN`** | Yaklaşık en yakın komşu (ANN), disk-optimize | Yüksek ama yaklaşık (recall < %100) | **Milyonlarca vector**, düşük gecikme/RU | 4096'ya kadar | Üretim ölçeği |

\* Sınırlar sürümle değişebilir; sayıları ezberleme, **"flat'in boyut sınırı daha düşüktür"** bilgisini hatırla.
- `quantizedFlat` ve `diskANN` için doğru nicemleme adına kabaca **en az ~1000 vector** gerekir; daha azında sistem tam tarama (full scan) yapar — küçük test verisinde "indeks işe yaramıyor" izlenimi buradan gelir.
- Embedding **boyutu modelle eşleşmelidir**: `text-embedding-3-small` varsayılan **1536**, `text-embedding-3-large` **3072** (boyut kısaltma parametresiyle daha düşük). Policy `dimensions` ile eklenen vector uzunluğu farklıysa yazma/sorgu hata verir.
- **Distance function modelle uyumlu seçilir:** normalize embedding'lerde cosine ≈ dot product; modelin dokümanı hangisini öneriyorsa o.

**.NET** ile container oluşturma (`Microsoft.Azure.Cosmos` 3.45+):

```csharp
using System.Collections.ObjectModel;
using Microsoft.Azure.Cosmos;

var embeddings = new Collection<Embedding>
{
    new()
    {
        Path = "/embedding",
        DataType = VectorDataType.Float32,
        DistanceFunction = DistanceFunction.Cosine,
        Dimensions = 1536,
    }
};

var props = new ContainerProperties(id: "products", partitionKeyPath: "/category")
{
    VectorEmbeddingPolicy = new VectorEmbeddingPolicy(embeddings),
    IndexingPolicy = new IndexingPolicy
    {
        VectorIndexes = { new VectorIndexPath { Path = "/embedding", Type = VectorIndexType.DiskANN } },
        IncludedPaths = { new IncludedPath { Path = "/*" } },
        ExcludedPaths = { new ExcludedPath { Path = "/embedding/*" } },   // keep the vector out of the range index
    },
};

Container products = (await database.CreateContainerIfNotExistsAsync(props, throughput: 400)).Container;
```

**Python** eşdeğeri:

```python
from azure.cosmos import PartitionKey

vector_embedding_policy = {"vectorEmbeddings": [
    {"path": "/embedding", "dataType": "float32", "distanceFunction": "cosine", "dimensions": 1536}]}
indexing_policy = {
    "indexingMode": "consistent",
    "includedPaths": [{"path": "/*"}],
    "excludedPaths": [{"path": "/embedding/*"}],
    "vectorIndexes": [{"path": "/embedding", "type": "diskANN"}],
}
products = db.create_container_if_not_exists(
    id="products", partition_key=PartitionKey(path="/category"),
    indexing_policy=indexing_policy, vector_embedding_policy=vector_embedding_policy)
```

#### 3.2 Vector sorguları

`VectorDistance(<vectorPath>, <queryVector>)` benzerlik skorunu döndürür; **top-k = `TOP k` + `ORDER BY VectorDistance(...)`**. Her zaman `TOP` kullan (sınırsız sonuç RU'yu patlatır), skor için embedding'in kendisini **projection'a koyma** (6 KB'lık vector'ü her satırda taşımak RU ve bant genişliği israfı).

```sql
-- top-5 most similar products in a category (metadata filter + vector ranking)
SELECT TOP @k c.id, c.name, c.category,
       VectorDistance(c.embedding, @queryVector) AS score
FROM c
WHERE c.category = @category AND c.price < @maxPrice
ORDER BY VectorDistance(c.embedding, @queryVector)
```

- Sıralama **en benzer ilk** gelecek şekilde yapılır; cosine/dotproduct'ta skor yüksek = benzer, euclidean'da düşük = benzer (`ASC/DESC` ile oynamak yerine doğru distance function'ı seç).
- **Metadata filtresi** (`WHERE`) vector aramayla birleşir. Filtre partition key üzerindeyse sorgu **tek partition'a** gider (en ucuzu); aksi hâlde cross-partition (→ C2.4).
- `VectorDistance` için **yol, vector policy'deki `path` ile aynı** ve sorgu vector'ünün boyutu `dimensions` ile aynı olmalıdır; vector index yoksa sorgu çalışır ama brute-force taranır (pahalı).
- *(bağlam)* **Full-text search** ve **hybrid search**: BM25 tabanlı full-text skoru ile vector skoru **RRF (Reciprocal Rank Fusion)** ile birleştirilir (`ORDER BY RANK RRF(VectorDistance(...), FullTextScore(...))`) — tam söz dizimi için güncel dokümana bak; kavramsal arka plan → M15.4.

```csharp
float[] queryVector = await embedder.EmbedAsync("waterproof hiking boots");   // dimensions must match the policy

var q = new QueryDefinition("""
        SELECT TOP @k c.id, c.name, VectorDistance(c.embedding, @v) AS score
        FROM c WHERE c.category = @cat
        ORDER BY VectorDistance(c.embedding, @v)
        """)
    .WithParameter("@k", 5).WithParameter("@v", queryVector).WithParameter("@cat", "footwear");

double ru = 0;
using var it = products.GetItemQueryIterator<dynamic>(q,
    requestOptions: new QueryRequestOptions { PartitionKey = new PartitionKey("footwear") });
while (it.HasMoreResults) { var page = await it.ReadNextAsync(); ru += page.RequestCharge; foreach (var r in page) Console.WriteLine($"{r.name}  {r.score}"); }
Console.WriteLine($"RU: {ru}");
```

```python
rows = products.query_items(
    query="""SELECT TOP @k c.id, c.name, VectorDistance(c.embedding, @v) AS score
             FROM c WHERE c.category = @cat ORDER BY VectorDistance(c.embedding, @v)""",
    parameters=[{"name": "@k", "value": 5}, {"name": "@v", "value": query_vector},
                {"name": "@cat", "value": "footwear"}],
    partition_key="footwear")
```

#### 3.3 Change feed ⭐

**Change feed**, container'daki **eklenen ve güncellenen** item'ların kalıcı, sıralı kaydıdır (yalnızca okuma; ayrı bir "kuyruk" kaynağı kurmazsın, maliyeti okuma RU'sudur).

| Mod | Ne görür? | Silmeler | Not |
|---|---|---|---|
| **Latest version** (varsayılan) | Her item'ın **son hâli** (insert ve update ayırt edilmez) | **Görünmez** | Ara değişiklikler birleşebilir |
| **All versions and deletes** | Her değişiklik + **silme/TTL** olayları, işlem türü metadata'sı | Görünür | Hesapta **continuous backup** şart; retention backup penceresine bağlı; 2026 ortasında GA (SDK/trigger desteğini doğrula) |

- **Sıra garantisi:** *bir logical partition (aynı partition key) içinde* değişiklik sırası korunur; partition'lar arası global sıra yoktur.
- **At-least-once:** Aynı değişiklik tekrar teslim edilebilir → işleyici **idempotent** olmalı.
- **Silme çözümü (latest version modda):** fiziksel silme yerine `deleted: true` (soft delete) yaz ve `ttl` ile sonra temizle; ya da all-versions-and-deletes modu.

**Tüketim yolları:**

| Yol | Ne? | Ne zaman |
|---|---|---|
| **Change feed processor** (.NET/Java SDK) | Dağıtık, otomatik checkpoint'li, ölçeklenir | Arka plan worker/servis — **bu konunun odağı** |
| **Azure Functions Cosmos DB trigger** | Processor'ı saran serverless trigger (→ C2.8) | Olay güdümlü, ölçek yönetimi platformda |
| **Pull model** (`GetChangeFeedIterator` / Python `query_items_change_feed`) | Tüketim hızını/partition'ı kendin yönetirsin | Özel kontrol, tek seferlik tarama |

**Change feed processor** bileşenleri:
1. **Monitored container** — değişikliklerin okunduğu container.
2. **Lease container** — her partition aralığı için bir **lease** (sahiplik + **continuation token/checkpoint**) tutar. Birden çok worker örneği lease'leri **paylaşır** (yatay ölçek); worker çökerse lease başka örneğe geçer, **kaldığı yerden devam eder**. Lease container kaybolursa/sıfırlanırsa işleme baştan (veya `WithStartTime`'dan) başlar. Partition key `/id`.
3. **Processor adı** (`processorName`) — aynı monitored container'ı birbirinden bağımsız okuyan farklı tüketici grupları ayrı adlarla (lease prefix'iyle) çalışır.
4. **Delegate/handler** — değişiklik grubunu işler; başarıyla dönerse checkpoint ilerler, **exception fırlarsa** o grup yeniden teslim edilir.
5. **Instance name** — worker örneğinin benzersiz adı (lease sahipliği için).

```csharp
// Reads changes from "products", writes leases to "leases" (partition key /id)
ContainerResponse leaseResponse = await database.CreateContainerIfNotExistsAsync("leases", "/id");
Container leases = leaseResponse.Container;

ChangeFeedProcessor processor = products
    .GetChangeFeedProcessorBuilder<Product>(processorName: "embedding-refresher", onChangesDelegate: HandleChangesAsync)
    .WithInstanceName(Environment.MachineName)       // unique per worker instance
    .WithLeaseContainer(leases)
    .WithPollInterval(TimeSpan.FromSeconds(5))
    .Build();

await processor.StartAsync();
Console.WriteLine("Change feed processor started. Ctrl+C to stop.");
await Task.Delay(Timeout.Infinite, cancellationToken);   // keep the worker alive
await processor.StopAsync();

async Task HandleChangesAsync(ChangeFeedProcessorContext ctx, IReadOnlyCollection<Product> changes, CancellationToken ct)
{
    foreach (Product p in changes)
    {
        string hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(p.Description)));
        if (p.EmbeddingSourceHash == hash) continue;            // guard: our own PATCH comes back through the feed -> avoid infinite loop

        float[] vector = await embedder.EmbedAsync(p.Description, ct);
        await products.PatchItemAsync<Product>(p.Id, new PartitionKey(p.Category),
        [
            PatchOperation.Set("/embedding", vector),
            PatchOperation.Set("/embeddingSourceHash", hash),
        ], cancellationToken: ct);                               // partial update, no read-modify-write race
    }
}

public sealed record Product(string Id, string Category, string Name, string Description,
                             float[]? Embedding, string? EmbeddingSourceHash);
```

**Azure Functions (isolated worker)** karşılığı (→ C2.8):

```csharp
[Function("RefreshEmbedding")]
public async Task Run([CosmosDBTrigger(
        databaseName: "shop", containerName: "products",
        Connection = "CosmosConnection",                    // identity-based: CosmosConnection__accountEndpoint
        LeaseContainerName = "leases", CreateLeaseContainerIfNotExists = true)] IReadOnlyList<Product> changes)
{ /* same handler body */ }
```

Python'da (azure-cosmos) processor sınıfı yoktur; **pull model** kullanılır (checkpoint'i sen tutarsın):

```python
for change in products.query_items_change_feed(is_start_from_beginning=True):
    if change.get("embeddingSourceHash") != sha256_hex(change["description"]):
        change["embedding"] = embed(change["description"])
        change["embeddingSourceHash"] = sha256_hex(change["description"])
        products.upsert_item(change)
```

#### 3.4 Lab — ürün araması ve embedding yenileme

Bütçe alarmı kur (→ C1.11). **Serverless** hesap kuruşluktur; **emülatör** da kullanılabilir (vector/change feed desteği emülatör sürümüne bağlı — doğrula). Gerçek embedding modeli isteğe bağlıdır (→ M15.4): önce deterministik **sahte embedder** (kelime hash'lerinden 64 boyutlu vector) kullan, sonra `Microsoft.Extensions.AI` `IEmbeddingGenerator<string, Embedding<float>>` ile gerçek modele geç.

```bash
RG=rg-ai200-c25
ACC=cosmos-c25-$RANDOM
az group create --name $RG --location westeurope

az cosmosdb create --name $ACC --resource-group $RG --kind GlobalDocumentDB \
  --capabilities EnableServerless EnableNoSQLVectorSearch \
  --locations regionName=westeurope failoverPriority=0 isZoneRedundant=False
az cosmosdb sql database create --account-name $ACC --resource-group $RG --name shop
# The products container (with vector policy) is created from code (3.1): vector policy cannot be set afterwards.
```

Adımlar:
1. 3.1'deki kodla `products` (`flat`, `dimensions = 64`) ve `leases` container'larını oluştur; 40 ürünü sahte embedding'leriyle yükle.
2. 3.2 sorgusuyla `"waterproof boots"` için `category = footwear` filtreli top-5 ara; `RequestCharge`'ı not et. Filtreyi kaldırıp (cross-partition) RU farkını ölç.
3. 3.3'teki processor'ı çalıştır; bir ürünün `description`'ını güncelle → birkaç saniye içinde `embedding` ve `embeddingSourceHash`'in değiştiğini, ikinci bir tetiklemenin **hash koruması** sayesinde oluşmadığını gözle.
4. **Bilerek bozuk senaryolar:** (a) 1536 boyutlu vector yaz → boyut uyuşmazlığı hatası; (b) hash korumasını kaldır → worker sonsuz döngüde RU yakar; (c) bir item'ı **sil** → processor'a hiç düşmez (latest version mod); (d) worker'ı durdurup yeniden başlat → lease sayesinde kaldığı yerden devam eder; (e) lease container'ı silip başlat → baştan işler.

```bash
# Cleanup
az group delete --name $RG --yes --no-wait
```

---

### 4. Gerçek dünya senaryosu

Bir outdoor mağazası ürün arama ve sohbet asistanı (RAG) kuruyor:

| İhtiyaç | Karar | Neden |
|---|---|---|
| 2 milyon ürün, "su geçirmez yürüyüş botu" gibi anlamsal arama | `diskANN` + cosine | Ölçekte düşük RU/gecikme; `flat` bu ölçekte pahalı |
| "Yalnızca stokta olan, 2000 TL altı" | `WHERE c.inStock = true AND c.price < @max` + `VectorDistance` | Tek sorguda filtre + benzerlik |
| Kategori başına sorgu | Partition key `/category` | Filtreli arama tek partition'a gider |
| Editörler açıklamayı güncelliyor | Change feed processor → embedding'i yenile | Vector her zaman güncel içeriği yansıtır |
| Worker 3 örnek çalışıyor, biri çöküyor | Lease container | İş otomatik paylaşılır/devralınır |
| Ürün silindiğinde arama sonucundan çıkmalı | `deleted: true` soft delete (veya all-versions-and-deletes) | Latest version feed silmeyi göstermez |
| Embedding modeli değişiyor (1536 → 3072) | Yeni container + yeni policy, toplu yeniden embedding | Vector policy sonradan değişmez; farklı modellerin vector'leri karşılaştırılamaz |

---

### 5. Sınav perspektifi

| Senaryodaki ifade | Cevap |
|---|---|
| "Milyonlarca vector, düşük gecikme" | **diskANN** |
| "Küçük veri seti, %100 doğruluk" | **flat** |
| "Orta ölçek, düşük RU, sıkıştırılmış" | **quantizedFlat** |
| "Vector yazma hatası: dimension mismatch" | Policy `dimensions` ≠ embedding modeli çıktısı |
| "Vector alanı yazma RU'sunu çok artırıyor" | Vector yolunu **excludedPaths**'e ekle (vector index ayrı) |
| "En benzer 5 kayıt" | `SELECT TOP 5 ... ORDER BY VectorDistance(c.embedding, @q)` |
| "Mevcut container'a vector policy ekle" | Genelde mümkün değil; yeni container (2026 başı itibarıyla — doğrula) |
| "Sadece 100 vector var, diskANN yavaş/yaklaşık" | `quantizedFlat/diskANN` için ~1000 vector altı full scan |
| "Change feed'de silinen item görünmüyor" | Latest version modu silmeleri yakalamaz → soft delete veya all versions and deletes |
| "Worker yeniden başladı, baştan mı işler?" | Hayır — **lease container**'daki checkpoint'ten devam eder |
| "Birden çok worker aynı değişikliği işlemesin" | Aynı processor adı + ortak lease container, farklı instance adı |
| "Item'ı güncelleyince feed yeniden tetikleniyor, döngü" | İdempotency/hash koruması, değişmediyse yazma |
| "Olay güdümlü serverless tüketim" | Azure Functions Cosmos DB trigger |

**Tuzaklar:**
- Policy'deki `path` ile `vectorIndexes[].path` aynı olmalı; vector policy ile **normal index ayrı** kavramlardır.
- Change feed bir **mesaj kuyruğu değildir**: kalıcı, tekrar okunabilir, sıra yalnızca partition içinde, at-least-once.
- `VectorDistance` skoru metrik yönü: cosine/dot için yüksek = benzer; euclidean için düşük = benzer.
- Lease container'ı "gereksiz kopya" sanıp silmek: checkpoint kaybı.
- Change feed "yeni insert + update" içindir; TTL ile silinenler yalnızca all-versions-and-deletes modunda görünür.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Embedding'i her sorguda projection'a koymak | Yalnızca gereken alanlar + skor |
| `TOP` olmadan `ORDER BY VectorDistance` | Her zaman `TOP k` |
| Vector yolunu range index'te bırakmak | `excludedPaths: /embedding/*` |
| Farklı modellerin vector'lerini aynı alanda karıştırmak | Tek model/boyut/distance per alan; model değişince yeniden embedding |
| Çok küçük veri setinde `diskANN` performansını yargılamak | ~1000 vector altında full scan; gerçekçi veriyle ölç |
| Processor handler'ında hata yutmak | Exception → yeniden teslim; yutarsan değişiklik kaybolur |
| Güncelleme yazan handler'da koruma yok | Sonsuz döngü + RU yangını (hash/versiyon alanı) |
| Her worker örneğine aynı `instanceName` vermek | Benzersiz instance adı (lease sahipliği) |
| Lease container'ı monitored container'la aynı yapıp partition key'i karıştırmak | Ayrı `leases` container'ı, `/id` |
| Embedding üretimini handler'da seri/sınırsız çağırmak | Model rate limit (429) → batch/retry/backoff (→ M15.4) |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| `flat` → `quantizedFlat` → `diskANN` | Ölçek, düşük RU/gecikme | Recall < %100 (ANN), index oluşturma/RU ayarı, ≥~1000 vector |
| Yüksek boyutlu embedding (3072) | Daha zengin anlam | Depolama, RU, gecikme ↑; boyut kısaltma çoğu zaman yeterli |
| Vector + metadata filtresi aynı container'da | Tek sistem, filtreli arama | Container RU'su iş yükü ile paylaşılır; ayrı container düşünülebilir |
| Partition key = filtre alanı | Filtreli arama tek partition | Filtresiz arama fan-out |
| Change feed processor | Ölçeklenebilir, dayanıklı | Lease container + kendi worker'ını işletmek; Functions trigger bunu platforma bırakır |
| All versions and deletes | Silme/ara versiyon görünür | Continuous backup maliyeti ve retention bağımlılığı |
| Embedding'i yazma anında (senkron) üretmek | Anlık tutarlılık | Yazma gecikmesi + model hatası yazmayı bozar; change feed ile **asenkron** + eventual tazelik genelde daha sağlam |

---

### 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Vector search in Azure Cosmos DB for NoSQL](https://learn.microsoft.com/en-us/azure/cosmos-db/vector-search) · [Index and query vector data](https://learn.microsoft.com/en-us/cosmos-db/index-vector-data)
- [Index and query vectors in .NET](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-dotnet-vector-index-query) · [in Python](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-python-vector-index-query)
- [Change feed in Azure Cosmos DB](https://learn.microsoft.com/en-us/azure/cosmos-db/change-feed) · [Change feed processor](https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/change-feed-processor) · [All versions and deletes mode](https://devblogs.microsoft.com/cosmosdb/azure-cosmos-db-all-versions-and-deletes-change-feed-mode-is-now-generally-available/)
- [Azure Functions Cosmos DB trigger](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-cosmosdb-v2-trigger)
- Önceki: [C2.4 Cosmos DB for NoSQL](#21-cosmos-db-for-nosql-sdk-partitioning-indexing-consistency-ru) · Sonraki: [C2.6 PostgreSQL ve pgvector](#23-azure-database-for-postgresql-ve-pgvector)


### 📝 Bölüm 2.2 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir ekip 3 milyon doküman parçasının embedding'ini Cosmos DB for NoSQL'de saklayacak ve düşük gecikmeyle top-k arama yapacak. Hangi vector index türü **en uygundur**?

- A) `flat`
- B) `quantizedFlat`
- C) `diskANN`
- D) Normal range index

<details><summary>Cevap</summary>

**C.** `diskANN`, milyonlarca vector için düşük gecikme/RU sunan yaklaşık (ANN) indekstir. `flat` brute-force'tur ve bu ölçekte pahalı; `quantizedFlat` orta ölçeğe uygundur; range index vector araması için kullanılmaz.
</details>

#### Soru 2: Tek seçim
Bir geliştirici 1536 boyutlu embedding üreten bir model kullanıyor; ancak container'ın vector embedding policy'sinde `"dimensions": 768` yazıyor. Item yazılınca ne olur?

- A) Vector otomatik olarak 768'e kırpılır
- B) Yazma/sorgu, boyut uyuşmazlığı nedeniyle hata verir
- C) Cosmos policy'yi otomatik 1536'ya günceller
- D) Vector indekslenmez ama yazma başarılı olur ve sessizce görmezden gelinir

<details><summary>Cevap</summary>

**B.** Vector uzunluğu policy'deki `dimensions` ile birebir eşleşmelidir. Cosmos kırpmaz, policy'yi kendiliğinden güncellemez; vector policy zaten sonradan değiştirilemez, bu yüzden doğru çözüm yeni container'dır.
</details>

#### Soru 3: Kod tamamlama (SQL)
En benzer 5 ürünü, `footwear` kategorisinde bulmak için boşluğu doldurun:

```sql
SELECT __________ c.id, c.name, VectorDistance(c.embedding, @q) AS score
FROM c
WHERE c.category = 'footwear'
ORDER BY VectorDistance(c.embedding, @q)
```

- A) `LIMIT 5`
- B) `TOP 5`
- C) `FIRST 5`
- D) `DISTINCT 5`

<details><summary>Cevap</summary>

**B.** Cosmos NoSQL sorgu dilinde sonuç sayısı `TOP n` ile sınırlanır (`LIMIT` bu söz diziminde yoktur; `OFFSET ... LIMIT` ayrı bir ifadedir). Vector sorgularında `TOP k` kullanmak zorunlu olmasa da RU'yu kontrol eden önerilen uygulamadır.
</details>

#### Soru 4: Çoklu seçim
Vector alanı içeren bir container için aşağıdakilerden hangileri **doğrudur**? (İki seçenek)

- A) Vector yolu, indexing policy'de `excludedPaths`'e eklenerek range index'ten çıkarılmalıdır.
- B) `vectorIndexes[].path`, vector embedding policy'deki `path` ile aynı olmalıdır.
- C) Vector embedding policy her zaman container oluşturulduktan sonra değiştirilebilir.
- D) `flat` index tüm boyut sınırlarında `diskANN`'dan daha ölçeklenebilirdir.

<details><summary>Cevap</summary>

**A ve B.** C yanlış: vector policy container oluşturulurken verilir ve sonradan değiştirilemez (2026 başı itibarıyla). D yanlış: `flat` brute-force'tur, boyut sınırı daha düşüktür ve büyük veride ölçeklenmez.
</details>

#### Soru 5: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) `quantizedFlat` ve `diskANN` indeksleri için çok az vector varsa (kabaca < 1000) sistem tam tarama yapabilir. | ☐ | ☐ |
| b) `flat` index %100 recall (tam doğruluk) sağlar. | ☐ | ☐ |
| c) `distanceFunction` seçenekleri arasında `cosine`, `dotproduct` ve `euclidean` vardır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Evet.** Nicemlemenin doğruluğu için yeterli vector gerekir; `flat` brute-force olduğundan tam sonuç verir (karşılığında büyük veride pahalıdır); üç mesafe fonksiyonu desteklenir.
</details>

#### Soru 6: Senaryo
Bir ekip her sorguda aşağıdaki gibi bir projection yazıyor ve yüksek RU ile bant genişliği tüketiyor:

```sql
SELECT TOP 10 * FROM c ORDER BY VectorDistance(c.embedding, @q)
```

Hangi değişiklik **en doğrudur**?

- A) `SELECT TOP 10 c.id, c.name, VectorDistance(c.embedding, @q) AS score ...` — embedding'i döndürme
- B) Consistency'yi Strong yapmak
- C) `TOP 10` yerine `TOP 1000` yapmak
- D) Embedding'i ikinci bir alana kopyalamak

<details><summary>Cevap</summary>

**A.** `SELECT *` her satırda binlerce float'lık vector'ü de taşır. Yalnızca gereken alanları ve skoru seçmek RU ve ağ yükünü düşürür. Strong okuma maliyetini artırır; daha büyük `TOP` ve ekstra kopya durumu kötüleştirir.
</details>

#### Soru 7: Sıralama
Aşağıdaki adımları, bir **change feed processor** değişiklik işlerken doğru **sırada** sıralayın: (1) handler başarıyla döner (2) lease'teki checkpoint ilerler (3) processor bir lease'e sahip olur (4) feed'den değişiklik grubu okunur ve handler'a verilir

<details><summary>Cevap</summary>

**3 → 4 → 1 → 2.** Processor önce partition aralığının lease'ini alır, değişiklikleri okuyup handler'a verir; handler hatasız dönünce checkpoint ilerler. Hata fırlatılırsa grup yeniden teslim edilir (at-least-once).
</details>

#### Soru 8: Tek seçim
Change feed processor'ın **lease container**'ının işlevi nedir?

- A) Silinen item'ların yedeğini tutmak
- B) Her partition aralığının sahipliğini ve checkpoint'ini (continuation) saklayıp worker örnekleri arasında paylaştırmak
- C) Embedding'leri önbelleğe almak
- D) Container'ın indexing policy'sini saklamak

<details><summary>Cevap</summary>

**B.** Lease'ler hem yük dağıtımını (hangi worker hangi aralığı işler) hem de kaldığı yerden devam etmeyi (checkpoint) sağlar. Silinen item yedekleme, önbellek veya indexing policy ile ilgisi yoktur.
</details>

#### Soru 9: Senaryo
Varsayılan (latest version) change feed ile çalışan bir processor, kaynak container'dan **silinen** ürünleri arama dizininden çıkarmak için kullanılıyor ama silmeler hiç tetiklenmiyor. Hangi iki çözüm doğrudur? (İki seçenek)

- A) Silme yerine `deleted: true` yazıp (soft delete) `ttl` ile sonra temizlemek
- B) **All versions and deletes** change feed modunu kullanmak (continuous backup gerektirir)
- C) Lease container'ı silip yeniden oluşturmak
- D) Processor'ın poll aralığını azaltmak

<details><summary>Cevap</summary>

**A ve B.** Latest version modu silmeleri yakalamaz. C ve D bu durumu değiştirmez; lease silmek yalnızca baştan işlemeye neden olur, poll aralığı gecikmeyi etkiler.
</details>

#### Soru 10: Kod tamamlama (C#)
Değişiklikleri işleyecek change feed processor'ı kurun. Boşlukları doldurun:

```csharp
ChangeFeedProcessor processor = products
    .GetChangeFeedProcessorBuilder<Product>("embedding-refresher", HandleChangesAsync)
    .WithInstanceName(Environment.MachineName)
    .__________(leaseContainer)
    .Build();
await processor.__________();
```

- A) `WithLeaseContainer` ve `StartAsync`
- B) `WithCheckpointContainer` ve `RunAsync`
- C) `WithStateContainer` ve `BeginAsync`
- D) `WithLeaseContainer` ve `ExecuteAsync`

<details><summary>Cevap</summary>

**A.** `WithLeaseContainer(Container)` ve `StartAsync()`/`StopAsync()` change feed processor API'sidir.
</details>

#### Soru 11: Senaryo
Bir worker, ürün güncellendiğinde yeni embedding'i hesaplayıp **aynı item'ı** `ReplaceItem` ile güncelliyor. Birkaç dakika sonra RU tüketimi fırlıyor ve model çağrı sayısı sürekli artıyor. En olası neden ve çözüm hangisidir?

- A) Lease container çok küçük; RU artırılmalı
- B) Worker'ın kendi yazması change feed'e yeniden düşüyor (sonsuz döngü); içerik hash'i/versiyon alanıyla değişmemiş içerik için yazmayı atla
- C) Consistency Strong olmalı
- D) Change feed ters sırada okunuyor; poll aralığı artırılmalı

<details><summary>Cevap</summary>

**B.** Her yazma yeni bir değişiklik olayı üretir. İşleyici idempotent olmalı: `embeddingSourceHash` gibi bir alanla kaynak metin değişmediyse embedding'i yeniden üretme/yazma. Diğer şıklar döngüyü çözmez.
</details>

#### Soru 12: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Change feed, aynı logical partition içindeki değişikliklerin sırasını korur. | ☐ | ☐ |
| b) Change feed processor, her değişikliği tam olarak bir kez (exactly-once) teslim etmeyi garanti eder. | ☐ | ☐ |
| c) Aynı monitored container'ı iki farklı iş için okumak istiyorsan farklı processor adları kullanabilirsin. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Teslim at-least-once'tır; işleyici idempotent olmalı. Farklı processor adları (lease prefix'i) bağımsız tüketici grupları oluşturur.
</details>

#### Soru 13: Kod tamamlama (Python)
Aşağıdaki Python kodu, vector policy ve vector index ile container oluşturur. Boşluğu doldurun:

```python
indexing_policy = {
    "includedPaths": [{"path": "/*"}],
    "excludedPaths": [{"path": "/embedding/*"}],
    "vectorIndexes": [{"path": "/embedding", "type": "__________"}],
}
```

- A) `hnsw`
- B) `diskANN`
- C) `ivfflat`
- D) `ann`

<details><summary>Cevap</summary>

**B.** Cosmos DB for NoSQL vector index türleri `flat`, `quantizedFlat` ve `diskANN`'dır. `hnsw` ve `ivfflat` pgvector'ün index türleridir (→ C2.6).
</details>

#### Soru 14: Tek seçim
Bir ekip, ürün açıklaması değiştiğinde embedding'i **olay güdümlü serverless** olarak yenilemek istiyor ve worker altyapısını işletmek istemiyor. Hangisi en uygundur?

- A) Zamanlayıcıyla her gece tüm container'ı taramak
- B) Azure Functions **Cosmos DB trigger**'ı
- C) Container'ı polling eden bir Logic App
- D) İstemci uygulamada embedding'i hesaplayıp yazmak

<details><summary>Cevap</summary>

**B.** Cosmos DB trigger, change feed processor'ı platform üzerinde çalıştırır (lease yönetimi, ölçek dahil; → C2.8). Gece taraması taze olmayan embedding bırakır; istemcide hesaplama güvenlik/tutarlılık sorunları yaratır.
</details>

#### Soru 15: Çoklu seçim
Hangi ifadeler vector tabanlı RAG için **embedding modelini değiştirme** (1536 → 3072 boyut) konusunda doğrudur? (İki seçenek)

- A) Yeni boyut için yeni vector policy gerekir; pratikte yeni bir container oluşturulur.
- B) Eski ve yeni modelin vector'leri aynı alanda karıştırılırsa benzerlik skorları anlamsızlaşır.
- C) Cosmos mevcut vector'leri otomatik yeni modele dönüştürür.
- D) Yalnızca `distanceFunction` değiştirilirse tüm vector'ler uyumlu hâle gelir.

<details><summary>Cevap</summary>

**A ve B.** Farklı modellerin uzayları karşılaştırılamaz; yeniden embedding gerekir. Bu iş de bir change feed/toplu iş ile yapılır. Cosmos embedding üretmez/dönüştürmez.
</details>

#### Soru 16: Vaka (case study)
**Senaryo:** *Fabrikam Docs*, bilgi tabanı makalelerini Cosmos DB for NoSQL'de tutuyor (`/tenantId` partition key, ~800.000 makale, `text-embedding-3-small`, cosine). Gereksinimler: (1) kullanıcı sorgusuna göre kendi kiracısının makaleleri içinde anlamsal arama, düşük gecikme; (2) editörler makaleyi güncelleyince arama sonuçları dakikalar içinde güncel olmalı; (3) arşivlenen (silinen) makaleler arama sonuçlarından çıkmalı; (4) worker, 3 örnek olarak çalışacak.

**Soru A.** Vector embedding policy ve index türünü belirleyin.
**Soru B.** Kiracıya özel aramayı yazın (yalnızca sorgu şekli).
**Soru C.** Embedding'in taze kalması ve silmelerin işlenmesi için mimariyi tarif edin.

<details><summary>Cevap</summary>

**A.** `/embedding`, `float32`, `dimensions: 1536`, `distanceFunction: cosine`; indexing policy'de `vectorIndexes: [{ path: "/embedding", type: "diskANN" }]` ve `excludedPaths: [/embedding/*]`. 800 bin vector → `diskANN` (flat/quantizedFlat bu ölçekte pahalı ve yavaş). Policy container oluşturulurken verilir.

**B.**
```sql
SELECT TOP @k c.id, c.title, VectorDistance(c.embedding, @q) AS score
FROM c WHERE c.tenantId = @tenant
ORDER BY VectorDistance(c.embedding, @q)
```
Sorguya `PartitionKey(tenantId)` verilirse arama tek partition'a gider; embedding projection'a konmaz.

**C.** Change feed processor (veya Functions Cosmos DB trigger) `articles` container'ını izler; `lease` container'ı 3 worker örneği arasında partition aralıklarını paylaştırır ve checkpoint tutar. Handler, içerik **hash'i** değiştiyse embedding'i yeniden üretip `PatchItem` ile yazar (hash koruması sonsuz döngüyü önler; handler idempotent, at-least-once teslime hazır). Silmeler latest version feed'de görünmediği için ya **soft delete** (`deleted: true`, aramada `WHERE c.deleted != true` ve `ttl` ile temizlik) ya da **all versions and deletes** modu (continuous backup gerekir) kullanılır.
</details>

---

**Puanlama:** 13-16 → hazırsın · 10-12 → README 3.1 (policy/index türleri) ve 3.3 (change feed bileşenleri) bölümlerini tekrar et · < 10 → konuyu baştan çalış, 3.4 laboratuvarındaki bilerek bozuk senaryoları uygula.


---

## 2.3 Azure Database for PostgreSQL ve pgvector

> **Sınav Alanı:** Develop AI solutions by using Azure data management services (%30-35) · **Konu Kodu:** C2.6

### 1. Ne?

**Azure Database for PostgreSQL — Flexible Server**, yönetilen PostgreSQL'dir (yedekleme, yama, HA, zone redundancy). **pgvector** (SQL'de eklenti adı `vector`), PostgreSQL'e `vector` veri tipi, mesafe operatörleri ve ANN index'leri (HNSW, IVFFlat) ekleyen açık kaynak eklentidir. Sonuç: ilişkisel veri + **vector benzerliği** + `WHERE`/`JOIN`/transaction **tek veritabanında**.

```text
documents tablosu
 ├─ id, tenant_id, title, content, created_at         ← ilişkisel kolonlar (metadata)
 └─ embedding vector(1536)                            ← pgvector
Sorgu:  WHERE tenant_id = 7  ORDER BY embedding <=> $query  LIMIT 5
Index:  HNSW (vector_cosine_ops)   veya   IVFFlat
```

| Kavram | Özet |
|---|---|
| `vector(n)` | n boyutlu float32 vector (index'lenebilir boyut üst sınırı eklenti sürümüne göre ~2000; `halfvec` ~4000) |
| `<->` / `<=>` / `<#>` | **L2** / **cosine distance** / **negatif inner product** (`<+>` L1 — daha yeni sürümlerde) |
| **HNSW** | Çok katmanlı grafik; yüksek recall/hız, yavaş ve bellek-yoğun build |
| **IVFFlat** | Kümeleme (listeler); hızlı build, daha az bellek, veri yüklendikten sonra kurulmalı |
| `hnsw.ef_search`, `ivfflat.probes` | **Sorgu zamanı** recall ↔ gecikme düğmeleri |

Cosmos DB ile karşılaştırma ve depo seçimi → bölüm 5 ve [C2.5](#22-cosmos-db-ile-vector-search-ve-change-feed), [C2.7](#24-azure-managed-redis-cache-ve-vector-index).

---

### 2. Neden?

- **Sınav:** "HNSW mi IVFFlat mı?", "index neden kullanılmıyor?" (operator class), "tenant filtresiyle sonuç eksik" (iterative scan), "bağlantılar tükendi" (pooling) kalıpları bu konudadır.
- **Gerçek işte:** Uygulama verisi zaten PostgreSQL'deyse vector için ayrı bir sisteme gerek yoktur; **transaction'da veriyle birlikte tutarlı** güncellenir, SQL filtreleri ve JOIN'ler hazırdır, yedekleme/HA/RBAC tek yerdedir.
- **Maliyet:** Vector'ler büyüktür (1536 boyut ≈ 6 KB/satır). Index belleğe sığmazsa gecikme patlar; bu yüzden boyutlandırma ve `halfvec`/boyut indirgeme kararları parayla ilgilidir.

---

### 3. Nasıl?

#### 3.1 Sunucu ve eklentiyi etkinleştirme

Flexible Server'da eklentiler **izin listesi (allowlist)** ile yönetilir: önce sunucu parametresi `azure.extensions`'a `VECTOR` eklenir, sonra **her veritabanında** `CREATE EXTENSION` çalıştırılır (kullanıcı `azure_pg_admin` üyesi olmalı).

```sql
-- 1) server parameter (portal or: az postgres flexible-server parameter set ... --name azure.extensions --value VECTOR)
SHOW azure.extensions;

-- 2) per database
CREATE EXTENSION IF NOT EXISTS vector;
SELECT extname, extversion FROM pg_extension WHERE extname = 'vector';
```

- **Kimlik doğrulama:** parola yerine **Microsoft Entra** (token tabanlı, managed identity) tercih edilir (→ C2.11). Sunucuda Entra auth etkinleştirilir ve Entra kimliği veritabanı rolü olarak eklenir; token süresi sınırlıdır, istemci tarafında yenilenmelidir.
- *(bağlam)* Azure ayrıca **`pg_diskann`** adlı DiskANN tabanlı index eklentisi sunar (`CREATE EXTENSION pg_diskann CASCADE;` — `vector`'e bağlıdır, allowlist gerekir). Büyük ölçekte HNSW'ye alternatif olarak anılır; kullanılabilirlik ve parametreler güncel dokümandan **doğrulanmalı**. Sınavın çekirdeği pgvector'ün HNSW/IVFFlat'idir.

#### 3.2 Şema ve mesafe operatörleri

```sql
CREATE TABLE documents (
    id          bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    tenant_id   int          NOT NULL,
    title       text         NOT NULL,
    content     text         NOT NULL,
    category    text,
    created_at  timestamptz  NOT NULL DEFAULT now(),
    embedding   vector(1536) NOT NULL          -- must match the embedding model's dimension
);
```

| Operatör | Mesafe | Operator class (index için) | Not |
|---|---|---|---|
| `<->` | **L2 (euclidean)** | `vector_l2_ops` | |
| `<=>` | **Cosine distance** (1 − cosine similarity) | `vector_cosine_ops` | Metin embedding'lerinde en yaygın |
| `<#>` | **Negatif inner product** | `vector_ip_ops` | **Negatiftir** — benzerlik = `-(a <#> b)`; normalize vector'lerde cosine ile aynı sıralama |
| `<+>` | L1 (taxicab) | `vector_l1_ops` | Sürüme bağlı |

- **Kritik kural:** Sorgudaki operatör, index'in **operator class**'ı ile eşleşmezse planlayıcı index'i **kullanmaz** (sessizce sequential scan). `vector_cosine_ops` ile kurulan index `<->` sorgusuna yardım etmez.
- Cosine similarity istersen: `1 - (embedding <=> $1)`. `ORDER BY` içinde **ham operatörü** bırak (ifade sarmalarsan index kullanılmaz).

#### 3.3 Index'ler: HNSW vs IVFFlat ⭐

```sql
-- HNSW: can be created on an empty table; best recall/latency; heavier build + memory
CREATE INDEX documents_embedding_hnsw ON documents
USING hnsw (embedding vector_cosine_ops)
WITH (m = 16, ef_construction = 64);

-- IVFFlat: create AFTER loading data (lists are k-means centroids learned from existing rows)
CREATE INDEX documents_embedding_ivf ON documents
USING ivfflat (embedding vector_cosine_ops)
WITH (lists = 1000);          -- rule of thumb: rows/1000 up to ~1M rows, sqrt(rows) above that

-- Query-time knobs (per session / per transaction)
SET hnsw.ef_search = 100;     -- default 40; higher = better recall, slower
SET ivfflat.probes = 32;      -- default 1; start around sqrt(lists); higher = better recall, slower
```

| Özellik | **HNSW** | **IVFFlat** |
|---|---|---|
| Yapı | Çok katmanlı proximity graph | `lists` adet küme + her kümede düz liste |
| Build parametreleri | `m` (bağlantı sayısı, varsayılan 16), `ef_construction` (varsayılan 64) | `lists` |
| Sorgu parametresi | `hnsw.ef_search` (varsayılan 40) | `ivfflat.probes` (varsayılan 1) |
| Recall/hız | **Genelde daha iyi** | Orta; `probes` düşükse recall düşük |
| Build süresi / bellek | Yavaş, **çok bellek** | **Hızlı, az bellek** |
| Boş tabloda kurulur mu? | **Evet**; veri eklendikçe güncellenir | Kurulur ama **anlamsız** (kümeler veriden öğrenilir) → **veri yükledikten sonra** kur |
| Veri değişince | Index kendini günceller | Veri dağılımı çok değişirse **yeniden kur** (REINDEX) |
| Tipik seçim | **Varsayılan öneri**, çoğu RAG | Çok büyük veri + build süresi/bellek kısıtı |

- Build hızlandırma: `SET maintenance_work_mem = '2GB';` (index bellekte kurulabilirse çok hızlanır; sunucu RAM'ini aşma), `max_parallel_maintenance_workers`.
- **Index yok = tam doğru sonuç (exact, sequential scan)**; ANN index'ler **yaklaşıktır** (recall < %100). Küçük tablolarda (birkaç bin satır) index gerekmez.
- Recall'ı ölç: aynı sorguyu index'li (yaklaşık) ve index'siz (tam) çalıştırıp top-k kesişimini hesapla (3.7 lab).

#### 3.4 Maliyet ve boyut

| Konu | Hesap / karar |
|---|---|
| Satır boyutu | `vector(1536)` ≈ 4 B × 1536 + başlık ≈ **6 KB**; 1 milyon satır ≈ **6 GB** yalnızca vector |
| `halfvec(n)` | 2 B/boyut → **yarı** depolama ve index; hafif doğruluk kaybı; index'lenebilir boyut üst sınırı daha yüksek |
| Boyut indirgeme | Modelin `dimensions` parametresiyle (ör. 3072 → 1024) veya PCA; depolama ve gecikme düşer |
| Index belleğe sığma | HNSW index'i `shared_buffers` + OS cache'e sığarsa hızlıdır; sığmazsa **disk IO'ya düşüp gecikme patlar** → daha fazla RAM/compute, `halfvec`, boyut indirgeme |
| Boyutlandırma | Compute (vCore/RAM), storage ve **IOPS** birlikte seçilir; Burstable SKU yalnızca dev/test |

```sql
-- Index on a half-precision cast (expression index) — cast must also appear in the query
CREATE INDEX documents_embedding_half ON documents
USING hnsw ((embedding::halfvec(1536)) halfvec_cosine_ops);

SELECT id FROM documents
ORDER BY embedding::halfvec(1536) <=> $1::halfvec(1536) LIMIT 5;
```

#### 3.5 RAG + metadata filtresi ve iterative scan

```sql
-- Classic RAG retrieval: tenant isolation + top-k
SELECT id, title, 1 - (embedding <=> $1) AS similarity
FROM documents
WHERE tenant_id = $2 AND category = 'policy'
ORDER BY embedding <=> $1
LIMIT 5;
```

**Sorun — filtre ANN'den sonra uygulanır:** HNSW index önce `ef_search` (varsayılan 40) aday döndürür, `WHERE` bunları **sonradan** eler. Filtre seçiciyse (ör. kiracı verinin %1'i) 40 adaydan birkaçı kalır → **LIMIT 5'ten az veya hiç sonuç, recall düşüklüğü** ("over-filtering").

| Çözüm | Ne? |
|---|---|
| **Iterative index scan** (pgvector 0.8.0+) | `SET hnsw.iterative_scan = relaxed_order;` (veya `strict_order`) — yeterli sonuç çıkana kadar index taramaya devam eder; üst sınır `hnsw.max_scan_tuples`. IVFFlat için `ivfflat.iterative_scan`, `ivfflat.max_probes` |
| `ef_search` / `probes` artır | Aday sayısını büyüt |
| **Partial index** | Az sayıda sabit kategori için: `CREATE INDEX ... WHERE category = 'policy'` |
| **Partitioning** | Büyük kiracılar için `PARTITION BY LIST (tenant_id)` — her partition kendi index'ine sahip; kiracı filtresi tek partition'a iner |
| B-tree index (`tenant_id`) | Filtre çok seçiciyse planlayıcı önce B-tree ile daraltıp **exact** sıralama yapabilir |

`relaxed_order` sonuçları hafif sırasız olabilir (recall karşılığı); sıra gerekiyorsa `strict_order` veya sonucu üstte yeniden sırala. Sürüm/davranış için sunucudaki pgvector sürümünü doğrula (`extversion`).

#### 3.6 Bağlantı optimizasyonu ⭐

PostgreSQL bağlantısı **süreç** (process) tabanlıdır: pahalıdır ve `max_connections` sınırlıdır (SKU'ya bağlı). Kısa ömürlü bağlantı açmak (her istekte `new NpgsqlConnection` + TLS + auth) hem gecikme hem bağlantı tükenmesi demektir.

| Katman | Ne yapar? |
|---|---|
| **İstemci havuzu** (Npgsql / psycopg_pool) | Uygulama içinde bağlantıları yeniden kullanır; `NpgsqlDataSource` **singleton** olmalı |
| **PgBouncer** (Flexible Server'da yerleşik, **port 6432**) | Sunucu önünde havuz; binlerce istemci bağlantısını az sayıda sunucu bağlantısına çoklar. Sunucu parametresi `pgbouncer.enabled` ile etkinleştirilir (varsayılan durumunu sunucunda doğrula) |
| Prepared statement | Npgsql otomatik hazırlama (`Max Auto Prepare`); PgBouncer **transaction pooling** modunda session-düzeyi durum (`SET`, advisory lock, bazı prepared-statement kullanımları) sorun çıkarabilir — `SET LOCAL` ve transaction içinde kullan, sürüm/ayarları doğrula |

```csharp
// dotnet add package Npgsql ; dotnet add package Pgvector ; dotnet add package Azure.Identity
using Azure.Core; using Azure.Identity; using Npgsql; using Pgvector; using Pgvector.Npgsql;

var csb = new NpgsqlConnectionStringBuilder
{
    Host = "mypg.postgres.database.azure.com",
    Port = 6432,                              // PgBouncer; 5432 = direct
    Database = "ragdb",
    Username = "my-managed-identity-name",    // Entra principal, no password
    SslMode = SslMode.Require,
    MaxPoolSize = 20,                         // per app instance! instances x MaxPoolSize must stay below server limit
    MinPoolSize = 2,
    ConnectionIdleLifetime = 60,
    Multiplexing = false,
};

var builder = new NpgsqlDataSourceBuilder(csb.ConnectionString);
builder.UseVector();                           // maps Pgvector.Vector <-> vector
var cred = new DefaultAzureCredential();
builder.UsePeriodicPasswordProvider(           // refresh the Entra access token before it expires
    async (_, ct) => (await cred.GetTokenAsync(
        new TokenRequestContext(["https://ossrdbms-aad.database.windows.net/.default"]), ct)).Token,
    successRefreshInterval: TimeSpan.FromMinutes(55), failureRefreshInterval: TimeSpan.FromSeconds(5));

NpgsqlDataSource dataSource = builder.Build();            // register as a SINGLETON (DI)

// RAG query: SET LOCAL inside a transaction is safe even with transaction pooling
await using var conn = await dataSource.OpenConnectionAsync();
await using var tx = await conn.BeginTransactionAsync();
await using (var set = new NpgsqlCommand("SET LOCAL hnsw.iterative_scan = relaxed_order", conn, tx))
    await set.ExecuteNonQueryAsync();

await using var cmd = new NpgsqlCommand("""
    SELECT id, title, embedding <=> $1 AS distance
    FROM documents WHERE tenant_id = $2
    ORDER BY embedding <=> $1 LIMIT $3
    """, conn, tx);
cmd.Parameters.AddWithValue(new Vector(queryEmbedding));   // float[]
cmd.Parameters.AddWithValue(7);
cmd.Parameters.AddWithValue(5);
await using var reader = await cmd.ExecuteReaderAsync();
while (await reader.ReadAsync()) Console.WriteLine($"{reader.GetInt64(0)} {reader.GetString(1)} {reader.GetDouble(2):F4}");
await tx.CommitAsync();
```

**Python eşdeğeri** (`pip install "psycopg[binary,pool]" pgvector numpy`):

```python
import numpy as np
from psycopg_pool import ConnectionPool
from pgvector.psycopg import register_vector

pool = ConnectionPool(conninfo=CONN, min_size=2, max_size=20,
                      configure=register_vector)            # one pool per process

with pool.connection() as conn:
    with conn.transaction():
        conn.execute("SET LOCAL hnsw.iterative_scan = relaxed_order")
        rows = conn.execute(
            "SELECT id, title, embedding <=> %s AS distance FROM documents "
            "WHERE tenant_id = %s ORDER BY embedding <=> %s LIMIT %s",
            (np.array(q), 7, np.array(q), 5)).fetchall()
```

#### 3.7 Lab — HNSW ve IVFFlat karşılaştırması

Maliyet: **Burstable** küçük bir sunucu saatlik kuruşlardır; bütçe alarmı kur (→ C1.11), iş bitince sil. Alternatif: yerelde `pgvector/pgvector` Docker imajı — SQL ve sorgular aynıdır (C2'de compose dosyası eklenmez; `docker run` ile tek seferlik çalıştır).

```bash
RG=rg-ai200-c26
PG=pgc26-$RANDOM
az group create --name $RG --location westeurope

az postgres flexible-server create --resource-group $RG --name $PG --location westeurope \
  --tier Burstable --sku-name Standard_B1ms --storage-size 32 --version 16 \
  --admin-user pgadmin --admin-password "$PGPASS" --public-access 0.0.0.0     # lab only: restrict to your IP in real life
az postgres flexible-server parameter set --resource-group $RG --server-name $PG \
  --name azure.extensions --value VECTOR
export PGHOST=$PG.postgres.database.azure.com PGUSER=pgadmin PGDATABASE=postgres PGSSLMODE=require
psql -c "CREATE EXTENSION IF NOT EXISTS vector;"
```

```sql
-- 100k rows with random 64-dim vectors (correlated subquery => a different vector per row)
CREATE TABLE documents (id int PRIMARY KEY, tenant_id int, embedding vector(64));
INSERT INTO documents
SELECT g, g % 50,
       ARRAY(SELECT random()::real FROM generate_series(1, 64) WHERE g IS NOT NULL)::vector(64)
FROM generate_series(1, 100000) AS g;
ANALYZE documents;

-- 0) exact baseline (no vector index yet)
EXPLAIN (ANALYZE, BUFFERS)
SELECT id FROM documents ORDER BY embedding <=> (SELECT embedding FROM documents WHERE id = 1) LIMIT 10;   -- Seq Scan + Sort

-- 1) HNSW
CREATE INDEX docs_hnsw ON documents USING hnsw (embedding vector_cosine_ops) WITH (m = 16, ef_construction = 64);
-- 2) look at plan: Index Scan using docs_hnsw
EXPLAIN (ANALYZE) SELECT id FROM documents ORDER BY embedding <=> (SELECT embedding FROM documents WHERE id = 1) LIMIT 10;
-- 3) Wrong operator => index NOT used (L2 operator vs cosine ops)
EXPLAIN SELECT id FROM documents ORDER BY embedding <-> (SELECT embedding FROM documents WHERE id = 1) LIMIT 10;

-- 4) IVFFlat for comparison
DROP INDEX docs_hnsw;
CREATE INDEX docs_ivf ON documents USING ivfflat (embedding vector_cosine_ops) WITH (lists = 100);
SET ivfflat.probes = 1;
EXPLAIN (ANALYZE) SELECT id FROM documents ORDER BY embedding <=> (SELECT embedding FROM documents WHERE id = 1) LIMIT 10;   -- fast, lower recall
SET ivfflat.probes = 10;
EXPLAIN (ANALYZE) SELECT id FROM documents ORDER BY embedding <=> (SELECT embedding FROM documents WHERE id = 1) LIMIT 10;   -- slower, higher recall

-- 5) Filtered query: tenant_id = 7 returns <10 rows under HNSW ef_search=40 -> turn on iterative scan
SET hnsw.iterative_scan = relaxed_order;
```

Python ile **recall ölçümü** (exact vs ANN):

```python
def top_ids(conn, q, k=10, exact=False):
    with conn.transaction():
        if exact:
            conn.execute("SET LOCAL enable_indexscan = off")        # force sequential scan = exact
        return {r[0] for r in conn.execute(
            "SELECT id FROM documents ORDER BY embedding <=> %s LIMIT %s", (q, k))}

recalls = []
for q in sample_queries:                                           # e.g. 100 random vectors
    recalls.append(len(top_ids(conn, q) & top_ids(conn, q, exact=True)) / 10)
print(f"recall@10 = {sum(recalls)/len(recalls):.3f}")              # tune ef_search / probes and re-measure
```

**Bilerek bozuk senaryolar:** (a) **boş tabloda IVFFlat** kur, sonra veri yükle → düşük kalite/uyarı; (b) `<->` ile `vector_cosine_ops` index'i → index kullanılmaz; (c) uygulamada her istekte `new NpgsqlConnection` + yük testi → `too many connections` / `remaining connection slots are reserved`; Npgsql `DataSource` singleton + PgBouncer 6432 ile düzelt.

```bash
# Cleanup
az group delete --name $RG --yes --no-wait
```

---

### 4. Gerçek dünya senaryosu

Bir SaaS firması çok kiracılı bilgi tabanı asistanı yapıyor (500 kiracı, toplam 4 milyon parça, 1536 boyut):

| İhtiyaç | Karar | Neden |
|---|---|---|
| Vektörler faturalama verisiyle aynı transaction'da güncellenmeli | **PostgreSQL + pgvector** | ACID + JOIN; ayrı vector DB senkronu yok |
| 4M × 6 KB ≈ 24 GB ham vector, index belleğe sığmıyor | `halfvec` index veya boyut 1024'e indirgeme + daha fazla RAM | Index'in sıcak kalması gecikmeyi belirler |
| Kiracı başına sonuç | `WHERE tenant_id = $1` + `hnsw.iterative_scan = relaxed_order` | Over-filtering'i önler |
| Çok büyük 3 kiracı | `PARTITION BY LIST (tenant_id)` + partition başına HNSW | Kiracı filtresi tek index'e iner |
| Gece batch yeniden embedding | IVFFlat değil **HNSW** (yazma sırasında self-maintaining); build için `maintenance_work_mem` artır | Veri dağılımı sürekli değişiyor |
| 200 pod, her biri pool açıyor | **PgBouncer 6432** + pod başına küçük `MaxPoolSize` | `max_connections` tükenmesin |
| Parola yönetimi istemiyoruz | Entra + managed identity, token yenileme | Gizli bilgi yok |

---

### 5. Sınav perspektifi

| Senaryodaki ifade | Cevap |
|---|---|
| "En iyi recall/hız, boş tabloda da kurulabilen" | **HNSW** |
| "Hızlı build, düşük bellek, veri zaten yüklendi" | **IVFFlat** (`lists`, `probes`) |
| "Cosine benzerlik, index kullanılsın" | `vector_cosine_ops` + `<=>` |
| "Index var ama EXPLAIN'de Seq Scan" | Operatör ↔ operator class uyuşmazlığı, `ORDER BY`'da ifade sarmalama, ya da küçük tablo/seçicilik |
| "Recall'ı artır (sorgu zamanı)" | `hnsw.ef_search` ↑ / `ivfflat.probes` ↑ (gecikme artar) |
| "Tenant filtresiyle eksik sonuç" | Iterative scan (`hnsw.iterative_scan`), `ef_search` ↑, partial index/partition |
| "Eklentiyi etkinleştir" | `azure.extensions` allowlist → `CREATE EXTENSION vector` (her DB'de) |
| "Çok sayıda kısa ömürlü bağlantı, 'too many connections'" | Connection pooling: Npgsql pool (singleton data source) + **PgBouncer (6432)** |
| "Parolasız bağlantı" | Microsoft Entra auth + token yenileme |
| "Depolamayı yarıya indir" | `halfvec` / daha düşük boyut |
| "ACID + vector + ilişkisel sorgular" | PostgreSQL pgvector; "global dağıtım + esnek şema + RU" → Cosmos; "milisaniye, geçici" → Redis |

**Depo seçimi (üç konu):**

| Gereksinim | PostgreSQL pgvector | Cosmos DB (→ C2.5) | Managed Redis (→ C2.7) |
|---|---|---|---|
| İlişkisel veri + JOIN + transaction | **En iyi** | Sınırlı (tek partition) | Yok |
| Çok bölgeli yazma, otomatik ölçek, esnek şema | Sınırlı | **En iyi** | Active geo-replication (farklı model) |
| En düşük gecikme, önbellek/geçici index | Orta | Orta | **En iyi** |
| Kalıcı kaynak veri (system of record) | Evet | Evet | **Hayır** (önbellek) |
| Vector index türleri | HNSW, IVFFlat (+pg_diskann) | flat, quantizedFlat, diskANN | HNSW, FLAT |

**Tuzaklar:**
- IVFFlat'i **boş tabloda** kurmak; HNSW'nin böyle bir kısıtı yoktur.
- `<#>` negatif inner product döndürür.
- `SET` ile yapılan parametre yalnızca o oturum/transaction içindir; PgBouncer transaction modunda `SET LOCAL` kullan.
- Pool boyutu: `uygulama örneği × MaxPoolSize ≤ sunucu max_connections` (yedek bağlantılar düşülerek).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Embedding boyutu ≠ kolon boyutu (`vector(1536)` ← 3072) | Modelin `dimensions`'ı kolonla eşleşmeli; model değişirse kolon + index yeniden |
| Cosine ops ile kurup `<->` ile sorgulamak | Operatör ↔ operator class eşleşmeli |
| `ORDER BY 1 - (embedding <=> $1) DESC` | Ham `ORDER BY embedding <=> $1` yaz; benzerliği SELECT'te hesapla |
| IVFFlat'i veri yüklemeden kurmak | Önce veri, sonra index; dağılım değişince REINDEX |
| `ef_search`/`probes` hiç ayarlanmamış (recall ölçülmemiş) | Recall@k ölç, ayarla |
| Her istekte yeni bağlantı | Singleton `NpgsqlDataSource` + PgBouncer |
| Pool'u büyük tutup çok pod çalıştırmak | Toplam bağlantı sunucu sınırını aşar |
| Filtreli aramada tek tek `LIMIT 5` ve boş sonuç | Iterative scan / partition |
| Index'i bellek hesabı yapmadan büyütmek | Index boyutu ≈ RAM; `pg_relation_size('idx')` ile izle |
| Entra token'ı süresi dolunca yenilememek | `UsePeriodicPasswordProvider` / token callback |
| `azure.extensions` eklemeden `CREATE EXTENSION` | "extension is not allow-listed" hatası |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| HNSW yerine IVFFlat | Hızlı build, az bellek | Daha düşük recall, veri sonrası kurulum, yeniden kurulum |
| `ef_search` ↑ | Recall ↑ | Gecikme ↑ |
| `m` / `ef_construction` ↑ | Daha iyi graf kalitesi | Build süresi, index boyutu ↑ |
| `halfvec` / daha az boyut | Depolama + bellek yarıya | Küçük doğruluk kaybı |
| Iterative scan | Filtreli aramada eksik sonuç yok | Daha çok tarama → gecikme; `relaxed_order` sırası bozabilir |
| Tek tablo + `tenant_id` filtresi | Basit | Büyük kiracıda over-filtering |
| Partition per tenant | Kiracı izolasyonu, küçük index'ler | Operasyonel karmaşıklık, çok partition planlama maliyeti |
| PgBouncer | Binlerce istemci, az sunucu bağlantısı | Ek hop; transaction pooling'de session durumu kısıtı |
| Vector'ü PostgreSQL'de tutmak | Tek sistem, ACID | Çok büyük/ham ANN iş yüklerinde özel vector DB kadar ölçeklenmeyebilir |

---

### 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [How to use pgvector on Azure Database for PostgreSQL](https://learn.microsoft.com/en-us/azure/postgresql/extensions/how-to-use-pgvector) · [Optimize performance when using pgvector](https://learn.microsoft.com/en-us/azure/postgresql/extensions/how-to-optimize-performance-pgvector)
- [How to allowlist and create extensions](https://learn.microsoft.com/en-us/azure/postgresql/extensions/how-to-create-extensions) · [DiskANN extension (pg_diskann)](https://learn.microsoft.com/en-us/azure/postgresql/extensions/how-to-use-pgdiskann)
- [PgBouncer in Flexible Server](https://learn.microsoft.com/en-us/azure/postgresql/flexible-server/concepts-pgbouncer) · [Microsoft Entra authentication](https://learn.microsoft.com/en-us/azure/postgresql/flexible-server/security-entra-concepts)
- [pgvector (GitHub README)](https://github.com/pgvector/pgvector) · [Npgsql + pgvector](https://github.com/pgvector/pgvector-dotnet) · [pgvector-python](https://github.com/pgvector/pgvector-python)
- Önceki: [C2.5 Cosmos DB Vector ve Change Feed](#22-cosmos-db-ile-vector-search-ve-change-feed) · Sonraki: [C2.7 Azure Managed Redis](#24-azure-managed-redis-cache-ve-vector-index)


### 📝 Bölüm 2.3 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir ekip Azure Database for PostgreSQL Flexible Server'da `CREATE EXTENSION vector;` çalıştırıyor ve `extension "vector" is not allow-listed` hatası alıyor. İlk yapılması gereken nedir?

- A) Sunucuyu Cosmos DB'ye taşımak
- B) `azure.extensions` sunucu parametresine `VECTOR` eklemek
- C) `shared_buffers`'ı artırmak
- D) Veritabanını yeniden oluşturmak

<details><summary>Cevap</summary>

**B.** Flexible Server'da eklentiler önce izin listesine (`azure.extensions`) eklenir; sonra her veritabanında `CREATE EXTENSION` çalıştırılır. Bellek ayarı veya veritabanını yeniden oluşturmak hatayı çözmez.
</details>

#### Soru 2: SQL tamamlama
Metin embedding'leri için **cosine distance** kullanan bir HNSW index oluşturun. Boşluğu doldurun:

```sql
CREATE INDEX docs_hnsw ON documents
USING hnsw (embedding __________);
```

- A) `vector_l2_ops`
- B) `vector_ip_ops`
- C) `vector_cosine_ops`
- D) `cosine_ops`

<details><summary>Cevap</summary>

**C.** Cosine için operator class `vector_cosine_ops`'tur (L2 → `vector_l2_ops`, inner product → `vector_ip_ops`). `cosine_ops` pgvector'de yoktur.
</details>

#### Soru 3: Tek seçim
`embedding` kolonunda `vector_cosine_ops` ile HNSW index var. Aşağıdaki sorgunun planında **Seq Scan** görülüyor:

```sql
SELECT id FROM documents ORDER BY embedding <-> $1 LIMIT 10;
```

En olası neden nedir?

- A) HNSW index yalnızca `LIMIT 100` ve üzerinde kullanılır
- B) Sorgudaki operatör (`<->`, L2) index'in operator class'ıyla (cosine) eşleşmiyor
- C) `ORDER BY` HNSW ile kullanılamaz
- D) Tablo çok büyük olduğu için index kullanılamaz

<details><summary>Cevap</summary>

**B.** Planlayıcı, yalnızca sorgudaki mesafe operatörü index'in operator class'ı ile eşleşirse index'i kullanır. Cosine index'i için `<=>` yazılmalıdır.
</details>

#### Soru 4: Eşleştirme
Operatörleri mesafe türleriyle eşleştirin: `<->` · `<=>` · `<#>`

1. Cosine distance
2. Negatif inner product
3. L2 (euclidean) mesafesi

<details><summary>Cevap</summary>

**`<->` → 3 · `<=>` → 1 · `<#>` → 2.** `<#>` *negatif* inner product döndürür (benzerlik = `-(a <#> b)`).
</details>

#### Soru 5: Tek seçim
Bir ekip 5 milyon satırlık tabloyu yükledikten sonra, **hızlı build** ve **düşük bellek kullanımı** isteyen bir ANN index arıyor; recall'dan biraz fedakârlık edilebilir ve sorgu zamanında ayar yapılabiliyor. Hangi seçim uygundur?

- A) IVFFlat (veri yüklendikten sonra, `lists` uygun seçilerek)
- B) HNSW, `m = 64`, `ef_construction = 400`
- C) B-tree index
- D) GIN index

<details><summary>Cevap</summary>

**A.** IVFFlat hızlı kurulur ve daha az bellek kullanır; recall `ivfflat.probes` ile ayarlanır. HNSW daha iyi recall verir ama build'i yavaş ve bellek-yoğundur. B-tree/GIN vector benzerliği için uygun değildir.
</details>

#### Soru 6: Çoklu seçim
IVFFlat index'i hakkında hangi iki ifade doğrudur?

- A) Kümeler (lists) mevcut veriden öğrenildiği için index veri yüklendikten sonra kurulmalıdır.
- B) Sorgu zamanında recall'ı `ivfflat.probes` ile artırabilirsiniz.
- C) Boş tabloda kurulması en iyi sonucu verir.
- D) `hnsw.ef_search` IVFFlat'i de etkiler.

<details><summary>Cevap</summary>

**A ve B.** Boş tabloda kurulan IVFFlat anlamsız kümeler üretir (C yanlış). `ef_search` HNSW'ye aittir (D yanlış).
</details>

#### Soru 7: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) HNSW index boş bir tabloda oluşturulabilir ve sonradan eklenen satırlarla kendini günceller. | ☐ | ☐ |
| b) `hnsw.ef_search` değerini artırmak recall'ı genelde artırır, gecikmeyi de artırır. | ☐ | ☐ |
| c) ANN index'ler (HNSW/IVFFlat) her zaman %100 doğru (exact) sonuç döner. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** ANN index'ler yaklaşıktır; tam sonuç için index'siz (sequential scan) çalıştırılır.
</details>

#### Soru 8: Senaryo
Çok kiracılı bir RAG uygulamasında şu sorgu çalışıyor:

```sql
SELECT id FROM documents WHERE tenant_id = 42 ORDER BY embedding <=> $1 LIMIT 5;
```

Kiracı 42 tablonun yalnızca %0,5'i. HNSW index (`ef_search = 40`) kullanılırken sorgu sıklıkla 5'ten **az** satır döndürüyor. En uygun çözüm hangisidir?

- A) `hnsw.iterative_scan = relaxed_order` ayarlamak (pgvector 0.8.0+)
- B) `LIMIT`'i 1'e düşürmek
- C) HNSW yerine `flat` index kullanmak
- D) Embedding boyutunu artırmak

<details><summary>Cevap</summary>

**A.** HNSW önce ~`ef_search` aday döndürür, `WHERE` bunları sonradan eler (over-filtering). Iterative scan, yeterli sonuç çıkana kadar index taramaya devam eder. Alternatifler: `ef_search` artırmak, kiracıya göre partitioning/partial index. `LIMIT` düşürmek veya boyut artırmak sorunu çözmez; pgvector'de `flat` index türü yoktur.
</details>

#### Soru 9: Kod tamamlama (C#)
Npgsql ile `Pgvector` paketini kullanarak vector parametresi gönderin. Boşluğu doldurun:

```csharp
var builder = new NpgsqlDataSourceBuilder(connectionString);
builder.__________();                         // register vector type mapping
NpgsqlDataSource dataSource = builder.Build();

cmd.Parameters.AddWithValue(new Vector(queryEmbedding));
```

- A) `UseVector`
- B) `EnablePgvector`
- C) `MapVector`
- D) `AddVectorType`

<details><summary>Cevap</summary>

**A.** `Pgvector.Npgsql` paketi `NpgsqlDataSourceBuilder.UseVector()` uzantısını sağlar; `Vector` tipi `float[]` ile oluşturulur.
</details>

#### Soru 10: Senaryo
Bir ASP.NET Core API her istekte `new NpgsqlConnection(...)` açıp kapatıyor ve 200 pod ile çalışıyor. Yük altında `remaining connection slots are reserved` hatası alınıyor. Hangi iki değişiklik **birlikte** en doğrudur?

- A) `NpgsqlDataSource`'u singleton yapıp pod başına `MaxPoolSize`'ı küçük tutmak
- B) Flexible Server'ın yerleşik **PgBouncer**'ını etkinleştirip uygulamayı port **6432**'ye yönlendirmek
- C) `max_connections`'ı sınırsız yapmak
- D) Her istekte bağlantıyı açık bırakıp hiç kapatmamak

<details><summary>Cevap</summary>

**A ve B.** Havuzlama ve sunucu önü bağlantı çoklayıcı bağlantı sayısını kontrol eder. `max_connections` bellek ve process maliyetiyle sınırlıdır; bağlantıyı kapatmamak sızıntıdır.
</details>

#### Soru 11: Sıralama
Sıfırdan bir RAG vector deposu kurulumunu doğru **sıraya** koyun: (1) HNSW/IVFFlat index oluştur (2) `azure.extensions`'a `VECTOR` ekle (3) `CREATE EXTENSION vector` (4) tabloyu `vector(n)` kolonuyla oluştur ve veriyi yükle

<details><summary>Cevap</summary>

**2 → 3 → 4 → 1.** (HNSW için index'i erken kurmak da mümkündür; IVFFlat için index mutlaka veri yüklendikten sonra kurulur — bu yüzden güvenli genel sıra index'in sonda olmasıdır.)
</details>

#### Soru 12: CLI tamamlama
Aşağıdaki komut izin listesine `VECTOR` eklemelidir. Boşluğu doldurun:

```bash
az postgres flexible-server parameter set --resource-group $RG --server-name $PG \
  --name __________ --value VECTOR
```

- A) `shared_preload_libraries`
- B) `azure.extensions`
- C) `pgvector.enabled`
- D) `extensions.allowed`

<details><summary>Cevap</summary>

**B.** `azure.extensions` izin listesidir. `shared_preload_libraries` bazı eklentiler (örn. `pg_cron`) için ek olarak gerekir ama pgvector için allowlist yeterlidir; diğer adlar yoktur.
</details>

#### Soru 13: Tek seçim
1536 boyutlu `vector` kolonunda 2 milyon satır var; HNSW index'i RAM'e sığmıyor ve sorgu gecikmeleri artıyor. Hangi seçenek **depolama ve bellek** ihtiyacını doğrudan azaltır?

- A) `halfvec` kullanmak veya embedding boyutunu düşürmek (modelin `dimensions` parametresi)
- B) `ef_search`'i artırmak
- C) `probes` değerini artırmak
- D) Daha fazla `tenant_id` filtresi eklemek

<details><summary>Cevap</summary>

**A.** `halfvec` (2 bayt/boyut) vector ve index boyutunu yaklaşık yarıya indirir; boyut düşürmek de benzer etki yapar. `ef_search`/`probes` recall'ı değiştirir, boyutu değil.
</details>

#### Soru 14: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) PgBouncer transaction pooling modunda oturum düzeyi `SET` yerine transaction içinde `SET LOCAL` kullanmak daha güvenlidir. | ☐ | ☐ |
| b) Uygulama örnek sayısı × `MaxPoolSize`, sunucu `max_connections` sınırını aşmamalıdır. | ☐ | ☐ |
| c) Microsoft Entra ile bağlanıldığında token süresi dolsa bile açık havuz bağlantıları otomatik olarak yeni kimlikle yenilenir; istemci kodu bir şey yapmaz. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Token (yaklaşık bir saatlik) süresi dolmadan istemcide yenilenmelidir (örn. Npgsql `UsePeriodicPasswordProvider`); yeni bağlantılar yeni token'la açılır.
</details>

#### Soru 15: Kod tamamlama (Python)
Psycopg ile `pgvector` türünü kaydedin. Boşluğu doldurun:

```python
from psycopg_pool import ConnectionPool
from pgvector.psycopg import __________

pool = ConnectionPool(conninfo=CONN, min_size=2, max_size=20, configure=__________)
```

- A) `register_vector`
- B) `enable_vector`
- C) `VectorAdapter`
- D) `load_vector`

<details><summary>Cevap</summary>

**A.** `pgvector.psycopg.register_vector`, her bağlantıda `vector` tipini numpy dizileriyle eşler (`configure` callback'inde çağrılır).
</details>

#### Soru 16: Vaka (case study)
**Senaryo:** *Northwind Legal*, sözleşme parçalarını PostgreSQL Flexible Server'da saklıyor: `contracts(id, tenant_id, clause_text, embedding vector(3072))`, 8 milyon satır. Sorgular her zaman `tenant_id` filtreli top-5 benzer madde arar. Gözlemler: (1) ilk kurulan IVFFlat index'i **boş tabloda** oluşturulmuştu ve recall düşük; (2) kiracıların çoğu küçük, 3 kiracı çok büyük; (3) 3072 boyutlu vector'ler depolamayı ve bellek ihtiyacını şişiriyor; (4) 120 uygulama örneği doğrudan 5432 portuna bağlanıp ayrı ayrı açık bağlantı tutuyor ve sunucu arada `too many connections` veriyor.

**Soru A.** Index stratejisini düzeltin. **Soru B.** Filtreli aramada sonuç eksikliği riskini nasıl yönetirsiniz? **Soru C.** Boyut/bellek ve bağlantı sorunlarını nasıl çözersiniz?

<details><summary>Cevap</summary>

**A.** IVFFlat boş tabloda kümeleri veriden öğrenemez; ya veri yüklendikten sonra yeniden kurulmalı ya da (tercihen, sürekli güncellenen veri için) **HNSW** (`vector_cosine_ops`, sorguda `<=>`) kullanılmalı. HNSW boş tabloda kurulabilir ve veriyle birlikte güncellenir; build için `maintenance_work_mem` artırılır. 3072 boyut doğrudan index'lenebilir vector sınırını aşabilir; bu yüzden **`halfvec` ifade index'i** veya boyut düşürme gerekir (bkz. C).

**B.** `tenant_id` filtresi HNSW adaylarını sonradan eler (over-filtering). Çözüm: `hnsw.iterative_scan = relaxed_order` (pgvector 0.8.0+; sıra önemliyse `strict_order`), gerekirse `ef_search` artırma; **3 büyük kiracı için `PARTITION BY LIST (tenant_id)`** (her partition kendi HNSW'sine sahip, kiracı filtresi tek partition'a iner), küçük kiracılar için B-tree(`tenant_id`) ile daraltıp exact sıralama da yeterli olabilir.

**C.** Boyut/bellek: embedding modelinde `dimensions` parametresiyle (örn. 1024) boyut düşürmek ve/veya `halfvec` (≈ yarı depolama/index). Index'in belleğe sığması için compute/RAM boyutlandırılır. Bağlantı: `NpgsqlDataSource`/psycopg havuzu singleton, örnek başına küçük `MaxPoolSize`; **PgBouncer'ı** (`pgbouncer.enabled`) etkinleştirip uygulamaları **6432**'ye yönlendirmek; `SET LOCAL` kullanmak. Kimlik: parola yerine Entra + token yenileme.
</details>

---

**Puanlama:** 13-16 → hazırsın · 10-12 → README 3.2 (operatörler), 3.3 (HNSW/IVFFlat) ve 3.5 (filtre/iterative scan) bölümlerini tekrar et · < 10 → konuyu baştan çalış, 3.7 laboratuvarında `EXPLAIN` planlarını kendin karşılaştır.


---

## 2.4 Azure Managed Redis: Cache ve Vector Index

> **Sınav Alanı:** Develop AI solutions by using Azure data management services (%30-35) · **Konu Kodu:** C2.7

### 1. Ne?

**Azure Managed Redis (AMR)**, Redis Ltd. ile birlikte sunulan, **Redis Enterprise** yazılımı üzerine kurulu, bellek-içi, yönetilen Redis hizmetidir. Önceki **Azure Cache for Redis** (Basic/Standard/Premium ve Enterprise/Enterprise Flash) ürünlerinin **halefidir**. İki işte kullanılır:

| Kullanım | Ne yapar? | Anahtar kavramlar |
|---|---|---|
| **Cache** | Yavaş/pahalı kaynağın (veritabanı, API, LLM) önüne milisaniyenin altında okuma katmanı koyar | cache-aside, **TTL (expiration)**, **invalidation**, stampede önleme |
| **Vector index** | Embedding'leri saklar, **KNN benzerlik araması** ve **semantic cache** sağlar | RediSearch modülü, `FT.CREATE ... VECTOR`, HNSW / FLAT |

**Geçiş notu (tarihleri resmî duyurudan doğrula):** Azure Cache for Redis **Basic/Standard/Premium** katmanlarının emeklilik tarihi **30 Eylül 2028**; **Enterprise/Enterprise Flash** için **31 Mart 2027** olarak duyurulmuştur ve Microsoft yeni projelerde AMR'yi önerir; yeni oluşturmalar da kademeli olarak kapatılmaktadır. Sınav güncel ürün olan **Azure Managed Redis**'i sorar.

**AMR katmanları** (bellek:vCPU oranına göre):

| Katman | Bellek : vCPU | Ne için? |
|---|---|---|
| **Memory Optimized** | 8:1 | Bellek-yoğun, en yüksek throughput gerekmeyen iş yükleri |
| **Balanced** | 4:1 | Standart/çoğu iş yükü (**varsayılan başlangıç noktası**) |
| **Compute Optimized** | 2:1 | En yüksek throughput, CPU-yoğun |
| **Flash Optimized** | RAM + NVMe | Çok büyük veri setleri, seyrek erişilen kısım NVMe'de — **RediSearch/vector modülü bu katmanda desteklenmez** *(doğrula)* |

---

### 2. Neden?

- **Sınav:** "TTL ile eviction farkı?", "yazma sonrası bayat veri nasıl önlenir?", "vector için modül ne zaman seçilir?", `FT.CREATE` tamamlama, "semantic cache nedir?", "Redis'i ne için kullanmamalı?" soruları.
- **Gerçek işte:** Cache, veritabanı yükünü ve gecikmeyi büyük ölçüde düşürür; LLM çağrılarında ise **semantic cache** hem maliyeti (token) hem gecikmeyi düşürür: "iade politikası nedir?" ile "ürünü nasıl iade ederim?" aynı yanıtı paylaşabilir.
- **Doğru araç:** Redis **bellek** maliyetlidir ve (cache olarak) kaynak veri deposu değildir. Kalıcı vector verisi → Cosmos ([C2.5](#22-cosmos-db-ile-vector-search-ve-change-feed)) veya PostgreSQL ([C2.6](#23-azure-database-for-postgresql-ve-pgvector)); Redis hızlı, geçici, tekrar üretilebilir katmandır.

---

### 3. Nasıl?

#### 3.1 Sağlama: katman, modül, clustering, eviction

Üç karar **oluşturma sırasında** verilir ve sonradan değiştirilemez (veya kısıtlıdır — güncel dokümanı doğrula):

| Karar | Seçenekler | Kural |
|---|---|---|
| **Modüller** | RediSearch (arama + vector), RedisJSON, RedisBloom, RedisTimeSeries | **Oluşturma sırasında seçilir; sonradan eklenemez.** Vector için **RediSearch** şart |
| **Clustering policy** | **OSS** cluster API (en yüksek performans, cluster-aware istemci ister) · **Enterprise** (tek endpoint gibi görünür, cluster-aware olmayan istemciler çalışır) | Oluşturma sonrası değişmez. **RediSearch → Enterprise policy** gerekir |
| **Eviction policy** | `noeviction`, `volatile-lru`, `allkeys-lru`, `volatile-lfu`, `allkeys-lfu`, `volatile-ttl`, `volatile-random`, `allkeys-random` | **RediSearch → yalnızca `NoEviction`**. Geo-replication grubunda oluşturma sonrası değişmez |

- **Bağlantı:** TLS, port **10000**, ad biçimi `<ad>.<region>.redis.azure.net`. Klasik `6380` portu eski ürünündü.
- **Kimlik:** **Microsoft Entra ID** (managed identity) önerilir; access key kimlik doğrulaması yeni örneklerde varsayılan kapalıdır *(doğrula)*. Kimlik, cache üzerinde Redis kullanıcısı olarak tanımlanmalıdır.
- **Ağ:** klasik üründeki **VNet injection** ve IP güvenlik duvarı kuralları yerine **Private Endpoint** kullanılır *(güncel durumu doğrula)*.
- **Dayanıklılık:** zone redundancy/HA, **active geo-replication** ve (isteğe bağlı) veri kalıcılığı (RDB/AOF) vardır; ancak bunlar Redis'i **system of record** yapmaz — kaynak veri başka yerde olmalı.

```text
Cache-only API cache      → Balanced, OSS cluster policy, volatile-lru / allkeys-lru, modül yok
Cache + vector/semantic   → Balanced/Memory Optimized, RediSearch modülü,
                            Enterprise clustering policy, NoEviction (bellek dolarsa yazma hatası! → TTL şart)
```

#### 3.2 Cache-aside, TTL ve invalidation ⭐

**Cache-aside (lazy loading):** okuma sırasında önce cache; yoksa kaynaktan oku, cache'e **TTL ile** yaz.

```text
read(key):
  v = cache.get(key)
  if v != null  → return v                  (HIT)
  v = db.load(key)                          (MISS)
  cache.set(key, v, TTL)                    ← her zaman süre ver
  return v
write(key, v):
  db.save(key, v)
  cache.delete(key)                         ← invalidation (yazınca sil)
```

**TTL (expiration) ≠ eviction** ⭐:

| | **Expiration (TTL)** | **Eviction** |
|---|---|---|
| Tetikleyen | Anahtara verilen **süre** doldu (uygulama kararı) | **Bellek doldu** (sunucu kararı) |
| Hangi anahtar? | Süresi dolan | Eviction policy'ye göre seçilen (LRU/LFU/random/TTL'i en yakın) |
| Amaç | Veriyi **bayatlamadan** temizlemek | Sistemi **çökmeden** ayakta tutmak |
| `noeviction` iken | Çalışır | Bellek dolunca **yazma hatası (OOM)** |

**Invalidation stratejileri:**

| Strateji | Nasıl? | Artı / eksi |
|---|---|---|
| **TTL ile yaşlanma** | Sadece süre | En basit; süre boyunca bayat veri kabul edilmeli |
| **Yazınca sil** (write-invalidate) | DB yazısından sonra `DEL key` | Bayatlığı azaltır; DB yazısı başarılı + `DEL` başarısız olursa bayat kalır (TTL güvenlik ağı) |
| **Yazınca güncelle** (write-through) | Yazarken cache'i de güncelle | Okuma hep sıcak; yarış koşulu ve çift yazma karmaşıklığı |
| **Versiyonlu anahtar** | `product:42:v7` (versiyon sayacı artar) | Silme gerekmez; eski anahtarlar TTL ile kendiliğinden ölür |
| **Olay tabanlı** | Değişiklik olayı (change feed/Event Grid/mesaj) → ilgili anahtarı sil | Çok servisli sistemlerde; ek altyapı (→ C2.5, C2.9) |

**Stampede (thundering herd):** popüler anahtar süresi dolunca yüzlerce istek aynı anda DB'ye gider. Önlemler: **tek uçuş (single-flight)** kilidi (`SET lock:key token NX PX 5000`), **TTL jitter** (süre ± rastgele), erken yenileme, **HybridCache** (yerleşik stampede koruması) (→ M8.4).

```csharp
// dotnet add package StackExchange.Redis ; dotnet add package Microsoft.Azure.StackExchangeRedis
using Azure.Identity;
using Microsoft.Azure.StackExchangeRedis;
using StackExchange.Redis;

// Entra-based connection (no access key). The extension acquires and refreshes the token.
ConfigurationOptions options = ConfigurationOptions.Parse("myredis.westeurope.redis.azure.net:10000");
await options.ConfigureForAzureWithTokenCredentialAsync(new DefaultAzureCredential());
options.AbortOnConnectFail = false;
ConnectionMultiplexer mux = await ConnectionMultiplexer.ConnectAsync(options);   // SINGLETON for the app lifetime
IDatabase redis = mux.GetDatabase();

// Cache-aside with TTL (+ jitter) and write-invalidate
async Task<Product?> GetProductAsync(int id)
{
    string key = $"product:{id}";
    RedisValue cached = await redis.StringGetAsync(key);
    if (cached.HasValue) return JsonSerializer.Deserialize<Product>((string)cached!);       // HIT

    Product? p = await db.Products.FindAsync(id);                                           // MISS -> source of truth
    if (p is not null)
    {
        TimeSpan ttl = TimeSpan.FromMinutes(10) + TimeSpan.FromSeconds(Random.Shared.Next(0, 60)); // jitter
        await redis.StringSetAsync(key, JsonSerializer.Serialize(p), ttl);
    }
    return p;
}

async Task UpdateProductAsync(Product p)
{
    await db.SaveAsync(p);                           // 1) write the source of truth first
    await redis.KeyDeleteAsync($"product:{p.Id}");   // 2) then invalidate; TTL is the safety net if this fails
}
```

**ASP.NET Core ile (.NET 9/10):** `IDistributedCache` (Redis L2) + **HybridCache** (L1 bellek + L2 Redis, stampede koruması, `GetOrCreateAsync`):

```csharp
builder.Services.AddStackExchangeRedisCache(o => o.ConnectionMultiplexerFactory = () => Task.FromResult<IConnectionMultiplexer>(mux));
builder.Services.AddHybridCache(o => o.DefaultEntryOptions = new HybridCacheEntryOptions
{
    Expiration = TimeSpan.FromMinutes(10),          // L2 (Redis) TTL
    LocalCacheExpiration = TimeSpan.FromMinutes(1), // L1 (in-process) TTL
});

// usage: concurrent callers for the same key share ONE factory call (stampede protection)
Product? p = await hybridCache.GetOrCreateAsync($"product:{id}", async ct => await db.Products.FindAsync([id], ct), cancellationToken: ct);
await hybridCache.RemoveAsync($"product:{id}");     // invalidate on write
```

**Python eşdeğeri** (`pip install redis`):

```python
import json, random, redis

r = redis.Redis(host="myredis.westeurope.redis.azure.net", port=10000, ssl=True,
                password=ACCESS_KEY_OR_TOKEN, decode_responses=True)   # Entra: use the `redis-entraid` credential provider instead of a key

def get_product(pid: int):
    key = f"product:{pid}"
    if (hit := r.get(key)) is not None:
        return json.loads(hit)
    product = db_load(pid)                                   # miss -> source of truth
    r.set(key, json.dumps(product), ex=600 + random.randint(0, 60))   # TTL + jitter
    return product

def update_product(product):
    db_save(product)
    r.delete(f"product:{product['id']}")                     # invalidate
```

#### 3.3 Vector index ve KNN arama

Vector arama için cache **RediSearch** modülüyle (ve Enterprise clustering + NoEviction ile) oluşturulmalıdır. Vector'ler **HASH** (binary blob) veya **JSON** belge alanında tutulur.

```text
# 1) Create the index over HASH keys with prefix "product:"
FT.CREATE idx:products ON HASH PREFIX 1 product: SCHEMA
    name TEXT
    category TAG
    embedding VECTOR HNSW 10 TYPE FLOAT32 DIM 1536 DISTANCE_METRIC COSINE M 16 EF_CONSTRUCTION 200

# 2) Store a document (embedding = packed float32 bytes)
HSET product:1 name "Waterproof hiking boots" category footwear embedding <binary 1536 x float32>

# 3) KNN: top-5 nearest, optionally pre-filtered by a TAG
FT.SEARCH idx:products "(@category:{footwear})=>[KNN 5 @embedding $vec AS score]"
    PARAMS 2 vec <binary> SORTBY score RETURN 2 name score DIALECT 2
```

| Parça | Anlam |
|---|---|
| `VECTOR HNSW 10 ...` / `VECTOR FLAT 6 ...` | Algoritma + **öznitelik sayısı** (ad+değer çiftlerinin toplam token sayısı: `TYPE FLOAT32 DIM 1536 DISTANCE_METRIC COSINE` = 6) |
| `HNSW` | Yaklaşık (ANN), büyük veri, hızlı; `M`, `EF_CONSTRUCTION`, sorguda `EF_RUNTIME` |
| `FLAT` | Brute-force, tam doğru, küçük veri |
| `DISTANCE_METRIC` | `COSINE` / `IP` (inner product) / `L2` |
| `DIM` | Modelin çıktı boyutuyla **aynı** olmalı (`text-embedding-3-small` = 1536) |
| `=>[KNN k @field $param AS alias]` | k en yakın komşu; skor = **mesafe** (COSINE'da `1 - benzerlik`, **küçük = benzer**) |
| `DIALECT 2` | Vector sorgu söz dizimi için gerekli |
| Ön-filtre `(@category:{footwear})` | Hybrid sorgu: önce meta veriyle daralt, sonra KNN |

```csharp
using System.Runtime.InteropServices;

static byte[] ToBytes(float[] v) => MemoryMarshal.AsBytes(v.AsSpan()).ToArray();   // float32 little-endian blob

// FT.CREATE (raw command keeps it identical to the Redis docs)
await redis.ExecuteAsync("FT.CREATE", "idx:products", "ON", "HASH", "PREFIX", "1", "product:", "SCHEMA",
    "name", "TEXT", "category", "TAG",
    "embedding", "VECTOR", "HNSW", "6", "TYPE", "FLOAT32", "DIM", "1536", "DISTANCE_METRIC", "COSINE");

await redis.HashSetAsync("product:1", [
    new("name", "Waterproof hiking boots"), new("category", "footwear"), new("embedding", ToBytes(vec))]);

RedisResult res = await redis.ExecuteAsync("FT.SEARCH", "idx:products",
    "(@category:{footwear})=>[KNN 5 @embedding $vec AS score]",
    "PARAMS", "2", "vec", ToBytes(queryVec), "SORTBY", "score", "RETURN", "2", "name", "score", "DIALECT", "2");
```

```python
import numpy as np
from redis.commands.search.field import TextField, TagField, VectorField
from redis.commands.search.indexDefinition import IndexDefinition, IndexType
from redis.commands.search.query import Query

r2 = redis.Redis(host=HOST, port=10000, ssl=True, password=KEY)          # no decode_responses: we send bytes
r2.ft("idx:products").create_index(
    [TextField("name"), TagField("category"),
     VectorField("embedding", "HNSW", {"TYPE": "FLOAT32", "DIM": 1536, "DISTANCE_METRIC": "COSINE"})],
    definition=IndexDefinition(prefix=["product:"], index_type=IndexType.HASH))

r2.hset("product:1", mapping={"name": "Waterproof hiking boots", "category": "footwear",
                              "embedding": np.array(vec, dtype=np.float32).tobytes()})

q = (Query("(@category:{footwear})=>[KNN 5 @embedding $vec AS score]")
     .sort_by("score").return_fields("name", "score").dialect(2))
docs = r2.ft("idx:products").search(q, query_params={"vec": np.array(query_vec, dtype=np.float32).tobytes()}).docs
```

#### 3.4 Semantic cache

**Semantic cache**, LLM çağrısının önüne konur: sorunun **embedding'i** daha önce cevaplanmış bir sorununkine yeterince yakınsa kayıtlı cevabı döndür; değilse LLM'i çağır, sonucu (TTL ile) kaydet.

```text
answer(question):
  qv = embed(question)
  hit = FT.SEARCH idx:semcache "*=>[KNN 1 @embedding $vec AS dist]"   → (answer, dist)
  if hit and dist <= THRESHOLD (örn. 0.10 cosine mesafesi)  → return hit.answer     (SEMANTIC HIT; LLM çağrısı yok)
  a = llm(question)
  HSET semcache:<hash> question .. answer a embedding qv ; EXPIRE semcache:<hash> 86400
  return a
```

- **Eşik** (threshold) kritik bir ayardır: çok gevşek → yanlış cevap döner (*"parolamı sıfırla"* ≠ *"hesabımı sil"*); çok sıkı → hit oranı düşer. Gerçek soru seti ile ölç.
- **TTL şart:** cevaplar bayatlar; ayrıca cache doluluğunu sınırlar. **Kiracı/kullanıcıya özel** cevaplar için `tenant` TAG ile ön-filtre (veri sızıntısı riski!).
- Semantic cache **kalıcı bilgi tabanı değildir**; kaybolması yalnızca maliyet/gecikme demektir.
- Resmî AMR eğitiminde semantic cache örnekleri vardır (kaynaklara bak).

**Ne zaman Redis'te vector tutulur?**

| Durum | Karar |
|---|---|
| Düşük gecikmeli, **geçici/önbellek niteliğinde** vector (semantic cache, oturum/özellik deposu, sıcak alt küme) | **Redis** |
| Kalıcı kaynak vector verisi, filtre + JOIN + ACID | **PostgreSQL pgvector** ([C2.6](#23-azure-database-for-postgresql-ve-pgvector)) |
| Global dağıtım, esnek şema, operasyonel veriyle aynı yerde | **Cosmos DB** ([C2.5](#22-cosmos-db-ile-vector-search-ve-change-feed)) |
| Çok büyük kalıcı vector seti, maliyet hassas | Redis **değil** (bellek pahalı) — Cosmos/PostgreSQL |

#### 3.5 Lab — cache-aside, vector index ve semantic cache

Maliyet notu: AMR örnekleri **saatlik faturalanır**; en küçük Balanced katmanı bile boşta ücret doğurur — bütçe alarmı kur (→ C1.11), lab bitince hemen sil. **Ucuz yol:** komutları yerelde **Redis Stack / Redis 8** ile çalıştır (RediSearch dahil; `FT.*` komutları ve kodlar aynıdır, yalnızca bağlantı: `localhost:6379`, TLS ve Entra yok):

```bash
docker run --rm -p 6379:6379 redis/redis-stack-server:latest      # or an official Redis 8 image (search/vector built in) — verify the image tag
```

Azure'da AMR (modül oluşturma sırasında seçilir; **bayrak adları ve cluster-vs-database yerleşimi CLI sürümüne göre değişebilir — `az redisenterprise create -h` ile doğrula**, alternatif: portal/Bicep):

```bash
RG=rg-ai200-c27
REDIS=amr-c27-$RANDOM
az group create --name $RG --location westeurope

az redisenterprise create --resource-group $RG --name $REDIS --location westeurope \
  --sku Balanced_B0 \
  --modules name=RediSearch \
  --clustering-policy EnterpriseCluster \
  --eviction-policy NoEviction

az redisenterprise show --resource-group $RG --name $REDIS --query "{host:hostName, sku:sku.name}" -o table

# Entra: add your identity as a Redis user (portal: cache -> Authentication), then connect with DefaultAzureCredential (3.2)
```

Adımlar:
1. **Cache-aside (3.2):** `GET /products/{id}` ve `PUT /products/{id}` ile API yaz; TTL 60 sn; `redis-cli TTL product:1`, `redis-cli GET product:1` ile sürenin azaldığını gözle. Bilerek bozuk: yazınca **`DEL`'i kaldır** → eski fiyat TTL bitene kadar döner (bayat veri).
2. **Stampede:** TTL'i 1 sn yap, 200 eşzamanlı istek at, DB çağrı sayacını say; sonra HybridCache/`SET NX` kilidi ekleyip farkı ölç.
3. **Vector (3.3):** 40 ürünü (sahte/yerel embedder veya gerçek model, → M15.4) yükle; `FT.CREATE` ile HNSW index; `FT.SEARCH` KNN + kategori TAG ön-filtresi; `FT.INFO idx:products` ile index durumunu gör. **Bozuk:** `DIM 1536` ile 64 boyutlu vector yaz → belge indekslenmez (`FT.INFO` `hash_indexing_failures` artar); `DIALECT 2` olmadan KNN sorgusu → söz dizimi hatası.
4. **Semantic cache (3.4):** 10 soru/cevap yaz; benzer ifadeyle sor; eşik 0.05 / 0.10 / 0.25 için hit oranını ve yanlış-hit'leri tabloya yaz.

```bash
# Cleanup (AMR is billed hourly — do not skip)
az group delete --name $RG --yes --no-wait
```

---

### 4. Gerçek dünya senaryosu

Bir e-ticaret sitesi ürün API'si ve müşteri destek botu işletiyor:

| İhtiyaç | Karar | Neden |
|---|---|---|
| Ürün detay sayfası DB'yi eziyor | **Cache-aside**, TTL 10 dk (+ jitter) | Okuma ağırlıklı, nadiren değişen veri |
| Fiyat güncellenince hemen yansımalı | DB yaz → **`DEL product:{id}`** (TTL güvenlik ağı) | Bayatlığı saniyelere indirir |
| Kampanya anahtarı her 10 dk'da bir aynı anda düşüyor | Jitter + HybridCache/kilit | Stampede önleme |
| Bot LLM maliyeti yüksek, sorular tekrarlıyor | **Semantic cache** (RediSearch, eşik 0.1, TTL 24 saat) | Benzer soruya LLM'siz cevap |
| Bilgi tabanı vector'leri (kalıcı) | **Cosmos/PostgreSQL**'de; Redis yalnızca sıcak alt küme/semantic cache | Redis kaynak veri deposu değil |
| Bellek dolarsa semantic cache yazamıyor | Tüm anahtarlara TTL + boyut izleme (`NoEviction` zorunlu) | RediSearch eviction'ı desteklemez |
| Eski Azure Cache for Redis Premium emeklilik | **AMR'ye geçiş** planı | Duyurulan emeklilik tarihleri |

---

### 5. Sınav perspektifi

| Senaryodaki ifade | Cevap |
|---|---|
| "Anahtar belirli süre sonra otomatik silinsin" | **TTL / expiration** (`SET key v EX 600`, `EXPIRE`) |
| "Bellek dolunca hangi anahtarlar silinsin" | **Eviction policy** (`allkeys-lru`, `volatile-lru`...) |
| "Vector arama kullanacağız" | **RediSearch** modülü (oluşturma sırasında), **Enterprise** clustering, **NoEviction**; Flash Optimized değil |
| "Modülü cache oluşturduktan sonra ekle" | **Mümkün değil** — yeniden oluştur |
| "Yazma sonrası bayat veri" | Yazınca invalidate (`DEL`) / versiyonlu anahtar / olay tabanlı + TTL |
| "Popüler anahtar süresi dolunca DB çöküyor" | Stampede: kilit/single-flight, jitter, HybridCache |
| "Benzer sorulara LLM'i tekrar çağırma" | **Semantic cache** |
| "Parolasız bağlantı" | Entra ID + `ConfigureForAzureWithTokenCredentialAsync` (managed identity) |
| "FT.CREATE'te vector alanı" | `VECTOR HNSW|FLAT <sayı> TYPE FLOAT32 DIM n DISTANCE_METRIC COSINE` |
| "En düşük gecikme, geçici vector indeksi" | Redis; **kalıcı** kaynak → Cosmos/PostgreSQL |
| "Azure Cache for Redis yerine yeni ürün" | **Azure Managed Redis** |
| "Tek `ConnectionMultiplexer`" | Singleton; her istekte yeni bağlantı = hata |

**Tuzaklar:**
- **TTL ≠ eviction**; `noeviction` iken bellek dolarsa yazmalar hata verir.
- Cache **kaynak veri deposu değildir**; "Redis'e yaz, DB'ye sonra" (write-behind) veri kaybı riski taşır.
- `KNN` skoru bir **mesafe**dir (COSINE'da küçük = benzer); eşik karşılaştırmasını ters yapma.
- Vector `DIM` modelle aynı olmalı; yanlış boyutlu belge sessizce indekslenmez.
- Anahtara TTL vermeyi unutmak: bellek sızıntısı + (eviction politikasına göre) kontrolsüz silme.
- Kiracılar arası semantic cache paylaşımı: veri sızıntısı.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Her istekte `ConnectionMultiplexer.Connect` | Uygulama başına tek singleton |
| TTL'siz `SET` | Her yazımda süre ver; jitter ekle |
| Önce `DEL` sonra DB yazmak | Önce kaynak yaz, sonra invalidate (yarış penceresi küçülür); TTL güvenlik ağı |
| Cache hatasında isteği başarısız bırakmak | Cache bir optimizasyondur: hata olursa kaynağa düş (graceful degradation) |
| `KEYS *` ile üretimde tarama | `SCAN` kullan (veya hiç tarama yapma) |
| Büyük değerleri (MB'lar) cache'lemek | Küçük, serileştirilmiş DTO; ağ/bellek maliyeti |
| Vector modülünü sonradan eklemeye çalışmak | Oluşturma sırasında seç; yoksa yeniden oluştur |
| Semantic cache eşiğini ölçmeden seçmek | Gerçek veriyle hit/yanlış-hit analizi |
| Redis'i tek doğruluk kaynağı yapmak | Kalıcı veri DB'de; Redis türetilmiş veri |
| Access key'i kodda tutmak | Entra + managed identity |
| Eski `6380`/Azure Cache for Redis varsayımları | AMR: port 10000, `*.redis.azure.net`, yeni katman adları |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Uzun TTL | Yüksek hit oranı, düşük DB yükü | Bayat veri penceresi büyür |
| Kısa TTL | Taze veri | Düşük hit oranı, sık miss |
| Yazınca sil | Hızlı tazelik | Ek `DEL` yolu; hata durumunda bayat kalma riski |
| Versiyonlu anahtar | Silme/yarış yok | Eski anahtarlar bellekte TTL'e kadar kalır |
| HybridCache (L1+L2) | Redis'e gidiş sayısı azalır, stampede koruması | Çoklu örnekte L1 kısa süre bayat kalabilir (`LocalCacheExpiration` kısa tut) |
| HNSW | Hızlı ANN, büyük veri | Bellek-yoğun; yaklaşık sonuç |
| FLAT | Tam doğruluk, basit | Büyük veride yavaş |
| Redis'te vector | Çok düşük gecikme | **Bellek maliyeti**; kalıcı kaynak değil; RediSearch → Enterprise policy + NoEviction + modül kararı geri alınamaz |
| Semantic cache eşiği gevşek | Yüksek hit, düşük LLM maliyeti | Yanlış cevap riski |
| Memory Optimized / Balanced / Compute Optimized | Bellek:CPU dengesini iş yüküne göre seç | Yanlış seçim ya boş bellek ya CPU darboğazı; ölçerek ölçekle |
| Flash Optimized | Çok büyük veri seti ucuz | Soğuk veri NVMe'de daha yavaş; vector modülü desteği yok *(doğrula)* |

---

### 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Azure Managed Redis overview](https://learn.microsoft.com/en-us/azure/redis/overview) · [Quickstart: create an Azure Managed Redis instance](https://learn.microsoft.com/en-us/azure/redis/quickstart-create-managed-redis)
- [Redis modules in Azure Managed Redis](https://learn.microsoft.com/en-us/azure/redis/redis-modules) · [Vector similarity overview](https://learn.microsoft.com/en-us/azure/redis/overview-vector-similarity) · [Tutorial: semantic cache](https://learn.microsoft.com/en-us/azure/redis/tutorial-semantic-cache)
- [Azure Cache for Redis retirement FAQ](https://learn.microsoft.com/en-us/azure/azure-cache-for-redis/retirement-faq) · [Migration guide](https://learn.microsoft.com/en-us/azure/azure-cache-for-redis/cache-migration-guide)
- [.NET client guidance (Microsoft Entra, StackExchange.Redis)](https://learn.microsoft.com/en-us/azure/redis/dotnet) · [HybridCache in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/hybrid)
- [Redis vector search documentation](https://redis.io/docs/latest/develop/ai/search-and-query/vectors/) · [redis-py](https://redis.readthedocs.io/)
- Önceki: [C2.6 PostgreSQL ve pgvector](#23-azure-database-for-postgresql-ve-pgvector) · [C2.5 Cosmos DB Vector ve Change Feed](#22-cosmos-db-ile-vector-search-ve-change-feed)


### 📝 Bölüm 2.4 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir ürün API'si veritabanından okuduğu veriyi Redis'e yazıyor; yazılan her anahtara 10 dakika sonra silinmesi için süre veriliyor. Bu mekanizmanın adı nedir?

- A) Eviction
- B) Expiration (TTL)
- C) Persistence
- D) Replication

<details><summary>Cevap</summary>

**B.** Anahtara verilen süre dolunca silinmesi **expiration/TTL**'dir (`SET key value EX 600`). Eviction, bellek dolduğunda sunucunun eviction policy'ye göre anahtar silmesidir.
</details>

#### Soru 2: Tek seçim
Cache-aside deseninde ürün fiyatı güncellendi. Okuyucuların eski fiyatı görme süresini **en aza indirmek** için doğru işlem sırası hangisidir?

- A) Cache'te anahtarı sil, sonra veritabanını güncelle
- B) Veritabanını güncelle, sonra cache'te anahtarı sil (TTL güvenlik ağı olarak kalır)
- C) Sadece cache'i güncelle, veritabanını gece güncelle
- D) Hiçbir şey yapma, TTL'in dolmasını bekle

<details><summary>Cevap</summary>

**B.** Önce kaynak veriyi yaz, sonra cache'i invalidate et. A seçeneğinde silme ile yazma arasında bir okuyucu eski veriyi cache'e geri yazabilir. C veri kaybı riskidir (cache kaynak deposu değildir). D sadece TTL'e güvenir; bayat pencere büyür.
</details>

#### Soru 3: Çoklu seçim
Aşağıdakilerden hangileri bir cache'te **invalidation** stratejisidir? (İki seçenek)

- A) Veritabanına yazdıktan sonra ilgili anahtarı silmek
- B) Anahtara sürüm numarası ekleyip (`product:42:v7`) sürümü artırmak
- C) Cache'in clustering policy'sini değiştirmek
- D) Modül eklemek

<details><summary>Cevap</summary>

**A ve B.** Yazınca sil ve versiyonlu anahtar klasik invalidation yöntemleridir (üçüncüsü olay tabanlı silme). Clustering policy ve modüller invalidation ile ilgili değildir.
</details>

#### Soru 4: Senaryo
Bir ana sayfa verisi Redis'te 5 dakika TTL ile tutuluyor. TTL dolduğu anda yüzlerce eşzamanlı istek aynı anda veritabanına gidip veritabanını zorluyor. Bu sorunun adı ve uygun çözümü nedir?

- A) Cache stampede; tek uçuş (single-flight) kilidi, TTL jitter veya HybridCache
- B) Eviction storm; `noeviction` kullanmak
- C) Cold start; kaynak ekleme
- D) Split brain; clustering policy'yi Enterprise yapmak

<details><summary>Cevap</summary>

**A.** Cache stampede (thundering herd) önlemleri: aynı anahtar için tek bir yükleyici (kilit / HybridCache `GetOrCreateAsync`), TTL'e rastgele jitter ve erken yenileme. Diğer seçenekler farklı sorunlardır.
</details>

#### Soru 5: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Eviction policy, belleğin dolması durumunda hangi anahtarların silineceğini belirler. | ☐ | ☐ |
| b) `noeviction` politikasında bellek dolarsa yeni yazmalar hata verebilir. | ☐ | ☐ |
| c) TTL ile eviction aynı mekanizmadır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** TTL uygulamanın verdiği süreye göre silmedir; eviction bellek baskısıyla sunucu tarafında tetiklenir. RediSearch/vector kullanan cache'lerde yalnızca `NoEviction` desteklendiği için TTL ve boyut izleme şarttır.
</details>

#### Soru 6: Senaryo
Bir ekip Azure Managed Redis'te vector benzerlik araması kullanmak istiyor ve cache'i zaten Flash Optimized katmanında oluşturmuş. Cache hâlen vector index oluşturmuyor. Sorunu çözmek için en doğru yaklaşım nedir?

- A) Portal'dan RediSearch modülünü sonradan eklemek
- B) `FT.CREATE`'i `DIALECT 3` ile çalıştırmak
- C) RediSearch modülü destekleyen bir katmanda (Memory Optimized/Balanced/Compute Optimized) modülü **oluşturma sırasında seçerek** yeni bir cache oluşturmak
- D) Eviction policy'yi `allkeys-lru` yapmak

<details><summary>Cevap</summary>

**C.** Modüller yalnızca oluşturma sırasında seçilir ve sonradan eklenemez; RediSearch, Flash Optimized katmanında desteklenmez (güncel dokümanı doğrula). Vector için ayrıca Enterprise clustering policy ve `NoEviction` gerekir; `allkeys-lru` bu durumda desteklenmez.
</details>

#### Soru 7: Tek seçim
RediSearch modülünü kullanacak bir Azure Managed Redis örneği için hangi yapılandırma kombinasyonu **gereklidir**?

- A) OSS clustering policy + `allkeys-lru`
- B) Enterprise clustering policy + `NoEviction`
- C) OSS clustering policy + `volatile-ttl`
- D) Herhangi bir clustering policy + herhangi bir eviction policy

<details><summary>Cevap</summary>

**B.** RediSearch Enterprise clustering policy ve `NoEviction` gerektirir; ikisi de oluşturma sırasında belirlenir.
</details>

#### Soru 8: Komut tamamlama (Redis)
Aşağıdaki komut, 1536 boyutlu embedding'ler için cosine mesafeli HNSW vector indexi oluşturmalıdır. Boşluğu doldurun:

```text
FT.CREATE idx:docs ON HASH PREFIX 1 doc: SCHEMA
    title TEXT
    embedding VECTOR HNSW __ TYPE FLOAT32 DIM 1536 DISTANCE_METRIC COSINE M 16 EF_CONSTRUCTION 200
```

- A) `5`
- B) `6`
- C) `10`
- D) `12`

<details><summary>Cevap</summary>

**C.** Sayı, algoritma özniteliklerinin **token** sayısıdır (ad ve değerler ayrı sayılır): `TYPE FLOAT32` (2) + `DIM 1536` (2) + `DISTANCE_METRIC COSINE` (2) + `M 16` (2) + `EF_CONSTRUCTION 200` (2) = **10**. `5` çift sayısıdır (yanlış birim); `6` yalnızca zorunlu üç özniteliği kapsar.
</details>

#### Soru 9: Komut tamamlama (Redis)
Aşağıdaki komut yalnızca zorunlu öznitelikleri veren bir **FLAT** vector alanı tanımlar. Boşluğu doldurun:

```text
embedding VECTOR FLAT __ TYPE FLOAT32 DIM 768 DISTANCE_METRIC COSINE
```

- A) `3`
- B) `4`
- C) `6`
- D) `8`

<details><summary>Cevap</summary>

**C.** `TYPE FLOAT32 DIM 768 DISTANCE_METRIC COSINE` = 6 token (3 ad-değer çifti).
</details>

#### Soru 10: Tek seçim
`FT.SEARCH` ile KNN sorgusu çalıştırılıyor:

```text
FT.SEARCH idx:docs "*=>[KNN 3 @embedding $vec AS score]" PARAMS 2 vec <blob> SORTBY score DIALECT 2
```

COSINE metriği ile `score` değeri **0.02** olan sonuç hakkında ne söylenebilir?

- A) Skor bir benzerliktir; 0.02 çok düşük benzerlik demektir
- B) Skor bir **mesafedir**; küçük değer, sorguya çok benzer anlamına gelir
- C) Skor negatif olmalıdır
- D) Sonuç geçersizdir; skor en az 1 olmalıdır

<details><summary>Cevap</summary>

**B.** RediSearch'te `KNN ... AS score` bir **mesafe** döndürür (cosine mesafesi = 1 − cosine benzerliği); küçük = benzer. Bu yüzden eşik karşılaştırması `score <= eşik` biçimindedir.
</details>

#### Soru 11: Senaryo
Bir sohbet botunun LLM maliyeti yüksek; kullanıcılar aynı soruyu farklı cümlelerle sürekli soruyor ("İade süresi kaç gün?" / "Ürünü kaç gün içinde iade edebilirim?"). En uygun Redis tabanlı çözüm hangisidir?

- A) Tam metin eşleşmesiyle (string key) cache
- B) Soru embedding'i + vector index ile **semantic cache** (eşik altı mesafede kayıtlı cevabı döndür, TTL ile)
- C) Tüm soruları kalıcı olarak Redis'e yazıp TTL kullanmamak
- D) Eviction'ı kapatmak

<details><summary>Cevap</summary>

**B.** Anlamca benzer sorular farklı string'lerdir; bire bir anahtar eşleşmesi onları yakalayamaz. Semantic cache embedding benzerliğiyle yakalar. TTL, cevapların bayatlamasını önler ve belleği sınırlar; eşik ölçülerek ayarlanmalıdır.
</details>

#### Soru 12: Sıralama
Semantic cache'te **miss** (eşik dışı) durumunda doğru adım sırasını belirleyin: (1) LLM'i çağır (2) sorunun embedding'ini üret (3) cevabı embedding ile birlikte TTL'le Redis'e yaz (4) `KNN 1` ile en yakın kaydı ara ve mesafeyi eşikle karşılaştır

<details><summary>Cevap</summary>

**2 → 4 → 1 → 3.** Embedding üret, en yakın komşuyu ara; mesafe eşikten büyükse LLM'i çağır ve yeni cevabı TTL ile kaydet. Hit durumunda akış 4'ten sonra kayıtlı cevabı döndürüp biter.
</details>

#### Soru 13: Kod tamamlama (C#)
Managed Redis'e parolasız, **Entra** kimliğiyle bağlanın. Boşluğu doldurun:

```csharp
ConfigurationOptions options = ConfigurationOptions.Parse("myredis.westeurope.redis.azure.net:10000");
await options.__________(new DefaultAzureCredential());
ConnectionMultiplexer mux = await ConnectionMultiplexer.ConnectAsync(options);
```

- A) `ConfigureForAzureWithTokenCredentialAsync`
- B) `UseManagedIdentity`
- C) `SetPasswordFromToken`
- D) `AddEntraAuthentication`

<details><summary>Cevap</summary>

**A.** `Microsoft.Azure.StackExchangeRedis` paketinin uzantısı token'ı alır ve yeniler. Token'ı elle `Password` alanına koymak yerine bu yöntem kullanılmalıdır. `ConnectionMultiplexer` uygulama ömrü boyunca tek (singleton) olmalıdır.
</details>

#### Soru 14: Çoklu seçim
Hangi iki senaryoda vector verisini **Redis'te** tutmak, kalıcı kaynak depoya (Cosmos/PostgreSQL) göre daha uygundur?

- A) LLM yanıtları için düşük gecikmeli, TTL'li semantic cache
- B) Kullanıcı oturumuna özel, kısa ömürlü ve yeniden üretilebilir öneri vector'leri
- C) 200 milyon dokümanlık kalıcı bilgi tabanı, ACID ve JOIN gereksinimi
- D) Denetim için yıllarca saklanması gereken sözleşme embedding'leri

<details><summary>Cevap</summary>

**A ve B.** Redis düşük gecikmeli, geçici ve yeniden üretilebilir veri için uygundur. C ve D kalıcı kaynak veri gerektirir: bellek maliyeti ve cache doğası nedeniyle Cosmos DB veya PostgreSQL (pgvector) kullanılmalıdır.
</details>

#### Soru 15: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Managed Redis, Azure Cache for Redis'in halefi olan Redis Enterprise tabanlı hizmettir. | ☐ | ☐ |
| b) Redis modülleri (ör. RediSearch) cache oluşturulduktan sonra istenildiğinde eklenebilir. | ☐ | ☐ |
| c) AMR için Microsoft Entra ID ile kimlik doğrulama desteklenir ve managed identity ile kullanılabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Modüller oluşturma sırasında seçilir; sonradan eklenmez.
</details>

#### Soru 16: Vaka (case study)
**Senaryo:** *Tailwind Travel*, otel arama API'sini Azure Cache for Redis Premium üzerinde çalıştırıyor ve bir de müşteri destek asistanı (LLM) geliştiriyor. Gözlemler: (1) otel detayı sayfası DB'den okunuyor, fiyatlar günde birkaç kez değişiyor ve kullanıcılar bayat fiyat şikayeti ediyor; (2) her gece 03:00'te binlerce anahtar aynı anda doluyor ve süresi aynı anda bitiyor, sabah DB zirve yapıyor; (3) asistan her tekrarlayan soru için LLM'i çağırıyor; ekip semantic cache denemek istiyor ve mevcut cache'e "vector modülünü ekleyelim" diyor; (4) Premium katmanın emeklilik tarihi yaklaşıyor.

**Soru A.** Bayat fiyat sorununu nasıl çözersiniz? **Soru B.** 03:00 yığılmasını nasıl çözersiniz? **Soru C.** Semantic cache için yeni cache'i nasıl yapılandırırsınız ve nelere dikkat edersiniz?

<details><summary>Cevap</summary>

**A.** Cache-aside'a **yazınca invalidate** ekleyin: fiyat DB'ye yazıldıktan sonra `DEL hotel:{id}` (veya olay tabanlı silme / versiyonlu anahtar); TTL güvenlik ağı olarak kalır (kısa-orta süre).

**B.** Tüm anahtarların aynı anda dolması ve aynı anda bitmesi **stampede** yaratır: TTL'e **jitter** ekleyin, ısıtmayı (warm-up) zamana yayın, popüler anahtarlarda **single-flight kilit / HybridCache** kullanın.

**C.** Mevcut cache'e modül **sonradan eklenemez**. **Azure Managed Redis**'te yeni bir örnek oluşturun (bu aynı zamanda Premium emeklilik göçünün hedefidir): RediSearch modülünü oluşturma sırasında seçin, **Enterprise clustering policy** ve **NoEviction** kullanın (Flash Optimized katmanını seçmeyin). `FT.CREATE ... VECTOR HNSW ... DIM <model boyutu> DISTANCE_METRIC COSINE` ile index; her kayda **TTL**, kiracı/kullanıcı verisi için **TAG ön-filtresi**; eşiği gerçek soru setiyle ölçerek seçin (yanlış-hit riski). `NoEviction` olduğu için bellek dolarsa yazma hatası alınır — doluluğu izleyin. Bağlantı Entra + managed identity ile, `ConnectionMultiplexer` singleton.
</details>

---

**Puanlama:** 13-16 → hazırsın · 10-12 → README 3.1 (modül/policy kararları), 3.2 (TTL, invalidation) ve 3.3 (`FT.CREATE`) bölümlerini tekrar et · < 10 → konuyu baştan çalış, 3.5 laboratuvarındaki bilerek bozuk senaryoları uygula.


---

# BÖLÜM 3: Connect to and consume Azure services (%20-25)

---

## 3.1 Azure Functions: Serverless API, Trigger ve Binding

> **Sınav Alanı:** Connect to and consume Azure services (%20-25) · **Konu Kodu:** C2.8

### 1. Ne?

**Azure Functions**, olay güdümlü, **serverless** bir compute servisidir: kodu (bir *function*) yazarsın, platform onu bir **trigger** (tetikleyici) ile çalıştırır, ölçekler ve altyapıyı yönetir. Bir function'ın bağlantı kodunu azaltan iki kavram sınavın merkezidir:

| Kavram | Ne yapar? | Kaç tane? | Örnek |
|---|---|---|---|
| **Trigger** | Function'ı **başlatır**, veriyi function'a taşır | Function başına **tam 1** | HTTP, Timer, Service Bus, Event Grid, Cosmos DB (change feed), Blob, Queue |
| **Input binding** | Function çalışırken başka bir kaynaktan **veri okur** | 0..n | Cosmos DB'den belge, Blob içeriği |
| **Output binding** | Function'ın sonucunu başka bir kaynağa **yazar** | 0..n | Cosmos DB'ye belge, Service Bus'a mesaj, kuyruğa ekleme |

```text
HTTP isteği ──▶ [HTTP trigger] Function ──▶ [Cosmos DB output binding] ──▶ orders container
Service Bus ──▶ [ServiceBus trigger] Function ──▶ [Cosmos DB input binding ile lookup]
```

Bu konu: **isolated worker model** (.NET) ve **Python v2** programlama modeli, HTTP trigger ile API, trigger/binding seçimi, `host.json` / `local.settings.json`, **hosting planları** (Flex Consumption, Consumption, Premium, Dedicated), **identity-based bağlantı** ve dağıtım. Laboratuvar yerelde Core Tools + emülatörlerle başlar, Flex Consumption'a dağıtımla biter (3.8). Mesajlaşma ayrıntıları → [C2.9](#32-azure-service-bus-kuyruk-topic-ve-dead-letter), olay yönlendirme → [C2.10](#33-azure-event-grid-custom-event-filtre-ve-retry), Cosmos DB → [C2.4](#21-cosmos-db-for-nosql-sdk-partitioning-indexing-consistency-ru).

---

### 2. Neden?

- **Sınav:** "Connect to and consume Azure services" alanında Functions, kod/CLI tamamlama soruları için en verimli konudur: *hangi attribute?*, *hangi app setting adı?*, *hangi plan?*, *trigger mı binding mi?*
- **Gerçek işte:** Webhook'lar, kuyruk işleyiciler, zamanlanmış işler ve hafif API'ler için en az operasyonla çalışan bir ölçek birimidir; kullanılmayan zamanda (scale-to-zero) maliyet de sıfıra yaklaşır.
- **Kimlik:** "Connection string'i app setting'e koy" yerine **managed identity** ile secret'sız bağlantı (→ [C2.11](#41-managed-identity-ve-azure-key-vault)) hem güvenlik hem sınav için artık varsayılan beklentidir.
- **Takvim:** **In-process model**'in desteği **10 Kasım 2026**'da sona eriyor; yeni ve migrate edilen .NET uygulamalarının yolu **isolated worker model**'dir. Linux Consumption planı ise **30 Eylül 2028**'de emekliye ayrılıyor; öneri Flex Consumption'a geçiştir *(tarihler Microsoft duyurularına göre; sınav öncesi doğrula)*.

---

### 3. Nasıl?

#### 3.1 Programlama modelleri

| Dil | Model | Not |
|---|---|---|
| **.NET** | **Isolated worker** (kendi `Program.cs`'i olan ayrı süreç) | Functions runtime v4; .NET 10 (LTS), 9, 8 ve .NET Framework 4.8 desteklenir; **in-process model .NET 8'de kalır ve 10 Kasım 2026'da destek dışına çıkar** |
| **Python** | **v2 programlama modeli** (decorator tabanlı, `function_app.py`) | v1'deki `function.json` dosyası gerekmez; binding'ler decorator ile tanımlanır |
| JavaScript/TypeScript, Java, PowerShell | Kendi worker'ları | Sınav odağı .NET ve Python |

Isolated worker'ın in-process'e göre avantajları: kendi DI/middleware/yapılandırması, .NET sürümünü runtime'dan bağımsız seçme, **ASP.NET Core entegrasyonu** (HTTP function'larda `HttpRequest`/`IActionResult`), çakışmayan assembly'ler.

```csharp
// Program.cs (isolated worker, ASP.NET Core integration)
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);
builder.ConfigureFunctionsWebApplication();          // ASP.NET Core integration (HttpRequest / IActionResult)
builder.Services.AddApplicationInsightsTelemetryWorkerService();
builder.Services.ConfigureFunctionsApplicationInsights();
builder.Build().Run();
```

> `ConfigureFunctionsWebApplication` ASP.NET Core entegrasyonudur; HTTP kullanmayan bir uygulamada `ConfigureFunctionsWorkerDefaults` yeterlidir. Eski stil `new HostBuilder()...ConfigureFunctionsWorkerDefaults()` hâlâ çalışır ve `HttpRequestData`/`HttpResponseData` ile birlikte kullanılır. `FunctionsApplication.CreateBuilder` 2.x worker paketlerini ister.

#### 3.2 HTTP trigger ile API

```csharp
public class OrdersApi(ILogger<OrdersApi> logger)
{
    public record OrderRequest(string CustomerId, decimal Amount);
    public record Order(string id, string customerId, decimal amount, DateTimeOffset createdAt);

    // POST /api/orders  -> writes the order to Cosmos DB via an output binding
    [Function("CreateOrder")]
    public CreateOrderResult CreateOrder(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "orders")] HttpRequest req,
        [FromBody] OrderRequest body)
    {
        var order = new Order(Guid.NewGuid().ToString(), body.CustomerId, body.Amount, DateTimeOffset.UtcNow);
        logger.LogInformation("Order {Id} accepted", order.id);
        return new CreateOrderResult
        {
            Order = order,
            HttpResponse = new CreatedResult($"/api/orders/{order.id}", order),
        };
    }

    public class CreateOrderResult
    {
        [CosmosDBOutput(databaseName: "shop", containerName: "orders", Connection = "CosmosConnection")]
        public Order? Order { get; set; }

        [HttpResult]                                     // the response returned to the HTTP caller
        public IActionResult? HttpResponse { get; set; }
    }
}
```

- **Birden çok çıktı** için dönüş tipi, her çıktı özelliğine attribute konmuş bir sınıftır; HTTP yanıtı `[HttpResult]` ile işaretlenir.
- **Auth level** (`AuthorizationLevel`): `Anonymous` (anahtar yok) · `Function` (function veya host anahtarı: `x-functions-key` header'ı ya da `?code=`) · `Admin` (master anahtar). Kullanıcı kimliği doğrulaması (Entra/OAuth) için App Service Authentication ("Easy Auth") veya API Management kullanılır; anahtarlar **kimlik doğrulama değil**, basit erişim kontrolüdür.
- `Route = "orders/{id}"` parametreleri method imzasına bağlanır; varsayılan önek `/api` `host.json` içindeki `extensions.http.routePrefix` ile değişir.

#### 3.3 Trigger ve binding seçimi ⭐

| İhtiyaç | Trigger | Binding örneği | Not |
|---|---|---|---|
| HTTP API / webhook | **HTTP** | Cosmos DB output | Yanıt en fazla ~230 sn bekler (load balancer zaman aşımı) — uzun iş kuyruğa |
| Zamanlanmış iş | **Timer** | — | **6 alanlı NCRONTAB** `{saniye} {dakika} {saat} {gün} {ay} {haftanın günü}` |
| Kuyruktaki iş | **Service Bus** (queue/topic) | Cosmos DB input | Hata → abandon → DLQ (→ [C2.9](#32-azure-service-bus-kuyruk-topic-ve-dead-letter)) |
| Olay tepkisi | **Event Grid** | — | Olay şeması veya `CloudEvent` (→ [C2.10](#33-azure-event-grid-custom-event-filtre-ve-retry)) |
| Veri değişimine tepki | **Cosmos DB** (change feed) | — | Lease container tutar (→ [C2.5](#22-cosmos-db-ile-vector-search-ve-change-feed)) |
| Blob'a tepki | **Blob** | Blob input/output | Flex Consumption'da yalnızca **event-based** (Event Grid kaynaklı) blob trigger desteklenir *(doğrula)* |
| Basit kuyruk | **Queue** (Storage) | Queue output | 64 KB mesaj sınırı; zengin özellik için Service Bus |

**Binding ifadeleri:** `{queueTrigger}`, `{data.orderId}` (JSON alanı), `{rand-guid}`, `{DateTime}`; ayar değerleri `%SettingName%` ile okunur (ör. `Connection = "ServiceBusConnection"`, `QueueName = "%OrdersQueue%"`).

```csharp
// Service Bus trigger (isolated): manual settlement via ServiceBusMessageActions
[Function("ProcessOrder")]
public async Task Run(
    [ServiceBusTrigger("order-processing", Connection = "ServiceBusConnection", AutoCompleteMessages = false)]
    ServiceBusReceivedMessage message,
    ServiceBusMessageActions actions)
{
    var order = message.Body.ToObjectFromJson<Order>();
    if (order is null) { await actions.DeadLetterMessageAsync(message, deadLetterReason: "BadPayload"); return; }
    // ... business logic; an unhandled exception => the message is abandoned and redelivered
    await actions.CompleteMessageAsync(message);
}
```

#### 3.4 `host.json` ve `local.settings.json`

| Dosya | Amaç | Dağıtılır mı? |
|---|---|---|
| **`host.json`** | **Host** düzeyi davranış: loglama/sampling, `extensions.*` (Service Bus `maxConcurrentCalls`, `maxAutoLockRenewalDuration`, HTTP `routePrefix`), `functionTimeout`, `retry` | Evet |
| **`local.settings.json`** | **Yerel** app settings + connection string'ler (`Values`) | **Hayır** (`.gitignore`'a gider; secret içerir) — Azure'da karşılığı *Application settings* |
| `*.csproj` / `function_app.py` | Function tanımı (attribute / decorator) | Evet |

```jsonc
// host.json
{
  "version": "2.0",
  "functionTimeout": "00:10:00",
  "logging": { "applicationInsights": { "samplingSettings": { "isEnabled": true, "excludedTypes": "Request" } } },
  "extensions": { "serviceBus": { "maxConcurrentCalls": 8, "maxAutoLockRenewalDuration": "00:05:00" } }
}
```

```jsonc
// local.settings.json (local only, never committed)
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "ServiceBusConnection": "Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;",
    "CosmosConnection": "AccountEndpoint=https://localhost:8081/;AccountKey=<emulator-key>;"
  }
}
```

#### 3.5 Hosting planları ⭐

| | **Flex Consumption** | **Consumption** (klasik) | **Premium** (Elastic) | **Dedicated** (App Service planı) |
|---|---|---|---|---|
| Scale to zero | **Evet** | Evet | Hayır (en az 1 instance) | Hayır |
| Cold start azaltma | **Always ready** instance'lar (isteğe bağlı) + hızlı ölçek | Yok | **Pre-warmed** + her zaman ısınmış instance | Always On |
| **VNet entegrasyonu** | **Evet** | **Hayır** | Evet | Evet |
| Varsayılan zaman aşımı | 30 dk (üst sınır sınırsız) | 5 dk (en çok 10 dk) | 30 dk (sınırsız) | 30 dk (sınırsız) |
| Ölçek | Fonksiyon/grup başına bağımsız; en çok **1000** instance (kota ile sınırlı) | Eski ölçek denetleyicisi (daha yavaş) | Önceden ısınmış + elastik | Manuel/otomatik App Service ölçeği |
| Instance boyutu | **2048 / 4096 MB** | Sabit | EP1-EP3 | Plan SKU'su |
| Fiyat modeli | İstek/çalışma süresi (+ always ready) | Çalışma süresi × bellek | Instance-saati (ısınmış min.) | Plan ücreti (sabit) |
| Not | Linux; **in-process .NET desteklenmez**; deployment slot desteklenmez *(doğrula)* | Linux Consumption **30 Eyl 2028**'de emekli | Slot, uzun işler | Mevcut App Service planını yeniden kullanma |

**Karar ağacı:**
1. Hem **VNet** hem **scale-to-zero** gerekiyor → **Flex Consumption**.
2. Soğuk başlangıç **kabul edilemez**, sabit gecikme + VNet + slot → **Premium**.
3. Zaten kullanılan, boşta kapasitesi olan App Service planı → **Dedicated**.
4. Basit, VNet gerekmeyen, Windows tabanlı → klasik **Consumption** (yeni projelerde Flex önerilir).

#### 3.6 Yapılandırma ve identity-based bağlantılar ⭐

Trigger/binding'in `Connection` özelliği bir **ayar adı öneki**dir. Connection string yerine kimlik kullanmak için ayarlar **`<ÖNEK>__<özellik>`** (çift alt çizgi) biçiminde verilir:

| Servis | Ayar adı (önek `MyConn`) | Gereken rol |
|---|---|---|
| **Service Bus** | `MyConn__fullyQualifiedNamespace` = `mysb.servicebus.windows.net` | *Azure Service Bus Data Receiver* (trigger) · *Data Sender* (output) |
| **Cosmos DB** | `MyConn__accountEndpoint` = `https://myacct.documents.azure.com:443/` | Cosmos DB **data-plane RBAC** rolü (*Built-in Data Contributor*) — Azure RBAC değil, `az cosmosdb sql role assignment` |
| **Storage** (Blob/Queue) | `MyConn__accountName` (veya `__blobServiceUri`, `__queueServiceUri`) | *Storage Blob Data Owner/Contributor*, *Storage Queue Data Contributor* |
| **Event Hubs** | `MyConn__fullyQualifiedNamespace` | *Azure Event Hubs Data Receiver/Sender* |
| **`AzureWebJobsStorage`** (host'un kendi deposu) | `AzureWebJobsStorage__accountName` | Blob/Queue/Table için ilgili *Data* rolleri |

- Kullanıcı tarafından atanmış (user-assigned) kimlik için ek olarak `MyConn__credential = managedidentity` ve `MyConn__clientId = <id>` verilir; atanmamışsa **system-assigned** kimlik kullanılır.
- Aynı önek için hem connection string hem identity ayarı varsa **connection string önceliklidir**.
- **Key Vault referansı:** `@Microsoft.KeyVault(SecretUri=https://kv.vault.azure.net/secrets/Name/)` biçiminde app setting değeri; function app'in kimliği *Key Vault Secrets User* rolüne ihtiyaç duyar (→ [C2.11](#41-managed-identity-ve-azure-key-vault)). App Configuration / Key Vault'tan okurken ayırıcı `__` yerine `:` kullanılır.
- Flex Consumption'da `FUNCTIONS_WORKER_RUNTIME` gibi bazı ayarlar app setting değil **function app yapılandırmasıdır** (CLI `--runtime`); Azure Files kullanılmaz, dağıtım paketi bir **blob container**'dadır *(ayrıntılar için Flex dokümanını doğrula)*.

#### 3.7 Dağıtım

| Yöntem | Komut / araç | Not |
|---|---|---|
| Core Tools | `func azure functionapp publish <app>` | Hızlı geliştirici dağıtımı |
| Zip deploy / Run-from-package | `az functionapp deployment source config-zip` | Paket salt-okunur bağlanır |
| CI/CD | **GitHub Actions** (`Azure/functions-action`), Azure Pipelines | OIDC (federated credential) ile secret'sız giriş |
| **Deployment slot** | `az functionapp deployment slot create` + `swap` | Premium/Dedicated (ve sınırlı Consumption); sıfır kesintili geçiş |
| IaC | Bicep / Terraform | Flex için `functionAppConfig` bölümü |

*(bağlam)* **Durable Functions** (orchestrator + activity): fan-out/fan-in, human interaction, async HTTP API gibi kalıpları kod olarak yazılan bir iş akışıyla çözer; AI-200 hedeflerinde açıkça yoktur, mimari bilgisi olarak yeterlidir.

#### 3.8 Lab — yerelde çalıştır, Flex Consumption'a dağıt

> Maliyet: Flex Consumption'da kullanılmayan zamanda ücret yok; ücretsiz aylık istek/çalışma süresi hibesi var. Yine de önce **budget alarm**'ı kur (→ C1.11). Cosmos DB için **serverless** hesap veya ücretsiz katman kullan.

**a) Yerel geliştirme** (Core Tools + Azurite + Service Bus emulator → emulator kurulumu [C2.9](#32-azure-service-bus-kuyruk-topic-ve-dead-letter#39-lab--emulatör-ile-kuyruk-topic-ve-dlq)):

```bash
npm install -g azurite && azurite --silent --location ./.azurite &     # storage emulator for AzureWebJobsStorage
func init OrdersApi --worker-runtime dotnet-isolated --target-framework net10.0
cd OrdersApi
func new --template "Http trigger" --name CreateOrder --authlevel function
dotnet add package Microsoft.Azure.Functions.Worker.Extensions.CosmosDB
dotnet add package Microsoft.Azure.Functions.Worker.Extensions.ServiceBus
func start
# in another terminal
curl -s -X POST "http://localhost:7071/api/orders" -H "Content-Type: application/json" \
     -d '{"customerId":"c-42","amount":1500}'
```

**b) Azure kaynakları ve Flex Consumption function app:**

```bash
RG=rg-ai200-c28; LOC=westeurope
ST=stfunc$RANDOM; APP=func-ai200-$RANDOM; COSMOS=cosmos-ai200-$RANDOM
az group create --name $RG --location $LOC
az storage account create --name $ST --resource-group $RG --location $LOC --sku Standard_LRS
az cosmosdb create --name $COSMOS --resource-group $RG --capabilities EnableServerless
az cosmosdb sql database create --account-name $COSMOS --resource-group $RG --name shop
az cosmosdb sql container create --account-name $COSMOS --resource-group $RG --database-name shop \
  --name orders --partition-key-path /customerId

az functionapp list-flexconsumption-runtimes --location $LOC --runtime dotnet-isolated -o table   # pick a supported version
az functionapp create --name $APP --resource-group $RG --storage-account $ST \
  --flexconsumption-location $LOC --runtime dotnet-isolated --runtime-version 10.0 \
  --instance-memory 2048 --maximum-instance-count 40

# identity-based connections: system-assigned identity + roles
az functionapp identity assign --name $APP --resource-group $RG
PID=$(az functionapp identity show --name $APP --resource-group $RG --query principalId -o tsv)
az role assignment create --assignee $PID --role "Storage Blob Data Owner" \
  --scope $(az storage account show --name $ST --resource-group $RG --query id -o tsv)
az cosmosdb sql role assignment create --account-name $COSMOS --resource-group $RG \
  --role-definition-name "Cosmos DB Built-in Data Contributor" --principal-id $PID \
  --scope "/"                                              # data-plane RBAC, not az role assignment
az functionapp config appsettings set --name $APP --resource-group $RG --settings \
  "CosmosConnection__accountEndpoint=$(az cosmosdb show --name $COSMOS --resource-group $RG --query documentEndpoint -o tsv)"

cd OrdersApi && func azure functionapp publish $APP
KEY=$(az functionapp keys list --name $APP --resource-group $RG --query functionKeys.default -o tsv)
curl -s -X POST "https://$APP.azurewebsites.net/api/orders?code=$KEY" -H "Content-Type: application/json" \
     -d '{"customerId":"c-42","amount":1500}'
```

> Flex'te `--runtime-version` ve dağıtım depolama kimliği seçenekleri CLI sürümüne göre değişir; `az functionapp create --help` ve `list-flexconsumption-runtimes` çıktısını esas al.

**c) Python v2 eşdeğeri** (`function_app.py`):

```python
import json, uuid, azure.functions as func

app = func.FunctionApp(http_auth_level=func.AuthLevel.FUNCTION)

@app.route(route="orders", methods=["POST"])
@app.cosmos_db_output(arg_name="doc", database_name="shop", container_name="orders",
                      connection="CosmosConnection")
def create_order(req: func.HttpRequest, doc: func.Out[func.Document]) -> func.HttpResponse:
    body = req.get_json()
    order = {"id": str(uuid.uuid4()), "customerId": body["customerId"], "amount": body["amount"]}
    doc.set(func.Document.from_dict(order))
    return func.HttpResponse(json.dumps(order), status_code=201, mimetype="application/json")

@app.service_bus_queue_trigger(arg_name="msg", queue_name="order-processing",
                               connection="ServiceBusConnection")
def process_order(msg: func.ServiceBusMessage) -> None:
    print("order:", msg.get_body().decode())

@app.timer_trigger(schedule="0 */5 * * * *", arg_name="timer", run_on_startup=False)
def every_five_minutes(timer: func.TimerRequest) -> None:
    print("tick")
```

**d) Temizlik:**

```bash
az group delete --name $RG --yes --no-wait
```

> **Alıştırma.** `ProcessOrder` function'ına bilerek `throw new InvalidOperationException()` ekle, kuyruğa bir mesaj gönder ve mesajın kaç kez yeniden denendiğini ve nereye düştüğünü gözlemle (→ [C2.9](#32-azure-service-bus-kuyruk-topic-ve-dead-letter) DLQ).
>
> <details><summary>Çözüm</summary>
>
> İşlenmeyen exception → Functions host mesajı **abandon** eder → kuyruk teslim sayısını artırır → **MaxDeliveryCount** (varsayılan 10) aşılınca mesaj `order-processing/$deadletterqueue` alt kuyruğuna `MaxDeliveryCountExceeded` nedeniyle düşer. Bu *Service Bus* davranışıdır; Functions'ın `host.json` içindeki `retry` ilkesi ise function'ı aynı örnekte tekrar çalıştırır — ikisini karıştırma.
> </details>

---

### 4. Gerçek dünya senaryosu

Bir e-ticaret şirketi sipariş akışını Functions ile kuruyor:

| Parça | Seçim | Neden |
|---|---|---|
| `POST /api/orders` | **HTTP trigger** + **Cosmos DB output binding**, function key | Kodsuz persist; Easy Auth/APIM ile kullanıcı doğrulama |
| Ödeme sonrası iş akışı | **Service Bus trigger** (`order-processing`) | Yük dalgalanmasını tamponlar, DLQ ile zehirli mesaj izolasyonu |
| Stok bilgisinin okunması | **Cosmos DB input binding** | Bağlantı kodu yok |
| Gece raporu | **Timer trigger** (`0 0 2 * * *`) | Sunucu yönetimi yok |
| Ortam | **Flex Consumption** (VNet: özel Cosmos/Service Bus endpoint'leri; scale-to-zero) | Gece trafik yokken maliyet ~0, kampanyada hızlı ölçek |
| Gizli bilgiler | **Managed identity** (`__fullyQualifiedNamespace`, `__accountEndpoint`), Key Vault referansı | Secret rotasyon yükü yok |
| Dağıtım | GitHub Actions + OIDC | Sıfır statik credential |

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Kod tamamlama (attribute/ayar adı), senaryo → plan, "trigger mı binding mi?" ayrımı.

| Senaryodaki ifade | Cevap |
|---|---|
| "Function'ı başlatan olay" | **Trigger** (function başına tam bir tane) |
| "Sonucu kuyruğa/DB'ye yaz, ek SDK kodu yazma" | **Output binding** |
| "VNet entegrasyonu **ve** scale-to-zero" | **Flex Consumption** |
| "Soğuk başlangıç olmasın, VNet, slot" | **Premium** |
| "Mevcut App Service planında çalış" | **Dedicated** |
| "Secret saklamadan Service Bus'a bağlan" | `<önek>__fullyQualifiedNamespace` + *Data Receiver* rolü |
| "Cosmos DB'ye identity ile bağlan" | `<önek>__accountEndpoint` + **Cosmos DB data-plane** rolü |
| "Storage'a identity ile bağlan" | `AzureWebJobsStorage__accountName` + Blob/Queue/Table *Data* rolleri |
| "Her 5 dakikada bir çalış" | Timer: `0 */5 * * * *` (**6 alan**) |
| "Yerel ayarlar nerede?" | `local.settings.json` (dağıtılmaz) |
| "Anahtarsız herkese açık endpoint" | `AuthorizationLevel.Anonymous` |

**Tuzaklar:**
- **Trigger vs binding:** Function'ı sadece trigger başlatır; input/output binding başlatmaz. Bir function'ın iki trigger'ı olamaz.
- **Cosmos DB RBAC:** Cosmos'ta identity erişimi Azure RBAC rolü ile değil **kendi data-plane rol atamasıyla** verilir; `Contributor` rolü veriyi okutmaz.
- **`__` ayırıcısı:** `MyConn:fullyQualifiedNamespace` yalnızca Key Vault/App Configuration kaynaklı ayarlarda; app settings'te **`__`**.
- **Klasik Consumption'da VNet yok**; "VNet + scale-to-zero" = Flex.
- **Timer** ifadesi 6 alanlıdır (Linux cron'daki 5 alan değil); saat dilimi varsayılan UTC (`WEBSITE_TIME_ZONE` ile değişir).
- **`function.json`**: Python v1 ve script dillerinde binding tanımı; **Python v2'de decorator**, .NET isolated'da **attribute** kullanılır.
- Function key bir **kimlik doğrulama** değildir.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| In-process modelle yeni proje başlatmak | **Isolated worker** (in-process desteği 10 Kasım 2026'da biter, Flex'te yok) |
| `local.settings.json`'u commit etmek | Secret içerir; `.gitignore`'da tut; Azure'da app settings / Key Vault |
| Function içinde `new ServiceBusClient(cs)` ve `new CosmosClient(...)` yazmak (basit durumda) | Binding kullan; karmaşık durumda client'ı **DI ile singleton** tut, istek başına yaratma |
| HTTP function'da uzun iş yapmak | Kuyruğa at, `202 Accepted` dön; HTTP yanıtı ~230 sn'de kesilir |
| İşlenmeyen exception'ı yutmak | Exception fırlatılmazsa mesaj tamamlanmış sayılır; poison mesaj DLQ'ya gitmez |
| Identity için rolü atayıp ayar adını yanlış yazmak (`Connection__namespace`) | `__fullyQualifiedNamespace` / `__accountEndpoint` / `__accountName` |
| Timer'ı 5 alanlı cron ile yazmak | 6 alanlı NCRONTAB |
| Consumption'da VNet beklemek | Flex veya Premium |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Flex Consumption **always ready** instance | Soğuk başlangıç yok / çok kısa | Kullanılmasa da ücretlenen instance |
| 2048 → 4096 MB instance | Daha fazla bellek/CPU | Birim süre maliyeti ↑ |
| Yüksek `maxConcurrentCalls` / `maxScale` | Verim ↑ | Aşağı akış servisi (Cosmos RU, SQL bağlantısı) boğulabilir; **maximum instance count** ile sınırla |
| Premium | Sıfır soğuk başlangıç, slot, VNet | En az 1 instance sürekli ücretli |
| Binding'ler | Az kod | Esneklik azalır (özel retry, toplu işlem, bağlantı havuzu denetimi için SDK) |
| ASP.NET Core entegrasyonu | Tanıdık `HttpRequest`/`IActionResult` | Biraz daha büyük ayak izi |
| `host.json` `retry` | Geçici hatalarda basit tekrar | Service Bus gibi kendi teslim sayacı olan kaynaklarda **çift retry**; idempotency şart |

Functions **at-least-once** çalışır: aynı mesaj iki kez işlenebilir → function'ı **idempotent** yaz.

---

### 8. Kaynaklar

- [AI-200 study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Azure Functions overview](https://learn.microsoft.com/en-us/azure/functions/functions-overview) · [Triggers and bindings concepts](https://learn.microsoft.com/en-us/azure/azure-functions/functions-triggers-bindings)
- [Guide for .NET isolated worker model](https://learn.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-process-guide) · [Migrate from in-process to isolated](https://learn.microsoft.com/en-us/azure/azure-functions/migrate-dotnet-to-isolated-model)
- [Python v2 programming model](https://learn.microsoft.com/en-us/azure/azure-functions/functions-reference-python)
- [Hosting plans comparison](https://learn.microsoft.com/en-us/azure/azure-functions/functions-scale) · [Flex Consumption plan](https://learn.microsoft.com/en-us/azure/azure-functions/flex-consumption-plan)
- [Identity-based connections](https://learn.microsoft.com/en-us/azure/azure-functions/functions-reference#configure-an-identity-based-connection) · [Service Bus trigger](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-service-bus-trigger)
- [host.json reference](https://learn.microsoft.com/en-us/azure/azure-functions/functions-host-json) · [Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- Emeklilik duyuruları: in-process model (10 Kasım 2026) ve Linux Consumption (30 Eylül 2028) — Azure Updates / Functions hosting dokümanı


### 📝 Bölüm 3.1 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir function; bir Service Bus kuyruğundan mesaj alıyor, Cosmos DB'den bir belge okuyor ve sonucu başka bir kuyruğa yazıyor. Bu function'da kaç **trigger** vardır?

- A) 3
- B) 2
- C) 1
- D) 0; yalnızca binding vardır

<details><summary>Cevap</summary>

**C.** Function'ı başlatan tek şey trigger'dır ve function başına **tam bir** trigger bulunur (Service Bus). Cosmos DB okuması **input binding**, ikinci kuyruğa yazma **output binding**'dir.
</details>

#### Soru 2: Tek seçim
Yeni bir .NET 10 function app yazıyorsun ve HTTP function'larında `HttpRequest` / `IActionResult` kullanmak istiyorsun. Hangi model ve builder çağrısı doğrudur?

- A) In-process model, `ConfigureFunctionsWorkerDefaults`
- B) Isolated worker model, `ConfigureFunctionsWebApplication`
- C) In-process model, `ConfigureFunctionsWebApplication`
- D) Isolated worker model, yalnızca `HttpRequestData` kullanılabilir; ASP.NET Core tipleri desteklenmez

<details><summary>Cevap</summary>

**B.** .NET 10 yalnızca isolated worker modelinde desteklenir (in-process 10 Kasım 2026'da destek dışına çıkıyor ve .NET 8'de kalıyor). ASP.NET Core entegrasyonu `ConfigureFunctionsWebApplication` ile açılır; D yanlıştır, `HttpRequestData` eski/alternatif yoldur.
</details>

#### Soru 3: Tek seçim
Bir function app'in **hem sanal ağdaki özel endpoint'lere erişmesi hem de trafik yokken sıfıra ölçeklenmesi** gerekiyor. Hangi plan?

- A) Klasik Consumption
- B) Flex Consumption
- C) Premium (EP1)
- D) Dedicated (App Service planı)

<details><summary>Cevap</summary>

**B.** Flex Consumption hem VNet entegrasyonu hem scale-to-zero sunar. Klasik Consumption'da VNet yoktur; Premium ve Dedicated VNet destekler ama **scale-to-zero yapmaz** (en az bir instance çalışır).
</details>

#### Soru 4: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) `local.settings.json` dosyası `func azure functionapp publish` ile Azure'a dağıtılan ayarların kaynağıdır ve repoya konması önerilir. | ☐ | ☐ |
| b) Azure'daki karşılığı **Application settings**'tir. | ☐ | ☐ |
| c) `host.json` host düzeyindeki davranışı (ör. Service Bus `maxConcurrentCalls`) yapılandırır ve uygulamayla birlikte dağıtılır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** `local.settings.json` yalnızca yerel çalışma içindir, secret içerebilir, repoya konmaz ve dağıtılmaz (publish sırasında ayarları yüklemek ayrı ve bilinçli bir seçenektir). `host.json` ise dağıtılır.
</details>

#### Soru 5: Kod tamamlama (C#)
Bir Service Bus trigger, **system-assigned managed identity** ile bağlanacak. Function şöyle:

```csharp
[Function("ProcessOrder")]
public void Run([ServiceBusTrigger("order-processing", Connection = "SbConn")] ServiceBusReceivedMessage msg) { }
```

Azure'da hangi **app setting** tanımlanmalıdır?

- A) `SbConn = sb://mysb.servicebus.windows.net`
- B) `SbConn__fullyQualifiedNamespace = mysb.servicebus.windows.net`
- C) `SbConn:fullyQualifiedNamespace = mysb.servicebus.windows.net`
- D) `ServiceBusConnection__namespace = mysb`

<details><summary>Cevap</summary>

**B.** `Connection` bir **ayar öneki**dir; identity-based bağlantıda `<önek>__fullyQualifiedNamespace` (çift alt çizgi) kullanılır. `:` ayırıcısı yalnızca Key Vault/App Configuration kaynaklı yapılandırmada geçerlidir. Ek olarak identity'ye *Azure Service Bus Data Receiver* rolü verilmelidir.
</details>

#### Soru 6: Çoklu seçim (iki doğru cevap)
Bir function app'in **Cosmos DB output binding**'ini managed identity ile kullanması için **hangi ikisi** gerekir?

- A) `<önek>__accountEndpoint` app setting'i
- B) Identity'ye Azure RBAC **Contributor** rolü
- C) Cosmos DB **data-plane** rol ataması (ör. Built-in Data Contributor)
- D) `AzureWebJobsStorage` içine Cosmos anahtarı

<details><summary>Cevap</summary>

**A ve C.** Endpoint ayarı bağlantıyı tanımlar; Cosmos DB veri erişimi Azure RBAC'tan **ayrı**, kendi data-plane rol atamasıyla (`az cosmosdb sql role assignment create`) verilir. Contributor yönetim düzlemi rolüdür, veri okutmaz/yazdırmaz (B yanlış).
</details>

#### Soru 7: Kod tamamlama (Python)
Aşağıdaki Python v2 function'ı her gün 02:00'de (UTC) çalışmalıdır. Boşluğu doldurun.

```python
@app.timer_trigger(schedule="______", arg_name="timer")
def nightly(timer: func.TimerRequest) -> None:
    ...
```

- A) `0 2 * * *`
- B) `0 0 2 * * *`
- C) `2 0 * * * *`
- D) `* * 2 * * *`

<details><summary>Cevap</summary>

**B.** Timer trigger **altı alanlı NCRONTAB** kullanır: `{saniye} {dakika} {saat} {gün} {ay} {haftanın günü}`. A beş alanlıdır (geçersiz); C her saatin 0. dakikasının 2. saniyesinde çalışır; D 02:00 saati boyunca her saniye çalışır.
</details>

#### Soru 8: Sıralama
Yerelde geliştirip Azure'a dağıtmak için adımları doğru sıraya koyun: **(1)** `func azure functionapp publish <app>` · **(2)** `func init --worker-runtime dotnet-isolated` · **(3)** `az functionapp create --flexconsumption-location …` · **(4)** `func start` ile yerelde test.

<details><summary>Cevap</summary>

**2 → 4 → 3 → 1.** Proje oluşturulur, yerelde test edilir, hedef function app (ve gerekli kimlik/ayarlar) oluşturulur, ardından kod yayımlanır. (Function app'i test öncesinde oluşturmak da mümkündür; ancak publish, hedefin var olmasını gerektirir ve her zaman en sonda gelir.)
</details>

#### Soru 9: CLI tamamlama
Flex Consumption plan üzerinde, .NET isolated çalışan bir function app oluşturuyorsunuz. Eksik parametreyi seçin.

```bash
az functionapp create --name func-demo --resource-group rg-demo --storage-account stdemo \
  ______ westeurope --runtime dotnet-isolated --runtime-version 10.0
```

- A) `--consumption-plan-location`
- B) `--flexconsumption-location`
- C) `--plan`
- D) `--premium-plan-location`

<details><summary>Cevap</summary>

**B.** Flex Consumption'a özel parametre `--flexconsumption-location`'dır (A klasik Consumption içindir). Desteklenen runtime sürümleri `az functionapp list-flexconsumption-runtimes` ile sorgulanır.
</details>

#### Soru 10: Tek seçim
Bir HTTP function'ı `AuthorizationLevel.Function` ile korunuyor. Bir istemci, kullanıcının **kim olduğunu** (Entra ID kimliği) doğrulamak istiyor. Function key bunun için yeterli midir?

- A) Evet, function key kullanıcıyı doğrular
- B) Hayır; key yalnızca paylaşılan bir sır olarak erişimi kısıtlar, kullanıcı doğrulaması için Easy Auth / API Management / token doğrulama gerekir
- C) Evet, ama yalnızca `AuthorizationLevel.Admin` ile
- D) Hayır; Functions HTTP trigger'larında kimlik doğrulama hiç desteklenmez

<details><summary>Cevap</summary>

**B.** Anahtarlar kimlik doğrulama değil basit erişim kontrolüdür. Kullanıcı kimliği için App Service Authentication (Easy Auth), API Management veya kod içinde JWT doğrulaması kullanılır.
</details>

#### Soru 11: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Klasik Consumption planı VNet entegrasyonunu destekler. | ☐ | ☐ |
| b) Premium planda her zaman en az bir instance çalışır (scale-to-zero yoktur). | ☐ | ☐ |
| c) Flex Consumption'da in-process .NET modeli kullanılabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** VNet için Flex, Premium veya Dedicated gerekir. Premium önceden ısıtılmış instance'larla soğuk başlangıcı ortadan kaldırır ama sürekli ücretlenir. Flex yalnızca isolated worker destekler.
</details>

#### Soru 12: Kod tamamlama (C#)
Bir function HTTP isteğiyle bir belgeyi Cosmos DB'ye yazacak. Boşluğu doldurun.

```csharp
public class Result
{
    [______(databaseName: "shop", containerName: "orders", Connection = "CosmosConnection")]
    public Order? Order { get; set; }

    [HttpResult]
    public IActionResult? Response { get; set; }
}
```

- A) `CosmosDBTrigger`
- B) `CosmosDBInput`
- C) `CosmosDBOutput`
- D) `Output`

<details><summary>Cevap</summary>

**C.** Yazma işlemi **output binding**'dir: `CosmosDBOutput`. `CosmosDBTrigger` change feed'e tepki verir; `CosmosDBInput` okur. `[HttpResult]` HTTP yanıtını işaretler.
</details>

#### Soru 13: Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** Functions'ta **trigger** olabilir?

- A) Event Grid
- B) Cosmos DB change feed
- C) Key Vault referansı
- D) `host.json`

<details><summary>Cevap</summary>

**A ve B.** Event Grid ve Cosmos DB (change feed) trigger'dır. Key Vault referansı bir app setting değeri biçimidir; `host.json` yapılandırma dosyasıdır.
</details>

#### Soru 14: Senaryo
Bir function, Service Bus kuyruğundan mesaj işliyor; kod bazen geçici bir hata nedeniyle exception fırlatıyor. Ekip, `host.json`'a function-level `retry` ilkesi (fixed delay, 5 deneme) ekledi; kuyruğun `MaxDeliveryCount` değeri de 10. Sorun: bir mesaj beklenenden çok daha fazla kez işleniyor. Neden?

- A) Retry ilkesi Service Bus trigger'larında desteklenmez, hata verir
- B) Function-level retry ile Service Bus'ın kendi teslim sayacı **birbirinin üstüne biner**: her teslimde function 5 kez denenir, ardından mesaj abandon edilip yeniden teslim edilir
- C) `MaxDeliveryCount` yalnızca topic'lerde çalışır
- D) Retry ilkesi mesajı otomatik olarak tamamlar

<details><summary>Cevap</summary>

**B.** Service Bus gibi kendi yeniden teslim mekanizması olan kaynaklarda iki retry katmanı çarpılır (5 × 10'a kadar çalışma). Çözüm: tek katman seç, function'ı **idempotent** yaz ve kalıcı hataları DLQ'ya yönlendir (→ C2.9).
</details>

#### Soru 15: Kod tamamlama (app setting)
Function app'in kimliğine, **Azure Blob/Queue** ile çalışan host depolaması (`AzureWebJobsStorage`) için secret'sız erişim verilecek. Doğru app setting adı hangisidir?

- A) `AzureWebJobsStorage__accountName`
- B) `AzureWebJobsStorage__fullyQualifiedNamespace`
- C) `AzureWebJobsStorage__accountEndpoint`
- D) `AzureWebJobsStorage:identity`

<details><summary>Cevap</summary>

**A.** Storage için kimlik tabanlı bağlantıda `<önek>__accountName` (veya `__blobServiceUri`/`__queueServiceUri`) kullanılır. `fullyQualifiedNamespace` Service Bus/Event Hubs, `accountEndpoint` Cosmos DB içindir. İlgili *Storage Blob/Queue/Table Data* rolleri de atanmalıdır.
</details>

#### Soru 16: Vaka (kısa)
**Durum:** Bir fintech, ödeme webhook'unu alan bir API'yi Functions'a taşıyor. Gereksinimler: (1) iç ağdaki özel endpoint'li bir Cosmos DB hesabına erişim, (2) gece trafik yokken ücretin sıfıra yakın olması, (3) kampanya anlarında dakikalar içinde yüzlerce instance'a ölçek, (4) kodda secret/connection string bulunmaması, (5) webhook çağrılarının yalnızca bilinen bir anahtarla yapılabilmesi.

**Soru:** Hangi seçimler gereksinimleri karşılar? (Üç doğru cevap)

- A) Flex Consumption planı
- B) Klasik Consumption planı
- C) System-assigned managed identity + `CosmosConnection__accountEndpoint` + Cosmos data-plane rolü
- D) `AuthorizationLevel.Function` ile HTTP trigger
- E) Cosmos DB anahtarını `local.settings.json`'dan Azure'a kopyalamak

<details><summary>Cevap</summary>

**A, C ve D.** Flex: VNet + scale-to-zero + hızlı ölçek (1-3). Managed identity: secret'sız bağlantı (4). Function key: bilinen anahtarla erişim (5; gerçek kimlik doğrulama gerekiyorsa imza doğrulaması/APIM eklenir). B VNet desteklemez; E secret'ı koda/ayara taşır ve `local.settings.json` dağıtılmaz.
</details>


---

## 3.2 Azure Service Bus: Kuyruk, Topic ve Dead-letter

> **Sınav Alanı:** Connect to and consume Azure services (%20-25) · **Konu Kodu:** C2.9

### 1. Ne?

**Azure Service Bus**, kurumsal sınıf, yönetilen bir **mesaj broker**'ıdır (AMQP 1.0): gönderen ile alıcıyı zaman ve hız açısından birbirinden ayırır, mesajı kalıcı saklar ve **teslim garantisi** (at-least-once) verir. İki temel desen:

| Desen | Varlık | Yapı |
|---|---|---|
| **Point-to-point** (iş kuyruğu) | **Queue** | N gönderen → 1 kuyruk → **rakip tüketiciler** (her mesajı tek tüketici işler) |
| **Publish/subscribe** | **Topic + Subscription** | N gönderen → 1 topic → her **subscription** mesajın kendi **kopyasını** alır (filtreyle) |

```text
Producer ─▶ [queue: order-processing] ─▶ Consumer A / B (competing)

Producer ─▶ [topic: orders] ─┬─ sub: high-value  (amount > 1000) ─▶ Fraud service
                             └─ sub: eu-region   (region = 'eu') ─▶ EU fulfilment
                                      ▲ her subscription: kendi DLQ'su
```

Bu konu: namespace/queue/topic/subscription ve filtreler, mesaj özellikleri, **PeekLock vs ReceiveAndDelete**, lock süresi/yenileme, **`ServiceBusProcessor`**, **dead-letter queue** ⭐, session'lar, duplicate detection, scheduled message, tier'lar ve managed identity. Functions ile tüketim → [C2.8](#31-azure-functions-serverless-api-trigger-ve-binding); olay yönlendirme farkı → [C2.10](#33-azure-event-grid-custom-event-filtre-ve-retry); mimari derinlik → M12.3.

---

### 2. Neden?

- **Sınav:** "Arka plan işlemlerini kuyrukla" hedefi doğrudan Service Bus'tır; en sık sorular: *queue mu topic mi?*, *PeekLock mı?*, *mesaj neden DLQ'ya düştü?*, *hangi tier?*, *sıra için ne?*.
- **Gerçek işte:** Anlık yük dalgalanmasını tamponlar (load leveling), servisleri gevşek bağlar, arızalı tüketici mesajı kaybetmez, **zehirli mesaj** (poison message) tüm hattı kilitlemek yerine DLQ'ya ayrılır.
- **Karşılaştırma:** Storage Queue basittir (≤ 64 KB, topic/DLQ/session yok); Event Grid **olay** dağıtır (→ [C2.10](#33-azure-event-grid-custom-event-filtre-ve-retry)); Service Bus **iş komutu/mesajı** içindir.

---

### 3. Nasıl?

#### 3.1 Namespace, queue, topic, subscription

- **Namespace:** kapsayıcı + DNS adı (`mysb.servicebus.windows.net`); tier ve ağ/kimlik ayarları burada.
- **Queue:** FIFO'ya yakın (tek tüketici ve sıralama gereksinimi için session), mesajı bir tüketici alır.
- **Topic + subscription:** gönderen yalnızca topic'e yazar; **her subscription bağımsız bir sanal kuyruktur** (kendi lock, teslim sayacı ve **DLQ'su** vardır). Aynı subscription'a bağlı tüketiciler birbiriyle rekabet eder; farklı subscription'lar aynı mesajın ayrı kopyasını görür.
- **Auto-forwarding:** bir queue/subscription'ın mesajlarını otomatik başka bir queue/topic'e iletir (zincirleme/fan-in).

#### 3.2 Subscription filtreleri ve kurallar

| Filtre | Ne? | Not |
|---|---|---|
| **SQL filter** | SQL-92 alt kümesi ifadesi: `amount > 1000 AND region = 'eu'` | Application properties ve system properties (`sys.Label`, `sys.To` …) üzerinde |
| **Correlation filter** | Belirli özellikler için **eşitlik** eşleşmesi (`CorrelationId`, `Subject`, `To`, `ContentType`, `SessionId`, özel property'ler…) | SQL'e göre daha **hızlı/ucuz** değerlendirilir |
| **Boolean (True/False)** | Hepsini al / hiçbirini alma | `1=1` ≡ TrueFilter |

> **Tuzak ⭐:** Yeni bir subscription **`$Default` kuralıyla** (TrueFilter) gelir. Kendi filtreni eklerken `$Default`'u **silmezsen** subscription **her mesajı** almaya devam eder.

Filtre **mesajın gövdesine değil**, yalnızca **property**'lerine bakar; bu yüzden filtrelenecek değeri gönderirken `ApplicationProperties`'e koy. Filtre değerlendirmesi hata verirse (ör. tip uyuşmazlığı) mesaj, subscription'da `deadLetteringOnFilterEvaluationExceptions` açıksa **DLQ**'ya düşer (varsayılan açık; doğrula). Kural ayrıca **SQL action** ile property değiştirebilir.

#### 3.3 Mesaj özellikleri

| Özellik | Amaç |
|---|---|
| `MessageId` | Uygulama tarafından atanan benzersiz kimlik; **duplicate detection** buna bakar |
| `Subject` (eski `Label`), `ContentType`, `CorrelationId`, `To`, `ReplyTo` | Yönlendirme/üst veri |
| `SessionId` | Aynı session'ın mesajlarını **sıralı** ve **tek tüketicide** işlemek için |
| `TimeToLive` | Mesajın ömrü; dolunca mesaj düşürülür **veya** DLQ'ya gider (3.5) |
| `ScheduledEnqueueTime` | İleri tarihli (scheduled) mesaj |
| `ApplicationProperties` | Özel anahtar-değer (filtre buna bakar) |
| `DeliveryCount`, `SequenceNumber`, `EnqueuedTime`, `LockedUntil` | **Broker** tarafından atanan salt-okunur alanlar |

Mesaj boyutu: Basic/Standard'da en çok **256 KB**, **Premium**'da **100 MB**'a kadar (doğrula). Büyük içeriği blob'a koyup mesajda referansını (**claim-check**) gönder.

#### 3.4 Alma modları ⭐: PeekLock vs ReceiveAndDelete

| | **PeekLock** (varsayılan) | **ReceiveAndDelete** |
|---|---|---|
| Mesaj ne zaman silinir? | Tüketici **Complete** dediğinde | **Alınır alınmaz** |
| Tüketici çökerse | Lock süresi dolar, mesaj **tekrar teslim edilir** | Mesaj **kaybolur** |
| Teslim | **At-least-once** | **At-most-once** |
| Ne zaman? | İş kaybı kabul edilemez (sipariş, ödeme) | Kayıp tolere edilir, en yüksek hız (telemetri) |

PeekLock **settlement** işlemleri:

| İşlem | Sonuç |
|---|---|
| **Complete** | Mesaj kalıcı silinir |
| **Abandon** | Lock bırakılır, `DeliveryCount++`, mesaj hemen tekrar alınabilir |
| **Dead-letter** | Mesaj **DLQ**'ya taşınır (sebep + açıklama yazılabilir) |
| **Defer** | Mesaj kuyrukta kalır ama normal akıştan çıkar; yalnızca **SequenceNumber** ile alınır |

- **Lock süresi** (`LockDuration`): varsayılan **1 dk**, en çok **5 dk**. İşlem uzunsa lock **yenilenir**: `ServiceBusProcessor` `MaxAutoLockRenewalDuration` (SDK varsayılanı 5 dk) boyunca otomatik yeniler. Lock sona ererse `Complete` **`MessageLockLost`** ile başarısız olur ve mesaj yeniden teslim edilir.
- **`DeliveryCount`:** her abandon veya lock-expiry'de artar; **`MaxDeliveryCount`** (varsayılan **10**) aşılınca mesaj otomatik **DLQ**'ya gider.
- **`ServiceBusProcessor`:** event-tabanlı tüketici; `ProcessMessageAsync` + `ProcessErrorAsync`, `MaxConcurrentCalls`, `AutoCompleteMessages` (varsayılan **true**: handler exception atmadan dönerse Complete; atarsa Abandon), `PrefetchCount`. Elle kontrol için `AutoCompleteMessages = false`.

#### 3.5 Dead-letter queue ⭐

Her queue ve **her subscription**'ın otomatik bir alt kuyruğu vardır: `<queue>/$deadletterqueue`, `<topic>/Subscriptions/<sub>/$deadletterqueue`. DLQ'ya düşme **nedenleri**:

| Neden | `DeadLetterReason` | Ne zaman? |
|---|---|---|
| **MaxDeliveryCount aşıldı** | `MaxDeliveryCountExceeded` | Mesaj ardışık abandon/lock-expiry ile eşik kadar teslim edildi (varsayılan 10); **kapatılamaz** |
| **TTL doldu** | `TTLExpiredException` | Varlıkta **dead-lettering on message expiration** açıksa (varsayılan **kapalı**; kapalıysa mesaj sessizce silinir) |
| **Filtre değerlendirme hatası** | filtre istisnası | Subscription'da ilgili ayar açıksa |
| **Uygulama kararı** | **sizin yazdığınız** reason | `DeadLetterMessageAsync(msg, reason, description)` — ör. doğrulama hatası, bozuk payload |
| Diğer sistem sebepleri | `HeaderSizeExceeded`, `MaxTransferHopCountExceeded` (auto-forward zinciri > 4) | Nadir |

Önemli noktalar:
- **Deferred** mesajlar TTL'den dolayı DLQ'ya taşınmaz.
- DLQ'daki mesajlar **kendiliğinden işlenmez** ve **süresi dolmaz**; bir DLQ mesajı tekrar DLQ'ya alınamaz. DLQ boyutu varlık kotasına dahildir — izle ve boşalt.
- DLQ'yu okumak = normal bir **receiver** + `SubQueue.DeadLetter`; mesajlar `DeadLetterReason` ve `DeadLetterErrorDescription` taşır.
- **Yeniden işleme (resubmit):** DLQ'dan oku → düzelt/aynen kopyala → **orijinal varlığa yeni mesaj olarak gönder** → DLQ mesajını Complete et (3.9).

#### 3.6 Session'lar, duplicate detection, scheduled message

| Özellik | Amaç | Not |
|---|---|---|
| **Session** | Aynı `SessionId`'li mesajlar **sıralı (FIFO)** ve **aynı alıcıda** işlenir; session state saklanabilir | Varlık oluştururken `RequiresSession = true`; **sıra gereksinimi → session** |
| **Duplicate detection** | Aynı `MessageId` zaman penceresinde (varsayılan 10 dk) ikinci kez gönderilirse yutulur | **Oluştururken** açılır, sonradan değişmez; gönderici tekrarlarına karşıdır (tüketici idempotency'sinin yerini tutmaz) |
| **Scheduled message** | Belirli zamanda görünür: `ScheduleMessageAsync` / `ScheduledEnqueueTime` | Zamanlanan mesaj iptal edilebilir (`CancelScheduledMessageAsync`) |
| **Transaction** | Birden çok işlemi (ör. complete + send) atomik yapar | Standard ve üstü |

#### 3.7 Tier'lar

| | **Basic** | **Standard** | **Premium** |
|---|---|---|---|
| Queue | Evet | Evet | Evet |
| **Topic / subscription** | **Hayır** | Evet | Evet |
| Session, duplicate detection, transaction | **Hayır** | Evet | Evet |
| Mesaj boyutu | 256 KB | 256 KB | **100 MB'a kadar** |
| Kaynak modeli | Paylaşılan, işlem başı fiyat | Paylaşılan, işlem başı fiyat | **Ayrılmış messaging unit**, öngörülebilir gecikme |
| VNet/Private Link, geo-replication | Hayır | Hayır/kısıtlı | **Evet** |

*(Tier ayrıntılarını sınavdan önce resmî tablodan doğrula.)* Basit karar: topic gerekiyorsa en az **Standard**; izolasyon/büyük mesaj/ağ gereksinimi → **Premium**.

#### 3.8 Kimlik ve bağlantı

Connection string (SAS) yerine **Microsoft Entra ID** önerilir (→ [C2.11](#41-managed-identity-ve-azure-key-vault); mekanik M12.3):

| Rol | İzin |
|---|---|
| **Azure Service Bus Data Sender** | Mesaj gönder |
| **Azure Service Bus Data Receiver** | Al / settle et |
| **Azure Service Bus Data Owner** | Hepsi + yönetim |

```csharp
// Entra-based client: no secret in code. The identity needs Data Sender/Receiver on the namespace.
await using var client = new ServiceBusClient("mysb.servicebus.windows.net", new DefaultAzureCredential());
```

Functions'ta karşılığı: `ServiceBusConnection__fullyQualifiedNamespace` app setting'i (→ [C2.8](#31-azure-functions-serverless-api-trigger-ve-binding)).

#### 3.9 Lab — emulatör ile kuyruk, topic ve DLQ

> Bu lab **Service Bus emulator** (Docker) ile **ücretsiz** çalışır. Gerçek Azure sürümü 3.9.6'da; orada Standard tier kullanılır (küçük bir ücret) ve lab sonunda kaynak grubu silinir; yine de önce **budget alarm** kur (→ C1.11).

##### 3.9.1 Emulator kurulumu

Emulator, Docker'da çalışan resmî bir imajdır (`mcr.microsoft.com/azure-messaging/servicebus-emulator`); entity'ler **yalnızca** bir `Config.json` ile tanımlanır (çalışırken yeni queue/topic oluşturulamaz) ve container yeniden başlayınca veri sıfırlanır. Emulator bir SQL bağımlılığı kullanır (sürüme göre SQL Edge/SQL Server) — güncel `docker compose` dosyası veya `LaunchEmulator` betiği için resmî *azure-service-bus-emulator-installer* deposunu esas al. Özet:

```yaml
# compose.yaml (sketch; use the official installer repo for the current version)
services:
  emulator:
    image: mcr.microsoft.com/azure-messaging/servicebus-emulator:latest
    ports: ["5672:5672"]                      # AMQP
    environment:
      ACCEPT_EULA: "Y"
      SQL_SERVER: sqlserver
      MSSQL_SA_PASSWORD: "<Strong-Passw0rd!>"
    volumes: ["./Config.json:/ServiceBus_Emulator/ConfigFiles/Config.json"]
    depends_on: [sqlserver]
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment: { ACCEPT_EULA: "Y", MSSQL_SA_PASSWORD: "<Strong-Passw0rd!>" }
```

```jsonc
// Config.json — queue + topic with two filtered subscriptions (schema may vary slightly per emulator version)
{
  "UserConfig": {
    "Namespaces": [{
      "Name": "sbemulatorns",
      "Queues": [{
        "Name": "order-processing",
        "Properties": { "MaxDeliveryCount": 3, "LockDuration": "PT30S",
                        "DefaultMessageTimeToLive": "PT1H", "DeadLetteringOnMessageExpiration": true }
      }],
      "Topics": [{
        "Name": "orders",
        "Properties": { "DefaultMessageTimeToLive": "PT1H" },
        "Subscriptions": [
          { "Name": "high-value", "Properties": { "MaxDeliveryCount": 3, "DeadLetteringOnMessageExpiration": true },
            "Rules": [{ "Name": "amount-gt-1000",
                        "Properties": { "FilterType": "Sql", "SqlFilter": { "SqlExpression": "amount > 1000" } } }] },
          { "Name": "eu-region", "Properties": { "MaxDeliveryCount": 3 },
            "Rules": [{ "Name": "region-eu",
                        "Properties": { "FilterType": "Sql", "SqlFilter": { "SqlExpression": "region = 'eu'" } } }] }
        ]
      }]
    }],
    "Logging": { "Type": "File" }
  }
}
```

Bağlantı dizesi (emulator, SAS anahtarı sabit yer tutucudur): 

```text
Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;
```

##### 3.9.2 Gönderme (queue + topic, TTL, scheduled)

```csharp
using System.Text.Json;
using Azure.Messaging.ServiceBus;

const string Conn = "Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;";
await using var client = new ServiceBusClient(Conn);

var queueSender = client.CreateSender("order-processing");
var topicSender = client.CreateSender("orders");

// 1) queue: one good message and one "poison" message
await queueSender.SendMessageAsync(new ServiceBusMessage("""{"id":"o-1","amount":50}""") { MessageId = "o-1", ContentType = "application/json" });
await queueSender.SendMessageAsync(new ServiceBusMessage("""{"id":"o-2","amount":-1}""") { MessageId = "o-2" });   // will fail in the handler

// 2) topic: filters look at application properties, NOT at the body
var big = new ServiceBusMessage("""{"id":"o-3","amount":2500}""") { MessageId = "o-3" };
big.ApplicationProperties["amount"] = 2500;
big.ApplicationProperties["region"] = "eu";
await topicSender.SendMessageAsync(big);                                  // matches high-value AND eu-region

// 3) short TTL: expires -> DLQ (dead-lettering on expiration is ON for this queue)
await queueSender.SendMessageAsync(new ServiceBusMessage("expires soon") { TimeToLive = TimeSpan.FromSeconds(10), MessageId = "o-ttl" });

// 4) scheduled: becomes visible in 30 seconds
await queueSender.ScheduleMessageAsync(new ServiceBusMessage("later") { MessageId = "o-later" }, DateTimeOffset.UtcNow.AddSeconds(30));
```

##### 3.9.3 Bilerek hata veren işleyici → MaxDeliveryCount → DLQ

```csharp
await using var processor = client.CreateProcessor("order-processing", new ServiceBusProcessorOptions
{
    AutoCompleteMessages = false,      // we settle explicitly
    MaxConcurrentCalls = 2,
    MaxAutoLockRenewalDuration = TimeSpan.FromMinutes(2),
});

processor.ProcessMessageAsync += async args =>
{
    var order = args.Message.Body.ToObjectFromJson<Order>(JsonSerializerOptions.Web);
    Console.WriteLine($"{args.Message.MessageId} delivery #{args.Message.DeliveryCount}");

    if (order.Amount < 0)
    {
        // transient/poison simulation: abandon -> redelivered until MaxDeliveryCount (3) is exceeded -> DLQ
        await args.AbandonMessageAsync(args.Message);
        return;
    }
    if (order.Id is null)
    {   // a permanent failure: dead-letter immediately with a reason instead of burning retries
        await args.DeadLetterMessageAsync(args.Message, "BadPayload", "id is missing");
        return;
    }
    await args.CompleteMessageAsync(args.Message);
};
processor.ProcessErrorAsync += args => { Console.WriteLine($"ERR {args.ErrorSource}: {args.Exception.Message}"); return Task.CompletedTask; };

await processor.StartProcessingAsync();
await Task.Delay(TimeSpan.FromSeconds(20));
await processor.StopProcessingAsync();

public record Order(string? Id, decimal Amount);
```

Beklenen çıktı (özet): `o-1 delivery #1` → tamamlanır; `o-2 delivery #1`, `#2`, `#3` → 3. abandon'dan sonra mesaj `MaxDeliveryCountExceeded` ile DLQ'ya düşer; `o-ttl` 10 sn sonra `TTLExpiredException` ile DLQ'da; `o-later` 30. saniyede görünür.

##### 3.9.4 DLQ'yu oku ve yeniden işle

```csharp
await using var dlqReceiver = client.CreateReceiver("order-processing", new ServiceBusReceiverOptions { SubQueue = SubQueue.DeadLetter });
var resubmit = client.CreateSender("order-processing");

await foreach (var dead in dlqReceiver.ReceiveMessagesAsync())          // use ReceiveMessagesAsync(maxMessages, maxWaitTime) in production loops
{
    Console.WriteLine($"{dead.MessageId}: {dead.DeadLetterReason} - {dead.DeadLetterErrorDescription}");

    if (dead.DeadLetterReason == "MaxDeliveryCountExceeded")
    {
        var fixedCopy = new ServiceBusMessage(dead);                     // copies body + properties into a NEW message
        await resubmit.SendMessageAsync(fixedCopy);                      // back to the original entity
    }
    await dlqReceiver.CompleteMessageAsync(dead);                        // remove from the DLQ
}
```

*(Topic subscription DLQ'su için: `client.CreateReceiver("orders", "high-value", new ServiceBusReceiverOptions { SubQueue = SubQueue.DeadLetter })`.)*

##### 3.9.5 Python eşdeğeri (`azure-servicebus`)

```python
from datetime import datetime, timedelta, timezone
from azure.servicebus import ServiceBusClient, ServiceBusMessage, ServiceBusSubQueue

CONN = "Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;"

with ServiceBusClient.from_connection_string(CONN) as client:
    # send: topic message with application properties (filters use these) + TTL + scheduled
    with client.get_topic_sender("orders") as sender:
        sender.send_messages(ServiceBusMessage('{"id":"o-3","amount":2500}', message_id="o-3",
                                               application_properties={"amount": 2500, "region": "eu"}))
    with client.get_queue_sender("order-processing") as sender:
        sender.send_messages(ServiceBusMessage("expires soon", time_to_live=timedelta(seconds=10)))
        sender.schedule_messages(ServiceBusMessage("later"), datetime.now(timezone.utc) + timedelta(seconds=30))

    # receive in PeekLock mode (default); abandon increments delivery_count
    with client.get_queue_receiver("order-processing", max_wait_time=5) as receiver:
        for msg in receiver:
            if b'"amount":-1' in b"".join(msg.body):
                receiver.abandon_message(msg)
            else:
                receiver.complete_message(msg)

    # read the dead-letter sub-queue and resubmit
    with client.get_queue_receiver("order-processing", sub_queue=ServiceBusSubQueue.DEAD_LETTER, max_wait_time=5) as dlq, \
         client.get_queue_sender("order-processing") as sender:
        for dead in dlq:
            print(dead.message_id, dead.dead_letter_reason, dead.dead_letter_error_description)
            sender.send_messages(ServiceBusMessage(b"".join(dead.body), message_id=dead.message_id))
            dlq.complete_message(dead)
```

> Python SDK'nın emulator bağlantı dizesini (`UseDevelopmentEmulator=true`) tanıması yeterince yeni bir `azure-servicebus` sürümü ister; takılırsan sürümü güncelle. Entra ile: `ServiceBusClient(fully_qualified_namespace, credential=DefaultAzureCredential())`.

##### 3.9.6 Gerçek Azure'da aynı yapı (az CLI) ve temizlik

```bash
RG=rg-ai200-c29; NS=sb-ai200-$RANDOM
az group create --name $RG --location westeurope
az servicebus namespace create --resource-group $RG --name $NS --sku Standard            # Basic has no topics

az servicebus queue create --resource-group $RG --namespace-name $NS --name order-processing \
  --max-delivery-count 3 --lock-duration PT30S --default-message-time-to-live PT1H \
  --enable-dead-lettering-on-message-expiration true

az servicebus topic create --resource-group $RG --namespace-name $NS --name orders
az servicebus topic subscription create --resource-group $RG --namespace-name $NS --topic-name orders \
  --name high-value --max-delivery-count 3
# remove the match-all $Default rule, then add the filter
az servicebus topic subscription rule delete --resource-group $RG --namespace-name $NS --topic-name orders \
  --subscription-name high-value --name '$Default'
az servicebus topic subscription rule create --resource-group $RG --namespace-name $NS --topic-name orders \
  --subscription-name high-value --name amount-gt-1000 --filter-sql-expression "amount > 1000"

# Entra auth for your own user (replace with the app's managed identity in real life)
az role assignment create --assignee $(az ad signed-in-user show --query id -o tsv) \
  --role "Azure Service Bus Data Owner" --scope $(az servicebus namespace show -g $RG -n $NS --query id -o tsv)

# cleanup
az group delete --name $RG --yes --no-wait
```

> **Alıştırma.** `amount-gt-1000` kuralını eklediğin hâlde `high-value` subscription'ı 50 TL'lik siparişleri de alıyor. Neden ve nasıl düzelir?
>
> <details><summary>Çözüm</summary>
>
> Subscription `$Default` (TrueFilter) kuralıyla oluşur; yeni kural **eklenir**, varsayılan kural silinmez ve iki kural **OR** ile birleşir → her mesaj geçer. `$Default` kuralını sil (`az servicebus topic subscription rule delete … --name '$Default'`).
> </details>

---

### 4. Gerçek dünya senaryosu

Bir perakende şirketinin sipariş hattı:

| Gereksinim | Karar | Neden |
|---|---|---|
| Kampanyada saniyede binlerce sipariş, ödeme servisi yavaş | **Queue** (`order-processing`) | Yük tamponu; tüketici sayısı ölçeklenir |
| Aynı sipariş hem stok, hem fraud, hem e-posta servisine gitmeli | **Topic `orders`** + 3 subscription | Her servis kendi kopyasını, kendi hızında alır |
| Yalnızca > 1000 TL siparişler fraud servisine | **SQL filter** (`amount > 1000`) + `$Default` silinmiş | Gereksiz iş yok |
| Aynı müşterinin sipariş güncellemeleri sıralı işlenmeli | **Session** (`SessionId = customerId`) | FIFO + tek alıcı |
| Çift tıklama aynı siparişi iki kez göndermesin | **Duplicate detection** (`MessageId = orderId`) | Gönderici tekrarını yutar |
| Bozuk JSON bütün hattı durdurmasın | Uygulama **dead-letter** (`BadPayload`) + DLQ izleme alarmı | Zehirli mesaj izole; operatör inceler, düzeltip yeniden gönderir |
| 24 saat içinde ödenmeyen sipariş iptal | **Scheduled message** (iptal kontrolü) | Cron'suz zamanlayıcı |
| Özel ağ + 5 MB sipariş eki | **Premium** tier (veya claim-check) | VNet/Private Link, büyük mesaj |

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → varlık/mod seçimi, "mesaj neden DLQ'ya düştü", SDK kod tamamlama (`SubQueue.DeadLetter`, `AbandonMessageAsync`), tier kısıtları.

| Senaryodaki ifade | Cevap |
|---|---|
| "Her mesaj **tek** tüketici tarafından işlensin" | **Queue** |
| "Aynı mesajı **birden çok** bağımsız tüketici alsın" | **Topic + subscription'lar** |
| "Mesaj kaybedilemez; çökmeye dayanıklı" | **PeekLock** + Complete |
| "En yüksek hız, ara sıra kayıp tolere edilir" | ReceiveAndDelete |
| "Mesaj sırayla işlenmeli" | **Session** (`SessionId`) |
| "Aynı mesaj iki kez gönderilirse yutulsun" | **Duplicate detection** (`MessageId`) |
| "Belirli saatte görünsün" | **Scheduled message** |
| "Zehirli mesaj sistemi tıkamasın" | **DLQ** + `MaxDeliveryCount` |
| "Mesaj 10 teslimden sonra kayboldu/DLQ'da" | `MaxDeliveryCountExceeded` |
| "TTL dolan mesaj DLQ'da görünmüyor" | Dead-lettering on message expiration **kapalı** |
| "Topic gerekli, en ucuz" | **Standard** (Basic'te topic yok) |
| "Subscription filtre eklendi ama her mesajı alıyor" | `$Default` kuralı silinmedi |
| "Filtre gövdedeki alana bakıyor" | Yanlış: filtre yalnızca **property**'lere bakar |
| "Secret'sız Service Bus erişimi" | Entra ID + *Data Sender/Receiver* rolü |
| "Alma kodunda DLQ" | `SubQueue = SubQueue.DeadLetter` |

**Tuzaklar:**
- **Storage Queue vs Service Bus:** topic, DLQ, session, duplicate detection, transaction, >64 KB → Service Bus.
- `Abandon` mesajı DLQ'ya **atmaz**; sayacı artırır — DLQ'ya düşüş eşik aşılınca olur. Hemen DLQ için **DeadLetter** işlemi.
- Lock süresi işlemden kısa ve yenileme yoksa **`MessageLockLost`** + gereksiz tekrar teslim → DLQ.
- DLQ mesajları **kendiliğinden yeniden işlenmez**; birisi okuyup resubmit etmelidir.
- Duplicate detection **oluştururken** seçilir; sonradan açılamaz.
- SDK'da `ServiceBusClient` ve sender/processor nesneleri **uzun ömürlü** (singleton) tutulur.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Her mesajda yeni `ServiceBusClient` açmak | Singleton client/sender; bağlantı (AMQP) pahalı |
| `AutoCompleteMessages=true` iken handler'da hatayı yutmak | Hata yutulursa mesaj **Complete** olur; ya yeniden fırlat ya da bilinçli dead-letter et |
| Kalıcı hatayı sonsuz abandon ile denemek | Doğrulama/bozuk payload → **hemen dead-letter**; geçici hata → abandon + backoff |
| DLQ'yu izlememek | DLQ derinliği için Azure Monitor alarmı; boşaltma/resubmit süreci |
| İdempotent olmayan tüketici | At-least-once → aynı mesaj ikinci kez gelebilir |
| Filtre için gövdeyi parse etmeyi beklemek | Filtreleyeceğin değeri `ApplicationProperties`'e koy |
| `$Default` kuralını silmemek | Silmeden özel kural = hepsini almak |
| Büyük payload'ı doğrudan gönderip 256 KB'a çarpmak | Claim-check (blob) veya Premium |
| Sıra gerektiren akışta rakip tüketici kullanmak | Session |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| PeekLock → ReceiveAndDelete | Daha az ağ gidiş-dönüşü | Kayıp riski (at-most-once) |
| `PrefetchCount` ↑ | Yüksek verim | Prefetch edilen mesajların lock'u çalışmadan dolabilir → tekrar teslim |
| `MaxConcurrentCalls` ↑ | Paralellik | Aşağı akış baskısı; sıra garantisi azalır (sıra için session) |
| Session | Sıralı işleme | Paralellik session sayısıyla sınırlı; "sıcak" session darboğaz |
| Duplicate detection | Gönderici tekrarını eler | Bellek/penceresi; yalnızca pencere içinde |
| Correlation filter (SQL yerine) | Daha ucuz değerlendirme | Yalnızca eşitlik |
| Standard → Premium | İzolasyon, büyük mesaj, VNet, öngörülebilir gecikme | Messaging unit başına sabit maliyet |
| `MaxDeliveryCount` düşük | Zehirli mesaj hızla izole | Geçici hata yaşayan mesajlar erken DLQ'ya gider (backoff yok) |

---

### 8. Kaynaklar

- [AI-200 study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Service Bus messaging overview](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview) · [Queues, topics, subscriptions](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-queues-topics-subscriptions)
- [Dead-letter queues](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dead-letter-queues) · [Message transfers, locks, settlement](https://learn.microsoft.com/en-us/azure/service-bus-messaging/message-transfers-locks-settlement)
- [Topic filters and actions](https://learn.microsoft.com/en-us/azure/service-bus-messaging/topic-filters) · [Message sessions](https://learn.microsoft.com/en-us/azure/service-bus-messaging/message-sessions) · [Duplicate detection](https://learn.microsoft.com/en-us/azure/service-bus-messaging/duplicate-detection)
- [Service Bus tiers / Premium](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-premium-messaging) · [Quotas](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-quotas)
- [Service Bus emulator](https://learn.microsoft.com/en-us/azure/service-bus-messaging/test-locally-with-service-bus-emulator) · [azure-service-bus-emulator-installer](https://github.com/Azure/azure-service-bus-emulator-installer)
- [Azure.Messaging.ServiceBus (.NET)](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/messaging.servicebus-readme) · [azure-servicebus (Python)](https://learn.microsoft.com/en-us/python/api/overview/azure/servicebus-readme)


### 📝 Bölüm 3.2 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir sipariş sistemi her sipariş olayını **stok**, **fraud** ve **bildirim** servislerinin hepsine, her servisin kendi hızında işleyeceği şekilde iletmek istiyor. En uygun Service Bus yapısı hangisidir?

- A) Tek bir queue ve üç rakip tüketici
- B) Bir topic ve her servis için ayrı bir subscription
- C) Üç ayrı queue'ya aynı mesajı elle üç kez gönderen bir queue
- D) Basic tier'da bir queue

<details><summary>Cevap</summary>

**B.** Topic'te her subscription mesajın **kendi kopyasını** alır; tüketiciler bağımsızdır. A'da her mesajı yalnızca bir tüketici işler (rakip tüketici); C gönderene gereksiz iş yükler; D'de zaten topic yoktur.
</details>

#### Soru 2: Tek seçim
Bir queue'dan PeekLock modunda alınan mesajın işlenmesi sırasında tüketici süreci çöküyor. Mesaja ne olur?

- A) Kalıcı olarak kaybolur
- B) Lock süresi dolunca kuyrukta yeniden görünür ve tekrar teslim edilir (`DeliveryCount` artar)
- C) Hemen dead-letter queue'ya taşınır
- D) Deferred duruma geçer

<details><summary>Cevap</summary>

**B.** PeekLock at-least-once sağlar: Complete edilmeyen mesaj lock dolunca yeniden teslim edilir. Kayıp (A) ReceiveAndDelete'in riskidir. DLQ'ya ancak `MaxDeliveryCount` aşılırsa veya uygulama açıkça dead-letter ederse gidilir.
</details>

#### Soru 3: Çoklu seçim (iki doğru cevap)
Bir mesajın bir queue'nun **dead-letter queue**'suna düşmesine **hangi ikisi** neden olabilir?

- A) `DeliveryCount` değerinin `MaxDeliveryCount`'u aşması
- B) Tüketicinin mesajı `Complete` etmesi
- C) Uygulamanın `DeadLetterMessageAsync` çağırması
- D) Mesajın `Peek` ile okunması

<details><summary>Cevap</summary>

**A ve C.** Eşik aşımı (`MaxDeliveryCountExceeded`) ve uygulama kararı DLQ nedenleridir. Complete mesajı siler; Peek mesajı kilitlemez/taşımaz.
</details>

#### Soru 4: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Basic tier'da topic ve subscription oluşturulabilir. | ☐ | ☐ |
| b) Her subscription'ın kendi dead-letter queue'su vardır. | ☐ | ☐ |
| c) DLQ'daki mesajlar `MaxDeliveryCount` sonrasında otomatik olarak orijinal kuyruğa geri taşınır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** Topic için en az Standard gerekir. DLQ her queue ve subscription için ayrı bir alt kuyruktur. DLQ mesajları kendiliğinden işlenmez; birinin okuyup yeniden göndermesi gerekir.
</details>

#### Soru 5: Kod tamamlama (C#)
Aşağıdaki kod, `order-processing` kuyruğunun **dead-letter** alt kuyruğundan okumalıdır. Boşluğu doldurun.

```csharp
var receiver = client.CreateReceiver("order-processing",
    new ServiceBusReceiverOptions { ______ = SubQueue.DeadLetter });
```

- A) `ReceiveMode`
- B) `SubQueue`
- C) `PrefetchCount`
- D) `Identifier`

<details><summary>Cevap</summary>

**B.** Alt kuyruk seçimi `ServiceBusReceiverOptions.SubQueue` ile yapılır. `ReceiveMode` PeekLock/ReceiveAndDelete'i belirler.
</details>

#### Soru 6: Senaryo
Bir queue'da `MaxDeliveryCount = 3` ve işleyici, ödeme servisi geçici olarak kapalıyken her çağrıda `AbandonMessageAsync` yapıyor. Kuyrukta bekleyen **geçerli** bir mesaj kısa sürede kayboluyor (hedef kuyrukta yok). Mesaj nerede aranmalıdır ve neden?

- A) Silinmiştir; Abandon mesajı siler
- B) DLQ'da, `MaxDeliveryCountExceeded` nedeniyle
- C) Deferred kuyruğunda
- D) Scheduled mesajlar listesinde

<details><summary>Cevap</summary>

**B.** Her abandon `DeliveryCount`'u artırır; üçüncü başarısız teslimden sonra eşik aşılır ve mesaj DLQ'ya taşınır. Mesaj **kaybolmadı**; operatör DLQ'dan okuyup yeniden gönderebilir. Çözüm: eşiği ve backoff stratejisini geçici hatalara göre ayarlamak, DLQ alarmı kurmak.
</details>

#### Soru 7: Tek seçim
`high-value` subscription'ında `amount > 1000` SQL filtre kuralını ekledin ama subscription hâlâ 50 TL'lik siparişleri de alıyor. En olası neden?

- A) SQL filtreler mesajın gövdesini (body) değerlendirir ve JSON'u desteklemez
- B) Varsayılan `$Default` (TrueFilter) kuralı silinmedi
- C) Subscription'ın `MaxDeliveryCount` değeri çok düşük
- D) Topic partitioned olduğu için filtre çalışmıyor

<details><summary>Cevap</summary>

**B.** Yeni subscription `$Default` kuralıyla gelir ve kurallar OR ile değerlendirilir. A yanlıştır: filtreler zaten gövdeye değil yalnızca **property**'lere bakar (`amount` gönderirken `ApplicationProperties`'e eklenmelidir). `MaxDeliveryCount` (C) ve topic bölümleme (D) filtre eşleşmesini etkilemez.
</details>

#### Soru 8: Kod tamamlama (az CLI)
`orders` topic'indeki `high-value` subscription'ının varsayılan kuralını silin.

```bash
az servicebus topic subscription rule ______ --resource-group rg --namespace-name sb \
  --topic-name orders --subscription-name high-value --name '$Default'
```

- A) `remove-all`
- B) `delete`
- C) `disable`
- D) `reset`

<details><summary>Cevap</summary>

**B.** `az servicebus topic subscription rule delete`. Ardından `rule create --filter-sql-expression "amount > 1000"` ile kendi kuralın eklenir.
</details>

#### Soru 9: Çoklu seçim (iki doğru cevap)
Müşteri başına sipariş güncellemelerinin **sıralı** ve aynı anda **tek alıcıda** işlenmesi gerekiyor. **Hangi ikisi** gerekir?

- A) Gönderirken `SessionId = customerId`
- B) Varlığın `RequiresSession = true` ile oluşturulması
- C) Basic tier
- D) ReceiveAndDelete modu

<details><summary>Cevap</summary>

**A ve B.** Sıra ve tek-alıcı garantisi session ile sağlanır; session gerektiren varlıkta `SessionId`'siz mesaj gönderilemez. Basic'te session yoktur; alma modu sıra garantisi vermez.
</details>

#### Soru 10: Sıralama
Bir mesajın başarısız işlenip DLQ'dan kurtarılma akışını sıraya koyun: **(1)** Operatör DLQ'dan okuyup mesajı yeni mesaj olarak orijinal kuyruğa gönderir · **(2)** İşleyici mesajı `MaxDeliveryCount` kez abandon eder · **(3)** Mesaj `MaxDeliveryCountExceeded` ile DLQ'ya taşınır · **(4)** DLQ'daki orijinal mesaj Complete edilir.

<details><summary>Cevap</summary>

**2 → 3 → 1 → 4.** Eşik aşılınca broker mesajı DLQ'ya taşır; operatör (veya bir araç) DLQ'dan okuyup yeniden gönderir; ardından DLQ kopyası Complete edilerek temizlenir.
</details>

#### Soru 11: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) `ServiceBusProcessor`'da `AutoCompleteMessages = true` iken handler exception fırlatırsa mesaj abandon edilir. | ☐ | ☐ |
| b) `AutoCompleteMessages = true` iken handler'da exception'ı yutup sessizce dönersen mesaj Complete edilir. | ☐ | ☐ |
| c) Lock süresi dolduktan sonra `CompleteMessageAsync` çağrısı başarılı olur ve mesaj silinir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Otomatik modda başarılı dönüş Complete, exception Abandon demektir; hatayı yutmak mesajı kaybettirir. Lock sona erdiyse Complete `MessageLockLost` ile başarısız olur ve mesaj yeniden teslim edilir.
</details>

#### Soru 12: Tek seçim
Bir mesajın `TimeToLive` süresi doldu, ancak kuyruğun DLQ'sunda görünmüyor. Mesaj ne olmuştur?

- A) Kuyruğa hiç yazılmamıştır
- B) Kuyrukta **dead-lettering on message expiration** kapalıdır; mesaj sessizce silinmiştir
- C) Deferred kuyruğa taşınmıştır
- D) `MaxDeliveryCount` aşıldığı için session'a bağlanmıştır

<details><summary>Cevap</summary>

**B.** TTL dolan mesaj yalnızca varlıkta bu özellik açıksa `TTLExpiredException` ile DLQ'ya gider; kapalıysa (varsayılan) düşürülür. Deferred mesajlar TTL nedeniyle DLQ'ya taşınmaz — C yanlıştır.
</details>

#### Soru 13: Kod tamamlama (C#)
Bir işleyici, payload'ı bozuk mesajı tekrar denemeden **hemen** DLQ'ya göndermelidir. Boşluğu doldurun.

```csharp
await args.______(args.Message, "BadPayload", "id is missing");
```

- A) `AbandonMessageAsync`
- B) `DeferMessageAsync`
- C) `DeadLetterMessageAsync`
- D) `CompleteMessageAsync`

<details><summary>Cevap</summary>

**C.** Kalıcı hata (poison/validation) → doğrudan dead-letter; sebep ve açıklama `DeadLetterReason` / `DeadLetterErrorDescription` olarak mesajla saklanır. Abandon geçici hata içindir ve eşik dolana kadar tekrar dener.
</details>

#### Soru 14: Kod tamamlama (Python)
Python'da bir queue'nun DLQ'sundan okumak için boşluğu doldurun.

```python
receiver = client.get_queue_receiver("order-processing", ______=ServiceBusSubQueue.DEAD_LETTER)
```

- A) `sub_queue`
- B) `receive_mode`
- C) `session_id`
- D) `prefetch_count`

<details><summary>Cevap</summary>

**A.** `sub_queue=ServiceBusSubQueue.DEAD_LETTER`. Mesajlarda `dead_letter_reason` ve `dead_letter_error_description` alanları bulunur.
</details>

#### Soru 15: Tek seçim
Function app, Service Bus trigger'ı **secret'sız** kullanacak ve yalnızca mesaj alıp settle edecek. Minimum doğru yapılandırma hangisidir?

- A) Connection string içeren app setting
- B) `<önek>__fullyQualifiedNamespace` app setting'i + kimliğe *Azure Service Bus Data Receiver* rolü
- C) `<önek>__fullyQualifiedNamespace` + kimliğe *Azure Service Bus Data Sender* rolü
- D) `<önek>__accountEndpoint` + *Cosmos DB Built-in Data Reader*

<details><summary>Cevap</summary>

**B.** Trigger alma ve settle için *Data Receiver* gerekir (en az yetki). Sender yalnızca göndermek içindir (C); `accountEndpoint` Cosmos DB bağlantı ayarıdır (D).
</details>

#### Soru 16: Tek seçim
Bir uygulama, **100 MB** büyüklüğünde mesajları sanal ağ içinde özel endpoint üzerinden iletmek ve öngörülebilir gecikme istiyor. Hangi tier?

- A) Basic
- B) Standard
- C) Premium
- D) Hepsi aynı

<details><summary>Cevap</summary>

**C.** Büyük mesaj (100 MB'a kadar), ayrılmış kaynaklar ve ağ izolasyonu (Private Link/VNet) Premium'a özgüdür. Basic/Standard'da mesaj ~256 KB ile sınırlıdır (limitleri sınavdan önce doğrula).
</details>

#### Soru 17: Vaka (kısa)
**Durum:** Bir lojistik şirketi, kargo olaylarını `shipments` topic'ine gönderiyor. `customs` subscription'ı yalnızca `destinationCountry = 'GB'` olan olayları işleyecek (SQL filtre), işleyici bir bağımlı API kapalıyken sürekli abandon yapıyor, ekip ise dün gece 4.000 olayın "kaybolduğunu" bildirdi. Ayrıca `customs`'a, gönderirken **gövdeye** `destinationCountry` yazılmış olaylar da ulaşmıyor.

**Soru:** Hangi üç bulgu/çözüm doğrudur?

- A) Kayıp sanılan olaylar büyük olasılıkla `customs` subscription'ının DLQ'sunda `MaxDeliveryCountExceeded` ile bekliyordur
- B) Filtrenin çalışması için `destinationCountry` mesajın **ApplicationProperties**'ine eklenmelidir; filtre gövdeye bakmaz
- C) DLQ derinliği için alarm kurmak ve DLQ'dan resubmit süreci tanımlamak gerekir
- D) Çözüm olarak ReceiveAndDelete moduna geçmek mesajların kaybolmasını önler
- E) Topic'i Basic tier'a taşımak DLQ sorununu giderir

<details><summary>Cevap</summary>

**A, B ve C.** Abandon'lanan mesajlar eşik aşılınca subscription DLQ'sunda birikir (kayıp değil); filtreler property tabanlıdır; operasyonel olarak DLQ izlenmeli ve yeniden işleme süreci olmalıdır. D tam tersine kayıp riskini artırır (at-most-once); E yanlıştır (Basic'te topic yok).
</details>


---

## 3.3 Azure Event Grid: Custom Event, Filtre ve Retry

> **Sınav Alanı:** Connect to and consume Azure services (%20-25) · **Konu Kodu:** C2.10

### 1. Ne?

**Azure Event Grid**, tamamen yönetilen bir **olay yönlendirme (event routing)** servisidir: bir kaynakta *bir şey oldu* bilgisini (olay) alır ve ilgilenen **handler**'lara dağıtır. Olay bir **bildirimdir** ("`Order.Placed` oldu"); bir iş komutu ya da veri taşıma aracı değildir.

```text
 Event source            Topic                 Event subscription (filtre + retry + DLQ)      Event handler
 ───────────────       ──────────           ───────────────────────────────────────────      ─────────────
 Blob Storage    ─▶  [system topic]   ─┐
 Resource Group  ─▶  [system topic]   ─┼─▶ sub A: subject begins with /blobServices/...  ─▶  Azure Function
 Your application ─▶ [custom topic]   ─┤    sub B: data.amount > 1000                    ─▶  Service Bus queue
 SaaS (partner)  ─▶  [partner topic]  ─┘    sub C: type = Order.Cancelled                ─▶  Webhook / Storage queue
```

| Kavram | Anlamı |
|---|---|
| **Event source** | Olayı üreten: Azure servisi (Blob, Resource Group, Key Vault…), kendi uygulaman, partner SaaS |
| **Topic** | Olayların gönderildiği uç nokta. Türler: **system topic** (Azure servisi olayları; kaynak sahibi servistir), **custom topic** (kendi uygulama olayların), **partner topic** (üçüncü taraf SaaS), **domain** (çok kiracılı/çok topic'li yönetim birimi) |
| **Event subscription** | "Bu topic'ten **şu filtreye** uyan olayları **şu handler**'a, **şu retry/dead-letter ayarlarıyla** ilet" |
| **Event handler** | Webhook, **Azure Functions**, **Service Bus** queue/topic, **Storage Queue**, Event Hubs, Hybrid Connections… |
| **Event şeması** | **Event Grid şeması** veya **CloudEvents v1.0** (açık standart) |
| **Namespace topic'leri** | Yeni model: bir **Event Grid namespace** içinde topic'ler; **CloudEvents** şeması, **push** *ve* **pull** teslim (queue subscription), MQTT broker. Azure servis/partner olaylarını **tüketmez** (yalnızca kendi uygulama olayların) — desteklenen handler kümesi klasik topic'lerden farklıdır |

Laboratuvar: custom topic'e **`amount > 1000`** siparişleri filtreleyip Function handler'a yönlendirme, handler'ı bilerek hata verdirip **retry** ve **dead-letter blob**'unu gözlemleme (3.8). Mesaj/iş komutu tarafı → [C2.9](#32-azure-service-bus-kuyruk-topic-ve-dead-letter); handler olarak Function → [C2.8](#31-azure-functions-serverless-api-trigger-ve-binding); event-based blob trigger (Event Grid kaynaklı) → C2.8.

---

### 2. Neden?

- **Sınav:** Filtre türü seçimi, retry/TTL/dead-letter ayarı ve "Event Grid mi Service Bus mı?" kararı en sık gelen üç soru tipidir.
- **Gerçek işte:** Polling yerine **push** ile tepki: blob yüklendi → thumbnail üret; sipariş verildi → muhasebe/stok/bildirim; kaynak oluşturuldu → etiket denetimi. Yayıncı, kimlerin dinlediğini bilmez (gevşek bağlılık); yeni dinleyici = yeni subscription, yayıncıda değişiklik yok.
- **Maliyet:** Olay başına fiyatlanır, kullanılmadığında maliyet yok; serverless Function ile birleşince "olay olmadığında sıfır" mimarisi.

---

### 3. Nasıl?

#### 3.1 Olay şemaları: Event Grid şeması vs CloudEvents

```jsonc
// Event Grid schema
{ "id": "o-1001", "topic": "/subscriptions/.../topics/orders", "subject": "orders/o-1001",
  "eventType": "Contoso.Order.Placed", "eventTime": "2026-10-26T09:00:00Z",
  "data": { "orderId": "o-1001", "amount": 1500 }, "dataVersion": "1.0", "metadataVersion": "1" }

// CloudEvents v1.0 (JSON)
{ "specversion": "1.0", "id": "o-1001", "source": "/orders/api", "type": "Contoso.Order.Placed",
  "subject": "orders/o-1001", "time": "2026-10-26T09:00:00Z", "datacontenttype": "application/json",
  "data": { "orderId": "o-1001", "amount": 1500 } }
```

| | Event Grid şeması | CloudEvents v1.0 |
|---|---|---|
| Tür alanı | `eventType` | `type` |
| Kaynak alanı | `topic` (servis atar) | `source` (yayıncı verir, **zorunlu**) |
| Standart mı? | Azure'a özgü | **CNCF standardı**, taşınabilir |
| Namespace topic'leri | Desteklenmez | **Tek** desteklenen şema |

- **Input schema** topic oluştururken seçilir (`--input-schema`); sonradan değiştirilemez. **Delivery schema** her subscription'da ayrı seçilir (`--event-delivery-schema`). **CloudEvents girişli topic'ten Event Grid şemasıyla teslim yapılamaz**.
- Bir olayın üst sınırı **1 MB**'dır (yayın isteği toplamı; 64 KB'lık dilimlerle faturalanır — doğrula). Büyük içeriği blob'a koy, olayda **referans** taşı.

#### 3.2 Custom event yayınlama ⭐

`EventGridPublisherClient` (paket `Azure.Messaging.EventGrid`) custom topic endpoint'ine (`https://<topic>.<region>-1.eventgrid.azure.net/api/events`) yazar. Kimlik doğrulama yolları:

| Yol | Kullanım | Not |
|---|---|---|
| **Entra ID** (`DefaultAzureCredential`) | **Önerilen** | Kimliğe **EventGrid Data Sender** rolü |
| **Access key** (`AzureKeyCredential`) | Hızlı deneme | `az eventgrid topic key list`; secret yönetimi gerekir |
| **SAS token** | Kısa ömürlü, yetkisi daraltılmış paylaşım | `aeg-sas-token` header'ı |

```csharp
using Azure.Identity;
using Azure.Messaging;
using Azure.Messaging.EventGrid;

var client = new EventGridPublisherClient(new Uri(topicEndpoint), new DefaultAzureCredential());

// CloudEvents input schema topic
var evt = new CloudEvent(source: "/orders/api", type: "Contoso.Order.Placed",
                         jsonSerializableData: new { orderId = "o-1001", amount = 1500m, region = "eu" })
{
    Subject = "orders/o-1001",
};
await client.SendEventAsync(evt);            // SendEventsAsync(IEnumerable<CloudEvent>) to batch
```

*(Event Grid şemalı topic için `EventGridEvent("orders/o-1001", "Contoso.Order.Placed", "1.0", data)` kullanılır; topic'in şeması ile gönderilen olay biçimi **eşleşmelidir**.)*

#### 3.3 Filtreleme ⭐

Filtreler **event subscription**'a yazılır; uymayan olay o subscription'a **hiç teslim edilmez** (diğer subscription'ları etkilemez).

| Filtre | Ne yapar? | Ne zaman? |
|---|---|---|
| **Event type** (`--included-event-types`) | Yalnızca listelenen `eventType`/`type` değerleri | Varsayılan hepsi (`All`) |
| **Subject begins with / ends with** | `subject` alanında **önek/sonek** eşleşmesi (varsayılan **büyük/küçük harf duyarsız**; `--subject-case-sensitive`) | Yol benzeri hiyerarşi: `/blobServices/default/containers/images/`, `.jpg` uzantısı |
| **Advanced filter** | `data.*` dahil herhangi bir alanda **operatör** tabanlı koşul | Payload'a göre yönlendirme (`data.amount`, `data.region`) |

**Advanced filter operatörleri:** sayı: `NumberIn`, `NumberNotIn`, `NumberLessThan`, `NumberGreaterThan`, `…OrEquals`, `NumberInRange`, `NumberNotInRange` · metin: `StringIn`, `StringNotIn`, `StringContains`, `StringBeginsWith`, `StringEndsWith` (+ `Not…`) · `BoolEquals` · `IsNullOrUndefined`, `IsNotNull`.

| Kural | Ayrıntı |
|---|---|
| **Anahtar (key)** | Event Grid şemasında `subject`, `eventType`, `id`, `data.<yol>`; CloudEvents'te `subject`, `type`, `source`, `id`, `data.<yol>` |
| **Birleştirme** | Birden çok advanced filter **AND**; aynı filtredeki değer listesi (`StringIn a,b`) **OR** |
| **Kota** | Subscription başına sınırlı sayıda advanced filter/değer vardır (ör. 25 filtre; doğrula) |
| **Tuzak** | Filtre **gövdenin tamamına** değil `data` altındaki alanlara/envelope'a bakar; string sayıyı `NumberGreaterThan` ile karşılaştırmaz — tip uyumu gerekir |

#### 3.4 Teslim ve dayanıklılık ⭐

Event Grid **at-least-once** teslim eder: aynı olay **birden fazla** gelebilir ve **sıra garanti edilmez** → handler **idempotent** olmalı (olay `id` ile tekilleştir).

| Konu | Davranış |
|---|---|
| **Başarı** | Handler **2xx** döner (200/201/202/203/204) → olay teslim edildi sayılır |
| **Retry** | Başarısız (zaman aşımı, 5xx, 429, 408…) teslimler **exponential backoff** ile tekrar denenir; aralıklar kabaca 10 sn, 30 sn, 1 dk, 5 dk, 10 dk, 30 dk, 1 saat, sonra saatlik — **yapılandırılamaz**, rastgele sapma (jitter) eklenir |
| **Retry policy (ayarlanabilir)** | **Max delivery attempts** (klasik topic varsayılan **30**, en çok 30) ve **Event time-to-live** (varsayılan **1440 dk = 24 sa**, en çok 1440). İkisi birlikte verilirse **önce dolan** işi bitirir |
| **Kalıcı hatalar** | **400 Bad Request**, **413 Payload Too Large** gibi "asla başarılı olmayacak" yanıtlarda olay **retry edilmeden** doğrudan dead-letter'a gönderilir (diğer kalıcı kodlar handler türüne göre değişir; doğrula) |
| **Dead-letter** | **Varsayılan kapalı**. Subscription oluştururken bir **Storage hesabı/blob container'ı** verilirse, teslim edilemeyen/süresi dolan olaylar oraya **blob** olarak yazılır (son denemeden **~5 dk** sonra). Dead-letter ayarlı değilse olay **sessizce düşer** |
| **Batch teslim** | Webhook/Function için `maxEventsPerBatch` ve `preferredBatchSizeInKilobytes` ile toplu teslim |
| **Webhook doğrulama** | Webhook endpoint'i subscription oluşturulurken **handshake** ile sahipliğini kanıtlar: Event Grid şemasında `SubscriptionValidationEvent` içindeki `validationCode`'u `{"validationResponse": "<code>"}` olarak döner (veya `validationUrl`'e GET); CloudEvents v1.0'da **OPTIONS** isteğine `WebHook-Allowed-Origin` yanıtı. **Azure Functions, Service Bus, Storage Queue** gibi Azure hedeflerinde handshake **otomatik**dir |

> Namespace topic'lerinde varsayılanlar farklıdır (ör. max delivery count **10**, olay saklama süresi en çok **7 gün**); klasik topic değerlerini oraya taşıma.

#### 3.5 Event Grid vs Service Bus vs Event Hubs ⭐

| | **Event Grid** | **Service Bus** | **Event Hubs** |
|---|---|---|---|
| Mesajın doğası | **Olay** (oldu bilgisi) | **Mesaj/iş komutu** (yapılması gereken iş) | **Telemetri akışı** |
| Teslim | **Push** (klasik: handler'a it; namespace: push + pull) | **Pull** (alıcı çeker, lock/settle) | Pull (offset ile okuma) |
| Kalıcılık / işleme garantisi | Retry + (isteğe bağlı) dead-letter; **kuyruk değildir** | **Kuyruk**, PeekLock, DLQ, TTL | Saklama süresi boyunca log |
| Sıralama, session, transaction | Yok | **Var** | Partition içinde sıra |
| Kimler tüketir? | Çok sayıda bağımsız abone (fan-out) | Queue: biri; topic: abone başına kopya | Consumer group'lar |
| Tipik örnek | "Blob yüklendi", "Order.Placed" bildirimi | "Bu siparişi işle", ödeme komutu | Log/IoT telemetri, saniyede milyonlarca olay |

**Birlikte kullanım:** Event Grid → **Service Bus queue** handler'ı ("olay geldi → işi dayanıklı kuyruğa koy → tüketici kendi hızında işlesin"). Bu, "Event Grid'i kuyruk gibi kullanma" hatasının doğru çözümüdür.

#### 3.6 Handler olarak Azure Function

```csharp
// Isolated worker, Microsoft.Azure.Functions.Worker.Extensions.EventGrid
public class OrderEventHandlers(ILogger<OrderEventHandlers> logger)
{
    public record OrderPlaced(string OrderId, decimal Amount, bool SimulateFailure = false);

    [Function("HighValueOrderHandler")]
    public void Run([EventGridTrigger] CloudEvent cloudEvent)              // use EventGridEvent for the Event Grid schema
    {
        var order = cloudEvent.Data!.ToObjectFromJson<OrderPlaced>(JsonSerializerOptions.Web);
        logger.LogInformation("Event {Id} type={Type} order={OrderId} amount={Amount}",
            cloudEvent.Id, cloudEvent.Type, order!.OrderId, order.Amount);

        if (order.SimulateFailure)
            throw new InvalidOperationException("Simulated handler failure");   // => HTTP 500 => Event Grid retries
    }
}
```

Function bir **trigger**'dır (→ C2.8); Event Grid subscription'ı Function'a `azurefunction` endpoint tipiyle bağlanır, doğrulama otomatiktir. Function içindeki hata Event Grid'e 5xx olarak döner ve **Event Grid retry**'ı devreye girer. (Service Bus handler'ında ise retry **Service Bus**'a devredilir, Event Grid için teslim başarılıdır.)

#### 3.7 System topic vs custom topic

| | **System topic** | **Custom topic** |
|---|---|---|
| Olayı kim üretir? | Azure servisi (Storage, Resource Group, Key Vault, App Configuration…) | **Senin uygulaman** (`EventGridPublisherClient` ile) |
| Yayın yapılır mı? | Hayır, servis kendisi yayınlar | Evet; endpoint + kimlik doğrulama |
| Olay türleri | Servise göre sabit (`Microsoft.Storage.BlobCreated`) | Sana ait (`Contoso.Order.Placed`) |
| Örnek | "Blob oluştu → Function" | "Sipariş verildi → muhasebe" |

#### 3.8 Lab — custom event, advanced filter, retry ve dead-letter

> Event Grid için resmî **local emulator yoktur**; lab Azure'da çalışır (olay başına mikro fiyat, ilk aylık olay grubu ücretsiz — güncel fiyat için doğrula). Önce **budget alarm** kur (→ C1.11). Function app için [C2.8](#31-azure-functions-serverless-api-trigger-ve-binding) laboratuvarındaki aynı komutları kullan; aşağıda kısaltılmıştır. Yerelde handler testi: `func start` ardından
> `curl -X POST "http://localhost:7071/runtime/webhooks/EventGrid?functionName=HighValueOrderHandler" -H "aeg-event-type: Notification" -H "Content-Type: application/cloudevents+json" -d @event.json`.

```bash
RG=rg-ai200-c210; LOC=westeurope
TOPIC=evgt-orders-$RANDOM; ST=stc210$RANDOM; APP=func-ai200-c210-$RANDOM
az group create --name $RG --location $LOC
az storage account create --name $ST --resource-group $RG --location $LOC --sku Standard_LRS
az storage container create --account-name $ST --name deadletter --auth-mode login

# 1) custom topic with the CloudEvents input schema
az eventgrid topic create --name $TOPIC --resource-group $RG --location $LOC --input-schema cloudeventschemav1_0
TOPIC_ID=$(az eventgrid topic show --name $TOPIC --resource-group $RG --query id -o tsv)
ENDPOINT=$(az eventgrid topic show --name $TOPIC --resource-group $RG --query endpoint -o tsv)

# 2) function app + handler (create as in C2.8: az functionapp create ... --flexconsumption-location; publish HighValueOrderHandler)
FUNC_ID=$(az functionapp function show --name $APP --resource-group $RG --function-name HighValueOrderHandler --query id -o tsv)

# 3) subscription: only amount > 1000, 3 attempts, 5 minute TTL, dead-letter blob
ST_ID=$(az storage account show --name $ST --resource-group $RG --query id -o tsv)
az eventgrid event-subscription create --name high-value-orders --source-resource-id $TOPIC_ID \
  --endpoint-type azurefunction --endpoint $FUNC_ID \
  --event-delivery-schema cloudeventschemav1_0 \
  --included-event-types Contoso.Order.Placed \
  --advanced-filter data.amount NumberGreaterThan 1000 \
  --max-delivery-attempts 3 --event-ttl 5 \
  --deadletter-endpoint "$ST_ID/blobServices/default/containers/deadletter"

# 4) let your user publish with Entra (no key)
az role assignment create --assignee $(az ad signed-in-user show --query id -o tsv) \
  --role "EventGrid Data Sender" --scope $TOPIC_ID
echo $ENDPOINT
```

**Yayıncı (C#)** — üç olay: biri filtreye takılır (`amount = 300`), biri başarıyla işlenir (`1500`), biri bilerek hata verir (`2500`, `SimulateFailure = true`):

```csharp
var client = new EventGridPublisherClient(new Uri(endpoint), new DefaultAzureCredential());
await client.SendEventsAsync(new[]
{
    new CloudEvent("/orders/api", "Contoso.Order.Placed", new { orderId = "o-1", amount = 300m }) { Subject = "orders/o-1" },
    new CloudEvent("/orders/api", "Contoso.Order.Placed", new { orderId = "o-2", amount = 1500m }) { Subject = "orders/o-2" },
    new CloudEvent("/orders/api", "Contoso.Order.Placed", new { orderId = "o-3", amount = 2500m, simulateFailure = true }) { Subject = "orders/o-3" },
});
```

**Gözlem:**
1. `o-1` handler'a **hiç ulaşmaz** (filtre: `300 > 1000` değil).
2. `o-2` bir kez teslim edilir ve loglanır.
3. `o-3` handler'da exception → **HTTP 5xx** → Event Grid aynı olayı birkaç saniye arayla **en çok 3 kez** dener (Application Insights/log stream'de aynı `cloudEvent.Id`).
4. Denemeler bitince (≈ son denemeden 5 dk sonra) olay `deadletter` container'ında bir **blob** olarak belirir:

```bash
az storage blob list --account-name $ST --container-name deadletter --auth-mode login \
  --query "[].name" -o tsv
# each blob holds the event JSON plus deadLetterReason (e.g. MaxDeliveryAttemptsExceeded) and lastDeliveryOutcome
```

**Python eşdeğeri (`azure-eventgrid`)** — yayıncı ve handler:

```python
from azure.core.messaging import CloudEvent
from azure.eventgrid import EventGridPublisherClient
from azure.identity import DefaultAzureCredential

client = EventGridPublisherClient(endpoint, DefaultAzureCredential())
client.send([
    CloudEvent(source="/orders/api", type="Contoso.Order.Placed", subject="orders/o-2",
               data={"orderId": "o-2", "amount": 1500}),
])
```

```python
# function_app.py (Python v2 model) - handler
import azure.functions as func, logging
app = func.FunctionApp()

@app.event_grid_trigger(arg_name="event")
def high_value_order_handler(event: func.EventGridEvent):
    data = event.get_json()
    logging.info("event %s amount=%s", event.id, data.get("amount"))
    if data.get("simulateFailure"):
        raise RuntimeError("Simulated handler failure")           # => 5xx => retry
```

> Python Event Grid trigger'ın CloudEvents teslim şemasındaki davranışı extension sürümüne bağlıdır; sorun yaşarsan subscription'ı Event Grid şemasıyla (topic'i de o şemayla) dene.

**Temizlik:**

```bash
az group delete --name $RG --yes --no-wait
```

> **Alıştırma.** Aynı topic'e `amount = 1500`, `region = "us"` olayı gönderip yalnızca **EU** siparişlerinin handler'a ulaşmasını sağla.
>
> <details><summary>Çözüm</summary>
>
> İkinci bir advanced filter ekle: `--advanced-filter data.region StringIn eu` (filtreler **AND**'lenir; `StringIn eu eu-west` değer listesi OR'dur). Subscription'ı `az eventgrid event-subscription update` ile güncelleyebilir veya yeniden oluşturabilirsin.
> </details>

---

### 4. Gerçek dünya senaryosu

Bir e-ticaret platformu:

| Gereksinim | Karar | Neden |
|---|---|---|
| Ürün görseli blob'a yüklenince thumbnail üretilsin | **System topic** (Storage) + `subject-ends-with .jpg` + Function | Polling yok; yalnızca jpg tetikler |
| `Order.Placed` olayını muhasebe, stok ve pazarlama bağımsız dinlesin | **Custom topic** + her biri için ayrı subscription | Yayıncı dinleyicileri bilmez; yeni servis = yeni subscription |
| Yalnızca 1000 TL üstü siparişler fraud servisine | **Advanced filter** `data.amount NumberGreaterThan 1000` | Gereksiz çağrı ve maliyet yok |
| Muhasebe sistemi geceleri kapalı, olaylar kaybolmamalı | Subscription **TTL 24 sa** + **dead-letter blob**; veya handler = **Service Bus queue** | Retry penceresi + kayıp olay incelemesi; kuyruk dayanıklılığı |
| Ödeme komutu **tam bir kez**, sıralı işlenmeli | **Service Bus** (session + DLQ), Event Grid değil | Event Grid olay bildirir; iş garantisi vermez |
| Aynı olay iki kez gelebilir | Handler'da `cloudEvent.Id` ile **idempotency** | At-least-once |

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → filtre türü, retry/TTL/dead-letter ayarı, topic türü, Event Grid vs Service Bus; CLI/SDK tamamlama (`--advanced-filter`, `EventGridPublisherClient`).

| Senaryodaki ifade | Cevap |
|---|---|
| "Blob/Resource Group gibi **Azure servisi** olayına tepki" | **System topic** |
| "Kendi uygulamamın olaylarını yayınla" | **Custom topic** + `EventGridPublisherClient` |
| "SaaS sağlayıcının olayları" | **Partner topic** |
| "Yalnızca `.jpg` olsun" / "şu container'daki" | **Subject ends with / begins with** |
| "Yalnızca `data.amount` > 1000" | **Advanced filter** `NumberGreaterThan` |
| "Yalnızca belirli olay türleri" | **Event type** filtresi |
| "Teslim edilemeyen olayları sakla" | **Dead-letter** (storage blob container) — **elle açılmalı** |
| "Teslim deneme sayısı / süresi" | **Max delivery attempts** (≤ 30) ve **event TTL** (≤ 1440 dk) |
| "İş kuyruğu, sıralama, session, DLQ, işleme garantisi" | **Service Bus** |
| "Binlerce olay/sn telemetri akışı" | **Event Hubs** |
| "Webhook subscription oluşturulamıyor" | **Validation handshake** yanıtı eksik |
| "Event Grid → dayanıklı işleme" | Handler olarak **Service Bus queue** |
| "Olay çift geldi" | At-least-once → **idempotent** handler |
| "Secret'sız yayın" | Entra + **EventGrid Data Sender** rolü |

**Tuzaklar:**
- **Event Grid kuyruk değildir:** sıra, session, "tüketici hazır olunca al" yok; retry süresi dolunca olay dead-letter'a (varsa) gider veya **kaybolur**.
- **Dead-letter varsayılan kapalıdır;** kapalıysa 400/413 ve süresi dolan olaylar **sessizce düşer**.
- **Retry ve TTL ikisi birlikte:** 30 deneme istesen de TTL erken dolarsa deneme sayısı fiilen daha azdır.
- **Filtre türü:** yol/uzantı → subject filtresi; payload alanı → **advanced** filter; olay türü → event type.
- **Şema eşleşmesi:** CloudEvents girişli topic'e Event Grid şemalı olay yollanmaz; CloudEvents girişli topic'ten Event Grid şemasıyla teslim yapılamaz.
- **Retry schedule yapılandırılamaz**; yapılandırılan yalnızca *max attempts* ve *TTL*'dir.
- **System topic**'e kendin olay **yayınlayamazsın**.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Event Grid'i iş kuyruğu gibi kullanmak | İş komutları → Service Bus; Event Grid → bildirim (veya handler = Service Bus) |
| Handler'ı idempotent yazmamak | At-least-once + retry → çift işleme; `id` ile tekilleştir |
| Dead-letter'ı yapılandırmamak | Subscription'da storage container ver; blob'ları izle |
| Webhook handler'da handshake'i atlamak | `validationCode` yanıtı / CloudEvents `OPTIONS` yanıtı |
| Handler'ın uzun sürmesi (zaman aşımı → retry fırtınası) | Hızlı `2xx` dön, işi kuyruğa at |
| Filtreyi gövdenin dışındaki alana yazmak (`amount`) | Anahtar `data.amount` |
| Büyük payload'ı olaya koymak | Olayda referans, veri blob'da |
| Access key'i koda gömmek | `DefaultAzureCredential` + **EventGrid Data Sender** |
| Klasik topic varsayılanlarını (30/1440) namespace topic'e taşımak | Namespace topic'leri farklı varsayılanlara sahiptir |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Sunucu tarafı filtreleme | Gereksiz handler çağrısı/maliyeti yok | Filtre kotası; karmaşık mantık handler'da kalır |
| Retry/TTL uzun | Geçici kesintiyi tolere eder | Eski olay geç teslim edilebilir; sıra bozulur |
| Retry/TTL kısa + dead-letter | Hızlı hata görünürlüğü | Daha çok elle müdahale |
| Batch teslim | Daha az HTTP çağrısı | Handler toplu işleme + kısmi hata yönetimi gerektirir |
| Handler = Function | Sıfır altyapı, otomatik doğrulama | Function hata/zaman aşımı → retry maliyeti |
| Handler = Service Bus queue | Dayanıklılık, sıra/session, DLQ | Ek servis maliyeti ve gecikmesi |
| Namespace topic (pull) | Tüketici hızını kendisi belirler | Farklı model/varsayılanlar; klasik handler seti yok |

---

### 8. Kaynaklar

- [AI-200 study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Event Grid overview](https://learn.microsoft.com/en-us/azure/event-grid/overview) · [Concepts](https://learn.microsoft.com/en-us/azure/event-grid/concepts) · [Event handlers](https://learn.microsoft.com/en-us/azure/event-grid/event-handlers)
- [Delivery and retry](https://learn.microsoft.com/en-us/azure/event-grid/delivery-and-retry) · [Set dead-letter location and retry policy](https://learn.microsoft.com/en-us/azure/event-grid/manage-event-delivery)
- [Event filtering for subscriptions](https://learn.microsoft.com/en-us/azure/event-grid/event-filtering) · [CloudEvents schema](https://learn.microsoft.com/en-us/azure/event-grid/cloud-event-schema)
- [Webhook event delivery / validation](https://learn.microsoft.com/en-us/azure/event-grid/webhook-event-delivery) · [Namespace topics: pull and push delivery](https://learn.microsoft.com/en-us/azure/event-grid/pull-delivery-overview)
- [Choose between Event Grid, Event Hubs and Service Bus](https://learn.microsoft.com/en-us/azure/event-grid/compare-messaging-services)
- [Azure.Messaging.EventGrid (.NET)](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/messaging.eventgrid-readme) · [azure-eventgrid (Python)](https://learn.microsoft.com/en-us/python/api/overview/azure/eventgrid-readme) · [Event Grid trigger for Functions](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-event-grid-trigger)


### 📝 Bölüm 3.3 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir blob storage hesabına dosya yüklendiğinde bir Azure Function'ın tetiklenmesini istiyorsun. Olayı hangi topic türü sağlar?

- A) Custom topic
- B) System topic
- C) Partner topic
- D) Kendi yazdığın bir Service Bus topic'i

<details><summary>Cevap</summary>

**B.** Azure servislerinin ürettiği olaylar (ör. `Microsoft.Storage.BlobCreated`) **system topic** üzerinden gelir; kaynak sahibi servis olayı kendisi yayınlar. Custom topic kendi uygulama olayların içindir; partner topic üçüncü taraf SaaS olayları içindir.
</details>

#### Soru 2: Tek seçim
Uygulaman `Contoso.Order.Placed` olaylarını yayınlayacak. Hangi seçim doğrudur?

- A) System topic oluşturup `EventGridPublisherClient` ile yazmak
- B) Custom topic oluşturup `EventGridPublisherClient` ile yazmak
- C) Event Hubs kullanmak, çünkü Event Grid yalnızca Azure olaylarını destekler
- D) Partner topic oluşturmak

<details><summary>Cevap</summary>

**B.** Kendi uygulama olayların için custom topic kullanılır; yayın `EventGridPublisherClient` ile topic endpoint'ine yapılır. System topic'e kendin olay yazamazsın; Event Grid custom olayları destekler.
</details>

#### Soru 3: Kod tamamlama (C#)
Aşağıdaki kod custom topic'e **secret'sız** (Entra ID) yayın yapmalıdır. Boşluğu doldurun.

```csharp
var client = new EventGridPublisherClient(new Uri(topicEndpoint), new ______());
```

- A) `AzureKeyCredential(key)`
- B) `DefaultAzureCredential`
- C) `StorageSharedKeyCredential`
- D) `ServiceBusClient`

<details><summary>Cevap</summary>

**B.** `DefaultAzureCredential` (veya başka bir `TokenCredential`) Entra kimliğiyle yayın yapar; kimliğin topic üzerinde **EventGrid Data Sender** rolü olmalıdır. `AzureKeyCredential` access key içindir (secret yönetimi gerektirir).
</details>

#### Soru 4: Çoklu seçim (iki doğru cevap)
Bir subscription'ın yalnızca `Contoso.Order.Placed` türünde **ve** `data.amount` değeri 1000'den büyük olayları alması için **hangi ikisi** yapılandırılır?

- A) `--included-event-types Contoso.Order.Placed`
- B) `--advanced-filter data.amount NumberGreaterThan 1000`
- C) `--subject-begins-with 1000`
- D) `--max-delivery-attempts 1000`

<details><summary>Cevap</summary>

**A ve B.** Olay türü filtresi ve payload alanına advanced filter birlikte (AND) uygulanır. Subject filtresi `subject` alanına bakar, `data` alanına değil; `max-delivery-attempts` retry ayarıdır.
</details>

#### Soru 5: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Birden çok advanced filter bir subscription'da birlikte AND ile değerlendirilir. | ☐ | ☐ |
| b) `StringIn a,b` filtresindeki değerler OR ile değerlendirilir. | ☐ | ☐ |
| c) Filtreye uymayan olay, aynı topic'teki diğer subscription'lara da iletilmez. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Filtre **subscription'a özgüdür**: uymayan olay yalnızca o subscription'a teslim edilmez, diğer subscription'ları etkilemez.
</details>

#### Soru 6: Tek seçim
Bir subscription'da **max delivery attempts = 10** ve **event TTL = 30 dakika** ayarlı. Handler sürekli 500 dönüyor. Ne olur?

- A) Event Grid tam 10 deneme yapar, TTL yok sayılır
- B) Önce hangi sınır dolarsa (deneme sayısı **veya** TTL) teslim biter; TTL dolarsa 10 deneme tamamlanmadan da son bulabilir
- C) Event Grid 30 dakika boyunca saniyede bir dener
- D) Olay anında silinir

<details><summary>Cevap</summary>

**B.** İki sınır birlikte uygulanır, önce dolan işi bitirir. Backoff aralıkları büyüdüğü için 30 dakikada 10 denemeye ulaşılamayabilir. Bu aralıklar yapılandırılamaz; yalnızca max attempts ve TTL ayarlanır.
</details>

#### Soru 7: Tek seçim
Handler bir webhook ve **400 Bad Request** dönüyor (olay bozuk). Subscription'da dead-letter **yapılandırılmamış**. Olaya ne olur?

- A) 30 kez yeniden denenir
- B) Retry edilmeden dead-letter'a gönderilmek istenir; dead-letter yapılandırılmadığı için olay düşer
- C) Otomatik olarak Service Bus'a taşınır
- D) Event Grid, subscription'ı hemen siler

<details><summary>Cevap</summary>

**B.** 400/413 gibi kalıcı hatalar retry edilmez. Dead-letter **varsayılan kapalıdır**; kapalıysa olay kaybolur. Bu yüzden üretimde dead-letter container'ı tanımlanmalıdır.
</details>

#### Soru 8: Kod tamamlama (az CLI)
Subscription'a dead-letter hedefi ve retry ilkesi ekleyin. Boşlukları doldurun.

```bash
az eventgrid event-subscription create --name sub1 --source-resource-id $TOPIC_ID \
  --endpoint-type azurefunction --endpoint $FUNC_ID \
  --max-delivery-attempts 5 --______ 60 \
  --deadletter-endpoint "$ST_ID/blobServices/default/containers/deadletter"
```

- A) `--event-ttl`
- B) `--lock-duration`
- C) `--retry-interval`
- D) `--message-ttl`

<details><summary>Cevap</summary>

**A.** Olayın retry için yaşayabileceği süre dakika cinsinden `--event-ttl` ile verilir (varsayılan 1440, en çok 1440). `--lock-duration` Service Bus kavramıdır; Event Grid retry aralığı yapılandırılamaz.
</details>

#### Soru 9: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Event Grid teslimi **at-least-once**'tır; aynı olay birden fazla kez gelebilir. | ☐ | ☐ |
| b) Event Grid olayların gönderildiği sırayla teslim edileceğini garanti eder. | ☐ | ☐ |
| c) Dead-letter edilen olaylar bir Storage container'ında blob olarak saklanır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Handler'lar idempotent olmalı ve sıra bağımlı olmamalıdır; sıra/session gerekiyorsa Service Bus kullanılır.
</details>

#### Soru 10: Senaryo
Bir ödeme sistemi, "bu ödemeyi tahsil et" komutlarını **aynı müşteri için sıralı** ve **tam olarak bir tüketici** tarafından, hata olursa DLQ'ya ayırarak işlemek istiyor. Hangisi uygundur?

- A) Event Grid custom topic + webhook
- B) Event Grid system topic
- C) Service Bus queue (session ile) ve DLQ
- D) Event Hubs

<details><summary>Cevap</summary>

**C.** Bu bir **iş komutu**dur; sıra, session, işleme garantisi ve DLQ Service Bus'ın alanıdır. Event Grid bildirim içindir ve sıra/session sunmaz; Event Hubs telemetri akışıdır.
</details>

#### Soru 11: Çoklu seçim (iki doğru cevap)
Event Grid ile Service Bus'ı **birlikte** kullanmanın **hangi ikisi** doğru gerekçesidir?

- A) Event Grid olayı yakalar, Service Bus queue handler'ı olayı dayanıklı kuyruğa koyar; tüketici kendi hızında işler
- B) Event Grid'in retry politikasını tamamen devre dışı bırakır
- C) Service Bus tüketicisi sıra/session/DLQ özelliklerinden yararlanır
- D) Service Bus topic'leri Event Grid olaylarının şemasını otomatik CloudEvents'e çevirir

<details><summary>Cevap</summary>

**A ve C.** Event Grid → Service Bus queue, "olay bildirimi + dayanıklı işleme" kalıbıdır. Retry'ı devre dışı bırakmak (B) veya şema çevirisi (D) bu kalıbın amacı değildir.
</details>

#### Soru 12: Sıralama
Bir webhook handler'ın Event Grid subscription'ına eklenmesini sıraya koyun: **(1)** Event Grid doğrulama olayını (`validationCode`) webhook'a gönderir · **(2)** Subscription oluşturma isteği yapılır · **(3)** Handler `validationResponse` ile kodu geri döner · **(4)** Subscription etkinleşir ve olaylar akmaya başlar.

<details><summary>Cevap</summary>

**2 → 1 → 3 → 4.** Webhook sahipliği handshake ile kanıtlanır. Azure Functions, Service Bus, Storage Queue gibi Azure hedeflerinde bu adım otomatiktir.
</details>

#### Soru 13: Kod tamamlama (Python)
Aşağıdaki Python kodu custom topic'e bir CloudEvent yayınlar. Boşluğu doldurun.

```python
from azure.core.messaging import CloudEvent
from azure.eventgrid import EventGridPublisherClient
from azure.identity import DefaultAzureCredential

client = EventGridPublisherClient(endpoint, DefaultAzureCredential())
client.______(CloudEvent(source="/orders/api", type="Contoso.Order.Placed", data={"amount": 1500}))
```

- A) `publish`
- B) `send`
- C) `emit`
- D) `post_event`

<details><summary>Cevap</summary>

**B.** `EventGridPublisherClient.send(...)` tek olay veya liste kabul eder. `CloudEvent` için `source` ve `type` zorunludur.
</details>

#### Soru 14: Tek seçim
Bir custom topic **CloudEvents v1.0 input schema** ile oluşturuldu. Bir geliştirici subscription'ı `--event-delivery-schema eventgridschema` ile oluşturmak istiyor. Sonuç?

- A) Sorunsuz çalışır; Event Grid otomatik çevirir
- B) Desteklenmez: CloudEvents girişli topic'ten Event Grid şemasıyla teslim yapılamaz
- C) Yalnızca dead-letter açıksa çalışır
- D) Yalnızca Service Bus handler'larında çalışır

<details><summary>Cevap</summary>

**B.** Event Grid şeması CloudEvents'in extension özniteliklerini taşıyamaz; bu yüzden CloudEvents input schema'sından Event Grid çıktı şeması kullanılamaz. Delivery schema subscription'da seçilir ama input schema ile uyumlu olmalıdır.
</details>

#### Soru 15: Tek seçim
Subject'i `/blobServices/default/containers/images/blobs/photo1.jpg` olan olaylardan yalnızca **`.jpg`** uzantılı olanları almak istiyorsun. En basit filtre hangisidir?

- A) `--advanced-filter data.url StringContains jpg`
- B) `--subject-ends-with .jpg`
- C) `--included-event-types .jpg`
- D) `--subject-begins-with .jpg`

<details><summary>Cevap</summary>

**B.** Uzantı/yol eşleşmesi için subject-ends-with/begins-with filtreleri yeterlidir ve en basitidir. Event type filtresi `eventType` değerine bakar; begins-with öneki eşler. (A de çalışabilir ama gereksiz karmaşıktır ve `data` yapısına bağımlıdır.)
</details>

#### Soru 16: Vaka (kısa)
**Durum:** Bir medya şirketi `VideoPublished` olaylarını custom topic'e yayınlıyor. Üç subscription var: *search-index* (Function), *notifications* (webhook, geceleri bakımda) ve *analytics* (Service Bus queue). Gereksinimler: (1) yalnızca `data.durationSeconds > 60` olan videolar *search-index*'e gitsin, (2) *notifications* bakımdayken (6 saat) olaylar kaybolmasın, bakım sonrası teslim edilsin, (3) yine de teslim edilemeyen olaylar incelenebilsin, (4) *analytics* tüketicisi olayları kendi hızında, hata olursa DLQ ile işlesin.

**Soru:** Hangi üç çözüm gereksinimleri karşılar?

- A) *search-index* subscription'ına `--advanced-filter data.durationSeconds NumberGreaterThan 60`
- B) *notifications* subscription'ında event TTL'i bakım süresinden uzun (ör. 1440 dk) ve max attempts'i yeterli tutmak; dead-letter container'ı tanımlamak
- C) *analytics* handler'ı olarak Service Bus queue kullanmak; DLQ ve PeekLock tüketicinin tarafındadır
- D) Event Grid retry aralığını 1 saate sabitlemek
- E) Dead-letter'ı kapalı bırakmak; varsayılan zaten güvenlidir

<details><summary>Cevap</summary>

**A, B ve C.** A payload filtresidir. B bakım penceresini kapsayan retry/TTL + dead-letter ile kayıp riskini azaltır (retry aralığı Event Grid tarafından yönetilir). C, olayı dayanıklı bir kuyruğa taşır ve tüketici kendi hızında, DLQ ile işler. D yapılandırılamaz bir ayardır; E'de varsayılan dead-letter kapalıdır, bu da olayların sessizce düşmesi demektir.
</details>


---

# BÖLÜM 4: Secure, monitor, and troubleshoot Azure solutions (%20-25)

---

## 4.1 Managed Identity ve Azure Key Vault

> **Sınav Alanı:** Secure, monitor, and troubleshoot Azure solutions (%20-25) · **Konu Kodu:** C2.11

### 1. Ne?

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

### 2. Neden?

- **Sınav:** Hedef doğrudan "secret'ları Key Vault ile güvenceye al (rotation + retrieval)" der. Sorular senaryo + kod tamamlama olur: *"En az yetkiyle uygulamanın secret okumasını sağla"*, *"near-expiry olayı ile rotation'ı otomatikleştir"*, `SecretClient`/`DefaultAzureCredential` boşluk doldurma.
- **Gerçek işte:** Sızan bağlantı dizesi en yaygın bulut ihlali nedenlerindendir. Repo'daki, pipeline değişkenindeki, `appsettings.json`'daki secret'lar kopyalanır, eskir, rotasyona girmez. MI + Key Vault ile **saklanacak secret sayısı azalır**, kalan secret tek yerde, denetlenebilir ve döndürülebilir olur.
- **Diğer konularla bağ:** Container Apps secret'ları (→ [C2.2](#12-azure-container-apps-ortam-revision-ve-keda)), Functions bağlantıları (→ [C2.8](#31-azure-functions-serverless-api-trigger-ve-binding)), Cosmos/PostgreSQL/Service Bus'a Entra kimliğiyle bağlanma ve App Configuration'daki Key Vault referansları (→ [C2.12](#42-azure-app-configuration)) bu konunun üstüne kurulur. Kimlik derinliği → M9, secret yönetimi mimarisi → S6/M9.

---

### 3. Nasıl?

#### 3.1 Managed identity türleri

| | **System-assigned** | **User-assigned** |
|---|---|---|
| Oluşturma | Kaynakta açılır (`--system-assigned`) | Ayrı kaynak olarak oluşturulur, kaynağa **bağlanır** |
| Yaşam döngüsü | Kaynakla **birlikte** silinir | Bağımsız; kaynak silinse de kalır |
| Paylaşım | Hayır (kaynağa özel) | **Evet** — aynı kimlik N kaynakta |
| Ne zaman? | Tek kaynak, basit, en az yönetim | Birden çok kaynak aynı yetkiyi paylaşır (ör. 3 Function + 1 API); **yetkiyi dağıtımdan önce hazırlamak** gerekir; kaynak sık yeniden oluşturulur; Key Vault referansı için önceden tanımlı kimlik |
| Tuzak | Kaynağı silip yeniden oluşturunca principal ID değişir → rol atamaları **kaybolur** | Bir kaynağa **birden çok** user-assigned atanabilir → hangisini kullanacağını (`client_id`) belirtmelisin |

> **Çoklu kimlik tuzağı:** Bir kaynakta hem system- hem user-assigned veya birden çok user-assigned varsa `DefaultAzureCredential()` hangisini seçeceğini bilemez; `AZURE_CLIENT_ID` ortam değişkeni veya `ManagedIdentityClientId` ile **belirt**.

**Workload identity federation** (→ M9): MI'nın dışında çalışan iş yükü (AKS pod'u, GitHub Actions, başka bulut) için **federated credential**: dış IdP'nin token'ı Entra token'ıyla değiştirilir; secret yok. AKS'te `WorkloadIdentityCredential` (service account ↔ user-assigned MI), GitHub Actions'ta OIDC ile `azure/login`.

#### 3.2 `DefaultAzureCredential` ve credential seçimi

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

#### 3.3 Key Vault nesneleri, sürümler ve izin modeli

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

#### 3.4 Retrieval — secret'ı uygulamaya almanın yolları ⭐

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

#### 3.5 Rotation ⭐

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

- **Olaylar:** `Microsoft.KeyVault.SecretNearExpiry`, `SecretExpired`, `SecretNewVersionCreated` (key ve certificate için benzerleri). Abonelik kaynağı **vault**'tur; handler: Function, Logic App, webhook, Service Bus… (→ [C2.10](#33-azure-event-grid-custom-event-filtre-ve-retry)). Event Grid **at-least-once** teslim eder → rotation handler'ı **idempotent** olmalı.
- **Döngü tuzağı:** Function yeni sürüm yazınca `SecretNewVersionCreated` doğar; yalnızca `SecretNearExpiry`/`SecretExpired`'a abone ol (olay türü filtresi) ya da handler'da kendi yazdığın sürümü yoksay.
- **İki kimlik bilgisi (dual credentials) kalıbı:** Hedef servis iki anahtar sunuyorsa (Storage key1/key2, Service Bus primary/secondary) uygulama bir anahtarı kullanırken **diğerini** döndür, vault'a yaz; uygulama yenileyince geçer; sonraki turda ilkini döndür. Kesintisiz rotation budur.
- **Key rotation policy:** Key'ler için vault'ta **otomatik** döndürme politikası (`az keyvault key rotation-policy update`): süre, `Rotate`/`Notify` eylemi, `expiryTime`. Yeni key sürümü oluşur; key'i kullanan servis (ör. CMK) sürümsüz URI ile yeni sürüme geçer.
- Rotation sonrası **uygulamanın yeni değeri alması** ayrı bir sorudur: SDK önbelleği, `ReloadInterval`, platform referans yenileme aralığı, `IOptions` vs `IOptionsMonitor`. En iyi çözüm **secret'ı hiç kullanmamak**: Entra kimliğiyle bağlanılan servislerde (Cosmos, Service Bus, Storage, PostgreSQL) rotation sorunu ortadan kalkar.

#### 3.6 Koruma: soft delete, purge protection, ağ

| Özellik | Davranış |
|---|---|
| **Soft delete** | Silinen vault/nesne **7–90 gün** (varsayılan 90) kurtarılabilir; yeni vault'larda **her zaman açık** (kapatılamaz); süre vault oluşturulurken seçilir, sonra değişmez |
| **Purge protection** | Açıksa soft-deleted nesne retention bitmeden **kimse (Microsoft dahil) kalıcı silemez**; bir kez açılınca **kapatılamaz**; CMK senaryoları için gereklidir |
| **Ağ** | Firewall (IP/VNet kuralı) veya **private endpoint** (`privatelink.vaultcore.azure.net`) + public erişimi kapat; "trusted Microsoft services" istisnası |
| **Denetim** | Diagnostic settings → Log Analytics (`AzureDiagnostics`/`AZKVAuditLogs`); kim hangi secret'ı ne zaman okudu (→ [C2.13](#43-opentelemetry-ile-izleme-ve-kql-ile-sorun-giderme)) |

#### 3.7 Lab — MI ile secret oku, rotation yap, near-expiry olayını işle

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

### 4. Gerçek dünya senaryosu

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

### 5. Sınav perspektifi

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

### 6. Yaygın hatalar

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

### 7. Performans & trade-off

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

### 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Managed identities for Azure resources](https://learn.microsoft.com/en-us/entra/identity/managed-identities-azure-resources/overview) · [Best practices for managed identities](https://learn.microsoft.com/en-us/entra/identity/managed-identities-azure-resources/managed-identity-best-practice-recommendations)
- [Credential chains in the Azure Identity library for .NET](https://learn.microsoft.com/en-us/dotnet/azure/sdk/authentication/credential-chains) (`DefaultAzureCredential` üretim notları)
- [Azure Key Vault overview](https://learn.microsoft.com/en-us/azure/key-vault/general/overview) · [Grant access with Azure RBAC](https://learn.microsoft.com/en-us/azure/key-vault/general/rbac-guide) · [Plan for Azure RBAC as the default access control model](https://learn.microsoft.com/en-us/azure/key-vault/general/access-control-default)
- [Soft delete](https://learn.microsoft.com/en-us/azure/key-vault/general/soft-delete-overview) · [Recovery and purge protection](https://learn.microsoft.com/en-us/azure/key-vault/general/key-vault-recovery)
- [Key Vault as Event Grid source](https://learn.microsoft.com/en-us/azure/event-grid/event-schema-key-vault) · [Rotation tutorial — resources with two sets of credentials](https://learn.microsoft.com/en-us/azure/key-vault/secrets/tutorial-rotation-dual)
- [Key Vault references (App Service/Functions)](https://learn.microsoft.com/en-us/azure/app-service/app-service-key-vault-references) · [Manage secrets in Azure Container Apps](https://learn.microsoft.com/en-us/azure/container-apps/manage-secrets)
- [Azure Key Vault secret client library for .NET](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/security.keyvault.secrets-readme) · [azure-keyvault-secrets (Python)](https://learn.microsoft.com/en-us/python/api/overview/azure/keyvault-secrets-readme)

> **Doğrulama notu:** "Yeni vault'larda RBAC varsayılanı" Key Vault control plane API `2026-02-01` ile ilişkilendirilir (mevcut vault'lar etkilenmez); platform referans yenileme aralıkları (App Service ≈ 24 saat, Container Apps ≈ 30 dk) dokümantasyonda/Q&A'da değişken ifade edilir — sınav öncesi güncel sayfaları kontrol et.


### 📝 Bölüm 4.1 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Üç Azure Function uygulaması ve bir API aynı Key Vault'tan secret okuyacak. Kaynaklar CI/CD ile sık sık silinip yeniden oluşturuluyor. Rol atamalarının **bir kez** yapılıp yeniden oluşturmalarda korunmasını istiyorsun. Ne kullanmalısın?

- A) Her kaynakta system-assigned managed identity
- B) Tek bir user-assigned managed identity'yi dört kaynağa atamak
- C) Vault'a bir client secret ile bağlanmak
- D) Key Vault access policy'ye her kaynak için ayrı Entra kullanıcısı eklemek

<details><summary>Cevap</summary>

**B.** User-assigned MI bağımsız bir kaynaktır; kaynaklar silinse de principal ID aynı kalır ve rol ataması korunur; ayrıca birden çok kaynağa atanabilir. System-assigned (A) kaynakla birlikte silinir, yeniden oluşturulunca **yeni principal** gelir ve roller kaybolur. C ve D secret/kullanıcı yönetimi getirir ve "secret'sız" hedefine aykırıdır.
</details>

#### Soru 2: Tek seçim
Bir API'nin managed identity'si, RBAC izin modelindeki vault'tan yalnızca secret **değerlerini okuyacak**. **En az yetkiyi** hangi rol verir?

- A) Contributor
- B) Key Vault Administrator
- C) Key Vault Secrets User
- D) Key Vault Reader

<details><summary>Cevap</summary>

**C.** *Key Vault Secrets User* secret değerini okur (get/list). *Contributor* control plane rolüdür ve değer okutmaz. *Administrator* gereğinden geniştir. *Key Vault Reader* yalnızca meta veriyi (ad, öznitelik) gösterir, **değeri vermez**.
</details>

#### Soru 3: Tek seçim
Rotation Function'ı vault'a **yeni secret sürümü yazacak**. Function'ın MI'ına hangi rol verilmelidir (en az yetki)?

- A) Key Vault Secrets User
- B) Key Vault Secrets Officer
- C) Key Vault Crypto Officer
- D) Owner

<details><summary>Cevap</summary>

**B.** Secret oluşturma/güncelleme data plane'de *Secrets Officer* gerektirir. *Secrets User* salt okunurdur (A). *Crypto Officer* key'ler içindir, secret'lar için değil (C). *Owner* control plane'dir ve fazlasıyla geniştir (D).
</details>

#### Soru 4: Evet / Hayır

Vault, **Azure RBAC** izin modelini kullanıyor.

| İfade | Evet | Hayır |
|---|---|---|
| a) Vault üzerinde `Owner` olan bir kullanıcı, ek rol almadan secret değerini okuyabilir. | ☐ | ☐ |
| b) Rol atamasını tek bir secret kapsamında yapmak mümkündür. | ☐ | ☐ |
| c) Access policy girdileri RBAC rolleriyle birlikte etkin sayılır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** `Owner` control plane'dir; data plane (secret okuma) için ayrı bir rol gerekir. RBAC modelinde kapsam vault, kaynak grubu ve **tek nesne** düzeyinde olabilir (access policy yalnızca vault düzeyidir). Bir vault'ta aynı anda tek model geçerlidir; RBAC açıkken access policy'ler **yok sayılır**.
</details>

#### Soru 5: Tek seçim
Bir App Service uygulaması şu app setting'i kullanıyor:
`@Microsoft.KeyVault(SecretUri=https://kv-demo.vault.azure.net/secrets/db-conn/3f1a9c…)`
Secret'ın yeni sürümü yazıldı ama uygulama eski değeri kullanmaya devam ediyor. Neden?

- A) Key Vault referansları rotation'ı desteklemez
- B) Referans belirli bir **sürüme sabitlenmiş**; sürümsüz URI gerekir
- C) Managed identity'nin Secrets Officer rolü yok
- D) Event Grid aboneliği eksik

<details><summary>Cevap</summary>

**B.** URI'daki sürüm kimliği referansı o sürüme sabitler; en son sürümü almak için `.../secrets/db-conn/` (sürümsüz) kullanılır. Sürümsüz referanslar platform tarafından belirli aralıklarla yenilenir (App Service'te yaklaşık günlük, kesin SLA yok; app setting değişimi/restart anında yeniler). Okuma için Secrets User yeterlidir (C); Event Grid referansın yenilenmesi için gerekmez (D).
</details>

#### Soru 6: Çoklu seçim (iki doğru cevap)
Secret'lar için "süresi dolmak üzere" olayıyla otomatik rotation kurmak istiyorsun. **Hangi iki** bileşen akışın parçasıdır?

- A) Secret'a bir expiry date (`--expires`) atanması
- B) Key Vault'un `Microsoft.KeyVault.SecretNearExpiry` olayına Event Grid aboneliği
- C) Azure Policy'nin secret'ı otomatik yeniden oluşturması
- D) Application Insights availability test

<details><summary>Cevap</summary>

**A ve B.** Near-expiry olayı yalnızca expiry tarihi olan secret'lar için doğar; Event Grid olayı Function/Logic App'e iletir, yeni değeri üreten ve vault'a yazan o handler'dır. Azure Policy secret üretmez (C); availability test ilgisizdir (D).
</details>

#### Soru 7: Sıralama
Tek anahtarlı bir hedef servis yerine **iki anahtarlı** (primary/secondary) bir servis için kesintisiz rotation adımlarını doğru sıraya koy:

1. Uygulama vault'tan yeni değeri yenileyip secondary anahtara geçer
2. Event Grid `SecretNearExpiry` olayı Function'ı tetikler
3. Function, **kullanılmayan** anahtarı hedef serviste yeniden üretir
4. Function yeni anahtarı vault'a yeni secret sürümü olarak yazar
5. Bir sonraki turda eski (artık kullanılmayan) anahtar yenilenir

<details><summary>Cevap</summary>

**2 → 3 → 4 → 1 → 5.** Olay tetikler; Function kullanılmayan anahtarı üretir; vault'a yeni sürüm yazar; uygulama yenilemeyle yeni değere geçer; eski anahtar sonraki rotasyonda döndürülür. Aktif anahtarı önce değiştirmek, uygulamayı vault'u okumadan kıracağı için yanlıştır.
</details>

#### Soru 8: Kod tamamlama (C#)
Yerelde `az login` ile, Azure'da **belirli bir user-assigned MI** ile çalışacak şekilde boşluğu doldur:

```csharp
var client = new SecretClient(
    new Uri("https://kv-demo.vault.azure.net/"),
    new DefaultAzureCredential(new DefaultAzureCredentialOptions
    {
        ______ = "11111111-2222-3333-4444-555555555555"   // user-assigned identity client id
    }));
```

- A) `TenantId`
- B) `ManagedIdentityClientId`
- C) `VisualStudioTenantId`
- D) `AuthorityHost`

<details><summary>Cevap</summary>

**B.** `ManagedIdentityClientId` (veya `AZURE_CLIENT_ID` ortam değişkeni) zincirdeki managed identity adımının hangi user-assigned kimliği kullanacağını belirler. Yerelde MI adımı atlanır, `az login` kimliği devreye girer. Diğerleri tenant/authority ayarlarıdır.
</details>

#### Soru 9: Kod tamamlama (Python)
Aşağıdaki kod secret'ın **yeni bir sürümünü** oluşturmalı ve 90 gün sonra sona erecek şekilde işaretlemelidir:

```python
from datetime import datetime, timedelta, timezone
from azure.identity import DefaultAzureCredential
from azure.keyvault.secrets import SecretClient

client = SecretClient("https://kv-demo.vault.azure.net/", DefaultAzureCredential())
client.______("Db--ConnectionString", new_value,
              expires_on=datetime.now(timezone.utc) + timedelta(days=90))
```

- A) `update_secret_properties`
- B) `set_secret`
- C) `begin_recover_deleted_secret`
- D) `backup_secret`

<details><summary>Cevap</summary>

**B.** `set_secret` aynı adla çağrılınca yeni sürüm üretir. `update_secret_properties` **mevcut** sürümün özniteliklerini değiştirir, değer/sürüm üretmez. C silinmiş secret'ı kurtarır, D yedek alır.
</details>

#### Soru 10: CLI tamamlama
Yeni bir vault'u **RBAC modeliyle** ve kalıcı silmeye karşı korumalı oluşturmak istiyorsun:

```bash
az keyvault create --resource-group rg-demo --name kv-demo-123 --location westeurope \
  --______ true --enable-purge-protection true
```

- A) `--enable-rbac-authorization`
- B) `--enable-soft-delete`
- C) `--public-network-access`
- D) `--sku premium`

<details><summary>Cevap</summary>

**A.** `--enable-rbac-authorization true` izin modelini Azure RBAC yapar. Soft delete yeni vault'larda zaten **her zaman açıktır** (B gereksiz); C ağ erişimidir; D HSM destekli key içindir, izin modeliyle ilgisizdir.
</details>

#### Soru 11: Tek seçim
Bir ekip, silinen bir vault'un ve içeriğinin **retention süresi dolmadan hiç kimse (yöneticiler dahil) tarafından** kalıcı silinememesini istiyor. Ne etkinleştirilmelidir?

- A) Soft delete
- B) Purge protection
- C) Private endpoint
- D) Resource lock `CanNotDelete`

<details><summary>Cevap</summary>

**B.** Purge protection açıkken soft-deleted nesne retention bitene kadar kalıcı silinemez; bir kez açılınca kapatılamaz. Soft delete (A) kurtarma penceresi sağlar ama purge'ü engellemez. Private endpoint ağ erişimidir (C); resource lock silme işlemini bloklar ama purge protection'ın yerini tutmaz (D).
</details>

#### Soru 12: Senaryo
Bir ASP.NET Core API `AddAzureKeyVault(...)` ile yapılandırılmış ve `ReloadInterval` ayarlanmamış. Secret `Api--Key` rotation ile yeni sürüm aldı; yeni değerin uygulamada görünmesi için **en az değişiklikle** ne yapılmalı? (İki doğru cevap)

- A) `AzureKeyVaultConfigurationOptions.ReloadInterval` ayarlamak
- B) Secret'ı options'a `IOptions<T>` singleton'ı üzerinden okumak
- C) Secret'ı `IOptionsMonitor<T>` (veya `IOptionsSnapshot<T>`) ile tüketmek
- D) Uygulamayı Premium Key Vault'a taşımak

<details><summary>Cevap</summary>

**A ve C.** Sağlayıcı varsayılan olarak yalnızca başlangıçta yükler; `ReloadInterval` periyodik yeniden okuma ekler. Değerin tüketiciye yansıması için options'ın değişikliği izlemesi gerekir (`IOptionsMonitor`/`IOptionsSnapshot`). `IOptions<T>` singleton'dır, ilk değeri tutar (B). Premium SKU HSM key içindir (D).
</details>

#### Soru 13: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) System-assigned MI, ait olduğu kaynak silinince otomatik silinir. | ☐ | ☐ |
| b) Bir kaynağa birden çok user-assigned MI atanabilir. | ☐ | ☐ |
| c) Key Vault, secret'lar için yerleşik otomatik rotation politikası sunar (key'lerdeki gibi). | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Otomatik rotation policy **key'ler** (ve sertifikalarda auto-renew) için vardır; secret'ların değeri hedef serviste de değişmesi gerektiğinden rotation Event Grid + Function ile uygulanır. Birden çok user-assigned atandığında `client_id` ile seçim yapılmalıdır.
</details>

#### Soru 14: Tek seçim
Bir Container Apps uygulaması secret'ı vault'tan referansla almak istiyor. Aşağıdaki komutta boşluğa ne yazılmalıdır?

```bash
az containerapp secret set -g rg-demo -n api-demo \
  --secrets "db-conn=______:https://kv-demo.vault.azure.net/secrets/db-conn,identityref:system"
```

- A) `secretref`
- B) `keyvaultref`
- C) `kvuri`
- D) `vaultref`

<details><summary>Cevap</summary>

**B.** Söz dizimi `<ad>=keyvaultref:<secret-uri>,identityref:<system | kullanıcı-atanmış MI kaynak ID'si>`. `secretref` (A) ise **env var'dan** uygulama secret'ına başvurmak içindir (`DB_CONN=secretref:db-conn`). Sürümsüz URI yeni sürümlerin platform tarafından alınmasını sağlar.
</details>

#### Soru 15: Çoklu seçim (iki doğru cevap)
Üretim API'si `new DefaultAzureCredential()` kullanıyor ve zaman zaman beklenmedik bir kimlikle 403 alıyor. Eski bir CI ajanında `AZURE_CLIENT_ID/SECRET` ortam değişkenleri tanımlı. **Hangi iki** düzeltme doğrudur?

- A) Üretimde belirli credential kullanmak (`ManagedIdentityCredential`)
- B) Credential zincirini `ChainedTokenCredential` ile deterministik hale getirmek
- C) Key Vault'a `Contributor` rolü eklemek
- D) Vault'un soft delete süresini artırmak

<details><summary>Cevap</summary>

**A ve B.** `DefaultAzureCredential` ortam değişkenlerini zincirin başında dener; beklenmeyen bir kimlik seçilebilir. Üretimde belirli credential veya sıralı/özel bir zincir kullanmak belirsizliği ve zincir tarama gecikmesini kaldırır. `Contributor` data plane izni vermez (C); soft delete ilgisizdir (D).
</details>

#### Soru 16: Vaka çalışması

**Durum.** *Contoso Orders* şirketi, Container Apps üzerinde çalışan `orders-api`, iki Azure Function (`invoice-fn`, `rotate-fn`) ve PostgreSQL kullanıyor. Gereksinimler:

1. Uygulamalar PostgreSQL parolasını yalnızca **Key Vault**'tan okuyacak; kodda/pipeline'da secret olmayacak.
2. Parola **90 günde bir** kesintisiz değişecek; değişim insan müdahalesi olmadan yapılacak.
3. `orders-api` ve `invoice-fn` yalnızca okuyacak; yazma yetkisi **yalnızca** `rotate-fn`'de olacak.
4. Yanlışlıkla veya kötü niyetle silinen vault geri getirilebilmeli; kalıcı silme engellenmeli.
5. Vault'a yalnızca şirket VNet'inden erişilmeli.

**Soru A.** Rol atamalarını nasıl yaparsın?
**Soru B.** Rotation'ı hangi bileşenlerle kurarsın?
**Soru C.** Gereksinim 4 ve 5 için hangi ayarlar?

<details><summary>Cevap</summary>

**A.** Vault'u **RBAC** modelinde oluştur. `orders-api` ve `invoice-fn` için tek bir **user-assigned MI** (veya her biri için ayrı MI) → vault/secret kapsamında **Key Vault Secrets User**. `rotate-fn`'ın MI'ı → **Key Vault Secrets Officer** (secret yazar) + PostgreSQL'de yeni rol/parola oluşturma yetkisi. Okuyan kimliklere yazma yetkisi verme (gereksinim 3).

**B.** Secret'a `--expires` ver (≈ 90 gün). Vault'tan **Event Grid** `Microsoft.KeyVault.SecretNearExpiry` olayına abone ol → `rotate-fn` (Event Grid trigger). Function **iki rol/parola kalıbıyla** kullanılmayan PostgreSQL parolasını yenileyip vault'a **yeni sürüm** yazar (idempotent). Uygulamalar sürümsüz referans/`AddAzureKeyVault` + `ReloadInterval` + `IOptionsMonitor` (veya Container Apps `keyvaultref`) ile yeni sürümü alır. Yalnızca `SecretNearExpiry`/`SecretExpired`'a abone ol ki `SecretNewVersionCreated` döngüsü oluşmasın.

**C.** **Soft delete** (yeni vault'larda zaten açık) kurtarma sağlar; kalıcı silmeyi engellemek için **purge protection**'ı etkinleştir (kapatılamaz). Ağ için **private endpoint** oluştur (`privatelink.vaultcore.azure.net` DNS zone) ve public network access'i kapat; Container Apps/Functions aynı VNet'e entegre olmalı.
</details>

---

**Puanlama:** 14-16 → hazırsın · 11-13 → README 3.3 (rol tablosu) ve 3.5 (rotation akışı) bölümlerini tekrar et · < 11 → konuyu baştan çalış ve lab'ı (3.7) uygula.


---

## 4.2 Azure App Configuration

> **Sınav Alanı:** Secure, monitor, and troubleshoot Azure solutions (%20-25) · **Konu Kodu:** C2.12

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
- **Diğer konular:** Kimlik ve secret (→ [C2.11](#41-managed-identity-ve-azure-key-vault)), olay tabanlı yenileme (→ [C2.10](#33-azure-event-grid-custom-event-filtre-ve-retry)), Container Apps ayarları (→ [C2.2](#12-azure-container-apps-ortam-revision-ve-keda)). Feature flag stratejisi ve konfigürasyon mimarisi → M9, S8.3.

---

### 3. Nasıl?

#### 3.1 App Configuration vs Key Vault vs diğerleri

| Soru | **App Configuration** | **Key Vault** | `appsettings.json` / env var |
|---|---|---|---|
| Ne saklar? | Gizli olmayan ayar, feature flag | **Secret**, key, certificate | Paketle gelen varsayılanlar / platform ayarı |
| Değişince | Canlı yenileme (restart yok) | Yeni **sürüm** (rotation) | Yeniden dağıtım/restart |
| Sürüm/geçmiş | Revision geçmişi, snapshot | Secret/key sürümleri | Git |
| Erişim | RBAC: *App Configuration Data Reader/Owner* | RBAC: *Key Vault Secrets User…* | — |
| Ortam ayrımı | **Label** (veya ayrı store) | Ayrı vault/secret adı | Dosya/ortam başına |
| Throttling | Tier başına istek kotası | İşlem başına istek sınırı | — |

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


### 📝 Bölüm 4.2 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir uygulamanın şu ayarlarını merkezi olarak saklaman gerekiyor: log seviyesi, sayfa boyutu ve bir üçüncü taraf API'nin **anahtarı**. En doğru depolama dağılımı hangisidir?

- A) Üçünü de App Configuration'da düz key-value olarak
- B) Log seviyesi ve sayfa boyutu App Configuration'da; API anahtarı Key Vault'ta (App Configuration'dan Key Vault referansıyla)
- C) Üçünü de Key Vault secret olarak
- D) Üçünü de `appsettings.json`'da

<details><summary>Cevap</summary>

**B.** Gizli olmayan ayarlar App Configuration'da, secret Key Vault'ta tutulur; App Configuration'daki Key Vault referansı uygulamaya tek bir yapılandırma kaynağı sunar. A secret'ı düz metin yapar; C gizli olmayan ayarları gereksiz yere vault işlemi/yetkisiyle yönetir; D merkezi değildir ve değişiklik için yeniden dağıtım gerektirir.
</details>

#### Soru 2: Tek seçim
Aynı `Api:BaseUrl` anahtarının Dev ve Prod ortamlarında farklı değeri olacak ve ikisi aynı store'da duracak. Hangisi bunu sağlar?

- A) İki ayrı key adı (`Api:BaseUrlDev`) kullanmak zorunludur
- B) Anahtara **label** vermek (`Dev`, `Prod`)
- C) Content type değiştirmek
- D) Snapshot oluşturmak

<details><summary>Cevap</summary>

**B.** Label aynı anahtarın varyantlarını ayırır; sağlayıcıda `Select("Api:*", labelFilter: env)` ile ortam seçilir. Content type değerin türüdür (C); snapshot bir kümenin değiştirilemez kopyasıdır, ortam ayrımı için değildir (D). Ayrı anahtar adı çalışır ama label'ın tam amacıdır ve kod değişikliği gerektirir (A zorunlu değildir).
</details>

#### Soru 3: Tek seçim
Bir ASP.NET Core uygulaması App Configuration'dan ayar yüklüyor. Ayarlar **birlikte, tutarlı** güncellenmeli ve uygulama yeniden başlatılmamalı. Hangi kalıp önerilir?

- A) Her anahtarı ayrı ayrı `Register` edip kısa aralıkla izlemek
- B) Tek bir **sentinel key** kaydetmek ve `refreshAll: true` kullanmak; sentinel'i değerlerden **sonra** güncellemek
- C) `IOptions<T>` kullanıp her gün restart planlamak
- D) Access key'i döndürmek

<details><summary>Cevap</summary>

**B.** Tek anahtar izlendiği için istek sayısı düşük kalır; sentinel en son yükseltilince uygulama tutarlı bir kümeyi bir arada yükler. A pahalıdır ve yarı güncel küme riski taşır; C yenileme sağlamaz; D ilgisizdir.
</details>

#### Soru 4: Kod tamamlama (C#)
Boşluğu doldur; sentinel değişince **tüm** seçili yapılandırma yeniden yüklensin:

```csharp
options.Connect(endpoint, new DefaultAzureCredential())
       .Select("App:*", env)
       .ConfigureRefresh(refresh => refresh
           .Register("App:Sentinel", env, ______: true)
           .SetRefreshInterval(TimeSpan.FromSeconds(30)));
```

- A) `refreshAll`
- B) `reloadAll`
- C) `forceRefresh`
- D) `cacheExpiration`

<details><summary>Cevap</summary>

**A.** `Register(key, label, refreshAll: true)` — `refreshAll` false (varsayılan) ise yalnızca kayıtlı anahtarın kendisi yenilenir. Diğerleri API'de bu anlamda yoktur; yenileme aralığı `SetRefreshInterval` ile verilir.
</details>

#### Soru 5: Evet / Hayır

Bir ASP.NET Core uygulaması `AddAzureAppConfiguration` ile dinamik yenileme kurmuş.

| İfade | Evet | Hayır |
|---|---|---|
| a) `app.UseAzureAppConfiguration()` eklenmezse yenileme kontrolleri tetiklenmez. | ☐ | ☐ |
| b) Yenileme kontrolü, aralık dolduktan sonra gelen bir istekle tetiklenir; uygulama tamamen boştaysa kontrol yapılmaz. | ☐ | ☐ |
| c) `IOptions<T>` (singleton) ile okunan değer yenilemeden sonra otomatik güncellenir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** ASP.NET Core'da yenileme middleware tarafından istek hattında tetiklenir (poll tabanlı, yenileme aralığına bağlı). `IOptions<T>` ilk değeri tutar; `IOptionsSnapshot<T>` veya `IOptionsMonitor<T>` gerekir.
</details>

#### Soru 6: Tek seçim
Bir App Configuration anahtarı Key Vault referansıdır. Uygulama değeri okurken **403** alıyor; uygulamanın MI'ı App Configuration'da `App Configuration Data Reader` rolüne sahip. Eksik olan nedir?

- A) `App Configuration Data Owner`
- B) Referans verilen vault'ta uygulama kimliği için **Key Vault Secrets User**
- C) Vault için `Contributor`
- D) Store'da soft delete

<details><summary>Cevap</summary>

**B.** Sağlayıcı referansı uygulamanın kimliğiyle **vault'tan** çözer; vault'ta okuma rolü gerekir. Data Owner (A) yazma yetkisidir, sorunu çözmez; Contributor (C) data plane değildir; soft delete (D) erişimle ilgisizdir.
</details>

#### Soru 7: Tek seçim
Yeni bir özelliği **kullanıcıların kabaca %20'sine** açmak ve kodu yeniden dağıtmadan oranı artırmak istiyorsun. Kullanıcı başına tutarlılık gerekmiyor. Hangi filtre en basit çözümdür?

- A) TimeWindow
- B) Percentage
- C) Targeting ile gruplar
- D) Özel `IFeatureFilter`

<details><summary>Cevap</summary>

**B.** `Microsoft.Percentage` filtresi isteklerin yüzde N'i için flag'i açar; oran App Configuration'dan değiştirilir. Tutarlı kullanıcı deneyimi gerekseydi **Targeting** (C) seçilirdi. TimeWindow zaman aralığıdır (A); özel filtre gereksiz karmaşıklıktır (D).
</details>

#### Soru 8: Tek seçim
Pazarlama, kampanya özelliğinin **1 Kasım 09:00 – 7 Kasım 23:59 (UTC)** arasında otomatik açık kalmasını istiyor; sonrasında kod/ayar müdahalesi olmamalı. Hangi feature flag filtresi?

- A) Percentage
- B) Targeting
- C) TimeWindow
- D) Flag'i `enabled=false` yapıp elle açmak

<details><summary>Cevap</summary>

**C.** `Microsoft.TimeWindow` başlangıç/bitiş tarihleriyle flag'i otomatik açıp kapatır. Diğerleri zamandan bağımsızdır; D elle işlem gerektirir.
</details>

#### Soru 9: Çoklu seçim (iki doğru cevap)
200 instance'lı bir serviste 5 saniyelik yenileme aralığı **429** hatalarına yol açıyor. **Hangi iki** değişiklik sorunu çözer?

- A) Yenileme aralığını uzatmak
- B) `Microsoft.AppConfiguration.KeyValueModified` olayını Event Grid ile uygulamaya iletip **push** yenileme kullanmak
- C) `App Configuration Data Owner` rolü vermek
- D) Tüm anahtarları tek bir Key Vault secret'ına taşımak

<details><summary>Cevap</summary>

**A ve B.** Polling istekleri store kotasını tüketir; aralığı uzatmak ve değişiklikleri push ile bildirmek istek sayısını düşürür. Rol, kotayı etkilemez (C); D mimariyi bozar ve vault'un kendi throttling'i vardır.
</details>

#### Soru 10: CLI tamamlama
Bir feature flag'e **%20** yüzde filtresi eklemek için komutu tamamla:

```bash
az appconfig feature filter add -n appcs-demo --auth-mode login --feature NewCheckout --label Prod \
  --filter-name ______ --filter-parameters Value=20
```

- A) `Microsoft.Percentage`
- B) `Microsoft.Targeting`
- C) `Microsoft.TimeWindow`
- D) `Microsoft.Sentinel`

<details><summary>Cevap</summary>

**A.** Percentage filtresi `Value` parametresini alır. Targeting `Audience` nesnesi (kullanıcı/grup/varsayılan yüzde), TimeWindow `Start`/`End` ister. "Sentinel" bir filtre değil, yenileme anahtarı kalıbıdır.
</details>

#### Soru 11: Sıralama
Dinamik yenilemeyi **tutarlı** biçimde tetiklemek için yönetici adımlarını sırala:

1. `App:Sentinel` değerini yükselt
2. `Db:Pool:MaxSize` değerini güncelle
3. `App:Greeting` değerini güncelle
4. Uygulama bir sonraki yenileme aralığında sentinel değişimini görür ve tüm yapılandırmayı yeniden yükler

<details><summary>Cevap</summary>

**2 → 3 → 1 → 4** (2 ve 3 kendi aralarında yer değiştirebilir). Önce asıl değerler, **sentinel en son**; aksi hâlde uygulama sentinel'i görüp yenilerken eski değerleri okuyabilir ve sonraki değişikliklere kadar tutarsız kalır.
</details>

#### Soru 12: Senaryo
Bir release öncesinde Prod ayarlarının (`App:*`, label `Prod`) **değiştirilemez** bir kopyasını dondurmak ve gerekirse ona dönmek istiyorsun. Ne kullanırsın?

- A) Yeni bir label
- B) Snapshot
- C) Key Vault soft delete
- D) Kilitli bir kaynak grubu

<details><summary>Cevap</summary>

**B.** Snapshot, filtre ile seçilen key-value kümesinin değiştirilemez anlık görüntüsüdür; sağlayıcı `SelectSnapshot(...)` ile bu kümeden yükleyebilir. Label (A) değiştirilebilir değerler arasında ayrım yapar; C ve D yapılandırma sürümlemesi değildir.
</details>

#### Soru 13: Vaka çalışması

**Durum.** *Fabrikam Shop*, 8 mikroservisi Container Apps'te çalıştırıyor. Gereksinimler:

1. Servis ayarları (sayfa boyutu, zaman aşımı) kod dağıtılmadan değiştirilebilmeli; yeniden başlatma olmamalı.
2. Prod ayarlarını yalnızca platform ekibi değiştirebilmeli; geliştiriciler Dev ayarlarını yönetebilmeli.
3. Ödeme sağlayıcısının API anahtarı App Configuration'dan okunur görünmeli ama düz metin olarak saklanmamalı.
4. Yeni "tek tıkla ödeme" özelliği önce çalışanlara, sonra müşterilerin %10'una, sonra %50'sine açılmalı; aynı müşteri her ziyaretinde aynı deneyimi görmeli.
5. Servisler yalnızca okuma yetkisiyle çalışmalı ve parola/anahtar taşımamalı.

**Soru A.** Ortam ayrımını nasıl kurarsın?
**Soru B.** Gereksinim 3 ve 5'i hangi bileşenlerle karşılarsın?
**Soru C.** Gereksinim 4 için hangi feature flag filtresi ve neden?

<details><summary>Cevap</summary>

**A.** Yetki ayrımı gerektiği için **ortam başına ayrı store** (Dev ve Prod) ve ayrı RBAC: Prod store'unda yalnızca platform ekibine `App Configuration Data Owner`, Dev store'unda geliştiricilere. Label ile tek store basit olurdu ama yetki store düzeyinde olduğundan Prod/Dev ayrımı sağlanamaz. Yenileme için **sentinel key** (`refreshAll: true`) + `IOptionsMonitor`.

**B.** API anahtarı **Key Vault**'ta secret olarak durur; App Configuration'da **Key Vault referansı** (uri) tutulur → düz metin yok. Servisler **managed identity** kullanır: store'da **App Configuration Data Reader**, vault'ta **Key Vault Secrets User** (okuma, en az yetki); parola/connection string yok (`DefaultAzureCredential`/`ManagedIdentityCredential`).

**C.** **Targeting** filtresi: önce grup (`Employees`) %100, sonra varsayılan yüzde %10 → %50. Karar kullanıcı kimliğine göre tutarlı verilir; Percentage filtresi istek başına rastgele olduğundan aynı müşteri farklı sonuç görebilir ve gereksinimi karşılamaz.
</details>

---

**Puanlama:** 12-13 → hazırsın · 9-11 → README 3.4 (yenileme) ve 3.5 (filtre tablosu) bölümlerini tekrar et · < 9 → konuyu baştan çalış ve lab'ı (3.7) uygula.


---

## 4.3 OpenTelemetry ile İzleme ve KQL ile Sorun Giderme

> **Sınav Alanı:** Secure, monitor, and troubleshoot Azure solutions (%20-25) · **Konu Kodu:** C2.13

### 1. Ne?

İki beceri: **telemetri üretmek** (OpenTelemetry ile enstrümantasyon) ve **telemetriyi sorgulamak** (KQL ile Log Analytics / Application Insights).

**OpenTelemetry (OTel)** satıcıdan bağımsız, CNCF'in gözlemlenebilirlik standardıdır: API + SDK + exporter + wire protokolü (OTLP). Üç sinyal:

| Sinyal | Ne? | .NET karşılığı | Python karşılığı |
|---|---|---|---|
| **Trace** | Bir isteğin servisler arası yolu; **span** ağacı | `ActivitySource` / `Activity` | `tracer.start_as_current_span` |
| **Metric** | Zamanla ölçülen sayısal değer (counter, histogram, gauge) | `System.Diagnostics.Metrics.Meter` | `meter.create_counter` |
| **Log** | Zaman damgalı olay kaydı; trace'e bağlanır (`TraceId`/`SpanId`) | `ILogger` | `logging` |

| Kavram | Tek cümle |
|---|---|
| **Span** | Tek bir işin süresi + öznitelikleri (ad, başlangıç/bitiş, durum, `kind`: Server/Client/Producer/Consumer/Internal) |
| **Trace** | Aynı **trace-id**'yi taşıyan span'ler topluluğu (uçtan uca istek) |
| **Context propagation** | trace-id/span-id'nin süreç ve servis sınırlarını **geçirilmesi** — HTTP'de `traceparent` header'ı, mesajda mesaj özelliği |
| **W3C Trace Context** | `traceparent: 00-<trace-id 32 hex>-<parent-span-id 16 hex>-<flags>` (`01` = sampled) ve `tracestate` |
| **Resource** | Telemetriyi üreten varlığın kimliği: `service.name`, `service.instance.id` (App Insights'ta `cloud_RoleName`/`AppRoleName`) |
| **Sampler** | Hangi trace'lerin kaydedileceğine karar verir (trace-id'ye göre → aynı trace tüm servislerde **tutarlı**) |
| **Exporter** | Telemetriyi hedefe yollar (Azure Monitor, OTLP → Collector) |
| **Azure Monitor OpenTelemetry Distro** | OTel SDK + otomatik enstrümantasyon + Azure Monitor exporter'ı **tek çağrıda** (`UseAzureMonitor()` / `configure_azure_monitor()`) |

**Application Insights**, Azure Monitor'ün APM özelliğidir; **workspace-based** olarak telemetriyi bir **Log Analytics workspace**'te saklar, yani aynı veriyi **KQL** ile sorgularsın.

---

### 2. Neden?

- **Sınav:** Hedef iki yönlüdür: tracing/OTel kavramları (trace–span–context, sampling) ve **KQL** (okuma + eksik operatörü tamamlama). Kod tamamlama: `UseAzureMonitor()`, `configure_azure_monitor()`, `ActivitySource`; KQL: `summarize ... by bin(timestamp, 5m)`.
- **Gerçek işte:** Mikroservis + kuyruk mimarisinde tek bir log dosyası hatayı bulmaz; **hangi servis, hangi bağımlılık, hangi istek** sorusunu yalnızca korelasyonlu trace cevaplar. OTel ile satıcıya kilitlenmeden (Azure Monitor → başka backend) enstrümante edersin.
- **Sınavın "monitor" kısmı:** Hata oranı alert'i, action group, "en yavaş bağımlılık", `operation_Id` ile uçtan uca trace — sorun giderme akışı. Gözlemlenebilirlik teorisi → M13; sampling derinliği → M13.3.

---

### 3. Nasıl?

#### 3.1 Veri akışı ve Application Insights tabloları

```
 API (UseAzureMonitor) ──traceparent──► Service Bus mesajı ──► Worker (UseAzureMonitor)
        │  spans/metrics/logs                                        │
        └────────────────► Azure Monitor exporter ◄──────────────────┘
                                  │  (connection string)
                                  ▼
                    Application Insights  ──►  Log Analytics workspace  ──► KQL / Alerts / Workbooks
```

> **Connection string** kullan (`InstrumentationKey=…;IngestionEndpoint=…`); yalnızca instrumentation key ile veri alımı **sonlandırıldı** (2025). Ortam değişkeni: `APPLICATIONINSIGHTS_CONNECTION_STRING`.

| Klasik (App Insights **Logs**) | Workspace tablosu (Log Analytics) | Ne? |
|---|---|---|
| `requests` | `AppRequests` | Gelen istek / iş (Server, Consumer span) |
| `dependencies` | `AppDependencies` | Giden çağrı: HTTP, SQL, Service Bus, Cosmos (Client/Producer span) |
| `exceptions` | `AppExceptions` | İstisnalar |
| `traces` | `AppTraces` | `ILogger` / log kayıtları |
| `customMetrics` | `AppMetrics` | Özel metrikler (`Meter`) |
| `customEvents` | `AppEvents` | Özel olaylar |
| `pageViews`, `availabilityResults` | `AppPageViews`, `AppAvailabilityResults` | Tarayıcı / availability test |

Sütun eşlemesi (en sık kullanılanlar):

| Klasik | Workspace | Anlamı |
|---|---|---|
| `timestamp` | `TimeGenerated` | Zaman (UTC) |
| `name` | `Name` | İstek/bağımlılık adı |
| `duration` (ms) | `DurationMs` | Süre |
| `success` | `Success` | Başarı (bool) |
| `resultCode` | `ResultCode` | HTTP/sonuç kodu |
| `operation_Id` | `OperationId` | **Trace-id** — uçtan uca kimlik |
| `id` / `operation_ParentId` | `Id` / `ParentId` | Span-id / üst span-id |
| `cloud_RoleName` | `AppRoleName` | Servis adı (`service.name`) |
| `itemCount` | `ItemCount` | Örnekleme sonrası temsil edilen kayıt sayısı |
| `customDimensions` | `Properties` | Özel öznitelikler (dynamic) |

> Sınav ekranları App Insights **Logs** bölümündeki **klasik** adları (`requests`, `timestamp`, `duration`) da, Log Analytics'teki **`App*`** adlarını da kullanabilir; ikisini de oku.

#### 3.2 Enstrümantasyon: Distro, otomatik ve elle

```csharp
// Program.cs — API (ASP.NET Core). NuGet: Azure.Monitor.OpenTelemetry.AspNetCore
using Azure.Monitor.OpenTelemetry.AspNetCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

AppContext.SetSwitch("Azure.Experimental.EnableActivitySource", true);   // messaging SDK tracing is opt-in

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService(serviceName: "orders-api"))        // -> cloud_RoleName
    .UseAzureMonitor(o =>
    {
        o.ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];
        o.SamplingRatio = 0.2f;        // keep ~20 % of traces (fixed percentage); see 3.5
        // o.TracesPerSecond = 5;      // alternative: rate-limited sampling
    })
    .WithTracing(t => t.AddSource(Telemetry.ActivitySourceName,   // our own spans
                                  "Azure.*"));                     // Azure SDK spans (Service Bus, ...)

var app = builder.Build();
app.MapPost("/orders", async (OrderDto dto, ServiceBusSender sender, ILogger<Program> log) =>
{
    using var activity = Telemetry.Source.StartActivity("orders.create");   // manual span (Internal)
    activity?.SetTag("order.id", dto.Id);
    activity?.SetTag("order.amount", dto.Amount);

    await sender.SendMessageAsync(new ServiceBusMessage(BinaryData.FromObjectAsJson(dto)));
    // the Azure SDK adds a Producer span and writes the trace context into the message ("Diagnostic-Id")
    log.LogInformation("Order {OrderId} queued", dto.Id);   // log is correlated with the active trace
    return Results.Accepted();
});
app.Run();

public static class Telemetry
{
    public const string ActivitySourceName = "Orders.Api";
    public static readonly ActivitySource Source = new(ActivitySourceName);
}
```

```csharp
// Worker (Generic Host). Same call; the processor continues the trace from the message.
builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("orders-worker"))
    .UseAzureMonitor()
    .WithTracing(t => t.AddSource("Orders.Worker", "Azure.*"));

processor.ProcessMessageAsync += async args =>
{
    using var a = WorkerTelemetry.Source.StartActivity("orders.process");   // child of the message's trace context
    // ... call DB / HTTP -> shows up as dependencies under the same operation_Id
    await args.CompleteMessageAsync(args.Message);
};
```

- **Otomatik enstrümantasyon:** ASP.NET Core (gelen istek), `HttpClient`, SQL client; Azure SDK'lar (kararlı olanlar) — Distro etkin tutar. **Messaging SDK'ları (Service Bus, Event Hubs)** için ActivitySource desteği **deneysel**dir ve ortam değişkeni `AZURE_EXPERIMENTAL_ENABLE_ACTIVITY_SOURCE=true` / `AppContext` anahtarı gerektirir *(2026 başı itibarıyla; Azure.Messaging.ServiceBus sürüm notlarından doğrula)*.
- **Elle enstrümantasyon:** `ActivitySource` + `StartActivity` + `SetTag`/`AddEvent`/`SetStatus`; metrik için `Meter.CreateCounter<long>("orders.created")`.
- **Context propagation:** HTTP'de `traceparent` otomatik; **Service Bus**'ta gönderen mesaja `Diagnostic-Id` (W3C traceparent) özelliğini yazar, işleyici bunu çıkarır → API'deki istek ve worker'daki işlem **aynı `operation_Id`** altında görünür. Propagation'ın otomatik olmadığı kanallarda (ör. custom protokol) `traceparent`'i `Propagators` ile elle enjekte/çıkar.
- **Application Insights SDK (klasik)** ile fark: yeni projelerde **OTel Distro** önerilir; klasik SDK'nın telemetri modeli (`TelemetryClient.TrackEvent`) ve OTel'in span modeli farklıdır; eşleme tablosu yukarıdadır.
- *Not:* Microsoft yeni bir birleşik API (`UseMicrosoftOpenTelemetry`) üzerinde de çalışıyor; `UseAzureMonitor()` sınavda hâlâ kanonik çağrıdır — paket sürümüne göre doğrula.

```python
# Python (azure-monitor-opentelemetry). Call BEFORE importing frameworks you want auto-instrumented.
import os
from azure.monitor.opentelemetry import configure_azure_monitor
from opentelemetry import trace, metrics

configure_azure_monitor(
    connection_string=os.environ["APPLICATIONINSIGHTS_CONNECTION_STRING"],
    # sampling_ratio=0.2,            # fixed-percentage; newer versions also support traces_per_second
)

tracer = trace.get_tracer("orders.api")
orders_created = metrics.get_meter("orders.api").create_counter("orders.created")

def create_order(order):
    with tracer.start_as_current_span("orders.create") as span:       # manual span
        span.set_attribute("order.id", order["id"])
        span.set_attribute("order.amount", order["amount"])
        # servicebus_sender.send_messages(...)  # context propagates when Azure SDK tracing is enabled
        orders_created.add(1, {"channel": "web"})
```

#### 3.3 KQL temelleri ⭐

KQL bir **boru hattı**dır: tablo `|` operatör `|` operatör. Her adım bir önceki adımın çıktısını işler.

| Operatör | Ne yapar? | Not |
|---|---|---|
| `where` | Satırları süzer | Zaman filtresini **en başa** koy (`where timestamp > ago(1h)`) → hızlı |
| `project` | Sütun seç / yeniden adlandır | `project-away` çıkarır, `project-rename` adlandırır |
| `extend` | **Hesaplanan** sütun ekler | Satırı değiştirmez, sütun ekler |
| `summarize … by` | Gruplayıp **topla** (`count()`, `sum()`, `avg()`, `percentile()`, `dcount()`, `countif`, `sumif`, `arg_max`) | Zaman kovası: `by bin(timestamp, 5m)` |
| `top N by x` | Sırala + ilk N | `take N` = **rastgele** N satır (sıralamaz) |
| `order by` / `sort by` | Sırala | Varsayılan **desc** |
| `join kind=… ` | İki tabloyu anahtarla birleştir | **Varsayılan `innerunique`** (sol anahtarı tekilleştirir!) — beklenmedik satır kaybı; `kind=inner` açıkça yaz |
| `union` | Tabloları alt alta ekler | Sütunlar birleşir |
| `let` | Değişken/alt sorgu | `let window = 5m;` |
| `render timechart` | Grafik | Zaman ekseni gerekir (`bin` ile) |
| `parse_json`, `tostring`, `todynamic` | `dynamic` (customDimensions) alanları | `tostring(customDimensions.orderId)` |

String ve karşılaştırma tuzakları: `==` **büyük/küçük harfe duyarlı**, `=~` duyarsız; `has` (tam terim, **indeksli, hızlı**) ↔ `contains` (alt dize, yavaş); `in`, `!in`, `between (a .. b)`. Zaman: `ago(1h)`, `now()`, `startofday()`, **her zaman UTC**.

```kusto
// 1) Slowest 5 endpoints by p95 latency (last hour)  -- classic App Insights names
requests
| where timestamp > ago(1h)
| summarize p95 = percentile(duration, 95), avg_ms = avg(duration), calls = sum(itemCount) by name
| top 5 by p95 desc

// 2) Error rate every 5 minutes
requests
| where timestamp > ago(1h)
| summarize total = sum(itemCount), failed = sumif(itemCount, success == false) by bin(timestamp, 5m)
| extend errorRatePct = round(100.0 * failed / total, 2)
| project timestamp, errorRatePct
| render timechart

// 3) End-to-end flow of one operation (API -> Service Bus -> worker -> DB)
union requests, dependencies, exceptions, traces
| where operation_Id == "4bf92f3577b34da6a3ce929d0e0e4736"
| project timestamp, itemType, cloud_RoleName, name = coalesce(name, message, outerMessage),
          duration, success, id, operation_ParentId
| order by timestamp asc

// 4) Which dependency is slow? (database, queue, HTTP ...)
dependencies
| where timestamp > ago(1h)
| summarize p95 = percentile(duration, 95), failures = sumif(itemCount, success == false), calls = sum(itemCount)
            by type, target
| top 5 by p95 desc

// 5) Failed requests together with their exceptions (explicit join kind)
requests
| where timestamp > ago(1h) and success == false
| join kind=inner (exceptions | where timestamp > ago(1h)) on operation_Id
| project timestamp, name, resultCode, exceptionType = type, outerMessage, operation_Id

// 6) Custom metric (Meter) per hour
customMetrics
| where name == "orders.created"
| summarize orders = sum(valueSum) by bin(timestamp, 1h)
| render columnchart
```

Aynı sorgular Log Analytics'te workspace adlarıyla: `AppRequests | where TimeGenerated > ago(1h) | summarize p95 = percentile(DurationMs, 95) by Name | top 5 by p95 desc`.

**Sonuç okuma kuralları (sınav sorusu kalıbı):**
- `summarize count() by bin(timestamp, 5m)` yalnızca **verisi olan** kovalar için satır döner (boş kova = satır yok; boşlukları göstermek için `make-series`).
- `summarize` sonrası yalnızca `by` sütunları + hesaplanan sütunlar kalır; `where` summarize'dan **sonra** gelirse toplanmış değeri süzer.
- `top 5 by p95 desc` ≠ `take 5`.
- `percentile(duration, 95)` tek sayı; birden çok yüzdelik için `percentiles(duration, 50, 95, 99)`.
- Örneklenmiş veride **`count()` eksik sayar**; `sum(itemCount)` kullan (3.5).

**Platform logları (workspace tabloları):**

| Kaynak | Tablo (2026 başı itibarıyla — doğrula) | Örnek |
|---|---|---|
| Container Apps | `ContainerAppConsoleLogs_CL` / kaynağa özgü `ContainerAppConsoleLogs`; `ContainerAppSystemLogs` | `ContainerAppConsoleLogs_CL \| where ContainerAppName_s == "orders-api" and Log_s has "error"` |
| AKS (Container Insights) | `ContainerLogV2`, `KubePodInventory`, `KubeEvents` | `ContainerLogV2 \| where PodName startswith "orders" and LogMessage has "Exception"` |
| Azure kaynak logları/metrikleri | `AzureDiagnostics`, kaynağa özgü tablolar, `AzureMetrics`, `AzureActivity` | `AzureActivity \| where OperationNameValue has "delete"` |
| Key Vault denetim | `AZKVAuditLogs` / `AzureDiagnostics` | kim hangi secret'ı okudu (→ [C2.11](#41-managed-identity-ve-azure-key-vault)) |

#### 3.4 Uyarılar (alerts) ve sorun giderme akışı

| Alert türü | Veri | Gecikme/maliyet | Ne zaman? |
|---|---|---|---|
| **Metric alert** | Platform/özel **metrik** | Dakika düzeyi, ucuz | Eşik: CPU, istek sayısı, kuyruk uzunluğu |
| **Log search alert** (scheduled query rule) | **KQL** sonucu | Ingestion gecikmesi + değerlendirme aralığı; her değerlendirme ücretli | "Hata oranı > %5", karmaşık koşul |
| **Activity log alert** | Control plane olayı | — | "Kaynak silindi" |
| Smart detection | Application Insights anormallik | — | Otomatik, ayar gerektirmez |

**Action group:** bildirim/eylem listesi — e-posta, SMS, push, sesli arama, **webhook, Azure Function, Logic App**, ITSM, Automation runbook. Alert kuralı bir veya birden çok action group'a bağlanır; **alert processing rule** bakım penceresinde bastırır.

**Sorun giderme akışı (sınavın kalıbı):**
1. **Belirti:** hata oranı alert'i / Failures blade → *hangi endpoint, hangi sonuç kodu?* (sorgu 2, `resultCode` kırılımı)
2. **Daralt:** *Hangi bağımlılık yavaş/başarısız?* (sorgu 4: `dependencies` → `target`/`type`)
3. **Örnek trace:** bir başarısız isteğin `operation_Id`'si → **end-to-end transaction** / sorgu 3
4. **Neden:** `exceptions` (`outerMessage`, stack), ilgili `traces`, dağıtım/yapılandırma değişikliği (`AzureActivity`)
5. **Doğrula:** düzeltmeden sonra hata oranı normale dönüyor mu?

#### 3.5 Sampling

- **Sampling**, ingestion maliyetini düşürmek için telemetrinin yalnızca bir kısmını **saklar**. Karar **trace-id**'ye göre verilir → bir trace ya **tamamen** ya da hiç kaydedilmez (parent-based: alt servisler ebeveynin kararına uyar). `traceparent` flag'i (`01`) örneklendi demektir.
- Distro seçenekleri: **sabit yüzde** (`SamplingRatio`) veya **hız sınırlı** (`TracesPerSecond`, trafik arttıkça oran düşer). Hangisinin varsayılan olduğu sürüme göre değişir (rate-limited ≈ 5 trace/sn belgelenmiştir — doğrula); ortam değişkenleri: `OTEL_TRACES_SAMPLER` (`microsoft.rate_limited` / `microsoft.fixed_percentage`), `OTEL_TRACES_SAMPLER_ARG`.
- **Etkisi:** `requests | count` gerçek istek sayısını **eksik** gösterir → `sum(itemCount)`; p95 gibi dağılımlar örneklemde bile makul kalır, **nadir hatalar** kaybolabilir (tail-based sampling OTel Collector gerektirir). Metrikler (`customMetrics`/standart metrikler) örneklenmez → alert'leri **metrik** üzerine kurmak daha güvenilirdir.
- Sampling'i **tüm servislerde aynı** kararla kullan; aksi hâlde trace'ler kopuk görünür.

#### 3.6 Lab — API + worker'ı enstrümante et, KQL setini çalıştır, hata oranı alert'i kur

Maliyet: Application Insights/Log Analytics **GB başına** ücretlenir (ilk ay/aylık ücretsiz kota bulunur — doğrula); lab verisi KB'dır. Daily cap koy, **bütçe alarmı** kur (→ C1.11). Service Bus'ı **emülatörle** (C2.9) çalıştırabilirsin; telemetri gerçek bir Application Insights kaynağına gider (emülatörü yok).

```bash
RG=rg-c213-lab
LOC=westeurope
az group create --name $RG --location $LOC
az extension add --name application-insights --upgrade --yes
az extension add --name scheduled-query --upgrade --yes
az extension add --name log-analytics --upgrade --yes

# 1) Workspace (with a daily cap) + workspace-based Application Insights
az monitor log-analytics workspace create -g $RG -n law-c213 -l $LOC --quota 1      # 1 GB/day cap
WS_ID=$(az monitor log-analytics workspace show -g $RG -n law-c213 --query id -o tsv)
az monitor app-insights component create --app appi-c213 -g $RG -l $LOC --workspace $WS_ID --kind web
CS=$(az monitor app-insights component show --app appi-c213 -g $RG --query connectionString -o tsv)

# 2) Run API + worker locally with the connection string (Service Bus emulator from C2.9)
export APPLICATIONINSIGHTS_CONNECTION_STRING="$CS"
#   dotnet run --project Orders.Api   &   dotnet run --project Orders.Worker
#   for i in $(seq 1 50); do curl -s -X POST localhost:5000/orders -H 'content-type: application/json' \
#        -d "{\"id\":\"o-$i\",\"amount\":$((i*30))}"; done
#   force failures: make the worker throw for amount > 1200 (like C2.9's poison messages)

# 3) Query (data appears after ~2-5 minutes of ingestion latency)
WS_CUSTOMER_ID=$(az monitor log-analytics workspace show -g $RG -n law-c213 --query customerId -o tsv)
az monitor log-analytics query -w $WS_CUSTOMER_ID -o table --analytics-query "
AppRequests
| where TimeGenerated > ago(1h)
| summarize p95 = percentile(DurationMs, 95), calls = sum(ItemCount) by Name
| top 5 by p95 desc"

# 4) Action group (e-mail) + log search alert: error rate > 5 % in the last 5 minutes
AG_ID=$(az monitor action-group create -g $RG -n ag-c213 --short-name ag213 \
          --action email oncall oncall@example.com --query id -o tsv)
az monitor scheduled-query create -g $RG -n alert-error-rate --scopes $WS_ID \
  --condition "count 'Q1' > 0" \
  --condition-query Q1="AppRequests
      | summarize total = sum(ItemCount), failed = sumif(ItemCount, Success == false)
      | where total > 20 and 100.0 * failed / total > 5" \
  --window-size 5m --evaluation-frequency 5m --severity 2 \
  --action-groups $AG_ID --description "Request failure rate above 5 percent"

# 5) Cleanup (workspace data is deleted with the group; soft-delete of the workspace may hold the name ~14 days)
az group delete --name $RG --yes --no-wait
```

Sonra portalda **Application Insights → Application map** (API → Service Bus → worker), **Transaction search** → bir isteğe tıkla → *end-to-end transaction* (aynı `operation_Id`), ve **Logs** bölümünde 3.3'teki altı sorguyu çalıştır. Worker'ı `amount > 1200` için hata fırlatacak şekilde ayarladıysan 5 dakikalık hata oranı grafiğinde (sorgu 2) sıçramayı ve alert'in tetiklenmesini gözlemle.

> **Alıştırma.** Sorgu: `requests | where timestamp > ago(30m) | summarize n = count() by bin(timestamp, 10m)` 3 satır döndürmesi beklenirken 2 döndürdü. İlk akla gelen iki neden?
>
> <details><summary>Çözüm</summary>
>
> (1) Bir kovada **hiç istek yok** — `summarize` boş kovaları yazmaz (`make-series` ile doldurulur). (2) Zaman penceresi kovalara tam oturmaz (`ago(30m)` ilk kovayı kısmen keser) veya telemetri **ingestion gecikmesi** yüzünden son kova henüz gelmemiştir. Örnekleme ise satır sayısını değil, `n` değerini (eksik sayma) etkiler → `sum(itemCount)`.
> </details>

---

### 4. Gerçek dünya senaryosu

Bir sipariş sisteminde `orders-api` (Container Apps) siparişi Service Bus'a atar, `orders-worker` işler ve PostgreSQL'e yazar. Gece yarısı kampanya sırasında müşteriler "sipariş onayı gelmiyor" diyor:

| Adım | Eylem | Bulgu |
|---|---|---|
| 1. Alert | "Hata oranı > %5" log alert'i action group ile e-posta + Teams webhook'u yolladı | 00:12'de başladı |
| 2. Belirti | Sorgu 2 (5 dk hata oranı) | Yalnızca **worker**'da `AppRoleName == "orders-worker"` hataları var; API 202 dönüyor |
| 3. Daralt | Sorgu 4 (`dependencies` → p95/failure by `target`) | PostgreSQL `target` p95 8 sn, timeout'lar |
| 4. Örnek trace | Başarısız bir `operation_Id` → sorgu 3 | API span → Service Bus producer → worker consumer → SQL span (süre 30 sn, hata) **tek trace**'te |
| 5. Neden | `exceptions` + `AppTraces` | Connection pool tükenmiş; kampanyada worker ölçeği 20 replikaya çıkmış |
| 6. Çözüm | Pool boyutu/`MaxConcurrentCalls` ayarı (→ C2.12 ile canlı yenileme), KEDA `maxReplicas` sınırı (→ C2.2) | Hata oranı normale döndü |
| 7. Önlem | Metrik tabanlı alert (kuyruk uzunluğu), sampling oranı gözden geçirildi (`sum(itemCount)`) | — |

Mesaj kuyruğu olmasaydı bu zinciri bağlayan **context propagation** olmadan API'de başarı, worker'da hata iki ayrı, ilişkisiz kayıt olarak kalırdı.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** (a) Verilen KQL'in sonucunu okuma, (b) eksik operatörü tamamlama, (c) OTel kavramlarını eşleştirme, (d) sorun giderme senaryosunda doğru adım/araç, (e) `UseAzureMonitor`/`configure_azure_monitor` kod tamamlama.

| Senaryodaki ifade | Cevap |
|---|---|
| "5 dakikalık dilimlerde hata oranı" | `summarize … by bin(timestamp, 5m)` + `extend` oran |
| "En yavaş 5 endpoint (p95)" | `summarize percentile(duration, 95) by name` + `top 5 by … desc` |
| "Tek isteğin tüm servislerdeki izi" | **`operation_Id`** ile `union requests, dependencies, …` |
| "Yeni sütun hesapla" | `extend` (`project` sütun **seçer**) |
| "Sonucu grafik olarak göster" | `render timechart` (zaman ekseni için `bin`) |
| "İlk N satır sıralı" | `top N by` (`take` rastgele) |
| "İki tabloyu birleştir, satır kaybı olmasın" | `join kind=inner` (varsayılan `innerunique` tuzağı) |
| "Servisler arası istek korelasyonu" | **W3C `traceparent`** / context propagation |
| "Kodu değiştirmeden/az kodla Azure Monitor'e telemetri" | **Azure Monitor OpenTelemetry Distro** |
| "Özel iş adımını trace'te göster" | Elle span: `ActivitySource.StartActivity` |
| "Maliyeti düşür, trace'ler bütün kalsın" | **Sampling** (trace-id tabanlı, tüm servislerde aynı) |
| "Örneklemeli veride gerçek istek sayısı" | `sum(itemCount)` |
| "Hata oranı eşiğini aşınca e-posta" | **Log search alert** (veya metric alert) + **action group** |
| "Log/metrik/trace'i tek yerde sorgula" | **Log Analytics workspace** + KQL |
| "Container Apps konsol logu" | `ContainerAppConsoleLogs` tablosu |

**Tuzaklar:**
- **Trace ≠ span ≠ log:** trace = span'ler topluluğu; log trace'e bağlanabilir ama span değildir.
- **Context propagation yoksa trace kopar** (kuyruk, custom protokol). Mesajlaşmada SDK'nın tracing'i açık olmalı.
- `count()` örneklenmiş telemetride **eksik** sayar.
- `summarize` yalnızca verisi olan kovaları döner.
- `project` yeni hesaplama için değil, seçim içindir (`extend`); `where` summarize'dan önce/sonra anlamı değiştirir.
- `has` ile `contains`, `==` ile `=~` farkı.
- Distro'nun varsayılan sampler'ı sürüme göre değişir; üretimde oranı `SamplingRatio`/`TracesPerSecond` ile **açıkça** ayarla.
- Alert için **ingestion gecikmesi**: log alert'i gerçek zamanlı değildir; hızlı tepki gerekiyorsa metrik.
- Instrumentation key değil **connection string**.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| `configure_azure_monitor()`'ü framework import'larından **sonra** çağırmak | Önce çağır; otomatik enstrümantasyon patch'i erken ister |
| `ActivitySource`'u `AddSource` ile kaydetmemek | Kaydedilmeyen kaynağın span'leri **dinlenmez/yollanmaz** |
| Worker'da distro eklememek | API'de iz var, worker'da yok → iki ayrı iz |
| Servis adını vermemek | Hepsi `unknown_service` → Application map okunmaz; `service.name` ver |
| Örneklemeli veride `count()` ile iş metriği hesaplamak | `sum(itemCount)` veya özel metrik (örneklenmez) |
| Her şeyi `traces` olarak loglamak | Yapılandırılmış log + metrik; trace/dependency otomatik gelir |
| `join` varsayılanına güvenmek | `kind=inner/leftouter` yaz |
| Zaman filtresini sorgunun sonuna koymak | `where timestamp > ago(…)` en başa |
| Çok geniş zaman aralığıyla tüm tabloyu taramak | Dar aralık + `project` ile sütun azalt |
| Hata alert'ini tek log satırına bağlamak | Oran/eşik + pencere; `total > N` koşulu ile gürültüyü ele |
| Kişisel veriyi `SetTag`/log'a yazmak | PII'yi telemetriye koyma; maskele |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Sampling oranını düşürmek | Ingestion maliyeti ↓ | Nadir hata ve kuyruk istatistiği kaybı; `count()` yanıltır |
| %100 sampling | Tam görünürlük | Maliyet ve depolama |
| Otomatik enstrümantasyon | Sıfıra yakın kod | Kontrolsüz hacim (gürültülü bağımlılıklar); filtre gerekebilir |
| Elle span'ler | İş anlamı (sipariş no, tutar) | Kod yükü; yüksek kardinalite öznitelikleri maliyeti artırır |
| Log alert ↔ metric alert | Log: esnek KQL koşulu · Metric: hızlı ve ucuz | Log: gecikme + değerlendirme ücreti · Metric: yalnızca metrik |
| Doğrudan Azure Monitor exporter ↔ **OTel Collector** | Collector: yönlendirme, tail sampling, çoklu backend | Ek bileşen işletimi |
| Uzun retention | Geçmiş analiz | Depolama ücreti; arşiv/Basic logs katmanları |
| `summarize` ile ön hesap | Hızlı panolar | Ayrıntıdan vazgeçme; ham veri gerekirse kaynak tabloya dön |

---

### 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Azure Monitor OpenTelemetry — enable for .NET, Node.js, Python, Java](https://learn.microsoft.com/en-us/azure/azure-monitor/app/opentelemetry-enable) · [Configure OpenTelemetry in Application Insights](https://learn.microsoft.com/en-us/azure/azure-monitor/app/opentelemetry-configuration) (sampling, connection string)
- [Azure Monitor Distro client library for .NET](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/monitor.opentelemetry.aspnetcore-readme) · [azure-monitor-opentelemetry (Python)](https://learn.microsoft.com/en-us/python/api/overview/azure/monitor-opentelemetry-readme)
- [Azure SDK for .NET — distributed tracing / diagnostics](https://learn.microsoft.com/en-us/dotnet/azure/sdk/observability) · [Service Bus tracing sample](https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/servicebus/Azure.Messaging.ServiceBus/samples/Sample19_DistributedTracing.md)
- [OpenTelemetry concepts](https://opentelemetry.io/docs/concepts/) · [W3C Trace Context](https://www.w3.org/TR/trace-context/)
- [Application Insights telemetry data model](https://learn.microsoft.com/en-us/azure/azure-monitor/app/data-model-complete) · [Log Analytics tables reference (AppRequests…)](https://learn.microsoft.com/en-us/azure/azure-monitor/reference/tables/apprequests)
- [KQL overview](https://learn.microsoft.com/en-us/kusto/query/) · [KQL quick reference](https://learn.microsoft.com/en-us/kusto/query/kql-quick-reference)
- [Azure Monitor alerts](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-overview) · [Action groups](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/action-groups) · [`az monitor scheduled-query`](https://learn.microsoft.com/en-us/cli/azure/monitor/scheduled-query)
- [Container Apps logging](https://learn.microsoft.com/en-us/azure/container-apps/log-monitoring)

> **Doğrulama notu:** Distro'nun varsayılan sampler'ı, Service Bus ActivitySource'unun deneysel bayrağı, Container Apps tablo adları ve Python parametre adları (`sampling_ratio` / `traces_per_second`) sürüme göre değişir; sınav öncesi güncel sayfaları kontrol et. Kavramlar (trace–span–context, `summarize … bin()`, `operation_Id`) kararlıdır.


### 📝 Bölüm 4.3 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir API, Service Bus'a mesaj gönderiyor; worker mesajı işliyor. Her iki servis de OpenTelemetry ile enstrümante edildi, ama Application Insights'ta API'deki istek ile worker'daki işlem **ayrı trace'ler** olarak görünüyor. En olası neden?

- A) Application Insights Service Bus'ı desteklemez
- B) Trace context (traceparent) mesajla taşınmıyor; mesajlaşma SDK'sı tracing'i etkin değil
- C) Log Analytics workspace'i yanlış bölgede
- D) Sampling oranı %100

<details><summary>Cevap</summary>

**B.** Servisler arasında kuyruk üzerinden trace'i bağlayan şey **context propagation**'dır: gönderen trace context'i mesaj özelliğine yazar, işleyici çıkarır. Messaging SDK'sının ActivitySource desteği etkin değilse (veya `AddSource` ile dinlenmiyorsa) bu bağ kopar. Bölge (C) ve %100 sampling (D) trace'i bölmez; A yanlıştır.
</details>

#### Soru 2: Eşleştirme
Kavramları açıklamalarla eşleştir:

| Kavram | Açıklama |
|---|---|
| 1. Span | a) trace-id'yi servis sınırları arasında taşıma mekanizması |
| 2. Trace | b) tek bir işin süre ve öznitelikleriyle kaydı |
| 3. Context propagation | c) aynı trace-id'yi taşıyan span'ler topluluğu |
| 4. Sampler | d) hangi trace'lerin kaydedileceğine karar veren bileşen |

<details><summary>Cevap</summary>

**1-b, 2-c, 3-a, 4-d.** Span tek bir iştir, trace span'lerin uçtan uca ağacıdır, propagation (HTTP'de `traceparent` header'ı) trace-id'yi sınırlardan geçirir, sampler kayıt kararını verir.
</details>

#### Soru 3: Tek seçim
`traceparent: 00-4bf92f3577b34da6a3ce929d0e0e4736-00f067aa0ba902b7-01` header'ında **trace-id** hangisidir ve Application Insights'ta hangi alanla eşleşir?

- A) `00f067aa0ba902b7` — `id`
- B) `4bf92f3577b34da6a3ce929d0e0e4736` — `operation_Id`
- C) `01` — `itemCount`
- D) `00` — `operation_ParentId`

<details><summary>Cevap</summary>

**B.** Biçim: `version-traceid-parentspanid-flags`. İkinci parça (32 hex) trace-id'dir ve `operation_Id`'ye karşılık gelir; üçüncü parça üst span'dir (`operation_ParentId`); `01` flag'i örneklendi anlamına gelir.
</details>

#### Soru 4: Kod tamamlama (C#)
ASP.NET Core API'yi Azure Monitor'e bağlayan çağrıyı tamamla:

```csharp
builder.Services.AddOpenTelemetry()
    .______(o => o.ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);
```

- A) `AddApplicationInsightsTelemetry`
- B) `UseAzureMonitor`
- C) `AddAzureMonitorLogging`
- D) `WithMetrics`

<details><summary>Cevap</summary>

**B.** Azure Monitor OpenTelemetry Distro (`Azure.Monitor.OpenTelemetry.AspNetCore`) `OpenTelemetryBuilder` üzerinde `UseAzureMonitor` sunar. `AddApplicationInsightsTelemetry` klasik Application Insights SDK'sına aittir; `WithMetrics` yalnızca metrik boru hattını yapılandırır.
</details>

#### Soru 5: Kod tamamlama (C#)
Kendi iş adımını trace'te görmek istiyorsun. Boşlukları doldur:

```csharp
static readonly ActivitySource Source = new("Orders.Api");

builder.Services.AddOpenTelemetry()
    .UseAzureMonitor()
    .WithTracing(t => t.______("Orders.Api"));

using var activity = Source.StartActivity("orders.create");
activity?.SetTag("order.id", id);
```

- A) `AddProcessor`
- B) `AddSource`
- C) `AddInstrumentation`
- D) `SetSampler`

<details><summary>Cevap</summary>

**B.** `ActivitySource`'un span'leri yalnızca `AddSource` ile **adı kayıtlıysa** dinlenir ve export edilir; kayıtsız kaynağın `StartActivity` çağrısı `null` döner. Diğer seçenekler kaynak kaydı değildir.
</details>

#### Soru 6: Kod tamamlama (Python)

```python
from azure.monitor.opentelemetry import ______

______(connection_string=os.environ["APPLICATIONINSIGHTS_CONNECTION_STRING"])
```

- A) `enable_telemetry`
- B) `configure_azure_monitor`
- C) `use_azure_monitor`
- D) `start_monitoring`

<details><summary>Cevap</summary>

**B.** Python distro'nun giriş noktası `configure_azure_monitor()`'dır; otomatik enstrümantasyon için framework'lerden **önce** çağrılmalıdır.
</details>

#### Soru 7: KQL okuma
Aşağıdaki sorgu ne döndürür?

```kusto
requests
| where timestamp > ago(1h)
| summarize p95 = percentile(duration, 95) by name
| top 3 by p95 desc
```

- A) Son 1 saatte en çok çağrılan 3 endpoint
- B) Son 1 saatte p95 süresi **en yüksek** 3 endpoint (ad ve p95)
- C) Son 1 saatteki rastgele 3 istek
- D) p95'i en düşük 3 endpoint

<details><summary>Cevap</summary>

**B.** `summarize` ada göre p95 hesaplar; `top 3 by p95 desc` en yüksek üçü sıralı verir. Çağrı sayısı hesaplanmamıştır (A). Rastgele satır `take`'tir (C). `desc` en yüksekleri getirir (D).
</details>

#### Soru 8: KQL tamamlama
Her 5 dakikalık dilim için istek sayısını almak istiyorsun:

```kusto
requests
| where timestamp > ago(2h)
| summarize requestCount = count() by ______(timestamp, 5m)
| render timechart
```

- A) `round`
- B) `bin`
- C) `bucket_by`
- D) `startofday`

<details><summary>Cevap</summary>

**B.** `bin(timestamp, 5m)` değeri 5 dakikalık kovaya yuvarlar; `render timechart` zaman ekseni ister. `startofday` yalnızca günlük kovadır; `bucket_by` geçerli bir fonksiyon değildir; `round` ise `datetime` değerini 5 dakikalık kovaya yuvarlamak için kullanılmaz.
</details>

#### Soru 9: KQL tamamlama
5 dakikalık hata **oranını** (%) hesaplamak için eksik satırı seç:

```kusto
requests
| where timestamp > ago(1h)
| summarize total = count(), failed = countif(success == false) by bin(timestamp, 5m)
| ______ errorRatePct = round(100.0 * failed / total, 2)
| project timestamp, errorRatePct
```

- A) `project`
- B) `extend`
- C) `where`
- D) `distinct`

<details><summary>Cevap</summary>

**B.** `extend` mevcut sütunlara (`total`, `failed`) **hesaplanmış** bir sütun ekler; ardından `project` ile yalnızca gereken sütunlar seçilir. Burada `project` kullanmak `total`/`failed` sütunlarını düşürürdü (ve sonraki `project` anlamsızlaşırdı); `where` satır süzer, `distinct` tekilleştirir.
</details>

#### Soru 10: KQL okuma — sonuç
`requests` tablosunda son 30 dakikada yalnızca saat 10:03 ve 10:21 zamanlarında birer istek var. Sorgu:

```kusto
requests
| where timestamp between (datetime(2026-10-01 10:00) .. datetime(2026-10-01 10:30))
| summarize n = count() by bin(timestamp, 10m)
| order by timestamp asc
```

Kaç satır döner?

- A) 1
- B) 2
- C) 3
- D) 4

<details><summary>Cevap</summary>

**B.** `summarize` yalnızca **verisi olan** kovalar için satır üretir: 10:00 kovası (n=1) ve 10:20 kovası (n=1). Boş 10:10 kovası satır olmaz (boşlukları doldurmak için `make-series`). Aralığın sonu dahil olsa da 10:30'da veri yoktur.
</details>

#### Soru 11: Tek seçim
Application Insights'ta sampling %20 açıkken `requests | summarize count()` panodaki gerçek istek sayısından çok daha **düşük** çıkıyor. Doğru yaklaşım hangisidir?

- A) `count()` yerine `sum(itemCount)` kullanmak
- B) `summarize` yerine `distinct` kullanmak
- C) `take 10000` eklemek
- D) Workspace retention süresini artırmak

<details><summary>Cevap</summary>

**A.** Örneklenmiş kayıtlar `itemCount` ile kaç gerçek kaydı temsil ettiğini taşır; `sum(itemCount)` tahmini gerçek sayıyı verir. Diğer seçenekler sayıyı düzeltmez.
</details>

#### Soru 12: Çoklu seçim (iki doğru cevap)
Sampling hakkında **hangi iki** ifade doğrudur?

- A) Karar trace-id'ye göre verildiğinde bir trace ya tamamen ya da hiç kaydedilmez
- B) Sampling, özel ve standart **metrikleri** de aynı oranda düşürür
- C) Yüksek örneklemde nadir hataların kaybolma olasılığı artar
- D) Her serviste farklı oran kullanmak trace bütünlüğünü artırır

<details><summary>Cevap</summary>

**A ve C.** Trace-id tabanlı sampler tutarlı karar verir; düşük oran nadir olayları kaçırabilir. Metrikler örneklenmez (B) — bu yüzden alert'ler için daha güvenilirdir. Servislerde farklı oran trace'leri **kırar** (D).
</details>

#### Soru 13: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) `==` operatörü string karşılaştırmasında büyük/küçük harfe duyarlıdır. | ☐ | ☐ |
| b) `has` operatörü `contains`'e göre genellikle daha hızlıdır (tam terim eşleşmesi). | ☐ | ☐ |
| c) `take 5` sonucu en büyük değere göre sıralanmış 5 satırdır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** `=~` büyük/küçük harf duyarsızdır. `has` terim indeksini kullanır; `contains` alt dize arar. `take` herhangi N satır döner (sırasız); sıralı en büyükler için `top N by`.
</details>

#### Soru 14: Tek seçim
Bir uçtan uca isteğin API, worker ve veritabanı adımlarını **tek listede**, zaman sırasıyla görmek istiyorsun. Elinde `operation_Id` var. Hangi sorgu uygundur?

- A) `requests | where operation_Id == "…" | order by timestamp asc`
- B) `union requests, dependencies, exceptions, traces | where operation_Id == "…" | order by timestamp asc`
- C) `dependencies | summarize count() by operation_Id`
- D) `traces | take 100`

<details><summary>Cevap</summary>

**B.** Bir trace; istekleri (`requests`), giden çağrıları (`dependencies`), istisnaları ve logları birlikte içerir. `union` ile hepsini `operation_Id` ile süzmek tüm yolu verir. A yalnızca isteklerdir; C ve D belirli bir akış göstermez.
</details>

#### Soru 15: Sıralama
Uyarı tetiklendi: "5 dakikalık hata oranı > %5". Sorun giderme adımlarını doğru sıraya koy:

1. Başarısız bir isteğin `operation_Id`'si ile uçtan uca trace'i incele
2. Hata oranını zaman ve `resultCode` kırılımıyla doğrula
3. En yavaş/başarısız **bağımlılığı** (`target`) bul
4. `exceptions` ve ilgili log kayıtlarında kök nedeni oku
5. Düzeltmeyi uygula ve hata oranının normale döndüğünü doğrula

<details><summary>Cevap</summary>

**2 → 3 → 1 → 4 → 5.** Önce belirtiyi doğrula ve daralt (hangi endpoint/kod), sonra hangi bağımlılığın sorun olduğunu bul, örnek bir trace'i incele, istisna/loglardan kök nedeni oku, düzelt ve doğrula.
</details>

#### Soru 16: CLI tamamlama
Hata oranı sorgusunu çalıştıran bir **log search alert** kuralı oluşturmak istiyorsun. Eksik parçayı seç:

```bash
az monitor ______ create -g rg-demo -n alert-error-rate --scopes $WS_ID \
  --condition "count 'Q1' > 0" \
  --condition-query Q1="AppRequests | summarize f = countif(Success == false) | where f > 10" \
  --window-size 5m --evaluation-frequency 5m --severity 2 --action-groups $AG_ID
```

- A) `metrics alert`
- B) `scheduled-query`
- C) `activity-log alert`
- D) `diagnostic-settings`

<details><summary>Cevap</summary>

**B.** Log search alert'leri **scheduled query rule** olarak oluşturulur (`az monitor scheduled-query`; CLI uzantısı gerekir). `metrics alert` metrik eşiği, `activity-log alert` control plane olayları içindir; `diagnostic-settings` log yönlendirmedir.
</details>

#### Soru 17: Çoklu seçim (iki doğru cevap)
Bir **action group**'un tetikleyebileceği eylemlerden **hangi ikisi** doğrudur?

- A) Azure Function çağırmak
- B) E-posta / SMS göndermek
- C) KQL sorgusunu otomatik düzeltmek
- D) Log Analytics workspace'ini silmek (yerleşik eylem)

<details><summary>Cevap</summary>

**A ve B.** Action group; e-posta, SMS, push, sesli arama, webhook, Azure Function, Logic App, ITSM ve Automation runbook gibi eylemleri içerir. C ve D yerleşik eylem değildir.
</details>

#### Soru 18: Vaka çalışması

**Durum.** *Northwind Logistics*, Container Apps'te `shipments-api`, `label-worker` (Service Bus kuyruğunu işler) ve PostgreSQL kullanıyor. Her ikisi de Azure Monitor OpenTelemetry Distro ile Application Insights'a (workspace-based) bağlı. Son hafta "etiket oluşturma bazen 30 sn sürüyor" şikâyetleri var. Maliyeti düşürmek için API'de `SamplingRatio = 0.1`, worker'da `1.0` ayarlanmış. Application map'te API ile worker arasında bağlantı çizgisi **bazı trace'lerde kopuk**.

**Soru A.** Trace'lerin kopuk görünmesinin olası nedeni ve çözümü nedir?
**Soru B.** "Etiket oluşturma yavaş" şikâyetini araştırmak için hangi sorgu mantığını kurarsın? (yazılı sorgu iskeleti)
**Soru C.** %5'in üzerinde hata olduğunda ekibe nasıl haber verirsin; neden metrik yerine log alert'i seçebilirsin?

<details><summary>Cevap</summary>

**A.** Servislerde **farklı sampling oranları**: API trace'in %90'ını atarken worker %100 kaydeder; atılan trace'lerin worker span'leri tek başına (köksüz) kalır ve map/trace görünümünde kopar. Çözüm: **tüm servislerde aynı, trace-id tabanlı (parent-based) sampling**; worker ebeveynin kararına uysun, oranı iki serviste eşitle. Ayrıca messaging SDK tracing'in açık olduğunu ve context'in (`Diagnostic-Id`/`traceparent`) mesajla taşındığını doğrula.

**B.** Önce yavaş işlemi bul, sonra bağımlılığı daralt, sonra trace'i incele:

```kusto
requests
| where timestamp > ago(7d) and cloud_RoleName == "label-worker"
| summarize p95 = percentile(duration, 95), calls = sum(itemCount) by bin(timestamp, 1h)

dependencies
| where timestamp > ago(7d) and cloud_RoleName == "label-worker"
| summarize p95 = percentile(duration, 95), calls = sum(itemCount) by type, target
| top 5 by p95 desc
```

Sonra en yavaş çağrıdan bir `operation_Id` seçip `union requests, dependencies, exceptions, traces | where operation_Id == "…" | order by timestamp asc` ile uçtan uca incele. `sum(itemCount)` örneklemeyi hesaba katar.

**C.** Bir **log search alert** (scheduled query rule): `summarize total = sum(itemCount), failed = sumif(itemCount, success == false)` ile oran hesaplayıp `total > N and oran > 5` koşuluyla, 5 dakikalık pencere ve değerlendirme aralığıyla; bir **action group** (e-posta + webhook/Teams/Function) bağla. Oran, birden çok sinyalden hesaplanan **KQL koşulu** gerektirdiği için log alert uygundur; yalnızca basit bir eşik (ör. kuyruk uzunluğu) için **metric alert** daha hızlı ve ucuzdur. Log alert'te ingestion gecikmesi ve değerlendirme ücreti vardır.
</details>

---

**Puanlama:** 16-18 → hazırsın · 13-15 → README 3.3 (KQL operatörleri) ve 3.5 (sampling) bölümlerini tekrar et · < 13 → konuyu baştan çalış ve lab'ı (3.6) uygula.


---

# BÖLÜM 5: AI-200 Deneme Sınavı ve Sınav Stratejisi

---

## 5.0 Kapsamlı AI-200 Deneme Sınavı (50 Soru, 2 Vaka Çalışması) ve Sınav Stratejisi

> **Kapsam:** Tüm Sınav Alanları (C2.14) · **Hedef:** Gerçek Associate sınav simülasyonu (≥ 42/50 - %84)

### 5.1 Sınav Stratejisi ve Hazırlık Rehberi

### 1. Ne?

AI-200 bir **associate** sınavıdır: AZ-900 gibi kavramları eşleştirmekle yetinmez; senaryodan **doğru servisi, doğru yapılandırmayı ve doğru kod/CLI/KQL parçasını** seçmeni ister. Bu konu üç parçadan oluşur:

| Parça | Dosya | Amaç |
|---|---|---|
| **Deneme sınavı** | aşağıdaki quiz bölümüne | Resmî ağırlıklara göre dağıtılmış **50 özgün soru**; her soru bir alana ve konuya (`[B · C2.5]`) bağlı. 2 vaka çalışması (13–16, 29–32), C#/Python/CLI/KQL/SQL/YAML/JSON tamamlama, sıralama, eşleştirme, evet/hayır |
| **Puanlama ve tekrar planı** | `quiz.md` sonu | Alan bazlı puan tablosu; her yanlışı C2.x'e bağlayan tekrar tablosu |
| **Sınav stratejisi** | Bu README | Soru türleri, vaka çalışması ve kod tamamlama teknikleri, anahtar kelime → servis tablosu, zaman yönetimi, sınav günü |

**Soru dağılımı** (resmî ağırlıklara göre):

| Alan | Konular | Ağırlık | Bu denemede |
|---|---|---|---|
| **A** — Develop containerized solutions on Azure | C2.1 ACR · C2.2 Container Apps · C2.3 AKS | %20-25 | 11 soru (%22) |
| **B** — Develop AI solutions by using Azure data management services | C2.4 Cosmos NoSQL · C2.5 Cosmos vector/change feed · C2.6 PostgreSQL/pgvector · C2.7 Managed Redis | **%30-35** | 17 soru (%34) |
| **C** — Connect to and consume Azure services | C2.8 Functions · C2.9 Service Bus · C2.10 Event Grid | %20-25 | 11 soru (%22) |
| **D** — Secure, monitor, and troubleshoot Azure solutions | C2.11 Identity/Key Vault · C2.12 App Configuration · C2.13 OpenTelemetry/KQL | %20-25 | 11 soru (%22) |

**Sınavın genel çerçevesi** (ayrıntılar değişebilir ve kaynaklarda tutarsızdır — resmî sayfadan doğrula):

| | |
|---|---|
| Geçme puanı | **700 / 1000** (ölçeklenmiş puan; "doğru yüzdesi" ile birebir aynı değildir) |
| Soru sayısı ve süre | Çevrimiçi kaynaklarda kabaca 40-60 soru ve 100-120 dakika aralığı geçiyor; **kesin değerleri sınav sayfasından ve sınav başındaki ekrandan al**. Bu deneme 50 soru / 100 dakika varsayar |
| Soru türleri | Tek seçim, çoklu seçim, evet/hayır tabloları, sürükle-bırak (sıralama/eşleştirme), cümle/kod tamamlama (açılır liste), senaryo ve **vaka çalışması** |
| Nerede | **Pearson VUE** test merkezi veya **çevrimiçi gözetimli** (online proctored) |
| Geçerlilik | Associate sertifikaları süreli olup (genellikle 1 yıl) Microsoft Learn'de **ücretsiz yenileme değerlendirmesi** ile uzatılır — güncel kuralı doğrula |
| Hazırlık materyali | Yeni bir sınav olduğu için **resmî ücretsiz practice assessment henüz yayımlanmamış veya sınırlı olabilir**; üçüncü taraf "practice test" satıcıları çoğunlukla doğrulanmamış içerik sunar. Bu deneme o boşluğu doldurmak içindir, resmî değerlendirmenin yerine geçmez |

---

### 2. Neden?

- **Bilmek ile sınavda göstermek farklıdır.** Konuları tek tek çalışırken her soru bağlamıyla gelir; gerçek sınavda bir Cosmos partition key sorusu hemen ardından bir KEDA kuralı sorusuyla yan yana gelir. Benzer şıklar (Secrets User ↔ Contributor, `AcrPull` ↔ `AcrPush`, HNSW ↔ IVFFlat, TTL ↔ eviction, Event Grid ↔ Service Bus) karışır; karışık bir deneme bunu ortaya çıkarır.
- **Format riski:** Associate sınavlarında **kod/CLI/KQL tamamlama** ve **vaka çalışması** vardır. Bunlar bilgiden çok *okuma ve çıkarım disiplini* ister; ilk kez gerçek sınavda görmek zaman kaybettirir.
- **Zayıf alanı ölçmek:** En ağır alan **B (%30-35)** dört konuya (C2.4–C2.7) yayılır. Alan bazlı puan, "hangi C2.x'i tekrar edeyim?" sorusunu veriyle cevaplar.
- **Yeni sınav, az kaynak:** Resmî practice assessment eksikse ve forumlardaki sorular güvenilmezse, kendi zayıf noktanı bulmanın en iyi yolu **gerekçeli** bir deneme sınavıdır.

---

### 3. Nasıl?

#### 3.1 Deneme sınavını kullanma (önerilen akış)

1. **Hazırlık:** C2.1–C2.13 konularının quiz'lerinde ≥ %80 al; en azından her konunun lab'ını (veya emülatör/yerel karşılığını) bir kez yaptığından emin ol. Deneme bir **ölçüm aracıdır**, ders değil.
2. **Zamanlı ve kapalı kitap çöz:** 50 soru için **100 dakika** ayır. Dokümanı, README'leri, kod editörünü ve `<details>` bloklarını açma. Cevaplarını ayrı bir kâğıda/dosyaya yaz (soru no → şık).
3. **Puanla:** Her sorunun cevabı kendi `<details>` bloğundadır. `quiz.md` sonundaki **alan bazlı puan tablosunu** doldur (çok parçalı sorularda tüm parçalar doğruysa 1 puan).
4. **Yanlışları sınıflandır:** Her yanlış için (a) hangi C2.x, (b) **bilgi eksiği mi, dikkat hatası mı?** (örn. `__` yerine `:` yazdım — sözdizimi bilgisi; `count()` ↔ `sum(itemCount)` — kavram; vakada bir gereksinimi kaçırdım — okuma hatası).
5. **Tekrar et:** Bir konuda 2+ bilgi eksiği varsa o konunun README + lab + quiz döngüsünü baştan yap.
6. **Bir hafta sonra** soruları **farklı sırayla** yeniden çöz; ezber değil anlama ölçülsün diye her cevabın gerekçesini kendi cümlelerinle yaz.
7. Resmî practice assessment (varsa) veya yeniden sertifikasyon değerlendirmesi formatını görmek için kapalı kitap çöz; istikrarlı **%80+** hazır olduğunun işaretidir.

#### 3.2 Alan bazlı puanlama

| Toplam | Yorum | Eylem |
|---|---|---|
| ≥ 42 / 50 (%84+) ve her alan ≥ %70 | Hazır | Sınav tarihi al; son hafta anahtar kelime tablosunu (3.5) ve "Karışan çiftler" tablosunu (5) tekrar et |
| 35–41 | Neredeyse | Zayıf alanın konularını README + lab ile tekrar et, bir hafta sonra yeniden çöz |
| < 35 | Hazır değil | Konu döngüsüne dön (README → lab → konu quiz'i); deneme bir sonraki tur için sakla |

- **Toplam yüzde yetmez, alan dağılımına bak:** Alan B'de %50 alıp diğer alanlarda %95 alan biri %70 civarına düşer ve ağırlığı en yüksek alanda zayıf kalır. Gerçek sınav sonuç raporu da **alan bazında** performans gösterir; bu denemenin tablosu onun provasıdır.
- **Alan içinde konu dağılımına bak:** Alan B'de 17 sorudan C2.4 (5), C2.5 (4), C2.6 (4), C2.7 (4). Tek bir konuda 3 yanlış varsa sorun alan değil o konudur.
- **Yüzdeyi 700/1000 ile birebir eşleme:** ölçeklenmiş puan soru ağırlıklarına ve sınav formuna bağlıdır; buradaki yüzde yalnızca göstergedir.

#### 3.3 Vaka çalışması ve kod / CLI / KQL tamamlama soruları

**Vaka çalışması (case study).** Bir senaryo (mevcut mimari, iş gereksinimleri, kısıtlar) ve ona bağlı birkaç soru gelir. Genel kural: vaka sorularının tamamı aynı senaryoyu paylaşır; bazı sınav arayüzlerinde bir vaka bölümünü bitirip ilerledikten sonra **geri dönemezsin** — sınav ekranındaki uyarıyı oku ve her bölümde kalan süreyi gözle.

1. **Önce gereksinimleri ve kısıtları listele.** Senaryoyu okurken numaralı bir liste çıkar: *"R1 değişmez imaj, R2 parolasız, R3 %10 canary, R4 sıfıra ölçek, R5 rotation"*. Her soru bu listedeki bir veya iki maddeyi sorar.
2. **Anahtar kelimeleri işaretle:** "en az yönetim yükü", "en düşük maliyet", "en az yetki", "secret'sız", "yeniden dağıtım olmadan", "tutarlı", "yalnızca VNet'ten". Bunlar şıkları eler.
3. **Soruyu okumadan şıklara bakma:** Önce sorunun hangi gereksinimi hedeflediğine karar ver, sonra şıkları bu gereksinimle test et.
4. **Vaka dışı bilgiyi uydurma:** Senaryoda yazmayan bir kısıt (örn. "bütçe sınırsız") varsayma; ama senaryodaki sayıları kullan (replica sayısı, `maxReplicas`, `messageCount`).
5. **Vaka süresi:** 4 soruluk bir vakaya 10-12 dakikadan fazla harcama; takılırsan işaretle ve geç (kısıtlar varsa geri dönülemeyebileceğini unutma).

**Kod tamamlama (C#/Python).** Boşluk genellikle bir **tip/metot adı**, bir **ayar anahtarı** veya bir **boolean**'dır. Teknik:

| Teknik | Örnek |
|---|---|
| Boşluğun etrafındaki **imzaya** bak: dönüş tipi, parametre sayısı, `await` | `ReadItemAsync<Order>(id, new PartitionKey(...))` → point read; `GetItemQueryIterator` `FeedIterator` döner, `await` ile tek item gelmez |
| **Hangi paket/SDK?** (Azure.Identity, Azure.Messaging.ServiceBus, Microsoft.Azure.Cosmos) | `SubQueue.DeadLetter`, `VectorDistance`, `UseAzureMonitor()` |
| **Sürüm/model farkı:** isolated worker ↔ in-process, Python v1 ↔ v2 | Python v2: decorator; .NET isolated: attribute; `function.json` Python v2'de yok |
| **Idempotent / güvenli varsayılan** | `refreshAll: true`, `AddSource("…")`, `DIALECT 2` |
| Python ↔ C# adı **birebir aynı olmaz** | C# `SetSecretAsync` ↔ Python `set_secret` |

**CLI tamamlama.** `az` komut grupları ve parametre adları: ezber yerine **desen**:

- `az acr build/run/task/import`, `az containerapp revision/ingress traffic/secret/replica`, `az aks create --attach-acr/get-credentials`, `az cosmosdb sql role assignment`, `az servicebus topic subscription rule`, `az eventgrid event-subscription create --advanced-filter/--max-delivery-attempts/--event-ttl/--deadletter-endpoint`, `az keyvault create --enable-rbac-authorization --enable-purge-protection`, `az appconfig kv/feature`, `az monitor scheduled-query`.
- Rol soruları: `--assignee-object-id` + `--assignee-principal-type ServicePrincipal` + `--role` + `--scope`. **Rol adını** kapsamla birlikte düşün: `AcrPull` registry'de, *Key Vault Secrets User* vault'ta, *Azure Service Bus Data Receiver* namespace'te, Cosmos için **`az cosmosdb sql role assignment`** (Azure RBAC değil).

**KQL tamamlama/okuma:**

| Operatör ikilisi | Ayırt eden |
|---|---|
| `extend` ↔ `project` | Yeni hesaplanan sütun ↔ sütun seçimi |
| `top N by x` ↔ `take N` | Sıralı ilk N ↔ rastgele N |
| `summarize … by bin(timestamp, 5m)` | Zaman kovası; **boş kova satır üretmez** |
| `count()` ↔ `sum(itemCount)` | Örneklenmiş veride eksik ↔ gerçek sayı |
| `has` ↔ `contains` | Tam terim (indeksli, hızlı) ↔ alt dize |
| `==` ↔ `=~` | Büyük/küçük harfe duyarlı ↔ duyarsız |
| `join` varsayılanı | `innerunique` tuzağı → `kind=inner` yaz |

Klasik App Insights adları (`requests`, `timestamp`, `duration`, `operation_Id`) ile workspace adları (`AppRequests`, `TimeGenerated`, `DurationMs`, `OperationId`) arasındaki eşlemeyi bil; sınav ikisini de kullanabilir (→ [C2.13](#43-opentelemetry-ile-izleme-ve-kql-ile-sorun-giderme)).

#### 3.4 Soru çözme teknikleri

| Teknik | Nasıl? | Örnek |
|---|---|---|
| **Önce soru kökünü oku** | Son cümle asıl soruyu taşır: "en az yönetim yükü", "en düşük maliyet", "en az yetki" | "…**en az yönetim yükü** ile container'ları çalıştır" → Container Apps (AKS değil) |
| **NOT / EXCEPT** | Soruyu olumluya çevir, her şıkkı "doğru mu?" diye işaretle | "Hangisi revision oluşturmaz?" → secret/ingress/traffic |
| **Eleme** | Kategori dışı (başka servis), fazla yetkili, mutlak ifadeli, "yalnızca"/"her zaman" şıkları at | `Contributor` secret okutmaz; `Owner` fazla yetkilidir |
| **Evet/hayır tablosu** | Her satır **bağımsızdır** | "Hepsi evet olamaz" kuralı yok |
| **"En iyi" cevap** | Birden çok şık çalışabilir; senaryonun kısıtını en iyi karşılayanı seç | Storage Queue de kuyruk olur, ama DLQ/session isteniyorsa Service Bus |
| **Çoklu seçim** | Kaç cevap istendiğini oku ("iki doğru cevap seç"); kısmi puan olabilir ama varsayma | — |
| **Sıralama** | Önce ilk ve son adımı yerleştir, ortayı doldur | Rotation: olay → Event Grid → yeni credential → yeni sürüm → uygulama alır |
| **Sayısal hesap** | Önce tavanı (`maxReplicas`) kontrol et | `ceil(300/25) = 12` → `maxReplicas 8` ile sınırlı |
| **Bilmiyorsan işaretle** | 2 dakikayı aşan soruyu işaretle; boş bırakma (yanlış cevap için ek ceza olmadığı varsayımını sınav başında doğrula) | — |

#### 3.5 Anahtar kelime → servis/çözüm tablosu

| Senaryodaki ifade | Büyük olasılıkla | Konu |
|---|---|---|
| "yerel Docker olmadan image derle" | `az acr build` | C2.1 |
| "base image güncellenince otomatik yeniden build" | ACR Task + base image update trigger | C2.1 |
| "geo-replication / private endpoint" | ACR **Premium** | C2.1 |
| "test edilen image'ı birebir çalıştır" | **Digest** ile deploy | C2.1 |
| "image çekmek için en az yetki" | Managed identity + **`AcrPull`** | C2.1 |
| "önce %10 trafik, sonra geri al" | **Multiple** revision mode + traffic weights | C2.2 |
| "kuyruk uzunluğuna göre ölçek, boştayken sıfır" | KEDA `azure-servicebus` kuralı, `min-replicas 0` | C2.2 |
| "secret değişti, yeni revision?" | Hayır (application-scope) | C2.2 |
| "`ImagePullBackOff` + unauthorized / manifest unknown" | `AcrPull` eksik / yanlış tag | C2.3 |
| "`OOMKilled`, Exit Code 137" | Bellek limiti | C2.3 |
| "`READY 0/1`" / "endpoints boş" | Readiness probe / Service selector–label | C2.3 |
| "Kubernetes API gerekmiyor, scale-to-zero" | **Container Apps** | C2.2/C2.3 |
| "id + partition key ile oku" | `ReadItemAsync` (point read) | C2.4 |
| "düşük cardinality'li partition key, 429" | Hot partition → `/customerId` vb. | C2.4 |
| "kullanıcı kendi yazdığını görsün, en ucuz" | **Session** consistency | C2.4 |
| "iki alanla ORDER BY hata veriyor" | **Composite index** | C2.4 |
| "yazma RU'su yüksek, sorgular 3 alanda" | Dar indexing policy (allow-list) | C2.4 |
| "milyonlarca vector, düşük gecikme" | **diskANN** | C2.5 |
| "dimension mismatch" | Policy `dimensions` ≠ model çıktısı | C2.5 |
| "worker yeniden başladı, kaldığı yerden" | **Lease container** | C2.5 |
| "silinen item change feed'de yok" | Soft delete / all versions and deletes | C2.5 |
| "boş tabloda kurulabilen, en iyi recall" | **HNSW** (IVFFlat veri sonrası) | C2.6 |
| "index var ama Seq Scan" | Operatör ↔ operator class uyuşmazlığı | C2.6 |
| "tenant filtresiyle eksik sonuç" | `hnsw.iterative_scan` / partitioning | C2.6 |
| "too many connections" | Singleton pool + **PgBouncer (6432)** | C2.6 |
| "vector araması için Redis" | **RediSearch** (oluştururken), Enterprise policy, NoEviction | C2.7 |
| "bellek dolunca hangi anahtar silinsin" ↔ "süre dolunca sil" | Eviction ↔ **TTL** | C2.7 |
| "benzer sorulara LLM'i tekrar çağırma" | **Semantic cache** | C2.7 |
| "VNet + scale-to-zero" | Functions **Flex Consumption** | C2.8 |
| "function'ı başlatan olay" | **Trigger** (function başına tam bir) | C2.8 |
| "secret'sız Service Bus/Cosmos bağlantısı" | `<önek>__fullyQualifiedNamespace` / `__accountEndpoint` + doğru rol | C2.8 |
| "her mesaj tek tüketici" ↔ "aynı mesaj birçok tüketici" | **Queue** ↔ **Topic + subscription** | C2.9 |
| "sırayla işle" / "çift gönderimi yut" | **Session** / **Duplicate detection** | C2.9 |
| "zehirli mesaj sistemi tıkamasın" | **DLQ** + `MaxDeliveryCount` | C2.9 |
| "subscription filtresi çalışmıyor" | `$Default` kuralı silinmedi | C2.9 |
| "Azure servisi olayına tepki" | Event Grid **system topic** | C2.10 |
| "payload alanına göre yönlendir" | **Advanced filter** (`data.*`) | C2.10 |
| "teslim edilemeyen olayları sakla" | **Dead-letter** (varsayılan kapalı) | C2.10 |
| "sıralama, session, işleme garantisi" | Service Bus (Event Grid değil) | C2.10 |
| "secret'sız bağlan" | **Managed identity** | C2.11 |
| "yalnızca secret oku, en az yetki" | **Key Vault Secrets User** | C2.11 |
| "Contributor var ama secret okunamıyor" | Control plane ≠ data plane | C2.11 |
| "süre dolmadan rotation tetikle" | Event Grid `SecretNearExpiry` → Function | C2.11 |
| "kimse kalıcı silemesin" | **Purge protection** | C2.11 |
| "ortam başına farklı değer" | **Label** (yetki ayrımı için ayrı store) | C2.12 |
| "restart etmeden yeniden yükle" | **Sentinel key** + `refreshAll: true` | C2.12 |
| "gizli olmayan ayar ↔ secret" | App Configuration ↔ Key Vault (+ referans) | C2.12 |
| "kullanıcıların %20'sine, tutarlı" | Feature flag **Targeting** (Percentage rastgele) | C2.12 |
| "servisler arası istek korelasyonu" | W3C `traceparent` / context propagation | C2.13 |
| "Azure Monitor'e az kodla telemetri" | OpenTelemetry **Distro** (`UseAzureMonitor`) | C2.13 |
| "örneklemeli veride gerçek sayı" | `sum(itemCount)` | C2.13 |
| "hata oranı eşiği aşınca e-posta" | Log alert (veya metric alert) + **action group** | C2.13 |

#### 3.6 Zaman yönetimi

- Sınav başlamadan gösterilen **soru sayısı ve süreyi** not et; soru başına düşen süreyi hesapla (50 soru / 100 dk → ≈ 2 dk).
- Dağılım önerisi: kısa tek seçim/evet-hayır **~1 dk**; kod/CLI/KQL tamamlama **~2 dk**; sıralama/eşleştirme **~2 dk**; vaka sorusu **~2,5 dk** + vakayı okumak için **~3 dk**.
- İlk geçişte bildiklerini hızla cevapla, emin olmadıklarını **işaretle**; ikinci geçişe en az %15 süre bırak.
- Uzun senaryolarda önce son cümleyi (asıl soruyu), sonra senaryoyu oku.
- Bazı soru grupları (vaka, geri dönüşsüz bölümler) cevaplandıktan sonra **geri dönülemeyebilir**; kullanılabilir olduğunda "review" ekranını son dakikalara değil, ortaya yakın kullan.
- **Microsoft Learn erişimi:** Microsoft bazı rol tabanlı sınavlarda sınav içinde Microsoft Learn'e (arama dahil) erişim sunmaktadır. AI-200 için bunun geçerli olup olmadığını ve kurallarını **güncel sınav sayfasından kontrol et**; geçerli olsa bile her soru için dokümana bakmak süreyi yetmez kılar — erişim bir **doğrulama aracıdır** (parametre adı, CLI bayrağı gibi), öğrenme yerine geçmez.

#### 3.7 Sınav günü

| | Test merkezi (Pearson VUE) | Çevrimiçi gözetimli (online proctored) |
|---|---|---|
| Ortam | Hazır bilgisayar, gözetmen yerinde | Kendi bilgisayarın; webcam ve mikrofon zorunlu |
| Hazırlık | Kimlik belgesi, randevudan önce varış | **Sistem testi** (önceden), boş masa/oda, check-in fotoğrafları |
| Risk | Ulaşım | İnternet kopması, ortam kuralları (birinin odaya girmesi, ekrandan uzaklaşmak) |
| Öneri | İlk sınavında stres azaltır | Esnek saat; check-in için 15-30 dk erken başla |

- Sınav arayüzünü önceden görmek için Microsoft'un **exam sandbox** (sınav deneyimi demosu) sayfasını dene; soru türlerini (sürükle-bırak, açılır liste, vaka) orada tanı.
- Kimlikteki ad, sınav profilindeki adla **aynı** olmalıdır.
- Sonuç genellikle sınav bitiminde görünür; başarısızlıkta **alan bazlı** performans raporu verilir — bu repodaki C2.x tekrar planına doğrudan eşlenir. Yeniden girme bekleme süreleri için resmî **retake policy**'ye bak.

---

### 4. Gerçek dünya senaryosu

Bir backend geliştirici AI-200'e yedi haftalık plana göre hazırlanıyor (→ C2 modül sırası):

- **Hafta 1-2:** C2.1–C2.3 (containerized) — her konunun lab'ı kendi aboneliğinde, bütçe alarmı kurulu; her lab `az group delete` ile kapatılıyor. AKS lab'ı yalnızca kısa süre açık tutuluyor.
- **Hafta 3-4:** C2.4–C2.7 (veri) — Cosmos free tier/serverless, yerelde `pgvector` imajı, Redis Stack; **en büyük alan** olduğu için iki hafta ayrıldı.
- **Hafta 5:** C2.8–C2.10 (Functions, Service Bus, Event Grid) — emülatörlerle yerel, Event Grid için kısa Azure lab'ı.
- **Hafta 6:** C2.11–C2.13 (Key Vault, App Configuration, OTel/KQL).
- **Hafta 7, gün 1:** Bu deneme sınavı zamanlı: **36/50**. Alan tablosu: A 9/11, **B 9/17**, C 9/11, D 9/11. B'deki 8 yanlışın 4'ü C2.6 (operator class, iterative scan, PgBouncer), 3'ü C2.7 (modülün oluştururken seçilmesi, TTL ↔ eviction), 1'i C2.4.
- **Gün 2-3:** C2.6 ve C2.7 README + lab'ları tekrar; C2.7'de Redis Stack ile `FT.CREATE`/`FT.SEARCH` yeniden yazıldı; anahtar kelime tablosu (3.5) el yazısıyla çıkarıldı.
- **Gün 4:** Denemenin B alanı soruları farklı sırayla tekrar: 15/17. Resmî practice assessment (yayımlandıysa) kapalı kitap çözüldü.
- **Gün 6:** Çevrimiçi gözetimli sınav; önceki gün sistem testi yapılmış, oda hazırlanmış.

Ders: toplam puan (%72) tek başına "hazırsın" demiyordu; **alan B'deki %53** tekrarın yönünü gösterdi.

---

### 5. Sınav perspektifi

**Microsoft'un sevdiği kalıplar ve tuzaklar** (tüm modülden en sık karışanlar):

| Karışan çift | Ayırt eden soru |
|---|---|
| `AcrPull` ↔ `AcrPush` ↔ `Contributor` | Yalnızca çekme mi, push mu, registry yönetimi mi? (en az yetki) |
| Tag ↔ digest | Değişebilir işaretçi mi, değişmez içerik kimliği mi? |
| Single ↔ multiple revision mode | Trafik bölme/canary var mı? |
| Revision-scope ↔ application-scope değişiklik | Image/env/scale kuralı (yeni revision) ↔ secret/ingress/traffic (yeni revision yok) |
| Container Apps ↔ AKS ↔ App Service ↔ Functions | Kubernetes API gerekiyor mu? Olay güdümlü, sıfıra ölçek, operasyon yükü? |
| Liveness ↔ readiness | Yeniden başlatır mı, trafikten mi çıkarır? |
| Partition key ↔ `id` | Cardinality/dağılım/sorgu uyumu; point read |
| Strong ↔ Session ↔ Eventual | "Kendi yazdığını gör" Session ister, Strong pahalıdır |
| Cosmos vector `flat` ↔ `quantizedFlat` ↔ `diskANN` | Veri ölçeği ve boyut |
| HNSW ↔ IVFFlat | Boş tabloda kurulur mu? Build süresi/bellek? |
| TTL ↔ eviction | Süre mi, bellek baskısı mı? |
| Cosmos ↔ PostgreSQL ↔ Redis (vector) | Global/esnek şema ↔ ACID+JOIN ↔ geçici/ultra düşük gecikme |
| Trigger ↔ binding | Function'ı kim başlatır? |
| Flex ↔ Consumption ↔ Premium ↔ Dedicated | VNet + sıfıra ölçek ↔ soğuk başlangıç yok + slot ↔ mevcut plan |
| Queue ↔ topic | Tek tüketici ↔ her abone kopya |
| Abandon ↔ dead-letter ↔ defer | Sayaç artırır ↔ DLQ'ya taşır ↔ `SequenceNumber` ile sonra al |
| Event Grid ↔ Service Bus ↔ Event Hubs | Olay bildirimi ↔ iş komutu ↔ telemetri akışı |
| System-assigned ↔ user-assigned MI | Kaynakla ölür ↔ paylaşılır, bağımsız yaşar |
| Control plane ↔ data plane rolleri | `Contributor` ↔ *Secrets User*, Cosmos data-plane rolü |
| App Configuration ↔ Key Vault | Gizli olmayan ayar ↔ secret (+ Key Vault referansı) |
| Percentage ↔ Targeting ↔ TimeWindow | Rastgele ↔ kullanıcıya tutarlı ↔ tarih aralığı |
| Metric alert ↔ log search alert | Hızlı/ucuz eşik ↔ esnek KQL, ingestion gecikmeli |
| `count()` ↔ `sum(itemCount)` | Sampling var mı? |

**Sınavda olmayanlar / az olanlar:** fiyat rakamları, tam limit sayıları, portal ekranlarının yeri. Kavram, yapılandırma anahtarı ve doğru komut/ayar adı önemlidir. AZ-204'ün bazı konuları (App Service ayrıntıları, API Management, Event Hubs ayrıntıları, Microsoft Graph) AI-200 hedeflerinde görünmez — güncel müfredatla karşılaştır.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Braindump/"çıkmış soru" ezberlemek | Microsoft'un sınav sözleşmesine (NDA) aykırıdır, sertifika iptaline yol açabilir; sorular değişir — kavramı öğren, bu depodaki gibi **özgün** sorularla çalış |
| Yalnızca deneme çözerek çalışmak | Önce konu (README + lab), sonra konu quiz'i, en son deneme |
| Lab yapmadan kod/CLI sorularına girmek | Parametre adları ve rol atama deseni elde yazılmadan oturmaz; lab'ları emülatör/ucuz katmanla yap |
| Vakada gereksinim listesi çıkarmamak | Her vaka sorusu 1-2 gereksinime bağlıdır; liste çıkar |
| Evet/hayır satırlarını birbirine bağlamak | Her satır bağımsızdır |
| "NOT"u kaçırmak | Soru kökündeki olumsuzluğu zihnen altını çiz |
| Çoklu seçimde istenen sayıdan fazla/az seçmek | "İki doğru cevap" ise tam iki |
| Bir soruda 4-5 dakika takılmak | İşaretle, geç, sona dön |
| Bütçe alarmı kurmadan lab yapmak, lab'ı silmemek | AKS/Managed Redis saatlik faturalanır; `az group delete` ile temizle |
| Online sınavda ortam kurallarını hafife almak | Masada telefon/kâğıt, odaya giren biri veya ekrandan uzaklaşmak iptale yol açabilir |
| Müfredat tarihini kontrol etmemek | "skills measured as of" tarihini ve değişiklik kaydını sınavdan önce kontrol et |
| Eski ürün adlarına/varsayımlara güvenmek | Azure Managed Redis (Azure Cache for Redis'in halefi), Functions isolated worker, Docker Content Trust yerine Notation vb. — güncel dokümanı doğrula |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Erken sınav tarihi almak | Motivasyon ve odak | Hazır değilsen tekrar ücreti ve bekleme süresi |
| Test merkezi | Teknik risk yok, sakin ortam | Ulaşım, sınırlı saat |
| Online proctored | Esnek saat ve yer | Bağlantı/ortam riski, sıkı kurallar |
| Çok sayıda deneme sınavı | Format rahatlığı | Ezber riski: aynı soruları tanımaya başlarsın; gerekçeyi açıklayamıyorsan bilmiyorsundur |
| Lab'ları atlamak | Zaman kazancı | Kod/CLI tamamlama ve "hangisi gerçekten böyle çalışır?" sezgisi zayıf kalır |
| Tüm lab'ları gerçek Azure'da yapmak | Gerçekçi davranış (ör. RBAC yayılımı, KEDA ölçeği) | Maliyet ve temizlik yükü; emülatör/yerel karşılık (Cosmos emulator, Service Bus emulator, pgvector Docker, Redis Stack) çoğu kavram için yeter |
| Üçüncü taraf practice test'ler | Soru sayısı fazla | Doğrulanmamış/eski içerik riski; gerekçe zayıf olabilir — resmî dokümanla çapraz kontrol et |
| Microsoft Learn erişimine (varsa) güvenmek | Parametre adını doğrulama | Zaman kaybı; öğrenmeyi ikame etmez |

---

### 8. Kaynaklar

- [AI-200 study guide (skills measured + değişiklik kaydı)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Exam duration and question types](https://learn.microsoft.com/en-us/credentials/support/exam-duration-exam-experience) · [Exam sandbox](https://aka.ms/examdemo)
- [Online exams with Pearson VUE](https://learn.microsoft.com/en-us/credentials/certifications/online-exams) · [Retake policy](https://learn.microsoft.com/en-us/credentials/support/retake-policy)
- [Credentials support (sınav politikaları, gizlilik sözleşmesi, sınav bütünlüğü)](https://learn.microsoft.com/en-us/credentials/support/) · [Sertifika yenileme (renewal assessment)](https://learn.microsoft.com/en-us/credentials/certifications/renew-your-microsoft-certification)
- Bu modül: [C2.1](#11-azure-container-registry-ve-acr-tasks) · [C2.2](#12-azure-container-apps-ortam-revision-ve-keda) · [C2.3](#13-akse-manifest-ile-dağıtım-ve-container-teşhisi) · [C2.4](#21-cosmos-db-for-nosql-sdk-partitioning-indexing-consistency-ru) · [C2.5](#22-cosmos-db-ile-vector-search-ve-change-feed) · [C2.6](#23-azure-database-for-postgresql-ve-pgvector) · [C2.7](#24-azure-managed-redis-cache-ve-vector-index) · [C2.8](#31-azure-functions-serverless-api-trigger-ve-binding) · [C2.9](#32-azure-service-bus-kuyruk-topic-ve-dead-letter) · [C2.10](#33-azure-event-grid-custom-event-filtre-ve-retry) · [C2.11](#41-managed-identity-ve-azure-key-vault) · [C2.12](#42-azure-app-configuration) · [C2.13](#43-opentelemetry-ile-izleme-ve-kql-ile-sorun-giderme)
- Önceki sertifika denemesi (format karşılaştırması için): [AZ-900 Deneme Sınavı](../01-az-900-azure-fundamentals/AZ-900-Study-Guide.md)

> **Doğrulama notu:** AI-200 yeni bir sınavdır; sınav süresi, soru sayısı, resmî practice assessment'ın varlığı, sınav içi Microsoft Learn erişimi ve yenileme kuralları kaynaklarda tutarsız veya değişken ifade ediliyor. Bu README'deki rakamlar (50 soru / 100 dakika) **yalnızca bu denemenin varsayımıdır**; gerçek değerleri sınav sayfasından ve sınav başındaki ekrandan al.

---

### 5.2 50 Soruluk AI-200 Deneme Sınavı

> Strateji ve kullanım: [README.md](README.md) · Müfredat: *skills measured as of October 7, 2026*
>
> **Kurallar:** 100 dakika (soru başına ≈ 2 dakika; kod ve vaka soruları daha uzun sürer), kapalı kitap — kod ve CLI boşlukları dahil. Cevaplarını ayrı bir yere yaz; `<details>` bloklarını sınav bitene kadar açma. Her sorunun etiketi, sorunun **alanını** ve **konusunu** (C2.x) gösterir: **A** = Develop containerized solutions on Azure (C2.1–C2.3) · **B** = Develop AI solutions by using Azure data management services (C2.4–C2.7) · **C** = Connect to and consume Azure services (C2.8–C2.10) · **D** = Secure, monitor, and troubleshoot Azure solutions (C2.11–C2.13).
>
> Dağılım resmî ağırlıklara göre: **A 11 soru (%22) · B 17 soru (%34) · C 11 soru (%22) · D 11 soru (%22)**. Sorular **özgündür**; gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Biçimler: tek/çoklu seçim, evet/hayır, sıralama, eşleştirme, senaryo, **C#/Python/CLI/KQL/SQL/YAML/JSON tamamlama** ve **iki vaka çalışması** (13–16 ve 29–32). Gerçek sınavın puanı ölçeklenmiştir (geçme 700/1000); buradaki yüzde yalnızca bir göstergedir.

---

#### Soru 1: `[B · C2.4]` Tek seçim
Bir e-ticaret API'si siparişleri Cosmos DB for NoSQL'de tutacak. Sorguların %90'ı "bir müşterinin siparişleri", geri kalanı ise `id` ile tek sipariş okumadır. Müşteri sayısı milyonlarcadır ve siparişler müşterilere görece eşit dağılır. Container için **en uygun** partition key hangisidir?

- A) `/status` (`new`, `paid`, `shipped`)
- B) `/orderDate` (gün bazında)
- C) `/customerId`
- D) `/country`

<details><summary>Cevap</summary>

**C.** `/customerId` yüksek cardinality, iyi dağılım ve sorgu uyumu sağlar: "müşterinin siparişleri" tek partition'a gider, `ReadItemAsync(id, customerId)` ucuz bir point read olur. `/status` ve `/country` düşük cardinality'dir (hot partition, 429). `/orderDate` bugüne yazmayı tek partition'a yığar ve müşteri sorgularını cross-partition yapar. Partition key path'i container oluşturulduktan sonra değiştirilemez.
</details>

#### Soru 2: `[A · C2.1]` Tek seçim
Bir ekip container image'larını tek bir registry'de tutacak. Gereksinimler: aynı registry'nin **iki Azure bölgesine geo-replication** ile yayılması ve registry'ye yalnızca **VNet içinden (private endpoint)** erişilmesi. Hangi ACR SKU'su **gereklidir**?

- A) Basic
- B) Standard
- C) Premium
- D) Bu özellikler SKU'dan bağımsızdır; yalnızca `az acr update` ile açılır

<details><summary>Cevap</summary>

**C.** Geo-replication ve private endpoint (Private Link) **Premium**'a özgüdür. Basic/Standard aynı programatik API'yi sunar ama bu ağ/dağıtım özelliklerini içermez. SKU sonradan yükseltilebilir (`az acr update --sku Premium`), ama özellik SKU'ya bağlıdır (D yanlış).
</details>

#### Soru 3: `[C · C2.8]` Tek seçim
Bir ekip HTTP ve kuyruk tetiklemeli Azure Functions uygulamasını Linux üzerinde çalıştıracak. Gereksinimler: (1) Cosmos DB ve Service Bus'a **private endpoint** üzerinden erişim için **VNet entegrasyonu**, (2) kullanılmadığı zamanlarda **sıfıra ölçeklenme**, (3) mümkünse kullandığın kadar öde. Hangi hosting planı **en uygundur**?

- A) Klasik Consumption
- B) Flex Consumption
- C) Premium (Elastic Premium)
- D) Dedicated (App Service planı)

<details><summary>Cevap</summary>

**B.** Flex Consumption hem VNet entegrasyonu hem scale-to-zero sunar. Klasik Consumption'da VNet entegrasyonu yoktur. Premium'da en az bir instance sürekli çalışır (sıfıra inmez). Dedicated de sıfıra inmez ve sabit plan ücreti vardır.
</details>

#### Soru 4: `[D · C2.11]` Tek seçim
Bir Container Apps API'si, bir Key Vault'taki (Azure RBAC izin modeli) secret'ları **yalnızca okuyacak**. API'nin user-assigned managed identity'sine **en az yetki ilkesine** uygun hangi rol, vault kapsamında atanmalıdır?

- A) Contributor
- B) Key Vault Reader
- C) Key Vault Secrets User
- D) Key Vault Secrets Officer

<details><summary>Cevap</summary>

**C.** *Key Vault Secrets User* secret **değerini okur** (data plane). Contributor control plane rolüdür ve RBAC modelinde secret değerini okutmaz. Key Vault Reader yalnızca meta veriyi gösterir, değeri vermez. Secrets Officer yazma/silme yetkisi de içerir — okuma ihtiyacı için fazladır.
</details>

#### Soru 5: `[B · C2.6]` Tek seçim
Bir RAG uygulaması Azure Database for PostgreSQL'de pgvector kullanıyor. Tablo **boş** olarak oluşturulacak, veri uygulama çalışırken sürekli eklenecek/güncellenecek ve öncelik **yüksek recall ile düşük sorgu gecikmesi**. Hangi index türü **en uygundur**?

- A) IVFFlat; `lists` değeri tablo boşken verilsin
- B) HNSW
- C) Index yok; her zaman exact tarama
- D) B-tree

<details><summary>Cevap</summary>

**B.** HNSW boş tabloda kurulabilir, veri geldikçe kendini günceller ve genelde en iyi recall/hız dengesini verir. IVFFlat'in kümeleri **mevcut veriden** öğrenilir: boş tabloda kurmak anlamsızdır, veri dağılımı değişince yeniden kurulmalıdır. Index'siz tarama doğru ama yavaştır; B-tree vector benzerliğini hızlandırmaz.
</details>

#### Soru 6: `[A · C2.3]` Senaryo
AKS'te yeni dağıtımdan sonra `todoapi` pod'ları sürekli yeniden başlıyor. `kubectl describe pod` çıktısında şunlar var:

```text
Last State:   Terminated
  Reason:     OOMKilled
  Exit Code:  137
Limits:
  memory:  256Mi
```

Kök neden ve **en doğru** düzeltme hangisidir?

- A) Registry yetkisi eksik; kubelet kimliğine `AcrPull` ver
- B) Container bellek limitini aştı; gerçek kullanımı ölçüp limit'i artır veya bellek sızıntısını düzelt
- C) Readiness probe yolu yanlış; probe path'ini düzelt
- D) Service selector pod label'larıyla eşleşmiyor; selector'ı düzelt

<details><summary>Cevap</summary>

**B.** `OOMKilled` + `Exit Code 137` bellek limitinin aşıldığını gösterir (CPU limit aşımı yalnızca throttle eder, öldürmez). A `ImagePullBackOff/unauthorized`, C `READY 0/1`, D boş `endpoints` belirtisidir.
</details>

#### Soru 7: `[C · C2.9]` Tek seçim
Bir sipariş olayı **fraud**, **stok** ve **e-posta** servislerinin üçüne de ayrı ayrı iletilmeli; her servis kendi hızında ve birbirinden bağımsız işleyecek. Fraud servisi yalnızca `amount > 1000` siparişleri almalı. Hangi Service Bus tasarımı **en uygundur**?

- A) Tek queue; üç servis rakip tüketici olarak okur
- B) Bir topic + üç subscription; fraud subscription'ında SQL filter
- C) Üç ayrı Storage Queue
- D) Basic tier'da bir queue ve istemci tarafı filtreleme

<details><summary>Cevap</summary>

**B.** Topic'te her subscription mesajın **kendi kopyasını** alır (kendi lock/DLQ'su ile); filtre subscription'a yazılır. Queue'da mesajı yalnızca **bir** tüketici işler (A yanlış). Topic için en az **Standard** tier gerekir; Basic'te topic yoktur (D yanlış). Storage Queue'da topic/filter/DLQ yoktur.
</details>

#### Soru 8: `[D · C2.12]` Senaryo
Bir ekip 12 servisin ayarlarını merkezileştiriyor. Sorunlar: Dev ve Prod için aynı anahtarın farklı değerleri olmalı; ayarlar değişince servisler **yeniden dağıtım olmadan** yeni değeri almalı; veritabanı parolası ise düz metin olarak **hiçbir yerde** (App Configuration dahil) saklanmamalı. Hangi tasarım uygundur?

- A) Her şeyi `appsettings.Production.json` içine koy ve imajı yeniden build et
- B) App Configuration'da `Dev`/`Prod` **label**'ları; parola için **Key Vault referansı**; dinamik yenileme için sentinel key
- C) Tüm ayarları, parola dahil, App Configuration'a düz değer olarak yaz
- D) Tüm ayarları Key Vault secret olarak sakla ve feature flag'leri de oraya yaz

<details><summary>Cevap</summary>

**B.** Label ortam varyantı sağlar; Key Vault referansı secret'ı vault'ta tutar (uygulama kimliğinin vault'ta da yetkisi olmalıdır); sentinel ile restart'sız yenileme yapılır. A yeniden dağıtım ister, C secret'ı düz saklar, D Key Vault'u yapılandırma/feature flag deposu olarak kullanır (yanlış araç).
</details>

#### Soru 9: `[B · C2.4]` Kod tamamlama (C#)
Sipariş detay sayfası, `id` ve `customerId` değerini bildiği bir siparişi **mümkün olan en düşük RU** ile okumalı. Boşluğu doldurun:

```csharp
Container orders = client.GetContainer("shop", "orders");

ItemResponse<Order> r = await orders.______<Order>(
    "o-1", new PartitionKey("customer-42"));
Console.WriteLine($"RU: {r.RequestCharge}");
```

- A) `GetItemQueryIterator`
- B) `ReadItemAsync`
- C) `ReadManyItemsAsync`
- D) `CreateTransactionalBatch`

<details><summary>Cevap</summary>

**B.** `ReadItemAsync` **point read**'dir (id + partition key): 1 KB'lık item için yaklaşık 1 RU ile en ucuz okumadır. `SELECT * FROM c WHERE c.id = ...` yazan `GetItemQueryIterator` daha pahalıdır. `ReadManyItemsAsync` birden çok item içindir; batch ise yazma/okuma işlemlerini atomik gruplar.
</details>

#### Soru 10: `[A · C2.1]` CLI tamamlama
Geliştiricinin makinesinde Docker yok. İmaj ACR'da derlenecek, ardından Container Apps'in çalışma zamanı kimliği (managed identity) imajı çekebilecek. İki boşluğu **doğru sırayla** doldurun:

```bash
az acr ______ --registry $ACR --image todoapi:1.0.1 src/TodoApi

az role assignment create --assignee-object-id $PRINCIPAL \
  --assignee-principal-type ServicePrincipal \
  --role "______" \
  --scope $(az acr show --name $ACR --query id -o tsv)
```

- A) `run` · `AcrPush`
- B) `build` · `AcrPull`
- C) `build` · `Contributor`
- D) `import` · `AcrPull`

<details><summary>Cevap</summary>

**B.** `az acr build` kaynağı buluta gönderir, orada derler ve registry'ye push eder (yerel Docker gerekmez). Yalnızca **çekme** gereken runtime kimliğine en az yetkili rol `AcrPull`'dur. `az acr run` bir container/komut çalıştırır (build değil), `az acr import` var olan bir imajı kopyalar; `AcrPush` ve `Contributor` fazla yetkilidir.
</details>

#### Soru 11: `[D · C2.13]` KQL tamamlama
İş birimi, Application Insights'ta **5 dakikalık dilimlerle** hata oranını grafik olarak görmek istiyor (örnekleme açık olabilir). Boşluğu doldurun:

```kusto
requests
| where timestamp > ago(1h)
| summarize total = sum(itemCount),
            failed = sumif(itemCount, success == false)
            by ______(timestamp, 5m)
| extend errorRatePct = round(100.0 * failed / total, 2)
| project timestamp, errorRatePct
| render timechart
```

- A) `bin`
- B) `top`
- C) `take`
- D) `extend`

<details><summary>Cevap</summary>

**A.** `bin(timestamp, 5m)` zamanı 5 dakikalık kovalara yuvarlar; `render timechart` zaman ekseni gerektirir. `top`/`take` satır seçer, `extend` hesaplanan sütun ekler (kovalamaz). `sum(itemCount)` örneklenmiş telemetride gerçek sayıyı verir (`count()` eksik sayar).
</details>

#### Soru 12: `[B · C2.7]` Evet / Hayır
Azure Managed Redis'te vector araması için bir cache hazırlanıyor.

| İfade | Evet | Hayır |
|---|---|---|
| a) RediSearch modülü, cache oluşturulduktan sonra da eklenebilir. | ☐ | ☐ |
| b) RediSearch kullanılan cache'te eviction policy yalnızca `NoEviction` olabilir. | ☐ | ☐ |
| c) TTL (expiration), bellek dolduğunda sunucunun hangi anahtarı sileceğine karar verdiği mekanizmadır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** Modüller **oluşturma sırasında** seçilir; sonradan eklemek için cache yeniden oluşturulur. RediSearch ile yalnızca `NoEviction` desteklenir — bu yüzden bellek dolarsa yazma hatası olur ve tüm anahtarlara TTL vermek şarttır. c ifadesi **eviction**'ı tarif eder; TTL ise anahtara verilen **süre** dolunca kaydı temizleyen, uygulama kararıdır.
</details>

---

## Vaka çalışması 1 — Fabrikam Lojistik (13–16)

> **Durum:** Fabrikam, kargo takip sistemini Azure Container Apps (workload profiles ortamı) üzerinde çalıştırıyor: `shipments-api` (HTTP, dış ingress) ve `shipments-worker` (Service Bus `shipments` kuyruğunu işler). İmajlar bir ACR'da tutuluyor ve CI'da ACR Tasks ile üretiliyor; kimsenin makinesinde Docker yok.
>
> **Gereksinimler:**
> 1. Üretime giden imaj, test edilenle **birebir aynı** olmalı; hiçbir yerde registry parolası veya admin user olmamalı.
> 2. API'nin yeni sürümü önce trafiğin **%10**'una açılacak, sorun çıkarsa **hemen** eski sürüme dönülecek.
> 3. Worker kuyruk boşken **sıfır replica**, kuyruk dolunca ölçeklenmeli (en çok 8 replica).
> 4. Veritabanı parolası Key Vault'ta tutulacak, **90 günde bir** döndürülecek; uygulama yeni sürümü **yeniden dağıtım yapılmadan** almalı, en az yetki ilkesi geçerli.

#### Soru 13: `[A · C2.1]` Tek seçim — Vaka 1
Gereksinim 1'i karşılamak için CI hattı Container Apps'e hangi image referansı ve hangi çekme kimliğiyle dağıtım yapmalıdır?

- A) `fabrikam.azurecr.io/shipments-api:latest` · registry admin user
- B) `fabrikam.azurecr.io/shipments-api@sha256:<digest>` · user-assigned managed identity + `AcrPull`
- C) `fabrikam.azurecr.io/shipments-api:1.4.2` · managed identity + registry üzerinde `Contributor`
- D) `fabrikam.azurecr.io/shipments-api:sha-3fa91c2` · service principal parolası ortam değişkeninde

<details><summary>Cevap</summary>

**B.** **Digest** içeriğin değişmez kimliğidir ("test edileni birebir çalıştır"); tag'ler aynı adla başka manifest'e taşınabilir (kilitlenmediyse). Parolasız çekme için managed identity + yalnızca çekme yetkisi veren `AcrPull` kullanılır; ilk revision'da kimlik gerektiği için **user-assigned** identity pratiktir. A `latest` + admin user'dır, C fazla yetkilidir (Contributor registry'yi yönetir) ve tag değişebilir, D parola saklar.
</details>

#### Soru 14: `[A · C2.2]` CLI tamamlama — Vaka 1
Gereksinim 2 için `v2` revision'ı oluşturulduktan sonra iki boşluğu doldurun:

```bash
az containerapp revision set-mode -n shipments-api -g $RG --mode ______

az containerapp ingress traffic set -n shipments-api -g $RG \
  --revision-weight shipments-api--v1=__ shipments-api--v2=__
```

- A) `single` · 90 / 10
- B) `multiple` · 90 / 10
- C) `multiple` · 90 / 20
- D) `multiple` · 10 / 90

<details><summary>Cevap</summary>

**B.** Trafik bölme için **multiple** revision mode gerekir (single modda yalnızca son hazır revision %100 alır). Ağırlıkların toplamı **%100** olmalıdır: v1 = 90, v2 = 10. C geçersizdir (toplam 110), D canary'yi tersine çevirir. Geri alma, v1 aktifken ağırlığı `v1=100` yapmaktır — bu yüzden canary sürerken v1'i deactivate etme.
</details>

#### Soru 15: `[D · C2.11]` Tek seçim — Vaka 1
Gereksinim 4 için hangi yaklaşım doğrudur?

- A) Container App secret'ında **sürümlü** Key Vault URI'sini sabitle; uygulama kimliğine `Contributor` ver
- B) Container App secret'ını **sürümsüz** Key Vault URI'sine `keyvaultref` ile bağla; uygulamanın managed identity'sine vault'ta **Key Vault Secrets User** ver; döndürmeyi (yeni secret sürümü yazma) ayrı bir kimliğe (**Secrets Officer**) bırak
- C) Her döndürmeden sonra yeni parolayı Container App secret'ına elle kopyala
- D) Parolayı App Configuration'a düz değer olarak yaz, Key Vault'u kullanma

<details><summary>Cevap</summary>

**B.** Sürümsüz URI "en son sürüm" demektir; yeni sürüm yazıldığında platform bunu periyodik olarak alır (aralığı güncel dokümandan doğrula) veya revision restart ile hemen yansır. Uygulama yalnızca okur → *Secrets User*; yazma (rotation) yetkisi ayrı kimlikte kalır. A'da sürümlü URI rotation'ı **engeller** ve Contributor data plane'i okutmaz; C elle ve hataya açık; D secret'ı düz saklar.
</details>

#### Soru 16: `[A · C2.2]` Tek seçim — Vaka 1
Worker için aşağıdaki scale kuralı tanımlı. Kuyrukta **300** mesaj varken kaç replica beklenir ve kuyruk boşalınca ne olur?

```yaml
scale:
  minReplicas: 0
  maxReplicas: 8
  rules:
    - name: shipments-queue
      custom:
        type: azure-servicebus
        metadata:
          queueName: shipments
          namespace: sb-fabrikam
          messageCount: "25"
        identity: system
```

- A) 12 replica; kuyruk boşalınca hemen sıfıra iner
- B) 8 replica (ceil(300/25) = 12 istenir, `maxReplicas` ile sınırlanır); kuyruk boşalınca cooldown süresinden sonra sıfıra iner
- C) 25 replica; kuyruk boşalınca 1 replica kalır
- D) 1 replica; `messageCount` toplam mesaj sayısı eşiğidir

<details><summary>Cevap</summary>

**B.** `messageCount` **replica başına hedef** mesaj sayısıdır: 300/25 = 12 replica istenir, ama `maxReplicas: 8` tavandır. Kural 0 replica istediğinde son replica cooldown (varsayılan 300 sn) dolana kadar kapanmaz, sonra scale-to-zero olur. `identity: system` için kimliğin Service Bus'ta *Azure Service Bus Data Receiver* rolü olmalıdır.
</details>

---

#### Soru 17: `[C · C2.8]` Ayar tamamlama
Bir Service Bus trigger function'ı, **connection string olmadan**, function app'in system-assigned managed identity'siyle bağlanacak. Trigger: `[ServiceBusTrigger("orders", Connection = "SbConn")]`. İki boşluğu doldurun:

```text
App setting:  SbConn______ = mysb.servicebus.windows.net
Rol (namespace kapsamında): ______
```

- A) `__fullyQualifiedNamespace` · Azure Service Bus Data Receiver
- B) `:fullyQualifiedNamespace` · Contributor
- C) `__connectionString` · Azure Service Bus Data Receiver
- D) `__accountEndpoint` · Azure Service Bus Data Owner

<details><summary>Cevap</summary>

**A.** Identity-based bağlantılarda ayar adı **`<önek>__<özellik>`** (çift alt çizgi) biçimindedir; Service Bus için özellik `fullyQualifiedNamespace`'tir. Trigger mesaj aldığı/settle ettiği için *Data Receiver* yeterlidir (gönderen output için *Data Sender*). `:` ayırıcı app settings'te değil, Key Vault/App Configuration kaynaklı ayarlarda kullanılır; `accountEndpoint` Cosmos DB'ye aittir; Data Owner fazla yetkilidir.
</details>

#### Soru 18: `[B · C2.4]` Tek seçim
Bir uygulama kullanıcının yorumunu Cosmos DB'ye yazıyor ve **aynı kullanıcı** hemen ardından yorum listesini okuyor; kullanıcı kendi yorumunu **her zaman** görmeli. Hesap varsayılanı Session. Hedef: **en düşük RU ve gecikme**. Ne yapılmalıdır?

- A) Hesabı Strong'a yükselt
- B) Session'ı koru ve yazan/okuyan işlemde aynı `CosmosClient`'ı (veya session token'ı) kullan
- C) Eventual'a gevşet
- D) Bounded Staleness (K = 1) kullan

<details><summary>Cevap</summary>

**B.** Session, aynı session içinde *read-your-writes* garantisi verir ve Strong/Bounded'ın ~2× okuma RU'su maliyetini taşımaz. Bu garanti aynı client/session token'ı gerektirir; farklı süreçler arasında token taşınmalıdır. Eventual garanti vermez; Strong ve Bounded Staleness bu ihtiyaç için gereksiz pahalıdır.
</details>

#### Soru 19: `[A · C2.3]` Manifest hata bulma
`kubectl apply` hatasız tamamlanıyor, pod'lar `Running` ve `READY 1/1`, fakat Service'e yapılan istekler zaman aşımına uğruyor; `kubectl get endpoints todoapi -n todo` çıktısı `<none>`. Manifest'in ilgili parçaları:

```yaml
# Deployment
spec:
  selector:
    matchLabels: { app: todoapi }
  template:
    metadata:
      labels: { app: todoapi }
---
# Service
spec:
  selector:
    app: todo-api
  ports:
    - port: 80
      targetPort: 8080
```

Sorunun kaynağı ve çözümü nedir?

- A) Deployment `selector`'ı template label'larıyla eşleşmiyor; `matchLabels`'i değiştir
- B) Service `selector` pod label'larıyla eşleşmiyor; Service selector'ını `app: todoapi` yap
- C) `targetPort` yanlış; 80 olmalı
- D) Pod'lara `imagePullSecrets` eklenmeli

<details><summary>Cevap</summary>

**B.** Service `app: todo-api` arıyor, pod'lar `app: todoapi` taşıyor → Service'in **endpoint'i yok**. Deployment tarafında selector ile template label'ları eşleşiyor (aksi halde `apply` doğrulamada reddedilirdi — A farklı bir hatadır). `targetPort` container'ın dinlediği portu göstermelidir (8080 doğru). Pod'lar zaten çalıştığı için image çekme sorunu yok.
</details>

#### Soru 20: `[D · C2.12]` Kod tamamlama (C#)
ASP.NET Core uygulaması App Configuration'ı sentinel key ile dinamik yenileyecek. İki boşluğu doldurun:

```csharp
builder.Configuration.AddAzureAppConfiguration(o =>
    o.Connect(endpoint, new DefaultAzureCredential())
     .Select("App:*", env)
     .ConfigureRefresh(r => r
         .Register("App:Sentinel", env, refreshAll: ______)
         .SetRefreshInterval(TimeSpan.FromSeconds(30))));

builder.Services.AddAzureAppConfiguration();
var app = builder.Build();
app.Use______();
```

- A) `true` · `AzureAppConfiguration`
- B) `false` · `AzureAppConfiguration`
- C) `true` · `Authentication`
- D) `false` · `Refresh`

<details><summary>Cevap</summary>

**A.** Sentinel değişince **tüm** seçili anahtarların yeniden yüklenmesi için `refreshAll: true` gerekir (`false` yalnızca kayıtlı anahtarın kendisini yeniler). `app.UseAzureAppConfiguration()` refresh middleware'idir; yoksa ASP.NET Core'da yenileme tetiklenmez. Ayrıca tüketici tarafında `IOptions<T>` değil `IOptionsSnapshot<T>`/`IOptionsMonitor<T>` kullanılmalı, sentinel de değerlerden **sonra** güncellenmelidir.
</details>

#### Soru 21: `[B · C2.6]` SQL tamamlama
Metin embedding'leri (cosine) için PostgreSQL'de HNSW index kuruluyor ve sorgu index'i kullanmalı. İki boşluğu doldurun:

```sql
CREATE INDEX docs_hnsw ON documents
USING hnsw (embedding ______);

SELECT id, title FROM documents
ORDER BY embedding ____ $1
LIMIT 5;
```

- A) `vector_cosine_ops` · `<=>`
- B) `vector_l2_ops` · `<=>`
- C) `vector_cosine_ops` · `<->`
- D) `vector_ip_ops` · `<=>`

<details><summary>Cevap</summary>

**A.** Cosine distance operatörü `<=>`, onun operator class'ı `vector_cosine_ops`'tur. Sorgudaki operatör ile index'in operator class'ı eşleşmezse planlayıcı index'i **kullanmaz** (sessizce sequential scan): B, C ve D bu uyumsuzluğu taşır (`<->` = L2, `vector_ip_ops` = inner product `<#>`).
</details>

#### Soru 22: `[C · C2.9]` Senaryo
Bir ekip `high-value` subscription'ına `amount > 1000` SQL filter kuralı ekledi, fakat subscription 50 TL'lik mesajları da almaya devam ediyor. Mesajlar `amount` değerini `ApplicationProperties`'e yazıyor. En olası neden nedir?

- A) SQL filter yalnızca Premium tier'da çalışır
- B) Subscription ile birlikte gelen `$Default` (TrueFilter) kuralı silinmedi
- C) Filtreler yalnızca mesaj gövdesine bakar, `ApplicationProperties`'e değil
- D) Duplicate detection kapalı

<details><summary>Cevap</summary>

**B.** Yeni subscription `$Default` (her mesajı kabul eden) kuralıyla gelir; yeni kural **eklenir**, varsayılan kural silinmez ve kurallar OR ile birleşir. Çözüm: `az servicebus topic subscription rule delete ... --name '$Default'`. Filtre **gövdeye değil property'lere** bakar (C tersini söylüyor); SQL filter Standard'da da çalışır; duplicate detection filtreyle ilgisizdir.
</details>

#### Soru 23: `[A · C2.2]` Evet / Hayır
Container Apps'te çalışan bir uygulamada aşağıdaki değişikliklerden hangileri **yeni bir revision oluşturur**?

| Değişiklik | Evet | Hayır |
|---|---|---|
| a) Bir secret'ın değerini güncellemek (`az containerapp secret set`) | ☐ | ☐ |
| b) Bir ortam değişkenini (`--set-env-vars`) değiştirmek | ☐ | ☐ |
| c) Scale kuralındaki `messageCount` değerini değiştirmek | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Revision-scope (`properties.template`): image, env var, probe, kaynaklar ve **scale kuralları** → yeni revision. Application-scope (`properties.configuration`): secret'lar, ingress, trafik bölme kuralları, revision mode → yeni revision **oluşturmaz**; çalışan replica'ların yeni secret değerini alması için revision restart gerekir.
</details>

#### Soru 24: `[B · C2.5]` Kod tamamlama (SQL)
Cosmos DB for NoSQL'de, vector embedding policy ve vector index tanımlı bir container'da kategori filtreli **top-5 benzer ürün** sorgulanıyor. Boşluğu doldurun:

```sql
SELECT TOP 5 c.id, c.name,
       ______(c.embedding, @queryVector) AS score
FROM c
WHERE c.category = @category
ORDER BY ______(c.embedding, @queryVector)
```

- A) `VectorDistance`
- B) `ST_DISTANCE`
- C) `ARRAY_CONTAINS`
- D) `RANK`

<details><summary>Cevap</summary>

**A.** `VectorDistance(<vectorPath>, <queryVector>)` Cosmos DB NoSQL'in vector benzerlik fonksiyonudur; top-k için `TOP k` + `ORDER BY VectorDistance(...)` kullanılır. `ST_DISTANCE` coğrafi (spatial) mesafedir, `ARRAY_CONTAINS` dizi üyeliğine bakar, `RANK` bir hybrid search bileşenidir (tek başına benzerlik hesaplamaz). `WHERE` filtresi partition key üzerindeyse sorgu tek partition'a gider.
</details>

#### Soru 25: `[D · C2.13]` KQL okuma
Aşağıdaki sorgu Application Insights **Logs**'ta çalıştırılıyor:

```kusto
dependencies
| where timestamp > ago(1h)
| summarize p95 = percentile(duration, 95), calls = sum(itemCount) by target
| top 3 by p95 desc
```

Sonuç hakkında hangisi **doğrudur**?

- A) Son bir saatteki rastgele 3 bağımlılık çağrısını döndürür
- B) Her `target` için p95 süresini hesaplar ve p95'i en yüksek 3 `target`'i, süreye göre azalan sırayla döndürür
- C) En fazla 3 satır döndürür, ama `p95` sütunu `by` yan tümcesi olmadığı için hesaplanamaz
- D) Son bir saatte en çok çağrılan 3 `target`'i döndürür

<details><summary>Cevap</summary>

**B.** `summarize ... by target` her hedef için bir satır üretir; `top 3 by p95 desc` sıralar ve ilk 3'ü alır (`take` rastgele satır seçerdi — A yanlış). Sıralama `calls`'a göre değil `p95`'e göredir (D yanlış). `percentile(duration, 95)` `by` yan tümcesinden bağımsız hesaplanır (C yanlış). `sum(itemCount)` örneklemeyi hesaba katar.
</details>

#### Soru 26: `[C · C2.8]` Tek seçim
Bir Timer trigger'lı function **her gün 02:30'da** (function app'in saat dilimine göre, varsayılan UTC) çalışmalı. Hangi ifade doğrudur?

- A) `30 2 * * *`
- B) `0 30 2 * * *`
- C) `0 2 30 * * *`
- D) `2:30:00`

<details><summary>Cevap</summary>

**B.** Functions Timer trigger'ı **6 alanlı NCRONTAB** kullanır: `{saniye} {dakika} {saat} {gün} {ay} {haftanın günü}` → `0 30 2 * * *`. A klasik 5 alanlı cron'dur, C saat/dakikayı ters yazar, D bir NCRONTAB değildir. Saat dilimi varsayılan UTC'dir (`WEBSITE_TIME_ZONE` ile değişir).
</details>

#### Soru 27: `[B · C2.4]` JSON tamamlama
Cosmos DB container'ına yazma ağırlıklıdır ve sorgular yalnızca `customerId` ve `orderDate` alanlarını kullanır. Ayrıca sık çalışan sorgu şudur:

```sql
SELECT * FROM c ORDER BY c.customerId ASC, c.orderDate DESC
```

Yazma RU'sunu düşürmek ve bu `ORDER BY`'ı desteklemek için indexing policy'deki boşlukları doldurun:

```json
{
  "indexingMode": "consistent",
  "includedPaths": [ { "path": "/customerId/?" }, { "path": "/orderDate/?" } ],
  "excludedPaths": [ { "path": "______" } ],
  "compositeIndexes": [[
    { "path": "/customerId", "order": "ascending" },
    { "path": "/orderDate",  "order": "______" }
  ]]
}
```

- A) `/*` · `descending`
- B) `/*` · `ascending`
- C) `/_etag/?` · `descending`
- D) `/customerId/?` · `descending`

<details><summary>Cevap</summary>

**A.** Dar (allow-list) indeks için `excludedPaths: [/*]` + yalnızca gerekli yollar `includedPaths`'te; yazma RU'su ve depolama düşer. Birden çok alanla `ORDER BY` için **composite index** gerekir ve sıra/yön sorgudaki ile aynı (veya tam ters) olmalıdır: `customerId ASC, orderDate DESC` → `ascending`, `descending`. Composite olmadan bu sorgu hata verir.
</details>

#### Soru 28: `[D · C2.11]` Kod tamamlama (Python)
Bir rotation function'ı, hedef serviste yeni parolayı ürettikten sonra Key Vault'a **yeni bir secret sürümü** yazmalı. Boşluğu doldurun:

```python
from datetime import datetime, timedelta, timezone
from azure.identity import DefaultAzureCredential
from azure.keyvault.secrets import SecretClient

client = SecretClient(vault_url=VAULT_URL, credential=DefaultAzureCredential())

client.______(
    "Db--ConnectionString",
    new_password,
    expires_on=datetime.now(timezone.utc) + timedelta(days=90),
)
```

- A) `update_secret_properties`
- B) `set_secret`
- C) `begin_recover_deleted_secret`
- D) `purge_deleted_secret`

<details><summary>Cevap</summary>

**B.** `set_secret` aynı adla çağrıldığında **yeni bir sürüm** üretir (sürümsüz okuyanlar en son sürümü görür). `update_secret_properties` mevcut sürümün yalnızca özniteliklerini (etkin/expires/etiket) günceller, değer yazmaz; `begin_recover_deleted_secret` ve `purge_deleted_secret` soft-delete yaşam döngüsü işlemleridir. İşleyicinin Entra kimliğine *Key Vault Secrets Officer* gerekir.
</details>

---

## Vaka çalışması 2 — Northwind Outdoor (29–32)

> **Durum:** Northwind, 3 milyon ürünlü kataloğunu Azure Cosmos DB for NoSQL'de tutuyor (`products` container'ı, partition key `/category`). Ürün açıklamalarından `text-embedding-3-small` (1536 boyut) ile embedding üretiliyor ve "su geçirmez yürüyüş botu" gibi **anlamsal arama** yapılıyor. Bir sohbet asistanı aynı soruların çoğunu tekrar tekrar LLM'e soruyor.
>
> **Gereksinimler:**
> 1. Kategori filtreli vector arama, milyonlarca kayıtta **düşük gecikme/RU** ile çalışmalı.
> 2. Editörler açıklamayı güncelleyince embedding **otomatik** yenilenmeli; 3 worker örneği işi paylaşmalı, yeniden başlatılan worker **baştan başlamamalı**.
> 3. Tekrarlanan sorularda **LLM maliyeti**ni düşürmek için Azure Managed Redis'te semantic cache kurulacak.
> 4. Storage'a görsel yüklendiğinde yalnızca **`.jpg`** dosyaları için küçük resim (thumbnail) üretilecek; teslim edilemeyen olaylar **incelenmek üzere saklanacak**.

#### Soru 29: `[B · C2.5]` Tek seçim — Vaka 2
Gereksinim 1 için yeni container'ın vector yapılandırması **hangisi olmalıdır**?

- A) `flat` index, 3072 boyut, vector yolu normal range index'e dahil
- B) `diskANN` index, `dimensions: 1536`, `distanceFunction: cosine`, vector yolu `excludedPaths`'e eklenmiş
- C) `quantizedFlat` index, `dimensions: 1536`, vector yolu `includedPaths`'te
- D) Vector policy'siz container; embedding'ler normal alan olarak saklanıp `ORDER BY` ile sıralanır

<details><summary>Cevap</summary>

**B.** Milyonlarca vector için **diskANN** (ANN) uygundur; `dimensions` embedding modelinin çıktısıyla (1536) eşleşmeli ve vector policy ile vector index **container oluşturulurken** verilmelidir. Vector yolu `excludedPaths`'e alınır (range index'e sokmak yazma RU'sunu şişirir; arama `vectorIndexes` ile yapılır). `flat` küçük veri içindir, `quantizedFlat` orta ölçektir, 3072 modelle uyuşmaz; policy/index olmadan sorgu brute-force taranır.
</details>

#### Soru 30: `[B · C2.5]` Çoklu seçim (iki doğru cevap) — Vaka 2
Gereksinim 2 için change feed processor'ı 3 worker örneğiyle çalıştırırken, işin **çift işlenmeden paylaşılması** ve yeniden başlatmada **kaldığı yerden devam edilmesi** için **hangi iki** yapılandırma gereklidir?

- A) Tüm örneklerde aynı `processorName`
- B) Tüm örneklerin paylaştığı tek bir `leases` container'ı
- C) Tüm örneklerde aynı `instanceName`
- D) Her örnek için ayrı bir lease container'ı

<details><summary>Cevap</summary>

**A ve B.** Aynı processor adı + ortak lease container, lease'lerin (partition aralığı başına sahiplik + checkpoint) örnekler arasında paylaşılmasını ve bir örnek durduğunda devralınmasını sağlar; yeniden başlatma lease'teki continuation token'dan devam eder. `instanceName` her örnekte **benzersiz** olmalıdır (C yanlış). Örnek başına ayrı lease container'ı herkesin tüm değişiklikleri işlemesine yol açar (D yanlış). Handler'ın **idempotent** olması ve kendi PATCH'inin feed'e geri dönüp döngü yaratmaması için bir hash/versiyon koruması da gerekir.
</details>

#### Soru 31: `[B · C2.7]` Tek seçim — Vaka 2
Gereksinim 3 için Azure Managed Redis cache'i nasıl hazırlanmalıdır?

- A) Memory Optimized, modülsüz oluştur; RediSearch'i sonradan ekle
- B) Oluştururken **RediSearch** modülünü seç, **Enterprise** clustering policy ve **NoEviction** kullan; cache girdilerine TTL ver
- C) Flash Optimized katman, `allkeys-lru` eviction
- D) OSS cluster policy, `allkeys-lru` eviction

<details><summary>Cevap</summary>

**B.** Vector araması/semantic cache için RediSearch gerekir; modül **oluşturma sırasında** seçilir, RediSearch Enterprise clustering policy ve yalnızca `NoEviction` ile çalışır — bu yüzden bellek dolmaması için her girdiye TTL verilir. A (sonradan modül) mümkün değildir; C'de Flash Optimized katmanda vector modülü desteklenmez *(doğrula)* ve `allkeys-lru` RediSearch ile uyumsuzdur; D de uyumsuz politika seçimleridir.
</details>

#### Soru 32: `[C · C2.10]` CLI tamamlama — Vaka 2
Gereksinim 4 için Storage account'a bir **system topic** oluşturuldu. Event subscription'daki iki boşluğu doldurun:

```bash
az eventgrid system-topic event-subscription create -n thumbs \
  --system-topic-name st-images -g $RG \
  --endpoint-type azurefunction --endpoint $FUNC_ID \
  --included-event-types Microsoft.Storage.BlobCreated \
  --subject-ends-with ______ \
  --deadletter-endpoint $ST_ID/blobServices/default/containers/______
```

- A) `.jpg` · `deadletter` (önceden oluşturulmuş blob container adı)
- B) `image/jpeg` · `deadletter`
- C) `.jpg` · hiçbir şey; dead-letter varsayılan olarak açıktır, parametre gerekmez
- D) `data.contentType` · `$deadletterqueue`

<details><summary>Cevap</summary>

**A.** Dosya uzantısı gibi **yol/sonek** koşulları `subject` filtreleriyle (`--subject-ends-with .jpg`) yapılır; `image/jpeg` bir `subject` değeri değil içerik türüdür (B), `data.contentType` ise advanced filter anahtarıdır, `--subject-ends-with` için değildir (D). Event Grid'de **dead-letter varsayılan olarak kapalıdır**; teslim edilemeyen olayları saklamak için bir blob container verilmelidir (C yanlış); `$deadletterqueue` Service Bus kavramıdır.
</details>

---

#### Soru 33: `[A · C2.1]` Evet / Hayır
ACR image yönetimi hakkında aşağıdaki ifadeleri değerlendirin.

| İfade | Evet | Hayır |
|---|---|---|
| a) Aynı `todoapi:1.4.2` tag'i yeniden push edilirse (tag kilitli değilse) başka bir manifest'e işaret edebilir. | ☐ | ☐ |
| b) Base image update trigger'ı, task bir kez çalışıp bağımlılıkları izlenmeye başlamadan önce de çalışır. | ☐ | ☐ |
| c) ACR retention policy (Premium) tag'li image'ları da N gün sonra siler. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Hayır.** Tag değişebilir, **digest değişmez** (tag'i `--write-enabled false` ile kilitleyebilirsin). Base image bağımlılıkları task'ın **ilk build'inden sonra** izlenir; ayrıca base image sabit tag taşımalı ve (çok aşamalı Dockerfile'da) runtime image izlenir. Retention policy yalnızca **untagged** manifest'leri temizler.
</details>

#### Soru 34: `[B · C2.6]` Çoklu seçim (iki doğru cevap)
Çok kiracılı bir RAG uygulamasında, verinin ~%1'ini oluşturan bir kiracı için `WHERE tenant_id = 7 ORDER BY embedding <=> $1 LIMIT 5` sorgusu HNSW index'iyle **5'ten az satır** döndürüyor. **Hangi iki** çözüm uygundur?

- A) `SET hnsw.iterative_scan = relaxed_order;` (pgvector 0.8.0+)
- B) Tabloyu `PARTITION BY LIST (tenant_id)` ile bölmek ve partition başına HNSW index kullanmak
- C) `maintenance_work_mem` değerini artırmak
- D) `vector(1536)` kolonunu `vector(3072)` yapmak

<details><summary>Cevap</summary>

**A ve B.** HNSW önce `ef_search` kadar (varsayılan 40) aday döndürür, `WHERE` bunları **sonradan** eler ("over-filtering"). Iterative scan, yeterli sonuç çıkana kadar taramaya devam eder; partitioning kiracı filtresini tek partition'a/index'e indirir (ek olarak `hnsw.ef_search` artırımı, partial index veya B-tree de seçenektir). `maintenance_work_mem` yalnızca **index build** hızını etkiler; boyutu artırmak recall sorununu çözmez ve embedding modeliyle uyuşmazlık yaratır.
</details>

#### Soru 35: `[C · C2.9]` Kod tamamlama (C#)
Bir operatör, `order-processing` kuyruğunun **dead-letter alt kuyruğundaki** mesajları okuyup inceleyecek. Boşluğu doldurun:

```csharp
await using ServiceBusReceiver dlq = client.CreateReceiver(
    "order-processing",
    new ServiceBusReceiverOptions { SubQueue = SubQueue.______ });

ServiceBusReceivedMessage msg = await dlq.ReceiveMessageAsync();
Console.WriteLine($"{msg.DeadLetterReason}: {msg.DeadLetterErrorDescription}");
```

- A) `None`
- B) `DeadLetter`
- C) `TransferDeadLetter`
- D) `Deferred`

<details><summary>Cevap</summary>

**B.** DLQ'yu okumak normal bir receiver + `SubQueue.DeadLetter`'dır (`<queue>/$deadletterqueue`); mesajlar `DeadLetterReason`/`DeadLetterErrorDescription` taşır. `None` ana kuyruktur, `TransferDeadLetter` auto-forward/transfer hatalarının kuyruğudur; `Deferred` bir `SubQueue` değeri değildir (ertelenmiş mesajlar `SequenceNumber` ile alınır). Yeniden işleme için mesajı orijinal varlığa **yeni mesaj olarak** gönderip DLQ'dakini Complete et.
</details>

#### Soru 36: `[D · C2.12]` Tek seçim
Yeni ödeme akışı **önce iç çalışanlara (beta grubu)**, sonra kademeli olarak kullanıcıların %5 → %20 → %100'üne açılacak. Her kullanıcı, açıldığı andan itibaren **tutarlı** biçimde aynı deneyimi görmeli. Hangi feature filter **en uygundur**?

- A) `Microsoft.Percentage`
- B) `Microsoft.TimeWindow`
- C) `Microsoft.Targeting`
- D) Özel bir `IFeatureFilter` yazılmalı; yerleşik filtre yok

<details><summary>Cevap</summary>

**C.** **Targeting** kullanıcı/grup hedefleme ve varsayılan yüzde ile kullanıcıya göre **tutarlı** karar verir (ring deployment). Percentage rastgeledir: aynı kullanıcı farklı isteklerde farklı sonuç alabilir. TimeWindow tarih penceresi içindir. Yerleşik Targeting yeterli olduğundan özel filtre gerekmez.
</details>

#### Soru 37: `[A · C2.2]` Çoklu seçim (iki doğru cevap)
Container Apps ölçekleme kuralları hakkında **hangi iki** ifade doğrudur?

- A) HTTP scale rule ile uygulama sıfır replica'ya inebilir.
- B) Yalnızca CPU kullanımına dayalı bir kuralla uygulama sıfır replica'ya inebilir.
- C) Service Bus kuralındaki `messageCount`, kuyruktaki **toplam** mesaj sayısı eşiğidir.
- D) Birden çok scale kuralı varsa replica sayısı, **en çok replica isteyen** kurala göre belirlenir.

<details><summary>Cevap</summary>

**A ve D.** HTTP (ve TCP/KEDA olay) kuralları scale-to-zero yapabilir; CPU/bellek kuralları yapamaz (en az 1 replica). `messageCount` **replica başına hedeftir**, toplam değildir. Kurallar çakışınca en yüksek replica talebi kazanır.
</details>

#### Soru 38: `[B · C2.7]` Kod tamamlama (Redis)
`text-embedding-3-small` (1536 boyut) embedding'leri için Azure Managed Redis'te vector index oluşturuluyor ve KNN sorgusu çalıştırılıyor. İki boşluğu doldurun:

```text
FT.CREATE idx:products ON HASH PREFIX 1 product: SCHEMA
    category TAG
    embedding VECTOR HNSW 6 TYPE FLOAT32 DIM ______ DISTANCE_METRIC COSINE

FT.SEARCH idx:products "(@category:{footwear})=>[KNN 5 @embedding $vec AS score]"
    PARAMS 2 vec <blob> SORTBY score RETURN 2 name score DIALECT ____
```

- A) `1536` · `2`
- B) `512` · `1`
- C) `1536` · `0`
- D) `3072` · `2`

<details><summary>Cevap</summary>

**A.** `DIM` modelin çıktı boyutuyla **aynı** olmalıdır (yanlış boyutlu belge sessizce indekslenmez; `FT.INFO` `hash_indexing_failures` artar). Vector KNN sözdizimi için **`DIALECT 2`** gerekir. `HNSW 6` altı token'lık öznitelik sayısıdır (`TYPE FLOAT32 DIM n DISTANCE_METRIC COSINE`). KNN skoru COSINE'da bir **mesafedir** (küçük = benzer).
</details>

#### Soru 39: `[C · C2.10]` Eşleştirme
Her ihtiyacı en uygun servisle eşleştirin: **Azure Event Grid · Azure Service Bus · Azure Event Hubs**

1. Ödeme komutları **sırayla**, **session** ve **dead-letter queue** ile, işlenene kadar dayanıklı biçimde tutulmalı.
2. "Blob oluştu" gibi bir **olay bildirimi**, birbirinden habersiz birçok aboneye **push** edilmeli; her abone kendi filtresini tanımlıyor.
3. IoT cihazlarından **saniyede yüz binlerce telemetri olayı** alınıp consumer group'larla okunacak.

<details><summary>Cevap</summary>

**1 → Service Bus · 2 → Event Grid · 3 → Event Hubs.** Service Bus iş komutu/mesajı içindir (kuyruk, PeekLock, session, DLQ, transaction). Event Grid "oldu" bilgisini filtreli, push tabanlı fan-out ile dağıtır (kuyruk değildir). Event Hubs yüksek hacimli telemetri akışı (partition + consumer group) içindir. Birlikte kullanım: Event Grid → handler olarak Service Bus queue.
</details>

#### Soru 40: `[D · C2.13]` Senaryo
Application Insights'ta **%20 sabit yüzde sampling** açıkken, bir pano `requests | summarize n = count()` ile günlük istek sayısını 10.000 gösteriyor; yük dengeleyici ise yaklaşık 50.000 istek raporluyor. Bu farkı **doğru** gideren ve metrikleri yanıltmayan yaklaşım hangisidir?

- A) Sampling'i kapatmak için `connection string` yerine instrumentation key kullanmak
- B) `count()` yerine `sum(itemCount)` kullanmak
- C) `where` koşulunu `summarize`'dan sonraya taşımak
- D) Log Analytics workspace'ini başka bölgeye taşımak

<details><summary>Cevap</summary>

**B.** Örneklenmiş telemetride her kayıt `itemCount` kadar gerçek isteği **temsil eder**; `count()` eksik sayar, `sum(itemCount)` gerçek sayıyı verir. Instrumentation-key-only ingestion artık sonlandırılmıştır ve sampling kararıyla ilgisizdir; `where` yeri sayıyı değil süzme anlamını değiştirir; bölge sayımı etkilemez. Alert'leri örneklenmeyen **metrikler** üzerine kurmak da daha güvenilirdir.
</details>

#### Soru 41: `[A · C2.3]` Sıralama
Yeni dağıtımdan sonra `todoapi` pod'ları `CrashLoopBackOff`'ta. Teşhis ve düzeltme adımlarını **doğru sıraya** koyun:

1. Düzeltmeyi uygula (eksik yapılandırmayı ekle) ve gerekirse `kubectl rollout undo deploy/todoapi`; ardından `kubectl rollout status` ile doğrula
2. `kubectl logs <pod> --previous` ile çöken container'ın önceki çalışma logunu oku
3. `kubectl get pods -n todo` ile durum, `READY` ve `RESTARTS` değerlerine bak
4. `kubectl describe pod <pod> -n todo` ile `Last State`, `Reason`, `Exit Code` ve Events'e bak

<details><summary>Cevap</summary>

**3 → 4 → 2 → 1.** Sistematik teşhis sırası: durum (`get`) → ayrıntı (`describe`: Exit Code 1 = uygulama hatası, 137 = OOM) → log (`--previous`; yeni container'ın logu çoğu zaman boştur) → düzeltme/geri alma ve doğrulama. `CrashLoopBackOff` bir **belirtidir**; kök neden `logs --previous` ve `describe`'dadır.
</details>

#### Soru 42: `[B · C2.4]` Evet / Hayır
Cosmos DB for NoSQL hakkında aşağıdaki ifadeleri değerlendirin.

| İfade | Evet | Hayır |
|---|---|---|
| a) Transactional batch, **farklı partition key** değerlerine sahip item'ları tek bir atomik işlemde yazabilir. | ☐ | ☐ |
| b) 429 (Request rate too large) aldığında .NET SDK varsayılan olarak isteği otomatik yeniden dener. | ☐ | ☐ |
| c) İstek bazında consistency level, hesap varsayılanından **daha güçlü** bir seviyeye çıkarılabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** Transactional batch'te tüm işlemler **aynı partition key**'e ait olmalıdır. SDK, `x-ms-retry-after-ms` kadar bekleyip (varsayılan 9 deneme / 30 sn toplam) yeniden dener; kalıcı 429 = RU yetersiz veya hot partition. Consistency istek bazında yalnızca **gevşetilebilir**, güçlendirilemez.
</details>

#### Soru 43: `[C · C2.8]` Çoklu seçim (iki doğru cevap)
Azure Functions trigger/binding kavramları hakkında **hangi iki** ifade doğrudur?

- A) Bir function'ın tam olarak bir trigger'ı vardır.
- B) Output binding function'ı **başlatabilir**.
- C) Cosmos DB'ye identity-based bağlantıda hesap üzerinde Azure RBAC `Contributor` rolü yeterlidir.
- D) Python v2 programlama modelinde trigger ve binding'ler `function.json` yerine decorator'larla tanımlanır.

<details><summary>Cevap</summary>

**A ve D.** Function'ı yalnızca **trigger** başlatır (function başına tam bir tane); input/output binding'ler başlatmaz. Cosmos DB'de veri erişimi Azure RBAC ile değil **Cosmos DB data-plane role assignment**'ı (`az cosmosdb sql role assignment create`) ile verilir. .NET isolated'da attribute, Python v2'de decorator kullanılır.
</details>

#### Soru 44: `[D · C2.11]` Sıralama
Key Vault'taki bir secret için **otomatik rotation** akışını doğru sıraya koyun:

1. Function, hedef serviste (ör. ikincil anahtar/ikinci kimlik bilgisi) **yeni parolayı üretir**
2. Key Vault, secret süresi dolmadan önce `Microsoft.KeyVault.SecretNearExpiry` olayını yayınlar
3. Uygulama, sürümsüz referansı yenileyerek yeni sürümü kullanmaya başlar
4. Event Grid olayı abone olan Azure Function'a iletir
5. Function, Key Vault'a **yeni bir secret sürümü** yazar

<details><summary>Cevap</summary>

**2 → 4 → 1 → 5 → 3.** Event Grid rotation'ı **yapmaz**, yalnızca tetikler; yeni değeri hedef serviste üreten ve vault'a yazan sizin kodunuzdur (handler **idempotent** olmalı: Event Grid at-least-once teslim eder). Yalnızca `SecretNearExpiry`/`SecretExpired` türlerine abone ol — `SecretNewVersionCreated`'a abone olup aynı function'dan yazmak sonsuz döngü doğurur. Uygulama sürümsüz referans + periyodik yenileme (`ReloadInterval`, platform referans yenilemesi) ile yeni sürümü alır.
</details>

#### Soru 45: `[B · C2.5]` Tek seçim
Bir ekip change feed processor'ı **latest version** (varsayılan) modunda çalıştırıyor. Bir ürün fiziksel olarak silindiğinde embedding temizleme mantığı hiç tetiklenmiyor. Neden ve en pratik çözüm nedir?

- A) Processor silmeleri yalnızca lease container boşken görür
- B) Latest version modu silmeleri **göstermez**; fiziksel silme yerine `deleted: true` (soft delete) yaz (veya *all versions and deletes* modunu kullan)
- C) Silmeler yalnızca Functions Cosmos DB trigger'ında görünür
- D) `diskANN` index silmeleri gizler

<details><summary>Cevap</summary>

**B.** Latest version modu yalnızca eklenen/güncellenen item'ların son halini gösterir; silmeler ve TTL ile silinenler görünmez. Çözüm soft delete (+ `ttl` ile sonra temizlik) veya *all versions and deletes* modudur (continuous backup gerektirir). Functions trigger'ı da aynı change feed'i kullanır (C yanlış); lease ve vector index silme görünürlüğünü etkilemez.
</details>

#### Soru 46: `[C · C2.9]` Çoklu seçim (iki doğru cevap)
Bir sipariş sisteminde: (1) aynı müşterinin sipariş güncellemeleri **sırayla ve tek tüketicide** işlenmeli, (2) çift tıklamayla aynı sipariş **iki kez gönderilirse** ikincisi yutulmalı. **Hangi iki** yapılandırma gerekir?

- A) Kuyrukta `RequiresSession = true` ve mesajlarda `SessionId = customerId`
- B) Duplicate detection (kuyruğu oluştururken açılır) ve `MessageId = orderId`
- C) `ReceiveAndDelete` alma modu
- D) Basic tier

<details><summary>Cevap</summary>

**A ve B.** Sıra + tek alıcı için **session**; gönderici tekrarını elemek için **duplicate detection** (oluştururken seçilir, sonradan açılamaz; `MessageId` üzerinden, zaman penceresi içinde). `ReceiveAndDelete` at-most-once'tır ve sıra sağlamaz; Basic'te session ve duplicate detection yoktur (Standard+ gerekir). Duplicate detection tüketici idempotency'sinin yerini tutmaz.
</details>

#### Soru 47: `[B · C2.6]` Tek seçim
Bir API 200 pod'da çalışıyor; her istekte yeni `NpgsqlConnection` açıyor ve yük altında `remaining connection slots are reserved` / `too many connections` hataları alıyor. Azure Database for PostgreSQL Flexible Server için **en uygun** çözüm hangisidir?

- A) `max_connections` değerini sınırsız yapmak
- B) Pod başına tek `NpgsqlDataSource` (singleton, küçük `MaxPoolSize`) kullanmak ve yerleşik **PgBouncer**'a (port 6432) bağlanmak
- C) Vector index'i IVFFlat'e çevirmek
- D) `Multiplexing`'i kapatıp pool'u kaldırmak

<details><summary>Cevap</summary>

**B.** PostgreSQL bağlantısı süreç tabanlı ve pahalıdır; istemci havuzu (singleton `NpgsqlDataSource`) + sunucu tarafı PgBouncer çoklaması bağlantı sayısını düşürür. Kural: `uygulama örneği × MaxPoolSize ≤ sunucu max_connections` (yedek bağlantılar düşülerek). `max_connections` sınırsız yapılamaz (SKU'ya bağlıdır); IVFFlat bağlantıyla ilgisizdir; pool'u kaldırmak sorunu büyütür. PgBouncer transaction pooling'inde session düzeyi `SET` yerine `SET LOCAL` kullan.
</details>

#### Soru 48: `[D · C2.13]` Kod tamamlama (C#)
`orders-worker` servisi Application Insights'a OpenTelemetry Distro ile telemetri gönderecek ve kendi `ActivitySource`'unun (`"Orders.Worker"`) span'leri de **dinlenecek**. İki boşluğu doldurun:

```csharp
builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("orders-worker"))
    .______()
    .WithTracing(t => t.______("Orders.Worker"));
```

- A) `UseAzureMonitor` · `AddSource`
- B) `AddAzureMonitor` · `AddProcessor`
- C) `UseAzureMonitor` · `AddInstrumentation`
- D) `AddApplicationInsightsTelemetry` · `AddSource`

<details><summary>Cevap</summary>

**A.** `UseAzureMonitor()` (paket `Azure.Monitor.OpenTelemetry.AspNetCore`) Distro'yu etkinleştirir (bağlantı `APPLICATIONINSIGHTS_CONNECTION_STRING` ile); `AddSource("Orders.Worker")` ile kaydedilmeyen bir `ActivitySource`'un span'leri **dinlenmez ve gönderilmez**. `AddService` ise `cloud_RoleName`'i belirler. D klasik Application Insights SDK çağrısıdır; `AddInstrumentation` kütüphane enstrümantasyonu içindir, kaynak kaydı için değil.
</details>

#### Soru 49: `[C · C2.10]` CLI tamamlama
Bir custom topic'e gelen sipariş olaylarından yalnızca `data.amount` değeri **1000'den büyük** olanlar handler'a gidecek; teslim en fazla **3 kez** denenecek. İki boşluğu doldurun:

```bash
az eventgrid event-subscription create --name high-value-orders \
  --source-resource-id $TOPIC_ID \
  --endpoint-type azurefunction --endpoint $FUNC_ID \
  --advanced-filter data.amount ______ 1000 \
  --______ 3
```

- A) `NumberGreaterThan` · `max-delivery-attempts`
- B) `NumberIn` · `event-ttl`
- C) `StringGreaterThan` · `max-delivery-attempts`
- D) `NumberGreaterThan` · `max-events-per-batch`

<details><summary>Cevap</summary>

**A.** Payload alanına göre filtreleme **advanced filter**'dır; sayısal karşılaştırma için `NumberGreaterThan`. Deneme sayısı `--max-delivery-attempts` ile (en çok 30 klasik topic'te), olayın yaşam süresi ise `--event-ttl` ile (dakika) ayarlanır — ikisi birlikte verilirse **önce dolan** işi bitirir. `NumberIn` küme eşleşmesidir, `StringGreaterThan` bu bağlamda sayısal karşılaştırma yapmaz, `max-events-per-batch` toplu teslimi ayarlar.
</details>

#### Soru 50: `[B · C2.7]` Tek seçim
Bir ürün API'sinde cache-aside kullanılıyor. Fiyat güncellendikten sonra kullanıcılar eski fiyatı **10 dakikaya kadar** görüyor (TTL 10 dk). Stampede riski yaratmadan bayatlığı **en çok azaltan** değişiklik hangisidir?

- A) TTL'i 1 saniyeye düşürmek
- B) Veritabanına yazdıktan **sonra** ilgili anahtarı `DEL` ile silmek (invalidate); TTL'i güvenlik ağı olarak korumak
- C) Önce cache'i güncelleyip veritabanına sonra yazmak (write-behind)
- D) Eviction policy'yi `allkeys-lru` yapmak

<details><summary>Cevap</summary>

**B.** Yazınca-sil (write-invalidate) bayatlığı saniyelere indirir; sıra "önce kaynak, sonra invalidate"tir ve TTL, `DEL` başarısız olursa güvenlik ağıdır. A hit oranını çökertir ve popüler anahtarlarda stampede doğurur; C veri kaybı riski taşır (cache system of record değildir); D bellek dolunca ne silineceğini belirler, bayatlığı çözmez. Stampede için ayrıca jitter/kilit/HybridCache düşünülebilir.
</details>

---

## Puan tablosu

Her doğru cevabı ilgili satıra işaretle (çoklu seçim, evet/hayır, eşleştirme, sıralama ve çok boşluklu tamamlama sorularında **tümü** doğruysa 1 puan say). Vaka alt soruları ayrı ayrı puanlanır.

| Alan | Ağırlık | Soru numaraları | Doğru | Yüzde |
|---|---|---|---|---|
| **A** — Develop containerized solutions (C2.1–C2.3) | %20-25 | 2, 6, 10, 13, 14, 16, 19, 23, 33, 37, 41 | ___ / 11 | |
| **B** — AI solutions with data management services (C2.4–C2.7) | %30-35 | 1, 5, 9, 12, 18, 21, 24, 27, 29, 30, 31, 34, 38, 42, 45, 47, 50 | ___ / 17 | |
| **C** — Connect to and consume Azure services (C2.8–C2.10) | %20-25 | 3, 7, 17, 22, 26, 32, 35, 39, 43, 46, 49 | ___ / 11 | |
| **D** — Secure, monitor, and troubleshoot (C2.11–C2.13) | %20-25 | 4, 8, 11, 15, 20, 25, 28, 36, 40, 44, 48 | ___ / 11 | |
| **Toplam** | | | ___ / 50 | |

**Yorum:** ≥ 42 (%84) → sınav tarihi al (resmî practice assessment varsa önce onu çöz) · 35-41 → zayıf alanın konularını tekrar et, bir hafta sonra soruları farklı sırayla yeniden çöz · < 35 → zayıf konuların README + lab + quiz döngüsüne geri dön.

## Yanlışlarını C2.x ile eşleştir

| Konu | Bu sınavdaki sorular | Yanlış sayın | Tekrar edilecek |
|---|---|---|---|
| C2.1 Container Registry ve ACR Tasks | 2, 10, 13, 33 | | [README](#11-azure-container-registry-ve-acr-tasks) · [quiz](#-bölüm-11-quiz-soruları) |
| C2.2 Container Apps: revision ve KEDA | 14, 16, 23, 37 | | [README](#12-azure-container-apps-ortam-revision-ve-keda) · [quiz](#-bölüm-12-quiz-soruları) |
| C2.3 AKS manifest ve teşhis | 6, 19, 41 | | [README](#13-akse-manifest-ile-dağıtım-ve-container-teşhisi) · [quiz](#-bölüm-13-quiz-soruları) |
| C2.4 Cosmos DB for NoSQL | 1, 9, 18, 27, 42 | | [README](#21-cosmos-db-for-nosql-sdk-partitioning-indexing-consistency-ru) · [quiz](#-bölüm-21-quiz-soruları) |
| C2.5 Cosmos vector search ve change feed | 24, 29, 30, 45 | | [README](#22-cosmos-db-ile-vector-search-ve-change-feed) · [quiz](#-bölüm-22-quiz-soruları) |
| C2.6 PostgreSQL ve pgvector | 5, 21, 34, 47 | | [README](#23-azure-database-for-postgresql-ve-pgvector) · [quiz](#-bölüm-23-quiz-soruları) |
| C2.7 Azure Managed Redis | 12, 31, 38, 50 | | [README](#24-azure-managed-redis-cache-ve-vector-index) · [quiz](#-bölüm-24-quiz-soruları) |
| C2.8 Azure Functions | 3, 17, 26, 43 | | [README](#31-azure-functions-serverless-api-trigger-ve-binding) · [quiz](#-bölüm-31-quiz-soruları) |
| C2.9 Service Bus | 7, 22, 35, 46 | | [README](#32-azure-service-bus-kuyruk-topic-ve-dead-letter) · [quiz](#-bölüm-32-quiz-soruları) |
| C2.10 Event Grid | 32, 39, 49 | | [README](#33-azure-event-grid-custom-event-filtre-ve-retry) · [quiz](#-bölüm-33-quiz-soruları) |
| C2.11 Managed identity ve Key Vault | 4, 15, 28, 44 | | [README](#41-managed-identity-ve-azure-key-vault) · [quiz](#-bölüm-41-quiz-soruları) |
| C2.12 App Configuration | 8, 20, 36 | | [README](#42-azure-app-configuration) · [quiz](#-bölüm-42-quiz-soruları) |
| C2.13 OpenTelemetry ve KQL | 11, 25, 40, 48 | | [README](#43-opentelemetry-ile-izleme-ve-kql-ile-sorun-giderme) · [quiz](#-bölüm-43-quiz-soruları) |

**Tekrar planı:** Her yanlış için *bilgi eksiği* mi *dikkat hatası* mı olduğunu not et (kod/CLI/KQL tamamlamada: sözdizimini mi bilmiyordun, yoksa kavramı mı karıştırdın?). Bir konuda **2 veya daha fazla** bilgi eksiği varsa o konunun README'sini ve laboratuvarını baştan yap, ardından konu quiz'ini ≥ %80 olana kadar çöz. Vaka sorularında yanlışın nedeni çoğu zaman bir **gereksinimi kaçırmak**tır — [5.1 Sınav Stratejisi](#33-vaka-çalışması-ve-kod--cli--kql-tamamlama-soruları)'teki gereksinim listesi tekniğini tekrar oku.

---

## 📚 Resmî Kaynaklar ve Sınav Taktikleri

### Resmî Microsoft Kaynakları

- [AI-200 Study Guide (Skills Measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Azure Architecture Center](https://learn.microsoft.com/en-us/azure/architecture/)
- [Azure SDK for .NET Dokümantasyonu](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/)
- [Microsoft Learn AI-200 Öğrenme Yolu](https://learn.microsoft.com/en-us/training/courses/ai-200t00)
- [Azure Pricing Calculator](https://azure.microsoft.com/pricing/calculator/)

### Sınavda En Çok Hayat Kurtaran Kurallar

1. **Partition Key Seçimi:** Yüksek kardinalite, homojen veri ve istek dağılımı. `id` veya `tenantId_userId` gibi bileşik anahtarlar hot partition'ı engeller.
2. **Change Feed:** Change feed **yalnızca eklemeleri ve güncellemeleri (INSERT & UPDATE)** yakalar; fiziksel silmeleri (DELETE) yakalamaz (soft delete deseni gerekir).
3. **Vector Search İndeksleri:** HNSW (yüksek bellek, çok hızlı arama, grafik tabanlı) vs IVFFlat (düşük bellek, invert index, daha yavaş build). Cosine distance en yaygın ölçüdür.
4. **Managed Identity Rolleri:** Bir servisin Key Vault secret'larını okuması için RBAC'ta **Key Vault Secrets User** gerekir (**Contributor/Reader YETMEZ**).
5. **Service Bus vs Event Grid:** Service Bus = **Mesajlaşma** (pull, sırayla işleme, transactions, dead-letter). Event Grid = **Olaylar** (reaktif, push, yüksek hacimli bildirim).
6. **KEDA Scaling:** CPU/bellek yerine **harici olaylara** (kuyruktaki mesaj sayısı, veritabanı satırı) göre 0'a kadar ölçekler (scale to zero).
7. **Application Insights & KQL:** İstekler için `requests` / `AppRequests`, bağımlılıklar için `dependencies` / `AppDependencies`, hatalar için `exceptions` / `AppExceptions`. `summarize count() by bin(timestamp, 1h)` en sık kullanılan kalıptır.
8. **Container Apps Ingress:** Dışarıdan erişim için `external: true`, mikroservisler arası özel iletişim için `external: false` (yalnızca container apps environment içinden).
