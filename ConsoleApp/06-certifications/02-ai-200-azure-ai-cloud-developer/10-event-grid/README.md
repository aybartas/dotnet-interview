# C2.10 Azure Event Grid: Custom Event, Filtre ve Retry

> 📁 `ConsoleApp/06-certifications/02-ai-200-azure-ai-cloud-developer/10-event-grid/` · Sınav alanı: **Connect to and consume Azure services (%20-25)** · Önkoşul: C2.8 (Functions), C2.9 (Service Bus) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of October 7, 2026):** *Connect to and consume Azure services* — Azure Event Grid ile **olay güdümlü iş akışları**: **filtreler**, **custom event**'ler ve **retry**'lar. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)'ı kontrol et.

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

**Azure Event Grid**, tamamen yönetilen bir **olay yönlendirme (event routing)** servisidir: bir kaynakta *bir şey oldu* bilgisini (olay) alır ve ilgilenen **handler**'lara dağıtır. Olay bir **bildirimdir** ("`Order.Placed` oldu"); bir iş komutu ya da veri taşıma aracı değildir.

```text
 Event source            Topic                 Event subscription (filtre + retry + DLQ)      Event handler
 ───────────────       ──────────           ───────────────────────────────────────────      ─────────────
 Blob Storage    ─▶  [system topic]   ─┐
 Resource Group  ─▶  [system topic]   ─┼─▶ sub A: subject begins with /blobServices/...  ─▶  Azure Function
 Your application ─▶ [custom topic]   ─┤    sub B: data.amount > 1000                    ─▶  Service Bus queue
 SaaS (partner)  ─▶  [partner topic]  ─┘    sub C: type = Order.Cancelled                ─▶  Webhook / Storage queue
```

| Kavram | Anlamı |
|---|---|
| **Event source** | Olayı üreten: Azure servisi (Blob, Resource Group, Key Vault…), kendi uygulaman, partner SaaS |
| **Topic** | Olayların gönderildiği uç nokta. Türler: **system topic** (Azure servisi olayları; kaynak sahibi servistir), **custom topic** (kendi uygulama olayların), **partner topic** (üçüncü taraf SaaS), **domain** (çok kiracılı/çok topic'li yönetim birimi) |
| **Event subscription** | "Bu topic'ten **şu filtreye** uyan olayları **şu handler**'a, **şu retry/dead-letter ayarlarıyla** ilet" |
| **Event handler** | Webhook, **Azure Functions**, **Service Bus** queue/topic, **Storage Queue**, Event Hubs, Hybrid Connections… |
| **Event şeması** | **Event Grid şeması** veya **CloudEvents v1.0** (açık standart) |
| **Namespace topic'leri** | Yeni model: bir **Event Grid namespace** içinde topic'ler; **CloudEvents** şeması, **push** *ve* **pull** teslim (queue subscription), MQTT broker. Azure servis/partner olaylarını **tüketmez** (yalnızca kendi uygulama olayların) — desteklenen handler kümesi klasik topic'lerden farklıdır |

Laboratuvar: custom topic'e **`amount > 1000`** siparişleri filtreleyip Function handler'a yönlendirme, handler'ı bilerek hata verdirip **retry** ve **dead-letter blob**'unu gözlemleme (3.8). Mesaj/iş komutu tarafı → [C2.9](../09-service-bus/README.md); handler olarak Function → [C2.8](../08-azure-functions/README.md); event-based blob trigger (Event Grid kaynaklı) → C2.8.

---

## 2. Neden?

- **Sınav:** Filtre türü seçimi, retry/TTL/dead-letter ayarı ve "Event Grid mi Service Bus mı?" kararı en sık gelen üç soru tipidir.
- **Gerçek işte:** Polling yerine **push** ile tepki: blob yüklendi → thumbnail üret; sipariş verildi → muhasebe/stok/bildirim; kaynak oluşturuldu → etiket denetimi. Yayıncı, kimlerin dinlediğini bilmez (gevşek bağlılık); yeni dinleyici = yeni subscription, yayıncıda değişiklik yok.
- **Maliyet:** Olay başına fiyatlanır, kullanılmadığında maliyet yok; serverless Function ile birleşince "olay olmadığında sıfır" mimarisi.

---

## 3. Nasıl?

### 3.1 Olay şemaları: Event Grid şeması vs CloudEvents

```jsonc
// Event Grid schema
{ "id": "o-1001", "topic": "/subscriptions/.../topics/orders", "subject": "orders/o-1001",
  "eventType": "Contoso.Order.Placed", "eventTime": "2026-10-26T09:00:00Z",
  "data": { "orderId": "o-1001", "amount": 1500 }, "dataVersion": "1.0", "metadataVersion": "1" }

// CloudEvents v1.0 (JSON)
{ "specversion": "1.0", "id": "o-1001", "source": "/orders/api", "type": "Contoso.Order.Placed",
  "subject": "orders/o-1001", "time": "2026-10-26T09:00:00Z", "datacontenttype": "application/json",
  "data": { "orderId": "o-1001", "amount": 1500 } }
```

| | Event Grid şeması | CloudEvents v1.0 |
|---|---|---|
| Tür alanı | `eventType` | `type` |
| Kaynak alanı | `topic` (servis atar) | `source` (yayıncı verir, **zorunlu**) |
| Standart mı? | Azure'a özgü | **CNCF standardı**, taşınabilir |
| Namespace topic'leri | Desteklenmez | **Tek** desteklenen şema |

- **Input schema** topic oluştururken seçilir (`--input-schema`); sonradan değiştirilemez. **Delivery schema** her subscription'da ayrı seçilir (`--event-delivery-schema`). **CloudEvents girişli topic'ten Event Grid şemasıyla teslim yapılamaz**.
- Bir olayın üst sınırı **1 MB**'dır (yayın isteği toplamı; 64 KB'lık dilimlerle faturalanır — doğrula). Büyük içeriği blob'a koy, olayda **referans** taşı.

