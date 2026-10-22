# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this repo is

A .NET Junior → Mid → Senior interview-prep study repo. Target platform is **.NET 10 (LTS) / C# 14**; .NET 11 / C# 15 features may be mentioned but must be marked **(.NET 11)**.

The repo started **docs-only** (`ROADMAP.md`, `README.md`, `CLAUDE.md`) and is being filled in one topic at a time. Content written so far: **J1 — all 5 topics**, **J2 — all 6**, **J3 — all 3**, **J4 — all 3**, **J5 — all 3** (each topic: theory `README.md`, runnable code, `interview-questions.md`); certification track: **C1 (AZ-900) — all 15 topics C1.1–C1.15** (each topic: `README.md` + `quiz.md`; code only where it helps — C1.1–C1.3, C1.8, C1.11). The remaining `02-mid/` … `05-interview/` folders described in `ROADMAP.md → Repo Yapısı` are created as their topics are written.

**The whole repo is one solution — `DotnetInterview.slnx` — with exactly two projects: `ConsoleApp` and `WebApp`.** No `Directory.Build.props`, `Directory.Packages.props`, `global.json`, `docker-compose.yml`, NuGet packages, per-module projects or class libraries. Simplicity is the goal: F5 in Visual Studio opens an interactive topic menu in `ConsoleApp`. The SDK is .NET 10 (`dotnet` CLI is needed to build/run the examples).

| File / folder | Role |
|---------------|------|
| `ROADMAP.md` | ~4.3k lines, the master plan: 6 tracks, 51 modules, ~300 topics. Each topic's theory outline and the scope of its code live here. |
| `README.md` | For the human studying: how to work through it, folder layout, where theory vs. practice lives. |
| `CLAUDE.md` | This file. |
| `DotnetInterview.slnx` | The single solution (`ConsoleApp` + `WebApp`). |
| `ConsoleApp/` | The one console project: every console topic (code + README + questions). |
| `WebApp/` | The one web project: topics that need an HTTP server (J1.3, J6, J7, M3, M5 …). |

