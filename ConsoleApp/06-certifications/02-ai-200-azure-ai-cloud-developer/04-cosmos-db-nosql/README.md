# C2.4 Cosmos DB for NoSQL: SDK, Partitioning, Indexing, Consistency, RU

> 📁 `ConsoleApp/06-certifications/02-ai-200-azure-ai-cloud-developer/04-cosmos-db-nosql/` · Sınav alanı: **Develop AI solutions by using Azure data management services (%30-35)** · Önkoşul: C1 (storage/identity temelleri), M8 (NoSQL) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of October 7, 2026):** *Develop AI solutions by using Azure data management services* — Azure Cosmos DB for NoSQL'e SDK ile bağlan, item oluştur/oku/sorgula · **partition key**, **indexing policy** ve **consistency level** yapılandır · **RU (request unit)** maliyetini ölç ve optimize et. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)'ı kontrol et.

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

Vector arama ve change feed ayrı konudur (→ [C2.5](../05-cosmos-vector-change-feed/README.md)); bu konu temel veri düzlemini ve RU ekonomisini kapsar.

---

## 2. Neden?

- **Sınav:** Bu alan toplam sınavın **%30-35**'idir ve Cosmos DB bunun en büyük parçasıdır. Sorular genelde "şu senaryoda hangi partition key?", "RU neden arttı?", "hangi consistency?", "boşluğu doldur" biçimindedir.
- **Gerçek işte:** Yanlış partition key, **hot partition** (tek physical partition'ın 10.000 RU/s sınırında 429 almaya başlaması) ve cross-partition sorgu faturası demektir. Cosmos'ta *performans sorunu çoğu zaman bir RU sorunudur*, ve RU'yu ölçmeyi bilmeyen ekip tahminle sizing yapar.
- **AI iş yükü:** RAG/ajan uygulamalarında Cosmos; sohbet geçmişi, ajan durumu, doküman metadata'sı ve embedding'ler için kalıcı depodur (→ C2.5, M15.4).

---

## 3. Nasıl?

### 3.1 Partition key ⭐

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

### 3.2 SDK: `CosmosClient` ve temel işlemler

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

### 3.3 Throughput modelleri

| Model | Nasıl çalışır | Ne zaman | Dikkat |
|---|---|---|---|
| **Provisioned — manual** | Sabit RU/s (saatlik faturalanır) | Öngörülebilir, sürekli yük | Fazla ayırırsan boşa ödersin, azsa 429 |
| **Provisioned — autoscale** | Tmax belirlenir; sistem Tmax'ın %10'u ile %100'ü arasında ölçekler | Değişken/zirveli yük | Birim fiyat manual'dan yüksektir; faturalama saatlik kullanılan **en yüksek** RU/s'ye göre |
| **Serverless** | Rezerve throughput yok, **tüketilen RU** kadar öde | Dev/test, seyrek/küçük yük | Tek bölge, container başına throughput/depolama üst sınırları var; sürekli yüksek yük için pahalı |
| **Free tier** | Hesap başına (abonelikte tek) ilk **1000 RU/s + 25 GB** ücretsiz | Öğrenme/laboratuvar | Aboneliğte yalnızca bir hesapta |

- **Database düzeyi (shared) vs container düzeyi (dedicated)** throughput: paylaşılan RU havuzu birçok küçük container için ucuzdur ama bir container havuzu tüketip diğerlerini aç bırakabilir.
- Bir physical partition en çok yaklaşık **10.000 RU/s** ve **50 GB** taşır; toplam RU/s partition sayısına bölündüğü için **hot partition'da container'ın RU/s'si ne olursa olsun 429** alırsın.
- **429 (Request rate too large):** SDK `x-ms-retry-after-ms` kadar bekleyip otomatik yeniden dener; sürekli 429 = ya throughput yetersiz ya hot partition.

### 3.4 Indexing policy ⭐

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

### 3.5 Consistency level'lar ⭐

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

### 3.6 RU optimizasyon reçetesi

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

### 3.7 Lab — iki partition key ile RU ölçümü

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

## 4. Gerçek dünya senaryosu

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

## 5. Sınav perspektifi

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

## 6. Yaygın hatalar

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

## 7. Performans & trade-off

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

## 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Azure Cosmos DB for NoSQL documentation](https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/)
- [Partitioning and horizontal scaling](https://learn.microsoft.com/en-us/azure/cosmos-db/partitioning-overview) · [Hierarchical partition keys](https://learn.microsoft.com/en-us/azure/cosmos-db/hierarchical-partition-keys)
- [Request Units](https://learn.microsoft.com/en-us/azure/cosmos-db/request-units) · [Optimize request cost](https://learn.microsoft.com/en-us/azure/cosmos-db/optimize-cost-reads-writes)
- [Indexing policies](https://learn.microsoft.com/en-us/azure/cosmos-db/index-policy) · [Consistency levels](https://learn.microsoft.com/en-us/azure/cosmos-db/consistency-levels)
- [.NET SDK best practices](https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/best-practice-dotnet) · [Python SDK (`azure-cosmos`)](https://learn.microsoft.com/en-us/python/api/overview/azure/cosmos-readme)
- [Data-plane RBAC](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-connect-role-based-access-control) · [Cosmos DB emulator](https://learn.microsoft.com/en-us/azure/cosmos-db/emulator)
- Sonraki konu: [C2.5 Vector Search ve Change Feed](../05-cosmos-vector-change-feed/README.md)