### 3.2 Custom event yayınlama ⭐

`EventGridPublisherClient` (paket `Azure.Messaging.EventGrid`) custom topic endpoint'ine (`https://<topic>.<region>-1.eventgrid.azure.net/api/events`) yazar. Kimlik doğrulama yolları:

| Yol | Kullanım | Not |
|---|---|---|
| **Entra ID** (`DefaultAzureCredential`) | **Önerilen** | Kimliğe **EventGrid Data Sender** rolü |
| **Access key** (`AzureKeyCredential`) | Hızlı deneme | `az eventgrid topic key list`; secret yönetimi gerekir |
| **SAS token** | Kısa ömürlü, yetkisi daraltılmış paylaşım | `aeg-sas-token` header'ı |

```csharp
using Azure.Identity;
using Azure.Messaging;
using Azure.Messaging.EventGrid;

var client = new EventGridPublisherClient(new Uri(topicEndpoint), new DefaultAzureCredential());

// CloudEvents input schema topic
var evt = new CloudEvent(source: "/orders/api", type: "Contoso.Order.Placed",
                         jsonSerializableData: new { orderId = "o-1001", amount = 1500m, region = "eu" })
{
    Subject = "orders/o-1001",
};
await client.SendEventAsync(evt);            // SendEventsAsync(IEnumerable<CloudEvent>) to batch
```

