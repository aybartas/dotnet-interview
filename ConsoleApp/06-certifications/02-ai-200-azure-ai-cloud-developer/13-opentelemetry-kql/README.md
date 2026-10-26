# C2.13 OpenTelemetry ile İzleme ve KQL ile Sorun Giderme

> 📁 `ConsoleApp/06-certifications/02-ai-200-azure-ai-cloud-developer/13-opentelemetry-kql/` · Sınav alanı: **Secure, monitor, and troubleshoot Azure solutions (%20-25)** · Önkoşul: C1.14 (Azure Monitor temelleri), M13 (observability), [C2.9](../09-service-bus/README.md) (Service Bus) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of October 7, 2026):** *Secure, monitor, and troubleshoot Azure solutions* — **OpenTelemetry** SDK'larıyla **dağıtık sistemleri izle** (tracing) · log ve metrikleri analiz eden **KQL** sorguları yaz. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)'ı kontrol et.

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

İki beceri: **telemetri üretmek** (OpenTelemetry ile enstrümantasyon) ve **telemetriyi sorgulamak** (KQL ile Log Analytics / Application Insights).

**OpenTelemetry (OTel)** satıcıdan bağımsız, CNCF'in gözlemlenebilirlik standardıdır: API + SDK + exporter + wire protokolü (OTLP). Üç sinyal:

| Sinyal | Ne? | .NET karşılığı | Python karşılığı |
|---|---|---|---|
| **Trace** | Bir isteğin servisler arası yolu; **span** ağacı | `ActivitySource` / `Activity` | `tracer.start_as_current_span` |
| **Metric** | Zamanla ölçülen sayısal değer (counter, histogram, gauge) | `System.Diagnostics.Metrics.Meter` | `meter.create_counter` |
| **Log** | Zaman damgalı olay kaydı; trace'e bağlanır (`TraceId`/`SpanId`) | `ILogger` | `logging` |

| Kavram | Tek cümle |
|---|---|
| **Span** | Tek bir işin süresi + öznitelikleri (ad, başlangıç/bitiş, durum, `kind`: Server/Client/Producer/Consumer/Internal) |
| **Trace** | Aynı **trace-id**'yi taşıyan span'ler topluluğu (uçtan uca istek) |
| **Context propagation** | trace-id/span-id'nin süreç ve servis sınırlarını **geçirilmesi** — HTTP'de `traceparent` header'ı, mesajda mesaj özelliği |
| **W3C Trace Context** | `traceparent: 00-<trace-id 32 hex>-<parent-span-id 16 hex>-<flags>` (`01` = sampled) ve `tracestate` |
| **Resource** | Telemetriyi üreten varlığın kimliği: `service.name`, `service.instance.id` (App Insights'ta `cloud_RoleName`/`AppRoleName`) |
| **Sampler** | Hangi trace'lerin kaydedileceğine karar verir (trace-id'ye göre → aynı trace tüm servislerde **tutarlı**) |
| **Exporter** | Telemetriyi hedefe yollar (Azure Monitor, OTLP → Collector) |
| **Azure Monitor OpenTelemetry Distro** | OTel SDK + otomatik enstrümantasyon + Azure Monitor exporter'ı **tek çağrıda** (`UseAzureMonitor()` / `configure_azure_monitor()`) |

**Application Insights**, Azure Monitor'ün APM özelliğidir; **workspace-based** olarak telemetriyi bir **Log Analytics workspace**'te saklar, yani aynı veriyi **KQL** ile sorgularsın.

---

## 2. Neden?

- **Sınav:** Hedef iki yönlüdür: tracing/OTel kavramları (trace–span–context, sampling) ve **KQL** (okuma + eksik operatörü tamamlama). Kod tamamlama: `UseAzureMonitor()`, `configure_azure_monitor()`, `ActivitySource`; KQL: `summarize ... by bin(timestamp, 5m)`.
- **Gerçek işte:** Mikroservis + kuyruk mimarisinde tek bir log dosyası hatayı bulmaz; **hangi servis, hangi bağımlılık, hangi istek** sorusunu yalnızca korelasyonlu trace cevaplar. OTel ile satıcıya kilitlenmeden (Azure Monitor → başka backend) enstrümante edersin.
- **Sınavın "monitor" kısmı:** Hata oranı alert'i, action group, "en yavaş bağımlılık", `operation_Id` ile uçtan uca trace — sorun giderme akışı. Gözlemlenebilirlik teorisi → M13; sampling derinliği → M13.3.

