# C1.22 Destek Planları, SLA'lar, Servis Yaşam Döngüsü ve Benimseme Çerçeveleri

> 📁 `ConsoleApp/06-certifications/01-az-900-azure-fundamentals/22-support-sla-lifecycle/` · Sınav alanı: **Describe Azure management and governance — genişletilmiş kapsam (bağlam)** · Önkoşul: [C1.2](../02-cloud-benefits/README.md), [C1.12](../12-governance-compliance/README.md), [C1.14](../14-monitoring-tools/README.md) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Bağlantı:** *Describe Azure management and governance* alanının eski sürümlerindeki ve kurslardaki "Azure support, SLA and service lifecycle" bölümü; C1.2 (SLA hesapları) ve C1.12 (uyumluluk) ile birlikte okunur.
>
> Bu konu resmî study guide'ın (July 20, 2026) doğrudan hedefi değildir; kurs müfredatlarında ve senaryo sorularının seçeneklerinde geçer.

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

## 2. Neden?

- **Sınav/kurs:** "Ücretsiz destek planı hangisi?", "Preview servisin SLA'sı var mı?", "%99,95 SLA aylık kaç dakika?", "Advisor'ın beş kategorisi hangi çerçevenin sütunları?" soruları eski AZ-900 sürümlerinde ve kurs sınavlarında sabittir; seçeneklerde bugün de görünür.
- **Gerçek işte:** Üretim sistemine **preview** bir servisi koymak veya **SLA'yı hesaplamadan** "%99,99" sözü vermek sık görülen kurumsal hatadır. Ayrıca gece 03:00'te bir kesintide **hangi destek planının** 7/24 telefon desteği verdiğini önceden bilmek gerekir.
- **Mülakat/mimari:** "Bu mimarinin SLA'sı nedir?" sorusu composite SLA hesabıyla cevaplanır; "iyi tasarım nedir?" sorusu Well-Architected sütunlarıyla (→ S8.3).
- **Zincir:** SLA'nın nasıl hesaplanıp kesinti süresine çevrildiği C1.2'dedir; bu konu **hizmet sözleşmesi, destek ve yaşam döngüsü** tarafını ekler.

---

## 3. Nasıl?

### 3.1 Azure destek planları ⭐

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

### 3.2 SLA'lar ⭐

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

### 3.3 Servis yaşam döngüsü ⭐

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

### 3.4 Benimseme çerçeveleri ⭐

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

### 3.5 Karar tablosu alıştırması — destek planı ve yaşam döngüsü

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

