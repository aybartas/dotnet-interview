[← AZ-900 İçindekiler](../README.md) · [Sınav stratejisi](README.md) · [📝 Deneme sınavı](quiz.md)

## ⚡ AZ-900 Hızlı Tekrar Kartı

> Sınavdan önceki gün ve sınav sabahı için **tek sayfalık** tekrar. Her madde ilgili konuya bağlıdır; emin olmadığın satırda konuya dön.
> Telefonda okunacak şekilde yazıldı: geniş tablo yok, her kural tek satır.

---

### 1. Ezberlenecek sayılar

**SLA → aylık kesinti bütçesi** (→ [C1.2](../1.2-cloud-benefits/README.md))

- %99 → ~7 sa 18 dk
- %99,9 → ~43 dk 48 sn
- %99,95 → ~21 dk 53 sn
- %99,99 → ~4 dk 22 sn
- Her ek "9" kesinti bütçesini **10'a böler**.
- **Seri** bileşenler: SLA'lar **çarpılır** (toplam en zayıftan da düşük). **Paralel/yedekli**: `1 − (1−a)(1−b)` (toplam yükselir).
- **Preview** servisin SLA'sı **yoktur**; ücretsiz katmanlarda da genellikle yoktur.

**Mimari** (→ [C1.4](../2.1-core-architecture/README.md), [C1.5](../2.2-compute-virtual-machines/README.md))

- Zone destekleyen region'da **en az 3** availability zone.
- Management group derinliği: root ve subscription hariç **6 seviye**; dizin başına **10.000** management group.
- Bir kaynak **tek** resource group'ta; resource group'lar **iç içe olmaz**; RG silinirse içindeki **her şey** silinir.
- Availability set: **3** fault domain'e, **20** update domain'e kadar (varsayılan 5); ek ücret yok.

**Storage** (→ [C1.8](../2.5-storage/README.md))

- LRS: 1 datacenter, **3** kopya · ZRS: **3 zone**, 3 kopya · GRS / GZRS: **6** kopya (ikincisi region pair'de).
- Önerilen en az süre: Cool **30**, Cold **90**, Archive **180** gün; Archive **çevrimdışıdır**, önce rehydrate (saatler).
- Storage account adı: **3–24** karakter, küçük harf + rakam, Azure genelinde benzersiz.

**Destek planları** (→ [C1.22](../4.7-support-sla-lifecycle/README.md))

- Sıra: **Basic** (ücretsiz, her abonelikte) → **Developer** (üretim dışı) → **Standard** (üretim, 7/24) → **Professional Direct** (iş-kritik) → **Unified** (kurum geneli, atanmış TAM).
- Basic **teknik destek vermez**; faturalama ve abonelik desteği verir.

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
- "İş ortağı kendi hesabıyla girsin" → **B2B** · "Müşteri uygulaması için kayıt/giriş" → **B2C / External ID**
- "Eski LDAP/Kerberos uygulamaları, domain join, DC yönetmeden" → **Entra Domain Services**
- "Asla güvenme, her zaman doğrula, en az yetki, ihlal varsay" → **Zero Trust**
- "Katman katman koruma" → **defense in depth**
- "Secure score, öneriler, iş yükü koruması" → **Defender for Cloud**
- "Sırlar, anahtarlar, sertifikalar" → **Key Vault**

**Yönetim ve yönetişim**

- "Kullanıcı **ne yapabilir**?" → **RBAC** · "Kaynak **nasıl olmalı**?" → **Azure Policy**
- "Kazara silinmesin" → **CanNotDelete lock** · "Hiç değişmesin" → **ReadOnly lock** (Owner'ı da durdurur)
- "Maliyeti departmana göre ayır" → **tag**
- "Azure'a geçersem ne tutar?" → **Pricing Calculator** · "On-prem mi, Azure mu?" → **TCO / Azure Migrate business case**
- "Bütçe ve uyarı, harcama analizi" → **Cost Management**
- "Verinin haritası, sınıflandırma, soy ağacı" → **Microsoft Purview**
- "GDPR'a ne kadar uyumluyuz, compliance score" → **Purview Compliance Manager**
- "Microsoft'un ISO/SOC denetim raporları" → **Service Trust Portal**
- "Azure dışındaki sunucuları Azure'dan yönet" → **Azure Arc**
- "Ortamı tekrar tekrar aynı kur" → **IaC (ARM template / Bicep)**
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
- **Service endpoint vs private endpoint:** servis yine public IP'de vs servise **özel IP**.
- **RBAC vs Policy:** kişi vs kaynak.
- **Lock vs Policy:** silme/değiştirmeyi engeller vs yapılandırma kuralı.
- **Advisor vs Service Health vs Monitor:** öneri vs Azure'un sağlığı vs kendi kaynaklarının telemetrisi.
- **Service Health vs Azure Status:** **senin** aboneliğini etkileyen olaylar vs herkese açık genel durum.
- **Pricing Calculator vs TCO:** "Azure'da ne tutar?" vs "on-prem'e göre ne kazandırır?"
- **Entra ID vs Entra Domain Services:** modern kimlik (OAuth/OIDC/SAML) vs klasik domain (LDAP/Kerberos/NTLM).
- **Azure RBAC vs Entra rolleri:** Azure kaynakları vs directory (kullanıcı, lisans).
- **AVD vs Windows 365:** esnek, multi-session Azure servisi vs kullanıcı başına sabit fiyatlı Cloud PC.
- **Purview vs Compliance Manager vs Service Trust Portal:** verinin yönetişimi vs **senin** uyumluluk puanın vs **Microsoft'un** denetim raporları.

---

### 4. "Her zaman / asla" kuralları

- Veri, cihazlar, hesaplar ve kimlikler **her modelde müşteridedir** (→ [C1.1](../1.1-cloud-computing-models-pricing/README.md)).
- Fiziksel datacenter, host ve ağ bulutta **her zaman Microsoft'tadır**.
- MFA'yı açmak ve şifre politikası **her zaman müşterinin** sorumluluğudur.
- Ayarlar hiyerarşide **yukarıdan aşağı miras** kalır: management group → subscription → RG → kaynak.
- RBAC izinleri **toplanır**: Reader + Contributor = Contributor.
- Redundancy **yedek değildir**: silinen veri tüm kopyalardan silinir; geri dönüş için backup / soft delete gerekir (→ [C1.19](../4.4-backup-dr-migration/README.md)).
- **Gelen** veri trafiği genellikle ücretsiz, **giden** (egress) ücretlidir.
- Deallocate edilmiş VM compute ücreti almaz; **disk** (ve varsa **statik** public IP) ücreti sürer.

---

### 5. Mini quiz — son eklenen konular

#### Soru 1: Tek seçim

Bir şirket, GDPR gereksinimlerine göre **kendi** uyumluluk durumunu bir puanla izlemek ve yapılacak iyileştirme adımlarının listesini görmek istiyor. Hangisini kullanmalı?

- A) Service Trust Portal
- B) Microsoft Purview Compliance Manager
- C) Azure Policy
- D) Azure Advisor

