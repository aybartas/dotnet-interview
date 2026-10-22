using System.Globalization;
using Certification.AzureFundamentals.CloudBenefits;
using Certification.AzureFundamentals.CloudComputing;
using Certification.AzureFundamentals.CloudServiceTypes;
using Certification.AzureFundamentals.CostManagement;
using Certification.AzureFundamentals.Storage;
using Junior.AsyncBasics.AsyncAwait;
using Junior.AsyncBasics.CancellationBasics;
using Junior.AsyncBasics.Concepts;
using Junior.BuildRunBasics.BigPicture;
using Junior.BuildRunBasics.BinObj;
using Junior.BuildRunBasics.RunBuildPublish;
using Junior.CollectionsLinq.CollectionsGenerics;
using Junior.CollectionsLinq.DelegatesLambdasEvents;
using Junior.CollectionsLinq.LinqBasics;
using Junior.CSharpFundamentals.ClassMembers;
using Junior.CSharpFundamentals.ExceptionHandling;
using Junior.CSharpFundamentals.ModernCSharp;
using Junior.CSharpFundamentals.Oop;
using Junior.CSharpFundamentals.ProgramAnatomy;
using Junior.CSharpFundamentals.TypeSystemMemory;
using Junior.DevFoundations.IdeDebugging.Exercises;
using Junior.DevFoundations.NetworkFundamentals;

// Tüm konsol konuları bu tek dosyadan çalışır. Bu dosyanın KENDİSİ "top-level statements" örneğidir (bkz. J2.1 Section01):
// class ve Main yazmadık; derleyici üretti. `args` komut satırı argümanlarıdır, `return <int>` process exit code'udur,
// `await` kullanınca derleyici Main'i `async Task<int>` yapar ("async all the way", bkz. J4).
//
// J = Junior, C = Sertifika. HTTP sunucusu gerektiren konular WebApp projesindedir.
//
//   dotnet run                       → konu listesi
//   dotnet run -- J3.2               → J3.2'nin bölüm listesi
//   dotnet run -- J3.2 4             → J3.2'nin 4. bölümü  (all: hepsi)
//   dotnet run -- C1.2 seri 99.95 99.99   → konuya özel argümanlar (bkz. konunun README'si)

Topic[] topics =
[
    // ── Junior ──
    new("J1.2", "Ağ Temelleri (TCP / UDP / Socket)",                       NetworkFundamentalsTopic.Run),
    new("J1.5", "IDE, Debugging & Kod Kalitesi",                           IdeDebuggingTopic.Run),
    new("J2.1", "Program Anatomisi",                                       ProgramAnatomyTopic.Run),
    new("J2.2", "Tip Sistemi & Bellek Modeli",                             TypeSystemMemoryTopic.Run),
    new("J2.3", "OOP Temelleri",                                           OopTopic.Run),
    new("J2.4", "Sınıf Üyeleri & Nesne Semantiği",                         ClassMembersTopic.Run),
    new("J2.5", "Exception Handling",                                      ExceptionHandlingTopic.Run),
    new("J2.6", "Günlük Modern C#",                                        ModernCSharpTopic.Run),
    new("J3.1", "Collections & Generics",                                  CollectionsGenericsTopic.Run),
    new("J3.2", "Delegates, Lambdas & Events",                             DelegatesLambdasEventsTopic.Run),
    new("J3.3", "LINQ Temelleri",                                          LinqBasicsTopic.Run),
    new("J4.1", "Temel Kavramlar (concurrency, parallelism, I/O vs CPU)",  ConceptsTopic.Run),
    new("J4.2", "async/await Kullanımı",                                   AsyncAwaitTopic.Run),
    new("J4.3", "Cancellation Temelleri",                                  CancellationBasicsTopic.Run),
    new("J5.1", "C# → IL → Native: Büyük Resim",                           BigPictureTopic.Run),
    new("J5.2", "bin/ ve obj/",                                            BinObjTopic.Run),
    new("J5.3", "run vs build vs publish",                                 RunBuildPublishTopic.Run),

    // ── Sertifika: AZ-900 (kodu olan konular; diğerleri yalnızca README + quiz) ──
    new("C1.1",  "Cloud Computing, Cloud Modelleri ve Fiyatlandırma",      CloudComputingTopic.Run),
    new("C1.2",  "Cloud'un Faydaları (HA, ölçek, SLA…)",                   CloudBenefitsTopic.Run),
    new("C1.3",  "Cloud Hizmet Türleri: IaaS, PaaS, SaaS, Serverless",     CloudServiceTypesTopic.Run),
    new("C1.8",  "Azure Storage: Blob, Files, Taşıma ve Migration",        StorageTopic.Run),
    new("C1.11", "Azure'da Maliyet Yönetimi",                              CostManagementTopic.Run),
];

if (args.Length == 0 || topics.FirstOrDefault(t => t.Id.Equals(args[0], StringComparison.OrdinalIgnoreCase)) is not { } topic)
{
    Console.WriteLine("Konular:\n");
    foreach (var t in topics) Console.WriteLine($"  {t.Id,-5} {t.Title}");
    Console.WriteLine("\nÇalıştırmak için: dotnet run -- <konu> [bölüm | all]   (README-only konular: J1.1 Git, J1.4 .NET CLI, C1.4–C1.7, C1.9–C1.10, C1.12–C1.15)");
    return args.Length == 0 ? 0 : 1;
}

if (topic.Id.StartsWith('C')) CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentCulture = new CultureInfo("tr-TR");   // yüzde/para çıktıları tr-TR

return await topic.Run(args[1..]);

/// <summary>Bir konu: Id (ör. "J3.2"), başlık ve giriş noktası (kalan argümanları alır, exit code döner). Senkron konular için ikinci yapıcı var.</summary>
public sealed record Topic(string Id, string Title, Func<string[], Task<int>> Run)
{
    public Topic(string id, string title, Func<string[], int> run) : this(id, title, args => Task.FromResult(run(args))) { }
}
