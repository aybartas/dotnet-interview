[← AZ-900 İçindekiler](../README.md) · [📝 Quiz](quiz.md) · [← Önceki konu](../2.2-compute-virtual-machines/README.md)

## 2.3 Uygulama Barındırma: App Service, Container'lar, Functions, AVD

> **Sınav Alanı:** Describe Azure architecture and services (%35-40) · **Konu Kodu:** C1.6
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe Azure compute and networking services* — compute türleri (container'lar, functions) · VM seçenekleri içinde **Azure Virtual Desktop** · **uygulama barındırma seçenekleri** (web apps, container'lar, VM'ler). Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

### 1. Ne?

Bir uygulamayı Azure'da çalıştırmanın tek yolu VM değildir (VM'ler → C1.5). Yönetim yükü azaldıkça kontrol de azalır (→ C1.3):

- **Virtual Machines**
  - Hizmet türü: IaaS · Ne çalıştırırsın?: Tam bir işletim sistemi
  - Tipik kullanım: OS kontrolü, lift-and-shift (→ C1.5)
- **Azure App Service**
  - Hizmet türü: PaaS
  - Ne çalıştırırsın?: Web uygulaması, REST API, mobil backend, WebJobs
  - Tipik kullanım: OS yönetmeden web/API barındırma
- **Azure Container Instances (ACI)**
  - Hizmet türü: PaaS
  - Ne çalıştırırsın?: Tek container (veya küçük container grubu)
  - Tipik kullanım: Hızlı, basit, orkestrasyonsuz çalıştırma
- **Azure Container Apps**
  - Hizmet türü: PaaS (serverless)
  - Ne çalıştırırsın?: Container'lardan oluşan mikroservisler
  - Tipik kullanım: Otomatik ölçek (sıfıra kadar), HTTP ingress
- **Azure Kubernetes Service (AKS)**
  - Hizmet türü: PaaS (yönetilen Kubernetes)
  - Ne çalıştırırsın?: Çok sayıda container'ın orkestrasyonu
  - Tipik kullanım: Büyük mikroservis platformu, en fazla kontrol
- **Azure Functions**
  - Hizmet türü: PaaS (serverless)
  - Ne çalıştırırsın?: Olay tetiklemeli kısa kod
  - Tipik kullanım: Zamanlayıcı, kuyruk, HTTP, dosya yükleme tetikleyicisi
