# C1.6 Uygulama Barındırma: App Service, Container'lar, AVD, Functions — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Describe Azure architecture and services* (%35-40) · 18 soru
>
> Sorular **özgündür** ve sınav formatındadır; gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 15/18**.

---

### 1. Tek seçim
Bir ekip ASP.NET Core web uygulamasını yayınlamak istiyor. İşletim sistemi yaması yapmak istemiyor; otomatik ölçekleme ve deployment slot'larına ihtiyaç duyuyor. Hangi servis **en uygundur**?

- A) Azure Virtual Machines
- B) Azure App Service
- C) Azure Container Instances
- D) Azure Virtual Desktop

<details><summary>Cevap</summary>

**B.** App Service, OS yönetmeden web/API barındıran PaaS'tır; autoscale ve deployment slot'ları yerleşik özelliklerdir. VM OS yönetimi gerektirir; ACI orkestrasyonsuz tek container içindir; AVD masaüstü sanallaştırmadır.
</details>

### 2. Tek seçim
App Service'te uygulamanın çalıştığı compute kaynağını tanımlayan ve **ücretlendirilen birim** olan nedir?

- A) Deployment slot
- B) Resource group
- C) App Service plan
- D) WebJob

<details><summary>Cevap</summary>

**C.** App Service plan (katman + instance sayısı) ücretlendirilir; birden çok uygulama aynı planı paylaşabilir.
</details>

### 3. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) App Service hem Windows hem Linux üzerinde uygulama çalıştırabilir. | ☐ | ☐ |
| b) App Service'te uygulamanın işletim sistemi yamalarını müşteri uygular. | ☐ | ☐ |
| c) WebJobs, App Service içinde arka plan işi çalıştırmaya olanak tanır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** App Service PaaS'tır; OS yamaları Microsoft'tadır.
</details>

### 4. Tek seçim
Container'ları sanal makinelerden ayıran temel özellik hangisidir?

- A) Container'lar donanımı sanallaştırır, her biri kendi çekirdeğini çalıştırır
- B) Container'lar işletim sistemini sanallaştırır, host'un çekirdeğini paylaşır ve saniyeler içinde başlar
- C) Container'lar yalnızca Windows üzerinde çalışır
- D) Container'lar her zaman VM'lerden daha güçlü izolasyon sağlar

<details><summary>Cevap</summary>

**B.** VM donanımı sanallaştırır (tam OS); container OS'u sanallaştırır — hafif, taşınabilir, hızlı başlar. İzolasyon VM'de daha güçlüdür (D yanlış).
</details>

### 5. Senaryo
Bir veri ekibi ayda bir kez çalışan, yaklaşık 10 dakika süren tek bir container'ı **en basit** yoldan çalıştırmak istiyor. VM veya orkestratör yönetmek istemiyor. Hangi servisi seçmeliler?

- A) Azure Kubernetes Service
- B) Azure Container Instances
- C) Azure Virtual Machine Scale Sets
- D) Azure Virtual Desktop

<details><summary>Cevap</summary>

**B.** Tek container, kısa süre, orkestrasyon gerekmiyor → ACI; saniye bazında ücretlenir. AKS orkestrasyon için ağır bir seçimdir.
</details>

### 6. Senaryo
Bir şirket yüzlerce container'dan oluşan bir mikroservis platformu çalıştıracak; rolling update, servis keşfi ve Kubernetes API'sine tam erişim istiyor. Hangi servis **en uygundur**?

- A) Azure Container Instances
- B) Azure Functions
- C) Azure Kubernetes Service (AKS)
- D) Azure App Service

<details><summary>Cevap</summary>

**C.** Çok sayıda container'ın orkestrasyonu ve Kubernetes API'si = AKS (yönetilen Kubernetes).
</details>

### 7. Tek seçim
Bir ekip container tabanlı API'lerini çalıştırmak istiyor; Kubernetes yönetmek istemiyor, trafik yokken uygulamanın **sıfıra ölçeklenmesini** ve kullanım kadar ödemeyi istiyor. Hangisi **en uygundur**?

- A) Azure Container Apps
- B) Azure Virtual Machines
- C) Azure Kubernetes Service
- D) Azure Virtual Desktop

<details><summary>Cevap</summary>

**A.** Container Apps serverless container platformudur: otomatik ölçek (sıfıra kadar), HTTP ingress, mikroservis özellikleri; Kubernetes karmaşıklığını gizler.
</details>

### 8. Eşleştirme
Her ihtiyacı servisle eşleştirin: **App Service · Container Instances · AKS · Azure Functions**

1. Her gece 02:00'de 3 dakika çalışan temizlik kodu
2. Sürekli çalışan kurumsal web sitesi, OS yönetimi yok
3. Tek bir test container'ını hızlıca ayağa kaldırmak
4. Büyük ölçekli container orkestrasyonu

<details><summary>Cevap</summary>

**1 → Functions** (zamanlayıcı tetikleyici) · **2 → App Service** · **3 → Container Instances** · **4 → AKS**.
</details>

### 9. Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** Azure Functions'ı tetikleyebilir?

- A) Bir Blob Storage container'ına dosya yüklenmesi
- B) Bir VM'in boyutunun değiştirilmesi için portalda menü açılması
- C) Bir zamanlayıcı (cron ifadesi)
- D) Kullanıcının ofis bilgisayarını kapatması

<details><summary>Cevap</summary>

