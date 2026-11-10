[← AZ-900 İçindekiler](../README.md) · [📝 Quiz](quiz.md) · [← Önceki konu](../4.2-load-balancing-traffic/README.md)

## 4.3 Ağ Güvenliği, Bastion ve Key Vault

> **Sınav Alanı:** Describe Azure architecture and services — genişletilmiş kapsam · **Konu Kodu:** C1.18
>
> 📌 **Bağlantı:** *Describe Azure identity, access, and security* + C1.7/C1.10'daki "defense in depth" soruları; C2.11 (Key Vault geliştirici derinliği), M9, S8.3.
>
> Bu konu resmî study guide'ın (July 20, 2026) doğrudan hedefi değildir; kurs müfredatlarında ve senaryo sorularının seçeneklerinde geçer.

### 1. Ne?

[C1.10](../2.7-security-zero-trust-defender/README.md) *defense in depth* modelinin katmanlarını anlattı; bu konu o katmanlardaki **somut Azure servislerini** anlatır:

- **Ağ**
  - Servis: **NSG**
  - Tek cümle: Subnet/NIC düzeyinde, IP-port-protokol tabanlı **stateful** izin/engel kuralları
- **Ağ**
  - Servis: **ASG**
  - Tek cümle: VM'leri **mantıksal gruplara** ayırıp NSG kurallarında IP yerine grup adı kullanma
- **Çevre / ağ**
  - Servis: **Azure Firewall**
  - Tek cümle: Merkezi, yönetilen, **stateful** güvenlik duvarı (L3–L7, FQDN kuralları, threat intelligence)
- **Çevre**
  - Servis: **Azure DDoS Protection**
  - Tek cümle: Hacimsel/protokol saldırılarına karşı koruma
- **Uygulama / çevre**
  - Servis: **WAF**
  - Tek cümle: Web saldırılarına (SQL injection, XSS) karşı L7 koruma (→ [C1.17](../4.2-load-balancing-traffic/README.md))
- **Compute / yönetim erişimi**
  - Servis: **Azure Bastion**, **JIT VM access**
  - Tek cümle: Public IP'siz / süreli açılan yönetim erişimi
- **Ağ**
  - Servis: **Service endpoint ↔ Private endpoint**
  - Tek cümle: PaaS servisine ağ izolasyonlu erişim (→ C1.7)
- **Uygulama / veri**
  - Servis: **Azure Key Vault**
  - Tek cümle: Secret, anahtar ve sertifika için merkezi, denetimli depo
- **Veri**
  - Servis: **Şifreleme** (at rest / in transit)
  - Tek cümle: Platform yönetimli ya da müşteri yönetimli anahtarlar
- ***(bağlam)***
  - Servis: **Sentinel, Defender XDR, Customer Lockbox**
  - Tek cümle: SIEM/SOAR, XDR, Microsoft mühendis erişimine onay

Bu konunun kodu yoktur; pratik kısım `az` CLI laboratuvarıdır (3.7).

---

### 2. Neden?

- **Sınav:** "Hangi servis?" soruları: NSG ↔ Azure Firewall ↔ DDoS ↔ WAF; "VM'e public IP vermeden RDP/SSH" → Bastion; "parolayı/anahtarı kodda tutma" → Key Vault + managed identity. Defense in depth sorularının seçenekleri bu servislerden oluşur.
- **Gerçek işte:** İhlallerin çoğu **yanlış yapılandırmadan** olur: internete açık 3389/22, koda gömülü connection string, herkese açık storage. NSG + Bastion + Key Vault bu üç klasik deliği kapatır.
- **Bağlam:** Key Vault SDK, `DefaultAzureCredential`, managed identity kodu → C2.11; kimlik ve erişim → M9; kurumsal ağ güvenliği mimarisi (hub-spoke + Firewall) → S8.3.

---

### 3. Nasıl?

#### 3.1 Network Security Group (NSG) ⭐

