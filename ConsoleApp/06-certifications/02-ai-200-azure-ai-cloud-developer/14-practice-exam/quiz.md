# C2.14 AI-200 Deneme Sınavı — 50 Soru

> Strateji ve kullanım: [README.md](README.md) · Müfredat: *skills measured as of October 7, 2026*
>
> **Kurallar:** 100 dakika (soru başına ≈ 2 dakika; kod ve vaka soruları daha uzun sürer), kapalı kitap — kod ve CLI boşlukları dahil. Cevaplarını ayrı bir yere yaz; `<details>` bloklarını sınav bitene kadar açma. Her sorunun etiketi, sorunun **alanını** ve **konusunu** (C2.x) gösterir: **A** = Develop containerized solutions on Azure (C2.1–C2.3) · **B** = Develop AI solutions by using Azure data management services (C2.4–C2.7) · **C** = Connect to and consume Azure services (C2.8–C2.10) · **D** = Secure, monitor, and troubleshoot Azure solutions (C2.11–C2.13).
>
> Dağılım resmî ağırlıklara göre: **A 11 soru (%22) · B 17 soru (%34) · C 11 soru (%22) · D 11 soru (%22)**. Sorular **özgündür**; gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Biçimler: tek/çoklu seçim, evet/hayır, sıralama, eşleştirme, senaryo, **C#/Python/CLI/KQL/SQL/YAML/JSON tamamlama** ve **iki vaka çalışması** (13–16 ve 29–32). Gerçek sınavın puanı ölçeklenmiştir (geçme 700/1000); buradaki yüzde yalnızca bir göstergedir.

---

### 1. `[B · C2.4]` Tek seçim
Bir e-ticaret API'si siparişleri Cosmos DB for NoSQL'de tutacak. Sorguların %90'ı "bir müşterinin siparişleri", geri kalanı ise `id` ile tek sipariş okumadır. Müşteri sayısı milyonlarcadır ve siparişler müşterilere görece eşit dağılır. Container için **en uygun** partition key hangisidir?

- A) `/status` (`new`, `paid`, `shipped`)
- B) `/orderDate` (gün bazında)
- C) `/customerId`
- D) `/country`

<details><summary>Cevap</summary>

**C.** `/customerId` yüksek cardinality, iyi dağılım ve sorgu uyumu sağlar: "müşterinin siparişleri" tek partition'a gider, `ReadItemAsync(id, customerId)` ucuz bir point read olur. `/status` ve `/country` düşük cardinality'dir (hot partition, 429). `/orderDate` bugüne yazmayı tek partition'a yığar ve müşteri sorgularını cross-partition yapar. Partition key path'i container oluşturulduktan sonra değiştirilemez.
</details>

### 2. `[A · C2.1]` Tek seçim
Bir ekip container image'larını tek bir registry'de tutacak. Gereksinimler: aynı registry'nin **iki Azure bölgesine geo-replication** ile yayılması ve registry'ye yalnızca **VNet içinden (private endpoint)** erişilmesi. Hangi ACR SKU'su **gereklidir**?

- A) Basic
- B) Standard
- C) Premium
- D) Bu özellikler SKU'dan bağımsızdır; yalnızca `az acr update` ile açılır

<details><summary>Cevap</summary>

**C.** Geo-replication ve private endpoint (Private Link) **Premium**'a özgüdür. Basic/Standard aynı programatik API'yi sunar ama bu ağ/dağıtım özelliklerini içermez. SKU sonradan yükseltilebilir (`az acr update --sku Premium`), ama özellik SKU'ya bağlıdır (D yanlış).
</details>

### 3. `[C · C2.8]` Tek seçim
Bir ekip HTTP ve kuyruk tetiklemeli Azure Functions uygulamasını Linux üzerinde çalıştıracak. Gereksinimler: (1) Cosmos DB ve Service Bus'a **private endpoint** üzerinden erişim için **VNet entegrasyonu**, (2) kullanılmadığı zamanlarda **sıfıra ölçeklenme**, (3) mümkünse kullandığın kadar öde. Hangi hosting planı **en uygundur**?

- A) Klasik Consumption
- B) Flex Consumption
- C) Premium (Elastic Premium)
- D) Dedicated (App Service planı)

<details><summary>Cevap</summary>

**B.** Flex Consumption hem VNet entegrasyonu hem scale-to-zero sunar. Klasik Consumption'da VNet entegrasyonu yoktur. Premium'da en az bir instance sürekli çalışır (sıfıra inmez). Dedicated de sıfıra inmez ve sabit plan ücreti vardır.
</details>

### 4. `[D · C2.11]` Tek seçim
Bir Container Apps API'si, bir Key Vault'taki (Azure RBAC izin modeli) secret'ları **yalnızca okuyacak**. API'nin user-assigned managed identity'sine **en az yetki ilkesine** uygun hangi rol, vault kapsamında atanmalıdır?

