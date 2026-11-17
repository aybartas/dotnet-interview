[← AZ-900 İçindekiler](../README.md) · [📝 Quiz](quiz.md) · [← Önceki konu](../1.1-cloud-computing-models-pricing/README.md)

## 1.2 Bulutun Temel Faydaları

> **Sınav Alanı:** Describe cloud concepts (%25-30) · **Konu Kodu:** C1.2
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe the benefits of using cloud services* — high availability ve scalability · reliability ve predictability · security ve governance · manageability. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

### 1. Ne?

Resmî hedef, bulutun faydalarını **dört çift** halinde sayar: *high availability & scalability*, *reliability & predictability*, *security & governance* ve *manageability*. Sınav bunları tek tek tanımlatmaktan çok **senaryodan faydayı buldurur** ve en çok birbirine benzeyenleri (HA ↔ reliability, scalability ↔ elasticity, security ↔ governance) şık yapar.

- **High availability**
  - Tanım: Arıza olsa bile hizmetin **hedeflenen çalışma süresinde** kalması
  - Bulutta nasıl sağlanır: Yedeklilik, tek arıza noktasını kaldırmak, otomatik failover
  - Azure örneği: **SLA**'lar, availability zone, scale set
- **Scalability**
  - Tanım: Talebe göre **kapasiteyi artırma/azaltma** yeteneği
  - Bulutta nasıl sağlanır: **Vertical** (scale up/down) · **horizontal** (scale out/in)
  - Azure örneği: VM boyutu değiştirme, VM Scale Sets, App Service plan
- **Elasticity**
  - Tanım: Ölçeklemenin yüke göre **otomatik** yapılması
  - Bulutta nasıl sağlanır: Metrik/zaman tabanlı **autoscale** kuralları
  - Azure örneği: Autoscale, Functions'ın otomatik ölçeklemesi
- **Reliability**
  - Tanım: Arızadan **toparlanma** ve çalışmaya devam etme
  - Bulutta nasıl sağlanır: Dağıtık tasarım, zone/bölge yedekliliği, felaket kurtarma
  - Azure örneği: Region pair, geo-redundant storage, *(bağlam)* Azure Site Recovery
- **Predictability**
  - Tanım: **Performansın** ve **maliyetin** öngörülebilir olması
  - Bulutta nasıl sağlanır: Performans: autoscale, load balancing, HA · Maliyet: kullanım takibi ve tahmin
  - Azure örneği: Pricing Calculator, Cost Management, Well-Architected Framework (→ S8.3)
- **Security**
  - Tanım: Güvenlik kontrolleri üzerinde **istenen düzeyde kontrol**
  - Bulutta nasıl sağlanır: IaaS'ta tam kontrol, PaaS/SaaS'ta yamalar sağlayıcıda; altyapı düzeyinde koruma
  - Azure örneği: Defender for Cloud, Key Vault, DDoS Protection
- **Governance**
  - Tanım: Kurumsal standartlara ve regülasyonlara **uyum**
  - Bulutta nasıl sağlanır: Şablonlar, politikalar, denetim ve uyumluluk raporları
  - Azure örneği: Azure Policy, management group'lar, Purview
- **Manageability**
  - Tanım: Bulutun yönetimi (*of*) ve bulutta yönetim (*in*)
  - Bulutta nasıl sağlanır: *Of:* otomatik ölçek, izleme ve uyarı, şablonla dağıtım · *In:* portal, CLI, API, PowerShell
  - Azure örneği: Azure Monitor, ARM/Bicep, portal, Cloud Shell


---

### 2. Neden?

- **Sınav:** Fayda eşleştirme soruları bu alanın en sık çıkan sorularıdır ve "hangisi en iyi tanımlar?" kalıbında iki şık çoğu zaman birbirine çok yakındır. Farkı bilmeyen için bu sorular yazı turasıdır.
- **Gerçek işte:** "Buluta neden geçiyoruz?" sorusunun iş tarafına verilen cevabı bu sekiz faydadır. Bir mimari kararını (iki zone mu, tek VM mi?) savunurken SLA'yı kesinti süresine çevirebilmek ve maliyetiyle tartabilmek gerekir.
- **Sonraki konular:** availability zone ve region pair (C1.4), VM Scale Sets ve availability set (C1.5), Policy ve Purview (C1.12), Monitor (C1.14) — hepsi bu faydaların **araçlarıdır**.

