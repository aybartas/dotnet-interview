# C2.4 Cosmos DB for NoSQL: SDK, Partitioning, Indexing, Consistency, RU — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Develop AI solutions by using Azure data management services* (%30-35) · 18 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, kod/CLI tamamlama, senaryo, vaka); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 15/18** (≥ %80).

---

### 1. Tek seçim
Bir IoT çözümünde cihaz telemetrisi Cosmos DB container'ına yazılıyor. 50.000 cihaz var, sorguların çoğu "belirli bir cihazın son 1 saatlik verisi". Hangi partition key **en uygundur**?

- A) `/deviceType` (5 farklı değer)
- B) `/deviceId`
- C) `/timestamp`
- D) `/region` (3 farklı değer)

<details><summary>Cevap</summary>

**B.** `/deviceId` yüksek cardinality'e sahiptir, yük cihazlara eşit dağılır ve en sık sorgu tek partition'a gider. A ve D düşük cardinality (hot partition); C ise tüm yeni yazmaları "bugünün" partition'ına yığar ve cihaz sorgularını cross-partition yapar.
</details>

### 2. Tek seçim
Bir container 8 ay önce `/status` partition key'i ile oluşturuldu ve hot partition nedeniyle 429 alıyor. Ekip partition key'i `/customerId` yapmak istiyor. Doğru yaklaşım hangisidir?

- A) `az cosmosdb sql container update --partition-key-path /customerId`
- B) Yeni container'ı `/customerId` ile oluşturup veriyi kopyalamak
- C) Container'ın indexing policy'sini değiştirmek
- D) Autoscale'i açmak, partition key otomatik değişir

<details><summary>Cevap</summary>

**B.** Partition key path'i container oluşturulduktan sonra değiştirilemez; yeni container + veri taşıma (ör. change feed ile kopyalama) gerekir. Autoscale hot partition'ı çözmez; indexing policy dağılımı etkilemez.
</details>

### 3. Tek seçim
Bir API, sipariş detay sayfası için sipariş `id`'sini ve `customerId` (partition key) değerini zaten biliyor. Hangi çağrı **en düşük RU** maliyetlidir?

- A) `SELECT * FROM c WHERE c.id = @id` sorgusu, `GetItemQueryIterator` ile
- B) `ReadItemAsync<Order>(id, new PartitionKey(customerId))`
- C) `SELECT VALUE COUNT(1) FROM c WHERE c.id = @id`
- D) Cross-partition sorgu + `TOP 1`

<details><summary>Cevap</summary>

**B.** Point read, id + partition key ile doğrudan adreslenir; 1 KB'lık item için yaklaşık 1 RU ile en ucuz okumadır. Sorgular indeks aramasını ve sorgu motorunu devreye sokar; cross-partition olanlar daha da pahalıdır.
</details>

### 4. Kod tamamlama (C#)
Aşağıdaki kodda tek bir partition içinde, atomik olarak iki işlem yapılmak isteniyor. Boşluğu doldurun.

```csharp
TransactionalBatchResponse r = await container
    .__________(new PartitionKey("customer-42"))
    .CreateItem(order2)
    .ReplaceItem(order1.Id, order1)
    .ExecuteAsync();
```

- A) `CreateBulkOperation`
- B) `CreateTransactionalBatch`
- C) `GetItemQueryIterator`
- D) `CreateTransaction`

<details><summary>Cevap</summary>

**B.** `container.CreateTransactionalBatch(PartitionKey)` — batch'teki tüm işlemler aynı partition key değerini paylaşmalı ve atomik uygulanır (en çok 100 işlem). Diğer adlar SDK'da bu amaçla yoktur; bulk için `AllowBulkExecution` + paralel görevler kullanılır.
</details>

### 5. Evet / Hayır
Cosmos DB SDK kullanımı hakkında:

