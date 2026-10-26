# C1.17 Yük Dengeleme ve Trafik Dağıtımı — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *genişletilmiş kapsam (bağlam)* · 16 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, eşleştirme, senaryo). Gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ %80** (≥ 13/16).

---

### 1. Tek seçim
Bir ekip, tek bir region'daki VM'ler arasında **TCP trafiğini** düşük gecikmeyle dağıtmak istiyor; HTTP başlıkları veya URL'lerle ilgilenmiyor. Hangi servis **en uygundur**?

- A) Azure Front Door
- B) Azure Application Gateway
- C) Azure Load Balancer
- D) Traffic Manager

<details><summary>Cevap</summary>

**C.** Load Balancer, bölgesel katman 4 (TCP/UDP) dağıtıcıdır. Application Gateway ve Front Door HTTP/HTTPS içindir; Traffic Manager DNS tabanlıdır.
</details>

### 2. Tek seçim
Bir web uygulaması için `/images/*` isteklerinin bir backend havuzuna, `/api/*` isteklerinin başka bir havuza gitmesi, ayrıca **SQL injection** koruması isteniyor. Tek bir **bölgesel** servis seçilecek. Hangisi?

- A) Standard Load Balancer
- B) Traffic Manager
- C) Application Gateway (WAF ile)
- D) Azure DNS

<details><summary>Cevap</summary>

**C.** URL path tabanlı yönlendirme ve WAF, bölgesel L7 servisi olan Application Gateway'in yetenekleridir.
</details>

### 3. Eşleştirme
Her servisi, **kapsamı ve katmanı** doğru anlatan ifadeyle eşleştirin: **Load Balancer · Application Gateway · Front Door · Traffic Manager**

1. Bölgesel, L4
2. Bölgesel, L7
3. Global, L7 (reverse proxy)
4. Global, DNS tabanlı

<details><summary>Cevap</summary>

**1 → Load Balancer · 2 → Application Gateway · 3 → Front Door · 4 → Traffic Manager.**
</details>

### 4. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Traffic Manager, istemci trafiğinin kendi üzerinden geçmesini sağlar. | ☐ | ☐ |
| b) Traffic Manager yalnızca HTTP/HTTPS uygulamalarla çalışır. | ☐ | ☐ |
| c) Traffic Manager, sağlıksız endpoint'i DNS yanıtından çıkarabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Hayır · c) Evet.** Traffic Manager yalnızca **DNS cevabı** üretir; istemci endpoint'e doğrudan bağlanır. DNS seviyesinde çalıştığı için protokolden bağımsızdır. Health probe ile sağlıksız endpoint cevaptan çıkar.
</details>

### 5. Tek seçim
Bir şirket, **aktif/pasif** mimaride ana region çökerse DNS ile **yedek region'a** yönlendirme yapmak istiyor. Traffic Manager'da hangi yöntem uygundur?

- A) Weighted
- B) Priority
- C) Geographic
- D) Multivalue

<details><summary>Cevap</summary>

**B.** Priority her zaman en yüksek öncelikli sağlıklı endpoint'i döndürür; o sağlıksızsa sıradakine geçer — klasik failover.
</details>

### 6. Eşleştirme
Her ihtiyacı Traffic Manager yöntemiyle eşleştirin: **Performance · Weighted · Geographic · Subnet**

1. Trafiğin %10'unu yeni sürümün olduğu endpoint'e gönder
2. İstemciye gecikme açısından en yakın endpoint'i ver
3. AB kullanıcılarını veri egemenliği nedeniyle yalnızca AB region'ına yönlendir
4. Belirli bir kurumsal IP aralığındaki istemcileri belirli bir endpoint'e gönder

<details><summary>Cevap</summary>

**1 → Weighted · 2 → Performance · 3 → Geographic · 4 → Subnet.**
</details>

### 7. Senaryo
Global kullanıcıları olan bir web uygulaması için istenenler: edge'de **WAF**, **statik içerik önbellekleme**, bir region düştüğünde **DNS TTL'ini beklemeden** otomatik geçiş. Hangi servis **en uygundur**?

- A) Traffic Manager
- B) Azure Load Balancer
- C) Azure Front Door
- D) Application Gateway tek başına

<details><summary>Cevap</summary>

**C.** Front Door global bir reverse proxy'dir: edge'de WAF, CDN ve health-probe tabanlı anında failover sağlar. Traffic Manager DNS TTL'ine bağlıdır ve WAF/önbellek yoktur; Application Gateway bölgeseldir.
</details>

### 8. Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi iki** yetenek Application Gateway'de bulunur?

- A) SSL/TLS termination (offload)
- B) URL path tabanlı yönlendirme
- C) DNS yanıtını coğrafi konuma göre değiştirme
- D) UDP trafiğini katman 4'te dağıtma

<details><summary>Cevap</summary>