- A) Contributor
- B) Key Vault Reader
- C) Key Vault Secrets User
- D) Key Vault Secrets Officer

<details><summary>Cevap</summary>

**C.** *Key Vault Secrets User* secret **değerini okur** (data plane). Contributor control plane rolüdür ve RBAC modelinde secret değerini okutmaz. Key Vault Reader yalnızca meta veriyi gösterir, değeri vermez. Secrets Officer yazma/silme yetkisi de içerir — okuma ihtiyacı için fazladır.
</details>

### 5. `[B · C2.6]` Tek seçim
Bir RAG uygulaması Azure Database for PostgreSQL'de pgvector kullanıyor. Tablo **boş** olarak oluşturulacak, veri uygulama çalışırken sürekli eklenecek/güncellenecek ve öncelik **yüksek recall ile düşük sorgu gecikmesi**. Hangi index türü **en uygundur**?

- A) IVFFlat; `lists` değeri tablo boşken verilsin
- B) HNSW
- C) Index yok; her zaman exact tarama
- D) B-tree

<details><summary>Cevap</summary>

**B.** HNSW boş tabloda kurulabilir, veri geldikçe kendini günceller ve genelde en iyi recall/hız dengesini verir. IVFFlat'in kümeleri **mevcut veriden** öğrenilir: boş tabloda kurmak anlamsızdır, veri dağılımı değişince yeniden kurulmalıdır. Index'siz tarama doğru ama yavaştır; B-tree vector benzerliğini hızlandırmaz.
</details>

### 6. `[A · C2.3]` Senaryo
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

### 7. `[C · C2.9]` Tek seçim
Bir sipariş olayı **fraud**, **stok** ve **e-posta** servislerinin üçüne de ayrı ayrı iletilmeli; her servis kendi hızında ve birbirinden bağımsız işleyecek. Fraud servisi yalnızca `amount > 1000` siparişleri almalı. Hangi Service Bus tasarımı **en uygundur**?

- A) Tek queue; üç servis rakip tüketici olarak okur
- B) Bir topic + üç subscription; fraud subscription'ında SQL filter
- C) Üç ayrı Storage Queue
- D) Basic tier'da bir queue ve istemci tarafı filtreleme

<details><summary>Cevap</summary>

**B.** Topic'te her subscription mesajın **kendi kopyasını** alır (kendi lock/DLQ'su ile); filtre subscription'a yazılır. Queue'da mesajı yalnızca **bir** tüketici işler (A yanlış). Topic için en az **Standard** tier gerekir; Basic'te topic yoktur (D yanlış). Storage Queue'da topic/filter/DLQ yoktur.
</details>

### 8. `[D · C2.12]` Senaryo
Bir ekip 12 servisin ayarlarını merkezileştiriyor. Sorunlar: Dev ve Prod için aynı anahtarın farklı değerleri olmalı; ayarlar değişince servisler **yeniden dağıtım olmadan** yeni değeri almalı; veritabanı parolası ise düz metin olarak **hiçbir yerde** (App Configuration dahil) saklanmamalı. Hangi tasarım uygundur?

- A) Her şeyi `appsettings.Production.json` içine koy ve imajı yeniden build et
- B) App Configuration'da `Dev`/`Prod` **label**'ları; parola için **Key Vault referansı**; dinamik yenileme için sentinel key
- C) Tüm ayarları, parola dahil, App Configuration'a düz değer olarak yaz
- D) Tüm ayarları Key Vault secret olarak sakla ve feature flag'leri de oraya yaz

<details><summary>Cevap</summary>

**B.** Label ortam varyantı sağlar; Key Vault referansı secret'ı vault'ta tutar (uygulama kimliğinin vault'ta da yetkisi olmalıdır); sentinel ile restart'sız yenileme yapılır. A yeniden dağıtım ister, C secret'ı düz saklar, D Key Vault'u yapılandırma/feature flag deposu olarak kullanır (yanlış araç).
</details>

### 9. `[B · C2.4]` Kod tamamlama (C#)
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

### 10. `[A · C2.1]` CLI tamamlama
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

### 11. `[D · C2.13]` KQL tamamlama
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

### 12. `[B · C2.7]` Evet / Hayır
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

### 13. `[A · C2.1]` Tek seçim — Vaka 1
Gereksinim 1'i karşılamak için CI hattı Container Apps'e hangi image referansı ve hangi çekme kimliğiyle dağıtım yapmalıdır?

- A) `fabrikam.azurecr.io/shipments-api:latest` · registry admin user
- B) `fabrikam.azurecr.io/shipments-api@sha256:<digest>` · user-assigned managed identity + `AcrPull`
- C) `fabrikam.azurecr.io/shipments-api:1.4.2` · managed identity + registry üzerinde `Contributor`
- D) `fabrikam.azurecr.io/shipments-api:sha-3fa91c2` · service principal parolası ortam değişkeninde

