# C2.10 Azure Event Grid: Custom Event, Filtre ve Retry — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Connect to and consume Azure services* (%20-25) · 16 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek/çoklu seçim, evet/hayır, sıralama, senaryo, kod ve CLI tamamlama, vaka); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 13/16** (~%80).

---

### 1. Tek seçim
Bir blob storage hesabına dosya yüklendiğinde bir Azure Function'ın tetiklenmesini istiyorsun. Olayı hangi topic türü sağlar?

- A) Custom topic
- B) System topic
- C) Partner topic
- D) Kendi yazdığın bir Service Bus topic'i

<details><summary>Cevap</summary>

**B.** Azure servislerinin ürettiği olaylar (ör. `Microsoft.Storage.BlobCreated`) **system topic** üzerinden gelir; kaynak sahibi servis olayı kendisi yayınlar. Custom topic kendi uygulama olayların içindir; partner topic üçüncü taraf SaaS olayları içindir.
</details>

### 2. Tek seçim
Uygulaman `Contoso.Order.Placed` olaylarını yayınlayacak. Hangi seçim doğrudur?

- A) System topic oluşturup `EventGridPublisherClient` ile yazmak
- B) Custom topic oluşturup `EventGridPublisherClient` ile yazmak
- C) Event Hubs kullanmak, çünkü Event Grid yalnızca Azure olaylarını destekler
- D) Partner topic oluşturmak

<details><summary>Cevap</summary>

**B.** Kendi uygulama olayların için custom topic kullanılır; yayın `EventGridPublisherClient` ile topic endpoint'ine yapılır. System topic'e kendin olay yazamazsın; Event Grid custom olayları destekler.
</details>

### 3. Kod tamamlama (C#)
Aşağıdaki kod custom topic'e **secret'sız** (Entra ID) yayın yapmalıdır. Boşluğu doldurun.

```csharp
var client = new EventGridPublisherClient(new Uri(topicEndpoint), new ______());
```

- A) `AzureKeyCredential(key)`
- B) `DefaultAzureCredential`
- C) `StorageSharedKeyCredential`
- D) `ServiceBusClient`

<details><summary>Cevap</summary>

**B.** `DefaultAzureCredential` (veya başka bir `TokenCredential`) Entra kimliğiyle yayın yapar; kimliğin topic üzerinde **EventGrid Data Sender** rolü olmalıdır. `AzureKeyCredential` access key içindir (secret yönetimi gerektirir).
</details>

### 4. Çoklu seçim (iki doğru cevap)
Bir subscription'ın yalnızca `Contoso.Order.Placed` türünde **ve** `data.amount` değeri 1000'den büyük olayları alması için **hangi ikisi** yapılandırılır?

- A) `--included-event-types Contoso.Order.Placed`
- B) `--advanced-filter data.amount NumberGreaterThan 1000`
- C) `--subject-begins-with 1000`
- D) `--max-delivery-attempts 1000`

<details><summary>Cevap</summary>

**A ve B.** Olay türü filtresi ve payload alanına advanced filter birlikte (AND) uygulanır. Subject filtresi `subject` alanına bakar, `data` alanına değil; `max-delivery-attempts` retry ayarıdır.
</details>

### 5. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Birden çok advanced filter bir subscription'da birlikte AND ile değerlendirilir. | ☐ | ☐ |
| b) `StringIn a,b` filtresindeki değerler OR ile değerlendirilir. | ☐ | ☐ |
| c) Filtreye uymayan olay, aynı topic'teki diğer subscription'lara da iletilmez. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Filtre **subscription'a özgüdür**: uymayan olay yalnızca o subscription'a teslim edilmez, diğer subscription'ları etkilemez.
</details>

### 6. Tek seçim
Bir subscription'da **max delivery attempts = 10** ve **event TTL = 30 dakika** ayarlı. Handler sürekli 500 dönüyor. Ne olur?

- A) Event Grid tam 10 deneme yapar, TTL yok sayılır
- B) Önce hangi sınır dolarsa (deneme sayısı **veya** TTL) teslim biter; TTL dolarsa 10 deneme tamamlanmadan da son bulabilir
- C) Event Grid 30 dakika boyunca saniyede bir dener
- D) Olay anında silinir

<details><summary>Cevap</summary>

**B.** İki sınır birlikte uygulanır, önce dolan işi bitirir. Backoff aralıkları büyüdüğü için 30 dakikada 10 denemeye ulaşılamayabilir. Bu aralıklar yapılandırılamaz; yalnızca max attempts ve TTL ayarlanır.
</details>

