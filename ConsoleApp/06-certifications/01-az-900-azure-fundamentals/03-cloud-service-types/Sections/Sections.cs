using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudServiceTypes;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "IaaS, PaaS, SaaS: who manages which layer of the stack? ⭐", Section01_ServiceModels.Run),
        new(2, "Azure product → service type catalog ⭐",                    Section02_ProductCatalog.Run),
        new(3, "Scenario → service type decision rules",                     Section03_Scenarios.Run),
        new(4, "Serverless: per-call billing vs an always-on resource",      Section04_ServerlessBilling.Run),
    ];
}