<details><summary>Cevap</summary>

**B.** Compliance Manager hazır düzenleme değerlendirmeleri, iyileştirme eylemleri ve **compliance score** sunar. Service Trust Portal Microsoft'un **kendi** denetim raporlarını verir (A); Azure Policy kaynak yapılandırmasını zorlar ama düzenleme bazlı bir puan ve eylem listesi vermez (C); Advisor maliyet/güvenilirlik/performans önerileri verir (D).
</details>

#### Soru 2: Tek seçim

Yöneticiler, operatörlerin VM'leri **başlatıp durdurabilmesini** ama **silememesini** istiyor. Hiçbir built-in rol tam olarak uymuyor. Ne yapılmalı?

- A) Operatörlere Owner rolü verip bir CanNotDelete lock eklemek
- B) Uygun `Actions` listesiyle bir **custom rol** tanımlayıp en dar kapsamda atamak
- C) Azure Policy ile VM silmeyi engellemek
- D) Operatörleri Global Administrator yapmak

<details><summary>Cevap</summary>

**B.** Built-in roller yetmediğinde **custom rol** (Actions/NotActions/AssignableScopes) tanımlanır ve en az yetki ilkesiyle atanır. Owner gereğinden fazla yetki verir; lock tüm kullanıcıları etkiler ve Owner kaldırabilir (A). Policy kaynağın özelliklerini denetler, "kimin silebileceğini" değil (C). Global Administrator bir Entra (directory) rolüdür (D).
</details>

#### Soru 3: Evet / Hayır

Her ifade için Evet veya Hayır seç.

- a) Azure Virtual Desktop, birden çok kullanıcının aynı VM'i paylaştığı Windows 11 **multi-session** destekler.
- b) Windows 365, oturum host'larını kendi Azure aboneliğinde ölçeklediğin bir servistir.
- c) Bir kullanıcının bir storage account'ta **Contributor** rolü olması, o hesaptaki blob'ları Entra kimliğiyle okuyabileceği anlamına gelir.

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Hayır.** Windows 365 kullanıcı başına sabit fiyatlı, Microsoft'un yönettiği Cloud PC'dir; host'ları sen ölçeklemezsin. Contributor bir **control plane** rolüdür; Entra kimliğiyle blob okumak için *Storage Blob Data Reader* gibi bir **data plane** rolü gerekir.
</details>

#### Soru 4: Tek seçim

Bir yönetici portalda doğal dille "Bu abonelikte son bir haftada maliyeti en çok artan kaynaklar hangileri?" diye sormak ve bir Azure CLI taslağı almak istiyor. Hangisi bu deneyimi sağlar?

- A) Azure Cloud Shell
- B) Microsoft Copilot in Azure
- C) Azure Service Health
- D) Azure Arc

<details><summary>Cevap</summary>

**B.** Copilot in Azure portalda doğal dil soruları yanıtlar ve komut/şablon taslağı üretir; kullanıcının kendi RBAC yetkisiyle çalışır. Cloud Shell yalnızca komut çalıştırma ortamıdır (A); Service Health Azure olaylarını gösterir (C); Arc Azure dışı kaynakları yönetime alır (D).
</details>

#### Soru 5: Tek seçim

Uygulama **App Service (%99,95)** ve **Azure SQL Database (%99,99)** kullanıyor; ikisi seri bağlı. Toplam SLA yaklaşık nedir?

- A) %99,99
- B) %99,95
- C) %99,94
- D) %99,97

<details><summary>Cevap</summary>

**C.** Seri bağlı bileşenlerde SLA'lar çarpılır: `0,9995 × 0,9999 ≈ 0,9994`. Toplam, en zayıf bileşenden (%99,95) bile düşüktür.
</details>

---

➡️ [📝 50 soruluk deneme sınavı](quiz.md) · [AZ-900 İçindekiler](../README.md)