<details><summary>Cevap</summary>

**B.** **Digest** içeriğin değişmez kimliğidir ("test edileni birebir çalıştır"); tag'ler aynı adla başka manifest'e taşınabilir (kilitlenmediyse). Parolasız çekme için managed identity + yalnızca çekme yetkisi veren `AcrPull` kullanılır; ilk revision'da kimlik gerektiği için **user-assigned** identity pratiktir. A `latest` + admin user'dır, C fazla yetkilidir (Contributor registry'yi yönetir) ve tag değişebilir, D parola saklar.
</details>

### 14. `[A · C2.2]` CLI tamamlama — Vaka 1
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

### 15. `[D · C2.11]` Tek seçim — Vaka 1
Gereksinim 4 için hangi yaklaşım doğrudur?

- A) Container App secret'ında **sürümlü** Key Vault URI'sini sabitle; uygulama kimliğine `Contributor` ver
- B) Container App secret'ını **sürümsüz** Key Vault URI'sine `keyvaultref` ile bağla; uygulamanın managed identity'sine vault'ta **Key Vault Secrets User** ver; döndürmeyi (yeni secret sürümü yazma) ayrı bir kimliğe (**Secrets Officer**) bırak
- C) Her döndürmeden sonra yeni parolayı Container App secret'ına elle kopyala
- D) Parolayı App Configuration'a düz değer olarak yaz, Key Vault'u kullanma

<details><summary>Cevap</summary>

**B.** Sürümsüz URI "en son sürüm" demektir; yeni sürüm yazıldığında platform bunu periyodik olarak alır (aralığı güncel dokümandan doğrula) veya revision restart ile hemen yansır. Uygulama yalnızca okur → *Secrets User*; yazma (rotation) yetkisi ayrı kimlikte kalır. A'da sürümlü URI rotation'ı **engeller** ve Contributor data plane'i okutmaz; C elle ve hataya açık; D secret'ı düz saklar.
</details>

### 16. `[A · C2.2]` Tek seçim — Vaka 1
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

### 17. `[C · C2.8]` Ayar tamamlama
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

### 18. `[B · C2.4]` Tek seçim
Bir uygulama kullanıcının yorumunu Cosmos DB'ye yazıyor ve **aynı kullanıcı** hemen ardından yorum listesini okuyor; kullanıcı kendi yorumunu **her zaman** görmeli. Hesap varsayılanı Session. Hedef: **en düşük RU ve gecikme**. Ne yapılmalıdır?

- A) Hesabı Strong'a yükselt
- B) Session'ı koru ve yazan/okuyan işlemde aynı `CosmosClient`'ı (veya session token'ı) kullan
- C) Eventual'a gevşet
- D) Bounded Staleness (K = 1) kullan

<details><summary>Cevap</summary>

**B.** Session, aynı session içinde *read-your-writes* garantisi verir ve Strong/Bounded'ın ~2× okuma RU'su maliyetini taşımaz. Bu garanti aynı client/session token'ı gerektirir; farklı süreçler arasında token taşınmalıdır. Eventual garanti vermez; Strong ve Bounded Staleness bu ihtiyaç için gereksiz pahalıdır.
</details>

### 19. `[A · C2.3]` Manifest hata bulma
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

### 20. `[D · C2.12]` Kod tamamlama (C#)
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

### 21. `[B · C2.6]` SQL tamamlama
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

### 22. `[C · C2.9]` Senaryo
Bir ekip `high-value` subscription'ına `amount > 1000` SQL filter kuralı ekledi, fakat subscription 50 TL'lik mesajları da almaya devam ediyor. Mesajlar `amount` değerini `ApplicationProperties`'e yazıyor. En olası neden nedir?

- A) SQL filter yalnızca Premium tier'da çalışır
- B) Subscription ile birlikte gelen `$Default` (TrueFilter) kuralı silinmedi
- C) Filtreler yalnızca mesaj gövdesine bakar, `ApplicationProperties`'e değil
- D) Duplicate detection kapalı

<details><summary>Cevap</summary>

**B.** Yeni subscription `$Default` (her mesajı kabul eden) kuralıyla gelir; yeni kural **eklenir**, varsayılan kural silinmez ve kurallar OR ile birleşir. Çözüm: `az servicebus topic subscription rule delete ... --name '$Default'`. Filtre **gövdeye değil property'lere** bakar (C tersini söylüyor); SQL filter Standard'da da çalışır; duplicate detection filtreyle ilgisizdir.
</details>

### 23. `[A · C2.2]` Evet / Hayır
Container Apps'te çalışan bir uygulamada aşağıdaki değişikliklerden hangileri **yeni bir revision oluşturur**?