---

## 3. Nasıl?

### 3.1 Veri akışı ve Application Insights tabloları

```
 API (UseAzureMonitor) ──traceparent──► Service Bus mesajı ──► Worker (UseAzureMonitor)
        │  spans/metrics/logs                                        │
        └────────────────► Azure Monitor exporter ◄──────────────────┘
                                  │  (connection string)
                                  ▼
                    Application Insights  ──►  Log Analytics workspace  ──► KQL / Alerts / Workbooks
```

> **Connection string** kullan (`InstrumentationKey=…;IngestionEndpoint=…`); yalnızca instrumentation key ile veri alımı **sonlandırıldı** (2025). Ortam değişkeni: `APPLICATIONINSIGHTS_CONNECTION_STRING`.

| Klasik (App Insights **Logs**) | Workspace tablosu (Log Analytics) | Ne? |
|---|---|---|
| `requests` | `AppRequests` | Gelen istek / iş (Server, Consumer span) |
| `dependencies` | `AppDependencies` | Giden çağrı: HTTP, SQL, Service Bus, Cosmos (Client/Producer span) |
| `exceptions` | `AppExceptions` | İstisnalar |
| `traces` | `AppTraces` | `ILogger` / log kayıtları |
| `customMetrics` | `AppMetrics` | Özel metrikler (`Meter`) |
| `customEvents` | `AppEvents` | Özel olaylar |
| `pageViews`, `availabilityResults` | `AppPageViews`, `AppAvailabilityResults` | Tarayıcı / availability test |

Sütun eşlemesi (en sık kullanılanlar):

| Klasik | Workspace | Anlamı |
|---|---|---|
| `timestamp` | `TimeGenerated` | Zaman (UTC) |
| `name` | `Name` | İstek/bağımlılık adı |
| `duration` (ms) | `DurationMs` | Süre |
| `success` | `Success` | Başarı (bool) |
| `resultCode` | `ResultCode` | HTTP/sonuç kodu |
| `operation_Id` | `OperationId` | **Trace-id** — uçtan uca kimlik |
| `id` / `operation_ParentId` | `Id` / `ParentId` | Span-id / üst span-id |
| `cloud_RoleName` | `AppRoleName` | Servis adı (`service.name`) |
| `itemCount` | `ItemCount` | Örnekleme sonrası temsil edilen kayıt sayısı |
| `customDimensions` | `Properties` | Özel öznitelikler (dynamic) |

> Sınav ekranları App Insights **Logs** bölümündeki **klasik** adları (`requests`, `timestamp`, `duration`) da, Log Analytics'teki **`App*`** adlarını da kullanabilir; ikisini de oku.

### 3.2 Enstrümantasyon: Distro, otomatik ve elle

```csharp
// Program.cs — API (ASP.NET Core). NuGet: Azure.Monitor.OpenTelemetry.AspNetCore
using Azure.Monitor.OpenTelemetry.AspNetCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

AppContext.SetSwitch("Azure.Experimental.EnableActivitySource", true);   // messaging SDK tracing is opt-in

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService(serviceName: "orders-api"))        // -> cloud_RoleName
    .UseAzureMonitor(o =>
    {
        o.ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];
        o.SamplingRatio = 0.2f;        // keep ~20 % of traces (fixed percentage); see 3.5
        // o.TracesPerSecond = 5;      // alternative: rate-limited sampling
    })
    .WithTracing(t => t.AddSource(Telemetry.ActivitySourceName,   // our own spans
                                  "Azure.*"));                     // Azure SDK spans (Service Bus, ...)

var app = builder.Build();
app.MapPost("/orders", async (OrderDto dto, ServiceBusSender sender, ILogger<Program> log) =>
{
    using var activity = Telemetry.Source.StartActivity("orders.create");   // manual span (Internal)
    activity?.SetTag("order.id", dto.Id);
    activity?.SetTag("order.amount", dto.Amount);

    await sender.SendMessageAsync(new ServiceBusMessage(BinaryData.FromObjectAsJson(dto)));
    // the Azure SDK adds a Producer span and writes the trace context into the message ("Diagnostic-Id")
    log.LogInformation("Order {OrderId} queued", dto.Id);   // log is correlated with the active trace
    return Results.Accepted();
});
app.Run();

public static class Telemetry
{
    public const string ActivitySourceName = "Orders.Api";
    public static readonly ActivitySource Source = new(ActivitySourceName);
}
```

