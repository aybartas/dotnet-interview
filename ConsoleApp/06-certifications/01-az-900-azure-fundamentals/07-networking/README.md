# C1.7 Ağ Servisleri

> 📁 `ConsoleApp/06-certifications/01-az-900-azure-fundamentals/07-networking/` · Sınav alanı: **Describe Azure architecture and services (%35-40)** · Önkoşul: C1.4, C1.5 · Quiz: [`quiz.md`](quiz.md) · Derinlemesine ağ → J1.2, S8.3
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe Azure compute and networking services* — virtual networking: **VNet, subnet, peering, Azure DNS, VPN Gateway, ExpressRoute** · **public ve private endpoint**'ler. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

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

Azure'daki kaynakların (VM, App Service, veritabanı…) birbirleriyle, internetle ve şirketin kendi datacenter'ıyla nasıl konuştuğunu belirleyen servisler:

| Servis | Tek cümle |
|---|---|
| **Virtual Network (VNet)** | Azure'daki kaynakların **izole**, özel ağı; bir region'a bağlıdır |
| **Subnet** | VNet'in alt ağı; kaynakları gruplar, güvenlik kuralları subnet'e uygulanır |
| **VNet peering** | İki VNet'i **Microsoft omurgası** üzerinden (internete çıkmadan) birbirine bağlar |
| **Azure DNS** | Domain'lerin DNS kayıtlarını Azure'da **barındırır** (domain satmaz) |
| **VPN Gateway** | On-prem ile Azure arasında **internet üzerinden şifreli** tünel |
| **ExpressRoute** | On-prem ile Azure arasında **internete çıkmayan özel** bağlantı |
| **Public endpoint** | Bir servise **internetten** erişilen genel adres |
| **Private endpoint** (Private Link) | PaaS servisine VNet içinden **özel IP** ile erişim |

Bu konunun C01 projesinde kodu yoktur; pratik kısım `az` CLI laboratuvarıdır (3.7).

---

## 2. Neden?