| Değişiklik | Evet | Hayır |
|---|---|---|
| a) Bir secret'ın değerini güncellemek (`az containerapp secret set`) | ☐ | ☐ |
| b) Bir ortam değişkenini (`--set-env-vars`) değiştirmek | ☐ | ☐ |
| c) Scale kuralındaki `messageCount` değerini değiştirmek | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Revision-scope (`properties.template`): image, env var, probe, kaynaklar ve **scale kuralları** → yeni revision. Application-scope (`properties.configuration`): secret'lar, ingress, trafik bölme kuralları, revision mode → yeni revision **oluşturmaz**; çalışan replica'ların yeni secret değerini alması için revision restart gerekir.
</details>

### 24. `[B · C2.5]` Kod tamamlama (SQL)
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

### 25. `[D · C2.13]` KQL okuma
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

### 26. `[C · C2.8]` Tek seçim
Bir Timer trigger'lı function **her gün 02:30'da** (function app'in saat dilimine göre, varsayılan UTC) çalışmalı. Hangi ifade doğrudur?

- A) `30 2 * * *`
- B) `0 30 2 * * *`
- C) `0 2 30 * * *`
- D) `2:30:00`

<details><summary>Cevap</summary>

**B.** Functions Timer trigger'ı **6 alanlı NCRONTAB** kullanır: `{saniye} {dakika} {saat} {gün} {ay} {haftanın günü}` → `0 30 2 * * *`. A klasik 5 alanlı cron'dur, C saat/dakikayı ters yazar, D bir NCRONTAB değildir. Saat dilimi varsayılan UTC'dir (`WEBSITE_TIME_ZONE` ile değişir).
</details>

### 27. `[B · C2.4]` JSON tamamlama
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

### 28. `[D · C2.11]` Kod tamamlama (Python)
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

### 29. `[B · C2.5]` Tek seçim — Vaka 2
Gereksinim 1 için yeni container'ın vector yapılandırması **hangisi olmalıdır**?

- A) `flat` index, 3072 boyut, vector yolu normal range index'e dahil
- B) `diskANN` index, `dimensions: 1536`, `distanceFunction: cosine`, vector yolu `excludedPaths`'e eklenmiş
- C) `quantizedFlat` index, `dimensions: 1536`, vector yolu `includedPaths`'te
- D) Vector policy'siz container; embedding'ler normal alan olarak saklanıp `ORDER BY` ile sıralanır

<details><summary>Cevap</summary>

**B.** Milyonlarca vector için **diskANN** (ANN) uygundur; `dimensions` embedding modelinin çıktısıyla (1536) eşleşmeli ve vector policy ile vector index **container oluşturulurken** verilmelidir. Vector yolu `excludedPaths`'e alınır (range index'e sokmak yazma RU'sunu şişirir; arama `vectorIndexes` ile yapılır). `flat` küçük veri içindir, `quantizedFlat` orta ölçektir, 3072 modelle uyuşmaz; policy/index olmadan sorgu brute-force taranır.
</details>

### 30. `[B · C2.5]` Çoklu seçim (iki doğru cevap) — Vaka 2
Gereksinim 2 için change feed processor'ı 3 worker örneğiyle çalıştırırken, işin **çift işlenmeden paylaşılması** ve yeniden başlatmada **kaldığı yerden devam edilmesi** için **hangi iki** yapılandırma gereklidir?

- A) Tüm örneklerde aynı `processorName`
- B) Tüm örneklerin paylaştığı tek bir `leases` container'ı
- C) Tüm örneklerde aynı `instanceName`
- D) Her örnek için ayrı bir lease container'ı

<details><summary>Cevap</summary>

**A ve B.** Aynı processor adı + ortak lease container, lease'lerin (partition aralığı başına sahiplik + checkpoint) örnekler arasında paylaşılmasını ve bir örnek durduğunda devralınmasını sağlar; yeniden başlatma lease'teki continuation token'dan devam eder. `instanceName` her örnekte **benzersiz** olmalıdır (C yanlış). Örnek başına ayrı lease container'ı herkesin tüm değişiklikleri işlemesine yol açar (D yanlış). Handler'ın **idempotent** olması ve kendi PATCH'inin feed'e geri dönüp döngü yaratmaması için bir hash/versiyon koruması da gerekir.
</details>

### 31. `[B · C2.7]` Tek seçim — Vaka 2
Gereksinim 3 için Azure Managed Redis cache'i nasıl hazırlanmalıdır?

- A) Memory Optimized, modülsüz oluştur; RediSearch'i sonradan ekle
- B) Oluştururken **RediSearch** modülünü seç, **Enterprise** clustering policy ve **NoEviction** kullan; cache girdilerine TTL ver
- C) Flash Optimized katman, `allkeys-lru` eviction
- D) OSS cluster policy, `allkeys-lru` eviction

