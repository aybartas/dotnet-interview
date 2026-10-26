# C1.21 Entegrasyon, Mesajlaşma ve DevOps Araçları — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *genişletilmiş kapsam (bağlam)* · 14 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, eşleştirme, senaryo). Gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ %80** (14 soruda ≥ 12).

---

### 1. Tek seçim
Bir blob storage hesabına dosya yüklendiğinde, ilgilenen birkaç sistemin **hafif bir bildirimle** haberdar edilmesi isteniyor. Hangi servis bu **olay yönlendirme** işi için tasarlanmıştır?

- A) Azure Service Bus
- B) Azure Event Grid
- C) Azure API Management
- D) Azure DevTest Labs

<details><summary>Cevap</summary>

**B.** Event Grid, "bir şey oldu" olaylarını abonelere push ile ileten yönetilen olay yönlendirme servisidir (blob oluşturma olayı bir system topic'tir).
</details>

### 2. Tek seçim
Bir ödeme sistemi, sipariş komutlarını **kaybetmeden**, **sırayla** işlemek; işlenemeyenleri ayrı bir kuyrukta biriktirmek istiyor. Hangi servis uygundur?

- A) Azure Event Grid
- B) Azure Event Hubs
- C) Azure Service Bus
- D) Azure Logic Apps

<details><summary>Cevap</summary>

**C.** Service Bus: kuyruk/topic, session ile FIFO, dead-letter queue, tekrar deneme ve transaction'lar. Event Grid ve Event Hubs bu tür kurumsal mesaj teslimi için tasarlanmamıştır.
</details>

### 3. Tek seçim
Bir mobil oyun, saniyede yüz binlerce kullanım olayını toplayıp sonradan analiz için akışa vermek istiyor; mevcut analiz ekibi **Kafka** istemcileri kullanıyor. Hangi servis en uygundur?

- A) Azure Event Hubs
- B) Azure Storage Queue
- C) Azure Logic Apps
- D) Azure Service Bus Basic

<details><summary>Cevap</summary>

**A.** Event Hubs yüksek hacimli olay akışı alımı içindir; Kafka uyumlu uç nokta sunar ve olaylar saklama süresi boyunca yeniden okunabilir.
</details>

### 4. Eşleştirme
Her servisi tanımıyla eşleştirin: **Logic Apps · Event Grid · Service Bus · Event Hubs · Storage Queue · API Management**

1. Kuyruk/topic, dead-letter ve session sunan kurumsal mesaj aracısı
2. 1000'den fazla connector'lü, görsel tasarımcıyla iş akışı servisi
3. Basit ve ucuz bir Storage hesabı kuyruğu
4. Yüksek hacimli olay/telemetri alımı ve akışı
5. Arka uç API'lerin önünde rate limit, kota ve geliştirici portalı sunan ağ geçidi
6. Azure kaynaklarından gelen olayları abonelere ileten pub/sub servisi

<details><summary>Cevap</summary>

**1 → Service Bus · 2 → Logic Apps · 3 → Storage Queue · 4 → Event Hubs · 5 → API Management · 6 → Event Grid.**
</details>

### 5. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Service Bus Basic katmanı topic ve subscription destekler. | ☐ | ☐ |
| b) Service Bus'ta dead-letter queue, işlenemeyen mesajların park edildiği alt kuyruktur. | ☐ | ☐ |
| c) Event Hubs'ta bir tüketici okuduktan sonra olay akıştan otomatik silinir. | ☐ | ☐ |
| d) Event Grid, mesajların her zaman kesin FIFO sırasıyla teslim edilmesini garanti eder. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır · d) Hayır.** Topic için Standard veya üstü gerekir. Event Hubs olayları saklama süresi boyunca tutar ve yeniden okunabilir. Event Grid için sıralı FIFO garantisi yoktur; sıra gereken yerde Service Bus session'ları kullanılır.
</details>

### 6. Tek seçim
Aşağıdakilerden hangisi **olay (event)** ile **mesaj (message)** arasındaki farkı **en iyi** özetler?

- A) Olay her zaman şifreli, mesaj şifresizdir.
- B) Olay "bir şey oldu" bildirimidir, yayıncı sonucuyla ilgilenmez; mesaj ise tüketicinin işlemesi beklenen veri/komuttur.
- C) Olaylar yalnızca Azure içinde, mesajlar yalnızca Azure dışında kullanılır.
- D) Olay bir veritabanıdır, mesaj bir dosyadır.

<details><summary>Cevap</summary>

**B.** Olay = durum değişikliği bildirimi (Event Grid/Event Hubs); mesaj = işlenmesi beklenen iş verisi veya komut (Service Bus/Storage Queue).
</details>

### 7. Tek seçim
Bir şirket, "yeni müşteri formu gelince CRM'e kayıt ekle, e-posta gönder ve Teams'e bildirim at" iş akışını **mümkün olduğunca az kodla**, hazır connector'larla kurmak istiyor. Hangi servis?

- A) Azure Functions (tamamen kodla)
- B) Azure Logic Apps
- C) Azure Batch
- D) Azure Backup

<details><summary>Cevap</summary>

**B.** Logic Apps görsel tasarımcı ve connector'larla iş akışı kurar. Functions kod gerektirir; özel mantık için Logic Apps ile birlikte kullanılabilir (→ C1.6).
</details>

### 8. Çoklu seçim (iki doğru cevap)
Bir ekip, partner firmaların kullanacağı REST API'lere **abonelik anahtarı**, **günlük kota** ve bir **geliştirici portalı** eklemek istiyor. Aşağıdakilerden **hangi ikisi** doğru bir ifadedir?

