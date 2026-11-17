[← Mid](../02-mid/README.md) · [Roadmap](../ROADMAP.md)

# 🔴 Senior: design, run and evolve distributed systems

~24 weeks · Project: **Shop v3**

## Topics

### S1 · Runtime and performance · 4 wk
- **S1.1** JIT, PGO & Native AOT ⭐: tiering, ReadyToRun vs AOT, trimming, `AssemblyLoadContext`
- **S1.2** GC deep dive: phases, pinning, container GC settings, finding leaks in dumps
- **S1.3** High-performance C# ⭐: `Span`/`Memory`, `ref struct`, `ArrayPool`, hidden allocations, source generators
- **S1.4** Benchmarking & profiling ⭐: BenchmarkDotNet, profilers, flame graphs, k6, finding the real bottleneck
- **S1.5** Memory model & concurrency: `volatile`, barriers, lock-free, `ConcurrentDictionary` traps, Dataflow

### S2 · Domain-Driven Design · 3 wk
- **S2.1** Strategic DDD ⭐: subdomains, ubiquitous language, bounded contexts, context maps, Event Storming
- **S2.2** Tactical DDD ⭐: entities, value objects, aggregates & invariants, domain vs integration events
- **S2.3** Event sourcing & CQRS: streams, projections, snapshots, versioning, Marten; when not to use it
- **S2.4** Architecture decisions ⭐: quality attributes, ADRs, C4 diagrams, fitness functions, tech debt

### S3 · Microservices · 6 wk
- **S3.1** Distributed fundamentals ⭐: fallacies, partial failure, CAP/PACELC, consistency models, distributed locks
- **S3.2** When to use microservices ⭐: costs vs benefits, Conway's law, the distributed monolith, Strangler Fig
- **S3.3** Communication & gateways: sync vs async, API gateway/BFF with YARP ⭐, composition, service discovery
- **S3.4** Data ownership ⭐: database per service, replicated read models, CDC (Debezium), cross-service queries
- **S3.5** Sagas ⭐: why not 2PC, choreography vs orchestration, compensations, state machines
- **S3.6** Event-driven & Kafka ⭐: partitions & ordering, consumer groups, offsets, KRaft, schema registry, Kafka vs RabbitMQ
- **S3.7** Contracts & versioning ⭐: tolerant reader, expand/contract, Pact, migrations safe for rolling deploys

### S4 · Cloud-native platform and Azure · 4 wk
- **S4.1** Kubernetes in production ⭐: HPA/KEDA, PDBs, affinity and topology spread, workload identity
- **S4.2** Cloud & cluster networking ⭐: VNets and subnets, private endpoints and private DNS, NSGs, L4 vs L7 load balancers, CNI, Gateway API, network policies, egress control
- **S4.3** Helm, GitOps & IaC: Helm vs Kustomize, Argo CD, Bicep vs Terraform ⭐, secrets
- **S4.4** Deployment strategies ⭐: rolling, blue-green, canary, feature flags, zero-downtime migrations
- **S4.5** Azure for microservices ⭐: App Service vs Container Apps vs AKS, Service Bus vs Event Hubs, Key Vault, APIM
- **S4.6** Service mesh & Dapr: mTLS, traffic splitting, Istio/Linkerd, when it's worth it

### S5 · Reliability, scale and security · 4 wk
- **S5.1** SLOs & error budgets ⭐: SLIs, burn-rate alerts, dashboards
- **S5.2** Incident response: triage with traces, debugging pods, blameless postmortems
- **S5.3** Scalability ⭐: stateless services, load-balancing algorithms, CDN and anycast, caching layers, connection limits, backpressure, sharding, consistent hashing
- **S5.4** Chaos & load testing: Polly chaos strategies, broker down, duplicate delivery, pod killed mid-saga, k6 soak
- **S5.5** Security engineering ⭐: zero trust, service-to-service auth, OWASP API Top 10, STRIDE, supply chain
- **S5.6** Disaster recovery: RPO/RTO ⭐, tested restores, multi-zone vs multi-region, failover

### S6 · System design and leadership · 3 wk
- **S6.1** System design method & building blocks ⭐: requirements, estimation, LB/CDN/cache/queue, ID generation
- **S6.2** Case studies: URL shortener, rate limiter, notifications, checkout, ticket booking, payments, chat
- **S6.3** Technical leadership ⭐: code review, mentoring, design docs, migrations, tech debt, disagreeing well

## Project: Shop v3

Split Shop v2 into microservices where it pays off. Do each task after its stage.

- [ ] **S1:** benchmark and optimize one hot path, set a k6 p99 target, try Native AOT on one service
- [ ] **S2:** a context map, an `Order` aggregate with invariants, C4 diagrams
- [ ] **S3:** extract Ordering, Payments, Inventory and Notifications (Catalog and Basket stay in the monolith); a YARP gateway; an orchestrated order saga with compensations; Kafka → an analytics projection; Pact tests
- [ ] **S4:** Helm + Argo CD on AKS (or Container Apps + Bicep), private networking, KEDA scaling on queue length, Key Vault with workload identity, a canary release
- [ ] **S5:** a checkout SLO with burn-rate alerts, 3 chaos scenarios written up, a threat model, a tested restore
- [ ] **S6:** ADRs for every major decision, final C4 diagrams, and a postmortem for one injected failure

**Done when:** everything above works, every S quiz is ≥ 80%, and you've passed 3 mock system design interviews.
