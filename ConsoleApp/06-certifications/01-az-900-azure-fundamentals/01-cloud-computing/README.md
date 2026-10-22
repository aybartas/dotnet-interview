# C1.1 Cloud Computing, Cloud Modelleri ve Fiyatlandırma

> 📁 `ConsoleApp/06-certifications/01-az-900-azure-fundamentals/01-cloud-computing/` · Sınav alanı: **Describe cloud concepts (%25-30)** · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe cloud computing* — cloud computing'i tanımla · shared responsibility model · cloud modelleri (public, private, hybrid) · her modelin kullanım senaryosu · consumption-based model · cloud fiyatlandırma modellerini karşılaştır · serverless *(serverless → [C1.3](../03-cloud-service-types/README.md))*. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

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

**Cloud computing**, bilgi işlem hizmetlerinin — sunucu, depolama, ağ, veritabanı, yazılım, analitik, yapay zekâ — **internet üzerinden, ihtiyaç anında ve kullanım kadar ücretle** sunulmasıdır. Donanımı sen almazsın, kurmazsın, bakımını yapmazsın; bir sağlayıcının (Azure) datacenter'larındaki kapasiteyi kiralarsın.

Bu konu AZ-900'ün ilk alanının temelidir ve dört soruyu cevaplar:

| Soru | Kavram |
|------|--------|
| Bulut tam olarak nedir, datacenter'dan farkı ne? | Tanım + NIST'in 5 niteliği |
| Bulutta güvenlikten/işletimden kim sorumlu? | **Shared responsibility model** ⭐ |
| Bulut nerede çalışır, kime aittir? | **Public / private / hybrid** (+ multicloud) |
| Nasıl ödenir? | **Consumption-based model**, **CapEx vs OpEx** ⭐, fiyatlandırma modelleri |

Kod: `ConsoleApp` projesinde [`Sections/`](Sections/) altında 5 bölüm ve bir sorumluluk "soru-cevap" aracı. `ConsoleApp` klasöründe:

```bash
dotnet run -- C1.1              # section list
dotnet run -- C1.1 all          # all sections
dotnet run -- C1.1 who os paas  # "Who is responsible for the operating system in PaaS?" → Microsoft
```

---

## 2. Neden?

- **Sınav ağırlığı:** "Describe cloud concepts" alanı sınavın **%25-30**'u; bu alanın en çok soru üreten iki tablosu (shared responsibility ve CapEx/OpEx) bu konudadır. Sonraki her konu (IaaS/PaaS/SaaS, SLA, maliyet, güvenlik) bu kavramların üzerine kurulur.
- **Gerçek işte:** "Bu sunucuyu buluta taşıyalım mı?" kararı; sorumluluğun kime geçtiğini (yama, yedek, güvenlik), hangi modelin regülasyona uyduğunu ve maliyetin nasıl değiştiğini bilmeyi gerektirir.
- **Yaygın yanlış:** "Buluta taşıdık, güvenlik artık Microsoft'un işi" düşüncesi gerçek veri sızıntılarının bir numaralı sebeplerindendir — shared responsibility model tam olarak bunu düzeltir.

---

## 3. Nasıl?

### 3.1 Cloud computing nedir? NIST'in beş niteliği ([`Section01`](Sections/Section01_WhatIsCloud.cs))

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