<details><summary>Cevap</summary>

**B.** Vector araması/semantic cache için RediSearch gerekir; modül **oluşturma sırasında** seçilir, RediSearch Enterprise clustering policy ve yalnızca `NoEviction` ile çalışır — bu yüzden bellek dolmaması için her girdiye TTL verilir. A (sonradan modül) mümkün değildir; C'de Flash Optimized katmanda vector modülü desteklenmez *(doğrula)* ve `allkeys-lru` RediSearch ile uyumsuzdur; D de uyumsuz politika seçimleridir.
</details>

### 32. `[C · C2.10]` CLI tamamlama — Vaka 2
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

### 33. `[A · C2.1]` Evet / Hayır
ACR image yönetimi hakkında aşağıdaki ifadeleri değerlendirin.

| İfade | Evet | Hayır |
|---|---|---|
| a) Aynı `todoapi:1.4.2` tag'i yeniden push edilirse (tag kilitli değilse) başka bir manifest'e işaret edebilir. | ☐ | ☐ |
| b) Base image update trigger'ı, task bir kez çalışıp bağımlılıkları izlenmeye başlamadan önce de çalışır. | ☐ | ☐ |
| c) ACR retention policy (Premium) tag'li image'ları da N gün sonra siler. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Hayır.** Tag değişebilir, **digest değişmez** (tag'i `--write-enabled false` ile kilitleyebilirsin). Base image bağımlılıkları task'ın **ilk build'inden sonra** izlenir; ayrıca base image sabit tag taşımalı ve (çok aşamalı Dockerfile'da) runtime image izlenir. Retention policy yalnızca **untagged** manifest'leri temizler.
</details>

### 34. `[B · C2.6]` Çoklu seçim (iki doğru cevap)
Çok kiracılı bir RAG uygulamasında, verinin ~%1'ini oluşturan bir kiracı için `WHERE tenant_id = 7 ORDER BY embedding <=> $1 LIMIT 5` sorgusu HNSW index'iyle **5'ten az satır** döndürüyor. **Hangi iki** çözüm uygundur?

- A) `SET hnsw.iterative_scan = relaxed_order;` (pgvector 0.8.0+)
- B) Tabloyu `PARTITION BY LIST (tenant_id)` ile bölmek ve partition başına HNSW index kullanmak
- C) `maintenance_work_mem` değerini artırmak
- D) `vector(1536)` kolonunu `vector(3072)` yapmak

<details><summary>Cevap</summary>

**A ve B.** HNSW önce `ef_search` kadar (varsayılan 40) aday döndürür, `WHERE` bunları **sonradan** eler ("over-filtering"). Iterative scan, yeterli sonuç çıkana kadar taramaya devam eder; partitioning kiracı filtresini tek partition'a/index'e indirir (ek olarak `hnsw.ef_search` artırımı, partial index veya B-tree de seçenektir). `maintenance_work_mem` yalnızca **index build** hızını etkiler; boyutu artırmak recall sorununu çözmez ve embedding modeliyle uyuşmazlık yaratır.
</details>

### 35. `[C · C2.9]` Kod tamamlama (C#)
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

### 36. `[D · C2.12]` Tek seçim
Yeni ödeme akışı **önce iç çalışanlara (beta grubu)**, sonra kademeli olarak kullanıcıların %5 → %20 → %100'üne açılacak. Her kullanıcı, açıldığı andan itibaren **tutarlı** biçimde aynı deneyimi görmeli. Hangi feature filter **en uygundur**?

- A) `Microsoft.Percentage`
- B) `Microsoft.TimeWindow`
- C) `Microsoft.Targeting`
- D) Özel bir `IFeatureFilter` yazılmalı; yerleşik filtre yok

<details><summary>Cevap</summary>

**C.** **Targeting** kullanıcı/grup hedefleme ve varsayılan yüzde ile kullanıcıya göre **tutarlı** karar verir (ring deployment). Percentage rastgeledir: aynı kullanıcı farklı isteklerde farklı sonuç alabilir. TimeWindow tarih penceresi içindir. Yerleşik Targeting yeterli olduğundan özel filtre gerekmez.
</details>

### 37. `[A · C2.2]` Çoklu seçim (iki doğru cevap)
Container Apps ölçekleme kuralları hakkında **hangi iki** ifade doğrudur?

- A) HTTP scale rule ile uygulama sıfır replica'ya inebilir.
- B) Yalnızca CPU kullanımına dayalı bir kuralla uygulama sıfır replica'ya inebilir.
- C) Service Bus kuralındaki `messageCount`, kuyruktaki **toplam** mesaj sayısı eşiğidir.
- D) Birden çok scale kuralı varsa replica sayısı, **en çok replica isteyen** kurala göre belirlenir.

