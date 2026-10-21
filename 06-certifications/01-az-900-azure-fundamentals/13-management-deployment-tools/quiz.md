# C1.13 Azure Kaynaklarını Yönetme ve Dağıtma Araçları — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Describe Azure management and governance* (%30-35) · 16 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, eşleştirme, senaryo); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 13/16**.

---

### 1. Tek seçim
Bir şirket, geliştirme, test ve üretim ortamlarının **aynı şekilde ve tekrar tekrar tutarlı** kurulmasını, altyapı tanımlarının da git'te sürümlenmesini istiyor. Hangisini kullanmalıdır?

- A) Azure portal
- B) ARM template'leri veya Bicep
- C) Azure Advisor
- D) Azure Service Health

<details><summary>Cevap</summary>

**B.** Tekrarlanabilir, sürüm kontrollü, deklaratif dağıtım = infrastructure as code (ARM template / Bicep). Portal elle ve tekrarlanamaz; Advisor ve Service Health izleme araçlarıdır (→ C1.14).
</details>

### 2. Tek seçim
Bir yönetici, kendi bilgisayarına hiçbir araç kuramıyor ve **tarayıcıdan**, kimliği zaten doğrulanmış bir komut satırıyla Azure kaynaklarını yönetmek istiyor. Hangisini kullanmalıdır?

- A) Azure Cloud Shell
- B) Azure Arc
- C) Azure Resource Manager template
- D) Azure mobile app

<details><summary>Cevap</summary>

**A.** Cloud Shell tarayıcı tabanlıdır, kurulum gerektirmez, kimlik hazırdır ve Azure CLI/PowerShell yüklü gelir.
</details>

### 3. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Cloud Shell hem Bash hem de PowerShell deneyimi sunar. | ☐ | ☐ |
| b) Azure CLI yalnızca Linux işletim sisteminde çalışır. | ☐ | ☐ |
| c) Azure CLI ve Azure PowerShell ile büyük ölçüde aynı yönetim işleri yapılabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Azure CLI Windows, macOS ve Linux'ta çalışır; iki araç aynı yetenekleri farklı sözdizimiyle sunar.
</details>

### 4. Tek seçim
Bir şirketin kendi datacenter'ında ve başka bir bulut sağlayıcısında çalışan sunucuları var. Şirket bu sunuculara **Azure Policy** uygulamak ve onları Azure portalında envanter olarak görmek istiyor. Hangi hizmeti kullanmalıdır?

- A) Azure Migrate
- B) Azure Arc
- C) Azure Cloud Shell
- D) Azure Virtual Desktop

<details><summary>Cevap</summary>

**B.** Arc, Azure dışındaki sunucuları ve Kubernetes kümelerini ARM'a yansıtıp Policy, RBAC, Defender ve Monitor'ü uygulanabilir kılar. Azure Migrate (A) sunucuları Azure'a **taşır**; burada taşıma değil yerinde yönetim isteniyor.
</details>

### 5. Tek seçim
Azure portal, Azure CLI, Azure PowerShell ve REST API'den gelen tüm kaynak yönetimi isteklerinin geçtiği, kimlik doğrulama ve yetkilendirmeyi uygulayan katman hangisidir?

- A) Microsoft Entra ID
- B) Azure Resource Manager
- C) Azure Monitor
- D) Azure Policy

<details><summary>Cevap</summary>

**B.** ARM, Azure'un dağıtım ve yönetim servisidir; tüm araçlar aynı ARM API'sini kullanır, bu yüzden RBAC, Policy ve lock her araçta tutarlı uygulanır. Entra ID (A) kimliği doğrular ama yönetim isteklerini işleyen katman ARM'dır.
</details>

### 6. Tek seçim
ARM template'leri hangi biçimde yazılır?

- A) YAML
- B) XML
- C) JSON
- D) PowerShell betiği

<details><summary>Cevap</summary>

**C.** ARM template'leri JSON biçimindedir; Bicep ise ARM JSON'a derlenen, daha okunabilir bir dildir.
</details>

### 7. Çoklu seçim (iki doğru cevap)
Infrastructure as code (IaC) yaklaşımının **hangi ikisi** faydasıdır?

- A) Altyapı tanımları sürüm kontrolünde tutulup gözden geçirilebilir
- B) Kaynakların hiçbir zaman ücretlendirilmemesini sağlar
- C) Aynı ortam tekrar tekrar tutarlı biçimde kurulabilir
- D) Azure portal kullanımını zorunlu hale getirir

<details><summary>Cevap</summary>

**A ve C.** IaC tekrarlanabilirlik, sürüm kontrolü/inceleme, idempotent dağıtım ve otomasyon sağlar. Maliyeti ortadan kaldırmaz; portala bağımlılığı tersine azaltır.
</details>

### 8. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) ARM template'leri deklaratiftir: kurulacak son durumu tanımlarsınız. | ☐ | ☐ |
| b) Aynı ARM template'i aynı resource group'a ikinci kez dağıtmak, kaynakların kopyalarını oluşturur. | ☐ | ☐ |
| c) Resource Manager, şablondaki bağımsız kaynakları paralel olarak dağıtabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Şablon dağıtımı **idempotenttir**: aynı ad ve ayarlarla tekrar uygulandığında kopya oluşmaz, değişiklik yoksa hiçbir şey değişmez. ARM bağımlılıkları çözer, bağımsız kaynakları paralel dağıtır.
</details>