### 3.2 Shared responsibility model ⭐ ([`Section02`](Sections/Section02_SharedResponsibility.cs), [`ResponsibilityMatrix.cs`](ResponsibilityMatrix.cs))

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
dotnet run -- C1.1 who account saas           # Accounts and identities · SaaS → Customer
dotnet run -- C1.1 who physical-network iaas  # Physical network · IaaS → Microsoft
```

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

### 3.3 Public, private, hybrid cloud ([`Section03`](Sections/Section03_CloudModels.cs))

| Model | Kime ait / nerede | Artıları | Eksileri | Tipik senaryo |
|---|---|---|---|---|
| **Public cloud** | Sağlayıcının altyapısı; birçok kuruluş paylaşır (mantıksal izolasyonla) | CapEx yok, dakikalar içinde başla, neredeyse sınırsız ölçek, kullandıkça öde | Fiziksel altyapı üzerinde kontrol yok; bazı regülasyonlar için ek değerlendirme | Yeni web uygulaması, değişken yük, startup |
| **Private cloud** | Tek kuruluşa ait (kendi datacenter'ı veya ayrılmış barındırma); bulut niteliklerini (self-service, otomasyon) kurum içinde sunar | Tam kontrol, sıkı uyumluluk, özel donanım | Donanım, bakım ve personel maliyeti/sorumluluğu kurumda; ölçek donanımla sınırlı | Sıkı regülasyonlu kurum, gizli veriler |
| **Hybrid cloud** | Public + private birlikte, aralarında bağlantıyla | Hassas veri/eski sistem yerinde, esnek yük bulutta; **cloud bursting** (yerel kapasite dolunca taşan yük buluta); kademeli taşıma | İki ortamı birlikte yönetme karmaşıklığı | "Veri ülkede/kurumda kalmalı ama web katmanı ölçeklenmeli" |
| *(bağlam)* **Multicloud** | Birden çok public cloud sağlayıcısı (ör. Azure + AWS) | Sağlayıcıya bağımlılığı azaltma, her sağlayıcının güçlü servisi | Farklı araçlar/beceriler, yönetim karmaşıklığı | Şirket birleşmesi, belirli bir servise ihtiyaç |

- Hybrid ve multicloud ortamları tek yerden yönetmek için **Azure Arc** (Azure dışındaki sunucu, Kubernetes ve veritabanlarını Azure'dan yönetme) → C1.13.
- Datacenter'ını buluta bağlamanın ağ tarafı (VPN Gateway, ExpressRoute) → C1.7.
- Hybrid ≠ multicloud: hybrid **kendi altyapın + public cloud**; multicloud **birden çok public cloud**. Bir kuruluş ikisi birden olabilir.

### 3.4 Consumption-based model: CapEx vs OpEx ⭐ ([`Section04`](Sections/Section04_ConsumptionModel.cs))

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

### 3.5 Cloud fiyatlandırma modelleri ([`Section05`](Sections/Section05_PricingModels.cs))

| Model | Nasıl çalışır | Ne zaman? |
|---|---|---|
| **Pay-as-you-go** | Taahhüt yok; saniye/dakika/saat veya işlem/GB başına ödeme | Değişken veya öngörülemeyen yük, deneme, kısa süreli proje |
| **Reserved instances** / **savings plan** | 1 veya 3 yıllık **taahhüt** karşılığı ciddi indirim (Microsoft'un ifadesiyle reservation'da "en fazla %72"); kaynak kullanılmasa da taahhüt ödenir | Yıllarca 7/24 çalışacak **sabit** iş yükü |
| **Spot** | Azure'un boşta kalan kapasitesi, büyük indirimle ("en fazla %90"); Azure kapasiteye ihtiyaç duyunca VM **kesilir** (eviction) | Kesintiye dayanıklı toplu iş, render, CI, test |
| **Free tier / ücretsiz hesap** | Yeni hesaba sınırlı süreli kredi + bazı servislerde ücretsiz kullanım miktarları; bazı servislerin kalıcı ücretsiz katmanı (ör. App Service F1) | Öğrenme, deneme |
| **Dev/test fiyatlandırması** | Visual Studio aboneleri için geliştirme/test aboneliklerinde indirimli fiyatlar | Üretim dışı ortamlar |

> İndirim oranları pazarlama rakamlarıdır ve zamanla değişir; sınav oranı değil **hangi modelin hangi iş yüküne uyduğunu** sorar. Azure Hybrid Benefit (mevcut Windows Server/SQL Server lisansını bulutta kullanmak), maliyeti etkileyen faktörler, Pricing Calculator ve Cost Management → C1.11.

### 3.6 Pratik — ilk adımlar: hesap, ilk resource group ve bütçe

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

## 4. Gerçek dünya senaryosu

Orta ölçekli bir sigorta şirketi, kendi datacenter'ındaki sunucuların donanım ömrü dolunca karar veriyor:

1. **Müşteri portalı** (kasım-aralıkta yük 5 katına çıkıyor) → **public cloud**, tüketim bazlı. Zirve için yıl boyu boşta duracak donanım almak yerine kapasite talebi izliyor (CapEx → OpEx).
2. **Poliçe veritabanı** (regülasyon gereği belirli verilerin kurum kontrolünde kalması isteniyor) → bir süre **kurum içinde** kalıyor; portal bu veriye güvenli bir bağlantıyla (VPN/ExpressRoute) erişiyor → toplam mimari **hybrid cloud**.
3. **E-posta ve ofis uygulamaları** → **SaaS** (Microsoft 365); sunucu yönetimi tamamen kalkıyor, ama kullanıcı hesapları, MFA ve dosya paylaşım izinleri **hâlâ şirketin** sorumluluğunda.
4. **Gece çalışan risk hesaplamaları** → **spot VM**'ler: kesilirse iş kaldığı yerden yeniden başlıyor, maliyet çok düşük. Portalın 7/24 çalışan çekirdek kapasitesi → **reservation**.
5. Taşıma başlamadan önce abonelikte **bütçe alarmı** kuruluyor; ilk ay test ortamında unutulan büyük bir VM bu sayede fark ediliyor.

---

## 5. Sınav perspektifi

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

## 6. Yaygın hatalar

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

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| On-prem → public cloud | CapEx yok, hızlı başlangıç, esnek ölçek, fiziksel işletme yükü yok | Fiziksel kontrol yok; maliyet disiplini (izleme, bütçe) gerekir; internet/ağ bağımlılığı |
| Public yerine private | Tam kontrol, özel uyumluluk | Donanım/personel maliyeti, sınırlı ölçek, yavaş kapasite artışı |
| Hybrid | Regülasyona uyum + bulut esnekliği, kademeli taşıma | İki ortamı birlikte işletme, bağlantı ve kimlik entegrasyonu karmaşıklığı |
| IaaS → PaaS → SaaS | Daha az sorumluluk ve yönetim yükü | Daha az kontrol ve özelleştirme (→ C1.3) |
| Pay-as-you-go → reservation | Sabit yükte büyük indirim | Taahhüt: kullanmasan da ödersin; esneklik azalır |
| Pay-as-you-go → spot | Çok düşük fiyat | Her an kesilebilir; yalnızca kesintiye dayanıklı işler |

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900) — resmî hedefler ve değişiklik kaydı
- [Microsoft Learn — Describe cloud computing](https://learn.microsoft.com/en-us/training/modules/describe-cloud-compute/) (AZ-900 öğrenme yolu, 1. modül)
- [Shared responsibility in the cloud](https://learn.microsoft.com/en-us/azure/security/fundamentals/shared-responsibility)
- [NIST SP 800-145 — The NIST Definition of Cloud Computing](https://csrc.nist.gov/pubs/sp/800/145/final)
- [Azure reservations](https://learn.microsoft.com/en-us/azure/cost-management-billing/reservations/save-compute-costs-reservations) · [Azure Spot Virtual Machines](https://learn.microsoft.com/en-us/azure/virtual-machines/spot-vms)
- [Tutorial: Create and manage budgets](https://learn.microsoft.com/en-us/azure/cost-management-billing/costs/tutorial-acm-create-budgets) · [`az consumption budget`](https://learn.microsoft.com/en-us/cli/azure/consumption/budget)
