# J1.2 Ağ Temelleri — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. ⭐ OSI modeli nedir? Backend geliştirici hangi katmanlarla ilgilenir?
OSI, ağ iletişimini 7 katmana ayıran kavramsal bir modeldir (Physical, Data Link, Network, Transport, Session, Presentation, Application). Pratikte kullanılan model 4 katmanlı TCP/IP'dir. Backend geliştirici için asıl önemli olanlar **L3 (IP — adresleme/yönlendirme)**, **L4 (TCP/UDP — port, bağlantı, güvenilirlik)** ve **L7 (HTTP — uygulama protokolü)** katmanlarıdır. Önemli olan 7 katmanı ezberlemek değil, bir sorunun hangi katmanda olduğunu sınıflamaktır: `ping` çalışıyor ama `curl` timeout veriyorsa L3 sağlam, sorun L4/L7'dedir.

### 2. Encapsulation nedir?
Veri uygulamadan ağa inerken her katman kendi header'ını ekler: HTTP verisi → TCP header'ı eklenir (segment) → IP header'ı eklenir (packet) → Ethernet header'ı eklenir (frame). Alıcıda aynı sıra tersine soyulur. Bu yüzden Wireshark'ta bir paketin içinde iç içe geçmiş katman header'ları görürüz.

### 3. ⭐ TCP ile UDP arasındaki fark nedir? UDP ne zaman tercih edilir?
TCP bağlantı odaklıdır (handshake), teslimi garanti eder, sırayı korur, flow ve congestion control uygular; bedeli daha büyük header (20+ byte) ve gecikmedir. UDP bağlantısızdır, teslim/sıra garantisi yoktur, header'ı 8 byte'tır, çok hızlıdır. Her datagram bağımsızdır (mesaj sınırı korunur), TCP ise bayt akışıdır. UDP; gecikmenin güvenilirlikten önemli olduğu veya eski verinin değersiz kaldığı durumlarda seçilir: canlı ses/video, online oyun, DNS sorguları, telemetri. **QUIC (HTTP/3)** UDP üzerinde kendi güvenilirliğini kurar.

### 4. ⭐ TCP three-way handshake'i anlat. Neden 3 adım?
İstemci `SYN` (seq=x) gönderir; sunucu `SYN-ACK` (seq=y, ack=x+1) ile cevap verir; istemci `ACK` (ack=y+1) ile bağlantıyı onaylar. Üç adım gerekir çünkü **iki tarafın da kendi başlangıç sequence number'ını duyurması ve karşı tarafın bunu onaylaması** gerekir; sunucunun SYN ve ACK'i tek pakette birleştirmesi 4 adımı 3'e indirir. İki adım olsaydı sunucu, istemcinin kendi seq'ini aldığını doğrulayamaz, eski/yinelenmiş SYN'ler yarım açık bağlantı yaratabilirdi. Maliyeti **1 RTT**'dir; HTTPS'te üzerine TLS handshake eklenir.

### 5. ⭐ `TIME_WAIT` nedir, neden birikir?
Bağlantıyı **ilk kapatan** taraf (active close), son `ACK`'i gönderdikten sonra `TIME_WAIT` durumunda ~2×MSL (Linux'ta 60 sn) bekler. Amaç: (1) son `ACK` kaybolursa gelecek tekrar `FIN`'e cevap verebilmek, (2) ağda gecikmiş eski paketlerin aynı dörtlüyle açılacak yeni bağlantıya karışmasını önlemek. Normal ve gereklidir; ama çok sayıda kısa ömürlü bağlantı açıp ilk kapatan taraf olursak (ör. her istekte yeni bağlantı) soketler birikir ve **ephemeral port havuzu** tükenebilir.

