# C1.11 Azure'da Maliyet Yönetimi

> 📁 `ConsoleApp/06-certifications/01-az-900-azure-fundamentals/11-cost-management/` · Sınav alanı: **Describe Azure management and governance (%30-35)** · Önkoşul: [C1.1](../01-cloud-computing/README.md) (CapEx/OpEx, fiyatlandırma modelleri) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe cost management in Azure* — **maliyeti etkileyen faktörler** · **Pricing Calculator** · **Microsoft Cost Management** yetenekleri · **tag**'lerin amacı. TCO Calculator'ın güncel müfredattaki yeri için 3.2'deki nota ve resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'a bak.

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

Bulutta maliyet **tüketime bağlıdır** (→ C1.1): ne kadar, ne tür ve nerede kullandığına göre değişir. Maliyet yönetimi üç zamana ayrılır:

| Zaman | Soru | Araç |
|---|---|---|
| **Önce** (tahmin) | "Bu mimari ayda kaça mal olur?" · "On-prem'e göre ne kazanırım?" | **Pricing Calculator** · on-prem kıyası (TCO / Azure Migrate business case) |
| **Sırasında** (izleme) | "Şu an ne harcıyorum, nereye gidiyor, bütçeyi aşacak mıyım?" | **Microsoft Cost Management**: cost analysis, budgets, alerts |
| **Sonra** (iyileştirme) | "Nerede tasarruf ederim?" | Advisor önerileri, reservation, Hybrid Benefit, kapatma/silme |

Bunların hepsini bağlayan kavram **tag**'dir: maliyeti departmana, projeye, ortama göre raporlamanın yolu.

Kod: `ConsoleApp` projesinde [`Sections/`](Sections/) altında 4 bölüm ve bir **3 yıllık TCO hesaplayıcısı** ([`TcoCalculator.cs`](TcoCalculator.cs)). `ConsoleApp` klasöründe:

```bash
dotnet run -- C1.11 all          # all sections
dotnet run -- C1.11 tco 20       # 3-year CapEx (on-prem) vs OpEx (Azure) comparison for 20 servers
```

> Koddaki tüm fiyatlar **örnektir**; amaç maliyet kalemlerinin **yapısını** görmek, gerçek teklif çıkarmak değil.

---

## 2. Neden?

- **Sınav:** "Management and governance" alanının (%30-35) ilk hedefidir; araç seçimi (Pricing Calculator ↔ Cost Management ↔ TCO), maliyet faktörü ve tag soruları her sınavda çıkar.
- **Gerçek işte:** Bulut faturası sürprizleri neredeyse hep aynı sebeplerden gelir: unutulan kaynaklar, yanlış boyut, bölgeler arası/internete çıkan trafik, 7/24 sabit yükte pay-as-you-go. Bunları önceden bilmek ve **bütçe alarmıyla** yakalamak FinOps'un temelidir.
- **Yönetişimle bağı:** Tag'lerin zorunlu kılınması ve gruptan kopyalanması Azure Policy ile yapılır (→ C1.12); "kim ne harcıyor?" sorusu tag disiplini olmadan cevaplanamaz.

---

## 3. Nasıl?

### 3.1 Maliyeti etkileyen faktörler ⭐ ([`Section01`](Sections/Section01_CostFactors.cs))

| Faktör | Açıklama | Örnek |
|---|---|---|
| **Kaynak türü ve ayarları** | Servis, SKU/boyut, tier, redundancy, performans seviyesi | 2 vCPU → 8 vCPU VM; LRS → GZRS depolama; Hot ↔ Archive |
| **Tüketim** | Pay-as-you-go ↔ taahhütlü indirim (**reservations**, **savings plan**) | 7/24 VM'de 1-3 yıllık reservation ciddi indirim sağlar |
| **Bakım** | Kullanılmayan/unutulan kaynaklar faturada kalır | VM silindi ama **disk, public IP, NIC** kaldı; test ortamı kapatılmadı |
| **Coğrafya (bölge)** | Aynı servis bölgeye göre farklı fiyattadır (enerji, vergi, işçilik) | Aynı VM boyutu iki bölgede farklı fiyat |
| **Ağ trafiği** | **Gelen** (ingress) veri genellikle **ücretsiz**; **giden** (egress) — internete, bölgeler arası, hatta zone'lar arası — **ücretli** | Büyük dosya indirme hizmeti; bölgeler arası replikasyon |
| **Subscription türü** | Ücretsiz hesap kredisi ve ücretsiz kullanım miktarları; EA/CSP sözleşme fiyatları | Free account kotası bitince pay-as-you-go'ya geçiş |
| **Azure Marketplace** | Üçüncü taraf ürünlerde Azure ücretine ek olarak **satıcının ücreti** | Ticari bir güvenlik duvarı imajı |