```csharp
// Worker (Generic Host). Same call; the processor continues the trace from the message.
builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("orders-worker"))
    .UseAzureMonitor()
    .WithTracing(t => t.AddSource("Orders.Worker", "Azure.*"));

processor.ProcessMessageAsync += async args =>
{
    using var a = WorkerTelemetry.Source.StartActivity("orders.process");   // child of the message's trace context
    // ... call DB / HTTP -> shows up as dependencies under the same operation_Id
    await args.CompleteMessageAsync(args.Message);
};
```

- **Otomatik enstrümantasyon:** ASP.NET Core (gelen istek), `HttpClient`, SQL client; Azure SDK'lar (kararlı olanlar) — Distro etkin tutar. **Messaging SDK'ları (Service Bus, Event Hubs)** için ActivitySource desteği **deneysel**dir ve ortam değişkeni `AZURE_EXPERIMENTAL_ENABLE_ACTIVITY_SOURCE=true` / `AppContext` anahtarı gerektirir *(2026 başı itibarıyla; Azure.Messaging.ServiceBus sürüm notlarından doğrula)*.
- **Elle enstrümantasyon:** `ActivitySource` + `StartActivity` + `SetTag`/`AddEvent`/`SetStatus`; metrik için `Meter.CreateCounter<long>("orders.created")`.
- **Context propagation:** HTTP'de `traceparent` otomatik; **Service Bus**'ta gönderen mesaja `Diagnostic-Id` (W3C traceparent) özelliğini yazar, işleyici bunu çıkarır → API'deki istek ve worker'daki işlem **aynı `operation_Id`** altında görünür. Propagation'ın otomatik olmadığı kanallarda (ör. custom protokol) `traceparent`'i `Propagators` ile elle enjekte/çıkar.
- **Application Insights SDK (klasik)** ile fark: yeni projelerde **OTel Distro** önerilir; klasik SDK'nın telemetri modeli (`TelemetryClient.TrackEvent`) ve OTel'in span modeli farklıdır; eşleme tablosu yukarıdadır.
- *Not:* Microsoft yeni bir birleşik API (`UseMicrosoftOpenTelemetry`) üzerinde de çalışıyor; `UseAzureMonitor()` sınavda hâlâ kanonik çağrıdır — paket sürümüne göre doğrula.

```python
# Python (azure-monitor-opentelemetry). Call BEFORE importing frameworks you want auto-instrumented.
import os
from azure.monitor.opentelemetry import configure_azure_monitor
from opentelemetry import trace, metrics

configure_azure_monitor(
    connection_string=os.environ["APPLICATIONINSIGHTS_CONNECTION_STRING"],
    # sampling_ratio=0.2,            # fixed-percentage; newer versions also support traces_per_second
)

tracer = trace.get_tracer("orders.api")
orders_created = metrics.get_meter("orders.api").create_counter("orders.created")

def create_order(order):
    with tracer.start_as_current_span("orders.create") as span:       # manual span
        span.set_attribute("order.id", order["id"])
        span.set_attribute("order.amount", order["amount"])
        # servicebus_sender.send_messages(...)  # context propagates when Azure SDK tracing is enabled
        orders_created.add(1, {"channel": "web"})
```

### 3.3 KQL temelleri ⭐

KQL bir **boru hattı**dır: tablo `|` operatör `|` operatör. Her adım bir önceki adımın çıktısını işler.

| Operatör | Ne yapar? | Not |
|---|---|---|
| `where` | Satırları süzer | Zaman filtresini **en başa** koy (`where timestamp > ago(1h)`) → hızlı |
| `project` | Sütun seç / yeniden adlandır | `project-away` çıkarır, `project-rename` adlandırır |
| `extend` | **Hesaplanan** sütun ekler | Satırı değiştirmez, sütun ekler |
| `summarize … by` | Gruplayıp **topla** (`count()`, `sum()`, `avg()`, `percentile()`, `dcount()`, `countif`, `sumif`, `arg_max`) | Zaman kovası: `by bin(timestamp, 5m)` |
| `top N by x` | Sırala + ilk N | `take N` = **rastgele** N satır (sıralamaz) |
| `order by` / `sort by` | Sırala | Varsayılan **desc** |
| `join kind=… ` | İki tabloyu anahtarla birleştir | **Varsayılan `innerunique`** (sol anahtarı tekilleştirir!) — beklenmedik satır kaybı; `kind=inner` açıkça yaz |
| `union` | Tabloları alt alta ekler | Sütunlar birleşir |
| `let` | Değişken/alt sorgu | `let window = 5m;` |
| `render timechart` | Grafik | Zaman ekseni gerekir (`bin` ile) |
| `parse_json`, `tostring`, `todynamic` | `dynamic` (customDimensions) alanları | `tostring(customDimensions.orderId)` |