### 3.6 Pratik — Service Health olayı arama ve composite SLA hesabı

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
dotnet run -- C1.2 serial 99.95 99.99    # App Service x Azure SQL Database -> 99.94001 %
dotnet run -- C1.2 downtime 99.94        # monthly downtime budget of that composite
dotnet run -- C1.2 parallel 99.95 99.95  # two redundant App Service instances (independent failures assumed)
```

> **Alıştırma (elle hesapla, sonra doğrula).** Bir uygulama: **App Service %99,95 → Azure SQL Database %99,99 → Storage %99,9** (hepsi seri). (a) Toplam SLA? (b) Aylık kesinti bütçesi? (c) Web katmanını **iki bağımsız instance** ile paralel kurarsan (yönlendirme katmanını yok sayarak) toplam ne olur?
>
> <details><summary>Çözüm</summary>
>
> (a) `0,9995 × 0,9999 × 0,999 ≈ 0,99840` → **≈ %99,84**. `dotnet run -- C1.2 serial 99.95 99.99 99.9`.
> (b) Kayıp %0,16 × 43.800 dk ≈ **70 dakika/ay**.
> (c) Web katmanı: `1 − (1 − 0,9995)² = 0,99999975`. Toplam: `0,99999975 × 0,9999 × 0,999 ≈ 0,99890` → **≈ %99,89 (~48 dk/ay)**. Gözlem: web katmanını ikiletmek kazanç sağladı ama toplamı artık **Storage %99,9** ve **SQL %99,99** sınırlıyor; asıl darboğaz en zayıf **seri** halkadır. Gerçek mimaride trafiği yönlendiren Front Door/Traffic Manager da seri bağlıdır ve onun SLA'sıyla da çarpılır (→ C1.17).
> </details>

*Bu laboratuvar kaynak oluşturmaz; maliyet yoktur, temizlik gerekmez.* Başka bir `az` laboratuvarı yaptıysan kaynak grubunu sildiğinden ve bütçe alarmının kurulu olduğundan emin ol (→ C1.11).

---

## 4. Gerçek dünya senaryosu

Bir fintech, ödeme API'sini Azure'a taşıyor ve yönetim kuruluna "%99,95 çalışma süresi" sözü verilmesini istiyor.

1. **CAF:** Önce **Strategy** (neden taşıyoruz: ölçek ve uyumluluk), **Plan** (hangi sistemler, hangi sırayla — Azure Migrate değerlendirmesi, → C1.19), **Ready** (**landing zone**: management group'lar, Policy, hub-spoke ağ). Sonra **Adopt**, ardından sürekli **Govern/Manage/Secure**.
2. **SLA analizi:** Mimari: Front Door (%99,99) → App Service (%99,95) → Azure SQL (%99,99) → Storage (%99,9). Seri çarpım ≈ **%99,83** (~74 dk/ay) — hedefin **altında**. İyileştirme: App Service ve SQL'i **zone-redundant** yapmak ve SLA belgesindeki üst katmanları seçmek; ayrıca **bağımlılığı azaltmak**. Yeni composite hedefi tutturunca iş birimine "ayda yaklaşık 20 dakika" olarak anlatılır.
3. **Yaşam döngüsü:** Ekip, fraud tespiti için yeni bir AI özelliğini **public preview**'da buldu. Karar: **üretimde kullanılmaz** (SLA ve destek garantisi yok); pilot ortamında denenir, GA ve SLA netleşince geçilir. Risk kaydı tutulur.
4. **Destek planı:** Üretim aboneliği için en az **Standard**; ödeme akışı iş-kritik olduğundan **Professional Direct** düşünülür. Dev/test aboneliği **Developer**'da kalır; öğrenme/sandbox'ta **Basic**. Kesinti başında ilk bakış: **Service Health** (Azure tarafı mı?) → değilse uygulama; Azure ise Sev A destek talebi.
5. **Retirement takibi:** Ekip **Azure Updates** ve Service Health *health advisories* abonelikleri kurar; bir veritabanı API sürümünün emeklilik duyurusu 10 ay önceden fark edilir ve sürüm geçişi bir sprint'e planlanır — acil durum olmaz.
6. **WAF review:** Üç ayda bir **Well-Architected Review**: Reliability (DR tatbikatı), Security (Defender secure score), Cost (Advisor önerileri), Operational Excellence (IaC/CI-CD), Performance (autoscale). **Advisor**'ın beş kategorisi aynı sütunlara bağlandığı için iki kaynak birbirini doğrular.

---

## 5. Sınav perspektifi

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

## 6. Yaygın hatalar

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

## 7. Performans & trade-off

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

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Compare Azure support plans](https://azure.microsoft.com/support/plans/) · [Azure support plan response times](https://azure.microsoft.com/support/plans/response/) · [How to create an Azure support request](https://learn.microsoft.com/en-us/azure/azure-portal/supportability/how-to-create-azure-support-request)
- [Service Level Agreements for Online Services](https://www.microsoft.com/licensing/docs/view/Service-Level-Agreements-SLA-for-Online-Services) · [Azure SLA summary](https://azure.microsoft.com/support/legal/sla/summary/)
- [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/) · [Azure Updates](https://azure.microsoft.com/updates/) · [Azure Status](https://azure.status.microsoft/) · [Azure Retirements (Service Health)](https://learn.microsoft.com/en-us/azure/service-health/overview)
- [Cloud Adoption Framework](https://learn.microsoft.com/en-us/azure/cloud-adoption-framework/overview) · [Azure landing zones](https://learn.microsoft.com/en-us/azure/cloud-adoption-framework/ready/landing-zone/)
- [Azure Well-Architected Framework](https://learn.microsoft.com/en-us/azure/well-architected/) · [Azure Architecture Center](https://learn.microsoft.com/en-us/azure/architecture/)
- [`az monitor activity-log`](https://learn.microsoft.com/en-us/cli/azure/monitor/activity-log) · [`az feature`](https://learn.microsoft.com/en-us/cli/azure/feature)
