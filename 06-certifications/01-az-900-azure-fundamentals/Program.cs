using System.Globalization;
using Certification.AzureFundamentals;
using Certification.AzureFundamentals.CloudBenefits;
using Certification.AzureFundamentals.CloudComputing;
using Certification.AzureFundamentals.CloudServiceTypes;
using Certification.AzureFundamentals.CostManagement;
using Certification.AzureFundamentals.Storage;

// C1 — Microsoft Azure Fundamentals (AZ-900). Her konu: README (teori) + quiz.md; kodu olan konular (hesaplayıcı/demo) bu listede.
// 1.8'in Blob demosu Azurite ister: bu klasörde `docker compose up -d`.
//
//   dotnet run                         → konu listesi
//   dotnet run -- 1.2                  → 1.2'nin bölüm listesi
//   dotnet run -- 1.2 3                → 1.2'nin 3. bölümü  (all: hepsi)
//   dotnet run -- 1.2 seri 99.95 99.99 → composite SLA hesaplayıcısı (bkz. 1.2 README)

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentCulture = new CultureInfo("tr-TR");

Topic[] topics =
[
    new("1.1", "Cloud Computing, Cloud Modelleri ve Fiyatlandırma", CloudComputingTopic.Run),
    new("1.2", "Cloud'un Faydaları (HA, ölçek, SLA…)",               CloudBenefitsTopic.Run),
    new("1.3", "Cloud Hizmet Türleri: IaaS, PaaS, SaaS, Serverless", CloudServiceTypesTopic.Run),
    new("1.8", "Azure Storage: Blob, Files, Taşıma ve Migration",    StorageTopic.Run),
    new("1.11", "Azure'da Maliyet Yönetimi",                          CostManagementTopic.Run),
];

if (args.Length == 0 || topics.FirstOrDefault(t => t.Id == args[0]) is not { } topic)
{
    Console.WriteLine("C1 Microsoft Azure Fundamentals (AZ-900) — konular:\n");
    foreach (var t in topics) Console.WriteLine($"  {t.Id}  {t.Title}");
    Console.WriteLine("\nÇalıştırmak için: dotnet run -- <konu> [bölüm | all]");
    return args.Length == 0 ? 0 : 1;
}

return topic.Run(args[1..]);
