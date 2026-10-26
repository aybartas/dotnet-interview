# 🎓 AZ-900: Microsoft Azure Fundamentals — Kapsamlı Çalışma Dokümanı

> **Son Güncelleme:** Ekim 2026 · **Resmî Sınav Hedefleri:** [Skills measured as of July 20, 2026](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
> **Sınav Formatı:** 40-60 soru · 45-60 dakika · Geçme puanı: **700 / 1000** · Soru tipleri: Tek seçim, çoklu seçim, evet/hayır, eşleştirme, senaryo
>
> Bu doküman, 22 konunun tüm teorik anlatımlarını, mimari tablolarını, karşılaştırma matrislerini, sınav ipuçlarını ve 400'e yakın sınav formatında özgün quiz ve deneme sınavı sorusunu tek bir kaynakta birleştirmektedir.

---

## 📋 Sınav Alanları ve Ağırlıkları

| Sınav Alanı (Domain) | Ağırlık | Bölümler |
|---|---|---|
| **Describe cloud concepts** | %25-30 | Bölüm 1 (1.1, 1.2, 1.3) |
| **Describe Azure architecture and services** | %35-40 | Bölüm 2 (2.1 - 2.7) + Bölüm 4 |
| **Describe Azure management and governance** | %30-35 | Bölüm 3 (3.1 - 3.4) + Destek/SLA |
| **Deneme Sınavı & Strateji** | 50 Soru | Bölüm 5 (5.1, 5.2, 5.3) |

---

## 📑 Kapsamlı İçindekiler

- **BÖLÜM 1: Describe Cloud Concepts (%25-30)**
  - [1.1 Cloud Computing, Cloud Modelleri ve Fiyatlandırma](#11-cloud-computing-cloud-modelleri-ve-fiyatlandırma)
    - [1.1 Quiz Soruları](#-bölüm-11-quiz-soruları)
  - [1.2 Bulutun Temel Faydaları](#12-bulutun-temel-faydaları)
    - [1.2 Quiz Soruları](#-bölüm-12-quiz-soruları)
  - [1.3 Bulut Hizmet Türleri: IaaS, PaaS, SaaS, Serverless](#13-bulut-hizmet-türleri-iaas-paas-saas-serverless)
    - [1.3 Quiz Soruları](#-bölüm-13-quiz-soruları)
- **BÖLÜM 2: Describe Azure Architecture and Services (%35-40)**
  - [2.1 Azure Temel Mimari Bileşenleri](#21-azure-temel-mimari-bileşenleri)
    - [2.1 Quiz Soruları](#-bölüm-21-quiz-soruları)
  - [2.2 Azure Compute ve Virtual Machines](#22-azure-compute-ve-virtual-machines)
    - [2.2 Quiz Soruları](#-bölüm-22-quiz-soruları)
  - [2.3 Uygulama Barındırma: App Service, Container'lar, Functions, AVD](#23-uygulama-barındırma-app-service-containerlar-functions-avd)
    - [2.3 Quiz Soruları](#-bölüm-23-quiz-soruları)
  - [2.4 Ağ Servisleri: VNet, Peering, VPN, ExpressRoute, DNS](#24-ağ-servisleri-vnet-peering-vpn-expressroute-dns)
    - [2.4 Quiz Soruları](#-bölüm-24-quiz-soruları)
  - [2.5 Azure Storage: Blob, Files, Redundancy, Tiering](#25-azure-storage-blob-files-redundancy-tiering)
    - [2.5 Quiz Soruları](#-bölüm-25-quiz-soruları)
  - [2.6 Kimlik ve Erişim: Entra ID, MFA, Conditional Access, RBAC](#26-kimlik-ve-erişim-entra-id-mfa-conditional-access-rbac)
    - [2.6 Quiz Soruları](#-bölüm-26-quiz-soruları)
  - [2.7 Güvenlik: Zero Trust, Defense in Depth, Defender for Cloud](#27-güvenlik-zero-trust-defense-in-depth-defender-for-cloud)
    - [2.7 Quiz Soruları](#-bölüm-27-quiz-soruları)
- **BÖLÜM 3: Describe Azure Management and Governance (%30-35)**
  - [3.1 Maliyet Yönetimi: Pricing Calculator, TCO, Budgets](#31-maliyet-yönetimi-pricing-calculator-tco-budgets)
    - [3.1 Quiz Soruları](#-bölüm-31-quiz-soruları)
  - [3.2 Yönetişim ve Uyumluluk: Policy, Locks, Tags, Purview](#32-yönetişim-ve-uyumluluk-policy-locks-tags-purview)
    - [3.2 Quiz Soruları](#-bölüm-32-quiz-soruları)
  - [3.3 Kaynak Yönetme ve Dağıtma Araçları: Portal, CLI, PowerShell, Arc, Bicep](#33-kaynak-yönetme-ve-dağıtma-araçları-portal-cli-powershell-arc-bicep)
    - [3.3 Quiz Soruları](#-bölüm-33-quiz-soruları)
  - [3.4 İzleme Araçları: Monitor, Log Analytics, Service Health, Advisor](#34-izleme-araçları-monitor-log-analytics-service-health-advisor)
    - [3.4 Quiz Soruları](#-bölüm-34-quiz-soruları)
- **BÖLÜM 4: Genişletilmiş ve Bağlamsal Konular (Sınavda Seçeneklerde ve Senaryolarda Çıkan Servisler)**
  - [4.1 Veri ve Analitik Servisleri](#41-veri-ve-analitik-servisleri)
    - [4.1 Quiz Soruları](#-bölüm-41-quiz-soruları)
  - [4.2 Yük Dengeleme ve Trafik Dağıtımı](#42-yük-dengeleme-ve-trafik-dağıtımı)
    - [4.2 Quiz Soruları](#-bölüm-42-quiz-soruları)
  - [4.3 Ağ Güvenliği, Bastion ve Key Vault](#43-ağ-güvenliği-bastion-ve-key-vault)
    - [4.3 Quiz Soruları](#-bölüm-43-quiz-soruları)
  - [4.4 Yedekleme, Felaket Kurtarma ve Taşıma](#44-yedekleme-felaket-kurtarma-ve-taşıma)
    - [4.4 Quiz Soruları](#-bölüm-44-quiz-soruları)
  - [4.5 AI, Machine Learning ve IoT Servisleri](#45-ai-machine-learning-ve-iot-servisleri)
    - [4.5 Quiz Soruları](#-bölüm-45-quiz-soruları)
  - [4.6 Entegrasyon, Mesajlaşma ve DevOps](#46-entegrasyon-mesajlaşma-ve-devops)
    - [4.6 Quiz Soruları](#-bölüm-46-quiz-soruları)
  - [4.7 Destek Planları, SLA ve Servis Yaşam Döngüsü](#47-destek-planları-sla-ve-servis-yaşam-döngüsü)
    - [4.7 Quiz Soruları](#-bölüm-47-quiz-soruları)
- **BÖLÜM 5: AZ-900 Deneme Sınavı ve Sınav Stratejisi**
  - [5.0 Kapsamlı AZ-900 Deneme Sınavı (50 Soru) ve Sınav Stratejisi](#50-kapsamlı-az-900-deneme-sınavı-50-soru-ve-sınav-stratejisi)
    - [5.1 Sınav Stratejisi ve Hazırlık Rehberi](#51-sınav-stratejisi-ve-hazırlık-rehberi)
    - [5.2 50 Soruluk AZ-900 Deneme Sınavı](#52-50-soruluk-az-900-deneme-sınavı)
    - [5.3 Puanlama ve Zayıf Alan Analizi](#puan-tablosu)

---

# BÖLÜM 1: Describe Cloud Concepts (%25-30)

---

## 1.1 Cloud Computing, Cloud Modelleri ve Fiyatlandırma

> **Sınav Alanı:** Describe cloud concepts (%25-30) · **Konu Kodu:** C1.1

### 1. Ne?

**Cloud computing**, bilgi işlem hizmetlerinin — sunucu, depolama, ağ, veritabanı, yazılım, analitik, yapay zekâ — **internet üzerinden, ihtiyaç anında ve kullanım kadar ücretle** sunulmasıdır. Donanımı sen almazsın, kurmazsın, bakımını yapmazsın; bir sağlayıcının (Azure) datacenter'larındaki kapasiteyi kiralarsın.

Bu konu AZ-900'ün ilk alanının temelidir ve dört soruyu cevaplar:

| Soru | Kavram |
|------|--------|
| Bulut tam olarak nedir, datacenter'dan farkı ne? | Tanım + NIST'in 5 niteliği |
| Bulutta güvenlikten/işletimden kim sorumlu? | **Shared responsibility model** ⭐ |
| Bulut nerede çalışır, kime aittir? | **Public / private / hybrid** (+ multicloud) |
| Nasıl ödenir? | **Consumption-based model**, **CapEx vs OpEx** ⭐, fiyatlandırma modelleri |


---

### 2. Neden?

- **Sınav ağırlığı:** "Describe cloud concepts" alanı sınavın **%25-30**'u; bu alanın en çok soru üreten iki tablosu (shared responsibility ve CapEx/OpEx) bu konudadır. Sonraki her konu (IaaS/PaaS/SaaS, SLA, maliyet, güvenlik) bu kavramların üzerine kurulur.
- **Gerçek işte:** "Bu sunucuyu buluta taşıyalım mı?" kararı; sorumluluğun kime geçtiğini (yama, yedek, güvenlik), hangi modelin regülasyona uyduğunu ve maliyetin nasıl değiştiğini bilmeyi gerektirir.
- **Yaygın yanlış:** "Buluta taşıdık, güvenlik artık Microsoft'un işi" düşüncesi gerçek veri sızıntılarının bir numaralı sebeplerindendir — shared responsibility model tam olarak bunu düzeltir.

---

### 3. Nasıl?

#### 3.1 Cloud computing nedir? NIST'in beş niteliği (`Bölüm 01`)

ABD standartlar enstitüsü NIST'in tanımı, bir hizmetin "bulut" sayılması için beş nitelik arar:

| Nitelik | Anlamı | Azure'da örnek |
|---------|--------|----------------|
| **On-demand self-service** | Kimseye sormadan, kendin kaynak açarsın | Portal/CLI ile dakikalar içinde VM |
| **Broad network access** | Ağ üzerinden, her cihazdan erişim | Portal, REST API, CLI, mobil uygulama |
| **Resource pooling** | Fiziksel kaynaklar çok kiracılı (multi-tenant) havuzda paylaşılır | Aynı fiziksel host'ta farklı müşterilerin VM'leri, izole |
| **Rapid elasticity** | Kapasite hızla (otomatik) büyür/küçülür | Autoscale, VM Scale Sets (→ C1.2) |
| **Measured service** | Kullanım ölçülür, ona göre faturalanır | Saniye/saat/işlem/GB bazlı fatura |

**Geleneksel datacenter ile farkı:**

| | Kendi datacenter'ın | Bulut |
|---|---|---|
| Kaynağı hazır etme | Haftalar–aylar (satın alma, teslimat, kurulum) | Dakikalar |
| Ön yatırım | Donanım, lisans, bina, enerji (CapEx) | Yok — kullandıkça öde (OpEx) |
| Kapasite planlama | Zirveye göre tahmin; fazlası boşta, azı kesinti | İhtiyaç kadar; artır/azalt |
| Bakım ve fiziksel güvenlik | Senin ekibin | Sağlayıcı |
| Küresel erişim | Yeni ülke = yeni datacenter | Başka bir region seç (→ C1.4) |

#### 3.2 Shared responsibility model ⭐ (`Bölüm 02`, [`ResponsibilityMatrix.cs`](ResponsibilityMatrix.cs))

Bulutta güvenlik ve işletim sorumluluğu **sağlayıcı ile müşteri arasında paylaşılır**; hangi katmanın kimde olduğunu **dağıtım modeli** belirler. Microsoft'un yayımladığı tablo:

| Sorumluluk | On-prem | IaaS | PaaS | SaaS |
|---|---|---|---|---|
| Bilgi ve veri | Müşteri | **Müşteri** | **Müşteri** | **Müşteri** |
| Cihazlar (mobil ve PC) | Müşteri | **Müşteri** | **Müşteri** | **Müşteri** |
| Hesaplar ve kimlikler | Müşteri | **Müşteri** | **Müşteri** | **Müşteri** |
| Kimlik ve directory altyapısı | Müşteri | Müşteri | Paylaşımlı | Paylaşımlı |
| Uygulamalar | Müşteri | Müşteri | Paylaşımlı | Microsoft |
| Ağ kontrolleri | Müşteri | Müşteri | Paylaşımlı | Microsoft |
| İşletim sistemi | Müşteri | Müşteri | Microsoft | Microsoft |
| Fiziksel host'lar | Müşteri | **Microsoft** | **Microsoft** | **Microsoft** |
| Fiziksel ağ | Müşteri | **Microsoft** | **Microsoft** | **Microsoft** |
| Fiziksel datacenter | Müşteri | **Microsoft** | **Microsoft** | **Microsoft** |

Tabloyu ezberlemek yerine **üç kural** yeterli:

1. **Veri, cihazlar, hesaplar ve kimlikler her zaman müşteride** — SaaS'ta bile. Microsoft 365'te kimin hangi dosyaya erişebildiği, MFA'nın açık olup olmadığı, hangi verinin sınıflandırıldığı senin sorumluluğundur.
2. **Fiziksel her şey bulutta her zaman Microsoft'ta** — datacenter'ın kapısındaki güvenlikten diskin imhasına kadar.
3. **Arada kalanlar modele göre kayar:** IaaS → PaaS → SaaS yönünde ilerledikçe sorumluluk Microsoft'a geçer. İşletim sistemi IaaS'ta müşteride (yama, antivirüs, güvenlik duvarı), PaaS'ta Microsoft'tadır.

"**Paylaşımlı**" ne demek? Platformu (ör. App Service'in ağ özellikleri, Entra ID'nin kimlik altyapısı) Microsoft sağlar ve işletir; **doğru yapılandırmak** (hangi IP'lerin erişeceği, hangi kimlik politikalarının açık olduğu) müşterinindir.

Soru-cevap aracı:

```bash

> **Pratik — sorumluluk matrisi alıştırması.** Bir şirketin "web uygulaması + veritabanı" iş yükü dört farklı biçimde çalıştırılıyor. Her hücreye **M** (müşteri), **MS** (Microsoft) veya **P** (paylaşımlı) yaz:
>
> | Görev | On-prem (kendi sunucuları) | IaaS (VM + VM'de SQL Server) | PaaS (App Service + Azure SQL Database) | SaaS (hazır bir web ürünü) |
> |---|---|---|---|---|
> | Müşteri verisinin sınıflandırılması | | | | |
> | Çalışanların MFA kullanması | | | | |
> | İşletim sistemi güvenlik yamaları | | | | |
> | Veritabanı motoru yamaları | | | | |
> | Uygulamanın ağ erişim kuralları (hangi IP girebilir) | | | | |
> | Sunucu donanımı arızası | | | | |
> | Datacenter'a fiziksel giriş kontrolü | | | | |
>
> <details><summary>Çözüm</summary>
>
> | Görev | On-prem | IaaS | PaaS | SaaS |
> |---|---|---|---|---|
> | Veri sınıflandırma | M | M | M | M |
> | MFA | M | M | M | M |
> | OS yamaları | M | M | MS | MS |
> | Veritabanı motoru yamaları | M | M (SQL VM'de; otomatik yama özelliği yardımcı olur ama sorumluluk müşteride) | MS | MS |
> | Uygulamanın ağ erişim kuralları | M | M | P (özellik Microsoft'ta, kuralları sen yazarsın) | MS (SaaS ürününün sunduğu ayarlar kadarı müşteride) |
> | Donanım arızası | M | MS | MS | MS |
> | Fiziksel giriş | M | MS | MS | MS |
>
> İlk iki satır **hiçbir modelde** devredilemez; son iki satır **bulutun her modelinde** Microsoft'tadır.
> </details>

#### 3.3 Public, private, hybrid cloud (`Bölüm 03`)

| Model | Kime ait / nerede | Artıları | Eksileri | Tipik senaryo |
|---|---|---|---|---|
| **Public cloud** | Sağlayıcının altyapısı; birçok kuruluş paylaşır (mantıksal izolasyonla) | CapEx yok, dakikalar içinde başla, neredeyse sınırsız ölçek, kullandıkça öde | Fiziksel altyapı üzerinde kontrol yok; bazı regülasyonlar için ek değerlendirme | Yeni web uygulaması, değişken yük, startup |
| **Private cloud** | Tek kuruluşa ait (kendi datacenter'ı veya ayrılmış barındırma); bulut niteliklerini (self-service, otomasyon) kurum içinde sunar | Tam kontrol, sıkı uyumluluk, özel donanım | Donanım, bakım ve personel maliyeti/sorumluluğu kurumda; ölçek donanımla sınırlı | Sıkı regülasyonlu kurum, gizli veriler |
| **Hybrid cloud** | Public + private birlikte, aralarında bağlantıyla | Hassas veri/eski sistem yerinde, esnek yük bulutta; **cloud bursting** (yerel kapasite dolunca taşan yük buluta); kademeli taşıma | İki ortamı birlikte yönetme karmaşıklığı | "Veri ülkede/kurumda kalmalı ama web katmanı ölçeklenmeli" |
| *(bağlam)* **Multicloud** | Birden çok public cloud sağlayıcısı (ör. Azure + AWS) | Sağlayıcıya bağımlılığı azaltma, her sağlayıcının güçlü servisi | Farklı araçlar/beceriler, yönetim karmaşıklığı | Şirket birleşmesi, belirli bir servise ihtiyaç |

- Hybrid ve multicloud ortamları tek yerden yönetmek için **Azure Arc** (Azure dışındaki sunucu, Kubernetes ve veritabanlarını Azure'dan yönetme) → C1.13.
- Datacenter'ını buluta bağlamanın ağ tarafı (VPN Gateway, ExpressRoute) → C1.7.
- Hybrid ≠ multicloud: hybrid **kendi altyapın + public cloud**; multicloud **birden çok public cloud**. Bir kuruluş ikisi birden olabilir.

#### 3.4 Consumption-based model: CapEx vs OpEx ⭐ (`Bölüm 04`)

**Consumption-based (tüketim bazlı) model:** yalnızca kullandığın kaynak için, kullandığın süre/miktar kadar ödersin.

- Ön yatırım (donanım) **yok**.
- Kapasite **fazlası** (boşta bekleyen sunucu) veya **eksiği** (zirvede yetmeyen kapasite) riski yok — kapasite talebi izler.
- Kullanmayı bıraktığında (kaynağı sildiğinde) ödeme durur.

| | **CapEx** (capital expenditure) | **OpEx** (operational expenditure) |
|---|---|---|
| Ne? | Fiziksel altyapıya **baştan** yapılan büyük, sabit yatırım | Hizmet/ürün için **dönem dönem** yapılan, kullanıma göre değişen harcama |
| Örnek | Sunucu, depolama ünitesi, bina, jeneratör satın almak | Bulut aboneliği, aylık hizmet bedeli, lisans kiralama |
| Muhasebe | Varlık; değeri yıllara yayılarak **amortisman** düşülür | O dönemin **gideri** olarak düşülür |
| Risk | Tahmin hatası: fazla alırsan para boşta, az alırsan kapasite yetmez | Kullanım kontrol edilmezse fatura sürprizi (→ bütçe alarmı) |

**Bulut, harcamayı CapEx'ten OpEx'e kaydırır.** `Section04`, zirvesi dalgalı bir e-ticaret yükünde (aylık 4 → 20 sunucu) kendi donanımın zirveye göre boyutlanınca kapasitenin büyük kısmının yıl boyunca boşta kaldığını, tüketim bazlı modelde bu boşluk için ödeme yapılmadığını hesaplar (fiyatlar örnektir).

> Dikkat: Yük **sabit ve 7/24** ise tüketim bazlı modelin avantajı azalır; o durumda reservation gibi taahhütlü fiyatlar devreye girer (3.5).

#### 3.5 Cloud fiyatlandırma modelleri (`Bölüm 05`)

| Model | Nasıl çalışır | Ne zaman? |
|---|---|---|
| **Pay-as-you-go** | Taahhüt yok; saniye/dakika/saat veya işlem/GB başına ödeme | Değişken veya öngörülemeyen yük, deneme, kısa süreli proje |
| **Reserved instances** / **savings plan** | 1 veya 3 yıllık **taahhüt** karşılığı ciddi indirim (Microsoft'un ifadesiyle reservation'da "en fazla %72"); kaynak kullanılmasa da taahhüt ödenir | Yıllarca 7/24 çalışacak **sabit** iş yükü |
| **Spot** | Azure'un boşta kalan kapasitesi, büyük indirimle ("en fazla %90"); Azure kapasiteye ihtiyaç duyunca VM **kesilir** (eviction) | Kesintiye dayanıklı toplu iş, render, CI, test |
| **Free tier / ücretsiz hesap** | Yeni hesaba sınırlı süreli kredi + bazı servislerde ücretsiz kullanım miktarları; bazı servislerin kalıcı ücretsiz katmanı (ör. App Service F1) | Öğrenme, deneme |
| **Dev/test fiyatlandırması** | Visual Studio aboneleri için geliştirme/test aboneliklerinde indirimli fiyatlar | Üretim dışı ortamlar |

> İndirim oranları pazarlama rakamlarıdır ve zamanla değişir; sınav oranı değil **hangi modelin hangi iş yüküne uyduğunu** sorar. Azure Hybrid Benefit (mevcut Windows Server/SQL Server lisansını bulutta kullanmak), maliyeti etkileyen faktörler, Pricing Calculator ve Cost Management → C1.11.

#### 3.6 Pratik — ilk adımlar: hesap, ilk resource group ve bütçe

Azure CLI ile (yerelde `az` kurulu veya portaldaki **Cloud Shell**'de; Cloud Shell'de `az login` gerekmez). Bu laboratuvar **ücretsizdir**: boş bir resource group fatura üretmez.

```bash
# 1) Sign in and verify which subscription you are on
az login
az account show --output table
az account list --output table                   # if you have several subscriptions
# az account set --subscription "<subscription name or id>"

# 2) List the regions (→ C1.4) and create your first resource group
az account list-locations --output table
az group create --name rg-az900-c11 --location westeurope
az group list --output table

# 3) Cleanup — the last step of every lab
az group delete --name rg-az900-c11 --yes --no-wait
az group list --output table                     # verify it is gone
```

**Bütçe alarmı — öğrenmeye başlarken ilk iş.** Ücretli kaynak açacağın laboratuvarlardan önce aylık bir bütçe tanımla; harcama eşiği aşınca (ör. %50, %80, %100) e-posta gelsin. Bütçeler **Cost Management → Budgets** altında tanımlanır ve abonelik veya resource group kapsamında olabilir. CLI ile de bütçe oluşturulabilir:

```bash
# Monthly budget of 10 units (start-date must be the first day of a month)
az consumption budget create \
  --budget-name monthly-learning-budget \
  --amount 10 \
  --category cost \
  --time-grain monthly \
  --start-date 2026-11-01 \
  --end-date 2027-10-31
az consumption budget list --output table
```

Bütçe **harcamayı durdurmaz**, yalnızca uyarır; e-posta bildirim eşikleri bütçe ekranından (veya REST API ile) eklenir. Ayrıntılar → C1.11.

---

### 4. Gerçek dünya senaryosu

Orta ölçekli bir sigorta şirketi, kendi datacenter'ındaki sunucuların donanım ömrü dolunca karar veriyor:

1. **Müşteri portalı** (kasım-aralıkta yük 5 katına çıkıyor) → **public cloud**, tüketim bazlı. Zirve için yıl boyu boşta duracak donanım almak yerine kapasite talebi izliyor (CapEx → OpEx).
2. **Poliçe veritabanı** (regülasyon gereği belirli verilerin kurum kontrolünde kalması isteniyor) → bir süre **kurum içinde** kalıyor; portal bu veriye güvenli bir bağlantıyla (VPN/ExpressRoute) erişiyor → toplam mimari **hybrid cloud**.
3. **E-posta ve ofis uygulamaları** → **SaaS** (Microsoft 365); sunucu yönetimi tamamen kalkıyor, ama kullanıcı hesapları, MFA ve dosya paylaşım izinleri **hâlâ şirketin** sorumluluğunda.
4. **Gece çalışan risk hesaplamaları** → **spot VM**'ler: kesilirse iş kaldığı yerden yeniden başlıyor, maliyet çok düşük. Portalın 7/24 çalışan çekirdek kapasitesi → **reservation**.
5. Taşıma başlamadan önce abonelikte **bütçe alarmı** kuruluyor; ilk ay test ortamında unutulan büyük bir VM bu sayede fark ediliyor.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Bu konu çoğunlukla kısa senaryolar, **evet/hayır tabloları** ("Bu ifade doğru mu?") ve "boşluğu doldur" tarzı cümle tamamlama ile sorulur. Hesap istenmez; kavramın doğru eşleştirilmesi istenir.

| Soru kalıbı | Doğru cevap | Tuzak |
|---|---|---|
| "MFA'yı / şifre politikasını / kullanıcı erişimini kim yönetir?" | **Her modelde müşteri** | SaaS'ta "Microsoft" şıkkı |
| "Verinin doğruluğu ve sınıflandırılması kimin?" | **Her modelde müşteri** | |
| "İşletim sistemi yamaları kimin?" | IaaS'ta **müşteri**, PaaS/SaaS'ta **Microsoft** | |
| "Fiziksel güvenlik / donanım kimin?" | Bulutta **her zaman Microsoft** | IaaS'ta "müşteri" şıkkı |
| "Hangi model en çok sorumluluğu müşteriye bırakır?" | On-prem; bulut modelleri içinde **IaaS** | |
| "Veri kurum içinde kalmalı ama ölçek gerekiyor" | **Hybrid** | Private |
| "Birden çok bulut sağlayıcısı" | **Multicloud** | Hybrid |
| "Donanım satın almadan, ön yatırım olmadan" | **OpEx / consumption-based** | CapEx |
| "Fiziksel sunucu satın almak" | **CapEx** | |
| "Kullanılmayan kapasite için ödeme yapılmaz" | **Consumption-based model** | |
| "7/24, yıllarca çalışacak sabit iş yükü için en ucuz" | **Reserved instance / savings plan** | Spot (kesilebilir) |
| "Kesintiye dayanıklı toplu iş için en ucuz" | **Spot** | Reserved |

**Anahtar kelimeler:** *shared responsibility*, *always retained by the customer* (information and data, devices, accounts and identities), *CapEx/OpEx*, *upfront cost*, *pay-as-you-go*, *consumption-based*, *cloud bursting*, *private cloud = single organization*.

**Evet/hayır tablosunda dikkat:** Cümledeki **mutlak ifadeler** ("her zaman", "hiçbir zaman", "yalnızca") genellikle cevabı belirler: "Public cloud'da fiziksel sunucuların bakımı müşterinin sorumluluğundadır" → Hayır; "SaaS'ta veri her zaman müşterinin sorumluluğundadır" → Evet.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "SaaS'ta her şey Microsoft'un sorumluluğunda" | Veri, cihazlar, hesaplar ve kimlikler SaaS'ta da müşteride |
| "IaaS'ta fiziksel sunucu da benim" | IaaS'ta fiziksel host/ağ/datacenter Microsoft'ta; OS ve üstü sende |
| "Private cloud = Azure'da ayrılmış bir abonelik" | Private cloud tek kuruluşa ayrılmış **altyapıdır**; Azure aboneliği public cloud kaynağıdır |
| Hybrid ile multicloud'u karıştırmak | Hybrid: kendi altyapın + public cloud · Multicloud: birden çok public cloud |
| "Bulut her zaman daha ucuz" | Sabit 7/24 yükte pay-as-you-go pahalı olabilir; reservation, doğru boyutlandırma ve kapatma alışkanlığı gerekir |
| "OpEx = maliyet düşer" | OpEx maliyetin **türüdür** (dönemsel, değişken), miktarı değil |
| Bütçe alarmı kurmadan laboratuvar yapmak | İlk iş bütçe alarmı; her laboratuvar `az group delete` ile biter |
| "Bütçe aşılınca kaynaklar durur" | Budget yalnızca **uyarır**; harcamayı durdurmaz (otomatik eylem ayrıca kurulmalı) |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| On-prem → public cloud | CapEx yok, hızlı başlangıç, esnek ölçek, fiziksel işletme yükü yok | Fiziksel kontrol yok; maliyet disiplini (izleme, bütçe) gerekir; internet/ağ bağımlılığı |
| Public yerine private | Tam kontrol, özel uyumluluk | Donanım/personel maliyeti, sınırlı ölçek, yavaş kapasite artışı |
| Hybrid | Regülasyona uyum + bulut esnekliği, kademeli taşıma | İki ortamı birlikte işletme, bağlantı ve kimlik entegrasyonu karmaşıklığı |
| IaaS → PaaS → SaaS | Daha az sorumluluk ve yönetim yükü | Daha az kontrol ve özelleştirme (→ C1.3) |
| Pay-as-you-go → reservation | Sabit yükte büyük indirim | Taahhüt: kullanmasan da ödersin; esneklik azalır |
| Pay-as-you-go → spot | Çok düşük fiyat | Her an kesilebilir; yalnızca kesintiye dayanıklı işler |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900) — resmî hedefler ve değişiklik kaydı
- [Microsoft Learn — Describe cloud computing](https://learn.microsoft.com/en-us/training/modules/describe-cloud-compute/) (AZ-900 öğrenme yolu, 1. modül)
- [Shared responsibility in the cloud](https://learn.microsoft.com/en-us/azure/security/fundamentals/shared-responsibility)
- [NIST SP 800-145 — The NIST Definition of Cloud Computing](https://csrc.nist.gov/pubs/sp/800/145/final)
- [Azure reservations](https://learn.microsoft.com/en-us/azure/cost-management-billing/reservations/save-compute-costs-reservations) · [Azure Spot Virtual Machines](https://learn.microsoft.com/en-us/azure/virtual-machines/spot-vms)
- [Tutorial: Create and manage budgets](https://learn.microsoft.com/en-us/azure/cost-management-billing/costs/tutorial-acm-create-budgets) · [`az consumption budget`](https://learn.microsoft.com/en-us/cli/azure/consumption/budget)


### 📝 Bölüm 1.1 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Aşağıdakilerden hangisi cloud computing'in tanımını **en iyi** ifade eder?

- A) Şirketin kendi sunucularını sanallaştırarak tek bir fiziksel makinede toplaması
- B) Bilgi işlem hizmetlerinin internet üzerinden, ihtiyaç anında ve kullanım kadar ücretle sunulması
- C) Uygulamaların yalnızca web tarayıcısı üzerinden çalıştırılması
- D) Verilerin birden fazla datacenter'a yedeklenmesi

<details><summary>Cevap</summary>

**B.** Tanımın üç ayağı: internet üzerinden, ihtiyaç anında (on-demand), kullanım kadar ücret (measured service). A sanallaştırmadır (bulutun bir teknolojisi, kendisi değil); C yalnızca SaaS'ın bir özelliği; D yedeklemedir.
</details>

#### Soru 2: Çoklu seçim (iki doğru cevap)
NIST tanımına göre aşağıdakilerden **hangi ikisi** cloud computing'in temel niteliklerindendir?

- A) On-demand self-service
- B) Tek kiracılı (single-tenant) donanım
- C) Measured service
- D) Uzun dönemli donanım sözleşmesi

<details><summary>Cevap</summary>

**A ve C.** Beş nitelik: on-demand self-service, broad network access, resource pooling (çok kiracılı havuz — B'nin tersi), rapid elasticity, measured service. D, bulutun ortadan kaldırdığı şeydir.
</details>

#### Soru 3: Evet / Hayır
Her ifade için Evet veya Hayır seçin.

| İfade | Evet | Hayır |
|---|---|---|
| a) SaaS modelinde kullanıcı hesaplarının ve erişim izinlerinin yönetimi müşterinin sorumluluğundadır. | ☐ | ☐ |
| b) IaaS modelinde fiziksel sunucuların bakımı müşterinin sorumluluğundadır. | ☐ | ☐ |
| c) PaaS modelinde işletim sistemi yamaları Microsoft'un sorumluluğundadır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Hesaplar ve kimlikler her modelde müşteride; fiziksel katmanlar bulutun her modelinde Microsoft'ta; işletim sistemi IaaS'ta müşteride, PaaS'ta Microsoft'ta.
</details>

#### Soru 4: Tek seçim
Bir şirket Azure'da bir sanal makine (IaaS) çalıştırıyor. Aşağıdakilerden hangisi **Microsoft'un** sorumluluğundadır?

- A) VM'deki işletim sisteminin güvenlik yamaları
- B) VM'e erişen kullanıcıların MFA kullanması
- C) VM'in çalıştığı fiziksel host'un donanım arızası
- D) VM üzerindeki uygulamanın güvenlik duvarı kuralları

<details><summary>Cevap</summary>

**C.** IaaS'ta Microsoft fiziksel host, fiziksel ağ ve datacenter'dan sorumludur. OS (A), kimlik/MFA (B) ve uygulama/ağ kuralları (D) müşteridedir.
</details>

#### Soru 5: Tek seçim
Shared responsibility model'de aşağıdakilerden hangisi **tüm dağıtım modellerinde** (on-prem, IaaS, PaaS, SaaS) müşteride kalır?

- A) İşletim sistemi
- B) Ağ kontrolleri
- C) Bilgi ve veri
- D) Uygulamalar

<details><summary>Cevap</summary>

**C.** Her zaman müşteride kalan üçlü: bilgi ve veri; cihazlar (mobil ve PC); hesaplar ve kimlikler. OS PaaS'ta, ağ kontrolleri ve uygulamalar SaaS'ta Microsoft'a geçer.
</details>

#### Soru 6: Sıralama
Aşağıdaki dağıtım modellerini, **müşterinin sorumluluğu en fazladan en aza** doğru sıralayın: PaaS · On-prem · SaaS · IaaS

<details><summary>Cevap</summary>

**On-prem → IaaS → PaaS → SaaS.** Her adımda bir katman grubu Microsoft'a geçer: IaaS'ta fiziksel katman, PaaS'ta işletim sistemi (ve kısmen ağ/uygulama), SaaS'ta uygulama ve ağ kontrolleri.
</details>

#### Soru 7: Cümle tamamlama
"PaaS modelinde kimlik ve directory altyapısı \_\_\_\_\_\_\_\_\_\_."

- A) tamamen müşterinin sorumluluğundadır
- B) müşteri ile Microsoft arasında paylaşılır
- C) tamamen Microsoft'un sorumluluğundadır
- D) hiçbir tarafın sorumluluğunda değildir

<details><summary>Cevap</summary>

**B.** Kimlik altyapısı IaaS'ta müşteride, PaaS ve SaaS'ta **paylaşımlıdır**: altyapıyı (ör. Entra ID) Microsoft işletir, doğru yapılandırmayı (politikalar, roller) müşteri yapar.
</details>

#### Soru 8: Senaryo
Bir banka, regülasyon gereği müşteri verisini kendi datacenter'ında tutmak zorunda. Aynı zamanda mobil bankacılık uygulamasının ön yüzünün, kampanya dönemlerinde hızla ölçeklenmesini istiyor. Hangi cloud modeli **en uygundur**?

- A) Public cloud
- B) Private cloud
- C) Hybrid cloud
- D) Multicloud

<details><summary>Cevap</summary>

**C.** Hassas veri kurum içinde (private/on-prem), ölçeklenmesi gereken ön yüz public cloud'da → ikisinin birlikte kullanımı hybrid cloud'dur. B ölçek ihtiyacını karşılamaz; A veri yerleşimi gereksinimini karşılamaz; D birden çok public sağlayıcı demektir.
</details>

#### Soru 9: Tek seçim
Bir şirket iş yüklerini hem Azure'da hem de başka bir public cloud sağlayıcısında çalıştırıyor. Kendi datacenter'ı yok. Bu yaklaşımın adı nedir?

- A) Hybrid cloud
- B) Private cloud
- C) Multicloud
- D) Community cloud

<details><summary>Cevap</summary>

**C.** Birden çok public cloud sağlayıcısı = multicloud. Hybrid, kurum içi (private) altyapı ile public cloud'un birlikte kullanımıdır; burada kurum içi altyapı yok.
</details>

#### Soru 10: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Private cloud, tek bir kuruluşun kullanımına ayrılmış bulut altyapısıdır. | ☐ | ☐ |
| b) Public cloud kullanmak için önce sunucu donanımı satın almak gerekir. | ☐ | ☐ |
| c) Hybrid cloud, yerel kapasite yetmediğinde fazla yükün public cloud'a taşınmasına (cloud bursting) olanak tanır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Public cloud'da ön yatırım yoktur; donanım sağlayıcınındır. Cloud bursting, hybrid'in klasik kullanım senaryosudur.
</details>

#### Soru 11: Tek seçim
Aşağıdakilerden hangisi bir **CapEx** (capital expenditure) örneğidir?

- A) Aylık Azure faturası
- B) Kendi datacenter'ı için sunucu ve depolama ünitesi satın almak
- C) Kullanıcı başına aylık Microsoft 365 aboneliği
- D) Saatlik ücretlendirilen bir sanal makine

<details><summary>Cevap</summary>

**B.** CapEx, fiziksel altyapıya baştan yapılan, amortismanla yıllara yayılan yatırımdır. A, C, D dönemsel, kullanıma bağlı giderlerdir → OpEx.
</details>

#### Soru 12: Çoklu seçim (iki doğru cevap)
Consumption-based (tüketim bazlı) modelin **hangi ikisi** faydasıdır?

- A) Ön yatırım (upfront cost) gerekmez
- B) Fatura her ay sabittir
- C) Kullanılmayan kapasite için ödeme yapılmaz
- D) Donanım müşterinin mülkiyetine geçer

<details><summary>Cevap</summary>

**A ve C.** Tüketim bazlı modelde maliyet kullanımla orantılıdır, dolayısıyla fatura **değişkendir** (B yanlış); donanım sağlayıcıda kalır (D yanlış).
</details>

#### Soru 13: Cümle tamamlama
"Bir kuruluşun kendi datacenter'ından public cloud'a geçmesi, BT harcamalarını genellikle \_\_\_\_\_\_\_\_\_\_ kaydırır."

- A) OpEx'ten CapEx'e
- B) CapEx'ten OpEx'e
- C) CapEx'ten CapEx'e
- D) Hiçbir değişiklik olmaz

<details><summary>Cevap</summary>

**B.** Donanım yatırımı (CapEx) yerine dönemsel hizmet bedeli (OpEx) ödenir.
</details>

#### Soru 14: Senaryo
Bir şirketin muhasebe uygulaması, önümüzdeki üç yıl boyunca 7/24, yükü neredeyse hiç değişmeden çalışacak. Maliyeti **en aza** indirmek için hangi fiyatlandırma modeli **en uygundur**?

- A) Pay-as-you-go
- B) Spot VM
- C) 3 yıllık reserved instance (veya savings plan)
- D) Ücretsiz katman

<details><summary>Cevap</summary>

**C.** Sabit ve uzun süreli yükte taahhüt karşılığı indirim en ucuzudur. Spot (B) kesilebildiği için 7/24 kritik iş için uygun değildir; pay-as-you-go (A) esneklik için ödenen primdir; ücretsiz katman (D) üretim yükü için değildir.
</details>

#### Soru 15: Senaryo
Bir film stüdyosu, her gece binlerce kareyi işleyen bir render işi çalıştırıyor. İş kesilirse kaldığı yerden devam edebiliyor ve maliyet en önemli kriter. Hangi seçenek **en uygundur**?

- A) Spot VM
- B) 3 yıllık reserved instance
- C) Pay-as-you-go standart VM
- D) Kendi datacenter'ına sunucu almak

<details><summary>Cevap</summary>

**A.** Kesintiye dayanıklı, esnek zamanlı toplu iş = spot VM'in tipik kullanımı; Azure'un boşta kapasitesi çok düşük fiyata kullanılır, karşılığında VM geri alınabilir (eviction).
</details>

#### Soru 16: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Reserved instance alındığında, VM kapalı kalsa bile taahhüt bedeli ödenir. | ☐ | ☐ |
| b) Spot VM'ler, Azure kapasiteye ihtiyaç duyduğunda kesilebilir. | ☐ | ☐ |
| c) Pay-as-you-go modelinde uzun dönem taahhüt gerekir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Reservation bir taahhüttür (kullanım değil, rezerve edilen kapasite ödenir); spot kesilebilir kapasitedir; pay-as-you-go'nun tanımı taahhütsüzlüktür.
</details>

#### Soru 17: Tek seçim
Bir geliştirici Azure'u öğrenmeye başlıyor ve beklenmedik bir faturayla karşılaşmamak istiyor. **İlk** yapması gereken nedir?

- A) Reserved instance satın almak
- B) Aboneliğe bir bütçe (budget) ve uyarı eşiği tanımlamak
- C) Tüm kaynakları tek bir region'da açmak
- D) Spot VM kullanmak

<details><summary>Cevap</summary>

**B.** Cost Management bütçesi, harcama eşiğe yaklaşınca uyarı gönderir. Dikkat: bütçe harcamayı **durdurmaz**, yalnızca bildirir. Her laboratuvarı kaynakları silerek (`az group delete`) bitirmek ikinci kuraldır.
</details>

#### Soru 18: Senaryo
Bir şirket e-posta altyapısını kendi Exchange sunucularından Microsoft 365'e (SaaS) taşıdı. Taşımadan sonra aşağıdakilerden hangisi **hâlâ şirketin** sorumluluğundadır?

- A) E-posta sunucularının işletim sistemi yamaları
- B) Datacenter'daki sunucu donanımının yenilenmesi
- C) Çalışanların hesaplarında MFA'nın zorunlu tutulması ve posta kutusu verisinin doğru sınıflandırılması
- D) E-posta uygulamasının yeni sürüme yükseltilmesi

<details><summary>Cevap</summary>

**C.** SaaS'ta uygulama, OS ve fiziksel katman Microsoft'tadır (A, B, D); hesaplar/kimlikler ve veri her modelde müşteride kalır.
</details>

---

**Puanlama:** 16-18 → hazırsın · 12-15 → README'nin 3.2 ve 3.4 bölümlerini tekrar et · < 12 → konuyu baştan çalış, `dotnet run -- C1.1 all` çıktısını incele.


---

## 1.2 Bulutun Temel Faydaları

> **Sınav Alanı:** Describe cloud concepts (%25-30) · **Konu Kodu:** C1.2

### 1. Ne?

Resmî hedef, bulutun faydalarını **dört çift** halinde sayar: *high availability & scalability*, *reliability & predictability*, *security & governance* ve *manageability*. Sınav bunları tek tek tanımlatmaktan çok **senaryodan faydayı buldurur** ve en çok birbirine benzeyenleri (HA ↔ reliability, scalability ↔ elasticity, security ↔ governance) şık yapar.

| Fayda | Tanım | Bulutta nasıl sağlanır | Azure örneği |
|---|---|---|---|
| **High availability** | Arıza olsa bile hizmetin **hedeflenen çalışma süresinde** kalması | Yedeklilik, tek arıza noktasını kaldırmak, otomatik failover | **SLA**'lar, availability zone, scale set |
| **Scalability** | Talebe göre **kapasiteyi artırma/azaltma** yeteneği | **Vertical** (scale up/down) · **horizontal** (scale out/in) | VM boyutu değiştirme, VM Scale Sets, App Service plan |
| **Elasticity** | Ölçeklemenin yüke göre **otomatik** yapılması | Metrik/zaman tabanlı **autoscale** kuralları | Autoscale, Functions'ın otomatik ölçeklemesi |
| **Reliability** | Arızadan **toparlanma** ve çalışmaya devam etme | Dağıtık tasarım, zone/bölge yedekliliği, felaket kurtarma | Region pair, geo-redundant storage, *(bağlam)* Azure Site Recovery |
| **Predictability** | **Performansın** ve **maliyetin** öngörülebilir olması | Performans: autoscale, load balancing, HA · Maliyet: kullanım takibi ve tahmin | Pricing Calculator, Cost Management, Well-Architected Framework (→ S8.3) |
| **Security** | Güvenlik kontrolleri üzerinde **istenen düzeyde kontrol** | IaaS'ta tam kontrol, PaaS/SaaS'ta yamalar sağlayıcıda; altyapı düzeyinde koruma | Defender for Cloud, Key Vault, DDoS Protection |
| **Governance** | Kurumsal standartlara ve regülasyonlara **uyum** | Şablonlar, politikalar, denetim ve uyumluluk raporları | Azure Policy, management group'lar, Purview |
| **Manageability** | Bulutun yönetimi (*of*) ve bulutta yönetim (*in*) | *Of:* otomatik ölçek, izleme ve uyarı, şablonla dağıtım · *In:* portal, CLI, API, PowerShell | Azure Monitor, ARM/Bicep, portal, Cloud Shell |


---

### 2. Neden?

- **Sınav:** Fayda eşleştirme soruları bu alanın en sık çıkan sorularıdır ve "hangisi en iyi tanımlar?" kalıbında iki şık çoğu zaman birbirine çok yakındır. Farkı bilmeyen için bu sorular yazı turasıdır.
- **Gerçek işte:** "Buluta neden geçiyoruz?" sorusunun iş tarafına verilen cevabı bu sekiz faydadır. Bir mimari kararını (iki zone mu, tek VM mi?) savunurken SLA'yı kesinti süresine çevirebilmek ve maliyetiyle tartabilmek gerekir.
- **Sonraki konular:** availability zone ve region pair (C1.4), VM Scale Sets ve availability set (C1.5), Policy ve Purview (C1.12), Monitor (C1.14) — hepsi bu faydaların **araçlarıdır**.

---

### 3. Nasıl?

#### 3.1 Sekiz fayda ve karıştırılan çiftler (`Bölüm 01`, `Bölüm 05`)

**High availability ↔ reliability (↔ disaster recovery)**

- **High availability (HA):** hizmetin çalışmayı **sürdürmesi**; kesinti süresini küçültmek. Ölçüsü **SLA**'dır (%99,9, %99,99 …). Araç: yedeklilik — iki VM'i iki availability zone'a koymak, tek arıza noktasını kaldırmak.
- **Reliability:** sistemin arızadan **toparlanma** ve çalışmaya devam etme yeteneği. Bulut, kaynakları dünyaya yayarak (region'lar, region pair'ler) bir bölgedeki felakette bile başka bölgeden devam etmeyi mümkün kılar.
- *(bağlam)* **Disaster recovery (DR):** reliability'nin "büyük felaket" hali — yedekten veya ikinci bölgeden geri dönme planı.

> Kural: "**devam etti** / kesintisiz kaldı" → HA · "**toparlandı** / geri döndü" → reliability.

**Scalability ↔ elasticity**

- **Scalability:** kaynak **eklenebilme/çıkarılabilme yeteneği**; elle de olabilir.
  - **Vertical scaling (scale up/down):** aynı kaynağın gücünü değiştirmek (daha çok CPU/RAM). Üst sınırı vardır (en büyük VM boyutu); VM'de genellikle yeniden başlatma gerekir.
  - **Horizontal scaling (scale out/in):** kaynak **sayısını** değiştirmek (2 VM → 10 VM). Pratikte sınırsıza yakındır, HA'ya da katkı verir; uygulamanın durumsuz (stateless) olması gerekir.
- **Elasticity:** ölçeklemenin yüke göre **otomatik** yapılması — autoscale kuralı (ör. "CPU %70'i geçerse 2 instance ekle, %30'un altına inerse çıkar").

> Kural: "**eklenebilir/büyütülebilir**" → scalability · "**kendiliğinden/otomatik, yüke göre**" → elasticity.

**Security ↔ governance**

- **Security:** bulut, ihtiyacına göre **kontrol düzeyini** seçtirir: IaaS'ta OS'a kadar her şeyi kendin yapılandırırsın; PaaS/SaaS'ta yamalar ve bakım sağlayıcıda olur. Sağlayıcı ayrıca altyapı düzeyinde koruma (fiziksel güvenlik, DDoS koruması) sunar.
- **Governance:** kuruluşun **kurallarına ve regülasyonlara uyum**: standart şablonlar, politikalar (ör. "yalnızca AB region'ları"), denetim ve uyumluluk raporları; bulut, uyumsuz kaynakları **tespit edip** (ve bazen otomatik **düzeltip**) raporlayabilir.

> Kural: "**saldırı, tehdit, yama, koruma**" → security · "**uyum, standart, kural, denetim**" → governance.

**Predictability'nin iki yüzü**

- **Performans öngörülebilirliği:** autoscale, load balancing ve HA ile kullanıcı sayısı değişse de yanıt sürelerinin hedefte kalması.
- **Maliyet öngörülebilirliği:** kullanımı gerçek zamanlı izlemek, gelecek faturayı tahmin etmek (Cost Management, bütçeler), işe başlamadan maliyet çıkarmak (Pricing Calculator) (→ C1.11).

**Manageability'nin iki yüzü**

- **Management *of* the cloud** (bulutun yönetimi): kaynakların otomatik ölçeklenmesi, şablonla (ARM/Bicep) tekrarlanabilir dağıtım, sağlık izleme, arızalı kaynağın otomatik değiştirilmesi, metrik/uyarı.
- **Management *in* the cloud** (bulutta yönetim): yönetim **arayüzleri** — Azure portal, Azure CLI, Azure PowerShell, REST API (→ C1.13).

#### 3.2 SLA'yı kesinti süresine çevirmek ⭐ (`Bölüm 02`)

**SLA (Service Level Agreement)**, Microsoft'un bir hizmet için taahhüt ettiği **aylık çalışma süresi yüzdesidir**. Taahhüt tutturulamazsa müşteri, faturasından indirim olarak **servis kredisi** talep edebilir. Ay = 730 saat:

| SLA | Günlük | Haftalık | Aylık | Yıllık |
|---|---|---|---|---|
| %99 | 14 dk 24 sn | 1 sa 40 dk | 7 sa 18 dk | 3,65 gün |
| %99,9 | 1 dk 26 sn | 10 dk 4 sn | **43 dk 48 sn** | 8 sa 45 dk |
| %99,95 | 43 sn | 5 dk 2 sn | **21 dk 53 sn** | 4 sa 22 dk |
| %99,99 | 8 sn | 1 dk 0 sn | **4 dk 22 sn** | 52 dk 33 sn |
| %99,999 | < 1 sn | 6 sn | 26 sn | 5 dk 15 sn |

- Her ek "9" kesinti bütçesini **10'a böler**.
- SLA'lar **ürüne ve yapılandırmaya** göre değişir. Örnek (Microsoft'un yayımladığı tipik değerler; güncelini SLA belgesinden kontrol et): tek instance VM (Premium SSD) **%99,9** · aynı availability set'te 2+ VM **%99,95** · iki+ availability zone'a yayılmış 2+ VM **%99,99**.
- **Preview** (önizleme) servislerin SLA'sı **yoktur**; ücretsiz katmanların da genellikle SLA'sı yoktur.
- SLA'yı yükseltmenin yolu **yedekliliktir** — bedeli daha çok kaynak, yani daha çok maliyet.

#### 3.3 Composite SLA *(bağlam — hesap sorusu gelebilir)* (`Bölüm 03`)

Bir uygulama birden çok servisten oluşuyorsa uygulamanın toplam SLA'sı tek tek servislerin SLA'sından farklıdır:

- **Seri bağlı** (hepsi ayakta olmalı; ör. web uygulaması → veritabanı): SLA'lar **çarpılır**.
  `App Service %99,95 × SQL Database %99,99 = %99,94` → toplam SLA, **en zayıf bileşenden de düşüktür**.
- **Paralel / yedekli** (biri ayakta yeterli; ör. aynı yığın iki region'da): `1 − (1 − a) × (1 − b)`.
  `1 − (1 − 0,9994)² ≈ %99,99996` → yedeklilik SLA'yı **yükseltir**. Varsayım: arızalar birbirinden **bağımsız** ve trafiği yönlendiren katman (Front Door/Traffic Manager) çalışıyor — o katman da seri bağlıdır, onun SLA'sıyla da çarpılır.

```bash

> **Alıştırma.** Bir uygulama: Front Door (%99,99) → App Service (%99,95) → Azure SQL Database (%99,99) → Storage (%99,9). (a) Toplam SLA nedir? (b) Bunun aylık kesinti bütçesi kaç dakikadır? (c) Hangi bileşeni iyileştirmek toplam SLA'ya en çok katkı verir?
>
> <details><summary>Çözüm</summary>
>
> (c) En **düşük** SLA'lı bileşen (Storage %99,9): zincirin en zayıf halkası toplamı en çok aşağı çeker. Örneğin daha yüksek SLA'lı bir yedeklilik seçeneği veya okuma için ikinci bölge kullanmak.
> </details>

#### 3.4 Scale up, scale out ve elasticity simülasyonu (`Bölüm 04`)

Simülasyon, gündüz yoğun bir iş uygulamasının 24 saatlik instance ihtiyacını dört stratejiyle karşılar ve **maliyet (instance-saat)**, **yetersiz kalınan saat** ve **boşta kapasite** sayar:

| Strateji | Kavram | 1. gün (her günkü yük) | 2. gün (habersiz kampanya) |
|---|---|---|---|
| Sabit 4 instance | Ölçekleme yok | Ucuz ama gündüz 14 saat yetersiz | Aynı |
| Sabit 10 instance | Zirveye göre boyutlama | Hiç takılmaz, kapasitenin büyük kısmı boşta | Beklenmedik zirvede **o da yetmez** |
| Elle: 08:00'de 10'a çık, 20:00'de 4'e in | **Scalability** | İyi; yalnızca rampa saatlerinde kaçırır | Kampanyayı kaçırır (insan tahminine bağlı) |
| Autoscale: ihtiyaç + 1, saatte en fazla +4 | **Elasticity** | Maliyet ihtiyaca en yakın, kesinti yok | Zirveyi **kendiliğinden** karşılar, bitince küçülür; yalnızca ani sıçramanın ilk saati eksik |

Çıkarım: yük öngörülebilir ise zamanlanmış ölçek bile iyi iş görür; elasticity'nin asıl değeri **öngörülemeyen** yüktedir.

#### 3.5 Pratik — availability zone desteğini görmek (ücretsiz, salt okunur)

Hangi VM boyutunun bir region'da hangi availability zone'larda sunulduğunu sorgulamak HA tasarımının ilk adımıdır (zone'lar → C1.4). Bu komutlar **kaynak oluşturmaz**, temizlik gerekmez:

```bash
az login
# Zone support of small B-series sizes in West Europe (Zones column: 1,2,3)
az vm list-skus --location westeurope --size Standard_B1s --zone --output table
# The same size in another region (--zone lists only zone-capable SKUs; if the output is empty there is no zone support there)
az vm list-skus --location northeurope --size Standard_B1s --zone --output table
```

---

### 4. Gerçek dünya senaryosu

Bir bilet satış platformu büyük bir konser satışından önce mimarisini gözden geçiriyor:

- **Ölçü:** Satış anında site 10 dakika bile kapalı kalırsa gelir kaybı çok büyük. Mevcut yapı: tek VM (%99,9 → ayda ~44 dk kesinti bütçesi) + veritabanı. Seri bağlı oldukları için toplam SLA tek VM'in de altında.
- **High availability:** Web katmanı iki availability zone'a yayılmış bir **VM Scale Set**'e taşınıyor (%99,99); veritabanı yönetilen, zone-redundant bir servise geçiyor. Toplam SLA hesaplanıp iş birimine "ayda en fazla birkaç dakika" olarak anlatılıyor.
- **Elasticity:** Scale set'e autoscale kuralları (CPU ve istek kuyruğu) ve satış saatinden önce kapasiteyi önceden artıran **zamanlanmış** bir kural ekleniyor — ani sıçramanın ilk dakikalarındaki gecikme böylece kapanıyor.
- **Reliability:** Uygulama ikinci bir region'da da hazır tutuluyor; bölgesel bir felakette trafik oraya yönlendirilecek ve veri geo-replication ile oradan devam edecek.
- **Predictability & governance:** Satış haftası için Pricing Calculator ile maliyet çıkarılıyor, Cost Management'ta bütçe kuruluyor; Azure Policy, kaynakların yalnızca onaylı region'larda açılmasını zorunlu kılıyor.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Neredeyse her zaman "Bu senaryo hangi faydayı anlatır?" veya "Aşağıdakilerden hangisi X'i en iyi tanımlar?" kalıbında; evet/hayır tablolarında da sık çıkar. Hesap nadirdir; SLA yüzdesini yaklaşık kesinti süresine çevirebilmek yeterlidir.

| Senaryodaki ifade | Fayda |
|---|---|
| "Bir datacenter'da arıza oldu, hizmet **kesintisiz devam etti**" | High availability |
| "SLA", "çalışma süresi garantisi", "uptime" | High availability |
| "Kapasite **eklenebilir/azaltılabilir**", "daha büyük VM'e geçti" | Scalability (vertical) |
| "VM **sayısı** artırıldı" | Scalability (horizontal / scale out) |
| "Yük artınca **otomatik** büyüdü, azalınca küçüldü" | Elasticity |
| "Bölgesel felaketten sonra **toparlandı**", "başka bölgeden devam etti" | Reliability |
| "Gelecek ayın faturası **tahmin edilebildi**" | Predictability (maliyet) |
| "Kullanıcı arttı, yanıt süresi **değişmedi**" | Predictability (performans) |
| "Yamalar sağlayıcıda", "DDoS koruması", "tehditlere karşı" | Security |
| "Regülasyon/standart **uyumu**", "uyumsuz kaynak tespiti", "denetim" | Governance |
| "Portal, CLI, API ile yönetim" | Manageability (*in*) |
| "Şablonla dağıtım, izleme, uyarı, otomatik ölçek" | Manageability (*of*) |

**Tuzaklar:**
- Scalability ile elasticity aynı senaryoda şık olur: "**otomatik**" kelimesi yoksa scalability.
- "Scale up" ↔ "scale out": up = **daha güçlü**, out = **daha çok**.
- HA ile reliability: SLA ve "kesintisiz" → HA; felaket ve "toparlanma" → reliability.
- "Preview servisin SLA'sı %99,9'dur" → **Hayır**, preview'ların SLA'sı yoktur.
- "SLA'yı yükseltmek için ne yaparsın?" → **yedeklilik** (zone/region'a yayma), "daha büyük VM" değil.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "Elasticity = scalability" | Scalability yetenek; elasticity onun **otomatik** kullanımı |
| "Scale up = daha çok sunucu" | Scale up daha **güçlü** sunucu; daha çok sunucu = scale **out** |
| "Daha büyük VM almak HA sağlar" | Tek büyük VM hâlâ tek arıza noktasıdır; HA için **birden çok** instance ve zone gerekir |
| "İki servisin SLA'sı %99,9 ise uygulama da %99,9" | Seri bağlıysa çarpılır: %99,8 |
| "SLA = Microsoft kesintiyi önler" | SLA bir **taahhüt** ve tazminat (servis kredisi) mekanizmasıdır; tasarım senin işin |
| "Governance = security" | Governance uyum ve kuralları; security koruma ve kontrolü anlatır |
| "Predictability yalnızca maliyet" | Hem performans hem maliyet |
| Autoscale'i kurup sınır koymamak | Max instance ve bütçe alarmı olmadan hatalı bir kural faturayı patlatabilir |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Tek VM → iki zone'a yayılmış VM'ler | SLA %99,9 → %99,99; aylık kesinti bütçesi ~44 dk → ~4 dk | ~2× compute maliyeti; uygulamanın çok instance'a uygun (stateless) olması |
| Tek region → iki region (aktif-pasif / aktif-aktif) | Bölgesel felakete dayanıklılık (reliability), çok yüksek composite SLA | Maliyet, veri replikasyonu ve tutarlılık karmaşıklığı, yönlendirme katmanı |
| Scale up | Basit, uygulama değişikliği gerekmez | Üst sınır, yeniden başlatma, tek arıza noktası |
| Scale out | Neredeyse sınırsız, HA'ya katkı | Stateless tasarım, load balancer, oturum/önbellek yönetimi |
| Autoscale (elasticity) | Maliyet talebi izler, insan müdahalesi yok | Tepki gecikmesi (cold start, cool-down), yanlış kuralla dalgalanma veya fatura sürprizi |
| Sabit zirve kapasitesi | Öngörülebilir performans | Yılın çoğunda boşta kapasite için ödeme |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe the benefits of using cloud services](https://learn.microsoft.com/en-us/training/modules/describe-benefits-use-cloud-services/)
- [Service Level Agreements (SLA) for Online Services](https://www.microsoft.com/licensing/docs/view/Service-Level-Agreements-SLA-for-Online-Services)
- [Azure Well-Architected Framework — Reliability](https://learn.microsoft.com/en-us/azure/well-architected/reliability/) (composite SLA ve hedef belirleme)
- [Autoscale in Azure](https://learn.microsoft.com/en-us/azure/azure-monitor/autoscale/autoscale-overview) · [What are availability zones?](https://learn.microsoft.com/en-us/azure/reliability/availability-zones-overview)
- [`az vm list-skus`](https://learn.microsoft.com/en-us/cli/azure/vm#az-vm-list-skus)


### 📝 Bölüm 1.2 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir e-ticaret sitesinin çalıştığı datacenter'lardan birinde elektrik kesintisi oldu; site, başka bir availability zone'daki kopyası sayesinde kullanıcılar fark etmeden yayında kaldı. Bu, bulutun hangi faydasını gösterir?

- A) Scalability
- B) High availability
- C) Predictability
- D) Governance

<details><summary>Cevap</summary>

**B.** Arızaya rağmen hizmetin **kesintisiz sürmesi** high availability'dir. Scalability kapasite değişimi, predictability öngörülebilirlik, governance uyumdur.
</details>

#### Soru 2: Tek seçim
Bir uygulama, bütün bir Azure region'ını etkileyen bir felaketten sonra eşleşik bölgede yeniden ayağa kaldırıldı ve verilerin çoğu korunmuş olarak çalışmaya devam etti. Bu en iyi hangi faydayla açıklanır?

- A) Reliability
- B) Elasticity
- C) Manageability
- D) Security

<details><summary>Cevap</summary>

**A.** Arızadan/felaketten **toparlanıp** çalışmaya devam etmek reliability'dir (felaket kurtarma bunun bir parçasıdır).
</details>

#### Soru 3: Tek seçim
Bir yönetici, kampanya öncesinde uygulamanın çalıştığı VM'in boyutunu 2 vCPU'dan 8 vCPU'ya yükseltti. Bu işlemin adı nedir?

- A) Scale out
- B) Scale in
- C) Scale up
- D) Failover

<details><summary>Cevap</summary>

**C.** Aynı kaynağı **daha güçlü** hale getirmek vertical scaling, yani scale up'tır. Scale out instance **sayısını** artırmaktır.
</details>

#### Soru 4: Tek seçim
Bir uygulamanın instance sayısı, CPU kullanımı %70'i geçtiğinde **otomatik olarak** 2'den 6'ya çıkıyor ve yük azalınca tekrar 2'ye iniyor. Bu, bulutun hangi faydasıdır?

- A) Elasticity
- B) High availability
- C) Reliability
- D) Predictability

<details><summary>Cevap</summary>

**A.** Ölçeklemenin yüke göre **otomatik** yapılması elasticity'dir. (Scalability bu yeteneğin kendisidir; "otomatik" kelimesi elasticity'yi işaret eder.)
</details>

#### Soru 5: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Scale out, mevcut sunucuya daha fazla bellek eklemek demektir. | ☐ | ☐ |
| b) Horizontal scaling, kaynak sayısını artırmak veya azaltmaktır. | ☐ | ☐ |
| c) Vertical scaling'in, kullanılabilecek en büyük makine boyutuyla sınırlı bir üst limiti vardır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Bellek/CPU eklemek scale **up**'tır; scale out instance sayısını artırır. Vertical scaling en büyük boyutla sınırlıdır.
</details>

#### Soru 6: Cümle tamamlama
"Bir sistemin talebe göre kaynak ekleme veya çıkarma **yeteneğine** \_\_\_\_\_\_\_\_\_\_ denir; bunun yüke göre **otomatik** yapılmasına ise \_\_\_\_\_\_\_\_\_\_ denir."

- A) elasticity · scalability
- B) scalability · elasticity
- C) availability · reliability
- D) reliability · scalability

<details><summary>Cevap</summary>

**B.** Scalability yetenek; elasticity onun otomatik kullanımı.
</details>

#### Soru 7: Tek seçim
Bir finans ekibi, önümüzdeki çeyreğin bulut harcamasını gerçek kullanım verilerine dayanarak tahmin edebiliyor ve bütçe aşımı olmadan planlama yapıyor. Bu hangi faydadır?

- A) Governance
- B) Predictability
- C) Security
- D) Elasticity

<details><summary>Cevap</summary>

**B.** Maliyetin öngörülebilmesi **cost predictability**'dir. Predictability'nin diğer yüzü performans öngörülebilirliğidir.
</details>

#### Soru 8: Tek seçim
Bir web sitesinin kullanıcı sayısı gün içinde 10 kat dalgalanmasına rağmen, load balancer ve autoscale sayesinde sayfa yanıt süresi sürekli aynı aralıkta kalıyor. Bu en iyi hangi faydayla açıklanır?

- A) Performance predictability
- B) Cost predictability
- C) Reliability
- D) Governance

<details><summary>Cevap</summary>

**A.** Yük değişse de performansın **öngörülebilir** kalması. (Autoscale aynı zamanda elasticity'dir; ama sorunun vurgusu "yanıt süresinin aynı kalması" yani performans öngörülebilirliğidir.)
</details>

#### Soru 9: Tek seçim
Bir kuruluş, tüm aboneliklerinde yalnızca Avrupa Birliği içindeki region'larda kaynak oluşturulmasına izin veren ve uyumsuz kaynakları raporlayan kurallar uyguluyor. Bu hangi faydadır?

- A) Security
- B) Governance
- C) High availability
- D) Scalability

<details><summary>Cevap</summary>

**B.** Kurumsal standartlara ve regülasyonlara **uyum**, uyumsuzluğun tespiti ve raporlanması governance'tır (araç: Azure Policy → C1.12).
</details>

#### Soru 10: Tek seçim
Bir şirket PaaS'a geçtikten sonra işletim sistemi güvenlik yamalarının bulut sağlayıcısı tarafından otomatik uygulanmasından ve altyapının DDoS saldırılarına karşı korunmasından faydalanıyor. Bu hangi faydadır?

- A) Security
- B) Governance
- C) Manageability
- D) Predictability

<details><summary>Cevap</summary>

**A.** Tehditlere karşı koruma ve yamaların sağlayıcıda olması security faydasıdır. Bulut, ihtiyaca göre kontrol düzeyi seçtirir: IaaS'ta tam kontrol, PaaS/SaaS'ta bakım sağlayıcıda.
</details>

#### Soru 11: Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** *management of the cloud* (bulutun yönetimi) örneğidir?

- A) Kaynakların talebe göre otomatik ölçeklenmesi
- B) Azure portal üzerinden kaynak oluşturulması
- C) Bir kaynak sorun yaşadığında uyarı gönderilmesi
- D) Azure CLI komutlarının kullanılması

<details><summary>Cevap</summary>

**A ve C.** Bulutun yönetimi: otomatik ölçekleme, şablonla dağıtım, sağlık izleme ve uyarı. Portal ve CLI (B, D) *management in the cloud* — yani yönetim **arayüzleridir**.
</details>

#### Soru 12: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure'daki her servis, preview aşamasında da bir SLA ile sunulur. | ☐ | ☐ |
| b) SLA tutturulamadığında müşteri servis kredisi talep edebilir. | ☐ | ☐ |
| c) Bir servisin SLA'sı, nasıl yapılandırıldığına (ör. tek VM mi, birden çok zone'a yayılmış VM'ler mi) göre değişebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Preview servislerin SLA'sı yoktur. SLA ihlalinde faturadan indirim (servis kredisi) talep edilir. Tek VM ile zone'lara yayılmış VM'lerin SLA'sı farklıdır.
</details>

#### Soru 13: Tek seçim
%99,9 SLA'lı bir servis için bir ayda (730 saat) izin verilen **en fazla** kesinti yaklaşık ne kadardır?

- A) ~4 dakika
- B) ~22 dakika
- C) ~44 dakika
- D) ~7 saat

<details><summary>Cevap</summary>

**C.** `730 sa × 0,001 ≈ 0,73 sa ≈ 43,8 dk`. ~4 dk %99,99'a, ~22 dk %99,95'e, ~7 saat %99'a karşılık gelir. (`dotnet run -- C1.2 downtime 99.9`)
</details>

#### Soru 14: Tek seçim
Bir uygulama, SLA'sı %99,95 olan bir web servisi ile SLA'sı %99,99 olan bir veritabanından oluşuyor; çalışması için **ikisinin de** ayakta olması gerekiyor. Uygulamanın composite SLA'sı yaklaşık nedir?

- A) %99,99
- B) %99,97
- C) %99,95
- D) %99,94

<details><summary>Cevap</summary>

**D.** Seri bağlı bileşenlerde SLA'lar çarpılır: `0,9995 × 0,9999 ≈ 0,9994`. Sonuç en zayıf bileşenden (%99,95) bile düşüktür. (`dotnet run -- C1.2 serial 99.95 99.99`)
</details>

#### Soru 15: Senaryo
Bir şirket, kritik uygulamasının SLA'sını **yükseltmek** istiyor. Hangi değişiklik bunu **en etkili** biçimde sağlar?

- A) Uygulamanın çalıştığı tek VM'i daha büyük bir boyuta yükseltmek
- B) Uygulamayı iki farklı availability zone'daki birden çok VM'e dağıtmak
- C) VM'i spot fiyatlandırmaya geçirmek
- D) VM'e daha fazla disk eklemek

<details><summary>Cevap</summary>

**B.** SLA'yı yükselten şey **yedekliliktir**: zone'lara yayılmış birden çok VM (%99,99), tek VM'den (%99,9) yüksek SLA'ya sahiptir. Daha büyük VM (A) hâlâ tek arıza noktasıdır; spot (C) kesilebilir olduğundan HA'yı düşürür.
</details>

#### Soru 16: Tek seçim (hesap)
Composite SLA'sı %99,9 olan aynı uygulama yığını, birbirinden bağımsız iki region'a kuruluyor ve biri çökerse diğeri tüm trafiği alıyor (yönlendirme katmanını yok sayın). Toplam SLA yaklaşık nedir?

- A) %99,8
- B) %99,9
- C) %99,95
- D) %99,9999

<details><summary>Cevap</summary>

**D.** Paralel (yedekli) bileşenlerde: `1 − (1 − 0,999)² = 1 − 0,000001 = %99,9999`. Seri bağlantıda çarpım SLA'yı düşürürdü (A); yedeklilik yükseltir. (`dotnet run -- C1.2 parallel 99.9 99.9`)
</details>

#### Soru 17: Eşleştirme
Her senaryoyu doğru faydayla eşleştirin: **High availability · Scalability · Reliability · Manageability**

1. Geliştiriciler kaynakları Azure CLI ve REST API ile yönetiyor.
2. Kampanya için uygulamaya elle 10 yeni instance eklendi.
3. Datacenter arızasında uygulama başka zone'dan yayında kaldı.
4. Bölgesel felaketten sonra sistem yedek bölgede yeniden çalıştırıldı.

<details><summary>Cevap</summary>

**1 → Manageability** (*in* the cloud) · **2 → Scalability** (elle olduğu için elasticity değil) · **3 → High availability** (kesintisiz devam) · **4 → Reliability** (toparlanma).
</details>

#### Soru 18: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Elasticity, kaynakların iş yüküne göre otomatik olarak artırılıp azaltılmasıdır. | ☐ | ☐ |
| b) High availability, bir felaketten sonra verilerin yedekten geri yüklenmesi anlamına gelir. | ☐ | ☐ |
| c) Governance, kuruluşun kurallarına ve regülasyonlara uyumu sağlamaya yardımcı olur. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Felaketten sonra geri dönüş reliability/disaster recovery'dir; HA, kesinti **olmadan** çalışmayı sürdürmektir.
</details>

#### Soru 19: Tek seçim
Bir ekip, uygulamanın altyapısını her ortam için aynı **şablondan** dağıtıyor ve kaynakların sağlığını izleyip sorun olduğunda uyarı alıyor. Bu, bulutun hangi faydasının örneğidir?

- A) Manageability
- B) Elasticity
- C) Predictability
- D) High availability

<details><summary>Cevap</summary>

**A.** Şablonla dağıtım ve izleme/uyarı, *management of the cloud* yani manageability'dir (araçlar: ARM/Bicep, Azure Monitor → C1.13, C1.14).
</details>

#### Soru 20: Senaryo
Bir haber sitesi, büyük bir olay olduğunda **önceden tahmin edilemeyen** trafik sıçramaları yaşıyor. Hem sıçrama anında kesinti yaşamamak hem de sakin zamanlarda boşta kapasite için ödeme yapmamak istiyor. Hangi yaklaşım **en uygundur**?

- A) Zirve trafiğe göre boyutlanmış sabit sayıda sunucu çalıştırmak
- B) Her sabah elle sunucu sayısını artırmak
- C) Metrik tabanlı autoscale kuralları ile yatay ölçekleme (elasticity)
- D) Tek bir çok büyük VM kullanmak

<details><summary>Cevap</summary>

**C.** Öngörülemeyen yükte maliyeti talebe bağlayan ve insan müdahalesi gerektirmeyen yaklaşım elasticity'dir. A boşta kapasite için öder; B tahmine dayalıdır ve öngörülemeyen sıçramayı kaçırır; D tek arıza noktasıdır ve üst sınırı vardır.
</details>

---

**Puanlama:** 18-20 → hazırsın · 14-17 → README 3.1'deki "karıştırılan çiftler"i tekrar et · < 14 → konuyu baştan çalış, `dotnet run -- C1.2 all` çıktısını incele.


---

## 1.3 Bulut Hizmet Türleri: IaaS, PaaS, SaaS, Serverless

> **Sınav Alanı:** Describe cloud concepts (%25-30) · **Konu Kodu:** C1.3

### 1. Ne?

Bulut hizmetleri, sağlayıcının yığının **ne kadarını** yönettiğine göre üç ana türe ayrılır; serverless, PaaS'ın en "yönetimsiz" biçimidir:

| | **IaaS** (Infrastructure as a Service) | **PaaS** (Platform as a Service) | **SaaS** (Software as a Service) |
|---|---|---|---|
| Ne kiralarsın | Altyapı: sanal makine, ağ, depolama | Platform: çalışma ortamı (runtime), veritabanı, ara katman | Hazır uygulama |
| Kontrol / esneklik | **En fazla** | Orta | **En az** |
| Yönetim yükü / sorumluluk | **En fazla** | Orta | **En az** |
| Sen ne yaparsın | OS, yama, runtime, uygulama, veri, ağ kuralları | Kod ve veri; uygulama yapılandırması | Kullanıcılar, veri, ayarlar |
| Kullanım senaryosu | **Lift-and-shift** taşıma, test/geliştirme ortamı, özel yazılım, depolama/yedekleme, yüksek performanslı hesap (HPC) | Web/API geliştirme, iş zekâsı/analitik, geliştirme çerçeveleri, IoT backend | E-posta ve iletişim, ofis uygulamaları, CRM/ERP, iş birliği |
| Azure örnekleri | Virtual Machines, VM Scale Sets, Virtual Network, Managed Disks | App Service, Azure SQL Database, Cosmos DB, AKS, **Functions** | Microsoft 365, Dynamics 365, Outlook.com |


---

### 2. Neden?

- **Sınav:** "Ürün → hizmet türü" ve "senaryo → hizmet türü" eşleştirmeleri bu alanın sabit sorularıdır. Shared responsibility (C1.1) soruları da IaaS/PaaS/SaaS'ı bilmeden cevaplanamaz.
- **Gerçek işte:** Hizmet türü seçimi; ekibin ne kadar operasyon yükü taşıyacağını, ne kadar özelleştirme yapabileceğini, maliyet yapısını ve taşıma süresini belirler. Yanlış seçim ya gereksiz operasyon yükü (her şeyi VM'de çalıştırmak) ya da tıkanmış bir platform (PaaS'ın izin vermediği bir bağımlılık) demektir.
- **Sonraki konular:** VM ve scale set (C1.5), App Service/container/Functions (C1.6), depolama (C1.8) bu türlerin Azure'daki ürünleridir.

---

### 3. Nasıl?

#### 3.1 Yığının hangi katmanını kim yönetir? (`Bölüm 01`)

Klasik uygulama yığını gösterimi (Sen = müşteri, MS = Microsoft):

| Katman | On-prem | IaaS | PaaS | SaaS |
|---|---|---|---|---|
| Uygulama | Sen | Sen | Sen | MS |
| Veri | Sen | Sen | Sen | MS* |
| Runtime | Sen | Sen | MS | MS |
| Middleware | Sen | Sen | MS | MS |
| İşletim sistemi | Sen | Sen | MS | MS |
| Sanallaştırma | Sen | MS | MS | MS |
| Sunucular | Sen | MS | MS | MS |
| Depolama | Sen | MS | MS | MS |
| Ağ (fiziksel) | Sen | MS | MS | MS |

\* Bu klasik gösterim bir **sadeleştirmedir**: SaaS'ta verinin saklanması Microsoft'ta olsa da verinin kendisi, hesaplar ve erişim **her modelde müşterinin sorumluluğundadır**. Sınav C1.1'deki shared responsibility tablosunu temel alır.

**Pizza benzetmesi** (hatırlamak için):

| Model | Pizza | Sen ne yaparsın? |
|---|---|---|
| On-prem | Evde sıfırdan yap | Fırın, malzeme, pişirme, masa — hepsi |
| IaaS | Hazır hamur al, evde pişir | Tarif, pişirme, servis (mutfak/altyapı hazır) |
| PaaS | Eve sipariş | Masa, içecek, sunum — yani **kodun ve verin** |
| SaaS | Restorana git | Yalnızca ye — **uygulamayı kullan** |

#### 3.2 IaaS — Infrastructure as a Service

Sağlayıcı fiziksel altyapıyı (sunucu, depolama, ağ, sanallaştırma) işletir; sen üstüne **işletim sisteminden itibaren** her şeyi kurar ve yönetirsin.

- **Avantaj:** en fazla kontrol ve esneklik — istediğin OS, sürücü, ajan, yapılandırma; mevcut uygulamayı **değiştirmeden** taşıyabilirsin (lift-and-shift).
- **Dezavantaj:** en fazla yönetim yükü — OS yaması, antivirüs, yedekleme, güvenlik duvarı, izleme senin işin.
- **Senaryolar:** lift-and-shift taşıma (datacenter boşaltma), hızla açılıp silinen **test/geliştirme ortamları**, özel ya da eski (legacy) yazılım, depolama/yedekleme/kurtarma altyapısı, HPC.
- **Azure:** Virtual Machines, VM Scale Sets, Virtual Network, Managed Disks, *(depolama altyapısı olarak)* Azure Storage; **VM üzerinde SQL Server**.

#### 3.3 PaaS — Platform as a Service

Sağlayıcı altyapıya ek olarak **işletim sistemini, runtime'ı, ara katmanı** ve platformun yamasını, yedeklemesini, ölçeklenmesini işletir; sen **kodunu ve verini** getirirsin.

- **Avantaj:** daha hızlı geliştirme ve dağıtım; OS/yama yükü yok; yerleşik ölçekleme, yedekleme ve yüksek erişilebilirlik.
- **Dezavantaj:** daha az kontrol — OS'a erişemezsin, platformun desteklemediği bir bileşeni (özel sürücü, belirli bir OS ayarı) kuramazsın; platforma bağımlılık.
- **Senaryolar:** web uygulaması ve API geliştirme, iş zekâsı/analitik, uygulama geliştirme çerçeveleri, IoT backend.
- **Azure:** App Service, **Azure SQL Database**, Azure SQL Managed Instance, Cosmos DB, Azure Kubernetes Service (yönetilen Kubernetes; control plane Microsoft'ta), Container Instances; serverless olanlar → 3.5.

#### 3.4 SaaS — Software as a Service

Sağlayıcı **uygulamanın kendisini** işletir; sen yalnızca kullanır, kullanıcıları ve ayarları yönetirsin. Genellikle kullanıcı başına abonelikle ödenir.

- **Avantaj:** en hızlı başlangıç, en az yönetim; güncellemeler ve kapasite sağlayıcıda; her cihazdan erişim.
- **Dezavantaj:** en az kontrol ve özelleştirme — uygulama ne sunuyorsa o; verinin konumu ve yapılandırma seçenekleri sağlayıcıya bağlı.
- **Senaryolar:** e-posta ve mesajlaşma, ofis uygulamaları, CRM/ERP, iş birliği, takvim.
- **Microsoft örnekleri:** Microsoft 365 (Exchange Online, SharePoint, Teams), Dynamics 365, Outlook.com. *(Bunlar Azure servisi değildir ama sınavda SaaS örneği olarak geçer.)*

#### 3.5 Serverless (`Bölüm 04`)

**Serverless**, sunucuların olmadığı anlamına gelmez: sunucuların **sağlanması, ölçeklenmesi ve yönetilmesi tamamen sağlayıcıdadır**; sen yalnızca iş mantığını (kod veya iş akışı) yazarsın. Serverless, PaaS'ın bir alt türü olarak düşünülür.

| Nitelik | Anlamı |
|---|---|
| **Olay tetiklemeli** (event-driven) | Kod bir olay olunca çalışır: HTTP isteği, kuyruk mesajı, dosya yükleme, zamanlayıcı |
| **Otomatik ölçekleme** | Yük artınca kendiliğinden büyür, boşta **sıfıra** iner |
| **Kullanım kadar ücret** | Çağrı sayısı ve çalışma süresi × bellek (GB-saniye) üzerinden; boşta ücret yok |
| **Altyapı yönetimi yok** | VM, OS, kapasite planlaması yok |

Azure'da serverless örnekleri:

- **Azure Functions** — kod (C#, JavaScript, Python …) ile olay tetiklemeli fonksiyonlar. Microsoft yeni uygulamalar için **Flex Consumption** planını öneriyor; klasik Consumption planı da aynı mantıkla (çağrı + GB-saniye, aylık ücretsiz kota) faturalanır.
- **Azure Logic Apps** — kod yazmadan (low-code), görsel tasarımcıyla iş akışı ve entegrasyon (yüzlerce hazır connector).
- **Azure Container Apps** — serverless container platformu; sıfıra kadar ölçeklenir (→ C1.6).
- *(bağlam)* Event Grid (olay yönlendirme), Cosmos DB'nin serverless kapasite modu.

`Section04`, 300 ms süren ve 512 MB bellek kullanan bir fonksiyonun aylık maliyetini çağrı sayısına göre 7/24 açık küçük bir sunucuyla karşılaştırır (oranlar örnektir): aralıklı ve düşük/orta yükte serverless **neredeyse bedavaya** yakındır; çok yüksek ve **sürekli** yükte 7/24 açık kaynak ucuzlayabilir. Trade-off: boştan uyanırken **cold start** gecikmesi ve uzun süren işler için çalışma süresi sınırları.

#### 3.6 Ürün → hizmet türü ve senaryo → hizmet türü (`Bölüm 02`, `Bölüm 03`)

**Karar sırası** (yukarıdan aşağı, ilk "evet"te dur):

1. İhtiyacı karşılayan **hazır bir uygulama** var mı? → **SaaS**
2. Kendi kodun var ve **olayla/aralıklı** mı çalışıyor? → **Serverless** (Functions, Logic Apps)
3. Kendi kodun var, **OS'a erişim gerekmiyor** mu? → **PaaS** (App Service, SQL Database)
4. OS/ağ üzerinde **tam kontrol** veya **aynen taşıma** mı gerekiyor? → **IaaS** (VM)

**Aynı teknoloji, farklı tür:** belirleyici olan ürünün adı değil **yönetim düzeyidir**:

| Teknoloji | IaaS | PaaS | SaaS |
|---|---|---|---|
| SQL Server | SQL Server on Azure VM | Azure SQL Database, SQL Managed Instance | — |
| Web sitesi | VM'e IIS/nginx kurmak | App Service | Hazır site oluşturucu |
| E-posta | VM'de Exchange Server | — | Exchange Online (Microsoft 365) |
| Container | VM'e Docker kurmak | AKS, Container Instances | — |

#### 3.7 Pratik — aynı uygulama, üç hizmet türü

**(a) Pricing Calculator karşılaştırması.** Küçük bir "Todo" web uygulaması (aylık ~50.000 istek, 1 GB veri, 20 kullanıcılı ekip) için [Azure Pricing Calculator](https://azure.microsoft.com/pricing/calculator/)'da iki tahmin çıkar ve tabloyu doldur:

| | IaaS | PaaS | SaaS |
|---|---|---|---|
| Bileşenler | 1 Linux VM (küçük boyut) + VM'de veritabanı + disk | App Service planı + Azure SQL Database (en küçük katman) | Hazır bir görev yönetimi SaaS ürünü (kullanıcı başı abonelik) |
| Aylık tahmini maliyet | | | |
| OS yamasını kim yapar? | | | |
| Veritabanı yedeğini kim alır? | | | |
| Ölçeklemek için ne gerekir? | | | |
| Özelleştirme sınırı | | | |

<details><summary>Beklenen sonuç (fiyatlar region'a ve zamana göre değişir; yapıyı karşılaştır)</summary>

| | IaaS | PaaS | SaaS |
|---|---|---|---|
| Aylık maliyet | Küçük VM + disk sabit; 7/24 açık kalırsa kullanılmasa da ödenir | Küçük katmanlarda benzer veya daha düşük; yedek/HA fiyata dahil | Kullanıcı sayısı × abonelik; altyapı maliyeti yok |
| OS yaması | **Sen** | Microsoft | Microsoft |
| Veritabanı yedeği | **Sen** (veya VM yedeği kurarsın) | Microsoft (otomatik yedek, saklama süresini sen seçersin) | Sağlayıcı |
| Ölçekleme | Daha büyük VM veya scale set + load balancer kurmak | Plan katmanını/instance sayısını değiştirmek (autoscale) | Daha çok lisans almak |
| Özelleştirme | Sınırsız | Platformun izin verdiği kadar | Uygulamanın sunduğu ayarlar kadar |

Sonuç: Kontrol IaaS → SaaS yönünde azalır; yönetim yükü de onunla birlikte azalır. Maliyet karşılaştırmasında yalnızca fatura değil **operasyon emeği** (yama, yedek, izleme) de hesaba katılmalıdır.
</details>

**(b) CLI ile IaaS ve PaaS farkını hissetmek.** Bütçe alarmın kurulu olsun (→ C1.1). PaaS kısmı ücretsiz F1 katmanını kullanır; VM **ücretlidir** — oluşturup inceledikten hemen sonra sil.

```bash
az group create --name rg-az900-c13 --location westeurope

# PaaS: App Service (F1 = free tier). No OS, no server; just a web app.
az webapp list-runtimes --os linux --output table              # valid runtime values
az appservice plan create --resource-group rg-az900-c13 --name plan-c13 --sku F1 --is-linux
az webapp create --resource-group rg-az900-c13 --plan plan-c13 \
  --name todo-c13-$RANDOM --runtime "DOTNETCORE:10.0"
az webapp list --resource-group rg-az900-c13 --query "[].defaultHostName" --output tsv

# IaaS: VM (paid!). You choose the OS; patching, firewall, software installation are now your job.
az vm create --resource-group rg-az900-c13 --name vm-c13 \
  --image Ubuntu2404 --size Standard_B1s \
  --admin-username azureuser --generate-ssh-keys
az vm list --resource-group rg-az900-c13 --show-details --output table
# (Optional) ssh azureuser@<publicIp> → sudo apt update && sudo apt upgrade  ← in IaaS this responsibility is yours

# Cleanup — mandatory
az group delete --name rg-az900-c13 --yes --no-wait
```

Gözlem: App Service için ne bir OS imajı seçtin ne de bir disk; VM için imaj, boyut, kullanıcı, SSH anahtarı ve (oluşturulan) public IP/NSG gibi altyapı parçalarıyla uğraştın. Fark, shared responsibility tablosunun ta kendisidir.

---

### 4. Gerçek dünya senaryosu

Bir lojistik şirketi on yıllık uygulama portföyünü buluta taşıyor ve her iş yükü için hizmet türünü seçiyor:

| İş yükü | Karar | Neden |
|---|---|---|
| Üreticisi artık destek vermeyen, özel bir Windows sürücüsüne bağımlı depo yönetim yazılımı | **IaaS** (VM, lift-and-shift) | Kod değiştirilemiyor, OS düzeyinde kontrol gerekiyor; datacenter kapanış tarihine yetişmeli |
| Müşterilerin gönderi takip ettiği web sitesi ve API | **PaaS** (App Service + Azure SQL Database) | Ekip kod yazıyor, OS yönetmek istemiyor; kampanyada autoscale |
| Sürücülerin yüklediği teslimat fotoğraflarını küçültüp etiketleyen işlem | **Serverless** (Azure Functions, blob yükleme tetikleyicisi) | Gün içinde düzensiz, gece neredeyse hiç; boşta ücret yok |
| Muhasebe sistemine fatura aktaran onay akışı | **Serverless** (Logic Apps) | Hazır connector'larla kod yazmadan entegrasyon |
| Kurum içi e-posta ve doküman paylaşımı | **SaaS** (Microsoft 365) | Hazır ürün; sunucu yönetimi tamamen kalkıyor |

Bir yıl sonra depo yazılımı yeniden yazıldığında o da VM'den PaaS'a geçiriliyor (**refactor**): lift-and-shift hızlı taşımanın, PaaS ise uzun vadeli işletme kolaylığının aracıdır.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** (1) Ürün → tür eşleştirme, (2) senaryo → tür seçimi, (3) türlerin nitelikleri hakkında evet/hayır, (4) serverless'in nitelikleri.

| Soru kalıbı | Cevap |
|---|---|
| "İşletim sistemine **tam erişim/kontrol**", "özel yazılım/sürücü kurulacak" | **IaaS** |
| "Mevcut sunucuları **değiştirmeden** taşı" (lift-and-shift) | **IaaS** |
| "**En fazla** esneklik/kontrol" | **IaaS** |
| "Geliştiriciler yalnızca **koda** odaklansın", "OS yaması yok" | **PaaS** |
| "**En az** yönetim yükü" + hazır uygulama | **SaaS** |
| "**En az** yönetim yükü" + kendi kodunu çalıştırma | **PaaS / serverless** |
| "Yalnızca kod **çalışırken** ödeme", "olaya tepki olarak çalışma" | **Serverless** |
| Azure SQL Database / App Service / Cosmos DB | **PaaS** |
| VM üzerinde SQL Server / Virtual Machines | **IaaS** |
| Microsoft 365 / Dynamics 365 / Outlook.com | **SaaS** |
| Azure Functions / Logic Apps | **Serverless** (PaaS) |

**Tuzaklar:**
- "SQL" kelimesi tek başına türü belirlemez: **nerede ve kim yönetiyor?**
- "En az yönetim yükü" sorusunda şıklarda hem SaaS hem PaaS varsa senaryoyu oku: **kendi uygulamasını** geliştiren bir ekip için SaaS bir seçenek değildir.
- "Serverless'te sunucu yoktur" → **Hayır**; sunucular vardır, yönetimleri sağlayıcıdadır.
- "PaaS'ta müşteri işletim sistemini yamar" → **Hayır**.
- "IaaS'ta müşteri fiziksel sunucudan sorumludur" → **Hayır** (fiziksel her şey Microsoft'ta).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "Serverless = sunucusuz" | Sunucular var; sağlama, ölçekleme ve yönetim sağlayıcıda |
| "Azure SQL = IaaS" | Azure SQL Database ve Managed Instance PaaS; yalnızca VM'deki SQL Server IaaS |
| "SaaS'ta hiçbir sorumluluğum yok" | Kullanıcılar, erişim, MFA ve veri hâlâ sende (→ C1.1) |
| "PaaS her zaman IaaS'tan ucuz" | Fatura tek başına belirleyici değil; ama operasyon emeği hesaba katılınca genellikle PaaS lehine |
| "Her şeyi VM'e koyalım, sonra bakarız" | Lift-and-shift hızlıdır ama IaaS'ın tüm işletme yükünü getirir; uygun iş yüklerini zamanla PaaS'a taşımak planlanmalı |
| "Functions her iş için en ucuzu" | Çok yüksek ve sürekli yükte 7/24 açık bir plan daha ucuz olabilir; ayrıca cold start ve süre sınırları |
| Microsoft 365'i "Azure servisi" saymak | Microsoft'un SaaS ürünüdür; Azure'un kendi servis kataloğunda değildir |

---

### 7. Performans & trade-off

| Boyut | IaaS | PaaS | Serverless | SaaS |
|---|---|---|---|---|
| Kontrol/özelleştirme | ★★★★ | ★★★ | ★★ | ★ |
| Yönetim yükü | Yüksek | Orta | Düşük | En düşük |
| Ölçekleme | Kendin kurarsın (scale set, LB) | Yerleşik (plan/instance, autoscale) | Otomatik, sıfıra kadar | Sağlayıcı |
| Maliyet modeli | Çalıştığı süre (boşta da) | Plan/instance süresi | Çağrı + çalışma süresi; boşta 0 | Kullanıcı başı abonelik |
| Taşıma hızı (mevcut uygulama) | En hızlı (değişiklik yok) | Uygulama uyarlanmalı | Yeniden tasarım gerekir | Veri taşıma + süreç değişikliği |
| Tipik risk | Yama/güvenlik ihmali | Platform kısıtları | Cold start, süre sınırı, satıcı bağımlılığı | Özelleştirme ve veri konumu sınırları |

Kural: **İşletme yükünü azaltabildiğin kadar yukarı (SaaS'a doğru) çık; kontrol gerektiren yerde aşağıda (IaaS) kal.**

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe cloud service types](https://learn.microsoft.com/en-us/training/modules/describe-cloud-service-types/)
- [What is IaaS?](https://azure.microsoft.com/resources/cloud-computing-dictionary/what-is-iaas) · [What is PaaS?](https://azure.microsoft.com/resources/cloud-computing-dictionary/what-is-paas) · [What is SaaS?](https://azure.microsoft.com/resources/cloud-computing-dictionary/what-is-saas) · [What is serverless computing?](https://azure.microsoft.com/resources/cloud-computing-dictionary/what-is-serverless-computing)
- [Azure Functions hosting options](https://learn.microsoft.com/en-us/azure/azure-functions/functions-scale) (Flex Consumption, Consumption, Premium, Dedicated)
- [Azure Pricing Calculator](https://azure.microsoft.com/pricing/calculator/) · [Azure Functions pricing](https://azure.microsoft.com/pricing/details/functions/)
- [`az webapp`](https://learn.microsoft.com/en-us/cli/azure/webapp) · [`az vm create`](https://learn.microsoft.com/en-us/cli/azure/vm#az-vm-create)


### 📝 Bölüm 1.3 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir şirket, kurum içindeki bir uygulamayı **hiçbir kod değişikliği yapmadan** ve işletim sistemi üzerinde tam kontrolü koruyarak buluta taşımak istiyor. Hangi hizmet türü **en uygundur**?

- A) SaaS
- B) PaaS
- C) IaaS
- D) Serverless

<details><summary>Cevap</summary>

**C.** Lift-and-shift ve OS üzerinde tam kontrol = IaaS (Azure Virtual Machines). PaaS ve serverless OS erişimi vermez; SaaS hazır uygulamadır.
</details>

#### Soru 2: Eşleştirme
Her ürünü hizmet türüyle eşleştirin: **IaaS · PaaS · SaaS**

1. Azure Virtual Machines
2. Azure SQL Database
3. Microsoft 365
4. Azure App Service
5. Dynamics 365

<details><summary>Cevap</summary>

**1 → IaaS · 2 → PaaS · 3 → SaaS · 4 → PaaS · 5 → SaaS.**
</details>

#### Soru 3: Tek seçim
Bir geliştirme ekibi web API'sini yayınlamak istiyor. Ekip işletim sistemi yaması, sunucu yapılandırması veya runtime kurulumu ile uğraşmak istemiyor; yalnızca kodlarına odaklanmak istiyor. Hangi hizmet türü **en uygundur**?

- A) IaaS
- B) PaaS
- C) SaaS
- D) On-premises

<details><summary>Cevap</summary>

**B.** Kendi kodunu çalıştırıp OS/runtime yönetmemek PaaS'tır (ör. App Service). SaaS hazır uygulama olduğundan kendi API'lerini yayınlamaya uygun değildir.
</details>

#### Soru 4: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) PaaS'ta işletim sistemi güncellemeleri müşterinin sorumluluğundadır. | ☐ | ☐ |
| b) IaaS, üç hizmet türü arasında müşteriye en fazla kontrolü verir. | ☐ | ☐ |
| c) SaaS'ta müşteri, uygulamanın yeni sürümlerini kendisi kurmak zorundadır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** PaaS'ta OS Microsoft'ta; IaaS en fazla kontrolü verir; SaaS'ta uygulama güncellemeleri sağlayıcıdadır.
</details>

#### Soru 5: Tek seçim
Bir şirket SQL Server'ı bir Azure sanal makinesine kurup çalıştırıyor. Bu dağıtım hangi hizmet türüdür?

- A) IaaS
- B) PaaS
- C) SaaS
- D) Serverless

<details><summary>Cevap</summary>

**A.** VM üzerinde SQL Server = IaaS: OS ve SQL Server yamaları müşteride. Yönetilen Azure SQL Database veya Managed Instance olsaydı PaaS olurdu.
</details>

#### Soru 6: Tek seçim
Bir kuruluş, 300 çalışanının **hemen** e-posta, takvim ve ofis uygulamalarını kullanmasını istiyor; hiçbir sunucu veya uygulama yönetmek istemiyor. Hangi hizmet türü **en uygundur**?

- A) IaaS
- B) PaaS
- C) SaaS
- D) Hybrid cloud

<details><summary>Cevap</summary>

**C.** Hazır uygulamayı abonelikle kullanmak SaaS'tır (ör. Microsoft 365). D bir dağıtım modelidir, hizmet türü değildir.
</details>

#### Soru 7: Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** serverless bilgi işlemin niteliğidir?

- A) Müşteri, kodun çalıştığı sanal makinelerin boyutunu seçer ve yamalar
- B) Kod, bir olay (HTTP isteği, kuyruk mesajı, dosya yükleme) ile tetiklenir
- C) Kaynaklar yüke göre otomatik ölçeklenir ve boşta kaldığında ücret oluşmaz
- D) Kaynaklar 7/24 ayrılmış kapasiteyle sabit ücretle çalışır

<details><summary>Cevap</summary>

**B ve C.** Serverless: olay tetiklemeli, otomatik ölçekleme (sıfıra kadar), kullanım kadar ücret, altyapı yönetimi yok. A IaaS'ı, D sabit kapasiteli planları anlatır.
</details>

#### Soru 8: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Serverless'te kodun çalıştığı fiziksel sunucular yoktur. | ☐ | ☐ |
| b) Azure Functions bir serverless bilgi işlem hizmetidir. | ☐ | ☐ |
| c) Azure Logic Apps, kod yazmadan iş akışları oluşturmaya olanak tanır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Sunucular vardır; sağlanmaları ve yönetimleri sağlayıcıdadır. Functions (kod) ve Logic Apps (görsel, low-code iş akışı) Azure'un serverless hizmetleridir.
</details>

#### Soru 9: Senaryo
Bir uygulama, kullanıcılar bir Blob Storage hesabına fotoğraf yüklediğinde her fotoğrafın küçük bir kopyasını oluşturmalı. Yüklemeler gün içinde düzensiz geliyor, gece neredeyse hiç yok. Şirket yalnızca işlem yapıldığında ödeme yapmak istiyor. Hangi çözüm **en uygundur**?

- A) 7/24 çalışan bir sanal makine
- B) Azure Functions
- C) Microsoft 365
- D) Kurum içi bir sunucu

<details><summary>Cevap</summary>

**B.** Olay (blob yükleme) ile tetiklenen, düzensiz yük ve "yalnızca çalışırken ödeme" = serverless, Azure Functions. VM (A) boşta da ücretlidir.
</details>

#### Soru 10: Sıralama
Aşağıdaki hizmet türlerini, **müşterinin yönetim yükü en fazladan en aza** doğru sıralayın: SaaS · IaaS · PaaS

<details><summary>Cevap</summary>

**IaaS → PaaS → SaaS.** Kontrol de aynı sırayla azalır.
</details>

#### Soru 11: Tek seçim
Aşağıdaki senaryolardan hangisi **en tipik** IaaS kullanım senaryosudur?

- A) Çalışanlara hazır bir CRM uygulaması sunmak
- B) Geliştiricilerin, gerektiğinde dakikalar içinde açıp işi bitince silebilecekleri, özel yazılım kurulu test ortamları oluşturmak
- C) Kod yazmadan iki SaaS uygulaması arasında veri aktarmak
- D) Bir web API'sini OS yönetmeden yayınlamak

<details><summary>Cevap</summary>

**B.** Hızla açılıp kapatılan, özel yapılandırmalı test/geliştirme ortamları IaaS'ın klasik senaryosudur. A → SaaS, C → serverless (Logic Apps), D → PaaS.
</details>

#### Soru 12: Tek seçim
Bir şirket "SQL Server ile neredeyse tam uyumlu ama yama ve yedeklemeyi Microsoft'un yaptığı" bir veritabanı istiyor. Hangisini seçmelidir ve bu hangi hizmet türüdür?

- A) SQL Server on Azure VM — IaaS
- B) Azure SQL Managed Instance — PaaS
- C) Microsoft 365 — SaaS
- D) Azure Virtual Machines — IaaS

<details><summary>Cevap</summary>

**B.** SQL Managed Instance, SQL Server'a yüksek uyumluluğu yönetilen (PaaS) bir hizmette sunar. A da uyumludur ama yama/yedek müşteride kalır.
</details>

#### Soru 13: Cümle tamamlama
"\_\_\_\_\_\_\_\_\_\_ modelinde bulut sağlayıcısı donanımı, işletim sistemini ve runtime'ı yönetir; müşteri ise uygulamasını ve verisini yönetir."

- A) IaaS
- B) PaaS
- C) SaaS
- D) On-premises

<details><summary>Cevap</summary>

**B.** PaaS'ın tanımı: platform (OS, runtime, ara katman) sağlayıcıda, uygulama ve veri müşteride.
</details>

#### Soru 14: Senaryo
Bir yazılım ekibi kendi geliştirdiği uygulamayı çalıştıracak ve **en az yönetim yükünü** istiyor. Şıklarda şunlar var: (A) Azure Virtual Machines, (B) Azure App Service, (C) Microsoft 365, (D) kendi datacenter'ları. Hangisini seçmelidir?

<details><summary>Cevap</summary>

**B.** "En az yönetim yükü" sorusunda SaaS (C) ancak ihtiyaç **hazır bir uygulamayla** karşılanıyorsa doğrudur; burada ekip **kendi uygulamasını** çalıştıracak, bu yüzden en az yükü PaaS (App Service) verir.
</details>

#### Soru 15: Evet / Hayır

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


---

# BÖLÜM 2: Describe Azure Architecture and Services (%35-40)

---

## 2.1 Azure Temel Mimari Bileşenleri

> **Sınav Alanı:** Describe Azure architecture and services (%35-40) · **Konu Kodu:** C2.1

### 1. Ne?

Azure'un mimarisi iki eksende anlatılır:

| Eksen | Bileşenler | Soruyu cevaplar |
|---|---|---|
| **Fiziksel altyapı** | Datacenter → **availability zone** → **region** → **region pair** · **sovereign region** | Kaynağım fiziksel olarak **nerede** çalışıyor, hangi arızaya dayanıyor? |
| **Mantıksal (yönetim) yapı** | **Resource** → **resource group** → **subscription** → **management group** (→ Entra tenant) | Kaynaklarımı **nasıl düzenler**, faturalar ve yetkilendiririm? |

```
Physical                                   Logical (management hierarchy)
────────                                   ──────────────────────────────
Geography (e.g. Europe)                    Microsoft Entra tenant
 └─ Region pair: North Europe ↔ West Europe  └─ Root management group
     └─ Region: West Europe                      └─ Management group(s) (≤ 6 levels)
         └─ Availability zone 1, 2, 3                └─ Subscription
             └─ Datacenter(s)                            └─ Resource group
                                                             └─ Resource (VM, VNet, storage…)
```

Bu konunun kodu yok; pratik, README'deki `az` CLI laboratuvarıdır (3.7).

---

### 2. Neden?

- **Sınav:** "Describe Azure architecture and services" alanı sınavın en ağır alanıdır (**%35-40**); availability zone ↔ region pair ve resource group kuralları bu alanın en sık sorulan maddeleridir.
- **Gerçek işte:** Region seçimi gecikmeyi, maliyeti, veri ikametgâhını (regülasyon) ve dayanıklılığı belirler. Resource group/subscription tasarımı ise kimin neye erişeceğini, faturanın nasıl bölüneceğini ve bir kaynağın ne zaman güvenle silinebileceğini belirler. Yanlış bir `az group delete` bütün bir ortamı siler.
- **Sonraki konular:** RBAC (C1.9), Policy ve lock (C1.12), tag ve maliyet (C1.11) bu hiyerarşinin **kapsamlarına** (management group, subscription, resource group, kaynak) uygulanır ve aşağı doğru miras alınır.

---

### 3. Nasıl?

#### 3.1 Datacenter ve region

- **Datacenter:** sunucu rafları, bağımsız güç, soğutma ve ağ altyapısı olan fiziksel tesis. Müşteri belirli bir datacenter'ı **seçemez**; datacenter'lar region ve zone olarak gruplanarak sunulur.
- **Region:** bir coğrafi alanda bulunan ve **düşük gecikmeli** bir ağla birbirine bağlı bir veya daha çok datacenter. Azure'da 60'ı aşkın region vardır; kaynak oluştururken genellikle bir region seçersin.
  - Servis ve özellik **uygunluğu** region'a göre değişir (her servis veya VM boyutu her region'da yoktur).
  - **Fiyat** region'a göre değişebilir (→ C1.11).
  - Bazı servisler **global**'dir, belirli bir region'a bağlanmaz: **Microsoft Entra ID**, **Azure Front Door**, **Traffic Manager**, **Azure DNS**.

**Region seçim kriterleri:**

| Kriter | Soru |
|---|---|
| Gecikme | Kullanıcılarım nerede? En yakın region hangisi? |
| Veri ikametgâhı / regülasyon | Veri belirli bir ülke/coğrafya içinde kalmalı mı? |
| Servis uygunluğu | İhtiyacım olan servis/VM boyutu bu region'da var mı? |
| Fiyat | Aynı kaynak hangi region'da daha ucuz? |
| Dayanıklılık | Region availability zone destekliyor mu, eşleşik bölgesi var mı? |

#### 3.2 Availability zones ⭐

**Availability zone**, bir region içinde **fiziksel olarak ayrı** bir veya daha çok datacenter'dan oluşan gruptur; her zone'un **bağımsız gücü, soğutması ve ağı** vardır. Zone'lar birbirine yüksek hızlı, özel fiber ağla bağlıdır.

- Zone destekleyen region'larda **en az üç** zone bulunur. Her region zone desteklemez.
- Koruduğu arıza: **bir datacenter'ın (zone'un) tamamen çökmesi** — elektrik, soğutma veya ağ kesintisi. Bir kaynağın kopyalarını farklı zone'lara koyarsan biri düşse de diğerleri çalışır (→ C1.2: iki zone'a yayılmış VM'ler %99,99 SLA).
- Zone'ları kullanan servisler iki gruptadır:

| Tür | Anlamı | Örnek |
|---|---|---|
| **Zonal** | Kaynağı **sen** belirli bir zone'a sabitlersin (ör. zone 1) | Virtual Machines, managed disk, public IP |
| **Zone-redundant** | Platform kaynağı zone'lar arasında **otomatik** çoğaltır | ZRS storage (→ C1.8), Azure SQL Database (zone-redundant yapılandırma) |
| *(bağlam)* Zone desteği olmayan / global | Zone kavramı uygulanmaz | Entra ID, Front Door |

> Zone yedekliliği ek maliyet getirir: daha çok kaynak ve zone'lar arası veri aktarımı (→ C1.11).

#### 3.3 Region pairs ⭐

Çoğu region, **aynı coğrafya** içinde (ör. Avrupa, ABD), mümkün olduğunca birbirinden **en az ~300 mil (≈ 480 km)** uzakta başka bir region'la **eşleştirilmiştir**: West Europe ↔ North Europe, East US ↔ West US gibi.

Eşleşmenin faydaları:

- **Bölgesel felakette öncelik:** Geniş çaplı bir kesintide, eşleşik iki bölgeden birinin **öncelikli** olarak kurtarılması.
- **Sıralı güncelleme:** Planlı Azure güncellemeleri eşleşik bölgelere **aynı anda değil, sırayla** uygulanır; hatalı bir güncelleme iki bölgeyi birden etkilemez.
- **Replikasyon:** Bazı servisler (ör. **GRS** storage → C1.8) ikincil kopyayı otomatik olarak eşleşik bölgede tutar.
- **Veri ikametgâhı:** Eşleşik bölge aynı coğrafyada olduğundan veri (yasal amaçlar için) coğrafya içinde kalır.

Dikkat edilecek ayrıntılar:

- Bazı eşleşmeler **tek yönlüdür** (ör. bir region'ın yedeği başka bir region'dır ama tersi farklıdır; klasik örnek: West India → South India, South India'nın eşi Central India).
- Daha yeni bazı region'ların **eşleşik bölgesi yoktur**; bu region'larda bölgesel dayanıklılık availability zone'larla ve uygulamanın kendi çok-bölgeli tasarımıyla sağlanır.

> Kural: **datacenter/zone arızası** → availability zone · **bütün bir bölgenin felaketi** → region pair (veya çok bölgeli tasarım).

#### 3.4 Sovereign regions

Ana (public) Azure'dan **yasal ve işletimsel olarak ayrılmış** Azure örnekleri:

| Sovereign cloud | Kim için | Özellik |
|---|---|---|
| **Azure Government** | ABD kamu kurumları ve iş ortakları | Fiziksel ve ağ olarak ayrı; yalnızca taranmış (screened) ABD personeli işletir; FedRAMP, DoD gibi uyumluluk gereksinimleri |
| **Azure China** (Azure operated by 21Vianet) | Çin'de hizmet | Microsoft değil **21Vianet** işletir; Çin regülasyonlarına uyum; global Azure'dan fiziksel olarak ayrı |

Sovereign bulutlarda portal adresi, servis listesi ve fiyatlar global Azure'dan farklı olabilir.

#### 3.5 Resources ve resource groups ⭐

- **Resource (kaynak):** Azure'da oluşturduğun her yönetilebilir öğe — VM, VNet, storage account, veritabanı, web app, public IP, NSG…
- **Resource group (RG):** kaynakların **mantıksal kabı**. Genellikle **aynı yaşam döngüsünü** paylaşan (birlikte oluşturulup, güncellenip, silinen) kaynaklar aynı gruba konur — ör. "bir uygulamanın test ortamı".

**Resource group kuralları** (sınavın en sevdiği liste):

| Kural | Ayrıntı |
|---|---|
| Her kaynak **tam olarak bir** gruptadır | Bir kaynak gruba ait olmadan var olamaz ve iki gruba birden ait olamaz |
| Gruplar **iç içe olmaz** | RG içinde RG yoktur |
| Kaynaklar **taşınabilir** | Bir kaynak başka bir RG'ye (hatta başka bir subscription'a) taşınabilir; bazı kaynak türlerinde kısıtlar vardır |
| Kaynakların region'ı **farklı** olabilir | RG'nin bir konumu vardır (meta verisinin tutulduğu yer) ama içindeki kaynaklar farklı region'larda olabilir |
| **Grup silinirse içindeki her şey silinir** | Ortamı tek komutla temizlemenin yolu; aynı zamanda en tehlikeli işlem (→ C1.12 resource lock) |
| Erişim ve yönetim **kapsamıdır** | RG'ye verilen RBAC rolü, Policy ve lock içindeki kaynaklara **miras** kalır (→ C1.9, C1.12) |

> Tag'ler ise **varsayılan olarak miras alınmaz**: RG'ye verilen tag içindeki kaynaklara geçmez (→ C1.11).

**Nasıl gruplanır?** Yaşam döngüsüne göre (uygulama/ortam: `rg-shop-prod`, `rg-shop-test`), erişime göre (aynı ekibin yönettiği kaynaklar), veya faturalama/raporlamaya göre. Tek bir doğru yoktur; kural "**birlikte silinecek olanlar birlikte durur**"dur.

#### 3.6 Subscriptions ve management groups ⭐

**Subscription**, Azure kaynaklarının oluşturulduğu **yönetim, faturalama ve ölçek birimidir**; her resource group bir subscription'a aittir. Bir subscription iki tür sınır çizer:

- **Billing boundary:** her subscription için ayrı maliyet raporu ve fatura; maliyeti subscription bazında ayırmak.
- **Access control boundary:** erişim politikaları subscription düzeyinde uygulanabilir; ayrı ekiplere ayrı subscription.

**Neden birden çok subscription?**

| Neden | Örnek |
|---|---|
| Ortam ayrımı | `sub-dev`, `sub-test`, `sub-prod` — üretime erişim daha sıkı |
| Organizasyon yapısı | Departman/iş birimi başına subscription |
| Faturalama | Maliyet merkezi başına ayrı fatura |
| **Kota/limit** | Subscription başına kaynak limitleri aşıldığında yeni subscription |

Subscription türleri: ücretsiz deneme (free account), pay-as-you-go, Visual Studio abonelikleri, **Enterprise Agreement (EA)**, Microsoft Customer Agreement, **CSP** (bir iş ortağı üzerinden). Bir **Azure hesabı** birden çok subscription'a sahip olabilir; her subscription **tek bir** Entra tenant'a güvenir.

**Management group**, subscription'ları gruplayan **kapsamdır**. Bir management group'a uygulanan **Azure Policy** ve **RBAC** ataması, altındaki **tüm** subscription'lara (ve onların RG ve kaynaklarına) miras kalır — "bu kuralı şirketin 40 subscription'ına birden uygula" sorusunun cevabı budur.

| Kural | Değer |
|---|---|
| Root | Her tenant'ta **tek bir root management group** vardır; tüm management group ve subscription'lar onun altındadır |
| Derinlik | Root ve subscription seviyesi hariç **6 seviyeye kadar** |
| Sayı | Tek bir dizinde **10.000** management group'a kadar |
| Ebeveyn | Her management group ve subscription **yalnızca bir** ebeveyne bağlıdır (ama bir ebeveynin birden çok çocuğu olabilir) |

**Hiyerarşi ve miras (yukarıdan aşağı):**

```
Entra tenant
└─ Root management group              ← "All resources only in EU regions" (Policy)
   ├─ MG: Platform
   │  └─ sub-connectivity
   └─ MG: Applications                 ← "Security team has Reader everywhere" (RBAC)
      ├─ MG: Prod
      │  └─ sub-prod
      │     └─ rg-shop-prod            ← CanNotDelete lock (→ C1.12)
      │        └─ VM, SQL Database, storage
      └─ MG: Dev/Test
         └─ sub-dev
            └─ rg-shop-dev
```

#### 3.7 Pratik — region keşfi, resource group yaşam döngüsü, hiyerarşi

Bu laboratuvar **neredeyse ücretsizdir**: boş bir VNet ücret üretmez, boş bir storage account kuruşluk maliyetlidir ve hemen silinir. Bütçe alarmın kurulu değilse önce C1.1 / 3.6'yı yap.

```bash
az login

# 1) Region discovery
az account list-locations --output table
# Only physical regions and their paired regions
az account list-locations \
  --query "[?metadata.regionType=='Physical'].{Region:name, Name:displayName, Pair:metadata.pairedRegion[0].name, Geography:metadata.geographyGroup}" \
  --output table
# Which zones have the VM size in a region? (same command as C1.2 / 3.5)
az vm list-skus --location westeurope --size Standard_B1s --zone --output table

# 2) Resource group lifecycle: two resources, one delete
az group create --name rg-az900-c14 --location westeurope
az network vnet create --resource-group rg-az900-c14 --name vnet-c14 --address-prefix 10.14.0.0/16
az storage account create --resource-group rg-az900-c14 --name stc14$RANDOM \
  --location northeurope --sku Standard_LRS        # note: RG is West Europe, storage is North Europe → allowed
az resource list --resource-group rg-az900-c14 --query "[].{Name:name, Type:type, Region:location}" --output table

# 3) Delete the group → both resources inside it go too
az group delete --name rg-az900-c14 --yes
az resource list --resource-group rg-az900-c14 --output table 2>/dev/null || echo "Group and its resources deleted"

# 4) Find your place in the hierarchy
az account show --query "{Tenant:tenantId, Subscription:name, Id:id}" --output table
az account management-group list --output table   # returns empty/error if you lack permission — that is information too
```

> **Alıştırma — kendi hiyerarşini çiz.** 3 ekibi (web, veri, platform) ve 3 ortamı (dev, test, prod) olan bir şirket için tenant → management group → subscription → resource group şemasını çiz. Şu iki kuralı en **yüksek uygun kapsama** yerleştir: (a) "Tüm kaynaklar yalnızca West Europe ve North Europe'ta", (b) "Prod kaynaklarını yalnızca platform ekibi değiştirebilir".
>
> <details><summary>Örnek çözüm</summary>
>
> ```
> Tenant
> └─ Root MG                         ← (a) Allowed locations politikası: tüm subscription'lara miras
>    ├─ MG: Prod                     ← (b) Platform ekibine Contributor; diğer ekiplere Reader
>    │  └─ sub-prod
>    │     ├─ rg-web-prod
>    │     └─ rg-veri-prod
>    └─ MG: NonProd                  ← ekiplere kendi RG'lerinde Contributor
>       ├─ sub-dev  → rg-web-dev, rg-veri-dev
>       └─ sub-test → rg-web-test, rg-veri-test
> ```
>
> Gerekçe: Kurallar **miras** aldığı için en geniş geçerli kapsama bir kez atanır; her subscription'a tek tek atamak hem iş yükü hem de unutulma riski demektir. Ortamlara ayrı subscription vermek faturayı ve erişimi doğal olarak ayırır. (Rollerin ayrıntısı → C1.9, Policy → C1.12.)
> </details>

---

### 4. Gerçek dünya senaryosu

Bir Avrupa e-ticaret şirketi Azure'a geçerken mimarisini kuruyor:

- **Region:** Müşterilerin çoğu Almanya ve Hollanda'da; veri AB içinde kalmalı → birincil **West Europe**, eşleşik bölge **North Europe** (aynı coğrafya, sıralı güncellemeler).
- **Availability zones:** Web katmanı West Europe'ta **üç zone'a** yayılmış VM'lerde; veritabanı **zone-redundant** yapılandırmada. Bir datacenter'ın elektriği kesildiğinde site ayakta kalıyor.
- **Region pair:** Ürün görselleri **GRS** storage'da; West Europe'un tamamı etkilenirse kopya North Europe'ta. Kritik sipariş servisinin ikinci kopyası North Europe'ta hazır bekliyor.
- **Hiyerarşi:** Root management group'a "yalnızca AB region'ları" politikası atanıyor. Prod ve NonProd ayrı management group'larda; her ortamın ayrı subscription'ı var, böylece finans ekibi maliyeti ortam bazında görüyor.
- **Resource group'lar:** Her uygulama ve ortam ayrı bir RG'de (`rg-siparis-prod`, `rg-siparis-test`). Test ortamı her akşam `az group delete` ile silinip sabah şablondan yeniden kuruluyor (→ C1.13); prod RG'lerinde silme kilidi var (→ C1.12).

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo + "hangisini kullanmalısın?", evet/hayır tabloları (özellikle resource group kuralları) ve hiyerarşi/kapsam soruları.

| Soru kalıbı | Cevap | Tuzak |
|---|---|---|
| "Bir **datacenter** arızasına karşı koru, aynı region'da kal" | **Availability zone** | Region pair |
| "Bütün bir **region**'ın felaketine karşı koru" | **Region pair** / ikinci region | Availability zone |
| "Planlı güncellemeler iki bölgeye aynı anda uygulanmasın" | **Region pair** | |
| "ABD kamu kurumu, ayrı ve uyumlu bulut" | **Azure Government** (sovereign) | |
| "Bir politikayı **birden çok subscription**'a tek seferde uygula" | **Management group** | Resource group |
| "Kaynakları yaşam döngüsüne göre gruplayıp birlikte sil" | **Resource group** | Subscription |
| "Departman başına **ayrı fatura**" | **Subscription** | Resource group (maliyet tag'le de ayrılır → C1.11) |
| "Bir kaynak iki resource group'a ait olabilir mi?" | **Hayır** | |
| "Resource group'lar iç içe olabilir mi?" | **Hayır** | |
| "RG'deki kaynaklar farklı region'larda olabilir mi?" | **Evet** | |
| "RG silinirse içindeki kaynaklar?" | **Hepsi silinir** | "Kaynaklar başka RG'ye taşınır" |
| "Hiyerarşiyi sırala" | Management group → subscription → resource group → resource | |
| "Region seçmeden kullanılan servis" | Global servisler (Entra ID, Front Door, DNS) | |

**Anahtar kelimeler:** *physically separate locations within a region* (zone) · *at least three zones* · *300 miles* (pair) · *sequential updates* · *logical container* (RG) · *billing boundary / access control boundary* (subscription) · *inherit* (management group).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "Availability zone = ayrı region" | Zone'lar **aynı region içinde** fiziksel olarak ayrı datacenter grupları |
| "Her region'ın üç zone'u var" | Zone **destekleyen** region'larda en az üç; her region zone desteklemez |
| "Her region'ın bir eşi var" | Bazı yeni region'ların eşi yok; bazı eşleşmeler tek yönlü |
| "Kaynak, RG'nin region'ında olmak zorunda" | RG'nin konumu yalnızca meta veri içindir; kaynaklar farklı region'larda olabilir |
| "RG'yi silince kaynaklar boşa çıkar" | Hepsi **silinir**; prod RG'lerine lock koy (→ C1.12) |
| "Tag'ler de RBAC gibi miras alınır" | RBAC/Policy/lock miras alınır; **tag alınmaz** (→ C1.11) |
| "Management group = büyük resource group" | Management group **subscription**'ları gruplar; kaynak içermez |
| "Policy'yi her subscription'a tek tek atarım" | En üst uygun management group'a bir kez ata; miras |
| "Subscription = hesap" | Bir hesabın birden çok subscription'ı olabilir; subscription bir tenant'a bağlıdır |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Tek zone (zonal) yerleşim | En düşük gecikme ve maliyet | Datacenter arızasında kesinti |
| Zone'lara yayılmak | Datacenter arızasına dayanıklılık, daha yüksek SLA | Ek kaynak, zone'lar arası trafik ücreti ve küçük gecikme |
| İkinci region (pair) | Bölgesel felakete dayanıklılık | ~2× maliyet, veri replikasyonu, tutarlılık ve failover karmaşıklığı |
| Kullanıcıya yakın region | Düşük gecikme | O region'da servis/boyut olmayabilir; fiyat farklı olabilir |
| Çok sayıda küçük RG | Net yaşam döngüsü ve erişim sınırları | Yönetilecek daha çok kapsam |
| Ortam başına subscription | Faturalama ve erişim izolasyonu, kota ayrımı | Ağ bağlantısı ve yönetişim için management group/hub tasarımı gerekir |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe the core architectural components of Azure](https://learn.microsoft.com/en-us/training/modules/describe-core-architectural-components-of-azure/)
- [Azure regions list](https://learn.microsoft.com/en-us/azure/reliability/regions-list) · [What are availability zones?](https://learn.microsoft.com/en-us/azure/reliability/availability-zones-overview) · [Azure region pairs and nonpaired regions](https://learn.microsoft.com/en-us/azure/reliability/regions-paired)
- [Azure Government overview](https://learn.microsoft.com/en-us/azure/azure-government/documentation-government-welcome) · [Azure operated by 21Vianet](https://learn.microsoft.com/en-us/azure/china/overview-operations)
- [Manage resource groups](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/manage-resource-groups-cli) · [Move resources](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/move-resource-group-and-subscription)
- [What are Azure management groups?](https://learn.microsoft.com/en-us/azure/governance/management-groups/overview)
- [`az account list-locations`](https://learn.microsoft.com/en-us/cli/azure/account#az-account-list-locations)


### 📝 Bölüm 2.1 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir şirket, uygulamasının **aynı region içindeki** tek bir datacenter'ın elektrik kesintisinden etkilenmemesini istiyor. Ne kullanmalıdır?

- A) Region pair
- B) Availability zone
- C) Sovereign region
- D) Management group

<details><summary>Cevap</summary>

**B.** Availability zone'lar bir region içinde bağımsız güç, soğutma ve ağa sahip fiziksel olarak ayrı datacenter gruplarıdır; kaynakları zone'lara yaymak datacenter arızasına karşı korur. Region pair bölgesel felaket içindir.
</details>

#### Soru 2: Tek seçim
Bir şirket, **bütün bir Azure region'ını** etkileyen bir felakette bile verilerinin başka bir bölgede kopyasının bulunmasını istiyor ve verinin aynı coğrafya içinde kalması gerekiyor. Hangi kavram bunu destekler?

- A) Availability zone
- B) Region pair
- C) Resource group
- D) Availability set

<details><summary>Cevap</summary>

**B.** Region pair'ler aynı coğrafyada, birbirinden uzak iki bölgedir; GRS gibi servisler ikincil kopyayı eşleşik bölgede tutar ve veri coğrafya içinde kalır.
</details>

#### Soru 3: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Availability zone destekleyen bir region'da en az üç zone bulunur. | ☐ | ☐ |
| b) Her Azure region'ı availability zone destekler. | ☐ | ☐ |
| c) Availability zone'ların her birinin bağımsız gücü, soğutması ve ağı vardır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Zone'lar yalnızca zone destekleyen region'larda vardır; orada en az üç tanedir ve her biri bağımsız altyapıya sahiptir.
</details>

#### Soru 4: Çoklu seçim (iki doğru cevap)
Region pair'lerin **hangi ikisi** faydasıdır?

- A) Planlı Azure güncellemeleri eşleşik bölgelere sırayla uygulanır
- B) Eşleşik bölgeler arasındaki tüm trafik ücretsizdir
- C) Geniş çaplı bir kesintide eşleşik bölgelerden birinin kurtarılmasına öncelik verilir
- D) Eşleşik bölgeler aynı datacenter'ı paylaşır

<details><summary>Cevap</summary>

**A ve C.** Sıralı güncelleme ve kurtarma önceliği region pair'in temel faydalarıdır. Bölgeler arası trafik ücretlidir (B); eşleşik bölgeler mümkün olduğunca ~300 mil uzaktadır, datacenter paylaşmaz (D).
</details>

#### Soru 5: Tek seçim
ABD'deki bir kamu kurumu, fiziksel ve ağ olarak ana Azure'dan ayrılmış, yalnızca taranmış ABD personelinin işlettiği ve kamu uyumluluk gereksinimlerini karşılayan bir bulut istiyor. Hangisini kullanmalıdır?

- A) Azure Government
- B) Azure China (21Vianet)
- C) Bir availability zone
- D) Ayrı bir subscription

<details><summary>Cevap</summary>

**A.** Azure Government, ABD kamu kurumları için bir **sovereign region**'dır. Ayrı bir subscription (D) yalnızca mantıksal ayrımdır; fiziksel ve yasal ayrım sağlamaz.
</details>

#### Soru 6: Tek seçim
Aşağıdaki servislerden hangisi **global** bir servistir, yani belirli bir region seçilmeden kullanılır?

- A) Azure Virtual Machines
- B) Azure SQL Database
- C) Microsoft Entra ID
- D) Azure Storage account

<details><summary>Cevap</summary>

**C.** Entra ID (ve Front Door, Traffic Manager, Azure DNS) global servislerdir. VM, SQL Database ve storage account bir region'da oluşturulur.
</details>

#### Soru 7: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Bir kaynak aynı anda iki resource group'a ait olabilir. | ☐ | ☐ |
| b) Bir resource group başka bir resource group'un içine yerleştirilebilir. | ☐ | ☐ |
| c) Bir resource group'taki kaynaklar farklı region'larda olabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Hayır · c) Evet.** Her kaynak tam olarak bir gruptadır; gruplar iç içe olmaz; RG'nin konumu yalnızca meta veri içindir, kaynaklar farklı region'larda olabilir.
</details>

#### Soru 8: Tek seçim
Bir yönetici, içinde bir VM, bir VNet ve bir storage account bulunan `rg-test` resource group'unu siliyor. Ne olur?

- A) Yalnızca resource group silinir, kaynaklar varsayılan gruba taşınır
- B) Resource group ve içindeki tüm kaynaklar silinir
- C) İçinde kaynak olan bir resource group silinemez
- D) Yalnızca VM silinir

<details><summary>Cevap</summary>

**B.** Bir resource group silindiğinde içindeki tüm kaynaklar da silinir. Bu yüzden önemli gruplara resource lock konur (→ C1.12).
</details>

#### Soru 9: Tek seçim
Bir şirketin 30 subscription'ı var. Güvenlik ekibi, **tüm subscription'larda** yalnızca belirli region'larda kaynak oluşturulmasına izin veren bir politikayı **tek bir atamayla** uygulamak istiyor. Politika nereye atanmalıdır?

- A) Her resource group'a
- B) Bir management group'a
- C) Her kaynağa
- D) Bir availability zone'a

<details><summary>Cevap</summary>

**B.** Management group'a atanan Policy ve RBAC, altındaki tüm subscription'lara ve onların kaynaklarına miras kalır.
</details>

#### Soru 10: Sıralama
Azure yönetim hiyerarşisini **en geniş kapsamdan en dara** doğru sıralayın: Resource group · Resource · Management group · Subscription

<details><summary>Cevap</summary>

**Management group → Subscription → Resource group → Resource.** (En üstte Entra tenant ve onun root management group'u bulunur.)
</details>

#### Soru 11: Tek seçim
Bir şirket, departmanlarının Azure maliyetlerini **ayrı faturalarla** takip etmek ve her departmanın erişimini birbirinden ayırmak istiyor. En uygun yapı hangisidir?

- A) Her departman için ayrı bir availability zone
- B) Her departman için ayrı bir subscription
- C) Tüm kaynakları tek bir resource group'ta toplamak
- D) Her departman için ayrı bir region

<details><summary>Cevap</summary>

**B.** Subscription hem **billing boundary** hem **access control boundary**'dir. (Maliyeti tek subscription içinde de tag'lerle raporlamak mümkündür → C1.11, ama ayrı fatura ve erişim sınırı subscription ile sağlanır.)
</details>

#### Soru 12: Çoklu seçim (iki doğru cevap)
Bir kuruluşun **birden çok subscription** kullanmasının **hangi ikisi** geçerli bir nedendir?

- A) Dev, test ve prod ortamlarını faturalama ve erişim açısından ayırmak
- B) Bir resource group'u başka bir resource group'un içine koyabilmek
- C) Subscription başına kaynak limitlerine (kota) takılmak
- D) Availability zone'ları etkinleştirmek

<details><summary>Cevap</summary>

**A ve C.** Ortam/departman ayrımı, faturalama ve kota/limitler birden çok subscription'ın klasik nedenleridir. RG'ler iç içe olmaz (B); zone kullanımı subscription sayısından bağımsızdır (D).
</details>

#### Soru 13: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Her Entra tenant'ında tek bir root management group bulunur. | ☐ | ☐ |
| b) Bir subscription aynı anda iki management group'un altında olabilir. | ☐ | ☐ |
| c) Management group hiyerarşisi (root ve subscription seviyesi hariç) 6 seviyeye kadar derinleşebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Her management group ve subscription tek bir ebeveyne bağlıdır; derinlik root ve subscription hariç 6 seviyedir.
</details>

#### Soru 14: Tek seçim
Bir resource group üzerinde bir kullanıcıya **Reader** rolü atanıyor. Bu atama nereyi etkiler?

- A) Yalnızca resource group nesnesinin kendisini
- B) Resource group'u ve içindeki tüm kaynakları
- C) Subscription'daki tüm resource group'ları
- D) Hiçbir kaynağı; rol yalnızca kaynak düzeyinde atanabilir

<details><summary>Cevap</summary>

**B.** RBAC atamaları kapsamdan **aşağı** miras alınır: resource group'a verilen rol içindeki tüm kaynaklarda geçerlidir, ama üstteki subscription'a yayılmaz (→ C1.9).
</details>

#### Soru 15: Senaryo
Bir şirket "datacenter seçerek" kaynak oluşturmak istiyor. Bu Azure'da nasıl karşılanır?

- A) Kaynak oluştururken datacenter adını seçerek
- B) Müşteri datacenter seçemez; region ve (destekleniyorsa) availability zone seçer
- C) Yalnızca Azure Government'ta datacenter seçilebilir
- D) Management group ile datacenter atanır

<details><summary>Cevap</summary>

**B.** Datacenter'lar müşteriye doğrudan açılmaz; region'lar ve availability zone'lar halinde gruplanır.
</details>

#### Soru 16: Eşleştirme
Her ihtiyacı doğru kavramla eşleştirin: **Availability zone · Region pair · Resource group · Management group · Subscription**

1. Bir uygulamanın test ortamındaki tüm kaynakları tek komutla silmek
2. Şirket genelindeki 25 subscription'a aynı RBAC atamasını uygulamak
3. Aynı region içinde datacenter arızasına dayanmak
4. Pazarlama departmanı için ayrı fatura
5. Bölgesel felakette ikinci bölgeden devam etmek

<details><summary>Cevap</summary>

**1 → Resource group · 2 → Management group · 3 → Availability zone · 4 → Subscription · 5 → Region pair.**
</details>

#### Soru 17: Tek seçim
**Zone-redundant** bir servis ile **zonal** bir servis arasındaki fark nedir?

- A) Zone-redundant servisi müşteri tek bir zone'a sabitler; zonal servisi platform otomatik çoğaltır
- B) Zonal servisi müşteri belirli bir zone'a sabitler; zone-redundant servisi platform zone'lar arasında otomatik çoğaltır
- C) İkisi aynı şeydir
- D) Zonal servisler yalnızca sovereign region'larda bulunur

<details><summary>Cevap</summary>

**B.** Zonal: VM, disk, public IP gibi kaynakları sen bir zone'a yerleştirirsin (dayanıklılık için birden çok zone'a ayrı kopyalar koyarsın). Zone-redundant: ZRS storage veya zone-redundant SQL Database gibi servislerde platform kopyaları zone'lara kendisi dağıtır.
</details>

#### Soru 18: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Her Azure region'ının bir eşleşik bölgesi vardır. | ☐ | ☐ |
| b) Bir resource group'taki kaynak başka bir resource group'a taşınabilir. | ☐ | ☐ |
| c) Resource group'a verilen tag'ler içindeki kaynaklara otomatik olarak miras kalır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** Bazı yeni region'ların eşi yoktur. Kaynaklar RG ve subscription'lar arasında taşınabilir (bazı türlerde kısıtlarla). Tag'ler varsayılan olarak miras alınmaz; bunun için Azure Policy kullanılır (→ C1.11, C1.12).
</details>

#### Soru 19: Çoklu seçim (iki doğru cevap)
Bir şirket yeni uygulaması için region seçiyor. **Hangi ikisi** region seçiminde dikkate alınması gereken kriterdir?

- A) Kullanıcılara olan ağ gecikmesi
- B) Subscription'ın adı
- C) Verinin belirli bir ülkede kalmasını gerektiren regülasyonlar
- D) Resource group sayısı

<details><summary>Cevap</summary>

**A ve C.** Gecikme ve veri ikametgâhı/regülasyon temel kriterlerdir; servis uygunluğu, fiyat ve zone/pair durumu da dikkate alınır. Subscription adı ve RG sayısı region seçimini etkilemez.
</details>

#### Soru 20: Senaryo
Bir şirketin kökte "Allowed locations" politikası atanmış bir management group'u, onun altında `MG-Prod`, onun altında `sub-prod` subscription'ı ve içinde `rg-api` resource group'u var. `rg-api`'de izin verilmeyen bir region'da VM oluşturmaya çalışan bir kullanıcının subscription'da **Owner** rolü var. Ne olur?

- A) Owner olduğu için VM oluşturulur
- B) Politika üst kapsamdan miras alındığı için istek reddedilir
- C) Politika yalnızca management group'un kendisini etkiler
- D) VM oluşturulur ama otomatik olarak izinli region'a taşınır

<details><summary>Cevap</summary>

**B.** Management group'a atanan Policy, tüm alt subscription, RG ve kaynaklara miras kalır. Policy **kaynağın** nasıl olabileceğini belirler; RBAC'taki Owner rolü onu aşmaz (→ C1.12).
</details>

---

**Puanlama:** 18-20 → hazırsın · 14-17 → README 3.3 (zone vs pair) ve 3.5 (RG kuralları) tablolarını tekrar et · < 14 → konuyu baştan çalış ve 3.7 laboratuvarını yap.


---

## 2.2 Azure Compute ve Virtual Machines

> **Sınav Alanı:** Describe Azure architecture and services (%35-40) · **Konu Kodu:** C2.2

### 1. Ne?

**Azure compute**, uygulamaları çalıştırmak için isteğe bağlı işlem gücü (CPU, bellek, disk, ağ) sunan servislerin genel adıdır. Seçenekler, yönetim yükü ile kontrol arasındaki yelpazede sıralanır (→ C1.3):

| Seçenek | Hizmet türü | Ne zaman? | Konu |
|---|---|---|---|
| **Virtual Machines** | IaaS | İşletim sistemi üzerinde tam kontrol | Bu konu |
| **VM Scale Sets** | IaaS | Aynı VM'den çok sayıda, otomatik ölçekle | Bu konu |
| **Container'lar** (ACI, Container Apps, AKS) | PaaS / serverless | Hafif, taşınabilir paketler; mikroservis | C1.6 |
| **App Service** | PaaS | OS yönetmeden web uygulaması/API | C1.6 |
| **Azure Functions** | Serverless | Olay tetiklemeli kısa kod | C1.6 |
| **Azure Virtual Desktop** | Masaüstü sanallaştırma | Buluttan Windows masaüstü | C1.6 |

Bu konu VM tarafını derinlemesine işler: **VM'in kendisi ve gerektirdiği kaynaklar**, **VM Scale Sets**, **availability sets** ve bağlam olarak **proximity placement groups**. Konunun kodu yok; pratik, README'deki `az` CLI laboratuvarıdır (3.6).

---

### 2. Neden?

- **Sınav:** "VM için hangi kaynak gerekir?", "availability set mi, scale set mi, zone mu?" ve "VM ücretini nasıl durdururum?" bu alanın standart sorularıdır.
- **Gerçek işte:** Taşınan iş yüklerinin büyük kısmı önce VM olarak başlar (lift-and-shift). VM'in tek başına bir kaynak olmadığını (disk, NIC, IP, NSG, VNet) bilmek; maliyeti (silinmeyen disk ve IP'ler), güvenliği (internete açık port) ve dayanıklılığı (tek VM = tek arıza noktası) doğrudan etkiler.

---

### 3. Nasıl?

#### 3.1 Azure Virtual Machines

**Azure VM**, fiziksel donanımı satın almadan kiralanan **sanallaştırılmış bir sunucudur**. IaaS olduğu için işletim sisteminden yukarısı (yama, yazılım, yapılandırma, yedek) senin sorumluluğundadır (→ C1.1).

Ne zaman VM?

- İşletim sistemi üzerinde **tam kontrol** veya özel yazılım/sürücü/yapılandırma gerektiğinde
- Mevcut sunucuların **lift-and-shift** ile değiştirilmeden taşınmasında
- Hızla açılıp kapatılan **test/geliştirme** ortamlarında
- Uygulama barındırma ve **datacenter'ı buluta genişletme** (ağ → C1.7)

#### 3.2 VM için gereken kaynaklar ⭐

Bir VM, birlikte çalışan birkaç Azure kaynağından oluşur. `az vm create` (veya portal sihirbazı) eksik olanları otomatik oluşturur:

| Kaynak | Ne işe yarar | Seçenekler / not |
|---|---|---|
| **Boyut** (size, SKU) | vCPU, bellek, geçici disk, ağ bant genişliği | Aileler: genel amaçlı (B, D), compute optimized (F), memory optimized (E, M), storage optimized (L), GPU (N), HPC (H); boyut sonradan değiştirilebilir (genellikle yeniden başlatma ile) |
| **Image** | VM'in kurulacağı işletim sistemi ve (varsa) hazır yazılım | Windows Server, Ubuntu, RHEL…; **Azure Marketplace** imajları; kendi özel imajın |
| **Disk** | OS diski + isteğe bağlı **veri diskleri** | **Managed disk** (Azure yönetir); Standard HDD · Standard SSD · Premium SSD (· Premium SSD v2, Ultra Disk); disk performansı SLA'yı da etkiler (→ C1.2) |
| **Virtual network + subnet** | VM'in bağlandığı özel ağ | VNet bir region'a bağlıdır (→ C1.7) |
| **Network interface (NIC)** | VM'i subnet'e bağlayan sanal ağ kartı; özel IP taşır | Bir VM'de birden çok NIC olabilir |
| **Public IP** *(isteğe bağlı)* | İnternetten erişim | Prod'da genellikle yoktur; erişim VPN/Bastion/load balancer üzerinden |
| **Network security group (NSG)** | Gelen/giden trafik için izin/engelleme kuralları | Varsayılan olarak SSH (22) / RDP (3389) açılabilir — internete açık bırakma |

> Sınav sorusu kalıbı: "Bir VM oluşturmak için aşağıdakilerden hangisi **gerekli değildir**?" — public IP (isteğe bağlı), load balancer, App Service plan, storage account gibi şıklar gelir. Zorunlu çekirdek: **boyut, image, OS diski, VNet/subnet ve NIC**.

**Fiyatlandırma:**

| VM durumu | Compute ücreti | Disk ücreti |
|---|---|---|
| Çalışıyor (Running) | Var (saniye/saat bazında) | Var |
| **Stopped** (işletim sisteminden kapatılmış veya `az vm stop`) | **Var** — donanım hâlâ ayrılmış | Var |
| **Stopped (deallocated)** (`az vm deallocate`, portalda *Stop*) | **Yok** — donanım serbest bırakıldı | **Var** (ve statik public IP de ücretlendirilebilir) |
| Silindi | Yok | Disk/IP **ayrıca** silinmediyse ücret sürer |

Maliyeti düşüren diğer seçenekler: reservation/savings plan, spot VM (→ C1.1), Azure Hybrid Benefit, otomatik kapatma, doğru boyutlandırma (→ C1.11, C1.14 Advisor).

#### 3.3 VM Scale Sets (VMSS)

**VM Scale Set**, **aynı yapılandırmadaki** VM'lerden oluşan ve **tek bir kaynak gibi** yönetilen gruptur.

- **Otomatik ölçekleme:** VM sayısı metriğe (CPU, kuyruk uzunluğu) veya zamana göre **otomatik** artar/azalır (scale out/in) — elasticity'nin (→ C1.2) VM'deki aracı.
- **Yük dengeleme:** Önüne bir **load balancer** veya Application Gateway konarak trafik instance'lara dağıtılır.
- **Dayanıklılık:** Instance'lar **availability zone'lara** ve fault domain'lere yayılabilir.
- **Ölçek:** Yüzlerce VM'i (orkestrasyon moduna göre 1.000'e kadar) tek tanımla yönetmek; güncellemeleri kademeli uygulamak.
- **Ücret:** Scale set'in kendisi ek ücretli değildir; çalışan VM'ler ve kullanılan kaynaklar ödenir.
- *(bağlam)* İki orkestrasyon modu vardır: **Uniform** (tamamen aynı VM'ler) ve **Flexible** (farklı VM boyutlarını, spot ve normal VM'leri birlikte barındırabilir). Microsoft yeni yüksek erişilebilirlik tasarımları için Flexible modu öneriyor.

Kullanım: değişken trafikli web katmanı, büyük hesaplama/toplu işler, container host'ları.

#### 3.4 Availability sets

**Availability set**, VM'leri **tek bir datacenter içinde** farklı donanım ve bakım gruplarına yayarak, planlı bakımda veya donanım arızasında tüm VM'lerin aynı anda kapanmasını önler. İki kavram:

| Kavram | Anlamı | Değer |
|---|---|---|
| **Fault domain (FD)** | Ortak güç kaynağını ve ağ anahtarını paylaşan **raf** grubu; bir FD'deki donanım arızası yalnızca o FD'deki VM'leri etkiler | En çok **3** (region'a göre 2 veya 3) |
| **Update domain (UD)** | Planlı bakımda **aynı anda yeniden başlatılabilecek** VM grubu; Azure UD'leri **sırayla** günceller | Varsayılan **5**, en çok **20** |

- Ek ücret yoktur; yalnızca VM'ler ödenir.
- VM availability set'e **oluşturulurken** yerleştirilir (mevcut bir VM sonradan eklenemez).
- 2+ VM bir availability set'te → **%99,95** SLA (→ C1.2).
- Microsoft, yeni dağıtımlarda daha yüksek dayanıklılık için **availability zone**'ları veya **Flexible** orkestrasyonlu scale set'leri öneriyor; availability set'ler hâlâ desteklenir ve sınav kapsamındadır.

**Availability set ↔ availability zone ↔ scale set:**

| | Availability set | Availability zone | VM Scale Set |
|---|---|---|---|
| Neye karşı korur | Datacenter **içindeki** donanım (raf) arızası ve planlı bakım | **Datacenter'ın** (zone'un) tamamen çökmesi | Kendisi koruma değil **ölçek** aracıdır; zone/FD'lere yayılarak korur |
| Tipik SLA (2+ VM) | %99,95 | %99,99 | Yerleşime bağlı (zone'lara yayılırsa %99,99) |
| Otomatik ölçek | Yok | Yok | **Var** |
| Ana fikir | "Aynı rafta olmasınlar" | "Aynı binada olmasınlar" | "Yüke göre çoğalsınlar" |

#### 3.5 Proximity placement groups *(bağlam — kurslarda işlenir)*

**Proximity placement group (PPG)**, VM'leri (ve scale set/availability set'leri) **fiziksel olarak birbirine yakın** — aynı datacenter içinde — yerleştirerek aralarındaki **ağ gecikmesini en aza** indiren mantıksal gruplamadır.

- Kullanım: katmanlar arası çok düşük gecikme isteyen iş yükleri (oyun sunucuları, finansal işlem sistemleri, HPC, SAP gibi çok katmanlı uygulamalar).
- Trade-off: **yakınlık ↔ dayanıklılık.** VM'leri aynı yere toplamak, zone'lara yaymanın tam tersidir; bir datacenter arızası hepsini etkileyebilir. Ayrıca belirli bir datacenter'da istenen VM boyutu bulunmayabilir (kapasite riski).
- Availability set ve scale set ile birlikte kullanılabilir: "aynı datacenter'da, ama farklı raflarda".

#### 3.6 Pratik — VM, deallocate, scale set ve availability set

> ⚠️ Bu laboratuvar **ücretlidir** (küçük `Standard_B1s` VM'ler, birkaç saatlik kullanım genellikle düşük maliyetlidir). Önce bütçe alarmını kur (→ C1.1 / 3.6), laboratuvarı tek oturumda bitir ve **en sondaki `az group delete`'i atlama**.

```bash
az login
az group create --name rg-az900-c15 --location westeurope
az vm image list --output table                     # available image aliases (Ubuntu2404 etc.)

# 1) Single VM — see which resources get created together
az vm create --resource-group rg-az900-c15 --name vm-c15 \
  --image Ubuntu2404 --size Standard_B1s \
  --admin-username azureuser --generate-ssh-keys
az resource list --resource-group rg-az900-c15 \
  --query "[].{Name:name, Type:type}" --output table
# Expected: virtualMachines, disks (OS disk), networkInterfaces, networkSecurityGroups,
#           publicIPAddresses, virtualNetworks

# 2) Difference between stop and deallocate
az vm stop --resource-group rg-az900-c15 --name vm-c15         # "VM stopped" → compute billing CONTINUES
az vm show --resource-group rg-az900-c15 --name vm-c15 --show-details --query powerState --output tsv
az vm deallocate --resource-group rg-az900-c15 --name vm-c15   # "VM deallocated" → compute billing STOPS
az vm show --resource-group rg-az900-c15 --name vm-c15 --show-details --query powerState --output tsv
# The disk still exists and is billed:
az disk list --resource-group rg-az900-c15 --query "[].{Disk:name, GB:diskSizeGb, Sku:sku.name}" --output table

# 3) VM Scale Set with two instances + a simple autoscale rule
az vmss create --resource-group rg-az900-c15 --name vmss-c15 \
  --image Ubuntu2404 --vm-sku Standard_B1s --instance-count 2 \
  --admin-username azureuser --generate-ssh-keys
az monitor autoscale create --resource-group rg-az900-c15 --name autoscale-c15 \
  --resource vmss-c15 --resource-type Microsoft.Compute/virtualMachineScaleSets \
  --min-count 2 --max-count 4 --count 2
az monitor autoscale rule create --resource-group rg-az900-c15 --autoscale-name autoscale-c15 \
  --condition "Percentage CPU > 70 avg 5m" --scale out 1
az monitor autoscale rule create --resource-group rg-az900-c15 --autoscale-name autoscale-c15 \
  --condition "Percentage CPU < 30 avg 5m" --scale in 1
az vmss list-instances --resource-group rg-az900-c15 --name vmss-c15 --output table

# 4) Availability set: fault / update domain values
az vm availability-set create --resource-group rg-az900-c15 --name avset-c15 \
  --platform-fault-domain-count 2 --platform-update-domain-count 5
az vm create --resource-group rg-az900-c15 --name vm-avset-1 \
  --image Ubuntu2404 --size Standard_B1s --availability-set avset-c15 \
  --admin-username azureuser --generate-ssh-keys --public-ip-address ""
az vm get-instance-view --resource-group rg-az900-c15 --name vm-avset-1 \
  --query "{FaultDomain:instanceView.platformFaultDomain, UpdateDomain:instanceView.platformUpdateDomain}" --output table

# 5) Cleanup — mandatory
az group delete --name rg-az900-c15 --yes --no-wait
```

> **Alıştırma.** Aşağıdaki gereksinimler için doğru seçeneği yaz (VM / availability set / availability zone / scale set / proximity placement group / deallocate):
> 1. Gece kullanılmayan test VM'leri için compute ücreti ödenmesin.
> 2. İki VM aynı datacenter'da kalsın ama aynı raf arızasından etkilenmesin.
> 3. Web katmanı yük artınca kendiliğinden yeni VM eklesin.
> 4. Bir datacenter tamamen çökse bile uygulama ayakta kalsın.
> 5. İki katman arasındaki ağ gecikmesi mümkün olan en düşük düzeyde olsun.
>
> <details><summary>Çözüm</summary>
>
> 1. **Deallocate** (ör. otomatik kapatma ile) — `stop` yetmez, donanım ayrılmış kalır.
> 2. **Availability set** (farklı fault domain'ler).
> 3. **VM Scale Set** + autoscale kuralı.
> 4. **Availability zone** (VM'leri veya scale set'i zone'lara yaymak).
> 5. **Proximity placement group** — bedeli: dayanıklılığın azalması.
> </details>

---

### 4. Gerçek dünya senaryosu

Bir sigorta şirketi poliçe uygulamasını kendi datacenter'ından Azure'a taşıyor:

- **Lift-and-shift:** Eski poliçe motoru özel bir Windows servisine bağımlı, kod değiştirilemiyor → **Azure VM**. Doğru boyut için mevcut sunucunun CPU/RAM kullanımı ölçülüyor (Azure Migrate → C1.8); genel amaçlı D serisi seçiliyor, veritabanı diski Premium SSD.
- **Dayanıklılık:** Poliçe motorunun iki VM'i aynı region'da **iki farklı availability zone'a** konuyor; ilk aşamada zone desteği olmayan bir yan sistem için iki VM bir **availability set**'te.
- **Ölçek:** Müşteri portalının web katmanı **VM Scale Set**'te; ay sonu yoğunluğunda CPU kuralıyla 2'den 8 instance'a çıkıyor, sonra kendiliğinden 2'ye iniyor.
- **Maliyet:** Test ortamının VM'leri her akşam 20:00'de **deallocate** ediliyor; ilk ay faturasında, silinen VM'lerden kalan **sahipsiz diskler ve public IP'ler** fark edilip temizleniyor.
- **Güvenlik:** Prod VM'lerinin public IP'si yok; NSG'de RDP/SSH internete kapalı, yönetim erişimi özel ağ üzerinden (→ C1.7, C1.10).

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → doğru VM seçeneği; "VM için hangi kaynak gerekli/gereksiz?"; durdurma ve ücretlendirme; availability set kavramları (fault/update domain) için evet/hayır tabloları.

| Soru kalıbı | Cevap | Tuzak |
|---|---|---|
| "Planlı bakımda tüm VM'ler aynı anda yeniden başlamasın" | **Availability set** (update domain) | Scale set |
| "Raf/donanım arızası tüm VM'leri etkilemesin, aynı datacenter" | **Availability set** (fault domain) | Availability zone |
| "Datacenter arızasına karşı koru" | **Availability zone** | Availability set |
| "Talebe göre VM sayısı **otomatik** artsın" | **VM Scale Set** | Availability set |
| "Aynı yapılandırmadaki yüzlerce VM'i tek kaynak gibi yönet" | **VM Scale Set** | |
| "VM'ler arası en düşük gecikme" | **Proximity placement group** | Availability zone |
| "Kullanılmayan VM için compute ücreti ödememek" | **Deallocate** (portalda Stop) | İşletim sisteminden kapatmak |
| "OS üzerinde tam kontrol" | **VM** (IaaS) | App Service |
| "VM oluşturmak için gerekmeyen kaynak" | Public IP, load balancer, App Service plan… | NIC, disk, VNet (bunlar gerekli) |

**Anahtar kelimeler:** *fault domain* (güç/ağ/raf), *update domain* (planlı bakım, sırayla), *identical, load-balanced VMs* (scale set), *automatically increase or decrease* (scale set), *deallocated* (ücret yok), *co-locate / low latency* (PPG).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "VM'i işletim sisteminden kapattım, ücret durdu" | Durum *Stopped* olur, donanım ayrılmış kalır; ücret için **deallocate** gerekir |
| "VM'i silince her şey gitti" | Disk, public IP, NIC, NSG ayrı kaynaklardır; silinmeyenler kalır ve bazıları ücretlenir — RG'yi silmek en temiz yol |
| "Availability set datacenter arızasına karşı korur" | Set tek datacenter içindedir; datacenter arızası için **zone** |
| "Scale set otomatik HA demektir" | Scale set **ölçek** aracıdır; dayanıklılık için zone/FD'lere yayılmalıdır |
| "Mevcut VM'i availability set'e eklerim" | Set'e oluşturma anında yerleştirilir |
| "Public IP her VM için zorunlu" | İsteğe bağlıdır; prod'da genellikle verilmez |
| "Proximity placement group dayanıklılığı artırır" | Gecikmeyi azaltır; dayanıklılığı **azaltabilir** |
| NSG'de 22/3389'u internete açık bırakmak | Kaynak IP'yi kısıtla, Bastion/VPN kullan (→ C1.7, C1.10) |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Tek VM | Basit, ucuz | Tek arıza noktası, SLA %99,9 (Premium SSD ile) |
| Availability set | Raf ve bakım arızasına dayanıklılık, ek ücret yok | Datacenter arızasına karşı koruma yok |
| Availability zone'lar | Datacenter arızasına dayanıklılık, %99,99 | Zone'lar arası gecikme ve trafik ücreti; her region desteklemez |
| VM Scale Set + autoscale | Talebe göre maliyet, insan müdahalesi yok | Stateless uygulama tasarımı, load balancer, ölçek kurallarının ayarı |
| Proximity placement group | En düşük ağ gecikmesi | Dayanıklılık ve kapasite riski |
| Büyük VM boyutu (scale up) | Basit performans artışı | Üst sınır, yeniden başlatma, maliyet |
| Premium SSD / Ultra Disk | Yüksek IOPS, düşük gecikme, daha iyi tek VM SLA'sı | Daha yüksek disk maliyeti |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure compute and networking services](https://learn.microsoft.com/en-us/training/modules/describe-azure-compute-networking-services/)
- [Virtual machines in Azure](https://learn.microsoft.com/en-us/azure/virtual-machines/overview) · [VM sizes](https://learn.microsoft.com/en-us/azure/virtual-machines/sizes/overview) · [States and billing status](https://learn.microsoft.com/en-us/azure/virtual-machines/states-billing)
- [What are Virtual Machine Scale Sets?](https://learn.microsoft.com/en-us/azure/virtual-machine-scale-sets/overview) · [Orchestration modes](https://learn.microsoft.com/en-us/azure/virtual-machine-scale-sets/virtual-machine-scale-sets-orchestration-modes)
- [Availability sets overview](https://learn.microsoft.com/en-us/azure/virtual-machines/availability-set-overview) · [Proximity placement groups](https://learn.microsoft.com/en-us/azure/virtual-machines/co-location)
- [`az vm`](https://learn.microsoft.com/en-us/cli/azure/vm) · [`az vmss`](https://learn.microsoft.com/en-us/cli/azure/vmss) · [`az monitor autoscale`](https://learn.microsoft.com/en-us/cli/azure/monitor/autoscale)


### 📝 Bölüm 2.2 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir şirket, kurum içindeki bir uygulamayı özel bir sürücüye ihtiyaç duyduğu için işletim sistemi üzerinde tam kontrolle Azure'a taşımak istiyor. Hangi compute seçeneği **en uygundur**?

- A) Azure Functions
- B) Azure App Service
- C) Azure Virtual Machines
- D) Azure Container Apps

<details><summary>Cevap</summary>

**C.** OS üzerinde tam kontrol ve özel sürücü = IaaS, Azure VM. Diğerleri OS erişimi vermez.
</details>

#### Soru 2: Çoklu seçim (iki doğru cevap)
Bir Azure VM oluşturulurken **hangi ikisi** VM'in çalışması için **gerekli** kaynaklardır?

- A) Network interface (NIC)
- B) Load balancer
- C) Virtual network / subnet
- D) App Service plan

<details><summary>Cevap</summary>

**A ve C.** VM bir NIC ile bir VNet'teki subnet'e bağlanır (ayrıca boyut, image ve OS diski gerekir). Load balancer isteğe bağlıdır; App Service plan VM ile ilgisizdir.
</details>

#### Soru 3: Tek seçim
Aşağıdakilerden hangisi bir Azure VM için **isteğe bağlıdır**?

- A) OS diski
- B) VM boyutu
- C) Public IP adresi
- D) Image

<details><summary>Cevap</summary>

**C.** Public IP yalnızca VM'e internetten doğrudan erişim gerekiyorsa eklenir; prod'da çoğu zaman verilmez.
</details>

#### Soru 4: Tek seçim
Bir geliştirici, gece kullanılmayan test VM'leri için **compute ücreti ödememek** ama diskleri ve yapılandırmayı korumak istiyor. Ne yapmalıdır?

- A) VM'in içinden işletim sistemini kapatmak
- B) VM'i deallocate etmek
- C) VM'i silmek
- D) VM boyutunu küçültmek

<details><summary>Cevap</summary>

**B.** Deallocate edilen VM'in donanımı serbest bırakılır ve compute ücreti durur; diskler korunur (ve disk ücreti sürer). OS'tan kapatmak VM'i *Stopped* yapar ama donanım ayrılmış kaldığı için ücret sürer. Silmek yapılandırmayı korumaz.
</details>

#### Soru 5: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Deallocate edilmiş bir VM'in managed diskleri için ücret ödenmeye devam edilir. | ☐ | ☐ |
| b) İşletim sisteminden kapatılan (Stopped) bir VM için compute ücreti alınmaz. | ☐ | ☐ |
| c) Bir VM'in boyutu oluşturulduktan sonra değiştirilebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Diskler deallocate sonrası da depolama ücreti üretir. *Stopped* durumunda donanım ayrılmış kaldığından compute ücreti sürer. Boyut değiştirilebilir (genellikle yeniden başlatma ile).
</details>

#### Soru 6: Tek seçim
Bir web uygulamasının trafiği gün içinde çok dalgalanıyor. Şirket, **aynı yapılandırmadaki** VM'lerin sayısının CPU kullanımına göre **otomatik** artıp azalmasını istiyor. Ne kullanmalıdır?

- A) Availability set
- B) Proximity placement group
- C) Virtual Machine Scale Set
- D) Tek, büyük bir VM

<details><summary>Cevap</summary>

**C.** Scale set aynı VM'lerden oluşan grubu tek kaynak gibi yönetir ve autoscale kurallarıyla instance sayısını otomatik değiştirir.
</details>

#### Soru 7: Tek seçim
Bir availability set'te **fault domain** neyi temsil eder?

- A) Planlı bakım sırasında aynı anda yeniden başlatılan VM grubunu
- B) Ortak güç kaynağını ve ağ anahtarını paylaşan donanım (raf) grubunu
- C) Bir region içindeki ayrı bir datacenter'ı
- D) Bir subscription'daki resource group'ları

<details><summary>Cevap</summary>

**B.** Fault domain fiziksel donanım arızasının etki alanıdır. A update domain'in tanımıdır; C availability zone'dur.
</details>

#### Soru 8: Tek seçim
Azure'un planlı bakım sırasında bir uygulamanın tüm VM'lerini **aynı anda** yeniden başlatmasını önleyen availability set kavramı hangisidir?

- A) Fault domain
- B) Update domain
- C) Availability zone
- D) Scale set

<details><summary>Cevap</summary>

**B.** Update domain'ler planlı bakımda **sırayla** güncellenir; VM'ler farklı UD'lerde olduğu için aynı anda yeniden başlamaz.
</details>

#### Soru 9: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Availability set kullanmak için VM ücretine ek bir ücret ödenir. | ☐ | ☐ |
| b) Bir availability set, bir datacenter'ın tamamen çökmesine karşı koruma sağlar. | ☐ | ☐ |
| c) Bir availability set en fazla 3 fault domain içerebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Hayır · c) Evet.** Availability set'in kendisi ücretsizdir. Set tek bir datacenter içindedir; datacenter arızası için availability zone gerekir. Fault domain sayısı en çok 3'tür (update domain varsayılan 5, en çok 20).
</details>

#### Soru 10: Senaryo
Bir şirket, iki VM'li uygulamasının **datacenter düzeyinde** bir arızada da çalışmaya devam etmesini istiyor; uygulama tek bir region'da kalacak. Hangi seçenek **en uygundur**?

- A) VM'leri aynı availability set'e koymak
- B) VM'leri iki farklı availability zone'a dağıtmak
- C) VM'leri bir proximity placement group'a koymak
- D) VM'leri daha büyük bir boyuta yükseltmek

<details><summary>Cevap</summary>

**B.** Availability zone'lar bir region içinde fiziksel olarak ayrı datacenter gruplarıdır. Availability set tek datacenter içinde korur; PPG VM'leri bilerek yakınlaştırır.
</details>

#### Soru 11: Tek seçim
Çok katmanlı bir finansal işlem uygulamasında katmanlar arasındaki **ağ gecikmesinin mümkün olan en düşük** seviyede olması gerekiyor. Hangi özellik bunu sağlar?

- A) Availability zone
- B) Region pair
- C) Proximity placement group
- D) Update domain

<details><summary>Cevap</summary>

**C.** Proximity placement group VM'leri fiziksel olarak birbirine yakın (aynı datacenter içinde) yerleştirir. Bedeli: dayanıklılığın azalması.
</details>

#### Soru 12: Eşleştirme
Her gereksinimi doğru seçenekle eşleştirin: **Availability set · Availability zone · VM Scale Set · Proximity placement group**

1. "Yük artınca yeni VM'ler otomatik eklensin."
2. "Aynı datacenter'da kalsınlar ama aynı raf arızasından etkilenmesinler."
3. "Bir datacenter yansa da uygulama çalışsın."
4. "VM'ler arası gecikme en düşük olsun."

<details><summary>Cevap</summary>

**1 → VM Scale Set · 2 → Availability set · 3 → Availability zone · 4 → Proximity placement group.**
</details>

#### Soru 13: Tek seçim
İki veya daha fazla VM'in aynı **availability set**'e yerleştirildiği bir yapı için Microsoft'un tipik SLA'sı nedir?

- A) %99,9
- B) %99,95
- C) %99,99
- D) SLA yoktur

<details><summary>Cevap</summary>

**B.** Tek VM (Premium SSD) %99,9, availability set'te 2+ VM %99,95, iki+ zone'a yayılmış 2+ VM %99,99 (→ C1.2). Güncel değerler için SLA belgesine bakılmalıdır.
</details>

#### Soru 14: Çoklu seçim (iki doğru cevap)
VM Scale Sets ile ilgili **hangi iki** ifade doğrudur?

- A) Scale set'teki VM'ler aynı yapılandırmayla oluşturulur ve merkezi olarak yönetilir
- B) Scale set kullanmak, VM ücretlerine ek olarak ayrı bir lisans ücreti gerektirir
- C) Scale set instance'ları availability zone'lara yayılabilir
- D) Scale set yalnızca Windows VM'leri destekler

<details><summary>Cevap</summary>

**A ve C.** Scale set'in kendisi ek ücretli değildir (B yanlış); Windows ve Linux'u destekler (D yanlış).
</details>

#### Soru 15: Tek seçim
Bir yönetici bir VM'i siliyor ama ay sonunda faturada hâlâ o VM'le ilgili disk ve public IP ücretleri görüyor. En olası neden nedir?

- A) Deallocate edilen VM'ler faturalanmaya devam eder
- B) VM silinse de OS diski ve public IP gibi ilişkili kaynaklar ayrıca silinmedi
- C) Azure silme işlemini 30 gün geciktirir
- D) Availability set ücreti ödenir

<details><summary>Cevap</summary>

**B.** VM'in diskleri, NIC, public IP ve NSG ayrı kaynaklardır; VM'i silmek onları her zaman silmez. Bütün ortamı silmenin temiz yolu resource group'u silmektir.
</details>

#### Soru 16: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) VM Scale Set, ölçekleme kurallarına göre instance sayısını otomatik değiştirebilir. | ☐ | ☐ |
| b) Proximity placement group, VM'leri farklı region'lara dağıtarak dayanıklılığı artırır. | ☐ | ☐ |
| c) Var olan, çalışan bir VM sonradan bir availability set'e eklenebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Hayır.** PPG VM'leri bir araya toplar (gecikme için); VM availability set'e oluşturulurken yerleştirilir.
</details>

#### Soru 17: Tek seçim
Bir şirketin veritabanı sunucusu yüksek disk performansı (IOPS) ve düşük gecikme gerektiriyor. VM için hangi disk türü **en uygundur**?

- A) Standard HDD
- B) Standard SSD
- C) Premium SSD
- D) Disk gerekmez

<details><summary>Cevap</summary>

**C.** Premium SSD (veya daha uç ihtiyaçlarda Premium SSD v2 / Ultra Disk) üretim veritabanları için yüksek IOPS ve düşük gecikme sağlar; tek VM SLA'sını da etkiler.
</details>

#### Soru 18: Senaryo
Bir e-ticaret sitesinin web katmanı için şu gereksinimler var: (1) trafik artınca VM sayısı otomatik artsın, (2) bir datacenter arızasında site ayakta kalsın. Hangi tasarım **her iki** gereksinimi de karşılar?

- A) Tek bir availability set'te iki VM
- B) Availability zone'lara yayılmış ve autoscale kuralı tanımlı bir VM Scale Set
- C) Proximity placement group'ta dört VM
- D) Tek bir büyük VM ve günlük yedek

<details><summary>Cevap</summary>

**B.** Scale set + autoscale otomatik ölçeği, zone'lara yayılma datacenter arızasına dayanıklılığı sağlar. A otomatik ölçek ve datacenter koruması sunmaz; C dayanıklılığı azaltır; D tek arıza noktasıdır.
</details>

---

**Puanlama:** 16-18 → hazırsın · 12-15 → README 3.2 (VM kaynakları ve ücretlendirme) ile 3.4'teki karşılaştırma tablosunu tekrar et · < 12 → konuyu baştan çalış ve 3.6 laboratuvarını yap.


---

## 2.3 Uygulama Barındırma: App Service, Container'lar, Functions, AVD

> **Sınav Alanı:** Describe Azure architecture and services (%35-40) · **Konu Kodu:** C2.3

### 1. Ne?

Bir uygulamayı Azure'da çalıştırmanın tek yolu VM değildir (VM'ler → C1.5). Yönetim yükü azaldıkça kontrol de azalır (→ C1.3):

| Seçenek | Hizmet türü | Ne çalıştırırsın? | Tipik kullanım |
|---|---|---|---|
| **Virtual Machines** | IaaS | Tam bir işletim sistemi | OS kontrolü, lift-and-shift (→ C1.5) |
| **Azure App Service** | PaaS | Web uygulaması, REST API, mobil backend, WebJobs | OS yönetmeden web/API barındırma |
| **Azure Container Instances (ACI)** | PaaS | Tek container (veya küçük container grubu) | Hızlı, basit, orkestrasyonsuz çalıştırma |
| **Azure Container Apps** | PaaS (serverless) | Container'lardan oluşan mikroservisler | Otomatik ölçek (sıfıra kadar), HTTP ingress |
| **Azure Kubernetes Service (AKS)** | PaaS (yönetilen Kubernetes) | Çok sayıda container'ın orkestrasyonu | Büyük mikroservis platformu, en fazla kontrol |
| **Azure Functions** | PaaS (serverless) | Olay tetiklemeli kısa kod | Zamanlayıcı, kuyruk, HTTP, dosya yükleme tetikleyicisi |
| **Azure Virtual Desktop (AVD)** | Masaüstü sanallaştırma (VM'ler üzerinde) | Windows masaüstü ve uygulamaları | Uzaktan çalışan kullanıcılara bulut masaüstü |

Bu konunun C01 projesinde kodu yoktur; pratik kısım `az` CLI ve `dotnet`/Core Tools komutlarından oluşur (3.6).

---

### 2. Neden?

- **Sınav:** "Hangi barındırma seçeneği?" soruları *Describe Azure architecture and services* alanının (%35-40) en sık senaryo sorularındandır. Anahtar ifadeyi ("OS yönetmeden", "tek container", "orkestrasyon", "zamanlayıcıyla", "uzaktan masaüstü") ürüne çevirebilmek gerekir.
- **Gerçek işte:** Ekibin bir .NET API'sini nerede barındıracağı; maliyet modelini, ölçekleme biçimini, deployment sürecini ve operasyon yükünü belirler. Çoğu kurumsal web uygulaması için ilk soru "App Service yetmez mi?" olmalıdır.
- **Geliştirici yolunda:** Container'lar ve AKS'nin geliştirici derinliği → C2.1–C2.3; Functions'ın geliştirici derinliği → C2.

---

### 3. Nasıl?

#### 3.1 Azure App Service (PaaS)

**App Service**, web uygulamalarını, REST API'leri ve mobil backend'leri **işletim sistemi yönetmeden** barındıran HTTP tabanlı PaaS servisidir.

- **Diller/platformlar:** .NET, Java, Node.js, Python, PHP; **Windows ve Linux**; özel container image'ı da çalıştırabilir.
- **Uygulama türleri:** Web Apps, **API Apps** (REST API), mobil backend, **WebJobs** (aynı App Service içinde arka plan işi/script).
- **Platform özellikleri:**
  - Otomatik OS ve runtime yaması, yerleşik yük dengeleme
  - **Autoscale** (instance sayısını kurala göre artır/azalt — elasticity → C1.2)
  - **Deployment slot**'ları (ör. `staging` slotuna dağıt, test et, üretimle **swap** et — kesintisiz yayın)
  - CI/CD entegrasyonu (GitHub Actions, Azure DevOps), ZIP deploy
  - Özel domain ve TLS sertifikası, yerleşik kimlik doğrulama (Entra ID, sosyal hesaplar → C1.9)
- **App Service plan:** uygulamanın çalıştığı **compute** kaynağıdır ve **ücretlendirilen birimdir** (katman + instance sayısı). Birden çok uygulama aynı planı paylaşabilir — plandaki kaynakları da paylaşırlar. Katmanlar kabaca: Free/Shared (deneme, SLA yok) → Basic → Standard → Premium → Isolated (ayrılmış ortam). Slot ve autoscale gibi özellikler belirli katmanlardan itibaren gelir.

> Uygulama ayarları (**Application settings**) ve connection string'ler, uygulamaya **environment variable** olarak verilir; ASP.NET Core bunları `IConfiguration` üzerinden okur. Şifreleri koda/`appsettings.json`'a değil bu ayarlara (daha iyisi Key Vault referansına) koymak buradaki temel alışkanlıktır.

#### 3.2 Container'lar: ACI, Container Apps, AKS

**Container vs VM:**

| | Virtual machine | Container |
|---|---|---|
| Neyi sanallaştırır? | **Donanımı** — her VM'in kendi tam işletim sistemi var | **İşletim sistemini** — container'lar host'un çekirdeğini paylaşır |
| Boyut | GB'lar | MB'lar |
| Başlama süresi | Dakikalar | Saniyeler |
| Taşınabilirlik | İmaj ağır, sağlayıcıya bağlı | Aynı image her yerde (laptop, test, bulut) aynı çalışır |
| İzolasyon | Güçlü (ayrı çekirdek) | Daha hafif (çekirdek paylaşımı) |

Azure'daki container seçenekleri:

| Servis | Ne zaman? | Öne çıkan nitelikler |
|---|---|---|
| **Azure Container Instances (ACI)** | **Tek bir container'ı** en hızlı ve en basit biçimde çalıştırmak; kısa işler, test, toplu görev | VM veya orkestratör yönetmek yok; **saniye bazında** ücret; public IP/DNS adı verilebilir; **orkestrasyon yok** (otomatik ölçek, servis keşfi, rolling update yok) |
| **Azure Container Apps** | Container'lardan oluşan **mikroservis** ve API'ler; olay tetiklemeli container işleri | **Serverless**: altyapı yok, otomatik ölçek (**sıfıra kadar**), HTTP ingress, trafik bölme, revizyonlar; Kubernetes'in karmaşıklığını gizler |
| **Azure Kubernetes Service (AKS)** | **Çok sayıda** container'ın orkestrasyonu; Kubernetes API'sine tam erişim gereken platformlar | Yönetilen Kubernetes: control plane Microsoft'ta, node'lar (VM'ler) senin aboneliğinde; en fazla kontrol, en fazla bilgi gerektirir |

> Container image'ları genellikle **Azure Container Registry (ACR)**'de saklanır *(bağlam)*. App Service de container çalıştırabilir.

#### 3.3 Azure Virtual Desktop (AVD)

**Azure Virtual Desktop**, Azure'da çalışan bir **masaüstü ve uygulama sanallaştırma** servisidir: kullanıcılar herhangi bir cihazdan (Windows, macOS, iOS, Android, tarayıcı) bulutta çalışan bir Windows masaüstüne veya yalnızca belirli uygulamalara (RemoteApp) bağlanır.

- **Windows 11 / Windows 10 Enterprise multi-session:** birden çok kullanıcı **aynı VM'i** eşzamanlı paylaşır, tam Windows 11/10 deneyimini alır — maliyeti düşürür. (Windows Server tabanlı oturum host'ları da desteklenir.)
- **Kimlik ve güvenlik:** Microsoft Entra ID ile kimlik doğrulama, **MFA** ve Conditional Access (→ C1.9); uygulama ve **veri cihazda değil bulutta** kalır — kaybolan bir dizüstü veri sızıntısı demek değildir.
- **Yönetim:** masaüstleri ve kullanıcı atamaları merkezden yönetilir; altta yatan oturum host'ları senin aboneliğindeki VM'lerdir (ölçeklenebilir, gerektiğinde kapatılabilir).
- Tipik senaryolar: uzaktan çalışma, geçici/sözleşmeli personel, güvenlik gereği verinin cihaza inmemesi, ağır uygulamaları zayıf cihazlarda çalıştırma.

#### 3.4 Azure Functions

**Azure Functions**, **olay tetiklemeli**, kısa süreli kod parçalarını sunucu yönetmeden çalıştıran serverless compute servisidir (serverless kavramı → C1.3).

- **Tetikleyiciler (trigger):** HTTP isteği, **zamanlayıcı** (cron), kuyruk mesajı (Storage Queue, Service Bus), **blob yüklemesi**, Event Grid/Event Hubs olayı, Cosmos DB değişikliği…
- **Binding'ler** *(bağlam)*: girdi/çıktı bağlantılarını (ör. "sonucu bir kuyruğa yaz") kod yazmadan bildirmek.
- **Stateless** (varsayılan; her çağrı bağımsız) ve **stateful** — **Durable Functions** ile uzun süren, adımları ve durumu olan iş akışları (orkestrasyon).
- **Ölçek ve ücret:** yüke göre otomatik ölçek; serverless planlarda yalnızca çalıştığı süre ve çağrı kadar ücret. Microsoft yeni serverless uygulamalar için **Flex Consumption** planını öneriyor; Linux üzerindeki klasik Consumption barındırması **30 Eylül 2028**'de emekliye ayrılacak (Windows Consumption bu duyurunun dışında). Sürekli yük veya VNet/ön ısıtma ihtiyacı için Premium ve Dedicated (App Service plan) seçenekleri de var.

**Functions vs App Service vs Logic Apps:**

| İhtiyaç | Seçim |
|---|---|
| Sürekli çalışan web uygulaması/API | App Service |
| Bir olaya tepki veren, kısa süreli **kod** | Functions |
| **Kod yazmadan**, görsel tasarımcı ve hazır connector'larla iş akışı | Logic Apps |

#### 3.5 Barındırma karar ağacı

```
Full control over the OS / custom software / lift-and-shift? → Virtual Machines (→ C1.5)
Serve desktops or Windows apps remotely?                     → Azure Virtual Desktop
Short event-triggered code (timer, queue, blob, HTTP)?       → Azure Functions
No code, visual workflow/integration?                        → Logic Apps
Ready-made web/API platform, without managing the OS?        → App Service
Container:
   single container, fast, no orchestration                  → Container Instances
   microservices, serverless, scale to zero                  → Container Apps
   many containers, full Kubernetes control                  → AKS
```

#### 3.6 Pratik — App Service, Container Instances ve yerel Function

> Bütçe alarmın kurulu olsun (→ C1.1). App Service **F1 (ücretsiz)** katmanda; ACI saniye bazında ücretlenir (birkaç dakikalık deneme kuruşlar mertebesindedir); yerel Function ücretsizdir. **Sonunda `az group delete` şart.**

**(a) Todo API'yi App Service'e deploy etmek**

```bash
# A small .NET Web API (we do not create a project in the repo; use a temporary folder)
dotnet new webapi -n TodoApi && cd TodoApi

az login
az group create --name rg-az900-c16 --location westeurope

# az webapp up: creates the plan + web app, builds the code and ZIP-deploys it
APP=todo-c16-$RANDOM
az webapp list-runtimes --os linux --output table            # valid runtime values
az webapp up --resource-group rg-az900-c16 --name $APP \
  --sku F1 --os-type linux --runtime "DOTNETCORE:10.0"

curl https://$APP.azurewebsites.net/weatherforecast          # the template's sample endpoint
```

**(b) Ayarların environment variable olarak `IConfiguration`'a akması**

```bash
# Application setting: "__" (double underscore) becomes ":" in IConfiguration → Todo:Greeting
az webapp config appsettings set --resource-group rg-az900-c16 --name $APP \
  --settings Todo__Greeting="Hello App Service"

# Connection string: reaches the app as the SQLAZURECONNSTR_TodoDb env var;
# ASP.NET Core reads it with builder.Configuration.GetConnectionString("TodoDb")
az webapp config connection-string set --resource-group rg-az900-c16 --name $APP \
  --connection-string-type SQLAzure --settings TodoDb="Server=tcp:example.database.windows.net;Database=todo;"

az webapp config appsettings list --resource-group rg-az900-c16 --name $APP --output table
```

Uygulama tarafında okumak için `Program.cs`'e tek satırlık bir endpoint eklemek yeterli (deneme kodu, yeniden `az webapp up` ile dağıtılır):

```csharp
app.MapGet("/config", (IConfiguration c) => new { greeting = c["Todo:Greeting"], db = c.GetConnectionString("TodoDb") is not null });
```

**(c) Log stream — canlı log**

```bash
az webapp log config --resource-group rg-az900-c16 --name $APP \
  --docker-container-logging filesystem --level information   # enable console logs on a Linux app
az webapp log tail --resource-group rg-az900-c16 --name $APP   # exit with Ctrl+C; send a request from another terminal
```

**(d) Aynı işi bir container ile: Azure Container Instances**

```bash
# A sample web container with a single command — no VM, no plan, no orchestrator
az container create --resource-group rg-az900-c16 --name aci-c16 \
  --image mcr.microsoft.com/azuredocs/aci-helloworld \
  --os-type Linux --cpu 1 --memory 1.5 --ports 80 \
  --dns-name-label aci-c16-$RANDOM
az container show --resource-group rg-az900-c16 --name aci-c16 --query "{fqdn:ipAddress.fqdn,state:instanceView.state}" --output table
az container logs --resource-group rg-az900-c16 --name aci-c16
```

> Kendi Todo API'ni container olarak çalıştırmak için image'ı bir registry'ye (ör. Azure Container Registry) push etmen gerekir; .NET SDK `dotnet publish /t:PublishContainer` ile Dockerfile'sız image üretebilir *(bağlam — container derinliği → C2.1)*.

**(e) HTTP tetiklemeli bir Azure Function — yerelde, Core Tools ile (ücretsiz)**

```bash
# Azure Functions Core Tools v4 must be installed (func --version)
func init FuncC16 --worker-runtime dotnet-isolated
cd FuncC16
func new --name Hello --template "HTTP trigger" --authlevel anonymous
func start                                       # http://localhost:7071/api/Hello
curl "http://localhost:7071/api/Hello?name=az900"
```

Gözlem: Function için ne bir sunucu ne bir plan seçtin; yalnızca **tetikleyici** (HTTP) ve **kod** var. Azure'a yayınlamak istersen `func azure functionapp publish <ad>` kullanılır (bir Function App ve storage account gerektirir).

**Temizlik**

```bash
az group delete --name rg-az900-c16 --yes --no-wait
```

<details><summary>Kendini sına: hangi adım hangi kavramı gösterdi?</summary>

| Adım | Kavram |
|---|---|
| `az webapp up` → OS seçmeden uygulama yayında | App Service = PaaS; plan = ücretlendirilen compute |
| `Todo__Greeting` → `Todo:Greeting` | Application settings env var olarak `IConfiguration`'a akar |
| `az webapp log tail` | Platformun sunduğu yerleşik izleme (management *of* the cloud → C1.2) |
| `az container create` | ACI: tek container, orkestrasyonsuz, saniye bazında ücret |
| `func new … HTTP trigger` | Functions: tetikleyici + kod, sunucu yok |
| `az group delete` | Resource group yaşam döngüsü (→ C1.4) |
</details>

---

### 4. Gerçek dünya senaryosu

Bir sigorta acentesi ağı dijitalleşme projesinde iş yüklerini şöyle dağıtıyor:

| İş yükü | Seçim | Gerekçe |
|---|---|---|
| Müşteri portalı ve mobil uygulamanın REST API'si | **App Service** (Standard, autoscale, `staging` slotu) | Ekip .NET kodu yazıyor; OS yönetmek istemiyor; slot swap ile kesintisiz yayın |
| Poliçe PDF'lerini gece üreten iş | **Azure Functions** (zamanlayıcı tetikleyici) | Günde bir kez birkaç dakika çalışıyor; boşta ücret yok |
| Yüklenen hasar fotoğraflarını işleme | **Functions** (blob tetikleyici) | Olay tetiklemeli, düzensiz yük |
| Dört mikroservisten oluşan teklif motoru | **Container Apps** | Container'lar hazır; Kubernetes ekibi yok; yoğunlukta otomatik ölçek, gece sıfıra iner |
| Aylık bir kez çalışan veri dönüştürme container'ı | **Container Instances** | Tek container, birkaç dakika; orkestrasyona gerek yok |
| 400 acente çalışanının eski Windows masaüstü uygulaması | **Azure Virtual Desktop** (Windows 11 multi-session) | Uygulama ve müşteri verisi bulutta kalıyor; acentelerin cihazları önemsizleşiyor; MFA zorunlu |
| Lisansı belirli bir OS sürümüne bağlı eski hesaplama motoru | **VM** (→ C1.5) | OS üzerinde tam kontrol gerekiyor |

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Neredeyse tamamen senaryo: "X yapmak istiyorsunuz, **hangi** Azure servisini önerirsiniz?" ve evet/hayır tabloları ("Azure Functions yalnızca HTTP ile tetiklenebilir" → Hayır).

| Senaryodaki ifade | Cevap |
|---|---|
| "Web uygulaması/API, **OS yönetmeden**, otomatik yama ve autoscale" | **App Service** |
| "Aynı compute'u paylaşan birden çok web uygulaması" / "App Service'te ne ücretlendirilir?" | **App Service plan** |
| "**Tek bir container**'ı hızlıca, VM/orkestratör yönetmeden" | **Container Instances** |
| "**Binlerce container**, orkestrasyon, Kubernetes" | **AKS** |
| "Serverless container, mikroservis, **sıfıra kadar ölçek**" | **Container Apps** |
| "**Zamanlayıcıyla** / kuyruk mesajıyla / dosya yüklenince çalışan kısa kod" | **Azure Functions** |
| "Uzun süren, **durum tutan** fonksiyon iş akışı" | **Durable Functions** |
| "Kod yazmadan iş akışı" | **Logic Apps** |
| "Kullanıcılar her cihazdan **Windows masaüstüne** erişsin; veri cihazda kalmasın" | **Azure Virtual Desktop** |
| "Birden çok kullanıcı **aynı VM'de** Windows 11 oturumu" | AVD **multi-session** |
| "Container **işletim sistemini**, VM **donanımı** sanallaştırır" | Doğru |

**Tuzaklar:**
- ACI ile AKS: "en basit/hızlı tek container" → ACI; "orkestrasyon/ölçek/çok container" → AKS (veya serverless isteniyorsa Container Apps).
- Functions ile Logic Apps: **kod** → Functions; **görsel/low-code** → Logic Apps.
- App Service ile VM: soru "işletim sistemine erişim" diyorsa VM; "yönetim yükü az" diyorsa App Service.
- AVD bir "uzak masaüstü protokolü" değil, **yönetilen bir masaüstü sanallaştırma servisidir**; kimlik Entra ID ile yapılır.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "App Service'te uygulama başına ödenir" | Ücretlendirilen birim **App Service plan**'dır; aynı plandaki uygulamalar ek compute ücreti doğurmaz ama kaynakları paylaşır |
| "Container = küçük VM" | Container OS'u sanallaştırır, host çekirdeğini paylaşır; VM'in kendi OS'u vardır |
| "ACI ile mikroservis platformu kurarım" | ACI'de orkestrasyon yok; mikroservis için Container Apps veya AKS |
| "Functions yalnızca HTTP ile çalışır" | Zamanlayıcı, kuyruk, blob, Event Grid … birçok tetikleyici var |
| "Functions her zaman en ucuzu" | Sürekli yüksek yükte sabit bir plan ucuzlayabilir (→ C1.3); Durable olmayan fonksiyonlar uzun iş için uygun değildir |
| "AVD kullanıcının bilgisayarına Windows kurar" | Masaüstü Azure'daki VM'lerde çalışır; cihaz yalnızca bağlantı istemcisidir |
| Connection string'i `appsettings.json`'a yazıp repoya koymak | App Service ayarları / Key Vault referansı; ayarlar env var olarak gelir |
| Laboratuvardan sonra kaynakları bırakmak | `az group delete` — özellikle plan ve ACI süre bazlı ücretlenir |

---

### 7. Performans & trade-off

| Seçenek | Kazanç | Bedel |
|---|---|---|
| VM | Tam kontrol, her yazılım çalışır | Tüm OS/yama/ölçek yükü sende |
| App Service | Hızlı yayın, yama yok, slot, autoscale | Platform kısıtları; plan boşta da ücretlenir |
| Container Instances | En basit container çalıştırma, saniye bazında ücret | Orkestrasyon, otomatik ölçek, rolling update yok |
| Container Apps | Serverless container, sıfıra ölçek, mikroservis özellikleri | Kubernetes API'sine tam erişim yok; cold start |
| AKS | Tam Kubernetes esnekliği, büyük ölçek | Öğrenme eğrisi ve operasyon yükü (node'lar, yükseltmeler) en yüksek |
| Functions | Olay tetiklemeli, boşta 0 ücret, otomatik ölçek | Cold start, çalışma süresi sınırları, durum için Durable gerekir |
| AVD | Veri bulutta, merkezi yönetim, her cihazdan erişim | Ağ gecikmesine duyarlı kullanıcı deneyimi; oturum host'larının maliyeti ve boyutlandırması |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure compute and networking services](https://learn.microsoft.com/en-us/training/modules/describe-azure-compute-networking-services/)
- [App Service overview](https://learn.microsoft.com/en-us/azure/app-service/overview) · [App Service plans](https://learn.microsoft.com/en-us/azure/app-service/overview-hosting-plans) · [Configure an App Service app (app settings, connection strings)](https://learn.microsoft.com/en-us/azure/app-service/configure-common)
- [Azure Container Instances](https://learn.microsoft.com/en-us/azure/container-instances/container-instances-overview) · [Azure Container Apps](https://learn.microsoft.com/en-us/azure/container-apps/overview) · [AKS](https://learn.microsoft.com/en-us/azure/aks/what-is-aks)
- [What is Azure Virtual Desktop?](https://learn.microsoft.com/en-us/azure/virtual-desktop/overview)
- [Azure Functions overview](https://learn.microsoft.com/en-us/azure/azure-functions/functions-overview) · [Hosting options (Flex Consumption …)](https://learn.microsoft.com/en-us/azure/azure-functions/functions-scale) · [Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- [`az webapp up`](https://learn.microsoft.com/en-us/cli/azure/webapp#az-webapp-up) · [`az container create`](https://learn.microsoft.com/en-us/cli/azure/container#az-container-create)


### 📝 Bölüm 2.3 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir ekip ASP.NET Core web uygulamasını yayınlamak istiyor. İşletim sistemi yaması yapmak istemiyor; otomatik ölçekleme ve deployment slot'larına ihtiyaç duyuyor. Hangi servis **en uygundur**?

- A) Azure Virtual Machines
- B) Azure App Service
- C) Azure Container Instances
- D) Azure Virtual Desktop

<details><summary>Cevap</summary>

**B.** App Service, OS yönetmeden web/API barındıran PaaS'tır; autoscale ve deployment slot'ları yerleşik özelliklerdir. VM OS yönetimi gerektirir; ACI orkestrasyonsuz tek container içindir; AVD masaüstü sanallaştırmadır.
</details>

#### Soru 2: Tek seçim
App Service'te uygulamanın çalıştığı compute kaynağını tanımlayan ve **ücretlendirilen birim** olan nedir?

- A) Deployment slot
- B) Resource group
- C) App Service plan
- D) WebJob

<details><summary>Cevap</summary>

**C.** App Service plan (katman + instance sayısı) ücretlendirilir; birden çok uygulama aynı planı paylaşabilir.
</details>

#### Soru 3: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) App Service hem Windows hem Linux üzerinde uygulama çalıştırabilir. | ☐ | ☐ |
| b) App Service'te uygulamanın işletim sistemi yamalarını müşteri uygular. | ☐ | ☐ |
| c) WebJobs, App Service içinde arka plan işi çalıştırmaya olanak tanır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** App Service PaaS'tır; OS yamaları Microsoft'tadır.
</details>

#### Soru 4: Tek seçim
Container'ları sanal makinelerden ayıran temel özellik hangisidir?

- A) Container'lar donanımı sanallaştırır, her biri kendi çekirdeğini çalıştırır
- B) Container'lar işletim sistemini sanallaştırır, host'un çekirdeğini paylaşır ve saniyeler içinde başlar
- C) Container'lar yalnızca Windows üzerinde çalışır
- D) Container'lar her zaman VM'lerden daha güçlü izolasyon sağlar

<details><summary>Cevap</summary>

**B.** VM donanımı sanallaştırır (tam OS); container OS'u sanallaştırır — hafif, taşınabilir, hızlı başlar. İzolasyon VM'de daha güçlüdür (D yanlış).
</details>

#### Soru 5: Senaryo
Bir veri ekibi ayda bir kez çalışan, yaklaşık 10 dakika süren tek bir container'ı **en basit** yoldan çalıştırmak istiyor. VM veya orkestratör yönetmek istemiyor. Hangi servisi seçmeliler?

- A) Azure Kubernetes Service
- B) Azure Container Instances
- C) Azure Virtual Machine Scale Sets
- D) Azure Virtual Desktop

<details><summary>Cevap</summary>

**B.** Tek container, kısa süre, orkestrasyon gerekmiyor → ACI; saniye bazında ücretlenir. AKS orkestrasyon için ağır bir seçimdir.
</details>

#### Soru 6: Senaryo
Bir şirket yüzlerce container'dan oluşan bir mikroservis platformu çalıştıracak; rolling update, servis keşfi ve Kubernetes API'sine tam erişim istiyor. Hangi servis **en uygundur**?

- A) Azure Container Instances
- B) Azure Functions
- C) Azure Kubernetes Service (AKS)
- D) Azure App Service

<details><summary>Cevap</summary>

**C.** Çok sayıda container'ın orkestrasyonu ve Kubernetes API'si = AKS (yönetilen Kubernetes).
</details>

#### Soru 7: Tek seçim
Bir ekip container tabanlı API'lerini çalıştırmak istiyor; Kubernetes yönetmek istemiyor, trafik yokken uygulamanın **sıfıra ölçeklenmesini** ve kullanım kadar ödemeyi istiyor. Hangisi **en uygundur**?

- A) Azure Container Apps
- B) Azure Virtual Machines
- C) Azure Kubernetes Service
- D) Azure Virtual Desktop

<details><summary>Cevap</summary>

**A.** Container Apps serverless container platformudur: otomatik ölçek (sıfıra kadar), HTTP ingress, mikroservis özellikleri; Kubernetes karmaşıklığını gizler.
</details>

#### Soru 8: Eşleştirme
Her ihtiyacı servisle eşleştirin: **App Service · Container Instances · AKS · Azure Functions**

1. Her gece 02:00'de 3 dakika çalışan temizlik kodu
2. Sürekli çalışan kurumsal web sitesi, OS yönetimi yok
3. Tek bir test container'ını hızlıca ayağa kaldırmak
4. Büyük ölçekli container orkestrasyonu

<details><summary>Cevap</summary>

**1 → Functions** (zamanlayıcı tetikleyici) · **2 → App Service** · **3 → Container Instances** · **4 → AKS**.
</details>

#### Soru 9: Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** Azure Functions'ı tetikleyebilir?

- A) Bir Blob Storage container'ına dosya yüklenmesi
- B) Bir VM'in boyutunun değiştirilmesi için portalda menü açılması
- C) Bir zamanlayıcı (cron ifadesi)
- D) Kullanıcının ofis bilgisayarını kapatması

<details><summary>Cevap</summary>

**A ve C.** Blob, zamanlayıcı, HTTP, kuyruk mesajı, Event Grid gibi olaylar tetikleyicidir.
</details>

#### Soru 10: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Functions yalnızca HTTP istekleriyle tetiklenebilir. | ☐ | ☐ |
| b) Durable Functions, durum tutan (stateful) uzun süreli iş akışları yazmaya olanak tanır. | ☐ | ☐ |
| c) Serverless planlarda Functions, çalışmadığı sürede de compute ücreti doğurur. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** Birçok tetikleyici vardır; Durable Functions stateful orkestrasyon sağlar; serverless planlarda boşta compute ücreti yoktur.
</details>

#### Soru 11: Tek seçim
Bir iş analisti, **kod yazmadan**, görsel bir tasarımcıyla bir SaaS uygulamasındaki yeni kaydı e-postayla bildiren bir iş akışı kurmak istiyor. Hangi servis **en uygundur**?

- A) Azure Functions
- B) Azure Logic Apps
- C) Azure Kubernetes Service
- D) Azure App Service

<details><summary>Cevap</summary>

**B.** Low-code, görsel tasarımcı ve hazır connector'lar = Logic Apps. Functions aynı işi **kodla** yapar.
</details>

#### Soru 12: Senaryo
Bir şirket, sözleşmeli çalışanlarının kendi cihazlarından şirket uygulamalarına erişmesini; ancak **verinin bu cihazlara inmemesini** istiyor. Kimlik doğrulamada MFA zorunlu olacak. Hangi servis **en uygundur**?

- A) Azure Virtual Desktop
- B) Azure Container Instances
- C) Azure Functions
- D) Azure App Service

<details><summary>Cevap</summary>

**A.** AVD, masaüstü ve uygulamaları Azure'da çalıştırır; cihaz yalnızca bağlanır, veri bulutta kalır; kimlik Entra ID ve MFA ile yapılır.
</details>

#### Soru 13: Tek seçim
Azure Virtual Desktop'ta birden çok kullanıcının **aynı VM üzerinde** eşzamanlı olarak tam Windows 11 masaüstü deneyimi almasını sağlayan özellik hangisidir?

- A) Availability set
- B) Windows 11 Enterprise multi-session
- C) Deployment slot
- D) VM Scale Sets

<details><summary>Cevap</summary>

**B.** Windows 11/10 Enterprise multi-session, AVD'ye özgü çok oturumlu Windows sürümüdür ve VM başına maliyeti düşürür.
</details>

#### Soru 14: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Virtual Desktop'ta kullanıcı masaüstleri Azure'daki VM'lerde çalışır. | ☐ | ☐ |
| b) Azure Virtual Desktop, Microsoft Entra ID ile kimlik doğrulamayı ve MFA'yı destekler. | ☐ | ☐ |
| c) Azure Virtual Desktop'a yalnızca Windows cihazlardan bağlanılabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Windows, macOS, iOS, Android istemcileri ve web tarayıcısı desteklenir.
</details>

#### Soru 15: Tek seçim
Bir .NET uygulaması App Service'te çalışıyor. Veritabanı connection string'ini **koda yazmadan** uygulamaya vermenin doğru yolu hangisidir?

- A) Connection string'i `appsettings.json`'a yazıp repoya eklemek
- B) App Service'in Application settings / Connection strings ayarlarını kullanmak (uygulamaya environment variable olarak gelir)
- C) Connection string'i bir VM'in masaüstüne kaydetmek
- D) Her istekte kullanıcıdan istemek

<details><summary>Cevap</summary>

**B.** App Service ayarları uygulamaya env var olarak verilir ve ASP.NET Core `IConfiguration` ile okunur; daha güvenli seçenek Key Vault referansıdır (→ C1.10).
</details>

#### Soru 16: Sıralama
Aşağıdaki barındırma seçeneklerini **müşterinin yönetim yükü en fazladan en aza** doğru sıralayın: Azure Functions · Virtual Machines · Azure App Service

<details><summary>Cevap</summary>

**Virtual Machines → App Service → Azure Functions.** VM'de OS sende; App Service'te platform Microsoft'ta ama planı sen boyutlandırırsın; serverless Functions'ta kapasite yönetimi de yoktur.
</details>

#### Soru 17: Senaryo
Bir uygulama, belirli bir OS sürümüne ve özel bir sürücüye bağımlı; kod değiştirilemiyor. Hangi barındırma seçeneği **zorunlu** hale gelir?

- A) Azure Virtual Machines
- B) Azure App Service
- C) Azure Container Apps
- D) Azure Functions

<details><summary>Cevap</summary>

**A.** OS üzerinde tam kontrol ve özel sürücü kurulumu yalnızca IaaS'ta (VM) mümkündür (→ C1.5).
</details>

#### Soru 18: Tek seçim
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


---

## 2.4 Ağ Servisleri: VNet, Peering, VPN, ExpressRoute, DNS

> **Sınav Alanı:** Describe Azure architecture and services (%35-40) · **Konu Kodu:** C2.4

### 1. Ne?

Azure'daki kaynakların (VM, App Service, veritabanı…) birbirleriyle, internetle ve şirketin kendi datacenter'ıyla nasıl konuştuğunu belirleyen servisler:

| Servis | Tek cümle |
|---|---|
| **Virtual Network (VNet)** | Azure'daki kaynakların **izole**, özel ağı; bir region'a bağlıdır |
| **Subnet** | VNet'in alt ağı; kaynakları gruplar, güvenlik kuralları subnet'e uygulanır |
| **VNet peering** | İki VNet'i **Microsoft omurgası** üzerinden (internete çıkmadan) birbirine bağlar |
| **Azure DNS** | Domain'lerin DNS kayıtlarını Azure'da **barındırır** (domain satmaz) |
| **VPN Gateway** | On-prem ile Azure arasında **internet üzerinden şifreli** tünel |
| **ExpressRoute** | On-prem ile Azure arasında **internete çıkmayan özel** bağlantı |
| **Public endpoint** | Bir servise **internetten** erişilen genel adres |
| **Private endpoint** (Private Link) | PaaS servisine VNet içinden **özel IP** ile erişim |

Bu konunun C01 projesinde kodu yoktur; pratik kısım `az` CLI laboratuvarıdır (3.7).

---

### 2. Neden?

- **Sınav:** "Hangi bağlantı seçeneği?" (VPN Gateway, ExpressRoute, peering) ve "public mi private endpoint mi?" soruları bu alanın klasikleridir; ayrımı tek bir anahtar ifade yapar ("internetten geçmeden", "şifreli tünel", "iki VNet").
- **Gerçek işte:** Ağ tasarımı güvenliğin (segmentasyon, NSG, private endpoint → C1.10 defense in depth) ve hybrid mimarinin (→ C1.1) temelidir. Yanlış adres planı (çakışan CIDR'ler) peering'i ve VPN'i sonradan imkânsız kılar.
- **Bağlam:** IP adresleme, CIDR, DNS ve TCP/IP temelleri → J1.2; kurumsal ağ topolojileri (hub-spoke, Virtual WAN) → S8.3.

---

### 3. Nasıl?

#### 3.1 Azure Virtual Network ve subnet'ler

**VNet**, Azure kaynaklarının birbirleriyle, internetle ve on-prem ağlarla **güvenli** iletişim kurmasını sağlayan özel ağdır.

- **Adres alanı:** özel IP aralığı, CIDR ile (ör. `10.0.0.0/16` → ~65 bin adres). Aynı adres alanına bağlanacak ağlarla (diğer VNet'ler, on-prem) **çakışmamalıdır**.
- **Kapsam:** bir VNet **tek bir region**'dadır, ama o region'ın tüm availability zone'larına yayılır; bir subscription'a aittir.
- **Subnet:** adres alanının alt aralığı (ör. `10.0.1.0/24` web, `10.0.2.0/24` veri); kaynakları gruplamak ve **NSG** (network security group — izin/engel kuralları) uygulamak için.

VNet'in sağladığı yetenekler (resmî içerikte sayılan başlıklar):

| Yetenek | Açıklama | Örnek |
|---|---|---|
| **İzolasyon ve segmentasyon** | Ağı subnet'lere bölüp birbirinden ayırmak | Web / uygulama / veri subnet'leri |
| **İnternet iletişimi** | Kaynağa public IP veya public load balancer ile internetten erişim; giden trafik varsayılan olarak mümkün | Public IP'li VM, load balancer |
| **Azure kaynakları arası iletişim** | Aynı VNet'teki kaynaklar; **service endpoint**/private endpoint ile PaaS servisleri; peering ile diğer VNet'ler | VM → Azure SQL (private endpoint) |
| **On-prem ile iletişim** | Point-to-site / site-to-site VPN, ExpressRoute | Ofis ağı ↔ Azure |
| **Trafik yönlendirme** | Varsayılan sistem rotaları; **route table** (user-defined routes) ve BGP ile özel yönlendirme | Trafiği bir firewall'dan geçirmek |
| **Trafik filtreleme** | **NSG** (IP/port/protokol kuralları) ve network virtual appliance (NVA — firewall gibi özel cihazlar) | "Yalnızca 443'e izin ver" |

#### 3.2 VNet peering

**Peering**, iki VNet'i **Microsoft'un omurga ağı** üzerinden bağlar; trafik **internete çıkmaz**, gateway gerekmez.

- **Regional peering:** aynı region'daki VNet'ler · **Global peering:** farklı region'lardaki VNet'ler.
- **Düşük gecikme, yüksek bant genişliği** — aynı ağdaymış gibi.
- **Kurallar:** adres alanları **çakışmamalı** · peering **iki yönlü** tanımlanır (her VNet'te bir peering bağlantısı) · **geçişli (transitive) değildir**: A↔B ve B↔C peering'i, A ile C'yi konuşturmaz (A↔C ayrıca peering ister veya hub'da bir router/firewall gerekir) · farklı subscription ve hatta farklı tenant'lardaki VNet'ler de peering yapabilir.
- Ücret: peering'in kendisi değil, peering üzerinden geçen **veri transferi** ücretlendirilir (global peering daha pahalıdır) (→ C1.11).

#### 3.3 Azure DNS

**Azure DNS**, DNS domain'lerini Azure altyapısında barındıran servistir.

- **Güvenilirlik ve performans:** Azure'un dünya çapındaki ad sunucu ağı; anycast ile kullanıcıya en yakın sunucu cevap verir.
- **Yönetim kolaylığı:** kayıtlar portal, CLI, PowerShell, ARM/Bicep ile yönetilir; aynı faturada, aynı kimlikle.
- **Güvenlik:** Azure **RBAC** (kim DNS kaydını değiştirebilir), **activity log** (kim neyi değiştirdi), **resource lock** (kritik zone'un silinmesini engelleme → C1.12).
- **Alias kayıtları** *(bağlam)*: bir kaydı doğrudan bir Azure kaynağına (public IP, Traffic Manager, Front Door) bağlamak; IP değişince kayıt kendiliğinden güncellenir.
- **Önemli:** Azure DNS ile **domain satın alınmaz** — domain bir kayıt kuruluşundan (registrar) alınır, NS kayıtları Azure DNS'e yönlendirilir. *(App Service Domains gibi ayrı bir satın alma seçeneği vardır ama bu Azure DNS'in kendisi değildir.)*
- **Private DNS zone:** VNet içinde, internete açık olmayan **özel isim çözümleme** (ör. `db.internal.contoso.com` → `10.0.2.4`; private endpoint'lerin `privatelink.*` zone'ları).

#### 3.4 VPN Gateway

Bir **VPN**, iki ağ arasında **şifreli tünel** kurar. **Azure VPN Gateway**, bir VNet'e konuşlandırılan ve **public internet üzerinden** IPsec/IKE ile şifreli bağlantı sağlayan sanal ağ geçididir (VNet başına bir VPN gateway).

| Bağlantı türü | Ne bağlanır? |
|---|---|
| **Site-to-site (S2S)** | Ofis/datacenter ağı ↔ Azure VNet (her iki uçta VPN cihazı) |
| **Point-to-site (P2S)** | Tek bir istemci (geliştiricinin dizüstü bilgisayarı) ↔ Azure VNet |
| **VNet-to-VNet** | Azure VNet ↔ Azure VNet (peering'e alternatif; şifreli tünel) |

- **Policy-based** VPN: tüneli hangi trafiğin kullanacağını statik IP aralığı kurallarıyla belirler (eski/kısıtlı; yalnızca belirli senaryolar). **Route-based** VPN: rota tablosuna göre yönlendirir; çoğu senaryo, P2S ve çoklu bağlantılar için önerilen tür.
- **Yüksek erişilebilirlik:** **active/standby** (varsayılan — bakım/arıza sırasında yedek instance kısa bir kesintiyle devralır) · **active/active** (iki instance da tünel taşır) · **zone-redundant gateway** (instance'lar availability zone'lara yayılır) · **ExpressRoute failover** (ExpressRoute'un yedeği olarak VPN).
- *(bağlam — güncel SKU durumu)* Microsoft, zone desteksiz (non-AZ) VpnGw1–5 SKU'larını **AZ SKU'larına** taşıyor; duyurulara göre non-AZ SKU'lar Eylül 2026'da emekliye ayrıldı/otomatik taşındı. Sınav SKU adlarını sormaz; güncel durumu VPN Gateway "What's new" sayfasından kontrol et.

#### 3.5 ExpressRoute

**ExpressRoute**, on-prem ağı Microsoft bulutuna bir **bağlantı sağlayıcısı** (telekom operatörü/ortak tesis) üzerinden bağlayan **özel** bir bağlantıdır; trafik **public internete çıkmaz**.

- **Faydaları:** daha **yüksek bant genişliği**, daha **öngörülebilir gecikme**, daha yüksek **güvenilirlik**; Azure'un yanında **Microsoft 365** ve Dynamics 365 servislerine de bağlanabilir; dinamik yönlendirme (BGP); yerleşik yedeklilik (bağlantı çifti).
- **Bağlantı modelleri** *(bağlam)*: CloudExchange co-location, point-to-point Ethernet, any-to-any (IPVPN) ve **ExpressRoute Direct** (Microsoft'un ağına doğrudan, 10/100 Gbps fiziksel port).
- **Global Reach:** farklı şubelerin on-prem ağlarını ExpressRoute devreleri üzerinden **Microsoft omurgasıyla birbirine** bağlamak.
- Not: ExpressRoute **özel** bir hattır ama varsayılan olarak **şifreli tünel değildir**; şifreleme gerekiyorsa ExpressRoute Direct'te MACsec veya ExpressRoute üzerinden IPsec kullanılır *(bağlam)*.

**VPN Gateway vs ExpressRoute** ⭐

| | VPN Gateway | ExpressRoute |
|---|---|---|
| Trafik nereden geçer? | **Public internet** (şifreli tünel) | **Özel bağlantı** — internete çıkmaz |
| Kurulum | Hızlı; VPN cihazı + gateway | Bağlantı sağlayıcısıyla sözleşme, devre tahsisi (haftalar) |
| Bant genişliği / gecikme | Daha düşük, internete bağlı değişken | Yüksek, öngörülebilir |
| Maliyet | Düşük | Yüksek |
| Tipik kullanım | Küçük/orta ofis, geliştirici erişimi (P2S), ExpressRoute yedeği | Büyük veri hacmi, kritik hybrid iş yükleri, regülasyon |

#### 3.6 Public ve private endpoint'ler

- **Public endpoint:** bir kaynağın internetten erişilebilen adresi (ör. `mystorage.blob.core.windows.net`, `myapp.azurewebsites.net`). Erişim firewall kuralları, kimlik doğrulama ve TLS ile korunur ama adres **internete açıktır**.
- **Private endpoint** (**Azure Private Link**): bir PaaS servisine (Storage, SQL Database, Key Vault…) VNet'inin içinden bir **özel IP** ile bağlanan ağ arayüzü.
  - Trafik **Microsoft ağında** kalır; servisin **public erişimi tamamen kapatılabilir**.
  - On-prem ağdan da (VPN/ExpressRoute üzerinden) özel IP ile erişilebilir.
  - DNS: servis adı (`mystorage.blob.core.windows.net`) VNet içinde **private DNS zone** (`privatelink.blob.core.windows.net`) sayesinde özel IP'ye çözülür.
- *(bağlam)* **Service endpoint:** VNet'in kimliğini servise tanıtıp servis firewall'ında yalnızca o subnet'e izin vermek; servis yine public IP'sinden erişilir. Private endpoint daha sıkı izolasyon sağlar.

*(bağlam — sınavda ad olarak geçebilir)*

| Servis | Ne yapar? |
|---|---|
| **NSG** | Subnet/NIC düzeyinde IP-port-protokol izin/engel kuralları |
| **Azure Load Balancer** | Katman 4 (TCP/UDP) yük dengeleme |
| **Application Gateway** | Katman 7 (HTTP) yük dengeleme + **WAF** (web uygulama güvenlik duvarı) |
| **Azure Front Door** | Global HTTP giriş noktası, CDN, WAF, bölgeler arası yönlendirme |
| **Azure Firewall** | Yönetilen, durum bilgili ağ güvenlik duvarı |
| **Azure DDoS Protection** | Dağıtık hizmet reddi saldırılarına karşı koruma (→ C1.10) |

#### 3.7 Pratik — VNet, peering, private DNS ve private endpoint

> Bütçe alarmın kurulu olsun (→ C1.1). VNet, subnet ve peering bağlantısının kendisi ücretsizdir (peering'de **veri transferi** ücretlidir); private DNS zone, private endpoint ve test VM'i **saatlik/aylık küçük ücret** doğurur — laboratuvarı tek oturumda bitirip **sil**. VPN Gateway ve ExpressRoute **kurmuyoruz** (gateway'in kurulumu ~30-45 dk sürer ve sürekli ücretlenir; ExpressRoute sağlayıcı sözleşmesi ister).

```bash
az login
RG=rg-az900-c17; LOC=westeurope
az group create --name $RG --location $LOC

# 1) VNet-A: two subnets (app + private endpoint)
az network vnet create --resource-group $RG --name vnet-a --address-prefixes 10.1.0.0/16 \
  --subnet-name snet-app --subnet-prefixes 10.1.1.0/24
az network vnet subnet create --resource-group $RG --vnet-name vnet-a --name snet-pe --address-prefixes 10.1.2.0/24

# 2) VNet-B: non-overlapping address space
az network vnet create --resource-group $RG --name vnet-b --address-prefixes 10.2.0.0/16 \
  --subnet-name snet-app --subnet-prefixes 10.2.1.0/24

# 3) Peering — defined in BOTH directions
az network vnet peering create --resource-group $RG --name a-to-b --vnet-name vnet-a --remote-vnet vnet-b --allow-vnet-access
az network vnet peering create --resource-group $RG --name b-to-a --vnet-name vnet-b --remote-vnet vnet-a --allow-vnet-access
az network vnet peering list --resource-group $RG --vnet-name vnet-a --query "[].{name:name,state:peeringState}" --output table
#   peeringState should be Connected (stays "Initiated" if only one direction is defined)

# 4) Storage account (starts with its public endpoint)
ST=stc17$RANDOM
az storage account create --resource-group $RG --name $ST --location $LOC --sku Standard_LRS --kind StorageV2
nslookup $ST.blob.core.windows.net              # on your local machine: resolves to a public IP

# 5) Private DNS zone + VNet link
az network private-dns zone create --resource-group $RG --name privatelink.blob.core.windows.net
az network private-dns link vnet create --resource-group $RG --zone-name privatelink.blob.core.windows.net \
  --name link-vnet-a --virtual-network vnet-a --registration-enabled false

# 6) Private endpoint (for the blob sub-resource) + DNS zone group (writes the A record automatically)
az network private-endpoint create --resource-group $RG --name pe-$ST \
  --vnet-name vnet-a --subnet snet-pe \
  --private-connection-resource-id $(az storage account show -g $RG -n $ST --query id -o tsv) \
  --group-id blob --connection-name pe-$ST-conn
az network private-endpoint dns-zone-group create --resource-group $RG --endpoint-name pe-$ST \
  --name default --private-dns-zone privatelink.blob.core.windows.net --zone-name blob

# 7) Disable public access
az storage account update --resource-group $RG --name $ST --public-network-access Disabled

# 8) Test: a small VM inside VNet-A; run the command via run-command (no SSH needed).
#    run-command uses the VM agent's outbound connection to Azure, so we create the VM with
#    default settings (public IP + NSG). The request to storage stays inside the VNet via the private endpoint.
az vm create --resource-group $RG --name vm-test --image Ubuntu2404 --size Standard_B1s \
  --vnet-name vnet-a --subnet snet-app \
  --admin-username azureuser --generate-ssh-keys
az vm run-command invoke --resource-group $RG --name vm-test --command-id RunShellScript \
  --scripts "nslookup $ST.blob.core.windows.net; curl -s -o /dev/null -w '%{http_code}\n' https://$ST.blob.core.windows.net/"
#   From inside the VNet: 10.1.2.x (private IP) — from outside: the request is rejected because public access is disabled

# 9) Cleanup — mandatory
az group delete --name $RG --yes --no-wait
```

<details><summary>Beklenen gözlemler ve açıklamaları</summary>

| Adım | Gözlem | Kavram |
|---|---|---|
| 3 | Yalnızca `a-to-b` tanımlıyken durum `Initiated`; `b-to-a` eklenince `Connected` | Peering iki yönlü tanımlanır |
| 4 | Storage adı public bir IP'ye çözülür | Public endpoint |
| 6–8 | VM'den aynı ad `privatelink` üzerinden **10.1.2.x**'e çözülür | Private endpoint + private DNS zone |
| 7–8 | Yerel makineden blob isteği reddedilir (HTTP 403 / erişim yok), VM'den ağ erişimi vardır | Public erişim kapatıldı; trafik VNet içinde |
| (düşünce deneyi) | VNet-B'deki bir VM de private endpoint'e erişebilir mi? | Peering'li VNet'ten özel IP'ye erişim mümkündür, ama DNS zone'un VNet-B'ye de bağlanması gerekir |

`curl` çıktısındaki HTTP kodu kimlik doğrulama olmadığı için 4xx olabilir; önemli olan isteğin **özel IP üzerinden ulaşmasıdır**.
</details>

---

### 4. Gerçek dünya senaryosu

Türkiye'de üç fabrikası ve İstanbul'da bir genel merkezi olan bir üretim şirketi Azure'a geçiyor:

1. **Adres planı:** on-prem `10.0.0.0/12` kullanıyor; Azure VNet'leri çakışmasın diye `10.100.0.0/16` (üretim) ve `10.101.0.0/16` (test) seçiliyor. Üretim VNet'i web, uygulama ve veri **subnet**'lerine bölünüyor; her subnet'e bir **NSG**.
2. **Genel merkez ↔ Azure:** ERP verisi büyük ve gecikmeye duyarlı → **ExpressRoute** (bir operatör üzerinden). Aynı bağlantının yedeği olarak **site-to-site VPN Gateway**.
3. **Fabrikalar ↔ Azure:** küçük veri hacmi → **site-to-site VPN**. Fabrikaları birbirine bağlamak için ayrıca **ExpressRoute Global Reach** değerlendiriliyor.
4. **Evden çalışan mühendisler:** **point-to-site VPN** ile yalnızca test VNet'ine.
5. **Üretim ↔ test VNet'i:** **VNet peering** (internete çıkmadan); test VNet'inin, üretimin peering yaptığı paylaşılan servis VNet'ine erişemediği fark ediliyor — peering **geçişli değil** — ayrı bir peering ekleniyor.
6. **Veritabanı ve storage:** Azure SQL Database ve storage account'lara **private endpoint**; public erişim kapatılıyor; isim çözümleme için **private DNS zone**'lar hem Azure VNet'lerine hem (DNS forwarder ile) on-prem'e bağlanıyor.
7. **Şirketin public sitesi:** `contoso.com.tr` domain'i registrar'dan alınmış; DNS kayıtları **Azure DNS**'te barındırılıyor, zone'a **CanNotDelete lock** (→ C1.12).

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Bağlantı seçimi senaryoları, VNet/peering/DNS yetenekleri hakkında evet/hayır tabloları ve "public mi private endpoint mi" ayrımı.

| Senaryodaki ifade | Cevap |
|---|---|
| "On-prem ile Azure arasında **internetten geçmeyen**, özel, yüksek bant genişlikli bağlantı" | **ExpressRoute** |
| "On-prem ile Azure arasında **şifreli**, **internet üzerinden**, düşük maliyetli bağlantı" | **VPN Gateway (site-to-site)** |
| "Tek bir geliştiricinin dizüstü bilgisayarından VNet'e bağlanmak" | **Point-to-site VPN** |
| "İki Azure VNet'ini **Microsoft omurgası** üzerinden bağlamak" | **VNet peering** |
| "Farklı region'lardaki iki VNet'i bağlamak" | **Global VNet peering** |
| "Kaynakları ağ içinde gruplayıp ayrı kurallar uygulamak" | **Subnet** (+ NSG) |
| "DNS kayıtlarını Azure'da barındırmak, RBAC ile yönetmek" | **Azure DNS** |
| "VNet içinde özel isim çözümleme" | **Private DNS zone** |
| "PaaS servisine **özel IP** ile, trafik Microsoft ağında kalarak erişmek; public erişimi kapatmak" | **Private endpoint** (Private Link) |
| "Şube ağlarını Microsoft omurgasıyla birbirine bağlamak" | **ExpressRoute Global Reach** |

**Tuzaklar:**
- "Peering transitive'tir" → **Hayır**.
- "Azure DNS ile domain satın alabilirsiniz" → **Hayır** (barındırır).
- "ExpressRoute trafiği public internet üzerinden şifreli olarak taşır" → **Hayır** (özel bağlantı; o tanım VPN'indir).
- "VPN Gateway, iki Azure VNet'ini bağlamanın **tek** yoludur" → **Hayır** (peering de var; VNet-to-VNet VPN de bir seçenek).
- "Bir VNet birden çok region'a yayılır" → **Hayır** (tek region; birden çok **zone**'a yayılır).
- "VNet'ler arası peering için adres alanları çakışabilir" → **Hayır**.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| VNet adres alanını on-prem ile çakışacak şekilde seçmek | Baştan bir IP adres planı; çakışan ağlar peering/VPN ile bağlanamaz |
| Peering'i tek yönlü tanımlayıp "çalışmıyor" demek | İki yönlü tanımlanır (`Connected` durumu) |
| A↔B↔C peering'iyle A'nın C'ye ulaşmasını beklemek | Peering geçişli değildir; A↔C peering'i veya hub'da router/firewall gerekir |
| "ExpressRoute = çok hızlı VPN" | VPN internet üzerinden şifreli tüneldir; ExpressRoute internete çıkmayan özel hattır |
| Private endpoint kurup DNS'i unutmak | Servis adı hâlâ public IP'ye çözülür; private DNS zone ve VNet bağlantısı gerekir |
| Private endpoint ekleyip public erişimi açık bırakmak | Amaç izolasyonsa public network access **kapatılmalı** |
| Laboratuvarda VPN Gateway kurup unutmak | Gateway saatlik ücretlenir ve kurulumu uzun sürer; deneme bitince sil |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| VPN Gateway (S2S) | Hızlı kurulum, düşük maliyet, şifreli | İnternete bağlı değişken gecikme/bant genişliği; gateway SKU'su verimi sınırlar |
| ExpressRoute | Yüksek, öngörülebilir performans; internete çıkmama; M365 erişimi | Yüksek maliyet, operatör sözleşmesi, kurulum süresi; şifreleme ayrıca planlanmalı |
| ExpressRoute + VPN yedeği | Yüksek erişilebilirlik | İki bağlantının maliyeti ve yönetimi |
| VNet peering | Düşük gecikme, basit, gateway yok | Geçişli değil — çok VNet'te peering sayısı katlanır (hub-spoke tasarımı → S8.3); veri transferi ücreti |
| Private endpoint | Public erişimi kapatma, veri sızıntısı riskini azaltma | Saatlik endpoint + işlenen veri ücreti; DNS yönetimi karmaşıklığı |
| Public endpoint + firewall kuralları | Basit, ek maliyet yok | Adres internete açık; yanlış yapılandırma riski |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure compute and networking services](https://learn.microsoft.com/en-us/training/modules/describe-azure-compute-networking-services/)
- [What is Azure Virtual Network?](https://learn.microsoft.com/en-us/azure/virtual-network/virtual-networks-overview) · [Virtual network peering](https://learn.microsoft.com/en-us/azure/virtual-network/virtual-network-peering-overview)
- [What is Azure DNS?](https://learn.microsoft.com/en-us/azure/dns/dns-overview) · [Azure Private DNS](https://learn.microsoft.com/en-us/azure/dns/private-dns-overview)
- [What is VPN Gateway?](https://learn.microsoft.com/en-us/azure/vpn-gateway/vpn-gateway-about-vpngateways) · [VPN Gateway — What's new (SKU değişiklikleri)](https://learn.microsoft.com/en-us/azure/vpn-gateway/whats-new)
- [What is ExpressRoute?](https://learn.microsoft.com/en-us/azure/expressroute/expressroute-introduction) · [ExpressRoute Global Reach](https://learn.microsoft.com/en-us/azure/expressroute/expressroute-global-reach)
- [What is a private endpoint?](https://learn.microsoft.com/en-us/azure/private-link/private-endpoint-overview) · [Private endpoint DNS configuration](https://learn.microsoft.com/en-us/azure/private-link/private-endpoint-dns)
- [`az network vnet peering`](https://learn.microsoft.com/en-us/cli/azure/network/vnet/peering) · [`az network private-endpoint`](https://learn.microsoft.com/en-us/cli/azure/network/private-endpoint)


### 📝 Bölüm 2.4 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir şirket, genel merkezini Azure'a **public internetten geçmeyen**, özel ve yüksek bant genişlikli bir bağlantıyla bağlamak istiyor. Hangi servisi kullanmalıdır?

- A) VPN Gateway (site-to-site)
- B) ExpressRoute
- C) VNet peering
- D) Azure DNS

<details><summary>Cevap</summary>

**B.** ExpressRoute, bağlantı sağlayıcısı üzerinden kurulan, internete çıkmayan özel bağlantıdır. VPN Gateway şifrelidir ama trafiği public internet üzerinden taşır.
</details>

#### Soru 2: Tek seçim
Küçük bir ofis, Azure'daki VNet'ine **düşük maliyetle** ve **şifreli** bir bağlantı istiyor; bağlantının internet üzerinden geçmesi sorun değil. Hangisi **en uygundur**?

- A) ExpressRoute Direct
- B) Site-to-site VPN (VPN Gateway)
- C) ExpressRoute Global Reach
- D) Private endpoint

<details><summary>Cevap</summary>

**B.** Site-to-site VPN, internet üzerinden IPsec/IKE ile şifreli tünel kurar; hızlı ve ucuzdur.
</details>

#### Soru 3: Tek seçim
Bir geliştirici, evdeki dizüstü bilgisayarından bir Azure VNet'indeki test VM'lerine güvenli olarak bağlanmak istiyor. Hangi VPN bağlantı türü **en uygundur**?

- A) Site-to-site
- B) Point-to-site
- C) VNet-to-VNet
- D) ExpressRoute

<details><summary>Cevap</summary>

**B.** Point-to-site, tek bir istemciyi VNet'e bağlar; site-to-site bütün bir ağı (ofis/datacenter) bağlar.
</details>

#### Soru 4: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) VNet peering ile bağlanan VNet'ler arasındaki trafik Microsoft omurga ağında kalır. | ☐ | ☐ |
| b) VNet-A, VNet-B ile; VNet-B de VNet-C ile peering yaptıysa, VNet-A ile VNet-C doğrudan iletişim kurabilir. | ☐ | ☐ |
| c) Farklı region'lardaki iki VNet peering ile bağlanabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Peering internete çıkmaz; **geçişli değildir** (A↔C ayrı peering ister); farklı region'lar için **global peering** vardır.
</details>

#### Soru 5: Tek seçim
Aynı subscription'daki iki Azure VNet'ini, **gateway kurmadan**, düşük gecikmeyle birbirine bağlamanın en basit yolu hangisidir?

- A) Site-to-site VPN
- B) ExpressRoute
- C) VNet peering
- D) Public endpoint

<details><summary>Cevap</summary>

**C.** Peering iki VNet'i doğrudan Microsoft omurgası üzerinden bağlar; gateway gerekmez. VNet-to-VNet VPN de mümkündür ama gateway gerektirir ve daha yavaştır.
</details>

#### Soru 6: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Bir Azure VNet birden çok region'a yayılabilir. | ☐ | ☐ |
| b) Bir VNet, adres alanını subnet'lere bölerek kaynakları segmentlere ayırmaya olanak tanır. | ☐ | ☐ |
| c) Peering yapılacak iki VNet'in adres alanları çakışabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** VNet tek bir region'dadır (o region'ın zone'larına yayılır); peering için adres alanları çakışmamalıdır.
</details>

#### Soru 7: Çoklu seçim (iki doğru cevap)
Azure Virtual Network'ün **hangi iki** yeteneği resmî tanımda yer alır?

- A) Ağ trafiğini NSG'lerle filtreleme
- B) Domain adı satın alma
- C) On-prem ağlarla iletişim (VPN/ExpressRoute)
- D) Uygulama kodunu derleme

<details><summary>Cevap</summary>

**A ve C.** VNet: izolasyon/segmentasyon, internet iletişimi, Azure kaynakları arası iletişim, on-prem ile iletişim, trafik yönlendirme, trafik filtreleme. Domain satın almak VNet'in (veya Azure DNS'in) işi değildir.
</details>

#### Soru 8: Tek seçim
Bir şirket, `contoso.com` domain'inin DNS kayıtlarını Azure'da barındırmak ve kayıtları kimin değiştirebileceğini **Azure RBAC** ile yönetmek istiyor. Hangi servisi kullanmalıdır?

- A) Azure DNS
- B) Azure Front Door
- C) VPN Gateway
- D) Azure Firewall

<details><summary>Cevap</summary>

**A.** Azure DNS, DNS zone'larını barındırır; RBAC, activity log ve resource lock ile yönetilir.
</details>

#### Soru 9: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure DNS kullanarak yeni bir domain adı satın alabilirsiniz. | ☐ | ☐ |
| b) Private DNS zone, VNet içinde özel isim çözümlemesi sağlar. | ☐ | ☐ |
| c) Azure DNS kayıtları Azure portal ve Azure CLI ile yönetilebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Azure DNS domain **barındırır**, satmaz; domain bir registrar'dan alınır ve NS kayıtları Azure DNS'e yönlendirilir.
</details>

#### Soru 10: Senaryo
Bir uygulama ekibi, Azure SQL Database'e yalnızca VNet içinden, **özel bir IP adresi** üzerinden erişilmesini ve veritabanının **public erişiminin tamamen kapatılmasını** istiyor. Ne kullanmalıdır?

- A) Public endpoint
- B) Private endpoint (Azure Private Link)
- C) ExpressRoute Global Reach
- D) Azure DNS public zone

<details><summary>Cevap</summary>

**B.** Private endpoint, PaaS servisine VNet'ten özel IP ile erişim sağlar; trafik Microsoft ağında kalır ve public erişim kapatılabilir.
</details>

#### Soru 11: Tek seçim
Bir storage account'a private endpoint eklendi, fakat VNet içindeki VM'ler storage adını hâlâ **public IP**'ye çözüyor. En olası eksik nedir?

- A) Storage account'un redundancy türü
- B) Private DNS zone'un (`privatelink.blob.core.windows.net`) oluşturulup VNet'e bağlanması
- C) VPN Gateway kurulması
- D) VM'in boyutunun büyütülmesi

<details><summary>Cevap</summary>

**B.** Private endpoint'in isimle çalışması için servis adının VNet içinde özel IP'ye çözülmesi gerekir; bunu private DNS zone ve VNet bağlantısı sağlar.
</details>

#### Soru 12: Eşleştirme
Her ihtiyacı bağlantı seçeneğiyle eşleştirin: **VNet peering · Site-to-site VPN · ExpressRoute · Point-to-site VPN**

1. Şirket datacenter'ı ↔ Azure, internetten geçmeden, öngörülebilir gecikme
2. Ofis ağı ↔ Azure, internet üzerinden şifreli tünel
3. Azure VNet ↔ Azure VNet, Microsoft omurgasında
4. Tek bir kullanıcının bilgisayarı ↔ Azure VNet

<details><summary>Cevap</summary>

**1 → ExpressRoute · 2 → Site-to-site VPN · 3 → VNet peering · 4 → Point-to-site VPN.**
</details>

#### Soru 13: Tek seçim
VPN Gateway'de **varsayılan** yüksek erişilebilirlik yapılandırması hangisidir?

- A) Active/active
- B) Active/standby
- C) Zone-redundant (her zaman)
- D) Yüksek erişilebilirlik yoktur

<details><summary>Cevap</summary>

**B.** Varsayılan olarak gateway iki instance ile **active/standby** çalışır; bakım veya arızada yedek instance kısa bir kesintiyle devralır. Active/active ve zone-redundant gateway'ler ek yapılandırma seçenekleridir.
</details>

#### Soru 14: Tek seçim
Bir şirketin farklı ülkelerdeki iki şubesi ayrı ExpressRoute devreleriyle Azure'a bağlı. Şubeler arası trafiği de Microsoft omurgası üzerinden taşımak istiyorlar. Hangi özellik bunu sağlar?

- A) ExpressRoute Global Reach
- B) Point-to-site VPN
- C) Private DNS zone
- D) Azure Load Balancer

<details><summary>Cevap</summary>

**A.** Global Reach, ExpressRoute devreleri üzerinden on-prem siteleri birbirine bağlar.
</details>

#### Soru 15: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) ExpressRoute ile Microsoft 365 gibi Microsoft bulut servislerine de bağlanılabilir. | ☐ | ☐ |
| b) VPN Gateway, ExpressRoute bağlantısı için yedek (failover) olarak kullanılabilir. | ☐ | ☐ |
| c) ExpressRoute trafiği public internet üzerinden şifreli bir tünelle taşır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** ExpressRoute özel bir bağlantıdır; trafik public internete çıkmaz. "İnternet üzerinden şifreli tünel" VPN Gateway'in tanımıdır.
</details>

#### Soru 16: Tek seçim
Bir VNet'te web sunucularını ve veritabanı sunucularını ayrı gruplara ayırıp her gruba farklı ağ güvenlik kuralları uygulamak istiyorsunuz. Ne kullanmalısınız?

- A) Ayrı subnet'ler ve her subnet'e bir NSG
- B) Ayrı Azure DNS zone'ları
- C) ExpressRoute devreleri
- D) Ayrı resource group'lar

<details><summary>Cevap</summary>

**A.** Subnet'ler ağ segmentasyonu sağlar; NSG'ler subnet (veya NIC) düzeyinde izin/engel kurallarıdır. Resource group ağ izolasyonu sağlamaz (→ C1.4).
</details>

#### Soru 17: Tek seçim
Aşağıdakilerden hangisi bir **public endpoint**'tir?

- A) Bir storage account'un `mystorage.blob.core.windows.net` adresinin internetten erişilebilir olması
- B) VNet içindeki `10.1.2.5` özel IP'li private endpoint
- C) Bir VM'in yalnızca subnet içinden erişilebilen NIC'i
- D) Private DNS zone'daki bir A kaydı

<details><summary>Cevap</summary>

**A.** Public endpoint, kaynağa internetten erişilen genel adrestir. B, C ve D özel ağ içi adreslemedir.
</details>

#### Soru 18: Senaryo
Bir şirket üç Azure VNet'ini (hub, spoke-1, spoke-2) birbirine bağlıyor: hub↔spoke-1 ve hub↔spoke-2 peering'leri var. spoke-1'deki VM'ler spoke-2'deki VM'lere ulaşamıyor. Neden?

- A) VNet peering yalnızca farklı region'lar arasında çalışır
- B) VNet peering geçişli değildir; spoke-1↔spoke-2 için ayrı peering ya da hub'da yönlendirme (ör. firewall/router) gerekir
- C) Spoke VNet'lerin DNS zone'u yoktur
- D) Peering için ExpressRoute zorunludur

<details><summary>Cevap</summary>

**B.** Peering transitive değildir. Çözüm: doğrudan spoke-1↔spoke-2 peering'i veya hub'daki bir NVA/Azure Firewall üzerinden route table ile yönlendirme (hub-spoke tasarımı → S8.3).
</details>

---

**Puanlama:** 16-18 → hazırsın · 12-15 → README 3.5'teki "VPN Gateway vs ExpressRoute" tablosunu ve peering kurallarını tekrar et · < 12 → konuyu baştan çalış ve 3.7'deki laboratuvarı uygula.


---

## 2.5 Azure Storage: Blob, Files, Redundancy, Tiering

> **Sınav Alanı:** Describe Azure architecture and services (%35-40) · **Konu Kodu:** C2.5

### 1. Ne?

**Azure Storage**, Microsoft'un yönetilen bulut depolama platformudur: dayanıklı (birden çok kopya), ölçeklenebilir, güvenli (varsayılan olarak şifreli) ve HTTP/HTTPS ile her yerden erişilebilir. Tüm servisler bir **storage account** altında yaşar.

Bu konu sınavın beş sorusunu cevaplar:

| Soru | Kavram |
|------|--------|
| Ne tür veri, hangi servise? | Blob · Files · Queue · Table · Disks |
| Veri hangi arızaya dayanmalı? | **Redundancy**: LRS, ZRS, GRS, GZRS, RA-GRS, RA-GZRS ⭐ |
| Ne sıklıkla erişilecek? | **Access tier**: Hot, Cool, Cold, Archive ⭐ |
| Dosyaları nasıl taşırım/senkronlarım? | AzCopy · Storage Explorer · Azure File Sync |
| Sunucuları/büyük veriyi nasıl taşırım? | Azure Migrate · Azure Data Box |


---

### 2. Neden?

- **Sınav:** Storage, "Azure architecture and services" alanının (%35-40) en çok soru üreten konularındandır; özellikle **redundancy ↔ senaryo** ve **tier ↔ erişim sıklığı** eşleştirmeleri hemen her sınavda çıkar.
- **Gerçek işte:** Yanlış redundancy seçimi ya felakette veri kaybı (LRS'te datacenter felaketi) ya da gereksiz maliyettir (her şeyi RA-GZRS yapmak). Yanlış tier ise ya yüksek depolama faturası (yedekleri Hot'ta tutmak) ya da sürpriz erişim/rehydrate ücretleri ve saatlerce bekleme (sık okunan veriyi Archive'a koymak) demektir.
- **Taşıma:** Datacenter'dan buluta geçişte ilk soru "terabaytlarca veriyi nasıl taşıyacağız?" sorusudur; ağ, cihaz ve senkron seçeneklerini bilmek taşıma planının temelidir.

---

### 3. Nasıl?

#### 3.1 Storage account ve storage servisleri (`Bölüm 01`)

**Storage account:** Azure genelinde **benzersiz** bir ad alanı (3-24 karakter, küçük harf + rakam); her servis bu adla bir endpoint alır:

| Servis | Ne saklar? | Endpoint | Tipik kullanım |
|---|---|---|---|
| **Blob** | Nesne — yapılandırılmamış veri | `https://<ad>.blob.core.windows.net` | Görsel, video, yedek, log, statik web sitesi, veri gölü |
| **Azure Files** | Tam yönetilen **SMB/NFS dosya paylaşımı** | `https://<ad>.file.core.windows.net` | Paylaşılan klasör, lift-and-shift uygulamalar |
| **Queue** | Basit mesaj kuyruğu (mesaj ≤ 64 KB) | `https://<ad>.queue.core.windows.net` | Bileşenler arası asenkron iş listesi |
| **Table** | NoSQL anahtar-değer (şemasız) | `https://<ad>.table.core.windows.net` | Basit yapılandırılmış veri, meta veri |
| **Disks** (managed disks) | VM'e bağlanan **blok** depolama | — (VM'e bağlı kaynak) | VM işletim sistemi ve veri diskleri (→ C1.5) |

**Storage account türleri:**

| Tür | Desteklediği servisler | Redundancy | Ne zaman? |
|---|---|---|---|
| **Standard general-purpose v2** | Blob, Files, Queue, Table | LRS, ZRS, GRS, RA-GRS, GZRS, RA-GZRS | **Çoğu senaryo** — varsayılan seçim |
| **Premium block blobs** | Block ve append blob | LRS, ZRS | Düşük gecikme, yüksek işlem hızı, küçük nesneler |
| **Premium file shares** | Azure Files | LRS, ZRS | Kurumsal/yüksek performanslı dosya paylaşımı |
| **Premium page blobs** | Page blob | LRS, ZRS | Yüksek performanslı page blob senaryoları |

> Premium türler SSD tabanlıdır ve **coğrafi** (GRS/GZRS) redundancy sunmaz.

**Blob hiyerarşisi:** storage account → **container** → **blob**. Blob türleri: **block** (dosyalar; en yaygın), **append** (sona ekleme — log), **page** (rastgele okuma/yazma — VM diskleri).

#### 3.2 Redundancy ⭐ (`Bölüm 02`)

Azure Storage veriyi her zaman **birden çok kopya** olarak tutar; seçenek, kopyaların **nereye** konduğunu belirler:

| Seçenek | Kopyalar | Disk/raf arızası | Datacenter/zone arızası | Bölgesel felaket | Felakette okuma |
|---|---|---|---|---|---|
| **LRS** (locally redundant) | Birincil bölgede **tek datacenter** içinde 3 kopya | ✔ | ✘ | ✘ | ✘ |
| **ZRS** (zone-redundant) | Birincil bölgede **3 availability zone**'a | ✔ | ✔ | ✘ | ✘ |
| **GRS** (geo-redundant) | LRS + **eşleşik bölgede** LRS (6 kopya) | ✔ | — (failover gerekir) | ✔ (failover ile) | Failover sonrası |
| **GZRS** (geo-zone-redundant) | ZRS + eşleşik bölgede LRS | ✔ | ✔ | ✔ (failover ile) | Failover sonrası |
| **RA-GRS** | GRS + ikincilden **her zaman okuma** | ✔ | — | ✔ | ✔ ikincilden |
| **RA-GZRS** | GZRS + ikincilden **her zaman okuma** | ✔ | ✔ | ✔ | ✔ ikincilden |

- İkincil bölge her zaman birincilin **region pair**'idir (→ C1.4); ikincile replikasyon **asenkrondur** (felakette son birkaç saniyenin/dakikanın verisi kaybolabilir).
- GRS/GZRS'te ikincil bölge **normalde okunamaz**; ancak **failover** (müşteri başlatır veya Microsoft) sonrası yeni birincil olur. Sürekli okuma istiyorsan **RA-** (read-access) seçeneği gerekir.
- Maliyet sırası (kabaca): LRS < ZRS < GRS < RA-GRS ≈ GZRS < RA-GZRS.

#### 3.3 Blob Storage ve access tier'ları ⭐ (`Bölüm 03`)

| Tier | Kullanım | Önerilen en az süre | Erişim | Maliyet dengesi |
|---|---|---|---|---|
| **Hot** | Sık erişilen veri (aktif içerik) | — | Anında | Depolama en pahalı, erişim en ucuz |
| **Cool** | Seyrek erişim (kısa vadeli yedek, eski içerik) | **30 gün** | Anında | Depolama ucuz, erişim daha pahalı |
| **Cold** | Nadir erişim ama hızlı okunmalı | **90 gün** | Anında | Depolama daha ucuz, erişim daha pahalı |
| **Archive** | Çok nadir (uzun dönem arşiv, uyumluluk kayıtları) | **180 gün** | **Çevrimdışı** — önce **rehydrate** (saatler sürebilir) | Depolama en ucuz, erişim en pahalı |

- **Archive'daki blob okunamaz:** önce Hot/Cool/Cold'a taşınması (rehydrate) gerekir; standart öncelikte **saatler** (en çok ~15 saat), yüksek öncelikte daha kısa sürer ve daha pahalıdır.
- En az süre dolmadan silmek veya tier değiştirmek **erken silme ücreti** doğurur.
- Tier **blob düzeyinde** ayarlanabilir; hesap düzeyinde bir varsayılan tier vardır (Archive varsayılan olamaz).
- **Lifecycle management:** kurallarla otomatik geçiş — "30 gün değişmeyen blob → Cool, 180 gün → Archive, 7 yıl → sil".

`Section03` 1 TB veri için üç farklı okuma yoğunluğunda aylık maliyeti hesaplar (örnek fiyatlar): az okunan veride Archive, orta yoğunlukta Cool, çok okunan veride Hot kazanır — **depolama ucuzladıkça erişim pahalılaşır**.

#### 3.4 Azure Files ve Azure File Sync

- **Azure Files:** bulutta tam yönetilen **SMB** (ve Premium'da **NFS**) dosya paylaşımı; Windows, Linux ve macOS'tan **ağ sürücüsü olarak bağlanır**; birden çok VM/kullanıcı aynı paylaşımı kullanır. Kullanım: dosya sunucusunu buluta taşımak, lift-and-shift uygulamaların paylaşılan klasörleri, paylaşılan yapılandırma/log klasörleri.
- **Azure File Sync:** on-prem **Windows Server**'ı Azure Files paylaşımının **önbelleği** yapar:
  - Kullanıcılar yerel sunucudan hızlı erişmeye devam eder; veri Azure Files ile **iki yönlü senkron** kalır.
  - **Cloud tiering:** seyrek kullanılan dosyalar yalnızca bulutta tutulur, yerelde yer açılır (dosya adı görünür, açılınca indirilir).
  - Birden çok şubede aynı paylaşım; sunucu arızalanınca yeni sunucuya hızlı kurtarma.

#### 3.5 Dosya taşıma araçları (`Bölüm 04`)

| Araç | Ne? | Ne zaman? |
|---|---|---|
| **AzCopy** | Blob ve dosya kopyalamak için **komut satırı** aracı | Betikli/toplu kopyalama, `sync`, storage account'lar ve bulutlar (ör. S3) arası kopya |
| **Azure Storage Explorer** | Blob, Files, Queue, Table, disk yönetimi için **grafik** masaüstü uygulaması (Windows/macOS/Linux; arka planda AzCopy) | Elle dosya yükleme/indirme, göz atma, erişim yönetimi |
| **Azure File Sync** | Windows Server ↔ Azure Files **sürekli, iki yönlü senkron** | Şube dosya sunucuları, hybrid dosya paylaşımı |

#### 3.6 Migration seçenekleri

- **Azure Migrate:** buluta taşıma için **merkezi hub**: on-prem ortamı **keşfet** (discovery) → **değerlendir** (boyutlandırma, uyumluluk, tahmini maliyet) → **taşı**. Sunucular, veritabanları, web uygulamaları ve sanal masaüstleri için entegre araçlar içerir: *Discovery and assessment*, *Migration and modernization* (sunucu taşıma), *Data Migration Assistant*, *Azure Database Migration Service*, *Web app migration assistant*, Data Box. Ayrıca on-prem ile Azure maliyetini karşılaştıran **business case** üretir (→ C1.11).
- **Azure Data Box:** veriyi **fiziksel cihazla** taşıma — Microsoft güvenli bir cihaz gönderir, veriyi yerelde kopyalarsın, cihaz Azure datacenter'ına döner ve veri storage account'a yüklenir; cihaz sonra NIST standartlarına göre güvenle silinir. Hem **içe** (on-prem → Azure) hem **dışa** (Azure → on-prem) aktarım.
  - Ne zaman: **çok büyük veri** (onlarca TB – petabayt), **ağ bağlantısı yok/yavaş/pahalı**, tek seferlik toplu taşıma, ilk yükleme sonrası ağdan artımlı senkron, felaket kurtarma için toplu geri yükleme.
  - Ailesi *(bağlam — kapasiteler değişir)*: **Data Box Disk** (SSD diskler, küçük-orta veri), **Data Box** (yeni nesil cihazlar ~120 TB ve ~525 TB kullanılabilir kapasite; eski 80 TB'lık cihaz bölge bölge kullanımdan kalkıyor), **Data Box Heavy** emekliye ayrıldı. Sınav kapasite değil **"ağ yetersiz + büyük veri → Data Box"** eşleştirmesini sorar.

Kaba hesap: 100 Mbps'lik bir hatla 40 TB, hattın tamamı bu işe ayrılsa bile **~37 gün** sürer; Data Box birkaç günlük kargo ve kopyalama süresiyle biter.

#### 3.7 Pratik — CLI ve AzCopy laboratuvarı

Önce bütçe alarmının kurulu olduğundan emin ol (→ C1.1). Küçük bir Standard LRS hesabı ve birkaç KB'lık dosya **kuruşluk** maliyettir; laboratuvar sonunda her şey silinir.

```bash
RG=rg-az900-c18
ST=stc18$RANDOM                         # must be unique across Azure
az group create --name $RG --location westeurope

# 1) Storage account (Standard general-purpose v2, LRS)
az storage account create --resource-group $RG --name $ST \
  --location westeurope --sku Standard_LRS --kind StorageV2 --access-tier Hot
az storage account show --resource-group $RG --name $ST \
  --query "{sku:sku.name, kind:kind, tier:accessTier, blob:primaryEndpoints.blob}" -o table

# 2) Container + blob upload + change tier (--auth-mode key: the CLI fetches the account key itself)
az storage container create --account-name $ST --name backups --auth-mode key
echo "sample backup" > backup.txt
az storage blob upload --account-name $ST --container-name backups --name backup.txt --file backup.txt --auth-mode key
az storage blob set-tier --account-name $ST --container-name backups --name backup.txt --tier Cool --auth-mode key
az storage blob list --account-name $ST --container-name backups --auth-mode key \
  --query "[].{name:name, tier:properties.blobTier}" -o table

# 3) AzCopy: copy a local folder to the container (with a short-lived SAS)
mkdir -p reports && for i in 1 2 3; do echo "report $i" > reports/r$i.txt; done
SAS=$(az storage container generate-sas --account-name $ST --name backups \
  --permissions acdlrw --expiry $(date -u -d '+1 hour' +%Y-%m-%dT%H:%MZ) --auth-mode key -o tsv)
azcopy copy "./reports" "https://$ST.blob.core.windows.net/backups?$SAS" --recursive
azcopy list "https://$ST.blob.core.windows.net/backups?$SAS"

# 4) Azure Files share (5 GiB quota)
az storage share-rm create --resource-group $RG --storage-account $ST --name myshare --quota 5
az storage share-rm list --resource-group $RG --storage-account $ST -o table
# Mounting: the share's "Connect" tab in the portal generates a ready-made script for Windows/Linux/macOS
# (SMB port 445 is blocked on many home/corporate networks — if you cannot connect, this may be why).

# 5) Cleanup
az group delete --name $RG --yes --no-wait
```

> **Alıştırma — redundancy ve maliyet.** Pricing Calculator'da aynı Standard general-purpose v2 hesabını (1 TB, Hot, aylık 1 milyon okuma) **LRS** ve **GZRS** olarak fiyatla. (a) Fark yaklaşık kaç kat? (b) Hangi senaryo için GZRS'in ek maliyeti haklıdır?
>
> <details><summary>Çözüm</summary>
>
> (a) Bölgeye göre değişir; GZRS depolama fiyatı LRS'in kabaca **2 katı** civarındadır (ikinci bölgedeki kopyalar + zone yayılımı; ayrıca coğrafi replikasyon için veri aktarım ücreti).
> (b) Verinin hem bir **zone arızasında kesintisiz** erişilebilir olması hem de **bölgesel felakette kaybolmaması** gerekiyorsa (ör. müşteri belgeleri, finansal kayıtlar). Yeniden üretilebilen veri (önbellek, geçici işlem çıktısı) için LRS yeterlidir.
> </details>

---

### 4. Gerçek dünya senaryosu

Bir sigorta şirketi on yıllık dosya arşivini ve dosya sunucularını buluta taşıyor:

| İhtiyaç | Karar | Neden |
|---|---|---|
| 300 TB taranmış poliçe arşivi, ofisin hattı 200 Mbps | **Azure Data Box** (birden çok cihaz) | Ağdan aylar sürer; cihazla birkaç hafta |
| Arşiv yalnızca denetimde okunuyor, 10 yıl saklanmalı | Blob **Archive** tier + lifecycle kuralı | En ucuz depolama; denetimden önce rehydrate için saatler yeterli |
| Son 1 yılın poliçeleri müşteri portalında gösteriliyor | Blob **Hot** (eski olanlar lifecycle ile **Cool**'a) | Sık okunan veri pahalı erişim ücretine girmemeli |
| Müşteri belgeleri hem zone arızasında erişilebilir hem bölgesel felakette korunmalı; felakette okunabilmeli | **RA-GZRS** | Zone + bölge koruması + ikincilden okuma |
| 12 şubedeki Windows dosya sunucuları | **Azure Files** + **Azure File Sync** (cloud tiering) | Şubelerde hızlı yerel erişim, tek kaynak bulutta, yer tasarrufu |
| Sunucuların hangisi nasıl taşınacak, maliyeti ne? | **Azure Migrate** keşif + değerlendirme | Boyutlandırma ve maliyet tahmini taşımadan önce |
| Gece raporlarının blob'a betikle yüklenmesi | **AzCopy** (`azcopy sync`) | Komut satırı, zamanlanmış iş |

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Neredeyse tamamen **senaryo → seçenek** eşleştirmesi: redundancy, tier, servis ve taşıma aracı. Evet/hayır tablolarında Archive ve redundancy kuralları sık çıkar.

| Senaryodaki ifade | Cevap |
|---|---|
| "En düşük maliyetli redundancy", "tek datacenter yeterli" | **LRS** |
| "Bir zone/datacenter düşse de erişim sürsün, veri bölgeden çıkmasın" | **ZRS** |
| "Bölgesel felakete dayan" | **GRS** (zone koruması da isteniyorsa **GZRS**) |
| "Birincil bölge çökse bile veriyi okuyabilmeliyim" | **RA-GRS / RA-GZRS** |
| "Sık erişilen veri" | **Hot** |
| "30 gün+ saklanan, seyrek erişilen yedek" | **Cool** |
| "Yıllarca saklanacak, nadiren ve gecikmeyle okunabilecek veri" | **Archive** |
| "Archive'daki dosyayı hemen okumak" | **Mümkün değil** — önce rehydrate |
| "Yapılandırılmamış veri: görsel, video, yedek" | **Blob** |
| "SMB ile bağlanan paylaşılan klasör" | **Azure Files** |
| "On-prem dosya sunucusu bulutla senkron, sık dosyalar yerelde" | **Azure File Sync** |
| "Komut satırından blob kopyalama" | **AzCopy** |
| "Grafik araçla blob yönetimi" | **Azure Storage Explorer** |
| "Taşıma öncesi keşif, değerlendirme, maliyet" | **Azure Migrate** |
| "Terabaytlarca veri, ağ yetersiz/yok" | **Azure Data Box** |

**Tuzaklar:**
- GRS ile RA-GRS: GRS'te ikincil bölge failover olmadan **okunamaz**.
- ZRS bölgesel felakete dayanmaz; GRS zone arızasında (failover yapılana kadar) kesintisiz erişim sağlamaz — ikisi birden isteniyorsa GZRS.
- File Sync ile AzCopy: sürekli senkron mu, tek seferlik kopya mı?
- Azure Migrate bir **platform/hub**; Data Box bir **cihaz**. "Sunucuları değerlendir" sorusunda Data Box yanlıştır.
- Storage account adı yalnızca küçük harf ve rakam; Azure genelinde benzersiz.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "LRS 3 kopya tutuyor, felakete dayanır" | 3 kopya **aynı datacenter**'da; datacenter felaketinde veri kaybolabilir |
| "GRS ile ikincil bölgeden her zaman okurum" | Yalnızca **RA-**GRS/RA-GZRS'te; GRS'te failover gerekir |
| Sık okunan veriyi "ucuz" diye Archive'a koymak | Archive çevrimdışı; rehydrate saatler sürer ve erişim/rehydrate pahalıdır |
| Cool/Archive'daki veriyi birkaç gün sonra silmek | Erken silme ücreti (30/90/180 gün kuralları) |
| "Azure Files = Blob'a klasör yüklemek" | Azure Files SMB/NFS ile **bağlanan** dosya sistemi; Blob nesne deposu |
| "File Sync tek seferlik taşıma aracı" | Sürekli, iki yönlü senkron + cloud tiering; tek seferlik kopya için AzCopy |
| "Data Box ile sunucu taşınır" | Data Box **veri** taşır; sunucu taşıma Azure Migrate'in işi |
| Premium hesapta GRS aramak | Premium türler yalnızca LRS/ZRS |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| LRS → ZRS | Zone arızasında kesintisiz erişim | Daha yüksek fiyat; tüm bölgelerde/türlerde yok |
| ZRS → GZRS / RA-GZRS | Bölgesel felakete dayanıklılık (+ ikincilden okuma) | ~2× depolama, coğrafi replikasyon trafiği, asenkron replikasyon (RPO > 0) |
| Hot → Cool/Cold | Düşük depolama maliyeti | Daha pahalı okuma/işlem, erken silme ücreti |
| Cool → Archive | En düşük depolama maliyeti | Çevrimdışı; saatlerce rehydrate, pahalı erişim |
| Standard → Premium | Düşük gecikme, yüksek IOPS | Daha yüksek GB fiyatı, coğrafi redundancy yok |
| Ağdan taşıma (AzCopy) → Data Box | Büyük veride çok daha hızlı, ağ yükü yok | Kargo ve kopyalama süresi, cihaz ücreti; sürekli senkron değil |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure storage services](https://learn.microsoft.com/en-us/training/modules/describe-azure-storage-services/)
- [Storage account overview](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-overview) · [Azure Storage redundancy](https://learn.microsoft.com/en-us/azure/storage/common/storage-redundancy)
- [Access tiers for blob data](https://learn.microsoft.com/en-us/azure/storage/blobs/access-tiers-overview) · [Blob rehydration from the archive tier](https://learn.microsoft.com/en-us/azure/storage/blobs/archive-rehydrate-overview)
- [What is Azure Files?](https://learn.microsoft.com/en-us/azure/storage/files/storage-files-introduction) · [Azure File Sync](https://learn.microsoft.com/en-us/azure/storage/file-sync/file-sync-introduction)
- [Get started with AzCopy](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azcopy-v10) · [Azure Storage Explorer](https://azure.microsoft.com/products/storage/storage-explorer/)
- [About Azure Migrate](https://learn.microsoft.com/en-us/azure/migrate/migrate-services-overview) · [Azure Data Box overview](https://learn.microsoft.com/en-us/azure/databox/data-box-overview) (yeni nesil 120/525 TB cihazlar) · [Data Box Heavy overview](https://learn.microsoft.com/en-us/azure/databox/data-box-heavy-overview) (emeklilik notu)


### 📝 Bölüm 2.5 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir uygulama kullanıcıların yüklediği fotoğraf ve videoları saklayacak. Hangi Azure Storage servisi **en uygundur**?

- A) Azure Files
- B) Blob Storage
- C) Queue Storage
- D) Table Storage

<details><summary>Cevap</summary>

**B.** Görsel, video, yedek gibi yapılandırılmamış veri (nesne) Blob Storage'ın işidir. Files dosya paylaşımı, Queue mesaj kuyruğu, Table anahtar-değer verisidir.
</details>

#### Soru 2: Tek seçim
Bir şirket, birden çok Windows ve Linux VM'in **SMB protokolüyle aynı anda bağlanabileceği** yönetilen bir dosya paylaşımına ihtiyaç duyuyor. Hangi servisi kullanmalıdır?

- A) Azure Files
- B) Blob Storage
- C) Azure Managed Disks
- D) Queue Storage

<details><summary>Cevap</summary>

**A.** Azure Files, SMB (ve NFS) ile bağlanan tam yönetilen dosya paylaşımıdır. Managed disk tek bir VM'e bağlı blok depolamadır.
</details>

#### Soru 3: Eşleştirme
Her ihtiyacı storage servisiyle eşleştirin: **Blob · Files · Queue · Table · Disks**

1. VM'in işletim sistemi diski
2. Siparişleri işleyen arka plan servisine iş öğeleri iletmek
3. Şemasız, anahtar-değer biçiminde cihaz meta verisi
4. Log ve yedek dosyalarını saklamak
5. Ağ sürücüsü olarak bağlanan paylaşılan klasör

<details><summary>Cevap</summary>

**1 → Disks · 2 → Queue · 3 → Table · 4 → Blob · 5 → Files.**
</details>

#### Soru 4: Tek seçim
Aşağıdaki redundancy seçeneklerinden hangisi verinin **üç kopyasını tek bir datacenter** içinde tutar?

- A) ZRS
- B) GRS
- C) LRS
- D) GZRS

<details><summary>Cevap</summary>

**C.** LRS (locally redundant storage) birincil bölgede tek datacenter içinde 3 kopya tutar; datacenter felaketine dayanmaz.
</details>

#### Soru 5: Senaryo
Bir şirketin verisi, regülasyon gereği **yalnızca bir bölgede** kalmalı; ancak bölgedeki bir datacenter (availability zone) tamamen çökerse de veriye **kesintisiz** erişilebilmeli. Hangi seçenek **en uygundur**?

- A) LRS
- B) ZRS
- C) GRS
- D) RA-GZRS

<details><summary>Cevap</summary>

**B.** ZRS veriyi tek bölgedeki üç availability zone'a yayar: zone arızasında erişim sürer ve veri bölgeden çıkmaz. GRS/RA-GZRS veriyi ikinci bir bölgeye kopyalar (gereksinime aykırı); LRS zone arızasına dayanmaz.
</details>

#### Soru 6: Senaryo
Bir şirket, birincil Azure bölgesi tamamen kullanılamaz hale gelse bile, **failover beklemeden** verisini okumaya devam edebilmek istiyor. Hangi seçeneği seçmelidir?

- A) GRS
- B) ZRS
- C) RA-GRS
- D) LRS

<details><summary>Cevap</summary>

**C.** RA-GRS (read-access geo-redundant), ikincil bölgeden sürekli okuma izni verir. GRS'te ikincil bölge ancak failover sonrası erişilebilir olur.
</details>

#### Soru 7: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) GRS ile ikincil bölgedeki veri, failover yapılmadan her zaman okunabilir. | ☐ | ☐ |
| b) GZRS, birincil bölgede veriyi availability zone'lara yayar ve ikincil bölgeye de kopyalar. | ☐ | ☐ |
| c) ZRS, bölgesel bir felakete karşı koruma sağlar. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** Sürekli okuma yalnızca RA- seçeneklerinde vardır; GZRS = ZRS (birincil) + ikincil bölgede LRS; ZRS tek bölgededir.
</details>

#### Soru 8: Tek seçim
GRS ve GZRS'te ikincil bölge nasıl belirlenir?

- A) Müşteri dünyadaki herhangi bir bölgeyi seçer
- B) Birincil bölgenin region pair'i kullanılır
- C) Her zaman aynı bölgedeki başka bir zone kullanılır
- D) Azure her ay farklı bir bölge seçer

<details><summary>Cevap</summary>

**B.** Coğrafi redundancy'de ikincil bölge, birincil bölgenin eşleşik bölgesidir (region pair → C1.4).
</details>

#### Soru 9: Tek seçim
Yıllarca saklanması gereken ve **yılda bir kez bile okunmayabilecek** uyumluluk kayıtları için **en düşük depolama maliyetini** hangi access tier sağlar?

- A) Hot
- B) Cool
- C) Cold
- D) Archive

<details><summary>Cevap</summary>

**D.** Archive en ucuz depolama tier'ıdır; karşılığında veri çevrimdışıdır ve okumadan önce rehydrate gerekir — nadiren okunan uyumluluk kaydı için uygundur.
</details>

#### Soru 10: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Archive tier'daki bir blob, Hot tier'daki gibi anında indirilebilir. | ☐ | ☐ |
| b) Archive'daki bir blob'u okumak için önce Hot, Cool veya Cold tier'a rehydrate etmek gerekir ve bu saatler sürebilir. | ☐ | ☐ |
| c) Cool tier'daki bir blob'u 30 günden önce silmek ek ücret doğurabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Archive çevrimdışıdır; rehydrate standart öncelikte saatler sürebilir; Cool (30), Cold (90), Archive (180 gün) için erken silme ücreti vardır.
</details>

#### Soru 11: Sıralama
Aşağıdaki blob access tier'larını **depolama maliyeti en yüksekten en düşüğe** sıralayın: Archive · Hot · Cold · Cool

<details><summary>Cevap</summary>

**Hot → Cool → Cold → Archive.** Erişim (okuma) maliyeti ise ters yönde artar.
</details>

#### Soru 12: Senaryo
Bir web sitesinin ana sayfa görselleri günde on binlerce kez okunuyor. Bu görseller hangi tier'da tutulmalıdır?

- A) Hot
- B) Cool
- C) Cold
- D) Archive

<details><summary>Cevap</summary>

**A.** Sık erişilen veri için erişim maliyeti en düşük olan Hot tier doğrudur; Cool/Cold/Archive'da okuma ücretleri yüksek olur ve Archive'da veri anında okunamaz.
</details>

#### Soru 13: Tek seçim
Bir yönetici, yerel bir klasördeki binlerce dosyayı **komut satırından, betikle** bir blob container'a kopyalamak istiyor. Hangi aracı kullanmalıdır?

- A) Azure Storage Explorer
- B) AzCopy
- C) Azure File Sync
- D) Azure Data Box

<details><summary>Cevap</summary>

**B.** AzCopy, blob/dosya kopyalamak için komut satırı aracıdır (`azcopy copy … --recursive`). Storage Explorer grafik araçtır; File Sync Windows Server ↔ Azure Files senkronudur; Data Box fiziksel cihazdır.
</details>

#### Soru 14: Tek seçim
Bir kullanıcı, blob ve dosya paylaşımlarına **grafik bir masaüstü uygulamasıyla** göz atmak, dosya yükleyip indirmek istiyor. Hangi araç **en uygundur**?

- A) AzCopy
- B) Azure Storage Explorer
- C) Azure Migrate
- D) Azure Cloud Shell

<details><summary>Cevap</summary>

**B.** Azure Storage Explorer; Windows, macOS ve Linux'ta çalışan grafik yönetim aracıdır (arka planda AzCopy kullanır).
</details>

#### Soru 15: Senaryo
Bir şirketin şubelerinde Windows Server dosya sunucuları var. Şirket, dosyaların merkezi olarak Azure'da tutulmasını, ama şubelerdeki kullanıcıların **sık kullanılan dosyalara yerel sunucudan hızlı erişmesini** ve seyrek dosyaların yerel diskte yer kaplamamasını istiyor. Ne kullanmalıdır?

- A) AzCopy ile her gece kopyalama
- B) Azure File Sync (cloud tiering ile)
- C) Azure Data Box
- D) Blob Storage Archive tier

<details><summary>Cevap</summary>

**B.** Azure File Sync, Windows Server'ı Azure Files'ın önbelleği yapar; iki yönlü senkron ve cloud tiering (seyrek dosyalar yalnızca bulutta) sağlar.
</details>

#### Soru 16: Senaryo
Bir şirket 250 TB veriyi Azure'a taşımak istiyor; internet bağlantısı yavaş ve taşıma ağ üzerinden aylar sürecek. **En uygun** çözüm nedir?

- A) AzCopy
- B) Azure Storage Explorer
- C) Azure Data Box
- D) Azure File Sync

<details><summary>Cevap</summary>

**C.** Büyük veri + yetersiz ağ = Azure Data Box: veri fiziksel cihaza kopyalanır, cihaz Azure datacenter'ına gönderilir ve storage account'a yüklenir.
</details>

#### Soru 17: Tek seçim
Bir şirket, datacenter'ındaki yüzlerce sunucuyu **keşfetmek**, Azure'da uygun **boyutlarını ve tahmini maliyetini değerlendirmek** ve ardından taşımak için merkezi bir araç arıyor. Hangisi?

- A) Azure Data Box
- B) Azure Migrate
- C) AzCopy
- D) Azure Advisor

<details><summary>Cevap</summary>

**B.** Azure Migrate, keşif (discovery), değerlendirme (assessment) ve taşıma için merkezi hub'dır. Data Box veri taşıma cihazıdır; Advisor zaten Azure'da olan kaynaklar için öneri verir.
</details>

#### Soru 18: Çoklu seçim (iki doğru cevap)
Azure Data Box hakkında **hangi iki** ifade doğrudur?

- A) Veriyi hem Azure'a aktarmak hem de Azure'dan dışarı almak için kullanılabilir.
- B) Sunucuları keşfedip boyutlandırma önerisi üretir.
- C) Ağ bağlantısının yetersiz veya pahalı olduğu büyük veri taşımaları için uygundur.
- D) Windows Server ile Azure Files arasında sürekli senkron sağlar.

<details><summary>Cevap</summary>

**A ve C.** Data Box içe ve dışa aktarmayı destekler ve büyük, ağdan taşınması zor veri için tasarlanmıştır. B Azure Migrate'i, D Azure File Sync'i anlatır.
</details>

#### Soru 19: Tek seçim
Bir ekip; Blob, Files, Queue ve Table servislerini **tek bir hesapta** kullanmak ve ileride GRS'e geçebilmek istiyor. Hangi storage account türünü seçmelidir?

- A) Premium block blobs
- B) Premium file shares
- C) Standard general-purpose v2
- D) Premium page blobs

<details><summary>Cevap</summary>

**C.** Standard general-purpose v2; tüm servisleri ve tüm redundancy seçeneklerini (LRS'ten RA-GZRS'e) destekleyen, çoğu senaryo için önerilen türdür. Premium türler belirli bir servise özeldir ve yalnızca LRS/ZRS sunar.
</details>

#### Soru 20: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Storage account adı Azure genelinde benzersiz olmalıdır. | ☐ | ☐ |
| b) Premium storage account'lar GRS redundancy'yi destekler. | ☐ | ☐ |
| c) Bir blob'un access tier'ı blob düzeyinde değiştirilebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Ad, endpoint URL'sinin parçasıdır (`<ad>.blob.core.windows.net`); Premium türler yalnızca LRS/ZRS sunar; tier blob düzeyinde ayarlanabilir (`az storage blob set-tier`, `SetAccessTier`).
</details>

---

**Puanlama:** 18-20 → hazırsın · 14-17 → README 3.2 (redundancy) ve 3.3 (tier) tablolarını tekrar et · < 14 → konuyu baştan çalış, `dotnet run -- C1.8 all` çıktısını incele.


---

## 2.6 Kimlik ve Erişim: Entra ID, MFA, Conditional Access, RBAC

> **Sınav Alanı:** Describe Azure architecture and services (%35-40) · **Konu Kodu:** C2.6

### 1. Ne?

Bulutta kaynaklar herhangi bir yerden, herhangi bir cihazdan erişilebilir olduğu için güvenliğin sınırı artık şirket ağının duvarı değil, **kimliktir** ("identity is the new perimeter"). Bu konu iki soruyu cevaplar:

| Soru | Kavram | Azure aracı |
|---|---|---|
| **Sen kimsin?** (authentication, AuthN) | Kimlik doğrulama: şifre, **MFA**, **passwordless**, **SSO** | **Microsoft Entra ID**, Conditional Access |
| **Ne yapabilirsin?** (authorization, AuthZ) | Yetkilendirme: hangi kaynakta hangi işlem | **Azure RBAC** (rol atamaları) |

Çevresindeki parçalar:

- **Directory servisleri:** Microsoft Entra ID (bulut tabanlı kimlik) ve Microsoft Entra Domain Services (yönetilen klasik AD özellikleri).
- **External identities:** iş ortakları (**B2B**) ve müşteriler (**B2C** / Entra External ID) için kimlik.
- **Conditional Access:** "kim, nereden, hangi cihazla?" sinyallerine göre erişime karar veren politika motoru.

Bu konunun C# kodu yoktur; pratik, `az` CLI laboratuvarıdır (3.11).

---

### 2. Neden?

- **Sınav:** Kimlik/erişim/güvenlik maddeleri "Describe Azure architecture and services" alanının (%35-40) büyük parçasıdır; Entra ID ↔ Domain Services, B2B ↔ B2C, AuthN ↔ AuthZ, Owner ↔ Contributor ↔ Reader eşleştirmeleri neredeyse her sınavda çıkar.
- **Gerçek işte:** Gerçek ihlallerin çoğu çalınmış veya zayıf kimlik bilgisiyle başlar. Microsoft'un kendi açıklamalarına göre MFA, hesap ele geçirme saldırılarının çok büyük kısmını durdurur; Azure bu yüzden yönetim işlemlerinde MFA'yı **zorunlu** hale getirdi (3.6).
- **Shared responsibility (→ C1.1):** Hesaplar ve kimlikler **her modelde müşterinin** sorumluluğundadır. Entra ID altyapıyı sağlar; kimin hangi role sahip olduğu, MFA'nın açık olup olmadığı senin kararındır.

---

### 3. Nasıl?

#### 3.1 Microsoft Entra ID (eski adı Azure Active Directory)

**Microsoft Entra ID**, Microsoft'un **bulut tabanlı kimlik ve erişim yönetimi** servisidir. Kullanıcıların, grupların, uygulamaların ve cihazların kimliklerini tutar; Microsoft 365, Azure portal, Dynamics 365 ve binlerce SaaS/özel uygulamaya girişi yönetir. (Azure AD, 2023'te **Microsoft Entra ID** adını aldı; sınav ve dokümanlar yeni adı kullanır.)

- **Tenant:** bir kuruluşun Entra ID örneği (ör. `contoso.onmicrosoft.com`). Her Azure subscription **tek bir tenant'a** güvenir; bir tenant'a birden çok subscription bağlı olabilir (→ C1.4 hiyerarşisinin en üstü).
- **Kimler kullanır?**
  - **BT yöneticileri:** kullanıcı/grup yönetimi, erişim politikaları, MFA.
  - **Geliştiriciler:** uygulamalarına SSO ve kimlik eklemek (app registration, OAuth 2.0/OIDC → M9).
  - **Kullanıcılar:** tek hesapla giriş, **self-service password reset** (SSPR).
  - **Online servis aboneleri:** Microsoft 365, Azure ve Dynamics 365 zaten Entra ID kullanır.

**On-prem Active Directory (AD DS) ile farkı:**

| | Active Directory Domain Services (on-prem) | Microsoft Entra ID |
|---|---|---|
| Nerede | Kendi domain controller sunucuların | Microsoft'un yönettiği küresel bulut servisi |
| Protokoller | **Kerberos, NTLM, LDAP**, Group Policy (GPO) | **HTTP tabanlı modern protokoller:** OAuth 2.0, OpenID Connect, SAML, WS-Federation |
| Yapı | Forest, domain, OU, domain join | Düz yapı: tenant, kullanıcı, grup, administrative unit |
| Tipik kullanım | Kurum içi Windows sunucu/istemci yönetimi | Bulut ve SaaS uygulamalarına giriş, internet üzerinden erişim |

**Hibrit kimlik:** Kurum içi AD kullanıcılarını Entra ID'ye senkronize etmek için **Microsoft Entra Connect** (Sync) veya daha hafif ajan tabanlı **Microsoft Entra Cloud Sync** kullanılır. Böylece kullanıcı aynı kimlikle hem kurum içi kaynaklara hem bulut uygulamalarına girer. Senkronizasyon seçenekleri (password hash sync, pass-through authentication, federation) *(bağlam)* sınavda ad olarak geçebilir.

#### 3.2 Microsoft Entra Domain Services

Bazı eski (legacy) uygulamalar modern protokolleri konuşamaz; **domain join**, **Group Policy**, **LDAP** veya **Kerberos/NTLM** ister. Bunları buluta taşırken iki seçenek vardır:

1. Azure VM'lere kendi domain controller'larını kurup yönetmek (IaaS — yama, yedek, replikasyon senin işin).
2. **Microsoft Entra Domain Services:** Bu klasik AD özelliklerini **yönetilen** (managed) bir hizmet olarak sunar. Domain controller'ları Microsoft kurar, yamalar ve yedekler; sen yalnızca VM'leri bu yönetilen domain'e katarsın.

- Yönetilen domain, Entra ID'den **tek yönlü** senkronize olur: kullanıcı ve gruplar Entra ID'den gelir. Domain Services'ta oluşturulan nesneler Entra ID'ye geri yazılmaz.
- Sınav kalıbı: "Eski uygulama LDAP/Kerberos/domain join istiyor, **domain controller yönetmek istemiyoruz**" → **Entra Domain Services**.

#### 3.3 Benefits of Microsoft Entra ID

| Fayda | Ne sağlar |
|---|---|
| **Authentication** | Kimlik doğrulama, **self-service password reset**, **MFA**, özel yasaklı şifre listesi, **smart lockout** (kaba kuvvet denemelerini kilitleme) |
| **Single sign-on (SSO)** | Tek kimlikle birçok uygulamaya tek giriş (3.5) |
| **Application management** | Bulut (SaaS) ve kurum içi uygulamaları tek yerden yönetme; **My Apps** portalı; **Application Proxy** ile kurum içi web uygulamalarına VPN'siz güvenli erişim |
| **Device management** | Cihazları Entra ID'ye kaydetme/katma; **Microsoft Intune** ile cihaz politikaları; cihaz durumunu Conditional Access sinyali olarak kullanma |
| **Identity protection** *(P2)* | Riskli oturum ve riskli kullanıcı tespiti (sızdırılmış kimlik bilgisi, olağandışı konum) |
| **Conditional Access** *(P1)* | Sinyale göre erişim kararı (3.9) |
| **Denetim ve raporlama** | Giriş (sign-in) ve denetim (audit) log'ları, merkezi görünürlük |

> *(bağlam)* Lisans katmanları: **Free** (Azure/Microsoft 365 aboneliğiyle gelen temel kimlik, security defaults), **P1** (Conditional Access, gelişmiş grup yönetimi, hibrit özellikler), **P2** (Identity Protection, Privileged Identity Management). Sınav fiyat sormaz; "Conditional Access lisans ister" bilgisi yeterlidir.

#### 3.4 Authentication vs authorization ⭐

| | **Authentication (AuthN)** | **Authorization (AuthZ)** |
|---|---|---|
| Soru | "**Sen kimsin?**" — iddia ettiğin kişi misin? | "**Ne yapabilirsin?**" — hangi kaynağa hangi erişimin var? |
| Benzetme | Havalimanında **pasaport** kontrolü | **Biniş kartı**: hangi uçağa, hangi koltuğa |
| Araç | Şifre, MFA, passwordless, sertifika | Azure RBAC rolleri, Entra rolleri, uygulama izinleri |
| Sıra | **Önce** | **Sonra** (kimliği doğrulanmış kişinin yetkisi) |

AuthN başarılı olsa bile AuthZ reddedebilir: kullanıcı portala girer (AuthN ✓) ama başka bir resource group'u göremez (AuthZ ✗).

#### 3.5 Single sign-on (SSO)

**SSO**, kullanıcının **tek kimlik ve tek giriş** ile birden çok bağımsız uygulamaya erişmesidir.

- **Kullanıcı için:** tek şifre (veya passwordless), tekrar tekrar giriş yok.
- **Güvenlik için:** her ek kimlik = ek şifre = ek risk. Kimlikler tek yerde olunca **politika tek yerde** uygulanır (MFA, Conditional Access) ve çalışan ayrıldığında **tek hesabı kapatmak** tüm erişimi bitirir.
- **Uygulama için:** Entra ID, SAML/OIDC ile binlerce SaaS uygulamasına hazır SSO entegrasyonu sunar (uygulama galerisi).

> SSO **kimliğin kaç kez sorulduğunu** azaltır; MFA ise kimliğin **ne kadar güçlü** kanıtlandığını artırır. İkisi birlikte kullanılır ve birbirinin alternatifi değildir.

#### 3.6 Multi-factor authentication (MFA / 2FA)

**MFA**, kimliği **iki veya daha fazla farklı türde faktörle** kanıtlamaktır:

| Faktör türü | Örnek |
|---|---|
| **Bildiğin bir şey** (knowledge) | Şifre, PIN, güvenlik sorusu |
| **Sahip olduğun bir şey** (possession) | Telefon (Authenticator bildirimi/kodu), donanım anahtarı, akıllı kart |
| **Olduğun bir şey** (inherence) | Parmak izi, yüz tanıma |

- **2FA**, MFA'nın iki faktörlü halidir. İki şifre istemek MFA **değildir**; ikisi de aynı türdendir (bildiğin).
- **Microsoft Entra MFA yöntemleri:** Microsoft Authenticator (bildirim veya tek kullanımlık kod), OATH donanım/yazılım token'ları, SMS ve sesli arama (en zayıfları), FIDO2 güvenlik anahtarları, Windows Hello for Business.
- **Şifre çalınsa bile** saldırgan ikinci faktöre sahip olmadığı için giremez; MFA'nın değeri buradadır.
- **Security defaults:** Entra ID'nin **ücretsiz**, tek anahtarla açılan temel güvenlik ayarları. Tüm kullanıcılara MFA kaydı yaptırır, yöneticilere ve riskli işlemlere MFA ister, MFA desteklemeyen **eski (legacy) kimlik doğrulama protokollerini** engeller. Yeni tenant'larda varsayılan olarak açıktır. Ayrıntılı ve koşullu kurallar gerekiyorsa yerine Conditional Access kullanılır.
- **Azure'da zorunlu MFA:** Microsoft, Azure yönetim işlemleri için MFA'yı kademeli olarak **zorunlu** kıldı: **1. aşama** Azure portal, Microsoft Entra admin center ve Intune admin center girişleri (2025 Mart itibarıyla tüm tenant'lara yayıldı); **2. aşama** 1 Ekim 2025'ten itibaren kademeli olarak Azure CLI, Azure PowerShell, IaC araçları ve REST API üzerinden yapılan **oluşturma/güncelleme/silme** işlemleri. Managed identity ve service principal gibi **iş yükü kimlikleri** etkilenmez. (Güncel durum için Microsoft Learn'deki "mandatory MFA" sayfasını kontrol et.)

#### 3.7 Passwordless authentication

Şifre hem en zayıf halkadır (tahmin, tekrar kullanım, phishing) hem de kullanıcı için zahmetlidir. **Passwordless** yöntemler şifreyi tamamen kaldırır ve "**sahip olduğun** cihaz + **olduğun** (biyometri) veya cihaza bağlı **bildiğin** (yerel PIN)" ile giriş yapar. Bu yöntemler zaten çok faktörlüdür. Microsoft'un üç ana seçeneği:

| Yöntem | Nasıl çalışır | Tipik kullanım |
|---|---|---|
| **Windows Hello for Business** | Kullanıcının **kendi Windows cihazına** bağlı biyometri veya PIN; kimlik bilgisi cihazdan çıkmaz | Atanmış bir Windows bilgisayarı kullanan bilgi çalışanları |
| **Microsoft Authenticator** | Telefon, kimlik doğrulayıcı olur: phone sign-in (eşleşen numara + biyometri/PIN) veya Authenticator'da saklanan **passkey** | Mobil çalışanlar, her cihazdan giriş |
| **FIDO2 güvenlik anahtarı / passkey** | Açık standart (FIDO2/WebAuthn) donanım anahtarı (USB, NFC) veya cihazdaki passkey; **phishing'e dayanıklı** | Paylaşılan cihazlar, yüksek ayrıcalıklı hesaplar, telefon kullanılamayan ortamlar |

> Passwordless hem **daha güvenli** (phishing'e dayanıklı, çalınacak şifre yok) hem **daha kolaydır**. Sınav bu iki faydayı birlikte sorar.

#### 3.8 External identities: B2B ve B2C

Kuruluş dışındaki kişilerin (iş ortakları, tedarikçiler, müşteriler) **kendi kimlikleriyle** uygulamalarına güvenli erişimi:

| | **B2B collaboration** | **B2C** / **Microsoft Entra External ID** (müşteri) |
|---|---|---|
| Kim | **İş ortakları**, tedarikçiler, danışmanlar | Uygulamanı kullanan **müşteriler** (tüketiciler) |
| Nasıl | Kendi kimlikleriyle (kendi Entra ID'leri, Microsoft hesabı, Google, e-posta tek kullanımlık kod) **misafir (guest)** olarak senin tenant'ına davet edilirler | Uygulamana **kayıt olur/giriş yaparlar**: e-posta/şifre, sosyal hesaplar (Google, Facebook…), özelleştirilebilir marka sayfaları |
| Nerede yönetilir | Kendi (workforce) tenant'ında, diğer kullanıcılar gibi gruplara ve rollere atanır | Ayrı bir müşteri (external) tenant'ında, milyonlarca kullanıcıya ölçeklenir |
| Örnek | Ajans çalışanına SharePoint sitesi ve Azure resource group'unda Reader yetkisi | E-ticaret sitesinde "Google ile giriş yap" |

- **B2B direct connect** *(bağlam)*: iki kuruluş arasında karşılıklı güven; ör. Teams **paylaşılan kanallar**, misafir hesabı oluşturmadan.
- **Azure AD B2C** 1 Mayıs 2025'ten itibaren **yeni müşterilere satılmıyor**; mevcut müşteriler kullanmaya devam ediyor ve Microsoft en az 2030'a kadar desteği taahhüt ediyor. Yeni müşteri kimliği (CIAM) projeleri için önerilen ürün **Microsoft Entra External ID**'dir. Sınavda "B2C" terimi kavram olarak geçmeye devam edebilir: **müşteriye dönük kimlik**.

#### 3.9 Conditional Access

**Conditional Access**, Entra ID'nin **politika motorudur**: her giriş isteğinde sinyalleri toplar, bir karar verir ve uygular. Yapısı **"if-then"**dir: *eğer* (bu sinyaller) *ise* (bu kararı uygula).

| 1. Sinyaller (if) | 2. Karar | 3. Uygulama (then) |
|---|---|---|
| **Kullanıcı / grup** (yönetici mi, misafir mi?) | Değerlendirme | **Erişime izin ver** |
| **Konum** (IP aralığı, ülke, bilinen ofis ağı) | | **MFA iste** |
| **Cihaz** (uyumlu mu, kurumsal mı, platform) | | **Uyumlu/kurumsal cihaz** zorunlu tut |
| **Uygulama** (hangi uygulamaya erişiliyor) | | Şifre değişikliği iste, oturum süresini kısalt |
| **Risk** (oturum/kullanıcı riski, P2 ile) | | **Erişimi engelle** |

Örnek politikalar:

- "**Yönetici rolündeki** herkes, her girişte MFA kullanır."
- "**Bilinen ofis IP'leri dışından** giren kullanıcıdan MFA iste."
- "**Uyumsuz** (yönetilmeyen) cihazdan e-postaya erişim yok."
- "Belirli **ülkelerden** gelen tüm girişleri engelle."
- "Riskli oturumda (sızdırılmış kimlik bilgisi şüphesi) şifre değişikliği zorunlu."

> Conditional Access, MFA'yı **her zaman** değil **koşula bağlı** ister: güvenilir durumda kullanıcıyı rahat bırakır, riskli durumda ek kanıt ister. **Microsoft Entra ID P1** (risk tabanlı politikalar için P2) lisansı gerektirir. Ücretsiz alternatif, daha az esnek olan **security defaults**'tur.

Portalda politika oluşturma ekranları ve hazır şablonlar (ör. *Require MFA for administrators*) bu konunun teorisinin birebir karşılığıdır; yukarıdaki sinyal → karar → uygulama tablosunu bilmek sınav için yeterlidir.

#### 3.10 Azure role-based access control (RBAC) ⭐

**Azure RBAC**, Azure kaynaklarına **kimin, nerede, ne yapabileceğini** belirler. Bir **rol ataması** (role assignment) üç parçadan oluşur:

```
Role assignment = Security principal (WHO)  +  Role definition (WHAT)  +  Scope (WHERE)
                  user, group,                 permission set:            management group,
                  service principal,           Owner, Contributor,        subscription,
                  managed identity             Reader, …                  resource group, resource
```

**Built-in roller (sınavın sorduğu dört temel rol):**

| Rol | Kaynakları görüntüleme | Kaynak oluşturma/değiştirme/silme | Erişim (rol atamalarını) yönetme |
|---|---|---|---|
| **Owner** | ✓ | ✓ | ✓ |
| **Contributor** | ✓ | ✓ | ✗ |
| **Reader** | ✓ | ✗ | ✗ |
| **User Access Administrator** | ✓ (yalnızca erişimle ilgili okuma) | ✗ | ✓ |

- Ayrıca **servise özel** built-in roller vardır (Virtual Machine Contributor, Storage Blob Data Reader…) ve ihtiyaca göre **custom roller** tanımlanabilir.
- **Kapsam ve miras:** Bir kapsamda verilen rol, **altındaki tüm kapsamlara miras** kalır. Subscription'da Reader olan kullanıcı, o subscription'daki tüm resource group'ları ve kaynakları okuyabilir (→ C1.4 hiyerarşisi).
- **İzinler toplanır (additive):** Bir kullanıcının subscription'da Reader, bir resource group'ta Contributor rolü varsa, o resource group'ta **Contributor** gibi davranır. *(bağlam)* Açıkça yasaklamak için ayrı bir mekanizma olan **deny assignment** vardır; bunları genellikle platform (ör. managed application) oluşturur.
- **En az yetki ilkesi (least privilege):** İşi yapmaya yetecek **en düşük** rolü, **en dar** kapsamda ver; kişiler yerine **gruplara** ata.
- **Nasıl uygulanır:** Portal, CLI, PowerShell, SDK veya REST'ten gelen her yönetim isteği **Azure Resource Manager**'dan geçer (→ C1.13). ARM, isteği yapanın o kapsamda gereken izne sahip olup olmadığını kontrol eder.

**Karıştırılan üç kavram:**

| | **Azure RBAC** | **Microsoft Entra rolleri** | **Azure Policy** (→ C1.12) |
|---|---|---|---|
| Neyi kontrol eder | **Azure kaynakları** üzerindeki işlemler (VM oluştur, storage sil) | **Directory** üzerindeki işlemler (kullanıcı oluştur, şifre sıfırla, lisans ata) | **Kaynağın özellikleri** (hangi bölge, hangi SKU, zorunlu tag) |
| Örnek rol/kural | Owner, Contributor, Reader | Global Administrator, User Administrator | "Yalnızca West Europe", "tag zorunlu" |
| Kapsam | Management group → subscription → RG → kaynak | Tenant (veya administrative unit) | Management group → subscription → RG |
| Soru | "**Kullanıcı** ne yapabilir?" | "**Directory'de** kim ne yapabilir?" | "**Kaynak** nasıl olmalı?" |

#### 3.11 Pratik — kullanıcı, grup ve kapsamlı rol ataması (`az` CLI)

Bu laboratuvar **ücretsizdir**: Entra ID'de kullanıcı/grup oluşturmak ve boş bir resource group'a rol atamak ücret üretmez. Yine de bütçe alarmın kurulu olsun (→ C1.1). Kullanıcı oluşturmak için tenant'ta **User Administrator** (veya Global Administrator), rol atamak için hedef kapsamda **Owner** veya **User Access Administrator** gerekir. En temizi, kendi oluşturduğun bir deneme tenant'ı/aboneliğidir.

```bash
az login
TENANT_DOMAIN=$(az ad signed-in-user show --query userPrincipalName -o tsv | cut -d@ -f2)
echo "$TENANT_DOMAIN"          # e.g. contoso.onmicrosoft.com

# 1) Two resource groups: the permission will exist on only one
az group create --name rg-az900-c19-a --location westeurope
az group create --name rg-az900-c19-b --location westeurope

# 2) A user and a group in Entra ID
az ad user create \
  --display-name "AZ900 Reader" \
  --user-principal-name "az900reader@$TENANT_DOMAIN" \
  --password "Temp-Password-$RANDOM!aA" \
  --force-change-password-next-sign-in true
az ad group create --display-name grp-az900-readers --mail-nickname grp-az900-readers

USER_ID=$(az ad user show --id "az900reader@$TENANT_DOMAIN" --query id -o tsv)
GROUP_ID=$(az ad group show --group grp-az900-readers --query id -o tsv)
az ad group member add --group grp-az900-readers --member-id "$USER_ID"
az ad group member list --group grp-az900-readers --query "[].userPrincipalName" -o tsv

# 3) Assign the Reader role to the GROUP, only at the rg-az900-c19-a scope (least privilege + assign to groups)
SCOPE_A=$(az group show --name rg-az900-c19-a --query id -o tsv)
az role assignment create --assignee-object-id "$GROUP_ID" --assignee-principal-type Group \
  --role Reader --scope "$SCOPE_A"

# 4) Verify: the assignment exists only at scope A, not at B
az role assignment list --scope "$SCOPE_A" --output table
az role assignment list --scope "$(az group show --name rg-az900-c19-b --query id -o tsv)" --output table
az role assignment list --all --assignee "$GROUP_ID" --query "[].{role:roleDefinitionName, scope:scope}" -o table

# 5) Look inside the roles: Reader is only */read; Contributor can do everything except manage permissions
az role definition list --name Reader      --query "[].permissions[0].actions"    -o tsv
az role definition list --name Contributor --query "[].permissions[0].notActions" -o tsv
az role definition list --name Owner       --query "[].permissions[0].actions"    -o tsv
```

Beklenen gözlem: Kullanıcı (portalda veya `az login` ile, ilk girişte şifre değiştirip MFA kaydı yaparak) `rg-az900-c19-a`'yı ve içini **görür** ama içinde kaynak **oluşturamaz**; `rg-az900-c19-b`'yi ise **hiç görmez**. Contributor'ın `notActions` listesinde `Microsoft.Authorization/*/Write` ve `*/Delete` bulunur: Contributor her şeyi yapar ama **başkasına rol atayamaz**.

**B2B misafir daveti** (isteğe bağlı): `az` CLI'nin doğrudan bir "davet" komutu yoktur. Microsoft Graph'ın `invitations` API'si `az rest` ile çağrılabilir. Bunun için oturumdaki hesabın davet yetkisi (ör. Guest Inviter veya User Administrator rolü) olmalıdır:

```bash
az rest --method POST --url "https://graph.microsoft.com/v1.0/invitations" \
  --headers "Content-Type=application/json" \
  --body '{"invitedUserEmailAddress":"partner@example.com","inviteRedirectUrl":"https://myapps.microsoft.com","sendInvitationMessage":true}'
# The invited user appears in the tenant as userType=Guest:
az ad user list --filter "userType eq 'Guest'" --query "[].{name:displayName, upn:userPrincipalName}" -o table
```

**Temizlik:**

```bash
az role assignment delete --assignee "$GROUP_ID" --role Reader --scope "$SCOPE_A"
az ad group delete --group grp-az900-readers
az ad user delete --id "az900reader@$TENANT_DOMAIN"
# (if you sent an invitation) az ad user delete --id <guest user's id>
az group delete --name rg-az900-c19-a --yes --no-wait
az group delete --name rg-az900-c19-b --yes --no-wait
```

> **Alıştırma.** Bir şirkette üç ekip var: **Platform** (tüm subscription'ları yönetir, başkalarına yetki verir), **Geliştiriciler** (yalnızca `rg-app-dev` içinde kaynak açıp siler), **Denetçiler** (tüm subscription'ları yalnızca görüntüler). Her ekip için rol ve kapsamı yaz.
>
> <details><summary>Çözüm</summary>
>
> | Ekip | Rol | Kapsam | Neden |
> |---|---|---|---|
> | Platform | **Owner** | Management group (veya her subscription) | Kaynak yönetimi + rol atama; management group'tan tüm subscription'lara miras |
> | Geliştiriciler | **Contributor** | `rg-app-dev` resource group | Kaynak oluştur/sil, ama yetki dağıtamazlar; kapsam yalnızca o RG |
> | Denetçiler | **Reader** | Management group | Yalnızca görüntüleme, tüm subscription'lara miras |
>
> Hepsi **gruplara** atanır (kişiye değil); ekip değişince yalnızca grup üyeliği değişir.
> </details>

---

### 4. Gerçek dünya senaryosu

Bir sigorta şirketi kimlik altyapısını modernize ediyor:

1. **Hibrit kimlik:** Kurum içindeki AD kullanıcıları **Entra Connect** ile Entra ID'ye senkronize ediliyor. Çalışanlar aynı hesapla hem dosya sunucusuna hem Microsoft 365'e giriyor. Microsoft 365, Salesforce ve iç uygulamalar Entra ID üzerinden **SSO** ile bağlanıyor.
2. **Eski poliçe uygulaması** LDAP ve domain join istiyor. Azure'a taşınırken VM'lere domain controller kurmak yerine **Entra Domain Services** kullanılıyor.
3. **MFA ve passwordless:** Önce tüm çalışanlar Authenticator ile **MFA**'ya kaydediliyor. Yöneticiler ve çağrı merkezinin paylaşılan cihazları için **FIDO2 anahtarları**, ofis çalışanları için **Windows Hello for Business** devreye alınıyor.
4. **Conditional Access (P1):** "Yöneticiler her zaman MFA", "ofis dışından girişte MFA", "yönetilmeyen cihazdan poliçe sistemine erişim yok", "şirketin faaliyet göstermediği ülkelerden giriş engelli" politikaları tanımlanıyor.
5. **External identities:** Acente çalışanları **B2B misafir** olarak davet edilip yalnızca acente portalına yetkilendiriliyor. Sigortalıların kullandığı mobil uygulama için ayrı bir **Entra External ID** (müşteri) tenant'ı kuruluyor ve "Google/Apple ile giriş" ekleniyor.
6. **RBAC:** Azure'da Platform ekibi management group'ta **Owner**, uygulama ekipleri kendi resource group'larında **Contributor**, iç denetim **Reader**. Tüm atamalar gruplara yapılıyor ve üç ayda bir gözden geçiriliyor.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Bu konu çoğunlukla **senaryo → servis/yöntem** eşleştirmesi ve **evet/hayır** tablolarıyla sorulur. RBAC'ta rol ve kapsam seçimi, Conditional Access'te sinyal/karar mantığı beklenir.

| Soru kalıbı | Doğru cevap | Tuzak |
|---|---|---|
| "Bulut tabanlı kimlik ve erişim yönetimi servisi" | **Microsoft Entra ID** | Entra Domain Services |
| "Eski uygulama domain join / LDAP / Kerberos / Group Policy istiyor, DC yönetmek istemiyoruz" | **Entra Domain Services** | Entra ID (bu protokolleri sunmaz) |
| "Kurum içi AD kullanıcılarını Entra ID'ye senkronize et" | **Entra Connect** (veya Cloud Sync) | Domain Services |
| "Kimliği doğrulama" / "kimliğini kanıtla" | **Authentication** | Authorization |
| "Hangi kaynağa erişebileceğini belirleme" | **Authorization** (RBAC) | Authentication |
| "Tek girişle birçok uygulama" | **SSO** | MFA |
| "Şifre + telefona gelen bildirim" | **MFA** | SSO |
| "Şifre olmadan, biyometri/güvenlik anahtarıyla giriş" | **Passwordless** (Windows Hello, Authenticator, FIDO2) | MFA (passwordless zaten çok faktörlüdür) |
| "İş ortağını kendi hesabıyla misafir olarak davet et" | **B2B** | B2C |
| "Müşterilerin sosyal hesapla uygulamaya kaydolması" | **B2C / Entra External ID** | B2B |
| "Konuma/cihaza/riske göre MFA iste veya engelle" | **Conditional Access** | Security defaults (koşulsuz), RBAC |
| "Kaynakları yalnızca görüntüleme" | **Reader** | Contributor |
| "Her şeyi yönet ama başkasına erişim veremesin" | **Contributor** | Owner |
| "Kaynakları ve erişimi tam yönet" | **Owner** | User Access Administrator (kaynakları yönetemez) |
| "Yalnızca erişim (rol atamaları) yönetsin" | **User Access Administrator** | Owner |
| "Rolü birçok subscription'a birden uygula" | **Management group** kapsamında rol ataması | Her subscription'a tek tek |

**Anahtar kelimeler:** *identity as the primary security perimeter*, *tenant*, *managed domain*, *guest user*, *social identities*, *signals → decisions → enforcement*, *security principal + role definition + scope*, *inherited*, *least privilege*.

**Evet/hayır tuzakları:**
- "RBAC atamaları alt kapsamlara miras kalır" → **Evet**.
- "Entra ID, Kerberos ve LDAP ile domain join sağlar" → **Hayır** (bu Domain Services'tır).
- "Conditional Access, kullanıcının bir resource group'ta hangi işlemleri yapabileceğini belirler" → **Hayır** (bu RBAC'tır; Conditional Access **girişe/erişime** karar verir).
- "Contributor rolü başka kullanıcılara rol atayabilir" → **Hayır**.
- "MFA, iki farklı şifre istemektir" → **Hayır** (iki farklı **faktör türü**).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "Entra ID = bulutta çalışan Active Directory" | Entra ID modern HTTP protokolleri (OAuth/OIDC/SAML) kullanır; domain join, GPO, Kerberos/LDAP sunmaz. Bunlar için Entra Domain Services |
| "SSO açtık, MFA'ya gerek yok" | SSO giriş sayısını azaltır, MFA kimliği güçlendirir; ikisi birlikte |
| "SMS ile kod = en güvenli MFA" | SMS en zayıf yöntemlerdendir (SIM swap); Authenticator, FIDO2 ve passkey daha güçlüdür |
| "B2B müşteriler içindir" | B2B **iş ortakları**, B2C/External ID **müşteriler** içindir |
| Rolleri kişilere tek tek atamak | Gruplara ata; ayrılan/katılan kişide yalnızca üyelik değişsin |
| Herkese subscription'da Owner vermek | En az yetki: işi yapan en düşük rol, en dar kapsam |
| "Global Administrator otomatik olarak tüm Azure kaynaklarına erişir" | Entra rolleri ile Azure RBAC ayrıdır. Global Admin varsayılan olarak Azure kaynaklarına erişemez; ancak erişimi kendine yükseltebilir (*elevate access*) *(bağlam)* |
| "Conditional Access ücretsiz" | P1 lisansı gerekir; ücretsiz seçenek security defaults |
| "Yeni bir müşteri uygulaması için Azure AD B2C tenant'ı açarım" | Yeni müşterilere B2C satılmıyor (Mayıs 2025); Entra External ID kullanılır |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Security defaults | Ücretsiz, tek tıkla temel MFA ve eski protokol engeli | Esneklik yok: istisna, konum/cihaz koşulu tanımlanamaz |
| Conditional Access | Riske göre ayarlı güvenlik; güvenilir durumda kullanıcı rahat | P1/P2 lisansı; yanlış politika herkesi (yöneticiler dahil) dışarıda bırakabilir. Bu yüzden **break-glass** acil durum hesabı tutulur ve politika önce "report-only" modda denenir *(bağlam)* |
| MFA zorunluluğu | Hesap ele geçirmeye karşı çok güçlü koruma | Kullanıcı sürtünmesi, kayıt süreci, yardım masası yükü |
| Passwordless | Daha güvenli ve daha hızlı giriş, şifre sıfırlama talepleri azalır | Cihaz/anahtar dağıtımı ve kayıp anahtar süreci |
| Entra Domain Services | Eski uygulamalar DC yönetmeden buluta | Ücretli servis; tam AD DS esnekliği yok (ör. şema genişletme kısıtlı) |
| Kendi DC'lerin (IaaS VM) | Tam AD kontrolü | Yama, yedek, replikasyon ve güvenlik yükü sende |
| Geniş kapsamlı, yüksek roller | Az sayıda atama, kolay yönetim | Büyük saldırı yüzeyi; tek hesap ele geçirilince tüm ortam risk altında |
| Dar kapsamlı, en az yetkili roller | Etki alanı küçük | Daha çok atama ve gözden geçirme |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure identity, access, and security](https://learn.microsoft.com/en-us/training/modules/describe-azure-identity-access-security/)
- [What is Microsoft Entra ID?](https://learn.microsoft.com/en-us/entra/fundamentals/whatis) · [Microsoft Entra Domain Services overview](https://learn.microsoft.com/en-us/entra/identity/domain-services/overview)
- [Passwordless authentication options](https://learn.microsoft.com/en-us/entra/identity/authentication/concept-authentication-passwordless) · [Security defaults](https://learn.microsoft.com/en-us/entra/fundamentals/security-defaults)
- [Mandatory MFA for Azure](https://learn.microsoft.com/en-us/entra/identity/authentication/concept-mandatory-multifactor-authentication) · [Azure mandatory MFA phase 2 (Azure blog)](https://azure.microsoft.com/en-us/blog/azure-mandatory-multifactor-authentication-phase-2-starting-in-october-2025/)
- [What is Conditional Access?](https://learn.microsoft.com/en-us/entra/identity/conditional-access/overview)
- [External ID overview (B2B, External ID for customers)](https://learn.microsoft.com/en-us/entra/external-id/external-identities-overview) · [Azure AD B2C FAQ (end of sale)](https://learn.microsoft.com/en-us/azure/active-directory-b2c/faq)
- [What is Azure RBAC?](https://learn.microsoft.com/en-us/azure/role-based-access-control/overview) · [Azure built-in roles](https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles) · [Azure roles vs Microsoft Entra roles](https://learn.microsoft.com/en-us/azure/role-based-access-control/rbac-and-directory-admin-roles)
- [`az ad`](https://learn.microsoft.com/en-us/cli/azure/ad) · [`az role assignment`](https://learn.microsoft.com/en-us/cli/azure/role/assignment) · [Microsoft Graph: create invitation](https://learn.microsoft.com/en-us/graph/api/invitation-post)


### 📝 Bölüm 2.6 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Microsoft'un bulut tabanlı kimlik ve erişim yönetimi servisi hangisidir?

- A) Microsoft Entra Domain Services
- B) Microsoft Entra ID
- C) Azure Policy
- D) Microsoft Defender for Cloud

<details><summary>Cevap</summary>

**B.** Entra ID (eski adı Azure AD), kullanıcı, grup, uygulama ve cihaz kimliklerini yöneten bulut kimlik servisidir. Domain Services klasik AD özelliklerini yönetilen olarak sunar; Policy kaynak kurallarıdır; Defender güvenlik duruşudur.
</details>

#### Soru 2: Senaryo
Bir şirket, **LDAP ve Kerberos** kullanan ve sunucularının **domain'e katılmasını** gerektiren eski bir uygulamayı Azure VM'lerine taşıyacak. Şirket domain controller kurmak, yamalamak ve yedeklemek istemiyor. Hangi servisi kullanmalıdır?

- A) Microsoft Entra ID
- B) Microsoft Entra Domain Services
- C) Microsoft Entra External ID
- D) Azure RBAC

<details><summary>Cevap</summary>

**B.** Entra Domain Services; domain join, Group Policy, LDAP ve Kerberos/NTLM'i domain controller yönetimi olmadan, **yönetilen** olarak sunar. Entra ID bu klasik protokolleri sunmaz.
</details>

#### Soru 3: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Microsoft Entra ID, OAuth 2.0, OpenID Connect ve SAML gibi modern protokolleri kullanır. | ☐ | ☐ |
| b) Microsoft Entra ID, sanal makinelerin geleneksel domain join işlemini doğrudan sağlar. | ☐ | ☐ |
| c) Kurum içi Active Directory kullanıcıları Microsoft Entra ID'ye senkronize edilebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Geleneksel domain join (Kerberos/LDAP/GPO) Entra **Domain Services**'tır. Senkronizasyon Entra Connect veya Cloud Sync ile yapılır.
</details>

#### Soru 4: Tek seçim
Kurum içindeki Active Directory kullanıcı hesaplarını Microsoft Entra ID ile senkronize etmek için hangi araç kullanılır?

- A) Azure Migrate
- B) Microsoft Entra Connect
- C) Azure Arc
- D) Microsoft Entra Domain Services

<details><summary>Cevap</summary>

**B.** Entra Connect (Sync) ve daha hafif Entra Cloud Sync, on-prem AD → Entra ID senkronizasyonunu yapar. Domain Services senkronizasyonun yönü tersidir (Entra ID'den yönetilen domain'e).
</details>

#### Soru 5: Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** Microsoft Entra ID'nin faydalarındandır?

- A) Self-service password reset
- B) Sanal makine yedekleme
- C) SaaS uygulamalarına single sign-on
- D) Storage account redundancy

<details><summary>Cevap</summary>

**A ve C.** Entra ID'nin faydaları: authentication (SSPR, MFA, smart lockout), SSO, uygulama ve cihaz yönetimi, identity protection, Conditional Access. B ve D kimlik özelliği değildir.
</details>

#### Soru 6: Eşleştirme
Her ifadeyi **Authentication** veya **Authorization** ile eşleştirin.

1. Kullanıcının iddia ettiği kişi olduğunu kanıtlaması
2. Kullanıcının bir resource group'ta Reader olması
3. Şifre ve Authenticator bildirimiyle giriş
4. Kullanıcının bir VM'i silip silemeyeceğine karar verilmesi

<details><summary>Cevap</summary>

**1 → Authentication · 2 → Authorization · 3 → Authentication · 4 → Authorization.** AuthN "sen kimsin?", AuthZ "ne yapabilirsin?" sorusunu cevaplar. AuthN önce gelir.
</details>

#### Soru 7: Tek seçim
Kullanıcıların **bir kez** giriş yaparak Microsoft 365, Salesforce ve şirket içi web uygulamalarına tekrar şifre girmeden erişmesini sağlayan özellik hangisidir?

- A) Multi-factor authentication
- B) Conditional Access
- C) Single sign-on
- D) Passwordless authentication

<details><summary>Cevap</summary>

**C.** SSO, tek kimlikle birçok uygulamaya tek giriştir. MFA kimliğin gücünü artırır, girişlerin sayısını azaltmaz.
</details>

#### Soru 8: Tek seçim
Aşağıdakilerden hangisi **multi-factor authentication** örneğidir?

- A) Kullanıcının şifresini ve ardından güvenlik sorusunun cevabını girmesi
- B) Kullanıcının şifresini girmesi ve telefonundaki Microsoft Authenticator bildirimini onaylaması
- C) Kullanıcının iki farklı şifre girmesi
- D) Kullanıcının tek bir uzun parola kullanması

<details><summary>Cevap</summary>

**B.** MFA **farklı türde** faktör ister: şifre (bildiğin) + telefon (sahip olduğun). A ve C'deki iki faktör de "bildiğin" türündendir; bu yüzden MFA değildir.
</details>

#### Soru 9: Eşleştirme
Her kimlik doğrulama faktörünü türüyle eşleştirin: **Bildiğin · Sahip olduğun · Olduğun**

1. Parmak izi
2. PIN
3. FIDO2 güvenlik anahtarı
4. Yüz tanıma
5. Şifre

<details><summary>Cevap</summary>

**1 → Olduğun · 2 → Bildiğin · 3 → Sahip olduğun · 4 → Olduğun · 5 → Bildiğin.**
</details>

#### Soru 10: Çoklu seçim (üç doğru cevap)
Microsoft'un passwordless kimlik doğrulama seçenekleri hangileridir?

- A) Windows Hello for Business
- B) SMS ile tek kullanımlık kod + şifre
- C) Microsoft Authenticator (phone sign-in / passkey)
- D) FIDO2 güvenlik anahtarları
- E) Güvenlik soruları

<details><summary>Cevap</summary>

**A, C ve D.** B şifre kullanır (passwordless değildir, klasik MFA'dır); E zayıf bir "bildiğin" faktörüdür.
</details>

#### Soru 11: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Passwordless kimlik doğrulama, şifreye göre phishing saldırılarına daha dayanıklıdır. | ☐ | ☐ |
| b) Windows Hello for Business, kullanıcının cihazına bağlı biyometri veya PIN kullanır. | ☐ | ☐ |
| c) Passwordless yöntemler, kullanıcı deneyimini zorlaştırdığı için yalnızca yöneticilere önerilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Passwordless hem daha güvenli hem daha kolaydır; tüm kullanıcılar için önerilir.
</details>

#### Soru 12: Senaryo
Bir şirket, dış danışmanlık firmasının çalışanlarının **kendi şirket hesaplarıyla** giriş yaparak şirketin SharePoint sitesine ve bir Azure resource group'una erişmesini istiyor. Danışmanlara yeni şifre verilmeyecek. Ne kullanılmalıdır?

- A) Microsoft Entra B2B collaboration
- B) Azure AD B2C / Entra External ID (müşteri)
- C) Microsoft Entra Domain Services
- D) Her danışmana şirket tenant'ında yeni bir kullanıcı hesabı oluşturmak

<details><summary>Cevap</summary>

**A.** B2B, iş ortaklarını kendi kimlikleriyle **misafir (guest)** olarak davet eder. Erişimleri senin tenant'ında grup ve rollerle yönetilir. B müşteriye dönük uygulamalar içindir.
</details>

#### Soru 13: Senaryo
Bir e-ticaret şirketi, mobil uygulamasının **müşterilerinin** Google veya Facebook hesaplarıyla kayıt olup giriş yapabilmesini ve giriş sayfalarının şirket markasıyla özelleştirilmesini istiyor. Hangi çözüm uygundur?

- A) Microsoft Entra B2B collaboration
- B) Müşteri kimliği için Microsoft Entra External ID (eski adıyla B2C senaryosu)
- C) Azure RBAC
- D) Microsoft Entra Connect

<details><summary>Cevap</summary>

**B.** Müşteriye dönük kimlik (CIAM): kayıt, sosyal hesaplar, markalı sayfalar. Azure AD B2C Mayıs 2025'ten beri yeni müşterilere satılmıyor; yeni projeler için Microsoft Entra External ID önerilir. Kavram sınavda "B2C" olarak geçebilir.
</details>

#### Soru 14: Tek seçim
Microsoft Entra Conditional Access'in işlevini **en iyi** hangisi tanımlar?

- A) Kaynakların hangi Azure bölgelerinde oluşturulabileceğini kısıtlar
- B) Kullanıcı, konum, cihaz ve risk gibi sinyalleri değerlendirerek erişime izin verir, MFA ister veya erişimi engeller
- C) Kaynakların kazara silinmesini engeller
- D) Kurum içi AD'yi Entra ID'ye senkronize eder

<details><summary>Cevap</summary>

**B.** Conditional Access, sinyal → karar → uygulama mantığıyla çalışan if-then politika motorudur. A Azure Policy, C resource lock, D Entra Connect'tir.
</details>

#### Soru 15: Senaryo
Bir şirket, kullanıcıların **ofis ağından** girişte yalnızca şifre kullanmasını, ama **ofis dışından** girişte ek olarak MFA yapmasını istiyor. Ne yapılandırılmalıdır?

- A) Security defaults
- B) Bir Conditional Access politikası (konum koşulu → MFA iste)
- C) Azure Policy ile *Allowed locations*
- D) Resource group üzerinde Reader rol ataması

<details><summary>Cevap</summary>

**B.** Konuma bağlı, koşullu MFA Conditional Access'tir (Entra ID P1). Security defaults koşul tanımlamaz. Azure Policy'nin *Allowed locations* kuralı kullanıcı konumunu değil, **kaynakların** bölgesini kısıtlar.
</details>

#### Soru 16: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Conditional Access, cihazın kurumsal politikalara uyumlu olup olmadığını bir sinyal olarak kullanabilir. | ☐ | ☐ |
| b) Security defaults, tüm kullanıcılara MFA kaydı yaptırır ve eski kimlik doğrulama protokollerini engeller. | ☐ | ☐ |
| c) Conditional Access, bir kullanıcının bir VM'i yeniden başlatma iznini belirler. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Kaynak üzerindeki işlem izni Azure RBAC'ın işidir; Conditional Access girişe ve uygulamaya erişime karar verir.
</details>

#### Soru 17: Tek seçim
Bir kullanıcının bir resource group'taki tüm kaynakları **görüntüleyebilmesi** ama hiçbir şeyi değiştirememesi gerekiyor. Hangi built-in rol atanmalıdır?

- A) Owner
- B) Contributor
- C) Reader
- D) User Access Administrator

<details><summary>Cevap</summary>

**C.** Reader yalnızca okuma (`*/read`) izni verir.
</details>

#### Soru 18: Tek seçim
Bir geliştirici ekibi bir resource group'ta kaynak oluşturup silebilmeli, ancak **başka kullanıcılara rol atayamamalıdır**. En az yetki ilkesine göre hangi rol uygundur?

- A) Owner
- B) Contributor
- C) Reader
- D) Global Administrator

<details><summary>Cevap</summary>

**B.** Contributor, erişim yönetimi (`Microsoft.Authorization/*/Write`, `*/Delete`) dışında her şeyi yapar. Owner erişim de yönetebilir (fazla yetki). Global Administrator bir Entra (directory) rolüdür.
</details>

#### Soru 19: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Bir subscription kapsamında atanan rol, o subscription'daki tüm resource group'lara ve kaynaklara miras kalır. | ☐ | ☐ |
| b) Bir rol ataması; bir güvenlik sorumlusu, bir rol tanımı ve bir kapsamdan oluşur. | ☐ | ☐ |
| c) Azure RBAC rolleri yalnızca tek tek kullanıcılara atanabilir, gruplara atanamaz. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Roller kullanıcı, grup, service principal ve managed identity'ye atanabilir. En iyi uygulama gruplara atamaktır.
</details>

#### Soru 20: Senaryo
Bir şirketin 40 subscription'ı var. Güvenlik denetim ekibinin **tüm subscription'lardaki** kaynakları görüntüleyebilmesi gerekiyor ve ileride eklenecek subscription'lara da erişimi otomatik olarak olmalı. En verimli çözüm nedir?

- A) Her subscription'da denetim ekibine ayrı ayrı Reader rolü atamak
- B) Subscription'ları bir management group altında toplayıp denetim ekibinin grubuna o management group kapsamında Reader rolü atamak
- C) Denetim ekibine tenant'ta Global Administrator rolü vermek
- D) Her resource group'a Reader rolü atamak

<details><summary>Cevap</summary>

**B.** Management group kapsamındaki atama, altındaki tüm subscription'lara (yenileri dahil) miras kalır. C hem yanlış türde (Entra rolü) hem en az yetki ilkesine aykırıdır.
</details>

#### Soru 21: Tek seçim
Bir kullanıcının subscription kapsamında **Reader**, aynı subscription'daki `rg-web` resource group'unda ise **Contributor** rolü var. Kullanıcı `rg-web` içinde ne yapabilir?

- A) Yalnızca kaynakları görüntüleyebilir; daha kısıtlayıcı rol geçerlidir
- B) Kaynak oluşturabilir, değiştirebilir ve silebilir; izinler toplanır
- C) Hiçbir şey yapamaz; çakışan roller birbirini iptal eder
- D) Rol atayabilir

<details><summary>Cevap</summary>

**B.** Azure RBAC **additive**'dir: etkin izin, atamaların toplamıdır. `rg-web`'de Contributor geçerlidir. Contributor rol atayamaz (D yanlış).
</details>

#### Soru 22: Tek seçim
Aşağıdaki gereksinimlerden hangisi Azure RBAC ile **değil**, Azure Policy ile karşılanır?

- A) Geliştiricilerin yalnızca `rg-dev` içinde kaynak oluşturabilmesi
- B) Denetçilerin kaynakları yalnızca görüntüleyebilmesi
- C) Hiç kimsenin (Owner dahil) West Europe dışındaki bir bölgede kaynak oluşturamaması
- D) Bir ekibin yalnızca rol atamalarını yönetebilmesi

<details><summary>Cevap</summary>

**C.** RBAC **kullanıcının** ne yapabileceğini, Policy **kaynağın** nasıl olabileceğini (bölge, SKU, tag) belirler. Policy'nin Deny etkisi Owner'ı da durdurur (→ C1.12). A, B ve D rol atamalarıyla çözülür.
</details>

---

**Puanlama:** 20-22 → hazırsın · 15-19 → README 3.4–3.10'daki karşılaştırma tablolarını tekrar et · < 15 → konuyu baştan çalış ve 3.11 laboratuvarını yap.


---

## 2.7 Güvenlik: Zero Trust, Defense in Depth, Defender for Cloud

> **Sınav Alanı:** Describe Azure architecture and services (%35-40) · **Konu Kodu:** C2.7

### 1. Ne?

Bu konu güvenliğe üç farklı açıdan bakar:

| Kavram | Türü | Tek cümle |
|---|---|---|
| **Zero Trust** | Güvenlik **modeli / felsefesi** | "Asla güvenme, her zaman doğrula": hiçbir isteğe ağın neresinden geldiği için güvenme |
| **Defense in depth** | Güvenlik **stratejisi / mimarisi** | Katmanlı savunma: bir katman aşılırsa sıradaki korur |
| **Microsoft Defender for Cloud** | Güvenlik **aracı / servisi** | Azure, kurum içi ve diğer bulutlardaki kaynakların güvenlik duruşunu ölçer, iyileştirir ve tehditleri tespit eder |

Zero Trust **nasıl düşündüğünü**, defense in depth kontrolleri **nereye koyduğunu**, Defender for Cloud ise bunun **ne kadar iyi uygulandığını** gösterir. Kimlik tarafı (Entra ID, MFA, Conditional Access, RBAC) → C1.9.

Bu konunun C# kodu yoktur. Pratik, defense in depth tablo alıştırması (3.2) ve `az` CLI ile Defender for Cloud laboratuvarıdır (3.4).

---

### 2. Neden?

- **Sınav:** Zero Trust'ın **üç ilkesi**, defense in depth'in **katman sırası** ve katman ↔ kontrol eşleştirmesi ile Defender for Cloud'un **hybrid/multicloud** kapsamı sabit sorulardır.
- **Gerçek işte:** Bulutta "iç ağ" ile "dış dünya" arasındaki net sınır kaybolur. Kullanıcılar evden, kafeden, telefondan bağlanır; uygulamalar SaaS'tadır. Yalnızca güvenlik duvarına güvenen bir model, içeri sızan ilk saldırgana her şeyi açar.
- **Shared responsibility (→ C1.1):** Microsoft fiziksel katmanı ve platformu korur. Kimlik, veri, ağ kuralları ve (IaaS'ta) OS güvenliği senin işindir. Defender for Cloud, kendi payına düşen işte nerede eksik kaldığını gösteren araçtır.

---

### 3. Nasıl?

#### 3.1 Zero Trust model ⭐

**Geleneksel model ("kale ve hendek"):** Şirket ağı güvenli kabul edilir; güvenlik duvarı dışarıyı keser, içeridekiler güvenilirdir. Sorun: VPN'le içeri giren çalınmış bir hesap veya ele geçirilmiş bir dizüstü bilgisayar her şeye ulaşır. Üstelik kullanıcılar ve veriler artık zaten "kalenin" dışındadır.

**Zero Trust:** Ağın **içi de güvenli değildir**; her istek, **açık bir ağdan geliyormuş gibi** doğrulanır. Microsoft'un üç ilkesi:

| İlke | Anlamı | Azure'daki karşılığı |
|---|---|---|
| **Verify explicitly** (açıkça doğrula) | Her isteği **tüm sinyallerle** doğrula ve yetkilendir: kimlik, konum, cihaz durumu, servis/iş yükü, veri sınıflandırması, anomaliler | Entra ID + **MFA** + **Conditional Access** (→ C1.9) |
| **Use least privilege access** (en az yetki) | Yalnızca gerektiği kadar (**just-enough-access**) ve yalnızca gerektiği süre (**just-in-time**) yetki ver; risk tabanlı uyarlanabilir politikalar; veriyi koru | Dar kapsamlı **RBAC** rolleri, *(bağlam)* Privileged Identity Management (PIM), VM'ler için just-in-time erişim |
| **Assume breach** (ihlali varsay) | İhlal olmuş gibi tasarla: **etki alanını küçült** (segmentasyon), **uçtan uca şifrele**, analitik ve izlemeyle tehditleri **tespit et** | VNet/subnet segmentasyonu, NSG, **private endpoint** (→ C1.7), şifreleme, Defender for Cloud, Microsoft Sentinel *(bağlam)* |

> Zero Trust bir ürün değildir; satın alınmaz, **uygulanır**. Sınav ilkenin adını tanımla eşleştirir: "her isteği konum ve cihaz durumuyla doğrula" → *verify explicitly*; "yetkiyi yalnızca iş süresince ver" → *least privilege*; "ağı segmentlere ayır, saldırganın yatay hareketini sınırla" → *assume breach*.

#### 3.2 Defense in depth ⭐

**Amaç:** Tek bir güvenlik kontrolüne güvenmemek. Saldırgan bir katmanı aşsa bile sıradaki katman onu **yavaşlatır, durdurur veya tespit eder**; hiçbir katman tek başına "tek arıza noktası" olmaz. Microsoft yedi katmanlı bir model kullanır. Katmanlar **dıştan içe** doğru sıralanır, en içte saldırganın asıl hedefi olan **veri** vardır:

| # | Katman | Ne korunur / amaç | Örnek kontroller ve Azure servisleri | Kimin sorumluluğu (bulutta) |
|---|---|---|---|---|
| 1 | **Fiziksel güvenlik** | Datacenter binası ve donanım | Kartlı/biyometrik giriş, kamera, disk imhası | **Microsoft** |
| 2 | **Kimlik ve erişim** | Altyapıya ve değişiklik kontrolüne erişim | **SSO, MFA**, Conditional Access, denetim log'ları; Entra ID (→ C1.9) | Müşteri (platform Microsoft'ta) |
| 3 | **Çevre (perimeter)** | Ağın dış sınırı, büyük ölçekli saldırılar | **Azure DDoS Protection**, çevre güvenlik duvarı (Azure Firewall) | Paylaşımlı |
| 4 | **Ağ** | Kaynaklar arası iletişim, yatay hareket | **Segmentasyon** (VNet/subnet), **NSG**, varsayılan olarak engelle, internete erişimi kısıtla, private endpoint | Müşteri |
| 5 | **Compute** | Sanal makineler ve sunucular | VM erişimini kısıtla, **yamaları güncel tut**, uç nokta koruması (antimalware), Defender for Servers | IaaS'ta müşteri |
| 6 | **Uygulama** | Uygulama kodu ve yapılandırması | Güvenli geliştirme (zafiyetsiz kod), **secret'ları koda değil Azure Key Vault'a** koymak, Web Application Firewall | Müşteri |
| 7 | **Veri** | Verinin kendisi (veritabanı, disk, SaaS verisi) | **Şifreleme** (durağan ve aktarımda), erişim kontrolü, sınıflandırma, yedek | **Her zaman müşteri** |

Hatırlatma cümlesi (dıştan içe): **F**iziksel → **K**imlik → **Ç**evre → **A**ğ → **C**ompute → **U**ygulama → **V**eri.

> **Alıştırma — kendi mimarin için defense in depth tablosu.** Mimari: internetten erişilen bir **App Service** web uygulaması, arkasında **Azure SQL Database**, yüklenen dosyalar için bir **storage account** ve yönetim için bir **Linux VM** (jump box). Her katmana en az bir somut kontrol yaz:
>
> | Katman | Senin kontrolün |
> |---|---|
> | Fiziksel güvenlik | |
> | Kimlik ve erişim | |
> | Çevre | |
> | Ağ | |
> | Compute | |
> | Uygulama | |
> | Veri | |
>
> <details><summary>Çözüm (örnek)</summary>
>
> | Katman | Kontrol |
> |---|---|
> | Fiziksel güvenlik | Microsoft'un datacenter kontrolleri. Müşteri tarafında yapılacak bir şey yok; denetim raporları Service Trust Portal'da (→ C1.12) |
> | Kimlik ve erişim | Tüm yöneticilere MFA ve Conditional Access; RBAC ile ekiplere dar kapsamlı roller; uygulamanın SQL/storage'a erişimi için şifre yerine **managed identity** (→ C2.11) |
> | Çevre | Web uygulamasının önünde **WAF**'lı Application Gateway veya Front Door; **DDoS Protection** |
> | Ağ | SQL ve storage için **private endpoint**, public erişim kapalı (→ C1.7); jump box subnet'inde NSG ile SSH yalnızca şirket IP'sinden; varsayılan engelle |
> | Compute | Jump box'ta otomatik OS yaması, Defender for Servers, just-in-time VM erişimi (port yalnızca istek anında ve süreli açılır) |
> | Uygulama | Connection string ve API anahtarları **Key Vault**'ta; App Service'te yalnızca HTTPS ve güncel TLS; bağımlılık zafiyet taraması |
> | Veri | SQL'de Transparent Data Encryption (varsayılan açık), storage'da durağan şifreleme (varsayılan açık), hassas sütunlar için sınıflandırma (Purview → C1.12), düzenli yedek ve geri yükleme testi |
>
> Gözlem: Bir katman (ör. uygulamadaki bir SQL injection zafiyeti) aşılsa bile private endpoint, en az yetkili managed identity ve şifreleme hasarı sınırlar. Defense in depth tam olarak budur.
> </details>

#### 3.3 Microsoft Defender for Cloud

**Microsoft Defender for Cloud**, bir **cloud-native application protection platform (CNAPP)**'tır. İki büyük yeteneği vardır:

| Yetenek | Kısaltma | Ne yapar |
|---|---|---|
| **Cloud security posture management** | **CSPM** | Kaynakların **yapılandırmasını** sürekli değerlendirir, zafiyetleri ve yanlış ayarları bulur, **öneriler** ve **secure score** üretir. "Kapı kilitli mi?" sorusu |
| **Cloud workload protection** | **CWP** | Çalışan iş yüklerine (sunucu, container, veritabanı, storage…) yönelik **tehditleri tespit eder** ve **uyarı** üretir. "Biri kapıyı zorluyor mu?" sorusu |

**Kapsam — yalnızca Azure değil** ⭐:

- **Azure:** Azure'un kendi kaynakları; PaaS servisleri için ajan gerekmeden yerleşik.
- **Kurum içi (on-prem) ve diğer bulutlardaki sunucular:** **Azure Arc** ile Azure'a bağlanır (→ C1.13).
- **Diğer bulutlar:** **AWS** ve **Google Cloud (GCP)** hesapları, **connector**'larla ajan gerektirmeden bağlanır.
- *(bağlam)* Kod depoları (GitHub, Azure DevOps, GitLab): DevOps güvenliği.

**Üç ihtiyaç: Assess → Secure → Defend**

| Aşama | Ne yapar | Örnek |
|---|---|---|
| **Assess** (sürekli değerlendir) | Güvenlik durumunu sürekli değerlendirir, VM ve container image'larında **zafiyet taraması** yapar | "Bu VM'de kritik bir yama eksik" |
| **Secure** (güçlendir) | **Microsoft cloud security benchmark** (MCSB; Azure Policy tabanlı varsayılan güvenlik girişimi) ile kaynakları karşılaştırır; **öneriler** ve **secure score** sunar; regulatory compliance panosu (ISO 27001, PCI DSS… standartlarına göre) | "Storage account public erişime açık → kapat", secure score %62 → %71 |
| **Defend** (tehditlere karşı koru) | **Güvenlik uyarıları** (ör. olağandışı konumdan erişim, kaba kuvvet denemesi, şüpheli işlem), MITRE ATT&CK eşlemesi, olay müdahalesi, *(bağlam)* just-in-time VM erişimi, Microsoft Sentinel'e (SIEM) aktarım | "VM'e 500 başarısız SSH denemesi", "storage'dan olağandışı miktarda veri indirildi" |

**Secure score:** Önerilerin uygulanma oranına göre hesaplanan güvenlik duruşu **yüzdesidir**. Öneriler **güvenlik kontrolleri** (security controls) altında gruplanır. Bir kontrolün puanını almak için o kontroldeki önerilerin **tüm** ilgili kaynaklarda düzeltilmesi gerekir. Yüksek puan = daha az açık. Puan **düşük riskli** demek değildir; yalnızca bilinen önerilere uyumu ölçer.

**Planlar** *(bağlam — adlar sınavda geçebilir)*:

- **Foundational CSPM** — **ücretsiz**; Defender for Cloud açıldığı anda gelir: temel değerlendirmeler, öneriler, secure score, MCSB.
- **Defender CSPM** — ücretli; ajansız zafiyet taraması, saldırı yolu analizi (attack path), cloud security graph, veri farkındalıklı duruş.
- **CWP planları** — her iş yükü türü için ayrı açılır (ücretli): **Defender for Servers** (Plan 1 / Plan 2), Defender for Storage, Defender for Databases (SQL, açık kaynak veritabanları, Cosmos DB), Defender for Containers, Defender for App Service, Defender for Key Vault, Defender for Resource Manager, Defender for APIs, AI iş yükleri için tehdit koruması.

**Defender for Cloud ile karıştırılan araçlar:**

| Araç | Odak | Fark |
|---|---|---|
| **Defender for Cloud** | **Güvenlik** duruşu + tehdit tespiti (Azure, hybrid, multicloud) | Güvenlik uzmanı aracı |
| **Azure Policy** (→ C1.12) | **Kaynak kurallarını** zorla/denetle (bölge, SKU, tag, güvenlik ayarı) | Defender'ın güvenlik önerileri altta Policy tanımlarıyla değerlendirilir; Policy **genel yönetişim** aracıdır |
| **Azure Advisor** (→ C1.14) | Beş kategoride **genel öneri**: reliability, security, performance, cost, operational excellence | Advisor'ın **Security** kategorisindeki öneriler **Defender for Cloud'dan** gelir |
| **Microsoft Sentinel** *(bağlam)* | **SIEM/SOAR**: birçok kaynaktan log toplayıp olay korelasyonu ve otomatik müdahale | Defender uyarılarını da tüketir; güvenlik operasyon merkezinin (SOC) aracı |

#### 3.4 Pratik — Defender for Cloud: secure score ve bir öneriyi uygulamak (`az` CLI)

Foundational CSPM **ücretsizdir** ve subscription'da varsayılan olarak etkindir. Aşağıdaki laboratuvar yalnızca **küçük bir storage account** oluşturur (kuruşlar seviyesinde ücret). **Ücretli Defender planlarını açma**; açtıysan laboratuvar sonunda Free'ye geri al. Bütçe alarmın kurulu olsun (→ C1.1).

```bash
az login

# 1) Current security posture: secure score and controls (read-only)
az security secure-scores list --output table
az security secure-score-controls list --output table

# 2) Which Defender plans are on? (Free = Foundational CSPM only; Standard = paid plan)
az security pricing list --query "value[].{plan:name, tier:pricingTier}" --output table

# 3) A deliberately "bad" storage account: it allows anonymous blob access
az group create --name rg-az900-c110 --location westeurope
ST=staz900c110$RANDOM
az storage account create --name "$ST" --resource-group rg-az900-c110 --location westeurope \
  --sku Standard_LRS --allow-blob-public-access true

# 4) Assessments: list the unhealthy ones
#    Note: it can take a few hours (up to ~24 hours) for a new resource to show up in assessments.
az security assessment list \
  --query "[?status.code=='Unhealthy'].{recommendation:displayName, resource:resourceDetails.Id}" --output table

# 5) Apply the recommendation: disable anonymous blob access
az storage account update --name "$ST" --resource-group rg-az900-c110 --allow-blob-public-access false
az storage account show --name "$ST" --resource-group rg-az900-c110 --query allowBlobPublicAccess

# 6) Cleanup
az group delete --name rg-az900-c110 --yes --no-wait
```

Beklenen gözlem: Değerlendirme yenilendiğinde *"Storage account public access should be disallowed"* türündeki öneri o kaynak için **Healthy** olur ve ilgili kontrolün puanı (dolayısıyla secure score) artar. Kontroldeki **tüm** kaynaklar düzeltilmeden kontrolün tam puanı alınmaz. CLI çıktı alanları API sürümüne göre değişebilir; aynı bilgi portalda *Defender for Cloud → Recommendations / Secure score* ekranında da görünür.

---

### 4. Gerçek dünya senaryosu

Bir sağlık teknolojisi şirketi, hasta verisi işleyen platformunu Azure'da çalıştırıyor. Ayrıca eski bir raporlama sistemi kendi datacenter'ında, bir analitik iş yükü ise AWS'te.

1. **Zero Trust'a geçiş:** VPN'e bağlanan herkesin her şeye erişebildiği eski model bırakılıyor. *Verify explicitly*: tüm girişler Entra ID + MFA + Conditional Access'ten geçiyor ve yönetilmeyen cihazdan hasta verisine erişim yok. *Least privilege*: geliştiricilerin üretimdeki yetkileri yalnızca talep edildiğinde ve 4 saatliğine açılıyor. *Assume breach*: veritabanları private endpoint arkasına alınıp ağ segmentlere ayrılıyor.
2. **Defense in depth:** Çevrede DDoS Protection ve WAF; ağda NSG ve private endpoint; compute'ta otomatik yama ve Defender for Servers; uygulamada secret'lar Key Vault'ta; veride şifreleme ve sınıflandırma. Pentest'te uygulamada bir zafiyet bulunuyor, ama saldırgan simülasyonu en az yetkili managed identity ve private endpoint yüzünden veritabanının dışına çıkamıyor.
3. **Defender for Cloud:** Azure subscription'ları, Azure Arc ile bağlanan kurum içi sunucular ve connector ile bağlanan AWS hesabı **tek panelde**. Başlangıç secure score'u %48. İlk sprintte en yüksek puanlı öneriler (MFA, public erişimli storage, eksik yamalar) kapatılıyor ve puan %74'e çıkıyor. Bir gece AWS'teki bir sunucuda olağandışı bir işlem için **güvenlik uyarısı** üretiliyor ve olay ekibi sunucuyu izole ediyor.
4. **Uyumluluk:** Regulatory compliance panosu, ilgili standartlara (ör. ISO 27001) göre hangi kontrollerin eksik olduğunu denetçiye rapor olarak sunuyor.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Zero Trust'ta **ilke ↔ tanım** eşleştirmesi; defense in depth'te **katman sırası** (sıralama sorusu) ve **kontrol ↔ katman** eşleştirmesi; Defender for Cloud'da **ne yaptığı** ve **nerede çalıştığı** (evet/hayır tabloları).

| Soru kalıbı | Doğru cevap | Tuzak |
|---|---|---|
| "Asla güvenme, her zaman doğrula" | **Zero Trust** | Defense in depth |
| "Ağın içindeki kullanıcıya otomatik güvenilir" | Geleneksel (perimeter) model, **Zero Trust değil** | |
| "Her isteği kimlik, konum ve cihaz sinyalleriyle doğrula" | **Verify explicitly** | Least privilege |
| "Just-in-time / just-enough-access" | **Use least privilege access** | Assume breach |
| "Etki alanını segmentasyonla küçült, şifrele, analitikle tespit et" | **Assume breach** | Verify explicitly |
| "Bir katman aşılsa bile diğerleri korur" | **Defense in depth** | Zero Trust |
| "Katmanların en dışı / en içi" | **Fiziksel güvenlik** / **Veri** | Çevre / Uygulama |
| "DDoS koruması hangi katman?" | **Çevre (perimeter)** | Ağ |
| "Segmentasyon, NSG, varsayılan engelle" | **Ağ** | Çevre |
| "Yamalar, VM erişimini kısıtlama, antimalware" | **Compute** | Uygulama |
| "Secret'ları Key Vault'ta saklamak, güvenli kod" | **Uygulama** | Veri |
| "MFA, SSO, erişim denetimi" | **Kimlik ve erişim** | Çevre |
| "Şifreleme ve verinin kendisine erişim kontrolü" | **Veri** | |
| "Azure, on-prem ve AWS/GCP kaynaklarının güvenlik duruşunu izle, tehditleri tespit et" | **Microsoft Defender for Cloud** | Azure Policy, Advisor |
| "Güvenlik duruşunu yüzde olarak gösteren ölçü" | **Secure score** | Advisor score |

**Anahtar kelimeler:** *never trust, always verify*, *verify explicitly*, *least privilege*, *assume breach*, *layered approach*, *slow the advance of an attack*, *CSPM*, *CWP*, *secure score*, *recommendations*, *security alerts*, *hybrid and multicloud*.

**Evet/hayır tuzakları:**
- "Defender for Cloud yalnızca Azure kaynaklarını korur" → **Hayır** (on-prem Arc ile, AWS/GCP connector'larla).
- "Defense in depth'te veri katmanı genellikle saldırganın asıl hedefidir" → **Evet**.
- "Zero Trust, güvenlik duvarını gereksiz kılar" → **Hayır** (Zero Trust tek bir kontrole güvenmez; çevre kontrolleri yine kullanılır).
- "Secure score yükseldikçe güvenlik duruşu iyileşir" → **Evet**.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "Zero Trust bir ürün, satın alırız" | Bir model/felsefedir; Entra ID, Conditional Access, RBAC, segmentasyon gibi araçlarla **uygulanır** |
| "VPN'le bağlanan kullanıcı içeridedir, güvenilirdir" | Zero Trust'ta konum tek başına güven gerekçesi değildir; her istek doğrulanır |
| Defense in depth'i "çok sayıda firewall" sanmak | Farklı **türde** kontrollerin farklı **katmanlarda** bulunmasıdır (kimlik, ağ, compute, uygulama, veri) |
| Katman sırasını karıştırmak (ör. ağı çevrenin dışına koymak) | Dıştan içe: fiziksel → kimlik → çevre → ağ → compute → uygulama → veri |
| "Bulutta fiziksel güvenlik benim işim" | Fiziksel katman bulutta **Microsoft'tadır** (→ C1.1) |
| "Defender for Cloud = antivirüs" | Antimalware/uç nokta koruması bir parçasıdır; asıl amaç duruş yönetimi (CSPM) + iş yükü koruması (CWP) |
| "Secure score %100 = güvendeyim" | Yalnızca bilinen önerilere uyumu ölçer; tehdit tespiti, olay müdahalesi ve süreçler yine gerekir |
| Tüm ücretli Defender planlarını denemek için açıp unutmak | Planlar kaynak başına ücretlidir; deneme sonrası Free'ye geri al, bütçe alarmı kur |
| "Defender for Cloud ile Azure Policy aynı şey" | Defender güvenlik odaklıdır ve önerileri Policy tanımlarıyla değerlendirir; Policy genel yönetişim aracıdır (→ C1.12) |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Zero Trust (her isteği doğrula) | İçeri sızan saldırganın yatay hareketi sınırlanır; uzaktan çalışma ve SaaS için uygun | Kimlik, cihaz yönetimi ve politika yatırımı; yanlış ayarlanmış politika kullanıcı sürtünmesi yaratır |
| Just-in-time / en az yetki | Kalıcı yüksek yetki yok; ele geçirilen hesabın etkisi küçük | Erişim talep/onay süreci; acil durumlarda gecikme (break-glass hesabı gerekir) |
| Segmentasyon ve private endpoint (assume breach) | Bir bileşenin ihlali tüm ortama yayılmaz | Ağ tasarımı ve DNS karmaşıklığı (→ C1.7) |
| Defense in depth (çok katman) | Tek kontrol hatası felaket olmaz; saldırı yavaşlar ve tespit edilir | Maliyet, yönetilecek daha çok bileşen, olası gecikme (WAF, ek doğrulama) |
| Foundational CSPM (ücretsiz) | Secure score, öneriler ve MCSB sıfır maliyetle | Tehdit tespiti, saldırı yolu analizi ve gelişmiş tarama yok |
| Ücretli Defender planları (CWP, Defender CSPM) | Tehdit tespiti, zafiyet taraması, uyarılar | Kaynak başına aylık ücret; uyarı hacmi için güvenlik ekibi/süreç gerekir |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure identity, access, and security](https://learn.microsoft.com/en-us/training/modules/describe-azure-identity-access-security/) (Zero Trust, defense in depth, Defender for Cloud üniteleri)
- [Zero Trust guidance center](https://learn.microsoft.com/en-us/security/zero-trust/) · [Zero Trust principles](https://learn.microsoft.com/en-us/security/zero-trust/zero-trust-overview)
- [What is Microsoft Defender for Cloud?](https://learn.microsoft.com/en-us/azure/defender-for-cloud/defender-for-cloud-introduction) · [Secure score](https://learn.microsoft.com/en-us/azure/defender-for-cloud/secure-score-security-controls)
- [Cloud security posture management (Foundational CSPM / Defender CSPM)](https://learn.microsoft.com/en-us/azure/defender-for-cloud/concept-cloud-security-posture-management) · [Defender for Cloud pricing](https://azure.microsoft.com/pricing/details/defender-for-cloud/)
- [Microsoft cloud security benchmark](https://learn.microsoft.com/en-us/security/benchmark/azure/introduction)
- [`az security`](https://learn.microsoft.com/en-us/cli/azure/security) · [Prevent anonymous read access to blobs](https://learn.microsoft.com/en-us/azure/storage/blobs/anonymous-read-access-prevent)


### 📝 Bölüm 2.7 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
"Asla güvenme, her zaman doğrula" ifadesi hangi güvenlik modelini tanımlar?

- A) Defense in depth
- B) Zero Trust
- C) Shared responsibility model
- D) Perimeter (kale ve hendek) modeli

<details><summary>Cevap</summary>

**B.** Zero Trust, ağın içi dahil hiçbir isteğe otomatik güvenmez ve her isteği doğrular. Perimeter modeli (D) tam tersine iç ağa güvenir.
</details>

#### Soru 2: Eşleştirme
Her uygulamayı Zero Trust ilkesiyle eşleştirin: **Verify explicitly · Use least privilege access · Assume breach**

1. Yöneticiye üretim ortamında yalnızca talep ettiğinde ve 2 saatliğine yetki vermek
2. Her girişte kullanıcı kimliğini, konumunu ve cihazın uyumlu olup olmadığını değerlendirmek
3. Ağı segmentlere ayırıp bir sunucunun ele geçirilmesinin diğerlerine yayılmasını engellemek
4. Trafiği uçtan uca şifrelemek ve analitikle anormal davranışı tespit etmek

<details><summary>Cevap</summary>

**1 → Least privilege** (just-in-time) · **2 → Verify explicitly** (tüm sinyallerle doğrula) · **3 → Assume breach** (etki alanını küçült) · **4 → Assume breach** (şifrele, tespit et).
</details>

#### Soru 3: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Zero Trust modelinde, şirket ağından gelen isteklere otomatik olarak güvenilir. | ☐ | ☐ |
| b) Zero Trust, satın alınıp kurulan tek bir Microsoft ürünüdür. | ☐ | ☐ |
| c) Conditional Access, Zero Trust'ın "verify explicitly" ilkesini uygulamaya yardımcı olur. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Hayır · c) Evet.** Zero Trust konuma göre güven vermez; bir model/stratejidir, ürün değildir. Conditional Access kimlik, konum, cihaz ve risk sinyallerini değerlendirir.
</details>

#### Soru 4: Tek seçim
Defense in depth stratejisinin **temel amacı** nedir?

- A) Tüm güvenliği tek ve çok güçlü bir güvenlik duvarında toplamak
- B) Birden çok güvenlik katmanı kullanarak bir katman aşıldığında diğerlerinin saldırıyı yavaşlatmasını veya durdurmasını sağlamak
- C) Güvenlik sorumluluğunu tamamen bulut sağlayıcısına devretmek
- D) Kullanıcıların tek girişle birden çok uygulamaya erişmesini sağlamak

<details><summary>Cevap</summary>

**B.** Katmanlı savunmada hiçbir kontrol tek arıza noktası değildir. A tam tersidir; D SSO'dur.
</details>

#### Soru 5: Sıralama
Defense in depth katmanlarını **dıştan içe** doğru sıralayın: Ağ · Veri · Fiziksel güvenlik · Uygulama · Kimlik ve erişim · Compute · Çevre (perimeter)

<details><summary>Cevap</summary>

**Fiziksel güvenlik → Kimlik ve erişim → Çevre → Ağ → Compute → Uygulama → Veri.** En içte, saldırganın asıl hedefi olan veri bulunur.
</details>

#### Soru 6: Tek seçim
Azure DDoS Protection, defense in depth modelinin hangi katmanında yer alır?

- A) Veri
- B) Uygulama
- C) Çevre (perimeter)
- D) Compute

<details><summary>Cevap</summary>

**C.** Çevre katmanı ağın dış sınırını büyük ölçekli saldırılardan (DDoS) ve çevre güvenlik duvarlarıyla korur.
</details>

#### Soru 7: Eşleştirme
Her kontrolü defense in depth katmanıyla eşleştirin: **Kimlik ve erişim · Ağ · Compute · Uygulama · Veri**

1. VM'lerde işletim sistemi yamalarını güncel tutmak
2. Connection string'leri kodda değil Azure Key Vault'ta saklamak
3. Subnet'ler arasında NSG ile varsayılan olarak trafiği engellemek
4. Tüm yöneticiler için MFA zorunluluğu
5. Veritabanını durağan halde (at rest) şifrelemek

<details><summary>Cevap</summary>

**1 → Compute · 2 → Uygulama · 3 → Ağ · 4 → Kimlik ve erişim · 5 → Veri.**
</details>

#### Soru 8: Tek seçim
Defense in depth modelinde, saldırganların **asıl hedefi** genellikle hangi katmandaki varlıktır ve bu katmanın korunması bulutta kimin sorumluluğundadır?

- A) Fiziksel güvenlik — Microsoft
- B) Veri — her zaman müşteri
- C) Çevre — Microsoft
- D) Compute — her zaman Microsoft

<details><summary>Cevap</summary>

**B.** En içteki veri katmanı asıl hedeftir. Shared responsibility'ye göre bilgi ve veri her modelde müşteridedir (→ C1.1).
</details>

#### Soru 9: Tek seçim
Microsoft Defender for Cloud'un amacını **en iyi** hangisi tanımlar?

- A) Kaynakların hangi bölgede oluşturulabileceğini zorlamak
- B) Kaynakların güvenlik duruşunu sürekli değerlendirmek, öneriler sunmak ve iş yüklerine yönelik tehditleri tespit etmek
- C) Bulut harcamasını izlemek ve bütçe uyarısı göndermek
- D) Kullanıcıların uygulamalara tek girişle erişmesini sağlamak

<details><summary>Cevap</summary>

**B.** Defender for Cloud = CSPM (duruş yönetimi, öneriler, secure score) + CWP (iş yükü koruması, tehdit uyarıları). A Azure Policy, C Cost Management, D SSO'dur.
</details>

#### Soru 10: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Defender for Cloud yalnızca Azure'da çalışan kaynakları izleyebilir. | ☐ | ☐ |
| b) Kurum içi sunucular Azure Arc aracılığıyla Defender for Cloud'a bağlanabilir. | ☐ | ☐ |
| c) Defender for Cloud, AWS ve Google Cloud kaynaklarının güvenlik duruşunu da değerlendirebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Defender for Cloud hybrid ve multicloud'dur: on-prem için Azure Arc, AWS/GCP için connector'lar.
</details>

#### Soru 11: Tek seçim
Defender for Cloud'da, önerilerin uygulanma durumuna göre bir subscription'ın güvenlik duruşunu **yüzde** olarak gösteren ölçü hangisidir?

- A) Advisor score
- B) Secure score
- C) SLA
- D) Compliance score

<details><summary>Cevap</summary>

**B.** Secure score, güvenlik kontrolleri altındaki önerilerin ne kadarının uygulandığını gösterir. Yükseldikçe duruş iyileşir.
</details>

#### Soru 12: Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** Microsoft Defender for Cloud'un yeteneklerindendir?

- A) Bir VM'e yönelik kaba kuvvet (brute-force) giriş denemelerinde güvenlik uyarısı üretmek
- B) Kaynakları kazara silinmeye karşı kilitlemek
- C) Yanlış yapılandırılmış kaynaklar için güvenlik önerileri sunmak
- D) Kurum içi AD kullanıcılarını Entra ID'ye senkronize etmek

<details><summary>Cevap</summary>

**A ve C.** A tehdit tespiti (Defend/CWP), C duruş önerisidir (Secure/CSPM). B resource lock (→ C1.12), D Entra Connect'tir (→ C1.9).
</details>

#### Soru 13: Senaryo
Bir şirket Azure'da, kendi datacenter'ında ve AWS'te sunucular çalıştırıyor. Güvenlik ekibi bu ortamların **hepsinin** güvenlik açıklarını ve tehditlerini **tek bir panelden** görmek istiyor. Hangi çözüm uygundur?

- A) Azure Advisor
- B) Azure Policy
- C) Microsoft Defender for Cloud (kurum içi sunucular için Azure Arc ve AWS connector'ı ile)
- D) Azure Service Health

<details><summary>Cevap</summary>

**C.** Defender for Cloud; Azure, kurum içi (Arc) ve AWS/GCP (connector) kaynaklarını tek panelde değerlendirir ve korur. Advisor yalnızca Azure kaynakları için genel öneri verir; Service Health Azure platform olaylarını bildirir.
</details>

#### Soru 14: Tek seçim
Azure Advisor'ın **Security** kategorisinde gösterilen öneriler nereden gelir?

- A) Azure Service Health
- B) Microsoft Defender for Cloud
- C) Azure Cost Management
- D) Microsoft Purview

<details><summary>Cevap</summary>

**B.** Advisor'ın güvenlik önerileri Defender for Cloud değerlendirmelerinden beslenir; Advisor bunları diğer dört kategoriyle (reliability, performance, cost, operational excellence) birlikte gösterir (→ C1.14).
</details>

#### Soru 15: Senaryo
Bir uygulamada SQL injection zafiyeti bulundu. Ancak saldırganın simülasyonu, veritabanının private endpoint arkasında olması, uygulamanın yalnızca tek tabloya okuma yetkisi olan bir kimlik kullanması ve verilerin şifreli olması nedeniyle sınırlı veriye ulaşabildi. Bu durum en iyi hangi kavramı örnekler?

- A) Single sign-on
- B) Defense in depth
- C) Consumption-based model
- D) High availability

<details><summary>Cevap</summary>

**B.** Uygulama katmanı aşılsa bile ağ (private endpoint), kimlik (en az yetki) ve veri (şifreleme) katmanları hasarı sınırladı. Katmanlı savunmanın amacı budur (bu kontrollerin bir kısmı Zero Trust'ın "least privilege" ve "assume breach" ilkeleriyle de örtüşür).
</details>

---

**Puanlama:** 14-15 → hazırsın · 11-13 → README 3.1 (üç ilke) ve 3.2 (katman tablosu) bölümlerini tekrar et · < 11 → konuyu baştan çalış ve 3.2'deki alıştırmayı yap.


---

# BÖLÜM 3: Describe Azure Management and Governance (%30-35)

---

## 3.1 Maliyet Yönetimi: Pricing Calculator, TCO, Budgets

> **Sınav Alanı:** Describe Azure management and governance (%30-35) · **Konu Kodu:** C3.1

### 1. Ne?

Bulutta maliyet **tüketime bağlıdır** (→ C1.1): ne kadar, ne tür ve nerede kullandığına göre değişir. Maliyet yönetimi üç zamana ayrılır:

| Zaman | Soru | Araç |
|---|---|---|
| **Önce** (tahmin) | "Bu mimari ayda kaça mal olur?" · "On-prem'e göre ne kazanırım?" | **Pricing Calculator** · on-prem kıyası (TCO / Azure Migrate business case) |
| **Sırasında** (izleme) | "Şu an ne harcıyorum, nereye gidiyor, bütçeyi aşacak mıyım?" | **Microsoft Cost Management**: cost analysis, budgets, alerts |
| **Sonra** (iyileştirme) | "Nerede tasarruf ederim?" | Advisor önerileri, reservation, Hybrid Benefit, kapatma/silme |

Bunların hepsini bağlayan kavram **tag**'dir: maliyeti departmana, projeye, ortama göre raporlamanın yolu.


> Koddaki tüm fiyatlar **örnektir**; amaç maliyet kalemlerinin **yapısını** görmek, gerçek teklif çıkarmak değil.

---

### 2. Neden?

- **Sınav:** "Management and governance" alanının (%30-35) ilk hedefidir; araç seçimi (Pricing Calculator ↔ Cost Management ↔ TCO), maliyet faktörü ve tag soruları her sınavda çıkar.
- **Gerçek işte:** Bulut faturası sürprizleri neredeyse hep aynı sebeplerden gelir: unutulan kaynaklar, yanlış boyut, bölgeler arası/internete çıkan trafik, 7/24 sabit yükte pay-as-you-go. Bunları önceden bilmek ve **bütçe alarmıyla** yakalamak FinOps'un temelidir.
- **Yönetişimle bağı:** Tag'lerin zorunlu kılınması ve gruptan kopyalanması Azure Policy ile yapılır (→ C1.12); "kim ne harcıyor?" sorusu tag disiplini olmadan cevaplanamaz.

---

### 3. Nasıl?

#### 3.1 Maliyeti etkileyen faktörler ⭐ (`Bölüm 01`)

| Faktör | Açıklama | Örnek |
|---|---|---|
| **Kaynak türü ve ayarları** | Servis, SKU/boyut, tier, redundancy, performans seviyesi | 2 vCPU → 8 vCPU VM; LRS → GZRS depolama; Hot ↔ Archive |
| **Tüketim** | Pay-as-you-go ↔ taahhütlü indirim (**reservations**, **savings plan**) | 7/24 VM'de 1-3 yıllık reservation ciddi indirim sağlar |
| **Bakım** | Kullanılmayan/unutulan kaynaklar faturada kalır | VM silindi ama **disk, public IP, NIC** kaldı; test ortamı kapatılmadı |
| **Coğrafya (bölge)** | Aynı servis bölgeye göre farklı fiyattadır (enerji, vergi, işçilik) | Aynı VM boyutu iki bölgede farklı fiyat |
| **Ağ trafiği** | **Gelen** (ingress) veri genellikle **ücretsiz**; **giden** (egress) — internete, bölgeler arası, hatta zone'lar arası — **ücretli** | Büyük dosya indirme hizmeti; bölgeler arası replikasyon |
| **Subscription türü** | Ücretsiz hesap kredisi ve ücretsiz kullanım miktarları; EA/CSP sözleşme fiyatları | Free account kotası bitince pay-as-you-go'ya geçiş |
| **Azure Marketplace** | Üçüncü taraf ürünlerde Azure ücretine ek olarak **satıcının ücreti** | Ticari bir güvenlik duvarı imajı |

**Tasarruf yolları** (`Bölüm 03`):

| Yöntem | Ne? | Ne zaman? |
|---|---|---|
| **Reservations** | Belirli kaynak (ör. VM boyutu + bölge) için 1/3 yıllık taahhüt | Sabit, uzun süreli yük |
| **Savings plan for compute** | Saatlik compute harcama **tutarına** 1/3 yıllık taahhüt; boyut/bölge esnek | Sürekli ama değişen compute |
| **Azure Hybrid Benefit** | Mevcut (Software Assurance'lı) **Windows Server / SQL Server** lisanslarını Azure'da kullanmak | Lisansı olan kuruluşlar |
| **Spot VM** | Boşta kapasite, büyük indirim, kesilebilir | Kesintiye dayanıklı toplu işler |
| **Dev/test fiyatlandırması** | Dev/test aboneliklerinde indirimli oranlar | Yalnızca üretim dışı |
| **Doğru boyutlandırma ve kapatma** | Advisor önerileri, otomatik kapatma (auto-shutdown), deallocate, kullanılmayanı silme | Her zaman |

#### 3.2 Pricing Calculator ve on-prem karşılaştırması (TCO)

**Azure Pricing Calculator** — henüz **kurulmamış** bir mimarinin **tahmini** aylık maliyeti:

1. Servisleri ekle (ör. App Service + Azure SQL Database + Storage).
2. Her biri için **bölge, tier/SKU, adet, çalışma saati, depolama miktarı, trafik** seç.
3. Satın alma seçeneklerini karşılaştır (pay-as-you-go, 1/3 yıl reservation, Hybrid Benefit).
4. Tahmini **dışa aktar** (Excel) veya paylaş; Microsoft hesabıyla girişte tahminleri kaydet.

> Pricing Calculator bir **tahmin**dir, fatura değildir: gerçek tüketim, trafik ve indirimler sonucu değiştirir. Calculator'a erişmek için Azure aboneliği gerekmez.

**TCO (Total Cost of Ownership) — on-prem vs Azure.** Klasik **TCO Calculator**, mevcut on-prem altyapını (sunucu, depolama, ağ, lisans, enerji, datacenter alanı, işçilik) girip aynı iş yükünün Azure'daki 3-5 yıllık toplam maliyetiyle karşılaştırmanı sağlıyordu. *(Durum notu — Ekim 2026: Bazı güncel AZ-900 kaynakları bağımsız TCO Calculator aracının 2025'te kaldırıldığını ve on-prem ↔ Azure karşılaştırmasının artık **Azure Migrate business case** özelliğiyle yapıldığını belirtiyor; Microsoft'un resmî duyurusunu doğrulayamadık. Eski materyallerde ve sorularda TCO Calculator geçebilir — sınav için ayrım aynı kalır.)*

| | Pricing Calculator | TCO (TCO Calculator / Azure Migrate business case) |
|---|---|---|
| Soru | "Azure'da bu ne kadar tutar?" | "On-prem'de kalmak mı, Azure'a geçmek mi ucuz?" |
| Girdi | Azure servisleri ve ayarları | Mevcut on-prem altyapı + varsayımlar (enerji, işçilik…) |
| Çıktı | Aylık tahmini Azure maliyeti | Çok yıllık toplam sahip olma maliyeti karşılaştırması |


#### 3.3 Microsoft Cost Management

Azure (ve bağlanırsa AWS) harcamasını **izleme, analiz etme ve optimize etme** servisi; Azure portalında *Cost Management + Billing* altında, ek ücret olmadan:

| Yetenek | Ne yapar? |
|---|---|
| **Cost analysis** | Gerçekleşen ve tahmin edilen (forecast) maliyeti **kaynak, servis, bölge, resource group, tag** bazında gruplama ve filtreleme; trend grafikleri |
| **Budgets** | Bir kapsam (subscription, RG…) için aylık/çeyreklik/yıllık limit; gerçekleşen veya tahmini harcama eşiği (%50, %80, %100…) aşılınca **uyarı** (e-posta, **action group** → otomasyon) |
| **Cost alerts** | Bütçe uyarıları, **kredi** uyarıları (ör. Azure kredisinin %90'ı bitti), **departman harcama kotası** uyarıları (EA), anomali uyarıları |
| **Öneriler** | Advisor'ın maliyet önerileri (az kullanılan VM, reservation fırsatı) (→ C1.14) |
| **Exports** | Maliyet verisini zamanlanmış olarak storage account'a dışa aktarma (Power BI, özel raporlar) |

> **Budget harcamayı durdurmaz**, yalnızca uyarır. Kaynakları otomatik kapatmak istiyorsan bütçe uyarısını bir **action group**'a (ör. Automation/Logic App/Function) bağlarsın.

#### 3.4 Resource tags ⭐ (`Bölüm 04`)

**Tag** = kaynağa, resource group'a veya subscription'a eklenen **anahtar-değer** meta verisi: `env=prod`, `costCenter=1001`, `owner=alice`, `project=ecommerce`.

| Kullanım | Örnek |
|---|---|
| **Maliyet yönetimi ve raporlama** ⭐ | Cost analysis'te "group by tag: costCenter" → departman bazında fatura |
| Kaynak yönetimi | `env=dev` olan tüm kaynakları bul (`az resource list --tag env=dev`) |
| Operasyon ve otomasyon | `autoShutdown=true` olan VM'leri gece kapat |
| Güvenlik | `dataClassification=confidential` |
| Uyumluluk/yönetişim | `owner` tag'i olmayan kaynağa izin verme (Policy) |

**Kurallar:**
- Tag'ler **varsayılan olarak miras alınmaz**: resource group'un `costCenter=1001` tag'i içerideki kaynağa **geçmez**. `Section04`'te tag'i unutulmuş iki kaynağın maliyeti raporda **"(no tag)"** satırına düşer.
- Mirası ve zorunluluğu **Azure Policy** sağlar: *Require a tag on resources* (**Deny** — tag'siz kaynak oluşturulamaz), *Inherit a tag from the resource group if missing* (**Modify** — eksik tag'i gruptan kopyalar; mevcut kaynaklar için **remediation**) (→ C1.12).
- *(bağlam)* Cost Management'ın **tag inheritance** ayarı, subscription/RG tag'lerini kaynakların kendisine değil yalnızca **maliyet kayıtlarına** uygular — raporlama için bir kolaylıktır; kaynak üzerindeki tag'ler yine değişmez. Sınavdaki "kaynak, RG'nin tag'lerini devralır mı?" sorusunun cevabı **Hayır**'dır.
- Tag'ler portal, CLI, PowerShell, ARM/Bicep ile eklenir; her kaynak türü tag desteklemez ve kaynak başına tag sayısı sınırlıdır *(bağlam: 50)*.

#### 3.5 Pratik — CLI ile bütçe, tag ve fiyat sorgusu

Bütçe ve tag işlemleri **ücretsizdir**; laboratuvarda yalnızca boş resource group'lar ve ücretsiz F1 App Service planı kullanılır.

```bash
RG=rg-az900-c111
az group create --name $RG --location westeurope --tags env=dev costCenter=1001 owner=alice

# 1) Budget: 20 units per month (start-date must be the first day of a month) — email thresholds are added from the budget screen / REST API
az consumption budget create --budget-name c111-monthly \
  --amount 20 --category cost --time-grain monthly \
  --start-date 2026-11-01 --end-date 2027-10-31
az consumption budget list -o table

# 2) Tag: does the group's tag pass down to the resource? (F1 = free plan)
az appservice plan create --resource-group $RG --name plan-c111 --sku F1 --is-linux
az resource list --resource-group $RG --query "[].{name:name, tags:tags}" -o json     # the plan has no tags!
PLAN_ID=$(az appservice plan show --resource-group $RG --name plan-c111 --query id -o tsv)
az tag update --resource-id $PLAN_ID --operation Merge --tags costCenter=1001 env=dev   # Merge: keeps the existing ones
az resource list --tag costCenter=1001 -o table                                       # search by tag

# 3) Price difference by region — public Retail Prices API (no authentication needed)
curl -s "https://prices.azure.com/api/retail/prices?\$filter=serviceName%20eq%20'Virtual%20Machines'%20and%20armSkuName%20eq%20'Standard_D2s_v5'%20and%20priceType%20eq%20'Consumption'" \
  | jq -r '.Items[] | select(.productName | test("Windows") | not) | select(.skuName | test("Spot|Low") | not) | "\(.armRegionName)\t\(.retailPrice) \(.currencyCode)/hour"' \
  | sort -t$'\t' -k2 -n | head -15

# 4) Cost recommendations and actual usage
az advisor recommendation list --category Cost -o table
az consumption usage list --start-date 2026-10-01 --end-date 2026-10-21 \
  --query "[].{resource:instanceName, amount:pretaxCost, currency:currency}" -o table

# 5) Cleanup (the budget is tied to the subscription, not the resource group — delete it separately)
az group delete --name $RG --yes --no-wait
az consumption budget delete --budget-name c111-monthly
```

> **Alıştırma — Pricing Calculator.** Küçük bir web uygulaması için (West Europe): App Service **B1** (1 instance, 730 saat) + Azure SQL Database (en küçük DTU/vCore katmanı) + Storage (Standard LRS, 50 GB Hot). (a) Aylık tahmini toplamı çıkar. (b) Aynı tahmini başka bir bölge için tekrarla — fark var mı? (c) App Service için 1 yıllık reservation/savings plan seçeneği tahmini nasıl değiştiriyor?
>
> <details><summary>Çözüm (yapı — fiyatlar zamana ve bölgeye göre değişir)</summary>
>
> (a) Toplamın büyük kısmını genellikle veritabanı ve App Service planı oluşturur; 50 GB Hot blob birkaç dolar civarındadır.
> (b) Evet — **coğrafya bir maliyet faktörüdür**; aynı SKU bölgeler arasında yüzde birkaçtan %20'ye kadar farklı olabilir. Bölge seçerken gecikme ve veri ikametgâhı da hesaba katılır (→ C1.4).
> (c) Taahhüt seçeneği aylık eşdeğeri düşürür; ancak **taahhüt** süre boyunca kullanılsın kullanılmasın ödenir — yalnızca sürekli çalışacak plan için mantıklıdır.
> </details>

---

### 4. Gerçek dünya senaryosu

Bir e-ticaret şirketinin bulut faturası üç ayda %60 artmış; FinOps ekibi şu adımları atıyor:

1. **Görünürlük:** Cost analysis'te maliyet **resource group** ve **tag** bazında gruplanıyor. Faturanın %18'i "(no tag)" satırında → Azure Policy ile `costCenter` ve `env` tag'leri **zorunlu** kılınıyor (Deny) ve mevcut kaynaklara gruptan **kopyalanıyor** (Modify + remediation).
2. **Bütçe:** Her ekibin aboneliğine aylık budget; %80 tahmini harcamada ekip lideri, %100'de finans e-postası; dev aboneliğinde %100 uyarısı bir **action group** üzerinden dev VM'lerini kapatan bir otomasyonu tetikliyor.
3. **Bakım:** Advisor; 40 az kullanılan VM, 120 sahipsiz disk ve 30 kullanılmayan public IP buluyor → küçültme ve silme.
4. **Tüketim modeli:** Üretimdeki 7/24 veritabanı ve web katmanı için 3 yıllık reservation; Windows sunucularında **Azure Hybrid Benefit**; gece çalışan raporlama işleri **spot VM**'lere.
5. **Ağ:** CDN ile görsellerin internete çıkışı (egress) azaltılıyor; gereksiz bölgeler arası replikasyon kapatılıyor.
6. **Yeni projeler:** Her yeni mimari önce **Pricing Calculator**'da tahmin ediliyor; on-prem'den taşınacak uygulamalar için **Azure Migrate business case** ile TCO karşılaştırması yapılıyor.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Araç seçimi, maliyet faktörü tanıma, tasarruf yolu seçimi ve tag kuralları — çoğunlukla kısa senaryo ve evet/hayır tablosu.

| Senaryodaki ifade | Cevap |
|---|---|
| "Henüz kurulmamış bir çözümün aylık maliyetini tahmin et" | **Pricing Calculator** |
| "On-prem datacenter'ın toplam maliyetini Azure ile karşılaştır" | **TCO Calculator** (güncel: Azure Migrate business case) |
| "Mevcut harcamayı görüntüle/analiz et, departmana göre grupla" | **Cost Management — cost analysis** (+ tag) |
| "Harcama limitin %80'ine ulaşınca e-posta gönder" | **Cost Management — budget** + uyarı |
| "Maliyeti departmana/projeye göre raporla" | **Resource tags** |
| "RG'nin tag'i içindeki kaynaklara otomatik uygulansın" | Varsayılan olarak **olmaz** → **Azure Policy** (Modify) |
| "Mevcut Windows Server lisanslarını kullanarak tasarruf" | **Azure Hybrid Benefit** |
| "7/24 sabit yükte en büyük indirim" | **Reservations** (veya savings plan) |
| "Gelen mi giden mi trafik ücretli?" | **Giden (egress)**; gelen genellikle ücretsiz |
| "Aynı servisin fiyatı neden farklı?" | **Bölge (coğrafya)**, SKU, tüketim modeli |

**Tuzaklar:**
- Pricing Calculator **tahmin** eder; Cost Management **gerçekleşen** maliyeti gösterir.
- Budget **uyarır, durdurmaz**.
- Tag'ler **miras alınmaz** — bu kuralı soran bir evet/hayır satırı çok sık çıkar ("Bir kaynak, resource group'unun tag'lerini otomatik devralır" → **Hayır**).
- Spot ile reservation: kesilebilirlik ↔ taahhüt.
- "VM'i durdurdum, ücret neden sürüyor?" → **deallocate** edilmemiş olabilir ve disk ücreti her durumda sürer (→ C1.5).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "Budget aşılınca Azure kaynakları kapatır" | Budget yalnızca uyarır; kapatma için action group + otomasyon gerekir |
| "Pricing Calculator ile geçen ayın faturasını incelerim" | Gerçekleşen maliyet → Cost Management cost analysis |
| "RG'ye tag verdim, içindeki kaynaklar da etiketlendi" | Tag mirası yok; Policy (Modify) ile kopyalanır |
| `az resource tag --tags owner=ali` ile tag eklemek | Bu komut kaynağın **tüm** tag'lerini değiştirir; eklemek için `az tag update --operation Merge` |
| "Ağ trafiği ücretsizdir" | Egress, bölgeler arası ve zone'lar arası trafik ücretlidir |
| VM'i silip diskleri/IP'leri unutmak | Kalan kaynaklar faturalanır; RG düzeyinde temizlik veya Advisor |
| Kısa süreli projeye 3 yıllık reservation | Taahhüt süre boyunca ödenir; değişken/kısa yükte pay-as-you-go |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Pay-as-you-go | Tam esneklik, taahhüt yok | Sabit yükte en pahalı seçenek |
| Reservation / savings plan | Sabit yükte büyük indirim | 1-3 yıl taahhüt; yanlış tahminde boşa ödeme |
| Spot | En düşük fiyat | Her an kesilebilir; yalnızca dayanıklı işler |
| Ucuz bölge seçmek | Daha düşük birim fiyat | Kullanıcıya gecikme, veri ikametgâhı, servis uygunluğu |
| Yüksek redundancy / çok bölge | Dayanıklılık, yüksek SLA (→ C1.2, C1.8) | Daha çok kaynak + replikasyon trafiği (egress) |
| Sıkı tag politikası (Deny) | Temiz maliyet raporu | Dağıtımlarda sürtünme; şablonların tag içermesi gerekir |
| Agresif otomatik kapatma | Dev/test maliyetinde büyük düşüş | Sabah açılış gecikmesi, yanlış kaynağı kapatma riski (tag ile hedefle) |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe cost management in Azure](https://learn.microsoft.com/en-us/training/modules/describe-cost-management-azure/)
- [Azure Pricing Calculator](https://azure.microsoft.com/pricing/calculator/) · [Azure Retail Prices REST API](https://learn.microsoft.com/en-us/rest/api/cost-management/retail-prices/azure-retail-prices)
- [Azure Migrate business case overview](https://learn.microsoft.com/en-us/azure/migrate/concepts-business-case-calculation) — on-prem ↔ Azure TCO karşılaştırması
- TCO Calculator durumu (ikincil kaynaklar): [MindMesh Academy — AZ-900 3.2.2](https://www.mindmeshacademy.com/certifications/azure/az-900-microsoft-azure-fundamentals/study-guide/3-2-2-estimating-cost-before-you-spend) · [Tutorials Dojo forum yanıtı](https://portal.tutorialsdojo.com/forums/reply/778971/)
- [What is Microsoft Cost Management?](https://learn.microsoft.com/en-us/azure/cost-management-billing/costs/overview-cost-management) · [Create and manage budgets](https://learn.microsoft.com/en-us/azure/cost-management-billing/costs/tutorial-acm-create-budgets)
- [Use tags to organize your Azure resources](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/tag-resources) · [Group and allocate costs using tag inheritance](https://learn.microsoft.com/en-us/azure/cost-management-billing/costs/enable-tag-inheritance)
- [Azure Hybrid Benefit](https://azure.microsoft.com/pricing/hybrid-benefit/) · [Azure reservations](https://learn.microsoft.com/en-us/azure/cost-management-billing/reservations/save-compute-costs-reservations) · [Azure savings plan for compute](https://learn.microsoft.com/en-us/azure/cost-management-billing/savings-plan/savings-plan-compute-overview)
- [Bandwidth pricing](https://azure.microsoft.com/pricing/details/bandwidth/) (egress)


### 📝 Bölüm 3.1 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir ekip, **henüz kurmadığı** bir web uygulamasının (App Service + Azure SQL Database + Storage) aylık Azure maliyetini tahmin etmek istiyor. Hangi aracı kullanmalıdır?

- A) Microsoft Cost Management — cost analysis
- B) Azure Pricing Calculator
- C) Azure Advisor
- D) Azure Service Health

<details><summary>Cevap</summary>

**B.** Pricing Calculator, kurulmamış bir mimarinin tahmini maliyetini hesaplar. Cost analysis gerçekleşen maliyeti gösterir; Advisor mevcut kaynaklar için öneri verir; Service Health olayları bildirir.
</details>

#### Soru 2: Tek seçim
Bir CFO, şirketin kendi datacenter'ını işletmenin 3 yıllık toplam maliyetini (donanım, enerji, alan, işçilik dahil) aynı iş yükünün Azure'daki maliyetiyle **karşılaştırmak** istiyor. Hangi yaklaşım bu ihtiyaca yöneliktir?

- A) Pricing Calculator
- B) TCO karşılaştırması (TCO Calculator / Azure Migrate business case)
- C) Azure Policy
- D) Resource tags

<details><summary>Cevap</summary>

**B.** On-prem ↔ Azure toplam sahip olma maliyeti karşılaştırması TCO'nun işidir. Klasik TCO Calculator'ın yerini bazı kaynaklara göre Azure Migrate business case almıştır; sınav mantığı aynıdır: Pricing Calculator yalnızca Azure tarafını tahmin eder.
</details>

#### Soru 3: Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** bir Azure kaynağının maliyetini **doğrudan** etkiler?

- A) Kaynağın dağıtıldığı bölge
- B) Kaynağın adı
- C) Kaynağın SKU'su / boyutu
- D) Kaynağı oluşturan kullanıcının rolü

<details><summary>Cevap</summary>

**A ve C.** Coğrafya (bölge) ve kaynak türü/ayarları (SKU, boyut, tier) maliyet faktörleridir. Ad ve oluşturanın rolü fiyatı değiştirmez.
</details>

#### Soru 4: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure'a gelen (inbound) veri trafiği genellikle ücretsizdir. | ☐ | ☐ |
| b) Bir Azure bölgesinden internete çıkan (egress) veri trafiği ücretlendirilebilir. | ☐ | ☐ |
| c) Aynı VM boyutunun fiyatı tüm Azure bölgelerinde aynıdır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Ingress genellikle ücretsiz, egress ücretlidir; fiyatlar bölgeye göre değişir.
</details>

#### Soru 5: Senaryo
Bir şirket, aylık Azure harcaması belirlediği limitin **%80'ine ulaştığında** finans ekibine otomatik e-posta gönderilmesini istiyor. Ne yapılandırmalıdır?

- A) Azure Policy ile Deny politikası
- B) Cost Management'ta bir budget ve uyarı eşiği
- C) Resource lock
- D) Pricing Calculator tahmini

<details><summary>Cevap</summary>

**B.** Budget + eşik uyarısı (e-posta veya action group). Policy ve lock maliyet uyarısı göndermez; Pricing Calculator tahmin aracıdır.
</details>

#### Soru 6: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Cost Management'ta bir budget aşıldığında Azure kaynakları otomatik olarak durdurulur. | ☐ | ☐ |
| b) Budget uyarısı bir action group'a bağlanarak otomasyon tetiklenebilir. | ☐ | ☐ |
| c) Cost analysis ile maliyetler resource group veya tag'e göre gruplanabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Budget yalnızca uyarır; kapatma gibi eylemler action group üzerinden kurulan otomasyonla yapılır.
</details>

#### Soru 7: Tek seçim
Bir şirket, maliyetleri **departmanlara göre** raporlamak istiyor. Kaynaklara hangi bilgi eklenmelidir?

- A) Resource lock
- B) `costCenter` gibi bir **tag**
- C) Ek bir subscription rolü
- D) Daha yüksek bir SLA

<details><summary>Cevap</summary>

**B.** Tag'ler (anahtar-değer meta veri) cost analysis'te gruplama ve filtreleme için kullanılır. (Ayrı subscription'lar da bir yöntemdir, ama şıklardaki "rol" yöntem değildir.)
</details>

#### Soru 8: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Bir resource group'a eklenen tag'ler, gruptaki kaynaklara varsayılan olarak otomatik uygulanır. | ☐ | ☐ |
| b) Azure Policy, eksik tag'lerin resource group'tan kaynaklara kopyalanması için kullanılabilir. | ☐ | ☐ |
| c) Tag'ler anahtar-değer çiftlerinden oluşur. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Tag'ler miras alınmaz; Policy (Modify etkisi, ör. *Inherit a tag from the resource group if missing*) bunu sağlar.
</details>

#### Soru 9: Tek seçim
Bir şirket, Software Assurance kapsamındaki mevcut **Windows Server ve SQL Server lisanslarını** Azure'daki VM'lerde kullanarak maliyetini düşürmek istiyor. Hangi seçenek bunu sağlar?

- A) Spot VM
- B) Azure Hybrid Benefit
- C) Dev/test fiyatlandırması
- D) Azure Marketplace

<details><summary>Cevap</summary>

**B.** Azure Hybrid Benefit, mevcut lisansların Azure'da kullanılmasıyla lisans maliyetini düşürür.
</details>

#### Soru 10: Senaryo
Bir uygulamanın veritabanı sunucusu önümüzdeki üç yıl 7/24 aynı boyutta çalışacak. **En büyük indirimi** hangi seçenek sağlar?

- A) Pay-as-you-go
- B) 3 yıllık reservation
- C) Spot VM
- D) Ücretsiz hesap

<details><summary>Cevap</summary>

**B.** Sabit, uzun süreli yükte taahhütlü indirim en ucuzudur. Spot kesilebilir olduğu için 7/24 kritik veritabanına uygun değildir.
</details>

#### Soru 11: Tek seçim
Bir geliştirici bir VM'i sildi, ancak ertesi ay faturada hâlâ o VM'e ait **disk ve public IP** ücretleri görülüyor. Bu hangi maliyet faktörüne örnektir?

- A) Coğrafya
- B) Bakım (unutulan/kullanılmayan kaynaklar)
- C) Azure Marketplace
- D) Subscription türü

<details><summary>Cevap</summary>

**B.** VM'i silmek bağlı kaynakları her zaman silmez; kullanılmayan kaynaklar faturalanmaya devam eder. Çözüm: resource group düzeyinde temizlik, Advisor önerileri.
</details>

#### Soru 12: Eşleştirme
Her ihtiyacı araçla eşleştirin: **Pricing Calculator · Cost Management (cost analysis) · Cost Management (budgets) · Azure Advisor**

1. Geçen ayın gerçekleşen maliyetini servis bazında görmek
2. Az kullanılan VM'leri küçültme önerisi almak
3. Yeni bir projenin tahmini aylık maliyetini çıkarmak
4. Harcama eşiği aşılınca uyarı almak

<details><summary>Cevap</summary>

**1 → Cost analysis · 2 → Advisor · 3 → Pricing Calculator · 4 → Budgets.**
</details>

#### Soru 13: Tek seçim
Bir şirket Azure Marketplace'ten üçüncü taraf bir güvenlik duvarı imajıyla VM dağıtıyor. Fatura hakkında hangisi doğrudur?

- A) Yalnızca Microsoft'un VM ücreti ödenir
- B) VM ücretine ek olarak satıcının yazılım ücreti de ödenebilir
- C) Marketplace ürünleri her zaman ücretsizdir
- D) Marketplace ürünleri yalnızca Enterprise Agreement ile alınabilir

<details><summary>Cevap</summary>

**B.** Marketplace ürünlerinde Azure altyapı ücreti + yayıncının (satıcının) ürün ücreti olabilir.
</details>

#### Soru 14: Senaryo
Bir şirketin geliştirme ortamındaki VM'ler yalnızca hafta içi mesai saatlerinde kullanılıyor. Maliyeti düşürmek için **en uygun** adım hangisidir?

- A) VM'leri 3 yıllık reservation'a almak
- B) Mesai dışında VM'leri otomatik kapatmak (deallocate) ve dev/test fiyatlandırmasını kullanmak
- C) VM'leri başka bir bölgeye taşımak
- D) VM'lere daha çok tag eklemek

<details><summary>Cevap</summary>

**B.** Haftada ~60 saat kullanılan VM'i kalan ~108 saat kapatmak compute ücretini yarıdan fazla düşürür; dev/test fiyatlandırması üretim dışı ortamlarda ek indirim sağlar. Reservation, çoğu zaman kapalı duran bir VM için boşa ödeme olur.
</details>

#### Soru 15: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Pricing Calculator'ın sonucu bir tahmindir; gerçek fatura kullanıma göre farklı olabilir. | ☐ | ☐ |
| b) Pricing Calculator'ı kullanmak için bir Azure aboneliği gerekir. | ☐ | ☐ |
| c) Pricing Calculator'da reservation ve Azure Hybrid Benefit gibi seçeneklerin etkisi görülebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Calculator herkese açıktır; satın alma seçeneklerini karşılaştırmaya izin verir; sonuç tahmindir.
</details>

#### Soru 16: Tek seçim
Bir kuruluş, kimsenin `costCenter` tag'i olmadan kaynak oluşturamamasını istiyor. Ne kullanmalıdır?

- A) Cost Management budget
- B) Azure Policy (*Require a tag* — Deny etkisi)
- C) Resource lock (ReadOnly)
- D) Azure Advisor

<details><summary>Cevap</summary>

**B.** Tag'i zorunlu kılmak Azure Policy'nin işidir (→ C1.12). Budget uyarır; lock silme/değiştirmeyi engeller; Advisor öneri verir.
</details>

#### Soru 17: Senaryo (hesap)
Kendi datacenter'ında 3 yıllık maliyet: 1. yıl 380.000 $ (donanım peşin dahil), 2. ve 3. yıllar 100.000'er $. Azure'da aynı iş yükü her yıl 90.000 $. Aşağıdakilerden hangisi doğrudur?

- A) On-prem 3 yılda daha ucuzdur
- B) Azure 3 yılda yaklaşık 310.000 $ daha ucuzdur ve harcama yıllara eşit yayılır
- C) Azure'da 1. yıl donanım için peşin ödeme gerekir
- D) İki seçenek de aynı maliyettedir

<details><summary>Cevap</summary>

**B.** On-prem: 380.000 + 100.000 + 100.000 = 580.000 $; Azure: 3 × 90.000 = 270.000 $; fark 310.000 $. On-prem'de harcama 1. yılda yığılır (CapEx), Azure'da dönemseldir (OpEx). Kendi rakamlarınla yapıyı görmek için: `dotnet run -- C1.11 tco 20`.
</details>

#### Soru 18: Tek seçim
Bir şirket kesintiye dayanıklı, gece çalışan toplu veri işleme işlerinin maliyetini **en aza** indirmek istiyor; işler kesilirse yeniden başlatılabiliyor. Hangi seçenek **en uygundur**?

- A) Spot VM
- B) 3 yıllık reservation
- C) Azure Hybrid Benefit
- D) Premium SSD diskler

<details><summary>Cevap</summary>

**A.** Spot VM, Azure'un boşta kapasitesini büyük indirimle sunar; karşılığında VM geri alınabilir — kesintiye dayanıklı toplu işler için idealdir.
</details>

---

**Puanlama:** 16-18 → hazırsın · 12-15 → README 3.2 (araç ayrımı) ve 3.4 (tag kuralları) bölümlerini tekrar et · < 12 → konuyu baştan çalış, `dotnet run -- C1.11 all` çıktısını incele.


---

## 3.2 Yönetişim ve Uyumluluk: Policy, Locks, Tags, Purview

> **Sınav Alanı:** Describe Azure management and governance (%30-35) · **Konu Kodu:** C3.2

### 1. Ne?

**Yönetişim (governance)**, bir kuruluşun kurallarını — güvenlik, maliyet, uyumluluk, adlandırma, veri — **ölçekte** uygulaması ve uygulandığını **denetleyebilmesidir**. Yüzlerce subscription ve binlerce kaynakta bu kuralları insan dikkatiyle korumak mümkün değildir; Azure bunun için birbirini tamamlayan araçlar sunar:

| Araç | Soru | Neyi kontrol eder? |
|---|---|---|
| **Management group / subscription / RG** (→ C1.4) | Kurallar **nereye** uygulanır? | Kapsam ve miras |
| **Azure RBAC** (→ C1.9) | **Kim** ne yapabilir? | Kullanıcının/kimliğin yetkisi |
| **Azure Policy** ⭐ | Kaynak **nasıl** olabilir? | Kaynağın özellikleri (bölge, SKU, tag, ayar) |
| **Resource lock** ⭐ | Kaynak **silinebilir/değiştirilebilir** mi? | Kazara silme/değişiklik |
| **Tag** (→ C1.11) | Kaynak **kime/neye** ait? | Meta veri, maliyet raporlama |
| **Microsoft Purview** | **Veri** nerede, ne içeriyor, kim kullanıyor? | Veri yönetişimi, risk ve uyumluluk |

Bu konu resmî hedefteki üç aracı — Policy, lock, Purview — derinlemesine işler. Konunun kodu yoktur; pratik, README'deki `az` CLI laboratuvarıdır (3.5).

---

### 2. Neden?

- **Sınav:** *Describe Azure management and governance* alanı sınavın **%30-35**'i; "Policy mi, RBAC mı, lock mı?" ayrımı bu alanın en klasik sorusudur.
- **Gerçek işte:** "Kimse ABD region'ında kaynak açmasın", "her kaynağın `costCenter` tag'i olsun", "üretim veritabanı yanlışlıkla silinmesin" — bu kuralların hepsi bir e-posta duyurusuyla değil, **platformun zorladığı** kurallarla korunur.
- **Uyumluluk:** Regülasyonlar (KVKK, GDPR, sektör standartları) "kuralı koydum" değil "kuralın uygulandığını **kanıtlayabiliyorum**" ister; Policy'nin compliance panosu ve Purview'un veri haritası bu kanıtı üretir.

---

### 3. Nasıl?

#### 3.1 Genel çerçeve ve uyumluluk kaynakları

- Yönetişim araçları **kapsam hiyerarşisi** üzerinde çalışır: management group'a atanan bir politika veya lock, altındaki tüm subscription, resource group ve kaynaklara **miras** kalır (→ C1.4).
- *(bağlam)* Microsoft'un kendi uyumluluğunu gösteren kaynaklar: **Microsoft Service Trust Portal** (bağımsız denetim raporları, ISO/SOC belgeleri), **Microsoft Trust Center** (güvenlik, gizlilik ve uyumluluk yaklaşımı), **gizlilik bildirimi** ve **ürün koşulları**. Bunlar *Microsoft'un* uyumluluğunu belgeler; *senin* kaynaklarının uyumluluğu Policy ve Defender for Cloud (→ C1.10) ile izlenir.

#### 3.2 Azure Policy ⭐

**Azure Policy**, kaynakların **özelliklerinin** kurumsal kurallara uygun olup olmadığını **değerlendirir** ve gerekirse **zorlar**. Değerlendirme, kaynak oluşturulurken/güncellenirken ve mevcut kaynaklar için periyodik olarak yapılır.

**Yapı taşları:**

| Kavram | Ne? | Örnek |
|---|---|---|
| **Policy definition** | Kural (JSON): hangi koşulda hangi etki | *Allowed locations*, *Require a tag and its value on resources* |
| **Initiative** (policy set) | Birlikte yönetilen tanımlar kümesi | Bir güvenlik benchmark'ı (ör. Microsoft cloud security benchmark), "etiketleme standardı" |
| **Assignment** | Tanımın/initiative'in bir **kapsama** atanması (+ parametreler, istisnalar) | *Allowed locations* → `rg-prod`, `listOfAllowedLocations = [westeurope]` |
| **Exemption** *(bağlam)* | Belirli bir kapsamı değerlendirmenin dışında tutma | Test aboneliği |
| **Remediation task** | Mevcut uyumsuz kaynakları düzeltme (Modify / DeployIfNotExists) | Eksik tag'i gruptan kopyalamak |

**Etkiler (effect)** — her tanımın tek bir etkisi vardır:

| Etki | Ne yapar? | Sınavdaki ipucu |
|---|---|---|
| **Audit** | Kaynağa izin verir, **uyumsuz** olarak raporlar | "uyar / raporla, engelleme" |
| **Deny** | Kurala aykırı oluşturma/güncellemeyi **reddeder** | "izin verme / engelle" |
| **Append** / **Modify** | İstek sırasında özellik/tag **ekler veya düzeltir** | "tag'i otomatik ekle" |
| **AuditIfNotExists** | İlişkili bir kaynak/ayar (ör. tanılama ayarı) yoksa raporlar | |
| **DeployIfNotExists** | İlişkili kaynak/ayar yoksa **dağıtır** | "eksikse otomatik kur" |
| **Disabled** | Tanımı devre dışı bırakır | |
| *(bağlam)* **DenyAction**, **Manual** | Belirli bir eylemi (ör. silme) engeller · uyumluluğu elle beyan | |

**Bir politikanın yaşam döngüsü:**

1. Built-in tanımı bul (ör. *Allowed locations*).
2. Kapsama **ata** (management group / subscription / resource group), parametreyi ver.
3. Aykırı **yeni** kaynak isteği **Deny** ile reddedilir (`RequestDisallowedByPolicy`).
4. **Mevcut** uyumsuz kaynaklar silinmez; **compliance** panosunda "Non-compliant" olarak listelenir.
5. Gerekirse **remediation task** ile düzeltilir (Modify/DeployIfNotExists etkili politikalar için).

**Policy ↔ RBAC:** RBAC **kullanıcının** ne yapabileceğini belirler ("Ali bu RG'de kaynak oluşturabilir"); Policy **kaynağın** nasıl olabileceğini belirler ("bu RG'deki kaynaklar yalnızca West Europe'ta olabilir"). İkisi birlikte çalışır: Owner rolündeki biri bile Deny politikasına aykırı kaynağı oluşturamaz.

#### 3.3 Resource lock'lar ⭐

**Resource lock**, kaynakların **kazara silinmesini veya değiştirilmesini** önler. İki tür vardır:

| Tür | Portaldaki adı | Okuma | Değiştirme | Silme |
|---|---|---|---|---|
| **CanNotDelete** | **Delete** | ✅ | ✅ | ❌ |
| **ReadOnly** | **Read-only** | ✅ | ❌ | ❌ |

- Subscription, resource group veya tek kaynak düzeyinde uygulanır; alt kapsamlara **miras** kalır (RG'deki lock, içindeki tüm kaynakları korur).
- **Rol yetkisinden bağımsızdır:** Owner bile kilitli kaynağı silemez; önce lock'ı **kaldırması** gerekir (lock'ı kaldırmak için de `Microsoft.Authorization/locks/*` yetkisi gerekir — Owner ve User Access Administrator'da vardır).
- Lock **yönetim düzlemini** (control plane — Azure Resource Manager istekleri) korur, **veriyi** değil: CanNotDelete'li bir storage account silinemez ama içindeki blob'lar silinebilir (veri koruması için blob soft delete/versioning gibi özellikler gerekir).
- **ReadOnly yan etkileri** — beklenenden fazlasını engelleyebilir: ARM'daki POST işlemleri de engellendiği için storage account'ta **erişim anahtarlarını listeleme** (`listKeys`) başarısız olur; o kapsamda **RBAC rol ataması** yapılamaz; bazı portal ekranları çalışmaz. Bu yüzden ReadOnly dikkatle, çoğu zaman CanNotDelete tercih edilir.

#### 3.4 Microsoft Purview

**Microsoft Purview**, kuruluşun **verisini** yönetmek, korumak ve uyumluluğunu sağlamak için bir **veri yönetişimi, risk ve uyumluluk** çözümleri ailesidir. Kimlik ya da maliyet aracı değildir; konusu **veridir**.

| Alan | Ne yapar? | Örnek |
|---|---|---|
| **Birleşik veri yönetişimi** (Data Map + Unified Catalog) | On-prem, multicloud ve SaaS veri kaynaklarını (Azure SQL, Data Lake, Amazon S3, Power BI…) **tarar**, meta verisini bir **veri haritasına** toplar; aranabilir **katalog**, veri **soy ağacı** (lineage), veri kalitesi ve erişim yönetimi | "Müşteri verisi hangi veritabanlarında, kim kullanıyor, nereden besleniyor?" |
| **Risk ve uyumluluk** (çoğunlukla Microsoft 365 verisi) | Hassas veriyi otomatik **keşif ve sınıflandırma** (kredi kartı, kimlik numarası), **bilgi koruma** (sensitivity label), **veri kaybı önleme** (DLP), kayıt/yaşam döngüsü yönetimi, denetim | "Kredi kartı numarası içeren e-posta dışarı gönderilmesin" |

- Purview'un Data Map ve katalog rolleri **meta veriye** erişim verir; alttaki verinin kendisine erişim vermez.
- Sınav düzeyinde akılda kalması gereken: **"tüm verinin haritası, sınıflandırma, soy ağacı, veri yönetişimi" → Purview**.

#### 3.5 Pratik — Policy ve lock laboratuvarı (`az` CLI)

Maliyet: politika ve lock'lar **ücretsizdir**; laboratuvardaki tek ücretli kaynak boş bir storage account'tur (kuruşlar düzeyinde). Bütçe alarmın kurulu olsun (→ C1.1). Cloud Shell'de veya yerel `az` ile:

```bash
RG=rg-az900-c112
ST=stc112$RANDOM                      # storage account name: lowercase + digits, globally unique
az group create --name $RG --location westeurope

# 1) Find the built-in "Allowed locations" definition
az policy definition list \
  --query "[?displayName=='Allowed locations'].{name:name, displayName:displayName}" -o table
# name: e56962a6-4747-49cd-b67b-bf8b01975c4c

# 2) Assign at RG scope: West Europe only
az policy assignment create \
  --name allowed-locations-c112 \
  --display-name "C1.12 - westeurope only" \
  --policy e56962a6-4747-49cd-b67b-bf8b01975c4c \
  --scope $(az group show --name $RG --query id -o tsv) \
  --params '{ "listOfAllowedLocations": { "value": [ "westeurope" ] } }'

# 3) Try to create a resource in a disallowed region → should be rejected with RequestDisallowedByPolicy
#    (the assignment can take a few minutes to take effect; if it is not rejected right away, wait a bit and retry)
az storage account create --name $ST --resource-group $RG --location eastus --sku Standard_LRS

# 4) The same resource in an allowed region → succeeds
az storage account create --name $ST --resource-group $RG --location westeurope --sku Standard_LRS

# 5) CanNotDelete lock: the group cannot be deleted
az lock create --name lock-nodelete --lock-type CanNotDelete --resource-group $RG
az lock list --resource-group $RG -o table
az group delete --name $RG --yes                     # → ScopeLocked error
az storage account update --name $ST --resource-group $RG --tags env=lab   # changes are allowed ✅

# 6) ReadOnly lock (resource level): modifying and listing keys are blocked too
az lock create --name lock-readonly --lock-type ReadOnly --resource-group $RG \
  --resource-name $ST --resource-type Microsoft.Storage/storageAccounts
az storage account update --name $ST --resource-group $RG --tags env=prod  # → ScopeLocked
az storage account keys list --account-name $ST --resource-group $RG       # → ScopeLocked (POST is blocked)

# 7) Cleanup: locks first, then the assignment, then the group
az lock delete --name lock-readonly --resource-group $RG \
  --resource-name $ST --resource-type Microsoft.Storage/storageAccounts
az lock delete --name lock-nodelete --resource-group $RG
az policy assignment delete --name allowed-locations-c112 \
  --scope $(az group show --name $RG --query id -o tsv)
az group delete --name $RG --yes --no-wait
```

> **Alıştırma.** Şu kuralları hangi araçla (Policy / RBAC / lock / tag / Purview) ve hangi kapsamda uygularsın?
> 1. Tüm şirkette yalnızca AB bölgelerinde kaynak açılabilsin.
> 2. Stajyerler üretim aboneliğindeki kaynakları yalnızca görebilsin.
> 3. Üretim veritabanı sunucusu yanlışlıkla silinmesin, ama ayarları değiştirilebilsin.
> 4. Her kaynağın `costCenter` tag'i olsun; yoksa resource group'unkinden kopyalansın.
> 5. Şirketin hangi veri kaynaklarında TC kimlik numarası bulunduğu görülebilsin.
>
> <details><summary>Çözüm</summary>
>
> 1. **Azure Policy** (*Allowed locations*, Deny) → **root/üst management group**'a atanır, tüm subscription'lara miras kalır.
> 2. **RBAC** → stajyer grubuna **Reader** rolü, üretim **subscription** kapsamında.
> 3. **Resource lock** → **CanNotDelete**, veritabanı sunucusu kaynağı (veya RG) düzeyinde. ReadOnly değil — ayar değişikliğini de engellerdi.
> 4. **Tag + Azure Policy** → *Inherit a tag from the resource group if missing* (**Modify** etkisi); mevcut kaynaklar için **remediation task**. Tag'ler kendiliğinden miras alınmaz (→ C1.11).
> 5. **Microsoft Purview** → veri kaynaklarını Data Map ile tarama ve otomatik **sınıflandırma**.
> </details>

---

### 4. Gerçek dünya senaryosu

Bir sağlık şirketi, KVKK ve sektör denetimi öncesinde Azure ortamını düzene sokuyor:

1. **Kapsam:** Kök management group altında `Platform`, `Üretim`, `Geliştirme` management group'ları; her biri kendi subscription'larını içeriyor (→ C1.4).
2. **Policy:** Kök management group'a bir **initiative** atanıyor: *Allowed locations* (yalnızca AB bölgeleri, **Deny**), *Require a tag* (`costCenter`, `dataClass`, **Deny**), storage account'larda public erişimin kapalı olması (**Audit** ile başlanıyor, etki görüldükten sonra **Deny**'a çevriliyor), tanılama ayarlarının Log Analytics'e gitmesi (**DeployIfNotExists**). Compliance panosu ilk gün %62 uyum gösteriyor; remediation task'larla ve ekiplere açılan işlerle üç haftada %97'ye çıkıyor.
3. **Lock:** Üretim veritabanlarının ve ağ omurgasının bulunduğu resource group'lara **CanNotDelete**. Bir gece otomasyon hatasıyla çalışan `az group delete` komutu lock'a çarpıp duruyor.
4. **Purview:** Data Map; Azure SQL, Data Lake ve on-prem SQL Server kaynaklarını tarıyor; hasta kimlik bilgisi içeren tablolar otomatik sınıflandırılıyor. Denetçiye "hassas veri nerede ve kim erişiyor?" sorusunun cevabı katalogdan veriliyor.
5. **Kanıt:** Denetimde Microsoft'un kendi sertifikaları (ISO 27001, SOC raporları) Service Trust Portal'dan, şirketin kaynak uyumluluğu Policy compliance raporundan sunuluyor.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Çoğunlukla "bu gereksinimi hangi araç karşılar?" senaryoları ve evet/hayır tabloları; özellikle **Policy ↔ RBAC ↔ lock** ayrımı ve lock türleri.

| Senaryodaki ifade | Cevap |
|---|---|
| "Kaynakların **yalnızca belirli bölgelerde** oluşturulmasını sağla" | **Azure Policy** (Allowed locations) |
| "Belirli VM boyutlarına izin ver", "her kaynağın tag'i olsun" | **Azure Policy** |
| "Uyumsuz kaynakları **raporla**, engelleme" | Policy, **Audit** etkisi |
| "Birden çok politikayı **tek birim** olarak ata" | **Initiative** |
| "Politikayı **tüm subscription'lara** uygula" | Policy'yi **management group**'a ata |
| "Kullanıcının kaynak **oluşturma yetkisi**" | **RBAC** |
| "Kaynağın **kazara silinmesini** önle" | **Resource lock — CanNotDelete (Delete)** |
| "Kaynağın **değiştirilmesini ve silinmesini** önle" | **Resource lock — ReadOnly** |
| "Owner silmeye çalışıyor ama lock var" | Önce lock **kaldırılmalı** — lock Owner'ı da durdurur |
| "Verinin **haritası**, **sınıflandırma**, **soy ağacı**, veri yönetişimi" | **Microsoft Purview** |
| "Microsoft'un **denetim raporlarına** (ISO, SOC) eriş" | **Service Trust Portal** |

**Tuzaklar:**
- Policy **kaynağı** kısıtlar, RBAC **kullanıcıyı**: "Kullanıcılar yalnızca West Europe'ta kaynak açabilsin" → Policy (bölge bir kaynak özelliğidir), RBAC değil.
- Deny politikası **mevcut** uyumsuz kaynakları **silmez**; yalnızca raporlar.
- Lock'lar **miras alınır**; RG'deki lock içindeki her kaynağı korur.
- "ReadOnly lock kaynağı yalnızca silmeye karşı korur" → **Hayır**, değiştirmeyi de engeller.
- "Purview, kullanıcıların kimliklerini yönetir" → **Hayır** (o Entra ID); "Purview maliyetleri raporlar" → **Hayır** (o Cost Management).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Policy ile RBAC'ı karıştırmak | RBAC = **kim** ne yapabilir · Policy = kaynak **nasıl** olabilir |
| "Owner her şeyi yapabilir" | Deny politikasına ve lock'a Owner da takılır |
| Doğrudan **Deny** ile başlamak | Önce **Audit** ile etkisini gör, sonra Deny'a geç; aksi halde ekiplerin dağıtımları bir anda kırılır |
| "Politika atandı, eski kaynaklar düzeldi" | Mevcut kaynaklar yalnızca **raporlanır**; düzeltmek için remediation task veya elle müdahale |
| Her yere **ReadOnly** lock koymak | `listKeys`, rol ataması ve bazı yönetim işlemlerini de bozar; çoğu senaryoda **CanNotDelete** yeterli |
| "Lock veriyi korur" | Lock yönetim düzlemini korur; blob/dosya silmeye karşı soft delete, versioning, yedek gerekir |
| Purview'u Policy'nin alternatifi sanmak | Policy kaynak **yapılandırmasını**, Purview **veriyi** yönetir |
| Politikayı her subscription'a tek tek atamak | Management group'a bir kez ata, miras kalsın |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| **Deny** etkisi | Kural ihlali hiç oluşmaz | Yanlış kapsam/parametre meşru dağıtımları kırar; istisna yönetimi gerekir |
| **Audit** etkisi | Kimseyi durdurmadan görünürlük | İhlaller oluşmaya devam eder; birinin raporu takip etmesi gerekir |
| **Modify / DeployIfNotExists** | Uyumsuzluğu otomatik düzeltir | Managed identity ve yetki gerektirir; "sihirli" değişiklikler ekipleri şaşırtabilir |
| Politikaları üst management group'a atamak | Tek noktadan, tüm ortama tutarlı kural | Etki alanı büyük; hata da tüm ortama yayılır — önce test kapsamında dene |
| **CanNotDelete** lock | Kazara silmeye karşı ucuz, etkili koruma | Meşru silme/yeniden dağıtım için önce lock kaldırılmalı (otomasyonda adım) |
| **ReadOnly** lock | Yapılandırma kaymasını tamamen durdurur | Anahtar listeleme, rol ataması, bazı portal işlemleri bozulur |
| Purview ile veri kataloğu | Hassas verinin görünürlüğü, uyumluluk kanıtı | Tarama ve sınıflandırma kurulumu, sahiplik ve süreç gerektirir; ayrı lisans/ücret |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe features and tools in Azure for governance and compliance](https://learn.microsoft.com/en-us/training/modules/describe-features-tools-azure-for-governance-compliance/)
- [What is Azure Policy?](https://learn.microsoft.com/en-us/azure/governance/policy/overview) · [Azure Policy definitions effect basics](https://learn.microsoft.com/azure/governance/policy/concepts/effects)
- [Lock your Azure resources to protect your infrastructure](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/lock-resources) · [Apply a lock to a storage account](https://learn.microsoft.com/en-us/azure/storage/common/lock-account-resource)
- [Microsoft Purview data governance overview](https://learn.microsoft.com/purview/data-governance-overview) · [Unified Catalog](https://learn.microsoft.com/purview/unified-catalog)
- [Service Trust Portal](https://servicetrust.microsoft.com/) · [`az policy assignment`](https://learn.microsoft.com/en-us/cli/azure/policy/assignment) · [`az lock`](https://learn.microsoft.com/en-us/cli/azure/lock)


### 📝 Bölüm 3.2 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir şirket, çalışanlarının yalnızca **West Europe** ve **North Europe** bölgelerinde kaynak oluşturabilmesini istiyor. Hangi Azure özelliğini kullanmalıdır?

- A) Azure RBAC
- B) Resource lock
- C) Azure Policy
- D) Microsoft Purview

<details><summary>Cevap</summary>

**C.** Bölge bir **kaynak özelliğidir**; kaynağın özelliklerini kısıtlayan araç Azure Policy'dir (*Allowed locations*). RBAC kullanıcının hangi işlemleri yapabileceğini belirler, bölgeyi kısıtlamaz.
</details>

#### Soru 2: Tek seçim
Bir yönetici, üretim resource group'undaki kaynakların **kazara silinmesini** önlemek ama ekiplerin ayarları değiştirmesine izin vermek istiyor. Ne yapmalıdır?

- A) Resource group'a ReadOnly lock uygulamak
- B) Resource group'a CanNotDelete (Delete) lock uygulamak
- C) Ekiplere Reader rolü vermek
- D) Bir Audit politikası atamak

<details><summary>Cevap</summary>

**B.** CanNotDelete okumaya ve değiştirmeye izin verir, silmeyi engeller. ReadOnly (A) değişikliği de engeller; Reader (C) ekiplerin ayar değiştirmesini de engeller; Audit (D) yalnızca raporlar.
</details>

#### Soru 3: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Owner rolüne sahip bir kullanıcı, CanNotDelete lock'ı olan bir kaynağı lock'ı kaldırmadan silebilir. | ☐ | ☐ |
| b) Bir resource group'a uygulanan lock, içindeki kaynaklara miras kalır. | ☐ | ☐ |
| c) ReadOnly lock, kaynağın okunmasına izin verir ama değiştirilmesini ve silinmesini engeller. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Lock'lar rol yetkisinden bağımsızdır; Owner da önce lock'ı kaldırmak zorundadır. Lock'lar alt kapsamlara miras kalır.
</details>

#### Soru 4: Tek seçim
Azure Policy ile Azure RBAC arasındaki fark en iyi hangisiyle açıklanır?

- A) Policy kullanıcıların izinlerini, RBAC kaynakların özelliklerini yönetir
- B) Policy kaynakların özelliklerinin kurallara uymasını, RBAC kullanıcıların hangi işlemleri yapabileceğini yönetir
- C) İkisi de aynı işi yapar; Policy yalnızca daha yenidir
- D) Policy yalnızca maliyeti, RBAC yalnızca güvenliği yönetir

<details><summary>Cevap</summary>

**B.** RBAC = **kim** ne yapabilir; Policy = kaynak **nasıl** olabilir. Owner bile Deny politikasına aykırı kaynak oluşturamaz.
</details>

#### Soru 5: Tek seçim
Bir kuruluş, kendi güvenlik standardını oluşturan **20 politika tanımını** tek bir birim olarak birçok subscription'a atamak ve uyumluluğu tek bir yerden izlemek istiyor. Hangisini kullanmalıdır?

- A) Resource lock
- B) Policy initiative
- C) Management group
- D) Tag

<details><summary>Cevap</summary>

**B.** Initiative (policy set), birlikte yönetilen politika tanımları kümesidir. Management group (C) **kapsamdır** — initiative'i oraya atamak, "birçok subscription" gereksinimini karşılar; ama birim olarak gruplayan şey initiative'dir.
</details>

#### Soru 6: Tek seçim
Bir şirket, yeni politikasının mevcut dağıtımları bozup bozmayacağını görmek istiyor; uyumsuz kaynakların **engellenmesini değil**, yalnızca **raporlanmasını** istiyor. Hangi politika etkisini seçmelidir?

- A) Deny
- B) Audit
- C) Modify
- D) DeployIfNotExists

<details><summary>Cevap</summary>

**B.** Audit kaynağa izin verir ve uyumsuz olarak raporlar. Deny engeller; Modify ve DeployIfNotExists değişiklik yapar.
</details>

#### Soru 7: Eşleştirme
Her politika etkisini davranışıyla eşleştirin: **Deny · Audit · Modify · DeployIfNotExists**

1. Eksik tanılama ayarını otomatik olarak dağıtır.
2. Kurala aykırı oluşturma isteğini reddeder.
3. Oluşturma sırasında eksik bir tag'i ekler.
4. Uyumsuz kaynağa izin verir ama raporlar.

<details><summary>Cevap</summary>

**1 → DeployIfNotExists · 2 → Deny · 3 → Modify · 4 → Audit.**
</details>

#### Soru 8: Senaryo
Bir şirket, tüm subscription'larındaki (şu an 40, her ay yenileri ekleniyor) kaynakların zorunlu `costCenter` tag'ine sahip olmasını istiyor. Yönetim yükünü **en aza** indirmek için politikayı nereye atamalıdır?

- A) Her resource group'a ayrı ayrı
- B) Her subscription'a ayrı ayrı
- C) Subscription'ları içeren management group'a
- D) Her kaynağa ayrı ayrı

<details><summary>Cevap</summary>

**C.** Management group'a yapılan atama altındaki **tüm mevcut ve yeni** subscription'lara miras kalır; tek noktadan yönetilir.
</details>

#### Soru 9: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Deny etkili bir politika atandığında, kurala aykırı **mevcut** kaynaklar otomatik olarak silinir. | ☐ | ☐ |
| b) Azure Policy, mevcut kaynakların uyumluluk durumunu bir panoda gösterir. | ☐ | ☐ |
| c) Azure Policy'yi management group, subscription veya resource group kapsamına atayabilirsiniz. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Mevcut uyumsuz kaynaklar silinmez, "non-compliant" olarak raporlanır; düzeltme için remediation task veya elle müdahale gerekir.
</details>

#### Soru 10: Tek seçim
Bir yönetici, kritik bir storage account'a **ReadOnly** lock uyguladıktan sonra bir uygulamanın hesabın erişim anahtarlarını listeleyemediğini fark ediyor. Bunun sebebi nedir?

- A) Lock, storage account'taki tüm blob'ları şifreledi
- B) ReadOnly lock, anahtar listeleme gibi Resource Manager üzerinden yapılan bazı POST işlemlerini de engeller
- C) Uygulamanın rolü otomatik olarak Reader'a düşürüldü
- D) Lock, storage account'u başka bir bölgeye taşıdı

<details><summary>Cevap</summary>

**B.** ReadOnly lock yalnızca "değiştirmeyi" değil, `listKeys` gibi POST işlemlerini de engeller. Bu yüzden ReadOnly beklenmedik yan etkiler doğurabilir; çoğu durumda CanNotDelete yeterlidir. Lock rolleri değiştirmez (C).
</details>

#### Soru 11: Tek seçim
Bir şirketin veri ekibi; Azure SQL Database, Azure Data Lake, on-prem SQL Server ve Amazon S3'teki verilerin **tek bir haritasını** çıkarmak, hassas verileri **otomatik sınıflandırmak** ve verinin hangi kaynaktan geldiğini (soy ağacı) görmek istiyor. Hangi çözümü kullanmalıdır?

- A) Azure Policy
- B) Microsoft Purview
- C) Azure Monitor
- D) Microsoft Defender for Cloud

<details><summary>Cevap</summary>

**B.** Veri haritası, katalog, sınıflandırma ve lineage Microsoft Purview'un veri yönetişimi yetenekleridir. Policy kaynak yapılandırmasını, Monitor metrik/log'ları, Defender güvenlik duruşunu yönetir.
</details>

#### Soru 12: Çoklu seçim (iki doğru cevap)
Microsoft Purview ile **hangi ikisi** yapılabilir?

- A) Kredi kartı numarası gibi hassas verileri otomatik keşfedip sınıflandırmak
- B) Kullanıcılara MFA zorunluluğu getirmek
- C) Hassas verinin kuruluş dışına gönderilmesini veri kaybı önleme (DLP) politikalarıyla engellemek
- D) Sanal makinelerin boyutunu otomatik büyütmek

<details><summary>Cevap</summary>

**A ve C.** Purview'un risk ve uyumluluk tarafı sınıflandırma, bilgi koruma ve DLP'yi kapsar. MFA Entra ID'nin (→ C1.9), ölçekleme autoscale'in işidir.
</details>

#### Soru 13: Tek seçim
Bir denetçi, **Microsoft'un** Azure için aldığı ISO 27001 sertifikasını ve SOC denetim raporlarını görmek istiyor. Bu belgeler nereden alınır?

- A) Azure Policy compliance panosu
- B) Microsoft Service Trust Portal
- C) Azure Advisor
- D) Microsoft Purview Data Map

<details><summary>Cevap</summary>

**B.** Service Trust Portal, Microsoft'un bağımsız denetim raporlarını ve uyumluluk belgelerini yayımlar. Policy compliance panosu (A) **senin** kaynaklarının kurallarına uyumunu gösterir.
</details>

#### Soru 14: Senaryo
Bir ekip, her gece otomatik olarak bir test resource group'unu silip yeniden oluşturan bir pipeline çalıştırıyor. Bir yönetici bu RG'ye CanNotDelete lock koyduktan sonra pipeline hata vermeye başladı. **En doğru** çözüm hangisidir?

- A) Pipeline'ın kimliğine Owner rolü vermek
- B) Lock'ı test RG'sinden kaldırmak (lock'ı üretim kaynaklarında tutmak)
- C) Lock'ı ReadOnly'ye çevirmek
- D) Bir Audit politikası atamak

<details><summary>Cevap</summary>

**B.** Lock rol yetkisinden bağımsızdır; Owner olmak (A) silmeyi sağlamaz. ReadOnly (C) durumu kötüleştirir. Düzenli silinen test kaynakları için lock uygun değildir; lock kalıcı/üretim kaynakları içindir.
</details>

#### Soru 15: Cümle tamamlama
"Azure Policy'de, ilişkili bir kaynak veya ayar mevcut değilse onu **otomatik olarak dağıtan** etkiye \_\_\_\_\_\_\_\_\_\_ denir."

- A) Append
- B) AuditIfNotExists
- C) DeployIfNotExists
- D) Disabled

<details><summary>Cevap</summary>

**C.** DeployIfNotExists eksik ilişkili kaynağı dağıtır; AuditIfNotExists (B) yalnızca raporlar.
</details>

#### Soru 16: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Resource lock'lar subscription, resource group veya tek kaynak düzeyinde uygulanabilir. | ☐ | ☐ |
| b) CanNotDelete lock'lı bir storage account'taki blob'lar da silinemez. | ☐ | ☐ |
| c) Bir lock'ı kaldırmak için de uygun yetki (ör. Owner veya User Access Administrator) gerekir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Lock yönetim düzlemini (hesabın kendisini) korur, içindeki veriyi değil; blob'ları korumak için soft delete/versioning gibi veri koruma özellikleri gerekir. Lock kaldırmak `Microsoft.Authorization/locks/*` yetkisi ister.
</details>

#### Soru 17: Eşleştirme
Her gereksinimi doğru araçla eşleştirin: **Azure Policy · RBAC · Resource lock · Microsoft Purview**

1. Pazarlama ekibi yalnızca kendi resource group'undaki kaynakları yönetebilsin.
2. Yalnızca onaylı VM boyutları kullanılabilsin.
3. Ağ omurgası kaynakları kazara silinmesin.
4. Hangi veri kaynaklarında kişisel veri olduğu bilinsin.

<details><summary>Cevap</summary>

**1 → RBAC** (kim ne yapabilir, RG kapsamında Contributor) · **2 → Azure Policy** (izin verilen SKU'lar) · **3 → Resource lock** (CanNotDelete) · **4 → Microsoft Purview** (veri keşfi ve sınıflandırma).
</details>

#### Soru 18: Tek seçim
Bir şirket, kaynaklara `env` tag'inin **resource group'tan otomatik olarak kopyalanmasını** ve mevcut tag'siz kaynakların da düzeltilmesini istiyor. Ne yapmalıdır?

- A) Resource group'a tag vermek yeterlidir; tag'ler otomatik miras alınır
- B) *Inherit a tag from the resource group* gibi **Modify** etkili bir politika atamak ve mevcut kaynaklar için **remediation task** çalıştırmak
- C) Resource group'a ReadOnly lock uygulamak
- D) Tüm kullanıcılara Contributor rolü vermek

<details><summary>Cevap</summary>

**B.** Tag'ler varsayılan olarak miras **alınmaz** (A yanlış; → C1.11). Modify etkili politika yeni/güncellenen kaynaklara tag'i ekler; mevcut kaynakları remediation task düzeltir.
</details>

---

**Puanlama:** 16-18 → hazırsın · 12-15 → README 3.2'deki etkiler tablosunu ve 3.3'teki lock türlerini tekrar et · < 12 → konuyu baştan çalış, 3.5'teki laboratuvarı uygula.


---

## 3.3 Kaynak Yönetme ve Dağıtma Araçları: Portal, CLI, PowerShell, Arc, Bicep

> **Sınav Alanı:** Describe Azure management and governance (%30-35) · **Konu Kodu:** C3.3

### 1. Ne?

Azure'da bir kaynağı oluşturmanın, değiştirmenin ve silmenin birden çok yolu vardır; hepsi aynı kapıdan — **Azure Resource Manager (ARM)** — geçer:

```
 Azure portal ─┐
 Azure CLI ────┤
 PowerShell ───┼──►  Azure Resource Manager  ──►  authentication + RBAC + Policy + lock + tag
 REST API/SDK ─┤          (single management layer)    ──►  resource providers (Compute, Storage, …)
 ARM/Bicep ────┘
```

| Araç | Ne? | En iyi olduğu iş |
|---|---|---|
| **Azure portal** | Web tabanlı grafik konsol | Keşif, tek seferlik işler, dashboard'lar, öğrenme |
| **Azure Cloud Shell** | Tarayıcıdan açılan, kimliği hazır Bash/PowerShell kabuğu | Kurulumsuz komut satırı, hızlı betikler |
| **Azure CLI** (`az …`) | Platformlar arası komut satırı aracı | Komut satırı otomasyonu (Bash/PowerShell/cmd) |
| **Azure PowerShell** (`*-Az*` cmdlet'leri) | PowerShell modülü | PowerShell ekosisteminde otomasyon |
| **ARM template / Bicep** | Deklaratif **infrastructure as code** | Tekrarlanabilir, tutarlı ortam kurulumu |
| **Azure Arc** | Azure dışındaki kaynakları ARM'a yansıtma | Hybrid ve multicloud yönetişimi |

Konunun kodu yoktur; pratik, README'deki `az` CLI ve Bicep laboratuvarıdır (3.7).

---

### 2. Neden?

- **Sınav:** "Bu iş için hangi araç?" soruları (kurulumsuz komut satırı → Cloud Shell; aynı ortamı tekrar tekrar kur → ARM template; başka buluttaki sunucu → Arc) bu alanın sabitleridir.
- **Gerçek işte:** Portalda tıklanarak kurulan ortam **tekrarlanamaz** ve **denetlenemez**: "Test'te çalışıyordu, üretimde neden farklı?" sorusunun çoğu zaman cevabı elle yapılmış bir ayardır. IaC ortamı kod olarak saklar, gözden geçirir ve aynen tekrarlar.
- **ARM'ı anlamak**, Policy, lock ve RBAC'ın neden **her araçta** aynı şekilde uygulandığını açıklar (→ C1.9, C1.12).

---

### 3. Nasıl?

#### 3.1 Azure portal

- Web tabanlı, birleşik konsol (`portal.azure.com`): kaynak oluşturma ve yönetme, maliyet ve izleme ekranları, **özelleştirilebilir dashboard**'lar (→ C1.14), erişilebilirlik desteği.
- Microsoft tarafından sürekli güncellenir; bakım için kesinti gerektirmez. Ayrıca mobil cihazlar için **Azure mobile app** vardır *(bağlam)*.
- Sınır: grafik arayüz **tekrarlanabilir ve otomatik** işler için uygun değildir; yüz ortamı aynı şekilde kurmak için betik veya IaC gerekir.

#### 3.2 Azure Cloud Shell, Azure CLI ve Azure PowerShell

**Cloud Shell**, portaldan (üst çubuktaki `>_` simgesi) veya `shell.azure.com`'dan açılan, **tarayıcı tabanlı** ve **kimliği zaten doğrulanmış** etkileşimli bir kabuktur:

- İki deneyim: **Bash** (Azure CLI hazır) ve **PowerShell** (Azure PowerShell hazır); ikisinde de `az` ve `Az` modülü, git, editörler gibi araçlar güncel olarak kurulu gelir.
- **Kurulum gerektirmez**, her cihazdan çalışır; `az login` gerekmez.
- Dosya kalıcılığı isteğe bağlıdır: bir storage account (Azure Files paylaşımı) bağlanırsa `$HOME` oturumlar arasında korunur; **ephemeral session** (storage account olmadan) seçilirse oturum kapanınca dosyalar silinir.

**Azure CLI ↔ Azure PowerShell** — aynı yetenekler, farklı ekosistem; ikisi de Windows, Linux ve macOS'a yerel olarak da kurulabilir:

| İş | Azure CLI | Azure PowerShell |
|---|---|---|
| Giriş | `az login` | `Connect-AzAccount` |
| Resource group oluştur | `az group create --name rg-demo --location westeurope` | `New-AzResourceGroup -Name rg-demo -Location westeurope` |
| Listele | `az group list -o table` | `Get-AzResourceGroup \| Format-Table` |
| Şablon dağıt | `az deployment group create -g rg-demo --template-file main.bicep` | `New-AzResourceGroupDeployment -ResourceGroupName rg-demo -TemplateFile main.bicep` |
| Sil | `az group delete --name rg-demo --yes` | `Remove-AzResourceGroup -Name rg-demo -Force` |

Çıktıyı süzmek: Azure CLI'da `--query` (JMESPath) ve `-o table|tsv|json`; PowerShell'de nesne boru hattı (`Where-Object`, `Select-Object`):

```bash
az vm list --query "[?location=='westeurope'].{name:name, rg:resourceGroup}" -o table
az group show --name rg-demo --query id -o tsv     # to capture a single value into a variable
```

#### 3.3 Azure Arc

**Azure Arc**, Azure **dışındaki** kaynakları Azure Resource Manager'a **yansıtır** (her biri Azure'da bir kaynak olarak görünür) ve onları Azure'dan yönetmeyi sağlar.

| Arc ile yönetilebilenler | Örnek |
|---|---|
| **Sunucular** (Windows/Linux, fiziksel veya sanal) | On-prem datacenter'daki sunucular, AWS/GCP'deki VM'ler |
| **Kubernetes kümeleri** | On-prem veya başka buluttaki Kubernetes |
| **SQL Server** ve Azure veri servisleri | On-prem SQL Server envanteri ve lisans yönetimi; Arc-enabled SQL Managed Instance |
| **VM'ler (VMware vSphere, System Center VMM, Azure Local)** | Mevcut sanallaştırma ortamındaki VM'lerin yaşam döngüsü |

Sağladıkları: tek panelde **envanter**, **Azure Policy** ve **RBAC**, tag'ler, **Defender for Cloud** (→ C1.10), **Azure Monitor** (→ C1.14), güncelleme yönetimi. Yani Azure'un yönetişim araçları, kaynak **nerede çalışırsa çalışsın** uygulanabilir hale gelir (hybrid → C1.1).

#### 3.4 Azure Resource Manager (ARM) ⭐

**ARM**, Azure'un **dağıtım ve yönetim servisidir**. Portal, CLI, PowerShell, REST API ve SDK'lardan gelen **tüm** istekler ARM'dan geçer; ARM isteği doğrular, **yetkilendirir** (RBAC), **Policy** ve **lock**'ları uygular, sonra ilgili resource provider'a iletir. Bu yüzden hangi araçla yapılırsa yapılsın sonuç ve kurallar **tutarlıdır**.

ARM'ın faydaları:

- Kaynakları tek tek değil, **grup halinde** (resource group) yönetmek, dağıtmak ve silmek.
- **Deklaratif şablonlarla** dağıtım; bağımlılıkları doğru sırayla kurmak (önce VNet, sonra VM).
- Aynı şablonla **tekrar tekrar tutarlı** dağıtım.
- Erişim kontrolü (RBAC), tag, lock ve Policy'nin tek noktadan uygulanması.

#### 3.5 Infrastructure as Code (IaC)

**IaC**, altyapıyı **kodla** (metin dosyalarıyla) tanımlayıp dağıtmaktır.

| Fayda | Açıklama |
|---|---|
| **Tekrarlanabilirlik** | Dev, test ve prod aynı şablondan kurulur; "bende çalışıyordu" farkı kalkar |
| **Sürüm kontrolü ve inceleme** | Altyapı değişikliği git'te, pull request ile gözden geçirilir; geçmiş ve geri alma |
| **İdempotentlik** | Aynı şablon tekrar uygulanınca aynı sonuç: eksik olan oluşturulur, değişen güncellenir, aynı olan dokunulmaz |
| **Otomasyon** | CI/CD pipeline'ında otomatik dağıtım (→ J10.5) |

- **Deklaratif** ("**ne** istiyorum": son durumu tanımla, nasıl kurulacağını araç çözsün) ↔ **imperatif** ("**nasıl**": adım adım komutlar — `az` betikleri).
- Azure'un yerel IaC dilleri: **ARM template** (JSON) ve **Bicep**; *(bağlam)* platformlar arası Terraform (→ S8.2).

#### 3.6 ARM template'leri ve Bicep

**ARM template**, dağıtılacak kaynakları tanımlayan **JSON** biçimindeki deklaratif dosyadır. Bölümleri:

| Bölüm | Ne? |
|---|---|
| `parameters` | Dağıtımda verilen değerler (ad, bölge, SKU) |
| `variables` | Şablon içinde yeniden kullanılan hesaplanmış değerler |
| `resources` | Oluşturulacak/güncellenecek kaynaklar |
| `outputs` | Dağıtımdan dönen değerler (endpoint, id) |

Nitelikleri: ARM bağımlılıkları kendisi çözer ve bağımsız kaynakları **paralel** dağıtır; dağıtımdan önce **doğrulama** (validation) ve **what-if** (ne değişecek önizlemesi) yapılabilir; şablonlar modüler olarak birbirini çağırabilir; *(bağlam)* **Template Specs** şablonları Azure'da paylaşılabilir kaynak olarak saklar.

**Bicep**, ARM template'lerin daha okunabilir, kısa bir **alana özgü dilidir (DSL)**; **ARM JSON'a derlenir** ve aynı yeteneklere sahiptir. Microsoft yeni şablonlar için Bicep'i öneriyor. Aynı storage account iki biçimde:

```bicep
// storage.bicep
param storageName string
param location string = resourceGroup().location

var skuName = 'Standard_LRS'

resource st 'Microsoft.Storage/storageAccounts@2023-05-01' = {
  name: storageName
  location: location
  sku: { name: skuName }
  kind: 'StorageV2'
  properties: {
    minimumTlsVersion: 'TLS1_2'
    allowBlobPublicAccess: false
  }
}

output blobEndpoint string = st.properties.primaryEndpoints.blob
```

```json
{
  "$schema": "https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#",
  "contentVersion": "1.0.0.0",
  "parameters": {
    "storageName": { "type": "string" },
    "location": { "type": "string", "defaultValue": "[resourceGroup().location]" }
  },
  "variables": {
    "skuName": "Standard_LRS"
  },
  "resources": [
    {
      "type": "Microsoft.Storage/storageAccounts",
      "apiVersion": "2023-05-01",
      "name": "[parameters('storageName')]",
      "location": "[parameters('location')]",
      "sku": { "name": "[variables('skuName')]" },
      "kind": "StorageV2",
      "properties": {
        "minimumTlsVersion": "TLS1_2",
        "allowBlobPublicAccess": false
      }
    }
  ],
  "outputs": {
    "blobEndpoint": {
      "type": "string",
      "value": "[reference(resourceId('Microsoft.Storage/storageAccounts', parameters('storageName'))).primaryEndpoints.blob]"
    }
  }
}
```

`az bicep build --file storage.bicep` komutu, Bicep dosyasından yukarıdakine eşdeğer ARM JSON'u üretir.

**Portalda şablon üretme (teori).** Portalda tıklayarak yapılan işin ARM JSON karşılığını almak için iki yol vardır:

- **Download a template for automation:** Bir kaynak oluşturma sihirbazının son adımında (**Review + create**), oluşturulmak üzere olan kaynağın şablonunu ve parametre dosyasını verir — *dağıtımdan önce*.
- **Export template:** Var olan bir kaynağın veya resource group'un menüsünden (Automation → **Export template**), mevcut durumun şablonunu verir — *dağıtımdan sonra*.

Dışa aktarılan şablon otomasyon için bir **başlangıç noktasıdır**: sabit kodlanmış adlar ve gereksiz özellikler içerir; parametrelenip temizlenmesi gerekir (bazı kaynak türleri dışa aktarılamayabilir).

#### 3.7 Pratik — imperatif CLI ve deklaratif Bicep (`az` CLI)

Maliyet: App Service planı **F1 (ücretsiz)** katmanında, storage account boş — maliyet kuruşlar düzeyinde. Bütçe alarmın kurulu olsun (→ C1.1).

**(a) İmperatif: adım adım CLI ile oluştur ve sil**

```bash
RG=rg-az900-c113
SUFFIX=$RANDOM
az group create --name $RG --location westeurope
az storage account create --name stc113$SUFFIX --resource-group $RG --sku Standard_LRS
az appservice plan create --name plan-c113 --resource-group $RG --sku F1 --is-linux
az webapp create --name app-c113-$SUFFIX --resource-group $RG --plan plan-c113 \
  --runtime "DOTNETCORE:10.0"                       # valid values: az webapp list-runtimes --os linux
az resource list --resource-group $RG --query "[].{name:name, type:type}" -o table
az group delete --name $RG --yes                    # all four resources go with one command
```

**(b) Deklaratif: Bicep ile dağıt, ikinci dağıtımda değişiklik olmadığını gör**

```bash
RG=rg-az900-c113-iac
ST=stc113iac$RANDOM
az group create --name $RG --location westeurope

# Write the Bicep content from 3.6 to a file (in Cloud Shell you can also open it with: code storage.bicep)
cat > storage.bicep <<'EOF'
param storageName string
param location string = resourceGroup().location
var skuName = 'Standard_LRS'
resource st 'Microsoft.Storage/storageAccounts@2023-05-01' = {
  name: storageName
  location: location
  sku: { name: skuName }
  kind: 'StorageV2'
  properties: { minimumTlsVersion: 'TLS1_2', allowBlobPublicAccess: false }
}
output blobEndpoint string = st.properties.primaryEndpoints.blob
EOF

# 1) Preview before the first deployment → "+ Create" (1 resource will be created)
az deployment group what-if --resource-group $RG --template-file storage.bicep --parameters storageName=$ST

# 2) Deploy and read the output
az deployment group create --resource-group $RG --template-file storage.bicep \
  --parameters storageName=$ST --query properties.outputs.blobEndpoint.value -o tsv

# 3) Preview the same template again → "= Nochange": idempotent, nothing is recreated
az deployment group what-if --resource-group $RG --template-file storage.bicep --parameters storageName=$ST

# 4) Change a value in the template (e.g. allowBlobPublicAccess: true) → what-if shows "~ Modify"; revert it

# 5) Bicep → ARM JSON
az bicep build --file storage.bicep --stdout | head -40

# Cleanup
az group delete --name $RG --yes --no-wait
rm storage.bicep
```

> what-if çıktısında bazı salt okunur özellikler zaman zaman "değişecek" gibi görünebilir (*noise*); bu kaynakların yeniden oluşturulacağı anlamına gelmez.

> **Alıştırma.** Her ihtiyaç için aracı seç: portal · Cloud Shell · Azure CLI/PowerShell betiği · ARM template/Bicep · Azure Arc.
> 1. Yeni katılan bir stajyer Azure'u ilk kez keşfedecek.
> 2. Kişisel bilgisayarına hiçbir şey kuramayan bir yönetici, tarayıcıdan acil bir komut çalıştıracak.
> 3. Her yeni müşteri için aynı 12 kaynaklık ortam, sürüm kontrolünde ve aynı şekilde kurulacak.
> 4. Şirketin kendi datacenter'ındaki 300 Linux sunucusuna Azure Policy ve Defender for Cloud uygulanacak.
>
> <details><summary>Çözüm</summary>
>
> 1. **Azure portal** — grafik, keşfetmeye uygun.
> 2. **Cloud Shell** — kurulumsuz, tarayıcıdan, kimliği hazır.
> 3. **ARM template / Bicep** (IaC) — deklaratif, tekrarlanabilir, git'te sürümlenir; CI/CD ile otomatikleşir.
> 4. **Azure Arc** — sunucular Arc-enabled server olarak ARM'a yansıtılır; Policy ve Defender Azure kaynağıymış gibi uygulanır.
> </details>

---

### 4. Gerçek dünya senaryosu

Bir SaaS şirketi her yeni kurumsal müşteri için ayrı bir ortam (VNet, App Service, Azure SQL, Key Vault, storage, izleme) kuruyor:

1. **Başlangıç:** İlk müşteri ortamı portaldan elle kuruldu; ikincide bir ayar unutuldu ve güvenlik denetiminde açık çıktı.
2. **Şablona geçiş:** Mühendis, ilk ortamın resource group'undan **Export template** ile şablonu aldı, parametreleyip temizledi ve **Bicep**'e çevirdi; dosyalar git deposuna kondu, her değişiklik pull request ile inceleniyor.
3. **Dağıtım:** Pipeline her müşteri için `az deployment group what-if` ile değişikliği önizliyor, onaydan sonra `az deployment group create` çalıştırıyor. Mevcut ortamlara aynı şablon tekrar uygulandığında yalnızca **farklar** değişiyor (idempotent).
4. **Günlük iş:** Destek ekibi küçük sorgular için **Cloud Shell** kullanıyor; kimse dizüstüne araç kurmak zorunda değil.
5. **Hybrid:** Şirketin hâlâ on-prem'de duran lisans sunucuları **Azure Arc** ile Azure'a bağlandı; Azure'daki tag, Policy ve izleme kuralları onlara da uygulanıyor.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** "Hangi araç?" senaryoları ve ARM/IaC nitelikleri hakkında evet/hayır.

| Senaryodaki ifade | Cevap |
|---|---|
| "Aynı ortamı **tekrar tekrar tutarlı** kur", "altyapıyı kod olarak sürümle" | **ARM template / Bicep** (IaC) |
| "**Kurulum yapmadan**, tarayıcıdan komut satırı" | **Azure Cloud Shell** |
| "Hem **Bash** hem **PowerShell** seçeneği sunan tarayıcı kabuğu" | **Cloud Shell** |
| "**Grafik** arayüz", "özelleştirilebilir dashboard" | **Azure portal** |
| "**On-prem / başka buluttaki** sunucuyu Azure'dan yönet, Policy uygula" | **Azure Arc** |
| "Tüm yönetim isteklerinin geçtiği **katman**" | **Azure Resource Manager** |
| "ARM template **hangi biçimde**?" | **JSON** (deklaratif) |
| "Bicep nedir?" | ARM JSON'a derlenen, daha okunabilir DSL |
| "Şablonu dağıtmadan **ne değişeceğini** gör" | **what-if** |

**Tuzaklar:**
- "Azure CLI yalnızca Linux'ta çalışır" → **Hayır** (Windows, macOS, Linux, Cloud Shell).
- "Azure PowerShell, Azure CLI'ın yapamadığı işleri yapar" → genellikle **Hayır**; yetenekler büyük ölçüde aynıdır, sözdizimi farklıdır.
- "ARM template imperatiftir" → **Hayır**, **deklaratiftir** (son durumu tanımlar).
- "Portal, CLI ve PowerShell farklı API'ler kullanır, kurallar farklı uygulanır" → **Hayır**; hepsi ARM'dan geçer, RBAC/Policy/lock aynı uygulanır.
- "Azure Arc, on-prem sunucuları Azure'a **taşır**" → **Hayır**; sunucu yerinde kalır, yalnızca Azure'dan **yönetilir** (taşıma → Azure Migrate, C1.8).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Üretim ortamını portaldan elle kurmak | Elle kurulum tekrarlanamaz ve denetlenemez; IaC kullan |
| "Bicep ayrı bir servis/runtime" | Bicep yalnızca bir dildir; ARM JSON'a derlenir, dağıtımı yine ARM yapar |
| Export edilen şablonu olduğu gibi kullanmak | Sabit adlar/gereksiz özellikler içerir; parametrele ve temizle |
| CLI betiğinin idempotent olduğunu sanmak | İmperatif betik ikinci çalıştırmada "zaten var" hatası verebilir; deklaratif şablon son durumu sağlar |
| Ephemeral Cloud Shell'de dosya saklamak | Oturum kapanınca silinir; kalıcılık için storage bağla, secret'ları Key Vault'ta tut |
| Arc'ı taşıma aracı sanmak | Arc **yönetim** sağlar; taşıma için Azure Migrate |
| what-if çıktısındaki her farkı gerçek değişiklik sanmak | Bazı salt okunur özellikler "noise" olarak görünebilir |

---

### 7. Performans & trade-off

| Seçim | Kazanç | Bedel |
|---|---|---|
| Portal | Öğrenmesi en kolay, görsel | Tekrarlanamaz, ölçeklenmez, hata yapmaya açık |
| CLI/PowerShell betiği (imperatif) | Hızlı otomasyon, esnek mantık | Durum takibi ve idempotentlik betiği yazana kalır |
| ARM JSON | Tam yetenek, her araç destekler | Uzun, okunması/yazılması zor |
| Bicep | Kısa, okunabilir, modüler, aynı yetenek | Ek araç (Bicep CLI — `az` içinde gelir); yalnızca Azure |
| *(bağlam)* Terraform | Çok bulutlu, geniş ekosistem | Ayrı durum (state) dosyası yönetimi (→ S8.2) |
| Azure Arc | Azure yönetişimi her yerde | Ajan kurulumu, bağlantı gereksinimi; bazı Arc özellikleri ücretli |
| Cloud Shell | Kurulumsuz, kimlik hazır | Oturum zaman aşımı; ephemeral modda dosyalar kalıcı değil |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe features and tools for managing and deploying Azure resources](https://learn.microsoft.com/en-us/training/modules/describe-features-tools-manage-deploy-azure-resources/)
- [What is Azure Resource Manager?](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/overview) · [ARM templates overview](https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/overview) · [What is Bicep?](https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/overview)
- [ARM template deployment what-if](https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/deploy-what-if) · [Export template in the portal](https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/export-template-portal)
- [Azure Cloud Shell features](https://learn.microsoft.com/azure/cloud-shell/features) · [Cloud Shell ephemeral sessions](https://learn.microsoft.com/azure/cloud-shell/get-started/ephemeral)
- [Azure Arc overview](https://learn.microsoft.com/en-us/azure/azure-arc/overview)
- [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/) · [Azure PowerShell](https://learn.microsoft.com/en-us/powershell/azure/)


### 📝 Bölüm 3.3 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir şirket, geliştirme, test ve üretim ortamlarının **aynı şekilde ve tekrar tekrar tutarlı** kurulmasını, altyapı tanımlarının da git'te sürümlenmesini istiyor. Hangisini kullanmalıdır?

- A) Azure portal
- B) ARM template'leri veya Bicep
- C) Azure Advisor
- D) Azure Service Health

<details><summary>Cevap</summary>

**B.** Tekrarlanabilir, sürüm kontrollü, deklaratif dağıtım = infrastructure as code (ARM template / Bicep). Portal elle ve tekrarlanamaz; Advisor ve Service Health izleme araçlarıdır (→ C1.14).
</details>

#### Soru 2: Tek seçim
Bir yönetici, kendi bilgisayarına hiçbir araç kuramıyor ve **tarayıcıdan**, kimliği zaten doğrulanmış bir komut satırıyla Azure kaynaklarını yönetmek istiyor. Hangisini kullanmalıdır?

- A) Azure Cloud Shell
- B) Azure Arc
- C) Azure Resource Manager template
- D) Azure mobile app

<details><summary>Cevap</summary>

**A.** Cloud Shell tarayıcı tabanlıdır, kurulum gerektirmez, kimlik hazırdır ve Azure CLI/PowerShell yüklü gelir.
</details>

#### Soru 3: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Cloud Shell hem Bash hem de PowerShell deneyimi sunar. | ☐ | ☐ |
| b) Azure CLI yalnızca Linux işletim sisteminde çalışır. | ☐ | ☐ |
| c) Azure CLI ve Azure PowerShell ile büyük ölçüde aynı yönetim işleri yapılabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Azure CLI Windows, macOS ve Linux'ta çalışır; iki araç aynı yetenekleri farklı sözdizimiyle sunar.
</details>

#### Soru 4: Tek seçim
Bir şirketin kendi datacenter'ında ve başka bir bulut sağlayıcısında çalışan sunucuları var. Şirket bu sunuculara **Azure Policy** uygulamak ve onları Azure portalında envanter olarak görmek istiyor. Hangi hizmeti kullanmalıdır?

- A) Azure Migrate
- B) Azure Arc
- C) Azure Cloud Shell
- D) Azure Virtual Desktop

<details><summary>Cevap</summary>

**B.** Arc, Azure dışındaki sunucuları ve Kubernetes kümelerini ARM'a yansıtıp Policy, RBAC, Defender ve Monitor'ü uygulanabilir kılar. Azure Migrate (A) sunucuları Azure'a **taşır**; burada taşıma değil yerinde yönetim isteniyor.
</details>

#### Soru 5: Tek seçim
Azure portal, Azure CLI, Azure PowerShell ve REST API'den gelen tüm kaynak yönetimi isteklerinin geçtiği, kimlik doğrulama ve yetkilendirmeyi uygulayan katman hangisidir?

- A) Microsoft Entra ID
- B) Azure Resource Manager
- C) Azure Monitor
- D) Azure Policy

<details><summary>Cevap</summary>

**B.** ARM, Azure'un dağıtım ve yönetim servisidir; tüm araçlar aynı ARM API'sini kullanır, bu yüzden RBAC, Policy ve lock her araçta tutarlı uygulanır. Entra ID (A) kimliği doğrular ama yönetim isteklerini işleyen katman ARM'dır.
</details>

#### Soru 6: Tek seçim
ARM template'leri hangi biçimde yazılır?

- A) YAML
- B) XML
- C) JSON
- D) PowerShell betiği

<details><summary>Cevap</summary>

**C.** ARM template'leri JSON biçimindedir; Bicep ise ARM JSON'a derlenen, daha okunabilir bir dildir.
</details>

#### Soru 7: Çoklu seçim (iki doğru cevap)
Infrastructure as code (IaC) yaklaşımının **hangi ikisi** faydasıdır?

- A) Altyapı tanımları sürüm kontrolünde tutulup gözden geçirilebilir
- B) Kaynakların hiçbir zaman ücretlendirilmemesini sağlar
- C) Aynı ortam tekrar tekrar tutarlı biçimde kurulabilir
- D) Azure portal kullanımını zorunlu hale getirir

<details><summary>Cevap</summary>

**A ve C.** IaC tekrarlanabilirlik, sürüm kontrolü/inceleme, idempotent dağıtım ve otomasyon sağlar. Maliyeti ortadan kaldırmaz; portala bağımlılığı tersine azaltır.
</details>

#### Soru 8: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) ARM template'leri deklaratiftir: kurulacak son durumu tanımlarsınız. | ☐ | ☐ |
| b) Aynı ARM template'i aynı resource group'a ikinci kez dağıtmak, kaynakların kopyalarını oluşturur. | ☐ | ☐ |
| c) Resource Manager, şablondaki bağımsız kaynakları paralel olarak dağıtabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Şablon dağıtımı **idempotenttir**: aynı ad ve ayarlarla tekrar uygulandığında kopya oluşmaz, değişiklik yoksa hiçbir şey değişmez. ARM bağımlılıkları çözer, bağımsız kaynakları paralel dağıtır.
</details>

#### Soru 9: Tek seçim
Bir ekip, ARM template'lerinin JSON sözdizimini fazla uzun buluyor ve aynı yetenekleri daha kısa, okunabilir bir dille kullanmak istiyor. Ne kullanmalıdır?

- A) Bicep
- B) Azure Policy
- C) Azure Arc
- D) Azure Blueprints

<details><summary>Cevap</summary>

**A.** Bicep, ARM JSON'a derlenen Azure'a özgü bir dildir; aynı yetenekler, daha kısa sözdizimi.
</details>

#### Soru 10: Eşleştirme
Her ihtiyacı en uygun araçla eşleştirin: **Azure portal · Cloud Shell · ARM template · Azure Arc**

1. Grafik arayüzle kaynakları keşfetmek ve kişisel bir dashboard oluşturmak.
2. AWS'deki VM'lere Azure tag'leri ve politikaları uygulamak.
3. Kurulumsuz, tarayıcıdan `az` komutları çalıştırmak.
4. 50 müşteri ortamını aynı tanımdan kurmak.

<details><summary>Cevap</summary>

**1 → Azure portal · 2 → Azure Arc · 3 → Cloud Shell · 4 → ARM template** (veya Bicep).
</details>

#### Soru 11: Tek seçim
Bir mühendis, bir ARM/Bicep şablonunu dağıtmadan **önce**, mevcut ortamda hangi kaynakların oluşturulacağını, değiştirileceğini veya silineceğini görmek istiyor. Ne kullanmalıdır?

- A) Export template
- B) What-if işlemi
- C) Azure Advisor
- D) Resource lock

<details><summary>Cevap</summary>

**B.** `az deployment group what-if`, dağıtımın etkisini (Create / Modify / NoChange / Delete) önceden gösterir. Export template (A) mevcut durumun şablonunu verir.
</details>

#### Soru 12: Senaryo
Bir yönetici portalda bir web uygulamasını elle oluşturdu ve aynı yapılandırmayı otomasyona dönüştürmek için bir **başlangıç şablonu** istiyor. En hızlı yol hangisidir?

- A) Kaynağı silip yeniden oluşturmak
- B) Resource group'un veya kaynağın **Export template** seçeneğiyle mevcut durumun ARM şablonunu almak
- C) Azure Service Health'ten rapor indirmek
- D) Azure Arc'a bağlamak

<details><summary>Cevap</summary>

**B.** Export template (veya oluşturma sırasında *Download a template for automation*) portalda yapılan işin ARM JSON karşılığını verir; otomasyon için parametrelenip temizlenmesi gerekir.
</details>

#### Soru 13: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Arc, on-prem sunucuları Azure datacenter'larına taşır. | ☐ | ☐ |
| b) Azure Arc ile Azure dışındaki Kubernetes kümeleri de yönetilebilir. | ☐ | ☐ |
| c) Azure Arc ile bağlanan sunuculara Microsoft Defender for Cloud uygulanabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Arc kaynağı yerinde bırakır ve Azure'dan yönetilebilir kılar; taşıma Azure Migrate'in işidir (→ C1.8).
</details>

#### Soru 14: Tek seçim
Bir kullanıcı Azure CLI ile bir kaynak silmeye çalışıyor, ancak kaynakta CanNotDelete lock var. Aynı kullanıcı aynı işlemi portaldan denerse ne olur?

- A) Portal lock'ları yok saydığı için silme başarılı olur
- B) Silme yine reddedilir; çünkü portal da CLI da istekleri Azure Resource Manager üzerinden gönderir
- C) Portal lock'ı otomatik olarak kaldırır
- D) Kaynak başka bir resource group'a taşınır

<details><summary>Cevap</summary>

**B.** Tüm araçlar ARM'dan geçer; lock, RBAC ve Policy hangi araç kullanılırsa kullanılsın aynı şekilde uygulanır (→ C1.12).
</details>

#### Soru 15: Tek seçim
Cloud Shell'i **storage account bağlamadan** (ephemeral session) kullanan bir kullanıcı için hangisi doğrudur?

- A) Oturumda oluşturulan dosyalar oturum kapandığında silinir
- B) Cloud Shell hiç açılmaz
- C) Azure CLI kullanılamaz, yalnızca PowerShell çalışır
- D) Dosyalar otomatik olarak OneDrive'a yedeklenir

<details><summary>Cevap</summary>

**A.** Ephemeral oturumda dosyalar kalıcı değildir; kalıcılık için bir storage account (Azure Files paylaşımı) bağlanmalıdır. Her iki kabuk da çalışır.
</details>

#### Soru 16: Senaryo
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


---

## 3.4 İzleme Araçları: Monitor, Log Analytics, Service Health, Advisor

> **Sınav Alanı:** Describe Azure management and governance (%30-35) · **Konu Kodu:** C3.4

### 1. Ne?

AZ-900 üç izleme aracını sorar; her biri **farklı bir soruya** cevap verir:

| Araç | Cevapladığı soru | Kısaca |
|---|---|---|
| **Azure Advisor** | "Kaynaklarımı **nasıl daha iyi** yapılandırabilirim?" | Kişiselleştirilmiş **öneri** servisi (5 kategori) |
| **Azure Service Health** | "**Azure'da** benim kaynaklarımı etkileyen bir sorun/bakım var mı?" | Azure Status + Service Health + Resource Health |
| **Azure Monitor** | "Uygulamam ve kaynaklarım **şu anda nasıl** çalışıyor, ne oldu?" | Metrik + log toplama, analiz (Log Analytics), uyarı (alerts), uygulama izleme (Application Insights), dashboard ve Workbooks |

Bu konuda kod yok; pratik, `az` CLI komutlarıyla (Advisor önerileri, action group, metrik ve Service Health uyarısı, Log Analytics sorgusu) yapılır. Portal ekranları (Monitor sayfası, dashboard, Workbooks galerisi) teoride anlatılır.

---

### 2. Neden?

- **Sınav:** Bu üç aracın birbirine karıştırılması bu alanın klasik tuzağıdır: "öneri" mi (Advisor), "Azure kaynaklı olay" mı (Service Health), "benim metriğim/log'um" mu (Monitor)? Service Health'in üç parçasının **kapsam farkı** da sık sorulur.
- **Gerçek işte:** Bir kesintide ilk soru "sorun bizde mi, Azure'da mı?"dır. Service Health bunu dakikalar içinde ayırır; Monitor ve Application Insights ise sorunun **nerede** olduğunu gösterir. Advisor da maliyet ve güvenilirlik iyileştirmelerini **ücretsiz** olarak listeler.
- **Bağlantılar:** Maliyet önerileri → C1.11 (Cost Management), güvenlik önerileri → C1.10 (Defender for Cloud), KQL derinliği → C2.13, observability derinliği → M13.

---

### 3. Nasıl?

#### 3.1 Azure Advisor

Kaynak yapılandırmanı ve kullanım (telemetri) verini analiz edip **kişiselleştirilmiş, uygulanabilir öneriler** veren ücretsiz servis. Öneriler beş kategoridedir ⭐:

| Kategori | Amaç | Örnek öneri |
|---|---|---|
| **Reliability** (eski adı *High Availability*) | İş açısından kritik uygulamaların sürekliliği | Tek instance VM'i availability set/zone'a taşı, storage için soft delete aç |
| **Security** | Güvenlik açığına yol açabilecek tehdit ve zafiyetleri tespit | Defender for Cloud önerileri (Advisor'da da görünür), MFA |
| **Performance** | Uygulamaların hızını ve yanıt süresini iyileştirme | Veritabanı için önerilen index, daha uygun disk türü |
| **Cost** | Genel Azure harcamasını optimize etme | Az kullanılan VM'i küçült veya kapat, **reservation** al |
| **Operational Excellence** | Süreç/iş akışı verimliliği, yönetilebilirlik, dağıtım iyi uygulamaları | Service Health uyarısı kur, kaynakları tag'le |

- **Advisor score:** kategori puanlarından hesaplanan, önerilere ne kadar uyduğunu gösteren yüzde; günlük yenilenir.
- Öneriler üzerinde yapılabilenler: **uygula** (çoğu zaman tek tıkla veya ilgili kaynağa yönlendirme), **ertele** (belirli süre), **kapat** (dismiss), öneriler için **uyarı** kur, PDF/CSV olarak **dışa aktar**.
- Advisor **önerir**, kendiliğinden değiştirmez; Azure Policy gibi **zorlamaz** (→ C1.12).

#### 3.2 Azure Service Health

Azure platformunun sağlık durumunu **üç kapsamda** gösterir ⭐:

| Bileşen | Kapsam | Ne gösterir? |
|---|---|---|
| **Azure Status** | **Tüm Azure**, herkese açık (`azure.status.microsoft`) | Geniş çaplı (birçok müşteriyi etkileyen) olayların genel durum sayfası; giriş gerektirmez |
| **Service Health** | **Senin** subscription'ların, kullandığın servisler ve region'lar | **Service issues** (hizmet sorunları), **planned maintenance** (planlı bakım), **health advisories** (aksiyon gerektiren değişiklikler, ör. bir özelliğin emekliliği), **security advisories**; olay geçmişi ve **kök neden analizi (RCA)** raporları; **Service Health alert**'leri |
| **Resource Health** | **Tek bir kaynak** (ör. bir VM, bir SQL veritabanı) | Kaynağın mevcut ve geçmiş sağlık durumu (Available / Unavailable / Degraded / Unknown) ve sorun Azure platformundan mı, kullanıcı eyleminden mi kaynaklanıyor |

> Kural: **genel Azure** → Azure Status · **benim aboneliğim/bölgem** → Service Health · **tek bir kaynağım** → Resource Health.

Service Health uyarıları, teknik olarak **Activity Log alert**'tir (kategori = ServiceHealth) ve bir **action group**'a (e-posta, SMS, webhook…) bağlanır (pratik → 3.6).

#### 3.3 Azure Monitor

Azure, on-prem ve diğer bulutlardaki kaynaklardan **telemetri toplayan, analiz eden ve buna göre harekete geçen** birleşik izleme platformu.

**İki temel veri türü:**

| | **Metrics** (metrikler) | **Logs** (log'lar) |
|---|---|---|
| Ne? | Belirli aralıklarla toplanan **sayısal** değerler (CPU %, istek sayısı, gecikme) | Zaman damgalı **olay kayıtları** (metin, yapılandırılmış alanlar) |
| Güçlü yanı | Hafif, **neredeyse gerçek zamanlı**, grafik ve eşik uyarısı | Zengin bağlam, çok kaynaklı **sorgulama** (KQL), kök neden analizi |
| Araç | **Metrics explorer** | **Log Analytics** (workspace) |

**Veri kaynakları (katmanlar):** uygulama (Application Insights), işletim sistemi (Azure Monitor Agent), Azure kaynakları (platform metrikleri, resource log'ları), subscription (**Activity Log** — "kim, ne zaman, hangi kaynağı değiştirdi?"), tenant (Entra ID log'ları).

**Azure Monitor'un parçaları (sınavın adını andığı):**

- **Log Analytics:** log verisini toplayan **workspace** ve bu veriyi **KQL** (Kusto Query Language) ile sorgulama aracı; birçok kaynağın log'unu tek yerde birleştirir.

  ```kusto
  // Top 5 requests with the most failures in the last day (Application Insights table)
  AppRequests
  | where TimeGenerated > ago(1d) and Success == false
  | summarize failures = count() by Name
  | top 5 by failures
  ```

- **Azure Monitor alerts:** bir **koşul** sağlandığında (metrik eşiği, log sorgusu sonucu, Activity Log olayı) çalışan **uyarı kuralı** → bir **action group**'u tetikler. Uyarı türleri: **metric alert**, **log search alert**, **activity log alert** (Service Health ve Resource Health uyarıları dahil); *(bağlam)* Prometheus ve smart detection uyarıları. Action group eylemleri: e-posta, SMS, push, sesli arama, **webhook**, **Logic App**, **Azure Function**, Automation runbook, ITSM.
- **Application Insights:** uygulama performans izleme (**APM**) — istek süreleri ve sayıları, **hata oranı**, **bağımlılık** çağrıları (DB, HTTP), sayfa görüntüleme ve tarayıcı performansı, exception'lar, uygulama haritası; **availability test**'ler (uygulamaya dünyanın farklı noktalarından düzenli istek atıp erişilebilirliği ölçme). Uygulama SDK/OpenTelemetry ile veya bazı servislerde **kod değişikliği olmadan** (auto-instrumentation) izlenebilir; Azure dışında (on-prem, başka bulut) çalışan uygulamaları da izler. Güncel Application Insights kaynakları bir **Log Analytics workspace**'ine bağlıdır (*workspace-based*; klasik kaynaklar 29 Şubat 2024'te emekliye ayrıldı).

#### 3.4 Azure Monitor merkezi dashboard'u ve görselleştirme

- **Azure Monitor sayfası** (portal): tüm izleme yeteneklerine **tek giriş noktası** — Metrics, Logs, Alerts, Insights, Workbooks, Activity Log.
- **Azure dashboard'ları:** metrik grafiklerini, log sorgu sonuçlarını, kaynak kutucuklarını ve Markdown notlarını **tek ekranda** toplayan, kaydedilip **RBAC ile paylaşılabilen** portal panoları (ör. ekip için "üretim sağlığı" panosu). *(bağlam)* Dashboard'lar JSON olarak dışa aktarılıp şablonla dağıtılabilir; **Grafana** (Azure Managed Grafana) da Azure Monitor verisini gösterebilir.
- **Insights:** belirli kaynak türleri için **hazır (küratörlü)** izleme görünümleri — **VM insights**, **Container insights** (AKS), Storage insights, **Application Insights**.

#### 3.5 Azure Monitor raporları: Workbooks

- **Workbooks:** metrik, log sorgusu, parametre (zaman aralığı, subscription seçici), grafik, tablo ve açıklama metnini birleştiren **etkileşimli rapor** belgeleri. Galeride hazır şablonlar vardır (ör. VM performansı, Activity Log özeti, Advisor önerileri); kopyalanıp özelleştirilebilir ve ekiple paylaşılabilir.
- Dashboard ↔ Workbook: dashboard **anlık bakış panosu** (birçok kaynaktan kutucuk); workbook **zengin, etkileşimli analiz raporu** (parametreli, adım adım hikâye).
- Diğer rapor yolları: Log Analytics sorgusunun sonucunu `render timechart` ile grafiğe dökmek; uyarı geçmişi ve **Activity Log** görünümleri; *(bağlam)* Power BI'a aktarma.

#### 3.6 Pratik — CLI ile izleme ve uyarı

Maliyet: action group, uyarı kuralları ve boş bir storage account'un maliyeti ihmal edilebilir; Log Analytics workspace yalnızca **alınan veri** kadar ücret çıkarır (bu laboratuvarda neredeyse sıfır). Bütçe alarmın kurulu olsun (→ C1.1, C1.11). Komutlar Cloud Shell'de (Bash) çalıştırılabilir.

```bash
RG=rg-az900-c114
LOC=westeurope
az group create --name $RG --location $LOC

# 1) Advisor recommendations (based on existing resources in the subscription; the list may be empty in an empty subscription)
az advisor recommendation list --output table
az advisor recommendation list --category Cost --output table
# CLI category names: Cost, HighAvailability (= Reliability), OperationalExcellence, Performance, Security

# 2) Action group: who/what to notify when an alert fires
az monitor action-group create --resource-group $RG --name ag-ops --short-name ops \
  --action email ops-team ops@example.com
AG_ID=$(az monitor action-group show --resource-group $RG --name ag-ops --query id --output tsv)

# 3) Metric alert: send an email if the transaction count on the storage account exceeds the threshold
SA=stc114$RANDOM
az storage account create --resource-group $RG --name $SA --location $LOC --sku Standard_LRS
SA_ID=$(az storage account show --resource-group $RG --name $SA --query id --output tsv)
az monitor metrics alert create --resource-group $RG --name alert-sa-transactions \
  --scopes $SA_ID --condition "total Transactions > 1000" \
  --window-size 5m --evaluation-frequency 5m --action $AG_ID \
  --description "More than 1000 requests in 5 minutes"
az monitor metrics list --resource $SA_ID --metric Transactions --interval PT1H --output table

# 4) Service Health alert: service issue / planned maintenance affecting my subscription → email
SUB_ID=$(az account show --query id --output tsv)
az monitor activity-log alert create --resource-group $RG --name alert-service-health \
  --scope /subscriptions/$SUB_ID --condition category=ServiceHealth --action-group $AG_ID

# 5) Log Analytics workspace + KQL query
az monitor log-analytics workspace create --resource-group $RG --workspace-name law-c114 --location $LOC
WS=$(az monitor log-analytics workspace show --resource-group $RG --workspace-name law-c114 \
  --query customerId --output tsv)
# A query that also works on an empty workspace: an inline table to show KQL's where/summarize/order flow
az monitor log-analytics query --workspace $WS --analytics-query '
datatable(Service:string, DurationMs:int, Succeeded:bool)
["api", 120, true, "api", 900, false, "web", 80, true, "api", 1500, false]
| where Succeeded == false
| summarize failures = count(), avgDuration = avg(DurationMs) by Service
| order by failures desc' --output table

# 6) List the alert rules, then clean up
az monitor metrics alert list --resource-group $RG --output table
az monitor activity-log alert list --resource-group $RG --output table
az group delete --name $RG --yes --no-wait
```

> Workspace'e gerçek log akması için kaynaklarda **diagnostic settings** tanımlanır (ör. Activity Log'u workspace'e gönder → `AzureActivity` tablosu); bu, C2.13'te derinleştirilir.

---

### 4. Gerçek dünya senaryosu

Bir e-ticaret şirketinde pazartesi sabahı ödeme sayfası yavaşlıyor:

1. **Application Insights** uyarısı (log search alert: "son 5 dakikada ödeme isteklerinin hata oranı %5'i geçti") **action group** üzerinden nöbetçi ekibe SMS ve Teams webhook'u gönderiyor.
2. Ekip önce **Service Health**'e bakıyor: West Europe'ta Azure SQL Database için aktif bir **service issue** var mı? Yok. **Resource Health**: veritabanı *Available*. Sorun Azure'da değil, uygulamada.
3. Application Insights'ın **bağımlılık** görünümü, ödeme servisinin dış bir kur servisine yaptığı çağrının 4 saniyeye çıktığını gösteriyor; **Log Analytics**'te KQL ile hangi sürümden sonra başladığı bulunuyor (Activity Log'da o sabah bir deployment var).
4. Olay sonrası ekip, haftalık toplantı için hata oranı ve gecikmeyi gösteren bir **Workbook** hazırlıyor; operasyon ekranına bir **dashboard** sabitleniyor.
5. Aynı hafta **Advisor**, gece boş çalışan iki büyük test VM'ini (Cost) ve tek instance çalışan bir üretim VM'ini (Reliability) öneri olarak listeliyor; ekip birini kapatıyor, diğerini zone'lara yayıyor.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Neredeyse tamamı **araç seçimi**: "Hangi aracı kullanmalısınız?" senaryoları ve evet/hayır tabloları. Konfigürasyon ayrıntısı sorulmaz.

| Senaryodaki ifade | Doğru araç |
|---|---|
| "Maliyeti düşürmek/güvenilirliği artırmak için **öneriler**", "kullanılmayan VM'leri bul" | **Azure Advisor** |
| "Microsoft'un **planlı bakımı** benim VM'lerimi etkileyecek mi?", "bölgemdeki Azure kesintisinden haberdar ol" | **Service Health** (+ Service Health alert) |
| "**Tek bir VM** neden erişilemez oldu, platform sorunu mu?" | **Resource Health** |
| "Azure'un **genel** durumu, giriş yapmadan" | **Azure Status** sayfası |
| "Uygulamanın **istek süreleri, hata oranı, bağımlılıkları**" | **Application Insights** |
| "Birden çok kaynağın **log'larını sorgula/analiz et**" | **Log Analytics** (KQL) |
| "CPU %80'i geçince **e-posta/SMS** gönder" | **Azure Monitor alert** + **action group** |
| "Kim bu kaynağı sildi/değiştirdi?" | **Activity Log** (Azure Monitor) |
| "Metrikleri ve log'ları **tek ekranda** topla, ekiple paylaş" | **Azure dashboard** |
| "Parametreli, **etkileşimli rapor**" | **Workbooks** |

**Tuzaklar:**
- Advisor ↔ Defender for Cloud: ikisi de güvenlik önerisi gösterir; ama **beş kategoride genel öneri** = Advisor, **güvenlik duruşu/secure score/tehdit koruması** = Defender for Cloud (→ C1.10).
- Advisor ↔ Azure Policy: Advisor **önerir**; Policy **zorlar** (→ C1.12).
- Service Health ↔ Azure Monitor: Service Health **Azure platformunun** sağlığı; Monitor **senin kaynaklarının ve uygulamanın** telemetrisi.
- Azure Status herkese açıktır ve **kişiselleştirilmemiştir**; "benim aboneliğime özel" diyorsa Service Health.
- Application Insights, Azure Monitor'un **bir parçasıdır** ve Azure dışında çalışan uygulamaları da izleyebilir.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "Advisor önerileri otomatik uygular" | Advisor önerir; uygulamak senin kararındır |
| "Azure Status benim kaynaklarımı gösterir" | Azure Status geneldir; kişisel görünüm Service Health, tek kaynak Resource Health |
| "Service Health benim uygulamamın hatalarını gösterir" | Yalnızca Azure platformu kaynaklı olayları; uygulama hataları için Application Insights/Monitor |
| "Uyarı kuralı kendi başına bildirim gönderir" | Bildirim/eylem **action group** ile tanımlanır; kural action group'u tetikler |
| "Metrikler ve log'lar aynı şey" | Metrik sayısal ve hafif (grafik/eşik); log zengin olay kaydı (KQL ile sorgulanır) |
| "Application Insights yalnızca Azure'daki uygulamalar için" | On-prem ve diğer bulutlarda çalışan uygulamaları da izler |
| "Log Analytics'e her şeyi gönderelim" | Log'un maliyeti **alınan veri** hacmine bağlıdır; gereksiz log ve uzun saklama süresi faturayı büyütür |
| Service Health uyarısı kurmamak | Planlı bakım ve bölgesel olaylardan e-postayla haberdar olmak için abonelik başına bir Service Health alert kurulmalıdır |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Yalnızca platform metrikleri | Ücretsiz, otomatik, hafif | Bağlam yok: "neden" sorusunu cevaplamaz |
| Log'ları Log Analytics'e toplamak | Çok kaynaklı sorgu, kök neden analizi | Veri alma (GB) ve saklama maliyeti; örnekleme/filtre planı gerekir |
| Application Insights | Uygulama düzeyinde görünürlük, bağımlılık haritası, availability test | Telemetri hacmi maliyeti; örnekleme ayarı; az da olsa uygulama yükü |
| Çok sayıda hassas uyarı | Sorunu erken yakalama | **Alert yorgunluğu**: ekip uyarıları görmezden gelmeye başlar |
| Metrik alert vs log search alert | Metrik: hızlı ve ucuz · Log: karmaşık koşul | Log alert daha gecikmeli ve sorgu başına maliyetli |
| Dashboard vs Workbook | Dashboard: anlık bakış · Workbook: derin, etkileşimli rapor | İkisini birden sürdürmek bakım ister |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe monitoring tools in Azure](https://learn.microsoft.com/en-us/training/modules/describe-monitoring-tools-azure/) · [Describe the purpose of Azure Advisor](https://learn.microsoft.com/en-gb/training/modules/describe-monitoring-tools-azure/2-describe-purpose-of-azure-advisor)
- [Azure Advisor overview](https://learn.microsoft.com/en-us/azure/advisor/advisor-overview) · [`az advisor`](https://learn.microsoft.com/en-us/cli/azure/advisor)
- [Azure Service Health overview](https://learn.microsoft.com/en-us/azure/service-health/overview) · [Resource Health overview](https://learn.microsoft.com/en-us/azure/service-health/resource-health-overview) · [Azure status](https://azure.status.microsoft/)
- [Azure Monitor overview](https://learn.microsoft.com/en-us/azure/azure-monitor/overview) · [Alerts overview](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-overview) · [Action groups](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/action-groups)
- [Application Insights overview](https://learn.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview) · [Classic Application Insights retirement (29 Şubat 2024)](https://azure.microsoft.com/updates/we-re-retiring-classic-application-insights-on-29-february-2024)
- [Log Analytics tutorial](https://learn.microsoft.com/en-us/azure/azure-monitor/logs/log-analytics-tutorial) · [Azure Workbooks](https://learn.microsoft.com/en-us/azure/azure-monitor/visualize/workbooks-overview) · [Azure dashboards](https://learn.microsoft.com/en-us/azure/azure-portal/azure-portal-dashboards)


### 📝 Bölüm 3.4 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir şirket, düşük kullanımlı sanal makinelerini tespit edip maliyetini düşürmek için **kişiselleştirilmiş öneriler** almak istiyor. Hangi servisi kullanmalıdır?

- A) Azure Service Health
- B) Azure Advisor
- C) Azure Monitor alerts
- D) Azure Policy

<details><summary>Cevap</summary>

**B.** Advisor, kullanım verisine dayanarak Cost, Reliability, Security, Performance ve Operational Excellence kategorilerinde öneri verir; "az kullanılan VM'i küçült/kapat" tipik bir Cost önerisidir. Policy kural zorlar, öneri vermez.
</details>

#### Soru 2: Çoklu seçim (üç doğru cevap)
Aşağıdakilerden **hangi üçü** Azure Advisor öneri kategorisidir?

- A) Reliability
- B) Compliance
- C) Operational Excellence
- D) Cost
- E) Networking

<details><summary>Cevap</summary>

**A, C, D.** Beş kategori: Reliability, Security, Performance, Cost, Operational Excellence. Compliance (uyumluluk) Policy/Defender for Cloud'un, Networking ise bir servis alanının adıdır.
</details>

#### Soru 3: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Advisor, önerilerini kaynaklara otomatik olarak uygular. | ☐ | ☐ |
| b) Azure Advisor önerileri ertelenebilir veya kapatılabilir. | ☐ | ☐ |
| c) Azure Advisor güvenlik önerileri de sunar. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Advisor yalnızca önerir; uygulama kararı kullanıcınındır. Öneriler ertelenebilir/kapatılabilir. Security kategorisi vardır (öneriler Defender for Cloud ile entegredir).
</details>

#### Soru 4: Tek seçim
Bir yönetici, Microsoft'un önümüzdeki hafta **kendi aboneliğindeki** VM'leri etkileyecek planlı bakımından önceden e-postayla haberdar olmak istiyor. Ne yapmalıdır?

- A) Azure Status sayfasını her gün kontrol etmek
- B) Azure Service Health'te bir Service Health alert oluşturmak
- C) Azure Advisor score'u izlemek
- D) Bir Workbook oluşturmak

<details><summary>Cevap</summary>

**B.** Service Health, **senin** subscription'larını ve region'larını etkileyen hizmet sorunlarını ve **planlı bakımı** gösterir ve uyarı (action group ile e-posta) kurulabilir. Azure Status geneldir ve kişiselleştirilmemiştir.
</details>

#### Soru 5: Eşleştirme
Her ihtiyacı doğru bileşenle eşleştirin: **Azure Status · Service Health · Resource Health**

1. Tek bir SQL veritabanının dün gece neden erişilemez olduğunu ve sorunun platformdan mı kaynaklandığını görmek
2. Giriş yapmadan, Azure'un genelinde geniş çaplı bir kesinti olup olmadığını görmek
3. Kullandığım region'larda ve servislerde aktif bir hizmet sorunu ve geçmiş olayların kök neden raporlarını görmek

<details><summary>Cevap</summary>

**1 → Resource Health** (tek kaynak) · **2 → Azure Status** (genel, herkese açık) · **3 → Service Health** (abonelik/bölge kapsamı, RCA raporları).
</details>

#### Soru 6: Tek seçim
Bir geliştirici, web uygulamasının **istek yanıt sürelerini, hata oranını ve veritabanı bağımlılık çağrılarını** izlemek istiyor. Hangi servis **en uygundur**?

- A) Application Insights
- B) Azure Advisor
- C) Resource Health
- D) Azure Arc

<details><summary>Cevap</summary>

**A.** Application Insights, Azure Monitor'un uygulama performans izleme (APM) özelliğidir: istekler, hatalar, bağımlılıklar, exception'lar, availability test'ler.
</details>

#### Soru 7: Tek seçim
Birden çok kaynaktan toplanan log verisini **sorgulayıp analiz etmek** için Azure Monitor'un hangi aracı kullanılır?

- A) Metrics explorer
- B) Log Analytics
- C) Azure Status
- D) Advisor score

<details><summary>Cevap</summary>

**B.** Log Analytics, log'ları bir workspace'te toplar ve KQL (Kusto Query Language) ile sorgulatır. Metrics explorer sayısal metriklerin grafiği içindir.
</details>

#### Soru 8: Senaryo
Bir VM'in CPU kullanımı 10 dakika boyunca %85'in üzerinde kalırsa operasyon ekibine **SMS** gönderilmesi gerekiyor. Hangi iki bileşen birlikte kullanılmalıdır?

- A) Azure Advisor ve Azure Policy
- B) Azure Monitor metric alert ve action group
- C) Service Health ve Azure Status
- D) Log Analytics ve resource lock

<details><summary>Cevap</summary>

**B.** Uyarı kuralı (metric alert) koşulu izler; bildirimi (SMS, e-posta, webhook…) **action group** gönderir.
</details>

#### Soru 9: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Monitor, Azure dışındaki (on-prem, diğer bulutlar) kaynaklardan da veri toplayabilir. | ☐ | ☐ |
| b) Application Insights, Azure Monitor'un bir özelliğidir. | ☐ | ☐ |
| c) Azure Monitor alerts yalnızca e-posta gönderebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Action group'lar e-posta, SMS, push, sesli arama, webhook, Logic App, Azure Function, Automation runbook gibi eylemler tetikleyebilir.
</details>

#### Soru 10: Tek seçim
Bir denetçi, geçen hafta bir resource group'taki bir storage account'u **kimin sildiğini** öğrenmek istiyor. Nereye bakmalıdır?

- A) Azure Advisor
- B) Azure Activity Log
- C) Azure Status
- D) Application Insights

<details><summary>Cevap</summary>

**B.** Activity Log, subscription düzeyindeki yönetim işlemlerini (kim, ne zaman, hangi kaynağı oluşturdu/değiştirdi/sildi) kaydeder ve Azure Monitor'un bir veri kaynağıdır.
</details>

#### Soru 11: Tek seçim
Bir ekip, farklı kaynaklardan metrik grafiklerini ve log sorgu sonuçlarını **tek bir ekranda** toplayıp diğer ekip üyeleriyle **paylaşmak** istiyor. Ne kullanmalıdır?

- A) Azure dashboard
- B) Azure Advisor
- C) Resource lock
- D) Azure Status

<details><summary>Cevap</summary>

**A.** Azure dashboard'ları portalda kutucuklardan oluşan, kaydedilip RBAC ile paylaşılabilen panolardır.
</details>

#### Soru 12: Tek seçim
Metrik, log sorgusu, parametre ve açıklama metnini birleştiren **etkileşimli raporlar** oluşturmak için Azure Monitor'un hangi özelliği kullanılır?

- A) Workbooks
- B) Action groups
- C) Service Health
- D) Advisor score

<details><summary>Cevap</summary>

**A.** Workbooks; parametreli, etkileşimli, paylaşılabilir raporlardır ve galeride hazır şablonları vardır.
</details>

#### Soru 13: Senaryo
Kullanıcılar uygulamanın yavaşladığını bildiriyor. Ekip önce **sorunun Azure platformundan mı yoksa kendi uygulamalarından mı** kaynaklandığını hızla ayırt etmek istiyor. İlk hangi araca bakmalıdır?

- A) Azure Service Health
- B) Azure Advisor
- C) Azure Pricing Calculator
- D) Azure Policy

<details><summary>Cevap</summary>

**A.** Service Health (ve tek kaynak için Resource Health), kullandığın servis/bölgede Azure kaynaklı bir olay olup olmadığını gösterir. Olay yoksa sorun büyük olasılıkla uygulamadadır → Application Insights ve Log Analytics.
</details>

#### Soru 14: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Status sayfası, yalnızca oturum açmış kullanıcıların kendi kaynaklarını gösterir. | ☐ | ☐ |
| b) Resource Health, tek bir kaynağın geçmiş sağlık durumunu da gösterir. | ☐ | ☐ |
| c) Service Health, planlı bakım bildirimlerini içerir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Azure Status herkese açık ve geneldir; kişisel görünüm Service Health'tir.
</details>

#### Soru 15: Tek seçim
Bir web sitesinin dünyanın farklı noktalarından **düzenli olarak erişilebilir olup olmadığını** test etmek ve erişilemezse uyarı almak isteniyor. Hangi özellik **en uygundur**?

- A) Application Insights availability test'leri
- B) Azure Advisor
- C) Resource lock
- D) Azure DNS

<details><summary>Cevap</summary>

**A.** Availability test'ler uygulamaya belirli aralıklarla farklı konumlardan istek atar ve başarısızlıkta uyarı üretebilir.
</details>

#### Soru 16: Tek seçim
Azure Advisor ile Azure Policy arasındaki temel fark nedir?

- A) Advisor kuralları zorlar, Policy öneri verir
- B) Advisor öneri verir, Policy kuralları değerlendirip zorlar
- C) İkisi de yalnızca maliyet içindir
- D) İkisi de yalnızca güvenlik içindir

<details><summary>Cevap</summary>

**B.** Advisor iyi uygulama **önerileri** üretir; Azure Policy kaynakların kurallara uygunluğunu **değerlendirir ve zorlar** (ör. Deny) (→ C1.12).
</details>

#### Soru 17: Eşleştirme
Her ihtiyacı doğru araçla eşleştirin: **Advisor · Application Insights · Log Analytics · Service Health**

1. "Tek instance VM'lerimi availability zone'lara yaymam önerilsin."
2. "Uygulamamdaki exception'ları ve yavaş sayfaları göreyim."
3. "Bölgemdeki Azure kesintisinin kök neden raporunu okuyayım."
4. "Son 24 saatteki hata log'larını KQL ile süzeyim."

<details><summary>Cevap</summary>

**1 → Advisor** (Reliability önerisi) · **2 → Application Insights** · **3 → Service Health** (RCA) · **4 → Log Analytics**.
</details>

#### Soru 18: Çoklu seçim (iki doğru cevap)
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


---

# BÖLÜM 4: Genişletilmiş ve Bağlamsal Konular (Sınavda Seçeneklerde ve Senaryolarda Çıkan Servisler)

---

## 4.1 Veri ve Analitik Servisleri

> **Sınav Alanı:** Describe Azure architecture and services — genişletilmiş kapsam · **Konu Kodu:** C4.1

### 1. Ne?

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

### 2. Neden?

- **Sınav:** Senaryo sorularının seçeneklerinde sürekli geçer: "Mevcut SQL Server'ı **en az değişiklikle** taşı" → Managed Instance; "yeni uygulama, minimum yönetim" → SQL Database; "küresel dağıtım, tek haneli ms" → Cosmos DB; "ETL orkestrasyonu" → Data Factory. Ayrıca "VM'e kurulu SQL = PaaS mi?" tuzağı klasiktir.
- **Gerçek işte:** Yanlış veri servisi, ya aşırı maliyet (her şeye Cosmos DB) ya da mimari duvar (global yazma ihtiyacı olan uygulamayı tek region'lık SQL'e koymak) demektir. Veri modeli seçimi sonradan **en pahalı** değişen kararlardan biridir.
- **Bağlam:** EF Core ve SQL derinliği → M6/M8; Cosmos SDK, change feed, partition key tasarımı → C2.4–C2.7; geri yükleme ve felaket kurtarma → C1.19.

---

### 3. Nasıl?

#### 3.1 Azure SQL ailesi ⭐

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

#### 3.2 Azure Cosmos DB ⭐ (NoSQL)

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

#### 3.3 Önbellek ve Storage'ın NoSQL benzeri servisleri

| Servis | Ne? | Not |
|---|---|---|
| **Azure Managed Redis** | Yönetilen **in-memory** önbellek/veri yapıları (Redis) | Oturum, sık okunan veri, rate limiting. **Güncel durum:** eski *Azure Cache for Redis* (Basic/Standard/Premium) emekliye ayrılıyor — yeni oluşturma kısıtlandı, **30 Eylül 2028**'de tamamen emekli; Enterprise katmanları **Mart 2027** civarı. Önerilen hedef **Azure Managed Redis**. Tarihleri resmî duyurudan doğrula |
| **Table storage** | Storage account içinde basit anahtar/değer-özellik deposu | Ucuz, basit; zengin sorgu/global dağıtım yok (premium ihtiyaç → Cosmos DB Table API) |
| **Blob storage** | Yapılandırılmamış nesne verisi (→ C1.8) | Veritabanı değildir; ama **Data Lake**'in temelidir |

#### 3.4 İlişkisel ↔ NoSQL karar tablosu ⭐

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

#### 3.5 Analitik ve büyük veri

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

> Ayrı bir hat: **Microsoft Purview** veri yönetişimi ve sınıflandırma (→ C1.12), **Azure Machine Learning ve AI servisleri** (→ C1.20).

#### 3.6 Taşıma ve güvenlik özeti

- **Taşıma:** **Azure Database Migration Service (DMS)** SQL Server/PostgreSQL/MySQL gibi veritabanlarını Azure'a **çevrimiçi (minimum kesinti)** veya **çevrimdışı** taşır; **Azure Migrate** keşif/değerlendirme/taşıma merkezidir (→ C1.19).
- **Şifreleme:** durağan veri — SQL'de **TDE** (varsayılan açık), Cosmos DB ve Storage'da platform yönetimli anahtarlarla varsayılan şifreleme (isteğe bağlı müşteri yönetimli anahtar → C1.18); aktarımda — bağlantıda **TLS**; sütun düzeyi **Always Encrypted** *(bağlam)*.
- **Kimlik doğrulama:** parola yerine **Microsoft Entra** kimlik doğrulaması ve **managed identity** (→ C1.9, C2.11).
- **Ağ:** SQL Database/Cosmos DB için **private endpoint** + public erişimi kapatma (→ C1.7); firewall kuralları.
- **Dayanıklılık:** **active geo-replication / failover group** (SQL), Cosmos DB'de çok region'lı hesap; yedekler — redundancy yedek yerine geçmez (→ C1.8, C1.19).

#### 3.7 Pratik — ücretsiz/ucuz SQL Database, Cosmos DB free tier

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
| 2–3 | `compute-model Serverless`, `autoPauseDelay: 60` (dakika cinsinden boşta kalma süresi) | Serverless: boşta duraklar, compute ücreti kesilir |
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

### 4. Gerçek dünya senaryosu

Bir perakende şirketi e-ticaret platformunu modernleştiriyor:

1. **Mevcut ERP** veritabanı şirket içi SQL Server (SQL Agent job'ları var) → **Database Migration Service** ile **SQL Managed Instance**'a taşınır (kod değişikliği yok, VNet içinde, private).
2. **Yeni sepet/oturum servisi**, bölgeler arası düşük gecikme ister → **Cosmos DB** (Avrupa ve ABD'de çok region'da yazma, Session tutarlılığı) + sık okunan ürün verisi için **Azure Managed Redis**.
3. **Ürün kataloğu** ilişkisel kalıyor; yeni mikroservisler için **Azure SQL Database** (serverless, geliştirme ortamında auto-pause).
4. **Analitik:** **Data Factory** her gece SQL'ten, Cosmos'tan ve SaaS CRM'den veriyi **Data Lake Storage Gen2**'ye çeker; **Fabric/Synapse** ile dönüştürülür; **Power BI** ile satış panoları.
5. **Mağaza kasalarından telemetri** → **Event Hubs → Stream Analytics** ile anlık stok uyarıları.
6. **Güvenlik:** hepsi için **private endpoint**, public erişim kapalı; kimlik doğrulama **managed identity**; TDE ve durağan şifreleme varsayılan; **Defender for SQL/Cosmos DB** (→ C1.10).

---

### 5. Sınav perspektifi

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

### 6. Yaygın hatalar

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

### 7. Performans & trade-off

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

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Azure SQL documentation](https://learn.microsoft.com/en-us/azure/azure-sql/) · [Deploy Azure SQL Database for free](https://learn.microsoft.com/en-us/azure/azure-sql/database/free-offer)
- [Azure Cosmos DB introduction](https://learn.microsoft.com/en-us/azure/cosmos-db/introduction) · [Consistency levels](https://learn.microsoft.com/en-us/azure/cosmos-db/consistency-levels) · [Free tier](https://learn.microsoft.com/en-us/azure/cosmos-db/free-tier)
- [Azure Managed Redis overview](https://learn.microsoft.com/en-us/azure/redis/overview) · [Azure Cache for Redis retirement FAQ](https://learn.microsoft.com/en-us/azure/redis/retirement-faq)
- [What is Microsoft Fabric?](https://learn.microsoft.com/en-us/fabric/fundamentals/microsoft-fabric-overview) · [Azure Synapse Analytics](https://learn.microsoft.com/en-us/azure/synapse-analytics/overview-what-is)
- [What is Azure Data Factory?](https://learn.microsoft.com/en-us/azure/data-factory/introduction) · [Data Lake Storage Gen2](https://learn.microsoft.com/en-us/azure/storage/blobs/data-lake-storage-introduction)
- [Azure Database Migration Service](https://learn.microsoft.com/en-us/azure/dms/dms-overview)
- [`az sql db create`](https://learn.microsoft.com/en-us/cli/azure/sql/db) · [`az cosmosdb create`](https://learn.microsoft.com/en-us/cli/azure/cosmosdb)


### 📝 Bölüm 4.1 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir şirket, şirket içindeki SQL Server'ını (SQL Agent job'ları ve cross-database sorguları kullanıyor) **en az kod/yapılandırma değişikliğiyle**, yine de **yönetilen (PaaS)** bir servise taşımak istiyor. Hangisi **en uygundur**?

- A) Azure SQL Database (single database)
- B) Azure SQL Managed Instance
- C) SQL Server on Azure VM
- D) Azure Table storage

<details><summary>Cevap</summary>

**B.** Managed Instance, instance düzeyindeki özelliklerle (SQL Agent, cross-database sorgu…) neredeyse tam SQL Server uyumluluğu sunan PaaS servisidir. SQL Server on Azure VM de uyumludur ama **IaaS**'tır; yönetim yükünü sen taşırsın.
</details>

#### Soru 2: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure VM üzerine kurulan SQL Server bir PaaS çözümdür. | ☐ | ☐ |
| b) Azure SQL Database'de yama ve yedekleme platform tarafından yönetilir. | ☐ | ☐ |
| c) SQL Managed Instance'ın OS'una RDP ile bağlanılabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** VM'deki SQL Server IaaS'tır (OS ve SQL yamaları sende). SQL Database ve Managed Instance PaaS'tır; ikisinde de OS erişimi yoktur.
</details>

#### Soru 3: Tek seçim
Çok kiracılı (multi-tenant) bir SaaS uygulamasında yüzlerce küçük veritabanının kullanım zirveleri farklı saatlerde oluşuyor. Maliyeti düşürmek için **kaynakları veritabanları arasında paylaştırmak** istiyorsunuz. Ne kullanmalısınız?

- A) Her veritabanı için ayrı Business Critical katmanı
- B) SQL Database **elastic pool**
- C) Cosmos DB serverless
- D) SQL Server on Azure VM

<details><summary>Cevap</summary>

**B.** Elastic pool, aralarında kullanım zamanları farklı olan çok sayıda veritabanının paylaşımlı bir kaynak havuzunu kullanmasını sağlar.
</details>

#### Soru 4: Tek seçim
Bir geliştirme veritabanı günde yalnızca birkaç saat kullanılıyor; boşta olduğunda **compute ücretinin kesilmesi** isteniyor. Hangi özellik bunu sağlar?

- A) Azure SQL Database **serverless compute tier** (auto-pause)
- B) DTU modelinde Premium katman
- C) Geo-replication
- D) Elastic pool'a eklemek

<details><summary>Cevap</summary>

**A.** Serverless katmanı, boşta kalınca veritabanını duraklatır; duraklamışken yalnızca depolama ücretlenir. Bedeli: duraklamadan uyanırken ilk istekte gecikmedir.
</details>

#### Soru 5: Tek seçim
Küresel bir oyunun oyuncu profilleri **birden çok region'da yazılabilir**, **esnek şemalı** ve **tek haneli milisaniye gecikmeli** olmalıdır. Hangi servis **en uygundur**?

- A) Azure SQL Database
- B) Azure Cosmos DB
- C) Azure Table storage
- D) Azure Data Lake Storage Gen2

<details><summary>Cevap</summary>

**B.** Cosmos DB yerleşik küresel dağıtım, çok region'da yazma, esnek şema ve SLA ile desteklenen düşük gecikme sunar. Table storage basit bir anahtar-değer deposudur; küresel dağıtım/SLA'lı gecikme sağlamaz.
</details>

#### Soru 6: Sıralama
Cosmos DB tutarlılık düzeylerini **en güçlüden en zayıfa** sıralayın: **Eventual · Strong · Session · Bounded staleness · Consistent prefix**

<details><summary>Cevap</summary>

**Strong → Bounded staleness → Session → Consistent prefix → Eventual.** Güçlü tutarlılık gecikme ve erişilebilirlik pahasınadır; **Session** varsayılandır ve çoğu uygulama için dengelidir.
</details>

#### Soru 7: Tek seçim
Cosmos DB'de **RU/s** neyi ifade eder?

- A) Replica Unit per second — region başına replika sayısı
- B) Request Unit per second — işlemlerin CPU/bellek/IO maliyetini ortak bir birimle gösteren throughput
- C) Read Update per second — yalnızca yazma sayısı
- D) Retention Unit per second — verinin saklanma süresi

<details><summary>Cevap</summary>

**B.** RU, bir işlemin maliyetinin soyut birimidir; throughput **RU/s** olarak sağlanır (provisioned, autoscale veya serverless modelle).
</details>

#### Soru 8: Çoklu seçim (iki doğru cevap)
Bir ekip, anlık Event Hubs akışını **gerçek zamanlı analiz edip** sonucu Power BI'a göndermek istiyor. Hangi **iki** servis bu hatta doğal olarak yer alır?

- A) Azure Event Hubs (akışı almak için)
- B) Azure Stream Analytics (akışı sorgulamak için)
- C) Azure HDInsight, yalnızca depolama için
- D) Azure Database Migration Service

<details><summary>Cevap</summary>

**A ve B.** Event Hubs yüksek hacimli olayları **alır**, Stream Analytics akan veri üzerinde gerçek zamanlı sorgu çalıştırır; çıktı Power BI'a gidebilir. DMS veritabanı taşıma aracıdır.
</details>

#### Soru 9: Eşleştirme
Her işi servisle eşleştirin: **Data Factory · Data Lake Storage Gen2 · Power BI · Azure Databricks**

1. Birden çok kaynaktan veriyi çekip dönüştüren zamanlanmış pipeline'lar
2. Ham ve yapılandırılmamış analitik verinin hiyerarşik namespace'li deposu
3. Raporlar ve dashboard'lar (iş zekâsı)
4. Apache Spark tabanlı, notebook ile ortak çalışılan analitik/ML platformu

<details><summary>Cevap</summary>

**1 → Data Factory · 2 → Data Lake Storage Gen2 · 3 → Power BI · 4 → Azure Databricks.**
</details>

#### Soru 10: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Microsoft Fabric, veri mühendisliği, depo ve Power BI'ı birleştiren bir SaaS analitik platformudur. | ☐ | ☐ |
| b) Azure Data Factory, verinin kalıcı olarak saklandığı asıl depolama servisidir. | ☐ | ☐ |
| c) Data Lake Storage Gen2, Blob Storage üzerine hiyerarşik namespace ekler. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Data Factory veriyi **taşır/dönüştürür**, depolamaz; Data Lake Storage Gen2 = Blob + hiyerarşik namespace.
</details>

#### Soru 11: Tek seçim
OLTP ve OLAP için hangi eşleme **doğrudur**?

- A) OLTP: büyük veri kümeleri üzerinde karmaşık toplama; OLAP: çok sayıda küçük işlem
- B) OLTP: çok sayıda küçük, hızlı işlem; OLAP: büyük veri üzerinde analitik sorgular
- C) İkisi de yalnızca NoSQL veritabanlarına aittir
- D) OLTP yalnızca streaming, OLAP yalnızca batch demektir

<details><summary>Cevap</summary>

**B.** OLTP: sipariş ver, bakiye güncelle gibi işlemsel iş yükü. OLAP: geçmiş veriyi toplayıp analiz etme. Analitik yük operasyonel veritabanından ayrılır.
</details>

#### Soru 12: Senaryo
Bir şirket sık okunan ürün verilerini, veritabanı yükünü azaltmak için **bellek içi** bir katmanda tutmak istiyor. Hangi servis uygundur?

- A) Azure Managed Redis
- B) Azure Data Factory
- C) Azure Synapse Analytics
- D) Azure Blob Storage Archive tier

<details><summary>Cevap</summary>

**A.** Yönetilen Redis, bellek içi önbellek ve hızlı veri yapıları için kullanılır. (Eski *Azure Cache for Redis* emekli edilmektedir; yeni hedef Azure Managed Redis'tir — tarihleri resmî duyurudan doğrula.)
</details>

#### Soru 13: Senaryo
Bir ekip Azure SQL Database'e **parola** ile bağlanan bir App Service uygulaması çalıştırıyor. Güvenliği artırmak için **en iyi** adım hangisidir?

- A) Parolayı `appsettings.json`'a yazıp repoya koymak
- B) Managed identity ile Microsoft Entra kimlik doğrulamasına geçmek ve SQL için private endpoint kullanmak
- C) SQL firewall'unda tüm IP aralıklarına izin vermek
- D) TDE'yi kapatmak

<details><summary>Cevap</summary>

**B.** Parolasız (managed identity + Entra) erişim secret sızıntısını önler; private endpoint ağ yüzeyini kapatır. TDE varsayılan açıktır ve kapatmak güvenliği düşürür.
</details>

#### Soru 14: Tek seçim
Hangi ifade **yanlıştır**?

- A) Cosmos DB free tier, hesap başına ilk 1000 RU/s ve 25 GB'ı ücretsiz verir.
- B) Aboneliğe en fazla bir Cosmos DB free tier hesabı açılabilir.
- C) SQL Database serverless'ta duraklamış veritabanı için depolama ücretlenmez.
- D) Azure SQL Database'in ücretsiz teklifinde limit dolunca otomatik duraklama seçilebilir.

<details><summary>Cevap</summary>

**C.** Duraklamışken **compute** ücretlenmez ama **depolama** ücretlenmeye devam eder (ücretsiz teklifin depolama kotası dışında). Diğerleri doğrudur (güncel limitleri resmî sayfadan doğrula).
</details>

#### Soru 15: Senaryo
Bir perakende şirketi yeni bir mikroservis için ilişkisel veri, minimum yönetim ve kullanıma göre ölçeklenen bir veritabanı istiyor. Aşağıdakilerden hangi kombinasyon **en uygundur**?

- A) SQL Server on Azure VM + manuel yedek betikleri
- B) Azure SQL Database (serverless) + private endpoint
- C) Cosmos DB, yalnızca JOIN gerektiren raporlar için
- D) HDInsight Hadoop kümesi

<details><summary>Cevap</summary>

**B.** Yeni, ilişkisel, düşük yönetimli iş yükü için SQL Database; serverless ile ölçek/maliyet esnekliği; private endpoint ile ağ güvenliği. A yönetim yükü, C yanlış veri modeli, D yanlış iş türüdür.
</details>


---

## 4.2 Yük Dengeleme ve Trafik Dağıtımı

> **Sınav Alanı:** Describe Azure architecture and services — genişletilmiş kapsam · **Konu Kodu:** C4.2

### 1. Ne?

**Yük dengeleme (load balancing)**, gelen istekleri birden çok sunucu/instance arasında dağıtarak (a) tek sunucuyu aşırı yüklememek, (b) sağlıksız instance'ı devreden çıkarmak ve (c) kesintisiz hizmeti sağlamaktır. Azure'da dört ana servis vardır; hepsini **iki eksende** konumlandır:

```
                  Katman 4 (TCP/UDP)            Katman 7 (HTTP/HTTPS)
               ┌─────────────────────────┬──────────────────────────────┐
   Bölgesel    │  Azure Load Balancer    │  Application Gateway         │
               ├─────────────────────────┼──────────────────────────────┤
   Global      │  (Traffic Manager: DNS) │  Azure Front Door            │
               └─────────────────────────┴──────────────────────────────┘
        Traffic Manager = DNS tabanlı (trafiğin içinden geçmez): katman 4/7 ayrımına girmez, "global" ekseninde
```

| Servis | Katman | Kapsam | Tek cümle |
|---|---|---|---|
| **Azure Load Balancer** | L4 (TCP/UDP) | Bölgesel | Hızlı, düşük gecikmeli paket düzeyi dağıtım; HTTP'yi anlamaz |
| **Application Gateway** | L7 (HTTP/HTTPS) | Bölgesel | URL/host tabanlı yönlendirme, SSL termination, **WAF** |
| **Azure Front Door** | L7 (HTTP/HTTPS) | **Global** | Edge'de (POP) karşılayan global giriş: hızlandırma, CDN, WAF, failover |
| **Traffic Manager** | DNS | **Global** | DNS yanıtını değiştirerek istemciyi bir endpoint'e yönlendirir |

> *(bağlam)* Azure Load Balancer'ın **cross-region (global) tier**'ı da vardır (L4, anycast). Sınav temelinde "Load Balancer = bölgesel, L4" cevabı doğrudur; global L4 ihtiyacı çok nadirdir.

Bu konunun kodu yoktur; pratik kısım `az` CLI laboratuvarıdır (3.7).

---

### 2. Neden?

- **Sınav:** "Hangi servis?" senaryoları (WAF/SSL offload/URL routing → Application Gateway; global HTTP + hızlandırma → Front Door; DNS tabanlı → Traffic Manager; TCP/UDP → Load Balancer). Ayrım **iki anahtar**la yapılır: **bölgesel/global** ve **L4/L7**.
- **Gerçek işte:** Tek instance = tek arıza noktası. Yük dengeleyici + availability zone'lar (→ C1.4) uygulama içi yüksek erişilebilirliği, global servisler **region arızasına** karşı dayanıklılığı sağlar. SLA hesabında (→ C1.2) trafiği yönlendiren katman da **seri bir bileşendir**.
- **Bağlam:** Mimari kararlar (hub-spoke, multi-region aktif/aktif) → S8.3; ASP.NET Core arkasında proxy başlıkları (`X-Forwarded-*`), health endpoint'leri (`/health`) → M12.

---

### 3. Nasıl?

#### 3.1 Azure Load Balancer (L4, bölgesel) ⭐

Gelen **TCP/UDP** akışlarını 5-tuple hash'ine (kaynak IP, kaynak port, hedef IP, hedef port, protokol) göre backend havuzundaki sağlıklı instance'lara dağıtır. İçeriği (HTTP başlığı, URL) **görmez**; çok hızlıdır.

| Kavram | Açıklama |
|---|---|
| **Frontend IP** | İstemcilerin bağlandığı adres. **Public LB:** public IP; **Internal LB:** VNet içi özel IP (ör. web katmanından veri katmanına) |
| **Backend pool** | Trafiği alan VM'ler / VM Scale Set instance'ları |
| **Health probe** | Instance sağlıklı mı? TCP / HTTP / HTTPS ile periyodik yoklama; **sağlıksız instance trafikten çıkarılır** |
| **Load-balancing rule** | `frontend:port → backend:port` eşlemesi (ör. 80 → 80) |
| **Inbound NAT rule** | Belirli bir porttan belirli **tek** bir VM'e yönlendirme (ör. `:50001` → VM1:22) — dağıtım değil, port yönlendirme |
| **Outbound rule** | Backend'in internete çıkış (SNAT) yönetimi |
| **Session persistence** | Varsayılan 5-tuple hash; istenirse **source IP affinity** (aynı istemci aynı instance'a) |
| **Zone-redundant frontend** | Frontend IP, tüm availability zone'larda yaşar — bir zone düşse de ayakta (→ C1.4) |

**Standard ↔ Basic:**

| | **Standard** | **Basic** |
|---|---|---|
| Durum | Güncel, önerilen | **Emekliye ayrıldı** (30 Eylül 2025); yeni dağıtım yapılamaz *(durum notları sayfalarda farklı ifade edilebilir — doğrula)* |
| Availability zone | Zone-redundant / zonal | Yok |
| SLA | Var (%99,99 — backend'de ≥ 2 sağlıklı instance şartıyla) | Yok |
| Güvenlik | Varsayılan **kapalı** (inbound için **NSG ile izin** gerekir) | Varsayılan açık |
| Probe | TCP, HTTP, HTTPS | TCP, HTTP |

> Sınav tuzağı: Standard Load Balancer **secure by default**'tur — NSG ile açıkça izin vermezsen inbound trafik geçmez.

*(bağlam)* **Gateway Load Balancer**: ağ sanal cihazlarını (NVA — firewall, IDS) şeffaf biçimde zincire eklemek için.

#### 3.2 Application Gateway (L7, bölgesel) ⭐

HTTP/HTTPS trafiği için **web trafiği yük dengeleyici** — isteğin **içeriğine** (URL yolu, host adı, başlık, cookie) göre karar verir.

| Yetenek | Ne yapar? |
|---|---|
| **URL path-based routing** | `/images/*` → görsel havuzu, `/api/*` → API havuzu |
| **Multi-site / host-based routing** | `a.contoso.com` ve `b.contoso.com` aynı gateway'de, farklı backend'lere |
| **SSL/TLS termination (offload)** | TLS'i gateway'de sonlandır; backend'e HTTP (veya end-to-end TLS ile yeniden şifrele) — backend CPU'sunu rahatlatır |
| **Cookie-based session affinity** | Aynı kullanıcıyı aynı backend'e yapıştır |
| **Web Application Firewall (WAF)** | SQL injection, XSS gibi **OWASP** saldırılarına karşı koruma |
| **Header/URL rewrite, redirect** | İstek/yanıt başlıklarını değiştir, HTTP→HTTPS yönlendir |
| **Autoscale + zone redundancy** | **v2** SKU ile (Standard_v2 / WAF_v2) |
| Backend türleri | VM, VMSS, App Service, IP/FQDN (hibrit) |

- **V1 emekli oldu:** Application Gateway **V1**, **28 Nisan 2026** itibarıyla emekliye ayrıldı; yeni/mevcut dağıtımlar **V2**'ye taşınmalıdır. Sınav SKU adlarını sormaz.
- Frontend **public veya private IP** olabilir (internal uygulama geçidi).
- *(bağlam)* **Application Gateway for Containers**: AKS için L7 yük dengeleme/ingress.
- WAF, **Front Door** ve Application Gateway'e **eklenir**; bağımsız bir "WAF servisi" değil, bu ikisinin özelliğidir (→ [C1.18](#43-ağ-güvenliği-bastion-ve-key-vault)).

#### 3.3 Azure Front Door (L7, global) ⭐

Dünya çapındaki **edge location'larda (POP)** (Microsoft'un global ağında, **anycast**) istemciyi karşılayan, HTTP/HTTPS için **global giriş noktası**. İstemci en yakın POP'a bağlanır; oradan Microsoft omurgasıyla origin'e (backend'e) taşınır.

| Yetenek | Ne yapar? |
|---|---|
| **Global anycast + hızlandırma** | TCP/TLS el sıkışması istemciye yakın POP'ta; origin'e optimize yol — dinamik içerikte de gecikmeyi düşürür |
| **Global yük dengeleme + failover** | Origin'leri (farklı region'lar) health probe ile izler; sağlıksızı otomatik devreden çıkarır — **DNS TTL beklemez** (trafik Front Door'dan geçtiği için anında) |
| **CDN / önbellekleme** | Statik içeriği edge'de önbellekler |
| **WAF** | Edge'de saldırıyı durdurur (bölgesel WAF'tan **önce**) |
| **TLS, özel domain, yönlendirme kuralları** | Yönetilen sertifika, URL rewrite/redirect, rules engine |
| **Origin'ler** | App Service, Storage, Application Gateway, public IP, herhangi bir public host |

- **Katmanlar:** **Standard** ve **Premium**. Premium; yönetilen (managed) WAF kural setleri, bot koruması ve **Private Link** ile özel origin bağlantısı gibi ek güvenlik yetenekleri içerir *(farkları güncel tablodan doğrula)*. **Front Door (classic)** **31 Mart 2027**'de emekli olacak; yeni kurulumlar Standard/Premium'dur.
- **Global servistir:** region seçmezsin (→ C1.4).

#### 3.4 Traffic Manager (DNS tabanlı, global) ⭐

Traffic Manager **trafiğin içinden geçmez**. İstemci `app.contoso.com` adını çözmek istediğinde Traffic Manager, **yönlendirme yöntemine** ve endpoint sağlığına göre uygun endpoint'in adresini **DNS cevabı** olarak döndürür; istemci sonra o endpoint'e **doğrudan** bağlanır. Her protokolle çalışır (DNS seviyesinde olduğu için HTTP olmak zorunda değil).

| Yöntem | Davranış | Tipik kullanım |
|---|---|---|
| **Priority** | Her zaman 1. öncelikli endpoint; sağlıksızsa 2.'ye | **Aktif/pasif failover** |
| **Weighted** | Ağırlıklara göre dağıt (ör. %90 / %10) | Canary, kademeli geçiş |
| **Performance** | İstemciye **gecikme olarak en yakın** endpoint | Çok region'lı kullanıcı deneyimi |
| **Geographic** | Kullanıcının **coğrafi konumuna** göre sabit endpoint | Veri egemenliği, bölgeye özel içerik |
| **Multivalue** | Birden çok sağlıklı IP'yi **tek cevapta** döndür | İstemci tarafı yeniden deneme |
| **Subnet** | İstemci **IP aralığına** göre endpoint | Kurumsal ağ / ISP'ye özel yönlendirme |

- **Endpoint türleri:** Azure endpoint'leri, **external** endpoint'ler (Azure dışı), **nested** (profil içinde profil).
- **Sağlık izleme:** HTTP/HTTPS/TCP probe'ları; sağlıksız endpoint cevaptan çıkar.
- **Sınırlar:** **DNS önbellekleme** (TTL) nedeniyle failover **saniyelerle değil, TTL + probe süresiyle** gerçekleşir; trafik Traffic Manager'dan geçmediği için **WAF, TLS sonlandırma, önbellek yoktur**.

**Traffic Manager ↔ Front Door** ⭐

| | Traffic Manager | Front Door |
|---|---|---|
| Mekanizma | **DNS** cevabı (yönlendirir, geçmez) | **Reverse proxy** — trafik içinden geçer |
| Protokol | Herhangi (DNS üzerinden) | HTTP/HTTPS |
| Failover hızı | DNS TTL'ye bağlı (dakikalar) | Anında (bağlantı Front Door'da) |
| WAF / CDN / TLS offload | **Yok** | **Var** |
| İstemci neyi görür? | Endpoint'in kendi IP'si | Front Door'un edge adresi |
| Maliyet | Çok düşük | Daha yüksek (özellikler) |
| Ne zaman? | HTTP-dışı protokol, basit DNS failover, ucuz çözüm | Global web uygulaması, hızlandırma, WAF |

#### 3.5 Karar ağacı ve kombinasyon örüntüleri ⭐

```
Trafik HTTP/HTTPS mi?
 ├─ Hayır (TCP/UDP/diğer)
 │    ├─ Tek region'da dağıt?               → Azure Load Balancer (public veya internal)
 │    └─ Global/DNS failover?               → Traffic Manager
 └─ Evet (web trafiği)
      ├─ Global kullanıcı, hızlandırma/CDN/anında failover?   → Azure Front Door
      ├─ Tek region, URL/host routing, WAF, SSL offload?      → Application Gateway
      └─ DNS tabanlı basit yönlendirme/ucuz?                  → Traffic Manager
Yalnızca iç (internal) trafik mi?            → Internal Load Balancer / özel IP'li Application Gateway
```

**Kombinasyon örüntüleri** (çoğu gerçek mimari birden çok katmanı birleştirir):

| Örüntü | Akış | Neden? |
|---|---|---|
| **Front Door + Application Gateway + Load Balancer** | İnternet → **Front Door** (global, WAF, CDN) → **Application Gateway** (bölgesel L7) → **Load Balancer** (VM katmanı) | Global giriş + bölgesel L7 yönlendirme + VM katmanı dağıtımı |
| **Traffic Manager + Application Gateway** | DNS → Traffic Manager (performance/priority) → bölgesel Application Gateway'ler | HTTP-dışı veya mevcut DNS tabanlı çok region'lı mimari |
| **Application Gateway + Internal LB** | Web katmanı (L7) → uygulama katmanı (L4 internal) | 3 katmanlı uygulamada her katmanın kendi dağıtıcısı |
| **Front Door + App Service (çok region)** | Aynı uygulama iki region'da; origin'ler Front Door'da | Region arızasında otomatik geçiş |

- **Health probe mantığı:** her servis backend'e düzenli probe gönderir; **belirli sayıda ardışık başarısızlıkta** instance havuzdan çıkar, başarılı olunca geri girer. Uygulamada `/health` gibi **gerçek bağımlılıkları** (DB) kontrol eden bir endpoint, "port açık" kontrolünden daha doğru sinyaldir. Probe'u **WAF/NSG kuralları engellememeli** (`AzureLoadBalancer` service tag'i → C1.18).
- **Availability zone ile birlikte (→ C1.4):** bölgesel L4/L7 servisleri **zone-redundant** yapılandırılır; backend instance'ları farklı zone'lara yayılır — yük dengeleyici *zone* arızasına, Front Door/Traffic Manager *region* arızasına karşı korur.

#### 3.6 Bağlam: Azure CDN, Bastion ve 2026 notları

- **Azure CDN:** coğrafi olarak dağıtık edge sunucularında **statik içeriği** (görsel, JS, video) önbelleğe alarak gecikmeyi ve origin yükünü azaltır. **2026 durumu:** Edgio (eski Verizon) tabanlı Azure CDN **2025 başında kapatıldı**; **Azure CDN Standard from Microsoft (classic)** yeni profil/domain'e kapalı, **30 Eylül 2027**'de emekli olacak — Microsoft'un yönlendirdiği yer **Azure Front Door Standard/Premium**'dur. *(tarihleri resmî sayfalardan doğrula)*
- **Azure Bastion:** yük dengeleme değil, **yönetim erişimi** servisidir (public IP'siz VM'e portaldan RDP/SSH) → [C1.18](#43-ağ-güvenliği-bastion-ve-key-vault).
- **2026 yeniden markalama / emeklilik özeti** *(durum tarihleri değişebilir — "What's new / retirement" sayfalarını kontrol et)*:

| Ürün | Durum |
|---|---|
| Basic Load Balancer / Basic Public IP | Emekli (30 Eylül 2025) → Standard |
| Application Gateway V1 | Emekli (28 Nisan 2026) → V2 |
| Front Door (classic) | 31 Mart 2027'de emekli → Standard/Premium |
| Azure CDN Standard from Microsoft (classic) | 30 Eylül 2027'de emekli → Front Door |
| Azure CDN from Edgio | Kapatıldı (2025) → Front Door |

#### 3.7 Pratik — Traffic Manager ile DNS tabanlı yönlendirme ve Standard Load Balancer

> Bütçe alarmın kurulu olsun (→ C1.1). **Traffic Manager**: profil ve endpoint başına çok küçük ücret + DNS sorgusu başına ücret — laboratuvar için sentim düzeyinde. **Standard Load Balancer + Standard public IP** saatlik ücretlenir (küçük, ama sürekli); VM kurmuyoruz, yalnızca kaynakların nasıl tanımlandığını görüyoruz — **hemen sil**. **Front Door, Application Gateway ve WAF kurmuyoruz**: Standard/Premium profil ve gateway'in sabit aylık/saatlik maliyeti yüksektir (WAF_v2 ve Premium özellikle); yalnızca teoride.

```bash
az login
RG=rg-az900-c117; LOC=westeurope
az group create --name $RG --location $LOC

# 1) Traffic Manager profile — Priority routing, short TTL so changes show quickly
DNSNAME=tm-az900-$RANDOM
az network traffic-manager profile create --resource-group $RG --name tm-demo \
  --routing-method Priority --unique-dns-name $DNSNAME --ttl 30 \
  --protocol HTTPS --port 443 --path "/" --status-code-ranges 200-399   # redirects count as healthy

# 2) Two external endpoints (stand-ins for "primary" and "secondary" regions)
az network traffic-manager endpoint create --resource-group $RG --profile-name tm-demo \
  --name ep-primary   --type externalEndpoints --target www.microsoft.com --priority 1
az network traffic-manager endpoint create --resource-group $RG --profile-name tm-demo \
  --name ep-secondary --type externalEndpoints --target learn.microsoft.com --priority 2

# 3) Which endpoint does DNS return? (Priority: always the primary while it is healthy)
nslookup $DNSNAME.trafficmanager.net

# 4) Simulate a primary failure: disable the primary endpoint, query again
az network traffic-manager endpoint update --resource-group $RG --profile-name tm-demo \
  --name ep-primary --type externalEndpoints --endpoint-status Disabled
sleep 40; nslookup $DNSNAME.trafficmanager.net        # the CNAME now points to the secondary target

# 5) Switch the routing method — no client change needed, only the DNS answer changes
az network traffic-manager profile update --resource-group $RG --name tm-demo --routing-method Weighted
az network traffic-manager endpoint update --resource-group $RG --profile-name tm-demo \
  --name ep-primary --type externalEndpoints --endpoint-status Enabled --weight 90
az network traffic-manager endpoint update --resource-group $RG --profile-name tm-demo \
  --name ep-secondary --type externalEndpoints --weight 10
for i in 1 2 3 4 5; do nslookup $DNSNAME.trafficmanager.net | grep -i "canonical"; sleep 31; done   # mostly primary, occasionally secondary

# 6) Standard Load Balancer definition: public IP, probe, rule (backend pool stays empty — no VMs, no traffic)
az network public-ip create --resource-group $RG --name pip-lb --sku Standard --allocation-method Static
az network lb create --resource-group $RG --name lb-demo --sku Standard \
  --public-ip-address pip-lb --frontend-ip-name fe-web --backend-pool-name be-web
az network lb probe create --resource-group $RG --lb-name lb-demo --name probe-http \
  --protocol Http --port 80 --path /health
az network lb rule create --resource-group $RG --lb-name lb-demo --name rule-http \
  --protocol Tcp --frontend-port 80 --backend-port 80 \
  --frontend-ip-name fe-web --backend-pool-name be-web --probe-name probe-http
az network lb show --resource-group $RG --name lb-demo \
  --query "{sku:sku.name, rules:loadBalancingRules[].name, probes:probes[].name, pools:backendAddressPools[].name}"
#   Real use: add VMs/NICs to be-web, and open port 80 in the NSG — Standard LB is closed by default.

# 7) Cleanup — mandatory (the Standard public IP and LB bill per hour)
az group delete --name $RG --yes --no-wait
```

<details><summary>Beklenen gözlemler ve açıklamaları</summary>

| Adım | Gözlem | Kavram |
|---|---|---|
| 3 | `nslookup` çıktısında `$DNSNAME.trafficmanager.net` bir **CNAME** ile `www.microsoft.com` (edge adresi zinciri) gösterir | Traffic Manager yalnızca **DNS cevabı** döndürür; trafiğin içinden geçmez |
| 4 | Primary devre dışı → CNAME `learn.microsoft.com`'a döner (TTL ~30 sn + birkaç sn) | Priority = failover; **DNS önbelleği** yüzünden anlık değil |
| 5 | Weighted'ta çoğu sorgu primary'yi, bazıları secondary'yi verir | Ağırlıklı dağıtım; yöntem değişince istemci değişmez |
| 6 | `sku: Standard`, kural/probe/havuz adları listelenir; backend yok | LB bileşenleri: frontend, backend pool, probe, kural |
| 6 | Probe yolu `/health` | Probe uygulama sağlığını yoklar (HTTP 200 bekler) |

`--status-code-ranges` parametresi eski CLI sürümlerinde farklı adlanabilir; hata alırsan `az network traffic-manager profile create --help` ile kontrol et. İlk sorguda yerel DNS önbelleği eski cevabı gösterebilir; birkaç saniye bekle. Hedef sitelere HTTPS probe gittiği için profilin `Online` görünmesi 1–2 dakika sürebilir.
</details>

---

### 4. Gerçek dünya senaryosu

Avrupa ve ABD'de müşterisi olan bir online bilet satış şirketi:

1. **Global giriş:** `tickets.contoso.com` → **Azure Front Door (Premium)**: POP'larda TLS sonlandırma, **WAF** (bot ve OWASP kuralları), statik içerik (afiş görselleri) edge'de önbellekleniyor.
2. **Origin'ler:** West Europe ve East US'te iki aynı uygulama yığını. Front Door, sağlık probe'larıyla gecikmeye göre en yakın sağlıklı bölgeye yönlendiriyor; bir bölge düşerse trafik **otomatik** diğerine geçiyor.
3. **Bölgesel katman:** Her bölgede **Application Gateway (WAF_v2, zone-redundant)** — `/api/*` API'ye, `/*` web'e; backend'e özel IP'den ulaşıldığı için origin'ler **private** (Front Door Premium Private Link).
4. **Uygulama katmanı:** Web → iç **Internal Load Balancer** (Standard, zone-redundant frontend) → App VM'leri; VM'ler 3 zone'a yayılı.
5. **Veritabanı:** her bölgede replikalı/geo-replicated veri katmanı (→ C1.16).
6. **HTTP-dışı servis:** Oyun tarzı bir UDP mesajlaşma servisi için **Standard Load Balancer** (L4) + **Traffic Manager (Performance)**.
7. **Maliyet notu:** Front Door Premium + WAF_v2 + iki bölge, sabit maliyeti yüksek bir mimaridir; "yalnızca bir bölge, HTTP, WAF yok" olan basit bir blog için **Application Gateway veya doğrudan App Service** yeterlidir.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → servis eşleme; L4/L7 ve bölgesel/global ayrımı; Traffic Manager yöntemleri; "NOT" sorularında "hangisi HTTP içeriğini görmez?".

| Senaryodaki ifade | Cevap |
|---|---|
| "TCP/UDP trafiğini bölgesel olarak dağıt", "çok hızlı, katman 4" | **Azure Load Balancer** |
| "Backend VM'lere iç (internal) dağıtım, özel IP" | **Internal Load Balancer** |
| "URL path / host adına göre yönlendir" | **Application Gateway** (veya Front Door) |
| "SSL/TLS offload", "cookie affinity" | **Application Gateway** |
| "SQL injection / XSS'e karşı web uygulaması koruması" | **WAF** (Application Gateway veya Front Door üzerinde) |
| "Global HTTP uygulaması, hızlandırma, edge'de CDN + WAF" | **Azure Front Door** |
| "DNS tabanlı yönlendirme, her protokol, trafik içinden geçmesin" | **Traffic Manager** |
| "Aktif/pasif failover, DNS ile" | Traffic Manager — **Priority** |
| "Kullanıcıya en düşük gecikmeli endpoint" | Traffic Manager — **Performance** |
| "Kullanıcıyı konumuna göre belirli bölgeye (veri egemenliği)" | Traffic Manager — **Geographic** |
| "Trafiği %90/%10 böl (canary)" | Traffic Manager — **Weighted** |
| "Statik içeriği kullanıcıya yakın önbellekten sun" | **CDN** (günümüzde **Front Door**) |
| "Sağlıksız backend'i otomatik devreden çıkar" | **Health probe** (tüm servislerde) |

**Tuzaklar:**
- **Traffic Manager ↔ Front Door:** DNS (yönlendirir) ↔ reverse proxy (içinden geçer). "WAF/CDN/anında failover" gören Front Door'u seçer.
- **Application Gateway ↔ Load Balancer:** L7 (URL, WAF, TLS) ↔ L4 (TCP/UDP, sadece IP:port).
- "Load Balancer URL'ye göre yönlendirir" → **Hayır**.
- "Traffic Manager trafiği şifreler/WAF uygular" → **Hayır**.
- "Application Gateway global bir servistir" → **Hayır**, bölgeseldir.
- "Front Door bir region'a konuşlandırılır" → **Hayır**, global servistir.
- "Standard LB varsayılan olarak her inbound trafiğe izin verir" → **Hayır**, NSG ile izin gerekir.
- "Availability zone yük dengelemenin alternatifidir" → **Hayır**; zone'lar fiziksel yalıtım, LB trafik dağıtımıdır — birlikte kullanılır.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Web uygulaması için L4 Load Balancer seçip "WAF neden yok?" demek | L7 + WAF için Application Gateway/Front Door |
| Traffic Manager'dan anlık failover beklemek | DNS TTL + probe süresi kadar gecikir; anında failover için Front Door |
| Health probe'u yalnızca "port açık mı" ile yapmak | Uygulamayı ve bağımlılıkları (DB) doğrulayan `/health` endpoint'i |
| NSG'nin probe trafiğini (`AzureLoadBalancer`) engellemesi | Probe kaynak adresine izin ver; yoksa tüm backend "sağlıksız" görünür |
| Standard LB kurup NSG'de port açmayı unutmak | Standard varsayılan kapalıdır; NSG ile inbound izin ver |
| Tek instance'ı LB arkasına koyup "HA sağladım" demek | Backend'de ≥ 2 instance ve farklı zone'lar gerekir; tek backend'in arızası LB ile çözülmez |
| Front Door ve Application Gateway'de aynı kuralları gereksiz yere ikilemek | WAF'ı bilinçli konumlandır; çift WAF ek maliyet ve yanlış pozitif getirir |
| Basic LB/Basic public IP ile yeni mimari kurmak | Emekli; Standard SKU |
| ASP.NET Core arkasında `X-Forwarded-For/Proto` başlıklarını işlememek | `UseForwardedHeaders` (gerçek istemci IP'si, HTTPS şeması) → M12 |
| Laboratuvardan Standard public IP/LB'yi silmeyi unutmak | Saatlik ücretlenir; resource group'u sil |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Load Balancer (L4) | Çok düşük gecikme, yüksek throughput, ucuz | İçeriği görmez: WAF/URL routing/TLS offload yok |
| Application Gateway (L7) | Zengin yönlendirme, WAF, TLS offload | Bölgesel; sabit saatlik maliyet (WAF_v2 daha yüksek); autoscale ısınma süresi |
| Front Door | Global hızlandırma, edge WAF/CDN, anında failover | Daha yüksek maliyet; sadece HTTP/HTTPS; yapılandırma yayılması dakikalar alabilir |
| Traffic Manager | Ucuz, her protokol, basit | DNS TTL ile yavaş failover; WAF/önbellek yok |
| TLS offload (gateway'de) | Backend CPU'su rahatlar, sertifika tek yerde | Gateway–backend arası şifresiz kalabilir → hassas veri için end-to-end TLS |
| Çok katmanlı kombinasyon | Derinlikli koruma, her katmanda uzmanlık | Karmaşıklık, gecikme, çoklu maliyet kalemi |
| Zone-redundant frontend | Zone arızasına dayanıklılık | Bölgeler arası değil — region arızası için global katman şart |
| Çok region'lı aktif/aktif | Region arızası + yakın kullanıcı deneyimi | Veri katmanı karmaşıklığı (çoğaltma/tutarlılık), 2× maliyet |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Load-balancing options in Azure](https://learn.microsoft.com/en-us/azure/architecture/guide/technology-choices/load-balancing-overview)
- [What is Azure Load Balancer?](https://learn.microsoft.com/en-us/azure/load-balancer/load-balancer-overview) · [Basic Load Balancer retirement](https://learn.microsoft.com/en-us/azure/load-balancer/load-balancer-basic-upgrade-guidance)
- [What is Azure Application Gateway?](https://learn.microsoft.com/en-us/azure/application-gateway/overview) · [Application Gateway V1 retirement](https://learn.microsoft.com/en-us/azure/application-gateway/v1-retirement)
- [What is Azure Front Door?](https://learn.microsoft.com/en-us/azure/frontdoor/front-door-overview) · [Front Door (classic) retirement](https://azure.microsoft.com/updates/azure-front-door-classic-will-be-retired-on-31-march-2027)
- [What is Traffic Manager?](https://learn.microsoft.com/en-us/azure/traffic-manager/traffic-manager-overview) · [Traffic routing methods](https://learn.microsoft.com/en-us/azure/traffic-manager/traffic-manager-routing-methods)
- [Azure CDN overview](https://learn.microsoft.com/en-us/azure/cdn/cdn-overview)
- [`az network traffic-manager`](https://learn.microsoft.com/en-us/cli/azure/network/traffic-manager) · [`az network lb`](https://learn.microsoft.com/en-us/cli/azure/network/lb)


### 📝 Bölüm 4.2 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir ekip, tek bir region'daki VM'ler arasında **TCP trafiğini** düşük gecikmeyle dağıtmak istiyor; HTTP başlıkları veya URL'lerle ilgilenmiyor. Hangi servis **en uygundur**?

- A) Azure Front Door
- B) Azure Application Gateway
- C) Azure Load Balancer
- D) Traffic Manager

<details><summary>Cevap</summary>

**C.** Load Balancer, bölgesel katman 4 (TCP/UDP) dağıtıcıdır. Application Gateway ve Front Door HTTP/HTTPS içindir; Traffic Manager DNS tabanlıdır.
</details>

#### Soru 2: Tek seçim
Bir web uygulaması için `/images/*` isteklerinin bir backend havuzuna, `/api/*` isteklerinin başka bir havuza gitmesi, ayrıca **SQL injection** koruması isteniyor. Tek bir **bölgesel** servis seçilecek. Hangisi?

- A) Standard Load Balancer
- B) Traffic Manager
- C) Application Gateway (WAF ile)
- D) Azure DNS

<details><summary>Cevap</summary>

**C.** URL path tabanlı yönlendirme ve WAF, bölgesel L7 servisi olan Application Gateway'in yetenekleridir.
</details>

#### Soru 3: Eşleştirme
Her servisi, **kapsamı ve katmanı** doğru anlatan ifadeyle eşleştirin: **Load Balancer · Application Gateway · Front Door · Traffic Manager**

1. Bölgesel, L4
2. Bölgesel, L7
3. Global, L7 (reverse proxy)
4. Global, DNS tabanlı

<details><summary>Cevap</summary>

**1 → Load Balancer · 2 → Application Gateway · 3 → Front Door · 4 → Traffic Manager.**
</details>

#### Soru 4: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Traffic Manager, istemci trafiğinin kendi üzerinden geçmesini sağlar. | ☐ | ☐ |
| b) Traffic Manager yalnızca HTTP/HTTPS uygulamalarla çalışır. | ☐ | ☐ |
| c) Traffic Manager, sağlıksız endpoint'i DNS yanıtından çıkarabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Hayır · c) Evet.** Traffic Manager yalnızca **DNS cevabı** üretir; istemci endpoint'e doğrudan bağlanır. DNS seviyesinde çalıştığı için protokolden bağımsızdır. Health probe ile sağlıksız endpoint cevaptan çıkar.
</details>

#### Soru 5: Tek seçim
Bir şirket, **aktif/pasif** mimaride ana region çökerse DNS ile **yedek region'a** yönlendirme yapmak istiyor. Traffic Manager'da hangi yöntem uygundur?

- A) Weighted
- B) Priority
- C) Geographic
- D) Multivalue

<details><summary>Cevap</summary>

**B.** Priority her zaman en yüksek öncelikli sağlıklı endpoint'i döndürür; o sağlıksızsa sıradakine geçer — klasik failover.
</details>

#### Soru 6: Eşleştirme
Her ihtiyacı Traffic Manager yöntemiyle eşleştirin: **Performance · Weighted · Geographic · Subnet**

1. Trafiğin %10'unu yeni sürümün olduğu endpoint'e gönder
2. İstemciye gecikme açısından en yakın endpoint'i ver
3. AB kullanıcılarını veri egemenliği nedeniyle yalnızca AB region'ına yönlendir
4. Belirli bir kurumsal IP aralığındaki istemcileri belirli bir endpoint'e gönder

<details><summary>Cevap</summary>

**1 → Weighted · 2 → Performance · 3 → Geographic · 4 → Subnet.**
</details>

#### Soru 7: Senaryo
Global kullanıcıları olan bir web uygulaması için istenenler: edge'de **WAF**, **statik içerik önbellekleme**, bir region düştüğünde **DNS TTL'ini beklemeden** otomatik geçiş. Hangi servis **en uygundur**?

- A) Traffic Manager
- B) Azure Load Balancer
- C) Azure Front Door
- D) Application Gateway tek başına

<details><summary>Cevap</summary>

**C.** Front Door global bir reverse proxy'dir: edge'de WAF, CDN ve health-probe tabanlı anında failover sağlar. Traffic Manager DNS TTL'ine bağlıdır ve WAF/önbellek yoktur; Application Gateway bölgeseldir.
</details>

#### Soru 8: Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi iki** yetenek Application Gateway'de bulunur?

- A) SSL/TLS termination (offload)
- B) URL path tabanlı yönlendirme
- C) DNS yanıtını coğrafi konuma göre değiştirme
- D) UDP trafiğini katman 4'te dağıtma

<details><summary>Cevap</summary>

**A ve B.** C Traffic Manager'ın (Geographic yöntemi), D Load Balancer'ın işidir.
</details>

#### Soru 9: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Front Door global bir servistir; bir region seçerek dağıtılmaz. | ☐ | ☐ |
| b) Application Gateway birden çok region'ı kapsayan global bir servistir. | ☐ | ☐ |
| c) WAF, Application Gateway ve Front Door'a eklenebilen bir özelliktir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Application Gateway bölgeseldir; Front Door globaldir.
</details>

#### Soru 10: Tek seçim
Bir **internal** (iç) uygulama katmanında, web katmanındaki VM'lerden gelen TCP isteklerini **özel IP** ile app VM'lerine dağıtmak gerekiyor. Ne kullanılır?

- A) Public Standard Load Balancer
- B) Internal Load Balancer
- C) Azure Front Door
- D) Traffic Manager

<details><summary>Cevap</summary>

**B.** Internal Load Balancer'ın frontend'i VNet içi özel IP'dir; public IP gerekmez.
</details>

#### Soru 11: Tek seçim
Bir Standard Load Balancer kuruldu, backend VM'ler sağlıklı, fakat internetten gelen 80 portu isteği backend'e **ulaşmıyor**. En olası neden nedir?

- A) Standard Load Balancer varsayılan olarak kapalıdır; backend subnet/NIC'te NSG ile 80'e izin verilmemiştir
- B) Standard SKU yalnızca UDP destekler
- C) Load Balancer yalnızca iç trafik içindir
- D) Availability zone etkin olduğu için trafik engellenir

<details><summary>Cevap</summary>

**A.** Standard LB "secure by default"tur: inbound trafik NSG ile açıkça izin verilmedikçe geçmez.
</details>

#### Soru 12: Tek seçim
Load Balancer'da **health probe**'un amacı nedir?

- A) Backend'e gelen trafiği şifrelemek
- B) Sağlıksız instance'ları tespit edip trafikten çıkarmak
- C) Backend'lerin maliyetini hesaplamak
- D) DNS kaydı oluşturmak

<details><summary>Cevap</summary>

**B.** Probe periyodik yoklama yapar; ardışık başarısızlıkta instance havuzdan çıkarılır, düzelince geri alınır.
</details>

#### Soru 13: Sıralama
Aşağıdaki mimaride bir isteğin izleyeceği yolu **internetten başlayarak** sıralayın: **Internal Load Balancer · Azure Front Door · App VM · Application Gateway**

<details><summary>Cevap</summary>

**Azure Front Door → Application Gateway → Internal Load Balancer → App VM.** Global L7 giriş, bölgesel L7 yönlendirme, iç L4 dağıtım, en sonda backend.
</details>

#### Soru 14: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Availability zone'lar, yük dengeleyicinin yerini alır. | ☐ | ☐ |
| b) Zone-redundant bir Load Balancer frontend'i, bir zone arızasında hizmetin sürmesine yardımcı olur. | ☐ | ☐ |
| c) Region arızasına karşı dayanıklılık için bölgesel yük dengeleyici tek başına yeterlidir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** Zone'lar fiziksel yalıtımdır, LB trafik dağıtır — birlikte kullanılır. Region arızası için global bir katman (Front Door/Traffic Manager) ve çok region'lı dağıtım gerekir.
</details>

#### Soru 15: Senaryo
Bir ekibin UDP tabanlı bir oyun sunucusu var; kullanıcıları gecikmeye göre en yakın region'a yönlendirmek istiyorlar. HTTP kullanılmıyor. Aşağıdakilerden **hangisi** doğru bir tasarımdır?

- A) Azure Front Door (Premium) + WAF
- B) Her region'da Standard Load Balancer + Traffic Manager (Performance yöntemi)
- C) Application Gateway + cookie affinity
- D) Yalnızca Azure DNS özel zone

<details><summary>Cevap</summary>

**B.** UDP için L4 gerekir (Load Balancer); Traffic Manager DNS düzeyinde protokolden bağımsızdır ve Performance yöntemiyle en düşük gecikmeli endpoint'i verir. Front Door ve Application Gateway HTTP/HTTPS içindir.
</details>

#### Soru 16: Tek seçim
Aşağıdaki ifadelerden hangisi **yanlıştır**?

- A) Front Door (classic), Standard/Premium'a taşınması gereken, emekliye ayrılacak bir üründür.
- B) Azure CDN statik içeriği kullanıcıya yakın edge sunucularında önbelleğe alır.
- C) Basic Load Balancer'ın SLA'sı, Standard Load Balancer ile aynıdır ve yeni projelerde tercih edilir.
- D) Application Gateway V1, V2'ye taşınması gereken emekli bir SKU'dur.

<details><summary>Cevap</summary>

**C.** Basic Load Balancer emekliye ayrılmıştır, SLA'sı yoktur ve yeni projelerde kullanılmaz; Standard'ın SLA'sı vardır. Emeklilik tarihlerini resmî duyurulardan doğrula.
</details>


---

## 4.3 Ağ Güvenliği, Bastion ve Key Vault

> **Sınav Alanı:** Describe Azure architecture and services — genişletilmiş kapsam · **Konu Kodu:** C4.3

### 1. Ne?

[C1.10](#27-güvenlik-zero-trust-defense-in-depth-defender-for-cloud) *defense in depth* modelinin katmanlarını anlattı; bu konu o katmanlardaki **somut Azure servislerini** anlatır:

| Katman (C1.10) | Servis | Tek cümle |
|---|---|---|
| **Ağ** | **NSG** | Subnet/NIC düzeyinde, IP-port-protokol tabanlı **stateful** izin/engel kuralları |
| **Ağ** | **ASG** | VM'leri **mantıksal gruplara** ayırıp NSG kurallarında IP yerine grup adı kullanma |
| **Çevre / ağ** | **Azure Firewall** | Merkezi, yönetilen, **stateful** güvenlik duvarı (L3–L7, FQDN kuralları, threat intelligence) |
| **Çevre** | **Azure DDoS Protection** | Hacimsel/protokol saldırılarına karşı koruma |
| **Uygulama / çevre** | **WAF** | Web saldırılarına (SQL injection, XSS) karşı L7 koruma (→ [C1.17](#42-yük-dengeleme-ve-trafik-dağıtımı)) |
| **Compute / yönetim erişimi** | **Azure Bastion**, **JIT VM access** | Public IP'siz / süreli açılan yönetim erişimi |
| **Ağ** | **Service endpoint ↔ Private endpoint** | PaaS servisine ağ izolasyonlu erişim (→ C1.7) |
| **Uygulama / veri** | **Azure Key Vault** | Secret, anahtar ve sertifika için merkezi, denetimli depo |
| **Veri** | **Şifreleme** (at rest / in transit) | Platform yönetimli ya da müşteri yönetimli anahtarlar |
| *(bağlam)* | **Sentinel, Defender XDR, Customer Lockbox** | SIEM/SOAR, XDR, Microsoft mühendis erişimine onay |

Bu konunun kodu yoktur; pratik kısım `az` CLI laboratuvarıdır (3.7).

---

### 2. Neden?

- **Sınav:** "Hangi servis?" soruları: NSG ↔ Azure Firewall ↔ DDoS ↔ WAF; "VM'e public IP vermeden RDP/SSH" → Bastion; "parolayı/anahtarı kodda tutma" → Key Vault + managed identity. Defense in depth sorularının seçenekleri bu servislerden oluşur.
- **Gerçek işte:** İhlallerin çoğu **yanlış yapılandırmadan** olur: internete açık 3389/22, koda gömülü connection string, herkese açık storage. NSG + Bastion + Key Vault bu üç klasik deliği kapatır.
- **Bağlam:** Key Vault SDK, `DefaultAzureCredential`, managed identity kodu → C2.11; kimlik ve erişim → M9; kurumsal ağ güvenliği mimarisi (hub-spoke + Firewall) → S8.3.

---

### 3. Nasıl?

#### 3.1 Network Security Group (NSG) ⭐

**NSG**, bir **subnet'e** veya bir **NIC'e** (VM'in ağ kartı) bağlanan, trafiği **IP, port, protokol** ile filtreleyen güvenlik kuralları listesidir.

- **Kural alanları:** öncelik (priority), ad, **yön** (inbound/outbound), kaynak, hedef, port, protokol, **eylem** (Allow/Deny).
- **Değerlendirme:** kurallar **öncelik numarasına göre, küçükten büyüğe (100–4096)** işlenir; **ilk eşleşen kural uygulanır**, sonrakilere bakılmaz.
- **Stateful:** bir bağlantıya inbound izin verdiysen **dönüş trafiği için ayrı kural gerekmez** (ve tersi).
- **Hizmet etiketleri (service tags):** `Internet`, `VirtualNetwork`, `AzureLoadBalancer`, `Storage`, `Sql`… — Microsoft'un yönettiği IP gruplarıdır; IP listesini elle takip etmezsin.
- **Varsayılan kurallar** (silinemez, en düşük öncelikli — 65000+):

| Yön | Kural | Öncelik | Etki |
|---|---|---|---|
| Inbound | `AllowVnetInBound` | 65000 | VNet içi trafiğe izin |
| Inbound | `AllowAzureLoadBalancerInBound` | 65001 | Azure LB health probe'larına izin |
| Inbound | `DenyAllInBound` | 65500 | Geri kalan **her şey engel** |
| Outbound | `AllowVnetOutBound` | 65000 | VNet içi |
| Outbound | `AllowInternetOutBound` | 65001 | İnternete çıkışa **izin** |
| Outbound | `DenyAllOutBound` | 65500 | Geri kalan engel |

> Yani: internetten **gelen** trafik varsayılan olarak **engelli**; **giden** internet trafiği varsayılan olarak **serbest**.

- **Subnet + NIC birlikte:** ikisinde de NSG varsa, **inbound** trafik önce subnet NSG'den, sonra NIC NSG'den geçer; **outbound** tersi. **İkisi de izin vermelidir.**
- **NSG ücretsizdir** (kural sayısı limitine bağlı).
- NSG **L3/L4** filtredir: HTTP içeriğini, URL'yi, FQDN'i bilmez.

**Application Security Group (ASG):** "web sunucuları", "veri sunucuları" gibi **mantıksal gruplar** tanımlayıp NSG kuralında IP yerine grup adını kullanırsın (`asg-web → asg-db : 1433 izin`). VM eklendikçe kural değişmez — kuralları **uygulama yapısına göre** yazmanın yolu.

#### 3.2 Azure Firewall ⭐ ve NSG ile farkı

**Azure Firewall**, VNet'e konuşlandırılan **yönetilen, bulut-yerel, stateful** bir ağ güvenlik duvarıdır (yerleşik yüksek erişilebilirlik ve ölçekleme).

| Yetenek | Açıklama |
|---|---|
| **Kural türleri** | **Network** (IP/port/protokol), **Application** (**FQDN** ve URL — `*.windows.net`'e çıkış izin), **DNAT** (gelen trafiği iç adrese çevir) |
| **Threat intelligence** | Bilinen kötü IP/domain'lerle trafiği **uyar veya engelle** |
| **FQDN etiketleri** | Windows Update, Azure servis FQDN grupları için hazır etiketler |
| **Merkezi yönetim** | **Firewall Policy** (Firewall Manager ile çoklu firewall/subscription) |
| **Hub-spoke** | Tüm spoke'lardan çıkış/giriş trafiği hub'daki firewall'dan (UDR ile) geçirilir |
| **SKU'lar** | **Basic** (küçük/orta işletme), **Standard**, **Premium** (**TLS inspection, IDPS**, URL filtreleme, web kategorileri) |

> **Maliyet uyarısı:** Azure Firewall **sabit saatlik** bir ücret + işlenen veri ücreti taşır ve küçük bir lab için bile pahalıdır (Premium en pahalı). Bu yüzden laboratuvarda **kurmuyoruz**; teoridir.

**NSG ↔ Azure Firewall** ⭐

| | **NSG** | **Azure Firewall** |
|---|---|---|
| Ne? | Subnet/NIC'te **paket filtresi** (dağıtık) | **Merkezi**, yönetilen güvenlik duvarı servisi |
| Katman | L3–L4 (IP, port, protokol) | L3–**L7** (FQDN, URL; Premium'da TLS/IDPS) |
| FQDN/URL filtreleme | **Yok** | **Var** |
| Threat intelligence / IDPS | Yok | Var (IDPS Premium) |
| Kapsam | Tek VNet'in subnet'i/NIC'i | Birçok VNet/subscription'ı **merkezden** (hub-spoke) |
| Maliyet | **Ücretsiz** | **Sabit saatlik + veri** |
| Tipik rol | **Segmentasyon**, mikro-ağ kuralları | **Çevre ve çıkış kontrolü**, merkezi politika |

İkisi **birlikte** kullanılır (defense in depth): NSG her subnet'te "ne konuşabilir"i, Firewall merkezde "internete/dışarıya ne gidebilir"i belirler.

#### 3.3 Azure DDoS Protection

**DDoS** (Distributed Denial of Service): kaynağı, çok sayıda kaynaktan gelen sahte trafikle erişilemez kılma saldırısı.

| Koruma | Kapsam | Not |
|---|---|---|
| **Altyapı koruması (basic)** | **Tüm Azure** public IP'leri için **varsayılan, ücretsiz, her zaman açık** | Platformun kendini koruması; ayarlanamaz |
| **DDoS Network Protection** | Bir **VNet**'teki tüm public IP'ler (plan VNet'lere bağlanır) | Adaptif ayarlama, **DDoS Rapid Response** uzman desteği, **maliyet koruması**, WAF indirimi; sabit aylık ücret **yüksek** |
| **DDoS IP Protection** | **Tek tek public IP** başına | Aynı mitigasyon motoru; **Rapid Response, cost protection, WAF indirimi yok**; küçük ölçeklerde daha ucuz |

> Eski adlandırma: "DDoS Protection **Basic / Standard**" — sınav materyallerinde hâlâ geçebilir. **Basic ≈ varsayılan altyapı koruması; Standard ≈ Network Protection**. Fiyatlar için güncel fiyat sayfasına bak.
> DDoS Protection **L3/L4 hacimsel** saldırıları durdurur; **uygulama katmanı (L7)** saldırılar (SQL injection, bot) için **WAF** gerekir — ikisi tamamlayıcıdır.

#### 3.4 WAF, JIT ve Bastion

- **Web Application Firewall (WAF):** OWASP tipi web saldırılarına karşı **L7** koruma; **Application Gateway** veya **Front Door** üzerinde yaşar (→ [C1.17](#42-yük-dengeleme-ve-trafik-dağıtımı)).
- **Just-in-time (JIT) VM access** *(Defender for Cloud özelliği → C1.10)*: yönetim portları (22/3389) **varsayılan kapalıdır**; kullanıcı **istek yapınca, belirli IP ve süre için** NSG kuralı otomatik açılır, süre bitince kapanır.

**Azure Bastion** ⭐ — VM'e **public IP olmadan**, tarayıcıdan (Azure portal) veya native client ile **TLS (443)** üzerinden RDP/SSH bağlantısı sağlayan yönetilen PaaS.

| Özellik | Değer |
|---|---|
| Neden? | VM'lerde **22/3389'u internete açmamak**; VM'in public IP'si olmaması |
| Konum | VNet'te özel **`AzureBastionSubnet`** (genellikle `/26` veya daha büyük); peering'li VNet'lerdeki VM'lere de erişebilir (SKU'ya bağlı) |
| Güvenlik | Bağlantı TLS ile; VM'e erişim Bastion'ın **özel IP**'sinden; port taraması/kaba kuvvet yüzeyi yok |
| SKU'lar | **Developer** (paylaşımlı, **saatlik ücretsiz**, tek VM bağlantısı, sınırlı bölgeler), **Basic**, **Standard** (native client, dosya aktarımı, ölçekleme…), **Premium** (oturum kaydı, **public IP'siz özel dağıtım**) *(özellik–SKU eşlemesini güncel tablodan doğrula)* |

> **Maliyet uyarısı:** Basic/Standard/Premium Bastion, dağıtım **saatlik** ücretlenir (sürekli) ve lab için pahalıdır; bu yüzden **kurmuyoruz** (yalnızca teori). Developer SKU ücretsizdir ama kısıtlıdır (paylaşımlı altyapı, tek bağlantı, seçili bölgeler).

**Bastion ↔ JIT ↔ VPN:**

| İhtiyaç | Seçenek |
|---|---|
| Public IP'siz, tarayıcıdan RDP/SSH, yönetilen | **Bastion** |
| Portlar kapalı, istek anında süreli açılsın (VM'in public IP'si var/gerekli) | **JIT** |
| Geliştirici makinesinden VNet'e genel erişim | **Point-to-site VPN** (→ C1.7) |

#### 3.5 Service endpoint ↔ Private endpoint (özet)

(→ [C1.7](#24-ağ-servisleri-vnet-peering-vpn-expressroute-dns) §3.6) **Service endpoint:** VNet'in kimliğini servise bildirir; servis firewall'ında yalnızca o subnet'e izin verirsin — ama servis **public IP'siyle** erişilir. **Private endpoint:** servise VNet içinde **özel IP** verir; public erişim tamamen kapatılabilir (**daha güçlü izolasyon**, ücretli).

#### 3.6 Azure Key Vault ⭐ ve şifreleme

**Key Vault**, **secret'ları** (parola, connection string, API anahtarı), **kriptografik anahtarları** ve **sertifikaları** güvenli saklayan, erişimi denetleyen ve **denetim kaydı** (audit log) tutan yönetilen servistir. Amaç: secret'ların **kodda, config dosyasında veya repoda** durmaması.

| Nesne | Ne? | Örnek |
|---|---|---|
| **Secret** | Küçük gizli metin (≤ 25 KB) | DB parolası, API anahtarı, connection string |
| **Key** | Şifreleme/imzalama için **kriptografik anahtar** (RSA/EC); anahtarın kendisi dışarı çıkmadan işlem yapılır | Customer-managed key, imzalama |
| **Certificate** | Sertifika yaşam döngüsü (oluştur, yenile, sakla) | TLS sertifikası |

**Katmanlar:**

| | **Standard** | **Premium** | **Managed HSM** |
|---|---|---|---|
| Anahtar koruması | Yazılım tabanlı | **HSM destekli** anahtarlar (paylaşımlı HSM) | **Tek kiracılı, ayrılmış HSM** (yüksek güvence/uyumluluk) |
| Tür | Key Vault | Key Vault | Ayrı kaynak türü; kendi RBAC'ı |
| Ne zaman? | Çoğu uygulama | Uyumluluk HSM gerektiriyorsa | Katı regülasyon, tam anahtar sahipliği |

**Erişim kontrolü — iki düzlem:**

- **Yönetim düzlemi (management plane):** kasa oluşturma/silme/ayar — **Azure RBAC** (Owner, Contributor…).
- **Veri düzlemi (data plane):** secret/key/certificate **okuma/yazma** — **Azure RBAC** (önerilen) **veya** eski **vault access policy**.

| | **Azure RBAC (önerilen)** | **Access policy (eski model)** |
|---|---|---|
| Rol örnekleri | `Key Vault Administrator`, `Secrets Officer`, `Secrets User`, `Crypto User`, `Certificates Officer`, `Reader` | Kasa başına izin listesi (get/list/set…) |
| Kapsam | Kasa, resource group, subscription ya da **tek nesne** düzeyinde | Yalnızca kasa düzeyinde |
| Tutarlılık | Tüm Azure ile aynı model, PIM/koşullu erişim, merkezi denetim | Ayrı yönetim, ayrıntı sınırlı |
| Tuzak | — | **Contributor** rolü olan biri kendine access policy ekleyip **secret'lara erişebilir** (RBAC modelinde Contributor veri düzlemine erişemez) |

**Silinme koruması:**

- **Soft delete:** silinen kasa/nesne **bekleme süresi** (7–90 gün, varsayılan 90) boyunca geri alınabilir (`recover`). Artık **her zaman açıktır**.
- **Purge protection:** açıksa, silinmiş nesne bekleme süresi dolmadan **kalıcı silinemez** (kimse — yönetici dahil — `purge` yapamaz). **Bir kez açılınca kapatılamaz.** Kritik üretim kasalarında önerilir; lab'da **açma** (kasa adı bekleme süresince rezerve kalır).

**Managed identity ile kimlik bilgisiz erişim** (→ C1.9, C2.11): uygulama (App Service, VM, Function) bir **managed identity** alır; bu kimliğe kasada `Key Vault Secrets User` rolü verilir; uygulama kodu **parola olmadan** token alıp secret'ı okur:

```csharp
// No password in code: DefaultAzureCredential picks up the managed identity in Azure
var client = new SecretClient(new Uri("https://kv-demo.vault.azure.net/"), new DefaultAzureCredential());
KeyVaultSecret secret = await client.GetSecretAsync("DbPassword");
```

Alternatif: App Service ayarında **Key Vault reference** (`@Microsoft.KeyVault(SecretUri=...)`) — kod hiç değişmez, uygulama ortam değişkeni olarak okur (→ C1.6).

**Şifreleme özeti:**

| | Açıklama |
|---|---|
| **Encryption at rest** (durağan) | Storage, managed disk, SQL (TDE), Cosmos DB vb. varsayılan olarak **sunucu tarafında şifrelenir** — **platform-managed key (PMK)** ile, ek yapılandırma gerekmez |
| **Customer-managed key (CMK)** | Anahtarı **sen** Key Vault/Managed HSM'de tutarsın; rotasyon/iptal kontrolü sende (regülasyon, "anahtarı kendi kontrolümde istiyorum") |
| **Encryption in transit** | **TLS** (HTTPS, SQL bağlantı şifrelemesi); VPN/ExpressRoute ayrıca (→ C1.7) |
| **Azure Disk Encryption (ADE)** | VM disklerini OS içinde (BitLocker/dm-crypt) şifreler; anahtar Key Vault'ta. **2026 notu:** ADE'nin **15 Eylül 2028**'de emekliye ayrılması planlanıyor; Microsoft yeni VM'lerde **encryption at host** öneriyor *(tarihi resmî duyurudan doğrula)* |
| **Encryption at host** | VM host'unda uçtan uca şifreleme (temp disk ve cache dahil) |

#### 3.7 Bağlam: Sentinel, Defender XDR, Customer Lockbox

- **Microsoft Sentinel:** bulut-yerel **SIEM/SOAR** — log toplar, olayları korele eder, otomatik müdahale (playbook) çalıştırır. SOC aracıdır. *(Sentinel'in yönetim deneyimi **Microsoft Defender portal**'a taşınıyor; Azure portal desteğinin Mart 2027'de bittiği duyuruldu — tarihi resmî duyurudan doğrula.)*
- **Microsoft Defender XDR:** Microsoft 365 tarafındaki uç nokta, e-posta, kimlik, uygulama korumalarını **birleştiren** XDR portalı; **Defender for Cloud** (→ C1.10) **Azure/multicloud iş yükü** koruması ve posture yönetimidir. İkisi **birbirini tamamlar**: Defender for Cloud uyarıları Defender XDR/Sentinel'e akar.
- **Customer Lockbox:** Microsoft mühendisinin **destek talebi sırasında senin verilerine** erişmesi gerekirse, bu erişimi sen **onaylarsın/reddedersin**.
- **Paylaşılan sorumluluk (→ C1.1):** IaaS'ta NSG/yama/Key Vault politikası sende; PaaS'ta platform altyapısı Microsoft'ta; **veri ve kimlik her zaman müşterinde.**

#### 3.8 Pratik — NSG ve Key Vault

> Bütçe alarmın kurulu olsun (→ C1.1). **NSG, ASG ve VNet ücretsizdir.** Key Vault'ta işlem başına çok küçük bir ücret vardır (lab için sentim). **Bastion ve Azure Firewall'ı KURMUYORUZ** — saatlik sabit ücretleri yüksektir; teoridir. Key Vault'ta **purge protection AÇMA** (kapatılamaz ve kasa adını günlerce rezerve eder).

```bash
az login
RG=rg-az900-c118; LOC=westeurope
az group create --name $RG --location $LOC

# 1) NSG + rules: 80/443 from the internet, 22/3389 only from your own IP
MYIP=$(curl -s https://api.ipify.org)
az network nsg create --resource-group $RG --name nsg-web --location $LOC
az network nsg rule create --resource-group $RG --nsg-name nsg-web --name allow-web \
  --priority 100 --direction Inbound --access Allow --protocol Tcp \
  --source-address-prefixes Internet --destination-port-ranges 80 443
az network nsg rule create --resource-group $RG --nsg-name nsg-web --name allow-mgmt-from-me \
  --priority 110 --direction Inbound --access Allow --protocol Tcp \
  --source-address-prefixes "$MYIP/32" --destination-port-ranges 22 3389
az network nsg rule list --resource-group $RG --nsg-name nsg-web --include-default --output table
#   Custom rules at 100/110, defaults at 65000+ (…DenyAllInBound at 65500)

# 2) Attach the NSG to a subnet
az network vnet create --resource-group $RG --name vnet-demo --address-prefixes 10.10.0.0/16 \
  --subnet-name snet-web --subnet-prefixes 10.10.1.0/24 --network-security-group nsg-web

# 3) ASG: rule by application role instead of IP
az network asg create --resource-group $RG --name asg-web
az network asg create --resource-group $RG --name asg-db
az network nsg create --resource-group $RG --name nsg-db
az network nsg rule create --resource-group $RG --nsg-name nsg-db --name allow-sql-from-web \
  --priority 100 --direction Inbound --access Allow --protocol Tcp \
  --source-asgs asg-web --destination-asgs asg-db --destination-port-ranges 1433

# 4) Key Vault with RBAC authorization (soft delete is always on; short retention for the lab)
KV=kv-az900-$RANDOM
az keyvault create --resource-group $RG --name $KV --location $LOC \
  --enable-rbac-authorization true --retention-days 7

# 5) Grant yourself a data-plane role (Owner on the vault is NOT enough to read secrets under RBAC)
ME=$(az ad signed-in-user show --query id --output tsv)
KVID=$(az keyvault show --name $KV --query id --output tsv)
az role assignment create --assignee-object-id $ME --assignee-principal-type User \
  --role "Key Vault Secrets Officer" --scope $KVID
sleep 60   # role assignment propagation

# 6) Create / read a secret
az keyvault secret set --vault-name $KV --name DbPassword --value 'Demo-P@ssw0rd!'
az keyvault secret show --vault-name $KV --name DbPassword --query value --output tsv

# 7) Soft delete: delete, see it in the deleted list, recover
az keyvault secret delete --vault-name $KV --name DbPassword
az keyvault secret list-deleted --vault-name $KV --output table
az keyvault secret recover --vault-name $KV --name DbPassword
az keyvault secret show --vault-name $KV --name DbPassword --query attributes.enabled

# 8) Passwordless access: a managed identity that can only READ secrets
az identity create --resource-group $RG --name id-app
PID=$(az identity show --resource-group $RG --name id-app --query principalId --output tsv)
az role assignment create --assignee-object-id $PID --assignee-principal-type ServicePrincipal \
  --role "Key Vault Secrets User" --scope $KVID

# 9) Cleanup — mandatory. Deleting the group only SOFT-deletes the vault (the name stays reserved), so purge it
az keyvault delete --name $KV
az keyvault purge --name $KV --location $LOC          # works because purge protection is NOT enabled
az group delete --name $RG --yes --no-wait
```

<details><summary>Beklenen gözlemler ve açıklamaları</summary>

| Adım | Gözlem | Kavram |
|---|---|---|
| 1 | Özel kurallar 100/110'da; `65000-65500` varsayılanlar listelenir | Öncelik ve varsayılan kurallar; `DenyAllInBound` en sonda |
| 2 | Subnet'e NSG bağlanır | NSG subnet düzeyinde uygulanır |
| 3 | ASG kuralı IP içermez | Kuralları uygulama rolüne göre yaz |
| 5 | Rol atamadan `secret set` → `Forbidden` / yetki hatası (Owner olsan bile) | RBAC modelinde **veri düzlemi** ayrı roller ister |
| 6 | Secret değeri düz metin döner | Kasadan okuma; erişimler audit log'a yazılır |
| 7 | Silinen secret `list-deleted`'da görünür, `recover` ile geri gelir | Soft delete |
| 8 | Identity yalnızca `Secrets User` — secret okur ama **yazamaz/silemez** | En az yetki + managed identity |
| 9 | `az keyvault purge` olmadan aynı ad yeniden alınamaz | Soft-deleted kasa adı rezerve kalır |

**Düşünce deneyi:** VM'e RDP açmak için Bastion kullansaydık, NSG'de 3389'a hâlâ internetten izin gerekir miydi? (Hayır — Bastion VM'e VNet içinden özel IP ile bağlanır; VM'de public IP olmaz.)
</details>

---

### 4. Gerçek dünya senaryosu

Bir sağlık-teknolojisi şirketi hasta portalını Azure'a taşıyor:

1. **Çevre:** Portal **Front Door + WAF** arkasında; **DDoS Network Protection** public IP'leri koruyor (rapid response ve maliyet koruması gerekçesiyle).
2. **Ağ segmentasyonu:** Web, uygulama ve veri subnet'lerine **NSG**; kurallar **ASG** ile (`asg-web → asg-app → asg-db`). Varsayılan "engelle"; yalnızca gereken portlar açık.
3. **Çıkış kontrolü:** Tüm giden trafik hub'daki **Azure Firewall (Premium)** üzerinden; yalnızca onaylı FQDN'lere (ödeme sağlayıcı, güncelleme sunucuları) izin, **IDPS** açık.
4. **Yönetim erişimi:** VM'lerde **public IP yok**; yöneticiler **Bastion** ile bağlanır; ihtiyaç anında **JIT** ile süreli port açılır.
5. **Sırlar:** Connection string ve API anahtarları **Key Vault**'ta (RBAC); uygulamalar **managed identity** ile okur. Müşteri yönetimli anahtarlar **Key Vault Premium/Managed HSM**'de; **purge protection** açık.
6. **Veri:** SQL ve Storage için **private endpoint**, public erişim kapalı (→ C1.7); durağan şifreleme (CMK), aktarımda TLS.
7. **İzleme:** Defender for Cloud (→ C1.10) uyarıları **Sentinel**'e akar; SOC olayları oradan yönetir. **Customer Lockbox** açık: Microsoft destek mühendisi veriye ancak onayla erişir.
8. **Sonuç:** Tek bir katman (ör. NSG yanlış yapılandırması) aşılsa bile sonraki katmanlar yavaşlatır/durdurur/tespit eder — defense in depth.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** "Hangi servis hangi katmanı çözer?" senaryoları; NSG/Firewall ayrımı; Bastion; Key Vault'un amacı; evet/hayır tabloları.

| Senaryodaki ifade | Cevap |
|---|---|
| "Subnet/NIC düzeyinde IP-port izin/engel kuralları, ücretsiz" | **NSG** |
| "VM'leri uygulama rolüne göre grupla, kuralda IP yerine grup kullan" | **ASG** |
| "Merkezi, FQDN'e göre çıkış filtreleme, threat intelligence" | **Azure Firewall** |
| "TLS inspection, IDPS" | **Azure Firewall Premium** |
| "Hacimsel (volumetric) DDoS saldırısına karşı koruma" | **DDoS Protection** (temel: varsayılan; Network/IP: ücretli) |
| "SQL injection, XSS" | **WAF** |
| "VM'e public IP olmadan, portaldan RDP/SSH" | **Azure Bastion** |
| "Yönetim portları normalde kapalı, istek anında süreli açılsın" | **Just-in-time VM access** |
| "Secret/parola/sertifika/anahtarı güvenle sakla ve denetle" | **Key Vault** |
| "Uygulama, kodda parola olmadan Key Vault'a erişsin" | **Managed identity** |
| "Anahtar sahipliği müşteride, HSM tek kiracılı" | **Managed HSM** / customer-managed key |
| "Yanlışlıkla silinen secret geri alınsın" | **Soft delete** (purge protection: kalıcı silmeyi engeller) |
| "Microsoft destek mühendisinin verilerine erişimini sen onayla" | **Customer Lockbox** |
| "SIEM/SOAR, olay korelasyonu, playbook" | **Microsoft Sentinel** |

**Tuzaklar:**
- **NSG ↔ Azure Firewall:** NSG ücretsiz L3/L4 dağıtık filtre; Firewall ücretli, merkezi, FQDN/L7.
- **DDoS temel ↔ Network:** temel koruma **her zaman açık ve ücretsiz**; Network Protection (eski "Standard") ek yetenekler ve ücret getirir; **DDoS L3/L4**, **WAF L7**.
- **Bastion = public IP'siz erişim** — "VM'e public IP ver, 3389'u internete aç" **yanlış** cevaptır.
- "Secret'ı `appsettings.json`'a/repoya koy" → **Key Vault + managed identity**.
- "Key Vault'taki secret'ı okumak için **Contributor** yeterli" → RBAC modelinde **hayır**; veri düzlemi rolü gerekir.
- "NSG stateless'tır" → **Hayır**, stateful.
- "Varsayılan olarak outbound internet engellidir (NSG)" → **Hayır**, `AllowInternetOutBound` vardır; inbound engelli.
- "Purge protection geri alınabilir" → **Hayır**, bir kez açılınca kapatılamaz.
- "Sentinel = Defender for Cloud" → **Hayır**: Sentinel SIEM/SOAR (olay analizi), Defender for Cloud posture + workload koruması.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| 22/3389'u `Any` kaynağa açık bırakmak | Kaynak IP kısıtla; **Bastion/JIT/VPN** |
| NSG kurallarını IP'lerle yazıp VM eklendikçe yamamak | **ASG** ve **service tag**'ler |
| Düşük öncelikli numaralı `Allow`'un, sonraki `Deny`'ı ezdiğini unutmak | Önce eşleşen kazanır; öncelikleri bilinçli sırala |
| Subnet NSG'de izin verip NIC NSG'de engeli unutmak | İkisi de izin vermeli |
| NSG'nin `AzureLoadBalancer` probe trafiğini engellemesi | Probe'a izin ver (→ C1.17) |
| NSG ile FQDN'e göre çıkış filtrelemeyi denemek | FQDN için Azure Firewall |
| DDoS'u "WAF olmadan yeterli" sanmak | DDoS L3/L4; L7 için WAF |
| Secret'ları kodda/config'te/Git'te tutmak | Key Vault + managed identity |
| Kasada herkese `Key Vault Administrator` vermek | En az yetki: `Secrets User` gibi dar roller |
| Eski access policy modeliyle başlamak | Yeni kasalarda **RBAC** |
| Üretim kasasında purge protection'ı açmamak | Kritik kasalarda aç (bir kez açılınca geri alınamaz; lab'da açma) |
| Lab kasasını resource group'la silip adın rezerve kaldığını fark etmek | `az keyvault purge` |
| Bastion/Firewall'ı lab'da kurup silmeyi unutmak | Saatlik ücretlenir; mümkünse **kurma**, sil |
| Durağan şifrelemeyi "kapalı" sanıp ek iş yapmak | Çoğu servis varsayılan şifreler; CMK yalnızca gereksinim varsa |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| NSG ile segmentasyon | Ücretsiz, basit, dağıtık | L3/L4 sınırı; kural karmaşıklığı büyüyebilir (ASG ile azaltılır) |
| Azure Firewall | Merkezi politika, FQDN/L7, IDPS, çıkış kontrolü | Sabit saatlik ücret yüksek; tüm trafik hub'dan geçer (gecikme/tek nokta tasarımı) |
| DDoS Network Protection | Rapid Response, maliyet koruması, adaptif ayar | Yüksek aylık sabit ücret; IP Protection daha ucuz ama daha az yetenek |
| Bastion | Public IP'siz yönetim, kaba kuvvet yüzeyi yok | Saatlik ücret; ek subnet; bazı SKU'larda sınırlı özellik |
| JIT | Portlar çoğu zaman kapalı | Talep/onay süreci; Defender for Servers gerektirir (ücretli plan) |
| Key Vault + managed identity | Secret kodda yok, denetim, rotasyon | Bağımlılık (kasa kesintisi/throttling → önbellekle); RBAC yönetimi |
| Customer-managed key | Anahtar kontrolü, iptal imkânı | Operasyonel yük; anahtar kaybı = veri kaybı; ek gecikme/maliyet |
| Managed HSM | En yüksek güvence, tek kiracılı | En pahalı seçenek; yönetim karmaşıklığı |
| Purge protection | Kötü niyetli/yanlışlıkla kalıcı silmeyi engeller | Geri alınamaz; kasa adı süre boyunca rezerve |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Network security groups](https://learn.microsoft.com/en-us/azure/virtual-network/network-security-groups-overview) · [Application security groups](https://learn.microsoft.com/en-us/azure/virtual-network/application-security-groups)
- [What is Azure Firewall?](https://learn.microsoft.com/en-us/azure/firewall/overview) · [Azure Firewall features by SKU](https://learn.microsoft.com/en-us/azure/firewall/features-by-sku)
- [Azure DDoS Protection overview](https://learn.microsoft.com/en-us/azure/ddos-protection/ddos-protection-overview) · [Tier comparison](https://learn.microsoft.com/en-us/azure/ddos-protection/ddos-protection-sku-comparison)
- [What is Azure Bastion?](https://learn.microsoft.com/en-us/azure/bastion/bastion-overview) · [Bastion SKU comparison](https://learn.microsoft.com/en-us/azure/bastion/bastion-sku-comparison)
- [About Azure Key Vault](https://learn.microsoft.com/en-us/azure/key-vault/general/overview) · [Key Vault RBAC guide](https://learn.microsoft.com/en-us/azure/key-vault/general/rbac-guide) · [Soft delete](https://learn.microsoft.com/en-us/azure/key-vault/general/soft-delete-overview)
- [Azure Disk Encryption retirement / encryption at host](https://learn.microsoft.com/en-us/azure/virtual-machines/disk-encryption-overview)
- [Microsoft Sentinel in the Defender portal](https://learn.microsoft.com/en-us/azure/sentinel/microsoft-sentinel-defender-portal) · [Customer Lockbox](https://learn.microsoft.com/en-us/azure/security/fundamentals/customer-lockbox-overview)
- [`az network nsg`](https://learn.microsoft.com/en-us/cli/azure/network/nsg) · [`az keyvault`](https://learn.microsoft.com/en-us/cli/azure/keyvault)


### 📝 Bölüm 4.3 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir subnet'teki VM'lere gelen trafiği **IP, port ve protokole** göre filtrelemek ve bunu **ek ücret ödemeden** yapmak istiyorsunuz. Hangisini kullanırsınız?

- A) Azure Firewall Premium
- B) Network security group (NSG)
- C) DDoS Network Protection
- D) Azure Bastion

<details><summary>Cevap</summary>

**B.** NSG, subnet/NIC düzeyinde ücretsiz L3/L4 paket filtresidir. Azure Firewall ücretli ve merkezidir; DDoS hacimsel saldırı korumasıdır; Bastion yönetim erişimi içindir.
</details>

#### Soru 2: Tek seçim
Bir NSG'de aşağıdaki iki inbound kural var. `443` portuna internetten gelen bir istek ne olur?

| Öncelik | Ad | Port | Kaynak | Eylem |
|---|---|---|---|---|
| 200 | deny-all-web | 443 | Internet | Deny |
| 300 | allow-web | 443 | Internet | Allow |

- A) İzin verilir, çünkü Allow kuralı sonradan tanımlanmıştır
- B) Engellenir, çünkü düşük numaralı (200) kural önce işlenir ve ilk eşleşme uygulanır
- C) Rastgele biri uygulanır
- D) Her iki kural da uygulanır; sonuç Allow olur

<details><summary>Cevap</summary>

**B.** NSG kuralları öncelik numarasına göre küçükten büyüğe işlenir; ilk eşleşen kural uygulanır ve sonrakilere bakılmaz.
</details>

#### Soru 3: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) NSG stateful'dur; izin verilen bir inbound bağlantının dönüş trafiği için ayrı kural gerekmez. | ☐ | ☐ |
| b) Varsayılan olarak NSG, internete giden (outbound) trafiği engeller. | ☐ | ☐ |
| c) Varsayılan olarak NSG, internetten gelen (inbound) trafiği engeller. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Varsayılan kurallar: `AllowInternetOutBound` outbound'u açık tutar; `DenyAllInBound` internetten gelen trafiği engeller (VNet ve Azure Load Balancer trafiği hariç).
</details>

#### Soru 4: Tek seçim
Bir NSG kuralında VM'lerin IP adresleri yerine **"web sunucuları"** ve **"veritabanı sunucuları"** gibi mantıksal gruplar kullanılmak isteniyor. Hangi kaynak bu amaca hizmet eder?

- A) Application security group (ASG)
- B) Route table
- C) Azure DNS
- D) Resource lock

<details><summary>Cevap</summary>

**A.** ASG, VM'leri uygulama rolüne göre gruplar ve NSG kurallarında kaynak/hedef olarak kullanılır; VM eklendikçe kural değişmez.
</details>

#### Soru 5: Tek seçim
Bir şirket, VNet'lerinden internete **yalnızca belirli FQDN'lere** (ör. `*.windows.net`) çıkışa izin vermek ve bilinen kötü domain'leri **threat intelligence** ile engellemek istiyor; politikanın **merkezi** yönetilmesini istiyor. Hangisi **en uygundur**?

- A) NSG
- B) Azure Firewall
- C) Application security group
- D) Service endpoint

<details><summary>Cevap</summary>

**B.** FQDN filtreleme, threat intelligence ve merkezi politika Azure Firewall'ın yetenekleridir; NSG yalnızca IP/port/protokol bilir.
</details>

#### Soru 6: Eşleştirme
Her ihtiyacı servisle eşleştirin: **NSG · Azure Firewall Premium · Azure DDoS Protection · WAF**

1. Subnet'te basit, ücretsiz port/IP filtresi
2. Giden TLS trafiğinin incelenmesi ve IDPS
3. Hacimsel (L3/L4) saldırı koruması
4. SQL injection ve XSS gibi web uygulaması saldırılarına karşı koruma

<details><summary>Cevap</summary>

**1 → NSG · 2 → Azure Firewall Premium · 3 → Azure DDoS Protection · 4 → WAF.**
</details>

#### Soru 7: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure'daki tüm public IP'ler için altyapı düzeyinde temel DDoS koruması varsayılan ve ücretsiz olarak açıktır. | ☐ | ☐ |
| b) DDoS Protection, uygulama katmanı (L7) SQL injection saldırılarını durdurmak için tek başına yeterlidir. | ☐ | ☐ |
| c) DDoS IP Protection, tek tek public IP'ler için seçilebilen bir korumadır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** DDoS L3/L4 hacimsel/protokol saldırılarına karşıdır; L7 web saldırıları için WAF gerekir. Network Protection VNet'i, IP Protection tek tek IP'leri kapsar.
</details>

#### Soru 8: Tek seçim
Bir ekip, sanal makinelerinde **public IP olmadan**, Azure portalından tarayıcı üzerinden RDP/SSH bağlantısı kurmak ve 3389/22 portlarını internete **açmamak** istiyor. Hangisi **en uygundur**?

- A) Azure Bastion
- B) Her VM'e public IP + `Any` kaynağa 3389 izni
- C) Traffic Manager
- D) Azure CDN

<details><summary>Cevap</summary>

**A.** Bastion, VNet'teki `AzureBastionSubnet` üzerinden TLS ile VM'lere özel IP'yle bağlanır; VM'de public IP ve internete açık yönetim portu gerekmez.
</details>

#### Soru 9: Tek seçim
Yönetim portlarının **normalde kapalı** olup yalnızca kullanıcı **istek yaptığında, belirli IP'den ve sınırlı süre** için açılmasını sağlayan özellik hangisidir?

- A) Just-in-time (JIT) VM access
- B) Resource lock
- C) Azure Policy
- D) Availability set

<details><summary>Cevap</summary>

**A.** JIT, istek üzerine NSG kuralını süreli olarak açar ve süre bitince kapatır (Defender for Cloud özelliği).
</details>

#### Soru 10: Senaryo
Bir geliştirici, veritabanı parolasını `appsettings.json` içine yazıp repoya göndermiş. **En iyi** düzeltme hangisidir?

- A) Dosyayı özel repoya taşımak
- B) Parolayı Azure Key Vault'a koymak ve uygulamayı managed identity ile kasadan okutmak
- C) Parolayı Base64 ile kodlamak
- D) Parolayı ortam değişkenine düz metin yazıp aynı VM'de tutmak

<details><summary>Cevap</summary>

**B.** Key Vault secret'ları merkezi ve denetimli saklar; managed identity ile uygulama **kodda parola olmadan** erişir. Base64 şifreleme değildir. (Ayrıca sızdırılmış parola hemen **döndürülmelidir** — rotate.)
</details>

#### Soru 11: Çoklu seçim (iki doğru cevap)
Azure Key Vault'ta **hangi iki** nesne türü saklanabilir?

- A) Secret
- B) Sertifika
- C) Virtual machine görüntüsü
- D) Blob container

<details><summary>Cevap</summary>

**A ve B.** Key Vault secret, key (kriptografik anahtar) ve certificate saklar. VM görüntüsü ve blob container başka servislerin nesneleridir.
</details>

#### Soru 12: Tek seçim
Yeni bir Key Vault için veri düzlemi (secret/key/certificate) erişimi hangi model **önerilir**?

- A) Yalnızca vault access policy
- B) Azure RBAC
- C) Paylaşılan hesap anahtarı (shared key)
- D) Anonim erişim

<details><summary>Cevap</summary>

**B.** Azure RBAC önerilir; tutarlı denetim, daha ince kapsam (tek nesneye kadar) sağlar. Access policy eski modeldir.
</details>

#### Soru 13: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Key Vault'ta soft delete, silinen nesnelerin bekleme süresi boyunca geri alınmasını sağlar. | ☐ | ☐ |
| b) Purge protection açıldığında silinmiş bir nesne bekleme süresi dolmadan kalıcı olarak silinemez. | ☐ | ☐ |
| c) Purge protection bir kez açıldıktan sonra istenirse kapatılabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Purge protection kapatılamaz; bu yüzden lab ortamlarında açılmamalıdır.
</details>

#### Soru 14: Sıralama
RBAC modelindeki bir Key Vault'ta **soft delete** davranışını göstermek için aşağıdaki adımları doğru sıraya koyun: **(a) Secret'ı sil · (b) Kendine veri düzlemi yazma rolü ata (ör. Secrets Officer) · (c) Secret'ı `recover` ile geri al · (d) RBAC authorization etkin kasayı oluştur · (e) Secret'ı oluştur**

<details><summary>Cevap</summary>

**d → b → e → a → c.** Önce kasa, sonra veri düzlemi rolü (Owner/Contributor gibi yönetim düzlemi rolleri secret yazmak için yeterli değildir), ardından secret oluşturma; silinen secret bekleme süresi içinde `recover` ile geri alınabilir.
</details>

#### Soru 15: Eşleştirme
Her ifadeyi doğru şifreleme kavramıyla eşleştirin: **Platform-managed key · Customer-managed key · TLS · Managed HSM**

1. Hizmetlerin durağan veriyi, müşteri ek yapılandırma yapmadan varsayılan olarak şifrelediği anahtar türü
2. Anahtarın müşteri tarafından Key Vault'ta tutulduğu ve rotasyonunun müşteride olduğu model
3. Aktarım halindeki verinin korunması
4. Tek kiracılı, ayrılmış HSM üzerinde yüksek güvenceli anahtar saklama

<details><summary>Cevap</summary>

**1 → Platform-managed key · 2 → Customer-managed key · 3 → TLS · 4 → Managed HSM.**
</details>

#### Soru 16: Senaryo
Bir şirketin güvenlik ekibi, birçok kaynaktan log toplayıp olayları **korele etmek** ve **otomatik müdahale playbook'ları** çalıştırmak istiyor. Hangi servis **en uygundur**?

- A) Azure Bastion
- B) Microsoft Sentinel
- C) Azure DNS
- D) Network security group

<details><summary>Cevap</summary>

**B.** Sentinel, bulut-yerel SIEM/SOAR'dır. Defender for Cloud ise kaynakların güvenlik duruşunu (posture) ve iş yükü korumasını sağlar; uyarıları Sentinel'e akabilir.
</details>


---

## 4.4 Yedekleme, Felaket Kurtarma ve Taşıma

> **Sınav Alanı:** Describe Azure architecture and services — genişletilmiş kapsam · **Konu Kodu:** C4.4

### 1. Ne?

"Verim güvende mi?" sorusunun üç ayrı cevabı vardır ve karıştırılması bu konunun en büyük tuzağıdır:

| Kavram | Soru | Çözüm | Azure servisi |
|---|---|---|---|
| **High availability (HA)** | "Küçük arızalarda hizmet **kesintisiz** sürsün" | Yedeklilik, zone'lar, load balancer | Availability zone, zone-redundant servisler (→ C1.2, C1.4) |
| **Backup** (yedekleme) | "Sildim / bozuldu / fidye yazılımı vurdu — **geçmiş bir noktaya dönebilir miyim?**" | Zaman içinde alınan, **ayrı** saklanan kurtarma noktaları | **Azure Backup** |
| **Disaster recovery (DR)** | "Bölge/datacenter **tamamen gitti** — hizmet başka yerde **ayağa kalkabilir mi?**" | İkincil konumda replika + failover | **Azure Site Recovery** |

Taşıma (migration) ise ayrı bir iştir: mevcut iş yüklerini **Azure'a** (veya Azure içinde başka bölgeye) getirmek. Araçları: **Azure Migrate**, **Database Migration Service**, **Data Box**, **Resource Mover** *(bağlam)*, hibrit yönetim için **Azure Arc**.

İki ölçü her konuşmanın merkezindedir:

- **RPO (Recovery Point Objective):** En fazla **ne kadar veri kaybı** kabul edilir? (zaman cinsinden; "son 15 dakikanın verisi gidebilir" → RPO = 15 dk)
- **RTO (Recovery Time Objective):** Hizmet **ne kadar sürede** geri gelmeli? ("en geç 2 saatte ayakta" → RTO = 2 sa)

Bu konunun C# kodu yoktur. Pratik, `az` CLI ile Recovery Services vault ve VM yedeklemesi laboratuvarıdır (3.6).

---

### 2. Neden?

- **Sınav/kurs:** "Backup mı Site Recovery mi?", "GRS yedek midir?", "Büyük veriyi çevrimdışı taşımak?" kalıpları senaryo sorularında seçenek olarak çıkar.
- **Gerçek işte:** Felaketlerin çoğu datacenter yangını değildir; **insan hatası** (yanlış `DELETE`), **kötü niyetli silme** ve **fidye yazılımıdır**. Replikasyon/redundancy bunları **de kopyalar**; yalnızca zaman içinde alınmış, değiştirilemez yedek kurtarır.
- **Shared responsibility (→ C1.1):** Microsoft platformu dayanıklı yapar (redundancy), ama **verinin yedeklenmesi müşterinin işidir**. "Buluttayım, yedeğe gerek yok" tehlikeli bir yanılgıdır.
- **Reliability (→ C1.2):** Güvenilir mimari = HA + backup + DR üçlüsünün RPO/RTO hedeflerine göre seçilmiş karışımı.

---

### 3. Nasıl?

#### 3.1 Yedekleme ↔ DR ↔ HA ayrımı ⭐

| | HA | Backup | DR (Site Recovery) |
|---|---|---|---|
| Koruduğu şey | Küçük arızalar (VM, rack, zone) | Silinme, bozulma, fidye yazılımı, yanlış değişiklik | Bölge/datacenter kaybı |
| Mekanizma | Yedeklilik, otomatik failover | Zaman içinde noktalar (point-in-time) | **Sürekli replikasyon** + failover |
| Tipik RPO | ~0 (eşzamanlı yedeklilik) | Saat–gün (yedekleme sıklığı kadar) | Saniye–dakika |
| Tipik RTO | Saniye–dakika | Saat (geri yükleme süresi) | Dakika–saat (recovery plan'a bağlı) |
| Geçmişe dönüş | **Yok** (bozulma da yayılır) | **Evet** | Sınırlı (kısa süreli recovery point'ler) |
| Maliyet | Ek kaynak | Depolama + korunan örnek ücreti | Replika + korunan örnek ücreti |

> Hatırlatma: **HA arızayı sürdürür, backup geçmişi geri getirir, DR bölgeyi yedekler.** Üçü birbirinin yerine geçmez; üretim iş yükü genellikle üçüne birden ihtiyaç duyar.

**RPO / RTO yorumlama:**

```
         son iyi yedek            felaket                    hizmet geri geldi
  ───────────●─────────────────────✖───────────────────────────●──────────▶ zaman
             │◄──────  RPO  ──────►│◄───────────  RTO  ─────────►│
          (kaybolacak veri)                (kesinti süresi)
```

| Hedef | Anlamı | Çözüm yönü |
|---|---|---|
| RPO = 24 sa, RTO = 1 gün | Dün geceki yedeğe dönmek yeter, bir gün beklenebilir | Günlük Azure Backup |
| RPO = 5 dk, RTO = 1 sa | Neredeyse hiç veri kaybı, hızlı dönüş | Site Recovery (sürekli replikasyon) + recovery plan |
| RPO ≈ 0, RTO ≈ 0 | Kesintisiz | Aktif-aktif çok bölgeli mimari + veri tabanında senkron/yakın-senkron replikasyon (maliyetli; → C1.17, S8.3) |

Kural: **RPO küçüldükçe ve RTO küçüldükçe maliyet artar.** İş birimi bu iki sayıyı verir; mimari araç seçimi onlardan çıkar.

#### 3.2 Azure Backup ⭐

**Azure Backup**, yönetilen, ölçeklenebilir yedekleme servisidir: altyapı kurmadan, politika tabanlı, kurtarma noktalarını bir **vault**'ta saklar. İki tür vault vardır:

| Vault | Neleri korur (özet — liste sürümlere göre genişler) |
|---|---|
| **Recovery Services vault** | **Azure VM**'ler, **VM içindeki SQL Server** ve **SAP HANA**, **Azure Files**, kurum içi dosya/klasör (MARS ajanı), DPM / Azure Backup Server |
| **Backup vault** | Daha yeni iş yükleri: **Azure Blobs**, **Azure Disks**, Azure Database for PostgreSQL, AKS *(bağlam)* |

> Sınav hangi iş yükünün hangi vault'ta olduğunu **sormaz**; "Azure Backup nelerin yedeğini alır?" sorusunda VM, SQL/SAP HANA (VM içinde), Azure Files, Blob, disk ve kurum içi sunucular cevaptır. Ayrıntı için güncel [desteklenen senaryolar](https://learn.microsoft.com/en-us/azure/backup/backup-support-matrix) tablosuna bak.

**Temel kavramlar:**

| Kavram | Anlamı |
|---|---|
| **Backup policy** | *Ne sıklıkla* yedek alınır (günlük/haftalık) ve *ne kadar* saklanır (retention: günlük, haftalık, aylık, yıllık) |
| **Recovery point** | Geri dönülebilecek bir zaman noktası |
| **Instant restore (VM)** | Son birkaç günün anlık görüntüleri (snapshot) VM'in diskinin yanında tutulur; hızlı geri dönüş sağlar *(varsayılan politikada kısa süre; ayrıntı sürüme göre değişir)* |
| **Vault tier** | Uzun süreli kurtarma noktaları vault'a taşınır; **maliyeti düşük**, geri yükleme biraz daha yavaş |
| **Storage redundancy (vault)** | LRS / ZRS / GRS; **varsayılan GRS**'tir *(doğrula)*. **Cross-region restore** için GRS gerekir. Redundancy, korunan ilk öğeden **önce** seçilmelidir |
| **Soft delete** | Silinen yedek verisi hemen yok edilmez; varsayılan **14 gün** geri alınabilir. *Enhanced soft delete* ile süre uzatılabilir ve "always-on" yapılırsa kapatılamaz |
| **Immutable vault** | Kurtarma noktalarının politikayla **erken silinmesini/değiştirilmesini engeller** (WORM benzeri); fidye yazılımı ve kötü niyetli yönetici senaryosuna karşı |
| **Multi-user authorization** *(bağlam)* | Kritik işlemler (ör. korumayı durdurma) için ikinci bir onay (Resource Guard) |

**Backup ↔ snapshot** ⭐:

| | Snapshot | Azure Backup |
|---|---|---|
| Ne | Bir diskin/blob'un o anki **salt-okunur kopyası** | Politika, saklama, vault ve yönetim sunan **servis** |
| Nerede | Çoğunlukla **kaynağın yanında** (aynı bölge/hesap) | Ayrı **vault** (izole, farklı yönetim sınırı) |
| Saklama/otomasyon | Elle | Politika tabanlı, otomatik |
| Silme koruması | Kaynakla birlikte silinebilir | Soft delete, immutable vault |

Azure Backup VM yedeklerken **snapshot kullanır** (anlık görüntü → vault'a aktarım); ama "snapshot almak = yedekleme stratejisi" demek değildir.

**Faturalama (özet):** korunan örnek (boyuta göre kademeli) + vault'ta tüketilen depolama. Kullanılmayan, unutulmuş korumalar para yakar → laboratuvarda temizlik adımı kritik.

#### 3.3 Azure Site Recovery (ASR) ⭐

**Azure Site Recovery**, **felaket kurtarma (DR)**: iş yüklerini ikincil bir konuma **sürekli replike eder**; felakette **failover** ile oradan çalıştırırsın.

| Senaryo | Kaynak → Hedef |
|---|---|
| Azure → Azure | Bir bölgedeki **Azure VM**'ler → başka bir bölge (veya zone) |
| Kurum içi → Azure | **VMware**, **Hyper-V** VM'leri ve **fiziksel sunucular** → Azure |

**Kavramlar:**

| Kavram | Anlamı |
|---|---|
| **Replication** | Kaynak VM'in disk değişiklikleri sürekli hedefe kopyalanır (RPO tipik olarak saniye–dakika) |
| **Recovery point** | Kısa süreli (saatler–günler) kurtarma noktaları: crash-consistent ve app-consistent |
| **Test failover** | Üretimi ve replikasyonu **etkilemeden**, **yalıtılmış ağda** DR tatbikatı; tatbikatı yapmayan DR planı güvenilmezdir |
| **Failover** | Gerçek geçiş: replika VM hedef bölgede çalışmaya başlar |
| **Recovery plan** | Birden çok VM'i **sırayla** (ör. önce veritabanı, sonra uygulama, sonra web) ve betik/runbook ile ayağa kaldırma düzeni |
| **Failback (reprotect)** | Birincil geri geldiğinde ters yönde replikasyon + geri dönüş; **otomatik değildir**, planlanır |

ASR **geçmişe dönüş aracı değildir**: replikasyon, bozulmuş/silinmiş veriyi de hızla karşıya taşır. Bu yüzden DR + backup birlikte kullanılır.

#### 3.4 Depolama redundancy'si yedek değildir ⭐

C1.8'deki LRS/ZRS/GRS/GZRS **donanım/zone/bölge arızasına** karşıdır:

| Olay | LRS/ZRS/GRS/GZRS korur mu? | Çünkü |
|---|---|---|
| Disk/rack arızası | ✔ | Birden çok kopya |
| Datacenter/zone kaybı | ✔ (ZRS/GZRS) | Zone'lara yayılmış kopyalar |
| Bölge felaketi | ✔ (GRS/GZRS, failover ile) | Eşleşik bölgede kopya |
| **Yanlışlıkla silme** | ✘ | Silme işlemi **tüm kopyalara** uygulanır |
| **Bozuk/şifrelenmiş veri** (fidye yazılımı) | ✘ | Bozulma kopyalanır |
| Yanlış uygulama sürümü veriyi bozdu | ✘ | Aynı |

Bu nedenle: **GRS "yedek" değildir.** Blob için ek koruma: *soft delete, versioning, point-in-time restore* (Storage özellikleri) ve Azure Backup (blob yedekleme). Bunlar da **veri koruma** katmanıdır; DR için ayrı değerlendirilir.

#### 3.5 Taşıma (migration) araçları ⭐

**Azure Migrate** — taşıma için **merkezi hub**: **discover → assess → migrate**.

| Aşama | Ne yapar |
|---|---|
| **Discover** | Kurum içi sunucuları/VM'leri/uygulamaları keşfeder (appliance ile) |
| **Assess** | Azure'a uygunluk, **boyutlandırma**, tahmini maliyet, bağımlılık analizi; *business case* |
| **Migrate** | Sunucuları (VMware, Hyper-V, fiziksel, diğer bulutlar), veritabanlarını, web uygulamalarını Azure'a taşır |

Taşıma stratejileri — **"R" modeli** (Cloud Adoption Framework'ün sınıflandırması; kaynağa göre 5 veya 6 R olarak anlatılır):

| Strateji | Ne yapılır | Efor | Kazanç |
|---|---|---|---|
| **Rehost** ("lift and shift") | Olduğu gibi Azure VM'e | Düşük | Hızlı çıkış; bulut faydaları sınırlı |
| **Replatform** *(bazı anlatımlarda)* | Küçük değişiklikle yönetilen servise (ör. SQL Server → Azure SQL MI) | Orta-düşük | Yönetim yükü azalır |
| **Refactor** | Kodu bulut servislerini kullanacak şekilde düzenle (ör. App Service) | Orta | PaaS faydaları |
| **Rearchitect** | Mimariyi değiştir (monolit → mikroservis) | Yüksek | Ölçek, çeviklik |
| **Rebuild** | Bulut-yerel sıfırdan yeniden yaz | Çok yüksek | Tam bulut-yerel |
| **Replace** | Hazır SaaS ile değiştir | Değişken | Yazılım bakımı biter |

> Sınav ayrıntı sormaz; "olduğu gibi taşı" → **rehost / lift-and-shift**, "SaaS'a geç" → **replace** bilmek yeter.

**Diğer taşıma servisleri:**

| Servis | Rol |
|---|---|
| **Azure Database Migration Service (DMS)** | Veritabanlarını (SQL Server, PostgreSQL, MySQL, … → Azure veritabanı servisleri) **çevrimiçi/çevrimdışı** taşır; Azure Migrate içinden de erişilir (→ C1.16) |
| **Azure Data Box** ailesi | **Fiziksel cihazla** çevrimdışı büyük veri taşıma (→ C1.8) |
| **Azure Resource Mover** *(bağlam)* | Azure kaynaklarını **bölgeler arası** (ve subscription/resource group arası) taşımak için orkestrasyon. Servisin güncel durumunu *Azure Updates*'ten doğrula; sınavda nadirdir |
| **Azure Arc** | Taşımadan **hibrit yönetim**: kurum içi/diğer bulut sunucularını Azure'dan yönetmek (→ [C1.13](#33-kaynak-yönetme-ve-dağıtma-araçları-portal-cli-powershell-arc-bicep)) |
| **AzCopy / Storage Explorer / File Sync** | Ağ üzerinden dosya/blob taşıma-senkron (→ C1.8) |

Taşıma, CAF'nin **Adopt → Migrate** aşamasıdır (→ [C1.22](#47-destek-planları-sla-ve-servis-yaşam-döngüsü)).

#### 3.6 Pratik — Recovery Services vault, VM yedekleme, soft delete (`az` CLI)

> **Maliyet uyarısı:** Lab küçük bir VM (B serisi) ve bir yedek kurtarma noktası oluşturur. Saatlik cüzi VM ücreti + korunan örnek/depolama ücreti doğar; **aynı gün temizle**. Bütçe alarmın (→ [C1.11](#31-maliyet-yönetimi-pricing-calculator-tco-budgets)) kurulu olsun. VM boyutu bölgede/abonelikte yoksa farklı bir küçük boyut seç.

```bash
RG=rg-az900-c119
LOC=westeurope
VAULT=rsv-az900-c119
VM=vm-az900-c119
az group create --name $RG --location $LOC

# 1) Vault: before protecting anything choose storage redundancy (default GRS; LRS is cheaper for a lab)
az backup vault create --resource-group $RG --name $VAULT --location $LOC
az backup vault backup-properties set --resource-group $RG --name $VAULT \
  --backup-storage-redundancy LocallyRedundant

# 2) A small VM to protect (use your own SSH key or --generate-ssh-keys)
az vm create --resource-group $RG --name $VM --image Ubuntu2204 --size Standard_B1s \
  --admin-username azureuser --generate-ssh-keys

# 3) Default policy: look at it (schedule + retention)
az backup policy show --resource-group $RG --vault-name $VAULT --name DefaultPolicy --output jsonc

# 4) Enable protection for the VM with the default policy
az backup protection enable-for-vm --resource-group $RG --vault-name $VAULT \
  --vm $VM --policy-name DefaultPolicy

# 5) Optional: trigger the first backup now (takes a while) and watch the job
az backup protection backup-now --resource-group $RG --vault-name $VAULT \
  --container-name $VM --item-name $VM \
  --backup-management-type AzureIaasVM --workload-type VM
az backup job list --resource-group $RG --vault-name $VAULT --output table
az backup recoverypoint list --resource-group $RG --vault-name $VAULT \
  --container-name $VM --item-name $VM \
  --backup-management-type AzureIaasVM --workload-type VM --output table

# 6) Soft delete behaviour: look at the vault's security settings (soft delete state, retention days)
az backup vault backup-properties show --resource-group $RG --name $VAULT --output jsonc
```

Beklenen gözlem: Yedek işi **Completed** olunca bir *recovery point* listelenir. Portalda *Recovery Services vault → Backup items → Azure Virtual Machine* ekranında korunan VM ve son yedek zamanı görünür; aynı ekrandan **Restore VM** (yeni VM / disk / tek tek dosya) seçilebilir. CLI parametre adları ve çıktı alanları sürümden sürüme değişebilir; takılırsan `az backup --help` ve portal aynı işi yapar.

**Temizlik — dikkat: korunan öğesi olan vault silinemez.**

```bash
# 7) Stop protection and delete the backup data (a vault with protected items or recovery points cannot be deleted)
az backup protection disable --resource-group $RG --vault-name $VAULT \
  --container-name $VM --item-name $VM \
  --backup-management-type AzureIaasVM --workload-type VM \
  --delete-backup-data true --yes

# 8) With soft delete ON the deleted backup stays in "soft deleted" state (14 days by default) and still blocks vault deletion.
#    For a throw-away lab vault: turn soft delete off BEFORE step 7 (not possible if "always-on"):
#    az backup vault backup-properties set --resource-group $RG --name $VAULT --soft-delete-feature-state Disable
#    Otherwise: portal → vault → Backup items → soft-deleted item → Undelete → Stop backup & delete data.

# 9) Cleanup (if the vault is empty the group delete succeeds)
az group delete --name $RG --yes --no-wait
```

> Bu cleanup sorunu aynı zamanda bir **özelliğin kanıtıdır**: soft delete ve "korumayı durdurmadan vault silinmez" kuralı, kazara/kötü niyetli silmeyi zorlaştırmak için vardır. Üretimde bunu **kapatmazsın**. Soft delete'in varsayılan durumu bölgeye/vault türüne göre değişebilir *(güncel durumu doğrula)*; laboratuvarda `Disable` komutu reddedilirse portaldan *Undelete → Stop backup* yolunu kullan.

---

### 4. Gerçek dünya senaryosu

Bir lojistik şirketi kurum içi iki VMware kümesinde 120 sunucu ve bir SQL Server çalıştırıyor; hedef: 9 ayda Azure'a geçmek ve felaket dayanıklılığını artırmak.

1. **Keşif ve değerlendirme:** **Azure Migrate** appliance'ı kurulur, 120 sunucu keşfedilir; bağımlılık analizi hangi sunucuların birlikte taşınması gerektiğini gösterir; boyutlandırma ve maliyet raporu (business case) yönetime sunulur.
2. **Strateji:** 90 sunucu **rehost** (lift-and-shift, Azure Migrate sunucu taşıma); SQL Server **Azure SQL Managed Instance**'a (**DMS** ile çevrimiçi taşıma, kısa kesinti); eski bir CRM **replace** edilip SaaS'a geçer; müşteri portalı **refactor** ile App Service'e alınır.
3. **Büyük arşiv:** 250 TB'lık taranmış belge arşivi 200 Mbps hatla ayları bulacağından **Data Box** ile taşınır (→ C1.8).
4. **Backup:** Taşınan VM'ler **Azure Backup** ile günlük yedeklenir; vault **immutable** ve soft delete açık; sorumlu yönetici hesapları MFA'lıdır. Bir mühendis yanlışlıkla test yerine **üretim** veritabanı VM'ini siler — yedekten saatler içinde geri döner (**RTO** ~ 3 sa, **RPO** ≤ 24 sa; iş birimi bunu onaylamıştı).
5. **DR:** Sipariş sistemi için **RPO 15 dk / RTO 1 sa** hedeflenir: West Europe'taki VM'ler **Site Recovery** ile North Europe'a replike edilir; *recovery plan* veritabanı → uygulama → web sırasını belirler; **her çeyrek test failover** yapılır. DR bölgesi, birincil bölgenin eşleşik bölgesi olarak planlanır (→ C1.4).
6. **Hibrit kalan parça:** Fabrikadaki iki yerel sunucu taşınamaz; **Azure Arc** ile Azure'a bağlanır, Policy ve Defender onlara da uygulanır (→ C1.13, C1.10).
7. **Ders:** Yönetim "storage zaten GRS, yedeğe gerek yok" demişti; tatbikatta bir blob kapsayıcısının toplu silinmesi **tüm kopyalara** yayıldı ve ancak blob soft delete / Backup ile geri alınabildi.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → servis eşleştirmesi (Backup / Site Recovery / Migrate / Data Box / DMS), RPO ve RTO tanımları, "redundancy yedek midir?" ve evet/hayır tabloları.

| Soru kalıbı | Doğru cevap | Tuzak |
|---|---|---|
| "Silinen/bozulan VM'i **dünkü haline** geri getir" | **Azure Backup** | Site Recovery, GRS |
| "Bölge arızasında VM'leri **başka bölgede çalıştır**, failover" | **Azure Site Recovery** | Azure Backup |
| "Kurum içi VMware/Hyper-V'yi Azure'a **DR için** replike et" | **Site Recovery** | Azure Migrate |
| "Kurum içi sunucuları **keşfet, değerlendir ve taşı**" | **Azure Migrate** | Data Box, Site Recovery |
| "Veritabanını (SQL Server → Azure SQL) **kesintiyi azaltarak** taşı" | **Database Migration Service** | Backup |
| "Çevrimdışı, onlarca/yüzlerce TB, ağ yetersiz" | **Azure Data Box** | AzCopy, Azure Migrate |
| "En fazla kabul edilen **veri kaybı** süresi" | **RPO** | RTO |
| "Hizmetin **geri gelme** süresi hedefi" | **RTO** | RPO |
| "GRS açık, o halde yedeğim var" | **Yanlış**: redundancy ≠ backup | |
| "Kodu değiştirmeden olduğu gibi Azure VM'e taşı" | **Rehost (lift-and-shift)** | Refactor, Rearchitect |
| "Kurum içi sunucuları Azure'dan **yönet**, taşımadan" | **Azure Arc** | Azure Migrate |
| "Yedeklerin ransomware ile silinmesini/değiştirilmesini engelle" | **Soft delete + immutable vault** | Yalnız GRS |
| "Aynı tarihe **geri dönülebilen** kurtarma noktaları" | **Azure Backup** (recovery point) | Site Recovery |

**Anahtar kelimeler:** *recovery point, retention, vault, restore* → Backup · *failover, failback, replicate, recovery plan, test failover* → Site Recovery · *discover, assess, migrate, lift and shift* → Azure Migrate · *offline, physical device, terabytes* → Data Box · *RPO = data loss, RTO = downtime*.

**Evet/hayır tuzakları:**
- "Azure Site Recovery, silinen bir dosyayı dünkü sürüme geri getirmek için en uygun araçtır" → **Hayır** (Backup).
- "GRS ile saklanan blob yanlışlıkla silinirse ikincil bölgeden geri alınabilir" → **Hayır** (silme kopyalara da uygulanır).
- "Azure Migrate yalnızca sunucuları değerlendirir, taşıma yapmaz" → **Hayır** (hub: discover + assess + migrate).
- "Azure Backup yalnızca Azure VM'lerini yedekler" → **Hayır** (SQL/SAP HANA in VM, Azure Files, Blob, disk, kurum içi dosyalar da).
- "RTO, kaybedilmesine izin verilen veri miktarıdır" → **Hayır** (o RPO).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "GRS/ZRS = yedek" | Redundancy **erişilebilirlik/dayanıklılık**; silme ve bozulmayı da kopyalar. Yedek için Backup (ve blob soft delete/versioning) |
| "Site Recovery ile yedek alırım" | ASR replikasyonu **DR içindir**; geçmişe dönüş yok. Yedek için Backup |
| Yedeği hiç **geri yüklemeyi denememek** | Test restore ve test failover tatbikatı yapmayan plan "inanç"tır |
| Vault'u **kaynakla aynı abonelik/yönetim sınırında**, ayrı koruma olmadan bırakmak | Soft delete, immutable vault, multi-user authorization, RBAC ile koru |
| Vault redundancy'sini korumaya başladıktan **sonra** değiştirmeyi denemek | Önce redundancy'yi seç; sonra kilitlenir (değişiklik koşulları sınırlıdır) |
| RPO/RTO'yu **iş birimiyle konuşmadan** teknik olarak seçmek | Hedefi iş verir, maliyet-fayda tartılır; "hepsi sıfır" çok pahalıdır |
| "Veriyi Azure'a taşıdım, iş bitti" | Taşıma + **yedek + DR + izleme** birlikte gelir |
| Büyük veriyi ağdan taşımaya inat etmek | Süreyi hesapla (C1.8: 40 TB / 100 Mbps ≈ 37 gün); gerekirse Data Box |
| Lab/deneme vault'larını **korumayı durdurmadan silmeye çalışmak** | Önce korumayı durdur ve yedek verisini sil (soft delete'e dikkat), sonra vault |
| Site Recovery'de failback'i **otomatik** sanmak | Reprotect + geri dönüş planlanan bir süreçtir |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Daha sık yedek (günde 1 → saatlik) | Daha küçük RPO | Daha çok depolama, işlem; her iş yükü desteklemez |
| Uzun retention (yıllık noktalar) | Uyumluluk, geriye dönük adli analiz | Depolama maliyeti (vault tier ile düşürülür) |
| Vault GRS + cross-region restore | Bölge kaybında yedekten bölgesel olmayan geri yükleme | LRS'ten pahalı |
| Immutable vault / always-on soft delete | Fidye yazılımına ve kötü niyetli silmeye karşı koruma | Hatalı politikayı geri almak zorlaşır; planlı olmayan silinemez |
| Site Recovery ile hot DR | Dakikalar mertebesinde RPO/RTO | Korunan örnek ücreti + hedef bölgede depolama; test failover'ın geçici hesaplama maliyeti |
| Backup-only (DR yok) | Ucuz; bölge felaketinde gün-hafta RTO | Bölge çökerse yeniden kurulum süresi uzun |
| Rehost (lift-and-shift) | Hızlı, düşük risk | PaaS faydaları (yönetim, ölçek) alınmaz; maliyet optimizasyonu sonraya kalır |
| Refactor/Rearchitect | Uzun vadeli maliyet ve çeviklik | Yüksek mühendislik eforu, uzun süre |
| Çevrimiçi DMS (minimal kesinti) | Kesinti dakikalar | Kurulum karmaşıklığı; çevrimdışıya göre daha çok izleme |
| Data Box | Ağ yükü yok, büyük veri hızlı | Kargo süresi, cihaz ücreti, sürekli senkron değil |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [What is the Azure Backup service?](https://learn.microsoft.com/en-us/azure/backup/backup-overview) · [Recovery Services vaults overview](https://learn.microsoft.com/en-us/azure/backup/backup-azure-recovery-services-vault-overview) · [Azure Backup support matrix](https://learn.microsoft.com/en-us/azure/backup/backup-support-matrix)
- [Soft delete for Azure Backup (enhanced soft delete)](https://learn.microsoft.com/en-us/azure/backup/backup-azure-enhanced-soft-delete-about) · [Immutable vault](https://learn.microsoft.com/en-us/azure/backup/backup-azure-immutable-vault-concept)
- [About Azure Site Recovery](https://learn.microsoft.com/en-us/azure/site-recovery/site-recovery-overview) · [Recovery plans](https://learn.microsoft.com/en-us/azure/site-recovery/recovery-plan-overview)
- [About Azure Migrate](https://learn.microsoft.com/en-us/azure/migrate/migrate-services-overview) · [Azure Database Migration Service](https://learn.microsoft.com/en-us/azure/dms/dms-overview) · [Azure Data Box overview](https://learn.microsoft.com/en-us/azure/databox/data-box-overview)
- [Cloud Adoption Framework — Migrate](https://learn.microsoft.com/en-us/azure/cloud-adoption-framework/migrate/) · [Azure Resource Mover overview](https://learn.microsoft.com/en-us/azure/resource-mover/overview) *(güncel durumu doğrula)*
- [Azure Well-Architected Framework — Reliability (RPO/RTO, backup & DR)](https://learn.microsoft.com/en-us/azure/well-architected/reliability/)
- [`az backup`](https://learn.microsoft.com/en-us/cli/azure/backup)


### 📝 Bölüm 4.4 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir geliştirici, üretim veritabanı VM'ini yanlışlıkla sildi. Şirket, VM'i **dünkü haline** geri getirebilmek istiyor. Hangi servis uygundur?

- A) Azure Site Recovery
- B) Azure Backup
- C) Azure Data Box
- D) Azure Load Balancer

<details><summary>Cevap</summary>

**B.** Geçmiş bir kurtarma noktasına dönmek **yedekleme** işidir (Azure Backup). Site Recovery sürekli replikasyonla çalışır; silme işlemi de karşıya yansıyacağından geçmişe dönüş sağlamaz.
</details>

#### Soru 2: Tek seçim
West Europe bölgesinin tamamen kullanılamaz hale gelmesi durumunda VM'lerin **North Europe'ta** çalıştırılabilmesi için hangi servis kullanılır?

- A) Azure Backup
- B) Azure Migrate
- C) Azure Site Recovery
- D) Azure Advisor

<details><summary>Cevap</summary>

**C.** Site Recovery, VM'leri ikincil bölgeye sürekli replike eder ve felakette **failover** sağlar.
</details>

#### Soru 3: Eşleştirme
Her ifadeyi **RPO** veya **RTO** ile eşleştirin.

1. "Felakette en fazla 10 dakikalık veri kaybını kabul edebiliriz."
2. "Hizmet felaketten sonra en geç 2 saatte yeniden çalışıyor olmalı."
3. "Son iyi yedek ile felaket anı arasındaki süre."
4. "Hedeflenen kesinti süresi."

<details><summary>Cevap</summary>

**1 → RPO · 2 → RTO · 3 → RPO · 4 → RTO.** RPO = kabul edilebilir **veri kaybı** (zaman cinsinden), RTO = kabul edilebilir **kesinti süresi**.
</details>

#### Soru 4: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Bir storage account GRS ile yapılandırıldıysa, bir blob'un yanlışlıkla silinmesi ikincil bölgedeki kopyadan geri alınabilir. | ☐ | ☐ |
| b) Site Recovery replikasyonu, bozulmuş verinin hedef bölgeye de kopyalanmasına yol açabilir. | ☐ | ☐ |
| c) Azure Backup yedekleri, kaynağından ayrı bir vault'ta saklanır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Silme ve bozulma işlemleri redundancy kopyalarına da uygulanır; redundancy yedek değildir. Replikasyon da bozulmayı taşıyabilir. Backup, ayrı bir vault'ta zaman içinde kurtarma noktaları tutar.
</details>

#### Soru 5: Tek seçim
Bir şirket kurum içi VMware sunucularını önce **keşfetmek**, Azure'a uygunluğunu ve maliyetini **değerlendirmek**, sonra da **taşımak** istiyor. Hangi servis bu üç adım için merkezi hub'dır?

- A) Azure Data Box
- B) Azure Site Recovery
- C) Azure Migrate
- D) Azure Arc

<details><summary>Cevap</summary>

**C.** Azure Migrate: discover → assess → migrate. Data Box yalnızca veri taşır; Site Recovery DR içindir; Arc taşımadan yönetim sağlar.
</details>

#### Soru 6: Tek seçim
Bir kurumun ofis hattı 100 Mbps. 500 TB'lık arşivi Azure'a **çevrimdışı** taşımak istiyor. En uygun seçenek hangisidir?

- A) AzCopy ile ağdan kopyalama
- B) Azure Data Box ailesi
- C) Azure Site Recovery
- D) Azure Backup

<details><summary>Cevap</summary>

**B.** Ağ yetersiz ve veri çok büyük: fiziksel cihaz (Data Box). 500 TB'ı 100 Mbps ile göndermek yıl mertebesinde sürer (→ C1.8).
</details>

#### Soru 7: Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** Azure Backup'ın yedekleyebildiği iş yüklerindendir?

- A) Azure VM'ler
- B) Azure Files paylaşımları
- C) Azure DNS bölge kayıtlarının canlı trafik yönlendirmesi
- D) Bir Application Gateway'in WAF kuralları (canlı trafikle birlikte)

<details><summary>Cevap</summary>

**A ve B.** Azure Backup VM'leri, VM içindeki SQL Server/SAP HANA'yı, Azure Files'ı, blob'ları, diskleri ve kurum içi dosyaları yedekler. C ve D yedekleme iş yükü değildir.
</details>

#### Soru 8: Eşleştirme
Her senaryoyu doğru servisle eşleştirin: **Azure Backup · Azure Site Recovery · Azure Migrate · Database Migration Service · Azure Data Box**

1. Kurum içi SQL Server veritabanını minimum kesintiyle Azure SQL Managed Instance'a taşımak
2. VM'lerin dünkü, geçen haftaki ve geçen ayki sürümlerine dönebilmek
3. Bir bölgedeki VM'leri sürekli başka bölgeye replike edip felakette failover yapmak
4. Yüzlerce sunucunun Azure'a uygunluğunu ve tahmini maliyetini çıkarmak
5. Ağ üzerinden aylar sürecek büyük veriyi kargo ile taşımak

<details><summary>Cevap</summary>

**1 → Database Migration Service · 2 → Azure Backup · 3 → Azure Site Recovery · 4 → Azure Migrate · 5 → Azure Data Box.**
</details>

#### Soru 9: Sıralama
Azure Migrate ile tipik bir taşıma akışını doğru sıraya koyun: **Taşı (migrate) · Keşfet (discover) · Değerlendir (assess)**

<details><summary>Cevap</summary>

**Keşfet → Değerlendir → Taşı.** Önce ortamın envanteri, sonra boyutlandırma/maliyet/uygunluk analizi, en son taşıma.
</details>

#### Soru 10: Tek seçim
"Uygulamayı kodunu değiştirmeden, olduğu gibi Azure VM'lere taşıyoruz." Bu taşıma stratejisinin adı nedir?

- A) Rebuild
- B) Replace
- C) Rearchitect
- D) Rehost (lift and shift)

<details><summary>Cevap</summary>

**D.** Rehost, değişiklik yapmadan taşımadır. Rebuild sıfırdan yazmak, Replace hazır SaaS'a geçmek, Rearchitect mimariyi değiştirmektir.
</details>

#### Soru 11: Senaryo
Bir finans şirketi, bir yöneticinin hesabı ele geçirilse bile saldırganın **yedeklerini silemeyeceği** (fidye yazılımı senaryosu) bir yapı istiyor. Aşağıdakilerden hangisi bu hedefe **en çok** hizmet eder?

- A) Storage account'ı LRS'ten GRS'e çevirmek
- B) Azure Backup vault'unda **soft delete** ve **immutable vault** özelliklerini etkinleştirmek
- C) VM'leri bir availability set'e koymak
- D) Azure Advisor önerilerini uygulamak

<details><summary>Cevap</summary>

**B.** Soft delete silinen yedeği süreli geri alınabilir tutar; immutable vault kurtarma noktalarının erken silinmesini/değiştirilmesini engeller. GRS (A) silmeyi de kopyalar; availability set (C) HA içindir.
</details>

#### Soru 12: Tek seçim
Bir ekip, Site Recovery için **DR tatbikatı** yapmak istiyor ama üretim iş yüklerini ve replikasyonu **etkilememesini** şart koşuyor. Hangi özellik uygundur?

- A) Failover'ı üretimde çalıştırmak
- B) Test failover
- C) Reprotect
- D) Soft delete

<details><summary>Cevap</summary>

**B.** Test failover, yalıtılmış bir ağda replika VM'i ayağa kaldırır; üretimi ve replikasyonu etkilemez.
</details>

#### Soru 13: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Arc, kurum içi sunucuları Azure'a taşımadan Azure'dan yönetmeyi sağlar. | ☐ | ☐ |
| b) RTO, felaket sonrası kaybedilmesine izin verilen veri miktarını belirtir. | ☐ | ☐ |
| c) Site Recovery, kurum içi Hyper-V VM'lerini Azure'a replike edebilir. | ☐ | ☐ |
| d) Bir Recovery Services vault'ta korunan VM'in yedek verileri silinmeden vault silinebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet · d) Hayır.** b RPO'nun tanımıdır (RTO kesinti süresidir). Vault'u silmeden önce korumayı durdurup yedek verisini silmek (ve soft delete'i hesaba katmak) gerekir.
</details>

#### Soru 14: Senaryo
Bir şirketin iş birimi şu hedefleri verdi: **RPO = 24 saat, RTO = 1 gün** olan bir raporlama VM'i ve **RPO = 15 dakika, RTO = 1 saat** olan bir sipariş sistemi. Her iş yükü için **en uygun** koruma yaklaşımı hangisidir?

- A) Her ikisi için de yalnızca GRS storage
- B) Raporlama VM'i → günlük Azure Backup; sipariş sistemi → Azure Site Recovery (artı yedek)
- C) Raporlama VM'i → Azure Site Recovery; sipariş sistemi → yalnızca günlük yedek
- D) Her ikisi için de Azure Data Box

<details><summary>Cevap</summary>

**B.** 24 saatlik RPO günlük yedekle karşılanır. 15 dakikalık RPO ve 1 saatlik RTO sürekli replikasyon ve hızlı failover (Site Recovery) gerektirir; geçmişe dönüş için yedek de eklenir. GRS yedek değildir; Data Box taşıma aracıdır.
</details>

---

**Puanlama:** 13-14 → hazırsın · 10-12 → README 3.1 (HA/Backup/DR tablosu) ve 3.4 (redundancy yedek değildir) bölümlerini tekrar et · < 10 → konuyu baştan çalış ve 3.6'daki laboratuvarı uygula.


---

## 4.5 AI, Machine Learning ve IoT Servisleri

> **Sınav Alanı:** Describe Azure architecture and services — genişletilmiş kapsam · **Konu Kodu:** C4.5

### 1. Ne?

Bu konu iki servis ailesini tanıtır; ikisi de "kendin kurmadan hazır kullan" mantığındadır:

| Aile | Soru | Servisler (özet) |
|---|---|---|
| **AI ve makine öğrenmesi** | "Metni/görüntüyü/sesi anlamak ya da kendi modelimi eğitmek istiyorum" | **Microsoft Foundry** (platform), **Foundry Tools / Azure AI services** (hazır API'ler), **Azure OpenAI / Foundry Models**, **Azure Machine Learning**, **Azure AI Search**, bot/agent servisleri |
| **IoT** | "Binlerce cihazı bağlayıp telemetri toplamak ve onlara komut göndermek istiyorum" | **IoT Hub**, **DPS**, **IoT Central**, **Digital Twins**, **IoT Edge** |

Ek olarak "diğer popüler servisler" listesinde **Azure Batch** (HPC), **Azure Local / Azure Stack** (hibrit) ve **Azure Communication Services** vardır (3.5).

> **Adlandırma uyarısı (Ekim 2026 itibarıyla):** Azure AI dünyasında adlar hızlı değişir. *Azure AI Studio* → *Azure AI Foundry* (Kasım 2024) → **Microsoft Foundry** (Kasım 2025, Ignite). Hazır API'ler *Cognitive Services* → *Azure AI services* → **Foundry Tools** olarak yeniden markalandı; Azure kaynak türü hâlâ `Microsoft.CognitiveServices/accounts`, CLI hâlâ `az cognitiveservices`. Eski sınav soruları ve kurslar eski adları kullanır. Sınavdan/mülakattan önce güncel adı Learn'den kontrol et.

Bu konunun C# kodu yoktur. Pratik, ücretsiz katmanda bir Language kaynağı ve REST çağrısıdır; isteğe bağlı olarak IoT Hub F1 (3.6).

---

### 2. Neden?

- **Sınav/kurs:** Eski AZ-900 sürümleri "popular products" olarak AI, IoT, Batch gibi servisleri sorardı; bugün bu servisler **seçeneklerde (distractor)** ve senaryo sorularında geçer: "hazır API mi, özel model mi?" ve "IoT Hub mı, Event Hubs mı?" klasiktir.
- **Gerçek işte:** AI/IoT projelerinin en sık hatası yanlış katmandan başlamaktır: metin dil tespiti için model eğitmek (gereksiz), ya da cihaz yönetimi için salt olay alımı servisi seçmek (yetersiz).
- **Kariyer yolu:** AI-900 → AI-102 → **AI-200 (C2)**. Burada ad dağarcığını edinirsin; geliştirici derinliği C2'dedir ve .NET tarafı M15'tedir (→ M15.4 embeddings & vector search).
- **Güvenlik (→ C1.18):** AI servis anahtarları ve cihaz bağlantı dizeleri **sırdır**; koda gömülmez.

---

### 3. Nasıl?

#### 3.1 AI servis katmanları ⭐

Üç katmanı ayır; çoğu soru "hangi katman?" sorusudur:

| Katman | Ne | Kimin işi | Örnek |
|---|---|---|---|
| **1. Hazır API'ler** (prebuilt) | Önceden eğitilmiş modeller, REST/SDK ile çağır | Model eğitmezsin; veri gönderir, sonuç alırsın | Vision, Speech, Language, Translator, Document Intelligence |
| **2. Temel/üretken modeller** | LLM'ler ve diğer foundation model'ler; prompt, RAG, agent | Model seçer, prompt/veri bağlarsın | **Azure OpenAI / Foundry Models** |
| **3. Özel ML** | Kendi verinle model **eğit**, dağıt, yönet (MLOps) | Veri bilimci/ML mühendisi | **Azure Machine Learning** |

**Microsoft Foundry** — AI uygulama ve agent geliştirme **platformu**: model kataloğu (OpenAI ve diğer sağlayıcıların modelleri), dağıtım, değerlendirme, agent'lar, güvenlik/içerik filtreleri ve hazır AI servislerine tek yerden erişim. (Eski adlarıyla Azure AI Foundry / AI Studio.)

**Hazır API'ler (Foundry Tools / Azure AI services):**

| Servis | Ne yapar | Tipik iş |
|---|---|---|
| **Vision** (Image Analysis, OCR) | Görüntüde nesne/metin/etiket bulma | Ürün fotoğrafı etiketleme, OCR |
| **Face** *(bağlam; erişim sınırlı)* | Yüz algılama | Kimlik doğrulama senaryoları — sorumlu AI kısıtları var |
| **Speech** | Ses ↔ metin, konuşma sentezi, çeviri | Çağrı merkezi transkripsiyonu |
| **Language** | Duygu analizi, dil tespiti, varlık çıkarma, özetleme, soru-cevap | Yorum analizi |
| **Translator** | Metin çevirisi | Çok dilli içerik |
| **Document Intelligence** (eski adı Form Recognizer) | Fatura/makbuz/form alanlarını çıkarma | Fatura işleme |
| **Content Safety** | Zararlı içerik tespiti | Kullanıcı içeriği moderasyonu |
| **Azure AI Search** (eski adı Cognitive Search) | Arama + vektör/semantik arama + enrichment; RAG'ın "R"si | Kurumsal doküman arama (→ M15.4) |

**Azure OpenAI / Foundry Models:** OpenAI modellerini (ve katalogdaki diğer modelleri) Azure'ın güvenlik, ağ (private endpoint) ve uyumluluk sınırları içinde kullanma; GPT sohbet, embeddings, görüntü/ses modelleri. Model adları/sürümleri sık değiştiği için sınav sürüm değil **"ne işe yarar"** sorar.

**Azure Machine Learning (Azure ML):** Kendi modelini **eğitmek** ve yönetmek: workspace, **AutoML** (algoritma/hiperparametre aramasını otomatikleştirir), **designer** (sürükle-bırak pipeline), notebook'lar, model kaydı, **managed endpoint**'ler, **MLOps** (versiyonlama, izleme, yeniden eğitim).

**Bot/agent servisleri (özet):** Konuşmalı uygulamalar için *Azure AI Bot Service* + *Bot Framework SDK* geleneksel yoldu. Bot Framework SDK **archive** edildi ve Aralık 2025 sonrası destek bileti kapsamı dışına çıktı *(doğrula)*; Microsoft yeni işler için **Microsoft 365 Agents SDK**, **Copilot Studio** (low-code) ve **Foundry Agent Service**'i öneriyor. QnA Maker 31 Mart 2025'te emekliye ayrıldı. Sınavda "chatbot" cevabı için eski ad "Azure Bot Service" hâlâ görülebilir.

**Hazır API ↔ özel model karar tablosu:**

| İhtiyaç | Seçim | Gerekçe |
|---|---|---|
| Fatura PDF'inden toplam ve tarihi çek | **Document Intelligence** | Hazır model var; eğitim gereksiz |
| Müşteri yorumlarında duygu analizi | **Language** | Hazır |
| Çağrı kayıtlarını yazıya dök | **Speech** | Hazır |
| Kendi şirket verinle, sektörüne özgü bir tahmin modeli (ör. arıza tahmini) | **Azure Machine Learning** | Özel veri + özel hedef |
| Şirket dokümanları üzerinde soru-cevap yapan sohbet asistanı | **Foundry Models/Azure OpenAI + AI Search** (RAG) | Üretken model + kendi verin |
| Binlerce görüntüde şirkete özgü kusur tespiti (hazır etiketler yetmiyor) | **Azure ML** veya özel görüntü modeli eğitimi | Özel sınıflar |

> Kural: **Hazır bir API sorunu çözüyorsa model eğitme.** Eğitim; veri, zaman, MLOps ve maliyet demektir. Özel model yalnızca hazır API'nin kapsamadığı yerde.

#### 3.2 Sorumlu AI ilkeleri *(bağlam — AI-900'de ağırlıklıdır)*

Microsoft'un altı ilkesi:

| İlke | Soru | Örnek |
|---|---|---|
| **Fairness** (adalet) | Sistem herkese eşit davranıyor mu? | Kredi modeli cinsiyete göre sapmıyor |
| **Reliability & safety** (güvenilirlik ve güvenlik) | Beklenen koşullarda ve uç durumlarda güvenle çalışıyor mu? | Otonom sistemde test, güvenli hata modu |
| **Privacy & security** (gizlilik ve güvenlik) | Kişisel veriyi koruyor mu? | Eğitim verisinde anonimleştirme, şifreleme |
| **Inclusiveness** (kapsayıcılık) | Engelli dahil herkesi dikkate alıyor mu? | Konuşma/altyazı, erişilebilirlik |
| **Transparency** (şeffaflık) | Sistemin nasıl karar verdiği anlaşılabilir mi? | "Bu karar şu faktörlere dayandı" açıklaması |
| **Accountability** (hesap verebilirlik) | Kim sorumlu? İnsan denetimi var mı? | Karar sürecinde insan onayı |

#### 3.3 Azure IoT servisleri ⭐

**Azure IoT Hub** — bulutta barındırılan, IoT cihazları ile bulut arasında **güvenli, çift yönlü** iletişim sağlayan **PaaS** servis.

| Özellik | Ne |
|---|---|
| **Device-to-cloud (D2C)** | Cihazdan bulutta telemetri alımı (MQTT, AMQP, HTTPS) |
| **Cloud-to-device (C2D)** | Buluttan cihaza mesaj, **direct method** (anlık komut), **configuration** |
| **Device identity registry** | Her cihazın kimliği ve anahtarı/sertifikası; tek tek iptal edilebilir |
| **Device twin** | Cihazın bulutta tutulan **JSON durum kopyası** (desired/reported properties, tag'ler); cihaz çevrimdışıyken bile istenen durum bırakılır |
| **Message routing** | Mesajları koşula göre Storage, Event Hubs, Service Bus'a yönlendirme; yerleşik Event Hubs uyumlu uç nokta |
| **Device Provisioning Service (DPS)** | Binlerce cihazı **sıfır dokunuşla** (zero-touch) doğru IoT Hub'a kaydeder, yük dağıtır |

**Diğer IoT servisleri:**

| Servis | Konum | Tek cümle |
|---|---|---|
| **IoT Central** | **SaaS** | Kod azıyla cihaz yönetimi + panolar + kurallar; IoT Hub'ın üstünde hazır uygulama. *(Şubat 2024'te yanlışlıkla "emekli oluyor" bildirimi çıktı; Microsoft bunu hatalı sistem mesajı olarak düzeltti. Güncel durumu Azure Updates'ten doğrula.)* |
| **Azure Digital Twins** | PaaS | Fiziksel ortamın (bina, fabrika) **dijital ikizini** grafik modelle (DTDL) kurma |
| **Azure IoT Edge** | Cihaz üstü çalışma zamanı | Bulut mantığını (container'lar) **cihazda/uçta** çalıştırma; çevrimdışı çalışma, gecikme azaltma |
| **Azure Sphere** *(bağlam)* | Güvenli MCU platformu | Duyuruya göre **31 Temmuz 2031'de** tüm güncelleme desteği biter (Azure Updates AZ557123 — doğrula); yeni tasarım için önerilmez |
| **Stream Analytics / Event Hubs** | Akan veri | Telemetriyi gerçek zamanlı işleme/alım (→ C1.16, 3.4) |

**Uçtan uca akış:**

```
 Device ──D2C──▶ IoT Hub ──routing──▶ Stream Analytics ──▶ Storage / Cosmos DB ──▶ Power BI
   ▲               │ ▲                                             (hot/cold path)
   │               │ └── device twin (desired/reported), identity registry
   └──C2D/direct───┘
 New devices ──▶ DPS ──▶ (assigns) IoT Hub          Edge devices ◀── IoT Edge modules
```

**IoT Hub ↔ Event Hubs** ⭐ (sık tuzak):

| | **IoT Hub** | **Event Hubs** |
|---|---|---|
| Yön | **Çift yönlü** (D2C + C2D) | Tek yönlü: yüksek hacimli **alım** (ingest) |
| Cihaz kimliği | **Cihaz başına** kimlik, iptal | Yok (paylaşımlı erişim anahtarları/SAS) |
| Cihaz yönetimi | Twin, direct methods, config | Yok |
| Kullanım | IoT cihaz filosu | Uygulama log'u/telemetrisi, clickstream, Kafka uyumlu alım (→ C1.21) |

> Pratik karar: cihazlara **komut gönderiyor / cihazları yönetiyor / cihaz kimliği** gerekiyorsa **IoT Hub**; yalnızca çok yüksek hacimli olay akışı alıyorsan **Event Hubs**. Gerçekte ikisi birlikte de kullanılır (IoT Hub'ın yerleşik uç noktası Event Hubs uyumludur).

#### 3.4 Akan veri ve analitik bağlantısı

IoT/uygulama telemetrisi **hot path** (anlık: Stream Analytics → alarm) ve **cold path** (toplu: Storage/Data Lake → analiz → Power BI) olarak ikiye ayrılır. Veri/analitik servisleri C1.16'dadır (Stream Analytics, Event Hubs, Data Lake, Synapse/Fabric).

#### 3.5 Diğer popüler servisler *(bağlam)*

| Servis | Ne | Tek cümle |
|---|---|---|
| **Azure Batch** | HPC / büyük ölçekli paralel iş | VM havuzlarını otomatik oluşturup işleri dağıtır; **Batch servisinin kendisi için ücret yok, kullanılan compute/depolama ücretli** *(doğrula)*; render, simülasyon, Monte Carlo |
| **Azure Local** (eski adı Azure Stack HCI) | Hibrit | Kurum içi donanımda Azure destekli altyapı (→ C1.1) |
| **Azure Stack Hub / Edge** | Hibrit/uç | Kurum içinde Azure benzeri hizmetler / uçta (edge) donanım cihazı. *Azure Stack ailesinin adları ve ürün durumu değişti; güncel kataloğu kontrol et* |
| **Azure Communication Services** | CPaaS | Uygulamaya **SMS, sesli/görüntülü arama, sohbet, e-posta** ekleme |
| **Azure Arc** | Hibrit yönetim | Kurum içi/başka bulut kaynaklarını Azure'dan yönetme (→ [C1.13](#33-kaynak-yönetme-ve-dağıtma-araçları-portal-cli-powershell-arc-bicep)) |

#### 3.6 Pratik — Language kaynağı (F0) ve tek REST çağrısı (`az` CLI)

Ücretsiz katman (F0), dil tespiti için fazlasıyla yeterlidir; F0 sınırlıdır (sınırlı aylık işlem, abonelik/bölge başına sınır) *(güncel kotayı doğrula)*. **Anahtarı koda veya depoya yazma** (→ C1.18); kabuk değişkeninde tut ve işin sonunda kaynağı sil. Bütçe alarmın (→ [C1.11](#31-maliyet-yönetimi-pricing-calculator-tco-budgets)) kurulu olsun.

```bash
RG=rg-az900-c120
LOC=westeurope
LANG_NAME=lang-az900-c120-$RANDOM
az group create --name $RG --location $LOC

# 1) Language resource, free tier (kind "TextAnalytics" = Language; F0 = free).
#    If a free resource of this kind already exists in the subscription the command fails - then use sku S0 briefly or skip.
az cognitiveservices account create --resource-group $RG --name $LANG_NAME \
  --kind TextAnalytics --sku F0 --location $LOC --yes

# 2) Endpoint and key (secret: keep it in a shell variable, never in source code)
ENDPOINT=$(az cognitiveservices account show --resource-group $RG --name $LANG_NAME \
  --query properties.endpoint --output tsv)
KEY=$(az cognitiveservices account keys list --resource-group $RG --name $LANG_NAME \
  --query key1 --output tsv)

# 3) One REST call: language detection (the exact API version/path may change; see the REST reference)
curl -s -X POST "${ENDPOINT}language/:analyze-text?api-version=2023-04-01" \
  -H "Ocp-Apim-Subscription-Key: $KEY" -H "Content-Type: application/json" \
  -d '{
        "kind": "LanguageDetection",
        "parameters": { "modelVersion": "latest" },
        "analysisInput": { "documents": [
          { "id": "1", "text": "Hello, how are you?" },
          { "id": "2", "text": "Bugun hava cok guzel." } ] }
      }'

# 4) Cleanup
az group delete --name $RG --yes --no-wait
```

Beklenen gözlem: Her doküman için `detectedLanguage` alanında `name`, `iso6391Name` (`en`, `tr`) ve bir `confidenceScore` döner. Uç nokta bölgesel veya özel alt alan adıyla farklı gelebilir; 401/404 alırsan Learn'deki güncel *Language REST* başlangıç sayfasına bak. **Gözlem:** hiç model eğitmedin; bu, "hazır API" katmanıdır.

**İsteğe bağlı — IoT Hub F1 + simüle cihaz** *(F1: abonelik başına **1 tane**; günlük mesaj kotası küçüktür, üçüncü taraf kaynaklar 8.000 mesaj/gün der — portaldaki Overview'dan doğrula; `azure-iot` CLI uzantısı gerekir)*:

```bash
RG=rg-az900-c120b
az group create --name $RG --location westeurope
az extension add --name azure-iot
HUB=hub-az900-c120-$RANDOM
az iot hub create --resource-group $RG --name $HUB --sku F1 --partition-count 2
az iot hub device-identity create --hub-name $HUB --device-id sim-device-01
az iot device simulate --hub-name $HUB --device-id sim-device-01 --msg-count 5   # device-to-cloud telemetry
az iot hub monitor-events --hub-name $HUB --timeout 30                           # read what the hub received
az group delete --name $RG --yes --no-wait
```

> `monitor-events` ve `device simulate` uzantı sürümüne göre değişebilir; çalışmazsa portalda *IoT Hub → Devices → Test* veya Device Explorer yolu aynı fikri gösterir.

---

### 4. Gerçek dünya senaryosu

Bir gıda üreticisi, üç fabrikadaki 4.000 soğutucu ve konveyör sensörüyle üretimi izlemek ve müşteri destek sürecini otomatikleştirmek istiyor.

1. **Cihaz tarafı:** Sensörler **IoT Hub**'a bağlanır; her birinin kimliği vardır. Yeni fabrika açılınca cihazlar **DPS** ile otomatik kayıt olur. Fabrikada internet kesilse bile **IoT Edge** modülleri yerelde eşik alarmını çalıştırır.
2. **Veri akışı:** Telemetri IoT Hub'dan **Stream Analytics**'e yönlenir; sıcaklık aşımı **hot path**'te saniyeler içinde alarm üretir, ham veri **Data Lake**'e (cold path) yazılır, **Power BI** panosunda üretim kalitesi izlenir. Bir soğutucu sorunlu çıkınca bulut, **direct method** ile cihaza "kompresör yeniden başlat" komutu yollar; **device twin**'de istenen eşik değeri güncellenir.
3. **Neden Event Hubs değil?** Cihaz kimliği, komut gönderme ve filo yönetimi gerektiğinden IoT Hub. (Aynı şirketin web sitesi click-stream'i ise Event Hubs'a gider.)
4. **Destek tarafı:** Müşteri e-postaları **Language** servisiyle duygu ve konu analizine girer; sipariş faturaları **Document Intelligence** ile okunur — **hiç model eğitilmeden**. Çağrı merkezi kayıtları **Speech** ile yazıya dökülür.
5. **Özel model:** Soğutucu arızalarını **7 gün önceden tahmin eden** model şirketin kendi geçmiş sensör verisine dayanır; hazır bir API bunu bilemez → **Azure Machine Learning** ile eğitilir, managed endpoint'ten sunulur, **MLOps** ile aylık yeniden eğitilir.
6. **Üretken AI:** Teknisyenlerin bakım kılavuzlarında soru sorabildiği asistan için **Foundry Models/Azure OpenAI + AI Search** (RAG) kullanılır; modele özel veri verilirken private endpoint ve Entra kimliği tercih edilir, anahtarlar **Key Vault**'ta tutulur.
7. **Sorumlu AI:** Arıza tahmin modelinin bir fabrikaya sistematik olarak daha çok yanlış alarm verdiği görülür (**fairness/reliability**); sonuçlar teknisyene **açıklanır** (**transparency**) ve üretim durdurma kararı **insan onayına** bağlanır (**accountability**).

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → servis eşleştirmesi (hazır API ↔ Azure ML; IoT Hub ↔ Event Hubs ↔ IoT Central) ve sorumlu AI ilkelerini tanımla eşleştirme.

| Soru kalıbı | Doğru cevap | Tuzak |
|---|---|---|
| "Önceden eğitilmiş görüntü/metin/konuşma API'si" | **Azure AI services (Foundry Tools)** | Azure Machine Learning |
| "**Kendi verinle** özel model eğit, dağıt, MLOps" | **Azure Machine Learning** | Language, Vision |
| "Fatura/formdan alan çıkar" | **Document Intelligence** | Vision (genel OCR) |
| "Konuşmayı yazıya çevir / sesi sentezle" | **Speech** | Language |
| "Metni başka dile çevir" | **Translator** | Language |
| "Dokümanlarda arama + vektör arama" | **Azure AI Search** | Cosmos DB |
| "Üretken AI modelleri (GPT), güvenli kurumsal kullanım" | **Azure OpenAI / Foundry Models** | Azure ML |
| "Cihaz-bulut **çift yönlü** iletişim, cihaz kimliği, device twin" | **IoT Hub** | Event Hubs |
| "Yüksek hacimli olay **alımı**, Kafka uyumu, cihaz yönetimi yok" | **Event Hubs** | IoT Hub |
| "Binlerce cihazı **otomatik** kaydet (zero-touch)" | **IoT Hub Device Provisioning Service** | IoT Central |
| "Kodsuz/low-code IoT **SaaS** uygulaması" | **IoT Central** | IoT Hub (PaaS) |
| "Fiziksel ortamın dijital ikizi/modeli" | **Azure Digital Twins** | IoT Hub |
| "Bulut mantığını cihazda/uçta çalıştır" | **IoT Edge** | Azure Arc |
| "Büyük ölçekli paralel/HPC iş" | **Azure Batch** | VM Scale Set |
| "Uygulamaya SMS/arama/sohbet ekle" | **Azure Communication Services** | Logic Apps |
| "Modelin kararlarını açıklayabilmek" | **Transparency** | Accountability |
| "Verilerin korunması" | **Privacy & security** | Fairness |
| "Herkes, engelliler dahil kullanabilsin" | **Inclusiveness** | Fairness |

**Anahtar kelimeler:** *prebuilt, pretrained, cognitive* → AI services · *train, custom model, MLOps, AutoML, designer* → Azure ML · *telemetry, bi-directional, device twin, provisioning* → IoT Hub/DPS · *ingest millions of events/s* → Event Hubs · *digital twin graph* → Digital Twins · *HPC* → Batch.

**Evet/hayır tuzakları:**
- "Dil tespiti için önce bir model eğitmen gerekir" → **Hayır** (hazır API).
- "Event Hubs ile cihaza komut gönderilebilir" → **Hayır** (C2D/direct method IoT Hub'da).
- "IoT Hub PaaS'tır; IoT Central SaaS'tır" → **Evet**.
- "Azure ML, yalnızca hazır eğitilmiş API'leri çağırmak içindir" → **Hayır** (kendi modelini eğitme/yönetme).
- "Sorumlu AI'da 'accountability', sistem sahiplerinin sonuçlardan sorumlu olması demektir" → **Evet**.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Hazır API yeterken model eğitmeye kalkmak | Önce hazır API (Language/Vision/Speech/Document Intelligence); özel model son çare |
| "Azure OpenAI = Azure ML" | Biri **hazır/foundation model kullanımı**, diğeri **kendi modelini eğitme ve yönetme**; karıştırılır |
| Servis adlarını ezberleyip güncel adı kontrol etmemek | Cognitive Services → AI services → Foundry Tools; AI Studio → AI Foundry → Microsoft Foundry. Eski kurslarda eski ad |
| IoT filosunu Event Hubs ile yönetmeye çalışmak | Cihaz kimliği, twin ve komut için IoT Hub |
| IoT Hub'ı cihaz kimlik bilgisini kodda/depoda tutarak kullanmak | DPS + sertifika/TPM; anahtarlar Key Vault'ta; bağlantı dizesi depoya girmez (→ C1.18) |
| AI servis anahtarını istemci tarafına (tarayıcı/mobil) gömmek | Anahtar sunucuda, tercihen **Entra kimliği/managed identity** ile; istemci doğrudan çağırmaz |
| F0/F1 ücretsiz katmanını üretimde kullanmak | Sınırlı kota, SLA yok/sınırlı; yalnızca deneme (→ [C1.22](#47-destek-planları-sla-ve-servis-yaşam-döngüsü)) |
| Sorumlu AI'yı "sadece hukuk işi" saymak | Tasarım ve test sürecine girer: veri dengesi, açıklanabilirlik, insan denetimi |
| Edge/Arc/Azure Stack'i aynı şey sanmak | Arc = yönetim düzlemi; IoT Edge = cihazda container çalıştırma; Azure Local/Stack = kurum içi altyapı |
| Deneme kaynağını silmeyi unutmak | Kaynak grubunu sil; IoT Hub'ın F1 kotası aboneliğe bir tane! |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Hazır API (AI services) | Dakikalar içinde sonuç, eğitim yok, sürekli iyileşen model | Genel amaçlı; alana özgü doğruluk sınırlı; çağrı başına ücret, veri bölgesi/gizlilik değerlendirmesi |
| Özel model (Azure ML) | Alana özgü doğruluk, tam kontrol | Veri toplama/etiketleme, MLOps, eğitim/çıkarım maliyeti, uzman gerekir |
| Foundation model + RAG | Yeniden eğitim olmadan şirket verisine dayalı yanıt | Token maliyeti, gecikme, halüsinasyon riski; içerik filtresi ve değerlendirme gerekir |
| IoT Hub (PaaS) | Güvenlik, ölçek, cihaz yönetimi hazır | Kurulum/tasarım, birim ve mesaj sayısına göre ücret |
| IoT Central (SaaS) | Hızlı başlangıç, kodsuz pano | Esneklik ve özelleştirme sınırlı; platform bağımlılığı |
| Edge işleme | Düşük gecikme, çevrimdışı çalışma, bant genişliği tasarrufu | Cihaz yönetimi/güncelleme karmaşıklığı |
| Hot path (Stream Analytics) | Saniyelik alarm | Sürekli çalışan işlem maliyeti |
| Cold path (Data Lake) | Ucuz uzun süreli depolama, geriye dönük analiz | Anlık değil |
| Ücretsiz katman | Maliyetsiz öğrenme | Kota, bölge/abonelik sınırı, üretime uygun değil |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Foundry documentation](https://learn.microsoft.com/en-us/azure/ai-foundry/) *(URL yapısı rebrand ile değişebilir; "Microsoft Foundry" araması)* · [Foundry Tools (Azure AI services) overview](https://learn.microsoft.com/en-us/azure/ai-services/what-are-ai-services)
- [Azure Machine Learning](https://learn.microsoft.com/en-us/azure/machine-learning/overview-what-is-azure-machine-learning) · [Azure AI Search](https://learn.microsoft.com/en-us/azure/search/search-what-is-azure-search)
- [Responsible AI principles](https://www.microsoft.com/en-us/ai/responsible-ai) · [Microsoft Learn — Fundamental principles of responsible AI](https://learn.microsoft.com/en-us/training/modules/get-started-ai-fundamentals/)
- [What is Azure IoT Hub?](https://learn.microsoft.com/en-us/azure/iot-hub/iot-concepts-and-iot-hub) · [IoT Hub quotas and throttling](https://learn.microsoft.com/en-us/azure/iot-hub/iot-hub-devguide-quotas-throttling) · [Device Provisioning Service](https://learn.microsoft.com/en-us/azure/iot-dps/about-iot-dps)
- [Azure IoT Central](https://learn.microsoft.com/en-us/azure/iot-central/core/overview-iot-central) · [Azure Digital Twins](https://learn.microsoft.com/en-us/azure/digital-twins/overview) · [Azure IoT Edge](https://learn.microsoft.com/en-us/azure/iot-edge/about-iot-edge)
- [Azure Updates — retirements (Azure Sphere, Bot Framework SDK vb.)](https://azure.microsoft.com/updates/) · [Azure Batch](https://learn.microsoft.com/en-us/azure/batch/batch-technical-overview) · [Azure Communication Services](https://learn.microsoft.com/en-us/azure/communication-services/overview)
- [`az cognitiveservices`](https://learn.microsoft.com/en-us/cli/azure/cognitiveservices) · [`az iot`](https://learn.microsoft.com/en-us/cli/azure/iot)


### 📝 Bölüm 4.5 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir e-ticaret şirketi, müşteri yorumlarının olumlu mu olumsuz mu olduğunu **kendi model eğitmeden** otomatik belirlemek istiyor. Hangi servis ailesi en uygundur?

- A) Azure Machine Learning ile sıfırdan model eğitimi
- B) Azure AI services / Foundry Tools içindeki Language servisi
- C) Azure IoT Hub
- D) Azure Batch

<details><summary>Cevap</summary>

**B.** Duygu analizi, Language servisinin hazır yeteneğidir. Hazır API sorunu çözüyorsa model eğitmek gereksizdir.
</details>

#### Soru 2: Tek seçim
Bir enerji şirketi, **kendi geçmiş sensör verisiyle** trafo arızalarını önceden tahmin eden özel bir model **eğitmek, dağıtmak ve izlemek** istiyor. Hangi servis uygundur?

- A) Azure Machine Learning
- B) Translator
- C) Azure Communication Services
- D) Event Hubs

<details><summary>Cevap</summary>

**A.** Özel model eğitimi ve MLOps Azure Machine Learning'in alanıdır; hazır API şirkete özgü arıza tahminini bilmez.
</details>

#### Soru 3: Eşleştirme
Her ihtiyacı servisle eşleştirin: **Speech · Translator · Document Intelligence · Azure AI Search · Vision**

1. Faturalardan toplam tutar ve tarih alanlarını çıkarmak
2. Çağrı merkezi kayıtlarını metne dökmek
3. Ürün fotoğraflarındaki nesneleri etiketlemek
4. Web sitesi metnini 20 dile çevirmek
5. Kurumsal dokümanlarda tam metin ve vektör araması yapmak

<details><summary>Cevap</summary>

**1 → Document Intelligence · 2 → Speech · 3 → Vision · 4 → Translator · 5 → Azure AI Search.**
</details>

#### Soru 4: Tek seçim
Microsoft'un AI geliştirme platformu için doğru ad ve rebrand sırası hangisidir (2026)?

- A) Azure Cognitive Studio → Azure Studio → Foundry
- B) Azure AI Studio → Azure AI Foundry → Microsoft Foundry
- C) Azure ML Studio → Azure AI Hub → Fabric AI
- D) Bot Studio → Copilot Studio → Foundry

<details><summary>Cevap</summary>

**B.** Azure AI Studio (2023) → Azure AI Foundry (Kasım 2024) → Microsoft Foundry (Kasım 2025). Hazır API'ler de Cognitive Services → Azure AI services → Foundry Tools olarak yeniden markalandı. Adlar sık değiştiği için güncel olanı Learn'den doğrula.
</details>

#### Soru 5: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Metin dilini tespit etmek için önce kendi eğitim verinle bir model eğitmen gerekir. | ☐ | ☐ |
| b) Azure OpenAI/Foundry Models, kendi modelini sıfırdan eğitme platformu olan Azure Machine Learning ile aynı şeydir. | ☐ | ☐ |
| c) AI servis anahtarı kaynak kodunda ve sürüm kontrolünde tutulmamalıdır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Hayır · c) Evet.** Dil tespiti hazır API'dir. Foundation model kullanımı ile özel model eğitimi farklı katmanlardır. Anahtarlar sırdır (Key Vault / managed identity — → C1.18).
</details>

#### Soru 6: Eşleştirme
Her tanımı sorumlu AI ilkesiyle eşleştirin: **Fairness · Reliability & safety · Privacy & security · Inclusiveness · Transparency · Accountability**

1. Modelin bir karara nasıl vardığı kullanıcılara açıklanabilir
2. Bir kredi modeli, benzer profildeki başvuranlara cinsiyetten bağımsız davranır
3. Sistemin sahipleri, sonuçlardan ve insan denetiminden sorumludur
4. Kişisel veriler eğitimde ve çıkarımda korunur
5. Sistem, engelli kullanıcılar dahil geniş bir kitle için tasarlanmıştır
6. Sistem, beklenmedik girdilerde bile güvenli ve tutarlı çalışır

<details><summary>Cevap</summary>

**1 → Transparency · 2 → Fairness · 3 → Accountability · 4 → Privacy & security · 5 → Inclusiveness · 6 → Reliability & safety.**
</details>

#### Soru 7: Tek seçim
Bir üretici, 10.000 sensörle **iki yönlü** iletişim kurmak (telemetri toplamak ve cihazlara komut göndermek), her cihaza **ayrı kimlik** vermek ve cihazın son durumunu bulutta bir JSON kopyası olarak tutmak istiyor. Hangi servis?

- A) Azure Event Hubs
- B) Azure Service Bus
- C) Azure IoT Hub
- D) Azure Stream Analytics

<details><summary>Cevap</summary>

**C.** IoT Hub: D2C + C2D, cihaz başına kimlik ve device twin. Event Hubs yüksek hacimli tek yönlü alımdır; cihaz yönetimi yoktur.
</details>

#### Soru 8: Eşleştirme
Her tanımı IoT servisiyle eşleştirin: **Device Provisioning Service · IoT Central · Azure Digital Twins · IoT Edge · Device twin**

1. Bir fabrikanın dijital ikizini grafik modelle kurmak
2. Bulut mantığını (container'lar) cihaz üzerinde, çevrimdışı çalışabilecek şekilde yürütmek
3. Binlerce yeni cihazı sıfır dokunuşla doğru IoT Hub'a kaydetmek
4. Cihazın desired/reported özelliklerini bulutta JSON olarak tutmak
5. IoT çözümünü SaaS olarak, az kodla panolar ve kurallarla işletmek

<details><summary>Cevap</summary>

**1 → Digital Twins · 2 → IoT Edge · 3 → Device Provisioning Service · 4 → Device twin · 5 → IoT Central.**
</details>

#### Soru 9: Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** IoT Hub ile Event Hubs arasındaki doğru farklardır?

- A) IoT Hub cihaza komut gönderebilir; Event Hubs gönderemez.
- B) Event Hubs cihaz başına kimlik ve device twin sunar; IoT Hub sunmaz.
- C) IoT Hub cihaz başına kimlik yönetimi sunar; Event Hubs yüksek hacimli olay alımına odaklanır.
- D) İkisi de yalnızca SaaS'tır.

<details><summary>Cevap</summary>

**A ve C.** B tam tersidir; D yanlıştır (ikisi de PaaS'tır; IoT Central SaaS'tır).
</details>

#### Soru 10: Senaryo
Bir şirket, telemetriyi iki yoldan işlemek istiyor: sıcaklık eşiği aşıldığında **saniyeler içinde alarm** ve aynı verinin **ucuz uzun süreli depolanıp** sonradan Power BI'da incelenmesi. Aşağıdaki akıştan hangisi doğrudur?

- A) Cihaz → IoT Hub → Stream Analytics (anlık alarm) + Data Lake (uzun süreli depolama) → Power BI
- B) Cihaz → Azure Batch → Active Directory
- C) Cihaz → Azure Backup → Site Recovery
- D) Cihaz → Azure DNS → Azure Policy

<details><summary>Cevap</summary>

**A.** Hot path (Stream Analytics) anlık işler; cold path (Data Lake) toplu analiz içindir; Power BI raporlar.
</details>

#### Soru 11: Tek seçim
Bir geliştirme ekibi, mobil uygulamalarına **SMS, görüntülü arama ve sohbet** özelliği eklemek istiyor. Hangi Azure servisi bunu sağlar?

- A) Azure Communication Services
- B) Azure Batch
- C) Azure Digital Twins
- D) Azure Machine Learning

<details><summary>Cevap</summary>

**A.** Communication Services; SMS, sesli/görüntülü arama, sohbet ve e-posta için CPaaS.
</details>

#### Soru 12: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) IoT Hub bir PaaS servistir; IoT Central ise SaaS yaklaşımıdır. | ☐ | ☐ |
| b) Azure Batch, büyük ölçekli paralel/HPC işleri çalıştırmak için kullanılır. | ☐ | ☐ |
| c) IoT Hub'ın F1 (ücretsiz) katmanı üretim için önerilir. | ☐ | ☐ |
| d) Event Hubs ile bir cihazın **desired** özelliğini güncelleyebilirsin. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır · d) Hayır.** Ücretsiz katman deneme içindir (abonelik başına bir tane, küçük kota). Desired property'ler device twin'in parçasıdır; IoT Hub'ın özelliğidir.
</details>

#### Soru 13: Senaryo
Bir bankanın veri bilimi ekibi, kendi iç dokümanları üzerinde soru-cevap yapan bir sohbet asistanı geliştiriyor. Modeli **sıfırdan eğitmek istemiyorlar**; hazır bir üretken modeli kendi verileriyle beslemek (arama sonuçlarını yanıta eklemek) istiyorlar. Aşağıdakilerden hangi kombinasyon bu yaklaşıma (RAG) en uygundur?

- A) Azure OpenAI / Foundry Models + Azure AI Search
- B) Azure Batch + Azure Local
- C) IoT Hub + Digital Twins
- D) Azure Backup + Site Recovery

<details><summary>Cevap</summary>

**A.** RAG'de arama servisi (AI Search) ilgili doküman parçalarını bulur; üretken model bunlarla yanıt üretir. Model eğitmeye gerek yoktur (→ M15.4).
</details>

---

**Puanlama:** 12-13 → hazırsın · 9-11 → README 3.1 (üç katman, karar tablosu) ve 3.3 (IoT Hub ↔ Event Hubs) bölümlerini tekrar et · < 9 → konuyu baştan çalış ve 3.6'daki laboratuvarı uygula.


---

## 4.6 Entegrasyon, Mesajlaşma ve DevOps

> **Sınav Alanı:** Describe Azure architecture and services — genişletilmiş kapsam · **Konu Kodu:** C4.6

### 1. Ne?

Uygulamalar tek başına çalışmaz; birbirleriyle ve dış sistemlerle konuşur. Azure bunun için bir **entegrasyon** araç kutusu sunar; kodu üretime taşımak için de **DevOps** araçları vardır:

| Alan | Soru | Servisler |
|---|---|---|
| **İş akışı / otomasyon** | "A olunca B'yi yap" — kodsuz/az kodlu | **Logic Apps** (Functions ↔ Logic Apps → C1.6) |
| **Olay yönlendirme** | "Bir şey **oldu**, ilgilenenleri haberdar et" | **Event Grid** |
| **Kurumsal mesajlaşma** | "Bu işi **güvenle** teslim et, kaybetme, sıraya koy" | **Service Bus** (kuyruk/topic) |
| **Olay/telemetri akışı** | "Saniyede milyonlarca olayı al" | **Event Hubs** |
| **Basit kuyruk** | "Ucuz, basit bir kuyruk yeter" | **Storage Queue** |
| **API yönetimi** | "API'lerimi güvenli ve yönetilebilir yayınla" | **API Management** |
| **DevOps** | "Kodu planla, derle, test et, yayınla" | **Azure DevOps** ↔ **GitHub**, **Pipelines/Actions**, **DevTest Labs**, **Deployment Environments**, **azd** |

Bu konunun C# kodu yoktur. Pratik: `az` CLI ile bir Service Bus kuyruğu, REST ile mesaj gönder/al (3.6) ve bir GitHub Actions workflow'unu okuma alıştırması (3.5).

---

### 2. Neden?

- **Sınav/kurs:** "Olay mı, mesaj mı?" ve "Event Grid / Service Bus / Event Hubs hangisi?" sorusu kurs sınavlarının klasiğidir; seçeneklerde (distractor) AZ-900 senaryo sorularında da görünür. API Management ve Logic Apps "ürün adı → görev" eşleştirme sorularında çıkar.
- **Gerçek işte:** Dağıtık sistemlerde en pahalı hata **yanlış mesajlaşma aracı seçmektir**: sipariş gibi kaybolmaması gereken bir komutu Event Grid ile yollamak (teslimat garantisi ve sıra yok), ya da basit bir arka plan işi için Service Bus Premium ödemek.
- **Mülakat:** Mesajlaşma desenleri (kuyruk, pub/sub, outbox, idempotency) M12'nin konusudur; burada araç envanterini ve seçim mantığını öğrenirsin.
- **Güvenlik (→ C1.18):** Bu servislerde **bağlantı dizesi/SAS anahtarı yerine Entra ID + managed identity** tercih edilir; CI/CD'de kalıcı sır yerine **OIDC** kullanılır (3.5).

---

### 3. Nasıl?

#### 3.1 Logic Apps ve entegrasyon iş akışları

**Azure Logic Apps**, **kod yazmadan/az kodla** görsel tasarımcıda iş akışı kuran, **1000'den fazla connector** (Microsoft 365, Salesforce, SAP, SQL, Service Bus, HTTP…) bulunan bir PaaS entegrasyon servisidir. Yapı: **trigger** (iş akışını başlatan olay) + **action**'lar (adımlar) + koşul/döngü.

| | Logic Apps | Azure Functions |
|---|---|---|
| Yaklaşım | Görsel tasarımcı, hazır connector'lar | Kod (C#, JS, Python…) |
| Güçlü yanı | SaaS/sistemler arası entegrasyon, hızlı kurulum, onay akışları | Özel iş mantığı, algoritma, tam kontrol |
| Faturalama (özet) | **Consumption:** çalışan eylem başına; **Standard:** ayrılmış kapasite | Consumption/Flex Consumption: yürütme + süre; Premium/Dedicated |
| Tipik örnek | "Yeni e-posta eki gelince Blob'a kaydet, Teams'e haber ver" | "Gelen siparişin vergisini hesapla" |

Birlikte kullanılırlar: Logic Apps iş akışı bir Function'ı eylem olarak çağırır.

#### 3.2 Olay mı, mesaj mı? ⭐

Bu ayrım üç servisin seçimini belirler:

| | **Olay (event)** | **Mesaj (message)** |
|---|---|---|
| Anlamı | "**Bir şey oldu**" bildirimi (durum değişikliği, **gerçek**) | Bir sisteme verilen **veri/komut/iş** (ör. "bu siparişi işle") |
| Yayıncının beklentisi | Kim dinlerse dinlesin; sonucuyla ilgilenmez | Tüketicinin **işlemesini bekler** (teslim garantisi önemli) |
| İçerik | Genelde küçük: ne oldu + nerede | Asıl iş verisi (sipariş, ödeme) |
| Ana ihtiyaç | Reaktif yönlendirme, fan-out | Güvenilir teslim, sıra, tekrar deneme, dead-letter |
| Servis | **Event Grid** (ayrık olaylar), **Event Hubs** (olay **akışı**) | **Service Bus**, Storage Queue |

Mnemonik: **Event Grid = "bir şey oldu, haber ver" · Service Bus = "bu işi güvenle işle" · Event Hubs = "milyonlarca olayı al, akışı sakla/oku".**

#### 3.3 Servisler ⭐

**Event Grid** — tam yönetilen **olay yönlendirme** (pub/sub, **push**): kaynak olayı yayınlar, abonelere HTTP/webhook, Function, Logic App, Service Bus, Event Hubs vb. olarak iletir.

- **Kaynaklar:** Azure servisleri (**system topic** — ör. blob oluşturuldu, kaynak değişti, Key Vault sırrı süreleniyor), kendi uygulamaların (**custom topic**), partner olayları.
- **Filtreleme** (olay türü/konu), **retry** ve dead-lettering; en az bir kez teslim (at-least-once). Olay başına/işlem başına ücretlendirilir; aylık küçük bir ücretsiz işlem kotası vardır *(doğrula)*.
- Ek: Event Grid *namespace*'leri **MQTT** ve **pull** teslimi de destekler *(bağlam; IoT/MQTT senaryoları → C1.20)*.
- Tipik: "Blob yüklenince thumbnail Function'ı çalıştır", "Resource group'ta VM oluşunca Logic App uyarı yollasın".

**Service Bus** — kurumsal **mesaj aracısı (broker)**.

| Kavram | Anlamı |
|---|---|
| **Queue** | Noktadan noktaya; mesajı **tek** tüketici işler |
| **Topic + subscription** | Yayınla/abone ol (pub/sub); her abonelik mesajın kendi kopyasını alır, **filtre** kuralları olabilir |
| **Dead-letter queue (DLQ)** | İşlenemeyen/süresi dolan/çok denenen mesajların park edildiği alt kuyruk |
| **Sessions** | Aynı oturumdaki mesajlar **sıralı (FIFO)** ve tek tüketicide |
| **Peek-lock** | Mesaj kilitlenir; işlenince `complete`, olmazsa tekrar kuyruğa döner (**at-least-once**) → tüketici **idempotent** olmalı (→ M12) |
| Diğer | Duplicate detection, scheduled delivery, transactions, AMQP/JMS |
| **Katmanlar** | **Basic** (yalnızca kuyruk), **Standard** (topic dahil), **Premium** (izole kapasite, büyük mesaj, private endpoint/VNet, geo-DR) — özellikler katmana bağlı |

**Event Hubs** — yüksek hacimli **olay/telemetri alımı** (big data streaming): **partition**'lar, **consumer group**'lar, **Capture** (Blob/Data Lake'e otomatik yazma), **Kafka uyumlu** uç nokta; saniyede milyonlarca olay. Mesaj/komut değil **akış**; olaylar saklama süresi boyunca **yeniden okunabilir**. IoT Hub ile farkı → C1.20 (3.3).

**Storage Queue** — Storage hesabındaki **basit, ucuz** kuyruk; REST/HTTP, çok büyük kuyruk kapasitesi, ama topic yok, **garanti edilen FIFO yok**, DLQ/session gibi gelişmiş özellikler yok; mesaj boyutu küçük (kabaca 64 KB — doğrula). "Yalnızca basit bir arka plan işi kuyruğu" için yeterlidir.

**API Management (APIM)** — arka uç API'lerinin önünde duran **API ağ geçidi**:

| Yetenek | Örnek |
|---|---|
| **Güvenlik/kimlik** | JWT doğrulama, subscription key, IP kısıtı, OAuth |
| **Politikalar** | Rate limit, **kota**, dönüşüm (XML↔JSON, header ekle/sil), cache |
| **Yayınlama** | **Geliştirici portalı**, API dokümantasyonu, sürümleme |
| **Gözlem** | Analitik, log, Monitor entegrasyonu |
| **Birleştirme** | Birden çok arka ucu (App Service, Function, on-prem) tek API yüzeyinde toplamak |

> APIM bir **mesajlaşma** servisi değildir; HTTP API'leri yönetir. WAF/Front Door gibi ağ güvenlik servisleri (→ C1.17/C1.18) ile birlikte kullanılabilir.

**Seçim tablosu — olay ↔ mesaj** ⭐ *(→ C2.9, C2.10, M12)*

| İhtiyaç | Servis |
|---|---|
| Bir Azure kaynağında olay olunca (blob oluştu) başka bir şeyi **tetikle** | **Event Grid** |
| Sipariş komutlarını **kaybetmeden, sırayla, tekrar denemeyle** işle; DLQ lazım | **Service Bus** (queue) |
| Bir yayını **birden çok sisteme** ilet, her biri kendi filtresi ile | **Service Bus topic** (güvenilir) veya Event Grid (hafif, olay) |
| Saniyede **yüz binlerce** telemetri/log olayı al, sonra analiz et, Kafka uyumu | **Event Hubs** |
| Ucuz, basit, çok büyük kuyruk; gelişmiş özellik gerekmez | **Storage Queue** |
| Kodsuz iş akışı: yeni e-posta → CRM kaydı → Teams bildirimi | **Logic Apps** |
| Dış partnerlere REST API sun, kota ve anahtar uygula | **API Management** |

#### 3.4 Azure DevOps ↔ GitHub

| | **Azure DevOps** | **GitHub** |
|---|---|---|
| Planlama | **Boards** (iş öğeleri, backlog, sprint) | Issues, Projects |
| Kod deposu | **Repos** (Git) | Repositories (Git) — açık kaynak ekosistemin merkezi |
| CI/CD | **Pipelines** (YAML veya klasik) | **Actions** (YAML workflow'lar, marketplace) |
| Test | **Test Plans** (manuel/keşif testi) | Actions içinde otomatik test (ayrı manuel test aracı yok) |
| Paket | **Artifacts** (NuGet, npm, Maven…) | **Packages** |
| Geliştirici ortamı | — | **Codespaces** (bulutta geliştirme ortamı) |
| AI yardımcı | — | **GitHub Copilot** |
| Güvenlik | — | Advanced Security (kod tarama, secret scanning, Dependabot) |

> Her ikisi de Microsoft'a aittir; çoğu kurum ikisini de kullanır (ör. kod GitHub'da, iş takibi Boards'ta). Yeni projelerde GitHub ağırlıkla öne çıkar; sınav ve kurslar ikisini de **"DevOps araç seti"** olarak sorar.

**CI/CD kavramları:** **CI** (continuous integration) — her commit'te derle + test et; **CD** (continuous delivery/deployment) — doğrulanmış çıktıyı ortamlara (dev → test → prod) otomatik/onaylı yayınla. Çıktı bir **artifact**'tır (derlenmiş paket/container image).

**Ek servisler:**

| Servis | Rol |
|---|---|
| **Azure DevTest Labs** | Geliştirme/test ortamlarını **maliyet kontrolüyle** kurma: otomatik kapatma/başlatma, VM sayısı/boyutu politikaları, harcama eşiği, hazır **formül/şablonlar**, "claimable" VM'ler |
| **Azure Deployment Environments** | Platform ekibi onaylı **IaC şablonları** (katalog) sunar; geliştirici tek tıkla standartlara uygun ortam alır (self-service) |
| **Azure Developer CLI (`azd`)** *(bağlam)* | `azd init / up / down` ile **uygulama + altyapıyı** (Bicep) tek komutla sağlama ve dağıtma; CI/CD yapılandırması da üretir |
| **IaC (→ C1.13)** | ARM/Bicep/Terraform: ortam kod olarak; pipeline'ın dağıtım adımı |

#### 3.5 CI/CD akışı ve OIDC (workload identity federation) *(bağlam)*

Pipeline'ın Azure'a dağıtım yapabilmesi için bir kimliğe ihtiyacı vardır. **Kötü yol:** service principal **secret'ını** depoya/GitHub secret'ına koymak — süresi dolar, sızabilir, rotasyon gerektirir. **İyi yol:** **workload identity federation (OIDC)**: Entra ID'de bir uygulama kaydına/managed identity'ye *federated credential* tanımlanır ("şu repo, şu branch/environment'tan gelen GitHub token'ına güven"); workflow kısa ömürlü bir token takas eder, **kalıcı sır yoktur**.

**GitHub Actions workflow — oku ve yorumla** (çalıştırma yok; kimlik değerleri secret değil *yapılandırma*dır):

```yaml
name: ci-cd

on:
  push:
    branches: [ main ]
  workflow_dispatch:

permissions:
  id-token: write      # allows the job to request an OIDC token
  contents: read

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: 10.0.x
      - run: dotnet build --configuration Release
      - run: dotnet test --configuration Release --no-build
      - run: dotnet publish --configuration Release --output ./publish
      - uses: actions/upload-artifact@v4
        with:
          name: webapp
          path: ./publish

  deploy:
    needs: build
    runs-on: ubuntu-latest
    environment: production        # approval rules can be attached to the environment
    steps:
      - uses: actions/download-artifact@v4
        with:
          name: webapp
          path: ./publish
      - uses: azure/login@v2        # exchanges the GitHub OIDC token for an Azure token (no password)
        with:
          client-id: ${{ secrets.AZURE_CLIENT_ID }}
          tenant-id: ${{ secrets.AZURE_TENANT_ID }}
          subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
      - uses: azure/webapps-deploy@v3
        with:
          app-name: app-az900-demo
          package: ./publish
```

> **Alıştırma.** (1) Hangi satır CI, hangisi CD'dir? (2) `needs: build` ne sağlar? (3) `permissions: id-token: write` neden gerekli? (4) Depoda hangi kalıcı **sır** yoktur? (5) Üretime onay kuralı nereye eklenir?
>
> <details><summary>Çözüm</summary>
>
> 1. `build` job'u **CI** (derle, test et, paketle); `deploy` job'u **CD** (yayınla). 2. `deploy`, `build` başarılı olmadan çalışmaz — sıra ve bağımlılık. 3. Job'un GitHub'dan OIDC token isteyebilmesi için; `azure/login` bu token'ı Azure token'ıyla takas eder. 4. Parola/secret yok — yalnızca kimlik tanımlayıcıları (client/tenant/subscription id) var; güven Entra'daki federated credential'dadır. 5. GitHub'da `production` **environment**'ına *required reviewers* kuralı ve federated credential'ın subject'ini o environment'a daraltma.
> </details>

#### 3.6 Pratik — Service Bus kuyruğu, REST ile gönder/al (`az` CLI)

> **Maliyet:** *Basic* katman çok düşük (işlem başına) ücretlidir; kuyruk dışında kaynak yoktur. Bütçe alarmın (→ [C1.11](#31-maliyet-yönetimi-pricing-calculator-tco-budgets)) kurulu olsun, işin sonunda grubu sil. `az servicebus` komutları **yönetim düzlemini** (namespace/kuyruk oluşturma) yapar; **mesaj gönder/al** işini CLI doğrudan yapmaz — burada **Entra token + REST** ile yapıyoruz (SAS anahtarı kullanmadan).

```bash
RG=rg-az900-c121
LOC=westeurope
NS=sbns-az900-c121-$RANDOM
Q=orders
az group create --name $RG --location $LOC

# 1) Namespace (Basic = queues only; topics need Standard) and a queue
az servicebus namespace create --resource-group $RG --name $NS --location $LOC --sku Basic
az servicebus queue create --resource-group $RG --namespace-name $NS --name $Q

# 2) Data-plane role for yourself (Entra ID instead of a shared key). Propagation can take a few minutes.
NS_ID=$(az servicebus namespace show --resource-group $RG --name $NS --query id --output tsv)
ME=$(az ad signed-in-user show --query id --output tsv)
az role assignment create --assignee-object-id "$ME" --assignee-principal-type User \
  --role "Azure Service Bus Data Owner" --scope "$NS_ID"

# 3) Token for the Service Bus data plane
TOKEN=$(az account get-access-token --resource https://servicebus.azure.net --query accessToken --output tsv)

# 4) Send two messages (REST: POST <queue>/messages)
for N in 1 2; do
  curl -s -o /dev/null -w "send $N -> HTTP %{http_code}\n" -X POST \
    "https://$NS.servicebus.windows.net/$Q/messages" \
    -H "Authorization: Bearer $TOKEN" -H "Content-Type: text/plain" \
    --data "order-$N created"
done

# 5) Look at the counters (active messages = 2)
az servicebus queue show --resource-group $RG --namespace-name $NS --name $Q --query countDetails

# 6) Receive-and-delete (REST: DELETE <queue>/messages/head) - twice; the third call returns HTTP 204 (empty)
for N in 1 2 3; do
  curl -s -w "\n-> HTTP %{http_code}\n" -X DELETE \
    "https://$NS.servicebus.windows.net/$Q/messages/head?timeout=10" \
    -H "Authorization: Bearer $TOKEN"
done

# 7) Cleanup
az group delete --name $RG --yes --no-wait
```

Beklenen gözlem: Gönderimler **201**; `countDetails.activeMessageCount` = 2; ilk iki `DELETE` mesaj gövdesini (`order-1 created`, `order-2 created`) döndürür ve mesaj **silinir** (destructive read), üçüncü çağrı **204** (kuyruk boş). 401/403 alırsan rol atamasının yayılmasını birkaç dakika bekle ve token'ı yenile. İstersen portalda *Service Bus Explorer* ile aynı mesajları **peek-lock** ile okuyup `complete`/`dead-letter` deneyebilirsin. *(REST yolları ve başlıklar API sürümüne göre değişebilir; güncel başvuru Learn'de.)*

**Karar tablosu alıştırması:** Her senaryo için servisi seç → **Event Grid · Service Bus · Event Hubs · Storage Queue · Logic Apps · API Management**.

| # | Senaryo |
|---|---|
| 1 | Ödeme komutları sırayla, tekrar denemeli ve dead-letter'lı işlenecek |
| 2 | Bir blob yüklendiğinde 3 farklı sistem haberdar edilecek (hafif olay) |
| 3 | Mobil uygulamalardan saniyede 500.000 kullanım olayı alınıp analiz hattına akıtılacak |
| 4 | Basit bir arka plan işi için en ucuz kuyruk yeterli |
| 5 | Yeni müşteri formu → CRM + e-posta + Teams bildirimi, kodsuz |
| 6 | Partner firmalara anahtar ve günlük kota ile REST API açılacak |

<details><summary>Çözüm</summary>

1 → **Service Bus** (queue) · 2 → **Event Grid** · 3 → **Event Hubs** · 4 → **Storage Queue** · 5 → **Logic Apps** · 6 → **API Management**.
</details>

---

### 4. Gerçek dünya senaryosu

Bir perakende şirketi, mağaza ve web siparişlerini işleyen sistemini modernleştiriyor.

1. **Sipariş alma:** Web ve mobil istemciler REST API'ye gelir; API'ler **API Management** arkasındadır (JWT doğrulama, rate limit, dev portalı). Sipariş **Service Bus topic**'ine yazılır — komut kaybolmamalıdır; **stok**, **ödeme**, **kargo** servisleri ayrı abonelikle işler. İşlenemeyen sipariş **dead-letter**'a düşer, operasyon ekibi inceler.
2. **Olaylar:** Fatura PDF'i Blob'a yüklendiğinde **Event Grid** bir Function'ı tetikler (OCR/arşiv) ve bir Logic App'e haber verir — hafif, reaktif.
3. **Telemetri:** Mağaza kasalarının ve web sitesinin kullanım olayları **Event Hubs**'a akar; **Capture** ham veriyi Data Lake'e yazar; Stream Analytics anlık pano besler (→ C1.16, C1.20).
4. **İş akışı:** "Büyük kurumsal sipariş → satış müdürü onayı → CRM kaydı → Teams bildirimi" **Logic Apps** ile az kodla kurulur; özel fiyat hesabı ise bir **Azure Function**'dır.
5. **DevOps:** Kod **GitHub**'da, iş takibi **Azure Boards**'ta. **GitHub Actions** workflow'u derler, test eder, paketi **OIDC** ile (kalıcı sır olmadan) staging'e dağıtır; production için **environment onayı** gerekir. Altyapı Bicep ile kod olarak tutulur (→ C1.13).
6. **Maliyet:** Geliştirici test ortamları **DevTest Labs**'te: gece otomatik kapanma ve VM sayısı politikası; platform ekibi **Deployment Environments** ile standart ortam şablonları sunar.
7. **Ders:** İlk tasarımda siparişler **Event Grid** ile akıyordu; bir tüketici kapalıyken olaylar tükenip kaybolduğunda (retry süresi doldu) siparişler düştü. Komut/iş verisi **Service Bus**'a taşındı; Event Grid yalnızca "olay oldu" bildirimlerinde kaldı.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → servis eşleştirmesi (olay/mesaj/akış), servis → görev eşleştirmesi (APIM, Logic Apps), DevOps araç eşleştirmesi (Boards, Repos, Pipelines, Test Plans, Artifacts; Actions, Codespaces, Copilot).

| Soru kalıbı | Doğru cevap | Tuzak |
|---|---|---|
| "Olay oldu, **abonelere ilet** (reaktif, hafif)" | **Event Grid** | Service Bus |
| "**Kurumsal mesajlaşma**, kuyruk/topic, dead-letter, sıra, transaction" | **Service Bus** | Storage Queue |
| "Saniyede **milyonlarca** olay/telemetri, **Kafka** uyumu" | **Event Hubs** | Service Bus |
| "Basit ve **ucuz** kuyruk" | **Storage Queue** | Service Bus |
| "Kodsuz iş akışı, **1000+ connector**" | **Logic Apps** | Functions |
| "**Kod** ile olay tetiklemeli çalıştır" | **Azure Functions** | Logic Apps |
| "API'lere kota, anahtar, dönüşüm, **geliştirici portalı**" | **API Management** | Application Gateway |
| "Backlog, sprint, iş öğesi" | **Azure Boards** | Repos |
| "Git deposu" | **Azure Repos / GitHub** | Artifacts |
| "CI/CD YAML pipeline" | **Azure Pipelines / GitHub Actions** | Test Plans |
| "Manuel/keşif test planı" | **Azure Test Plans** | Pipelines |
| "NuGet/npm paket akışı" | **Azure Artifacts** | Repos |
| "Bulutta geliştirme ortamı (tarayıcıdan VS Code)" | **GitHub Codespaces** | DevTest Labs |
| "Dev/test VM'lerini otomatik kapatıp **maliyeti kontrol et**" | **Azure DevTest Labs** | Azure Policy |
| "Kod önerisi / yapay zekâ eşlikçisi" | **GitHub Copilot** | Advisor |
| "Pipeline'a **parolasız** Azure erişimi" | **OIDC / workload identity federation** | Service principal secret |

**Anahtar kelimeler:** *react to events, publish-subscribe, push* → Event Grid · *enterprise messaging, dead letter, sessions, FIFO* → Service Bus · *big data streaming, partitions, capture, Kafka* → Event Hubs · *simple queue, lowest cost* → Storage Queue · *designer, connectors, workflow* → Logic Apps · *gateway, policies, developer portal* → API Management.

**Evet/hayır tuzakları:**
- "Event Grid mesajları sıralı ve garantili FIFO iletir" → **Hayır** (Service Bus sessions).
- "Event Hubs, kuyruktur; mesaj tüketilince silinir" → **Hayır** (akış; saklama süresi boyunca yeniden okunur).
- "Service Bus Basic katmanı topic destekler" → **Hayır** (Standard ve üstü).
- "Azure DevOps ve GitHub aynı şirketin ürünleridir" → **Evet** (Microsoft).
- "API Management bir mesaj kuyruğudur" → **Hayır** (HTTP API ağ geçidi).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Kritik komutu (sipariş) Event Grid ile yollamak | Komut/iş verisi → Service Bus; Event Grid "bir şey oldu" bildirimi içindir |
| Event Hubs'ı kuyruk gibi kullanmak | Akış servisidir; tüketici offset'i kendi tutar, mesaj "tüketilince silinmez" |
| Mesajın tek kez işleneceğini varsaymak | At-least-once: tüketiciyi **idempotent** yaz (→ M12) |
| Service Bus'ta DLQ'yu izlememek | Dead-letter birikir ve sessizce veri kaybı olur; izle ve uyarı kur |
| Logic Apps'e karmaşık algoritma gömmek / Functions'ta basit connector işi yazmak | İş akışı ve entegrasyon → Logic Apps; özel mantık → Functions |
| Bağlantı dizesi/SAS anahtarını koda ve depoya koymak | Entra ID + managed identity, RBAC rolleri (Service Bus Data Sender/Receiver); anahtar gerekiyorsa Key Vault |
| CI/CD'ye uzun ömürlü service principal secret'ı koymak | **OIDC / workload identity federation**; federated credential'ı repo/branch/environment ile daralt |
| "Azure DevOps eskidi, GitHub kullan" ya da tersi dogması | İkisi de destekli ve entegre; karar ekip ve ihtiyaçla |
| API Management'ı ucuz sanmak | Katmana göre sabit ücret/kapasite; basit tek API için fazla olabilir |
| Deneme namespace'ini/hub'ını açık unutmak | Premium/Dedicated katmanlar sabit ücretlidir; grubu sil |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Event Grid (olay) | Hafif, reaktif, çok sayıda aboneye fan-out; sunucusuz maliyet modeli | Sıra yok, kısıtlı retry penceresi, mesaj aracısı özellikleri yok |
| Service Bus Standard/Premium | Güvenilir teslim, DLQ, session, transaction, topic | Basic/Storage Queue'dan pahalı; kavram yükü; Premium sabit kapasite ücreti |
| Event Hubs | Çok yüksek throughput, yeniden okunabilir akış, Kafka uyumu | Mesaj broker'ı değildir; partition ve consumer yönetimi |
| Storage Queue | En ucuz/basit, büyük kapasite | Gelişmiş özellik yok (FIFO garantisi, topic, DLQ) |
| Logic Apps | Hızlı kurulum, bakım kolaylığı (kod yok) | Karmaşık mantıkta tasarımcı hantallaşır; tüketim başına eylem ücreti büyüyebilir |
| API Management | Merkezî güvenlik, politika, gözlem | Ek ağ sıçraması (gecikme), katman maliyeti, yönetim yükü |
| GitHub Actions / Azure Pipelines (hosted agent) | Altyapı yok, hızlı başlangıç | Dakika kotası/ücreti; özel ağ erişimi için self-hosted agent gerekir |
| OIDC federasyonu | Kalıcı sır yok, rotasyon yok | Entra'da başlangıç yapılandırması; subject eşleşmesi hatalarına dikkat |
| DevTest Labs / otomatik kapatma | Dev/test maliyeti düşer | Geliştiriciler için kapanma/başlama bekleme süresi |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Choose between Azure messaging services — Event Grid, Event Hubs, and Service Bus](https://learn.microsoft.com/en-us/azure/service-bus-messaging/compare-messaging-services)
- [What is Azure Logic Apps?](https://learn.microsoft.com/en-us/azure/logic-apps/logic-apps-overview) · [What is Azure Event Grid?](https://learn.microsoft.com/en-us/azure/event-grid/overview) · [What is Azure Service Bus?](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview) · [Azure Event Hubs](https://learn.microsoft.com/en-us/azure/event-hubs/event-hubs-about) · [Azure Queue Storage](https://learn.microsoft.com/en-us/azure/storage/queues/storage-queues-introduction)
- [Service Bus REST — send / receive-and-delete](https://learn.microsoft.com/en-us/rest/api/servicebus/) · [`az servicebus`](https://learn.microsoft.com/en-us/cli/azure/servicebus)
- [About API Management](https://learn.microsoft.com/en-us/azure/api-management/api-management-key-concepts)
- [What is Azure DevOps?](https://learn.microsoft.com/en-us/azure/devops/user-guide/what-is-azure-devops) · [GitHub Actions documentation](https://docs.github.com/en/actions) · [Azure DevTest Labs](https://learn.microsoft.com/en-us/azure/devtest-labs/devtest-lab-overview) · [Azure Deployment Environments](https://learn.microsoft.com/en-us/azure/deployment-environments/overview-what-is-azure-deployment-environments) · [Azure Developer CLI](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/overview)
- [Use GitHub Actions to connect to Azure (OIDC)](https://learn.microsoft.com/en-us/azure/developer/github/connect-from-azure-openid-connect) · [Workload identity federation](https://learn.microsoft.com/en-us/entra/workload-id/workload-identity-federation)


### 📝 Bölüm 4.6 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir blob storage hesabına dosya yüklendiğinde, ilgilenen birkaç sistemin **hafif bir bildirimle** haberdar edilmesi isteniyor. Hangi servis bu **olay yönlendirme** işi için tasarlanmıştır?

- A) Azure Service Bus
- B) Azure Event Grid
- C) Azure API Management
- D) Azure DevTest Labs

<details><summary>Cevap</summary>

**B.** Event Grid, "bir şey oldu" olaylarını abonelere push ile ileten yönetilen olay yönlendirme servisidir (blob oluşturma olayı bir system topic'tir).
</details>

#### Soru 2: Tek seçim
Bir ödeme sistemi, sipariş komutlarını **kaybetmeden**, **sırayla** işlemek; işlenemeyenleri ayrı bir kuyrukta biriktirmek istiyor. Hangi servis uygundur?

- A) Azure Event Grid
- B) Azure Event Hubs
- C) Azure Service Bus
- D) Azure Logic Apps

<details><summary>Cevap</summary>

**C.** Service Bus: kuyruk/topic, session ile FIFO, dead-letter queue, tekrar deneme ve transaction'lar. Event Grid ve Event Hubs bu tür kurumsal mesaj teslimi için tasarlanmamıştır.
</details>

#### Soru 3: Tek seçim
Bir mobil oyun, saniyede yüz binlerce kullanım olayını toplayıp sonradan analiz için akışa vermek istiyor; mevcut analiz ekibi **Kafka** istemcileri kullanıyor. Hangi servis en uygundur?

- A) Azure Event Hubs
- B) Azure Storage Queue
- C) Azure Logic Apps
- D) Azure Service Bus Basic

<details><summary>Cevap</summary>

**A.** Event Hubs yüksek hacimli olay akışı alımı içindir; Kafka uyumlu uç nokta sunar ve olaylar saklama süresi boyunca yeniden okunabilir.
</details>

#### Soru 4: Eşleştirme
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

#### Soru 5: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Service Bus Basic katmanı topic ve subscription destekler. | ☐ | ☐ |
| b) Service Bus'ta dead-letter queue, işlenemeyen mesajların park edildiği alt kuyruktur. | ☐ | ☐ |
| c) Event Hubs'ta bir tüketici okuduktan sonra olay akıştan otomatik silinir. | ☐ | ☐ |
| d) Event Grid, mesajların her zaman kesin FIFO sırasıyla teslim edilmesini garanti eder. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır · d) Hayır.** Topic için Standard veya üstü gerekir. Event Hubs olayları saklama süresi boyunca tutar ve yeniden okunabilir. Event Grid için sıralı FIFO garantisi yoktur; sıra gereken yerde Service Bus session'ları kullanılır.
</details>

#### Soru 6: Tek seçim
Aşağıdakilerden hangisi **olay (event)** ile **mesaj (message)** arasındaki farkı **en iyi** özetler?

- A) Olay her zaman şifreli, mesaj şifresizdir.
- B) Olay "bir şey oldu" bildirimidir, yayıncı sonucuyla ilgilenmez; mesaj ise tüketicinin işlemesi beklenen veri/komuttur.
- C) Olaylar yalnızca Azure içinde, mesajlar yalnızca Azure dışında kullanılır.
- D) Olay bir veritabanıdır, mesaj bir dosyadır.

<details><summary>Cevap</summary>

**B.** Olay = durum değişikliği bildirimi (Event Grid/Event Hubs); mesaj = işlenmesi beklenen iş verisi veya komut (Service Bus/Storage Queue).
</details>

#### Soru 7: Tek seçim
Bir şirket, "yeni müşteri formu gelince CRM'e kayıt ekle, e-posta gönder ve Teams'e bildirim at" iş akışını **mümkün olduğunca az kodla**, hazır connector'larla kurmak istiyor. Hangi servis?

- A) Azure Functions (tamamen kodla)
- B) Azure Logic Apps
- C) Azure Batch
- D) Azure Backup

<details><summary>Cevap</summary>

**B.** Logic Apps görsel tasarımcı ve connector'larla iş akışı kurar. Functions kod gerektirir; özel mantık için Logic Apps ile birlikte kullanılabilir (→ C1.6).
</details>

#### Soru 8: Çoklu seçim (iki doğru cevap)
Bir ekip, partner firmaların kullanacağı REST API'lere **abonelik anahtarı**, **günlük kota** ve bir **geliştirici portalı** eklemek istiyor. Aşağıdakilerden **hangi ikisi** doğru bir ifadedir?

- A) Bu ihtiyaç için Azure API Management uygundur.
- B) Bu ihtiyaç için Azure Event Hubs uygundur.
- C) API Management, politikalarla rate limit ve istek/yanıt dönüşümü uygulayabilir.
- D) API Management bir mesaj kuyruğudur.

<details><summary>Cevap</summary>

**A ve C.** APIM HTTP API'ler için ağ geçididir (güvenlik, politika, kota, portal). Event Hubs ve kuyruklar API yönetimi değildir.
</details>

#### Soru 9: Eşleştirme
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

#### Soru 10: Tek seçim
Bir yazılım şirketi, geliştirici/test ortamlarındaki VM'lerin **gece otomatik kapanmasını**, VM sayısı/boyutu politikasını ve harcama kontrolünü kolaylaştırmak istiyor. Hangi servis bu iş için tasarlanmıştır?

- A) Azure DevTest Labs
- B) Azure Event Grid
- C) Azure Site Recovery
- D) Azure API Management

<details><summary>Cevap</summary>

**A.** DevTest Labs, dev/test ortamlarında otomatik kapatma/başlatma, politikalar ve maliyet kontrolü sunar.
</details>

#### Soru 11: Senaryo
Bir GitHub Actions workflow'u Azure'a dağıtım yapacak. Güvenlik ekibi, depoda veya GitHub secret'ında **uzun ömürlü bir service principal parolası** bulunmasını istemiyor. En uygun çözüm hangisidir?

- A) Parolayı workflow dosyasına düz metin yazmak
- B) Workflow için OIDC ile workload identity federation kullanıp Entra'da federated credential tanımlamak
- C) Aynı parolayı her depoda paylaşmak
- D) Dağıtımı herkese açık bir Storage anahtarıyla yapmak

<details><summary>Cevap</summary>

**B.** OIDC federasyonunda workflow GitHub'ın verdiği kısa ömürlü token'ı Azure token'ıyla takas eder; kalıcı sır yoktur (`permissions: id-token: write` + `azure/login`).
</details>

#### Soru 12: Senaryo
Bir sipariş sistemi, sipariş komutlarını Event Grid ile iletiyordu. Bir tüketici servisi bir süre kapalı kaldığında, retry süresi dolan siparişler kayboldu. Sorunun **temel nedeni** ve **doğru düzeltme** hangisidir?

- A) Event Grid çok yavaştır; Event Hubs'a geçilmeli
- B) Sipariş bir **komut/iş verisi**dir; kaybolmamalı ve tekrar denenmelidir → Service Bus kuyruğu/topic'i kullanılmalı
- C) Siparişler şifrelenmediği için kaybolmuştur; disk şifrelemesi açılmalı
- D) Logic Apps eklenirse sorun çözülür

<details><summary>Cevap</summary>

**B.** Event Grid hafif "bir şey oldu" bildirimleri içindir. Güvenilir teslim, kalıcı kuyruk, dead-letter ve tekrar deneme Service Bus'ın görevidir.
</details>

#### Soru 13: Sıralama
Bir CI/CD akışının mantıksal sırasını yazın: **Üretime yayınla · Kodu commit/push et · Derle ve birim testleri çalıştır · Çıktıyı (artifact) paketle · Staging'de doğrula**

<details><summary>Cevap</summary>

**Commit/push → Derle ve birim testleri çalıştır (CI) → Artifact'ı paketle → Staging'de doğrula → Üretime yayınla (CD, genellikle onayla).**
</details>

#### Soru 14: Evet / Hayır

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


---

## 4.7 Destek Planları, SLA ve Servis Yaşam Döngüsü

> **Sınav Alanı:** Describe Azure management and governance — genişletilmiş kapsam · **Konu Kodu:** C4.7

### 1. Ne?

Bu konu "servisi kullanmaya başladım, **taahhüt ve destek** ne?" sorusunun cevabıdır. Dört ayrı konu bir arada anlatılır:

| Konu | Soru | Anahtar kavram |
|---|---|---|
| **Destek planları** | "Sorun çıkarsa **kim, ne kadar hızlı** yardım eder?" | Basic (ücretsiz) → Developer → Standard → Professional Direct → Unified |
| **SLA** | "Microsoft ne kadar **çalışma süresi** taahhüt ediyor, tutturamazsa ne olur?" | Aylık %, **servis kredisi**, composite SLA |
| **Servis yaşam döngüsü** | "Bu servis **olgun mu**, SLA'sı var mı, yarın kaldırılır mı?" | Private preview → Public preview → **GA**; retirement |
| **Benimseme çerçeveleri** | "Buluta **nasıl geçer ve nasıl iyi tasarlarım**?" | **Cloud Adoption Framework (CAF)**, **Well-Architected Framework (WAF)** |

Bu konunun C# kodu yoktur. Pratik: karar tablosu alıştırması, Service Health olayı arama ve composite SLA hesabı (3.6).

> **Kısaltma tuzağı:** **WAF** iki farklı şeydir: **Well-Architected Framework** (bu konu — mimari ilkeler) ve **Web Application Firewall** (→ C1.17/C1.18 — ağ güvenliği). Bağlama bak.

---

### 2. Neden?

- **Sınav/kurs:** "Ücretsiz destek planı hangisi?", "Preview servisin SLA'sı var mı?", "%99,95 SLA aylık kaç dakika?", "Advisor'ın beş kategorisi hangi çerçevenin sütunları?" soruları eski AZ-900 sürümlerinde ve kurs sınavlarında sabittir; seçeneklerde bugün de görünür.
- **Gerçek işte:** Üretim sistemine **preview** bir servisi koymak veya **SLA'yı hesaplamadan** "%99,99" sözü vermek sık görülen kurumsal hatadır. Ayrıca gece 03:00'te bir kesintide **hangi destek planının** 7/24 telefon desteği verdiğini önceden bilmek gerekir.
- **Mülakat/mimari:** "Bu mimarinin SLA'sı nedir?" sorusu composite SLA hesabıyla cevaplanır; "iyi tasarım nedir?" sorusu Well-Architected sütunlarıyla (→ S8.3).
- **Zincir:** SLA'nın nasıl hesaplanıp kesinti süresine çevrildiği C1.2'dedir; bu konu **hizmet sözleşmesi, destek ve yaşam döngüsü** tarafını ekler.

---

### 3. Nasıl?

#### 3.1 Azure destek planları ⭐

> **Doğrulama notu:** Planların **adları, fiyatları ve yanıt süreleri** sık değişir (ör. Premier'in yerini Unified aldı; EA/MCA-E müşterilerine yönelik bazı ücretsiz destek kapsamları Temmuz 2024'te sona erdi). Aşağıdaki değerler Microsoft'un yayımladığı sayfalara dayanır ama **sınavdan/satın almadan önce [güncel planlar sayfasını](https://azure.microsoft.com/support/plans/) kontrol et**. AZ-900 genelde **sayı değil, kavram** sorar: *hangi plan ücretsiz, hangisi 7/24 teknik destek verir, hangisi üretim için*.

| Plan | Kim için | Teknik destek | Yanıt süresi (özet, **doğrula**) | Ücret |
|---|---|---|---|---|
| **Basic** | Herkes | **Yok** (faturalama ve abonelik yönetimi desteği 7/24; self-help) | — | **Ücretsiz** (her Azure aboneliğinde) |
| **Developer** | Deneme / **üretim dışı** | İş saatlerinde e-posta | Sev A için garanti yok; B/C için iş saati yanıtı | Düşük aylık ücret (yayımlanan liste ≈ aylık 29 $) |
| **Standard** | **Üretim** iş yükleri | **7/24** telefon + e-posta | **Sev A < 1 saat** | Orta aylık ücret (≈ aylık 100 $) |
| **Professional Direct** | İş-kritik, orta ölçek | 7/24 + **hızlı yanıt**, mimari/operasyon rehberliği, daha kapsamlı kaynaklar | **Sev A < 1 saat**, öncelikli işleme | Yüksek aylık ücret (≈ aylık 1.000 $) |
| **Unified** (eski Premier'in yerini aldı) | Büyük kurum; **Azure + M365 + Dynamics** vb. tüm Microsoft ürünleri | 7/24, atanmış hesap/müşteri başarı yönetimi, proaktif hizmetler | Sözleşmeye göre; Sev A için en hızlı | Kuruma özel; kullanım yüzdesine dayalı sözleşme |
| *Azure Rapid Response* (ek) | Kritik üretim iş yükü | Hızlı müdahale için ek | Sev A için daha kısa yanıt *(yayımlanan: < 15 dk)* | Ek ücret |

**Severity (önem) seviyeleri:** **Sev A (Critical)** — üretim tamamen durdu, iş etkisi kritik · **Sev B (Moderate)** — iş yükü etkilendi/kısıtlı · **Sev C (Minimal)** — genel soru/küçük etki. Yanıt süresi = talebi açtığın andan bir Microsoft mühendisinin **ilk dönüşüne** kadar geçen süre; **çözüm süresi değildir**.

**Tanımlar:**

| Terim | Anlamı |
|---|---|
| **TAM (Technical Account Manager)** | Büyük planlarda atanmış, kurumunu tanıyan teknik yönetici; proaktif rehberlik |
| **Support request (destek talebi)** | Portal → **Help + support** → *Create a support request*: sorun türü, abonelik, hizmet, önem seviyesi, iletişim yöntemi |
| **Plan kapsamı** | Planlar abonelik/fatura hesabı bazında satın alınır; plan, o kapsamın kaynakları için geçerlidir |

**Basic'te neler vardır?** Faturalama/abonelik talepleri (7/24), **dokümantasyon, Microsoft Learn, Microsoft Q&A topluluğu, Azure Advisor, Service Health, Azure Status** — yani **self-service**. Teknik bir "sunucum neden çöktü" talebi için **ücretli plan** gerekir (kota artırma talepleri gibi bazı talepler Basic'te de açılabilir *(doğrula)*).

**Self-service kaynaklar:**

| Kaynak | Ne için |
|---|---|
| **Azure Status** | Global Azure sağlığı (genel, herkese açık) → C1.14 |
| **Service Health / Resource Health** | **Senin** abonelik/kaynaklarını etkileyen olaylar (→ C1.14) |
| **Microsoft Learn / Docs** | Eğitim ve referans |
| **Microsoft Q&A / Stack Overflow** | Topluluk soru-cevap |
| **Azure Updates** | Yeni özellik, **preview/GA**, **retirement** duyuruları |
| **Azure Advisor** | Kişiselleştirilmiş öneri (→ C1.14) |

**Plan seçim ağacı:**

```
Teknik destek gerekiyor mu?
├── Hayır (yalnızca fatura/self-help)                 → Basic (ücretsiz)
└── Evet
    ├── Üretim dışı / öğrenme, iş saatinde yeter       → Developer
    ├── Üretim iş yükü, 7/24 telefon                   → Standard
    ├── İş-kritik, hızlı yanıt + rehberlik             → Professional Direct
    └── Kurum geneli, Azure + M365/Dynamics, TAM        → Unified
```

#### 3.2 SLA'lar ⭐

**SLA (Service Level Agreement)**, Microsoft'un bir servis için taahhüt ettiği **aylık çalışma süresi yüzdesidir**; tutturamazsa müşteri, faturadan **servis kredisi** talep edebilir. SLA **kesintiyi önleme garantisi değil**, **mali taahhüttür**. Ayrıntı ve hesap altyapısı C1.2'dedir.

**Yüzde → kesinti** (ay = 730 saat ≈ 43.800 dk):

| SLA | Aylık izin verilen kesinti |
|---|---|
| %99 | ≈ 7,3 saat |
| %99,9 | ≈ 43,8 dk |
| %99,95 | ≈ 21,9 dk |
| %99,99 | ≈ 4,4 dk |
| %99,999 | ≈ 26 sn |

**SLA'yı doğru okumak:**

| Kural | Açıklama |
|---|---|
| **Servise ve yapılandırmaya özgüdür** | Tek VM (Premium SSD/Ultra) için tipik %99,9; availability set'te 2+ VM için %99,95; ≥ 2 zone'a yayılmış VM'lerde %99,99 — **güncelini SLA belgesinden** kontrol et |
| **Servis kredisi kademelidir** | Gerçekleşen çalışma süresi düştükçe iade yüzdesi artar (belgedeki kademe tablosu); **talep etmek müşterinin işidir**, süresi içinde başvurulur |
| **Kapsam dışı olanlar** | Planlı bakım (duyurulmuş), müşterinin kendi kodu/yapılandırması, önizleme servisler, **Free/Shared katmanlar** vb. SLA'ya sayılmaz |
| **SLA yok** | **Preview** servisler ve genellikle **ücretsiz katmanlar** (ör. App Service Free/Shared); ayrıca bazı ücretsiz servisler/özellikler |
| **Kaynak: Azure SLA belgesi** | Microsoft **Service Level Agreements for Online Services** sayfasında servis servis listelenir |

**Composite SLA** *(hesap sorusu gelebilir; → C1.2)*:

- **Seri bağlı** (hepsi çalışmalı): **çarpılır** → toplam, en zayıf halkadan da düşüktür.
- **Paralel/yedekli:** `1 − (1 − s)ⁿ` → yedeklilik SLA'yı **yükseltir** (arızaların bağımsız ve yönlendirme katmanının çalışır olduğu varsayımıyla).

**SLA'yı yükseltme yolları:** (1) **availability zone**'lara yaymak, (2) **birden çok region** (aktif-pasif/aktif-aktif; yönlendirme için → C1.17), (3) servisin **daha üst SLA'lı** katmanını/SKU'sunu seçmek, (4) bağımlılık sayısını azaltmak (seri zinciri kısaltmak), (5) uygulama tarafında yeniden deneme/devre kesici. **Bedeli:** maliyet ve karmaşıklık.

#### 3.3 Servis yaşam döngüsü ⭐

| Aşama | Kim kullanabilir | SLA | Destek | Notlar |
|---|---|---|---|---|
| **Private preview** | **Davetle**, sınırlı müşteri/kuruluş | Yok | Çoğunlukla ürün ekibiyle doğrudan / sınırlı | Erken geri bildirim; sözleşme/NDA olabilir |
| **Public preview** | **Herkes** (genellikle opt-in) | **Yok** | Standart destek kapsamı **dışında/sınırlı** olabilir *(servise göre değişir)* | Özellik/API **değişebilir**, kaldırılabilir; kota/bölge sınırlı olabilir; **üretim için önerilmez** |
| **General Availability (GA)** | Herkes | **Evet** (servise özgü) | **Tam destek** (planına göre) | Üretim için hedeflenen aşama |

> Kural: **Preview = deneme.** Preview'lar, Microsoft'un ayrı yayımladığı *Supplemental Terms of Use for Microsoft Azure Previews* şartlarına tabidir; özet: "SLA yok, destek garantisi yok, özellik ve fiyat değişebilir". Üretimde preview bağımlılığı bir **risk kaydı** konusu olmalıdır.

**Preview özelliklerini açma:** Abonelik düzeyinde *Preview features* (portal) veya `az feature register` (Azure Feature Exposure Control) ile bir özelliğe **katılım** sağlanır:

```bash
az feature list --namespace Microsoft.Compute --output table            # features and their registration state
az feature show --namespace Microsoft.Compute --name <FeatureName>      # a single feature
az feature register --namespace Microsoft.Compute --name <FeatureName>  # opt in (preview!)
```

**Retirement (emeklilik) ve değişiklik takibi:** Servisler, özellikler ve API sürümleri **kaldırılabilir**. Takip: **Azure Updates** (retirement duyuruları), portalda **Service Health → Health advisories** (aksiyon gerektiren değişiklikler, ör. bir özelliğin emekliliği), e-posta bildirimleri ve **Azure Advisor**'ın emeklilik uyarıları. Duyuru süresi **servise göre değişir** (çoğu zaman en az aylar–yıl mertebesinde); duyuruyu kaçırmak **zorunlu geçiş** yapmak demektir. Örnek: Azure Sphere'in 2031'de emekli olacağı duyurusu (→ C1.20, doğrula). Bu, C1.2 **reliability**/operasyonel olgunluk hedefiyle de bağlantılıdır.

**Kısa notlar:**

| Konu | Not |
|---|---|
| **Azure Marketplace** | Microsoft ve **üçüncü taraf (ISV)** yazılım/servislerinin (VM imajı, SaaS, yönetilen uygulama) kataloğu; çoğu zaman **Azure faturasıyla** satın alınır (→ C1.4) |
| **Azure Government / Azure China** | Ayrı ("sovereign") bulutlar; kendi uyumluluk/işletim modeli; **servis/özellik kümesi ve GA zamanları global Azure'dan farklı** olabilir (→ C1.4, C1.12) |
| **Bölgesel kullanılabilirlik** | Bir servis GA olsa bile **her bölgede** olmayabilir; Azure Products by Region sayfasını kontrol et |

#### 3.4 Benimseme çerçeveleri ⭐

**Microsoft Cloud Adoption Framework (CAF)** — buluta **geçiş yolculuğunun** (strateji → işletme) rehberi: süreç, roller, kontrol listeleri.

| Aşama (metodoloji) | Soru | Örnek çıktı |
|---|---|---|
| **Strategy** | *Neden* bulut? İş hedefleri, iş gerekçesi | Gerekçe belgesi, hedef sonuçlar |
| **Plan** | *Ne*yi, hangi sırayla? Dijital varlık envanteri, beceri planı | Taşıma planı, ön değerlendirme (→ C1.19 Migrate) |
| **Ready** | Azure ortamını **hazırla**: hesap yapısı, ağ, kimlik | **Landing zone** |
| **Adopt** | **Taşı** (migrate) ve/veya **yenilikçi/yeni geliştir** (innovate/modernize) | Taşınan ve yeni uygulamalar |
| **Govern** | Kural ve risk yönetimi | Azure Policy, maliyet ve uyumluluk (→ C1.11, C1.12) |
| **Manage** | Operasyon: izleme, yedek, DR, olay yönetimi | Monitor, Backup, Site Recovery (→ C1.14, C1.19) |
| **Secure** | Sürekli güvenlik iyileştirme | Defender for Cloud, Zero Trust (→ C1.10) |

> Aşama adları ve sayısı CAF belgesinin sürümüne göre kısmen değişir (bazı anlatımlar *Organize* ekler, bazıları *Secure*'u ayırmaz); sınav için **Strategy–Plan–Ready–Adopt–Govern–Manage** akışı yeterlidir. Strategy/Plan/Ready/Adopt temel sıra, Govern/Manage/(Secure) **sürekli** yürüyen aşamalardır.

**Azure landing zone** *(bağlam)* — buluta taşınan iş yüklerinin **güvenle yerleşeceği önceden hazırlanmış ortam**: management group hiyerarşisi, abonelik düzeni, Policy atamaları, ağ topolojisi (hub-spoke), kimlik ve izleme taban çizgisi. "Ready" aşamasının ana çıktısıdır.

**Azure Well-Architected Framework (WAF)** — **bir iş yükünün kalitesini** değerlendirmek için **beş sütun**:

| Sütun | Soru | Örnek | Advisor kategorisi (→ C1.14) |
|---|---|---|---|
| **Reliability** | Arızada çalışmaya devam eder/toparlanır mı? | Zone/region yedekliliği, backup, DR | **Reliability** |
| **Security** | Veriyi ve sistemi koruyor mu? | Zero Trust, şifreleme, en az yetki | **Security** |
| **Cost Optimization** | Gereksiz harcama yok mu? | Doğru boyut, reservation, kapatma | **Cost** |
| **Operational Excellence** | Operasyon/dağıtım sağlıklı mı? | IaC, CI/CD, izleme | **Operational Excellence** |
| **Performance Efficiency** | Talebe göre ölçekleniyor mu? | Autoscale, cache, doğru SKU | **Performance** |

> Advisor'ın beş kategorisi, Well-Architected Framework'ün beş sütunuyla **bire bir örtüşür**. Well-Architected Review aracı iş yükünü bu sütunlara göre değerlendirir.

**Azure Architecture Center** — referans mimariler, tasarım desenleri, en iyi uygulamalar ve karar rehberlerinin merkezi (CAF ve WAF ile birlikte "üçlü" olarak kullanılır).

**CAF ↔ WAF farkı:** CAF **kurumun buluta geçiş yolculuğunu** (strateji, plan, hazırlık, yönetişim) yönetir; WAF **tek bir iş yükünün tasarım kalitesini** değerlendirir. Biri "yol haritası", diğeri "mimari kalite çıtası".

#### 3.5 Karar tablosu alıştırması — destek planı ve yaşam döngüsü

| # | Senaryo | Senin cevabın |
|---|---|---|
| 1 | Öğrenci, ücretsiz aboneliğinde sadece öğreniyor; yalnızca fatura sorusu olabilir | |
| 2 | Küçük ekip, geliştirme/test ortamı, iş saatlerinde e-posta yeter | |
| 3 | E-ticaret üretimi, gece 03:00'te de telefonla teknik destek gerekli | |
| 4 | Bankanın tüm Microsoft ürünleri için atanmış TAM ve proaktif destek | |
| 5 | Yeni bir veri servisi *public preview*'da; ekip müşteriye bunun üzerine "%99,99 SLA" taahhüt etmek istiyor | |
| 6 | İş yükünün bir sütununda (güvenlik) zayıflık çıktı; hangi çerçeve bunu sistematik değerlendirir? | |

<details><summary>Çözüm</summary>

1 → **Basic**. 2 → **Developer**. 3 → **Standard** (en az; iş-kritikse Professional Direct). 4 → **Unified** (TAM). 5 → **Yapılamaz**: preview servisin SLA'sı **yoktur**; GA'yı bekle veya SLA'lı alternatifi seç; en azından riski belgele. 6 → **Well-Architected Framework** (Security sütunu, Well-Architected Review).
</details>

#### 3.6 Pratik — Service Health olayı arama ve composite SLA hesabı

**1) Service Health olaylarını bul** (Activity Log; boş abonelikte boş dönmesi normaldir):

```bash
# ServiceHealth events from the last 30 days (service issues, planned maintenance, health advisories)
az monitor activity-log list --offset 30d \
  --query "[?category.value=='ServiceHealth'].{time:eventTimestamp, status:status.value, title:properties.title}" \
  --output table

# Count events by category over the last 7 days (what's in the log at all?)
az monitor activity-log list --offset 7d --query "[].category.value" --output tsv | sort | uniq -c
```

Portalda aynı bilgi *Service Health → Health history* ve *Health advisories* altındadır; olay detayında etkilenen servis/bölge ve (kapanmış olaylarda) **RCA** raporu bulunur (→ C1.14).

**2) Composite SLA hesabı — iki servis** (C1.2'nin `serial` demosu; repo kökünde `ConsoleApp` klasöründe):

```bash
cd ConsoleApp

> **Alıştırma (elle hesapla, sonra doğrula).** Bir uygulama: **App Service %99,95 → Azure SQL Database %99,99 → Storage %99,9** (hepsi seri). (a) Toplam SLA? (b) Aylık kesinti bütçesi? (c) Web katmanını **iki bağımsız instance** ile paralel kurarsan (yönlendirme katmanını yok sayarak) toplam ne olur?
>
> <details><summary>Çözüm</summary>
>
> (b) Kayıp %0,16 × 43.800 dk ≈ **70 dakika/ay**.
> (c) Web katmanı: `1 − (1 − 0,9995)² = 0,99999975`. Toplam: `0,99999975 × 0,9999 × 0,999 ≈ 0,99890` → **≈ %99,89 (~48 dk/ay)**. Gözlem: web katmanını ikiletmek kazanç sağladı ama toplamı artık **Storage %99,9** ve **SQL %99,99** sınırlıyor; asıl darboğaz en zayıf **seri** halkadır. Gerçek mimaride trafiği yönlendiren Front Door/Traffic Manager da seri bağlıdır ve onun SLA'sıyla da çarpılır (→ C1.17).
> </details>

*Bu laboratuvar kaynak oluşturmaz; maliyet yoktur, temizlik gerekmez.* Başka bir `az` laboratuvarı yaptıysan kaynak grubunu sildiğinden ve bütçe alarmının kurulu olduğundan emin ol (→ C1.11).

---

### 4. Gerçek dünya senaryosu

Bir fintech, ödeme API'sini Azure'a taşıyor ve yönetim kuruluna "%99,95 çalışma süresi" sözü verilmesini istiyor.

1. **CAF:** Önce **Strategy** (neden taşıyoruz: ölçek ve uyumluluk), **Plan** (hangi sistemler, hangi sırayla — Azure Migrate değerlendirmesi, → C1.19), **Ready** (**landing zone**: management group'lar, Policy, hub-spoke ağ). Sonra **Adopt**, ardından sürekli **Govern/Manage/Secure**.
2. **SLA analizi:** Mimari: Front Door (%99,99) → App Service (%99,95) → Azure SQL (%99,99) → Storage (%99,9). Seri çarpım ≈ **%99,83** (~74 dk/ay) — hedefin **altında**. İyileştirme: App Service ve SQL'i **zone-redundant** yapmak ve SLA belgesindeki üst katmanları seçmek; ayrıca **bağımlılığı azaltmak**. Yeni composite hedefi tutturunca iş birimine "ayda yaklaşık 20 dakika" olarak anlatılır.
3. **Yaşam döngüsü:** Ekip, fraud tespiti için yeni bir AI özelliğini **public preview**'da buldu. Karar: **üretimde kullanılmaz** (SLA ve destek garantisi yok); pilot ortamında denenir, GA ve SLA netleşince geçilir. Risk kaydı tutulur.
4. **Destek planı:** Üretim aboneliği için en az **Standard**; ödeme akışı iş-kritik olduğundan **Professional Direct** düşünülür. Dev/test aboneliği **Developer**'da kalır; öğrenme/sandbox'ta **Basic**. Kesinti başında ilk bakış: **Service Health** (Azure tarafı mı?) → değilse uygulama; Azure ise Sev A destek talebi.
5. **Retirement takibi:** Ekip **Azure Updates** ve Service Health *health advisories* abonelikleri kurar; bir veritabanı API sürümünün emeklilik duyurusu 10 ay önceden fark edilir ve sürüm geçişi bir sprint'e planlanır — acil durum olmaz.
6. **WAF review:** Üç ayda bir **Well-Architected Review**: Reliability (DR tatbikatı), Security (Defender secure score), Cost (Advisor önerileri), Operational Excellence (IaC/CI-CD), Performance (autoscale). **Advisor**'ın beş kategorisi aynı sütunlara bağlandığı için iki kaynak birbirini doğrular.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Destek planı seçimi (senaryo → plan), preview/GA farkı, SLA yorumlama/yüzde → kesinti, composite SLA mantığı ve CAF/WAF eşleştirme.

| Soru kalıbı | Doğru cevap | Tuzak |
|---|---|---|
| "**Ücretsiz** destek planı" | **Basic** | Developer |
| "Her Azure aboneliğinde varsayılan plan" | **Basic** | Standard |
| "Üretim dışı, iş saatinde e-posta" | **Developer** | Standard |
| "Üretim iş yükü, **7/24** teknik destek" | **Standard** (veya üstü) | Developer |
| "İş-kritik, hızlı yanıt, mimari rehberlik" | **Professional Direct** | Standard |
| "Kurum geneli, **atanmış TAM**, tüm Microsoft ürünleri" | **Unified** (eski Premier) | Professional Direct |
| "Preview servisin SLA'sı" | **Yok** | %99,9 |
| "Hangi aşamada **SLA ve tam destek** vardır?" | **General Availability (GA)** | Public preview |
| "Davetle sınırlı kullanıcı, erken aşama" | **Private preview** | Public preview |
| "Herkesin deneyebildiği, SLA'sız önizleme" | **Public preview** | GA |
| "SLA tutturulamazsa ne olur?" | **Servis kredisi** (müşteri talep eder) | Otomatik para iadesi |
| "İki servisin SLA'sı seri bağlıysa toplam" | **Çarpım** (daha düşük) | Ortalama / yüksek olan |
| "SLA'yı yükseltmek için" | **Zone/region yedekliliği**, üst SLA'lı SKU | Daha büyük VM |
| "Buluta geçiş yolculuğu rehberi: strateji, plan, ready, adopt, govern, manage" | **Cloud Adoption Framework** | Well-Architected Framework |
| "Beş sütun: reliability, security, cost, operational excellence, performance" | **Well-Architected Framework** | Cloud Adoption Framework |
| "Referans mimariler ve tasarım desenleri" | **Azure Architecture Center** | Learn |
| "Hazır, standartlara uygun başlangıç ortamı (management group, policy, ağ)" | **Azure landing zone** | Resource group |
| "WAF" (mimari bağlamda) | **Well-Architected Framework** | Web Application Firewall |

**Anahtar kelimeler:** *free, billing support only* → Basic · *production, 24/7* → Standard · *business-critical* → Professional Direct · *Technical Account Manager* → Unified · *no SLA, subject to change, not recommended for production* → preview · *monthly uptime percentage, service credit* → SLA · *five pillars* → WAF · *landing zone, strategy–plan–ready–adopt* → CAF.

**Evet/hayır tuzakları:**
- "Public preview'daki bir servis için Microsoft %99,9 SLA verir" → **Hayır**.
- "Basic planda 7/24 telefonla teknik destek vardır" → **Hayır** (yalnızca faturalama/abonelik desteği).
- "SLA, kesintiyi engelleyeceğini garanti eder" → **Hayır** (taahhüt + kredi).
- "Seri bağlı iki servisin toplam SLA'sı, ikisinden de düşüktür" → **Evet**.
- "Advisor'ın beş kategorisi Well-Architected sütunlarına karşılık gelir" → **Evet**.
- "Yanıt süresi, sorunun çözüldüğü süredir" → **Hayır** (ilk dönüş süresi).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "Basic plan teknik destek verir" | Basic = faturalama/abonelik desteği + self-help; teknik destek ücretli plan |
| "Premier hâlâ güncel plan" | Premier'in yerini **Unified** aldı; kurslarda eski ad geçebilir |
| Yanıt süresiyle çözüm süresini karıştırmak | Yanıt = ilk dönüş; çözüm süresi taahhüdü yoktur |
| Plan fiyat/süresini ezberlemek | Sayılar değişir; kavramı (ücretsiz/7-24/iş-kritik/TAM) bil, rakamı güncel sayfadan doğrula |
| Preview servisi üretime koymak | SLA yok, kırılabilir, değişebilir; yalnızca pilot |
| "Servis GA, her bölgede vardır" | Bölgesel kullanılabilirliği ayrıca kontrol et |
| Tek servisin SLA'sını **uygulamanın** SLA'sı sanmak | Seri bağlı bileşenlerin SLA'larını çarp (composite) |
| Paralel yedekliliğin SLA'yı sınırsız artırdığını sanmak | Bağımsız arıza varsayımı ve seri halkalar (yönlendirme, DB) toplamı sınırlar |
| SLA kredisini otomatik beklemek | Müşteri **talep eder**, süresi ve koşulları belgededir |
| Retirement duyurularını izlememek | Azure Updates + Service Health health advisories; acil geçiş maliyeti büyüktür |
| CAF ile WAF'ı karıştırmak | CAF = geçiş yolculuğu; WAF = iş yükü mimari kalitesi (5 sütun) |
| WAF'ı her zaman "firewall" sanmak | Bağlama bak: mimari → Well-Architected; ağ güvenliği → Web Application Firewall |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Basic plan | Ücretsiz; self-service yeterliyse sıfır maliyet | Teknik destek yok; kesintide kendi başına |
| Standard/Professional Direct | 7/24 teknik destek, hızlı yanıt | Aylık sabit ücret (abonelik/hesap başına); küçük iş yüklerinde pahalı olabilir |
| Unified | TAM, proaktif hizmet, kurum geneli kapsam | Pahalı sözleşme; yalnızca büyük kuruma uygun |
| Preview servis kullanmak | Yeni yeteneğe erken erişim, rekabet avantajı | SLA/destek yok, API değişebilir, geri çekilebilir |
| GA'yı beklemek | SLA, destek, kararlılık | Özelliğe geç erişim |
| Daha yüksek SLA (zone/region) | Daha az kesinti, **mali** koruma değil **gerçek** kesinti azalır | Maliyet ve karmaşıklık; bağımsız arıza varsayımı |
| SLA'yı yalnızca belgeden okumak | Hızlı | Composite hesabı yapılmaz; hedefin altında kalınabilir |
| Landing zone'a yatırım (CAF Ready) | Tutarlı yönetişim, hızlı ve güvenli onboarding | Başlangıç eforu |
| Well-Architected Review | Sistematik iyileştirme, Advisor ile doğrulama | Zaman ve çabası; sütunlar arası çelişki (güvenlik ↔ maliyet) yönetilmelidir |

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Compare Azure support plans](https://azure.microsoft.com/support/plans/) · [Azure support plan response times](https://azure.microsoft.com/support/plans/response/) · [How to create an Azure support request](https://learn.microsoft.com/en-us/azure/azure-portal/supportability/how-to-create-azure-support-request)
- [Service Level Agreements for Online Services](https://www.microsoft.com/licensing/docs/view/Service-Level-Agreements-SLA-for-Online-Services) · [Azure SLA summary](https://azure.microsoft.com/support/legal/sla/summary/)
- [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/) · [Azure Updates](https://azure.microsoft.com/updates/) · [Azure Status](https://azure.status.microsoft/) · [Azure Retirements (Service Health)](https://learn.microsoft.com/en-us/azure/service-health/overview)
- [Cloud Adoption Framework](https://learn.microsoft.com/en-us/azure/cloud-adoption-framework/overview) · [Azure landing zones](https://learn.microsoft.com/en-us/azure/cloud-adoption-framework/ready/landing-zone/)
- [Azure Well-Architected Framework](https://learn.microsoft.com/en-us/azure/well-architected/) · [Azure Architecture Center](https://learn.microsoft.com/en-us/azure/architecture/)
- [`az monitor activity-log`](https://learn.microsoft.com/en-us/cli/azure/monitor/activity-log) · [`az feature`](https://learn.microsoft.com/en-us/cli/azure/feature)


### 📝 Bölüm 4.7 Quiz Soruları

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Her Azure aboneliğinde **varsayılan** olarak bulunan ve **ücretsiz** olan destek planı hangisidir?

- A) Developer
- B) Standard
- C) Basic
- D) Professional Direct

<details><summary>Cevap</summary>

**C.** Basic ücretsizdir; faturalama/abonelik desteği ve self-help kaynakları (Learn, Q&A, Advisor, Service Health) içerir, teknik destek içermez.
</details>

#### Soru 2: Eşleştirme
Her ihtiyacı destek planıyla eşleştirin: **Basic · Developer · Standard · Professional Direct · Unified**

1. Yalnızca öğrenme amaçlı abonelik; sadece fatura sorusu ve dokümantasyon yeter
2. Test ortamı; iş saatlerinde e-posta ile teknik destek yeterli
3. Üretim iş yükü; gece de telefonla teknik destek gerekli
4. İş-kritik iş yükü; hızlı yanıt ve mimari/operasyon rehberliği isteniyor
5. Bankanın Azure, Microsoft 365 ve Dynamics için tek sözleşmesi ve atanmış Technical Account Manager'ı

<details><summary>Cevap</summary>

**1 → Basic · 2 → Developer · 3 → Standard · 4 → Professional Direct · 5 → Unified** (eski Premier'in yerini aldı). Ücret ve yanıt süresi sayıları değişebilir; güncel plan sayfasını kontrol et.
</details>

#### Soru 3: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Basic plan 7/24 telefonla teknik sorun giderme desteği içerir. | ☐ | ☐ |
| b) Destek talebinde "yanıt süresi", sorunun çözüldüğü süreyi ifade eder. | ☐ | ☐ |
| c) Azure Status ve Service Health, Basic planda da kullanılabilen self-service kaynaklardır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Hayır · c) Evet.** Basic teknik destek içermez. Yanıt süresi, bir mühendisin ilk dönüşüne kadar geçen süredir; çözüm taahhüdü değildir.
</details>

#### Soru 4: Tek seçim
Bir ekip yeni bir Azure servisini kullanmak istiyor; servis **public preview** aşamasında. Aşağıdakilerden hangisi **doğrudur**?

- A) Microsoft, preview servisler için GA ile aynı SLA'yı verir.
- B) Preview servisler için SLA yoktur ve destek/özellik garantisi sınırlıdır; üretim için önerilmez.
- C) Preview servisler yalnızca Azure Government'ta bulunur.
- D) Preview servisler her zaman ücretsizdir ve kalıcıdır.

<details><summary>Cevap</summary>

**B.** Preview: SLA yok, özellik/API değişebilir, destek kapsamı sınırlı olabilir. GA aşamasında SLA ve tam destek gelir.
</details>

#### Soru 5: Sıralama
Bir servisin yaşam döngüsü aşamalarını **erken aşamadan olgun aşamaya** doğru sıralayın: **General Availability (GA) · Public preview · Private preview**

<details><summary>Cevap</summary>

**Private preview → Public preview → General Availability (GA).** Private: davetle, sınırlı; Public: herkese açık ama SLA'sız; GA: SLA ve tam destek.
</details>

#### Soru 6: Tek seçim
Bir servisin SLA'sı **%99,9**'dur. Bir ayda (730 saat) izin verilen toplam kesinti yaklaşık ne kadardır?

- A) ≈ 4,4 dakika
- B) ≈ 21,9 dakika
- C) ≈ 43,8 dakika
- D) ≈ 7,3 saat

<details><summary>Cevap</summary>

**C.** 730 saat = 43.800 dk; %0,1 × 43.800 ≈ 43,8 dk. (%99,99 ≈ 4,4 dk, %99,95 ≈ 21,9 dk, %99 ≈ 7,3 saat.)
</details>

#### Soru 7: Tek seçim
Bir uygulama, **seri bağlı** iki bileşenden oluşuyor: web katmanı (SLA %99,9) ve veritabanı (SLA %99,9). Uygulamanın toplam (composite) SLA'sı yaklaşık nedir?

- A) %99,9'dan yüksek
- B) Yaklaşık %99,8
- C) Tam olarak %99,9
- D) Yaklaşık %100

<details><summary>Cevap</summary>

**B.** Seri bileşenlerin SLA'ları çarpılır: 0,999 × 0,999 ≈ 0,998 → %99,8. Toplam, en zayıf bileşenden de düşüktür.
</details>

#### Soru 8: Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** bir uygulamanın SLA'sını **artırmaya** yönelik doğru yaklaşımlardır?

- A) VM'leri birden fazla availability zone'a yaymak
- B) Mevcut tek VM'i daha büyük bir boyuta çıkarmak (yedeklilik eklemeden)
- C) Uygulamayı birden çok bölgede çalıştırıp trafiği yönlendirmek
- D) Resource group'a tag eklemek

<details><summary>Cevap</summary>

**A ve C.** SLA'yı yükseltmenin yolu yedekliliktir (zone, region) veya daha yüksek SLA'lı katman seçmektir. Daha büyük VM tek arıza noktasını kaldırmaz; tag SLA'yı etkilemez.
</details>

#### Soru 9: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) SLA tutturulamadığında, müşteri faturasından servis kredisi talep edebilir. | ☐ | ☐ |
| b) SLA, servisin hiçbir zaman kesintiye uğramayacağını garanti eder. | ☐ | ☐ |
| c) Ücretsiz katmanlar (ör. App Service Free) genellikle SLA kapsamında değildir. | ☐ | ☐ |
| d) Seri bağlı bileşenlerin toplam SLA'sı, en düşük bileşenin SLA'sından yüksektir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet · d) Hayır.** SLA bir mali taahhüttür, kesintisizlik garantisi değildir. Seri toplam en düşük bileşenin altına iner.
</details>

#### Soru 10: Eşleştirme
Her Well-Architected sütununu tanımıyla eşleştirin: **Reliability · Security · Cost Optimization · Operational Excellence · Performance Efficiency**

1. Arıza anında çalışmaya devam etme ve toparlanma
2. Veriyi ve sistemi koruma (en az yetki, şifreleme)
3. Gereksiz harcamayı azaltma (doğru boyut, kullanılmayanı kapatma)
4. IaC, CI/CD ve izleme ile sağlıklı dağıtım ve operasyon
5. Talebe göre ölçekleme ve kaynakları verimli kullanma

<details><summary>Cevap</summary>

**1 → Reliability · 2 → Security · 3 → Cost Optimization · 4 → Operational Excellence · 5 → Performance Efficiency.** Azure Advisor'ın beş kategorisi (Reliability, Security, Cost, Operational Excellence, Performance) bu sütunlarla örtüşür (→ C1.14).
</details>

#### Soru 11: Tek seçim
Bir kurum, buluta geçiş yolculuğunu **strateji, plan, hazırlık, benimseme, yönetişim ve yönetim** aşamalarıyla yönlendirecek bir **rehber** arıyor. Hangi çerçeve uygundur?

- A) Azure Well-Architected Framework
- B) Microsoft Cloud Adoption Framework
- C) Azure Policy
- D) Azure Advisor

<details><summary>Cevap</summary>

**B.** CAF, kurumun buluta geçiş yolculuğunu yönlendirir. WAF tek bir iş yükünün mimari kalitesini değerlendirir; Policy ve Advisor araçlardır.
</details>

#### Soru 12: Tek seçim
Bir dokümanda "WAF'ın beş sütunu" ifadesi geçiyor ve bağlam bir **iş yükü mimarisinin kalitesini** değerlendirmekten söz ediyor. Buradaki WAF nedir?

- A) Web Application Firewall
- B) Well-Architected Framework
- C) Windows Admin Framework
- D) Workload Assessment Feature

<details><summary>Cevap</summary>

**B.** Mimari kalite ve beş sütun bağlamında WAF = Well-Architected Framework. Web Application Firewall bir ağ güvenliği servisidir (→ C1.17/C1.18).
</details>

#### Soru 13: Tek seçim
Bir ekip, buluta taşınan iş yüklerinin güvenle yerleşeceği, **management group'lar, Policy atamaları, ağ topolojisi ve kimlik tabanı** önceden hazır bir ortam kurmak istiyor. Bu kavramın adı nedir?

- A) Availability zone
- B) Azure landing zone
- C) Proximity placement group
- D) Resource lock

<details><summary>Cevap</summary>

**B.** Azure landing zone, CAF'nin "Ready" aşamasında hazırlanan, yönetişim ve ağ tabanıyla önceden yapılandırılmış ortamdır.
</details>

#### Soru 14: Senaryo
Bir ekip, üretimde kullandığı bir servisin bir özelliğinin **emekliye ayrılacağını** son anda öğrenip acil geçiş yapmak zorunda kaldı. Gelecekte bu tür duyuruları **zamanında** fark etmek için aşağıdakilerden **hangisi en uygundur**?

- A) Azure Updates'i ve Service Health'teki *health advisories* bildirimlerini takip etmek
- B) Yalnızca Basic destek planına güvenmek
- C) Resource group'lara lock koymak
- D) Cost Management bütçesi oluşturmak

<details><summary>Cevap</summary>

**A.** Retirement duyuruları Azure Updates'te yayımlanır ve Service Health'te aksiyon gerektiren değişiklikler (health advisories) olarak gelir; uyarı kuralları ve e-posta ile izlenir (→ C1.14).
</details>

#### Soru 15: Senaryo
Bir e-ticaret şirketi, **üretim** iş yükleri için **7/24 teknik destek** istiyor ve bütçesi sınırlı. Aynı şirket dev/test aboneliği için yalnızca iş saatinde destek yeterli buluyor. Hangi kombinasyon en makul olur?

- A) Her iki abonelik için Basic
- B) Üretim → Standard (veya üstü), dev/test → Developer
- C) Üretim → Developer, dev/test → Unified
- D) Her iki abonelik için Unified

<details><summary>Cevap</summary>

**B.** Üretim için 7/24 teknik destek veren plan (Standard ve üstü), üretim dışı ortam için Developer yeterlidir. Basic teknik destek vermez; Unified büyük kuruma yöneliktir ve bu senaryo için gereksiz pahalıdır. Güncel plan özelliklerini sayfadan doğrula.
</details>

---

**Puanlama:** 14-15 → hazırsın · 11-13 → README 3.1 (plan ağacı), 3.2 (SLA) ve 3.4 (CAF/WAF) bölümlerini tekrar et · < 11 → konuyu baştan çalış; 3.5'teki karar tablosunu ve 3.6'daki composite SLA hesabını yap.


---

# BÖLÜM 5: AZ-900 Deneme Sınavı ve Sınav Stratejisi

---

## 5.0 Kapsamlı AZ-900 Deneme Sınavı (50 Soru) ve Sınav Stratejisi

> **Kapsam:** Tüm Sınav Alanları (C1.1 – C1.22) · **Hedef:** Gerçek sınav simülasyonu (≥ 40/50 - %80)

### 5.1 Sınav Stratejisi ve Hazırlık Rehberi

### 1. Ne?

AZ-900 bir **genişlik** sınavıdır: derin yapılandırma değil, kavramların ve Azure servislerinin **doğru senaryoya eşleştirilmesi** ölçülür. Bu konu üç parçadan oluşur:

| Parça | Dosya | Amaç |
|---|---|---|
| **Deneme sınavı** | aşağıdaki quiz bölümüne | Alan ağırlıklarına göre dağıtılmış **50 özgün soru**; her soru bir konu koduna (C1.x) bağlı |
| **Puanlama ve tekrar planı** | `quiz.md` sonu | Alan bazlı puan tablosu; yanlışları C1.x'e eşleyip zayıf konuları tekrar etme |
| **Sınav stratejisi** | Bu README | Soru türleri, eleme yöntemi, anahtar kelimeler, zaman yönetimi, sınav günü |

**Sınavın genel çerçevesi** (ayrıntılar değişebilir, resmî sayfadan doğrula):

| | |
|---|---|
| Alanlar ve ağırlıklar | *Describe cloud concepts* %25-30 · *Describe Azure architecture and services* %35-40 · *Describe Azure management and governance* %30-35 |
| Geçme puanı | **700 / 1000** (ölçeklenmiş puan; "doğru yüzdesi" ile birebir aynı değildir) |
| Soru sayısı ve süre | Fundamentals sınavlarında genellikle birkaç düzine soru ve kısa bir süre (yaklaşık 45 dakika civarı) bulunur; kesin değerler sınav sayfasında ve sınav başında gösterilir |
| Soru türleri | Tek seçim, çoklu seçim ("iki/üç doğru cevap seçin"), **evet/hayır** tabloları, eşleştirme/sürükle-bırak, cümle tamamlama (açılır liste), kısa senaryolar |
| Nerede | **Pearson VUE** test merkezi veya **çevrimiçi gözetimli** (online proctored, evden/ofisten) |
| Geçerlilik | Fundamentals sertifikalarının **süresi dolmaz** (yenileme gerekmez) |

---

### 2. Neden?

- **Bilmek ile sınavda göstermek farklıdır.** Konuları tek tek çalışırken her soru bağlamıyla gelir; gerçek sınavda C1.12'den bir soru, C1.4'ten bir soruyla yan yana gelir ve benzer şıklar (Policy/RBAC/lock, zone/pair, Advisor/Service Health) birbirine karışır. Karışık bir deneme bunu ortaya çıkarır.
- **Zayıf alanı ölçmek:** Alan bazlı puan, "hangi C1.x'i tekrar edeyim?" sorusunu veriyle cevaplar. Ağırlığı en büyük alan (%35-40, mimari ve servisler) genellikle en çok yanlışın çıktığı alandır.
- **Format alışkanlığı:** Evet/hayır tabloları ve "NOT/EXCEPT" soruları, bilgiden çok **okuma disiplini** ister; denemede yakalanan dikkat hatası sınavda tekrarlanmaz.

---

### 3. Nasıl?

#### 3.1 Deneme sınavını kullanma (önerilen akış)

1. **Hazırlık:** C1.1–C1.14 konularının quiz'lerinde ≥ %80 al. Eksik konu varken denemeye girme; deneme bir ölçüm aracıdır, ders değil.
2. **Zamanlı ve kapalı kitap çöz:** 50 soru için **45 dakika** ayır (soru başına ~50 saniye). Not alma; kodu ve README'leri açma. Cevaplarını bir kâğıda/dosyaya yaz, `<details>` bloklarını açma.
3. **Puanla:** Sonda cevap anahtarı yok — her sorunun cevabı kendi `<details>` bloğundadır; tek tek kontrol et ve **alan bazlı puan tablosunu** doldur.
4. **Yanlışları sınıflandır:** Her yanlış için (a) hangi C1.x, (b) **bilgi eksiği mi, dikkat hatası mı** ("NOT"u kaçırdım, "otomatik"i görmedim). Bilgi eksiği → konu README'si ve quiz'i; dikkat hatası → 3.2'deki teknikler.
5. **Tekrar et ve resmî testi çöz:** Zayıf konuları tekrar ettikten sonra Microsoft Learn'deki **ücretsiz resmî Practice Assessment**'ı kapalı kitap çöz. Hem bu denemede hem resmî testte istikrarlı biçimde **%80+** alıyorsan sınava hazırsın.
6. Bir hafta sonra denemeyi **sırayı değiştirerek** tekrar çöz; ezber değil anlama ölçülsün diye gerekçeleri kendi cümlelerinle açıkla.

#### 3.2 Soru çözme teknikleri

| Teknik | Nasıl? | Örnek |
|---|---|---|
| **Önce soru kökünü oku** | Senaryonun son cümlesi asıl soruyu taşır: "en düşük maliyet", "en az yönetim yükü", "en yüksek erişilebilirlik" | "…**en az yönetim yükü** ile web uygulaması" → PaaS (App Service) |
| **NOT / EXCEPT soruları** | Soruyu olumluya çevir, her şıkkı "bu doğru mu?" diye işaretle; tek "yanlış" olanı seç | "Aşağıdakilerden hangisi resource group için **doğru değildir**?" |
| **Eleme yöntemi** | Kesin yanlışları at (kategori dışı, farklı katman, mutlak ifade); kalan iki şık arasında anahtar kelimeye bak | Zone ↔ pair: "datacenter arızası" mı "bölgesel felaket" mi? |
| **Anahtar kelime avı** | Kelime → kavram eşlemesi (aşağıdaki tablo) | "internetten geçmeden" → ExpressRoute |
| **Evet/hayır tablosu** | Her satır **bağımsızdır**; bir satırın cevabı diğerini etkilemez. "Hepsi evet olamaz" gibi bir kural yoktur | — |
| **Mutlak ifadeler** | "Her zaman", "hiçbir zaman", "yalnızca", "tüm" — ifade tek bir istisnayla çöker mi? | "SaaS'ta **tüm** sorumluluk Microsoft'tadır" → Hayır |
| **"En iyi" cevap** | Birden çok şık teknik olarak çalışabilir; senaryonun kısıtını (maliyet, yönetim, zaman) en iyi karşılayanı seç | VM de web uygulaması çalıştırır ama "OS yönetmeden" denmişse App Service |
| **Çoklu seçim** | Kaç cevap istendiğini oku ("iki doğru cevap"); kısmi puan olabilir ama varsayma | — |
| **Tahmin** | Boş bırakılan soru puan getirmez; yanlış cevap için ek ceza yoktur — her soruyu cevapla | — |
| **İşaretle ve geç** | 60 saniyeyi aşan soruyu işaretle (*mark for review*), sona bırak | — |

**Anahtar kelime → cevap tablosu** (tüm konulardan):

| Kelime/ifade | Büyük olasılıkla | Konu |
|---|---|---|
| "her zaman müşteride" | veri, cihazlar, hesaplar/kimlikler | C1.1 |
| "ön yatırım yok", "kullandıkça öde" | OpEx / consumption-based | C1.1 |
| "otomatik olarak büyü/küçül" | elasticity / autoscale | C1.2 |
| "kesintisiz devam etti" ↔ "felaketten toparlandı" | high availability ↔ reliability | C1.2 |
| "OS'a tam erişim", "lift-and-shift" | IaaS (VM) | C1.3, C1.5 |
| "yalnızca kod çalışırken ödeme", "olay tetiklemeli" | serverless (Functions) | C1.3, C1.6 |
| "datacenter arızası" ↔ "bölgesel felaket" | availability zone ↔ region pair | C1.4 |
| "politikayı tüm subscription'lara" | management group | C1.4, C1.12 |
| "update/fault domain" | availability set | C1.5 |
| "tek container, orkestrasyon yok" | Container Instances | C1.6 |
| "uzaktan Windows masaüstü" | Azure Virtual Desktop | C1.6 |
| "internetten geçmeden, özel bağlantı" | ExpressRoute | C1.7 |
| "iki VNet'i bağla" | VNet peering | C1.7 |
| "bölgesel felakette de okunabilir" | RA-GRS / RA-GZRS | C1.8 |
| "terabaytlarca veri, ağ yetersiz" | Data Box | C1.8 |
| "klasik domain join, LDAP, Kerberos" | Entra Domain Services | C1.9 |
| "koşula göre MFA iste" | Conditional Access | C1.9 |
| "asla güvenme, her zaman doğrula" | Zero Trust | C1.10 |
| "henüz kurulmamış iş yükünün maliyeti" | Pricing Calculator | C1.11 |
| "maliyeti departmana göre raporla" | tag | C1.11 |
| "izin verilmeyen bölgede kaynak oluşturmayı engelle" | Azure Policy (Deny) | C1.12 |
| "kazara silinmeyi önle" | resource lock (CanNotDelete) | C1.12 |
| "aynı ortamı tekrar tekrar tutarlı kur" | ARM template / Bicep (IaC) | C1.13 |
| "başka buluttaki sunucuyu Azure'dan yönet" | Azure Arc | C1.13 |
| "kişiselleştirilmiş öneri" | Azure Advisor | C1.14 |
| "benim bölgemi etkileyen Azure olayı" | Service Health | C1.14 |

#### 3.3 Zaman yönetimi

- Sınav başlamadan önce gösterilen **soru sayısı ve süreyi** not et; soru başına düşen süreyi hesapla (≈ süre / soru sayısı).
- İlk geçişte bildiklerini hızla cevapla, emin olmadıklarını **işaretle**; ikinci geçişe en az %15 süre bırak.
- Uzun senaryolu sorularda önce son cümleyi (asıl soruyu), sonra senaryoyu oku.
- Bazı soru grupları (ör. aynı senaryoya bağlı ardışık sorular) cevaplandıktan sonra **geri dönülemeyebilir**; sınav ekranındaki uyarıyı oku.

#### 3.4 Sınav günü

| | Test merkezi (Pearson VUE) | Çevrimiçi gözetimli (online proctored) |
|---|---|---|
| Ortam | Hazır bilgisayar, gözetmen yerinde | Kendi bilgisayarın; webcam ve mikrofon zorunlu |
| Hazırlık | Kimlik belgesi, randevudan önce varış | **Sistem testi** (önceden), sessiz ve boş bir oda, masada hiçbir şey olmaması, check-in fotoğrafları |
| Risk | Ulaşım | İnternet kopması, ortam kurallarının ihlali (birinin odaya girmesi, ekrandan uzaklaşmak) |
| Öneri | İlk sınavında stres azaltır | Esnek saat; ama check-in için 15-30 dakika erken başla |

- Sınav arayüzünü önceden görmek için Microsoft'un **exam sandbox** (sınav deneyimi demosu) sayfasını dene.
- Kimlikteki ad, sınav profilindeki adla **aynı** olmalıdır.
- Sonuç genellikle sınav bitiminde ekranda görünür; başarısızlıkta alan bazlı performans raporu verilir — o rapor, bu repodaki C1.x tekrar planına doğrudan eşlenir. Yeniden girme bekleme süreleri için resmî **retake policy**'ye bak.
- Microsoft zaman zaman ücretsiz eğitim etkinlikleri (ör. *Virtual Training Days*) ve öğrenci programlarıyla sınav indirimi/ücretsiz hak sunar; güncel durumu resmî sayfalardan kontrol et.

---

### 4. Gerçek dünya senaryosu

Bir geliştirici AZ-900'e beş haftalık plana göre hazırlanıyor (→ C1 modül süresi):

- **Hafta 1-4:** Her hafta 3-4 konu; her konunun sonunda `quiz.md`'yi çözüp ≥ %80 alana kadar tekrar. Laboratuvarları kendi aboneliğinde, bütçe alarmı kurulu olarak yapıyor ve her laboratuvarı `az group delete` ile kapatıyor.
- **Hafta 5, gün 1:** Bu deneme sınavı zamanlı: **38/50**. Alan tablosu: cloud concepts 13/14, mimari ve servisler 12/19, yönetim ve yönetişim 13/17. Yanlışların 5'i C1.7 ve C1.8'den (ağ, storage redundancy), 3'ü C1.12'den (Policy/RBAC/lock).
- **Gün 2-3:** C1.7, C1.8, C1.12 README'lerini ve quiz'lerini tekrar; anahtar kelime tablosunu kendi el yazısıyla yeniden yazıyor.
- **Gün 4:** Resmî Practice Assessment: %84. Deneme sınavını farklı sırayla tekrar: 46/50.
- **Gün 6:** Çevrimiçi gözetimli sınav; önceki gün sistem testini yapmış, odayı hazırlamış.

---

### 5. Sınav perspektifi

**Microsoft'un sevdiği kalıplar ve tuzaklar** (tüm modülden en sık karışanlar):

| Karışan çift | Ayırt eden soru |
|---|---|
| Availability zone ↔ region pair | Arıza datacenter düzeyinde mi, bölge düzeyinde mi? |
| Availability set ↔ zone ↔ scale set | Datacenter içi donanım/bakım mı, datacenter arızası mı, otomatik ölçek mi? |
| Scalability ↔ elasticity | "Otomatik" var mı? |
| HA ↔ reliability | "Devam etti" mi, "toparlandı" mı? |
| VPN Gateway ↔ ExpressRoute | İnternet üzerinden şifreli mi, özel hat mı? |
| LRS ↔ ZRS ↔ GRS ↔ GZRS ↔ RA-* | Hangi arızaya dayanmalı, ikincilden okuma gerekli mi? |
| Entra ID ↔ Entra Domain Services | Modern kimlik mi, klasik AD özellikleri (domain join, LDAP, Kerberos) mi? |
| B2B ↔ B2C (External ID) | İş ortağı mı, müşteri mi? |
| RBAC ↔ Policy ↔ lock | Kim ne yapabilir mi, kaynak nasıl olmalı mı, silinmesin/değişmesin mi? |
| Pricing Calculator ↔ TCO | Yeni iş yükünün tahmini mi, on-prem ile karşılaştırma mı? |
| Advisor ↔ Service Health ↔ Monitor | Öneri mi, Azure olayı mı, benim telemetrim mi? |
| Defender for Cloud ↔ Advisor | Güvenlik duruşu ve tehdit koruması mı, beş kategoride genel öneri mi? |

**Sınavda olmayanlar:** komut sözdizimi ezberi, fiyat rakamları, hesaplama ağırlıklı sorular (SLA hesabı nadiren ve basit), portal ekranlarının yeri. Laboratuvar (hands-on lab) bölümü fundamentals sınavlarında genellikle yoktur.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Braindump/"çıkmış soru" ezberlemek | Microsoft'un sınav sözleşmesine aykırıdır, sertifika iptaline yol açabilir; sorular değişir — kavramı öğren |
| Yalnızca deneme sınavı çözerek çalışmak | Önce konu (README + laboratuvar), sonra quiz, en son deneme |
| Evet/hayır satırlarını birbirine bağlamak | Her satır bağımsız bir doğru/yanlış sorusudur |
| "NOT"u kaçırmak | Soru kökündeki olumsuzluğu altını çizerek (zihnen) oku |
| Bir soruda 3 dakika takılmak | İşaretle, geç, sona dön |
| Online sınavda ortam kurallarını hafife almak | Masada telefon/kâğıt, odaya giren biri veya ekrandan uzun süre uzaklaşmak sınavın iptaline yol açabilir |
| Müfredat tarihini kontrol etmemek | Study guide'daki "skills measured as of" tarihini ve değişiklik kaydını sınavdan önce kontrol et |

---

### 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Erken sınav tarihi almak | Motivasyon ve odak | Hazır değilsen tekrar ücreti ve bekleme süresi |
| Test merkezi | Teknik risk yok, sakin ortam | Ulaşım, sınırlı saat |
| Online proctored | Esnek saat ve yer | Bağlantı/ortam riski, sıkı kurallar |
| Çok sayıda deneme sınavı | Format rahatlığı | Ezber riski: aynı soruları tanımaya başlarsın; gerekçeyi açıklayamıyorsan bilmiyorsundur |
| Laboratuvarları atlamak | Zaman kazancı | Kavramlar soyut kalır; senaryo sorularında "hangisi gerçekten böyle çalışır?" sezgisi zayıflar |

---

### 8. Kaynaklar

- [Microsoft Certified: Azure Fundamentals (AZ-900)](https://learn.microsoft.com/en-us/credentials/certifications/azure-fundamentals/) — sınav planlama, Practice Assessment, randevu
- [AZ-900 study guide (skills measured + değişiklik kaydı)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Azure Fundamentals learning path'leri](https://learn.microsoft.com/en-us/training/courses/az-900t00)
- [Exam duration and question types](https://learn.microsoft.com/en-us/credentials/support/exam-duration-exam-experience) · [Exam sandbox](https://aka.ms/examdemo)
- [Online exams with Pearson VUE](https://learn.microsoft.com/en-us/credentials/certifications/online-exams) · [Retake policy](https://learn.microsoft.com/en-us/credentials/support/retake-policy)
- [Credentials support (sınav politikaları, gizlilik sözleşmesi, sınav bütünlüğü)](https://learn.microsoft.com/en-us/credentials/support/)

---

### 5.2 50 Soruluk AZ-900 Deneme Sınavı

> Strateji ve kullanım: [README.md](README.md) · Müfredat: *skills measured as of July 20, 2026*
>
> **Kurallar:** 45 dakika, kapalı kitap. Cevaplarını ayrı bir yere yaz; `<details>` bloklarını sınav bitene kadar açma. Her sorunun etiketi, sorunun **konusunu** (C1.x) ve **alanını** gösterir: **A** = Describe cloud concepts · **B** = Describe Azure architecture and services · **C** = Describe Azure management and governance.
>
> Dağılım resmî ağırlıklara göre: **A 14 soru (%28) · B 19 soru (%38) · C 17 soru (%34)**. Sorular **özgündür**; gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Gerçek sınavın puanı ölçeklenmiştir (geçme 700/1000); buradaki yüzde yalnızca bir göstergedir.

---

#### Soru 1: `[A · C1.1]` Tek seçim
Aşağıdakilerden hangisi bir **operational expenditure (OpEx)** örneğidir?

- A) Yeni bir datacenter binası inşa etmek
- B) Beş yıl kullanılacak depolama ünitesi satın almak
- C) Kullanılan saat kadar ödenen bir Azure sanal makinesi
- D) Sunucu odası için jeneratör satın almak

<details><summary>Cevap</summary>

**C.** Kullanıma göre dönem dönem ödenen bulut hizmeti OpEx'tir. A, B, D fiziksel altyapıya baştan yapılan yatırımlardır (CapEx).
</details>

#### Soru 2: `[B · C1.4]` Tek seçim
Bir uygulamanın, bir Azure region'ı içindeki **tek bir datacenter'ın** tamamen devre dışı kalmasından etkilenmemesi isteniyor. Ne kullanılmalıdır?

- A) Region pair
- B) Availability zone'lar
- C) Resource group
- D) Management group

<details><summary>Cevap</summary>

**B.** Availability zone'lar bir region içinde fiziksel olarak ayrı (bağımsız güç, soğutma, ağ) datacenter gruplarıdır; datacenter arızasına karşı korur. Region pair **bölgesel** felakete karşıdır.
</details>

#### Soru 3: `[C · C1.12]` Tek seçim
Bir şirket, çalışanlarının yalnızca **West Europe ve North Europe** region'larında kaynak oluşturabilmesini, diğer bölgelerdeki denemelerin **reddedilmesini** istiyor. Ne kullanmalıdır?

- A) Resource lock
- B) Azure Policy
- C) Azure RBAC
- D) Resource tag

<details><summary>Cevap</summary>

**B.** *Allowed locations* politikası (Deny etkisi) izin verilmeyen bölgede oluşturma isteğini reddeder. RBAC **kimin** ne yapabileceğini belirler, kaynağın **özelliğini** (bölgesini) kısıtlamaz.
</details>

#### Soru 4: `[A · C1.3]` Eşleştirme
Her ürünü hizmet türüyle eşleştirin: **IaaS · PaaS · SaaS**

1. Azure Cosmos DB
2. Azure Virtual Machine Scale Sets
3. Microsoft 365
4. Azure App Service

<details><summary>Cevap</summary>

**1 → PaaS · 2 → IaaS · 3 → SaaS · 4 → PaaS.** Yönetilen veritabanı ve web platformu PaaS; VM grupları IaaS; hazır ofis uygulamaları SaaS.
</details>

#### Soru 5: `[B · C1.8]` Tek seçim
Bir storage account'taki verinin, birincil bölgede **zone arızasına** dayanması ve **bölgesel bir felaket sırasında da okunabilmesi** gerekiyor. Hangi redundancy seçeneği **en uygundur**?

- A) LRS
- B) ZRS
- C) GRS
- D) RA-GZRS

<details><summary>Cevap</summary>

**D.** GZRS birincil bölgede zone'lara (ZRS) ve ikincil bölgeye kopyalar; **RA-** ön eki ikincil bölgeden sürekli okuma erişimi verir. GRS'de birincil bölge LRS'tir (zone arızasına karşı korumaz) ve ikincil failover olmadan okunamaz.
</details>

#### Soru 6: `[C · C1.14]` Tek seçim
Bir yönetici, Microsoft'un **kendi aboneliğindeki** kaynakları etkileyecek **planlı bakım** etkinlikleri hakkında bilgi ve bildirim almak istiyor. Hangi servisi kullanmalıdır?

- A) Azure Advisor
- B) Azure Service Health
- C) Microsoft Defender for Cloud
- D) Azure Policy

<details><summary>Cevap</summary>

**B.** Service Health, abonelik ve region kapsamında hizmet sorunlarını, planlı bakımı ve sağlık tavsiyelerini gösterir; Service Health alert'leri ile bildirim gönderir.
</details>

#### Soru 7: `[B · C1.9]` Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Authentication, bir kullanıcının kimliğini doğrulama işlemidir. | ☐ | ☐ |
| b) Authorization, kimliği doğrulanmış bir kullanıcının hangi kaynaklara ne düzeyde erişebileceğini belirler. | ☐ | ☐ |
| c) Bir şifre ve bir güvenlik sorusu birlikte kullanıldığında bu, iki farklı faktörlü kimlik doğrulamadır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Şifre ve güvenlik sorusu ikisi de **bildiğin** şeydir — aynı faktör türü. MFA farklı türlerden faktör ister (bildiğin + sahip olduğun + olduğun).
</details>

#### Soru 8: `[A · C1.2]` Tek seçim
Bir video platformu, akşam saatlerinde artan izleyici sayısına göre sunucu sayısını **otomatik** artırıp gece **otomatik** azaltıyor. Bu, bulutun hangi faydasıdır?

- A) Elasticity
- B) Reliability
- C) Governance
- D) Predictability

<details><summary>Cevap</summary>

**A.** Ölçeklemenin yüke göre otomatik yapılması elasticity'dir.
</details>

#### Soru 9: `[C · C1.11]` Tek seçim
Bir şirket, kendi datacenter'ındaki sunucuları, depolamayı, enerjiyi ve BT işçiliğini **Azure'a taşıma maliyetiyle karşılaştırmak** istiyor. Hangi araç bu amaçla tasarlanmıştır?

- A) Azure Pricing Calculator
- B) Total Cost of Ownership (TCO) Calculator
- C) Azure Advisor
- D) Azure Monitor

<details><summary>Cevap</summary>

**B.** TCO Calculator on-prem altyapının toplam sahip olma maliyetini Azure ile karşılaştırır. Pricing Calculator ise Azure'da kurulacak bir iş yükünün tahmini maliyetini verir. (TCO Calculator'ın güncel müfredattaki yerini study guide'dan kontrol et.)
</details>

#### Soru 10: `[B · C1.5]` Tek seçim
Azure'un **planlı bakım** sırasında tüm VM'leri aynı anda yeniden başlatmasını önlemek ve VM'leri ortak güç/ağ donanımını paylaşmayacak şekilde dağıtmak için ne kullanılır?

- A) Availability set
- B) Resource group
- C) Proximity placement group
- D) Azure Arc

<details><summary>Cevap</summary>

**A.** Availability set, VM'leri **update domain**'lere (planlı bakımda ayrı yeniden başlatma) ve **fault domain**'lere (ayrı güç/ağ donanımı) dağıtır. Proximity placement group ise VM'leri düşük gecikme için birbirine **yakın** yerleştirir.
</details>

#### Soru 11: `[C · C1.13]` Tek seçim
Bir şirket, kendi datacenter'ındaki ve başka bir bulut sağlayıcısındaki Linux sunucularını Azure portal'dan görmek ve onlara **Azure Policy** uygulamak istiyor. Ne kullanmalıdır?

- A) Azure Arc
- B) Azure Virtual Desktop
- C) VPN Gateway
- D) Azure Migrate

<details><summary>Cevap</summary>

**A.** Azure Arc, Azure dışındaki sunucuları, Kubernetes kümelerini ve veri servislerini Azure Resource Manager'a yansıtır; Policy, RBAC, Defender for Cloud ve Monitor bu kaynaklara uygulanabilir.
</details>

#### Soru 12: `[A · C1.1]` Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) IaaS'ta işletim sistemi güncellemeleri müşterinin sorumluluğundadır. | ☐ | ☐ |
| b) SaaS'ta cihazların (mobil ve PC) güvenliği Microsoft'un sorumluluğundadır. | ☐ | ☐ |
| c) PaaS'ta fiziksel ağın güvenliği Microsoft'un sorumluluğundadır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Cihazlar, veri ve hesaplar her modelde müşteride; fiziksel katmanlar bulutun her modelinde Microsoft'ta; OS IaaS'ta müşteride.
</details>

#### Soru 13: `[B · C1.7]` Tek seçim
Bir banka, datacenter'ı ile Azure arasında **public internet üzerinden geçmeyen**, yüksek bant genişlikli ve öngörülebilir gecikmeli bir bağlantı istiyor. Ne kullanmalıdır?

- A) Site-to-site VPN Gateway
- B) Point-to-site VPN
- C) Azure ExpressRoute
- D) VNet peering

<details><summary>Cevap</summary>

**C.** ExpressRoute, bağlantı sağlayıcısı üzerinden **özel** bir bağlantıdır. VPN Gateway şifrelidir ama public internet üzerinden geçer; peering iki VNet'i bağlar.
</details>

#### Soru 14: `[C · C1.12]` Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) CanNotDelete (Delete) lock bulunan bir kaynağın ayarları değiştirilebilir. | ☐ | ☐ |
| b) Bir resource group'a uygulanan lock, içindeki kaynaklara miras kalır. | ☐ | ☐ |
| c) Owner rolüne sahip bir kullanıcı, ReadOnly lock bulunan bir kaynağı lock'ı kaldırmadan silebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Delete lock yalnızca silmeyi engeller; lock'lar alt kapsamlara miras kalır ve rol yetkisinden bağımsızdır — Owner bile önce lock'ı kaldırmalıdır.
</details>

#### Soru 15: `[B · C1.4]` Çoklu seçim (iki doğru cevap)
Resource group'lar hakkında **hangi iki** ifade doğrudur?

- A) Bir kaynak aynı anda iki resource group'a ait olabilir.
- B) Bir resource group silindiğinde içindeki tüm kaynaklar silinir.
- C) Resource group'lar iç içe yerleştirilebilir.
- D) Bir resource group'taki kaynaklar farklı region'larda olabilir.

<details><summary>Cevap</summary>

**B ve D.** Bir kaynak yalnızca bir gruptadır (A yanlış); gruplar iç içe olmaz (C yanlış); grup silinince içindekiler silinir; gruptaki kaynaklar farklı bölgelerde olabilir.
</details>

#### Soru 16: `[A · C1.2]` Tek seçim
SLA'sı **%99,99** olan bir servis için bir ayda izin verilen en fazla kesinti yaklaşık ne kadardır?

- A) ~4 dakika
- B) ~22 dakika
- C) ~44 dakika
- D) ~7 saat

<details><summary>Cevap</summary>

**A.** `730 sa × 0,0001 ≈ 0,073 sa ≈ 4,4 dk`. ~22 dk %99,95'e, ~44 dk %99,9'a karşılık gelir.
</details>

#### Soru 17: `[B · C1.6]` Tek seçim
Bir geliştirici tek bir container image'ını, VM veya orkestrasyon kurmadan, **en hızlı ve en basit** şekilde Azure'da çalıştırmak istiyor. Hangi servis **en uygundur**?

- A) Azure Kubernetes Service
- B) Azure Container Instances
- C) Azure Virtual Desktop
- D) Azure Virtual Machines

<details><summary>Cevap</summary>

**B.** Container Instances tek container'ı saniye bazında ücretle, orkestrasyonsuz çalıştırır. AKS çok sayıda container'ın orkestrasyonu içindir.
</details>

#### Soru 18: `[C · C1.11]` Tek seçim
Finans ekibi, Azure maliyetlerini **departman ve maliyet merkezine** göre raporlamak istiyor. Kaynaklara ne eklenmelidir?

- A) Resource lock
- B) Tag
- C) Availability zone
- D) Management group

<details><summary>Cevap</summary>

**B.** `department`, `costCenter` gibi tag'ler Cost Management'ta maliyeti filtreleyip gruplamaya olanak tanır. (Tag'ler varsayılan olarak resource group'tan miras alınmaz; Policy ile zorunlu kılınabilir.)
</details>

#### Soru 19: `[B · C1.10]` Sıralama
Defense in depth katmanlarını **dıştan içe** doğru sıralayın: Ağ · Veri · Fiziksel güvenlik · Uygulama · Kimlik ve erişim · Compute · Çevre (perimeter)

<details><summary>Cevap</summary>

**Fiziksel güvenlik → Kimlik ve erişim → Çevre (perimeter) → Ağ → Compute → Uygulama → Veri.** En içteki katman, saldırganın asıl hedefi olan veridir.
</details>

#### Soru 20: `[C · C1.13]` Tek seçim
Bir ekip, geliştirme, test ve üretim ortamlarını **her seferinde aynı ve tekrarlanabilir** biçimde kurmak, altyapı tanımını sürüm kontrolünde tutmak istiyor. Hangi yaklaşım **en uygundur**?

- A) Kaynakları Azure portal'da elle oluşturmak
- B) ARM template veya Bicep dosyalarıyla infrastructure as code
- C) Azure Advisor önerilerini uygulamak
- D) Resource lock kullanmak

<details><summary>Cevap</summary>

**B.** Deklaratif şablonlar (ARM JSON, Bicep) aynı sonucu tekrar tekrar (idempotent) üretir, kod gibi sürümlenir ve incelenir. Portal tekrarlanabilir otomasyon için uygun değildir.
</details>

#### Soru 21: `[A · C1.3]` Çoklu seçim (iki doğru cevap)
Serverless bilgi işlem hakkında **hangi iki** ifade doğrudur?

- A) Kod yalnızca bir olay tetiklediğinde çalışır.
- B) Müşteri, kodun çalıştığı sunucuların işletim sistemini yamalar.
- C) Ücret, genellikle çağrı sayısı ve çalışma süresine göre hesaplanır.
- D) Sunucular fiziksel olarak hiç yoktur.

<details><summary>Cevap</summary>

**A ve C.** Serverless olay tetiklemelidir ve kullanım kadar ücretlendirilir; sunucular vardır ama sağlanmaları ve yönetimleri sağlayıcıdadır (B ve D yanlış).
</details>

#### Soru 22: `[B · C1.8]` Tek seçim
Bir şirket, internet bağlantısının yavaş ve pahalı olduğu bir lokasyondan **80 TB** arşiv verisini Azure'a taşımak istiyor. Hangi seçenek **en uygundur**?

- A) AzCopy
- B) Azure Storage Explorer
- C) Azure Data Box
- D) Azure File Sync

<details><summary>Cevap</summary>

**C.** Data Box, büyük veriyi **fiziksel cihazla** taşır; ağ yetersizken tercih edilir. AzCopy ve Storage Explorer ağ üzerinden kopyalar; File Sync sürekli senkronizasyon içindir.
</details>

#### Soru 23: `[C · C1.14]` Eşleştirme
Her ihtiyacı doğru araçla eşleştirin: **Azure Advisor · Application Insights · Log Analytics · Resource Health**

1. Bir web uygulamasındaki yavaş isteklerin ve exception'ların izlenmesi
2. Tek bir VM'in geçmiş erişilebilirlik durumunun ve platform kaynaklı bir sorun olup olmadığının görülmesi
3. Birden çok kaynaktan toplanan log'ların KQL ile sorgulanması
4. Güvenilirlik ve maliyet için kişiselleştirilmiş önerilerin alınması

<details><summary>Cevap</summary>

**1 → Application Insights · 2 → Resource Health · 3 → Log Analytics · 4 → Azure Advisor.**
</details>

#### Soru 24: `[A · C1.3]` Senaryo
Bir şirketin muhasebe yazılımı, belirli bir işletim sistemi sürümü ve özel bir donanım sürücüsü gerektiriyor; yazılımın kodu değiştirilemiyor. Şirket bu yazılımı buluta taşımak istiyor. Hangi hizmet türü **en uygundur**?

- A) SaaS
- B) PaaS
- C) Serverless
- D) IaaS

<details><summary>Cevap</summary>

**D.** OS ve sürücü üzerinde tam kontrol ve kod değişikliği olmadan taşıma (lift-and-shift) IaaS'tır (Azure Virtual Machines).
</details>

#### Soru 25: `[A · C1.1]` Senaryo
Bir hastane, hasta kayıtlarını yasal zorunluluk nedeniyle kendi datacenter'ında tutmaya devam edecek; randevu web sitesini ise talebe göre ölçeklenebilmesi için Azure'da çalıştıracak. Bu hangi cloud modelidir?

- A) Public cloud
- B) Private cloud
- C) Hybrid cloud
- D) Multicloud

<details><summary>Cevap</summary>

**C.** Kurum içi altyapı ile public cloud'un birlikte kullanımı hybrid cloud'dur.
</details>

#### Soru 26: `[B · C1.4]` Tek seçim
Bir kuruluşun 40 subscription'ı var. Güvenlik ekibi bir Azure Policy atamasını ve bir RBAC rol atamasını **tüm subscription'lara tek seferde** uygulamak istiyor. Bu atamalar hangi kapsamda yapılmalıdır?

- A) Her resource group'ta ayrı ayrı
- B) Management group
- C) Her kaynakta ayrı ayrı
- D) Availability zone

<details><summary>Cevap</summary>

**B.** Management group'lar subscription'ları gruplar; burada yapılan Policy ve RBAC atamaları altındaki tüm subscription'lara miras kalır.
</details>

#### Soru 27: `[C · C1.12]` Tek seçim
Bir şirket, on-prem veritabanlarında, Azure'daki veri depolarında ve SaaS uygulamalarında bulunan **hassas veriyi (ör. kredi kartı numaraları) otomatik keşfetmek, sınıflandırmak** ve verinin haritasını çıkarmak istiyor. Hangi çözüm **en uygundur**?

- A) Microsoft Purview
- B) Azure Policy
- C) Microsoft Entra ID
- D) Azure Advisor

<details><summary>Cevap</summary>

**A.** Purview; veri yönetişimi, risk ve uyumluluk çözümleri ailesidir: veri haritası/kataloğu, otomatik keşif ve sınıflandırma, lineage.
</details>

#### Soru 28: `[B · C1.7]` Tek seçim
Aynı Azure aboneliğinde, farklı region'lardaki iki VNet'in kaynaklarının **Microsoft omurgası üzerinden, özel IP'lerle** birbiriyle iletişim kurması isteniyor. En basit çözüm nedir?

- A) Azure DNS
- B) Global VNet peering
- C) ExpressRoute
- D) Public endpoint

<details><summary>Cevap</summary>

**B.** VNet peering (farklı bölgelerde global peering) iki VNet'i internete çıkmadan bağlar. Peering transitive değildir ve adres alanları çakışmamalıdır.
</details>

#### Soru 29: `[A · C1.2]` Eşleştirme
Her senaryoyu doğru faydayla eşleştirin: **Governance · Security · Manageability · Predictability**

1. Önümüzdeki altı ayın bulut harcaması kullanım geçmişine göre tahmin edildi.
2. Kaynaklar standart şablonlarla dağıtılıyor ve kurumsal kurallara uymayanlar raporlanıyor.
3. Kaynaklar Azure CLI ve REST API ile yönetiliyor.
4. Altyapı DDoS saldırılarına karşı sağlayıcı tarafından korunuyor.

<details><summary>Cevap</summary>

**1 → Predictability** (maliyet) · **2 → Governance** · **3 → Manageability** (*in* the cloud) · **4 → Security**.
</details>

#### Soru 30: `[C · C1.11]` Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Aynı Azure servisi farklı region'larda farklı fiyatlara sahip olabilir. | ☐ | ☐ |
| b) Azure'dan dışarı (egress) giden veri trafiği ücretlendirilebilir. | ☐ | ☐ |
| c) Cost Management'ta tanımlanan bir budget aşıldığında kaynaklar otomatik olarak durdurulur. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Budget yalnızca uyarı üretir (e-posta, action group); harcamayı kendiliğinden durdurmaz.
</details>

#### Soru 31: `[B · C1.5]` Tek seçim
Bir geliştirici, hafta sonu kullanmayacağı bir test VM'inin **compute ücretini durdurmak**, ama disklerini ve yapılandırmasını korumak istiyor. Ne yapmalıdır?

- A) VM'i işletim sisteminden kapatmak (shutdown)
- B) VM'i Azure'dan durdurup **deallocate** etmek
- C) VM'in resource group'unu silmek
- D) VM'e bir ReadOnly lock eklemek

<details><summary>Cevap</summary>

**B.** Deallocate edilen VM compute ücreti almaz; disk ücreti sürer. Yalnızca OS içinden kapatmak, VM'in ayrılmış kalmasına ve ücretlenmeye devam etmesine yol açabilir; grubu silmek her şeyi siler.
</details>

#### Soru 32: `[C · C1.13]` Tek seçim
Bir yönetici, kendi bilgisayarına hiçbir araç kurmadan, tarayıcı üzerinden **kimliği hazır** bir komut satırında Azure CLI veya Azure PowerShell komutları çalıştırmak istiyor. Ne kullanmalıdır?

- A) Azure Cloud Shell
- B) Azure Arc
- C) Azure Advisor
- D) Azure Storage Explorer

<details><summary>Cevap</summary>

**A.** Cloud Shell portal'dan veya `shell.azure.com`'dan erişilen, Bash (Azure CLI) ve PowerShell seçenekli, kurulum gerektirmeyen etkileşimli kabuktur.
</details>

#### Soru 33: `[A · C1.3]` Senaryo
Bir yazılım ekibi kendi geliştirdiği web uygulamasını yayınlayacak; işletim sistemi yaması, sunucu kurulumu ve runtime yönetimiyle uğraşmak istemiyor. Hangi hizmet türü **en az yönetim yüküyle** bu ihtiyacı karşılar?

- A) IaaS
- B) PaaS
- C) SaaS
- D) On-premises

<details><summary>Cevap</summary>

**B.** Kendi kodunu çalıştıran ekip için en az yönetim yükü PaaS'tır (ör. App Service). SaaS hazır uygulamadır; kendi uygulamanı yayınlamaya uygun değildir.
</details>

#### Soru 34: `[B · C1.9]` Tek seçim
Bir kullanıcının bir resource group'taki **tüm kaynakları oluşturup yönetebilmesi**, ancak **başkalarına erişim verememesi** gerekiyor. Hangi built-in rol **en uygundur**?

- A) Owner
- B) Contributor
- C) Reader
- D) User Access Administrator

<details><summary>Cevap</summary>

**B.** Contributor erişim yönetimi hariç her şeyi yapabilir. Owner erişim de verebilir; Reader yalnızca görüntüler; User Access Administrator yalnızca erişimi yönetir.
</details>

#### Soru 35: `[B · C1.6]` Tek seçim
Bir şirket, çalışanlarının kişisel dizüstü bilgisayarlarından ve tabletlerinden, verinin cihazda değil bulutta kaldığı **Windows masaüstlerine** erişmesini istiyor. Hangi servis **en uygundur**?

- A) Azure App Service
- B) Azure Virtual Desktop
- C) Azure Functions
- D) Azure Container Apps

<details><summary>Cevap</summary>

**B.** Azure Virtual Desktop bulutta masaüstü ve uygulama sanallaştırmasıdır; Windows çok oturumlu desteği ve Entra ID ile kimlik/MFA sunar.
</details>

#### Soru 36: `[C · C1.14]` Tek seçim
Bir geliştirme ekibi, Azure'da ve kendi datacenter'ında çalışan web uygulamalarının **istek süreleri, hata oranları ve dış servis bağımlılıkları** hakkında telemetri toplamak istiyor. Ne kullanmalıdır?

- A) Azure Service Health
- B) Application Insights
- C) Azure Advisor
- D) Azure Status

<details><summary>Cevap</summary>

**B.** Application Insights, Azure Monitor'un uygulama performans izleme özelliğidir ve Azure dışında çalışan uygulamaları da izleyebilir.
</details>

#### Soru 37: `[A · C1.1]` Tek seçim
Bir araştırma ekibi, kesintiye uğrarsa kaldığı yerden devam edebilen ve zaman açısından esnek olan büyük bir hesaplama işini **en düşük maliyetle** çalıştırmak istiyor. Hangi seçenek **en uygundur**?

- A) Spot VM'ler
- B) 3 yıllık reserved instance
- C) Pay-as-you-go standart VM'ler
- D) Dedicated host

<details><summary>Cevap</summary>

**A.** Spot VM'ler Azure'un boşta kapasitesini büyük indirimle sunar; Azure kapasiteye ihtiyaç duyunca kesilebilir — kesintiye dayanıklı işler için idealdir.
</details>

#### Soru 38: `[B · C1.4]` Tek seçim
ABD federal kurumlarının özel uyumluluk gereksinimleri için ana Azure'dan **fiziksel ve mantıksal olarak ayrılmış** bulut örneği hangisidir?

- A) Bir availability zone
- B) Azure Government (sovereign region)
- C) Bir region pair
- D) Bir management group

<details><summary>Cevap</summary>

**B.** Azure Government, ABD kamu kurumları için ayrılmış bir **sovereign region**'dır. Diğer örnek: 21Vianet tarafından işletilen Azure China.
</details>

#### Soru 39: `[C · C1.12]` Tek seçim
Bir subscription'da **Owner** rolüne sahip bir kullanıcı, subscription'a atanmış bir Azure Policy'nin **Deny** etkisiyle yasakladığı bir VM boyutunu oluşturmaya çalışıyor. Ne olur?

- A) Owner rolü Policy'yi geçersiz kıldığı için VM oluşturulur.
- B) İstek reddedilir; Policy, kullanıcının rolünden bağımsız olarak uygulanır.
- C) VM oluşturulur ama Advisor uyarı verir.
- D) VM oluşturulur ve otomatik olarak silinir.

<details><summary>Cevap</summary>

**B.** RBAC kullanıcının **ne yapabileceğini**, Policy kaynağın **nasıl olabileceğini** belirler; Deny etkisi Owner'ın isteğini de reddeder.
</details>

#### Soru 40: `[B · C1.8]` Tek seçim
**Archive** access tier'daki bir blob'a hemen erişmeye çalışan bir kullanıcıyla ilgili hangisi doğrudur?

- A) Blob, Hot tier'daki gibi anında okunur.
- B) Blob önce daha sıcak bir tier'a **rehydrate** edilmelidir; bu işlem saatler sürebilir.
- C) Archive tier'daki blob'lar okunamaz, yalnızca silinebilir.
- D) Archive tier yalnızca Azure Files için geçerlidir.

<details><summary>Cevap</summary>

**B.** Archive çevrimdışı bir katmandır; okumak için Hot/Cool/Cold'a rehydrate gerekir ve saatler alabilir. Depolaması en ucuz, erişimi en pahalı/yavaş katmandır.
</details>

#### Soru 41: `[A · C1.2]` Tek seçim
Bir uygulama, bulunduğu region'ın tamamını etkileyen bir felaketten sonra başka bir region'da **yeniden ayağa kaldırıldı** ve hizmet vermeye devam etti. Bu en iyi hangi faydayla açıklanır?

- A) Scalability
- B) Reliability
- C) Elasticity
- D) Manageability

<details><summary>Cevap</summary>

**B.** Arızadan/felaketten **toparlanıp** çalışmaya devam etmek reliability'dir. "Kesinti olmadan sürdü" denseydi high availability olurdu.
</details>

#### Soru 42: `[C · C1.13]` Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure portal, Azure CLI ve Azure PowerShell'den gelen istekler Azure Resource Manager üzerinden işlenir. | ☐ | ☐ |
| b) ARM template'leri JSON biçiminde, deklaratif şablonlardır. | ☐ | ☐ |
| c) Bicep dosyaları dağıtımdan önce ARM JSON'a dönüştürülür (derlenir). | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Evet.** Tüm yönetim istekleri ARM'dan geçer (RBAC, Policy, lock, tag burada uygulanır); ARM template JSON ve deklaratiftir; Bicep, ARM template'in okunabilir DSL'idir ve ARM JSON'a derlenir.
</details>

#### Soru 43: `[B · C1.7]` Tek seçim
Bir şirket, VNet'indeki uygulamaların bir storage account'a **özel bir IP adresiyle** erişmesini ve storage account'un **internetten erişimini kapatmayı** istiyor. Ne kullanmalıdır?

- A) Public endpoint
- B) Private endpoint (Azure Private Link)
- C) Azure DNS public zone
- D) Point-to-site VPN

<details><summary>Cevap</summary>

**B.** Private endpoint, PaaS servisine VNet içinden özel IP ile erişim sağlar; trafik Microsoft ağında kalır ve public erişim kapatılabilir.
</details>

#### Soru 44: `[C · C1.11]` Tek seçim
Bir proje yöneticisi, aylık Azure harcaması belirlenen limitin **%80'ine ulaştığında e-posta** almak istiyor. Ne yapılandırmalıdır?

- A) Azure Advisor önerisi
- B) Microsoft Cost Management'ta bir budget ve uyarı eşiği
- C) Resource lock
- D) Azure Service Health alert

<details><summary>Cevap</summary>

**B.** Cost Management budget'ları tanımlanan eşiklerde (ör. %80, %100) bildirim gönderir.
</details>

#### Soru 45: `[B · C1.9]` Tek seçim
Bir şirket, kullanıcıların **yalnızca şirket ağı dışından** oturum açtıklarında MFA ile doğrulanmasını, şirket ağından girişte ise MFA istenmemesini istiyor. Hangi özellik kullanılmalıdır?

- A) Azure RBAC
- B) Microsoft Entra Conditional Access
- C) Resource lock
- D) Azure Policy

<details><summary>Cevap</summary>

**B.** Conditional Access, sinyallere (konum/IP, cihaz, uygulama, risk) göre karar verir: izin ver, MFA iste veya engelle.
</details>

#### Soru 46: `[C · C1.14]` Tek seçim
Bir yönetici, tek bir Azure SQL veritabanının dün gece 40 dakika erişilemez olmasının **Azure platformundaki bir sorundan** kaynaklanıp kaynaklanmadığını öğrenmek istiyor. Nereye bakmalıdır?

- A) Resource Health
- B) Azure Pricing Calculator
- C) Azure Advisor score
- D) Azure Policy compliance panosu

<details><summary>Cevap</summary>

**A.** Resource Health, tek bir kaynağın mevcut ve geçmiş sağlık durumunu ve sorunun platform kaynaklı olup olmadığını gösterir.
</details>

#### Soru 47: `[A · C1.1]` Tek seçim
Aşağıdakilerden hangisi **private cloud**'u en iyi tanımlar?

- A) Birden çok kuruluşun paylaştığı, sağlayıcıya ait altyapı
- B) Yalnızca tek bir kuruluşun kullanımına ayrılmış bulut altyapısı
- C) Birden çok public cloud sağlayıcısının birlikte kullanılması
- D) Yalnızca SaaS uygulamalarından oluşan bir ortam

<details><summary>Cevap</summary>

**B.** Private cloud tek bir kuruluşa ayrılmıştır (kendi datacenter'ı veya ayrılmış barındırma). A public cloud'u, C multicloud'u tanımlar.
</details>

#### Soru 48: `[B · C1.5]` Tek seçim
Bir web katmanının, CPU kullanımına göre **otomatik olarak** 2 ile 20 arasında **aynı yapılandırmada** VM çalıştırması ve yükün bu VM'lere dağıtılması gerekiyor. Ne kullanılmalıdır?

- A) Availability set
- B) Virtual Machine Scale Sets
- C) Proximity placement group
- D) Tek bir büyük VM

<details><summary>Cevap</summary>

**B.** VM Scale Sets aynı yapılandırmadaki VM'leri yük dengeleyiciyle birlikte çalıştırır ve autoscale kurallarıyla sayıyı otomatik değiştirir.
</details>

#### Soru 49: `[C · C1.12]` Tek seçim
Bir güvenlik ekibi, birlikte uygulanması gereken **30 farklı policy tanımını** tek bir paket halinde bir management group'a atamak istiyor. Bu paketin adı nedir?

- A) Resource lock
- B) Policy initiative
- C) Role assignment
- D) Blueprint lock

<details><summary>Cevap</summary>

**B.** Initiative (policy set), birlikte yönetilen policy tanımlarının grubudur (ör. bir güvenlik benchmark'ı) ve tek atamayla uygulanır.
</details>

#### Soru 50: `[A · C1.2]` Evet / Hayır

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
| C1.1 Cloud computing, modeller, fiyatlandırma | 1, 12, 25, 37, 47 | | [README](#11-cloud-computing-cloud-modelleri-ve-fiyatlandırma) · [quiz](#-bölüm-11-quiz-soruları) |
| C1.2 Cloud'un faydaları, SLA | 8, 16, 29, 41, 50 | | [README](#12-bulutun-temel-faydaları) · [quiz](#-bölüm-12-quiz-soruları) |
| C1.3 IaaS, PaaS, SaaS, serverless | 4, 21, 24, 33 | | [README](#13-bulut-hizmet-türleri-iaas-paas-saas-serverless) · [quiz](#-bölüm-13-quiz-soruları) |
| C1.4 Temel mimari bileşenler | 2, 15, 26, 38 | | [README](#21-azure-temel-mimari-bileşenleri) · [quiz](#-bölüm-21-quiz-soruları) |
| C1.5 Compute ve VM'ler | 10, 31, 48 | | [README](#22-azure-compute-ve-virtual-machines) · [quiz](#-bölüm-22-quiz-soruları) |
| C1.6 Uygulama barındırma | 17, 35 | | [README](#23-uygulama-barındırma-app-service-containerlar-functions-avd) · [quiz](#-bölüm-23-quiz-soruları) |
| C1.7 Ağ servisleri | 13, 28, 43 | | [README](#24-ağ-servisleri-vnet-peering-vpn-expressroute-dns) · [quiz](#-bölüm-24-quiz-soruları) |
| C1.8 Storage | 5, 22, 40 | | [README](#25-azure-storage-blob-files-redundancy-tiering) · [quiz](#-bölüm-25-quiz-soruları) |
| C1.9 Kimlik ve erişim | 7, 34, 45 | | [README](#26-kimlik-ve-erişim-entra-id-mfa-conditional-access-rbac) · [quiz](#-bölüm-26-quiz-soruları) |
| C1.10 Güvenlik | 19 | | [README](#27-güvenlik-zero-trust-defense-in-depth-defender-for-cloud) · [quiz](#-bölüm-27-quiz-soruları) |
| C1.11 Maliyet yönetimi | 9, 18, 30, 44 | | [README](#31-maliyet-yönetimi-pricing-calculator-tco-budgets) · [quiz](#-bölüm-31-quiz-soruları) |
| C1.12 Yönetişim ve uyumluluk | 3, 14, 27, 39, 49 | | [README](#32-yönetişim-ve-uyumluluk-policy-locks-tags-purview) · [quiz](#-bölüm-32-quiz-soruları) |
| C1.13 Yönetim ve dağıtım araçları | 11, 20, 32, 42 | | [README](#33-kaynak-yönetme-ve-dağıtma-araçları-portal-cli-powershell-arc-bicep) · [quiz](#-bölüm-33-quiz-soruları) |
| C1.14 İzleme araçları | 6, 23, 36, 46 | | [README](#34-izleme-araçları-monitor-log-analytics-service-health-advisor) · [quiz](#-bölüm-34-quiz-soruları) |

**Tekrar planı:** Her yanlış için *bilgi eksiği* mi *dikkat hatası* mı olduğunu not et. Bir konuda **2 veya daha fazla** bilgi eksiği varsa o konunun README'sini ve laboratuvarını baştan yap, ardından konu quiz'ini ≥ %80 olana kadar çöz. Dikkat hataları için [5.1 Sınav Stratejisi](#32-soru-çözme-teknikleri)'deki teknikleri tekrar oku.

---

## 📚 Resmî Kaynaklar ve Sınav Taktikleri

### Resmî Microsoft Kaynakları

- [AZ-900 Study Guide (Skills Measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — AZ-900 Öğrenme Yolu](https://learn.microsoft.com/en-us/training/paths/azure-fundamentals-describe-cloud-concepts/)
- [Microsoft Practice Assessment (Ücretsiz Resmî Deneme)](https://learn.microsoft.com/en-us/credentials/certifications/azure-fundamentals/practice/assessment?assessment-type=practice&assessmentId=23)
- [Azure Architecture Center](https://learn.microsoft.com/en-us/azure/architecture/)
- [Azure Pricing Calculator](https://azure.microsoft.com/pricing/calculator/)
- [Azure SLA Dokümantasyonu](https://www.microsoft.com/licensing/docs/view/Service-Level-Agreements-SLA-for-Online-Services)

### Sınavda En Çok Hayat Kurtaran Kurallar

1. **Veri ve Kimlik Kuralı:** Hangi bulut modeli olursa olsun (SaaS dahil) **veri, cihazlar ve kimlikler HER ZAMAN müşteridedir**.
2. **Fiziksel Donanım Kuralı:** Bulutun her modelinde fiziksel datacenter, host sunucular ve fiziksel ağ **HER ZAMAN Microsoft'tadır**.
3. **Otomatik = Elasticity:** Eğer soruda 'yüke göre kendiliğinden/otomatik arttı/azaldı' diyorsa cevap kesinlikle **elasticity**'dir.
4. **Kesinti vs Toparlanma:** 'Sistem arızaya rağmen kesintisiz çalıştı' → **High Availability (HA)**. 'Sistem çöktü ama hızla toparlandı' → **Reliability**.
5. **Daha Güçlü vs Daha Çok:** Tek sunucunun CPU/RAM'ini artırmak → **Scale Up (Vertical)**. Sunucu sayısını artırmak → **Scale Out (Horizontal)**.
6. **Miras Alma:** Azure Policy, RBAC ve Resource Lock üst kapsamdan alt kapsama **miras kalır**. **Tag'ler (etiketler) varsayılan olarak miras KALMAZ**.
7. **Preview Servisler:** Public Preview veya Private Preview servislerin **SLA garantisi YOKTUR** ve üretim (production) için önerilmez.
8. **Stopped vs Deallocated:** Bir VM'i OS içinden kapatırsanız durumu 'Stopped' olur ve compute ücreti **ödenmeye devam eder**. Ücreti durdurmak için Azure Portal/CLI üzerinden 'Deallocate' edilmelidir.