**A ve B.** C Traffic Manager'ın (Geographic yöntemi), D Load Balancer'ın işidir.
</details>

### 9. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Front Door global bir servistir; bir region seçerek dağıtılmaz. | ☐ | ☐ |
| b) Application Gateway birden çok region'ı kapsayan global bir servistir. | ☐ | ☐ |
| c) WAF, Application Gateway ve Front Door'a eklenebilen bir özelliktir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Application Gateway bölgeseldir; Front Door globaldir.
</details>

### 10. Tek seçim
Bir **internal** (iç) uygulama katmanında, web katmanındaki VM'lerden gelen TCP isteklerini **özel IP** ile app VM'lerine dağıtmak gerekiyor. Ne kullanılır?

- A) Public Standard Load Balancer
- B) Internal Load Balancer
- C) Azure Front Door
- D) Traffic Manager

<details><summary>Cevap</summary>

**B.** Internal Load Balancer'ın frontend'i VNet içi özel IP'dir; public IP gerekmez.
</details>

### 11. Tek seçim
Bir Standard Load Balancer kuruldu, backend VM'ler sağlıklı, fakat internetten gelen 80 portu isteği backend'e **ulaşmıyor**. En olası neden nedir?

- A) Standard Load Balancer varsayılan olarak kapalıdır; backend subnet/NIC'te NSG ile 80'e izin verilmemiştir
- B) Standard SKU yalnızca UDP destekler
- C) Load Balancer yalnızca iç trafik içindir
- D) Availability zone etkin olduğu için trafik engellenir

<details><summary>Cevap</summary>

**A.** Standard LB "secure by default"tur: inbound trafik NSG ile açıkça izin verilmedikçe geçmez.
</details>

### 12. Tek seçim
Load Balancer'da **health probe**'un amacı nedir?

- A) Backend'e gelen trafiği şifrelemek
- B) Sağlıksız instance'ları tespit edip trafikten çıkarmak
- C) Backend'lerin maliyetini hesaplamak
- D) DNS kaydı oluşturmak

<details><summary>Cevap</summary>

**B.** Probe periyodik yoklama yapar; ardışık başarısızlıkta instance havuzdan çıkarılır, düzelince geri alınır.
</details>

### 13. Sıralama
Aşağıdaki mimaride bir isteğin izleyeceği yolu **internetten başlayarak** sıralayın: **Internal Load Balancer · Azure Front Door · App VM · Application Gateway**

<details><summary>Cevap</summary>

**Azure Front Door → Application Gateway → Internal Load Balancer → App VM.** Global L7 giriş, bölgesel L7 yönlendirme, iç L4 dağıtım, en sonda backend.
</details>

### 14. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Availability zone'lar, yük dengeleyicinin yerini alır. | ☐ | ☐ |
| b) Zone-redundant bir Load Balancer frontend'i, bir zone arızasında hizmetin sürmesine yardımcı olur. | ☐ | ☐ |
| c) Region arızasına karşı dayanıklılık için bölgesel yük dengeleyici tek başına yeterlidir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** Zone'lar fiziksel yalıtımdır, LB trafik dağıtır — birlikte kullanılır. Region arızası için global bir katman (Front Door/Traffic Manager) ve çok region'lı dağıtım gerekir.
</details>

### 15. Senaryo
Bir ekibin UDP tabanlı bir oyun sunucusu var; kullanıcıları gecikmeye göre en yakın region'a yönlendirmek istiyorlar. HTTP kullanılmıyor. Aşağıdakilerden **hangisi** doğru bir tasarımdır?

- A) Azure Front Door (Premium) + WAF
- B) Her region'da Standard Load Balancer + Traffic Manager (Performance yöntemi)
- C) Application Gateway + cookie affinity
- D) Yalnızca Azure DNS özel zone

<details><summary>Cevap</summary>

**B.** UDP için L4 gerekir (Load Balancer); Traffic Manager DNS düzeyinde protokolden bağımsızdır ve Performance yöntemiyle en düşük gecikmeli endpoint'i verir. Front Door ve Application Gateway HTTP/HTTPS içindir.
</details>

### 16. Tek seçim
Aşağıdaki ifadelerden hangisi **yanlıştır**?

- A) Front Door (classic), Standard/Premium'a taşınması gereken, emekliye ayrılacak bir üründür.
- B) Azure CDN statik içeriği kullanıcıya yakın edge sunucularında önbelleğe alır.
- C) Basic Load Balancer'ın SLA'sı, Standard Load Balancer ile aynıdır ve yeni projelerde tercih edilir.
- D) Application Gateway V1, V2'ye taşınması gereken emekli bir SKU'dur.

<details><summary>Cevap</summary>

**C.** Basic Load Balancer emekliye ayrılmıştır, SLA'sı yoktur ve yeni projelerde kullanılmaz; Standard'ın SLA'sı vardır. Emeklilik tarihlerini resmî duyurulardan doğrula.
</details>
