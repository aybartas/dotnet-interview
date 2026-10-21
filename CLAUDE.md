# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this repo is

A .NET Junior → Mid → Senior interview-prep study repo. **All content is written in Turkish** — keep new prose in Turkish (technical terms and identifiers stay in English, as in the existing text). Target platform is **.NET 10 (LTS) / C# 14**; .NET 11 / C# 15 features may be mentioned but must be marked **(.NET 11)**.

The repo started **docs-only** (`ROADMAP.md`, `README.md`, `CLAUDE.md`) and is being filled in one topic at a time. Content written so far: **J1 (`01-junior/01-dev-foundations/`) — all 5 topics**, **J2 (`01-junior/02-csharp-fundamentals/`) — all 6 topics** and **J3 (`01-junior/03-collections-linq/`) — all 3 topics**, **J4 (`01-junior/04-async-basics/`) — all 3 topics** and **J5 (`01-junior/05-build-run-basics/`) — all 3 topics** (each topic: theory `README.md`, runnable code inside the module project, `interview-questions.md`); certification track: **C1 (`06-certifications/01-az-900-azure-fundamentals/`, project `C01.AzureFundamentals`) — C1.1–C1.7** ( each topic: `README.md`, code, `quiz.md`). The remaining `01-junior/` … `05-interview/` folders described in `ROADMAP.md → Repo Yapısı` are created as their topics are written. The whole repo is **one solution** — `DotnetInterview.slnx` at the root (open this in Visual Studio) — plus root `Directory.Build.props`, `Directory.Packages.props` (central package management) and `global.json`; the SDK is .NET 10 (`dotnet` CLI is needed to build/run the examples).

| File | Role |
|------|------|
| `ROADMAP.md` | ~4.2k lines, the master plan: 6 tracks, 51 modules, ~300 topics. Each topic's theory outline and the scope of its code project live here. |
| `README.md` | For the human studying: how to work through it, folder layout, where theory vs. practice lives, how to author a new topic. |
| `CLAUDE.md` | This file. |
| `DotnetInterview.slnx` | The single solution (all module projects). |
| `01-junior/…` | Module projects; each topic folder holds README + code + `interview-questions.md`. |

