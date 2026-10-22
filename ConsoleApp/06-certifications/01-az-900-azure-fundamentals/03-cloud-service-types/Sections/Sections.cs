using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudServiceTypes;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "IaaS, PaaS, SaaS: yığının hangi katmanını kim yönetir? ⭐", Section01_ServiceModels.Run),
        new(2, "Azure ürünü → hizmet türü kataloğu ⭐",                     Section02_ProductCatalog.Run),
        new(3, "Senaryo → hizmet türü karar kuralları",                     Section03_Scenarios.Run),
        new(4, "Serverless: çağrı başına ücret vs 7/24 açık kaynak",        Section04_ServerlessBilling.Run),
    ];
}
