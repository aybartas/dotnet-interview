[← Konu anlatımı](README.md) · [AZ-900 İçindekiler](../README.md)

### 📝 Bölüm 2.3 Quiz Soruları

> Teori: [README.md](README.md) · Sınav alanı: *Describe Azure architecture and services* (%35-40) · 21 soru
>
> Sorular **özgündür** ve sınav formatındadır; gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 17/21**.

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir ekip ASP.NET Core web uygulamasını yayınlamak istiyor. İşletim sistemi yaması yapmak istemiyor; otomatik ölçekleme ve yerleşik yama yönetimi istiyor. Hangi servis **en uygundur**?

- A) Azure Virtual Machines
- B) Azure App Service
- C) Azure Container Instances
- D) Azure Virtual Desktop

<details><summary>Cevap</summary>

**B.** App Service, OS yönetmeden web/API barındıran PaaS'tır; otomatik yama ve autoscale yerleşik özelliklerdir. VM OS yönetimi gerektirir; ACI orkestrasyonsuz tek container içindir; AVD masaüstü sanallaştırmadır.
</details>

#### Soru 2: Tek seçim
App Service'te uygulamanın çalıştığı compute kaynağını tanımlayan ve **ücretlendirilen birim** olan nedir?

- A) Storage account
- B) Resource group
- C) App Service plan
- D) WebJob

<details><summary>Cevap</summary>

**C.** App Service plan (katman + instance sayısı) ücretlendirilir; birden çok uygulama aynı planı paylaşabilir.
</details>

#### Soru 3: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) App Service hem Windows hem Linux üzerinde uygulama çalıştırabilir. | ☐ | ☐ |
| b) App Service'te uygulamanın işletim sistemi yamalarını müşteri uygular. | ☐ | ☐ |
| c) WebJobs, App Service içinde arka plan işi çalıştırmaya olanak tanır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** App Service PaaS'tır; OS yamaları Microsoft'tadır.
</details>

#### Soru 4: Tek seçim
Container'ları sanal makinelerden ayıran temel özellik hangisidir?

- A) Container'lar donanımı sanallaştırır, her biri kendi çekirdeğini çalıştırır
- B) Container'lar işletim sistemini sanallaştırır, host'un çekirdeğini paylaşır ve saniyeler içinde başlar
- C) Container'lar yalnızca Windows üzerinde çalışır
- D) Container'lar her zaman VM'lerden daha güçlü izolasyon sağlar

<details><summary>Cevap</summary>

**B.** VM donanımı sanallaştırır (tam OS); container OS'u sanallaştırır — hafif, taşınabilir, hızlı başlar. İzolasyon VM'de daha güçlüdür (D yanlış).
</details>

#### Soru 5: Senaryo
Bir veri ekibi ayda bir kez çalışan, yaklaşık 10 dakika süren tek bir container'ı **en basit** yoldan çalıştırmak istiyor. VM veya orkestratör yönetmek istemiyor. Hangi servisi seçmeliler?

- A) Azure Kubernetes Service
- B) Azure Container Instances
- C) Azure Virtual Machine Scale Sets
- D) Azure Virtual Desktop

<details><summary>Cevap</summary>

**B.** Tek container, kısa süre, orkestrasyon gerekmiyor → ACI; saniye bazında ücretlenir. AKS orkestrasyon için ağır bir seçimdir.
</details>

#### Soru 6: Senaryo
Bir şirket yüzlerce container'dan oluşan bir mikroservis platformu çalıştıracak; rolling update, servis keşfi ve Kubernetes API'sine tam erişim istiyor. Hangi servis **en uygundur**?

- A) Azure Container Instances
- B) Azure Functions
- C) Azure Kubernetes Service (AKS)
- D) Azure App Service

<details><summary>Cevap</summary>

**C.** Çok sayıda container'ın orkestrasyonu ve Kubernetes API'si = AKS (yönetilen Kubernetes).
</details>

#### Soru 7: Tek seçim
Bir ekip container tabanlı API'lerini çalıştırmak istiyor; Kubernetes yönetmek istemiyor, trafik yokken uygulamanın **sıfıra ölçeklenmesini** ve kullanım kadar ödemeyi istiyor. Hangisi **en uygundur**?

- A) Azure Container Apps
- B) Azure Virtual Machines
- C) Azure Kubernetes Service
- D) Azure Virtual Desktop

<details><summary>Cevap</summary>

**A.** Container Apps serverless container platformudur: otomatik ölçek (sıfıra kadar), HTTP ingress, mikroservis özellikleri; Kubernetes karmaşıklığını gizler.
</details>

#### Soru 8: Eşleştirme
Her ihtiyacı servisle eşleştirin: **App Service · Container Instances · AKS · Azure Functions**

1. Her gece 02:00'de 3 dakika çalışan temizlik kodu
2. Sürekli çalışan kurumsal web sitesi, OS yönetimi yok
3. Tek bir test container'ını hızlıca ayağa kaldırmak
4. Büyük ölçekli container orkestrasyonu

