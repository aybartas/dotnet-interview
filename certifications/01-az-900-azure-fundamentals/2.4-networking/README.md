[← AZ-900 İçindekiler](../README.md) · [📝 Quiz](quiz.md) · [← Önceki konu](../2.3-app-hosting-containers-functions-avd/README.md)

## 2.4 Ağ Servisleri: VNet, Peering, VPN, ExpressRoute, DNS

> **Sınav Alanı:** Describe Azure architecture and services (%35-40) · **Konu Kodu:** C1.7
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe Azure compute and networking services* — virtual networking: **VNet, subnet, peering, Azure DNS, VPN Gateway, ExpressRoute** · **public ve private endpoint**'ler. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

### 1. Ne?

Azure'daki kaynakların (VM, App Service, veritabanı…) birbirleriyle, internetle ve şirketin kendi datacenter'ıyla nasıl konuştuğunu belirleyen servisler:

| Servis | Tek cümle |
|---|---|
| **Virtual Network (VNet)** | Azure'daki kaynakların **izole**, özel ağı; bir region'a bağlıdır |
| **Subnet** | VNet'in alt ağı; kaynakları gruplar ve ağı segmentlere böler |
| **VNet peering** | İki VNet'i **Microsoft omurgası** üzerinden (internete çıkmadan) birbirine bağlar |
| **Public / private IP adresi** | Kaynağın internetten (public) veya yalnızca VNet içinden (private) ulaşılan adresi |
| **Azure DNS** | Domain'lerin DNS kayıtlarını Azure'da **barındırır** (domain satmaz) |
| **VPN Gateway** | On-prem ile Azure arasında **internet üzerinden şifreli** tünel |
| **ExpressRoute** | On-prem ile Azure arasında **internete çıkmayan özel** bağlantı |
| **Public endpoint** | Bir servise **internetten** erişilen genel adres |
| **Private endpoint** (Private Link) | PaaS servisine VNet içinden **özel IP** ile erişim |

Bu konunun kodu yok; pratik kısım `az` CLI laboratuvarıdır (3.7).

---

### 2. Neden?