| İfade | Evet | Hayır |
|---|---|---|
| a) `CosmosClient` uygulama yaşam süresi boyunca tek örnek (singleton) olarak kullanılmalıdır. | ☐ | ☐ |
| b) Transactional batch, farklı partition key değerlerine sahip item'ları tek atomik işlemde yazabilir. | ☐ | ☐ |
| c) SDK, 429 yanıtlarını varsayılan olarak belirli sayıda otomatik yeniden dener. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Client bağlantı havuzunu, metadata önbelleğini ve retry politikasını taşır; her istekte yaratmak zararlıdır. Batch yalnızca tek logical partition içinde atomiktir. SDK 429'da `retry-after` kadar bekleyip yeniden dener (varsayılan 9 deneme / 30 sn toplam bekleme).
</details>

### 6. Tek seçim
Bir sosyal uygulamada kullanıcı bir gönderi yazdıktan hemen sonra kendi profil sayfasını yeniler ve gönderiyi **mutlaka** görmelidir. Okuma RU maliyeti mümkün olduğunca düşük olmalı. Hangi consistency level seçilmelidir?

- A) Strong
- B) Bounded Staleness
- C) Session
- D) Eventual

<details><summary>Cevap</summary>

**C.** Session, aynı oturumda read-your-writes garantisi verir ve okuma RU'su Strong/Bounded'ın yarısıdır. Strong gereksiz pahalıdır; Eventual kendi yazdığını görmeyi garanti etmez.
</details>

### 7. Çoklu seçim
Hangi ifadeler **Strong** consistency için doğrudur? (İki seçenek)

- A) Okuma işlemleri Session'a göre yaklaşık 2× RU tüketir.
- B) En son commit edilmiş yazmayı garanti eder (linearizability).
- C) İstek bazında Strong'a *yükseltilebilir* (hesap Eventual iken).
- D) Varsayılan consistency level'dır.

<details><summary>Cevap</summary>

**A ve B.** C yanlış: istek bazında consistency yalnızca gevşetilebilir, hesap varsayılanından güçlüsüne çıkılamaz. D yanlış: varsayılan Session'dır.
</details>

### 8. Sıralama
Aşağıdaki consistency level'ları **en güçlü garantiden en zayıfa** sıralayın: Eventual · Session · Strong · Consistent Prefix · Bounded Staleness

<details><summary>Cevap</summary>

**Strong → Bounded Staleness → Session → Consistent Prefix → Eventual.** Güç azaldıkça gecikme ve (Strong/Bounded için) okuma RU'su düşer.
</details>

### 9. Senaryo
Bir sorgu şu ifadeyi içeriyor ve `The order by query does not have a corresponding composite index` benzeri hata veriyor:

```sql
SELECT c.id, c.total FROM c ORDER BY c.customerId ASC, c.orderDate DESC
```

Ne yapılmalıdır?

- A) Container throughput'unu artırmak
- B) `(customerId ASC, orderDate DESC)` için composite index eklemek
- C) Consistency'yi Eventual yapmak
- D) `indexingMode`'u `none` yapmak

<details><summary>Cevap</summary>

**B.** Birden çok alanla `ORDER BY` composite index gerektirir; sıra ve yönler sorgudakiyle aynı (veya tam ters) olmalıdır. Throughput/consistency bu hatayı çözmez; `none` hiçbir sorguyu çalıştırmaz.
</details>

### 10. Kod tamamlama (indexing policy)
Ekip, yazma RU'sunu düşürmek için yalnızca `customerId`, `status` ve `orderDate` alanlarını indekslemek istiyor. Boşluğu doldurun:

```json
{
  "indexingMode": "consistent",
  "includedPaths": [
    { "path": "/customerId/?" }, { "path": "/status/?" }, { "path": "/orderDate/?" }
  ],
  "excludedPaths": [ { "path": "__________" } ]
}
```

- A) `/_etag/?`
- B) `/*`
- C) `/customerId/*`
- D) `/?`

<details><summary>Cevap</summary>

**B.** `"/*"` hariç tutulur ve yalnızca `includedPaths`'teki yollar indekslenir (allow-list). Böylece diğer alanlar için indeks girişi yazılmaz ve yazma RU'su düşer.
</details>

### 11. Çoklu seçim
Bir container'ın **yazma RU maliyetini** düşürmek için hangi iki eylem uygundur?

