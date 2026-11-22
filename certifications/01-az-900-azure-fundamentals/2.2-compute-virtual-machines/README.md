[← AZ-900 İçindekiler](../README.md) · [📝 Quiz](quiz.md) · [← Önceki konu](../2.1-core-architecture/README.md)

## 2.2 Azure Compute ve Virtual Machines

> **Sınav Alanı:** Describe Azure architecture and services (%35-40) · **Konu Kodu:** C1.5
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe Azure compute and networking services* — compute türleri (VM, container, functions) · VM seçenekleri: **Azure VMs, VM Scale Sets, availability sets** (Azure Virtual Desktop → [C1.6](../2.3-app-hosting-containers-functions-avd/README.md)) · **VM için gereken kaynaklar**. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

### 1. Ne?

**Azure compute**, uygulamaları çalıştırmak için isteğe bağlı işlem gücü (CPU, bellek, disk, ağ) sunan servislerin genel adıdır. Seçenekler, yönetim yükü ile kontrol arasındaki yelpazede sıralanır (→ C1.3):

- **Virtual Machines**
  - Hizmet türü: IaaS
  - Ne zaman?: İşletim sistemi üzerinde tam kontrol
  - Konu: Bu konu
- **VM Scale Sets**
  - Hizmet türü: IaaS
  - Ne zaman?: Aynı VM'den çok sayıda, otomatik ölçekle
  - Konu: Bu konu
- **Container'lar** (ACI, Container Apps, AKS)
  - Hizmet türü: PaaS / serverless
  - Ne zaman?: Hafif, taşınabilir paketler; mikroservis
  - Konu: C1.6
- **App Service** — Hizmet türü: PaaS · Ne zaman?: OS yönetmeden web uygulaması/API · Konu: C1.6
- **Azure Functions** — Hizmet türü: Serverless · Ne zaman?: Olay tetiklemeli kısa kod · Konu: C1.6
- **Azure Virtual Desktop** — Hizmet türü: Masaüstü sanallaştırma · Ne zaman?: Buluttan Windows masaüstü · Konu: C1.6

Bu konu VM tarafını derinlemesine işler: **VM'in kendisi ve gerektirdiği kaynaklar**, **VM Scale Sets**, **availability sets** **VM boyutları, image'ları ve diskleri**. Konunun kodu yok; pratik, README'deki `az` CLI laboratuvarıdır (3.5).

---

### 2. Neden?

- **Sınav:** "VM için hangi kaynak gerekir?", "availability set mi, scale set mi, zone mu?" ve "VM ücretini nasıl durdururum?" bu alanın standart sorularıdır.
- **Gerçek işte:** Taşınan iş yüklerinin büyük kısmı önce VM olarak başlar (lift-and-shift). VM'in tek başına bir kaynak olmadığını (disk, NIC, IP, VNet) bilmek; maliyeti (silinmeyen disk ve IP'ler), güvenliği (internete açık port) ve dayanıklılığı (tek VM = tek arıza noktası) doğrudan etkiler.

---

### 3. Nasıl?

#### 3.1 Azure Virtual Machines

**Azure VM**, fiziksel donanımı satın almadan kiralanan **sanallaştırılmış bir sunucudur**. IaaS olduğu için işletim sisteminden yukarısı (yama, yazılım, yapılandırma, yedek) senin sorumluluğundadır (→ C1.1).

Ne zaman VM?

- İşletim sistemi üzerinde **tam kontrol** veya özel yazılım/sürücü/yapılandırma gerektiğinde
- Mevcut sunucuların **lift-and-shift** ile değiştirilmeden taşınmasında
- Hızla açılıp kapatılan **test/geliştirme** ortamlarında
- Uygulama barındırma ve **datacenter'ı buluta genişletme** (ağ → C1.7)

#### 3.2 VM için gereken kaynaklar ⭐

Bir VM, birlikte çalışan birkaç Azure kaynağından oluşur. `az vm create` (veya portal sihirbazı) eksik olanları otomatik oluşturur:

- **Boyut** (size, SKU)
  - Ne işe yarar: vCPU, bellek, geçici disk, ağ bant genişliği
  - Seçenekler / not: Aileler: genel amaçlı (B, D), compute optimized (F), memory optimized (E, M), storage optimized (L), GPU (N), HPC (H); boyut sonradan değiştirilebilir (genellikle yeniden başlatma ile)
- **Image**
  - Ne işe yarar: VM'in kurulacağı işletim sistemi ve (varsa) hazır yazılım
  - Seçenekler / not: Windows Server, Ubuntu, RHEL…; **Azure Marketplace** imajları; kendi özel imajın
- **Disk**
  - Ne işe yarar: OS diski + isteğe bağlı **veri diskleri**
  - Seçenekler / not: **Managed disk** (Azure yönetir); Standard HDD · Standard SSD · Premium SSD (· Premium SSD v2, Ultra Disk); disk türü performansı ve maliyeti belirler
- **Virtual network + subnet**
  - Ne işe yarar: VM'in bağlandığı özel ağ
  - Seçenekler / not: VNet bir region'a bağlıdır (→ C1.7)
- **Network interface (NIC)**
  - Ne işe yarar: VM'i subnet'e bağlayan sanal ağ kartı; özel IP taşır
  - Seçenekler / not: Bir VM'de birden çok NIC olabilir
- **Public IP** *(isteğe bağlı)*
  - Ne işe yarar: İnternetten erişim
  - Seçenekler / not: Prod'da genellikle yoktur; erişim özel ağ (VPN) üzerinden
- **Network security group (NSG)**
  - Ne işe yarar: Gelen/giden trafik için izin/engelleme kuralları
  - Seçenekler / not: Varsayılan olarak SSH (22) / RDP (3389) açılabilir — internete açık bırakma

> Sınav sorusu kalıbı: "Bir VM oluşturmak için aşağıdakilerden hangisi **gerekli değildir**?" — public IP (isteğe bağlı), App Service plan, Azure Functions uygulaması gibi şıklar gelir. Zorunlu çekirdek: **boyut, image, OS diski, VNet/subnet ve NIC**.

**Fiyatlandırma:**

- **Çalışıyor (Running)** — Compute ücreti: Var (saniye/saat bazında) · Disk ücreti: Var
- **Stopped** (işletim sisteminden kapatılmış veya `az vm stop`) — Compute ücreti: **Var** — donanım hâlâ ayrılmış · Disk ücreti: Var
- **Stopped (deallocated)** (`az vm deallocate`, portalda *Stop*)
  - Compute ücreti: **Yok** — donanım serbest bırakıldı
  - Disk ücreti: **Var** (ve statik public IP de ücretlendirilebilir)
- **Silindi**
  - Compute ücreti: Yok
  - Disk ücreti: Disk/IP **ayrıca** silinmediyse ücret sürer

Maliyeti düşüren diğer seçenekler: reservation/savings plan, spot VM (→ C1.1), Azure Hybrid Benefit, otomatik kapatma, doğru boyutlandırma (→ C1.11).

#### 3.3 VM Scale Sets (VMSS)

**VM Scale Set**, **aynı yapılandırmadaki** VM'lerden oluşan ve **tek bir kaynak gibi** yönetilen gruptur.

- **Otomatik ölçekleme:** VM sayısı metriğe (CPU, kuyruk uzunluğu) veya zamana göre **otomatik** artar/azalır (scale out/in) — elasticity'nin (→ C1.2) VM'deki aracı.
- **Dayanıklılık:** Instance'lar **availability zone'lara** ve fault domain'lere yayılabilir.
- **Ölçek:** Yüzlerce VM'i (orkestrasyon moduna göre 1.000'e kadar) tek tanımla yönetmek; güncellemeleri kademeli uygulamak.
- **Ücret:** Scale set'in kendisi ek ücretli değildir; çalışan VM'ler ve kullanılan kaynaklar ödenir.

Kullanım: değişken trafikli web katmanı, büyük hesaplama/toplu işler, container host'ları.

#### 3.4 Availability sets

**Availability set**, VM'leri **tek bir datacenter içinde** farklı donanım ve bakım gruplarına yayarak, planlı bakımda veya donanım arızasında tüm VM'lerin aynı anda kapanmasını önler. İki kavram:

