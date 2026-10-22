# C1.15 AZ-900 Deneme Sınavı — 50 Soru

> Strateji ve kullanım: [README.md](README.md) · Müfredat: *skills measured as of July 20, 2026*
>
> **Kurallar:** 45 dakika, kapalı kitap. Cevaplarını ayrı bir yere yaz; `<details>` bloklarını sınav bitene kadar açma. Her sorunun etiketi, sorunun **konusunu** (C1.x) ve **alanını** gösterir: **A** = Describe cloud concepts · **B** = Describe Azure architecture and services · **C** = Describe Azure management and governance.
>
> Dağılım resmî ağırlıklara göre: **A 14 soru (%28) · B 19 soru (%38) · C 17 soru (%34)**. Sorular **özgündür**; gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Gerçek sınavın puanı ölçeklenmiştir (geçme 700/1000); buradaki yüzde yalnızca bir göstergedir.

---

### 1. `[A · C1.1]` Tek seçim
Aşağıdakilerden hangisi bir **operational expenditure (OpEx)** örneğidir?

- A) Yeni bir datacenter binası inşa etmek
- B) Beş yıl kullanılacak depolama ünitesi satın almak
- C) Kullanılan saat kadar ödenen bir Azure sanal makinesi
- D) Sunucu odası için jeneratör satın almak

<details><summary>Cevap</summary>

**C.** Kullanıma göre dönem dönem ödenen bulut hizmeti OpEx'tir. A, B, D fiziksel altyapıya baştan yapılan yatırımlardır (CapEx).
</details>

### 2. `[B · C1.4]` Tek seçim
Bir uygulamanın, bir Azure region'ı içindeki **tek bir datacenter'ın** tamamen devre dışı kalmasından etkilenmemesi isteniyor. Ne kullanılmalıdır?

- A) Region pair
- B) Availability zone'lar
- C) Resource group
- D) Management group

<details><summary>Cevap</summary>

**B.** Availability zone'lar bir region içinde fiziksel olarak ayrı (bağımsız güç, soğutma, ağ) datacenter gruplarıdır; datacenter arızasına karşı korur. Region pair **bölgesel** felakete karşıdır.
</details>

### 3. `[C · C1.12]` Tek seçim
Bir şirket, çalışanlarının yalnızca **West Europe ve North Europe** region'larında kaynak oluşturabilmesini, diğer bölgelerdeki denemelerin **reddedilmesini** istiyor. Ne kullanmalıdır?

- A) Resource lock
- B) Azure Policy
- C) Azure RBAC
- D) Resource tag

<details><summary>Cevap</summary>

**B.** *Allowed locations* politikası (Deny etkisi) izin verilmeyen bölgede oluşturma isteğini reddeder. RBAC **kimin** ne yapabileceğini belirler, kaynağın **özelliğini** (bölgesini) kısıtlamaz.
</details>

### 4. `[A · C1.3]` Eşleştirme
Her ürünü hizmet türüyle eşleştirin: **IaaS · PaaS · SaaS**

1. Azure Cosmos DB
2. Azure Virtual Machine Scale Sets
3. Microsoft 365
4. Azure App Service

<details><summary>Cevap</summary>

**1 → PaaS · 2 → IaaS · 3 → SaaS · 4 → PaaS.** Yönetilen veritabanı ve web platformu PaaS; VM grupları IaaS; hazır ofis uygulamaları SaaS.
</details>

### 5. `[B · C1.8]` Tek seçim
Bir storage account'taki verinin, birincil bölgede **zone arızasına** dayanması ve **bölgesel bir felaket sırasında da okunabilmesi** gerekiyor. Hangi redundancy seçeneği **en uygundur**?

- A) LRS
- B) ZRS
- C) GRS
- D) RA-GZRS

<details><summary>Cevap</summary>

**D.** GZRS birincil bölgede zone'lara (ZRS) ve ikincil bölgeye kopyalar; **RA-** ön eki ikincil bölgeden sürekli okuma erişimi verir. GRS'de birincil bölge LRS'tir (zone arızasına karşı korumaz) ve ikincil failover olmadan okunamaz.
</details>

### 6. `[C · C1.14]` Tek seçim
Bir yönetici, Microsoft'un **kendi aboneliğindeki** kaynakları etkileyecek **planlı bakım** etkinlikleri hakkında bilgi ve bildirim almak istiyor. Hangi servisi kullanmalıdır?

- A) Azure Advisor
- B) Azure Service Health
- C) Microsoft Defender for Cloud
- D) Azure Policy

<details><summary>Cevap</summary>

**B.** Service Health, abonelik ve region kapsamında hizmet sorunlarını, planlı bakımı ve sağlık tavsiyelerini gösterir; Service Health alert'leri ile bildirim gönderir.
</details>