*(Event Grid şemalı topic için `EventGridEvent("orders/o-1001", "Contoso.Order.Placed", "1.0", data)` kullanılır; topic'in şeması ile gönderilen olay biçimi **eşleşmelidir**.)*

### 3.3 Filtreleme ⭐

Filtreler **event subscription**'a yazılır; uymayan olay o subscription'a **hiç teslim edilmez** (diğer subscription'ları etkilemez).

| Filtre | Ne yapar? | Ne zaman? |
|---|---|---|
| **Event type** (`--included-event-types`) | Yalnızca listelenen `eventType`/`type` değerleri | Varsayılan hepsi (`All`) |
| **Subject begins with / ends with** | `subject` alanında **önek/sonek** eşleşmesi (varsayılan **büyük/küçük harf duyarsız**; `--subject-case-sensitive`) | Yol benzeri hiyerarşi: `/blobServices/default/containers/images/`, `.jpg` uzantısı |
| **Advanced filter** | `data.*` dahil herhangi bir alanda **operatör** tabanlı koşul | Payload'a göre yönlendirme (`data.amount`, `data.region`) |

**Advanced filter operatörleri:** sayı: `NumberIn`, `NumberNotIn`, `NumberLessThan`, `NumberGreaterThan`, `…OrEquals`, `NumberInRange`, `NumberNotInRange` · metin: `StringIn`, `StringNotIn`, `StringContains`, `StringBeginsWith`, `StringEndsWith` (+ `Not…`) · `BoolEquals` · `IsNullOrUndefined`, `IsNotNull`.

| Kural | Ayrıntı |
|---|---|
| **Anahtar (key)** | Event Grid şemasında `subject`, `eventType`, `id`, `data.<yol>`; CloudEvents'te `subject`, `type`, `source`, `id`, `data.<yol>` |
| **Birleştirme** | Birden çok advanced filter **AND**; aynı filtredeki değer listesi (`StringIn a,b`) **OR** |
| **Kota** | Subscription başına sınırlı sayıda advanced filter/değer vardır (ör. 25 filtre; doğrula) |
| **Tuzak** | Filtre **gövdenin tamamına** değil `data` altındaki alanlara/envelope'a bakar; string sayıyı `NumberGreaterThan` ile karşılaştırmaz — tip uyumu gerekir |

### 3.4 Teslim ve dayanıklılık ⭐

Event Grid **at-least-once** teslim eder: aynı olay **birden fazla** gelebilir ve **sıra garanti edilmez** → handler **idempotent** olmalı (olay `id` ile tekilleştir).

| Konu | Davranış |
|---|---|
| **Başarı** | Handler **2xx** döner (200/201/202/203/204) → olay teslim edildi sayılır |
| **Retry** | Başarısız (zaman aşımı, 5xx, 429, 408…) teslimler **exponential backoff** ile tekrar denenir; aralıklar kabaca 10 sn, 30 sn, 1 dk, 5 dk, 10 dk, 30 dk, 1 saat, sonra saatlik — **yapılandırılamaz**, rastgele sapma (jitter) eklenir |
| **Retry policy (ayarlanabilir)** | **Max delivery attempts** (klasik topic varsayılan **30**, en çok 30) ve **Event time-to-live** (varsayılan **1440 dk = 24 sa**, en çok 1440). İkisi birlikte verilirse **önce dolan** işi bitirir |
| **Kalıcı hatalar** | **400 Bad Request**, **413 Payload Too Large** gibi "asla başarılı olmayacak" yanıtlarda olay **retry edilmeden** doğrudan dead-letter'a gönderilir (diğer kalıcı kodlar handler türüne göre değişir; doğrula) |
| **Dead-letter** | **Varsayılan kapalı**. Subscription oluştururken bir **Storage hesabı/blob container'ı** verilirse, teslim edilemeyen/süresi dolan olaylar oraya **blob** olarak yazılır (son denemeden **~5 dk** sonra). Dead-letter ayarlı değilse olay **sessizce düşer** |
| **Batch teslim** | Webhook/Function için `maxEventsPerBatch` ve `preferredBatchSizeInKilobytes` ile toplu teslim |
| **Webhook doğrulama** | Webhook endpoint'i subscription oluşturulurken **handshake** ile sahipliğini kanıtlar: Event Grid şemasında `SubscriptionValidationEvent` içindeki `validationCode`'u `{"validationResponse": "<code>"}` olarak döner (veya `validationUrl`'e GET); CloudEvents v1.0'da **OPTIONS** isteğine `WebHook-Allowed-Origin` yanıtı. **Azure Functions, Service Bus, Storage Queue** gibi Azure hedeflerinde handshake **otomatik**dir |

> Namespace topic'lerinde varsayılanlar farklıdır (ör. max delivery count **10**, olay saklama süresi en çok **7 gün**); klasik topic değerlerini oraya taşıma.

### 3.5 Event Grid vs Service Bus vs Event Hubs ⭐

| | **Event Grid** | **Service Bus** | **Event Hubs** |
|---|---|---|---|
| Mesajın doğası | **Olay** (oldu bilgisi) | **Mesaj/iş komutu** (yapılması gereken iş) | **Telemetri akışı** |
| Teslim | **Push** (klasik: handler'a it; namespace: push + pull) | **Pull** (alıcı çeker, lock/settle) | Pull (offset ile okuma) |
| Kalıcılık / işleme garantisi | Retry + (isteğe bağlı) dead-letter; **kuyruk değildir** | **Kuyruk**, PeekLock, DLQ, TTL | Saklama süresi boyunca log |
| Sıralama, session, transaction | Yok | **Var** | Partition içinde sıra |
| Kimler tüketir? | Çok sayıda bağımsız abone (fan-out) | Queue: biri; topic: abone başına kopya | Consumer group'lar |
| Tipik örnek | "Blob yüklendi", "Order.Placed" bildirimi | "Bu siparişi işle", ödeme komutu | Log/IoT telemetri, saniyede milyonlarca olay |

**Birlikte kullanım:** Event Grid → **Service Bus queue** handler'ı ("olay geldi → işi dayanıklı kuyruğa koy → tüketici kendi hızında işlesin"). Bu, "Event Grid'i kuyruk gibi kullanma" hatasının doğru çözümüdür.

### 3.6 Handler olarak Azure Function

```csharp
// Isolated worker, Microsoft.Azure.Functions.Worker.Extensions.EventGrid
public class OrderEventHandlers(ILogger<OrderEventHandlers> logger)
{
    public record OrderPlaced(string OrderId, decimal Amount, bool SimulateFailure = false);

    [Function("HighValueOrderHandler")]
    public void Run([EventGridTrigger] CloudEvent cloudEvent)              // use EventGridEvent for the Event Grid schema
    {
        var order = cloudEvent.Data!.ToObjectFromJson<OrderPlaced>(JsonSerializerOptions.Web);
        logger.LogInformation("Event {Id} type={Type} order={OrderId} amount={Amount}",
            cloudEvent.Id, cloudEvent.Type, order!.OrderId, order.Amount);

        if (order.SimulateFailure)
            throw new InvalidOperationException("Simulated handler failure");   // => HTTP 500 => Event Grid retries
    }
}
```

Function bir **trigger**'dır (→ C2.8); Event Grid subscription'ı Function'a `azurefunction` endpoint tipiyle bağlanır, doğrulama otomatiktir. Function içindeki hata Event Grid'e 5xx olarak döner ve **Event Grid retry**'ı devreye girer. (Service Bus handler'ında ise retry **Service Bus**'a devredilir, Event Grid için teslim başarılıdır.)

### 3.7 System topic vs custom topic

| | **System topic** | **Custom topic** |
|---|---|---|
| Olayı kim üretir? | Azure servisi (Storage, Resource Group, Key Vault, App Configuration…) | **Senin uygulaman** (`EventGridPublisherClient` ile) |
| Yayın yapılır mı? | Hayır, servis kendisi yayınlar | Evet; endpoint + kimlik doğrulama |
| Olay türleri | Servise göre sabit (`Microsoft.Storage.BlobCreated`) | Sana ait (`Contoso.Order.Placed`) |
| Örnek | "Blob oluştu → Function" | "Sipariş verildi → muhasebe" |

### 3.8 Lab — custom event, advanced filter, retry ve dead-letter

> Event Grid için resmî **local emulator yoktur**; lab Azure'da çalışır (olay başına mikro fiyat, ilk aylık olay grubu ücretsiz — güncel fiyat için doğrula). Önce **budget alarm** kur (→ C1.11). Function app için [C2.8](../08-azure-functions/README.md) laboratuvarındaki aynı komutları kullan; aşağıda kısaltılmıştır. Yerelde handler testi: `func start` ardından
> `curl -X POST "http://localhost:7071/runtime/webhooks/EventGrid?functionName=HighValueOrderHandler" -H "aeg-event-type: Notification" -H "Content-Type: application/cloudevents+json" -d @event.json`.

```bash
RG=rg-ai200-c210; LOC=westeurope
TOPIC=evgt-orders-$RANDOM; ST=stc210$RANDOM; APP=func-ai200-c210-$RANDOM
az group create --name $RG --location $LOC
az storage account create --name $ST --resource-group $RG --location $LOC --sku Standard_LRS
az storage container create --account-name $ST --name deadletter --auth-mode login

# 1) custom topic with the CloudEvents input schema
az eventgrid topic create --name $TOPIC --resource-group $RG --location $LOC --input-schema cloudeventschemav1_0
TOPIC_ID=$(az eventgrid topic show --name $TOPIC --resource-group $RG --query id -o tsv)
ENDPOINT=$(az eventgrid topic show --name $TOPIC --resource-group $RG --query endpoint -o tsv)

# 2) function app + handler (create as in C2.8: az functionapp create ... --flexconsumption-location; publish HighValueOrderHandler)
FUNC_ID=$(az functionapp function show --name $APP --resource-group $RG --function-name HighValueOrderHandler --query id -o tsv)

# 3) subscription: only amount > 1000, 3 attempts, 5 minute TTL, dead-letter blob
ST_ID=$(az storage account show --name $ST --resource-group $RG --query id -o tsv)
az eventgrid event-subscription create --name high-value-orders --source-resource-id $TOPIC_ID \
  --endpoint-type azurefunction --endpoint $FUNC_ID \
  --event-delivery-schema cloudeventschemav1_0 \
  --included-event-types Contoso.Order.Placed \
  --advanced-filter data.amount NumberGreaterThan 1000 \
  --max-delivery-attempts 3 --event-ttl 5 \
  --deadletter-endpoint "$ST_ID/blobServices/default/containers/deadletter"

# 4) let your user publish with Entra (no key)
az role assignment create --assignee $(az ad signed-in-user show --query id -o tsv) \
  --role "EventGrid Data Sender" --scope $TOPIC_ID
echo $ENDPOINT
```

**Yayıncı (C#)** — üç olay: biri filtreye takılır (`amount = 300`), biri başarıyla işlenir (`1500`), biri bilerek hata verir (`2500`, `SimulateFailure = true`):

```csharp
var client = new EventGridPublisherClient(new Uri(endpoint), new DefaultAzureCredential());
await client.SendEventsAsync(new[]
{
    new CloudEvent("/orders/api", "Contoso.Order.Placed", new { orderId = "o-1", amount = 300m }) { Subject = "orders/o-1" },
    new CloudEvent("/orders/api", "Contoso.Order.Placed", new { orderId = "o-2", amount = 1500m }) { Subject = "orders/o-2" },
    new CloudEvent("/orders/api", "Contoso.Order.Placed", new { orderId = "o-3", amount = 2500m, simulateFailure = true }) { Subject = "orders/o-3" },
});
```

**Gözlem:**
1. `o-1` handler'a **hiç ulaşmaz** (filtre: `300 > 1000` değil).
2. `o-2` bir kez teslim edilir ve loglanır.
3. `o-3` handler'da exception → **HTTP 5xx** → Event Grid aynı olayı birkaç saniye arayla **en çok 3 kez** dener (Application Insights/log stream'de aynı `cloudEvent.Id`).
4. Denemeler bitince (≈ son denemeden 5 dk sonra) olay `deadletter` container'ında bir **blob** olarak belirir:

```bash
az storage blob list --account-name $ST --container-name deadletter --auth-mode login \
  --query "[].name" -o tsv
# each blob holds the event JSON plus deadLetterReason (e.g. MaxDeliveryAttemptsExceeded) and lastDeliveryOutcome
```

**Python eşdeğeri (`azure-eventgrid`)** — yayıncı ve handler:

```python
from azure.core.messaging import CloudEvent
from azure.eventgrid import EventGridPublisherClient
from azure.identity import DefaultAzureCredential

client = EventGridPublisherClient(endpoint, DefaultAzureCredential())
client.send([
    CloudEvent(source="/orders/api", type="Contoso.Order.Placed", subject="orders/o-2",
               data={"orderId": "o-2", "amount": 1500}),
])
```

```python
# function_app.py (Python v2 model) - handler
import azure.functions as func, logging
app = func.FunctionApp()

@app.event_grid_trigger(arg_name="event")
def high_value_order_handler(event: func.EventGridEvent):
    data = event.get_json()
    logging.info("event %s amount=%s", event.id, data.get("amount"))
    if data.get("simulateFailure"):
        raise RuntimeError("Simulated handler failure")           # => 5xx => retry
```

> Python Event Grid trigger'ın CloudEvents teslim şemasındaki davranışı extension sürümüne bağlıdır; sorun yaşarsan subscription'ı Event Grid şemasıyla (topic'i de o şemayla) dene.

**Temizlik:**

```bash
az group delete --name $RG --yes --no-wait
```

> **Alıştırma.** Aynı topic'e `amount = 1500`, `region = "us"` olayı gönderip yalnızca **EU** siparişlerinin handler'a ulaşmasını sağla.
>
> <details><summary>Çözüm</summary>
>
> İkinci bir advanced filter ekle: `--advanced-filter data.region StringIn eu` (filtreler **AND**'lenir; `StringIn eu eu-west` değer listesi OR'dur). Subscription'ı `az eventgrid event-subscription update` ile güncelleyebilir veya yeniden oluşturabilirsin.
> </details>

---

## 4. Gerçek dünya senaryosu

Bir e-ticaret platformu:

| Gereksinim | Karar | Neden |
|---|---|---|
| Ürün görseli blob'a yüklenince thumbnail üretilsin | **System topic** (Storage) + `subject-ends-with .jpg` + Function | Polling yok; yalnızca jpg tetikler |
| `Order.Placed` olayını muhasebe, stok ve pazarlama bağımsız dinlesin | **Custom topic** + her biri için ayrı subscription | Yayıncı dinleyicileri bilmez; yeni servis = yeni subscription |
| Yalnızca 1000 TL üstü siparişler fraud servisine | **Advanced filter** `data.amount NumberGreaterThan 1000` | Gereksiz çağrı ve maliyet yok |
| Muhasebe sistemi geceleri kapalı, olaylar kaybolmamalı | Subscription **TTL 24 sa** + **dead-letter blob**; veya handler = **Service Bus queue** | Retry penceresi + kayıp olay incelemesi; kuyruk dayanıklılığı |
| Ödeme komutu **tam bir kez**, sıralı işlenmeli | **Service Bus** (session + DLQ), Event Grid değil | Event Grid olay bildirir; iş garantisi vermez |
| Aynı olay iki kez gelebilir | Handler'da `cloudEvent.Id` ile **idempotency** | At-least-once |

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → filtre türü, retry/TTL/dead-letter ayarı, topic türü, Event Grid vs Service Bus; CLI/SDK tamamlama (`--advanced-filter`, `EventGridPublisherClient`).

| Senaryodaki ifade | Cevap |
|---|---|
| "Blob/Resource Group gibi **Azure servisi** olayına tepki" | **System topic** |
| "Kendi uygulamamın olaylarını yayınla" | **Custom topic** + `EventGridPublisherClient` |
| "SaaS sağlayıcının olayları" | **Partner topic** |
| "Yalnızca `.jpg` olsun" / "şu container'daki" | **Subject ends with / begins with** |
| "Yalnızca `data.amount` > 1000" | **Advanced filter** `NumberGreaterThan` |
| "Yalnızca belirli olay türleri" | **Event type** filtresi |
| "Teslim edilemeyen olayları sakla" | **Dead-letter** (storage blob container) — **elle açılmalı** |
| "Teslim deneme sayısı / süresi" | **Max delivery attempts** (≤ 30) ve **event TTL** (≤ 1440 dk) |
| "İş kuyruğu, sıralama, session, DLQ, işleme garantisi" | **Service Bus** |
| "Binlerce olay/sn telemetri akışı" | **Event Hubs** |
| "Webhook subscription oluşturulamıyor" | **Validation handshake** yanıtı eksik |
| "Event Grid → dayanıklı işleme" | Handler olarak **Service Bus queue** |
| "Olay çift geldi" | At-least-once → **idempotent** handler |
| "Secret'sız yayın" | Entra + **EventGrid Data Sender** rolü |

**Tuzaklar:**
- **Event Grid kuyruk değildir:** sıra, session, "tüketici hazır olunca al" yok; retry süresi dolunca olay dead-letter'a (varsa) gider veya **kaybolur**.
- **Dead-letter varsayılan kapalıdır;** kapalıysa 400/413 ve süresi dolan olaylar **sessizce düşer**.
- **Retry ve TTL ikisi birlikte:** 30 deneme istesen de TTL erken dolarsa deneme sayısı fiilen daha azdır.
- **Filtre türü:** yol/uzantı → subject filtresi; payload alanı → **advanced** filter; olay türü → event type.
- **Şema eşleşmesi:** CloudEvents girişli topic'e Event Grid şemalı olay yollanmaz; CloudEvents girişli topic'ten Event Grid şemasıyla teslim yapılamaz.
- **Retry schedule yapılandırılamaz**; yapılandırılan yalnızca *max attempts* ve *TTL*'dir.
- **System topic**'e kendin olay **yayınlayamazsın**.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Event Grid'i iş kuyruğu gibi kullanmak | İş komutları → Service Bus; Event Grid → bildirim (veya handler = Service Bus) |
| Handler'ı idempotent yazmamak | At-least-once + retry → çift işleme; `id` ile tekilleştir |
| Dead-letter'ı yapılandırmamak | Subscription'da storage container ver; blob'ları izle |
| Webhook handler'da handshake'i atlamak | `validationCode` yanıtı / CloudEvents `OPTIONS` yanıtı |
| Handler'ın uzun sürmesi (zaman aşımı → retry fırtınası) | Hızlı `2xx` dön, işi kuyruğa at |
| Filtreyi gövdenin dışındaki alana yazmak (`amount`) | Anahtar `data.amount` |
| Büyük payload'ı olaya koymak | Olayda referans, veri blob'da |
| Access key'i koda gömmek | `DefaultAzureCredential` + **EventGrid Data Sender** |
| Klasik topic varsayılanlarını (30/1440) namespace topic'e taşımak | Namespace topic'leri farklı varsayılanlara sahiptir |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Sunucu tarafı filtreleme | Gereksiz handler çağrısı/maliyeti yok | Filtre kotası; karmaşık mantık handler'da kalır |
| Retry/TTL uzun | Geçici kesintiyi tolere eder | Eski olay geç teslim edilebilir; sıra bozulur |
| Retry/TTL kısa + dead-letter | Hızlı hata görünürlüğü | Daha çok elle müdahale |
| Batch teslim | Daha az HTTP çağrısı | Handler toplu işleme + kısmi hata yönetimi gerektirir |
| Handler = Function | Sıfır altyapı, otomatik doğrulama | Function hata/zaman aşımı → retry maliyeti |
| Handler = Service Bus queue | Dayanıklılık, sıra/session, DLQ | Ek servis maliyeti ve gecikmesi |
| Namespace topic (pull) | Tüketici hızını kendisi belirler | Farklı model/varsayılanlar; klasik handler seti yok |

---

## 8. Kaynaklar

- [AI-200 study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Event Grid overview](https://learn.microsoft.com/en-us/azure/event-grid/overview) · [Concepts](https://learn.microsoft.com/en-us/azure/event-grid/concepts) · [Event handlers](https://learn.microsoft.com/en-us/azure/event-grid/event-handlers)
- [Delivery and retry](https://learn.microsoft.com/en-us/azure/event-grid/delivery-and-retry) · [Set dead-letter location and retry policy](https://learn.microsoft.com/en-us/azure/event-grid/manage-event-delivery)
- [Event filtering for subscriptions](https://learn.microsoft.com/en-us/azure/event-grid/event-filtering) · [CloudEvents schema](https://learn.microsoft.com/en-us/azure/event-grid/cloud-event-schema)
- [Webhook event delivery / validation](https://learn.microsoft.com/en-us/azure/event-grid/webhook-event-delivery) · [Namespace topics: pull and push delivery](https://learn.microsoft.com/en-us/azure/event-grid/pull-delivery-overview)
- [Choose between Event Grid, Event Hubs and Service Bus](https://learn.microsoft.com/en-us/azure/event-grid/compare-messaging-services)
- [Azure.Messaging.EventGrid (.NET)](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/messaging.eventgrid-readme) · [azure-eventgrid (Python)](https://learn.microsoft.com/en-us/python/api/overview/azure/eventgrid-readme) · [Event Grid trigger for Functions](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-event-grid-trigger)
