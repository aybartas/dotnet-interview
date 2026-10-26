# C2.8 Azure Functions: Serverless API, Trigger ve Binding — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Connect to and consume Azure services* (%20-25) · 16 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek/çoklu seçim, evet/hayır, sıralama, senaryo, kod ve CLI tamamlama, vaka); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 13/16** (~%80).

---

### 1. Tek seçim
Bir function; bir Service Bus kuyruğundan mesaj alıyor, Cosmos DB'den bir belge okuyor ve sonucu başka bir kuyruğa yazıyor. Bu function'da kaç **trigger** vardır?

- A) 3
- B) 2
- C) 1
- D) 0; yalnızca binding vardır

<details><summary>Cevap</summary>

**C.** Function'ı başlatan tek şey trigger'dır ve function başına **tam bir** trigger bulunur (Service Bus). Cosmos DB okuması **input binding**, ikinci kuyruğa yazma **output binding**'dir.
</details>

### 2. Tek seçim
Yeni bir .NET 10 function app yazıyorsun ve HTTP function'larında `HttpRequest` / `IActionResult` kullanmak istiyorsun. Hangi model ve builder çağrısı doğrudur?

- A) In-process model, `ConfigureFunctionsWorkerDefaults`
- B) Isolated worker model, `ConfigureFunctionsWebApplication`
- C) In-process model, `ConfigureFunctionsWebApplication`
- D) Isolated worker model, yalnızca `HttpRequestData` kullanılabilir; ASP.NET Core tipleri desteklenmez

<details><summary>Cevap</summary>

**B.** .NET 10 yalnızca isolated worker modelinde desteklenir (in-process 10 Kasım 2026'da destek dışına çıkıyor ve .NET 8'de kalıyor). ASP.NET Core entegrasyonu `ConfigureFunctionsWebApplication` ile açılır; D yanlıştır, `HttpRequestData` eski/alternatif yoldur.
</details>

### 3. Tek seçim
Bir function app'in **hem sanal ağdaki özel endpoint'lere erişmesi hem de trafik yokken sıfıra ölçeklenmesi** gerekiyor. Hangi plan?

- A) Klasik Consumption
- B) Flex Consumption
- C) Premium (EP1)
- D) Dedicated (App Service planı)

<details><summary>Cevap</summary>

**B.** Flex Consumption hem VNet entegrasyonu hem scale-to-zero sunar. Klasik Consumption'da VNet yoktur; Premium ve Dedicated VNet destekler ama **scale-to-zero yapmaz** (en az bir instance çalışır).
</details>

### 4. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) `local.settings.json` dosyası `func azure functionapp publish` ile Azure'a dağıtılan ayarların kaynağıdır ve repoya konması önerilir. | ☐ | ☐ |
| b) Azure'daki karşılığı **Application settings**'tir. | ☐ | ☐ |
| c) `host.json` host düzeyindeki davranışı (ör. Service Bus `maxConcurrentCalls`) yapılandırır ve uygulamayla birlikte dağıtılır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** `local.settings.json` yalnızca yerel çalışma içindir, secret içerebilir, repoya konmaz ve dağıtılmaz (publish sırasında ayarları yüklemek ayrı ve bilinçli bir seçenektir). `host.json` ise dağıtılır.
</details>

### 5. Kod tamamlama (C#)
Bir Service Bus trigger, **system-assigned managed identity** ile bağlanacak. Function şöyle:

```csharp
[Function("ProcessOrder")]
public void Run([ServiceBusTrigger("order-processing", Connection = "SbConn")] ServiceBusReceivedMessage msg) { }
```

Azure'da hangi **app setting** tanımlanmalıdır?

- A) `SbConn = sb://mysb.servicebus.windows.net`
- B) `SbConn__fullyQualifiedNamespace = mysb.servicebus.windows.net`
- C) `SbConn:fullyQualifiedNamespace = mysb.servicebus.windows.net`
- D) `ServiceBusConnection__namespace = mysb`

<details><summary>Cevap</summary>

**B.** `Connection` bir **ayar öneki**dir; identity-based bağlantıda `<önek>__fullyQualifiedNamespace` (çift alt çizgi) kullanılır. `:` ayırıcısı yalnızca Key Vault/App Configuration kaynaklı yapılandırmada geçerlidir. Ek olarak identity'ye *Azure Service Bus Data Receiver* rolü verilmelidir.
</details>

### 6. Çoklu seçim (iki doğru cevap)
Bir function app'in **Cosmos DB output binding**'ini managed identity ile kullanması için **hangi ikisi** gerekir?

