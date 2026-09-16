# .NET Interview Hazırlık Roadmap'i

> Junior → Mid → Senior seviye .NET geliştirici mülakatlarına hazırlık için kapsamlı yol haritası.
> Her başlık için ayrı bir Markdown dokümanı ve **çalıştırılabilir kod örnekleri** oluşturulacaktır.

## İçindekiler

- [Roadmap Felsefesi ve Kullanım](#roadmap-felsefesi-ve-kullanım)
- [Repo Yapısı](#repo-yapısı)
- [1. Junior Level](#1-junior-level)
- [2. Mid Level](#2-mid-level)
- [3. Senior Level](#3-senior-level)
- [4. Veritabanları](#4-veritabanları)
- [5. Docker & Container Ekosistemi](#5-docker--container-ekosistemi)
- [6. Azure Cloud](#6-azure-cloud)
- [7. AI Engineering (.NET tarafında)](#7-ai-engineering-net-tarafında)
- [8. Sistem Tasarımı ve Mimari](#8-sistem-tasarımı-ve-mimari)
- [9. Mülakat Süreci ve Soft Skills](#9-mülakat-süreci-ve-soft-skills)
- [Öğrenme Sırası ve Süreleri](#öğrenme-sırası-ve-süreleri)
- [Referans Kaynaklar](#referans-kaynaklar)

---

## Roadmap Felsefesi ve Kullanım

Bu repo klasik "listeleme" bir roadmap değildir. Her konu için üç katman vardır:

1. **Kavramsal `.md` dokümanı** — konunun teorisi, mülakatta sorulan şekli, tuzakları.
2. **Çalıştırılabilir kod örnekleri** — `dotnet run` ile hemen çalışan, tek başına anlamlı projeler.
3. **Gerçek dünya senaryosu** — bu konunun bir üretim uygulamasında hangi problemi çözdüğü.

**Seviye kodlaması:**
- 🟢 Junior — 0-2 yıl deneyim; bilinmesi zorunlu
- 🟡 Mid — 2-5 yıl deneyim; iyi seviyede kavranmış olmalı
- 🔴 Senior — 5+ yıl deneyim; deneyimle içselleştirilmiş, trade-off'ları bilinen

Bir konu üst seviyede tekrar ediyorsa, o seviyede **daha derin** bir kesitiyle ele alınacak demektir.

---

## Repo Yapısı

```
dotnet-interview/
├── ROADMAP.md                          # bu dosya
├── README.md
├── 01-junior/
│   ├── 01-csharp-fundamentals/
│   │   ├── README.md                   # kavramsal anlatım
│   │   ├── src/                        # çalışan .NET projesi
│   │   └── interview-questions.md
│   ├── 02-oop/
│   ├── 03-collections/
│   ├── 04-exception-handling/
│   ├── 05-linq-basics/
│   ├── 06-async-await-intro/
│   ├── 07-file-io/
│   └── 08-unit-testing-basics/
├── 02-mid/
│   ├── 01-solid/
│   ├── 02-design-patterns/
│   ├── 03-async-advanced/
│   ├── 04-aspnetcore-webapi/
│   ├── 05-entity-framework-core/
│   ├── 06-dependency-injection/
│   ├── 07-middleware-filters/
│   ├── 08-authentication-authorization/
│   ├── 09-caching/
│   ├── 10-testing-advanced/
│   └── 11-logging-observability/
├── 03-senior/
│   ├── 01-clean-architecture/
│   ├── 02-ddd/
│   ├── 03-cqrs-mediatr/
│   ├── 04-event-driven/
│   ├── 05-microservices/
│   ├── 06-message-brokers/
│   ├── 07-performance-tuning/
│   ├── 08-memory-management-gc/
│   ├── 09-concurrency-primitives/
│   ├── 10-distributed-systems/
│   └── 11-security-deep/
├── 04-databases/
│   ├── 01-relational-fundamentals/
│   ├── 02-sql-server/
│   ├── 03-postgresql/
│   ├── 04-mongodb/
│   ├── 05-query-optimization/
│   ├── 06-transactions-isolation/
│   ├── 07-nosql-patterns/
│   └── 08-migrations/
├── 05-docker/
│   ├── 01-container-basics/
│   ├── 02-dockerfile-dotnet/
│   ├── 03-docker-compose/
│   ├── 04-multi-stage-builds/
│   └── 05-kubernetes-intro/
├── 06-azure/
│   ├── 01-app-service/
│   ├── 02-azure-sql/
│   ├── 03-cosmos-db/
│   ├── 04-blob-storage/
│   ├── 05-service-bus/
│   ├── 06-functions/
│   ├── 07-key-vault/
│   ├── 08-azure-ad/
│   ├── 09-application-insights/
│   └── 10-devops-pipelines/
├── 07-ai/
│   ├── 01-semantic-kernel-basics/
│   ├── 02-openai-integration/
│   ├── 03-embeddings-vector-search/
│   ├── 04-rag-pipeline/
│   ├── 05-ml-net/
│   └── 06-ai-agent-with-dotnet/
├── 08-system-design/
│   ├── 01-scalability/
│   ├── 02-caching-strategies/
│   ├── 03-rate-limiting/
│   ├── 04-api-gateway/
│   └── 05-case-studies/
└── 09-interview/
    ├── behavioral-questions.md
    ├── system-design-drills.md
    ├── coding-challenges/
    └── mock-interview-scripts.md
```

Her klasördeki `README.md` şu şablonu kullanacak:

```markdown
# <Konu>

## Ne?
## Neden?
## Nerede kullanılır?
## Mülakat perspektifi
## Kod örnekleri (src/)
## Yaygın hatalar / tuzaklar
## Kaynaklar
```

---

## 1. Junior Level

**Hedef:** Bir .NET junior developer'ın "işi teslim edebilmesi" için gereken temel bilgi ve reflex'ler. Mülakatın ilk teknik turunu geçecek seviye.

### 1.1 C# Fundamentals 🟢
- Value type vs reference type (stack/heap)
- `string` immutability, `StringBuilder`
- Nullable types, `?.`, `??`, `??=`
- Boxing / unboxing
- `var`, `dynamic`, `object` farkları
- Pattern matching (C# 8+), switch expressions
- `record`, `record struct` (C# 9/10)
- Top-level statements, file-scoped namespace
- Global usings, implicit usings

**Örnek kod:** `01-junior/01-csharp-fundamentals/src/` — `dotnet run` ile her kavramın çıktısını basan console app.

### 1.2 OOP (Nesne Yönelimli Programlama) 🟢
- Encapsulation, Inheritance, Polymorphism, Abstraction
- `abstract` vs `interface` — ne zaman hangisi
- `sealed`, `virtual`, `override`, `new` keyword'leri
- Method overloading vs overriding
- Interface default implementations (C# 8+)
- Composition over inheritance

### 1.3 Collections & Generics 🟢
- `List<T>`, `Dictionary<TKey,TValue>`, `HashSet<T>`, `Queue<T>`, `Stack<T>`
- `IEnumerable<T>` vs `ICollection<T>` vs `IList<T>` vs `IReadOnlyList<T>`
- `Array` vs `List<T>` performans farkı
- `foreach` altında ne oluyor (`IEnumerator`)
- Generic constraint'ler (`where T : class, new()`)
- Big-O açısından karşılaştırma

### 1.4 Exception Handling 🟢
- `try` / `catch` / `finally` / `using`
- Custom exception yazımı
- Exception filter (`when`)
- `throw` vs `throw ex` (stack trace!)
- Ne zaman exception atılmalı, ne zaman geri dönüş kodu

### 1.5 LINQ Basics 🟢
- Query syntax vs method syntax
- Deferred vs immediate execution
- `Select`, `Where`, `GroupBy`, `Join`, `OrderBy`, `First/FirstOrDefault`, `Any/All`
- `IEnumerable` (LINQ to Objects) vs `IQueryable` (LINQ to SQL/EF)
- Yaygın hata: N+1 query, `ToList()` yerinin önemi

### 1.6 Async / Await (giriş) 🟢
- Neden async? (thread bloklamamak)
- `Task`, `Task<T>`, `ValueTask`
- `async void` neden kötü
- `.Result` / `.Wait()` deadlock riski
- `ConfigureAwait(false)` — ne zaman

### 1.7 File I/O 🟢
- `System.IO.File`, `FileStream`, `StreamReader/Writer`
- `using` deyimi ve `IDisposable`
- Path manipulation (`Path.Combine`, cross-platform yolları)
- Async I/O (`File.ReadAllTextAsync`)

### 1.8 Unit Testing Basics 🟢
- xUnit temel yapısı (`Fact`, `Theory`, `InlineData`)
- Arrange-Act-Assert pattern
- `FluentAssertions` kullanımı
- Test isimlendirme (`MethodName_Scenario_Expected`)
- Test coverage kavramı

**Junior mülakat çıktısı:** FizzBuzz, palindrome check, string reverse, anagram, fibonacci, basit CRUD API yazabilme.

---

## 2. Mid Level

**Hedef:** Bir mid-level developer'ın uygulama tasarlayabilmesi, kod incelemesi yapabilmesi ve production kalitesinde .NET Core Web API teslim edebilmesi.

### 2.1 SOLID Prensipleri 🟡
- **S**ingle Responsibility — sınıfın tek bir değişme sebebi olması
- **O**pen/Closed — genişlemeye açık, değişikliğe kapalı
- **L**iskov Substitution — alt tip üst tip yerine geçebilmeli
- **I**nterface Segregation — küçük odaklı interface'ler
- **D**ependency Inversion — abstraction'a bağımlılık

Her prensip için **kötü** ve **iyi** örnek karşılaştırması + refactor hikayesi.

### 2.2 Design Patterns 🟡
- **Creational:** Singleton (thread-safe implementasyonu), Factory, Abstract Factory, Builder
- **Structural:** Adapter, Decorator, Facade, Proxy
- **Behavioral:** Strategy, Observer, Mediator, Chain of Responsibility, Command, Template Method
- **.NET-özel:** Options Pattern, Repository, Unit of Work, Specification

### 2.3 Async / Await (ileri) 🟡
- `SynchronizationContext` ve deadlock analizi
- `Task.WhenAll`, `Task.WhenAny`, `Task.Run`
- `CancellationToken` doğru kullanımı
- `IAsyncEnumerable<T>` ve `await foreach`
- `Parallel.ForEachAsync`
- Thread-safe collections (`ConcurrentDictionary`, `Channel<T>`)

### 2.4 ASP.NET Core Web API 🟡
- Minimal API vs Controller-based API
- Model binding, model validation (`DataAnnotations`, FluentValidation)
- Content negotiation, `Accept` / `Content-Type`
- Route templates, attribute routing
- `IActionResult` vs `ActionResult<T>` vs `Results.Ok()`
- API versioning
- OpenAPI / Swagger

### 2.5 Entity Framework Core 🟡
- Code-first vs Database-first
- Migrations (`dotnet ef migrations add/update`)
- `DbContext` lifetime (scoped)
- Change tracking, `AsNoTracking()`
- Eager (`Include`), Lazy, Explicit loading
- Fluent API vs Data Annotations
- Global query filters (soft delete, multi-tenancy)
- N+1 problem, tespit ve çözüm

### 2.6 Dependency Injection 🟡
- Built-in DI container
- Singleton / Scoped / Transient — hangisi ne zaman
- Captive dependency problemi
- `IServiceProvider`, `IServiceScopeFactory`
- Named services (keyed services — .NET 8+)
- 3rd party container (Autofac, Scrutor)

### 2.7 Middleware & Filters 🟡
- Request pipeline, sırası neden önemli
- Custom middleware yazımı (`InvokeAsync`)
- Global exception handling middleware
- `IActionFilter`, `IExceptionFilter`, `IResourceFilter`, `IResultFilter`
- Middleware vs Filter — hangisi ne zaman

### 2.8 Authentication & Authorization 🟡
- Cookie authentication
- JWT Bearer token flow
- OAuth 2.0 / OpenID Connect temelleri
- Policy-based authorization, requirements & handlers
- Role-based vs Claim-based
- `[Authorize(Roles = "...")]`, `[Authorize(Policy = "...")]`
- Refresh token stratejileri

### 2.9 Caching 🟡
- `IMemoryCache` (in-process)
- `IDistributedCache` (Redis)
- HTTP response caching, ETag
- Cache-aside, write-through, write-behind pattern'leri
- Cache stampede, cache invalidation problemleri
- Output caching (.NET 7+)

### 2.10 Testing (ileri) 🟡
- Unit test vs Integration test vs E2E
- `WebApplicationFactory<T>` ile integration test
- Test containers (Postgres, SQL Server container ile gerçek DB testi)
- Mocking: Moq, NSubstitute
- Snapshot testing (Verify)
- Coverage araçları (Coverlet)

### 2.11 Logging & Observability 🟡
- `ILogger<T>`, structured logging
- Log level'ları ne için kullanılır
- Serilog + sink'ler (Console, Seq, File, Elasticsearch)
- Correlation ID, distributed tracing (OpenTelemetry)
- Metrics (Prometheus format), health checks

**Mid mülakat çıktısı:** Katmanlı bir Web API tasarlayabilme, EF Core sorgu optimizasyonu, JWT'li endpoint güvenliği, Redis ile caching, container'da çalıştırma.

---

## 3. Senior Level

**Hedef:** Sistem tasarlayan, ekip yönlendiren, üretim problemlerini kök nedenine kadar takip edebilen developer.

### 3.1 Clean Architecture 🔴
- Katmanlar: Domain, Application, Infrastructure, Presentation
- Dependency Rule (bağımlılıklar içeriye doğru)
- Use case (Application layer) driven design
- Ports & Adapters (Hexagonal architecture)
- Trade-off: klasik n-tier vs clean architecture

### 3.2 Domain-Driven Design (DDD) 🔴
- Ubiquitous Language
- Entity, Value Object, Aggregate, Aggregate Root
- Domain Event, Domain Service
- Repository pattern (DDD anlamında)
- Bounded Context, Context Map
- Strategic vs Tactical DDD

### 3.3 CQRS & MediatR 🔴
- Command Query Responsibility Segregation
- Command, Query, Handler ayrımı
- MediatR ile pipeline behavior'lar (validation, logging, caching)
- Read model vs Write model
- Ne zaman CQRS aşırıya kaçar

### 3.4 Event-Driven Architecture 🔴
- Domain event vs Integration event
- Event Sourcing temelleri
- Outbox / Inbox pattern (dual-write problemi çözümü)
- Eventual consistency
- Saga pattern (orchestration vs choreography)

### 3.5 Microservices 🔴
- Ne zaman microservices, ne zaman monolit
- Service decomposition stratejileri
- API Gateway (YARP, Ocelot)
- Backend For Frontend (BFF)
- Service mesh temelleri (Istio, Linkerd)
- Distributed transactions ve saga

### 3.6 Message Brokers 🔴
- RabbitMQ (AMQP)
- Azure Service Bus
- Kafka temel modeli (topic, partition, consumer group)
- Kütüphaneler: MassTransit, NServiceBus
- At-most-once, at-least-once, exactly-once delivery semantics

### 3.7 Performance Tuning 🔴
- BenchmarkDotNet ile ölçüm
- Span<T>, Memory<T>, `stackalloc`
- Allocation azaltma (`ArrayPool<T>`, `MemoryPool<T>`)
- Source generators (JSON, regex, DI)
- AOT compilation (.NET 8+)
- Kestrel tuning, connection pooling

### 3.8 Memory Management & GC 🔴
- Managed heap, generations (Gen 0/1/2, LOH)
- Workstation vs Server GC
- `IDisposable`, finalizer, `IAsyncDisposable`
- Memory leak nedenleri (event handler, static referans)
- Profiling: dotMemory, PerfView, `dotnet-counters`, `dotnet-dump`

### 3.9 Concurrency Primitives 🔴
- `lock`, `Monitor`, `Mutex`, `Semaphore`, `SemaphoreSlim`
- `Interlocked` (atomic operations)
- `ReaderWriterLockSlim`
- Lock-free programming temelleri
- `Channel<T>` producer-consumer pattern

### 3.10 Distributed Systems 🔴
- CAP teoremi (bir üretim örneği üstünden)
- Consistency modelleri (strong, eventual, causal)
- Consensus (Raft, Paxos temel fikirleri)
- Idempotency, retries, backoff, circuit breaker (Polly)
- Distributed tracing, correlation

### 3.11 Security (derin) 🔴
- OWASP Top 10 — .NET Core özelinde her biri nasıl karşılanır
- SQL injection, XSS, CSRF, SSRF
- Secrets management (User Secrets, Key Vault)
- Data protection API
- Rate limiting, DDoS koruması
- Supply chain security (NuGet)

**Senior mülakat çıktısı:** Sistem tasarımı sorularını çözebilme, mimari trade-off'ları savunabilme, production incident post-mortem yazabilme, ekip mentorluğu senaryolarına cevap.

---

## 4. Veritabanları

### 4.1 Relational Fundamentals 🟢→🔴
- Normalizasyon (1NF, 2NF, 3NF, BCNF) ve denormalizasyon
- Primary key, foreign key, unique, check constraints
- ACID özellikleri
- Isolation levels (Read Uncommitted → Serializable), phantom read, dirty read, non-repeatable read
- Locking (row-level, table-level), deadlock

### 4.2 SQL Server 🟡
- T-SQL: CTE, window functions, `MERGE`
- Stored procedure, function, view, trigger
- Execution plan okuma
- Index çeşitleri (clustered, non-clustered, covering, filtered)
- Query hints
- Temp tables vs table variables vs CTE

### 4.3 PostgreSQL 🟡
- Npgsql driver, EF Core Postgres provider
- JSON/JSONB alanları, indeksleme (GIN, GiST)
- `psql` komutları, `EXPLAIN ANALYZE`
- Partitioning, VACUUM, autovacuum
- Extension'lar (pg_trgm, PostGIS, TimescaleDB)
- Row-Level Security

### 4.4 MongoDB 🟡
- Document model, BSON
- MongoDB.Driver kullanımı (.NET)
- Aggregation pipeline
- Indexler (single, compound, text, geospatial)
- Replication (replica set), sharding
- Ne zaman NoSQL, ne zaman değil

### 4.5 Query Optimization 🔴
- Query plan analizi
- Index seek vs index scan vs table scan
- Statistics, cardinality estimation
- Parameter sniffing
- N+1 tespiti (MiniProfiler, EF Core logging)

### 4.6 Transactions & Isolation 🔴
- `TransactionScope`, `IDbTransaction`
- Distributed transactions (MSDTC → günümüzde outbox tercih edilir)
- Optimistic vs pessimistic concurrency
- Row version / timestamp

### 4.7 NoSQL Patterns 🔴
- Document vs Key-Value vs Wide Column vs Graph
- Schema design in MongoDB (embedded vs referenced)
- Two-phase commit alternatifleri
- Multi-model kullanımı (SQL + NoSQL beraber)

### 4.8 Migrations 🟡
- EF Core migrations, FluentMigrator, DbUp
- Backward-compatible schema değişikliği
- Blue-green deployment ile şema değişimi
- Rollback stratejileri

---

## 5. Docker & Container Ekosistemi

### 5.1 Container Basics 🟢
- Image vs Container vs Registry
- Layer, cache, `docker inspect`
- `docker run`, `-v`, `-p`, `-e` bayrakları
- Docker Hub, GHCR, ACR

### 5.2 Dockerfile for .NET 🟡
- `mcr.microsoft.com/dotnet/sdk` vs `aspnet` runtime
- `WORKDIR`, `COPY`, `RUN`, `ENTRYPOINT`, `CMD`
- `.dockerignore`
- Non-root user, security

### 5.3 Docker Compose 🟡
- Multi-service tanımlama (`docker-compose.yml`)
- Networks, volumes
- API + Postgres + Redis + Seq bir arada
- Health check'ler

### 5.4 Multi-stage Builds 🟡
- Build image vs runtime image
- SDK'yı final image'da bırakmama
- Image boyut optimizasyonu (Alpine, chiseled images .NET 8+)

### 5.5 Kubernetes Girişi 🔴
- Pod, Deployment, Service, Ingress
- ConfigMap, Secret
- Helm chart temelleri
- Health probe'ları (liveness, readiness, startup)
- .NET workload'unun k8s'ye taşınması (graceful shutdown)

---

## 6. Azure Cloud

Junior seviyesinde temel PaaS servisleri, mid seviyesinde deployment ve konfigürasyon, senior seviyesinde mimari trade-off.

### 6.1 App Service 🟡
- Web App deployment (ZIP, GitHub Actions, DevOps)
- Application Settings, connection strings
- Slot deployment, blue-green
- Scale up vs Scale out

### 6.2 Azure SQL 🟡
- Single DB, Elastic Pool, Managed Instance
- DTU vs vCore model
- Firewall, Managed Identity ile auth
- Failover group, geo-replication

### 6.3 Cosmos DB 🟡
- API çeşitleri (SQL, Mongo, Cassandra, Gremlin, Table)
- Partition key seçimi (en kritik karar)
- RU/s ekonomisi
- Consistency levels
- Change Feed

### 6.4 Blob Storage 🟢
- Blob, Container, Access tier (Hot/Cool/Archive)
- SAS token
- `Azure.Storage.Blobs` SDK ile upload/download

### 6.5 Service Bus 🟡
- Queue vs Topic (pub-sub)
- Session, dead-letter, deferred messages
- MassTransit ile Service Bus kullanımı

### 6.6 Azure Functions 🟡
- Trigger türleri (HTTP, Timer, Blob, Queue, Service Bus, Cosmos)
- Isolated vs In-process worker model
- Durable Functions
- Cold start, plan tipi (Consumption, Premium, Dedicated)

### 6.7 Key Vault 🟡
- Secret, Key, Certificate
- Managed Identity ile Key Vault erişimi
- Konfigürasyondan secret enjekte etme

### 6.8 Azure AD / Entra ID 🟡
- App registration, tenant, service principal
- On-behalf-of flow, client credentials
- Microsoft.Identity.Web

### 6.9 Application Insights 🟡
- Telemetry types (Requests, Dependencies, Traces, Exceptions)
- Custom event tracking
- Kusto (KQL) sorgu temelleri
- Live Metrics, Application Map

### 6.10 Azure DevOps / GitHub Actions 🟡
- YAML pipeline temelleri (build → test → publish → deploy)
- Environment approvals
- Variable groups, secure files
- ARM / Bicep / Terraform ile IaC

---

## 7. AI Engineering (.NET tarafında)

.NET giderek AI-first bir platform oluyor. Bir "AI Engineer" .NET developer'ın bilmesi gerekenler:

### 7.1 Semantic Kernel Basics 🟡
- Kernel, Plugin, Function, Planner
- Prompt template'leri
- Semantic function vs Native function
- Memory (vector store bağlama)

### 7.2 OpenAI Integration 🟡
- `Azure.AI.OpenAI` SDK
- Chat completion, streaming
- Function calling / tool use
- Structured output (JSON schema)
- Token maliyetleri, rate limit

### 7.3 Embeddings & Vector Search 🟡
- Embedding nedir, ne işe yarar
- Cosine similarity, Euclidean, dot product
- Vector database'ler: Azure AI Search, Qdrant, pgvector, Milvus
- Chunking stratejileri

### 7.4 RAG Pipeline (Retrieval-Augmented Generation) 🔴
- Retriever + Generator pattern
- Chunk → Embed → Store → Retrieve → Prompt inject → Generate
- Hybrid search (keyword + vector)
- Reranking
- Evaluation (groundedness, relevance)

### 7.5 ML.NET 🟡
- AutoML kullanımı
- Model training, evaluation, prediction
- ONNX ile model içe aktarma
- Regression, classification, clustering örnekleri

### 7.6 AI Agent with .NET 🔴
- Agent = LLM + Tools + Memory + Loop
- Multi-agent orchestration
- Guardrails, prompt injection savunması
- Cost tracking, observability
- Semantic Kernel Agents vs kendi yazdığın loop

---

## 8. Sistem Tasarımı ve Mimari

### 8.1 Scalability 🔴
- Vertical vs Horizontal scaling
- Stateless service tasarımı
- Session affinity ve sorunları
- Database scaling: read replica, sharding, partitioning
- Load balancer katmanı

### 8.2 Caching Strategies 🔴
- Cache aside, read-through, write-through, write-behind
- CDN
- HTTP cache header'ları
- Cache invalidation problemi
- Consistent hashing

### 8.3 Rate Limiting 🔴
- Token bucket, leaky bucket, fixed window, sliding window
- .NET 7+ `Microsoft.AspNetCore.RateLimiting`
- Distributed rate limit (Redis üzerinden)

### 8.4 API Gateway 🔴
- Gateway pattern (routing, auth, rate limit, aggregation)
- YARP (Yet Another Reverse Proxy)
- Azure API Management

### 8.5 System Design Case Studies 🔴
- URL shortener
- Rate limiter
- Ticket booking / seat selection (concurrency)
- Notification system (fan-out)
- News feed
- Chat service (WebSocket / SignalR)
- E-commerce checkout (saga)

---

## 9. Mülakat Süreci ve Soft Skills

### 9.1 Coding Interview
- Whiteboard / LeetCode-tarzı sorular (C# reflex'i)
- Big-O analizi
- "Test cases" düşünme alışkanlığı

### 9.2 Live Coding — Pair Programming
- Konuşarak düşünme
- Assumption'ları açıkça söyleme
- Refactor teklifi vs "önce çalışsın" tercihi

### 9.3 Behavioral (STAR yöntemi)
- Conflict, deadline miss, mentoring, learning failure hikayeleri
- "Neden ayrılmak istiyorsun" — dürüst ama olumsuz olmayan cevap
- "Neden burası" — şirkete özel araştırma

### 9.4 System Design Interview
- Requirements clarification (functional + non-functional)
- Back-of-envelope tahmin (QPS, storage, bandwidth)
- High-level → deep-dive → trade-off
- Bottleneck ve failure mode analizi

### 9.5 Take-home Assignments
- Zamanı yönetmek (over-engineering yapmamak)
- README'nin önemi (tasarım kararlarını yazmak)
- Test yazma
- Docker Compose ile "tek komutla çalış" deneyimi

---

## Öğrenme Sırası ve Süreleri

Bu sıralama, sıfırdan başlayan bir mid-level adayı içindir. Süreler günde 2-3 saat çalışma varsayımıyla.

| Faz | Süre | İçerik |
|-----|------|--------|
| 1 | 3-4 hafta | Junior tüm başlıklar |
| 2 | 4-6 hafta | Mid — SOLID, Design Patterns, ASP.NET Core, EF Core, DI, Middleware |
| 3 | 2-3 hafta | Databases (SQL Server / Postgres) + Docker |
| 4 | 2 hafta | Auth + Caching + Testing (ileri) + Logging |
| 5 | 3-4 hafta | Senior — Clean Arch, DDD, CQRS, Event-driven, Microservices |
| 6 | 2-3 hafta | Azure (App Service, SQL, Service Bus, Functions, Key Vault, App Insights) |
| 7 | 2-3 hafta | AI (Semantic Kernel, OpenAI, RAG, ML.NET) |
| 8 | 2 hafta | System Design + Mülakat simülasyonu |

**Toplam:** ~5-6 ay yoğun çalışma.

Her hafta 1-2 gün **tekrar** ve **kodlama pratiği** (LeetCode/HackerRank C#) için ayrılmalı.

---

## Referans Kaynaklar

### Kitaplar
- *CLR via C#* — Jeffrey Richter
- *C# in Depth* — Jon Skeet
- *Pro ASP.NET Core* — Adam Freeman
- *Clean Architecture* — Robert C. Martin
- *Domain-Driven Design* — Eric Evans
- *Implementing DDD* — Vaughn Vernon
- *Designing Data-Intensive Applications* — Martin Kleppmann
- *Building Microservices* — Sam Newman
- *Concurrency in C# Cookbook* — Stephen Cleary

### Online Kaynaklar
- Microsoft Learn (learn.microsoft.com) — .NET, Azure resmi
- roadmap.sh/aspnet-core
- Milan Jovanović YouTube / blog
- Nick Chapsas YouTube
- Tim Corey YouTube
- Andrew Lock — Sean Cleary — Steve Gordon blogları
- .NET blog (devblogs.microsoft.com/dotnet)
- Awesome .NET (github.com/quozd/awesome-dotnet)

### Pratik
- LeetCode (C# ile)
- HackerRank
- Codewars
- Exercism.io (.NET track)
- Frontend Masters (backend/system design)

### Community
- .NET Foundation
- Local .NET meetup'ları
- r/dotnet, r/csharp
- Discord: .NET Community

---

## Nasıl İlerleyeceğim?

1. Bu roadmap **canlı bir doküman**. Her yeni konu tamamlandığında altındaki link aktif hale gelecek.
2. Her konunun klasöründe:
   - `README.md` — kavramsal
   - `src/` — çalışan proje (`dotnet run`)
   - `interview-questions.md` — o konudan çıkabilecek 10-20 mülakat sorusu ve cevabı
3. Konular **paralel** değil, **sıralı** ilerlenmeli. Bir konu bitmeden diğerine geçme.
4. Her hafta sonunda o haftanın konularından **kendi kendine mock mülakat** yap.

---

*Bu roadmap sürekli güncellenecektir. .NET ekosistemi hızlı değişiyor (özellikle AI tarafı), o yüzden her 3 ayda bir gözden geçir.*
