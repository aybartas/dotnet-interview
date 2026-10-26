# .NET Mülakat Hazırlık Roadmap'i — Junior → Mid → Senior

> Junior'dan Senior'a **sıralı** ilerleyen, her konusu ileride **kendi çalıştırılabilir kod projesine** dönüşecek şekilde gruplanmış .NET yol haritası.
> Konular "hangi seviyede sorulur" sorusuna göre yerleştirildi: önce Junior'ın bilmesi gerekenler, sonra aynı konuların Mid ve Senior derinlikleri.

**Hedef sürüm:** .NET 10 (LTS, Kasım 2025 → Kasım 2028) / C# 14
**Yaklaşan:** .NET 11 (STS, Kasım 2026, 24 ay destek) / C# 15 — önizleme özellikleri metinde **(.NET 11)** ile işaretli
**Güncelleme:** Ekim 2026

---

## İçindekiler

| # | Bölüm | İçerik |
|---|-------|--------|
| 1 | [Bu Roadmap Nasıl Kullanılır?](#bu-roadmap-nasıl-kullanılır) | Yapı, ID sistemi, konu klasörlerinin yeri ve şablonu |
| 2 | [Seviyeler ve Çıkış Kriterleri](#seviyeler-ve-çıkış-kriterleri) | Junior/Mid/Senior'dan ne beklenir |
| 3 | [Genel Harita](#genel-harita) | Tüm modüller tek bakışta |
| 4 | [Seviye 1 — Junior](#seviye-1--junior) | J1–J13 · temeller, ilk API, ilk veritabanı, ilk container |
| 5 | [Seviye 2 — Mid](#seviye-2--mid) | M1–M15 · derinlik, production, kimlik, NoSQL, AI uygulamaları |
| 6 | [Seviye 3 — Senior](#seviye-3--senior) | S1–S10 · runtime, performans, ölçek, dağıtık sistemler, mimari |
| 7 | [DSA Pisti — Veri Yapıları & Algoritmalar](#dsa-pisti--veri-yapıları--algoritmalar) | D1–D11 · 1. haftadan itibaren **paralel** ilerler |
| 8 | [Mülakat Hazırlığı](#mülakat-hazırlığı) | I1–I6 · coding, behavioral, system design, liderlik |
| 9 | [Sertifika Pisti — Azure Sertifikaları](#sertifika-pisti--azure-sertifikaları) | C1–C2 · **AZ-900** ve **AI-200**: teori + laboratuvar + sınav tarzı quiz, seviyelerden bağımsız |
| 10 | [Repo Yapısı](#repo-yapısı) | Klasör hiyerarşisi |
| 11 | [Çalışma Planı](#çalışma-planı) | Seviye bazlı haftalık plan |
| 12 | [2026 Radarı — Neler Değişti?](#2026-radarı--neler-değişti) | .NET, Azure, AI ve veri ekosistemindeki güncel değişiklikler |
| 13 | [Kaynaklar](#kaynaklar) | Kitaplar, dokümantasyon, pratik platformları |

---

## Bu Roadmap Nasıl Kullanılır?

### Yapı: Seviye → Modül → Konu

```
Seviye   (Junior / Mid / Senior / DSA / Mülakat / Sertifika)
  └── Modül   (ör. J8 Veritabanı Temelleri)        → bir klasör
        └── Konu   (ör. J8.4 Transaction & ACID)   → bir alt klasör = bir kod projesi
```

- **Seviyeler sıralıdır.** Junior modüllerini bitirmeden Mid'e geçme. Aynı konu (ör. veritabanı, async, güvenlik) her seviyede **daha derin bir kesitle** tekrar gelir — bu bilinçli bir tasarım.
- **DSA ayrı bir pisttir.** Seviyelerle **paralel** yürür: haftada 2-3 gün DSA, kalan günler seviye modülleri.
- **Sertifika pisti de ayrıdır.** AZ-900 (C1) ve AI-200 (C2) seviyelerin parçası değildir; ilgili modüller bittikten sonra, sınav odaklı (teori + laboratuvar + quiz) çalışılır.
- **Her seviye bir capstone projesiyle biter.** Capstone, o seviyedeki modülleri tek bir gerçekçi projede birleştirir ve mülakatta gösterilebilir bir portföy parçasıdır.

### ID sistemi

| Önek | Anlamı | Örnek |
|------|--------|-------|
| `J` | Junior modülü | J6.2 = Junior, 6. modül, 2. konu |
| `M` | Mid modülü | M9.3 = Keycloak |
| `S` | Senior modülü | S4.3 = Partitioning & Sharding |
| `D` | DSA pisti | D7 = Pattern kataloğu |
| `I` | Mülakat hazırlığı | I3 = System design interview |
| `C` | Sertifika pisti (Azure) | C1 = AZ-900, C2.4 = AI-200 · Cosmos DB for NoSQL |

Metin içi çapraz referanslar bu ID'lerle yapılır: **(→ M6.3)** "bu konunun derin hali M6.3'te" demektir.

### İşaretler

| İşaret | Anlamı |
|--------|--------|
| 🛠 **Kod** | Bu konunun klasöründe (`ConsoleApp` veya `WebApp` içinde) yazılacak çalıştırılabilir kodun kapsamı |
| 🎯 **Mülakat** | Modül sonunda cevaplayabilmen gereken tipik sorular |
| 📝 **Quiz / Sınav** | (Sertifika pistinde) sınav tarzı quiz ve modülün tipik sınav soruları |
| ⭐ | Mülakatlarda **çok sık** sorulan klasik konu |
| **(.NET 11)** | Kasım 2026'da gelen / önizlemedeki özellik — bil ama production'da .NET 10 varsay |
| 🟢 🟡 🔴 | (DSA pistinde) Junior / Mid / Senior zorluk seviyesi |

### Her konunun yeri: tek console + tek web uygulaması

**Repo iki projeden oluşur** (tek solution, `DotnetInterview.slnx`): **`ConsoleApp`** (konsol konularının hepsi) ve **`WebApp`** (HTTP sunucusu gerektiren konular: J1.3, J6, J7, M3, M5 …). Seviye, modül ve konu **klasördür**; modül başına proje, `src/`, `tests/`, `Directory.Build.props` yoktur. Aşağıdaki `📁` yolları **uygulama klasörüne göredir**: konsol konuları `ConsoleApp/` altında, web konuları `WebApp/` altında aynı yolla bulunur.

```
ConsoleApp/
├── ConsoleApp.csproj
├── Program.cs                 # menu for ALL console topics: dotnet run -- J3.2 [section | all]
└── <level>/<NN-module>/<NN-topic>/
    ├── README.md               # conceptual explanation (template below)
    ├── <code files>            # the topic's code (<Topic>Topic.cs, Sections/, Models/ …)
    └── interview-questions.md  # 10-20 questions + answers
WebApp/
├── WebApp.csproj
├── Program.cs                 # menu; runs ONE web topic at a time
└── <level>/<NN-module>/<NN-topic>/  # same shape: README.md + endpoint code + interview-questions.md
docker-compose.yml             # (added with the first infra topic — J8) DB, Redis, Keycloak, broker …
```

- **Konu ID'leri:** konsolda `dotnet run -- J3.2` (Junior), `M…`, `S…`, `D…`, `C1.8` (sertifika); `Program.cs`'e her konu tek satırla eklenir.
- **Test projesi yoktur**; yalnızca konusu test olan modüller (J10, M10…) için gerektiğinde eklenir. Diğer konularda doğrulama, kodun çıktısını okumakla yapılır.
- **Script yoktur** (`.sh` vb.): komut satırı alıştırmaları README'de kod bloğu olarak anlatılır; **ikinci assembly** gerektiren deneyler (ör. `internal` sınırı) de böyle: README'de `dotnet new classlib` alıştırması.
- Kod içermeyen modüller (ör. Kubernetes, CI/CD) klasör açmaz ya da yalnızca README'lerden oluşur.

Her konunun `README.md`'si şu şablonu izler:

```markdown
# <Konu>

## Ne? (tanım, zihinsel model)
## Neden? (hangi problemi çözüyor)
## Nasıl? (kod üzerinden — konu klasöründeki kodla birebir)
## Gerçek dünya senaryosu
## Mülakat perspektifi (nasıl sorulur, ne beklenir)
## Yaygın hatalar / tuzaklar
## Performans & trade-off notları
## Kaynaklar
```

### Nasıl çalışılır?

1. Konunun `README.md`'sini oku.
2. Konunun kodunu **kendin yaz** — kopyalama. Okumak öğrenmek değildir.
3. `interview-questions.md`'yi **kapalı kitap** cevapla, sonra kontrol et.
4. Haftada 1 gün tekrar, ayda 1 kez seviyene uygun mock mülakat (→ I6).

---

## Seviyeler ve Çıkış Kriterleri

| | 🟢 Junior (0-2 yıl) | 🟡 Mid (2-5 yıl) | 🔴 Senior (5+ yıl) |
|---|---|---|---|
| **Tek cümle** | Çalışan, test edilmiş bir CRUD API'yi tek başına yazar ve deploy eder | Production'da yaşayan servisleri tasarlar, güvenceye alır, ölçer, hata ayıklar | Trade-off'ları gerekçelendirir, sistem tasarlar, ekibe yön verir, production'ı yönetir |
| **C# & .NET** | Sözdizimi, tip sistemi, OOP, collections, LINQ, async kullanımı | Dil iç yapısı, generics, async internals, build/deployment modelleri | Runtime (JIT, GC, AOT), performans mühendisliği, bellek modeli |
| **Web/API** | REST temelleri, Minimal API/Controller, validation, OpenAPI | Versioning, gRPC/GraphQL/SignalR, resilience, rate limiting | Gateway/BFF, API evrimi, ölçekte API tasarımı |
| **Veri** | SQL, ilişkisel model, index, ACID, transaction temeli, EF Core CRUD, SQL vs NoSQL | PostgreSQL derinlemesine, isolation/locking, MongoDB, Redis, caching, EF performansı | Storage engine'ler, replication, sharding, CAP/PACELC, veritabanı seçimi |
| **Kimlik & güvenlik** | AuthN vs AuthZ, JWT, OAuth/OIDC kavramları, temel web açıkları | OAuth 2.1/OIDC akışları, Keycloak, policy-based authz, BFF | OWASP 2025, threat modeling, supply chain, zero trust, ölçekte yetkilendirme |
| **Mimari** | Katmanlı yapı, DI, temiz kod | SOLID, pattern'ler, Clean/Hexagonal, vertical slice, CQRS temeli, mesajlaşma | DDD, event sourcing, modular monolith, microservices, saga/outbox |
| **Ops & Cloud** | Docker, Compose, ilk CI (Azure/App Service → sertifika pisti C1) | CI/CD, Kubernetes temeli, Aspire, observability (Azure servisleri → sertifika pisti C2) | IaC, AKS, Azure mimarisi, SRE/SLO, production teşhisi |
| **AI** | LLM temelleri, AI asistanını doğru kullanma, ilk `IChatClient` | RAG, embeddings, MCP, Foundry, agentic coding | Agent Framework, multi-agent, eval, guardrail, LLMOps |
| **DSA** | Big-O, temel yapılar, kolay pattern'ler (LeetCode Easy) | Ağaç/graf, backtracking, heap, DP temeli (Medium) | DP ileri, ileri yapılar, sistem tasarımında DSA (Medium/Hard) |

> **Çıkış kriteri = capstone + mock mülakat.** Bir seviyeyi "bitti" saymak için o seviyenin capstone projesini tamamla ve I6'daki mock mülakat senaryosunu kapalı kitap geç.

---

## Genel Harita

### 🟢 Seviye 1 — Junior · `01-junior/`

| ID | Modül | Odak |
|----|-------|------|
| [J1](#j1-geliştirici-temelleri) | Geliştirici Temelleri | Git, **ağ temelleri (OSI, TCP/UDP)**, HTTP/DNS/TLS, .NET CLI & SDK, debugging |
| [J2](#j2-c-temelleri) | C# Temelleri | Program anatomisi, tip sistemi, **OOP ilkeleri & nesne ilişkileri**, **property/constructor/eşitlik/kopyalama**, exception, günlük modern C# |
| [J3](#j3-collections-delegates--linq) | Collections, Delegates & LINQ | Koleksiyonlar, generics, lambda, LINQ temel |
| [J4](#j4-async-programlamaya-giriş) | Async Programlamaya Giriş | Thread/Task kavramı, `async`/`await`, cancellation |
| [J5](#j5-build--run-temelleri) | Build & Run Temelleri | C# → IL → native, `bin`/`obj`, `run` vs `publish` |
| [J6](#j6-aspnet-core-temelleri) | ASP.NET Core Temelleri | Host, DI, configuration, middleware, logging |
| [J7](#j7-web-api-temelleri) | Web API Temelleri | REST, Minimal API, validation, DTO, OpenAPI, HttpClient |
| [J8](#j8-veritabanı-temelleri) | Veritabanı Temelleri | İlişkisel model, SQL, index, ACID, transaction, SQL vs NoSQL, PostgreSQL |
| [J9](#j9-ef-core-ile-veri-erişimi) | EF Core ile Veri Erişimi | ADO.NET, DbContext, migration, sorgulama, change tracking |
| [J10](#j10-test-temelleri) | Test Temelleri | xUnit v3, mocking, test edilebilir kod, ilk integration test, **ilk CI** |
| [J11](#j11-güvenlik--kimlik-temelleri) | Güvenlik & Kimlik Temelleri | AuthN/AuthZ, JWT, OAuth/OIDC'ye giriş, temel web açıkları |
| [J12](#j12-docker-temelleri) | Docker Temelleri | Container, Dockerfile, Compose |
| [J13](#j13-ai-ile-çalışmaya-giriş) | AI ile Çalışmaya Giriş | LLM temelleri, AI asistanı kullanımı, ilk `IChatClient` |
| [🏁](#junior-capstone-görev-yönetimi-apisi) | **Junior Capstone** | Görev yönetimi API'si — uçtan uca |

### 🟡 Seviye 2 — Mid · `02-mid/`

| ID | Modül | Odak |
|----|-------|------|
| [M1](#m1-c-derinlemesine) | C# Derinlemesine | C# 8→15, generics, expression tree, iterator, dispose |
| [M2](#m2-build-runtime--deployment) | Build, Runtime & Deployment | Roslyn, IL, MSBuild, apphost, deployment modelleri |
| [M3](#m3-aspnet-core-derinlemesine) | ASP.NET Core Derinlemesine | Host, Options, DI, middleware, filter, background service |
| [M4](#m4-concurrency--async-derinlemesine) | Concurrency & Async Derinlemesine | ThreadPool, TPL, async internals, senkronizasyon, Channels |
| [M5](#m5-api-tasarımı--protokoller) | API Tasarımı & Protokoller | REST ileri, versioning, gRPC, GraphQL, SignalR, resilience, **HTTP/2-3 & transport** |
| [M6](#m6-sql--postgresql-derinlemesine) | SQL & PostgreSQL Derinlemesine | İleri SQL, index/plan, isolation/locking, PostgreSQL, migration |
| [M7](#m7-ef-core-derinlemesine--veri-erişim-desenleri) | EF Core Derinlemesine & Veri Erişim Desenleri | EF performansı, ileri modelleme, Dapper, mapping |
| [M8](#m8-nosql--caching) | NoSQL & Caching | NoSQL modelleme, MongoDB, Redis, caching, arama |
| [M9](#m9-kimlik--erişim-yönetimi-oauth-oidc-keycloak) | Kimlik & Erişim Yönetimi | OAuth 2.1, OIDC, **Keycloak**, policy authz, BFF |
| [M10](#m10-test-stratejisi) | Test Stratejisi | Integration, Testcontainers, snapshot/contract, E2E |
| [M11](#m11-mimari--tasarım-temelleri) | Mimari & Tasarım Temelleri | SOLID, **coupling/cohesion**, pattern'ler, Clean/Hexagonal, vertical slice, CQRS |
| [M12](#m12-mesajlaşma--message-brokerlar) | Mesajlaşma & Message Broker'lar | RabbitMQ, Service Bus, Kafka'ya giriş, kütüphaneler |
| [M13](#m13-observability) | Observability | Structured logging, metrics, OpenTelemetry tracing |
| [M14](#m14-devops-cicd-container--kubernetes-temelleri) | DevOps | CI/CD, container ileri (BuildKit, sertleştirme), **Kubernetes temelleri + workload/storage**, Aspire |
| [M15](#m15-ai-engineering-uygulama-geliştirme) | AI Engineering: Uygulama Geliştirme | M.E.AI, RAG, vector search, **MCP**, Foundry |
| [🏁](#mid-capstone-e-ticaret-katalog--sipariş-platformu) | **Mid Capstone** | E-ticaret katalog & sipariş platformu |

### 🔴 Seviye 3 — Senior · `03-senior/`

| ID | Modül | Odak |
|----|-------|------|
| [S1](#s1-clr--runtime-derinlemesine) | CLR & Runtime Derinlemesine | JIT/PGO, assembly loading, GC, Native AOT, source generator |
| [S2](#s2-performans-mühendisliği) | Performans Mühendisliği | BenchmarkDotNet, Span, allocation, profiling, leak avı |
| [S3](#s3-concurrency-bellek-modeli--dağıtık-koordinasyon) | Concurrency: Bellek Modeli & Dağıtık Koordinasyon | Lock-free, pipelines, distributed lock, Orleans |
| [S4](#s4-ölçekte-veri) | Ölçekte Veri | Storage engine, replication, sharding, CAP, CDC, DB seçimi |
| [S5](#s5-mimari-derinlemesine-ddd-event-sourcing--modular-monolith) | Mimari Derinlemesine | DDD, event sourcing, modular monolith, mimari stiller, ADR |
| [S6](#s6-dağıtık-sistemler--microservices) | Dağıtık Sistemler & Microservices | EDA, outbox/saga, resilience, gateway, Kafka |
| [S7](#s7-güvenlik-mühendisliği) | Güvenlik Mühendisliği | OWASP 2025, kripto, supply chain, threat modeling |
| [S8](#s8-cloud-native-platform--sre) | Cloud-Native Platform & SRE | AKS, IaC, Azure mimarisi, SLO, production teşhisi |
| [S9](#s9-ai-sistemleri--agent-mimarisi) | AI Sistemleri & Agent Mimarisi | **Microsoft Agent Framework**, MCP/A2A, eval, LLMOps |
| [S10](#s10-sistem-tasarımı) | Sistem Tasarımı | Kapasite tahmini, yapı taşları, case study'ler |
| [🏁](#senior-capstone-dağıtık-sipariş-platformu--ai-destek-agentı) | **Senior Capstone** | Dağıtık sipariş platformu + AI destek agent'ı |

### 🧮 DSA Pisti · `04-dsa/` — paralel

| ID | Modül | Seviye |
|----|-------|--------|
| [D1](#d1-karmaşıklık-analizi) | Karmaşıklık Analizi | 🟢 |
| [D2](#d2-doğrusal-veri-yapıları-ve-hash-table) | Doğrusal Veri Yapıları ve Hash Table | 🟢 |
| [D3](#d3-ağaçlar-heap--trie) | Ağaçlar, Heap & Trie | 🟢→🟡 |
| [D4](#d4-graflar) | Graflar | 🟡 |
| [D5](#d5-sıralama--arama) | Sıralama & Arama | 🟢→🟡 |
| [D6](#d6-recursion--backtracking) | Recursion & Backtracking | 🟡 |
| [D7](#d7-pattern-kataloğu) | Pattern Kataloğu (18 pattern) | 🟡 |
| [D8](#d8-problemden-patterne-eşleştirme) | Problemden Pattern'e Eşleştirme | 🟡 |
| [D9](#d9-dinamik-programlama) | Dinamik Programlama | 🟡→🔴 |
| [D10](#d10-gelişmiş-veri-yapıları--sistem-tasarımında-dsa) | Gelişmiş Veri Yapıları & Sistem Tasarımında DSA | 🔴 |
| [D11](#d11-c-ile-pratik--problem-listeleri) | C# ile Pratik & Problem Listeleri | 🟢→🔴 |

### 🎯 Mülakat Hazırlığı · `05-interview/`

| ID | Modül |
|----|-------|
| [I1](#i1-coding-interview--live-coding) | Coding Interview & Live Coding |
| [I2](#i2-behavioral--star) | Behavioral / STAR |
| [I3](#i3-system-design-interview) | System Design Interview |
| [I4](#i4-take-home-ödevleri) | Take-home Ödevleri |
| [I5](#i5-senior-teknik-liderlik-soruları) | Senior: Teknik Liderlik Soruları |
| [I6](#i6-mock-mülakat-senaryoları--checklist) | Mock Mülakat Senaryoları & Checklist |

### 📜 Sertifika Pisti · `06-certifications/` — seviyelerden bağımsız

| ID | Modül | Sertifika · alanlar |
|----|-------|---------------------|
| [C1](#c1-microsoft-azure-fundamentals-az-900) | Microsoft Azure Fundamentals | **AZ-900** (July 20, 2026) · 22 konu (15 resmî hedef + deneme sınavı, 7 genişletilmiş): cloud computing & shared responsibility, faydalar & SLA, IaaS/PaaS/SaaS & serverless, temel mimari, VM'ler & scale set & availability set, App Service/container/AVD/Functions, ağ, storage & migration, Entra ID & RBAC, Zero Trust & Defender, maliyet, yönetişim, yönetim araçları & ARM, izleme + deneme sınavı + genişletilmiş kapsam: veri servisleri, yük dengeleme, ağ güvenliği & Key Vault, yedekleme & DR, AI/IoT, entegrasyon & DevOps, destek planları & SLA & CAF/WAF |
| [C2](#c2-azure-ai-cloud-developer-associate-ai-200) | Azure AI Cloud Developer Associate | **AI-200** (October 7, 2026) · 14 konu: ACR, Container Apps & KEDA, AKS; Cosmos DB (NoSQL, vector, change feed), pgvector, Managed Redis; Functions, Service Bus, Event Grid; Key Vault, App Configuration, OpenTelemetry & KQL + deneme sınavı |

---

## Seviye 1 — Junior

> **Hedef:** Çalışan, test edilmiş, veritabanına bağlı bir CRUD API'yi tek başına yazmak, container'a koymak ve buluta deploy etmek.
> **Süre:** ~7 ay (günde 2-3 saat, DSA dahil) · **Klasör:** `01-junior/`
> **Bu seviyede "neden"i bilmek yeterli; "nasıl çalışıyor"un derin kısmı Mid'de gelir.**

---

### J1. Geliştirici Temelleri

> 📁 `01-junior/01-dev-foundations/` · ⏱ ~2 hafta · Önkoşul: —
> Kod yazmadan önceki zemin: versiyon kontrolü, ağın ve web'in nasıl çalıştığı ve .NET araç zinciri.

#### J1.1 Git & Versiyon Kontrolü · `01-git`
- Commit, branch, `merge` vs `rebase` ⭐, fast-forward vs merge commit
- Merge conflict çözümü; `git stash`, `git cherry-pick`, `git bisect`
- `reset` (soft/mixed/hard) vs `revert` — paylaşılmış geçmişi neden yeniden yazmamalı
- Pull request akışı, code review kültürü, küçük PR prensibi
- Branching stratejileri: GitHub Flow, trunk-based development, Git Flow — hangisi ne zaman
- Conventional commits, semantic versioning (`MAJOR.MINOR.PATCH`)
- .NET için `.gitignore` (`bin/`, `obj/`, `*.user`, `.vs/`)
- 🛠 **Kod:** Script ile oluşturulan örnek repo üzerinde merge/rebase conflict senaryosu + `git bisect` ile hatayı getiren commit'i bulma alıştırması.

#### J1.2 Ağ Temelleri: OSI, TCP/UDP & Socket · `02-network-fundamentals` ⭐
> HTTP'nin altında ne olduğunu bilmeden "neden bu istek yavaş?" veya "socket exhaustion nedir?" sorularına cevap verilemez. Backend mülakatlarının klasik ısınma turu.

- **Katmanlı model:** OSI 7 katman (Physical → Data Link → Network → Transport → Session → Presentation → Application) ve pratikte kullanılan **TCP/IP 4 katman** modeliyle eşlemesi
  - Backend geliştiricinin gerçekten ilgilendiği katmanlar: **L3 (IP), L4 (TCP/UDP), L7 (HTTP)** — yedi katmanı ezberlemek değil, hangi sorunun hangi katmanda çözüldüğünü bilmek önemli
  - Encapsulation: her katmanın üst katmanın verisini kendi header'ıyla sarması
- **L3 — IP:** IPv4 vs IPv6, subnet/CIDR (`/24`), private IP aralıkları (`10.0.0.0/8`, `172.16.0.0/12`, `192.168.0.0/16`), loopback (`127.0.0.1`), NAT, routing ve `traceroute`'un ne gösterdiği
- **Port kavramı:** well-known portlar (80, 443, 5432, 6379, 5672), kayıtlı portlar, **ephemeral port aralığı**; bir portu neden iki process dinleyemez
- **L4 — TCP vs UDP** ⭐

  | | TCP | UDP |
  |---|-----|-----|
  | Bağlantı | Connection-oriented (handshake) | Connectionless |
  | Güvenilirlik | Garantili teslim, sıra korunur | Garanti yok, sıra yok |
  | Hız / overhead | Daha yavaş, header 20+ byte | Hızlı, header 8 byte |
  | Akış kontrolü | Flow + congestion control | Yok |
  | Kullanım | HTTP/1.1-2, SQL, SMTP, SSH | DNS, video/ses, oyun, **QUIC → HTTP/3** |

- **TCP three-way handshake** ⭐: `SYN` → `SYN-ACK` → `ACK`; bağlantı kurmanın **1 RTT** maliyeti (TLS ile toplam maliyet → J1.3)
- Bağlantı kapatma: `FIN`/`ACK` dörtlüsü, **`TIME_WAIT`** durumu ve neden ~2×MSL bekler
- TCP güvenilirlik mekanizmaları (kavramsal): sequence number, ACK, retransmission, **sliding window**, flow control (receive window), **congestion control** (slow start, congestion avoidance)
- Head-of-line blocking'in TCP seviyesindeki kökeni — HTTP/2 ve HTTP/3'ün neden farklı davrandığının temeli (→ M5.11)
- **Socket:** endpoint = IP + port + protokol; server akışı (`bind` → `listen` → `accept`) vs client akışı (`connect`)
  - .NET karşılıkları: `Socket`, `TcpListener`/`TcpClient`, `UdpClient`; `NetworkStream`
  - **Ephemeral port exhaustion** ⭐ — çok sayıda kısa ömürlü bağlantı + `TIME_WAIT` birikmesi; .NET'teki `HttpClient` socket exhaustion probleminin ağ katmanındaki sebebi budur (→ J7.5, M5.9)
  - Connection reuse / keep-alive neden bu kadar önemli
- Performans kavramları: **latency vs bandwidth vs throughput**, RTT, MTU ve paket parçalanması, bufferbloat (kısa not)
- **L4 vs L7 load balancer** — hangisi neyi görebilir (L4 IP/port, L7 header/path/cookie) (→ S6.5)
- Güvenlik duvarı, security group, port açma/kapama mantığı; "bağlantı neden timeout oluyor" teşhisi
- **Teşhis araç kutusu:** `ping`, `traceroute`/`tracert`, `nslookup`/`dig`, `netstat`/`ss -tanp`, `telnet`/`nc` ile port testi, `curl -v`, Wireshark / `tcpdump`
- 🛠 **Kod:** `TcpListener` ile satır tabanlı echo server + `TcpClient` istemcisi; aynı protokolün `UdpClient` versiyonu ve paket kaybı farkının gözlemi; yüzlerce kısa ömürlü bağlantı açan bir istemciyle `ss -tan | grep TIME_WAIT` sayacının artışını izleme; Wireshark/`tcpdump` ile three-way handshake'i yakalama.

#### J1.3 Web Nasıl Çalışır: HTTP, DNS, TLS · `03-how-the-web-works`
- "Tarayıcıya URL yazınca ne olur?" ⭐ — URL anatomisi → DNS çözümleme → TCP bağlantısı (→ J1.2) → TLS handshake → HTTP isteği → yanıt
- **HTTP anatomisi:** method, path, query, header, body, status code
- Method semantiği: safe (GET, HEAD) ve **idempotent** (GET, PUT, DELETE) metodlar ⭐
- Status code aileleri: 1xx, 2xx, 3xx (redirect), 4xx (istemci hatası), 5xx (sunucu hatası)
- Önemli header'lar: `Content-Type`, `Accept`, `Authorization`, `Cache-Control`, `ETag`, `Location`
- Cookie, session ve stateless HTTP
- HTTP/1.1 vs HTTP/2 (multiplexing) vs HTTP/3 (QUIC) — kavramsal fark
- TLS: sertifika, sertifika zinciri, HTTPS neden zorunlu, HSTS
- Same-origin policy ve **CORS** (preflight isteği)
- Proxy, reverse proxy, load balancer, CDN — kavram düzeyinde
- 🛠 **Kod:** Gelen ham isteği (method, header, body) loglayan küçük bir Kestrel uygulaması + `curl -v` ile TLS/header inceleme + `.http` dosyasıyla istek koleksiyonu.

#### J1.4 .NET Ekosistemi & CLI · `04-dotnet-ecosystem-cli`
- .NET Framework → .NET Core → birleşik .NET (5+) tarihçesi; .NET Standard'ın rolü ve neden geri plana düştüğü
- **SDK vs Runtime vs Target Framework Moniker** (`net10.0`, `net10.0-windows`) ⭐
- Sürüm politikası: her Kasım yeni sürüm; çift sayılar **LTS (3 yıl)**, tek sayılar **STS (24 ay)** — .NET 10 LTS, .NET 11 STS
- `dotnet new`, `build`, `run`, `test`, `add package`, `sln add`; template'ler (`webapi`, `console`, `classlib`, `xunit`)
- Solution (`.sln` ve yeni XML tabanlı `.slnx`) ve proje (`.csproj`) yapısı, proje referansı
- NuGet temelleri: paket kaynağı, versiyon, transitive bağımlılık
- `global.json` ile SDK sürümünü sabitleme
- **File-based apps (.NET 10):** `dotnet run app.cs` — `.csproj` olmadan tek dosya; `#:package`, `#:sdk Microsoft.NET.Sdk.Web`, `#:property` direktifleri; büyüyünce projeye dönüştürme
- 🛠 **Kod:** Aynı küçük uygulamayı (a) file-based app, (b) console projesi, (c) solution + class library + test projesi olarak üç farklı şekilde kur.

#### J1.5 IDE, Debugging & Kod Kalitesi Araçları · `05-ide-debugging`
- Visual Studio, JetBrains Rider, VS Code + C# Dev Kit — farkları
- Breakpoint, conditional breakpoint, tracepoint, watch, immediate window, call stack, step into/over/out
- Exception ayarları: "first-chance exception"da durmak
- `dotnet watch` ve Hot Reload
- Roslyn analyzer'ları, `.editorconfig`, `dotnet format`, `TreatWarningsAsErrors`
- Nullable uyarılarını okumak ve ciddiye almak
- 🛠 **Kod:** Bilerek hatalar yerleştirilmiş bir program — her hatayı debugger ile bulma alıştırması + örnek `.editorconfig` ve analyzer kuralları.

🎯 **Mülakat:** "Merge ile rebase farkı?" · "OSI modeli nedir, backend geliştirici hangi katmanlarla ilgilenir?" · "TCP ile UDP farkı, UDP ne zaman tercih edilir?" ⭐ · "Three-way handshake'i anlat" · "`TIME_WAIT` nedir, neden birikir?" · "Tarayıcıya URL yazınca neler olur?" · "SDK ile runtime farkı nedir?" · "LTS ile STS arasındaki fark?" · "Hangi HTTP metodları idempotent?"

---

### J2. C# Temelleri

> 📁 `01-junior/02-csharp-fundamentals/` · ⏱ ~3.5 hafta · Önkoşul: J1
> Mülakatın ilk teknik turu çoğunlukla buradan gelir. Yüzeysel tanım değil, **örnekle açıklama** beklenir.

#### J2.1 Program Anatomisi · `01-program-anatomy`
- **Entry point:** `Main`'in geçerli imzaları (`void`, `int`, `string[] args`, `async Task`) ve exit code'un anlamı
- **Top-level statements** — derleyici bunu gizli bir `Program` sınıfı + `Main`'e çevirir; `args` ve `return` nasıl çalışır
- Namespace, file-scoped namespace (`namespace X;`)
- `using` türleri: normal, `static`, alias (`using Json = System.Text.Json;`), **global using**, implicit usings
- **Erişim belirleyicileri — tam tablo** ⭐

  | Belirleyici | Aynı sınıf | Türeyen (aynı assembly) | Aynı assembly | Türeyen (farklı assembly) | Her yer |
  |-------------|:---------:|:----------------------:|:-------------:|:------------------------:|:-------:|
  | `private` | ✅ | ❌ | ❌ | ❌ | ❌ |
  | `private protected` | ✅ | ✅ | ❌ | ❌ | ❌ |
  | `protected` | ✅ | ✅ | ❌ | ✅ | ❌ |
  | `internal` | ✅ | ✅ | ✅ | ❌ | ❌ |
  | `protected internal` | ✅ | ✅ | ✅ | ✅ | ❌ |
  | `public` | ✅ | ✅ | ✅ | ✅ | ✅ |

  - Varsayılanlar: tip → `internal`, üye → `private`
  - `InternalsVisibleTo` ile test projesine `internal` açma
- `static class`, static üye, static constructor (ne zaman çalıştığının derin hali → S1.2)
- `partial` class/method — source generator'ların temel mekanizması
- Statement vs expression; expression-bodied members (`=>`)
- Değişken kapsamı (scope), gölgeleme, definite assignment
- Tip dönüşümleri: implicit, explicit cast, `is`, `as`, `Convert`, `Parse` vs `TryParse` ⭐
- `checked`/`unchecked` aritmetik, integer overflow (varsayılan **unchecked**)
- Operatörler: öncelik, short-circuit (`&&`/`||` vs `&`/`|`)
- `nameof`, `typeof`, `sizeof`, `default`
- Preprocessor: `#if DEBUG`, `#region`, `#nullable`, `#pragma warning`
- XML documentation comment (`///`) — IntelliSense ve OpenAPI'ye yansıması
- `[CallerMemberName]`, `[CallerArgumentExpression]` — derleme zamanında enjekte edilen bilgi
- 🛠 **Kod:** Her başlık için küçük, yorumlu örnekler içeren bir console uygulaması + iki projeli solution ile erişim belirleyicilerinin assembly sınırındaki davranışı.

#### J2.2 Tip Sistemi & Bellek Modeli · `02-type-system-memory` ⭐
> Mülakatın en klasik açılışı: "Value type ile reference type farkı nedir?" — ama "biri stack'te, biri heap'te" cevabı yetmez.

- **Unified type system:** her şey `System.Object`'ten türer; `int` = `System.Int32`
- **Value type vs reference type:**
  - Asıl fark **kopyalama semantiği** (değer kopyası vs referans kopyası)
  - **Yaygın yanılgı:** "value type her zaman stack'te" — sınıf alanı olan, closure'a yakalanan, boxing'e uğrayan, array elemanı olan value type **heap**'tedir
  - Eşitlik: `==` varsayılan davranışı (value: değer; reference: referans eşitliği)
- **`struct` vs `class` vs `record` vs `record struct`** — karar tablosu
  - Struct ne zaman: küçük, immutable, kısa ömürlü, değer semantiği isteniyor
  - Mutable struct neden tehlikeli (koleksiyonda güncelleme tuzağı)
- **Boxing / unboxing:** ne zaman gizlice olur (interface'e atama, `object` parametre, non-generic koleksiyon), maliyeti
- **`string`:**
  - Immutability ve sonuçları; döngüde birleştirmede `StringBuilder`
  - String interning; `==` string'de neden değer karşılaştırır (operator overload)
  - `StringComparison.Ordinal` vs kültür duyarlı karşılaştırma ("Türkçe I problemi" ⭐ — `"title".ToUpper()` Türk kültüründe `"TİTLE"` olur)
- **Nullable:**
  - Nullable value types (`int?` = `Nullable<int>`)
  - Nullable reference types (`string?`) — **sadece derleme zamanı** uyarısı, runtime'da iz yok
  - Null operatörleri: `?.`, `?[]`, `??`, `??=`
- `var` (derleme zamanı çıkarım) vs `dynamic` (runtime binding) vs `object`
- `const` (çağıran assembly'ye gömülür) vs `readonly` vs `static readonly`
- `ref`, `out`, `in` parametreleri
- Tuple (`ValueTuple`), deconstruction
- `enum`, `[Flags]`, `Enum.TryParse`
- 🛠 **Kod:** Value/reference kopyalama davranışını, boxing'i ve mutable struct tuzağını gösteren deneyler; `BenchmarkDotNet`'e geçmeden `Stopwatch` ile boxing maliyetinin kaba ölçümü.

#### J2.3 OOP Temelleri · `03-oop` ⭐
> Mülakatların en çok soru üretilen başlığı. "Dört ilkeyi say" değil, **kendi kodundan örnek verebilmek** beklenir.

- **Dört ilke — tanım değil, örnekle:**
  - **Encapsulation:** veriyi ve onu değiştiren davranışı birlikte tutmak, geçersiz duruma düşmeyi engellemek (uygulaması → J2.4 property)
  - **Abstraction:** "ne yaptığını" açığa çıkarıp "nasıl yaptığını" saklamak
  - **Inheritance:** "is-a" ilişkisi; ne zaman doğru araç, ne zaman tuzak
  - **Polymorphism:** aynı arayüzle farklı davranış
- **Polimorfizmin iki türü** ⭐
  - **Compile-time / static** (early binding): **overloading**, generic'ler, operator overloading
  - **Runtime / dynamic** (late binding): **overriding** — `virtual`/`abstract` + vtable üzerinden çözümleme
- `abstract class` vs `interface` — **karar tablosu** ⭐; C# 8+ default implementation sonrası fark ne kaldı (durum/alan tutabilme, tek kalıtım kısıtı, constructor, erişim belirleyicileri)
- `virtual`, `override`, `new` (**method hiding**), `sealed` — `new` ile `override` arasındaki davranış farkını base tipten çağırarak gösterme ⭐
- `abstract` üye: gövdesi yok, türetilmiş sınıf uygulamak zorunda; abstract sınıf **örneklenemez ama constructor'ı olabilir** (türetilmiş sınıf çağırır)
- Interface default implementation (C# 8+), **explicit interface implementation** (iki interface'in aynı imzalı üyesi — "diamond" benzeri çakışmanın çözümü)
- Upcasting / downcasting; `is`, `as`, pattern matching ile güvenli tip kontrolü
- **Nesne ilişkileri — ownership ve yaşam süresi** ⭐

  | İlişki | Anlam | Yaşam süresi | Örnek |
  |--------|-------|--------------|-------|
  | **Inheritance** | "is-a" | — | `SavingsAccount` : `Account` |
  | **Association** | "uses-a", sahiplik yok | Bağımsız | `Order` ↔ `Courier` |
  | **Aggregation** | zayıf "has-a" | Parça bütünden **uzun yaşayabilir** | `Team` ve `Employee` |
  | **Composition** | güçlü "has-a" | Parça bütünle **birlikte ölür** | `Order` ve `OrderLine` |

  - DDD'de aggregate sınırının bu ayrımdan beslenmesi (→ S5.1)
- **Composition over inheritance** ⭐ — neden (kırılgan taban sınıf / fragile base class problemi, derin hiyerarşinin bakım maliyeti, tek kalıtım kısıtı) ve nasıl refactor edilir
- **Coupling & cohesion** ⭐ — "gevşek bağlılık, yüksek uyum" ne demek, nasıl ölçülür (bir sınıfı değiştirince kaç yer etkileniyor?); `new` ile somut tipe bağlanmanın maliyeti ve interface ile gevşetilmesi
- **SOLID'e giriş** — beş prensibin adı ve bir cümlelik özeti; özellikle SRP ve DIP'i örnekleyebilmek (tamamı ve refactor hikâyeleri → M11.1)
- `struct`'ın OOP kısıtları: sınıftan **kalıtım alamaz** (örtük olarak `ValueType`'tan türer), interface uygulayabilir, `sealed` gibi davranır
- `static` sınıf ve üyeler ne zaman uygun — "her şeyi static yapma" eleştirisi ve test edilebilirliğe etkisi
- 🛠 **Kod:** Ödeme yöntemleri (kredi kartı, havale, cüzdan) üzerinden **inheritance → composition** refactor'u ve her adımda testlerin yeşil kalması; `new` ile `override` edilmiş iki metodun base referans üzerinden çağrıldığında farklı davrandığını gösteren demo; aynı domain'i (takım/çalışan, sipariş/sipariş kalemi) aggregation ve composition olarak modelleyip yaşam süresi farkını testle kanıtlama.

#### J2.4 Sınıf Üyeleri & Nesne Semantiği · `04-class-members` ⭐
> OOP ilkelerinin C#'taki somut karşılıkları: property, constructor, eşitlik, kopyalama. Mülakatın "ince detay" soruları buradan gelir.

- **Property vs field** ⭐ — en temel encapsulation aracı
  - Public field neden kullanılmaz: doğrulama yapılamaz, salt okunur yapılamaz, sonradan property'ye çevirmek **binary breaking change**'dir (kütüphane yazarken kritik)
  - Auto-property ve derleyicinin ürettiği backing field (lowering → M2.1)
  - `get`/`set`/**`init`**-only; **private setter**; salt okunur (`=> _x`) **computed property**
  - Property'de doğrulama ve invariant koruma; C# 14 **`field` keyword** ile backing field yazmadan doğrulama (→ M1.1)
  - **`required` members** ve object initializer ile zorunlu alan garantisi
  - Property vs metot: property ucuz ve yan etkisiz olmalı; pahalı iş yapan şey metot olmalı
  - Static property, `readonly` field ile `{ get; }` property farkı
- **Constructor çeşitleri ve başlatma sırası** ⭐
  - Default (derleyicinin ürettiği), parameterized, `static`, **`private`** (singleton / static factory method deseni), copy constructor
  - Constructor zinciri: `this(...)`, `base(...)`; primary constructor (→ J2.6)
  - **Başlatma sırası** ⭐ — klasik "ince" mülakat sorusu:
    1. Alanlar varsayılan değere (0/null) ayarlanır
    2. **Field initializer'lar en türetilmiş sınıftan tabana doğru** çalışır
    3. **Constructor gövdeleri tabandan türetilmişe doğru** çalışır
  - **Constructor'dan `virtual` metot çağırmanın tehlikesi** ⭐ (analyzer kuralı **CA2214**) — base constructor çalışırken türetilmiş sınıfın constructor gövdesi **henüz çalışmamıştır**; override edilen metot yarım kurulmuş nesneyi (varsayılan değerli alanları) görür
  - Static constructor ne zaman çalışır, thread-safety garantisi, exception fırlatırsa tipin kullanılamaz hale gelmesi
  - Object initializer ve `with` (→ J2.6) ile nesne kurma yolları
- **Eşitlik ve hash** ⭐
  - `Equals`, `GetHashCode`, `ToString` — **birlikte override kuralı**: eşit nesneler aynı hash'i vermek zorundadır
  - Referans eşitliği vs değer eşitliği; `==` ile `Equals` farkı ve `ReferenceEquals`
  - `IEquatable<T>` (boxing'siz eşitlik), `IComparable<T>`, `IComparer<T>`
  - **Mutable key tuzağı:** `Dictionary`/`HashSet`'e eklendikten sonra hash'i değişen nesne bulunamaz hale gelir (→ J3.1)
  - `record`'un bunları otomatik üretmesi (→ J2.6)
- **Extension method'lar** ⭐ — `static class` + `this` parametresi kuralı; çözümleme (resolution) sırası ve `using` gerekliliği; örnek metodun her zaman önce geldiği; ne zaman extension, ne zaman inheritance/wrapper; **LINQ'in tamamı extension method'dur** (C# 14 extension members → M1.1)
- **Operator overloading** — `+`, `==`/`!=` (ikisi **birlikte** tanımlanmak zorunda), `implicit`/`explicit` conversion operatörü; `==` tanımlandığında `Equals`/`GetHashCode` ile tutarlılık zorunluluğu
- **Indexer** (`public T this[int index]`) — koleksiyon benzeri API tasarımı, çok parametreli indexer
- **Shallow copy vs deep copy** ⭐
  - `object.MemberwiseClone()` **shallow**'dur: iç referans tipler paylaşılır → kopyayı değiştirmek orijinali etkiler
  - **`ICloneable` neden önerilmez:** `Clone()` `object` döner (cast gerekir) ve shallow mı deep mi olduğunu sözleşmede belirtmez — Microsoft public API'de kullanılmamasını önerir
  - Deep copy yolları: elle kopyalama (açık ve hızlı), copy constructor, JSON round-trip (kolay ama yavaş ve tip kısıtlı)
  - **`record` ile `with` de shallow'dur** — iç içe record'da referans paylaşılır; bilinmesi gereken tuzak
- Optional parametreler (varsayılan değerler) ve **named arguments**; optional parametre ile overload'ın karışması, varsayılan değerin **çağıran assembly'ye derlenmesi** (versiyonlama tuzağı — `const` ile aynı mantık)
- Anonim tipler ve ne zaman `record`/tuple tercih edilmeli
- Nested (iç içe) sınıflar — ne zaman anlamlı
- Object yaşam döngüsü ve `IDisposable`'a giriş (`using`) (dispose pattern → M1.5)
- 🛠 **Kod:** Public field'lı bir modeli property'ye çevirip doğrulama ekleyen refactor; **başlatma sırasını ekrana basan** base/derived sınıf çifti ve constructor'dan virtual metot çağrısının `null`/0 gördüğünü kanıtlayan demo (CA2214 uyarısıyla birlikte); `Equals`/`GetHashCode` yanlış override edilince `HashSet`'in bozulduğu ve mutable key'in kaybolduğu testler; iç içe nesne içeren sipariş modelinde shallow copy'nin orijinali bozduğunu, deep copy'nin bozmadığını kanıtlayan test; `Money` value object'inde operator overloading + indexer'lı küçük bir koleksiyon tipi.

#### J2.5 Exception Handling · `05-exception-handling`
- `try` / `catch` / `finally`, `using` / `await using`
- Exception hiyerarşisi; custom exception ne zaman gerekli
- Exception filter (`catch (X ex) when (...)`)
- **`throw` vs `throw ex`** — stack trace kaybı ⭐
- Exception maliyeti — control flow için kullanmama kuralı
- Hangi exception'ı yakalamalı, hangisini yakalamamalı (`catch (Exception)` ne zaman meşru)
- `Task` içindeki exception'lar ve `AggregateException`'a giriş
- Exception vs `Result` dönüşü — kavramsal giriş (→ M11.6)
- 🛠 **Kod:** `throw` vs `throw ex` stack trace karşılaştırması, exception filter örneği, custom domain exception hiyerarşisi.

#### J2.6 Günlük Modern C# · `06-everyday-modern-csharp`
> 2026'da junior bir geliştiricinin her gün yazdığı sözdizimi. Sürüm sürüm tarihçe ve ileri özellikler → M1.1.

- **Records:** `record`, `record struct`, positional records, `with` expression, value-based equality
- `init`-only setter, `required` members
- **Pattern matching:** type, constant, relational (`> 5`), logical (`and`/`or`/`not`), property pattern
- **`switch` expression**
- String interpolation, **raw string literals** (`"""`)
- **Collection expressions** (`[1, 2, 3]`, spread `..`)
- **Primary constructors** (class/struct/record)
- File-scoped namespace, global usings, target-typed `new()`
- `using` declaration (süslü parantezsiz `using var`)
- C# 14 ile gelen ve günlük kodda görülen: `field` keyword (backing field'sız property doğrulaması), null-conditional assignment (`customer?.Name = "x"`)
- 🛠 **Kod:** Aynı sipariş domain modelini "C# 7 tarzı" ve "C# 14 tarzı" iki versiyonda yaz; satır sayısını ve okunabilirliği karşılaştır.

🎯 **Mülakat:** "Value type ile reference type farkı?" · "`struct` ne zaman tercih edilir?" · "Boxing nedir, ne zaman olur?" · "String neden immutable?" · "OOP'un dört ilkesini kendi kodundan örnekle anlat" ⭐ · "`abstract class` vs `interface` — C# 8 default implementation'dan sonra fark ne kaldı?" ⭐ · "Compile-time ve runtime polimorfizm farkı?" · "`override` ile `new` (method hiding) farkı?" ⭐ · "Aggregation ile composition farkı?" · "Coupling ve cohesion nedir?" · "Neden public field yerine property kullanılır?" ⭐ · "Field initializer'lar ve constructor'lar hangi sırayla çalışır?" ⭐ · "Constructor'dan virtual metot çağırmak neden tehlikeli?" ⭐ · "`Equals` override edince neden `GetHashCode` de override edilmeli?" · "Shallow copy ile deep copy farkı, C#'ta nasıl yapılır?" ⭐ · "Extension method nasıl yazılır, ne zaman kullanılır?" · "`throw` vs `throw ex`?" · "`record` ile `class` farkı?"

---

### J3. Collections, Delegates & LINQ

> 📁 `01-junior/03-collections-linq/` · ⏱ ~2 hafta · Önkoşul: J2
> Veri yapılarının **teorisi ve Big-O'su DSA pistinde** (→ D2, D3); burada .NET API'lerinin doğru kullanımı.

#### J3.1 Collections & Generics · `01-collections-generics`
- `List<T>`, `Dictionary<TKey,TValue>`, `HashSet<T>`, `Queue<T>`, `Stack<T>`, `LinkedList<T>`
- `SortedDictionary`, `SortedSet`, `PriorityQueue<TElement,TPriority>`
- Arayüz hiyerarşisi: `IEnumerable<T>` → `ICollection<T>` → `IList<T>`; `IReadOnlyList<T>`, `IReadOnlyDictionary<,>` — public API'de neden read-only arayüz dönülür
- `Array` vs `List<T>`: kapasite büyümesi, `new List<T>(capacity)`
- `foreach` altında `IEnumerator<T>` (derin hali → M1.4)
- **Dictionary key sözleşmesi:** `GetHashCode`/`Equals` tutarlılığı, mutable key tuzağı ⭐
- Generic tip ve metod yazma; temel constraint'ler (`where T : class`, `struct`, `new()`, `notnull`, interface)
- Generics neden var: tip güvenliği + boxing'siz performans
- "Hangi koleksiyon ne zaman?" karar tablosu
- 🛠 **Kod:** Aynı veri setinde `List.Contains` vs `HashSet.Contains` vs `Dictionary` arama süresi karşılaştırması + bozuk `GetHashCode`'un `Dictionary`'yi nasıl bozduğunu gösteren demo + generic `Repository<T>` taslağı.

#### J3.2 Delegates, Lambdas & Events · `02-delegates-lambdas-events`
- `delegate`, `Func<>`, `Action<>`, `Predicate<>`, method group
- Lambda expression; statement vs expression lambda
- **Closure ve captured variable** — döngüde closure tuzağı ⭐
- `event` keyword — neden çıplak delegate değil; `EventHandler<TEventArgs>`
- Abone olma/abonelikten çıkma — unutulan aboneliğin memory leak'e yol açması (derin hali → M1.6)
- Local function vs lambda
- 🛠 **Kod:** Lambda ile strateji alan bir indirim motoru + event ile "sipariş oluşturuldu" bildirimi yayınlayan sınıf + closure tuzağı demosu.

#### J3.3 LINQ Temelleri · `03-linq-basics`
- Query syntax vs method syntax
- **Deferred vs immediate execution** ⭐ — en sık sorulan LINQ konusu
- Temel operatörler: `Where`, `Select`, `SelectMany`, `OrderBy`/`ThenBy`, `GroupBy`, `Join`, `Distinct`/`DistinctBy`
- Aggregate: `Count`, `Sum`, `Average`, `Min`/`Max`, `MinBy`/`MaxBy`, `Any`, `All`
- `Take`, `Skip`, `Chunk`, `Zip`, `ToDictionary`, `ToLookup`
- **`First` vs `FirstOrDefault` vs `Single` vs `SingleOrDefault`** ⭐
- `Any()` vs `Count() > 0`; **çoklu enumeration** problemi
- Yeni operatörler: `CountBy`, `AggregateBy`, `Index` (.NET 9); `LeftJoin`, `RightJoin` (.NET 10)
- `IEnumerable<T>` vs `IQueryable<T>`'ye giriş ⭐ (→ J9.4, M1.3)
- 🛠 **Kod:** Örnek e-ticaret sipariş verisi üzerinde 20 rapor alıştırması (en çok satan ürün, müşteri bazlı ciro...) + deferred execution tuzağını gösteren test.

🎯 **Mülakat:** "`List` ile `HashSet` farkı, ne zaman hangisi?" · "Closure nedir, döngüde neden sorun çıkarır?" · "Deferred execution nedir?" · "`First` ile `Single` farkı?" · "`IEnumerable` ile `IQueryable` farkı?"

---

### J4. Async Programlamaya Giriş

> 📁 `01-junior/04-async-basics/` · ⏱ ~1.5 hafta · Önkoşul: J2, J3
> Junior'dan beklenen: async kodu **doğru kullanmak**. İç yapısı (state machine, SynchronizationContext, ThreadPool) → M4.

#### J4.1 Temel Kavramlar · `01-concepts`
- Process vs thread vs task
- **Concurrency vs parallelism** ⭐ — aynı şey değil
- **CPU-bound vs I/O-bound** iş — hangisinde ne kullanılır
- Blocking vs non-blocking; neden bir web sunucusunda thread bloklamak pahalıdır
- Thread safety'ye giriş: paylaşılan state + race condition basit örneği
- 🛠 **Kod:** Aynı 10 HTTP çağrısını sıralı, `Task.WhenAll` ile ve `Parallel.For` ile yap; süreyi ve kullanılan thread sayısını karşılaştır. Paylaşılan sayaçta race condition demosu.

#### J4.2 `async`/`await` Kullanımı · `02-async-await`
- `Task` ve `Task<T>`; `async` metod imzası, `await`
- **Async all the way** — senkron/asenkron karıştırmama
- **`async void` neden kötü** (event handler hariç) ⭐
- **`.Result` / `.Wait()` neden tehlikeli** ⭐ (deadlock ve thread pool starvation → M4.3)
- `Task.WhenAll`, `Task.WhenAny`; `await` ile exception yakalama
- `Thread.Sleep` vs `await Task.Delay`
- `IAsyncEnumerable<T>` ve `await foreach`'e giriş
- `await using` ve `IAsyncDisposable`
- 🛠 **Kod:** Senkron yazılmış dosya okuma + HTTP çağırma yapan bir uygulamayı adım adım async'e dönüştürme; `async void` içindeki exception'ın uygulamayı nasıl çökerttiğini gösteren demo.

#### J4.3 Cancellation Temelleri · `03-cancellation-basics`
- `CancellationToken` / `CancellationTokenSource` modeli; iptal **kooperatiftir**
- Token'ı metod imzasında **son parametre** olarak geçirme konvansiyonu
- ASP.NET Core'da `HttpContext.RequestAborted` / endpoint parametresi olarak `CancellationToken`
- `CancelAfter` ile timeout
- `OperationCanceledException` yakalama
- EF Core ve `HttpClient` çağrılarına token geçirmeyi unutmama
- 🛠 **Kod:** Uzun süren bir rapor üretimini iptal edilebilir yapan API endpoint'i; istemci bağlantıyı kestiğinde işin durduğunu gösteren test.

🎯 **Mülakat:** "Concurrency ile parallelism farkı?" · "`async void` neden kullanılmamalı?" · "`.Result` kullanmanın riski ne?" · "`Task.WhenAll` içinde bir task hata verirse ne olur?" · "CPU-bound bir iş için `async` yeterli mi?"

---

### J5. Build & Run Temelleri

> 📁 `01-junior/05-build-run-basics/` · ⏱ ~1 hafta · Önkoşul: J1.4
> "Kod derlenince ne oluyor?" sorusunun junior cevabı. Roslyn, IL, apphost ve deployment modellerinin derin hali → M2.

#### J5.1 C# → IL → Native: Büyük Resim · `01-big-picture`
- Tek cümlelik özet:
  ```
  C# kaynak kod
    → [Roslyn derleyici] → IL + metadata içeren .dll (assembly)
    → [MSBuild] bin/ klasörüne .dll + .exe(apphost) + .deps.json + .runtimeconfig.json
    → [çalıştırma] apphost.exe → hostfxr → hostpolicy → CoreCLR
    → [JIT] IL'i metodun ilk çağrısında native makine koduna çevirir
    → CPU çalıştırır
  ```
- **"C# kodu nasıl .exe oluyor?"** ⭐ — olmuyor: senin kodun **her zaman `.dll`**; `.exe` o `.dll`'i başlatan küçük bir native launcher (apphost)
- IL nedir, neden var (dil ve platform bağımsızlığı)
- JIT nedir — bir cümleyle
- Debug vs Release farkı (optimizasyon, `DEBUG` sembolü)
- Compile-time error vs runtime exception
- 🛠 **Kod:** Küçük bir console uygulamasını build et → `.dll`'i ILSpy ile aç → bir metodun IL'ine bak → sharplab.io'da `foreach` ve `using`'in "lowered" halini incele.

#### J5.2 `bin/` ve `obj/` · `02-bin-obj`
- **`obj/` = ara çıktılar:** `project.assets.json` (restore sonucu), NuGet'in ürettiği props/targets, ara derleme çıktıları
- **`bin/` = son çıktı:**

  | Dosya | Ne işe yarar |
  |-------|--------------|
  | `MyApp.dll` | **Senin kodun** — IL + metadata |
  | `MyApp.exe` | apphost — `.dll`'i başlatan native launcher (Windows) |
  | `MyApp.pdb` | Debug sembolleri (satır numarası eşlemesi) |
  | `MyApp.deps.json` | Bağımlılık manifesti |
  | `MyApp.runtimeconfig.json` | Hangi runtime sürümü, roll-forward, GC ayarları |
  | `*.dll` (diğerleri) | NuGet bağımlılıkları |
  | `appsettings.json` | `CopyToOutputDirectory` ile kopyalanan dosyalar |

- Neden ikisi de `.gitignore`'da; `dotnet clean` ne zaman gerekir
- 🛠 **Kod:** Boş bir console app'i build et, `bin/Debug/net10.0/` içeriğini listele ve her dosyayı tabloyla eşleştir; `runtimeconfig.json`'ı açıp oku.

#### J5.3 `run` vs `build` vs `publish` · `03-run-build-publish` ⭐

| Komut | Ne yapar | Çıktı | Ne zaman |
|-------|----------|-------|----------|
| `dotnet restore` | Bağımlılıkları çözer/indirir | `obj/project.assets.json` | CI'da explicit adım |
| `dotnet build` | Derler (implicit restore) | `bin/<config>/<tfm>/` | Geliştirme, derleme doğrulama |
| `dotnet run` | Build eder **ve çalıştırır** | Çalışan process | **Sadece geliştirme** |
| `dotnet watch` | Değişiklikte yeniden çalıştırır / hot reload | — | Geliştirme döngüsü |
| `dotnet publish` | **Dağıtıma hazır** klasör üretir | `bin/<config>/<tfm>/publish/` | **Deployment** |
| `dotnet test` | Testleri derleyip çalıştırır | Test sonuçları | CI |
| `dotnet pack` | NuGet paketi üretir | `.nupkg` | Kütüphane yayını |

- `publish` varsayılan olarak **Release**'dir (.NET 8+); `build`/`run` varsayılan Debug
- **`dotnet run` neden production'da kullanılmaz:** SDK gerektirir, her çalıştırmada build kontrolü, kaynak kod ve build araçları sunucuda, büyük image
- `launchSettings.json` **sadece geliştirme** içindir, publish çıktısına girmez — production ayarları env var / appsettings'ten gelir
- Deployment modellerine (framework-dependent, self-contained, AOT) bir paragraflık giriş (→ M2.5)
- 🛠 **Kod:** Aynı projeyi `dotnet build` ve `dotnet publish` ile çıkar; iki klasörü karşılaştırıp farkları listele.

🎯 **Mülakat:** "C# kodu nasıl `.exe` oluyor?" · "IL nedir?" · "`bin` ve `obj` klasörlerinde ne var?" · "`dotnet run` ile `dotnet publish` farkı, production'a hangisiyle çıkılır?"

---

### J6. ASP.NET Core Temelleri

> 📁 `01-junior/06-aspnetcore-fundamentals/` · ⏱ ~2 hafta · Önkoşul: J2–J4
> "C# biliyorum ama ASP.NET Core'u bilmiyorum" boşluğunu kapatır. Her konunun derin hali → M3.

#### J6.1 `Program.cs` & Host · `01-program-host`
- `WebApplication.CreateBuilder(args)` → `builder.Services` / `builder.Configuration` / `builder.Logging` → `Build()` → pipeline tanımı → `Run()`
- Minimal hosting modeli; Kestrel web sunucusu
- **"Request geldiğinde ne oluyor?"** ⭐ — TCP → Kestrel → middleware pipeline → routing → endpoint → response
- `launchSettings.json`, port'lar, HTTPS geliştirme sertifikası (`dotnet dev-certs https`)
- 🛠 **Kod:** Boş bir web projesinin her satırı yorumlanmış `Program.cs`'i.

#### J6.2 Dependency Injection Temelleri · `02-di-basics` ⭐
- DI nedir, Inversion of Control ile ilişkisi; neden `new` ile bağımlılık oluşturmayız
- Kayıt: `AddTransient`, `AddScoped`, `AddSingleton`
- **Lifetime'lar:** Transient (her çözümlemede yeni), Scoped (request başına bir), Singleton (uygulama boyunca bir) ⭐
- Constructor injection; `IEnumerable<IService>` ile çoklu implementasyon
- **Captive dependency'ye giriş:** Singleton içine Scoped enjekte etmek neden hatalı ⭐ (Development'ta `ValidateScopes` hatayı yakalar)
- 🛠 **Kod:** Üç lifetime'ı GUID ile gösteren endpoint — her request'te hangi ID'nin değiştiği; captive dependency hatasını tetikleyen örnek.

#### J6.3 Configuration & Options Temelleri · `03-configuration-basics`
- Kaynaklar ve öncelik sırası (**sonra gelen kazanır**): `appsettings.json` → `appsettings.{Environment}.json` → User Secrets (Development) → environment variables → command-line
- Hiyerarşik key'ler ve env var karşılığı: `Logging:LogLevel:Default` → `Logging__LogLevel__Default`
- `IConfiguration`, `GetValue<T>`, `GetSection`
- **Options pattern'e giriş:** `IOptions<T>` + `builder.Services.Configure<T>(...)` / `AddOptions<T>().Bind(...)`
- **Secret'lar asla `appsettings.json`'a ve git'e girmez** ⭐ — User Secrets (dev), env var / Key Vault (prod)
- 🛠 **Kod:** Aynı ayarı dört kaynaktan verip hangisinin kazandığını gösteren endpoint + strongly-typed `SmtpOptions`.

#### J6.4 Middleware Pipeline Temelleri · `04-middleware-basics` ⭐
- Pipeline zihinsel modeli: iç içe halkalar — request içeri, response dışarı
- `Use`, `Run`, `Map` farkı; `next()` çağrılmazsa ne olur (short-circuit)
- **Sıralama neden kritik:** exception handler en başta, `UseAuthentication` → `UseAuthorization` sırası
- Yerleşik middleware'ler: exception handler, HTTPS redirection, static files, routing, CORS, authentication, authorization
- Basit custom middleware yazma
- 🛠 **Kod:** Request süresini ölçüp header'a yazan + correlation ID ekleyen iki custom middleware; sıralama değişince davranışın nasıl değiştiğini gösteren deney.

#### J6.5 Logging Temelleri · `05-logging-basics`
- `ILogger<T>` ve kategori
- Log seviyeleri: Trace, Debug, Information, Warning, Error, Critical — hangisi ne zaman
- **Structured logging** — interpolation neden yanlış ⭐
  ```csharp
  logger.LogInformation("User {UserId} logged in", userId);   // ✅ sorgulanabilir alan
  logger.LogInformation($"User {userId} logged in");          // ❌ düz metin
  ```
- `appsettings.json` üzerinden seviye filtreleme
- Şifre, token, kişisel veri (PII) **loglanmaz** (KVKK/GDPR)
- 🛠 **Kod:** Farklı seviyelerde log yazan servis + ortam bazlı seviye değiştirme + JSON console formatter.

#### J6.6 Ortamlar & Hata Sayfaları · `06-environments`
- `ASPNETCORE_ENVIRONMENT` / `DOTNET_ENVIRONMENT`; Development / Staging / Production
- `app.Environment.IsDevelopment()` ile ortama göre davranış
- Developer exception page vs `UseExceptionHandler`; production'da stack trace sızdırmamak
- `ProblemDetails`'e giriş (→ J7.3)
- 🛠 **Kod:** Aynı hatanın Development ve Production'da farklı görünen çıktısı.

🎯 **Mülakat:** "Request geldiğinde ASP.NET Core'da ne olur?" · "Transient/Scoped/Singleton farkı?" · "Singleton içine Scoped enjekte edersen ne olur?" · "Configuration kaynaklarının öncelik sırası?" · "Middleware sıralaması neden önemli?" · "Structured logging nedir?"

---

### J7. Web API Temelleri

> 📁 `01-junior/07-web-api-basics/` · ⏱ ~2.5 hafta · Önkoşul: J6
> .NET backend mülakatlarının merkezi. İleri API tasarımı (versioning, gRPC, GraphQL, resilience) → M5.

#### J7.1 REST Tasarım Temelleri · `01-rest-basics`
- Kaynak (resource) odaklı tasarım: `/api/orders/{id}/items` — çoğul isim, URL'de fiil yok
- HTTP method semantiği: GET, POST, PUT (tam değiştirme), PATCH (kısmi), DELETE
- **Doğru status code** ⭐
  - `200 OK`, `201 Created` (+ `Location` header), `204 No Content`
  - `400 Bad Request`, `404 Not Found`, `409 Conflict`, `422 Unprocessable Entity`
  - **`401 Unauthorized` vs `403 Forbidden`** ⭐ — en sık karıştırılan
  - `500` (bizim hatamız) vs `503` (geçici olarak kullanılamıyor)
- Hata gövdesi standardı: **`ProblemDetails` (RFC 9457)**
- Basit pagination (`?page=2&pageSize=20`), filtreleme ve sıralama query parametreleri
- 🛠 **Kod:** Bir kütüphane API'si için kaynak modeli, URL'ler ve status code'ları belgeleyen tasarım dokümanı + `.http` dosyası.

#### J7.2 Minimal API vs Controller · `02-minimal-api-controllers`
- İki yaklaşımın sözdizimi ve ne zaman hangisi
- Routing: route template, route parametresi, constraint (`{id:int}`)
- Model binding kaynakları: `[FromRoute]`, `[FromQuery]`, `[FromBody]`, `[FromHeader]`, `[FromServices]`
- `[ApiController]` attribute'unun getirdikleri (otomatik 400, binding inference)
- Minimal API'de `MapGroup` ile gruplama, `TypedResults` ve `Results<Ok<T>, NotFound>`
- `IActionResult` vs `ActionResult<T>`
- 🛠 **Kod:** Aynı "Todo" API'sini Minimal API ve Controller ile ayrı ayrı yaz; endpoint'leri ve testleri birebir eşleştir.

#### J7.3 Validation & Hata Yönetimi · `03-validation-errors`
- `DataAnnotations` (`[Required]`, `[Range]`, `[EmailAddress]`, `[StringLength]`)
- **.NET 10 Minimal API yerleşik validation** (`builder.Services.AddValidation()`)
- **FluentValidation**'a giriş — karmaşık kurallar için
- Input validation vs iş kuralı (domain) validation ayrımı
- **Global hata yönetimi:** `AddProblemDetails()` + `IExceptionHandler` (.NET 8+)
- Hata yanıtında stack trace / connection string sızdırmamak
- 🛠 **Kod:** Validation hatası → 400 `ValidationProblemDetails`, bulunamadı → 404, beklenmeyen hata → 500 `ProblemDetails` dönen API + testleri.

#### J7.4 DTO & Mapping · `04-dto-mapping`
- Entity'yi doğrudan API'den dönmemek: **over-posting / mass assignment** ⭐, API kontratının kararlılığı, döngüsel referans
- Request DTO, response DTO; DTO olarak `record` kullanımı
- Manuel mapping (extension method) — varsayılan tercih
- Mapping kütüphaneleri (Mapperly, AutoMapper) ve lisans durumu → M7.4
- 🛠 **Kod:** Entity'yi doğrudan bind eden "açık" endpoint ile `IsAdmin` alanının saldırgan tarafından set edildiği demo → DTO ile düzeltilmiş versiyon.

#### J7.5 OpenAPI & API Test Araçları · `05-openapi`
- OpenAPI (Swagger) spesifikasyonu ne işe yarar
- **Yerleşik `Microsoft.AspNetCore.OpenApi`** — .NET 10'da varsayılan **OpenAPI 3.1**, YAML çıktısı
- UI: **Scalar** (Swashbuckle .NET 9'dan itibaren şablonlarda yok)
- XML comment'lerin dokümana yansıması, endpoint açıklamaları (`WithSummary`, `WithDescription`)
- `.http` dosyaları (VS/Rider/VS Code), Postman, Bruno
- 🛠 **Kod:** J7.2'deki API'ye OpenAPI + Scalar ekle; tüm endpoint'ler için `.http` koleksiyonu yaz.

#### J7.6 HttpClient ile Dış Servis Çağırma · `06-httpclient-basics`
- **`new HttpClient()` + `using` neden felaket** — socket exhaustion ⭐
- `IHttpClientFactory`: named client, **typed client**
- `GetFromJsonAsync`, `PostAsJsonAsync`, `EnsureSuccessStatusCode`
- Timeout ayarı, hata durumunda anlamlı exception
- Retry/circuit breaker gibi resilience konuları → M5.9
- 🛠 **Kod:** Ücretsiz bir döviz/hava durumu API'sini typed client ile çağıran endpoint + `DelegatingHandler` ile istek loglama.

🎯 **Mülakat:** "401 ile 403 farkı?" · "PUT ile PATCH farkı?" · "POST idempotent mi, nasıl yapılır?" · "Entity'yi doğrudan dönmenin riski?" · "Minimal API mı Controller mı?" · "`HttpClient` neden `using` ile oluşturulmamalı?"

---

### J8. Veritabanı Temelleri

> 📁 `01-junior/08-database-fundamentals/` · ⏱ ~3 hafta · Önkoşul: J1
> Altyapı (repo köküne eklenecek `docker-compose.yml` servisleri): PostgreSQL + pgAdmin + MongoDB + Redis.
> Veritabanı soruları **her seviyede** gelir: Junior'da kavram (ACID, index, JOIN), Mid'de mekanizma (isolation, plan, MVCC), Senior'da ölçek (replication, sharding). Bu modül o zeminin tamamı.

#### J8.1 Veri Modelleme & İlişkisel Model · `01-relational-modeling`
- Temel kavramlar: veritabanı, DBMS, tablo, satır, kolon, şema
- **ER modelleme:** entity, attribute, relationship; ER diyagramı çizme
- Kardinalite: 1-1, 1-N, N-N (ara tablo / junction table)
- **Key'ler:** primary key, foreign key, unique key, composite key, candidate key
- **Surrogate vs natural key**; `int`/`bigint` identity vs GUID vs **UUIDv7** (zaman sıralı → index dostu; PostgreSQL 18 `uuidv7()`, .NET 9+ `Guid.CreateVersion7()`)
- Constraint'ler: `NOT NULL`, `CHECK`, `DEFAULT`, `UNIQUE`, `FOREIGN KEY` + `ON DELETE CASCADE / RESTRICT / SET NULL`
- **Normalizasyon:** 1NF, 2NF, 3NF (BCNF'ye kısa bakış) — her biri **bozuk tablo örneğiyle** ⭐
- Denormalizasyon ne zaman bilinçli bir tercih (okuma performansı, raporlama, snapshot veri — "siparişteki fiyat")
- Veri tipi seçimi: `varchar` vs `text`; **para için `decimal`/`numeric`, asla `float`** ⭐; `timestamp` vs `timestamptz` (zaman dilimi tuzağı)
- 🛠 **Kod:** E-ticaret şeması (müşteri, ürün, kategori, sipariş, sipariş kalemi) — normalize edilmemiş halinden 3NF'ye adım adım SQL migration script'leri + Mermaid ER diyagramı.

#### J8.2 SQL Temelleri · `02-sql-basics`
- SQL alt dilleri: DDL (`CREATE`/`ALTER`/`DROP`), DML (`INSERT`/`UPDATE`/`DELETE`), DQL (`SELECT`), DCL (`GRANT`), TCL (`COMMIT`/`ROLLBACK`)
- **`SELECT`'in mantıksal çalışma sırası** ⭐: `FROM` → `WHERE` → `GROUP BY` → `HAVING` → `SELECT` → `ORDER BY` → `LIMIT` (bu yüzden `WHERE`'de `SELECT` alias'ı kullanılamaz)
- Filtreleme: karşılaştırma, `IN`, `BETWEEN`, `LIKE`/`ILIKE`, `IS NULL`
- **NULL semantiği:** üç değerli mantık (true/false/unknown), `NULL = NULL` neden true değil, `COUNT(*)` vs `COUNT(kolon)`, `COALESCE`
- **JOIN tipleri** ⭐: `INNER`, `LEFT`, `RIGHT`, `FULL OUTER`, `CROSS`, self join — satır örnekleriyle
- Aggregate: `COUNT`, `SUM`, `AVG`, `MIN`, `MAX`; **`WHERE` vs `HAVING`** ⭐
- Subquery (scalar, `IN`, `EXISTS`), correlated subquery; `EXISTS` vs `IN`
- `UNION` vs `UNION ALL`
- CTE (`WITH`) ile okunabilir sorgu
- Window function'lara giriş: `ROW_NUMBER()`, `RANK()`, `SUM() OVER (PARTITION BY ...)` (derin hali → M6.1)
- **`DELETE` vs `TRUNCATE` vs `DROP`** ⭐

  | | `DELETE` | `TRUNCATE` | `DROP` |
  |---|---------|-----------|-------|
  | Türü | DML | DDL | DDL |
  | `WHERE` ile filtre | ✅ | ❌ (tamamı) | ❌ |
  | Tablo yapısı kalır | ✅ | ✅ | ❌ (tablo yok olur) |
  | Satır tetikleyicileri (trigger) | çalışır | **çalışmaz** ⭐ | — |
  | Identity/sequence sıfırlanır | hayır | genelde evet | — |
  | Hız (büyük tabloda) | yavaş (satır satır loglar) | çok hızlı | çok hızlı |

  - **Rollback motora bağlıdır** ⭐: PostgreSQL'de `TRUNCATE` transaction'a dahildir (`BEGIN; TRUNCATE; ROLLBACK;` veriyi geri getirir); MySQL'de DDL implicit commit yapar — "TRUNCATE geri alınamaz" ezberi yanlıştır, "hangi veritabanı?" diye sorulmalı
  - Başka tablo FK ile referans veriyorsa `TRUNCATE` reddedilir (referans veren tablolar boş olsa bile)
- **View, stored procedure, function, trigger** — kavramsal giriş (derin hali ve trade-off'lar → M6.1)
  - View: sorguya isim vermek; materialized view ile farkı
  - **Stored procedure vs function:** procedure çıkış parametresi alabilir ve DML yapabilir; function genelde değer döndürmeye odaklıdır ve sorgu içinde kullanılabilir (kesin kurallar veritabanına göre değişir)
  - **Trigger:** satır değiştiğinde otomatik çalışan kod — audit/denormalizasyon için kullanılır ama **gizli yan etki** yarattığı için tartışmalıdır
  - "İş mantığı veritabanında mı uygulamada mı?" tartışmasına giriş
- Klasik mülakat sorguları: "N'inci en yüksek maaş", "duplicate kayıtları bul ve sil", "hiç sipariş vermemiş müşteriler", "her kategorinin en pahalı ürünü"
- 🛠 **Kod:** J8.1 şeması + seed data üzerinde kolaydan zora 40 SQL alıştırması ve cevap anahtarı; Npgsql ile alıştırmaları çalıştırıp sonucu doğrulayan küçük bir .NET test runner'ı.

#### J8.3 Index Temelleri · `03-index-basics` ⭐
- Index nedir: kitabın sonundaki dizin benzetmesi; index yoksa **full table scan**
- **B-tree**'nin sezgisel yapısı: sıralı, dengeli ağaç → O(log n) arama (veri yapısı → D3; storage engine detayı → S4.1)
- **Clustered vs non-clustered index** — SQL Server terminolojisi; PostgreSQL'de tablo heap'tir, tüm index'ler ayrıdır
- Primary key ve unique constraint otomatik index oluşturur; **foreign key oluşturmaz** (PostgreSQL) ⭐ — JOIN'ler için FK kolonuna index ekle
- **Composite index ve kolon sırası** — leftmost prefix kuralı ⭐
- Index'in maliyeti: yazma yavaşlar, disk/bellek tüketir — "her kolona index" neden yanlış
- **Index ne zaman kullanılmaz:** düşük seçicilik, kolona fonksiyon uygulanması (`WHERE LOWER(email) = ...`), baştan wildcard (`LIKE '%abc'`), tip uyuşmazlığı
- `EXPLAIN` / `EXPLAIN ANALYZE` okumaya giriş: `Seq Scan` vs `Index Scan` vs `Index Only Scan`
- 🛠 **Kod:** 1 milyon satırlık tablo üreten script; aynı sorguyu index'siz ve index'li çalıştırıp `EXPLAIN ANALYZE` ve süre karşılaştırması; composite index'te kolon sırası deneyi.

#### J8.4 Transaction & ACID · `04-transactions-acid` ⭐
- Transaction: "ya hep ya hiç" iş birimi; `BEGIN` / `COMMIT` / `ROLLBACK`; autocommit
- **ACID — her harf örnekle** ⭐
  - **Atomicity:** para transferinde borç düşüldü ama alacak eklenmedi → olamaz
  - **Consistency:** constraint'ler ve iş kuralları transaction sonunda geçerlidir
  - **Isolation:** eşzamanlı transaction'lar birbirinin yarım işini görmez (ne kadar görmeyeceği isolation level'a bağlı)
  - **Durability:** commit edilen veri çökme sonrasında da kalıcıdır (WAL — write-ahead log fikri)
- **Eşzamanlılık anomalileri:** dirty read, non-repeatable read, phantom read, lost update ⭐
- **Isolation level'lara giriş:** Read Uncommitted, **Read Committed** (PostgreSQL ve SQL Server varsayılanı), Repeatable Read, Serializable — hangisi hangi anomaliyi engeller (tam tablo ve MVCC → M6.3)
- Lock kavramı: shared vs exclusive; deadlock'un basit örneği
- **Optimistic vs pessimistic concurrency**'ye giriş: version kolonu vs `SELECT ... FOR UPDATE`
- Savepoint
- .NET'te transaction: ADO.NET `DbTransaction`; EF Core'da `SaveChanges`'in kendisi bir transaction'dır; `Database.BeginTransactionAsync`
- **Transaction'ı kısa tut:** içinde HTTP çağrısı, kullanıcı beklemesi, uzun hesaplama yapma
- 🛠 **Kod:** Banka transferi senaryosu — transaction'sız (bozuk veri), transaction'lı (doğru) ve iki eşzamanlı istekle **lost update** demosu; iki ayrı `psql` oturumunda isolation level deneyleri için adım adım script.

#### J8.5 İlişkisel vs İlişkisel Olmayan (SQL vs NoSQL) · `05-sql-vs-nosql` ⭐
- İlişkisel modelin güçlü yanları: şema, ilişki bütünlüğü, JOIN, ACID, olgun araç ekosistemi
- NoSQL neden ortaya çıktı: yatay ölçeklenme, esnek şema, belirli erişim desenleri için optimizasyon
- **NoSQL türleri:**

  | Tür | Örnek | Ne için iyi |
  |-----|-------|-------------|
  | Document | MongoDB, Cosmos DB | Esnek şemalı nesneler: katalog, içerik, profil |
  | Key-Value | Redis, Valkey, DynamoDB | Cache, session, sayaç, rate limit |
  | Wide-column | Cassandra, ScyllaDB | Çok yüksek yazma hacmi, zaman bazlı veri |
  | Graph | Neo4j, Cosmos DB (Gremlin) | İlişki ağırlıklı sorgu: sosyal ağ, öneri, dolandırıcılık tespiti |
  | Time-series | InfluxDB, TimescaleDB | Metrik, IoT sensör verisi |
  | Search | Elasticsearch, OpenSearch | Full-text arama, log analizi |
  | Vector | pgvector, Qdrant, Azure AI Search | Embedding benzerlik araması (AI/RAG) |

- **Schema-on-write vs schema-on-read**
- **ACID vs BASE** (Basically Available, Soft state, Eventually consistent)
- **CAP teoremine giriş** — ağ bölünmesinde (partition) tutarlılık mı erişilebilirlik mi (PACELC ve derin hali → S4.4)
- Ölçekleme: vertical vs horizontal; replication ve sharding'e kavramsal giriş (→ S4.2, S4.3)
- **"Hangi veritabanı?" karar soruları:** veri yapısı, erişim deseni, tutarlılık ihtiyacı, ölçek, ekip yetkinliği, maliyet
- **Polyglot persistence** — tek sistemde birden fazla veri deposu
- Yaygın yanılgılar: "NoSQL = şemasız", "MongoDB transaction desteklemez" (multi-document transaction var), "PostgreSQL ölçeklenmez"
- Sınırların bulanıklaşması: PostgreSQL JSONB ile doküman saklama, MongoDB'de ACID transaction, Redis'te vector search
- 🛠 **Kod:** Aynı "ürün kataloğu" verisini (1) normalize PostgreSQL, (2) PostgreSQL JSONB ve (3) MongoDB'de modelleyip aynı 5 sorguyu üçünde de yazan karşılaştırma projesi + Redis'te ürün cache'i — hepsi tek `docker compose up` ile.

#### J8.6 PostgreSQL ile Başlangıç · `06-postgresql-getting-started`
- Neden PostgreSQL: açık kaynak, standartlara uyum, zengin veri tipleri, eklenti ekosistemi; .NET dünyasında yükselen varsayılan
- Docker ile kurulum; `psql` temel komutları (`\l`, `\c`, `\dt`, `\d tablo`, `\x`); pgAdmin / DBeaver
- Database, schema (`public`), role/user, `GRANT`
- Önemli veri tipleri: `bigint`, `numeric`, `text`, `boolean`, `timestamptz`, `uuid`, `jsonb`, array, enum
- `GENERATED ALWAYS AS IDENTITY` (eski `SERIAL` yerine)
- PostgreSQL 18 yeniliklerine giriş: `uuidv7()`, virtual generated columns, asenkron I/O
- Yedekleme: `pg_dump` / `pg_restore`
- .NET'ten bağlanma: **Npgsql**, connection string, `NpgsqlDataSource`
- Identifier büyük/küçük harf tuzağı (`"OrderId"` vs `orderid`) → EF Core ile snake_case (`EFCore.NamingConventions`)
- 🛠 **Kod:** Compose ile PostgreSQL + pgAdmin; şema/seed/backup/restore script'leri; Npgsql ile bağlanıp CRUD yapan console uygulaması.

🎯 **Mülakat:** "ACID nedir, her harfi örnekle anlat" · "Index nasıl çalışır, neden her kolona index konmaz?" · "Composite index'te kolon sırası neden önemli?" · "`INNER JOIN` ile `LEFT JOIN` farkı?" · "`WHERE` ile `HAVING` farkı?" · "`DELETE`, `TRUNCATE` ve `DROP` farkı?" ⭐ · "3NF nedir?" · "Dirty read, phantom read nedir?" · "SQL mi NoSQL mi — neye göre seçersin?" · "Para için neden `float` kullanılmaz?" · "`SELECT` sorgusunun mantıksal çalışma sırası nedir?"

---

### J9. EF Core ile Veri Erişimi

> 📁 `01-junior/09-ef-core-basics/` · ⏱ ~2 hafta · Önkoşul: J8
> Performans, ileri modelleme ve Dapper → M7.

#### J9.1 ADO.NET: ORM'in Altında Ne Var · `01-adonet`
- `DbConnection`, `DbCommand`, `DbDataReader`, `DbTransaction` (Npgsql provider'ı üzerinden)
- **Parametreli sorgu ve SQL injection** ⭐
- Connection pooling nasıl çalışır; connection'ı `using` ile kapatmak; connection leak
- ORM'in neyi otomatikleştirdiğini görmek için önce elle yapmak
- 🛠 **Kod:** SQL injection'a açık bir login sorgusu → parametreli versiyonu; kapatılmayan connection'larla pool tükenmesi simülasyonu.

#### J9.2 DbContext & Modelleme · `02-dbcontext-modeling`
- Code-first vs database-first (`dotnet ef dbcontext scaffold`)
- `DbContext`, `DbSet<T>`, provider seçimi (Npgsql, SQL Server, SQLite)
- **`DbContext` neden scoped?** ⭐ — thread-safe değildir, kısa ömürlü bir unit of work'tür
- Konvansiyonlar, Data Annotations vs **Fluent API**, `IEntityTypeConfiguration<T>`
- İlişkiler: 1-1, 1-N, N-N (skip navigation), navigation property, foreign key
- 🛠 **Kod:** J8.1 e-ticaret şemasının EF Core code-first karşılığı; Fluent API konfigürasyon sınıfları.

#### J9.3 Migrations · `03-migrations`
- `dotnet ef migrations add / remove`, `database update`, `migrations script --idempotent`
- Migration dosyasının içi (`Up`/`Down`) ve model snapshot
- Seed data: `HasData`, `UseSeeding`/`UseAsyncSeeding` (EF Core 9+)
- Migration'ı production'da kim uygular — giriş (stratejiler → M6.6)
- 🛠 **Kod:** Kolon ekleme ve yeniden adlandırma migration'ları; üretilen SQL'in incelenmesi.

#### J9.4 Sorgulama · `04-querying`
- LINQ to Entities: **`IQueryable` → SQL çevirisi** ⭐; `ToQueryString()` ve `LogTo` ile üretilen SQL'i görme
- **`IEnumerable` vs `IQueryable`** — `AsEnumerable()`/`ToList()` sonrası filtrelemenin bellekte yapılması ⭐
- Loading stratejileri: eager (`Include`/`ThenInclude`), explicit, lazy (ve riskleri)
- **N+1 problemi** ⭐ — nasıl oluşur, loglardan nasıl tespit edilir
- **Projection** (`Select` ile DTO'ya) — sadece gereken kolonlar
- `AsNoTracking()` — salt okuma senaryoları
- Pagination: `Skip`/`Take` ve `OrderBy` zorunluluğu
- 🛠 **Kod:** N+1 üreten bir endpoint → `Include` ile → projection ile düzeltme; her adımda SQL loglarının ve sorgu sayısının karşılaştırılması.

#### J9.5 Change Tracking & Kaydetme · `05-change-tracking`
- Entity state'leri: Added, Modified, Deleted, Unchanged, Detached
- `SaveChanges` tek bir transaction'dır
- Güncelleme yolları: tracked entity üzerinden, `Update()`, `ExecuteUpdate`/`ExecuteDelete` (EF 7+, giriş)
- **Optimistic concurrency'ye giriş:** concurrency token (`[Timestamp]`, PostgreSQL'de `xmin`), `DbUpdateConcurrencyException` → `409 Conflict`
- 🛠 **Kod:** Aynı kaydı iki eşzamanlı istekle güncelleme → concurrency token ile çakışmayı yakalayıp `409` dönen endpoint + testi.

🎯 **Mülakat:** "`DbContext` neden scoped?" · "N+1 problemi nedir, nasıl çözülür?" · "`IEnumerable` ile `IQueryable` farkı?" · "`AsNoTracking` ne zaman kullanılır?" · "EF Core'da SQL injection olur mu?" (`FromSqlRaw` + string birleştirme ile evet)

---

### J10. Test Temelleri

> 📁 `01-junior/10-testing-basics/` · ⏱ ~2 hafta · Önkoşul: J6, J9
> Test stratejisi, Testcontainers, contract ve E2E testleri → M10.

#### J10.1 Unit Testing (xUnit v3) · `01-unit-testing`
- Neden test yazılır; test piramidine giriş
- **xUnit v3:** `[Fact]`, `[Theory]`, `[InlineData]`, `[MemberData]`; test projelerinin kendi başına çalıştırılabilir olması
- **Microsoft.Testing.Platform (MTP)** ve `dotnet test` entegrasyonu (.NET 10)
- NUnit, MSTest ve TUnit ile kısa karşılaştırma
- **Arrange-Act-Assert** ve isimlendirme (`Method_Scenario_ExpectedResult`)
- Assertion kütüphaneleri: yerleşik `Assert`, Shouldly, AwesomeAssertions — **FluentAssertions v8+ ticari lisanslıdır**
- Test verisi üretimi: Bogus
- Edge case disiplini: null, boş, sınır değer, negatif, çok büyük girdi
- 🛠 **Kod:** Fiyat hesaplama ve indirim kuralları servisi için kapsamlı unit test seti (`[Theory]` ağırlıklı).

#### J10.2 Mocking Temelleri · `02-mocking`
- Test double türleri: dummy, stub, fake, spy, mock
- **NSubstitute** (yaygın tercih), Moq (SponsorLink tartışması), FakeItEasy
- Ne zaman mock **gereksiz**: değer nesneleri, saf fonksiyonlar
- Zamanı soyutlama: **`TimeProvider`** (.NET 8+) ve `FakeTimeProvider`
- 🛠 **Kod:** E-posta gönderen sipariş servisinin NSubstitute ile testi; `FakeTimeProvider` ile "24 saat içinde ödenmeyen sipariş iptal edilir" kuralının testi.

#### J10.3 Test Edilebilir Kod & TDD · `03-testable-code-tdd`
- Test edilebilirliği bozanlar: static bağımlılık, `DateTime.Now`, servis içinde `new` ile bağımlılık, gizli global state
- DI'ın test edilebilirliğe katkısı
- TDD döngüsü: red → green → refactor; gerçekçi kullanımı
- 🛠 **Kod:** Test edilemez yazılmış bir servisin adım adım test edilebilir hale getirilmesi + TDD ile çözülmüş bir kata (String Calculator).

#### J10.4 İlk Integration Test · `04-first-integration-test`
- Unit vs integration test farkı
- **`WebApplicationFactory<Program>`** ile API'yi bellekte ayağa kaldırma; `public partial class Program { }` neden gerekir
- `HttpClient` ile endpoint'i uçtan uca çağırma
- Test veritabanı: **EF Core InMemory provider neden yanıltıcı** ⭐ (gerçek DB ile test → M10.2)
- 🛠 **Kod:** J7 Todo API'si için happy path, validation hatası ve 404 integration testleri.

#### J10.5 GitHub Actions ile İlk CI · `05-first-ci`
- Workflow, job, step, runner kavramları
- `actions/setup-dotnet` → restore → build → test
- Pull request'te otomatik test ve branch koruma kuralı
- *(Kategori notu: CI aslında DevOps'a aittir; testlerin her PR'da koşması gerektiği için Junior'da burada duruyor, derinliği → M14.1.)*
- 🛠 **Kod:** Junior capstone için build + test pipeline'ı.

🎯 **Mülakat:** "Unit test ile integration test farkı?" · "Mock ile stub farkı?" · "`DateTime.Now` test edilebilirliği neden bozar?" · "AAA nedir?" · "%100 coverage iyi test demek mi?" · "CI nedir, neden PR'da test çalıştırılır?"

---

### J11. Güvenlik & Kimlik Temelleri

> 📁 `01-junior/11-security-auth-basics/` · ⏱ ~2 hafta · Önkoşul: J7
> OAuth 2.1/OIDC akışlarının derinlemesine hali ve **Keycloak** → M9. Güvenlik mühendisliği → S7.

#### J11.1 Authentication vs Authorization · `01-authn-vs-authz` ⭐
- AuthN = "Kimsin?" · AuthZ = "Ne yapabilirsin?"
- Session/cookie tabanlı (stateful) vs token tabanlı (stateless) kimlik doğrulama
- ASP.NET Core: `AddAuthentication`, `AddAuthorization`, `[Authorize]`, `[AllowAnonymous]`, `RequireAuthorization()`
- Role-based ve claim-based authorization'a giriş
- **ASP.NET Core Identity** temelleri: kullanıcı/rol tabloları, password hashing (PBKDF2), lockout; Identity API endpoint'leri (.NET 8+); **passkey desteği (.NET 10)**
- Şifre saklama: **asla düz metin, asla MD5/SHA1** — salt'lı, yavaş hash (PBKDF2, bcrypt, Argon2)
- 🛠 **Kod:** Identity ile kayıt/giriş yapan, `Admin` ve `User` rolleriyle yetkilendirilmiş bir API.

#### J11.2 JWT Temelleri · `02-jwt-basics`
- Yapı: `header.payload.signature` — Base64Url kodlu; **şifreli değil, imzalı** ⭐ (payload'a hassas veri konmaz)
- Standart claim'ler: `iss`, `aud`, `sub`, `exp`, `iat`, `nbf`
- İmzalama: HS256 (simetrik, paylaşılan secret) vs RS256/ES256 (asimetrik, public key ile doğrulama)
- Doğrulama adımları: imza, `exp`, `iss`, `aud`; `alg: none` saldırısı
- Access token vs refresh token; kısa ömürlü access token neden
- `AddJwtBearer` ile API koruma
- 🛠 **Kod:** Öğrenme amaçlı kendi JWT'sini üreten login endpoint'i + JwtBearer ile korunan endpoint + token'ı jwt.io'da inceleme. Not: production'da token'ı **identity provider** üretir (→ M9).

#### J11.3 OAuth 2.0 & OpenID Connect'e Giriş · `03-oauth-oidc-intro`
- Çözülen problem: "Şifremi vermeden bir uygulamaya başka bir sistemdeki verime erişim izni vermek"
- **OAuth 2.0 = yetkilendirme (delegated authorization); OIDC = OAuth üzerine kimlik katmanı** ⭐
- Roller: Resource Owner, Client, Authorization Server, Resource Server
- Akışlar (kavramsal):
  - **Authorization Code + PKCE** — kullanıcı içeren tüm uygulamalar (web, SPA, mobil)
  - **Client Credentials** — servisten servise
  - Implicit ve Resource Owner Password akışları **artık kullanılmaz** (OAuth 2.1)
- **Access token vs ID token vs refresh token** — kimin için, ne için ⭐
- Scope kavramı ve consent ekranı
- "Google ile giriş yap" akışının adım adım anlatımı
- Identity provider'lar: **Keycloak**, Microsoft Entra ID, Auth0, Duende IdentityServer, OpenIddict (karşılaştırma → M9.7)
- 🛠 **Kod:** Docker'da Keycloak'ı ayağa kaldır, bir realm + client + kullanıcı oluştur, ASP.NET Core API'yi Keycloak'ın verdiği token ile koru (en basit kurulum; derinlemesine → M9.3).

#### J11.4 Temel Web Güvenliği · `04-web-security-basics`
- HTTPS zorunluluğu, HSTS, `UseHttpsRedirection`
- **SQL Injection** — parametreli sorgu (→ J9.1)
- **XSS** — Razor'un otomatik HTML encode etmesi, `Html.Raw` tehlikesi
- **CSRF** — cookie tabanlı auth'ta anti-forgery token, `SameSite` cookie
- **CORS** — bir güvenlik özelliği değil, same-origin policy'nin **kontrollü gevşetmesi**; `AllowAnyOrigin` + credentials neden çalışmaz
- Secret yönetimi: git'e secret commit edildiyse silmek yetmez, **rotate et**
- Bağımlılık güvenliği: NuGet audit uyarıları, `dotnet list package --vulnerable`
- OWASP Top 10'un varlığını bilmek (derin → S7.1)
- 🛠 **Kod:** Kasıtlı olarak açıklı küçük bir uygulama (SQLi, XSS, gevşek CORS, loglanan şifre) + her açığın düzeltilmiş hali ve açığı kanıtlayan test.

🎯 **Mülakat:** "Authentication ile authorization farkı?" · "JWT şifreli midir?" · "Access token ile ID token farkı?" · "OAuth ile OIDC farkı?" · "PKCE ne işe yarar?" · "XSS ile CSRF farkı?" · "Şifreler nasıl saklanmalı?"

---

### J12. Docker Temelleri

> 📁 `01-junior/12-docker-basics/` · ⏱ ~1 hafta · Önkoşul: J7
> Container ileri konuları ve Kubernetes → M14.

#### J12.1 Container Kavramları · `01-container-concepts`
- Container ≠ VM — izolasyon modeli farkı
- Image, container, registry; layer ve layer cache
- `docker run` bayrakları (`-p`, `-v`, `-e`, `--name`, `--rm`, `-d`); `ps`, `logs`, `exec`, `inspect`, `stop`, `rm`
- Volume vs bind mount; named volume'un yaşam döngüsü
- **Ağ modları:** `bridge` (varsayılan, container'lar arası DNS), `host`, `none`; kullanıcı tanımlı network neden gerekli
- Registry'ler: Docker Hub (ve **anonim pull rate limit**'i), GitHub Container Registry, Azure Container Registry (→ C2.1)
- Disk yönetimi: `docker system df` / `prune` — dangling image ve volume'ların diski doldurması
- 🛠 **Kod:** PostgreSQL, Redis ve MongoDB'yi `docker run` ile ayağa kaldırıp volume ile veri kalıcılığını test eden script'ler; aynı iki container'ı önce varsayılan bridge'de sonra kullanıcı tanımlı network'te konuşturup DNS farkını gözlemleme.

#### J12.2 .NET için Dockerfile · `02-dockerfile-dotnet` ⭐
- Base image: `sdk` (build) vs `aspnet`/`runtime` (çalıştırma); base image'ı **digest ile sabitleme** (tekrarlanabilir build)
- **Multi-stage build** ⭐ — SDK'yı final image'a taşımamak
- Layer cache: önce `.csproj` kopyala → `restore` → sonra kaynak kodu (BuildKit cache mount ile daha iyisi → M14.2)
- **`ENTRYPOINT` vs `CMD`** ⭐ — en sık sorulan Dockerfile sorusu
  - `ENTRYPOINT` sabit çalıştırılabilir, `CMD` **üzerine yazılabilen** varsayılan argüman; `docker run <image> <arg>` CMD'yi ezer, ENTRYPOINT'i ezmez (`--entrypoint` gerekir)
  - Tipik kalıp: `ENTRYPOINT ["dotnet","App.dll"]` + `CMD` ile varsayılan argümanlar
  - **Exec form (`["..."]`) vs shell form** ⭐ — shell form'da uygulama `sh -c`'nin alt süreci olur, **SIGTERM uygulamaya ulaşmaz** → graceful shutdown çalışmaz (Kubernetes'te asıl sorun → M14.4)
- **`COPY` vs `ADD`** ⭐ — varsayılan `COPY`; `ADD` URL indirir ve tar arşivini otomatik açar, bu yüzden build'i **tekrarlanamaz** ve sürprizli hale getirir
- **`HEALTHCHECK`** instruction — süreç ayakta mı değil, **uygulama çalışıyor mu** sorusunu cevaplar; art arda sıfır dışı çıkış kodu container'ı `unhealthy` yapar; Dockerfile `HEALTHCHECK` vs Compose `healthcheck` (→ J12.3) vs Kubernetes probe (→ M14.4) ilişkisi
- `EXPOSE`'un gerçekte ne yaptığı ⭐ — **port açmaz**, yalnızca dokümantasyondur; yayınlamak için `-p` gerekir
- `WORKDIR`, `ARG` vs `ENV`; **secret'ı `ENV`/layer'a gömmemek** (build secret → M14.2)
- Non-root kullanıcı (`USER $APP_UID`)
- `dotnet publish /t:PublishContainer` ile Dockerfile'sız image
- 🛠 **Kod:** Todo API için tek aşamalı (kötü) ve multi-stage (iyi) Dockerfile; image boyutu ve build süresi karşılaştırması; aynı image'ı shell form ve exec form `ENTRYPOINT` ile çalıştırıp `docker stop` sonrası uygulamanın SIGTERM alıp almadığını loglardan kanıtlama.

#### J12.3 Docker Compose ile Geliştirme Ortamı · `03-docker-compose`
- Çok servisli tanım: API + PostgreSQL + Redis + pgAdmin
- `depends_on` + `healthcheck` ile başlangıç sırası
- Servis adıyla DNS çözümleme; **container içinden `localhost` tuzağı** ⭐
- Environment değişkenleri, `.env`, override dosyaları
- Hedef: "tek komutla çalışan repo"
- 🛠 **Kod:** `docker compose up` ile API + DB + cache'in birlikte kalktığı, sağlık kontrollü geliştirme ortamı.

🎯 **Mülakat:** "Container ile VM farkı?" · "Multi-stage build neden?" · "`ENTRYPOINT` ile `CMD` farkı?" ⭐ · "`COPY` ile `ADD` farkı, hangisini kullanırsın?" ⭐ · "Shell form `ENTRYPOINT` neden graceful shutdown'ı bozar?" · "`EXPOSE` portu açar mı?" · "Container içindeki uygulama `localhost:5432`'ye neden bağlanamaz?" · "Volume ne işe yarar?" · "Image layer cache'ini bozmamak için Dockerfile'ı nasıl sıralarsın?"

---

### J13. AI ile Çalışmaya Giriş

> 📁 `01-junior/13-ai-basics/` · ⏱ ~1 hafta · Önkoşul: J7
> 2026'da junior pozisyonlarda bile soruluyor: "AI araçlarını nasıl kullanıyorsun?" RAG, MCP, agent'lar → M15, S9.

#### J13.1 LLM Temelleri · `01-llm-basics`
- LLM nedir (sezgisel): bir sonraki token'ı tahmin eden model
- Token, context window, temperature, max tokens
- Mesaj rolleri: system / user / assistant
- Hallucination, knowledge cutoff, determinizm eksikliği
- Prompt temelleri: net görev tanımı, örnek verme (few-shot), çıktı formatı belirtme
- Sağlayıcılar ve modeller: OpenAI, Anthropic Claude, Microsoft Foundry üzerinden sunulan modeller, yerel modeller (Ollama, Foundry Local)
- Maliyet: token bazlı fiyatlandırma, input vs output token
- 🛠 **Kod:** Aynı prompt'u farklı temperature ve system prompt'larla çalıştırıp çıktıları karşılaştıran deney.

#### J13.2 AI Kodlama Asistanlarını Doğru Kullanmak · `02-ai-coding-assistants`
- GitHub Copilot, Claude Code, Cursor — 2026'da standart geliştirici aracı
- **"Vibe coding" vs mühendislik** ⭐ — üretilen kodu anlamadan kabul etmemek; her satırı okumak ve test etmek
- İyi görev tanımı: bağlam, kısıtlar, kabul kriteri
- AI'ın tipik hataları: var olmayan API uydurma, eski sürüm sözdizimi, güvenlik açığı, gereksiz karmaşıklık
- Gizlilik: şirket kodunu ve secret'ları hangi araca verdiğini bilmek
- Öğrenme dengesi: temel konuları **önce kendin** yaz, AI'ı açıklama ve review için kullan
- 🛠 **Kod:** Bir görevi önce kendin çöz, sonra AI ile çöz; iki çözümü doğruluk, okunabilirlik ve test kapsamı açısından karşılaştıran kısa değerlendirme (README).

#### J13.3 İlk LLM Entegrasyonu · `03-first-llm-integration`
- **Microsoft.Extensions.AI** ve `IChatClient` — sağlayıcıdan bağımsız kod
- Sağlayıcılar: OpenAI, Azure OpenAI / Microsoft Foundry, Ollama (yerel, ücretsiz)
- `GetResponseAsync` ve **streaming** (`GetStreamingResponseAsync`)
- Konuşma geçmişini tutma
- API anahtarını User Secrets'ta saklama
- 🛠 **Kod:** Ollama ile yerelde çalışan, streaming yanıt veren console chat uygulaması; tek satır değişiklikle bulut sağlayıcısına geçiş.

🎯 **Mülakat:** "LLM nasıl çalışır (sezgisel olarak)?" · "Hallucination nedir, nasıl azaltılır?" · "AI'ın ürettiği kodu nasıl doğrularsın?" · "Temperature ne işe yarar?"

---

### Junior Capstone: Görev Yönetimi API'si

> 📁 `01-junior/capstone/` · ⏱ ~2 hafta
> Junior seviyesinin bitiş çizgisi. Mülakatta "bana bir projeni anlat" sorusunun cevabı.

**Gereksinimler:**
- Minimal API (veya Controller) ile görev/proje/etiket CRUD'u, filtreleme, sıralama, pagination
- PostgreSQL + EF Core (migration'lar, seed, projection, `AsNoTracking`)
- Keycloak (veya ASP.NET Core Identity) ile JWT authentication; `Admin`/`User` rolleri
- Validation + `ProblemDetails` + global exception handler
- OpenAPI + Scalar + `.http` koleksiyonu
- Unit test + `WebApplicationFactory` integration testleri
- Structured logging, health check endpoint'i
- Multi-stage Dockerfile + `docker compose up` ile tek komutta çalışma
- GitHub Actions CI (build + test); isteğe bağlı olarak App Service'e deploy (Azure laboratuvarı → C1.6)
- **Bonus:** Görev açıklamasından `IChatClient` ile otomatik etiket önerisi
- README: kurulum, mimari kararlar, varsayımlar, yapılmayanlar ve nedenleri

**Çıkış kriteri:** Capstone tamam + I6'daki Junior mock mülakatı kapalı kitap geçildi + D1–D3 ve D5'te LeetCode Easy seviyesinde rahatlık.

---

## Seviye 2 — Mid

> **Hedef:** Production'da yaşayan servisleri tasarlamak, güvenceye almak, ölçmek ve hata ayıklamak. "Nasıl kullanılır"dan **"nasıl çalışır ve ne zaman bozulur"**a geçiş.
> **Süre:** ~10 ay (günde 2-3 saat, DSA dahil) · **Klasör:** `02-mid/` · **Önkoşul:** Junior capstone
> Mid mülakatlarında her cevaba **"neden?"** ve **"production'da ne olur?"** soruları eklenir.

---

### M1. C# Derinlemesine

> 📁 `02-mid/01-csharp-deep-dive/` · ⏱ ~2 hafta · Önkoşul: J2, J3
> Dilin iç yüzü. `Span<T>`, reflection ve source generator'lar → S1, S2.

#### M1.1 Dil Evrimi: C# 8 → 15 · `01-language-evolution`
- **C# 8:** nullable reference types, switch expression, async streams, ranges/indices (`^1`, `1..3`), default interface methods, `using` declaration
- **C# 9:** records, init-only setter, top-level statements, target-typed `new`, pattern geliştirmeleri
- **C# 10:** file-scoped namespace, global usings, record struct, constant interpolated string
- **C# 11:** raw string literal, `required` members, generic attribute, **list patterns**, UTF-8 string literal, static abstract interface members
- **C# 12:** primary constructors (tüm tiplerde), collection expressions, inline arrays, alias any type
- **C# 13:** `params` collections, `System.Threading.Lock`, `ref`/`unsafe` iterator ve async'te, `allows ref struct`
- **C# 14 (.NET 10):**
  - **Extension members** — `extension` blokları ile extension **property**, **static** extension üye ve operatör
  - **`field` keyword** — backing field tanımlamadan property accessor'ında doğrulama
  - Null-conditional assignment (`a?.B = x`), implicit `Span<T>` dönüşümleri, `nameof` ile unbound generic, partial constructor/event, kullanıcı tanımlı compound assignment operatörleri
- **C# 15 (.NET 11, önizleme):** union tipleri, extension indexer'lar
- Pattern matching'in tamamı: type, constant, relational, logical, property, positional, list patterns
- 🛠 **Kod:** Her sürüm için "önce/sonra" örnekleri içeren tek console uygulaması; C# 14 extension members ile `IEnumerable<T>.IsEmpty` property'si ve static `string.IsNullOrBlank` benzeri üye.

#### M1.2 Generics Derinlemesine · `02-generics-advanced`
- Tüm constraint'ler: `class`, `struct`, `unmanaged`, `notnull`, `new()`, base class, interface, `allows ref struct`
- **Covariance (`out`) / contravariance (`in`)** ⭐ — `IEnumerable<Kedi>` → `IEnumerable<Hayvan>`; `Action<Hayvan>` → `Action<Kedi>`; **array covariance tuzağı** (`ArrayTypeMismatchException`)
- **Generic'ler runtime'da reified** ⭐ — Java'nın type erasure'ından farkı; value type'lar için ayrı native kod, reference type'lar için paylaşılan kod
- Generic tipte static alan: her kapalı tip için ayrı (`Cache<T>` deseni)
- **Generic math:** `INumber<T>`, static abstract interface members
- 🛠 **Kod:** Generic math ile tip bağımsız `Sum<T>`/`Average<T>`, variance demosu, tip başına static cache.

#### M1.3 LINQ Derinlemesine & Expression Tree · `03-linq-advanced`
- **`Func<T,bool>` vs `Expression<Func<T,bool>>`** ⭐ — derlenmiş kod vs veri olarak kod
- Expression tree'yi dinamik oluşturma (`Expression.Parameter`, `Expression.Lambda`), `Compile()`
- EF Core'un expression tree'yi SQL'e çevirmesi; çevrilemeyen ifadeler ve client evaluation
- Custom LINQ operatörü yazma (extension + `yield`), `IQueryable` için `WhereIf`
- LINQ performansı: allocation, materialization noktaları, `OrderBy` maliyeti; .NET 9/10 LINQ iyileştirmeleri
- 🛠 **Kod:** Kullanıcı filtrelerinden dinamik `Expression` üretip EF Core'a SQL olarak gönderen sorgu oluşturucu + üretilen SQL'in testi.

#### M1.4 Iterator'lar & Async Stream'ler · `04-iterators-async-streams`
- `yield return` / `yield break` → derleyicinin ürettiği iterator state machine
- `foreach`'in `Dispose` çağırması, `finally` bloğunun iterator'da ne zaman çalıştığı
- `IAsyncEnumerable<T>`, `await foreach`, `[EnumeratorCancellation]`, `WithCancellation`
- EF Core'dan stream (`AsAsyncEnumerable`), ASP.NET Core'dan `IAsyncEnumerable` dönme
- 🛠 **Kod:** GB'lık CSV'yi satır satır işleyen iterator (sabit bellek) + DB'den istemciye stream eden endpoint; bellek kullanımı karşılaştırması.

#### M1.5 Kaynak Yönetimi: `IDisposable` & Dispose Pattern · `05-resource-management`
- Managed vs unmanaged kaynak
- `IDisposable`, `IAsyncDisposable`, `using`/`await using`
- Dispose pattern (`Dispose(bool disposing)`), finalizer ne zaman gerekir (nadiren), `SafeHandle`
- DI container'ın oluşturduğu servisleri dispose etmesi; **root scope'tan çözülen transient disposable tuzağı** ⭐
- Dispose edilmesi unutulan tipik nesneler: `CancellationTokenSource`, `Timer`, `HttpResponseMessage`, `Stream`
- 🛠 **Kod:** Disposable servislerin DI yaşam döngüsünü loglayan demo; dispose edilmeyen kaynakla handle sızıntısı ve `dotnet-counters` ile gözlemi.

#### M1.6 Delegate'ler & Event'ler Derinlemesine · `06-delegates-events-advanced`
- Multicast delegate, invocation list, multicast'te dönüş değeri ve exception davranışı
- **Event handler memory leak** ⭐ — uzun ömürlü publisher, kısa ömürlü subscriber; weak event pattern
- Closure'ın maliyeti: display class allocation; `static` lambda ile yakalamayı engellemek
- `IObservable<T>` / Reactive Extensions'a giriş
- 🛠 **Kod:** Event aboneliği yüzünden GC'nin toplayamadığı nesne demosu (`dotnet-gcdump` ile kanıt) ve düzeltmesi.

🎯 **Mülakat:** "Covariance/contravariance nedir?" · "`Func` ile `Expression<Func>` farkı?" · "`yield` nasıl çalışır?" · "C# generics ile Java generics farkı?" · "C# 14 extension members ne getirdi?" · "Finalizer ne zaman gerekir?" · ".NET'te en yaygın memory leak sebebi?"

---

### M2. Build, Runtime & Deployment

> 📁 `02-mid/02-build-runtime-deployment/` · ⏱ ~2 hafta · Önkoşul: J5
> **"C# kodu derlenince ne oluyor? Kod nasıl çalışmaya başlıyor? Production'a hangi formatta çıkılır?"**
> Bu modül okunacak değil, **gözlemlenecek** — her konuda "aç ve bak" lab'ı var. JIT, GC ve assembly loading'in derin hali → S1.

#### M2.1 Roslyn & Lowering · `01-roslyn-lowering`
- **Roslyn:** C#/VB derleyicisi, "compiler as a service" (IDE, analyzer, source generator hep bunu kullanır)
- Derleme aşamaları: **lexing** → **parsing** (syntax tree) → **binding** (semantic model) → **lowering** → **IL emission**
- **Lowering — derleyicinin gizli dönüşümleri** ⭐

  | Yazdığın | Derleyicinin ürettiği |
  |----------|----------------------|
  | `foreach` | `GetEnumerator()` + `while(MoveNext())` + `try/finally { Dispose() }` |
  | `using` | `try/finally` + `Dispose()` |
  | `async`/`await` | `IAsyncStateMachine` implement eden **state machine** |
  | `yield return` | Iterator state machine sınıfı |
  | Lambda + closure | **Display class** (yakalanan değişkenler alan olur) |
  | `var` | Gerçek tip — IL'de `var` yoktur |
  | String interpolation | `DefaultInterpolatedStringHandler` |
  | LINQ query syntax | Extension method zinciri |
  | Auto-property | `private` backing field + `get_X()`/`set_X()` |
  | `record` | Class + `Equals`/`GetHashCode`/`ToString`/`Deconstruct`/`<Clone>$` |
  | `lock` | `Monitor.Enter/Exit` + `try/finally` (C# 13'te `Lock` tipiyle `EnterScope`) |
  | Collection expression | Hedef tipe göre en verimli oluşturma kodu |

- Nullable reference types **sadece derleme zamanı** — IL'de `[Nullable]` attribute'u olarak iz bırakır
- `dynamic`'in IL karşılığı (`CallSite` + DLR)
- Conditional compilation: `#if`, `DefineConstants`, `[Conditional("DEBUG")]`
- Source generator çıktılarını görmek: `EmitCompilerGeneratedFiles`
- 🛠 **Kod:** Lowering tablosundaki her satır için örnek dosya + sharplab.io bağlantıları; `EmitCompilerGeneratedFiles` ile regex/JSON source generator çıktılarının incelenmesi.

#### M2.2 IL, Metadata & Assembly Anatomisi · `02-il-metadata-assembly`
- IL neden var: dil bağımsızlığı + platform bağımsızlığı; **stack-based** sanal makine dili
- Temel opcode'lar: `ldarg`, `ldloc`, `stloc`, `ldfld`, `call`, `callvirt`, `newobj`, `box`/`unbox`, `ret`
- **`call` vs `callvirt`** — `callvirt` null kontrolü de yapar; derleyici neden non-virtual metodlarda bile `callvirt` üretir ⭐
- Assembly = PE dosyası: PE header, **CLR header**, **metadata tabloları** (`TypeDef`, `MethodDef`, `MemberRef`, `AssemblyRef`), IL, **manifest**, gömülü kaynaklar
- `.dll` ile `.exe` aynı formattadır — fark PE bayrağı ve entry point
- Reflection'ın mümkün olmasının sebebi: metadata
- Assembly identity, strong naming
- Araçlar: ILSpy, dotPeek, `dotnet-ildasm`; decompile edilebilirlik ve obfuscation'ın sınırları
- 🛠 **Kod:** Kendi `.dll`'ini ILSpy ile aç; bir metodun IL'ini satır satır yorumla; Debug vs Release IL farkını belgele.

#### M2.3 MSBuild & Build Süreci · `03-msbuild`
- `.csproj` = MSBuild proje dosyası; SDK-style format
- Kavramlar: **Property**, **Item**, **Target**, **Task**
- Build hattı: `Restore` → `Build` → (`Publish`)
- Restore: paket grafiği, `project.assets.json`, transitive bağımlılık, **nearest wins** kuralı, `packages.lock.json` ile deterministik restore
- **`Directory.Build.props`/`.targets`** ve **Central Package Management** (`Directory.Packages.props`)
- **Debug vs Release:**

  | | Debug | Release |
  |---|-------|---------|
  | JIT optimizasyonu | Kapalı | Açık |
  | `DEBUG` sembolü | Tanımlı | Tanımsız |
  | Inline / dead code elimination | Yok | Var |
  | Debug deneyimi | Tam | Kısıtlı ("optimized away") |

- Incremental build; deterministic build
- `TreatWarningsAsErrors`, `NoWarn`, `.editorconfig` severity
- **`dotnet build -bl`** + MSBuild Structured Log Viewer — build teşhisinin en güçlü aracı
- Multi-targeting (`<TargetFrameworks>net8.0;net10.0</TargetFrameworks>`) ve `#if NET10_0_OR_GREATER`
- 🛠 **Kod:** Çok projeli bir solution'a Central Package Management + `Directory.Build.props` ekle; `-bl` ile binlog al ve en yavaş target'ı bul.

#### M2.4 apphost & Başlatma Zinciri · `04-apphost-startup-chain`
- **apphost:** SDK'nın, içindeki placeholder'ı senin `.dll` adınla patch'leyip `bin/`'e koyduğu native launcher; `<UseAppHost>false</UseAppHost>`
- **Başlatma zinciri:**
  ```
  MyApp.exe (apphost — native)
    ↓
  hostfxr      runtimeconfig.json'ı okur, framework'ü çözer, roll-forward uygular, runtime seçer
    ↓
  hostpolicy   deps.json'ı okur, probing path'leri ve TPA listesini kurar
    ↓
  coreclr      GC heap'i, default AssemblyLoadContext'i, type system'i ayağa kaldırır
    ↓
  MyApp.dll    Main bulunur ve çağrılır
  ```
- `runtimeconfig.json`: `framework`, `rollForward`, `configProperties` (`System.GC.Server`, `System.Globalization.Invariant`...)
- `deps.json` eksik/yanlışsa `FileNotFoundException`
- `dotnet MyApp.dll` — `dotnet` muxer'ı apphost'un yerini alır
- Roll-forward politikaları: `LatestPatch`, `Minor`, `Major`, `Disable`
- Ortam teşhisi: `dotnet --info`, `--list-runtimes`, `--list-sdks`, `DOTNET_ROOT`
- 🛠 **Kod:** `UseAppHost=false` ile build et → `.exe` kaybolur, `dotnet app.dll` çalışır; `runtimeconfig.json`'daki sürümü bozup hata mesajını incele; `COREHOST_TRACE=1` ile host log'larını oku.

#### M2.5 Deployment Modelleri · `05-deployment-models`

| Model | Runtime gereksinimi | Boyut | Cold start | Not |
|-------|--------------------|-------|-----------|-----|
| **Framework-dependent** | Makinede kurulu | Küçük | Orta | Varsayılan; runtime yamaları ayrı gelir |
| **Self-contained** | Uygulamayla gelir | ~70 MB | Orta | Runtime güncellemesi senin sorumluluğun |
| **Single-file** | Modele göre | Tek dosya | Orta | Dağıtım kolaylığı |
| **Trimmed** | Uygulamayla | Belirgin küçülme | Orta | Reflection riski, IL2xxx uyarıları |
| **ReadyToRun** | Kurulu veya SCD | Büyür | **İyileşir** | Önceden derlenmiş native + IL; JIT hâlâ var |
| **Native AOT** | Yok | Birkaç MB | **En hızlı** | JIT yok, reflection/dinamik kod kısıtlı |

- **RID:** `win-x64`, `linux-x64`, `linux-musl-x64`, `linux-arm64`, `osx-arm64`
- Komutlar:
  ```bash
  dotnet publish -c Release                                    # FDD
  dotnet publish -c Release -r linux-x64 --self-contained      # SCD
  dotnet publish -c Release -r linux-x64 -p:PublishSingleFile=true
  dotnet publish -c Release -r linux-x64 -p:PublishTrimmed=true
  dotnet publish -c Release -r linux-x64 -p:PublishReadyToRun=true
  dotnet publish -c Release -r linux-x64 -p:PublishAot=true
  ```
- **Karar rehberi:** uzun ömürlü web API/container → FDD + chiseled image; serverless/CLI → Native AOT veya R2R; kurulumsuz araç → self-contained single-file
- 🛠 **Kod:** Aynı "Hello API"yi altı modelde publish et; **boyut + ilk isteğe kadar geçen süre + bellek** tablosunu çıkar.

#### M2.6 PDB, Debug Sembolleri & Source Link · `06-pdb-symbols`
- PDB: IL offset ↔ kaynak satır eşlemesi; portable vs embedded PDB
- Release build'de stack trace'te satır numarası için PDB gerekir
- **Source Link** — NuGet paketinin kaynağına debugger ile inmek; `.snupkg` ve symbol server
- Production'a PDB göndermeli mi: teşhis vs tersine mühendislik trade-off'u
- `DebuggerDisplay`, `DebuggerStepThrough` attribute'ları
- 🛠 **Kod:** PDB'li ve PDB'siz Release build'de exception stack trace karşılaştırması; Source Link'li bir paketin içine adım atma.

#### M2.7 Gözlem Laboratuvarı · `07-observation-labs`
1. Console app → `bin/` içeriğini listele, her dosyanın görevini yaz
2. ILSpy ile kendi `.dll`'ini aç, bir metodun IL'ini oku
3. sharplab.io'da `foreach`/`async`/lambda/`record` lowering'ini incele
4. `UseAppHost=false` ile `.exe`'nin kaybolduğunu gör
5. `runtimeconfig.json` + `deps.json`'ı satır satır yorumla
6. Runtime sürümünü bozup hata mesajını gözlemle
7. Altı deployment modelinde **boyut + cold start tablosu** çıkar
8. `dotnet build -bl` → en yavaş target'ı bul
9. Debug vs Release IL'ini karşılaştır
10. Reflection kullanan kodu `PublishTrimmed` ile yayınla → runtime hatasını gör → `[DynamicDependency]` veya source generator ile düzelt
11. Aynı API'yi `aspnet`, `aspnet:*-chiseled` ve AOT image olarak paketle; boyutları karşılaştır

- 🛠 **Kod:** Her lab ayrı alt klasörde (`lab-01-bin-contents/` ... `lab-11-container-images/`): çalıştırma script'i + gözlem notları + sonuç tablosu.

🎯 **Mülakat:** "`dotnet run` ile `dotnet publish` farkı, production'a hangisiyle çıkarsın?" · "C# kodu nasıl `.exe` oluyor?" · "IL nedir, neden var?" · "`call` ile `callvirt` farkı?" · "Self-contained ile framework-dependent farkı?" · "Native AOT ne kazandırır, ne kaybettirir?"

---

### M3. ASP.NET Core Derinlemesine

> 📁 `02-mid/03-aspnetcore-deep-dive/` · ⏱ ~2.5 hafta · Önkoşul: J6, J7
> Mülakatlarda **en çok atlanan ve en çok sorulan** kısım. "Kullanıyorum"dan "nasıl çalıştığını biliyorum"a.

#### M3.1 Generic Host & Yaşam Döngüsü · `01-generic-host`
- `IHost`, `HostApplicationBuilder`, `WebApplication.CreateBuilder()` altında ne oluyor
- `IHostedService` / `BackgroundService` sırası: `StartAsync` → `ExecuteAsync` → `StopAsync`; `IHostedLifecycleService`
- **Graceful shutdown:** `IHostApplicationLifetime`, `ApplicationStopping`, `HostOptions.ShutdownTimeout`; container/Kubernetes'te SIGTERM
- **Startup'ta async iş** (migration, cache ısıtma) — doğru ve yanlış yolları
- `IStartupFilter`
- Startup performansı ve cold start
- 🛠 **Kod:** Yaşam döngüsü event'lerini loglayan host; SIGTERM sonrası devam eden isteklerin tamamlanmasını bekleyen graceful shutdown demosu.

#### M3.2 Configuration & Options Derinlemesine · `02-configuration-options`
- Provider zinciri ve öncelik; custom configuration provider yazma
- **Options pattern:**
  - `IOptions<T>` — singleton, uygulama ömrü boyunca sabit
  - `IOptionsSnapshot<T>` — scoped, request başına yeniden okunur
  - `IOptionsMonitor<T>` — singleton, `OnChange` ile değişiklik bildirimi
  - Hangisi ne zaman ⭐
- Validation: `ValidateDataAnnotations()`, `Validate()`, **`ValidateOnStart()`**; options validation source generator
- Named options, `IConfigureOptions<T>`, `PostConfigure`
- `reloadOnChange` ve maliyeti
- Azure App Configuration ile merkezi config; **feature flag**'ler (`Microsoft.FeatureManagement`)
- 🛠 **Kod:** Üç options arayüzünün çalışma anında değişen config'e tepkisini gösteren demo + yanlış config'le uygulamanın başlamadığı `ValidateOnStart` örneği.

#### M3.3 Dependency Injection Derinlemesine · `03-dependency-injection`
- **Captive dependency** ⭐ — tespit (`ValidateScopes`, `ValidateOnBuild`) ve çözüm
- Singleton içinde scoped kullanmak: `IServiceScopeFactory` ile manuel scope
- **Keyed services** (.NET 8+): `[FromKeyedServices("x")]`
- `TryAdd*`, `Replace`, `RemoveAll`; factory ile kayıt
- **Decorator pattern** DI ile (Scrutor `.Decorate<>()` veya elle)
- Assembly scanning (Scrutor)
- Service Locator neden anti-pattern
- `ActivatorUtilities`, open generic kayıt
- 3rd party container (Autofac) ne zaman gerekir
- 🛠 **Kod:** Önbellekleme decorator'ı olan bir repository; keyed services ile ödeme sağlayıcısı seçimi; captive dependency tespiti.

#### M3.4 Middleware Derinlemesine · `04-middleware`
- Önerilen sıra ve **gerekçeleri:**
  ```
  ExceptionHandler → HSTS → HttpsRedirection → StaticFiles → Routing → CORS →
  Authentication → Authorization → RateLimiter → OutputCache → Endpoints
  ```
- `Use`, `Run`, `Map`, `MapWhen`, `UseWhen`
- Convention-based middleware vs **`IMiddleware`** (DI-friendly)
- Middleware'e scoped servis enjekte etme: constructor değil, **`InvokeAsync` parametresi** ⭐
- Request/response body okuma (`EnableBuffering`) ve stream tuzakları
- `HttpContext.Items`, `IHttpContextAccessor` ve riskleri
- **Middleware vs filter vs endpoint filter** — karar tablosu
- 🛠 **Kod:** Request/response body loglayan (hassas alanları maskeleyen) middleware; aynı cross-cutting işi middleware, filter ve endpoint filter ile ayrı ayrı yazıp karşılaştırma.

#### M3.5 Filters & Endpoint Filters · `05-filters`
- MVC filter sırası: Authorization → Resource → Action → Exception → Result
- `IAsyncActionFilter`, `IResourceFilter` (cache için ideal), `IExceptionFilter`, `IResultFilter`
- Filter scope (global, controller, action) ve sıra
- `ServiceFilter` vs `TypeFilter` — DI ile filter
- **`IEndpointFilter`** (Minimal API)
- 🛠 **Kod:** Idempotency-Key header'ını kontrol eden filter + Minimal API'de validation endpoint filter'ı.

#### M3.6 Background Services & Zamanlanmış İşler · `06-background-services`
- `BackgroundService` doğru implementasyonu, `CancellationToken` ile döngü
- **Background service'te scoped servis** (`IServiceScopeFactory`) — çok sık hata ⭐
- `PeriodicTimer`
- Exception olursa ne olur: `BackgroundServiceExceptionBehavior`
- `Channel<T>` ile in-process kuyruk (→ M4.7)
- Kalıcı job'lar: **Hangfire** (dashboard, retry), **Quartz.NET** (cron, cluster), TickerQ, Coravel
- Çoklu instance'ta aynı job'un iki kez çalışması → distributed lock (→ S3.4)
- Worker Service template (`dotnet new worker`)
- 🛠 **Kod:** Outbox tablosunu periyodik tarayan worker (→ S6.3'ün temeli) + Hangfire ile recurring job.

#### M3.7 Hosting, Reverse Proxy & Health Checks · `07-hosting-health`
- Kestrel yapılandırması (limitler, endpoint'ler, HTTP/2-3)
- Reverse proxy arkasında çalışma (nginx, IIS, YARP); **`ForwardedHeaders`** middleware — gerçek IP ve scheme
- **Health checks:** `/health/live` vs `/health/ready`, bağımlılık kontrolleri (DB, Redis), Kubernetes probe eşlemesi
- `AddRequestTimeouts`, `AddRequestDecompression`, response compression
- 🛠 **Kod:** YARP arkasında çalışan API; forwarded headers olmadan ve olarak loglanan IP; liveness/readiness ayrımlı health check'ler.

🎯 **Mülakat:** "`IOptions`, `IOptionsSnapshot`, `IOptionsMonitor` farkı?" · "Captive dependency nedir, nasıl tespit edilir?" · "Middleware'e scoped servis nasıl enjekte edilir?" · "Middleware mi filter mı?" · "BackgroundService içinde DbContext nasıl kullanılır?" · "Graceful shutdown nasıl sağlanır?" · "Liveness ile readiness farkı?"

---

### M4. Concurrency & Async Derinlemesine

> 📁 `02-mid/04-concurrency-async/` · ⏱ ~3 hafta · Önkoşul: J4
> Mid mülakatlarının en ayırt edici bölümü. "Tanımı biliyor mu" değil, **"production'da yaşadı mı"** ölçülür. Bellek modeli, lock-free ve dağıtık kilitleme → S3.

#### M4.1 Thread & ThreadPool · `01-thread-threadpool`
- `Thread` ile manuel thread — ne zaman hâlâ gerekli (nadiren); foreground vs background thread
- Thread maliyeti: stack (varsayılan 1 MB), context switch
- ThreadPool: work queue (global + local), worker thread, I/O completion
- **Thread injection / hill-climbing** — pool'un neden yavaş büyüdüğü
- **Thread pool starvation** ⭐ — sebepleri (sync-over-async), belirtileri (artan gecikme), teşhis (`dotnet-counters`: queue length, thread count)
- `ThreadPool.SetMinThreads` — ne zaman, neden dikkatli
- `[ThreadStatic]`, `ThreadLocal<T>`, **`AsyncLocal<T>`** — farkları
- 🛠 **Kod:** `.Result` kullanan endpoint'e yük testi (k6/Bombardier) → starvation'ı `dotnet-counters` ile gözlemle → async'e çevirip tekrar ölç.

#### M4.2 Task & TPL · `02-task-tpl`
- Task bir thread **değildir** ⭐
- `Task.Run` vs `Task.Factory.StartNew` (`LongRunning`, `Unwrap` tuzağı)
- **`TaskCompletionSource<T>`** — callback/event API'sini Task'e çevirme
- `Task.WhenAll` / `WhenAny` / **`WhenEach`** (.NET 9)
- `WhenAll`'da exception davranışı: `AggregateException`, `await` sadece ilkini fırlatır
- **`ValueTask<T>`** — ne zaman, **tek kez await kuralı** ⭐
- Fire-and-forget tehlikesi ve güvenli yöntemleri; unobserved task exception
- `Task.WaitAsync(timeout)`
- 🛠 **Kod:** Event tabanlı eski bir API'yi `TaskCompletionSource` ile sarmalama; `WhenAll` exception toplama; `ValueTask`'ın iki kez await edilince bozulması.

#### M4.3 `async`/`await` İç Yapısı · `03-async-internals`
- Derleyicinin ürettiği **state machine** (sharplab.io ile)
- `await` noktasında ne olur: thread serbest bırakılır, continuation kaydedilir
- **`SynchronizationContext`** ve **deadlock senaryosu** ⭐ — `.Result` + UI/eski ASP.NET context'i; ASP.NET Core'da context yok → deadlock değil ama starvation var
- **`ConfigureAwait(false)`** — kütüphanede neden, uygulama kodunda neden gereksiz; `ConfigureAwaitOptions` (.NET 8)
- Sync-over-async ve async-over-sync anti-pattern'leri
- Async exception yayılımı ve stack trace
- Async metodların allocation maliyeti
- **Runtime async (.NET 11, önizleme)** — state machine yerine runtime desteği; ne değişecek
- 🛠 **Kod:** WinForms/console'da `SynchronizationContext` ile klasik deadlock reprodüksiyonu ve çözümü; async state machine'in elle yazılmış eşdeğeri.

#### M4.4 Senkronizasyon Primitifleri · `04-synchronization`

| Primitif | Kullanım | Async destekler mi |
|----------|----------|--------------------|
| `lock` / `Monitor` | Process içi karşılıklı dışlama | ❌ |
| `System.Threading.Lock` (C# 13) | Modern, daha hızlı `lock` nesnesi | ❌ |
| `Mutex` | Process'ler arası | ❌ |
| `SemaphoreSlim` | Process içi, N eşzamanlı | ✅ `WaitAsync` |
| `ReaderWriterLockSlim` | Çok okuma / az yazma | ❌ |
| `Interlocked` | Atomik sayaç/flag | — |
| `SpinLock` | Çok kısa kritik bölüm | ❌ |
| `CountdownEvent`, `Barrier`, `ManualResetEventSlim` | Sinyalleşme, fazlı algoritma | ❌ |

- **`lock` içinde `await` neden kullanılamaz** ⭐ ve çözümü (`SemaphoreSlim`)
- `lock(this)`, `lock(typeof(X))`, `lock("str")` neden yanlış
- Lock granularity: coarse vs fine-grained
- `Lazy<T>` ve `LazyThreadSafetyMode`
- 🛠 **Kod:** Aynı paylaşılan kaynağı `lock`, `SemaphoreSlim`, `Interlocked` ve `ReaderWriterLockSlim` ile koruyan dört versiyon + BenchmarkDotNet karşılaştırması.

#### M4.5 Concurrent Collections · `05-concurrent-collections`
- `ConcurrentDictionary` — **`GetOrAdd` factory'si birden fazla kez çalışabilir** ⭐ (`Lazy<T>` ile çözüm)
- `ConcurrentQueue`, `ConcurrentStack`, `ConcurrentBag`, `BlockingCollection`
- Concurrent koleksiyon ≠ thread-safe kod — **compound operation** problemi (check-then-act)
- `ImmutableDictionary`/`FrozenDictionary` ile copy-on-write ve okuma ağırlıklı senaryolar
- 🛠 **Kod:** `GetOrAdd` factory'sinin iki kez çalıştığını gösteren test + `Lazy` ile düzeltme; check-then-act race condition'ı.

#### M4.6 Parallel & PLINQ · `06-parallel-plinq`
- `Parallel.For`, `Parallel.ForEach`, **`Parallel.ForEachAsync`** (async iş için doğru araç)
- `MaxDegreeOfParallelism` — neden sınırlamalı
- PLINQ: `AsParallel()`, `WithDegreeOfParallelism`, `AsOrdered()`; ne zaman **yavaşlatır**
- Thread-local state ile aggregation
- **False sharing** ve cache line
- 🛠 **Kod:** 1000 URL'yi `Parallel.ForEachAsync` ile sınırlı eşzamanlılıkla indiren araç; PLINQ'in küçük koleksiyonda yavaşladığını gösteren benchmark.

#### M4.7 Channels & Producer/Consumer · `07-channels`
- `System.Threading.Channels` — modern producer/consumer
- `CreateUnbounded` vs `CreateBounded` — **backpressure** ⭐
- `BoundedChannelFullMode`: `Wait`, `DropOldest`, `DropNewest`, `DropWrite`
- `ReadAllAsync` + `await foreach`; single reader/writer optimizasyonu
- `Channel` vs `BlockingCollection` vs `ConcurrentQueue`
- 🛠 **Kod:** API'den gelen olayları bounded channel'a yazıp arka planda batch halinde DB'ye yazan pipeline; backpressure davranışının testi.

#### M4.8 Cancellation & Timeout Derinlemesine · `08-cancellation-timeouts`
- `CreateLinkedTokenSource` — birden fazla iptal kaynağını birleştirme
- `ThrowIfCancellationRequested` vs `IsCancellationRequested`
- `CancellationTokenSource` dispose ve `Register` callback sızıntısı
- Timeout katmanları: `HttpClient.Timeout`, `CancelAfter`, `Task.WaitAsync`, ASP.NET Core request timeout middleware
- İptal edilemeyen işi "terk etmek" vs "durdurmak" farkı
- 🛠 **Kod:** İstemci iptali + sunucu timeout'u + shutdown token'ını birleştiren linked token örneği.

🎯 **Mülakat:** "Bir endpoint yük altında yavaşlıyor, thread pool starvation'dan şüpheleniyorsun — nasıl doğrular ve çözersin?" ⭐ · "`ConfigureAwait(false)` ne yapar?" · "`lock` içinde neden `await` olmaz?" · "`ValueTask` ne zaman?" · "`ConcurrentDictionary.GetOrAdd` thread-safe mi?" · "Backpressure nedir?"

---

### M5. API Tasarımı & Protokoller

> 📁 `02-mid/05-api-design/` · ⏱ ~3.5 hafta · Önkoşul: J7, M3
> Bir API'yi tasarlayabilmek, evrimleştirebilmek ve doğru protokolü seçebilmek. Gateway/BFF ve ölçekte API → S6.5, S10.
> Modülün en büyüğü; üç blok hâlinde okunur: **tasarım & evrim** (M5.1-M5.4) · **protokol seçimi** (M5.5-M5.7) · **taşıma, serialization & sertleştirme** (M5.8-M5.11).
> Zaman kısıtlıysa M5.1, M5.2 ve M5.9 mülakatta en çok karşılığı olanlar.

#### M5.1 REST Tasarımı Derinlemesine · `01-rest-advanced`
- REST kısıtları; **Richardson Maturity Model** (Level 0 → 3 / HATEOAS)
- **Pagination:** offset/limit vs **cursor (keyset)** — büyük veri setinde neden cursor ⭐
- Filtering, sorting, sparse fieldsets, arama
- **Idempotency key** ile POST'u güvenle tekrarlanabilir yapmak ⭐
- **Conditional requests:** `ETag` + `If-None-Match` (→ `304`), `If-Match` ile optimistic concurrency (→ `412`)
- Bulk/batch endpoint tasarımı; kısmi başarı
- **Long-running operation:** `202 Accepted` + status endpoint / callback
- Tüm status code'lar: `409`, `412`, `422`, `429`, `502`/`503`/`504` farkları
- API tasarım kılavuzları: Microsoft REST API Guidelines, Google AIP, Zalando
- 🛠 **Kod:** Cursor pagination, ETag/If-Match ve Idempotency-Key destekli sipariş API'si + her davranışın testi.

#### M5.2 API Versioning & Evrim · `02-versioning`
- Breaking vs non-breaking değişiklik; tolerant reader
- Stratejiler: URL path (`/v1/`), query string, header, media type
- `Asp.Versioning.Http` / `Asp.Versioning.Mvc`
- Deprecation: `Sunset` ve `Deprecation` header'ları, emeklilik politikası
- OpenAPI'de çoklu versiyon
- 🛠 **Kod:** v1 ve v2'si birlikte yaşayan endpoint grubu; v1'de deprecation header'ları.

#### M5.3 OpenAPI, Contract-First & Client Üretimi · `03-openapi-contract-first`
- Code-first vs **contract-first** yaklaşım
- `Microsoft.AspNetCore.OpenApi` ileri kullanım: document/operation/schema transformer'ları; OpenAPI 3.1 (.NET 10), **OpenAPI 3.2 (.NET 11)**
- Client üretimi: **Kiota**, NSwag, Refit (elle yazılan arayüz)
- Spec linting (Spectral) ve **breaking change tespiti** (oasdiff) CI'da
- 🛠 **Kod:** Spec'ten Kiota ile client üretip başka bir servisten çağırma; CI'da breaking change'i yakalayan adım.

#### M5.4 Endpoint Organizasyonu: REPR & Feature Klasörleri · `04-endpoint-organization`
- Controller şişmesi problemi
- **REPR pattern** (Request–Endpoint–Response)
- Minimal API'yi modüler organize etme: extension method'lar, `IEndpointRouteBuilder`, Carter
- **FastEndpoints**
- Feature klasörü yapısı ve vertical slice ile ilişkisi (→ M11.4)
- 🛠 **Kod:** Aynı API'nin (a) şişkin controller, (b) REPR + feature klasörleri versiyonu.

#### M5.5 gRPC · `05-grpc`
- Protocol Buffers, `.proto`, contract-first kod üretimi
- HTTP/2 üzerine kurulu olması
- **Dört çağrı tipi:** unary, server streaming, client streaming, bidirectional
- Interceptor, deadline, cancellation, `RpcException` ve status code'lar
- gRPC-Web ve **JSON transcoding**
- Protobuf şema evrimi (field number kuralları)
- **REST vs gRPC karar tablosu** — iç servis iletişimi vs public API
- 🛠 **Kod:** Stok servisi (gRPC) + sipariş servisi (client); dört çağrı tipinin hepsi; JSON transcoding ile REST uç noktası.

#### M5.6 GraphQL · `06-graphql`
- Over-fetching/under-fetching problemi; schema, query, mutation, subscription
- **HotChocolate**
- Resolver ve **N+1 → DataLoader** ⭐
- Filtering, sorting, projection; Relay cursor pagination
- **Güvenlik:** depth limit, complexity analysis, persisted queries, introspection'ı kapatma
- Federation / schema stitching
- **Ne zaman GraphQL, ne zaman REST** — dürüst trade-off
- 🛠 **Kod:** Ürün kataloğu GraphQL API'si; DataLoader'sız ve DataLoader'lı SQL sayısı karşılaştırması.

#### M5.7 Real-time: SignalR, WebSockets & SSE · `07-realtime`
- WebSocket handshake ve upgrade; ham WebSocket vs SignalR
- SignalR Hub, `IHubContext`, strongly-typed hub, group/user mesajlaşma
- Transport fallback: WebSocket → SSE → long polling
- **Scale-out:** Redis backplane, Azure SignalR Service; sticky session gereksinimi
- **Server-Sent Events** — .NET 10'da yerleşik (`TypedResults.ServerSentEvents`); LLM streaming yanıtları için de kullanılır
- Karar tablosu: SSE vs WebSocket vs SignalR vs polling
- 🛠 **Kod:** Sipariş durumunu canlı gösteren SignalR hub (Redis backplane ile iki instance) + aynı akışın SSE versiyonu.

#### M5.8 Serialization · `08-serialization`
- `System.Text.Json` vs `Newtonsoft.Json` — farklar ve göç tuzakları
- `JsonSerializerOptions`, naming policy, enum converter, custom `JsonConverter<T>`
- **Source-generated serialization** (`JsonSerializerContext`) — AOT ve performans
- Polymorphic serialization (`[JsonDerivedType]`)
- `JsonNode` / `JsonDocument` / `Utf8JsonReader` — hangisi ne zaman
- Tuzaklar: `DateTime` vs `DateTimeOffset`, `decimal` hassasiyeti ve JavaScript `Number` sınırı, döngüsel referans
- Binary alternatifler: MessagePack, Protobuf
- 🛠 **Kod:** Reflection vs source-gen serializer benchmark'ı; polymorphic event serialization.

#### M5.9 HttpClient & Resilience · `09-http-resilience`
- `IHttpClientFactory` ileri: handler ömrü, DNS değişikliği, `SocketsHttpHandler.PooledConnectionLifetime`
- **`Microsoft.Extensions.Http.Resilience`** (Polly v8 tabanlı): `AddStandardResilienceHandler()`
  - Retry + **exponential backoff + jitter**
  - **Circuit breaker** (closed / open / half-open)
  - Timeout (attempt ve toplam)
  - Rate limiter, **hedging**
- Retry'ın tehlikesi: idempotent olmayan istek, retry storm
- **Refit** ile declarative client
- Dış servis hatasını domain hatasına çevirme
- Test: `DelegatingHandler`, **WireMock.Net**
- 🛠 **Kod:** Kararsız bir sahte servise (WireMock) karşı standart resilience pipeline'ı; circuit breaker'ın açılıp kapandığını gösteren test.

#### M5.10 Rate Limiting & API Sertleştirme · `10-rate-limiting-hardening`
- `Microsoft.AspNetCore.RateLimiting`: fixed window, sliding window, token bucket, concurrency limiter
- Partition key ile kullanıcı/IP/tenant bazlı limit; `429` + `Retry-After`
- Tek instance limiti vs dağıtık rate limiting (Redis → S10.4)
- CORS'u doğru yapılandırma, güvenlik header'ları (HSTS, CSP, `X-Content-Type-Options`)
- Request boyutu limiti, request timeout, yavaş istemci (slowloris) koruması
- **.NET 11:** cross-origin unsafe isteklerin otomatik reddi (yerleşik CSRF koruması)
- 🛠 **Kod:** Kullanıcı bazlı token bucket + anonim kullanıcılar için IP bazlı fixed window; yük testinde `429` davranışı.

#### M5.11 Protokol & Transport Derinlemesine · `11-protocols-transport` ⭐
> J1.2'deki TCP/UDP ve J1.3'teki HTTP temelinin üzerine: protokol sürümleri arasındaki gerçek farklar ve bunların .NET'teki sonuçları. "Neden HTTP/3?" ve "socket exhaustion neden olur?" sorularının tam cevabı.

- **HTTP/1.1:** persistent connection, keep-alive, pipelining'in neden pratikte başarısız olduğu; **tarayıcı başına domain bağlantı limiti** ve bunun doğurduğu "domain sharding" / "sprite" hack'leri
- **HTTP/2:** tek bağlantı üzerinde **multiplexing** (stream'ler), binary framing, **HPACK** header sıkıştırma, server push (neden terk edildi); **TCP seviyesinde head-of-line blocking devam eder** ⭐ — paket kaybında tüm stream'ler beklemek zorundadır
- **HTTP/3 & QUIC:** UDP üzerine kurulu, **stream başına bağımsız teslim** → HOL blocking çözülür; TLS 1.3'ün protokole gömülü olması; **0-RTT / 1-RTT bağlantı kurulumu**, connection migration (ağ değişince bağlantının kopmaması); .NET'te HTTP/3 desteği ve Kestrel'de etkinleştirme (→ M3.7)
- **Protokol seçim tablosu:** HTTP/1.1 (uyumluluk, basit araçlar), HTTP/2 (gRPC, çok sayıda paralel istek), HTTP/3 (mobil/kayıplı ağ, yüksek gecikme)
- **TLS derinlemesine:** handshake adımları ve **RTT maliyeti** (TLS 1.2 → 2 RTT, TLS 1.3 → 1 RTT), cipher suite, ALPN ile protokol seçimi, SNI; **session resumption** ve ticket; sertifika zinciri doğrulama, OCSP/CRL, sertifika pinning (ve riskleri)
- **mTLS (mutual TLS)** — servisler arası kimlik doğrulama; .NET'te client certificate ile `HttpClient` ve Kestrel tarafında doğrulama (→ S6.5 service mesh, S7)
- **Bağlantı yönetimi .NET'te** ⭐
  - `SocketsHttpHandler`: `MaxConnectionsPerServer`, `PooledConnectionLifetime`, `PooledConnectionIdleTimeout`, `ConnectTimeout`
  - **Socket exhaustion'ın tam zinciri:** `new HttpClient()` her seferinde → her biri yeni bağlantı havuzu → kapanan bağlantılar `TIME_WAIT`'te birikir → ephemeral port tükenir (→ J1.2, M5.9)
  - Ters problem: `HttpClient`'ı singleton tutup `PooledConnectionLifetime` ayarlamamak → **DNS değişikliklerinin görülmemesi**
  - .NET'te DNS çözümleme ve cache davranışı
- **Gecikme bütçesi düşünmek:** DNS + TCP + TLS + sunucu işleme + aktarım; bir isteğin nerede zaman harcadığını `curl -w` ve tracing ile ayrıştırma (→ M13)
- Compression: gzip vs Brotli vs **Zstandard (.NET 11)**; `Accept-Encoding` müzakeresi; **BREACH** riski nedeniyle hassas yanıtlarda dikkat
- L4 vs L7 load balancer'ın protokole etkisi: HTTP/2 bağlantılarının tek bağlantıda kalması nedeniyle **gRPC'de yük dağılımı problemi** ve çözümleri (client-side LB, proxy, `MaxConnectionsPerServer`)
- 🛠 **Kod:** Aynı API'yi HTTP/1.1, HTTP/2 ve HTTP/3 üzerinden çağırıp 100 paralel istekte süre/bağlantı sayısı karşılaştırması (`SocketsHttpHandler` + `ss` ile bağlantı gözlemi); `new HttpClient()` ile `IHttpClientFactory` kullanımının ephemeral port tüketimini yan yana ölçen deney; `curl -w` ile DNS/TCP/TLS/TTFB kırılımı; mTLS ile korunan bir endpoint ve sertifikasız istemcinin reddedilmesi.

🎯 **Mülakat:** "Offset yerine cursor pagination neden?" · "POST'u nasıl idempotent yaparsın?" · "API'yi breaking change olmadan nasıl evrimleştirirsin?" · "REST mi gRPC mi GraphQL mi?" · "GraphQL'de N+1 nasıl çözülür?" · "SignalR'ı birden fazla instance'a nasıl ölçeklersin?" · "HTTP/2 ile HTTP/3 farkı, HTTP/3 neden UDP üzerine kuruldu?" ⭐ · "HTTP/2'de head-of-line blocking çözüldü mü?" · "Socket exhaustion neden olur, nasıl önlenir?" ⭐ · "Retry neden jitter ister?" · "Circuit breaker nasıl çalışır?"

---

### M6. SQL & PostgreSQL Derinlemesine

> 📁 `02-mid/06-sql-postgresql/` · ⏱ ~3 hafta · Önkoşul: J8, J9
> Altyapı (repo köküne eklenecek `docker-compose.yml` servisleri): PostgreSQL 18 (+ isteğe bağlı SQL Server).
> J8'deki kavramların **mekanizması**: plan nasıl seçilir, MVCC nasıl çalışır, kilitler neden çakışır. Ölçek (replication, sharding, storage engine) → S4.

#### M6.1 İleri SQL · `01-advanced-sql`
- **Window function'lar:** `ROW_NUMBER`, `RANK`, `DENSE_RANK`, `NTILE`, `LAG`/`LEAD`, `FIRST_VALUE`; running total; frame tanımı (`ROWS BETWEEN ...`)
- CTE ve **recursive CTE** (kategori ağacı, organizasyon şeması)
- Upsert: `INSERT ... ON CONFLICT DO UPDATE` (PostgreSQL), `MERGE` (SQL Server, PostgreSQL 15+) ve bilinen tuzakları
- `LATERAL` join, `DISTINCT ON`, `RETURNING`
- Klasik problemler: top-N per group, gaps & islands, deduplication, pivot
- **Set-based düşünme vs satır satır (cursor/döngü)** ⭐
- **Programatik SQL: stored procedure, function, trigger** ⭐
  - PostgreSQL'de `FUNCTION` vs `PROCEDURE` (procedure transaction kontrolü yapabilir), `plpgsql`; SQL Server'da stored procedure, scalar vs **table-valued function** (scalar UDF'in satır başına çalışıp planı bozması)
  - .NET'ten çağırma: EF Core `FromSql`/`ExecuteSql`, Dapper ile `CommandType.StoredProcedure`, table-valued parameter (→ M7.3)
  - **Trigger'ın gerçek maliyeti:** gizli yan etki (kodu okuyan geliştirici göremez), hata ayıklama zorluğu, recursive/zincirleme trigger, toplu işlemde performans, `TRUNCATE`'te çalışmaması
  - **"İş mantığı nerede yaşamalı?"** ⭐ — veritabanında (performans, tek giriş noktası, eski sistemler) vs uygulamada (test edilebilirlik, versiyonlama, taşınabilirlik, code review); kurumsal .NET dünyasında hâlâ canlı bir tartışma ve mülakat sorusu
  - Stored procedure'ün versiyonlanması: migration'a dahil etme zorunluluğu (→ M6.6)
  - Ne zaman meşru: ağır veri işleme (satır trafiğini azaltma), batch ETL, güvenlik sınırı (kullanıcıya tabloya değil procedure'e yetki verme)
- 🛠 **Kod:** 25 ileri seviye SQL problemi (top-N per group, running total, hiyerarşi, gaps & islands) + çözümleri + sonuçları doğrulayan .NET test runner'ı; aynı iş kuralını (stok düşme) bir kez trigger ile bir kez uygulama katmanında yazıp test edilebilirlik ve hata ayıklama deneyimini karşılaştırma.

#### M6.2 Index Stratejisi & Execution Plan · `02-indexing-execution-plans` ⭐
- **Plan okuma:** `EXPLAIN (ANALYZE, BUFFERS)` — Seq Scan, Index Scan, Index Only Scan, Bitmap Heap Scan
- **Join algoritmaları:** nested loop, hash join, merge join — optimizer hangisini ne zaman seçer
- Estimated vs actual rows; **istatistikler**, `ANALYZE`, cardinality tahmini hataları
- Index türlerinin kullanımı: **covering index** (`INCLUDE`), **partial index** (`WHERE status = 'active'`), **expression index** (`LOWER(email)`), unique index
- Composite index tasarımı: önce eşitlik kolonları, sonra aralık (range) kolonları
- **SARGability** ⭐ — `WHERE EXTRACT(YEAR FROM created_at) = 2026` yerine aralık sorgusu
- Implicit conversion nedeniyle index kaybı
- **Keyset pagination** vs derin `OFFSET`
- Index bakımı: bloat, kullanılmayan index tespiti (`pg_stat_user_indexes`), `REINDEX CONCURRENTLY`
- Yavaş sorgu keşfi: **`pg_stat_statements`**, `auto_explain`, `log_min_duration_statement`
- 🛠 **Kod:** "Yavaş sorgu kliniği" — 10 yavaş sorgu; her biri için plan analizi, index veya yeniden yazımla düzeltme, öncesi/sonrası ölçüm raporu.

#### M6.3 Transaction, Isolation & Locking Derinlemesine · `03-transactions-isolation` ⭐
- **Isolation level'lar ve engelledikleri anomaliler:**

  | Level | Dirty Read | Non-repeatable Read | Phantom Read | Lost Update | Write Skew |
  |-------|:---:|:---:|:---:|:---:|:---:|
  | Read Uncommitted | olur | olur | olur | olur | olur |
  | Read Committed | ❌ | olur | olur | olur | olur |
  | Repeatable Read | ❌ | ❌ | standartta olur* | ❌* | olur |
  | Snapshot (MVCC) | ❌ | ❌ | ❌ | ❌ | **olur** |
  | Serializable | ❌ | ❌ | ❌ | ❌ | ❌ |

  <sub>* PostgreSQL'in Repeatable Read'i aslında Snapshot Isolation'dır: phantom'ı da engeller, lost update'i serialization hatasıyla durdurur.</sub>

- **MVCC nasıl çalışır:** PostgreSQL'de satır versiyonları (`xmin`/`xmax`), snapshot; SQL Server'da varsayılan locking vs `READ_COMMITTED_SNAPSHOT`
- PostgreSQL Serializable = **SSI** — serialization failure → uygulama **retry** etmeli
- **Write skew** örneği: "nöbette en az bir doktor kalmalı"
- Kilitler: row-level (`FOR UPDATE`, `FOR SHARE`, `FOR NO KEY UPDATE`), table-level, **advisory lock**; SQL Server lock escalation
- **`SELECT ... FOR UPDATE SKIP LOCKED`** — veritabanını iş kuyruğu olarak kullanma ⭐
- **Deadlock:** oluşumu, tespiti, kilit sıralamasıyla önleme, retry
- **Optimistic** (rowversion / `xmin` / version kolonu) **vs pessimistic** — hangisi ne zaman ⭐
- `TransactionScope` ve async (`TransactionScopeAsyncFlowOption.Enabled`); dağıtık transaction'dan kaçınma (→ S6.3 outbox)
- Retry: EF Core execution strategy (`EnableRetryOnFailure`) ve kullanıcı transaction'larıyla birlikte kullanım tuzağı
- Uzun transaction'ların zararı: bloat, kilit çekişmesi
- 🛠 **Kod:** Her anomaliyi (dirty read, non-repeatable read, phantom, lost update, write skew) iki eşzamanlı bağlantıyla reprodükleyen test suite'i — her isolation level'da hangisinin engellendiğini tabloya döken program; `SKIP LOCKED` ile çok worker'lı iş kuyruğu.

#### M6.4 PostgreSQL Derinlemesine · `04-postgresql-deep-dive`
- Mimari: **process-per-connection** → connection pooling zorunluluğu (Npgsql pool, **PgBouncer**, PgCat); `max_connections`
- MVCC'nin sonuçları: **VACUUM** / autovacuum, table bloat, HOT update, transaction ID wraparound
- **Index türleri:** B-tree, **GIN** (jsonb, array, full-text), GiST (geo, range), **BRIN** (devasa zaman serisi tablolar), Hash — hangisi ne zaman
- **JSONB:** `->`, `->>`, `@>`, `?` operatörleri, GIN index, jsonpath; JSONB mi kolon mu
- Array, enum, **range tipleri** (`tstzrange`) + **exclusion constraint** (çakışan rezervasyonu DB seviyesinde engelleme)
- Generated column'lar (stored / virtual)
- Full-text search (`tsvector`, `tsquery`), **`pg_trgm`** ile fuzzy arama
- **Declarative partitioning** (range/list/hash), partition pruning
- Eklentiler: **pgvector** (AI), PostGIS, TimescaleDB, `pg_cron`, `pg_stat_statements`
- **Row-Level Security** ile multi-tenancy
- `LISTEN`/`NOTIFY`
- Replication'a giriş: streaming vs logical; read replica (derin → S4.2)
- **PostgreSQL 18:** asenkron I/O, `uuidv7()`, virtual generated columns, B-tree skip scan, `RETURNING`'de `OLD`/`NEW`, OAuth kimlik doğrulama desteği
- Npgsql'e özel: `NpgsqlDataSource`, tip eşlemeleri, **binary `COPY` ile bulk import** ⭐
- 🛠 **Kod:** Toplantı odası rezervasyonu (`tstzrange` + exclusion constraint ile çakışma önleme); JSONB ürün özellikleri + GIN index; `pg_trgm` ile typo toleranslı arama; `COPY` ile 1M satır import vs tek tek insert karşılaştırması; RLS ile tenant izolasyonu.

#### M6.5 SQL Server & Azure SQL Farkları · `05-sql-server`
- Kurumsal .NET dünyasında hâlâ en yaygın veritabanı — farkları bilmek gerekir
- T-SQL'e özel: `IDENTITY`, `NEWSEQUENTIALID()`, `TOP` vs `LIMIT`, `MERGE` sorunları
- Clustered index = tablonun kendisi; **key lookup**; included columns
- Execution plan (SSMS), **Query Store**
- **Parameter sniffing** ⭐ — belirtileri ve çözümleri (`OPTION (RECOMPILE)`, `OPTIMIZE FOR`, SQL Server 2022 PSP optimization)
- Varsayılan locking davranışı vs RCSI; deadlock graph
- Temp table vs table variable vs CTE
- Temporal table, columnstore index
- SQL Server 2025 / Azure SQL: native `json` tipi, **`VECTOR` tipi** (→ M15.4)
- PostgreSQL ↔ SQL Server göç tuzakları: case sensitivity, identifier quoting, tarih tipleri, `IDENTITY` vs `SERIAL`
- 🛠 **Kod:** Aynı şema ve sorgu setini iki veritabanında çalıştıran karşılaştırma projesi; parameter sniffing reprodüksiyonu ve çözümü.

#### M6.6 Şema Migration Stratejileri · `06-schema-migrations`
- Araçlar: EF Core Migrations, **migration bundle** (`dotnet ef migrations bundle`), DbUp, FluentMigrator, Flyway, Liquibase
- **Migration'ı kim uygular** ⭐ — uygulama startup'ı (çoklu instance'ta yarış riski) vs pipeline adımı vs Kubernetes init container
- **Zero-downtime: expand → migrate → contract** ⭐
- Kolon yeniden adlandırma/silme işlemini birden fazla deployment'a yaymak
- `NOT NULL` kolon ekleme, büyük tabloda `CREATE INDEX CONCURRENTLY`, `lock_timeout`
- Büyük veri backfill'ini batch'ler halinde yapmak
- Forward-only migration felsefesi ve rollback planı
- 🛠 **Kod:** Bir kolonu üç deployment'ta sıfır kesintiyle yeniden adlandıran migration seti + EF migration bundle'ını CI/CD'de çalıştıran pipeline adımı.

🎯 **Mülakat:** "Index var ama planda Seq Scan görüyorsun — neden olabilir?" ⭐ · "MVCC nasıl çalışır?" · "Lost update ve write skew nedir, nasıl önlenir?" · "Optimistic mi pessimistic mi?" · "`SKIP LOCKED` ne işe yarar?" · "VACUUM neden gerekli?" · "Parameter sniffing nedir?" · "Production'da bir kolonu kesintisiz nasıl yeniden adlandırırsın?"

---

### M7. EF Core Derinlemesine & Veri Erişim Desenleri

> 📁 `02-mid/07-data-access-advanced/` · ⏱ ~2 hafta · Önkoşul: J9, M6

#### M7.1 EF Core Performansı · `01-ef-performance`
- N+1 tespiti: SQL logları, interceptor, MiniProfiler
- **Cartesian explosion** → `AsSplitQuery()` vs tek sorgu ⭐
- `Include` yerine projection; `AsNoTracking`, `AsNoTrackingWithIdentityResolution`
- Compiled query (`EF.CompileAsyncQuery`), compiled model
- **`DbContext` pooling** (`AddDbContextPool`), `IDbContextFactory` (Blazor, background service)
- **`ExecuteUpdate` / `ExecuteDelete`** — entity yüklemeden toplu işlem (EF 10'da normal lambda ile `ExecuteUpdate`)
- Bulk insert: batching, EFCore.BulkExtensions, Npgsql binary `COPY`
- `TagWith` ile sorguyu trace'lerde bulunabilir yapmak
- **EF Core performans anti-pattern checklist'i**
- 🛠 **Kod:** Performans "öncesi/sonrası" laboratuvarı — her anti-pattern için BenchmarkDotNet ölçümü ve üretilen SQL.

#### M7.2 EF Core İleri Modelleme · `02-ef-advanced-modeling`
- Owned types vs **complex types**; EF 10'da complex type'ların JSON'a eşlenmesi
- JSON kolonları (`ToJson()`), PostgreSQL `jsonb` eşlemesi
- Inheritance: **TPH, TPT, TPC** — trade-off ⭐
- Value converter, value comparer; **strongly-typed ID**'ler
- **Global query filters** — soft delete, multi-tenancy; **named query filters (EF 10)** ile tek tek kapatılabilir filtreler
- Concurrency token'ları; Npgsql'de `xmin`
- **Interceptor'lar:** `SaveChangesInterceptor` ile audit alanları ve soft delete; `DbCommandInterceptor`
- Raw SQL: `FromSql`, `SqlQuery<T>`, `ExecuteSql` — interpolated string'in güvenli parametreleştirmesi
- Shadow property, backing field (DDD dostu modelleme → S5.1)
- Temporal tables, spatial data
- 🛠 **Kod:** Multi-tenant + soft delete'li domain; named query filter'larla birini kapatıp diğerini açık tutma; strongly-typed ID converter'ları; audit interceptor.

#### M7.3 Dapper & Raw SQL · `03-dapper`
- Micro-ORM felsefesi; ne zaman EF Core yerine Dapper
- `Query`, `QueryFirstOrDefault`, `Execute`; multi-mapping (`splitOn`), `QueryMultiple`
- `DynamicParameters`, stored procedure, table-valued parameter
- **Hibrit kullanım:** yazma EF Core, karmaşık okuma Dapper (CQRS'e doğal uyum)
- Dapper.AOT
- 🛠 **Kod:** Aynı rapor sorgusunu EF Core LINQ, EF raw SQL ve Dapper ile yazıp benchmark'lama.

#### M7.4 Object Mapping · `04-object-mapping`
- **Manuel mapping** — varsayılan tercih: en hızlı, en açık, derleme zamanı güvenli
- **Mapperly** — source generator, AOT uyumlu, sıfır runtime maliyeti
- **AutoMapper** — konvansiyon tabanlı; **2025'ten itibaren ticari lisans (v15+)**; runtime maliyeti ve "gizli" mapping hataları eleştirisi
- Mapster alternatifi
- DB seviyesinde projection (`Select`, `ProjectTo`)
- 🛠 **Kod:** Aynı mapping'i manuel, Mapperly ve AutoMapper ile yazıp benchmark + Mapperly'nin ürettiği kodun incelenmesi.

#### M7.5 Repository, Unit of Work & Specification · `05-repository-specification`
- Repository pattern — ne zaman faydalı, ne zaman **gereksiz soyutlama**
- "`DbContext` zaten Repository + Unit of Work" argümanı ve karşı argümanlar
- Generic repository neden çoğu zaman anti-pattern
- **Specification pattern** (Ardalis.Specification)
- Test edilebilirlik: mock'lanmış repository vs Testcontainers ile gerçek DB
- 🛠 **Kod:** Aynı use case'in (a) doğrudan `DbContext`, (b) generic repository, (c) specification pattern versiyonları + test edilebilirlik karşılaştırması.

🎯 **Mülakat:** "`AsSplitQuery` ne zaman kullanılır?" · "EF Core'da 100 bin satırı nasıl güncellersin?" · "EF Core ile Repository pattern gerekli mi?" · "TPH/TPT/TPC farkı?" · "Dapper ne zaman?" · "AutoMapper'ın sorunları neler?"

---

### M8. NoSQL & Caching

> 📁 `02-mid/08-nosql-caching/` · ⏱ ~3 hafta · Önkoşul: J8, M6
> Altyapı (repo köküne eklenecek `docker-compose.yml` servisleri): MongoDB (tek node'lu replica set — transaction için), Redis 8, Elasticsearch/OpenSearch.
> Ölçekte MongoDB/Redis (sharding, cluster) → S4.6.

#### M8.1 NoSQL Veri Modelleme Prensipleri · `01-nosql-modeling`
- **Erişim deseni önce** ⭐ — ilişkisel: önce veriyi modelle, sonra sorgula; NoSQL: önce sorguları listele, sonra modelle
- Embedding vs referencing; one-to-few / one-to-many / one-to-squillions
- Denormalizasyonun bedeli: güncelleme fan-out'u, tutarsızlık penceresi
- Aggregate = tutarlılık birimi (DDD ile bağlantı → S5.1)
- Desenler: bucket, computed, subset, extended reference, outlier, schema versioning
- Single-table design fikri (DynamoDB) — kısa bakış
- Partition/shard key'i ilk günden düşünmek
- 🛠 **Kod:** Blog + yorum + beğeni sistemini aynı erişim deseni listesiyle ilişkisel ve doküman modelinde tasarlayan karşılaştırma + karar dokümanı.

#### M8.2 MongoDB · `02-mongodb`
- Document model, BSON, `_id` ve ObjectId
- **MongoDB.Driver:** `IMongoClient` singleton, `IMongoCollection<T>`, LINQ provider, `Builders<T>`, class map ve konvansiyonlar
- CRUD: `UpdateOne` vs `ReplaceOne`, update operatörleri (`$set`, `$inc`, `$push`, `$addToSet`), upsert, **`FindOneAndUpdate`** (atomik)
- Şema tasarımı: embedded vs referenced — 16 MB doküman limiti, sınırsız büyüyen array anti-pattern'i
- **Aggregation pipeline:** `$match`, `$group`, `$project`, `$lookup`, `$unwind`, `$facet`, `$bucket`; aşama sırasının performansa etkisi
- Index'ler: single, compound (**ESR kuralı: Equality → Sort → Range** ⭐), multikey, text, geospatial, TTL, partial, unique, wildcard
- `explain()` — `COLLSCAN` vs `IXSCAN`
- **Transaction:** replica set gerektirir, maliyetlidir; çoğu zaman tek doküman atomikliği yeterlidir
- Replica set temelleri, **read preference, write concern, read concern** (derin → S4.6)
- Change Streams; schema validation (`$jsonSchema`)
- **EF Core MongoDB provider** — ne zaman driver yerine; MongoDB 8.x ile Community Edition'da search ve vector search
- Atlas vs self-hosted vs Azure Cosmos DB for MongoDB (vCore)
- **Ne zaman MongoDB, ne zaman ilişkisel** ⭐ — dürüst karşılaştırma
- 🛠 **Kod:** Ürün kataloğu + yorum servisi — driver ile CRUD, aggregation ile kategori raporları, ESR'ye göre index + `explain()` kanıtı, change stream ile fiyat değişikliği olayları; aynı modelin EF Core provider versiyonu.

#### M8.3 Redis · `03-redis`
- In-memory, komut yürütmede tek thread'li model — neden hızlı
- **Veri tipleri ve kullanım senaryoları:**

  | Tip | Senaryo |
  |-----|---------|
  | String | Cache, sayaç, basit kilit (`SET NX PX`) |
  | Hash | Nesne saklama, kısmi güncelleme |
  | List | Basit kuyruk |
  | Set | Tekil üyelik, etiketler |
  | **Sorted Set** | Leaderboard, sliding window rate limiting, gecikmeli kuyruk |
  | **Stream** | Event log, consumer group'lu iş kuyruğu |
  | Bitmap / HyperLogLog | Günlük aktif kullanıcı, tekil ziyaretçi tahmini |
  | Geo | Yakındaki kuryeler/mağazalar |
  | JSON + Query Engine | Doküman saklama ve sorgulama (Redis 8'de yerleşik) |
  | Vector Set | Embedding benzerlik araması (Redis 8) |

- **StackExchange.Redis:** `ConnectionMultiplexer` **singleton** kuralı ⭐, `IDatabase`, async, `CommandFlags`
- Key tasarımı (`app:entity:id`), namespace konvansiyonu, key boyutunun bellek maliyeti
- **`KEYS` vs `SCAN`** ⭐ — `KEYS *` tüm sunucuyu **bloklar** (tek thread'li model!) ve production'da asla kullanılmaz; `SCAN` cursor tabanlı, artımlı ve bloklamaz; `HSCAN`/`SSCAN`/`ZSCAN`; `SCAN`'in garantisi (aynı anahtarı iki kez görebilir, baştan sona var olan anahtarı kaçırmaz)
- **TTL ve süre sonu (expiration) semantiği:** `EXPIRE`/`SETEX`/`PERSIST`; **lazy expiration** (erişimde silme) + **active expiration** (arka plan örnekleme) — "TTL geçti ama bellek düşmedi" sorusunun cevabı
- **Eviction policy'leri** (`allkeys-lru`, `allkeys-lfu`, `volatile-ttl`, `volatile-random`, `noeviction`) ⭐ — `maxmemory` ile birlikte; cache için `allkeys-lru`/`lfu`, kuyruk/kalıcı veri için `noeviction`
- Persistence: RDB (snapshot) vs AOF (append-only) vs ikisi birlikte; `fsync` politikası ve veri kaybı penceresi
- **Pipelining vs transaction vs Lua — üçü farklı şeydir** ⭐
  - **Pipelining:** ağ gidiş-dönüşünü (RTT) azaltma → **throughput** için; araya başka istemcinin komutu girebilir, atomiklik yoktur
  - **`MULTI`/`EXEC`:** komutlar kuyruklanır ve araya girilmeden çalışır → **atomiklik** için; **rollback yoktur** (bir komut hata verirse diğerleri yine çalışır), `WATCH` ile optimistic locking
  - **Lua script / Redis Functions:** sunucu tarafında atomik çalışan mantık — "oku-karar ver-yaz" gerektiren işlemler (rate limiter, stok düşme) için tek doğru yol
- **Pub/Sub vs Streams** — Pub/Sub ateşle-unut (abone yoksa mesaj kaybolur, kalıcılık yok), Streams kalıcı log + consumer group + ack
- **Keyspace notifications** — key değişiminde event yayınlama (`notify-keyspace-events`); cache invalidation ve "key süresi doldu" tetikleyicisi için; **güvenilir teslim garantisi yoktur** (Pub/Sub üzerine kurulu), kritik akışlarda Streams tercih edilir
- **Client-side caching (RESP3 tracking)** — sunucunun istemciye "bu key değişti" bildirimi göndermesi; çok katmanlı cache'te L1 (in-process) tutarlılığını sağlamanın Redis-native yolu (→ M8.4 `HybridCache`/FusionCache backplane)
- Dağıtık kilit: `SET key value NX PX` deseni ve **Redlock tartışması** — tek node'da yeterli, çoklu node'da garanti iddiası eleştirilir (→ S3.4)
- Replication, Sentinel, Cluster'a giriş (→ S4.6)
- **2026 durumu:** Redis 8 AGPLv3 ile yeniden açık kaynak; **Valkey** (Linux Foundation forku, AWS/Google destekli); Azure'da **Azure Managed Redis** (Azure Cache for Redis emekliye ayrılıyor); Microsoft'un .NET ile yazılmış Redis uyumlu sunucusu **Garnet**
- 🛠 **Kod:** Leaderboard (sorted set), Lua script ile atomik rate limiter, session store, Redis Streams ile consumer group'lu iş kuyruğu, HyperLogLog ile tekil ziyaretçi sayımı; 100k key'li bir veritabanında `KEYS *` ile `SCAN`'in sunucu üzerindeki etkisini (`SLOWLOG`, latency) karşılaştırma; `MULTI`/`EXEC`'in rollback yapmadığını kanıtlayan test.

#### M8.4 Caching Stratejileri · `04-caching-strategies` ⭐
- Cache katmanları: tarayıcı → CDN → output cache → in-process (`IMemoryCache`) → distributed (Redis) → DB
- `IMemoryCache` (boyut limiti!), `IDistributedCache`, **`HybridCache`** (.NET 9+: L1+L2, stampede koruması, tag ile invalidation)
- **Output caching** (.NET 7+) tag invalidation ile; response caching'den farkı
- Desenler: **cache-aside**, read-through, write-through, write-behind, refresh-ahead
- **Cache invalidation** stratejileri; TTL seçimi, absolute vs sliding expiration
- **Cache stampede / thundering herd** ⭐ — kilit, jitter, erken yenileme
- Negatif cache, cache key tasarımı ve versiyonlama
- Çoklu instance'ta L1 tutarlılığı: backplane ile invalidation (FusionCache)
- Neyi cache'lememeli: kişiye özel hassas veri, çok sık değişen veri
- 🛠 **Kod:** Ürün detay endpoint'ine aşamalı caching: önce hiç → `IMemoryCache` → Redis cache-aside → `HybridCache`; stampede'i 1000 eşzamanlı istekle reprodükle ve `HybridCache` ile çöz; output cache tag invalidation.

#### M8.5 Arama Motorları & Full-Text Search · `05-search`
- `LIKE '%...%'` neden yetmez
- Inverted index, analyzer, tokenizer, stemming (**Türkçe analyzer**), BM25 relevance
- **Elasticsearch / OpenSearch:** index, mapping, query DSL, aggregation/facet; `Elastic.Clients.Elasticsearch`
- Alternatifler: PostgreSQL FTS + `pg_trgm` (çoğu zaman yeterli), Meilisearch, Typesense, Azure AI Search
- DB → arama index'i senkronizasyonu: dual write problemi → outbox/CDC (→ S4.7)
- Hybrid search (keyword + vector) → M15.4
- 🛠 **Kod:** Ürün arama — PostgreSQL FTS versiyonu ve Elasticsearch versiyonu (Türkçe analyzer, facet, typo toleransı) + sonuç kalitesi karşılaştırması.

🎯 **Mülakat:** "MongoDB'de embed mi reference mı — neye göre?" · "ESR kuralı nedir?" · "Redis tek thread'li ise nasıl bu kadar hızlı?" · "Production'da `KEYS *` neden kullanılmaz, alternatifi ne?" ⭐ · "Pipelining ile `MULTI`/`EXEC` farkı?" ⭐ · "Redis transaction'ı rollback yapar mı?" · "Eviction policy nasıl seçilir?" · "Cache stampede nedir, nasıl önlenir?" · "Cache invalidation stratejin ne?" · "`HybridCache` ne çözdü?" · "Redis mi Valkey mi?"

---

### M9. Kimlik & Erişim Yönetimi: OAuth, OIDC, Keycloak

> 📁 `02-mid/09-identity-access/` · ⏱ ~3 hafta · Önkoşul: J11
> Altyapı (repo köküne eklenecek `docker-compose.yml` servisleri): Keycloak 26 + PostgreSQL.
> Junior'da "JWT ile API koru" yeterliydi; Mid'de **akışı uçtan uca anlatmak, doğru IdP'yi seçmek ve güvenli entegre etmek** beklenir.

#### M9.1 OAuth 2.0 → 2.1 Derinlemesine · `01-oauth2`
- Front channel vs back channel; confidential vs public client
- **Authorization Code + PKCE — adım adım** ⭐: `code_verifier`, `code_challenge` (S256), `state`, `redirect_uri` birebir eşleşmesi, code → token takası
- **Client Credentials**; **Device Authorization Grant** (TV, CLI)
- **Refresh token:** rotation, reuse detection, sliding vs absolute ömür
- **Kaldırılan akışlar:** Implicit ve Resource Owner Password — neden
- **OAuth 2.1** konsolidasyonu ve **RFC 9700 (Security BCP)**: her yerde PKCE, birebir redirect URI, URL'de token yok
- **Scope vs audience vs role** ⭐
- Token formatları: **JWT access token (RFC 9068)** vs opak/reference token + **introspection (RFC 7662)**; revocation (RFC 7009)
- İleri: **PAR** (Pushed Authorization Requests), **DPoP** (sender-constrained token), mTLS-bound token, **Token Exchange (RFC 8693)**, RAR
- FAPI 2.0 (bankacılık / open finance profili)
- 🛠 **Kod:** Hiç auth kütüphanesi kullanmadan (sadece `HttpClient`) Authorization Code + PKCE akışını her isteği loglayarak adım adım yürüten console uygulaması; ardından aynı akışın `Microsoft.AspNetCore.Authentication.OpenIdConnect` ile hali.

#### M9.2 OpenID Connect · `02-openid-connect`
- OIDC = OAuth 2.0 + **ID token** + UserInfo + discovery
- ID token claim'leri (`sub`, `aud`, `nonce`, `auth_time`, `acr`, `amr`) ve doğrulaması
- **Discovery** (`/.well-known/openid-configuration`), **JWKS** ve anahtar rotasyonu
- **`state` (CSRF) vs `nonce` (replay)** ⭐
- Standart scope'lar: `openid`, `profile`, `email`, `offline_access`
- Oturum yönetimi ve **logout:** RP-initiated, front-channel, **back-channel logout**
- `prompt`, `max_age`, `acr_values` — **step-up authentication** (Level of Assurance)
- 🛠 **Kod:** Razor Pages uygulamasında OIDC login + logout (back-channel dahil) + hassas sayfa için MFA zorunlu step-up authentication.

#### M9.3 Keycloak · `03-keycloak` ⭐
- Keycloak nedir: açık kaynak IAM sunucusu (CNCF projesi); OAuth 2.1, OIDC, SAML 2.0
- **Çalıştırma:** Docker image, dev vs prod mode, PostgreSQL ile, hostname ve reverse proxy ayarları, health/metrics endpoint'leri
- **Temel kavramlar:**
  - **Realm** — izole kimlik alanı (tenant)
  - **Client** — public / confidential; client scope'ları ve **protocol mapper**'lar
  - **Realm role vs client role** ⭐, composite role
  - **Group**, kullanıcı ve kullanıcı attribute'ları
  - **Organizations** — B2B multi-tenancy (Keycloak 26)
- **Authentication flow'ları:** browser flow, custom flow, required actions; MFA (OTP, WebAuthn), **passkeys**
- **Identity brokering** (Google, GitHub, Entra ID'yi dış IdP olarak bağlama) ve **user federation** (LDAP/Active Directory)
- Token ayarları: ömürler, refresh rotation, **DPoP**, PAR, FAPI 2.0 desteği
- **Service account** (client credentials)
- Authorization Services (UMA 2.0, policy/permission) — ne zaman değer
- **Otomasyon:** Admin REST API, realm export/import, keycloak-config-cli, Terraform provider — "tıklayarak yapılandırma" yerine kod olarak yapılandırma
- Event'ler ve audit
- **ASP.NET Core entegrasyonu:**
  - API: `AddJwtBearer` + `Authority` = realm URL; **audience mapper eksikliği** — en yaygın entegrasyon hatası ⭐
  - Web uygulaması: `AddOpenIdConnect` + code flow + PKCE
  - **Rol eşleme:** Keycloak rolleri `realm_access.roles` ve `resource_access.{client}.roles` altında gelir → **`IClaimsTransformation`** ile `ClaimTypes.Role`'e eşleme ⭐
  - `Keycloak.AuthServices` kütüphanesi; Aspire Keycloak entegrasyonu
- Production: HA cluster, veritabanı, yedekleme, sürüm yükseltme, tema özelleştirme; self-hosted Keycloak vs yönetilen IdP maliyeti
- 🛠 **Kod:** Compose ile Keycloak + PostgreSQL ve script'le otomatik realm import'u; (1) JWT ile korunan API, (2) OIDC ile giriş yapan web uygulaması, (3) client credentials ile servis-servis çağrı, (4) realm/client rollerini eşleyen `IClaimsTransformation`, (5) Google ile identity brokering, (6) Testcontainers ile Keycloak'lı integration testi.

#### M9.4 ASP.NET Core'da Authentication & Authorization Derinlemesine · `04-aspnetcore-auth`
- Authentication scheme'leri: default scheme, çoklu scheme (cookie + JWT), policy scheme, `ForwardDefaultSelector`
- `ClaimsPrincipal`, `ClaimsIdentity`, claims transformation
- **Policy-based authorization** ⭐: `IAuthorizationRequirement` + `AuthorizationHandler<T>`
- **Resource-based authorization** (`IAuthorizationService.AuthorizeAsync(user, resource, policy)`) — IDOR'a karşı asıl savunma
- **`FallbackPolicy`** ile varsayılan olarak kilitli API; `[AllowAnonymous]` istisna olmalı
- Permission-based (fine-grained) model
- SignalR ve gRPC'de authorization
- Test için auth handler
- 🛠 **Kod:** "Belgeyi sadece sahibi veya `Editor` rolü düzenleyebilir" kuralını resource-based handler ile uygulayan API + yetki testleri.

#### M9.5 SPA, Mobil & BFF Güvenliği · `05-spa-bff`
- Tarayıcıda token saklama: localStorage (XSS riski) vs bellek vs cookie
- **BFF (Backend for Frontend) pattern** ⭐ — token'lar sunucuda, tarayıcıda sadece HttpOnly + SameSite cookie; YARP ile BFF proxy; Duende BFF
- SPA'nın doğrudan PKCE ile public client olması — riskleri
- Mobil: sistem tarayıcısı (AppAuth), app link / custom URI scheme
- BFF'de CORS + cookie + CSRF
- 🛠 **Kod:** SPA (React veya Blazor WASM) + ASP.NET Core BFF (YARP) + Keycloak — token'ların hiçbir zaman tarayıcıya inmediği kurulum.

#### M9.6 Servisler Arası Kimlik · `06-service-to-service`
- Client credentials + **token cache** (`Duende.AccessTokenManagement`)
- **Token exchange / on-behalf-of** — kullanıcı bağlamını servisler arasında taşımak
- mTLS; API key'ler ne zaman yeterli
- Cloud'da secret'sız kimlik: managed identity, workload identity federation (→ C2.11)
- Zero trust'a giriş (→ S7.5)
- 🛠 **Kod:** Sipariş servisi → stok servisi çağrısı: client credentials + token cache; kullanıcı bağlamının token exchange ile taşınması.

#### M9.7 Identity Provider Seçimi · `07-idp-selection`
- Karşılaştırma:

  | Seçenek | Tür | Ne zaman |
  |---------|-----|----------|
  | **Keycloak** | Açık kaynak, self-hosted | Tam kontrol, on-prem, lisans maliyeti istemeyenler |
  | **Microsoft Entra ID** | Yönetilen (workforce) | Kurum içi çalışan kimliği, Microsoft 365 ekosistemi |
  | **Entra External ID** | Yönetilen (müşteri) | Müşteri kimliği; Azure AD B2C yeni müşterilere kapandı |
  | Auth0 / Okta | Yönetilen SaaS | Hızlı başlangıç, zengin özellik, yüksek maliyet |
  | Duende IdentityServer | .NET kütüphanesi, ticari | .NET içinde gömülü, özelleştirilebilir auth sunucusu |
  | OpenIddict | .NET kütüphanesi, açık kaynak | Duende'nin açık kaynak alternatifi |
  | ASP.NET Core Identity | Kütüphane | Sadece yerel hesap yönetimi — tek başına bir OAuth sunucusu değildir |

- Karar kriterleri: operasyon yükü, maliyet, uyumluluk (KVKK/GDPR, veri yerleşimi), özelleştirme, multi-tenancy, protokol desteği
- "Kendi auth sunucumu yazarım" — neden yapılmamalı
- 🛠 **Kod:** Karar matrisi dokümanı + aynı API'nin sadece konfigürasyon değişikliğiyle Keycloak ve Entra ID arasında geçiş yapabildiği örnek.

🎯 **Mülakat:** "Authorization Code + PKCE akışını adım adım anlat" ⭐ · "Implicit flow neden kaldırıldı?" · "Refresh token rotation nedir?" · "JWT access token mı reference token mı?" · "`state` ile `nonce` farkı?" · "Keycloak'ta realm role ile client role farkı?" · "Keycloak rolleri ASP.NET Core'da neden doğrudan çalışmaz?" · "SPA'da token nerede saklanmalı?" · "Servisler arası kimlik doğrulamayı nasıl yaparsın?"

---

### M10. Test Stratejisi

> 📁 `02-mid/10-testing-strategy/` · ⏱ ~2 hafta · Önkoşul: J10
> Performans testi → S2.6; architecture testleri → S5.5.

#### M10.1 Integration Testing Derinlemesine · `01-integration-testing`
- `WebApplicationFactory` özelleştirme: `ConfigureTestServices`, servis değiştirme, config override
- Test authentication handler; authorization policy'lerini test etmek
- Veritabanı stratejileri: InMemory'nin tuzakları, SQLite, gerçek DB; **Respawn** ile temizleme; test başına transaction rollback
- Fixture'lar: `IClassFixture`, `ICollectionFixture`, `IAsyncLifetime`; paralel çalıştırma ve izolasyon
- Background service'leri ve zamanı (`FakeTimeProvider`) test etmek
- Dış HTTP bağımlılıkları: **WireMock.Net**
- 🛠 **Kod:** Junior capstone'a kapsamlı integration test suite'i (auth, validation, concurrency, dış servis hatası).

#### M10.2 Testcontainers · `02-testcontainers`
- Gerçek bağımlılıkları Docker'da ayağa kaldırma: PostgreSQL, SQL Server, MongoDB, Redis, RabbitMQ, Kafka, **Keycloak**, Azurite
- Container yaşam döngüsü (sınıf/koleksiyon başına), reuse, başlangıç süresi optimizasyonu
- CI'da çalıştırma
- **Aspire testing** (`DistributedApplicationTestingBuilder`) — alternatif orkestrasyon
- 🛠 **Kod:** PostgreSQL + Redis + Keycloak container'larıyla uçtan uca test suite'i.

#### M10.3 Snapshot & Contract Testing · `03-snapshot-contract`
- **Verify** ile snapshot testing — API yanıtları, OpenAPI dokümanı, EF Core'un ürettiği SQL
- Consumer-driven contract testing: **Pact**
- OpenAPI tabanlı kontrat kontrolleri
- 🛠 **Kod:** OpenAPI dokümanı ve üretilen SQL için snapshot testleri; iki servis arasında Pact kontratı.

#### M10.4 E2E Testing (Playwright) · `04-e2e-playwright`
- **Playwright for .NET:** tarayıcı otomasyonu, auto-wait, trace viewer
- API E2E ve deploy sonrası smoke test
- Test piramidi vs testing trophy
- 🛠 **Kod:** BFF + SPA + Keycloak login akışını uçtan uca test eden Playwright senaryosu.

#### M10.5 Test Kalitesi: Coverage, Mutation & Flaky Testler · `05-test-quality`
- Coverage (Coverlet, Microsoft Code Coverage, ReportGenerator) — **yüzdenin yanıltıcılığı** ⭐
- **Mutation testing: Stryker.NET** — testlerin gerçekten hata yakalayıp yakalamadığı
- Flaky test sebepleri: zaman, sıra bağımlılığı, paylaşılan state, async yarışlar; karantinaya alma
- BDD: Reqnroll (SpecFlow'un devamı)
- 🛠 **Kod:** %90 coverage'lı ama zayıf bir test setini Stryker ile ifşa edip güçlendirme.

🎯 **Mülakat:** "Integration testte veritabanını nasıl yönetirsin?" · "InMemory provider neden yanıltıcı?" · "Contract testing neyi çözer?" · "Flaky testlerin sebepleri?" · "Coverage yüksek ama bug kaçıyor — neden?"

---

### M11. Mimari & Tasarım Temelleri

> 📁 `02-mid/11-architecture-design/` · ⏱ ~2.5 hafta · Önkoşul: J2.3, M3
> Kod seviyesinde tasarım ve uygulama içi mimari. DDD, event sourcing, modular monolith ve mimari stillerin tam karşılaştırması → S5.

#### M11.1 SOLID & Temel Prensipler · `01-solid`
- **S**ingle Responsibility — "tek bir değişme sebebi"
- **O**pen/Closed — genişlemeye açık, değişikliğe kapalı
- **L**iskov Substitution — klasik ihlaller (Square/Rectangle, `NotImplementedException` fırlatan override)
- **I**nterface Segregation — şişman interface'i bölmek
- **D**ependency Inversion — DI ile ilişkisi ve farkı ⭐
- DRY, KISS, YAGNI, Law of Demeter, composition over inheritance, separation of concerns
- **Coupling & cohesion derinlemesine** ⭐ (giriş → J2.3)
  - Coupling türleri: content, common, control, stamp, data coupling — en gevşekten en sıkıya
  - **Temporal coupling** (çağrı sırası zorunluluğu) ve **temporal decoupling** (mesajlaşma → M12.1)
  - Afferent/efferent coupling, instability metriği; `NetArchTest`/ArchUnitNET ile sınır testleri (→ M10)
  - Yüksek cohesion'ın pratikte görünümü: birlikte değişen kodun birlikte durması (vertical slice ile bağlantı → M11.4)
- **Connascence** — "iki kod parçası niçin birlikte değişmek zorunda" sorusunu coupling'den daha ince taneli anlatan model (isim, tip, anlam, konum, sıra, zamanlama)
- SOLID'in aşırı uygulanması: gereksiz soyutlama, "interface her şeye" eleştirisi
- 🛠 **Kod:** Her prensip için **kötü → iyi** refactor hikayesi; her adımda testlerin yeşil kaldığı commit serisi.

#### M11.2 Design Patterns · `02-design-patterns`
- **Creational:** Singleton (ve DI ile alternatifi), Factory Method, Abstract Factory, Builder
- **Structural:** Adapter, Decorator, Facade, Proxy, Composite
- **Behavioral:** Strategy, Observer, Mediator, Chain of Responsibility, Command, Template Method, State, Visitor
- .NET'teki doğal karşılıkları: `IEnumerable` = Iterator, middleware = Chain of Responsibility, `IObservable<T>` = Observer, `HttpClient` handler'ları = Decorator, Options = Builder
- **Anti-pattern'ler:** God object, anemic domain model, service locator, primitive obsession, magic string
- Pattern seçimi: **problemden başla, pattern'den değil**
- 🛠 **Kod:** Her pattern için gerçekçi bir senaryo (ör. Strategy: kargo ücreti hesaplama, Decorator: cache'li repository, State: sipariş durum makinesi).

#### M11.3 Katmanlı, Clean, Hexagonal & Onion · `03-layered-clean-hexagonal`
- Klasik **N-tier** (Presentation → Business → Data) ve sınırları — bağımlılık kuralı yok
- **Clean Architecture** (Robert C. Martin): Entities → Use Cases → Interface Adapters → Frameworks & Drivers; **Dependency Rule** — bağımlılıklar içe doğru
- **Hexagonal / Ports & Adapters** (Alistair Cockburn): port = interface, adapter = implementasyon; driving (controller) vs driven (repository) adapter
- **Onion Architecture** (Jeffrey Palermo): iç içe halkalar, dış katman içe bağımlı olabilir, tersi asla
- **Asıl mesaj** ⭐ — üçü de **aynı fikrin farklı isimlendirmesi**: iş mantığını framework/DB/UI'dan izole etmek. Fark vokabülerde: Hexagonal port/adapter, Onion halka, Clean use-case dilini kullanır.
- **Hexagonal'in test avantajı** ⭐ — çekirdeği hiç adapter ayağa kaldırmadan, port'lara test double vererek test etmek; "DB'siz, HTTP'siz test" bu mimarinin asıl getirisi
- **İş mantığını organize etmenin üç yolu (Fowler)** ⭐ — mimariyi seçmeden önce sorulacak soru
  - **Transaction Script** — istek başına prosedürel akış; CRUD ağırlıklı, kuralı az işte **dürüst ve doğru** tercih
  - **Table Module** — tablo başına bir sınıf (klasik DataSet dünyası)
  - **Domain Model** — davranışı olan zengin nesneler; karmaşık kural ve invariant varsa (→ S5.1 DDD)
  - Clean/Hexagonal/Onion asıl olarak **Domain Model**'i korumak için vardır; Transaction Script yazıyorsan bu törene çoğu zaman ihtiyaç yoktur
- **Screaming Architecture** — klasör yapısı domain'i mi ("Siparişler", "Ödeme") yoksa framework'ü mü ("Controllers", "Services", "Repositories") haykırıyor (→ M11.4)
- **.NET'te pratik yerleşim:** `Domain` / `Application` / `Infrastructure` / `Web` projeleri; **interface'ler hangi katmanda tanımlanır** (DIP gereği ihtiyacı duyan katmanda, implementasyon Infrastructure'da); `Web`'in `Infrastructure`'a yalnızca **composition root**'ta dokunması; kuralı derleme zamanında zorlamak (→ S5.5 architecture testleri)
- Katmanları ayrı proje mi, klasör mü yapmalı — proje sınırı kuralı derleyiciye zorlatır, klasör sınırı disipline bırakır
- **Trade-off:** küçük CRUD serviste fazla tören; karmaşık domain'de kurtarıcı
- 🛠 **Kod:** Aynı "sipariş ver" use case'inin üç versiyonu — Transaction Script, N-tier ve Clean Architecture; DB'yi PostgreSQL'den MongoDB'ye değiştirmenin her birindeki maliyeti ölçme; Clean versiyonda çekirdeği in-memory port'larla (DB/HTTP olmadan) test eden suite.

#### M11.4 Vertical Slice Architecture · `04-vertical-slice`
- Katman yerine **özellik (feature)** bazlı organizasyon
- Feature klasöründe request, handler, validator, endpoint bir arada
- Slice'lar arası tekrarı kabullenmek; yüksek cohesion
- Clean Architecture ile karşılaştırma — ne zaman hangisi
- Minimal API / REPR / FastEndpoints ile doğal uyum (→ M5.4)
- 🛠 **Kod:** M11.3'teki uygulamanın vertical slice versiyonu; yeni bir özellik eklerken dokunulan dosya sayısı karşılaştırması.

#### M11.5 CQRS Temelleri & Mediator · `05-cqrs-mediator`
- Command / query ayrımının **asıl amacı:** farklı model, farklı optimizasyon
- **CQRS ≠ Event Sourcing** ⭐ — sık karıştırılır
- Basit CQRS (aynı DB, ayrı okuma modeli) vs tam CQRS (ayrı read store → S5.2)
- Mediator pattern ve handler organizasyonu
- **MediatR lisans durumu:** v13+ ticari/RPL dual lisans (küçük şirketlere ücretsiz community edition); alternatifler: **Wolverine**, source generator tabanlı `Mediator`, düz handler sınıfları
- **Pipeline behavior** ile cross-cutting: validation, logging, transaction, caching
- CQRS'in maliyeti — ne zaman gereksiz karmaşıklık
- 🛠 **Kod:** Aynı uygulamanın MediatR'lı, `Mediator` (source-gen) ile ve kütüphanesiz düz handler'lı versiyonları; validation pipeline behavior'ı.

#### M11.6 Hata Modelleme: Exception vs Result · `06-error-modeling`
- Exception ne zaman doğru araç (beklenmeyen durum), `Result` ne zaman (beklenen iş hatası)
- `Result<T>` / `ErrorOr` / `FluentResults` / `OneOf`
- Domain hatalarını `ProblemDetails`'e eşleme stratejisi
- Railway-oriented programming fikri
- Exception tabanlı akışın performans ve okunabilirlik maliyeti
- 🛠 **Kod:** Aynı use case'in exception tabanlı ve Result tabanlı versiyonları; hataların HTTP yanıtına tutarlı eşlenmesi.

🎯 **Mülakat:** "SOLID'i örnekle anlat" · "Dependency Inversion ile Dependency Injection farkı?" · "Clean, Hexagonal ve Onion aynı şey mi?" ⭐ · "Hexagonal mimarinin test tarafındaki getirisi nedir?" · "Bu iş için Transaction Script mi Domain Model mi — neye göre karar verirsin?" ⭐ · "Clean Architecture'da interface'i hangi katmana koyarsın, neden?" ⭐ · "Clean Architecture mı Vertical Slice mı?" · "CQRS ile Event Sourcing farkı?" · "MediatR'ı neden kullanırsın / kullanmazsın?" · "Exception mı Result mı?"

---

### M12. Mesajlaşma & Message Broker'lar

> 📁 `02-mid/12-messaging/` · ⏱ ~2 hafta · Önkoşul: M4, M11
> Altyapı (repo köküne eklenecek `docker-compose.yml` servisleri): RabbitMQ, Kafka (KRaft), Azure Service Bus emulator.
> Event-driven mimari, outbox, saga ve Kafka derinlemesine → S6.

#### M12.1 Mesajlaşma Kavramları · `01-messaging-concepts`
- Senkron (HTTP/gRPC) vs asenkron (mesaj) iletişim — temporal coupling
- **Queue (point-to-point) vs topic (pub/sub)**
- **Command vs event** mesajları — isimlendirme ve sahiplik
- **Delivery semantics:** at-most-once, **at-least-once**, "exactly-once" (pratikte = at-least-once + idempotency) ⭐
- **Idempotent consumer** — mesaj ID'si, inbox tablosu
- Retry, exponential backoff, **dead-letter queue**, poison message
- Sıralama garantileri ve partition/session key
- Competing consumers; mesaj zarfı: header, correlation ID, causation ID
- **Dual write problemi**'ne giriş (çözümü outbox → S6.3)
- 🛠 **Kod:** Önce `Channel<T>` ile in-process kavramsal simülasyon (duplicate, retry, DLQ), sonra aynı akışın gerçek broker ile hali.

#### M12.2 RabbitMQ · `02-rabbitmq` ⭐
- **AMQP 0-9-1 modeli:** producer → **exchange** → binding → queue → consumer; broker neden "akıllı", istemci neden "aptal" (Kafka'nın tersi)
- **Exchange tipleri** ⭐

  | Tip | Yönlendirme | Kullanım |
  |-----|-------------|----------|
  | `direct` | routing key **tam eşleşme** | İş kuyruğu, belirli bir tüketiciye yönlendirme |
  | `fanout` | routing key yok sayılır, **tüm** bağlı kuyruklara | Broadcast, cache invalidation |
  | `topic` | noktalı key + wildcard: `*` bir kelime, `#` sıfır veya daha fazla | `order.created.tr` gibi hiyerarşik olaylar |
  | `headers` | routing key yerine **header** eşleşmesi (`x-match: all/any`) | Çok kriterli yönlendirme (nadir) |

  - **Alternate exchange** — eşleşmeyen mesajın sessizce kaybolması yerine yakalanması
  - Default (nameless) exchange ve kuyruk adıyla yönlendirme
- **Connection ve channel modeli** ⭐ — connection TCP bağlantısıdır (pahalı, uygulama ömrü boyunca paylaşılır), **channel** ise onun üzerindeki hafif mantıksal kanaldır; **channel thread-safe değildir** → thread/consumer başına ayrı channel, connection'ı paylaş
- **Kuyruk tipleri:**
  - **Quorum queue** — Raft tabanlı replikasyon, dayanıklı iş yükleri için **varsayılan tercih**; tek sayıda replika (3 veya 5) ile çoğunluk; Raft log'u bellek + disk tuttuğu için çok derin kuyrukta bellek maliyeti yüksek
    - **`delivery-limit`** ⭐ — 4.0'dan itibaren varsayılan **20**; limit aşılınca mesaj dead-letter edilir, DLX yoksa **düşer** → bu yüzden her quorum queue'ya bir dead-letter yapılandırması önerilir. `-1` ile kapatılabilir ama önerilmez. 4.3'ten itibaren `acquired-count` ve `delivery-count` ayrı sayılır; limite yalnızca `delivery-count` bakar
    - `basic.reject` sayacı artırır, `basic.nack` artırmaz — poison message davranışını belirleyen ayrım
  - **Classic queue** — tek replika; **classic queue mirroring 4.0'da tamamen kaldırıldı** (mirror policy'leri artık etkisiz); transient (non-durable) non-exclusive classic queue'lar 4.3'ten itibaren varsayılan olarak bildirilemiyor
  - **Lazy queue** — mesajları diskte tutarak bellek tasarrufu; quorum queue'lar büyük ölçüde yerini aldı (tarihsel bilgi olarak bilinmeli)
  - **Stream** — append-only log, tekrar okunabilir (replay), çok tüketicili; RabbitMQ'nun Kafka'ya en yakın yapısı
- **Teslim güvenliği:** `ack`/`nack`/`reject` (+ `requeue`), **prefetch (QoS)** ile tüketici başına eşzamanlı mesaj sınırı — prefetch çok yüksekse tek tüketici kuyruğu kilitler, çok düşükse throughput düşer; **publisher confirms** (asenkron onay) ve neden `tx` yerine tercih edilir
- **Dead-lettering:** DLX + DLQ; `x-dead-letter-exchange`, red nedeni header'ları; **TTL + DLX ile gecikmeli retry** (ping-pong deseni) vs **Delayed Message Exchange plugin** — hangisi ne zaman
- **Clustering & yüksek erişilebilirlik:** node'lar, disk vs RAM node, **metadata store Khepri** (Raft tabanlı; 4.2'den itibaren yeni kurulumlarda varsayılan, yükseltilen node'lar eski store'da kalır — `khepri_db` feature flag'i ile geçiş), network partition davranışı (`pause_minority`), quorum queue'ların partition'da nasıl davrandığı
- **Federation vs Shovel** ⭐ — iki bölge/veri merkezi arasında mesaj taşımanın iki yolu

  | | Federation | Shovel |
  |---|-----------|--------|
  | Model | Downstream broker, upstream exchange/queue'ya **abone olur** | Kaynak kuyruktan okur, hedefe **publish eder** (point-to-point) |
  | Bağlılık | Gevşek; kopma sonrası kendini toparlar | Daha açık/kontrollü, tek yönlü akış |
  | Tipik kullanım | Sürekli, hub-and-spoke çok bölgeli topoloji | Tek seferlik göç, kuyruk boşaltma, köprüleme |

- **Akış kontrolü (back pressure):** bellek ve disk alarmları (`vm_memory_high_watermark`, `disk_free_limit`) — alarm tetiklendiğinde publisher'ların bloklanması; "mesajlar birikti, publish donuyor" teşhisi
- Sanal host (vhost), kullanıcı/permission modeli, policy ile kuyruk ayarı yönetimi
- **İzleme:** Management UI, Prometheus plugin, bakılacak metrikler (queue depth, unacked, consumer utilisation, redelivery oranı); **4.2'de Raft metrik adları değişti** — dashboard'ların güncellenmesi gerekir
- `RabbitMQ.Client` 7.x — tamamen **async API**; publisher confirm ve consumer örüntüleri
- 🛠 **Kod:** Sipariş olaylarını topic exchange ile birden fazla servise dağıtan sistem (`order.created.*` binding'leri); DLX + TTL ile artan gecikmeli retry ve son durakta DLQ; publisher confirms ile "mesaj gerçekten yazıldı mı" kanıtı; prefetch değerini 1 / 10 / 1000 yapıp throughput ve adil dağıtım farkını ölçme; Compose ile 3 node'lu cluster + quorum queue üzerinde bir node'u düşürüp mesaj kaybı olmadığını gösterme; iki broker arasında shovel ile kuyruk taşıma.

#### M12.3 Azure Service Bus · `03-azure-service-bus`
- Queue vs topic/subscription; subscription filtreleri
- **Session** ile FIFO ve sıralı işleme
- Peek-lock vs receive-and-delete; lock yenileme
- Dead-letter, scheduled message, **duplicate detection**
- `Azure.Messaging.ServiceBus` SDK; managed identity ile bağlantı
- Yerel geliştirme: **Service Bus emulator**
- 🛠 **Kod:** Emulator ile yerelde, Azure'da gerçek namespace ile session'lı sipariş işleme.

#### M12.4 Kafka'ya Giriş · `04-kafka-intro`
- **Log tabanlı model:** topic, partition, offset, retention — kuyruktan farkı
- Consumer group, partition ataması, rebalancing
- Key ile sıralama garantisi (partition başına)
- KRaft (ZooKeeper'sız Kafka)
- `Confluent.Kafka` client
- **Kafka vs RabbitMQ karar tablosu** ⭐ (derin hali → S6.6)
- 🛠 **Kod:** Tıklama olaylarını Kafka'ya yazan producer + aynı topic'i bağımsız okuyan iki consumer group (analitik ve bildirim).

#### M12.5 .NET Mesajlaşma Kütüphaneleri · `05-messaging-libraries`
- Neden soyutlama: retry, serialization, outbox, saga, test desteği
- **MassTransit** — v9'dan itibaren ticari lisans (2026); v8 açık kaynak ve 2026 boyunca güvenlik yaması alıyor
- **Wolverine** (açık kaynak) — mesajlaşma + mediator + outbox birlikte
- NServiceBus (ticari), Rebus, Brighter
- Ham SDK ile çalışmak — ne zaman yeterli
- 🛠 **Kod:** Aynı consumer'ın ham `RabbitMQ.Client`, MassTransit v8 ve Wolverine ile yazılıp karşılaştırılması (kod miktarı, retry/DLQ yapılandırması, test edilebilirlik).

🎯 **Mülakat:** "At-least-once teslimatta duplicate mesajları nasıl ele alırsın?" ⭐ · "Queue ile topic farkı?" · "RabbitMQ exchange tipleri nedir, `topic` ile `direct` farkı?" ⭐ · "Quorum queue ile classic queue farkı, hangisini seçersin?" · "Prefetch değeri neyi etkiler?" · "Gecikmeli retry'ı RabbitMQ'da nasıl yaparsın?" · "Federation ile shovel farkı?" · "Connection mı channel mı paylaşılır?" · "Kafka mı RabbitMQ mı?" · "Poison message nedir?" · "Mesaj sırasını nasıl garanti edersin?" · "Command ile event farkı?"

---

### M13. Observability

> 📁 `02-mid/13-observability/` · ⏱ ~2 hafta · Önkoşul: J6.5
> Altyapı (repo köküne eklenecek `docker-compose.yml` servisleri): OpenTelemetry Collector + Grafana LGTM (`grafana/otel-lgtm`) + Seq.
> "Loglama zaten var" — ama **tek başına log yetmez.** Production teşhis metodolojisi, SLO ve APM seçimi → S8.

#### M13.1 Üç Sinyal: Log, Metric, Trace · `01-three-signals`
- **Log:** "Tam olarak ne oldu?" — ayrık olay, detaylı bağlam, en yüksek hacim/maliyet
- **Metric:** "Şu an sistem nasıl?" — sayısal zaman serisi, ucuz, alerting için ideal
- **Trace:** "Bu istek nereye gitti, nerede yavaşladı?" — dağıtık sistemlerde zorunlu
- Birlikte çalışma: metric anomaliyi gösterir → trace sorumlu adımı bulur → log tam bağlamı verir
- **Monitoring vs observability** — önceden bilinen sorular vs bilinmeyen sorular
- Maliyet: "her şeyi logla" stratejisinin sürdürülemezliği
- 🛠 **Kod:** Kasıtlı yavaş bir bağımlılığı olan küçük bir sistem; problemi sadece log, sadece metric ve üçü birlikte kullanarak bulma alıştırması.

#### M13.2 Structured Logging Derinlemesine · `02-structured-logging`
- Neden structured: log'u **sorgulanabilir veriye** çevirmek
- **`LoggerMessage` source generator** (`[LoggerMessage]`) — yüksek performanslı logging
- **Serilog:** sink'ler (Console, Seq, OTLP, Elasticsearch), enricher'lar, `Enrich.WithProperty`
- Log scope ile bağlam; **correlation/trace ID** ile istek bazlı log takibi
- **PII redaksiyonu:** `Microsoft.Extensions.Compliance.Redaction`, veri sınıflandırma attribute'ları
- Log sampling ve buffering (.NET 9+ `Microsoft.Extensions.Telemetry`)
- Dinamik log seviyesi (runtime'da Debug'a çekme)
- Log pipeline: uygulama → stdout → collector (Fluent Bit, OTel Collector) → depo (Loki, Elasticsearch, Seq)
- Retention, maliyet ve KVKK/GDPR dengesi
- 🛠 **Kod:** Serilog + Seq kurulumu; source-generated log mesajları; kredi kartı ve e-posta alanlarını otomatik maskeleyen redaction; interpolation vs template performans benchmark'ı.

#### M13.3 Metrics · `03-metrics`
- Metrik türleri: **counter**, **gauge**, **histogram** (p50/p95/p99), up-down counter
- **`System.Diagnostics.Metrics`:** `Meter`, `Counter<T>`, `Histogram<T>`, `ObservableGauge<T>`; `IMeterFactory`
- **RED** (Rate, Errors, Duration — servisler) ve **USE** (Utilization, Saturation, Errors — kaynaklar); Four Golden Signals
- ASP.NET Core, Kestrel, `HttpClient`, runtime'ın yerleşik metrikleri
- **Cardinality problemi** ⭐ — `UserId`'yi etiket yapmak metrik veritabanını patlatır
- Pull (Prometheus `/metrics`) vs push (OTLP)
- Grafana dashboard'ları, PromQL temelleri
- Latency percentile'ları — **ortalama neden yalan söyler** ⭐
- 🛠 **Kod:** İş metrikleri (oluşturulan sipariş, ödeme süresi histogramı) yayınlayan API; Grafana'da RED dashboard'u.

#### M13.4 Distributed Tracing & OpenTelemetry · `04-tracing-opentelemetry`
- **Trace / span / baggage** kavram modeli; parent-child span ilişkisi
- **W3C Trace Context** (`traceparent`, `tracestate`) ile context propagation
- **OpenTelemetry:** API (.NET'te `ActivitySource`/`Activity`), SDK, **Collector**, exporter (OTLP)
- `AddOpenTelemetry().WithTracing().WithMetrics().WithLogging()` kurulumu
- Auto-instrumentation (ASP.NET Core, `HttpClient`, EF Core, Npgsql, Redis, MassTransit) vs manuel span
- `Activity.Current`, custom tag/event ekleme
- **Sampling:** head-based vs tail-based; oran bazlı
- Trace ID'yi loglara otomatik eklemek — log ↔ trace korelasyonu
- Context propagation'ın kırıldığı yerler: fire-and-forget, background job, **mesaj kuyruğu** (header ile taşımak)
- 🛠 **Kod:** API → gRPC servisi → RabbitMQ consumer → PostgreSQL zincirinde uçtan uca tek trace; Collector üzerinden Tempo/Jaeger'a export.

#### M13.5 Health Checks & Alerting Temelleri · `05-health-alerting`
- Health check'in observability ile ilişkisi: "ayakta mı?" vs "neden yavaş?"
- Bağımlılık health check'leri ve degrade durumlar
- **Aspire Dashboard** ile yerel geliştirmede log/metric/trace
- Alerting temelleri: eşik vs anomali tabanlı; **alert fatigue**; actionable alert
- SLI/SLO kavramına giriş (derin → S8.4)
- 🛠 **Kod:** Hata oranı ve p95 latency için Grafana alert kuralları + bilinçli tetikleme senaryosu.

🎯 **Mülakat:** "Log, metric ve trace ne zaman hangisi?" · "Structured logging neden önemli?" · "Ortalama latency neden yanıltıcı?" · "Distributed tracing nasıl çalışır, context servisler arasında nasıl taşınır?" · "Metrik etiketlerinde cardinality neden önemli?" · "OpenTelemetry Collector ne işe yarar?"

---

### M14. DevOps: CI/CD, Container & Kubernetes Temelleri

> 📁 `02-mid/14-devops/` · ⏱ ~3 hafta · Önkoşul: J12, J10.5
> Docker temelleri J12'de; burada **container'ı production'a hazırlamak** (BuildKit, image sertleştirme) ve **Kubernetes'i çalıştırmak** var. İleri Kubernetes (HPA/KEDA, Helm, GitOps, NetworkPolicy), IaC ve platform mühendisliği → S8.

#### M14.1 CI/CD Pipeline'ları · `01-ci-cd`
- Aşamalar: restore → build → test → analiz → publish → containerize → deploy
- **GitHub Actions** ve Azure Pipelines YAML; reusable workflow, matrix, cache, artifact
- Environment'lar ve onay kapıları (approval gate)
- **OIDC ile secret'sız Azure deploy** (workload identity federation) ⭐
- Test sonuçları ve coverage raporlama; PR kalite kapıları
- Versiyonlama: SemVer, GitVersion/MinVer; NuGet paket yayını
- Bağımlılık güncelleme otomasyonu: Dependabot / Renovate
- Deployment stratejilerine giriş: rolling, blue-green, canary, feature flag ile karanlık yayın; rollback planı
- DORA metrikleri: deployment frequency, lead time, MTTR, change failure rate
- 🛠 **Kod:** PR'da test + analiz, `main`'de container build + push + staging deploy + onaylı production deploy yapan tam pipeline.

#### M14.2 Container Derinlemesine · `02-containers-advanced`
- Image varyantları: Debian, Alpine, **chiseled** (Ubuntu, distroless benzeri), Native AOT image
- Image boyutu ve saldırı yüzeyi; **image tarama** (Trivy, Defender for Containers), SBOM
- **BuildKit & `docker buildx`** ⭐ — modern varsayılan builder; aşamaları paralel yürütme, atlanabilen kullanılmayan aşamalar
  - **Cache mount:** `RUN --mount=type=cache,target=/root/.nuget/packages dotnet restore` ile NuGet paketlerini build'ler arasında saklamak; `# syntax=docker/dockerfile:1` satırı zorunlu; çok projeli solution'da kazanç büyük
  - **Tuzak:** cache mount **yerel builder'da** yaşar, final image'a girmez → **hosted CI runner'da her çalıştırmada kaybolur**; GitHub Actions cache backend'i ile `--cache-to`/`--cache-from` export/import gerekir
  - Windows container'larda `RUN --mount` desteklenmez
  - **Build secret:** `--mount=type=secret` ile özel NuGet feed PAT'ini layer'a gömmeden kullanma (`ARG`/`ENV` ile geçirmek image geçmişine sızdırır)
- Multi-arch build (`buildx` ile amd64/arm64)
- Container'da .NET: CPU/bellek limitlerine duyarlılık, GC ayarları (`DOTNET_GCHeapHardLimit`), DATAS
- `dotnet publish /t:PublishContainer` ileri kullanım
- Rootless çalışma, read-only filesystem
- 🛠 **Kod:** Aynı API'nin dört image varyantı: boyut, CVE sayısı (Trivy), başlangıç süresi ve bellek tablosu.

#### M14.3 Kubernetes Temelleri · `03-kubernetes-basics` ⭐
- **Mimari:** control plane (API server, scheduler, controller manager, etcd), node, kubelet, kube-proxy
- **Declarative model:** istenen durum (desired state) vs mevcut durum, reconciliation loop; `kubectl apply` ve idempotency
- **Namespace** — mantıksal izolasyon, ortam/takım ayrımı; `ResourceQuota` ve `LimitRange` ile kota
- **Label, selector & annotation** ⭐ — Service'in Pod'ları nasıl bulduğu label selector ile olur; annotation metadata içindir, seçim için kullanılmaz
- **Pod** — en küçük dağıtım birimi, çok container'lı Pod ne zaman anlamlı
- **ReplicaSet → Deployment** — rolling update, `maxSurge`/`maxUnavailable`, `kubectl rollout status` / `undo`
- **Service tipleri** ⭐
  - **ClusterIP** (varsayılan, cluster içi)
  - **NodePort** (her node'da sabit port)
  - **LoadBalancer** (cloud yük dengeleyici)
  - **ExternalName** (DNS CNAME)
  - **Headless** (`clusterIP: None`) — Pod IP'lerini doğrudan döner, StatefulSet'in stabil DNS'inin temeli (→ M14.4)
- **Ingress** ve **Gateway API**'ye giriş (derinlemesine → S8.1)
- **ConfigMap ve Secret** ile konfigürasyon; Secret'ın varsayılan olarak yalnızca base64 olduğu (şifreleme değil) ⭐ — gerçek secret yönetimi → S8.1
- `kubectl` temelleri ve **teşhis**: `get`, `describe`, `logs` (+ `--previous`), `get events`, `port-forward`, `exec`
- Yerel cluster: kind, k3d, Docker Desktop; sürüm penceresi — proje aynı anda yalnızca **son üç minor sürümü** destekler (2026'da 1.35-1.37 hattı)
- 🛠 **Kod:** Capstone API'sinin Deployment + Service + Ingress + ConfigMap + Secret manifest'leri; kind üzerinde rolling update ve `rollout undo`; Service tiplerini tek tek deneyip hangisinin nereden erişilebildiğini tabloya dökme.

#### M14.4 Kubernetes Workload'ları, Storage & Yaşam Döngüsü · `04-kubernetes-workloads` ⭐
> Deployment her şey değildir: durumlu servisler, batch işler ve node başına ajanlar farklı nesneler ister. Mülakatta "StatefulSet ile Deployment farkı?" klasik sorudur.

- **Workload nesneleri — hangisi ne zaman** ⭐

  | Nesne | Ne yapar | Tipik kullanım |
  |-------|----------|----------------|
  | **Deployment** | Stateless, birbirinin yerine geçebilen replikalar | Web API, worker |
  | **StatefulSet** | Sıralı ve **kalıcı kimlik** (`web-0`, `web-1`), Pod başına kendi PVC'si, headless Service ile stabil DNS | PostgreSQL, Kafka, Redis |
  | **DaemonSet** | Her node'da (veya seçili node'larda) bir Pod | Log toplayıcı, metrik ajanı, CNI |
  | **Job** | Pod'u **tamamlanana kadar** çalıştırır, hata olursa yeniden dener | Migration, batch hesaplama |
  | **CronJob** | Zamanlanmış Job üretir (cron sözdizimi) | Gece raporu, yedekleme |

  - StatefulSet'in `volumeClaimTemplates`'i ile her Pod'un kendi kalıcı diskini alması ve yeniden zamanlamada korunması
  - Job'da `completions`/`parallelism`/`backoffLimit`; CronJob'da `concurrencyPolicy` ve kaçırılan çalıştırma
  - **EF Core migration'ı Kubernetes'te nasıl çalıştırmalı:** uygulama startup'ında mı, **Job/init container** ile mi (çoklu replikada yarış riski → M6.6)
- **Kalıcı depolama** ⭐
  - **PersistentVolume (PV)** = cluster'daki depolama, **PersistentVolumeClaim (PVC)** = kullanıcının boyut ve erişim modu talebi
  - **StorageClass** ile **dinamik provisioning** (cloud disk otomatik oluşsun)
  - **Access mode'lar:** `ReadWriteOnce` (tek node), `ReadOnlyMany`, `ReadWriteMany` — çoğu cloud blok diski yalnızca RWO verir, bu yüzden "iki Pod aynı diske yazsın" beklentisi çöker ⭐
  - **Reclaim policy:** `Retain` (veri kalır, elle temizlenir) vs `Delete` (PVC silinince disk de gider) — production'da yanlış seçimin bedeli
  - `emptyDir`, `configMap`/`secret` volume; ephemeral vs kalıcı
- **Pod yaşam döngüsü & container türleri**
  - **Init container** — ana container'lardan önce sırayla çalışır ve biter (migration, bekleme)
  - **Sidecar container** — K8s **1.33'te stable**; `initContainers` içinde `restartPolicy: Always` ile tanımlanır, ana container'larla birlikte yaşar ve **onlar bittikten sonra** SIGTERM alır; Pod seviyesindeki `restartPolicy`'yi yok sayar
  - **Probe'lar:** liveness, readiness, startup ⭐ — ASP.NET Core health check eşlemesi (`/health/live`, `/health/ready` → M3.7); yanlış liveness probe'un sağlıklı Pod'u sürekli öldürmesi
  - **Graceful shutdown:** SIGTERM → `terminationGracePeriodSeconds` → SIGKILL; `preStop` hook; exec form `ENTRYPOINT` zorunluluğu (→ J12.2) ve `IHostApplicationLifetime` (→ M3.1)
  - Restart policy ve `CrashLoopBackOff`'un backoff mantığı
- **Resource yönetimi & zamanlama**
  - Resource `requests`/`limits`, **QoS sınıfları** (Guaranteed / Burstable / BestEffort), **OOMKilled** ⭐; CPU throttling
  - .NET'in container limitlerine duyarlılığı (→ M14.2)
  - Zamanlamaya giriş: `nodeSelector`, **taint & toleration**, node/pod **affinity & anti-affinity**, topology spread (derinlemesine → S8.1)
  - **PodDisruptionBudget** — node bakımında kaç replikanın aynı anda düşebileceği
- **CRD & Operator** kavramına giriş — Kubernetes API'sini kendi tipinle genişletmek; operator pattern'in "reconciliation loop" fikri (→ S8.6)
- 🛠 **Kod:** PostgreSQL'i StatefulSet + PVC ile ayağa kaldırıp Pod'u silip verinin kaldığını kanıtlama; EF Core migration'ını Job olarak çalıştırıp uygulama Deployment'ının ona `initContainer` ile beklemesi; bilerek yanlış ayarlanmış liveness probe ile sağlıklı Pod'un öldürülmesini gözlemleyip düzeltme; bellek limiti düşük bir Pod'u `OOMKilled` yapıp `describe` çıktısından teşhis; `concurrencyPolicy: Forbid` olan bir CronJob ile üst üste binen çalıştırmaları engelleme.

#### M14.5 Aspire ile Orkestrasyon · `05-aspire`
- **Aspire** (13 sürümüyle ".NET" öneki kalktı, polyglot oldu — Python, JavaScript/TypeScript da birinci sınıf)
- **AppHost:** kaynak modeli (PostgreSQL, Redis, RabbitMQ, Kafka, Keycloak, Ollama container'larını kodla tanımlama), referanslar ve servis keşfi
- **Service defaults:** OpenTelemetry, health check, resilience, service discovery hazır
- **Aspire Dashboard** ile yerel gözlemlenebilirlik
- `aspire` CLI; AI coding agent'lar için MCP desteği
- Deploy: `azd` ile Azure Container Apps'e, Docker Compose / Kubernetes çıktıları
- Integration testlerinde Aspire (`DistributedApplicationTestingBuilder`)
- Docker Compose'a göre avantaj/dezavantaj
- 🛠 **Kod:** Mid capstone'un AppHost'u — tüm bağımlılıklar tek `aspire run` ile; `azd up` ile Container Apps'e deploy.

🎯 **Mülakat:** "CI ile CD farkı?" · "Pipeline'da secret'ları nasıl yönetirsin?" · "BuildKit cache mount CI'da neden işe yaramaz?" · "Deployment ile StatefulSet farkı, hangisini ne zaman?" ⭐ · "DaemonSet ne için kullanılır?" · "Job ile CronJob farkı?" · "PV, PVC ve StorageClass ilişkisi nedir?" ⭐ · "İki Pod aynı diske yazabilir mi?" (access mode) · "Reclaim policy `Delete` seçmenin riski ne?" · "Liveness ile readiness probe farkı?" ⭐ · "Yanlış liveness probe ne yapar?" · "Init container ile sidecar farkı?" · "Pod neden OOMKilled olur?" · "Kubernetes'te graceful shutdown nasıl sağlanır?" ⭐ · "Service tiplerini say, headless Service ne işe yarar?" ⭐ · "Kubernetes Secret gerçekten güvenli mi?" · "Aspire ne çözüyor, Docker Compose'dan farkı ne?"

---

### M15. AI Engineering: Uygulama Geliştirme

> 📁 `02-mid/15-ai-engineering/` · ⏱ ~3 hafta · Önkoşul: J13, M5.9
> Altyapı (repo köküne eklenecek `docker-compose.yml` servisleri): Ollama, Qdrant, PostgreSQL + pgvector.
> .NET artık AI-first bir platform; 2026'da backend rollerinde bile soruluyor. Agent mimarisi, Agent Framework, eval ve LLMOps → S9.

#### M15.1 Microsoft.Extensions.AI Derinlemesine · `01-microsoft-extensions-ai`
- `IChatClient`, `IEmbeddingGenerator<TInput,TEmbedding>`, `ChatOptions`, `ChatResponse`
- **Middleware pipeline** (`ChatClientBuilder`): `UseLogging`, `UseOpenTelemetry`, `UseDistributedCache`, `UseFunctionInvocation`; custom middleware (rate limit, PII redaksiyonu)
- DI kaydı (`AddChatClient`), birden fazla model için keyed client
- Sağlayıcılar: OpenAI, Azure OpenAI / Microsoft Foundry, Ollama, Anthropic, Foundry Local
- **Bir LLM çağrısı da bir dış servis çağrısıdır** ⭐ — timeout, retry, fallback model, maliyet izleme (→ M5.9)
- .NET AI şablonları (`dotnet new aichatweb`)
- 🛠 **Kod:** Sağlayıcı değiştirilebilen chat servisi; caching + telemetri + logging middleware'leri; Aspire Dashboard'da token ve gecikme metrikleri.

#### M15.2 Prompt & Context Engineering · `02-prompt-context-engineering`
- System prompt tasarımı, talimat hiyerarşisi, few-shot, güvenilmeyen içeriği ayırma (delimiter)
- **Context engineering** ⭐ — pencereye ne girer: getirilen dokümanlar, tool sonuçları, bellek, konuşma özeti; context bütçesi
- Prompt'ları **kod gibi** yönetmek: dosyada versiyonlama, testlerle koruma
- Reasoning modelleri ve "düşünme" bütçesi
- Provider tarafı **prompt caching** ile maliyet/gecikme düşürme
- Prompt injection farkındalığı (savunma → S9.6)
- 🛠 **Kod:** Prompt'ları versiyonlanan ve eval testleriyle (→ S9.5) korunan bir destek talebi sınıflandırma servisi.

#### M15.3 Function Calling & Structured Output · `03-tools-structured-output`
- Tool/function calling: model karar verir, uygulama çalıştırır; `AIFunctionFactory.Create`, `UseFunctionInvocation`
- **Tool tasarımı:** net isim ve açıklama, dar kapsam, deterministik dönüş, anlamlı hata
- **Structured output:** JSON schema, `GetResponseAsync<T>()` ile tipli yanıt
- Yan etkili tool'larda onay (human-in-the-loop)
- 🛠 **Kod:** "Siparişim nerede?" asistanı — sipariş sorgulama ve kargo takibi tool'ları, tipli yanıt, iade gibi yan etkili işlemde kullanıcı onayı.

#### M15.4 Embeddings & Vector Search · `04-embeddings-vector-search`
- Embedding nedir; cosine / dot product / Euclidean benzerlik; boyut ve maliyet
- **`Microsoft.Extensions.VectorData`** — sağlayıcıdan bağımsız vector store soyutlaması
- Vector store seçenekleri: **pgvector**, Azure AI Search, Qdrant, Redis, **SQL Server / Azure SQL `VECTOR` + EF Core 10 `VectorDistance`**, Cosmos DB, MongoDB, Milvus, Weaviate, Pinecone
- **ANN index'leri:** HNSW, IVFFlat, DiskANN — recall / gecikme / bellek trade-off'u ⭐
- Metadata filtreleme; **hybrid search** (BM25 + vector) ve Reciprocal Rank Fusion
- 🛠 **Kod:** Aynı doküman setini VectorData soyutlamasıyla pgvector ve Qdrant'ta indeksleyip arama kalitesi ve gecikmesini karşılaştırma.

#### M15.5 RAG Pipeline · `05-rag`
- **RAG neden** ⭐ — güncel/özel veri, kaynak gösterimi, fine-tuning'e göre maliyet ve esneklik
- **Ingestion:** yükle → parse (PDF/Office) → chunk → embed → metadata ile sakla
- **Chunking stratejileri:** sabit boyut + overlap, semantic, yapıya duyarlı (başlık/paragraf), parent-child ⭐
- **Retrieval:** sorgu embedding'i → vector/hybrid search → **reranking**
- Query transformation: yeniden yazma, multi-query, HyDE
- Prompt oluşturma, **kaynak gösterimi (citation)**, "bilmiyorum" davranışı
- Yaygın hatalar: kötü chunking, eksik metadata, reranking yokluğu, değerlendirme yapmamak (ileri RAG ve eval → S9.4, S9.5)
- 🛠 **Kod:** Şirket içi dokümanlar için Q&A servisi — ingestion worker, hybrid search, reranking, kaynak gösterimi ve basit web arayüzü.

#### M15.6 MCP: Model Context Protocol · `06-mcp`
- Nedir: LLM uygulamalarını (host/client) araç ve veri kaynaklarına (server) bağlayan açık protokol
- Primitifler: **tools, resources, prompts** (+ sampling, elicitation)
- Transport'lar: **stdio** vs **Streamable HTTP**
- **Resmî C# SDK** (`ModelContextProtocol`, `ModelContextProtocol.AspNetCore` — 1.x kararlı): `[McpServerToolType]`, `[McpServerTool]`, `WithStdioServerTransport()`, `MapMcp()`
- .NET'te MCP client: MCP tool'larını `IChatClient`'a `AIFunction` olarak vermek
- **Güvenlik:** OAuth 2.1 tabanlı MCP yetkilendirmesi, en az yetki, tool poisoning, tool çıktısı üzerinden prompt injection
- MCP server'larını Claude Code, VS Code, GitHub Copilot'ta kullanmak
- 🛠 **Kod:** Sipariş veritabanını salt-okunur sorgulayan MCP server'ı (stdio + HTTP); HTTP versiyonu Keycloak ile korumalı; Claude Code/VS Code'a bağlama + aynı server'ı kullanan .NET MCP client.

#### M15.7 Microsoft Foundry & Azure OpenAI · `07-microsoft-foundry`
- **Microsoft Foundry** (Ocak 2026'da Azure AI Foundry'den yeniden adlandırıldı): proje, model kataloğu (OpenAI, Anthropic Claude, Meta, Mistral, Phi...), deployment türleri, kota ve TPM
- SDK'lar: OpenAI .NET SDK / `Azure.AI.OpenAI`, `Azure.AI.Projects`; **Entra ID ile anahtarsız kimlik doğrulama** ⭐
- Azure AI Content Safety, prompt shields
- Foundry Agent Service'e giriş (→ S9.1, S9.7)
- Azure AI Search entegrasyonu
- Maliyet: pay-as-you-go vs provisioned throughput
- Sertifika notu: **AI-102 emekliye ayrıldı → AI-103 (Azure AI App & Agent Developer)**
- 🛠 **Kod:** M15.5'teki RAG servisini Foundry modelleri + Azure AI Search + managed identity ile Azure'a taşıma (`azd` + Bicep).

#### M15.8 Agentic Coding Workflow · `08-agentic-coding`
> 2026 mülakatlarında: "AI araçlarını nasıl kullanıyorsun, sadece kod mu ürettiriyorsun yoksa süreci mi yönetiyorsun?"

- **Plan → Act → Verify** döngüsü: önce plan onayı, sonra uygulama, sonra test/derleme ile doğrulama
- Coding agent için context engineering: ilgili dosyalar, konvansiyonlar, test beklentisi; büyük görevi doğrulanabilir küçük adımlara bölmek
- **Proje talimat dosyaları:** `CLAUDE.md`, `AGENTS.md`, `.github/copilot-instructions.md` — build/test komutları, mimari kısıtlar
- **Custom skill / slash command**'lar ile tekrarlanan iş akışlarını standartlaştırma
- MCP server'ları ile agent'ı iç sistemlere bağlama (issue tracker, DB, CI)
- **İzin katmanları ve hook'lar:** hangi komut otomatik, hangisi onaylı; commit öncesi lint/test
- Subagent'lar ve worktree izolasyonu ile paralel çalışma
- AI'ın ürettiği diff'i review etmek: testler guardrail'dir; güvenlik, veri erişimi ve ödeme mantığında **zorunlu insan incelemesi**
- SDLC'de AI: test üretimi (edge case keşfi), PR açıklaması, ilk geçiş code review, legacy kodu haritalama
- **Mülakat perspektifi** ⭐ — en güçlü cevap: hangi işlerde kullandığın, nasıl doğruladığın, **nerede kullanmamayı seçtiğin** ve neden
- 🛠 **Kod:** Bu repo için örnek `CLAUDE.md`, "yeni konu klasörü oluştur" skill'i ve commit öncesi `dotnet format` + test çalıştıran hook içeren yapılandırma.

🎯 **Mülakat:** "RAG nedir, ne zaman fine-tuning yerine?" · "Chunking stratejisi nasıl seçilir?" · "Vector index türleri ve trade-off'ları?" · "Function calling nasıl çalışır?" · "MCP nedir, hangi problemi çözer?" · "LLM çağrısını production'da nasıl sağlamlaştırırsın?" · "AI coding agent'ı nasıl kullanıyorsun?"

---

### Mid Capstone: E-ticaret Katalog & Sipariş Platformu

> 📁 `02-mid/capstone/` · ⏱ ~4 hafta
> Mid seviyesinin bitiş çizgisi: birden fazla servis, birden fazla veri deposu, gerçek kimlik yönetimi ve gözlemlenebilirlik.

**Gereksinimler:**
- **Aspire AppHost** ile tüm sistemin tek komutla ayağa kalkması
- **Catalog API:** MongoDB + `HybridCache` (Redis) + arama (PostgreSQL FTS veya Elasticsearch)
- **Ordering API:** PostgreSQL + EF Core; optimistic concurrency, Idempotency-Key, cursor pagination
- **Stok kontrolü:** Ordering → Catalog arası **gRPC**
- **Notification worker:** RabbitMQ (Wolverine veya MassTransit v8) ile sipariş olayları; DLQ ve idempotent consumer
- **Kimlik:** Keycloak + SPA + **BFF**; servisler arası client credentials
- **Canlı sipariş durumu:** SignalR veya SSE
- **Observability:** OpenTelemetry ile servisler ve kuyruk boyunca uçtan uca trace; RED dashboard'u
- **Test:** Testcontainers integration testleri, bir Pact kontratı, Playwright smoke testi
- **CI/CD:** GitHub Actions → Azure Container Apps; managed identity + Key Vault, OIDC ile secret'sız deploy
- **AI:** Ürün kataloğu üzerinde RAG tabanlı "ürün asistanı" + sipariş sorgulayan MCP server
- README: mimari diyagram, alınan kararlar ve alternatifleri (ADR formatına giriş)

**Çıkış kriteri:** Capstone tamam + I6'daki Mid mock mülakatı kapalı kitap geçildi + D4, D6–D9'da LeetCode Medium seviyesinde rahatlık.

---

## Seviye 3 — Senior

> **Hedef:** Trade-off'ları gerekçelendirmek, sistem tasarlamak, production'ı yönetmek ve ekibe teknik yön vermek. "Nasıl çalışır"dan **"hangi koşulda neyi neden seçerim"**e geçiş.
> **Süre:** ~8 ay (günde 2-3 saat, DSA dahil) · **Klasör:** `03-senior/` · **Önkoşul:** Mid capstone
> Senior mülakatında doğru cevap çoğu zaman **"duruma göre değişir — şu koşullarda şunu, şu koşullarda bunu seçerim, çünkü..."** cümlesidir. Tek doğru cevap veren aday kırmızı bayraktır.

---

### S1. CLR & Runtime Derinlemesine

> 📁 `03-senior/01-runtime-internals/` · ⏱ ~2.5 hafta · Önkoşul: M2
> Kodun çalıştığı makinenin iç yapısı. Performans mühendisliğinin (S2) zemini.

#### S1.1 JIT, Tiered Compilation & PGO · `01-jit-tiered-pgo`
- IL makine kodu değildir → **JIT** metodu **ilk çağrıldığında** native koda çevirir; method stub / pre-stub mekanizması
- **Tiered compilation:** Tier 0 (hızlı derle, az optimize) → Tier 1 (sık çağrılan metodları tam optimizasyonla yeniden derle); **OSR** (on-stack replacement)
- **Dynamic PGO** (.NET 8+ varsayılan): guarded devirtualization, sıcak/soğuk kod düzeni, daha akıllı inline
- JIT optimizasyonları: inlining, bounds check elimination, constant folding, dead code elimination, loop hoisting, devirtualization, SIMD intrinsics, escape analysis ile stack allocation (.NET 10)
- Release build'i debug etmek neden zor: inline edilen metodlar, "optimized away" değişkenler
- `[MethodImpl(NoInlining / AggressiveInlining / AggressiveOptimization)]` — ne zaman meşru
- ReadyToRun ile JIT yükünü öne almak; Native AOT ile tamamen kaldırmak
- Runtime davranışını değiştiren ortam değişkenleri: `DOTNET_TieredCompilation`, `DOTNET_TieredPGO`, `DOTNET_ReadyToRun`
- 🛠 **Kod:** Sık çağrılan bir metodun tiered/PGO açık ve kapalı startup + steady-state performansı; JIT disassembly (`DOTNET_JitDisasm`) ile üretilen makine kodunu okuma.

#### S1.2 Assembly Loading & Type Loading · `02-assembly-loading`
- **`AssemblyLoadContext`:** default ALC, custom ve **collectible** ALC; eski `AppDomain` modelinin yerini alan izolasyon
- Plugin mimarisi: izole yükleme, bağımlılık çakışması, plugin'i bellekten boşaltma
- Probing: uygulama klasörü → `deps.json` → NuGet fallback; `Resolving` event'i
- Assembly versiyon çakışması ve Core'daki çözümü
- Type loading: tipin ilk kullanımda yüklenmesi, method table
- **Static constructor ne zaman çalışır** ⭐ — `beforefieldinit` semantiği ve CLR'ın thread-safety garantisi
- Uygulama sonlanması: `Main` dönüşü, `Environment.Exit`, `ProcessExit`, finalizer'ların **garantili çalışmaması**
- 🛠 **Kod:** Çalışma anında yüklenip boşaltılabilen plugin sistemi (collectible ALC); `beforefieldinit` farkını gösteren deney.

#### S1.3 Bellek Yönetimi & GC · `03-memory-gc` ⭐
- Managed heap, **generation'lar** (Gen 0/1/2) ve generational hipotez
- **Large Object Heap** (85 KB eşiği), fragmentation, LOH compaction; **Pinned Object Heap**
- **Workstation vs Server GC**; concurrent/background GC
- **Regions** (.NET 7+) ve **DATAS** (dinamik heap adaptasyonu, .NET 9+ Server GC'de varsayılan) — container'da bellek davranışı
- GC pause türleri, latency mode'ları (`SustainedLowLatency`), `GCSettings`
- Container'da GC: CPU/bellek limitleri, `GCHeapHardLimit`, `GCHeapAffinitizeMask`
- Finalization queue, finalizer'ın maliyeti, `SafeHandle`
- GC ETW event'leri ve `dotnet-counters` ile GC metrikleri (% time in GC, allocation rate, gen sizes)
- 🛠 **Kod:** Aynı yük altında Workstation, Server ve Server+DATAS GC karşılaştırması (bellek, throughput, p99 pause); LOH fragmentation reprodüksiyonu.

#### S1.4 Native AOT & Trimming · `04-native-aot-trimming`
- Native AOT: JIT yok, tek native binary, çok hızlı cold start, düşük bellek
- **Kısıtlar:** `Assembly.Load`, `Reflection.Emit`, dinamik kod üretimi; trim uyumsuz kütüphaneler
- Trimming: `TrimMode`, **IL2xxx / IL3xxx uyarılarını çözmek**, `[DynamicallyAccessedMembers]`, `[RequiresUnreferencedCode]`
- **Source generator'ların AOT'taki rolü:** JSON, logging, regex, configuration binding, Minimal API request delegate generator
- AOT'un kaybettirdikleri: dynamic PGO, build süresi, platform-spesifik çıktı
- Serverless, CLI ve sidecar senaryolarında AOT; ASP.NET Core'da AOT uyumlu şablon (`webapiaot`)
- 🛠 **Kod:** Reflection ağırlıklı bir API'yi adım adım AOT uyumlu hale getirme: uyarıları kapatma, source-gen serializer, sonuçta boyut/cold start/bellek karşılaştırması.

#### S1.5 Reflection, Source Generators & Analyzer'lar · `05-reflection-sourcegen`
- `Type`, `MethodInfo`, `PropertyInfo`, `Activator.CreateInstance`; reflection'ın maliyeti ve cache'leme
- Custom attribute yazma/okuma
- `Expression.Compile()` ve `UnsafeAccessor` (.NET 8+) — reflection'a hızlı alternatifler
- **Incremental source generator** yazma: syntax provider, semantic model, caching ve performans
- Yerleşik generator'lar: `[JsonSerializable]`, `[LoggerMessage]`, `[GeneratedRegex]`, options validation, configuration binder
- Interceptor'lar (derleme zamanında çağrı yönlendirme)
- **Roslyn analyzer + code fix** yazma; analyzer'ı NuGet ile dağıtma
- 🛠 **Kod:** `[AutoToString]` attribute'u için incremental source generator + projeye özel bir kuralı (ör. "`DateTime.Now` kullanma") zorlayan analyzer ve code fix.

🎯 **Mülakat:** "Tiered compilation nedir?" · "Dynamic PGO ne kazandırır?" · "GC generation'ları neden var?" · "Server GC ile Workstation GC farkı?" · "LOH nedir, neden sorun çıkarır?" · "Static constructor tam olarak ne zaman çalışır?" · "Native AOT'a geçerken reflection nasıl ele alınır?" · "Source generator neden reflection'a tercih edilir?"

---

### S2. Performans Mühendisliği

> 📁 `03-senior/02-performance/` · ⏱ ~3 hafta · Önkoşul: S1
> Kural: **"Ölçmeden optimize etme."** Her konunun kodu bir ölçüm + öncesi/sonrası raporuyla biter.

#### S2.1 Benchmarking · `01-benchmarking`
- **BenchmarkDotNet:** `[MemoryDiagnoser]`, `[Params]`, baseline, job'lar (farklı runtime'ları karşılaştırma), `[DisassemblyDiagnoser]`
- Mikro-benchmark tuzakları: dead code elimination, warm-up, gürültü, yanlış ortam (Debug, laptop pil modu)
- Sonuç okuma: mean, error, stddev, allocated, Gen0 sayısı
- Profiling vs benchmarking farkı
- 🛠 **Kod:** String birleştirme, koleksiyon arama ve serialization üzerine doğru kurulmuş benchmark seti + bilerek yanlış kurulmuş (dead code elimination'a uğrayan) bir benchmark.

#### S2.2 Span, Memory & Yüksek Performans Tipleri · `02-span-memory`
- **`Span<T>` / `ReadOnlySpan<T>`** — stack-only, allocation'sız dilimleme
- **`Memory<T>`** — async'te neden `Span` kullanılamaz
- `ref struct` kısıtları, `scoped`, `ref` return / `ref` local, `ref readonly`
- `stackalloc` — güvenli kullanım sınırları
- `SearchValues<T>`, `FrozenDictionary`/`FrozenSet`, `CollectionsMarshal`, inline array
- SIMD: `Vector<T>`, `Vector128/256/512`, `TensorPrimitives`
- `Utf8JsonReader` / `Utf8JsonWriter` ile allocation'sız JSON
- 🛠 **Kod:** Log satırı parser'ının `string.Split` → `Span` → `SearchValues` evrimi; her adımın benchmark'ı.

#### S2.3 Allocation Azaltma · `03-allocation-reduction`
- Allocation'ın gerçek maliyeti: GC baskısı ve pause
- `ArrayPool<T>`, `MemoryPool<T>`, `ObjectPool<T>`
- `struct`, `readonly struct`, `in` parametre; defensive copy tuzağı
- String: interpolation handler, `string.Create`, `StringBuilder` havuzlama
- Sıcak yolda LINQ ve closure allocation'ı; `static` lambda
- `ValueTask` ile async allocation azaltma
- Boxing avı
- 🛠 **Kod:** Yoğun bir endpoint'in allocation profilini çıkarıp (`dotnet-counters`, BenchmarkDotNet) adım adım sıfıra yaklaştırma.

#### S2.4 Profiling & Diagnostics · `04-profiling-diagnostics`
- CLI araçları: **`dotnet-counters`**, **`dotnet-trace`**, **`dotnet-dump`**, `dotnet-gcdump`, `dotnet-stack`, **`dotnet-monitor`** (container/Kubernetes'te)
- EventPipe, EventSource, `System.Diagnostics.Metrics`
- GUI araçları: Visual Studio Profiler, PerfView, JetBrains dotTrace/dotMemory, Speedscope
- **Flame graph okuma** ⭐
- Production'da düşük etkili profiling; dump almanın riskleri (boyut, PII)
- 🛠 **Kod:** CPU'yu yakan bir endpoint için `dotnet-trace` ile trace alıp flame graph'ta sıcak yolu bulma ve düzeltme.

#### S2.5 Memory Leak Avı · `05-memory-leaks`
- **Yaygın sebepler:** event handler aboneliği, static koleksiyon, sınırsız cache, dispose edilmeyen kaynak, closure ile beklenmedik referans, timer, `HttpClient` yanlış kullanımı
- Metodoloji: belirti (artan working set) → doğrulama (`dotnet-counters`) → iki gcdump/dump karşılaştırması → GC root analizi
- Managed vs unmanaged leak ayrımı
- Container'da bellek limiti ve **OOMKilled** teşhisi
- 🛠 **Kod:** Üç farklı leak içeren bir servis — her birini `dotnet-gcdump` ve dump analiziyle bulup düzeltme raporu.

#### S2.6 Uçtan Uca API Performansı & Yük Testi · `06-api-load-testing`
- Yük testi araçları: **k6**, **NBomber** (C#), Bombardier, Crank
- Test türleri: load, stress, soak, spike
- **Latency percentile'ları (p50/p95/p99)** ve tail latency; coordinated omission
- Darboğaz analizi: CPU, thread pool, DB connection pool, lock contention, GC, dış servis
- **Little's Law** ile kapasite ve pool boyutu hesabı
- Kestrel limitleri, response compression (Brotli, .NET 11'de Zstandard), output caching
- Streaming yanıt (`IAsyncEnumerable`) ile bellek düşürme
- **CI'da performans regresyon tespiti** (baseline karşılaştırma)
- SLO hedefleriyle test kriteri belirleme
- 🛠 **Kod:** Mid capstone'a k6 yük testi seti; darboğazı bulup gidermenin raporu (öncesi/sonrası p99 ve throughput); CI'da otomatik regresyon kontrolü.

🎯 **Mülakat:** "Production'da bir servisin belleği sürekli artıyor — nasıl teşhis edersin?" ⭐ · "`Span<T>` neden async metodda kullanılamaz?" · "Flame graph nasıl okunur?" · "p99 neden ortalamadan önemli?" · "Bir API yük altında yavaşlıyor — hangi sırayla neye bakarsın?" · "BenchmarkDotNet olmadan ölçüm neden yanıltıcı?"

---

### S3. Concurrency: Bellek Modeli & Dağıtık Koordinasyon

> 📁 `03-senior/03-concurrency-advanced/` · ⏱ ~2 hafta · Önkoşul: M4

#### S3.1 .NET Bellek Modeli & Lock-free Programlama · `01-memory-model-lockfree`
- **Instruction reordering** (derleyici, JIT, CPU); x64 vs ARM64 farkı
- `volatile`, `Volatile.Read/Write`, `Interlocked.MemoryBarrier` — ne garanti eder, ne etmez
- `Interlocked.CompareExchange` ile **CAS döngüsü**; lock-free stack
- **ABA problemi**
- **Torn read/write** — 64-bit değerlerin 32-bit platformda atomik olmaması
- **Double-checked locking** — doğru implementasyonu ve `Lazy<T>` alternatifi
- **False sharing** ve cache line padding
- Immutability ile concurrency'den kaçınmak (çoğu zaman en iyi çözüm)
- 🛠 **Kod:** Lock-free stack + ABA senaryosu; false sharing'i gösteren benchmark ve padding ile düzeltmesi.

#### S3.2 Concurrency Problemleri · `02-concurrency-problems`
- **Race condition** — tespit ve çözüm
- **Deadlock** — dört Coffman koşulu, kilit sıralamasıyla önleme ⭐
- **Livelock**, **starvation**, priority inversion
- Check-then-act ve read-modify-write hataları
- Test etmek: stres testi, deterministik scheduling fikri
- 🛠 **Kod:** Her problem türü için reprodüksiyon + düzeltme + tekrar eden stres testi.

#### S3.3 Yüksek Performanslı I/O & Pipeline'lar · `03-pipelines-dataflow`
- **`System.IO.Pipelines`** — buffer yönetimi, backpressure, sıfır-kopya I/O (Kestrel'in temeli)
- **TPL Dataflow:** `BufferBlock`, `TransformBlock`, `ActionBlock`, `BatchBlock`; bounded capacity ve paralellik ayarı
- Channels vs Dataflow vs Rx — ne zaman hangisi
- 🛠 **Kod:** TCP üzerinden satır tabanlı protokol parser'ı (Pipelines); çok aşamalı ETL akışı (Dataflow) ve throughput ölçümü.

#### S3.4 Dağıtık Kilitleme & Leader Election · `04-distributed-locking`
- Tek instance'ta `lock` yeterli, çoklu instance'ta değil
- Redis tabanlı lock (`SET NX PX`), **Redlock ve eleştirisi** (Martin Kleppmann) ⭐
- **Fencing token** — süresi dolan kilidin sahibinin zarar vermesini engellemek
- PostgreSQL **advisory lock**, SQL Server `sp_getapplock`, Azure Blob lease
- `DistributedLock` kütüphanesi
- **Leader election:** lease tabanlı, Kubernetes lease
- **Idempotency ile kilit ihtiyacını ortadan kaldırmak** — çoğu zaman tercih edilen yol
- Optimistic concurrency vs distributed lock
- 🛠 **Kod:** Üç instance'lı worker'da aynı job'un tek sefer çalışmasını Redis lock, Postgres advisory lock ve idempotency ile sağlayan üç versiyon; GC pause simülasyonuyla fencing token'ın neden gerektiğini gösteren test.

#### S3.5 Actor Model & Orleans · `05-actor-model-orleans`
- Actor model: paylaşılan state yok, mesajla iletişim, tek thread'li işleme
- **Microsoft Orleans:** virtual actor (grain), otomatik aktivasyon/deaktivasyon, grain persistence, streams, reminders, cluster
- Akka.NET, Proto.Actor karşılaştırması
- Ne zaman uygun: çok sayıda bağımsız, durum tutan varlık (oyun oturumu, IoT cihazı, sepet)
- 🛠 **Kod:** Orleans ile alışveriş sepeti ve canlı açık artırma servisi; çoklu silo'da çalıştırma.

#### S3.6 Concurrency Debugging · `06-concurrency-debugging`
- Visual Studio Parallel Stacks / Parallel Tasks
- **Hang analizi:** `dotnet-dump` + SOS (`clrstack`, `syncblk`, `dumpasync`, `threadpool`)
- `dotnet-trace` ile async akış ve thread pool olayları
- Thread pool starvation'ı doğrulamak
- Stres ve chaos testiyle concurrency bug'larını yüzeye çıkarmak
- 🛠 **Kod:** Deadlock'a giren bir servisten dump alıp SOS ile kilit zincirini bulma alıştırması (adım adım rehber).

🎯 **Mülakat:** "`volatile` ne garanti eder?" · "Double-checked locking'i doğru yaz" · "Deadlock'un dört koşulu?" · "Redis ile distributed lock güvenli mi?" ⭐ · "Fencing token nedir?" · "Ne zaman Orleans/actor model?"

---

### S4. Ölçekte Veri

> 📁 `03-senior/04-data-at-scale/` · ⏱ ~3.5 hafta · Önkoşul: M6, M8
> J8 (kavram) → M6/M8 (mekanizma) → **S4 (ölçek ve dağıtık veri).** Ana referans: *Designing Data-Intensive Applications*.

#### S4.1 Veritabanı İç Yapısı: Storage Engine'ler · `01-storage-engines`
- Sayfa (page), buffer pool, **WAL** ve checkpoint — durability nasıl sağlanır
- **B-tree vs LSM-tree** ⭐ — PostgreSQL/SQL Server vs RocksDB/Cassandra; read/write/space amplification
- MVCC implementasyonları: PostgreSQL (yeni satır versiyonu + VACUUM) vs undo log (MySQL InnoDB, Oracle) vs SQL Server version store
- **Row store vs column store** — OLTP vs OLAP; sıkıştırma
- Index'lerin disk üzerindeki yapısı; neden rastgele UUID (v4) primary key B-tree'yi parçalar, UUIDv7 neden çözer
- 🛠 **Kod:** Mini bir LSM-tree key-value store (memtable + SSTable + compaction) ve basit B-tree; yazma/okuma karakteristiklerinin karşılaştırması.

#### S4.2 Replication · `02-replication`
- **Leader-follower:** senkron, asenkron, semi-senkron
- **Multi-leader** ve çakışma çözümü; **leaderless** (quorum, N/R/W)
- **Replication lag** sorunları: read-your-writes, monotonic reads, consistent prefix ⭐
- Failover, **split brain**, fencing
- PostgreSQL streaming vs logical replication, Patroni ile HA
- .NET tarafı: okuma/yazma ayrımı (ayrı `DbContext`/connection string, Npgsql multi-host `Target Session Attributes`)
- 🛠 **Kod:** Compose ile PostgreSQL primary + replica; okumaları replica'ya yönlendiren .NET uygulaması; replication lag yüzünden "yazdığımı göremiyorum" hatası ve read-your-writes çözümü.

#### S4.3 Partitioning & Sharding · `03-partitioning-sharding`
- Partitioning (tek DB içinde) vs **sharding** (birden fazla DB) vs replication
- Stratejiler: range, hash, directory/lookup, coğrafi
- **Shard key seçimi** ⭐ — kardinalite, dağılım, sorgu desenleri; geri dönüşü en zor karar
- **Hot partition / celebrity problemi**
- Resharding acısı; **consistent hashing** (→ D10)
- Cross-shard sorgu, join ve transaction; global secondary index
- Multi-tenancy modelleri: tenant başına DB vs şema vs satır (RLS) — izolasyon/maliyet trade-off'u
- Araçlar: PostgreSQL declarative partitioning, Citus, MongoDB sharding, Cosmos DB partition
- 🛠 **Kod:** Uygulama seviyesinde tenant-bazlı sharding yapan .NET servisi (shard map + routing + yeni shard ekleme); consistent hashing ile anahtar dağılımı simülasyonu.

#### S4.4 Tutarlılık Modelleri, CAP & PACELC · `04-consistency-cap`
- Tutarlılık modelleri: **linearizable**, sequential, causal, read-your-writes, eventual
- **CAP'i doğru anlamak** ⭐ — "üçünden ikisi" değil, ağ bölünmesi anında C ile A arasında seçim
- **PACELC** — bölünme yokken de latency ile consistency arasında seçim var
- **Consensus sezgisi:** Raft (leader election, log replication), quorum; Paxos'un varlığı
- Gerçek sistemlerin sınıflandırması: PostgreSQL, MongoDB (write/read concern ile ayarlanabilir), Cassandra (tunable), Cosmos DB (5 seviye)
- 🛠 **Kod:** Üç node'lu basitleştirilmiş Raft simülasyonu (leader election + log replication) ve ağ bölünmesi senaryosu.

#### S4.5 Ölçekte Sorgu Optimizasyonu · `05-query-optimization`
- Yavaş sorguyu bulmak: `pg_stat_statements`, Query Store, APM, EF Core logging
- Plan regresyonları ve istatistik yönetimi
- **Connection pool boyutlandırma** — "daha büyük pool = daha hızlı" yanılgısı; Little's Law
- Okuma ölçekleme: read replica, materialized view, **CQRS read model**, cache
- Yazma ölçekleme: batching, `COPY`, asenkron yazma, kuyruk ile tamponlama
- **OLTP ve OLAP'ı ayırmak:** analitik sorguları kolon tabanlı depoya taşımak (ClickHouse, DuckDB, Microsoft Fabric)
- N+1'in her biçimi: EF, GraphQL, MongoDB `$lookup`
- 🛠 **Kod:** Ağır raporlama sorgusunu OLTP veritabanından materialized view'a ve ardından kolon tabanlı depoya taşıyıp latency/yük etkisini ölçme.

#### S4.6 Redis & MongoDB Ölçekte · `06-redis-mongodb-at-scale`
- **Redis:** replication, Sentinel, **Cluster** (16384 hash slot, `MOVED`/`ASK`, multi-key işlem kısıtı, hash tag'ler); hot key, big key, bellek fragmentation; production persistence ayarları
- Redis vs Valkey vs Garnet vs Azure Managed Redis — karar
- **MongoDB:** replica set seçimleri (election), `majority` write concern, read concern, causal consistency session'ları
- MongoDB sharding: shard key, chunk, balancer, zone sharding; şema evrimi
- 🛠 **Kod:** Compose ile Redis Cluster ve MongoDB sharded cluster; hash tag ile multi-key işlem, kötü shard key'in yarattığı dengesizliğin gözlemi.

#### S4.7 CDC & Veri Senkronizasyonu · `07-cdc`
- **Change Data Capture** kavramı; dual write problemine kalıcı çözüm
- **Debezium** + PostgreSQL logical decoding (WAL)
- CDC ile outbox (→ S6.3), DB → arama index'i / cache / read model senkronizasyonu
- Sıralama, idempotency, yeniden işleme (backfill/replay)
- 🛠 **Kod:** PostgreSQL → Debezium → Kafka → .NET consumer → Elasticsearch senkronizasyonu; consumer'ı durdurup yeniden başlatınca kaldığı yerden devam ettiğini gösteren test.

#### S4.8 Veritabanı Seçimi & Polyglot Persistence · `08-database-selection`
- Karar çerçevesi: veri modeli, erişim deseni, tutarlılık, ölçek, gecikme, operasyon yükü, maliyet, ekip yetkinliği
- Managed vs self-hosted
- **"Her şey için PostgreSQL" tartışması** ⭐ — JSONB, pgvector, FTS, kuyruk (`SKIP LOCKED`) ile ne kadar ileri gidilebilir, nerede durulmalı
- Polyglot persistence'ın gizli maliyetleri: tutarlılık, operasyon, beceri dağılımı
- Veritabanları arası göç stratejisi
- 🛠 **Kod:** Örnek bir ürün için (sosyal ağ / IoT / e-ticaret) veri deposu seçimini gerekçelendiren karar dokümanı (ADR formatında) + prototip benchmark'ları.

🎯 **Mülakat:** "B-tree ile LSM-tree farkı?" · "Replication lag'i kullanıcıya nasıl hissettirmezsin?" · "Shard key nasıl seçilir?" ⭐ · "CAP teoremini doğru anlat" · "Read-your-writes nasıl sağlanır?" · "Connection pool'u büyütmek neden her zaman çözüm değil?" · "Dual write problemini nasıl çözersin?" · "Postgres her şeye yeter mi?"

---

### S5. Mimari Derinlemesine: DDD, Event Sourcing & Modular Monolith

> 📁 `03-senior/05-architecture-advanced/` · ⏱ ~3 hafta · Önkoşul: M11

#### S5.1 Domain-Driven Design · `01-ddd`
- **Strategic DDD:** Ubiquitous Language, **Bounded Context**, Context Map (shared kernel, customer/supplier, anti-corruption layer...), subdomain türleri (core / supporting / generic)
- **Tactical DDD:**
  - Entity vs **Value Object** ⭐
  - **Aggregate** ve Aggregate Root — sınır belirleme, transaction sınırı ile ilişkisi
  - Aggregate tasarım kuralları: küçük tut, diğer aggregate'lere ID ile referans ver, bir transaction'da bir aggregate
  - Domain Event, Domain Service, Factory, Repository (DDD anlamında)
- Anemic vs rich domain model; invariant'ların domain'de korunması
- **Domain Model mi Transaction Script mi** ⭐ (→ M11.3) — DDD'nin bedeli ve ne zaman ödenmeye değer; **strategic DDD (bounded context, ubiquitous language) tactical DDD olmadan da değerlidir**
- Domain event → integration event dönüşümü
- EF Core ile DDD: private setter, backing field, owned/complex type, value object eşleme
- Event Storming ile modelleme
- DDD ne zaman **aşırı** (basit CRUD)
- 🛠 **Kod:** Sipariş bounded context'inin rich domain modeli (aggregate, value object, domain event) + EF Core eşlemesi + invariant testleri.

#### S5.2 CQRS Derinlemesine & Event Sourcing · `02-cqrs-event-sourcing`
- Ayrı read store ile tam CQRS; projection ve **eventual consistency**'yi kullanıcıya yansıtmak
- **Event Sourcing:** append-only event log, state'in event'lerden türetilmesi, replay, snapshot
- Projection'lar (inline, async), yeniden oluşturma
- **Event versioning ve upcasting**
- Araçlar: **Marten** (PostgreSQL üzerinde), **KurrentDB** (eski EventStoreDB)
- Ne zaman event sourcing: denetim izi, zamansal sorgu, karmaşık iş akışı — ne zaman **değil**
- 🛠 **Kod:** Banka hesabı aggregate'i Marten ile event-sourced; iki farklı projection, snapshot ve event şeması değişikliğinde upcasting.

#### S5.3 Modular Monolith · `03-modular-monolith`
- Microservice'e geçmeden önce doğru adım
- Modül sınırları = bounded context
- Modüller arası iletişim: public API (in-process) vs event
- Veritabanı ayrımı: **schema-per-module**
- Modülerliği derleme zamanında zorlamak: ayrı projeler, `internal`, architecture testleri
- Monolith → microservice göç yolu
- **"Microservice'e ihtiyacın var mı?"** — dürüst değerlendirme ⭐
- 🛠 **Kod:** Katalog, sipariş ve ödeme modüllü modular monolith; modül sınırlarını zorlayan architecture testleri; bir modülün ayrı servise çıkarılması.

#### S5.4 Mimari Stiller — Tam Karşılaştırma · `04-architecture-styles`
- Hexagonal / Onion / Clean özeti (→ M11.3) — aynı fikrin farklı adları
- **N-Tier / Layered** — en basit, en az korumalı
- **Modular Monolith** — modülerlik disiplini, dağıtık sistemin operasyon yükü olmadan
- **Microservices** — bağımsız deploy, veri sahipliği, ağ üzerinden iletişim
- **SOA vs Microservices** — paylaşılan ESB ve ağır kontratlar vs hafif protokoller ve merkezsiz yönetişim
- **Event-Driven Architecture** — gevşek bağlı, olay yayınlayan/dinleyen servisler
- **Serverless / FaaS** — scale-to-zero, cold start, stateless zorunluluğu
- **Space-Based / Cell-Based** — aşırı ölçek için bağımsız hücrelere bölme
- Micro-frontend (kısa not)
- **Karar tablosu:**

  | Senaryo | Önerilen mimari |
  |---------|------------------|
  | Küçük CRUD servis, tek takım | Layered veya Vertical Slice |
  | Orta ölçek, tek deployment, ileride bölünebilmeli | **Modular Monolith** |
  | Karmaşık domain, framework/DB'den bağımsızlık öncelik | Clean / Hexagonal / Onion |
  | Çok takım, bağımsız deploy/ölçek ihtiyacı | Microservices |
  | Yüksek hacimli, gevşek bağlı iş akışları | Event-Driven |
  | Düzensiz/patlamalı trafik, düşük operasyon yükü | Serverless |
  | Aşırı ölçek, hata izolasyonu kritik | Cell-Based |

- **Mülakat perspektifi:** "Hangi mimariyi kullanırsın?" sorusunun tek doğru cevabı yok; gereksinimi netleştirip trade-off'u gerekçelendirmek beklenir.
- 🛠 **Kod:** Aynı küçük domain'in üç stilde (modular monolith, microservices, serverless) iskeleti + her birinin operasyonel maliyet notları.

#### S5.5 Mimari Yönetişim: ADR, C4 & Fitness Functions · `05-architecture-governance`
- **ADR (Architecture Decision Record)** — bağlam, karar, alternatifler, sonuçlar
- **C4 modeli** (Context, Container, Component, Code) — Structurizr, Mermaid
- **Architecture testleri:** NetArchTest, ArchUnitNET — "Domain, Infrastructure'a referans veremez", "Controller `DbContext` kullanamaz"
- **Fitness function**'lar: performans, bağımlılık, güvenlik kurallarını CI'da otomatik doğrulamak
- RFC/tasarım dokümanı süreci, teknoloji radarı
- 🛠 **Kod:** Mid capstone için ADR seti, C4 diyagramları ve CI'da çalışan architecture testleri.

#### S5.6 Legacy Modernizasyon · `06-legacy-modernization`
- **Strangler fig** pattern; **anti-corruption layer**; branch by abstraction
- **.NET Framework → .NET 10 göçü:** .NET Upgrade Assistant, AI destekli modernizasyon araçları (GitHub Copilot app modernization), `System.Web` adapters
- **YARP ile kademeli göç** — eski ve yeni uygulamanın aynı domain arkasında yan yana yaşaması
- WCF → CoreWCF veya gRPC; Web Forms → Razor/Blazor
- Paylaşılan veritabanını ayrıştırmak
- Risk yönetimi: karakterizasyon testleri, özellik eşitliği, kademeli trafik
- 🛠 **Kod:** Küçük bir .NET Framework uygulamasının YARP + strangler fig ile endpoint endpoint .NET 10'a taşınması.

🎯 **Mülakat:** "Aggregate sınırını nasıl belirlersin?" ⭐ · "Entity ile value object farkı?" · "Event sourcing ne zaman mantıklı?" · "Modular monolith mi microservices mi?" · "Mimari kararlarını nasıl belgelersin?" · "Legacy bir .NET Framework uygulamasını kesintisiz nasıl taşırsın?"

---

### S6. Dağıtık Sistemler & Microservices

> 📁 `03-senior/06-distributed-systems/` · ⏱ ~3 hafta · Önkoşul: M12, S5
> Altyapı (repo köküne eklenecek `docker-compose.yml` servisleri): Kafka, RabbitMQ, PostgreSQL, Redis, OpenTelemetry Collector.

#### S6.1 Microservice Temelleri & Ayrıştırma · `01-microservices`
- Ne zaman microservice, ne zaman **monolit** — ekip yapısı (Conway yasası), bağımsız deploy, ölçek profili ⭐
- Gizli maliyetler: dağıtık debugging, veri tutarlılığı, operasyon yükü, ağ gecikmesi
- Ayrıştırma: bounded context, iş yeteneği, veri sahipliği
- **Database-per-service** ve servisler arası sorgu problemi (API composition, CQRS read model)
- Senkron (HTTP/gRPC) vs asenkron (event) iletişim; zincirleme senkron çağrıların riski
- Service discovery; client-side vs server-side load balancing
- **Dapr** building block yaklaşımı; **Aspire** ile yerel orkestrasyon
- Distributed monolith anti-pattern'i
- 🛠 **Kod:** S5.3'teki modular monolith'ten ödeme modülünü ayrı servise çıkarma; ortaya çıkan yeni problemlerin (ağ hatası, tutarlılık, trace) listesi ve çözümleri.

#### S6.2 Event-Driven Architecture · `02-event-driven`
- **Domain event vs integration event** — sınır, serialization ve sözleşme farkı ⭐
- Event notification vs **event-carried state transfer** vs event sourcing (→ S5.2)
- Choreography: avantajı (gevşek bağ) ve dezavantajı (akış görünmez)
- **Event şema evrimi:** versiyonlama, schema registry, geriye uyumluluk; CloudEvents, AsyncAPI
- **Eventual consistency**'yi UI'da ve kullanıcıya yansıtmak
- Event storming ile modelleme
- 🛠 **Kod:** Sipariş → stok → bildirim akışının event-carried state transfer ile kurulması; event şemasının v1 → v2 geçişinde eski consumer'ların çalışmaya devam etmesi.

#### S6.3 Outbox, Inbox & Saga · `03-outbox-saga` ⭐
- **Dual-write problemi:** DB'ye yazıp mesaj göndermek neden atomik değil
- **Transactional Outbox** — aynı transaction'da outbox tablosuna yazma, ayrı süreçle yayınlama; polling vs CDC tabanlı (→ S4.7)
- **Inbox pattern** ile consumer idempotency
- Wolverine / MassTransit yerleşik outbox desteği
- **Saga pattern:**
  - Choreography (event zinciri) vs **orchestration** (merkezi koordinatör)
  - **Compensating transaction** tasarımı
  - Saga state machine (MassTransit), Durable Functions, Dapr Workflow, Temporal
- 2PC neden kaçınılır
- 🛠 **Kod:** Sipariş → ödeme → stok → kargo akışı: outbox + inbox + orchestration saga; her adımın telafisi; ödeme servisi çökertilince sistemin tutarlı kaldığını gösteren test.

#### S6.4 Resilience Desenleri · `04-resilience`
- Retry + **exponential backoff + jitter**; **retry budget** ve retry storm
- **Circuit breaker**, timeout (katmanlı timeout bütçesi), **bulkhead**, fallback, **hedging**
- **Load shedding** ve backpressure; graceful degradation
- Cascading failure — tek yavaş bağımlılığın tüm sistemi düşürmesi
- Idempotent olmayan işlemlerde retry tehlikesi
- **Chaos engineering:** Polly chaos stratejileri (fault, latency, outcome injection), Azure Chaos Studio
- 🛠 **Kod:** Yavaşlayan bir bağımlılık senaryosunda resilience'sız sistemin çöküşü vs bulkhead + circuit breaker + timeout ile ayakta kalması; chaos injection ile otomatik test.

#### S6.5 API Gateway, BFF & Service Mesh · `05-gateway-mesh`
- Gateway sorumlulukları: routing, auth, rate limit, aggregation, protokol dönüşümü
- **YARP** ile özelleştirilebilir gateway; Azure API Management; Ocelot, Kong, Envoy
- **BFF** — web/mobil için ayrı API katmanı (→ M9.5); API composition
- **Webhook tasarımı:** HMAC imzası, retry, idempotency, replay koruması
- Async API pattern'i: `202 Accepted` + status / callback
- **Service mesh** (Istio, Linkerd): mTLS, trafik yönetimi, gözlemlenebilirlik — ne zaman gerçekten gerekir
- Dapr sidecar modeli
- Gateway'in tek hata noktası olma riski
- 🛠 **Kod:** YARP gateway (JWT doğrulama, rate limit, iki servisin yanıtını birleştiren aggregation) + HMAC imzalı webhook gönderici/alıcı.

#### S6.6 Kafka & Event Streaming Derinlemesine · `06-kafka-streaming`
- Partition, key ve **sıralama garantisi**; partition sayısı seçimi
- Consumer group rebalancing (cooperative sticky), **offset commit stratejileri** ve at-least-once
- **Idempotent producer** ve Kafka transaction'ları (Kafka içinde exactly-once)
- Log compaction, retention; replay
- **Schema Registry** (Avro / Protobuf / JSON Schema)
- Stream processing kavramları: windowing, stateful işleme
- Consumer lag izleme
- Azure Event Hubs'ın Kafka uyumlu endpoint'i
- 🛠 **Kod:** Sipariş olaylarını schema registry ile yayınlayan producer; manuel offset commit'li idempotent consumer; partition rebalance sırasında mesaj kaybı/tekrarı testi.

#### S6.7 Dağıtık Sistem Teorisi · `07-distributed-theory`
- **Idempotency** — dağıtık sistemin en önemli tek kavramı ⭐
- **Dağıtık hesaplamanın 8 yanılgısı** (ağ güvenilirdir, gecikme sıfırdır...)
- İki generaller problemi; "exactly-once" efsanesi
- Saatler: fiziksel saat, clock skew, **Lamport clock**, vector clock
- 2PC ve neden kaçınılır; alternatifleri
- Correlation ID propagation ve trace (→ M13.4)
- 🛠 **Kod:** Lamport ve vector clock simülasyonu; idempotency key + inbox ile "tam olarak bir kez etki" sağlayan ödeme endpoint'i.

🎯 **Mülakat:** "Microservice'e ne zaman geçersin, ne zaman geçmezsin?" ⭐ · "Dual write problemini nasıl çözersin?" · "Saga choreography mi orchestration mı?" · "Compensating transaction örneği ver" · "Retry neden sistemi daha da kötüleştirebilir?" · "Kafka'da sıralama nasıl garanti edilir?" · "Exactly-once mümkün mü?" · "Service mesh gerçekten gerekli mi?"

---

### S7. Güvenlik Mühendisliği

> 📁 `03-senior/07-security-engineering/` · ⏱ ~3 hafta · Önkoşul: J11, M9
> Senior'dan beklenen: açıkları tek tek bilmenin ötesinde **güvenli tasarım, tehdit modelleme ve tedarik zinciri** bakışı.

#### S7.1 OWASP Top 10:2025 — .NET Karşılıkları · `01-owasp-top10`
> 2021'den sonraki ilk büyük güncelleme (Kasım 2025). Her madde: **ne, nasıl oluşur, .NET'te nasıl engellenir.**

1. **A01 Broken Access Control** (SSRF artık bu kategoride)
   - IDOR, yatay/dikey yetki yükseltme, SSRF
   - Önlem: resource-based authorization, `FallbackPolicy` ile varsayılan kilitli API, her sorguda sahiplik kontrolü; dış URL çağrılarında allowlist ve private IP (`169.254.169.254` dahil) engelleme
2. **A02 Security Misconfiguration**
   - Prod'da açık developer exception page, varsayılan şifreler, açık storage container, gereksiz endpoint
   - Önlem: ortama göre pipeline, güvenlik header'ları, `Server` header'ını kapatma, IaC ile tekrarlanabilir güvenli yapılandırma
3. **A03 Software Supply Chain Failures** (yeni — "Vulnerable Components"ın genişlemiş hali)
   - Zafiyetli/kötü niyetli paket, ele geçirilmiş CI, imzasız artefakt
   - Önlem: NuGet audit, lock file, package source mapping, SBOM, imzalı paket/image, sabitlenmiş GitHub Action SHA'ları (→ S7.6)
4. **A04 Cryptographic Failures**
   - Önlem: her yerde TLS, Data Protection API, şifre için yavaş hash, anahtarlar Key Vault'ta (→ S7.4)
5. **A05 Injection** (SQL, NoSQL, command, LDAP, XSS)
   - Önlem: EF Core LINQ / `FromSql` interpolation ile parametreleştirme, `SqlParameter`; MongoDB'de kullanıcı girdisini operatör olarak yorumlatmama; `ProcessStartInfo.ArgumentList`; Razor encoding + CSP
6. **A06 Insecure Design**
   - Önlem: **threat modeling** (→ S7.7), iş akışlarında kötüye kullanım senaryoları, iş kuralı seviyesinde rate limit
7. **A07 Authentication Failures**
   - Önlem: Identity lockout, MFA/**passkeys**, login endpoint'inde rate limiting, credential stuffing tespiti, session fixation koruması
8. **A08 Software or Data Integrity Failures**
   - Önlem: `BinaryFormatter` .NET 9'da **kaldırıldı** — güvensiz deserializer kullanma; Newtonsoft'ta `TypeNameHandling` kapalı; imzalı güncelleme ve artefakt doğrulama
9. **A09 Security Logging & Alerting Failures**
   - Önlem: audit log (kim, ne, ne zaman), başarısız login/yetki ihlali alarmları (→ M13)
10. **A10 Mishandling of Exceptional Conditions** (yeni)
    - Hata durumunda "fail-open" davranış, exception'ın yetki kontrolünü atlatması, hata mesajında bilgi sızıntısı, kaynak tükenmesi
    - Önlem: **fail securely** (hata = erişim reddi), global exception handler + generic `ProblemDetails`, authorization handler'larında exception yutmama, timeout ve limitler

- İlgili listeler: **OWASP API Security Top 10** (BOLA, BFLA, mass assignment...) ve **OWASP Top 10 for LLM Applications** (→ S9.6)
- 🛠 **Kod:** Her kategori için kasıtlı açıklı bir endpoint + exploit testi + düzeltilmiş versiyon.

#### S7.2 Zafiyet Referans Tablosu · `02-vulnerability-reference`
> OWASP kategorilerinin ötesinde, mülakatlarda **tek tek** sorulan zafiyetler.

| Açık | Nasıl oluşur | .NET'te nasıl engellenir |
|------|--------------|---------------------------|
| **SQL Injection** | Girdi SQL string'ine birleştirilir | EF Core LINQ, `FromSql` interpolation, `SqlParameter` — asla string birleştirme |
| **XSS (reflected/stored/DOM)** | Girdi HTML'e encode edilmeden basılır | Razor varsayılan encoding; `Html.Raw`'ı girdiyle kullanmamak; **CSP** |
| **CSRF** | Oturum açıkken başka site kullanıcı adına istek yapar | Anti-forgery token, `SameSite` cookie; .NET 11'de cross-origin unsafe isteklerin otomatik reddi |
| **XXE** | XML parser dış entity'leri işler | `DtdProcessing.Prohibit`; mümkünse JSON |
| **Path Traversal** | Girdiyle dosya yolu oluşturulur (`../../`) | `Path.GetFullPath` + kök dizin kontrolü; kullanıcıdan gelen dosya adını kullanmamak |
| **Command Injection** | Girdi shell komutuna birleştirilir | `ArgumentList` ile ayrı argümanlar; shell'i devre dışı bırakmak |
| **SSRF** | Sunucu kullanıcı kontrolündeki URL'ye istek atar | Allowlist, private IP engelleme, redirect takibini kapatma |
| **Insecure Deserialization** | Güvenilmeyen veri tip bilgisiyle deserialize edilir | `BinaryFormatter` yok; `System.Text.Json` + polymorphism allowlist |
| **IDOR / BOLA** | ID ile yetkisiz kaynağa erişim | Her erişimde sahiplik/yetki kontrolü — GUID tahmin edilemezliği yetki değildir |
| **Open Redirect** | `returnUrl` doğrulanmadan yönlendirilir | `Url.IsLocalUrl()`, `LocalRedirect()` |
| **Clickjacking** | Sayfa başka sitede iframe'e gömülür | `X-Frame-Options: DENY` / CSP `frame-ancestors` |
| **Mass Assignment** | DTO'da olmayan alan (`IsAdmin`) bind edilir | Ayrı input DTO'ları, entity'yi bind etmemek |
| **Race Condition / TOCTOU** | Kontrol ile işlem arasında başka istek araya girer | Atomik `UPDATE ... WHERE`, optimistic concurrency, uygun isolation |
| **ReDoS** | Regex catastrophic backtracking'e girer | Regex timeout, `RegexOptions.NonBacktracking`, `[GeneratedRegex]` |
| **Header Injection** | CRLF ile header'a enjeksiyon | Framework API'leri; girdiyi header'a koymadan doğrulama |
| **JWT zafiyetleri** | `alg: none`, zayıf secret, `aud` doğrulanmaması | Asimetrik imza, tüm claim'lerin doğrulanması, kısa ömür |
| **Hassas veri sızıntısı** | Stack trace, connection string, token log/yanıtta | Generic `ProblemDetails`, log redaksiyonu (→ M13.2) |

- 🛠 **Kod:** Tablodaki her satır için "açıklı → exploit testi → düzeltilmiş" üçlüsü.

#### S7.3 Güvenli Kod Yazma İlkeleri · `03-secure-coding`
- **Input validation — allowlist > denylist**
- **Context-aware output encoding** (HTML, URL, JS, SQL)
- **Defense in depth** — tek kontrole güvenmemek
- **Fail securely** — hata durumunda varsayılan olarak reddet
- **Least privilege** — servis hesabı, DB kullanıcısı, token scope'ları
- **Secure by default** — yeni endpoint kilitli başlar, `[AllowAnonymous]` istisnadır
- Immutable veri modelleme ile kazara state mutasyonunu önleme
- **Security code review checklist'i:** yeni sorgu parametreli mi? yeni dış URL çağrısı → SSRF? yeni dosya işlemi → path traversal? auth/authz değişti mi → test edildi mi? yeni bağımlılık → CVE?
- Risk bazlı derinlik: auth, ödeme, dosya yükleme gibi yüzeylerde derin inceleme
- 🛠 **Kod:** Mid capstone'a uygulanmış güvenlik code review raporu ve düzeltme PR'ları.

#### S7.4 Kriptografi & Data Protection · `04-cryptography`
- Simetrik (AES-GCM) vs asimetrik (RSA, ECDSA); hibrit şifreleme
- Hashing (SHA-256) vs **password hashing** (PBKDF2, bcrypt, Argon2) — fark kritik ⭐
- HMAC ile bütünlük (webhook imzası); `CryptographicOperations.FixedTimeEquals` ile timing attack önleme
- `RandomNumberGenerator` vs `Random`
- **ASP.NET Core Data Protection API** — key ring, çoklu instance'ta anahtar paylaşımı (Blob + Key Vault)
- Sertifika yönetimi, mTLS, anahtar rotasyonu
- Zarf şifreleme (envelope encryption) ve Key Vault/HSM
- **Post-quantum kriptografi:** .NET 10'da ML-KEM / ML-DSA desteği — "harvest now, decrypt later" tehdidi
- "Kendi kriptonu yazma" kuralı
- 🛠 **Kod:** Kolon seviyesinde envelope encryption (Key Vault ile), HMAC'li webhook doğrulama, çoklu instance'ta Data Protection anahtar paylaşımı.

#### S7.5 Secrets & Zero Trust · `05-secrets-zero-trust`
- Secret'sız mimari: **managed identity**, workload identity federation — en iyi secret, olmayan secret'tır
- Key Vault, HashiCorp Vault; Kubernetes secret'larının sınırları (Secrets Store CSI, External Secrets)
- **Secret rotation** stratejisi ve uygulamanın kesintisiz yeni secret'a geçmesi
- Secret scanning (GitHub push protection, gitleaks) ve sızıntı sonrası müdahale
- **Zero trust** ilkeleri: açıkça doğrula, en az yetki, ihlal varsay; private endpoint, ağ segmentasyonu
- 🛠 **Kod:** Kesintisiz secret rotasyonu (iki aktif secret penceresi) yapan servis + gitleaks'li pre-commit hook.

#### S7.6 Supply Chain Güvenliği · `06-supply-chain`
- **NuGet güvenliği:** NuGet audit, typosquatting, **package source mapping**, lock file, imzalı paketler
- **SBOM** (CycloneDX, Microsoft SBOM tool) üretimi ve takibi
- **SLSA** seviyeleri, build provenance, GitHub artifact attestation, container image imzalama (Notation, cosign)
- **CI/CD sertleştirme:** Action'ları SHA ile sabitlemek, en az yetkili `GITHUB_TOKEN`, OIDC ile secret'sız deploy, self-hosted runner riskleri
- SAST (CodeQL), DAST (OWASP ZAP), container tarama (Trivy, Defender)
- 🛠 **Kod:** SBOM üreten, image'ı imzalayan, CodeQL + Trivy çalıştıran ve kritik CVE'de build'i kıran sertleştirilmiş pipeline.

#### S7.7 Threat Modeling & Güvenli SDLC · `07-threat-modeling`
- **STRIDE:** Spoofing, Tampering, Repudiation, Information disclosure, DoS, Elevation of privilege
- Data flow diagram ve güven sınırları; attack tree; Microsoft Threat Modeling Tool
- Güvenlik gereksinimlerini tasarım aşamasında yazmak
- Security champion modeli, pentest, bug bounty
- Güvenlik olay müdahalesi (incident response) temelleri
- 🛠 **Kod:** Mid capstone için STRIDE tehdit modeli dokümanı (DFD + tehdit tablosu + önlemler + kalan risk).

#### S7.8 Ölçekte Yetkilendirme & Multi-tenancy · `08-authorization-at-scale`
- **RBAC vs ABAC vs ReBAC** ⭐ — rol patlaması problemi
- Google Zanzibar modeli; **OpenFGA**, SpiceDB; policy engine'ler: OPA, Cedar
- Merkezi vs dağıtık yetkilendirme kararı; yetki verisinin servislere dağıtımı
- Keycloak Authorization Services vs harici yetkilendirme servisi
- **Tenant izolasyonu** ve tenant sızıntısı testleri; yatay/dikey yetki yükseltme testleri
- 🛠 **Kod:** Doküman paylaşım sistemi (sahip/editör/görüntüleyici + klasör kalıtımı) OpenFGA ile; aynı kuralların RBAC ile modellenmeye çalışıldığında ortaya çıkan rol patlaması.

🎯 **Mülakat:** "OWASP Top 10:2025'te ne değişti?" · "SSRF'i nasıl önlersin?" · "Supply chain saldırısına karşı pipeline'ını nasıl korursun?" ⭐ · "Password hashing ile normal hashing farkı?" · "Secret rotasyonunu kesintisiz nasıl yaparsın?" · "Yeni bir özellik için threat modeling nasıl yaparsın?" · "RBAC ne zaman yetmez?"

---

### S8. Cloud-Native Platform & SRE

> 📁 `03-senior/08-cloud-native-sre/` · ⏱ ~3 hafta · Önkoşul: M14, C1 (Azure temelleri)
> Sertifika notu: Azure çözüm mimarisi için **AZ-305**.

#### S8.1 Kubernetes Derinlemesine & AKS · `01-kubernetes-advanced`
- Ölçekleme: **HPA**, **KEDA** (event-driven), VPA, cluster autoscaler
- Deployment stratejileri: rolling, **blue-green**, **canary** (Argo Rollouts, Flagger)
- Paketleme: **Helm**, Kustomize; **GitOps** (Argo CD, Flux)
- Güvenlik: RBAC, service account, **NetworkPolicy**, Pod Security Standards
- Secret yönetimi: Secrets Store CSI driver, External Secrets
- Ingress → **Gateway API**
- **AKS'e özel:** workload identity, node pool'lar, Azure CNI, AKS Automatic
- 🛠 **Kod:** Capstone için Helm chart + Argo CD ile GitOps + Argo Rollouts canary (metrik tabanlı otomatik rollback) + KEDA ile kuyruk derinliğine göre ölçekleme.

#### S8.2 Infrastructure as Code · `02-iac`
- **Bicep** (Azure-native), **Terraform** (state, module, plan/apply, drift), **Pulumi** (C# ile IaC)
- `azd` şablonları ve Aspire'ın altyapı çıktıları
- **Policy as code:** Azure Policy, OPA/Conftest
- Ortam paritesi (dev/staging/prod aynı tanımdan); pipeline'da plan onayı
- 🛠 **Kod:** Capstone altyapısının (Container Apps/AKS, PostgreSQL, Redis, Key Vault, Service Bus) hem Bicep hem Terraform ile tanımı + PR'da `plan` çıktısı yorumlayan pipeline.

#### S8.3 Azure Çözüm Mimarisi · `03-azure-architecture`
- **Well-Architected Framework** — 5 sütun: reliability, security, cost optimization, operational excellence, performance efficiency ⭐
- Landing zone, abonelik ve ağ topolojisi (hub-spoke)
- Ağ: VNet, private endpoint, NSG, Front Door, Application Gateway + WAF
- **Multi-region ve felaket kurtarma:** RTO/RPO, active-active vs active-passive, veri replikasyonu
- **Maliyet optimizasyonu (FinOps):** reservation, savings plan, autoscale, right-sizing, maliyet etiketleme
- Azure Architecture Center referans mimarileri
- 🛠 **Kod:** Capstone için WAF değerlendirme raporu + multi-region DR tasarımı (diyagram + RTO/RPO hedefleri + failover tatbikatı runbook'u).

#### S8.4 SRE: SLO, On-call & Incident Yönetimi · `04-sre`
- **SLI / SLO / SLA** ⭐ ve **error budget**; error budget politikası (bütçe bittiyse özellik dondurma)
- **Burn rate alert**'leri (çoklu pencere)
- Toil azaltma
- On-call pratikleri: **actionable alert**, runbook, eskalasyon
- Incident yönetimi: incident commander rolü, iletişim, durum sayfası
- **Blameless post-mortem** ve aksiyon takibi
- DORA metrikleri ile güvenilirlik-hız dengesi
- 🛠 **Kod:** Capstone için SLO tanımları (OpenSLO formatı), Grafana burn rate alert'leri, örnek runbook ve chaos ile tetiklenmiş bir olayın post-mortem dokümanı.

#### S8.5 Production Teşhis Metodolojisi & APM · `05-production-diagnosis`
- **Belirti → metrik → hipotez → doğrulama → düzeltme → post-mortem** döngüsü
- Dashboard'dan başlamak; metrikten trace'e, trace'ten log'a inmek
- Canlı sistemde teşhis: `dotnet-monitor`, dump alma, feature flag ile hızlı geri alma
- **APM seçimi:** Azure Application Insights, Grafana LGTM, Datadog, New Relic, Dynatrace, Seq — maliyet, lock-in, ekip büyüklüğü
- Gözlemlenebilirlik maliyet yönetimi: sampling, retention, kritik servislere odaklanma
- 🛠 **Kod:** "Teşhis tatbikatları" — beş farklı production problemi (thread pool starvation, memory leak, yavaş sorgu, bağımlılık timeout'u, hatalı deploy) ve her biri için adım adım teşhis rehberi.

#### S8.6 Platform Engineering · `06-platform-engineering`
- Internal developer platform, "paved road" / golden path
- Self-servis altyapı ve şablonlar (`dotnet new` template'leri, Backstage)
- Aspire'ı ekip platformu olarak kullanmak (ortak service defaults, ortak AppHost bileşenleri)
- Geliştirici deneyimi metrikleri (DORA, SPACE)
- 🛠 **Kod:** Şirket standartlarını (OTel, health check, auth, Dockerfile, pipeline) içeren özel `dotnet new` şablonu + ortak service defaults paketi.

🎯 **Mülakat:** "Canary deployment nasıl kurulur, ne zaman otomatik rollback olur?" · "SLO ile SLA farkı, error budget nedir?" ⭐ · "RTO ve RPO nedir, nasıl belirlenir?" · "Well-Architected Framework'ün sütunları?" · "Production'da gece 3'te alarm çaldı — ilk 15 dakikada ne yaparsın?" · "Bicep mi Terraform mu?"

---

### S9. AI Sistemleri & Agent Mimarisi

> 📁 `03-senior/09-ai-systems/` · ⏱ ~3.5 hafta · Önkoşul: M15
> 2026'nın en hızlı değişen alanı. Senior'dan beklenen: agent'ı **production'a güvenle çıkarmak** — mimari, değerlendirme, güvenlik, maliyet.

#### S9.1 Microsoft Agent Framework · `01-agent-framework`
- **Agent Framework 1.0 (Nisan 2026)** — Semantic Kernel'in kurumsal temelleri + AutoGen'in orkestrasyon fikirleri tek SDK'da; .NET ve Python
- Durum: **Semantic Kernel** destekte ama yeni projeler Agent Framework ile başlamalı; **AutoGen** bakım modunda
- Kavramlar: `IChatClient` üzerine kurulu agent'lar, konuşma durumu (thread/session), tool'lar (`AIFunction`, MCP tool'ları, hosted tool'lar), middleware, context provider'lar ve bellek
- **Workflow'lar:** graph tabanlı (executor + edge), sequential / concurrent / handoff / group chat orkestrasyonları, checkpointing, **human-in-the-loop**
- Yerleşik OpenTelemetry
- Hosting: ASP.NET Core, Aspire, **Foundry Agent Service** (hosted agent'lar)
- Semantic Kernel'den göç: plugin → tool, kernel → agent
- 🛠 **Kod:** Müşteri destek agent'ı — sipariş tool'ları, bilgi tabanı (RAG) tool'u, MCP üzerinden kargo servisi; iade için insan onaylı workflow; küçük bir Semantic Kernel örneğinin Agent Framework'e taşınması.

#### S9.2 Agent Mimari Desenleri · `02-agent-patterns`
- **Workflow mu agent mı?** ⭐ — deterministik akış yeterliyse agent kullanma
- Desenler: prompt chaining, routing, parallelization, orchestrator-workers, evaluator-optimizer, otonom agent
- ReAct döngüsü (reason → act → observe); planner-executor
- **Multi-agent:** supervisor, handoff, group chat — ve tek agent'ın daha iyi olduğu durumlar
- **Bellek:** kısa süreli (konuşma), uzun süreli (kullanıcı tercihleri), episodik/semantik; context rot ve özetleme
- Uzun süren agent'lar: durable state, checkpoint, kaldığı yerden devam
- Başarısızlık modları: döngü, yanlış tool kullanımı, hedef kayması; **adım ve maliyet limitleri**
- 🛠 **Kod:** Aynı görevin (rapor üretimi) tek agent, orchestrator-workers ve evaluator-optimizer desenleriyle çözümü; kalite/maliyet/gecikme karşılaştırması.

#### S9.3 Agent Birlikte Çalışabilirliği: MCP & A2A · `03-mcp-a2a`
- **MCP ölçekte:** uzak (remote) MCP server'ları, OAuth 2.1 tabanlı yetkilendirme, MCP gateway, server registry, versiyonlama
- **A2A (Agent2Agent) protokolü:** Agent Card, task yaşam döngüsü, agent'lar arası delegasyon; Linux Foundation altında
- **MCP vs A2A** ⭐ — MCP: agent ↔ tool/veri; A2A: agent ↔ agent
- Agent skill paketleri kavramı
- Agent'lar arası güven, kimlik ve yetki devri
- 🛠 **Kod:** İki bağımsız agent'ın (satış ve lojistik) A2A ile iş birliği; her birinin kendi MCP tool'larını kullanması; OAuth ile korunan remote MCP server.

#### S9.4 RAG Derinlemesine: Agentic, Hybrid & GraphRAG · `04-advanced-rag`
- Hybrid search + RRF, **cross-encoder reranking**
- Query decomposition, contextual retrieval
- **Agentic RAG** — ne zaman ve neyi getireceğine agent'ın karar vermesi
- **GraphRAG** — knowledge graph ile çok adımlı sorular
- Long-context model vs RAG trade-off'u
- **Doküman seviyesinde yetki filtreleme** ⭐ — kullanıcının görmemesi gereken chunk'ın cevaba sızmaması
- Tazelik: artımlı yeniden indeksleme, embedding modeli değişiminde göç
- 🛠 **Kod:** M15.5'teki RAG'ı ACL filtreli, reranking'li ve agentic hale getirme; her iyileştirmenin eval skorlarına etkisi (→ S9.5).

#### S9.5 AI Değerlendirme & Gözlemlenebilirlik · `05-ai-evaluation-observability`
- **Eval-driven development** ⭐ — "prompt'u değiştirdim, daha mı iyi oldu?" sorusunu ölçmek
- **`Microsoft.Extensions.AI.Evaluation`:** kalite (relevance, coherence, groundedness...), güvenlik değerlendiricileri, raporlama, yanıt cache'i
- Golden dataset oluşturma; **LLM-as-judge** ve sınırları
- RAG metrikleri: context precision/recall, faithfulness, answer relevance
- Offline eval vs online (A/B) değerlendirme; kullanıcı geri bildirimi
- **OpenTelemetry GenAI semantic conventions** — model, token, tool çağrısı span'leri
- Agent tracing: her adım, her tool çağrısı, maliyet
- **CI'da eval regresyon testleri**
- 🛠 **Kod:** Destek agent'ı için golden dataset + eval suite'i; CI'da kalite eşiğinin altına düşen prompt değişikliğini reddeden pipeline; Aspire/Grafana'da token-maliyet-kalite dashboard'u.

#### S9.6 AI Güvenliği & Guardrail'ler · `06-ai-security`
- **OWASP Top 10 for LLM Applications (2025):** prompt injection (doğrudan/dolaylı), hassas bilgi ifşası, tedarik zinciri, veri/model zehirleme, **güvensiz çıktı işleme**, **aşırı yetki (excessive agency)**, system prompt sızıntısı, vector/embedding zafiyetleri, yanlış bilgi, sınırsız tüketim
- **Dolaylı prompt injection** ⭐ — agent'ın okuduğu doküman/web sayfası/tool çıktısındaki gizli talimatlar
- Savunmalar: input/output filtreleri, **Azure AI Content Safety + Prompt Shields**, en az yetkili tool'lar, yan etkili işlemde insan onayı, kod çalıştırmada sandbox
- **LLM çıktısı güvenilmeyen girdidir** — HTML/SQL/komut olarak kullanmadan önce encode/doğrula
- PII redaksiyonu, veri yerleşimi, sağlayıcının veri saklama politikası
- Responsible AI: şeffaflık, bias, insan gözetimi
- 🛠 **Kod:** Destek agent'ına karşı prompt injection test seti (red teaming) + guardrail katmanı; savunma öncesi/sonrası başarı oranı.

#### S9.7 LLMOps: Maliyet, Gecikme & Üretim · `07-llmops`
- **Model yönlendirme (routing):** basit işe küçük model, karmaşık işe büyük model
- Caching: exact cache, **semantic cache**, provider prompt caching
- Streaming UX, batching
- Kota ve rate limit (TPM/RPM); sağlayıcı/bölge arası **fallback**; APIM AI gateway
- Kullanıcı/tenant başına **token bütçesi** ve maliyet atfı
- Prompt ve model versiyonlama; prompt değişikliklerinin canary ile yayını; model emekliliğine hazırlık
- **Foundry Agent Service** ile hosted agent'lar (oturum başına izole sandbox)
- 🛠 **Kod:** Model router + semantic cache + tenant bazlı token bütçesi uygulayan AI gateway servisi; maliyet ve gecikme düşüşünün raporu.

#### S9.8 Klasik ML & Yerel Modeller · `08-ml-net-local-models`
- Klasik ML ne zaman LLM'den doğru araç: tabular veri, düşük gecikme, düşük maliyet, açıklanabilirlik ⭐
- **ML.NET:** regression, classification, anomaly detection, recommendation; AutoML / Model Builder; `PredictionEnginePool`
- **ONNX Runtime** ile PyTorch/TensorFlow modellerini .NET'te çalıştırma
- Yerel küçük modeller (Phi, Llama, Qwen) — Ollama, **Foundry Local**; gizlilik ve maliyet avantajı
- 🛠 **Kod:** Sipariş dolandırıcılık skoru (ML.NET) ve aynı problemin LLM ile çözümünün maliyet/gecikme/doğruluk karşılaştırması; ONNX ile yerel embedding modeli.

#### S9.9 Kurumsal Ölçekte AI-Driven Engineering · `09-ai-driven-engineering-org`
- Takım genelinde **paylaşılan yapılandırma:** ortak `CLAUDE.md`/`AGENTS.md`, skill'ler, MCP server'ları, izin politikaları
- Yönetişim: hangi araç, hangi veri, hangi repo; audit trail
- AI'ın ürettiği kod için code review politikası; güvenlik açısından kritik alanlarda zorunlu insan incelemesi
- CI'da agent'lar: otomatik PR review, issue triage, test üretimi — **insan incelemesinin önüne, yerine değil**
- Etkiyi ölçmek: DORA, hata oranı, review süresi — "daha çok kod" değil "daha iyi sonuç"
- Güvenlik: repo içi prompt injection, secret erişimi, sandbox, komut allowlist'i
- Ekibi eğitmek ve "vibe coding"den mühendisliğe taşımak
- 🛠 **Kod:** Örnek kurumsal yapılandırma paketi: ortak talimat dosyası, izin politikası, PR'da otomatik AI review yapan GitHub Action ve kullanım/etki metrikleri.

🎯 **Mülakat:** "Ne zaman agent, ne zaman deterministik workflow?" ⭐ · "Semantic Kernel ile Agent Framework ilişkisi?" · "MCP ile A2A farkı?" · "RAG'da kullanıcının yetkisiz dokümanı görmesini nasıl engellersin?" · "Bir LLM özelliğinin kalitesini nasıl ölçersin?" · "Dolaylı prompt injection nedir, nasıl savunursun?" · "LLM maliyetini nasıl düşürürsün?"

---

### S10. Sistem Tasarımı

> 📁 `03-senior/10-system-design/` · ⏱ ~3 hafta · Önkoşul: S4, S6
> Her case study şu yapıda: gereksinim → kapasite tahmini → API → veri modeli → üst düzey tasarım → derinleşme → darboğaz ve hata senaryoları → trade-off özeti. Mülakat formatı → I3.

#### S10.1 Ölçeklenebilirlik Temelleri & Kapasite Tahmini · `01-scalability`
- Vertical vs horizontal scaling; **stateless servis** — yatay ölçeğin ön koşulu
- Sticky session ve sorunları
- Queue ile load leveling; asenkron işleme
- **Back-of-the-envelope:** QPS, depolama, bant genişliği, instance sayısı; "her programcının bilmesi gereken gecikme değerleri"
- Little's Law ile kapasite
- 🛠 **Kod:** Kapasite tahmin hesap tablosu/şablonu + birkaç örnek sistem için doldurulmuş hali.

#### S10.2 Yapı Taşları · `02-building-blocks`
- Load balancer (L4 vs L7), health check tabanlı yönlendirme
- CDN, API gateway, reverse proxy
- Cache, queue, stream, object storage (presigned URL), arama motoru
- Veritabanı türleri ve seçim (→ S4.8)
- **Benzersiz ID üretimi:** auto-increment, UUIDv4, **UUIDv7**, ULID, Snowflake
- Bildirim altyapıları (push, e-posta, SMS)
- 🛠 **Kod:** Snowflake tarzı dağıtık ID üreteci + UUIDv7 ile karşılaştırması (sıralılık, çakışma, index etkisi).

#### S10.3 Ölçekte Caching · `03-caching-at-scale`
- Çok katmanlı cache mimarisi (tarayıcı → CDN → gateway → uygulama → DB)
- Cache hit ratio ölçümü ve iyileştirme
- Tag tabanlı invalidation; **hot key** problemi; consistent hashing
- Cache'in kendisinin darboğaz veya tek hata noktası olması
- 🛠 **Kod:** Hot key'i yerel L1 cache + key replikasyonuyla dağıtan deney.

#### S10.4 Rate Limiter Tasarımı · `04-rate-limiter-design`
- Algoritmalar: fixed window, sliding window log/counter, **token bucket**, leaky bucket ⭐
- Dağıtık rate limiting: Redis + Lua ile atomik sayaç
- Kullanıcı/tenant/IP/endpoint bazlı partition; kota ve fair usage
- `429` + `Retry-After`, istemci tarafı backoff
- Doğruluk vs performans trade-off'u
- 🛠 **Kod:** Redis tabanlı dağıtık rate limiter servisi (dört algoritma) + yük testi altında doğruluk ölçümü.

#### S10.5 Case Study'ler · `05-case-studies`
Her biri ayrı alt klasörde tasarım dokümanı + kritik kısmın prototipi:
- **URL shortener** — ID üretimi, çakışma, okuma ağırlıklı yük, cache
- **Bilet/koltuk rezervasyonu** — concurrency, optimistic locking, geçici tutma süresi
- **Bildirim sistemi** — fan-out, çoklu kanal, retry, idempotency, kullanıcı tercihleri
- **Haber akışı / timeline** — fan-out on write vs read, ünlü kullanıcı problemi
- **Chat servisi** — SignalR, presence, mesaj sırası, offline teslim
- **E-ticaret checkout** — saga, envanter rezervasyonu, ödeme, telafi
- **Dosya yükleme servisi** — chunked upload, presigned URL, virüs taraması
- **Arama / otomatik tamamlama** — trie, arama motoru, sıralama
- **Rate limiter servisi** — (→ S10.4)
- **Ödeme sistemi / cüzdan** — çift kayıtlı defter, idempotency, mutabakat
- **AI destekli doküman Q&A** — ingestion pipeline, ACL'li retrieval, maliyet, eval
- **AI müşteri destek agent'ı** — tool'lar, insan devri, guardrail, gözlemlenebilirlik
- 🛠 **Kod:** Her case study için `design.md` (diyagramlar dahil) + en riskli bileşenin çalışan prototipi.

🎯 **Mülakat:** I3'teki formatla herhangi bir case study'yi 45 dakikada uçtan uca tasarlayıp her kararı gerekçelendirebilmek.

---

### Senior Capstone: Dağıtık Sipariş Platformu + AI Destek Agent'ı

> 📁 `03-senior/capstone/` · ⏱ ~5 hafta
> Mid capstone'u **ölçek, güvenilirlik ve yönetişim** boyutunda olgunlaştırmak. Mülakatta "en karmaşık projen" sorusunun cevabı.

**Gereksinimler:**
- **Mimari:** Modular monolith'ten strangler fig ile ödeme servisinin ayrılması; DDD aggregate'leri; ödeme defteri **event-sourced** (Marten)
- **Tutarlılık:** Sipariş → ödeme → stok → kargo için outbox + inbox + orchestration saga ve telafi adımları
- **Event streaming:** Kafka + schema registry; Debezium CDC ile arama index'i senkronizasyonu
- **Veri ölçeği:** read replica yönlendirmesi, sipariş tablosu partitioning, UUIDv7 anahtarlar
- **Resilience:** circuit breaker, bulkhead, hedging; chaos testleri
- **Platform:** AKS (veya Container Apps) + Bicep/Terraform IaC + GitOps + canary deployment
- **SRE:** SLO'lar, burn rate alert'leri, runbook'lar, chaos ile tetiklenmiş bir olayın blameless post-mortem'i
- **Performans:** k6 yük testi raporu (p99 hedefleri), en az bir darboğazın profiling ile bulunup giderildiği rapor
- **Güvenlik:** STRIDE tehdit modeli, SBOM + imzalı image, OWASP 2025 review'u, OpenFGA ile ince taneli yetki
- **AI:** Agent Framework ile destek agent'ı — MCP tool'ları, ACL filtreli RAG, insan onaylı iade workflow'u, CI'da eval regresyon testleri, guardrail'ler, token maliyet dashboard'u
- **Dokümantasyon:** ADR seti, C4 diyagramları, kapasite tahmini

**Çıkış kriteri:** Capstone tamam + I6'daki Senior mock mülakatı (system design + teknik liderlik) kapalı kitap geçildi + D9–D10'da Medium/Hard problemlerde rahatlık.

---

## DSA Pisti — Veri Yapıları & Algoritmalar

> 📁 `04-dsa/` · **Seviyelerle paralel ilerler** — 1. haftadan itibaren haftada 2-3 gün.
> Mülakatta fark yaratan şey algoritma ezberlemek değil, **problemdeki sinyali doğru pattern'e eşlemek** ve çözümü sesli, test edilebilir şekilde yazmaktır.
> DSA tek bir projedir (`04-dsa/`; her modül bir klasör): veri yapısını **sıfırdan** implementasyon + çalıştırılabilir doğrulama (konsol çıktısı) + .NET'in yerleşik karşılığıyla BenchmarkDotNet karşılaştırması + çözülmüş problemler.

**Seviyeye göre DSA hedefi:**

| Seviye | Modüller | Hedef |
|--------|----------|-------|
| 🟢 Junior | D1, D2, D3 (ağaç/heap temeli), D5, D11 | LeetCode Easy'yi 20-25 dakikada, Big-O'su ile çözmek |
| 🟡 Mid | D3, D4, D6, D7, D8, D9 (temel) | Medium'u 30-35 dakikada; doğru pattern'i ilk 5 dakikada seçmek |
| 🔴 Senior | D9 (ileri), D10 | Medium/Hard; veri yapısı seçimini sistem tasarımında gerekçelendirmek |

---

### D1. Karmaşıklık Analizi

> 📁 `04-dsa/01-complexity/` · 🟢

- **Big-O:** time ve space complexity; worst / average / best case
- Büyüme sıralaması: `O(1) < O(log n) < O(n) < O(n log n) < O(n²) < O(2ⁿ) < O(n!)`
- **Amortized complexity** — `List<T>.Add` neden ortalama O(1)
- Recursion'da karmaşıklık: recurrence relation, Master theorem'e kısa bakış (`T(n) = 2T(n/2) + O(n)` → `O(n log n)`)
- Space complexity'de **call stack**'in de sayıldığı (recursion derinliği = O(n) alan)
- Girdi boyutundan beklenen karmaşıklığı tahmin etmek (n ≤ 20 → üstel olabilir; n ≤ 10⁵ → O(n log n); n ≤ 10⁸ → O(n))
- Nested loop'larda karmaşıklığı sesli analiz etme pratiği

**.NET koleksiyonları hızlı referansı:**

| Yapı | Erişim | Arama | Ekleme (son) | Ekleme (baş/orta) | Silme |
|------|:------:|:-----:|:-------------:|:------------------:|:-----:|
| `Array` | O(1) | O(n) | — | — | — |
| `List<T>` | O(1) | O(n) | O(1)* | O(n) | O(n) |
| `LinkedList<T>` | O(n) | O(n) | O(1) | O(1)** | O(1)** |
| `Dictionary<K,V>` | — | O(1)*** | O(1)*** | — | O(1)*** |
| `HashSet<T>` | — | O(1)*** | O(1)*** | — | O(1)*** |
| `SortedDictionary<K,V>` | — | O(log n) | O(log n) | — | O(log n) |
| `SortedList<K,V>` | O(1) (index ile) | O(log n) | O(n) | O(n) | O(n) |
| `Queue<T>` / `Stack<T>` | — | O(n) | O(1) | — | O(1) |
| `PriorityQueue<TElement,TPriority>` | O(1) peek | — | O(log n) | — | O(log n) |

<sub>* amortized · ** node referansı elindeyse · *** amortized; hash collision'da worst-case O(n)</sub>

- 🛠 **Kod:** Aynı problemin O(n²), O(n log n) ve O(n) çözümleri + girdi büyüdükçe süre grafiği üreten benchmark.

---

### D2. Doğrusal Veri Yapıları ve Hash Table

> 📁 `04-dsa/02-linear-structures/` · 🟢
> Her yapı için: **nasıl çalışır → .NET karşılığı → ne zaman kullanılır → gerçek dünyada nerede kullanılır.**

- **Array** — bitişik bellek, cache-friendly; `Span<T>` ile allocation'sız dilimleme; 2D array / matris dolaşma
- **Dynamic array (`List<T>`)** — kapasite dolunca ~2x büyüme ve kopyalama; `new List<T>(n)` neden kazandırır
- **String** — immutability, `StringBuilder`, `char[]`/`Span<char>` ile yerinde işlem
- **Linked list** — singly vs doubly, sentinel node; cache-unfriendly olması; gerçek dünya: **LRU cache'in kalbi**
- **Stack (LIFO)** — call stack, undo/redo, parantez doğrulama, iteratif DFS, monotonic stack
- **Queue (FIFO)** — BFS'in temeli, iş kuyrukları (uygulama seviyesi karşılığı → M12), `Channel<T>`
- **Deque** — sliding window maksimum/minimum için monotonic deque
- **Hash table (`Dictionary`/`HashSet`)** — hash fonksiyonu, collision çözümü (chaining vs open addressing), **load factor** ve resize; `GetHashCode`/`Equals` sözleşmesi; gerçek dünya: cache, deduplication, frekans sayımı, memoization tablosu
- 🛠 **Kod:** Sıfırdan `DynamicArray<T>`, `DoublyLinkedList<T>`, `MyStack<T>`, `MyQueue<T>` (circular buffer) ve chaining'li `MyHashMap<K,V>` + testler + yerleşik tiplerle benchmark; 15 klasik problem (two sum, valid parentheses, reverse linked list, merge two lists, group anagrams...).

---

### D3. Ağaçlar, Heap & Trie

> 📁 `04-dsa/03-trees-heaps-tries/` · 🟢→🟡

- **Binary tree** — inorder/preorder/postorder (recursive ve iteratif), level-order (BFS); yükseklik, çap, LCA
- **BST** — invariant, arama/ekleme/silme; dengesizlik ve O(n) zincire dönüşme
- **Dengeli ağaçlar (AVL, Red-Black)** — neden dengeleme; `SortedDictionary`/`SortedSet` red-black tree tabanlıdır → garanti O(log n)
- **Heap / `PriorityQueue<TElement,TPriority>`** — min/max heap, dizi temsili (`2i+1`, `2i+2`), sift-up/sift-down, heapify O(n); gerçek dünya: görev önceliklendirme, Dijkstra, event scheduling, top-K, median tracking
- **Trie (prefix tree)** — .NET'te yerleşik yok; gerçek dünya: autocomplete, spell-checker, IP routing (longest prefix match)
- 🛠 **Kod:** Sıfırdan BST, min-heap ve trie + testler; 20 problem (max depth, validate BST, level order, kth smallest, top-K frequent, implement trie, word search II...).

---

### D4. Graflar

> 📁 `04-dsa/04-graphs/` · 🟡

- Temsil: komşuluk listesi (`Dictionary<T, List<T>>`) vs komşuluk matrisi — seyrek/yoğun graf; grid'i graf olarak görmek
- Yönlü/yönsüz, ağırlıklı/ağırlıksız, DAG
- **BFS** (ağırlıksız en kısa yol, seviye seviye) ve **DFS** (bağlantılılık, tüm yollar)
- Bağlı bileşenler, **döngü tespiti** (yönlü ve yönsüz), bipartite kontrolü
- **Topological sort:** Kahn (BFS) ve DFS tabanlı
- En kısa yol: **Dijkstra** (min-heap ile), Bellman-Ford (negatif ağırlık), Floyd-Warshall (kavramsal)
- **Union-Find (Disjoint Set)** — path compression + union by rank
- **Minimum spanning tree:** Kruskal (union-find ile), Prim
- Gerçek dünya: sosyal ağlar, harita/rota servisleri, **bağımlılık grafları (NuGet, build sistemleri, DI container)**, öneri motorları, ağ topolojisi
- 🛠 **Kod:** Generic `Graph<T>` + BFS/DFS/topological sort/Dijkstra/union-find/Kruskal implementasyonları; 15 problem (number of islands, course schedule, clone graph, network delay time, redundant connection...); bonus: bir solution'daki proje referanslarından build sırasını çıkaran araç.

---

### D5. Sıralama & Arama

> 📁 `04-dsa/05-sorting-searching/` · 🟢→🟡

- O(n²) algoritmalar: bubble, selection, insertion — neden yavaş, insertion sort neden küçük dizide hâlâ kullanılır
- **Merge sort** (stabil, O(n log n), ek alan), **quick sort** (pivot seçimi, worst case O(n²)), heap sort
- Karşılaştırmasız sıralama: counting, radix, bucket
- **Stabilite** ⭐ — `Array.Sort` (introsort, **stabil değil**) vs LINQ `OrderBy` (**stabil**)
- `IComparer<T>`, `Comparison<T>`, `Comparer<T>.Create`, çok kriterli sıralama
- **Binary search** ve varyantları: lower/upper bound, döndürülmüş dizide arama, **cevap uzayında binary search** ("minimum kapasite", "en küçük maksimum")
- **Quickselect** — K'inci eleman ortalama O(n)
- Harici sıralama (bellekten büyük veri) — kavramsal
- 🛠 **Kod:** Tüm algoritmaların implementasyonu + stabilite testleri + `Array.Sort` ile benchmark; 15 problem (search in rotated array, find first and last position, koko eating bananas, kth largest, merge intervals...).

---

### D6. Recursion & Backtracking

> 📁 `04-dsa/06-recursion-backtracking/` · 🟡

- Recursion zihinsel modeli: base case, ilerleme, call stack
- **.NET'te stack overflow riski** ⭐ — varsayılan ~1 MB stack, C#'ta tail call garantisi yok; derin recursion'ı explicit `Stack<T>` ile iterasyona çevirmek
- Divide & conquer
- **Backtracking şablonu:** seç → keşfet → geri al
- Subsets, permutations, combinations, combination sum
- N-Queens, Sudoku çözücü, word search
- Pruning ile arama uzayını küçültmek
- Memoization'a giriş (→ D9)
- 🛠 **Kod:** Ortak backtracking şablonu + 12 problem; derin recursion'ın `StackOverflowException` ile çöktüğü ve iteratif versiyonla çözüldüğü demo.

---

### D7. Pattern Kataloğu

> 📁 `04-dsa/07-pattern-catalog/` · 🟡
> Aşağıdaki 18 pattern, coding interview sorularının büyük çoğunluğunu kapsar. Her pattern ayrı alt klasör: şablon kod + 3-5 problem.

| # | Pattern | Sinyal | Karmaşıklık | Örnek problem |
|---|---------|--------|:-----------:|----------------|
| 1 | **Two Pointers** | Sıralı array/string, çift bulma | O(n) | Two Sum II, palindrome kontrolü |
| 2 | **Sliding Window** | Alt-dizi/alt-string, "en uzun/en kısa/koşullu" | O(n) | En uzun tekrarsız substring |
| 3 | **Fast & Slow Pointers** | Linked list, döngü | O(n) | Cycle tespiti (Floyd), ortanca düğüm |
| 4 | **Merge Intervals** | Aralıklar, çakışma | O(n log n) | Toplantı odaları, aralık birleştirme |
| 5 | **Cyclic Sort** | `1..n` aralığında sayılar | O(n) | Eksik/yinelenen sayı |
| 6 | **In-place Linked List Reversal** | O(1) alanda ters çevirme | O(n) | Reverse list, K'li grup ters çevirme |
| 7 | **BFS** | Ağırlıksız en kısa yol, seviye | O(V+E) | Level order, word ladder |
| 8 | **DFS** | Tüm yollar, bağlantılılık | O(V+E) | Ada sayısı, path sum |
| 9 | **Two Heaps** | Akan veride medyan | O(log n)/işlem | Find median from data stream |
| 10 | **Subsets / Backtracking** | Tüm kombinasyon/permütasyon | O(2ⁿ)/O(n!) | Subsets, N-Queens |
| 11 | **Modified Binary Search** | Sıralı/kısmen sıralı veri | O(log n) | Rotated array search |
| 12 | **Top K Elements** | "En büyük/küçük/sık K" | O(n log k) | Top K frequent |
| 13 | **K-way Merge** | Çok sayıda sıralı liste | O(n log k) | Merge K sorted lists |
| 14 | **Topological Sort** | Bağımlılık sırası | O(V+E) | Course schedule |
| 15 | **Union-Find** | Dinamik bağlantılılık | ~O(1)/işlem | Redundant connection |
| 16 | **Monotonic Stack/Queue** | "Bir sonraki büyük/küçük" | O(n) | Daily temperatures, histogram |
| 17 | **Prefix Sum / Difference Array** | Çoklu aralık toplamı sorgusu | O(n) + O(1) | Subarray sum equals K |
| 18 | **Dynamic Programming** | Optimal alt yapı + çakışan alt problem | Değişken | → D9 |

**Yardımcı pattern'ler:** bit manipülasyonu (XOR ile tekil eleman, bitmask ile subset, `n & (n-1)`), greedy (kanıtlanabilir yerel en iyi seçim), divide & conquer.

- 🛠 **Kod:** 18 alt klasör — her birinde yeniden kullanılabilir şablon + çözülmüş problemler + testler.

---

### D8. Problemden Pattern'e Eşleştirme

> 📁 `04-dsa/08-pattern-matching-guide/` · 🟡
> Mülakatın ilk 60 saniyesinde doğru pattern'i seçmek için **soru metnindeki anahtar kelimeleri** tara.

| Problemde görürsen... | Muhtemel pattern |
|------------------------|-------------------|
| "sıralı array/list" | Binary search, two pointers |
| "alt-dizi / alt-string / pencere" + "en uzun/en kısa/maksimum toplam" | Sliding window |
| "toplamı X olan çift" | Two pointers (sıralıysa) veya hash map (sırasızsa) |
| "linked list", "döngü var mı" | Fast & slow pointers |
| "aralıklar", "çakışan", "birleştir" | Merge intervals |
| "`1..n` arasında", "eksik/yinelenen" | Cyclic sort veya XOR |
| "tüm kombinasyonlar/permütasyonlar/alt kümeler" | Backtracking |
| "en kısa yol" + ağırlıksız | BFS |
| "en kısa yol" + ağırlıklı | Dijkstra |
| "tüm yollar", "bağlı mı", "ada sayısı" | DFS |
| "K en büyük/en küçük/en sık" | Heap (top-K) |
| "medyan", "akan veri" | Two heaps |
| "bağımlılık sırası", "önce X sonra Y" | Topological sort |
| "gruplar", "bağlantılı mı" | Union-Find |
| "bir sonraki daha büyük/küçük" | Monotonic stack |
| "alt-dizi toplamı = K" | Prefix sum + hash map |
| "kaç farklı yol", "minimum/maksimum maliyet" | Dynamic programming |
| "en az sayıda adım" + yerel seçim kanıtlanabilir | Greedy |
| "in-place", "O(1) ekstra alan" | Two pointers, cyclic sort, bit manipülasyonu |
| "K sıralı liste" | K-way merge |
| "autocomplete", "önek" | Trie |
| "LRU/LFU cache tasarla" | Hash map + doubly linked list (→ D10) |
| "minimum kapasite/hız ile ... yapılabilir mi" | Cevap uzayında binary search |

**Mülakat stratejisi — UMPIRE:**
1. **U**nderstand — soruyu kendi cümlelerinle tekrar et, edge case sor (boş girdi? negatif? tekrar eden eleman?)
2. **M**atch — tabloyla pattern eşleştir
3. **P**lan — pseudocode ile yaklaşımı anlat, **kodlamadan önce onay al**
4. **I**mplement — sesli düşünerek kodla
5. **R**eview — örnek girdiyle elle çalıştır (dry run)
6. **E**valuate — zaman/alan karmaşıklığını söyle, iyileştirme tartış

- 🛠 **Kod:** Etiketsiz 30 problemlik "pattern tanıma" alıştırma seti — önce sadece pattern tahmini, sonra çözüm.

---

### D9. Dinamik Programlama

> 📁 `04-dsa/09-dynamic-programming/` · 🟡→🔴

- **Ne zaman DP:** optimal alt yapı + çakışan alt problemler — ikisi de yoksa DP gerekmez
- **Top-down (memoization)** vs **bottom-up (tabulation)**
- **5 adımlı çözüm şablonu:**
  1. State'i tanımla (alt problemi hangi parametreler belirler?)
  2. Recurrence relation'ı yaz
  3. Base case'leri belirle
  4. Hesaplama sırasına karar ver
  5. Alan optimizasyonu (2D → 1D, rolling array)
- **Klasik kalıplar:**
  - 1D DP — climbing stairs, house robber, decode ways
  - **0/1 Knapsack** — subset sum, partition equal subset
  - **Unbounded Knapsack** — coin change, rod cutting
  - **LCS ailesi** — edit distance, longest common substring
  - **LIS** — O(n²) DP'den O(n log n) binary search'e
  - **Grid DP** — unique paths, minimum path sum
  - **Interval DP** — palindrome partitioning, burst balloons
  - **State machine DP** — hisse alım-satım problemleri
  - Bitmask DP (🔴) — TSP benzeri küçük n problemleri
- DP vs greedy: greedy'nin **kanıtlanması gerekir**, DP her zaman güvenli ama daha maliyetli
- 🛠 **Kod:** Her kalıp için top-down ve bottom-up iki çözüm + alan optimize versiyon; 25 problem.

---

### D10. Gelişmiş Veri Yapıları & Sistem Tasarımında DSA

> 📁 `04-dsa/10-advanced-structures/` · 🔴
> Senior mülakatlarında DSA, sistem tasarımıyla birleşir: "Redis sorted set neden skip list kullanır?", "Consistent hashing nasıl çalışır?"

| Yapı | Nasıl çalışır (özet) | Gerçek sistemde nerede |
|------|----------------------|------------------------|
| **LRU / LFU cache** | Hash map + doubly linked list (LFU: frekans listeleri) | `IMemoryCache`, Redis eviction, CDN |
| **Bloom filter** | Bit dizisi + k hash; false positive var, false negative yok | Cassandra/RocksDB okuma ön elemesi, CDN, zararlı URL listeleri |
| **Skip list** | Olasılıksal çok katmanlı bağlı liste, O(log n) | **Redis sorted set** |
| **Consistent hashing** | Hash ring + virtual node'lar | Dağıtık cache, sharding, load balancing (→ S4.3) |
| **HyperLogLog** | Olasılıksal kardinalite tahmini, sabit bellek | Redis `PFCOUNT`, tekil ziyaretçi |
| **B-tree / B+tree** | Disk sayfasına uygun geniş dallı dengeli ağaç | İlişkisel DB index'leri (→ S4.1) |
| **LSM-tree** | Memtable + sıralı immutable dosyalar + compaction | RocksDB, Cassandra (→ S4.1) |
| **Segment tree / Fenwick tree** | Aralık sorgusu ve güncelleme O(log n) | Analitik, oyun, zaman serisi sorguları |
| **Interval tree** | Çakışan aralıkları hızlı bulma | Takvim, rezervasyon |
| **Geohash / Quadtree** | 2D uzayı hiyerarşik bölme | "Yakınımdaki" sorguları, harita servisleri |
| **Merkle tree** | Hash ağacı ile farkı hızlı bulma | Git, replica senkronizasyonu (Cassandra), blockchain |
| **Ring buffer** | Sabit boyutlu döngüsel dizi | Log tamponu, `Channel` iç yapısı, ses/video akışı |
| **Token bucket / sliding window** | Zaman tabanlı sayaçlar | Rate limiter (→ S10.4) |

- **Klasik tasarım soruları:** LRU cache (O(1) get/put), LFU cache, "time-based key-value store", "design hit counter", "design Twitter" (veri yapısı seviyesi)
- 🛠 **Kod:** Tablodaki her yapının sıfırdan implementasyonu + testleri; Bloom filter'ın false positive oranı ve consistent hashing'in node ekleme/çıkarmada taşınan anahtar oranı deneyleri.

---

### D11. C# ile Pratik & Problem Listeleri

> 📁 `04-dsa/11-csharp-practice/` · 🟢→🔴

- **C#'a özel tuzaklar ve ipuçları:**
  - `PriorityQueue<TElement,TPriority>` **min-heap**'tir — max-heap için `Comparer<int>.Create((a, b) => b.CompareTo(a))`
  - `Dictionary` için `TryGetValue`, `CollectionsMarshal.GetValueRefOrAddDefault` ile tek lookup'ta sayaç artırma
  - Tuple key'ler (`(int, int)`), `record` ile hızlı değer tipi modelleme (node, interval, pair)
  - `Span<T>` / `stackalloc` ile allocation'sız çözümler (senior ayırt edici)
  - `Array.Fill`, `Array.Sort` ile `Comparison<T>`, `string.Create`
  - Recursion derinliği ve stack overflow (→ D6)
- **Çözüm repo düzeni:** `problems/<pattern>/<problem>.cs` + her çözüm için xUnit `[Theory]` testleri (boş girdi, tek eleman, tüm elemanlar eşit, çok büyük girdi)
- **Problem listeleri:**
  - 🟢 Junior: NeetCode 150'nin Easy kısmı + Blind 75'in Easy'leri (~50 problem)
  - 🟡 Mid: NeetCode 150'nin Medium'ları (~100 problem)
  - 🔴 Senior: kalan Medium/Hard + D10 tasarım soruları (~50 problem)
- **Ritim:** haftada 5-10 problem; her pattern bitince 3 problemi süre tutarak yeniden çöz; ayda bir 45 dakikalık mock coding interview (→ I1)
- Platformlar: LeetCode (C# desteği), NeetCode, HackerRank, Codewars, Exercism C# track, Advent of Code
- 🛠 **Kod:** Tüm çözümlerin pattern'e göre klasörlendiği, CI'da testleri çalışan çözüm deposu + ilerleme takip tablosu.

---

## Mülakat Hazırlığı

> 📁 `05-interview/` · Her seviyenin sonunda ve başvurudan önceki 2-3 haftada yoğun çalışılır.

### I1. Coding Interview & Live Coding

> 📁 `05-interview/01-coding-live-coding/`

- C# ile LeetCode refleksi (→ D11); UMPIRE (→ D8)
- **Klasik .NET live coding görevleri:** FizzBuzz, string ters çevirme, palindrome, anagram, en sık geçen eleman, basit LRU cache, thread-safe sayaç, `HttpClient` ile paralel istek + toplama, küçük bir REST endpoint + test, var olan kodu refactor etme
- LINQ ile mi döngü ile mi — hangisini ne zaman göstermeli
- **Sesli düşünme** — sessiz kalmak en büyük hata
- Varsayımları açıkça söylemek ve soru sormak
- Küçük adımlarla, çalışan koddan başlayarak ilerlemek; "önce çalışsın, sonra iyileştirelim"
- Edge case listeleme alışkanlığı (null, boş, tek eleman, çok büyük, negatif, unicode)
- IDE hakimiyeti; hata yapınca panik yerine sistematik debug
- 🛠 **Kod:** 20 klasik live coding görevi — her biri için 45 dakikalık zamanlı çözüm, testler ve "görüşmeciye söylenecekler" notu.

### I2. Behavioral / STAR

> 📁 `05-interview/02-behavioral/`

- **STAR:** Situation → Task → Action → Result (mümkünse sayısal sonuç)
- Hazırlanması gereken hikayeler:
  - Zor bir production incident ve çözümü
  - Takım içi teknik anlaşmazlık
  - Deadline kaçırma / kapsam pazarlığı
  - Başarısız bir proje ve çıkarılan ders
  - Bir teknik kararı savunma veya fikir değiştirme
  - Mentorluk ve bilgi paylaşımı
  - Belirsiz bir gereksinimle başa çıkma
- "Neden ayrılmak istiyorsun?", "Neden burası?" — dürüst ve olumlu çerçeve
- Görüşmeciye sorulacak sorular: ekip yapısı, code review kültürü, teknik borç, on-call, AI araç politikası
- 🛠 **Kod:** (README) Kişisel STAR hikaye bankası şablonu — her hikaye için kısa ve uzun versiyon.

### I3. System Design Interview

> 📁 `05-interview/03-system-design-interview/`

- **Yapı (45-60 dk):**
  1. Gereksinim netleştirme — fonksiyonel + non-fonksiyonel (5-10 dk)
  2. Ölçek tahmini — kullanıcı, QPS, veri, okuma/yazma oranı
  3. API sözleşmesi
  4. Veri modeli ve depolama seçimi
  5. Üst düzey tasarım (kutu-ok diyagramı)
  6. Derinleşme — görüşmecinin seçtiği bileşen
  7. Darboğaz, hata modları, ölçekleme
  8. Trade-off özeti
- Gereksinimi netleştirmeden çizmeye başlamamak
- Her seçimi **gerekçelendirmek** ("PostgreSQL seçtim çünkü...")
- Bilmediğini kabul edip muhakemeyi göstermek
- Seviyeye göre beklenti: Mid → doğru yapı taşları ve temel ölçek; Senior → trade-off derinliği, hata senaryoları, operasyon ve maliyet
- 🛠 **Kod:** (README) S10.5 case study'leri için 45 dakikalık tatbikat senaryoları + değerlendirme rubriği.

### I4. Take-home Ödevleri

> 📁 `05-interview/04-take-home/`

- Zaman yönetimi ve **over-engineering tuzağı**
- README'nin önemi: kurulum, mimari kararlar, varsayımlar, **yapılmayanlar ve nedenleri**
- Test yazmak — en çok fark yaratan tek şey
- `docker compose up` ile tek komutta çalışma
- Temiz commit geçmişi
- Kapsamı erken netleştirmek için soru sormak
- AI araçlarını kullandıysan bunu ve nasıl doğruladığını açıkça belirtmek
- 🛠 **Kod:** Örnek bir take-home ödevi (4-6 saatlik kapsam) + referans çözüm + değerlendirme checklist'i.

### I5. Senior: Teknik Liderlik Soruları

> 📁 `05-interview/05-technical-leadership/`
> Senior ve üstü mülakatlarda teknik derinlik kadar ağırlıklı.

- Teknik karar süreci yürütmek: RFC/ADR, alternatifleri değerlendirmek, ekibi ikna etmek (→ S5.5)
- **Teknik borcu** önceliklendirmek ve iş tarafına anlatmak
- Code review kültürü kurmak; review'da yapıcı geri bildirim
- Mentorluk: junior'ı büyütmek, delegasyon
- Tahminleme ve belirsizlik yönetimi
- Ürün/iş tarafıyla anlaşmazlık; "hayır" demek ve alternatif sunmak
- Incident liderliği ve post-mortem kültürü (→ S8.4)
- Ekipler arası koordinasyon; yetkisi olmadan etki yaratmak
- İşe alım süreçlerine katılım
- Ekipte AI araçlarının benimsenmesine liderlik etmek (→ S9.9)
- 🛠 **Kod:** (README) Her konu için 2-3 örnek soru + STAR formatında cevap iskeleti.

### I6. Mock Mülakat Senaryoları & Checklist

> 📁 `05-interview/06-mock-checklist/`

**Seviyeye göre mock mülakat senaryosu:**

| Seviye | Süre | İçerik |
|--------|------|--------|
| 🟢 Junior | ~60 dk | 15 dk C# temelleri (J2–J4) · 15 dk SQL & veritabanı (J8) + ASP.NET Core (J6–J7) · 20 dk Easy coding · 10 dk behavioral |
| 🟡 Mid | ~75 dk | 15 dk async/concurrency (M4) · 15 dk API tasarımı + kimlik (M5, M9) · 15 dk veritabanı/isolation/cache (M6, M8) · 20 dk Medium coding · 10 dk behavioral |
| 🔴 Senior | 2 × ~60 dk | Oturum 1: system design (S10). Oturum 2: teknik derinlik (runtime/performans/dağıtık sistemler — S1, S2, S6) + teknik liderlik (I5) |

**Mülakat öncesi checklist:**
- [ ] CV'deki her teknolojiyi savunabiliyor muyum?
- [ ] Son projemi 2 dakikada anlatabiliyor muyum (problem → çözüm → sonuç)?
- [ ] En gurur duyduğum ve en çok pişman olduğum teknik karar?
- [ ] Şirketin ürününü ve teknoloji yığınını araştırdım mı?
- [ ] Capstone projelerim GitHub'da, README'leri güncel mi?
- [ ] AI araçlarını nasıl kullandığımı anlatabiliyor muyum (→ M15.8)?
- [ ] Benim sorularım hazır mı?
- [ ] Ortam testi (kamera, mikrofon, IDE, ekran paylaşımı)?

- 🛠 **Kod:** (README) Her seviye için soru havuzu ve değerlendirme rubriği — bir arkadaşla veya AI ile mock mülakat yapmak için.

---

## Sertifika Pisti — Azure Sertifikaları

> 📁 `06-certifications/` · **Seviyelerden bağımsız** bir pisttir: Junior/Mid/Senior sırasının parçası değildir, ilgili modülleri bitirdikten sonra (veya paralel) çalışılır.
> Amaç: **sertifika sınavına hazırlık** — resmî sınav alanlarını (skills measured) konu konu, **teori + laboratuvar (pratik) + sınav tarzı quiz** ile derinlemesine anlatmak.
> İki sertifika iki ayrı modüldür: **C1 = AZ-900** (giriş, genişlik sınavı) ve **C2 = AI-200** (geliştirici, associate). Çözüm mimarisi sertifikası AZ-305 → S8.3'te kapsanır.

**Bu pistin farkları (diğer pistlerden):**

| Konu klasöründe | Açıklama |
|-----------------|----------|
| `README.md` | Teori — 8 başlıklı şablon; 5. başlık **"Sınav perspektifi"** (sınav neyi, nasıl sorar; tuzaklar; anahtar kelimeler) |
| Kod / `az` CLI laboratuvarı | **Pratik**: Azure CLI komutları README'de kod bloğu olarak; Azure SDK/emülatör (Azurite, Cosmos emulator) ile çalışan .NET kodu `ConsoleApp` içinde |
| `quiz.md` | Sınav tarzı sorular (tek/çoklu seçim, evet-hayır tablosu, sıralama, senaryo; AI-200'de ayrıca kod tamamlama ve vaka) — cevap ve **gerekçe** `<details>` içinde |
| `15-practice-exam/` · `14-practice-exam/` | Her sertifikanın sonunda **tam uzunlukta deneme sınavı**, alan bazlı puan tablosu ve sınav stratejisi |

- **Sorular özgündür:** Gerçek sınav soruları Microsoft'un gizlilik sözleşmesi (NDA) kapsamındadır; "çıkmış soru" veya braindump kullanılmaz. Sorular **sınav formatında** ve resmî **konu ağırlıklarına** göre yazılır.
- **Müfredat tarihlidir:** study guide'lar "skills measured as of …" ile tarihlenir; sınava girmeden önce resmî sayfayı kontrol et.
- **Maliyet:** Laboratuvarlar ücretsiz katman/emülatörle ve **temizleme (cleanup) adımıyla** tasarlanır; her laboratuvar bütçe alarmı ve `az group delete` ile biter.
- Proje yapısı: ayrı proje yok; kod `ConsoleApp/06-certifications/…` altında (SLA/TCO hesaplayıcıları, Azure SDK demoları gerektiğinde, emülatörler kökteki `docker-compose.yml`'den); testler yok, quiz'ler `.md`'dir.

---

### C1. Microsoft Azure Fundamentals (AZ-900)

> 📁 `06-certifications/01-az-900-azure-fundamentals/` · ⏱ ~6 hafta · Önkoşul: — (J12 sonrasında çalışmak en verimli yoldur; Docker/CLI aşinalığı laboratuvarları kolaylaştırır)
> 📜 **Sertifika:** Microsoft Azure Fundamentals (**AZ-900**) — giriş seviyesi, **genişlik** sınavı. Müfredat: resmî study guide, **"skills measured as of July 20, 2026"**. Değişiklik kaydına göre bu sürümde compute & networking, kaynak yönetimi/dağıtımı ve izleme araçları bölümlerinde **küçük** düzenlemeler var; alan ağırlıkları değişmedi.
>
> | Sınav alanı | Ağırlık | Konular |
> |---|---|---|
> | Describe cloud concepts | %25-30 | C1.1 – C1.3 |
> | Describe Azure architecture and services | %35-40 | C1.4 – C1.10 |
> | Describe Azure management and governance | %30-35 | C1.11 – C1.14 |
> | *Genişletilmiş kapsam (study guide dışı, kurs müfredatları — bağlam)* | — | C1.16 – C1.22 |
>
> Her konuda dört katman var: **📌 Resmî hedef** (study guide'daki hangi maddeyi kapsadığı) · **Teori** (README) · **🛠 Pratik** (az CLI laboratuvarı ve/veya `ConsoleApp` içindeki kod; portal demoları yok, gereken bilgi teoride) · **📝 Quiz** (sınav formatında, gerekçeli). *(bağlam)* etiketli maddeler sınav hedefinde açıkça yazmaz ama soruyu anlamak ve gerçek işte kullanmak için gereklidir.
> Sınav bilgileri (süre, geçme puanı 700/1000, ücret, dil) zaman zaman değişir — sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı ve değişiklik kaydını kontrol et. Geliştirici derinliği → C2 (AI-200); çözüm mimarisi → S8.3 (AZ-305).
> **Quiz notu:** Gerçek sınav soruları Microsoft'un gizlilik sözleşmesi kapsamındadır; "çıkmış soru"/braindump kullanılmaz. Sorular sınav formatında (tek seçim, çoklu seçim, evet/hayır tablosu, sıralama, senaryo), resmî alan ağırlıklarına göre **özgün** yazılır.

**Kapsam kontrol listesi** — AZ-900 hazırlık kurslarındaki standart bölüm başlıklarının hepsi bu modülde (resmî hedefler C1.1–C1.14; kurs müfredatlarında geçen ek konular C1.16–C1.22):

| Bölüm | Başlıklar | Konu |
|---|---|---|
| Cloud kavramları | What is cloud computing · Shared responsibility model · Public / private / hybrid cloud · Consumption-based model · Cloud pricing | C1.1 |
| | Benefits: high availability, scalability, elasticity, reliability, predictability, security, governance, manageability | C1.2 |
| | Cloud service types: IaaS, PaaS, SaaS · Serverless | C1.3 |
| | Core architectural components: regions, region pairs, sovereign regions · availability zones, datacenters · resources & resource groups · subscriptions · management groups | C1.4 |
| Compute & ağ | Azure compute services & Azure Virtual Machines · VM Scale Sets · Availability Sets & Proximity Placement Groups | C1.5 |
| | App Service, containers, Azure Virtual Desktop · Azure Functions | C1.6 |
| | Azure networking services · VNet peering · Azure DNS · VPN Gateway (+ ExpressRoute) · Public & private endpoints | C1.7 |
| Storage | Azure Storage · Blob Storage · AzCopy · Azure Files & Azure File Sync · Azure Migrate · Azure Data Box | C1.8 |
| Kimlik, erişim, güvenlik | Identity & Microsoft Entra ID · Benefits of Entra ID · Authentication vs authorization · External identities (B2B/B2C) · SSO · Conditional Access · MFA/2FA · Passwordless · RBAC | C1.9 |
| | Zero Trust · Defense in depth · Microsoft Defender for Cloud | C1.10 |
| Maliyet | Factors that affect cost · Pricing Calculator · Cost Management · Resource tags | C1.11 |
| Yönetişim | Governance & compliance · Azure Policy · Resource locks · Microsoft Purview | C1.12 |
| Araçlar | Azure portal · Cloud Shell (CLI/PowerShell ile kaynak oluşturma) · Azure Arc · IaC · ARM templates (portalda şablon üretme dahil) | C1.13 |
| İzleme | Azure Advisor · Azure Service Health · Azure Monitor merkezi dashboard'u · Azure Monitor raporları (Workbooks) | C1.14 |
| Sınav | Deneme sınavı, puanlama, strateji | C1.15 |
| Genişletilmiş kapsam *(bağlam)* | Azure SQL/Cosmos DB/Fabric/Synapse/Databricks/Data Factory | C1.16 |
| | Load Balancer · Application Gateway · Front Door · Traffic Manager | C1.17 |
| | NSG/ASG · Azure Firewall · DDoS Protection · Bastion · Key Vault | C1.18 |
| | Azure Backup · Site Recovery · Azure Migrate · Database Migration Service | C1.19 |
| | Azure AI services · Azure Machine Learning · IoT Hub/Central | C1.20 |
| | Logic Apps · Event Grid · Service Bus · Event Hubs · API Management · Azure DevOps/GitHub | C1.21 |
| | Destek planları · SLA · preview/GA yaşam döngüsü · Cloud Adoption Framework · Well-Architected | C1.22 |

#### C1.1 Cloud Computing, Cloud Modelleri ve Fiyatlandırma · `01-cloud-computing` ⭐
- 📌 **Resmî hedef:** *Describe cloud computing* — cloud computing'i tanımla · shared responsibility model · cloud modelleri (public, private, hybrid) · her modelin kullanım senaryosu · consumption-based model · cloud fiyatlandırma modellerini karşılaştır · serverless *(serverless → C1.3'te hizmet türleriyle birlikte)*
- **Teori — What is cloud computing:**
  - Tanım: bilgi işlem hizmetlerinin (sunucu, depolama, ağ, veritabanı, yazılım, analitik, AI) **internet üzerinden, ihtiyaç anında ve kullanım kadar ücretle** sunulması
  - NIST'in beş niteliği: **on-demand self-service**, **broad network access**, **resource pooling**, **rapid elasticity**, **measured service**
  - Geleneksel datacenter ile farkı: donanım satın alma/kurma süresi, kapasite tahmini, bakım, fiziksel güvenlik
- **Teori — Shared responsibility model** ⭐ (sınavın en sık sorduğu tablo):

  | Sorumluluk | On-prem | IaaS | PaaS | SaaS |
  |---|---|---|---|---|
  | Bilgi ve veri · cihazlar (mobil, PC) · hesaplar ve kimlikler | Müşteri | **Müşteri** | **Müşteri** | **Müşteri** |
  | Kimlik ve directory altyapısı | Müşteri | Müşteri | Paylaşımlı | Paylaşımlı |
  | Uygulamalar | Müşteri | Müşteri | Paylaşımlı | Microsoft |
  | Ağ kontrolleri | Müşteri | Müşteri | Paylaşımlı | Microsoft |
  | İşletim sistemi | Müşteri | Müşteri | Microsoft | Microsoft |
  | Fiziksel host · fiziksel ağ · fiziksel datacenter | Müşteri | **Microsoft** | **Microsoft** | **Microsoft** |

  - Kural: **veri, cihazlar, hesaplar/kimlikler her zaman müşteride**; **fiziksel her şey bulutta her zaman Microsoft'ta**; arada kalanlar modele göre kayar
- **Teori — Public, private, hybrid cloud:**
  - **Public cloud:** sağlayıcının altyapısı, birçok müşteri paylaşır; CapEx yok, hızlı başlangıç, sonsuza yakın ölçek; daha az fiziksel kontrol
  - **Private cloud:** tek kuruluşa ait (kendi datacenter'ı veya ayrılmış barındırma); tam kontrol ve uyumluluk; donanım/bakım maliyeti ve sorumluluğu kuruluşta
  - **Hybrid cloud:** public + private birlikte; eski sistemler ve hassas veri yerinde, esnek iş yükü bulutta; **cloud bursting**; regülasyon/veri egemenliği senaryoları
  - *(bağlam)* **Multicloud:** birden çok public cloud sağlayıcısı; yönetimi Azure Arc ile (→ C1.13)
  - Senaryo eşleştirme: "regülasyon nedeniyle veri kurum içinde kalmalı ama web katmanı ölçeklenmeli" → hybrid
- **Teori — Consumption-based model:**
  - Kullandığın kadar öde; ön yatırım (donanım) yok; kapasite fazlası/eksiği riski yok; maliyet tüketimle orantılı
  - **CapEx vs OpEx** ⭐: CapEx = baştan büyük sabit yatırım, amortisman (kendi datacenter'ın); OpEx = işletme gideri, dönemsel ve değişken (bulut aboneliği); bulut harcamayı **CapEx'ten OpEx'e** kaydırır
- **Teori — Cloud pricing:**
  - **Pay-as-you-go** (taahhütsüz, saniye/dakika/saat veya işlem bazında), **reserved instances / savings plan** (1-3 yıl taahhütle indirim), **spot** (boşta kapasite, kesilebilir, çok ucuz), **free tier / ücretsiz hesap kredisi**, **dev/test fiyatlandırması**
  - Ne zaman hangisi: sabit 7/24 yük → reservation; değişken yük → pay-as-you-go; kesintiye dayanıklı toplu iş → spot (ayrıntılı maliyet → C1.11)
- **Sınav tuzakları:** "MFA/şifre politikası kimin sorumluluğunda?" → **her zaman müşteri**; "işletim sistemi yaması" → IaaS'ta müşteri, PaaS/SaaS'ta Microsoft; hybrid ile multicloud; "baştan yatırım gerektirmeyen model" → OpEx/consumption
- 🛠 **Pratik:** Aynı iş yükü (web uygulaması + veritabanı) için on-prem / IaaS / PaaS / SaaS **sorumluluk matrisini** doldurma alıştırması (README'de boş tablo + `<details>` çözüm); `az login` → `az account show` → ilk resource group'u açıp silme; **bütçe alarmı** kurma (Cost Management → Budgets, `az consumption budget create`) — öğrenirken fatura şoku yaşamamak için ilk iş. `ConsoleApp` içinde (C1 klasörü): sorumluluk matrisi sorgu aracı (`dotnet run -- 1.1 kim os paas`), CapEx/OpEx ve fiyatlandırma modeli karşılaştırma hesapları (örnek fiyatlarla).
- 📝 **Quiz:** sorumluluk matrisi, cloud modeli senaryoları, CapEx/OpEx, fiyatlandırma modeli seçimi (≈18 soru)

#### C1.2 Cloud'un Faydaları · `02-cloud-benefits` ⭐
- 📌 **Resmî hedef:** *Describe the benefits of using cloud services* — high availability ve scalability · reliability ve predictability · security ve governance · manageability
- **Teori — her fayda: tanım, nasıl sağlanır, Azure örneği:**

  | Fayda | Tanım | Bulutta nasıl sağlanır | Azure örneği |
  |---|---|---|---|
  | **High availability** | Kesinti olsa bile hizmetin **hedeflenen çalışma süresinde** kalması | Yedeklilik, tek arıza noktasını kaldırmak, otomatik failover | **SLA**'lar (%99,9 / %99,95 / %99,99), availability zone, scale set |
  | **Scalability** | Talebe göre **kapasiteyi artırma/azaltma** yeteneği | **Vertical** (scale up/down: daha güçlü/zayıf makine) · **horizontal** (scale out/in: makine sayısı) | VM boyutu değiştirme, VM Scale Sets, App Service plan ölçeği |
  | **Elasticity** | Ölçeklemenin yüke göre **otomatik** yapılması | Metrik/zaman tabanlı **autoscale** kuralları | Autoscale, Functions'ın otomatik ölçeklemesi |
  | **Reliability** | Arızadan **toparlanma** ve çalışmaya devam etme | Dağıtık tasarım, bölge/zone yedekliliği, felaket kurtarma | Region pair, geo-redundant storage, Azure Site Recovery *(bağlam)* |
  | **Predictability** | **Performans** ve **maliyetin** öngörülebilir olması | Performans: autoscale, load balancing, HA · Maliyet: kullanım takibi ve tahmin | Pricing Calculator, Cost Management, Well-Architected Framework (→ S8.3) |
  | **Security** | Güvenlik kontrolleri üzerinde **istenen düzeyde kontrol** | IaaS'ta tam kontrol, PaaS/SaaS'ta yamalar sağlayıcıda; DDoS koruması gibi altyapı güvenliği | Defender for Cloud, Key Vault, DDoS Protection |
  | **Governance** | Kurumsal standartlara ve regülasyonlara **uyum** | Şablonlar, politikalar, denetim ve uyumluluk raporları | Azure Policy, management group'lar, Purview |
  | **Manageability** | Bulutun **yönetimi** (management *of* the cloud) ve bulutta **yönetim** (management *in* the cloud) | *Of:* otomatik ölçek, izleme ve uyarı, şablonla dağıtım · *In:* portal, CLI, API, PowerShell | Azure Monitor, ARM/Bicep, Azure portal, Cloud Shell |

  - **High availability vs reliability vs disaster recovery:** HA = çalışmayı **sürdürmek** (kesinti süresini küçültmek); reliability/DR = arızadan veya felaketten **toparlanmak**
  - **SLA** ⭐: yüzdeyi kesinti süresine çevirme — %99,9 ≈ ayda ~43 dk, %99,95 ≈ ~22 dk, %99,99 ≈ ~4,3 dk; preview servislerin SLA'sı yoktur; ücretsiz katmanlarda genellikle SLA yoktur
  - **Composite SLA** *(bağlam — hesap sorusu gelebilir)*: seri bağlı bileşenlerde SLA'lar **çarpılır** (%99,95 × %99,99 ≈ %99,94); yedekli yapıda `1 − (1−a)(1−b)`; SLA'yı yükseltmek için zone/bölge yedekliliği ve maliyeti
  - **Scalability vs elasticity** ⭐: scalability **yetenek**, elasticity bu yeteneğin **otomatik** kullanımı
- **Sınav tuzakları:** fayda ↔ tanım eşleştirme (özellikle HA ile reliability, scalability ile elasticity, security ile governance); scale up mı scale out mu; "kesintide hizmet sürsün" → HA; "talep artınca otomatik büyüsün" → elasticity
- 🛠 **Kod:** `ConsoleApp` içinde (C1 klasörü) **konsol hesaplayıcı**: SLA → günlük/aylık/yıllık izin verilen kesinti tablosu; seri ve paralel **composite SLA** hesabı; tek VM vs iki zone'a yayılmış VM karşılaştırması.
- 📝 **Quiz:** fayda eşleştirme (her faydadan en az 2 soru), scale up/out/elasticity senaryoları, SLA hesabı (≈20 soru)

#### C1.3 Cloud Hizmet Türleri: IaaS, PaaS, SaaS ve Serverless · `03-cloud-service-types` ⭐
- 📌 **Resmî hedef:** *Describe cloud service types* — IaaS · PaaS · SaaS · her hizmet türünün kullanım senaryosu · (*Describe cloud computing*'den) serverless
- **Teori:**

  | | IaaS | PaaS | SaaS |
  |---|---|---|---|
  | Ne kiralarsın | Altyapı: sanal makine, ağ, depolama | Platform: çalışma ortamı, veritabanı, ara katman | Hazır uygulama |
  | Kontrol / sorumluluk | **En fazla** | Orta | **En az** |
  | Kullanım senaryosu | **Lift-and-shift** taşıma, test/geliştirme ortamı, özel yazılım, depolama/yedekleme, yüksek performanslı hesap | Web/API geliştirme, iş zekâsı/analitik, geliştirme çerçeveleri, IoT backend | E-posta ve iletişim, ofis uygulamaları, CRM/ERP, iş birliği |
  | Azure örnekleri | Virtual Machines, Virtual Network, Managed Disks | App Service, Azure SQL Database, Azure Functions, Cosmos DB | Microsoft 365, Dynamics 365, Outlook |

  - Pizza benzetmesi ("evde yap → hazır al → yemeğe çık") ve sorumluluk matrisiyle (→ C1.1) ilişkisi
- **Teori — Serverless:**
  - Tanım: sunucular hâlâ vardır ama **sağlanmaları, ölçeklenmeleri ve yönetilmeleri** tamamen sağlayıcıdadır; geliştirici yalnızca kod/iş mantığı yazar
  - Nitelikler: **olay tetiklemeli** çalışma, **otomatik ölçekleme** (sıfıra kadar), **çalışma süresi/çağrı başına ücret**, altyapı yönetimi yok
  - Serverless, PaaS'ın bir alt türü olarak düşünülür; Azure örnekleri: **Azure Functions** (kod), **Azure Logic Apps** (görsel iş akışı), serverless container (Container Apps) (→ C1.6)
- **Sınav tuzakları:** "işletim sistemine tam erişim" → IaaS; "en az yönetim yükü" → SaaS (uygulama hazırsa) / PaaS (kendi kodunu çalıştıracaksan); ürün → model eşleştirme (Azure SQL Database = PaaS, VM üzerinde SQL Server = IaaS, Microsoft 365 = SaaS); "yalnızca kod çalışırken ödeme" → serverless
- 🛠 **Pratik:** Aynı Todo uygulaması için IaaS (VM), PaaS (App Service) ve SaaS seçeneklerini **Pricing Calculator**'da fiyatlandırıp kontrol/maliyet/sorumluluk karşılaştırma tablosu çıkarma (README'de şablon); `az` CLI ile F1 App Service ve küçük bir VM açıp farkı görme, ardından `az group delete`. `ConsoleApp` içinde (C1 klasörü): ürün → hizmet türü kataloğu (`dotnet run -- 1.3 tur sql`) ve serverless (çağrı + GB-saniye) ile 7/24 açık kaynak maliyet karşılaştırması.
- 📝 **Quiz:** ürün → hizmet türü eşleştirme, kullanım senaryosu seçimi, serverless nitelikleri (≈15 soru)

#### C1.4 Azure Temel Mimari Bileşenleri · `04-core-architecture` ⭐
- 📌 **Resmî hedef:** *Describe the core architectural components of Azure* — regions, region pairs, sovereign regions · availability zones · Azure datacenters · resources ve resource groups · subscriptions · management groups · resource group / subscription / management group hiyerarşisi
- **Teori — Fiziksel altyapı:**
  - **Datacenter:** sunucu rafları, bağımsız güç, soğutma ve ağ; müşteriye doğrudan açılmaz, region ve zone olarak gruplanır
  - **Region:** coğrafi bölgede, düşük gecikmeli ağla bağlı bir veya daha çok datacenter; servis/özellik uygunluğu ve fiyat bölgeye göre değişir; bazı servisler **global**'dir (Entra ID, Front Door, Azure DNS)
  - **Availability zone** ⭐: bir region içinde **fiziksel olarak ayrı** datacenter grupları (bağımsız güç, soğutma, ağ); zone destekli bölgelerde **en az üç**; datacenter arızasına karşı koruma; **zonal** (kaynak tek zone'a sabitlenir: VM, disk, IP) vs **zone-redundant** (platform otomatik çoğaltır: ZRS storage, SQL Database) servisler
  - **Region pair** ⭐: aynı coğrafyada, mümkün olduğunca ≥300 mil uzakta iki bölge; bölgesel felakette **öncelikli kurtarma**, planlı güncellemelerin **sıralı** yapılması, bazı servislerde (GRS gibi) ikincil bölgeye replikasyon, veri ikametgâhının coğrafya içinde kalması; bazı eşleşmeler tek yönlüdür, yeni bölgelerde eşleşme olmayabilir
  - **Sovereign region:** ana Azure'dan yasal/işletimsel olarak ayrılmış örnekler — **Azure Government** (ABD kamu kurumları), **Azure China** (21Vianet tarafından işletilen)
  - Bölge seçim kriterleri: kullanıcıya **gecikme**, **veri ikametgâhı/regülasyon**, servis uygunluğu, fiyat, zone ve pair durumu
- **Teori — Mantıksal yapı:**
  - **Resource:** oluşturulan her öğe (VM, VNet, storage account, veritabanı…)
  - **Resource group** ⭐: kaynakların **yaşam döngüsüne göre** mantıksal kabı; kurallar: bir kaynak **yalnızca bir** gruptadır · gruplar **iç içe olmaz** · kaynaklar gruplar arasında **taşınabilir** · gruptaki kaynaklar **farklı bölgelerde** olabilir · **grup silinince içindeki her şey silinir** · RBAC/Policy/lock grup düzeyinde uygulanıp içerideki kaynaklara miras kalır
  - **Subscription:** yönetim, **faturalama** ve ölçek sınırı; **billing boundary** (ayrı fatura/maliyet raporu) ve **access control boundary** (ayrı erişim politikası); birden çok subscription nedenleri: ortam (dev/test/prod), departman/organizasyon, faturalama ayrımı, **kota/limit** aşımı; subscription türleri (free, pay-as-you-go, Enterprise Agreement, CSP…)
  - **Management group** ⭐: subscription'ları gruplayan kapsam; Policy ve RBAC'ı **birçok subscription'a birden** uygulama; **6 seviyeye kadar** derinlik (root hariç), tek bir root management group, bir subscription/management group tek bir ebeveyne bağlıdır; 10.000 management group'a kadar
  - **Hiyerarşi:** Entra **tenant** → root management group → management group'lar → **subscription** → **resource group** → **resource**; ayarlar yukarıdan aşağı **miras** alınır
- **Sınav tuzakları:** availability zone ile region pair ("datacenter arızası" → zone; "bölgesel felaket" → pair); "politikayı tüm subscription'lara uygula" → management group; resource group silinince ne olur; bir kaynak iki grupta olabilir mi; global servisler
- 🛠 **Pratik:** `az account list-locations -o table` ve `az account list-locations --query "[?metadata.regionType=='Physical']"` (eşleşik bölge sütunu `metadata.pairedRegion` ile) ile bölge/pair keşfi; `az group create/list/delete` ile resource group yaşam döngüsü — bir grupta iki kaynak açıp **grubu silince ikisinin de gittiğini** gözlemleme; kendi tenant → management group → subscription → RG şemanı çizme.
- 📝 **Quiz:** zone/pair/sovereign senaryoları, resource group kuralları, subscription ve management group kapsamı, hiyerarşi ve miras (≈20 soru)

#### C1.5 Azure Compute ve Virtual Machines · `05-compute-virtual-machines` ⭐
- 📌 **Resmî hedef:** *Describe Azure compute and networking services* — compute türleri (VM, container, functions) · VM seçenekleri: **Azure VMs, VM Scale Sets, availability sets**, (Azure Virtual Desktop → C1.6) · **VM için gereken kaynaklar**
- **Teori — Azure compute services (genel bakış):** isteğe bağlı işlem gücü; **VM** (IaaS) ↔ **container** ↔ **App Service** (PaaS) ↔ **Functions** (serverless) seçim yelpazesi
- **Teori — Azure Virtual Machines:**
  - Ne zaman: işletim sistemi üzerinde **tam kontrol**, özel yazılım/yapılandırma, **lift-and-shift**, test/geliştirme, uygulama barındırma, datacenter'ı buluta genişletme
  - **VM için gereken kaynaklar** ⭐: **boyut** (SKU: CPU/RAM ailesi — genel amaçlı, compute/memory optimized, GPU), **image** (Windows/Linux, Marketplace), **disk** (OS diski + veri diskleri; managed disk; Standard HDD/SSD, Premium SSD), **ağ**: **VNet + subnet**, **NIC**, (isteğe bağlı) **public IP**, **NSG**
  - Fiyatlandırma: çalışma süresi kadar; **durdurulup deallocate** edilen VM compute ücreti almaz (disk ücreti sürer)
- **Teori — VM Scale Sets (VMSS):**
  - Aynı yapılandırmadaki VM'lerden oluşan, **yük dengeleyiciyle** birlikte çalışan grup; talebe veya zamana göre **otomatik** sayı artırma/azaltma (scale out/in); yüzlerce VM'i tek kaynak gibi yönetme; zone'lara yayılabilir
  - Kullanım: değişken trafikli web katmanı, büyük hesaplama işleri
- **Teori — Availability sets:**
  - Tek datacenter içinde VM'leri **update domain** (planlı bakımda aynı anda yeniden başlatılan grup; varsayılan 5, en çok 20) ve **fault domain** (ortak güç/ağ anahtarını paylaşan donanım grubu; en çok 3) arasına dağıtma
  - Ek ücret yok (yalnızca VM'ler); **availability zone ile farkı:** set = datacenter içi donanım/bakım arızası, zone = datacenter arızası; Microsoft yeni HA tasarımlarında availability zone'ları / Flexible orkestrasyonlu scale set'leri öneriyor
- **Teori — Proximity placement groups** *(bağlam — kurslarda işlenir)*:
  - VM'leri **fiziksel olarak birbirine yakın** (aynı datacenter içinde) yerleştirerek **en düşük ağ gecikmesini** sağlamak; düşük gecikme gerektiren çok katmanlı/HPC iş yükleri
  - Trade-off: yakınlık ↔ dayanıklılık (aynı yere toplamak zone yayılımının tersidir); availability set ve scale set ile birlikte kullanılabilir
- **Sınav tuzakları:** availability set ↔ availability zone ↔ scale set eşleştirme; "VM ücretini durdur" → deallocate; "VM için hangisi gereksiz?" kaynak listesi soruları; scale set'in otomatik ölçek sağlaması
- 🛠 **Pratik:** `az vm create` ile Linux VM (NSG, public IP ve disk kaynaklarının **birlikte oluştuğunu** `az resource list -g …` ile görme); VM'i `deallocate` edip ücretlendirme durumunu gözlemleme; iki örnekli **VM Scale Set** ve basit autoscale kuralı; availability set oluşturup fault/update domain değerlerine bakma; `az group delete` ile temizlik.
- 📝 **Quiz:** VM kaynakları, VMSS/availability set/zone/proximity group seçimi senaryoları (≈18 soru)

#### C1.6 Uygulama Barındırma: App Service, Container'lar, Azure Virtual Desktop, Functions · `06-app-hosting`
- 📌 **Resmî hedef:** *Describe Azure compute and networking services* — compute türleri (container'lar, functions) · VM seçenekleri içinde **Azure Virtual Desktop** · **uygulama barındırma seçenekleri** (web apps, container'lar, VM'ler)
- **Teori — Azure App Service** (PaaS):
  - Web uygulaması, **REST API**, mobil backend, **WebJobs** (arka plan işi); .NET, Java, Node.js, Python, PHP; Windows ve Linux
  - Platform özellikleri: otomatik yama, **autoscale**, deployment **slot**'ları, CI/CD entegrasyonu, özel domain ve TLS, kimlik doğrulama entegrasyonu
  - **App Service plan:** altta yatan compute (ücretlendirilen birim); birden çok uygulama aynı planı paylaşabilir
- **Teori — Container'lar:**
  - Container vs VM: container **işletim sistemini sanallaştırır** (hafif, saniyeler içinde başlar), VM **donanımı** sanallaştırır (tam OS)
  - **Azure Container Instances (ACI):** tek container'ı en hızlı/basit çalıştırma, saniye bazında ücret; orkestrasyon yok
  - **Azure Container Apps:** serverless container platformu; otomatik ölçek (sıfıra kadar), HTTP ingress, mikroservis
  - **Azure Kubernetes Service (AKS):** yönetilen Kubernetes; çok sayıda container'ın orkestrasyonu, en fazla kontrol (geliştirici derinliği → C2.1–C2.3)
- **Teori — Azure Virtual Desktop (AVD):**
  - Bulutta **masaüstü ve uygulama sanallaştırma**; herhangi bir cihazdan Windows masaüstüne erişim; **Windows 11/10 çok oturumlu** (multi-session) desteği; Entra ID ile kimlik ve MFA
  - Fayda: veri cihazda değil bulutta (güvenlik), kullanıcı ve masaüstü yönetiminin merkezileşmesi
- **Teori — Azure Functions:**
  - **Olay tetiklemeli**, kısa süreli kod parçaları; HTTP isteği, zamanlayıcı, kuyruk mesajı, blob yüklemesi gibi tetikleyiciler
  - **Stateless** (varsayılan) ve **stateful** (Durable Functions); otomatik ölçek; tüketim planında yalnızca çalıştığı süre kadar ücret
  - Functions vs App Service vs Logic Apps: kod mu, sürekli çalışan uygulama mı, görsel iş akışı mı
- **Barındırma karar ağacı:** tam OS kontrolü → VM · hazır web/API platformu → App Service · container ve mikroservis → ACI / Container Apps / AKS · olay tetiklemeli kısa kod → Functions · masaüstü sanallaştırma → AVD
- **Sınav tuzakları:** "işletim sistemi yönetmeden web uygulaması" → App Service; "tek container'ı hızlıca, orkestrasyonsuz" → ACI; "binlerce container orkestrasyonu" → AKS; "zamanlayıcıyla 5 dakikalık iş" → Functions; "uzaktan Windows masaüstü" → AVD; container vs VM
- 🛠 **Pratik:** Todo API'yi **App Service'e deploy** etme (`az webapp up` / ZIP deploy); connection string'i **Application Settings**'ten verip `IConfiguration`'a environment variable olarak akışını gözlemleme; **log stream** ile canlı log; örnek bir image'ı **Container Instances**'ta tek komutla (`az container create`) çalıştırma (kendi image'ın için registry/ACR → C2.1); HTTP tetiklemeli tek bir Azure Function (yerelde Core Tools ile); sonunda `az group delete`.
- 📝 **Quiz:** barındırma seçimi senaryoları, container servisleri, AVD ve Functions nitelikleri (≈18 soru)

#### C1.7 Ağ Servisleri · `07-networking` ⭐ *(derinlemesine ağ → J1.2, S8.3)*
- 📌 **Resmî hedef:** *Describe Azure compute and networking services* — virtual networking: **VNet, subnet, peering, Azure DNS, VPN Gateway, ExpressRoute** · **public ve private endpoint**'ler
- **Teori — Azure networking services / Virtual Network:**
  - **VNet**: Azure kaynaklarının **izole** ağı; adres alanı (CIDR, ör. `10.0.0.0/16`); bölgeye bağlıdır
  - VNet'in sağladıkları: **izolasyon ve segmentasyon** (subnet'ler), **internet iletişimi** (public IP / load balancer), **Azure kaynakları arası iletişim**, **on-prem ile iletişim** (VPN/ExpressRoute), **trafik yönlendirme** (route table), **trafik filtreleme** (NSG, NVA)
  - **Subnet:** VNet'in alt ağı; kaynakları gruplama ve güvenlik kuralı uygulama
- **Teori — VNet peering:**
  - İki VNet'i **Microsoft omurgası üzerinden** (internete çıkmadan) bağlama; aynı bölge (regional) veya farklı bölgeler (**global peering**); düşük gecikme; geçişli (transitive) değildir; adres alanları çakışmamalı
- **Teori — Azure DNS:**
  - Azure altyapısında **DNS domain barındırma**; tanıdık araçlarla (portal, CLI) kayıt yönetimi, RBAC ve kayıt kilidi; Azure'un global ad sunucu ağıyla yüksek erişilebilirlik; **Azure DNS ile domain satın alınmaz** (yalnızca barındırılır)
  - **Private DNS zone:** VNet içinde özel isim çözümleme
- **Teori — VPN Gateway:**
  - On-prem ile Azure arasında **public internet üzerinden şifreli** (IPsec/IKE) tünel; **site-to-site** (ofis/datacenter), **point-to-site** (tek istemci), **VNet-to-VNet**
  - **Policy-based** vs **route-based** VPN; yüksek erişilebilirlik: **active/standby** (varsayılan), **active/active**, zone-redundant gateway, ExpressRoute için yedek
- **Teori — ExpressRoute** (VPN ile karşılaştırma için zorunlu):
  - **Özel**, **internete çıkmayan** bağlantı (bağlantı sağlayıcısı/ortak tesis üzerinden); daha yüksek bant genişliği, daha öngörülebilir gecikme ve güvenilirlik; **Global Reach** (şubeler arası), **ExpressRoute Direct**; Microsoft 365 ve Azure servislerine özel erişim
  - **VPN Gateway vs ExpressRoute** ⭐: şifreli ama internet üzerinden ↔ özel hat, internet değil
- **Teori — Public ve private endpoint'ler:**
  - **Public endpoint:** kaynağa internetten erişilebilen genel adres (ör. `mystorage.blob.core.windows.net`)
  - **Private endpoint** (Azure **Private Link**): PaaS servisine VNet'ten **özel IP** ile erişim; trafik Microsoft ağında kalır; public erişim kapatılabilir
  - *(bağlam)* NSG ile trafik filtreleme; Load Balancer (L4), Application Gateway (L7/WAF), Front Door, Azure Firewall — sınavda ad olarak geçebilir
- **Sınav tuzakları:** "internet üzerinden geçmeyen bağlantı" → ExpressRoute; "şifreli, internet üzerinden, düşük maliyet" → VPN Gateway; iki VNet'i bağlamak → peering (VPN değil); peering transitive değildir; Azure DNS domain satmaz; private endpoint ile public endpoint
- 🛠 **Pratik:** `az network vnet create` + iki subnet; ikinci VNet ve **peering** (iki yönlü); **private DNS zone** ve VNet bağlantısı; bir storage account'a **private endpoint** ekleyip public erişimi kapatma ve erişimi VNet içindeki VM'den test etme; kaynakları temizleme. VPN Gateway ve ExpressRoute maliyet ve kurulum süresi nedeniyle laboratuvarda kurulmaz, teoride anlatılır.
- 📝 **Quiz:** bağlantı seçenekleri (VPN/ExpressRoute/peering), VNet ve DNS senaryoları, endpoint türleri (≈18 soru)

#### C1.8 Azure Storage: Blob, Files, Taşıma ve Migration · `08-storage` ⭐
- 📌 **Resmî hedef:** *Describe Azure storage services* — storage servislerini karşılaştır · storage tier'ları · redundancy seçenekleri · storage account seçenekleri ve türleri · dosya taşıma seçenekleri (**AzCopy, Azure Storage Explorer, Azure File Sync**) · migration seçenekleri (**Azure Migrate, Azure Data Box**)
- **Teori — Azure Storage ve storage account:**
  - **Storage account:** benzersiz ad alanı, endpoint'ler (`<ad>.blob/.file/.queue/.table.core.windows.net`); türleri: **Standard general-purpose v2** (çoğu senaryo), **Premium block blobs**, **Premium file shares**, **Premium page blobs**
  - Storage servisleri: **Blob** (nesne), **Azure Files** (dosya paylaşımı), **Queue** (basit mesajlaşma), **Table** (NoSQL anahtar-değer), **Disks** (VM için blok depolama) — hangisi ne için
- **Teori — Redundancy** ⭐:

  | Seçenek | Kopya | Neye dayanır |
  |---|---|---|
  | **LRS** | Birincil bölgede tek datacenter içinde 3 kopya | Sunucu/raf arızası — datacenter felaketine **dayanmaz** |
  | **ZRS** | Birincil bölgede 3 availability zone'a | Datacenter/zone arızası |
  | **GRS** | LRS + ikincil bölgede (region pair) LRS | Bölgesel felaket (ikincil yalnızca failover sonrası okunur) |
  | **GZRS** | ZRS + ikincil bölgede LRS | Zone **ve** bölgesel felaket |
  | **RA-GRS / RA-GZRS** | GRS/GZRS + ikincilden **sürekli okuma** | Bölgesel felakette de okuma erişimi |

- **Teori — Blob Storage:**
  - Yapılandırılmamış veri (görsel, video, yedek, log, dosya); hiyerarşi: account → **container** → **blob**; blob türleri: **block** (dosyalar), **append** (log), **page** (VM diskleri)
  - **Access tier'ları:** **Hot** (sık erişim) · **Cool** (seyrek, ≥30 gün) · **Cold** (nadir, ≥90 gün) · **Archive** (çok nadir, ≥180 gün, **çevrimdışı** — okumak için **rehydrate**, saatler sürer); erişim maliyeti ↔ depolama maliyeti dengesi; lifecycle kuralları
- **Teori — Azure Files ve Azure File Sync:**
  - **Azure Files:** bulutta tam yönetilen **SMB/NFS dosya paylaşımı**; Windows/Linux/macOS'tan bağlanma; "lift-and-shift" uygulamaların paylaşılan klasörleri
  - **Azure File Sync:** on-prem **Windows Server**'ı Azure Files'ın **önbelleği** yapar; çok noktada senkron, **cloud tiering** (seyrek dosyalar bulutta, yer açılır), hızlı felaket kurtarma
- **Teori — Dosya taşıma araçları:**
  - **AzCopy:** blob/dosya kopyalama için **komut satırı** aracı; senkron, toplu kopya, storage account'lar ve bulutlar arası
  - **Azure Storage Explorer:** blob/dosya/kuyruk/tablo yönetimi için **grafik** masaüstü uygulaması (arka planda AzCopy kullanır)
  - **Azure File Sync:** yukarıda — sürekli, iki yönlü senkron
- **Teori — Migration seçenekleri:**
  - **Azure Migrate:** taşıma için **merkezi hub** — keşif, değerlendirme (boyutlandırma/maliyet), sunucu, veritabanı, web uygulaması ve VDI taşıma; entegre araçlar (Discovery and assessment, Server Migration, Data Migration Assistant, Database Migration Service, Web app migration assistant)
  - **Azure Data Box:** çok büyük veriyi **fiziksel cihazla** taşıma (Data Box Disk; yeni nesil Data Box ~120/525 TB; Data Box Heavy emekli — kapasiteler değişir); ağ bağlantısı yetersiz/pahalı/yoksa; içe ve dışa aktarma; cihaz verisi güvenle silinir
- **Sınav tuzakları:** redundancy ↔ senaryo eşleştirme ("bölgesel felakette de okunabilir" → RA-GRS/RA-GZRS; "zone arızası, tek bölge" → ZRS); Archive'dan anında okuma yok; "on-prem dosya sunucusunu Azure ile senkronla, önbellekte tut" → File Sync; "terabaytlarca veri, ağ yok" → Data Box; "komut satırı" → AzCopy, "grafik araç" → Storage Explorer; "taşıma için değerlendirme" → Azure Migrate
- 🛠 **Pratik:** `ConsoleApp` içinde (C1 klasörü) redundancy matrisi, access tier maliyet karşılaştırması ve taşıma aracı seçimi tabloları; `az storage` ile container/blob/**access tier değiştirme** ve AzCopy ile yerel klasörü blob container'a kopyalama (`azcopy copy … --recursive`) README'de CLI laboratuvarı; Azure Files paylaşımı oluşturup bağlama; aynı storage account'u LRS ve GZRS olarak Pricing Calculator'da karşılaştırma.
- 📝 **Quiz:** servis seçimi, redundancy ve tier eşleştirme, taşıma/migration aracı seçimi (≈20 soru)

#### C1.9 Kimlik ve Erişim: Microsoft Entra ID, Kimlik Doğrulama, RBAC · `09-identity-access` ⭐ *(geliştirici derinliği → M9, C2.11)*
- 📌 **Resmî hedef:** *Describe Azure identity, access, and security* — directory servisleri (**Microsoft Entra ID**, **Microsoft Entra Domain Services**) · kimlik doğrulama yöntemleri (**SSO, MFA, passwordless**) · **external identities** (B2B, B2C) · **Conditional Access** · **Azure RBAC**
- **Teori — Identity ve Microsoft Entra ID** (eski adı Azure Active Directory):
  - Kimlik = yeni güvenlik çevresi; Entra ID: Microsoft'un **bulut tabanlı kimlik ve erişim yönetimi** servisi — kullanıcı, grup, uygulama ve cihaz kimlikleri; **tenant** = kuruluşun Entra örneği
  - Kimler kullanır: BT yöneticileri (erişim kontrolü), geliştiriciler (uygulamaya SSO/kimlik ekleme), kullanıcılar (self-servis şifre sıfırlama), Microsoft 365/Azure/Dynamics aboneleri
  - On-prem **Active Directory** ile farkı (Kerberos/LDAP/GPO ↔ HTTP tabanlı modern protokoller: OAuth 2.0, OIDC, SAML); **Microsoft Entra Connect** ile on-prem AD → Entra senkronizasyonu
  - **Microsoft Entra Domain Services:** domain join, Group Policy, LDAP, Kerberos/NTLM gibi **klasik AD özelliklerini yönetilen** olarak sunar — domain controller kurmadan eski uygulamaları buluta taşımak
- **Teori — Benefits of Microsoft Entra ID:**
  - **Authentication** (kimlik doğrulama, self-servis şifre sıfırlama, MFA, akıllı kilitleme) · **Single sign-on** · **Application management** (SaaS ve şirket içi uygulamalar, uygulama proxy) · **Device management** (cihaz kaydı, Intune ile politika) · **Identity protection** (riskli oturum/kullanıcı tespiti) · **Conditional Access** · merkezi denetim ve log'lar
- **Teori — Authentication vs authorization** ⭐: **AuthN** = "sen kimsin?" (kimliği kanıtlama: şifre, MFA); **AuthZ** = "ne yapabilirsin?" (kimliği doğrulanmış kullanıcının yetkileri: RBAC); önce AuthN sonra AuthZ
- **Teori — External identities:**
  - **B2B collaboration:** iş ortaklarını kendi kimlikleriyle (kendi Entra/Microsoft/Google hesabı) **misafir** olarak davet etme; kendi tenant'ında erişim yönetimi; B2B direct connect (Teams paylaşımlı kanallar)
  - **B2C / Microsoft Entra External ID:** **müşteriye dönük** uygulamalar için kimlik (kayıt, giriş, sosyal hesaplar, özelleştirilebilir sayfalar); Azure AD B2C 1 Mayıs 2025'ten beri yeni müşterilere kapalı (mevcutlara destek sürüyor) → **Entra External ID**
- **Teori — Single Sign-On (SSO):** tek kimlikle **birden çok uygulamaya** bir kez giriş; kullanıcı deneyimi + güvenlik (tek kimliğin yönetimi, hesap kapatıldığında tüm erişimin bitmesi); ne kadar çok kimlik o kadar çok şifre/risk
- **Teori — Multi-Factor Authentication (MFA / 2FA):**
  - İki veya daha fazla **faktör**: **bildiğin** (şifre, PIN) · **sahip olduğun** (telefon, donanım anahtarı) · **olduğun** (parmak izi, yüz); 2FA = MFA'nın iki faktörlü hâli
  - Entra MFA yöntemleri: Microsoft Authenticator bildirimi/kodu, SMS, sesli arama, OATH token, FIDO2; **security defaults** ile temel MFA; şifre çalınsa bile hesabın korunması
- **Teori — Passwordless:**
  - Şifreyi tamamen kaldırıp "sahip olduğun + olduğun/bildiğin (cihaz PIN'i)" ile giriş: **Windows Hello for Business**, **Microsoft Authenticator** (telefonla oturum), **FIDO2 güvenlik anahtarları / passkey**; daha güvenli (phishing'e dayanıklı) ve daha kolay
- **Teori — Conditional Access:**
  - **Sinyal → karar → uygulama:** sinyaller (kullanıcı/grup, **konum/IP**, **cihaz** durumu, uygulama, **oturum/kullanıcı riski**) → kararlar (**izin ver**, **MFA iste**, cihazı uyumlu olmaya zorla, **engelle**)
  - Örnek politikalar: "yöneticiler her zaman MFA", "bilinmeyen konumdan girişte MFA", "uyumsuz cihazdan e-postaya erişim yok"; Entra ID P1 lisansı gerektirir *(bağlam)*
- **Teori — Role-Based Access Control (RBAC)** ⭐:
  - **Rol** (izin kümesi) + **güvenlik sorumlusu** (kullanıcı, grup, service principal, managed identity) + **kapsam** (management group, subscription, resource group, kaynak) = **rol ataması**
  - Built-in roller: **Owner** (her şey + erişim yönetimi), **Contributor** (her şey, erişim yönetimi hariç), **Reader** (yalnızca görüntüleme), **User Access Administrator**; servise özel roller; özel (custom) roller
  - Atama **kapsamdan aşağı miras** alınır; izinler toplanır (allow model); **en az yetki** ilkesi; Azure Resource Manager üzerinden uygulanır
  - RBAC vs **Entra rolleri** (Global Administrator gibi directory rolleri) vs **Azure Policy** (→ C1.12)
- **Sınav tuzakları:** Entra ID ile Entra Domain Services ("klasik domain join ve LDAP" → Domain Services); B2B ile B2C; SSO ile MFA; Conditional Access'in **MFA'yı koşula bağladığı**; AuthN ile AuthZ eşleştirme; "yalnızca görüntüleme" → Reader; "erişim yönetimi hariç her şey" → Contributor; RBAC'ın miras alınması
- 🛠 **Pratik:** Entra'da bir kullanıcı ve grup oluşturma; bir resource group üzerinde **Reader** rolünü gruba atama ve yetkinin yalnızca o kapsamda geçerli olduğunu doğrulama (`az role assignment create/list`); Microsoft Graph `/invitations` ile (`az rest`) bir B2B misafir kullanıcı daveti; temizlik. MFA/security defaults ve Conditional Access şablonları teoride anlatılır.
- 📝 **Quiz:** kimlik servisi seçimi, Entra faydaları, AuthN/AuthZ, external identities, SSO/MFA/passwordless/Conditional Access, RBAC kapsamı ve rolleri (≈22 soru)

#### C1.10 Güvenlik: Zero Trust, Defense in Depth, Microsoft Defender for Cloud · `10-security` ⭐
- 📌 **Resmî hedef:** *Describe Azure identity, access, and security* — **Zero Trust** kavramı · **defense-in-depth** modelinin amacı · **Microsoft Defender for Cloud**'un amacı
- **Teori — Zero-Trust model of security:**
  - Varsayım: ağın içi de güvenli değildir — "**asla güvenme, her zaman doğrula**"; geleneksel "kale ve hendek" (güvenli iç ağ) modeline karşı
  - Üç ilke ⭐: **Verify explicitly** (her isteği tüm sinyallerle doğrula: kimlik, konum, cihaz, servis, veri sınıfı, anomali) · **Use least privilege access** (just-in-time/just-enough-access, risk tabanlı politika) · **Assume breach** (etki alanını küçült — segmentasyon, uçtan uca şifreleme, analitikle tespit)
  - Azure'daki karşılıkları: Entra ID + Conditional Access + MFA, RBAC + PIM *(bağlam)*, private endpoint ve segmentasyon
- **Teori — Defense in depth:**
  - Tek bir kontrol aşılsa bile diğerleri korur: katmanlı savunma; katmanlar (dıştan içe) ⭐: **fiziksel güvenlik** (datacenter erişimi) → **kimlik ve erişim** (SSO, MFA, denetim) → **çevre** (perimeter: DDoS koruması, çevre firewall'ları) → **ağ** (segmentasyon, NSG, varsayılan engelleme) → **compute** (VM erişimi, yamalar, uç nokta koruması) → **uygulama** (güvenli geliştirme, secret'ları Key Vault'ta tutmak) → **veri** (şifreleme, erişim kontrolü — saldırganın asıl hedefi)
  - Her katmana bir Azure servisi eşleme: DDoS Protection, Azure Firewall, NSG, Defender for Cloud, Key Vault, şifreleme
- **Teori — Microsoft Defender for Cloud:**
  - **Bulut güvenliği duruş yönetimi (CSPM)** ve **iş yükü koruması (CWPP)**: Azure, **on-prem** (Arc ile) ve **diğer bulutlar** (AWS, GCP)
  - **Assess** (sürekli değerlendirme, zafiyet tarama) · **Secure** (**secure score**, öneriler, Azure Policy tabanlı güvenlik benchmark'ı) · **Defend** (tehdit tespiti, güvenlik uyarıları, olay müdahalesi); Defender planları (Servers, Storage, SQL, Containers…)
- **Sınav tuzakları:** Zero Trust ilkesi ↔ uygulama eşleştirme; defense in depth katman sırası ve katman ↔ kontrol eşleştirmesi; Defender for Cloud'un multicloud/hybrid kapsamı; Defender for Cloud ile Azure Policy ve Advisor farkı
- 🛠 **Pratik:** `az security` komutlarıyla Defender for Cloud **secure score** ve önerilerine bakma; public blob erişimine açık bir storage account'u `--allow-blob-public-access false` ile düzeltme; kendi örnek mimarin için defense in depth katman tablosunu doldurma (README'de şablon + çözüm).
- 📝 **Quiz:** Zero Trust ilkeleri, defense in depth katmanları, Defender for Cloud yetenekleri (≈15 soru)

#### C1.11 Azure'da Maliyet Yönetimi · `11-cost-management` ⭐
- 📌 **Resmî hedef:** *Describe cost management in Azure* — **maliyeti etkileyen faktörler** · **Pricing Calculator** *(TCO Calculator'ın güncel müfredattaki yeri için study guide'ı kontrol et)* · **Microsoft Cost Management** yetenekleri · **tag**'lerin amacı
- **Teori — Factors that affect cost:**
  - **Kaynak türü ve ayarları** (SKU, boyut, tier), **tüketim** (pay-as-you-go ↔ **reservations** / savings plan indirimi), **bakım** (unutulan/kullanılmayan kaynaklar — VM silinince disk/IP kalabilir), **coğrafya/bölge** (aynı servis bölgeye göre farklı fiyat), **ağ trafiği** (gelen genelde ücretsiz, **giden/egress** ve bölgeler/zone'lar arası trafik ücretli; bant genişliği bölgeleri), **subscription türü** (free, kredi), **Azure Marketplace** (üçüncü taraf ürün ücretleri)
  - Tasarruf yolları: reservations, savings plan, **spot VM**, **Azure Hybrid Benefit** (mevcut Windows Server/SQL Server lisansları), dev/test fiyatlandırması, doğru boyutlandırma (Advisor), otomatik kapatma, kaynak silme
- **Teori — Azure Pricing Calculator:**
  - Henüz kurulmamış bir iş yükünün **tahmini maliyetini** hesaplama: servis ekle → bölge, tier, saat, adet seç → aylık tahmin; dışa aktarma ve paylaşma; tahmin olduğu, fatura olmadığı
  - *(bağlam)* **TCO Calculator** *(bağımsız araç kaldırıldı; karşılaştırma artık **Azure Migrate business case** ile — bkz. 2026 Radarı)*: on-prem altyapının (sunucu, depolama, ağ, işçilik, enerji) Azure ile **toplam sahip olma maliyeti** karşılaştırması — Pricing Calculator ile farkı klasik bir sorudur
- **Teori — Azure Cost Management:**
  - **Cost analysis** (gerçekleşen maliyetleri kaynak/servis/bölge/**tag** bazında görme, trend), **budgets** (limit + eşik **uyarıları**, e-posta/action group), **cost alerts** (bütçe, kredi, departman harcama kotası), **öneriler** (Advisor ile), **dışa aktarma** ve zamanlanmış raporlar; birden çok subscription ve bulut
- **Teori — Resource tags:**
  - **Anahtar-değer** meta veri (`env=prod`, `costCenter=1234`, `owner=ali`); kullanım: **maliyet yönetimi ve raporlama**, kaynak yönetimi/arama, operasyon (otomatik kapatma), güvenlik (veri sınıflandırma), uyumluluk
  - Kurallar: tag'ler **varsayılan olarak miras alınmaz** (resource group tag'i içindeki kaynağa geçmez); **Azure Policy** ile zorunlu kılma veya gruptan kopyalama (Modify etkisi); portal/CLI/ARM ile ekleme
- **Sınav tuzakları:** Pricing ile TCO Calculator; "bütçe %80 olunca e-posta" → Cost Management budget; egress ücreti ve bölge farkı; "maliyeti departmana göre raporla" → tag; tag mirası; reservation ile spot
- 🛠 **Pratik:** **Pricing Calculator**'da küçük bir web uygulaması (App Service + SQL Database + storage) tahmini (README'de şablon); **budget** (aylık limit, %80 ve %100 eşikleri) ve alarm; `az tag`/`az resource tag` ile `env`/`owner`/`costCenter` tag'leri ve **tag bazlı** maliyet raporu (ConsoleApp'te tag mirası olmadığını gösteren örnek rapor); `ConsoleApp` hesaplayıcısına 3 yıllık **CapEx (on-prem) vs OpEx (cloud)** karşılaştırması.
- 📝 **Quiz:** maliyet faktörleri, araç seçimi, tasarruf yolu, budget ve tag senaryoları (≈18 soru)

#### C1.12 Azure Yönetişim ve Uyumluluk · `12-governance-compliance` ⭐
- 📌 **Resmî hedef:** *Describe features and tools in Azure for governance and compliance* — **Microsoft Purview** · **Azure Policy** · **resource lock**'lar
- **Teori — Azure governance and compliance (genel çerçeve):**
  - Yönetişim = kuruluşun kurallarını (güvenlik, maliyet, uyumluluk, adlandırma) **ölçekte** uygulamak ve denetlemek; araç seti: management group'lar (→ C1.4), RBAC (→ C1.9), **Policy**, **lock**, tag (→ C1.11), Purview
  - *(bağlam)* Uyumluluk kaynakları: **Microsoft Service Trust Portal** (denetim raporları, ISO/SOC belgeleri), Microsoft Trust Center, gizlilik bildirimi ve ürün koşulları
- **Teori — Azure Policy** ⭐:
  - Kaynakların **özelliklerinin** kurallara uygunluğunu **değerlendirip zorlama** (oluşturma/güncelleme anında ve mevcut kaynaklar için sürekli)
  - Yapı taşları: **policy definition** (kural, JSON), **assignment** (kapsama atama: management group/subscription/RG; istisna), **initiative** (policy set — birlikte uygulanan tanımlar; ör. güvenlik benchmark'ı)
  - **Etkiler:** **Audit** (uyarı/raporla), **Deny** (oluşturmayı engelle), **Modify** / **Append** (tag ekle/düzelt), **DeployIfNotExists** (eksik ayarı kur, ör. tanılama), AuditIfNotExists, Disabled
  - Örnekler: **izin verilen bölgeler**, izin verilen VM SKU'ları, **zorunlu tag**, gruptan tag devralma; **uyumluluk** panosu ve **remediation** (mevcut uyumsuz kaynakları düzeltme)
  - Policy ↔ RBAC: RBAC **kullanıcının** ne yapabileceğini, Policy **kaynağın** nasıl olabileceğini belirler; Owner bile Deny politikasına takılır
  - Bir politikanın yaşam döngüsü: built-in tanımı seç (ör. *Require a tag and its value on resources*, *Allowed locations*) → kapsama **ata** → aykırı yeni kaynak **Deny** ile reddedilir → mevcut uyumsuz kaynaklar **compliance** panosunda listelenir → gerekiyorsa **remediation task** ile düzeltilir
- **Teori — Resource locks:**
  - Kaynakların **kazara silinmesini/değiştirilmesini** önler; türler: **Delete (CanNotDelete)** — okunur ve değiştirilir ama silinemez · **ReadOnly** — okunur, değiştirilemez ve silinemez (bazı yönetim işlemlerini de engelleyebilir)
  - Subscription/RG/kaynak düzeyinde; alt kaynaklara **miras**; **rol yetkisinden bağımsızdır** — Owner bile işlem için önce lock'ı kaldırmak zorundadır (lock'ı kaldırmak da yetki ister)
- **Teori — Microsoft Purview:**
  - **Veri yönetişimi, risk ve uyumluluk** çözümleri ailesi: tüm verinin (on-prem, multicloud, SaaS) **haritası/kataloğu**, otomatik **keşif ve sınıflandırma** (hassas veri, ör. kredi kartı numarası), veri soy ağacı (lineage), erişim/yaşam döngüsü yönetimi
  - İki ana alan: **risk ve uyumluluk** (Microsoft 365 verisi: bilgi koruma, veri kaybı önleme) ve **birleşik veri yönetişimi** (Azure SQL, Data Lake, S3 vb. veri kaynakları)
- **Sınav tuzakları:** **Policy vs RBAC**; Policy vs lock; Delete vs ReadOnly; lock'ın Owner'ı da durdurması ve miras alınması; Purview'un ne yaptığı (veri yönetişimi — kimlik veya maliyet aracı değil); initiative'in ne olduğu
- 🛠 **Pratik:** `az policy assignment create` ile *Allowed locations* politikasını bir RG'ye atayıp izin verilmeyen bölgede kaynak oluşturma denemesinin reddedildiğini görme; production RG'ye `CanNotDelete` lock koyup silme denemesinin **reddedildiğini** gösterme; ReadOnly lock'ın bir ayarı değiştirmeyi engellediğini gözlemleme; lock'ları kaldırıp temizleme (`az lock create/list/delete`).
- 📝 **Quiz:** Policy etkileri ve kapsamı, Policy/RBAC/lock ayrımı, lock türleri, Purview senaryoları (≈18 soru)

#### C1.13 Azure Kaynaklarını Yönetme ve Dağıtma Araçları · `13-management-deployment-tools` ⭐
- 📌 **Resmî hedef:** *Describe features and tools for managing and deploying Azure resources* — **Azure portal** · **Azure Cloud Shell** (Azure CLI ve Azure PowerShell dahil) · **Azure Arc** · **infrastructure as code** · **Azure Resource Manager** ve **ARM template**'leri
- **Teori — The Azure portal:**
  - Web tabanlı birleşik konsol: kaynak oluşturma/yönetme, **dashboard**'lar (özelleştirilebilir), izleme, erişilebilirlik; sürekli güncellenir, kesintisiz bakım; tekrarlanabilir/otomatik işler için uygun değildir
- **Teori — Azure Cloud Shell:**
  - Tarayıcıdan (portal içinden veya `shell.azure.com`) erişilen, **kimliği hazır** etkileşimli kabuk; **Bash** (Azure CLI) veya **PowerShell** (Azure PowerShell); kurulum gerektirmez, araçlar güncel, dosyalar için isteğe bağlı depolama
  - **Azure CLI** (`az …`, Bash'e yakın sözdizimi, platformlar arası) vs **Azure PowerShell** (`New-AzResourceGroup` gibi cmdlet'ler) — aynı yetenekler, farklı ekosistem; yerel kurulum da mümkündür
  - Aynı iş iki sözdizimiyle: `az group create --name rg-demo --location westeurope` ↔ `New-AzResourceGroup -Name rg-demo -Location westeurope`; çıktı süzme: `--query` (JMESPath) ve `-o table`
- **Teori — Azure Arc:**
  - Azure dışındaki kaynakları (on-prem sunucular, **diğer bulutlardaki** sunucular, **Kubernetes** kümeleri, SQL Server/veri servisleri, VMware/SCVMM VM'leri) **Azure Resource Manager'a yansıtıp** Azure'dan yönetme
  - Sağladıkları: tek panelde envanter, **Azure Policy** ve **RBAC**, **Defender for Cloud**, Azure Monitor, tag'ler; hybrid ve multicloud yönetişimi
- **Teori — Azure Resource Manager (ARM)** ⭐:
  - Azure'un **dağıtım ve yönetim servisi**: portal, CLI, PowerShell, REST API, SDK'dan gelen **tüm** istekler ARM'dan geçer → tutarlı sonuç, kimlik doğrulama/yetkilendirme, RBAC/tag/lock/Policy'nin uygulandığı tek nokta
  - Faydaları: kaynakları **grup halinde** yönetme, **deklaratif şablonlarla** dağıtım, bağımlılıkların doğru sırayla kurulması, tekrar tekrar tutarlı dağıtım
- **Teori — Infrastructure as Code (IaC):**
  - Altyapıyı **kodla** (dosyalarla) tanımlayıp dağıtmak: tekrarlanabilirlik, sürüm kontrolü ve kod incelemesi, ortamlar arası tutarlılık, **idempotent** dağıtım (aynı şablon tekrar uygulandığında aynı sonuç), otomasyon (CI/CD)
  - **Deklaratif** ("ne istediğim") vs imperatif ("adım adım ne yapılacağı") yaklaşım; Azure'da: **ARM template**, **Bicep**; *(bağlam)* Terraform (→ S8.2)
- **Teori — ARM templates:**
  - **JSON** biçiminde deklaratif şablon: `parameters`, `variables`, `resources`, `outputs`; **paralel dağıtım** (ARM bağımlılık sırasını kendisi çözer), şablon doğrulama (validation) ve önizleme (**what-if**), modüler şablonlar, Template Specs *(bağlam)*
  - **Bicep:** ARM template'in okunabilir DSL'i; **ARM JSON'a derlenir**; aynı yetenekler, daha kısa sözdizimi
  - **Portalda şablon üretme:** kaynak oluşturma sihirbazının **Review + create** adımındaki *Download a template for automation* ve mevcut kaynak/resource group için **Export template** — portalda tıklanarak yapılan işin ARM JSON karşılığını verir; otomasyona başlangıç noktasıdır (dışa aktarılan şablon genellikle temizlenip parametrelenmelidir)
- 🛠 **Pratik:** resource group + storage account + App Service plan + web app'i `az` CLI ile oluşturup silme; aynı storage account'u bir ARM template/Bicep dosyasıyla `az deployment group create` ile dağıtma ve ikinci dağıtımda `what-if` ile **değişiklik olmadığını** (idempotent) görme; `az bicep build` ile Bicep → ARM JSON; CLI ↔ Azure PowerShell karşılıkları.
- **Sınav tuzakları:** "aynı ortamı tekrar tekrar tutarlı kur" → ARM template/IaC; "kurulumsuz tarayıcı komut satırı" → Cloud Shell; "başka buluttaki sunucuya Azure Policy uygula" → Arc; tüm isteklerin ARM'dan geçmesi; ARM template biçimi (JSON) ve deklaratif olması; CLI ile PowerShell'in aynı işi yapabilmesi
- 📝 **Quiz:** araç seçimi, ARM/IaC nitelikleri, Arc senaryoları, Cloud Shell (≈16 soru)

#### C1.14 Azure İzleme Araçları · `14-monitoring-tools` ⭐
- 📌 **Resmî hedef:** *Describe monitoring tools in Azure* — **Azure Advisor** · **Azure Service Health** · **Azure Monitor** (Log Analytics, Azure Monitor alert'leri, Application Insights dahil)
- **Teori — Azure Advisor:**
  - Kaynak yapılandırmanı ve kullanım verini analiz eden **kişiselleştirilmiş öneri** servisi; beş kategori ⭐: **Reliability**, **Security**, **Performance**, **Cost**, **Operational Excellence**; **Advisor score**; öneriyi uygulama/erteleme/kapatma, uyarı kurma, öneri dışa aktarma
  - Örnekler: az kullanılan VM'i küçült/kapat (maliyet), tek örnekli VM'e availability set öner (güvenilirlik)
- **Teori — Azure Service Health:**
  - Üç parça ⭐: **Azure Status** (tüm Azure'un **genel** durum sayfası, herkese açık) · **Service Health** (**senin** subscription ve bölgelerini etkileyen olaylar: hizmet sorunları, **planlı bakım**, sağlık tavsiyeleri, güvenlik bildirimleri; olay geçmişi ve kök neden raporları; uyarı kuralları) · **Resource Health** (**tek bir kaynağın** mevcut ve geçmiş sağlık durumu)
  - Kullanım: "uygulamam mı bozuk, Azure mı?" sorusunu ayırt etmek
- **Teori — Azure Monitor:**
  - Azure, on-prem ve diğer bulutlardan **metrik** ve **log** toplayan, analiz eden ve harekete geçiren **birleşik izleme platformu**; veri kaynakları: uygulama, işletim sistemi, Azure kaynakları, subscription (Activity Log), tenant
  - **Log Analytics:** log verisini **KQL** ile sorgulama aracı (workspace) (→ C2.13) · **Azure Monitor alerts:** metrik/log/aktivite koşulu → **action group** (e-posta, SMS, webhook, Logic App, Function) · **Application Insights:** uygulama performans izleme (istek süreleri, hata oranı, bağımlılıklar, sayfa görüntüleme, **availability test**'ler)
- **Teori — Azure Monitor merkezi dashboard'u:**
  - Portal'daki **Azure Monitor** sayfası: tüm izleme yeteneklerine tek giriş noktası (metrics explorer, logs, alerts, insights)
  - **Azure dashboard**'ları: metrik grafiklerini, log sorgu sonuçlarını ve kaynak kutucuklarını **tek ekranda** toplama, paylaşma (RBAC ile); *(bağlam)* Grafana entegrasyonu
  - **Insights:** VM insights, Container insights, Application Insights gibi hazır **küratörlü** görünümler
- **Teori — Azure Monitor raporları:**
  - **Workbooks:** metrik, log ve metni birleştiren **etkileşimli rapor** şablonları (parametreler, grafikler, tablolar); hazır galeri şablonları ve özel raporlar; paylaşılabilir
  - Log Analytics sorgularından grafik (`render`), alert geçmişi ve **Activity Log** raporları; *(bağlam)* Power BI'a aktarma
- **Sınav tuzakları:** Advisor ile Service Health (öneri ↔ olay); Service Health ile Resource Health ile Azure Status (kapsam farkı); "uygulamanın yavaş isteklerini izle" → Application Insights; "metrik eşiği aşınca SMS" → alert + action group; "log'ları sorgula" → Log Analytics; "etkileşimli rapor" → Workbooks
- 🛠 **Pratik:** `az advisor recommendation list -o table` ile önerileri listeleme; Service Health için bir **activity log alert** (hizmet sorunu/planlı bakım → e-posta); bir storage account'a **metrik alert + action group**; Log Analytics workspace'te basit bir KQL sorgusu; temizlik. Azure dashboard'ları ve Workbook'lar teoride anlatılır.
- 📝 **Quiz:** izleme aracı seçimi, Advisor kategorileri, Service Health bileşenleri, Monitor özellikleri (≈18 soru)

#### C1.15 Deneme Sınavı ve Sınav Stratejisi · `15-practice-exam`
- **Tam uzunlukta deneme sınavı** (≈50 soru) — alan ağırlıklarına göre dağıtılmış (%25-30 / %35-40 / %30-35); tek/çoklu seçim, evet-hayır tablosu, sıralama, senaryo
- **Puanlama:** alan bazında puan tablosu; her yanlışı konu koduna (C1.x) bağlayan tekrar planı
- **Sınav teknikleri:** "NOT / EXCEPT" soruları, eleme yöntemi, anahtar kelimeler ("en az yönetim yükü", "en düşük maliyet", "bölgesel felaket", "internetten geçmeden", "otomatik"), evet-hayır tablolarında her satırı bağımsız değerlendirmek, zaman yönetimi
- **Sınav günü:** Pearson VUE test merkezi veya çevrimiçi gözetim kuralları; resmî ücretsiz **practice assessment**; Microsoft Learn'deki ücretsiz AZ-900 learning path ve sandbox'lar
- 🛠 **Pratik:** Deneme sınavını zamanlı çöz → yanlışları C1.x ile eşleştir → zayıf alan tekrar listesi; ardından resmî practice assessment'ı kapalı kitap çöz.
- 📝 **Quiz:** Deneme sınavı + cevap anahtarı, gerekçeler ve alan bazlı puan tablosu

#### Genişletilmiş kapsam — C1.16 – C1.22 (kurs müfredatlarında işlenen, study guide'ın kenarındaki veya dışındaki konular)

> **Neden ek konular?** Resmî study guide (July 20, 2026) yalnızca yukarıdaki C1.1 – C1.14 hedeflerini ölçer; deneme sınavı (C1.15) da bunlardan çıkar. Ama Udemy/Microsoft Learn/eğitim kurumu AZ-900 kurslarının büyük çoğunluğu — ve gerçek Azure işi — şu konuları da işler: **veri ve analitik servisleri, yük dengeleme, ağ güvenliği, yedekleme/felaket kurtarma, AI/IoT servisleri, entegrasyon ve DevOps araçları, destek planları/SLA/servis yaşam döngüsü**. Bunlar sınavda *doğrudan hedef olarak yazmaz* ama (a) eski sürüm soruları ve çalışma materyalleri bunlara dayanır, (b) senaryo sorularında seçeneklerde (distractor) geçerler, (c) AI-200 (C2) ve AZ-305 (S8.3) için zemindir. Hepsi **(bağlam)** etiketlidir; her konu hangi resmî hedefe bağlandığını README başında söyler. Hedef: geniş kurs kataloğunu bu repoda tek yerde kapatmak. Sıra serbest; sınav alanı eşlemesi her konunun başındadır.

#### C1.16 Azure Veri ve Analitik Servisleri · `16-data-analytics-services` *(bağlam)*
- 📌 **Bağlantı:** *Describe Azure architecture and services* alanındaki servis tanıma soruları; C2.4–C2.7 (geliştirici derinliği) ve M6/M8 (veritabanı) için zemin
- **Teori — ilişkisel (yönetilen) veritabanları:** Azure SQL ailesi ⭐ (**Azure SQL Database** — tek veritabanı/elastic pool, **SQL Managed Instance** — neredeyse tam SQL Server uyumu, **SQL Server on Azure VM** — IaaS) ile **Azure Database for PostgreSQL / MySQL**; PaaS faydaları (otomatik yedek, yama, HA, ölçekleme), DTU ↔ vCore, serverless compute tier; hangi senaryoda hangisi (lift-and-shift → Managed Instance; yeni uygulama → SQL Database)
- **Teori — NoSQL ve diğer:** **Azure Cosmos DB** ⭐ (çok modelli, küresel dağıtım, tek haneli ms gecikme, tutarlılık düzeyleri, RU/s), **Azure Managed Redis** (cache), **Table/Blob** (Storage'ta NoSQL benzeri); ilişkisel ↔ NoSQL karar tablosu
- **Teori — analitik ve büyük veri:** **Microsoft Fabric** (birleşik analitik SaaS), **Azure Synapse Analytics**, **Azure Databricks**, **HDInsight**, **Azure Data Factory** (ETL/ELT orkestrasyonu), **Azure Data Lake Storage Gen2**, **Stream Analytics**/**Event Hubs** (akan veri); OLTP ↔ OLAP, batch ↔ streaming; hangi servis hangi iş (veri taşıma, dönüştürme, depolama, analiz, raporlama — Power BI)
- **Teori — taşıma ve güvenlik:** **Azure Database Migration Service** / Azure Migrate (→ C1.19), şifreleme (TDE, bağlantıda TLS), Entra kimlik doğrulama, private endpoint (→ C1.7), geo-replication ve yedekleme özeti
- **Sınav tuzakları:** "yönetilen veritabanı = PaaS" (VM'de SQL ≠ PaaS); Managed Instance ↔ SQL Database; Cosmos DB'nin "birden çok region'da yazma"; "veri ambarı/analitik" → Synapse/Fabric, "Spark tabanlı ortak çalışma" → Databricks; ETL → Data Factory
- 🛠 **Pratik:** `az sql server create` + `az sql db create --compute-model Serverless` (otomatik duraklatma) ile ücretsiz/ucuz bir SQL Database ve temizlik; Cosmos DB free tier ve emulator'ü *(bağlam)* README'de; karar tablosu alıştırması (senaryo → servis). Portal demoları yok.
- 📝 **Quiz:** servis seçimi, SQL ailesi karşılaştırması, ilişkisel/NoSQL, analitik servis eşleştirme (≈15 soru)

#### C1.17 Yük Dengeleme ve Trafik Dağıtımı · `17-load-balancing-traffic` *(bağlam)* ⭐
- 📌 **Bağlantı:** *Describe Azure networking services* (study guide'da "load balancing" adıyla yazmaz ama C1.7 ve C1.5'in senaryo soruları bunu varsayar); S8.3 (AZ-305) için zemin
- **Teori:** dört servis, iki eksen ⭐ — **bölgesel ↔ global** ve **katman 4 (TCP/UDP) ↔ katman 7 (HTTP/HTTPS)**: **Azure Load Balancer** (L4, bölgesel; public/internal; Standard ↔ Basic; health probe, yük dengeleme kuralları, inbound NAT, zone-redundant frontend) · **Application Gateway** (L7, bölgesel; URL/host tabanlı yönlendirme, SSL termination, cookie affinity, **WAF**) · **Azure Front Door** (L7, **global**; CDN + hızlandırma + WAF + yüklü failover; edge'de POP) · **Traffic Manager** (DNS tabanlı, **global**; yönlendirme yöntemleri: priority, weighted, performance, geographic, multivalue, subnet — trafik içinden geçmez, yalnızca DNS yanıtı)
- **Teori:** karar ağacı (internal mi/public mi · bölgesel/global · HTTP/non-HTTP · WAF gerekli mi · DNS-based failover) ve **kombinasyon örüntüleri** (Front Door + Application Gateway + Load Balancer); health probe mantığı, availability zone ile birlikte kullanım (→ C1.4)
- **Teori — bağlam:** **Azure CDN** (statik içerik önbellekleme), **Azure Bastion** (→ C1.18); 2026 notları — hangi ürünler yeniden markalandı/emekli oldu (doğrula: Azure CDN (klasik) sunumları, Front Door tier'ları)
- **Sınav tuzakları:** Traffic Manager ↔ Front Door (DNS ↔ proxy); Application Gateway ↔ Load Balancer (L7 ↔ L4); "SSL offload/WAF/URL routing" → Application Gateway/Front Door; "global HTTP + hızlandırma" → Front Door
- 🛠 **Pratik:** `az network lb create` + iki VM'li backend pool ve health probe (veya `az network traffic-manager profile create` — ucuz, DNS tabanlı) ile yönlendirme yöntemini değiştirip `nslookup` ile sonucu görme; temizlik.
- 📝 **Quiz:** senaryo → hangi servis tabloları, L4/L7, bölgesel/global eşleştirme, Traffic Manager yöntemleri (≈16 soru)

#### C1.18 Ağ Güvenliği, Bastion ve Key Vault · `18-network-security-key-vault` *(bağlam)* ⭐
- 📌 **Bağlantı:** *Describe Azure identity, access, and security* + C1.7/C1.10'daki "defense in depth" soruları; C2.11 (Key Vault geliştirici derinliği), M9, S8.3
- **Teori — ağ güvenliği:** **Network Security Group (NSG)** ⭐ (subnet/NIC; öncelik, allow/deny, hizmet etiketleri, varsayılan kurallar; **stateful**) · **Application Security Group (ASG)** · **Azure Firewall** (yönetilen, stateful L3–L7, FQDN kuralları, threat intelligence; NSG ile farkı) · **Azure DDoS Protection** (Network ↔ IP; temel koruma her zaman açık) · **Web Application Firewall** (→ C1.17) · **Azure Bastion** (portaldan/SSH-RDP, public IP'siz VM erişimi) · **service endpoint ↔ private endpoint** (→ C1.7) · **Just-in-time VM erişimi** (→ C1.10)
- **Teori — sırlar ve anahtarlar:** **Azure Key Vault** ⭐ (secret, key, certificate; Standard ↔ Premium/HSM; **Managed HSM** özeti; erişim: **RBAC** önerilir ↔ eski access policy; soft delete + purge protection; **managed identity** ile kimlik bilgisiz erişim); şifreleme özeti: encryption at rest (Storage/SQL/disk — platform-managed ↔ customer-managed key), in transit (TLS), **Azure Disk Encryption**
- **Teori — bağlam:** **Microsoft Sentinel** (SIEM/SOAR özeti), **Microsoft Defender XDR** ailesi ile Defender for Cloud ilişkisi (→ C1.10), **Customer Lockbox**; güvenlik sorumluluğu paylaşımı (→ C1.1)
- **Sınav tuzakları:** NSG ↔ Azure Firewall (alt ağ filtresi ↔ merkezi, FQDN'li); DDoS koruması temel/standart farkı; Bastion = public IP'siz erişim; "secret'ı kodda tutma" → Key Vault + managed identity
- 🛠 **Pratik:** `az network nsg create` + `rule create` (80/443 izin, 22/3389 kaynak IP kısıtı); `az keyvault create --enable-rbac-authorization` + secret oluşturma/okuma + soft delete/purge; Bastion'ı **ücretli olduğu için teoride** bırak (README'de maliyet uyarısı); temizlik.
- 📝 **Quiz:** NSG/Firewall/DDoS/Bastion seçimi, Key Vault kavramları, savunma katmanları eşleştirme (≈16 soru)

#### C1.19 Yedekleme, Felaket Kurtarma ve Taşıma · `19-backup-dr-migration` *(bağlam)*
- 📌 **Bağlantı:** *Describe Azure storage services* (Azure Migrate, Data Box → C1.8) ve *benefits of the cloud* (reliability/HA → C1.2); S8.3 için zemin
- **Teori:** yedekleme ↔ felaket kurtarma ↔ yüksek erişilebilirlik ayrımı ⭐; **RPO / RTO** kavramları; **Azure Backup** (Recovery Services vault, **Backup vault**, desteklenen iş yükleri: VM, SQL/SAP HANA in VM, Azure Files, Blobs, diskler; saklama politikaları, soft delete, immutable vault; backup ↔ snapshot) · **Azure Site Recovery** (VM/fiziksel/on-prem → Azure veya region→region replikasyon, failover/failback, recovery plan, test failover) · **depolama redundancy'sinin yedek olmadığı** (silmeyi/bozmayı kopyalar → C1.8)
- **Teori — taşıma:** **Azure Migrate** (discover/assess/migrate hub'ı; 5R/6R kavramı: rehost, refactor, rearchitect, rebuild, replace), **Azure Database Migration Service**, **Azure Data Box** ailesi (→ C1.8), **Azure Resource Mover** *(bağlam)*, **Azure Arc** ile hibrit yönetim (→ C1.13); Cloud Adoption Framework'ün "migrate" aşaması (→ C1.22)
- **Sınav tuzakları:** Backup ↔ Site Recovery (veri geri yükleme ↔ iş sürekliliği/failover); "GRS yedektir" yanılgısı; Azure Migrate'in "değerlendir + taşı" rolü; "çevrimdışı büyük veri taşıma" → Data Box
- 🛠 **Pratik:** `az backup vault create` + VM için varsayılan politika ile korumayı açma + isteğe bağlı yedek tetikleme; soft delete davranışı; temizlik (vault'ta koruma durdurulmadan silinmeyeceği uyarısı dahil).
- 📝 **Quiz:** RPO/RTO yorumlama, Backup/Site Recovery/Migrate eşleştirme, taşıma stratejisi senaryoları (≈14 soru)

#### C1.20 Azure AI, Machine Learning ve IoT Servisleri · `20-ai-iot-services` *(bağlam)*
- 📌 **Bağlantı:** AZ-900'ün eski sürümlerinde ve birçok kursta "popular Azure services" bölümü; **AI-200 (C2) ve AI-102/AI-900 yoluna zemin**
- **Teori — AI servisleri:** **Microsoft Foundry** (eski adlarıyla Azure AI Foundry / AI Studio — 2026 adlandırmasını doğrula) ve **Azure OpenAI/Foundry Models**, **Azure AI services** (Vision, Speech, Language, Document Intelligence, Translator, AI Search) ↔ **Azure Machine Learning** (kendi modelini eğit/yönet; MLOps) ↔ **Azure AI Bot/Agent** servisleri özeti; hazır API ↔ özel model karar tablosu; sorumlu AI ilkeleri (fairness, reliability & safety, privacy & security, inclusiveness, transparency, accountability) *(bağlam)*
- **Teori — IoT:** **Azure IoT Hub** (cihaz-bulut çift yönlü, device twin, **DPS**), **IoT Central** (SaaS), **Azure Digital Twins**, **Azure Sphere/IoT Edge** özeti; uçtan uca akış: cihaz → IoT Hub → Stream Analytics/Event Hubs → depolama/Power BI
- **Teori — diğer:** **Azure Batch**/HPC *(bağlam)*, **Azure Stack / Azure Local** (hibrit; → C1.1), **Azure Communication Services** özeti
- **Sınav tuzakları:** "hazır bilişsel API" → AI services ↔ "özel model eğitimi" → Azure Machine Learning; IoT Hub ↔ Event Hubs (cihaz yönetimi/çift yönlü ↔ salt yüksek hacimli alım)
- 🛠 **Pratik:** `az cognitiveservices account create` ile (free tier F0) bir Language/Vision kaynağı ve REST ile tek çağrı; ya da `az iot hub create --sku F1` + simüle cihaz *(F1 abonelik başına 1 tane — doğrula)*; temizlik. Anahtarı koda gömme uyarısı (→ C1.18).
- 📝 **Quiz:** senaryo → hangi AI/IoT servisi, sorumlu AI ilkeleri eşleştirme (≈12 soru)

#### C1.21 Entegrasyon, Mesajlaşma ve DevOps Araçları · `21-integration-devops-tools` *(bağlam)*
- 📌 **Bağlantı:** C1.6'daki Functions/Logic Apps sorularının devamı; **C2.8–C2.10** (Functions, Service Bus, Event Grid) ve M12 (mesajlaşma) için zemin; *Describe management and deployment tools* ile (→ C1.13) birlikte okunur
- **Teori — entegrasyon:** **Azure Logic Apps** (no/low-code workflow, 1000+ connector; Functions ↔ Logic Apps) · **Event Grid** (reaktif olay yönlendirme, push) · **Service Bus** (kurumsal mesajlaşma: kuyruk/topic, dead-letter, sıralama) · **Event Hubs** (yüksek hacimli olay/telemetri alımı, Kafka uyumluluğu) · **Storage Queue** (basit/ucuz kuyruk) · **API Management** (API gateway: kota, auth, dönüştürme, geliştirici portalı) · seçim tablosu: olay ↔ mesaj ⭐ (→ C2.9, C2.10, M12)
- **Teori — DevOps:** **Azure DevOps** (Boards, Repos, Pipelines, Test Plans, Artifacts) ↔ **GitHub** (Actions, Codespaces, Copilot) · **Azure Pipelines/GitHub Actions** ile CI/CD akışı, **Azure DevTest Labs** (geliştirme/test ortamlarında maliyet kontrolü), **Azure Deployment Environments**, **Azure Developer CLI (azd)** *(bağlam)*; IaC ile ilişki (→ C1.13), kalıcı kimlik bilgisi yerine **workload identity federation (OIDC)** *(bağlam)*
- **Sınav tuzakları:** Event Grid ↔ Service Bus ↔ Event Hubs ayrımı; Logic Apps (tasarımcı, connector'lar) ↔ Functions (kod); API Management'ın rolü
- 🛠 **Pratik:** `az servicebus namespace create` + queue + CLI ile mesaj gönderme/alma (Basic/Standard ucuz; temizlik); bir GitHub Actions workflow YAML'ını README'de oku/yaz (çalıştırma yok); karar tablosu alıştırması.
- 📝 **Quiz:** entegrasyon servisi seçimi, olay ↔ mesaj ayrımı, DevOps araç eşleştirme (≈14 soru)

#### C1.22 Destek Planları, SLA'lar, Servis Yaşam Döngüsü ve Benimseme Çerçeveleri · `22-support-sla-lifecycle` *(bağlam)* ⭐
- 📌 **Bağlantı:** *Describe Azure management and governance* alanının eski sürümlerindeki ve kurslardaki "Azure support, SLA and service lifecycle" bölümü; C1.2 (SLA hesapları) ve C1.12 (uyumluluk) ile birlikte okunur
- **Teori — destek planları ⭐:** **Basic** (ücretsiz: faturalama/abonelik desteği, self-help, Service Health) · **Developer** · **Standard** · **Professional Direct** · **Premier/Unified** (güncel adları ve fiyat/yanıt sürelerini doğrula); teknik destek, 7/24, yanıt süresi (Sev A/B/C), TAM; **support request** açma (portal, abonelik türüne göre), **Microsoft Q&A / Learn / Docs / Azure Updates / Azure Status** gibi self-service kaynakları
- **Teori — SLA'lar ⭐:** SLA nasıl okunur (hizmet kredisi, aylık % → dakika; → C1.2), **composite SLA**, SLA'yı yükseltme (availability zone, çok region), **SLA yok/free/preview** hizmetler; **Azure SLA sayfası**
- **Teori — servis yaşam döngüsü ⭐:** **Private preview → Public preview → General Availability (GA)**; preview'da SLA ve destek **garantisi yoktur** (sözleşmesel destek kapsamı farklı); **Azure Updates** ile değişiklik/emeklilik takibi (retirement duyuruları, → C1.2 reliability), **özellik önizleme (Preview features)**; **Azure Marketplace** ve **Azure Government/China** kısa notları (→ C1.4, C1.12)
- **Teori — benimseme çerçeveleri:** **Microsoft Cloud Adoption Framework (CAF)** — strategy, plan, ready, adopt (migrate/innovate), govern, manage, secure; **Azure Well-Architected Framework** — beş sütun: **reliability, security, cost optimization, operational excellence, performance efficiency** (Advisor kategorileriyle eşleşme → C1.14); **Azure landing zone** kavramı *(bağlam)*; **Azure Architecture Center**
- **Sınav tuzakları:** "preview hizmetinin SLA'sı" → yok; "ücretsiz destek planı hangisi" → Basic; WAF (framework) ↔ WAF (web application firewall) kısaltma tuzağı; Advisor'ın beş kategorisi = Well-Architected sütunları
- 🛠 **Pratik:** destek planlarını/SLA'ları karşılaştıran bir karar tablosu alıştırması; `az monitor activity-log list` ile bir Service Health olayını bulma; Azure SLA belgesinden iki servisin composite SLA'sını hesaplama (C1.2'nin `serial` demosu ile).
- 📝 **Quiz:** destek planı seçimi, preview/GA, SLA yorumlama, CAF/WAF eşleştirme (≈14 soru)

📝 **Sınav:** "Shared responsibility'de hangi sorumluluk her zaman müşteride?" · "CapEx ile OpEx; consumption-based model?" · "Scale up / scale out / elasticity?" · "HA ile reliability farkı?" · "%99,95 SLA aylık kaç dakika kesinti?" · "Ürün → IaaS/PaaS/SaaS eşleştirme; serverless nitelikleri?" · "Availability zone ile region pair?" · "Resource group silinince; bir kaynak iki grupta olabilir mi?" · "Politikayı tüm subscription'lara uygula?" · "Availability set ile scale set ile availability zone?" · "Proximity placement group ne sağlar?" · "Tek container'ı orkestrasyonsuz çalıştır?" · "VPN Gateway ile ExpressRoute; peering transitive mi?" · "LRS/ZRS/GRS/GZRS/RA-GRS hangi arızaya dayanır?" · "Archive tier'dan okuma?" · "File Sync, AzCopy, Storage Explorer farkı?" · "Data Box ne zaman; Azure Migrate ne yapar?" · "Entra ID ile Entra Domain Services; B2B ile B2C?" · "Conditional Access ne yapar; MFA faktörleri?" · "Owner, Contributor, Reader farkı?" · "Zero Trust'ın üç ilkesi; defense in depth katman sırası?" · "Defender for Cloud ne yapar?" · "Pricing Calculator ile TCO?" · "Tag'ler miras alınır mı?" · "RBAC ile Azure Policy; Policy etkileri?" · "Resource lock Owner'ı da durdurur mu?" · "Purview ne yapar?" · "Azure Arc ne yapar; ARM template neden?" · "Advisor, Service Health, Resource Health farkı?" · "Workbooks ne için?"

---

### C2. Azure AI Cloud Developer Associate (AI-200)

> 📁 `06-certifications/02-ai-200-azure-ai-cloud-developer/` · ⏱ ~8 hafta · Önkoşul: C1, M6 (PostgreSQL), M8 (NoSQL & Redis), M9 (kimlik), M12 (mesajlaşma), M13 (observability), M14 (container/Kubernetes), M15.4 (embeddings & vector search)
> 📜 **Sertifika:** **AI-200 — Developing AI Cloud Solutions on Azure** → *Microsoft Certified: Azure AI Cloud Developer Associate*. **AZ-204 31 Temmuz 2026'da emekliye ayrıldı**; .NET/Python geliştiricisinin Azure associate yolu artık bu sınav. Müfredat: resmî study guide, **"skills measured as of October 7, 2026"** — bu sürümde veri servisleri alanının ağırlığı **%25-30 → %30-35**'e çıktı.
>
> | Sınav alanı | Ağırlık | Konular |
> |---|---|---|
> | Develop containerized solutions on Azure | %20-25 | C2.1 – C2.3 |
> | Develop AI solutions by using Azure data management services | **%30-35** | C2.4 – C2.7 |
> | Connect to and consume Azure services | %20-25 | C2.8 – C2.10 |
> | Secure, monitor, and troubleshoot Azure solutions | %20-25 | C2.11 – C2.13 |
>
> **Uygulama notu:** Repoda NuGet paketi/Docker/ek proje olmadığı için C2 laboratuvarları (`🛠 Kod` satırları) derlenen kod değil, README içindeki **`az` CLI, C# ve Python kod bloklarıdır**; her konu `README.md` + `quiz.md` içerir.
> **Dil notu:** Study guide'ın hedef kitle tanımı Azure SDK'ları, veri servisleri, izleme, mesajlaşma, **vector veritabanları** ve **Python** bilgisi bekler. Bu repoda laboratuvarlar **C#/.NET** ile yazılır (aynı Azure SDK kavramları), her konunun README'sinde sınavdaki kod tamamlama sorularına hazırlık için **eşdeğer Python parçası** da verilir.
> **Kapsam notu:** AZ-204'te olan ama AI-200 hedeflerinde **görünmeyen** konular (App Service ayrıntıları, Blob lifecycle, API Management, Event Hubs, Microsoft Graph) bu modülde yalnızca *(bağlam)* olarak geçer; derinliği → S6 (gateway), S8.3 (mimari). Associate sınavlarında **senaryo/vaka** ve **kod/CLI/KQL tamamlama** soruları vardır; quiz'ler bu biçimleri içerir. AI-200 için resmî practice assessment henüz yayımlanmamış olabilir — deneme sınavı (C2.14) bu boşluğu doldurur.
> Gerçek sınav soruları NDA kapsamındadır — braindump kullanılmaz; sorular özgün yazılır, her cevabın gerekçesi vardır. Her laboratuvar emülatörle (kökteki `docker-compose.yml`: Azurite, Cosmos DB emulator, Service Bus emulator, PostgreSQL + pgvector, Redis) veya en ucuz katmanla çalışır ve `az group delete` ile biter.

#### C2.1 Azure Container Registry ve ACR Tasks · `01-container-registry` ⭐
- 📌 **Resmî hedef:** *Develop containerized solutions* — Azure Container Registry ile container image'larını **oluştur, sakla, sürümle ve yönet** · **ACR Tasks** ile image oluştur ve çalıştır
- **Teori:**
  - Registry, repository, tag, **digest**; SKU'lar (Basic / Standard / **Premium**: geo-replication, private endpoint, content trust, daha yüksek limit)
  - **Tag stratejisi:** semver (`:1.4.2`), commit SHA (`:sha-abc123`), neden `:latest` üretimde kullanılmaz; **digest ile sabitleme** (immutable deploy); `az acr repository show-tags`, `az acr manifest`
  - **ACR Tasks:** `az acr build` (yerel Docker gerekmeden bulutta build), `az acr run` (quick run), **multi-step task** (YAML), **otomatik tetikleyiciler**: kaynak kod commit'i, **base image güncellemesi** (güvenlik yaması zinciri), zamanlayıcı
  - Kimlik: admin user yerine **Entra kimliği / managed identity + `AcrPull` / `AcrPush` rolleri** ⭐; `az acr login`
  - Bakım: retention policy, untagged manifest temizliği, `az acr repository delete`, maliyet
- **Sınav tuzakları:** özellik → SKU eşleştirme; `az acr build` ile `docker build`; digest ile tag; image çekemeyen servis için doğru rol (`AcrPull`)
- 🛠 **Kod:** Todo API image'ını **`az acr build`** ile ACR'da derleme (yerel Docker'sız); semver + SHA tag'leri; digest'i not etme; base image güncellemesinde otomatik yeniden build eden **ACR Task**; untagged image temizliği. *(Python eşdeğeri: `azure-containerregistry` SDK ile tag listeleme.)*
- 📝 **Quiz:** SKU/özellik, tag/digest, ACR Tasks tetikleyicileri, CLI tamamlama (≈15 soru)

#### C2.2 Azure Container Apps: Ortam, Revision ve KEDA · `02-container-apps` ⭐
- 📌 **Resmî hedef:** *Implement container-orchestrated solutions* — uygulamaları **Azure Container Apps**'e dağıt (ortam yapılandırması ve **revision yönetimi** dahil) · Container Apps'te **KEDA** ile olay güdümlü ölçekleme
- **Teori:**
  - **Container Apps environment:** ortak ağ/log sınırı, Log Analytics bağlantısı, workload profilleri (consumption / dedicated)
  - Uygulama yapılandırması: **ingress** (dış/iç, hedef port), environment variable ve **secret**'lar (Key Vault referansı → C2.11), registry kimliği (managed identity ile ACR'dan çekme)
  - **Revision yönetimi** ⭐: revision = değişmez anlık görüntü; **single vs multiple revision mode**; **trafik bölme** (canary %10 → %100), revision label'ları, geri alma
  - **KEDA ile ölçekleme** ⭐: scale rule'lar (HTTP eşzamanlılık, **Service Bus kuyruk uzunluğu**, CPU/bellek, özel scaler), min/max replica, **scale-to-zero**, kuralın kimlik doğrulaması (secret / managed identity)
  - *(bağlam)* Container Apps Jobs (zamanlanmış/olay tetiklemeli işler), Dapr; App Service for Containers ile karşılaştırma
- **Sınav tuzakları:** single vs multiple revision mode; yeni revision ne zaman oluşur (revision-scope değişiklik) — ne zaman oluşmaz (secret/app-scope); trafik ağırlığı toplamı; KEDA kuralının hangi metriğe bağlanacağı; scale-to-zero'nun soğuk başlangıç etkisi
- 🛠 **Kod:** ACR'daki image'ı **digest ile** Container Apps'e dağıtma; yeni revision'a **%10 trafik** verip metriklere bakarak %100'e çıkarma ve geri alma; **Service Bus kuyruk uzunluğuna göre** ölçeklenen worker (KEDA scale rule) — kuyruğa 500 mesaj atıp replica sayısının artıp sıfıra indiğini gözlemleme.
- 📝 **Quiz:** revision/trafik senaryoları, KEDA kural seçimi, `az containerapp` komut tamamlama (≈18 soru)

#### C2.3 AKS'e Manifest ile Dağıtım ve Container Teşhisi · `03-aks-manifests`
- 📌 **Resmî hedef:** *Implement container-orchestrated solutions* — uygulamaları manifest dosyaları kullanarak **Azure Kubernetes Service**'e dağıt ve yönet
- **Teori:**
  - Manifest'ler: **Deployment** (replica, rolling update), **Service** (ClusterIP / LoadBalancer), **Ingress**, **ConfigMap**, **Secret**, resource **request/limit**, liveness/readiness probe (Kubernetes temelleri → M14.3–M14.4)
  - AKS ↔ ACR entegrasyonu (`az aks update --attach-acr`), `az aks get-credentials`, `kubectl apply / rollout status / rollout undo / scale`
  - **Teşhis** ⭐: `kubectl get pods / describe / logs (--previous) / get events / exec`; hata kalıpları: `ImagePullBackOff` (kimlik/tag), `CrashLoopBackOff` (uygulama/env/port), `OOMKilled` (limit), readiness başarısız (yanlış port/yol); Container Apps'te log stream ve `ContainerAppConsoleLogs_CL`
  - Container Apps vs AKS vs App Service for Containers — karar tablosu
- **Sınav tuzakları:** verilen manifest'te eksik/yanlış alanı bulma (selector-label uyuşmazlığı, port); hata mesajından kök nedene gitme; rollout geri alma komutu
- 🛠 **Kod:** Aynı API için Deployment + Service + ConfigMap + Secret manifest'leri; `kubectl apply` ile AKS'e (veya yerel kind/k3d kümesine) dağıtım; ardından **bilerek bozuk** iki dağıtım (yanlış image tag → `ImagePullBackOff`, yanlış env var/port → `CrashLoopBackOff` / readiness hatası) ve `describe/logs/events` ile teşhis; `rollout undo`.
- 📝 **Quiz:** manifest okuma/tamamlama, `kubectl` komut seçimi, hata → kök neden eşleştirme (≈15 soru)

#### C2.4 Cosmos DB for NoSQL: SDK, Partitioning, Indexing, Consistency, RU · `04-cosmos-db-nosql` ⭐
- 📌 **Resmî hedef:** *Develop AI solutions by using Azure data management services* — Cosmos DB for NoSQL'e SDK ile bağlan ve sorgula · **indexing policy** ve **consistency level** yapılandır · **RU** maliyetini optimize et
- **Teori:**
  - Kaynak modeli: account → database → container → item; **partition key** ⭐ (cardinality, eşit dağılım, sorgu desenine uyum; **container oluşturulduktan sonra değiştirilemez**), hot partition, hierarchical partition key, synthetic key
  - **SDK** (`Microsoft.Azure.Cosmos` / Python `azure-cosmos`): `CosmosClient` **singleton**, direct vs gateway mod, point read (`ReadItemAsync` — en ucuz işlem: 1 KB ≈ 1 RU), sorgu (`GetItemQueryIterator`), **bulk** ve **transactional batch** (aynı partition), `RequestCharge` ölçümü, 429 ve retry
  - **RU/s modelleri:** provisioned (manual / **autoscale**), **serverless**; database vs container düzeyi throughput
  - **Indexing policy** ⭐: varsayılan "her şeyi indeksle"; `includedPaths`/`excludedPaths` ile daraltma, **composite index** (ORDER BY birden çok alan), range/spatial; yazma RU'suna etkisi
  - **Consistency level'lar** ⭐: Strong, Bounded Staleness, **Session (varsayılan)**, Consistent Prefix, Eventual — garanti ↔ gecikme ↔ **okuma RU'su** (Strong/Bounded okumaları ~2 kat RU); istek bazında gevşetme
  - RU düşürme reçetesi: point read > sorgu, cross-partition sorgudan kaçın, projection, indexing policy daraltma, uygun consistency
- **Sınav tuzakları:** partition key seçimi senaryoları; hangi consistency "kendi yazdığını oku" garantisi verir (Session); RU artışının nedeni; composite index ne zaman gerekir; `CosmosClient`'ı her istekte yaratmak
- 🛠 **Kod:** **Cosmos DB emulator** ile sipariş verisini iki farklı partition key ile modelleyip aynı sorguların **RU maliyetlerini `RequestCharge` ile ölçme**; point read vs sorgu; indexing policy'yi daraltıp yazma/okuma RU farkını kanıtlama; composite index ile ORDER BY. *(Python eşdeğeri: `azure-cosmos` ile `read_item` / `query_items`.)*
- 📝 **Quiz:** partition key, consistency, indexing, RU optimizasyonu, SDK kod tamamlama (≈20 soru)

#### C2.5 Cosmos DB ile Vector Search ve Change Feed · `05-cosmos-vector-change-feed` ⭐
- 📌 **Resmî hedef:** Cosmos DB'de **embedding'leri sakla ve vector indexing policy yapılandır**, vector benzerlik araması yap · **change feed processor** ile yeni/güncellenen item'ları işle
- **Teori:**
  - **Vector embedding policy:** yol, veri tipi, **boyut (dimensions)**, **distance function** (cosine, dot product, euclidean)
  - **Vector index türleri:** `flat`, `quantizedFlat`, **`diskANN`** — doğruluk/maliyet/ölçek dengesi; vector yolunu normal indexing'den hariç tutmak
  - Sorgu: `SELECT TOP k ... ORDER BY VectorDistance(c.embedding, @q)`; **metadata filtresiyle** birlikte arama; *(bağlam)* full-text ve **hybrid search** (RRF) (→ M15.4)
  - **Change feed** ⭐: item'ların değişim sırasıyla okunması; **change feed processor** (lease container, checkpoint, ölçekleme, yeniden başlatmada kaldığı yerden devam); Azure Functions Cosmos DB trigger'ı; silmelerin varsayılan modda görünmemesi
  - Kanonik desen: içerik değişince **embedding'i yeniden üretip** vector alanını güncellemek (RAG verisini taze tutmak)
- **Sınav tuzakları:** vector policy alanları (boyut uyuşmazlığı), index türü seçimi, change feed'in silmeleri yakalamaması, lease container'ın amacı
- 🛠 **Kod:** Ürün açıklamalarının embedding'lerini Cosmos'ta saklayıp `VectorDistance` ile top-k arama (kategori filtresiyle); **change feed processor** worker'ı: ürün açıklaması değişince embedding'i yeniden hesaplayıp item'ı güncelleme (embedding için yerel/sahte üretici → gerçek model isteğe bağlı, → M15.4).
- 📝 **Quiz:** vector policy/index seçimi, VectorDistance sorgu tamamlama, change feed senaryoları (≈15 soru)

#### C2.6 Azure Database for PostgreSQL ve pgvector · `06-postgresql-pgvector` ⭐
- 📌 **Resmî hedef:** PostgreSQL'de **vector iş yükü**: şema ve index tasarımı, vector benzerlik araması, **metadata filtreli RAG** sorguları, bağlantı optimizasyonu
- **Teori:**
  - Azure Database for PostgreSQL **Flexible Server**; `CREATE EXTENSION vector` (izin listesi/`azure.extensions`); Entra ile şifresiz kimlik doğrulama
  - Şema: `vector(n)` kolonu, içerik + metadata kolonları; distance operatörleri: `<->` (L2), `<=>` (cosine), `<#>` (inner product)
  - **Index:** **HNSW** (`m`, `ef_construction`; sorguda `hnsw.ef_search`) vs **IVFFlat** (`lists`, sorguda `ivfflat.probes`; veri yüklendikten sonra oluşturulmalı) — recall/gecikme/oluşturma süresi/bellek dengesi; operator class'ın sorgudaki distance ile eşleşmesi
  - **Maliyet ve boyut:** `halfvec`/quantization, boyut indirgeme, index'in belleğe sığması; sığmazsa gecikme patlaması; compute/storage/IOPS boyutlandırma
  - **RAG + metadata filtresi:** `WHERE tenant_id = … ORDER BY embedding <=> @q LIMIT k`; filtrenin recall'a etkisi (iterative scan)
  - **Bağlantı optimizasyonu:** connection pooling (Npgsql pool / **PgBouncer**), kısa ömürlü bağlantılardan kaçınma, prepared statement
- **Sınav tuzakları:** HNSW vs IVFFlat seçimi; yanlış operator class → index kullanılmaması; IVFFlat'i boş tabloda oluşturmak; bağlantı tükenmesi senaryosu
- 🛠 **Kod:** Docker'da PostgreSQL + pgvector ile doküman tablosu; aynı veri setinde **HNSW ve IVFFlat** index'leriyle `EXPLAIN ANALYZE` ve recall/gecikme karşılaştırması; tenant filtreli RAG sorgusu; Npgsql connection pool ayarları. *(Python eşdeğeri: `psycopg` + `pgvector` paketi.)*
- 📝 **Quiz:** index seçimi/parametreler, SQL tamamlama, RAG filtre senaryoları (≈15 soru)

#### C2.7 Azure Managed Redis: Cache ve Vector Index · `07-managed-redis`
- 📌 **Resmî hedef:** Azure Managed Redis ile **cache** (expiration ve invalidation dahil) ve **vector indexing** ile benzerlik araması
- **Teori:**
  - Azure Cache for Redis → **Azure Managed Redis** geçişi; tier'lar ve modül desteği (RediSearch/vector için uygun tier ve oluşturma sırasında modül seçimi — güncel dokümana bak), clustering, eviction policy
  - **Cache-aside** deseni, **TTL/expiration**, **invalidation** stratejileri (yazınca sil, versiyonlu anahtar, olay tabanlı), stampede önleme (→ M8.4)
  - **Vector index:** `FT.CREATE ... VECTOR (HNSW|FLAT)`, KNN sorgusu; **semantic cache** (benzer soruya önceki LLM cevabını döndürme); ne zaman Redis'te vector tutmak mantıklı (düşük gecikme, kısa ömürlü/önbellek index) — kalıcı kaynak Cosmos/PostgreSQL
  - .NET: `StackExchange.Redis` / `IDistributedCache` / HybridCache; Entra ile kimlik doğrulama
- **Sınav tuzakları:** TTL ile eviction farkı; invalidation ne zaman gerekir; vector için hangi tier/modül; Redis'in kalıcı veri deposu olarak yanlış kullanımı
- 🛠 **Kod:** Docker'da Redis (Redis Stack/8) ile ürün API'sine **cache-aside + TTL + yazınca invalidation**; aynı embedding setiyle Redis vector index ve KNN arama; **semantic cache** demosu (eşik benzerlikte önbellekten dönme). *(Python eşdeğeri: `redis-py`.)*
- 📝 **Quiz:** cache desenleri, TTL/invalidation, vector index komut tamamlama, depo seçimi (Cosmos / pgvector / Redis) (≈15 soru)

#### C2.8 Azure Functions: Serverless API, Trigger ve Binding · `08-azure-functions` ⭐
- 📌 **Resmî hedef:** *Connect to and consume Azure services* — trigger ve binding'lerle **serverless API** oluştur · function app'leri **yapılandır ve dağıt**
- **Teori:**
  - **Isolated worker model** (.NET), Python v2 programlama modeli; HTTP trigger ile API (route, auth level, `HttpRequestData`/ASP.NET Core entegrasyonu)
  - **Trigger vs input/output binding** ⭐: HTTP, Timer, **Service Bus**, **Event Grid**, **Cosmos DB** (change feed), Blob, Queue; binding ifadeleri, `host.json` ve `local.settings.json`
  - **Hosting planları:** **Flex Consumption** (hızlı ölçek, VNet, scale-to-zero), Consumption, Premium (önceden ısıtılmış, VNet), Dedicated (App Service planı) — soğuk başlangıç, zaman aşımı, ölçek sınırları
  - **Yapılandırma:** app settings, connection'ların **managed identity** ile tanımlanması (`__fullyQualifiedNamespace` gibi kimlik tabanlı bağlantı), Key Vault referansı; **dağıtım:** `func azure functionapp publish`, zip deploy, GitHub Actions, deployment slot
  - *(bağlam)* Durable Functions kalıpları (fan-out/fan-in, human interaction) — sınav hedeflerinde açıkça yok, mimari bilgisi olarak
- **Sınav tuzakları:** trigger ile binding; plan seçimi (VNet + scale-to-zero → Flex Consumption); kimlik tabanlı bağlantı ayar adları; `function.json`/attribute tamamlama
- 🛠 **Kod:** HTTP trigger'lı küçük **serverless API** (isolated worker) + Cosmos DB output binding; Service Bus trigger'lı işleyici (C2.9 ile birlikte); yerelde **Azure Functions Core Tools** + Azurite/emülatörlerle çalıştırma; Flex Consumption'a dağıtım ve managed identity ile bağlantı. *(Python eşdeğeri: v2 model decorator'ları.)*
- 📝 **Quiz:** trigger/binding seçimi, plan seçimi, yapılandırma ve kod tamamlama (≈15 soru)

#### C2.9 Azure Service Bus: Kuyruk, Topic ve Dead-letter · `09-service-bus`
- 📌 **Resmî hedef:** Azure Service Bus ile **arka plan işlemlerini kuyrukla ve işle** — mesajlar, **topic ve subscription**'lar, **dead-letter queue** yönetimi
- **Teori:**
  - Namespace, **queue**, **topic + subscription** (filtre: SQL/correlation), mesaj özellikleri (`MessageId`, `SessionId`, `TimeToLive`, application properties)
  - Alma modları: **PeekLock** (complete / abandon / dead-letter / defer) vs ReceiveAndDelete; **lock süresi** ve yenileme; `ServiceBusProcessor`
  - **Dead-letter queue** ⭐: nedenleri (**MaxDeliveryCount aşımı**, **TTL dolması** (DLQ etkinse), filtre değerlendirme hatası, uygulamanın açıkça dead-letter etmesi), DLQ'yu okuma/yeniden işleme
  - Session'lar (sıralı işleme), duplicate detection, scheduled message, tier'lar (Basic'te topic yok); managed identity ile bağlantı (→ M12.3 mekanik)
- **Sınav tuzakları:** queue vs topic; PeekLock vs ReceiveAndDelete; mesaj neden DLQ'ya düştü; Basic tier kısıtları; sıralama için session
- 🛠 **Kod:** **Service Bus emulator** ile sipariş kuyruğu + `orders` topic'i ve iki subscription (filtreli); bilerek hata fırlatan işleyiciyle **MaxDeliveryCount** sonrası mesajın DLQ'ya düşmesini gözlemleme ve DLQ'dan yeniden işleme; TTL'li mesaj. *(Python eşdeğeri: `azure-servicebus`.)*
- 📝 **Quiz:** queue/topic seçimi, alma modları, DLQ senaryoları, SDK kod tamamlama (≈15 soru)

#### C2.10 Azure Event Grid: Custom Event, Filtre ve Retry · `10-event-grid`
- 📌 **Resmî hedef:** Azure Event Grid ile **olay güdümlü iş akışları** — **filtreler**, **custom event**'ler ve **retry**'lar
- **Teori:**
  - Kavramlar: event source, **topic** (system / custom / partner), **event subscription**, event handler (webhook, Function, Service Bus, Storage Queue…); **CloudEvents** ve Event Grid şeması; namespace topic'leri (push/pull)
  - **Custom event** yayınlama: `EventGridPublisherClient` (SAS / Entra kimliği)
  - **Filtreleme** ⭐: event type, **subject begins/ends with**, **advanced filter** (payload alanı; `NumberGreaterThan`, `StringIn`…)
  - **Teslim ve dayanıklılık:** **retry policy** (maks. deneme, event TTL), exponential backoff, **dead-letter** hedefi (storage blob), **at-least-once** teslim → idempotent handler; webhook validation handshake
  - Event Grid vs Service Bus: olay (oldu bilgisi) vs mesaj (iş komutu) — seçim tablosu
- **Sınav tuzakları:** filtre türü seçimi; retry/TTL/dead-letter yapılandırması; Event Grid'i kuyruk gibi kullanmak; system topic vs custom topic
- 🛠 **Kod:** Uygulamadan **custom event** yayınlayıp **advanced filter** ile yalnızca `amount > 1000` olan siparişleri bir Function handler'a yönlendirme; handler'ı bilerek hata döndürecek şekilde yazıp **retry**'ları ve sonunda dead-letter blob'una düşüşü gözlemleme. *(Python eşdeğeri: `azure-eventgrid`.)*
- 📝 **Quiz:** topic/subscription türleri, filtre senaryoları, retry/dead-letter, Event Grid vs Service Bus (≈15 soru)

#### C2.11 Managed Identity ve Azure Key Vault · `11-identity-key-vault` ⭐
- 📌 **Resmî hedef:** *Secure, monitor, and troubleshoot* — **Azure Key Vault ile secret'ları güvenceye al** (**rotation** ve **retrieval** dahil)
- **Teori:**
  - **Managed identity** (secret'sız kimlik — tüm laboratuvarların temeli): system-assigned vs user-assigned; `DefaultAzureCredential` / `ManagedIdentityCredential` (`azure-identity`); üretimde belirli credential seçmek
  - Key Vault nesneleri: **secret**, **key**, **certificate**; sürümler; **RBAC izin modeli** (önerilen) vs access policy; data plane rolleri: *Key Vault Secrets User* / *Secrets Officer* (Contributor sırları **okutmaz**)
  - **Retrieval:** `SecretClient.GetSecretAsync`, `IConfiguration` entegrasyonu (`AddAzureKeyVault`), App Service/Functions/Container Apps **Key Vault referansları**; önbellekleme ve yenileme
  - **Rotation** ⭐: secret sürümleme, **rotation policy** (key'ler için otomatik), **Event Grid ile "near expiry" olayı** → Function ile döndürme; uygulamanın yeni sürümü alması
  - Soft delete, **purge protection**, ağ kısıtları (private endpoint)
- **Sınav tuzakları:** doğru rol seçimi (en az yetki); system vs user-assigned; rotation olayı hangi servisle tetiklenir; soft delete ile purge protection
- 🛠 **Kod:** API'nin Key Vault'tan secret'ı **managed identity** ile okuması (yerelde geliştirici kimliğiyle aynı kod); **secret rotation**: yeni sürüm oluşturup uygulamanın yenilemeyle yeni değeri alması; "near expiry" olayını Event Grid → Function ile işleme. *(Python eşdeğeri: `azure-keyvault-secrets`.)*
- 📝 **Quiz:** kimlik türleri, RBAC rolleri, rotation/retrieval senaryoları, kod tamamlama (≈15 soru)

#### C2.12 Azure App Configuration · `12-app-configuration`
- 📌 **Resmî hedef:** **Azure App Configuration** ile uygulama yapılandırmasını sakla ve al
- **Teori:**
  - Anahtar-değer, **label** (ortam/sürüm), içerik türü, anahtar hiyerarşisi (`App:Section:Key`); **Key Vault referansları** (secret App Configuration'da değil Key Vault'ta durur)
  - .NET (`AddAzureAppConfiguration`) / Python sağlayıcısı; **dinamik yenileme**: **sentinel key**, yenileme aralığı, push (Event Grid) ile yenileme
  - **Feature flag**'ler (`Microsoft.FeatureManagement`): filtreler (yüzde, zaman penceresi, hedefleme), kademeli açma
  - Snapshot'lar, geçmiş/geri alma; managed identity ile erişim (*App Configuration Data Reader*)
- **Sınav tuzakları:** App Configuration vs Key Vault (ne nerede saklanır); sentinel key neden; label ile ortam ayırma; feature flag filtresi seçimi
- 🛠 **Kod:** API yapılandırmasını App Configuration'a taşıma (label'larla Dev/Prod), Key Vault referansı ile bağlantı dizesi; **sentinel key** ile yeniden başlatmadan yenileme; feature flag ile yeni özelliği %20 kullanıcıya açma.
- 📝 **Quiz:** depolama seçimi, yenileme mekanizması, feature flag senaryoları (≈12 soru)

#### C2.13 OpenTelemetry ile İzleme ve KQL ile Sorun Giderme · `13-opentelemetry-kql` ⭐
- 📌 **Resmî hedef:** **OpenTelemetry** SDK'larıyla dağıtık sistemleri izle (tracing) · log ve metrikleri analiz eden **KQL** sorguları yaz
- **Teori:**
  - OpenTelemetry sinyalleri: **trace** (span, context propagation, W3C traceparent), metrik, log; **Azure Monitor OpenTelemetry Distro** (`UseAzureMonitor()` / `azure-monitor-opentelemetry`), otomatik ve elle enstrümantasyon (`ActivitySource`), sampling (→ M13.3)
  - Application Insights tabloları: `requests`, `dependencies`, `exceptions`, `traces`, `customMetrics`; workspace tabanlı tablolar (`AppRequests`…); `operation_Id` ile uçtan uca trace
  - **KQL** ⭐: `where`, `project`, `extend`, `summarize count()/avg()/percentile() by bin(timestamp, 5m)`, `top`, `join`, `render timechart`; metrik ve log sorguları; Container Apps/AKS log tabloları
  - Uyarılar: log/metrik alert kuralı + action group; sorun giderme akışı: hata oranı → en yavaş dependency → ilgili trace
- **Sınav tuzakları:** verilen KQL'in sonucunu okuma; eksik operatörü tamamlama (`summarize ... by bin(...)`); trace/span/context kavramları; sampling etkisi
- 🛠 **Kod:** API + worker'ı OpenTelemetry ile enstrümante edip (Service Bus üzerinden **dağıtık trace**) Application Insights'a gönderme; **KQL sorgu seti**: en yavaş 5 endpoint (p95), 5 dakikalık hata oranı, belirli `operation_Id`'nin uçtan uca akışı; hata oranı alert'i. *(Python eşdeğeri: `azure-monitor-opentelemetry`.)*
- 📝 **Quiz:** OpenTelemetry kavramları, KQL okuma/tamamlama, sorun giderme senaryoları (≈18 soru)

#### C2.14 Deneme Sınavı ve Sınav Stratejisi · `14-practice-exam`
- **Tam uzunlukta deneme sınavı** (≈50 soru + 1-2 vaka çalışması) — alan ağırlıklarına göre (%20-25 / **%30-35** / %20-25 / %20-25)
- **Soru biçimleri:** tek/çoklu seçim, evet-hayır tablosu, sıralama, **kod/CLI/KQL tamamlama** (C# ve Python parçaları), senaryo ve vaka
- **Puanlama:** alan bazında puan; her yanlışı C2.x konusuna bağlayan tekrar planı
- **Sınav teknikleri:** "en az yönetim yükü / en düşük maliyet / en az yetki / secret'sız" anahtar kelimeleri; vaka çalışmasında önce gereksinimleri ve kısıtları listele; eleme; zaman yönetimi; Microsoft Learn'ün sınav içinde erişime açık olup olmadığını güncel kurallardan kontrol et
- 🛠 **Kod:** Deneme sınavını zamanlı çöz → yanlışları C2.x ile eşleştir → zayıf alan laboratuvarlarını tekrar et.
- 📝 **Quiz:** Deneme sınavı + cevap anahtarı, gerekçeler ve alan bazlı puan tablosu

📝 **Sınav:** "ACR'da image'ı yerel Docker'sız nasıl derlersin; base image güncellenince otomatik build?" · "Image çekmek için admin user yerine ne?" · "Container Apps'te canary: revision mode ve trafik bölme" · "Service Bus kuyruğuna göre KEDA ölçekleme" · "`ImagePullBackOff` / `CrashLoopBackOff` kök nedeni" · "Cosmos partition key seçimi" · "Session consistency ne garanti eder; Strong'un RU etkisi?" · "Indexing policy ile RU nasıl düşer?" · "Vector embedding policy ve index türü" · "Change feed processor'da lease container" · "pgvector HNSW mi IVFFlat mi?" · "Managed Redis'te TTL ve invalidation; semantic cache" · "Trigger ile binding; Flex Consumption ne zaman?" · "Mesaj neden DLQ'ya düştü?" · "Event Grid advanced filter, retry ve dead-letter" · "Key Vault secret rotation nasıl otomatikleşir; doğru rol hangisi?" · "App Configuration'da sentinel key neden?" · "KQL: son 1 saatte 5 dakikalık hata oranı"

---

## Repo Yapısı

Klasör hiyerarşisi roadmap'in birebir aynısıdır: **seviye → modül → konu**. Konu klasör adları her konunun başlığında (`· 01-relational-modeling` gibi) yazar; burada sadece modül seviyesi gösterilir.

```
dotnet-interview/
├── DotnetInterview.slnx            # the ONE solution: open this in Visual Studio (ConsoleApp + WebApp)
├── README.md
├── ROADMAP.md                      # this file
├── CLAUDE.md
├── docker-compose.yml              # (added with the first infra topic — J8): PostgreSQL, Redis, broker …
│
├── ConsoleApp/                     # the ONE console project: every console topic
│   ├── ConsoleApp.csproj
│   ├── Program.cs                  # topic menu: dotnet run -- J3.2 [section | all]
│   │
│   ├── 01-junior/
│   │   ├── 01-dev-foundations/
│   │   ├── 02-csharp-fundamentals/
│   │   ├── 03-collections-linq/
│   │   ├── 04-async-basics/
│   │   ├── 05-build-run-basics/
│   │   ├── 06-aspnetcore-fundamentals/
│   │   ├── 07-web-api-basics/
│   │   ├── 08-database-fundamentals/
│   │   ├── 09-ef-core-basics/
│   │   ├── 10-testing-basics/
│   │   ├── 11-security-auth-basics/
│   │   ├── 12-docker-basics/
│   │   ├── 13-ai-basics/
│   │   └── capstone/
│   │
│   ├── 02-mid/
│   │   ├── 01-csharp-deep-dive/
│   │   ├── 02-build-runtime-deployment/
│   │   ├── 03-aspnetcore-deep-dive/
│   │   ├── 04-concurrency-async/
│   │   ├── 05-api-design/
│   │   ├── 06-sql-postgresql/
│   │   ├── 07-data-access-advanced/
│   │   ├── 08-nosql-caching/
│   │   ├── 09-identity-access/
│   │   ├── 10-testing-strategy/
│   │   ├── 11-architecture-design/
│   │   ├── 12-messaging/
│   │   ├── 13-observability/
│   │   ├── 14-devops/
│   │   ├── 15-ai-engineering/
│   │   └── capstone/
│   │
│   ├── 03-senior/
│   │   ├── 01-runtime-internals/
│   │   ├── 02-performance/
│   │   ├── 03-concurrency-advanced/
│   │   ├── 04-data-at-scale/
│   │   ├── 05-architecture-advanced/
│   │   ├── 06-distributed-systems/
│   │   ├── 07-security-engineering/
│   │   ├── 08-cloud-native-sre/
│   │   ├── 09-ai-systems/
│   │   ├── 10-system-design/
│   │   └── capstone/
│   │
│   ├── 04-dsa/
│   │   ├── 01-complexity/
│   │   ├── 02-linear-structures/
│   │   ├── 03-trees-heaps-tries/
│   │   ├── 04-graphs/
│   │   ├── 05-sorting-searching/
│   │   ├── 06-recursion-backtracking/
│   │   ├── 07-pattern-catalog/
│   │   ├── 08-pattern-matching-guide/
│   │   ├── 09-dynamic-programming/
│   │   ├── 10-advanced-structures/
│   │   └── 11-csharp-practice/
│   │
│   ├── 05-interview/
│   │   ├── 01-coding-live-coding/
│   │   ├── 02-behavioral/
│   │   ├── 03-system-design-interview/
│   │   ├── 04-take-home/
│   │   ├── 05-technical-leadership/
│   │   └── 06-mock-checklist/
│   │
│   └── 06-certifications/              # level-independent certification track
│       ├── 01-az-900-azure-fundamentals/      # AZ-900 Study Guide (kapsamlı MD dokümanı + 400+ quiz ve deneme sınavı)
│       └── 02-ai-200-azure-ai-cloud-developer/ # AI-200 Study Guide (kapsamlı MD dokümanı + 260 quiz ve deneme sınavı)
│
└── WebApp/                         # the ONE web project: topics that need an HTTP server (J1.3, J6, J7, M3, M5 …)
    ├── WebApp.csproj
    ├── Program.cs                  # menu; runs ONE web topic at a time
    └── 01-junior/ …                # same level/module/topic folder names as ConsoleApp
```

**Örnek — bir modülün açılmış hali:**

```
ConsoleApp/01-junior/08-database-fundamentals/
├── 01-relational-modeling/
│   ├── README.md
│   ├── <kod dosyaları>
│   └── interview-questions.md
├── 02-sql-basics/
├── 03-index-basics/
├── 04-transactions-acid/
├── 05-sql-vs-nosql/
└── 06-postgresql-getting-started/
```

Modülün konuları kökteki `docker-compose.yml`'deki servisleri (PostgreSQL + pgAdmin + MongoDB + Redis) kullanır; her konu `ConsoleApp/Program.cs`'e tek satırla eklenir.

**Kod konvansiyonları:**
- Tek console + tek web projesi; konu = klasör. Konsol: `cd ConsoleApp && dotnet run -- J8.2 [bölüm | all]`; web: `cd WebApp && dotnet run`
- Namespace `Junior.DatabaseFundamentals.TransactionsAcid` biçimindedir (konu başına ayrı namespace → aynı projede `Order`, `Customer` gibi demo tipleri çakışmaz)
- Konular birbirinin tipini kullanmaz; modülün ortak yardımcıları modül klasöründeki `Shared/` klasörüne konur
- Proje ayarları (`InvariantGlobalization`, `GenerateDocumentationFile`, `EnforceCodeStyleInBuild`) iki `.csproj`'a yazılır; yalnızca bir konuyu ilgilendiren analyzer ayarları o konu klasöründeki `.editorconfig`'e
- Altyapı kökteki `docker-compose.yml`'den gelir; connection string'ler `appsettings.Development.json` + User Secrets
- Capstone'lar katmanlı mimariyi göstermek için ek projeler içerebilir; hepsi aynı solution'a eklenir (tek istisna)
- **Sertifika pisti:** `ConsoleApp/06-certifications/` altında sertifika = klasör (`01-az-900-azure-fundamentals`, `02-ai-200-azure-ai-cloud-developer`); konu klasöründe `README.md` + (varsa) kod + **`quiz.md`** (sınav tarzı sorular, cevaplar `<details>` içinde); son konu = deneme sınavı. `interview-questions.md` yerine `quiz.md` kullanılır; testler yine yoktur.

---

## Çalışma Planı

Günde 2-3 saat varsayımıyla. Her satırda **seviye modülü + aynı haftalarda paralel DSA modülü** var. Zaten bir seviyedeysen modüllerin 🎯 sorularıyla kendini test et; rahat cevapladığın modülleri hızlı geç.

### 🟢 Junior (~29 hafta)

| Hafta | Seviye modülü | Paralel DSA |
|-------|---------------|-------------|
| 1-2 | J1 Geliştirici Temelleri (Git, **ağ temelleri**, HTTP, CLI) | D1 |
| 3-6 | **J2 C# Temelleri** (OOP ve sınıf üyeleri dahil) | D2 |
| 7-8 | J3 Collections, Delegates & LINQ | D2 |
| 9-10 | J4 Async + J5 Build & Run | D5 |
| 11-12 | J6 ASP.NET Core Temelleri | D5 |
| 13-15 | J7 Web API Temelleri | D3 (ağaç, heap) |
| 16-18 | **J8 Veritabanı Temelleri** | D3 |
| 19-20 | J9 EF Core | D11 — Easy tekrar |
| 21-22 | J10 Test (**ilk CI dahil**) + J12 Docker | D11 |
| 23-24 | J11 Güvenlik & Kimlik | D11 |
| 25-26 | J13 AI ile Çalışmaya Giriş | D11 |
| 27-29 | 🏁 **Junior Capstone** + I1, I2, I4 + I6 Junior mock | Zamanlı Easy setleri |

### 🟡 Mid (~43 hafta)

| Hafta | Seviye modülü | Paralel DSA |
|-------|---------------|-------------|
| 1-2 | M1 C# Derinlemesine | D4 |
| 3-4 | M2 Build, Runtime & Deployment | D4 |
| 5-7 | M3 ASP.NET Core Derinlemesine | D6 |
| 8-10 | **M4 Concurrency & Async** — en yüksek getirili modül | D6 |
| 11-14 | M5 API Tasarımı & Protokoller (**HTTP/2-3, transport** dahil) | D7 |
| 15-17 | **M6 SQL & PostgreSQL** | D7 |
| 18-19 | M7 EF Core Derinlemesine | D8 |
| 20-22 | **M8 NoSQL & Caching** (MongoDB, Redis) | D8 |
| 23-25 | **M9 Kimlik: OAuth, OIDC, Keycloak** | D9 (temel) |
| 26-27 | M10 Test Stratejisi | D9 |
| 28-30 | M11 Mimari & Tasarım | D9 |
| 31-32 | M12 Mesajlaşma (**RabbitMQ** derinlemesine) | D11 — Medium |
| 33-34 | M13 Observability | D11 |
| 35-37 | **M14 DevOps** (container ileri + Kubernetes) | D11 |
| 38-40 | M15 AI Engineering | D11 |
| 41-43 | 🏁 **Mid Capstone** + I3'e giriş + I6 Mid mock | Zamanlı Medium setleri |

### 🔴 Senior (~35 hafta)

| Hafta | Seviye modülü | Paralel DSA |
|-------|---------------|-------------|
| 1-3 | S1 CLR & Runtime | D9 — ileri |
| 4-6 | S2 Performans Mühendisliği | D9 |
| 7-8 | S3 Concurrency: Bellek Modeli & Dağıtık Koordinasyon | D10 |
| 9-12 | **S4 Ölçekte Veri** | D10 |
| 13-15 | S5 Mimari Derinlemesine | D10 |
| 16-18 | **S6 Dağıtık Sistemler** | D11 — Medium/Hard |
| 19-21 | S7 Güvenlik Mühendisliği | D11 |
| 22-24 | S8 Cloud-Native Platform & SRE | D11 |
| 25-28 | **S9 AI Sistemleri & Agent Mimarisi** | D11 |
| 29-31 | S10 Sistem Tasarımı + I3 tatbikatları | D10 tasarım soruları |
| 32-35 | 🏁 **Senior Capstone** + I5 + I6 Senior mock | — |

### 📜 Sertifika Pisti (~13 hafta, seviyelerle paralel)

Sertifika pisti seviye sırasının parçası değildir; ilgili modüller bittikten sonra **seviye çalışmasına paralel** (haftada 2-3 gün) ya da ardışık yürütülür. Her sertifika kendi **deneme sınavıyla** biter.

| Zamanlama | Modül | Not |
|-----------|-------|-----|
| Junior hafta ~21-29 (J12 sonrası) | **C1 AZ-900** (~6 hafta) | Junior capstone'dan önce veya hemen sonra; Azure'a ilk temas |
| Mid hafta ~35-43 ve sonrası (M9, M12, M13, M14, M15.4 sonrası) | **C2 AI-200** (~8 hafta) | Mid capstone öncesi/paralel; sınavdan önce M6/M8 (veri), M9 (kimlik), M12 (mesajlaşma) tekrarı |

> Sınava girme kararı sana aittir: C1/C2'yi **bilgi** olarak çalışıp sınava girmeyebilirsin; quiz ve deneme sınavı yine seviye ölçer. Sınavdan önce resmî study guide'ı ve **practice assessment**'ı kontrol et.

### ⚡ Hızlı yol — mülakat 4-6 hafta sonra ise

1. Kendi seviyenin ve bir alt seviyenin **⭐ işaretli** konularını tara — bunlar en sık sorulanlar
2. Bu modüllerin 🎯 sorularını **kapalı kitap** cevapla; takıldıklarını not al ve sadece onları çalış
3. D7 + D8 (pattern kataloğu ve eşleştirme) + günde 2 problem
4. I1, I2, I4 + seviyenin I6 mock senaryosu; Senior için ek olarak I3 ve I5
5. En güçlü projeni (capstone veya iş projesi) 2 dakikalık ve 10 dakikalık versiyonlarla anlatmaya hazırla

**Her hafta:** 1 gün tekrar. **Her ayın sonunda:** kapalı kitap self-mock mülakat.

---

## 2026 Radarı — Neler Değişti?

> .NET ve çevresindeki ekosistem 2025-2026'da hızlı değişti. Mülakatta güncel olduğunu gösteren bu başlıklar roadmap'e işlendi. Bu tabloyu **3 ayda bir** güncelle.

| Alan | Değişiklik | Roadmap'te |
|------|------------|------------|
| .NET sürümleri | **.NET 10 LTS** (Kasım 2025 → Kasım 2028) güncel üretim sürümü; **.NET 11 STS** Kasım 2026 (runtime async, C# 15 union tipleri, OpenAPI 3.2, Zstandard) | J1.4, M1.1, M4.3 |
| Destek politikası | STS desteği **24 aya** çıktı; **.NET 8 ve .NET 9 aynı gün, 10 Kasım 2026'da** destek dışı kalıyor → .NET 10'a geçiş gündemde | J1.4, S5.6 |
| C# 14 | Extension members, `field` keyword, null-conditional assignment, implicit span dönüşümleri | J2.6, M1.1 |
| SDK & araçlar | File-based apps (`dotnet run app.cs`), `.slnx`, `dotnet test`'te Microsoft.Testing.Platform, xUnit v3 | J1.4, J10.1 |
| ASP.NET Core 10 | Varsayılan OpenAPI 3.1, Minimal API yerleşik validation, Server-Sent Events, Identity'de passkey | J7, J11.1, M5.7 |
| EF Core 10 | Named query filters, complex type → JSON, SQL Server vector search, `LeftJoin`/`RightJoin` | M7, M15.4 |
| Lisans değişiklikleri | **MediatR** (v13+), **AutoMapper** (v15+), **MassTransit** (v9), **FluentAssertions** (v8) ticari lisansa geçti → alternatifleri bilmek gerekiyor | J10.1, M7.4, M11.5, M12.5 |
| Aspire | **Aspire 13:** ".NET" öneki kalktı, polyglot (Python, JS/TS AppHost), AI agent'lar için MCP desteği | M14.5 |
| .NET'te AI | **Microsoft Agent Framework 1.0** (Nisan 2026) — Semantic Kernel + AutoGen birleşimi; SK destekte, AutoGen bakım modunda | S9.1 |
| AI protokolleri | **MCP C# SDK** 1.x kararlı; **A2A** protokolü Linux Foundation altında | M15.6, S9.3 |
| Azure AI | Azure AI Foundry → **Microsoft Foundry** (Ocak 2026); Anthropic Claude modelleri katalogda; Foundry Agent Service'te hosted agent'lar | M15.7, S9.7 |
| Azure servisleri | Azure Cache for Redis → **Azure Managed Redis**; Functions **Flex Consumption**; Container Apps serverless GPU; Azure SQL `VECTOR` tipi GA | C2 |
| Sertifikalar | Giriş: **AZ-900** (3 alan: kavramlar %25-30, mimari & servisler %35-40, yönetişim %30-35). Geliştirici: **AZ-204 → AI-200** (AZ-204 31 Temmuz 2026'da emekli; study guide *as of October 7, 2026*: container %20-25, veri servisleri & vector **%30-35** (önceki sürümde %25-30), servis entegrasyonu %20-25, güvenlik/izleme %20-25; hedef kitle Python bilgisi bekler). AZ-900 study guide *as of July 20, 2026* (compute/ağ, yönetim araçları ve izleme bölümlerinde küçük değişiklik). Ayrıca **AI-102 → AI-103**; mimari için AZ-305 | C1, C2, M15.7, S8 |
| Veritabanları | **PostgreSQL 18** (async I/O, `uuidv7()`, virtual generated columns); **Redis 8** AGPLv3 ile yeniden açık kaynak + **Valkey** forku; MongoDB 8.x Community'de search/vector search; MongoDB EF Core provider'da vector search | J8.6, M6.4, M8 |
| Mesajlaşma | **RabbitMQ 4.x:** classic queue mirroring 4.0'da kaldırıldı (quorum queue dayanıklı varsayılan seçim), quorum queue'larda `delivery-limit` varsayılanı **20**; **Khepri** metadata store 4.2'den itibaren yeni kurulumlarda varsayılan ve Raft metrik adları değişti; 4.3'te transient classic queue'lar varsayılan olarak kapatıldı ve `delivery-count`/`acquired-count` ayrıldı | M12.2 |
| Azure (AZ-900 konuları) | Azure AD B2C 1 Mayıs 2025'ten beri yeni müşteriye kapalı → **Entra External ID**; Azure'da **zorunlu MFA** (portal fazı 2025 başı, CLI/PowerShell/IaC fazı Ekim 2025'ten itibaren kademeli); bağımsız **TCO Calculator** kaldırıldı (on-prem ↔ Azure karşılaştırması → **Azure Migrate business case**; resmî duyuru bulunamadı, ikincil kaynak); **Data Box Heavy** emekli, yeni nesil Data Box ~120/525 TB; Functions **Linux Consumption** 30 Eylül 2028'de emekli (→ Flex Consumption); klasik Application Insights emekli (workspace-based) | C1.6, C1.8, C1.9, C1.11, C1.14 |
| Kimlik | **OAuth 2.1 + RFC 9700**: her yerde PKCE, implicit/ROPC yok; DPoP ve PAR yaygınlaşıyor; **Keycloak 26**: passkeys, DPoP, FAPI 2.0, organizations; Azure AD B2C → Entra External ID | J11.3, M9 |
| Güvenlik | **OWASP Top 10:2025** — yeni: A03 Software Supply Chain Failures, A10 Mishandling of Exceptional Conditions; .NET 10'da post-quantum kriptografi | S7.1, S7.4 |

**Radar kaynakları:**
- [What's new in .NET 10](https://learn.microsoft.com/dotnet/core/whats-new/dotnet-10/overview) · [What's new in C# 14](https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-14) · [EF Core 10 yenilikleri](https://learn.microsoft.com/ef/core/what-is-new/ef-core-10.0/whatsnew)
- [.NET STS releases supported for 24 months](https://devblogs.microsoft.com/dotnet/dotnet-sts-releases-supported-for-24-months/) · [.NET 11 Preview 6 roundup](https://visualstudiomagazine.com/articles/2026/07/15/net-11-preview-6-roundup-aspnet-core-maui-c-ef-core-and-sdk-updates.aspx)
- [File-based apps](https://learn.microsoft.com/dotnet/core/sdk/file-based-apps) · [xUnit v3 + Microsoft Testing Platform](https://xunit.net/docs/getting-started/v3/microsoft-testing-platform)
- [Microsoft Agent Framework 1.0](https://visualstudiomagazine.com/articles/2026/04/06/microsoft-ships-production-ready-agent-framework-1-0-for-net-and-python.aspx) · [MCP C# SDK](https://csharp.sdk.modelcontextprotocol.io/)
- [Microsoft Foundry yeniden adlandırma](https://www.directionsonmicrosoft.com/reports/foundry-gets-new-name-anthropic-models/) · [What's new in Microsoft Foundry (Build 2026)](https://devblogs.microsoft.com/foundry/whats-new-in-microsoft-foundry-build-2026/)
- [Aspire 13 multi-language support](https://www.infoq.com/news/2025/11/dotnet-aspire-13-release/)
- **Resmî study guide'lar:** [AZ-900](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900) (20 Temmuz 2026) · [AI-200](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200) (7 Ekim 2026) · [Azure AI Cloud Developer Associate sertifika sayfası](https://learn.microsoft.com/en-us/credentials/certifications/azure-ai-cloud-developer-associate/) · [ücretsiz practice assessment'lar](https://learn.microsoft.com/en-us/credentials/certifications/practice-assessments-for-microsoft-certifications)
- [AZ-204 → AI-200 geçişi](https://examinotion.com/blog/az-204-retirement-ai-200-migration-guide) · [AI-103 study guide](https://tutorialsdojo.com/ai-103-azure-ai-app-and-agent-developer-associate-study-guide/)
- [MediatR & MassTransit lisans değişikliği](https://www.milanjovanovic.tech/blog/mediatr-and-masstransit-going-commerical-what-this-means-for-you)
- [PostgreSQL 18 yenilikleri](https://neon.com/postgresql/postgresql-18-new-features) · [Redis 8 AGPLv3](https://alternativeto.net/news/2025/5/redis-goes-open-source-again-with-the-launch-of-redis-8-under-the-agplv3-license) · [MongoDB EF Core provider: vector search](https://devblogs.microsoft.com/dotnet/mongodb-efcore-provider-queryable-encryption-vector-search/)
- [RabbitMQ 4.2.0 release notes](https://github.com/rabbitmq/rabbitmq-server/blob/main/release-notes/4.2.0.md) · [RabbitMQ 4.3 highlights](https://www.rabbitmq.com/blog/2026/04/23/rabbitmq-4.3-release) · [Khepri'nin varsayılan olma yol haritası](https://www.rabbitmq.com/blog/2025/09/01/6-khepri-default) · [Quorum queues dokümantasyonu](https://www.rabbitmq.com/docs/quorum-queues)
- [Keycloak 26.6 release](https://www.keycloak.org/2026/04/keycloak-2660-released) · [RFC 9700 — OAuth 2.0 Security BCP](https://datatracker.ietf.org/doc/rfc9700/)
- [OWASP Top 10:2025](https://owasp.org/Top10/2025/0x00_2025-Introduction/)
- AZ-900 konuları: [Azure AD B2C FAQ (yeni müşterilere kapanış)](https://learn.microsoft.com/azure/active-directory-b2c/faq) · [Mandatory MFA for Azure](https://learn.microsoft.com/entra/identity/authentication/concept-mandatory-multifactor-authentication) · [Data Box overview](https://learn.microsoft.com/azure/databox/data-box-overview) · [Data Box Heavy](https://learn.microsoft.com/azure/databox/data-box-heavy-overview) · [Azure Functions hosting options](https://learn.microsoft.com/azure/azure-functions/functions-scale) · [Classic Application Insights retirement](https://azure.microsoft.com/updates/we-re-retiring-classic-application-insights-on-29-february-2024) · [TCO Calculator → Azure Migrate business case (ikincil kaynak)](https://www.mindmeshacademy.com/certifications/azure/az-900-microsoft-azure-fundamentals/study-guide/3-2-2-estimating-cost-before-you-spend)

---

## Kaynaklar

### Kitaplar

| Kitap | Yazar | Seviye / Modül |
|-------|-------|----------------|
| C# in Depth | Jon Skeet | 🟡 M1 |
| **CLR via C#** | Jeffrey Richter | 🔴 S1 |
| Pro .NET Memory Management | Konrad Kokosa | 🔴 S1.3, S2 |
| Writing High-Performance .NET Code | Ben Watson | 🔴 S2 |
| Concurrency in C# Cookbook | Stephen Cleary | 🟡🔴 M4, S3 |
| Pro ASP.NET Core | Adam Freeman | 🟢🟡 J6, J7, M3 |
| **Computer Networking: A Top-Down Approach** | Kurose & Ross | 🟢🟡 J1.2, M5.11 |
| High Performance Browser Networking (ücretsiz) | Ilya Grigorik | 🟡 J1.2, J1.3, M5.11 |
| SQL Performance Explained / *Use The Index, Luke* | Markus Winand | 🟢🟡 J8.3, M6.2 |
| PostgreSQL 14 Internals (ücretsiz) | Egor Rogov | 🟡🔴 M6.4, S4.1 |
| Database Internals | Alex Petrov | 🔴 S4.1 |
| **Designing Data-Intensive Applications** | Martin Kleppmann | 🔴 S4, S6 |
| OAuth 2 in Action | Justin Richer, Antonio Sanso | 🟡 M9 |
| Keycloak — Identity and Access Management for Modern Applications | Stian Thorgersen, Pedro Igor Silva | 🟡 M9.3 |
| Clean Architecture | Robert C. Martin | 🟡 M11 |
| Domain-Driven Design | Eric Evans | 🔴 S5.1 |
| Implementing Domain-Driven Design | Vaughn Vernon | 🔴 S5.1 |
| Fundamentals of Software Architecture | Mark Richards, Neal Ford | 🟡🔴 M11, S5 |
| Software Architecture: The Hard Parts | Ford, Richards, Sadalage, Dehghani | 🔴 S5, S6 |
| Building Microservices / Monolith to Microservices | Sam Newman | 🔴 S6 |
| Release It! | Michael Nygard | 🔴 S6.4 |
| Building Secure & Reliable Systems | Google SRE/Security | 🔴 S7, S8 |
| Site Reliability Engineering (ücretsiz) | Google | 🔴 S8.4 |
| Distributed Tracing in Practice | Austin Parker vd. | 🔴 M13, S8.5 |
| AI Engineering | Chip Huyen | 🟡🔴 M15, S9 |
| Grokking Algorithms | Aditya Bhargava | 🟢 D1–D5 |
| Cracking the Coding Interview | Gayle Laakmann McDowell | 🟢🟡 D, I1 |
| Elements of Programming Interviews | Aziz, Lee, Prakash | 🟡🔴 D |
| The Pragmatic Programmer | Hunt & Thomas | Hepsi |

### Resmî Dokümantasyon
- [Microsoft Learn — .NET](https://learn.microsoft.com/dotnet/) · [ASP.NET Core](https://learn.microsoft.com/aspnet/core/) · [EF Core](https://learn.microsoft.com/ef/core/)
- [.NET Blog](https://devblogs.microsoft.com/dotnet/) · [.NET Architecture e-books](https://learn.microsoft.com/dotnet/architecture/)
- [.NET AI dokümantasyonu](https://learn.microsoft.com/dotnet/ai/) · [Microsoft Agent Framework (GitHub)](https://github.com/microsoft/agent-framework) · [Aspire](https://aspire.dev/)
- [Microsoft Learn — Azure](https://learn.microsoft.com/azure/) · [Azure Architecture Center](https://learn.microsoft.com/azure/architecture/) · [Well-Architected Framework](https://learn.microsoft.com/azure/well-architected/) · [Microsoft Foundry](https://learn.microsoft.com/azure/foundry/)
- [PostgreSQL](https://www.postgresql.org/docs/) · [MongoDB](https://www.mongodb.com/docs/) · [Redis](https://redis.io/docs/) · [Keycloak](https://www.keycloak.org/documentation)
- [OAuth 2.1](https://oauth.net/2.1/) · [OpenID Connect](https://openid.net/developers/how-connect-works/) · [OWASP Cheat Sheet Series](https://cheatsheetseries.owasp.org/)
- [Model Context Protocol](https://modelcontextprotocol.io/) · [MCP C# SDK](https://github.com/modelcontextprotocol/csharp-sdk) · [A2A Protocol](https://github.com/a2aproject/A2A)
- [OpenTelemetry .NET](https://opentelemetry.io/docs/languages/net/)

### Azure Sertifikaları (C1 → AZ-900, C2 → AI-200)
- **AZ-900 — Azure Fundamentals:** [resmî study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900) · [ücretsiz learning path'ler](https://learn.microsoft.com/en-us/training/courses/az-900t00) · [guided project: tag & lock ile kaynak koruma / maliyet guardrail](https://learn.microsoft.com/en-us/training/paths/introduction-cloud-infrastructure-apply-azure-skills-guided-projects/)
- **AI-200 — Azure AI Cloud Developer Associate:** [resmî study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200) · [sertifika sayfası](https://learn.microsoft.com/en-us/credentials/certifications/azure-ai-cloud-developer-associate/) · [AI-200T00 kursu](https://learn.microsoft.com/en-us/training/courses/ai-200t00) · [Cosmos DB ile AI çözümleri learning path](https://learn.microsoft.com/en-us/training/paths/develop-ai-solutions-azure-cosmos-db/) · [backend servis entegrasyonu learning path](https://learn.microsoft.com/en-us/training/paths/integrate-backend-services-ai-solutions/)
- [Ücretsiz practice assessment'lar](https://learn.microsoft.com/en-us/credentials/certifications/practice-assessments-for-microsoft-certifications) — sınav öncesi kapalı kitap deneme (AI-200 için henüz yayımlanmamış olabilir)
- Müfredat değişikliği takibi: [mscerts/hub — AI-200 study guide güncellemesi (Ekim 2026)](https://github.com/mscerts/hub/issues/74) · ikincil özetler (resmî sayfayla karşılaştırarak kullan): [Whizlabs AI-200](https://www.whizlabs.com/blog/ai-200-study-guide/), [CBT Nuggets AZ-900 güncellemeleri](https://www.cbtnuggets.com/blog/certifications/microsoft/what-are-the-latest-updates-to-the-az-900-exam)
- [Vector search için Azure servisi seçimi](https://learn.microsoft.com/en-us/azure/architecture/guide/technology-choices/vector-search) · [AI iş yükleri için veri platformu (WAF)](https://learn.microsoft.com/en-us/azure/well-architected/ai/data-platform)
- ⚠️ Sınav müfredatları değişiyor: resmî study guide'lar tarihli ("skills measured as of ...") — **sınava girmeden önce mutlaka kontrol et.**

### Derleme & Runtime İç Yapısı (M2, S1)
- [sharplab.io](https://sharplab.io/) — C# → lowered C# / IL / JIT asm
- [ILSpy](https://github.com/icsharpcode/ILSpy) · [MSBuild Structured Log Viewer](https://msbuildlog.com/)
- [Book of the Runtime (BOTR)](https://github.com/dotnet/runtime/tree/main/docs/design/coreclr/botr)
- [.NET host bileşenleri (apphost/hostfxr/hostpolicy)](https://github.com/dotnet/runtime/blob/main/docs/design/features/host-components.md)
- [Native AOT](https://learn.microsoft.com/dotnet/core/deploying/native-aot/) · [Trimming](https://learn.microsoft.com/dotnet/core/deploying/trimming/trim-self-contained)
- [Performance Improvements in .NET](https://devblogs.microsoft.com/dotnet/tag/performance/) — Stephen Toub'un yıllık serisi
- [ECMA-335 CLI spesifikasyonu](https://ecma-international.org/publications-and-standards/standards/ecma-335/)

### Ağ & Protokoller (J1.2, J1.3, M5)
- [High Performance Browser Networking](https://hpbn.co/) — TCP, TLS, HTTP/2, QUIC; ücretsiz ve hâlâ en iyi kaynak
- [MDN — HTTP](https://developer.mozilla.org/docs/Web/HTTP) · [HTTP/3 açıklaması (ücretsiz kitap)](https://http3-explained.haxx.se/)
- [Cloudflare Learning Center](https://www.cloudflare.com/learning/) — DNS, TLS, DDoS, CDN kavramları
- [.NET ağ API'leri](https://learn.microsoft.com/dotnet/fundamentals/networking/) · [`SocketsHttpHandler` ve bağlantı havuzu](https://learn.microsoft.com/dotnet/fundamentals/networking/http/httpclient-guidelines)

### Veri (J8, M6, M8, S4)
- [Use The Index, Luke](https://use-the-index-luke.com/) — index'lerin en iyi ücretsiz anlatımı
- [The Internals of PostgreSQL](https://www.interdb.jp/pg/)
- [MongoDB University](https://learn.mongodb.com/)
- [Redis dokümantasyonu](https://redis.io/docs/latest/) — veri tipleri, `SCAN`, keyspace notifications, client-side caching
- [RabbitMQ dokümantasyonu](https://www.rabbitmq.com/docs) — quorum queues, federation, shovel, akış kontrolü
- [Jepsen analizleri](https://jepsen.io/analyses) — dağıtık veritabanlarının tutarlılık testleri

### DSA & Pattern Pratiği (D1–D11)
- [NeetCode](https://neetcode.io/) — "Blind 75" / "NeetCode 150"
- [LeetCode](https://leetcode.com/) · [AlgoMonster](https://algo.monster/) · [VisuAlgo](https://visualgo.net/) · [Big-O Cheat Sheet](https://www.bigocheatsheet.com/)

### Observability & SRE (M13, S8)
- [Microsoft Learn — .NET observability with OpenTelemetry](https://learn.microsoft.com/dotnet/core/diagnostics/observability-with-otel)
- [Google SRE Book](https://sre.google/sre-book/table-of-contents/) · [Grafana dokümantasyonu](https://grafana.com/docs/) · [Seq](https://docs.datalust.co/docs)

### Mimari & AI-Driven Development (M11, M15.8, S5, S9)
- [Martin Fowler — bliki](https://martinfowler.com/bliki/) · [ThoughtWorks Technology Radar](https://www.thoughtworks.com/radar)
- [Anthropic — Building Effective Agents](https://www.anthropic.com/research/building-effective-agents)
- [Anthropic — Claude Code dokümantasyonu](https://docs.claude.com/claude-code)
- [OWASP Top 10 for LLM Applications](https://genai.owasp.org/)

### Diğer Roadmap'ler
- [milanm/DotNet-Developer-Roadmap](https://github.com/milanm/DotNet-Developer-Roadmap) — seviye bazlı .NET roadmap (2026)
- [The Ultimate .NET 2026 Roadmap](https://antondevtips.com/roadmap/dotnet)
- [roadmap.sh/aspnet-core](https://roadmap.sh/aspnet-core)

### Blog & YouTube
- **Milan Jovanović** — mimari, CQRS, EF Core, Keycloak entegrasyonu
- **Nick Chapsas** — performans, modern C#, kütüphane karşılaştırmaları
- **Stephen Cleary** — async/await ve concurrency
- **Andrew Lock** (.NET Escapades) — ASP.NET Core internals
- **Steve Gordon** — performans, HttpClient, internals
- **Damien Bowden** — ASP.NET Core güvenliği, OIDC, Keycloak
- **Derek Comartin** (CodeOpinion) — mesajlaşma, dağıtık sistemler
- **Konrad Kokosa** — bellek ve GC
- **Tim Corey** — junior/mid seviye eğitim
- **Awesome .NET** ve **Awesome ASP.NET Core** GitHub listeleri

### Pratik & Mülakat
- LeetCode / HackerRank / Codewars / [Exercism C# track](https://exercism.org/tracks/csharp) / [Advent of Code](https://adventofcode.com/)
- eShop referans uygulaması, Ardalis Clean Architecture şablonu
- Pramp / interviewing.io (mock interview) · System Design Primer (GitHub) · ByteByteGo

---

## Nasıl İlerleyeceğim?

1. Bu roadmap **canlı bir doküman**. Bir konu tamamlandığında başlığı klasör bağlantısına dönüşecek.
2. Her konunun klasöründe: `README.md` + kod dosyaları + `interview-questions.md`; kod `ConsoleApp`/`WebApp` projelerinin içindedir.
3. **Sıralı ilerle** — seviyeleri atlama, DSA'yı paralel yürüt.
4. Kodu **kopyalama, yaz.** AI asistanını açıklama ve review için kullan, yerine yazdırma (→ J13.2).
5. Her seviyeyi capstone + mock mülakatla kapat.
6. Anlamadığın bir konuyu "sonra dönerim" deme — o konu mülakatta gelir.

---

*Son güncelleme: Ekim 2026 · Hedef sürüm: .NET 10 LTS / C# 14 · .NET 11 Kasım 2026*
*.NET ekosistemi (özellikle AI, Azure ve lisans tarafı) hızlı değişiyor — [2026 Radarı](#2026-radarı--neler-değişti)'nı 3 ayda bir gözden geçir.*