### 7. `[B · C1.9]` Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Authentication, bir kullanıcının kimliğini doğrulama işlemidir. | ☐ | ☐ |
| b) Authorization, kimliği doğrulanmış bir kullanıcının hangi kaynaklara ne düzeyde erişebileceğini belirler. | ☐ | ☐ |
| c) Bir şifre ve bir güvenlik sorusu birlikte kullanıldığında bu, iki farklı faktörlü kimlik doğrulamadır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Şifre ve güvenlik sorusu ikisi de **bildiğin** şeydir — aynı faktör türü. MFA farklı türlerden faktör ister (bildiğin + sahip olduğun + olduğun).
</details>

### 8. `[A · C1.2]` Tek seçim
Bir video platformu, akşam saatlerinde artan izleyici sayısına göre sunucu sayısını **otomatik** artırıp gece **otomatik** azaltıyor. Bu, bulutun hangi faydasıdır?

- A) Elasticity
- B) Reliability
- C) Governance
- D) Predictability

<details><summary>Cevap</summary>

**A.** Ölçeklemenin yüke göre otomatik yapılması elasticity'dir.
</details>

### 9. `[C · C1.11]` Tek seçim
Bir şirket, kendi datacenter'ındaki sunucuları, depolamayı, enerjiyi ve BT işçiliğini **Azure'a taşıma maliyetiyle karşılaştırmak** istiyor. Hangi araç bu amaçla tasarlanmıştır?

- A) Azure Pricing Calculator
- B) Total Cost of Ownership (TCO) Calculator
- C) Azure Advisor
- D) Azure Monitor

<details><summary>Cevap</summary>

**B.** TCO Calculator on-prem altyapının toplam sahip olma maliyetini Azure ile karşılaştırır. Pricing Calculator ise Azure'da kurulacak bir iş yükünün tahmini maliyetini verir. (TCO Calculator'ın güncel müfredattaki yerini study guide'dan kontrol et.)
</details>

### 10. `[B · C1.5]` Tek seçim
Azure'un **planlı bakım** sırasında tüm VM'leri aynı anda yeniden başlatmasını önlemek ve VM'leri ortak güç/ağ donanımını paylaşmayacak şekilde dağıtmak için ne kullanılır?

- A) Availability set
- B) Resource group
- C) Proximity placement group
- D) Azure Arc

<details><summary>Cevap</summary>

**A.** Availability set, VM'leri **update domain**'lere (planlı bakımda ayrı yeniden başlatma) ve **fault domain**'lere (ayrı güç/ağ donanımı) dağıtır. Proximity placement group ise VM'leri düşük gecikme için birbirine **yakın** yerleştirir.
</details>

### 11. `[C · C1.13]` Tek seçim
Bir şirket, kendi datacenter'ındaki ve başka bir bulut sağlayıcısındaki Linux sunucularını Azure portal'dan görmek ve onlara **Azure Policy** uygulamak istiyor. Ne kullanmalıdır?

- A) Azure Arc
- B) Azure Virtual Desktop
- C) VPN Gateway
- D) Azure Migrate

<details><summary>Cevap</summary>

**A.** Azure Arc, Azure dışındaki sunucuları, Kubernetes kümelerini ve veri servislerini Azure Resource Manager'a yansıtır; Policy, RBAC, Defender for Cloud ve Monitor bu kaynaklara uygulanabilir.
</details>

### 12. `[A · C1.1]` Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) IaaS'ta işletim sistemi güncellemeleri müşterinin sorumluluğundadır. | ☐ | ☐ |
| b) SaaS'ta cihazların (mobil ve PC) güvenliği Microsoft'un sorumluluğundadır. | ☐ | ☐ |
| c) PaaS'ta fiziksel ağın güvenliği Microsoft'un sorumluluğundadır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Cihazlar, veri ve hesaplar her modelde müşteride; fiziksel katmanlar bulutun her modelinde Microsoft'ta; OS IaaS'ta müşteride.
</details>

### 13. `[B · C1.7]` Tek seçim
Bir banka, datacenter'ı ile Azure arasında **public internet üzerinden geçmeyen**, yüksek bant genişlikli ve öngörülebilir gecikmeli bir bağlantı istiyor. Ne kullanmalıdır?

- A) Site-to-site VPN Gateway
- B) Point-to-site VPN
- C) Azure ExpressRoute
- D) VNet peering

<details><summary>Cevap</summary>

**C.** ExpressRoute, bağlantı sağlayıcısı üzerinden **özel** bir bağlantıdır. VPN Gateway şifrelidir ama public internet üzerinden geçer; peering iki VNet'i bağlar.
</details>

