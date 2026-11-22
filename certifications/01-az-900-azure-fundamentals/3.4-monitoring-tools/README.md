[← AZ-900 İçindekiler](../README.md) · [📝 Quiz](quiz.md) · [← Önceki konu](../3.3-management-deployment-tools/README.md)

## 3.4 İzleme Araçları: Advisor, Service Health, Azure Monitor, Log Analytics, Alerts, Application Insights

> **Sınav Alanı:** Describe Azure management and governance (%30-35) · **Konu Kodu:** C1.14
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe monitoring tools in Azure* — **Azure Advisor** · **Azure Service Health** · **Azure Monitor** (Log Analytics, Azure Monitor alert'leri ve Application Insights dahil). Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

### 1. Ne?

AZ-900 üç izleme aracını sorar; her biri **farklı bir soruya** cevap verir:

- **Azure Advisor**
  - Cevapladığı soru: "Kaynaklarımı **nasıl daha iyi** yapılandırabilirim?"
  - Kısaca: Kişiselleştirilmiş **öneri** servisi (5 kategori)
- **Azure Service Health**
  - Cevapladığı soru: "**Azure'da** benim kaynaklarımı etkileyen bir sorun/bakım var mı?"
  - Kısaca: Azure Status + Service Health + Resource Health
- **Azure Monitor**
  - Cevapladığı soru: "Uygulamam ve kaynaklarım **şu anda nasıl** çalışıyor, ne oldu?"
  - Kısaca: Metrik + log toplama, analiz (Log Analytics), uyarı (alerts), uygulama izleme (Application Insights)

Bu konuda kod yok; pratik, `az` CLI komutlarıyla (Advisor önerileri, action group, metrik ve Service Health uyarısı, Log Analytics sorgusu) yapılır. Portal ekranları (Monitor sayfası, Metrics, Logs, Alerts) teoride anlatılır.

---

### 2. Neden?

- **Sınav:** Bu üç aracın birbirine karıştırılması bu alanın klasik tuzağıdır: "öneri" mi (Advisor), "Azure kaynaklı olay" mı (Service Health), "benim metriğim/log'um" mu (Monitor)? Service Health'in üç parçasının **kapsam farkı** da sık sorulur.
- **Gerçek işte:** Bir kesintide ilk soru "sorun bizde mi, Azure'da mı?"dır. Service Health bunu dakikalar içinde ayırır; Monitor ve Application Insights ise sorunun **nerede** olduğunu gösterir. Advisor da maliyet ve güvenilirlik iyileştirmelerini **ücretsiz** olarak listeler.
- **Bağlantılar:** Maliyet önerileri → C1.11 (Cost Management), güvenlik önerileri → C1.10 (Defender for Cloud).

---

### 3. Nasıl?

#### 3.1 Azure Advisor

Kaynak yapılandırmanı ve kullanım (telemetri) verini analiz edip **kişiselleştirilmiş, uygulanabilir öneriler** veren ücretsiz servis. Öneriler beş kategoridedir ⭐:

- **Reliability** (eski adı *High Availability*)
  - Amaç: İş açısından kritik uygulamaların sürekliliği
  - Örnek öneri: Tek instance VM'i availability set/zone'a taşı
- **Security**
  - Amaç: Güvenlik açığına yol açabilecek tehdit ve zafiyetleri tespit
  - Örnek öneri: Defender for Cloud önerileri (Advisor'da da görünür)
- **Performance**
  - Amaç: Uygulamaların hızını ve yanıt süresini iyileştirme
  - Örnek öneri: Daha uygun disk türü veya VM boyutu
- **Cost**
  - Amaç: Genel Azure harcamasını optimize etme
  - Örnek öneri: Az kullanılan VM'i küçült veya kapat, **reservation** al
- **Operational Excellence**
  - Amaç: Süreç/iş akışı verimliliği, yönetilebilirlik, dağıtım iyi uygulamaları
  - Örnek öneri: Service Health uyarısı kur, kaynakları tag'le

- Öneriler üzerinde yapılabilenler: **uygula**, **ertele** (belirli süre) veya **kapat** (dismiss).
- Advisor **önerir**, kendiliğinden değiştirmez; Azure Policy gibi **zorlamaz** (→ C1.12).

#### 3.2 Azure Service Health

Azure platformunun sağlık durumunu **üç kapsamda** gösterir ⭐:

- **Azure Status**
  - Kapsam: **Tüm Azure**, herkese açık (`azure.status.microsoft`)
  - Ne gösterir?: Geniş çaplı (birçok müşteriyi etkileyen) olayların genel durum sayfası; giriş gerektirmez
- **Service Health**
  - Kapsam: **Senin** subscription'ların, kullandığın servisler ve region'lar
  - Ne gösterir?: **Service issues** (hizmet sorunları — şu an seni etkileyen kesinti), **planned maintenance** (planlı bakım), **health advisories** (aksiyon gerektiren değişiklikler, ör. bir özelliğin emekliliği); olay geçmişi ve **Service Health alert**'leri; **bölgeye özgü** sorunları da gösterir
- **Resource Health**
  - Kapsam: **Tek bir kaynak** (ör. bir VM, bir SQL veritabanı)
  - Ne gösterir?: Kaynağın mevcut ve geçmiş sağlık durumu (Available / Unavailable / Degraded / Unknown) ve sorun Azure platformundan mı, kullanıcı eyleminden mi kaynaklanıyor

> Kural: **genel Azure** → Azure Status · **benim aboneliğim/bölgem** → Service Health · **tek bir kaynağım** → Resource Health.

Service Health uyarıları bir **action group**'a (e-posta, SMS, webhook…) bağlanır (pratik → 3.7).

**Azure Status ↔ Service Health:** Azure Status **herkese açık** genel durum sayfasıdır (giriş gerekmez, tüm Azure); Service Health ise **giriş gerektirir** ve yalnızca **senin** subscription/servis/region'larını ilgilendiren olayları kişiselleştirilmiş gösterir.

#### 3.3 Azure Monitor

Azure, on-prem ve diğer bulutlardaki kaynaklardan **telemetri toplayan, analiz eden ve buna göre harekete geçen** birleşik izleme platformu. Azure kaynaklarının **metriklerini** ve **log'larını** toplar, analiz eder ve **uyarı** üretir.

**İki temel veri türü:**

- **Ne?**
  - Metrics (metrikler): Belirli aralıklarla toplanan **sayısal** değerler (CPU %, istek sayısı, gecikme)
  - Logs (log'lar): Zaman damgalı **olay kayıtları** (metin, yapılandırılmış alanlar)
- **Güçlü yanı**
  - Metrics (metrikler): Hafif, **neredeyse gerçek zamanlı**, grafik ve eşik uyarısı
  - Logs (log'lar): Zengin bağlam, çok kaynaklı **sorgulama**, kök neden analizi
- **Araç** — Metrics (metrikler): **Metrics explorer** · Logs (log'lar): **Log Analytics**

**Veri kaynakları (katmanlar):** uygulama (Application Insights), işletim sistemi, Azure kaynakları (platform metrikleri, resource log'ları), subscription (**Activity Log** — "kim, ne zaman, hangi kaynağı değiştirdi?"), tenant (Entra ID log'ları).

**Azure Monitor'un parçaları:** **Log Analytics** (log toplama ve sorgulama), **Alerts** (uyarılar), **Application Insights** (uygulama performans izleme) — sırasıyla aşağıda.

#### 3.4 Log Analytics

**Azure Log Analytics**, Azure Monitor'un log verisini **toplayan, saklayan ve sorgulayan** bileşenidir: log'lar bir **Log Analytics workspace**'inde birikir ve **KQL** (Kusto Query Language) ile sorgulanır.

- **Log verisi toplama:** birçok kaynağın (VM'ler, Azure kaynakları, uygulamalar) log'u **tek bir workspace**'te birleşir.
- **Log sorguları:** KQL ile filtreleme (`where`), özetleme (`summarize`), sıralama (`order by`); sonuç tablo veya grafik olarak görüntülenir.
- **Log analizi:** olayları kaynaklar arası ilişkilendirerek **kök neden** bulmak ("hata hangi dağıtımdan sonra başladı?") ve sorguyu uyarı kuralına dönüştürmek.
- Maliyet **alınan veri** hacmine bağlıdır.

```kusto
// Failed requests in the last day grouped by name (illustrative query)
AppRequests
| where TimeGenerated > ago(1d) and Success == false
| summarize failures = count() by Name
| top 5 by failures
```

> Kural: **tek bir kaynağın sayısal değeri / hızlı grafik** → Metrics · **birçok kaynağın olay kayıtlarında arama ve analiz** → Log Analytics (KQL).

#### 3.5 Azure Monitor Alerts

**Alert**, izlenen veride bir **koşul** sağlandığında otomatik bildirim veya eylem üreten mekanizmadır.

- **Alert rule (uyarı kuralı):** *Ne izlenecek* (kapsam) + *hangi koşulda* (condition) + *ne yapılacak* (action group).
- **Alert türleri:**
  - **Metric alert** — Metrik bir eşiği aşınca (ör. "CPU > %80, 5 dakika boyunca"): hızlı ve ucuz.
  - **Log alert (log search alert)** — Bir log sorgusunun sonucu koşulu sağlayınca (ör. "son 5 dakikada hata sayısı > 50"): daha esnek, biraz daha gecikmeli.
  - **Activity log alert** — Abonelikteki bir yönetim olayında (ör. bir kaynağın silinmesi) veya **Service Health** olayında.
- **Alert conditions (koşullar):** eşik değeri, karşılaştırma operatörü (>, <), zaman penceresi ve değerlendirme sıklığı.
- **Action group:** uyarı tetiklenince **kime/neye** bildirileceğini tanımlayan yeniden kullanılabilir küme: **e-posta, SMS, push, sesli arama, webhook** gibi eylemler. Kural bildirim göndermez; **action group**'u tetikler.

#### 3.6 Application Insights

**Application Insights**, Azure Monitor'un **uygulama performans izleme (APM)** özelliğidir: çalışan uygulamanın kendisinden **telemetri** toplar.

- **Request tracking (istek izleme):** istek sayısı, yanıt süresi, başarısız istekler.
- **Dependency tracking (bağımlılık izleme):** uygulamanın çağırdığı veritabanı, HTTP servisi gibi bağımlılıkların süresi ve hataları.
- **Exception tracking (hata izleme):** işlenmeyen hatalar ve ayrıntıları.
- **Distributed tracing (dağıtık izleme):** bir kullanıcı isteğinin birden çok servis/bileşen arasındaki yolculuğunu **tek bir izde** birleştirir; yavaşlığın hangi bileşende olduğunu gösterir (uygulama haritası).
- **Application availability and performance:** **availability test**'ler uygulamaya dünyanın farklı noktalarından düzenli istek atıp erişilebilirliği ve yanıt süresini ölçer.
- Application Insights verisi **Log Analytics workspace**'ine akar ve oradan sorgulanabilir; Azure'da, on-prem'de veya başka bulutta çalışan uygulamaları izleyebilir.

> Kural: **"Uygulamamın istek süreleri, hata oranı, bağımlılıkları"** → Application Insights · **"VM'in CPU'su"** → Azure Monitor metrikleri.

#### 3.7 Pratik — CLI ile izleme ve uyarı

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

> Workspace'e gerçek log akması için kaynaklarda log gönderimi (diagnostic settings) tanımlanır; bu laboratuvarda KQL akışını görmek için satır içi bir tablo kullandık.

---

### 4. Gerçek dünya senaryosu

Bir e-ticaret şirketinde pazartesi sabahı ödeme sayfası yavaşlıyor:

1. **Application Insights** uyarısı (log search alert: "son 5 dakikada ödeme isteklerinin hata oranı %5'i geçti") **action group** üzerinden nöbetçi ekibe SMS ve Teams webhook'u gönderiyor.
2. Ekip önce **Service Health**'e bakıyor: West Europe'ta Azure SQL Database için aktif bir **service issue** var mı? Yok. **Resource Health**: veritabanı *Available*. Sorun Azure'da değil, uygulamada.
3. Application Insights'ın **bağımlılık** görünümü, ödeme servisinin dış bir kur servisine yaptığı çağrının 4 saniyeye çıktığını gösteriyor; **Log Analytics**'te KQL ile hangi sürümden sonra başladığı bulunuyor (Activity Log'da o sabah bir deployment var).
4. Olay sonrası ekip, Application Insights'taki hata oranı ve gecikme grafiklerini kalıcı bir **metric alert** ve **log alert** kuralına dönüştürüyor.
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
| "Bir uygulama isteğinin **servisler arası yolculuğu**" | **Application Insights** (distributed tracing) |

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

- **Yalnızca platform metrikleri**
  - Kazanç: Ücretsiz, otomatik, hafif
  - Bedel: Bağlam yok: "neden" sorusunu cevaplamaz
- **Log'ları Log Analytics'e toplamak**
  - Kazanç: Çok kaynaklı sorgu, kök neden analizi
  - Bedel: Veri alma (GB) ve saklama maliyeti; örnekleme/filtre planı gerekir
- **Application Insights**
  - Kazanç: Uygulama düzeyinde görünürlük, bağımlılık haritası, availability test
  - Bedel: Telemetri hacmi maliyeti; örnekleme ayarı; az da olsa uygulama yükü
- **Çok sayıda hassas uyarı**
  - Kazanç: Sorunu erken yakalama
  - Bedel: **Alert yorgunluğu**: ekip uyarıları görmezden gelmeye başlar
- **Metrik alert vs log search alert**
  - Kazanç: Metrik: hızlı ve ucuz · Log: karmaşık koşul
  - Bedel: Log alert daha gecikmeli ve sorgu başına maliyetli

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe monitoring tools in Azure](https://learn.microsoft.com/en-us/training/modules/describe-monitoring-tools-azure/) · [Describe the purpose of Azure Advisor](https://learn.microsoft.com/en-gb/training/modules/describe-monitoring-tools-azure/2-describe-purpose-of-azure-advisor)
- [Azure Advisor overview](https://learn.microsoft.com/en-us/azure/advisor/advisor-overview) · [`az advisor`](https://learn.microsoft.com/en-us/cli/azure/advisor)
- [Azure Service Health overview](https://learn.microsoft.com/en-us/azure/service-health/overview) · [Resource Health overview](https://learn.microsoft.com/en-us/azure/service-health/resource-health-overview) · [Azure status](https://azure.status.microsoft/)
- [Azure Monitor overview](https://learn.microsoft.com/en-us/azure/azure-monitor/overview) · [Alerts overview](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-overview) · [Action groups](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/action-groups)
- [Application Insights overview](https://learn.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview)
- [Log Analytics tutorial](https://learn.microsoft.com/en-us/azure/azure-monitor/logs/log-analytics-tutorial)

---

➡️ **Sıradaki:** [📝 Bu konunun quiz soruları](quiz.md)
