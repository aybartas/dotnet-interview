# C1.21 Entegrasyon, Mesajlaşma ve DevOps Araçları

> 📁 `ConsoleApp/06-certifications/01-az-900-azure-fundamentals/21-integration-devops-tools/` · Sınav alanı: **Describe Azure architecture and services — genişletilmiş kapsam (bağlam)** · Önkoşul: [C1.6](../06-app-hosting/README.md), [C1.13](../13-management-deployment-tools/README.md) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Bağlantı:** C1.6'daki Functions/Logic Apps sorularının devamı; **C2.8–C2.10** (Functions, Service Bus, Event Grid) ve M12 (mesajlaşma) için zemin; *Describe management and deployment tools* ile (→ C1.13) birlikte okunur.
>
> Bu konu resmî study guide'ın (July 20, 2026) doğrudan hedefi değildir; kurs müfredatlarında ve senaryo sorularının seçeneklerinde geçer.

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

Uygulamalar tek başına çalışmaz; birbirleriyle ve dış sistemlerle konuşur. Azure bunun için bir **entegrasyon** araç kutusu sunar; kodu üretime taşımak için de **DevOps** araçları vardır:

| Alan | Soru | Servisler |
|---|---|---|
| **İş akışı / otomasyon** | "A olunca B'yi yap" — kodsuz/az kodlu | **Logic Apps** (Functions ↔ Logic Apps → C1.6) |
| **Olay yönlendirme** | "Bir şey **oldu**, ilgilenenleri haberdar et" | **Event Grid** |
| **Kurumsal mesajlaşma** | "Bu işi **güvenle** teslim et, kaybetme, sıraya koy" | **Service Bus** (kuyruk/topic) |
| **Olay/telemetri akışı** | "Saniyede milyonlarca olayı al" | **Event Hubs** |
| **Basit kuyruk** | "Ucuz, basit bir kuyruk yeter" | **Storage Queue** |
| **API yönetimi** | "API'lerimi güvenli ve yönetilebilir yayınla" | **API Management** |
| **DevOps** | "Kodu planla, derle, test et, yayınla" | **Azure DevOps** ↔ **GitHub**, **Pipelines/Actions**, **DevTest Labs**, **Deployment Environments**, **azd** |

Bu konunun C# kodu yoktur. Pratik: `az` CLI ile bir Service Bus kuyruğu, REST ile mesaj gönder/al (3.6) ve bir GitHub Actions workflow'unu okuma alıştırması (3.5).

---

## 2. Neden?

- **Sınav/kurs:** "Olay mı, mesaj mı?" ve "Event Grid / Service Bus / Event Hubs hangisi?" sorusu kurs sınavlarının klasiğidir; seçeneklerde (distractor) AZ-900 senaryo sorularında da görünür. API Management ve Logic Apps "ürün adı → görev" eşleştirme sorularında çıkar.
- **Gerçek işte:** Dağıtık sistemlerde en pahalı hata **yanlış mesajlaşma aracı seçmektir**: sipariş gibi kaybolmaması gereken bir komutu Event Grid ile yollamak (teslimat garantisi ve sıra yok), ya da basit bir arka plan işi için Service Bus Premium ödemek.
- **Mülakat:** Mesajlaşma desenleri (kuyruk, pub/sub, outbox, idempotency) M12'nin konusudur; burada araç envanterini ve seçim mantığını öğrenirsin.
- **Güvenlik (→ C1.18):** Bu servislerde **bağlantı dizesi/SAS anahtarı yerine Entra ID + managed identity** tercih edilir; CI/CD'de kalıcı sır yerine **OIDC** kullanılır (3.5).

---

## 3. Nasıl?

### 3.1 Logic Apps ve entegrasyon iş akışları

**Azure Logic Apps**, **kod yazmadan/az kodla** görsel tasarımcıda iş akışı kuran, **1000'den fazla connector** (Microsoft 365, Salesforce, SAP, SQL, Service Bus, HTTP…) bulunan bir PaaS entegrasyon servisidir. Yapı: **trigger** (iş akışını başlatan olay) + **action**'lar (adımlar) + koşul/döngü.