### 7. Tek seçim
Handler bir webhook ve **400 Bad Request** dönüyor (olay bozuk). Subscription'da dead-letter **yapılandırılmamış**. Olaya ne olur?

- A) 30 kez yeniden denenir
- B) Retry edilmeden dead-letter'a gönderilmek istenir; dead-letter yapılandırılmadığı için olay düşer
- C) Otomatik olarak Service Bus'a taşınır
- D) Event Grid, subscription'ı hemen siler

<details><summary>Cevap</summary>

**B.** 400/413 gibi kalıcı hatalar retry edilmez. Dead-letter **varsayılan kapalıdır**; kapalıysa olay kaybolur. Bu yüzden üretimde dead-letter container'ı tanımlanmalıdır.
</details>

### 8. Kod tamamlama (az CLI)
Subscription'a dead-letter hedefi ve retry ilkesi ekleyin. Boşlukları doldurun.

```bash
az eventgrid event-subscription create --name sub1 --source-resource-id $TOPIC_ID \
  --endpoint-type azurefunction --endpoint $FUNC_ID \
  --max-delivery-attempts 5 --______ 60 \
  --deadletter-endpoint "$ST_ID/blobServices/default/containers/deadletter"
```

- A) `--event-ttl`
- B) `--lock-duration`
- C) `--retry-interval`
- D) `--message-ttl`

<details><summary>Cevap</summary>

**A.** Olayın retry için yaşayabileceği süre dakika cinsinden `--event-ttl` ile verilir (varsayılan 1440, en çok 1440). `--lock-duration` Service Bus kavramıdır; Event Grid retry aralığı yapılandırılamaz.
</details>

### 9. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Event Grid teslimi **at-least-once**'tır; aynı olay birden fazla kez gelebilir. | ☐ | ☐ |
| b) Event Grid olayların gönderildiği sırayla teslim edileceğini garanti eder. | ☐ | ☐ |
| c) Dead-letter edilen olaylar bir Storage container'ında blob olarak saklanır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Handler'lar idempotent olmalı ve sıra bağımlı olmamalıdır; sıra/session gerekiyorsa Service Bus kullanılır.
</details>

### 10. Senaryo
Bir ödeme sistemi, "bu ödemeyi tahsil et" komutlarını **aynı müşteri için sıralı** ve **tam olarak bir tüketici** tarafından, hata olursa DLQ'ya ayırarak işlemek istiyor. Hangisi uygundur?

- A) Event Grid custom topic + webhook
- B) Event Grid system topic
- C) Service Bus queue (session ile) ve DLQ
- D) Event Hubs

<details><summary>Cevap</summary>

**C.** Bu bir **iş komutu**dur; sıra, session, işleme garantisi ve DLQ Service Bus'ın alanıdır. Event Grid bildirim içindir ve sıra/session sunmaz; Event Hubs telemetri akışıdır.
</details>

### 11. Çoklu seçim (iki doğru cevap)
Event Grid ile Service Bus'ı **birlikte** kullanmanın **hangi ikisi** doğru gerekçesidir?

- A) Event Grid olayı yakalar, Service Bus queue handler'ı olayı dayanıklı kuyruğa koyar; tüketici kendi hızında işler
- B) Event Grid'in retry politikasını tamamen devre dışı bırakır
- C) Service Bus tüketicisi sıra/session/DLQ özelliklerinden yararlanır
- D) Service Bus topic'leri Event Grid olaylarının şemasını otomatik CloudEvents'e çevirir

<details><summary>Cevap</summary>

**A ve C.** Event Grid → Service Bus queue, "olay bildirimi + dayanıklı işleme" kalıbıdır. Retry'ı devre dışı bırakmak (B) veya şema çevirisi (D) bu kalıbın amacı değildir.
</details>

### 12. Sıralama
Bir webhook handler'ın Event Grid subscription'ına eklenmesini sıraya koyun: **(1)** Event Grid doğrulama olayını (`validationCode`) webhook'a gönderir · **(2)** Subscription oluşturma isteği yapılır · **(3)** Handler `validationResponse` ile kodu geri döner · **(4)** Subscription etkinleşir ve olaylar akmaya başlar.

<details><summary>Cevap</summary>

