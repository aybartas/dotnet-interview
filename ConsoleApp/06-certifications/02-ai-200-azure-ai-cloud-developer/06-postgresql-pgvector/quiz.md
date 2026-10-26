# C2.6 Azure Database for PostgreSQL ve pgvector — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Develop AI solutions by using Azure data management services* (%30-35) · 16 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, SQL/kod/CLI tamamlama, senaryo, vaka); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 13/16** (≥ %80).

---

### 1. Tek seçim
Bir ekip Azure Database for PostgreSQL Flexible Server'da `CREATE EXTENSION vector;` çalıştırıyor ve `extension "vector" is not allow-listed` hatası alıyor. İlk yapılması gereken nedir?

- A) Sunucuyu Cosmos DB'ye taşımak
- B) `azure.extensions` sunucu parametresine `VECTOR` eklemek
- C) `shared_buffers`'ı artırmak
- D) Veritabanını yeniden oluşturmak

<details><summary>Cevap</summary>

**B.** Flexible Server'da eklentiler önce izin listesine (`azure.extensions`) eklenir; sonra her veritabanında `CREATE EXTENSION` çalıştırılır. Bellek ayarı veya veritabanını yeniden oluşturmak hatayı çözmez.
</details>

### 2. SQL tamamlama
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

### 3. Tek seçim
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

### 4. Eşleştirme
Operatörleri mesafe türleriyle eşleştirin: `<->` · `<=>` · `<#>`

1. Cosine distance
2. Negatif inner product
3. L2 (euclidean) mesafesi

<details><summary>Cevap</summary>

**`<->` → 3 · `<=>` → 1 · `<#>` → 2.** `<#>` *negatif* inner product döndürür (benzerlik = `-(a <#> b)`).
</details>

### 5. Tek seçim
Bir ekip 5 milyon satırlık tabloyu yükledikten sonra, **hızlı build** ve **düşük bellek kullanımı** isteyen bir ANN index arıyor; recall'dan biraz fedakârlık edilebilir ve sorgu zamanında ayar yapılabiliyor. Hangi seçim uygundur?

- A) IVFFlat (veri yüklendikten sonra, `lists` uygun seçilerek)
- B) HNSW, `m = 64`, `ef_construction = 400`
- C) B-tree index
- D) GIN index

<details><summary>Cevap</summary>

**A.** IVFFlat hızlı kurulur ve daha az bellek kullanır; recall `ivfflat.probes` ile ayarlanır. HNSW daha iyi recall verir ama build'i yavaş ve bellek-yoğundur. B-tree/GIN vector benzerliği için uygun değildir.
</details>

### 6. Çoklu seçim
IVFFlat index'i hakkında hangi iki ifade doğrudur?

- A) Kümeler (lists) mevcut veriden öğrenildiği için index veri yüklendikten sonra kurulmalıdır.
- B) Sorgu zamanında recall'ı `ivfflat.probes` ile artırabilirsiniz.
- C) Boş tabloda kurulması en iyi sonucu verir.
- D) `hnsw.ef_search` IVFFlat'i de etkiler.

<details><summary>Cevap</summary>

**A ve B.** Boş tabloda kurulan IVFFlat anlamsız kümeler üretir (C yanlış). `ef_search` HNSW'ye aittir (D yanlış).
</details>

### 7. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) HNSW index boş bir tabloda oluşturulabilir ve sonradan eklenen satırlarla kendini günceller. | ☐ | ☐ |
| b) `hnsw.ef_search` değerini artırmak recall'ı genelde artırır, gecikmeyi de artırır. | ☐ | ☐ |
| c) ANN index'ler (HNSW/IVFFlat) her zaman %100 doğru (exact) sonuç döner. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** ANN index'ler yaklaşıktır; tam sonuç için index'siz (sequential scan) çalıştırılır.
</details>

### 8. Senaryo
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

### 9. Kod tamamlama (C#)
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

### 10. Senaryo
Bir ASP.NET Core API her istekte `new NpgsqlConnection(...)` açıp kapatıyor ve 200 pod ile çalışıyor. Yük altında `remaining connection slots are reserved` hatası alınıyor. Hangi iki değişiklik **birlikte** en doğrudur?

- A) `NpgsqlDataSource`'u singleton yapıp pod başına `MaxPoolSize`'ı küçük tutmak
- B) Flexible Server'ın yerleşik **PgBouncer**'ını etkinleştirip uygulamayı port **6432**'ye yönlendirmek
- C) `max_connections`'ı sınırsız yapmak
- D) Her istekte bağlantıyı açık bırakıp hiç kapatmamak

<details><summary>Cevap</summary>

**A ve B.** Havuzlama ve sunucu önü bağlantı çoklayıcı bağlantı sayısını kontrol eder. `max_connections` bellek ve process maliyetiyle sınırlıdır; bağlantıyı kapatmamak sızıntıdır.
</details>

### 11. Sıralama
Sıfırdan bir RAG vector deposu kurulumunu doğru **sıraya** koyun: (1) HNSW/IVFFlat index oluştur (2) `azure.extensions`'a `VECTOR` ekle (3) `CREATE EXTENSION vector` (4) tabloyu `vector(n)` kolonuyla oluştur ve veriyi yükle

