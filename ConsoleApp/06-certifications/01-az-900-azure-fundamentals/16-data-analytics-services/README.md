# C1.16 Azure Veri ve Analitik Servisleri

> 📁 `ConsoleApp/06-certifications/01-az-900-azure-fundamentals/16-data-analytics-services/` · Sınav alanı: **Describe Azure architecture and services — genişletilmiş kapsam (bağlam)** · Önkoşul: [C1.3](../03-cloud-service-types/README.md), [C1.8](../08-storage/README.md) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Bağlantı:** *Describe Azure architecture and services* alanındaki servis tanıma soruları; C2.4–C2.7 (geliştirici derinliği) ve M6/M8 (veritabanı) için zemin.
>
> Bu konu resmî study guide'ın (July 20, 2026) doğrudan hedefi değildir; kurs müfredatlarında ve senaryo sorularının seçeneklerinde geçer.

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

Azure'daki veri servisleri üç aileye ayrılır. Bu konunun işi, **"bu iş için hangi servis?"** sorusunu hızlı cevaplamaktır.

| Aile | Soru | Servisler |
|---|---|---|
| **Operasyonel (OLTP) veritabanları** | Uygulamanın günlük okuma/yazması | **Azure SQL** ailesi, **PostgreSQL/MySQL**, **Cosmos DB**, **Managed Redis** |
| **Analitik (OLAP) ve büyük veri** | Geçmiş veriyi toplayıp analiz etmek, raporlamak | **Microsoft Fabric**, **Synapse**, **Databricks**, **HDInsight**, **Data Lake Storage Gen2**, **Power BI** |
| **Taşıma ve akış** | Veriyi bir yerden bir yere taşımak/dönüştürmek | **Data Factory**, **Event Hubs**, **Stream Analytics**, **Database Migration Service** |

Çekirdek ayrım: veritabanı **yönetimi** kimde? (→ C1.3: IaaS ↔ PaaS)

| Seçenek | Model | Sen neyi yönetirsin? |
|---|---|---|
| **SQL Server on Azure VM** | IaaS | OS, SQL Server yaması, yedek, HA — her şey |
| **Azure SQL Managed Instance** | PaaS | Veritabanları ve ayarları; platform, yama, yedek, HA Microsoft'ta |
| **Azure SQL Database** | PaaS | Yalnızca veritabanı ve şema; en az yönetim yükü |

Bu konunun kodu yoktur; pratik kısım `az` CLI laboratuvarıdır (3.6).

---

## 2. Neden?

- **Sınav:** Senaryo sorularının seçeneklerinde sürekli geçer: "Mevcut SQL Server'ı **en az değişiklikle** taşı" → Managed Instance; "yeni uygulama, minimum yönetim" → SQL Database; "küresel dağıtım, tek haneli ms" → Cosmos DB; "ETL orkestrasyonu" → Data Factory. Ayrıca "VM'e kurulu SQL = PaaS mi?" tuzağı klasiktir.
- **Gerçek işte:** Yanlış veri servisi, ya aşırı maliyet (her şeye Cosmos DB) ya da mimari duvar (global yazma ihtiyacı olan uygulamayı tek region'lık SQL'e koymak) demektir. Veri modeli seçimi sonradan **en pahalı** değişen kararlardan biridir.
- **Bağlam:** EF Core ve SQL derinliği → M6/M8; Cosmos SDK, change feed, partition key tasarımı → C2.4–C2.7; geri yükleme ve felaket kurtarma → C1.19.

---

## 3. Nasıl?

### 3.1 Azure SQL ailesi ⭐

Üç ürün, aynı SQL Server motorunun farklı yönetim düzeyleri:

