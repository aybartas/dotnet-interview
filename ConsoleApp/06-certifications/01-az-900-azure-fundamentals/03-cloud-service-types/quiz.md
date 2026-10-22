# C1.3 Cloud Hizmet Türleri: IaaS, PaaS, SaaS ve Serverless — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Describe cloud concepts* (%25-30) · 15 soru
>
> Sorular **özgündür** ve sınav formatındadır; gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Ürün türünü doğrulamak için: `dotnet run -- C1.3 lookup <product>`. Hedef: **≥ 12/15**.

---

### 1. Tek seçim
Bir şirket, kurum içindeki bir uygulamayı **hiçbir kod değişikliği yapmadan** ve işletim sistemi üzerinde tam kontrolü koruyarak buluta taşımak istiyor. Hangi hizmet türü **en uygundur**?

- A) SaaS
- B) PaaS
- C) IaaS
- D) Serverless

<details><summary>Cevap</summary>

**C.** Lift-and-shift ve OS üzerinde tam kontrol = IaaS (Azure Virtual Machines). PaaS ve serverless OS erişimi vermez; SaaS hazır uygulamadır.
</details>

### 2. Eşleştirme
Her ürünü hizmet türüyle eşleştirin: **IaaS · PaaS · SaaS**

1. Azure Virtual Machines
2. Azure SQL Database
3. Microsoft 365
4. Azure App Service
5. Dynamics 365

<details><summary>Cevap</summary>

**1 → IaaS · 2 → PaaS · 3 → SaaS · 4 → PaaS · 5 → SaaS.**
</details>

### 3. Tek seçim
Bir geliştirme ekibi web API'sini yayınlamak istiyor. Ekip işletim sistemi yaması, sunucu yapılandırması veya runtime kurulumu ile uğraşmak istemiyor; yalnızca kodlarına odaklanmak istiyor. Hangi hizmet türü **en uygundur**?

- A) IaaS
- B) PaaS
- C) SaaS
- D) On-premises

<details><summary>Cevap</summary>

**B.** Kendi kodunu çalıştırıp OS/runtime yönetmemek PaaS'tır (ör. App Service). SaaS hazır uygulama olduğundan kendi API'lerini yayınlamaya uygun değildir.
</details>

### 4. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) PaaS'ta işletim sistemi güncellemeleri müşterinin sorumluluğundadır. | ☐ | ☐ |
| b) IaaS, üç hizmet türü arasında müşteriye en fazla kontrolü verir. | ☐ | ☐ |
| c) SaaS'ta müşteri, uygulamanın yeni sürümlerini kendisi kurmak zorundadır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** PaaS'ta OS Microsoft'ta; IaaS en fazla kontrolü verir; SaaS'ta uygulama güncellemeleri sağlayıcıdadır.
</details>

### 5. Tek seçim
Bir şirket SQL Server'ı bir Azure sanal makinesine kurup çalıştırıyor. Bu dağıtım hangi hizmet türüdür?

- A) IaaS
- B) PaaS
- C) SaaS
- D) Serverless

<details><summary>Cevap</summary>

**A.** VM üzerinde SQL Server = IaaS: OS ve SQL Server yamaları müşteride. Yönetilen Azure SQL Database veya Managed Instance olsaydı PaaS olurdu.
</details>

### 6. Tek seçim
Bir kuruluş, 300 çalışanının **hemen** e-posta, takvim ve ofis uygulamalarını kullanmasını istiyor; hiçbir sunucu veya uygulama yönetmek istemiyor. Hangi hizmet türü **en uygundur**?

- A) IaaS
- B) PaaS
- C) SaaS
- D) Hybrid cloud

<details><summary>Cevap</summary>

**C.** Hazır uygulamayı abonelikle kullanmak SaaS'tır (ör. Microsoft 365). D bir dağıtım modelidir, hizmet türü değildir.
</details>

### 7. Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** serverless bilgi işlemin niteliğidir?

- A) Müşteri, kodun çalıştığı sanal makinelerin boyutunu seçer ve yamalar
- B) Kod, bir olay (HTTP isteği, kuyruk mesajı, dosya yükleme) ile tetiklenir
- C) Kaynaklar yüke göre otomatik ölçeklenir ve boşta kaldığında ücret oluşmaz
- D) Kaynaklar 7/24 ayrılmış kapasiteyle sabit ücretle çalışır

<details><summary>Cevap</summary>

**B ve C.** Serverless: olay tetiklemeli, otomatik ölçekleme (sıfıra kadar), kullanım kadar ücret, altyapı yönetimi yok. A IaaS'ı, D sabit kapasiteli planları anlatır.
</details>

### 8. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Serverless'te kodun çalıştığı fiziksel sunucular yoktur. | ☐ | ☐ |
| b) Azure Functions bir serverless bilgi işlem hizmetidir. | ☐ | ☐ |
| c) Azure Logic Apps, kod yazmadan iş akışları oluşturmaya olanak tanır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Sunucular vardır; sağlanmaları ve yönetimleri sağlayıcıdadır. Functions (kod) ve Logic Apps (görsel, low-code iş akışı) Azure'un serverless hizmetleridir.
</details>

