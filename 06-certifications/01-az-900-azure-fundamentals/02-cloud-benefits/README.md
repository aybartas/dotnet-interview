# C1.2 Cloud'un Faydaları

> 📁 `06-certifications/01-az-900-azure-fundamentals/02-cloud-benefits/` · Sınav alanı: **Describe cloud concepts (%25-30)** · Önkoşul: [C1.1](../01-cloud-computing/README.md) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe the benefits of using cloud services* — high availability ve scalability · reliability ve predictability · security ve governance · manageability. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

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

Kod: modül projesinde [`Sections/`](Sections/) altında 5 bölüm ve bir **SLA hesaplayıcısı** ([`SlaCalculator.cs`](SlaCalculator.cs)). Modül klasöründe:

```bash
dotnet run -- 1.2 all                     # tüm bölümler
dotnet run -- 1.2 kesinti 99.95           # SLA → günlük/haftalık/aylık/yıllık kesinti bütçesi
dotnet run -- 1.2 seri 99.95 99.99        # seri bağlı bileşenlerin composite SLA'sı
dotnet run -- 1.2 paralel 99.94 99.94     # yedekli bileşenlerin composite SLA'sı
```

---

## 2. Neden?

- **Sınav:** Fayda eşleştirme soruları bu alanın en sık çıkan sorularıdır ve "hangisi en iyi tanımlar?" kalıbında iki şık çoğu zaman birbirine çok yakındır. Farkı bilmeyen için bu sorular yazı turasıdır.
- **Gerçek işte:** "Buluta neden geçiyoruz?" sorusunun iş tarafına verilen cevabı bu sekiz faydadır. Bir mimari kararını (iki zone mu, tek VM mi?) savunurken SLA'yı kesinti süresine çevirebilmek ve maliyetiyle tartabilmek gerekir.
- **Sonraki konular:** availability zone ve region pair (C1.4), VM Scale Sets ve availability set (C1.5), Policy ve Purview (C1.12), Monitor (C1.14) — hepsi bu faydaların **araçlarıdır**.

---

## 3. Nasıl?

### 3.1 Sekiz fayda ve karıştırılan çiftler ([`Section01`](Sections/Section01_Benefits.cs), [`Section05`](Sections/Section05_BenefitScenarios.cs))

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

### 3.2 SLA'yı kesinti süresine çevirmek ⭐ ([`Section02`](Sections/Section02_SlaDowntime.cs))

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

### 3.3 Composite SLA *(bağlam — hesap sorusu gelebilir)* ([`Section03`](Sections/Section03_CompositeSla.cs))

Bir uygulama birden çok servisten oluşuyorsa uygulamanın toplam SLA'sı tek tek servislerin SLA'sından farklıdır:

- **Seri bağlı** (hepsi ayakta olmalı; ör. web uygulaması → veritabanı): SLA'lar **çarpılır**.
  `App Service %99,95 × SQL Database %99,99 = %99,94` → toplam SLA, **en zayıf bileşenden de düşüktür**.
