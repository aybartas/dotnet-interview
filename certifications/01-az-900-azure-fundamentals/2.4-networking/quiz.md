[← Konu anlatımı](README.md) · [AZ-900 İçindekiler](../README.md)

### 📝 Bölüm 2.4 Quiz Soruları

> Teori: [README.md](README.md) · Sınav alanı: *Describe Azure architecture and services* (%35-40) · 21 soru
>
> Sorular **özgündür** ve sınav formatındadır; gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 17/21**.

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir şirket, genel merkezini Azure'a **public internetten geçmeyen**, özel ve yüksek bant genişlikli bir bağlantıyla bağlamak istiyor. Hangi servisi kullanmalıdır?

- A) VPN Gateway (site-to-site)
- B) ExpressRoute
- C) VNet peering
- D) Azure DNS

<details><summary>Cevap</summary>

**B.** ExpressRoute, bağlantı sağlayıcısı üzerinden kurulan, internete çıkmayan özel bağlantıdır. VPN Gateway şifrelidir ama trafiği public internet üzerinden taşır.
</details>

#### Soru 2: Tek seçim
Küçük bir ofis, Azure'daki VNet'ine **düşük maliyetle** ve **şifreli** bir bağlantı istiyor; bağlantının internet üzerinden geçmesi sorun değil. Hangisi **en uygundur**?

- A) ExpressRoute Direct
- B) Site-to-site VPN (VPN Gateway)
- C) VNet peering
- D) Private endpoint

<details><summary>Cevap</summary>

**B.** Site-to-site VPN, internet üzerinden IPsec/IKE ile şifreli tünel kurar; hızlı ve ucuzdur.
</details>

#### Soru 3: Tek seçim
Bir geliştirici, evdeki dizüstü bilgisayarından bir Azure VNet'indeki test VM'lerine güvenli olarak bağlanmak istiyor. Hangi VPN bağlantı türü **en uygundur**?

- A) Site-to-site
- B) Point-to-site
- C) VNet-to-VNet
- D) ExpressRoute

<details><summary>Cevap</summary>

**B.** Point-to-site, tek bir istemciyi VNet'e bağlar; site-to-site bütün bir ağı (ofis/datacenter) bağlar.
</details>

#### Soru 4: Evet / Hayır

- a) VNet peering ile bağlanan VNet'ler arasındaki trafik Microsoft omurga ağında kalır. — Evet: ☐ · Hayır: ☐
- b) VNet-A, VNet-B ile; VNet-B de VNet-C ile peering yaptıysa, VNet-A ile VNet-C doğrudan iletişim kurabilir. — Evet: ☐ · Hayır: ☐
- **c) Farklı region'lardaki iki VNet peering ile bağlanabilir.** — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Peering internete çıkmaz; **geçişli değildir** (A↔C ayrı peering ister); farklı region'lar için **global peering** vardır.
</details>

#### Soru 5: Tek seçim
Aynı subscription'daki iki Azure VNet'ini, **gateway kurmadan**, düşük gecikmeyle birbirine bağlamanın en basit yolu hangisidir?

- A) Site-to-site VPN
- B) ExpressRoute
- C) VNet peering
- D) Public endpoint

<details><summary>Cevap</summary>

**C.** Peering iki VNet'i doğrudan Microsoft omurgası üzerinden bağlar; gateway gerekmez. VNet-to-VNet VPN de mümkündür ama gateway gerektirir ve daha yavaştır.
</details>

#### Soru 6: Evet / Hayır

- **a) Bir Azure VNet birden çok region'a yayılabilir.** — Evet: ☐ · Hayır: ☐
- b) Bir VNet, adres alanını subnet'lere bölerek kaynakları segmentlere ayırmaya olanak tanır. — Evet: ☐ · Hayır: ☐
- **c) Peering yapılacak iki VNet'in adres alanları çakışabilir.** — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** VNet tek bir region'dadır (o region'ın zone'larına yayılır); peering için adres alanları çakışmamalıdır.
</details>

#### Soru 7: Çoklu seçim (iki doğru cevap)
Azure Virtual Network'ün **hangi iki** yeteneği resmî tanımda yer alır?

- A) Ağı subnet'lere bölerek izole etme (segmentasyon)
- B) Domain adı satın alma
- C) On-prem ağlarla iletişim (VPN/ExpressRoute)
- D) Uygulama kodunu derleme

<details><summary>Cevap</summary>

**A ve C.** VNet: izolasyon/segmentasyon, internet iletişimi, Azure kaynakları arası iletişim, on-prem ile iletişim, trafik yönlendirme, trafik filtreleme. Domain satın almak VNet'in (veya Azure DNS'in) işi değildir.
</details>

#### Soru 8: Tek seçim
Bir şirket, `contoso.com` domain'inin DNS kayıtlarını Azure'da barındırmak ve kayıtları kimin değiştirebileceğini **Azure RBAC** ile yönetmek istiyor. Hangi servisi kullanmalıdır?

- A) Azure DNS
- B) Azure Virtual Network
- C) VPN Gateway
- D) ExpressRoute