**Tasarruf yolları** ([`Section03`](Sections/Section03_Savings.cs)):

| Yöntem | Ne? | Ne zaman? |
|---|---|---|
| **Reservations** | Belirli kaynak (ör. VM boyutu + bölge) için 1/3 yıllık taahhüt | Sabit, uzun süreli yük |
| **Savings plan for compute** | Saatlik compute harcama **tutarına** 1/3 yıllık taahhüt; boyut/bölge esnek | Sürekli ama değişen compute |
| **Azure Hybrid Benefit** | Mevcut (Software Assurance'lı) **Windows Server / SQL Server** lisanslarını Azure'da kullanmak | Lisansı olan kuruluşlar |
| **Spot VM** | Boşta kapasite, büyük indirim, kesilebilir | Kesintiye dayanıklı toplu işler |
| **Dev/test fiyatlandırması** | Dev/test aboneliklerinde indirimli oranlar | Yalnızca üretim dışı |
| **Doğru boyutlandırma ve kapatma** | Advisor önerileri, otomatik kapatma (auto-shutdown), deallocate, kullanılmayanı silme | Her zaman |

### 3.2 Pricing Calculator ve on-prem karşılaştırması (TCO)

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

`Section02` / `dotnet run -- C1.11 tco <n>` bu karşılaştırmanın yapısını gösterir ([`TcoCalculator.cs`](TcoCalculator.cs)): on-prem'de donanım, depolama/ağ ve lisans **1. yılda peşin** (CapEx), enerji/alan/bakım/emek her yıl; Azure'da yalnızca dönemsel (OpEx) kalemler. Sonuçta on-prem'in harcamasının büyük kısmı ilk yıla yığılır, bulutta yıllara eşit yayılır; sabit 7/24 yükte pay-as-you-go en pahalı bulut seçeneğidir, reservation ve Hybrid Benefit farkı kapatır.

### 3.3 Microsoft Cost Management

Azure (ve bağlanırsa AWS) harcamasını **izleme, analiz etme ve optimize etme** servisi; Azure portalında *Cost Management + Billing* altında, ek ücret olmadan:

| Yetenek | Ne yapar? |
|---|---|
| **Cost analysis** | Gerçekleşen ve tahmin edilen (forecast) maliyeti **kaynak, servis, bölge, resource group, tag** bazında gruplama ve filtreleme; trend grafikleri |
| **Budgets** | Bir kapsam (subscription, RG…) için aylık/çeyreklik/yıllık limit; gerçekleşen veya tahmini harcama eşiği (%50, %80, %100…) aşılınca **uyarı** (e-posta, **action group** → otomasyon) |
| **Cost alerts** | Bütçe uyarıları, **kredi** uyarıları (ör. Azure kredisinin %90'ı bitti), **departman harcama kotası** uyarıları (EA), anomali uyarıları |
| **Öneriler** | Advisor'ın maliyet önerileri (az kullanılan VM, reservation fırsatı) (→ C1.14) |
| **Exports** | Maliyet verisini zamanlanmış olarak storage account'a dışa aktarma (Power BI, özel raporlar) |

> **Budget harcamayı durdurmaz**, yalnızca uyarır. Kaynakları otomatik kapatmak istiyorsan bütçe uyarısını bir **action group**'a (ör. Automation/Logic App/Function) bağlarsın.

### 3.4 Resource tags ⭐ ([`Section04`](Sections/Section04_Tags.cs))

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

### 3.5 Pratik — CLI ile bütçe, tag ve fiyat sorgusu

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

## 4. Gerçek dünya senaryosu

Bir e-ticaret şirketinin bulut faturası üç ayda %60 artmış; FinOps ekibi şu adımları atıyor:

1. **Görünürlük:** Cost analysis'te maliyet **resource group** ve **tag** bazında gruplanıyor. Faturanın %18'i "(no tag)" satırında → Azure Policy ile `costCenter` ve `env` tag'leri **zorunlu** kılınıyor (Deny) ve mevcut kaynaklara gruptan **kopyalanıyor** (Modify + remediation).
2. **Bütçe:** Her ekibin aboneliğine aylık budget; %80 tahmini harcamada ekip lideri, %100'de finans e-postası; dev aboneliğinde %100 uyarısı bir **action group** üzerinden dev VM'lerini kapatan bir otomasyonu tetikliyor.
3. **Bakım:** Advisor; 40 az kullanılan VM, 120 sahipsiz disk ve 30 kullanılmayan public IP buluyor → küçültme ve silme.
4. **Tüketim modeli:** Üretimdeki 7/24 veritabanı ve web katmanı için 3 yıllık reservation; Windows sunucularında **Azure Hybrid Benefit**; gece çalışan raporlama işleri **spot VM**'lere.
5. **Ağ:** CDN ile görsellerin internete çıkışı (egress) azaltılıyor; gereksiz bölgeler arası replikasyon kapatılıyor.
6. **Yeni projeler:** Her yeni mimari önce **Pricing Calculator**'da tahmin ediliyor; on-prem'den taşınacak uygulamalar için **Azure Migrate business case** ile TCO karşılaştırması yapılıyor.

---

## 5. Sınav perspektifi

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

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "Budget aşılınca Azure kaynakları kapatır" | Budget yalnızca uyarır; kapatma için action group + otomasyon gerekir |
| "Pricing Calculator ile geçen ayın faturasını incelerim" | Gerçekleşen maliyet → Cost Management cost analysis |
| "RG'ye tag verdim, içindeki kaynaklar da etiketlendi" | Tag mirası yok; Policy (Modify) ile kopyalanır |
| `az resource tag --tags owner=ali` ile tag eklemek | Bu komut kaynağın **tüm** tag'lerini değiştirir; eklemek için `az tag update --operation Merge` |
| "Ağ trafiği ücretsizdir" | Egress, bölgeler arası ve zone'lar arası trafik ücretlidir |
| VM'i silip diskleri/IP'leri unutmak | Kalan kaynaklar faturalanır; RG düzeyinde temizlik veya Advisor |
| Kısa süreli projeye 3 yıllık reservation | Taahhüt süre boyunca ödenir; değişken/kısa yükte pay-as-you-go |
| "On-prem daha ucuz" sonucuna yalnızca donanım fiyatına bakarak varmak | TCO'da enerji, alan, bakım, **emek** ve yenileme döngüsü de vardır (`dotnet run -- C1.11 tco 20`) |

---

## 7. Performans & trade-off

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

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe cost management in Azure](https://learn.microsoft.com/en-us/training/modules/describe-cost-management-azure/)
- [Azure Pricing Calculator](https://azure.microsoft.com/pricing/calculator/) · [Azure Retail Prices REST API](https://learn.microsoft.com/en-us/rest/api/cost-management/retail-prices/azure-retail-prices)
- [Azure Migrate business case overview](https://learn.microsoft.com/en-us/azure/migrate/concepts-business-case-calculation) — on-prem ↔ Azure TCO karşılaştırması
- TCO Calculator durumu (ikincil kaynaklar): [MindMesh Academy — AZ-900 3.2.2](https://www.mindmeshacademy.com/certifications/azure/az-900-microsoft-azure-fundamentals/study-guide/3-2-2-estimating-cost-before-you-spend) · [Tutorials Dojo forum yanıtı](https://portal.tutorialsdojo.com/forums/reply/778971/)
- [What is Microsoft Cost Management?](https://learn.microsoft.com/en-us/azure/cost-management-billing/costs/overview-cost-management) · [Create and manage budgets](https://learn.microsoft.com/en-us/azure/cost-management-billing/costs/tutorial-acm-create-budgets)
- [Use tags to organize your Azure resources](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/tag-resources) · [Group and allocate costs using tag inheritance](https://learn.microsoft.com/en-us/azure/cost-management-billing/costs/enable-tag-inheritance)
- [Azure Hybrid Benefit](https://azure.microsoft.com/pricing/hybrid-benefit/) · [Azure reservations](https://learn.microsoft.com/en-us/azure/cost-management-billing/reservations/save-compute-costs-reservations) · [Azure savings plan for compute](https://learn.microsoft.com/en-us/azure/cost-management-billing/savings-plan/savings-plan-compute-overview)
- [Bandwidth pricing](https://azure.microsoft.com/pricing/details/bandwidth/) (egress)