<details><summary>Cevap</summary>

**1 → Functions** (zamanlayıcı tetikleyici) · **2 → App Service** · **3 → Container Instances** · **4 → AKS**.
</details>

#### Soru 9: Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** Azure Functions'ı tetikleyebilir?

- A) Bir Blob Storage container'ına dosya yüklenmesi
- B) Bir VM'in boyutunun değiştirilmesi için portalda menü açılması
- C) Bir zamanlayıcı (cron ifadesi)
- D) Kullanıcının ofis bilgisayarını kapatması

<details><summary>Cevap</summary>

**A ve C.** Blob yüklemesi, zamanlayıcı, HTTP isteği ve kuyruk mesajı gibi olaylar tetikleyicidir.
</details>

#### Soru 10: Evet / Hayır

- **a) Azure Functions yalnızca HTTP istekleriyle tetiklenebilir.** — Evet: ☐ · Hayır: ☐
- b) Azure Functions kullanırken müşteri sunucu veya işletim sistemi yönetmez. — Evet: ☐ · Hayır: ☐
- c) Serverless planlarda Functions, çalışmadığı sürede de compute ücreti doğurur. — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** Birçok tetikleyici vardır; Functions serverless olduğundan sunucu/OS yönetimi yoktur; serverless planlarda boşta compute ücreti yoktur.
</details>

#### Soru 11: Tek seçim
Bir platform ekibi, **çok sayıda** container'ı orkestre etmek ve Kubernetes API'sine tam erişimle yönetmek istiyor. Hangi servis **en uygundur**?

- A) Azure Container Instances
- B) Azure Kubernetes Service (AKS)
- C) Azure Functions
- D) Azure App Service

<details><summary>Cevap</summary>

**B.** Çok sayıda container'ın orkestrasyonu ve Kubernetes'e tam erişim = AKS. ACI tek/basit container içindir ve orkestrasyon sunmaz; Functions olay tetiklemeli koddur; App Service web/API barındırır.
</details>

#### Soru 12: Senaryo
Bir şirket, sözleşmeli çalışanlarının kendi cihazlarından şirket uygulamalarına erişmesini; ancak **verinin bu cihazlara inmemesini** istiyor. Kimlik doğrulamada MFA zorunlu olacak. Hangi servis **en uygundur**?

- A) Azure Virtual Desktop
- B) Azure Container Instances
- C) Azure Functions
- D) Azure App Service

<details><summary>Cevap</summary>

**A.** AVD, masaüstü ve uygulamaları Azure'da çalıştırır; cihaz yalnızca bağlanır, veri bulutta kalır; kimlik Entra ID ve MFA ile yapılır.
</details>

#### Soru 13: Tek seçim
Azure Virtual Desktop'ta birden çok kullanıcının **aynı VM üzerinde** eşzamanlı olarak tam Windows 11 masaüstü deneyimi almasını sağlayan özellik hangisidir?

- A) Availability set
- B) Windows 11 Enterprise multi-session
- C) Azure Blob Storage
- D) VM Scale Sets

<details><summary>Cevap</summary>

**B.** Windows 11/10 Enterprise multi-session, AVD'ye özgü çok oturumlu Windows sürümüdür ve VM başına maliyeti düşürür.
</details>

#### Soru 14: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Virtual Desktop'ta kullanıcı masaüstleri Azure'daki VM'lerde çalışır. | ☐ | ☐ |
| b) Azure Virtual Desktop, Microsoft Entra ID ile kimlik doğrulamayı ve MFA'yı destekler. | ☐ | ☐ |
| c) Azure Virtual Desktop'a yalnızca Windows cihazlardan bağlanılabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Windows, macOS, iOS, Android istemcileri ve web tarayıcısı desteklenir.
</details>

#### Soru 15: Tek seçim
Container'lar ile sanal makineler (VM) arasındaki temel fark aşağıdakilerden hangisidir?

- A) Container'lar donanımı, VM'ler işletim sistemini sanallaştırır
- B) Container'lar host işletim sisteminin çekirdeğini paylaşır; her VM'in kendi tam işletim sistemi vardır
- C) Container'lar yalnızca Windows'ta çalışır
- D) VM'ler container'lardan daha hızlı başlar

<details><summary>Cevap</summary>

**B.** Container işletim sistemini sanallaştırır ve çekirdeği paylaşır; bu yüzden küçük (MB) ve hızlı başlar (saniyeler). VM donanımı sanallaştırır ve her VM tam bir işletim sistemi taşır (GB, dakikalar).
</details>

#### Soru 16: Sıralama
Aşağıdaki barındırma seçeneklerini **müşterinin yönetim yükü en fazladan en aza** doğru sıralayın: Azure Functions · Virtual Machines · Azure App Service

<details><summary>Cevap</summary>

