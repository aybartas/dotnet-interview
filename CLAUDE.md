# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this repo is

A .NET Junior → Mid → Senior interview-prep study repo. **All content is written in Turkish** — keep new prose in Turkish (technical terms and identifiers stay in English, as in the existing text). Target platform is **.NET 10 (LTS) / C# 14**; .NET 11 / C# 15 features may be mentioned but must be marked **(.NET 11)**.

Currently the repo is docs-only: `README.md` (overview) and `ROADMAP.md` (~3.8k lines, the master plan). The folders described in `ROADMAP.md → Repo Yapısı` do not exist yet; they are created as topics get written.

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

## Planned topic layout (for when code is added)

```
<level>/<NN-module>/<NN-topic>/
├── README.md               # follows the template in ROADMAP.md (Ne?/Neden?/Nasıl?/...)
├── src/                    # standalone project, runs with `dotnet run`; no cross-topic project refs
├── tests/                  # optional
└── interview-questions.md  # 10-20 Q&A
```

```bash
cd <level>/<module> && docker compose up -d   # only for modules with shared infra
cd <topic>/src && dotnet run
```

Recommended conventions (from `ROADMAP.md → Repo Yapısı`): root `Directory.Build.props` + `Directory.Packages.props` (central package management), namespaces derived from folders (`Junior.DatabaseFundamentals.TransactionsAcid`), capstones have their own `.slnx`.

## Editing ROADMAP.md — consistency invariants

When adding, removing, renaming, or renumbering a module or topic, update **all** of these together:

1. `## Genel Harita` tables (module ID, anchor link, name, focus) and the `## İçindekiler` table if a top-level section changes
2. The module/topic heading itself (ID, name, folder name) and any `(→ ID)` cross-references to it
3. `## Repo Yapısı` tree (module-level folders only) and folder numbers
4. `## Çalışma Planı` weekly tables (module + parallel DSA column); keep each level intro's `Süre: ~N ay` consistent with the sum of module `⏱` estimates
5. `## 2026 Radarı` table's "Roadmap'te" column if the moved topic is referenced there
6. `README.md` level table and its links into ROADMAP anchors
7. The `Güncelleme` date in the header and the `Son güncelleme` footer

**Anchor rules (GitHub slugs):** linked headings are lowercased, punctuation (`.`, `&`, `:`, `/`, `'`, `—`, `#`) is dropped, spaces become `-` — so `### J5. Build & Run Temelleri` → `#j5-build--run-temelleri`. **Never start a word with capital `İ` in a linked heading** (module headings, level headings, `## 2026 Radarı`): it lowercases to `i̇` (i + combining dot) and breaks the anchor. Rephrase instead (e.g. "API Tasarımı & Protokoller", "CLR & Runtime Derinlemesine").

**Freshness:** version/licensing/product-name claims (Agent Framework, Microsoft Foundry, MediatR/AutoMapper/MassTransit licenses, Azure cert names, PostgreSQL/Redis/Keycloak versions) are summarized in `## 2026 Radarı` with source links. Verify with a web search before changing them, and add sources there.

## Commits

Conventional-commit style with a `docs:` prefix for content changes, a short subject, and a body that summarizes per-level/per-module what changed.