**2 → 1 → 3 → 4.** Webhook sahipliği handshake ile kanıtlanır. Azure Functions, Service Bus, Storage Queue gibi Azure hedeflerinde bu adım otomatiktir.
</details>

### 13. Kod tamamlama (Python)
Aşağıdaki Python kodu custom topic'e bir CloudEvent yayınlar. Boşluğu doldurun.

```python
from azure.core.messaging import CloudEvent
from azure.eventgrid import EventGridPublisherClient
from azure.identity import DefaultAzureCredential

client = EventGridPublisherClient(endpoint, DefaultAzureCredential())
client.______(CloudEvent(source="/orders/api", type="Contoso.Order.Placed", data={"amount": 1500}))
```

- A) `publish`
- B) `send`
- C) `emit`
- D) `post_event`

<details><summary>Cevap</summary>

**B.** `EventGridPublisherClient.send(...)` tek olay veya liste kabul eder. `CloudEvent` için `source` ve `type` zorunludur.
</details>

### 14. Tek seçim
Bir custom topic **CloudEvents v1.0 input schema** ile oluşturuldu. Bir geliştirici subscription'ı `--event-delivery-schema eventgridschema` ile oluşturmak istiyor. Sonuç?

- A) Sorunsuz çalışır; Event Grid otomatik çevirir
- B) Desteklenmez: CloudEvents girişli topic'ten Event Grid şemasıyla teslim yapılamaz
- C) Yalnızca dead-letter açıksa çalışır
- D) Yalnızca Service Bus handler'larında çalışır

<details><summary>Cevap</summary>

**B.** Event Grid şeması CloudEvents'in extension özniteliklerini taşıyamaz; bu yüzden CloudEvents input schema'sından Event Grid çıktı şeması kullanılamaz. Delivery schema subscription'da seçilir ama input schema ile uyumlu olmalıdır.
</details>

### 15. Tek seçim
Subject'i `/blobServices/default/containers/images/blobs/photo1.jpg` olan olaylardan yalnızca **`.jpg`** uzantılı olanları almak istiyorsun. En basit filtre hangisidir?

- A) `--advanced-filter data.url StringContains jpg`
- B) `--subject-ends-with .jpg`
- C) `--included-event-types .jpg`
- D) `--subject-begins-with .jpg`

<details><summary>Cevap</summary>

**B.** Uzantı/yol eşleşmesi için subject-ends-with/begins-with filtreleri yeterlidir ve en basitidir. Event type filtresi `eventType` değerine bakar; begins-with öneki eşler. (A de çalışabilir ama gereksiz karmaşıktır ve `data` yapısına bağımlıdır.)
</details>

### 16. Vaka (kısa)
**Durum:** Bir medya şirketi `VideoPublished` olaylarını custom topic'e yayınlıyor. Üç subscription var: *search-index* (Function), *notifications* (webhook, geceleri bakımda) ve *analytics* (Service Bus queue). Gereksinimler: (1) yalnızca `data.durationSeconds > 60` olan videolar *search-index*'e gitsin, (2) *notifications* bakımdayken (6 saat) olaylar kaybolmasın, bakım sonrası teslim edilsin, (3) yine de teslim edilemeyen olaylar incelenebilsin, (4) *analytics* tüketicisi olayları kendi hızında, hata olursa DLQ ile işlesin.

**Soru:** Hangi üç çözüm gereksinimleri karşılar?

- A) *search-index* subscription'ına `--advanced-filter data.durationSeconds NumberGreaterThan 60`
- B) *notifications* subscription'ında event TTL'i bakım süresinden uzun (ör. 1440 dk) ve max attempts'i yeterli tutmak; dead-letter container'ı tanımlamak
- C) *analytics* handler'ı olarak Service Bus queue kullanmak; DLQ ve PeekLock tüketicinin tarafındadır
- D) Event Grid retry aralığını 1 saate sabitlemek
- E) Dead-letter'ı kapalı bırakmak; varsayılan zaten güvenlidir

<details><summary>Cevap</summary>

**A, B ve C.** A payload filtresidir. B bakım penceresini kapsayan retry/TTL + dead-letter ile kayıp riskini azaltır (retry aralığı Event Grid tarafından yönetilir). C, olayı dayanıklı bir kuyruğa taşır ve tüketici kendi hızında, DLQ ile işler. D yapılandırılamaz bir ayardır; E'de varsayılan dead-letter kapalıdır, bu da olayların sessizce düşmesi demektir.
</details>