- A) `<önek>__accountEndpoint` app setting'i
- B) Identity'ye Azure RBAC **Contributor** rolü
- C) Cosmos DB **data-plane** rol ataması (ör. Built-in Data Contributor)
- D) `AzureWebJobsStorage` içine Cosmos anahtarı

<details><summary>Cevap</summary>

**A ve C.** Endpoint ayarı bağlantıyı tanımlar; Cosmos DB veri erişimi Azure RBAC'tan **ayrı**, kendi data-plane rol atamasıyla (`az cosmosdb sql role assignment create`) verilir. Contributor yönetim düzlemi rolüdür, veri okutmaz/yazdırmaz (B yanlış).
</details>

### 7. Kod tamamlama (Python)
Aşağıdaki Python v2 function'ı her gün 02:00'de (UTC) çalışmalıdır. Boşluğu doldurun.

```python
@app.timer_trigger(schedule="______", arg_name="timer")
def nightly(timer: func.TimerRequest) -> None:
    ...
```

- A) `0 2 * * *`
- B) `0 0 2 * * *`
- C) `2 0 * * * *`
- D) `* * 2 * * *`

<details><summary>Cevap</summary>

**B.** Timer trigger **altı alanlı NCRONTAB** kullanır: `{saniye} {dakika} {saat} {gün} {ay} {haftanın günü}`. A beş alanlıdır (geçersiz); C her saatin 0. dakikasının 2. saniyesinde çalışır; D 02:00 saati boyunca her saniye çalışır.
</details>

### 8. Sıralama
Yerelde geliştirip Azure'a dağıtmak için adımları doğru sıraya koyun: **(1)** `func azure functionapp publish <app>` · **(2)** `func init --worker-runtime dotnet-isolated` · **(3)** `az functionapp create --flexconsumption-location …` · **(4)** `func start` ile yerelde test.

<details><summary>Cevap</summary>

**2 → 4 → 3 → 1.** Proje oluşturulur, yerelde test edilir, hedef function app (ve gerekli kimlik/ayarlar) oluşturulur, ardından kod yayımlanır. (Function app'i test öncesinde oluşturmak da mümkündür; ancak publish, hedefin var olmasını gerektirir ve her zaman en sonda gelir.)
</details>

### 9. CLI tamamlama
Flex Consumption plan üzerinde, .NET isolated çalışan bir function app oluşturuyorsunuz. Eksik parametreyi seçin.

```bash
az functionapp create --name func-demo --resource-group rg-demo --storage-account stdemo \
  ______ westeurope --runtime dotnet-isolated --runtime-version 10.0
```

- A) `--consumption-plan-location`
- B) `--flexconsumption-location`
- C) `--plan`
- D) `--premium-plan-location`

<details><summary>Cevap</summary>

**B.** Flex Consumption'a özel parametre `--flexconsumption-location`'dır (A klasik Consumption içindir). Desteklenen runtime sürümleri `az functionapp list-flexconsumption-runtimes` ile sorgulanır.
</details>

### 10. Tek seçim
Bir HTTP function'ı `AuthorizationLevel.Function` ile korunuyor. Bir istemci, kullanıcının **kim olduğunu** (Entra ID kimliği) doğrulamak istiyor. Function key bunun için yeterli midir?

- A) Evet, function key kullanıcıyı doğrular
- B) Hayır; key yalnızca paylaşılan bir sır olarak erişimi kısıtlar, kullanıcı doğrulaması için Easy Auth / API Management / token doğrulama gerekir
- C) Evet, ama yalnızca `AuthorizationLevel.Admin` ile
- D) Hayır; Functions HTTP trigger'larında kimlik doğrulama hiç desteklenmez

<details><summary>Cevap</summary>

**B.** Anahtarlar kimlik doğrulama değil basit erişim kontrolüdür. Kullanıcı kimliği için App Service Authentication (Easy Auth), API Management veya kod içinde JWT doğrulaması kullanılır.
</details>

### 11. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Klasik Consumption planı VNet entegrasyonunu destekler. | ☐ | ☐ |
| b) Premium planda her zaman en az bir instance çalışır (scale-to-zero yoktur). | ☐ | ☐ |
| c) Flex Consumption'da in-process .NET modeli kullanılabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** VNet için Flex, Premium veya Dedicated gerekir. Premium önceden ısıtılmış instance'larla soğuk başlangıcı ortadan kaldırır ama sürekli ücretlenir. Flex yalnızca isolated worker destekler.
</details>

### 12. Kod tamamlama (C#)
Bir function HTTP isteğiyle bir belgeyi Cosmos DB'ye yazacak. Boşluğu doldurun.

