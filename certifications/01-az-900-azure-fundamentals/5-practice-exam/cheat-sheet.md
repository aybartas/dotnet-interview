[← AZ-900 İçindekiler](../README.md) · [Sınav stratejisi](README.md) · [📝 Deneme sınavı](quiz.md)

## ⚡ AZ-900 Hızlı Tekrar Kartı

> Sınavdan önceki gün ve sınav sabahı için **tek sayfalık** tekrar. Her madde ilgili konuya bağlıdır; emin olmadığın satırda konuya dön.
> Telefonda okunacak şekilde yazıldı: geniş tablo yok, her kural tek satır.

---

### 1. Ezberlenecek sayılar

**Bulut kavramları** (→ [C1.1](../1.1-cloud-computing-models-pricing/README.md), [C1.2](../1.2-cloud-benefits/README.md))

- **CapEx** = baştan, fiziksel altyapıya büyük yatırım · **OpEx** = kullandıkça, dönemsel harcama (bulut = OpEx).
- **Scale up/down** = aynı kaynağı güçlendir/zayıflat (vertical) · **Scale out/in** = kaynak sayısını artır/azalt (horizontal).
- **HA** = kesintisiz sürmek · **Reliability** = arızadan toparlanmak · **Scalability** = yetenek · **Elasticity** = otomatik ölçekleme.
- Veri, cihazlar, hesaplar ve kimlikler **her modelde müşteride**; fiziksel altyapı, host ve sanallaştırma katmanı bulutta **Microsoft'ta**.

**Mimari** (→ [C1.4](../2.1-core-architecture/README.md), [C1.5](../2.2-compute-virtual-machines/README.md))

- Zone destekleyen region'da **en az 3** availability zone.
- Management group derinliği: root ve subscription hariç **6 seviye**; dizin başına **10.000** management group.
- Bir kaynak **tek** resource group'ta; resource group'lar **iç içe olmaz**; RG silinirse içindeki **her şey** silinir.
- Availability set: **3** fault domain'e, **20** update domain'e kadar (varsayılan 5); ek ücret yok.

**Storage** (→ [C1.8](../2.5-storage/README.md))

