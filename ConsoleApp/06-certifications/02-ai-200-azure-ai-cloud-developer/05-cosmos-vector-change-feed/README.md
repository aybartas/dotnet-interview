# C2.5 Cosmos DB ile Vector Search ve Change Feed

> 📁 `ConsoleApp/06-certifications/02-ai-200-azure-ai-cloud-developer/05-cosmos-vector-change-feed/` · Sınav alanı: **Develop AI solutions by using Azure data management services (%30-35)** · Önkoşul: [C2.4](../04-cosmos-db-nosql/README.md), M15.4 (embeddings & vector search) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of October 7, 2026):** Cosmos DB'de **embedding'leri sakla**, **vector indexing policy** yapılandır ve vector benzerlik araması yap · **change feed processor** ile yeni/güncellenen item'ları işle. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)'ı kontrol et.

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

## 2. Neden?

- **Sınav:** "Policy'de boyut uyuşmuyor", "hangi index türü?", "`VectorDistance` sorgusunu tamamla", "change feed silmeleri neden göstermiyor?", "lease container ne işe yarar?" sorularının hepsi bu konudandır.
- **Gerçek işte:** Operasyonel veri (ürün, sipariş, sohbet geçmişi) zaten Cosmos'ta ise **ayrı bir vector veritabanı** işletmek, senkronizasyon, ikinci güvenlik/yedekleme sınırı ve tutarsızlık demektir. Aynı container'da hem metadata hem vector tutmak ve `WHERE` filtresiyle birleştirmek mimariyi sadeleştirir.
- **Tazelik:** RAG'in en sinsi hatası **bayat embedding**'dir (içerik güncellendi, vector eski). Change feed bunu olay güdümlü ve ölçeklenebilir biçimde çözer.

---

## 3. Nasıl?

### 3.1 Vector embedding policy ve vector indexing policy

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

### 3.2 Vector sorguları

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

### 3.3 Change feed ⭐

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

### 3.4 Lab — ürün araması ve embedding yenileme

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

## 4. Gerçek dünya senaryosu

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

## 5. Sınav perspektifi

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

## 6. Yaygın hatalar

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

## 7. Performans & trade-off

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

## 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Vector search in Azure Cosmos DB for NoSQL](https://learn.microsoft.com/en-us/azure/cosmos-db/vector-search) · [Index and query vector data](https://learn.microsoft.com/en-us/cosmos-db/index-vector-data)
- [Index and query vectors in .NET](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-dotnet-vector-index-query) · [in Python](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-python-vector-index-query)
- [Change feed in Azure Cosmos DB](https://learn.microsoft.com/en-us/azure/cosmos-db/change-feed) · [Change feed processor](https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/change-feed-processor) · [All versions and deletes mode](https://devblogs.microsoft.com/cosmosdb/azure-cosmos-db-all-versions-and-deletes-change-feed-mode-is-now-generally-available/)
- [Azure Functions Cosmos DB trigger](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-cosmosdb-v2-trigger)
- Önceki: [C2.4 Cosmos DB for NoSQL](../04-cosmos-db-nosql/README.md) · Sonraki: [C2.6 PostgreSQL ve pgvector](../06-postgresql-pgvector/README.md)
