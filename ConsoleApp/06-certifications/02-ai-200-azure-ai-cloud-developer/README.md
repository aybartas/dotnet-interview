# 🎓 AI-200: Azure AI Cloud Developer Associate — Kapsamlı Çalışma Dokümanı

> **Son Güncelleme:** Ekim 2026 · **Resmî Sınav Hedefleri:** [Skills measured as of October 7, 2026](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
> **Sınav Formatı:** 40-60 soru · 100-120 dakika · Geçme puanı: **700 / 1000** · Soru tipleri: Vaka çalışması, kod/CLI/KQL tamamlama, tek seçim, çoklu seçim, evet/hayır, sürükle-bırak
>
> Bu doküman, AI-200 sınavının 14 konusunun tüm teorik anlatımlarını, mimari şemalarını, SDK/kod örneklerini, KQL/CLI komutlarını, performans/trade-off analizlerini ve 260'a yakın özgün sınav formatında quiz ve deneme sınavı sorusunu (2 vaka çalışması dahil) tek bir kaynakta birleştirmektedir.

---

## 📋 Sınav Alanları ve Ağırlıkları

| Sınav Alanı (Domain) | Ağırlık | Bölümler / Konular |
|---|---|---|
| **Develop containerized solutions on Azure** | %20-25 | Bölüm 1 (C2.1 ACR, C2.2 Container Apps, C2.3 AKS) |
| **Develop AI solutions by using Azure data management services** | **%30-35** | Bölüm 2 (C2.4 Cosmos NoSQL, C2.5 Vector/Change Feed, C2.6 PostgreSQL/pgvector, C2.7 Managed Redis) |
| **Connect to and consume Azure services** | %20-25 | Bölüm 3 (C2.8 Functions, C2.9 Service Bus, C2.10 Event Grid) |
| **Secure, monitor, and troubleshoot Azure solutions** | %20-25 | Bölüm 4 (C2.11 Managed Identity/Key Vault, C2.12 App Configuration, C2.13 OpenTelemetry/KQL) |
| **Deneme Sınavı & Strateji** | 50 Soru | Bölüm 5 (5.1, 5.2 - 2 Vaka Çalışması Dahil, 5.3) |

---

## 📑 Kapsamlı İçindekiler

- **BÖLÜM 1: Develop containerized solutions on Azure (%20-25)**
  - [1.1 Azure Container Registry ve ACR Tasks](./1.1-container-registry-acr-tasks.md)
    - [1.1 Quiz Soruları](./1.1-container-registry-acr-tasks.md#-bölüm-11-quiz-soruları)
  - [1.2 Azure Container Apps: Ortam, Revision ve KEDA](./1.2-container-apps-keda.md)
    - [1.2 Quiz Soruları](./1.2-container-apps-keda.md#-bölüm-12-quiz-soruları)
  - [1.3 AKS'e Manifest ile Dağıtım ve Container Teşhisi](./1.3-aks-manifests-diagnostics.md)
    - [1.3 Quiz Soruları](./1.3-aks-manifests-diagnostics.md#-bölüm-13-quiz-soruları)
- **BÖLÜM 2: Develop AI solutions by using Azure data management services (%30-35)**
  - [2.1 Cosmos DB for NoSQL: SDK, Partitioning, Indexing, Consistency, RU](./2.1-cosmos-db-nosql.md)
    - [2.1 Quiz Soruları](./2.1-cosmos-db-nosql.md#-bölüm-21-quiz-soruları)
  - [2.2 Cosmos DB ile Vector Search ve Change Feed](./2.2-cosmos-vector-change-feed.md)
    - [2.2 Quiz Soruları](./2.2-cosmos-vector-change-feed.md#-bölüm-22-quiz-soruları)
  - [2.3 Azure Database for PostgreSQL ve pgvector](./2.3-postgresql-pgvector.md)
    - [2.3 Quiz Soruları](./2.3-postgresql-pgvector.md#-bölüm-23-quiz-soruları)
  - [2.4 Azure Managed Redis: Cache ve Vector Index](./2.4-managed-redis.md)
    - [2.4 Quiz Soruları](./2.4-managed-redis.md#-bölüm-24-quiz-soruları)
- **BÖLÜM 3: Connect to and consume Azure services (%20-25)**
  - [3.1 Azure Functions: Serverless API, Trigger ve Binding](./3.1-azure-functions.md)
    - [3.1 Quiz Soruları](./3.1-azure-functions.md#-bölüm-31-quiz-soruları)
  - [3.2 Azure Service Bus: Kuyruk, Topic ve Dead-letter](./3.2-service-bus.md)
    - [3.2 Quiz Soruları](./3.2-service-bus.md#-bölüm-32-quiz-soruları)
  - [3.3 Azure Event Grid: Custom Event, Filtre ve Retry](./3.3-event-grid.md)
    - [3.3 Quiz Soruları](./3.3-event-grid.md#-bölüm-33-quiz-soruları)
- **BÖLÜM 4: Secure, monitor, and troubleshoot Azure solutions (%20-25)**
  - [4.1 Managed Identity ve Azure Key Vault](./4.1-managed-identity-key-vault.md)
    - [4.1 Quiz Soruları](./4.1-managed-identity-key-vault.md#-bölüm-41-quiz-soruları)
  - [4.2 Azure App Configuration](./4.2-app-configuration.md)
    - [4.2 Quiz Soruları](./4.2-app-configuration.md#-bölüm-42-quiz-soruları)
  - [4.3 OpenTelemetry ile İzleme ve KQL ile Sorun Giderme](./4.3-opentelemetry-kql.md)
    - [4.3 Quiz Soruları](./4.3-opentelemetry-kql.md#-bölüm-43-quiz-soruları)
- **BÖLÜM 5: AI-200 Deneme Sınavı ve Sınav Stratejisi**
  - [5.0 Kapsamlı AI-200 Deneme Sınavı (50 Soru, 2 Vaka Çalışması) ve Sınav Stratejisi](./5.1-exam-strategy.md)
    - [5.1 Sınav Stratejisi ve Hazırlık Rehberi](./5.1-exam-strategy.md#51-sınav-stratejisi-ve-hazırlık-rehberi)
    - [5.2 50 Soruluk AI-200 Deneme Sınavı](./5.2-practice-exam.md)
    - [5.3 Puanlama ve Zayıf Alan Analizi](./5.3-scoring-and-tactics.md)