When adding content, the usual task is "write topic X" — see [Writing a topic](#writing-a-topic).

## Language policy

- **Turkish — explanations only:** the prose of topic `README.md` files (theory paragraphs, bullets, explanatory table text), `interview-questions.md` / `quiz.md` (questions, answers, rationales), and the planning prose in `ROADMAP.md` and the root `README.md`. Technical terms stay in English inside Turkish sentences.
- **English — everything else:** C# identifiers, comments and XML docs, strings printed by demos (section titles, menu/help text, sample data labels), command-line words and arguments, file/folder/namespace names, `.csproj`/json/http/config contents, **code blocks inside Markdown** (comments, strings, sample output), `CLAUDE.md`, and **commit messages / PR descriptions**.
- Exception: data that *is* the lesson stays as is (Turkish culture/text demos: tr-TR, the Turkish İ/ı problem, Turkish city names for sorting). Demo output must be culture-independent English (explicit `CultureInfo.InvariantCulture`, e.g. `99.95%`, `21 min 53 s`, `$` for example prices) unless a topic is about culture.

## Roadmap structure: level → module → topic

`ROADMAP.md` is organized by seniority, and the folder layout mirrors it exactly:

| Level | ID prefix | Folder (inside `ConsoleApp/` or `WebApp/`) | Notes |
|-------|-----------|--------------------------------------------|-------|
| Junior | `J1`–`J13` | `01-junior/` | ends with `capstone/` |
| Mid | `M1`–`M15` | `02-mid/` | ends with `capstone/` |
| Senior | `S1`–`S10` | `03-senior/` | ends with `capstone/` |
| DSA track | `D1`–`D11` | `04-dsa/` | separate section, studied in parallel with levels |
| Interview | `I1`–`I6` | `05-interview/` | |
| Certification track | `C1`–`C2` | `06-certifications/` | **Level-independent**: C1 = AZ-900, C2 = AI-200 (Azure). Exam-oriented: theory + labs + exam-style quizzes (see [Certification track](#certification-track-06-certifications)) |

- A **module** (`### J8. Veritabanı Temelleri`) = a folder (`01-junior/08-database-fundamentals/`). Its header blockquote lists folder (`📁`), duration (`⏱`), prerequisites; infra-heavy modules (J8 …) will need a root `docker-compose.yml`, added when the first such topic is written. **`📁` paths in ROADMAP are relative to the app folder**: console topics live under `ConsoleApp/`, web topics under `WebApp/` with the same path.
- A **topic** (`#### J8.4 Transaction & ACID · \`04-transactions-acid\``) = a subfolder. The folder name is part of the heading.
- Every topic ends with a `- 🛠 **Kod:** ...` line describing the code its topic folder will contain; every J/M/S module ends with a `🎯 **Mülakat:** ...` line. `⭐` marks frequently-asked topics.
- The same subject recurs at deeper levels (e.g. DB: J8 → M6/M8 → S4). Cross-references use IDs in plain text: `(→ M6.3)`. Don't duplicate content across levels — put each sub-topic at the level where it's typically asked and cross-reference.

## Folder layout and where content lives

**One repo = one console app + one web app; one topic = one folder inside the app that runs it.** There are no per-module or per-topic projects and no `src/` or `tests/` folders. Folder names are **never invented** — they are written in the ROADMAP headings. A topic heading like

```markdown
#### J3.2 Delegates, Lambdas & Events · `02-delegates-lambdas-events`
```

combined with its module's `📁 01-junior/03-collections-linq/` line resolves to:

```
ConsoleApp/
├── ConsoleApp.csproj
├── Program.cs                                      # topic menu for ALL console topics: dotnet run -- J3.2 [section|all]
├── Properties/launchSettings.json                  # J5.3 lesson
└── 01-junior/03-collections-linq/02-delegates-lambdas-events/   # topic = folder
    ├── README.md                                   # THEORY — follows the template in ROADMAP.md
    ├── DelegatesLambdasEventsTopic.cs              # topic entry point: Run(args) → section menu
    ├── Sections/ Section01_*.cs … Sections.cs      # runnable demos
    ├── Models/ …                                   # supporting types
    └── interview-questions.md                      # 10-20 Q&A
WebApp/
├── WebApp.csproj
├── Program.cs                                      # calls each web topic's AddServices(builder) / Map(app)
└── 01-junior/01-dev-foundations/03-how-the-web-works/   # web topic: README + endpoint code + requests.http + questions
```

`DotnetInterview.slnx` holds just the two projects; **never add a project for a new module or topic**.

Where each kind of content belongs — keep this split, don't blur it:

| Content | Location |
|---------|----------|
| What to learn (outline) | ROADMAP topic bullets; `⭐` = frequently asked |
| Long-form theory | topic `README.md`, template: *Ne? / Neden? / Nasıl? / Gerçek dünya senaryosu / Mülakat perspektifi / Yaygın hatalar / Performans & trade-off / Kaynaklar* |
| What to build | the topic's `🛠 **Kod:**` line — the topic folder's code implements exactly that scope |
| Running code | topic folder inside `ConsoleApp` (or `WebApp` if it needs an HTTP server) |
| Interview Q&A | module's `🎯 **Mülakat:**` line + topic `interview-questions.md` (certification track: `📝 **Sınav:**` line + topic `quiz.md`) |
| Infra | root `docker-compose.yml` (does not exist yet; add it with the first infra topic, e.g. J8) |

**Lean rules (do not add things the user did not ask for):**
- **No tests** unless the topic itself is about testing (J10, M10 …). Verification of other topics = the demo's printed output. Do not create `*.Tests` projects, `tests/` folders or `xunit` references elsewhere.
- **No shell/PowerShell scripts, `.vscode/`, `global.json`, `Directory.*.props`, `SOLUTIONS.md`** etc. Command-line exercises (git, curl, tcpdump, `dotnet new` …) are written as code blocks inside the topic `README.md`; exercise solutions go into a `<details>` block at the end of the README.
- **No module-level README or csproj** (the ROADMAP already describes the module).
- **Not every topic needs code**: README-only topics (theory + questions) are fine; add demos only where they teach something.
- **No extra projects.** Experiments that need a second assembly (e.g. `internal` across assemblies, `const` embedding) are shown in-assembly where possible and otherwise written as a README exercise (`dotnet new classlib` in an empty folder). Only capstones (layered architecture) may add projects.
- Code-free modules (Kubernetes, CI/CD, Azure IaC…) get README-only topic folders.

Conventions:
- Project settings live in the two `.csproj` files (`net10.0`, nullable, implicit usings; `ConsoleApp` also `InvariantGlobalization=false`, `GenerateDocumentationFile`, `EnforceCodeStyleInBuild` and the Kestrel `FrameworkReference` for J4's local API; no NuGet packages). Folder-scoped analyzer settings go into an `.editorconfig` inside the topic folder.
- **One namespace per topic** (`Junior.CollectionsLinq.LinqBasics`) — this is what keeps same-named demo types (`Order`, `Customer` …) from colliding inside one project; topics never use each other's types (module-level helpers shared by that module's topics go into a `Shared/` folder inside the module, e.g. J04's `LocalApi`, `SectionRunner`).
- **Topic IDs** are prefixed: `J1.2`, `M5.1`, `C1.8` (case-insensitive on the command line).
- Capstones are the only place for layered multi-project folders (added to the same solution).

```bash
dotnet build DotnetInterview.slnx                     # whole repo
cd ConsoleApp && dotnet run                           # interactive menu (F5 in Visual Studio)
cd ConsoleApp && dotnet run -- J3.2 [4|all]           # without the menu: one topic / one section / all sections
cd WebApp && dotnet run                               # web topics (http://localhost:5080)
```

Gotchas of the single console project:
- `GlobalUsings.cs` is **project-wide** (J02's `global using System.Diagnostics;` can make `Trace` ambiguous elsewhere — fix with a `using Trace = …` alias in the affected file); `ConsoleApp.csproj` properties apply to every topic.
- `ConsoleApp/Program.cs` is an interactive menu (topic → section number / `all` / topic-specific command, loop) and also accepts `dotnet run -- <id> <args>`. It is async (`return await topic.Run(...)`): `Topic` accepts either `Func<string[], int>` or `Func<string[], Task<int>>`; async topics return `Task<int>`, topics that call async code from a sync `Run` wrap `RunAsync(args).GetAwaiter().GetResult()`. A topic's `Run(string[] args)` receives the arguments *after* the topic id.
- Topics that re-launch themselves (J4.2 async-void) pass the prefixed id (`J4.2`); J5 locates the project directory by `ConsoleApp.csproj` (`ProjectLocator`).
- A type named like its own namespace (`Exercises`) must live in that namespace; type names must not clash with a project-wide global using (alias them).
- Culture: `Program.cs` sets tr-TR only for `C…` topics; other topics use the machine culture.
- Web topics: `WebApp/Program.cs` calls `Topic.AddServices(builder)` and `Topic.Map(app)`; J1.3 uses global middleware + `MapFallback`, so a second web topic must move to route groups.

## Certification track (`06-certifications/`)

The Azure certification content (**C1 = AZ-900**, **C2 = AI-200**) is its **own track**, separate from Junior/Mid/Senior (the former J13 "Cloud & Azure" and M15 "Azure ile Uygulama Geliştirme" were moved here; the level modules were renumbered). Same folder rules as everywhere (topic = folder inside `ConsoleApp`), with these differences:

```
ConsoleApp/06-certifications/01-az-900-azure-fundamentals/     # C1
├── Shared/                                          # Section/SectionRunner/Table helpers for the C1 topics
├── 01-cloud-computing/
│   ├── README.md          # theory — 8-heading template, but heading 5 is "Sınav perspektifi" (what the exam asks, traps, keywords)
│   ├── <code>             # labs; `az` CLI labs are code blocks inside README (no scripts)
│   └── quiz.md            # exam-style questions; answers + rationale in <details>
└── 15-practice-exam/      # last topic: full-length practice exam, per-domain score table, exam strategy
```

- **`quiz.md` replaces `interview-questions.md`** (no interview-questions in this track). Question formats follow the real exam: single choice, multiple choice, yes/no table, ordering, scenario; for AI-200 also code/CLI/KQL completion and case studies. Every answer has a rationale. Distribute questions by the official domain weights.
- **Never reproduce real exam questions / braindumps** (NDA). Write original, exam-format questions. State the exam "skills measured as of …" date and tell the reader to re-check the official study guide.
- Labs must be **cheap and clean**: free tier/emulators where possible (Azurite, Cosmos DB emulator, Service Bus emulator via a root `docker-compose.yml` when needed), always end with a cleanup step (`az group delete`) and mention a budget alarm.
- Still lean: no test projects, no scripts. The ROADMAP topic lines are `📝 **Quiz:**` (in addition to `🛠 **Kod:**`) and each cert module ends with a `📝 **Sınav:**` line (typical exam questions) instead of `🎯 **Mülakat:**`.
- Each topic lists its **📌 official objective** (which study-guide bullet it covers). AI-200 labs are written in C#, with an equivalent Python snippet in the README, because the exam audience profile expects Python.
- Fact-check Azure/cert claims (exam weights, retirements, service names) against the official study guide before writing; record sources in ROADMAP `## 2026 Radarı` / `Kaynaklar`.

## Writing a topic

When asked to implement a topic, derive everything from the ROADMAP rather than inventing it:

1. Locate the topic heading; take the topic folder name from the heading and the module path from the module's `📁` line. Create the folder under `ConsoleApp/` (or `WebApp/` if it needs an HTTP server). **Do not create a project.**
2. Create the topic folder with `README.md`, the code, and `interview-questions.md` (certification track: `quiz.md` instead — see above). **No `src/`, no `tests/`** (tests only if the topic is about testing).
3. Write `README.md` from the topic's bullets — they are effectively its outline — using the 8-heading template above; prose in Turkish, code blocks in English (see the language policy). Start it with `> 📁 \`ConsoleApp/<path>/\`` (or `WebApp/…`). Command-line exercises go in as code blocks (no scripts).
4. Write the code to the scope in the `🛠 **Kod:**` line, no more and no less. Many of those lines ask for a *deliberately broken* case to diagnose (bad probe, wrong env var, OOM) — implement that too, it is the point of the exercise. Console topics follow the section pattern: `Sections/SectionNN_*.cs` (`Run()`), `Sections/Sections.cs` (list), `<Topic>Topic.cs` (`Run(args)`: list / `<n>` / `all`), one namespace per topic. **Register the topic with one line in `ConsoleApp/Program.cs`** (`new("J8.2", "Title", SqlBasicsTopic.Run)`); a web topic exposes `AddServices`/`Map` and is called from `WebApp/Program.cs`.
5. Put the module's `🎯` questions plus topic-specific ones into `interview-questions.md`, with answers.
6. If the topic needs infra, add the service to the root `docker-compose.yml` (create it with the first infra topic); don't add a per-topic compose file.
7. Cross-reference instead of duplicating: if a sub-topic belongs to a deeper level, link it as `(→ M6.3)`.
8. Build the whole solution (`dotnet build DotnetInterview.slnx`) and run the new topic (`cd ConsoleApp && dotnet run -- <id> all`) before committing; update the "Content written so far" sentence at the top of this file. After renaming/moving files, check relative links in the Markdown files.

## Editing ROADMAP.md — consistency invariants

When adding, removing, renaming, or renumbering a module or topic, update **all** of these together:

1. `## Genel Harita` tables (module ID, anchor link, name, focus) and the `## İçindekiler` table if a top-level section changes
2. The module/topic heading itself (ID, name, folder name) and any `(→ ID)` cross-references to it
3. `## Repo Yapısı` tree (module-level folders only) and folder numbers
4. `## Çalışma Planı` weekly tables (module + parallel DSA column); keep each level intro's `Süre: ~N ay` consistent with the sum of module `⏱` estimates
5. `## 2026 Radarı` table's "Roadmap'te" column if the moved topic is referenced there
6. `README.md`: the level table, its links into ROADMAP anchors, and — if the folder convention or the theory/practice split changes — the "Klasör yapısı" and "Teorik ve pratik anlatımlar nerede?" sections, which must stay consistent with this file (and with ROADMAP's "Her konunun yeri" / "Repo Yapısı")
7. The `Güncelleme` date in the header and the `Son güncelleme` footer

**Anchor rules (GitHub slugs):** linked headings are lowercased, punctuation (`.`, `&`, `:`, `/`, `'`, `—`, `#`) is dropped, spaces become `-` — so `### J5. Build & Run Temelleri` → `#j5-build--run-temelleri`. **Never start a word with capital `İ` in a linked heading** (module headings, level headings, `## 2026 Radarı`): it lowercases to `i̇` (i + combining dot) and breaks the anchor. Rephrase instead (e.g. "API Tasarımı & Protokoller", "CLR & Runtime Derinlemesine").

**Freshness:** version/licensing/product-name claims (Agent Framework, Microsoft Foundry, MediatR/AutoMapper/MassTransit licenses, Azure cert names, PostgreSQL/Redis/Keycloak versions) are summarized in `## 2026 Radarı` with source links. Verify with a web search before changing them, and add sources there.

## Commits

Conventional-commit style, **in English**: `docs:` prefix for content changes (`refactor:` for structure/code changes), a short subject, and a body that summarizes per-level/per-module what changed.