- **Sınav:** "Hangi bağlantı seçeneği?" (VPN Gateway, ExpressRoute, peering) ve "public mi private endpoint mi?" soruları bu alanın klasikleridir; ayrımı tek bir anahtar ifade yapar ("internetten geçmeden", "şifreli tünel", "iki VNet").
- **Gerçek işte:** Ağ tasarımı güvenliğin (segmentasyon, NSG, private endpoint → C1.10 defense in depth) ve hybrid mimarinin (→ C1.1) temelidir. Yanlış adres planı (çakışan CIDR'ler) peering'i ve VPN'i sonradan imkânsız kılar.
- **Bağlam:** IP adresleme, CIDR, DNS ve TCP/IP temelleri → J1.2; kurumsal ağ topolojileri (hub-spoke, Virtual WAN) → S8.3.

---

## 3. Nasıl?

### 3.1 Azure Virtual Network ve subnet'ler

**VNet**, Azure kaynaklarının birbirleriyle, internetle ve on-prem ağlarla **güvenli** iletişim kurmasını sağlayan özel ağdır.

- **Adres alanı:** özel IP aralığı, CIDR ile (ör. `10.0.0.0/16` → ~65 bin adres). Aynı adres alanına bağlanacak ağlarla (diğer VNet'ler, on-prem) **çakışmamalıdır**.
- **Kapsam:** bir VNet **tek bir region**'dadır, ama o region'ın tüm availability zone'larına yayılır; bir subscription'a aittir.
- **Subnet:** adres alanının alt aralığı (ör. `10.0.1.0/24` web, `10.0.2.0/24` veri); kaynakları gruplamak ve **NSG** (network security group — izin/engel kuralları) uygulamak için.

VNet'in sağladığı yetenekler (resmî içerikte sayılan başlıklar):

| Yetenek | Açıklama | Örnek |
|---|---|---|
| **İzolasyon ve segmentasyon** | Ağı subnet'lere bölüp birbirinden ayırmak | Web / uygulama / veri subnet'leri |
| **İnternet iletişimi** | Kaynağa public IP veya public load balancer ile internetten erişim; giden trafik varsayılan olarak mümkün | Public IP'li VM, load balancer |
| **Azure kaynakları arası iletişim** | Aynı VNet'teki kaynaklar; **service endpoint**/private endpoint ile PaaS servisleri; peering ile diğer VNet'ler | VM → Azure SQL (private endpoint) |
| **On-prem ile iletişim** | Point-to-site / site-to-site VPN, ExpressRoute | Ofis ağı ↔ Azure |
| **Trafik yönlendirme** | Varsayılan sistem rotaları; **route table** (user-defined routes) ve BGP ile özel yönlendirme | Trafiği bir firewall'dan geçirmek |
| **Trafik filtreleme** | **NSG** (IP/port/protokol kuralları) ve network virtual appliance (NVA — firewall gibi özel cihazlar) | "Yalnızca 443'e izin ver" |

### 3.2 VNet peering

**Peering**, iki VNet'i **Microsoft'un omurga ağı** üzerinden bağlar; trafik **internete çıkmaz**, gateway gerekmez.

- **Regional peering:** aynı region'daki VNet'ler · **Global peering:** farklı region'lardaki VNet'ler.
- **Düşük gecikme, yüksek bant genişliği** — aynı ağdaymış gibi.
- **Kurallar:** adres alanları **çakışmamalı** · peering **iki yönlü** tanımlanır (her VNet'te bir peering bağlantısı) · **geçişli (transitive) değildir**: A↔B ve B↔C peering'i, A ile C'yi konuşturmaz (A↔C ayrıca peering ister veya hub'da bir router/firewall gerekir) · farklı subscription ve hatta farklı tenant'lardaki VNet'ler de peering yapabilir.
- Ücret: peering'in kendisi değil, peering üzerinden geçen **veri transferi** ücretlendirilir (global peering daha pahalıdır) (→ C1.11).

### 3.3 Azure DNS

**Azure DNS**, DNS domain'lerini Azure altyapısında barındıran servistir.

- **Güvenilirlik ve performans:** Azure'un dünya çapındaki ad sunucu ağı; anycast ile kullanıcıya en yakın sunucu cevap verir.
- **Yönetim kolaylığı:** kayıtlar portal, CLI, PowerShell, ARM/Bicep ile yönetilir; aynı faturada, aynı kimlikle.
- **Güvenlik:** Azure **RBAC** (kim DNS kaydını değiştirebilir), **activity log** (kim neyi değiştirdi), **resource lock** (kritik zone'un silinmesini engelleme → C1.12).
- **Alias kayıtları** *(bağlam)*: bir kaydı doğrudan bir Azure kaynağına (public IP, Traffic Manager, Front Door) bağlamak; IP değişince kayıt kendiliğinden güncellenir.
- **Önemli:** Azure DNS ile **domain satın alınmaz** — domain bir kayıt kuruluşundan (registrar) alınır, NS kayıtları Azure DNS'e yönlendirilir. *(App Service Domains gibi ayrı bir satın alma seçeneği vardır ama bu Azure DNS'in kendisi değildir.)*
- **Private DNS zone:** VNet içinde, internete açık olmayan **özel isim çözümleme** (ör. `db.internal.contoso.com` → `10.0.2.4`; private endpoint'lerin `privatelink.*` zone'ları).

### 3.4 VPN Gateway

Bir **VPN**, iki ağ arasında **şifreli tünel** kurar. **Azure VPN Gateway**, bir VNet'e konuşlandırılan ve **public internet üzerinden** IPsec/IKE ile şifreli bağlantı sağlayan sanal ağ geçididir (VNet başına bir VPN gateway).

| Bağlantı türü | Ne bağlanır? |
|---|---|
| **Site-to-site (S2S)** | Ofis/datacenter ağı ↔ Azure VNet (her iki uçta VPN cihazı) |
| **Point-to-site (P2S)** | Tek bir istemci (geliştiricinin dizüstü bilgisayarı) ↔ Azure VNet |
| **VNet-to-VNet** | Azure VNet ↔ Azure VNet (peering'e alternatif; şifreli tünel) |

- **Policy-based** VPN: tüneli hangi trafiğin kullanacağını statik IP aralığı kurallarıyla belirler (eski/kısıtlı; yalnızca belirli senaryolar). **Route-based** VPN: rota tablosuna göre yönlendirir; çoğu senaryo, P2S ve çoklu bağlantılar için önerilen tür.
- **Yüksek erişilebilirlik:** **active/standby** (varsayılan — bakım/arıza sırasında yedek instance kısa bir kesintiyle devralır) · **active/active** (iki instance da tünel taşır) · **zone-redundant gateway** (instance'lar availability zone'lara yayılır) · **ExpressRoute failover** (ExpressRoute'un yedeği olarak VPN).
- *(bağlam — güncel SKU durumu)* Microsoft, zone desteksiz (non-AZ) VpnGw1–5 SKU'larını **AZ SKU'larına** taşıyor; duyurulara göre non-AZ SKU'lar Eylül 2026'da emekliye ayrıldı/otomatik taşındı. Sınav SKU adlarını sormaz; güncel durumu VPN Gateway "What's new" sayfasından kontrol et.

### 3.5 ExpressRoute

**ExpressRoute**, on-prem ağı Microsoft bulutuna bir **bağlantı sağlayıcısı** (telekom operatörü/ortak tesis) üzerinden bağlayan **özel** bir bağlantıdır; trafik **public internete çıkmaz**.

- **Faydaları:** daha **yüksek bant genişliği**, daha **öngörülebilir gecikme**, daha yüksek **güvenilirlik**; Azure'un yanında **Microsoft 365** ve Dynamics 365 servislerine de bağlanabilir; dinamik yönlendirme (BGP); yerleşik yedeklilik (bağlantı çifti).
- **Bağlantı modelleri** *(bağlam)*: CloudExchange co-location, point-to-point Ethernet, any-to-any (IPVPN) ve **ExpressRoute Direct** (Microsoft'un ağına doğrudan, 10/100 Gbps fiziksel port).
- **Global Reach:** farklı şubelerin on-prem ağlarını ExpressRoute devreleri üzerinden **Microsoft omurgasıyla birbirine** bağlamak.
- Not: ExpressRoute **özel** bir hattır ama varsayılan olarak **şifreli tünel değildir**; şifreleme gerekiyorsa ExpressRoute Direct'te MACsec veya ExpressRoute üzerinden IPsec kullanılır *(bağlam)*.

**VPN Gateway vs ExpressRoute** ⭐

| | VPN Gateway | ExpressRoute |
|---|---|---|
| Trafik nereden geçer? | **Public internet** (şifreli tünel) | **Özel bağlantı** — internete çıkmaz |
| Kurulum | Hızlı; VPN cihazı + gateway | Bağlantı sağlayıcısıyla sözleşme, devre tahsisi (haftalar) |
| Bant genişliği / gecikme | Daha düşük, internete bağlı değişken | Yüksek, öngörülebilir |
| Maliyet | Düşük | Yüksek |
| Tipik kullanım | Küçük/orta ofis, geliştirici erişimi (P2S), ExpressRoute yedeği | Büyük veri hacmi, kritik hybrid iş yükleri, regülasyon |

### 3.6 Public ve private endpoint'ler

- **Public endpoint:** bir kaynağın internetten erişilebilen adresi (ör. `mystorage.blob.core.windows.net`, `myapp.azurewebsites.net`). Erişim firewall kuralları, kimlik doğrulama ve TLS ile korunur ama adres **internete açıktır**.
- **Private endpoint** (**Azure Private Link**): bir PaaS servisine (Storage, SQL Database, Key Vault…) VNet'inin içinden bir **özel IP** ile bağlanan ağ arayüzü.
  - Trafik **Microsoft ağında** kalır; servisin **public erişimi tamamen kapatılabilir**.
  - On-prem ağdan da (VPN/ExpressRoute üzerinden) özel IP ile erişilebilir.
  - DNS: servis adı (`mystorage.blob.core.windows.net`) VNet içinde **private DNS zone** (`privatelink.blob.core.windows.net`) sayesinde özel IP'ye çözülür.
- *(bağlam)* **Service endpoint:** VNet'in kimliğini servise tanıtıp servis firewall'ında yalnızca o subnet'e izin vermek; servis yine public IP'sinden erişilir. Private endpoint daha sıkı izolasyon sağlar.

*(bağlam — sınavda ad olarak geçebilir)*

| Servis | Ne yapar? |
|---|---|
| **NSG** | Subnet/NIC düzeyinde IP-port-protokol izin/engel kuralları |
| **Azure Load Balancer** | Katman 4 (TCP/UDP) yük dengeleme |
| **Application Gateway** | Katman 7 (HTTP) yük dengeleme + **WAF** (web uygulama güvenlik duvarı) |
| **Azure Front Door** | Global HTTP giriş noktası, CDN, WAF, bölgeler arası yönlendirme |
| **Azure Firewall** | Yönetilen, durum bilgili ağ güvenlik duvarı |
| **Azure DDoS Protection** | Dağıtık hizmet reddi saldırılarına karşı koruma (→ C1.10) |

### 3.7 Pratik — VNet, peering, private DNS ve private endpoint

> Bütçe alarmın kurulu olsun (→ C1.1). VNet, subnet ve peering bağlantısının kendisi ücretsizdir (peering'de **veri transferi** ücretlidir); private DNS zone, private endpoint ve test VM'i **saatlik/aylık küçük ücret** doğurur — laboratuvarı tek oturumda bitirip **sil**. VPN Gateway ve ExpressRoute **kurmuyoruz** (gateway'in kurulumu ~30-45 dk sürer ve sürekli ücretlenir; ExpressRoute sağlayıcı sözleşmesi ister).

```bash
az login
RG=rg-az900-c17; LOC=westeurope
az group create --name $RG --location $LOC

# 1) VNet-A: iki subnet (uygulama + private endpoint)
az network vnet create --resource-group $RG --name vnet-a --address-prefixes 10.1.0.0/16 \
  --subnet-name snet-app --subnet-prefixes 10.1.1.0/24
az network vnet subnet create --resource-group $RG --vnet-name vnet-a --name snet-pe --address-prefixes 10.1.2.0/24

# 2) VNet-B: çakışmayan adres alanı
az network vnet create --resource-group $RG --name vnet-b --address-prefixes 10.2.0.0/16 \
  --subnet-name snet-app --subnet-prefixes 10.2.1.0/24

# 3) Peering — İKİ yönlü tanımlanır
az network vnet peering create --resource-group $RG --name a-to-b --vnet-name vnet-a --remote-vnet vnet-b --allow-vnet-access
az network vnet peering create --resource-group $RG --name b-to-a --vnet-name vnet-b --remote-vnet vnet-a --allow-vnet-access
az network vnet peering list --resource-group $RG --vnet-name vnet-a --query "[].{ad:name,durum:peeringState}" --output table
#   peeringState = Connected olmalı (tek yön tanımlanırsa "Initiated" kalır)

# 4) Storage account (public endpoint'i ile başlar)
ST=stc17$RANDOM
az storage account create --resource-group $RG --name $ST --location $LOC --sku Standard_LRS --kind StorageV2
nslookup $ST.blob.core.windows.net              # yerel makinende: public IP'ye çözülür

# 5) Private DNS zone + VNet bağlantısı
az network private-dns zone create --resource-group $RG --name privatelink.blob.core.windows.net
az network private-dns link vnet create --resource-group $RG --zone-name privatelink.blob.core.windows.net \
  --name link-vnet-a --virtual-network vnet-a --registration-enabled false

# 6) Private endpoint (blob alt kaynağı için) + DNS zone grubu (A kaydını otomatik yazar)
az network private-endpoint create --resource-group $RG --name pe-$ST \
  --vnet-name vnet-a --subnet snet-pe \
  --private-connection-resource-id $(az storage account show -g $RG -n $ST --query id -o tsv) \
  --group-id blob --connection-name pe-$ST-conn
az network private-endpoint dns-zone-group create --resource-group $RG --endpoint-name pe-$ST \
  --name default --private-dns-zone privatelink.blob.core.windows.net --zone-name blob

# 7) Public erişimi kapat
az storage account update --resource-group $RG --name $ST --public-network-access Disabled

# 8) Test: VNet-A içinde küçük bir VM; komutu run-command ile çalıştır (SSH'a gerek yok).
#    run-command, VM ajanının Azure'a giden bağlantısını kullanır; bu yüzden VM'i varsayılan ayarlarla
#    (public IP + NSG) açıyoruz. Storage'a giden istek ise private endpoint üzerinden VNet içinde kalır.
az vm create --resource-group $RG --name vm-test --image Ubuntu2404 --size Standard_B1s \
  --vnet-name vnet-a --subnet snet-app \
  --admin-username azureuser --generate-ssh-keys
az vm run-command invoke --resource-group $RG --name vm-test --command-id RunShellScript \
  --scripts "nslookup $ST.blob.core.windows.net; curl -s -o /dev/null -w '%{http_code}\n' https://$ST.blob.core.windows.net/"
#   VNet içinden: 10.1.2.x (özel IP) — dışarıdan: public erişim kapalı olduğu için istek reddedilir

# 9) Temizlik — mutlaka
az group delete --name $RG --yes --no-wait
```

<details><summary>Beklenen gözlemler ve açıklamaları</summary>

| Adım | Gözlem | Kavram |
|---|---|---|
| 3 | Yalnızca `a-to-b` tanımlıyken durum `Initiated`; `b-to-a` eklenince `Connected` | Peering iki yönlü tanımlanır |
| 4 | Storage adı public bir IP'ye çözülür | Public endpoint |
| 6–8 | VM'den aynı ad `privatelink` üzerinden **10.1.2.x**'e çözülür | Private endpoint + private DNS zone |
| 7–8 | Yerel makineden blob isteği reddedilir (HTTP 403 / erişim yok), VM'den ağ erişimi vardır | Public erişim kapatıldı; trafik VNet içinde |
| (düşünce deneyi) | VNet-B'deki bir VM de private endpoint'e erişebilir mi? | Peering'li VNet'ten özel IP'ye erişim mümkündür, ama DNS zone'un VNet-B'ye de bağlanması gerekir |

`curl` çıktısındaki HTTP kodu kimlik doğrulama olmadığı için 4xx olabilir; önemli olan isteğin **özel IP üzerinden ulaşmasıdır**.
</details>

---

## 4. Gerçek dünya senaryosu

Türkiye'de üç fabrikası ve İstanbul'da bir genel merkezi olan bir üretim şirketi Azure'a geçiyor:

1. **Adres planı:** on-prem `10.0.0.0/12` kullanıyor; Azure VNet'leri çakışmasın diye `10.100.0.0/16` (üretim) ve `10.101.0.0/16` (test) seçiliyor. Üretim VNet'i web, uygulama ve veri **subnet**'lerine bölünüyor; her subnet'e bir **NSG**.
2. **Genel merkez ↔ Azure:** ERP verisi büyük ve gecikmeye duyarlı → **ExpressRoute** (bir operatör üzerinden). Aynı bağlantının yedeği olarak **site-to-site VPN Gateway**.
3. **Fabrikalar ↔ Azure:** küçük veri hacmi → **site-to-site VPN**. Fabrikaları birbirine bağlamak için ayrıca **ExpressRoute Global Reach** değerlendiriliyor.
4. **Evden çalışan mühendisler:** **point-to-site VPN** ile yalnızca test VNet'ine.
5. **Üretim ↔ test VNet'i:** **VNet peering** (internete çıkmadan); test VNet'inin, üretimin peering yaptığı paylaşılan servis VNet'ine erişemediği fark ediliyor — peering **geçişli değil** — ayrı bir peering ekleniyor.
6. **Veritabanı ve storage:** Azure SQL Database ve storage account'lara **private endpoint**; public erişim kapatılıyor; isim çözümleme için **private DNS zone**'lar hem Azure VNet'lerine hem (DNS forwarder ile) on-prem'e bağlanıyor.
7. **Şirketin public sitesi:** `contoso.com.tr` domain'i registrar'dan alınmış; DNS kayıtları **Azure DNS**'te barındırılıyor, zone'a **CanNotDelete lock** (→ C1.12).

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Bağlantı seçimi senaryoları, VNet/peering/DNS yetenekleri hakkında evet/hayır tabloları ve "public mi private endpoint mi" ayrımı.

| Senaryodaki ifade | Cevap |
|---|---|
| "On-prem ile Azure arasında **internetten geçmeyen**, özel, yüksek bant genişlikli bağlantı" | **ExpressRoute** |
| "On-prem ile Azure arasında **şifreli**, **internet üzerinden**, düşük maliyetli bağlantı" | **VPN Gateway (site-to-site)** |
| "Tek bir geliştiricinin dizüstü bilgisayarından VNet'e bağlanmak" | **Point-to-site VPN** |
| "İki Azure VNet'ini **Microsoft omurgası** üzerinden bağlamak" | **VNet peering** |
| "Farklı region'lardaki iki VNet'i bağlamak" | **Global VNet peering** |
| "Kaynakları ağ içinde gruplayıp ayrı kurallar uygulamak" | **Subnet** (+ NSG) |
| "DNS kayıtlarını Azure'da barındırmak, RBAC ile yönetmek" | **Azure DNS** |
| "VNet içinde özel isim çözümleme" | **Private DNS zone** |
| "PaaS servisine **özel IP** ile, trafik Microsoft ağında kalarak erişmek; public erişimi kapatmak" | **Private endpoint** (Private Link) |
| "Şube ağlarını Microsoft omurgasıyla birbirine bağlamak" | **ExpressRoute Global Reach** |

**Tuzaklar:**
- "Peering transitive'tir" → **Hayır**.
- "Azure DNS ile domain satın alabilirsiniz" → **Hayır** (barındırır).
- "ExpressRoute trafiği public internet üzerinden şifreli olarak taşır" → **Hayır** (özel bağlantı; o tanım VPN'indir).
- "VPN Gateway, iki Azure VNet'ini bağlamanın **tek** yoludur" → **Hayır** (peering de var; VNet-to-VNet VPN de bir seçenek).
- "Bir VNet birden çok region'a yayılır" → **Hayır** (tek region; birden çok **zone**'a yayılır).
- "VNet'ler arası peering için adres alanları çakışabilir" → **Hayır**.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| VNet adres alanını on-prem ile çakışacak şekilde seçmek | Baştan bir IP adres planı; çakışan ağlar peering/VPN ile bağlanamaz |
| Peering'i tek yönlü tanımlayıp "çalışmıyor" demek | İki yönlü tanımlanır (`Connected` durumu) |
| A↔B↔C peering'iyle A'nın C'ye ulaşmasını beklemek | Peering geçişli değildir; A↔C peering'i veya hub'da router/firewall gerekir |
| "ExpressRoute = çok hızlı VPN" | VPN internet üzerinden şifreli tüneldir; ExpressRoute internete çıkmayan özel hattır |
| Private endpoint kurup DNS'i unutmak | Servis adı hâlâ public IP'ye çözülür; private DNS zone ve VNet bağlantısı gerekir |
| Private endpoint ekleyip public erişimi açık bırakmak | Amaç izolasyonsa public network access **kapatılmalı** |
| Laboratuvarda VPN Gateway kurup unutmak | Gateway saatlik ücretlenir ve kurulumu uzun sürer; deneme bitince sil |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| VPN Gateway (S2S) | Hızlı kurulum, düşük maliyet, şifreli | İnternete bağlı değişken gecikme/bant genişliği; gateway SKU'su verimi sınırlar |
| ExpressRoute | Yüksek, öngörülebilir performans; internete çıkmama; M365 erişimi | Yüksek maliyet, operatör sözleşmesi, kurulum süresi; şifreleme ayrıca planlanmalı |
| ExpressRoute + VPN yedeği | Yüksek erişilebilirlik | İki bağlantının maliyeti ve yönetimi |
| VNet peering | Düşük gecikme, basit, gateway yok | Geçişli değil — çok VNet'te peering sayısı katlanır (hub-spoke tasarımı → S8.3); veri transferi ücreti |
| Private endpoint | Public erişimi kapatma, veri sızıntısı riskini azaltma | Saatlik endpoint + işlenen veri ücreti; DNS yönetimi karmaşıklığı |
| Public endpoint + firewall kuralları | Basit, ek maliyet yok | Adres internete açık; yanlış yapılandırma riski |

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure compute and networking services](https://learn.microsoft.com/en-us/training/modules/describe-azure-compute-networking-services/)
- [What is Azure Virtual Network?](https://learn.microsoft.com/en-us/azure/virtual-network/virtual-networks-overview) · [Virtual network peering](https://learn.microsoft.com/en-us/azure/virtual-network/virtual-network-peering-overview)
- [What is Azure DNS?](https://learn.microsoft.com/en-us/azure/dns/dns-overview) · [Azure Private DNS](https://learn.microsoft.com/en-us/azure/dns/private-dns-overview)
- [What is VPN Gateway?](https://learn.microsoft.com/en-us/azure/vpn-gateway/vpn-gateway-about-vpngateways) · [VPN Gateway — What's new (SKU değişiklikleri)](https://learn.microsoft.com/en-us/azure/vpn-gateway/whats-new)
- [What is ExpressRoute?](https://learn.microsoft.com/en-us/azure/expressroute/expressroute-introduction) · [ExpressRoute Global Reach](https://learn.microsoft.com/en-us/azure/expressroute/expressroute-global-reach)
- [What is a private endpoint?](https://learn.microsoft.com/en-us/azure/private-link/private-endpoint-overview) · [Private endpoint DNS configuration](https://learn.microsoft.com/en-us/azure/private-link/private-endpoint-dns)
- [`az network vnet peering`](https://learn.microsoft.com/en-us/cli/azure/network/vnet/peering) · [`az network private-endpoint`](https://learn.microsoft.com/en-us/cli/azure/network/private-endpoint)
