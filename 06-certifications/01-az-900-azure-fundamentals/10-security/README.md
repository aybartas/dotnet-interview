# C1.10 Güvenlik: Zero Trust, Defense in Depth, Microsoft Defender for Cloud

> 📁 `06-certifications/01-az-900-azure-fundamentals/10-security/` · Sınav alanı: **Describe Azure architecture and services (%35-40)** · Önkoşul: [C1.9](../09-identity-access/README.md) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe Azure identity, access, and security* — **Zero Trust** kavramını tanımla · **defense-in-depth** modelinin amacını tanımla · **Microsoft Defender for Cloud**'un amacını tanımla. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

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

Bu konu güvenliğe üç farklı açıdan bakar:

| Kavram | Türü | Tek cümle |
|---|---|---|
| **Zero Trust** | Güvenlik **modeli / felsefesi** | "Asla güvenme, her zaman doğrula": hiçbir isteğe ağın neresinden geldiği için güvenme |
| **Defense in depth** | Güvenlik **stratejisi / mimarisi** | Katmanlı savunma: bir katman aşılırsa sıradaki korur |
| **Microsoft Defender for Cloud** | Güvenlik **aracı / servisi** | Azure, kurum içi ve diğer bulutlardaki kaynakların güvenlik duruşunu ölçer, iyileştirir ve tehditleri tespit eder |

Zero Trust **nasıl düşündüğünü**, defense in depth kontrolleri **nereye koyduğunu**, Defender for Cloud ise bunun **ne kadar iyi uygulandığını** gösterir. Kimlik tarafı (Entra ID, MFA, Conditional Access, RBAC) → C1.9.

Bu konunun C# kodu yoktur. Pratik, defense in depth tablo alıştırması (3.2) ve `az` CLI ile Defender for Cloud laboratuvarıdır (3.4).

---

## 2. Neden?