### 9. Senaryo
Bir uygulama, kullanıcılar bir Blob Storage hesabına fotoğraf yüklediğinde her fotoğrafın küçük bir kopyasını oluşturmalı. Yüklemeler gün içinde düzensiz geliyor, gece neredeyse hiç yok. Şirket yalnızca işlem yapıldığında ödeme yapmak istiyor. Hangi çözüm **en uygundur**?

- A) 7/24 çalışan bir sanal makine
- B) Azure Functions
- C) Microsoft 365
- D) Kurum içi bir sunucu

<details><summary>Cevap</summary>

**B.** Olay (blob yükleme) ile tetiklenen, düzensiz yük ve "yalnızca çalışırken ödeme" = serverless, Azure Functions. VM (A) boşta da ücretlidir.
</details>

### 10. Sıralama
Aşağıdaki hizmet türlerini, **müşterinin yönetim yükü en fazladan en aza** doğru sıralayın: SaaS · IaaS · PaaS

<details><summary>Cevap</summary>

**IaaS → PaaS → SaaS.** Kontrol de aynı sırayla azalır.
</details>

### 11. Tek seçim
Aşağıdaki senaryolardan hangisi **en tipik** IaaS kullanım senaryosudur?

- A) Çalışanlara hazır bir CRM uygulaması sunmak
- B) Geliştiricilerin, gerektiğinde dakikalar içinde açıp işi bitince silebilecekleri, özel yazılım kurulu test ortamları oluşturmak
- C) Kod yazmadan iki SaaS uygulaması arasında veri aktarmak
- D) Bir web API'sini OS yönetmeden yayınlamak

<details><summary>Cevap</summary>

**B.** Hızla açılıp kapatılan, özel yapılandırmalı test/geliştirme ortamları IaaS'ın klasik senaryosudur. A → SaaS, C → serverless (Logic Apps), D → PaaS.
</details>

### 12. Tek seçim
Bir şirket "SQL Server ile neredeyse tam uyumlu ama yama ve yedeklemeyi Microsoft'un yaptığı" bir veritabanı istiyor. Hangisini seçmelidir ve bu hangi hizmet türüdür?

- A) SQL Server on Azure VM — IaaS
- B) Azure SQL Managed Instance — PaaS
- C) Microsoft 365 — SaaS
- D) Azure Virtual Machines — IaaS

<details><summary>Cevap</summary>

**B.** SQL Managed Instance, SQL Server'a yüksek uyumluluğu yönetilen (PaaS) bir hizmette sunar. A da uyumludur ama yama/yedek müşteride kalır.
</details>

### 13. Cümle tamamlama
"\_\_\_\_\_\_\_\_\_\_ modelinde bulut sağlayıcısı donanımı, işletim sistemini ve runtime'ı yönetir; müşteri ise uygulamasını ve verisini yönetir."

- A) IaaS
- B) PaaS
- C) SaaS
- D) On-premises

<details><summary>Cevap</summary>

**B.** PaaS'ın tanımı: platform (OS, runtime, ara katman) sağlayıcıda, uygulama ve veri müşteride.
</details>

### 14. Senaryo
Bir yazılım ekibi kendi geliştirdiği uygulamayı çalıştıracak ve **en az yönetim yükünü** istiyor. Şıklarda şunlar var: (A) Azure Virtual Machines, (B) Azure App Service, (C) Microsoft 365, (D) kendi datacenter'ları. Hangisini seçmelidir?

<details><summary>Cevap</summary>

**B.** "En az yönetim yükü" sorusunda SaaS (C) ancak ihtiyaç **hazır bir uygulamayla** karşılanıyorsa doğrudur; burada ekip **kendi uygulamasını** çalıştıracak, bu yüzden en az yükü PaaS (App Service) verir.
</details>

### 15. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Bir iş yükü önce lift-and-shift ile IaaS'a taşınıp sonra PaaS'a dönüştürülebilir. | ☐ | ☐ |
| b) SaaS'ta, kullanıcı hesaplarının ve erişim izinlerinin yönetimi sağlayıcının sorumluluğundadır. | ☐ | ☐ |
| c) Çok yüksek ve sürekli yükte, 7/24 açık bir kaynak serverless'ten daha ekonomik olabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Kademeli taşıma (önce rehost, sonra refactor) yaygın bir stratejidir. Hesaplar ve kimlikler her modelde müşteridedir (→ C1.1). Serverless düşük/düzensiz yükte ucuzdur; sürekli yüksek yükte çağrı + GB-saniye toplamı sabit bir plandan fazla olabilir (`dotnet run -- C1.3 4`).
</details>

---

**Puanlama:** 14-15 → hazırsın · 11-13 → README 3.6'daki karar sırasını ve "aynı teknoloji, farklı tür" tablosunu tekrar et · < 11 → konuyu baştan çalış, `dotnet run -- C1.3 all` çıktısını incele.