| | Logic Apps | Azure Functions |
|---|---|---|
| Yaklaşım | Görsel tasarımcı, hazır connector'lar | Kod (C#, JS, Python…) |
| Güçlü yanı | SaaS/sistemler arası entegrasyon, hızlı kurulum, onay akışları | Özel iş mantığı, algoritma, tam kontrol |
| Faturalama (özet) | **Consumption:** çalışan eylem başına; **Standard:** ayrılmış kapasite | Consumption/Flex Consumption: yürütme + süre; Premium/Dedicated |
| Tipik örnek | "Yeni e-posta eki gelince Blob'a kaydet, Teams'e haber ver" | "Gelen siparişin vergisini hesapla" |

Birlikte kullanılırlar: Logic Apps iş akışı bir Function'ı eylem olarak çağırır.

### 3.2 Olay mı, mesaj mı? ⭐

Bu ayrım üç servisin seçimini belirler:

| | **Olay (event)** | **Mesaj (message)** |
|---|---|---|
| Anlamı | "**Bir şey oldu**" bildirimi (durum değişikliği, **gerçek**) | Bir sisteme verilen **veri/komut/iş** (ör. "bu siparişi işle") |
| Yayıncının beklentisi | Kim dinlerse dinlesin; sonucuyla ilgilenmez | Tüketicinin **işlemesini bekler** (teslim garantisi önemli) |
| İçerik | Genelde küçük: ne oldu + nerede | Asıl iş verisi (sipariş, ödeme) |
| Ana ihtiyaç | Reaktif yönlendirme, fan-out | Güvenilir teslim, sıra, tekrar deneme, dead-letter |
| Servis | **Event Grid** (ayrık olaylar), **Event Hubs** (olay **akışı**) | **Service Bus**, Storage Queue |

Mnemonik: **Event Grid = "bir şey oldu, haber ver" · Service Bus = "bu işi güvenle işle" · Event Hubs = "milyonlarca olayı al, akışı sakla/oku".**

### 3.3 Servisler ⭐

**Event Grid** — tam yönetilen **olay yönlendirme** (pub/sub, **push**): kaynak olayı yayınlar, abonelere HTTP/webhook, Function, Logic App, Service Bus, Event Hubs vb. olarak iletir.

- **Kaynaklar:** Azure servisleri (**system topic** — ör. blob oluşturuldu, kaynak değişti, Key Vault sırrı süreleniyor), kendi uygulamaların (**custom topic**), partner olayları.
- **Filtreleme** (olay türü/konu), **retry** ve dead-lettering; en az bir kez teslim (at-least-once). Olay başına/işlem başına ücretlendirilir; aylık küçük bir ücretsiz işlem kotası vardır *(doğrula)*.
- Ek: Event Grid *namespace*'leri **MQTT** ve **pull** teslimi de destekler *(bağlam; IoT/MQTT senaryoları → C1.20)*.
- Tipik: "Blob yüklenince thumbnail Function'ı çalıştır", "Resource group'ta VM oluşunca Logic App uyarı yollasın".

**Service Bus** — kurumsal **mesaj aracısı (broker)**.

| Kavram | Anlamı |
|---|---|
| **Queue** | Noktadan noktaya; mesajı **tek** tüketici işler |
| **Topic + subscription** | Yayınla/abone ol (pub/sub); her abonelik mesajın kendi kopyasını alır, **filtre** kuralları olabilir |
| **Dead-letter queue (DLQ)** | İşlenemeyen/süresi dolan/çok denenen mesajların park edildiği alt kuyruk |
| **Sessions** | Aynı oturumdaki mesajlar **sıralı (FIFO)** ve tek tüketicide |
| **Peek-lock** | Mesaj kilitlenir; işlenince `complete`, olmazsa tekrar kuyruğa döner (**at-least-once**) → tüketici **idempotent** olmalı (→ M12) |
| Diğer | Duplicate detection, scheduled delivery, transactions, AMQP/JMS |
| **Katmanlar** | **Basic** (yalnızca kuyruk), **Standard** (topic dahil), **Premium** (izole kapasite, büyük mesaj, private endpoint/VNet, geo-DR) — özellikler katmana bağlı |