- **Sınav:** Zero Trust'ın **üç ilkesi**, defense in depth'in **katman sırası** ve katman ↔ kontrol eşleştirmesi ile Defender for Cloud'un **hybrid/multicloud** kapsamı sabit sorulardır.
- **Gerçek işte:** Bulutta "iç ağ" ile "dış dünya" arasındaki net sınır kaybolur. Kullanıcılar evden, kafeden, telefondan bağlanır; uygulamalar SaaS'tadır. Yalnızca güvenlik duvarına güvenen bir model, içeri sızan ilk saldırgana her şeyi açar.
- **Shared responsibility (→ C1.1):** Microsoft fiziksel katmanı ve platformu korur. Kimlik, veri, ağ kuralları ve (IaaS'ta) OS güvenliği senin işindir. Defender for Cloud, kendi payına düşen işte nerede eksik kaldığını gösteren araçtır.

---

## 3. Nasıl?

### 3.1 Zero Trust model ⭐

**Geleneksel model ("kale ve hendek"):** Şirket ağı güvenli kabul edilir; güvenlik duvarı dışarıyı keser, içeridekiler güvenilirdir. Sorun: VPN'le içeri giren çalınmış bir hesap veya ele geçirilmiş bir dizüstü bilgisayar her şeye ulaşır. Üstelik kullanıcılar ve veriler artık zaten "kalenin" dışındadır.

**Zero Trust:** Ağın **içi de güvenli değildir**; her istek, **açık bir ağdan geliyormuş gibi** doğrulanır. Microsoft'un üç ilkesi:

| İlke | Anlamı | Azure'daki karşılığı |
|---|---|---|
| **Verify explicitly** (açıkça doğrula) | Her isteği **tüm sinyallerle** doğrula ve yetkilendir: kimlik, konum, cihaz durumu, servis/iş yükü, veri sınıflandırması, anomaliler | Entra ID + **MFA** + **Conditional Access** (→ C1.9) |
| **Use least privilege access** (en az yetki) | Yalnızca gerektiği kadar (**just-enough-access**) ve yalnızca gerektiği süre (**just-in-time**) yetki ver; risk tabanlı uyarlanabilir politikalar; veriyi koru | Dar kapsamlı **RBAC** rolleri, *(bağlam)* Privileged Identity Management (PIM), VM'ler için just-in-time erişim |
| **Assume breach** (ihlali varsay) | İhlal olmuş gibi tasarla: **etki alanını küçült** (segmentasyon), **uçtan uca şifrele**, analitik ve izlemeyle tehditleri **tespit et** | VNet/subnet segmentasyonu, NSG, **private endpoint** (→ C1.7), şifreleme, Defender for Cloud, Microsoft Sentinel *(bağlam)* |

> Zero Trust bir ürün değildir; satın alınmaz, **uygulanır**. Sınav ilkenin adını tanımla eşleştirir: "her isteği konum ve cihaz durumuyla doğrula" → *verify explicitly*; "yetkiyi yalnızca iş süresince ver" → *least privilege*; "ağı segmentlere ayır, saldırganın yatay hareketini sınırla" → *assume breach*.

### 3.2 Defense in depth ⭐

**Amaç:** Tek bir güvenlik kontrolüne güvenmemek. Saldırgan bir katmanı aşsa bile sıradaki katman onu **yavaşlatır, durdurur veya tespit eder**; hiçbir katman tek başına "tek arıza noktası" olmaz. Microsoft yedi katmanlı bir model kullanır. Katmanlar **dıştan içe** doğru sıralanır, en içte saldırganın asıl hedefi olan **veri** vardır:

| # | Katman | Ne korunur / amaç | Örnek kontroller ve Azure servisleri | Kimin sorumluluğu (bulutta) |
|---|---|---|---|---|
| 1 | **Fiziksel güvenlik** | Datacenter binası ve donanım | Kartlı/biyometrik giriş, kamera, disk imhası | **Microsoft** |
| 2 | **Kimlik ve erişim** | Altyapıya ve değişiklik kontrolüne erişim | **SSO, MFA**, Conditional Access, denetim log'ları; Entra ID (→ C1.9) | Müşteri (platform Microsoft'ta) |
| 3 | **Çevre (perimeter)** | Ağın dış sınırı, büyük ölçekli saldırılar | **Azure DDoS Protection**, çevre güvenlik duvarı (Azure Firewall) | Paylaşımlı |
| 4 | **Ağ** | Kaynaklar arası iletişim, yatay hareket | **Segmentasyon** (VNet/subnet), **NSG**, varsayılan olarak engelle, internete erişimi kısıtla, private endpoint | Müşteri |
| 5 | **Compute** | Sanal makineler ve sunucular | VM erişimini kısıtla, **yamaları güncel tut**, uç nokta koruması (antimalware), Defender for Servers | IaaS'ta müşteri |
| 6 | **Uygulama** | Uygulama kodu ve yapılandırması | Güvenli geliştirme (zafiyetsiz kod), **secret'ları koda değil Azure Key Vault'a** koymak, Web Application Firewall | Müşteri |
| 7 | **Veri** | Verinin kendisi (veritabanı, disk, SaaS verisi) | **Şifreleme** (durağan ve aktarımda), erişim kontrolü, sınıflandırma, yedek | **Her zaman müşteri** |

Hatırlatma cümlesi (dıştan içe): **F**iziksel → **K**imlik → **Ç**evre → **A**ğ → **C**ompute → **U**ygulama → **V**eri.

> **Alıştırma — kendi mimarin için defense in depth tablosu.** Mimari: internetten erişilen bir **App Service** web uygulaması, arkasında **Azure SQL Database**, yüklenen dosyalar için bir **storage account** ve yönetim için bir **Linux VM** (jump box). Her katmana en az bir somut kontrol yaz:
>
> | Katman | Senin kontrolün |
> |---|---|
> | Fiziksel güvenlik | |
> | Kimlik ve erişim | |
> | Çevre | |
> | Ağ | |
> | Compute | |
> | Uygulama | |
> | Veri | |
>
> <details><summary>Çözüm (örnek)</summary>
>
> | Katman | Kontrol |
> |---|---|
> | Fiziksel güvenlik | Microsoft'un datacenter kontrolleri. Müşteri tarafında yapılacak bir şey yok; denetim raporları Service Trust Portal'da (→ C1.12) |
> | Kimlik ve erişim | Tüm yöneticilere MFA ve Conditional Access; RBAC ile ekiplere dar kapsamlı roller; uygulamanın SQL/storage'a erişimi için şifre yerine **managed identity** (→ C2.11) |
> | Çevre | Web uygulamasının önünde **WAF**'lı Application Gateway veya Front Door; **DDoS Protection** |
> | Ağ | SQL ve storage için **private endpoint**, public erişim kapalı (→ C1.7); jump box subnet'inde NSG ile SSH yalnızca şirket IP'sinden; varsayılan engelle |
> | Compute | Jump box'ta otomatik OS yaması, Defender for Servers, just-in-time VM erişimi (port yalnızca istek anında ve süreli açılır) |
> | Uygulama | Connection string ve API anahtarları **Key Vault**'ta; App Service'te yalnızca HTTPS ve güncel TLS; bağımlılık zafiyet taraması |
> | Veri | SQL'de Transparent Data Encryption (varsayılan açık), storage'da durağan şifreleme (varsayılan açık), hassas sütunlar için sınıflandırma (Purview → C1.12), düzenli yedek ve geri yükleme testi |
>
> Gözlem: Bir katman (ör. uygulamadaki bir SQL injection zafiyeti) aşılsa bile private endpoint, en az yetkili managed identity ve şifreleme hasarı sınırlar. Defense in depth tam olarak budur.
> </details>

