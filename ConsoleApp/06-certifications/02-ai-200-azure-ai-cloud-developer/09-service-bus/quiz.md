# C2.9 Azure Service Bus: Kuyruk, Topic ve Dead-letter — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Connect to and consume Azure services* (%20-25) · 17 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek/çoklu seçim, evet/hayır, sıralama, senaryo, kod ve CLI tamamlama, vaka); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 14/17** (~%80).

---

### 1. Tek seçim
Bir sipariş sistemi her sipariş olayını **stok**, **fraud** ve **bildirim** servislerinin hepsine, her servisin kendi hızında işleyeceği şekilde iletmek istiyor. En uygun Service Bus yapısı hangisidir?

- A) Tek bir queue ve üç rakip tüketici
- B) Bir topic ve her servis için ayrı bir subscription
- C) Üç ayrı queue'ya aynı mesajı elle üç kez gönderen bir queue
- D) Basic tier'da bir queue

<details><summary>Cevap</summary>

**B.** Topic'te her subscription mesajın **kendi kopyasını** alır; tüketiciler bağımsızdır. A'da her mesajı yalnızca bir tüketici işler (rakip tüketici); C gönderene gereksiz iş yükler; D'de zaten topic yoktur.
</details>

### 2. Tek seçim
Bir queue'dan PeekLock modunda alınan mesajın işlenmesi sırasında tüketici süreci çöküyor. Mesaja ne olur?

- A) Kalıcı olarak kaybolur
- B) Lock süresi dolunca kuyrukta yeniden görünür ve tekrar teslim edilir (`DeliveryCount` artar)
- C) Hemen dead-letter queue'ya taşınır
- D) Deferred duruma geçer

<details><summary>Cevap</summary>

**B.** PeekLock at-least-once sağlar: Complete edilmeyen mesaj lock dolunca yeniden teslim edilir. Kayıp (A) ReceiveAndDelete'in riskidir. DLQ'ya ancak `MaxDeliveryCount` aşılırsa veya uygulama açıkça dead-letter ederse gidilir.
</details>

### 3. Çoklu seçim (iki doğru cevap)
Bir mesajın bir queue'nun **dead-letter queue**'suna düşmesine **hangi ikisi** neden olabilir?

- A) `DeliveryCount` değerinin `MaxDeliveryCount`'u aşması
- B) Tüketicinin mesajı `Complete` etmesi
- C) Uygulamanın `DeadLetterMessageAsync` çağırması
- D) Mesajın `Peek` ile okunması

<details><summary>Cevap</summary>

**A ve C.** Eşik aşımı (`MaxDeliveryCountExceeded`) ve uygulama kararı DLQ nedenleridir. Complete mesajı siler; Peek mesajı kilitlemez/taşımaz.
</details>

### 4. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Basic tier'da topic ve subscription oluşturulabilir. | ☐ | ☐ |
| b) Her subscription'ın kendi dead-letter queue'su vardır. | ☐ | ☐ |
| c) DLQ'daki mesajlar `MaxDeliveryCount` sonrasında otomatik olarak orijinal kuyruğa geri taşınır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** Topic için en az Standard gerekir. DLQ her queue ve subscription için ayrı bir alt kuyruktur. DLQ mesajları kendiliğinden işlenmez; birinin okuyup yeniden göndermesi gerekir.
</details>

### 5. Kod tamamlama (C#)
Aşağıdaki kod, `order-processing` kuyruğunun **dead-letter** alt kuyruğundan okumalıdır. Boşluğu doldurun.

```csharp
var receiver = client.CreateReceiver("order-processing",
    new ServiceBusReceiverOptions { ______ = SubQueue.DeadLetter });
```

- A) `ReceiveMode`
- B) `SubQueue`
- C) `PrefetchCount`
- D) `Identifier`

<details><summary>Cevap</summary>

**B.** Alt kuyruk seçimi `ServiceBusReceiverOptions.SubQueue` ile yapılır. `ReceiveMode` PeekLock/ReceiveAndDelete'i belirler.
</details>

### 6. Senaryo
Bir queue'da `MaxDeliveryCount = 3` ve işleyici, ödeme servisi geçici olarak kapalıyken her çağrıda `AbandonMessageAsync` yapıyor. Kuyrukta bekleyen **geçerli** bir mesaj kısa sürede kayboluyor (hedef kuyrukta yok). Mesaj nerede aranmalıdır ve neden?

- A) Silinmiştir; Abandon mesajı siler
- B) DLQ'da, `MaxDeliveryCountExceeded` nedeniyle
- C) Deferred kuyruğunda
- D) Scheduled mesajlar listesinde

<details><summary>Cevap</summary>

