# .NET Backend Engineer Roadmap: Junior → Mid → Senior

> A **spiral** roadmap to become a **senior .NET backend engineer**, with extra depth on **C# / .NET fundamentals** and **microservices**. Every level revisits every subject one layer deeper, and one project grows with you from a CRUD API into a microservices platform.
>
> **Target:** .NET 10 (LTS, supported until November 2028) · C# 14. Items marked **(.NET 11)** come from .NET 11 / C# 15 (STS, released November 10, 2026): learn them, but assume .NET 10 in production.
> **Last updated:** November 2026 · **Status:** roadmap only. Topic folders (`README.md` + `quiz.md`) are written one at a time.

---

## Contents

| Section | What's inside |
|---|---|
| [1. How this roadmap works](#1-how-this-roadmap-works) | Spiral design, tracks, IDs, topic template, quiz format |
| [2. Levels and exit criteria](#2-levels-and-exit-criteria) | What Junior, Mid and Senior mean here |
| [3. Map at a glance](#3-map-at-a-glance) | Every stage on one screen |
| [4. Level 1: Junior](#4-level-1-junior) | J1–J6: fundamentals, first API, first database, first container |
| [5. Level 2: Mid](#5-level-2-mid) | M1–M7: depth, production APIs, identity, messaging, observability |
| [6. Level 3: Senior](#6-level-3-senior) | S1–S6: runtime, DDD, microservices, platform, reliability, system design |
| [7. Interview lane](#7-interview-lane) | I1–I5, in parallel with the levels |
| [8. Track threads](#8-track-threads) | How each subject deepens level by level |
| [9. Study plan](#9-study-plan) | Pace, placement test, 12-week fast track |
| [10. Electives](#10-electives) | Useful, but outside the core path |
| [11. 2026 radar](#11-2026-radar) | Version and licensing facts, with sources |
| [12. Resources](#12-resources) | Books, docs, other roadmaps |

---

## 1. How this roadmap works

### Spiral, not sequential

Most roadmaps go *finish C# → then SQL → then ASP.NET Core → then Docker*. By the time you reach Docker you have forgotten half of C#, and you never see how the pieces fit together.

Here, **every level touches every track**, and each pass goes deeper:

```text
🧠 C#/.NET  types, LINQ ─► GC, threads ─► JIT, Span, perf
🌐 Web/API  REST, DI    ─► versioning  ─► gateway, BFF
🗄️ Data     SQL, EF     ─► plans, locks ─► ownership, CDC
🔀 Distrib. HttpClient  ─► queues, outbox ─► sagas, Kafka
🔐 Security JWT, OWASP  ─► OAuth, OIDC ─► zero trust
🚢 Ops      Docker, CI  ─► K8s, OTel   ─► GitOps, SLOs
            Junior         Mid            Senior
```

A **stage** is a 2–6 week block that mixes 2–4 tracks and ends with one step of the project. Within a level, no stage covers just one subject.

### Structure: Level → Stage → Topic

```text
Level   (Junior / Mid / Senior / Interview)
 └── Stage   (e.g. J4 Async, data access and HTTP clients)  → a folder
      └── Topic   (e.g. J4.4 EF Core fundamentals)          → a folder: README.md + quiz.md
```

### Tracks

| Icon | Track |
|---|---|
| 🧠 | C# and the .NET runtime |
| 🌐 | HTTP, ASP.NET Core and API design |
| 🗄️ | Databases, SQL, EF Core, caching |
| 🧩 | Design and architecture (SOLID → Clean → DDD) |
| 🔀 | Distributed systems and microservices |
| 🔐 | Security and identity |
| 🧪 | Testing and quality |
| 🚢 | Tooling, delivery, containers, cloud, observability |
| 🎤 | Interview skills |

### IDs and markers

- `J` Junior · `M` Mid · `S` Senior · `I` Interview · `C` Certification (already in the repo). `M5.4` = Mid, stage 5, topic 4.
- `(→ S3.6)` means "the deeper version is in S3.6". Topic docs link to each other instead of repeating content.
- ⭐ = very common in interviews · **(.NET 11)** = new in .NET 11 / C# 15 · 💰 = commercial license, check it before you use it at work.

### One project that grows with you: Shop

An e-commerce backend, built in **its own repository** (it's your portfolio piece):

- **Shop v1 (Junior):** one ASP.NET Core API, PostgreSQL through EF Core, JWT auth, tests, Docker Compose, CI.
- **Shop v2 (Mid):** a modular monolith (Catalog, Basket, Ordering, Payments, Notifications) with Redis, RabbitMQ and an outbox, Keycloak, OpenTelemetry, Aspire and Kubernetes.
- **Shop v3 (Senior):** services pulled out with the Strangler Fig pattern, a YARP gateway, sagas, Kafka, Helm and GitOps on Azure, SLOs, chaos tests and ADRs.

Going *monolith → modular monolith → microservices* is deliberate. It's the path a senior engineer should recommend in real life, and you learn **why** each split is or isn't worth its cost.

### Where topics live

```text
ConsoleApp/
├── 01-junior/
│   └── J1-how-code-runs/
│       └── J1.1-dotnet-platform-cli/
│           ├── README.md      # theory
│           └── quiz.md        # questions, answers in <details>
├── 02-mid/
├── 03-senior/
├── 04-interview/
└── certifications/            # existing: AZ-900, AI-200
```

- Stage folders come from each stage's `📁` line, and topic folders come from the slug under each topic heading.
- A topic gets runnable code only when the code teaches something (a deadlock demo, a benchmark). That code is registered in `ConsoleApp/Program.cs`. Most topics are just a README and a quiz.

### Topic README template

```markdown
[← Roadmap](../../../../ROADMAP.md#<stage-anchor>) · [📝 Quiz](quiz.md) · [Next topic →](../<next>/README.md)

# J4.4 EF Core fundamentals

> Track: 🗄️ · Level: Junior · Builds on: J2.6, J3.3 · Goes deeper in: M3.4

## 1. What is it?            (definition and mental model)
## 2. Why does it matter?    (the problem it solves)
## 3. How does it work?      (explanation with C# code)
## 4. Real-world scenario
## 5. Interview perspective  (how it's asked, what a strong answer has)
## 6. Common mistakes
## 7. Performance and trade-offs
## 8. Further reading
```

### Quiz format (`quiz.md`)

- 12–15 original questions. Types: single choice, multiple choice, true/false, *what does this print?*, *find the bug*, and at Mid/Senior level, scenario and design questions.
- Answers and reasoning sit inside `<details><summary>Answer</summary> … </details>`.
- Each question is tagged 🟢 / 🟡 / 🔴 by depth. The **last two questions review an earlier topic on the same track** (spaced repetition).
- Target ≥ 80%. If you score lower, re-read the README sections that your wrong answers point to.
- Pages start and end with navigation links. Write for a phone screen: no tables with 4+ columns; use bullet "cards" instead.

### How to study a topic

1. Read `README.md`.
2. Type the code yourself, then break it on purpose.
3. Take `quiz.md` closed-book, then check your answers.
4. Answer the stage's 🎯 checkpoint questions **out loud**. That is the interview skill.
5. Apply the topic to Shop.

---

## 2. Levels and exit criteria

### 🟢 Junior: owns a task

**In one sentence:** builds a tested CRUD API on a relational database, puts it in a container and ships it through CI without hand-holding.

- **C# / .NET:** type system, OOP, collections, LINQ, exceptions, using async/await correctly
- **Web:** HTTP, REST, the ASP.NET Core pipeline, DI, validation, OpenAPI
- **Data:** SQL joins and aggregates, indexes, transactions, EF Core CRUD, the N+1 problem
- **Quality:** unit tests, a first integration test, structured logging
- **Security:** authentication vs authorization, JWT, OWASP basics, secrets kept out of Git
- **Ops:** Git, Docker, Docker Compose, a CI pipeline
- **Exit:** Shop v1 done + every J quiz ≥ 80%

### 🟡 Mid: owns a feature or a service

**In one sentence:** designs, secures, observes and runs a production service, and uses async messaging without losing or duplicating data.

- **C# / .NET:** GC, threading, async internals, generics in depth, modern C# up to C# 15
- **Architecture:** SOLID, useful patterns, Clean and vertical slice, modular monolith
- **Data:** execution plans, isolation and locking, EF Core performance, caching, choosing NoSQL
- **APIs:** versioning, idempotency, gRPC, rate limiting, background services
- **Security:** OAuth 2 / OIDC flows, Keycloak or Entra ID, policy-based authorization
- **Distributed:** RabbitMQ, outbox, idempotent consumers, resilience patterns
- **Ops:** Testcontainers, CI/CD, Kubernetes basics, Aspire, OpenTelemetry
- **Exit:** Shop v2 done + every M quiz ≥ 80% + one mock technical interview (→ I5)

### 🔴 Senior: owns a system and helps the team

**In one sentence:** designs, runs and evolves distributed systems, states trade-offs clearly, and brings the team along.

- **Runtime and performance:** JIT, PGO, Native AOT, GC tuning, `Span<T>`, profiling with evidence
- **DDD:** subdomains, bounded contexts, aggregates, domain and integration events
- **Microservices:** decomposition, data ownership, sagas, Kafka, contracts and versioning
- **Platform:** Kubernetes in production, GitOps, deployment strategies, Azure, IaC
- **Reliability:** SLOs, incident response, scaling, chaos testing, disaster recovery
- **Security:** zero trust, service-to-service auth, threat modeling, supply chain
- **Leadership:** ADRs, design reviews, mentoring, system design interviews
- **Exit:** Shop v3 done + every S quiz ≥ 80% + three mock system design interviews (→ I3)

**Senior self-check.** You can:

- Decide whether a monolith, a modular monolith or microservices fits, and defend the choice.
- Draw service boundaries around business capabilities, not around tables.
- Design for partial failure and unreliable networks.
- Keep data consistent enough for the business without cross-service transactions.
- Make message handling idempotent and describe delivery guarantees honestly.
- Find a bottleneck with evidence (traces, profiles, benchmarks), not guesses.
- Secure public APIs and service-to-service calls.
- Deploy safely, roll back, and recover from a bad release.
- Plan backup, restore and disaster recovery with explicit RPO and RTO.
- Weigh reliability, performance, maintainability and cost, and write the decision down as an ADR.

---

## 3. Map at a glance

### 🟢 Junior · `01-junior/` · ~18 weeks

| Stage | Focus |
|---|---|
| [J1](#j1-how-code-runs-and-how-the-web-works) How code runs and how the web works | 🧠 🌐 🚢 .NET CLI, IL and JIT, TCP/DNS/TLS, HTTP, Git, debugging |
| [J2](#j2-types-objects-and-your-first-sql) Types, objects and your first SQL | 🧠 🗄️ type system, OOP, records, exceptions, modern C#, SQL basics |
| [J3](#j3-collections-linq-and-your-first-api) Collections, LINQ and your first API | 🧠 🌐 collections, delegates, LINQ, DI, middleware, REST |
| [J4](#j4-async-data-access-and-http-clients) Async, data access and HTTP clients | 🧠 🗄️ 🌐 async/await, cancellation, EF Core, indexes, HttpClient |
| [J5](#j5-tests-logs-and-security-basics) Tests, logs and security basics | 🧪 🚢 🔐 🧩 xUnit, logging, JWT, OWASP, clean code |
| [J6](#j6-ship-it-containers-and-ci) Ship it: containers and CI | 🚢 🧩 Docker, Compose, GitHub Actions, health checks, layering |
| [🏁](#junior-capstone-shop-v1) Junior capstone | Shop v1 |

### 🟡 Mid · `02-mid/` · ~29 weeks

| Stage | Focus |
|---|---|
| [M1](#m1-c-and-the-runtime-in-depth) C# and the runtime in depth | 🧠 generics, GC, reflection, C# 12–15, threading, async internals |
| [M2](#m2-design-and-architecture) Design and architecture | 🧩 SOLID, patterns, Clean, vertical slice, errors, modular monolith |
| [M3](#m3-data-in-depth) Data in depth | 🗄️ advanced SQL, isolation, plans, EF Core performance, caching, NoSQL |
| [M4](#m4-production-grade-apis-and-identity) Production-grade APIs and identity | 🌐 🔐 hosting, API evolution, gRPC, OAuth/OIDC, Keycloak, crypto |
| [M5](#m5-messaging-and-resilience) Messaging and resilience | 🔀 queues, RabbitMQ, libraries, outbox, Polly, background jobs |
| [M6](#m6-testing-delivery-and-kubernetes) Testing, delivery and Kubernetes | 🧪 🚢 Testcontainers, test strategy, CI/CD, images, K8s, Aspire |
| [M7](#m7-observability-and-production-diagnostics) Observability and production diagnostics | 🚢 🧠 logs, metrics, traces, dotnet-* tools |
| [🏁](#mid-capstone-shop-v2) Mid capstone | Shop v2 |

### 🔴 Senior · `03-senior/` · ~30 weeks

| Stage | Focus |
|---|---|
| [S1](#s1-runtime-and-performance-engineering) Runtime and performance engineering | 🧠 JIT and AOT, GC tuning, Span, generators, profiling, memory model |
| [S2](#s2-domain-driven-design) Domain-Driven Design | 🧩 strategic and tactical DDD, event sourcing, ADRs |
| [S3](#s3-microservices-architecture) Microservices architecture | 🔀 distributed fundamentals, decomposition, data, sagas, Kafka, contracts |
| [S4](#s4-cloud-native-platform-and-azure) Cloud-native platform and Azure | 🚢 K8s in production, GitOps, deployments, Azure, IaC, mesh |
| [S5](#s5-reliability-scale-and-security-engineering) Reliability, scale and security engineering | 🚢 🔀 🔐 SLOs, incidents, scaling, chaos, zero trust, DR |
| [S6](#s6-system-design-and-technical-leadership) System design and technical leadership | 🧩 method, building blocks, case studies, leadership |
| [🏁](#senior-capstone-shop-v3) Senior capstone | Shop v3 |

### 🎤 Interview lane · `04-interview/` · runs in parallel

| Topic | Focus |
|---|---|
| [I1–I5](#7-interview-lane) | Live coding and DSA, .NET question bank, system design, behavioral, mock interviews |

---

## 4. Level 1: Junior

> **Goal:** write, test, containerize and ship a small CRUD API on your own.
> **Mindset:** knowing *why* is enough here. The deep *how it works inside* comes at Mid.

---

### J1. How code runs and how the web works

> 📁 `ConsoleApp/01-junior/J1-how-code-runs/` · ⏱ 2 weeks · Tracks: 🧠 🌐 🚢
> **Goal:** know what happens from `dotnet run` to a running process, and from a URL to an HTTP response.

#### J1.1 The .NET platform and the CLI ⭐
🧠 · `J1.1-dotnet-platform-cli`
- .NET vs .NET Framework vs .NET Standard; "one .NET" since .NET 5
- Release policy: LTS (3 years) vs STS (2 years); why teams pin to LTS
- SDK vs runtime; `dotnet --info`, `global.json`
- CLI: `new`, `build`, `run`, `test`, `publish`, `add package`, `sln`
- SDK-style `.csproj`: `TargetFramework`, `Nullable`, `ImplicitUsings`, `LangVersion`
- NuGet: package references, versions, `dotnet list package --vulnerable`
- Solutions (`.slnx`) and file-based apps (`dotnet run app.cs`)

#### J1.2 From C# to running code ⭐
🧠 · `J1.2-csharp-to-running-code`
- Roslyn compiles C# to **IL** inside an assembly (`.dll`) with metadata
- The CLR loads the assembly, the **JIT** turns IL into machine code on first call
- The BCL; managed vs unmanaged code
- `bin/` vs `obj/`; Debug vs Release
- `run` vs `build` vs `publish`; framework-dependent vs self-contained vs single-file
- A first look at tiered compilation, ReadyToRun and Native AOT (→ S1.1)

#### J1.3 Networking for backend developers ⭐
🌐 · `J1.3-networking-basics`
- OSI vs TCP/IP model: the layers that matter to you are L3 (IP), L4 (TCP/UDP), L7 (HTTP)
- IPv4/IPv6, CIDR (`/24`), private ranges, loopback, ports, NAT
- TCP: three-way handshake, reliable ordered delivery, `TIME_WAIT`; UDP and when to use it
- DNS: A/AAAA/CNAME records, TTL, caching, why "it's always DNS"
- TLS 1.3 handshake and certificates (what the padlock proves)
- L4 vs L7 load balancers (first look, → S5.3)
- Tools: `curl -v`, `ping`, `nslookup`/`dig`, `traceroute`, `netstat`

#### J1.4 HTTP fundamentals ⭐
🌐 · `J1.4-http-fundamentals`
- Request and response anatomy: method, path, headers, body, status
- Methods; **safe** vs **idempotent** ⭐ (why it matters for retries)
- Status code classes; 200 vs 201 vs 204, 400 vs 401 vs 403 vs 404 vs 409 vs 422, 500 vs 502 vs 503 vs 504
- Headers: `Content-Type`, `Accept`, `Authorization`, `Cache-Control`, `ETag`, `Location`
- Cookies, statelessness, content negotiation
- HTTP/1.1 keep-alive vs HTTP/2 multiplexing vs HTTP/3 over QUIC
- CORS: what it is and who enforces it (→ J5.5)

#### J1.5 Git and the daily workflow
🚢 · `J1.5-git-workflow`
- Commits, branches, `merge` vs `rebase` ⭐, fast-forward vs merge commit
- Resolving conflicts; `stash`, `cherry-pick`, `bisect`
- `reset` (soft/mixed/hard) vs `revert`; never rewrite shared history
- Pull requests, code review, small PRs
- Trunk-based development vs GitHub Flow vs Git Flow
- Conventional commits, semantic versioning, `.gitignore` for .NET

#### J1.6 IDE and debugging
🚢 · `J1.6-ide-debugging`
- Breakpoints (conditional, hit count, tracepoints), stepping, watch, call stack
- Breaking on first-chance exceptions; reading a stack trace
- Hot Reload, `dotnet watch`
- Debugging a web API: launch profiles, environment variables, `.http` files
- When to debug and when to log

**🛠 Shop v0.1:** create the Shop repository, a solution, an empty API project and a README. Call a public API with `curl -v` and explain every line of the output.

**🎯 Checkpoint (answer out loud):**
1. What happens between `dotnet run` and the first line of your code running?
2. Framework-dependent vs self-contained publish: when would you choose each?
3. Walk through everything that happens when a browser opens `https://shop.example.com/products`.
4. Which HTTP methods are idempotent, and why does a retry policy care?
5. `merge` vs `rebase`: when is rebasing dangerous?

---

### J2. Types, objects and your first SQL

> 📁 `ConsoleApp/01-junior/J2-types-objects-sql/` · ⏱ 3 weeks · Tracks: 🧠 🗄️
> **Goal:** model a domain with the right C# types, and query a relational database by hand.

#### J2.1 Type system and memory ⭐
🧠 · `J2.1-type-system-memory`
- Value types vs reference types ⭐; what is copied on assignment
- Stack vs heap, and why "value types live on the stack" is only half true
- Boxing and unboxing (hidden allocations)
- `ref`, `out`, `in`; passing a reference type *by value* vs *by reference*
- `null`, nullable value types (`int?`), nullable reference types and warnings
- `string` immutability, interning, `StringBuilder`
- Equality: `==` vs `Equals` vs `ReferenceEquals`

#### J2.2 OOP in C# ⭐
🧠 · `J2.2-oop`
- Encapsulation, inheritance, polymorphism, abstraction, with real examples
- `virtual` / `override` / `new` (hiding) / `sealed`
- Abstract class vs interface ⭐ (including default interface methods)
- Access modifiers: `public`, `internal`, `protected`, `private protected`, `file`
- Composition over inheritance; static classes and members
- Object relationships: association, aggregation, composition

#### J2.3 Class members and object semantics
🧠 · `J2.3-class-members`
- Fields vs properties; auto, `init`, `required`, the `field` keyword (C# 14)
- Constructors, primary constructors, constructor chaining, static constructors
- `const` vs `readonly` vs `static readonly`
- class vs struct vs record ⭐ (record class, record struct), `with` expressions
- Object initializers, deconstruction, indexers, operators
- The `Equals` / `GetHashCode` / `ToString` contract

#### J2.4 Exceptions and error handling ⭐
🧠 · `J2.4-exceptions`
- `try` / `catch` / `finally`, exception filters (`when`)
- `throw;` vs `throw ex;` ⭐ (stack trace), wrapping with `InnerException`
- Custom exceptions; what to catch and what to let bubble up
- `IDisposable` and `using` as cleanup that runs even on exceptions
- Exceptions as a control-flow anti-pattern; a first look at the Result pattern (→ M2.5)

#### J2.5 Everyday modern C#
🧠 · `J2.5-modern-csharp`
- Top-level statements, global usings, file-scoped namespaces, `var`
- Pattern matching: type, property, relational, logical and list patterns; `switch` expressions
- String interpolation, raw string literals, collection expressions (`[1, 2, 3]`)
- Tuples and deconstruction
- Extension methods and C# 14 extension members
- Null operators: `?.`, `??`, `??=`, null-conditional assignment (C# 14)

#### J2.6 Relational model and SQL I ⭐
🗄️ · `J2.6-sql-basics`
- Tables, rows, primary keys, foreign keys, constraints
- Normalization up to 3NF, and when to denormalize
- `SELECT`, `WHERE`, `ORDER BY`, `LIMIT` / `TOP`
- `JOIN` types ⭐ (inner, left, right, full, cross), self-joins
- `GROUP BY`, `HAVING`, aggregate functions
- `NULL` semantics (three-valued logic)
- Logical query processing order ⭐ (`FROM` → `WHERE` → `GROUP BY` → `HAVING` → `SELECT` → `ORDER BY`)
- Running PostgreSQL with one `docker run` (properly in J6)

**🛠 Shop v0.2:** model `Product`, `Category`, `Customer`, `Order` and `OrderLine` in C#: choose class, record or struct for each and write down why. Write the SQL schema and 10 queries by hand: top products, revenue per category, customers with no orders.

**🎯 Checkpoint:**
1. Where does an `int` field of a class live? Why is "structs live on the stack" incomplete?
2. Abstract class vs interface in modern C#: when do you choose each?
3. class, record or struct for `Money`? Why?
4. `throw;` vs `throw ex;`?
5. Inner vs left join, and how a `WHERE` on the right-hand table quietly turns a left join into an inner join.

---

### J3. Collections, LINQ and your first API

> 📁 `ConsoleApp/01-junior/J3-collections-linq-first-api/` · ⏱ 3 weeks · Tracks: 🧠 🌐
> **Goal:** work with data in memory fluently, and expose it through a clean REST API.

#### J3.1 Collections and generics ⭐
🧠 · `J3.1-collections-generics`
- Arrays, `List<T>` (capacity and growth), `Dictionary<TKey, TValue>`, `HashSet<T>`, `Queue<T>`, `Stack<T>`, `LinkedList<T>`
- How a hash table works: buckets, collisions, why `GetHashCode` matters ⭐
- `IEnumerable<T>` vs `ICollection<T>` vs `IList<T>` vs `IReadOnlyList<T>`: what to accept and what to return
- Generics: why they exist (no boxing, type safety), constraints (`where T : class, new()`)
- Big-O of common operations ⭐

#### J3.2 Delegates, lambdas and events
🧠 · `J3.2-delegates-lambdas-events`
- Delegates, `Func`, `Action`, `Predicate`
- Lambdas and closures; captured variables and the classic loop-capture bug
- Events, `EventHandler<T>`, subscribe and unsubscribe (leaks, → M1.2)
- Multicast delegates; delegates as the base of LINQ and middleware

#### J3.3 LINQ ⭐
🧠 · `J3.3-linq`
- Query syntax vs method syntax
- Deferred vs immediate execution ⭐, multiple enumeration
- `Select`, `Where`, `OrderBy`, `GroupBy`, `Join`, `SelectMany`, `Aggregate`, `Any` vs `Count()`
- `First` vs `FirstOrDefault` vs `Single` vs `SingleOrDefault`
- `IEnumerable<T>` vs `IQueryable<T>` ⭐ (first look, before EF Core)
- `FullJoin` and tuple-returning joins **(.NET 11)**

#### J3.4 ASP.NET Core host, DI and configuration ⭐
🌐 · `J3.4-aspnetcore-host-di-config`
- `WebApplication.CreateBuilder`, the generic host, what `Program.cs` sets up
- Dependency injection: why it exists, registering and resolving services
- Lifetimes: transient, scoped, singleton ⭐; captive dependencies
- Configuration providers and their order: `appsettings.json`, `appsettings.{Environment}.json`, environment variables, command line
- The Options pattern (`IOptions<T>`, first look, → M4.1); environments

#### J3.5 Middleware and the request pipeline ⭐
🌐 · `J3.5-middleware-pipeline`
- What middleware is; `Use`, `Run`, `Map`; why order matters ⭐
- Built-in middleware and its usual order: exception handler, HSTS, HTTPS redirection, routing, CORS, authentication, authorization, endpoints
- Writing custom middleware (convention-based and `IMiddleware`)
- Short-circuiting; where endpoint filters fit (→ M4.1)

#### J3.6 Building REST APIs ⭐
🌐 · `J3.6-rest-apis`
- REST constraints, resource naming, plural nouns, nesting
- Minimal APIs vs controllers: when each fits
- Routing, route constraints, model binding (`[FromBody]`, `[FromQuery]`, `[AsParameters]`)
- DTOs vs entities (never expose entities directly)
- Validation: built-in Minimal API validation (`AddValidation`), DataAnnotations, FluentValidation
- Errors as ProblemDetails (RFC 9457)
- CRUD status codes, `CreatedAtRoute`, pagination, filtering and sorting
- Built-in OpenAPI document + Scalar UI; `.http` files

**🛠 Shop v0.3:** a Shop API with an in-memory store. CRUD for products and categories, order placement with validation, ProblemDetails errors, an OpenAPI document and a `.http` file that exercises every endpoint.

**🎯 Checkpoint:**
1. Why must `GetHashCode` agree with `Equals`? What breaks in a `Dictionary` if it doesn't?
2. When does a LINQ query actually run, and how can it accidentally run twice?
3. `IEnumerable<T>` vs `IQueryable<T>`?
4. What happens when you inject a scoped service into a singleton?
5. Why does middleware order matter? Give an example with authentication.

---

### J4. Async, data access and HTTP clients

> 📁 `ConsoleApp/01-junior/J4-async-data-http/` · ⏱ 3 weeks · Tracks: 🧠 🗄️ 🌐
> **Goal:** put Shop on a real database with non-blocking I/O, and call other services safely.

#### J4.1 Concurrency concepts
🧠 · `J4.1-concurrency-concepts`
- Process vs thread; the thread pool (first look, → M1.5)
- CPU-bound vs I/O-bound work ⭐
- Concurrency vs parallelism vs asynchrony
- Why async lets a server handle more requests, while each request is not faster

#### J4.2 async/await in practice ⭐
🧠 · `J4.2-async-await`
- `Task` / `Task<T>`; what `await` really does (it frees the thread, it doesn't create one)
- Async all the way down; `async void` and why it's dangerous
- `.Result` / `.Wait()` deadlocks and why ASP.NET Core has no `SynchronizationContext` (→ M1.6)
- `Task.WhenAll` / `Task.WhenAny`, running independent calls in parallel
- `ConfigureAwait(false)`: when it matters (library code)
- `IAsyncEnumerable<T>` and `await foreach`

#### J4.3 Cancellation and timeouts
🧠 · `J4.3-cancellation`
- `CancellationToken` and passing it through every layer
- `HttpContext.RequestAborted`; timeouts with `CancelAfter`
- Linked tokens; handling `OperationCanceledException` correctly
- Cooperative cancellation: why you can't kill a running task

#### J4.4 EF Core fundamentals ⭐
🗄️ · `J4.4-efcore-fundamentals`
- What an ORM does; `DbContext` (scoped lifetime) and `DbSet<T>`
- Conventions vs Fluent API configuration; relationships (1:1, 1:N, N:N)
- Migrations: add, apply, revert; generating SQL scripts
- Change tracking and entity states; `AsNoTracking` for reads
- Loading related data: eager (`Include`), explicit, lazy
- The N+1 problem ⭐: how to spot it in logs and how to fix it
- Raw SQL with `FromSql` (parameterized) vs string concatenation
- A first look at ADO.NET underneath

#### J4.5 Indexes, transactions and ACID ⭐
🗄️ · `J4.5-indexes-transactions`
- How a B-tree index speeds up lookups and range scans
- Clustered vs non-clustered (SQL Server) vs heap tables (PostgreSQL)
- Composite indexes and column order ⭐; when indexes hurt (writes, storage)
- ACID ⭐, explained with an order-and-stock example
- Transactions in SQL and EF Core (`SaveChanges` is one transaction; `BeginTransaction`)
- Isolation levels (first look, → M3.2)

#### J4.6 Calling other APIs with HttpClient ⭐
🌐 🔀 · `J4.6-httpclient`
- Socket exhaustion and DNS staleness: why `new HttpClient()` per call is wrong ⭐
- `IHttpClientFactory`; named vs typed clients
- `System.Text.Json`: options, naming policies, `GetFromJsonAsync`
- Timeouts and cancellation on outbound calls
- `AddStandardResilienceHandler` (first look, → M5.5)

**🛠 Shop v0.4:** move Shop to PostgreSQL with EF Core and migrations. Make every endpoint async with cancellation. Add a typed client for a fake "exchange rates" API to show prices in other currencies. Find and fix one N+1 query.

**🎯 Checkpoint:**
1. Why does async increase server throughput but not make one request faster?
2. Why can `.Result` deadlock in some apps but not in ASP.NET Core, and why avoid it anyway?
3. What is the N+1 problem, and how do you detect and fix it in EF Core?
4. Why is `new HttpClient()` per request a problem, and what does `IHttpClientFactory` fix?
5. Which index would you add for `WHERE customer_id = @id ORDER BY created_at DESC`?

---

### J5. Tests, logs and security basics

> 📁 `ConsoleApp/01-junior/J5-tests-logs-security/` · ⏱ 3 weeks · Tracks: 🧪 🚢 🔐 🧩
> **Goal:** make Shop safe to change (tests), safe to run (logs) and safe to expose (auth).

#### J5.1 Unit testing ⭐
🧪 · `J5.1-unit-testing`
- What a unit is; Arrange-Act-Assert; naming tests
- xUnit v3: `[Fact]`, `[Theory]`, fixtures; Microsoft.Testing.Platform and `dotnet test`
- Test doubles: dummy, stub, fake, mock, spy ⭐
- NSubstitute or Moq; assertions with Shouldly (FluentAssertions 8 is 💰)
- What *not* to unit test; testing behavior instead of implementation

#### J5.2 Testable design and a first integration test
🧪 · `J5.2-testable-design-integration`
- DI makes code testable; avoid hidden statics, `DateTime.Now` → `TimeProvider`
- `WebApplicationFactory<T>`: testing the real pipeline in memory
- Why the EF Core in-memory provider lies (→ M6.1 Testcontainers)
- Testing auth-protected endpoints

#### J5.3 Logging basics
🚢 · `J5.3-logging-basics`
- `ILogger<T>`, log levels and when to use each
- Structured logging and message templates ⭐ (`"Order {OrderId} created"`, not interpolation)
- Scopes, categories, filtering in configuration
- Serilog as a provider (first look)
- Never log secrets, tokens or personal data

#### J5.4 Authentication, authorization and JWT ⭐
🔐 · `J5.4-authn-authz-jwt`
- Authentication vs authorization ⭐
- Cookies vs bearer tokens
- JWT structure (header, payload, signature); what validation checks: signature, `iss`, `aud`, `exp` ⭐
- Claims, `[Authorize]`, roles, a first policy
- ASP.NET Core Identity basics, passkeys (.NET 10); password hashing (never roll your own)
- Why JWTs are hard to revoke; short lifetimes and refresh tokens (→ M4.4)

#### J5.5 Web security basics ⭐
🔐 · `J5.5-web-security-basics`
- OWASP Top 10 2025 overview ⭐ (broken access control, misconfiguration, supply chain …)
- SQL injection and parameterization; XSS; CSRF
- CORS configured correctly (it protects browsers, not your API)
- HTTPS and HSTS; secrets out of source control (User Secrets, environment variables)
- Validate input at trust boundaries; mass assignment (over-posting)

#### J5.6 Clean code and a first look at SOLID
🧩 · `J5.6-clean-code-solid-intro`
- Naming, small functions, guard clauses, comments that explain *why*
- DRY, KISS, YAGNI and their limits
- Code smells and safe refactoring steps
- SOLID at a glance (→ M2.1)

**🛠 Shop v0.5:** unit tests for pricing and order rules; integration tests for endpoints with `WebApplicationFactory`; JWT-protected admin endpoints; structured logs with an `OrderId` on every log line of a request.

**🎯 Checkpoint:**
1. Mock vs stub vs fake?
2. Why `LogInformation("Order {OrderId} created", id)` instead of string interpolation?
3. What does an API check when it validates a JWT?
4. How does parameterization stop SQL injection? Is EF Core always safe?
5. Explain CORS: who enforces it, and does it protect your API?

---

### J6. Ship it: containers and CI

> 📁 `ConsoleApp/01-junior/J6-containers-ci/` · ⏱ 2 weeks · Tracks: 🚢 🧩
> **Goal:** anyone can run Shop with one command, and every push is built and tested automatically.

#### J6.1 Docker fundamentals ⭐
🚢 · `J6.1-docker-fundamentals`
- Image vs container; layers and the build cache
- A multi-stage Dockerfile for ASP.NET Core ⭐; `.dockerignore`
- Ports, environment variables, volumes
- SDK container publishing (`dotnet publish /t:PublishContainer`), no Dockerfile needed
- Chiseled / distroless base images, running as non-root

#### J6.2 Docker Compose for local development
🚢 · `J6.2-docker-compose`
- Services, networks and DNS by service name (`postgres:5432`, not `localhost`) ⭐
- Volumes for data, `healthcheck` with `depends_on: condition: service_healthy`
- Environment files; overrides per environment
- API + PostgreSQL + pgAdmin + Seq in one file

#### J6.3 CI with GitHub Actions
🚢 · `J6.3-ci-github-actions`
- Workflows, jobs, steps, runners, triggers
- Restore, build, test on every pull request; NuGet caching
- Building and pushing an image to a registry (GHCR)
- Branch protection and required checks

#### J6.4 Running in production: configuration and health
🚢 · `J6.4-config-health`
- Twelve-factor config: environment overrides files
- Health check endpoints; liveness vs readiness ⭐ (→ M6.5)
- Graceful shutdown: `SIGTERM`, `IHostApplicationLifetime`, finishing in-flight requests
- Forwarded headers behind a proxy

#### J6.5 Layered architecture and project structure
🧩 · `J6.5-layered-architecture`
- Endpoints → services → data access; what each layer may know
- One project vs several; feature folders
- The "repository on top of EF Core" debate (→ M2.2)
- When layers help and when they're ceremony

**🛠 Shop v1.0:** see the capstone below.

**🎯 Checkpoint:**
1. Why use a multi-stage Dockerfile?
2. Why can the API reach `postgres:5432` inside Compose but not `localhost:5432`?
3. Liveness vs readiness?
4. What should CI run on every pull request, and what should block a merge?

---

### Junior capstone: Shop v1

Ship a production-shaped monolith:

- [ ] REST API for catalog, customers and orders (Minimal APIs or controllers, explain the choice)
- [ ] PostgreSQL through EF Core, migrations, at least one composite index you can justify
- [ ] Validation + ProblemDetails for every error path
- [ ] JWT authentication; admin-only endpoints for catalog changes
- [ ] Unit tests for domain rules, integration tests for endpoints
- [ ] Structured logging with request correlation
- [ ] `docker compose up` runs everything; health checks
- [ ] GitHub Actions: build, test, image push
- [ ] README: how to run it, architecture sketch, three things you would improve

---

## 5. Level 2: Mid

> **Goal:** design, secure, observe and run production services; turn Shop into a modular monolith with reliable async messaging.
> **Mindset:** now learn *how it works inside*, because production problems live there.

---

### M1. C# and the runtime in depth

> 📁 `ConsoleApp/02-mid/M1-csharp-runtime-depth/` · ⏱ 4 weeks · Track: 🧠
> **Goal:** explain what the runtime does with your code: memory, threads and async.

#### M1.1 Generics and type design in depth
🧠 · `M1.1-generics-type-design`
- Covariance and contravariance ⭐ (`IEnumerable<out T>`, `Action<in T>`), why arrays are unsafely covariant
- Constraints: `struct`, `class`, `notnull`, `unmanaged`, `new()`, base type, interface
- Static abstract interface members and generic math
- Default interface methods: what they're for (evolving interfaces)
- `IEquatable<T>`, `IComparable<T>`, `IComparer<T>`, `EqualityComparer<T>.Create` **(.NET 11)**
- How generics are compiled: shared code for reference types, specialized code for value types

#### M1.2 Memory management and the GC ⭐
🧠 · `M1.2-memory-gc`
- The managed heap; allocation is cheap, collection is not
- Generations 0/1/2 ⭐, the Large Object Heap, the Pinned Object Heap
- Workstation vs server GC, background GC, DATAS (dynamic heap sizing)
- Finalizers vs `IDisposable`; the dispose pattern; `IAsyncDisposable`, `using` declarations
- Memory leaks in managed code: events, static caches, timers, closures ⭐
- Weak references; reading GC metrics (→ M7.4)

#### M1.3 Iterators, expression trees, reflection and attributes
🧠 · `M1.3-iterators-expressions-reflection`
- `yield return` and the state machine the compiler generates
- `Func<T>` vs `Expression<Func<T>>` ⭐ (why EF Core needs expression trees)
- Reflection: reading types and attributes, its cost, caching delegates
- Custom attributes
- Source generators as the faster alternative (→ S1.4)

#### M1.4 Modern C#: from 12 to 15
🧠 · `M1.4-modern-csharp-12-15`
- C# 12–13: primary constructors, collection expressions, `params` collections, the `Lock` type
- C# 14: extension members, the `field` keyword, null-conditional assignment, implicit span conversions, partial constructors
- **(.NET 11)** C# 15: union types, closed hierarchies, collection expression arguments (`[with(capacity: n), ..items]`), extension indexers, labeled `break` / `continue`
- Exhaustive `switch` over unions and closed hierarchies; modeling results and states with them
- Reading language proposals; how to adopt a new language version in a team

#### M1.5 Threading and synchronization ⭐
🧠 · `M1.5-threading-synchronization`
- Threads vs the thread pool; thread pool starvation ⭐ and its symptoms
- Race conditions, deadlocks, livelocks; a deadlock demo
- `lock` and `System.Threading.Lock`, `Monitor`, `Interlocked`
- `SemaphoreSlim` for async code, `ReaderWriterLockSlim`
- Concurrent collections (`ConcurrentDictionary`, `ConcurrentQueue`), immutable collections
- `Parallel.For` / `Parallel.ForEachAsync`, PLINQ, and when parallelism hurts a web server

#### M1.6 Async internals and Channels ⭐
🧠 · `M1.6-async-internals-channels`
- The async state machine; what a continuation is
- `SynchronizationContext` vs `ExecutionContext` and `AsyncLocal<T>`
- `ValueTask` ⭐: when to use it and its rules (never await twice)
- `TaskCompletionSource`; wrapping callbacks; sync-over-async and fire-and-forget hazards
- `System.Threading.Channels`: producer/consumer, bounded channels and backpressure
- Runtime Async **(.NET 11)**: async handled by the runtime, cleaner stack traces, less overhead

**🛠 Shop v1.1:** a bounded `Channel` that processes order emails in the background; a C# 15 union (or a closed hierarchy on .NET 10) for payment results; a small console demo of thread pool starvation with sync-over-async.

**🎯 Checkpoint:**
1. Explain GC generations and why most objects should die young.
2. When is `ValueTask` the right choice, and what must you never do with one?
3. What is thread pool starvation, what are the symptoms, and what usually causes it?
4. Covariance vs contravariance, with `IEnumerable<out T>` and `Action<in T>`.
5. Model "a payment is Approved, Declined or Failed" in C# 14 and in C# 15.

---

### M2. Design and architecture

> 📁 `ConsoleApp/02-mid/M2-design-architecture/` · ⏱ 3 weeks · Track: 🧩
> **Goal:** structure code so it stays easy to change, and know what each style costs.

#### M2.1 SOLID, coupling and cohesion ⭐
🧩 · `M2.1-solid-coupling-cohesion`
- Each SOLID principle with a C# violation and a fix
- LSP traps (the rectangle/square problem, `NotSupportedException`)
- DIP vs DI vs IoC ⭐
- Coupling (afferent/efferent, temporal, data) and cohesion
- Law of Demeter, tell-don't-ask

#### M2.2 Design patterns that matter in backend code
🧩 · `M2.2-design-patterns`
- Strategy, Decorator ⭐ (with DI and Scrutor), Factory, Builder, Adapter, Facade
- Observer, Mediator, Chain of Responsibility (middleware, pipeline behaviors)
- Specification; Repository and Unit of Work, and why `DbContext` already is both ⭐
- Anti-patterns: service locator, god class, anemic domain model (a debate, → S2.2)

#### M2.3 Clean, Hexagonal and Onion architecture ⭐
🧩 · `M2.3-clean-hexagonal`
- The dependency rule; ports and adapters
- Use cases / application services; where EF Core and HTTP go
- Folder boundaries vs project boundaries
- Common criticisms: indirection, mapping overhead, "interface for everything"

#### M2.4 Vertical slices and CQRS basics ⭐
🧩 · `M2.4-vertical-slice-cqrs`
- Organizing by feature instead of by layer
- CQRS without event sourcing: separate read and write models
- The mediator pattern: MediatR is 💰 now; alternatives are plain handlers, the `Mediator` source generator or Wolverine
- Pipeline behaviors for validation, logging and transactions

#### M2.5 Error handling strategy
🧩 · `M2.5-error-handling-strategy`
- Exceptions vs the Result pattern ⭐ for expected business failures
- Mapping domain errors to ProblemDetails; `IExceptionHandler`
- Validation errors vs business rule violations vs infrastructure failures
- Results with C# 15 unions **(.NET 11)**

#### M2.6 The modular monolith ⭐
🧩 · `M2.6-modular-monolith`
- Modules with their own schema, public contracts and internal implementation
- In-process integration events between modules
- Enforcing boundaries: `internal`, separate projects, architecture tests
- Why it's the sensible default before microservices, and how it makes a later split cheap (→ S3.2)

**🛠 Shop v1.2:** restructure Shop into modules (Catalog, Basket, Ordering, Payments, Notifications), each with its own DB schema. Vertical slices inside modules, an architecture test that fails if Ordering touches Catalog's tables, and business errors as Results mapped to ProblemDetails.

**🎯 Checkpoint:**
1. Give a real LSP violation and fix it.
2. Is a repository on top of EF Core useful? Argue both sides.
3. Clean Architecture vs vertical slices: when do you choose each?
4. How do you keep module boundaries from eroding in a modular monolith?
5. Exceptions or Result types for "out of stock"?

---

### M3. Data in depth

> 📁 `ConsoleApp/02-mid/M3-data-depth/` · ⏱ 4 weeks · Track: 🗄️
> **Goal:** write fast, correct queries under concurrency, and choose the right store and cache.

#### M3.1 Advanced SQL
🗄️ · `M3.1-advanced-sql`
- Window functions ⭐ (`ROW_NUMBER`, `RANK`, `LAG`, running totals)
- CTEs and recursive CTEs; subqueries vs joins
- Upserts (`INSERT … ON CONFLICT`, `MERGE`)
- PostgreSQL `JSONB` and its indexes
- Views and materialized views; stored procedures and functions: when they're worth it

#### M3.2 Isolation levels, locking and concurrency ⭐
🗄️ · `M3.2-isolation-locking`
- Anomalies: dirty read, non-repeatable read, phantom, lost update, write skew ⭐
- Read committed, repeatable read, serializable, snapshot
- MVCC in PostgreSQL vs locking in SQL Server (and RCSI)
- Deadlocks: why they happen, how to read and avoid them
- Optimistic concurrency (`rowversion`, PostgreSQL `xmin`) vs pessimistic (`SELECT … FOR UPDATE`) ⭐

#### M3.3 Query performance and execution plans ⭐
🗄️ · `M3.3-query-performance`
- `EXPLAIN (ANALYZE, BUFFERS)`; sequential scan vs index scan vs index-only scan
- Covering, partial and expression indexes; statistics and selectivity
- SARGable predicates (functions on columns kill indexes)
- Pagination: offset vs keyset ⭐
- Connection pooling and its limits

#### M3.4 EF Core in depth and Dapper ⭐
🗄️ · `M3.4-efcore-depth-dapper`
- Projections, split queries, compiled queries, `ExecuteUpdate` / `ExecuteDelete`
- DbContext pooling, interceptors, global query filters (soft delete, multi-tenancy)
- Owned types, complex types, JSON columns, value converters, concurrency tokens
- Migrations in a team and in production: SQL scripts, migration bundles, never `Migrate()` on startup across many replicas
- Dapper: when a micro-ORM wins, and mixing both ⭐

#### M3.5 Caching ⭐
🗄️ · `M3.5-caching`
- In-memory vs distributed caching; `HybridCache` (with stampede protection)
- Redis: data types, TTL, eviction policies, persistence, Valkey and Garnet as alternatives
- Cache-aside, write-through, write-behind ⭐
- Invalidation strategies; cache stampede and thundering herd
- Output caching and HTTP caching (`ETag`, `Cache-Control`)

#### M3.6 NoSQL and choosing a database
🗄️ · `M3.6-nosql-choosing-db`
- Document (MongoDB, Cosmos DB), key-value (Redis), wide-column (Cassandra), search (Elasticsearch / OpenSearch)
- Document modeling: embed vs reference, partition keys
- When relational wins (most of the time) and when it doesn't
- Polyglot persistence and its operational cost

**🛠 Shop v1.3:** keyset pagination for the catalog; optimistic concurrency on stock; a sales report with window functions; `HybridCache` + Redis for product pages, with invalidation when a price changes; one query tuned with `EXPLAIN ANALYZE`, before and after recorded in the README.

**🎯 Checkpoint:**
1. Explain lost update and write skew. Which PostgreSQL isolation level prevents each?
2. Offset vs keyset pagination at page 10,000?
3. In an `EXPLAIN ANALYZE` output, what tells you that your index wasn't used?
4. How do you invalidate cached product data when the price changes? What about a stampede?
5. When would you choose a document database over PostgreSQL `JSONB`?

---

### M4. Production-grade APIs and identity

> 📁 `ConsoleApp/02-mid/M4-production-apis-identity/` · ⏱ 4 weeks · Tracks: 🌐 🔐
> **Goal:** build APIs that can evolve without breaking clients, protected by standard identity flows.

#### M4.1 ASP.NET Core in depth
🌐 · `M4.1-aspnetcore-depth`
- Host lifecycle; `IHostedService` vs `BackgroundService` ⭐ (→ M5.6)
- Options pattern in depth: `IOptions` vs `IOptionsSnapshot` vs `IOptionsMonitor`, `ValidateOnStart`
- DI in depth: keyed services, scope validation, decorators, factories
- Endpoint filters vs MVC filters; `IExceptionHandler`
- Rate limiting middleware ⭐ (fixed, sliding, token bucket, concurrency)
- Output caching, response compression, request timeouts; `[ShortCircuit]` **(.NET 11)**

#### M4.2 API design and evolution ⭐
🌐 · `M4.2-api-design-evolution`
- Resource modeling; versioning strategies (URL, header, media type) ⭐
- Breaking vs non-breaking changes; deprecation and sunset headers
- Idempotency keys for `POST` ⭐
- Cursor pagination, filtering, sorting; `PATCH` (JSON Patch vs merge patch)
- Long-running operations (`202 Accepted` + a status resource), webhooks
- OpenAPI as the contract; OpenAPI 3.2 is the default **(.NET 11)**

#### M4.3 gRPC and real-time communication
🌐 · `M4.3-grpc-realtime`
- Protobuf contracts and their evolution rules
- Unary vs streaming calls, deadlines, gRPC status codes
- gRPC vs REST: when to use each ⭐
- SignalR, WebSockets and Server-Sent Events: when each fits; scaling SignalR out (backplane)

#### M4.4 OAuth 2 and OpenID Connect ⭐
🔐 · `M4.4-oauth-oidc`
- Roles: resource owner, client, authorization server, resource server
- Authorization code + PKCE ⭐ (users), client credentials ⭐ (services), device code
- Access token vs ID token vs refresh token; refresh token rotation
- Scopes vs claims vs roles
- OAuth 2.1 and the security best practices (no implicit flow, no password grant)
- The BFF pattern for SPAs ⭐ (why tokens don't belong in `localStorage`)

#### M4.5 Keycloak, Entra ID and authorization in depth
🔐 · `M4.5-keycloak-entra-authorization`
- Keycloak in Docker: realms, clients, roles, mappers
- Microsoft Entra ID: app registrations, scopes, app roles
- Policy-based and resource-based authorization ⭐; `IAuthorizationHandler`
- Multi-tenancy and tenant claims; API keys and when they're acceptable
- Duende IdentityServer (💰) vs OpenIddict vs a hosted identity provider

#### M4.6 Cryptography, data protection and secrets
🔐 · `M4.6-crypto-secrets`
- Hashing vs encryption vs encoding ⭐; symmetric vs asymmetric; signatures
- Password hashing (PBKDF2, Argon2) and the ASP.NET Core Data Protection API
- Secret managers (Azure Key Vault) and managed identities, with no secrets in config
- TLS certificates and rotation
- Personal data: minimize it, encrypt it, delete it (GDPR basics)

**🛠 Shop v1.4:** Keycloak for login (auth code + PKCE in Scalar), client credentials for a worker, a policy where "customers can only read their own orders", idempotency keys on `POST /orders`, API versioning, rate limiting on public endpoints.

**🎯 Checkpoint:**
1. How do you make `POST /orders` safe to retry?
2. Authorization code + PKCE vs client credentials: who uses each?
3. Why shouldn't an SPA keep tokens in `localStorage`, and what does a BFF change?
4. List three breaking and three non-breaking API changes.
5. Hashing vs encryption vs encoding?

---

### M5. Messaging and resilience

> 📁 `ConsoleApp/02-mid/M5-messaging-resilience/` · ⏱ 4 weeks · Track: 🔀
> **Goal:** first steps into distributed systems: talk asynchronously without losing or duplicating work, and survive failing dependencies.

#### M5.1 Why async messaging ⭐
🔀 · `M5.1-messaging-fundamentals`
- Sync vs async communication; temporal coupling
- Queue (point-to-point) vs publish/subscribe
- Command vs event ⭐ (naming, ownership, one vs many receivers)
- Message queue vs event stream (RabbitMQ vs Kafka, first look, → S3.6)
- Delivery guarantees: at-most-once, at-least-once, and why "exactly-once" is misleading ⭐
- Message design: envelope, IDs, correlation, small vs fat events

#### M5.2 RabbitMQ fundamentals ⭐
🔀 · `M5.2-rabbitmq`
- AMQP model: exchanges (direct, topic, fanout, headers), queues, bindings, routing keys
- Acks, nacks, requeue; prefetch and consumer concurrency
- Publisher confirms; durable queues and persistent messages
- Quorum queues (RabbitMQ 4 removed classic mirrored queues) and streams
- Dead-letter exchanges, TTL, delayed retries; poison messages
- Competing consumers and what happens to ordering
- `RabbitMQ.Client` 7 (fully async API)

#### M5.3 .NET messaging libraries
🔀 · `M5.3-messaging-libraries`
- What a library adds over the raw client: serialization, retries, error queues, outbox, sagas, telemetry
- MassTransit (v8 Apache 2.0; v9 is 💰), Wolverine, Rebus, NServiceBus (💰)
- Consumers, message contracts, retry vs redelivery, error and skipped queues
- Choosing a library and checking its license

#### M5.4 Idempotent consumers and the outbox pattern ⭐
🔀 · `M5.4-outbox-idempotency`
- The dual-write problem ⭐ (save to DB *and* publish: one of them can fail)
- Transactional outbox: write the message in the same transaction, relay it later
- Inbox / deduplication table; idempotency keys; natural idempotency
- Message ordering and versioning in practice
- Library outboxes vs a hand-written relay (`BackgroundService` + `FOR UPDATE SKIP LOCKED`)

#### M5.5 Resilience patterns ⭐
🔀 · `M5.5-resilience-patterns`
- Transient vs permanent faults
- Timeouts (per attempt and total), retries with exponential backoff and jitter ⭐
- Circuit breaker ⭐, bulkhead, fallback, hedging, rate limiter
- Polly v8 and `Microsoft.Extensions.Http.Resilience` (`AddStandardResilienceHandler`)
- Retry storms and amplification: only retry idempotent operations, retry at one layer

#### M5.6 Background jobs and scheduling
🔀 · `M5.6-background-jobs`
- `BackgroundService` patterns: a DI scope per iteration, graceful stop, error handling
- Hangfire, Quartz.NET, TickerQ: persistence, retries, dashboards
- At-least-once job execution, so jobs must be idempotent
- Running a job only once across replicas (→ S3.1 distributed locks)

**🛠 Shop v1.5:** Ordering publishes `OrderPlaced` through an outbox; Notifications consumes it from RabbitMQ with an inbox table; poison messages go to a DLQ; a flaky fake payment provider sits behind a standard resilience handler; a nightly report runs as a scheduled job.

**🎯 Checkpoint:**
1. What is the dual-write problem, and how does an outbox solve it?
2. Why is "exactly-once delivery" misleading? What do we actually build?
3. Command vs event: naming and ownership?
4. Why add jitter to retries? What is a retry storm?
5. A consumer crashes after writing to the DB but before acking. What happens, and how do you make it safe?

---

### M6. Testing, delivery and Kubernetes

> 📁 `ConsoleApp/02-mid/M6-testing-delivery-k8s/` · ⏱ 4 weeks · Tracks: 🧪 🚢
> **Goal:** trust your tests, automate delivery, and run Shop on Kubernetes.

#### M6.1 Integration tests with real dependencies ⭐
🧪 · `M6.1-testcontainers`
- Testcontainers for PostgreSQL, Redis, RabbitMQ and Keycloak
- Customizing `WebApplicationFactory`: replacing services and config
- Resetting the database with Respawn; test data with Bogus and builders
- Testing messaging end to end (publish → consume → assert)
- Parallel tests and isolation

#### M6.2 Test strategy
🧪 · `M6.2-test-strategy`
- Pyramid vs trophy vs honeycomb (common for microservices)
- What to mock: only what you don't own; London vs classical schools
- Snapshot tests (Verify), architecture tests (NetArchTest, ArchUnitNET)
- Mutation testing (Stryker.NET); what code coverage does and doesn't tell you
- Flaky tests: causes and cures

#### M6.3 CI/CD pipelines
🚢 · `M6.3-cicd-pipelines`
- Pipeline stages, artifacts, environments and approvals
- Build once, deploy many ⭐; versioning artifacts and images
- Secrets in CI: OIDC federation instead of stored cloud credentials
- Central Package Management, NuGet audit, Dependabot / Renovate
- `dotnet format`, analyzers, warnings as errors
- GitHub Actions vs Azure Pipelines

#### M6.4 Containers in depth
🚢 · `M6.4-containers-depth`
- Image size and layer caching, BuildKit, multi-arch images
- Image scanning (Trivy), SBOMs, signing
- Tags vs digests; immutable tags
- Non-root, read-only file system, dropped capabilities
- .NET in containers: memory limits, GC heap hard limit, CPU limits and thread pool sizing

#### M6.5 Kubernetes fundamentals ⭐
🚢 · `M6.5-kubernetes-fundamentals`
- Why orchestration; control plane and worker nodes
- Pod, Deployment, ReplicaSet, Service (ClusterIP / NodePort / LoadBalancer), Namespace
- ConfigMaps and Secrets; mounting config into .NET
- Liveness, readiness and startup probes ⭐ mapped to ASP.NET Core health checks
- Requests and limits; QoS classes; rolling updates and rollbacks
- `kubectl` debugging: `get`, `describe`, `logs`, `exec`, `port-forward`; local clusters with kind

#### M6.6 Aspire for local distributed development
🚢 · `M6.6-aspire`
- AppHost and resources (PostgreSQL, Redis, RabbitMQ, Keycloak), references and service discovery
- Service defaults: OpenTelemetry, health checks, resilience
- The Aspire dashboard (logs, traces, metrics in one place)
- Integration testing with the Aspire test host; publishing and deployment targets
- What Aspire replaces (Compose for dev) and what it doesn't (production platform design)

**🛠 Shop v1.6:** Testcontainers suites for every module; an Aspire AppHost replacing Compose for development; CI that builds once, scans the image and pushes it by digest; Shop deployed to a local kind cluster with plain manifests and working probes.

**🎯 Checkpoint:**
1. Why Testcontainers instead of the EF Core in-memory provider?
2. "Build once, deploy many": why?
3. A pod keeps restarting. How do you find out why?
4. What does Aspire give you locally, and what does it not replace in production?
5. Why must memory limits and the .NET GC agree inside a container?

---

### M7. Observability and production diagnostics

> 📁 `ConsoleApp/02-mid/M7-observability/` · ⏱ 3 weeks · Tracks: 🚢 🧠
> **Goal:** know what Shop is doing in production, and find out why when it misbehaves.

#### M7.1 Structured logging at scale ⭐
🚢 · `M7.1-logging-at-scale`
- Log levels in production; high-performance logging with the `LoggerMessage` source generator
- Correlation IDs, enrichment, scopes
- Serilog sinks; Seq, Loki, Elasticsearch / OpenSearch
- Cost, retention and sampling; redaction (`Microsoft.Extensions.Compliance.Redaction`)

#### M7.2 Metrics ⭐
🚢 · `M7.2-metrics`
- `System.Diagnostics.Metrics`: `Meter`, `Counter`, `Histogram`, `UpDownCounter`, observable instruments
- Built-in ASP.NET Core, Kestrel, `HttpClient` and runtime metrics
- RED (rate, errors, duration) and USE (utilization, saturation, errors) ⭐
- Percentiles vs averages ⭐; histograms
- Prometheus scraping and Grafana dashboards; label cardinality traps

#### M7.3 Distributed tracing with OpenTelemetry ⭐
🚢 · `M7.3-distributed-tracing`
- Traces, spans, attributes, events; `ActivitySource` and `Activity`
- W3C Trace Context (`traceparent`) and propagation over HTTP and through message brokers ⭐
- OTLP, the OpenTelemetry Collector; Jaeger, Tempo, Azure Monitor
- Head vs tail sampling
- Native ASP.NET Core semantic-convention attributes **(.NET 11)**

#### M7.4 Diagnosing .NET in production
🧠 🚢 · `M7.4-dotnet-diagnostics`
- `dotnet-counters`, `dotnet-trace`, `dotnet-dump`, `dotnet-gcdump`, `dotnet-monitor`
- High CPU, memory growth and thread pool starvation: symptoms and first steps
- Reading a dump: threads, stacks, heap statistics
- Event pipe and what you can collect in a container (→ S1.5, S5.2)

**🛠 Shop v2.0:** see the capstone below.

**🎯 Checkpoint:**
1. Logs vs metrics vs traces: which answers which question?
2. Why are averages misleading for latency? What does p99 mean?
3. How does a trace context travel through RabbitMQ?
4. One pod is at 100% CPU in production. What are your first three steps?

---

### Mid capstone: Shop v2

A production-grade modular monolith:

- [ ] Modules: Catalog, Basket (Redis), Ordering, Payments (fake provider with random failures), Notifications; each with its own schema
- [ ] Integration events between modules; `OrderPlaced` → RabbitMQ through an outbox; idempotent consumers; DLQ handling
- [ ] Keycloak: auth code + PKCE for users, client credentials for workers; resource-based authorization
- [ ] Resilience on outbound calls; rate limiting and idempotency keys on public endpoints
- [ ] Testcontainers integration tests + architecture tests for module boundaries
- [ ] Aspire for local development; OpenTelemetry traces across HTTP **and** RabbitMQ
- [ ] A Grafana dashboard with RED metrics per endpoint
- [ ] Runs on a local Kubernetes cluster; CI builds, tests, scans and pushes the image
- [ ] Three ADRs: why a modular monolith, why RabbitMQ, why the outbox

---

## 6. Level 3: Senior

> **Goal:** design, run and evolve distributed systems; split Shop into microservices where it pays off, and prove it works under failure.
> **Mindset:** every answer starts with *"it depends on …"* and then says exactly what it depends on.

---

### S1. Runtime and performance engineering

> 📁 `ConsoleApp/03-senior/S1-runtime-performance/` · ⏱ 4 weeks · Track: 🧠
> **Goal:** make code fast with evidence, and know the runtime well enough to explain why it's fast.

#### S1.1 CLR internals: JIT, PGO and Native AOT ⭐
🧠 · `S1.1-clr-jit-aot`
- Tiered compilation, OSR, Dynamic PGO: what happens to a hot method over time
- ReadyToRun vs Native AOT ⭐: startup, size, throughput and what you give up (reflection, dynamic loading)
- Trimming and its warnings
- Assembly loading and `AssemblyLoadContext`; plugins and unloading
- Devirtualization, inlining, bounds-check elimination (why `sealed` can matter)

#### S1.2 GC deep dive and memory tuning
🧠 · `S1.2-gc-deep-dive`
- GC phases (mark, plan, sweep, compact), card tables, pinning and fragmentation
- Server GC + DATAS in containers; `GCHeapHardLimit`, `GCConserveMemory`
- `GC.GetGCMemoryInfo`, allocation rate as a key metric
- Finding leaks: heap snapshots, `gcroot`, comparing dumps

#### S1.3 High-performance C# ⭐
🧠 · `S1.3-high-performance-csharp`
- `Span<T>`, `ReadOnlySpan<T>`, `Memory<T>`, `ref struct` ⭐ and their limits
- `stackalloc`, `ArrayPool<T>`, `ObjectPool<T>`, `RecyclableMemoryStream`
- Strings: `string.Create`, `SearchValues`, UTF-8 literals, avoiding substrings
- Struct design: `readonly struct`, `in` parameters, avoiding defensive copies
- Hidden allocations: boxing, closures, LINQ and `params` in hot paths; frozen collections
- `System.IO.Pipelines` for parsing streams

#### S1.4 Source generators and AOT-friendly code
🧠 · `S1.4-source-generators`
- Incremental source generators: how they work and how to debug them
- Built-in generators: `System.Text.Json`, `LoggerMessage`, `Regex`, options validation, configuration binding
- Replacing reflection with generated code; making a library AOT- and trim-safe

#### S1.5 Benchmarking and profiling ⭐
🧠 · `S1.5-benchmarking-profiling`
- BenchmarkDotNet: `MemoryDiagnoser`, baselines, parameters, common mistakes
- Profilers: dotTrace / dotMemory, PerfView, Visual Studio; reading flame graphs
- Load testing with k6 or NBomber; latency under load vs in isolation
- Finding the real bottleneck: CPU vs I/O vs lock contention vs GC vs the database ⭐
- Performance budgets and regression checks in CI

#### S1.6 Memory model and advanced concurrency
🧠 · `S1.6-memory-model-concurrency`
- The .NET memory model: reordering, `volatile`, `Volatile`, `Interlocked`, memory barriers
- Lock-free patterns and when not to write them
- `ConcurrentDictionary` internals and the `GetOrAdd` factory trap
- Async coordination: async locks, `Channel` pipelines, TPL Dataflow, `IAsyncEnumerable` backpressure

**🛠 Shop v2.1:** benchmark and optimize one hot path (e.g. a price calculation or JSON serialization) with recorded BenchmarkDotNet results; a k6 load test with a p99 target; find and fix one allocation hotspot with a profiler; try Native AOT on one small service and record the trade-offs.

**🎯 Checkpoint:**
1. What does Native AOT cost you, and when is it worth it?
2. Why is `Span<T>` a `ref struct`, and what can't you do with it?
3. How do you *prove* a change improved performance?
4. Tiered compilation and Dynamic PGO: what happens to a hot method over time?
5. Server GC vs workstation GC vs DATAS in a 512 MB container?

---

### S2. Domain-Driven Design

> 📁 `ConsoleApp/03-senior/S2-ddd/` · ⏱ 3 weeks · Track: 🧩
> **Goal:** find good boundaries in the business before drawing them in code, and make decisions explicit.

#### S2.1 Strategic DDD ⭐
🧩 · `S2.1-strategic-ddd`
- Domain, subdomains: core, supporting, generic
- Ubiquitous language
- Bounded contexts ⭐ and how they become modules or services
- Context mapping: partnership, customer/supplier, conformist, anti-corruption layer, open host service, published language
- Event Storming as a discovery workshop

#### S2.2 Tactical DDD ⭐
🧩 · `S2.2-tactical-ddd`
- Entities vs value objects (records; EF Core complex types)
- Aggregates and invariants ⭐; small aggregates, reference by ID, one aggregate per transaction
- Domain services, factories, repositories per aggregate
- Domain events vs integration events ⭐
- Rich vs anemic models: when "anemic" is actually fine

#### S2.3 Event sourcing and CQRS in depth
🧩 · `S2.3-event-sourcing-cqrs`
- Events as the source of truth; streams, append-only storage
- Projections and read models; eventual consistency of reads
- Snapshots; event versioning and upcasting
- Marten, KurrentDB (formerly EventStoreDB)
- When **not** to use event sourcing (most systems)

#### S2.4 Architecture decisions and documentation ⭐
🧩 · `S2.4-architecture-decisions`
- Quality attributes (ISO 25010) and trade-off analysis
- Architecture Decision Records ⭐
- The C4 model for diagrams
- Fitness functions; managing technical debt
- Design docs and RFCs: how to write one that gets read

**🛠 Shop v2.2:** run a solo Event Storming session for checkout; draw a context map; refactor `Order` into a proper aggregate with invariants; split domain events from integration events; write C4 context and container diagrams.

**🎯 Checkpoint:**
1. How do you find bounded contexts? Use Shop as the example.
2. Why should an aggregate be small, and how do you refer to another aggregate?
3. Domain event vs integration event?
4. When is event sourcing the wrong choice?
5. Outline an ADR for "PostgreSQL vs Cosmos DB for orders".

---

### S3. Microservices architecture

> 📁 `ConsoleApp/03-senior/S3-microservices/` · ⏱ 6 weeks · Track: 🔀
> **Goal:** the core of this roadmap. Know when microservices pay off, how to split, and how to keep data consistent across services.

#### S3.1 Distributed systems fundamentals ⭐
🔀 · `S3.1-distributed-fundamentals`
- The fallacies of distributed computing; partial failure; a timeout is a guess
- CAP and PACELC ⭐ with concrete examples
- Consistency models: strong / linearizable, causal, read-your-writes, eventual
- Time and ordering: clock skew, logical and vector clocks (intuition)
- Consensus and leader election (Raft intuition)
- Distributed locks: Redis / Redlock debate, PostgreSQL advisory locks, fencing tokens ⭐

#### S3.2 Monolith, modular monolith or microservices? ⭐
🔀 · `S3.2-when-microservices`
- What microservices buy: independent deployment, team autonomy, independent scaling, fault isolation
- What they cost: network, consistency, operations, debugging, people
- Conway's law and Team Topologies
- The distributed monolith anti-pattern ⭐ and how to spot it
- Decomposing by business capability / subdomain (→ S2.1)
- The Strangler Fig pattern ⭐ for gradual extraction

#### S3.3 Service communication and API gateways
🔀 · `S3.3-communication-gateway`
- Sync (REST, gRPC) vs async (messages, events): choosing per interaction
- API gateway and Backend-for-Frontend ⭐ with YARP
- What a gateway should do (routing, auth offload, rate limiting, aggregation) and shouldn't (business logic)
- API composition; chatty services and how to fix them
- Service discovery: DNS, Kubernetes services, Aspire

#### S3.4 Data in microservices ⭐
🔀 · `S3.4-data-ownership`
- Database per service; no shared tables ⭐; data ownership
- Replicating data with events: event-carried state transfer, local read models
- Change Data Capture (Debezium) as an alternative to the outbox
- Queries across services: API composition vs replicated read models
- Reporting and analytics: a separate store, not cross-service joins

#### S3.5 Distributed transactions and sagas ⭐
🔀 · `S3.5-sagas`
- Why two-phase commit is avoided
- Saga choreography vs orchestration ⭐: trade-offs and when to pick each
- Compensating actions; pending states (semantic locks); timeouts
- Saga state machines (MassTransit, Wolverine, or hand-written); process managers
- Failure modes: lost replies, duplicate steps, compensation that fails

#### S3.6 Event-driven architecture and Kafka ⭐
🔀 · `S3.6-eda-kafka`
- Event notification vs event-carried state transfer vs event sourcing
- Kafka: topics, partitions ⭐, keys and ordering, consumer groups, offsets and commits
- Retention, compaction, replication and ISR; KRaft (Kafka 4 removed ZooKeeper)
- Idempotent producers and transactions; schema registry with Avro or Protobuf
- Kafka vs RabbitMQ vs Azure Service Bus vs Event Hubs ⭐
- The `Confluent.Kafka` client in .NET; consumer lag as a metric

#### S3.7 Contracts and versioning across services ⭐
🔀 🧪 · `S3.7-contracts-versioning`
- Versioning messages and APIs; tolerant reader
- Expand/contract (parallel change) ⭐ for APIs, events and database schemas
- Consumer-driven contract testing with Pact
- Backward- and forward-compatible schemas; deprecation policy
- Database migrations that survive rolling deployments

**🛠 Shop v2.3:** extract Ordering, Payments, Inventory and Notifications using the Strangler Fig pattern (Catalog and Basket stay in the monolith: prove you don't have to split everything). Add a YARP gateway, a database per service, an orchestrated order saga with compensations, and Kafka for order events feeding an analytics projection. Add Pact tests between Ordering and Payments.

**🎯 Checkpoint:**
1. Explain CAP and PACELC with a concrete example from Shop.
2. Your company wants microservices. How do you decide, and where do you start?
3. Design order → payment → inventory as a saga and show the compensations.
4. How does Kafka guarantee ordering, and how can you accidentally break it?
5. How do you roll out a breaking change to an event consumed by four services?
6. What is a distributed monolith, and how do you recognize one?

---

### S4. Cloud-native platform and Azure

> 📁 `ConsoleApp/03-senior/S4-cloud-native-azure/` · ⏱ 4 weeks · Track: 🚢
> **Goal:** choose the simplest platform that meets the requirements, and deploy to it repeatably and safely.

#### S4.1 Kubernetes in production ⭐
🚢 · `S4.1-kubernetes-production`
- HPA and KEDA ⭐ (scale consumers on queue length or Kafka lag)
- Pod disruption budgets, affinity / anti-affinity, topology spread
- Gateway API ⭐ (ingress-nginx was retired in March 2026)
- Network policies, RBAC, service accounts, workload identity
- StatefulSets, and why your database usually shouldn't run in the cluster
- Tuning resources for .NET workloads

#### S4.2 Packaging and GitOps
🚢 · `S4.2-helm-gitops`
- Helm charts vs Kustomize
- GitOps with Argo CD or Flux; environment promotion
- Secrets: External Secrets, Sealed Secrets, the Key Vault CSI driver

#### S4.3 Deployment strategies ⭐
🚢 · `S4.3-deployment-strategies`
- Rolling, blue-green, canary ⭐; traffic splitting
- Feature flags (`Microsoft.FeatureManagement`), dark launches
- Zero-downtime database migrations (expand/contract, → S3.7)
- Rollback vs roll forward; automated rollback on SLO breach

#### S4.4 Azure for .NET microservices ⭐
🚢 · `S4.4-azure-microservices`
- Choosing compute ⭐: App Service vs Container Apps vs AKS vs Functions
- Messaging: Service Bus (sessions, DLQ, duplicate detection) vs Event Hubs vs Event Grid
- Data: Azure Database for PostgreSQL, Azure SQL, Cosmos DB, Azure Managed Redis
- API Management; Front Door and Application Gateway
- Managed identity ⭐ and Key Vault; VNets, private endpoints, NSGs
- Cost awareness (→ certifications: AZ-900 and AI-200 below)

#### S4.5 Infrastructure as Code
🚢 · `S4.5-infrastructure-as-code`
- Bicep vs Terraform ⭐; `azd`
- State, modules, environments, drift detection
- Policy as code; reviewing infrastructure changes like code

#### S4.6 Service mesh, Dapr and sidecars
🚢 · `S4.6-service-mesh-dapr`
- What a mesh adds: mTLS, retries, traffic splitting, telemetry
- Istio (including ambient mode) and Linkerd
- Dapr building blocks
- When they're worth the complexity and when they aren't

**🛠 Shop v2.4:** Helm charts and Argo CD (or Container Apps provisioned with Bicep: pick one and write the ADR); KEDA scaling Notifications on queue length; workload identity to Key Vault with no secrets in config; a canary release of Payments.

**🎯 Checkpoint:**
1. App Service vs Container Apps vs AKS for eight services and a five-person team?
2. Canary vs blue-green: trade-offs, and what each means for database migrations?
3. How does a pod read secrets from Key Vault without any secret in its configuration?
4. How do you scale consumers on queue length?
5. Do you need a service mesh? What would convince you?

---

### S5. Reliability, scale and security engineering

> 📁 `ConsoleApp/03-senior/S5-reliability-security/` · ⏱ 4 weeks · Tracks: 🚢 🔀 🔐
> **Goal:** keep Shop up, fast and safe as it grows, and handle the day it breaks.

#### S5.1 SLIs, SLOs and error budgets ⭐
🚢 · `S5.1-slo-error-budgets`
- Choosing SLIs that reflect user experience
- SLO targets and error budget policies
- Alerting on symptoms with burn-rate alerts, not on causes
- Dashboards that answer questions

#### S5.2 Incident response and production debugging ⭐
🚢 · `S5.2-incident-response`
- On-call, severity levels, runbooks
- Triage with traces, metrics and logs; finding slow hops in a call chain
- Memory and CPU incidents in pods (`dotnet-monitor`, dumps from Kubernetes)
- Blameless postmortems and follow-up actions

#### S5.3 Scalability and performance at system level ⭐
🔀 · `S5.3-scalability`
- Vertical vs horizontal scaling; stateless services
- Caching layers: CDN, edge, distributed cache
- Load balancing algorithms; L4 vs L7
- Connection limits ⭐ (pods × pool size vs database max connections), PgBouncer
- Backpressure, load shedding, queue-based load leveling
- Read replicas, partitioning and sharding ⭐ (hot keys, rebalancing), consistent hashing

#### S5.4 Chaos and resilience testing
🧪 🔀 · `S5.4-chaos-resilience-testing`
- Failure injection with Polly chaos strategies; game days
- Scenarios: broker down, duplicate delivery, downstream timeout, DB write succeeds but publish fails, pod killed mid-message, bad contract deployed, traffic spike
- Load, stress and soak testing with k6

#### S5.5 Security engineering for distributed systems ⭐
🔐 · `S5.5-security-engineering`
- Zero trust: never trust the internal network
- Service-to-service auth: client credentials, mTLS, workload identity; token exchange and propagation
- Authorization at the gateway vs in each service
- OWASP API Security Top 10 ⭐; threat modeling with STRIDE
- Supply chain: SBOMs, signed images, NuGet audit, package age policies
- Secret rotation, audit logging, tenant isolation

#### S5.6 Disaster recovery and data resilience
🚢 · `S5.6-disaster-recovery`
- RPO and RTO ⭐; backups and **tested** restores
- Multi-zone vs multi-region; active-passive vs active-active
- Failover for brokers and databases; split brain
- Data residency and compliance constraints

**🛠 Shop v2.5:** SLOs for checkout with burn-rate alerts; a k6 soak test; three chaos scenarios run and written up; a postmortem for one injected outage; a threat model of the checkout flow; a documented and tested restore.

**🎯 Checkpoint:**
1. Define an SLO for checkout, and an alert that won't wake you for nothing.
2. Thirty pods with a pool size of 100 against a database that allows 500 connections: what happens, and how do you fix it?
3. Walk through a postmortem for an outage caused by a retry storm.
4. How do services authenticate to each other in a zero-trust setup on Azure?
5. Orders need RPO 5 minutes and RTO 1 hour. What architecture?

---

### S6. System design and technical leadership

> 📁 `ConsoleApp/03-senior/S6-system-design-leadership/` · ⏱ 3 weeks · Track: 🧩
> **Goal:** design systems on a whiteboard under time pressure, and lead technical work in a team.

#### S6.1 A method for system design ⭐
🧩 · `S6.1-system-design-method`
- Functional and non-functional requirements; asking the right questions
- Back-of-the-envelope estimates: QPS, storage, bandwidth, latency numbers
- API and data model → high-level design → deep dives → bottlenecks → trade-offs

#### S6.2 Building blocks
🧩 · `S6.2-building-blocks`
- Load balancer, reverse proxy, CDN, cache, queue, stream, object storage, search index
- Rate limiter, ID generation (UUIDv7 via `Guid.CreateVersion7()`, Snowflake IDs)
- Fan-out for notifications and feeds

#### S6.3 Case studies
🧩 · `S6.3-case-studies`
- URL shortener, rate limiter, notification system
- E-commerce checkout (Shop), ticket booking (no overselling), payments (idempotency and ledgers)
- News feed, chat (SignalR scale-out), distributed job scheduler

#### S6.4 Technical leadership ⭐
🧩 · `S6.4-technical-leadership`
- Code review culture; mentoring
- Writing design docs and ADRs; running design reviews
- Leading migrations without a big-bang rewrite
- Estimation and risk; managing technical debt; build vs buy
- Disagreeing productively; communicating with non-engineers

**🛠 Shop v3.0:** see the capstone below.

**🎯 Checkpoint:**
1. Design a flash-sale checkout that never oversells.
2. Estimate storage and peak QPS for one million orders a day.
3. How do you lead a monolith-to-services migration without a big-bang rewrite?
4. How do you push back on an architecture decision you disagree with?

---

### Senior capstone: Shop v3

Distributed, observable and failure-tested:

- [ ] Ordering, Payments, Inventory, Notifications as services; Catalog and Basket remain in the monolith, and the ADR says why
- [ ] YARP gateway / BFF; Keycloak or Entra ID; service-to-service client credentials
- [ ] Orchestrated order saga with compensations (payment timeout, out of stock)
- [ ] RabbitMQ or Azure Service Bus for commands; Kafka for order events → analytics projection
- [ ] Database per service; outbox or CDC
- [ ] Pact contract tests; an event moved from v1 to v2 with expand/contract
- [ ] Helm + Argo CD on AKS, **or** Container Apps with Bicep/Terraform; Key Vault + workload identity
- [ ] SLOs with burn-rate alerts; OpenTelemetry → Grafana (or Azure Monitor)
- [ ] k6 load test; chaos scenarios: broker down, duplicate delivery, payment timeout, pod killed mid-saga, failed deploy + rollback
- [ ] ADRs, C4 diagrams and one postmortem

---

## 7. Interview lane

> 📁 `ConsoleApp/04-interview/` · Track: 🎤 · About 1 hour a week from the start of Junior, more in the last 4 weeks before interviews.
> Each stage's 🎯 checkpoint is interview practice too: answer it out loud, against a timer.

#### I1 Live coding in C# and DSA essentials
🎤 · `I1-live-coding-dsa`
- Big-O analysis; arrays, strings, hash maps, sets
- Patterns: two pointers, sliding window, prefix sums, binary search, stack, BFS / DFS, heaps, intervals, basic dynamic programming
- C# tools for interviews: `PriorityQueue<TElement, TPriority>`, `Dictionary`, `HashSet`, spans, LINQ (and when not to use it)
- Thinking out loud; testing your own code; a curated list (Blind 75 / NeetCode 150 style)

#### I2 .NET technical question bank
🎤 · `I2-dotnet-question-bank`
- Every ⭐ topic as a rapid-fire question, grouped by level, linked to its topic
- What a strong answer contains vs red-flag answers
- "Explain it to a junior" and "go one level deeper" follow-ups

#### I3 System design interviews
🎤 · `I3-system-design-interviews`
- The 45-minute structure; clarifying questions; estimation shortcuts
- Drawing clearly; naming trade-offs; handling "what if traffic grows 10×?"
- Mock prompts based on S6.3, with sample answers

#### I4 Behavioral interviews and STAR
🎤 · `I4-behavioral-star`
- STAR answers; a bank of 8–10 stories (conflict, failure, ambiguity, ownership, mentoring, incident, disagreement)
- What senior answers sound like: scope, influence, trade-offs
- Questions to ask the interviewer

#### I5 Take-homes, code review and mock interviews
🎤 · `I5-take-homes-mocks`
- Scoping a take-home; README, tests and trade-off notes that reviewers want
- Live code review exercises: reviewing a PR in an interview
- Mock interview checklists for Junior, Mid and Senior

---

## 8. Track threads

How each subject spirals through the levels. Use this when you want to revise one subject end to end.

- **🧠 C# and the .NET runtime:** J1.1, J1.2 → J2.1–J2.5 → J3.1–J3.3 → J4.1–J4.3 → M1.1–M1.6 → M7.4 → S1.1–S1.6
- **🌐 HTTP, ASP.NET Core and APIs:** J1.3, J1.4 → J3.4–J3.6 → J4.6 → M4.1–M4.3 → S3.3
- **🗄️ Data:** J2.6 → J4.4, J4.5 → M3.1–M3.6 → S3.4 → S5.3
- **🧩 Design and architecture:** J5.6 → J6.5 → M2.1–M2.6 → S2.1–S2.4 → S6.1–S6.4
- **🔀 Distributed systems and microservices:** J4.6 → M5.1–M5.6 → S3.1–S3.7 → S5.3, S5.4
- **🔐 Security and identity:** J5.4, J5.5 → M4.4–M4.6 → S5.5
- **🧪 Testing and quality:** J5.1, J5.2 → M6.1, M6.2 → S1.5 (benchmarks), S3.7 (contracts), S5.4 (chaos)
- **🚢 Delivery and operations:** J1.5, J1.6 → J5.3 → J6.1–J6.4 → M6.3–M6.6 → M7.1–M7.3 → S4.1–S4.6 → S5.1, S5.2, S5.6
- **🎤 Interview skills:** I1–I5 in parallel, plus a 🎯 checkpoint at the end of every stage

---

## 9. Study plan

### Pace

About **8–10 hours a week**:

- ~5 h: topics (README + code + quiz)
- ~3 h: the Shop project step
- ~1 h: interview lane
- ~1 h: review (redo old quiz questions you got wrong)

| Level | Duration at this pace |
|---|---|
| 🟢 Junior: J1–J6 + capstone | ~18 weeks |
| 🟡 Mid: M1–M7 + capstone | ~29 weeks |
| 🔴 Senior: S1–S6 + capstone | ~30 weeks |

### Placement test (if you already work as a developer)

You don't have to start at J1. For each stage, take the quizzes first:

- **≥ 80%:** skim the README for gaps, do the project step, move on.
- **50–80%:** study only the topics you missed.
- **< 50%:** do the stage properly.

### 12-week fast track to a senior microservices interview

For when an interview is close. Only ⭐-heavy topics:

1. **Week 1:** J4.2, M1.5, M1.6 (async, threading)
2. **Week 2:** M1.2, S1.3 (GC, `Span<T>`)
3. **Week 3:** M3.2, M3.3, M3.4 (isolation, plans, EF Core)
4. **Week 4:** M2.3, M2.4, M2.6 (Clean, vertical slices, modular monolith)
5. **Week 5:** S2.1, S2.2 (DDD)
6. **Week 6:** S3.1, S3.2, S3.3 (distributed fundamentals, when to split, communication)
7. **Week 7:** M5.1, M5.2, M5.4 (messaging, RabbitMQ, outbox)
8. **Week 8:** S3.4, S3.5, S3.6 (data ownership, sagas, Kafka)
9. **Week 9:** M5.5, S3.7, S4.3 (resilience, contracts, deployments)
10. **Week 10:** M7.1–M7.3, S5.1 (observability, SLOs)
11. **Week 11:** M4.4, S5.5, S4.4 (OAuth/OIDC, security, Azure)
12. **Week 12:** S6.1–S6.3, I3, I4 + two mock interviews

### Certifications (already in this repo, in Turkish)

Optional and separate from the levels. Take them when the related stages are done:

- [AZ-900: Azure Fundamentals](ConsoleApp/certifications/01-az-900-azure-fundamentals/README.md): fits after M6
- [AI-200: Azure AI Cloud Developer](ConsoleApp/certifications/02-ai-200-azure-ai-cloud-developer/README.md): fits alongside S4

---

## 10. Electives

Worth knowing, but not on the core path. Add them as topics later if you need them:

- **AI in .NET backends:** `Microsoft.Extensions.AI`, Microsoft Agent Framework, the MCP C# SDK, RAG with pgvector
- **GraphQL** with Hot Chocolate
- **Orleans** (virtual actors) for stateful, high-concurrency workloads
- **Serverless** with Azure Functions, in depth
- **Search** with Elasticsearch / OpenSearch, in depth
- **Blazor** basics for backend developers
- **AWS equivalents** of the Azure services in S4.4

---

## 11. 2026 radar

Facts that change what you learn. Check them again before an interview.

- **.NET 11 is out (November 10, 2026)** as an STS release with two years of support. .NET 8 and .NET 9 reached end of support the same day; .NET 10 is the current LTS. [What's new in .NET 11](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-11/overview)
- **C# 15:** union types, closed hierarchies, collection expression arguments, extension indexers, labeled `break`/`continue`; memory-safety rules are still in preview. [What's new in C# 15](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-15)
- **.NET 11 runtime and SDK:** Runtime Async is on for `net11.0` projects; test templates default to xUnit v3 on Microsoft.Testing.Platform.
- **ASP.NET Core 11:** OpenAPI 3.2 is the default (a breaking change), async validation, built-in OpenTelemetry HTTP server attributes, `[ShortCircuit]`. [What's new in ASP.NET Core 11](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-11)
- **Aspire 13** dropped the ".NET" prefix and became polyglot (Python and JavaScript; TypeScript AppHost in 13.4). [What's new in Aspire 13](https://aspire.dev/whats-new/aspire-13/)
- **Licensing:** MediatR and AutoMapper went commercial in 2025; MassTransit v9 is commercial (v8 remains Apache 2.0); FluentAssertions 8 and Duende IdentityServer are paid. Free alternatives: plain handlers or `Mediator`, Mapperly, Wolverine or Rebus, Shouldly, OpenIddict or Keycloak. [MassTransit v9 announcement](https://masstransit.io/introduction/v9-announcement) · [Milan Jovanović on the license changes](https://milanjovanovic.tech/blog/mediatr-and-masstransit-going-commercial-what-this-means-for-you)
- **Kubernetes:** ingress-nginx was retired in March 2026; new work should use the Gateway API. [Kubernetes blog](https://kubernetes.io/blog/2025/11/11/ingress-nginx-retirement/)
- **OWASP Top 10 2025:** new A03 *Software Supply Chain Failures* and A10 *Mishandling of Exceptional Conditions*; SSRF folded into A01 Broken Access Control. [OWASP Top 10 2025](https://owasp.org/Top10/2025/0x00_2025-Introduction/)
- **Brokers:** Kafka 4 runs only in KRaft mode (no ZooKeeper); RabbitMQ 4 removed classic mirrored queues in favor of quorum queues; `RabbitMQ.Client` 7 is fully async.

---

## 12. Resources

### Books

- **C# and runtime:** *C# in Depth* (Skeet) · *Concurrency in C# Cookbook* (Cleary) · *Pro .NET Memory Management* (Kokosa) · *CLR via C#* (Richter, old but still the best on internals)
- **Design:** *Dependency Injection Principles, Practices, and Patterns* (van Deursen, Seemann) · *Unit Testing Principles, Practices, and Patterns* (Khorikov) · *Fundamentals of Software Architecture* (Richards, Ford)
- **DDD:** *Learning Domain-Driven Design* (Khononov) · *Domain-Driven Design Distilled* (Vernon)
- **Distributed systems:** *Designing Data-Intensive Applications* (Kleppmann) · *Building Microservices*, 2nd ed. and *Monolith to Microservices* (Newman) · *Microservices Patterns* (Richardson) · *Software Architecture: The Hard Parts* (Ford et al.)
- **Production:** *Release It!*, 2nd ed. (Nygard) · *Site Reliability Engineering* (Google, free online)
- **System design:** *System Design Interview*, vol. 1 and 2 (Xu)

### Official documentation

- [.NET documentation](https://learn.microsoft.com/dotnet/) · [ASP.NET Core](https://learn.microsoft.com/aspnet/core/) · [EF Core](https://learn.microsoft.com/ef/core/)
- [.NET microservices architecture guide](https://learn.microsoft.com/dotnet/architecture/microservices/) · [eShop reference app](https://github.com/dotnet/eShop)
- [Azure Architecture Center: cloud design patterns](https://learn.microsoft.com/azure/architecture/patterns/)
- [Aspire](https://aspire.dev/) · [Kubernetes](https://kubernetes.io/docs/) · [RabbitMQ](https://www.rabbitmq.com/docs) · [Apache Kafka](https://kafka.apache.org/documentation/) · [OpenTelemetry](https://opentelemetry.io/docs/) · [Terraform](https://developer.hashicorp.com/terraform/docs)
- [microservices.io](https://microservices.io/) (Chris Richardson's pattern catalog)

### Other roadmaps used for this one

- [roadmap.sh: ASP.NET Core](https://roadmap.sh/aspnet-core) · [roadmap.sh: Backend](https://roadmap.sh/backend) · [roadmap.sh: System Design](https://roadmap.sh/system-design)
- [milanm/DotNet-Developer-Roadmap](https://github.com/milanm/DotNet-Developer-Roadmap)

### Blogs and channels

Andrew Lock · Stephen Cleary · Stephen Toub's .NET performance posts · Steven Giesel · Milan Jovanović · Nick Chapsas · Derek Comartin (CodeOpinion) · Vladimir Khorikov · Martin Fowler