- LRS: 1 datacenter, **3** kopya · ZRS: **3 zone**, 3 kopya · GRS / GZRS: **6** kopya (ikincisi region pair'de).
- Önerilen en az süre: Cool **30**, Cold **90**, Archive **180** gün; Archive **çevrimdışıdır**, önce rehydrate (saatler).
- Storage account adı: **3–24** karakter, küçük harf + rakam, Azure genelinde benzersiz.

**Ağ ve bağlantı** (→ [C1.7](../2.4-networking/README.md))

- **Private IP** = VNet içi adres · **Public IP** = internetten erişilen adres (isteğe bağlı).
- **Site-to-site VPN** = ofis ağı ↔ Azure · **Point-to-site VPN** = tek cihaz ↔ Azure · **ExpressRoute** = internete çıkmayan özel hat (varsayılan olarak şifreli değil).
- **Peering** geçişli değildir; adres alanları çakışmamalıdır.

---

### 2. Anahtar kelime → cevap

**Cloud kavramları**

- "Baştan yatırım yok, kullandıkça öde" → **OpEx / consumption-based**
- "Kendi datacenter'ı, donanım satın alma" → **CapEx**
- "Veri kurum içinde kalmalı ama web katmanı ölçeklenmeli" → **hybrid cloud**
- "Talep artınca **otomatik** büyüsün" → **elasticity** (yetenek = scalability)
- "Kesintide de hizmet sürsün" → **high availability**
- "Arızadan/felaketten toparlansın" → **reliability / disaster recovery**
- "İşletim sistemine tam erişim" → **IaaS** · "Sadece kodumu çalıştır" → **PaaS** · "Hazır uygulama" → **SaaS**
- "Sadece kod çalışırken öde, olay tetiklemeli" → **serverless (Azure Functions)**

**Compute ve barındırma**

- "Tek container, orkestrasyonsuz, en hızlı" → **Azure Container Instances**
- "Binlerce container orkestrasyonu" → **AKS**
- "Serverless container, sıfıra ölçek" → **Azure Container Apps**
- "OS yönetmeden web uygulaması/API" → **App Service**
- "Aynı VM'den çok sayıda, otomatik ölçek" → **VM Scale Sets**
- "Datacenter içi donanım/bakım arızasına karşı" → **availability set**
- "Datacenter arızasına karşı" → **availability zone** · "Bölgesel felakete karşı" → **region pair**
- "Bulutta çok oturumlu Windows masaüstü" → **Azure Virtual Desktop**
- "VM ücretini durdur" → **stop (deallocate)**; disk ücreti sürer

**Ağ**

- "İki VNet'i birbirine bağla" → **VNet peering**
- "Şifreli tünel, **internet üzerinden**" → **VPN Gateway**
- "**Özel** bağlantı, internetten geçmez" → **ExpressRoute**
- "Alan adı kayıtlarını Azure'da barındır" → **Azure DNS** (domain **satın alınmaz**)
- "PaaS servisine VNet içinden **özel IP** ile eriş" → **private endpoint**

**Storage ve taşıma**

- "Komut satırından dosya kopyala" → **AzCopy** · "Grafik arayüz" → **Storage Explorer**
- "Dosya sunucusunu Azure Files ile senkronla, sık kullanılan önbellekte" → **Azure File Sync**
- "Sunucuları keşfet, değerlendir, taşı" → **Azure Migrate**
- "Terabaytlarca veri, ağ yetersiz" → **Azure Data Box**
- "Ayda bir okunan yedek" → **Cool/Cold** · "Yıllarca saklanan, nadiren okunan arşiv" → **Archive**
- "Felakette de ikinci bölgeden **okumaya devam**" → **RA-GRS / RA-GZRS**

**Kimlik ve güvenlik**

- "Kimsin?" → **authentication** · "Ne yapabilirsin?" → **authorization**
- "Tek girişle birçok uygulama" → **SSO**
- "Konuma/cihaza/risk durumuna göre MFA iste" → **Conditional Access**
- "İş ortağı kendi hesabıyla misafir olarak girsin" → **Entra External ID / B2B collaboration (guest user)**
- "Eski LDAP/Kerberos uygulamaları, domain join, DC yönetmeden" → **Entra Domain Services**
- "Asla güvenme, her zaman doğrula, en az yetki, ihlal varsay" → **Zero Trust**
- "Katman katman koruma" → **defense in depth**
- "Secure score, öneriler, iş yükü koruması" → **Defender for Cloud**

**Yönetim ve yönetişim**

- "Kullanıcı **ne yapabilir**?" → **RBAC** · "Kaynak **nasıl olmalı**?" → **Azure Policy**
- "Kazara silinmesin" → **CanNotDelete lock** · "Hiç değişmesin" → **ReadOnly lock** (Owner'ı da durdurur)
- "Maliyeti departmana göre ayır" → **tag**
- "Azure'a geçersem ne tutar?" → **Pricing Calculator** · "Ne harcadım, ay sonu tahmini ne?" → **Cost Management** (kredi bitince kaynakları durduran → **spending limit**)
- "Bütçe ve uyarı, harcama analizi" → **Cost Management**
- "Verinin haritası, sınıflandırma, soy ağacı" → **Microsoft Purview**
- "Azure dışındaki sunucuları Azure'dan yönet" → **Azure Arc**
- "Ortamı tekrar tekrar aynı kur" → **IaC (ARM template, JSON, deklaratif)** · "Tüm isteklerin geçtiği katman" → **Azure Resource Manager**
- "Tarayıcıdan, kurulumsuz CLI/PowerShell" → **Cloud Shell**

**İzleme**

- "Maliyet, güvenlik, güvenilirlik **önerileri**" → **Azure Advisor**
- "**Azure'un** bölgemdeki sorunu/planlı bakımı" → **Service Health** (genel durum: Azure Status)
- "Metrik, log, uyarı toplama" → **Azure Monitor** · "KQL ile log sorgusu" → **Log Analytics**
- "Uygulama performansı, istek/hata izleme" → **Application Insights**

---

### 3. En çok karıştırılan çiftler

- **Scalability vs elasticity:** yetenek vs **otomatik** kullanımı.
- **HA vs DR:** çalışmayı sürdürmek vs felaketten toparlanmak.
- **Availability set vs zone:** datacenter **içi** vs datacenter'lar **arası**.
- **Zone vs region pair:** datacenter arızası vs bölgesel felaket.
- **VPN Gateway vs ExpressRoute:** internet üzerinden şifreli vs **özel hat**.
- **Public endpoint vs private endpoint:** internete açık adres vs servise **özel IP** (public erişim kapatılabilir).
- **RBAC vs Policy:** kişi vs kaynak.
- **Lock vs Policy:** silme/değiştirmeyi engeller vs yapılandırma kuralı.
- **Advisor vs Service Health vs Monitor:** öneri vs Azure'un sağlığı vs kendi kaynaklarının telemetrisi.
- **Service Health vs Azure Status:** **senin** aboneliğini etkileyen olaylar vs herkese açık genel durum.
- **Pricing Calculator vs Cost Management:** "Azure'da ne **tutar**?" (önce, tahmin) vs "ne **harcadım**?" (gerçekleşen).
- **ARM vs ARM template:** yönetim/dağıtım **servisi** vs ona verilen JSON **dosyası**.
- **Budget vs spending limit:** yalnızca **uyarır** vs kaynakları **durdurur** (kredili abonelik türleri).
- **Entra ID vs Entra Domain Services:** modern kimlik (OAuth/OIDC/SAML) vs klasik domain (LDAP/Kerberos/NTLM).
- **Azure RBAC vs Entra rolleri:** Azure kaynakları vs directory (kullanıcı, lisans).
- **VM vs container vs Functions:** donanımı sanallaştırır (tam OS) vs OS'u sanallaştırır (çekirdek paylaşır) vs sunucusuz, olay tetiklemeli kod.
- **Metric alert vs log alert:** metrik eşiği vs log sorgusu sonucu (ikisi de **action group**'u tetikler).

---

### 4. "Her zaman / asla" kuralları

- Veri, cihazlar, hesaplar ve kimlikler **her modelde müşteridedir** (→ [C1.1](../1.1-cloud-computing-models-pricing/README.md)).
- Fiziksel datacenter, host ve ağ bulutta **her zaman Microsoft'tadır**.
- MFA'yı açmak ve şifre politikası **her zaman müşterinin** sorumluluğudur.
- Ayarlar hiyerarşide **yukarıdan aşağı miras** kalır: management group → subscription → RG → kaynak.
- RBAC izinleri **toplanır**: Reader + Contributor = Contributor.
- Redundancy **yedek değildir**: bir kopyadan silinen veri tüm kopyalardan silinir.
- **Gelen** veri trafiği genellikle ücretsiz, **giden** (egress) ücretlidir.
- Deallocate edilmiş VM compute ücreti almaz; **disk** (ve varsa **statik** public IP) ücreti sürer.

---

### 5. Mini quiz — hızlı ısınma

#### Soru 1: Tek seçim

Bir dış danışmanlık firmasının çalışanları, **kendi şirket hesaplarıyla** giriş yaparak kuruluşun uygulamalarına erişecek; onlara yeni parola verilmeyecek. Ne kullanılır?

- A) Microsoft Entra Domain Services
- B) Microsoft Entra External ID — B2B collaboration (guest user)
- C) Azure Policy
- D) Azure Resource Lock

