# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this repo is

A .NET Junior → Mid → Senior interview-prep study repo. **All content is written in Turkish** — keep new prose in Turkish (technical terms and identifiers stay in English, as in the existing text). Target platform is **.NET 10 (LTS) / C# 14**; .NET 11 / C# 15 features may be mentioned but must be marked **(.NET 11)**.

The repo started **docs-only** (`ROADMAP.md`, `README.md`, `CLAUDE.md`) and is being filled in one topic at a time. Content written so far: **J1 (`01-junior/01-dev-foundations/`) — all 5 topics** (theory README, runnable `src/`, `interview-questions.md`). The remaining `01-junior/` … `05-interview/` folders described in `ROADMAP.md → Repo Yapısı` are created as their topics are written. Root `Directory.Build.props` and `Directory.Packages.props` (central package management) already exist; the SDK is .NET 10 (`dotnet` CLI is needed to build/run the examples).

| File | Role |
|------|------|
| `ROADMAP.md` | ~4.2k lines, the master plan: 5 tracks, 51 modules, ~290 topics. Each topic's theory outline and the scope of its code project live here. |
| `README.md` | For the human studying: how to work through it, folder layout, where theory vs. practice lives, how to author a new topic. |
| `CLAUDE.md` | This file. |
| `01-junior/…` | Written topic folders (README + `src/` + `interview-questions.md`). |

When adding content, the usual task is "write topic X" — see [Writing a topic](#writing-a-topic).

## Roadmap structure: level → module → topic

`ROADMAP.md` is organized by seniority, and the folder layout mirrors it exactly:

| Level | ID prefix | Folder | Notes |
|-------|-----------|--------|-------|
| Junior | `J1`–`J14` | `01-junior/` | ends with `capstone/` |
| Mid | `M1`–`M16` | `02-mid/` | ends with `capstone/` |
| Senior | `S1`–`S10` | `03-senior/` | ends with `capstone/` |
| DSA track | `D1`–`D11` | `04-dsa/` | separate section, studied in parallel with levels |
| Interview | `I1`–`I6` | `05-interview/` | |

- A **module** (`### J8. Veritabanı Temelleri`) = a folder (`01-junior/08-database-fundamentals/`). Its header blockquote lists folder, duration (`⏱`), prerequisites; infra-heavy modules mention a shared module-level `docker-compose.yml`.
- A **topic** (`#### J8.4 Transaction & ACID · \`04-transactions-acid\``) = a subfolder and one independent code project. The folder name is part of the heading.
- Every topic ends with a `- 🛠 **Kod:** ...` line describing its future `src/` project; every J/M/S module ends with a `🎯 **Mülakat:** ...` line. `⭐` marks frequently-asked topics.
- The same subject recurs at deeper levels (e.g. DB: J8 → M6/M8 → S4). Cross-references use IDs in plain text: `(→ M6.3)`. Don't duplicate content across levels — put each sub-topic at the level where it's typically asked and cross-reference.

## Folder layout and where content lives

Folder names are **never invented** — they are written in the ROADMAP headings. A topic heading like

```markdown
#### J8.4 Transaction & ACID · `04-transactions-acid` ⭐
```

combined with its module's `📁 01-junior/08-database-fundamentals/` line resolves to:

```
01-junior/08-database-fundamentals/        # module
├── docker-compose.yml                     # module-level shared infra (only where the module says so)
└── 04-transactions-acid/                  # topic
    ├── README.md                          # THEORY — follows the template in ROADMAP.md
    ├── src/                               # PRACTICE — standalone project, `dotnet run`
    ├── tests/                             # optional
    └── interview-questions.md             # 10-20 Q&A
```

Where each kind of content belongs — keep this split, don't blur it:

| Content | Location |
|---------|----------|
| What to learn (outline) | ROADMAP topic bullets; `⭐` = frequently asked |
| Long-form theory | topic `README.md`, template: *Ne? / Neden? / Nasıl? / Gerçek dünya senaryosu / Mülakat perspektifi / Yaygın hatalar / Performans & trade-off / Kaynaklar* |
| What to build | the topic's `🛠 **Kod:**` line — `src/` implements exactly that scope |
| Running code | topic `src/` (+ `tests/`) |
| Interview Q&A | module's `🎯 **Mülakat:**` line + topic `interview-questions.md` |
| Infra | module-level `docker-compose.yml` |

Conventions: root `Directory.Build.props` + `Directory.Packages.props` (central package management); namespaces derived from folders (`Junior.DatabaseFundamentals.TransactionsAcid`); each topic is **self-contained — no cross-topic project references**; capstones have their own `.slnx`.

```bash
cd <level>/<module> && docker compose up -d   # only for modules with shared infra
cd <topic>/src && dotnet run
```

## Writing a topic

When asked to implement a topic, derive everything from the ROADMAP rather than inventing it:

1. Locate the topic heading; take the topic folder name from the heading and the module path from the module's `📁` line.
2. Create `README.md`, `src/`, `interview-questions.md` (and `tests/` if the topic warrants it).
3. Write `README.md` from the topic's bullets — they are effectively its outline — using the 8-heading template above, in Turkish.
4. Write `src/` to the scope in the `🛠 **Kod:**` line, no more and no less. Many of those lines ask for a *deliberately broken* case to diagnose (bad probe, wrong env var, OOM) — implement that too, it is the point of the exercise.
5. Put the module's `🎯` questions plus topic-specific ones into `interview-questions.md`, with answers.
6. If the topic needs infra, extend the module's existing `docker-compose.yml`; don't add a per-topic compose file.
7. Cross-reference instead of duplicating: if a sub-topic belongs to a deeper level, link it as `(→ M6.3)`.

## Editing ROADMAP.md — consistency invariants

When adding, removing, renaming, or renumbering a module or topic, update **all** of these together:

1. `## Genel Harita` tables (module ID, anchor link, name, focus) and the `## İçindekiler` table if a top-level section changes
2. The module/topic heading itself (ID, name, folder name) and any `(→ ID)` cross-references to it
3. `## Repo Yapısı` tree (module-level folders only) and folder numbers
4. `## Çalışma Planı` weekly tables (module + parallel DSA column); keep each level intro's `Süre: ~N ay` consistent with the sum of module `⏱` estimates
5. `## 2026 Radarı` table's "Roadmap'te" column if the moved topic is referenced there
6. `README.md`: the level table, its links into ROADMAP anchors, and — if the folder convention or the theory/practice split changes — the "Klasör yapısı" and "Teorik ve pratik anlatımlar nerede?" sections, which must stay consistent with this file
7. The `Güncelleme` date in the header and the `Son güncelleme` footer

**Anchor rules (GitHub slugs):** linked headings are lowercased, punctuation (`.`, `&`, `:`, `/`, `'`, `—`, `#`) is dropped, spaces become `-` — so `### J5. Build & Run Temelleri` → `#j5-build--run-temelleri`. **Never start a word with capital `İ` in a linked heading** (module headings, level headings, `## 2026 Radarı`): it lowercases to `i̇` (i + combining dot) and breaks the anchor. Rephrase instead (e.g. "API Tasarımı & Protokoller", "CLR & Runtime Derinlemesine").

**Freshness:** version/licensing/product-name claims (Agent Framework, Microsoft Foundry, MediatR/AutoMapper/MassTransit licenses, Azure cert names, PostgreSQL/Redis/Keycloak versions) are summarized in `## 2026 Radarı` with source links. Verify with a web search before changing them, and add sources there.

## Commits

Conventional-commit style with a `docs:` prefix for content changes, a short subject, and a body that summarizes per-level/per-module what changed.
