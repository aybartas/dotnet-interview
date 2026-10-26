# C2.8 Azure Functions: Serverless API, Trigger ve Binding

> 📁 `ConsoleApp/06-certifications/02-ai-200-azure-ai-cloud-developer/08-azure-functions/` · Sınav alanı: **Connect to and consume Azure services (%20-25)** · Önkoşul: C1.6 (app hosting), C2.4 (Cosmos DB) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of October 7, 2026):** *Connect to and consume Azure services* — **trigger ve binding**'lerle serverless API oluşturma · function app'leri **yapılandırma ve dağıtma**. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)'ı kontrol et.

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

**Azure Functions**, olay güdümlü, **serverless** bir compute servisidir: kodu (bir *function*) yazarsın, platform onu bir **trigger** (tetikleyici) ile çalıştırır, ölçekler ve altyapıyı yönetir. Bir function'ın bağlantı kodunu azaltan iki kavram sınavın merkezidir:

| Kavram | Ne yapar? | Kaç tane? | Örnek |
|---|---|---|---|
| **Trigger** | Function'ı **başlatır**, veriyi function'a taşır | Function başına **tam 1** | HTTP, Timer, Service Bus, Event Grid, Cosmos DB (change feed), Blob, Queue |
| **Input binding** | Function çalışırken başka bir kaynaktan **veri okur** | 0..n | Cosmos DB'den belge, Blob içeriği |
| **Output binding** | Function'ın sonucunu başka bir kaynağa **yazar** | 0..n | Cosmos DB'ye belge, Service Bus'a mesaj, kuyruğa ekleme |

```text
HTTP isteği ──▶ [HTTP trigger] Function ──▶ [Cosmos DB output binding] ──▶ orders container
Service Bus ──▶ [ServiceBus trigger] Function ──▶ [Cosmos DB input binding ile lookup]
```

Bu konu: **isolated worker model** (.NET) ve **Python v2** programlama modeli, HTTP trigger ile API, trigger/binding seçimi, `host.json` / `local.settings.json`, **hosting planları** (Flex Consumption, Consumption, Premium, Dedicated), **identity-based bağlantı** ve dağıtım. Laboratuvar yerelde Core Tools + emülatörlerle başlar, Flex Consumption'a dağıtımla biter (3.8). Mesajlaşma ayrıntıları → [C2.9](../09-service-bus/README.md), olay yönlendirme → [C2.10](../10-event-grid/README.md), Cosmos DB → [C2.4](../04-cosmos-db-nosql/README.md).

---

## 2. Neden?

- **Sınav:** "Connect to and consume Azure services" alanında Functions, kod/CLI tamamlama soruları için en verimli konudur: *hangi attribute?*, *hangi app setting adı?*, *hangi plan?*, *trigger mı binding mi?*
- **Gerçek işte:** Webhook'lar, kuyruk işleyiciler, zamanlanmış işler ve hafif API'ler için en az operasyonla çalışan bir ölçek birimidir; kullanılmayan zamanda (scale-to-zero) maliyet de sıfıra yaklaşır.
- **Kimlik:** "Connection string'i app setting'e koy" yerine **managed identity** ile secret'sız bağlantı (→ [C2.11](../11-identity-key-vault/README.md)) hem güvenlik hem sınav için artık varsayılan beklentidir.
- **Takvim:** **In-process model**'in desteği **10 Kasım 2026**'da sona eriyor; yeni ve migrate edilen .NET uygulamalarının yolu **isolated worker model**'dir. Linux Consumption planı ise **30 Eylül 2028**'de emekliye ayrılıyor; öneri Flex Consumption'a geçiştir *(tarihler Microsoft duyurularına göre; sınav öncesi doğrula)*.

---

## 3. Nasıl?

### 3.1 Programlama modelleri

