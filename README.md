# dotnet-interview

.NET Junior → Mid → Senior mülakat hazırlığı için kapsamlı bir çalışma reposu.

**Hedef sürüm:** .NET 10 (LTS) / C# 14 · .NET 11 (Kasım 2026) önizleme özellikleri **(.NET 11)** ile işaretli

---

## Şu an repoda ne var?

Bu repo **plan önce, kod sonra** yaklaşımıyla ilerliyor. Şu anki durum:

| Dosya | Ne işe yarar | Durum |
|-------|--------------|-------|
| **[ROADMAP.md](./ROADMAP.md)** | ~4.200 satırlık ana plan: 5 pist, 51 modül, ~290 konu. **Her konunun teorik içerik dökümü ve yazılacak kod projesinin kapsamı burada.** | ✅ Tamamlandı |
| **README.md** | Nasıl çalışılır, klasör yapısı, teori/pratik nerede | ✅ Bu dosya |
| **CLAUDE.md** | Claude Code için repo kuralları (dil, yapı, tutarlılık) | ✅ |
| `01-junior/` … `05-interview/` | Konu klasörleri: anlatım + çalıştırılabilir kod | ⏳ **Henüz yok** — her konu yazıldıkça oluşuyor |

> **Önemli:** Klasörleri repoda göremiyorsan bu bir hata değil. ROADMAP konu konu işlenerek hayata geçiriliyor; aşağıdaki [klasör yapısı](#klasör-yapısı) hedef yapıdır. Bugün çalışmaya başlamak için ROADMAP yeterli — konu maddeleri zaten neyi öğrenmen gerektiğini satır satır söylüyor.

---

## Nereden başlamalıyım?

1. **Seviyeni belirle:** [Seviyeler ve Çıkış Kriterleri](./ROADMAP.md#seviyeler-ve-çıkış-kriterleri) — her seviyenin "bunu yapabiliyorsan geçtin" ölçütü var.
2. **Haritaya bak:** [Genel Harita](./ROADMAP.md#genel-harita) — 51 modül tek tabloda.
3. **Takvimini kur:** [Çalışma Planı](./ROADMAP.md#çalışma-planı) — seviye bazlı haftalık plan + paralel DSA kolonu.
4. **Mülakat yakınsa:** aynı bölümdeki **⚡ Hızlı yol** — sadece ⭐ konuları tara, 🎯 sorularını kapalı kitap cevapla.

---

## Yapı: Seviye → Modül → Konu

```
Seviye   (Junior / Mid / Senior / DSA / Mülakat)
  └── Modül   (ör. J8 Veritabanı Temelleri)        → bir klasör
        └── Konu   (ör. J8.4 Transaction & ACID)   → bir alt klasör = bir bağımsız kod projesi
```

Seviyeler **sıralıdır**; aynı alan her seviyede daha derin bir kesitle tekrar gelir (veritabanı: J8 → M6/M8 → S4). DSA ayrı bir pisttir ve seviyelerle **paralel** yürür.

| Seviye | Klasör | Modüller | Odak |
|--------|--------|----------|------|
| 🟢 **Junior** (0-2 yıl) | `01-junior/` | J1–J14 + capstone | **Ağ temelleri (OSI, TCP/UDP, socket)**, HTTP/DNS/TLS, C# temelleri, **OOP ilkeleri & nesne ilişkileri**, **property/constructor/eşitlik/kopyalama semantiği**, ASP.NET Core, Web API, **veritabanı temelleri (ilişkisel model, SQL, index, ACID, transaction, SQL vs NoSQL, PostgreSQL)**, EF Core, test, JWT & OAuth/OIDC'ye giriş, Docker, Azure'a giriş (AZ-900), LLM temelleri |
| 🟡 **Mid** (2-5 yıl) | `02-mid/` | M1–M16 + capstone | C# ve build/runtime derinlemesine, concurrency, API tasarımı (gRPC/GraphQL/SignalR) ve **protokol/transport derinlemesine (HTTP/2, HTTP/3, QUIC, TLS, mTLS)**, **PostgreSQL derinlemesine (plan, MVCC, isolation, locking)**, **MongoDB, Redis**, caching, **OAuth 2.1, OIDC, Keycloak**, test stratejisi, mimari, **mesajlaşma (RabbitMQ, Service Bus, Kafka)**, observability, CI/CD, **container ileri & Kubernetes (workload'lar, storage, probe'lar)**, Aspire, Azure (AI-200), **RAG, MCP, Microsoft Foundry** |
| 🔴 **Senior** (5+ yıl) | `03-senior/` | S1–S10 + capstone | CLR/GC/AOT, performans mühendisliği, dağıtık koordinasyon, **ölçekte veri (storage engine, replication, sharding, CAP)**, DDD & event sourcing, microservices & saga, OWASP 2025 & supply chain, SRE & Azure mimarisi, **Microsoft Agent Framework, MCP/A2A, AI eval & guardrail**, sistem tasarımı |
| 🧮 **DSA** (paralel) | `04-dsa/` | D1–D11 | Karmaşıklık, veri yapıları, graflar, sıralama/arama, backtracking, 18 pattern kataloğu, problem → pattern eşleştirme, DP, sistem tasarımında DSA |
| 🎯 **Mülakat** | `05-interview/` | I1–I6 | Coding & live coding, STAR, system design, take-home, teknik liderlik, seviye bazlı mock mülakat |

Her seviye bir **capstone** projesiyle biter: görev yönetimi API'si → e-ticaret katalog & sipariş platformu → dağıtık sipariş platformu + AI destek agent'ı. Capstone'lar portföy parçasıdır; mülakatta anlatacağın proje bunlar olur.

---

## Klasör yapısı

### Üst seviye

```
dotnet-interview/
├── ROADMAP.md                  # ana plan (teorik içerik dökümü burada)
├── README.md
├── CLAUDE.md
├── Directory.Build.props       # ortak ayarlar: net10.0, nullable, analyzer'lar
├── Directory.Packages.props    # Central Package Management — tek yerden paket sürümü
│
├── 01-junior/                  # J1–J14 + capstone
├── 02-mid/                     # M1–M16 + capstone
├── 03-senior/                  # S1–S10 + capstone
├── 04-dsa/                     # D1–D11
└── 05-interview/               # I1–I6
```

Modül klasörlerinin tam listesi: [ROADMAP.md → Repo Yapısı](./ROADMAP.md#repo-yapısı).

### Bir modül ve konu klasörü

Klasör adları **uydurulmaz** — ROADMAP'teki başlıkta yazar. Örnek olarak şu konu başlığı:

```markdown
#### J8.4 Transaction & ACID · `04-transactions-acid` ⭐
```

modülün `📁 01-junior/08-database-fundamentals/` satırıyla birleşince şu yola karşılık gelir:

```
01-junior/
└── 08-database-fundamentals/        # modül = ### J8 başlığının 📁 satırındaki klasör
    ├── docker-compose.yml           # modülün ortak altyapısı (PostgreSQL, Redis, Mongo…)
    ├── 01-relational-modeling/
    ├── 02-sql-basics/
    ├── 03-index-basics/
    └── 04-transactions-acid/        # konu = #### J8.4 başlığındaki klasör adı
        ├── README.md                # TEORİ — kavramsal anlatım
        ├── src/                     # PRATİK — `dotnet run` ile çalışan bağımsız proje
        ├── tests/                   # (varsa) konuya ait testler
        └── interview-questions.md   # 10-20 soru + cevap
```

**Kurallar:**
- Her konu **bağımsız** bir projedir — konular arası proje referansı yoktur. Bir konuyu tek başına açıp çalıştırabilmelisin.
- Namespace klasörden türer: `Junior.DatabaseFundamentals.TransactionsAcid`
- Altyapı (veritabanı, Redis, Keycloak, broker) **modül kökünde** tek bir `docker-compose.yml` ile paylaşılır; her konu kendi compose'unu açmaz.
- Capstone'ların kendi `.slnx` çözüm dosyası vardır.

---

## Teorik ve pratik anlatımlar nerede?

| Ne arıyorsun | Nerede |
|--------------|--------|
| **Teori — ne öğrenmeliyim?** | ROADMAP'teki konu maddeleri. Her madde bir alt başlık; `⭐` işaretliler mülakatta en sık sorulanlar. |
| **Teori — detaylı anlatım** | Konu klasöründeki **`README.md`** (konu yazıldığında). Sabit şablon: *Ne? → Neden? → Nasıl? → Gerçek dünya senaryosu → Mülakat perspektifi → Yaygın hatalar → Performans & trade-off → Kaynaklar* |
| **Pratik — ne kodlayacağım?** | ROADMAP'te her konunun sonundaki **🛠 Kod** satırı. Yazılacak projenin kapsamını tanımlar — `src/` tam olarak bunu uygular. |
| **Pratik — çalışan kod** | Konu klasöründeki **`src/`** (ve varsa `tests/`). |
| **Mülakat soruları** | Modül sonundaki **🎯 Mülakat** satırı (hızlı kontrol) + konu klasöründeki **`interview-questions.md`** (cevaplı). |
| **Altyapı nasıl kalkar** | Modül kökündeki `docker-compose.yml`. |
| **Hangi hafta neyi çalışmalıyım** | [Çalışma Planı](./ROADMAP.md#çalışma-planı) — seviye bazlı haftalık tablo + paralel DSA. |
| **Bu bilgi güncel mi** | [2026 Radarı](./ROADMAP.md#2026-radarı--neler-değişti) — sürüm, lisans ve sertifika değişiklikleri kaynak linkleriyle. |

**Özetle:** *ne* öğreneceğin ve *ne* kodlayacağın **ROADMAP.md**'de; *nasıl* olduğunun uzun anlatımı konu klasörünün **`README.md`**'sinde; çalışan hali **`src/`**'de.

---

## Nasıl çalışılır?

### Konu döngüsü

1. **Planı aç** — [Çalışma Planı](./ROADMAP.md#çalışma-planı)'ndan bu haftanın modülünü ve paralel DSA konusunu al.
2. **Teoriyi oku** — konunun ROADMAP maddelerini, sonra (varsa) konu klasörünün `README.md`'sini.
3. **Kodu kendin yaz** — `🛠 Kod` satırındaki kapsamı `src/` içinde uygula. **Kopyalama.** Okumak öğrenmek değildir; AI asistanını açıklama ve review için kullan, yerine yazdırma (→ J14.2).
4. **Çalıştır ve kır** — çalıştığını gör, sonra bilerek boz: limiti düşür, bağlantıyı kes, yanlış config ver. Çoğu konuda ROADMAP'in `🛠 Kod` satırı bunu zaten istiyor ("bilerek bozuk deploy yap ve teşhis et" gibi).
5. **Kapalı kitap sına** — `interview-questions.md`'yi ve modül sonundaki 🎯 sorularını **önce cevapla**, sonra kontrol et. Takıldıklarını not al.

### Ritim

- **Haftada 1 gün tekrar** — not aldığın takıldıkların üzerinden geç.
- **DSA paralel** — haftada 2-3 gün DSA pisti, kalan günler seviye modülleri.
- **Ayda 1 mock mülakat** — seviyene uygun senaryo (→ [I6](./ROADMAP.md#i6-mock-mülakat-senaryoları--checklist)).
- **Seviyeyi capstone ile kapat** — modülleri tek projede birleştirmeden sonraki seviyeye geçme.

### Komutlar

```bash
# Altyapı gerektiren modüllerde önce modül kökünde:
cd 01-junior/08-database-fundamentals
docker compose up -d

# Sonra konunun projesinde:
cd 04-transactions-acid/src
dotnet run

# Testi olan konularda:
cd ../tests && dotnet test
```

---

## Yeni bir konu yazarken

Repo konu konu dolduğu için bu akış en sık yapılan iş:

1. ROADMAP'te konuyu bul; başlıktaki klasör adını ve modülün `📁` satırındaki yolu kullan — **isim uydurma**.
2. Klasörü aç, dört parçayı oluştur: `README.md` (şablona göre), `src/`, (gerekiyorsa) `tests/`, `interview-questions.md`.
3. `README.md`'yi konunun ROADMAP maddelerinden yaz — maddeler içindekiler listesi gibi çalışır.
4. `src/`'yi **`🛠 Kod` satırındaki kapsama göre** yaz; başka konuya proje referansı verme.
5. `interview-questions.md`'ye modülün 🎯 satırındaki soruları + konuya özel 10-20 soruyu cevaplarıyla ekle.
6. Modül altyapı istiyorsa modül kökündeki `docker-compose.yml`'a ekle (yeni compose dosyası açma).
7. Dil: **Türkçe anlatım, İngilizce teknik terim** — ROADMAP'teki üslubun aynısı.

ROADMAP'in kendisini değiştiriyorsan (konu ekleme/taşıma/yeniden numaralandırma) uyulması gereken tutarlılık kuralları **[CLAUDE.md](./CLAUDE.md)**'de listelidir.

---

## Azure sertifika hedefleri

İki sertifika roadmap'e gömülü — ilgili modüller sınavın alanlarına birebir eşlenmiş durumda:

| Sertifika | Modül | Kapsam |
|-----------|-------|--------|
| **AZ-900** — Azure Fundamentals | [J13](./ROADMAP.md#j13-cloud--azurea-giriş) | Cloud kavramları (%25-30) · Azure mimarisi & servisleri (%35-40) · yönetişim, maliyet & monitoring (%30-35) |
| **AI-200** — Azure AI Cloud Developer Associate | [M15](./ROADMAP.md#m15-azure-ile-uygulama-geliştirme) (+ [M16](./ROADMAP.md#m16-ai-engineering-uygulama-geliştirme)) | Container çözümleri (%20-25) · veri servisleri & vector search (%25-35) · servis entegrasyonu (%20-25) · güvenlik/izleme/teşhis (%20-25) |

AZ-204 31 Temmuz 2026'da emekliye ayrıldı; geliştirici yolu artık AI-200. Müfredatlar tarihli olarak güncellenir — sınav öncesi resmî study guide'ı kontrol edin ([kaynaklar](./ROADMAP.md#kaynaklar)).

---

## İşaretler

| İşaret | Anlamı |
|--------|--------|
| ⭐ | Mülakatlarda **çok sık** sorulan klasik konu |
| 🛠 **Kod** | Konunun `src/` klasöründe yazılacak projenin kapsamı |
| 🎯 **Mülakat** | Modül sonunda cevaplayabilmen gereken sorular |
| 📜 | Sertifika kapsamı notu (AZ-900 / AI-200) |
| ⏱ | Modülün tahmini süresi |
| **(.NET 11)** | Kasım 2026'da gelen / önizlemedeki özellik — bil ama production'da .NET 10 varsay |
| 🟢 🟡 🔴 | Junior / Mid / Senior |
| `(→ M6.3)` | "Bu konunun derin hali M6.3'te" — çapraz referans |

---

## Güncel kalmak

.NET, Azure ve AI ekosistemi hızlı değişiyor (lisans değişiklikleri, Agent Framework, Microsoft Foundry, sertifika güncellemeleri, RabbitMQ/Redis sürümleri...). Özet ve kaynak linkleri: **[ROADMAP.md → 2026 Radarı](./ROADMAP.md#2026-radarı--neler-değişti)** — 3 ayda bir gözden geçirilmeli.
