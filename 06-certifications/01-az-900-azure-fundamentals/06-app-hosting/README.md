# C1.6 Uygulama Barındırma: App Service, Container'lar, Azure Virtual Desktop, Functions

> 📁 `06-certifications/01-az-900-azure-fundamentals/06-app-hosting/` · Sınav alanı: **Describe Azure architecture and services (%35-40)** · Önkoşul: [C1.3](../03-cloud-service-types/README.md), C1.5 · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe Azure compute and networking services* — compute türleri (container'lar, functions) · VM seçenekleri içinde **Azure Virtual Desktop** · **uygulama barındırma seçenekleri** (web apps, container'lar, VM'ler). Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

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

Bir uygulamayı Azure'da çalıştırmanın tek yolu VM değildir (VM'ler → C1.5). Yönetim yükü azaldıkça kontrol de azalır (→ C1.3):

| Seçenek | Hizmet türü | Ne çalıştırırsın? | Tipik kullanım |
|---|---|---|---|
| **Virtual Machines** | IaaS | Tam bir işletim sistemi | OS kontrolü, lift-and-shift (→ C1.5) |
| **Azure App Service** | PaaS | Web uygulaması, REST API, mobil backend, WebJobs | OS yönetmeden web/API barındırma |
| **Azure Container Instances (ACI)** | PaaS | Tek container (veya küçük container grubu) | Hızlı, basit, orkestrasyonsuz çalıştırma |
| **Azure Container Apps** | PaaS (serverless) | Container'lardan oluşan mikroservisler | Otomatik ölçek (sıfıra kadar), HTTP ingress |
| **Azure Kubernetes Service (AKS)** | PaaS (yönetilen Kubernetes) | Çok sayıda container'ın orkestrasyonu | Büyük mikroservis platformu, en fazla kontrol |
| **Azure Functions** | PaaS (serverless) | Olay tetiklemeli kısa kod | Zamanlayıcı, kuyruk, HTTP, dosya yükleme tetikleyicisi |
| **Azure Virtual Desktop (AVD)** | Masaüstü sanallaştırma (VM'ler üzerinde) | Windows masaüstü ve uygulamaları | Uzaktan çalışan kullanıcılara bulut masaüstü |

Bu konunun C01 projesinde kodu yoktur; pratik kısım `az` CLI ve `dotnet`/Core Tools komutlarından oluşur (3.6).

---

## 2. Neden?

- **Sınav:** "Hangi barındırma seçeneği?" soruları *Describe Azure architecture and services* alanının (%35-40) en sık senaryo sorularındandır. Anahtar ifadeyi ("OS yönetmeden", "tek container", "orkestrasyon", "zamanlayıcıyla", "uzaktan masaüstü") ürüne çevirebilmek gerekir.
- **Gerçek işte:** Ekibin bir .NET API'sini nerede barındıracağı; maliyet modelini, ölçekleme biçimini, deployment sürecini ve operasyon yükünü belirler. Çoğu kurumsal web uygulaması için ilk soru "App Service yetmez mi?" olmalıdır.
- **Geliştirici yolunda:** Container'lar ve AKS'nin geliştirici derinliği → C2.1–C2.3; Functions'ın geliştirici derinliği → C2.

---

## 3. Nasıl?

### 3.1 Azure App Service (PaaS)

**App Service**, web uygulamalarını, REST API'leri ve mobil backend'leri **işletim sistemi yönetmeden** barındıran HTTP tabanlı PaaS servisidir.

- **Diller/platformlar:** .NET, Java, Node.js, Python, PHP; **Windows ve Linux**; özel container image'ı da çalıştırabilir.
- **Uygulama türleri:** Web Apps, **API Apps** (REST API), mobil backend, **WebJobs** (aynı App Service içinde arka plan işi/script).
- **Platform özellikleri:**
  - Otomatik OS ve runtime yaması, yerleşik yük dengeleme
  - **Autoscale** (instance sayısını kurala göre artır/azalt — elasticity → C1.2)
  - **Deployment slot**'ları (ör. `staging` slotuna dağıt, test et, üretimle **swap** et — kesintisiz yayın)
  - CI/CD entegrasyonu (GitHub Actions, Azure DevOps), ZIP deploy
  - Özel domain ve TLS sertifikası, yerleşik kimlik doğrulama (Entra ID, sosyal hesaplar → C1.9)
- **App Service plan:** uygulamanın çalıştığı **compute** kaynağıdır ve **ücretlendirilen birimdir** (katman + instance sayısı). Birden çok uygulama aynı planı paylaşabilir — plandaki kaynakları da paylaşırlar. Katmanlar kabaca: Free/Shared (deneme, SLA yok) → Basic → Standard → Premium → Isolated (ayrılmış ortam). Slot ve autoscale gibi özellikler belirli katmanlardan itibaren gelir.

> Uygulama ayarları (**Application settings**) ve connection string'ler, uygulamaya **environment variable** olarak verilir; ASP.NET Core bunları `IConfiguration` üzerinden okur. Şifreleri koda/`appsettings.json`'a değil bu ayarlara (daha iyisi Key Vault referansına) koymak buradaki temel alışkanlıktır.

### 3.2 Container'lar: ACI, Container Apps, AKS

**Container vs VM:**

| | Virtual machine | Container |
|---|---|---|
| Neyi sanallaştırır? | **Donanımı** — her VM'in kendi tam işletim sistemi var | **İşletim sistemini** — container'lar host'un çekirdeğini paylaşır |
| Boyut | GB'lar | MB'lar |
| Başlama süresi | Dakikalar | Saniyeler |
| Taşınabilirlik | İmaj ağır, sağlayıcıya bağlı | Aynı image her yerde (laptop, test, bulut) aynı çalışır |
| İzolasyon | Güçlü (ayrı çekirdek) | Daha hafif (çekirdek paylaşımı) |

Azure'daki container seçenekleri:

| Servis | Ne zaman? | Öne çıkan nitelikler |
|---|---|---|
| **Azure Container Instances (ACI)** | **Tek bir container'ı** en hızlı ve en basit biçimde çalıştırmak; kısa işler, test, toplu görev | VM veya orkestratör yönetmek yok; **saniye bazında** ücret; public IP/DNS adı verilebilir; **orkestrasyon yok** (otomatik ölçek, servis keşfi, rolling update yok) |
| **Azure Container Apps** | Container'lardan oluşan **mikroservis** ve API'ler; olay tetiklemeli container işleri | **Serverless**: altyapı yok, otomatik ölçek (**sıfıra kadar**), HTTP ingress, trafik bölme, revizyonlar; Kubernetes'in karmaşıklığını gizler |
| **Azure Kubernetes Service (AKS)** | **Çok sayıda** container'ın orkestrasyonu; Kubernetes API'sine tam erişim gereken platformlar | Yönetilen Kubernetes: control plane Microsoft'ta, node'lar (VM'ler) senin aboneliğinde; en fazla kontrol, en fazla bilgi gerektirir |

> Container image'ları genellikle **Azure Container Registry (ACR)**'de saklanır *(bağlam)*. App Service de container çalıştırabilir.

### 3.3 Azure Virtual Desktop (AVD)

**Azure Virtual Desktop**, Azure'da çalışan bir **masaüstü ve uygulama sanallaştırma** servisidir: kullanıcılar herhangi bir cihazdan (Windows, macOS, iOS, Android, tarayıcı) bulutta çalışan bir Windows masaüstüne veya yalnızca belirli uygulamalara (RemoteApp) bağlanır.

- **Windows 11 / Windows 10 Enterprise multi-session:** birden çok kullanıcı **aynı VM'i** eşzamanlı paylaşır, tam Windows 11/10 deneyimini alır — maliyeti düşürür. (Windows Server tabanlı oturum host'ları da desteklenir.)
- **Kimlik ve güvenlik:** Microsoft Entra ID ile kimlik doğrulama, **MFA** ve Conditional Access (→ C1.9); uygulama ve **veri cihazda değil bulutta** kalır — kaybolan bir dizüstü veri sızıntısı demek değildir.
- **Yönetim:** masaüstleri ve kullanıcı atamaları merkezden yönetilir; altta yatan oturum host'ları senin aboneliğindeki VM'lerdir (ölçeklenebilir, gerektiğinde kapatılabilir).
- Tipik senaryolar: uzaktan çalışma, geçici/sözleşmeli personel, güvenlik gereği verinin cihaza inmemesi, ağır uygulamaları zayıf cihazlarda çalıştırma.

### 3.4 Azure Functions

**Azure Functions**, **olay tetiklemeli**, kısa süreli kod parçalarını sunucu yönetmeden çalıştıran serverless compute servisidir (serverless kavramı → C1.3).

- **Tetikleyiciler (trigger):** HTTP isteği, **zamanlayıcı** (cron), kuyruk mesajı (Storage Queue, Service Bus), **blob yüklemesi**, Event Grid/Event Hubs olayı, Cosmos DB değişikliği…
- **Binding'ler** *(bağlam)*: girdi/çıktı bağlantılarını (ör. "sonucu bir kuyruğa yaz") kod yazmadan bildirmek.
- **Stateless** (varsayılan; her çağrı bağımsız) ve **stateful** — **Durable Functions** ile uzun süren, adımları ve durumu olan iş akışları (orkestrasyon).
- **Ölçek ve ücret:** yüke göre otomatik ölçek; serverless planlarda yalnızca çalıştığı süre ve çağrı kadar ücret. Microsoft yeni serverless uygulamalar için **Flex Consumption** planını öneriyor; Linux üzerindeki klasik Consumption barındırması **30 Eylül 2028**'de emekliye ayrılacak (Windows Consumption bu duyurunun dışında). Sürekli yük veya VNet/ön ısıtma ihtiyacı için Premium ve Dedicated (App Service plan) seçenekleri de var.

**Functions vs App Service vs Logic Apps:**

| İhtiyaç | Seçim |
|---|---|
| Sürekli çalışan web uygulaması/API | App Service |
| Bir olaya tepki veren, kısa süreli **kod** | Functions |
| **Kod yazmadan**, görsel tasarımcı ve hazır connector'larla iş akışı | Logic Apps |

### 3.5 Barındırma karar ağacı

```
OS üzerinde tam kontrol / özel yazılım / aynen taşıma?        → Virtual Machines (→ C1.5)
Masaüstü veya Windows uygulamalarını uzaktan sunmak?          → Azure Virtual Desktop
Olayla tetiklenen kısa kod (zamanlayıcı, kuyruk, blob, HTTP)? → Azure Functions
Kod yok, görsel iş akışı/entegrasyon?                         → Logic Apps
Hazır web/API platformu, OS yönetmeden?                       → App Service
Container:
   tek container, hızlı, orkestrasyonsuz                      → Container Instances
   mikroservisler, serverless, sıfıra ölçek                   → Container Apps
   çok sayıda container, tam Kubernetes kontrolü              → AKS
```

### 3.6 Pratik — App Service, Container Instances ve yerel Function

> Bütçe alarmın kurulu olsun (→ C1.1). App Service **F1 (ücretsiz)** katmanda; ACI saniye bazında ücretlenir (birkaç dakikalık deneme kuruşlar mertebesindedir); yerel Function ücretsizdir. **Sonunda `az group delete` şart.**

**(a) Todo API'yi App Service'e deploy etmek**

```bash
# Küçük bir .NET Web API (repoda proje oluşturmuyoruz; geçici bir klasörde)
dotnet new webapi -n TodoApi && cd TodoApi

az login
az group create --name rg-az900-c16 --location westeurope

# az webapp up: plan + web app oluşturur, kodu derleyip ZIP deploy eder
APP=todo-c16-$RANDOM
az webapp list-runtimes --os linux --output table            # geçerli runtime değerleri
az webapp up --resource-group rg-az900-c16 --name $APP \
  --sku F1 --os-type linux --runtime "DOTNETCORE:10.0"

curl https://$APP.azurewebsites.net/weatherforecast          # şablonun örnek endpoint'i
```

**(b) Ayarların environment variable olarak `IConfiguration`'a akması**

```bash
# Application setting: "__" (çift alt çizgi) → IConfiguration'da ":" olur → Todo:Greeting
az webapp config appsettings set --resource-group rg-az900-c16 --name $APP \
  --settings Todo__Greeting="Merhaba App Service"

# Connection string: uygulamaya SQLAZURECONNSTR_TodoDb env var'ı olarak gelir;
# ASP.NET Core bunu builder.Configuration.GetConnectionString("TodoDb") ile okur
az webapp config connection-string set --resource-group rg-az900-c16 --name $APP \
  --connection-string-type SQLAzure --settings TodoDb="Server=tcp:ornek.database.windows.net;Database=todo;"

az webapp config appsettings list --resource-group rg-az900-c16 --name $APP --output table
```

Uygulama tarafında okumak için `Program.cs`'e tek satırlık bir endpoint eklemek yeterli (deneme kodu, yeniden `az webapp up` ile dağıtılır):

```csharp
app.MapGet("/config", (IConfiguration c) => new { greeting = c["Todo:Greeting"], db = c.GetConnectionString("TodoDb") is not null });
```

**(c) Log stream — canlı log**

```bash
az webapp log config --resource-group rg-az900-c16 --name $APP \
  --docker-container-logging filesystem --level information   # Linux uygulamada konsol log'larını aç
az webapp log tail --resource-group rg-az900-c16 --name $APP   # Ctrl+C ile çık; başka terminalden istek at
```

**(d) Aynı işi bir container ile: Azure Container Instances**

```bash
# Örnek bir web container'ı tek komutla — VM yok, plan yok, orkestratör yok
az container create --resource-group rg-az900-c16 --name aci-c16 \
  --image mcr.microsoft.com/azuredocs/aci-helloworld \
  --os-type Linux --cpu 1 --memory 1.5 --ports 80 \
  --dns-name-label aci-c16-$RANDOM
az container show --resource-group rg-az900-c16 --name aci-c16 --query "{fqdn:ipAddress.fqdn,state:instanceView.state}" --output table
az container logs --resource-group rg-az900-c16 --name aci-c16
```

> Kendi Todo API'ni container olarak çalıştırmak için image'ı bir registry'ye (ör. Azure Container Registry) push etmen gerekir; .NET SDK `dotnet publish /t:PublishContainer` ile Dockerfile'sız image üretebilir *(bağlam — container derinliği → C2.1)*.

**(e) HTTP tetiklemeli bir Azure Function — yerelde, Core Tools ile (ücretsiz)**

```bash
# Azure Functions Core Tools v4 kurulu olmalı (func --version)
func init FuncC16 --worker-runtime dotnet-isolated
cd FuncC16
func new --name Hello --template "HTTP trigger" --authlevel anonymous
func start                                       # http://localhost:7071/api/Hello
curl "http://localhost:7071/api/Hello?name=az900"
```

Gözlem: Function için ne bir sunucu ne bir plan seçtin; yalnızca **tetikleyici** (HTTP) ve **kod** var. Azure'a yayınlamak istersen `func azure functionapp publish <ad>` kullanılır (bir Function App ve storage account gerektirir).

**Temizlik**

```bash
az group delete --name rg-az900-c16 --yes --no-wait
```

<details><summary>Kendini sına: hangi adım hangi kavramı gösterdi?</summary>

| Adım | Kavram |
|---|---|
| `az webapp up` → OS seçmeden uygulama yayında | App Service = PaaS; plan = ücretlendirilen compute |
| `Todo__Greeting` → `Todo:Greeting` | Application settings env var olarak `IConfiguration`'a akar |
| `az webapp log tail` | Platformun sunduğu yerleşik izleme (management *of* the cloud → C1.2) |
| `az container create` | ACI: tek container, orkestrasyonsuz, saniye bazında ücret |
| `func new … HTTP trigger` | Functions: tetikleyici + kod, sunucu yok |
| `az group delete` | Resource group yaşam döngüsü (→ C1.4) |
</details>

---

## 4. Gerçek dünya senaryosu

Bir sigorta acentesi ağı dijitalleşme projesinde iş yüklerini şöyle dağıtıyor:

| İş yükü | Seçim | Gerekçe |
|---|---|---|
| Müşteri portalı ve mobil uygulamanın REST API'si | **App Service** (Standard, autoscale, `staging` slotu) | Ekip .NET kodu yazıyor; OS yönetmek istemiyor; slot swap ile kesintisiz yayın |
| Poliçe PDF'lerini gece üreten iş | **Azure Functions** (zamanlayıcı tetikleyici) | Günde bir kez birkaç dakika çalışıyor; boşta ücret yok |
| Yüklenen hasar fotoğraflarını işleme | **Functions** (blob tetikleyici) | Olay tetiklemeli, düzensiz yük |
| Dört mikroservisten oluşan teklif motoru | **Container Apps** | Container'lar hazır; Kubernetes ekibi yok; yoğunlukta otomatik ölçek, gece sıfıra iner |
| Aylık bir kez çalışan veri dönüştürme container'ı | **Container Instances** | Tek container, birkaç dakika; orkestrasyona gerek yok |
| 400 acente çalışanının eski Windows masaüstü uygulaması | **Azure Virtual Desktop** (Windows 11 multi-session) | Uygulama ve müşteri verisi bulutta kalıyor; acentelerin cihazları önemsizleşiyor; MFA zorunlu |
| Lisansı belirli bir OS sürümüne bağlı eski hesaplama motoru | **VM** (→ C1.5) | OS üzerinde tam kontrol gerekiyor |

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Neredeyse tamamen senaryo: "X yapmak istiyorsunuz, **hangi** Azure servisini önerirsiniz?" ve evet/hayır tabloları ("Azure Functions yalnızca HTTP ile tetiklenebilir" → Hayır).

| Senaryodaki ifade | Cevap |
|---|---|
| "Web uygulaması/API, **OS yönetmeden**, otomatik yama ve autoscale" | **App Service** |
| "Aynı compute'u paylaşan birden çok web uygulaması" / "App Service'te ne ücretlendirilir?" | **App Service plan** |
| "**Tek bir container**'ı hızlıca, VM/orkestratör yönetmeden" | **Container Instances** |
| "**Binlerce container**, orkestrasyon, Kubernetes" | **AKS** |
| "Serverless container, mikroservis, **sıfıra kadar ölçek**" | **Container Apps** |
| "**Zamanlayıcıyla** / kuyruk mesajıyla / dosya yüklenince çalışan kısa kod" | **Azure Functions** |
| "Uzun süren, **durum tutan** fonksiyon iş akışı" | **Durable Functions** |
| "Kod yazmadan iş akışı" | **Logic Apps** |
| "Kullanıcılar her cihazdan **Windows masaüstüne** erişsin; veri cihazda kalmasın" | **Azure Virtual Desktop** |
| "Birden çok kullanıcı **aynı VM'de** Windows 11 oturumu" | AVD **multi-session** |
| "Container **işletim sistemini**, VM **donanımı** sanallaştırır" | Doğru |

**Tuzaklar:**
- ACI ile AKS: "en basit/hızlı tek container" → ACI; "orkestrasyon/ölçek/çok container" → AKS (veya serverless isteniyorsa Container Apps).
- Functions ile Logic Apps: **kod** → Functions; **görsel/low-code** → Logic Apps.
- App Service ile VM: soru "işletim sistemine erişim" diyorsa VM; "yönetim yükü az" diyorsa App Service.
- AVD bir "uzak masaüstü protokolü" değil, **yönetilen bir masaüstü sanallaştırma servisidir**; kimlik Entra ID ile yapılır.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "App Service'te uygulama başına ödenir" | Ücretlendirilen birim **App Service plan**'dır; aynı plandaki uygulamalar ek compute ücreti doğurmaz ama kaynakları paylaşır |
| "Container = küçük VM" | Container OS'u sanallaştırır, host çekirdeğini paylaşır; VM'in kendi OS'u vardır |
| "ACI ile mikroservis platformu kurarım" | ACI'de orkestrasyon yok; mikroservis için Container Apps veya AKS |
| "Functions yalnızca HTTP ile çalışır" | Zamanlayıcı, kuyruk, blob, Event Grid … birçok tetikleyici var |
| "Functions her zaman en ucuzu" | Sürekli yüksek yükte sabit bir plan ucuzlayabilir (→ C1.3); Durable olmayan fonksiyonlar uzun iş için uygun değildir |
| "AVD kullanıcının bilgisayarına Windows kurar" | Masaüstü Azure'daki VM'lerde çalışır; cihaz yalnızca bağlantı istemcisidir |
| Connection string'i `appsettings.json`'a yazıp repoya koymak | App Service ayarları / Key Vault referansı; ayarlar env var olarak gelir |
| Laboratuvardan sonra kaynakları bırakmak | `az group delete` — özellikle plan ve ACI süre bazlı ücretlenir |

---

## 7. Performans & trade-off

| Seçenek | Kazanç | Bedel |
|---|---|---|
| VM | Tam kontrol, her yazılım çalışır | Tüm OS/yama/ölçek yükü sende |
| App Service | Hızlı yayın, yama yok, slot, autoscale | Platform kısıtları; plan boşta da ücretlenir |
| Container Instances | En basit container çalıştırma, saniye bazında ücret | Orkestrasyon, otomatik ölçek, rolling update yok |
| Container Apps | Serverless container, sıfıra ölçek, mikroservis özellikleri | Kubernetes API'sine tam erişim yok; cold start |
| AKS | Tam Kubernetes esnekliği, büyük ölçek | Öğrenme eğrisi ve operasyon yükü (node'lar, yükseltmeler) en yüksek |
| Functions | Olay tetiklemeli, boşta 0 ücret, otomatik ölçek | Cold start, çalışma süresi sınırları, durum için Durable gerekir |
| AVD | Veri bulutta, merkezi yönetim, her cihazdan erişim | Ağ gecikmesine duyarlı kullanıcı deneyimi; oturum host'larının maliyeti ve boyutlandırması |

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure compute and networking services](https://learn.microsoft.com/en-us/training/modules/describe-azure-compute-networking-services/)
- [App Service overview](https://learn.microsoft.com/en-us/azure/app-service/overview) · [App Service plans](https://learn.microsoft.com/en-us/azure/app-service/overview-hosting-plans) · [Configure an App Service app (app settings, connection strings)](https://learn.microsoft.com/en-us/azure/app-service/configure-common)
- [Azure Container Instances](https://learn.microsoft.com/en-us/azure/container-instances/container-instances-overview) · [Azure Container Apps](https://learn.microsoft.com/en-us/azure/container-apps/overview) · [AKS](https://learn.microsoft.com/en-us/azure/aks/what-is-aks)
- [What is Azure Virtual Desktop?](https://learn.microsoft.com/en-us/azure/virtual-desktop/overview)
- [Azure Functions overview](https://learn.microsoft.com/en-us/azure/azure-functions/functions-overview) · [Hosting options (Flex Consumption …)](https://learn.microsoft.com/en-us/azure/azure-functions/functions-scale) · [Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- [`az webapp up`](https://learn.microsoft.com/en-us/cli/azure/webapp#az-webapp-up) · [`az container create`](https://learn.microsoft.com/en-us/cli/azure/container#az-container-create)