**B.** Her abandon `DeliveryCount`'u artırır; üçüncü başarısız teslimden sonra eşik aşılır ve mesaj DLQ'ya taşınır. Mesaj **kaybolmadı**; operatör DLQ'dan okuyup yeniden gönderebilir. Çözüm: eşiği ve backoff stratejisini geçici hatalara göre ayarlamak, DLQ alarmı kurmak.
</details>

### 7. Tek seçim
`high-value` subscription'ında `amount > 1000` SQL filtre kuralını ekledin ama subscription hâlâ 50 TL'lik siparişleri de alıyor. En olası neden?

- A) SQL filtreler mesajın gövdesini (body) değerlendirir ve JSON'u desteklemez
- B) Varsayılan `$Default` (TrueFilter) kuralı silinmedi
- C) Subscription'ın `MaxDeliveryCount` değeri çok düşük
- D) Topic partitioned olduğu için filtre çalışmıyor

<details><summary>Cevap</summary>

**B.** Yeni subscription `$Default` kuralıyla gelir ve kurallar OR ile değerlendirilir. A yanlıştır: filtreler zaten gövdeye değil yalnızca **property**'lere bakar (`amount` gönderirken `ApplicationProperties`'e eklenmelidir). `MaxDeliveryCount` (C) ve topic bölümleme (D) filtre eşleşmesini etkilemez.
</details>

### 8. Kod tamamlama (az CLI)
`orders` topic'indeki `high-value` subscription'ının varsayılan kuralını silin.

```bash
az servicebus topic subscription rule ______ --resource-group rg --namespace-name sb \
  --topic-name orders --subscription-name high-value --name '$Default'
```

- A) `remove-all`
- B) `delete`
- C) `disable`
- D) `reset`

<details><summary>Cevap</summary>

**B.** `az servicebus topic subscription rule delete`. Ardından `rule create --filter-sql-expression "amount > 1000"` ile kendi kuralın eklenir.
</details>

### 9. Çoklu seçim (iki doğru cevap)
Müşteri başına sipariş güncellemelerinin **sıralı** ve aynı anda **tek alıcıda** işlenmesi gerekiyor. **Hangi ikisi** gerekir?

- A) Gönderirken `SessionId = customerId`
- B) Varlığın `RequiresSession = true` ile oluşturulması
- C) Basic tier
- D) ReceiveAndDelete modu

<details><summary>Cevap</summary>

**A ve B.** Sıra ve tek-alıcı garantisi session ile sağlanır; session gerektiren varlıkta `SessionId`'siz mesaj gönderilemez. Basic'te session yoktur; alma modu sıra garantisi vermez.
</details>

### 10. Sıralama
Bir mesajın başarısız işlenip DLQ'dan kurtarılma akışını sıraya koyun: **(1)** Operatör DLQ'dan okuyup mesajı yeni mesaj olarak orijinal kuyruğa gönderir · **(2)** İşleyici mesajı `MaxDeliveryCount` kez abandon eder · **(3)** Mesaj `MaxDeliveryCountExceeded` ile DLQ'ya taşınır · **(4)** DLQ'daki orijinal mesaj Complete edilir.

<details><summary>Cevap</summary>

**2 → 3 → 1 → 4.** Eşik aşılınca broker mesajı DLQ'ya taşır; operatör (veya bir araç) DLQ'dan okuyup yeniden gönderir; ardından DLQ kopyası Complete edilerek temizlenir.
</details>

### 11. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) `ServiceBusProcessor`'da `AutoCompleteMessages = true` iken handler exception fırlatırsa mesaj abandon edilir. | ☐ | ☐ |
| b) `AutoCompleteMessages = true` iken handler'da exception'ı yutup sessizce dönersen mesaj Complete edilir. | ☐ | ☐ |
| c) Lock süresi dolduktan sonra `CompleteMessageAsync` çağrısı başarılı olur ve mesaj silinir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Otomatik modda başarılı dönüş Complete, exception Abandon demektir; hatayı yutmak mesajı kaybettirir. Lock sona erdiyse Complete `MessageLockLost` ile başarısız olur ve mesaj yeniden teslim edilir.
</details>

### 12. Tek seçim
Bir mesajın `TimeToLive` süresi doldu, ancak kuyruğun DLQ'sunda görünmüyor. Mesaj ne olmuştur?

- A) Kuyruğa hiç yazılmamıştır
- B) Kuyrukta **dead-lettering on message expiration** kapalıdır; mesaj sessizce silinmiştir
- C) Deferred kuyruğa taşınmıştır
- D) `MaxDeliveryCount` aşıldığı için session'a bağlanmıştır

<details><summary>Cevap</summary>

