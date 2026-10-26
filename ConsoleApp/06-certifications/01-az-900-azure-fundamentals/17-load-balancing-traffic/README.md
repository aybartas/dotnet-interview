# C1.17 Yük Dengeleme ve Trafik Dağıtımı

> 📁 `ConsoleApp/06-certifications/01-az-900-azure-fundamentals/17-load-balancing-traffic/` · Sınav alanı: **Describe Azure architecture and services — genişletilmiş kapsam (bağlam)** · Önkoşul: [C1.4](../04-core-architecture/README.md), [C1.7](../07-networking/README.md) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Bağlantı:** *Describe Azure networking services* (study guide'da "load balancing" adıyla yazmaz ama C1.7 ve C1.5'in senaryo soruları bunu varsayar); S8.3 (AZ-305) için zemin.
>
> Bu konu resmî study guide'ın (July 20, 2026) doğrudan hedefi değildir; kurs müfredatlarında ve senaryo sorularının seçeneklerinde geçer.

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

**Yük dengeleme (load balancing)**, gelen istekleri birden çok sunucu/instance arasında dağıtarak (a) tek sunucuyu aşırı yüklememek, (b) sağlıksız instance'ı devreden çıkarmak ve (c) kesintisiz hizmeti sağlamaktır. Azure'da dört ana servis vardır; hepsini **iki eksende** konumlandır:

```
                  Katman 4 (TCP/UDP)            Katman 7 (HTTP/HTTPS)
               ┌─────────────────────────┬──────────────────────────────┐
   Bölgesel    │  Azure Load Balancer    │  Application Gateway         │
               ├─────────────────────────┼──────────────────────────────┤
   Global      │  (Traffic Manager: DNS) │  Azure Front Door            │
               └─────────────────────────┴──────────────────────────────┘
        Traffic Manager = DNS tabanlı (trafiğin içinden geçmez): katman 4/7 ayrımına girmez, "global" ekseninde
```

| Servis | Katman | Kapsam | Tek cümle |
|---|---|---|---|
| **Azure Load Balancer** | L4 (TCP/UDP) | Bölgesel | Hızlı, düşük gecikmeli paket düzeyi dağıtım; HTTP'yi anlamaz |
| **Application Gateway** | L7 (HTTP/HTTPS) | Bölgesel | URL/host tabanlı yönlendirme, SSL termination, **WAF** |
| **Azure Front Door** | L7 (HTTP/HTTPS) | **Global** | Edge'de (POP) karşılayan global giriş: hızlandırma, CDN, WAF, failover |
| **Traffic Manager** | DNS | **Global** | DNS yanıtını değiştirerek istemciyi bir endpoint'e yönlendirir |

> *(bağlam)* Azure Load Balancer'ın **cross-region (global) tier**'ı da vardır (L4, anycast). Sınav temelinde "Load Balancer = bölgesel, L4" cevabı doğrudur; global L4 ihtiyacı çok nadirdir.

Bu konunun kodu yoktur; pratik kısım `az` CLI laboratuvarıdır (3.7).

---

## 2. Neden?

- **Sınav:** "Hangi servis?" senaryoları (WAF/SSL offload/URL routing → Application Gateway; global HTTP + hızlandırma → Front Door; DNS tabanlı → Traffic Manager; TCP/UDP → Load Balancer). Ayrım **iki anahtar**la yapılır: **bölgesel/global** ve **L4/L7**.
- **Gerçek işte:** Tek instance = tek arıza noktası. Yük dengeleyici + availability zone'lar (→ C1.4) uygulama içi yüksek erişilebilirliği, global servisler **region arızasına** karşı dayanıklılığı sağlar. SLA hesabında (→ C1.2) trafiği yönlendiren katman da **seri bir bileşendir**.
- **Bağlam:** Mimari kararlar (hub-spoke, multi-region aktif/aktif) → S8.3; ASP.NET Core arkasında proxy başlıkları (`X-Forwarded-*`), health endpoint'leri (`/health`) → M12.