**Event Hubs** — yüksek hacimli **olay/telemetri alımı** (big data streaming): **partition**'lar, **consumer group**'lar, **Capture** (Blob/Data Lake'e otomatik yazma), **Kafka uyumlu** uç nokta; saniyede milyonlarca olay. Mesaj/komut değil **akış**; olaylar saklama süresi boyunca **yeniden okunabilir**. IoT Hub ile farkı → C1.20 (3.3).

**Storage Queue** — Storage hesabındaki **basit, ucuz** kuyruk; REST/HTTP, çok büyük kuyruk kapasitesi, ama topic yok, **garanti edilen FIFO yok**, DLQ/session gibi gelişmiş özellikler yok; mesaj boyutu küçük (kabaca 64 KB — doğrula). "Yalnızca basit bir arka plan işi kuyruğu" için yeterlidir.

**API Management (APIM)** — arka uç API'lerinin önünde duran **API ağ geçidi**:

| Yetenek | Örnek |
|---|---|
| **Güvenlik/kimlik** | JWT doğrulama, subscription key, IP kısıtı, OAuth |
| **Politikalar** | Rate limit, **kota**, dönüşüm (XML↔JSON, header ekle/sil), cache |
| **Yayınlama** | **Geliştirici portalı**, API dokümantasyonu, sürümleme |
| **Gözlem** | Analitik, log, Monitor entegrasyonu |
| **Birleştirme** | Birden çok arka ucu (App Service, Function, on-prem) tek API yüzeyinde toplamak |

> APIM bir **mesajlaşma** servisi değildir; HTTP API'leri yönetir. WAF/Front Door gibi ağ güvenlik servisleri (→ C1.17/C1.18) ile birlikte kullanılabilir.

**Seçim tablosu — olay ↔ mesaj** ⭐ *(→ C2.9, C2.10, M12)*

| İhtiyaç | Servis |
|---|---|
| Bir Azure kaynağında olay olunca (blob oluştu) başka bir şeyi **tetikle** | **Event Grid** |
| Sipariş komutlarını **kaybetmeden, sırayla, tekrar denemeyle** işle; DLQ lazım | **Service Bus** (queue) |
| Bir yayını **birden çok sisteme** ilet, her biri kendi filtresi ile | **Service Bus topic** (güvenilir) veya Event Grid (hafif, olay) |
| Saniyede **yüz binlerce** telemetri/log olayı al, sonra analiz et, Kafka uyumu | **Event Hubs** |
| Ucuz, basit, çok büyük kuyruk; gelişmiş özellik gerekmez | **Storage Queue** |
| Kodsuz iş akışı: yeni e-posta → CRM kaydı → Teams bildirimi | **Logic Apps** |
| Dış partnerlere REST API sun, kota ve anahtar uygula | **API Management** |

### 3.4 Azure DevOps ↔ GitHub

| | **Azure DevOps** | **GitHub** |
|---|---|---|
| Planlama | **Boards** (iş öğeleri, backlog, sprint) | Issues, Projects |
| Kod deposu | **Repos** (Git) | Repositories (Git) — açık kaynak ekosistemin merkezi |
| CI/CD | **Pipelines** (YAML veya klasik) | **Actions** (YAML workflow'lar, marketplace) |
| Test | **Test Plans** (manuel/keşif testi) | Actions içinde otomatik test (ayrı manuel test aracı yok) |
| Paket | **Artifacts** (NuGet, npm, Maven…) | **Packages** |
| Geliştirici ortamı | — | **Codespaces** (bulutta geliştirme ortamı) |
| AI yardımcı | — | **GitHub Copilot** |
| Güvenlik | — | Advanced Security (kod tarama, secret scanning, Dependabot) |

> Her ikisi de Microsoft'a aittir; çoğu kurum ikisini de kullanır (ör. kod GitHub'da, iş takibi Boards'ta). Yeni projelerde GitHub ağırlıkla öne çıkar; sınav ve kurslar ikisini de **"DevOps araç seti"** olarak sorar.

**CI/CD kavramları:** **CI** (continuous integration) — her commit'te derle + test et; **CD** (continuous delivery/deployment) — doğrulanmış çıktıyı ortamlara (dev → test → prod) otomatik/onaylı yayınla. Çıktı bir **artifact**'tır (derlenmiş paket/container image).

**Ek servisler:**

| Servis | Rol |
|---|---|
| **Azure DevTest Labs** | Geliştirme/test ortamlarını **maliyet kontrolüyle** kurma: otomatik kapatma/başlatma, VM sayısı/boyutu politikaları, harcama eşiği, hazır **formül/şablonlar**, "claimable" VM'ler |
| **Azure Deployment Environments** | Platform ekibi onaylı **IaC şablonları** (katalog) sunar; geliştirici tek tıkla standartlara uygun ortam alır (self-service) |
| **Azure Developer CLI (`azd`)** *(bağlam)* | `azd init / up / down` ile **uygulama + altyapıyı** (Bicep) tek komutla sağlama ve dağıtma; CI/CD yapılandırması da üretir |
| **IaC (→ C1.13)** | ARM/Bicep/Terraform: ortam kod olarak; pipeline'ın dağıtım adımı |

### 3.5 CI/CD akışı ve OIDC (workload identity federation) *(bağlam)*

Pipeline'ın Azure'a dağıtım yapabilmesi için bir kimliğe ihtiyacı vardır. **Kötü yol:** service principal **secret'ını** depoya/GitHub secret'ına koymak — süresi dolar, sızabilir, rotasyon gerektirir. **İyi yol:** **workload identity federation (OIDC)**: Entra ID'de bir uygulama kaydına/managed identity'ye *federated credential* tanımlanır ("şu repo, şu branch/environment'tan gelen GitHub token'ına güven"); workflow kısa ömürlü bir token takas eder, **kalıcı sır yoktur**.

**GitHub Actions workflow — oku ve yorumla** (çalıştırma yok; kimlik değerleri secret değil *yapılandırma*dır):

```yaml
name: ci-cd

on:
  push:
    branches: [ main ]
  workflow_dispatch:

permissions:
  id-token: write      # allows the job to request an OIDC token
  contents: read

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: 10.0.x
      - run: dotnet build --configuration Release
      - run: dotnet test --configuration Release --no-build
      - run: dotnet publish --configuration Release --output ./publish
      - uses: actions/upload-artifact@v4
        with:
          name: webapp
          path: ./publish

  deploy:
    needs: build
    runs-on: ubuntu-latest
    environment: production        # approval rules can be attached to the environment
    steps:
      - uses: actions/download-artifact@v4
        with:
          name: webapp
          path: ./publish
      - uses: azure/login@v2        # exchanges the GitHub OIDC token for an Azure token (no password)
        with:
          client-id: ${{ secrets.AZURE_CLIENT_ID }}
          tenant-id: ${{ secrets.AZURE_TENANT_ID }}
          subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
      - uses: azure/webapps-deploy@v3
        with:
          app-name: app-az900-demo
          package: ./publish
```

> **Alıştırma.** (1) Hangi satır CI, hangisi CD'dir? (2) `needs: build` ne sağlar? (3) `permissions: id-token: write` neden gerekli? (4) Depoda hangi kalıcı **sır** yoktur? (5) Üretime onay kuralı nereye eklenir?
>
> <details><summary>Çözüm</summary>
>
> 1. `build` job'u **CI** (derle, test et, paketle); `deploy` job'u **CD** (yayınla). 2. `deploy`, `build` başarılı olmadan çalışmaz — sıra ve bağımlılık. 3. Job'un GitHub'dan OIDC token isteyebilmesi için; `azure/login` bu token'ı Azure token'ıyla takas eder. 4. Parola/secret yok — yalnızca kimlik tanımlayıcıları (client/tenant/subscription id) var; güven Entra'daki federated credential'dadır. 5. GitHub'da `production` **environment**'ına *required reviewers* kuralı ve federated credential'ın subject'ini o environment'a daraltma.
> </details>

### 3.6 Pratik — Service Bus kuyruğu, REST ile gönder/al (`az` CLI)

> **Maliyet:** *Basic* katman çok düşük (işlem başına) ücretlidir; kuyruk dışında kaynak yoktur. Bütçe alarmın (→ [C1.11](../11-cost-management/README.md)) kurulu olsun, işin sonunda grubu sil. `az servicebus` komutları **yönetim düzlemini** (namespace/kuyruk oluşturma) yapar; **mesaj gönder/al** işini CLI doğrudan yapmaz — burada **Entra token + REST** ile yapıyoruz (SAS anahtarı kullanmadan).

```bash
RG=rg-az900-c121
LOC=westeurope
NS=sbns-az900-c121-$RANDOM
Q=orders
az group create --name $RG --location $LOC

# 1) Namespace (Basic = queues only; topics need Standard) and a queue
az servicebus namespace create --resource-group $RG --name $NS --location $LOC --sku Basic
az servicebus queue create --resource-group $RG --namespace-name $NS --name $Q

# 2) Data-plane role for yourself (Entra ID instead of a shared key). Propagation can take a few minutes.
NS_ID=$(az servicebus namespace show --resource-group $RG --name $NS --query id --output tsv)
ME=$(az ad signed-in-user show --query id --output tsv)
az role assignment create --assignee-object-id "$ME" --assignee-principal-type User \
  --role "Azure Service Bus Data Owner" --scope "$NS_ID"

# 3) Token for the Service Bus data plane
TOKEN=$(az account get-access-token --resource https://servicebus.azure.net --query accessToken --output tsv)

# 4) Send two messages (REST: POST <queue>/messages)
for N in 1 2; do
  curl -s -o /dev/null -w "send $N -> HTTP %{http_code}\n" -X POST \
    "https://$NS.servicebus.windows.net/$Q/messages" \
    -H "Authorization: Bearer $TOKEN" -H "Content-Type: text/plain" \
    --data "order-$N created"
done

# 5) Look at the counters (active messages = 2)
az servicebus queue show --resource-group $RG --namespace-name $NS --name $Q --query countDetails

# 6) Receive-and-delete (REST: DELETE <queue>/messages/head) - twice; the third call returns HTTP 204 (empty)
for N in 1 2 3; do
  curl -s -w "\n-> HTTP %{http_code}\n" -X DELETE \
    "https://$NS.servicebus.windows.net/$Q/messages/head?timeout=10" \
    -H "Authorization: Bearer $TOKEN"
done

# 7) Cleanup
az group delete --name $RG --yes --no-wait
```

Beklenen gözlem: Gönderimler **201**; `countDetails.activeMessageCount` = 2; ilk iki `DELETE` mesaj gövdesini (`order-1 created`, `order-2 created`) döndürür ve mesaj **silinir** (destructive read), üçüncü çağrı **204** (kuyruk boş). 401/403 alırsan rol atamasının yayılmasını birkaç dakika bekle ve token'ı yenile. İstersen portalda *Service Bus Explorer* ile aynı mesajları **peek-lock** ile okuyup `complete`/`dead-letter` deneyebilirsin. *(REST yolları ve başlıklar API sürümüne göre değişebilir; güncel başvuru Learn'de.)*

**Karar tablosu alıştırması:** Her senaryo için servisi seç → **Event Grid · Service Bus · Event Hubs · Storage Queue · Logic Apps · API Management**.

| # | Senaryo |
|---|---|
| 1 | Ödeme komutları sırayla, tekrar denemeli ve dead-letter'lı işlenecek |
| 2 | Bir blob yüklendiğinde 3 farklı sistem haberdar edilecek (hafif olay) |
| 3 | Mobil uygulamalardan saniyede 500.000 kullanım olayı alınıp analiz hattına akıtılacak |
| 4 | Basit bir arka plan işi için en ucuz kuyruk yeterli |
| 5 | Yeni müşteri formu → CRM + e-posta + Teams bildirimi, kodsuz |
| 6 | Partner firmalara anahtar ve günlük kota ile REST API açılacak |

<details><summary>Çözüm</summary>

1 → **Service Bus** (queue) · 2 → **Event Grid** · 3 → **Event Hubs** · 4 → **Storage Queue** · 5 → **Logic Apps** · 6 → **API Management**.
</details>

---

## 4. Gerçek dünya senaryosu

Bir perakende şirketi, mağaza ve web siparişlerini işleyen sistemini modernleştiriyor.

1. **Sipariş alma:** Web ve mobil istemciler REST API'ye gelir; API'ler **API Management** arkasındadır (JWT doğrulama, rate limit, dev portalı). Sipariş **Service Bus topic**'ine yazılır — komut kaybolmamalıdır; **stok**, **ödeme**, **kargo** servisleri ayrı abonelikle işler. İşlenemeyen sipariş **dead-letter**'a düşer, operasyon ekibi inceler.
2. **Olaylar:** Fatura PDF'i Blob'a yüklendiğinde **Event Grid** bir Function'ı tetikler (OCR/arşiv) ve bir Logic App'e haber verir — hafif, reaktif.
3. **Telemetri:** Mağaza kasalarının ve web sitesinin kullanım olayları **Event Hubs**'a akar; **Capture** ham veriyi Data Lake'e yazar; Stream Analytics anlık pano besler (→ C1.16, C1.20).
4. **İş akışı:** "Büyük kurumsal sipariş → satış müdürü onayı → CRM kaydı → Teams bildirimi" **Logic Apps** ile az kodla kurulur; özel fiyat hesabı ise bir **Azure Function**'dır.
5. **DevOps:** Kod **GitHub**'da, iş takibi **Azure Boards**'ta. **GitHub Actions** workflow'u derler, test eder, paketi **OIDC** ile (kalıcı sır olmadan) staging'e dağıtır; production için **environment onayı** gerekir. Altyapı Bicep ile kod olarak tutulur (→ C1.13).
6. **Maliyet:** Geliştirici test ortamları **DevTest Labs**'te: gece otomatik kapanma ve VM sayısı politikası; platform ekibi **Deployment Environments** ile standart ortam şablonları sunar.
7. **Ders:** İlk tasarımda siparişler **Event Grid** ile akıyordu; bir tüketici kapalıyken olaylar tükenip kaybolduğunda (retry süresi doldu) siparişler düştü. Komut/iş verisi **Service Bus**'a taşındı; Event Grid yalnızca "olay oldu" bildirimlerinde kaldı.

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → servis eşleştirmesi (olay/mesaj/akış), servis → görev eşleştirmesi (APIM, Logic Apps), DevOps araç eşleştirmesi (Boards, Repos, Pipelines, Test Plans, Artifacts; Actions, Codespaces, Copilot).

| Soru kalıbı | Doğru cevap | Tuzak |
|---|---|---|
| "Olay oldu, **abonelere ilet** (reaktif, hafif)" | **Event Grid** | Service Bus |
| "**Kurumsal mesajlaşma**, kuyruk/topic, dead-letter, sıra, transaction" | **Service Bus** | Storage Queue |
| "Saniyede **milyonlarca** olay/telemetri, **Kafka** uyumu" | **Event Hubs** | Service Bus |
| "Basit ve **ucuz** kuyruk" | **Storage Queue** | Service Bus |
| "Kodsuz iş akışı, **1000+ connector**" | **Logic Apps** | Functions |
| "**Kod** ile olay tetiklemeli çalıştır" | **Azure Functions** | Logic Apps |
| "API'lere kota, anahtar, dönüşüm, **geliştirici portalı**" | **API Management** | Application Gateway |
| "Backlog, sprint, iş öğesi" | **Azure Boards** | Repos |
| "Git deposu" | **Azure Repos / GitHub** | Artifacts |
| "CI/CD YAML pipeline" | **Azure Pipelines / GitHub Actions** | Test Plans |
| "Manuel/keşif test planı" | **Azure Test Plans** | Pipelines |
| "NuGet/npm paket akışı" | **Azure Artifacts** | Repos |
| "Bulutta geliştirme ortamı (tarayıcıdan VS Code)" | **GitHub Codespaces** | DevTest Labs |
| "Dev/test VM'lerini otomatik kapatıp **maliyeti kontrol et**" | **Azure DevTest Labs** | Azure Policy |
| "Kod önerisi / yapay zekâ eşlikçisi" | **GitHub Copilot** | Advisor |
| "Pipeline'a **parolasız** Azure erişimi" | **OIDC / workload identity federation** | Service principal secret |

**Anahtar kelimeler:** *react to events, publish-subscribe, push* → Event Grid · *enterprise messaging, dead letter, sessions, FIFO* → Service Bus · *big data streaming, partitions, capture, Kafka* → Event Hubs · *simple queue, lowest cost* → Storage Queue · *designer, connectors, workflow* → Logic Apps · *gateway, policies, developer portal* → API Management.

**Evet/hayır tuzakları:**
- "Event Grid mesajları sıralı ve garantili FIFO iletir" → **Hayır** (Service Bus sessions).
- "Event Hubs, kuyruktur; mesaj tüketilince silinir" → **Hayır** (akış; saklama süresi boyunca yeniden okunur).
- "Service Bus Basic katmanı topic destekler" → **Hayır** (Standard ve üstü).
- "Azure DevOps ve GitHub aynı şirketin ürünleridir" → **Evet** (Microsoft).
- "API Management bir mesaj kuyruğudur" → **Hayır** (HTTP API ağ geçidi).

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Kritik komutu (sipariş) Event Grid ile yollamak | Komut/iş verisi → Service Bus; Event Grid "bir şey oldu" bildirimi içindir |
| Event Hubs'ı kuyruk gibi kullanmak | Akış servisidir; tüketici offset'i kendi tutar, mesaj "tüketilince silinmez" |
| Mesajın tek kez işleneceğini varsaymak | At-least-once: tüketiciyi **idempotent** yaz (→ M12) |
| Service Bus'ta DLQ'yu izlememek | Dead-letter birikir ve sessizce veri kaybı olur; izle ve uyarı kur |
| Logic Apps'e karmaşık algoritma gömmek / Functions'ta basit connector işi yazmak | İş akışı ve entegrasyon → Logic Apps; özel mantık → Functions |
| Bağlantı dizesi/SAS anahtarını koda ve depoya koymak | Entra ID + managed identity, RBAC rolleri (Service Bus Data Sender/Receiver); anahtar gerekiyorsa Key Vault |
| CI/CD'ye uzun ömürlü service principal secret'ı koymak | **OIDC / workload identity federation**; federated credential'ı repo/branch/environment ile daralt |
| "Azure DevOps eskidi, GitHub kullan" ya da tersi dogması | İkisi de destekli ve entegre; karar ekip ve ihtiyaçla |
| API Management'ı ucuz sanmak | Katmana göre sabit ücret/kapasite; basit tek API için fazla olabilir |
| Deneme namespace'ini/hub'ını açık unutmak | Premium/Dedicated katmanlar sabit ücretlidir; grubu sil |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Event Grid (olay) | Hafif, reaktif, çok sayıda aboneye fan-out; sunucusuz maliyet modeli | Sıra yok, kısıtlı retry penceresi, mesaj aracısı özellikleri yok |
| Service Bus Standard/Premium | Güvenilir teslim, DLQ, session, transaction, topic | Basic/Storage Queue'dan pahalı; kavram yükü; Premium sabit kapasite ücreti |
| Event Hubs | Çok yüksek throughput, yeniden okunabilir akış, Kafka uyumu | Mesaj broker'ı değildir; partition ve consumer yönetimi |
| Storage Queue | En ucuz/basit, büyük kapasite | Gelişmiş özellik yok (FIFO garantisi, topic, DLQ) |
| Logic Apps | Hızlı kurulum, bakım kolaylığı (kod yok) | Karmaşık mantıkta tasarımcı hantallaşır; tüketim başına eylem ücreti büyüyebilir |
| API Management | Merkezî güvenlik, politika, gözlem | Ek ağ sıçraması (gecikme), katman maliyeti, yönetim yükü |
| GitHub Actions / Azure Pipelines (hosted agent) | Altyapı yok, hızlı başlangıç | Dakika kotası/ücreti; özel ağ erişimi için self-hosted agent gerekir |
| OIDC federasyonu | Kalıcı sır yok, rotasyon yok | Entra'da başlangıç yapılandırması; subject eşleşmesi hatalarına dikkat |
| DevTest Labs / otomatik kapatma | Dev/test maliyeti düşer | Geliştiriciler için kapanma/başlama bekleme süresi |

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Choose between Azure messaging services — Event Grid, Event Hubs, and Service Bus](https://learn.microsoft.com/en-us/azure/service-bus-messaging/compare-messaging-services)
- [What is Azure Logic Apps?](https://learn.microsoft.com/en-us/azure/logic-apps/logic-apps-overview) · [What is Azure Event Grid?](https://learn.microsoft.com/en-us/azure/event-grid/overview) · [What is Azure Service Bus?](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview) · [Azure Event Hubs](https://learn.microsoft.com/en-us/azure/event-hubs/event-hubs-about) · [Azure Queue Storage](https://learn.microsoft.com/en-us/azure/storage/queues/storage-queues-introduction)
- [Service Bus REST — send / receive-and-delete](https://learn.microsoft.com/en-us/rest/api/servicebus/) · [`az servicebus`](https://learn.microsoft.com/en-us/cli/azure/servicebus)
- [About API Management](https://learn.microsoft.com/en-us/azure/api-management/api-management-key-concepts)
- [What is Azure DevOps?](https://learn.microsoft.com/en-us/azure/devops/user-guide/what-is-azure-devops) · [GitHub Actions documentation](https://docs.github.com/en/actions) · [Azure DevTest Labs](https://learn.microsoft.com/en-us/azure/devtest-labs/devtest-lab-overview) · [Azure Deployment Environments](https://learn.microsoft.com/en-us/azure/deployment-environments/overview-what-is-azure-deployment-environments) · [Azure Developer CLI](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/overview)
- [Use GitHub Actions to connect to Azure (OIDC)](https://learn.microsoft.com/en-us/azure/developer/github/connect-from-azure-openid-connect) · [Workload identity federation](https://learn.microsoft.com/en-us/entra/workload-id/workload-identity-federation)
