# C2.5 Cosmos DB ile Vector Search ve Change Feed — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Develop AI solutions by using Azure data management services* (%30-35) · 16 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, kod tamamlama, senaryo, vaka); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 13/16** (≥ %80).

---

### 1. Tek seçim
Bir ekip 3 milyon doküman parçasının embedding'ini Cosmos DB for NoSQL'de saklayacak ve düşük gecikmeyle top-k arama yapacak. Hangi vector index türü **en uygundur**?

- A) `flat`
- B) `quantizedFlat`
- C) `diskANN`
- D) Normal range index

<details><summary>Cevap</summary>

**C.** `diskANN`, milyonlarca vector için düşük gecikme/RU sunan yaklaşık (ANN) indekstir. `flat` brute-force'tur ve bu ölçekte pahalı; `quantizedFlat` orta ölçeğe uygundur; range index vector araması için kullanılmaz.
</details>

### 2. Tek seçim
Bir geliştirici 1536 boyutlu embedding üreten bir model kullanıyor; ancak container'ın vector embedding policy'sinde `"dimensions": 768` yazıyor. Item yazılınca ne olur?

- A) Vector otomatik olarak 768'e kırpılır
- B) Yazma/sorgu, boyut uyuşmazlığı nedeniyle hata verir
- C) Cosmos policy'yi otomatik 1536'ya günceller
- D) Vector indekslenmez ama yazma başarılı olur ve sessizce görmezden gelinir

<details><summary>Cevap</summary>

**B.** Vector uzunluğu policy'deki `dimensions` ile birebir eşleşmelidir. Cosmos kırpmaz, policy'yi kendiliğinden güncellemez; vector policy zaten sonradan değiştirilemez, bu yüzden doğru çözüm yeni container'dır.
</details>

### 3. Kod tamamlama (SQL)
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

### 4. Çoklu seçim
Vector alanı içeren bir container için aşağıdakilerden hangileri **doğrudur**? (İki seçenek)

- A) Vector yolu, indexing policy'de `excludedPaths`'e eklenerek range index'ten çıkarılmalıdır.
- B) `vectorIndexes[].path`, vector embedding policy'deki `path` ile aynı olmalıdır.
- C) Vector embedding policy her zaman container oluşturulduktan sonra değiştirilebilir.
- D) `flat` index tüm boyut sınırlarında `diskANN`'dan daha ölçeklenebilirdir.

<details><summary>Cevap</summary>

**A ve B.** C yanlış: vector policy container oluşturulurken verilir ve sonradan değiştirilemez (2026 başı itibarıyla). D yanlış: `flat` brute-force'tur, boyut sınırı daha düşüktür ve büyük veride ölçeklenmez.
</details>

### 5. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) `quantizedFlat` ve `diskANN` indeksleri için çok az vector varsa (kabaca < 1000) sistem tam tarama yapabilir. | ☐ | ☐ |
| b) `flat` index %100 recall (tam doğruluk) sağlar. | ☐ | ☐ |
| c) `distanceFunction` seçenekleri arasında `cosine`, `dotproduct` ve `euclidean` vardır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Evet.** Nicemlemenin doğruluğu için yeterli vector gerekir; `flat` brute-force olduğundan tam sonuç verir (karşılığında büyük veride pahalıdır); üç mesafe fonksiyonu desteklenir.
</details>

### 6. Senaryo
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

### 7. Sıralama
Aşağıdaki adımları, bir **change feed processor** değişiklik işlerken doğru **sırada** sıralayın: (1) handler başarıyla döner (2) lease'teki checkpoint ilerler (3) processor bir lease'e sahip olur (4) feed'den değişiklik grubu okunur ve handler'a verilir

<details><summary>Cevap</summary>

**3 → 4 → 1 → 2.** Processor önce partition aralığının lease'ini alır, değişiklikleri okuyup handler'a verir; handler hatasız dönünce checkpoint ilerler. Hata fırlatılırsa grup yeniden teslim edilir (at-least-once).
</details>

### 8. Tek seçim
Change feed processor'ın **lease container**'ının işlevi nedir?

- A) Silinen item'ların yedeğini tutmak
- B) Her partition aralığının sahipliğini ve checkpoint'ini (continuation) saklayıp worker örnekleri arasında paylaştırmak
- C) Embedding'leri önbelleğe almak
- D) Container'ın indexing policy'sini saklamak

<details><summary>Cevap</summary>

