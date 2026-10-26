# C2.7 Azure Managed Redis: Cache ve Vector Index — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Develop AI solutions by using Azure data management services* (%30-35) · 16 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, komut/kod tamamlama, senaryo, vaka); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 13/16** (≥ %80).

---

### 1. Tek seçim
Bir ürün API'si veritabanından okuduğu veriyi Redis'e yazıyor; yazılan her anahtara 10 dakika sonra silinmesi için süre veriliyor. Bu mekanizmanın adı nedir?

- A) Eviction
- B) Expiration (TTL)
- C) Persistence
- D) Replication

<details><summary>Cevap</summary>

**B.** Anahtara verilen süre dolunca silinmesi **expiration/TTL**'dir (`SET key value EX 600`). Eviction, bellek dolduğunda sunucunun eviction policy'ye göre anahtar silmesidir.
</details>

### 2. Tek seçim
Cache-aside deseninde ürün fiyatı güncellendi. Okuyucuların eski fiyatı görme süresini **en aza indirmek** için doğru işlem sırası hangisidir?

- A) Cache'te anahtarı sil, sonra veritabanını güncelle
- B) Veritabanını güncelle, sonra cache'te anahtarı sil (TTL güvenlik ağı olarak kalır)
- C) Sadece cache'i güncelle, veritabanını gece güncelle
- D) Hiçbir şey yapma, TTL'in dolmasını bekle

<details><summary>Cevap</summary>

**B.** Önce kaynak veriyi yaz, sonra cache'i invalidate et. A seçeneğinde silme ile yazma arasında bir okuyucu eski veriyi cache'e geri yazabilir. C veri kaybı riskidir (cache kaynak deposu değildir). D sadece TTL'e güvenir; bayat pencere büyür.
</details>

### 3. Çoklu seçim
Aşağıdakilerden hangileri bir cache'te **invalidation** stratejisidir? (İki seçenek)

- A) Veritabanına yazdıktan sonra ilgili anahtarı silmek
- B) Anahtara sürüm numarası ekleyip (`product:42:v7`) sürümü artırmak
- C) Cache'in clustering policy'sini değiştirmek
- D) Modül eklemek

<details><summary>Cevap</summary>

**A ve B.** Yazınca sil ve versiyonlu anahtar klasik invalidation yöntemleridir (üçüncüsü olay tabanlı silme). Clustering policy ve modüller invalidation ile ilgili değildir.
</details>

### 4. Senaryo
Bir ana sayfa verisi Redis'te 5 dakika TTL ile tutuluyor. TTL dolduğu anda yüzlerce eşzamanlı istek aynı anda veritabanına gidip veritabanını zorluyor. Bu sorunun adı ve uygun çözümü nedir?

- A) Cache stampede; tek uçuş (single-flight) kilidi, TTL jitter veya HybridCache
- B) Eviction storm; `noeviction` kullanmak
- C) Cold start; kaynak ekleme
- D) Split brain; clustering policy'yi Enterprise yapmak

<details><summary>Cevap</summary>

**A.** Cache stampede (thundering herd) önlemleri: aynı anahtar için tek bir yükleyici (kilit / HybridCache `GetOrCreateAsync`), TTL'e rastgele jitter ve erken yenileme. Diğer seçenekler farklı sorunlardır.
</details>

### 5. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Eviction policy, belleğin dolması durumunda hangi anahtarların silineceğini belirler. | ☐ | ☐ |
| b) `noeviction` politikasında bellek dolarsa yeni yazmalar hata verebilir. | ☐ | ☐ |
| c) TTL ile eviction aynı mekanizmadır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** TTL uygulamanın verdiği süreye göre silmedir; eviction bellek baskısıyla sunucu tarafında tetiklenir. RediSearch/vector kullanan cache'lerde yalnızca `NoEviction` desteklendiği için TTL ve boyut izleme şarttır.
</details>

### 6. Senaryo
Bir ekip Azure Managed Redis'te vector benzerlik araması kullanmak istiyor ve cache'i zaten Flash Optimized katmanında oluşturmuş. Cache hâlen vector index oluşturmuyor. Sorunu çözmek için en doğru yaklaşım nedir?

- A) Portal'dan RediSearch modülünü sonradan eklemek
- B) `FT.CREATE`'i `DIALECT 3` ile çalıştırmak
- C) RediSearch modülü destekleyen bir katmanda (Memory Optimized/Balanced/Compute Optimized) modülü **oluşturma sırasında seçerek** yeni bir cache oluşturmak
- D) Eviction policy'yi `allkeys-lru` yapmak

<details><summary>Cevap</summary>

**C.** Modüller yalnızca oluşturma sırasında seçilir ve sonradan eklenemez; RediSearch, Flash Optimized katmanında desteklenmez (güncel dokümanı doğrula). Vector için ayrıca Enterprise clustering policy ve `NoEviction` gerekir; `allkeys-lru` bu durumda desteklenmez.
</details>

### 7. Tek seçim
RediSearch modülünü kullanacak bir Azure Managed Redis örneği için hangi yapılandırma kombinasyonu **gereklidir**?