---

## 3. Nasıl?

### 3.1 Azure Load Balancer (L4, bölgesel) ⭐

Gelen **TCP/UDP** akışlarını 5-tuple hash'ine (kaynak IP, kaynak port, hedef IP, hedef port, protokol) göre backend havuzundaki sağlıklı instance'lara dağıtır. İçeriği (HTTP başlığı, URL) **görmez**; çok hızlıdır.

| Kavram | Açıklama |
|---|---|
| **Frontend IP** | İstemcilerin bağlandığı adres. **Public LB:** public IP; **Internal LB:** VNet içi özel IP (ör. web katmanından veri katmanına) |
| **Backend pool** | Trafiği alan VM'ler / VM Scale Set instance'ları |
| **Health probe** | Instance sağlıklı mı? TCP / HTTP / HTTPS ile periyodik yoklama; **sağlıksız instance trafikten çıkarılır** |
| **Load-balancing rule** | `frontend:port → backend:port` eşlemesi (ör. 80 → 80) |
| **Inbound NAT rule** | Belirli bir porttan belirli **tek** bir VM'e yönlendirme (ör. `:50001` → VM1:22) — dağıtım değil, port yönlendirme |
| **Outbound rule** | Backend'in internete çıkış (SNAT) yönetimi |
| **Session persistence** | Varsayılan 5-tuple hash; istenirse **source IP affinity** (aynı istemci aynı instance'a) |
| **Zone-redundant frontend** | Frontend IP, tüm availability zone'larda yaşar — bir zone düşse de ayakta (→ C1.4) |

**Standard ↔ Basic:**

| | **Standard** | **Basic** |
|---|---|---|
| Durum | Güncel, önerilen | **Emekliye ayrıldı** (30 Eylül 2025); yeni dağıtım yapılamaz *(durum notları sayfalarda farklı ifade edilebilir — doğrula)* |
| Availability zone | Zone-redundant / zonal | Yok |
| SLA | Var (%99,99 — backend'de ≥ 2 sağlıklı instance şartıyla) | Yok |
| Güvenlik | Varsayılan **kapalı** (inbound için **NSG ile izin** gerekir) | Varsayılan açık |
| Probe | TCP, HTTP, HTTPS | TCP, HTTP |

> Sınav tuzağı: Standard Load Balancer **secure by default**'tur — NSG ile açıkça izin vermezsen inbound trafik geçmez.

*(bağlam)* **Gateway Load Balancer**: ağ sanal cihazlarını (NVA — firewall, IDS) şeffaf biçimde zincire eklemek için.

### 3.2 Application Gateway (L7, bölgesel) ⭐

HTTP/HTTPS trafiği için **web trafiği yük dengeleyici** — isteğin **içeriğine** (URL yolu, host adı, başlık, cookie) göre karar verir.

| Yetenek | Ne yapar? |
|---|---|
| **URL path-based routing** | `/images/*` → görsel havuzu, `/api/*` → API havuzu |
| **Multi-site / host-based routing** | `a.contoso.com` ve `b.contoso.com` aynı gateway'de, farklı backend'lere |
| **SSL/TLS termination (offload)** | TLS'i gateway'de sonlandır; backend'e HTTP (veya end-to-end TLS ile yeniden şifrele) — backend CPU'sunu rahatlatır |
| **Cookie-based session affinity** | Aynı kullanıcıyı aynı backend'e yapıştır |
| **Web Application Firewall (WAF)** | SQL injection, XSS gibi **OWASP** saldırılarına karşı koruma |
| **Header/URL rewrite, redirect** | İstek/yanıt başlıklarını değiştir, HTTP→HTTPS yönlendir |
| **Autoscale + zone redundancy** | **v2** SKU ile (Standard_v2 / WAF_v2) |
| Backend türleri | VM, VMSS, App Service, IP/FQDN (hibrit) |

- **V1 emekli oldu:** Application Gateway **V1**, **28 Nisan 2026** itibarıyla emekliye ayrıldı; yeni/mevcut dağıtımlar **V2**'ye taşınmalıdır. Sınav SKU adlarını sormaz.
- Frontend **public veya private IP** olabilir (internal uygulama geçidi).
- *(bağlam)* **Application Gateway for Containers**: AKS için L7 yük dengeleme/ingress.
- WAF, **Front Door** ve Application Gateway'e **eklenir**; bağımsız bir "WAF servisi" değil, bu ikisinin özelliğidir (→ [C1.18](../18-network-security-key-vault/README.md)).

### 3.3 Azure Front Door (L7, global) ⭐

Dünya çapındaki **edge location'larda (POP)** (Microsoft'un global ağında, **anycast**) istemciyi karşılayan, HTTP/HTTPS için **global giriş noktası**. İstemci en yakın POP'a bağlanır; oradan Microsoft omurgasıyla origin'e (backend'e) taşınır.

| Yetenek | Ne yapar? |
|---|---|
| **Global anycast + hızlandırma** | TCP/TLS el sıkışması istemciye yakın POP'ta; origin'e optimize yol — dinamik içerikte de gecikmeyi düşürür |
| **Global yük dengeleme + failover** | Origin'leri (farklı region'lar) health probe ile izler; sağlıksızı otomatik devreden çıkarır — **DNS TTL beklemez** (trafik Front Door'dan geçtiği için anında) |
| **CDN / önbellekleme** | Statik içeriği edge'de önbellekler |
| **WAF** | Edge'de saldırıyı durdurur (bölgesel WAF'tan **önce**) |
| **TLS, özel domain, yönlendirme kuralları** | Yönetilen sertifika, URL rewrite/redirect, rules engine |
| **Origin'ler** | App Service, Storage, Application Gateway, public IP, herhangi bir public host |

- **Katmanlar:** **Standard** ve **Premium**. Premium; yönetilen (managed) WAF kural setleri, bot koruması ve **Private Link** ile özel origin bağlantısı gibi ek güvenlik yetenekleri içerir *(farkları güncel tablodan doğrula)*. **Front Door (classic)** **31 Mart 2027**'de emekli olacak; yeni kurulumlar Standard/Premium'dur.
- **Global servistir:** region seçmezsin (→ C1.4).

### 3.4 Traffic Manager (DNS tabanlı, global) ⭐

Traffic Manager **trafiğin içinden geçmez**. İstemci `app.contoso.com` adını çözmek istediğinde Traffic Manager, **yönlendirme yöntemine** ve endpoint sağlığına göre uygun endpoint'in adresini **DNS cevabı** olarak döndürür; istemci sonra o endpoint'e **doğrudan** bağlanır. Her protokolle çalışır (DNS seviyesinde olduğu için HTTP olmak zorunda değil).

| Yöntem | Davranış | Tipik kullanım |
|---|---|---|
| **Priority** | Her zaman 1. öncelikli endpoint; sağlıksızsa 2.'ye | **Aktif/pasif failover** |
| **Weighted** | Ağırlıklara göre dağıt (ör. %90 / %10) | Canary, kademeli geçiş |
| **Performance** | İstemciye **gecikme olarak en yakın** endpoint | Çok region'lı kullanıcı deneyimi |
| **Geographic** | Kullanıcının **coğrafi konumuna** göre sabit endpoint | Veri egemenliği, bölgeye özel içerik |
| **Multivalue** | Birden çok sağlıklı IP'yi **tek cevapta** döndür | İstemci tarafı yeniden deneme |
| **Subnet** | İstemci **IP aralığına** göre endpoint | Kurumsal ağ / ISP'ye özel yönlendirme |

- **Endpoint türleri:** Azure endpoint'leri, **external** endpoint'ler (Azure dışı), **nested** (profil içinde profil).
- **Sağlık izleme:** HTTP/HTTPS/TCP probe'ları; sağlıksız endpoint cevaptan çıkar.
- **Sınırlar:** **DNS önbellekleme** (TTL) nedeniyle failover **saniyelerle değil, TTL + probe süresiyle** gerçekleşir; trafik Traffic Manager'dan geçmediği için **WAF, TLS sonlandırma, önbellek yoktur**.

**Traffic Manager ↔ Front Door** ⭐

| | Traffic Manager | Front Door |
|---|---|---|
| Mekanizma | **DNS** cevabı (yönlendirir, geçmez) | **Reverse proxy** — trafik içinden geçer |
| Protokol | Herhangi (DNS üzerinden) | HTTP/HTTPS |
| Failover hızı | DNS TTL'ye bağlı (dakikalar) | Anında (bağlantı Front Door'da) |
| WAF / CDN / TLS offload | **Yok** | **Var** |
| İstemci neyi görür? | Endpoint'in kendi IP'si | Front Door'un edge adresi |
| Maliyet | Çok düşük | Daha yüksek (özellikler) |
| Ne zaman? | HTTP-dışı protokol, basit DNS failover, ucuz çözüm | Global web uygulaması, hızlandırma, WAF |