<details><summary>Cevap</summary>

**A ve D.** HTTP (ve TCP/KEDA olay) kuralları scale-to-zero yapabilir; CPU/bellek kuralları yapamaz (en az 1 replica). `messageCount` **replica başına hedeftir**, toplam değildir. Kurallar çakışınca en yüksek replica talebi kazanır.
</details>

### 38. `[B · C2.7]` Kod tamamlama (Redis)
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

### 39. `[C · C2.10]` Eşleştirme
Her ihtiyacı en uygun servisle eşleştirin: **Azure Event Grid · Azure Service Bus · Azure Event Hubs**

1. Ödeme komutları **sırayla**, **session** ve **dead-letter queue** ile, işlenene kadar dayanıklı biçimde tutulmalı.
2. "Blob oluştu" gibi bir **olay bildirimi**, birbirinden habersiz birçok aboneye **push** edilmeli; her abone kendi filtresini tanımlıyor.
3. IoT cihazlarından **saniyede yüz binlerce telemetri olayı** alınıp consumer group'larla okunacak.

<details><summary>Cevap</summary>

**1 → Service Bus · 2 → Event Grid · 3 → Event Hubs.** Service Bus iş komutu/mesajı içindir (kuyruk, PeekLock, session, DLQ, transaction). Event Grid "oldu" bilgisini filtreli, push tabanlı fan-out ile dağıtır (kuyruk değildir). Event Hubs yüksek hacimli telemetri akışı (partition + consumer group) içindir. Birlikte kullanım: Event Grid → handler olarak Service Bus queue.
</details>

### 40. `[D · C2.13]` Senaryo
Application Insights'ta **%20 sabit yüzde sampling** açıkken, bir pano `requests | summarize n = count()` ile günlük istek sayısını 10.000 gösteriyor; yük dengeleyici ise yaklaşık 50.000 istek raporluyor. Bu farkı **doğru** gideren ve metrikleri yanıltmayan yaklaşım hangisidir?

- A) Sampling'i kapatmak için `connection string` yerine instrumentation key kullanmak
- B) `count()` yerine `sum(itemCount)` kullanmak
- C) `where` koşulunu `summarize`'dan sonraya taşımak
- D) Log Analytics workspace'ini başka bölgeye taşımak

<details><summary>Cevap</summary>

**B.** Örneklenmiş telemetride her kayıt `itemCount` kadar gerçek isteği **temsil eder**; `count()` eksik sayar, `sum(itemCount)` gerçek sayıyı verir. Instrumentation-key-only ingestion artık sonlandırılmıştır ve sampling kararıyla ilgisizdir; `where` yeri sayıyı değil süzme anlamını değiştirir; bölge sayımı etkilemez. Alert'leri örneklenmeyen **metrikler** üzerine kurmak da daha güvenilirdir.
</details>

### 41. `[A · C2.3]` Sıralama
Yeni dağıtımdan sonra `todoapi` pod'ları `CrashLoopBackOff`'ta. Teşhis ve düzeltme adımlarını **doğru sıraya** koyun:

1. Düzeltmeyi uygula (eksik yapılandırmayı ekle) ve gerekirse `kubectl rollout undo deploy/todoapi`; ardından `kubectl rollout status` ile doğrula
2. `kubectl logs <pod> --previous` ile çöken container'ın önceki çalışma logunu oku
3. `kubectl get pods -n todo` ile durum, `READY` ve `RESTARTS` değerlerine bak
4. `kubectl describe pod <pod> -n todo` ile `Last State`, `Reason`, `Exit Code` ve Events'e bak

<details><summary>Cevap</summary>

**3 → 4 → 2 → 1.** Sistematik teşhis sırası: durum (`get`) → ayrıntı (`describe`: Exit Code 1 = uygulama hatası, 137 = OOM) → log (`--previous`; yeni container'ın logu çoğu zaman boştur) → düzeltme/geri alma ve doğrulama. `CrashLoopBackOff` bir **belirtidir**; kök neden `logs --previous` ve `describe`'dadır.
</details>

### 42. `[B · C2.4]` Evet / Hayır
Cosmos DB for NoSQL hakkında aşağıdaki ifadeleri değerlendirin.

| İfade | Evet | Hayır |
|---|---|---|
| a) Transactional batch, **farklı partition key** değerlerine sahip item'ları tek bir atomik işlemde yazabilir. | ☐ | ☐ |
| b) 429 (Request rate too large) aldığında .NET SDK varsayılan olarak isteği otomatik yeniden dener. | ☐ | ☐ |
| c) İstek bazında consistency level, hesap varsayılanından **daha güçlü** bir seviyeye çıkarılabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** Transactional batch'te tüm işlemler **aynı partition key**'e ait olmalıdır. SDK, `x-ms-retry-after-ms` kadar bekleyip (varsayılan 9 deneme / 30 sn toplam) yeniden dener; kalıcı 429 = RU yetersiz veya hot partition. Consistency istek bazında yalnızca **gevşetilebilir**, güçlendirilemez.
</details>

