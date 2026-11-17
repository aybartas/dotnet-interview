# .NET Backend Roadmap: Junior → Mid → Senior

> Target: .NET 10 LTS / C# 14. Items marked **(.NET 11)** are from .NET 11 / C# 15. ⭐ = asked often in interviews.

## How we proceed

1. Go **Junior → Mid → Senior**, stage by stage. Each stage mixes subjects (C#, web, data, networking, ops …), so you never finish all of C# before touching SQL.
2. Each level folder has a `README.md` that lists its topics and its Shop project tasks.
3. Every topic is a folder inside its level, e.g. `01-junior/J1-how-code-runs/J1.1-dotnet-platform-cli/`, containing:
   - `README.md`: theory with C# examples
   - `quiz.md`: 12–15 questions (multiple choice, *what does this print?*, *find the bug*, scenarios). Answers are hidden in `<details>`. Pass mark is 80%; the last 2 questions revisit an earlier topic.
   - code only if the topic needs it: a single-file app (`dotnet run demo.cs`), and a full project only when one file isn't enough
4. After each stage, do that stage's Shop tasks.

## Levels

| Level | Goal and project |
|---|---|
| [🟢 Junior](01-junior/README.md) | Ship a tested CRUD API on your own · **Shop v1:** a monolith API with PostgreSQL + EF Core, JWT, tests, Docker Compose and CI |
| [🟡 Mid](02-mid/README.md) | Run a production service with reliable messaging · **Shop v2:** a modular monolith with Redis, RabbitMQ + outbox, Keycloak, OpenTelemetry, Aspire and Kubernetes |
| [🔴 Senior](03-senior/README.md) | Design, run and evolve distributed systems · **Shop v3:** microservices with a YARP gateway, sagas, Kafka, GitOps on Azure, SLOs and chaos tests |

Shop is an e-commerce backend that lives in its own repo and grows with each level.

## 🎤 Interview (in parallel, ~1 h/week)

- **I1** Live coding & DSA: hash maps, two pointers, sliding window, BFS/DFS, heaps, `PriorityQueue`
- **I2** .NET question bank: every ⭐ topic as a rapid-fire question, with strong vs weak answers
- **I3** System design interviews: the 45-minute structure, estimation, trade-off language, mock prompts
- **I4** Behavioral & mocks: STAR story bank, senior signals, take-homes, mock interview checklists

## 📜 Certifications (separate track)

- [AZ-900: Azure Fundamentals](certifications/01-az-900-azure-fundamentals/README.md)
- [AI-200: Azure AI Cloud Developer](certifications/02-ai-200-azure-ai-cloud-developer/README.md)