### 3.5 Karar ağacı ve kombinasyon örüntüleri ⭐

```
Trafik HTTP/HTTPS mi?
 ├─ Hayır (TCP/UDP/diğer)
 │    ├─ Tek region'da dağıt?               → Azure Load Balancer (public veya internal)
 │    └─ Global/DNS failover?               → Traffic Manager
 └─ Evet (web trafiği)
      ├─ Global kullanıcı, hızlandırma/CDN/anında failover?   → Azure Front Door
      ├─ Tek region, URL/host routing, WAF, SSL offload?      → Application Gateway
      └─ DNS tabanlı basit yönlendirme/ucuz?                  → Traffic Manager
Yalnızca iç (internal) trafik mi?            → Internal Load Balancer / özel IP'li Application Gateway
```

**Kombinasyon örüntüleri** (çoğu gerçek mimari birden çok katmanı birleştirir):

| Örüntü | Akış | Neden? |
|---|---|---|
| **Front Door + Application Gateway + Load Balancer** | İnternet → **Front Door** (global, WAF, CDN) → **Application Gateway** (bölgesel L7) → **Load Balancer** (VM katmanı) | Global giriş + bölgesel L7 yönlendirme + VM katmanı dağıtımı |
| **Traffic Manager + Application Gateway** | DNS → Traffic Manager (performance/priority) → bölgesel Application Gateway'ler | HTTP-dışı veya mevcut DNS tabanlı çok region'lı mimari |
| **Application Gateway + Internal LB** | Web katmanı (L7) → uygulama katmanı (L4 internal) | 3 katmanlı uygulamada her katmanın kendi dağıtıcısı |
| **Front Door + App Service (çok region)** | Aynı uygulama iki region'da; origin'ler Front Door'da | Region arızasında otomatik geçiş |