### 43. `[C · C2.8]` Çoklu seçim (iki doğru cevap)
Azure Functions trigger/binding kavramları hakkında **hangi iki** ifade doğrudur?

- A) Bir function'ın tam olarak bir trigger'ı vardır.
- B) Output binding function'ı **başlatabilir**.
- C) Cosmos DB'ye identity-based bağlantıda hesap üzerinde Azure RBAC `Contributor` rolü yeterlidir.
- D) Python v2 programlama modelinde trigger ve binding'ler `function.json` yerine decorator'larla tanımlanır.

<details><summary>Cevap</summary>

**A ve D.** Function'ı yalnızca **trigger** başlatır (function başına tam bir tane); input/output binding'ler başlatmaz. Cosmos DB'de veri erişimi Azure RBAC ile değil **Cosmos DB data-plane role assignment**'ı (`az cosmosdb sql role assignment create`) ile verilir. .NET isolated'da attribute, Python v2'de decorator kullanılır.
</details>

### 44. `[D · C2.11]` Sıralama
Key Vault'taki bir secret için **otomatik rotation** akışını doğru sıraya koyun:

1. Function, hedef serviste (ör. ikincil anahtar/ikinci kimlik bilgisi) **yeni parolayı üretir**
2. Key Vault, secret süresi dolmadan önce `Microsoft.KeyVault.SecretNearExpiry` olayını yayınlar
3. Uygulama, sürümsüz referansı yenileyerek yeni sürümü kullanmaya başlar
4. Event Grid olayı abone olan Azure Function'a iletir
5. Function, Key Vault'a **yeni bir secret sürümü** yazar

<details><summary>Cevap</summary>

**2 → 4 → 1 → 5 → 3.** Event Grid rotation'ı **yapmaz**, yalnızca tetikler; yeni değeri hedef serviste üreten ve vault'a yazan sizin kodunuzdur (handler **idempotent** olmalı: Event Grid at-least-once teslim eder). Yalnızca `SecretNearExpiry`/`SecretExpired` türlerine abone ol — `SecretNewVersionCreated`'a abone olup aynı function'dan yazmak sonsuz döngü doğurur. Uygulama sürümsüz referans + periyodik yenileme (`ReloadInterval`, platform referans yenilemesi) ile yeni sürümü alır.
</details>

### 45. `[B · C2.5]` Tek seçim
Bir ekip change feed processor'ı **latest version** (varsayılan) modunda çalıştırıyor. Bir ürün fiziksel olarak silindiğinde embedding temizleme mantığı hiç tetiklenmiyor. Neden ve en pratik çözüm nedir?

- A) Processor silmeleri yalnızca lease container boşken görür
- B) Latest version modu silmeleri **göstermez**; fiziksel silme yerine `deleted: true` (soft delete) yaz (veya *all versions and deletes* modunu kullan)
- C) Silmeler yalnızca Functions Cosmos DB trigger'ında görünür
- D) `diskANN` index silmeleri gizler

<details><summary>Cevap</summary>

**B.** Latest version modu yalnızca eklenen/güncellenen item'ların son halini gösterir; silmeler ve TTL ile silinenler görünmez. Çözüm soft delete (+ `ttl` ile sonra temizlik) veya *all versions and deletes* modudur (continuous backup gerektirir). Functions trigger'ı da aynı change feed'i kullanır (C yanlış); lease ve vector index silme görünürlüğünü etkilemez.
</details>

### 46. `[C · C2.9]` Çoklu seçim (iki doğru cevap)
Bir sipariş sisteminde: (1) aynı müşterinin sipariş güncellemeleri **sırayla ve tek tüketicide** işlenmeli, (2) çift tıklamayla aynı sipariş **iki kez gönderilirse** ikincisi yutulmalı. **Hangi iki** yapılandırma gerekir?

- A) Kuyrukta `RequiresSession = true` ve mesajlarda `SessionId = customerId`
- B) Duplicate detection (kuyruğu oluştururken açılır) ve `MessageId = orderId`
- C) `ReceiveAndDelete` alma modu
- D) Basic tier

<details><summary>Cevap</summary>

**A ve B.** Sıra + tek alıcı için **session**; gönderici tekrarını elemek için **duplicate detection** (oluştururken seçilir, sonradan açılamaz; `MessageId` üzerinden, zaman penceresi içinde). `ReceiveAndDelete` at-most-once'tır ve sıra sağlamaz; Basic'te session ve duplicate detection yoktur (Standard+ gerekir). Duplicate detection tüketici idempotency'sinin yerini tutmaz.
</details>

