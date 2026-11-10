# 🎓 AI-200: Azure AI Cloud Developer Associate — Kapsamlı Çalışma Dokümanı

> **Son Güncelleme:** Ekim 2026 · **Resmî Sınav Hedefleri:** [Skills measured as of October 7, 2026](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
> **Sınav Formatı:** 40-60 soru · 100-120 dakika · Geçme puanı: **700 / 1000** · Soru tipleri: Vaka çalışması, kod/CLI/KQL tamamlama, tek seçim, çoklu seçim, evet/hayır, sürükle-bırak
>
> Bu doküman, AI-200 sınavının 14 konusunun tüm teorik anlatımlarını, mimari şemalarını, SDK/kod örneklerini, KQL/CLI komutlarını, performans/trade-off analizlerini ve 260'a yakın özgün sınav formatında quiz ve deneme sınavı sorusunu (2 vaka çalışması dahil) tek bir kaynakta birleştirmektedir.

> 📱 **Nasıl okunur?** Her konu kendi klasöründe: `README.md` konu anlatımı, `quiz.md` gerekçeli sorular. Her sayfanın başında ve sonunda *İçindekiler / Quiz / Sonraki konu* linkleri var; telefonda konudan konuya kaydırmadan geçebilirsin.

---

## 📋 Sınav Alanları ve Ağırlıkları

- **Develop containerized solutions on Azure**
  - Ağırlık: %20-25
  - Bölümler / Konular: Bölüm 1 (C2.1 ACR, C2.2 Container Apps, C2.3 AKS)
- **Develop AI solutions by using Azure data management services**
  - Ağırlık: **%30-35**
  - Bölümler / Konular: Bölüm 2 (C2.4 Cosmos NoSQL, C2.5 Vector/Change Feed, C2.6 PostgreSQL/pgvector, C2.7 Managed Redis)
- **Connect to and consume Azure services**
  - Ağırlık: %20-25
  - Bölümler / Konular: Bölüm 3 (C2.8 Functions, C2.9 Service Bus, C2.10 Event Grid)
- **Secure, monitor, and troubleshoot Azure solutions**
  - Ağırlık: %20-25
  - Bölümler / Konular: Bölüm 4 (C2.11 Managed Identity/Key Vault, C2.12 App Configuration, C2.13 OpenTelemetry/KQL)
- **Deneme Sınavı & Strateji**
  - Ağırlık: 50 Soru
  - Bölümler / Konular: Bölüm 5 (5.1, 5.2 - 2 Vaka Çalışması Dahil, 5.3)

---

## 📑 Kapsamlı İçindekiler

- **BÖLÜM 1: Develop containerized solutions on Azure (%20-25)**
  - [1.1 Azure Container Registry ve ACR Tasks](1.1-container-registry-acr-tasks/README.md)
    - [1.1 Quiz Soruları](1.1-container-registry-acr-tasks/quiz.md)
  - [1.2 Azure Container Apps: Ortam, Revision ve KEDA](1.2-container-apps-keda/README.md)
    - [1.2 Quiz Soruları](1.2-container-apps-keda/quiz.md)
  - [1.3 AKS'e Manifest ile Dağıtım ve Container Teşhisi](1.3-aks-manifests-diagnostics/README.md)
    - [1.3 Quiz Soruları](1.3-aks-manifests-diagnostics/quiz.md)
- **BÖLÜM 2: Develop AI solutions by using Azure data management services (%30-35)**
  - [2.1 Cosmos DB for NoSQL: SDK, Partitioning, Indexing, Consistency, RU](2.1-cosmos-db-nosql/README.md)
    - [2.1 Quiz Soruları](2.1-cosmos-db-nosql/quiz.md)
  - [2.2 Cosmos DB ile Vector Search ve Change Feed](2.2-cosmos-vector-change-feed/README.md)
    - [2.2 Quiz Soruları](2.2-cosmos-vector-change-feed/quiz.md)
  - [2.3 Azure Database for PostgreSQL ve pgvector](2.3-postgresql-pgvector/README.md)
    - [2.3 Quiz Soruları](2.3-postgresql-pgvector/quiz.md)
  - [2.4 Azure Managed Redis: Cache ve Vector Index](2.4-managed-redis/README.md)
    - [2.4 Quiz Soruları](2.4-managed-redis/quiz.md)
- **BÖLÜM 3: Connect to and consume Azure services (%20-25)**
  - [3.1 Azure Functions: Serverless API, Trigger ve Binding](3.1-azure-functions/README.md)
    - [3.1 Quiz Soruları](3.1-azure-functions/quiz.md)
  - [3.2 Azure Service Bus: Kuyruk, Topic ve Dead-letter](3.2-service-bus/README.md)
    - [3.2 Quiz Soruları](3.2-service-bus/quiz.md)
  - [3.3 Azure Event Grid: Custom Event, Filtre ve Retry](3.3-event-grid/README.md)
    - [3.3 Quiz Soruları](3.3-event-grid/quiz.md)
- **BÖLÜM 4: Secure, monitor, and troubleshoot Azure solutions (%20-25)**
  - [4.1 Managed Identity ve Azure Key Vault](4.1-managed-identity-key-vault/README.md)
    - [4.1 Quiz Soruları](4.1-managed-identity-key-vault/quiz.md)
  - [4.2 Azure App Configuration](4.2-app-configuration/README.md)
    - [4.2 Quiz Soruları](4.2-app-configuration/quiz.md)
  - [4.3 OpenTelemetry ile İzleme ve KQL ile Sorun Giderme](4.3-opentelemetry-kql/README.md)
    - [4.3 Quiz Soruları](4.3-opentelemetry-kql/quiz.md)
- **BÖLÜM 5: AI-200 Deneme Sınavı ve Sınav Stratejisi**
  - [5.0 Kapsamlı AI-200 Deneme Sınavı (50 Soru, 2 Vaka Çalışması) ve Sınav Stratejisi](5-practice-exam/README.md)
    - [5.1 Sınav Stratejisi ve Hazırlık Rehberi](5-practice-exam/README.md#51-sınav-stratejisi-ve-hazırlık-rehberi)
    - [5.2 50 Soruluk AI-200 Deneme Sınavı](5-practice-exam/quiz.md)
    - [5.3 Puanlama ve Zayıf Alan Analizi](5-practice-exam/quiz.md#puan-tablosu)