```csharp
public class Result
{
    [______(databaseName: "shop", containerName: "orders", Connection = "CosmosConnection")]
    public Order? Order { get; set; }

    [HttpResult]
    public IActionResult? Response { get; set; }
}
```

- A) `CosmosDBTrigger`
- B) `CosmosDBInput`
- C) `CosmosDBOutput`
- D) `Output`

<details><summary>Cevap</summary>

**C.** Yazma işlemi **output binding**'dir: `CosmosDBOutput`. `CosmosDBTrigger` change feed'e tepki verir; `CosmosDBInput` okur. `[HttpResult]` HTTP yanıtını işaretler.
</details>

### 13. Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** Functions'ta **trigger** olabilir?

- A) Event Grid
- B) Cosmos DB change feed
- C) Key Vault referansı
- D) `host.json`

<details><summary>Cevap</summary>

**A ve B.** Event Grid ve Cosmos DB (change feed) trigger'dır. Key Vault referansı bir app setting değeri biçimidir; `host.json` yapılandırma dosyasıdır.
</details>

### 14. Senaryo
Bir function, Service Bus kuyruğundan mesaj işliyor; kod bazen geçici bir hata nedeniyle exception fırlatıyor. Ekip, `host.json`'a function-level `retry` ilkesi (fixed delay, 5 deneme) ekledi; kuyruğun `MaxDeliveryCount` değeri de 10. Sorun: bir mesaj beklenenden çok daha fazla kez işleniyor. Neden?

- A) Retry ilkesi Service Bus trigger'larında desteklenmez, hata verir
- B) Function-level retry ile Service Bus'ın kendi teslim sayacı **birbirinin üstüne biner**: her teslimde function 5 kez denenir, ardından mesaj abandon edilip yeniden teslim edilir
- C) `MaxDeliveryCount` yalnızca topic'lerde çalışır
- D) Retry ilkesi mesajı otomatik olarak tamamlar

<details><summary>Cevap</summary>

**B.** Service Bus gibi kendi yeniden teslim mekanizması olan kaynaklarda iki retry katmanı çarpılır (5 × 10'a kadar çalışma). Çözüm: tek katman seç, function'ı **idempotent** yaz ve kalıcı hataları DLQ'ya yönlendir (→ C2.9).
</details>

### 15. Kod tamamlama (app setting)
Function app'in kimliğine, **Azure Blob/Queue** ile çalışan host depolaması (`AzureWebJobsStorage`) için secret'sız erişim verilecek. Doğru app setting adı hangisidir?

- A) `AzureWebJobsStorage__accountName`
- B) `AzureWebJobsStorage__fullyQualifiedNamespace`
- C) `AzureWebJobsStorage__accountEndpoint`
- D) `AzureWebJobsStorage:identity`

<details><summary>Cevap</summary>

**A.** Storage için kimlik tabanlı bağlantıda `<önek>__accountName` (veya `__blobServiceUri`/`__queueServiceUri`) kullanılır. `fullyQualifiedNamespace` Service Bus/Event Hubs, `accountEndpoint` Cosmos DB içindir. İlgili *Storage Blob/Queue/Table Data* rolleri de atanmalıdır.
</details>

### 16. Vaka (kısa)
**Durum:** Bir fintech, ödeme webhook'unu alan bir API'yi Functions'a taşıyor. Gereksinimler: (1) iç ağdaki özel endpoint'li bir Cosmos DB hesabına erişim, (2) gece trafik yokken ücretin sıfıra yakın olması, (3) kampanya anlarında dakikalar içinde yüzlerce instance'a ölçek, (4) kodda secret/connection string bulunmaması, (5) webhook çağrılarının yalnızca bilinen bir anahtarla yapılabilmesi.

**Soru:** Hangi seçimler gereksinimleri karşılar? (Üç doğru cevap)

- A) Flex Consumption planı
- B) Klasik Consumption planı
- C) System-assigned managed identity + `CosmosConnection__accountEndpoint` + Cosmos data-plane rolü
- D) `AuthorizationLevel.Function` ile HTTP trigger
- E) Cosmos DB anahtarını `local.settings.json`'dan Azure'a kopyalamak

<details><summary>Cevap</summary>

**A, C ve D.** Flex: VNet + scale-to-zero + hızlı ölçek (1-3). Managed identity: secret'sız bağlantı (4). Function key: bilinen anahtarla erişim (5; gerçek kimlik doğrulama gerekiyorsa imza doğrulaması/APIM eklenir). B VNet desteklemez; E secret'ı koda/ayara taşır ve `local.settings.json` dağıtılmaz.
</details>