- **Fault domain (FD)**
  - Anlamı: Ortak güç kaynağını ve ağ anahtarını paylaşan **raf** grubu; bir FD'deki donanım arızası yalnızca o FD'deki VM'leri etkiler
  - Değer: En çok **3** (region'a göre 2 veya 3)
- **Update domain (UD)**
  - Anlamı: Planlı bakımda **aynı anda yeniden başlatılabilecek** VM grubu; Azure UD'leri **sırayla** günceller
  - Değer: Varsayılan **5**, en çok **20**

- Ek ücret yoktur; yalnızca VM'ler ödenir.
- VM availability set'e **oluşturulurken** yerleştirilir (mevcut bir VM sonradan eklenemez).
- Microsoft, yeni dağıtımlarda daha yüksek dayanıklılık için **availability zone**'ları öneriyor; availability set'ler hâlâ desteklenir ve sınav kapsamındadır.

**Availability set ↔ availability zone ↔ scale set:**

- **Neye karşı korur**
  - Availability set: Datacenter **içindeki** donanım (raf) arızası ve planlı bakım
  - Availability zone: **Datacenter'ın** (zone'un) tamamen çökmesi
  - VM Scale Set: Kendisi koruma değil **ölçek** aracıdır; zone/FD'lere yayılarak korur
- **Otomatik ölçek** — Availability set: Yok · Availability zone: Yok · VM Scale Set: **Var**
- **Ana fikir**
  - Availability set: "Aynı rafta olmasınlar"
  - Availability zone: "Aynı binada olmasınlar"
  - VM Scale Set: "Yüke göre çoğalsınlar"

#### 3.5 Pratik — VM, deallocate, scale set ve availability set

