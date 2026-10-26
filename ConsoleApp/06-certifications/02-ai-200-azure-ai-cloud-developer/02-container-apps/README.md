# C2.2 Azure Container Apps: Ortam, Revision ve KEDA

> 📁 `ConsoleApp/06-certifications/02-ai-200-azure-ai-cloud-developer/02-container-apps/` · Sınav alanı: **Develop containerized solutions on Azure (%20-25)** · Önkoşul: [C2.1](../01-container-registry/README.md) (ACR, digest, `AcrPull`), Service Bus temelleri (→ M12.3) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of October 7, 2026):** *Implement container-orchestrated solutions* — uygulamaları **Azure Container Apps**'e dağıt (ortam yapılandırması ve **revision yönetimi** dahil) · Container Apps'te **KEDA** ile olay güdümlü ölçekleme. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)'ı kontrol et.

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

## 2. Neden?

- **Sınav:** *Implement container-orchestrated solutions* hedefi tam olarak buradadır: revision/trafik senaryoları, KEDA kural seçimi ve `az containerapp` komutları AI-200'ün sevdiği "senaryo + CLI tamamlama" kalıbıdır.
- **Gerçek işte:** AKS'in operasyon yükü (node, upgrade, ağ eklentileri) olmadan, event-driven mikroservisleri **sıfıra kadar ölçekleyerek** ve kullandığın kadar ödeyerek çalıştırırsın. Canary ve geri alma, revision + trafik bölme ile kod yazmadan yapılır.
- **Maliyet:** Boştaki worker'ın sıfır replica'ya inmesi (**scale-to-zero**) ve tüketim bazlı faturalama gece/hafta sonu yükünü neredeyse sıfırlar.

---

## 3. Nasıl?

### 3.1 Environment ⭐

- **Environment**, uygulamaların aynı sanal ağ ve log hedefini paylaştığı sınırdır; uygulamalar aynı environment içinde **iç DNS** ile birbirine ulaşır. Varsayılan olarak tüm uygulamaların konsol/sistem logları ortak bir **Log Analytics** çalışma alanına gider.
- **Environment türleri:**

| Tür | Plan | Not |
|---|---|---|
| **Workload profiles** (yeni ortamlarda varsayılan) | **Consumption** (tüketim bazlı) **ve Dedicated** (ayrılmış VM profilleri, GPU dahil) | UDR, daha büyük kaynak boyutları; Dedicated'da ortam için sabit yönetim ücreti vardır |
| **Consumption-only** (legacy) | Yalnızca Consumption | Uygulama başına sınırlı CPU/bellek tavanı (yaklaşık 4 vCPU / 8 GiB); ortamın kendisi ücretsiz, sadece uygulamalar faturalanır |

