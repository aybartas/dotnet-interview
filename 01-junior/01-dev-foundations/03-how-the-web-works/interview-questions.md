# J1.3 Web Nasıl Çalışır — Mülakat Soruları

> Teori: [README.md](README.md) · Pratik: [src/](src/). ⭐ = çok sık sorulan.

### 1. ⭐ Tarayıcıya bir URL yazıp Enter'a basınca ne olur?
1. **URL ayrıştırılır** (scheme, host, port, path, query; fragment sunucuya gitmez). 2. Tarayıcı cache/HSTS kontrol edilir. 3. **DNS çözümleme:** tarayıcı → OS cache/hosts → yerel resolver → (root → TLD → authoritative) zinciriyle ad IP'ye çevrilir (TTL boyunca önbelleklenir). 4. **TCP bağlantısı:** three-way handshake (1 RTT). 5. **TLS handshake** (HTTPS): sertifika doğrulanır, oturum anahtarı türetilir (TLS 1.3'te 1 RTT), ALPN ile HTTP sürümü seçilir. 6. **HTTP isteği** gönderilir. 7. Sunucu tarafında genellikle CDN/load balancer → reverse proxy → uygulama → veritabanı işler. 8. **HTTP yanıtı** döner. 9. Tarayıcı HTML'i ayrıştırır; CSS/JS/görsel için aynı süreci (çoğunlukla mevcut bağlantıyı yeniden kullanarak) tekrarlar ve sayfayı render eder. Derinleştirmek için: her adımın maliyetini (RTT'ler) ve önbelleğini (DNS TTL, HTTP cache, keep-alive) söylerim.

### 2. ⭐ Hangi HTTP metodları idempotent? Idempotent ile safe arasındaki fark nedir?
**Safe:** sunucu durumunu değiştirmeyen metodlar (`GET`, `HEAD`, `OPTIONS`). **Idempotent:** aynı isteği bir veya N kez yapmanın sunucuda aynı sonuç durumunu bırakması: `GET`, `HEAD`, `OPTIONS`, **`PUT`**, **`DELETE`** idempotenttir; `POST` ve genelde `PATCH` değildir. Her safe metod idempotenttir, tersi değil (`PUT`, `DELETE` idempotent ama safe değil). Önemi: ağ hatasında idempotent bir isteği güvenle otomatik tekrar deneyebiliriz (retry); `POST` için tekrar çift kayıt/çift ödeme yaratabilir, bu yüzden **Idempotency-Key** kullanılır (→ M5.1). Not: `DELETE` ikinci çağrıda 404 dönebilir; yanıt farklı olsa da sunucu durumu (kayıt yok) aynıdır.

### 3. `PUT` ile `POST` arasındaki fark nedir? `PATCH` nerede durur?
`POST` genelde koleksiyona yeni kaynak ekler (`POST /orders`) ve sunucu ID'yi üretir; idempotent değildir. `PUT` kaynağın **tamamını** bilinen adreste (`PUT /orders/42`) değiştirir/oluşturur; idempotenttir. `PATCH` kaynağın yalnızca belirtilen alanlarını günceller; idempotent olması garanti değildir (örn. "sayacı 1 artır" PATCH'i idempotent değildir).

### 4. ⭐ `401 Unauthorized` ile `403 Forbidden` arasındaki fark?
**401:** kimlik doğrulanamadı (kimlik bilgisi yok ya da geçersiz/süresi dolmuş) — "sen kimsin?". **403:** kimlik biliniyor ama bu kaynak/işlem için yetkisi yok — "kimsin biliyorum, ama giremezsin". İsmi yanıltıcıdır: 401 aslında *authentication*, 403 *authorization* hatasıdır. (Kaynağın varlığını sızdırmamak için bazen yetkisiz kullanıcıya 403 yerine 404 dönülür.)

### 5. `301` ile `302`, `307` ve `308` farkı?
`301` kalıcı yönlendirme (tarayıcı/arama motorları önbelleğe alır), `302` geçici. Tarihsel olarak 301/302'de tarayıcılar POST'u GET'e çevirebilir; **`307` (geçici) ve `308` (kalıcı)** method ve body'yi **korur**. HTTP→HTTPS yönlendirmesi genelde 301/308, kısa süreli bakım yönlendirmesi 302/307'dir.

### 6. `502`, `503` ve `504` farkı nedir?
Hepsi genelde önde bir gateway/proxy/load balancer bulunduğunda görülür. **502 Bad Gateway:** proxy, arkadaki servisten geçersiz/bozuk cevap aldı (servis çöktü, bağlantı koptu). **503 Service Unavailable:** servis o an hizmet veremiyor (aşırı yük, bakım, hiç sağlıklı instance yok); `Retry-After` verilebilir. **504 Gateway Timeout:** arkadaki servis zamanında cevap vermedi. 4xx istemci hatasıdır ve tekrar denemek aynı sonucu verir; 5xx sunucu hatasıdır ve geçici olabilir.

