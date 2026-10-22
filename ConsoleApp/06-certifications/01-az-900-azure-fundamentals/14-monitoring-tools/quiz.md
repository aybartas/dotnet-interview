# C1.14 Azure İzleme Araçları — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Describe Azure management and governance* (%30-35) · 18 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, eşleştirme, senaryo); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 15/18**.

---

### 1. Tek seçim
Bir şirket, düşük kullanımlı sanal makinelerini tespit edip maliyetini düşürmek için **kişiselleştirilmiş öneriler** almak istiyor. Hangi servisi kullanmalıdır?

- A) Azure Service Health
- B) Azure Advisor
- C) Azure Monitor alerts
- D) Azure Policy

<details><summary>Cevap</summary>

**B.** Advisor, kullanım verisine dayanarak Cost, Reliability, Security, Performance ve Operational Excellence kategorilerinde öneri verir; "az kullanılan VM'i küçült/kapat" tipik bir Cost önerisidir. Policy kural zorlar, öneri vermez.
</details>

### 2. Çoklu seçim (üç doğru cevap)
Aşağıdakilerden **hangi üçü** Azure Advisor öneri kategorisidir?

- A) Reliability
- B) Compliance
- C) Operational Excellence
- D) Cost
- E) Networking

<details><summary>Cevap</summary>

**A, C, D.** Beş kategori: Reliability, Security, Performance, Cost, Operational Excellence. Compliance (uyumluluk) Policy/Defender for Cloud'un, Networking ise bir servis alanının adıdır.
</details>

### 3. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Advisor, önerilerini kaynaklara otomatik olarak uygular. | ☐ | ☐ |
| b) Azure Advisor önerileri ertelenebilir veya kapatılabilir. | ☐ | ☐ |
| c) Azure Advisor güvenlik önerileri de sunar. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Advisor yalnızca önerir; uygulama kararı kullanıcınındır. Öneriler ertelenebilir/kapatılabilir. Security kategorisi vardır (öneriler Defender for Cloud ile entegredir).
</details>

### 4. Tek seçim
Bir yönetici, Microsoft'un önümüzdeki hafta **kendi aboneliğindeki** VM'leri etkileyecek planlı bakımından önceden e-postayla haberdar olmak istiyor. Ne yapmalıdır?

- A) Azure Status sayfasını her gün kontrol etmek
- B) Azure Service Health'te bir Service Health alert oluşturmak
- C) Azure Advisor score'u izlemek
- D) Bir Workbook oluşturmak

<details><summary>Cevap</summary>

**B.** Service Health, **senin** subscription'larını ve region'larını etkileyen hizmet sorunlarını ve **planlı bakımı** gösterir ve uyarı (action group ile e-posta) kurulabilir. Azure Status geneldir ve kişiselleştirilmemiştir.
</details>

### 5. Eşleştirme
Her ihtiyacı doğru bileşenle eşleştirin: **Azure Status · Service Health · Resource Health**

1. Tek bir SQL veritabanının dün gece neden erişilemez olduğunu ve sorunun platformdan mı kaynaklandığını görmek
2. Giriş yapmadan, Azure'un genelinde geniş çaplı bir kesinti olup olmadığını görmek
3. Kullandığım region'larda ve servislerde aktif bir hizmet sorunu ve geçmiş olayların kök neden raporlarını görmek

<details><summary>Cevap</summary>

**1 → Resource Health** (tek kaynak) · **2 → Azure Status** (genel, herkese açık) · **3 → Service Health** (abonelik/bölge kapsamı, RCA raporları).
</details>

### 6. Tek seçim
Bir geliştirici, web uygulamasının **istek yanıt sürelerini, hata oranını ve veritabanı bağımlılık çağrılarını** izlemek istiyor. Hangi servis **en uygundur**?

- A) Application Insights
- B) Azure Advisor
- C) Resource Health
- D) Azure Arc

<details><summary>Cevap</summary>

**A.** Application Insights, Azure Monitor'un uygulama performans izleme (APM) özelliğidir: istekler, hatalar, bağımlılıklar, exception'lar, availability test'ler.
</details>

### 7. Tek seçim
Birden çok kaynaktan toplanan log verisini **sorgulayıp analiz etmek** için Azure Monitor'un hangi aracı kullanılır?

- A) Metrics explorer
- B) Log Analytics
- C) Azure Status
- D) Advisor score

<details><summary>Cevap</summary>

**B.** Log Analytics, log'ları bir workspace'te toplar ve KQL (Kusto Query Language) ile sorgulatır. Metrics explorer sayısal metriklerin grafiği içindir.
</details>

### 8. Senaryo
Bir VM'in CPU kullanımı 10 dakika boyunca %85'in üzerinde kalırsa operasyon ekibine **SMS** gönderilmesi gerekiyor. Hangi iki bileşen birlikte kullanılmalıdır?

- A) Azure Advisor ve Azure Policy
- B) Azure Monitor metric alert ve action group
- C) Service Health ve Azure Status
- D) Log Analytics ve resource lock

<details><summary>Cevap</summary>

**B.** Uyarı kuralı (metric alert) koşulu izler; bildirimi (SMS, e-posta, webhook…) **action group** gönderir.
</details>

### 9. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Monitor, Azure dışındaki (on-prem, diğer bulutlar) kaynaklardan da veri toplayabilir. | ☐ | ☐ |
| b) Application Insights, Azure Monitor'un bir özelliğidir. | ☐ | ☐ |
| c) Azure Monitor alerts yalnızca e-posta gönderebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Action group'lar e-posta, SMS, push, sesli arama, webhook, Logic App, Azure Function, Automation runbook gibi eylemler tetikleyebilir.
</details>

