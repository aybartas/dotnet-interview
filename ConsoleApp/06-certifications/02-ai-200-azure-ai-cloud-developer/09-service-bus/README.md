# C2.9 Azure Service Bus: Kuyruk, Topic ve Dead-letter

> 📁 `ConsoleApp/06-certifications/02-ai-200-azure-ai-cloud-developer/09-service-bus/` · Sınav alanı: **Connect to and consume Azure services (%20-25)** · Önkoşul: C2.8 (Functions), C1.8 (Storage Queue ile fark) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of October 7, 2026):** *Connect to and consume Azure services* — Azure Service Bus ile **arka plan işlemlerini kuyrukla ve işle**: mesajlar, **topic ve subscription**'lar, **dead-letter queue** yönetimi. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)'ı kontrol et.

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

**Azure Service Bus**, kurumsal sınıf, yönetilen bir **mesaj broker**'ıdır (AMQP 1.0): gönderen ile alıcıyı zaman ve hız açısından birbirinden ayırır, mesajı kalıcı saklar ve **teslim garantisi** (at-least-once) verir. İki temel desen:

| Desen | Varlık | Yapı |
|---|---|---|
| **Point-to-point** (iş kuyruğu) | **Queue** | N gönderen → 1 kuyruk → **rakip tüketiciler** (her mesajı tek tüketici işler) |
| **Publish/subscribe** | **Topic + Subscription** | N gönderen → 1 topic → her **subscription** mesajın kendi **kopyasını** alır (filtreyle) |

```text
Producer ─▶ [queue: order-processing] ─▶ Consumer A / B (competing)

Producer ─▶ [topic: orders] ─┬─ sub: high-value  (amount > 1000) ─▶ Fraud service
                             └─ sub: eu-region   (region = 'eu') ─▶ EU fulfilment
                                      ▲ her subscription: kendi DLQ'su
```

Bu konu: namespace/queue/topic/subscription ve filtreler, mesaj özellikleri, **PeekLock vs ReceiveAndDelete**, lock süresi/yenileme, **`ServiceBusProcessor`**, **dead-letter queue** ⭐, session'lar, duplicate detection, scheduled message, tier'lar ve managed identity. Functions ile tüketim → [C2.8](../08-azure-functions/README.md); olay yönlendirme farkı → [C2.10](../10-event-grid/README.md); mimari derinlik → M12.3.

---

## 2. Neden?

- **Sınav:** "Arka plan işlemlerini kuyrukla" hedefi doğrudan Service Bus'tır; en sık sorular: *queue mu topic mi?*, *PeekLock mı?*, *mesaj neden DLQ'ya düştü?*, *hangi tier?*, *sıra için ne?*.
- **Gerçek işte:** Anlık yük dalgalanmasını tamponlar (load leveling), servisleri gevşek bağlar, arızalı tüketici mesajı kaybetmez, **zehirli mesaj** (poison message) tüm hattı kilitlemek yerine DLQ'ya ayrılır.
- **Karşılaştırma:** Storage Queue basittir (≤ 64 KB, topic/DLQ/session yok); Event Grid **olay** dağıtır (→ [C2.10](../10-event-grid/README.md)); Service Bus **iş komutu/mesajı** içindir.

---

## 3. Nasıl?

### 3.1 Namespace, queue, topic, subscription

- **Namespace:** kapsayıcı + DNS adı (`mysb.servicebus.windows.net`); tier ve ağ/kimlik ayarları burada.
- **Queue:** FIFO'ya yakın (tek tüketici ve sıralama gereksinimi için session), mesajı bir tüketici alır.
- **Topic + subscription:** gönderen yalnızca topic'e yazar; **her subscription bağımsız bir sanal kuyruktur** (kendi lock, teslim sayacı ve **DLQ'su** vardır). Aynı subscription'a bağlı tüketiciler birbiriyle rekabet eder; farklı subscription'lar aynı mesajın ayrı kopyasını görür.
- **Auto-forwarding:** bir queue/subscription'ın mesajlarını otomatik başka bir queue/topic'e iletir (zincirleme/fan-in).

### 3.2 Subscription filtreleri ve kurallar