- A) Indexing policy'yi yalnızca sorgulanan yollarla sınırlamak
- B) Gereksiz büyük alanları (ör. uzun açıklama metni) indeks dışı bırakmak
- C) Consistency'yi Strong yapmak
- D) Her sorguya `SELECT *` yazmak

<details><summary>Cevap</summary>

**A ve B.** Her indekslenen yol yazmada ek RU demektir. Strong okuma maliyetini artırır (yazmayı değil, ve düşürmez); `SELECT *` okuma tarafını etkiler.
</details>

### 12. CLI tamamlama
Aşağıdaki komut, değişken yüklü bir workload için **autoscale** container oluşturmalıdır (en çok 4000 RU/s'ye çıksın). Boşluğu doldurun:

```bash
az cosmosdb sql container create --account-name $ACC --resource-group $RG \
  --database-name shop --name orders --partition-key-path "/customerId" \
  --__________ 4000
```

- A) `--throughput`
- B) `--max-throughput`
- C) `--autoscale-rus`
- D) `--serverless`

<details><summary>Cevap</summary>

**B.** `--max-throughput` autoscale için Tmax'ı belirler (sistem Tmax'ın %10'u ile %100'ü arasında ölçeklenir). `--throughput` manual provisioned içindir. Serverless container değil **hesap** düzeyinde `EnableServerless` capability'siyle seçilir.
</details>

### 13. Senaryo
Bir ekip, geliştirme ortamı için seyrek kullanılan küçük bir uygulama yazıyor; trafik günde birkaç yüz istek, çoğu zaman sıfır. En düşük maliyet için hangi throughput modeli en uygundur?

- A) Manual provisioned, 10.000 RU/s
- B) Autoscale, Tmax 10.000 RU/s
- C) Serverless
- D) Database düzeyi shared throughput, 100.000 RU/s

<details><summary>Cevap</summary>

**C.** Serverless'ta rezerve throughput yoktur; yalnızca tüketilen RU ve depolama ödenir. Seyrek/dev-test için idealdir. Diğerleri boşta bile faturalanır (autoscale'de taban %10 Tmax).
</details>

### 14. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Sorguda partition key filtresi verilmezse sorgu cross-partition olur ve RU genellikle artar. | ☐ | ☐ |
| b) `x-ms-request-charge` başlığı / `RequestCharge` özelliği işlemin RU maliyetini verir. | ☐ | ☐ |
| c) Bir container'daki iki farklı partition key değeri aynı `id`'ye sahip item'lar içeremez. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Benzersizlik `(partition key değeri, id)` çiftidir; farklı partition'larda aynı `id` bulunabilir.
</details>

### 15. Kod tamamlama (Python)
Aşağıdaki Python kodu, partition key değerini bildiği bir item'ı **en ucuz yolla** okumalıdır. Boşluğu doldurun:

```python
item = container.__________(item="o-1", partition_key="customer-42")
```

- A) `query_items`
- B) `read_item`
- C) `get_item`
- D) `list_items`

<details><summary>Cevap</summary>

**B.** `ContainerProxy.read_item(item, partition_key)` point read'dir. `query_items` sorgu motorunu kullanır; `get_item`/`list_items` `azure-cosmos` API'sinde yoktur.
</details>

### 16. Tek seçim
Bir uygulama, Cosmos DB'ye hesap anahtarı kullanmadan, **managed identity** ile bağlanacak. Doğru yaklaşım hangisidir?

- A) Azure RBAC'ta kimliğe `Contributor` rolü vermek ve `new CosmosClient(endpoint, new DefaultAzureCredential())` kullanmak
- B) Cosmos DB **data-plane** rolü (ör. *Cosmos DB Built-in Data Contributor*) atamak (`az cosmosdb sql role assignment create`) ve `DefaultAzureCredential` kullanmak
- C) Connection string'i Key Vault yerine environment variable'a koymak
- D) Hesapta firewall'u kapatmak

<details><summary>Cevap</summary>