- **Azure Virtual Desktop (AVD)**
  - Hizmet türü: Masaüstü sanallaştırma (VM'ler üzerinde)
  - Ne çalıştırırsın?: Windows masaüstü ve uygulamaları
  - Tipik kullanım: Uzaktan çalışan kullanıcılara bulut masaüstü

Bu konunun kodu yok; pratik kısım `az` CLI komutlarından oluşur (3.6).

---

### 2. Neden?

- **Sınav:** "Hangi barındırma seçeneği?" soruları *Describe Azure architecture and services* alanının (%35-40) en sık senaryo sorularındandır. Anahtar ifadeyi ("OS yönetmeden", "tek container", "orkestrasyon", "zamanlayıcıyla", "uzaktan masaüstü") ürüne çevirebilmek gerekir.
- **Gerçek işte:** Ekibin bir .NET API'sini nerede barındıracağı; maliyet modelini, ölçekleme biçimini, deployment sürecini ve operasyon yükünü belirler. Çoğu kurumsal web uygulaması için ilk soru "App Service yetmez mi?" olmalıdır.

---

### 3. Nasıl?

#### 3.1 Azure App Service (PaaS)

**App Service**, web uygulamalarını, REST API'leri ve mobil backend'leri **işletim sistemi yönetmeden** barındıran HTTP tabanlı PaaS servisidir.

- **Diller/platformlar:** .NET, Java, Node.js, Python, PHP; **Windows ve Linux**; özel container image'ı da çalıştırabilir.
- **Uygulama türleri:** Web Apps, **API Apps** (REST API), mobil backend, **WebJobs** (aynı App Service içinde arka plan işi/script).
- **Platform özellikleri:**
  - Otomatik OS ve runtime yaması
  - **Autoscale** (instance sayısını kurala göre artır/azalt — elasticity → C1.2)
  - Özel domain ve TLS sertifikası, yerleşik kimlik doğrulama (Entra ID, sosyal hesaplar → C1.9)
- **App Service plan:** uygulamanın çalıştığı **compute** kaynağıdır ve **ücretlendirilen birimdir** (katman + instance sayısı). Birden çok uygulama aynı planı paylaşabilir — plandaki kaynakları da paylaşırlar. Katmanlar kabaca: Free/Shared (deneme) → Basic → Standard → Premium → Isolated (ayrılmış ortam). Slot ve autoscale gibi özellikler belirli katmanlardan itibaren gelir.

#### 3.2 Container'lar: ACI, Container Apps, AKS

**Container vs VM:**

- **Neyi sanallaştırır?**
  - Virtual machine: **Donanımı** — her VM'in kendi tam işletim sistemi var
  - Container: **İşletim sistemini** — container'lar host'un çekirdeğini paylaşır
- **Boyut** — Virtual machine: GB'lar · Container: MB'lar
- **Başlama süresi** — Virtual machine: Dakikalar · Container: Saniyeler
- **Taşınabilirlik**
  - Virtual machine: İmaj ağır, sağlayıcıya bağlı
  - Container: Aynı image her yerde (laptop, test, bulut) aynı çalışır
- **İzolasyon** — Virtual machine: Güçlü (ayrı çekirdek) · Container: Daha hafif (çekirdek paylaşımı)

Azure'daki container seçenekleri:

- **Azure Container Instances (ACI)**
  - Ne zaman?: **Tek bir container'ı** en hızlı ve en basit biçimde çalıştırmak; kısa işler, test, toplu görev
  - Öne çıkan nitelikler: VM veya orkestratör yönetmek yok; **saniye bazında** ücret; public IP/DNS adı verilebilir; **orkestrasyon yok** (otomatik ölçek, servis keşfi, rolling update yok)
- **Azure Container Apps**
  - Ne zaman?: Container'lardan oluşan **mikroservis** ve API'ler; olay tetiklemeli container işleri
  - Öne çıkan nitelikler: **Serverless**: altyapı yok, otomatik ölçek (**sıfıra kadar**), HTTP ingress, trafik bölme, revizyonlar; Kubernetes'in karmaşıklığını gizler
- **Azure Kubernetes Service (AKS)**
  - Ne zaman?: **Çok sayıda** container'ın orkestrasyonu; Kubernetes API'sine tam erişim gereken platformlar
  - Öne çıkan nitelikler: Yönetilen Kubernetes: control plane Microsoft'ta, node'lar (VM'ler) senin aboneliğinde; en fazla kontrol, en fazla bilgi gerektirir

> App Service de container çalıştırabilir.

#### 3.3 Azure Virtual Desktop (AVD)

**Azure Virtual Desktop**, Azure'da çalışan bir **masaüstü ve uygulama sanallaştırma** servisidir: kullanıcılar herhangi bir cihazdan (Windows, macOS, iOS, Android, tarayıcı) bulutta çalışan bir Windows masaüstüne veya yalnızca belirli uygulamalara (RemoteApp) bağlanır.

- **Windows 11 / Windows 10 Enterprise multi-session:** birden çok kullanıcı **aynı VM'i** eşzamanlı paylaşır, tam Windows 11/10 deneyimini alır — maliyeti düşürür. (Windows Server tabanlı oturum host'ları da desteklenir.)
- **Kimlik ve güvenlik:** Microsoft Entra ID ile kimlik doğrulama, **MFA** ve Conditional Access (→ C1.9); uygulama ve **veri cihazda değil bulutta** kalır — kaybolan bir dizüstü veri sızıntısı demek değildir.
- **Yönetim:** masaüstleri ve kullanıcı atamaları merkezden yönetilir; altta yatan oturum host'ları senin aboneliğindeki VM'lerdir (ölçeklenebilir, gerektiğinde kapatılabilir).
- Tipik senaryolar: uzaktan çalışma, geçici/sözleşmeli personel, güvenlik gereği verinin cihaza inmemesi, ağır uygulamaları zayıf cihazlarda çalıştırma.

#### 3.4 Azure Functions

**Azure Functions**, **olay tetiklemeli**, kısa süreli kod parçalarını sunucu yönetmeden çalıştıran serverless compute servisidir (serverless kavramı → C1.3).

- **Tetikleyiciler (trigger):** HTTP isteği, **zamanlayıcı** (cron), **kuyruk mesajı** (Storage Queue), **blob yüklemesi** gibi olaylar kodu çalıştırır.
- **Ölçek ve ücret:** yüke göre otomatik ölçek; serverless planlarda yalnızca çalıştığı süre ve çağrı kadar ücret, boşta ücret yok. Sürekli yük gerekiyorsa sabit kapasiteli (App Service plan tabanlı) planlar da vardır.
- **Kısıtlar:** boştan uyanırken **cold start** gecikmesi; tek bir çağrının çalışma süresi sınırlıdır — uzun süren, sürekli çalışan işler için uygun değildir.

**VM, container ve Functions karşılaştırması:**

| | Virtual Machine | Container | Azure Functions |
|---|---|---|---|
| Ne sanallaştırılır? | Donanım (her VM'de tam OS) | İşletim sistemi (çekirdek paylaşılır) | Hiçbiri — sunucu tamamen gizli |
| Yönetim yükü | En yüksek (OS, yama) | Orta (image ve çalıştırma ortamı) | En düşük (yalnızca kod) |
| Başlama süresi | Dakikalar | Saniyeler | Anında / cold start |
| Ölçek | Elle veya scale set | Platforma göre (ACI: elle; Container Apps: otomatik) | Otomatik, sıfıra kadar |
| Ücret | Çalıştığı süre (deallocate edilene dek) | Çalıştığı süre / kaynak | Çağrı ve çalışma süresi |
| Ne zaman? | OS kontrolü, lift-and-shift | Taşınabilir, hafif paketler; mikroservis | Olay tetiklemeli kısa kod |

**Functions vs App Service:**

| İhtiyaç | Seçim |
|---|---|
| Sürekli çalışan web uygulaması/API | App Service |
| Bir olaya tepki veren, kısa süreli **kod** | Functions |

#### 3.5 Barındırma karar ağacı

```
Full control over the OS / custom software / lift-and-shift? → Virtual Machines (→ C1.5)
Serve desktops or Windows apps remotely?                     → Azure Virtual Desktop
Short event-triggered code (timer, queue, blob, HTTP)?       → Azure Functions
Ready-made web/API platform, without managing the OS?        → App Service
Container:
   single container, fast, no orchestration                  → Container Instances
   microservices, serverless, scale to zero                  → Container Apps
   many containers, full Kubernetes control                  → AKS
```

#### 3.6 Pratik — App Service ve Container Instances

> Bütçe alarmın kurulu olsun (→ C1.1). App Service **F1 (ücretsiz)** katmanında; ACI saniye bazında ücretlenir (birkaç dakikalık deneme kuruşlar mertebesindedir). **Sonunda `az group delete` şart.**

**(a) App Service: işletim sistemi seçmeden web uygulaması**

```bash
az login
az group create --name rg-az900-c16 --location westeurope

# App Service plan (the billed compute unit) + a web app on it
az webapp list-runtimes --os linux --output table            # valid runtime values
az appservice plan create --resource-group rg-az900-c16 --name plan-c16 --sku F1 --is-linux
APP=todo-c16-$RANDOM
az webapp create --resource-group rg-az900-c16 --plan plan-c16 \
  --name $APP --runtime "DOTNETCORE:10.0"
az webapp show --resource-group rg-az900-c16 --name $APP --query "{Host:defaultHostName,State:state}" --output table
# Open https://$APP.azurewebsites.net in a browser: the platform's default page is already live
```

Gözlem: işletim sistemi, disk veya yama için hiçbir şey seçmedin; **plan** ücretlendirilen compute'tur, web app onun üzerinde çalışır.

**(b) Aynı işi bir container ile: Azure Container Instances**

```bash
# A sample web container with a single command — no VM, no plan, no orchestrator
az container create --resource-group rg-az900-c16 --name aci-c16 \
  --image mcr.microsoft.com/azuredocs/aci-helloworld \
  --os-type Linux --cpu 1 --memory 1.5 --ports 80 \
  --dns-name-label aci-c16-$RANDOM
az container show --resource-group rg-az900-c16 --name aci-c16 --query "{fqdn:ipAddress.fqdn,state:instanceView.state}" --output table
az container logs --resource-group rg-az900-c16 --name aci-c16
```

**(c) Azure Functions: portaldan keşif (kod yazmadan)**

Portalda **Create a resource → Function App** sihirbazını aç ve yalnızca seçenekleri incele: hosting planı (serverless seçenekler ve sabit kapasiteli planlar), runtime, region. Oluşturduktan sonra **Create function → HTTP trigger** şablonunu seç. Gözlem: ne bir sunucu ne bir VM seçtin; yalnızca **tetikleyici** ve **kod** var. İncelemen bitince Function App'i sil.

**Temizlik**

```bash
az group delete --name rg-az900-c16 --yes --no-wait
```

<details><summary>Kendini sına: hangi adım hangi kavramı gösterdi?</summary>

| Adım | Kavram |
|---|---|
| `az appservice plan create` + `az webapp create` | App Service = PaaS; plan = ücretlendirilen compute |
| `az container create` | ACI: tek container, orkestrasyonsuz, saniye bazında ücret |
| Function App sihirbazı | Functions: tetikleyici + kod, sunucu yönetimi yok |
| `az group delete` | Resource group yaşam döngüsü (→ C1.4) |
</details>

---

### 4. Gerçek dünya senaryosu

Bir sigorta acentesi ağı dijitalleşme projesinde iş yüklerini şöyle dağıtıyor:

- **Müşteri portalı ve mobil uygulamanın REST API'si**
  - Seçim: **App Service** (Standard, autoscale)
  - Gerekçe: Ekip .NET kodu yazıyor; OS yönetmek istemiyor; yük artınca otomatik ölçek
- **Poliçe PDF'lerini gece üreten iş**
  - Seçim: **Azure Functions** (zamanlayıcı tetikleyici)
  - Gerekçe: Günde bir kez birkaç dakika çalışıyor; boşta ücret yok
- **Yüklenen hasar fotoğraflarını işleme** — Seçim: **Functions** (blob tetikleyici) · Gerekçe: Olay tetiklemeli, düzensiz yük
- **Dört mikroservisten oluşan teklif motoru**
  - Seçim: **Container Apps**
  - Gerekçe: Container'lar hazır; Kubernetes ekibi yok; yoğunlukta otomatik ölçek, gece sıfıra iner
- **Aylık bir kez çalışan veri dönüştürme container'ı**
  - Seçim: **Container Instances**
  - Gerekçe: Tek container, birkaç dakika; orkestrasyona gerek yok
- **400 acente çalışanının eski Windows masaüstü uygulaması**
  - Seçim: **Azure Virtual Desktop** (Windows 11 multi-session)
  - Gerekçe: Uygulama ve müşteri verisi bulutta kalıyor; acentelerin cihazları önemsizleşiyor; MFA zorunlu
- **Lisansı belirli bir OS sürümüne bağlı eski hesaplama motoru**
  - Seçim: **VM** (→ C1.5)
  - Gerekçe: OS üzerinde tam kontrol gerekiyor

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Neredeyse tamamen senaryo: "X yapmak istiyorsunuz, **hangi** Azure servisini önerirsiniz?" ve evet/hayır tabloları ("Azure Functions yalnızca HTTP ile tetiklenebilir" → Hayır).

| Senaryodaki ifade | Cevap |
|---|---|
| "Web uygulaması/API, **OS yönetmeden**, otomatik yama ve autoscale" | **App Service** |
| "Aynı compute'u paylaşan birden çok web uygulaması" / "App Service'te ne ücretlendirilir?" | **App Service plan** |
| "**Tek bir container**'ı hızlıca, VM/orkestratör yönetmeden" | **Container Instances** |
| "**Binlerce container**, orkestrasyon, Kubernetes" | **AKS** |
| "Serverless container, mikroservis, **sıfıra kadar ölçek**" | **Container Apps** |
| "**Zamanlayıcıyla** / kuyruk mesajıyla / dosya yüklenince çalışan kısa kod" | **Azure Functions** |
| "Kullanıcılar her cihazdan **Windows masaüstüne** erişsin; veri cihazda kalmasın" | **Azure Virtual Desktop** |
| "Birden çok kullanıcı **aynı VM'de** Windows 11 oturumu" | AVD **multi-session** |
| "Container **işletim sistemini**, VM **donanımı** sanallaştırır" | Doğru |

**Tuzaklar:**
- ACI ile AKS: "en basit/hızlı tek container" → ACI; "orkestrasyon/ölçek/çok container" → AKS (veya serverless isteniyorsa Container Apps).
- Functions ile App Service: **olaya tepki veren kısa kod** → Functions; **sürekli çalışan web uygulaması/API** → App Service.
- App Service ile VM: soru "işletim sistemine erişim" diyorsa VM; "yönetim yükü az" diyorsa App Service.
- AVD bir "uzak masaüstü protokolü" değil, **yönetilen bir masaüstü sanallaştırma servisidir**; kimlik Entra ID ile yapılır.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "App Service'te uygulama başına ödenir" | Ücretlendirilen birim **App Service plan**'dır; aynı plandaki uygulamalar ek compute ücreti doğurmaz ama kaynakları paylaşır |
| "Container = küçük VM" | Container OS'u sanallaştırır, host çekirdeğini paylaşır; VM'in kendi OS'u vardır |
| "ACI ile mikroservis platformu kurarım" | ACI'de orkestrasyon yok; mikroservis için Container Apps veya AKS |
| "Functions yalnızca HTTP ile çalışır" | Zamanlayıcı, kuyruk, blob … birçok tetikleyici var |
| "Functions her zaman en ucuzu" | Sürekli yüksek yükte sabit bir plan ucuzlayabilir (→ C1.3); fonksiyonlar uzun süren işler için uygun değildir |
| "AVD kullanıcının bilgisayarına Windows kurar" | Masaüstü Azure'daki VM'lerde çalışır; cihaz yalnızca bağlantı istemcisidir |
| Laboratuvardan sonra kaynakları bırakmak | `az group delete` — özellikle plan ve ACI süre bazlı ücretlenir |

---

### 7. Performans & trade-off

- **VM** — Kazanç: Tam kontrol, her yazılım çalışır · Bedel: Tüm OS/yama/ölçek yükü sende
- **App Service**
  - Kazanç: Hızlı yayın, yama yok, autoscale
  - Bedel: Platform kısıtları; plan boşta da ücretlenir
- **Container Instances**
  - Kazanç: En basit container çalıştırma, saniye bazında ücret
  - Bedel: Orkestrasyon, otomatik ölçek, rolling update yok
- **Container Apps**
  - Kazanç: Serverless container, sıfıra ölçek, mikroservis özellikleri
  - Bedel: Kubernetes API'sine tam erişim yok; cold start
- **AKS**
  - Kazanç: Tam Kubernetes esnekliği, büyük ölçek
  - Bedel: Öğrenme eğrisi ve operasyon yükü (node'lar, yükseltmeler) en yüksek
- **Functions**
  - Kazanç: Olay tetiklemeli, boşta 0 ücret, otomatik ölçek
  - Bedel: Cold start, çalışma süresi sınırları
- **AVD**
  - Kazanç: Veri bulutta, merkezi yönetim, her cihazdan erişim
  - Bedel: Ağ gecikmesine duyarlı kullanıcı deneyimi; oturum host'larının maliyeti ve boyutlandırması

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure compute and networking services](https://learn.microsoft.com/en-us/training/modules/describe-azure-compute-networking-services/)
- [App Service overview](https://learn.microsoft.com/en-us/azure/app-service/overview) · [App Service plans](https://learn.microsoft.com/en-us/azure/app-service/overview-hosting-plans)
- [Azure Container Instances](https://learn.microsoft.com/en-us/azure/container-instances/container-instances-overview) · [Azure Container Apps](https://learn.microsoft.com/en-us/azure/container-apps/overview) · [AKS](https://learn.microsoft.com/en-us/azure/aks/what-is-aks)
- [What is Azure Virtual Desktop?](https://learn.microsoft.com/en-us/azure/virtual-desktop/overview)
- [Azure Functions overview](https://learn.microsoft.com/en-us/azure/azure-functions/functions-overview) · [Hosting options](https://learn.microsoft.com/en-us/azure/azure-functions/functions-scale)
- [`az webapp create`](https://learn.microsoft.com/en-us/cli/azure/webapp#az-webapp-create) · [`az container create`](https://learn.microsoft.com/en-us/cli/azure/container#az-container-create)

---

➡️ **Sıradaki:** [📝 Bu konunun quiz soruları](quiz.md)
