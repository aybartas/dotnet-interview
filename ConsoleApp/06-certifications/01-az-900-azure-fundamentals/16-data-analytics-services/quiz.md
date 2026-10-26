# C1.16 Azure Veri ve Analitik Servisleri — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *genişletilmiş kapsam (bağlam)* · 15 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, eşleştirme, senaryo). Gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ %80** (≥ 12/15).

---

### 1. Tek seçim
Bir şirket, şirket içindeki SQL Server'ını (SQL Agent job'ları ve cross-database sorguları kullanıyor) **en az kod/yapılandırma değişikliğiyle**, yine de **yönetilen (PaaS)** bir servise taşımak istiyor. Hangisi **en uygundur**?

- A) Azure SQL Database (single database)
- B) Azure SQL Managed Instance
- C) SQL Server on Azure VM
- D) Azure Table storage

<details><summary>Cevap</summary>

**B.** Managed Instance, instance düzeyindeki özelliklerle (SQL Agent, cross-database sorgu…) neredeyse tam SQL Server uyumluluğu sunan PaaS servisidir. SQL Server on Azure VM de uyumludur ama **IaaS**'tır; yönetim yükünü sen taşırsın.
</details>

### 2. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure VM üzerine kurulan SQL Server bir PaaS çözümdür. | ☐ | ☐ |
| b) Azure SQL Database'de yama ve yedekleme platform tarafından yönetilir. | ☐ | ☐ |
| c) SQL Managed Instance'ın OS'una RDP ile bağlanılabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** VM'deki SQL Server IaaS'tır (OS ve SQL yamaları sende). SQL Database ve Managed Instance PaaS'tır; ikisinde de OS erişimi yoktur.
</details>

### 3. Tek seçim
Çok kiracılı (multi-tenant) bir SaaS uygulamasında yüzlerce küçük veritabanının kullanım zirveleri farklı saatlerde oluşuyor. Maliyeti düşürmek için **kaynakları veritabanları arasında paylaştırmak** istiyorsunuz. Ne kullanmalısınız?

- A) Her veritabanı için ayrı Business Critical katmanı
- B) SQL Database **elastic pool**
- C) Cosmos DB serverless
- D) SQL Server on Azure VM

<details><summary>Cevap</summary>

**B.** Elastic pool, aralarında kullanım zamanları farklı olan çok sayıda veritabanının paylaşımlı bir kaynak havuzunu kullanmasını sağlar.
</details>

### 4. Tek seçim
Bir geliştirme veritabanı günde yalnızca birkaç saat kullanılıyor; boşta olduğunda **compute ücretinin kesilmesi** isteniyor. Hangi özellik bunu sağlar?

- A) Azure SQL Database **serverless compute tier** (auto-pause)
- B) DTU modelinde Premium katman
- C) Geo-replication
- D) Elastic pool'a eklemek

<details><summary>Cevap</summary>

**A.** Serverless katmanı, boşta kalınca veritabanını duraklatır; duraklamışken yalnızca depolama ücretlenir. Bedeli: duraklamadan uyanırken ilk istekte gecikmedir.
</details>

### 5. Tek seçim
Küresel bir oyunun oyuncu profilleri **birden çok region'da yazılabilir**, **esnek şemalı** ve **tek haneli milisaniye gecikmeli** olmalıdır. Hangi servis **en uygundur**?

- A) Azure SQL Database
- B) Azure Cosmos DB
- C) Azure Table storage
- D) Azure Data Lake Storage Gen2

<details><summary>Cevap</summary>

**B.** Cosmos DB yerleşik küresel dağıtım, çok region'da yazma, esnek şema ve SLA ile desteklenen düşük gecikme sunar. Table storage basit bir anahtar-değer deposudur; küresel dağıtım/SLA'lı gecikme sağlamaz.
</details>

### 6. Sıralama
Cosmos DB tutarlılık düzeylerini **en güçlüden en zayıfa** sıralayın: **Eventual · Strong · Session · Bounded staleness · Consistent prefix**

<details><summary>Cevap</summary>

**Strong → Bounded staleness → Session → Consistent prefix → Eventual.** Güçlü tutarlılık gecikme ve erişilebilirlik pahasınadır; **Session** varsayılandır ve çoğu uygulama için dengelidir.
</details>

### 7. Tek seçim
Cosmos DB'de **RU/s** neyi ifade eder?

- A) Replica Unit per second — region başına replika sayısı
- B) Request Unit per second — işlemlerin CPU/bellek/IO maliyetini ortak bir birimle gösteren throughput
- C) Read Update per second — yalnızca yazma sayısı
- D) Retention Unit per second — verinin saklanma süresi

<details><summary>Cevap</summary>

**B.** RU, bir işlemin maliyetinin soyut birimidir; throughput **RU/s** olarak sağlanır (provisioned, autoscale veya serverless modelle).
</details>

### 8. Çoklu seçim (iki doğru cevap)
Bir ekip, anlık Event Hubs akışını **gerçek zamanlı analiz edip** sonucu Power BI'a göndermek istiyor. Hangi **iki** servis bu hatta doğal olarak yer alır?

- A) Azure Event Hubs (akışı almak için)
- B) Azure Stream Analytics (akışı sorgulamak için)
- C) Azure HDInsight, yalnızca depolama için
- D) Azure Database Migration Service