**B.** Cosmos veri düzlemi, Azure RBAC'tan ayrı kendi SQL role assignment sistemini kullanır. Management-plane `Contributor` rolü veri okuma/yazma yetkisi vermez (A yanlış). C ve D kimlik doğrulama sorununu çözmez.
</details>

### 17. Sıralama
Aşağıdaki işlemleri tipik **RU maliyeti en düşükten en yükseğe** sıralayın (aynı veri, varsayılan ayarlar): (1) tek partition filtreli sorgu · (2) cross-partition `SELECT *` sorgu · (3) 1 KB'lık item point read

<details><summary>Cevap</summary>

**(3) point read → (1) tek partition sorgu → (2) cross-partition sorgu.** Mutlak değerler veri setine göre değişir; `RequestCharge` ile ölç.
</details>

### 18. Vaka (case study)
**Senaryo:** *Contoso Shop*, Cosmos DB for NoSQL üzerinde `orders` container'ını `/status` partition key'i ile çalıştırıyor (`new/paid/shipped/cancelled`). Hesap Session consistency'de, container 10.000 RU/s manual. Kampanya günlerinde `paid` partition'ına yazmalar yığılıyor ve API 429 alıyor; Application Insights'ta toplam RU kullanımının yalnızca %20 olduğu görülüyor. En sık sorgu: *"müşterinin son 20 siparişi"*, `SELECT TOP 20 * FROM c WHERE c.customerId = @c ORDER BY c.orderDate DESC` (cross-partition çalışıyor). Siparişin detayı da `SELECT * FROM c WHERE c.id = @id` sorgusuyla alınıyor. Ayrıca farklı bir worker servisi, API'nin yazdığı siparişi hemen okuyup bazen "bulunamadı" alıyor (iki servis ayrı `CosmosClient` kullanıyor).

**Soru A.** 429'ların kök nedeni nedir ve kalıcı çözüm nedir?

**Soru B.** "Son 20 sipariş" ve "detay" sorgularını nasıl iyileştirirsiniz?

**Soru C.** Worker'ın "bulunamadı" sorununun nedeni ve çözümü nedir?

<details><summary>Cevap</summary>

**A.** **Hot partition.** `/status` düşük cardinality'dir; `paid` logical partition'ı tek physical partition'a düşer ve o partition'ın RU/s sınırı (toplam 10.000 RU/s'nin yalnızca bir kısmı) dolar — toplam kullanım %20 olsa bile 429 alınır. Autoscale/RU artırmak sorunu çözmez. Kalıcı çözüm: **yeni container'ı `/customerId`** (yüksek cardinality, sorgu desenine uygun) ile oluşturup veriyi taşımak (partition key yerinde değiştirilemez).

**B.** Yeni key ile "son 20 sipariş" **tek partition sorgusu** olur; `ORDER BY` için `(customerId ASC, orderDate DESC)` **composite index** eklenir (filtre `customerId` eşitlik + sıralama `orderDate`; composite yoksa RU yüksek kalır/çoklu alan ORDER BY hata verir). Detay için sorgu yerine **`ReadItemAsync(id, new PartitionKey(customerId))`** (point read); projection ile gereksiz alanlar çekilmez.

**C.** Session consistency'de read-your-writes garantisi **aynı client/session token** içindir; API ve worker ayrı `CosmosClient` kullandığı için worker yazılanı henüz görmeyebilir. Çözüm: session token'ı API'den worker'a iletmek (`ItemRequestOptions.SessionToken`) — ya da gereksinim gerçekten küresel taze okuma ise (pahalı) Strong/Bounded; ya da worker'ı olay (mesaj) akışına bağlayıp item'ı payload'dan okumak. Strong'u "kolay çözüm" diye seçmek okuma RU'sunu ~2× yapar.
</details>

---

**Puanlama:** 15-18 → hazırsın · 12-14 → README 3.1 (partition key), 3.4 (indexing) ve 3.5 (consistency) tablolarını tekrar et · < 12 → konuyu baştan çalış, 3.7 laboratuvarında `RequestCharge` ölçümlerini kendin yap.