<details><summary>Cevap</summary>

**B.** B2B collaboration, dış kullanıcıları **kendi kimlikleriyle misafir (guest)** olarak davet eder. Domain Services klasik AD protokolleri içindir; Policy ve lock kaynak yönetişimidir.
</details>

#### Soru 2: Tek seçim

Bir yönetici, kullanıcıların bir resource group'ta kaynakları **yalnızca görüntüleyebilmesini** istiyor. Hangi built-in rol uygundur?

- A) Owner
- B) Contributor
- C) Reader
- D) Hiçbiri; Azure Policy kullanılmalı

<details><summary>Cevap</summary>

**C.** **Reader** yalnızca görüntüleme izni verir. Contributor kaynak oluşturur/siler; Owner ayrıca erişimi yönetir. Policy kullanıcıyı değil, kaynağın özelliklerini denetler.
</details>

#### Soru 3: Evet / Hayır

Her ifade için Evet veya Hayır seç.

- a) Azure Virtual Desktop, Azure'da çalışan bir masaüstü ve uygulama sanallaştırma servisidir.
- b) Bir resource group'a konan tag, içindeki kaynaklara varsayılan olarak otomatik uygulanır.
- c) ARM template'leri JSON biçiminde, deklaratif dosyalardır.

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Tag'ler varsayılan olarak miras alınmaz (RBAC, Policy ve lock alınır). ARM template'leri deklaratif JSON'dır.
</details>

#### Soru 4: Tek seçim

Bir ekip, uygulamasındaki **istek sürelerini, hata oranını ve bağımlılık çağrılarını** izlemek istiyor. Hangi servis uygundur?

- A) Azure Advisor
- B) Azure Service Health
- C) Application Insights
- D) Azure Policy

<details><summary>Cevap</summary>

**C.** Application Insights, Azure Monitor'un uygulama performans izleme (APM) özelliğidir: istek, bağımlılık, hata ve dağıtık izleme verilerini toplar.
</details>

#### Soru 5: Tek seçim

Bir yönetici, kredili bir abonelikte kredi bittiğinde **kaynakların otomatik olarak devre dışı kalmasını** istiyor. Hangisi bunu sağlar?

- A) Cost Management budget
- B) Pricing Calculator
- C) Spending limit
- D) Resource tags

<details><summary>Cevap</summary>

**C.** Spending limit, kredili/ücretsiz abonelik türlerinde kredi bitince kaynakları durdurur. **Budget** yalnızca uyarır; Pricing Calculator tahmin aracıdır; tag raporlama içindir.
</details>

---

➡️ [📝 50 soruluk deneme sınavı](quiz.md) · [AZ-900 İçindekiler](../README.md)