---

### 3. Nasıl?

#### 3.1 Sekiz fayda ve karıştırılan çiftler

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

#### 3.2 SLA'yı kesinti süresine çevirmek ⭐

**SLA (Service Level Agreement)**, Microsoft'un bir hizmet için taahhüt ettiği **aylık çalışma süresi yüzdesidir**. Taahhüt tutturulamazsa müşteri, faturasından indirim olarak **servis kredisi** talep edebilir. Ay = 730 saat:

- **%99** — Günlük: 14 dk 24 sn · Haftalık: 1 sa 40 dk · Aylık: 7 sa 18 dk · Yıllık: 3,65 gün
- **%99,9** — Günlük: 1 dk 26 sn · Haftalık: 10 dk 4 sn · Aylık: **43 dk 48 sn** · Yıllık: 8 sa 45 dk
- **%99,95** — Günlük: 43 sn · Haftalık: 5 dk 2 sn · Aylık: **21 dk 53 sn** · Yıllık: 4 sa 22 dk
- **%99,99** — Günlük: 8 sn · Haftalık: 1 dk 0 sn · Aylık: **4 dk 22 sn** · Yıllık: 52 dk 33 sn
- **%99,999** — Günlük: < 1 sn · Haftalık: 6 sn · Aylık: 26 sn · Yıllık: 5 dk 15 sn

- Her ek "9" kesinti bütçesini **10'a böler**.
- SLA'lar **ürüne ve yapılandırmaya** göre değişir. Örnek (Microsoft'un yayımladığı tipik değerler; güncelini SLA belgesinden kontrol et): tek instance VM (Premium SSD) **%99,9** · aynı availability set'te 2+ VM **%99,95** · iki+ availability zone'a yayılmış 2+ VM **%99,99**.
- **Preview** (önizleme) servislerin SLA'sı **yoktur**; ücretsiz katmanların da genellikle SLA'sı yoktur.
- SLA'yı yükseltmenin yolu **yedekliliktir** — bedeli daha çok kaynak, yani daha çok maliyet.

#### 3.3 Composite SLA *(bağlam — hesap sorusu gelebilir)*

Bir uygulama birden çok servisten oluşuyorsa uygulamanın toplam SLA'sı tek tek servislerin SLA'sından farklıdır:

- **Seri bağlı** (hepsi ayakta olmalı; ör. web uygulaması → veritabanı): SLA'lar **çarpılır**.
  `App Service %99,95 × SQL Database %99,99 = %99,94` → toplam SLA, **en zayıf bileşenden de düşüktür**.