### 47. `[B · C2.6]` Tek seçim
Bir API 200 pod'da çalışıyor; her istekte yeni `NpgsqlConnection` açıyor ve yük altında `remaining connection slots are reserved` / `too many connections` hataları alıyor. Azure Database for PostgreSQL Flexible Server için **en uygun** çözüm hangisidir?

- A) `max_connections` değerini sınırsız yapmak
- B) Pod başına tek `NpgsqlDataSource` (singleton, küçük `MaxPoolSize`) kullanmak ve yerleşik **PgBouncer**'a (port 6432) bağlanmak
- C) Vector index'i IVFFlat'e çevirmek
- D) `Multiplexing`'i kapatıp pool'u kaldırmak

<details><summary>Cevap</summary>

**B.** PostgreSQL bağlantısı süreç tabanlı ve pahalıdır; istemci havuzu (singleton `NpgsqlDataSource`) + sunucu tarafı PgBouncer çoklaması bağlantı sayısını düşürür. Kural: `uygulama örneği × MaxPoolSize ≤ sunucu max_connections` (yedek bağlantılar düşülerek). `max_connections` sınırsız yapılamaz (SKU'ya bağlıdır); IVFFlat bağlantıyla ilgisizdir; pool'u kaldırmak sorunu büyütür. PgBouncer transaction pooling'inde session düzeyi `SET` yerine `SET LOCAL` kullan.
</details>

### 48. `[D · C2.13]` Kod tamamlama (C#)
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

### 49. `[C · C2.10]` CLI tamamlama
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

### 50. `[B · C2.7]` Tek seçim
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
| C2.1 Container Registry ve ACR Tasks | 2, 10, 13, 33 | | [README](../01-container-registry/README.md) · [quiz](../01-container-registry/quiz.md) |
| C2.2 Container Apps: revision ve KEDA | 14, 16, 23, 37 | | [README](../02-container-apps/README.md) · [quiz](../02-container-apps/quiz.md) |
| C2.3 AKS manifest ve teşhis | 6, 19, 41 | | [README](../03-aks-manifests/README.md) · [quiz](../03-aks-manifests/quiz.md) |
| C2.4 Cosmos DB for NoSQL | 1, 9, 18, 27, 42 | | [README](../04-cosmos-db-nosql/README.md) · [quiz](../04-cosmos-db-nosql/quiz.md) |
| C2.5 Cosmos vector search ve change feed | 24, 29, 30, 45 | | [README](../05-cosmos-vector-change-feed/README.md) · [quiz](../05-cosmos-vector-change-feed/quiz.md) |
| C2.6 PostgreSQL ve pgvector | 5, 21, 34, 47 | | [README](../06-postgresql-pgvector/README.md) · [quiz](../06-postgresql-pgvector/quiz.md) |
| C2.7 Azure Managed Redis | 12, 31, 38, 50 | | [README](../07-managed-redis/README.md) · [quiz](../07-managed-redis/quiz.md) |
| C2.8 Azure Functions | 3, 17, 26, 43 | | [README](../08-azure-functions/README.md) · [quiz](../08-azure-functions/quiz.md) |
| C2.9 Service Bus | 7, 22, 35, 46 | | [README](../09-service-bus/README.md) · [quiz](../09-service-bus/quiz.md) |
| C2.10 Event Grid | 32, 39, 49 | | [README](../10-event-grid/README.md) · [quiz](../10-event-grid/quiz.md) |
| C2.11 Managed identity ve Key Vault | 4, 15, 28, 44 | | [README](../11-identity-key-vault/README.md) · [quiz](../11-identity-key-vault/quiz.md) |
| C2.12 App Configuration | 8, 20, 36 | | [README](../12-app-configuration/README.md) · [quiz](../12-app-configuration/quiz.md) |
| C2.13 OpenTelemetry ve KQL | 11, 25, 40, 48 | | [README](../13-opentelemetry-kql/README.md) · [quiz](../13-opentelemetry-kql/quiz.md) |

**Tekrar planı:** Her yanlış için *bilgi eksiği* mi *dikkat hatası* mı olduğunu not et (kod/CLI/KQL tamamlamada: sözdizimini mi bilmiyordun, yoksa kavramı mı karıştırdın?). Bir konuda **2 veya daha fazla** bilgi eksiği varsa o konunun README'sini ve laboratuvarını baştan yap, ardından konu quiz'ini ≥ %80 olana kadar çöz. Vaka sorularında yanlışın nedeni çoğu zaman bir **gereksinimi kaçırmak**tır — [README 3.3](README.md#33-vaka-çalışması-ve-kod--cli--kql-tamamlama-soruları)'teki gereksinim listesi tekniğini tekrar oku.
