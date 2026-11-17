[← Junior](../01-junior/README.md) · [Roadmap](../ROADMAP.md) · [Senior →](../03-senior/README.md)

# 🟡 Mid: run a production service with reliable messaging

~27 weeks · Project: **Shop v2**

## Topics

### M1 · C# and the runtime in depth · 4 wk
- **M1.1** Generics in depth: variance ⭐, constraints, static abstract members, generic math
- **M1.2** Memory & GC ⭐: generations, LOH/POH, server GC/DATAS, `IDisposable` pattern, managed memory leaks
- **M1.3** Reflection & expression trees: `yield` state machines, `Func` vs `Expression<Func>`, attributes
- **M1.4** Modern C# 12–15: primary constructors, extension members; **(.NET 11)** unions, closed hierarchies
- **M1.5** Threading ⭐: thread pool starvation, deadlocks, `Lock`, `SemaphoreSlim`, `Interlocked`, concurrent collections
- **M1.6** Async internals ⭐: state machine, `ExecutionContext`, `ValueTask` rules, `TaskCompletionSource`, Channels

### M2 · Design and architecture · 3 wk
- **M2.1** SOLID, coupling & cohesion ⭐: violations and fixes, DIP vs DI, Law of Demeter
- **M2.2** Design patterns: Strategy, Decorator ⭐, Factory, Adapter, Mediator, Specification, Repository/UoW
- **M2.3** Clean / Hexagonal ⭐: the dependency rule, ports and adapters, costs and critiques
- **M2.4** Vertical slices, CQRS & Result ⭐: feature folders, handlers (MediatR is now paid), Result vs exceptions
- **M2.5** Modular monolith ⭐: modules with their own schema, contracts, in-process events, architecture tests

### M3 · Data in depth · 4 wk
- **M3.1** Advanced SQL: window functions ⭐, CTEs, upserts, `JSONB`, materialized views
- **M3.2** Isolation & locking ⭐: anomalies, isolation levels, MVCC, deadlocks, optimistic vs pessimistic
- **M3.3** Query performance ⭐: `EXPLAIN ANALYZE`, covering/partial indexes, SARGability, keyset pagination
- **M3.4** EF Core depth & Dapper ⭐: projections, split queries, `ExecuteUpdate`, pooling, filters, migrations in prod
- **M3.5** Caching ⭐: `HybridCache`, Redis, cache-aside vs write-through, invalidation, stampedes, output caching
- **M3.6** NoSQL & choosing a DB: document vs key-value vs search, embed vs reference, polyglot cost

### M4 · Production APIs, networking and identity · 5 wk
- **M4.1** ASP.NET Core in depth: `BackgroundService`, Options validation, keyed DI, filters, rate limiting ⭐
- **M4.2** API design & evolution ⭐: versioning, breaking changes, idempotency keys, cursors, `202` + status
- **M4.3** HTTP/2, HTTP/3 & sockets ⭐: multiplexing, head-of-line blocking, QUIC, keep-alive and connection pooling, `SocketsHttpHandler`, Kestrel limits, proxies, L4 vs L7 load balancers
- **M4.4** gRPC & real-time: Protobuf evolution, streaming, deadlines; SignalR vs WebSockets vs SSE
- **M4.5** OAuth 2 & OIDC ⭐: auth code + PKCE, client credentials, token types, scopes, BFF
- **M4.6** Keycloak, Entra ID & authorization: realms, app registrations, policy- and resource-based authZ ⭐
- **M4.7** Crypto & secrets: hashing vs encryption vs encoding ⭐, Data Protection, Key Vault, managed identity

### M5 · Messaging and resilience · 4 wk
- **M5.1** Messaging fundamentals ⭐: queue vs pub/sub, command vs event, delivery guarantees, why "exactly-once" is a myth
- **M5.2** RabbitMQ & libraries ⭐: exchanges, acks, prefetch, quorum queues, DLX; MassTransit v8 / Wolverine / Rebus
- **M5.3** Outbox & idempotency ⭐: the dual-write problem, transactional outbox, inbox/dedup tables
- **M5.4** Resilience ⭐: timeouts, retry + jitter, circuit breaker, bulkhead, Polly v8, retry storms
- **M5.5** Background jobs: scoped work in a `BackgroundService`, Hangfire/Quartz, idempotent jobs

### M6 · Testing, delivery and Kubernetes · 4 wk
- **M6.1** Testcontainers & test strategy ⭐: real DB/broker tests, Respawn, pyramid vs honeycomb, contract/snapshot tests
- **M6.2** CI/CD: build once, deploy many ⭐; environments, OIDC to the cloud, NuGet audit, Renovate
- **M6.3** Containers in depth: image size, scanning, SBOMs, digests, .NET memory/CPU limits
- **M6.4** Kubernetes basics ⭐: Pod, Deployment, Service, ConfigMap/Secret, probes, requests/limits, cluster networking (kube-proxy, CoreDNS), `kubectl`
- **M6.5** Aspire: AppHost, resources, service defaults, dashboard, what it doesn't replace

### M7 · Observability · 3 wk
- **M7.1** Logging at scale ⭐: `LoggerMessage` source generator, correlation, Seq/Loki, redaction, cost
- **M7.2** Metrics ⭐: `Meter`/`Histogram`, RED/USE, percentiles vs averages, Prometheus, Grafana
- **M7.3** Distributed tracing ⭐: `Activity`, W3C `traceparent`, propagation through RabbitMQ, OTLP, sampling
- **M7.4** Production diagnostics: `dotnet-counters`/`trace`/`dump`, high CPU, memory growth, starvation

## Project: Shop v2

Turn Shop v1 into a modular monolith. Do each task after its stage.

- [ ] **M1:** a bounded `Channel` for background emails; payment results as a union (or a closed hierarchy on .NET 10)
- [ ] **M2:** split into modules (Catalog, Basket, Ordering, Payments, Notifications), each with its own schema; an architecture test that guards the boundaries
- [ ] **M3:** keyset paging for the catalog, optimistic concurrency on stock, a Redis cache with invalidation, one query tuned with `EXPLAIN ANALYZE`
- [ ] **M4:** Keycloak login, a "customers see only their own orders" policy, an idempotent `POST /orders`, API versioning, rate limiting
- [ ] **M5:** `OrderPlaced` → outbox → RabbitMQ → Notifications with an inbox table and a DLQ; a resilient client for a flaky fake payment provider
- [ ] **M6:** Testcontainers suites, Aspire for local development, CI that scans the image and pushes it by digest, deploy to a local kind cluster
- [ ] **M7:** traces across HTTP + RabbitMQ, a RED dashboard in Grafana, 3 ADRs (modular monolith, broker, outbox)

**Done when:** everything above works and every M quiz is ≥ 80%.