<details><summary>Cevap</summary>

**2 → 3 → 4 → 1.** (HNSW için index'i erken kurmak da mümkündür; IVFFlat için index mutlaka veri yüklendikten sonra kurulur — bu yüzden güvenli genel sıra index'in sonda olmasıdır.)
</details>

### 12. CLI tamamlama
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

### 13. Tek seçim
1536 boyutlu `vector` kolonunda 2 milyon satır var; HNSW index'i RAM'e sığmıyor ve sorgu gecikmeleri artıyor. Hangi seçenek **depolama ve bellek** ihtiyacını doğrudan azaltır?

- A) `halfvec` kullanmak veya embedding boyutunu düşürmek (modelin `dimensions` parametresi)
- B) `ef_search`'i artırmak
- C) `probes` değerini artırmak
- D) Daha fazla `tenant_id` filtresi eklemek

<details><summary>Cevap</summary>

**A.** `halfvec` (2 bayt/boyut) vector ve index boyutunu yaklaşık yarıya indirir; boyut düşürmek de benzer etki yapar. `ef_search`/`probes` recall'ı değiştirir, boyutu değil.
</details>

### 14. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) PgBouncer transaction pooling modunda oturum düzeyi `SET` yerine transaction içinde `SET LOCAL` kullanmak daha güvenlidir. | ☐ | ☐ |
| b) Uygulama örnek sayısı × `MaxPoolSize`, sunucu `max_connections` sınırını aşmamalıdır. | ☐ | ☐ |
| c) Microsoft Entra ile bağlanıldığında token süresi dolsa bile açık havuz bağlantıları otomatik olarak yeni kimlikle yenilenir; istemci kodu bir şey yapmaz. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Token (yaklaşık bir saatlik) süresi dolmadan istemcide yenilenmelidir (örn. Npgsql `UsePeriodicPasswordProvider`); yeni bağlantılar yeni token'la açılır.
</details>

### 15. Kod tamamlama (Python)
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

### 16. Vaka (case study)
**Senaryo:** *Northwind Legal*, sözleşme parçalarını PostgreSQL Flexible Server'da saklıyor: `contracts(id, tenant_id, clause_text, embedding vector(3072))`, 8 milyon satır. Sorgular her zaman `tenant_id` filtreli top-5 benzer madde arar. Gözlemler: (1) ilk kurulan IVFFlat index'i **boş tabloda** oluşturulmuştu ve recall düşük; (2) kiracıların çoğu küçük, 3 kiracı çok büyük; (3) 3072 boyutlu vector'ler depolamayı ve bellek ihtiyacını şişiriyor; (4) 120 uygulama örneği doğrudan 5432 portuna bağlanıp ayrı ayrı açık bağlantı tutuyor ve sunucu arada `too many connections` veriyor.

**Soru A.** Index stratejisini düzeltin. **Soru B.** Filtreli aramada sonuç eksikliği riskini nasıl yönetirsiniz? **Soru C.** Boyut/bellek ve bağlantı sorunlarını nasıl çözersiniz?

<details><summary>Cevap</summary>

**A.** IVFFlat boş tabloda kümeleri veriden öğrenemez; ya veri yüklendikten sonra yeniden kurulmalı ya da (tercihen, sürekli güncellenen veri için) **HNSW** (`vector_cosine_ops`, sorguda `<=>`) kullanılmalı. HNSW boş tabloda kurulabilir ve veriyle birlikte güncellenir; build için `maintenance_work_mem` artırılır. 3072 boyut doğrudan index'lenebilir vector sınırını aşabilir; bu yüzden **`halfvec` ifade index'i** veya boyut düşürme gerekir (bkz. C).

**B.** `tenant_id` filtresi HNSW adaylarını sonradan eler (over-filtering). Çözüm: `hnsw.iterative_scan = relaxed_order` (pgvector 0.8.0+; sıra önemliyse `strict_order`), gerekirse `ef_search` artırma; **3 büyük kiracı için `PARTITION BY LIST (tenant_id)`** (her partition kendi HNSW'sine sahip, kiracı filtresi tek partition'a iner), küçük kiracılar için B-tree(`tenant_id`) ile daraltıp exact sıralama da yeterli olabilir.

**C.** Boyut/bellek: embedding modelinde `dimensions` parametresiyle (örn. 1024) boyut düşürmek ve/veya `halfvec` (≈ yarı depolama/index). Index'in belleğe sığması için compute/RAM boyutlandırılır. Bağlantı: `NpgsqlDataSource`/psycopg havuzu singleton, örnek başına küçük `MaxPoolSize`; **PgBouncer'ı** (`pgbouncer.enabled`) etkinleştirip uygulamaları **6432**'ye yönlendirmek; `SET LOCAL` kullanmak. Kimlik: parola yerine Entra + token yenileme.
</details>

---

**Puanlama:** 13-16 → hazırsın · 10-12 → README 3.2 (operatörler), 3.3 (HNSW/IVFFlat) ve 3.5 (filtre/iterative scan) bölümlerini tekrar et · < 10 → konuyu baştan çalış, 3.7 laboratuvarında `EXPLAIN` planlarını kendin karşılaştır.