### 14. `[C · C1.12]` Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) CanNotDelete (Delete) lock bulunan bir kaynağın ayarları değiştirilebilir. | ☐ | ☐ |
| b) Bir resource group'a uygulanan lock, içindeki kaynaklara miras kalır. | ☐ | ☐ |
| c) Owner rolüne sahip bir kullanıcı, ReadOnly lock bulunan bir kaynağı lock'ı kaldırmadan silebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Delete lock yalnızca silmeyi engeller; lock'lar alt kapsamlara miras kalır ve rol yetkisinden bağımsızdır — Owner bile önce lock'ı kaldırmalıdır.
</details>

### 15. `[B · C1.4]` Çoklu seçim (iki doğru cevap)
Resource group'lar hakkında **hangi iki** ifade doğrudur?

- A) Bir kaynak aynı anda iki resource group'a ait olabilir.
- B) Bir resource group silindiğinde içindeki tüm kaynaklar silinir.
- C) Resource group'lar iç içe yerleştirilebilir.
- D) Bir resource group'taki kaynaklar farklı region'larda olabilir.

<details><summary>Cevap</summary>

**B ve D.** Bir kaynak yalnızca bir gruptadır (A yanlış); gruplar iç içe olmaz (C yanlış); grup silinince içindekiler silinir; gruptaki kaynaklar farklı bölgelerde olabilir.
</details>

### 16. `[A · C1.2]` Tek seçim
SLA'sı **%99,99** olan bir servis için bir ayda izin verilen en fazla kesinti yaklaşık ne kadardır?

- A) ~4 dakika
- B) ~22 dakika
- C) ~44 dakika
- D) ~7 saat

<details><summary>Cevap</summary>

**A.** `730 sa × 0,0001 ≈ 0,073 sa ≈ 4,4 dk`. ~22 dk %99,95'e, ~44 dk %99,9'a karşılık gelir.
</details>

### 17. `[B · C1.6]` Tek seçim
Bir geliştirici tek bir container image'ını, VM veya orkestrasyon kurmadan, **en hızlı ve en basit** şekilde Azure'da çalıştırmak istiyor. Hangi servis **en uygundur**?

- A) Azure Kubernetes Service
- B) Azure Container Instances
- C) Azure Virtual Desktop
- D) Azure Virtual Machines

<details><summary>Cevap</summary>

**B.** Container Instances tek container'ı saniye bazında ücretle, orkestrasyonsuz çalıştırır. AKS çok sayıda container'ın orkestrasyonu içindir.
</details>

### 18. `[C · C1.11]` Tek seçim
Finans ekibi, Azure maliyetlerini **departman ve maliyet merkezine** göre raporlamak istiyor. Kaynaklara ne eklenmelidir?

- A) Resource lock
- B) Tag
- C) Availability zone
- D) Management group

<details><summary>Cevap</summary>

**B.** `department`, `costCenter` gibi tag'ler Cost Management'ta maliyeti filtreleyip gruplamaya olanak tanır. (Tag'ler varsayılan olarak resource group'tan miras alınmaz; Policy ile zorunlu kılınabilir.)
</details>

### 19. `[B · C1.10]` Sıralama
Defense in depth katmanlarını **dıştan içe** doğru sıralayın: Ağ · Veri · Fiziksel güvenlik · Uygulama · Kimlik ve erişim · Compute · Çevre (perimeter)

<details><summary>Cevap</summary>

**Fiziksel güvenlik → Kimlik ve erişim → Çevre (perimeter) → Ağ → Compute → Uygulama → Veri.** En içteki katman, saldırganın asıl hedefi olan veridir.
</details>

### 20. `[C · C1.13]` Tek seçim
Bir ekip, geliştirme, test ve üretim ortamlarını **her seferinde aynı ve tekrarlanabilir** biçimde kurmak, altyapı tanımını sürüm kontrolünde tutmak istiyor. Hangi yaklaşım **en uygundur**?

- A) Kaynakları Azure portal'da elle oluşturmak
- B) ARM template veya Bicep dosyalarıyla infrastructure as code
- C) Azure Advisor önerilerini uygulamak
- D) Resource lock kullanmak

<details><summary>Cevap</summary>

**B.** Deklaratif şablonlar (ARM JSON, Bicep) aynı sonucu tekrar tekrar (idempotent) üretir, kod gibi sürümlenir ve incelenir. Portal tekrarlanabilir otomasyon için uygun değildir.
</details>

### 21. `[A · C1.3]` Çoklu seçim (iki doğru cevap)
Serverless bilgi işlem hakkında **hangi iki** ifade doğrudur?

- A) Kod yalnızca bir olay tetiklediğinde çalışır.
- B) Müşteri, kodun çalıştığı sunucuların işletim sistemini yamalar.
- C) Ücret, genellikle çağrı sayısı ve çalışma süresine göre hesaplanır.
- D) Sunucular fiziksel olarak hiç yoktur.