### 3.3 Microsoft Defender for Cloud

**Microsoft Defender for Cloud**, bir **cloud-native application protection platform (CNAPP)**'tır. İki büyük yeteneği vardır:

| Yetenek | Kısaltma | Ne yapar |
|---|---|---|
| **Cloud security posture management** | **CSPM** | Kaynakların **yapılandırmasını** sürekli değerlendirir, zafiyetleri ve yanlış ayarları bulur, **öneriler** ve **secure score** üretir. "Kapı kilitli mi?" sorusu |
| **Cloud workload protection** | **CWP** | Çalışan iş yüklerine (sunucu, container, veritabanı, storage…) yönelik **tehditleri tespit eder** ve **uyarı** üretir. "Biri kapıyı zorluyor mu?" sorusu |

**Kapsam — yalnızca Azure değil** ⭐:

- **Azure:** Azure'un kendi kaynakları; PaaS servisleri için ajan gerekmeden yerleşik.
- **Kurum içi (on-prem) ve diğer bulutlardaki sunucular:** **Azure Arc** ile Azure'a bağlanır (→ C1.13).
- **Diğer bulutlar:** **AWS** ve **Google Cloud (GCP)** hesapları, **connector**'larla ajan gerektirmeden bağlanır.
- *(bağlam)* Kod depoları (GitHub, Azure DevOps, GitLab): DevOps güvenliği.

**Üç ihtiyaç: Assess → Secure → Defend**

| Aşama | Ne yapar | Örnek |
|---|---|---|
| **Assess** (sürekli değerlendir) | Güvenlik durumunu sürekli değerlendirir, VM ve container image'larında **zafiyet taraması** yapar | "Bu VM'de kritik bir yama eksik" |
| **Secure** (güçlendir) | **Microsoft cloud security benchmark** (MCSB; Azure Policy tabanlı varsayılan güvenlik girişimi) ile kaynakları karşılaştırır; **öneriler** ve **secure score** sunar; regulatory compliance panosu (ISO 27001, PCI DSS… standartlarına göre) | "Storage account public erişime açık → kapat", secure score %62 → %71 |
| **Defend** (tehditlere karşı koru) | **Güvenlik uyarıları** (ör. olağandışı konumdan erişim, kaba kuvvet denemesi, şüpheli işlem), MITRE ATT&CK eşlemesi, olay müdahalesi, *(bağlam)* just-in-time VM erişimi, Microsoft Sentinel'e (SIEM) aktarım | "VM'e 500 başarısız SSH denemesi", "storage'dan olağandışı miktarda veri indirildi" |

**Secure score:** Önerilerin uygulanma oranına göre hesaplanan güvenlik duruşu **yüzdesidir**. Öneriler **güvenlik kontrolleri** (security controls) altında gruplanır. Bir kontrolün puanını almak için o kontroldeki önerilerin **tüm** ilgili kaynaklarda düzeltilmesi gerekir. Yüksek puan = daha az açık. Puan **düşük riskli** demek değildir; yalnızca bilinen önerilere uyumu ölçer.

**Planlar** *(bağlam — adlar sınavda geçebilir)*:

- **Foundational CSPM** — **ücretsiz**; Defender for Cloud açıldığı anda gelir: temel değerlendirmeler, öneriler, secure score, MCSB.
- **Defender CSPM** — ücretli; ajansız zafiyet taraması, saldırı yolu analizi (attack path), cloud security graph, veri farkındalıklı duruş.
- **CWP planları** — her iş yükü türü için ayrı açılır (ücretli): **Defender for Servers** (Plan 1 / Plan 2), Defender for Storage, Defender for Databases (SQL, açık kaynak veritabanları, Cosmos DB), Defender for Containers, Defender for App Service, Defender for Key Vault, Defender for Resource Manager, Defender for APIs, AI iş yükleri için tehdit koruması.

