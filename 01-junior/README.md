[← Roadmap](../ROADMAP.md) · [Mid →](../02-mid/README.md)

# 🟢 Junior: ship a tested CRUD API on your own

~17 weeks · Project: **Shop v1**

## Topics

### J1 · How code runs and how the internet works · 3 wk
- **J1.1** .NET platform & CLI ⭐: SDK vs runtime, LTS/STS, `csproj`, NuGet, `.slnx`, file-based apps
- **J1.2** From C# to running code ⭐: IL, assemblies, CLR, JIT, `bin`/`obj`, run vs build vs publish
- **J1.3** How the internet works ⭐: clients and servers, ISPs, packets and routing, *what happens when you type a URL*
- **J1.4** OSI & TCP/IP models ⭐: 7 vs 4 layers, encapsulation, L2/L3/L4/L7, which layer a problem lives in
- **J1.5** IP, TCP & UDP ⭐: IPv4/IPv6, CIDR, private ranges, NAT, ports; TCP handshake, retransmits, flow and congestion control, `TIME_WAIT`; UDP and when to use it; a C# TCP/UDP echo demo
- **J1.6** DNS & TLS ⭐: A/AAAA/CNAME records, the resolver chain, TTL and caching; the TLS 1.3 handshake, certificates, CAs
- **J1.7** HTTP ⭐: methods, safe vs idempotent, status codes, headers, cookies, HTTP/1.1 vs /2 vs /3
- **J1.8** Git & debugging: merge vs rebase, reset vs revert, PR flow, breakpoints, stack traces

### J2 · Types, objects and SQL · 3 wk
- **J2.1** Type system & memory ⭐: value vs reference, stack vs heap, boxing, `ref`/`out`/`in`, nullability, strings
- **J2.2** OOP ⭐: the four pillars, `virtual`/`override`/`new`, abstract class vs interface, access modifiers
- **J2.3** Class members & records ⭐: properties, `required`, `field`, constructors, `readonly`, class vs struct vs record
- **J2.4** Exceptions ⭐: `try`/`catch`/`finally`, filters, `throw;` vs `throw ex;`, custom exceptions, `using`
- **J2.5** Everyday modern C#: pattern matching, `switch` expressions, collection expressions, extension members
- **J2.6** SQL basics ⭐: keys, normalization, `JOIN`s, `GROUP BY`/`HAVING`, `NULL`, logical query order

### J3 · Collections, LINQ and the first API · 3 wk
- **J3.1** Collections & generics ⭐: `List`, `Dictionary` (hashing), `HashSet`, collection interfaces, constraints, Big-O
- **J3.2** Delegates, lambdas & events: `Func`/`Action`, closures, the loop-capture bug, event leaks
- **J3.3** LINQ ⭐: deferred execution, multiple enumeration, `First` vs `Single`, `IEnumerable` vs `IQueryable`
- **J3.4** Host, DI & configuration ⭐: lifetimes, captive dependencies, config providers, Options, environments
- **J3.5** Middleware pipeline ⭐: order, `Use`/`Run`/`Map`, custom middleware, short-circuiting
- **J3.6** REST APIs ⭐: Minimal APIs vs controllers, binding, DTOs, validation, ProblemDetails, OpenAPI

### J4 · Async, data access and HTTP clients · 3 wk
- **J4.1** async/await & cancellation ⭐: I/O- vs CPU-bound, `Task`, `.Result` deadlocks, `WhenAll`, `CancellationToken`
- **J4.2** EF Core ⭐: `DbContext`, migrations, change tracking, `AsNoTracking`, `Include`, the N+1 problem
- **J4.3** Indexes & transactions ⭐: B-tree, composite index column order, ACID, `SaveChanges` as a transaction
- **J4.4** HttpClient ⭐: socket exhaustion, `IHttpClientFactory`, typed clients, timeouts, `System.Text.Json`

### J5 · Tests, logs and security · 3 wk
- **J5.1** Unit testing ⭐: xUnit v3, AAA, theories, stub vs mock vs fake, NSubstitute, Shouldly
- **J5.2** Integration tests: `WebApplicationFactory`, `TimeProvider`, why the EF in-memory provider lies
- **J5.3** Logging: `ILogger`, levels, structured message templates ⭐, scopes, Serilog, never log secrets
- **J5.4** Auth & JWT ⭐: authN vs authZ, JWT validation (`iss`/`aud`/`exp`/signature), claims, roles, policies
- **J5.5** Web security ⭐: OWASP Top 10 2025, SQL injection, XSS, CSRF, CORS, HTTPS/HSTS, User Secrets

### J6 · Ship it · 2 wk
- **J6.1** Docker ⭐: images vs containers, layers, multi-stage Dockerfile, SDK container publish, non-root
- **J6.2** Docker Compose: service DNS, volumes, health checks, `depends_on`
- **J6.3** CI with GitHub Actions: build + test on PR, caching, push an image, branch protection
- **J6.4** Config & health: twelve-factor config, liveness vs readiness ⭐, graceful shutdown
- **J6.5** Clean code & layering: naming, SOLID at a glance, layers, the repository-over-EF debate

## Project: Shop v1

A monolith API. Do each task after its stage.

- [ ] **J1:** create the Shop repo and an empty API; explain the output of `curl -v`, `dig` and `traceroute` for a public API
- [ ] **J2:** domain types (Product, Category, Order, OrderLine), an SQL schema and 10 hand-written queries
- [ ] **J3:** a CRUD API on an in-memory store with validation, ProblemDetails, OpenAPI and a `.http` file
- [ ] **J4:** move to PostgreSQL + EF Core; async endpoints with cancellation; a typed client for a fake exchange-rates API; find and fix one N+1 query
- [ ] **J5:** unit + integration tests, JWT-protected admin endpoints, structured logs with `OrderId`
- [ ] **J6:** a Dockerfile, `docker compose up` runs everything, CI builds, tests and pushes the image; README with an architecture sketch

**Done when:** everything above works and every J quiz is ≥ 80%.