<details><summary>Cevap</summary>

**A ve C.** Serverless olay tetiklemelidir ve kullanım kadar ücretlendirilir; sunucular vardır ama sağlanmaları ve yönetimleri sağlayıcıdadır (B ve D yanlış).
</details>

### 22. `[B · C1.8]` Tek seçim
Bir şirket, internet bağlantısının yavaş ve pahalı olduğu bir lokasyondan **80 TB** arşiv verisini Azure'a taşımak istiyor. Hangi seçenek **en uygundur**?

- A) AzCopy
- B) Azure Storage Explorer
- C) Azure Data Box
- D) Azure File Sync

<details><summary>Cevap</summary>

**C.** Data Box, büyük veriyi **fiziksel cihazla** taşır; ağ yetersizken tercih edilir. AzCopy ve Storage Explorer ağ üzerinden kopyalar; File Sync sürekli senkronizasyon içindir.
</details>

### 23. `[C · C1.14]` Eşleştirme
Her ihtiyacı doğru araçla eşleştirin: **Azure Advisor · Application Insights · Log Analytics · Resource Health**

1. Bir web uygulamasındaki yavaş isteklerin ve exception'ların izlenmesi
2. Tek bir VM'in geçmiş erişilebilirlik durumunun ve platform kaynaklı bir sorun olup olmadığının görülmesi
3. Birden çok kaynaktan toplanan log'ların KQL ile sorgulanması
4. Güvenilirlik ve maliyet için kişiselleştirilmiş önerilerin alınması

<details><summary>Cevap</summary>

**1 → Application Insights · 2 → Resource Health · 3 → Log Analytics · 4 → Azure Advisor.**
</details>

### 24. `[A · C1.3]` Senaryo
Bir şirketin muhasebe yazılımı, belirli bir işletim sistemi sürümü ve özel bir donanım sürücüsü gerektiriyor; yazılımın kodu değiştirilemiyor. Şirket bu yazılımı buluta taşımak istiyor. Hangi hizmet türü **en uygundur**?

- A) SaaS
- B) PaaS
- C) Serverless
- D) IaaS

<details><summary>Cevap</summary>

**D.** OS ve sürücü üzerinde tam kontrol ve kod değişikliği olmadan taşıma (lift-and-shift) IaaS'tır (Azure Virtual Machines).
</details>

### 25. `[A · C1.1]` Senaryo
Bir hastane, hasta kayıtlarını yasal zorunluluk nedeniyle kendi datacenter'ında tutmaya devam edecek; randevu web sitesini ise talebe göre ölçeklenebilmesi için Azure'da çalıştıracak. Bu hangi cloud modelidir?

- A) Public cloud
- B) Private cloud
- C) Hybrid cloud
- D) Multicloud

<details><summary>Cevap</summary>

**C.** Kurum içi altyapı ile public cloud'un birlikte kullanımı hybrid cloud'dur.
</details>

### 26. `[B · C1.4]` Tek seçim
Bir kuruluşun 40 subscription'ı var. Güvenlik ekibi bir Azure Policy atamasını ve bir RBAC rol atamasını **tüm subscription'lara tek seferde** uygulamak istiyor. Bu atamalar hangi kapsamda yapılmalıdır?

- A) Her resource group'ta ayrı ayrı
- B) Management group
- C) Her kaynakta ayrı ayrı
- D) Availability zone

<details><summary>Cevap</summary>

**B.** Management group'lar subscription'ları gruplar; burada yapılan Policy ve RBAC atamaları altındaki tüm subscription'lara miras kalır.
</details>

### 27. `[C · C1.12]` Tek seçim
Bir şirket, on-prem veritabanlarında, Azure'daki veri depolarında ve SaaS uygulamalarında bulunan **hassas veriyi (ör. kredi kartı numaraları) otomatik keşfetmek, sınıflandırmak** ve verinin haritasını çıkarmak istiyor. Hangi çözüm **en uygundur**?

- A) Microsoft Purview
- B) Azure Policy
- C) Microsoft Entra ID
- D) Azure Advisor

<details><summary>Cevap</summary>

**A.** Purview; veri yönetişimi, risk ve uyumluluk çözümleri ailesidir: veri haritası/kataloğu, otomatik keşif ve sınıflandırma, lineage.
</details>

### 28. `[B · C1.7]` Tek seçim
Aynı Azure aboneliğinde, farklı region'lardaki iki VNet'in kaynaklarının **Microsoft omurgası üzerinden, özel IP'lerle** birbiriyle iletişim kurması isteniyor. En basit çözüm nedir?

- A) Azure DNS
- B) Global VNet peering
- C) ExpressRoute
- D) Public endpoint

<details><summary>Cevap</summary>

