# C2.14 Deneme Sınavı ve Sınav Stratejisi

> 📁 `ConsoleApp/06-certifications/02-ai-200-azure-ai-cloud-developer/14-practice-exam/` · Sınav alanı: **Tüm sınav alanları (C2.1–C2.13)** · Önkoşul: C2.1–C2.13 quiz'lerinde ≥ %80 · Quiz: [`quiz.md`](quiz.md) (50 soru, 2 vaka çalışması)
>
> 📌 **Resmî hedef (skills measured as of October 7, 2026):** Bu konu yeni bir hedef eklemez; AI-200'ün dört alanını tek sınavda ölçer: *Develop containerized solutions on Azure* (%20-25) · *Develop AI solutions by using Azure data management services* (%30-35) · *Connect to and consume Azure services* (%20-25) · *Secure, monitor, and troubleshoot Azure solutions* (%20-25). Sınav formatı, süre, soru sayısı ve ücret değişebilir — sınavdan önce resmî [AI-200 sınav sayfası](https://learn.microsoft.com/en-us/credentials/certifications/exams/ai-200/) ve [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)'ı kontrol et.

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

AI-200 bir **associate** sınavıdır: AZ-900 gibi kavramları eşleştirmekle yetinmez; senaryodan **doğru servisi, doğru yapılandırmayı ve doğru kod/CLI/KQL parçasını** seçmeni ister. Bu konu üç parçadan oluşur:

| Parça | Dosya | Amaç |
|---|---|---|
| **Deneme sınavı** | [`quiz.md`](quiz.md) | Resmî ağırlıklara göre dağıtılmış **50 özgün soru**; her soru bir alana ve konuya (`[B · C2.5]`) bağlı. 2 vaka çalışması (13–16, 29–32), C#/Python/CLI/KQL/SQL/YAML/JSON tamamlama, sıralama, eşleştirme, evet/hayır |
| **Puanlama ve tekrar planı** | `quiz.md` sonu | Alan bazlı puan tablosu; her yanlışı C2.x'e bağlayan tekrar tablosu |
| **Sınav stratejisi** | Bu README | Soru türleri, vaka çalışması ve kod tamamlama teknikleri, anahtar kelime → servis tablosu, zaman yönetimi, sınav günü |

**Soru dağılımı** (resmî ağırlıklara göre):

| Alan | Konular | Ağırlık | Bu denemede |
|---|---|---|---|
| **A** — Develop containerized solutions on Azure | C2.1 ACR · C2.2 Container Apps · C2.3 AKS | %20-25 | 11 soru (%22) |
| **B** — Develop AI solutions by using Azure data management services | C2.4 Cosmos NoSQL · C2.5 Cosmos vector/change feed · C2.6 PostgreSQL/pgvector · C2.7 Managed Redis | **%30-35** | 17 soru (%34) |
| **C** — Connect to and consume Azure services | C2.8 Functions · C2.9 Service Bus · C2.10 Event Grid | %20-25 | 11 soru (%22) |
| **D** — Secure, monitor, and troubleshoot Azure solutions | C2.11 Identity/Key Vault · C2.12 App Configuration · C2.13 OpenTelemetry/KQL | %20-25 | 11 soru (%22) |

**Sınavın genel çerçevesi** (ayrıntılar değişebilir ve kaynaklarda tutarsızdır — resmî sayfadan doğrula):

| | |
|---|---|
| Geçme puanı | **700 / 1000** (ölçeklenmiş puan; "doğru yüzdesi" ile birebir aynı değildir) |
| Soru sayısı ve süre | Çevrimiçi kaynaklarda kabaca 40-60 soru ve 100-120 dakika aralığı geçiyor; **kesin değerleri sınav sayfasından ve sınav başındaki ekrandan al**. Bu deneme 50 soru / 100 dakika varsayar |
| Soru türleri | Tek seçim, çoklu seçim, evet/hayır tabloları, sürükle-bırak (sıralama/eşleştirme), cümle/kod tamamlama (açılır liste), senaryo ve **vaka çalışması** |
| Nerede | **Pearson VUE** test merkezi veya **çevrimiçi gözetimli** (online proctored) |
| Geçerlilik | Associate sertifikaları süreli olup (genellikle 1 yıl) Microsoft Learn'de **ücretsiz yenileme değerlendirmesi** ile uzatılır — güncel kuralı doğrula |
| Hazırlık materyali | Yeni bir sınav olduğu için **resmî ücretsiz practice assessment henüz yayımlanmamış veya sınırlı olabilir**; üçüncü taraf "practice test" satıcıları çoğunlukla doğrulanmamış içerik sunar. Bu deneme o boşluğu doldurmak içindir, resmî değerlendirmenin yerine geçmez |