**B.** Lease'ler hem yük dağıtımını (hangi worker hangi aralığı işler) hem de kaldığı yerden devam etmeyi (checkpoint) sağlar. Silinen item yedekleme, önbellek veya indexing policy ile ilgisi yoktur.
</details>

### 9. Senaryo
Varsayılan (latest version) change feed ile çalışan bir processor, kaynak container'dan **silinen** ürünleri arama dizininden çıkarmak için kullanılıyor ama silmeler hiç tetiklenmiyor. Hangi iki çözüm doğrudur? (İki seçenek)

- A) Silme yerine `deleted: true` yazıp (soft delete) `ttl` ile sonra temizlemek
- B) **All versions and deletes** change feed modunu kullanmak (continuous backup gerektirir)
- C) Lease container'ı silip yeniden oluşturmak
- D) Processor'ın poll aralığını azaltmak

<details><summary>Cevap</summary>

**A ve B.** Latest version modu silmeleri yakalamaz. C ve D bu durumu değiştirmez; lease silmek yalnızca baştan işlemeye neden olur, poll aralığı gecikmeyi etkiler.
</details>

### 10. Kod tamamlama (C#)
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

### 11. Senaryo
Bir worker, ürün güncellendiğinde yeni embedding'i hesaplayıp **aynı item'ı** `ReplaceItem` ile güncelliyor. Birkaç dakika sonra RU tüketimi fırlıyor ve model çağrı sayısı sürekli artıyor. En olası neden ve çözüm hangisidir?

- A) Lease container çok küçük; RU artırılmalı
- B) Worker'ın kendi yazması change feed'e yeniden düşüyor (sonsuz döngü); içerik hash'i/versiyon alanıyla değişmemiş içerik için yazmayı atla
- C) Consistency Strong olmalı
- D) Change feed ters sırada okunuyor; poll aralığı artırılmalı

<details><summary>Cevap</summary>

**B.** Her yazma yeni bir değişiklik olayı üretir. İşleyici idempotent olmalı: `embeddingSourceHash` gibi bir alanla kaynak metin değişmediyse embedding'i yeniden üretme/yazma. Diğer şıklar döngüyü çözmez.
</details>

### 12. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Change feed, aynı logical partition içindeki değişikliklerin sırasını korur. | ☐ | ☐ |
| b) Change feed processor, her değişikliği tam olarak bir kez (exactly-once) teslim etmeyi garanti eder. | ☐ | ☐ |
| c) Aynı monitored container'ı iki farklı iş için okumak istiyorsan farklı processor adları kullanabilirsin. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Teslim at-least-once'tır; işleyici idempotent olmalı. Farklı processor adları (lease prefix'i) bağımsız tüketici grupları oluşturur.
</details>

### 13. Kod tamamlama (Python)
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

### 14. Tek seçim
Bir ekip, ürün açıklaması değiştiğinde embedding'i **olay güdümlü serverless** olarak yenilemek istiyor ve worker altyapısını işletmek istemiyor. Hangisi en uygundur?

- A) Zamanlayıcıyla her gece tüm container'ı taramak
- B) Azure Functions **Cosmos DB trigger**'ı
- C) Container'ı polling eden bir Logic App
- D) İstemci uygulamada embedding'i hesaplayıp yazmak

<details><summary>Cevap</summary>

**B.** Cosmos DB trigger, change feed processor'ı platform üzerinde çalıştırır (lease yönetimi, ölçek dahil; → C2.8). Gece taraması taze olmayan embedding bırakır; istemcide hesaplama güvenlik/tutarlılık sorunları yaratır.
</details>

### 15. Çoklu seçim
Hangi ifadeler vector tabanlı RAG için **embedding modelini değiştirme** (1536 → 3072 boyut) konusunda doğrudur? (İki seçenek)

- A) Yeni boyut için yeni vector policy gerekir; pratikte yeni bir container oluşturulur.
- B) Eski ve yeni modelin vector'leri aynı alanda karıştırılırsa benzerlik skorları anlamsızlaşır.
- C) Cosmos mevcut vector'leri otomatik yeni modele dönüştürür.
- D) Yalnızca `distanceFunction` değiştirilirse tüm vector'ler uyumlu hâle gelir.

<details><summary>Cevap</summary>

**A ve B.** Farklı modellerin uzayları karşılaştırılamaz; yeniden embedding gerekir. Bu iş de bir change feed/toplu iş ile yapılır. Cosmos embedding üretmez/dönüştürmez.
</details>

### 16. Vaka (case study)
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
