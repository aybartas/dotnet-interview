# 🎓 AZ-900: Microsoft Azure Fundamentals — Kapsamlı Çalışma Dokümanı

> **Son Güncelleme:** Kasım 2026 · **Resmî Sınav Hedefleri:** [Skills measured as of July 20, 2026](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
> **Sınav Formatı:** 40-60 soru · 45-60 dakika · Geçme puanı: **700 / 1000** · Soru tipleri: Tek seçim, çoklu seçim, evet/hayır, eşleştirme, senaryo
>
> Bu doküman, AZ-900 müfredatının **14 konusunun** teorik anlatımlarını, karşılaştırma matrislerini, sınav ipuçlarını ve **300'ü aşkın** sınav formatında özgün quiz ve deneme sınavı sorusunu tek bir kaynakta birleştirir. **İçerik yalnızca resmî AZ-900 müfredatındaki konularla sınırlıdır**; müfredat dışı servisler (ör. yük dengeleme, AI/ML, DevOps, destek planları/SLA hesapları) kasıtlı olarak yer almaz.

> 📱 **Nasıl okunur?** Her konu kendi klasöründe: `README.md` konu anlatımı, `quiz.md` gerekçeli sorular. Her sayfanın başında ve sonunda *İçindekiler / Quiz / Sonraki konu* linkleri var; telefonda konudan konuya kaydırmadan geçebilirsin. Sınavdan önceki gün: [⚡ Hızlı Tekrar Kartı](5-practice-exam/cheat-sheet.md).

---

## 📋 Sınav Alanları ve Ağırlıkları

| Sınav Alanı (Domain) | Ağırlık | Bölümler |
|---|---|---|
| **Describe cloud concepts** | %25-30 | Bölüm 1 (1.1, 1.2, 1.3) |
| **Describe Azure architecture and services** | %35-40 | Bölüm 2 (2.1 - 2.7) |
| **Describe Azure management and governance** | %30-35 | Bölüm 3 (3.1 - 3.4) |
| **Deneme Sınavı & Strateji** | 50 Soru | Bölüm 4 (4.1, 4.2, 4.3) |

---

## 🗺️ Müfredat → Doküman Haritası

Resmî AZ-900 müfredatındaki her başlık aşağıdaki konulardan birinde anlatılır; müfredatta olmayan konu bu setten çıkarılmıştır.

| Müfredat başlığı | Anlatıldığı konu |
|---|---|
| 1.1 Cloud Computing (CapEx/OpEx, pay-as-you-go, fiyatlandırma modelleri, serverless) | [1.1](1.1-cloud-computing-models-pricing/README.md), [1.3](1.3-service-types-iaas-paas-saas/README.md) |
| 1.2 Shared Responsibility Model | [1.1](1.1-cloud-computing-models-pricing/README.md) |
| 1.3 Cloud Deployment Models (public, private, hybrid) | [1.1](1.1-cloud-computing-models-pricing/README.md) |
| 1.4 Benefits of Cloud Computing (HA, scalability, elasticity, reliability, predictability, security, governance, manageability) | [1.2](1.2-cloud-benefits/README.md) |
| 1.5 Cloud Service Types (IaaS, PaaS, SaaS) | [1.3](1.3-service-types-iaas-paas-saas/README.md) |
| 2.1 Core Architectural Components (regions, zones, resource groups, subscriptions, management groups, ARM) | [2.1](2.1-core-architecture/README.md) |
| 2.2 Compute (VM, VMSS, availability sets, AVD, containers, Functions, App Service) | [2.2](2.2-compute-virtual-machines/README.md), [2.3](2.3-app-hosting-containers-functions-avd/README.md) |
| 2.3 Networking (VNet, subnet, peering, IP, DNS, VPN Gateway, ExpressRoute, endpoints) | [2.4](2.4-networking/README.md) |
| 2.4 Storage (account türleri, Blob/Files/Queue/Table/Disks, tier'lar, redundancy, AzCopy, Storage Explorer, File Sync, Azure Migrate, Data Box) | [2.5](2.5-storage/README.md) |
| 2.5 Identity, Access and Security (Entra ID, Domain Services, SSO/MFA/passwordless, External ID, Conditional Access, RBAC, Zero Trust, Defense in Depth, Defender for Cloud) | [2.6](2.6-identity-access-rbac/README.md), [2.7](2.7-security-zero-trust-defender/README.md) |
| 3.1 Cost Management (Pricing Calculator, Cost Management, spending limit, forecast, tag'ler) | [3.1](3.1-cost-management/README.md) |
| 3.2 Governance and Compliance (Purview, Azure Policy, Resource Locks) | [3.2](3.2-governance-compliance/README.md) |
| 3.3 Management and Deployment Tools (Portal, Cloud Shell, CLI, PowerShell, Arc, IaC, ARM template) | [3.3](3.3-management-deployment-tools/README.md) |
| 3.4 Monitoring Tools (Advisor, Service Health, Monitor, Log Analytics, Alerts, Application Insights) | [3.4](3.4-monitoring-tools/README.md) |

---

## 📑 Kapsamlı İçindekiler

- **BÖLÜM 1: Describe Cloud Concepts (%25-30)**
  - [1.1 Cloud Computing, Cloud Modelleri ve Fiyatlandırma](1.1-cloud-computing-models-pricing/README.md)
    - [1.1 Quiz Soruları](1.1-cloud-computing-models-pricing/quiz.md)
  - [1.2 Bulutun Temel Faydaları](1.2-cloud-benefits/README.md)
    - [1.2 Quiz Soruları](1.2-cloud-benefits/quiz.md)
  - [1.3 Bulut Hizmet Türleri: IaaS, PaaS, SaaS, Serverless](1.3-service-types-iaas-paas-saas/README.md)
    - [1.3 Quiz Soruları](1.3-service-types-iaas-paas-saas/quiz.md)
- **BÖLÜM 2: Describe Azure Architecture and Services (%35-40)**
  - [2.1 Azure Temel Mimari Bileşenleri](2.1-core-architecture/README.md)
    - [2.1 Quiz Soruları](2.1-core-architecture/quiz.md)
  - [2.2 Azure Compute ve Virtual Machines](2.2-compute-virtual-machines/README.md)
    - [2.2 Quiz Soruları](2.2-compute-virtual-machines/quiz.md)
  - [2.3 Uygulama Barındırma: App Service, Container'lar, Functions, AVD](2.3-app-hosting-containers-functions-avd/README.md)
    - [2.3 Quiz Soruları](2.3-app-hosting-containers-functions-avd/quiz.md)
  - [2.4 Ağ Servisleri: VNet, IP Adresleri, Peering, VPN, ExpressRoute, DNS, Endpoint'ler](2.4-networking/README.md)
    - [2.4 Quiz Soruları](2.4-networking/quiz.md)
  - [2.5 Azure Storage: Blob, Files, Redundancy, Tiering, Veri Taşıma](2.5-storage/README.md)
    - [2.5 Quiz Soruları](2.5-storage/quiz.md)
  - [2.6 Kimlik ve Erişim: Entra ID, SSO/MFA/Passwordless, External ID, Conditional Access, RBAC](2.6-identity-access-rbac/README.md)
    - [2.6 Quiz Soruları](2.6-identity-access-rbac/quiz.md)
  - [2.7 Güvenlik: Zero Trust, Defense in Depth, Defender for Cloud](2.7-security-zero-trust-defender/README.md)
    - [2.7 Quiz Soruları](2.7-security-zero-trust-defender/quiz.md)
- **BÖLÜM 3: Describe Azure Management and Governance (%30-35)**
  - [3.1 Maliyet Yönetimi: Pricing Calculator, Cost Management, Tag'ler](3.1-cost-management/README.md)
    - [3.1 Quiz Soruları](3.1-cost-management/quiz.md)
  - [3.2 Yönetişim ve Uyumluluk: Azure Policy, Resource Lock'lar, Purview](3.2-governance-compliance/README.md)
    - [3.2 Quiz Soruları](3.2-governance-compliance/quiz.md)
  - [3.3 Kaynak Yönetme ve Dağıtma Araçları: Portal, Cloud Shell, CLI, PowerShell, Arc, ARM Template'leri](3.3-management-deployment-tools/README.md)
    - [3.3 Quiz Soruları](3.3-management-deployment-tools/quiz.md)
  - [3.4 İzleme Araçları: Advisor, Service Health, Monitor, Log Analytics, Alerts, Application Insights](3.4-monitoring-tools/README.md)
    - [3.4 Quiz Soruları](3.4-monitoring-tools/quiz.md)
- **BÖLÜM 4: AZ-900 Deneme Sınavı ve Sınav Stratejisi**
  - [4.0 Kapsamlı AZ-900 Deneme Sınavı (50 Soru) ve Sınav Stratejisi](5-practice-exam/README.md)
    - [4.1 Sınav Stratejisi ve Hazırlık Rehberi](5-practice-exam/README.md#51-sınav-stratejisi-ve-hazırlık-rehberi)
    - [4.2 50 Soruluk AZ-900 Deneme Sınavı](5-practice-exam/quiz.md)
    - [4.3 Puanlama ve Zayıf Alan Analizi](5-practice-exam/quiz.md#puan-tablosu)
    - [⚡ Hızlı Tekrar Kartı (sınavdan önceki gün)](5-practice-exam/cheat-sheet.md)
