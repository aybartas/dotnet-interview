using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudServiceTypes;

public static class CloudServiceTypesTopic
{
    public static int Run(string[] args) => args is ["tur", .. var words]
        ? ServiceCatalog.Lookup(string.Join(' ', words))
        : SectionRunner.Run("1.3", "Cloud Hizmet Türleri: IaaS, PaaS, SaaS, Serverless", Sections.All, args,
            "Ürün sor: dotnet run -- 1.3 tur <ürün>   (ör. tur sql · tur functions · tur aks · tur m365)");
}