**A ve C.** Blob, zamanlayıcı, HTTP, kuyruk mesajı, Event Grid gibi olaylar tetikleyicidir.
</details>

### 10. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Functions yalnızca HTTP istekleriyle tetiklenebilir. | ☐ | ☐ |
| b) Durable Functions, durum tutan (stateful) uzun süreli iş akışları yazmaya olanak tanır. | ☐ | ☐ |
| c) Serverless planlarda Functions, çalışmadığı sürede de compute ücreti doğurur. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** Birçok tetikleyici vardır; Durable Functions stateful orkestrasyon sağlar; serverless planlarda boşta compute ücreti yoktur.
</details>

### 11. Tek seçim
Bir iş analisti, **kod yazmadan**, görsel bir tasarımcıyla bir SaaS uygulamasındaki yeni kaydı e-postayla bildiren bir iş akışı kurmak istiyor. Hangi servis **en uygundur**?

- A) Azure Functions
- B) Azure Logic Apps
- C) Azure Kubernetes Service
- D) Azure App Service

<details><summary>Cevap</summary>

**B.** Low-code, görsel tasarımcı ve hazır connector'lar = Logic Apps. Functions aynı işi **kodla** yapar.
</details>

### 12. Senaryo
Bir şirket, sözleşmeli çalışanlarının kendi cihazlarından şirket uygulamalarına erişmesini; ancak **verinin bu cihazlara inmemesini** istiyor. Kimlik doğrulamada MFA zorunlu olacak. Hangi servis **en uygundur**?

- A) Azure Virtual Desktop
- B) Azure Container Instances
- C) Azure Functions
- D) Azure App Service

<details><summary>Cevap</summary>

**A.** AVD, masaüstü ve uygulamaları Azure'da çalıştırır; cihaz yalnızca bağlanır, veri bulutta kalır; kimlik Entra ID ve MFA ile yapılır.
</details>

### 13. Tek seçim
Azure Virtual Desktop'ta birden çok kullanıcının **aynı VM üzerinde** eşzamanlı olarak tam Windows 11 masaüstü deneyimi almasını sağlayan özellik hangisidir?

- A) Availability set
- B) Windows 11 Enterprise multi-session
- C) Deployment slot
- D) VM Scale Sets

<details><summary>Cevap</summary>

**B.** Windows 11/10 Enterprise multi-session, AVD'ye özgü çok oturumlu Windows sürümüdür ve VM başına maliyeti düşürür.
</details>

### 14. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Virtual Desktop'ta kullanıcı masaüstleri Azure'daki VM'lerde çalışır. | ☐ | ☐ |
| b) Azure Virtual Desktop, Microsoft Entra ID ile kimlik doğrulamayı ve MFA'yı destekler. | ☐ | ☐ |
| c) Azure Virtual Desktop'a yalnızca Windows cihazlardan bağlanılabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Windows, macOS, iOS, Android istemcileri ve web tarayıcısı desteklenir.
</details>

### 15. Tek seçim
Bir .NET uygulaması App Service'te çalışıyor. Veritabanı connection string'ini **koda yazmadan** uygulamaya vermenin doğru yolu hangisidir?

- A) Connection string'i `appsettings.json`'a yazıp repoya eklemek
- B) App Service'in Application settings / Connection strings ayarlarını kullanmak (uygulamaya environment variable olarak gelir)
- C) Connection string'i bir VM'in masaüstüne kaydetmek
- D) Her istekte kullanıcıdan istemek

<details><summary>Cevap</summary>

**B.** App Service ayarları uygulamaya env var olarak verilir ve ASP.NET Core `IConfiguration` ile okunur; daha güvenli seçenek Key Vault referansıdır (→ C1.10).
</details>

### 16. Sıralama
Aşağıdaki barındırma seçeneklerini **müşterinin yönetim yükü en fazladan en aza** doğru sıralayın: Azure Functions · Virtual Machines · Azure App Service

<details><summary>Cevap</summary>

**Virtual Machines → App Service → Azure Functions.** VM'de OS sende; App Service'te platform Microsoft'ta ama planı sen boyutlandırırsın; serverless Functions'ta kapasite yönetimi de yoktur.
</details>

### 17. Senaryo
Bir uygulama, belirli bir OS sürümüne ve özel bir sürücüye bağımlı; kod değiştirilemiyor. Hangi barındırma seçeneği **zorunlu** hale gelir?

- A) Azure Virtual Machines
- B) Azure App Service
- C) Azure Container Apps
- D) Azure Functions

<details><summary>Cevap</summary>

**A.** OS üzerinde tam kontrol ve özel sürücü kurulumu yalnızca IaaS'ta (VM) mümkündür (→ C1.5).
</details>

### 18. Tek seçim
Bir geliştirici, yeni bir web sürümünü önce `staging` ortamında test edip ardından kesinti olmadan üretimle **yer değiştirmek** istiyor. App Service'in hangi özelliği bunu sağlar?

- A) WebJobs
- B) Deployment slot'ları ve swap
- C) App Service plan
- D) Log stream

<details><summary>Cevap</summary>

**B.** Deployment slot'ları ayrı bir canlı ortamdır; test sonrası **swap** ile üretime alınır, gerekirse geri swap edilir.
</details>

---

**Puanlama:** 16-18 → hazırsın · 12-15 → README 3.5'teki karar ağacını tekrar et · < 12 → konuyu baştan çalış ve 3.6'daki laboratuvarı uygula.