### 10. Tek seçim
Bir denetçi, geçen hafta bir resource group'taki bir storage account'u **kimin sildiğini** öğrenmek istiyor. Nereye bakmalıdır?

- A) Azure Advisor
- B) Azure Activity Log
- C) Azure Status
- D) Application Insights

<details><summary>Cevap</summary>

**B.** Activity Log, subscription düzeyindeki yönetim işlemlerini (kim, ne zaman, hangi kaynağı oluşturdu/değiştirdi/sildi) kaydeder ve Azure Monitor'un bir veri kaynağıdır.
</details>

### 11. Tek seçim
Bir ekip, farklı kaynaklardan metrik grafiklerini ve log sorgu sonuçlarını **tek bir ekranda** toplayıp diğer ekip üyeleriyle **paylaşmak** istiyor. Ne kullanmalıdır?

- A) Azure dashboard
- B) Azure Advisor
- C) Resource lock
- D) Azure Status

<details><summary>Cevap</summary>

**A.** Azure dashboard'ları portalda kutucuklardan oluşan, kaydedilip RBAC ile paylaşılabilen panolardır.
</details>

### 12. Tek seçim
Metrik, log sorgusu, parametre ve açıklama metnini birleştiren **etkileşimli raporlar** oluşturmak için Azure Monitor'un hangi özelliği kullanılır?

- A) Workbooks
- B) Action groups
- C) Service Health
- D) Advisor score

<details><summary>Cevap</summary>

**A.** Workbooks; parametreli, etkileşimli, paylaşılabilir raporlardır ve galeride hazır şablonları vardır.
</details>

### 13. Senaryo
Kullanıcılar uygulamanın yavaşladığını bildiriyor. Ekip önce **sorunun Azure platformundan mı yoksa kendi uygulamalarından mı** kaynaklandığını hızla ayırt etmek istiyor. İlk hangi araca bakmalıdır?

- A) Azure Service Health
- B) Azure Advisor
- C) Azure Pricing Calculator
- D) Azure Policy

<details><summary>Cevap</summary>

**A.** Service Health (ve tek kaynak için Resource Health), kullandığın servis/bölgede Azure kaynaklı bir olay olup olmadığını gösterir. Olay yoksa sorun büyük olasılıkla uygulamadadır → Application Insights ve Log Analytics.
</details>

### 14. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Status sayfası, yalnızca oturum açmış kullanıcıların kendi kaynaklarını gösterir. | ☐ | ☐ |
| b) Resource Health, tek bir kaynağın geçmiş sağlık durumunu da gösterir. | ☐ | ☐ |
| c) Service Health, planlı bakım bildirimlerini içerir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Azure Status herkese açık ve geneldir; kişisel görünüm Service Health'tir.
</details>

### 15. Tek seçim
Bir web sitesinin dünyanın farklı noktalarından **düzenli olarak erişilebilir olup olmadığını** test etmek ve erişilemezse uyarı almak isteniyor. Hangi özellik **en uygundur**?

- A) Application Insights availability test'leri
- B) Azure Advisor
- C) Resource lock
- D) Azure DNS

<details><summary>Cevap</summary>

**A.** Availability test'ler uygulamaya belirli aralıklarla farklı konumlardan istek atar ve başarısızlıkta uyarı üretebilir.
</details>

### 16. Tek seçim
Azure Advisor ile Azure Policy arasındaki temel fark nedir?

- A) Advisor kuralları zorlar, Policy öneri verir
- B) Advisor öneri verir, Policy kuralları değerlendirip zorlar
- C) İkisi de yalnızca maliyet içindir
- D) İkisi de yalnızca güvenlik içindir

<details><summary>Cevap</summary>

**B.** Advisor iyi uygulama **önerileri** üretir; Azure Policy kaynakların kurallara uygunluğunu **değerlendirir ve zorlar** (ör. Deny) (→ C1.12).
</details>

### 17. Eşleştirme
Her ihtiyacı doğru araçla eşleştirin: **Advisor · Application Insights · Log Analytics · Service Health**

1. "Tek instance VM'lerimi availability zone'lara yaymam önerilsin."
2. "Uygulamamdaki exception'ları ve yavaş sayfaları göreyim."
3. "Bölgemdeki Azure kesintisinin kök neden raporunu okuyayım."
4. "Son 24 saatteki hata log'larını KQL ile süzeyim."

<details><summary>Cevap</summary>

**1 → Advisor** (Reliability önerisi) · **2 → Application Insights** · **3 → Service Health** (RCA) · **4 → Log Analytics**.
</details>

### 18. Çoklu seçim (iki doğru cevap)
Azure Monitor'un topladığı **iki temel veri türü** hangileridir?

- A) Metrics
- B) Invoices
- C) Logs
- D) Policies

<details><summary>Cevap</summary>

**A ve C.** Metrikler sayısal ve neredeyse gerçek zamanlı değerlerdir; log'lar zaman damgalı olay kayıtlarıdır. Faturalar Cost Management'ın, politikalar Azure Policy'nin konusudur.
</details>

---

**Puanlama:** 16-18 → hazırsın · 12-15 → README 3.2'deki üç Service Health bileşenini ve 5. bölümdeki araç seçimi tablosunu tekrar et · < 12 → konuyu baştan çalış ve 3.6 laboratuvarını yap.