**Defender for Cloud ile karıştırılan araçlar:**

| Araç | Odak | Fark |
|---|---|---|
| **Defender for Cloud** | **Güvenlik** duruşu + tehdit tespiti (Azure, hybrid, multicloud) | Güvenlik uzmanı aracı |
| **Azure Policy** (→ C1.12) | **Kaynak kurallarını** zorla/denetle (bölge, SKU, tag, güvenlik ayarı) | Defender'ın güvenlik önerileri altta Policy tanımlarıyla değerlendirilir; Policy **genel yönetişim** aracıdır |
| **Azure Advisor** (→ C1.14) | Beş kategoride **genel öneri**: reliability, security, performance, cost, operational excellence | Advisor'ın **Security** kategorisindeki öneriler **Defender for Cloud'dan** gelir |
| **Microsoft Sentinel** *(bağlam)* | **SIEM/SOAR**: birçok kaynaktan log toplayıp olay korelasyonu ve otomatik müdahale | Defender uyarılarını da tüketir; güvenlik operasyon merkezinin (SOC) aracı |

### 3.4 Pratik — Defender for Cloud: secure score ve bir öneriyi uygulamak (`az` CLI)

Foundational CSPM **ücretsizdir** ve subscription'da varsayılan olarak etkindir. Aşağıdaki laboratuvar yalnızca **küçük bir storage account** oluşturur (kuruşlar seviyesinde ücret). **Ücretli Defender planlarını açma**; açtıysan laboratuvar sonunda Free'ye geri al. Bütçe alarmın kurulu olsun (→ C1.1).

```bash
az login

# 1) Mevcut güvenlik duruşu: secure score ve kontroller (salt okunur)
az security secure-scores list --output table
az security secure-score-controls list --output table

# 2) Hangi Defender planları açık? (Free = yalnızca Foundational CSPM; Standard = ücretli plan)
az security pricing list --query "value[].{plan:name, tier:pricingTier}" --output table

# 3) Bilerek "kötü" yapılandırılmış bir storage account: anonim blob erişimine izin veriyor
az group create --name rg-az900-c110 --location westeurope
ST=staz900c110$RANDOM
az storage account create --name "$ST" --resource-group rg-az900-c110 --location westeurope \
  --sku Standard_LRS --allow-blob-public-access true

# 4) Değerlendirmeler (assessments): sağlıksız olanları listele
#    Not: Yeni kaynağın değerlendirmeye girmesi birkaç saat (en çok ~24 saat) sürebilir.
az security assessment list \
  --query "[?status.code=='Unhealthy'].{oneri:displayName, kaynak:resourceDetails.Id}" --output table

# 5) Öneriyi uygula: anonim blob erişimini kapat
az storage account update --name "$ST" --resource-group rg-az900-c110 --allow-blob-public-access false
az storage account show --name "$ST" --resource-group rg-az900-c110 --query allowBlobPublicAccess

# 6) Temizlik
az group delete --name rg-az900-c110 --yes --no-wait
```

Beklenen gözlem: Değerlendirme yenilendiğinde *"Storage account public access should be disallowed"* türündeki öneri o kaynak için **Healthy** olur ve ilgili kontrolün puanı (dolayısıyla secure score) artar. Kontroldeki **tüm** kaynaklar düzeltilmeden kontrolün tam puanı alınmaz. CLI çıktı alanları API sürümüne göre değişebilir; aynı bilgi portalda *Defender for Cloud → Recommendations / Secure score* ekranında da görünür.

---

## 4. Gerçek dünya senaryosu

Bir sağlık teknolojisi şirketi, hasta verisi işleyen platformunu Azure'da çalıştırıyor. Ayrıca eski bir raporlama sistemi kendi datacenter'ında, bir analitik iş yükü ise AWS'te.

