# dotnet-interview

.NET Junior → Mid → Senior mülakat hazırlığı için kapsamlı bir çalışma reposu.

**Hedef sürüm:** .NET 10 (LTS) / C# 14 · .NET 11 (Kasım 2026) önizleme özellikleri ayrıca işaretli

## Nereden başlamalıyım?

1. **[ROADMAP.md](./ROADMAP.md)** — tüm çalışma planının haritası.
2. Kendi seviyeni bul: [Seviyeler ve Çıkış Kriterleri](./ROADMAP.md#seviyeler-ve-çıkış-kriterleri).
3. Tüm modüllere tek bakış: [Genel Harita](./ROADMAP.md#genel-harita).
4. Mülakat yakınsa: [Hızlı yol](./ROADMAP.md#çalışma-planı) — ⭐ işaretli konular + 🎯 soruları.

## Yapı: Seviye → Modül → Konu

Roadmap junior konulardan senior konulara **sıralı** ilerler. Aynı alan (veritabanı, async, güvenlik, AI...) her seviyede daha derin bir kesitle tekrar gelir. DSA ayrı bir pisttir ve seviyelerle **paralel** yürür.

| Seviye | Klasör | Modüller | Odak |
|--------|--------|----------|------|
| 🟢 **Junior** (0-2 yıl) | `01-junior/` | J1–J14 + capstone | C# temelleri, ASP.NET Core, Web API, **veritabanı temelleri (ilişkisel model, SQL, index, ACID, transaction, SQL vs NoSQL, PostgreSQL)**, EF Core, test, JWT & OAuth/OIDC'ye giriş, Docker, Azure'a giriş, LLM temelleri |
| 🟡 **Mid** (2-5 yıl) | `02-mid/` | M1–M16 + capstone | C# ve build/runtime derinlemesine, concurrency, API tasarımı (gRPC/GraphQL/SignalR), **PostgreSQL derinlemesine (plan, MVCC, isolation, locking)**, **MongoDB, Redis**, caching, **OAuth 2.1, OIDC, Keycloak**, test stratejisi, mimari, mesajlaşma, observability, Kubernetes, Aspire, Azure, **RAG, MCP, Microsoft Foundry** |
| 🔴 **Senior** (5+ yıl) | `03-senior/` | S1–S10 + capstone | CLR/GC/AOT, performans mühendisliği, dağıtık koordinasyon, **ölçekte veri (storage engine, replication, sharding, CAP)**, DDD & event sourcing, microservices & saga, OWASP 2025 & supply chain, SRE & Azure mimarisi, **Microsoft Agent Framework, MCP/A2A, AI eval & guardrail**, sistem tasarımı |
| 🧮 **DSA** (paralel) | `04-dsa/` | D1–D11 | Karmaşıklık, veri yapıları, graflar, sıralama/arama, backtracking, 18 pattern kataloğu, problem → pattern eşleştirme, DP, sistem tasarımında DSA |
| 🎯 **Mülakat** | `05-interview/` | I1–I6 | Coding & live coding, STAR, system design, take-home, teknik liderlik, seviye bazlı mock mülakat |

Her seviye bir **capstone** projesiyle biter (görev yönetimi API'si → e-ticaret platformu → dağıtık sipariş platformu + AI destek agent'ı).

## Her konu için ne var?

```
<seviye>/<NN-modül>/<NN-konu>/
├── README.md              # kavramsal anlatım + gerçek dünya senaryosu + mülakat perspektifi
├── src/                   # dotnet run ile çalışan bağımsız proje
├── tests/                 # (varsa) konuya ait testler
└── interview-questions.md # 10-20 mülakat sorusu + cevabı
```

Roadmap'te her konunun başlığında klasör adı ve `🛠 Kod` satırında yazılacak projenin kapsamı bulunur. Detaylı klasör yapısı: [ROADMAP.md → Repo Yapısı](./ROADMAP.md#repo-yapısı).

## Kullanım

Konunun `src/` klasörüne girip:

```bash
dotnet run
```

Altyapı gerektiren modüllerde (veritabanı, Redis, Keycloak, message broker) modül klasöründeki ortak compose dosyası kullanılır:

```bash
docker compose up -d   # modül klasöründe
cd <konu>/src
dotnet run
```

## İşaretler

| İşaret | Anlamı |
|--------|--------|
| ⭐ | Mülakatlarda çok sık sorulan klasik konu |
| 🛠 **Kod** | Konunun `src/` klasöründe yazılacak proje |
| 🎯 **Mülakat** | Modül sonunda cevaplanabilmesi gereken sorular |
| **(.NET 11)** | Kasım 2026'da gelen / önizlemedeki özellik |
| 🟢 🟡 🔴 | Junior / Mid / Senior |

## Güncel kalmak

.NET, Azure ve AI ekosistemi hızlı değişiyor (lisans değişiklikleri, Agent Framework, Microsoft Foundry, sertifika güncellemeleri...). Özet: [ROADMAP.md → 2026 Radarı](./ROADMAP.md#2026-radarı--neler-değişti).
