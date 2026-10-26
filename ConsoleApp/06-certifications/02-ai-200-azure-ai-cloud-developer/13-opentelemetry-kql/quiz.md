# C2.13 OpenTelemetry ile İzleme ve KQL ile Sorun Giderme — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Secure, monitor, and troubleshoot Azure solutions* (%20-25) · 18 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, senaryo, kod/CLI/KQL tamamlama, vaka); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. KQL soruları App Insights **Logs** bölümündeki klasik tablo adlarını kullanır. Hedef: **≥ 15/18** (≥ %80).

---

### 1. Tek seçim
Bir API, Service Bus'a mesaj gönderiyor; worker mesajı işliyor. Her iki servis de OpenTelemetry ile enstrümante edildi, ama Application Insights'ta API'deki istek ile worker'daki işlem **ayrı trace'ler** olarak görünüyor. En olası neden?

- A) Application Insights Service Bus'ı desteklemez
- B) Trace context (traceparent) mesajla taşınmıyor; mesajlaşma SDK'sı tracing'i etkin değil
- C) Log Analytics workspace'i yanlış bölgede
- D) Sampling oranı %100

<details><summary>Cevap</summary>

**B.** Servisler arasında kuyruk üzerinden trace'i bağlayan şey **context propagation**'dır: gönderen trace context'i mesaj özelliğine yazar, işleyici çıkarır. Messaging SDK'sının ActivitySource desteği etkin değilse (veya `AddSource` ile dinlenmiyorsa) bu bağ kopar. Bölge (C) ve %100 sampling (D) trace'i bölmez; A yanlıştır.
</details>

### 2. Eşleştirme
Kavramları açıklamalarla eşleştir:

| Kavram | Açıklama |
|---|---|
| 1. Span | a) trace-id'yi servis sınırları arasında taşıma mekanizması |
| 2. Trace | b) tek bir işin süre ve öznitelikleriyle kaydı |
| 3. Context propagation | c) aynı trace-id'yi taşıyan span'ler topluluğu |
| 4. Sampler | d) hangi trace'lerin kaydedileceğine karar veren bileşen |

<details><summary>Cevap</summary>

**1-b, 2-c, 3-a, 4-d.** Span tek bir iştir, trace span'lerin uçtan uca ağacıdır, propagation (HTTP'de `traceparent` header'ı) trace-id'yi sınırlardan geçirir, sampler kayıt kararını verir.
</details>

### 3. Tek seçim
`traceparent: 00-4bf92f3577b34da6a3ce929d0e0e4736-00f067aa0ba902b7-01` header'ında **trace-id** hangisidir ve Application Insights'ta hangi alanla eşleşir?

- A) `00f067aa0ba902b7` — `id`
- B) `4bf92f3577b34da6a3ce929d0e0e4736` — `operation_Id`
- C) `01` — `itemCount`
- D) `00` — `operation_ParentId`

<details><summary>Cevap</summary>

**B.** Biçim: `version-traceid-parentspanid-flags`. İkinci parça (32 hex) trace-id'dir ve `operation_Id`'ye karşılık gelir; üçüncü parça üst span'dir (`operation_ParentId`); `01` flag'i örneklendi anlamına gelir.
</details>

### 4. Kod tamamlama (C#)
ASP.NET Core API'yi Azure Monitor'e bağlayan çağrıyı tamamla:

```csharp
builder.Services.AddOpenTelemetry()
    .______(o => o.ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);
```

- A) `AddApplicationInsightsTelemetry`
- B) `UseAzureMonitor`
- C) `AddAzureMonitorLogging`
- D) `WithMetrics`

<details><summary>Cevap</summary>

**B.** Azure Monitor OpenTelemetry Distro (`Azure.Monitor.OpenTelemetry.AspNetCore`) `OpenTelemetryBuilder` üzerinde `UseAzureMonitor` sunar. `AddApplicationInsightsTelemetry` klasik Application Insights SDK'sına aittir; `WithMetrics` yalnızca metrik boru hattını yapılandırır.
</details>

### 5. Kod tamamlama (C#)
Kendi iş adımını trace'te görmek istiyorsun. Boşlukları doldur:

```csharp
static readonly ActivitySource Source = new("Orders.Api");

builder.Services.AddOpenTelemetry()
    .UseAzureMonitor()
    .WithTracing(t => t.______("Orders.Api"));

using var activity = Source.StartActivity("orders.create");
activity?.SetTag("order.id", id);
```