| Filtre | Ne? | Not |
|---|---|---|
| **SQL filter** | SQL-92 alt kümesi ifadesi: `amount > 1000 AND region = 'eu'` | Application properties ve system properties (`sys.Label`, `sys.To` …) üzerinde |
| **Correlation filter** | Belirli özellikler için **eşitlik** eşleşmesi (`CorrelationId`, `Subject`, `To`, `ContentType`, `SessionId`, özel property'ler…) | SQL'e göre daha **hızlı/ucuz** değerlendirilir |
| **Boolean (True/False)** | Hepsini al / hiçbirini alma | `1=1` ≡ TrueFilter |

> **Tuzak ⭐:** Yeni bir subscription **`$Default` kuralıyla** (TrueFilter) gelir. Kendi filtreni eklerken `$Default`'u **silmezsen** subscription **her mesajı** almaya devam eder.

Filtre **mesajın gövdesine değil**, yalnızca **property**'lerine bakar; bu yüzden filtrelenecek değeri gönderirken `ApplicationProperties`'e koy. Filtre değerlendirmesi hata verirse (ör. tip uyuşmazlığı) mesaj, subscription'da `deadLetteringOnFilterEvaluationExceptions` açıksa **DLQ**'ya düşer (varsayılan açık; doğrula). Kural ayrıca **SQL action** ile property değiştirebilir.

### 3.3 Mesaj özellikleri

| Özellik | Amaç |
|---|---|
| `MessageId` | Uygulama tarafından atanan benzersiz kimlik; **duplicate detection** buna bakar |
| `Subject` (eski `Label`), `ContentType`, `CorrelationId`, `To`, `ReplyTo` | Yönlendirme/üst veri |
| `SessionId` | Aynı session'ın mesajlarını **sıralı** ve **tek tüketicide** işlemek için |
| `TimeToLive` | Mesajın ömrü; dolunca mesaj düşürülür **veya** DLQ'ya gider (3.5) |
| `ScheduledEnqueueTime` | İleri tarihli (scheduled) mesaj |
| `ApplicationProperties` | Özel anahtar-değer (filtre buna bakar) |
| `DeliveryCount`, `SequenceNumber`, `EnqueuedTime`, `LockedUntil` | **Broker** tarafından atanan salt-okunur alanlar |

Mesaj boyutu: Basic/Standard'da en çok **256 KB**, **Premium**'da **100 MB**'a kadar (doğrula). Büyük içeriği blob'a koyup mesajda referansını (**claim-check**) gönder.

### 3.4 Alma modları ⭐: PeekLock vs ReceiveAndDelete

| | **PeekLock** (varsayılan) | **ReceiveAndDelete** |
|---|---|---|
| Mesaj ne zaman silinir? | Tüketici **Complete** dediğinde | **Alınır alınmaz** |
| Tüketici çökerse | Lock süresi dolar, mesaj **tekrar teslim edilir** | Mesaj **kaybolur** |
| Teslim | **At-least-once** | **At-most-once** |
| Ne zaman? | İş kaybı kabul edilemez (sipariş, ödeme) | Kayıp tolere edilir, en yüksek hız (telemetri) |

PeekLock **settlement** işlemleri:

| İşlem | Sonuç |
|---|---|
| **Complete** | Mesaj kalıcı silinir |
| **Abandon** | Lock bırakılır, `DeliveryCount++`, mesaj hemen tekrar alınabilir |
| **Dead-letter** | Mesaj **DLQ**'ya taşınır (sebep + açıklama yazılabilir) |
| **Defer** | Mesaj kuyrukta kalır ama normal akıştan çıkar; yalnızca **SequenceNumber** ile alınır |

- **Lock süresi** (`LockDuration`): varsayılan **1 dk**, en çok **5 dk**. İşlem uzunsa lock **yenilenir**: `ServiceBusProcessor` `MaxAutoLockRenewalDuration` (SDK varsayılanı 5 dk) boyunca otomatik yeniler. Lock sona ererse `Complete` **`MessageLockLost`** ile başarısız olur ve mesaj yeniden teslim edilir.
- **`DeliveryCount`:** her abandon veya lock-expiry'de artar; **`MaxDeliveryCount`** (varsayılan **10**) aşılınca mesaj otomatik **DLQ**'ya gider.
- **`ServiceBusProcessor`:** event-tabanlı tüketici; `ProcessMessageAsync` + `ProcessErrorAsync`, `MaxConcurrentCalls`, `AutoCompleteMessages` (varsayılan **true**: handler exception atmadan dönerse Complete; atarsa Abandon), `PrefetchCount`. Elle kontrol için `AutoCompleteMessages = false`.

### 3.5 Dead-letter queue ⭐

Her queue ve **her subscription**'ın otomatik bir alt kuyruğu vardır: `<queue>/$deadletterqueue`, `<topic>/Subscriptions/<sub>/$deadletterqueue`. DLQ'ya düşme **nedenleri**:

| Neden | `DeadLetterReason` | Ne zaman? |
|---|---|---|
| **MaxDeliveryCount aşıldı** | `MaxDeliveryCountExceeded` | Mesaj ardışık abandon/lock-expiry ile eşik kadar teslim edildi (varsayılan 10); **kapatılamaz** |
| **TTL doldu** | `TTLExpiredException` | Varlıkta **dead-lettering on message expiration** açıksa (varsayılan **kapalı**; kapalıysa mesaj sessizce silinir) |
| **Filtre değerlendirme hatası** | filtre istisnası | Subscription'da ilgili ayar açıksa |
| **Uygulama kararı** | **sizin yazdığınız** reason | `DeadLetterMessageAsync(msg, reason, description)` — ör. doğrulama hatası, bozuk payload |
| Diğer sistem sebepleri | `HeaderSizeExceeded`, `MaxTransferHopCountExceeded` (auto-forward zinciri > 4) | Nadir |

Önemli noktalar:
- **Deferred** mesajlar TTL'den dolayı DLQ'ya taşınmaz.
- DLQ'daki mesajlar **kendiliğinden işlenmez** ve **süresi dolmaz**; bir DLQ mesajı tekrar DLQ'ya alınamaz. DLQ boyutu varlık kotasına dahildir — izle ve boşalt.
- DLQ'yu okumak = normal bir **receiver** + `SubQueue.DeadLetter`; mesajlar `DeadLetterReason` ve `DeadLetterErrorDescription` taşır.
- **Yeniden işleme (resubmit):** DLQ'dan oku → düzelt/aynen kopyala → **orijinal varlığa yeni mesaj olarak gönder** → DLQ mesajını Complete et (3.9).

### 3.6 Session'lar, duplicate detection, scheduled message

| Özellik | Amaç | Not |
|---|---|---|
| **Session** | Aynı `SessionId`'li mesajlar **sıralı (FIFO)** ve **aynı alıcıda** işlenir; session state saklanabilir | Varlık oluştururken `RequiresSession = true`; **sıra gereksinimi → session** |
| **Duplicate detection** | Aynı `MessageId` zaman penceresinde (varsayılan 10 dk) ikinci kez gönderilirse yutulur | **Oluştururken** açılır, sonradan değişmez; gönderici tekrarlarına karşıdır (tüketici idempotency'sinin yerini tutmaz) |
| **Scheduled message** | Belirli zamanda görünür: `ScheduleMessageAsync` / `ScheduledEnqueueTime` | Zamanlanan mesaj iptal edilebilir (`CancelScheduledMessageAsync`) |
| **Transaction** | Birden çok işlemi (ör. complete + send) atomik yapar | Standard ve üstü |

### 3.7 Tier'lar

| | **Basic** | **Standard** | **Premium** |
|---|---|---|---|
| Queue | Evet | Evet | Evet |
| **Topic / subscription** | **Hayır** | Evet | Evet |
| Session, duplicate detection, transaction | **Hayır** | Evet | Evet |
| Mesaj boyutu | 256 KB | 256 KB | **100 MB'a kadar** |
| Kaynak modeli | Paylaşılan, işlem başı fiyat | Paylaşılan, işlem başı fiyat | **Ayrılmış messaging unit**, öngörülebilir gecikme |
| VNet/Private Link, geo-replication | Hayır | Hayır/kısıtlı | **Evet** |

*(Tier ayrıntılarını sınavdan önce resmî tablodan doğrula.)* Basit karar: topic gerekiyorsa en az **Standard**; izolasyon/büyük mesaj/ağ gereksinimi → **Premium**.

### 3.8 Kimlik ve bağlantı

Connection string (SAS) yerine **Microsoft Entra ID** önerilir (→ [C2.11](../11-identity-key-vault/README.md); mekanik M12.3):

| Rol | İzin |
|---|---|
| **Azure Service Bus Data Sender** | Mesaj gönder |
| **Azure Service Bus Data Receiver** | Al / settle et |
| **Azure Service Bus Data Owner** | Hepsi + yönetim |

```csharp
// Entra-based client: no secret in code. The identity needs Data Sender/Receiver on the namespace.
await using var client = new ServiceBusClient("mysb.servicebus.windows.net", new DefaultAzureCredential());
```

Functions'ta karşılığı: `ServiceBusConnection__fullyQualifiedNamespace` app setting'i (→ [C2.8](../08-azure-functions/README.md)).

### 3.9 Lab — emulatör ile kuyruk, topic ve DLQ

> Bu lab **Service Bus emulator** (Docker) ile **ücretsiz** çalışır. Gerçek Azure sürümü 3.9.6'da; orada Standard tier kullanılır (küçük bir ücret) ve lab sonunda kaynak grubu silinir; yine de önce **budget alarm** kur (→ C1.11).

#### 3.9.1 Emulator kurulumu

Emulator, Docker'da çalışan resmî bir imajdır (`mcr.microsoft.com/azure-messaging/servicebus-emulator`); entity'ler **yalnızca** bir `Config.json` ile tanımlanır (çalışırken yeni queue/topic oluşturulamaz) ve container yeniden başlayınca veri sıfırlanır. Emulator bir SQL bağımlılığı kullanır (sürüme göre SQL Edge/SQL Server) — güncel `docker compose` dosyası veya `LaunchEmulator` betiği için resmî *azure-service-bus-emulator-installer* deposunu esas al. Özet:

```yaml
# compose.yaml (sketch; use the official installer repo for the current version)
services:
  emulator:
    image: mcr.microsoft.com/azure-messaging/servicebus-emulator:latest
    ports: ["5672:5672"]                      # AMQP
    environment:
      ACCEPT_EULA: "Y"
      SQL_SERVER: sqlserver
      MSSQL_SA_PASSWORD: "<Strong-Passw0rd!>"
    volumes: ["./Config.json:/ServiceBus_Emulator/ConfigFiles/Config.json"]
    depends_on: [sqlserver]
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment: { ACCEPT_EULA: "Y", MSSQL_SA_PASSWORD: "<Strong-Passw0rd!>" }
```

```jsonc
// Config.json — queue + topic with two filtered subscriptions (schema may vary slightly per emulator version)
{
  "UserConfig": {
    "Namespaces": [{
      "Name": "sbemulatorns",
      "Queues": [{
        "Name": "order-processing",
        "Properties": { "MaxDeliveryCount": 3, "LockDuration": "PT30S",
                        "DefaultMessageTimeToLive": "PT1H", "DeadLetteringOnMessageExpiration": true }
      }],
      "Topics": [{
        "Name": "orders",
        "Properties": { "DefaultMessageTimeToLive": "PT1H" },
        "Subscriptions": [
          { "Name": "high-value", "Properties": { "MaxDeliveryCount": 3, "DeadLetteringOnMessageExpiration": true },
            "Rules": [{ "Name": "amount-gt-1000",
                        "Properties": { "FilterType": "Sql", "SqlFilter": { "SqlExpression": "amount > 1000" } } }] },
          { "Name": "eu-region", "Properties": { "MaxDeliveryCount": 3 },
            "Rules": [{ "Name": "region-eu",
                        "Properties": { "FilterType": "Sql", "SqlFilter": { "SqlExpression": "region = 'eu'" } } }] }
        ]
      }]
    }],
    "Logging": { "Type": "File" }
  }
}
```

Bağlantı dizesi (emulator, SAS anahtarı sabit yer tutucudur): 

```text
Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;
```

#### 3.9.2 Gönderme (queue + topic, TTL, scheduled)

```csharp
using System.Text.Json;
using Azure.Messaging.ServiceBus;

const string Conn = "Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;";
await using var client = new ServiceBusClient(Conn);

var queueSender = client.CreateSender("order-processing");
var topicSender = client.CreateSender("orders");

// 1) queue: one good message and one "poison" message
await queueSender.SendMessageAsync(new ServiceBusMessage("""{"id":"o-1","amount":50}""") { MessageId = "o-1", ContentType = "application/json" });
await queueSender.SendMessageAsync(new ServiceBusMessage("""{"id":"o-2","amount":-1}""") { MessageId = "o-2" });   // will fail in the handler

// 2) topic: filters look at application properties, NOT at the body
var big = new ServiceBusMessage("""{"id":"o-3","amount":2500}""") { MessageId = "o-3" };
big.ApplicationProperties["amount"] = 2500;
big.ApplicationProperties["region"] = "eu";
await topicSender.SendMessageAsync(big);                                  // matches high-value AND eu-region

// 3) short TTL: expires -> DLQ (dead-lettering on expiration is ON for this queue)
await queueSender.SendMessageAsync(new ServiceBusMessage("expires soon") { TimeToLive = TimeSpan.FromSeconds(10), MessageId = "o-ttl" });

// 4) scheduled: becomes visible in 30 seconds
await queueSender.ScheduleMessageAsync(new ServiceBusMessage("later") { MessageId = "o-later" }, DateTimeOffset.UtcNow.AddSeconds(30));
```

#### 3.9.3 Bilerek hata veren işleyici → MaxDeliveryCount → DLQ

```csharp
await using var processor = client.CreateProcessor("order-processing", new ServiceBusProcessorOptions
{
    AutoCompleteMessages = false,      // we settle explicitly
    MaxConcurrentCalls = 2,
    MaxAutoLockRenewalDuration = TimeSpan.FromMinutes(2),
});

processor.ProcessMessageAsync += async args =>
{
    var order = args.Message.Body.ToObjectFromJson<Order>(JsonSerializerOptions.Web);
    Console.WriteLine($"{args.Message.MessageId} delivery #{args.Message.DeliveryCount}");

    if (order.Amount < 0)
    {
        // transient/poison simulation: abandon -> redelivered until MaxDeliveryCount (3) is exceeded -> DLQ
        await args.AbandonMessageAsync(args.Message);
        return;
    }
    if (order.Id is null)
    {   // a permanent failure: dead-letter immediately with a reason instead of burning retries
        await args.DeadLetterMessageAsync(args.Message, "BadPayload", "id is missing");
        return;
    }
    await args.CompleteMessageAsync(args.Message);
};
processor.ProcessErrorAsync += args => { Console.WriteLine($"ERR {args.ErrorSource}: {args.Exception.Message}"); return Task.CompletedTask; };

await processor.StartProcessingAsync();
await Task.Delay(TimeSpan.FromSeconds(20));
await processor.StopProcessingAsync();

public record Order(string? Id, decimal Amount);
```

Beklenen çıktı (özet): `o-1 delivery #1` → tamamlanır; `o-2 delivery #1`, `#2`, `#3` → 3. abandon'dan sonra mesaj `MaxDeliveryCountExceeded` ile DLQ'ya düşer; `o-ttl` 10 sn sonra `TTLExpiredException` ile DLQ'da; `o-later` 30. saniyede görünür.

#### 3.9.4 DLQ'yu oku ve yeniden işle

```csharp
await using var dlqReceiver = client.CreateReceiver("order-processing", new ServiceBusReceiverOptions { SubQueue = SubQueue.DeadLetter });
var resubmit = client.CreateSender("order-processing");

await foreach (var dead in dlqReceiver.ReceiveMessagesAsync())          // use ReceiveMessagesAsync(maxMessages, maxWaitTime) in production loops
{
    Console.WriteLine($"{dead.MessageId}: {dead.DeadLetterReason} - {dead.DeadLetterErrorDescription}");

    if (dead.DeadLetterReason == "MaxDeliveryCountExceeded")
    {
        var fixedCopy = new ServiceBusMessage(dead);                     // copies body + properties into a NEW message
        await resubmit.SendMessageAsync(fixedCopy);                      // back to the original entity
    }
    await dlqReceiver.CompleteMessageAsync(dead);                        // remove from the DLQ
}
```

*(Topic subscription DLQ'su için: `client.CreateReceiver("orders", "high-value", new ServiceBusReceiverOptions { SubQueue = SubQueue.DeadLetter })`.)*

#### 3.9.5 Python eşdeğeri (`azure-servicebus`)

```python
from datetime import datetime, timedelta, timezone
from azure.servicebus import ServiceBusClient, ServiceBusMessage, ServiceBusSubQueue

CONN = "Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;"

with ServiceBusClient.from_connection_string(CONN) as client:
    # send: topic message with application properties (filters use these) + TTL + scheduled
    with client.get_topic_sender("orders") as sender:
        sender.send_messages(ServiceBusMessage('{"id":"o-3","amount":2500}', message_id="o-3",
                                               application_properties={"amount": 2500, "region": "eu"}))
    with client.get_queue_sender("order-processing") as sender:
        sender.send_messages(ServiceBusMessage("expires soon", time_to_live=timedelta(seconds=10)))
        sender.schedule_messages(ServiceBusMessage("later"), datetime.now(timezone.utc) + timedelta(seconds=30))

    # receive in PeekLock mode (default); abandon increments delivery_count
    with client.get_queue_receiver("order-processing", max_wait_time=5) as receiver:
        for msg in receiver:
            if b'"amount":-1' in b"".join(msg.body):
                receiver.abandon_message(msg)
            else:
                receiver.complete_message(msg)

    # read the dead-letter sub-queue and resubmit
    with client.get_queue_receiver("order-processing", sub_queue=ServiceBusSubQueue.DEAD_LETTER, max_wait_time=5) as dlq, \
         client.get_queue_sender("order-processing") as sender:
        for dead in dlq:
            print(dead.message_id, dead.dead_letter_reason, dead.dead_letter_error_description)
            sender.send_messages(ServiceBusMessage(b"".join(dead.body), message_id=dead.message_id))
            dlq.complete_message(dead)
```

> Python SDK'nın emulator bağlantı dizesini (`UseDevelopmentEmulator=true`) tanıması yeterince yeni bir `azure-servicebus` sürümü ister; takılırsan sürümü güncelle. Entra ile: `ServiceBusClient(fully_qualified_namespace, credential=DefaultAzureCredential())`.

#### 3.9.6 Gerçek Azure'da aynı yapı (az CLI) ve temizlik

```bash
RG=rg-ai200-c29; NS=sb-ai200-$RANDOM
az group create --name $RG --location westeurope
az servicebus namespace create --resource-group $RG --name $NS --sku Standard            # Basic has no topics

az servicebus queue create --resource-group $RG --namespace-name $NS --name order-processing \
  --max-delivery-count 3 --lock-duration PT30S --default-message-time-to-live PT1H \
  --enable-dead-lettering-on-message-expiration true

az servicebus topic create --resource-group $RG --namespace-name $NS --name orders
az servicebus topic subscription create --resource-group $RG --namespace-name $NS --topic-name orders \
  --name high-value --max-delivery-count 3
# remove the match-all $Default rule, then add the filter
az servicebus topic subscription rule delete --resource-group $RG --namespace-name $NS --topic-name orders \
  --subscription-name high-value --name '$Default'
az servicebus topic subscription rule create --resource-group $RG --namespace-name $NS --topic-name orders \
  --subscription-name high-value --name amount-gt-1000 --filter-sql-expression "amount > 1000"

# Entra auth for your own user (replace with the app's managed identity in real life)
az role assignment create --assignee $(az ad signed-in-user show --query id -o tsv) \
  --role "Azure Service Bus Data Owner" --scope $(az servicebus namespace show -g $RG -n $NS --query id -o tsv)

# cleanup
az group delete --name $RG --yes --no-wait
```

> **Alıştırma.** `amount-gt-1000` kuralını eklediğin hâlde `high-value` subscription'ı 50 TL'lik siparişleri de alıyor. Neden ve nasıl düzelir?
>
> <details><summary>Çözüm</summary>
>
> Subscription `$Default` (TrueFilter) kuralıyla oluşur; yeni kural **eklenir**, varsayılan kural silinmez ve iki kural **OR** ile birleşir → her mesaj geçer. `$Default` kuralını sil (`az servicebus topic subscription rule delete … --name '$Default'`).
> </details>

---

## 4. Gerçek dünya senaryosu

Bir perakende şirketinin sipariş hattı:

| Gereksinim | Karar | Neden |
|---|---|---|
| Kampanyada saniyede binlerce sipariş, ödeme servisi yavaş | **Queue** (`order-processing`) | Yük tamponu; tüketici sayısı ölçeklenir |
| Aynı sipariş hem stok, hem fraud, hem e-posta servisine gitmeli | **Topic `orders`** + 3 subscription | Her servis kendi kopyasını, kendi hızında alır |
| Yalnızca > 1000 TL siparişler fraud servisine | **SQL filter** (`amount > 1000`) + `$Default` silinmiş | Gereksiz iş yok |
| Aynı müşterinin sipariş güncellemeleri sıralı işlenmeli | **Session** (`SessionId = customerId`) | FIFO + tek alıcı |
| Çift tıklama aynı siparişi iki kez göndermesin | **Duplicate detection** (`MessageId = orderId`) | Gönderici tekrarını yutar |
| Bozuk JSON bütün hattı durdurmasın | Uygulama **dead-letter** (`BadPayload`) + DLQ izleme alarmı | Zehirli mesaj izole; operatör inceler, düzeltip yeniden gönderir |
| 24 saat içinde ödenmeyen sipariş iptal | **Scheduled message** (iptal kontrolü) | Cron'suz zamanlayıcı |
| Özel ağ + 5 MB sipariş eki | **Premium** tier (veya claim-check) | VNet/Private Link, büyük mesaj |

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → varlık/mod seçimi, "mesaj neden DLQ'ya düştü", SDK kod tamamlama (`SubQueue.DeadLetter`, `AbandonMessageAsync`), tier kısıtları.

| Senaryodaki ifade | Cevap |
|---|---|
| "Her mesaj **tek** tüketici tarafından işlensin" | **Queue** |
| "Aynı mesajı **birden çok** bağımsız tüketici alsın" | **Topic + subscription'lar** |
| "Mesaj kaybedilemez; çökmeye dayanıklı" | **PeekLock** + Complete |
| "En yüksek hız, ara sıra kayıp tolere edilir" | ReceiveAndDelete |
| "Mesaj sırayla işlenmeli" | **Session** (`SessionId`) |
| "Aynı mesaj iki kez gönderilirse yutulsun" | **Duplicate detection** (`MessageId`) |
| "Belirli saatte görünsün" | **Scheduled message** |
| "Zehirli mesaj sistemi tıkamasın" | **DLQ** + `MaxDeliveryCount` |
| "Mesaj 10 teslimden sonra kayboldu/DLQ'da" | `MaxDeliveryCountExceeded` |
| "TTL dolan mesaj DLQ'da görünmüyor" | Dead-lettering on message expiration **kapalı** |
| "Topic gerekli, en ucuz" | **Standard** (Basic'te topic yok) |
| "Subscription filtre eklendi ama her mesajı alıyor" | `$Default` kuralı silinmedi |
| "Filtre gövdedeki alana bakıyor" | Yanlış: filtre yalnızca **property**'lere bakar |
| "Secret'sız Service Bus erişimi" | Entra ID + *Data Sender/Receiver* rolü |
| "Alma kodunda DLQ" | `SubQueue = SubQueue.DeadLetter` |

**Tuzaklar:**
- **Storage Queue vs Service Bus:** topic, DLQ, session, duplicate detection, transaction, >64 KB → Service Bus.
- `Abandon` mesajı DLQ'ya **atmaz**; sayacı artırır — DLQ'ya düşüş eşik aşılınca olur. Hemen DLQ için **DeadLetter** işlemi.
- Lock süresi işlemden kısa ve yenileme yoksa **`MessageLockLost`** + gereksiz tekrar teslim → DLQ.
- DLQ mesajları **kendiliğinden yeniden işlenmez**; birisi okuyup resubmit etmelidir.
- Duplicate detection **oluştururken** seçilir; sonradan açılamaz.
- SDK'da `ServiceBusClient` ve sender/processor nesneleri **uzun ömürlü** (singleton) tutulur.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Her mesajda yeni `ServiceBusClient` açmak | Singleton client/sender; bağlantı (AMQP) pahalı |
| `AutoCompleteMessages=true` iken handler'da hatayı yutmak | Hata yutulursa mesaj **Complete** olur; ya yeniden fırlat ya da bilinçli dead-letter et |
| Kalıcı hatayı sonsuz abandon ile denemek | Doğrulama/bozuk payload → **hemen dead-letter**; geçici hata → abandon + backoff |
| DLQ'yu izlememek | DLQ derinliği için Azure Monitor alarmı; boşaltma/resubmit süreci |
| İdempotent olmayan tüketici | At-least-once → aynı mesaj ikinci kez gelebilir |
| Filtre için gövdeyi parse etmeyi beklemek | Filtreleyeceğin değeri `ApplicationProperties`'e koy |
| `$Default` kuralını silmemek | Silmeden özel kural = hepsini almak |
| Büyük payload'ı doğrudan gönderip 256 KB'a çarpmak | Claim-check (blob) veya Premium |
| Sıra gerektiren akışta rakip tüketici kullanmak | Session |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| PeekLock → ReceiveAndDelete | Daha az ağ gidiş-dönüşü | Kayıp riski (at-most-once) |
| `PrefetchCount` ↑ | Yüksek verim | Prefetch edilen mesajların lock'u çalışmadan dolabilir → tekrar teslim |
| `MaxConcurrentCalls` ↑ | Paralellik | Aşağı akış baskısı; sıra garantisi azalır (sıra için session) |
| Session | Sıralı işleme | Paralellik session sayısıyla sınırlı; "sıcak" session darboğaz |
| Duplicate detection | Gönderici tekrarını eler | Bellek/penceresi; yalnızca pencere içinde |
| Correlation filter (SQL yerine) | Daha ucuz değerlendirme | Yalnızca eşitlik |
| Standard → Premium | İzolasyon, büyük mesaj, VNet, öngörülebilir gecikme | Messaging unit başına sabit maliyet |
| `MaxDeliveryCount` düşük | Zehirli mesaj hızla izole | Geçici hata yaşayan mesajlar erken DLQ'ya gider (backoff yok) |

---

## 8. Kaynaklar

- [AI-200 study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Service Bus messaging overview](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview) · [Queues, topics, subscriptions](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-queues-topics-subscriptions)
- [Dead-letter queues](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dead-letter-queues) · [Message transfers, locks, settlement](https://learn.microsoft.com/en-us/azure/service-bus-messaging/message-transfers-locks-settlement)
- [Topic filters and actions](https://learn.microsoft.com/en-us/azure/service-bus-messaging/topic-filters) · [Message sessions](https://learn.microsoft.com/en-us/azure/service-bus-messaging/message-sessions) · [Duplicate detection](https://learn.microsoft.com/en-us/azure/service-bus-messaging/duplicate-detection)
- [Service Bus tiers / Premium](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-premium-messaging) · [Quotas](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-quotas)
- [Service Bus emulator](https://learn.microsoft.com/en-us/azure/service-bus-messaging/test-locally-with-service-bus-emulator) · [azure-service-bus-emulator-installer](https://github.com/Azure/azure-service-bus-emulator-installer)
- [Azure.Messaging.ServiceBus (.NET)](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/messaging.servicebus-readme) · [azure-servicebus (Python)](https://learn.microsoft.com/en-us/python/api/overview/azure/servicebus-readme)