When adding content, the usual task is "write topic X" — see [Writing a topic](#writing-a-topic).

## Roadmap structure: level → module → topic

`ROADMAP.md` is organized by seniority, and the folder layout mirrors it exactly:

| Level | ID prefix | Folder | Notes |
|-------|-----------|--------|-------|
| Junior | `J1`–`J13` | `01-junior/` | ends with `capstone/` |
| Mid | `M1`–`M15` | `02-mid/` | ends with `capstone/` |
| Senior | `S1`–`S10` | `03-senior/` | ends with `capstone/` |
| DSA track | `D1`–`D11` | `04-dsa/` | separate section, studied in parallel with levels |
| Interview | `I1`–`I6` | `05-interview/` | |
| Certification track | `C1`–`C2` | `06-certifications/` | **Level-independent**: C1 = AZ-900, C2 = AI-200 (Azure). Exam-oriented: theory + labs + exam-style quizzes (see [Certification track](#certification-track-06-certifications)) |

- A **module** (`### J8. Veritabanı Temelleri`) = a folder (`01-junior/08-database-fundamentals/`). Its header blockquote lists folder, duration (`⏱`), prerequisites; infra-heavy modules mention a shared module-level `docker-compose.yml`.
- A **topic** (`#### J8.4 Transaction & ACID · \`04-transactions-acid\``) = a subfolder and one independent code project. The folder name is part of the heading.
- Every topic ends with a `- 🛠 **Kod:** ...` line describing the code its topic folder will contain; every J/M/S module ends with a `🎯 **Mülakat:** ...` line. `⭐` marks frequently-asked topics.
- The same subject recurs at deeper levels (e.g. DB: J8 → M6/M8 → S4). Cross-references use IDs in plain text: `(→ M6.3)`. Don't duplicate content across levels — put each sub-topic at the level where it's typically asked and cross-reference.

## Folder layout and where content lives

**One module = one .NET project; one topic = one folder inside that project.** There are no per-topic projects and no `src/` or `tests/` folders. Folder names are **never invented** — they are written in the ROADMAP headings. A topic heading like

```markdown
#### J3.2 Delegates, Lambdas & Events · `02-delegates-lambdas-events`
```

combined with its module's `📁 01-junior/03-collections-linq/` line resolves to:

```
01-junior/03-collections-linq/                 # module = ONE project
├── J03.CollectionsLinq.csproj                 # project type fits the module: Console, Web, Web API, Worker…
├── Program.cs                                 # (console modules) topic menu: dotnet run -- 3.2 [section|all]
├── Topic.cs                                   # record Topic(Id, Title, Func<string[], int> Run)
├── docker-compose.yml                         # module-level infra, only where the module says so
└── 02-delegates-lambdas-events/               # topic = folder
    ├── README.md                              # THEORY — follows the template in ROADMAP.md
    ├── DelegatesLambdasEventsTopic.cs         # topic entry point: Run(args) → section menu
    ├── Sections/ Section01_*.cs … Sections.cs # runnable demos (console modules)
    ├── Models/ …                              # supporting types
    └── interview-questions.md                 # 10-20 Q&A
```

The solution (`DotnetInterview.slnx`) groups projects into solution folders by level (`/01 Junior/`, `/02 Mid/` …, `/06 Sertifika/` for the certification track); **every new module project must be added to it**, plus root files under `/00 Kök Dosyalar/`.

Where each kind of content belongs — keep this split, don't blur it:

| Content | Location |
|---------|----------|
| What to learn (outline) | ROADMAP topic bullets; `⭐` = frequently asked |
| Long-form theory | topic `README.md`, template: *Ne? / Neden? / Nasıl? / Gerçek dünya senaryosu / Mülakat perspektifi / Yaygın hatalar / Performans & trade-off / Kaynaklar* |
| What to build | the topic's `🛠 **Kod:**` line — the topic folder's code implements exactly that scope |
| Running code | topic folder inside the module project |
| Interview Q&A | module's `🎯 **Mülakat:**` line + topic `interview-questions.md` (certification track: `📝 **Sınav:**` line + topic `quiz.md`) |
| Infra | module-level `docker-compose.yml` |

**Lean rules (do not add things the user did not ask for):**
- **No tests** unless the topic itself is about testing (J10, M10 …). Verification of other topics = the demo's printed output. Do not create `*.Tests` projects, `tests/` folders or `xunit` references elsewhere.
- **No shell/PowerShell scripts, `.vscode/`, per-topic `global.json`, `SOLUTIONS.md`** etc. Command-line exercises (git, curl, tcpdump, `dotnet new` …) are written as code blocks inside the topic `README.md`; exercise solutions go into a `<details>` block at the end of the README.
- **No module-level README** (the ROADMAP already describes the module).
- Code-free modules (Kubernetes, CI/CD, Azure IaC…) get no project; their files appear as plain items in the solution folder.
- Extra projects only when the lesson *is* an assembly/process boundary or the module needs a different host (e.g. J7: Web API + a small console client). Example: `01-junior/02-csharp-fundamentals-lib/J02.Lib.csproj` (access modifiers, `const` embedding).

Conventions: root `Directory.Build.props` + `Directory.Packages.props` (central package management); project name `J<NN>.<ModuleName>` (so Solution Explorer sorts by module), `RootNamespace` `Junior.<ModuleName>`, and **one namespace per topic** (`Junior.CollectionsLinq.LinqBasics`) — this is what keeps same-named demo types (`Order`, `Customer` …) from colliding inside one project; topics never use each other's types (module-level helpers shared by all topics go into a `Shared/` folder, e.g. J04's `LocalApi`, `SectionRunner`); capstones are layered multi-project folders added to the same solution.

```bash
dotnet build DotnetInterview.slnx                   # whole repo
cd <level>/<module> && docker compose up -d        # only for modules with shared infra
cd <level>/<module> && dotnet run                  # topic list
cd <level>/<module> && dotnet run -- 3.2 [4|all]   # one topic / one section / all sections
```

Gotchas when merging topics into one project: `GlobalUsings.cs` is **project-wide** (a `global using System.Diagnostics;` can make `Trace` ambiguous — fix with a `using Trace = …` alias in the affected file); project properties such as `InvariantGlobalization=false`, `GenerateDocumentationFile` apply to the whole module (folder-scoped analyzer settings go into an `.editorconfig` inside the topic folder); the topic's old top-level `Program.cs` becomes `<Topic>Topic.cs` with `public static int Run(string[] args)` (async topics wrap `RunAsync(args).GetAwaiter().GetResult()`); a type named like its own namespace (`Exercises`) must live in that namespace.

## Certification track (`06-certifications/`)

The Azure certification content (**C1 = AZ-900**, **C2 = AI-200**) is its **own track**, separate from Junior/Mid/Senior (the former J13 "Cloud & Azure" and M15 "Azure ile Uygulama Geliştirme" were moved here; the level modules were renumbered). Same project rules as everywhere (module = one project, topic = folder), with these differences:

```
06-certifications/01-az-900-azure-fundamentals/     # module C1 → project C01.AzureFundamentals
├── C01.AzureFundamentals.csproj                     # console: SLA/TCO calculators, SDK demos (Azurite…)
├── Program.cs  Topic.cs
├── 01-cloud-concepts/
│   ├── README.md          # theory — 8-heading template, but heading 5 is "Sınav perspektifi" (what the exam asks, traps, keywords)
│   ├── <code>             # labs; `az` CLI labs are code blocks inside README (no scripts)
│   └── quiz.md            # exam-style questions; answers + rationale in <details>
└── 15-practice-exam/      # last topic: full-length practice exam, per-domain score table, exam strategy
```

- **`quiz.md` replaces `interview-questions.md`** (no interview-questions in this track). Question formats follow the real exam: single choice, multiple choice, yes/no table, ordering, scenario; for AI-200 also code/CLI/KQL completion and case studies. Every answer has a rationale. Distribute questions by the official domain weights.
- **Never reproduce real exam questions / braindumps** (NDA). Write original, exam-format questions. State the exam "skills measured as of …" date and tell the reader to re-check the official study guide.
- Labs must be **cheap and clean**: free tier/emulators where possible (Azurite, Cosmos DB emulator, Service Bus emulator via the module `docker-compose.yml`), always end with a cleanup step (`az group delete`) and mention a budget alarm.
- Still lean: no test projects, no scripts. The ROADMAP topic lines are `📝 **Quiz:**` (in addition to `🛠 **Kod:**`) and each cert module ends with a `📝 **Sınav:**` line (typical exam questions) instead of `🎯 **Mülakat:**`.
- Each topic lists its **📌 official objective** (which study-guide bullet it covers). AI-200 labs are written in C#, with an equivalent Python snippet in the README, because the exam audience profile expects Python.
- Fact-check Azure/cert claims (exam weights, retirements, service names) against the official study guide before writing; record sources in ROADMAP `## 2026 Radarı` / `Kaynaklar`.

## Writing a topic

When asked to implement a topic, derive everything from the ROADMAP rather than inventing it:

1. Locate the topic heading; take the topic folder name from the heading and the module path from the module's `📁` line. If the module project does not exist yet, create `J<NN>.<ModuleName>.csproj` (project type per module — see ROADMAP), `Program.cs` + `Topic.cs` (copy the pattern from J03), and add the project to `DotnetInterview.slnx`.
2. Create the topic folder with `README.md`, the code, and `interview-questions.md` (certification track: `quiz.md` instead — see above). **No `src/`, no `tests/`** (tests only if the topic is about testing).
3. Write `README.md` from the topic's bullets — they are effectively its outline — using the 8-heading template above, in Turkish. Command-line exercises go in as code blocks (no scripts).
4. Write the code to the scope in the `🛠 **Kod:**` line, no more and no less. Many of those lines ask for a *deliberately broken* case to diagnose (bad probe, wrong env var, OOM) — implement that too, it is the point of the exercise. Console topics follow the section pattern: `Sections/SectionNN_*.cs` (`Run()`), `Sections/Sections.cs` (list), `<Topic>Topic.cs` (`Run(args)`: list / `<n>` / `all`), registered in the module `Program.cs`.
5. Put the module's `🎯` questions plus topic-specific ones into `interview-questions.md`, with answers.
6. If the topic needs infra, extend the module's existing `docker-compose.yml`; don't add a per-topic compose file.
7. Cross-reference instead of duplicating: if a sub-topic belongs to a deeper level, link it as `(→ M6.3)`.
8. Build the whole solution (`dotnet build DotnetInterview.slnx`) and run the new topic (`dotnet run -- <id> all`) before committing; update the "Content written so far" sentence at the top of this file.

## Editing ROADMAP.md — consistency invariants

When adding, removing, renaming, or renumbering a module or topic, update **all** of these together:

1. `## Genel Harita` tables (module ID, anchor link, name, focus) and the `## İçindekiler` table if a top-level section changes
2. The module/topic heading itself (ID, name, folder name) and any `(→ ID)` cross-references to it
3. `## Repo Yapısı` tree (module-level folders only) and folder numbers
4. `## Çalışma Planı` weekly tables (module + parallel DSA column); keep each level intro's `Süre: ~N ay` consistent with the sum of module `⏱` estimates
5. `## 2026 Radarı` table's "Roadmap'te" column if the moved topic is referenced there
6. `README.md`: the level table, its links into ROADMAP anchors, and — if the folder convention or the theory/practice split changes — the "Klasör yapısı" and "Teorik ve pratik anlatımlar nerede?" sections, which must stay consistent with this file (and with ROADMAP's "Her modül klasörünün şablonu" / "Repo Yapısı"); also add new module projects to `DotnetInterview.slnx`
7. The `Güncelleme` date in the header and the `Son güncelleme` footer

**Anchor rules (GitHub slugs):** linked headings are lowercased, punctuation (`.`, `&`, `:`, `/`, `'`, `—`, `#`) is dropped, spaces become `-` — so `### J5. Build & Run Temelleri` → `#j5-build--run-temelleri`. **Never start a word with capital `İ` in a linked heading** (module headings, level headings, `## 2026 Radarı`): it lowercases to `i̇` (i + combining dot) and breaks the anchor. Rephrase instead (e.g. "API Tasarımı & Protokoller", "CLR & Runtime Derinlemesine").

**Freshness:** version/licensing/product-name claims (Agent Framework, Microsoft Foundry, MediatR/AutoMapper/MassTransit licenses, Azure cert names, PostgreSQL/Redis/Keycloak versions) are summarized in `## 2026 Radarı` with source links. Verify with a web search before changing them, and add sources there.

## Commits

Conventional-commit style with a `docs:` prefix for content changes, a short subject, and a body that summarizes per-level/per-module what changed.
