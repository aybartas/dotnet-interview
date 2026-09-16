# .NET Interview Hazırlık Roadmap'i

> Junior → Mid → Senior seviye .NET geliştirici mülakatlarına hazırlık için kapsamlı yol haritası.
> Her başlık için ayrı bir Markdown dokümanı ve **çalıştırılabilir kod örnekleri** oluşturulacaktır.

**Hedef sürüm:** .NET 10 (LTS, Kasım 2025 — destek Kasım 2028) / C# 14
**Güncelleme:** Eylül 2026

---

## İçindekiler

| # | Bölüm | Odak |
|---|-------|------|
| 0 | [Ön Koşullar](#0-ön-koşullar) | Git, HTTP, CLI, algoritma |
| 1 | [C# Dili](#1-c-dili) | Dilin kendisi, temelden ileriye |
| 2 | [Derleme, Build & Çalıştırma](#2-derleme-build--çalıştırma) | Roslyn, IL, MSBuild, apphost, JIT, publish modelleri |
| 3 | [.NET Platform Servisleri](#3-net-platform-servisleri) | Hosting, configuration, DI, middleware, logging |
| 4 | [Multithreading, Concurrency & Async](#4-multithreading-concurrency--async) | Thread, TPL, senkronizasyon, async internals |
| 5 | [API Geliştirme](#5-api-geliştirme) | REST, gRPC, GraphQL, SignalR, versioning, güvenlik |
| 6 | [Veri Erişimi & ORM](#6-veri-erişimi--orm) | EF Core, Dapper, ADO.NET, mapping |
| 7 | [Veritabanları](#7-veritabanları) | SQL Server, PostgreSQL, MongoDB, Redis |
| 8 | [Test](#8-test) | Unit, integration, E2E, performance, architecture |
| 9 | [Mimari & Tasarım](#9-mimari--tasarım) | SOLID, patterns, Clean Arch, DDD, CQRS |
| 10 | [Microservices & Dağıtık Sistemler](#10-microservices--dağıtık-sistemler) | Message broker, saga, gateway, resilience |
| 11 | [Performans & Bellek](#11-performans--bellek) | GC, allocation, benchmark, AOT |
| 12 | [Güvenlik](#12-güvenlik) | OWASP, auth, secrets, kriptografi |
| 13 | [Docker, Kubernetes & DevOps](#13-docker-kubernetes--devops) | Container, orchestration, CI/CD, IaC |
| 14 | [Azure Cloud](#14-azure-cloud) | PaaS servisleri, Aspire |
| 15 | [AI Engineering (.NET)](#15-ai-engineering-net) | Semantic Kernel, RAG, agents, ML.NET |
| 16 | [Sistem Tasarımı](#16-sistem-tasarımı) | Scalability, case study'ler |
| 17 | [Mülakat Süreci](#17-mülakat-süreci) | Coding, behavioral, system design |

Ayrıca: [Seviye Matrisi](#seviye-matrisi) · [Repo Yapısı](#repo-yapısı) · [Çalışma Planı](#çalışma-planı) · [Kaynaklar](#kaynaklar)

---

## Roadmap Felsefesi ve Kullanım

Bu repo klasik "listeleme" bir roadmap değildir. Her konu için üç katman vardır:

1. **Kavramsal `.md` dokümanı** — konunun teorisi, mülakatta sorulan şekli, tuzakları.
2. **Çalıştırılabilir kod örnekleri** — `dotnet run` ile hemen çalışan, tek başına anlamlı projeler.
3. **Gerçek dünya senaryosu** — bu konunun bir üretim uygulamasında hangi problemi çözdüğü.

**Seviye kodlaması:**
- 🟢 **Junior** — 0-2 yıl; bilinmesi zorunlu, ilk teknik turu geçirir
- 🟡 **Mid** — 2-5 yıl; iyi kavranmış, uygulamada kullanılmış olmalı
- 🔴 **Senior** — 5+ yıl; trade-off'ları bilinen, production deneyimiyle içselleştirilmiş

Bir konu birden fazla rozet taşıyorsa (`🟢→🔴` gibi), seviye yükseldikçe **aynı konunun daha derin bir kesiti** sorulur demektir. Örneğin LINQ junior'da "`Where` ve `Select` ne yapar", senior'da "`IQueryable` expression tree'si EF Core'da nasıl SQL'e çevriliyor, `AsSplitQuery` ne zaman gerekir" olarak gelir.

**Nasıl çalışılır:**
- Sıralı ilerle, paralel değil. Bir konu bitmeden diğerine geçme.
- Her konuda önce `README.md` oku → sonra `src/` içindeki kodu **kendin yaz** → sonra `interview-questions.md`'yi kapalı kitap cevapla.
- Haftada 1-2 gün tekrar + kodlama pratiği (LeetCode/HackerRank C#).

---

## Seviye Matrisi

Hangi seviyede hangi bölümlerin **hangi derinlikte** beklendiğinin özeti. Mülakata kısa sürede hazırlanıyorsan önce kendi seviyenin sütununa odaklan.

| Bölüm | 🟢 Junior | 🟡 Mid | 🔴 Senior |
|-------|----------|--------|-----------|
| **0. Ön Koşullar** | Git, HTTP, CLI | + REST olgunluk, TLS | + DNS/CDN/ağ katmanı |
| **1. C# Dili** | Sözdizimi, tip sistemi, OOP, collections, LINQ temel | Delegate/event, modern C#, LINQ ileri | Span/Memory, source generator, expression tree |
| **2. Derleme & Çalıştırma** | `bin`/`obj` ne, `run` vs `publish` | IL/metadata, apphost, deployment modelleri, MSBuild | JIT/tiered/PGO, AOT & trimming, assembly loading |
| **3. Platform Servisleri** | DI nedir, appsettings, ILogger | Middleware yazma, Options pattern, hosting, background service | Host internals, startup performansı, modül tasarımı |
| **4. Multithreading** | async/await kullanımı, Task | TPL, senkronizasyon, concurrent collections | Memory model, lock-free, Channel, distributed lock |
| **5. API** | REST, status code, CRUD endpoint | Versioning, validation, OpenAPI, HttpClientFactory | gRPC/GraphQL seçimi, BFF, gateway, API evolution |
| **6. Veri Erişimi** | EF Core CRUD, migration | Tracking, N+1, Dapper, Fluent API | Sorgu planı, bulk ops, çoklu provider stratejisi |
| **7. Veritabanları** | SELECT/JOIN, index nedir | Transaction, isolation, MongoDB, Redis | Sharding, replication, sorgu optimizasyonu |
| **8. Test** | xUnit, AAA, mocking | Integration test, TestContainers, coverage | Test stratejisi, architecture test, performans testi |
| **9. Mimari** | Katmanlı mimari | SOLID, design patterns, Clean Arch | DDD, CQRS, trade-off savunması |
| **10. Microservices** | — | Message broker temeli, Docker Compose | Saga, outbox, consistency, decomposition |
| **11. Performans** | StringBuilder, ToList yeri | Caching, async I/O | GC tuning, BenchmarkDotNet, AOT, profiling |
| **12. Güvenlik** | JWT kullanımı, HTTPS | OWASP Top 10, policy auth | Threat modeling, secrets rotation, supply chain |
| **13. Docker/DevOps** | `docker run`, Dockerfile | Compose, multi-stage, CI pipeline | K8s, IaC, deployment stratejileri |
| **14. Azure** | Portal, App Service deploy | Azure SQL, Storage, Key Vault, App Insights | Mimari seçimi, maliyet, Aspire, multi-region |
| **15. AI** | LLM/prompt temel | OpenAI SDK, embedding, Semantic Kernel | RAG mimarisi, agent, eval, guardrail |
| **16. System Design** | — | Temel case'ler | Full design interview |

---

## Repo Yapısı

```
dotnet-interview/
├── ROADMAP.md                          # bu dosya
├── README.md
│
├── 00-prerequisites/
│   ├── 01-git-workflow/
│   ├── 02-http-and-web/
│   ├── 03-dotnet-cli-tooling/
│   └── 04-algorithms-datastructures/
│
├── 01-csharp/
│   ├── 01-program-anatomy/             # entry point, namespace, erişim belirleyicileri
│   │   ├── README.md                   # kavramsal anlatım
│   │   ├── src/                        # çalışan .NET projesi
│   │   └── interview-questions.md
│   ├── 02-type-system-memory/          # value/reference, stack/heap, boxing
│   ├── 03-oop/
│   ├── 04-collections-generics/
│   ├── 05-exception-handling/
│   ├── 06-delegates-events-lambdas/
│   ├── 07-linq/
│   ├── 08-modern-csharp/               # C# 8 → 14
│   ├── 09-span-memory/
│   └── 10-reflection-sourcegen/
│
├── 02-compilation-runtime/             # "kod derlenince ne oluyor?"
│   ├── 01-roslyn-compilation/          # lexer→parser→binder→IL, lowering
│   ├── 02-il-metadata-assembly/        # IL okuma, PE formatı, dll vs exe
│   ├── 03-msbuild-build-process/       # csproj, restore, target, binlog
│   ├── 04-bin-obj-artifacts/           # hangi dosya ne işe yarar
│   ├── 05-pdb-debugging-symbols/
│   ├── 06-apphost-startup-chain/       # exe nasıl oluşuyor, hostfxr→coreclr
│   ├── 07-run-build-publish/           # komutların farkı
│   ├── 08-deployment-models/           # FDD, SCD, single-file, trimmed, AOT
│   ├── 09-jit-runtime-execution/       # tiered compilation, PGO, ALC
│   └── 10-observation-labs/            # ölç, aç, bak egzersizleri
│
├── 03-dotnet-platform/                 # ".NET basics" — platform servisleri
│   ├── 01-generic-host-lifecycle/
│   ├── 02-configuration/               # appsettings, env, secrets, Options
│   ├── 03-dependency-injection/
│   ├── 04-middleware-pipeline/
│   ├── 05-filters-attributes/
│   ├── 06-logging/
│   ├── 07-background-services/
│   └── 08-environments-deployment/
│
├── 04-multithreading/
│   ├── 01-concepts/                    # process/thread, concurrency vs parallelism
│   ├── 02-thread-threadpool/
│   ├── 03-task-tpl/
│   ├── 04-async-await-internals/
│   ├── 05-synchronization-primitives/
│   ├── 06-concurrent-collections/
│   ├── 07-parallel-plinq/
│   ├── 08-channels-pipelines/
│   ├── 09-cancellation/
│   ├── 10-concurrency-problems/        # deadlock, race, memory model
│   ├── 11-distributed-locking/
│   └── 12-debugging-profiling/
│
├── 05-api/
│   ├── 01-http-web-foundations/
│   ├── 02-rest-design/
│   ├── 03-aspnetcore-webapi/
│   ├── 04-minimal-api-repr/
│   ├── 05-validation-errorhandling/
│   ├── 06-versioning/
│   ├── 07-openapi-docs/
│   ├── 08-api-security/
│   ├── 09-grpc/
│   ├── 10-graphql/
│   ├── 11-realtime-signalr/
│   ├── 12-http-clients/
│   ├── 13-serialization/
│   ├── 14-api-performance/
│   └── 15-api-patterns/                # BFF, gateway, webhook, async API
│
├── 06-data-access/
│   ├── 01-adonet/
│   ├── 02-ef-core/
│   ├── 03-dapper/
│   ├── 04-object-mapping/
│   └── 05-repository-uow-specification/
│
├── 07-databases/
│   ├── 01-relational-fundamentals/
│   ├── 02-sql-server/
│   ├── 03-postgresql/
│   ├── 04-mongodb/
│   ├── 05-redis/
│   ├── 06-query-optimization/
│   ├── 07-transactions-isolation/
│   ├── 08-migrations/
│   └── 09-search-engines/
│
├── 08-testing/
│   ├── 01-unit-testing/
│   ├── 02-mocking/
│   ├── 03-integration-testing/
│   ├── 04-testcontainers/
│   ├── 05-e2e-testing/
│   ├── 06-performance-testing/
│   └── 07-architecture-testing/
│
├── 09-architecture/
│   ├── 01-solid/
│   ├── 02-design-patterns/
│   ├── 03-layered-vs-clean/
│   ├── 04-ddd/
│   ├── 05-cqrs-mediatr/
│   ├── 06-vertical-slice/
│   └── 07-modular-monolith/
│
├── 10-distributed/
│   ├── 01-microservices-fundamentals/
│   ├── 02-message-brokers/
│   ├── 03-event-driven/
│   ├── 04-outbox-saga/
│   ├── 05-api-gateway/
│   ├── 06-resilience-polly/
│   └── 07-distributed-concepts/        # CAP, consistency, consensus
│
├── 11-performance/
│   ├── 01-benchmarking/
│   ├── 02-memory-gc/
│   ├── 03-allocation-reduction/
│   ├── 04-caching-strategies/
│   ├── 05-aot-trimming/
│   └── 06-profiling-diagnostics/
│
├── 12-security/
│   ├── 01-owasp-top10/
│   ├── 02-authentication/
│   ├── 03-authorization/
│   ├── 04-secrets-management/
│   ├── 05-cryptography/
│   └── 06-supply-chain/
│
├── 13-devops/
│   ├── 01-docker-basics/
│   ├── 02-dockerfile-dotnet/
│   ├── 03-docker-compose/
│   ├── 04-kubernetes/
│   ├── 05-ci-cd/
│   └── 06-iac/
│
├── 14-azure/
│   ├── 01-app-service/
│   ├── 02-azure-sql/
│   ├── 03-cosmos-db/
│   ├── 04-blob-storage/
│   ├── 05-service-bus/
│   ├── 06-functions/
│   ├── 07-key-vault/
│   ├── 08-entra-id/
│   ├── 09-application-insights/
│   ├── 10-container-apps-aks/
│   └── 11-dotnet-aspire/
│
├── 15-ai/
│   ├── 01-llm-fundamentals/
│   ├── 02-microsoft-extensions-ai/
│   ├── 03-semantic-kernel/
│   ├── 04-embeddings-vector-search/
│   ├── 05-rag-pipeline/
│   ├── 06-ai-agents/
│   ├── 07-ml-net/
│   └── 08-ai-evaluation-safety/
│
├── 16-system-design/
│   ├── 01-scalability/
│   ├── 02-caching-at-scale/
│   ├── 03-rate-limiting/
│   ├── 04-data-partitioning/
│   └── 05-case-studies/
│
└── 17-interview/
    ├── coding-challenges/
    ├── behavioral-questions.md
    ├── system-design-drills.md
    ├── take-home-guidelines.md
    └── mock-interview-scripts.md
```

Her konu klasöründeki `README.md` şu şablonu kullanacak:

```markdown
# <Konu>

## Ne? (tanım, zihinsel model)
## Neden? (hangi problemi çözüyor)
## Nasıl? (kod üzerinden)
## Gerçek dünya senaryosu
## Mülakat perspektifi (nasıl sorulur, ne beklenir)
## Yaygın hatalar / tuzaklar
## Performans & trade-off notları
## Kaynaklar
```

---

## 0. Ön Koşullar

Bunlar "konu" değil, geri kalan her şeyin üstüne kurulduğu zemin. Eksikse önce burayı kapat.

### 0.1 Git & Versiyon Kontrolü 🟢
- Commit, branch, merge, rebase farkı
- Merge conflict çözümü
- `git stash`, `git cherry-pick`, `git bisect`
- Pull request akışı, code review kültürü
- Branching stratejileri: Git Flow, GitHub Flow, trunk-based
- Conventional commits, semantic versioning

### 0.2 HTTP & Web Temelleri 🟢→🟡
- Request/response anatomisi: method, header, body, status code
- HTTP/1.1 vs HTTP/2 vs HTTP/3 (QUIC) — multiplexing, head-of-line blocking
- Status code'ların doğru kullanımı (özellikle 201, 204, 400 vs 422, 409, 429)
- Header'lar: `Content-Type`, `Accept`, `Authorization`, `Cache-Control`, `ETag`
- Cookie, session, CORS
- TLS/SSL handshake, sertifika zinciri, HSTS
- DNS çözümleme, CDN mantığı
- Idempotency ve güvenli (safe) metodlar

### 0.3 .NET CLI & Tooling 🟢
- SDK vs Runtime vs Target Framework Moniker (`net10.0`)
- `dotnet new`, `build`, `run`, `test`, `publish`, `add package`
- Solution (`.sln`) ve proje (`.csproj`) yapısı
- `Directory.Build.props`, `Directory.Packages.props` (central package management)
- NuGet: paket kaynakları, lock file, versiyon çözümleme
- `global.json` ile SDK sabitleme
- Debugging: breakpoint, conditional breakpoint, watch, immediate window
- Analyzer'lar ve `.editorconfig`, StyleCop, `TreatWarningsAsErrors`

### 0.4 Algoritma & Veri Yapıları 🟢→🟡
- Big-O notasyonu (time & space)
- Array, Linked List, Stack, Queue, Hash Table, Tree, Graph, Heap
- Sorting (quick, merge, heap) ve searching (binary search)
- Recursion, memoization, dynamic programming temeli
- Two pointers, sliding window, prefix sum
- C#'ta bunların karşılıkları (`List<T>`, `Dictionary<,>`, `SortedSet<T>`, `PriorityQueue<,>`)

---

## 1. C# Dili

### 1.1 Bir C# Programının Anatomisi 🟢
> "Bu kod nasıl çalışıyor?" sorusunun dil tarafındaki cevabı. Derleyici tarafı [Bölüm 2](#2-derleme-build--çalıştırma)'de.

- **Entry point:** `Main` metodunun geçerli imzaları — `void Main()`, `int Main()`, `Main(string[] args)`, `async Task Main()` — ve exit code'un anlamı
- **Top-level statements** (C# 9+) — derleyici bunu gizli bir `Program` sınıfı + `Main` metoduna çevirir; `args` ve `return` nasıl çalışır
- Neden integration test'te `public partial class Program { }` eklemek gerekir (`WebApplicationFactory<Program>`)
- Namespace, file-scoped namespace (`namespace X;`), iç içe namespace
- `using` direktifi türleri: normal, `static`, alias (`using Json = System.Text.Json;`), **global using**, implicit usings (SDK'nın otomatik eklediği)
- **Erişim belirleyicileri — tam tablo:**

  | Belirleyici | Aynı sınıf | Türeyen (aynı assembly) | Aynı assembly | Türeyen (farklı assembly) | Her yer |
  |-------------|:---------:|:----------------------:|:-------------:|:------------------------:|:-------:|
  | `private` | ✅ | ❌ | ❌ | ❌ | ❌ |
  | `private protected` | ✅ | ✅ | ❌ | ❌ | ❌ |
  | `protected` | ✅ | ✅ | ❌ | ✅ | ❌ |
  | `internal` | ✅ | ✅ | ✅ | ❌ | ❌ |
  | `protected internal` | ✅ | ✅ | ✅ | ✅ | ❌ |
  | `public` | ✅ | ✅ | ✅ | ✅ | ✅ |

  - Varsayılan erişim seviyeleri (tip → `internal`, üye → `private`)
  - `InternalsVisibleTo` ile test projesine `internal` açma
- `static class`, static üye, **static constructor** — ne zaman çalışır, `beforefieldinit` etkisi, thread-safety garantisi
- `partial` class / method / property — source generator'ların temel mekanizması
- **Statement vs expression** ayrımı; expression-bodied members (`=>`)
- Değişken kapsamı (scope), gölgeleme (shadowing), definite assignment analizi
- Tip dönüşümleri: implicit, explicit (cast), `is`, `as`, `Convert`, `Parse` vs `TryParse`
- `checked` / `unchecked` aritmetik, integer overflow davranışı (varsayılan **unchecked**)
- Operatör önceliği, associativity, short-circuit (`&&`/`||` vs `&`/`|`)
- `nameof`, `typeof`, `sizeof`, `default`, `with`
- Preprocessor direktifleri: `#if DEBUG`, `#region`, `#nullable`, `#pragma warning`
- Yorum satırları ve **XML documentation comment** (`///`) — OpenAPI ve IntelliSense'e yansıması
- `[CallerMemberName]`, `[CallerLineNumber]`, `[CallerArgumentExpression]` — compile-time enjekte edilen bilgi

### 1.2 Tip Sistemi & Bellek Modeli 🟢→🟡
> Mülakatın en klasik açılışı: "value type ile reference type farkı nedir?" — ama yüzeysel cevap yetmez.

- **Unified type system:** her şey `System.Object`'ten türer; `int` = `System.Int32` (dil anahtar kelimesi = BCL tipi takma adı)
- Tip hiyerarşisi: `object` → `ValueType` → `Enum`; `class`, `interface`, `delegate`, `array`
- **Value type vs reference type:**
  - Kopyalama semantiği (değer kopyası vs referans kopyası) — asıl fark bu, "stack/heap" değil
  - **Yaygın yanılgı:** "value type her zaman stack'te" — sınıf alanı olan, closure'a yakalanan, boxing'e uğrayan, array elemanı olan value type **heap**'tedir
  - Atama, parametre geçişi ve `readonly` davranışı
  - Eşitlik: `==` varsayılan davranışı (value type: bit karşılaştırma; reference type: referans eşitliği)
- `struct` vs `class` vs `record` vs `record struct` vs `readonly struct` — **karar tablosu**
  - Struct ne zaman: küçük (≤16 byte civarı), immutable, kısa ömürlü, değer semantiği isteniyor
  - Mutable struct neden tehlikeli (defensive copy, koleksiyonda güncelleme tuzağı)
- **Boxing / unboxing:** ne zaman gizlice olur (interface'e atama, `object` parametre, non-generic koleksiyon, string interpolation, `Enum` ile `Equals`), allocation maliyeti, nasıl kaçınılır
- `string`:
  - Immutability — neden ve sonuçları
  - **String interning** ve literal'ların paylaşımı; `string.Intern`
  - `==` string'de neden değer karşılaştırması yapar (operator overload)
  - `StringBuilder` ne zaman kazandırır (döngüde birleştirme), ne zaman gereksiz
  - `string.Compare` vs `Equals` vs `StringComparison` — **kültür duyarlılığı** ve `StringComparison.Ordinal` tercihi
  - UTF-16 iç gösterimi, `char` vs rune vs grapheme; Unicode ve `Length` tuzağı
- **Nullable:**
  - Nullable value types (`int?` = `Nullable<int>`) — `HasValue`, `Value`, lifted operatörler
  - Nullable reference types (`string?`) — **sadece compile-time**, runtime'da hiçbir şey yok
  - `#nullable enable`, uyarı seviyeleri, `!` (null-forgiving) ne zaman meşru
- Null operatörleri: `?.`, `?[]`, `??`, `??=`, C# 14 `?.=`
- `default(T)` ve tip bazında varsayılan değerler
- `var` (compile-time çıkarım, IL'de izi yok) vs `dynamic` (runtime binding, DLR maliyeti) vs `object`
- `const` (compile-time sabit, **çağıran assembly'ye gömülür** — versiyonlama tuzağı) vs `readonly` vs `static readonly`
- `ref`, `out`, `in` parametreleri; `ref` return, `ref` local, `ref readonly`
- Tuple (`ValueTuple`) vs eski `Tuple`, deconstruction, named tuple elemanlarının derleme zamanı doğası
- `enum` — altta yatan tip, `[Flags]`, `Enum.Parse`/`TryParse`, boxing maliyeti
- Implicit/explicit conversion operatörü, `operator` overloading, `IParsable<T>`/`ISpanParsable<T>` (C# 11 generic math)
- Generic'ler **runtime'da reified** — Java'nın type erasure'ından farkı (klasik mülakat sorusu); value type için ayrı native kod, reference type için paylaşılan kod

### 1.3 OOP 🟢
- Encapsulation, Inheritance, Polymorphism, Abstraction — sadece tanım değil, **örnekle**
- `abstract class` vs `interface` — ne zaman hangisi (karar tablosu)
- `sealed`, `virtual`, `override`, `new` keyword'leri ve method hiding
- Method overloading vs overriding — compile-time vs runtime binding
- Interface default implementations (C# 8+) ve diamond problem
- Explicit interface implementation
- **Composition over inheritance** — neden, nasıl refactor edilir
- Object lifecycle: constructor, `IDisposable`, finalizer
- `object` sınıfının metodları: `Equals`, `GetHashCode`, `ToString` — birlikte override kuralı
- `IEquatable<T>`, `IComparable<T>`, `IComparer<T>`

### 1.4 Collections & Generics 🟢→🟡
- `List<T>`, `Dictionary<TKey,TValue>`, `HashSet<T>`, `Queue<T>`, `Stack<T>`, `LinkedList<T>`
- `SortedList`, `SortedDictionary`, `SortedSet`, `PriorityQueue<TElement,TPriority>`
- Arayüz hiyerarşisi: `IEnumerable<T>` → `ICollection<T>` → `IList<T>`; `IReadOnlyList<T>`, `IReadOnlyDictionary<,>`
- `Array` vs `List<T>` — kapasite büyümesi, amortized cost
- `foreach` altında ne oluyor — `IEnumerator<T>`, `yield return`, iterator state machine
- `Dictionary` iç yapısı: bucket, hash collision, `GetHashCode` sözleşmesi
- Generic constraint'ler: `where T : class`, `struct`, `new()`, `notnull`, `IComparable<T>`
- Covariance (`out`) / contravariance (`in`)
- `ImmutableArray<T>`, `FrozenDictionary<TKey,TValue>` (.NET 8+) — okuma ağırlıklı senaryolar
- Big-O karşılaştırma tablosu

### 1.5 Exception Handling 🟢
- `try` / `catch` / `finally` / `using` / `await using`
- Exception hiyerarşisi, custom exception yazımı (ne zaman gerekli)
- Exception filter (`when`) — stack unwinding farkı
- **`throw` vs `throw ex`** — stack trace kaybı (klasik mülakat sorusu)
- `ExceptionDispatchInfo.Capture` ile rethrow
- Exception maliyeti — control flow için kullanmama kuralı
- `AggregateException` ve `Task` içindeki exception davranışı
- Global exception handling: `IExceptionHandler` (.NET 8+), middleware, `ProblemDetails`
- Ne zaman exception, ne zaman `Result<T>` pattern

### 1.6 Delegates, Events, Lambdas 🟢→🟡
- `delegate`, `Func<>`, `Action<>`, `Predicate<>`
- Multicast delegate, invocation list
- `event` keyword — neden sadece delegate değil
- Event handler memory leak (en yaygın .NET leak sebebi) ve `WeakEventManager`
- Lambda expression, closure ve **captured variable tuzağı** (döngüde closure)
- `Expression<Func<T,bool>>` — expression tree, EF Core'un temel mekanizması
- Anonim metodlar, local function vs lambda (allocation farkı)

### 1.7 LINQ 🟢→🔴
- Query syntax vs method syntax
- **Deferred vs immediate execution** — en sık sorulan LINQ konusu
- Temel operatörler: `Select`, `SelectMany`, `Where`, `GroupBy`, `Join`, `GroupJoin`, `OrderBy/ThenBy`
- Eleman operatörleri: `First`, `FirstOrDefault`, `Single`, `SingleOrDefault` — farkları ve hangisi ne zaman
- Kümeler: `Distinct`, `Union`, `Intersect`, `Except`, `DistinctBy` (.NET 6+)
- Aggregate: `Sum`, `Average`, `Count`, `Aggregate`, `Any`, `All`
- `Chunk`, `Zip`, `TakeLast`, `SkipLast`, `MaxBy`/`MinBy`
- **`IEnumerable<T>` vs `IQueryable<T>`** — LINQ to Objects vs LINQ to Entities, ne zaman SQL'e çevrilir
- Client-side evaluation tuzağı (EF Core'da `AsEnumerable()` sonrası)
- Çoklu enumeration problemi (`ReSharper: possible multiple enumeration`)
- `ToList()` nereye konur — N+1 ve gereksiz materialization
- LINQ performans: `Count() > 0` vs `Any()`, `OrderBy` maliyeti
- Custom LINQ operatörü yazma (extension method)

### 1.8 Modern C# (8 → 14) 🟡
- **C# 8:** nullable reference types, pattern matching genişletmesi, `switch` expression, async streams (`IAsyncEnumerable`), ranges/indices (`^1`, `1..3`), default interface methods, `using` declaration
- **C# 9:** records, init-only setters, top-level statements, target-typed `new`, pattern enhancements
- **C# 10:** file-scoped namespace, global usings, record struct, constant interpolated strings
- **C# 11:** raw string literals, required members, generic attributes, list patterns, UTF-8 string literals
- **C# 12:** primary constructors (tüm tiplerde), collection expressions (`[1, 2, 3]`), inline arrays, alias any type
- **C# 13:** `params` collections, yeni lock objesi (`System.Threading.Lock`), `\e` escape, `ref`/`unsafe` iterator'larda
- **C# 14:** **extension members** (property/operator/static üye ekleme), **`field` keyword** (backing field'sız property validation), null-conditional assignment (`?.=`), `Span<T>` implicit conversion iyileştirmeleri, file-based apps (`dotnet run app.cs`)
- Pattern matching bütünü: type, constant, relational, logical (`and`/`or`/`not`), property, positional, list patterns

### 1.9 Span, Memory & Yüksek Performans Tipleri 🔴
- `Span<T>` / `ReadOnlySpan<T>` — stack-only, allocation'sız dilimleme
- `Memory<T>` / `ReadOnlyMemory<T>` — async'te neden `Span` kullanılamaz
- `ref struct` kısıtları, `scoped` keyword
- `stackalloc` — güvenli kullanım sınırları
- `ArrayPool<T>`, `MemoryPool<T>` — buffer yeniden kullanımı
- `System.IO.Pipelines` — yüksek performanslı I/O
- `Utf8JsonReader`/`Writer` ile allocation'sız JSON
- String işlemlerinde `AsSpan()` ile `Substring` allocation'ından kaçınma
- `SearchValues<T>` (.NET 8+)

### 1.10 Reflection, Attributes & Source Generators 🔴
- `Type`, `MethodInfo`, `PropertyInfo`, `Activator.CreateInstance`
- Reflection'ın maliyeti ve cache stratejileri
- Custom attribute yazma ve okuma
- `Expression` tree derleme (`Expression.Compile()`) — reflection'a hızlı alternatif
- **Source generators** — compile-time kod üretimi
  - `System.Text.Json` source-gen serializer
  - `LoggerMessage` source generator (yüksek performanslı logging)
  - Regex source generator (`[GeneratedRegex]`)
  - Mapperly (mapping), Riok, `[JsonSerializable]`
- Neden AOT/trimming senaryosunda reflection yerine source generator
- Roslyn analyzer & code fix yazma temeli

---

## 2. Derleme, Build & Çalıştırma

> **"C# kodu derlenince ne oluyor? `dotnet run` ile `publish` farkı ne? Kod nasıl `.exe` oluyor?"**
> Çoğu geliştiricinin yıllarca çalışıp hiç sormadığı, mülakatta ise kıdemi anında ele veren bölüm.
> Bu bölüm okunacak değil, **gözlemlenecek** — her başlıkta "aç ve bak" egzersizi var.

**Tek cümlelik özet (önce bunu oku, sonra detaya gir):**
```
C# kaynak kod
  → [Roslyn derleyici] → IL + metadata içeren .dll (assembly)
  → [MSBuild] bin/ klasörüne .dll + .exe(apphost) + .deps.json + .runtimeconfig.json
  → [çalıştırma] apphost.exe → hostfxr → hostpolicy → CoreCLR
  → [JIT] IL'i metodun ilk çağrısında native makine koduna çevirir
  → CPU çalıştırır
```
**Kritik gerçek:** Senin kodun **her zaman `.dll`** olarak derlenir. `.exe` senin IL'ini içermez — sadece o `.dll`'i başlatan küçük bir native launcher'dır.

### 2.1 Kaynak Koddan IL'e: Roslyn Derleyicisi 🟡
- **Roslyn** nedir: C#/VB derleyicisi, açık kaynak, "compiler as a service" (IDE, analyzer, source generator hep bunu kullanır)
- **Derleme aşamaları:**
  1. **Lexing** — karakterler → token'lar
  2. **Parsing** — token'lar → **syntax tree** (sözdizimsel yapı)
  3. **Binding / semantic analysis** — sembol çözümleme, tip kontrolü, overload resolution → **semantic model**
  4. **Lowering** — syntactic sugar'ın basit yapılara açılması
  5. **IL emission** — IL + metadata yazımı → `.dll`
- Compile-time error (CS####) vs runtime exception — hangi hata nerede yakalanır
- **Lowering: derleyicinin gizli dönüşümleri** — mülakatta çok değerli:

  | Yazdığın | Derleyicinin ürettiği |
  |----------|----------------------|
  | `foreach` | `GetEnumerator()` + `while(MoveNext())` + `try/finally { Dispose() }` |
  | `using` deyimi | `try/finally` + `Dispose()` |
  | `async`/`await` | `IAsyncStateMachine` implement eden **state machine** struct/class |
  | `yield return` | Iterator state machine sınıfı |
  | lambda + closure | **Display class** (yakalanan değişkenler alan olur) |
  | `var` | Gerçek tip — IL'de `var` diye bir şey yok |
  | String interpolation | `string.Format` veya `DefaultInterpolatedStringHandler` |
  | LINQ query syntax | Extension method zinciri (`Where().Select()`) |
  | Auto-property | `private` backing field + `get_X()`/`set_X()` metodları |
  | `record` | Class + `Equals`/`GetHashCode`/`ToString`/`Deconstruct`/`<Clone>$` |
  | Primary constructor | Yakalanan parametreler için gizli alanlar |
  | `lock` | `Monitor.Enter`/`Exit` + `try/finally` (C# 13'te `Lock.EnterScope`) |
  | Collection expression `[1,2]` | Tip bazında en verimli oluşturma kodu |

- **sharplab.io** — bu dönüşümleri canlı görmek için en iyi araç (Results: C# → "Lowered C#" veya "IL")
- Nullable reference types **sadece derleme zamanı** — IL'de `[Nullable]` attribute'u olarak iz bırakır, runtime kontrolü yoktur
- `var` ve `dynamic` farkının IL'deki karşılığı (`dynamic` → `CallSite` + DLR çağrıları)
- Conditional compilation: `#if DEBUG`, `DefineConstants`, `[Conditional("DEBUG")]` attribute'u
- Source generator'ların derlemeye katılması — üretilen kodun `obj/.../generated/` altında görünmesi (`EmitCompilerGeneratedFiles`)
- Analyzer'lar ve derleme zamanı kural ihlalleri

**Lab:** Aynı dosyayı sharplab.io'ya yapıştır; `foreach`, `async`, lambda, `record` için üretilen kodu incele.

### 2.2 IL, Metadata ve Assembly Anatomisi 🟡→🔴
- **IL (Intermediate Language / MSIL / CIL)** nedir, neden var: dil bağımsızlığı (C#/F#/VB aynı IL'e çıkar) + platform bağımsızlığı
- IL **stack-based** bir sanal makine dilidir (register-based değil)
- Temel opcode'lar: `ldarg`, `ldloc`, `stloc`, `ldfld`, `call`, `callvirt`, `newobj`, `box`/`unbox`, `ret`
- **`call` vs `callvirt`** — `callvirt` sanal dispatch yapar **ve null kontrolü içerir**; derleyici neden non-virtual metodlarda bile `callvirt` üretir (klasik derin soru)
- **Assembly = PE (Portable Executable) dosyası.** İçeriği:
  - DOS/PE header (Windows uyumluluk mirası)
  - **CLR header** (bu bir managed assembly'dir işareti)
  - **Metadata tabloları** — `TypeDef`, `MethodDef`, `Field`, `MemberRef`, `AssemblyRef`... (tiplerin ve üyelerin tam tanımı)
  - **IL kodu**
  - **Manifest** — assembly adı, versiyon, kültür, public key, referans verilen assembly'ler
  - Gömülü kaynaklar (resx, embedded file)
- **`.dll` ile `.exe` aynı formattadır** — fark PE header'daki bir bayrak ve entry point varlığı. .NET'te uygulama kodun **her zaman `.dll`**'dir
- Metadata sayesinde reflection mümkün — tip bilgisi assembly'nin içinde taşınır
- Assembly identity: `name, version, culture, publicKeyToken`; strong naming
- **İnceleme araçları:** ILSpy (ücretsiz, tercih edilen), dnSpy, JetBrains dotPeek, `ildasm`, `dotnet-ildasm`
- Decompile edilebilirlik — kodunun okunabilir olduğu gerçeği ve obfuscation'ın sınırları

**Lab:** Kendi `bin/Debug/net10.0/MyApp.dll` dosyanı ILSpy ile aç. Bir metodun IL'ini oku. Yazdığın C#'a "geri" decompile et, farkları gör.

### 2.3 MSBuild ve Build Süreci 🟡
- `.csproj` = **MSBuild proje dosyası** (XML). SDK-style (`<Project Sdk="Microsoft.NET.Sdk">`) vs eski verbose format
- MSBuild kavram modeli: **Property** (`<TargetFramework>`), **Item** (`<PackageReference>`), **Target** (çalıştırılabilir adım), **Task** (atomik iş)
- Build hattı: `Restore` → `Build` (→ `Publish`)
- **Restore ne yapar:** paket grafiğini çözer, indirir, `obj/project.assets.json` üretir
  - Transitive dependency, versiyon çakışması çözümü (**nearest wins** kuralı), `<PackageReference>` vs eski `packages.config`
  - Floating version (`1.2.*`) riski, lock file (`packages.lock.json`) ile deterministik restore
- **`Directory.Build.props` / `.targets`** — tüm projelere ortak ayar (repo genelinde tek yerden yönetim)
- **Central Package Management** (`Directory.Packages.props` + `ManagePackageVersionsCentrally`)
- **Debug vs Release farkı:**

  | | Debug | Release |
  |---|-------|---------|
  | JIT optimizasyonu | Kapalı | Açık |
  | `DEBUG` sembolü | Tanımlı | Tanımsız |
  | Inline / dead code elimination | Yok | Var |
  | Değişken ömrü | Uzatılmış (debug için) | Kısaltılmış |
  | Debug deneyimi | Tam | Kısıtlı (değişkenler "optimized away") |

- Incremental build ve neden bazen `dotnet clean` gerekir
- `TreatWarningsAsErrors`, `WarningsAsErrors`, `NoWarn`, `.editorconfig`
- **Deterministic build** — aynı girdi → byte-byte aynı çıktı (CI doğrulaması, reproducible build)
- `dotnet build -v detailed` ile ayrıntı; **`dotnet build -bl`** ile binary log + MSBuild Structured Log Viewer (build teşhisinin en güçlü aracı)
- Multi-targeting (`<TargetFrameworks>net8.0;net10.0</TargetFrameworks>`) ve `#if NET10_0_OR_GREATER`

**Lab:** `dotnet build -bl` çalıştır, `msbuild.binlog` dosyasını Structured Log Viewer'da aç; hangi target'ın ne kadar sürdüğünü gör.

### 2.4 `obj/` ve `bin/` — Hangi Dosya Ne İşe Yarar? 🟢
- **`obj/` = ara çıktılar (intermediate)**
  - `project.assets.json` — restore sonucu, tam bağımlılık grafiği
  - `*.nuget.g.props` / `*.nuget.g.targets` — NuGet'in ürettiği MSBuild import'ları
  - `Debug/net10.0/` → derlenmiş `.dll`, `.pdb`, `AssemblyInfo.cs` (otomatik üretilen), `.editorconfig` türevleri
  - `generated/` → source generator çıktıları (`EmitCompilerGeneratedFiles=true` ile)
- **`bin/` = son çıktı**

  | Dosya | Ne işe yarar |
  |-------|--------------|
  | `MyApp.dll` | **Senin kodun** — IL + metadata |
  | `MyApp.exe` | apphost — `.dll`'i başlatan native launcher (Windows) |
  | `MyApp.pdb` | Debug sembolleri (satır numarası eşlemesi) |
  | `MyApp.deps.json` | Bağımlılık manifesti: hangi assembly, hangi yol, hangi RID |
  | `MyApp.runtimeconfig.json` | Hangi runtime sürümü, roll-forward, GC/threading ayarları |
  | `*.dll` (diğerleri) | NuGet bağımlılıkları (framework dışı olanlar) |
  | `appsettings.json` | `CopyToOutputDirectory` ile kopyalanan içerik dosyaları |

- Neden ikisi de `.gitignore`'da olmalı
- `bin/`'i silmeden `obj/`'yi silmek neden bazen tek başına yetmez
- `CopyToOutputDirectory` vs `CopyToPublishDirectory` — `launchSettings.json` neden publish'e gitmez

**Lab:** Boş bir console app oluştur, build et, `bin/Debug/net10.0/` içeriğini listele; her dosyayı yukarıdaki tabloyla eşleştir. `runtimeconfig.json` ve `deps.json`'ı açıp oku.

### 2.5 PDB & Debug Sembolleri 🟡
- PDB nedir: **IL offset ↔ kaynak satır** eşlemesi + local değişken adları
- Portable PDB (cross-platform, modern) vs Windows PDB; **embedded PDB** (`<DebugType>embedded</DebugType>`)
- Release build'de stack trace'in **satır numarası gösterebilmesi için PDB gerekir** — production teşhisinde kritik
- **Source Link** — NuGet paketinin kaynak koduna debugger ile inebilmek
- Symbol server, `.snupkg` ve nuget.org sembol yayını
- Production'a PDB göndermeli mi: teşhis kolaylığı vs tersine mühendislik kolaylığı trade-off'u
- `DebuggerDisplay`, `DebuggerStepThrough`, `DebuggerHidden` attribute'ları

### 2.6 `.exe` Nasıl Oluşuyor? — apphost & Başlatma Zinciri 🟡→🔴
> **"C# kodu nasıl exe oluyor?"** sorusunun gerçek cevabı — ve çoğu adayın bilmediği kısım.

- **Yanıt:** Olmuyor. Senin kodun `.dll`'dir. `.exe` ayrı, **native** bir launcher'dır.
- **apphost** nedir: .NET SDK ile gelen küçük native yürütülebilir şablon
- Build sırasında ne olur:
  1. SDK, apphost şablonunu `bin/` içine `MyApp.exe` olarak kopyalar
  2. İçindeki placeholder string'i **senin `.dll` adınla patch'ler**
  3. Sonuç: çalıştırıldığında `MyApp.dll`'i arayıp başlatan bir exe
- `<UseAppHost>false</UseAppHost>` → `.exe` üretilmez, sadece `dotnet MyApp.dll` ile çalışır
- Linux/macOS'ta uzantısız executable, Windows'ta `.exe`
- **Başlatma zinciri (startup chain):**
  ```
  MyApp.exe          (apphost — native kod, .NET değil)
    ↓
  hostfxr            "Hangi runtime?"
                     · runtimeconfig.json'ı okur
                     · framework referansını çözer (Microsoft.NETCore.App / AspNetCore.App)
                     · roll-forward politikasını uygular (patch/minor/major)
                     · kurulu runtime'ları tarar, uygun sürümü seçer
    ↓
  hostpolicy         "Hangi assembly'ler, nereden?"
                     · deps.json'ı okur
                     · probing path'lerini kurar (app dir, NuGet fallback)
                     · TPA (Trusted Platform Assemblies) listesini oluşturur
    ↓
  coreclr            CLR'ı başlatır
                     · GC heap'ini kurar
                     · Default AssemblyLoadContext'i oluşturur
                     · type system'i ayağa kaldırır
    ↓
  MyApp.dll          Entry point (Main) bulunur ve çağrılır
  ```
- **`runtimeconfig.json` içeriği:** `tfm`, `framework` (ad + sürüm), `rollForward`, `configProperties`
  (`System.GC.Server`, `System.GC.Concurrent`, `System.Runtime.TieredCompilation`, `System.Globalization.Invariant`)
- **`deps.json`:** hangi kütüphane hangi yoldan, hangi RID için — eksik/yanlış olursa `FileNotFoundException`
- **`dotnet MyApp.dll`** ile çalıştırma: `dotnet` muxer'ı apphost'un yerini alır (aynı zinciri kendisi başlatır)
- Framework-dependent uygulamanın runtime bulamaması: *"You must install .NET to run this application"* hatası ve `DOTNET_ROOT`
- `dotnet --info`, `dotnet --list-runtimes`, `dotnet --list-sdks` ile ortam teşhisi
- Roll-forward politikaları: `LatestPatch` (varsayılan), `Minor`, `Major`, `Disable`

**Lab:** `UseAppHost=false` ile build et — `.exe` kaybolur, `dotnet MyApp.dll` hâlâ çalışır. Sonra `runtimeconfig.json`'daki sürümü kurulu olmayan bir sürüme elle değiştir, hatayı gör.

### 2.7 `dotnet run` vs `build` vs `publish` 🟢→🟡

| Komut | Ne yapar | Çıktı | Ne zaman |
|-------|----------|-------|----------|
| `dotnet restore` | Bağımlılıkları çözer/indirir | `obj/project.assets.json` | CI'da explicit adım |
| `dotnet build` | Derler (implicit restore) | `bin/<config>/<tfm>/` | Geliştirme, derleme doğrulama |
| `dotnet run` | Build eder **ve çalıştırır** | `bin/` + çalışan process | **Sadece geliştirme** |
| `dotnet watch run` | Dosya değişiminde yeniden çalıştırır / hot reload | — | Geliştirme döngüsü |
| `dotnet publish` | **Dağıtıma hazır, kendi kendine yeten** klasör üretir | `bin/<config>/<tfm>/publish/` | **Deployment** |
| `dotnet test` | Test projesini build edip çalıştırır | Test sonuçları | CI |
| `dotnet pack` | NuGet paketi üretir | `.nupkg` | Kütüphane yayını |

**`build` ile `publish` arasındaki gerçek farklar:**
- `build` sadece derler; bağımlılıkların tamamını tek klasöre toplamayı garanti etmez
- `publish` **tüm bağımlılıkları** toplar, `_framework`/statik dosyaları ekler, `web.config` üretir (IIS)
- `publish` RID-specific olabilir, `build` genelde portable
- **Trimming, single-file, AOT sadece `publish` aşamasında** uygulanır
- `publish` varsayılan olarak Release'dir (.NET 8+); `build` ve `run` varsayılan Debug

**`dotnet run` neden production'da kullanılmaz:**
- SDK gerektirir (production sunucusunda sadece runtime olmalı)
- Her çalıştırmada build kontrolü yapar → yavaş, öngörülemez başlangıç
- Build araçları ve kaynak kod sunucuda bulunur → güvenlik yüzeyi
- Container image'ı gereksiz şişer
- `--no-build` bile SDK bağımlılığını kaldırmaz

**Ek detaylar:**
- `dotnet run --project ./src/Api`, `dotnet run -- --arg değeri` (`--` sonrası uygulamaya geçer)
- `--launch-profile` ve `launchSettings.json` — **sadece geliştirme**, publish çıktısına dahil edilmez (çok sık yanlış anlaşılır; production ayarları env var / appsettings'ten gelir)
- `dotnet run` ile ortam: `ASPNETCORE_ENVIRONMENT` varsayılan `Development`
- **.NET 10 file-based apps:** `dotnet run app.cs` — `.csproj` olmadan tek dosya çalıştırma; `#:package` direktifi ile paket referansı

**Lab:** Aynı projeyi `dotnet build` ve `dotnet publish` ile çıkar; iki klasörü `diff` ile karşılaştır, farkı listele.

### 2.8 Deployment Modelleri 🟡→🔴

| Model | Runtime gereksinimi | Boyut | Cold start | Not |
|-------|--------------------|-------|-----------|-----|
| **Framework-dependent (FDD)** | Makinede kurulu | ~yüzlerce KB | Orta | Varsayılan; güvenlik yamaları runtime'dan gelir |
| **Self-contained (SCD)** | Yok, uygulamayla gelir | ~60-70 MB | Orta | Runtime'ı **sen** güncellemek zorundasın |
| **Single-file** | Modele göre | Tek dosya | Orta | Dağıtım kolaylığı; native kütüphaneler için extract olabilir |
| **Trimmed** | Uygulamayla | Belirgin küçülme | Orta | Reflection riski, trim uyarıları |
| **ReadyToRun (R2R)** | Kurulu veya SCD | Artar | **İyileşir** | IL + önceden derlenmiş native; JIT hâlâ var |
| **Native AOT** | Yok | ~1-10 MB | **En hızlı** | JIT yok, reflection/dynamic kısıtlı |

- **RID (Runtime Identifier):** `win-x64`, `linux-x64`, `linux-musl-x64` (Alpine), `linux-arm64`, `osx-arm64`
- Komut örnekleri:
  ```bash
  dotnet publish -c Release                                    # FDD, portable
  dotnet publish -c Release -r linux-x64 --self-contained      # SCD
  dotnet publish -c Release -r linux-x64 -p:PublishSingleFile=true
  dotnet publish -c Release -r linux-x64 -p:PublishTrimmed=true
  dotnet publish -c Release -r linux-x64 -p:PublishAot=true
  dotnet publish -c Release -r linux-x64 -p:PublishReadyToRun=true
  ```
- **Trimming:** kullanılmayan IL'in atılması; `TrimMode=full` vs `partial`; **IL2xxx uyarıları** ciddiye alınmalı — reflection ile erişilen tip atılabilir → runtime'da patlar
- **Native AOT kısıtları:** `Assembly.Load`, `Reflection.Emit`, dinamik kod üretimi, bazı serializer'lar çalışmaz → **source generator** ile çözüm (JSON, logging, regex, DI)
- AOT'un kazandırdığı: çok hızlı cold start, düşük bellek, küçük image, runtime kurulumu yok
- AOT'un kaybettirdiği: build süresi, platform-spesifik çıktı, kütüphane uyumluluğu, JIT'in runtime optimizasyonları
- **Karar rehberi:**
  - Uzun ömürlü web API, container → FDD + chiseled image (küçük + güvenlik yaması kolay)
  - Serverless / Function / CLI aracı → Native AOT veya R2R (cold start kritik)
  - Kurulum gerektirmeyen masaüstü/araç dağıtımı → self-contained single-file
- Docker ile ilişkisi: [Bölüm 13.2](#132-net-için-dockerfile-)

**Lab:** Aynı "Hello World" API'yi FDD / SCD / single-file / trimmed / AOT olarak publish et. **Klasör boyutlarını ve ilk isteğe kadar geçen süreyi ölç**, tabloya dök. Fark seni şaşırtacak.

### 2.9 Runtime'da Ne Oluyor? — JIT ve Çalıştırma 🟡→🔴
- IL makine kodu **değildir** → **JIT (Just-In-Time)** derleyici, metot **ilk çağrıldığında** onu native koda çevirir
- Method stub / pre-stub mekanizması: ilk çağrı JIT'i tetikler, sonraki çağrılar doğrudan native koda gider
- **Tiered compilation:**
  - **Tier 0** — hızlı derle, az optimize et (uygulama hızlı ayağa kalksın)
  - **Tier 1** — sık çağrılan metodlar (~30 çağrı eşiği) tam optimizasyonla yeniden derlenir
  - **OSR (On-Stack Replacement)** — uzun süren döngüdeki bir metodu **çalışırken** Tier 1'e yükseltme
- **Dynamic PGO** (.NET 8+ varsayılan açık): runtime profili ile guarded devirtualization, daha akıllı inline kararları
- JIT optimizasyonları: inlining, bounds check elimination, constant folding/propagation, dead code elimination, loop hoisting, register allocation, SIMD intrinsics
- **Neden Release build'i debug etmek zordur:** inline edilen metodlar stack'te görünmez, değişkenler "optimized away" olur
- `[MethodImpl(MethodImplOptions.NoInlining / AggressiveInlining)]` — ne zaman meşru
- ReadyToRun ile JIT yükünü öne alma; AOT ile tamamen kaldırma
- **Assembly yükleme:**
  - `AssemblyLoadContext` (Default ALC, collectible ALC)
  - Plugin mimarisi ve izole yükleme (eski `AppDomain`'in yerini alan model)
  - `AssemblyResolve` / `Resolving` event'leri
  - Probing: uygulama klasörü → `deps.json` yönlendirmesi → NuGet fallback folder
  - Assembly version conflict ve binding redirect'in Core'daki karşılığı
- **Type loading:** tip ilk kullanıldığında yüklenir; method table (vtable) kurulumu
- **Static constructor ne zaman çalışır:** `beforefieldinit` semantiği — tam olarak ne zaman tetiklendiğinin garantisi (ince ama sevilen mülakat sorusu); thread-safety garantisi CLR tarafından verilir
- Managed heap kurulumu ve GC başlangıcı (detay: [Bölüm 11.2](#112-bellek-yönetimi--gc-))
- **Uygulama sonlanması:** `Main` dönüşü, `Environment.Exit`, `ProcessExit` event'i, finalizer thread ve finalizer'ların **garantili çalışmaması**
- Ortam değişkenleriyle runtime davranışını değiştirme: `DOTNET_TieredCompilation`, `DOTNET_TieredPGO`, `DOTNET_ReadyToRun`, `DOTNET_gcServer`

**Lab:** Bir döngüde çok çağrılan metot yaz; `DOTNET_TieredCompilation=0` ile ve varsayılanla çalıştır, startup ve steady-state performansını karşılaştır.

### 2.10 Gözlem Laboratuvarı 🟡
Bu bölümün `src/` klasöründe yapılacaklar — **okumak değil, görmek**:

1. Console app oluştur → `bin/Debug/net10.0/` içeriğini listele, her dosyanın görevini yaz
2. `ILSpy` ile kendi `.dll`'ini aç, bir metodun IL'ini oku
3. sharplab.io'da `foreach` / `async` / lambda / `record` lowering'ini incele
4. `UseAppHost=false` ile build et — `.exe`'nin kaybolduğunu, `dotnet app.dll`'in çalıştığını gör
5. `runtimeconfig.json` + `deps.json` içeriğini satır satır yorumla
6. `runtimeconfig.json`'daki runtime sürümünü bozup hata mesajını gözlemle
7. FDD / SCD / single-file / trimmed / AOT publish et → **boyut + cold start tablosu** çıkar
8. `dotnet build -bl` → binlog'u Structured Log Viewer'da aç, en yavaş target'ı bul
9. Debug vs Release IL'ini ILSpy'da karşılaştır
10. Reflection kullanan bir kodu `PublishTrimmed` ile yayınla → runtime hatasını gör, `[DynamicDependency]` veya source generator ile düzelt
11. Aynı uygulamayı `aspnet` ve `aspnet:*-chiseled` ve AOT Docker image'ı olarak paketle, boyutları karşılaştır

**Bu bölümün mülakat çıktısı:** "`dotnet run` ile `dotnet publish` arasındaki fark nedir, production'a hangisiyle çıkarsın ve neden?", "C# kodu nasıl `.exe` oluyor?", "IL nedir, neden var?", "Native AOT ne kazandırır, ne kaybettirir?" sorularına **mekanizmayı anlatarak** cevap verebilmek.

---

## 3. .NET Platform Servisleri

> Bu bölüm "C# biliyorum ama .NET'i bilmiyorum" boşluğunu kapatır. Mülakatlarda **en çok atlanan ve en çok sorulan** kısım burasıdır.
> Platformun kendisi (derleme/çalıştırma) [Bölüm 2](#2-derleme-build--çalıştırma)'de; burası uygulamanın üzerine kurulduğu **servisler**.

### 3.0 .NET Sürüm Ekosistemi 🟢→🟡
- .NET Framework vs .NET Core vs .NET 5+ (birleşik .NET) tarihçesi ve neden hâlâ önemli
- **.NET 10 LTS** (Kasım 2025 → Kasım 2028); LTS vs STS release cadence (her Kasım yeni sürüm)
- .NET Standard'ın rolü ve artık neden gerilediği
- `TargetFramework` seçimi, multi-targeting, `net10.0` vs `net10.0-windows`
- Sürüm yükseltme stratejisi ve breaking change taramaları
- .NET 11 önizleme başlıkları: runtime-native async, ASP.NET Core'da Zstandard, EF Core vector search, C#'ta union types

### 3.1 Generic Host & Uygulama Yaşam Döngüsü 🟡
- `IHost`, `IHostBuilder`, `HostApplicationBuilder` (.NET 6+ minimal hosting model)
- `WebApplication.CreateBuilder()` altında ne oluyor
- `IHostedService` ve `BackgroundService` lifecycle: `StartAsync` → `ExecuteAsync` → `StopAsync`
- Graceful shutdown: `IHostApplicationLifetime`, `ApplicationStopping`, shutdown timeout
- **Startup'ta async iş yapma** (migration, warm-up) — doğru ve yanlış yolları
- `IStartupFilter`
- Container/Kubernetes'te SIGTERM handling
- Startup performansı ve cold start

### 3.2 Configuration 🟢→🟡
> Mülakatta "configuration nasıl çalışır" sorusu, kıdemi ayıran sorulardan.

- **Configuration provider zinciri ve öncelik sırası:**
  1. `appsettings.json`
  2. `appsettings.{Environment}.json`
  3. User Secrets (sadece Development)
  4. Environment variables
  5. Command-line arguments
  6. (Azure Key Vault, Azure App Configuration, custom provider)
- Sonra gelen öncekini **ezer** — sıralamanın önemi
- Hiyerarşik key'ler ve env var karşılığı: `Logging:LogLevel:Default` → `Logging__LogLevel__Default`
- `IConfiguration`, `IConfigurationSection`, `GetValue<T>`, `GetSection`, `Bind`
- **Options Pattern:**
  - `IOptions<T>` — singleton, uygulama ömrü boyunca sabit
  - `IOptionsSnapshot<T>` — scoped, request başına yeniden okunur
  - `IOptionsMonitor<T>` — singleton, değişiklik bildirimi (`OnChange`) ile
  - Hangisi ne zaman (klasik mülakat sorusu)
- Options validation: `ValidateDataAnnotations()`, `Validate()`, `ValidateOnStart()`
- Named options (`IOptionsFactory`)
- `reloadOnChange` ve dosya izleme maliyeti
- **Secrets yönetimi:** User Secrets (dev), environment variables, Key Vault (prod) — appsettings'e secret koymama kuralı
- Custom configuration provider yazma
- Azure App Configuration ile merkezi config ve feature flag

### 3.3 Dependency Injection 🟢→🔴
- DI nedir, Inversion of Control ile ilişkisi, Service Locator neden anti-pattern
- Built-in container (`Microsoft.Extensions.DependencyInjection`)
- **Lifetime'lar:**
  - `Transient` — her istekte yeni
  - `Scoped` — HTTP request (veya scope) başına bir tane
  - `Singleton` — uygulama ömrü boyunca bir tane
- **Captive dependency** problemi: Singleton içine Scoped enjekte etmek — neden patlar, nasıl tespit edilir (`ValidateScopes`)
- Singleton içinde scoped kullanma: `IServiceScopeFactory` ile manuel scope
- `IServiceProvider`, `ActivatorUtilities`
- Keyed services (.NET 8+): `[FromKeyedServices("name")]`
- Aynı interface'e birden fazla implementasyon: `IEnumerable<IService>` enjeksiyonu
- `TryAdd*`, `Replace`, `Decorate` (Scrutor)
- Constructor injection vs property injection vs method injection
- Decorator pattern DI ile (Scrutor `.Decorate<>()`)
- Assembly scanning ile otomatik kayıt (Scrutor)
- 3rd party container: Autofac (ne zaman gerekir — property injection, interceptor, modül)
- DI'ın performans etkisi ve `IServiceProvider` doğrulama

### 3.4 Middleware Pipeline 🟡
> ASP.NET Core'un kalbi. "Request geldiğinde ne oluyor?" sorusunun cevabı.

- Request pipeline zihinsel modeli: iç içe geçmiş halkalar (matruşka), request in → response out
- **Sıralamanın kritikliği** — önerilen sıra:
  ```
  ExceptionHandler → HSTS → HttpsRedirection → StaticFiles →
  Routing → CORS → Authentication → Authorization →
  RateLimiter → OutputCache → Endpoints
  ```
  - Neden `UseAuthentication` `UseAuthorization`'dan önce
  - Neden `UseCors` `UseRouting` ile `UseEndpoints` arasında
- `Use`, `Run`, `Map`, `MapWhen`, `UseWhen` farkları
- Terminal middleware vs pass-through middleware
- `RequestDelegate` ve `next` çağrısı — `next` çağırmazsan ne olur
- **Custom middleware yazma**: convention-based (`InvokeAsync`) vs `IMiddleware` (DI-friendly, scoped)
- Middleware'e scoped servis enjekte etme (constructor değil, `InvokeAsync` parametresi!)
- Global exception handling middleware ve `IExceptionHandler` (.NET 8+)
- Request/response body okuma (`EnableBuffering`) ve stream tuzakları
- Correlation ID middleware örneği
- `HttpContext`, `HttpContext.Items`, `IHttpContextAccessor` (ve neden dikkatli kullanılmalı)
- Middleware vs Filter vs Endpoint filter — karar tablosu
- Performans: middleware sayısının etkisi, short-circuit

### 3.5 Filters & Attributes 🟡
- Filter pipeline sırası: Authorization → Resource → Action → Exception → Result
- `IActionFilter` / `IAsyncActionFilter`
- `IResourceFilter` — caching için ideal nokta
- `IExceptionFilter`
- `IResultFilter`
- `IAuthorizationFilter`
- Filter scope: global, controller, action — ve çalışma sırası
- `ServiceFilter` vs `TypeFilter` vs doğrudan attribute (DI ile filter)
- `IEndpointFilter` (Minimal API karşılığı, .NET 7+)
- Middleware yerine filter ne zaman tercih edilir (model binding sonrasına erişim)
- Model validation filter örneği

### 3.6 Logging 🟢→🟡
- `ILogger<T>` ve kategori isimlendirmesi
- **Log level'ları ve doğru kullanımı:** Trace, Debug, Information, Warning, Error, Critical
- **Structured logging** — string interpolation neden yanlış:
  ```csharp
  logger.LogInformation("User {UserId} logged in", userId);   // ✅
  logger.LogInformation($"User {userId} logged in");          // ❌
  ```
- Log scope (`BeginScope`) ile bağlam taşıma
- `LoggerMessage` source generator — yüksek performanslı logging
- Provider'lar: Console, Debug, EventSource, ApplicationInsights
- **Serilog** ve sink'ler (Console, Seq, File, Elasticsearch, App Insights), enricher'lar
- NLog alternatifi
- Log filtreleme ve `appsettings.json` üzerinden level yönetimi
- Hassas veri loglamama (PII, token, şifre) — GDPR/KVKK
- Log maliyeti ve sampling
- **OpenTelemetry** ile logs/metrics/traces birleşimi (detay [Bölüm 10](#10-microservices--dağıtık-sistemler))

### 3.7 Background Services & Zamanlanmış Görevler 🟡
- `BackgroundService` / `IHostedService` implementasyonu
- `ExecuteAsync` içinde sonsuz döngü + `CancellationToken` doğru kullanımı
- Background service'te **scoped servis kullanımı** (`IServiceScopeFactory`) — çok sık hata
- `PeriodicTimer` (.NET 6+) — `Timer`'a modern alternatif
- Exception olursa host çöker mi? (`BackgroundServiceExceptionBehavior`)
- Kuyruk tabanlı background işleme (`Channel<T>` ile in-process queue)
- **Hangfire** — persistent job, dashboard, retry, recurring job
- **Quartz.NET** — cron tabanlı zamanlama, cluster desteği
- **Coravel** — hafif alternatif
- Çoklu instance'ta job çakışması ve distributed lock ihtiyacı
- Worker Service template (`dotnet new worker`)
- Ne zaman background service, ne zaman ayrı bir servis/function

### 3.8 Ortamlar & Deployment Konfigürasyonu 🟡
- `ASPNETCORE_ENVIRONMENT` / `DOTNET_ENVIRONMENT`
- `IWebHostEnvironment.IsDevelopment()` ve ortama göre davranış
- Development exception page vs production hata sayfası
- `dotnet publish` çıktıları: framework-dependent vs self-contained, single-file, trimmed, AOT
- Runtime identifier (RID)
- Health check endpoint'leri (`/health/live`, `/health/ready`) ve k8s probe eşleşmesi
- Kestrel konfigürasyonu, reverse proxy (nginx, IIS, YARP) arkasında çalışma
- `ForwardedHeaders` middleware (proxy arkasında gerçek IP ve scheme)
- Feature flag'ler (`Microsoft.FeatureManagement`)

---

## 4. Multithreading, Concurrency & Async

> Senior mülakatlarının en ayırt edici bölümü. "Tanımı biliyor mu" değil, **"production'da bunu yaşadı mı"** ölçülür.

### 4.1 Temel Kavramlar 🟢
- Process vs Thread vs Task — hangi seviyede ne
- **Concurrency vs Parallelism** — aynı şey değil (klasik açılış sorusu)
- **CPU-bound vs I/O-bound** iş yükü ayrımı — hangisinde ne kullanılır
  - CPU-bound → `Task.Run`, `Parallel`, PLINQ
  - I/O-bound → `async`/`await`, thread bloklamama
- Context switch maliyeti
- Thread stack boyutu (varsayılan 1 MB) ve binlerce thread'in maliyeti
- Amdahl yasası — paralelleştirmenin sınırı
- Thread safety nedir, immutability'nin rolü
- Foreground vs background thread

### 4.2 Thread & ThreadPool 🟡
- `Thread` sınıfı ile manuel thread — ne zaman hâlâ gerekli (nadiren)
- `Thread.Sleep` vs `await Task.Delay` — thread bloklama farkı
- `ThreadPool` nasıl çalışır: work queue, worker thread, I/O completion port
- **Thread injection / hill-climbing algoritması** — pool'un yavaş büyümesi
- `ThreadPool.SetMinThreads` — ne zaman ve neden (dikkatli!)
- **Thread pool starvation** — sebepleri, belirtileri (gecikme artışı), teşhisi
  - En yaygın sebep: async kod içinde `.Result` / `.Wait()` ile senkron bloklama
  - `dotnet-counters` ile `ThreadPool Thread Count` izleme
- `Thread.Yield`, `SpinWait` ve busy-wait
- `[ThreadStatic]`, `ThreadLocal<T>`, `AsyncLocal<T>` — farkları

### 4.3 Task & Task Parallel Library (TPL) 🟡
- `Task` ve `Task<T>` — "gelecekte tamamlanacak iş" soyutlaması
- `Task` vs `Thread` — Task bir thread değildir
- `Task.Run` vs `Task.Factory.StartNew` (ve `TaskCreationOptions.LongRunning`)
- `TaskCompletionSource<T>` — event-based API'yi Task'e çevirme
- `Task.WhenAll` / `Task.WhenAny` / `Task.WhenEach` (.NET 9+)
- `Task.WhenAll` içinde exception davranışı — `AggregateException` ve sadece ilkinin fırlatılması
- `ValueTask<T>` — ne zaman kullanılır, **tek kez await kuralı**
- Task continuation (`ContinueWith`) — neden modern kodda `await` tercih edilir
- Task scheduler ve `TaskScheduler.Current`
- Fire-and-forget tehlikesi ve güvenli yapma yöntemleri
- Unobserved task exception

### 4.4 async/await Derinlemesine 🟡→🔴
- `async`/`await`'in derleyici tarafından **state machine**'e dönüşümü (sharplab.io ile göster)
- `await` noktasında ne olur: thread serbest bırakılır, continuation kaydedilir
- **`async void` neden kötü** — exception yakalanamaz, tamamlanma beklenemez (istisna: event handler)
- **Deadlock senaryosu:** `.Result` / `.Wait()` + `SynchronizationContext`
  - ASP.NET Core'da `SynchronizationContext` yok → klasik deadlock yok, ama thread pool starvation var
  - WinForms/WPF/eski ASP.NET'te gerçek deadlock
- `ConfigureAwait(false)` — kütüphane kodunda neden, uygulama kodunda neden gereksiz
- `ConfigureAwait(ConfigureAwaitOptions)` (.NET 8+)
- Async all the way — senkron/asenkron karıştırmama kuralı
- Sync-over-async ve async-over-sync anti-pattern'leri
- `IAsyncEnumerable<T>` ve `await foreach` — streaming senaryoları
- `IAsyncDisposable` ve `await using`
- `async` metod allocation maliyeti, `ValueTask` ile azaltma
- Async exception yayılımı ve stack trace kalitesi
- **.NET 11 önizleme:** runtime-native async (state machine yerine runtime desteği)

### 4.5 Senkronizasyon Primitifleri 🟡→🔴
| Primitif | Kullanım | Async destekler mi |
|----------|----------|--------------------|
| `lock` / `Monitor` | Aynı process içi karşılıklı dışlama | ❌ |
| `System.Threading.Lock` (C# 13) | Modern `lock` nesnesi, daha hızlı | ❌ |
| `Mutex` | Process'ler arası | ❌ |
| `Semaphore` | Process'ler arası, N eşzamanlı | ❌ |
| `SemaphoreSlim` | Process içi, N eşzamanlı | ✅ `WaitAsync` |
| `ReaderWriterLockSlim` | Çok okuma / az yazma | ❌ |
| `Interlocked` | Atomik sayaç/flag, lock'suz | — |
| `SpinLock` | Çok kısa kritik bölüm | ❌ |
| `Barrier` | Fazlı paralel algoritma | ❌ |
| `CountdownEvent` | N iş bitene kadar bekle | ❌ |
| `ManualResetEventSlim` | Sinyalleşme | ❌ |

- **`lock` içinde `await` kullanılamaz** — neden ve çözümü (`SemaphoreSlim`)
- `lock(this)` / `lock(typeof(X))` / `lock("string")` neden yanlış
- Lock granularity: coarse vs fine-grained — trade-off
- `Interlocked.Increment`, `CompareExchange` ile lock-free sayaç
- `volatile` keyword ve memory barrier — ne yapar, ne yapmaz
- `Monitor.Wait` / `Pulse` ile condition variable

### 4.6 Concurrent Collections 🟡
- `ConcurrentDictionary<TKey,TValue>`
  - `GetOrAdd` / `AddOrUpdate` — **factory delegate birden fazla çalışabilir** (kritik detay)
  - `Count` ve enumeration'ın snapshot semantiği
- `ConcurrentQueue<T>`, `ConcurrentStack<T>`, `ConcurrentBag<T>`
- `BlockingCollection<T>` — producer/consumer, `CompleteAdding`
- `ImmutableDictionary` / `ImmutableList` ile copy-on-write yaklaşımı
- Concurrent collection kullanmak **her zaman** thread-safe kod demek değil (compound operation problemi)
- Lock'lu `Dictionary` vs `ConcurrentDictionary` — ne zaman hangisi daha hızlı

### 4.7 Parallel & PLINQ 🟡
- `Parallel.For`, `Parallel.ForEach`, `Parallel.Invoke`
- `Parallel.ForEachAsync` (.NET 6+) — async iş için doğru araç
- `ParallelOptions.MaxDegreeOfParallelism` — neden sınırlamak gerekir
- `ParallelLoopState` ile erken çıkış (`Break` vs `Stop`)
- Partitioner ve chunk boyutu
- **PLINQ:** `AsParallel()`, `WithDegreeOfParallelism`, `AsOrdered()`
- PLINQ ne zaman **yavaşlatır** (küçük koleksiyon, ucuz işlem, sıra korunması)
- Paralel kodda exception toplama (`AggregateException`)
- Thread-local state ile aggregation (`Parallel.For` overload'ları)
- False sharing ve cache line — paralel performansın gizli düşmanı

### 4.8 Channels & Pipeline'lar 🔴
- `System.Threading.Channels` — modern producer/consumer
- `Channel.CreateUnbounded` vs `CreateBounded` — **backpressure** kavramı
- `BoundedChannelFullMode`: Wait, DropOldest, DropNewest, DropWrite
- `ChannelReader.ReadAllAsync` ile `await foreach`
- Single-reader / single-writer optimizasyonları
- `Channel` vs `BlockingCollection` vs `ConcurrentQueue` karşılaştırması
- Pipeline mimarisi: aşamalar arası channel ile veri akışı
- TPL Dataflow (`System.Threading.Tasks.Dataflow`) — `BufferBlock`, `TransformBlock`, `ActionBlock`
- Gerçek senaryo: log işleme, batch insert, rate-limited API çağrısı

### 4.9 Cancellation 🟡
- `CancellationToken` / `CancellationTokenSource` modeli
- Cooperative cancellation — kimse zorla durdurulmaz
- `ThrowIfCancellationRequested()` vs `IsCancellationRequested` kontrolü
- `CancellationTokenSource.CreateLinkedTokenSource` — birden fazla iptal kaynağı
- Timeout ile iptal: `CancelAfter`, `new CancellationTokenSource(TimeSpan)`
- **ASP.NET Core'da `HttpContext.RequestAborted`** — istemci bağlantıyı kapattığında
- `CancellationToken` metod imzalarında **en son parametre** konvansiyonu
- `CancellationTokenSource` `IDisposable` — leak riski
- Cancellation ile `OperationCanceledException` yakalama stratejisi
- EF Core, HttpClient ve diğer I/O çağrılarına token geçirmeyi unutmama

### 4.10 Concurrency Problemleri & Bellek Modeli 🔴
- **Race condition** — örnek, tespit, çözüm
- **Deadlock** — dört koşul (mutual exclusion, hold&wait, no preemption, circular wait), kilit sıralaması ile önleme
- **Livelock** ve **starvation**
- **ABA problemi** (lock-free algoritmalarda)
- **Torn read/write** — 64-bit değerlerin atomik olmaması (32-bit'te)
- .NET memory model: instruction reordering, `volatile`, `Volatile.Read/Write`, `Thread.MemoryBarrier`
- `Interlocked` ile memory ordering garantileri
- Double-checked locking — doğru implementasyonu ve `Lazy<T>` ile alternatifi
- `Lazy<T>` ve `LazyThreadSafetyMode`
- Immutability ile concurrency'den kaçınma (en iyi çözüm çoğu zaman)
- Actor model'e giriş (Orleans, Akka.NET, Proto.Actor)

### 4.11 Dağıtık Kilitleme 🔴
- Tek instance'ta `lock` yeterli, çoklu instance'ta değil
- Redis tabanlı distributed lock (Redlock algoritması ve eleştirileri)
- SQL tabanlı lock (`sp_getapplock`, advisory lock in Postgres)
- Azure Blob lease ile lock
- `DistributedLock` NuGet kütüphanesi
- **Idempotency** ile lock ihtiyacını ortadan kaldırma (tercih edilen yaklaşım)
- Leader election
- Optimistic concurrency (rowversion) vs distributed lock — hangisi ne zaman

### 4.12 Concurrency Debugging & Profiling 🔴
- Visual Studio Parallel Stacks / Parallel Watch penceresi
- `dotnet-counters` ile ThreadPool metrikleri (queue length, thread count)
- `dotnet-dump` + `dotnet-stack` ile hang analizi
- `dotnet-trace` ile async akış izleme
- Deadlock dump analizi (`!syncblk`, `!clrstack` — SOS extension)
- Thread pool starvation belirtileri ve doğrulama
- Yük testi ile concurrency bug'larını ortaya çıkarma
- Stress testing, chaos testing

**Bu bölümün mülakat çıktısı:** "Bir API endpoint'i yük altında yavaşlıyor, thread pool starvation olduğundan şüpheleniyorsun — nasıl doğrularsın ve nasıl çözersin?" sorusuna uçtan uca cevap verebilmek.

---

## 5. API Geliştirme

> .NET backend mülakatlarının **merkezi**. Bir API'yi tasarlayabilmek, güvenceye alabilmek ve evrimleştirebilmek.

### 5.1 HTTP & Web Temelleri 🟢
- (Bkz. [0.2](#02-http--web-temelleri-)) — API bölümünün önkoşulu
- Request lifecycle: TCP → TLS → HTTP → Kestrel → middleware → endpoint
- Kestrel vs IIS vs HTTP.sys
- Keep-alive, connection pooling
- HTTP/2 multiplexing ve gRPC'ye etkisi
- HTTP/3 (QUIC) ve .NET desteği

### 5.2 REST API Tasarımı 🟢→🟡
- REST kısıtları: stateless, client-server, cacheable, uniform interface, layered
- **Richardson Maturity Model** (Level 0 → 3 / HATEOAS)
- Resource-oriented isimlendirme: `/api/orders/{id}/items` — çoğul isim, fiil kullanmama
- HTTP method semantiği: GET (safe), PUT (idempotent), POST, PATCH, DELETE
- **Doğru status code seçimi:**
  - `200 OK`, `201 Created` (+ `Location` header), `202 Accepted`, `204 No Content`
  - `400 Bad Request` vs `422 Unprocessable Entity`
  - `401 Unauthorized` vs `403 Forbidden` (en sık karıştırılan)
  - `404 Not Found` vs `410 Gone`
  - `409 Conflict`, `412 Precondition Failed`, `429 Too Many Requests`
  - `500` vs `502` vs `503` vs `504`
- **Pagination:** offset/limit vs cursor-based (keyset) — büyük veri setinde neden cursor
- Filtering, sorting, sparse fieldsets, search
- **Idempotency key** ile POST'u güvenli tekrarlanabilir yapma
- Bulk/batch endpoint tasarımı
- Long-running operation pattern (`202 Accepted` + polling / callback)
- Error response standardı: **RFC 7807 / 9457 `ProblemDetails`**
- API tasarım kılavuzları: Microsoft REST Guidelines, Google AIP, Zalando

### 5.3 ASP.NET Core Web API 🟡
- **Controller-based vs Minimal API** — karar kriterleri, performans farkı
- `[ApiController]` attribute'unun sağladıkları (otomatik 400, binding source inference)
- Routing: attribute routing, route template, route constraint, route parameter transformer
- **Model binding:** `[FromBody]`, `[FromQuery]`, `[FromRoute]`, `[FromHeader]`, `[FromForm]`, `[FromServices]`
- Custom model binder ve `TryParse`/`BindAsync` (Minimal API)
- Content negotiation, `Accept`/`Content-Type`, custom formatter
- Dönüş tipleri: `IActionResult` vs `ActionResult<T>` vs `Results<Ok<T>, NotFound>` (typed results)
- `TypedResults` ve OpenAPI metadata katkısı
- Endpoint metadata, `WithName`, `WithOpenApi`
- Route grupları (`MapGroup`) ile Minimal API organizasyonu
- File upload/download, streaming response, `IFormFile` ve büyük dosya
- Server-Sent Events (SSE) desteği (.NET 10)
- CORS yapılandırması ve preflight

### 5.4 Minimal API & REPR Pattern 🟡
- Minimal API felsefesi, performans avantajı, AOT uyumu
- Endpoint organizasyonu: extension method ile modüler gruplama
- **REPR pattern** (Request-Endpoint-Response) — controller şişmesine alternatif
- **FastEndpoints** kütüphanesi
- **Ardalis.ApiEndpoints**
- Vertical slice architecture ile uyumu ([Bölüm 9](#9-mimari--tasarım))
- `IEndpointFilter` ile cross-cutting concern
- Minimal API'de DI, validation, authorization

### 5.5 Validation & Hata Yönetimi 🟡
- `DataAnnotations` (`[Required]`, `[Range]`, `[EmailAddress]`, custom `ValidationAttribute`)
- **FluentValidation** — karmaşık kurallar, koşullu validation, async validation
- Minimal API'de validation (.NET 10 built-in validation desteği)
- `ModelState` ve otomatik 400 dönüşü
- Domain validation vs input validation ayrımı
- **Global exception handling:** `IExceptionHandler` (.NET 8+) ve `ProblemDetails` üretimi
- Exception'ı HTTP status'a eşleme stratejisi
- Hata mesajlarında bilgi sızdırmama (stack trace, connection string)
- Validation hatalarının i18n/lokalizasyonu
- `Result<T>` pattern ile exception'sız hata akışı (OneOf, ErrorOr, FluentResults)

### 5.6 API Versioning 🟡
- Neden versiyonlama gerekir, breaking vs non-breaking değişiklik
- **Versiyonlama stratejileri:**
  - URL path: `/api/v1/orders` (en yaygın, en açık)
  - Query string: `?api-version=1.0`
  - Header: `X-API-Version` veya `Api-Version`
  - Media type: `Accept: application/vnd.company.v1+json`
- `Asp.Versioning.Http` / `Asp.Versioning.Mvc` paketleri
- Version discovery, deprecation header (`Sunset`, `Deprecation`)
- Swagger'da çoklu versiyon gösterimi
- Backward compatibility kuralları (alan ekleme OK, silme/tip değiştirme breaking)
- Tolerant reader pattern
- Versiyon emeklilik (sunset) politikası

### 5.7 API Dokümantasyonu 🟡
- **OpenAPI (Swagger) spesifikasyonu** — ne işe yarar
- .NET 9+ built-in OpenAPI desteği (`Microsoft.AspNetCore.OpenApi`) vs Swashbuckle vs NSwag
- **Scalar** — modern Swagger UI alternatifi
- XML documentation comment'lerin OpenAPI'ye yansıması
- Örnek request/response ekleme, schema özelleştirme
- OpenAPI'den client kod üretimi (NSwag, Kiota, Refit)
- API contract-first yaklaşımı
- Postman/Bruno/`.http` dosyaları ile API test koleksiyonu

### 5.8 API Güvenliği 🟡→🔴
- **Authentication:**
  - JWT Bearer token akışı, token yapısı (header.payload.signature), imza doğrulama
  - Access token vs refresh token, token ömrü stratejisi
  - Token revocation problemi ve çözümleri (kısa ömür + refresh, blacklist, reference token)
  - OAuth 2.0 grant type'ları: authorization code + PKCE, client credentials, device code
  - OpenID Connect ve ID token
  - IdentityServer / Duende, OpenIddict, Auth0, Entra ID, Keycloak
  - API key authentication (ne zaman yeterli)
  - mTLS (servisler arası)
- **Authorization:**
  - Role-based vs Claim-based vs **Policy-based**
  - `IAuthorizationRequirement` + `AuthorizationHandler` ile custom policy
  - Resource-based authorization (`IAuthorizationService.AuthorizeAsync(user, resource, policy)`)
  - Multi-tenant authorization
- **Rate limiting** (`Microsoft.AspNetCore.RateLimiting`, .NET 7+)
  - Fixed window, sliding window, token bucket, concurrency limiter
  - Partition key ile kullanıcı/IP bazlı limit
  - Distributed rate limiting (Redis)
  - `429` + `Retry-After` header
- CORS doğru yapılandırma (`AllowAnyOrigin` + credentials neden çalışmaz)
- Input validation, mass assignment / over-posting koruması (DTO kullanımı)
- SSRF, XXE, deserialization saldırıları
- Security header'ları: HSTS, CSP, `X-Content-Type-Options`
- Request size limit, timeout, DoS koruması
- Audit logging

### 5.9 gRPC 🟡
- Protocol Buffers (protobuf) ve `.proto` şeması
- Contract-first geliştirme, kod üretimi
- HTTP/2 üzerine kurulu olması — performans kaynağı
- **Dört çağrı tipi:** unary, server streaming, client streaming, bidirectional streaming
- Interceptor (middleware karşılığı)
- Deadline/timeout ve cancellation
- Error handling: status code'lar, `RpcException`
- gRPC-Web (tarayıcı desteği) ve gRPC JSON transcoding
- **REST vs gRPC karar tablosu:** internal servis-servis iletişimi → gRPC; public API → REST
- Protobuf şema evrimi (field number kuralları, backward compatibility)
- .NET'te gRPC servisi ve client oluşturma, DI entegrasyonu
- gRPC health check, load balancing

### 5.10 GraphQL 🟡
- GraphQL nedir, REST'ten farkı (over-fetching / under-fetching çözümü)
- Schema, type system, query / mutation / subscription
- **HotChocolate** (.NET'te fiili standart), GraphQL-dotnet alternatifi
- Resolver ve **N+1 problemi** → `DataLoader` ile çözüm
- Schema-first vs code-first
- Filtering, sorting, projection (HotChocolate `[UseFiltering]`, `[UseProjection]`)
- Pagination: Relay cursor connection spec
- Subscription (WebSocket üzerinden real-time)
- **Güvenlik:** query depth limiting, complexity analysis, persisted queries, introspection kapatma
- Federation / schema stitching (microservice'lerde)
- **Ne zaman GraphQL, ne zaman REST** — dürüst trade-off

### 5.11 Real-time: SignalR & WebSockets 🟡
- WebSocket protokolü, handshake, upgrade
- Ham WebSocket vs SignalR
- **SignalR Hub** modeli, `HubContext` ile sunucudan client'a mesaj
- Transport fallback: WebSocket → SSE → Long polling
- Client tipleri: JS, .NET, Java, strongly-typed hub (`Hub<T>`)
- Group ve user bazlı mesajlaşma
- **Scale-out**: Redis backplane, Azure SignalR Service — sticky session gereksinimi
- Authentication/authorization SignalR'da
- Reconnection, connection lifetime
- **Server-Sent Events (SSE)** — tek yönlü, daha basit alternatif (.NET 10 built-in)
- Long polling ne zaman hâlâ mantıklı
- Karar tablosu: SSE vs WebSocket vs SignalR vs polling
- Gerçek senaryo: bildirim, canlı dashboard, chat, işbirlikçi düzenleme

### 5.12 HTTP Client & Dış Servis Entegrasyonu 🟡
- **`HttpClient` socket exhaustion problemi** — `using (var client = new HttpClient())` neden felaket
- `IHttpClientFactory`: named client, typed client, generated client
- `HttpClientHandler` ömrü ve DNS değişikliği sorunu (`SetHandlerLifetime`)
- **Polly / `Microsoft.Extensions.Http.Resilience`** ile:
  - Retry + exponential backoff + jitter
  - Circuit breaker
  - Timeout
  - Bulkhead isolation
  - Fallback
- `Refit` ile declarative REST client
- Kiota / NSwag ile OpenAPI'den client üretimi
- Serialization ayarları, compression
- Dış servis hatalarını domain hatasına çevirme
- Testte `HttpClient` mock'lama (`DelegatingHandler`, WireMock.Net)

### 5.13 Serialization 🟡
- **`System.Text.Json`** (varsayılan) vs `Newtonsoft.Json` — farklar ve migration tuzakları
- `JsonSerializerOptions`: naming policy (camelCase), ignore null, enum converter
- Custom `JsonConverter<T>`
- **Source-generated serialization** (`[JsonSerializable]`, `JsonSerializerContext`) — AOT ve performans
- Polymorphic serialization (`[JsonDerivedType]`, .NET 7+)
- `JsonNode` / `JsonDocument` / `Utf8JsonReader` — hangisi ne zaman
- DateTime/DateTimeOffset serialization tuzakları, ISO 8601, timezone
- `decimal` precision, büyük sayı ve JavaScript `Number` sınırı
- Circular reference (`ReferenceHandler.Preserve`)
- Alternatifler: MessagePack, Protobuf, System.Text.Json vs binary formatlar

### 5.14 API Performansı 🔴
- Response compression (Brotli, Gzip; .NET 11'de Zstandard)
- **Caching katmanları:**
  - Client cache (`Cache-Control`, `ETag`, `If-None-Match` → `304`)
  - Response caching middleware
  - **Output caching** (.NET 7+) — tag ile invalidation
  - Distributed cache (Redis), HybridCache (.NET 9+)
- Sorgu optimizasyonu ve projection (DTO'ya doğrudan `Select`)
- Pagination zorunluluğu — sınırsız liste dönmeme
- Async I/O ile throughput
- Connection pooling (DB, HttpClient)
- Kestrel limitleri ve tuning
- Payload küçültme: sparse fields, compression, binary format
- Streaming response ile bellek kullanımını düşürme (`IAsyncEnumerable<T>`)
- Yük testi: k6, Bombardier, NBomber, Crank
- Latency percentile'ları (p50/p95/p99) — ortalama neden yalan söyler

### 5.15 API Mimari Pattern'leri 🔴
- **API Gateway** (YARP, Ocelot, Azure API Management) — routing, auth, rate limit, aggregation
- **Backend For Frontend (BFF)** — web/mobile için ayrı API katmanı
- BFF + cookie ile SPA güvenliği (token'ı tarayıcıda tutmama)
- API composition / aggregation pattern
- **Webhook** tasarımı: imza doğrulama (HMAC), retry, idempotency, replay koruması
- Async API pattern: request → `202 Accepted` → status endpoint / callback
- Event-driven API ve AsyncAPI spesifikasyonu
- API contract testing (Pact) ve consumer-driven contracts
- Strangler fig pattern ile eski API'den göç
- Multi-tenancy API tasarımı (tenant resolution: subdomain, header, claim)
- API monetization, quota, SLA

**Bu bölümün mülakat çıktısı:** Sıfırdan bir API tasarlayıp (kaynak modeli, status code'lar, versiyonlama, auth, rate limit, hata formatı, dokümantasyon) savunabilmek; gRPC/GraphQL/REST arasında gerekçeli seçim yapabilmek.

---

## 6. Veri Erişimi & ORM

### 6.1 ADO.NET 🟢
- `DbConnection`, `DbCommand`, `DbDataReader`, `DbTransaction`
- **Parametreli sorgu** ve SQL injection önleme
- Connection pooling nasıl çalışır, connection string'in pool'a etkisi
- `using` ile connection yönetimi, connection leak
- ORM'lerin altında ne olduğunu bilmek — mülakatta değerli

### 6.2 Entity Framework Core 🟡→🔴
- Code-first vs Database-first (scaffold)
- `DbContext` ve `DbSet<T>`, **scoped lifetime** kuralı ve `IDbContextFactory` (Blazor/background service)
- **Migrations:** `dotnet ef migrations add/update/script`, idempotent script, production'da uygulama stratejisi
- **Change tracking** — nasıl çalışır, `AsNoTracking()` ne zaman (%30+ okuma performansı)
- `AsNoTrackingWithIdentityResolution()`
- **Loading stratejileri:** eager (`Include`/`ThenInclude`), lazy (proxy, riskleri), explicit (`Entry().Collection().Load()`)
- **N+1 problemi** — nasıl oluşur, loglardan tespit, çözüm
- `AsSplitQuery()` vs single query — cartesian explosion
- Fluent API vs Data Annotations, `IEntityTypeConfiguration<T>`
- İlişkiler: 1-1, 1-N, N-N (skip navigation), owned entity, table splitting, TPH/TPT/TPC inheritance
- **Global query filters** — soft delete, multi-tenancy
- Value converter, value comparer, JSON column mapping
- Raw SQL: `FromSql`, `ExecuteSql`, `SqlQuery<T>` (.NET 8+)
- **Bulk operations:** `ExecuteUpdate` / `ExecuteDelete` (.NET 7+), EFCore.BulkExtensions
- Compiled query, `DbContext` pooling
- Concurrency: `[Timestamp]` / `IsRowVersion()`, `DbUpdateConcurrencyException` yönetimi
- Interceptor'lar (`SaveChangesInterceptor`, audit alanları)
- Temporal tables, spatial data
- EF Core loglama ve `LogTo` ile üretilen SQL'i görme
- **EF Core performans anti-pattern'leri** checklist'i

### 6.3 Dapper 🟡
- Micro-ORM felsefesi, ne zaman EF Core yerine Dapper
- `Query`, `QueryAsync`, `QueryFirstOrDefault`, `Execute`
- Multi-mapping (`splitOn`), multiple result set (`QueryMultiple`)
- Parametre yönetimi, `DynamicParameters`
- Stored procedure çağırma
- EF Core + Dapper hibrit kullanımı (yazma EF, karmaşık okuma Dapper — CQRS'e doğal uyum)
- Dapper.Contrib, Dapper.FastCrud

### 6.4 Object Mapping 🟡
- Entity ↔ DTO ayrımı neden gerekli (over-posting, API contract kararlılığı)
- **Manuel mapping** — en hızlı ve en açık (varsayılan tercih olmalı)
- **Mapperly** — source generator tabanlı, AOT uyumlu, sıfır runtime maliyeti
- **AutoMapper** — konvansiyon tabanlı; lisans değişikliği ve runtime maliyeti eleştirileri
- Mapping profil testleri (`AssertConfigurationIsValid`)
- Projection ile DB seviyesinde mapping (`Select` → DTO, `ProjectTo`)

### 6.5 Repository, Unit of Work & Specification 🟡
- Repository pattern — ne zaman gerekli, ne zaman **gereksiz soyutlama**
- "`DbContext` zaten Repository + UoW" argümanı ve karşı argümanlar
- Generic repository neden çoğu zaman anti-pattern
- Unit of Work ve transaction sınırı
- **Specification pattern** (Ardalis.Specification) — sorgu mantığını kapsülleme
- Test edilebilirlik: in-memory provider tuzakları vs TestContainers ile gerçek DB

---

## 7. Veritabanları

### 7.1 İlişkisel Temeller 🟢→🔴
- Normalizasyon (1NF, 2NF, 3NF, BCNF) ve bilinçli **denormalizasyon**
- Primary key, foreign key, unique, check, default constraint
- Surrogate key vs natural key; int vs GUID vs **sequential GUID (GUIDv7)** — index fragmentation
- **ACID** özellikleri — her harfi örnekle açıklayabilmek
- **Index'ler:** clustered vs non-clustered, composite index ve **kolon sırası**, covering index (`INCLUDE`), filtered index
- Index'in yazma maliyeti — neden "her kolona index" yanlış
- JOIN tipleri ve execution'da ne anlama geldikleri (nested loop, hash, merge join)
- View, materialized/indexed view
- Stored procedure, function, trigger — ve trigger'ın neden tehlikeli olabildiği
- Window function'lar: `ROW_NUMBER`, `RANK`, `LAG`/`LEAD`, `SUM() OVER()`
- CTE ve recursive CTE

### 7.2 SQL Server 🟡
- T-SQL özellikleri, `MERGE` ve bilinen sorunları
- **Execution plan okuma** — estimated vs actual, en pahalı operatörü bulma
- Index seek vs index scan vs table scan vs key lookup
- Statistics ve cardinality estimation
- **Parameter sniffing** — belirtileri ve çözümleri (`OPTION (RECOMPILE)`, `OPTIMIZE FOR`)
- Temp table vs table variable vs CTE — ne zaman hangisi
- Isolation level'lar ve `READ_COMMITTED_SNAPSHOT`
- Deadlock analizi (deadlock graph, trace flag)
- Partitioning, columnstore index
- Always On availability group, backup/restore stratejisi
- DMV'ler ile teşhis (`sys.dm_exec_query_stats`, `sys.dm_db_index_usage_stats`)

### 7.3 PostgreSQL 🟡
- Npgsql driver, EF Core Postgres provider farkları
- **`EXPLAIN (ANALYZE, BUFFERS)`** ile plan analizi
- Index tipleri: B-tree, **GIN**, GiST, BRIN, Hash — hangi veri tipine hangisi
- **JSONB** — sorgulama operatörleri (`->`, `->>`, `@>`), GIN index, ne zaman JSONB ne zaman kolon
- Array tipi, `hstore`, enum tipleri
- **MVCC** ve `VACUUM` / autovacuum — table bloat
- Transaction ID wraparound
- Partitioning (declarative), inheritance
- Extension'lar: `pg_trgm` (fuzzy search), PostGIS, TimescaleDB, **`pgvector`** (AI/embedding için kritik)
- Row-Level Security ile multi-tenancy
- Logical replication, streaming replication, read replica
- `LISTEN`/`NOTIFY` ile pub-sub
- Advisory lock
- Connection pooling: PgBouncer neden gerekli
- SQL Server'dan Postgres'e göç tuzakları (case sensitivity, identifier quoting, `IDENTITY` vs `SERIAL`)

### 7.4 MongoDB 🟡
- Document model, BSON, `_id` ve ObjectId
- **MongoDB.Driver** (.NET): `IMongoCollection<T>`, LINQ provider, `FilterDefinition`
- CRUD, `UpdateOne` vs `ReplaceOne`, upsert, `FindOneAndUpdate`
- **Şema tasarımı:** embedded vs referenced — karar kriterleri (16 MB doküman limiti, erişim deseni)
- Anti-pattern: sınırsız büyüyen array, aşırı normalizasyon
- **Aggregation pipeline:** `$match`, `$group`, `$lookup`, `$project`, `$unwind`, `$facet`
- Index'ler: single, compound (**ESR kuralı**: Equality, Sort, Range), multikey, text, geospatial, TTL, partial
- `explain()` ile sorgu analizi, `COLLSCAN` avlamak
- Transaction desteği (replica set gerektirir) ve maliyeti
- Replication (replica set, primary/secondary, election), read preference, write concern, read concern
- Sharding: shard key seçimi (en kritik karar), chunk, balancer
- Change Streams ile event yayını
- Atlas Vector Search (AI senaryosu)
- **Ne zaman MongoDB, ne zaman ilişkisel** — dürüst karşılaştırma

### 7.5 Redis 🟡
- In-memory key-value store, tek thread'li model
- Veri tipleri: string, list, set, sorted set, hash, stream, bitmap, HyperLogLog
- **Kullanım senaryoları:** cache, session store, distributed lock, rate limiter, leaderboard, pub/sub, queue
- `StackExchange.Redis` kütüphanesi, `IConnectionMultiplexer` singleton kuralı
- `IDistributedCache` ve **`HybridCache`** (.NET 9+) — L1+L2 cache, stampede koruması
- TTL, eviction policy (LRU, LFU, volatile-*)
- Persistence: RDB vs AOF
- Redis Cluster, Sentinel, replication
- Lua script ile atomik operasyon
- Redis Streams ile event log
- **Cache invalidation** stratejileri ve cache stampede (thundering herd) çözümü

### 7.6 Sorgu Optimizasyonu 🔴
- Yavaş sorguyu bulma: DB tarafı (DMV, `pg_stat_statements`), uygulama tarafı (EF logging, MiniProfiler, APM)
- Execution plan okuma disiplini
- Index tasarımı: hangi kolonlar, hangi sırayla, covering index
- SARGability — `WHERE YEAR(date) = 2026` neden index kullanamaz
- `SELECT *` neden kötü (covering index kaçırma, network, memory)
- Implicit conversion nedeniyle index kaybı
- Pagination'da `OFFSET` derinleştikçe yavaşlama → keyset pagination
- Batch işlemler: satır satır vs toplu insert/update
- N+1 tespiti ve düzeltmesi (hem EF hem GraphQL hem MongoDB `$lookup` tarafında)
- Denormalizasyon ve materialized view ile okuma hızlandırma
- Read replica'ya okuma yönlendirme ve replication lag

### 7.7 Transaction'lar & Isolation 🔴
- **Isolation level'lar** ve engelledikleri anomaliler:

| Level | Dirty Read | Non-repeatable Read | Phantom Read |
|-------|-----------|---------------------|--------------|
| Read Uncommitted | ✅ olur | ✅ olur | ✅ olur |
| Read Committed | ❌ | ✅ olur | ✅ olur |
| Repeatable Read | ❌ | ❌ | ✅ olur |
| Serializable | ❌ | ❌ | ❌ |
| Snapshot (MVCC) | ❌ | ❌ | ❌ (write skew olabilir) |

- Lost update ve write skew problemleri
- Locking: shared/exclusive, row/page/table, lock escalation
- **Deadlock** — oluşumu, kilit sıralaması ile önleme, retry stratejisi
- **Optimistic concurrency** (rowversion/`xmin`) vs **pessimistic** (`SELECT ... FOR UPDATE`, `UPDLOCK`)
- `TransactionScope` ve async ile kullanımı (`TransactionScopeAsyncFlowOption`)
- Dağıtık transaction: 2PC neden kaçınılır → **Outbox pattern** ve saga
- Transaction süresini kısa tutma kuralı (içinde HTTP çağrısı yapmama)
- Retry politikası ve idempotency

### 7.8 Migration Stratejileri 🟡
- EF Core Migrations, FluentMigrator, DbUp, Flyway/Liquibase
- Migration'ı kim uygular: uygulama startup'ı mı, pipeline adımı mı (trade-off)
- **Zero-downtime şema değişikliği:** expand → migrate → contract pattern
- Kolon silme/yeniden adlandırmayı çok aşamalı yapma
- Büyük tabloda index ekleme (`CONCURRENTLY`, `ONLINE = ON`)
- Veri migration'ı ve backfill stratejisi
- Rollback planı ve ileri-uyumlu (forward-only) migration felsefesi
- Çoklu instance deployment sırasında şema uyumu
- Seed data yönetimi

### 7.9 Arama Motorları 🟡
- Neden DB `LIKE '%...%'` yetmez
- **Elasticsearch / OpenSearch**: index, mapping, analyzer, tokenizer
- Full-text search, fuzzy matching, relevance scoring (BM25)
- Aggregation ve faceted search
- .NET client (`Elastic.Clients.Elasticsearch`)
- Alternatifler: Meilisearch, Typesense, Azure AI Search
- PostgreSQL full-text search (`tsvector`) ve `pg_trgm` — ne zaman yeterli
- CDC ile DB → search index senkronizasyonu
- Hybrid search (keyword + vector) — [AI bölümü](#15-ai-engineering-net) ile kesişim

---

## 8. Test

### 8.1 Unit Testing 🟢
- xUnit (`[Fact]`, `[Theory]`, `[InlineData]`, `[MemberData]`, `[ClassData]`)
- NUnit ve MSTest farkları; TUnit (yeni nesil)
- **Arrange-Act-Assert** ve Given-When-Then
- Test isimlendirme: `Method_Scenario_ExpectedResult`
- `IClassFixture`, `ICollectionFixture` ile paylaşılan context
- Assertion kütüphaneleri: FluentAssertions (lisans notu), Shouldly, AwesomeAssertions
- Test data üretimi: Bogus, AutoFixture
- Parametrik test ve edge case düşünme disiplini
- Async test yazımı
- **Test edilebilir kod tasarımı** — DI, saf fonksiyon, `TimeProvider` (.NET 8+) ile zaman soyutlama
- Coverage: Coverlet, ReportGenerator — **coverage yüzdesinin yanıltıcılığı**
- TDD döngüsü (red-green-refactor) ve gerçekçi kullanımı

### 8.2 Mocking 🟡
- Test double türleri: dummy, stub, spy, mock, fake
- **NSubstitute** (tercih edilen), Moq (SponsorLink tartışması), FakeItEasy
- Mock ne zaman gereksiz — gerçek nesne kullanmak daha iyi olduğunda
- `HttpClient` mock'lama: `DelegatingHandler`, **WireMock.Net**
- Zaman, rastgelelik, GUID gibi non-deterministik bağımlılıkları soyutlama
- Over-mocking anti-pattern'i ve kırılgan testler

### 8.3 Integration Testing 🟡
- **`WebApplicationFactory<TProgram>`** ile in-memory API testi
- Test için servis değiştirme (`ConfigureTestServices`)
- Authentication'ı test için bypass etme (test auth handler)
- `HttpClient` ile uçtan uca endpoint testi
- Test veritabanı stratejileri: in-memory provider (**tuzakları**), SQLite in-memory, gerçek DB
- **Respawn** ile testler arası DB temizleme
- Test izolasyonu ve paralel çalıştırma

### 8.4 TestContainers 🟡
- Docker ile gerçek bağımlılıkları (Postgres, SQL Server, Redis, MongoDB, RabbitMQ, Kafka) ayağa kaldırma
- `Testcontainers` .NET kütüphanesi
- Container yaşam döngüsü ve test performansı
- CI'da TestContainers çalıştırma
- .NET Aspire ile test orkestrasyonu

### 8.5 E2E & Diğer Test Türleri 🟡
- **Playwright** (.NET) ile tarayıcı otomasyonu; Selenium alternatifi
- API E2E testi ve smoke test
- **Snapshot testing** — Verify
- Contract testing — Pact (consumer-driven)
- Mutation testing — Stryker.NET
- Behavior testing — SpecFlow/Reqnroll
- Test piramidi vs test trophy — modern görüş

### 8.6 Performans Testi 🔴
- **BenchmarkDotNet** — mikro-benchmark, `[MemoryDiagnoser]`, doğru ölçüm kuralları
- Yük testi: k6, NBomber, Bombardier, JMeter, Crank
- Load vs stress vs soak vs spike testi
- SLO/SLI tanımlama, p95/p99 hedefleri
- Baseline oluşturma ve CI'da regresyon tespiti

### 8.7 Architecture Testing 🔴
- **NetArchTest** / **ArchUnitNET** ile mimari kuralları test etme
- Örnek kurallar: "Domain katmanı Infrastructure'a referans veremez", "Controller'lar `DbContext` kullanamaz"
- Katman ihlallerini CI'da yakalama
- Naming convention testleri

---

## 9. Mimari & Tasarım

### 9.1 SOLID 🟡
- **S**ingle Responsibility — "tek bir değişme sebebi"
- **O**pen/Closed — genişlemeye açık, değişikliğe kapalı
- **L**iskov Substitution — klasik ihlal örnekleri (Square/Rectangle, `NotImplementedException`)
- **I**nterface Segregation — şişman interface'i bölme
- **D**ependency Inversion — DI ile ilişkisi ve farkı
- Her prensip için **kötü → iyi** refactor hikayesi ve çalışan kod
- DRY, KISS, YAGNI, Law of Demeter, composition over inheritance
- SOLID'in aşırı uygulanması (gereksiz soyutlama) eleştirisi

### 9.2 Design Patterns 🟡
- **Creational:** Singleton (thread-safe + DI ile alternatifi), Factory Method, Abstract Factory, Builder, Prototype
- **Structural:** Adapter, Decorator, Facade, Proxy, Composite, Bridge, Flyweight
- **Behavioral:** Strategy, Observer, Mediator, Chain of Responsibility, Command, Template Method, State, Visitor, Iterator, Memento
- **.NET'te yerleşik pattern'ler:** Options, Repository, Unit of Work, Specification, Result, Null Object
- Pattern'lerin .NET'teki doğal karşılıkları (`IEnumerable` = Iterator, DI = Factory/Service Locator ayrımı, `IObservable<T>` = Observer)
- **Anti-pattern'ler:** God object, anemic domain model, service locator, primitive obsession, magic string
- Pattern seçimi: problemden başla, pattern'den değil

### 9.3 Katmanlı vs Clean Architecture 🟡→🔴
- Klasik N-tier (Presentation / Business / Data) ve sınırları
- **Clean Architecture:** Domain → Application → Infrastructure → Presentation
- **Dependency Rule** — bağımlılıklar içeriye doğru, Domain hiçbir şeye bağlı değil
- Ports & Adapters (Hexagonal), Onion Architecture — aynı fikrin varyantları
- Application katmanı = use case'ler
- Infrastructure'ın interface'leri Domain/Application'da tanımlaması
- **Trade-off:** ne zaman fazla geliyor (küçük CRUD servis), ne zaman kurtarıyor
- Katmanları proje olarak mı klasör olarak mı ayırmalı

### 9.4 Domain-Driven Design 🔴
- **Strategic DDD:** Ubiquitous Language, Bounded Context, Context Map, subdomain türleri (core/supporting/generic)
- **Tactical DDD:**
  - Entity (kimlik) vs **Value Object** (değer, immutable)
  - **Aggregate** ve Aggregate Root — sınır belirleme, transaction sınırı ile ilişkisi
  - Aggregate tasarım kuralları (küçük tut, ID ile referansla, aggregate başına bir transaction)
  - Domain Event, Domain Service, Factory, Repository (DDD anlamında)
- Anemic vs Rich domain model
- Invariant'ların domain'de korunması
- Domain event → integration event dönüşümü
- DDD ne zaman **aşırı** (basit CRUD'da)
- EF Core ile DDD: private setter, backing field, owned type, value object mapping

### 9.5 CQRS & MediatR 🔴
- Command / Query ayrımının **asıl amacı** (farklı model, farklı optimizasyon)
- CQRS ≠ Event Sourcing (sık karıştırılır)
- Basit CQRS (aynı DB, farklı model) vs tam CQRS (ayrı read store)
- **MediatR** ile handler organizasyonu; lisans değişikliği ve alternatifler (Wolverine, Brighter, kendi dispatcher'ın)
- **Pipeline behavior** ile cross-cutting: validation, logging, transaction, caching, retry
- Read model projection ve eventual consistency
- CQRS'in maliyeti — ne zaman gereksiz karmaşıklık

### 9.6 Vertical Slice Architecture 🔴
- Katman yerine **özellik** bazlı organizasyon
- "Feature klasörü" içinde request, handler, validator, endpoint bir arada
- Clean Architecture ile karşılaştırma ve ne zaman hangisi
- Coupling'i kabullenme, yüksek cohesion
- FastEndpoints/Minimal API ile doğal uyum

### 9.7 Modular Monolith 🔴
- Microservice'e geçmeden önce doğru adım
- Modül sınırları = bounded context
- Modüller arası iletişim: in-process mediator vs event
- Veritabanı şeması ayrımı (schema-per-module)
- Modülerliği derleme zamanında zorlama (architecture test, ayrı proje)
- Monolith → microservice göç yolu (strangler fig)
- **"Microservice'e ihtiyacın var mı?"** dürüst değerlendirme — mülakatta çok değerli bir cevap

---

## 10. Microservices & Dağıtık Sistemler

### 10.1 Microservice Temelleri 🔴
- Ne zaman microservice, ne zaman **monolit** (ekip büyüklüğü, deployment bağımsızlığı, scale profili)
- Microservice'in gizli maliyetleri: dağıtık debugging, veri tutarlılığı, operasyon yükü
- Service decomposition: bounded context, subdomain, veri sahipliği
- Database-per-service ve sorguları birleştirme problemi
- Servisler arası iletişim: senkron (HTTP/gRPC) vs asenkron (event)
- Service discovery, client-side vs server-side load balancing
- Service mesh (Istio, Linkerd) — ne zaman gerekir
- **Dapr** ile building block yaklaşımı
- **.NET Aspire** ile yerel orkestrasyon ve servis keşfi

### 10.2 Message Broker'lar 🔴
- Kuyruk (point-to-point) vs Topic (pub/sub) semantiği
- **RabbitMQ:** exchange tipleri (direct, topic, fanout, headers), binding, queue, DLQ, prefetch
- **Apache Kafka:** topic, partition, offset, consumer group, log compaction, retention — event streaming modeli
- **Azure Service Bus:** queue, topic/subscription, session, dead-letter, scheduled message, duplicate detection
- Amazon SQS/SNS kısa karşılaştırma
- **Delivery semantics:** at-most-once, at-least-once, exactly-once (ve neden "exactly-once" pratikte idempotency demek)
- Message ordering garantileri ve partition key
- **MassTransit** / NServiceBus / Wolverine ile soyutlama
- Consumer idempotency, retry, exponential backoff, dead-letter işleme
- Poison message yönetimi
- Kafka vs RabbitMQ karar tablosu

### 10.3 Event-Driven Architecture 🔴
- Domain event vs **Integration event** — sınır ve serialization farkı
- Event notification vs event-carried state transfer vs event sourcing
- **Event Sourcing:** append-only log, replay, snapshot, projection; Marten/EventStoreDB
- Event versioning ve şema evrimi
- **Eventual consistency** — kullanıcıya nasıl anlatılır, UI'da nasıl ele alınır
- Event storming ile modelleme

### 10.4 Outbox & Saga 🔴
- **Dual-write problemi:** DB'ye yaz + mesaj gönder — neden atomik değil
- **Transactional Outbox pattern** — aynı transaction'da outbox tablosuna yazma, ayrı process ile publish
- Inbox pattern ile consumer idempotency
- CDC tabanlı outbox (Debezium)
- **Saga pattern:**
  - Choreography (event zinciri) — basit, ama akış görünmez
  - Orchestration (merkezi koordinatör) — görünür, ama tek nokta
  - Compensating transaction tasarımı
  - MassTransit state machine (Automatonymous)
- Gerçek senaryo: sipariş → ödeme → stok → kargo akışı ve her adımın telafisi

### 10.5 API Gateway & Edge 🔴
- Gateway sorumlulukları: routing, auth, rate limit, aggregation, protocol translation
- **YARP** (Microsoft'un reverse proxy'si) ile özelleştirilebilir gateway
- Ocelot, Azure API Management, Kong, nginx
- BFF ile ilişkisi
- Gateway'in tek hata noktası olma riski

### 10.6 Resilience 🔴
- **Polly** / `Microsoft.Extensions.Http.Resilience`
  - Retry + **exponential backoff + jitter** (neden jitter şart)
  - **Circuit breaker** — closed/open/half-open durumları
  - Timeout (pessimistic/optimistic)
  - Bulkhead isolation
  - Fallback
  - Hedging (.NET 8+)
- Retry'ın tehlikesi: idempotent olmayan işlemde, retry storm, cascading failure
- Graceful degradation
- Health check'ler ve dependency health
- Chaos engineering (Simmy, Azure Chaos Studio)

### 10.7 Dağıtık Sistem Kavramları 🔴
- **CAP teoremi** — pratikte PACELC ile birlikte düşünmek
- Consistency modelleri: strong, eventual, causal, read-your-writes
- Consensus: Raft ve Paxos'un temel fikri (detay değil, sezgi)
- Distributed transaction alternatifleri
- **Idempotency** — dağıtık sistemin en önemli tek kavramı
- Clock skew, logical clock, vector clock (kavramsal)
- Distributed tracing: **OpenTelemetry**, trace/span/baggage, W3C Trace Context
- Correlation ID propagation
- Observability üçlüsü: logs + metrics + traces; Prometheus, Grafana, Jaeger, Seq, Datadog
- SLI / SLO / SLA ve error budget

---

## 11. Performans & Bellek

### 11.1 Benchmarking 🔴
- **BenchmarkDotNet** doğru kullanımı, `[MemoryDiagnoser]`, `[Params]`, baseline
- Mikro-benchmark tuzakları: dead code elimination, JIT warm-up, ölçüm gürültüsü
- "Ölçmeden optimize etme" prensibi
- Profiling vs benchmarking farkı

### 11.2 Bellek Yönetimi & GC 🔴
- Managed heap, **generation'lar** (Gen 0/1/2) ve neden generational
- **Large Object Heap (LOH)** — 85 KB eşiği, fragmentation, `GCSettings.LargeObjectHeapCompactionMode`
- Workstation vs **Server GC**; concurrent/background GC
- **DATAS** (.NET 8+, dinamik heap adaptasyonu) — container'da bellek davranışı
- GC pause türleri ve latency mode'ları
- `IDisposable` / `IAsyncDisposable` ve dispose pattern
- Finalizer neden pahalı, `SafeHandle` ile doğru yaklaşım
- **Memory leak sebepleri:**
  - Event handler abone kalması (en yaygın)
  - Static koleksiyonlar
  - `IDisposable` dispose edilmemesi
  - `HttpClient` yanlış kullanımı
  - Closure ile beklenmedik referans tutma
  - Cache'in sınırsız büyümesi
- Teşhis: `dotnet-counters`, `dotnet-dump` + SOS, dotMemory, PerfView, Visual Studio Diagnostic Tools
- Container'da bellek limiti ve OOMKilled

### 11.3 Allocation Azaltma 🔴
- Allocation'ın gerçek maliyeti (GC baskısı)
- `Span<T>` / `Memory<T>` / `stackalloc`
- `ArrayPool<T>`, `MemoryPool<T>`, `ObjectPool<T>` (`Microsoft.Extensions.ObjectPool`)
- `struct` kullanımı ve defensive copy tuzağı; `readonly struct`, `in` parametre
- String allocation: interpolation handler, `string.Create`, `AsSpan`, `StringBuilder` havuzlama
- LINQ'in allocation maliyeti — sıcak yolda `for` döngüsü
- `ValueTask` ile async allocation azaltma
- Boxing avı (interface çağrıları, `object` parametreler)
- `System.IO.Pipelines` ile sıfır-kopya I/O

### 11.4 Caching Stratejileri 🟡→🔴
- Cache katmanları: in-process (`IMemoryCache`) → distributed (Redis) → CDN → browser
- **`HybridCache`** (.NET 9+) — L1+L2, stampede koruması, tag ile invalidation
- Pattern'ler: cache-aside, read-through, write-through, write-behind, refresh-ahead
- **Cache invalidation** — "bilgisayar bilimlerinin iki zor probleminden biri"
- **Cache stampede / thundering herd** ve çözümü (lock, jitter, probabilistic early expiration)
- TTL seçimi, sliding vs absolute expiration
- Cache key tasarımı ve versiyonlama
- Negatif cache (bulunamadı sonucunu cache'leme)
- Consistent hashing (dağıtık cache'te)
- Neyi cache'lememeli (kullanıcıya özel hassas veri, sık değişen veri)

### 11.5 AOT, Trimming & Startup 🔴
- **Native AOT** (.NET 10'da olgun): ~1 MB binary, çok hızlı cold start
- AOT kısıtları: reflection, dynamic code, bazı kütüphaneler
- Trimming ve `TrimMode`, trim warning'leri çözme
- ReadyToRun (R2R) — AOT'a göre daha esnek orta yol
- Source generator'ların AOT'taki rolü (JSON, logging, regex, DI)
- Startup süresini ölçme ve azaltma
- Serverless/Function senaryosunda cold start

### 11.6 Profiling & Diagnostics 🔴
- `dotnet-counters`, `dotnet-trace`, `dotnet-dump`, `dotnet-gcdump`, `dotnet-monitor`
- EventSource / EventPipe, `System.Diagnostics.Metrics`
- Visual Studio Profiler, PerfView, dotTrace, dotMemory
- Flame graph okuma
- Production'da düşük etkili profiling
- APM: Application Insights, Datadog, New Relic, OpenTelemetry
- **Üretim incident metodolojisi:** belirti → metrik → hipotez → doğrulama → düzeltme → post-mortem

---

## 12. Güvenlik

### 12.1 OWASP Top 10 (.NET karşılıkları) 🟡→🔴
1. **Broken Access Control** — resource-based authorization, IDOR koruması
2. **Cryptographic Failures** — TLS, at-rest şifreleme, zayıf algoritmalardan kaçınma
3. **Injection** — parametreli sorgu, EF Core'un koruması ve `FromSqlRaw` tuzağı; NoSQL injection; command injection
4. **Insecure Design** — threat modeling (STRIDE)
5. **Security Misconfiguration** — Development exception page'in prod'da açık kalması, default credential
6. **Vulnerable Components** — `dotnet list package --vulnerable`, Dependabot, SCA
7. **Identification & Authentication Failures** — brute force, weak password, session fixation
8. **Software & Data Integrity Failures** — güvensiz deserialization, supply chain
9. **Logging & Monitoring Failures** — yetersiz audit trail
10. **SSRF** — dış URL çağrılarında allowlist

### 12.2 Authentication 🟡
- Cookie authentication, `SameSite`, `Secure`, `HttpOnly`
- **JWT**: yapı, imza (HS256 vs RS256), doğrulama adımları, `alg: none` saldırısı
- Access + refresh token, rotation, reuse detection
- ASP.NET Core Identity, password hashing (PBKDF2/Argon2), lockout
- MFA/2FA, TOTP
- **Passkeys / WebAuthn** (.NET 10 Identity desteği)
- External login (Google, Microsoft, GitHub)
- OAuth 2.0 / OIDC akışları ve hangisini ne zaman
- Token storage: SPA'da localStorage neden riskli → BFF + HttpOnly cookie

### 12.3 Authorization 🟡→🔴
- Role vs Claim vs Policy
- `IAuthorizationRequirement` + handler ile custom policy
- Resource-based authorization
- Permission-based model (fine-grained)
- Multi-tenant izolasyonu ve tenant sızıntısı testleri
- Yatay vs dikey yetki yükseltme (privilege escalation)

### 12.4 Secrets Yönetimi 🟡
- Kaynak kontrolüne secret koymama (ve kazayla koyduysan **rotate et**)
- User Secrets (dev), environment variables, **Azure Key Vault**, HashiCorp Vault
- Managed Identity ile secret'sız kimlik doğrulama (en iyi yaklaşım)
- Connection string'lerin yönetimi
- Secret scanning (GitHub secret scanning, gitleaks)
- Secret rotation stratejisi

### 12.5 Kriptografi 🔴
- Simetrik (AES) vs asimetrik (RSA, ECDSA)
- Hashing (SHA-256) vs password hashing (bcrypt, Argon2, PBKDF2) — **fark kritik**
- HMAC ile mesaj bütünlüğü (webhook imzası)
- `RandomNumberGenerator` vs `Random` — güvenli rastgelelik
- **ASP.NET Core Data Protection API** — key ring, çoklu instance'ta paylaşım
- Certificate yönetimi, mTLS
- "Kendi kripton'u yazma" kuralı
- Timing attack ve `CryptographicOperations.FixedTimeEquals`

### 12.6 Supply Chain & Uygulama Güvenliği 🔴
- NuGet paket güvenliği, typosquatting, paket imzalama
- SBOM üretimi
- Dependency pinning ve lock file
- CI/CD güvenliği (secret'ların pipeline'da korunması, OIDC ile keyless deploy)
- Container image taraması (Trivy, Defender for Containers)
- SAST/DAST araçları, CodeQL
- Güvenlik header'ları ve CSP
- Rate limiting, bot koruması, WAF

---

## 13. Docker, Kubernetes & DevOps

### 13.1 Docker Temelleri 🟢
- Image vs Container vs Registry; layer ve cache mantığı
- `docker run` bayrakları: `-p`, `-v`, `-e`, `--network`, `--rm`
- `docker ps/logs/exec/inspect/stats`
- Volume vs bind mount
- Docker network türleri (bridge, host, none)
- Registry: Docker Hub, GHCR, Azure Container Registry
- Container ≠ VM — izolasyon modeli farkı

### 13.2 .NET için Dockerfile 🟡
- Base image seçimi: `sdk` (build) vs `aspnet`/`runtime` (çalıştırma)
- **Multi-stage build** — SDK'yı final image'da bırakmama
- Layer cache optimizasyonu: önce `.csproj` kopyala → `restore` → sonra kaynak kodu kopyala
- `.dockerignore` (bin, obj, .git — build context boyutu)
- **Non-root user** ile çalıştırma (.NET 8+ image'larda `$APP_UID`)
- Image varyantları: Debian, Alpine, **chiseled** (ultra-minimal, .NET 8+), AOT image
- `EXPOSE`, `ENTRYPOINT` vs `CMD`
- Sağlık kontrolü (`HEALTHCHECK`)
- `dotnet publish /t:PublishContainer` ile Dockerfile'sız image üretimi
- Image boyutu ve güvenlik yüzeyi optimizasyonu

### 13.3 Docker Compose 🟡
- Multi-service tanımı: API + PostgreSQL + Redis + Seq/Jaeger
- `depends_on` + `healthcheck` ile başlangıç sırası
- Network ve servis adıyla DNS çözümleme
- Volume ile veri kalıcılığı
- Environment değişkenleri, `.env` dosyası, override dosyaları
- Geliştirme ortamı olarak Compose — "tek komutla çalışan repo" hedefi

### 13.4 Kubernetes 🔴
- Temel nesneler: Pod, ReplicaSet, **Deployment**, Service, Ingress
- ConfigMap ve Secret ile konfigürasyon enjeksiyonu
- **Probe'lar:** liveness, readiness, startup — ve ASP.NET Core health check eşlemesi
- Resource request/limit, QoS sınıfı, OOMKilled
- **Graceful shutdown**: SIGTERM, `terminationGracePeriodSeconds`, `IHostApplicationLifetime`
- HPA ile otomatik ölçekleme
- Rolling update, blue-green, canary deployment
- Helm chart temelleri, Kustomize
- Namespace, RBAC, service account
- Log ve metrik toplama
- Azure Kubernetes Service (AKS), **Azure Container Apps** (daha basit alternatif)

### 13.5 CI/CD 🟡
- Pipeline aşamaları: restore → build → test → analyze → publish → containerize → deploy
- **GitHub Actions** ve **Azure Pipelines** YAML temelleri
- Matrix build, caching, artifact yönetimi
- Test sonuçları ve coverage raporlama
- Environment ve approval gate'leri
- Secret yönetimi, OIDC ile keyless Azure deploy
- Semantic versioning ve otomatik release (GitVersion, MinVer)
- NuGet paket yayınlama
- Deployment stratejileri: rolling, blue-green, canary, feature flag ile karanlık yayın
- Rollback planı
- DORA metrikleri (deployment frequency, lead time, MTTR, change failure rate)

### 13.6 Infrastructure as Code 🔴
- ARM template → **Bicep** (Azure-native)
- **Terraform** (multi-cloud), state yönetimi
- Pulumi (C# ile IaC)
- IaC'yi pipeline'a bağlama, plan/apply akışı
- Ortam parite'si (dev/staging/prod aynı tanımdan)

---

## 14. Azure Cloud

> Junior: portal kullanımı ve deploy. Mid: servis yapılandırma ve entegrasyon. Senior: mimari seçim, maliyet, dayanıklılık.

### 14.1 App Service 🟡
- Web App deployment (ZIP deploy, GitHub Actions, Azure Pipelines, container)
- Application Settings ve connection string'ler → `IConfiguration`'a nasıl akar
- **Deployment slot** ve slot swap ile sıfır kesintili yayın; slot-specific settings
- Scale up (plan boyutu) vs scale out (instance sayısı), autoscale kuralları
- Always On, health check, custom domain, TLS binding
- Managed Identity ile diğer servislere erişim
- Kudu/SCM, log stream, diagnostic

### 14.2 Azure SQL 🟡
- Deployment seçenekleri: Single DB, Elastic Pool, Managed Instance, SQL Server on VM
- DTU vs vCore, serverless tier ve auto-pause
- Firewall, Private Endpoint, **Entra ID / Managed Identity ile şifresiz auth**
- Geo-replication, failover group, backup/restore, PITR
- Query Performance Insight, otomatik tuning
- Retry politikası (`EnableRetryOnFailure`) — geçici hatalar (transient fault)

### 14.3 Cosmos DB 🟡
- API'ler: NoSQL(SQL), MongoDB, Cassandra, Gremlin, Table, PostgreSQL
- **Partition key seçimi** — en kritik ve geri dönüşü zor karar (cardinality, hot partition)
- RU/s ekonomisi: provisioned vs autoscale vs serverless; RU maliyetini ölçme
- **Consistency level'lar:** Strong, Bounded Staleness, Session, Consistent Prefix, Eventual — trade-off
- Indexing policy ve maliyet etkisi
- **Change Feed** ile event-driven entegrasyon
- TTL, stored procedure/trigger/UDF
- Global dağıtım ve multi-region write
- Cosmos DB ne zaman **yanlış** seçim (ilişkisel sorgu ihtiyacı, maliyet)

### 14.4 Storage 🟢→🟡
- Blob, File, Queue, Table storage
- Blob tier: Hot / Cool / Cold / Archive ve maliyet
- **SAS token** (user delegation SAS tercih edilir), stored access policy
- `Azure.Storage.Blobs` SDK: upload/download, streaming, blok blob
- Lifecycle management policy
- Static website hosting, CDN / Azure Front Door
- Event Grid ile blob event'leri

### 14.5 Messaging 🟡
- **Service Bus:** queue vs topic/subscription, session (ordering), dead-letter, scheduled message, duplicate detection, peek-lock vs receive-and-delete
- **Event Hubs:** yüksek hacimli telemetri, Kafka uyumlu arayüz, partition & consumer group
- **Event Grid:** olay yönlendirme, reaktif entegrasyon
- **Storage Queue** — basit ve ucuz alternatif
- Hangisi ne zaman: Service Bus (kurumsal mesajlaşma) vs Event Hubs (stream) vs Event Grid (event routing)
- MassTransit ile Azure Service Bus kullanımı

### 14.6 Azure Functions 🟡
- Trigger'lar: HTTP, Timer, Blob, Queue, Service Bus, Event Hub, Cosmos Change Feed
- Input/output binding'ler
- **Isolated worker model** (modern, .NET 10 ile tek seçenek)
- Hosting planları: Consumption, Flex Consumption, Premium, Dedicated — **cold start** etkisi
- **Durable Functions:** orchestrator, activity, entity; fan-out/fan-in, human interaction, eternal orchestration
- Function vs Container Apps vs App Service — karar kriterleri
- Yerel geliştirme ve test

### 14.7 Key Vault 🟡
- Secret, Key, Certificate ayrımı
- **Managed Identity** ile erişim (connection string'siz)
- `Azure.Extensions.AspNetCore.Configuration.Secrets` ile `IConfiguration` entegrasyonu
- Soft delete, purge protection, versiyonlama
- Secret rotation ve `IOptionsMonitor` ile yenileme
- RBAC vs access policy

### 14.8 Entra ID (Azure AD) 🟡
- Tenant, app registration, service principal, enterprise app
- Client credentials (servis-servis), authorization code + PKCE (kullanıcı)
- On-behalf-of flow (API → API)
- `Microsoft.Identity.Web` ile ASP.NET Core entegrasyonu
- App role vs group claim, scope (`scp`) vs role (`roles`)
- **Managed Identity** (system-assigned vs user-assigned) — en önemli pratik konu
- B2C / External ID ile müşteri kimlik yönetimi
- Conditional access, token ömrü

### 14.9 Application Insights & İzleme 🟡
- Telemetry türleri: Request, Dependency, Trace, Exception, Custom Event, Metric
- Otomatik toplama vs custom telemetry (`TelemetryClient`)
- **OpenTelemetry ile modern entegrasyon** (önerilen yol)
- Sampling ve maliyet kontrolü
- **KQL (Kusto) sorgu temelleri** — `requests | where ... | summarize ... | render`
- Application Map, Live Metrics, failure/performance blade'leri
- Availability test, alert kuralı, action group
- Log Analytics workspace ve merkezi loglama
- Dashboard ve workbook

### 14.10 Container Apps & AKS 🔴
- **Azure Container Apps** — serverless container, KEDA ile event-driven scaling, scale-to-zero, Dapr entegrasyonu
- AKS — tam Kubernetes kontrolü, ne zaman gerekir
- Container Registry, image build (ACR Tasks)
- Container Apps vs AKS vs App Service vs Functions — **karar tablosu**
- Ingress, revision, traffic splitting (canary)

### 14.11 .NET Aspire 🟡
- Aspire nedir: cloud-native uygulamalar için orkestrasyon + servis keşfi + telemetri
- AppHost projesi ve kaynak modeli (Postgres, Redis, RabbitMQ container'larını kod ile tanımlama)
- Service defaults: OpenTelemetry, health check, resilience hazır gelir
- Aspire Dashboard ile yerel gözlemlenebilirlik
- Integration test'te Aspire kullanımı
- Azure'a deploy (`azd up`), Container Apps'e çıkış
- Docker Compose'a göre avantaj/dezavantaj

---

## 15. AI Engineering (.NET)

> .NET artık AI-first bir platform. 2026 mülakatlarında backend rollerinde bile bu bölüm soruluyor.

### 15.1 LLM Temelleri 🟡
- Token, context window, temperature, top-p, max tokens
- Prompt engineering: system/user/assistant rolleri, few-shot, chain-of-thought
- Model seçimi: yetenek / gecikme / maliyet üçgeni
- Hallucination, grounding, determinizm eksikliği
- Streaming response ve kullanıcı deneyimi
- Rate limit, retry, token maliyeti hesaplama
- Güvenlik: **prompt injection**, jailbreak, veri sızıntısı

### 15.2 Microsoft.Extensions.AI 🟡
- .NET'in birleşik AI soyutlama katmanı (`IChatClient`, `IEmbeddingGenerator`)
- Provider bağımsızlığı (OpenAI, Azure OpenAI, Ollama, Anthropic, yerel model)
- Middleware pipeline: logging, caching, telemetry, function invocation
- DI entegrasyonu
- **Function calling / tool use** — LLM'e C# metodu çağırtma
- Structured output (JSON schema ile tip güvenli çıktı)

### 15.3 Semantic Kernel 🟡
- Kernel, plugin, function (semantic vs native)
- Prompt template'leri ve Handlebars/Liquid
- Planner ve otomatik fonksiyon çağırma
- Memory ve connector'lar
- Filter'lar (prompt/function invocation)
- **Semantic Kernel Agents** ve multi-agent orkestrasyon
- Semantic Kernel vs Microsoft.Extensions.AI — ne zaman hangisi

### 15.4 Embeddings & Vector Search 🟡
- Embedding nedir, semantik benzerlik nasıl ölçülür
- Similarity metrikleri: cosine, dot product, Euclidean
- **Chunking stratejileri:** sabit boyut, overlap, semantic chunking, parent-child
- Vector store seçenekleri:
  - **pgvector** (PostgreSQL) — mevcut DB'de kalmak
  - Azure AI Search — hybrid search + semantic ranker
  - Qdrant, Milvus, Weaviate, Pinecone
  - MongoDB Atlas Vector Search
  - **EF Core vector search** (.NET 11 önizleme)
- ANN index'leri: HNSW, IVFFlat — recall/latency trade-off
- Metadata filtreleme ile hibrit sorgu

### 15.5 RAG Pipeline 🔴
- RAG neden gerekli (güncel/özel veri + kaynak gösterimi + maliyet)
- **Ingestion:** yükle → parse → chunk → embed → store (+ metadata)
- **Retrieval:** query embed → vector search → **hybrid search** (BM25 + vector) → **reranking**
- Query transformation: HyDE, multi-query, query rewriting
- Context window yönetimi ve prompt oluşturma
- Citation / kaynak gösterimi
- **Değerlendirme:** groundedness, relevance, faithfulness, answer correctness; RAGAS yaklaşımı
- Yaygın RAG hataları: kötü chunking, eksik metadata, reranking yokluğu, değerlendirme yapmama
- Agentic RAG ve GraphRAG'e giriş
- Maliyet ve gecikme optimizasyonu, embedding cache

### 15.6 AI Agents 🔴
- Agent = LLM + tools + memory + loop
- ReAct döngüsü (reason → act → observe)
- Tool tasarımı: net açıklama, dar kapsam, deterministik dönüş
- Kısa/uzun süreli bellek
- Multi-agent orkestrasyon desenleri (supervisor, sequential, group chat)
- **Guardrails:** input/output filtreleme, allowlist, insan onayı (human-in-the-loop)
- Prompt injection savunması (özellikle tool çağıran agent'larda)
- Gözlemlenebilirlik: trace, token maliyeti, adım sayısı, başarısızlık modları
- MCP (Model Context Protocol) ile tool entegrasyonu
- Agent'ların üretime alınması: timeout, maliyet limiti, idempotency

### 15.7 ML.NET 🟡
- Klasik ML ne zaman LLM'den daha doğru araç (tabular veri, düşük gecikme, maliyet)
- Senaryolar: regression, classification, clustering, anomaly detection, recommendation
- AutoML ve Model Builder
- Pipeline: data load → transform → train → evaluate → predict
- Metrikler: accuracy, precision/recall, F1, AUC, RMSE
- `PredictionEnginePool` ile ASP.NET Core entegrasyonu
- **ONNX** ile eğitilmiş model içe aktarma (PyTorch/TensorFlow → .NET)
- Model versiyonlama ve yeniden eğitim

### 15.8 AI Değerlendirme & Güvenlik 🔴
- Eval-driven development — "prompt'u değiştirdim, iyi mi oldu?" sorusunu ölçmek
- `Microsoft.Extensions.AI.Evaluation`
- LLM-as-judge ve sınırları
- Offline eval vs online (A/B) test
- Content safety / moderation (Azure AI Content Safety)
- PII redaction, veri kalıcılığı politikası
- Responsible AI: bias, şeffaflık, insan gözetimi
- Maliyet yönetimi: model yönlendirme, caching, prompt sıkıştırma

---

## 16. Sistem Tasarımı

### 16.1 Ölçeklenebilirlik 🔴
- Vertical vs horizontal scaling
- **Stateless servis tasarımı** — neden ölçeklemenin ön koşulu
- Session affinity (sticky session) ve sorunları
- Load balancer katmanları (L4 vs L7), health check tabanlı yönlendirme
- Database ölçekleme: read replica, connection pooling, sharding, partitioning
- CQRS ile okuma/yazma ayrıştırma
- Asenkron işleme ile pik yükü tamponlama (queue ile load leveling)
- Back-of-the-envelope hesaplama: QPS, storage, bandwidth, instance sayısı

### 16.2 Ölçekte Caching 🔴
- Çok katmanlı cache mimarisi (browser → CDN → gateway → app → DB)
- Cache hit ratio ölçümü ve iyileştirme
- Invalidation stratejileri ve tag-based invalidation
- Consistent hashing
- Hot key problemi

### 16.3 Rate Limiting & Kotalar 🔴
- Algoritmalar: fixed window, sliding window (log/counter), token bucket, leaky bucket
- .NET `RateLimiter` API'leri
- Dağıtık rate limiting (Redis + Lua ile atomik sayaç)
- Kullanıcı/tenant/IP/endpoint bazlı partition
- `429` + `Retry-After` ve client tarafı backoff
- Quota, throttling, fair usage

### 16.4 Veri Bölümleme 🔴
- Partitioning vs sharding vs replication
- Shard key seçimi ve resharding acısı
- Hot partition problemi
- Cross-shard sorgu ve join
- Global ikincil index
- Multi-region veri yerleşimi, veri egemenliği (data residency)

### 16.5 Case Study'ler 🔴
Her biri için: gereksinim analizi → API tasarımı → veri modeli → ölçekleme → darboğaz → hata senaryoları.

- **URL shortener** (hash üretimi, çakışma, okuma ağırlıklı yük, cache)
- **Rate limiter servisi** (dağıtık sayaç, doğruluk/performans trade-off)
- **Bilet/koltuk rezervasyonu** (concurrency, optimistic locking, hold süresi)
- **Bildirim sistemi** (fan-out, çoklu kanal, retry, idempotency)
- **Haber akışı / timeline** (fan-out on write vs read, ünlü kullanıcı problemi)
- **Chat servisi** (SignalR, presence, mesaj sırası, offline teslim)
- **E-ticaret checkout** (saga, envanter rezervasyonu, ödeme, telafi)
- **Dosya yükleme servisi** (chunked upload, presigned URL, virüs taraması)
- **Arama/otomatik tamamlama** (index, trie, ES)
- **AI destekli doküman Q&A** (RAG, ingestion pipeline, maliyet)

---

## 17. Mülakat Süreci

### 17.1 Coding Interview 🟢→🔴
- C# ile LeetCode refleksi: string, array, hash map, two pointers, sliding window, recursion, BFS/DFS
- Klasik .NET soruları: FizzBuzz, string reverse, palindrome, anagram, fibonacci, en sık geçen eleman
- LINQ ile çözme vs döngü ile çözme — hangisini ne zaman göstermeli
- Big-O analizi sesli yapma
- Edge case listeleme alışkanlığı (null, boş, tek eleman, çok büyük, negatif, unicode)
- Kodu test edilebilir yazma

### 17.2 Live Coding / Pair Programming 🟡
- **Sesli düşünme** — sessiz kalmak en büyük hata
- Varsayımları açıkça söyleme ve soru sorma
- Küçük adımlarla ilerleme, çalışan koddan başlama
- "Önce çalışsın, sonra iyileştirelim" dengesi
- IDE hakimiyeti (kısayollar, refactoring araçları)
- Hata yapınca panik yerine sistematik debug

### 17.3 Behavioral / STAR 🟡
- **STAR:** Situation → Task → Action → Result (sayısal sonuçla)
- Hazırlanması gereken hikayeler:
  - Zor bir production incident ve çözümü
  - Takım içi anlaşmazlık
  - Deadline kaçırma / kapsam pazarlığı
  - Mentorluk ve bilgi paylaşımı
  - Başarısız bir proje ve çıkarılan ders
  - Teknik bir kararı savunma / fikir değiştirme
- "Neden ayrılmak istiyorsun" — dürüst ama olumsuz olmayan çerçeve
- "Neden burası" — şirkete özel araştırma (ürün, teknoloji, ekip)
- Sorulacak sorular listesi (ekip yapısı, code review kültürü, teknik borç, on-call)

### 17.4 System Design Interview 🔴
- **Yapı:**
  1. Gereksinim netleştirme (fonksiyonel + non-fonksiyonel)
  2. Ölçek tahmini (kullanıcı, QPS, veri, okuma/yazma oranı)
  3. Üst düzey tasarım (kutu-ok diyagramı)
  4. API sözleşmesi
  5. Veri modeli ve depolama seçimi
  6. Derinleşme (bir bileşeni detaylandırma)
  7. Darboğaz, hata modu, ölçekleme
  8. Trade-off özeti
- Gereksinimi netleştirmeden çizmeye başlamama
- Her seçimi **gerekçelendirme** ("Postgres seçtim çünkü...")
- Bilmediğini kabul etme ve muhakemeyi gösterme

### 17.5 Take-home Ödevleri 🟡
- Zaman yönetimi ve **over-engineering tuzağı**
- README'nin önemi: kurulum, mimari kararlar, varsayımlar, yapılmayanlar ve nedenleri
- Test yazma (en çok fark yaratan tek şey)
- `docker compose up` ile tek komutta çalışma
- Temiz commit geçmişi
- Kapsamı erken netleştirmek için soru sorma

### 17.6 Mülakat Öncesi Checklist
- [ ] CV'deki her teknolojiyi savunabiliyor muyum?
- [ ] Son projemi 2 dakikada anlatabiliyor muyum (problem → çözüm → sonuç)?
- [ ] En gurur duyduğum ve en çok pişman olduğum teknik karar?
- [ ] Şirketin ürününü ve teknoloji yığınını araştırdım mı?
- [ ] Benim sorularım hazır mı?
- [ ] Ortam testi (kamera, mikrofon, IDE, ekran paylaşımı)?

---

## Çalışma Planı

Günde 2-3 saat varsayımıyla. Kendi seviyene göre fazları atlayabilir veya hızlandırabilirsin.

### Junior hedefi (~3,5 ay)
| Hafta | İçerik |
|-------|--------|
| 1-2 | Bölüm 0 (ön koşullar) + 1.1-1.2 (program anatomisi, tip sistemi & bellek) |
| 3-4 | 1.3-1.5 (OOP, collections, exception) |
| 5 | 1.6-1.7 (delegate/event, LINQ) |
| 6 | **Bölüm 2 temel kısmı** — 2.4 (`bin`/`obj`), 2.7 (`run` vs `publish`), 2.6 (exe nasıl oluşuyor) |
| 7-8 | Bölüm 3 (platform: DI, configuration, middleware, logging) |
| 9-10 | Bölüm 5.1-5.5 (REST, Web API, validation) |
| 11-12 | Bölüm 6.2 (EF Core) + 7.1 (SQL temelleri) |
| 13 | Bölüm 8.1-8.2 (unit test, mocking) |
| 14 | Bölüm 13.1-13.3 (Docker) + tekrar + mock mülakat |

### Mid hedefi (~5,5 ay, junior içeriği biliniyor varsayımıyla)
| Hafta | İçerik |
|-------|--------|
| 1-2 | 1.8-1.9 (modern C#, Span) + 1.10 (reflection, source generators) |
| 3-4 | **Bölüm 2 tamamı** (Roslyn/IL, MSBuild, apphost, deployment modelleri, JIT) — lab'ları mutlaka yap |
| 5-6 | Bölüm 3 tekrar (Options pattern, hosting, background service, filter) |
| 7-9 | **Bölüm 4 tamamı (multithreading)** — en yüksek getirili bölüm |
| 10-12 | **Bölüm 5 tamamı (API)** — versioning, güvenlik, gRPC, GraphQL, SignalR |
| 13-14 | Bölüm 6 (EF Core ileri, Dapper) + 7.2-7.5 (SQL Server, Postgres, Mongo, Redis) |
| 15-16 | Bölüm 9.1-9.3 (SOLID, patterns, Clean Architecture) |
| 17-18 | Bölüm 8 (test ileri, TestContainers) + 11.4 (caching) |
| 19-20 | Bölüm 12 (güvenlik) + 13 (Docker/CI-CD) |
| 21-22 | Bölüm 14 (Azure) |
| 23-24 | Bölüm 15 (AI) + tekrar + mock mülakat |

### Senior hedefi (~7 ay)
Mid planına ek olarak:
| Hafta | İçerik |
|-------|--------|
| +1-3 | Bölüm 9.4-9.7 (DDD, CQRS, vertical slice, modular monolith) |
| +4-6 | Bölüm 10 (microservices, message broker, saga, outbox, resilience) |
| +7-9 | Bölüm 11 (performans, GC, profiling, AOT) — Bölüm 2.8-2.9 ile birlikte çalış |
| +10-11 | Bölüm 16 (system design, case study'ler) |
| +12-13 | Bölüm 17 (mülakat simülasyonu, behavioral hazırlık) |

**Her hafta:** 1 gün tekrar + 1 gün kodlama pratiği. Her ayın sonunda kapalı kitap self-mock mülakat.

---

## Kaynaklar

### Kitaplar
| Kitap | Yazar | Seviye |
|-------|-------|--------|
| C# in Depth | Jon Skeet | 🟡 |
| **CLR via C#** | Jeffrey Richter | 🔴 |
| **Pro .NET Assemblies / .NET IL Assembler** | Serge Lidin | 🔴 |
| **Writing High-Performance .NET Code** | Ben Watson | 🔴 |
| Pro ASP.NET Core | Adam Freeman | 🟡 |
| Concurrency in C# Cookbook | Stephen Cleary | 🟡🔴 |
| Pro .NET Memory Management | Konrad Kokosa | 🔴 |
| Clean Architecture | Robert C. Martin | 🟡 |
| Domain-Driven Design | Eric Evans | 🔴 |
| Implementing DDD | Vaughn Vernon | 🔴 |
| Designing Data-Intensive Applications | Martin Kleppmann | 🔴 |
| Building Microservices | Sam Newman | 🔴 |
| Monolith to Microservices | Sam Newman | 🔴 |
| Designing Distributed Systems | Brendan Burns | 🔴 |
| SQL Performance Explained | Markus Winand | 🟡 |
| Release It! | Michael Nygard | 🔴 |
| The Pragmatic Programmer | Hunt & Thomas | Hepsi |

### Resmî Dokümantasyon
- [Microsoft Learn — .NET](https://learn.microsoft.com/dotnet/)
- [Microsoft Learn — ASP.NET Core](https://learn.microsoft.com/aspnet/core/)
- [Microsoft Learn — Azure](https://learn.microsoft.com/azure/)
- [.NET Blog](https://devblogs.microsoft.com/dotnet/)
- [.NET Architecture e-books](https://learn.microsoft.com/dotnet/architecture/)
- [C# dil sürüm notları](https://learn.microsoft.com/dotnet/csharp/whats-new/)

### Derleme & Runtime İç Yapısı (Bölüm 2 için)
- [sharplab.io](https://sharplab.io/) — C# → lowered C# / IL / JIT asm; **bu bölümün en önemli aracı**
- [ILSpy](https://github.com/icsharpcode/ILSpy) — assembly decompiler
- [MSBuild Structured Log Viewer](https://msbuildlog.com/) — `dotnet build -bl` çıktısını okumak için
- [dotnet/runtime — Book of the Runtime (BOTR)](https://github.com/dotnet/runtime/tree/main/docs/design/coreclr/botr) — CLR'ın kendi iç dokümantasyonu
- [.NET application publishing overview](https://learn.microsoft.com/dotnet/core/deploying/)
- [Native AOT deployment](https://learn.microsoft.com/dotnet/core/deploying/native-aot/)
- [Trim self-contained apps](https://learn.microsoft.com/dotnet/core/deploying/trimming/trim-self-contained)
- [.NET host (apphost/hostfxr/hostpolicy)](https://github.com/dotnet/runtime/blob/main/docs/design/features/host-components.md)
- [Performance improvements in .NET](https://devblogs.microsoft.com/dotnet/tag/performance/) — Stephen Toub'un yıllık serisi
- [ECMA-335 CLI spesifikasyonu](https://ecma-international.org/publications-and-standards/standards/ecma-335/) — IL'in resmî tanımı

### Roadmap'ler
- [milanm/DotNet-Developer-Roadmap](https://github.com/milanm/DotNet-Developer-Roadmap) — seviye bazlı kapsamlı .NET roadmap
- [MoienTajik/AspNetCore-Developer-Roadmap](https://github.com/MoienTajik/AspNetCore-Developer-Roadmap) — kütüphane/araç odaklı ASP.NET Core roadmap
- [roadmap.sh/aspnet-core](https://roadmap.sh/aspnet-core)

### Blog & YouTube
- **Milan Jovanović** — mimari, CQRS, DDD, Clean Architecture
- **Nick Chapsas** — performans, modern C#, kütüphane karşılaştırmaları
- **Stephen Cleary** — async/await ve concurrency otoritesi
- **Andrew Lock** (.NET Escapades) — ASP.NET Core internals
- **Steve Gordon** — performans, HttpClient, internals
- **Konrad Kokosa** — bellek ve GC
- **Tim Corey** — junior/mid seviye eğitim
- **Derek Comartin** (CodeOpinion) — mesajlaşma, dağıtık sistemler
- **Jimmy Bogard** — MediatR, AutoMapper, mimari
- **Khalid Abuhakmeh**, **Maarten Balliauw** — JetBrains .NET içerikleri
- **Awesome .NET** ve **Awesome ASP.NET Core** GitHub listeleri

### Pratik
- LeetCode / HackerRank / Codewars (C# ile)
- [Exercism .NET track](https://exercism.org/tracks/csharp)
- [Advent of Code](https://adventofcode.com/) (C# ile)
- eShop / eShopOnContainers / eShopOnWeb referans uygulamaları
- Ardalis Clean Architecture template
- Open source .NET projelerine katkı

### Mülakat Pratiği
- Devinterview.io soru setleri
- Pramp / interviewing.io (mock interview)
- System Design Primer (GitHub)
- ByteByteGo (system design)

---

## Nasıl İlerleyeceğim?

1. Bu roadmap **canlı bir doküman**. Her konu tamamlandığında ilgili klasör linki aktif hale gelecek.
2. Her konunun klasöründe üç dosya olacak:
   - `README.md` — kavramsal anlatım + gerçek dünya senaryosu
   - `src/` — `dotnet run` ile çalışan proje
   - `interview-questions.md` — 10-20 soru + cevap
3. **Sıralı ilerle.** Bir konu bitmeden diğerine geçme.
4. Kodu **kopyalama, yaz.** Okumak öğrenmek değildir.
5. Her hafta sonunda kapalı kitap self-mock mülakat yap.
6. Anlamadığın bir konuyu "sonra dönerim" deme — o konu mülakatta gelir.

---

*Son güncelleme: Eylül 2026 · Hedef sürüm: .NET 10 LTS / C# 14*
*.NET ekosistemi (özellikle AI tarafı) hızlı değişiyor — bu dokümanı 3 ayda bir gözden geçir.*
