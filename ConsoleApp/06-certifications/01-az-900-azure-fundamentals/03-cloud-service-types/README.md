# C1.3 Cloud Hizmet Türleri: IaaS, PaaS, SaaS ve Serverless

> 📁 `ConsoleApp/06-certifications/01-az-900-azure-fundamentals/03-cloud-service-types/` · Sınav alanı: **Describe cloud concepts (%25-30)** · Önkoşul: [C1.1](../01-cloud-computing/README.md) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe cloud service types* — IaaS · PaaS · SaaS · her hizmet türünün kullanım senaryosu · (*Describe cloud computing*'den) serverless. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

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

Bulut hizmetleri, sağlayıcının yığının **ne kadarını** yönettiğine göre üç ana türe ayrılır; serverless, PaaS'ın en "yönetimsiz" biçimidir:

| | **IaaS** (Infrastructure as a Service) | **PaaS** (Platform as a Service) | **SaaS** (Software as a Service) |
|---|---|---|---|
| Ne kiralarsın | Altyapı: sanal makine, ağ, depolama | Platform: çalışma ortamı (runtime), veritabanı, ara katman | Hazır uygulama |
| Kontrol / esneklik | **En fazla** | Orta | **En az** |
| Yönetim yükü / sorumluluk | **En fazla** | Orta | **En az** |
| Sen ne yaparsın | OS, yama, runtime, uygulama, veri, ağ kuralları | Kod ve veri; uygulama yapılandırması | Kullanıcılar, veri, ayarlar |
| Kullanım senaryosu | **Lift-and-shift** taşıma, test/geliştirme ortamı, özel yazılım, depolama/yedekleme, yüksek performanslı hesap (HPC) | Web/API geliştirme, iş zekâsı/analitik, geliştirme çerçeveleri, IoT backend | E-posta ve iletişim, ofis uygulamaları, CRM/ERP, iş birliği |
| Azure örnekleri | Virtual Machines, VM Scale Sets, Virtual Network, Managed Disks | App Service, Azure SQL Database, Cosmos DB, AKS, **Functions** | Microsoft 365, Dynamics 365, Outlook.com |

Kod: `ConsoleApp` projesinde [`Sections/`](Sections/) altında 4 bölüm ve bir **ürün → hizmet türü** kataloğu ([`ServiceCatalog.cs`](ServiceCatalog.cs)). `ConsoleApp` klasöründe:

```bash
dotnet run -- C1.3 all                 # all sections
dotnet run -- C1.3 lookup sql          # which type is "SQL"? → SQL Server on a VM = IaaS, SQL Database / MI = PaaS
dotnet run -- C1.3 lookup functions    # Azure Functions → PaaS (serverless)
```

---

## 2. Neden?

- **Sınav:** "Ürün → hizmet türü" ve "senaryo → hizmet türü" eşleştirmeleri bu alanın sabit sorularıdır. Shared responsibility (C1.1) soruları da IaaS/PaaS/SaaS'ı bilmeden cevaplanamaz.
- **Gerçek işte:** Hizmet türü seçimi; ekibin ne kadar operasyon yükü taşıyacağını, ne kadar özelleştirme yapabileceğini, maliyet yapısını ve taşıma süresini belirler. Yanlış seçim ya gereksiz operasyon yükü (her şeyi VM'de çalıştırmak) ya da tıkanmış bir platform (PaaS'ın izin vermediği bir bağımlılık) demektir.
- **Sonraki konular:** VM ve scale set (C1.5), App Service/container/Functions (C1.6), depolama (C1.8) bu türlerin Azure'daki ürünleridir.

---

## 3. Nasıl?

### 3.1 Yığının hangi katmanını kim yönetir? ([`Section01`](Sections/Section01_ServiceModels.cs))

Klasik uygulama yığını gösterimi (Sen = müşteri, MS = Microsoft):

| Katman | On-prem | IaaS | PaaS | SaaS |
|---|---|---|---|---|
| Uygulama | Sen | Sen | Sen | MS |
| Veri | Sen | Sen | Sen | MS* |
| Runtime | Sen | Sen | MS | MS |
| Middleware | Sen | Sen | MS | MS |
| İşletim sistemi | Sen | Sen | MS | MS |
| Sanallaştırma | Sen | MS | MS | MS |
| Sunucular | Sen | MS | MS | MS |
| Depolama | Sen | MS | MS | MS |
| Ağ (fiziksel) | Sen | MS | MS | MS |

\* Bu klasik gösterim bir **sadeleştirmedir**: SaaS'ta verinin saklanması Microsoft'ta olsa da verinin kendisi, hesaplar ve erişim **her modelde müşterinin sorumluluğundadır**. Sınav C1.1'deki shared responsibility tablosunu temel alır.

**Pizza benzetmesi** (hatırlamak için):

| Model | Pizza | Sen ne yaparsın? |
|---|---|---|
| On-prem | Evde sıfırdan yap | Fırın, malzeme, pişirme, masa — hepsi |
| IaaS | Hazır hamur al, evde pişir | Tarif, pişirme, servis (mutfak/altyapı hazır) |
| PaaS | Eve sipariş | Masa, içecek, sunum — yani **kodun ve verin** |
| SaaS | Restorana git | Yalnızca ye — **uygulamayı kullan** |

### 3.2 IaaS — Infrastructure as a Service

Sağlayıcı fiziksel altyapıyı (sunucu, depolama, ağ, sanallaştırma) işletir; sen üstüne **işletim sisteminden itibaren** her şeyi kurar ve yönetirsin.

- **Avantaj:** en fazla kontrol ve esneklik — istediğin OS, sürücü, ajan, yapılandırma; mevcut uygulamayı **değiştirmeden** taşıyabilirsin (lift-and-shift).
- **Dezavantaj:** en fazla yönetim yükü — OS yaması, antivirüs, yedekleme, güvenlik duvarı, izleme senin işin.
- **Senaryolar:** lift-and-shift taşıma (datacenter boşaltma), hızla açılıp silinen **test/geliştirme ortamları**, özel ya da eski (legacy) yazılım, depolama/yedekleme/kurtarma altyapısı, HPC.
- **Azure:** Virtual Machines, VM Scale Sets, Virtual Network, Managed Disks, *(depolama altyapısı olarak)* Azure Storage; **VM üzerinde SQL Server**.

### 3.3 PaaS — Platform as a Service

Sağlayıcı altyapıya ek olarak **işletim sistemini, runtime'ı, ara katmanı** ve platformun yamasını, yedeklemesini, ölçeklenmesini işletir; sen **kodunu ve verini** getirirsin.

- **Avantaj:** daha hızlı geliştirme ve dağıtım; OS/yama yükü yok; yerleşik ölçekleme, yedekleme ve yüksek erişilebilirlik.
- **Dezavantaj:** daha az kontrol — OS'a erişemezsin, platformun desteklemediği bir bileşeni (özel sürücü, belirli bir OS ayarı) kuramazsın; platforma bağımlılık.
- **Senaryolar:** web uygulaması ve API geliştirme, iş zekâsı/analitik, uygulama geliştirme çerçeveleri, IoT backend.
- **Azure:** App Service, **Azure SQL Database**, Azure SQL Managed Instance, Cosmos DB, Azure Kubernetes Service (yönetilen Kubernetes; control plane Microsoft'ta), Container Instances; serverless olanlar → 3.5.

### 3.4 SaaS — Software as a Service

Sağlayıcı **uygulamanın kendisini** işletir; sen yalnızca kullanır, kullanıcıları ve ayarları yönetirsin. Genellikle kullanıcı başına abonelikle ödenir.

- **Avantaj:** en hızlı başlangıç, en az yönetim; güncellemeler ve kapasite sağlayıcıda; her cihazdan erişim.
- **Dezavantaj:** en az kontrol ve özelleştirme — uygulama ne sunuyorsa o; verinin konumu ve yapılandırma seçenekleri sağlayıcıya bağlı.
- **Senaryolar:** e-posta ve mesajlaşma, ofis uygulamaları, CRM/ERP, iş birliği, takvim.
- **Microsoft örnekleri:** Microsoft 365 (Exchange Online, SharePoint, Teams), Dynamics 365, Outlook.com. *(Bunlar Azure servisi değildir ama sınavda SaaS örneği olarak geçer.)*

### 3.5 Serverless ([`Section04`](Sections/Section04_ServerlessBilling.cs))

**Serverless**, sunucuların olmadığı anlamına gelmez: sunucuların **sağlanması, ölçeklenmesi ve yönetilmesi tamamen sağlayıcıdadır**; sen yalnızca iş mantığını (kod veya iş akışı) yazarsın. Serverless, PaaS'ın bir alt türü olarak düşünülür.

| Nitelik | Anlamı |
|---|---|
| **Olay tetiklemeli** (event-driven) | Kod bir olay olunca çalışır: HTTP isteği, kuyruk mesajı, dosya yükleme, zamanlayıcı |
| **Otomatik ölçekleme** | Yük artınca kendiliğinden büyür, boşta **sıfıra** iner |
| **Kullanım kadar ücret** | Çağrı sayısı ve çalışma süresi × bellek (GB-saniye) üzerinden; boşta ücret yok |
| **Altyapı yönetimi yok** | VM, OS, kapasite planlaması yok |

Azure'da serverless örnekleri:

- **Azure Functions** — kod (C#, JavaScript, Python …) ile olay tetiklemeli fonksiyonlar. Microsoft yeni uygulamalar için **Flex Consumption** planını öneriyor; klasik Consumption planı da aynı mantıkla (çağrı + GB-saniye, aylık ücretsiz kota) faturalanır.
- **Azure Logic Apps** — kod yazmadan (low-code), görsel tasarımcıyla iş akışı ve entegrasyon (yüzlerce hazır connector).
- **Azure Container Apps** — serverless container platformu; sıfıra kadar ölçeklenir (→ C1.6).
- *(bağlam)* Event Grid (olay yönlendirme), Cosmos DB'nin serverless kapasite modu.

`Section04`, 300 ms süren ve 512 MB bellek kullanan bir fonksiyonun aylık maliyetini çağrı sayısına göre 7/24 açık küçük bir sunucuyla karşılaştırır (oranlar örnektir): aralıklı ve düşük/orta yükte serverless **neredeyse bedavaya** yakındır; çok yüksek ve **sürekli** yükte 7/24 açık kaynak ucuzlayabilir. Trade-off: boştan uyanırken **cold start** gecikmesi ve uzun süren işler için çalışma süresi sınırları.

### 3.6 Ürün → hizmet türü ve senaryo → hizmet türü ([`Section02`](Sections/Section02_ProductCatalog.cs), [`Section03`](Sections/Section03_Scenarios.cs))

**Karar sırası** (yukarıdan aşağı, ilk "evet"te dur):

1. İhtiyacı karşılayan **hazır bir uygulama** var mı? → **SaaS**
2. Kendi kodun var ve **olayla/aralıklı** mı çalışıyor? → **Serverless** (Functions, Logic Apps)
3. Kendi kodun var, **OS'a erişim gerekmiyor** mu? → **PaaS** (App Service, SQL Database)
4. OS/ağ üzerinde **tam kontrol** veya **aynen taşıma** mı gerekiyor? → **IaaS** (VM)

**Aynı teknoloji, farklı tür:** belirleyici olan ürünün adı değil **yönetim düzeyidir**:

| Teknoloji | IaaS | PaaS | SaaS |
|---|---|---|---|
| SQL Server | SQL Server on Azure VM | Azure SQL Database, SQL Managed Instance | — |
| Web sitesi | VM'e IIS/nginx kurmak | App Service | Hazır site oluşturucu |
| E-posta | VM'de Exchange Server | — | Exchange Online (Microsoft 365) |
| Container | VM'e Docker kurmak | AKS, Container Instances | — |

### 3.7 Pratik — aynı uygulama, üç hizmet türü

**(a) Pricing Calculator karşılaştırması.** Küçük bir "Todo" web uygulaması (aylık ~50.000 istek, 1 GB veri, 20 kullanıcılı ekip) için [Azure Pricing Calculator](https://azure.microsoft.com/pricing/calculator/)'da iki tahmin çıkar ve tabloyu doldur:

| | IaaS | PaaS | SaaS |
|---|---|---|---|
| Bileşenler | 1 Linux VM (küçük boyut) + VM'de veritabanı + disk | App Service planı + Azure SQL Database (en küçük katman) | Hazır bir görev yönetimi SaaS ürünü (kullanıcı başı abonelik) |
| Aylık tahmini maliyet | | | |
| OS yamasını kim yapar? | | | |
| Veritabanı yedeğini kim alır? | | | |
| Ölçeklemek için ne gerekir? | | | |
| Özelleştirme sınırı | | | |

<details><summary>Beklenen sonuç (fiyatlar region'a ve zamana göre değişir; yapıyı karşılaştır)</summary>

| | IaaS | PaaS | SaaS |
|---|---|---|---|
| Aylık maliyet | Küçük VM + disk sabit; 7/24 açık kalırsa kullanılmasa da ödenir | Küçük katmanlarda benzer veya daha düşük; yedek/HA fiyata dahil | Kullanıcı sayısı × abonelik; altyapı maliyeti yok |
| OS yaması | **Sen** | Microsoft | Microsoft |
| Veritabanı yedeği | **Sen** (veya VM yedeği kurarsın) | Microsoft (otomatik yedek, saklama süresini sen seçersin) | Sağlayıcı |
| Ölçekleme | Daha büyük VM veya scale set + load balancer kurmak | Plan katmanını/instance sayısını değiştirmek (autoscale) | Daha çok lisans almak |
| Özelleştirme | Sınırsız | Platformun izin verdiği kadar | Uygulamanın sunduğu ayarlar kadar |

Sonuç: Kontrol IaaS → SaaS yönünde azalır; yönetim yükü de onunla birlikte azalır. Maliyet karşılaştırmasında yalnızca fatura değil **operasyon emeği** (yama, yedek, izleme) de hesaba katılmalıdır.
</details>

**(b) CLI ile IaaS ve PaaS farkını hissetmek.** Bütçe alarmın kurulu olsun (→ C1.1). PaaS kısmı ücretsiz F1 katmanını kullanır; VM **ücretlidir** — oluşturup inceledikten hemen sonra sil.

```bash
az group create --name rg-az900-c13 --location westeurope

# PaaS: App Service (F1 = free tier). No OS, no server; just a web app.
az webapp list-runtimes --os linux --output table              # valid runtime values
az appservice plan create --resource-group rg-az900-c13 --name plan-c13 --sku F1 --is-linux
az webapp create --resource-group rg-az900-c13 --plan plan-c13 \
  --name todo-c13-$RANDOM --runtime "DOTNETCORE:10.0"
az webapp list --resource-group rg-az900-c13 --query "[].defaultHostName" --output tsv

# IaaS: VM (paid!). You choose the OS; patching, firewall, software installation are now your job.
az vm create --resource-group rg-az900-c13 --name vm-c13 \
  --image Ubuntu2404 --size Standard_B1s \
  --admin-username azureuser --generate-ssh-keys
az vm list --resource-group rg-az900-c13 --show-details --output table
# (Optional) ssh azureuser@<publicIp> → sudo apt update && sudo apt upgrade  ← in IaaS this responsibility is yours

# Cleanup — mandatory
az group delete --name rg-az900-c13 --yes --no-wait
```

Gözlem: App Service için ne bir OS imajı seçtin ne de bir disk; VM için imaj, boyut, kullanıcı, SSH anahtarı ve (oluşturulan) public IP/NSG gibi altyapı parçalarıyla uğraştın. Fark, shared responsibility tablosunun ta kendisidir.

---

## 4. Gerçek dünya senaryosu

Bir lojistik şirketi on yıllık uygulama portföyünü buluta taşıyor ve her iş yükü için hizmet türünü seçiyor:

| İş yükü | Karar | Neden |
|---|---|---|
| Üreticisi artık destek vermeyen, özel bir Windows sürücüsüne bağımlı depo yönetim yazılımı | **IaaS** (VM, lift-and-shift) | Kod değiştirilemiyor, OS düzeyinde kontrol gerekiyor; datacenter kapanış tarihine yetişmeli |
| Müşterilerin gönderi takip ettiği web sitesi ve API | **PaaS** (App Service + Azure SQL Database) | Ekip kod yazıyor, OS yönetmek istemiyor; kampanyada autoscale |
| Sürücülerin yüklediği teslimat fotoğraflarını küçültüp etiketleyen işlem | **Serverless** (Azure Functions, blob yükleme tetikleyicisi) | Gün içinde düzensiz, gece neredeyse hiç; boşta ücret yok |
| Muhasebe sistemine fatura aktaran onay akışı | **Serverless** (Logic Apps) | Hazır connector'larla kod yazmadan entegrasyon |
| Kurum içi e-posta ve doküman paylaşımı | **SaaS** (Microsoft 365) | Hazır ürün; sunucu yönetimi tamamen kalkıyor |

Bir yıl sonra depo yazılımı yeniden yazıldığında o da VM'den PaaS'a geçiriliyor (**refactor**): lift-and-shift hızlı taşımanın, PaaS ise uzun vadeli işletme kolaylığının aracıdır.

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** (1) Ürün → tür eşleştirme, (2) senaryo → tür seçimi, (3) türlerin nitelikleri hakkında evet/hayır, (4) serverless'in nitelikleri.

| Soru kalıbı | Cevap |
|---|---|
| "İşletim sistemine **tam erişim/kontrol**", "özel yazılım/sürücü kurulacak" | **IaaS** |
| "Mevcut sunucuları **değiştirmeden** taşı" (lift-and-shift) | **IaaS** |
| "**En fazla** esneklik/kontrol" | **IaaS** |
| "Geliştiriciler yalnızca **koda** odaklansın", "OS yaması yok" | **PaaS** |
| "**En az** yönetim yükü" + hazır uygulama | **SaaS** |
| "**En az** yönetim yükü" + kendi kodunu çalıştırma | **PaaS / serverless** |
| "Yalnızca kod **çalışırken** ödeme", "olaya tepki olarak çalışma" | **Serverless** |
| Azure SQL Database / App Service / Cosmos DB | **PaaS** |
| VM üzerinde SQL Server / Virtual Machines | **IaaS** |
| Microsoft 365 / Dynamics 365 / Outlook.com | **SaaS** |
| Azure Functions / Logic Apps | **Serverless** (PaaS) |

**Tuzaklar:**
- "SQL" kelimesi tek başına türü belirlemez: **nerede ve kim yönetiyor?**
- "En az yönetim yükü" sorusunda şıklarda hem SaaS hem PaaS varsa senaryoyu oku: **kendi uygulamasını** geliştiren bir ekip için SaaS bir seçenek değildir.
- "Serverless'te sunucu yoktur" → **Hayır**; sunucular vardır, yönetimleri sağlayıcıdadır.
- "PaaS'ta müşteri işletim sistemini yamar" → **Hayır**.
- "IaaS'ta müşteri fiziksel sunucudan sorumludur" → **Hayır** (fiziksel her şey Microsoft'ta).

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "Serverless = sunucusuz" | Sunucular var; sağlama, ölçekleme ve yönetim sağlayıcıda |
| "Azure SQL = IaaS" | Azure SQL Database ve Managed Instance PaaS; yalnızca VM'deki SQL Server IaaS |
| "SaaS'ta hiçbir sorumluluğum yok" | Kullanıcılar, erişim, MFA ve veri hâlâ sende (→ C1.1) |
| "PaaS her zaman IaaS'tan ucuz" | Fatura tek başına belirleyici değil; ama operasyon emeği hesaba katılınca genellikle PaaS lehine |
| "Her şeyi VM'e koyalım, sonra bakarız" | Lift-and-shift hızlıdır ama IaaS'ın tüm işletme yükünü getirir; uygun iş yüklerini zamanla PaaS'a taşımak planlanmalı |
| "Functions her iş için en ucuzu" | Çok yüksek ve sürekli yükte 7/24 açık bir plan daha ucuz olabilir; ayrıca cold start ve süre sınırları |
| Microsoft 365'i "Azure servisi" saymak | Microsoft'un SaaS ürünüdür; Azure'un kendi servis kataloğunda değildir |

---

## 7. Performans & trade-off

| Boyut | IaaS | PaaS | Serverless | SaaS |
|---|---|---|---|---|
| Kontrol/özelleştirme | ★★★★ | ★★★ | ★★ | ★ |
| Yönetim yükü | Yüksek | Orta | Düşük | En düşük |
| Ölçekleme | Kendin kurarsın (scale set, LB) | Yerleşik (plan/instance, autoscale) | Otomatik, sıfıra kadar | Sağlayıcı |
| Maliyet modeli | Çalıştığı süre (boşta da) | Plan/instance süresi | Çağrı + çalışma süresi; boşta 0 | Kullanıcı başı abonelik |
| Taşıma hızı (mevcut uygulama) | En hızlı (değişiklik yok) | Uygulama uyarlanmalı | Yeniden tasarım gerekir | Veri taşıma + süreç değişikliği |
| Tipik risk | Yama/güvenlik ihmali | Platform kısıtları | Cold start, süre sınırı, satıcı bağımlılığı | Özelleştirme ve veri konumu sınırları |

Kural: **İşletme yükünü azaltabildiğin kadar yukarı (SaaS'a doğru) çık; kontrol gerektiren yerde aşağıda (IaaS) kal.**

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe cloud service types](https://learn.microsoft.com/en-us/training/modules/describe-cloud-service-types/)
- [What is IaaS?](https://azure.microsoft.com/resources/cloud-computing-dictionary/what-is-iaas) · [What is PaaS?](https://azure.microsoft.com/resources/cloud-computing-dictionary/what-is-paas) · [What is SaaS?](https://azure.microsoft.com/resources/cloud-computing-dictionary/what-is-saas) · [What is serverless computing?](https://azure.microsoft.com/resources/cloud-computing-dictionary/what-is-serverless-computing)
- [Azure Functions hosting options](https://learn.microsoft.com/en-us/azure/azure-functions/functions-scale) (Flex Consumption, Consumption, Premium, Dedicated)
- [Azure Pricing Calculator](https://azure.microsoft.com/pricing/calculator/) · [Azure Functions pricing](https://azure.microsoft.com/pricing/details/functions/)
- [`az webapp`](https://learn.microsoft.com/en-us/cli/azure/webapp) · [`az vm create`](https://learn.microsoft.com/en-us/cli/azure/vm#az-vm-create)