- **Paralel / yedekli** (biri ayakta yeterli; ör. aynı yığın iki region'da): `1 − (1 − a) × (1 − b)`.
  `1 − (1 − 0,9994)² ≈ %99,99996` → yedeklilik SLA'yı **yükseltir**. Varsayım: arızalar birbirinden **bağımsız** ve trafiği yönlendiren katman (Front Door/Traffic Manager) çalışıyor — o katman da seri bağlıdır, onun SLA'sıyla da çarpılır.

```bash
dotnet run -- 1.2 seri 99.95 99.99                 # %99,94001
dotnet run -- 1.2 paralel 99.94 99.94              # %99,99996
dotnet run -- 1.2 seri 99.99 99.95 99.99 99.9      # dört bileşenli zincir
```

> **Alıştırma.** Bir uygulama: Front Door (%99,99) → App Service (%99,95) → Azure SQL Database (%99,99) → Storage (%99,9). (a) Toplam SLA nedir? (b) Bunun aylık kesinti bütçesi kaç dakikadır? (c) Hangi bileşeni iyileştirmek toplam SLA'ya en çok katkı verir?
>
> <details><summary>Çözüm</summary>
>
> (a) `0,9999 × 0,9995 × 0,9999 × 0,999 ≈ %99,83` — `dotnet run -- 1.2 seri 99.99 99.95 99.99 99.9`.
> (b) ≈ **1 sa 14 dk**/ay (`dotnet run -- 1.2 kesinti 99.83`).
> (c) En **düşük** SLA'lı bileşen (Storage %99,9): zincirin en zayıf halkası toplamı en çok aşağı çeker. Örneğin daha yüksek SLA'lı bir yedeklilik seçeneği veya okuma için ikinci bölge kullanmak.
> </details>

### 3.4 Scale up, scale out ve elasticity simülasyonu ([`Section04`](Sections/Section04_ScalingSimulation.cs))

Simülasyon, gündüz yoğun bir iş uygulamasının 24 saatlik instance ihtiyacını dört stratejiyle karşılar ve **maliyet (instance-saat)**, **yetersiz kalınan saat** ve **boşta kapasite** sayar:

| Strateji | Kavram | 1. gün (her günkü yük) | 2. gün (habersiz kampanya) |
|---|---|---|---|
| Sabit 4 instance | Ölçekleme yok | Ucuz ama gündüz 14 saat yetersiz | Aynı |
| Sabit 10 instance | Zirveye göre boyutlama | Hiç takılmaz, kapasitenin büyük kısmı boşta | Beklenmedik zirvede **o da yetmez** |
| Elle: 08:00'de 10'a çık, 20:00'de 4'e in | **Scalability** | İyi; yalnızca rampa saatlerinde kaçırır | Kampanyayı kaçırır (insan tahminine bağlı) |
| Autoscale: ihtiyaç + 1, saatte en fazla +4 | **Elasticity** | Maliyet ihtiyaca en yakın, kesinti yok | Zirveyi **kendiliğinden** karşılar, bitince küçülür; yalnızca ani sıçramanın ilk saati eksik |

Çıkarım: yük öngörülebilir ise zamanlanmış ölçek bile iyi iş görür; elasticity'nin asıl değeri **öngörülemeyen** yüktedir.

### 3.5 Pratik — availability zone desteğini görmek (ücretsiz, salt okunur)

Hangi VM boyutunun bir region'da hangi availability zone'larda sunulduğunu sorgulamak HA tasarımının ilk adımıdır (zone'lar → C1.4). Bu komutlar **kaynak oluşturmaz**, temizlik gerekmez:

```bash
az login
# West Europe'ta B serisi küçük boyutların zone desteği (Zones sütunu: 1,2,3)
az vm list-skus --location westeurope --size Standard_B1s --zone --output table
# Aynı boyut başka bir region'da (--zone yalnızca zone destekli SKU'ları listeler; çıktı boşsa orada zone desteği yok)
az vm list-skus --location northeurope --size Standard_B1s --zone --output table
```

---

## 4. Gerçek dünya senaryosu

Bir bilet satış platformu büyük bir konser satışından önce mimarisini gözden geçiriyor:

- **Ölçü:** Satış anında site 10 dakika bile kapalı kalırsa gelir kaybı çok büyük. Mevcut yapı: tek VM (%99,9 → ayda ~44 dk kesinti bütçesi) + veritabanı. Seri bağlı oldukları için toplam SLA tek VM'in de altında.
- **High availability:** Web katmanı iki availability zone'a yayılmış bir **VM Scale Set**'e taşınıyor (%99,99); veritabanı yönetilen, zone-redundant bir servise geçiyor. Toplam SLA hesaplanıp iş birimine "ayda en fazla birkaç dakika" olarak anlatılıyor.
- **Elasticity:** Scale set'e autoscale kuralları (CPU ve istek kuyruğu) ve satış saatinden önce kapasiteyi önceden artıran **zamanlanmış** bir kural ekleniyor — ani sıçramanın ilk dakikalarındaki gecikme böylece kapanıyor.
- **Reliability:** Uygulama ikinci bir region'da da hazır tutuluyor; bölgesel bir felakette trafik oraya yönlendirilecek ve veri geo-replication ile oradan devam edecek.
- **Predictability & governance:** Satış haftası için Pricing Calculator ile maliyet çıkarılıyor, Cost Management'ta bütçe kuruluyor; Azure Policy, kaynakların yalnızca onaylı region'larda açılmasını zorunlu kılıyor.

---

## 5. Sınav perspektifi

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

## 6. Yaygın hatalar

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

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Tek VM → iki zone'a yayılmış VM'ler | SLA %99,9 → %99,99; aylık kesinti bütçesi ~44 dk → ~4 dk | ~2× compute maliyeti; uygulamanın çok instance'a uygun (stateless) olması |
| Tek region → iki region (aktif-pasif / aktif-aktif) | Bölgesel felakete dayanıklılık (reliability), çok yüksek composite SLA | Maliyet, veri replikasyonu ve tutarlılık karmaşıklığı, yönlendirme katmanı |
| Scale up | Basit, uygulama değişikliği gerekmez | Üst sınır, yeniden başlatma, tek arıza noktası |
| Scale out | Neredeyse sınırsız, HA'ya katkı | Stateless tasarım, load balancer, oturum/önbellek yönetimi |
| Autoscale (elasticity) | Maliyet talebi izler, insan müdahalesi yok | Tepki gecikmesi (cold start, cool-down), yanlış kuralla dalgalanma veya fatura sürprizi |
| Sabit zirve kapasitesi | Öngörülebilir performans | Yılın çoğunda boşta kapasite için ödeme |

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe the benefits of using cloud services](https://learn.microsoft.com/en-us/training/modules/describe-benefits-use-cloud-services/)
- [Service Level Agreements (SLA) for Online Services](https://www.microsoft.com/licensing/docs/view/Service-Level-Agreements-SLA-for-Online-Services)
- [Azure Well-Architected Framework — Reliability](https://learn.microsoft.com/en-us/azure/well-architected/reliability/) (composite SLA ve hedef belirleme)
- [Autoscale in Azure](https://learn.microsoft.com/en-us/azure/azure-monitor/autoscale/autoscale-overview) · [What are availability zones?](https://learn.microsoft.com/en-us/azure/reliability/availability-zones-overview)
- [`az vm list-skus`](https://learn.microsoft.com/en-us/cli/azure/vm#az-vm-list-skus)