| Dil | Model | Not |
|---|---|---|
| **.NET** | **Isolated worker** (kendi `Program.cs`'i olan ayrı süreç) | Functions runtime v4; .NET 10 (LTS), 9, 8 ve .NET Framework 4.8 desteklenir; **in-process model .NET 8'de kalır ve 10 Kasım 2026'da destek dışına çıkar** |
| **Python** | **v2 programlama modeli** (decorator tabanlı, `function_app.py`) | v1'deki `function.json` dosyası gerekmez; binding'ler decorator ile tanımlanır |
| JavaScript/TypeScript, Java, PowerShell | Kendi worker'ları | Sınav odağı .NET ve Python |

Isolated worker'ın in-process'e göre avantajları: kendi DI/middleware/yapılandırması, .NET sürümünü runtime'dan bağımsız seçme, **ASP.NET Core entegrasyonu** (HTTP function'larda `HttpRequest`/`IActionResult`), çakışmayan assembly'ler.

```csharp
// Program.cs (isolated worker, ASP.NET Core integration)
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);
builder.ConfigureFunctionsWebApplication();          // ASP.NET Core integration (HttpRequest / IActionResult)
builder.Services.AddApplicationInsightsTelemetryWorkerService();
builder.Services.ConfigureFunctionsApplicationInsights();
builder.Build().Run();
```

> `ConfigureFunctionsWebApplication` ASP.NET Core entegrasyonudur; HTTP kullanmayan bir uygulamada `ConfigureFunctionsWorkerDefaults` yeterlidir. Eski stil `new HostBuilder()...ConfigureFunctionsWorkerDefaults()` hâlâ çalışır ve `HttpRequestData`/`HttpResponseData` ile birlikte kullanılır. `FunctionsApplication.CreateBuilder` 2.x worker paketlerini ister.

### 3.2 HTTP trigger ile API

```csharp
public class OrdersApi(ILogger<OrdersApi> logger)
{
    public record OrderRequest(string CustomerId, decimal Amount);
    public record Order(string id, string customerId, decimal amount, DateTimeOffset createdAt);

    // POST /api/orders  -> writes the order to Cosmos DB via an output binding
    [Function("CreateOrder")]
    public CreateOrderResult CreateOrder(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "orders")] HttpRequest req,
        [FromBody] OrderRequest body)
    {
        var order = new Order(Guid.NewGuid().ToString(), body.CustomerId, body.Amount, DateTimeOffset.UtcNow);
        logger.LogInformation("Order {Id} accepted", order.id);
        return new CreateOrderResult
        {
            Order = order,
            HttpResponse = new CreatedResult($"/api/orders/{order.id}", order),
        };
    }

    public class CreateOrderResult
    {
        [CosmosDBOutput(databaseName: "shop", containerName: "orders", Connection = "CosmosConnection")]
        public Order? Order { get; set; }

        [HttpResult]                                     // the response returned to the HTTP caller
        public IActionResult? HttpResponse { get; set; }
    }
}
```