String ve karşılaştırma tuzakları: `==` **büyük/küçük harfe duyarlı**, `=~` duyarsız; `has` (tam terim, **indeksli, hızlı**) ↔ `contains` (alt dize, yavaş); `in`, `!in`, `between (a .. b)`. Zaman: `ago(1h)`, `now()`, `startofday()`, **her zaman UTC**.

```kusto
// 1) Slowest 5 endpoints by p95 latency (last hour)  -- classic App Insights names
requests
| where timestamp > ago(1h)
| summarize p95 = percentile(duration, 95), avg_ms = avg(duration), calls = sum(itemCount) by name
| top 5 by p95 desc

// 2) Error rate every 5 minutes
requests
| where timestamp > ago(1h)
| summarize total = sum(itemCount), failed = sumif(itemCount, success == false) by bin(timestamp, 5m)
| extend errorRatePct = round(100.0 * failed / total, 2)
| project timestamp, errorRatePct
| render timechart

// 3) End-to-end flow of one operation (API -> Service Bus -> worker -> DB)
union requests, dependencies, exceptions, traces
| where operation_Id == "4bf92f3577b34da6a3ce929d0e0e4736"
| project timestamp, itemType, cloud_RoleName, name = coalesce(name, message, outerMessage),
          duration, success, id, operation_ParentId
| order by timestamp asc

// 4) Which dependency is slow? (database, queue, HTTP ...)
dependencies
| where timestamp > ago(1h)
| summarize p95 = percentile(duration, 95), failures = sumif(itemCount, success == false), calls = sum(itemCount)
            by type, target
| top 5 by p95 desc

// 5) Failed requests together with their exceptions (explicit join kind)
requests
| where timestamp > ago(1h) and success == false
| join kind=inner (exceptions | where timestamp > ago(1h)) on operation_Id
| project timestamp, name, resultCode, exceptionType = type, outerMessage, operation_Id

// 6) Custom metric (Meter) per hour
customMetrics
| where name == "orders.created"
| summarize orders = sum(valueSum) by bin(timestamp, 1h)
| render columnchart
```

Aynı sorgular Log Analytics'te workspace adlarıyla: `AppRequests | where TimeGenerated > ago(1h) | summarize p95 = percentile(DurationMs, 95) by Name | top 5 by p95 desc`.

**Sonuç okuma kuralları (sınav sorusu kalıbı):**
- `summarize count() by bin(timestamp, 5m)` yalnızca **verisi olan** kovalar için satır döner (boş kova = satır yok; boşlukları göstermek için `make-series`).
- `summarize` sonrası yalnızca `by` sütunları + hesaplanan sütunlar kalır; `where` summarize'dan **sonra** gelirse toplanmış değeri süzer.
- `top 5 by p95 desc` ≠ `take 5`.
- `percentile(duration, 95)` tek sayı; birden çok yüzdelik için `percentiles(duration, 50, 95, 99)`.
- Örneklenmiş veride **`count()` eksik sayar**; `sum(itemCount)` kullan (3.5).

**Platform logları (workspace tabloları):**

| Kaynak | Tablo (2026 başı itibarıyla — doğrula) | Örnek |
|---|---|---|
| Container Apps | `ContainerAppConsoleLogs_CL` / kaynağa özgü `ContainerAppConsoleLogs`; `ContainerAppSystemLogs` | `ContainerAppConsoleLogs_CL \| where ContainerAppName_s == "orders-api" and Log_s has "error"` |
| AKS (Container Insights) | `ContainerLogV2`, `KubePodInventory`, `KubeEvents` | `ContainerLogV2 \| where PodName startswith "orders" and LogMessage has "Exception"` |
| Azure kaynak logları/metrikleri | `AzureDiagnostics`, kaynağa özgü tablolar, `AzureMetrics`, `AzureActivity` | `AzureActivity \| where OperationNameValue has "delete"` |
| Key Vault denetim | `AZKVAuditLogs` / `AzureDiagnostics` | kim hangi secret'ı okudu (→ [C2.11](../11-identity-key-vault/README.md)) |