| | **SQL Database** | **SQL Managed Instance** | **SQL Server on Azure VM** |
|---|---|---|---|
| Model | PaaS (veritabanı başına) | PaaS (instance başına) | **IaaS** |
| SQL Server uyumu | Yüksek, ama instance düzeyi özellikler sınırlı | **Neredeyse tam** (SQL Agent, cross-database sorgu, linked server, CLR…) | **Tam** (sürüm/özellik seçimi sende) |
| OS erişimi | Yok | Yok | **Var** |
| Ağ | Public endpoint + private endpoint | **VNet'e** yerleştirilir | VNet içinde VM |
| Yönetim yükü | En düşük | Düşük | **En yüksek** |
| Tipik senaryo | **Yeni** bulut uygulaması, SaaS çok kiracılı | **Lift-and-shift** — mevcut SQL Server'ı az değişiklikle taşıma | OS erişimi/özel ürün gereksinimi, eski sürüm |

**SQL Database dağıtım seçenekleri:**
- **Single database** — tek veritabanı, kendi kaynak ayarı.
- **Elastic pool** — çok sayıda veritabanının **kaynakları paylaştığı** havuz; kullanım zamanları farklı olan çok kiracılı (multi-tenant) uygulamalarda maliyet düşürür.

**Satın alma modelleri:**

| Model | Birim | Ne zaman? |
|---|---|---|
| **DTU** | Hazır paket (CPU+bellek+IO karışımı) | Basit, öngörülebilir, küçük iş yükü |
| **vCore** | Ayrı seçilen vCore, bellek, depolama | Kontrol, Azure Hybrid Benefit (→ C1.11), reserved capacity |

- vCore modelinde **servis katmanları:** General Purpose, Business Critical (düşük gecikme, yerel SSD, yerleşik okuma replikası), Hyperscale (çok büyük veritabanları, hızlı ölçekleme) — *(bağlam: sınav katman adlarını sormaz)*.
- **Serverless compute tier** (vCore, General Purpose): yük değiştikçe otomatik ölçeklenir, **boşta kalınca otomatik duraklar** (auto-pause); duraklamışken **depolama** ücretlenir, compute ücretlenmez. Seyrek kullanılan geliştirme/test veritabanları için idealdir. İlk istek duraklamadan uyanırken **gecikme** (cold start) yaşar.
- **Ücretsiz teklif:** Azure SQL Database için aboneliğe bağlı, ömür boyu bir ücretsiz teklif vardır (veritabanı başına aylık 100.000 vCore saniyesi ve 32 GB depolama; subscription başına en fazla 10 veritabanı — güncel şartları resmî sayfadan doğrula). Limit dolunca ya otomatik duraklar ya da normal fiyatla devam eder (seçilir).