- **Sınav:** "Hangi bağlantı seçeneği?" (VPN Gateway, ExpressRoute, peering) ve "public mi private endpoint mi?" soruları bu alanın klasikleridir; ayrımı tek bir anahtar ifade yapar ("internetten geçmeden", "şifreli tünel", "iki VNet").
- **Gerçek işte:** Ağ tasarımı güvenliğin (segmentasyon, private endpoint → C1.10 defense in depth) ve hybrid mimarinin (→ C1.1) temelidir. Yanlış adres planı (çakışan CIDR'ler) peering'i ve VPN'i sonradan imkânsız kılar.

---

### 3. Nasıl?

#### 3.1 Azure Virtual Network ve subnet'ler

**VNet**, Azure kaynaklarının birbirleriyle, internetle ve on-prem ağlarla **güvenli** iletişim kurmasını sağlayan özel ağdır.

- **Adres alanı:** özel IP aralığı, CIDR ile (ör. `10.0.0.0/16` → ~65 bin adres). Aynı adres alanına bağlanacak ağlarla (diğer VNet'ler, on-prem) **çakışmamalıdır**.
- **Kapsam:** bir VNet **tek bir region**'dadır, ama o region'ın tüm availability zone'larına yayılır; bir subscription'a aittir.
- **Subnet:** adres alanının alt aralığı (ör. `10.0.1.0/24` web, `10.0.2.0/24` veri); kaynakları gruplamak ve ağı **segmentlere** ayırmak için (her segmente ayrı güvenlik kuralları uygulanabilir).

VNet'in sağladığı yetenekler (resmî içerikte sayılan başlıklar):

- **İzolasyon ve segmentasyon**
  - Açıklama: Ağı subnet'lere bölüp birbirinden ayırmak
  - Örnek: Web / uygulama / veri subnet'leri
- **İnternet iletişimi**
  - Açıklama: Kaynağa **public IP** ile internetten erişim; giden trafik varsayılan olarak mümkün
  - Örnek: Public IP'li VM
- **Azure kaynakları arası iletişim**
  - Açıklama: Aynı VNet'teki kaynaklar doğrudan konuşur; PaaS servislerine private endpoint ile; diğer VNet'lere peering ile
  - Örnek: VM → storage account (private endpoint)
- **On-prem ile iletişim**
  - Açıklama: Point-to-site / site-to-site VPN, ExpressRoute
  - Örnek: Ofis ağı ↔ Azure
- **Trafik yönlendirme ve filtreleme**
  - Açıklama: Subnet/NIC düzeyinde izin/engel kuralları ve özel rotalar tanımlanabilir
  - Örnek: "Yalnızca 443'e izin ver"

**IP adresleme: public ve private IP**

- **Private IP adresi:** VNet'in adres alanından (ör. `10.0.1.4`) verilen, **yalnızca VNet içinden** (ve peering/VPN/ExpressRoute ile bağlı ağlardan) erişilen adres. Her NIC en az bir private IP taşır; varsayılan olarak **dinamik** atanır, istenirse **statik** yapılabilir.
- **Public IP adresi:** **internetten** erişilebilen adres; ayrı bir kaynaktır ve VM'in NIC'ine veya başka bir kaynağa bağlanır. Dinamik veya statik olabilir. **İsteğe bağlıdır** — prod VM'lerinin çoğunda yoktur — ve VM silinse bile ayrıca silinmedikçe ücretlenmeye devam edebilir (→ C1.5, C1.11).
- Kural: **iç iletişim** (VM ↔ VM, VM ↔ private endpoint) **private IP** ile; **internetten erişim** gereken giriş noktası için **public IP**.

**Ağ segmentasyonu:** VNet'i subnet'lere bölmek, bir saldırının yayılmasını sınırlar ve her segmente farklı kurallar uygulamayı mümkün kılar (ör. internete açık web subnet'i, yalnızca web subnet'inden erişilebilen veri subnet'i). Defense in depth'in **ağ** katmanıdır (→ C1.10).

#### 3.2 VNet peering

**Peering**, iki VNet'i **Microsoft'un omurga ağı** üzerinden bağlar; trafik **internete çıkmaz**, gateway gerekmez.

- **Regional peering:** aynı region'daki VNet'ler · **Global peering:** farklı region'lardaki VNet'ler.
- **Düşük gecikme, yüksek bant genişliği** — aynı ağdaymış gibi.
- **Kurallar:** adres alanları **çakışmamalı** · peering **iki yönlü** tanımlanır (her VNet'te bir peering bağlantısı) · **geçişli (transitive) değildir**: A↔B ve B↔C peering'i, A ile C'yi konuşturmaz (A↔C ayrıca peering ister) · farklı subscription ve hatta farklı tenant'lardaki VNet'ler de peering yapabilir.
- Ücret: peering'in kendisi değil, peering üzerinden geçen **veri transferi** ücretlendirilir (global peering daha pahalıdır) (→ C1.11).

#### 3.3 Azure DNS

**Azure DNS**, DNS domain'lerini Azure altyapısında barındıran servistir.

- **Güvenilirlik ve performans:** Azure'un dünya çapındaki ad sunucu ağı; anycast ile kullanıcıya en yakın sunucu cevap verir.
- **Yönetim kolaylığı:** kayıtlar portal, CLI, PowerShell, ARM template'leri ile yönetilir; aynı faturada, aynı kimlikle.
- **Güvenlik:** Azure **RBAC** (kim DNS kaydını değiştirebilir), **activity log** (kim neyi değiştirdi), **resource lock** (kritik zone'un silinmesini engelleme → C1.12).
- **Önemli:** Azure DNS ile **domain satın alınmaz** — domain bir kayıt kuruluşundan (registrar) alınır, NS kayıtları Azure DNS'e yönlendirilir. *(App Service Domains gibi ayrı bir satın alma seçeneği vardır ama bu Azure DNS'in kendisi değildir.)*
- **Private DNS zone:** VNet içinde, internete açık olmayan **özel isim çözümleme** (ör. `db.internal.contoso.com` → `10.0.2.4`); private endpoint'lerin özel IP'ye çözülmesi için de kullanılır.

#### 3.4 VPN Gateway

Bir **VPN**, iki ağ arasında **şifreli tünel** kurar. **Azure VPN Gateway**, bir VNet'e konuşlandırılan ve **public internet üzerinden** IPsec/IKE ile şifreli bağlantı sağlayan sanal ağ geçididir.

| Bağlantı türü | Ne bağlanır? |
|---|---|
| **Site-to-site (S2S)** | Ofis/datacenter ağı ↔ Azure VNet (her iki uçta VPN cihazı) |
| **Point-to-site (P2S)** | Tek bir istemci (geliştiricinin dizüstü bilgisayarı) ↔ Azure VNet |

- S2S, **iki ağı** kalıcı olarak birbirine bağlar; P2S ise **tek bir cihazın** uzaktan bağlanması içindir.
- Kurulumu hızlı ve maliyeti düşüktür; ancak trafik **internetten** geçtiği için gecikme ve bant genişliği internete bağlı değişkendir.

#### 3.5 ExpressRoute

**ExpressRoute**, on-prem ağı Microsoft bulutuna bir **bağlantı sağlayıcısı** (telekom operatörü) üzerinden bağlayan **özel** bir bağlantıdır; trafik **public internete çıkmaz**.

- **Faydaları:** daha **yüksek bant genişliği**, daha **öngörülebilir gecikme**, daha yüksek **güvenilirlik**; Azure'un yanında **Microsoft 365** servislerine de bağlanabilir.
- **Önemli:** ExpressRoute **özel** bir hattır ama varsayılan olarak **şifreli tünel değildir**; VPN ise internet üzerinden **şifreli** tüneldir.

**VPN Gateway vs ExpressRoute** ⭐

- **Trafik nereden geçer?**
  - VPN Gateway: **Public internet** (şifreli tünel)
  - ExpressRoute: **Özel bağlantı** — internete çıkmaz
- **Kurulum**
  - VPN Gateway: Hızlı; VPN cihazı + gateway
  - ExpressRoute: Bağlantı sağlayıcısıyla sözleşme, devre tahsisi (haftalar)
- **Bant genişliği / gecikme**
  - VPN Gateway: Daha düşük, internete bağlı değişken
  - ExpressRoute: Yüksek, öngörülebilir
- **Maliyet** — VPN Gateway: Düşük · ExpressRoute: Yüksek
- **Tipik kullanım**
  - VPN Gateway: Küçük/orta ofis, geliştirici erişimi (P2S)
  - ExpressRoute: Büyük veri hacmi, kritik hybrid iş yükleri, regülasyon

**Hybrid networking:** On-prem ve Azure'u birlikte kullanan (hybrid) mimarilerde ağ bağlantısı VPN Gateway (hızlı, ucuz, internet üzerinden) veya ExpressRoute (özel, öngörülebilir) ile kurulur; adres alanları çakışmamalıdır.

#### 3.6 Public ve private endpoint'ler

- **Public endpoint:** bir kaynağın internetten erişilebilen adresi (ör. `mystorage.blob.core.windows.net`, `myapp.azurewebsites.net`). Erişim firewall kuralları, kimlik doğrulama ve TLS ile korunur ama adres **internete açıktır**.
- **Private endpoint** (**Azure Private Link**): bir PaaS servisine (Storage, SQL Database…) VNet'inin içinden bir **özel IP** ile bağlanan ağ arayüzü.
  - Trafik **Microsoft ağında** kalır; servisin **public erişimi tamamen kapatılabilir**.
  - On-prem ağdan da (VPN/ExpressRoute üzerinden) özel IP ile erişilebilir.
  - DNS: servis adı (`mystorage.blob.core.windows.net`) VNet içinde **private DNS zone** (`privatelink.blob.core.windows.net`) sayesinde özel IP'ye çözülür.

**Public ↔ private endpoint karşılaştırması**

- **Erişim adresi** — Public endpoint: İnternete açık adres (public IP) · Private endpoint: VNet'teki **özel IP**
- **Trafik** — Public endpoint: Public internet üzerinden · Private endpoint: **Microsoft ağında**, VNet içinde
- **Public erişim** — Public endpoint: Açık (firewall kurallarıyla kısıtlanabilir) · Private endpoint: Servis için **tamamen kapatılabilir**
- **Ne zaman?** — Public endpoint: Herkese açık içerik, basit senaryo · Private endpoint: Hassas veri, kurumsal/regüle ortam

#### 3.7 Pratik — VNet, peering, private DNS ve private endpoint

> Bütçe alarmın kurulu olsun (→ C1.1). VNet, subnet ve peering bağlantısının kendisi ücretsizdir (peering'de **veri transferi** ücretlidir); private DNS zone ve private endpoint **saatlik/aylık küçük ücret** doğurur — laboratuvarı tek oturumda bitirip **sil**. VPN Gateway ve ExpressRoute **kurmuyoruz** (gateway'in kurulumu ~30-45 dk sürer ve sürekli ücretlenir; ExpressRoute sağlayıcı sözleşmesi ister).

```bash
az login
RG=rg-az900-c17; LOC=westeurope
az group create --name $RG --location $LOC

# 1) VNet-A: two subnets (app + private endpoint)
az network vnet create --resource-group $RG --name vnet-a --address-prefixes 10.1.0.0/16 \
  --subnet-name snet-app --subnet-prefixes 10.1.1.0/24
az network vnet subnet create --resource-group $RG --vnet-name vnet-a --name snet-pe --address-prefixes 10.1.2.0/24

# 2) VNet-B: non-overlapping address space
az network vnet create --resource-group $RG --name vnet-b --address-prefixes 10.2.0.0/16 \
  --subnet-name snet-app --subnet-prefixes 10.2.1.0/24

# 3) Peering — defined in BOTH directions
az network vnet peering create --resource-group $RG --name a-to-b --vnet-name vnet-a --remote-vnet vnet-b --allow-vnet-access
az network vnet peering create --resource-group $RG --name b-to-a --vnet-name vnet-b --remote-vnet vnet-a --allow-vnet-access
az network vnet peering list --resource-group $RG --vnet-name vnet-a --query "[].{name:name,state:peeringState}" --output table
#   peeringState should be Connected (stays "Initiated" if only one direction is defined)

# 4) Storage account (starts with its public endpoint)
ST=stc17$RANDOM
az storage account create --resource-group $RG --name $ST --location $LOC --sku Standard_LRS --kind StorageV2
nslookup $ST.blob.core.windows.net              # on your local machine: resolves to a public IP

# 5) Private DNS zone + VNet link
az network private-dns zone create --resource-group $RG --name privatelink.blob.core.windows.net
az network private-dns link vnet create --resource-group $RG --zone-name privatelink.blob.core.windows.net \
  --name link-vnet-a --virtual-network vnet-a --registration-enabled false

# 6) Private endpoint (for the blob sub-resource) + DNS zone group (writes the A record automatically)
az network private-endpoint create --resource-group $RG --name pe-$ST \
  --vnet-name vnet-a --subnet snet-pe \
  --private-connection-resource-id $(az storage account show -g $RG -n $ST --query id -o tsv) \
  --group-id blob --connection-name pe-$ST-conn
az network private-endpoint dns-zone-group create --resource-group $RG --endpoint-name pe-$ST \
  --name default --private-dns-zone privatelink.blob.core.windows.net --zone-name blob

# 7) Disable public access
az storage account update --resource-group $RG --name $ST --public-network-access Disabled

# 8) Inspect the private endpoint: it received a private IP from snet-pe (10.1.2.x)
az network private-endpoint show --resource-group $RG --name pe-$ST \
  --query "{Subnet:subnet.id, Dns:customDnsConfigs[0].fqdn, PrivateIp:customDnsConfigs[0].ipAddresses[0]}" --output json
# Public access is now disabled: a request from your own machine is rejected
curl -s -o /dev/null -w '%{http_code}\n' https://$ST.blob.core.windows.net/    # 403 (public network access disabled)

# 9) Cleanup — mandatory
az group delete --name $RG --yes --no-wait
```

<details><summary>Beklenen gözlemler ve açıklamaları</summary>

- **3**
  - Gözlem: Yalnızca `a-to-b` tanımlıyken durum `Initiated`; `b-to-a` eklenince `Connected`
  - Kavram: Peering iki yönlü tanımlanır
- **4**
  - Gözlem: Storage adı public bir IP'ye çözülür
  - Kavram: Public endpoint
- **6–8**
  - Gözlem: Private endpoint, `snet-pe` subnet'inden **10.1.2.x** özel IP alır; yerel makineden blob isteği reddedilir (HTTP 403)
  - Kavram: Private endpoint + public erişimin kapatılması; trafik VNet içinde
- **(düşünce deneyi)**
  - Gözlem: VNet-B'deki bir VM de private endpoint'e erişebilir mi?
  - Kavram: Peering'li VNet'ten özel IP'ye erişim mümkündür, ama DNS zone'un VNet-B'ye de bağlanması gerekir

Önemli olan, servise erişimin **özel IP üzerinden** yapılabilmesi ve public erişimin kapatılmasıdır.
</details>

---

### 4. Gerçek dünya senaryosu

Türkiye'de üç fabrikası ve İstanbul'da bir genel merkezi olan bir üretim şirketi Azure'a geçiyor:

1. **Adres planı:** on-prem `10.0.0.0/12` kullanıyor; Azure VNet'leri çakışmasın diye `10.100.0.0/16` (üretim) ve `10.101.0.0/16` (test) seçiliyor. Üretim VNet'i web, uygulama ve veri **subnet**'lerine bölünüyor (ağ segmentasyonu); yalnızca web katmanının public IP'si var.
2. **Genel merkez ↔ Azure:** ERP verisi büyük ve gecikmeye duyarlı → **ExpressRoute** (bir operatör üzerinden). Geçiş döneminde ise hızlı kurulan **site-to-site VPN Gateway** kullanılıyor.
3. **Fabrikalar ↔ Azure:** küçük veri hacmi → **site-to-site VPN**.
4. **Evden çalışan mühendisler:** **point-to-site VPN** ile yalnızca test VNet'ine.
5. **Üretim ↔ test VNet'i:** **VNet peering** (internete çıkmadan); test VNet'inin, üretimin peering yaptığı paylaşılan servis VNet'ine erişemediği fark ediliyor — peering **geçişli değil** — ayrı bir peering ekleniyor.
6. **Veritabanı ve storage:** Azure SQL Database ve storage account'lara **private endpoint**; public erişim kapatılıyor; isim çözümleme için **private DNS zone**'lar hem Azure VNet'lerine hem (DNS forwarder ile) on-prem'e bağlanıyor.
7. **Şirketin public sitesi:** `contoso.com.tr` domain'i registrar'dan alınmış; DNS kayıtları **Azure DNS**'te barındırılıyor, zone'a **CanNotDelete lock** (→ C1.12).

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Bağlantı seçimi senaryoları, VNet/peering/DNS yetenekleri hakkında evet/hayır tabloları ve "public mi private endpoint mi" ayrımı.

| Senaryodaki ifade | Cevap |
|---|---|
| "On-prem ile Azure arasında **internetten geçmeyen**, özel, yüksek bant genişlikli bağlantı" | **ExpressRoute** |
| "On-prem ile Azure arasında **şifreli**, **internet üzerinden**, düşük maliyetli bağlantı" | **VPN Gateway (site-to-site)** |
| "Tek bir geliştiricinin dizüstü bilgisayarından VNet'e bağlanmak" | **Point-to-site VPN** |
| "İki Azure VNet'ini **Microsoft omurgası** üzerinden bağlamak" | **VNet peering** |
| "Farklı region'lardaki iki VNet'i bağlamak" | **Global VNet peering** |
| "Kaynakları ağ içinde gruplayıp ayrı kurallar uygulamak" | **Subnet** (ağ segmentasyonu) |
| "Yalnızca VNet içinden erişilen adres" / "internetten erişilen adres" | **Private IP** / **Public IP** |
| "DNS kayıtlarını Azure'da barındırmak, RBAC ile yönetmek" | **Azure DNS** |
| "VNet içinde özel isim çözümleme" | **Private DNS zone** |
| "PaaS servisine **özel IP** ile, trafik Microsoft ağında kalarak erişmek; public erişimi kapatmak" | **Private endpoint** (Private Link) |

**Tuzaklar:**
- "Peering transitive'tir" → **Hayır**.
- "Azure DNS ile domain satın alabilirsiniz" → **Hayır** (barındırır).
- "ExpressRoute trafiği public internet üzerinden şifreli olarak taşır" → **Hayır** (özel bağlantı; o tanım VPN'indir).
- "Peering ve VPN aynı şeydir" → **Hayır** (peering Microsoft omurgasında, VPN internet üzerinde şifreli tünel; peering iki Azure VNet'i içindir).
- "Bir VNet birden çok region'a yayılır" → **Hayır** (tek region; birden çok **zone**'a yayılır).
- "VNet'ler arası peering için adres alanları çakışabilir" → **Hayır**.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| VNet adres alanını on-prem ile çakışacak şekilde seçmek | Baştan bir IP adres planı; çakışan ağlar peering/VPN ile bağlanamaz |
| Peering'i tek yönlü tanımlayıp "çalışmıyor" demek | İki yönlü tanımlanır (`Connected` durumu) |
| A↔B↔C peering'iyle A'nın C'ye ulaşmasını beklemek | Peering geçişli değildir; A↔C için ayrı peering gerekir |
| "ExpressRoute = çok hızlı VPN" | VPN internet üzerinden şifreli tüneldir; ExpressRoute internete çıkmayan özel hattır |
| Private endpoint kurup DNS'i unutmak | Servis adı hâlâ public IP'ye çözülür; private DNS zone ve VNet bağlantısı gerekir |
| Private endpoint ekleyip public erişimi açık bırakmak | Amaç izolasyonsa public network access **kapatılmalı** |
| Laboratuvarda VPN Gateway kurup unutmak | Gateway saatlik ücretlenir ve kurulumu uzun sürer; deneme bitince sil |

---

### 7. Performans & trade-off

- **VPN Gateway (S2S)**
  - Kazanç: Hızlı kurulum, düşük maliyet, şifreli
  - Bedel: İnternete bağlı değişken gecikme/bant genişliği
- **ExpressRoute**
  - Kazanç: Yüksek, öngörülebilir performans; internete çıkmama; M365 erişimi
  - Bedel: Yüksek maliyet, operatör sözleşmesi, kurulum süresi; varsayılan olarak şifreli değil
- **VNet peering**
  - Kazanç: Düşük gecikme, basit, gateway yok
  - Bedel: Geçişli değil — çok VNet'te peering sayısı katlanır; veri transferi ücreti
- **Private endpoint**
  - Kazanç: Public erişimi kapatma, veri sızıntısı riskini azaltma
  - Bedel: Saatlik endpoint + işlenen veri ücreti; DNS yönetimi karmaşıklığı
- **Public endpoint + erişim kuralları**
  - Kazanç: Basit, ek maliyet yok
  - Bedel: Adres internete açık; yanlış yapılandırma riski

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure compute and networking services](https://learn.microsoft.com/en-us/training/modules/describe-azure-compute-networking-services/)
- [What is Azure Virtual Network?](https://learn.microsoft.com/en-us/azure/virtual-network/virtual-networks-overview) · [Virtual network peering](https://learn.microsoft.com/en-us/azure/virtual-network/virtual-network-peering-overview)
- [What is Azure DNS?](https://learn.microsoft.com/en-us/azure/dns/dns-overview) · [Azure Private DNS](https://learn.microsoft.com/en-us/azure/dns/private-dns-overview)
- [What is VPN Gateway?](https://learn.microsoft.com/en-us/azure/vpn-gateway/vpn-gateway-about-vpngateways)
- [What is ExpressRoute?](https://learn.microsoft.com/en-us/azure/expressroute/expressroute-introduction)
- [What is a private endpoint?](https://learn.microsoft.com/en-us/azure/private-link/private-endpoint-overview) · [Private endpoint DNS configuration](https://learn.microsoft.com/en-us/azure/private-link/private-endpoint-dns)
- [`az network vnet peering`](https://learn.microsoft.com/en-us/cli/azure/network/vnet/peering) · [`az network private-endpoint`](https://learn.microsoft.com/en-us/cli/azure/network/private-endpoint)

---

➡️ **Sıradaki:** [📝 Bu konunun quiz soruları](quiz.md)