### 9. Tek seçim
Bir ekip, ARM template'lerinin JSON sözdizimini fazla uzun buluyor ve aynı yetenekleri daha kısa, okunabilir bir dille kullanmak istiyor. Ne kullanmalıdır?

- A) Bicep
- B) Azure Policy
- C) Azure Arc
- D) Azure Blueprints

<details><summary>Cevap</summary>

**A.** Bicep, ARM JSON'a derlenen Azure'a özgü bir dildir; aynı yetenekler, daha kısa sözdizimi.
</details>

### 10. Eşleştirme
Her ihtiyacı en uygun araçla eşleştirin: **Azure portal · Cloud Shell · ARM template · Azure Arc**

1. Grafik arayüzle kaynakları keşfetmek ve kişisel bir dashboard oluşturmak.
2. AWS'deki VM'lere Azure tag'leri ve politikaları uygulamak.
3. Kurulumsuz, tarayıcıdan `az` komutları çalıştırmak.
4. 50 müşteri ortamını aynı tanımdan kurmak.

<details><summary>Cevap</summary>

**1 → Azure portal · 2 → Azure Arc · 3 → Cloud Shell · 4 → ARM template** (veya Bicep).
</details>

### 11. Tek seçim
Bir mühendis, bir ARM/Bicep şablonunu dağıtmadan **önce**, mevcut ortamda hangi kaynakların oluşturulacağını, değiştirileceğini veya silineceğini görmek istiyor. Ne kullanmalıdır?

- A) Export template
- B) What-if işlemi
- C) Azure Advisor
- D) Resource lock

<details><summary>Cevap</summary>

**B.** `az deployment group what-if`, dağıtımın etkisini (Create / Modify / NoChange / Delete) önceden gösterir. Export template (A) mevcut durumun şablonunu verir.
</details>

### 12. Senaryo
Bir yönetici portalda bir web uygulamasını elle oluşturdu ve aynı yapılandırmayı otomasyona dönüştürmek için bir **başlangıç şablonu** istiyor. En hızlı yol hangisidir?

- A) Kaynağı silip yeniden oluşturmak
- B) Resource group'un veya kaynağın **Export template** seçeneğiyle mevcut durumun ARM şablonunu almak
- C) Azure Service Health'ten rapor indirmek
- D) Azure Arc'a bağlamak

<details><summary>Cevap</summary>

**B.** Export template (veya oluşturma sırasında *Download a template for automation*) portalda yapılan işin ARM JSON karşılığını verir; otomasyon için parametrelenip temizlenmesi gerekir.
</details>

### 13. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Arc, on-prem sunucuları Azure datacenter'larına taşır. | ☐ | ☐ |
| b) Azure Arc ile Azure dışındaki Kubernetes kümeleri de yönetilebilir. | ☐ | ☐ |
| c) Azure Arc ile bağlanan sunuculara Microsoft Defender for Cloud uygulanabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Arc kaynağı yerinde bırakır ve Azure'dan yönetilebilir kılar; taşıma Azure Migrate'in işidir (→ C1.8).
</details>

### 14. Tek seçim
Bir kullanıcı Azure CLI ile bir kaynak silmeye çalışıyor, ancak kaynakta CanNotDelete lock var. Aynı kullanıcı aynı işlemi portaldan denerse ne olur?

- A) Portal lock'ları yok saydığı için silme başarılı olur
- B) Silme yine reddedilir; çünkü portal da CLI da istekleri Azure Resource Manager üzerinden gönderir
- C) Portal lock'ı otomatik olarak kaldırır
- D) Kaynak başka bir resource group'a taşınır

<details><summary>Cevap</summary>

**B.** Tüm araçlar ARM'dan geçer; lock, RBAC ve Policy hangi araç kullanılırsa kullanılsın aynı şekilde uygulanır (→ C1.12).
</details>

### 15. Tek seçim
Cloud Shell'i **storage account bağlamadan** (ephemeral session) kullanan bir kullanıcı için hangisi doğrudur?

- A) Oturumda oluşturulan dosyalar oturum kapandığında silinir
- B) Cloud Shell hiç açılmaz
- C) Azure CLI kullanılamaz, yalnızca PowerShell çalışır
- D) Dosyalar otomatik olarak OneDrive'a yedeklenir

<details><summary>Cevap</summary>

**A.** Ephemeral oturumda dosyalar kalıcı değildir; kalıcılık için bir storage account (Azure Files paylaşımı) bağlanmalıdır. Her iki kabuk da çalışır.
</details>

### 16. Senaryo
Bir şirketin mevcut süreci: bir mühendis her yeni ortam için 40 adımlık bir belgeyi takip ederek portalda tıklıyor; ortamlar arasında farklar ve hatalar oluşuyor. Hangi değişiklik bu sorunu **en iyi** çözer?

- A) Daha deneyimli bir mühendis görevlendirmek
- B) Ortamı Bicep/ARM template ile tanımlayıp her ortamı aynı şablondan dağıtmak
- C) Her ortama ReadOnly lock uygulamak
- D) Azure Advisor önerilerini haftalık incelemek

<details><summary>Cevap</summary>

**B.** Elle yapılan çok adımlı kurulumun kalıcı çözümü IaC'dir: deklaratif şablon, tekrarlanabilir ve sürüm kontrollü dağıtım. Lock (C) değişikliği engeller ama doğru kurulumu sağlamaz.
</details>

---

**Puanlama:** 15-16 → hazırsın · 11-14 → README 3.4–3.6'yı (ARM, IaC, şablonlar) tekrar et · < 11 → konuyu baştan çalış, 3.7'deki Bicep laboratuvarını uygula.