**PaaS faydaları (üçü için ortak):** otomatik yedek (varsayılan point-in-time restore penceresi genellikle 7 gün, ayarlanabilir; uzun süreli saklama için **LTR**), otomatik yama, yerleşik **yüksek erişilebilirlik** (SLA %99,99'a varan), kolay ölçekleme, **Transparent Data Encryption (TDE)** varsayılan açık, tehdit algılama (Defender for SQL → C1.10).

**Azure Database for PostgreSQL / MySQL:** açık kaynak motorların yönetilen (PaaS) sürümleri (**Flexible Server** mimarisi; eski Single Server mimarisi emekli edildi). Uygulama zaten PostgreSQL/MySQL kullanıyorsa kod değişikliği olmadan taşınır; yama, yedek, HA platformdadır.

### 3.2 Azure Cosmos DB ⭐ (NoSQL)

**Cosmos DB**: tamamen yönetilen, **küresel dağıtımlı**, çok modelli bir NoSQL veritabanı.

| Özellik | Açıklama |
|---|---|
| **Çok modelli / çok API'li** | NoSQL (yerel, JSON belge) API'si; ayrıca MongoDB, Cassandra, Gremlin (graf), Table API'leri *(bağlam — güncel API listesini doğrula)* |
| **Küresel dağıtım** | Tek tıkla veriyi birden çok region'a çoğalt; **çok region'da yazma** (multi-region writes) seçeneği |
| **Düşük gecikme** | Okuma/yazma için SLA ile desteklenen **tek haneli milisaniye** (p99) |
| **Esnek şema** | Belge şeması sabit değil; şema evrimi uygulama tarafında |
| **Throughput = RU/s** | **Request Unit** — işlem maliyetinin ortak birimi (bir 1 KB'lık point-read ≈ 1 RU). Modlar: **provisioned** (sabit RU/s), **autoscale**, **serverless** (kullandığın kadar öde) |
| **Ölçekleme** | **Partition key** ile yatay bölümleme; iyi bir partition key tasarımı performans/maliyetin anahtarıdır (→ C2.4) |
| **SLA** | Okuma/yazma kullanılabilirliği için yüksek SLA'lar; çok region'lı hesapta en yüksek düzey *(rakamlar yapılandırmaya göre değişir — güncel SLA belgesine bak)* |

**Beş tutarlılık düzeyi** (güçlüden zayıfa) — SQL'de olmayan Cosmos'a özgü kavram:

| Düzey | Anlamı | Trade-off |
|---|---|---|
| **Strong** | Her okuma en son yazılanı görür | En yüksek gecikme; çok region'da yazma ile birlikte kullanılamaz |
| **Bounded staleness** | Okuma en fazla *K* sürüm/*T* süre geride kalır | Öngörülebilir gecikme farkı |
| **Session** *(varsayılan)* | Aynı oturumda kendi yazdığını okursun | Çoğu uygulama için en iyi denge |
| **Consistent prefix** | Sıralama bozulmaz, ama geride kalabilir | Daha düşük maliyet |
| **Eventual** | Sıra/yenilik garantisi yok, sonunda tutarlı | En düşük gecikme ve maliyet |

> Kural: **güçlü tutarlılık ↔ düşük gecikme/yüksek erişilebilirlik** arasında takas vardır (CAP/PACELC sezgisi → M8).

**Ücretsiz katman (free tier):** hesap başına ilk **1000 RU/s ve 25 GB** ücretsizdir; aboneliğe **bir** free tier hesabı, hesap oluştururken seçilir (serverless hesaplarda yoktur). Yerel geliştirme için **Cosmos DB emulator** (Windows/Linux/Docker imajı) *(bağlam)* gerçek hesap açmadan test imkânı verir.

### 3.3 Önbellek ve Storage'ın NoSQL benzeri servisleri

| Servis | Ne? | Not |
|---|---|---|
| **Azure Managed Redis** | Yönetilen **in-memory** önbellek/veri yapıları (Redis) | Oturum, sık okunan veri, rate limiting. **Güncel durum:** eski *Azure Cache for Redis* (Basic/Standard/Premium) emekliye ayrılıyor — yeni oluşturma kısıtlandı, **30 Eylül 2028**'de tamamen emekli; Enterprise katmanları **Mart 2027** civarı. Önerilen hedef **Azure Managed Redis**. Tarihleri resmî duyurudan doğrula |
| **Table storage** | Storage account içinde basit anahtar/değer-özellik deposu | Ucuz, basit; zengin sorgu/global dağıtım yok (premium ihtiyaç → Cosmos DB Table API) |
| **Blob storage** | Yapılandırılmamış nesne verisi (→ C1.8) | Veritabanı değildir; ama **Data Lake**'in temelidir |

### 3.4 İlişkisel ↔ NoSQL karar tablosu ⭐

| Soru | İlişkisel (Azure SQL, PostgreSQL) | NoSQL (Cosmos DB) |
|---|---|---|
| Veri modeli | Tablolar, sabit şema, ilişkiler/JOIN | Belge/anahtar-değer/graf, esnek şema |
| İşlem bütünlüğü | Güçlü ACID, karmaşık çok-tablo işlemler | Partition içinde işlemler; çapraz-partition sınırlı |
| Ölçekleme | Dikey ağırlıklı (Hyperscale hariç), okuma replikaları | **Yatay**, neredeyse sınırsız |
| Küresel dağıtım | Geo-replication (okuma replikaları, failover group) | **Yerleşik, çok region'da yazma** |
| Gecikme | İyi | Tek haneli ms (SLA) |
| Sorgu | Güçlü SQL, raporlama | Partition key'e göre verimli sorgu |
| Ne zaman? | Finansal kayıt, ERP, karmaşık raporlama | Katalog, IoT telemetri, oturum/profil, küresel oyun/e-ticaret |

Karar ağacı:

```
Mevcut SQL Server, az değişiklikle taşınacak?     → SQL Managed Instance
Mevcut SQL Server, OS erişimi/özel yazılım şart?  → SQL Server on Azure VM (IaaS)
Yeni uygulama, ilişkisel veri, düşük yönetim?     → Azure SQL Database
Mevcut PostgreSQL/MySQL uygulaması?               → Azure Database for PostgreSQL / MySQL
Küresel, esnek şema, çok yüksek ölçek, düşük ms?  → Cosmos DB
Sık okunan veriyi hızlandır / oturum?             → Azure Managed Redis (önbellek)
```

### 3.5 Analitik ve büyük veri

**OLTP ↔ OLAP:** OLTP (online transaction processing) çok sayıda küçük, hızlı işlem (sipariş ver, bakiye güncelle) için; **OLAP** (online analytical processing) büyük veri kümeleri üzerinde karmaşık sorgu/toplama (aylık satış trendi) için. Aynı veritabanında ikisini birden zorlamak performansı bozar → **ayrı analitik katman** kurulur.

**Batch ↔ streaming:** *batch* belirli aralıklarla toplu işler (gecelik); *streaming* gelen veriyi **gerçek zamanlı** işler (IoT, log akışı).

| Servis | Tek cümle | İş türü |
|---|---|---|
| **Microsoft Fabric** | **Birleşik analitik SaaS platformu**: veri mühendisliği, depo (warehouse), gerçek zamanlı analiz, Power BI — tek OneLake deposu üzerinde | Uçtan uca analitik, yeni projeler için Microsoft'un yönlendirdiği platform |
| **Azure Synapse Analytics** | Veri ambarı (SQL pool'lar) + Spark + pipeline'ları bir arada PaaS | Kurumsal veri ambarı (OLAP). Hâlâ destekli; ama Microsoft yeni işler için **Fabric**'e yönlendiriyor *(durumu resmî duyurudan doğrula)* |
| **Azure Databricks** | **Apache Spark** tabanlı, ortak çalışmaya dönük analitik/ML platformu (notebook'lar) | Büyük veri işleme, veri bilimi, ML |
| **Azure HDInsight** | Yönetilen **açık kaynak** büyük veri küme servisi (Hadoop, Spark, Kafka, HBase…) | Mevcut Hadoop/Spark iş yüklerini taşımak |
| **Azure Data Factory** | Bulutta **ETL/ELT orkestrasyonu**: kaynaklardan veri çek, dönüştür, hedefe yaz; zamanlanmış pipeline'lar, 90+ konnektör | **Veri taşıma ve dönüştürme** |
| **Azure Data Lake Storage Gen2** | **Blob Storage + hiyerarşik namespace**: analitik için optimize, ham/yapısız/yarı yapılı veri gölü | **Depolama** (veri gölü) |
| **Event Hubs** | Yüksek hacimli olay/telemetri **alım** servisi (milyonlarca olay/sn; Kafka uyumlu) | Akan veriyi toplama |
| **Stream Analytics** | Akan veri üzerinde **gerçek zamanlı SQL benzeri sorgu** | Akış **analizi** (Event Hubs → Stream Analytics → Power BI) |
| **Power BI** | İş zekâsı: görselleştirme, dashboard, rapor (SaaS) | **Raporlama** |

**Tek cümlelik eşleme:** veri **taşı/dönüştür** → Data Factory · **depola** → Data Lake Storage Gen2 · **işle/analiz et** → Synapse / Databricks / Fabric · **akışı al** → Event Hubs · **akışı işle** → Stream Analytics · **raporla** → Power BI.

Tipik analitik hattı (Medallion/lakehouse tarzı):

```
Kaynak sistemler ─▶ Data Factory ─▶ Data Lake Storage Gen2 ─▶ Synapse/Databricks/Fabric ─▶ Power BI
(SQL, SaaS, dosya)   (taşı)         (ham → temiz → hazır)     (dönüştür/analiz)          (rapor)
IoT cihazları ─▶ Event Hubs ─▶ Stream Analytics ─▶ (Data Lake / Power BI canlı panel)
```

> Ayrı bir hat: **Microsoft Purview** veri yönetişimi ve sınıflandırma (→ C1.12), **Azure Machine Learning/AI servisleri** (→ C1.20 — başka ajanın konusu; burada yalnızca ad olarak anılır).

### 3.6 Taşıma ve güvenlik özeti

- **Taşıma:** **Azure Database Migration Service (DMS)** SQL Server/PostgreSQL/MySQL gibi veritabanlarını Azure'a **çevrimiçi (minimum kesinti)** veya **çevrimdışı** taşır; **Azure Migrate** keşif/değerlendirme/taşıma merkezidir (→ C1.19).
- **Şifreleme:** durağan veri — SQL'de **TDE** (varsayılan açık), Cosmos DB ve Storage'da platform yönetimli anahtarlarla varsayılan şifreleme (isteğe bağlı müşteri yönetimli anahtar → C1.18); aktarımda — bağlantıda **TLS**; sütun düzeyi **Always Encrypted** *(bağlam)*.
- **Kimlik doğrulama:** parola yerine **Microsoft Entra** kimlik doğrulaması ve **managed identity** (→ C1.9, C2.11).
- **Ağ:** SQL Database/Cosmos DB için **private endpoint** + public erişimi kapatma (→ C1.7); firewall kuralları.
- **Dayanıklılık:** **active geo-replication / failover group** (SQL), Cosmos DB'de çok region'lı hesap; yedekler — redundancy yedek yerine geçmez (→ C1.8, C1.19).

### 3.7 Pratik — ücretsiz/ucuz SQL Database, Cosmos DB free tier

> Bütçe alarmın kurulu olsun (→ C1.1, C1.11). Serverless SQL Database + ücretsiz teklif ile kullanım **çok düşük** (genellikle sıfır) maliyetlidir; yine de **laboratuvar bitince sil**. Cosmos DB free tier aboneliğe **bir** hesap içindir — daha önce açtıysan bu adım hata verir. Parolayı repoya/komut geçmişine koyma.

```bash
az login
RG=rg-az900-c116; LOC=westeurope
az group create --name $RG --location $LOC

# 1) SQL logical server (name must be globally unique)
SRV=sql-az900-$RANDOM
PW="$(openssl rand -base64 24)Aa1!"          # generated; never commit a password
az sql server create --resource-group $RG --name $SRV --location $LOC \
  --admin-user sqladmin --admin-password "$PW"

# 2) Serverless database with the free offer + auto-pause
az sql db create --resource-group $RG --server $SRV --name appdb \
  --edition GeneralPurpose --family Gen5 --compute-model Serverless \
  --capacity 1 --min-capacity 0.5 --auto-pause-delay 60 \
  --use-free-limit --free-limit-exhaustion-behavior AutoPause

# 3) Inspect: compute model, auto-pause delay, free-limit flag
az sql db show --resource-group $RG --server $SRV --name appdb \
  --query "{sku:currentServiceObjectiveName, autoPauseMin:autoPauseDelay, minVcores:minCapacity, status:status}" --output table

# 4) (Optional) the database is PaaS: there is no VM, no OS, no patching to do.
#    Backups are automatic — check the point-in-time restore window:
az sql db str-policy show --resource-group $RG --server $SRV --name appdb

# 5) Cosmos DB account with free tier (one per subscription)
COSMOS=cosmos-az900-$RANDOM
az cosmosdb create --resource-group $RG --name $COSMOS \
  --enable-free-tier true --default-consistency-level Session \
  --locations regionName=$LOC failoverPriority=0 isZoneRedundant=False
az cosmosdb sql database create --account-name $COSMOS --resource-group $RG --name appdb --throughput 400
az cosmosdb sql container create --account-name $COSMOS --resource-group $RG \
  --database-name appdb --name orders --partition-key-path "/customerId"

# 6) Consistency policy; try changing it
az cosmosdb show --resource-group $RG --name $COSMOS --query "consistencyPolicy.defaultConsistencyLevel"
az cosmosdb update --resource-group $RG --name $COSMOS --default-consistency-level Eventual

# 7) Cleanup — mandatory
az group delete --name $RG --yes --no-wait
```

<details><summary>Beklenen gözlemler ve açıklamaları</summary>

| Adım | Gözlem | Kavram |
|---|---|---|
| 2–3 | `compute-model Serverless`, `autoPauseDelay: 60` (dakika; serverless'ta en düşük değer 60) | Serverless: boşta duraklar, compute ücreti kesilir |
| 2 | `--use-free-limit` reddedilirse: abonelikte zaten 10 ücretsiz veritabanı vardır veya bölge/teklif kısıtı var | Ücretsiz teklif limitleri |
| 4 | Yedek politikası kendiliğinden vardır | PaaS: otomatik yedek |
| 5 | `--enable-free-tier` ikinci hesapta hata verir | Aboneliğe bir free tier hesabı |
| 6 | `Session` → `Eventual`'a değiştirmek anında yapılır, uygulama kodu değişmez | Tutarlılık hesap düzeyinde ayarlanır |
| (düşünce deneyi) | Aynı hesabı `--locations` ile ikinci bir region'a eklesen ne değişir? | Küresel dağıtım + `--enable-multiple-write-locations` ile çok region'da yazma; maliyet **region sayısıyla** artar |

**Portal demosu yok:** Query editor ve Data Explorer'ı portalda denemek için `$SRV` firewall kuralı eklemek gerekir; bu laboratuvarın kapsamı dışında. **Emulator** (Docker imajı) yerel geliştirme için *(bağlam)*.
</details>

**Karar tablosu alıştırması** — her senaryoyu bir servise eşle (çözüm `<details>` içinde):

1. Şirket içi SQL Server (SQL Agent job'ları ve cross-database sorguları var) en az değişiklikle taşınacak.
2. Küresel bir mobil oyunun oyuncu profilleri; esnek şema, çok region'da yazma.
3. Gece 02:00'da 12 kaynaktan veri çekip dönüştüren pipeline.
4. Binlerce IoT cihazından saniyede milyonlarca olay alınıyor; anlık anomali tespiti isteniyor.
5. Yönetim kuruluna haftalık satış dashboard'u.

<details><summary>Çözüm</summary>

1 → **SQL Managed Instance** · 2 → **Cosmos DB** · 3 → **Data Factory** · 4 → **Event Hubs** (alım) + **Stream Analytics** (analiz) · 5 → **Power BI**.
</details>

---

## 4. Gerçek dünya senaryosu

Bir perakende şirketi e-ticaret platformunu modernleştiriyor:

1. **Mevcut ERP** veritabanı şirket içi SQL Server (SQL Agent job'ları var) → **Database Migration Service** ile **SQL Managed Instance**'a taşınır (kod değişikliği yok, VNet içinde, private).
2. **Yeni sepet/oturum servisi**, bölgeler arası düşük gecikme ister → **Cosmos DB** (Avrupa ve ABD'de çok region'da yazma, Session tutarlılığı) + sık okunan ürün verisi için **Azure Managed Redis**.
3. **Ürün kataloğu** ilişkisel kalıyor; yeni mikroservisler için **Azure SQL Database** (serverless, geliştirme ortamında auto-pause).
4. **Analitik:** **Data Factory** her gece SQL'ten, Cosmos'tan ve SaaS CRM'den veriyi **Data Lake Storage Gen2**'ye çeker; **Fabric/Synapse** ile dönüştürülür; **Power BI** ile satış panoları.
5. **Mağaza kasalarından telemetri** → **Event Hubs → Stream Analytics** ile anlık stok uyarıları.
6. **Güvenlik:** hepsi için **private endpoint**, public erişim kapalı; kimlik doğrulama **managed identity**; TDE ve durağan şifreleme varsayılan; **Defender for SQL/Cosmos DB** (→ C1.10).

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → servis eşleme; "PaaS mi IaaS mi" ayrımı; Cosmos DB özellikleri; analitik servis rolleri.

| Senaryodaki ifade | Cevap |
|---|---|
| "Mevcut SQL Server'ı **en az değişiklikle** (instance düzeyi özelliklerle) taşı, yine de yönetilen olsun" | **SQL Managed Instance** |
| "Yeni uygulama, minimum yönetim, ilişkisel" | **Azure SQL Database** |
| "OS erişimi / belirli SQL Server sürümü" | **SQL Server on Azure VM** (IaaS) |
| "Çok sayıda veritabanı kaynakları paylaşsın" | **Elastic pool** |
| "Boşta otomatik duraklayan, kullandığın kadar öde veritabanı" | **Serverless compute tier** |
| "Küresel dağıtım, tek haneli ms, esnek şema, çok region'da yazma" | **Cosmos DB** |
| "Veriyi sıcak tutmak için bellek içi önbellek" | **Azure Managed Redis** |
| "Veri ambarı / SQL tabanlı analitik" | **Synapse** (veya Fabric Warehouse) |
| "Spark tabanlı ortak çalışma, ML" | **Azure Databricks** |
| "Birleşik analitik SaaS (OneLake, Power BI dahil)" | **Microsoft Fabric** |
| "Hadoop/Kafka/HBase kümesini yönetilen çalıştır" | **HDInsight** |
| "ETL/ELT, pipeline orkestrasyonu, veri taşıma" | **Data Factory** |
| "Veri gölü, hiyerarşik namespace" | **Data Lake Storage Gen2** |
| "Gelen akışı gerçek zamanlı sorgula" | **Stream Analytics** (alım: **Event Hubs**) |
| "Dashboard/rapor" | **Power BI** |

**Tuzaklar:**
- "VM'e SQL Server kurmak PaaS'tır" → **Hayır**, IaaS.
- "Managed Instance ↔ SQL Database": **instance düzeyi uyumluluk** gerekiyorsa MI; yeni ve basit ise SQL Database.
- "Cosmos DB yalnızca tek region'dır" → **Hayır**; küresel dağıtım ve çok region'da yazma vardır.
- "Cosmos DB yalnızca bir veri modeli sunar" → birden çok API/model.
- "Data Factory veriyi depolar" → **Hayır**, **taşır/dönüştürür**; depolama Data Lake'tedir.
- "Event Hubs akışı analiz eder" → **Hayır**, **alır**; analiz Stream Analytics'te.
- "Geo-redundant storage veritabanı yedeğinin yerine geçer" → **Hayır** (→ C1.8, C1.19).

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Her yeni uygulama için "NoSQL modern" diye Cosmos DB seçmek | İlişkisel veri + JOIN + ACID ağırlıklıysa Azure SQL; Cosmos'u partition key tasarımını yapabildiğinde seç |
| Cosmos DB'de kötü partition key (düşük kardinalite, tek "sıcak" bölüm) | Yüksek kardinaliteli, istekleri eşit dağıtan anahtar; yoksa throttling (429) ve yüksek RU maliyeti |
| Cosmos DB'de tüm hesabı `Strong` tutarlılıkla açmak | Gecikme ve maliyet artar; çoğu senaryo için `Session` yeterli |
| Dev/test SQL Database'ini 7/24 provisioned bırakmak | Serverless + auto-pause veya ücretsiz teklif; ya da çalışma saatlerinde ölçekle |
| Analitik sorguları operasyonel (OLTP) veritabanında çalıştırmak | Ayrı analitik katman (replika, Data Lake + Synapse/Fabric) |
| Veritabanı parolasını connection string'te saklamak | Entra kimlik doğrulaması + managed identity, sır gerekiyorsa Key Vault (→ C1.18) |
| SQL Database'i public endpoint + geniş firewall aralığıyla bırakmak | Private endpoint, public erişimi kapat |
| Laboratuvardaki SQL server/Cosmos hesabını silmeyi unutmak | Resource group'u sil; ücretsiz limit dışı kullanım faturalanır |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| SQL Database (PaaS) vs VM'de SQL | Yama/yedek/HA devri, düşük yönetim | Instance düzeyi özellik ve OS erişimi sınırı |
| Managed Instance | Yüksek uyumluluk, kolay taşıma | Dağıtım süresi ve minimum maliyet daha yüksek; VNet gereksinimi |
| Serverless compute tier | Boşta sıfır compute maliyeti, otomatik ölçek | Duraklamadan uyanışta ilk istek gecikmesi; sürekli yükte provisioned daha ucuz olabilir |
| Elastic pool | Çok kiracılı uygulamada paylaşılan kaynak = düşük maliyet | Gürültülü komşu; havuz limitleri |
| Cosmos DB | Küresel dağıtım, ms gecikme, yatay ölçek | RU/s planlaması; region sayısı = maliyet; partition tasarımı zorunlu |
| Cosmos autoscale vs provisioned vs serverless | Autoscale: dalgalı yük; serverless: seyrek yük | Autoscale birim fiyatı daha yüksek; serverless'ta throughput ve ölçek limiti |
| Redis önbellek | Çok düşük okuma gecikmesi, DB yükü azalır | Ek servis maliyeti, **cache invalidation** karmaşıklığı |
| Fabric (SaaS) | Tek platform, az yönetim, Power BI entegre | Kapasite tabanlı maliyet modeli; platforma bağlılık |
| Synapse/Databricks/HDInsight | Daha fazla kontrol ve mevcut yatırım | Daha çok yönetim; Synapse'te yol haritası riski |

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Azure SQL documentation](https://learn.microsoft.com/en-us/azure/azure-sql/) · [Deploy Azure SQL Database for free](https://learn.microsoft.com/en-us/azure/azure-sql/database/free-offer)
- [Azure Cosmos DB introduction](https://learn.microsoft.com/en-us/azure/cosmos-db/introduction) · [Consistency levels](https://learn.microsoft.com/en-us/azure/cosmos-db/consistency-levels) · [Free tier](https://learn.microsoft.com/en-us/azure/cosmos-db/free-tier)
- [Azure Managed Redis overview](https://learn.microsoft.com/en-us/azure/redis/overview) · [Azure Cache for Redis retirement FAQ](https://learn.microsoft.com/en-us/azure/redis/retirement-faq)
- [What is Microsoft Fabric?](https://learn.microsoft.com/en-us/fabric/fundamentals/microsoft-fabric-overview) · [Azure Synapse Analytics](https://learn.microsoft.com/en-us/azure/synapse-analytics/overview-what-is)
- [What is Azure Data Factory?](https://learn.microsoft.com/en-us/azure/data-factory/introduction) · [Data Lake Storage Gen2](https://learn.microsoft.com/en-us/azure/storage/blobs/data-lake-storage-introduction)
- [Azure Database Migration Service](https://learn.microsoft.com/en-us/azure/dms/dms-overview)
- [`az sql db create`](https://learn.microsoft.com/en-us/cli/azure/sql/db) · [`az cosmosdb create`](https://learn.microsoft.com/en-us/cli/azure/cosmosdb)
