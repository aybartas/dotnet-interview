# C1.7 Ağ Servisleri — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Describe Azure architecture and services* (%35-40) · 18 soru
>
> Sorular **özgündür** ve sınav formatındadır; gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 15/18**.

---

### 1. Tek seçim
Bir şirket, genel merkezini Azure'a **public internetten geçmeyen**, özel ve yüksek bant genişlikli bir bağlantıyla bağlamak istiyor. Hangi servisi kullanmalıdır?

- A) VPN Gateway (site-to-site)
- B) ExpressRoute
- C) VNet peering
- D) Azure DNS

<details><summary>Cevap</summary>

**B.** ExpressRoute, bağlantı sağlayıcısı üzerinden kurulan, internete çıkmayan özel bağlantıdır. VPN Gateway şifrelidir ama trafiği public internet üzerinden taşır.
</details>

### 2. Tek seçim
Küçük bir ofis, Azure'daki VNet'ine **düşük maliyetle** ve **şifreli** bir bağlantı istiyor; bağlantının internet üzerinden geçmesi sorun değil. Hangisi **en uygundur**?

- A) ExpressRoute Direct
- B) Site-to-site VPN (VPN Gateway)
- C) ExpressRoute Global Reach
- D) Private endpoint

<details><summary>Cevap</summary>

**B.** Site-to-site VPN, internet üzerinden IPsec/IKE ile şifreli tünel kurar; hızlı ve ucuzdur.
</details>

### 3. Tek seçim
Bir geliştirici, evdeki dizüstü bilgisayarından bir Azure VNet'indeki test VM'lerine güvenli olarak bağlanmak istiyor. Hangi VPN bağlantı türü **en uygundur**?

- A) Site-to-site
- B) Point-to-site
- C) VNet-to-VNet
- D) ExpressRoute

<details><summary>Cevap</summary>

**B.** Point-to-site, tek bir istemciyi VNet'e bağlar; site-to-site bütün bir ağı (ofis/datacenter) bağlar.
</details>

### 4. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) VNet peering ile bağlanan VNet'ler arasındaki trafik Microsoft omurga ağında kalır. | ☐ | ☐ |
| b) VNet-A, VNet-B ile; VNet-B de VNet-C ile peering yaptıysa, VNet-A ile VNet-C doğrudan iletişim kurabilir. | ☐ | ☐ |
| c) Farklı region'lardaki iki VNet peering ile bağlanabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Peering internete çıkmaz; **geçişli değildir** (A↔C ayrı peering ister); farklı region'lar için **global peering** vardır.
</details>

### 5. Tek seçim
Aynı subscription'daki iki Azure VNet'ini, **gateway kurmadan**, düşük gecikmeyle birbirine bağlamanın en basit yolu hangisidir?

- A) Site-to-site VPN
- B) ExpressRoute
- C) VNet peering
- D) Public endpoint

<details><summary>Cevap</summary>

**C.** Peering iki VNet'i doğrudan Microsoft omurgası üzerinden bağlar; gateway gerekmez. VNet-to-VNet VPN de mümkündür ama gateway gerektirir ve daha yavaştır.
</details>

### 6. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Bir Azure VNet birden çok region'a yayılabilir. | ☐ | ☐ |
| b) Bir VNet, adres alanını subnet'lere bölerek kaynakları segmentlere ayırmaya olanak tanır. | ☐ | ☐ |
| c) Peering yapılacak iki VNet'in adres alanları çakışabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** VNet tek bir region'dadır (o region'ın zone'larına yayılır); peering için adres alanları çakışmamalıdır.
</details>

### 7. Çoklu seçim (iki doğru cevap)
Azure Virtual Network'ün **hangi iki** yeteneği resmî tanımda yer alır?

- A) Ağ trafiğini NSG'lerle filtreleme
- B) Domain adı satın alma
- C) On-prem ağlarla iletişim (VPN/ExpressRoute)
- D) Uygulama kodunu derleme

<details><summary>Cevap</summary>

**A ve C.** VNet: izolasyon/segmentasyon, internet iletişimi, Azure kaynakları arası iletişim, on-prem ile iletişim, trafik yönlendirme, trafik filtreleme. Domain satın almak VNet'in (veya Azure DNS'in) işi değildir.
</details>

### 8. Tek seçim
Bir şirket, `contoso.com` domain'inin DNS kayıtlarını Azure'da barındırmak ve kayıtları kimin değiştirebileceğini **Azure RBAC** ile yönetmek istiyor. Hangi servisi kullanmalıdır?

- A) Azure DNS
- B) Azure Front Door
- C) VPN Gateway
- D) Azure Firewall

<details><summary>Cevap</summary>

**A.** Azure DNS, DNS zone'larını barındırır; RBAC, activity log ve resource lock ile yönetilir.
</details>

### 9. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure DNS kullanarak yeni bir domain adı satın alabilirsiniz. | ☐ | ☐ |
| b) Private DNS zone, VNet içinde özel isim çözümlemesi sağlar. | ☐ | ☐ |
| c) Azure DNS kayıtları Azure portal ve Azure CLI ile yönetilebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Azure DNS domain **barındırır**, satmaz; domain bir registrar'dan alınır ve NS kayıtları Azure DNS'e yönlendirilir.
</details>

### 10. Senaryo
Bir uygulama ekibi, Azure SQL Database'e yalnızca VNet içinden, **özel bir IP adresi** üzerinden erişilmesini ve veritabanının **public erişiminin tamamen kapatılmasını** istiyor. Ne kullanmalıdır?

- A) Public endpoint
- B) Private endpoint (Azure Private Link)
- C) ExpressRoute Global Reach
- D) Azure DNS public zone

<details><summary>Cevap</summary>