1. **Zero Trust'a geçiş:** VPN'e bağlanan herkesin her şeye erişebildiği eski model bırakılıyor. *Verify explicitly*: tüm girişler Entra ID + MFA + Conditional Access'ten geçiyor ve yönetilmeyen cihazdan hasta verisine erişim yok. *Least privilege*: geliştiricilerin üretimdeki yetkileri yalnızca talep edildiğinde ve 4 saatliğine açılıyor. *Assume breach*: veritabanları private endpoint arkasına alınıp ağ segmentlere ayrılıyor.
2. **Defense in depth:** Çevrede DDoS Protection ve WAF; ağda NSG ve private endpoint; compute'ta otomatik yama ve Defender for Servers; uygulamada secret'lar Key Vault'ta; veride şifreleme ve sınıflandırma. Pentest'te uygulamada bir zafiyet bulunuyor, ama saldırgan simülasyonu en az yetkili managed identity ve private endpoint yüzünden veritabanının dışına çıkamıyor.
3. **Defender for Cloud:** Azure subscription'ları, Azure Arc ile bağlanan kurum içi sunucular ve connector ile bağlanan AWS hesabı **tek panelde**. Başlangıç secure score'u %48. İlk sprintte en yüksek puanlı öneriler (MFA, public erişimli storage, eksik yamalar) kapatılıyor ve puan %74'e çıkıyor. Bir gece AWS'teki bir sunucuda olağandışı bir işlem için **güvenlik uyarısı** üretiliyor ve olay ekibi sunucuyu izole ediyor.
4. **Uyumluluk:** Regulatory compliance panosu, ilgili standartlara (ör. ISO 27001) göre hangi kontrollerin eksik olduğunu denetçiye rapor olarak sunuyor.

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Zero Trust'ta **ilke ↔ tanım** eşleştirmesi; defense in depth'te **katman sırası** (sıralama sorusu) ve **kontrol ↔ katman** eşleştirmesi; Defender for Cloud'da **ne yaptığı** ve **nerede çalıştığı** (evet/hayır tabloları).

| Soru kalıbı | Doğru cevap | Tuzak |
|---|---|---|
| "Asla güvenme, her zaman doğrula" | **Zero Trust** | Defense in depth |
| "Ağın içindeki kullanıcıya otomatik güvenilir" | Geleneksel (perimeter) model, **Zero Trust değil** | |
| "Her isteği kimlik, konum ve cihaz sinyalleriyle doğrula" | **Verify explicitly** | Least privilege |
| "Just-in-time / just-enough-access" | **Use least privilege access** | Assume breach |
| "Etki alanını segmentasyonla küçült, şifrele, analitikle tespit et" | **Assume breach** | Verify explicitly |
| "Bir katman aşılsa bile diğerleri korur" | **Defense in depth** | Zero Trust |
| "Katmanların en dışı / en içi" | **Fiziksel güvenlik** / **Veri** | Çevre / Uygulama |
| "DDoS koruması hangi katman?" | **Çevre (perimeter)** | Ağ |
| "Segmentasyon, NSG, varsayılan engelle" | **Ağ** | Çevre |
| "Yamalar, VM erişimini kısıtlama, antimalware" | **Compute** | Uygulama |
| "Secret'ları Key Vault'ta saklamak, güvenli kod" | **Uygulama** | Veri |
| "MFA, SSO, erişim denetimi" | **Kimlik ve erişim** | Çevre |
| "Şifreleme ve verinin kendisine erişim kontrolü" | **Veri** | |
| "Azure, on-prem ve AWS/GCP kaynaklarının güvenlik duruşunu izle, tehditleri tespit et" | **Microsoft Defender for Cloud** | Azure Policy, Advisor |
| "Güvenlik duruşunu yüzde olarak gösteren ölçü" | **Secure score** | Advisor score |

**Anahtar kelimeler:** *never trust, always verify*, *verify explicitly*, *least privilege*, *assume breach*, *layered approach*, *slow the advance of an attack*, *CSPM*, *CWP*, *secure score*, *recommendations*, *security alerts*, *hybrid and multicloud*.