### 3.4 Uyarılar (alerts) ve sorun giderme akışı

| Alert türü | Veri | Gecikme/maliyet | Ne zaman? |
|---|---|---|---|
| **Metric alert** | Platform/özel **metrik** | Dakika düzeyi, ucuz | Eşik: CPU, istek sayısı, kuyruk uzunluğu |
| **Log search alert** (scheduled query rule) | **KQL** sonucu | Ingestion gecikmesi + değerlendirme aralığı; her değerlendirme ücretli | "Hata oranı > %5", karmaşık koşul |
| **Activity log alert** | Control plane olayı | — | "Kaynak silindi" |
| Smart detection | Application Insights anormallik | — | Otomatik, ayar gerektirmez |

**Action group:** bildirim/eylem listesi — e-posta, SMS, push, sesli arama, **webhook, Azure Function, Logic App**, ITSM, Automation runbook. Alert kuralı bir veya birden çok action group'a bağlanır; **alert processing rule** bakım penceresinde bastırır.

**Sorun giderme akışı (sınavın kalıbı):**
1. **Belirti:** hata oranı alert'i / Failures blade → *hangi endpoint, hangi sonuç kodu?* (sorgu 2, `resultCode` kırılımı)
2. **Daralt:** *Hangi bağımlılık yavaş/başarısız?* (sorgu 4: `dependencies` → `target`/`type`)
3. **Örnek trace:** bir başarısız isteğin `operation_Id`'si → **end-to-end transaction** / sorgu 3
4. **Neden:** `exceptions` (`outerMessage`, stack), ilgili `traces`, dağıtım/yapılandırma değişikliği (`AzureActivity`)
5. **Doğrula:** düzeltmeden sonra hata oranı normale dönüyor mu?

### 3.5 Sampling

- **Sampling**, ingestion maliyetini düşürmek için telemetrinin yalnızca bir kısmını **saklar**. Karar **trace-id**'ye göre verilir → bir trace ya **tamamen** ya da hiç kaydedilmez (parent-based: alt servisler ebeveynin kararına uyar). `traceparent` flag'i (`01`) örneklendi demektir.
- Distro seçenekleri: **sabit yüzde** (`SamplingRatio`) veya **hız sınırlı** (`TracesPerSecond`, trafik arttıkça oran düşer). Hangisinin varsayılan olduğu sürüme göre değişir (rate-limited ≈ 5 trace/sn belgelenmiştir — doğrula); ortam değişkenleri: `OTEL_TRACES_SAMPLER` (`microsoft.rate_limited` / `microsoft.fixed_percentage`), `OTEL_TRACES_SAMPLER_ARG`.
- **Etkisi:** `requests | count` gerçek istek sayısını **eksik** gösterir → `sum(itemCount)`; p95 gibi dağılımlar örneklemde bile makul kalır, **nadir hatalar** kaybolabilir (tail-based sampling OTel Collector gerektirir). Metrikler (`customMetrics`/standart metrikler) örneklenmez → alert'leri **metrik** üzerine kurmak daha güvenilirdir.
- Sampling'i **tüm servislerde aynı** kararla kullan; aksi hâlde trace'ler kopuk görünür.

### 3.6 Lab — API + worker'ı enstrümante et, KQL setini çalıştır, hata oranı alert'i kur

Maliyet: Application Insights/Log Analytics **GB başına** ücretlenir (ilk ay/aylık ücretsiz kota bulunur — doğrula); lab verisi KB'dır. Daily cap koy, **bütçe alarmı** kur (→ C1.11). Service Bus'ı **emülatörle** (C2.9) çalıştırabilirsin; telemetri gerçek bir Application Insights kaynağına gider (emülatörü yok).