- A) `AddProcessor`
- B) `AddSource`
- C) `AddInstrumentation`
- D) `SetSampler`

<details><summary>Cevap</summary>

**B.** `ActivitySource`'un span'leri yalnızca `AddSource` ile **adı kayıtlıysa** dinlenir ve export edilir; kayıtsız kaynağın `StartActivity` çağrısı `null` döner. Diğer seçenekler kaynak kaydı değildir.
</details>

### 6. Kod tamamlama (Python)

```python
from azure.monitor.opentelemetry import ______

______(connection_string=os.environ["APPLICATIONINSIGHTS_CONNECTION_STRING"])
```

- A) `enable_telemetry`
- B) `configure_azure_monitor`
- C) `use_azure_monitor`
- D) `start_monitoring`

<details><summary>Cevap</summary>

**B.** Python distro'nun giriş noktası `configure_azure_monitor()`'dır; otomatik enstrümantasyon için framework'lerden **önce** çağrılmalıdır.
</details>

### 7. KQL okuma
Aşağıdaki sorgu ne döndürür?

```kusto
requests
| where timestamp > ago(1h)
| summarize p95 = percentile(duration, 95) by name
| top 3 by p95 desc
```

- A) Son 1 saatte en çok çağrılan 3 endpoint
- B) Son 1 saatte p95 süresi **en yüksek** 3 endpoint (ad ve p95)
- C) Son 1 saatteki rastgele 3 istek
- D) p95'i en düşük 3 endpoint

<details><summary>Cevap</summary>

**B.** `summarize` ada göre p95 hesaplar; `top 3 by p95 desc` en yüksek üçü sıralı verir. Çağrı sayısı hesaplanmamıştır (A). Rastgele satır `take`'tir (C). `desc` en yüksekleri getirir (D).
</details>

### 8. KQL tamamlama
Her 5 dakikalık dilim için istek sayısını almak istiyorsun:

```kusto
requests
| where timestamp > ago(2h)
| summarize requestCount = count() by ______(timestamp, 5m)
| render timechart
```

- A) `round`
- B) `bin`
- C) `bucket_by`
- D) `startofday`

<details><summary>Cevap</summary>

**B.** `bin(timestamp, 5m)` değeri 5 dakikalık kovaya yuvarlar; `render timechart` zaman ekseni ister. `startofday` yalnızca günlük kovadır; `bucket_by` geçerli bir fonksiyon değildir; `round` ise `datetime` değerini 5 dakikalık kovaya yuvarlamak için kullanılmaz.
</details>

### 9. KQL tamamlama
5 dakikalık hata **oranını** (%) hesaplamak için eksik satırı seç:

```kusto
requests
| where timestamp > ago(1h)
| summarize total = count(), failed = countif(success == false) by bin(timestamp, 5m)
| ______ errorRatePct = round(100.0 * failed / total, 2)
| project timestamp, errorRatePct
```

- A) `project`
- B) `extend`
- C) `where`
- D) `distinct`

<details><summary>Cevap</summary>

**B.** `extend` mevcut sütunlara (`total`, `failed`) **hesaplanmış** bir sütun ekler; ardından `project` ile yalnızca gereken sütunlar seçilir. Burada `project` kullanmak `total`/`failed` sütunlarını düşürürdü (ve sonraki `project` anlamsızlaşırdı); `where` satır süzer, `distinct` tekilleştirir.
</details>

### 10. KQL okuma — sonuç
`requests` tablosunda son 30 dakikada yalnızca saat 10:03 ve 10:21 zamanlarında birer istek var. Sorgu:

```kusto
requests
| where timestamp between (datetime(2026-10-01 10:00) .. datetime(2026-10-01 10:30))
| summarize n = count() by bin(timestamp, 10m)
| order by timestamp asc
```

Kaç satır döner?

- A) 1
- B) 2
- C) 3
- D) 4

<details><summary>Cevap</summary>

**B.** `summarize` yalnızca **verisi olan** kovalar için satır üretir: 10:00 kovası (n=1) ve 10:20 kovası (n=1). Boş 10:10 kovası satır olmaz (boşlukları doldurmak için `make-series`). Aralığın sonu dahil olsa da 10:30'da veri yoktur.
</details>