- **Paralel / yedekli** (biri ayakta yeterli; ör. aynı yığın iki region'da): `1 − (1 − a) × (1 − b)`.
  `1 − (1 − 0,9994)² ≈ %99,99996` → yedeklilik SLA'yı **yükseltir**. Varsayım: arızalar birbirinden **bağımsız** ve trafiği yönlendiren katman (Front Door/Traffic Manager) çalışıyor — o katman da seri bağlıdır, onun SLA'sıyla da çarpılır.

```bash
# Serial (all must be up): App Service x SQL Database -> 99.94001 %
python3 -c "print(f'{0.9995 * 0.9999 * 100:.5f} %')"
# Parallel (one is enough): the same stack in two regions -> 99.99996 %
python3 -c "print(f'{(1 - (1 - 0.9994) ** 2) * 100:.5f} %')"
# A chain of four components: Front Door x App Service x SQL Database x Storage
python3 -c "print(f'{0.9999 * 0.9995 * 0.9999 * 0.999 * 100:.3f} %')"
```

> **Alıştırma.** Bir uygulama: Front Door (%99,99) → App Service (%99,95) → Azure SQL Database (%99,99) → Storage (%99,9). (a) Toplam SLA nedir? (b) Bunun aylık kesinti bütçesi kaç dakikadır? (c) Hangi bileşeni iyileştirmek toplam SLA'ya en çok katkı verir?
>
> <details><summary>Çözüm</summary>
>
> (a) `0,9999 × 0,9995 × 0,9999 × 0,999 ≈ %99,83`.
> (b) ≈ **1 sa 14 dk**/ay (`(1 − 0,9983) × 730 sa × 60 ≈ 74 dk`).
> (c) En **düşük** SLA'lı bileşen (Storage %99,9): zincirin en zayıf halkası toplamı en çok aşağı çeker. Örneğin daha yüksek SLA'lı bir yedeklilik seçeneği veya okuma için ikinci bölge kullanmak.
> </details>

#### 3.4 Scale up, scale out ve elasticity simülasyonu

Simülasyon, gündüz yoğun bir iş uygulamasının 24 saatlik instance ihtiyacını dört stratejiyle karşılar ve **maliyet (instance-saat)**, **yetersiz kalınan saat** ve **boşta kapasite** sayar:

- **Sabit 4 instance** — Kavram: Ölçekleme yok · 1. gün (her günkü yük): Ucuz ama gündüz 14 saat yetersiz · 2. gün (habersiz kampanya): Aynı
- **Sabit 10 instance**
  - Kavram: Zirveye göre boyutlama
  - 1. gün (her günkü yük): Hiç takılmaz, kapasitenin büyük kısmı boşta
  - 2. gün (habersiz kampanya): Beklenmedik zirvede **o da yetmez**
- **Elle: 08:00'de 10'a çık, 20:00'de 4'e in**
  - Kavram: **Scalability**
  - 1. gün (her günkü yük): İyi; yalnızca rampa saatlerinde kaçırır
  - 2. gün (habersiz kampanya): Kampanyayı kaçırır (insan tahminine bağlı)
- **Autoscale: ihtiyaç + 1, saatte en fazla +4**
  - Kavram: **Elasticity**
  - 1. gün (her günkü yük): Maliyet ihtiyaca en yakın, kesinti yok
  - 2. gün (habersiz kampanya): Zirveyi **kendiliğinden** karşılar, bitince küçülür; yalnızca ani sıçramanın ilk saati eksik

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

- **Tek VM → iki zone'a yayılmış VM'ler**
  - Kazanç: SLA %99,9 → %99,99; aylık kesinti bütçesi ~44 dk → ~4 dk
  - Bedel: ~2× compute maliyeti; uygulamanın çok instance'a uygun (stateless) olması
- **Tek region → iki region (aktif-pasif / aktif-aktif)**
  - Kazanç: Bölgesel felakete dayanıklılık (reliability), çok yüksek composite SLA
  - Bedel: Maliyet, veri replikasyonu ve tutarlılık karmaşıklığı, yönlendirme katmanı
- **Scale up**
  - Kazanç: Basit, uygulama değişikliği gerekmez
  - Bedel: Üst sınır, yeniden başlatma, tek arıza noktası
- **Scale out**
  - Kazanç: Neredeyse sınırsız, HA'ya katkı
  - Bedel: Stateless tasarım, load balancer, oturum/önbellek yönetimi
- **Autoscale (elasticity)**
  - Kazanç: Maliyet talebi izler, insan müdahalesi yok
  - Bedel: Tepki gecikmesi (cold start, cool-down), yanlış kuralla dalgalanma veya fatura sürprizi
- **Sabit zirve kapasitesi**
  - Kazanç: Öngörülebilir performans
  - Bedel: Yılın çoğunda boşta kapasite için ödeme

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe the benefits of using cloud services](https://learn.microsoft.com/en-us/training/modules/describe-benefits-use-cloud-services/)
- [Service Level Agreements (SLA) for Online Services](https://www.microsoft.com/licensing/docs/view/Service-Level-Agreements-SLA-for-Online-Services)
- [Azure Well-Architected Framework — Reliability](https://learn.microsoft.com/en-us/azure/well-architected/reliability/) (composite SLA ve hedef belirleme)
- [Autoscale in Azure](https://learn.microsoft.com/en-us/azure/azure-monitor/autoscale/autoscale-overview) · [What are availability zones?](https://learn.microsoft.com/en-us/azure/reliability/availability-zones-overview)
- [`az vm list-skus`](https://learn.microsoft.com/en-us/cli/azure/vm#az-vm-list-skus)

---

➡️ **Sıradaki:** [📝 Bu konunun quiz soruları](quiz.md)