**Evet/hayır tuzakları:**
- "Defender for Cloud yalnızca Azure kaynaklarını korur" → **Hayır** (on-prem Arc ile, AWS/GCP connector'larla).
- "Defense in depth'te veri katmanı genellikle saldırganın asıl hedefidir" → **Evet**.
- "Zero Trust, güvenlik duvarını gereksiz kılar" → **Hayır** (Zero Trust tek bir kontrole güvenmez; çevre kontrolleri yine kullanılır).
- "Secure score yükseldikçe güvenlik duruşu iyileşir" → **Evet**.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "Zero Trust bir ürün, satın alırız" | Bir model/felsefedir; Entra ID, Conditional Access, RBAC, segmentasyon gibi araçlarla **uygulanır** |
| "VPN'le bağlanan kullanıcı içeridedir, güvenilirdir" | Zero Trust'ta konum tek başına güven gerekçesi değildir; her istek doğrulanır |
| Defense in depth'i "çok sayıda firewall" sanmak | Farklı **türde** kontrollerin farklı **katmanlarda** bulunmasıdır (kimlik, ağ, compute, uygulama, veri) |
| Katman sırasını karıştırmak (ör. ağı çevrenin dışına koymak) | Dıştan içe: fiziksel → kimlik → çevre → ağ → compute → uygulama → veri |
| "Bulutta fiziksel güvenlik benim işim" | Fiziksel katman bulutta **Microsoft'tadır** (→ C1.1) |
| "Defender for Cloud = antivirüs" | Antimalware/uç nokta koruması bir parçasıdır; asıl amaç duruş yönetimi (CSPM) + iş yükü koruması (CWP) |
| "Secure score %100 = güvendeyim" | Yalnızca bilinen önerilere uyumu ölçer; tehdit tespiti, olay müdahalesi ve süreçler yine gerekir |
| Tüm ücretli Defender planlarını denemek için açıp unutmak | Planlar kaynak başına ücretlidir; deneme sonrası Free'ye geri al, bütçe alarmı kur |
| "Defender for Cloud ile Azure Policy aynı şey" | Defender güvenlik odaklıdır ve önerileri Policy tanımlarıyla değerlendirir; Policy genel yönetişim aracıdır (→ C1.12) |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Zero Trust (her isteği doğrula) | İçeri sızan saldırganın yatay hareketi sınırlanır; uzaktan çalışma ve SaaS için uygun | Kimlik, cihaz yönetimi ve politika yatırımı; yanlış ayarlanmış politika kullanıcı sürtünmesi yaratır |
| Just-in-time / en az yetki | Kalıcı yüksek yetki yok; ele geçirilen hesabın etkisi küçük | Erişim talep/onay süreci; acil durumlarda gecikme (break-glass hesabı gerekir) |
| Segmentasyon ve private endpoint (assume breach) | Bir bileşenin ihlali tüm ortama yayılmaz | Ağ tasarımı ve DNS karmaşıklığı (→ C1.7) |
| Defense in depth (çok katman) | Tek kontrol hatası felaket olmaz; saldırı yavaşlar ve tespit edilir | Maliyet, yönetilecek daha çok bileşen, olası gecikme (WAF, ek doğrulama) |
| Foundational CSPM (ücretsiz) | Secure score, öneriler ve MCSB sıfır maliyetle | Tehdit tespiti, saldırı yolu analizi ve gelişmiş tarama yok |
| Ücretli Defender planları (CWP, Defender CSPM) | Tehdit tespiti, zafiyet taraması, uyarılar | Kaynak başına aylık ücret; uyarı hacmi için güvenlik ekibi/süreç gerekir |

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure identity, access, and security](https://learn.microsoft.com/en-us/training/modules/describe-azure-identity-access-security/) (Zero Trust, defense in depth, Defender for Cloud üniteleri)
- [Zero Trust guidance center](https://learn.microsoft.com/en-us/security/zero-trust/) · [Zero Trust principles](https://learn.microsoft.com/en-us/security/zero-trust/zero-trust-overview)
- [What is Microsoft Defender for Cloud?](https://learn.microsoft.com/en-us/azure/defender-for-cloud/defender-for-cloud-introduction) · [Secure score](https://learn.microsoft.com/en-us/azure/defender-for-cloud/secure-score-security-controls)
- [Cloud security posture management (Foundational CSPM / Defender CSPM)](https://learn.microsoft.com/en-us/azure/defender-for-cloud/concept-cloud-security-posture-management) · [Defender for Cloud pricing](https://azure.microsoft.com/pricing/details/defender-for-cloud/)
- [Microsoft cloud security benchmark](https://learn.microsoft.com/en-us/security/benchmark/azure/introduction)
- [`az security`](https://learn.microsoft.com/en-us/cli/azure/security) · [Prevent anonymous read access to blobs](https://learn.microsoft.com/en-us/azure/storage/blobs/anonymous-read-access-prevent)