### 11. Tek seçim
Application Insights'ta sampling %20 açıkken `requests | summarize count()` panodaki gerçek istek sayısından çok daha **düşük** çıkıyor. Doğru yaklaşım hangisidir?

- A) `count()` yerine `sum(itemCount)` kullanmak
- B) `summarize` yerine `distinct` kullanmak
- C) `take 10000` eklemek
- D) Workspace retention süresini artırmak

<details><summary>Cevap</summary>

**A.** Örneklenmiş kayıtlar `itemCount` ile kaç gerçek kaydı temsil ettiğini taşır; `sum(itemCount)` tahmini gerçek sayıyı verir. Diğer seçenekler sayıyı düzeltmez.
</details>

### 12. Çoklu seçim (iki doğru cevap)
Sampling hakkında **hangi iki** ifade doğrudur?

- A) Karar trace-id'ye göre verildiğinde bir trace ya tamamen ya da hiç kaydedilmez
- B) Sampling, özel ve standart **metrikleri** de aynı oranda düşürür
- C) Yüksek örneklemde nadir hataların kaybolma olasılığı artar
- D) Her serviste farklı oran kullanmak trace bütünlüğünü artırır

<details><summary>Cevap</summary>

**A ve C.** Trace-id tabanlı sampler tutarlı karar verir; düşük oran nadir olayları kaçırabilir. Metrikler örneklenmez (B) — bu yüzden alert'ler için daha güvenilirdir. Servislerde farklı oran trace'leri **kırar** (D).
</details>

### 13. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) `==` operatörü string karşılaştırmasında büyük/küçük harfe duyarlıdır. | ☐ | ☐ |
| b) `has` operatörü `contains`'e göre genellikle daha hızlıdır (tam terim eşleşmesi). | ☐ | ☐ |
| c) `take 5` sonucu en büyük değere göre sıralanmış 5 satırdır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** `=~` büyük/küçük harf duyarsızdır. `has` terim indeksini kullanır; `contains` alt dize arar. `take` herhangi N satır döner (sırasız); sıralı en büyükler için `top N by`.
</details>

### 14. Tek seçim
Bir uçtan uca isteğin API, worker ve veritabanı adımlarını **tek listede**, zaman sırasıyla görmek istiyorsun. Elinde `operation_Id` var. Hangi sorgu uygundur?

- A) `requests | where operation_Id == "…" | order by timestamp asc`
- B) `union requests, dependencies, exceptions, traces | where operation_Id == "…" | order by timestamp asc`
- C) `dependencies | summarize count() by operation_Id`
- D) `traces | take 100`

<details><summary>Cevap</summary>

**B.** Bir trace; istekleri (`requests`), giden çağrıları (`dependencies`), istisnaları ve logları birlikte içerir. `union` ile hepsini `operation_Id` ile süzmek tüm yolu verir. A yalnızca isteklerdir; C ve D belirli bir akış göstermez.
</details>

### 15. Sıralama
Uyarı tetiklendi: "5 dakikalık hata oranı > %5". Sorun giderme adımlarını doğru sıraya koy:

1. Başarısız bir isteğin `operation_Id`'si ile uçtan uca trace'i incele
2. Hata oranını zaman ve `resultCode` kırılımıyla doğrula
3. En yavaş/başarısız **bağımlılığı** (`target`) bul
4. `exceptions` ve ilgili log kayıtlarında kök nedeni oku
5. Düzeltmeyi uygula ve hata oranının normale döndüğünü doğrula

<details><summary>Cevap</summary>

**2 → 3 → 1 → 4 → 5.** Önce belirtiyi doğrula ve daralt (hangi endpoint/kod), sonra hangi bağımlılığın sorun olduğunu bul, örnek bir trace'i incele, istisna/loglardan kök nedeni oku, düzelt ve doğrula.
</details>

### 16. CLI tamamlama
Hata oranı sorgusunu çalıştıran bir **log search alert** kuralı oluşturmak istiyorsun. Eksik parçayı seç:

```bash
az monitor ______ create -g rg-demo -n alert-error-rate --scopes $WS_ID \
  --condition "count 'Q1' > 0" \
  --condition-query Q1="AppRequests | summarize f = countif(Success == false) | where f > 10" \
  --window-size 5m --evaluation-frequency 5m --severity 2 --action-groups $AG_ID
```

- A) `metrics alert`
- B) `scheduled-query`
- C) `activity-log alert`
- D) `diagnostic-settings`