<details><summary>Cevap</summary>

**A ve B.** Event Hubs yüksek hacimli olayları **alır**, Stream Analytics akan veri üzerinde gerçek zamanlı sorgu çalıştırır; çıktı Power BI'a gidebilir. DMS veritabanı taşıma aracıdır.
</details>

### 9. Eşleştirme
Her işi servisle eşleştirin: **Data Factory · Data Lake Storage Gen2 · Power BI · Azure Databricks**

1. Birden çok kaynaktan veriyi çekip dönüştüren zamanlanmış pipeline'lar
2. Ham ve yapılandırılmamış analitik verinin hiyerarşik namespace'li deposu
3. Raporlar ve dashboard'lar (iş zekâsı)
4. Apache Spark tabanlı, notebook ile ortak çalışılan analitik/ML platformu

<details><summary>Cevap</summary>

**1 → Data Factory · 2 → Data Lake Storage Gen2 · 3 → Power BI · 4 → Azure Databricks.**
</details>

### 10. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Microsoft Fabric, veri mühendisliği, depo ve Power BI'ı birleştiren bir SaaS analitik platformudur. | ☐ | ☐ |
| b) Azure Data Factory, verinin kalıcı olarak saklandığı asıl depolama servisidir. | ☐ | ☐ |
| c) Data Lake Storage Gen2, Blob Storage üzerine hiyerarşik namespace ekler. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Data Factory veriyi **taşır/dönüştürür**, depolamaz; Data Lake Storage Gen2 = Blob + hiyerarşik namespace.
</details>

### 11. Tek seçim
OLTP ve OLAP için hangi eşleme **doğrudur**?

- A) OLTP: büyük veri kümeleri üzerinde karmaşık toplama; OLAP: çok sayıda küçük işlem
- B) OLTP: çok sayıda küçük, hızlı işlem; OLAP: büyük veri üzerinde analitik sorgular
- C) İkisi de yalnızca NoSQL veritabanlarına aittir
- D) OLTP yalnızca streaming, OLAP yalnızca batch demektir

<details><summary>Cevap</summary>

**B.** OLTP: sipariş ver, bakiye güncelle gibi işlemsel iş yükü. OLAP: geçmiş veriyi toplayıp analiz etme. Analitik yük operasyonel veritabanından ayrılır.
</details>

### 12. Senaryo
Bir şirket sık okunan ürün verilerini, veritabanı yükünü azaltmak için **bellek içi** bir katmanda tutmak istiyor. Hangi servis uygundur?

- A) Azure Managed Redis
- B) Azure Data Factory
- C) Azure Synapse Analytics
- D) Azure Blob Storage Archive tier

<details><summary>Cevap</summary>

**A.** Yönetilen Redis, bellek içi önbellek ve hızlı veri yapıları için kullanılır. (Eski *Azure Cache for Redis* emekli edilmektedir; yeni hedef Azure Managed Redis'tir — tarihleri resmî duyurudan doğrula.)
</details>

### 13. Senaryo
Bir ekip Azure SQL Database'e **parola** ile bağlanan bir App Service uygulaması çalıştırıyor. Güvenliği artırmak için **en iyi** adım hangisidir?

- A) Parolayı `appsettings.json`'a yazıp repoya koymak
- B) Managed identity ile Microsoft Entra kimlik doğrulamasına geçmek ve SQL için private endpoint kullanmak
- C) SQL firewall'unda tüm IP aralıklarına izin vermek
- D) TDE'yi kapatmak

<details><summary>Cevap</summary>

**B.** Parolasız (managed identity + Entra) erişim secret sızıntısını önler; private endpoint ağ yüzeyini kapatır. TDE varsayılan açıktır ve kapatmak güvenliği düşürür.
</details>

### 14. Tek seçim
Hangi ifade **yanlıştır**?

- A) Cosmos DB free tier, hesap başına ilk 1000 RU/s ve 25 GB'ı ücretsiz verir.
- B) Aboneliğe en fazla bir Cosmos DB free tier hesabı açılabilir.
- C) SQL Database serverless'ta duraklamış veritabanı için depolama ücretlenmez.
- D) Azure SQL Database'in ücretsiz teklifinde limit dolunca otomatik duraklama seçilebilir.

<details><summary>Cevap</summary>

**C.** Duraklamışken **compute** ücretlenmez ama **depolama** ücretlenmeye devam eder (ücretsiz teklifin depolama kotası dışında). Diğerleri doğrudur (güncel limitleri resmî sayfadan doğrula).
</details>

### 15. Senaryo
Bir perakende şirketi yeni bir mikroservis için ilişkisel veri, minimum yönetim ve kullanıma göre ölçeklenen bir veritabanı istiyor. Aşağıdakilerden hangi kombinasyon **en uygundur**?

- A) SQL Server on Azure VM + manuel yedek betikleri
- B) Azure SQL Database (serverless) + private endpoint
- C) Cosmos DB, yalnızca JOIN gerektiren raporlar için
- D) HDInsight Hadoop kümesi

<details><summary>Cevap</summary>

**B.** Yeni, ilişkisel, düşük yönetimli iş yükü için SQL Database; serverless ile ölçek/maliyet esnekliği; private endpoint ile ağ güvenliği. A yönetim yükü, C yanlış veri modeli, D yanlış iş türüdür.
</details>