- A) OSS clustering policy + `allkeys-lru`
- B) Enterprise clustering policy + `NoEviction`
- C) OSS clustering policy + `volatile-ttl`
- D) Herhangi bir clustering policy + herhangi bir eviction policy

<details><summary>Cevap</summary>

**B.** RediSearch Enterprise clustering policy ve `NoEviction` gerektirir; ikisi de oluşturma sırasında belirlenir.
</details>

### 8. Komut tamamlama (Redis)
Aşağıdaki komut, 1536 boyutlu embedding'ler için cosine mesafeli HNSW vector indexi oluşturmalıdır. Boşluğu doldurun:

```text
FT.CREATE idx:docs ON HASH PREFIX 1 doc: SCHEMA
    title TEXT
    embedding VECTOR HNSW __ TYPE FLOAT32 DIM 1536 DISTANCE_METRIC COSINE M 16 EF_CONSTRUCTION 200
```

- A) `5`
- B) `6`
- C) `10`
- D) `12`

<details><summary>Cevap</summary>

**C.** Sayı, algoritma özniteliklerinin **token** sayısıdır (ad ve değerler ayrı sayılır): `TYPE FLOAT32` (2) + `DIM 1536` (2) + `DISTANCE_METRIC COSINE` (2) + `M 16` (2) + `EF_CONSTRUCTION 200` (2) = **10**. `5` çift sayısıdır (yanlış birim); `6` yalnızca zorunlu üç özniteliği kapsar.
</details>

### 9. Komut tamamlama (Redis)
Aşağıdaki komut yalnızca zorunlu öznitelikleri veren bir **FLAT** vector alanı tanımlar. Boşluğu doldurun:

```text
embedding VECTOR FLAT __ TYPE FLOAT32 DIM 768 DISTANCE_METRIC COSINE
```

- A) `3`
- B) `4`
- C) `6`
- D) `8`

<details><summary>Cevap</summary>

**C.** `TYPE FLOAT32 DIM 768 DISTANCE_METRIC COSINE` = 6 token (3 ad-değer çifti).
</details>

### 10. Tek seçim
`FT.SEARCH` ile KNN sorgusu çalıştırılıyor:

```text
FT.SEARCH idx:docs "*=>[KNN 3 @embedding $vec AS score]" PARAMS 2 vec <blob> SORTBY score DIALECT 2
```

COSINE metriği ile `score` değeri **0.02** olan sonuç hakkında ne söylenebilir?

- A) Skor bir benzerliktir; 0.02 çok düşük benzerlik demektir
- B) Skor bir **mesafedir**; küçük değer, sorguya çok benzer anlamına gelir
- C) Skor negatif olmalıdır
- D) Sonuç geçersizdir; skor en az 1 olmalıdır

<details><summary>Cevap</summary>

**B.** RediSearch'te `KNN ... AS score` bir **mesafe** döndürür (cosine mesafesi = 1 − cosine benzerliği); küçük = benzer. Bu yüzden eşik karşılaştırması `score <= eşik` biçimindedir.
</details>

### 11. Senaryo
Bir sohbet botunun LLM maliyeti yüksek; kullanıcılar aynı soruyu farklı cümlelerle sürekli soruyor ("İade süresi kaç gün?" / "Ürünü kaç gün içinde iade edebilirim?"). En uygun Redis tabanlı çözüm hangisidir?

- A) Tam metin eşleşmesiyle (string key) cache
- B) Soru embedding'i + vector index ile **semantic cache** (eşik altı mesafede kayıtlı cevabı döndür, TTL ile)
- C) Tüm soruları kalıcı olarak Redis'e yazıp TTL kullanmamak
- D) Eviction'ı kapatmak

<details><summary>Cevap</summary>

**B.** Anlamca benzer sorular farklı string'lerdir; bire bir anahtar eşleşmesi onları yakalayamaz. Semantic cache embedding benzerliğiyle yakalar. TTL, cevapların bayatlamasını önler ve belleği sınırlar; eşik ölçülerek ayarlanmalıdır.
</details>

### 12. Sıralama
Semantic cache'te **miss** (eşik dışı) durumunda doğru adım sırasını belirleyin: (1) LLM'i çağır (2) sorunun embedding'ini üret (3) cevabı embedding ile birlikte TTL'le Redis'e yaz (4) `KNN 1` ile en yakın kaydı ara ve mesafeyi eşikle karşılaştır

<details><summary>Cevap</summary>

**2 → 4 → 1 → 3.** Embedding üret, en yakın komşuyu ara; mesafe eşikten büyükse LLM'i çağır ve yeni cevabı TTL ile kaydet. Hit durumunda akış 4'ten sonra kayıtlı cevabı döndürüp biter.
</details>

### 13. Kod tamamlama (C#)
Managed Redis'e parolasız, **Entra** kimliğiyle bağlanın. Boşluğu doldurun:

```csharp
ConfigurationOptions options = ConfigurationOptions.Parse("myredis.westeurope.redis.azure.net:10000");
await options.__________(new DefaultAzureCredential());
ConnectionMultiplexer mux = await ConnectionMultiplexer.ConnectAsync(options);
```

- A) `ConfigureForAzureWithTokenCredentialAsync`
- B) `UseManagedIdentity`
- C) `SetPasswordFromToken`
- D) `AddEntraAuthentication`

<details><summary>Cevap</summary>

**A.** `Microsoft.Azure.StackExchangeRedis` paketinin uzantısı token'ı alır ve yeniler. Token'ı elle `Password` alanına koymak yerine bu yöntem kullanılmalıdır. `ConnectionMultiplexer` uygulama ömrü boyunca tek (singleton) olmalıdır.
</details>

### 14. Çoklu seçim
Hangi iki senaryoda vector verisini **Redis'te** tutmak, kalıcı kaynak depoya (Cosmos/PostgreSQL) göre daha uygundur?

- A) LLM yanıtları için düşük gecikmeli, TTL'li semantic cache
- B) Kullanıcı oturumuna özel, kısa ömürlü ve yeniden üretilebilir öneri vector'leri
- C) 200 milyon dokümanlık kalıcı bilgi tabanı, ACID ve JOIN gereksinimi
- D) Denetim için yıllarca saklanması gereken sözleşme embedding'leri

<details><summary>Cevap</summary>

**A ve B.** Redis düşük gecikmeli, geçici ve yeniden üretilebilir veri için uygundur. C ve D kalıcı kaynak veri gerektirir: bellek maliyeti ve cache doğası nedeniyle Cosmos DB veya PostgreSQL (pgvector) kullanılmalıdır.
</details>

### 15. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Managed Redis, Azure Cache for Redis'in halefi olan Redis Enterprise tabanlı hizmettir. | ☐ | ☐ |
| b) Redis modülleri (ör. RediSearch) cache oluşturulduktan sonra istenildiğinde eklenebilir. | ☐ | ☐ |
| c) AMR için Microsoft Entra ID ile kimlik doğrulama desteklenir ve managed identity ile kullanılabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Modüller oluşturma sırasında seçilir; sonradan eklenmez.
</details>

### 16. Vaka (case study)
**Senaryo:** *Tailwind Travel*, otel arama API'sini Azure Cache for Redis Premium üzerinde çalıştırıyor ve bir de müşteri destek asistanı (LLM) geliştiriyor. Gözlemler: (1) otel detayı sayfası DB'den okunuyor, fiyatlar günde birkaç kez değişiyor ve kullanıcılar bayat fiyat şikayeti ediyor; (2) her gece 03:00'te binlerce anahtar aynı anda doluyor ve süresi aynı anda bitiyor, sabah DB zirve yapıyor; (3) asistan her tekrarlayan soru için LLM'i çağırıyor; ekip semantic cache denemek istiyor ve mevcut cache'e "vector modülünü ekleyelim" diyor; (4) Premium katmanın emeklilik tarihi yaklaşıyor.

**Soru A.** Bayat fiyat sorununu nasıl çözersiniz? **Soru B.** 03:00 yığılmasını nasıl çözersiniz? **Soru C.** Semantic cache için yeni cache'i nasıl yapılandırırsınız ve nelere dikkat edersiniz?

<details><summary>Cevap</summary>

**A.** Cache-aside'a **yazınca invalidate** ekleyin: fiyat DB'ye yazıldıktan sonra `DEL hotel:{id}` (veya olay tabanlı silme / versiyonlu anahtar); TTL güvenlik ağı olarak kalır (kısa-orta süre).

**B.** Tüm anahtarların aynı anda dolması ve aynı anda bitmesi **stampede** yaratır: TTL'e **jitter** ekleyin, ısıtmayı (warm-up) zamana yayın, popüler anahtarlarda **single-flight kilit / HybridCache** kullanın.

**C.** Mevcut cache'e modül **sonradan eklenemez**. **Azure Managed Redis**'te yeni bir örnek oluşturun (bu aynı zamanda Premium emeklilik göçünün hedefidir): RediSearch modülünü oluşturma sırasında seçin, **Enterprise clustering policy** ve **NoEviction** kullanın (Flash Optimized katmanını seçmeyin). `FT.CREATE ... VECTOR HNSW ... DIM <model boyutu> DISTANCE_METRIC COSINE` ile index; her kayda **TTL**, kiracı/kullanıcı verisi için **TAG ön-filtresi**; eşiği gerçek soru setiyle ölçerek seçin (yanlış-hit riski). `NoEviction` olduğu için bellek dolarsa yazma hatası alınır — doluluğu izleyin. Bağlantı Entra + managed identity ile, `ConnectionMultiplexer` singleton.
</details>

---

**Puanlama:** 13-16 → hazırsın · 10-12 → README 3.1 (modül/policy kararları), 3.2 (TTL, invalidation) ve 3.3 (`FT.CREATE`) bölümlerini tekrar et · < 10 → konuyu baştan çalış, 3.5 laboratuvarındaki bilerek bozuk senaryoları uygula.