- **Birden çok çıktı** için dönüş tipi, her çıktı özelliğine attribute konmuş bir sınıftır; HTTP yanıtı `[HttpResult]` ile işaretlenir.
- **Auth level** (`AuthorizationLevel`): `Anonymous` (anahtar yok) · `Function` (function veya host anahtarı: `x-functions-key` header'ı ya da `?code=`) · `Admin` (master anahtar). Kullanıcı kimliği doğrulaması (Entra/OAuth) için App Service Authentication ("Easy Auth") veya API Management kullanılır; anahtarlar **kimlik doğrulama değil**, basit erişim kontrolüdür.
- `Route = "orders/{id}"` parametreleri method imzasına bağlanır; varsayılan önek `/api` `host.json` içindeki `extensions.http.routePrefix` ile değişir.

### 3.3 Trigger ve binding seçimi ⭐

| İhtiyaç | Trigger | Binding örneği | Not |
|---|---|---|---|
| HTTP API / webhook | **HTTP** | Cosmos DB output | Yanıt en fazla ~230 sn bekler (load balancer zaman aşımı) — uzun iş kuyruğa |
| Zamanlanmış iş | **Timer** | — | **6 alanlı NCRONTAB** `{saniye} {dakika} {saat} {gün} {ay} {haftanın günü}` |
| Kuyruktaki iş | **Service Bus** (queue/topic) | Cosmos DB input | Hata → abandon → DLQ (→ [C2.9](../09-service-bus/README.md)) |
| Olay tepkisi | **Event Grid** | — | Olay şeması veya `CloudEvent` (→ [C2.10](../10-event-grid/README.md)) |
| Veri değişimine tepki | **Cosmos DB** (change feed) | — | Lease container tutar (→ [C2.5](../05-cosmos-vector-change-feed/README.md)) |
| Blob'a tepki | **Blob** | Blob input/output | Flex Consumption'da yalnızca **event-based** (Event Grid kaynaklı) blob trigger desteklenir *(doğrula)* |
| Basit kuyruk | **Queue** (Storage) | Queue output | 64 KB mesaj sınırı; zengin özellik için Service Bus |

**Binding ifadeleri:** `{queueTrigger}`, `{data.orderId}` (JSON alanı), `{rand-guid}`, `{DateTime}`; ayar değerleri `%SettingName%` ile okunur (ör. `Connection = "ServiceBusConnection"`, `QueueName = "%OrdersQueue%"`).

```csharp
// Service Bus trigger (isolated): manual settlement via ServiceBusMessageActions
[Function("ProcessOrder")]
public async Task Run(
    [ServiceBusTrigger("order-processing", Connection = "ServiceBusConnection", AutoCompleteMessages = false)]
    ServiceBusReceivedMessage message,
    ServiceBusMessageActions actions)
{
    var order = message.Body.ToObjectFromJson<Order>();
    if (order is null) { await actions.DeadLetterMessageAsync(message, deadLetterReason: "BadPayload"); return; }
    // ... business logic; an unhandled exception => the message is abandoned and redelivered
    await actions.CompleteMessageAsync(message);
}
```

### 3.4 `host.json` ve `local.settings.json`

| Dosya | Amaç | Dağıtılır mı? |
|---|---|---|
| **`host.json`** | **Host** düzeyi davranış: loglama/sampling, `extensions.*` (Service Bus `maxConcurrentCalls`, `maxAutoLockRenewalDuration`, HTTP `routePrefix`), `functionTimeout`, `retry` | Evet |
| **`local.settings.json`** | **Yerel** app settings + connection string'ler (`Values`) | **Hayır** (`.gitignore`'a gider; secret içerir) — Azure'da karşılığı *Application settings* |
| `*.csproj` / `function_app.py` | Function tanımı (attribute / decorator) | Evet |

```jsonc
// host.json
{
  "version": "2.0",
  "functionTimeout": "00:10:00",
  "logging": { "applicationInsights": { "samplingSettings": { "isEnabled": true, "excludedTypes": "Request" } } },
  "extensions": { "serviceBus": { "maxConcurrentCalls": 8, "maxAutoLockRenewalDuration": "00:05:00" } }
}
```

```jsonc
// local.settings.json (local only, never committed)
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "ServiceBusConnection": "Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;",
    "CosmosConnection": "AccountEndpoint=https://localhost:8081/;AccountKey=<emulator-key>;"
  }
}
```

### 3.5 Hosting planları ⭐

| | **Flex Consumption** | **Consumption** (klasik) | **Premium** (Elastic) | **Dedicated** (App Service planı) |
|---|---|---|---|---|
| Scale to zero | **Evet** | Evet | Hayır (en az 1 instance) | Hayır |
| Cold start azaltma | **Always ready** instance'lar (isteğe bağlı) + hızlı ölçek | Yok | **Pre-warmed** + her zaman ısınmış instance | Always On |
| **VNet entegrasyonu** | **Evet** | **Hayır** | Evet | Evet |
| Varsayılan zaman aşımı | 30 dk (üst sınır sınırsız) | 5 dk (en çok 10 dk) | 30 dk (sınırsız) | 30 dk (sınırsız) |
| Ölçek | Fonksiyon/grup başına bağımsız; en çok **1000** instance (kota ile sınırlı) | Eski ölçek denetleyicisi (daha yavaş) | Önceden ısınmış + elastik | Manuel/otomatik App Service ölçeği |
| Instance boyutu | **2048 / 4096 MB** | Sabit | EP1-EP3 | Plan SKU'su |
| Fiyat modeli | İstek/çalışma süresi (+ always ready) | Çalışma süresi × bellek | Instance-saati (ısınmış min.) | Plan ücreti (sabit) |
| Not | Linux; **in-process .NET desteklenmez**; deployment slot desteklenmez *(doğrula)* | Linux Consumption **30 Eyl 2028**'de emekli | Slot, uzun işler | Mevcut App Service planını yeniden kullanma |

