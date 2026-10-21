# J1.2 Ağ Temelleri: OSI, TCP/UDP & Socket ⭐

> 📁 `01-junior/01-dev-foundations/02-network-fundamentals/` · Önkoşul: — · Sorular: [`interview-questions.md`](interview-questions.md)

HTTP'nin altında ne olduğunu bilmeden "bu istek neden yavaş?", "bağlantı neden timeout oluyor?" ya da "socket exhaustion nedir?" sorularına cevap verilemez. Backend mülakatlarının klasik ısınma turudur.

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

Bilgisayarların birbiriyle konuşması **katmanlı** bir problemdir: her katman tek bir işi yapar ve altındaki katmanın hizmetini kullanır. Uygulama yazarken "veriyi kablodan nasıl taşırım" diye düşünmeyiz, çünkü alt katmanlar bunu soyutlar.

### 1.1 OSI 7 katman ve TCP/IP 4 katman

| OSI | Katman | Ne yapar? | Örnek | TCP/IP |
|:---:|--------|-----------|-------|--------|
| 7 | **Application** | Uygulamanın konuştuğu protokol | HTTP, DNS, SMTP, gRPC | **Application** |
| 6 | Presentation | Kodlama, sıkıştırma, şifreleme | TLS, JSON/UTF-8 | ↑ |
| 5 | Session | Oturum yönetimi | (pratikte L7'ye gömülü) | ↑ |
| 4 | **Transport** | Uçtan uca iletim, port | **TCP, UDP**, QUIC | **Transport** |
| 3 | **Network** | Adresleme ve yönlendirme | **IP**, ICMP | **Internet** |
| 2 | Data Link | Aynı ağ segmentinde çerçeve taşıma | Ethernet, Wi-Fi, MAC adresi | **Network Access** |
| 1 | Physical | Bit'lerin fiziksel taşınması | Kablo, fiber, radyo | ↑ |

Ezber için: **P**lease **D**o **N**ot **T**hrow **S**ausage **P**izza **A**way (aşağıdan yukarı).

> Gerçek hayatta kullanılan model **TCP/IP**'dir; OSI daha çok konuşma/öğrenme sözlüğüdür. "L4 load balancer", "L7 firewall" gibi terimler OSI numaralarıyla söylenir.

**Backend geliştiricinin gerçekten ilgilendiği üç katman:**

| Katman | Hangi soru burada çözülür? |
|--------|---------------------------|
| **L3 — IP** | "Bu makineye ulaşabiliyor muyum? Hangi ağdayız? DNS doğru IP'yi mi verdi?" |
| **L4 — TCP/UDP** | "Port açık mı? Bağlantı kuruluyor mu? Timeout mu, reset mi? Paket kaybı var mı?" |
| **L7 — HTTP** | "Status code, header, body, TLS, yönlendirme, auth ne diyor?" |

Yedi katmanı ezberlemek değil, **sorunun hangi katmanda olduğunu** sınıflamak önemlidir: `ping` çalışıyor ama `curl` timeout → L3 tamam, L4/L7'de sorun.

### 1.2 Encapsulation (sarmalama)

Veri aşağı inerken her katman kendi header'ını ekler; alıcıda yukarı çıkarken sırayla soyulur:

```
L7  [ HTTP: GET /orders ... ]                              ← veri (data)
L4  [ TCP header | HTTP verisi ]                           ← segment   (src/dst port, seq, flags)
L3  [ IP header | TCP header | HTTP verisi ]               ← packet    (src/dst IP)
L2  [ Eth header | IP | TCP | HTTP | Eth trailer ]         ← frame     (MAC adresleri)
```

Her birimin adı farklıdır: **segment** (TCP) / **datagram** (UDP) → **packet** (IP) → **frame** (Ethernet). Wireshark'ta bu iç içe geçmiş header'ları görürsünüz (yakalama alıştırması: 3.10).

---

## 2. Neden?

- **Teşhis:** "Uygulama yavaş" şikâyetinin altında DNS gecikmesi, TCP handshake maliyeti, paket kaybı ya da tükenen port olabilir. Katmanı bilmeden araç da seçemezsiniz.
- **Doğru protokol seçimi:** Sohbet, canlı video, oyun, DNS, ödeme API'si — hepsi aynı transport'u istemez.
- **.NET'te gerçek hatalar:** `HttpClient` socket exhaustion, `SocketException: Connection refused / timed out`, `Address already in use` — hepsi bu konunun doğrudan sonucudur.
- **Mimari kararlar:** L4 vs L7 load balancer, güvenlik duvarı/security group kuralları, HTTP/2–3'ün neden var olduğu (→ M5.11) bu zeminin üstüne oturur.

---

## 3. Nasıl?

### 3.1 L3 — IP adresleme

- **IPv4:** 32 bit (`192.168.1.10`), ~4,3 milyar adres → tükendi. **IPv6:** 128 bit (`2001:db8::1`).
- **Subnet / CIDR:** `10.0.1.0/24` = ilk 24 bit ağ kısmı, son 8 bit host kısmı → 256 adres (254 kullanılabilir). `/16` = 65 536 adres, `/32` = tek adres.
  - Hızlı hesap: adres sayısı = `2^(32 − önek)`.
- **Private (RFC 1918) aralıklar** — internete yönlendirilmez, iç ağda kullanılır:

  | Aralık | CIDR | Tipik kullanım |
  |--------|------|----------------|
  | `10.0.0.0 – 10.255.255.255` | `10.0.0.0/8` | Kurumsal ağlar, bulut VNet/VPC |
  | `172.16.0.0 – 172.31.255.255` | `172.16.0.0/12` | **Docker varsayılan köprü (`172.17.0.0/16`)** |
  | `192.168.0.0 – 192.168.255.255` | `192.168.0.0/16` | Ev/ofis router'ları |

- **Loopback:** `127.0.0.1` (`localhost`, IPv6'da `::1`). Paket ağ kartına hiç çıkmaz; kendi makinenizle konuşur. Test ederken ağ sorunlarını eler.
- **`0.0.0.0`:** "tüm arayüzler" (dinlerken) — `127.0.0.1`'e bağlı sunucu dışarıdan erişilemez, `0.0.0.0`'a bağlı olan erişilebilir. **Container içinde uygulama `localhost`'a bağlıysa dışarıdan erişilemez** — klasik Docker hatası (→ J12).
- **NAT:** Router, iç ağdaki birçok private IP'yi tek bir public IP arkasına gizler; (iç IP, port) ↔ (dış IP, port) eşleşmesini tablo tutar. Sonuç: dışarıdan iç makineye bağlanmak için port yönlendirme gerekir.
- **Routing:** Her router paketi hedef IP'ye bakıp "bir sonraki sekme"ye (next hop) iletir. `traceroute` (Windows: `tracert`) hedefe giden yoldaki router'ları TTL'i 1, 2, 3… artırarak keşfeder; yol boyunca gecikmenin nerede arttığını gösterir.

### 3.2 Port kavramı

IP **hangi makine**, port **o makinedeki hangi uygulama** demektir (16 bit: 0–65535).

| Port | Servis |
|-----:|--------|
| 22 | SSH |
| 53 | DNS (UDP ve TCP) |
| 80 / 443 | HTTP / HTTPS |
| 1433 | SQL Server |
| 3306 | MySQL |
| 5432 | PostgreSQL |
| 5672 / 15672 | RabbitMQ (AMQP / yönetim arayüzü) |
| 6379 | Redis |
| 27017 | MongoDB |

- **Well-known (0–1023)**, **registered (1024–49151)**, **dynamic/ephemeral (Linux varsayılanı 32768–60999; IANA önerisi 49152–65535)**.
- **Ephemeral port:** İstemci bağlanırken işletim sistemi ona geçici, rastgele bir kaynak portu verir. Sunucu `:443` dinler; istemci `:52341` → `:443` bağlanır. Aynı anda açık bağlantı sayısı, bu havuzla sınırlıdır (bkz. 3.5).
- **Neden iki process aynı portu dinleyemez?** Gelen paketi işletim sistemi `(protokol, hedef IP, hedef port)` ile bir socket'e eşler; iki sahip olsaydı paketin kime gideceği belirsiz olurdu. İkinci `bind` → `Address already in use`. (`SO_REUSEPORT` gibi bilinçli istisnalar vardır; varsayılan davranış budur.)

### 3.3 L4 — TCP vs UDP ⭐

| | TCP | UDP |
|---|-----|-----|
| Bağlantı | Connection-oriented (handshake) | Connectionless |
| Güvenilirlik | Garantili teslim, sıra korunur, kayıp yeniden gönderilir | Garanti yok, sıra yok |
| Mesaj sınırı | **Yok** — bayt akışı | **Var** — her datagram ayrı |
| Hız / overhead | Daha yavaş, header 20+ byte | Hızlı, header 8 byte |
| Akış kontrolü | Flow + congestion control | Yok |
| Kullanım | HTTP/1.1-2, SQL, SMTP, SSH | DNS, video/ses, oyun, **QUIC → HTTP/3** |

**UDP ne zaman?** Gecikmenin güvenilirlikten önemli olduğu, eski verinin değersiz olduğu durumlar: canlı ses/video (kaybolan kare beklenmez), online oyun konumu, DNS sorgusu (tek küçük soru-cevap, handshake israfı), telemetri/metrik. Üstüne kendi güvenilirliğini ekleyen protokoller de vardır: **QUIC** UDP üzerinde çalışır.

#### TCP three-way handshake ⭐

```
İstemci                                Sunucu
  │ ── SYN, seq=x ─────────────────────▶ │   "Bağlanmak istiyorum"
  │ ◀── SYN-ACK, seq=y, ack=x+1 ──────── │   "Tamam, ben de hazırım"
  │ ── ACK, ack=y+1 ───────────────────▶ │   "Aldım" → bağlantı KURULDU
  │ ── veri ... ───────────────────────▶ │
```

- Maliyeti **1 RTT** (round-trip time): veri göndermeden önce bir gidiş-dönüş beklenir. Yüksek gecikmeli hatta (kıtalararası 150 ms) bu, her yeni bağlantıya 150 ms ekler. HTTPS'te üstüne TLS handshake eklenir (→ J1.3). **Bu yüzden bağlantı yeniden kullanımı (keep-alive) önemlidir.**
- Sequence number'lar her iki yönde baytları numaralar: kayıp/sıralama tespiti bunun üstüne kuruludur.
- Aksi: port kapalıysa sunucu `RST` döner → istemci hemen **"Connection refused"** alır. Firewall paketi **sessizce düşürürse** (DROP) cevap hiç gelmez → istemci uzun süre bekler → **timeout**. *Refused = hedefe ulaştım, kapı kapalı; Timeout = duvara çarptım ya da yol yok.*

#### Bağlantı kapatma ve `TIME_WAIT`

```
İstemci                                Sunucu
  │ ── FIN ────────────────────────────▶ │   "Gönderecek verim bitti"
  │ ◀── ACK ──────────────────────────── │
  │ ◀── FIN ──────────────────────────── │   sunucu da bitirir
  │ ── ACK ────────────────────────────▶ │
  ▼ TIME_WAIT (~2×MSL, Linux'ta 60 sn)     ▼ kapalı
```

Her yön ayrı kapanır (dörtlü). **İlk FIN'i gönderen taraf (active close) `TIME_WAIT`'e girer** ve o bağlantının dörtlüsünü (`src IP:port, dst IP:port`) **2×MSL** (Maximum Segment Lifetime ≈ 30 sn → toplam ~60 sn) boyunca tutar. İki neden:
1. Son `ACK` kaybolursa karşı taraf `FIN`'i tekrar gönderir; cevap verecek biri olmalı.
2. Ağda gecikmiş eski paketlerin **aynı portlarla açılan yeni bağlantıya karışmasını** engellemek.

`TIME_WAIT` bir hata değil, normal ve gerekli bir durumdur — ancak çok sayıda kısa ömürlü bağlantıda **birikir** (bkz. 3.5).

#### TCP güvenilirlik mekanizmaları (kavramsal)

| Mekanizma | Ne işe yarar? |
|-----------|---------------|
| **Sequence number + ACK** | Hangi baytların ulaştığını doğrular, sıralamayı sağlar |
| **Retransmission** | ACK gelmezse (timeout / 3 duplicate ACK) segmenti tekrar gönderir |
| **Sliding window** | ACK beklemeden aynı anda birden çok segment "uçuşta" olabilir → hat verimli kullanılır |
| **Flow control (receive window)** | Alıcı "tamponumda şu kadar yerim var" diye bildirir; gönderen alıcıyı boğmaz |
| **Congestion control** | **Slow start** (küçük başla, her RTT'de üstel artır) → **congestion avoidance** (doğrusal artır) → kayıp görünce yavaşla. Ağı (alıcıyı değil) korur. |

[`LossDemo`](LossDemo.cs) flow control'ün etkisini gösterir: yavaş alıcıya TCP ile gönderen **bekletilir**, UDP ile gönderen aldırmaz ve paketler düşer.

**Head-of-line (HOL) blocking:** TCP sıralı teslim eder; bir segment kaybolursa sonraki segmentler (başka bir isteğe ait olsalar bile) **eksik olan gelene kadar** uygulamaya verilmez. HTTP/2 tek TCP bağlantısı üzerinde çoklama yapar; kayıp olduğunda tüm akışlar bu yüzden bekler. HTTP/3 (QUIC) bunu UDP üstünde akış bazında bağımsız teslimle çözer (→ M5.11).

### 3.4 Socket

**Socket**, uygulamanın ağa açılan kapısıdır: bir **endpoint = IP + port + protokol**. Bir TCP bağlantısı, iki endpoint'in dörtlüsüyle tanımlanır: `(kaynak IP, kaynak port, hedef IP, hedef port)`. Aynı sunucu portuna binlerce istemci bağlanabilir çünkü dörtlü her biri için farklıdır.

```
Sunucu (TCP):                          İstemci (TCP):
  socket()                               socket()
  bind(IP, port)                         connect(IP, port)  ──────▶ (3'lü el sıkışma)
  listen()
  accept()  ◀──────────────────────────
  read()/write()                         write()/read()
  close()                                close()
```

- `listen`, bağlantı kuyruğu (backlog) açar; `accept` kuyruktan **yeni bir socket** döndürür. Dinleyen socket ayrı, her bağlı istemci için üretilen socket ayrıdır.
- **.NET karşılıkları**

  | Soyutlama | Sınıf |
  |-----------|-------|
  | En alt seviye (BSD socket) | `System.Net.Sockets.Socket` |
  | TCP sunucu / istemci | `TcpListener` / `TcpClient` |
  | UDP | `UdpClient` |
  | Bağlantı üzerinde `Stream` | `NetworkStream` (`client.GetStream()`) |
  | Üst seviye | `HttpClient`, Kestrel (hepsi sonunda bunların üstüne kuruludur) |

- **TCP bayt akışıdır:** İki `Write("A")` ve `Write("B")` karşıda tek `Read` ile `"AB"` olarak gelebilir; tek bir `Write` birden çok `Read`'e bölünebilir. Mesaj sınırını uygulama protokolü çizer: satır sonu (`\n`), uzunluk öneki (length-prefix) ya da sabit boyut. [`TcpEcho`](TcpEcho.cs) satır tabanlı, [`LossDemo`](LossDemo.cs) sabit 512 byte + `ReadExactlyAsync` kullanır. UDP'de ise bir `Send` = bir `Receive`.

### 3.5 Ephemeral port exhaustion & keep-alive ⭐

Bir bağlantı açıp **ilk kapatan** taraf `TIME_WAIT`'e girer. İstemci her istekte yeni bağlantı açıp kapatırsa:

```
istek/sn × 60 sn  ≈  TIME_WAIT'te bekleyen soket sayısı
```

Linux varsayılanında ~28 000 ephemeral port vardır. Aynı hedef (IP:port) için saniyede ~470'ten fazla yeni bağlantı açarsanız havuz tükenir ve yeni bağlantı kurulamaz:

```
SocketException: Cannot assign requested address   (AddressNotAvailable)
```

**.NET'teki yüzü:** Her istekte `new HttpClient()` (veya `using var c = new HttpClient()`) → her örnek kendi bağlantı havuzunu açar → dispose edilince bağlantılar kapanır → `TIME_WAIT` birikir → yük altında port tükenir. Çözüm: `HttpClient`'ı yeniden kullanmak ya da `IHttpClientFactory` (→ J7.5, M5.9).

**Connection reuse / keep-alive neden önemli?** Yeni bağlantı = handshake (1 RTT) + (HTTPS ise TLS 1–2 RTT) + TCP slow-start'ın sıfırdan başlaması + port tüketimi. Açık bağlantıyı yeniden kullanmak hepsini ortadan kaldırır. HTTP/1.1'de `Connection: keep-alive` varsayılandır; HTTP/2 tek bağlantıda çoklama yapar.

> 🛠 Pratik: `dotnet run -- 1.2 time-wait 500` → sayacın arttığını, `--reuse` ile artmadığını görün.

### 3.6 Performans kavramları

| Kavram | Anlamı | Benzetme (boru) |
|--------|--------|-----------------|
| **Latency** | Bir paketin A'dan B'ye gitme süresi (ms). **RTT** = gidiş-dönüş | Borunun **uzunluğu** |
| **Bandwidth** | Hattın teorik kapasitesi (Mbps) | Borunun **kalınlığı** |
| **Throughput** | Gerçekte elde edilen veri hızı | Borudan gerçekten **akan** su |

- Throughput ≤ bandwidth; kayıp, RTT ve TCP window yüzünden daha düşüktür. Çok küçük istekler için **bandwidth değil latency** belirleyicidir: 100 Gbps hat 1 baytlık bir istek için RTT'yi kısaltmaz. Kıtalararası çağrıda her handshake'in 100+ ms olması bu yüzdendir. "Chatty" API'ler (çok sayıda küçük çağrı) bu yüzden kötüdür.
- **MTU:** Bir Ethernet çerçevesine sığan en büyük IP paketi (genelde **1500 byte**). Daha büyük paket **parçalanır (fragmentation)**; VPN/tünel MTU'yu düşürür, yanlış ayarlanırsa "küçük istekler çalışıyor, büyükler takılıyor" gibi sinsi hatalar çıkar.
- **Bufferbloat:** Ağ cihazlarında aşırı büyük tamponlar yüklü hatta gecikmeyi saniyelere çıkarır (kısa not: cevap "tamponları küçültmek / AQM").

### 3.7 L4 vs L7 load balancer ⭐

| | L4 Load Balancer | L7 Load Balancer |
|---|------------------|------------------|
| Gördüğü | IP, port, TCP/UDP | HTTP method, path, header, cookie, host adı |
| Kararı | Bağlantı bazında (hangi backend'e) | **İstek bazında** (`/api/*` → servis A) |
| TLS | Genelde geçirir (passthrough) | Genelde sonlandırır (termination) |
| Hız | Çok hızlı, ucuz | Daha yavaş, daha akıllı |
| Örnek | Azure Load Balancer, AWS NLB | Azure Application Gateway, AWS ALB, NGINX, YARP |

Sonuç: "path'e göre yönlendir", "cookie ile sticky session", "WAF" için L7; ham TCP/UDP trafiği ve maksimum performans için L4 (→ S6.5).

### 3.8 Güvenlik duvarı ve "bağlantı neden timeout oluyor" teşhisi

**Firewall / security group / NSG** kuralları `(yön, protokol, kaynak, hedef port)` ile trafiğe izin verir ya da engeller. Bulutta "uygulama çalışıyor ama dışarıdan erişemiyorum" vakasının 1 numaralı sebebi, **ilgili portun security group'ta açılmamış olmasıdır**.

Teşhis ağacı (aşağıdan yukarı):

```
1. İsim çözülüyor mu?              nslookup api.firm.com                       ✗ → DNS sorunu (L7/DNS)
2. IP'ye ulaşılıyor mu?            ping <ip>   /  traceroute <ip>              ✗ → yönlendirme / ICMP engelli olabilir
3. Port açık mı?                   nc -zv <ip> 443  /  telnet <ip> 443
      • "Connection refused"  → host'a ulaştık, o portta dinleyen YOK (servis kapalı / yanlış port)  [RST geldi]
      • "timed out"           → paket sessizce düşüyor: FIREWALL / security group / yanlış IP / route
4. Servis sağlıklı mı?             curl -v https://<host>/health               ✗ → uygulama / TLS / HTTP sorunu
```

> Not: Bazı ağlar `ping`'i (ICMP) engeller; `ping` başarısız diye host'un kapalı olduğu sonucuna varmayın, 3. adımla port seviyesinde doğrulayın.

### 3.9 Teşhis araç kutusu

| Araç | Ne için | Örnek |
|------|---------|-------|
| `ping` | L3 erişim, RTT, paket kaybı | `ping -c 4 8.8.8.8` |
| `traceroute` / `tracert` | Yoldaki router'lar, gecikme nerede artıyor | `traceroute api.example.com` |
| `nslookup` / `dig` | DNS sorgusu | `dig +short example.com` |
| `ss -tanp` (eski: `netstat -tanp`) | Açık bağlantılar, dinleyen portlar, durumlar (`ESTAB`, `TIME_WAIT`) | `ss -tan state time-wait \| wc -l` |
| `ss -ltnp` | Hangi process hangi portu **dinliyor** | `ss -ltnp \| grep 5000` |
| `nc` / `telnet` | Port erişim testi, ham TCP konuşması | `nc -zv db.firm.com 5432` |
| `curl -v` | HTTP isteği, header, TLS handshake detayı | `curl -v https://example.com` |
| `tcpdump` / **Wireshark** | Paket yakalama; handshake, retransmission, RST | `sudo tcpdump -i any -n port 5432` |

Wireshark'ta faydalı filtreler: `tcp.port == 5000`, `tcp.flags.syn == 1`, `tcp.analysis.retransmission`, `tcp.flags.reset == 1`, `dns`.

### 3.10 Pratik: kodla ve paket yakalamayla gözlem

Bu konunun kodu J01 projesindedir (modül klasöründen çalıştırın). Her komut ağın farklı bir yönünü gösterir; `dotnet run -- 1.2 help` hepsini listeler:

```bash
dotnet run -- 1.2 tcp-server 5000                          # satır tabanlı TCP echo (her istemci için ayrı Task)
dotnet run -- 1.2 tcp-client 127.0.0.1 5000 merhaba dunya  # ikinci terminal
dotnet run -- 1.2 udp-server 5001                          # UDP echo: bağlantı YOK, her datagram bağımsız
dotnet run -- 1.2 udp-client 127.0.0.1 5001 selam          # sunucu kapalıysa cevap gelmez → zaman aşımı
dotnet run -- 1.2 loss 20000                               # aynı yük TCP ve UDP ile: UDP'de paket kaybı
dotnet run -- 1.2 time-wait 500 [--reuse]                  # TIME_WAIT birikimi; --reuse tek bağlantı
```

**Handshake'i tcpdump ile yakalamak** (Linux/macOS, root gerekir; .NET gerekmez; Wireshark'ta: loopback, filtre `tcp.port == 5000`):

```bash
sudo tcpdump -i lo -n -S "tcp port 5000" &        # macOS'ta arayüz: lo0
nc -l 127.0.0.1 5000 >/dev/null &                 # sunucu
printf 'merhaba\n' | nc -q 1 127.0.0.1 5000       # istemci: bağlan, 1 mesaj gönder, kapat
sleep 1; sudo pkill tcpdump
```

Nasıl okunur (`Flags`: `S`=SYN, `S.`=SYN-ACK, `.`=ACK, `P.`=veri, `F.`=FIN-ACK):
1. `[S]` istemci → sunucu: "bağlanmak istiyorum" (seq = x)
2. `[S.]` sunucu → istemci: "tamam, ben de hazırım" (seq = y, ack = x+1)
3. `[.]` istemci → sunucu: "aldım" (ack = y+1) → bağlantı kuruldu, **1 RTT**
4. `[P.]` veri paketi (`merhaba\n` = 8 byte, `length 8`)
5. `[F.]` … `[F.]` … `[.]` kapanış: her yön ayrı FIN ile kapanır (dörtlü); ilk FIN'i gönderen `TIME_WAIT`'e girer.

---

## 4. Gerçek dünya senaryosu

**"Yük testinde API aniden `Cannot assign requested address` ile çöküyor."**

1. İlk 30 saniye her şey yolunda; sonra bir dalga hata, p99 patlıyor. CPU ve DB sakin.
2. Sunucuda `ss -tan state time-wait | wc -l` → **27 000+** → ephemeral port havuzu dolu.
3. Kodda: `public async Task<Product> Get(int id) { using var http = new HttpClient(); return await http.GetFromJsonAsync<Product>(...); }` → her çağrı yeni TCP bağlantısı + `TIME_WAIT`.
4. Çözüm: `HttpClient`'ı singleton/`IHttpClientFactory` ile yeniden kullanmak; bağlantılar keep-alive ile açık kalır, aynı TCP bağlantısı üzerinden binlerce istek gider.
5. Etki: `TIME_WAIT` sayısı düşer, handshake + TLS maliyeti kalkar, gecikme düşer.

Bu sorunu üretmek ve çözmek tam olarak [`TimeWaitDemo`](TimeWaitDemo.cs)'nun konusudur.

**Ek senaryo — "Uygulama pod'u DB'ye bağlanamıyor":** `nc -zv db 5432` → `timed out` (refused değil) ⇒ cevap hiç gelmiyor ⇒ servis kapalı değil, **ağ kuralı** (NSG/firewall/network policy) engelliyor. Doğru yönlendirme: uygulama kodunu değil ağ kuralını kontrol et.

---

## 5. Mülakat perspektifi

Sık sorulanlar ([`interview-questions.md`](interview-questions.md)'de cevaplarıyla):
- OSI modeli nedir, backend geliştirici hangi katmanlarla ilgilenir?
- **TCP vs UDP** — UDP ne zaman tercih edilir? ⭐
- **Three-way handshake**'i anlat; neden 2 değil 3 adım?
- **`TIME_WAIT` nedir, neden birikir, neyi bozar?**
- Connection refused ile timeout arasındaki fark?
- L4 ile L7 load balancer farkı?
- `HttpClient` neden tekrar tekrar `new`'lenmez?

**İpucu:** Cevabı mutlaka *"neden"* ile bitirin: "TIME_WAIT var çünkü gecikmiş paketler yeni bağlantıya karışmasın". Çizim yapmak (handshake okları) hem akışı netleştirir hem güven verir.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| TCP'nin "mesaj" ilettiğini sanmak (`Read` = bir `Write`) | TCP bayt akışıdır; mesaj sınırını protokol çizer (satır sonu / length-prefix), `ReadExactly` kullanın |
| `Stream.Read`'in istenen bayt sayısını döndüreceğini varsaymak | Kısmi okuma normaldir; döngü ya da `ReadExactly` |
| `ping` yok → sunucu kapalı sonucu | ICMP engelli olabilir; port seviyesinde `nc -zv` ile test edin |
| "Connection refused" ile "timeout"u karıştırmak | Refused = RST, port kapalı. Timeout = paket düşüyor (firewall/route) |
| Her istekte `new HttpClient()` | Socket exhaustion; `IHttpClientFactory` veya tek paylaşılan örnek |
| `localhost`/`127.0.0.1`'e bağlanıp dışarıdan erişim beklemek | `0.0.0.0` veya ilgili arayüz; container'da özellikle önemli |
| İstemci tarafında timeout koymamak | `ConnectAsync`/`ReadAsync` sonsuza dek bekleyebilir; `CancellationToken` + timeout |
| UDP'de teslim bekleyip hata yönetmemek | UDP kayıp/sıra/çift teslim yapabilir; uygulama kendi doğrulamasını yazmalı |
| Sunucu, istemci kopunca (RST/IOException) çöküyor | Ağ hataları normaldir; per-client `try/catch`, sunucuyu ayakta tut |
| Portu `TIME_WAIT` yüzünden "bloklu" sanıp ayarlarla oynamak (`tcp_tw_recycle` vb.) | Önce bağlantı yeniden kullanımını düzeltin; çekirdek ayarları son çare ve riskli |

---

## 7. Performans & trade-off

- **TCP vs UDP:** güvenilirlik + sıralama ↔ gecikme + overhead. Tek doğru yok; iş yüküne göre. QUIC ikisinin arasını doldurur (UDP üstünde güvenilir, akış bazlı, 0-RTT).
- **Bağlantı havuzu vs bağlantı başına istek:** havuz handshake/TLS/slow-start maliyetini ortadan kaldırır ama boşta bağlantılar kaynak tutar, uzun ömürlü bağlantılar load balancer'a dengesiz dağılabilir (DNS değişikliğini fark etmez → `PooledConnectionLifetime`, M5.9).
- **Nagle algoritması / `NoDelay`:** küçük yazmaları birleştirip verimlilik kazandırır, ama gecikme ekler. Düşük gecikme isteyen protokoller `TcpClient.NoDelay = true` yapar.
- **Socket tamponları:** `ReceiveBufferSize`/`SendBufferSize` büyütmek yüksek BDP (bandwidth × delay) hatlarda throughput'u artırır; bellek maliyeti vardır. UDP'de küçük tampon = kayıp (bkz. [`LossDemo`](LossDemo.cs)).
- **Backlog:** `listen(backlog)` küçükse ani bağlantı dalgasında istemciler `SYN` aşamasında bekler/düşer.
- **L4 vs L7 LB:** L7 esneklik ve gözlemlenebilirlik verir (path routing, WAF), L4 ham hız ve basitlik.

---

## 8. Kaynaklar

- Microsoft Learn — Sockets in .NET: <https://learn.microsoft.com/dotnet/fundamentals/networking/sockets/>
- `TcpListener` / `TcpClient` / `UdpClient` API referansı: <https://learn.microsoft.com/dotnet/api/system.net.sockets>
- Cloudflare Learning Center — OSI model, TCP/UDP, DDoS temelleri: <https://www.cloudflare.com/learning/ddos/glossary/open-systems-interconnection-model-osi/>
- "Computer Networking: A Top-Down Approach" (Kurose & Ross) — Bölüm 3: Transport Layer
- Beej's Guide to Network Programming (socket API'nin klasik anlatımı): <https://beej.us/guide/bgnet/>
- Vincent Bernat — Coping with the TCP `TIME-WAIT` state on busy Linux servers: <https://vincent.bernat.ch/en/blog/2014-tcp-time-wait-state-linux>
- Microsoft — HttpClient için yönergeler ve socket exhaustion: <https://learn.microsoft.com/dotnet/fundamentals/networking/http/httpclient-guidelines>
- Wireshark kullanıcı kılavuzu: <https://www.wireshark.org/docs/wsug_html_chunked/>