---

## 2. Neden?

- **Bilmek ile sınavda göstermek farklıdır.** Konuları tek tek çalışırken her soru bağlamıyla gelir; gerçek sınavda bir Cosmos partition key sorusu hemen ardından bir KEDA kuralı sorusuyla yan yana gelir. Benzer şıklar (Secrets User ↔ Contributor, `AcrPull` ↔ `AcrPush`, HNSW ↔ IVFFlat, TTL ↔ eviction, Event Grid ↔ Service Bus) karışır; karışık bir deneme bunu ortaya çıkarır.
- **Format riski:** Associate sınavlarında **kod/CLI/KQL tamamlama** ve **vaka çalışması** vardır. Bunlar bilgiden çok *okuma ve çıkarım disiplini* ister; ilk kez gerçek sınavda görmek zaman kaybettirir.
- **Zayıf alanı ölçmek:** En ağır alan **B (%30-35)** dört konuya (C2.4–C2.7) yayılır. Alan bazlı puan, "hangi C2.x'i tekrar edeyim?" sorusunu veriyle cevaplar.
- **Yeni sınav, az kaynak:** Resmî practice assessment eksikse ve forumlardaki sorular güvenilmezse, kendi zayıf noktanı bulmanın en iyi yolu **gerekçeli** bir deneme sınavıdır.

---

## 3. Nasıl?

### 3.1 Deneme sınavını kullanma (önerilen akış)

1. **Hazırlık:** C2.1–C2.13 konularının quiz'lerinde ≥ %80 al; en azından her konunun lab'ını (veya emülatör/yerel karşılığını) bir kez yaptığından emin ol. Deneme bir **ölçüm aracıdır**, ders değil.
2. **Zamanlı ve kapalı kitap çöz:** 50 soru için **100 dakika** ayır. Dokümanı, README'leri, kod editörünü ve `<details>` bloklarını açma. Cevaplarını ayrı bir kâğıda/dosyaya yaz (soru no → şık).
3. **Puanla:** Her sorunun cevabı kendi `<details>` bloğundadır. `quiz.md` sonundaki **alan bazlı puan tablosunu** doldur (çok parçalı sorularda tüm parçalar doğruysa 1 puan).
4. **Yanlışları sınıflandır:** Her yanlış için (a) hangi C2.x, (b) **bilgi eksiği mi, dikkat hatası mı?** (örn. `__` yerine `:` yazdım — sözdizimi bilgisi; `count()` ↔ `sum(itemCount)` — kavram; vakada bir gereksinimi kaçırdım — okuma hatası).
5. **Tekrar et:** Bir konuda 2+ bilgi eksiği varsa o konunun README + lab + quiz döngüsünü baştan yap.
6. **Bir hafta sonra** soruları **farklı sırayla** yeniden çöz; ezber değil anlama ölçülsün diye her cevabın gerekçesini kendi cümlelerinle yaz.
7. Resmî practice assessment (varsa) veya yeniden sertifikasyon değerlendirmesi formatını görmek için kapalı kitap çöz; istikrarlı **%80+** hazır olduğunun işaretidir.

### 3.2 Alan bazlı puanlama