### 6. ⭐ Ephemeral port exhaustion nedir? .NET'teki karşılığı nedir?
İstemci her bağlantıda işletim sisteminden geçici bir kaynak portu alır (Linux'ta varsayılan ~28k port). `TIME_WAIT`'teki bağlantılar portu 60 sn rehin tutar; aynı hedefe saniyede ~470'ten fazla yeni bağlantı açarsanız havuz biter ve `SocketException: Cannot assign requested address` alırsınız. .NET'te klasik sebebi, her istekte `new HttpClient()` oluşturup dispose etmektir (**socket exhaustion**). Çözüm: `HttpClient`'ı yeniden kullanmak ya da `IHttpClientFactory` ile havuzlanmış bağlantılar (keep-alive).

### 7. Bir bağlantıyı yeniden kullanmak (keep-alive) neden önemli?
Yeni bağlantı = TCP handshake (1 RTT) + HTTPS ise TLS handshake (1–2 RTT) + TCP slow-start'ın sıfırdan başlaması + bir ephemeral port + `TIME_WAIT` maliyeti. Açık bir bağlantıyı yeniden kullanmak bunların hepsini tek seferliğe indirir. Gecikmesi yüksek hatlarda fark yüzlerce milisaniyedir.

### 8. "Connection refused" ile "connection timed out" arasındaki fark nedir?
**Refused:** paket hedefe ulaştı, ama o portu dinleyen yok; işletim sistemi `RST` döndü → hemen hata. Servis kapalı ya da yanlış port. **Timeout:** hiçbir cevap gelmedi; paket sessizce düşürüldü (firewall/security group DROP kuralı, yanlış IP, route yok). Bu ayrım teşhisi yönlendirir: refused → servisi kontrol et, timeout → ağ kurallarını kontrol et.

### 9. Bir portu neden iki process dinleyemez?
Gelen paket işletim sistemi tarafından `(protokol, hedef IP, hedef port)` ile tek bir socket'e eşlenir. İki dinleyici olsaydı paketin kime ait olduğu belirsiz olurdu; ikinci `bind` `Address already in use` hatası verir. (Bilinçli istisna: `SO_REUSEPORT` — çekirdek yükü dinleyiciler arasında dağıtır.) Bir sunucu portuna ise binlerce istemci bağlanabilir, çünkü bağlantı dörtlüsü (kaynak IP:port + hedef IP:port) her biri için farklıdır.

### 10. Socket nedir? Sunucu ve istemci tarafında akış nasıldır?
Socket, uygulamanın ağa açılan uç noktasıdır (IP + port + protokol). Sunucu: `bind` (adrese bağlan) → `listen` (bağlantı kuyruğu aç) → `accept` (kuyruktan bir bağlantı al, **yeni bir socket** döner) → okuma/yazma. İstemci: `connect` → okuma/yazma. .NET'te `Socket` en alt seviyedir; `TcpListener`/`TcpClient`/`UdpClient` üst seviye sarmalayıcılar, `NetworkStream` ise socket'i `Stream`'e çevirir.

### 11. TCP'de iki `Write` karşıda iki `Read` olarak mı gelir?
Hayır. TCP bayt akışıdır, mesaj sınırı yoktur: iki `Write` tek `Read`'de birleşebilir, tek `Write` birden çok `Read`'e bölünebilir. Uygulama kendi sınırını çizmelidir: satır sonu (`\n`), uzunluk öneki veya sabit boyut. `Stream.Read` kısmi okuyabileceği için tam bayt sayısı gerektiğinde `ReadExactly` ya da döngü kullanılır. UDP'de ise bir `Send` = bir `Receive`.

### 12. Flow control ile congestion control arasındaki fark?
**Flow control** alıcıyı korur: alıcı receive window ile "tamponumda ne kadar yer var" bildirir, gönderen bunu aşmaz. **Congestion control** ağı korur: gönderen slow start (üstel artış) → congestion avoidance (doğrusal artış) ile hızını ağın taşıyabileceği seviyeye uyarlar, kayıp görünce hızını düşürür.

### 13. HTTP/2'de bile TCP kaynaklı head-of-line blocking neden var? HTTP/3 bunu nasıl çözer?
TCP tek bir sıralı bayt akışıdır; bir segment kaybolursa sonrasındaki veriler (başka bir HTTP isteğine ait olsalar da) eksik olan yeniden gönderilene kadar uygulamaya teslim edilmez. HTTP/2 tek TCP bağlantısında çoklama yaptığı için tüm akışlar etkilenir. HTTP/3, UDP üstündeki QUIC ile akışları transport seviyesinde bağımsız hale getirir; bir akıştaki kayıp diğerlerini bekletmez (→ M5.11).

### 14. Latency, bandwidth ve throughput arasındaki fark nedir?
Latency bir paketin gidiş süresi (RTT: gidiş-dönüş); bandwidth hattın teorik kapasitesi; throughput gerçekte ulaşılan hızdır (≤ bandwidth; kayıp, RTT ve TCP window'dan etkilenir). Boru benzetmesi: latency uzunluk, bandwidth kalınlık. Küçük/çok sayıda istekte performansı latency belirler, bu yüzden "chatty" API'ler kötüdür.

### 15. ⭐ L4 ile L7 load balancer farkı nedir?
L4 yalnızca IP/port/protokol (TCP/UDP) görür; karar bağlantı bazındadır, çok hızlıdır, TLS'e genelde dokunmaz (örn. Azure Load Balancer). L7 HTTP'yi anlar: path, header, cookie, host adına göre **istek bazında** yönlendirir, TLS sonlandırabilir, WAF/sticky session/rewrite sunar, daha pahalı ve yavaştır (örn. Application Gateway, NGINX, YARP).

### 16. Private IP aralıkları nelerdir? NAT ne işe yarar?
`10.0.0.0/8`, `172.16.0.0/12` ve `192.168.0.0/16` internete yönlendirilmeyen özel adreslerdir. NAT, router'ın iç ağdaki birçok private IP'yi tek bir public IP arkasında (IP:port eşleme tablosu tutarak) internete çıkarmasıdır; dışarıdan içeri doğrudan bağlantıyı da engellediğinden port yönlendirme gerekir.

### 17. `/24` ne demek? `10.0.1.0/24` kaç adres içerir?
İlk 24 bit ağ kısmı, kalan 8 bit host kısmıdır: `2^8 = 256` adres (ağ ve broadcast adresleri hariç 254 kullanılabilir). Genel formül `2^(32 − prefix)`.

### 18. Bir servise erişemiyorum. Nasıl teşhis edersin?
Aşağıdan yukarıya: (1) `nslookup`/`dig` ile isim çözülüyor mu; (2) `ping`/`traceroute` ile IP'ye ulaşılıyor mu (ICMP engelli olabilir); (3) `nc -zv host port` ile port açık mı — **refused** (servis kapalı) mi **timeout** (firewall/security group) mu; (4) `curl -v` ile TLS ve HTTP cevabı; (5) gerekirse `tcpdump`/Wireshark ile paketleri izleyip SYN'e SYN-ACK dönüp dönmediğine bakarım. Sunucu tarafında `ss -ltnp` ile servisin doğru arayüz/portta dinlediğini doğrularım.

### 19. Uygulamam container içinde çalışıyor ama dışarıdan erişemiyorum. İlk neye bakarsın?
Uygulama `localhost`/`127.0.0.1`'e bağlıysa container'ın kendi loopback'idir, dışarıdan erişilmez; `0.0.0.0`'a (ASP.NET'te `ASPNETCORE_URLS=http://+:8080`) bağlanmalı. Sonra port yayınlama (`-p 8080:8080`) ve firewall/security group kurallarına bakarım (→ J12).

### 20. UDP kullanan bir servise paket gitti mi, nasıl anlarsın?
UDP'de teslim bildirimi yoktur; `Send` başarılı dönse de paket kaybolmuş olabilir. Bilmek için uygulama katmanında kendi onay (ACK), sıra numarası ve zaman aşımı/yeniden gönderim mekanizmasını yazmak gerekir (QUIC ve birçok oyun protokolü bunu yapar). Hedef port kapalıysa bazen ICMP "port unreachable" döner ama bu garanti değildir.