<details><summary>Cevap</summary>

**B.** Log search alert'leri **scheduled query rule** olarak oluşturulur (`az monitor scheduled-query`; CLI uzantısı gerekir). `metrics alert` metrik eşiği, `activity-log alert` control plane olayları içindir; `diagnostic-settings` log yönlendirmedir.
</details>

### 17. Çoklu seçim (iki doğru cevap)
Bir **action group**'un tetikleyebileceği eylemlerden **hangi ikisi** doğrudur?

- A) Azure Function çağırmak
- B) E-posta / SMS göndermek
- C) KQL sorgusunu otomatik düzeltmek
- D) Log Analytics workspace'ini silmek (yerleşik eylem)

<details><summary>Cevap</summary>

**A ve B.** Action group; e-posta, SMS, push, sesli arama, webhook, Azure Function, Logic App, ITSM ve Automation runbook gibi eylemleri içerir. C ve D yerleşik eylem değildir.
</details>

### 18. Vaka çalışması

**Durum.** *Northwind Logistics*, Container Apps'te `shipments-api`, `label-worker` (Service Bus kuyruğunu işler) ve PostgreSQL kullanıyor. Her ikisi de Azure Monitor OpenTelemetry Distro ile Application Insights'a (workspace-based) bağlı. Son hafta "etiket oluşturma bazen 30 sn sürüyor" şikâyetleri var. Maliyeti düşürmek için API'de `SamplingRatio = 0.1`, worker'da `1.0` ayarlanmış. Application map'te API ile worker arasında bağlantı çizgisi **bazı trace'lerde kopuk**.

**Soru A.** Trace'lerin kopuk görünmesinin olası nedeni ve çözümü nedir?
**Soru B.** "Etiket oluşturma yavaş" şikâyetini araştırmak için hangi sorgu mantığını kurarsın? (yazılı sorgu iskeleti)
**Soru C.** %5'in üzerinde hata olduğunda ekibe nasıl haber verirsin; neden metrik yerine log alert'i seçebilirsin?

<details><summary>Cevap</summary>

**A.** Servislerde **farklı sampling oranları**: API trace'in %90'ını atarken worker %100 kaydeder; atılan trace'lerin worker span'leri tek başına (köksüz) kalır ve map/trace görünümünde kopar. Çözüm: **tüm servislerde aynı, trace-id tabanlı (parent-based) sampling**; worker ebeveynin kararına uysun, oranı iki serviste eşitle. Ayrıca messaging SDK tracing'in açık olduğunu ve context'in (`Diagnostic-Id`/`traceparent`) mesajla taşındığını doğrula.

**B.** Önce yavaş işlemi bul, sonra bağımlılığı daralt, sonra trace'i incele:

```kusto
requests
| where timestamp > ago(7d) and cloud_RoleName == "label-worker"
| summarize p95 = percentile(duration, 95), calls = sum(itemCount) by bin(timestamp, 1h)

dependencies
| where timestamp > ago(7d) and cloud_RoleName == "label-worker"
| summarize p95 = percentile(duration, 95), calls = sum(itemCount) by type, target
| top 5 by p95 desc
```

Sonra en yavaş çağrıdan bir `operation_Id` seçip `union requests, dependencies, exceptions, traces | where operation_Id == "…" | order by timestamp asc` ile uçtan uca incele. `sum(itemCount)` örneklemeyi hesaba katar.

**C.** Bir **log search alert** (scheduled query rule): `summarize total = sum(itemCount), failed = sumif(itemCount, success == false)` ile oran hesaplayıp `total > N and oran > 5` koşuluyla, 5 dakikalık pencere ve değerlendirme aralığıyla; bir **action group** (e-posta + webhook/Teams/Function) bağla. Oran, birden çok sinyalden hesaplanan **KQL koşulu** gerektirdiği için log alert uygundur; yalnızca basit bir eşik (ör. kuyruk uzunluğu) için **metric alert** daha hızlı ve ucuzdur. Log alert'te ingestion gecikmesi ve değerlendirme ücreti vardır.
</details>

---

**Puanlama:** 16-18 → hazırsın · 13-15 → README 3.3 (KQL operatörleri) ve 3.5 (sampling) bölümlerini tekrar et · < 13 → konuyu baştan çalış ve lab'ı (3.6) uygula.