**NSG**, bir **subnet'e** veya bir **NIC'e** (VM'in ağ kartı) bağlanan, trafiği **IP, port, protokol** ile filtreleyen güvenlik kuralları listesidir.

- **Kural alanları:** öncelik (priority), ad, **yön** (inbound/outbound), kaynak, hedef, port, protokol, **eylem** (Allow/Deny).
- **Değerlendirme:** kurallar **öncelik numarasına göre, küçükten büyüğe (100–4096)** işlenir; **ilk eşleşen kural uygulanır**, sonrakilere bakılmaz.
- **Stateful:** bir bağlantıya inbound izin verdiysen **dönüş trafiği için ayrı kural gerekmez** (ve tersi).
- **Hizmet etiketleri (service tags):** `Internet`, `VirtualNetwork`, `AzureLoadBalancer`, `Storage`, `Sql`… — Microsoft'un yönettiği IP gruplarıdır; IP listesini elle takip etmezsin.
- **Varsayılan kurallar** (silinemez, en düşük öncelikli — 65000+):

- **Inbound** — Kural: `AllowVnetInBound` · Öncelik: 65000 · Etki: VNet içi trafiğe izin
- **Inbound**
  - Kural: `AllowAzureLoadBalancerInBound` · Öncelik: 65001
  - Etki: Azure LB health probe'larına izin
- **Inbound** — Kural: `DenyAllInBound` · Öncelik: 65500 · Etki: Geri kalan **her şey engel**
- **Outbound** — Kural: `AllowVnetOutBound` · Öncelik: 65000 · Etki: VNet içi
- **Outbound** — Kural: `AllowInternetOutBound` · Öncelik: 65001 · Etki: İnternete çıkışa **izin**
- **Outbound** — Kural: `DenyAllOutBound` · Öncelik: 65500 · Etki: Geri kalan engel

> Yani: internetten **gelen** trafik varsayılan olarak **engelli**; **giden** internet trafiği varsayılan olarak **serbest**.

- **Subnet + NIC birlikte:** ikisinde de NSG varsa, **inbound** trafik önce subnet NSG'den, sonra NIC NSG'den geçer; **outbound** tersi. **İkisi de izin vermelidir.**
- **NSG ücretsizdir** (kural sayısı limitine bağlı).
- NSG **L3/L4** filtredir: HTTP içeriğini, URL'yi, FQDN'i bilmez.

**Application Security Group (ASG):** "web sunucuları", "veri sunucuları" gibi **mantıksal gruplar** tanımlayıp NSG kuralında IP yerine grup adını kullanırsın (`asg-web → asg-db : 1433 izin`). VM eklendikçe kural değişmez — kuralları **uygulama yapısına göre** yazmanın yolu.

#### 3.2 Azure Firewall ⭐ ve NSG ile farkı

**Azure Firewall**, VNet'e konuşlandırılan **yönetilen, bulut-yerel, stateful** bir ağ güvenlik duvarıdır (yerleşik yüksek erişilebilirlik ve ölçekleme).

| Yetenek | Açıklama |
|---|---|
| **Kural türleri** | **Network** (IP/port/protokol), **Application** (**FQDN** ve URL — `*.windows.net`'e çıkış izin), **DNAT** (gelen trafiği iç adrese çevir) |
| **Threat intelligence** | Bilinen kötü IP/domain'lerle trafiği **uyar veya engelle** |
| **FQDN etiketleri** | Windows Update, Azure servis FQDN grupları için hazır etiketler |
| **Merkezi yönetim** | **Firewall Policy** (Firewall Manager ile çoklu firewall/subscription) |
| **Hub-spoke** | Tüm spoke'lardan çıkış/giriş trafiği hub'daki firewall'dan (UDR ile) geçirilir |
| **SKU'lar** | **Basic** (küçük/orta işletme), **Standard**, **Premium** (**TLS inspection, IDPS**, URL filtreleme, web kategorileri) |

> **Maliyet uyarısı:** Azure Firewall **sabit saatlik** bir ücret + işlenen veri ücreti taşır ve küçük bir lab için bile pahalıdır (Premium en pahalı). Bu yüzden laboratuvarda **kurmuyoruz**; teoridir.

**NSG ↔ Azure Firewall** ⭐

- **Ne?**
  - NSG: Subnet/NIC'te **paket filtresi** (dağıtık)
  - Azure Firewall: **Merkezi**, yönetilen güvenlik duvarı servisi
- **Katman**
  - NSG: L3–L4 (IP, port, protokol)
  - Azure Firewall: L3–**L7** (FQDN, URL; Premium'da TLS/IDPS)
- **FQDN/URL filtreleme** — NSG: **Yok** · Azure Firewall: **Var**
- **Threat intelligence / IDPS** — NSG: Yok · Azure Firewall: Var (IDPS Premium)
- **Kapsam**
  - NSG: Tek VNet'in subnet'i/NIC'i
  - Azure Firewall: Birçok VNet/subscription'ı **merkezden** (hub-spoke)
- **Maliyet** — NSG: **Ücretsiz** · Azure Firewall: **Sabit saatlik + veri**
- **Tipik rol**
  - NSG: **Segmentasyon**, mikro-ağ kuralları
  - Azure Firewall: **Çevre ve çıkış kontrolü**, merkezi politika

İkisi **birlikte** kullanılır (defense in depth): NSG her subnet'te "ne konuşabilir"i, Firewall merkezde "internete/dışarıya ne gidebilir"i belirler.

#### 3.3 Azure DDoS Protection

**DDoS** (Distributed Denial of Service): kaynağı, çok sayıda kaynaktan gelen sahte trafikle erişilemez kılma saldırısı.

- **Altyapı koruması (basic)**
  - Kapsam: **Tüm Azure** public IP'leri için **varsayılan, ücretsiz, her zaman açık**
  - Not: Platformun kendini koruması; ayarlanamaz
- **DDoS Network Protection**
  - Kapsam: Bir **VNet**'teki tüm public IP'ler (plan VNet'lere bağlanır)
  - Not: Adaptif ayarlama, **DDoS Rapid Response** uzman desteği, **maliyet koruması**, WAF indirimi; sabit aylık ücret **yüksek**
- **DDoS IP Protection**
  - Kapsam: **Tek tek public IP** başına
  - Not: Aynı mitigasyon motoru; **Rapid Response, cost protection, WAF indirimi yok**; küçük ölçeklerde daha ucuz

> Eski adlandırma: "DDoS Protection **Basic / Standard**" — sınav materyallerinde hâlâ geçebilir. **Basic ≈ varsayılan altyapı koruması; Standard ≈ Network Protection**. Fiyatlar için güncel fiyat sayfasına bak.
> DDoS Protection **L3/L4 hacimsel** saldırıları durdurur; **uygulama katmanı (L7)** saldırılar (SQL injection, bot) için **WAF** gerekir — ikisi tamamlayıcıdır.

#### 3.4 WAF, JIT ve Bastion

- **Web Application Firewall (WAF):** OWASP tipi web saldırılarına karşı **L7** koruma; **Application Gateway** veya **Front Door** üzerinde yaşar (→ [C1.17](../4.2-load-balancing-traffic/README.md)).
- **Just-in-time (JIT) VM access** *(Defender for Cloud özelliği → C1.10)*: yönetim portları (22/3389) **varsayılan kapalıdır**; kullanıcı **istek yapınca, belirli IP ve süre için** NSG kuralı otomatik açılır, süre bitince kapanır.

**Azure Bastion** ⭐ — VM'e **public IP olmadan**, tarayıcıdan (Azure portal) veya native client ile **TLS (443)** üzerinden RDP/SSH bağlantısı sağlayan yönetilen PaaS.

| Özellik | Değer |
|---|---|
| Neden? | VM'lerde **22/3389'u internete açmamak**; VM'in public IP'si olmaması |
| Konum | VNet'te özel **`AzureBastionSubnet`** (genellikle `/26` veya daha büyük); peering'li VNet'lerdeki VM'lere de erişebilir (SKU'ya bağlı) |
| Güvenlik | Bağlantı TLS ile; VM'e erişim Bastion'ın **özel IP**'sinden; port taraması/kaba kuvvet yüzeyi yok |
| SKU'lar | **Developer** (paylaşımlı, **saatlik ücretsiz**, tek VM bağlantısı, sınırlı bölgeler), **Basic**, **Standard** (native client, dosya aktarımı, ölçekleme…), **Premium** (oturum kaydı, **public IP'siz özel dağıtım**) *(özellik–SKU eşlemesini güncel tablodan doğrula)* |

> **Maliyet uyarısı:** Basic/Standard/Premium Bastion, dağıtım **saatlik** ücretlenir (sürekli) ve lab için pahalıdır; bu yüzden **kurmuyoruz** (yalnızca teori). Developer SKU ücretsizdir ama kısıtlıdır (paylaşımlı altyapı, tek bağlantı, seçili bölgeler).

**Bastion ↔ JIT ↔ VPN:**

| İhtiyaç | Seçenek |
|---|---|
| Public IP'siz, tarayıcıdan RDP/SSH, yönetilen | **Bastion** |
| Portlar kapalı, istek anında süreli açılsın (VM'in public IP'si var/gerekli) | **JIT** |
| Geliştirici makinesinden VNet'e genel erişim | **Point-to-site VPN** (→ C1.7) |

#### 3.5 Service endpoint ↔ Private endpoint (özet)

(→ [C1.7](../2.4-networking/README.md) §3.6) **Service endpoint:** VNet'in kimliğini servise bildirir; servis firewall'ında yalnızca o subnet'e izin verirsin — ama servis **public IP'siyle** erişilir. **Private endpoint:** servise VNet içinde **özel IP** verir; public erişim tamamen kapatılabilir (**daha güçlü izolasyon**, ücretli).

#### 3.6 Azure Key Vault ⭐ ve şifreleme

**Key Vault**, **secret'ları** (parola, connection string, API anahtarı), **kriptografik anahtarları** ve **sertifikaları** güvenli saklayan, erişimi denetleyen ve **denetim kaydı** (audit log) tutan yönetilen servistir. Amaç: secret'ların **kodda, config dosyasında veya repoda** durmaması.

- **Secret**
  - Ne?: Küçük gizli metin (≤ 25 KB)
  - Örnek: DB parolası, API anahtarı, connection string
- **Key**
  - Ne?: Şifreleme/imzalama için **kriptografik anahtar** (RSA/EC); anahtarın kendisi dışarı çıkmadan işlem yapılır
  - Örnek: Customer-managed key, imzalama
- **Certificate**
  - Ne?: Sertifika yaşam döngüsü (oluştur, yenile, sakla)
  - Örnek: TLS sertifikası

**Katmanlar:**

- **Anahtar koruması**
  - Standard: Yazılım tabanlı
  - Premium: **HSM destekli** anahtarlar (paylaşımlı HSM)
  - Managed HSM: **Tek kiracılı, ayrılmış HSM** (yüksek güvence/uyumluluk)
- **Tür** — Standard: Key Vault · Premium: Key Vault · Managed HSM: Ayrı kaynak türü; kendi RBAC'ı
- **Ne zaman?**
  - Standard: Çoğu uygulama · Premium: Uyumluluk HSM gerektiriyorsa
  - Managed HSM: Katı regülasyon, tam anahtar sahipliği

**Erişim kontrolü — iki düzlem:**

- **Yönetim düzlemi (management plane):** kasa oluşturma/silme/ayar — **Azure RBAC** (Owner, Contributor…).
- **Veri düzlemi (data plane):** secret/key/certificate **okuma/yazma** — **Azure RBAC** (önerilen) **veya** eski **vault access policy**.

- **Rol örnekleri**
  - Azure RBAC (önerilen): `Key Vault Administrator`, `Secrets Officer`, `Secrets User`, `Crypto User`, `Certificates Officer`, `Reader`
  - Access policy (eski model): Kasa başına izin listesi (get/list/set…)
- **Kapsam**
  - Azure RBAC (önerilen): Kasa, resource group, subscription ya da **tek nesne** düzeyinde
  - Access policy (eski model): Yalnızca kasa düzeyinde
- **Tutarlılık**
  - Azure RBAC (önerilen): Tüm Azure ile aynı model, PIM/koşullu erişim, merkezi denetim
  - Access policy (eski model): Ayrı yönetim, ayrıntı sınırlı
- **Tuzak**
  - Azure RBAC (önerilen): —
  - Access policy (eski model): **Contributor** rolü olan biri kendine access policy ekleyip **secret'lara erişebilir** (RBAC modelinde Contributor veri düzlemine erişemez)

**Silinme koruması:**

- **Soft delete:** silinen kasa/nesne **bekleme süresi** (7–90 gün, varsayılan 90) boyunca geri alınabilir (`recover`). Artık **her zaman açıktır**.
- **Purge protection:** açıksa, silinmiş nesne bekleme süresi dolmadan **kalıcı silinemez** (kimse — yönetici dahil — `purge` yapamaz). **Bir kez açılınca kapatılamaz.** Kritik üretim kasalarında önerilir; lab'da **açma** (kasa adı bekleme süresince rezerve kalır).

**Managed identity ile kimlik bilgisiz erişim** (→ C1.9, C2.11): uygulama (App Service, VM, Function) bir **managed identity** alır; bu kimliğe kasada `Key Vault Secrets User` rolü verilir; uygulama kodu **parola olmadan** token alıp secret'ı okur:

```csharp
// No password in code: DefaultAzureCredential picks up the managed identity in Azure
var client = new SecretClient(new Uri("https://kv-demo.vault.azure.net/"), new DefaultAzureCredential());
KeyVaultSecret secret = await client.GetSecretAsync("DbPassword");
```

Alternatif: App Service ayarında **Key Vault reference** (`@Microsoft.KeyVault(SecretUri=...)`) — kod hiç değişmez, uygulama ortam değişkeni olarak okur (→ C1.6).

**Şifreleme özeti:**

| | Açıklama |
|---|---|
| **Encryption at rest** (durağan) | Storage, managed disk, SQL (TDE), Cosmos DB vb. varsayılan olarak **sunucu tarafında şifrelenir** — **platform-managed key (PMK)** ile, ek yapılandırma gerekmez |
| **Customer-managed key (CMK)** | Anahtarı **sen** Key Vault/Managed HSM'de tutarsın; rotasyon/iptal kontrolü sende (regülasyon, "anahtarı kendi kontrolümde istiyorum") |
| **Encryption in transit** | **TLS** (HTTPS, SQL bağlantı şifrelemesi); VPN/ExpressRoute ayrıca (→ C1.7) |
| **Azure Disk Encryption (ADE)** | VM disklerini OS içinde (BitLocker/dm-crypt) şifreler; anahtar Key Vault'ta. **2026 notu:** ADE'nin **15 Eylül 2028**'de emekliye ayrılması planlanıyor; Microsoft yeni VM'lerde **encryption at host** öneriyor *(tarihi resmî duyurudan doğrula)* |
| **Encryption at host** | VM host'unda uçtan uca şifreleme (temp disk ve cache dahil) |

#### 3.7 Bağlam: Sentinel, Defender XDR, Customer Lockbox

- **Microsoft Sentinel:** bulut-yerel **SIEM/SOAR** — log toplar, olayları korele eder, otomatik müdahale (playbook) çalıştırır. SOC aracıdır. *(Sentinel'in yönetim deneyimi **Microsoft Defender portal**'a taşınıyor; Azure portal desteğinin Mart 2027'de bittiği duyuruldu — tarihi resmî duyurudan doğrula.)*
- **Microsoft Defender XDR:** Microsoft 365 tarafındaki uç nokta, e-posta, kimlik, uygulama korumalarını **birleştiren** XDR portalı; **Defender for Cloud** (→ C1.10) **Azure/multicloud iş yükü** koruması ve posture yönetimidir. İkisi **birbirini tamamlar**: Defender for Cloud uyarıları Defender XDR/Sentinel'e akar.
- **Customer Lockbox:** Microsoft mühendisinin **destek talebi sırasında senin verilerine** erişmesi gerekirse, bu erişimi sen **onaylarsın/reddedersin**.
- **Paylaşılan sorumluluk (→ C1.1):** IaaS'ta NSG/yama/Key Vault politikası sende; PaaS'ta platform altyapısı Microsoft'ta; **veri ve kimlik her zaman müşterinde.**

#### 3.8 Pratik — NSG ve Key Vault

> Bütçe alarmın kurulu olsun (→ C1.1). **NSG, ASG ve VNet ücretsizdir.** Key Vault'ta işlem başına çok küçük bir ücret vardır (lab için sentim). **Bastion ve Azure Firewall'ı KURMUYORUZ** — saatlik sabit ücretleri yüksektir; teoridir. Key Vault'ta **purge protection AÇMA** (kapatılamaz ve kasa adını günlerce rezerve eder).

```bash
az login
RG=rg-az900-c118; LOC=westeurope
az group create --name $RG --location $LOC

# 1) NSG + rules: 80/443 from the internet, 22/3389 only from your own IP
MYIP=$(curl -s https://api.ipify.org)
az network nsg create --resource-group $RG --name nsg-web --location $LOC
az network nsg rule create --resource-group $RG --nsg-name nsg-web --name allow-web \
  --priority 100 --direction Inbound --access Allow --protocol Tcp \
  --source-address-prefixes Internet --destination-port-ranges 80 443
az network nsg rule create --resource-group $RG --nsg-name nsg-web --name allow-mgmt-from-me \
  --priority 110 --direction Inbound --access Allow --protocol Tcp \
  --source-address-prefixes "$MYIP/32" --destination-port-ranges 22 3389
az network nsg rule list --resource-group $RG --nsg-name nsg-web --include-default --output table
#   Custom rules at 100/110, defaults at 65000+ (…DenyAllInBound at 65500)

# 2) Attach the NSG to a subnet
az network vnet create --resource-group $RG --name vnet-demo --address-prefixes 10.10.0.0/16 \
  --subnet-name snet-web --subnet-prefixes 10.10.1.0/24 --network-security-group nsg-web

# 3) ASG: rule by application role instead of IP
az network asg create --resource-group $RG --name asg-web
az network asg create --resource-group $RG --name asg-db
az network nsg create --resource-group $RG --name nsg-db
az network nsg rule create --resource-group $RG --nsg-name nsg-db --name allow-sql-from-web \
  --priority 100 --direction Inbound --access Allow --protocol Tcp \
  --source-asgs asg-web --destination-asgs asg-db --destination-port-ranges 1433

# 4) Key Vault with RBAC authorization (soft delete is always on; short retention for the lab)
KV=kv-az900-$RANDOM
az keyvault create --resource-group $RG --name $KV --location $LOC \
  --enable-rbac-authorization true --retention-days 7

# 5) Grant yourself a data-plane role (Owner on the vault is NOT enough to read secrets under RBAC)
ME=$(az ad signed-in-user show --query id --output tsv)
KVID=$(az keyvault show --name $KV --query id --output tsv)
az role assignment create --assignee-object-id $ME --assignee-principal-type User \
  --role "Key Vault Secrets Officer" --scope $KVID
sleep 60   # role assignment propagation

# 6) Create / read a secret
az keyvault secret set --vault-name $KV --name DbPassword --value 'Demo-P@ssw0rd!'
az keyvault secret show --vault-name $KV --name DbPassword --query value --output tsv

# 7) Soft delete: delete, see it in the deleted list, recover
az keyvault secret delete --vault-name $KV --name DbPassword
az keyvault secret list-deleted --vault-name $KV --output table
az keyvault secret recover --vault-name $KV --name DbPassword
az keyvault secret show --vault-name $KV --name DbPassword --query attributes.enabled

# 8) Passwordless access: a managed identity that can only READ secrets
az identity create --resource-group $RG --name id-app
PID=$(az identity show --resource-group $RG --name id-app --query principalId --output tsv)
az role assignment create --assignee-object-id $PID --assignee-principal-type ServicePrincipal \
  --role "Key Vault Secrets User" --scope $KVID

# 9) Cleanup — mandatory. Deleting the group only SOFT-deletes the vault (the name stays reserved), so purge it
az keyvault delete --name $KV
az keyvault purge --name $KV --location $LOC          # works because purge protection is NOT enabled
az group delete --name $RG --yes --no-wait
```

<details><summary>Beklenen gözlemler ve açıklamaları</summary>

- **1**
  - Gözlem: Özel kurallar 100/110'da; `65000-65500` varsayılanlar listelenir
  - Kavram: Öncelik ve varsayılan kurallar; `DenyAllInBound` en sonda
- **2** — Gözlem: Subnet'e NSG bağlanır · Kavram: NSG subnet düzeyinde uygulanır
- **3**
  - Gözlem: ASG kuralı IP içermez
  - Kavram: Kuralları uygulama rolüne göre yaz
- **5**
  - Gözlem: Rol atamadan `secret set` → `Forbidden` / yetki hatası (Owner olsan bile)
  - Kavram: RBAC modelinde **veri düzlemi** ayrı roller ister
- **6**
  - Gözlem: Secret değeri düz metin döner
  - Kavram: Kasadan okuma; erişimler audit log'a yazılır
- **7**
  - Gözlem: Silinen secret `list-deleted`'da görünür, `recover` ile geri gelir
  - Kavram: Soft delete
- **8**
  - Gözlem: Identity yalnızca `Secrets User` — secret okur ama **yazamaz/silemez**
  - Kavram: En az yetki + managed identity
- **9**
  - Gözlem: `az keyvault purge` olmadan aynı ad yeniden alınamaz
  - Kavram: Soft-deleted kasa adı rezerve kalır

**Düşünce deneyi:** VM'e RDP açmak için Bastion kullansaydık, NSG'de 3389'a hâlâ internetten izin gerekir miydi? (Hayır — Bastion VM'e VNet içinden özel IP ile bağlanır; VM'de public IP olmaz.)
</details>

---

### 4. Gerçek dünya senaryosu

Bir sağlık-teknolojisi şirketi hasta portalını Azure'a taşıyor:

1. **Çevre:** Portal **Front Door + WAF** arkasında; **DDoS Network Protection** public IP'leri koruyor (rapid response ve maliyet koruması gerekçesiyle).
2. **Ağ segmentasyonu:** Web, uygulama ve veri subnet'lerine **NSG**; kurallar **ASG** ile (`asg-web → asg-app → asg-db`). Varsayılan "engelle"; yalnızca gereken portlar açık.
3. **Çıkış kontrolü:** Tüm giden trafik hub'daki **Azure Firewall (Premium)** üzerinden; yalnızca onaylı FQDN'lere (ödeme sağlayıcı, güncelleme sunucuları) izin, **IDPS** açık.
4. **Yönetim erişimi:** VM'lerde **public IP yok**; yöneticiler **Bastion** ile bağlanır; ihtiyaç anında **JIT** ile süreli port açılır.
5. **Sırlar:** Connection string ve API anahtarları **Key Vault**'ta (RBAC); uygulamalar **managed identity** ile okur. Müşteri yönetimli anahtarlar **Key Vault Premium/Managed HSM**'de; **purge protection** açık.
6. **Veri:** SQL ve Storage için **private endpoint**, public erişim kapalı (→ C1.7); durağan şifreleme (CMK), aktarımda TLS.
7. **İzleme:** Defender for Cloud (→ C1.10) uyarıları **Sentinel**'e akar; SOC olayları oradan yönetir. **Customer Lockbox** açık: Microsoft destek mühendisi veriye ancak onayla erişir.
8. **Sonuç:** Tek bir katman (ör. NSG yanlış yapılandırması) aşılsa bile sonraki katmanlar yavaşlatır/durdurur/tespit eder — defense in depth.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** "Hangi servis hangi katmanı çözer?" senaryoları; NSG/Firewall ayrımı; Bastion; Key Vault'un amacı; evet/hayır tabloları.

| Senaryodaki ifade | Cevap |
|---|---|
| "Subnet/NIC düzeyinde IP-port izin/engel kuralları, ücretsiz" | **NSG** |
| "VM'leri uygulama rolüne göre grupla, kuralda IP yerine grup kullan" | **ASG** |
| "Merkezi, FQDN'e göre çıkış filtreleme, threat intelligence" | **Azure Firewall** |
| "TLS inspection, IDPS" | **Azure Firewall Premium** |
| "Hacimsel (volumetric) DDoS saldırısına karşı koruma" | **DDoS Protection** (temel: varsayılan; Network/IP: ücretli) |
| "SQL injection, XSS" | **WAF** |
| "VM'e public IP olmadan, portaldan RDP/SSH" | **Azure Bastion** |
| "Yönetim portları normalde kapalı, istek anında süreli açılsın" | **Just-in-time VM access** |
| "Secret/parola/sertifika/anahtarı güvenle sakla ve denetle" | **Key Vault** |
| "Uygulama, kodda parola olmadan Key Vault'a erişsin" | **Managed identity** |
| "Anahtar sahipliği müşteride, HSM tek kiracılı" | **Managed HSM** / customer-managed key |
| "Yanlışlıkla silinen secret geri alınsın" | **Soft delete** (purge protection: kalıcı silmeyi engeller) |
| "Microsoft destek mühendisinin verilerine erişimini sen onayla" | **Customer Lockbox** |
| "SIEM/SOAR, olay korelasyonu, playbook" | **Microsoft Sentinel** |

**Tuzaklar:**
- **NSG ↔ Azure Firewall:** NSG ücretsiz L3/L4 dağıtık filtre; Firewall ücretli, merkezi, FQDN/L7.
- **DDoS temel ↔ Network:** temel koruma **her zaman açık ve ücretsiz**; Network Protection (eski "Standard") ek yetenekler ve ücret getirir; **DDoS L3/L4**, **WAF L7**.
- **Bastion = public IP'siz erişim** — "VM'e public IP ver, 3389'u internete aç" **yanlış** cevaptır.
- "Secret'ı `appsettings.json`'a/repoya koy" → **Key Vault + managed identity**.
- "Key Vault'taki secret'ı okumak için **Contributor** yeterli" → RBAC modelinde **hayır**; veri düzlemi rolü gerekir.
- "NSG stateless'tır" → **Hayır**, stateful.
- "Varsayılan olarak outbound internet engellidir (NSG)" → **Hayır**, `AllowInternetOutBound` vardır; inbound engelli.
- "Purge protection geri alınabilir" → **Hayır**, bir kez açılınca kapatılamaz.
- "Sentinel = Defender for Cloud" → **Hayır**: Sentinel SIEM/SOAR (olay analizi), Defender for Cloud posture + workload koruması.

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| 22/3389'u `Any` kaynağa açık bırakmak | Kaynak IP kısıtla; **Bastion/JIT/VPN** |
| NSG kurallarını IP'lerle yazıp VM eklendikçe yamamak | **ASG** ve **service tag**'ler |
| Düşük öncelikli numaralı `Allow`'un, sonraki `Deny`'ı ezdiğini unutmak | Önce eşleşen kazanır; öncelikleri bilinçli sırala |
| Subnet NSG'de izin verip NIC NSG'de engeli unutmak | İkisi de izin vermeli |
| NSG'nin `AzureLoadBalancer` probe trafiğini engellemesi | Probe'a izin ver (→ C1.17) |
| NSG ile FQDN'e göre çıkış filtrelemeyi denemek | FQDN için Azure Firewall |
| DDoS'u "WAF olmadan yeterli" sanmak | DDoS L3/L4; L7 için WAF |
| Secret'ları kodda/config'te/Git'te tutmak | Key Vault + managed identity |
| Kasada herkese `Key Vault Administrator` vermek | En az yetki: `Secrets User` gibi dar roller |
| Eski access policy modeliyle başlamak | Yeni kasalarda **RBAC** |
| Üretim kasasında purge protection'ı açmamak | Kritik kasalarda aç (bir kez açılınca geri alınamaz; lab'da açma) |
| Lab kasasını resource group'la silip adın rezerve kaldığını fark etmek | `az keyvault purge` |
| Bastion/Firewall'ı lab'da kurup silmeyi unutmak | Saatlik ücretlenir; mümkünse **kurma**, sil |
| Durağan şifrelemeyi "kapalı" sanıp ek iş yapmak | Çoğu servis varsayılan şifreler; CMK yalnızca gereksinim varsa |

---

### 7. Performans & trade-off

- **NSG ile segmentasyon**
  - Kazanç: Ücretsiz, basit, dağıtık
  - Bedel: L3/L4 sınırı; kural karmaşıklığı büyüyebilir (ASG ile azaltılır)
- **Azure Firewall**
  - Kazanç: Merkezi politika, FQDN/L7, IDPS, çıkış kontrolü
  - Bedel: Sabit saatlik ücret yüksek; tüm trafik hub'dan geçer (gecikme/tek nokta tasarımı)
- **DDoS Network Protection**
  - Kazanç: Rapid Response, maliyet koruması, adaptif ayar
  - Bedel: Yüksek aylık sabit ücret; IP Protection daha ucuz ama daha az yetenek
- **Bastion**
  - Kazanç: Public IP'siz yönetim, kaba kuvvet yüzeyi yok
  - Bedel: Saatlik ücret; ek subnet; bazı SKU'larda sınırlı özellik
- **JIT**
  - Kazanç: Portlar çoğu zaman kapalı
  - Bedel: Talep/onay süreci; Defender for Servers gerektirir (ücretli plan)
- **Key Vault + managed identity**
  - Kazanç: Secret kodda yok, denetim, rotasyon
  - Bedel: Bağımlılık (kasa kesintisi/throttling → önbellekle); RBAC yönetimi
- **Customer-managed key**
  - Kazanç: Anahtar kontrolü, iptal imkânı
  - Bedel: Operasyonel yük; anahtar kaybı = veri kaybı; ek gecikme/maliyet
- **Managed HSM**
  - Kazanç: En yüksek güvence, tek kiracılı
  - Bedel: En pahalı seçenek; yönetim karmaşıklığı
- **Purge protection**
  - Kazanç: Kötü niyetli/yanlışlıkla kalıcı silmeyi engeller
  - Bedel: Geri alınamaz; kasa adı süre boyunca rezerve

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Network security groups](https://learn.microsoft.com/en-us/azure/virtual-network/network-security-groups-overview) · [Application security groups](https://learn.microsoft.com/en-us/azure/virtual-network/application-security-groups)
- [What is Azure Firewall?](https://learn.microsoft.com/en-us/azure/firewall/overview) · [Azure Firewall features by SKU](https://learn.microsoft.com/en-us/azure/firewall/features-by-sku)
- [Azure DDoS Protection overview](https://learn.microsoft.com/en-us/azure/ddos-protection/ddos-protection-overview) · [Tier comparison](https://learn.microsoft.com/en-us/azure/ddos-protection/ddos-protection-sku-comparison)
- [What is Azure Bastion?](https://learn.microsoft.com/en-us/azure/bastion/bastion-overview) · [Bastion SKU comparison](https://learn.microsoft.com/en-us/azure/bastion/bastion-sku-comparison)
- [About Azure Key Vault](https://learn.microsoft.com/en-us/azure/key-vault/general/overview) · [Key Vault RBAC guide](https://learn.microsoft.com/en-us/azure/key-vault/general/rbac-guide) · [Soft delete](https://learn.microsoft.com/en-us/azure/key-vault/general/soft-delete-overview)
- [Azure Disk Encryption retirement / encryption at host](https://learn.microsoft.com/en-us/azure/virtual-machines/disk-encryption-overview)
- [Microsoft Sentinel in the Defender portal](https://learn.microsoft.com/en-us/azure/sentinel/microsoft-sentinel-defender-portal) · [Customer Lockbox](https://learn.microsoft.com/en-us/azure/security/fundamentals/customer-lockbox-overview)
- [`az network nsg`](https://learn.microsoft.com/en-us/cli/azure/network/nsg) · [`az keyvault`](https://learn.microsoft.com/en-us/cli/azure/keyvault)

---

➡️ **Sıradaki:** [📝 Bu konunun quiz soruları](quiz.md)
