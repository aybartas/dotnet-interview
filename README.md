# dotnet-interview

.NET Junior / Mid / Senior seviye mülakat hazırlığı için kapsamlı bir çalışma reposu.

**Hedef sürüm:** .NET 10 (LTS) / C# 14

## Nereden başlamalıyım?

Önce **[ROADMAP.md](./ROADMAP.md)** dosyasını okuyun — tüm çalışma planının haritasıdır.
Kendi seviyenize göre ne kadar derinlik beklendiğini görmek için [Seviye Matrisi](./ROADMAP.md#seviye-matrisi) bölümüne bakın.

## Kapsam

| # | Bölüm | İçerik |
|---|-------|--------|
| 0 | Ön Koşullar & DSA | Git, HTTP/TLS, .NET CLI, **veri yapıları derinlemesine, 18 pattern kataloğu, problem→pattern eşleştirme rehberi, DP** |
| 1 | C# Dili | **Program anatomisi** (entry point, erişim belirleyicileri), **tip sistemi & bellek modeli**, OOP, collections, LINQ, modern C# (8→14), Span/Memory, source generators |
| 2 | **Derleme, Build & Çalıştırma** | Roslyn & lowering, **IL/metadata/assembly**, MSBuild, `bin`/`obj`, PDB, **`.exe` nasıl oluşuyor (apphost → hostfxr → CoreCLR)**, **`run` vs `build` vs `publish`**, deployment modelleri (FDD/SCD/single-file/trimmed/AOT), **JIT & tiered compilation** |
| 3 | .NET Platform Servisleri | Sürüm ekosistemi, generic host, **configuration & Options**, **DI**, **middleware pipeline**, filters, logging, background services |
| 4 | Multithreading & Concurrency | Thread/ThreadPool, TPL, async internals, senkronizasyon primitifleri, concurrent collections, Parallel/PLINQ, Channels, cancellation, deadlock & memory model, distributed locking |
| 5 | API Geliştirme | REST tasarımı, Web API, Minimal API/REPR, validation, versioning, OpenAPI, API güvenliği & rate limiting, **gRPC**, **GraphQL**, **SignalR/WebSockets**, HttpClientFactory, serialization, API performansı, BFF/gateway |
| 6 | Veri Erişimi & ORM | ADO.NET, EF Core, Dapper, object mapping, repository/specification |
| 7 | Veritabanları | İlişkisel temeller, SQL Server, PostgreSQL, MongoDB, Redis, sorgu optimizasyonu, transaction & isolation, migration, arama motorları |
| 8 | Test | Unit, mocking, integration, TestContainers, E2E, performans, architecture testing |
| 9 | Mimari & Tasarım | SOLID, design patterns, DDD, CQRS, vertical slice, modular monolith, **hexagonal/onion/clean karşılaştırması, mimari stil karar tablosu, AI-driven development & agentic engineering** |
| 10 | Microservices & Dağıtık | Message broker'lar, event-driven, outbox & saga, API gateway, resilience, CAP |
| 11 | **Observability** | **Logging/metrics/tracing üçlüsü, structured logging, OpenTelemetry, distributed tracing, APM araçları, production teşhis metodolojisi** |
| 12 | Performans & Bellek | Benchmarking, GC, allocation azaltma, caching, AOT/trimming, profiling |
| 13 | Güvenlik | OWASP Top 10 (.NET önlemli), **yaygın açık → önlem referans tablosu (XSS/CSRF/XXE/SSRF/...)**, **güvenli kod yazma ilkeleri**, auth/authz, secrets, kriptografi, supply chain |
| 14 | Docker, K8s & DevOps | Container, Dockerfile, Compose, Kubernetes, CI/CD, IaC |
| 15 | Azure Cloud | App Service, Azure SQL, Cosmos, Storage, Service Bus, Functions, Key Vault, Entra ID, App Insights, Container Apps, .NET Aspire |
| 16 | AI Engineering | LLM temelleri, Microsoft.Extensions.AI, Semantic Kernel, embeddings & vector search, RAG, agents, ML.NET, eval & safety |
| 17 | Sistem Tasarımı | Ölçeklenebilirlik, caching, rate limiting, partitioning, case study'ler |
| 18 | Mülakat Süreci | Coding, live coding, behavioral (STAR), system design, take-home |

### Bölüm 2 neden var?

Çoğu roadmap'te olmayan ama mülakatta kıdemi ayıran sorular:

- C# kodu derlenince ne oluyor? IL nedir, neden var?
- `bin/` ve `obj/` içindeki dosyalar ne işe yarar?
- Kod nasıl `.exe` oluyor? (Spoiler: **olmuyor** — senin kodun `.dll`, `.exe` ayrı bir native launcher)
- `dotnet run` ile `dotnet publish` farkı ne, production'a hangisiyle çıkılır?
- Uygulama başlarken apphost → hostfxr → hostpolicy → CoreCLR zincirinde ne oluyor?
- JIT ne zaman devreye giriyor, tiered compilation nedir, Native AOT ne kazandırır/kaybettirir?

Bu bölüm okunmak için değil **gözlemlenmek** için yazıldı — her başlıkta "aç ve bak" lab'ı var.

### Bölüm 0'daki DSA neden bu kadar geniş?

Sadece "Array, List, Dictionary" listesi değil — **mülakatta fark yaratan asıl şey**:

- 18 pattern'lik katalog (two pointers, sliding window, backtracking, heap, union-find, monotonic stack...)
- "Problemde bunu görürsen, şu pattern'i kullan" eşleştirme tablosu
- Her veri yapısının **gerçek dünyada nerede kullanıldığı** (LRU cache = hash map + doubly linked list, trie = autocomplete, union-find = Kruskal MST)
- UMPIRE mülakat çözüm stratejisi (Understand → Match → Plan → Implement → Review → Evaluate)

### Yeni: Mimari stilleri + AI-driven development (Bölüm 9)

- Hexagonal vs Onion vs Clean Architecture — **aynı fikrin farklı isimlendirmeleri**, net karşılaştırma
- Microservices vs Modular Monolith vs SOA vs Event-Driven vs Serverless vs Space-Based — karar tablosu
- "Vibe coding" ile AI-driven engineering farkı, agentic coding workflow'ları, Claude Code kurumsal yapılandırması (CLAUDE.md, MCP, subagents, permission mode), AI özelliklerini ürüne entegre etme (RAG/vector DB/LLM)

### Yeni: Observability (Bölüm 11)

Logging tek başına yetmez — log + metric + trace üçlüsünün birlikte nasıl çalıştığı, OpenTelemetry mimarisi, RED/USE metodları, production teşhis metodolojisi.

### Genişletilmiş: Güvenlik (Bölüm 13)

OWASP Top 10'un her maddesi için somut .NET önlemi + 16 satırlık "açık → nasıl oluşur → .NET'te nasıl engellenir" referans tablosu (SQLi, XSS, CSRF, XXE, SSRF, IDOR, ReDoS, mass assignment...) + güvenli kod yazma ilkeleri.

## Her konu için ne var?

```
<bölüm>/<konu>/
├── README.md              # kavramsal anlatım + gerçek dünya senaryosu + mülakat perspektifi
├── src/                   # dotnet run ile çalışan proje
└── interview-questions.md # 10-20 mülakat sorusu + cevabı
```

Detaylı klasör yapısı için [ROADMAP.md → Repo Yapısı](./ROADMAP.md#repo-yapısı).

## Kullanım

Konunun `src/` klasörüne girip:

```bash
dotnet run
```

Bazı konular (veritabanı, Redis, message broker) Docker gerektirir:

```bash
docker compose up -d
dotnet run
```

## Seviye rozetleri

- 🟢 **Junior** (0-2 yıl) — bilinmesi zorunlu
- 🟡 **Mid** (2-5 yıl) — uygulamada kullanılmış olmalı
- 🔴 **Senior** (5+ yıl) — trade-off'ları bilinen, production deneyimi