**B.** Private endpoint, PaaS servisine VNet'ten özel IP ile erişim sağlar; trafik Microsoft ağında kalır ve public erişim kapatılabilir.
</details>

### 11. Tek seçim
Bir storage account'a private endpoint eklendi, fakat VNet içindeki VM'ler storage adını hâlâ **public IP**'ye çözüyor. En olası eksik nedir?

- A) Storage account'un redundancy türü
- B) Private DNS zone'un (`privatelink.blob.core.windows.net`) oluşturulup VNet'e bağlanması
- C) VPN Gateway kurulması
- D) VM'in boyutunun büyütülmesi

<details><summary>Cevap</summary>

**B.** Private endpoint'in isimle çalışması için servis adının VNet içinde özel IP'ye çözülmesi gerekir; bunu private DNS zone ve VNet bağlantısı sağlar.
</details>

### 12. Eşleştirme
Her ihtiyacı bağlantı seçeneğiyle eşleştirin: **VNet peering · Site-to-site VPN · ExpressRoute · Point-to-site VPN**

1. Şirket datacenter'ı ↔ Azure, internetten geçmeden, öngörülebilir gecikme
2. Ofis ağı ↔ Azure, internet üzerinden şifreli tünel
3. Azure VNet ↔ Azure VNet, Microsoft omurgasında
4. Tek bir kullanıcının bilgisayarı ↔ Azure VNet

<details><summary>Cevap</summary>

**1 → ExpressRoute · 2 → Site-to-site VPN · 3 → VNet peering · 4 → Point-to-site VPN.**
</details>

### 13. Tek seçim
VPN Gateway'de **varsayılan** yüksek erişilebilirlik yapılandırması hangisidir?

- A) Active/active
- B) Active/standby
- C) Zone-redundant (her zaman)
- D) Yüksek erişilebilirlik yoktur

<details><summary>Cevap</summary>

**B.** Varsayılan olarak gateway iki instance ile **active/standby** çalışır; bakım veya arızada yedek instance kısa bir kesintiyle devralır. Active/active ve zone-redundant gateway'ler ek yapılandırma seçenekleridir.
</details>

### 14. Tek seçim
Bir şirketin farklı ülkelerdeki iki şubesi ayrı ExpressRoute devreleriyle Azure'a bağlı. Şubeler arası trafiği de Microsoft omurgası üzerinden taşımak istiyorlar. Hangi özellik bunu sağlar?

- A) ExpressRoute Global Reach
- B) Point-to-site VPN
- C) Private DNS zone
- D) Azure Load Balancer

<details><summary>Cevap</summary>

**A.** Global Reach, ExpressRoute devreleri üzerinden on-prem siteleri birbirine bağlar.
</details>

### 15. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) ExpressRoute ile Microsoft 365 gibi Microsoft bulut servislerine de bağlanılabilir. | ☐ | ☐ |
| b) VPN Gateway, ExpressRoute bağlantısı için yedek (failover) olarak kullanılabilir. | ☐ | ☐ |
| c) ExpressRoute trafiği public internet üzerinden şifreli bir tünelle taşır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** ExpressRoute özel bir bağlantıdır; trafik public internete çıkmaz. "İnternet üzerinden şifreli tünel" VPN Gateway'in tanımıdır.
</details>

### 16. Tek seçim
Bir VNet'te web sunucularını ve veritabanı sunucularını ayrı gruplara ayırıp her gruba farklı ağ güvenlik kuralları uygulamak istiyorsunuz. Ne kullanmalısınız?

- A) Ayrı subnet'ler ve her subnet'e bir NSG
- B) Ayrı Azure DNS zone'ları
- C) ExpressRoute devreleri
- D) Ayrı resource group'lar

<details><summary>Cevap</summary>

**A.** Subnet'ler ağ segmentasyonu sağlar; NSG'ler subnet (veya NIC) düzeyinde izin/engel kurallarıdır. Resource group ağ izolasyonu sağlamaz (→ C1.4).
</details>

### 17. Tek seçim
Aşağıdakilerden hangisi bir **public endpoint**'tir?

- A) Bir storage account'un `mystorage.blob.core.windows.net` adresinin internetten erişilebilir olması
- B) VNet içindeki `10.1.2.5` özel IP'li private endpoint
- C) Bir VM'in yalnızca subnet içinden erişilebilen NIC'i
- D) Private DNS zone'daki bir A kaydı

<details><summary>Cevap</summary>

**A.** Public endpoint, kaynağa internetten erişilen genel adrestir. B, C ve D özel ağ içi adreslemedir.
</details>

### 18. Senaryo
Bir şirket üç Azure VNet'ini (hub, spoke-1, spoke-2) birbirine bağlıyor: hub↔spoke-1 ve hub↔spoke-2 peering'leri var. spoke-1'deki VM'ler spoke-2'deki VM'lere ulaşamıyor. Neden?

- A) VNet peering yalnızca farklı region'lar arasında çalışır
- B) VNet peering geçişli değildir; spoke-1↔spoke-2 için ayrı peering ya da hub'da yönlendirme (ör. firewall/router) gerekir
- C) Spoke VNet'lerin DNS zone'u yoktur
- D) Peering için ExpressRoute zorunludur

<details><summary>Cevap</summary>

**B.** Peering transitive değildir. Çözüm: doğrudan spoke-1↔spoke-2 peering'i veya hub'daki bir NVA/Azure Firewall üzerinden route table ile yönlendirme (hub-spoke tasarımı → S8.3).
</details>

---

**Puanlama:** 16-18 → hazırsın · 12-15 → README 3.5'teki "VPN Gateway vs ExpressRoute" tablosunu ve peering kurallarını tekrar et · < 12 → konuyu baştan çalış ve 3.7'deki laboratuvarı uygula.
