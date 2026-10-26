# CLAUDE.md

Guidance for Claude Code when working in this repository.

## What this repo is

A .NET Junior → Mid → Senior interview-prep study repo (target: **.NET 10 LTS / C# 14**; .NET 11 / C# 15 features only when marked **(.NET 11)**). `ROADMAP.md` is the master plan (6 tracks, 51 modules, ~300 topics); the repo is filled in one topic at a time.

Written so far: **J1–J6** (J1 5 topics, J2 6, J3 3, J4 3, J5 3, J6 6) and **C1 (AZ-900) — all 15 topics** (`README.md` + `quiz.md`; code only in C1.1–C1.3, C1.8, C1.11).

## Structure (keep it this simple)

```
DotnetInterview.slnx        # the one solution: exactly two projects
README.md  ROADMAP.md  CLAUDE.md  .gitignore
ConsoleApp/                 # console topics. F5 → interactive topic menu
├── ConsoleApp.csproj  Program.cs  Properties/launchSettings.json
└── 01-junior/<NN-module>/<NN-topic>/   (also 06-certifications/…)
    ├── README.md  interview-questions.md       # certification track: quiz.md
    └── <Topic>Topic.cs  Sections/  Models/ …   # code, only where it teaches something
WebApp/                     # topics that need an HTTP server; same folder shape, one topic runs at a time
├── WebApp.csproj  Program.cs
└── 01-junior/…
```

- **Never add a project, `Directory.*.props`, `global.json`, `docker-compose.yml` (until the first infra topic, J8), NuGet packages, scripts, `.vscode/`, test projects (unless the topic is about testing), module-level README/csproj, or `requests.http`-style extras.** Not every topic needs code: README-only topics are fine.
- Folder names come from the ROADMAP headings (`#### J3.2 … · \`02-delegates-lambdas-events\``) and the module's `📁` line; `📁` paths are relative to the app folder (`ConsoleApp/` or `WebApp/`).
- Topic IDs are prefixed: `J1.2`, `M5.1`, `C1.8`. One namespace per topic (`Junior.CollectionsLinq.LinqBasics`); topics never use each other's types; module-level helpers live in a `Shared/` folder inside the module.
- Settings live only in the two `.csproj` files; analyzer settings that concern one topic go in an `.editorconfig` inside that topic folder.

```bash
dotnet build DotnetInterview.slnx
cd ConsoleApp && dotnet run                 # menu (F5 in Visual Studio)   · dotnet run -- J3.2 [4|all]
cd WebApp && dotnet run                     # menu · dotnet run -- J6.2 [--environment Production]  (http://localhost:5080)
```

Gotchas: `GlobalUsings.cs` (J2) is project-wide — alias any type that clashes (e.g. `Trace`). `ConsoleApp/Program.cs` is async; `Topic` accepts `Func<string[], int>` or `Func<string[], Task<int>>`; a topic's `Run(string[] args)` receives the arguments after the topic id. A web topic's `Run` builds and runs its **own** host on `http://localhost:5080` and blocks until Ctrl+C (arguments are forwarded to the host), so topics may use global middleware, their own configuration and environment.

## Language policy

- **Turkish — explanations only:** prose of topic `README.md` files, `interview-questions.md` / `quiz.md`, and the prose of `ROADMAP.md` / root `README.md`. Technical terms stay English inside Turkish sentences.
- **English — everything else:** identifiers, comments, XML docs, strings printed by demos, commands and arguments, file/folder/namespace names, config files, code blocks inside Markdown (comments, strings, sample output), `CLAUDE.md`, commit messages and PR text.
- Exception: data that *is* the lesson (tr-TR culture demos, Turkish İ/ı, Turkish city names for sorting). Demo output is culture-independent (`CultureInfo.InvariantCulture`; `99.95%`, `$`).

## Writing a topic

1. Take the topic heading and the module's `📁` line from the ROADMAP; create the topic folder under `ConsoleApp/` (or `WebApp/` if it needs an HTTP server). No new project.
2. `README.md` from the ROADMAP bullets, 8-heading template (*Ne? / Neden? / Nasıl? / Gerçek dünya senaryosu / Mülakat perspektifi / Yaygın hatalar / Performans & trade-off / Kaynaklar*; certification track: heading 5 = *Sınav perspektifi*), starting with `> 📁 \`ConsoleApp/<path>/\``. Command-line exercises are code blocks (solutions in `<details>`).
3. Code only to the scope of the topic's `🛠 **Kod:**` line (implement deliberately broken cases too). Console: `<Topic>Topic.cs` (`Run(args)`: list / `<n>` / `all`) + `Sections/SectionNN_*.cs` + `Sections/Sections.cs`, registered with one line in `ConsoleApp/Program.cs`. Web: `public static int Run(string[] args)`, registered in `WebApp/Program.cs`.
4. `interview-questions.md` (10–20 Q&A with answers; module `🎯` questions included).
5. Cross-reference deeper levels instead of duplicating: `(→ M6.3)`.
6. Before committing: `dotnet build DotnetInterview.slnx`, run the topic (`cd ConsoleApp && dotnet run -- <id> all`), check relative Markdown links, update the "Written so far" line above.

## Certification track (`ConsoleApp/06-certifications/`)

C1 = AZ-900, C2 = AI-200 (level-independent). Each topic: `README.md` (📌 official objective, "skills measured as of …" date) + `quiz.md` (original exam-format questions, answers and rationale in `<details>`; **never reproduce real exam questions**). Labs are `az` CLI code blocks in the README, cheap, ending with a cleanup step and a budget-alarm note. Fact-check Azure claims against the official study guide; record sources in ROADMAP `## 2026 Radarı`.

## Editing ROADMAP.md

When adding/renaming/renumbering a module or topic, update together: `## Genel Harita` and `## İçindekiler`, the heading and `(→ ID)` cross-references, `## Repo Yapısı`, `## Çalışma Planı`, `## 2026 Radarı` (if referenced), the root `README.md` level table, and the `Güncelleme` / `Son güncelleme` dates. Anchors are GitHub slugs (lowercase, punctuation dropped, spaces → `-`); never start a word with capital `İ` in a linked heading. Version/licensing/product-name claims go into `## 2026 Radarı` with sources — verify with a web search first.

## Commits

Conventional commits **in English** (`docs:` for content, `refactor:` for structure/code), short subject, body summarizing what changed per level/module.