### 7. HTTP stateless ne demek? Oturum nasıl sağlanır?
Her istek bağımsızdır; sunucunun önceki istekleri hatırlaması gerekmez. Oturum, durumu **her istekle birlikte taşıyarak** sağlanır: sunucu girişte `Set-Cookie: session=...` döner, tarayıcı sonraki isteklerde `Cookie` header'ıyla geri yollar, sunucu o ID ile kendi tarafındaki session store'dan (bellek/Redis/DB) kullanıcıyı bulur. Alternatif: durumu token'ın içinde taşıyan (JWT) sunucu-stateless yaklaşım (→ J11). Stateless tasarım yatay ölçeklemeyi kolaylaştırır; instance belleğinde tutulan oturum, birden çok instance'ta sorun çıkarır.

### 8. Cookie güvenlik bayrakları nelerdir?
`HttpOnly`: JavaScript okuyamaz (XSS'te çalınmayı zorlaştırır). `Secure`: yalnızca HTTPS ile gönderilir. `SameSite=Lax/Strict`: başka site kaynaklı isteklerde gönderimi kısıtlar (CSRF'i azaltır). `Max-Age/Expires`: ömür. `Domain/Path`: kapsam. Oturum cookie'si en az `HttpOnly; Secure; SameSite` olmalıdır.

### 9. HTTP/1.1, HTTP/2 ve HTTP/3 arasındaki fark nedir?
**HTTP/1.1:** düz metin, bağlantı başına tek eşzamanlı istek (tarayıcılar ~6 paralel bağlantı açar), HTTP seviyesinde head-of-line blocking. **HTTP/2:** binary framing, header sıkıştırma (HPACK), tek TCP bağlantısında **multiplexing**; ama TCP seviyesinde HOL blocking sürer (bir paket kaybı tüm akışları bekletir). **HTTP/3:** QUIC (UDP üstünde) kullanır; akışlar bağımsızdır, TLS 1.3 dahilidir, bağlantı kurulumu daha hızlıdır (0-RTT), bağlantı göçünü (Wi-Fi ↔ mobil) destekler. Sürüm TLS handshake'te ALPN ile pazarlıkla seçilir.

### 10. HTTPS neden zorunlu? TLS ne sağlar?
Düz HTTP'de ağdaki herkes trafiği okuyabilir/değiştirebilir. TLS üç şey sağlar: **gizlilik** (şifreleme), **bütünlük** (değiştirilemez), **kimlik doğrulama** (sertifika ile sunucunun gerçekten iddia ettiği alan adı olduğu). Ek olarak HTTP/2, `Secure` cookie, Service Worker gibi özellikler HTTPS ister.