- Yeni ortam oluştururken workload profiles kullan; sınavda "GPU / büyük kaynak / dedicated" geçiyorsa **Dedicated workload profile** cevaptır.
- **Container Apps Jobs** *(bağlam)*: tetikleyici `Schedule` (cron), `Event` (KEDA scaler'ı ile, kuyruğa mesaj gelince) veya `Manual`. Sürekli çalışan servis değil, bitip kapanan iş içindir.
- **Dapr** *(bağlam)*: istenirse sidecar olarak etkinleştirilir (service invocation, pub/sub, state); sınavın ana odağı değildir.

### 3.2 Uygulama yapılandırması

| Alan | Ne? | Not |
|---|---|---|
| **Ingress** | `external` (internetten) veya `internal` (yalnızca environment/VNet); **target port** = container'ın dinlediği port | Port uyuşmazlığı = 502/timeout; ingress yoksa uygulama yalnızca dahili çağrı/worker'dır |
| **Env var** | Düz değer veya `secretref:` ile secret'a referans | Env var değişikliği **yeni revision** üretir |
| **Secret** | Uygulama düzeyinde saklanan değerler; **Key Vault referansı** + managed identity ile bağlanabilir (→ [C2.11](../11-identity-key-vault/README.md)) | Secret değişikliği **yeni revision oluşturmaz**; çalışan revision'ın yeni değeri alması için restart gerekir |
| **Registry kimliği** | `--registry-server` + `--registry-identity` (managed identity) → `AcrPull` | Admin parolası yerine **kimlik tabanlı çekme** |
| **Managed identity** | System- veya user-assigned | Registry, Service Bus, Key Vault, KEDA kimlik doğrulaması için |

> **Registry kimliği ve ilk dağıtım:** system-assigned identity uygulama **oluşturulduktan sonra** doğar; ama image çekmek için identity daha ilk revision'da gerekir. Bu yüzden pratikte **user-assigned identity** (önceden oluşturulup `AcrPull` verilmiş) kullanılır.

### 3.3 Revision yönetimi ⭐

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

### 3.4 KEDA ile ölçekleme ⭐

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

### 3.5 Lab — Digest ile dağıt, %10 canary, geri al, KEDA ile ölçekle

Maliyet: Consumption planında kullanılan kadar; Service Bus **Basic/Standard** namespace kuruşluk; sıfıra inen worker neredeyse ücretsizdir. Bütçe alarmı kur (→ C1.1). [C2.1](../01-container-registry/README.md) labındaki `$ACR` ve digest'i kullan (aksi halde `az acr import` ile örnek bir image al).

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

> Gönderen kimliğe *Azure Service Bus Data Sender* rolü gerekir. Mesaj işleme (`ServiceBusProcessor`, DLQ) → [C2.9](../09-service-bus/README.md).

> **Alıştırma.** `messageCount=20`, `maxReplicas=10`, kuyrukta 500 mesaj var. Kaç replica beklenir? Kuyruk boşalınca ne olur ve ne kadar sürede?
>
> <details><summary>Çözüm</summary>
>
> İstenen replica ≈ ceil(500 / 20) = 25 → **max 10** ile sınırlanır; 10 replica çalışır. Kuyruk boşalınca kural 0 replica ister; ancak **cooldown (varsayılan 300 sn)** dolana kadar son replica kapanmaz, sonra **0'a iner**. Daha hızlı boşaltmak için `maxReplicas` veya replica başına işleme hızı artırılır; sıfıra inmeyi hızlandırmak için cooldown (`cooldownPeriod`) kısaltılır.
> </details>

### 3.6 Log ve teşhis

- `az containerapp logs show -n APP -g RG --type console|system [--follow]` canlı log; portalda **Log stream**.
- Log Analytics'te iki tablo: **`ContainerAppConsoleLogs_CL`** (stdout/stderr; kolonlar `ContainerAppName_s`, `RevisionName_s`, `ContainerGroupName_g`, `Log_s`) ve **`ContainerAppSystemLogs_CL`** (platform olayları: image çekme, probe, restart). Kolon adları kayıt şemasına göre değişebilir; sorgudan önce `getschema` ile doğrula.

```kusto
ContainerAppConsoleLogs_CL
| where ContainerAppName_s == "todoapi" and TimeGenerated > ago(30m)
| where Log_s contains "error"
| project TimeGenerated, RevisionName_s, Log_s
| order by TimeGenerated desc
```

Container Apps / AKS / App Service karşılaştırması ve AKS teşhisi → [C2.3](../03-aks-manifests/README.md).

---

## 4. Gerçek dünya senaryosu

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

## 5. Sınav perspektifi

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

## 6. Yaygın hatalar

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

## 7. Performans & trade-off

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

## 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Azure Container Apps overview](https://learn.microsoft.com/en-us/azure/container-apps/overview) · [Environments](https://learn.microsoft.com/en-us/azure/container-apps/environment) · [Workload profiles](https://learn.microsoft.com/en-us/azure/container-apps/workload-profiles-overview)
- [Revisions](https://learn.microsoft.com/en-us/azure/container-apps/revisions) · [Traffic splitting](https://learn.microsoft.com/en-us/azure/container-apps/traffic-splitting) · [Revision labels](https://learn.microsoft.com/en-us/azure/container-apps/labels)
- [Set scaling rules](https://learn.microsoft.com/en-us/azure/container-apps/scale-app) · [KEDA Azure Service Bus scaler](https://keda.sh/docs/latest/scalers/azure-service-bus/)
- [Managed identities in Container Apps](https://learn.microsoft.com/en-us/azure/container-apps/managed-identity) · [Pull images with managed identity](https://learn.microsoft.com/en-us/azure/container-apps/managed-identity-image-pull)
- [Monitor logs with Log Analytics](https://learn.microsoft.com/en-us/azure/container-apps/log-monitoring) · [Jobs](https://learn.microsoft.com/en-us/azure/container-apps/jobs)
- [`az containerapp` CLI reference](https://learn.microsoft.com/en-us/cli/azure/containerapp)