**Karar ağacı:**
1. Hem **VNet** hem **scale-to-zero** gerekiyor → **Flex Consumption**.
2. Soğuk başlangıç **kabul edilemez**, sabit gecikme + VNet + slot → **Premium**.
3. Zaten kullanılan, boşta kapasitesi olan App Service planı → **Dedicated**.
4. Basit, VNet gerekmeyen, Windows tabanlı → klasik **Consumption** (yeni projelerde Flex önerilir).

### 3.6 Yapılandırma ve identity-based bağlantılar ⭐

Trigger/binding'in `Connection` özelliği bir **ayar adı öneki**dir. Connection string yerine kimlik kullanmak için ayarlar **`<ÖNEK>__<özellik>`** (çift alt çizgi) biçiminde verilir:

| Servis | Ayar adı (önek `MyConn`) | Gereken rol |
|---|---|---|
| **Service Bus** | `MyConn__fullyQualifiedNamespace` = `mysb.servicebus.windows.net` | *Azure Service Bus Data Receiver* (trigger) · *Data Sender* (output) |
| **Cosmos DB** | `MyConn__accountEndpoint` = `https://myacct.documents.azure.com:443/` | Cosmos DB **data-plane RBAC** rolü (*Built-in Data Contributor*) — Azure RBAC değil, `az cosmosdb sql role assignment` |
| **Storage** (Blob/Queue) | `MyConn__accountName` (veya `__blobServiceUri`, `__queueServiceUri`) | *Storage Blob Data Owner/Contributor*, *Storage Queue Data Contributor* |
| **Event Hubs** | `MyConn__fullyQualifiedNamespace` | *Azure Event Hubs Data Receiver/Sender* |
| **`AzureWebJobsStorage`** (host'un kendi deposu) | `AzureWebJobsStorage__accountName` | Blob/Queue/Table için ilgili *Data* rolleri |

- Kullanıcı tarafından atanmış (user-assigned) kimlik için ek olarak `MyConn__credential = managedidentity` ve `MyConn__clientId = <id>` verilir; atanmamışsa **system-assigned** kimlik kullanılır.
- Aynı önek için hem connection string hem identity ayarı varsa **connection string önceliklidir**.
- **Key Vault referansı:** `@Microsoft.KeyVault(SecretUri=https://kv.vault.azure.net/secrets/Name/)` biçiminde app setting değeri; function app'in kimliği *Key Vault Secrets User* rolüne ihtiyaç duyar (→ [C2.11](../11-identity-key-vault/README.md)). App Configuration / Key Vault'tan okurken ayırıcı `__` yerine `:` kullanılır.
- Flex Consumption'da `FUNCTIONS_WORKER_RUNTIME` gibi bazı ayarlar app setting değil **function app yapılandırmasıdır** (CLI `--runtime`); Azure Files kullanılmaz, dağıtım paketi bir **blob container**'dadır *(ayrıntılar için Flex dokümanını doğrula)*.

### 3.7 Dağıtım

| Yöntem | Komut / araç | Not |
|---|---|---|
| Core Tools | `func azure functionapp publish <app>` | Hızlı geliştirici dağıtımı |
| Zip deploy / Run-from-package | `az functionapp deployment source config-zip` | Paket salt-okunur bağlanır |
| CI/CD | **GitHub Actions** (`Azure/functions-action`), Azure Pipelines | OIDC (federated credential) ile secret'sız giriş |
| **Deployment slot** | `az functionapp deployment slot create` + `swap` | Premium/Dedicated (ve sınırlı Consumption); sıfır kesintili geçiş |
| IaC | Bicep / Terraform | Flex için `functionAppConfig` bölümü |

*(bağlam)* **Durable Functions** (orchestrator + activity): fan-out/fan-in, human interaction, async HTTP API gibi kalıpları kod olarak yazılan bir iş akışıyla çözer; AI-200 hedeflerinde açıkça yoktur, mimari bilgisi olarak yeterlidir.

### 3.8 Lab — yerelde çalıştır, Flex Consumption'a dağıt

> Maliyet: Flex Consumption'da kullanılmayan zamanda ücret yok; ücretsiz aylık istek/çalışma süresi hibesi var. Yine de önce **budget alarm**'ı kur (→ C1.11). Cosmos DB için **serverless** hesap veya ücretsiz katman kullan.

**a) Yerel geliştirme** (Core Tools + Azurite + Service Bus emulator → emulator kurulumu [C2.9](../09-service-bus/README.md#39-lab--emulatör-ile-kuyruk-topic-ve-dlq)):

```bash
npm install -g azurite && azurite --silent --location ./.azurite &     # storage emulator for AzureWebJobsStorage
func init OrdersApi --worker-runtime dotnet-isolated --target-framework net10.0
cd OrdersApi
func new --template "Http trigger" --name CreateOrder --authlevel function
dotnet add package Microsoft.Azure.Functions.Worker.Extensions.CosmosDB
dotnet add package Microsoft.Azure.Functions.Worker.Extensions.ServiceBus
func start
# in another terminal
curl -s -X POST "http://localhost:7071/api/orders" -H "Content-Type: application/json" \
     -d '{"customerId":"c-42","amount":1500}'
```

**b) Azure kaynakları ve Flex Consumption function app:**

```bash
RG=rg-ai200-c28; LOC=westeurope
ST=stfunc$RANDOM; APP=func-ai200-$RANDOM; COSMOS=cosmos-ai200-$RANDOM
az group create --name $RG --location $LOC
az storage account create --name $ST --resource-group $RG --location $LOC --sku Standard_LRS
az cosmosdb create --name $COSMOS --resource-group $RG --capabilities EnableServerless
az cosmosdb sql database create --account-name $COSMOS --resource-group $RG --name shop
az cosmosdb sql container create --account-name $COSMOS --resource-group $RG --database-name shop \
  --name orders --partition-key-path /customerId

az functionapp list-flexconsumption-runtimes --location $LOC --runtime dotnet-isolated -o table   # pick a supported version
az functionapp create --name $APP --resource-group $RG --storage-account $ST \
  --flexconsumption-location $LOC --runtime dotnet-isolated --runtime-version 10.0 \
  --instance-memory 2048 --maximum-instance-count 40

# identity-based connections: system-assigned identity + roles
az functionapp identity assign --name $APP --resource-group $RG
PID=$(az functionapp identity show --name $APP --resource-group $RG --query principalId -o tsv)
az role assignment create --assignee $PID --role "Storage Blob Data Owner" \
  --scope $(az storage account show --name $ST --resource-group $RG --query id -o tsv)
az cosmosdb sql role assignment create --account-name $COSMOS --resource-group $RG \
  --role-definition-name "Cosmos DB Built-in Data Contributor" --principal-id $PID \
  --scope "/"                                              # data-plane RBAC, not az role assignment
az functionapp config appsettings set --name $APP --resource-group $RG --settings \
  "CosmosConnection__accountEndpoint=$(az cosmosdb show --name $COSMOS --resource-group $RG --query documentEndpoint -o tsv)"

cd OrdersApi && func azure functionapp publish $APP
KEY=$(az functionapp keys list --name $APP --resource-group $RG --query functionKeys.default -o tsv)
curl -s -X POST "https://$APP.azurewebsites.net/api/orders?code=$KEY" -H "Content-Type: application/json" \
     -d '{"customerId":"c-42","amount":1500}'
```

> Flex'te `--runtime-version` ve dağıtım depolama kimliği seçenekleri CLI sürümüne göre değişir; `az functionapp create --help` ve `list-flexconsumption-runtimes` çıktısını esas al.

**c) Python v2 eşdeğeri** (`function_app.py`):