```bash
RG=rg-c213-lab
LOC=westeurope
az group create --name $RG --location $LOC
az extension add --name application-insights --upgrade --yes
az extension add --name scheduled-query --upgrade --yes
az extension add --name log-analytics --upgrade --yes

# 1) Workspace (with a daily cap) + workspace-based Application Insights
az monitor log-analytics workspace create -g $RG -n law-c213 -l $LOC --quota 1      # 1 GB/day cap
WS_ID=$(az monitor log-analytics workspace show -g $RG -n law-c213 --query id -o tsv)
az monitor app-insights component create --app appi-c213 -g $RG -l $LOC --workspace $WS_ID --kind web
CS=$(az monitor app-insights component show --app appi-c213 -g $RG --query connectionString -o tsv)

# 2) Run API + worker locally with the connection string (Service Bus emulator from C2.9)
export APPLICATIONINSIGHTS_CONNECTION_STRING="$CS"
#   dotnet run --project Orders.Api   &   dotnet run --project Orders.Worker
#   for i in $(seq 1 50); do curl -s -X POST localhost:5000/orders -H 'content-type: application/json' \
#        -d "{\"id\":\"o-$i\",\"amount\":$((i*30))}"; done
#   force failures: make the worker throw for amount > 1200 (like C2.9's poison messages)

# 3) Query (data appears after ~2-5 minutes of ingestion latency)
WS_CUSTOMER_ID=$(az monitor log-analytics workspace show -g $RG -n law-c213 --query customerId -o tsv)
az monitor log-analytics query -w $WS_CUSTOMER_ID -o table --analytics-query "
AppRequests
| where TimeGenerated > ago(1h)
| summarize p95 = percentile(DurationMs, 95), calls = sum(ItemCount) by Name
| top 5 by p95 desc"

# 4) Action group (e-mail) + log search alert: error rate > 5 % in the last 5 minutes
AG_ID=$(az monitor action-group create -g $RG -n ag-c213 --short-name ag213 \
          --action email oncall oncall@example.com --query id -o tsv)
az monitor scheduled-query create -g $RG -n alert-error-rate --scopes $WS_ID \
  --condition "count 'Q1' > 0" \
  --condition-query Q1="AppRequests
      | summarize total = sum(ItemCount), failed = sumif(ItemCount, Success == false)
      | where total > 20 and 100.0 * failed / total > 5" \
  --window-size 5m --evaluation-frequency 5m --severity 2 \
  --action-groups $AG_ID --description "Request failure rate above 5 percent"

# 5) Cleanup (workspace data is deleted with the group; soft-delete of the workspace may hold the name ~14 days)
az group delete --name $RG --yes --no-wait
```

Sonra portalda **Application Insights → Application map** (API → Service Bus → worker), **Transaction search** → bir isteğe tıkla → *end-to-end transaction* (aynı `operation_Id`), ve **Logs** bölümünde 3.3'teki altı sorguyu çalıştır. Worker'ı `amount > 1200` için hata fırlatacak şekilde ayarladıysan 5 dakikalık hata oranı grafiğinde (sorgu 2) sıçramayı ve alert'in tetiklenmesini gözlemle.

> **Alıştırma.** Sorgu: `requests | where timestamp > ago(30m) | summarize n = count() by bin(timestamp, 10m)` 3 satır döndürmesi beklenirken 2 döndürdü. İlk akla gelen iki neden?
>
> <details><summary>Çözüm</summary>
>
> (1) Bir kovada **hiç istek yok** — `summarize` boş kovaları yazmaz (`make-series` ile doldurulur). (2) Zaman penceresi kovalara tam oturmaz (`ago(30m)` ilk kovayı kısmen keser) veya telemetri **ingestion gecikmesi** yüzünden son kova henüz gelmemiştir. Örnekleme ise satır sayısını değil, `n` değerini (eksik sayma) etkiler → `sum(itemCount)`.
> </details>

---

## 4. Gerçek dünya senaryosu

Bir sipariş sisteminde `orders-api` (Container Apps) siparişi Service Bus'a atar, `orders-worker` işler ve PostgreSQL'e yazar. Gece yarısı kampanya sırasında müşteriler "sipariş onayı gelmiyor" diyor:

| Adım | Eylem | Bulgu |
|---|---|---|
| 1. Alert | "Hata oranı > %5" log alert'i action group ile e-posta + Teams webhook'u yolladı | 00:12'de başladı |
| 2. Belirti | Sorgu 2 (5 dk hata oranı) | Yalnızca **worker**'da `AppRoleName == "orders-worker"` hataları var; API 202 dönüyor |
| 3. Daralt | Sorgu 4 (`dependencies` → p95/failure by `target`) | PostgreSQL `target` p95 8 sn, timeout'lar |
| 4. Örnek trace | Başarısız bir `operation_Id` → sorgu 3 | API span → Service Bus producer → worker consumer → SQL span (süre 30 sn, hata) **tek trace**'te |
| 5. Neden | `exceptions` + `AppTraces` | Connection pool tükenmiş; kampanyada worker ölçeği 20 replikaya çıkmış |
| 6. Çözüm | Pool boyutu/`MaxConcurrentCalls` ayarı (→ C2.12 ile canlı yenileme), KEDA `maxReplicas` sınırı (→ C2.2) | Hata oranı normale döndü |
| 7. Önlem | Metrik tabanlı alert (kuyruk uzunluğu), sampling oranı gözden geçirildi (`sum(itemCount)`) | — |

Mesaj kuyruğu olmasaydı bu zinciri bağlayan **context propagation** olmadan API'de başarı, worker'da hata iki ayrı, ilişkisiz kayıt olarak kalırdı.

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** (a) Verilen KQL'in sonucunu okuma, (b) eksik operatörü tamamlama, (c) OTel kavramlarını eşleştirme, (d) sorun giderme senaryosunda doğru adım/araç, (e) `UseAzureMonitor`/`configure_azure_monitor` kod tamamlama.

| Senaryodaki ifade | Cevap |
|---|---|
| "5 dakikalık dilimlerde hata oranı" | `summarize … by bin(timestamp, 5m)` + `extend` oran |
| "En yavaş 5 endpoint (p95)" | `summarize percentile(duration, 95) by name` + `top 5 by … desc` |
| "Tek isteğin tüm servislerdeki izi" | **`operation_Id`** ile `union requests, dependencies, …` |
| "Yeni sütun hesapla" | `extend` (`project` sütun **seçer**) |
| "Sonucu grafik olarak göster" | `render timechart` (zaman ekseni için `bin`) |
| "İlk N satır sıralı" | `top N by` (`take` rastgele) |
| "İki tabloyu birleştir, satır kaybı olmasın" | `join kind=inner` (varsayılan `innerunique` tuzağı) |
| "Servisler arası istek korelasyonu" | **W3C `traceparent`** / context propagation |
| "Kodu değiştirmeden/az kodla Azure Monitor'e telemetri" | **Azure Monitor OpenTelemetry Distro** |
| "Özel iş adımını trace'te göster" | Elle span: `ActivitySource.StartActivity` |
| "Maliyeti düşür, trace'ler bütün kalsın" | **Sampling** (trace-id tabanlı, tüm servislerde aynı) |
| "Örneklemeli veride gerçek istek sayısı" | `sum(itemCount)` |
| "Hata oranı eşiğini aşınca e-posta" | **Log search alert** (veya metric alert) + **action group** |
| "Log/metrik/trace'i tek yerde sorgula" | **Log Analytics workspace** + KQL |
| "Container Apps konsol logu" | `ContainerAppConsoleLogs` tablosu |