| Toplam | Yorum | Eylem |
|---|---|---|
| ≥ 42 / 50 (%84+) ve her alan ≥ %70 | Hazır | Sınav tarihi al; son hafta anahtar kelime tablosunu (3.5) ve "Karışan çiftler" tablosunu (5) tekrar et |
| 35–41 | Neredeyse | Zayıf alanın konularını README + lab ile tekrar et, bir hafta sonra yeniden çöz |
| < 35 | Hazır değil | Konu döngüsüne dön (README → lab → konu quiz'i); deneme bir sonraki tur için sakla |

- **Toplam yüzde yetmez, alan dağılımına bak:** Alan B'de %50 alıp diğer alanlarda %95 alan biri %70 civarına düşer ve ağırlığı en yüksek alanda zayıf kalır. Gerçek sınav sonuç raporu da **alan bazında** performans gösterir; bu denemenin tablosu onun provasıdır.
- **Alan içinde konu dağılımına bak:** Alan B'de 17 sorudan C2.4 (5), C2.5 (4), C2.6 (4), C2.7 (4). Tek bir konuda 3 yanlış varsa sorun alan değil o konudur.
- **Yüzdeyi 700/1000 ile birebir eşleme:** ölçeklenmiş puan soru ağırlıklarına ve sınav formuna bağlıdır; buradaki yüzde yalnızca göstergedir.

### 3.3 Vaka çalışması ve kod / CLI / KQL tamamlama soruları

**Vaka çalışması (case study).** Bir senaryo (mevcut mimari, iş gereksinimleri, kısıtlar) ve ona bağlı birkaç soru gelir. Genel kural: vaka sorularının tamamı aynı senaryoyu paylaşır; bazı sınav arayüzlerinde bir vaka bölümünü bitirip ilerledikten sonra **geri dönemezsin** — sınav ekranındaki uyarıyı oku ve her bölümde kalan süreyi gözle.

1. **Önce gereksinimleri ve kısıtları listele.** Senaryoyu okurken numaralı bir liste çıkar: *"R1 değişmez imaj, R2 parolasız, R3 %10 canary, R4 sıfıra ölçek, R5 rotation"*. Her soru bu listedeki bir veya iki maddeyi sorar.
2. **Anahtar kelimeleri işaretle:** "en az yönetim yükü", "en düşük maliyet", "en az yetki", "secret'sız", "yeniden dağıtım olmadan", "tutarlı", "yalnızca VNet'ten". Bunlar şıkları eler.
3. **Soruyu okumadan şıklara bakma:** Önce sorunun hangi gereksinimi hedeflediğine karar ver, sonra şıkları bu gereksinimle test et.
4. **Vaka dışı bilgiyi uydurma:** Senaryoda yazmayan bir kısıt (örn. "bütçe sınırsız") varsayma; ama senaryodaki sayıları kullan (replica sayısı, `maxReplicas`, `messageCount`).
5. **Vaka süresi:** 4 soruluk bir vakaya 10-12 dakikadan fazla harcama; takılırsan işaretle ve geç (kısıtlar varsa geri dönülemeyebileceğini unutma).

**Kod tamamlama (C#/Python).** Boşluk genellikle bir **tip/metot adı**, bir **ayar anahtarı** veya bir **boolean**'dır. Teknik:

| Teknik | Örnek |
|---|---|
| Boşluğun etrafındaki **imzaya** bak: dönüş tipi, parametre sayısı, `await` | `ReadItemAsync<Order>(id, new PartitionKey(...))` → point read; `GetItemQueryIterator` `FeedIterator` döner, `await` ile tek item gelmez |
| **Hangi paket/SDK?** (Azure.Identity, Azure.Messaging.ServiceBus, Microsoft.Azure.Cosmos) | `SubQueue.DeadLetter`, `VectorDistance`, `UseAzureMonitor()` |
| **Sürüm/model farkı:** isolated worker ↔ in-process, Python v1 ↔ v2 | Python v2: decorator; .NET isolated: attribute; `function.json` Python v2'de yok |
| **Idempotent / güvenli varsayılan** | `refreshAll: true`, `AddSource("…")`, `DIALECT 2` |
| Python ↔ C# adı **birebir aynı olmaz** | C# `SetSecretAsync` ↔ Python `set_secret` |

**CLI tamamlama.** `az` komut grupları ve parametre adları: ezber yerine **desen**:

- `az acr build/run/task/import`, `az containerapp revision/ingress traffic/secret/replica`, `az aks create --attach-acr/get-credentials`, `az cosmosdb sql role assignment`, `az servicebus topic subscription rule`, `az eventgrid event-subscription create --advanced-filter/--max-delivery-attempts/--event-ttl/--deadletter-endpoint`, `az keyvault create --enable-rbac-authorization --enable-purge-protection`, `az appconfig kv/feature`, `az monitor scheduled-query`.
- Rol soruları: `--assignee-object-id` + `--assignee-principal-type ServicePrincipal` + `--role` + `--scope`. **Rol adını** kapsamla birlikte düşün: `AcrPull` registry'de, *Key Vault Secrets User* vault'ta, *Azure Service Bus Data Receiver* namespace'te, Cosmos için **`az cosmosdb sql role assignment`** (Azure RBAC değil).

**KQL tamamlama/okuma:**

| Operatör ikilisi | Ayırt eden |
|---|---|
| `extend` ↔ `project` | Yeni hesaplanan sütun ↔ sütun seçimi |
| `top N by x` ↔ `take N` | Sıralı ilk N ↔ rastgele N |
| `summarize … by bin(timestamp, 5m)` | Zaman kovası; **boş kova satır üretmez** |
| `count()` ↔ `sum(itemCount)` | Örneklenmiş veride eksik ↔ gerçek sayı |
| `has` ↔ `contains` | Tam terim (indeksli, hızlı) ↔ alt dize |
| `==` ↔ `=~` | Büyük/küçük harfe duyarlı ↔ duyarsız |
| `join` varsayılanı | `innerunique` tuzağı → `kind=inner` yaz |

Klasik App Insights adları (`requests`, `timestamp`, `duration`, `operation_Id`) ile workspace adları (`AppRequests`, `TimeGenerated`, `DurationMs`, `OperationId`) arasındaki eşlemeyi bil; sınav ikisini de kullanabilir (→ [C2.13](../13-opentelemetry-kql/README.md)).

### 3.4 Soru çözme teknikleri

| Teknik | Nasıl? | Örnek |
|---|---|---|
| **Önce soru kökünü oku** | Son cümle asıl soruyu taşır: "en az yönetim yükü", "en düşük maliyet", "en az yetki" | "…**en az yönetim yükü** ile container'ları çalıştır" → Container Apps (AKS değil) |
| **NOT / EXCEPT** | Soruyu olumluya çevir, her şıkkı "doğru mu?" diye işaretle | "Hangisi revision oluşturmaz?" → secret/ingress/traffic |
| **Eleme** | Kategori dışı (başka servis), fazla yetkili, mutlak ifadeli, "yalnızca"/"her zaman" şıkları at | `Contributor` secret okutmaz; `Owner` fazla yetkilidir |
| **Evet/hayır tablosu** | Her satır **bağımsızdır** | "Hepsi evet olamaz" kuralı yok |
| **"En iyi" cevap** | Birden çok şık çalışabilir; senaryonun kısıtını en iyi karşılayanı seç | Storage Queue de kuyruk olur, ama DLQ/session isteniyorsa Service Bus |
| **Çoklu seçim** | Kaç cevap istendiğini oku ("iki doğru cevap seç"); kısmi puan olabilir ama varsayma | — |
| **Sıralama** | Önce ilk ve son adımı yerleştir, ortayı doldur | Rotation: olay → Event Grid → yeni credential → yeni sürüm → uygulama alır |
| **Sayısal hesap** | Önce tavanı (`maxReplicas`) kontrol et | `ceil(300/25) = 12` → `maxReplicas 8` ile sınırlı |
| **Bilmiyorsan işaretle** | 2 dakikayı aşan soruyu işaretle; boş bırakma (yanlış cevap için ek ceza olmadığı varsayımını sınav başında doğrula) | — |

### 3.5 Anahtar kelime → servis/çözüm tablosu

| Senaryodaki ifade | Büyük olasılıkla | Konu |
|---|---|---|
| "yerel Docker olmadan image derle" | `az acr build` | C2.1 |
| "base image güncellenince otomatik yeniden build" | ACR Task + base image update trigger | C2.1 |
| "geo-replication / private endpoint" | ACR **Premium** | C2.1 |
| "test edilen image'ı birebir çalıştır" | **Digest** ile deploy | C2.1 |
| "image çekmek için en az yetki" | Managed identity + **`AcrPull`** | C2.1 |
| "önce %10 trafik, sonra geri al" | **Multiple** revision mode + traffic weights | C2.2 |
| "kuyruk uzunluğuna göre ölçek, boştayken sıfır" | KEDA `azure-servicebus` kuralı, `min-replicas 0` | C2.2 |
| "secret değişti, yeni revision?" | Hayır (application-scope) | C2.2 |
| "`ImagePullBackOff` + unauthorized / manifest unknown" | `AcrPull` eksik / yanlış tag | C2.3 |
| "`OOMKilled`, Exit Code 137" | Bellek limiti | C2.3 |
| "`READY 0/1`" / "endpoints boş" | Readiness probe / Service selector–label | C2.3 |
| "Kubernetes API gerekmiyor, scale-to-zero" | **Container Apps** | C2.2/C2.3 |
| "id + partition key ile oku" | `ReadItemAsync` (point read) | C2.4 |
| "düşük cardinality'li partition key, 429" | Hot partition → `/customerId` vb. | C2.4 |
| "kullanıcı kendi yazdığını görsün, en ucuz" | **Session** consistency | C2.4 |
| "iki alanla ORDER BY hata veriyor" | **Composite index** | C2.4 |
| "yazma RU'su yüksek, sorgular 3 alanda" | Dar indexing policy (allow-list) | C2.4 |
| "milyonlarca vector, düşük gecikme" | **diskANN** | C2.5 |
| "dimension mismatch" | Policy `dimensions` ≠ model çıktısı | C2.5 |
| "worker yeniden başladı, kaldığı yerden" | **Lease container** | C2.5 |
| "silinen item change feed'de yok" | Soft delete / all versions and deletes | C2.5 |
| "boş tabloda kurulabilen, en iyi recall" | **HNSW** (IVFFlat veri sonrası) | C2.6 |
| "index var ama Seq Scan" | Operatör ↔ operator class uyuşmazlığı | C2.6 |
| "tenant filtresiyle eksik sonuç" | `hnsw.iterative_scan` / partitioning | C2.6 |
| "too many connections" | Singleton pool + **PgBouncer (6432)** | C2.6 |
| "vector araması için Redis" | **RediSearch** (oluştururken), Enterprise policy, NoEviction | C2.7 |
| "bellek dolunca hangi anahtar silinsin" ↔ "süre dolunca sil" | Eviction ↔ **TTL** | C2.7 |
| "benzer sorulara LLM'i tekrar çağırma" | **Semantic cache** | C2.7 |
| "VNet + scale-to-zero" | Functions **Flex Consumption** | C2.8 |
| "function'ı başlatan olay" | **Trigger** (function başına tam bir) | C2.8 |
| "secret'sız Service Bus/Cosmos bağlantısı" | `<önek>__fullyQualifiedNamespace` / `__accountEndpoint` + doğru rol | C2.8 |
| "her mesaj tek tüketici" ↔ "aynı mesaj birçok tüketici" | **Queue** ↔ **Topic + subscription** | C2.9 |
| "sırayla işle" / "çift gönderimi yut" | **Session** / **Duplicate detection** | C2.9 |
| "zehirli mesaj sistemi tıkamasın" | **DLQ** + `MaxDeliveryCount` | C2.9 |
| "subscription filtresi çalışmıyor" | `$Default` kuralı silinmedi | C2.9 |
| "Azure servisi olayına tepki" | Event Grid **system topic** | C2.10 |
| "payload alanına göre yönlendir" | **Advanced filter** (`data.*`) | C2.10 |
| "teslim edilemeyen olayları sakla" | **Dead-letter** (varsayılan kapalı) | C2.10 |
| "sıralama, session, işleme garantisi" | Service Bus (Event Grid değil) | C2.10 |
| "secret'sız bağlan" | **Managed identity** | C2.11 |
| "yalnızca secret oku, en az yetki" | **Key Vault Secrets User** | C2.11 |
| "Contributor var ama secret okunamıyor" | Control plane ≠ data plane | C2.11 |
| "süre dolmadan rotation tetikle" | Event Grid `SecretNearExpiry` → Function | C2.11 |
| "kimse kalıcı silemesin" | **Purge protection** | C2.11 |
| "ortam başına farklı değer" | **Label** (yetki ayrımı için ayrı store) | C2.12 |
| "restart etmeden yeniden yükle" | **Sentinel key** + `refreshAll: true` | C2.12 |
| "gizli olmayan ayar ↔ secret" | App Configuration ↔ Key Vault (+ referans) | C2.12 |
| "kullanıcıların %20'sine, tutarlı" | Feature flag **Targeting** (Percentage rastgele) | C2.12 |
| "servisler arası istek korelasyonu" | W3C `traceparent` / context propagation | C2.13 |
| "Azure Monitor'e az kodla telemetri" | OpenTelemetry **Distro** (`UseAzureMonitor`) | C2.13 |
| "örneklemeli veride gerçek sayı" | `sum(itemCount)` | C2.13 |
| "hata oranı eşiği aşınca e-posta" | Log alert (veya metric alert) + **action group** | C2.13 |

### 3.6 Zaman yönetimi

- Sınav başlamadan gösterilen **soru sayısı ve süreyi** not et; soru başına düşen süreyi hesapla (50 soru / 100 dk → ≈ 2 dk).
- Dağılım önerisi: kısa tek seçim/evet-hayır **~1 dk**; kod/CLI/KQL tamamlama **~2 dk**; sıralama/eşleştirme **~2 dk**; vaka sorusu **~2,5 dk** + vakayı okumak için **~3 dk**.
- İlk geçişte bildiklerini hızla cevapla, emin olmadıklarını **işaretle**; ikinci geçişe en az %15 süre bırak.
- Uzun senaryolarda önce son cümleyi (asıl soruyu), sonra senaryoyu oku.
- Bazı soru grupları (vaka, geri dönüşsüz bölümler) cevaplandıktan sonra **geri dönülemeyebilir**; kullanılabilir olduğunda "review" ekranını son dakikalara değil, ortaya yakın kullan.
- **Microsoft Learn erişimi:** Microsoft bazı rol tabanlı sınavlarda sınav içinde Microsoft Learn'e (arama dahil) erişim sunmaktadır. AI-200 için bunun geçerli olup olmadığını ve kurallarını **güncel sınav sayfasından kontrol et**; geçerli olsa bile her soru için dokümana bakmak süreyi yetmez kılar — erişim bir **doğrulama aracıdır** (parametre adı, CLI bayrağı gibi), öğrenme yerine geçmez.

### 3.7 Sınav günü

| | Test merkezi (Pearson VUE) | Çevrimiçi gözetimli (online proctored) |
|---|---|---|
| Ortam | Hazır bilgisayar, gözetmen yerinde | Kendi bilgisayarın; webcam ve mikrofon zorunlu |
| Hazırlık | Kimlik belgesi, randevudan önce varış | **Sistem testi** (önceden), boş masa/oda, check-in fotoğrafları |
| Risk | Ulaşım | İnternet kopması, ortam kuralları (birinin odaya girmesi, ekrandan uzaklaşmak) |
| Öneri | İlk sınavında stres azaltır | Esnek saat; check-in için 15-30 dk erken başla |

- Sınav arayüzünü önceden görmek için Microsoft'un **exam sandbox** (sınav deneyimi demosu) sayfasını dene; soru türlerini (sürükle-bırak, açılır liste, vaka) orada tanı.
- Kimlikteki ad, sınav profilindeki adla **aynı** olmalıdır.
- Sonuç genellikle sınav bitiminde görünür; başarısızlıkta **alan bazlı** performans raporu verilir — bu repodaki C2.x tekrar planına doğrudan eşlenir. Yeniden girme bekleme süreleri için resmî **retake policy**'ye bak.

---

## 4. Gerçek dünya senaryosu

Bir backend geliştirici AI-200'e yedi haftalık plana göre hazırlanıyor (→ C2 modül sırası):

- **Hafta 1-2:** C2.1–C2.3 (containerized) — her konunun lab'ı kendi aboneliğinde, bütçe alarmı kurulu; her lab `az group delete` ile kapatılıyor. AKS lab'ı yalnızca kısa süre açık tutuluyor.
- **Hafta 3-4:** C2.4–C2.7 (veri) — Cosmos free tier/serverless, yerelde `pgvector` imajı, Redis Stack; **en büyük alan** olduğu için iki hafta ayrıldı.
- **Hafta 5:** C2.8–C2.10 (Functions, Service Bus, Event Grid) — emülatörlerle yerel, Event Grid için kısa Azure lab'ı.
- **Hafta 6:** C2.11–C2.13 (Key Vault, App Configuration, OTel/KQL).
- **Hafta 7, gün 1:** Bu deneme sınavı zamanlı: **36/50**. Alan tablosu: A 9/11, **B 9/17**, C 9/11, D 9/11. B'deki 8 yanlışın 4'ü C2.6 (operator class, iterative scan, PgBouncer), 3'ü C2.7 (modülün oluştururken seçilmesi, TTL ↔ eviction), 1'i C2.4.
- **Gün 2-3:** C2.6 ve C2.7 README + lab'ları tekrar; C2.7'de Redis Stack ile `FT.CREATE`/`FT.SEARCH` yeniden yazıldı; anahtar kelime tablosu (3.5) el yazısıyla çıkarıldı.
- **Gün 4:** Denemenin B alanı soruları farklı sırayla tekrar: 15/17. Resmî practice assessment (yayımlandıysa) kapalı kitap çözüldü.
- **Gün 6:** Çevrimiçi gözetimli sınav; önceki gün sistem testi yapılmış, oda hazırlanmış.

Ders: toplam puan (%72) tek başına "hazırsın" demiyordu; **alan B'deki %53** tekrarın yönünü gösterdi.

---

## 5. Sınav perspektifi

**Microsoft'un sevdiği kalıplar ve tuzaklar** (tüm modülden en sık karışanlar):

| Karışan çift | Ayırt eden soru |
|---|---|
| `AcrPull` ↔ `AcrPush` ↔ `Contributor` | Yalnızca çekme mi, push mu, registry yönetimi mi? (en az yetki) |
| Tag ↔ digest | Değişebilir işaretçi mi, değişmez içerik kimliği mi? |
| Single ↔ multiple revision mode | Trafik bölme/canary var mı? |
| Revision-scope ↔ application-scope değişiklik | Image/env/scale kuralı (yeni revision) ↔ secret/ingress/traffic (yeni revision yok) |
| Container Apps ↔ AKS ↔ App Service ↔ Functions | Kubernetes API gerekiyor mu? Olay güdümlü, sıfıra ölçek, operasyon yükü? |
| Liveness ↔ readiness | Yeniden başlatır mı, trafikten mi çıkarır? |
| Partition key ↔ `id` | Cardinality/dağılım/sorgu uyumu; point read |
| Strong ↔ Session ↔ Eventual | "Kendi yazdığını gör" Session ister, Strong pahalıdır |
| Cosmos vector `flat` ↔ `quantizedFlat` ↔ `diskANN` | Veri ölçeği ve boyut |
| HNSW ↔ IVFFlat | Boş tabloda kurulur mu? Build süresi/bellek? |
| TTL ↔ eviction | Süre mi, bellek baskısı mı? |
| Cosmos ↔ PostgreSQL ↔ Redis (vector) | Global/esnek şema ↔ ACID+JOIN ↔ geçici/ultra düşük gecikme |
| Trigger ↔ binding | Function'ı kim başlatır? |
| Flex ↔ Consumption ↔ Premium ↔ Dedicated | VNet + sıfıra ölçek ↔ soğuk başlangıç yok + slot ↔ mevcut plan |
| Queue ↔ topic | Tek tüketici ↔ her abone kopya |
| Abandon ↔ dead-letter ↔ defer | Sayaç artırır ↔ DLQ'ya taşır ↔ `SequenceNumber` ile sonra al |
| Event Grid ↔ Service Bus ↔ Event Hubs | Olay bildirimi ↔ iş komutu ↔ telemetri akışı |
| System-assigned ↔ user-assigned MI | Kaynakla ölür ↔ paylaşılır, bağımsız yaşar |
| Control plane ↔ data plane rolleri | `Contributor` ↔ *Secrets User*, Cosmos data-plane rolü |
| App Configuration ↔ Key Vault | Gizli olmayan ayar ↔ secret (+ Key Vault referansı) |
| Percentage ↔ Targeting ↔ TimeWindow | Rastgele ↔ kullanıcıya tutarlı ↔ tarih aralığı |
| Metric alert ↔ log search alert | Hızlı/ucuz eşik ↔ esnek KQL, ingestion gecikmeli |
| `count()` ↔ `sum(itemCount)` | Sampling var mı? |

**Sınavda olmayanlar / az olanlar:** fiyat rakamları, tam limit sayıları, portal ekranlarının yeri. Kavram, yapılandırma anahtarı ve doğru komut/ayar adı önemlidir. AZ-204'ün bazı konuları (App Service ayrıntıları, API Management, Event Hubs ayrıntıları, Microsoft Graph) AI-200 hedeflerinde görünmez — güncel müfredatla karşılaştır.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Braindump/"çıkmış soru" ezberlemek | Microsoft'un sınav sözleşmesine (NDA) aykırıdır, sertifika iptaline yol açabilir; sorular değişir — kavramı öğren, bu depodaki gibi **özgün** sorularla çalış |
| Yalnızca deneme çözerek çalışmak | Önce konu (README + lab), sonra konu quiz'i, en son deneme |
| Lab yapmadan kod/CLI sorularına girmek | Parametre adları ve rol atama deseni elde yazılmadan oturmaz; lab'ları emülatör/ucuz katmanla yap |
| Vakada gereksinim listesi çıkarmamak | Her vaka sorusu 1-2 gereksinime bağlıdır; liste çıkar |
| Evet/hayır satırlarını birbirine bağlamak | Her satır bağımsızdır |
| "NOT"u kaçırmak | Soru kökündeki olumsuzluğu zihnen altını çiz |
| Çoklu seçimde istenen sayıdan fazla/az seçmek | "İki doğru cevap" ise tam iki |
| Bir soruda 4-5 dakika takılmak | İşaretle, geç, sona dön |
| Bütçe alarmı kurmadan lab yapmak, lab'ı silmemek | AKS/Managed Redis saatlik faturalanır; `az group delete` ile temizle |
| Online sınavda ortam kurallarını hafife almak | Masada telefon/kâğıt, odaya giren biri veya ekrandan uzaklaşmak iptale yol açabilir |
| Müfredat tarihini kontrol etmemek | "skills measured as of" tarihini ve değişiklik kaydını sınavdan önce kontrol et |
| Eski ürün adlarına/varsayımlara güvenmek | Azure Managed Redis (Azure Cache for Redis'in halefi), Functions isolated worker, Docker Content Trust yerine Notation vb. — güncel dokümanı doğrula |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Erken sınav tarihi almak | Motivasyon ve odak | Hazır değilsen tekrar ücreti ve bekleme süresi |
| Test merkezi | Teknik risk yok, sakin ortam | Ulaşım, sınırlı saat |
| Online proctored | Esnek saat ve yer | Bağlantı/ortam riski, sıkı kurallar |
| Çok sayıda deneme sınavı | Format rahatlığı | Ezber riski: aynı soruları tanımaya başlarsın; gerekçeyi açıklayamıyorsan bilmiyorsundur |
| Lab'ları atlamak | Zaman kazancı | Kod/CLI tamamlama ve "hangisi gerçekten böyle çalışır?" sezgisi zayıf kalır |
| Tüm lab'ları gerçek Azure'da yapmak | Gerçekçi davranış (ör. RBAC yayılımı, KEDA ölçeği) | Maliyet ve temizlik yükü; emülatör/yerel karşılık (Cosmos emulator, Service Bus emulator, pgvector Docker, Redis Stack) çoğu kavram için yeter |
| Üçüncü taraf practice test'ler | Soru sayısı fazla | Doğrulanmamış/eski içerik riski; gerekçe zayıf olabilir — resmî dokümanla çapraz kontrol et |
| Microsoft Learn erişimine (varsa) güvenmek | Parametre adını doğrulama | Zaman kaybı; öğrenmeyi ikame etmez |

---

## 8. Kaynaklar

- [AI-200 study guide (skills measured + değişiklik kaydı)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Exam duration and question types](https://learn.microsoft.com/en-us/credentials/support/exam-duration-exam-experience) · [Exam sandbox](https://aka.ms/examdemo)
- [Online exams with Pearson VUE](https://learn.microsoft.com/en-us/credentials/certifications/online-exams) · [Retake policy](https://learn.microsoft.com/en-us/credentials/support/retake-policy)
- [Credentials support (sınav politikaları, gizlilik sözleşmesi, sınav bütünlüğü)](https://learn.microsoft.com/en-us/credentials/support/) · [Sertifika yenileme (renewal assessment)](https://learn.microsoft.com/en-us/credentials/certifications/renew-your-microsoft-certification)
- Bu modül: [C2.1](../01-container-registry/README.md) · [C2.2](../02-container-apps/README.md) · [C2.3](../03-aks-manifests/README.md) · [C2.4](../04-cosmos-db-nosql/README.md) · [C2.5](../05-cosmos-vector-change-feed/README.md) · [C2.6](../06-postgresql-pgvector/README.md) · [C2.7](../07-managed-redis/README.md) · [C2.8](../08-azure-functions/README.md) · [C2.9](../09-service-bus/README.md) · [C2.10](../10-event-grid/README.md) · [C2.11](../11-identity-key-vault/README.md) · [C2.12](../12-app-configuration/README.md) · [C2.13](../13-opentelemetry-kql/README.md)
- Önceki sertifika denemesi (format karşılaştırması için): [C1.15 AZ-900 Deneme Sınavı](../../01-az-900-azure-fundamentals/15-practice-exam/README.md)

> **Doğrulama notu:** AI-200 yeni bir sınavdır; sınav süresi, soru sayısı, resmî practice assessment'ın varlığı, sınav içi Microsoft Learn erişimi ve yenileme kuralları kaynaklarda tutarsız veya değişken ifade ediliyor. Bu README'deki rakamlar (50 soru / 100 dakika) **yalnızca bu denemenin varsayımıdır**; gerçek değerleri sınav sayfasından ve sınav başındaki ekrandan al.
