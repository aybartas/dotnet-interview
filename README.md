# dotnet-interview

.NET Junior / Mid / Senior seviye mülakat hazırlığı için kapsamlı bir çalışma reposu.

**Hedef sürüm:** .NET 10 (LTS) / C# 14

## Nereden başlamalıyım?

Önce **[ROADMAP.md](./ROADMAP.md)** dosyasını okuyun — tüm çalışma planının haritasıdır.
Kendi seviyenize göre ne kadar derinlik beklendiğini görmek için [Seviye Matrisi](./ROADMAP.md#seviye-matrisi) bölümüne bakın.

## Kapsam

| # | Bölüm | İçerik |
|---|-------|--------|
| 0 | Ön Koşullar | Git, HTTP/TLS, .NET CLI, algoritma & veri yapıları |
| 1 | C# Dili | **Program anatomisi** (entry point, erişim belirleyicileri), **tip sistemi & bellek modeli**, OOP, collections, LINQ, modern C# (8→14), Span/Memory, source generators |
| 2 | **Derleme, Build & Çalıştırma** | Roslyn & lowering, **IL/metadata/assembly**, MSBuild, `bin`/`obj`, PDB, **`.exe` nasıl oluşuyor (apphost → hostfxr → CoreCLR)**, **`run` vs `build` vs `publish`**, deployment modelleri (FDD/SCD/single-file/trimmed/AOT), **JIT & tiered compilation** |
| 3 | .NET Platform Servisleri | Sürüm ekosistemi, generic host, **configuration & Options**, **DI**, **middleware pipeline**, filters, logging, background services |
| 4 | Multithreading & Concurrency | Thread/ThreadPool, TPL, async internals, senkronizasyon primitifleri, concurrent collections, Parallel/PLINQ, Channels, cancellation, deadlock & memory model, distributed locking |
| 5 | API Geliştirme | REST tasarımı, Web API, Minimal API/REPR, validation, versioning, OpenAPI, API güvenliği & rate limiting, **gRPC**, **GraphQL**, **SignalR/WebSockets**, HttpClientFactory, serialization, API performansı, BFF/gateway |
| 6 | Veri Erişimi & ORM | ADO.NET, EF Core, Dapper, object mapping, repository/specification |
| 7 | Veritabanları | İlişkisel temeller, SQL Server, PostgreSQL, MongoDB, Redis, sorgu optimizasyonu, transaction & isolation, migration, arama motorları |
| 8 | Test | Unit, mocking, integration, TestContainers, E2E, performans, architecture testing |
| 9 | Mimari & Tasarım | SOLID, design patterns, Clean Architecture, DDD, CQRS, vertical slice, modular monolith |
| 10 | Microservices & Dağıtık | Message broker'lar, event-driven, outbox & saga, API gateway, resilience, CAP/observability |
| 11 | Performans & Bellek | Benchmarking, GC, allocation azaltma, caching, AOT/trimming, profiling |
| 12 | Güvenlik | OWASP Top 10, auth/authz, secrets, kriptografi, supply chain |
| 13 | Docker, K8s & DevOps | Container, Dockerfile, Compose, Kubernetes, CI/CD, IaC |
| 14 | Azure Cloud | App Service, Azure SQL, Cosmos, Storage, Service Bus, Functions, Key Vault, Entra ID, App Insights, Container Apps, .NET Aspire |
| 15 | AI Engineering | LLM temelleri, Microsoft.Extensions.AI, Semantic Kernel, embeddings & vector search, RAG, agents, ML.NET, eval & safety |
| 16 | Sistem Tasarımı | Ölçeklenebilirlik, caching, rate limiting, partitioning, case study'ler |
| 17 | Mülakat Süreci | Coding, live coding, behavioral (STAR), system design, take-home |

### Bölüm 2 neden var?

Çoğu roadmap'te olmayan ama mülakatta kıdemi ayıran sorular:

- C# kodu derlenince ne oluyor? IL nedir, neden var?
- `bin/` ve `obj/` içindeki dosyalar ne işe yarar?
- Kod nasıl `.exe` oluyor? (Spoiler: **olmuyor** — senin kodun `.dll`, `.exe` ayrı bir native launcher)
- `dotnet run` ile `dotnet publish` farkı ne, production'a hangisiyle çıkılır?
- Uygulama başlarken apphost → hostfxr → hostpolicy → CoreCLR zincirinde ne oluyor?
- JIT ne zaman devreye giriyor, tiered compilation nedir, Native AOT ne kazandırır/kaybettirir?

Bu bölüm okunmak için değil **gözlemlenmek** için yazıldı — her başlıkta "aç ve bak" lab'ı var.

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
