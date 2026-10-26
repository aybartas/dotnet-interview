# C2.7 Azure Managed Redis: Cache ve Vector Index

> 📁 `ConsoleApp/06-certifications/02-ai-200-azure-ai-cloud-developer/07-managed-redis/` · Sınav alanı: **Develop AI solutions by using Azure data management services (%30-35)** · Önkoşul: M8.4 (cache desenleri), M15.4 (embeddings & vector search) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of October 7, 2026):** **Azure Managed Redis** ile **cache** (expiration ve invalidation dahil) uygula · **vector indexing** ile benzerlik araması yap. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)'ı kontrol et.

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

## 2. Neden?

- **Sınav:** "TTL ile eviction farkı?", "yazma sonrası bayat veri nasıl önlenir?", "vector için modül ne zaman seçilir?", `FT.CREATE` tamamlama, "semantic cache nedir?", "Redis'i ne için kullanmamalı?" soruları.
- **Gerçek işte:** Cache, veritabanı yükünü ve gecikmeyi büyük ölçüde düşürür; LLM çağrılarında ise **semantic cache** hem maliyeti (token) hem gecikmeyi düşürür: "iade politikası nedir?" ile "ürünü nasıl iade ederim?" aynı yanıtı paylaşabilir.
- **Doğru araç:** Redis **bellek** maliyetlidir ve (cache olarak) kaynak veri deposu değildir. Kalıcı vector verisi → Cosmos ([C2.5](../05-cosmos-vector-change-feed/README.md)) veya PostgreSQL ([C2.6](../06-postgresql-pgvector/README.md)); Redis hızlı, geçici, tekrar üretilebilir katmandır.

---

## 3. Nasıl?

### 3.1 Sağlama: katman, modül, clustering, eviction

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

### 3.2 Cache-aside, TTL ve invalidation ⭐

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

### 3.3 Vector index ve KNN arama

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

### 3.4 Semantic cache

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
| Kalıcı kaynak vector verisi, filtre + JOIN + ACID | **PostgreSQL pgvector** ([C2.6](../06-postgresql-pgvector/README.md)) |
| Global dağıtım, esnek şema, operasyonel veriyle aynı yerde | **Cosmos DB** ([C2.5](../05-cosmos-vector-change-feed/README.md)) |
| Çok büyük kalıcı vector seti, maliyet hassas | Redis **değil** (bellek pahalı) — Cosmos/PostgreSQL |

### 3.5 Lab — cache-aside, vector index ve semantic cache

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

## 4. Gerçek dünya senaryosu

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

## 5. Sınav perspektifi

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

## 6. Yaygın hatalar

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

## 7. Performans & trade-off

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

## 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Azure Managed Redis overview](https://learn.microsoft.com/en-us/azure/redis/overview) · [Quickstart: create an Azure Managed Redis instance](https://learn.microsoft.com/en-us/azure/redis/quickstart-create-managed-redis)
- [Redis modules in Azure Managed Redis](https://learn.microsoft.com/en-us/azure/redis/redis-modules) · [Vector similarity overview](https://learn.microsoft.com/en-us/azure/redis/overview-vector-similarity) · [Tutorial: semantic cache](https://learn.microsoft.com/en-us/azure/redis/tutorial-semantic-cache)
- [Azure Cache for Redis retirement FAQ](https://learn.microsoft.com/en-us/azure/azure-cache-for-redis/retirement-faq) · [Migration guide](https://learn.microsoft.com/en-us/azure/azure-cache-for-redis/cache-migration-guide)
- [.NET client guidance (Microsoft Entra, StackExchange.Redis)](https://learn.microsoft.com/en-us/azure/redis/dotnet) · [HybridCache in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/hybrid)
- [Redis vector search documentation](https://redis.io/docs/latest/develop/ai/search-and-query/vectors/) · [redis-py](https://redis.readthedocs.io/)
- Önceki: [C2.6 PostgreSQL ve pgvector](../06-postgresql-pgvector/README.md) · [C2.5 Cosmos DB Vector ve Change Feed](../05-cosmos-vector-change-feed/README.md)