<details><summary>Cevap</summary>

**A.** Azure DNS, DNS zone'larını barındırır; RBAC, activity log ve resource lock ile yönetilir.
</details>

#### Soru 9: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure DNS kullanarak yeni bir domain adı satın alabilirsiniz. | ☐ | ☐ |
| b) Private DNS zone, VNet içinde özel isim çözümlemesi sağlar. | ☐ | ☐ |
| c) Azure DNS kayıtları Azure portal ve Azure CLI ile yönetilebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Azure DNS domain **barındırır**, satmaz; domain bir registrar'dan alınır ve NS kayıtları Azure DNS'e yönlendirilir.
</details>

#### Soru 10: Senaryo
Bir uygulama ekibi, Azure SQL Database'e yalnızca VNet içinden, **özel bir IP adresi** üzerinden erişilmesini ve veritabanının **public erişiminin tamamen kapatılmasını** istiyor. Ne kullanmalıdır?

- A) Public endpoint
- B) Private endpoint (Azure Private Link)
- C) VNet peering
- D) Azure DNS public zone

<details><summary>Cevap</summary>

**B.** Private endpoint, PaaS servisine VNet'ten özel IP ile erişim sağlar; trafik Microsoft ağında kalır ve public erişim kapatılabilir.
</details>

#### Soru 11: Tek seçim
Bir storage account'a private endpoint eklendi, fakat VNet içindeki VM'ler storage adını hâlâ **public IP**'ye çözüyor. En olası eksik nedir?

- A) Storage account'un redundancy türü
- B) Private DNS zone'un (`privatelink.blob.core.windows.net`) oluşturulup VNet'e bağlanması
- C) VPN Gateway kurulması
- D) VM'in boyutunun büyütülmesi

<details><summary>Cevap</summary>

**B.** Private endpoint'in isimle çalışması için servis adının VNet içinde özel IP'ye çözülmesi gerekir; bunu private DNS zone ve VNet bağlantısı sağlar.
</details>

#### Soru 12: Eşleştirme
Her ihtiyacı bağlantı seçeneğiyle eşleştirin: **VNet peering · Site-to-site VPN · ExpressRoute · Point-to-site VPN**

1. Şirket datacenter'ı ↔ Azure, internetten geçmeden, öngörülebilir gecikme
2. Ofis ağı ↔ Azure, internet üzerinden şifreli tünel
3. Azure VNet ↔ Azure VNet, Microsoft omurgasında
4. Tek bir kullanıcının bilgisayarı ↔ Azure VNet

<details><summary>Cevap</summary>

**1 → ExpressRoute · 2 → Site-to-site VPN · 3 → VNet peering · 4 → Point-to-site VPN.**
</details>

#### Soru 13: Tek seçim
Bir şirket, VNet içindeki sunucuların `db.internal.contoso.com` gibi **yalnızca iç ağda çözülen** isimlerle birbirine ulaşmasını istiyor. İsimler internete açık olmayacak. Hangisi kullanılmalıdır?

- A) Azure Private DNS zone
- B) Genel (public) DNS zone
- C) Public IP adresi
- D) ExpressRoute

<details><summary>Cevap</summary>

**A.** **Private DNS zone**, VNet içinde internete açık olmayan özel isim çözümleme sağlar. Public DNS zone isimleri internete açar; public IP ve ExpressRoute isim çözümleme servisi değildir.
</details>

#### Soru 14: Tek seçim
Bir VM'in, internetten **doğrudan erişilebilir** olması gerekmiyor; yalnızca aynı VNet'teki diğer VM'lerle konuşacak. Bu VM için hangisi doğru yaklaşımdır?

- A) VM'e yalnızca private IP vermek (public IP eklememek)
- B) VM'e mutlaka statik bir public IP eklemek
- C) VM'i başka bir region'a taşımak
- D) VM için ExpressRoute devresi kurmak

<details><summary>Cevap</summary>

**A.** Private IP, VNet içi (ve bağlı ağlardan) iletişim içindir; **public IP isteğe bağlıdır** ve yalnızca internetten erişim gereken kaynaklara eklenir. Gereksiz public IP saldırı yüzeyini ve maliyeti artırır.
</details>

#### Soru 15: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) ExpressRoute ile Microsoft 365 gibi Microsoft bulut servislerine de bağlanılabilir. | ☐ | ☐ |
| b) Site-to-site VPN, on-prem ile Azure'u internet üzerinden şifreli tünelle bağlar. | ☐ | ☐ |
| c) ExpressRoute trafiği public internet üzerinden şifreli bir tünelle taşır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** ExpressRoute özel bir bağlantıdır; trafik public internete çıkmaz. "İnternet üzerinden şifreli tünel" VPN Gateway'in tanımıdır.
</details>

#### Soru 16: Tek seçim
Bir VNet'te web sunucularını ve veritabanı sunucularını ayrı gruplara ayırıp her gruba farklı ağ güvenlik kuralları uygulamak istiyorsunuz. Ne kullanmalısınız?