**B.** TTL dolan mesaj yalnızca varlıkta bu özellik açıksa `TTLExpiredException` ile DLQ'ya gider; kapalıysa (varsayılan) düşürülür. Deferred mesajlar TTL nedeniyle DLQ'ya taşınmaz — C yanlıştır.
</details>

### 13. Kod tamamlama (C#)
Bir işleyici, payload'ı bozuk mesajı tekrar denemeden **hemen** DLQ'ya göndermelidir. Boşluğu doldurun.

```csharp
await args.______(args.Message, "BadPayload", "id is missing");
```

- A) `AbandonMessageAsync`
- B) `DeferMessageAsync`
- C) `DeadLetterMessageAsync`
- D) `CompleteMessageAsync`

<details><summary>Cevap</summary>

**C.** Kalıcı hata (poison/validation) → doğrudan dead-letter; sebep ve açıklama `DeadLetterReason` / `DeadLetterErrorDescription` olarak mesajla saklanır. Abandon geçici hata içindir ve eşik dolana kadar tekrar dener.
</details>

### 14. Kod tamamlama (Python)
Python'da bir queue'nun DLQ'sundan okumak için boşluğu doldurun.

```python
receiver = client.get_queue_receiver("order-processing", ______=ServiceBusSubQueue.DEAD_LETTER)
```

- A) `sub_queue`
- B) `receive_mode`
- C) `session_id`
- D) `prefetch_count`

<details><summary>Cevap</summary>

**A.** `sub_queue=ServiceBusSubQueue.DEAD_LETTER`. Mesajlarda `dead_letter_reason` ve `dead_letter_error_description` alanları bulunur.
</details>

### 15. Tek seçim
Function app, Service Bus trigger'ı **secret'sız** kullanacak ve yalnızca mesaj alıp settle edecek. Minimum doğru yapılandırma hangisidir?

- A) Connection string içeren app setting
- B) `<önek>__fullyQualifiedNamespace` app setting'i + kimliğe *Azure Service Bus Data Receiver* rolü
- C) `<önek>__fullyQualifiedNamespace` + kimliğe *Azure Service Bus Data Sender* rolü
- D) `<önek>__accountEndpoint` + *Cosmos DB Built-in Data Reader*

<details><summary>Cevap</summary>

**B.** Trigger alma ve settle için *Data Receiver* gerekir (en az yetki). Sender yalnızca göndermek içindir (C); `accountEndpoint` Cosmos DB bağlantı ayarıdır (D).
</details>

### 16. Tek seçim
Bir uygulama, **100 MB** büyüklüğünde mesajları sanal ağ içinde özel endpoint üzerinden iletmek ve öngörülebilir gecikme istiyor. Hangi tier?

- A) Basic
- B) Standard
- C) Premium
- D) Hepsi aynı

<details><summary>Cevap</summary>

**C.** Büyük mesaj (100 MB'a kadar), ayrılmış kaynaklar ve ağ izolasyonu (Private Link/VNet) Premium'a özgüdür. Basic/Standard'da mesaj ~256 KB ile sınırlıdır (limitleri sınavdan önce doğrula).
</details>

### 17. Vaka (kısa)
**Durum:** Bir lojistik şirketi, kargo olaylarını `shipments` topic'ine gönderiyor. `customs` subscription'ı yalnızca `destinationCountry = 'GB'` olan olayları işleyecek (SQL filtre), işleyici bir bağımlı API kapalıyken sürekli abandon yapıyor, ekip ise dün gece 4.000 olayın "kaybolduğunu" bildirdi. Ayrıca `customs`'a, gönderirken **gövdeye** `destinationCountry` yazılmış olaylar da ulaşmıyor.

**Soru:** Hangi üç bulgu/çözüm doğrudur?

- A) Kayıp sanılan olaylar büyük olasılıkla `customs` subscription'ının DLQ'sunda `MaxDeliveryCountExceeded` ile bekliyordur
- B) Filtrenin çalışması için `destinationCountry` mesajın **ApplicationProperties**'ine eklenmelidir; filtre gövdeye bakmaz
- C) DLQ derinliği için alarm kurmak ve DLQ'dan resubmit süreci tanımlamak gerekir
- D) Çözüm olarak ReceiveAndDelete moduna geçmek mesajların kaybolmasını önler
- E) Topic'i Basic tier'a taşımak DLQ sorununu giderir

<details><summary>Cevap</summary>

**A, B ve C.** Abandon'lanan mesajlar eşik aşılınca subscription DLQ'sunda birikir (kayıp değil); filtreler property tabanlıdır; operasyonel olarak DLQ izlenmeli ve yeniden işleme süreci olmalıdır. D tam tersine kayıp riskini artırır (at-most-once); E yanlıştır (Basic'te topic yok).
</details>