> ⚠️ Bu laboratuvar **ücretlidir** (küçük `Standard_B1s` VM'ler, birkaç saatlik kullanım genellikle düşük maliyetlidir). Önce bütçe alarmını kur (→ C1.1 / 3.6), laboratuvarı tek oturumda bitir ve **en sondaki `az group delete`'i atlama**.

```bash
az login
az group create --name rg-az900-c15 --location westeurope
az vm image list --output table                     # available image aliases (Ubuntu2404 etc.)

# 1) Single VM — see which resources get created together
az vm create --resource-group rg-az900-c15 --name vm-c15 \
  --image Ubuntu2404 --size Standard_B1s \
  --admin-username azureuser --generate-ssh-keys
az resource list --resource-group rg-az900-c15 \
  --query "[].{Name:name, Type:type}" --output table
# Expected: virtualMachines, disks (OS disk), networkInterfaces, networkSecurityGroups,
#           publicIPAddresses, virtualNetworks

# 2) Difference between stop and deallocate
az vm stop --resource-group rg-az900-c15 --name vm-c15         # "VM stopped" → compute billing CONTINUES
az vm show --resource-group rg-az900-c15 --name vm-c15 --show-details --query powerState --output tsv
az vm deallocate --resource-group rg-az900-c15 --name vm-c15   # "VM deallocated" → compute billing STOPS
az vm show --resource-group rg-az900-c15 --name vm-c15 --show-details --query powerState --output tsv
# The disk still exists and is billed:
az disk list --resource-group rg-az900-c15 --query "[].{Disk:name, GB:diskSizeGb, Sku:sku.name}" --output table

# 3) VM Scale Set with two instances + a simple autoscale rule
az vmss create --resource-group rg-az900-c15 --name vmss-c15 \
  --image Ubuntu2404 --vm-sku Standard_B1s --instance-count 2 \
  --admin-username azureuser --generate-ssh-keys
az monitor autoscale create --resource-group rg-az900-c15 --name autoscale-c15 \
  --resource vmss-c15 --resource-type Microsoft.Compute/virtualMachineScaleSets \
  --min-count 2 --max-count 4 --count 2
az monitor autoscale rule create --resource-group rg-az900-c15 --autoscale-name autoscale-c15 \
  --condition "Percentage CPU > 70 avg 5m" --scale out 1
az monitor autoscale rule create --resource-group rg-az900-c15 --autoscale-name autoscale-c15 \
  --condition "Percentage CPU < 30 avg 5m" --scale in 1
az vmss list-instances --resource-group rg-az900-c15 --name vmss-c15 --output table

# 4) Availability set: fault / update domain values
az vm availability-set create --resource-group rg-az900-c15 --name avset-c15 \
  --platform-fault-domain-count 2 --platform-update-domain-count 5
az vm create --resource-group rg-az900-c15 --name vm-avset-1 \
  --image Ubuntu2404 --size Standard_B1s --availability-set avset-c15 \
  --admin-username azureuser --generate-ssh-keys --public-ip-address ""
az vm get-instance-view --resource-group rg-az900-c15 --name vm-avset-1 \
  --query "{FaultDomain:instanceView.platformFaultDomain, UpdateDomain:instanceView.platformUpdateDomain}" --output table

# 5) Cleanup — mandatory
az group delete --name rg-az900-c15 --yes --no-wait
```

> **Alıştırma.** Aşağıdaki gereksinimler için doğru seçeneği yaz (VM / availability set / availability zone / scale set / deallocate):
> 1. Gece kullanılmayan test VM'leri için compute ücreti ödenmesin.
> 2. İki VM aynı datacenter'da kalsın ama aynı raf arızasından etkilenmesin.
> 3. Web katmanı yük artınca kendiliğinden yeni VM eklesin.
> 4. Bir datacenter tamamen çökse bile uygulama ayakta kalsın.
> 5. Mevcut sunucuyu hiç değiştirmeden, işletim sistemi üzerinde tam kontrolle buluta taşı.
>
> <details><summary>Çözüm</summary>
>
> 1. **Deallocate** (ör. otomatik kapatma ile) — `stop` yetmez, donanım ayrılmış kalır.
> 2. **Availability set** (farklı fault domain'ler).
> 3. **VM Scale Set** + autoscale kuralı.
> 4. **Availability zone** (VM'leri veya scale set'i zone'lara yaymak).
> 5. **Azure VM** (IaaS, lift-and-shift).
> </details>

---

### 4. Gerçek dünya senaryosu

Bir sigorta şirketi poliçe uygulamasını kendi datacenter'ından Azure'a taşıyor:

- **Lift-and-shift:** Eski poliçe motoru özel bir Windows servisine bağımlı, kod değiştirilemiyor → **Azure VM**. Doğru boyut için mevcut sunucunun CPU/RAM kullanımı ölçülüyor (Azure Migrate → C1.8); genel amaçlı D serisi seçiliyor, veritabanı diski Premium SSD.
- **Dayanıklılık:** Poliçe motorunun iki VM'i aynı region'da **iki farklı availability zone'a** konuyor; ilk aşamada zone desteği olmayan bir yan sistem için iki VM bir **availability set**'te.
- **Ölçek:** Müşteri portalının web katmanı **VM Scale Set**'te; ay sonu yoğunluğunda CPU kuralıyla 2'den 8 instance'a çıkıyor, sonra kendiliğinden 2'ye iniyor.
- **Maliyet:** Test ortamının VM'leri her akşam 20:00'de **deallocate** ediliyor; ilk ay faturasında, silinen VM'lerden kalan **sahipsiz diskler ve public IP'ler** fark edilip temizleniyor.
- **Güvenlik:** Prod VM'lerinin public IP'si yok; yönetim erişimi özel ağ (VPN) üzerinden (→ C1.7, C1.10).

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → doğru VM seçeneği; "VM için hangi kaynak gerekli/gereksiz?"; durdurma ve ücretlendirme; availability set kavramları (fault/update domain) için evet/hayır tabloları.

- **"Planlı bakımda tüm VM'ler aynı anda yeniden başlamasın"**
  - Cevap: **Availability set** (update domain)
  - Tuzak: Scale set
- **"Raf/donanım arızası tüm VM'leri etkilemesin, aynı datacenter"**
  - Cevap: **Availability set** (fault domain)
  - Tuzak: Availability zone
- **"Datacenter arızasına karşı koru"** — Cevap: **Availability zone** · Tuzak: Availability set
- "Talebe göre VM sayısı **otomatik** artsın" — Cevap: **VM Scale Set** · Tuzak: Availability set
- **"Aynı yapılandırmadaki yüzlerce VM'i tek kaynak gibi yönet"** — Cevap: **VM Scale Set** · Tuzak: —
- **"Kullanılmayan VM için compute ücreti ödememek"** — Cevap: **Deallocate** (portalda Stop) · Tuzak: İşletim sisteminden kapatmak
- **"OS üzerinde tam kontrol"** — Cevap: **VM** (IaaS) · Tuzak: App Service
- **"VM oluşturmak için gerekmeyen kaynak"**
  - Cevap: Public IP, App Service plan…
  - Tuzak: NIC, disk, VNet (bunlar gerekli)

**Anahtar kelimeler:** *fault domain* (güç/ağ/raf), *update domain* (planlı bakım, sırayla), *identical, load-balanced VMs* (scale set), *automatically increase or decrease* (scale set), *deallocated* (ücret yok).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "VM'i işletim sisteminden kapattım, ücret durdu" | Durum *Stopped* olur, donanım ayrılmış kalır; ücret için **deallocate** gerekir |
| "VM'i silince her şey gitti" | Disk, public IP ve NIC ayrı kaynaklardır; silinmeyenler kalır ve bazıları ücretlenir — RG'yi silmek en temiz yol |
| "Availability set datacenter arızasına karşı korur" | Set tek datacenter içindedir; datacenter arızası için **zone** |
| "Scale set otomatik HA demektir" | Scale set **ölçek** aracıdır; dayanıklılık için zone/FD'lere yayılmalıdır |
| "Mevcut VM'i availability set'e eklerim" | Set'e oluşturma anında yerleştirilir |
| "Public IP her VM için zorunlu" | İsteğe bağlıdır; prod'da genellikle verilmez |
| "VM'in boyutu değiştirilemez" | Boyut sonradan değiştirilebilir (genellikle yeniden başlatma ile) |

---

### 7. Performans & trade-off

- **Tek VM**
  - Kazanç: Basit, ucuz
  - Bedel: Tek arıza noktası
- **Availability set**
  - Kazanç: Raf ve bakım arızasına dayanıklılık, ek ücret yok
  - Bedel: Datacenter arızasına karşı koruma yok
- **Availability zone'lar**
  - Kazanç: Datacenter arızasına dayanıklılık
  - Bedel: Zone'lar arası gecikme ve trafik ücreti; her region desteklemez
- **VM Scale Set + autoscale**
  - Kazanç: Talebe göre maliyet, insan müdahalesi yok
  - Bedel: Stateless uygulama tasarımı, ölçek kurallarının ayarı
- **Büyük VM boyutu (scale up)**
  - Kazanç: Basit performans artışı
  - Bedel: Üst sınır, yeniden başlatma, maliyet
- **Premium SSD / Ultra Disk**
  - Kazanç: Yüksek IOPS, düşük gecikme
  - Bedel: Daha yüksek disk maliyeti

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure compute and networking services](https://learn.microsoft.com/en-us/training/modules/describe-azure-compute-networking-services/)
- [Virtual machines in Azure](https://learn.microsoft.com/en-us/azure/virtual-machines/overview) · [VM sizes](https://learn.microsoft.com/en-us/azure/virtual-machines/sizes/overview) · [States and billing status](https://learn.microsoft.com/en-us/azure/virtual-machines/states-billing)
- [What are Virtual Machine Scale Sets?](https://learn.microsoft.com/en-us/azure/virtual-machine-scale-sets/overview)
- [Availability sets overview](https://learn.microsoft.com/en-us/azure/virtual-machines/availability-set-overview)
- [`az vm`](https://learn.microsoft.com/en-us/cli/azure/vm) · [`az vmss`](https://learn.microsoft.com/en-us/cli/azure/vmss) · [`az monitor autoscale`](https://learn.microsoft.com/en-us/cli/azure/monitor/autoscale)

---

➡️ **Sıradaki:** [📝 Bu konunun quiz soruları](quiz.md)