**Tuzaklar:**
- **Trace ≠ span ≠ log:** trace = span'ler topluluğu; log trace'e bağlanabilir ama span değildir.
- **Context propagation yoksa trace kopar** (kuyruk, custom protokol). Mesajlaşmada SDK'nın tracing'i açık olmalı.
- `count()` örneklenmiş telemetride **eksik** sayar.
- `summarize` yalnızca verisi olan kovaları döner.
- `project` yeni hesaplama için değil, seçim içindir (`extend`); `where` summarize'dan önce/sonra anlamı değiştirir.
- `has` ile `contains`, `==` ile `=~` farkı.
- Distro'nun varsayılan sampler'ı sürüme göre değişir; üretimde oranı `SamplingRatio`/`TracesPerSecond` ile **açıkça** ayarla.
- Alert için **ingestion gecikmesi**: log alert'i gerçek zamanlı değildir; hızlı tepki gerekiyorsa metrik.
- Instrumentation key değil **connection string**.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| `configure_azure_monitor()`'ü framework import'larından **sonra** çağırmak | Önce çağır; otomatik enstrümantasyon patch'i erken ister |
| `ActivitySource`'u `AddSource` ile kaydetmemek | Kaydedilmeyen kaynağın span'leri **dinlenmez/yollanmaz** |
| Worker'da distro eklememek | API'de iz var, worker'da yok → iki ayrı iz |
| Servis adını vermemek | Hepsi `unknown_service` → Application map okunmaz; `service.name` ver |
| Örneklemeli veride `count()` ile iş metriği hesaplamak | `sum(itemCount)` veya özel metrik (örneklenmez) |
| Her şeyi `traces` olarak loglamak | Yapılandırılmış log + metrik; trace/dependency otomatik gelir |
| `join` varsayılanına güvenmek | `kind=inner/leftouter` yaz |
| Zaman filtresini sorgunun sonuna koymak | `where timestamp > ago(…)` en başa |
| Çok geniş zaman aralığıyla tüm tabloyu taramak | Dar aralık + `project` ile sütun azalt |
| Hata alert'ini tek log satırına bağlamak | Oran/eşik + pencere; `total > N` koşulu ile gürültüyü ele |
| Kişisel veriyi `SetTag`/log'a yazmak | PII'yi telemetriye koyma; maskele |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Sampling oranını düşürmek | Ingestion maliyeti ↓ | Nadir hata ve kuyruk istatistiği kaybı; `count()` yanıltır |
| %100 sampling | Tam görünürlük | Maliyet ve depolama |
| Otomatik enstrümantasyon | Sıfıra yakın kod | Kontrolsüz hacim (gürültülü bağımlılıklar); filtre gerekebilir |
| Elle span'ler | İş anlamı (sipariş no, tutar) | Kod yükü; yüksek kardinalite öznitelikleri maliyeti artırır |
| Log alert ↔ metric alert | Log: esnek KQL koşulu · Metric: hızlı ve ucuz | Log: gecikme + değerlendirme ücreti · Metric: yalnızca metrik |
| Doğrudan Azure Monitor exporter ↔ **OTel Collector** | Collector: yönlendirme, tail sampling, çoklu backend | Ek bileşen işletimi |
| Uzun retention | Geçmiş analiz | Depolama ücreti; arşiv/Basic logs katmanları |
| `summarize` ile ön hesap | Hızlı panolar | Ayrıntıdan vazgeçme; ham veri gerekirse kaynak tabloya dön |

---

## 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Azure Monitor OpenTelemetry — enable for .NET, Node.js, Python, Java](https://learn.microsoft.com/en-us/azure/azure-monitor/app/opentelemetry-enable) · [Configure OpenTelemetry in Application Insights](https://learn.microsoft.com/en-us/azure/azure-monitor/app/opentelemetry-configuration) (sampling, connection string)
- [Azure Monitor Distro client library for .NET](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/monitor.opentelemetry.aspnetcore-readme) · [azure-monitor-opentelemetry (Python)](https://learn.microsoft.com/en-us/python/api/overview/azure/monitor-opentelemetry-readme)
- [Azure SDK for .NET — distributed tracing / diagnostics](https://learn.microsoft.com/en-us/dotnet/azure/sdk/observability) · [Service Bus tracing sample](https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/servicebus/Azure.Messaging.ServiceBus/samples/Sample19_DistributedTracing.md)
- [OpenTelemetry concepts](https://opentelemetry.io/docs/concepts/) · [W3C Trace Context](https://www.w3.org/TR/trace-context/)
- [Application Insights telemetry data model](https://learn.microsoft.com/en-us/azure/azure-monitor/app/data-model-complete) · [Log Analytics tables reference (AppRequests…)](https://learn.microsoft.com/en-us/azure/azure-monitor/reference/tables/apprequests)
- [KQL overview](https://learn.microsoft.com/en-us/kusto/query/) · [KQL quick reference](https://learn.microsoft.com/en-us/kusto/query/kql-quick-reference)
- [Azure Monitor alerts](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-overview) · [Action groups](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/action-groups) · [`az monitor scheduled-query`](https://learn.microsoft.com/en-us/cli/azure/monitor/scheduled-query)
- [Container Apps logging](https://learn.microsoft.com/en-us/azure/container-apps/log-monitoring)

> **Doğrulama notu:** Distro'nun varsayılan sampler'ı, Service Bus ActivitySource'unun deneysel bayrağı, Container Apps tablo adları ve Python parametre adları (`sampling_ratio` / `traces_per_second`) sürüme göre değişir; sınav öncesi güncel sayfaları kontrol et. Kavramlar (trace–span–context, `summarize … bin()`, `operation_Id`) kararlıdır.