- A) Ayrı subnet'ler (ağ segmentasyonu)
- B) Ayrı Azure DNS zone'ları
- C) ExpressRoute devreleri
- D) Ayrı resource group'lar

<details><summary>Cevap</summary>

**A.** Subnet'ler ağ segmentasyonu sağlar; her subnet'e ayrı güvenlik kuralları uygulanabilir. Resource group ağ izolasyonu sağlamaz (→ C1.4).
</details>

#### Soru 17: Tek seçim
Aşağıdakilerden hangisi bir **public endpoint**'tir?

- A) Bir storage account'un `mystorage.blob.core.windows.net` adresinin internetten erişilebilir olması
- B) VNet içindeki `10.1.2.5` özel IP'li private endpoint
- C) Bir VM'in yalnızca subnet içinden erişilebilen NIC'i
- D) Private DNS zone'daki bir A kaydı

<details><summary>Cevap</summary>

**A.** Public endpoint, kaynağa internetten erişilen genel adrestir. B, C ve D özel ağ içi adreslemedir.
</details>

#### Soru 18: Senaryo
Bir şirket üç Azure VNet'ini (hub, spoke-1, spoke-2) birbirine bağlıyor: hub↔spoke-1 ve hub↔spoke-2 peering'leri var. spoke-1'deki VM'ler spoke-2'deki VM'lere ulaşamıyor. Neden?

- A) VNet peering yalnızca farklı region'lar arasında çalışır
- B) VNet peering geçişli değildir; spoke-1↔spoke-2 için ayrı bir peering gerekir
- C) Spoke VNet'lerin DNS zone'u yoktur
- D) Peering için ExpressRoute zorunludur

<details><summary>Cevap</summary>

**B.** Peering transitive (geçişli) değildir. Çözüm: spoke-1↔spoke-2 arasında doğrudan ayrı bir peering tanımlamaktır.
</details>

#### Soru 19: Evet / Hayır

- a) Private endpoint, bir PaaS servisine VNet içindeki özel bir IP adresiyle erişim sağlar. — Evet: ☐ · Hayır: ☐
- b) Public endpoint kullanan bir servise internetten erişilebilir; erişim kimlik doğrulama ve erişim kurallarıyla kısıtlanabilir. — Evet: ☐ · Hayır: ☐
- c) Bir servise private endpoint eklendiğinde, servisin public erişimi her zaman otomatik olarak kapanır. — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Private endpoint özel erişim yolu ekler; public erişim ayrıca ve bilinçli olarak **kapatılmalıdır**. Amaç izolasyonsa private endpoint + public erişimin kapatılması birlikte uygulanır.
</details>

#### Soru 20: Tek seçim
Bir şirket VNet'ini web, uygulama ve veri katmanları için **ayrı subnet'lere** bölüyor; veri subnet'ine yalnızca uygulama subnet'inden erişilebilecek. Bu yaklaşımın **temel güvenlik faydası** nedir?

- A) Tüm kaynakların internetten erişilebilir olması
- B) Ağ segmentasyonu: bir katman ele geçirilse bile saldırganın diğer katmanlara yayılması sınırlanır
- C) VM'lerin daha hızlı başlaması
- D) Storage maliyetinin düşmesi

<details><summary>Cevap</summary>

**B.** Subnet'lerle **ağ segmentasyonu**, her segmente ayrı kurallar uygulanmasını ve yatay hareketin sınırlanmasını sağlar; defense in depth'in **ağ** katmanıdır (→ C1.10).
</details>

#### Soru 21: Senaryo
Bir şirket, kurum içi datacenter'ı ile Azure arasında büyük hacimli ERP verisini **öngörülebilir gecikmeyle** taşımak istiyor ve trafiğin **public internete çıkmaması** şart. Ayrıca evden çalışan birkaç yöneticinin dizüstü bilgisayarlarından Azure VNet'e güvenli bağlanması gerekiyor. Hangi kombinasyon **en uygundur**?

- A) Datacenter için point-to-site VPN, yöneticiler için ExpressRoute
- B) Datacenter için ExpressRoute, yöneticiler için point-to-site VPN
- C) Her ikisi için VNet peering
- D) Her ikisi için Azure DNS

<details><summary>Cevap</summary>

**B.** İnternete çıkmayan, öngörülebilir gecikmeli kurumsal bağlantı **ExpressRoute**'tur; tek cihazların güvenli bağlantısı **point-to-site VPN**'dir. Peering yalnızca Azure VNet'leri arasındadır; Azure DNS isim çözümleme servisidir.
</details>

---

**Puanlama:** 19-21 → hazırsın · 15-18 → README 3.5'teki "VPN Gateway vs ExpressRoute" tablosunu ve peering kurallarını tekrar et · < 15 → konuyu baştan çalış ve 3.7'deki laboratuvarı uygula.

---

➡️ **Sonraki konu:** [2.5 Azure Storage: Blob, Files, Redundancy, Tiering](../2.5-storage/README.md) · [AZ-900 İçindekiler](../README.md)
