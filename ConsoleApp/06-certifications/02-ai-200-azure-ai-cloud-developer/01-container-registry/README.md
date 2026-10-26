# C2.1 Azure Container Registry ve ACR Tasks

> 📁 `ConsoleApp/06-certifications/02-ai-200-azure-ai-cloud-developer/01-container-registry/` · Sınav alanı: **Develop containerized solutions on Azure (%20-25)** · Önkoşul: Docker temelleri (→ M14.1–M14.2), C1.5 (compute seçenekleri) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of October 7, 2026):** *Develop containerized solutions* — Azure Container Registry ile container image'larını **oluştur, sakla, sürümle ve yönet** · **ACR Tasks** ile image oluştur ve çalıştır. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)'ı kontrol et.

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

Bu konu, AI-200'ün *Develop containerized solutions* alanının temelidir: sonraki iki konu ([C2.2](../02-container-apps/README.md), [C2.3](../03-aks-manifests/README.md)) burada üretilen image'ı çalıştırır.

---

## 2. Neden?

- **Sınav:** SKU ↔ özellik eşleştirmesi, `az acr build` komutu, tag/digest farkı, ACR Tasks tetikleyicileri ve "servis image çekemiyor" senaryolarında doğru rol (`AcrPull`) neredeyse her sınavda vardır; CLI tamamlama sorularına çok uygundur.
- **Gerçek işte:** Public registry'ye (Docker Hub) bağımlılık anonim pull rate limit'ine takılır, üstelik kontrolün dışındadır (→ M14.2). Private registry; ağ izolasyonu (private endpoint), kimlik tabanlı erişim, geo-replication ve güvenlik taraması için gerekir.
- **Tedarik zinciri güvenliği:** `:latest` ile deploy etmek "hangi kod çalışıyor?" sorusunu cevapsız bırakır; digest ile sabitleme ve base image yama zinciri (ACR Tasks) bu sorunu çözer.
- **Geliştirici deneyimi:** CI makinesinde veya Docker'sız bir dev ortamında `az acr build` tek komutla image üretir.

---

## 3. Nasıl?

### 3.1 SKU'lar ⭐

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

### 3.2 Tag stratejisi ve digest ⭐

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

### 3.3 Kimlik ve yetkilendirme ⭐

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

### 3.4 ACR Tasks ⭐

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

### 3.5 Bakım ve maliyet

| Araç | Ne yapar | Not |
|---|---|---|
| `az acr repository delete --image …` | Tag/manifest siler | Digest ile silmek tüm tag'leri götürür |
| **`acr purge`** (`az acr run --cmd`) | `--filter`, `--ago`, `--keep`, `--untagged`, `--dry-run` ile toplu temizlik | Timer task'a bağlanıp otomatikleştirilir; OCI manifest'lerini de temizler |
| **Retention policy** (Premium, preview) | Untagged manifest'i N gün sonra siler (0–365; varsayılan 7) | `az acr config retention update --status Enabled --days 30 --type UntaggedManifests`; OCI medya tipli manifest'lerde çalışmaz |
| Tag kilidi (`--write-enabled false`) | Release tag'ini korur | Purge de kilitli tag'i silemez |

Untagged manifest, bir tag'in başka bir image'a taşınmasıyla "yetim" kalır, ama **depolama ücreti** doğurmaya devam eder. Temizlik yapılmazsa registry şişer.

### 3.6 Lab — Todo API'yi bulutta derle, sürümle, temizle

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

## 4. Gerçek dünya senaryosu

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

## 5. Sınav perspektifi

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

## 6. Yaygın hatalar

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

## 7. Performans & trade-off

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

## 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Azure Container Registry service tiers](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-skus) · [Fiyatlandırma](https://azure.microsoft.com/pricing/details/container-registry/)
- [ACR Tasks overview](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-tasks-overview) · [Multi-step tasks](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-tasks-multi-step) · [Base image updates](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-tasks-base-images)
- [Authenticate with ACR](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-authentication) · [ABAC repository permissions](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-rbac-abac-repository-permissions)
- [Retention policy for untagged manifests](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-retention-policy) · [Automatically purge images (`acr purge`)](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-auto-purge)
- [Content trust deprecation](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-content-trust-deprecation)
- [`az acr` CLI reference](https://learn.microsoft.com/en-us/cli/azure/acr) · [azure-containerregistry (Python)](https://pypi.org/project/azure-containerregistry/)
