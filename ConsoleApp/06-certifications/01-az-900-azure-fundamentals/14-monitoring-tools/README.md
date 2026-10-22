# C1.14 Azure İzleme Araçları

> 📁 `ConsoleApp/06-certifications/01-az-900-azure-fundamentals/14-monitoring-tools/` · Sınav alanı: **Describe Azure management and governance (%30-35)** · Önkoşul: [C1.4](../04-core-architecture/README.md) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe monitoring tools in Azure* — **Azure Advisor** · **Azure Service Health** · **Azure Monitor** (Log Analytics, Azure Monitor alert'leri ve Application Insights dahil). Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

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

AZ-900 üç izleme aracını sorar; her biri **farklı bir soruya** cevap verir:

| Araç | Cevapladığı soru | Kısaca |
|---|---|---|
| **Azure Advisor** | "Kaynaklarımı **nasıl daha iyi** yapılandırabilirim?" | Kişiselleştirilmiş **öneri** servisi (5 kategori) |
| **Azure Service Health** | "**Azure'da** benim kaynaklarımı etkileyen bir sorun/bakım var mı?" | Azure Status + Service Health + Resource Health |
| **Azure Monitor** | "Uygulamam ve kaynaklarım **şu anda nasıl** çalışıyor, ne oldu?" | Metrik + log toplama, analiz (Log Analytics), uyarı (alerts), uygulama izleme (Application Insights), dashboard ve Workbooks |

Bu konuda kod yok; pratik, `az` CLI komutlarıyla (Advisor önerileri, action group, metrik ve Service Health uyarısı, Log Analytics sorgusu) yapılır. Portal ekranları (Monitor sayfası, dashboard, Workbooks galerisi) teoride anlatılır.

---

## 2. Neden?

- **Sınav:** Bu üç aracın birbirine karıştırılması bu alanın klasik tuzağıdır: "öneri" mi (Advisor), "Azure kaynaklı olay" mı (Service Health), "benim metriğim/log'um" mu (Monitor)? Service Health'in üç parçasının **kapsam farkı** da sık sorulur.
- **Gerçek işte:** Bir kesintide ilk soru "sorun bizde mi, Azure'da mı?"dır. Service Health bunu dakikalar içinde ayırır; Monitor ve Application Insights ise sorunun **nerede** olduğunu gösterir. Advisor da maliyet ve güvenilirlik iyileştirmelerini **ücretsiz** olarak listeler.
- **Bağlantılar:** Maliyet önerileri → C1.11 (Cost Management), güvenlik önerileri → C1.10 (Defender for Cloud), KQL derinliği → C2.13, observability derinliği → M13.

---

## 3. Nasıl?

### 3.1 Azure Advisor

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

### 3.2 Azure Service Health

Azure platformunun sağlık durumunu **üç kapsamda** gösterir ⭐:

| Bileşen | Kapsam | Ne gösterir? |
|---|---|---|
| **Azure Status** | **Tüm Azure**, herkese açık (`azure.status.microsoft`) | Geniş çaplı (birçok müşteriyi etkileyen) olayların genel durum sayfası; giriş gerektirmez |
| **Service Health** | **Senin** subscription'ların, kullandığın servisler ve region'lar | **Service issues** (hizmet sorunları), **planned maintenance** (planlı bakım), **health advisories** (aksiyon gerektiren değişiklikler, ör. bir özelliğin emekliliği), **security advisories**; olay geçmişi ve **kök neden analizi (RCA)** raporları; **Service Health alert**'leri |
| **Resource Health** | **Tek bir kaynak** (ör. bir VM, bir SQL veritabanı) | Kaynağın mevcut ve geçmiş sağlık durumu (Available / Unavailable / Degraded / Unknown) ve sorun Azure platformundan mı, kullanıcı eyleminden mi kaynaklanıyor |

> Kural: **genel Azure** → Azure Status · **benim aboneliğim/bölgem** → Service Health · **tek bir kaynağım** → Resource Health.

Service Health uyarıları, teknik olarak **Activity Log alert**'tir (kategori = ServiceHealth) ve bir **action group**'a (e-posta, SMS, webhook…) bağlanır (pratik → 3.6).

### 3.3 Azure Monitor

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
  // Son 1 günde en çok hata veren 5 istek (Application Insights tablosu)
  AppRequests
  | where TimeGenerated > ago(1d) and Success == false
  | summarize hatalar = count() by Name
  | top 5 by hatalar
  ```

- **Azure Monitor alerts:** bir **koşul** sağlandığında (metrik eşiği, log sorgusu sonucu, Activity Log olayı) çalışan **uyarı kuralı** → bir **action group**'u tetikler. Uyarı türleri: **metric alert**, **log search alert**, **activity log alert** (Service Health ve Resource Health uyarıları dahil); *(bağlam)* Prometheus ve smart detection uyarıları. Action group eylemleri: e-posta, SMS, push, sesli arama, **webhook**, **Logic App**, **Azure Function**, Automation runbook, ITSM.
- **Application Insights:** uygulama performans izleme (**APM**) — istek süreleri ve sayıları, **hata oranı**, **bağımlılık** çağrıları (DB, HTTP), sayfa görüntüleme ve tarayıcı performansı, exception'lar, uygulama haritası; **availability test**'ler (uygulamaya dünyanın farklı noktalarından düzenli istek atıp erişilebilirliği ölçme). Uygulama SDK/OpenTelemetry ile veya bazı servislerde **kod değişikliği olmadan** (auto-instrumentation) izlenebilir; Azure dışında (on-prem, başka bulut) çalışan uygulamaları da izler. Güncel Application Insights kaynakları bir **Log Analytics workspace**'ine bağlıdır (*workspace-based*; klasik kaynaklar 29 Şubat 2024'te emekliye ayrıldı).

### 3.4 Azure Monitor merkezi dashboard'u ve görselleştirme

- **Azure Monitor sayfası** (portal): tüm izleme yeteneklerine **tek giriş noktası** — Metrics, Logs, Alerts, Insights, Workbooks, Activity Log.
- **Azure dashboard'ları:** metrik grafiklerini, log sorgu sonuçlarını, kaynak kutucuklarını ve Markdown notlarını **tek ekranda** toplayan, kaydedilip **RBAC ile paylaşılabilen** portal panoları (ör. ekip için "üretim sağlığı" panosu). *(bağlam)* Dashboard'lar JSON olarak dışa aktarılıp şablonla dağıtılabilir; **Grafana** (Azure Managed Grafana) da Azure Monitor verisini gösterebilir.
- **Insights:** belirli kaynak türleri için **hazır (küratörlü)** izleme görünümleri — **VM insights**, **Container insights** (AKS), Storage insights, **Application Insights**.

### 3.5 Azure Monitor raporları: Workbooks

- **Workbooks:** metrik, log sorgusu, parametre (zaman aralığı, subscription seçici), grafik, tablo ve açıklama metnini birleştiren **etkileşimli rapor** belgeleri. Galeride hazır şablonlar vardır (ör. VM performansı, Activity Log özeti, Advisor önerileri); kopyalanıp özelleştirilebilir ve ekiple paylaşılabilir.
- Dashboard ↔ Workbook: dashboard **anlık bakış panosu** (birçok kaynaktan kutucuk); workbook **zengin, etkileşimli analiz raporu** (parametreli, adım adım hikâye).
- Diğer rapor yolları: Log Analytics sorgusunun sonucunu `render timechart` ile grafiğe dökmek; uyarı geçmişi ve **Activity Log** görünümleri; *(bağlam)* Power BI'a aktarma.

### 3.6 Pratik — CLI ile izleme ve uyarı

Maliyet: action group, uyarı kuralları ve boş bir storage account'un maliyeti ihmal edilebilir; Log Analytics workspace yalnızca **alınan veri** kadar ücret çıkarır (bu laboratuvarda neredeyse sıfır). Bütçe alarmın kurulu olsun (→ C1.1, C1.11). Komutlar Cloud Shell'de (Bash) çalıştırılabilir.

```bash
RG=rg-az900-c114
LOC=westeurope
az group create --name $RG --location $LOC

# 1) Advisor önerileri (abonelikteki mevcut kaynaklara göre; boş abonelikte liste boş olabilir)
az advisor recommendation list --output table
az advisor recommendation list --category Cost --output table
# CLI kategori adları: Cost, HighAvailability (= Reliability), OperationalExcellence, Performance, Security

# 2) Action group: uyarı olunca kime/ne yapılacak
az monitor action-group create --resource-group $RG --name ag-ops --short-name ops \
  --action email ops-ekibi ops@example.com
AG_ID=$(az monitor action-group show --resource-group $RG --name ag-ops --query id --output tsv)

# 3) Metrik uyarısı: storage account'taki işlem sayısı eşiği aşarsa e-posta
SA=stc114$RANDOM
az storage account create --resource-group $RG --name $SA --location $LOC --sku Standard_LRS
SA_ID=$(az storage account show --resource-group $RG --name $SA --query id --output tsv)
az monitor metrics alert create --resource-group $RG --name alert-sa-transactions \
  --scopes $SA_ID --condition "total Transactions > 1000" \
  --window-size 5m --evaluation-frequency 5m --action $AG_ID \
  --description "5 dakikada 1000'den fazla istek"
az monitor metrics list --resource $SA_ID --metric Transactions --interval PT1H --output table

# 4) Service Health uyarısı: aboneliğimi etkileyen hizmet sorunu / planlı bakım → e-posta
SUB_ID=$(az account show --query id --output tsv)
az monitor activity-log alert create --resource-group $RG --name alert-service-health \
  --scope /subscriptions/$SUB_ID --condition category=ServiceHealth --action-group $AG_ID

# 5) Log Analytics workspace + KQL sorgusu
az monitor log-analytics workspace create --resource-group $RG --workspace-name law-c114 --location $LOC
WS=$(az monitor log-analytics workspace show --resource-group $RG --workspace-name law-c114 \
  --query customerId --output tsv)
# Boş workspace'te de çalışan sorgu: satır içi tablo ile KQL'in where/summarize/order akışı
az monitor log-analytics query --workspace $WS --analytics-query '
datatable(Servis:string, SureMs:int, Basarili:bool)
["api", 120, true, "api", 900, false, "web", 80, true, "api", 1500, false]
| where Basarili == false
| summarize hata = count(), ortSure = avg(SureMs) by Servis
| order by hata desc' --output table

# 6) Uyarı kurallarını listele, sonra temizle
az monitor metrics alert list --resource-group $RG --output table
az monitor activity-log alert list --resource-group $RG --output table
az group delete --name $RG --yes --no-wait
```

> Workspace'e gerçek log akması için kaynaklarda **diagnostic settings** tanımlanır (ör. Activity Log'u workspace'e gönder → `AzureActivity` tablosu); bu, C2.13'te derinleştirilir.

---

## 4. Gerçek dünya senaryosu

Bir e-ticaret şirketinde pazartesi sabahı ödeme sayfası yavaşlıyor:

1. **Application Insights** uyarısı (log search alert: "son 5 dakikada ödeme isteklerinin hata oranı %5'i geçti") **action group** üzerinden nöbetçi ekibe SMS ve Teams webhook'u gönderiyor.
2. Ekip önce **Service Health**'e bakıyor: West Europe'ta Azure SQL Database için aktif bir **service issue** var mı? Yok. **Resource Health**: veritabanı *Available*. Sorun Azure'da değil, uygulamada.
3. Application Insights'ın **bağımlılık** görünümü, ödeme servisinin dış bir kur servisine yaptığı çağrının 4 saniyeye çıktığını gösteriyor; **Log Analytics**'te KQL ile hangi sürümden sonra başladığı bulunuyor (Activity Log'da o sabah bir deployment var).
4. Olay sonrası ekip, haftalık toplantı için hata oranı ve gecikmeyi gösteren bir **Workbook** hazırlıyor; operasyon ekranına bir **dashboard** sabitleniyor.
5. Aynı hafta **Advisor**, gece boş çalışan iki büyük test VM'ini (Cost) ve tek instance çalışan bir üretim VM'ini (Reliability) öneri olarak listeliyor; ekip birini kapatıyor, diğerini zone'lara yayıyor.

---

## 5. Sınav perspektifi

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

## 6. Yaygın hatalar

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

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Yalnızca platform metrikleri | Ücretsiz, otomatik, hafif | Bağlam yok: "neden" sorusunu cevaplamaz |
| Log'ları Log Analytics'e toplamak | Çok kaynaklı sorgu, kök neden analizi | Veri alma (GB) ve saklama maliyeti; örnekleme/filtre planı gerekir |
| Application Insights | Uygulama düzeyinde görünürlük, bağımlılık haritası, availability test | Telemetri hacmi maliyeti; örnekleme ayarı; az da olsa uygulama yükü |
| Çok sayıda hassas uyarı | Sorunu erken yakalama | **Alert yorgunluğu**: ekip uyarıları görmezden gelmeye başlar |
| Metrik alert vs log search alert | Metrik: hızlı ve ucuz · Log: karmaşık koşul | Log alert daha gecikmeli ve sorgu başına maliyetli |
| Dashboard vs Workbook | Dashboard: anlık bakış · Workbook: derin, etkileşimli rapor | İkisini birden sürdürmek bakım ister |

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe monitoring tools in Azure](https://learn.microsoft.com/en-us/training/modules/describe-monitoring-tools-azure/) · [Describe the purpose of Azure Advisor](https://learn.microsoft.com/en-gb/training/modules/describe-monitoring-tools-azure/2-describe-purpose-of-azure-advisor)
- [Azure Advisor overview](https://learn.microsoft.com/en-us/azure/advisor/advisor-overview) · [`az advisor`](https://learn.microsoft.com/en-us/cli/azure/advisor)
- [Azure Service Health overview](https://learn.microsoft.com/en-us/azure/service-health/overview) · [Resource Health overview](https://learn.microsoft.com/en-us/azure/service-health/resource-health-overview) · [Azure status](https://azure.status.microsoft/)
- [Azure Monitor overview](https://learn.microsoft.com/en-us/azure/azure-monitor/overview) · [Alerts overview](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-overview) · [Action groups](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/action-groups)
- [Application Insights overview](https://learn.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview) · [Classic Application Insights retirement (29 Şubat 2024)](https://azure.microsoft.com/updates/we-re-retiring-classic-application-insights-on-29-february-2024)
- [Log Analytics tutorial](https://learn.microsoft.com/en-us/azure/azure-monitor/logs/log-analytics-tutorial) · [Azure Workbooks](https://learn.microsoft.com/en-us/azure/azure-monitor/visualize/workbooks-overview) · [Azure dashboards](https://learn.microsoft.com/en-us/azure/azure-portal/azure-portal-dashboards)