**B.** VNet peering (farklı bölgelerde global peering) iki VNet'i internete çıkmadan bağlar. Peering transitive değildir ve adres alanları çakışmamalıdır.
</details>

### 29. `[A · C1.2]` Eşleştirme
Her senaryoyu doğru faydayla eşleştirin: **Governance · Security · Manageability · Predictability**

1. Önümüzdeki altı ayın bulut harcaması kullanım geçmişine göre tahmin edildi.
2. Kaynaklar standart şablonlarla dağıtılıyor ve kurumsal kurallara uymayanlar raporlanıyor.
3. Kaynaklar Azure CLI ve REST API ile yönetiliyor.
4. Altyapı DDoS saldırılarına karşı sağlayıcı tarafından korunuyor.

<details><summary>Cevap</summary>

**1 → Predictability** (maliyet) · **2 → Governance** · **3 → Manageability** (*in* the cloud) · **4 → Security**.
</details>

### 30. `[C · C1.11]` Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Aynı Azure servisi farklı region'larda farklı fiyatlara sahip olabilir. | ☐ | ☐ |
| b) Azure'dan dışarı (egress) giden veri trafiği ücretlendirilebilir. | ☐ | ☐ |
| c) Cost Management'ta tanımlanan bir budget aşıldığında kaynaklar otomatik olarak durdurulur. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Budget yalnızca uyarı üretir (e-posta, action group); harcamayı kendiliğinden durdurmaz.
</details>

### 31. `[B · C1.5]` Tek seçim
Bir geliştirici, hafta sonu kullanmayacağı bir test VM'inin **compute ücretini durdurmak**, ama disklerini ve yapılandırmasını korumak istiyor. Ne yapmalıdır?

- A) VM'i işletim sisteminden kapatmak (shutdown)
- B) VM'i Azure'dan durdurup **deallocate** etmek
- C) VM'in resource group'unu silmek
- D) VM'e bir ReadOnly lock eklemek

<details><summary>Cevap</summary>

**B.** Deallocate edilen VM compute ücreti almaz; disk ücreti sürer. Yalnızca OS içinden kapatmak, VM'in ayrılmış kalmasına ve ücretlenmeye devam etmesine yol açabilir; grubu silmek her şeyi siler.
</details>

### 32. `[C · C1.13]` Tek seçim
Bir yönetici, kendi bilgisayarına hiçbir araç kurmadan, tarayıcı üzerinden **kimliği hazır** bir komut satırında Azure CLI veya Azure PowerShell komutları çalıştırmak istiyor. Ne kullanmalıdır?

- A) Azure Cloud Shell
- B) Azure Arc
- C) Azure Advisor
- D) Azure Storage Explorer

<details><summary>Cevap</summary>

**A.** Cloud Shell portal'dan veya `shell.azure.com`'dan erişilen, Bash (Azure CLI) ve PowerShell seçenekli, kurulum gerektirmeyen etkileşimli kabuktur.
</details>

### 33. `[A · C1.3]` Senaryo
Bir yazılım ekibi kendi geliştirdiği web uygulamasını yayınlayacak; işletim sistemi yaması, sunucu kurulumu ve runtime yönetimiyle uğraşmak istemiyor. Hangi hizmet türü **en az yönetim yüküyle** bu ihtiyacı karşılar?

- A) IaaS
- B) PaaS
- C) SaaS
- D) On-premises

<details><summary>Cevap</summary>

**B.** Kendi kodunu çalıştıran ekip için en az yönetim yükü PaaS'tır (ör. App Service). SaaS hazır uygulamadır; kendi uygulamanı yayınlamaya uygun değildir.
</details>

### 34. `[B · C1.9]` Tek seçim
Bir kullanıcının bir resource group'taki **tüm kaynakları oluşturup yönetebilmesi**, ancak **başkalarına erişim verememesi** gerekiyor. Hangi built-in rol **en uygundur**?

- A) Owner
- B) Contributor
- C) Reader
- D) User Access Administrator

<details><summary>Cevap</summary>

**B.** Contributor erişim yönetimi hariç her şeyi yapabilir. Owner erişim de verebilir; Reader yalnızca görüntüler; User Access Administrator yalnızca erişimi yönetir.
</details>

### 35. `[B · C1.6]` Tek seçim
Bir şirket, çalışanlarının kişisel dizüstü bilgisayarlarından ve tabletlerinden, verinin cihazda değil bulutta kaldığı **Windows masaüstlerine** erişmesini istiyor. Hangi servis **en uygundur**?

- A) Azure App Service
- B) Azure Virtual Desktop
- C) Azure Functions
- D) Azure Container Apps

<details><summary>Cevap</summary>

