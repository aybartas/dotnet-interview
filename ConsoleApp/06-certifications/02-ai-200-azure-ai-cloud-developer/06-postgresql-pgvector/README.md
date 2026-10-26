# C2.6 Azure Database for PostgreSQL ve pgvector

> 📁 `ConsoleApp/06-certifications/02-ai-200-azure-ai-cloud-developer/06-postgresql-pgvector/` · Sınav alanı: **Develop AI solutions by using Azure data management services (%30-35)** · Önkoşul: M6 (PostgreSQL), M15.4 (embeddings & vector search) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of October 7, 2026):** PostgreSQL'de **vector iş yükü** için şema ve index tasarla · vector **benzerlik araması** yap · **metadata filtreli RAG** sorguları yaz · **bağlantı** (connection) yönetimini optimize et. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)'ı kontrol et.

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

Cosmos DB ile karşılaştırma ve depo seçimi → bölüm 5 ve [C2.5](../05-cosmos-vector-change-feed/README.md), [C2.7](../07-managed-redis/README.md).

---

## 2. Neden?

- **Sınav:** "HNSW mi IVFFlat mı?", "index neden kullanılmıyor?" (operator class), "tenant filtresiyle sonuç eksik" (iterative scan), "bağlantılar tükendi" (pooling) kalıpları bu konudadır.
- **Gerçek işte:** Uygulama verisi zaten PostgreSQL'deyse vector için ayrı bir sisteme gerek yoktur; **transaction'da veriyle birlikte tutarlı** güncellenir, SQL filtreleri ve JOIN'ler hazırdır, yedekleme/HA/RBAC tek yerdedir.
- **Maliyet:** Vector'ler büyüktür (1536 boyut ≈ 6 KB/satır). Index belleğe sığmazsa gecikme patlar; bu yüzden boyutlandırma ve `halfvec`/boyut indirgeme kararları parayla ilgilidir.

---

## 3. Nasıl?

### 3.1 Sunucu ve eklentiyi etkinleştirme

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

### 3.2 Şema ve mesafe operatörleri

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

### 3.3 Index'ler: HNSW vs IVFFlat ⭐

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

### 3.4 Maliyet ve boyut

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

### 3.5 RAG + metadata filtresi ve iterative scan

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

### 3.6 Bağlantı optimizasyonu ⭐

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

### 3.7 Lab — HNSW ve IVFFlat karşılaştırması

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

## 4. Gerçek dünya senaryosu

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

## 5. Sınav perspektifi

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

## 6. Yaygın hatalar

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

## 7. Performans & trade-off

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

## 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [How to use pgvector on Azure Database for PostgreSQL](https://learn.microsoft.com/en-us/azure/postgresql/extensions/how-to-use-pgvector) · [Optimize performance when using pgvector](https://learn.microsoft.com/en-us/azure/postgresql/extensions/how-to-optimize-performance-pgvector)
- [How to allowlist and create extensions](https://learn.microsoft.com/en-us/azure/postgresql/extensions/how-to-create-extensions) · [DiskANN extension (pg_diskann)](https://learn.microsoft.com/en-us/azure/postgresql/extensions/how-to-use-pgdiskann)
- [PgBouncer in Flexible Server](https://learn.microsoft.com/en-us/azure/postgresql/flexible-server/concepts-pgbouncer) · [Microsoft Entra authentication](https://learn.microsoft.com/en-us/azure/postgresql/flexible-server/security-entra-concepts)
- [pgvector (GitHub README)](https://github.com/pgvector/pgvector) · [Npgsql + pgvector](https://github.com/pgvector/pgvector-dotnet) · [pgvector-python](https://github.com/pgvector/pgvector-python)
- Önceki: [C2.5 Cosmos DB Vector ve Change Feed](../05-cosmos-vector-change-feed/README.md) · Sonraki: [C2.7 Azure Managed Redis](../07-managed-redis/README.md)
