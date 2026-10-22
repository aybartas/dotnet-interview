# J1.3 Web Nasıl Çalışır: HTTP, DNS, TLS

> 📁 `WebApp/01-junior/01-dev-foundations/03-how-the-web-works/` · Önkoşul: J1.2 · Sorular: [`interview-questions.md`](interview-questions.md)

## İçindekiler

1. [Ne?](#1-ne)
2. [Neden?](#2-neden)
3. [Nasıl?](#3-nasıl)
4. [Gerçek dünya senaryosu](#4-gerçek-dünya-senaryosu)
5. [Mülakat perspektifi](#5-mülakat-perspektifi)
6. [Yaygın hatalar](#6-yaygın-hatalar)
7. [Performans & trade-off](#7-performans--trade-off)
8. [Kaynaklar](#8-kaynaklar)

---

## 1. Ne?

Web, **istemci–sunucu** modeline dayanır: istemci (tarayıcı, mobil uygulama, başka bir servis) bir **istek (request)** gönderir, sunucu bir **yanıt (response)** döner. Bu konuşmanın dili **HTTP**'dir; adres çözümlemesini **DNS**, güvenli taşımayı **TLS**, iletimi ise **TCP** (J1.2) yapar.

```
Application (L7) HTTP request/response
                TLS   ← HTTPS = HTTP + TLS
Transport (L4)   TCP  (QUIC/UDP in HTTP/3)
Network (L3)     IP
                DNS: "api.example.com" → 93.184.216.34
```

HTTP'nin iki temel özelliği:
- **Request–response:** İstemci başlatır, sunucu cevap verir (sunucu kendiliğinden itemez — bunun için WebSocket/SSE/SignalR vardır, → M5).
- **Stateless (durumsuz):** Her istek kendi içinde tamdır; sunucu önceki isteği "hatırlamak zorunda değildir". Durum (oturum, kullanıcı) cookie/token ile **her istekte** taşınır.

---

## 2. Neden?

- Web API yazan her geliştirici günde onlarca kez status code, header, cookie, CORS ve TLS ile uğraşır. "401 mi 403 mü dönmeliyim?", "CORS hatası neden sadece tarayıcıda?", "PUT mı POST mu?" soruları bu bilgiye dayanır.
- **Yavaşlığın ve hataların yeri:** İsteğin yolculuğunu (DNS → TCP → TLS → HTTP) bilmek, gecikmeyi parçalara ayırıp teşhis etmenizi sağlar.
- **Mülakat:** "Tarayıcıya URL yazınca ne olur?" backend/full-stack mülakatlarının en klasik sorusudur; cevap derinliğiniz sizi hızlıca konumlandırır.
- Sonraki modüller (J6 ASP.NET Core, J7 Web API, J11 güvenlik) bu zemin üstüne inşa edilir.

---

## 3. Nasıl?

### 3.1 "Tarayıcıya URL yazınca ne olur?" ⭐

`https://www.example.com:443/products?id=5#reviews` yazıldığında sırasıyla:

```
1. Parse the URL        scheme=https  host=www.example.com  port=443  path=/products  query=?id=5  fragment=#reviews
                         (the fragment is NOT sent to the server; it stays in the browser)
2. Cache check          Browser cache / HSTS list / service worker
3. DNS resolution       www.example.com → 93.184.216.34
4. TCP connection       SYN → SYN-ACK → ACK                      (1 RTT)   (→ J1.2)
5. TLS handshake        certificate validation, key exchange     (TLS 1.3: 1 RTT)
6. HTTP request         GET /products?id=5 HTTP/1.1  + headers
7. Server processes     (load balancer → reverse proxy → application → DB)
8. HTTP response        200 OK + headers + HTML
9. Browser renders      parses the HTML, repeats 3-8 for CSS/JS/images (mostly reusing the existing connection)
```

**URL anatomisi:**

```
  https://user@www.example.com:8443/a/b?x=1&y=2#top
  └─┬──┘ └──┬─┘└──────┬──────┘└─┬─┘└─┬─┘└───┬───┘└┬─┘
  scheme userinfo    host     port path  query fragment
```

#### DNS çözümleme

İnsan-okunur ad → IP adresi. Çözümleme **zincirleme ve önbellekli** yürür:

```
browser cache → OS cache / hosts file → local DNS resolver (ISP/company/8.8.8.8)
   → root (.) → TLD (.com) → authoritative name server (example.com's own DNS) → answer
```

| Kayıt tipi | Anlamı |
|------------|--------|
| `A` / `AAAA` | Ad → IPv4 / IPv6 |
| `CNAME` | Ad → başka bir ad (takma ad) |
| `MX` | Mail sunucusu |
| `TXT` | Serbest metin (doğrulama, SPF…) |
| `NS` | O alanın name server'ları |

- **TTL:** Cevabın ne kadar süre önbellekte tutulacağı. DNS değişikliği (örn. yeni IP'ye geçiş) TTL dolana kadar **herkese anında yansımaz** — "DNS propagation" denen bekleme budur.
- UDP/53 ile çalışır (büyük cevaplarda TCP).
- Servis taşıma sırasında TTL'i önceden düşürmek klasik bir operasyon pratiğidir.

> Yavaş ilk istek mi? `curl -w` ile aşamaları ayırın: `time_namelookup` (DNS) → `time_connect` (TCP) → `time_appconnect` (TLS) → `time_starttransfer` (ilk bayt). Komut 3.11'de (9. örnek).

### 3.2 HTTP anatomisi

HTTP/1.1 düz metin bir protokoldür:

```http
POST /orders?source=web HTTP/1.1          ← request line: METHOD  PATH+QUERY  VERSION
Host: api.example.com                     ← headers: Name: Value
Content-Type: application/json
Authorization: Bearer eyJhbGciOi...
Content-Length: 29
                                          ← BLANK LINE: end of the headers
{"product":"book","quantity":2}           ← body (optional)
```

```http
HTTP/1.1 201 Created                      ← status line: VERSION  CODE  REASON
Content-Type: application/json
Location: /orders/42
                                          
{"id":42,"status":"created"}
```

Bu klasördeki Kestrel uygulaması (3.11) gelen isteği tam bu biçimde konsola yazar ve geri döner — kendi `curl`'ünüzle ne gönderdiğinizi sunucunun gözünden görürsünüz.

**HTTP/2 ve /3'te** mesaj binary çerçevelere (frame) dönüşür, header'lar sıkıştırılır (HPACK/QPACK), ama **mantıksal model aynıdır** (method, path, header, body, status). Wireshark'ta okunabilir metin görmezsiniz; araçlar (curl `-v`, tarayıcı DevTools) sizin için metne çevirir.

### 3.3 Method semantiği: safe ve idempotent ⭐

| Method | Amaç | Safe | Idempotent | Body |
|--------|------|:----:|:----------:|:----:|
| `GET` | Kaynağı oku | ✅ | ✅ | — |
| `HEAD` | GET'in sadece header'ları | ✅ | ✅ | — |
| `OPTIONS` | Desteklenen method'lar / CORS preflight | ✅ | ✅ | — |
| `PUT` | Kaynağı **tamamen değiştir** (veya o ID'yle oluştur) | ❌ | ✅ | ✓ |
| `DELETE` | Kaynağı sil | ❌ | ✅ | — |
| `POST` | Yeni kaynak oluştur / işlem tetikle | ❌ | ❌ | ✓ |
| `PATCH` | Kısmi güncelleme | ❌ | ❌ (genelde) | ✓ |

- **Safe:** Sunucu durumunu **değiştirmez** (okuma). Crawler, prefetch, tarayıcı geri tuşu GET'i güvenle tekrarlar → **GET ile silme/güncelleme yapmayın.**
- **Idempotent:** Aynı isteği 1 kez ya da N kez yapmak **sunucudaki sonuç durumu aynı** bırakır. Ağ hatasında istemcinin **otomatik yeniden deneme (retry)** yapabilmesinin temelidir: `PUT /counter/42`'yi 3 kez göndermek = 1 kez göndermek. `POST /counter/increment`'i 3 kez göndermek = 3 sipariş/3 artış. (Denemek için: [`requests.http`](requests.http) 5. ve 5b.)
  - `DELETE` idempotenttir: ilk çağrı 204, ikincisi 404 dönebilir — *yanıt* farklı olsa da sunucunun *durumu* (kayıt yok) aynıdır.
  - Aşağıdaki sorunun cevabı: *"Ödeme POST'u zaman aşımına uğradı, tekrar deneyeyim mi?"* — Hayır, çift ödeme riski; çözüm **Idempotency-Key** header'ıdır (→ M5.1).

### 3.4 Status code aileleri

| Aile | Anlam | Sık görülenler |
|------|-------|----------------|
| **1xx** Bilgi | Devam et | `101 Switching Protocols` (WebSocket) |
| **2xx** Başarı | | `200 OK`, `201 Created` (+`Location`), `202 Accepted` (asenkron işlenecek), `204 No Content` |
| **3xx** Yönlendirme | İstemci başka yere gitmeli | `301 Moved Permanently`, `302 Found`, `304 Not Modified` (cache geçerli), `307/308` (method korunarak yönlendirme) |
| **4xx** İstemci hatası | İstek hatalı, **tekrar denemek aynı sonucu verir** | `400 Bad Request`, `401 Unauthorized` (kimlik YOK/geçersiz), `403 Forbidden` (kimlik var, YETKİ yok), `404 Not Found`, `409 Conflict`, `422 Unprocessable Entity`, `429 Too Many Requests` |
| **5xx** Sunucu hatası | Sorun sunucuda, **geçici olabilir (retry mantıklı)** | `500 Internal Server Error`, `502 Bad Gateway` (upstream geçersiz cevap), `503 Service Unavailable`, `504 Gateway Timeout` (upstream cevap vermedi) |

- **401 vs 403** ⭐: 401 "sen kimsin?" (kimlik doğrulama yok/başarısız), 403 "kimsin biliyorum, ama bunu yapamazsın" (yetkilendirme).
- **502/503/504 üçlüsü** genellikle önünde proxy/load balancer olan sistemlerde görülür: 502 = arkadaki servis bozuk cevap verdi, 503 = servis hizmet vermiyor (aşırı yük/bakım), 504 = arkadaki servis zamanında cevaplamadı.
- İstemci hatasını 5xx, sunucu hatasını 4xx dönmek monitörlemeyi ve retry davranışını bozar.

### 3.5 Önemli header'lar

| Header | Yön | Ne yapar? |
|--------|-----|-----------|
| `Host` | istek | Hangi site (aynı IP'de çok site → **virtual hosting**). HTTP/1.1'de zorunlu |
| `Content-Type` | ikisi | Body'nin biçimi: `application/json`, `text/html`, `multipart/form-data` |
| `Accept` | istek | İstemcinin kabul ettiği biçimler (**content negotiation**) |
| `Authorization` | istek | Kimlik bilgisi: `Bearer <token>`, `Basic …` |
| `Cache-Control` | ikisi | Önbellek kuralı: `max-age=3600`, `no-store` (hiç saklama), `no-cache` (**saklayabilirsin ama kullanmadan önce sunucuya sor**), `private`/`public` |
| `ETag` / `If-None-Match` | yanıt / istek | İçerik parmak izi → değişmediyse `304 Not Modified`, body gönderilmez |
| `Location` | yanıt | 201 sonrası yeni kaynağın adresi / 3xx hedefi |
| `Set-Cookie` / `Cookie` | yanıt / istek | Cookie yazma / geri gönderme |
| `User-Agent` | istek | İstemci kimliği |

**Conditional GET / ETag akışı** (uygulamada `/cache/etag`):

```
1. GET /cache/etag                     →  200 OK, ETag: "abc", body
2. GET /cache/etag  If-None-Match: "abc" →  304 Not Modified (NO body → saves bandwidth)
```

### 3.6 Cookie, session ve stateless HTTP

HTTP durumsuz olduğu için "oturum" şöyle kurulur:

```
1. Login:   POST /login  →  200 + Set-Cookie: session=abc123; HttpOnly; Secure; SameSite=Lax
2. Later:   GET /orders  →  Cookie: session=abc123       (the browser adds it automatically)
3. Server:  abc123 → finds the user record (server-side session store: memory / Redis / DB)
```

| Cookie özelliği | Koruma |
|-----------------|--------|
| `HttpOnly` | JavaScript okuyamaz → XSS ile çalınmasını zorlaştırır |
| `Secure` | Sadece HTTPS ile gönderilir |
| `SameSite=Lax/Strict` | Başka site kaynaklı isteklerde gönderilmez → CSRF'i azaltır |
| `Max-Age` / `Expires` | Ömür; yoksa tarayıcı kapanınca silinir (session cookie) |
| `Domain` / `Path` | Hangi adres/yollar için gönderileceği |

- **Cookie tabanlı session** (durum sunucuda) vs **token tabanlı (JWT)** (durum token'ın içinde, sunucu stateless) farkı → J11.
- Sunucu durumu bellekte tutarsa birden çok instance arkasında "oturum başka sunucuda" sorunu çıkar → merkezi session store ya da stateless token (→ M8, J11).

### 3.7 HTTP/1.1 vs HTTP/2 vs HTTP/3 — kavramsal fark

| | HTTP/1.1 | HTTP/2 | HTTP/3 |
|---|----------|--------|--------|
| Biçim | Düz metin | Binary çerçeve, header sıkıştırma (HPACK) | Binary, QPACK |
| Taşıma | TCP | TCP (+TLS fiilen zorunlu) | **QUIC (UDP üstünde)**, TLS 1.3 dahili |
| Eşzamanlı istek | Bağlantı başına 1 (tarayıcılar ~6 paralel bağlantı açar) | **Multiplexing:** tek bağlantıda çok akış | Multiplexing, akışlar birbirinden bağımsız |
| HOL blocking | HTTP seviyesinde | Çözüldü (HTTP), ama **TCP seviyesinde kaldı** | **Çözüldü** — kayıp yalnız o akışı etkiler |
| Bağlantı kurulumu | TCP (1 RTT) + TLS (1–2) | Aynı | QUIC: 1 RTT, tekrar bağlanırken **0-RTT** |

Sürüm tarayıcıyla sunucu arasında TLS handshake'inde **ALPN** ile pazarlıkla belirlenir (`h2`, `http/1.1`). Uygulama bunu gösterir: `dotnet run -- --https` ve `curl -v` çıktısında `ALPN: server accepted h2`. Protokol derinliği → M5.11.

### 3.8 TLS: sertifika, zincir, HTTPS, HSTS

**Neden HTTPS?** Düz HTTP'de aradaki herkes (Wi-Fi, ISS, proxy) isteği **okuyabilir ve değiştirebilir**. TLS üç garanti verir: **gizlilik** (şifreleme), **bütünlük** (değiştirilemez), **kimlik doğrulama** (gerçekten example.com ile konuşuyorsun). Artık tarayıcılar HTTP sitelerini "güvenli değil" işaretler; HTTP/2 fiilen TLS ister; cookie `Secure`, Service Worker, geolocation gibi API'ler HTTPS ister. **HTTPS isteğe bağlı değil, varsayılandır.**

**TLS 1.3 handshake (özet):**

```
Client ──ClientHello (supported ciphers, key share, ALPN)──────────────────▶ Server
Client ◀──ServerHello + CERTIFICATE + signature + key share ────────────────  Server
   (the client validates the certificate, both sides derive the shared session key)
Client ──Finished──▶  ◀──Finished──     ← from here on data is ENCRYPTED (symmetric key)
```

- Asimetrik şifreleme (sertifika/anahtar değişimi) yavaştır ama sadece handshake'te; veri akışı hızlı simetrik şifreyle (AES-GCM) korunur.
- **Maliyet:** TCP (1 RTT) + TLS 1.3 (1 RTT) = veri göndermeden önce ~2 RTT. TLS 1.2'de 2 RTT (toplam 3). Bu yüzden bağlantı yeniden kullanımı ve **TLS session resumption** önemlidir.

**Sertifika ve zincir:**
- Sertifika: "bu açık anahtar `www.example.com`'a ait" beyanı; bir **Certificate Authority (CA)** tarafından imzalanır.
- **Zincir:** `Server (leaf) certificate` ← imzalayan `Intermediate CA` ← imzalayan `Root CA`. Root CA'lar işletim sistemi/tarayıcının **güvenilir kök deposunda** bulunur; istemci zinciri köke kadar doğrular.
- Doğrulama hataları: süresi dolmuş, host adı sertifikayla uyuşmuyor (`CN`/`SAN`), zincir eksik (intermediate sunulmamış), güvenilmeyen kök (kendi imzalı = self-signed).
- Geliştirme: `dotnet dev-certs https --trust` yerel self-signed sertifika üretip güvenilir yapar. Production'da Let's Encrypt, kurumsal CA veya bulut yönetimli sertifika.
- **HSTS** (`Strict-Transport-Security: max-age=31536000; includeSubDomains`): Tarayıcıya "bu siteye bundan sonra **sadece HTTPS** ile gel" der; HTTP→HTTPS yönlendirme sırasındaki ilk düz HTTP isteğini ve downgrade saldırısını (SSL stripping) ortadan kaldırır. Uygulama HTTPS yanıtlarına ekler. ASP.NET Core'da `app.UseHsts()` + `UseHttpsRedirection()` (→ J6, J11.4).

### 3.9 Same-origin policy ve CORS

**Origin** = `scheme + host + port`. `https://app.example.com` ile `https://api.example.com` (farklı host) veya `http://localhost:3000` ile `http://localhost:5080` (farklı port) **farklı origin**'dir.

**Same-Origin Policy (SOP):** Tarayıcı, bir origin'deki sayfadaki JavaScript'in *başka bir origin'in cevabını okumasını* varsayılan olarak engeller (yoksa kötü bir site, açık bankacılık oturumunuzla bankaya istek atıp cevabı okurdu).

**CORS**, sunucunun *"şu origin'lerin bana erişmesine izin veriyorum"* demesinin yoludur — SOP'u **gevşetir**, güvenliği "ortadan kaldırmaz".

**Basit istek** (GET/POST + basit header'lar): tarayıcı isteği gönderir, yanıtta `Access-Control-Allow-Origin` yoksa cevabı JS'e **vermez**.

**Preflight** (PUT/DELETE, `Content-Type: application/json`, özel header, `Authorization`…): tarayıcı önce otomatik bir **`OPTIONS`** isteği atar:

```http
OPTIONS /cors/data HTTP/1.1
Origin: http://localhost:3000
Access-Control-Request-Method: PUT
Access-Control-Request-Headers: content-type
```
```http
HTTP/1.1 204 No Content
Access-Control-Allow-Origin: http://localhost:3000
Access-Control-Allow-Methods: GET,POST,PUT
Access-Control-Allow-Headers: Content-Type,X-Demo
Access-Control-Max-Age: 600            ← preflight result cached for 10 min (not repeated on every request)
```
Preflight onaylanırsa tarayıcı gerçek isteği gönderir.

> **Kritik nokta:** CORS bir **tarayıcı** korumasıdır. `curl`, Postman, sunucudan-sunucuya çağrı CORS'a takılmaz. "Postman'de çalışıyor ama tarayıcıda CORS hatası alıyorum" cümlesi bu yüzden klasiktir. İstek çoğu zaman sunucuya ulaşır ve işlenir; **engeli tarayıcı uygular** (cevabı JS'e vermez). Bu yüzden `AllowAnyOrigin` + `AllowCredentials` gibi gevşek ayar bir güvenlik açığıdır. Denemek için [`requests.http`](requests.http) 8. adım.

### 3.10 Proxy, reverse proxy, load balancer, CDN

| Bileşen | Kimin adına? | Ne yapar? |
|---------|--------------|-----------|
| **(Forward) Proxy** | **İstemci** adına | İstemciyi internete çıkarır: filtreleme, önbellek, kimlik gizleme (kurumsal proxy, VPN) |
| **Reverse Proxy** | **Sunucu** adına | İstemcinin karşısındaki "ön kapı": TLS sonlandırma, sıkıştırma, cache, rate limit, routing. Örn. NGINX, YARP, Envoy. Kestrel genelde bir reverse proxy arkasında çalışır |
| **Load Balancer** | Sunucu grubu adına | İstekleri birden çok instance'a dağıtır (round-robin, least-connections), sağlık kontrolü yapar (L4/L7 farkı → J1.2, S6.5) |
| **CDN** | İçerik sahibi adına | Statik içeriği (görsel, JS, CSS, video) kullanıcıya **coğrafi olarak yakın** edge sunucularından verir → gecikme ↓, origin yükü ↓ |
| **API Gateway** | API'ler adına | Reverse proxy + auth, rate limit, versiyonlama, aggregation (→ S6) |

Proxy arkasındayken sunucu **gerçek istemci IP'sini** doğrudan göremez; `X-Forwarded-For` / `X-Forwarded-Proto` header'larına bakılır (ASP.NET Core'da `ForwardedHeaders` middleware — yalnızca **güvenilir proxy'lerden** gelen header'a güvenin, aksi halde sahte IP/şema enjekte edilir, → M3.7).

### 3.11 Pratik — Kestrel ile ham isteği görmek

Uygulama `WebApp` projesindedir (`WebApp` klasöründen çalıştırın; `Program.cs` bu konunun `AddServices` ve `Map` metotlarını çağırır):

```bash
dotnet run                       # http://localhost:5080
dotnet run -- --https            # also https://localhost:5443 (first: dotnet dev-certs https --trust)
```

| Dosya | Ne için? |
|-------|----------|
| [`RawRequestLoggingMiddleware.cs`](RawRequestLoggingMiddleware.cs) | Gelen isteği ham metne en yakın biçimde loglar |
| [`HowTheWebWorksTopic.cs`](HowTheWebWorksTopic.cs) | `MapFallback` (her şeyi yansıt), status code, redirect, idempotency, cookie, ETag/304, CORS, HSTS uç noktaları |
| [`requests.http`](requests.http) | Aynı senaryolar için `.http` dosyası (VS Code REST Client, Rider, Visual Studio 2022+) |

Uygulama çalışırken ikinci terminalden `curl` ile inceleyin (`-k`: güvenilmeyen geliştirme sertifikasını atla — **sadece yerel deney**, prod'da asla):

```bash
curl -v http://localhost:5080/hello -o /dev/null            # '>' request, '<' response headers; '*' connection info
curl -I http://localhost:5080/hello                         # HEAD: GET without the body (safe + idempotent)
curl -kv https://localhost:5443/hello -o /dev/null 2>&1 | grep -E 'ALPN|SSL connection|TLSv|subject:|issuer:|using HTTP'
curl -k --http1.1 -sI https://localhost:5443/hello | head -1   # comparing HTTP/1.1 and HTTP/2 (ALPN negotiation)
curl -k --http2   -sI https://localhost:5443/hello | head -1
curl -k -sI https://localhost:5443/hello | grep -i strict-transport   # HSTS only on the HTTPS response

curl -si http://localhost:5080/redirect | grep -E '^HTTP|^[Ll]ocation'   # WITHOUT following the redirect
curl -sL -w '(last status: %{http_code}, redirects: %{num_redirects})\n' http://localhost:5080/redirect   # with -L

curl -c jar.txt http://localhost:5080/cookie/set; echo      # -c writes, -b sends
curl -b jar.txt http://localhost:5080/cookie/read; echo     # with the jar: the server recognizes you
curl http://localhost:5080/cookie/read; echo                # without the jar: HTTP is stateless

ETAG=$(curl -s -D - -o /dev/null http://localhost:5080/cache/etag | grep -i '^etag' | awk '{print $2}' | tr -d '\r')
curl -si -H "If-None-Match: $ETAG" http://localhost:5080/cache/etag | head -1   # 304 Not Modified

curl -k -o /dev/null -w 'dns: %{time_namelookup}s | tcp: %{time_connect}s | tls: %{time_appconnect}s | first byte: %{time_starttransfer}s | total: %{time_total}s | %{http_version}\n' https://localhost:5443/hello
```

---

## 4. Gerçek dünya senaryosu

**"Frontend ekibi: API'ye istek atınca konsolda CORS hatası, ama Postman'de çalışıyor."**

1. Frontend `http://localhost:3000`'de, API `http://localhost:5080`'de → farklı port = **farklı origin**.
2. `fetch(..., { method: 'PUT', headers: { 'Content-Type': 'application/json' } })` → tarayıcı önce **preflight (`OPTIONS`)** atar.
3. API'de CORS politikası yok (ya da `OPTIONS`'ı reddediyor) → preflight yanıtında `Access-Control-Allow-Origin` yok → tarayıcı gerçek isteği hiç göndermez, konsola hatayı yazar.
4. Postman tarayıcı değildir → SOP/CORS uygulamaz → "çalışıyor".
5. Çözüm: API'de **sadece ihtiyaç duyulan origin/method/header'a** izin veren CORS politikası. `AllowAnyOrigin` + `AllowCredentials` kombinasyonu yapmak yerine allow-list.

**Ek senaryo — "Canlıda ilk sayfa açılışı yavaş, sonrası hızlı":** `curl -w` ile DNS 5 ms, TCP 150 ms, TLS 300 ms, ilk bayt 900 ms → mesafe (RTT=150 ms) + TLS handshake ağır basıyor. Çözüm: CDN/edge yakınlığı, TLS 1.3, HTTP/2–3, bağlantı yeniden kullanımı, sunucu işleme süresine ayrı bakılması.

---

## 5. Mülakat perspektifi

Sık sorulanlar ([`interview-questions.md`](interview-questions.md)'de cevaplarıyla):
- **Tarayıcıya URL yazınca neler olur?** ⭐ (DNS → TCP → TLS → HTTP → render)
- **Hangi HTTP metodları idempotent? Safe ile farkı?** ⭐
- 401 vs 403, 502 vs 504, 301 vs 302 vs 307
- HTTP/1.1, HTTP/2, HTTP/3 farkı
- HTTPS nasıl çalışır; sertifika zinciri nedir?
- CORS nedir, preflight neden var, neden Postman'de sorun çıkmıyor?
- Stateless ne demek; oturum nasıl sağlanır?
- Reverse proxy ile load balancer ve CDN farkı

**İpucu:** "URL yazınca ne olur"u 30 saniyelik özet + istenirse derinleşme şeklinde hazırlayın. Her adımın **maliyetini** (RTT'ler) ve **önbelleğini** (DNS TTL, HTTP cache, bağlantı yeniden kullanımı) söylemek sizi öne çıkarır.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| GET ile durum değiştirmek (`GET /delete?id=5`) | GET safe olmalı; crawler/prefetch yanlışlıkla siler. `DELETE` kullanın |
| Her hatada `200 OK` + gövdede `{"success":false}` | Status code anlamlıdır: araçlar, izleme, cache ve retry ona bakar |
| `401` ile `403`ü karıştırmak | 401 = kimlik yok/geçersiz, 403 = kimlik var ama yetki yok |
| POST'u körlemesine retry etmek | Idempotent değil; Idempotency-Key ya da yalnızca idempotent işlemler için otomatik retry |
| CORS'u "backend sorunu değil" sanmak ya da `AllowAnyOrigin` ile geçiştirmek | Allow-list kullanın; CORS tarayıcı koruması, sunucu güvenliğinin yerini tutmaz |
| `Cache-Control: no-cache`'i "önbelleğe alma" sanmak | `no-cache` = revalidate et; hiç saklamamak için `no-store` |
| Cookie'ye `Secure`/`HttpOnly`/`SameSite` koymamak | XSS ve CSRF yüzeyini açar |
| Hassas veriyi query string'e koymak (`?token=...`) | URL'ler log, tarayıcı geçmişi ve proxy'lerde görünür; header/body kullanın |
| Self-signed sertifikada doğrulamayı kapatmak (`ServerCertificateCustomValidationCallback = true`) | Kodu prod'a taşınca MITM'e kapı açar; sertifikayı güvenilir kök deposuna ekleyin |
| DNS değişikliğini anında yansıyacağını sanmak | TTL kadar bekleme; geçişten önce TTL'i düşürün |
| Proxy arkasında `X-Forwarded-*` header'ına koşulsuz güvenmek | Yalnızca bilinen proxy'lerden gelenleri kabul edin |

---

## 7. Performans & trade-off

- **Yeni bağlantı maliyeti:** DNS + TCP (1 RTT) + TLS (1–2 RTT) → keep-alive, bağlantı havuzu, HTTP/2 multiplexing, TLS session resumption bunu amorti eder (→ J1.2 §3.5).
- **HTTP/2 vs /3:** HTTP/2 çok sayıda küçük kaynakta bağlantı sayısını azaltır; kayıplı mobil/Wi-Fi ağlarında HTTP/3 (HOL blocking yok, 0-RTT) daha iyidir, ama UDP bazı kurumsal ağlarda engellidir → HTTP/2'ye düşüş (fallback) gerekir.
- **Cache stratejisi:** `max-age` (hiç istek yok, hızlı ama bayat riski) ↔ `ETag`/`304` (her seferinde doğrulama, küçük yanıt) ↔ `no-store` (güvenlik, maliyet). Statik varlıklar: dosya adına hash + uzun `max-age`; API yanıtları: `no-cache` + ETag ya da kısa `max-age`.
- **Sıkıştırma (gzip/brotli):** Metin tabanlı yanıtlarda bant genişliğini %70+ azaltır; CPU maliyeti vardır, zaten sıkıştırılmış içeriğe (JPEG, ZIP) uygulanmaz. (HTTPS + sıkıştırma + sırlar birlikteyse BREACH saldırısı gibi riskleri bilin.)
- **CDN:** Gecikme ve origin yükünü düşürür; cache invalidation karmaşıklığı ve maliyet ekler.
- **TLS sonlandırma noktası:** Load balancer'da sonlandırmak CPU'yu yükten alır ve sertifikayı tek yerde yönetir; arka uçta düz HTTP kalırsa iç ağ güvenliği (veya mTLS) gerekir.
- **Header şişkinliği:** Büyük cookie/token'lar her isteğe eklenir; HTTP/1.1'de sıkıştırılmaz (HTTP/2'de HPACK ile hafifler).

---

## 8. Kaynaklar

- MDN — HTTP kılavuzu (Türkçe kısımlar mevcut): <https://developer.mozilla.org/docs/Web/HTTP>
- MDN — HTTP yöntemleri / durum kodları / CORS: <https://developer.mozilla.org/docs/Web/HTTP/Methods> · <https://developer.mozilla.org/docs/Web/HTTP/Status> · <https://developer.mozilla.org/docs/Web/HTTP/CORS>
- RFC 9110 — HTTP Semantics: <https://www.rfc-editor.org/rfc/rfc9110>
- High Performance Browser Networking (Ilya Grigorik, ücretsiz): <https://hpbn.co>
- Cloudflare Learning Center — DNS, TLS, HTTP/3: <https://www.cloudflare.com/learning/>
- ASP.NET Core — Kestrel web server: <https://learn.microsoft.com/aspnet/core/fundamentals/servers/kestrel>
- ASP.NET Core — CORS etkinleştirme: <https://learn.microsoft.com/aspnet/core/security/cors>
- ASP.NET Core — HTTPS zorunlu kılma ve HSTS: <https://learn.microsoft.com/aspnet/core/security/enforcing-ssl>
- Let's Encrypt — sertifika zinciri nasıl çalışır: <https://letsencrypt.org/docs/certificate-compatibility/>
- curl `-w` değişkenleri: <https://curl.se/docs/manpage.html#-w>
