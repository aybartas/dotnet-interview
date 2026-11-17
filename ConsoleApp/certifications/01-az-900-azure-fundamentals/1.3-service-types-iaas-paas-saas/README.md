[← AZ-900 İçindekiler](../README.md) · [📝 Quiz](quiz.md) · [← Önceki konu](../1.2-cloud-benefits/README.md)

## 1.3 Bulut Hizmet Türleri: IaaS, PaaS, SaaS, Serverless

> **Sınav Alanı:** Describe cloud concepts (%25-30) · **Konu Kodu:** C1.3
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe cloud service types* — IaaS · PaaS · SaaS · her hizmet türünün kullanım senaryosu · (*Describe cloud computing*'den) serverless. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

### 1. Ne?

Bulut hizmetleri, sağlayıcının yığının **ne kadarını** yönettiğine göre üç ana türe ayrılır; serverless, PaaS'ın en "yönetimsiz" biçimidir:

- **Ne kiralarsın**
  - IaaS (Infrastructure as a Service): Altyapı: sanal makine, ağ, depolama
  - PaaS (Platform as a Service): Platform: çalışma ortamı (runtime), veritabanı, ara katman
  - SaaS (Software as a Service): Hazır uygulama
- **Kontrol / esneklik**
  - IaaS (Infrastructure as a Service): **En fazla**
  - PaaS (Platform as a Service): Orta
  - SaaS (Software as a Service): **En az**
- **Yönetim yükü / sorumluluk**
  - IaaS (Infrastructure as a Service): **En fazla**
  - PaaS (Platform as a Service): Orta
  - SaaS (Software as a Service): **En az**
- **Sen ne yaparsın**
  - IaaS (Infrastructure as a Service): OS, yama, runtime, uygulama, veri, ağ kuralları
  - PaaS (Platform as a Service): Kod ve veri; uygulama yapılandırması
  - SaaS (Software as a Service): Kullanıcılar, veri, ayarlar
- **Kullanım senaryosu**
  - IaaS (Infrastructure as a Service): **Lift-and-shift** taşıma, test/geliştirme ortamı, özel yazılım, depolama/yedekleme, yüksek performanslı hesap (HPC)
  - PaaS (Platform as a Service): Web/API geliştirme, iş zekâsı/analitik, geliştirme çerçeveleri, IoT backend
  - SaaS (Software as a Service): E-posta ve iletişim, ofis uygulamaları, CRM/ERP, iş birliği
- **Azure örnekleri**
  - IaaS (Infrastructure as a Service): Virtual Machines, VM Scale Sets, Virtual Network, Managed Disks
  - PaaS (Platform as a Service): App Service, Azure SQL Database, Cosmos DB, AKS, **Functions**
  - SaaS (Software as a Service): Microsoft 365, Dynamics 365, Outlook.com


---

### 2. Neden?

- **Sınav:** "Ürün → hizmet türü" ve "senaryo → hizmet türü" eşleştirmeleri bu alanın sabit sorularıdır. Shared responsibility (C1.1) soruları da IaaS/PaaS/SaaS'ı bilmeden cevaplanamaz.
- **Gerçek işte:** Hizmet türü seçimi; ekibin ne kadar operasyon yükü taşıyacağını, ne kadar özelleştirme yapabileceğini, maliyet yapısını ve taşıma süresini belirler. Yanlış seçim ya gereksiz operasyon yükü (her şeyi VM'de çalıştırmak) ya da tıkanmış bir platform (PaaS'ın izin vermediği bir bağımlılık) demektir.
- **Sonraki konular:** VM ve scale set (C1.5), App Service/container/Functions (C1.6), depolama (C1.8) bu türlerin Azure'daki ürünleridir.

---

### 3. Nasıl?

#### 3.1 Yığının hangi katmanını kim yönetir?

Klasik uygulama yığını gösterimi (Sen = müşteri, MS = Microsoft):

- **Uygulama** — On-prem: Sen · IaaS: Sen · PaaS: Sen · SaaS: MS
- **Veri** — On-prem: Sen · IaaS: Sen · PaaS: Sen · SaaS: MS*
- **Runtime** — On-prem: Sen · IaaS: Sen · PaaS: MS · SaaS: MS
- **Middleware** — On-prem: Sen · IaaS: Sen · PaaS: MS · SaaS: MS
- **İşletim sistemi** — On-prem: Sen · IaaS: Sen · PaaS: MS · SaaS: MS
- **Sanallaştırma** — On-prem: Sen · IaaS: MS · PaaS: MS · SaaS: MS
- **Sunucular** — On-prem: Sen · IaaS: MS · PaaS: MS · SaaS: MS
- **Depolama** — On-prem: Sen · IaaS: MS · PaaS: MS · SaaS: MS
- **Ağ (fiziksel)** — On-prem: Sen · IaaS: MS · PaaS: MS · SaaS: MS

\* Bu klasik gösterim bir **sadeleştirmedir**: SaaS'ta verinin saklanması Microsoft'ta olsa da verinin kendisi, hesaplar ve erişim **her modelde müşterinin sorumluluğundadır**. Sınav C1.1'deki shared responsibility tablosunu temel alır.

**Pizza benzetmesi** (hatırlamak için):

| Model | Pizza | Sen ne yaparsın? |
|---|---|---|
| On-prem | Evde sıfırdan yap | Fırın, malzeme, pişirme, masa — hepsi |
| IaaS | Hazır hamur al, evde pişir | Tarif, pişirme, servis (mutfak/altyapı hazır) |
| PaaS | Eve sipariş | Masa, içecek, sunum — yani **kodun ve verin** |
| SaaS | Restorana git | Yalnızca ye — **uygulamayı kullan** |

#### 3.2 IaaS — Infrastructure as a Service

Sağlayıcı fiziksel altyapıyı (sunucu, depolama, ağ, sanallaştırma) işletir; sen üstüne **işletim sisteminden itibaren** her şeyi kurar ve yönetirsin.

- **Avantaj:** en fazla kontrol ve esneklik — istediğin OS, sürücü, ajan, yapılandırma; mevcut uygulamayı **değiştirmeden** taşıyabilirsin (lift-and-shift).
- **Dezavantaj:** en fazla yönetim yükü — OS yaması, antivirüs, yedekleme, güvenlik duvarı, izleme senin işin.
- **Senaryolar:** lift-and-shift taşıma (datacenter boşaltma), hızla açılıp silinen **test/geliştirme ortamları**, özel ya da eski (legacy) yazılım, depolama/yedekleme/kurtarma altyapısı, HPC.
- **Azure:** Virtual Machines, VM Scale Sets, Virtual Network, Managed Disks, *(depolama altyapısı olarak)* Azure Storage; **VM üzerinde SQL Server**.

#### 3.3 PaaS — Platform as a Service

Sağlayıcı altyapıya ek olarak **işletim sistemini, runtime'ı, ara katmanı** ve platformun yamasını, yedeklemesini, ölçeklenmesini işletir; sen **kodunu ve verini** getirirsin.

- **Avantaj:** daha hızlı geliştirme ve dağıtım; OS/yama yükü yok; yerleşik ölçekleme, yedekleme ve yüksek erişilebilirlik.
- **Dezavantaj:** daha az kontrol — OS'a erişemezsin, platformun desteklemediği bir bileşeni (özel sürücü, belirli bir OS ayarı) kuramazsın; platforma bağımlılık.
- **Senaryolar:** web uygulaması ve API geliştirme, iş zekâsı/analitik, uygulama geliştirme çerçeveleri, IoT backend.
- **Azure:** App Service, **Azure SQL Database**, Azure SQL Managed Instance, Cosmos DB, Azure Kubernetes Service (yönetilen Kubernetes; control plane Microsoft'ta), Container Instances; serverless olanlar → 3.5.

#### 3.4 SaaS — Software as a Service

Sağlayıcı **uygulamanın kendisini** işletir; sen yalnızca kullanır, kullanıcıları ve ayarları yönetirsin. Genellikle kullanıcı başına abonelikle ödenir.

- **Avantaj:** en hızlı başlangıç, en az yönetim; güncellemeler ve kapasite sağlayıcıda; her cihazdan erişim.
- **Dezavantaj:** en az kontrol ve özelleştirme — uygulama ne sunuyorsa o; verinin konumu ve yapılandırma seçenekleri sağlayıcıya bağlı.
- **Senaryolar:** e-posta ve mesajlaşma, ofis uygulamaları, CRM/ERP, iş birliği, takvim.
- **Microsoft örnekleri:** Microsoft 365 (Exchange Online, SharePoint, Teams), Dynamics 365, Outlook.com. *(Bunlar Azure servisi değildir ama sınavda SaaS örneği olarak geçer.)*

#### 3.5 Serverless

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

#### 3.6 Ürün → hizmet türü ve senaryo → hizmet türü

**Karar sırası** (yukarıdan aşağı, ilk "evet"te dur):

1. İhtiyacı karşılayan **hazır bir uygulama** var mı? → **SaaS**
2. Kendi kodun var ve **olayla/aralıklı** mı çalışıyor? → **Serverless** (Functions, Logic Apps)
3. Kendi kodun var, **OS'a erişim gerekmiyor** mu? → **PaaS** (App Service, SQL Database)
4. OS/ağ üzerinde **tam kontrol** veya **aynen taşıma** mı gerekiyor? → **IaaS** (VM)

**Aynı teknoloji, farklı tür:** belirleyici olan ürünün adı değil **yönetim düzeyidir**:

- **SQL Server**
  - IaaS: SQL Server on Azure VM
  - PaaS: Azure SQL Database, SQL Managed Instance
  - SaaS: —
- **Web sitesi** — IaaS: VM'e IIS/nginx kurmak · PaaS: App Service · SaaS: Hazır site oluşturucu
- **E-posta** — IaaS: VM'de Exchange Server · PaaS: — · SaaS: Exchange Online (Microsoft 365)
- **Container** — IaaS: VM'e Docker kurmak · PaaS: AKS, Container Instances · SaaS: —

#### 3.7 Pratik — aynı uygulama, üç hizmet türü

**(a) Pricing Calculator karşılaştırması.** Küçük bir "Todo" web uygulaması (aylık ~50.000 istek, 1 GB veri, 20 kullanıcılı ekip) için [Azure Pricing Calculator](https://azure.microsoft.com/pricing/calculator/)'da iki tahmin çıkar ve tabloyu doldur:

- **Bileşenler**
  - IaaS: 1 Linux VM (küçük boyut) + VM'de veritabanı + disk
  - PaaS: App Service planı + Azure SQL Database (en küçük katman)
  - SaaS: Hazır bir görev yönetimi SaaS ürünü (kullanıcı başı abonelik)
- **Aylık tahmini maliyet** — IaaS: — · PaaS: — · SaaS: —
- **OS yamasını kim yapar?** — IaaS: — · PaaS: — · SaaS: —
- **Veritabanı yedeğini kim alır?** — IaaS: — · PaaS: — · SaaS: —
- **Ölçeklemek için ne gerekir?** — IaaS: — · PaaS: — · SaaS: —
- **Özelleştirme sınırı** — IaaS: — · PaaS: — · SaaS: —

<details><summary>Beklenen sonuç (fiyatlar region'a ve zamana göre değişir; yapıyı karşılaştır)</summary>

- **Aylık maliyet**
  - IaaS: Küçük VM + disk sabit; 7/24 açık kalırsa kullanılmasa da ödenir
  - PaaS: Küçük katmanlarda benzer veya daha düşük; yedek/HA fiyata dahil
  - SaaS: Kullanıcı sayısı × abonelik; altyapı maliyeti yok
- **OS yaması** — IaaS: **Sen** · PaaS: Microsoft · SaaS: Microsoft
- **Veritabanı yedeği**
  - IaaS: **Sen** (veya VM yedeği kurarsın)
  - PaaS: Microsoft (otomatik yedek, saklama süresini sen seçersin)
  - SaaS: Sağlayıcı
- **Ölçekleme**
  - IaaS: Daha büyük VM veya scale set + load balancer kurmak
  - PaaS: Plan katmanını/instance sayısını değiştirmek (autoscale)
  - SaaS: Daha çok lisans almak
- **Özelleştirme**
  - IaaS: Sınırsız · PaaS: Platformun izin verdiği kadar
  - SaaS: Uygulamanın sunduğu ayarlar kadar

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

### 4. Gerçek dünya senaryosu

Bir lojistik şirketi on yıllık uygulama portföyünü buluta taşıyor ve her iş yükü için hizmet türünü seçiyor:

- Üreticisi artık destek vermeyen, özel bir Windows sürücüsüne bağımlı depo yönetim yazılımı
  - Karar: **IaaS** (VM, lift-and-shift)
  - Neden: Kod değiştirilemiyor, OS düzeyinde kontrol gerekiyor; datacenter kapanış tarihine yetişmeli
- **Müşterilerin gönderi takip ettiği web sitesi ve API**
  - Karar: **PaaS** (App Service + Azure SQL Database)
  - Neden: Ekip kod yazıyor, OS yönetmek istemiyor; kampanyada autoscale
- Sürücülerin yüklediği teslimat fotoğraflarını küçültüp etiketleyen işlem
  - Karar: **Serverless** (Azure Functions, blob yükleme tetikleyicisi)
  - Neden: Gün içinde düzensiz, gece neredeyse hiç; boşta ücret yok
- **Muhasebe sistemine fatura aktaran onay akışı**
  - Karar: **Serverless** (Logic Apps)
  - Neden: Hazır connector'larla kod yazmadan entegrasyon
- **Kurum içi e-posta ve doküman paylaşımı**
  - Karar: **SaaS** (Microsoft 365)
  - Neden: Hazır ürün; sunucu yönetimi tamamen kalkıyor

Bir yıl sonra depo yazılımı yeniden yazıldığında o da VM'den PaaS'a geçiriliyor (**refactor**): lift-and-shift hızlı taşımanın, PaaS ise uzun vadeli işletme kolaylığının aracıdır.

---

### 5. Sınav perspektifi

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

### 6. Yaygın hatalar

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

### 7. Performans & trade-off

- **Kontrol/özelleştirme** — IaaS: ★★★★ · PaaS: ★★★ · Serverless: ★★ · SaaS: ★
- **Yönetim yükü** — IaaS: Yüksek · PaaS: Orta · Serverless: Düşük · SaaS: En düşük
- **Ölçekleme**
  - IaaS: Kendin kurarsın (scale set, LB)
  - PaaS: Yerleşik (plan/instance, autoscale)
  - Serverless: Otomatik, sıfıra kadar · SaaS: Sağlayıcı
- **Maliyet modeli**
  - IaaS: Çalıştığı süre (boşta da) · PaaS: Plan/instance süresi
  - Serverless: Çağrı + çalışma süresi; boşta 0
  - SaaS: Kullanıcı başı abonelik
- **Taşıma hızı (mevcut uygulama)**
  - IaaS: En hızlı (değişiklik yok) · PaaS: Uygulama uyarlanmalı
  - Serverless: Yeniden tasarım gerekir
  - SaaS: Veri taşıma + süreç değişikliği
- **Tipik risk**
  - IaaS: Yama/güvenlik ihmali · PaaS: Platform kısıtları
  - Serverless: Cold start, süre sınırı, satıcı bağımlılığı
  - SaaS: Özelleştirme ve veri konumu sınırları

Kural: **İşletme yükünü azaltabildiğin kadar yukarı (SaaS'a doğru) çık; kontrol gerektiren yerde aşağıda (IaaS) kal.**

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe cloud service types](https://learn.microsoft.com/en-us/training/modules/describe-cloud-service-types/)
- [What is IaaS?](https://azure.microsoft.com/resources/cloud-computing-dictionary/what-is-iaas) · [What is PaaS?](https://azure.microsoft.com/resources/cloud-computing-dictionary/what-is-paas) · [What is SaaS?](https://azure.microsoft.com/resources/cloud-computing-dictionary/what-is-saas) · [What is serverless computing?](https://azure.microsoft.com/resources/cloud-computing-dictionary/what-is-serverless-computing)
- [Azure Functions hosting options](https://learn.microsoft.com/en-us/azure/azure-functions/functions-scale) (Flex Consumption, Consumption, Premium, Dedicated)
- [Azure Pricing Calculator](https://azure.microsoft.com/pricing/calculator/) · [Azure Functions pricing](https://azure.microsoft.com/pricing/details/functions/)
- [`az webapp`](https://learn.microsoft.com/en-us/cli/azure/webapp) · [`az vm create`](https://learn.microsoft.com/en-us/cli/azure/vm#az-vm-create)

---

➡️ **Sıradaki:** [📝 Bu konunun quiz soruları](quiz.md)