**B.** Azure Virtual Desktop bulutta masaüstü ve uygulama sanallaştırmasıdır; Windows çok oturumlu desteği ve Entra ID ile kimlik/MFA sunar.
</details>

### 36. `[C · C1.14]` Tek seçim
Bir geliştirme ekibi, Azure'da ve kendi datacenter'ında çalışan web uygulamalarının **istek süreleri, hata oranları ve dış servis bağımlılıkları** hakkında telemetri toplamak istiyor. Ne kullanmalıdır?

- A) Azure Service Health
- B) Application Insights
- C) Azure Advisor
- D) Azure Status

<details><summary>Cevap</summary>

**B.** Application Insights, Azure Monitor'un uygulama performans izleme özelliğidir ve Azure dışında çalışan uygulamaları da izleyebilir.
</details>

### 37. `[A · C1.1]` Tek seçim
Bir araştırma ekibi, kesintiye uğrarsa kaldığı yerden devam edebilen ve zaman açısından esnek olan büyük bir hesaplama işini **en düşük maliyetle** çalıştırmak istiyor. Hangi seçenek **en uygundur**?

- A) Spot VM'ler
- B) 3 yıllık reserved instance
- C) Pay-as-you-go standart VM'ler
- D) Dedicated host

<details><summary>Cevap</summary>

**A.** Spot VM'ler Azure'un boşta kapasitesini büyük indirimle sunar; Azure kapasiteye ihtiyaç duyunca kesilebilir — kesintiye dayanıklı işler için idealdir.
</details>

### 38. `[B · C1.4]` Tek seçim
ABD federal kurumlarının özel uyumluluk gereksinimleri için ana Azure'dan **fiziksel ve mantıksal olarak ayrılmış** bulut örneği hangisidir?

- A) Bir availability zone
- B) Azure Government (sovereign region)
- C) Bir region pair
- D) Bir management group

<details><summary>Cevap</summary>

**B.** Azure Government, ABD kamu kurumları için ayrılmış bir **sovereign region**'dır. Diğer örnek: 21Vianet tarafından işletilen Azure China.
</details>

### 39. `[C · C1.12]` Tek seçim
Bir subscription'da **Owner** rolüne sahip bir kullanıcı, subscription'a atanmış bir Azure Policy'nin **Deny** etkisiyle yasakladığı bir VM boyutunu oluşturmaya çalışıyor. Ne olur?

- A) Owner rolü Policy'yi geçersiz kıldığı için VM oluşturulur.
- B) İstek reddedilir; Policy, kullanıcının rolünden bağımsız olarak uygulanır.
- C) VM oluşturulur ama Advisor uyarı verir.
- D) VM oluşturulur ve otomatik olarak silinir.

<details><summary>Cevap</summary>

**B.** RBAC kullanıcının **ne yapabileceğini**, Policy kaynağın **nasıl olabileceğini** belirler; Deny etkisi Owner'ın isteğini de reddeder.
</details>

### 40. `[B · C1.8]` Tek seçim
**Archive** access tier'daki bir blob'a hemen erişmeye çalışan bir kullanıcıyla ilgili hangisi doğrudur?

- A) Blob, Hot tier'daki gibi anında okunur.
- B) Blob önce daha sıcak bir tier'a **rehydrate** edilmelidir; bu işlem saatler sürebilir.
- C) Archive tier'daki blob'lar okunamaz, yalnızca silinebilir.
- D) Archive tier yalnızca Azure Files için geçerlidir.

<details><summary>Cevap</summary>

**B.** Archive çevrimdışı bir katmandır; okumak için Hot/Cool/Cold'a rehydrate gerekir ve saatler alabilir. Depolaması en ucuz, erişimi en pahalı/yavaş katmandır.
</details>

### 41. `[A · C1.2]` Tek seçim
Bir uygulama, bulunduğu region'ın tamamını etkileyen bir felaketten sonra başka bir region'da **yeniden ayağa kaldırıldı** ve hizmet vermeye devam etti. Bu en iyi hangi faydayla açıklanır?

- A) Scalability
- B) Reliability
- C) Elasticity
- D) Manageability

<details><summary>Cevap</summary>

**B.** Arızadan/felaketten **toparlanıp** çalışmaya devam etmek reliability'dir. "Kesinti olmadan sürdü" denseydi high availability olurdu.
</details>

### 42. `[C · C1.13]` Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure portal, Azure CLI ve Azure PowerShell'den gelen istekler Azure Resource Manager üzerinden işlenir. | ☐ | ☐ |
| b) ARM template'leri JSON biçiminde, deklaratif şablonlardır. | ☐ | ☐ |
| c) Bicep dosyaları dağıtımdan önce ARM JSON'a dönüştürülür (derlenir). | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Evet.** Tüm yönetim istekleri ARM'dan geçer (RBAC, Policy, lock, tag burada uygulanır); ARM template JSON ve deklaratiftir; Bicep, ARM template'in okunabilir DSL'idir ve ARM JSON'a derlenir.
</details>