```python
import json, uuid, azure.functions as func

app = func.FunctionApp(http_auth_level=func.AuthLevel.FUNCTION)

@app.route(route="orders", methods=["POST"])
@app.cosmos_db_output(arg_name="doc", database_name="shop", container_name="orders",
                      connection="CosmosConnection")
def create_order(req: func.HttpRequest, doc: func.Out[func.Document]) -> func.HttpResponse:
    body = req.get_json()
    order = {"id": str(uuid.uuid4()), "customerId": body["customerId"], "amount": body["amount"]}
    doc.set(func.Document.from_dict(order))
    return func.HttpResponse(json.dumps(order), status_code=201, mimetype="application/json")

@app.service_bus_queue_trigger(arg_name="msg", queue_name="order-processing",
                               connection="ServiceBusConnection")
def process_order(msg: func.ServiceBusMessage) -> None:
    print("order:", msg.get_body().decode())

@app.timer_trigger(schedule="0 */5 * * * *", arg_name="timer", run_on_startup=False)
def every_five_minutes(timer: func.TimerRequest) -> None:
    print("tick")
```

**d) Temizlik:**

```bash
az group delete --name $RG --yes --no-wait
```

> **Alıştırma.** `ProcessOrder` function'ına bilerek `throw new InvalidOperationException()` ekle, kuyruğa bir mesaj gönder ve mesajın kaç kez yeniden denendiğini ve nereye düştüğünü gözlemle (→ [C2.9](../09-service-bus/README.md) DLQ).
>
> <details><summary>Çözüm</summary>
>
> İşlenmeyen exception → Functions host mesajı **abandon** eder → kuyruk teslim sayısını artırır → **MaxDeliveryCount** (varsayılan 10) aşılınca mesaj `order-processing/$deadletterqueue` alt kuyruğuna `MaxDeliveryCountExceeded` nedeniyle düşer. Bu *Service Bus* davranışıdır; Functions'ın `host.json` içindeki `retry` ilkesi ise function'ı aynı örnekte tekrar çalıştırır — ikisini karıştırma.
> </details>