- **Health probe mantığı:** her servis backend'e düzenli probe gönderir; **belirli sayıda ardışık başarısızlıkta** instance havuzdan çıkar, başarılı olunca geri girer. Uygulamada `/health` gibi **gerçek bağımlılıkları** (DB) kontrol eden bir endpoint, "port açık" kontrolünden daha doğru sinyaldir. Probe'u **WAF/NSG kuralları engellememeli** (`AzureLoadBalancer` service tag'i → C1.18).
- **Availability zone ile birlikte (→ C1.4):** bölgesel L4/L7 servisleri **zone-redundant** yapılandırılır; backend instance'ları farklı zone'lara yayılır — yük dengeleyici *zone* arızasına, Front Door/Traffic Manager *region* arızasına karşı korur.

### 3.6 Bağlam: Azure CDN, Bastion ve 2026 notları

- **Azure CDN:** coğrafi olarak dağıtık edge sunucularında **statik içeriği** (görsel, JS, video) önbelleğe alarak gecikmeyi ve origin yükünü azaltır. **2026 durumu:** Edgio (eski Verizon) tabanlı Azure CDN **2025 başında kapatıldı**; **Azure CDN Standard from Microsoft (classic)** yeni profil/domain'e kapalı, **30 Eylül 2027**'de emekli olacak — Microsoft'un yönlendirdiği yer **Azure Front Door Standard/Premium**'dur. *(tarihleri resmî sayfalardan doğrula)*
- **Azure Bastion:** yük dengeleme değil, **yönetim erişimi** servisidir (public IP'siz VM'e portaldan RDP/SSH) → [C1.18](../18-network-security-key-vault/README.md).
- **2026 yeniden markalama / emeklilik özeti** *(durum tarihleri değişebilir — "What's new / retirement" sayfalarını kontrol et)*:

| Ürün | Durum |
|---|---|
| Basic Load Balancer / Basic Public IP | Emekli (30 Eylül 2025) → Standard |
| Application Gateway V1 | Emekli (28 Nisan 2026) → V2 |
| Front Door (classic) | 31 Mart 2027'de emekli → Standard/Premium |
| Azure CDN Standard from Microsoft (classic) | 30 Eylül 2027'de emekli → Front Door |
| Azure CDN from Edgio | Kapatıldı (2025) → Front Door |

### 3.7 Pratik — Traffic Manager ile DNS tabanlı yönlendirme ve Standard Load Balancer

> Bütçe alarmın kurulu olsun (→ C1.1). **Traffic Manager**: profil ve endpoint başına çok küçük ücret + DNS sorgusu başına ücret — laboratuvar için sentim düzeyinde. **Standard Load Balancer + Standard public IP** saatlik ücretlenir (küçük, ama sürekli); VM kurmuyoruz, yalnızca kaynakların nasıl tanımlandığını görüyoruz — **hemen sil**. **Front Door, Application Gateway ve WAF kurmuyoruz**: Standard/Premium profil ve gateway'in sabit aylık/saatlik maliyeti yüksektir (WAF_v2 ve Premium özellikle); yalnızca teoride.

```bash
az login
RG=rg-az900-c117; LOC=westeurope
az group create --name $RG --location $LOC

# 1) Traffic Manager profile — Priority routing, short TTL so changes show quickly
DNSNAME=tm-az900-$RANDOM
az network traffic-manager profile create --resource-group $RG --name tm-demo \
  --routing-method Priority --unique-dns-name $DNSNAME --ttl 30 \
  --protocol HTTPS --port 443 --path "/" --status-code-ranges 200-399   # redirects count as healthy

# 2) Two external endpoints (stand-ins for "primary" and "secondary" regions)
az network traffic-manager endpoint create --resource-group $RG --profile-name tm-demo \
  --name ep-primary   --type externalEndpoints --target www.microsoft.com --priority 1
az network traffic-manager endpoint create --resource-group $RG --profile-name tm-demo \
  --name ep-secondary --type externalEndpoints --target learn.microsoft.com --priority 2

# 3) Which endpoint does DNS return? (Priority: always the primary while it is healthy)
nslookup $DNSNAME.trafficmanager.net

# 4) Simulate a primary failure: disable the primary endpoint, query again
az network traffic-manager endpoint update --resource-group $RG --profile-name tm-demo \
  --name ep-primary --type externalEndpoints --endpoint-status Disabled
sleep 40; nslookup $DNSNAME.trafficmanager.net        # the CNAME now points to the secondary target

# 5) Switch the routing method — no client change needed, only the DNS answer changes
az network traffic-manager profile update --resource-group $RG --name tm-demo --routing-method Weighted
az network traffic-manager endpoint update --resource-group $RG --profile-name tm-demo \
  --name ep-primary --type externalEndpoints --endpoint-status Enabled --weight 90
az network traffic-manager endpoint update --resource-group $RG --profile-name tm-demo \
  --name ep-secondary --type externalEndpoints --weight 10
for i in 1 2 3 4 5; do nslookup $DNSNAME.trafficmanager.net | grep -i "canonical"; sleep 31; done   # mostly primary, occasionally secondary

# 6) Standard Load Balancer definition: public IP, probe, rule (backend pool stays empty — no VMs, no traffic)
az network public-ip create --resource-group $RG --name pip-lb --sku Standard --allocation-method Static
az network lb create --resource-group $RG --name lb-demo --sku Standard \
  --public-ip-address pip-lb --frontend-ip-name fe-web --backend-pool-name be-web
az network lb probe create --resource-group $RG --lb-name lb-demo --name probe-http \
  --protocol Http --port 80 --path /health
az network lb rule create --resource-group $RG --lb-name lb-demo --name rule-http \
  --protocol Tcp --frontend-port 80 --backend-port 80 \
  --frontend-ip-name fe-web --backend-pool-name be-web --probe-name probe-http
az network lb show --resource-group $RG --name lb-demo \
  --query "{sku:sku.name, rules:loadBalancingRules[].name, probes:probes[].name, pools:backendAddressPools[].name}"
#   Real use: add VMs/NICs to be-web, and open port 80 in the NSG — Standard LB is closed by default.

# 7) Cleanup — mandatory (the Standard public IP and LB bill per hour)
az group delete --name $RG --yes --no-wait
```

<details><summary>Beklenen gözlemler ve açıklamaları</summary>

| Adım | Gözlem | Kavram |
|---|---|---|
| 3 | `nslookup` çıktısında `$DNSNAME.trafficmanager.net` bir **CNAME** ile `www.microsoft.com` (edge adresi zinciri) gösterir | Traffic Manager yalnızca **DNS cevabı** döndürür; trafiğin içinden geçmez |
| 4 | Primary devre dışı → CNAME `learn.microsoft.com`'a döner (TTL ~30 sn + birkaç sn) | Priority = failover; **DNS önbelleği** yüzünden anlık değil |
| 5 | Weighted'ta çoğu sorgu primary'yi, bazıları secondary'yi verir | Ağırlıklı dağıtım; yöntem değişince istemci değişmez |
| 6 | `sku: Standard`, kural/probe/havuz adları listelenir; backend yok | LB bileşenleri: frontend, backend pool, probe, kural |
| 6 | Probe yolu `/health` | Probe uygulama sağlığını yoklar (HTTP 200 bekler) |

`--status-code-ranges` parametresi eski CLI sürümlerinde farklı adlanabilir; hata alırsan `az network traffic-manager profile create --help` ile kontrol et. İlk sorguda yerel DNS önbelleği eski cevabı gösterebilir; birkaç saniye bekle. Hedef sitelere HTTPS probe gittiği için profilin `Online` görünmesi 1–2 dakika sürebilir.
</details>

---

## 4. Gerçek dünya senaryosu

Avrupa ve ABD'de müşterisi olan bir online bilet satış şirketi:

1. **Global giriş:** `tickets.contoso.com` → **Azure Front Door (Premium)**: POP'larda TLS sonlandırma, **WAF** (bot ve OWASP kuralları), statik içerik (afiş görselleri) edge'de önbellekleniyor.
2. **Origin'ler:** West Europe ve East US'te iki aynı uygulama yığını. Front Door, sağlık probe'larıyla gecikmeye göre en yakın sağlıklı bölgeye yönlendiriyor; bir bölge düşerse trafik **otomatik** diğerine geçiyor.
3. **Bölgesel katman:** Her bölgede **Application Gateway (WAF_v2, zone-redundant)** — `/api/*` API'ye, `/*` web'e; backend'e özel IP'den ulaşıldığı için origin'ler **private** (Front Door Premium Private Link).
4. **Uygulama katmanı:** Web → iç **Internal Load Balancer** (Standard, zone-redundant frontend) → App VM'leri; VM'ler 3 zone'a yayılı.
5. **Veritabanı:** her bölgede replikalı/geo-replicated veri katmanı (→ C1.16).
6. **HTTP-dışı servis:** Oyun tarzı bir UDP mesajlaşma servisi için **Standard Load Balancer** (L4) + **Traffic Manager (Performance)**.
7. **Maliyet notu:** Front Door Premium + WAF_v2 + iki bölge, sabit maliyeti yüksek bir mimaridir; "yalnızca bir bölge, HTTP, WAF yok" olan basit bir blog için **Application Gateway veya doğrudan App Service** yeterlidir.

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → servis eşleme; L4/L7 ve bölgesel/global ayrımı; Traffic Manager yöntemleri; "NOT" sorularında "hangisi HTTP içeriğini görmez?".

| Senaryodaki ifade | Cevap |
|---|---|
| "TCP/UDP trafiğini bölgesel olarak dağıt", "çok hızlı, katman 4" | **Azure Load Balancer** |
| "Backend VM'lere iç (internal) dağıtım, özel IP" | **Internal Load Balancer** |
| "URL path / host adına göre yönlendir" | **Application Gateway** (veya Front Door) |
| "SSL/TLS offload", "cookie affinity" | **Application Gateway** |
| "SQL injection / XSS'e karşı web uygulaması koruması" | **WAF** (Application Gateway veya Front Door üzerinde) |
| "Global HTTP uygulaması, hızlandırma, edge'de CDN + WAF" | **Azure Front Door** |
| "DNS tabanlı yönlendirme, her protokol, trafik içinden geçmesin" | **Traffic Manager** |
| "Aktif/pasif failover, DNS ile" | Traffic Manager — **Priority** |
| "Kullanıcıya en düşük gecikmeli endpoint" | Traffic Manager — **Performance** |
| "Kullanıcıyı konumuna göre belirli bölgeye (veri egemenliği)" | Traffic Manager — **Geographic** |
| "Trafiği %90/%10 böl (canary)" | Traffic Manager — **Weighted** |
| "Statik içeriği kullanıcıya yakın önbellekten sun" | **CDN** (günümüzde **Front Door**) |
| "Sağlıksız backend'i otomatik devreden çıkar" | **Health probe** (tüm servislerde) |

**Tuzaklar:**
- **Traffic Manager ↔ Front Door:** DNS (yönlendirir) ↔ reverse proxy (içinden geçer). "WAF/CDN/anında failover" gören Front Door'u seçer.
- **Application Gateway ↔ Load Balancer:** L7 (URL, WAF, TLS) ↔ L4 (TCP/UDP, sadece IP:port).
- "Load Balancer URL'ye göre yönlendirir" → **Hayır**.
- "Traffic Manager trafiği şifreler/WAF uygular" → **Hayır**.
- "Application Gateway global bir servistir" → **Hayır**, bölgeseldir.
- "Front Door bir region'a konuşlandırılır" → **Hayır**, global servistir.
- "Standard LB varsayılan olarak her inbound trafiğe izin verir" → **Hayır**, NSG ile izin gerekir.
- "Availability zone yük dengelemenin alternatifidir" → **Hayır**; zone'lar fiziksel yalıtım, LB trafik dağıtımıdır — birlikte kullanılır.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Web uygulaması için L4 Load Balancer seçip "WAF neden yok?" demek | L7 + WAF için Application Gateway/Front Door |
| Traffic Manager'dan anlık failover beklemek | DNS TTL + probe süresi kadar gecikir; anında failover için Front Door |
| Health probe'u yalnızca "port açık mı" ile yapmak | Uygulamayı ve bağımlılıkları (DB) doğrulayan `/health` endpoint'i |
| NSG'nin probe trafiğini (`AzureLoadBalancer`) engellemesi | Probe kaynak adresine izin ver; yoksa tüm backend "sağlıksız" görünür |
| Standard LB kurup NSG'de port açmayı unutmak | Standard varsayılan kapalıdır; NSG ile inbound izin ver |
| Tek instance'ı LB arkasına koyup "HA sağladım" demek | Backend'de ≥ 2 instance ve farklı zone'lar gerekir; tek backend'in arızası LB ile çözülmez |
| Front Door ve Application Gateway'de aynı kuralları gereksiz yere ikilemek | WAF'ı bilinçli konumlandır; çift WAF ek maliyet ve yanlış pozitif getirir |
| Basic LB/Basic public IP ile yeni mimari kurmak | Emekli; Standard SKU |
| ASP.NET Core arkasında `X-Forwarded-For/Proto` başlıklarını işlememek | `UseForwardedHeaders` (gerçek istemci IP'si, HTTPS şeması) → M12 |
| Laboratuvardan Standard public IP/LB'yi silmeyi unutmak | Saatlik ücretlenir; resource group'u sil |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Load Balancer (L4) | Çok düşük gecikme, yüksek throughput, ucuz | İçeriği görmez: WAF/URL routing/TLS offload yok |
| Application Gateway (L7) | Zengin yönlendirme, WAF, TLS offload | Bölgesel; sabit saatlik maliyet (WAF_v2 daha yüksek); autoscale ısınma süresi |
| Front Door | Global hızlandırma, edge WAF/CDN, anında failover | Daha yüksek maliyet; sadece HTTP/HTTPS; yapılandırma yayılması dakikalar alabilir |
| Traffic Manager | Ucuz, her protokol, basit | DNS TTL ile yavaş failover; WAF/önbellek yok |
| TLS offload (gateway'de) | Backend CPU'su rahatlar, sertifika tek yerde | Gateway–backend arası şifresiz kalabilir → hassas veri için end-to-end TLS |
| Çok katmanlı kombinasyon | Derinlikli koruma, her katmanda uzmanlık | Karmaşıklık, gecikme, çoklu maliyet kalemi |
| Zone-redundant frontend | Zone arızasına dayanıklılık | Bölgeler arası değil — region arızası için global katman şart |
| Çok region'lı aktif/aktif | Region arızası + yakın kullanıcı deneyimi | Veri katmanı karmaşıklığı (çoğaltma/tutarlılık), 2× maliyet |

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Load-balancing options in Azure](https://learn.microsoft.com/en-us/azure/architecture/guide/technology-choices/load-balancing-overview)
- [What is Azure Load Balancer?](https://learn.microsoft.com/en-us/azure/load-balancer/load-balancer-overview) · [Basic Load Balancer retirement](https://learn.microsoft.com/en-us/azure/load-balancer/load-balancer-basic-upgrade-guidance)
- [What is Azure Application Gateway?](https://learn.microsoft.com/en-us/azure/application-gateway/overview) · [Application Gateway V1 retirement](https://learn.microsoft.com/en-us/azure/application-gateway/v1-retirement)
- [What is Azure Front Door?](https://learn.microsoft.com/en-us/azure/frontdoor/front-door-overview) · [Front Door (classic) retirement](https://azure.microsoft.com/updates/azure-front-door-classic-will-be-retired-on-31-march-2027)
- [What is Traffic Manager?](https://learn.microsoft.com/en-us/azure/traffic-manager/traffic-manager-overview) · [Traffic routing methods](https://learn.microsoft.com/en-us/azure/traffic-manager/traffic-manager-routing-methods)
- [Azure CDN overview](https://learn.microsoft.com/en-us/azure/cdn/cdn-overview)
- [`az network traffic-manager`](https://learn.microsoft.com/en-us/cli/azure/network/traffic-manager) · [`az network lb`](https://learn.microsoft.com/en-us/cli/azure/network/lb)