### 43. `[B · C1.7]` Tek seçim
Bir şirket, VNet'indeki uygulamaların bir storage account'a **özel bir IP adresiyle** erişmesini ve storage account'un **internetten erişimini kapatmayı** istiyor. Ne kullanmalıdır?

- A) Public endpoint
- B) Private endpoint (Azure Private Link)
- C) Azure DNS public zone
- D) Point-to-site VPN

<details><summary>Cevap</summary>

**B.** Private endpoint, PaaS servisine VNet içinden özel IP ile erişim sağlar; trafik Microsoft ağında kalır ve public erişim kapatılabilir.
</details>

### 44. `[C · C1.11]` Tek seçim
Bir proje yöneticisi, aylık Azure harcaması belirlenen limitin **%80'ine ulaştığında e-posta** almak istiyor. Ne yapılandırmalıdır?

- A) Azure Advisor önerisi
- B) Microsoft Cost Management'ta bir budget ve uyarı eşiği
- C) Resource lock
- D) Azure Service Health alert

<details><summary>Cevap</summary>

**B.** Cost Management budget'ları tanımlanan eşiklerde (ör. %80, %100) bildirim gönderir.
</details>

### 45. `[B · C1.9]` Tek seçim
Bir şirket, kullanıcıların **yalnızca şirket ağı dışından** oturum açtıklarında MFA ile doğrulanmasını, şirket ağından girişte ise MFA istenmemesini istiyor. Hangi özellik kullanılmalıdır?

- A) Azure RBAC
- B) Microsoft Entra Conditional Access
- C) Resource lock
- D) Azure Policy

<details><summary>Cevap</summary>

**B.** Conditional Access, sinyallere (konum/IP, cihaz, uygulama, risk) göre karar verir: izin ver, MFA iste veya engelle.
</details>

### 46. `[C · C1.14]` Tek seçim
Bir yönetici, tek bir Azure SQL veritabanının dün gece 40 dakika erişilemez olmasının **Azure platformundaki bir sorundan** kaynaklanıp kaynaklanmadığını öğrenmek istiyor. Nereye bakmalıdır?

- A) Resource Health
- B) Azure Pricing Calculator
- C) Azure Advisor score
- D) Azure Policy compliance panosu

<details><summary>Cevap</summary>

**A.** Resource Health, tek bir kaynağın mevcut ve geçmiş sağlık durumunu ve sorunun platform kaynaklı olup olmadığını gösterir.
</details>

### 47. `[A · C1.1]` Tek seçim
Aşağıdakilerden hangisi **private cloud**'u en iyi tanımlar?

- A) Birden çok kuruluşun paylaştığı, sağlayıcıya ait altyapı
- B) Yalnızca tek bir kuruluşun kullanımına ayrılmış bulut altyapısı
- C) Birden çok public cloud sağlayıcısının birlikte kullanılması
- D) Yalnızca SaaS uygulamalarından oluşan bir ortam

<details><summary>Cevap</summary>

**B.** Private cloud tek bir kuruluşa ayrılmıştır (kendi datacenter'ı veya ayrılmış barındırma). A public cloud'u, C multicloud'u tanımlar.
</details>

### 48. `[B · C1.5]` Tek seçim
Bir web katmanının, CPU kullanımına göre **otomatik olarak** 2 ile 20 arasında **aynı yapılandırmada** VM çalıştırması ve yükün bu VM'lere dağıtılması gerekiyor. Ne kullanılmalıdır?

- A) Availability set
- B) Virtual Machine Scale Sets
- C) Proximity placement group
- D) Tek bir büyük VM

<details><summary>Cevap</summary>

**B.** VM Scale Sets aynı yapılandırmadaki VM'leri yük dengeleyiciyle birlikte çalıştırır ve autoscale kurallarıyla sayıyı otomatik değiştirir.
</details>

### 49. `[C · C1.12]` Tek seçim
Bir güvenlik ekibi, birlikte uygulanması gereken **30 farklı policy tanımını** tek bir paket halinde bir management group'a atamak istiyor. Bu paketin adı nedir?

- A) Resource lock
- B) Policy initiative
- C) Role assignment
- D) Blueprint lock

<details><summary>Cevap</summary>

**B.** Initiative (policy set), birlikte yönetilen policy tanımlarının grubudur (ör. bir güvenlik benchmark'ı) ve tek atamayla uygulanır.
</details>

### 50. `[A · C1.2]` Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Bir VM'e daha fazla CPU ve bellek eklemek scale up (vertical scaling) örneğidir. | ☐ | ☐ |
| b) Preview (önizleme) aşamasındaki Azure servisleri genellikle bir SLA ile sunulmaz. | ☐ | ☐ |
| c) Seri bağlı iki servisin composite SLA'sı, iki servisin SLA'larından yüksek olanına eşittir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Seri bağlı bileşenlerde SLA'lar çarpılır; sonuç her iki SLA'dan da **düşüktür** (ör. %99,95 × %99,99 ≈ %99,94).
</details>