- A) Bu ihtiyaç için Azure API Management uygundur.
- B) Bu ihtiyaç için Azure Event Hubs uygundur.
- C) API Management, politikalarla rate limit ve istek/yanıt dönüşümü uygulayabilir.
- D) API Management bir mesaj kuyruğudur.

<details><summary>Cevap</summary>

**A ve C.** APIM HTTP API'ler için ağ geçididir (güvenlik, politika, kota, portal). Event Hubs ve kuyruklar API yönetimi değildir.
</details>

### 9. Eşleştirme
Her ihtiyacı doğru DevOps aracıyla eşleştirin: **Azure Boards · Azure Repos · Azure Pipelines · Azure Test Plans · Azure Artifacts · GitHub Codespaces · GitHub Actions**

1. Backlog, sprint ve iş öğesi takibi
2. NuGet/npm paketlerini bir kaynakta barındırıp paylaşmak
3. Git deposunda kod barındırmak (Azure DevOps tarafı)
4. Manuel ve keşif testlerini planlamak
5. YAML ile derle-test et-yayınla akışı (Azure DevOps tarafı)
6. Tarayıcıdan/VS Code'dan açılan, buluttaki hazır geliştirme ortamı
7. Depo olaylarıyla tetiklenen workflow'lar (GitHub tarafı)

<details><summary>Cevap</summary>

**1 → Boards · 2 → Artifacts · 3 → Repos · 4 → Test Plans · 5 → Pipelines · 6 → Codespaces · 7 → Actions.**
</details>

### 10. Tek seçim
Bir yazılım şirketi, geliştirici/test ortamlarındaki VM'lerin **gece otomatik kapanmasını**, VM sayısı/boyutu politikasını ve harcama kontrolünü kolaylaştırmak istiyor. Hangi servis bu iş için tasarlanmıştır?

- A) Azure DevTest Labs
- B) Azure Event Grid
- C) Azure Site Recovery
- D) Azure API Management

<details><summary>Cevap</summary>

**A.** DevTest Labs, dev/test ortamlarında otomatik kapatma/başlatma, politikalar ve maliyet kontrolü sunar.
</details>

### 11. Senaryo
Bir GitHub Actions workflow'u Azure'a dağıtım yapacak. Güvenlik ekibi, depoda veya GitHub secret'ında **uzun ömürlü bir service principal parolası** bulunmasını istemiyor. En uygun çözüm hangisidir?

- A) Parolayı workflow dosyasına düz metin yazmak
- B) Workflow için OIDC ile workload identity federation kullanıp Entra'da federated credential tanımlamak
- C) Aynı parolayı her depoda paylaşmak
- D) Dağıtımı herkese açık bir Storage anahtarıyla yapmak

<details><summary>Cevap</summary>

**B.** OIDC federasyonunda workflow GitHub'ın verdiği kısa ömürlü token'ı Azure token'ıyla takas eder; kalıcı sır yoktur (`permissions: id-token: write` + `azure/login`).
</details>

### 12. Senaryo
Bir sipariş sistemi, sipariş komutlarını Event Grid ile iletiyordu. Bir tüketici servisi bir süre kapalı kaldığında, retry süresi dolan siparişler kayboldu. Sorunun **temel nedeni** ve **doğru düzeltme** hangisidir?

- A) Event Grid çok yavaştır; Event Hubs'a geçilmeli
- B) Sipariş bir **komut/iş verisi**dir; kaybolmamalı ve tekrar denenmelidir → Service Bus kuyruğu/topic'i kullanılmalı
- C) Siparişler şifrelenmediği için kaybolmuştur; disk şifrelemesi açılmalı
- D) Logic Apps eklenirse sorun çözülür

<details><summary>Cevap</summary>

**B.** Event Grid hafif "bir şey oldu" bildirimleri içindir. Güvenilir teslim, kalıcı kuyruk, dead-letter ve tekrar deneme Service Bus'ın görevidir.
</details>

### 13. Sıralama
Bir CI/CD akışının mantıksal sırasını yazın: **Üretime yayınla · Kodu commit/push et · Derle ve birim testleri çalıştır · Çıktıyı (artifact) paketle · Staging'de doğrula**

<details><summary>Cevap</summary>

**Commit/push → Derle ve birim testleri çalıştır (CI) → Artifact'ı paketle → Staging'de doğrula → Üretime yayınla (CD, genellikle onayla).**
</details>

### 14. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure DevOps ve GitHub, ikisi de Microsoft'un sahip olduğu ürünlerdir. | ☐ | ☐ |
| b) Azure Storage Queue, topic/subscription (pub/sub) sunar. | ☐ | ☐ |
| c) Logic Apps ve Azure Functions birlikte kullanılabilir (iş akışı bir Function'ı çağırabilir). | ☐ | ☐ |
| d) Azure Deployment Environments, platform ekibinin onaylı IaC şablonlarını geliştiricilere self-service sunmasına yardım eder. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet · d) Evet.** Storage Queue basit kuyruktur; topic ve gelişmiş broker özellikleri Service Bus'tadır.
</details>

---

**Puanlama:** 13-14 → hazırsın · 10-12 → README 3.2 (olay ↔ mesaj) ve 3.3'teki seçim tablosunu tekrar et · < 10 → konuyu baştan çalış ve 3.6'daki laboratuvar ile karar tablosunu uygula.
