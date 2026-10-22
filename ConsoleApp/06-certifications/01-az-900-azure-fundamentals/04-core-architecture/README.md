# C1.4 Azure Temel Mimari Bileşenleri

> 📁 `ConsoleApp/06-certifications/01-az-900-azure-fundamentals/04-core-architecture/` · Sınav alanı: **Describe Azure architecture and services (%35-40)** · Önkoşul: [C1.2](../02-cloud-benefits/README.md) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe the core architectural components of Azure* — regions, region pairs, sovereign regions · availability zones · Azure datacenters · resources ve resource groups · subscriptions · management groups · resource group / subscription / management group hiyerarşisi. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

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

Azure'un mimarisi iki eksende anlatılır:

| Eksen | Bileşenler | Soruyu cevaplar |
|---|---|---|
| **Fiziksel altyapı** | Datacenter → **availability zone** → **region** → **region pair** · **sovereign region** | Kaynağım fiziksel olarak **nerede** çalışıyor, hangi arızaya dayanıyor? |
| **Mantıksal (yönetim) yapı** | **Resource** → **resource group** → **subscription** → **management group** (→ Entra tenant) | Kaynaklarımı **nasıl düzenler**, faturalar ve yetkilendiririm? |

```
Fiziksel                                   Mantıksal (yönetim hiyerarşisi)
────────                                   ───────────────────────────────
Coğrafya (ör. Avrupa)                      Microsoft Entra tenant
 └─ Region pair: North Europe ↔ West Europe  └─ Root management group
     └─ Region: West Europe                      └─ Management group(lar) (≤ 6 seviye)
         └─ Availability zone 1, 2, 3                └─ Subscription
             └─ Datacenter(lar)                          └─ Resource group
                                                             └─ Resource (VM, VNet, storage…)
```

Bu konunun kodu yok; pratik, README'deki `az` CLI laboratuvarıdır (3.7).

---

## 2. Neden?

- **Sınav:** "Describe Azure architecture and services" alanı sınavın en ağır alanıdır (**%35-40**); availability zone ↔ region pair ve resource group kuralları bu alanın en sık sorulan maddeleridir.
- **Gerçek işte:** Region seçimi gecikmeyi, maliyeti, veri ikametgâhını (regülasyon) ve dayanıklılığı belirler. Resource group/subscription tasarımı ise kimin neye erişeceğini, faturanın nasıl bölüneceğini ve bir kaynağın ne zaman güvenle silinebileceğini belirler. Yanlış bir `az group delete` bütün bir ortamı siler.
- **Sonraki konular:** RBAC (C1.9), Policy ve lock (C1.12), tag ve maliyet (C1.11) bu hiyerarşinin **kapsamlarına** (management group, subscription, resource group, kaynak) uygulanır ve aşağı doğru miras alınır.

---

## 3. Nasıl?

### 3.1 Datacenter ve region

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

### 3.2 Availability zones ⭐

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

### 3.3 Region pairs ⭐

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

### 3.4 Sovereign regions

Ana (public) Azure'dan **yasal ve işletimsel olarak ayrılmış** Azure örnekleri:

| Sovereign cloud | Kim için | Özellik |
|---|---|---|
| **Azure Government** | ABD kamu kurumları ve iş ortakları | Fiziksel ve ağ olarak ayrı; yalnızca taranmış (screened) ABD personeli işletir; FedRAMP, DoD gibi uyumluluk gereksinimleri |
| **Azure China** (Azure operated by 21Vianet) | Çin'de hizmet | Microsoft değil **21Vianet** işletir; Çin regülasyonlarına uyum; global Azure'dan fiziksel olarak ayrı |

Sovereign bulutlarda portal adresi, servis listesi ve fiyatlar global Azure'dan farklı olabilir.

### 3.5 Resources ve resource groups ⭐

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

### 3.6 Subscriptions ve management groups ⭐

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
└─ Root management group              ← "Tüm kaynaklar yalnızca AB region'larında" (Policy)
   ├─ MG: Platform
   │  └─ sub-connectivity
   └─ MG: Uygulamalar                  ← "Güvenlik ekibi her yerde Reader" (RBAC)
      ├─ MG: Prod
      │  └─ sub-prod
      │     └─ rg-shop-prod            ← CanNotDelete lock (→ C1.12)
      │        └─ VM, SQL Database, storage
      └─ MG: Dev/Test
         └─ sub-dev
            └─ rg-shop-dev
```

### 3.7 Pratik — region keşfi, resource group yaşam döngüsü, hiyerarşi

Bu laboratuvar **neredeyse ücretsizdir**: boş bir VNet ücret üretmez, boş bir storage account kuruşluk maliyetlidir ve hemen silinir. Bütçe alarmın kurulu değilse önce C1.1 / 3.6'yı yap.

```bash
az login