### 11. TLS handshake nasıl çalışır? Maliyeti nedir?
İstemci `ClientHello` (desteklenen cipher'lar, anahtar payı, ALPN) gönderir; sunucu `ServerHello` + sertifika + kendi anahtar payıyla cevap verir; istemci sertifikayı doğrular, iki taraf asimetrik anahtar değişimiyle ortak bir **simetrik oturum anahtarı** türetir ve veri bu anahtarla (AES-GCM gibi) şifrelenir. TLS 1.3'te 1 RTT, 1.2'de 2 RTT. TCP handshake ile birlikte veri göndermeden önce ~2 RTT harcanır; **session resumption** ve bağlantı yeniden kullanımı bu maliyeti azaltır.

### 12. Sertifika zinciri (certificate chain) nedir? "Sertifika doğrulama hatası" neden olur?
Sunucu (leaf) sertifikası bir Intermediate CA, o da güvenilir bir Root CA tarafından imzalanır; istemci zinciri işletim sisteminin/tarayıcının güvenilir kök deposundaki bir köke kadar doğrular. Tipik hatalar: sertifikanın süresinin dolması, alan adının sertifikadaki CN/SAN ile uyuşmaması, zincirin eksik sunulması (intermediate yok), köke güvenilmemesi (self-signed). Geliştirmede `dotnet dev-certs https --trust` yerel sertifikayı güvenilir yapar; kodda doğrulamayı kapatmak (`ServerCertificateCustomValidationCallback => true`) production'a kaçarsa MITM'e kapı açar.

### 13. HSTS nedir?
`Strict-Transport-Security` yanıt header'ıdır: tarayıcıya, belirtilen süre (`max-age`) boyunca bu siteye **yalnızca HTTPS** ile bağlanmasını söyler. Böylece kullanıcı `http://` yazsa bile tarayıcı isteği kendisi HTTPS'e çevirir; ilk düz HTTP isteğindeki SSL stripping/downgrade saldırısı engellenir. ASP.NET Core'da `UseHsts()` (+ `UseHttpsRedirection()`).

### 14. ⭐ CORS nedir? Same-Origin Policy ile ilişkisi? Preflight neden var?
Origin = scheme + host + port. **SOP**, tarayıcıdaki JS'in başka bir origin'in cevabını okumasını varsayılan olarak engeller. **CORS**, sunucunun `Access-Control-Allow-*` header'larıyla belirli origin'lere bu kısıtı gevşetmesidir. Basit olmayan istekler (PUT/DELETE, `Content-Type: application/json`, özel header, `Authorization`) için tarayıcı önce otomatik bir **`OPTIONS` preflight** atar ("bu origin bu method ve header'larla isteyebilir mi?"); onay gelirse gerçek isteği yollar. Preflight, sunucudaki durumu değiştirebilecek isteğin izinsiz gönderilmesini önlemek için vardır; `Access-Control-Max-Age` ile önbelleklenir.

### 15. Postman/curl'de çalışan istek tarayıcıda neden CORS hatası verir?
CORS bir **tarayıcı** mekanizmasıdır. Postman ve curl SOP/CORS uygulamaz. Tarayıcı, yanıtta uygun `Access-Control-Allow-Origin` yoksa cevabı JS'e vermez (çoğu zaman istek sunucuya ulaşır ve işlenir, engeli tarayıcı uygular). Çözüm sunucuda doğru origin/method/header allow-list'idir. `AllowAnyOrigin` ile `AllowCredentials`'ı birlikte kullanmak güvenlik açığıdır.

### 16. `Cache-Control: no-cache` ile `no-store` farkı nedir? `ETag` nasıl çalışır?
`no-store`: yanıt hiçbir yerde saklanmaz. `no-cache`: saklanabilir ama **her kullanımda sunucuya doğrulatılmalıdır** (revalidation). `ETag` içerik parmak izidir; istemci sonraki istekte `If-None-Match: "<etag>"` gönderir; içerik değişmediyse sunucu gövdesiz `304 Not Modified` döner (bant genişliği tasarrufu).

### 17. Forward proxy ile reverse proxy, load balancer ve CDN farkı nedir?
**Forward proxy** istemci adına internete çıkar (filtre, cache, gizlilik). **Reverse proxy** sunucu adına istemcileri karşılar (TLS sonlandırma, sıkıştırma, cache, routing — NGINX, YARP). **Load balancer** istekleri birden çok instance'a dağıtır ve sağlık kontrolü yapar (L4/L7). **CDN** statik içeriği coğrafi olarak kullanıcıya yakın edge sunucularından verir. Pratikte aynı ürün (NGINX, Envoy, bulut gateway'leri) birden çok rolü üstlenebilir. Proxy arkasında gerçek istemci IP'si için `X-Forwarded-For` kullanılır (yalnızca güvenilir proxy'lerden) — ASP.NET Core'da `ForwardedHeaders` (→ M3.7).

### 18. DNS nasıl çalışır? TTL nedir? A, CNAME ve MX kayıtları ne işe yarar?
DNS, alan adını IP'ye çevirir; sorgu önbellek zincirinden (tarayıcı, OS, yerel resolver) geçer, bulunamazsa root → TLD → authoritative name server hiyerarşisiyle çözülür. **TTL**, cevabın ne kadar önbellekte tutulacağıdır; bu yüzden DNS değişikliği anında her yere yansımaz. `A/AAAA`: ad → IPv4/IPv6, `CNAME`: ad → başka ad, `MX`: mail sunucusu, `TXT`: serbest metin (SPF, doğrulama), `NS`: yetkili name server'lar.

### 19. Bir sayfa ilk açılışta yavaş. Gecikmeyi nasıl parçalara ayırırsın?
`curl -w` ile aşamaları ölçerim: `time_namelookup` (DNS), `time_connect` (TCP), `time_appconnect` (TLS), `time_starttransfer` (ilk bayt — sunucu işleme süresi dahil), `time_total`. DNS/TCP/TLS yüksekse sorun mesafe/bağlantı kurulumu (CDN, TLS 1.3, HTTP/2–3, keep-alive); TCP+TLS düşük ama ilk bayt yüksekse sorun sunucu işleme süresindedir (DB, kod). Tarayıcıda DevTools → Network → Timing sekmesi aynı bilgiyi verir.

### 20. `GET` isteğinde body gönderilir mi? Query string'e ne konur, ne konmaz?
Teknik olarak GET'te body yasak değildir ama anlamı tanımsızdır; proxy/cache/sunucuların bir kısmı yok sayar veya reddeder — kullanılmaz. Query string filtre, sayfalama, sıralama gibi kaynağı tanımlayan parametreler içindir. Parola/token gibi **gizli veriler query string'e konmaz**: URL'ler sunucu/proxy loglarında, tarayıcı geçmişinde ve `Referer` header'ında görünür.
