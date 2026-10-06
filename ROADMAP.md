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
| 1 | [Bu Roadmap Nasıl Kullanılır?](#bu-roadmap-nasıl-kullanılır) | Yapı, ID sistemi, her konunun klasör şablonu |
| 2 | [Seviyeler ve Çıkış Kriterleri](#seviyeler-ve-çıkış-kriterleri) | Junior/Mid/Senior'dan ne beklenir |
| 3 | [Genel Harita](#genel-harita) | Tüm modüller tek bakışta |
| 4 | [Seviye 1 — Junior](#seviye-1--junior) | J1–J14 · temeller, ilk API, ilk veritabanı, ilk deploy |
| 5 | [Seviye 2 — Mid](#seviye-2--mid) | M1–M16 · derinlik, production, kimlik, NoSQL, AI uygulamaları |
| 6 | [Seviye 3 — Senior](#seviye-3--senior) | S1–S10 · runtime, performans, ölçek, dağıtık sistemler, mimari |
| 7 | [DSA Pisti — Veri Yapıları & Algoritmalar](#dsa-pisti--veri-yapıları--algoritmalar) | D1–D11 · 1. haftadan itibaren **paralel** ilerler |
| 8 | [Mülakat Hazırlığı](#mülakat-hazırlığı) | I1–I6 · coding, behavioral, system design, liderlik |
| 9 | [Repo Yapısı](#repo-yapısı) | Klasör hiyerarşisi |
| 10 | [Çalışma Planı](#çalışma-planı) | Seviye bazlı haftalık plan |
| 11 | [2026 Radarı — Neler Değişti?](#2026-radarı--neler-değişti) | .NET, Azure, AI ve veri ekosistemindeki güncel değişiklikler |
| 12 | [Kaynaklar](#kaynaklar) | Kitaplar, dokümantasyon, pratik platformları |

---

## Bu Roadmap Nasıl Kullanılır?

### Yapı: Seviye → Modül → Konu

```
Seviye   (Junior / Mid / Senior / DSA / Mülakat)
  └── Modül   (ör. J8 Veritabanı Temelleri)        → bir klasör
        └── Konu   (ör. J8.4 Transaction & ACID)   → bir alt klasör = bir kod projesi
```

- **Seviyeler sıralıdır.** Junior modüllerini bitirmeden Mid'e geçme. Aynı konu (ör. veritabanı, async, güvenlik) her seviyede **daha derin bir kesitle** tekrar gelir — bu bilinçli bir tasarım.
- **DSA ayrı bir pisttir.** Seviyelerle **paralel** yürür: haftada 2-3 gün DSA, kalan günler seviye modülleri.
- **Her seviye bir capstone projesiyle biter.** Capstone, o seviyedeki modülleri tek bir gerçekçi projede birleştirir ve mülakatta gösterilebilir bir portföy parçasıdır.

### ID sistemi

| Önek | Anlamı | Örnek |
|------|--------|-------|
| `J` | Junior modülü | J6.2 = Junior, 6. modül, 2. konu |
| `M` | Mid modülü | M9.3 = Keycloak |
| `S` | Senior modülü | S4.3 = Partitioning & Sharding |
| `D` | DSA pisti | D7 = Pattern kataloğu |
| `I` | Mülakat hazırlığı | I3 = System design interview |

Metin içi çapraz referanslar bu ID'lerle yapılır: **(→ M6.3)** "bu konunun derin hali M6.3'te" demektir.

### İşaretler

| İşaret | Anlamı |
|--------|--------|
| 🛠 **Kod** | Bu konunun `src/` klasöründe yazılacak projenin kapsamı |
| 🎯 **Mülakat** | Modül sonunda cevaplayabilmen gereken tipik sorular |
| ⭐ | Mülakatlarda **çok sık** sorulan klasik konu |
| **(.NET 11)** | Kasım 2026'da gelen / önizlemedeki özellik — bil ama production'da .NET 10 varsay |
| 🟢 🟡 🔴 | (DSA pistinde) Junior / Mid / Senior zorluk seviyesi |

### Her konu klasörünün şablonu

```
<seviye>/<NN-modül>/<NN-konu>/
├── README.md               # kavramsal anlatım (aşağıdaki şablon)
├── src/                    # `dotnet run` ile çalışan bağımsız proje
├── tests/                  # (varsa) konuya ait testler
└── interview-questions.md  # 10-20 soru + cevap
```

Altyapı gerektiren modüllerde (veritabanı, Redis, Keycloak, broker) modül klasöründe ortak bir `docker-compose.yml` bulunur; konular onu kullanır.

Her konunun `README.md`'si şu şablonu izler:

```markdown
# <Konu>

## Ne? (tanım, zihinsel model)
## Neden? (hangi problemi çözüyor)
## Nasıl? (kod üzerinden — src/ ile birebir)
## Gerçek dünya senaryosu
## Mülakat perspektifi (nasıl sorulur, ne beklenir)
## Yaygın hatalar / tuzaklar
## Performans & trade-off notları
## Kaynaklar
```

### Nasıl çalışılır?

1. Konunun `README.md`'sini oku.
2. `src/` içindeki projeyi **kendin yaz** — kopyalama. Okumak öğrenmek değildir.
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
| **Ops & Cloud** | Docker, Compose, ilk CI, App Service deploy | CI/CD, Kubernetes temeli, Aspire, Azure servisleri, observability | IaC, AKS, Azure mimarisi, SRE/SLO, production teşhisi |
| **AI** | LLM temelleri, AI asistanını doğru kullanma, ilk `IChatClient` | RAG, embeddings, MCP, Foundry, agentic coding | Agent Framework, multi-agent, eval, guardrail, LLMOps |
| **DSA** | Big-O, temel yapılar, kolay pattern'ler (LeetCode Easy) | Ağaç/graf, backtracking, heap, DP temeli (Medium) | DP ileri, ileri yapılar, sistem tasarımında DSA (Medium/Hard) |

> **Çıkış kriteri = capstone + mock mülakat.** Bir seviyeyi "bitti" saymak için o seviyenin capstone projesini tamamla ve I6'daki mock mülakat senaryosunu kapalı kitap geç.

---

## Genel Harita

### 🟢 Seviye 1 — Junior · `01-junior/`

| ID | Modül | Odak |
|----|-------|------|
| [J1](#j1-geliştirici-temelleri) | Geliştirici Temelleri | Git, HTTP/DNS/TLS, .NET CLI & SDK, debugging |
| [J2](#j2-c-temelleri) | C# Temelleri | Program anatomisi, tip sistemi, OOP, exception, günlük modern C# |
| [J3](#j3-collections-delegates--linq) | Collections, Delegates & LINQ | Koleksiyonlar, generics, lambda, LINQ temel |
| [J4](#j4-async-programlamaya-giriş) | Async Programlamaya Giriş | Thread/Task kavramı, `async`/`await`, cancellation |
| [J5](#j5-build--run-temelleri) | Build & Run Temelleri | C# → IL → native, `bin`/`obj`, `run` vs `publish` |
| [J6](#j6-aspnet-core-temelleri) | ASP.NET Core Temelleri | Host, DI, configuration, middleware, logging |
| [J7](#j7-web-api-temelleri) | Web API Temelleri | REST, Minimal API, validation, DTO, OpenAPI, HttpClient |
| [J8](#j8-veritabanı-temelleri) | Veritabanı Temelleri | İlişkisel model, SQL, index, ACID, transaction, SQL vs NoSQL, PostgreSQL |
| [J9](#j9-ef-core-ile-veri-erişimi) | EF Core ile Veri Erişimi | ADO.NET, DbContext, migration, sorgulama, change tracking |
| [J10](#j10-test-temelleri) | Test Temelleri | xUnit v3, mocking, test edilebilir kod, ilk integration test |
| [J11](#j11-güvenlik--kimlik-temelleri) | Güvenlik & Kimlik Temelleri | AuthN/AuthZ, JWT, OAuth/OIDC'ye giriş, temel web açıkları |
| [J12](#j12-docker-temelleri) | Docker Temelleri | Container, Dockerfile, Compose |
| [J13](#j13-cloud--azurea-giriş) | Cloud & Azure'a Giriş | Cloud kavramları, App Service, Blob, ilk CI |
| [J14](#j14-ai-ile-çalışmaya-giriş) | AI ile Çalışmaya Giriş | LLM temelleri, AI asistanı kullanımı, ilk `IChatClient` |
| [🏁](#junior-capstone-görev-yönetimi-apisi) | **Junior Capstone** | Görev yönetimi API'si — uçtan uca |

### 🟡 Seviye 2 — Mid · `02-mid/`

| ID | Modül | Odak |
|----|-------|------|
| [M1](#m1-c-derinlemesine) | C# Derinlemesine | C# 8→15, generics, expression tree, iterator, dispose |
| [M2](#m2-build-runtime--deployment) | Build, Runtime & Deployment | Roslyn, IL, MSBuild, apphost, deployment modelleri |
| [M3](#m3-aspnet-core-derinlemesine) | ASP.NET Core Derinlemesine | Host, Options, DI, middleware, filter, background service |
| [M4](#m4-concurrency--async-derinlemesine) | Concurrency & Async Derinlemesine | ThreadPool, TPL, async internals, senkronizasyon, Channels |
| [M5](#m5-api-tasarımı--protokoller) | API Tasarımı & Protokoller | REST ileri, versioning, gRPC, GraphQL, SignalR, resilience |
| [M6](#m6-sql--postgresql-derinlemesine) | SQL & PostgreSQL Derinlemesine | İleri SQL, index/plan, isolation/locking, PostgreSQL, migration |
| [M7](#m7-ef-core-derinlemesine--veri-erişim-desenleri) | EF Core Derinlemesine & Veri Erişim Desenleri | EF performansı, ileri modelleme, Dapper, mapping |
| [M8](#m8-nosql--caching) | NoSQL & Caching | NoSQL modelleme, MongoDB, Redis, caching, arama |
| [M9](#m9-kimlik--erişim-yönetimi-oauth-oidc-keycloak) | Kimlik & Erişim Yönetimi | OAuth 2.1, OIDC, **Keycloak**, policy authz, BFF |
| [M10](#m10-test-stratejisi) | Test Stratejisi | Integration, Testcontainers, snapshot/contract, E2E |
| [M11](#m11-mimari--tasarım-temelleri) | Mimari & Tasarım Temelleri | SOLID, pattern'ler, Clean/Hexagonal, vertical slice, CQRS |
| [M12](#m12-mesajlaşma--message-brokerlar) | Mesajlaşma & Message Broker'lar | RabbitMQ, Service Bus, Kafka'ya giriş, kütüphaneler |
| [M13](#m13-observability) | Observability | Structured logging, metrics, OpenTelemetry tracing |
| [M14](#m14-devops-cicd-container--kubernetes-temelleri) | DevOps | CI/CD, container ileri, Kubernetes temeli, Aspire |
| [M15](#m15-azure-ile-uygulama-geliştirme) | Azure ile Uygulama Geliştirme | Managed Identity, compute, veri servisleri, izleme |
| [M16](#m16-ai-engineering-uygulama-geliştirme) | AI Engineering: Uygulama Geliştirme | M.E.AI, RAG, vector search, **MCP**, Foundry |
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

---

## Seviye 1 — Junior

> **Hedef:** Çalışan, test edilmiş, veritabanına bağlı bir CRUD API'yi tek başına yazmak, container'a koymak ve buluta deploy etmek.
> **Süre:** ~6 ay (günde 2-3 saat, DSA dahil) · **Klasör:** `01-junior/`
> **Bu seviyede "neden"i bilmek yeterli; "nasıl çalışıyor"un derin kısmı Mid'de gelir.**

---

### J1. Geliştirici Temelleri

> 📁 `01-junior/01-dev-foundations/` · ⏱ ~1 hafta · Önkoşul: —
> Kod yazmadan önceki zemin: versiyon kontrolü, web'in nasıl çalıştığı ve .NET araç zinciri.

#### J1.1 Git & Versiyon Kontrolü · `01-git`
- Commit, branch, `merge` vs `rebase` ⭐, fast-forward vs merge commit
- Merge conflict çözümü; `git stash`, `git cherry-pick`, `git bisect`
- `reset` (soft/mixed/hard) vs `revert` — paylaşılmış geçmişi neden yeniden yazmamalı
- Pull request akışı, code review kültürü, küçük PR prensibi
- Branching stratejileri: GitHub Flow, trunk-based development, Git Flow — hangisi ne zaman
- Conventional commits, semantic versioning (`MAJOR.MINOR.PATCH`)
- .NET için `.gitignore` (`bin/`, `obj/`, `*.user`, `.vs/`)
- 🛠 **Kod:** Script ile oluşturulan örnek repo üzerinde merge/rebase conflict senaryosu + `git bisect` ile hatayı getiren commit'i bulma alıştırması.

#### J1.2 Web Nasıl Çalışır: HTTP, DNS, TLS · `02-how-the-web-works`
- "Tarayıcıya URL yazınca ne olur?" ⭐ — URL anatomisi → DNS çözümleme → TCP bağlantısı → TLS handshake → HTTP isteği → yanıt
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

#### J1.3 .NET Ekosistemi & CLI · `03-dotnet-ecosystem-cli`
- .NET Framework → .NET Core → birleşik .NET (5+) tarihçesi; .NET Standard'ın rolü ve neden geri plana düştüğü
- **SDK vs Runtime vs Target Framework Moniker** (`net10.0`, `net10.0-windows`) ⭐
- Sürüm politikası: her Kasım yeni sürüm; çift sayılar **LTS (3 yıl)**, tek sayılar **STS (24 ay)** — .NET 10 LTS, .NET 11 STS
- `dotnet new`, `build`, `run`, `test`, `add package`, `sln add`; template'ler (`webapi`, `console`, `classlib`, `xunit`)
- Solution (`.sln` ve yeni XML tabanlı `.slnx`) ve proje (`.csproj`) yapısı, proje referansı
- NuGet temelleri: paket kaynağı, versiyon, transitive bağımlılık
- `global.json` ile SDK sürümünü sabitleme
- **File-based apps (.NET 10):** `dotnet run app.cs` — `.csproj` olmadan tek dosya; `#:package`, `#:sdk Microsoft.NET.Sdk.Web`, `#:property` direktifleri; büyüyünce projeye dönüştürme
- 🛠 **Kod:** Aynı küçük uygulamayı (a) file-based app, (b) console projesi, (c) solution + class library + test projesi olarak üç farklı şekilde kur.

#### J1.4 IDE, Debugging & Kod Kalitesi Araçları · `04-ide-debugging`
- Visual Studio, JetBrains Rider, VS Code + C# Dev Kit — farkları
- Breakpoint, conditional breakpoint, tracepoint, watch, immediate window, call stack, step into/over/out
- Exception ayarları: "first-chance exception"da durmak
- `dotnet watch` ve Hot Reload
- Roslyn analyzer'ları, `.editorconfig`, `dotnet format`, `TreatWarningsAsErrors`
- Nullable uyarılarını okumak ve ciddiye almak
- 🛠 **Kod:** Bilerek hatalar yerleştirilmiş bir program — her hatayı debugger ile bulma alıştırması + örnek `.editorconfig` ve analyzer kuralları.

🎯 **Mülakat:** "Merge ile rebase farkı?" · "Tarayıcıya URL yazınca neler olur?" · "SDK ile runtime farkı nedir?" · "LTS ile STS arasındaki fark?" · "Hangi HTTP metodları idempotent?"

---

### J2. C# Temelleri

> 📁 `01-junior/02-csharp-fundamentals/` · ⏱ ~3 hafta · Önkoşul: J1
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

#### J2.3 OOP · `03-oop`
- Encapsulation, Inheritance, Polymorphism, Abstraction — tanım değil, **örnekle**
- `abstract class` vs `interface` — karar tablosu ⭐
- `virtual`, `override`, `new` (method hiding), `sealed`
- Overloading (derleme zamanı) vs overriding (runtime)
- Interface default implementation (C# 8+), explicit interface implementation
- **Composition over inheritance** — neden ve nasıl refactor edilir
- Constructor zinciri (`this(...)`, `base(...)`), object initializer
- `Equals`, `GetHashCode`, `ToString` — **birlikte override kuralı** ⭐
- `IEquatable<T>`, `IComparable<T>`, `IComparer<T>`
- Object yaşam döngüsü ve `IDisposable`'a giriş (`using`)
- 🛠 **Kod:** Ödeme yöntemleri (kredi kartı, havale, cüzdan) üzerinden inheritance → composition refactor'u; `Equals`/`GetHashCode` yanlış override edilince `HashSet`'in bozulduğu demo.

#### J2.4 Exception Handling · `04-exception-handling`
- `try` / `catch` / `finally`, `using` / `await using`
- Exception hiyerarşisi; custom exception ne zaman gerekli
- Exception filter (`catch (X ex) when (...)`)
- **`throw` vs `throw ex`** — stack trace kaybı ⭐
- Exception maliyeti — control flow için kullanmama kuralı
- Hangi exception'ı yakalamalı, hangisini yakalamamalı (`catch (Exception)` ne zaman meşru)
- `Task` içindeki exception'lar ve `AggregateException`'a giriş
- Exception vs `Result` dönüşü — kavramsal giriş (→ M11.6)
- 🛠 **Kod:** `throw` vs `throw ex` stack trace karşılaştırması, exception filter örneği, custom domain exception hiyerarşisi.

#### J2.5 Günlük Modern C# · `05-everyday-modern-csharp`
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

🎯 **Mülakat:** "Value type ile reference type farkı?" · "`struct` ne zaman tercih edilir?" · "Boxing nedir, ne zaman olur?" · "String neden immutable?" · "`abstract class` vs `interface`?" · "`throw` vs `throw ex`?" · "`record` ile `class` farkı?" · "`Equals` override edince neden `GetHashCode` de override edilmeli?"

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

> 📁 `01-junior/05-build-run-basics/` · ⏱ ~1 hafta · Önkoşul: J1.3
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
> Modül klasöründe ortak `docker-compose.yml`: PostgreSQL + pgAdmin + MongoDB + Redis.
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
- View
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

🎯 **Mülakat:** "ACID nedir, her harfi örnekle anlat" · "Index nasıl çalışır, neden her kolona index konmaz?" · "Composite index'te kolon sırası neden önemli?" · "`INNER JOIN` ile `LEFT JOIN` farkı?" · "`WHERE` ile `HAVING` farkı?" · "3NF nedir?" · "Dirty read, phantom read nedir?" · "SQL mi NoSQL mi — neye göre seçersin?" · "Para için neden `float` kullanılmaz?"

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

> 📁 `01-junior/10-testing-basics/` · ⏱ ~1.5 hafta · Önkoşul: J6, J9
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

🎯 **Mülakat:** "Unit test ile integration test farkı?" · "Mock ile stub farkı?" · "`DateTime.Now` test edilebilirliği neden bozar?" · "AAA nedir?" · "%100 coverage iyi test demek mi?"

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
- Volume vs bind mount; bridge network ve container'lar arası iletişim
- Registry'ler: Docker Hub, GitHub Container Registry, Azure Container Registry
- 🛠 **Kod:** PostgreSQL, Redis ve MongoDB'yi `docker run` ile ayağa kaldırıp volume ile veri kalıcılığını test eden script'ler.

#### J12.2 .NET için Dockerfile · `02-dockerfile-dotnet`
- Base image: `sdk` (build) vs `aspnet`/`runtime` (çalıştırma)
- **Multi-stage build** ⭐ — SDK'yı final image'a taşımamak
- Layer cache: önce `.csproj` kopyala → `restore` → sonra kaynak kodu
- `.dockerignore` (`bin/`, `obj/`, `.git`)
- Non-root kullanıcı (`USER $APP_UID`)
- `dotnet publish /t:PublishContainer` ile Dockerfile'sız image
- 🛠 **Kod:** Todo API için tek aşamalı (kötü) ve multi-stage (iyi) Dockerfile; image boyutu ve build süresi karşılaştırması.

#### J12.3 Docker Compose ile Geliştirme Ortamı · `03-docker-compose`
- Çok servisli tanım: API + PostgreSQL + Redis + pgAdmin
- `depends_on` + `healthcheck` ile başlangıç sırası
- Servis adıyla DNS çözümleme; **container içinden `localhost` tuzağı** ⭐
- Environment değişkenleri, `.env`, override dosyaları
- Hedef: "tek komutla çalışan repo"
- 🛠 **Kod:** `docker compose up` ile API + DB + cache'in birlikte kalktığı, sağlık kontrollü geliştirme ortamı.

🎯 **Mülakat:** "Container ile VM farkı?" · "Multi-stage build neden?" · "Container içindeki uygulama `localhost:5432`'ye neden bağlanamaz?" · "Volume ne işe yarar?"

---

### J13. Cloud & Azure'a Giriş

> 📁 `01-junior/13-azure-basics/` · ⏱ ~1.5 hafta · Önkoşul: J12
> Azure servislerinin derinlemesine kullanımı → M15; mimari → S8.

#### J13.1 Cloud Kavramları · `01-cloud-concepts`
- IaaS, PaaS, SaaS, serverless — örneklerle
- Azure hiyerarşisi: tenant → subscription → resource group → resource; region, availability zone
- Maliyet modeli: kullandıkça öde, free tier, **bütçe alarmı (ilk iş!)**
- Araçlar: Azure Portal, Azure CLI (`az`), Azure Developer CLI (`azd`)
- Shared responsibility model
- 🛠 **Kod:** `az` CLI ile resource group oluşturan, kaynakları listeleyen ve temizleyen script; bütçe alarmı kurulum adımları.

#### J13.2 App Service'e İlk Deploy · `02-app-service-deploy`
- App Service plan ve web app ilişkisi
- Deploy yolları: `az webapp up`, ZIP deploy, GitHub Actions
- Application Settings → `IConfiguration`'a environment variable olarak akması
- Log stream ile canlı log izleme
- 🛠 **Kod:** Todo API'yi App Service'e deploy et; connection string'i App Settings'ten ver.

#### J13.3 Blob Storage Temelleri · `03-blob-storage`
- Storage account, container, blob; access tier'lara giriş
- `Azure.Storage.Blobs` SDK ile upload/download/stream
- Yerel geliştirme için **Azurite** emülatörü
- SAS token'a giriş — neden public container yerine
- 🛠 **Kod:** Dosya yükleme/indirme API'si — yerelde Azurite, Azure'da gerçek storage.

#### J13.4 GitHub Actions ile İlk CI · `04-first-ci`
- Workflow, job, step, runner kavramları
- `actions/setup-dotnet` → restore → build → test
- Pull request'te otomatik test ve branch koruma kuralı
- 🛠 **Kod:** Junior capstone için build + test pipeline'ı.

🎯 **Mülakat:** "IaaS, PaaS, SaaS farkı?" · "App Service'te connection string nereye konur?" · "CI nedir, neden PR'da test çalıştırılır?"

---

### J14. AI ile Çalışmaya Giriş

> 📁 `01-junior/14-ai-basics/` · ⏱ ~1 hafta · Önkoşul: J7
> 2026'da junior pozisyonlarda bile soruluyor: "AI araçlarını nasıl kullanıyorsun?" RAG, MCP, agent'lar → M16, S9.

#### J14.1 LLM Temelleri · `01-llm-basics`
- LLM nedir (sezgisel): bir sonraki token'ı tahmin eden model
- Token, context window, temperature, max tokens
- Mesaj rolleri: system / user / assistant
- Hallucination, knowledge cutoff, determinizm eksikliği
- Prompt temelleri: net görev tanımı, örnek verme (few-shot), çıktı formatı belirtme
- Sağlayıcılar ve modeller: OpenAI, Anthropic Claude, Microsoft Foundry üzerinden sunulan modeller, yerel modeller (Ollama, Foundry Local)
- Maliyet: token bazlı fiyatlandırma, input vs output token
- 🛠 **Kod:** Aynı prompt'u farklı temperature ve system prompt'larla çalıştırıp çıktıları karşılaştıran deney.

#### J14.2 AI Kodlama Asistanlarını Doğru Kullanmak · `02-ai-coding-assistants`
- GitHub Copilot, Claude Code, Cursor — 2026'da standart geliştirici aracı
- **"Vibe coding" vs mühendislik** ⭐ — üretilen kodu anlamadan kabul etmemek; her satırı okumak ve test etmek
- İyi görev tanımı: bağlam, kısıtlar, kabul kriteri
- AI'ın tipik hataları: var olmayan API uydurma, eski sürüm sözdizimi, güvenlik açığı, gereksiz karmaşıklık
- Gizlilik: şirket kodunu ve secret'ları hangi araca verdiğini bilmek
- Öğrenme dengesi: temel konuları **önce kendin** yaz, AI'ı açıklama ve review için kullan
- 🛠 **Kod:** Bir görevi önce kendin çöz, sonra AI ile çöz; iki çözümü doğruluk, okunabilirlik ve test kapsamı açısından karşılaştıran kısa değerlendirme (README).

#### J14.3 İlk LLM Entegrasyonu · `03-first-llm-integration`
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
- GitHub Actions CI (build + test) ve App Service'e deploy
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

> 📁 `02-mid/05-api-design/` · ⏱ ~3 hafta · Önkoşul: J7, M3
> Bir API'yi tasarlayabilmek, evrimleştirebilmek ve doğru protokolü seçebilmek. Gateway/BFF ve ölçekte API → S6.5, S10.

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

🎯 **Mülakat:** "Offset yerine cursor pagination neden?" · "POST'u nasıl idempotent yaparsın?" · "API'yi breaking change olmadan nasıl evrimleştirirsin?" · "REST mi gRPC mi GraphQL mi?" · "GraphQL'de N+1 nasıl çözülür?" · "SignalR'ı birden fazla instance'a nasıl ölçeklersin?" · "Retry neden jitter ister?" · "Circuit breaker nasıl çalışır?"

---

### M6. SQL & PostgreSQL Derinlemesine

> 📁 `02-mid/06-sql-postgresql/` · ⏱ ~3 hafta · Önkoşul: J8, J9
> Modül klasöründe ortak `docker-compose.yml`: PostgreSQL 18 (+ isteğe bağlı SQL Server).
> J8'deki kavramların **mekanizması**: plan nasıl seçilir, MVCC nasıl çalışır, kilitler neden çakışır. Ölçek (replication, sharding, storage engine) → S4.

#### M6.1 İleri SQL · `01-advanced-sql`
- **Window function'lar:** `ROW_NUMBER`, `RANK`, `DENSE_RANK`, `NTILE`, `LAG`/`LEAD`, `FIRST_VALUE`; running total; frame tanımı (`ROWS BETWEEN ...`)
- CTE ve **recursive CTE** (kategori ağacı, organizasyon şeması)
- Upsert: `INSERT ... ON CONFLICT DO UPDATE` (PostgreSQL), `MERGE` (SQL Server, PostgreSQL 15+) ve bilinen tuzakları
- `LATERAL` join, `DISTINCT ON`, `RETURNING`
- Klasik problemler: top-N per group, gaps & islands, deduplication, pivot
- **Set-based düşünme vs satır satır (cursor/döngü)** ⭐
- 🛠 **Kod:** 25 ileri seviye SQL problemi (top-N per group, running total, hiyerarşi, gaps & islands) + çözümleri + sonuçları doğrulayan .NET test runner'ı.

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
- SQL Server 2025 / Azure SQL: native `json` tipi, **`VECTOR` tipi** (→ M16.4)
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
> Modül klasöründe ortak `docker-compose.yml`: MongoDB (tek node'lu replica set — transaction için), Redis 8, Elasticsearch/OpenSearch.
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

- **StackExchange.Redis:** `ConnectionMultiplexer` **singleton** kuralı ⭐, `IDatabase`, pipelining, async
- Key tasarımı (`app:entity:id`), TTL
- **Eviction policy'leri** (`allkeys-lru`, `allkeys-lfu`, `volatile-ttl`, `noeviction`) ⭐
- Persistence: RDB vs AOF trade-off'u
- Atomiklik: `MULTI`/`EXEC`, **Lua script**
- Pub/Sub vs Streams
- Replication, Sentinel, Cluster'a giriş (→ S4.6)
- **2026 durumu:** Redis 8 AGPLv3 ile yeniden açık kaynak; **Valkey** (Linux Foundation forku, AWS/Google destekli); Azure'da **Azure Managed Redis** (Azure Cache for Redis emekliye ayrılıyor); Microsoft'un .NET ile yazılmış Redis uyumlu sunucusu **Garnet**
- 🛠 **Kod:** Leaderboard (sorted set), Lua script ile atomik rate limiter, session store, Redis Streams ile consumer group'lu iş kuyruğu, HyperLogLog ile tekil ziyaretçi sayımı.

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
- Hybrid search (keyword + vector) → M16.4
- 🛠 **Kod:** Ürün arama — PostgreSQL FTS versiyonu ve Elasticsearch versiyonu (Türkçe analyzer, facet, typo toleransı) + sonuç kalitesi karşılaştırması.

🎯 **Mülakat:** "MongoDB'de embed mi reference mı — neye göre?" · "ESR kuralı nedir?" · "Redis tek thread'li ise nasıl bu kadar hızlı?" · "Eviction policy nasıl seçilir?" · "Cache stampede nedir, nasıl önlenir?" · "Cache invalidation stratejin ne?" · "`HybridCache` ne çözdü?" · "Redis mi Valkey mi?"

---

### M9. Kimlik & Erişim Yönetimi: OAuth, OIDC, Keycloak

> 📁 `02-mid/09-identity-access/` · ⏱ ~3 hafta · Önkoşul: J11
> Modül klasöründe ortak `docker-compose.yml`: Keycloak 26 + PostgreSQL.
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
- Cloud'da secret'sız kimlik: managed identity, workload identity federation (→ M15.1)
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
- Katmanları ayrı proje mi, klasör mü yapmalı
- **Trade-off:** küçük CRUD serviste fazla tören; karmaşık domain'de kurtarıcı
- 🛠 **Kod:** Aynı "sipariş ver" use case'inin N-tier ve Clean Architecture versiyonları; DB'yi PostgreSQL'den MongoDB'ye değiştirmenin iki versiyondaki maliyeti.

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

🎯 **Mülakat:** "SOLID'i örnekle anlat" · "Dependency Inversion ile Dependency Injection farkı?" · "Clean, Hexagonal ve Onion aynı şey mi?" ⭐ · "Clean Architecture mı Vertical Slice mı?" · "CQRS ile Event Sourcing farkı?" · "MediatR'ı neden kullanırsın / kullanmazsın?" · "Exception mı Result mı?"

---

### M12. Mesajlaşma & Message Broker'lar

> 📁 `02-mid/12-messaging/` · ⏱ ~2 hafta · Önkoşul: M4, M11
> Modül klasöründe ortak `docker-compose.yml`: RabbitMQ, Kafka (KRaft), Azure Service Bus emulator.
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

#### M12.2 RabbitMQ · `02-rabbitmq`
- AMQP modeli: **exchange** (direct, topic, fanout, headers), binding, queue
- **Quorum queue** (dayanıklı varsayılan tercih) vs classic queue; RabbitMQ Streams
- Ack/nack, prefetch (QoS), publisher confirms
- **Dead-letter exchange**, TTL ile gecikmeli retry
- `RabbitMQ.Client` 7.x (tamamen async API)
- 🛠 **Kod:** Sipariş olaylarını topic exchange ile birden fazla servise dağıtan sistem; DLX ile retry + dead-letter akışı.

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

🎯 **Mülakat:** "At-least-once teslimatta duplicate mesajları nasıl ele alırsın?" ⭐ · "Queue ile topic farkı?" · "Kafka mı RabbitMQ mı?" · "Poison message nedir?" · "Mesaj sırasını nasıl garanti edersin?" · "Command ile event farkı?"

---

### M13. Observability

> 📁 `02-mid/13-observability/` · ⏱ ~2 hafta · Önkoşul: J6.5
> Modül klasöründe ortak `docker-compose.yml`: OpenTelemetry Collector + Grafana LGTM (`grafana/otel-lgtm`) + Seq.
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

> 📁 `02-mid/14-devops/` · ⏱ ~2 hafta · Önkoşul: J12, J13.4
> İleri Kubernetes, IaC ve platform mühendisliği → S8.

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
- Multi-arch build (amd64/arm64)
- Container'da .NET: CPU/bellek limitlerine duyarlılık, GC ayarları (`DOTNET_GCHeapHardLimit`), DATAS
- `dotnet publish /t:PublishContainer` ileri kullanım
- Rootless çalışma, read-only filesystem
- 🛠 **Kod:** Aynı API'nin dört image varyantı: boyut, CVE sayısı (Trivy), başlangıç süresi ve bellek tablosu.

#### M14.3 Kubernetes Temelleri · `03-kubernetes-basics`
- Mimari: control plane, node, kubelet
- Temel nesneler: Pod, ReplicaSet, **Deployment**, Service, Ingress (ve Gateway API'ye giriş)
- ConfigMap ve Secret ile konfigürasyon
- **Probe'lar:** liveness, readiness, startup ⭐ — ASP.NET Core health check eşlemesi
- Resource request/limit, QoS sınıfı, **OOMKilled**
- **Graceful shutdown:** SIGTERM, `terminationGracePeriodSeconds`, preStop hook
- `kubectl` temelleri; yerel cluster (kind, k3d, Docker Desktop)
- 🛠 **Kod:** Capstone API'sinin Deployment + Service + Ingress + ConfigMap + Secret manifest'leri; kind üzerinde rolling update ve probe davranışı gözlemi.

#### M14.4 Aspire ile Orkestrasyon · `04-aspire`
- **Aspire** (13 sürümüyle ".NET" öneki kalktı, polyglot oldu — Python, JavaScript/TypeScript da birinci sınıf)
- **AppHost:** kaynak modeli (PostgreSQL, Redis, RabbitMQ, Kafka, Keycloak, Ollama container'larını kodla tanımlama), referanslar ve servis keşfi
- **Service defaults:** OpenTelemetry, health check, resilience, service discovery hazır
- **Aspire Dashboard** ile yerel gözlemlenebilirlik
- `aspire` CLI; AI coding agent'lar için MCP desteği
- Deploy: `azd` ile Azure Container Apps'e, Docker Compose / Kubernetes çıktıları
- Integration testlerinde Aspire (`DistributedApplicationTestingBuilder`)
- Docker Compose'a göre avantaj/dezavantaj
- 🛠 **Kod:** Mid capstone'un AppHost'u — tüm bağımlılıklar tek `aspire run` ile; `azd up` ile Container Apps'e deploy.

🎯 **Mülakat:** "CI ile CD farkı?" · "Pipeline'da secret'ları nasıl yönetirsin?" · "Liveness ile readiness probe farkı?" · "Pod neden OOMKilled olur?" · "Kubernetes'te graceful shutdown nasıl sağlanır?" · "Aspire ne çözüyor, Docker Compose'dan farkı ne?"

---

### M15. Azure ile Uygulama Geliştirme

> 📁 `02-mid/15-azure-development/` · ⏱ ~3 hafta · Önkoşul: J13, M9
> Sertifika notu: **AZ-204 31 Temmuz 2026'da emekliye ayrıldı**; geliştirici yolu artık **AI-200 (Azure AI Cloud Developer Associate)**. Bu modül + M16 bu sınavın kapsamıyla büyük ölçüde örtüşür.
> Azure çözüm mimarisi, Well-Architected Framework ve maliyet → S8.3.

#### M15.1 Azure'da Kimlik: Managed Identity, Entra ID & Key Vault · `01-identity-keyvault`
- **Managed Identity** ⭐ — system-assigned vs user-assigned; connection string'siz ve secret'sız bağlantı
- `Azure.Identity`: `DefaultAzureCredential` — yerelde geliştirici kimliği, Azure'da managed identity; production'da neden **belirli credential** seçmek daha güvenli
- **Azure RBAC** ve rol atamaları (data plane vs control plane)
- Entra ID: app registration, service principal, `Microsoft.Identity.Web`; scope (`scp`) vs app role (`roles`)
- **Key Vault:** secret / key / certificate; `IConfiguration` entegrasyonu; soft delete, purge protection, rotasyon
- Workload identity federation (GitHub Actions, AKS)
- 🛠 **Kod:** API'nin Azure SQL, Storage ve Key Vault'a **hiç secret kullanmadan** managed identity ile bağlanması; yerelde aynı kodun geliştirici kimliğiyle çalışması.

#### M15.2 Compute Seçenekleri: App Service, Functions, Container Apps · `02-compute`
- **App Service:** deployment slot ve swap, autoscale, health check, Always On
- **Azure Functions:** isolated worker model, trigger ve binding'ler, **Flex Consumption** planı (hızlı ölçek, VNet, scale-to-zero), **Durable Functions** (orchestrator, activity, entity; fan-out/fan-in, human interaction)
- **Azure Container Apps:** KEDA ile event-driven ölçekleme, scale-to-zero, revision ve trafik bölme, Dapr, Container Apps Jobs, serverless GPU
- AKS'e kısa bakış (→ S8.1)
- **Karar tablosu** ⭐ — App Service vs Functions vs Container Apps vs AKS
- 🛠 **Kod:** Aynı arka plan işinin Functions (Service Bus trigger) ve Container Apps (KEDA ile Service Bus ölçekleme) versiyonları; soğuk başlangıç ve maliyet notları.

#### M15.3 Veri Servisleri: Azure SQL, Cosmos DB, PostgreSQL, Managed Redis · `03-data-services`
- **Azure SQL:** single DB, elastic pool, serverless (auto-pause); Entra ile şifresiz auth; transient fault retry; `VECTOR` tipi
- **Azure Database for PostgreSQL Flexible Server:** pgvector, Entra auth, read replica, PgBouncer
- **Cosmos DB:**
  - **Partition key seçimi** ⭐ — en kritik ve geri dönüşü en zor karar (cardinality, hot partition)
  - RU/s ekonomisi: provisioned, autoscale, serverless
  - **Consistency level'lar:** Strong, Bounded Staleness, Session (varsayılan), Consistent Prefix, Eventual
  - Indexing policy, **Change Feed**, TTL, vector search
  - Cosmos DB for MongoDB (vCore) ve Cosmos DB for PostgreSQL seçenekleri
  - Cosmos DB ne zaman **yanlış** seçim
- **Azure Managed Redis** — Azure Cache for Redis'in halefi
- 🛠 **Kod:** Sipariş verisini Cosmos DB'de iki farklı partition key ile modelleyip RU maliyetlerini ölçme; Change Feed ile okuma modeli güncelleme.

#### M15.4 Storage & Event Grid · `04-storage-events`
- Blob tier'ları (Hot/Cool/Cold/Archive) ve lifecycle policy
- **User delegation SAS** (account key'li SAS yerine)
- Queue Storage, Table Storage — basit ve ucuz seçenekler
- **Event Grid** ile blob olaylarına tepki
- Front Door / CDN ile statik içerik
- 🛠 **Kod:** Yüklenen görseli Event Grid tetiklemesiyle küçük resme dönüştüren akış.

#### M15.5 Azure Mesajlaşma Servisleri Karşılaştırması · `05-messaging-services`
- **Service Bus** (kurumsal mesajlaşma, transaction, session) vs **Event Hubs** (yüksek hacimli stream, Kafka uyumlu) vs **Event Grid** (reaktif olay yönlendirme) vs **Storage Queue** (basit, ucuz) ⭐
- Hangisi ne zaman — karar tablosu ve maliyet
- 🛠 **Kod:** Aynı olayı dört servise gönderen ve tüketim modellerini karşılaştıran demo.

#### M15.6 Application Insights & Azure Monitor · `06-monitoring`
- **Azure Monitor OpenTelemetry Distro** (`UseAzureMonitor()`) — önerilen modern yol
- Telemetri türleri: request, dependency, trace, exception, custom event/metric
- **KQL temelleri:** `requests | where ... | summarize ... | render`
- Application Map, Live Metrics, failure/performance görünümleri, availability test
- Alert kuralları ve action group'lar
- Sampling ve **maliyet kontrolü**
- 🛠 **Kod:** Capstone'u Application Insights'a bağla; en yavaş 5 endpoint ve hata oranı için KQL sorguları + alert.

#### M15.7 API Management & App Configuration · `07-apim-app-config`
- **API Management:** policy'ler (rate limit, JWT doğrulama, dönüşüm), ürün ve abonelik, geliştirici portalı
- APIM'i **AI gateway** olarak kullanmak: LLM token limitleri, yük dağıtımı, semantic cache
- **App Configuration:** merkezi config, Key Vault referansları, **feature flag**'ler ve dinamik yenileme
- 🛠 **Kod:** APIM arkasında JWT doğrulamalı ve rate limit'li API; App Configuration feature flag'iyle yeni özelliği kademeli açma.

🎯 **Mülakat:** "Managed identity nedir, secret'sız nasıl bağlanırsın?" ⭐ · "Functions mı, Container Apps mı, App Service mi?" · "Cosmos DB'de partition key nasıl seçilir?" · "Cosmos consistency level'ları?" · "Service Bus, Event Hubs, Event Grid farkı?" · "`DefaultAzureCredential` production'da neden dikkat ister?"

---

### M16. AI Engineering: Uygulama Geliştirme

> 📁 `02-mid/16-ai-engineering/` · ⏱ ~3 hafta · Önkoşul: J14, M5.9
> Modül klasöründe ortak `docker-compose.yml`: Ollama, Qdrant, PostgreSQL + pgvector.
> .NET artık AI-first bir platform; 2026'da backend rollerinde bile soruluyor. Agent mimarisi, Agent Framework, eval ve LLMOps → S9.

#### M16.1 Microsoft.Extensions.AI Derinlemesine · `01-microsoft-extensions-ai`
- `IChatClient`, `IEmbeddingGenerator<TInput,TEmbedding>`, `ChatOptions`, `ChatResponse`
- **Middleware pipeline** (`ChatClientBuilder`): `UseLogging`, `UseOpenTelemetry`, `UseDistributedCache`, `UseFunctionInvocation`; custom middleware (rate limit, PII redaksiyonu)
- DI kaydı (`AddChatClient`), birden fazla model için keyed client
- Sağlayıcılar: OpenAI, Azure OpenAI / Microsoft Foundry, Ollama, Anthropic, Foundry Local
- **Bir LLM çağrısı da bir dış servis çağrısıdır** ⭐ — timeout, retry, fallback model, maliyet izleme (→ M5.9)
- .NET AI şablonları (`dotnet new aichatweb`)
- 🛠 **Kod:** Sağlayıcı değiştirilebilen chat servisi; caching + telemetri + logging middleware'leri; Aspire Dashboard'da token ve gecikme metrikleri.

#### M16.2 Prompt & Context Engineering · `02-prompt-context-engineering`
- System prompt tasarımı, talimat hiyerarşisi, few-shot, güvenilmeyen içeriği ayırma (delimiter)
- **Context engineering** ⭐ — pencereye ne girer: getirilen dokümanlar, tool sonuçları, bellek, konuşma özeti; context bütçesi
- Prompt'ları **kod gibi** yönetmek: dosyada versiyonlama, testlerle koruma
- Reasoning modelleri ve "düşünme" bütçesi
- Provider tarafı **prompt caching** ile maliyet/gecikme düşürme
- Prompt injection farkındalığı (savunma → S9.6)
- 🛠 **Kod:** Prompt'ları versiyonlanan ve eval testleriyle (→ S9.5) korunan bir destek talebi sınıflandırma servisi.

#### M16.3 Function Calling & Structured Output · `03-tools-structured-output`
- Tool/function calling: model karar verir, uygulama çalıştırır; `AIFunctionFactory.Create`, `UseFunctionInvocation`
- **Tool tasarımı:** net isim ve açıklama, dar kapsam, deterministik dönüş, anlamlı hata
- **Structured output:** JSON schema, `GetResponseAsync<T>()` ile tipli yanıt
- Yan etkili tool'larda onay (human-in-the-loop)
- 🛠 **Kod:** "Siparişim nerede?" asistanı — sipariş sorgulama ve kargo takibi tool'ları, tipli yanıt, iade gibi yan etkili işlemde kullanıcı onayı.

#### M16.4 Embeddings & Vector Search · `04-embeddings-vector-search`
- Embedding nedir; cosine / dot product / Euclidean benzerlik; boyut ve maliyet
- **`Microsoft.Extensions.VectorData`** — sağlayıcıdan bağımsız vector store soyutlaması
- Vector store seçenekleri: **pgvector**, Azure AI Search, Qdrant, Redis, **SQL Server / Azure SQL `VECTOR` + EF Core 10 `VectorDistance`**, Cosmos DB, MongoDB, Milvus, Weaviate, Pinecone
- **ANN index'leri:** HNSW, IVFFlat, DiskANN — recall / gecikme / bellek trade-off'u ⭐
- Metadata filtreleme; **hybrid search** (BM25 + vector) ve Reciprocal Rank Fusion
- 🛠 **Kod:** Aynı doküman setini VectorData soyutlamasıyla pgvector ve Qdrant'ta indeksleyip arama kalitesi ve gecikmesini karşılaştırma.

#### M16.5 RAG Pipeline · `05-rag`
- **RAG neden** ⭐ — güncel/özel veri, kaynak gösterimi, fine-tuning'e göre maliyet ve esneklik
- **Ingestion:** yükle → parse (PDF/Office) → chunk → embed → metadata ile sakla
- **Chunking stratejileri:** sabit boyut + overlap, semantic, yapıya duyarlı (başlık/paragraf), parent-child ⭐
- **Retrieval:** sorgu embedding'i → vector/hybrid search → **reranking**
- Query transformation: yeniden yazma, multi-query, HyDE
- Prompt oluşturma, **kaynak gösterimi (citation)**, "bilmiyorum" davranışı
- Yaygın hatalar: kötü chunking, eksik metadata, reranking yokluğu, değerlendirme yapmamak (ileri RAG ve eval → S9.4, S9.5)
- 🛠 **Kod:** Şirket içi dokümanlar için Q&A servisi — ingestion worker, hybrid search, reranking, kaynak gösterimi ve basit web arayüzü.

#### M16.6 MCP: Model Context Protocol · `06-mcp`
- Nedir: LLM uygulamalarını (host/client) araç ve veri kaynaklarına (server) bağlayan açık protokol
- Primitifler: **tools, resources, prompts** (+ sampling, elicitation)
- Transport'lar: **stdio** vs **Streamable HTTP**
- **Resmî C# SDK** (`ModelContextProtocol`, `ModelContextProtocol.AspNetCore` — 1.x kararlı): `[McpServerToolType]`, `[McpServerTool]`, `WithStdioServerTransport()`, `MapMcp()`
- .NET'te MCP client: MCP tool'larını `IChatClient`'a `AIFunction` olarak vermek
- **Güvenlik:** OAuth 2.1 tabanlı MCP yetkilendirmesi, en az yetki, tool poisoning, tool çıktısı üzerinden prompt injection
- MCP server'larını Claude Code, VS Code, GitHub Copilot'ta kullanmak
- 🛠 **Kod:** Sipariş veritabanını salt-okunur sorgulayan MCP server'ı (stdio + HTTP); HTTP versiyonu Keycloak ile korumalı; Claude Code/VS Code'a bağlama + aynı server'ı kullanan .NET MCP client.

#### M16.7 Microsoft Foundry & Azure OpenAI · `07-microsoft-foundry`
- **Microsoft Foundry** (Ocak 2026'da Azure AI Foundry'den yeniden adlandırıldı): proje, model kataloğu (OpenAI, Anthropic Claude, Meta, Mistral, Phi...), deployment türleri, kota ve TPM
- SDK'lar: OpenAI .NET SDK / `Azure.AI.OpenAI`, `Azure.AI.Projects`; **Entra ID ile anahtarsız kimlik doğrulama** ⭐
- Azure AI Content Safety, prompt shields
- Foundry Agent Service'e giriş (→ S9.1, S9.7)
- Azure AI Search entegrasyonu
- Maliyet: pay-as-you-go vs provisioned throughput
- Sertifika notu: **AI-102 emekliye ayrıldı → AI-103 (Azure AI App & Agent Developer)**
- 🛠 **Kod:** M16.5'teki RAG servisini Foundry modelleri + Azure AI Search + managed identity ile Azure'a taşıma (`azd` + Bicep).

#### M16.8 Agentic Coding Workflow · `08-agentic-coding`
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
> Modül klasöründe ortak `docker-compose.yml`: Kafka, RabbitMQ, PostgreSQL, Redis, OpenTelemetry Collector.

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

> 📁 `03-senior/08-cloud-native-sre/` · ⏱ ~3 hafta · Önkoşul: M14, M15
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

> 📁 `03-senior/09-ai-systems/` · ⏱ ~3.5 hafta · Önkoşul: M16
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
- 🛠 **Kod:** M16.5'teki RAG'ı ACL filtreli, reranking'li ve agentic hale getirme; her iyileştirmenin eval skorlarına etkisi (→ S9.5).

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
> Her modülün `src/` klasöründe: veri yapısını **sıfırdan** implementasyon + xUnit testleri + .NET'in yerleşik karşılığıyla BenchmarkDotNet karşılaştırması + çözülmüş problemler.

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
- [ ] AI araçlarını nasıl kullandığımı anlatabiliyor muyum (→ M16.8)?
- [ ] Benim sorularım hazır mı?
- [ ] Ortam testi (kamera, mikrofon, IDE, ekran paylaşımı)?

- 🛠 **Kod:** (README) Her seviye için soru havuzu ve değerlendirme rubriği — bir arkadaşla veya AI ile mock mülakat yapmak için.

---

## Repo Yapısı

Klasör hiyerarşisi roadmap'in birebir aynısıdır: **seviye → modül → konu**. Konu klasör adları her konunun başlığında (`· 01-relational-modeling` gibi) yazar; burada sadece modül seviyesi gösterilir.

```
dotnet-interview/
├── README.md
├── ROADMAP.md                      # bu dosya
├── CLAUDE.md
├── Directory.Build.props           # (önerilen) ortak ayarlar: net10.0, nullable, analyzer'lar
├── Directory.Packages.props        # (önerilen) Central Package Management — tüm projelerde tek paket sürümü
│
├── 01-junior/
│   ├── 01-dev-foundations/
│   ├── 02-csharp-fundamentals/
│   ├── 03-collections-linq/
│   ├── 04-async-basics/
│   ├── 05-build-run-basics/
│   ├── 06-aspnetcore-fundamentals/
│   ├── 07-web-api-basics/
│   ├── 08-database-fundamentals/
│   ├── 09-ef-core-basics/
│   ├── 10-testing-basics/
│   ├── 11-security-auth-basics/
│   ├── 12-docker-basics/
│   ├── 13-azure-basics/
│   ├── 14-ai-basics/
│   └── capstone/
│
├── 02-mid/
│   ├── 01-csharp-deep-dive/
│   ├── 02-build-runtime-deployment/
│   ├── 03-aspnetcore-deep-dive/
│   ├── 04-concurrency-async/
│   ├── 05-api-design/
│   ├── 06-sql-postgresql/
│   ├── 07-data-access-advanced/
│   ├── 08-nosql-caching/
│   ├── 09-identity-access/
│   ├── 10-testing-strategy/
│   ├── 11-architecture-design/
│   ├── 12-messaging/
│   ├── 13-observability/
│   ├── 14-devops/
│   ├── 15-azure-development/
│   ├── 16-ai-engineering/
│   └── capstone/
│
├── 03-senior/
│   ├── 01-runtime-internals/
│   ├── 02-performance/
│   ├── 03-concurrency-advanced/
│   ├── 04-data-at-scale/
│   ├── 05-architecture-advanced/
│   ├── 06-distributed-systems/
│   ├── 07-security-engineering/
│   ├── 08-cloud-native-sre/
│   ├── 09-ai-systems/
│   ├── 10-system-design/
│   └── capstone/
│
├── 04-dsa/
│   ├── 01-complexity/
│   ├── 02-linear-structures/
│   ├── 03-trees-heaps-tries/
│   ├── 04-graphs/
│   ├── 05-sorting-searching/
│   ├── 06-recursion-backtracking/
│   ├── 07-pattern-catalog/
│   ├── 08-pattern-matching-guide/
│   ├── 09-dynamic-programming/
│   ├── 10-advanced-structures/
│   └── 11-csharp-practice/
│
└── 05-interview/
    ├── 01-coding-live-coding/
    ├── 02-behavioral/
    ├── 03-system-design-interview/
    ├── 04-take-home/
    ├── 05-technical-leadership/
    └── 06-mock-checklist/
```

**Örnek — bir modülün açılmış hali:**

```
01-junior/08-database-fundamentals/
├── README.md                       # modül özeti, konu listesi, önkoşullar, kurulum
├── docker-compose.yml              # PostgreSQL + pgAdmin + MongoDB + Redis (modülün tüm konuları kullanır)
├── 01-relational-modeling/
│   ├── README.md
│   ├── src/
│   ├── tests/
│   └── interview-questions.md
├── 02-sql-basics/
├── 03-index-basics/
├── 04-transactions-acid/
├── 05-sql-vs-nosql/
└── 06-postgresql-getting-started/
```

**Kod konvansiyonları (önerilen):**
- Her konu bağımsız çalışır: `cd <konu>/src && dotnet run` — konular arası proje referansı yok
- Proje/namespace adı klasörden türetilir: `Junior.DatabaseFundamentals.TransactionsAcid`
- Altyapı modül seviyesindeki `docker-compose.yml`'den gelir; connection string'ler `appsettings.Development.json` + User Secrets
- Capstone'lar birden fazla proje içerebilir; kendi `*.slnx` dosyaları vardır

---

## Çalışma Planı

Günde 2-3 saat varsayımıyla. Her satırda **seviye modülü + aynı haftalarda paralel DSA modülü** var. Zaten bir seviyedeysen modüllerin 🎯 sorularıyla kendini test et; rahat cevapladığın modülleri hızlı geç.

### 🟢 Junior (~27 hafta)

| Hafta | Seviye modülü | Paralel DSA |
|-------|---------------|-------------|
| 1 | J1 Geliştirici Temelleri | D1 |
| 2-4 | J2 C# Temelleri | D2 |
| 5-6 | J3 Collections, Delegates & LINQ | D2 |
| 7-8 | J4 Async + J5 Build & Run | D5 |
| 9-10 | J6 ASP.NET Core Temelleri | D5 |
| 11-13 | J7 Web API Temelleri | D3 (ağaç, heap) |
| 14-16 | **J8 Veritabanı Temelleri** | D3 |
| 17-18 | J9 EF Core | D11 — Easy tekrar |
| 19-20 | J10 Test + J12 Docker | D11 |
| 21-22 | J11 Güvenlik & Kimlik | D11 |
| 23-24 | J13 Azure + J14 AI | D11 |
| 25-27 | 🏁 **Junior Capstone** + I1, I2, I4 + I6 Junior mock | Zamanlı Easy setleri |

### 🟡 Mid (~44 hafta)

| Hafta | Seviye modülü | Paralel DSA |
|-------|---------------|-------------|
| 1-2 | M1 C# Derinlemesine | D4 |
| 3-4 | M2 Build, Runtime & Deployment | D4 |
| 5-7 | M3 ASP.NET Core Derinlemesine | D6 |
| 8-10 | **M4 Concurrency & Async** — en yüksek getirili modül | D6 |
| 11-13 | M5 API Tasarımı | D7 |
| 14-16 | **M6 SQL & PostgreSQL** | D7 |
| 17-18 | M7 EF Core Derinlemesine | D8 |
| 19-21 | **M8 NoSQL & Caching** (MongoDB, Redis) | D8 |
| 22-24 | **M9 Kimlik: OAuth, OIDC, Keycloak** | D9 (temel) |
| 25-26 | M10 Test Stratejisi | D9 |
| 27-29 | M11 Mimari & Tasarım | D9 |
| 30-31 | M12 Mesajlaşma | D11 — Medium |
| 32-33 | M13 Observability | D11 |
| 34-35 | M14 DevOps | D11 |
| 36-38 | M15 Azure | D11 |
| 39-41 | M16 AI Engineering | D11 |
| 42-44 | 🏁 **Mid Capstone** + I3'e giriş + I6 Mid mock | Zamanlı Medium setleri |

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
| .NET sürümleri | **.NET 10 LTS** (Kasım 2025 → Kasım 2028) güncel üretim sürümü; **.NET 11 STS** Kasım 2026 (runtime async, C# 15 union tipleri, OpenAPI 3.2, Zstandard) | J1.3, M1.1, M4.3 |
| Destek politikası | STS desteği **24 aya** çıktı; **.NET 8 ve .NET 9 aynı gün, 10 Kasım 2026'da** destek dışı kalıyor → .NET 10'a geçiş gündemde | J1.3, S5.6 |
| C# 14 | Extension members, `field` keyword, null-conditional assignment, implicit span dönüşümleri | J2.5, M1.1 |
| SDK & araçlar | File-based apps (`dotnet run app.cs`), `.slnx`, `dotnet test`'te Microsoft.Testing.Platform, xUnit v3 | J1.3, J10.1 |
| ASP.NET Core 10 | Varsayılan OpenAPI 3.1, Minimal API yerleşik validation, Server-Sent Events, Identity'de passkey | J7, J11.1, M5.7 |
| EF Core 10 | Named query filters, complex type → JSON, SQL Server vector search, `LeftJoin`/`RightJoin` | M7, M16.4 |
| Lisans değişiklikleri | **MediatR** (v13+), **AutoMapper** (v15+), **MassTransit** (v9), **FluentAssertions** (v8) ticari lisansa geçti → alternatifleri bilmek gerekiyor | J10.1, M7.4, M11.5, M12.5 |
| Aspire | **Aspire 13:** ".NET" öneki kalktı, polyglot (Python, JS/TS AppHost), AI agent'lar için MCP desteği | M14.4 |
| .NET'te AI | **Microsoft Agent Framework 1.0** (Nisan 2026) — Semantic Kernel + AutoGen birleşimi; SK destekte, AutoGen bakım modunda | S9.1 |
| AI protokolleri | **MCP C# SDK** 1.x kararlı; **A2A** protokolü Linux Foundation altında | M16.6, S9.3 |
| Azure AI | Azure AI Foundry → **Microsoft Foundry** (Ocak 2026); Anthropic Claude modelleri katalogda; Foundry Agent Service'te hosted agent'lar | M16.7, S9.7 |
| Azure servisleri | Azure Cache for Redis → **Azure Managed Redis**; Functions **Flex Consumption**; Container Apps serverless GPU; Azure SQL `VECTOR` tipi GA | M15 |
| Sertifikalar | **AZ-204 → AI-200** (31 Temmuz 2026); **AI-102 → AI-103**; mimari için AZ-305 | M15, M16.7, S8 |
| Veritabanları | **PostgreSQL 18** (async I/O, `uuidv7()`, virtual generated columns); **Redis 8** AGPLv3 ile yeniden açık kaynak + **Valkey** forku; MongoDB 8.x Community'de search/vector search; MongoDB EF Core provider'da vector search | J8.6, M6.4, M8 |
| Kimlik | **OAuth 2.1 + RFC 9700**: her yerde PKCE, implicit/ROPC yok; DPoP ve PAR yaygınlaşıyor; **Keycloak 26**: passkeys, DPoP, FAPI 2.0, organizations; Azure AD B2C → Entra External ID | J11.3, M9 |
| Güvenlik | **OWASP Top 10:2025** — yeni: A03 Software Supply Chain Failures, A10 Mishandling of Exceptional Conditions; .NET 10'da post-quantum kriptografi | S7.1, S7.4 |

**Radar kaynakları:**
- [What's new in .NET 10](https://learn.microsoft.com/dotnet/core/whats-new/dotnet-10/overview) · [What's new in C# 14](https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-14) · [EF Core 10 yenilikleri](https://learn.microsoft.com/ef/core/what-is-new/ef-core-10.0/whatsnew)
- [.NET STS releases supported for 24 months](https://devblogs.microsoft.com/dotnet/dotnet-sts-releases-supported-for-24-months/) · [.NET 11 Preview 6 roundup](https://visualstudiomagazine.com/articles/2026/07/15/net-11-preview-6-roundup-aspnet-core-maui-c-ef-core-and-sdk-updates.aspx)
- [File-based apps](https://learn.microsoft.com/dotnet/core/sdk/file-based-apps) · [xUnit v3 + Microsoft Testing Platform](https://xunit.net/docs/getting-started/v3/microsoft-testing-platform)
- [Microsoft Agent Framework 1.0](https://visualstudiomagazine.com/articles/2026/04/06/microsoft-ships-production-ready-agent-framework-1-0-for-net-and-python.aspx) · [MCP C# SDK](https://csharp.sdk.modelcontextprotocol.io/)
- [Microsoft Foundry yeniden adlandırma](https://www.directionsonmicrosoft.com/reports/foundry-gets-new-name-anthropic-models/) · [What's new in Microsoft Foundry (Build 2026)](https://devblogs.microsoft.com/foundry/whats-new-in-microsoft-foundry-build-2026/)
- [Aspire 13 multi-language support](https://www.infoq.com/news/2025/11/dotnet-aspire-13-release/)
- [AZ-204 → AI-200 geçişi](https://examinotion.com/blog/az-204-retirement-ai-200-migration-guide) · [AI-103 study guide](https://tutorialsdojo.com/ai-103-azure-ai-app-and-agent-developer-associate-study-guide/)
- [MediatR & MassTransit lisans değişikliği](https://www.milanjovanovic.tech/blog/mediatr-and-masstransit-going-commerical-what-this-means-for-you)
- [PostgreSQL 18 yenilikleri](https://neon.com/postgresql/postgresql-18-new-features) · [Redis 8 AGPLv3](https://alternativeto.net/news/2025/5/redis-goes-open-source-again-with-the-launch-of-redis-8-under-the-agplv3-license) · [MongoDB EF Core provider: vector search](https://devblogs.microsoft.com/dotnet/mongodb-efcore-provider-queryable-encryption-vector-search/)
- [Keycloak 26.6 release](https://www.keycloak.org/2026/04/keycloak-2660-released) · [RFC 9700 — OAuth 2.0 Security BCP](https://datatracker.ietf.org/doc/rfc9700/)
- [OWASP Top 10:2025](https://owasp.org/Top10/2025/0x00_2025-Introduction/)

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
| AI Engineering | Chip Huyen | 🟡🔴 M16, S9 |
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

### Derleme & Runtime İç Yapısı (M2, S1)
- [sharplab.io](https://sharplab.io/) — C# → lowered C# / IL / JIT asm
- [ILSpy](https://github.com/icsharpcode/ILSpy) · [MSBuild Structured Log Viewer](https://msbuildlog.com/)
- [Book of the Runtime (BOTR)](https://github.com/dotnet/runtime/tree/main/docs/design/coreclr/botr)
- [.NET host bileşenleri (apphost/hostfxr/hostpolicy)](https://github.com/dotnet/runtime/blob/main/docs/design/features/host-components.md)
- [Native AOT](https://learn.microsoft.com/dotnet/core/deploying/native-aot/) · [Trimming](https://learn.microsoft.com/dotnet/core/deploying/trimming/trim-self-contained)
- [Performance Improvements in .NET](https://devblogs.microsoft.com/dotnet/tag/performance/) — Stephen Toub'un yıllık serisi
- [ECMA-335 CLI spesifikasyonu](https://ecma-international.org/publications-and-standards/standards/ecma-335/)

### Veri (J8, M6, M8, S4)
- [Use The Index, Luke](https://use-the-index-luke.com/) — index'lerin en iyi ücretsiz anlatımı
- [The Internals of PostgreSQL](https://www.interdb.jp/pg/)
- [MongoDB University](https://learn.mongodb.com/)
- [Jepsen analizleri](https://jepsen.io/analyses) — dağıtık veritabanlarının tutarlılık testleri

### DSA & Pattern Pratiği (D1–D11)
- [NeetCode](https://neetcode.io/) — "Blind 75" / "NeetCode 150"
- [LeetCode](https://leetcode.com/) · [AlgoMonster](https://algo.monster/) · [VisuAlgo](https://visualgo.net/) · [Big-O Cheat Sheet](https://www.bigocheatsheet.com/)

### Observability & SRE (M13, S8)
- [Microsoft Learn — .NET observability with OpenTelemetry](https://learn.microsoft.com/dotnet/core/diagnostics/observability-with-otel)
- [Google SRE Book](https://sre.google/sre-book/table-of-contents/) · [Grafana dokümantasyonu](https://grafana.com/docs/) · [Seq](https://docs.datalust.co/docs)

### Mimari & AI-Driven Development (M11, M16.8, S5, S9)
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
2. Her konunun klasöründe: `README.md` + `src/` + (varsa) `tests/` + `interview-questions.md`.
3. **Sıralı ilerle** — seviyeleri atlama, DSA'yı paralel yürüt.
4. Kodu **kopyalama, yaz.** AI asistanını açıklama ve review için kullan, yerine yazdırma (→ J14.2).
5. Her seviyeyi capstone + mock mülakatla kapat.
6. Anlamadığın bir konuyu "sonra dönerim" deme — o konu mülakatta gelir.

---

*Son güncelleme: Ekim 2026 · Hedef sürüm: .NET 10 LTS / C# 14 · .NET 11 Kasım 2026*
*.NET ekosistemi (özellikle AI, Azure ve lisans tarafı) hızlı değişiyor — [2026 Radarı](#2026-radarı--neler-değişti)'nı 3 ayda bir gözden geçir.*