**Virtual Machines → App Service → Azure Functions.** VM'de OS sende; App Service'te platform Microsoft'ta ama planı sen boyutlandırırsın; serverless Functions'ta kapasite yönetimi de yoktur.
</details>

#### Soru 17: Senaryo
Bir uygulama, belirli bir OS sürümüne ve özel bir sürücüye bağımlı; kod değiştirilemiyor. Hangi barındırma seçeneği **zorunlu** hale gelir?

- A) Azure Virtual Machines
- B) Azure App Service
- C) Azure Container Apps
- D) Azure Functions

<details><summary>Cevap</summary>

**A.** OS üzerinde tam kontrol ve özel sürücü kurulumu yalnızca IaaS'ta (VM) mümkündür (→ C1.5).
</details>

#### Soru 18: Tek seçim
Bir ekip, tek bir container'ı **VM veya orkestratör yönetmeden**, saniye bazında ücretle ve mümkün olan en basit biçimde çalıştırmak istiyor. Hangi servis **en uygundur**?

- A) Azure Kubernetes Service
- B) Azure Container Instances
- C) Azure Virtual Desktop
- D) VM Scale Sets

<details><summary>Cevap</summary>

**B.** Tek container'ı en hızlı ve en basit biçimde çalıştırmak **Azure Container Instances**'tır. AKS çok sayıda container'ın orkestrasyonu içindir; AVD masaüstü sanallaştırmadır; scale set VM grubudur.
</details>

#### Soru 19: Tek seçim
Bir şirket, kurumsal web sitesini ve mobil uygulamasının **REST API**'sini aynı platformda, işletim sistemi yönetmeden barındırmak istiyor. Windows veya Linux seçebilmeli ve .NET, Java, Node.js veya Python kullanabilmeli. Hangisi **en uygundur**?

- A) Azure App Service (Web Apps ve API Apps)
- B) Azure Virtual Desktop
- C) Azure Container Instances
- D) Azure Virtual Machines

<details><summary>Cevap</summary>

**A.** App Service, **web uygulamalarını** (Web Apps), **REST API**'leri ve mobil backend'leri OS yönetmeden barındıran PaaS'tır; Windows/Linux ve birçok dili destekler. VM OS yönetimi ister; ACI tek container içindir; AVD masaüstü sanallaştırmadır.
</details>

#### Soru 20: Senaryo
Bir görev günde bir kez, yaklaşık 30 saniye çalışıyor. Ekip iki seçeneği karşılaştırıyor: (1) 7/24 açık küçük bir VM, (2) zamanlayıcı tetiklemeli bir Azure Function. Aşağıdakilerden hangisi **doğrudur**?

- A) VM her zaman daha ucuzdur çünkü sabit fiyatlıdır
- B) Function yalnızca çalıştığı süre için ücretlenir ve sunucu yönetimi gerektirmez; VM boşta beklerken de ücretlenir ve OS yönetimi gerektirir
- C) Function'ı kullanmak için önce bir VM oluşturmak gerekir
- D) İkisi de aynı yönetim yükünü getirir

<details><summary>Cevap</summary>

**B.** Seyrek ve kısa çalışan, olay/zamanlayıcı tetiklemeli işte **Functions** boşta ücret üretmez ve altyapı yönetimi yoktur. VM sürekli çalıştığı sürece (deallocate edilmedikçe) ücretlenir ve işletim sistemi bakımı müşteridedir.
</details>

#### Soru 21: Tek seçim
Bir ekip, uzun süre **sürekli çalışan**, kendi bağımlılıklarını paketlemiş taşınabilir bir uygulamayı her ortamda (geliştirici makinesi, test, bulut) **aynı şekilde** çalıştırmak istiyor. Container ve Azure Functions arasında hangisi bu ihtiyaca daha uygundur ve neden?

- A) Functions; çünkü uzun süre sürekli çalışan işler için tasarlanmıştır
- B) Container; çünkü uygulama ve bağımlılıkları tek bir image olarak paketlenir, her ortamda aynı çalışır ve süre sınırı yoktur
- C) İkisi de aynıdır
- D) Hiçbiri; yalnızca VM kullanılabilir

<details><summary>Cevap</summary>

**B.** **Container**, uygulamayı bağımlılıklarıyla birlikte taşınabilir bir image'a paketler ve sürekli çalışabilir. **Functions** ise olay tetiklemeli **kısa** kod içindir; tek bir çağrının süresi sınırlıdır.
</details>

---

**Puanlama:** 19-21 → hazırsın · 15-18 → README 3.5'teki karar ağacını tekrar et · < 15 → konuyu baştan çalış ve 3.6'daki laboratuvarı uygula.

---

➡️ **Sonraki konu:** [2.4 Ağ Servisleri: VNet, Peering, VPN, ExpressRoute, DNS](../2.4-networking/README.md) · [AZ-900 İçindekiler](../README.md)