# 1) Region keşfi
az account list-locations --output table
# Yalnızca fiziksel region'lar ve eşleşik bölgeleri
az account list-locations \
  --query "[?metadata.regionType=='Physical'].{Region:name, Ad:displayName, Cift:metadata.pairedRegion[0].name, Cografya:metadata.geographyGroup}" \
  --output table
# Bir region'da hangi zone'larda VM boyutu var? (C1.2 / 3.5 ile aynı komut)
az vm list-skus --location westeurope --size Standard_B1s --zone --output table

# 2) Resource group yaşam döngüsü: iki kaynak, tek silme
az group create --name rg-az900-c14 --location westeurope
az network vnet create --resource-group rg-az900-c14 --name vnet-c14 --address-prefix 10.14.0.0/16
az storage account create --resource-group rg-az900-c14 --name stc14$RANDOM \
  --location northeurope --sku Standard_LRS        # dikkat: RG West Europe, storage North Europe → izinli
az resource list --resource-group rg-az900-c14 --query "[].{Ad:name, Tur:type, Region:location}" --output table

# 3) Grubu sil → içindeki iki kaynak da gider
az group delete --name rg-az900-c14 --yes
az resource list --resource-group rg-az900-c14 --output table 2>/dev/null || echo "Grup ve içindeki kaynaklar silindi"

# 4) Hiyerarşindeki yerini bul
az account show --query "{Tenant:tenantId, Subscription:name, Id:id}" --output table
az account management-group list --output table   # yetkin yoksa boş/hata döner — bu da bir bilgi
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

## 4. Gerçek dünya senaryosu

Bir Avrupa e-ticaret şirketi Azure'a geçerken mimarisini kuruyor:

- **Region:** Müşterilerin çoğu Almanya ve Hollanda'da; veri AB içinde kalmalı → birincil **West Europe**, eşleşik bölge **North Europe** (aynı coğrafya, sıralı güncellemeler).
- **Availability zones:** Web katmanı West Europe'ta **üç zone'a** yayılmış VM'lerde; veritabanı **zone-redundant** yapılandırmada. Bir datacenter'ın elektriği kesildiğinde site ayakta kalıyor.
- **Region pair:** Ürün görselleri **GRS** storage'da; West Europe'un tamamı etkilenirse kopya North Europe'ta. Kritik sipariş servisinin ikinci kopyası North Europe'ta hazır bekliyor.
- **Hiyerarşi:** Root management group'a "yalnızca AB region'ları" politikası atanıyor. Prod ve NonProd ayrı management group'larda; her ortamın ayrı subscription'ı var, böylece finans ekibi maliyeti ortam bazında görüyor.
- **Resource group'lar:** Her uygulama ve ortam ayrı bir RG'de (`rg-siparis-prod`, `rg-siparis-test`). Test ortamı her akşam `az group delete` ile silinip sabah şablondan yeniden kuruluyor (→ C1.13); prod RG'lerinde silme kilidi var (→ C1.12).

---

## 5. Sınav perspektifi

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

## 6. Yaygın hatalar

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

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Tek zone (zonal) yerleşim | En düşük gecikme ve maliyet | Datacenter arızasında kesinti |
| Zone'lara yayılmak | Datacenter arızasına dayanıklılık, daha yüksek SLA | Ek kaynak, zone'lar arası trafik ücreti ve küçük gecikme |
| İkinci region (pair) | Bölgesel felakete dayanıklılık | ~2× maliyet, veri replikasyonu, tutarlılık ve failover karmaşıklığı |
| Kullanıcıya yakın region | Düşük gecikme | O region'da servis/boyut olmayabilir; fiyat farklı olabilir |
| Çok sayıda küçük RG | Net yaşam döngüsü ve erişim sınırları | Yönetilecek daha çok kapsam |
| Ortam başına subscription | Faturalama ve erişim izolasyonu, kota ayrımı | Ağ bağlantısı ve yönetişim için management group/hub tasarımı gerekir |

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe the core architectural components of Azure](https://learn.microsoft.com/en-us/training/modules/describe-core-architectural-components-of-azure/)
- [Azure regions list](https://learn.microsoft.com/en-us/azure/reliability/regions-list) · [What are availability zones?](https://learn.microsoft.com/en-us/azure/reliability/availability-zones-overview) · [Azure region pairs and nonpaired regions](https://learn.microsoft.com/en-us/azure/reliability/regions-paired)
- [Azure Government overview](https://learn.microsoft.com/en-us/azure/azure-government/documentation-government-welcome) · [Azure operated by 21Vianet](https://learn.microsoft.com/en-us/azure/china/overview-operations)
- [Manage resource groups](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/manage-resource-groups-cli) · [Move resources](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/move-resource-group-and-subscription)
- [What are Azure management groups?](https://learn.microsoft.com/en-us/azure/governance/management-groups/overview)
- [`az account list-locations`](https://learn.microsoft.com/en-us/cli/azure/account#az-account-list-locations)