---

## Puan tablosu

Her doğru cevabı ilgili satıra işaretle (çoklu seçim, evet/hayır, eşleştirme ve sıralamada **tümü** doğruysa 1 puan say).

| Alan | Ağırlık | Soru numaraları | Doğru | Yüzde |
|---|---|---|---|---|
| **A** — Describe cloud concepts | %25-30 | 1, 4, 8, 12, 16, 21, 24, 25, 29, 33, 37, 41, 47, 50 | ___ / 14 | |
| **B** — Describe Azure architecture and services | %35-40 | 2, 5, 7, 10, 13, 15, 17, 19, 22, 26, 28, 31, 34, 35, 38, 40, 43, 45, 48 | ___ / 19 | |
| **C** — Describe Azure management and governance | %30-35 | 3, 6, 9, 11, 14, 18, 20, 23, 27, 30, 32, 36, 39, 42, 44, 46, 49 | ___ / 17 | |
| **Toplam** | | | ___ / 50 | |

**Yorum:** ≥ 42 (%84) → resmî Practice Assessment'a geç, sonra sınav tarihi al · 35-41 → zayıf alanı tekrar et, bir hafta sonra yeniden çöz · < 35 → zayıf konuların README + laboratuvar + quiz döngüsüne geri dön.

## Yanlışlarını C1.x ile eşleştir

| Konu | Bu sınavdaki sorular | Yanlış sayın | Tekrar edilecek |
|---|---|---|---|
| C1.1 Cloud computing, modeller, fiyatlandırma | 1, 12, 25, 37, 47 | | [README](../01-cloud-computing/README.md) · [quiz](../01-cloud-computing/quiz.md) |
| C1.2 Cloud'un faydaları, SLA | 8, 16, 29, 41, 50 | | [README](../02-cloud-benefits/README.md) · [quiz](../02-cloud-benefits/quiz.md) |
| C1.3 IaaS, PaaS, SaaS, serverless | 4, 21, 24, 33 | | [README](../03-cloud-service-types/README.md) · [quiz](../03-cloud-service-types/quiz.md) |
| C1.4 Temel mimari bileşenler | 2, 15, 26, 38 | | [README](../04-core-architecture/README.md) · [quiz](../04-core-architecture/quiz.md) |
| C1.5 Compute ve VM'ler | 10, 31, 48 | | [README](../05-compute-virtual-machines/README.md) · [quiz](../05-compute-virtual-machines/quiz.md) |
| C1.6 Uygulama barındırma | 17, 35 | | [README](../06-app-hosting/README.md) · [quiz](../06-app-hosting/quiz.md) |
| C1.7 Ağ servisleri | 13, 28, 43 | | [README](../07-networking/README.md) · [quiz](../07-networking/quiz.md) |
| C1.8 Storage | 5, 22, 40 | | [README](../08-storage/README.md) · [quiz](../08-storage/quiz.md) |
| C1.9 Kimlik ve erişim | 7, 34, 45 | | [README](../09-identity-access/README.md) · [quiz](../09-identity-access/quiz.md) |
| C1.10 Güvenlik | 19 | | [README](../10-security/README.md) · [quiz](../10-security/quiz.md) |
| C1.11 Maliyet yönetimi | 9, 18, 30, 44 | | [README](../11-cost-management/README.md) · [quiz](../11-cost-management/quiz.md) |
| C1.12 Yönetişim ve uyumluluk | 3, 14, 27, 39, 49 | | [README](../12-governance-compliance/README.md) · [quiz](../12-governance-compliance/quiz.md) |
| C1.13 Yönetim ve dağıtım araçları | 11, 20, 32, 42 | | [README](../13-management-deployment-tools/README.md) · [quiz](../13-management-deployment-tools/quiz.md) |
| C1.14 İzleme araçları | 6, 23, 36, 46 | | [README](../14-monitoring-tools/README.md) · [quiz](../14-monitoring-tools/quiz.md) |

**Tekrar planı:** Her yanlış için *bilgi eksiği* mi *dikkat hatası* mı olduğunu not et. Bir konuda **2 veya daha fazla** bilgi eksiği varsa o konunun README'sini ve laboratuvarını baştan yap, ardından konu quiz'ini ≥ %80 olana kadar çöz. Dikkat hataları için [README 3.2](README.md#32-soru-çözme-teknikleri)'deki teknikleri tekrar oku.