---

## 4. Gerçek dünya senaryosu

Bir e-ticaret şirketi sipariş akışını Functions ile kuruyor:

| Parça | Seçim | Neden |
|---|---|---|
| `POST /api/orders` | **HTTP trigger** + **Cosmos DB output binding**, function key | Kodsuz persist; Easy Auth/APIM ile kullanıcı doğrulama |
| Ödeme sonrası iş akışı | **Service Bus trigger** (`order-processing`) | Yük dalgalanmasını tamponlar, DLQ ile zehirli mesaj izolasyonu |
| Stok bilgisinin okunması | **Cosmos DB input binding** | Bağlantı kodu yok |
| Gece raporu | **Timer trigger** (`0 0 2 * * *`) | Sunucu yönetimi yok |
| Ortam | **Flex Consumption** (VNet: özel Cosmos/Service Bus endpoint'leri; scale-to-zero) | Gece trafik yokken maliyet ~0, kampanyada hızlı ölçek |
| Gizli bilgiler | **Managed identity** (`__fullyQualifiedNamespace`, `__accountEndpoint`), Key Vault referansı | Secret rotasyon yükü yok |
| Dağıtım | GitHub Actions + OIDC | Sıfır statik credential |

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Kod tamamlama (attribute/ayar adı), senaryo → plan, "trigger mı binding mi?" ayrımı.

| Senaryodaki ifade | Cevap |
|---|---|
| "Function'ı başlatan olay" | **Trigger** (function başına tam bir tane) |
| "Sonucu kuyruğa/DB'ye yaz, ek SDK kodu yazma" | **Output binding** |
| "VNet entegrasyonu **ve** scale-to-zero" | **Flex Consumption** |
| "Soğuk başlangıç olmasın, VNet, slot" | **Premium** |
| "Mevcut App Service planında çalış" | **Dedicated** |
| "Secret saklamadan Service Bus'a bağlan" | `<önek>__fullyQualifiedNamespace` + *Data Receiver* rolü |
| "Cosmos DB'ye identity ile bağlan" | `<önek>__accountEndpoint` + **Cosmos DB data-plane** rolü |
| "Storage'a identity ile bağlan" | `AzureWebJobsStorage__accountName` + Blob/Queue/Table *Data* rolleri |
| "Her 5 dakikada bir çalış" | Timer: `0 */5 * * * *` (**6 alan**) |
| "Yerel ayarlar nerede?" | `local.settings.json` (dağıtılmaz) |
| "Anahtarsız herkese açık endpoint" | `AuthorizationLevel.Anonymous` |

**Tuzaklar:**
- **Trigger vs binding:** Function'ı sadece trigger başlatır; input/output binding başlatmaz. Bir function'ın iki trigger'ı olamaz.
- **Cosmos DB RBAC:** Cosmos'ta identity erişimi Azure RBAC rolü ile değil **kendi data-plane rol atamasıyla** verilir; `Contributor` rolü veriyi okutmaz.
- **`__` ayırıcısı:** `MyConn:fullyQualifiedNamespace` yalnızca Key Vault/App Configuration kaynaklı ayarlarda; app settings'te **`__`**.
- **Klasik Consumption'da VNet yok**; "VNet + scale-to-zero" = Flex.
- **Timer** ifadesi 6 alanlıdır (Linux cron'daki 5 alan değil); saat dilimi varsayılan UTC (`WEBSITE_TIME_ZONE` ile değişir).
- **`function.json`**: Python v1 ve script dillerinde binding tanımı; **Python v2'de decorator**, .NET isolated'da **attribute** kullanılır.
- Function key bir **kimlik doğrulama** değildir.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| In-process modelle yeni proje başlatmak | **Isolated worker** (in-process desteği 10 Kasım 2026'da biter, Flex'te yok) |
| `local.settings.json`'u commit etmek | Secret içerir; `.gitignore`'da tut; Azure'da app settings / Key Vault |
| Function içinde `new ServiceBusClient(cs)` ve `new CosmosClient(...)` yazmak (basit durumda) | Binding kullan; karmaşık durumda client'ı **DI ile singleton** tut, istek başına yaratma |
| HTTP function'da uzun iş yapmak | Kuyruğa at, `202 Accepted` dön; HTTP yanıtı ~230 sn'de kesilir |
| İşlenmeyen exception'ı yutmak | Exception fırlatılmazsa mesaj tamamlanmış sayılır; poison mesaj DLQ'ya gitmez |
| Identity için rolü atayıp ayar adını yanlış yazmak (`Connection__namespace`) | `__fullyQualifiedNamespace` / `__accountEndpoint` / `__accountName` |
| Timer'ı 5 alanlı cron ile yazmak | 6 alanlı NCRONTAB |
| Consumption'da VNet beklemek | Flex veya Premium |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Flex Consumption **always ready** instance | Soğuk başlangıç yok / çok kısa | Kullanılmasa da ücretlenen instance |
| 2048 → 4096 MB instance | Daha fazla bellek/CPU | Birim süre maliyeti ↑ |
| Yüksek `maxConcurrentCalls` / `maxScale` | Verim ↑ | Aşağı akış servisi (Cosmos RU, SQL bağlantısı) boğulabilir; **maximum instance count** ile sınırla |
| Premium | Sıfır soğuk başlangıç, slot, VNet | En az 1 instance sürekli ücretli |
| Binding'ler | Az kod | Esneklik azalır (özel retry, toplu işlem, bağlantı havuzu denetimi için SDK) |
| ASP.NET Core entegrasyonu | Tanıdık `HttpRequest`/`IActionResult` | Biraz daha büyük ayak izi |
| `host.json` `retry` | Geçici hatalarda basit tekrar | Service Bus gibi kendi teslim sayacı olan kaynaklarda **çift retry**; idempotency şart |

Functions **at-least-once** çalışır: aynı mesaj iki kez işlenebilir → function'ı **idempotent** yaz.

---

## 8. Kaynaklar

- [AI-200 study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [Azure Functions overview](https://learn.microsoft.com/en-us/azure/functions/functions-overview) · [Triggers and bindings concepts](https://learn.microsoft.com/en-us/azure/azure-functions/functions-triggers-bindings)
- [Guide for .NET isolated worker model](https://learn.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-process-guide) · [Migrate from in-process to isolated](https://learn.microsoft.com/en-us/azure/azure-functions/migrate-dotnet-to-isolated-model)
- [Python v2 programming model](https://learn.microsoft.com/en-us/azure/azure-functions/functions-reference-python)
- [Hosting plans comparison](https://learn.microsoft.com/en-us/azure/azure-functions/functions-scale) · [Flex Consumption plan](https://learn.microsoft.com/en-us/azure/azure-functions/flex-consumption-plan)
- [Identity-based connections](https://learn.microsoft.com/en-us/azure/azure-functions/functions-reference#configure-an-identity-based-connection) · [Service Bus trigger](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-service-bus-trigger)
- [host.json reference](https://learn.microsoft.com/en-us/azure/azure-functions/functions-host-json) · [Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- Emeklilik duyuruları: in-process model (10 Kasım 2026) ve Linux Consumption (30 Eylül 2028) — Azure Updates / Functions hosting dokümanı
