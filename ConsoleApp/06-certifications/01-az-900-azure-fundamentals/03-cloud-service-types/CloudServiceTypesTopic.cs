using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudServiceTypes;

public static class CloudServiceTypesTopic
{
    public static int Run(string[] args) => args is ["lookup", .. var words]
        ? ServiceCatalog.Lookup(string.Join(' ', words))
        : SectionRunner.Run("1.3", "Cloud Service Types: IaaS, PaaS, SaaS, Serverless", Sections.All, args,
            "Ask about a product: dotnet run -- C1.3 lookup <product>   (e.g. lookup sql · lookup functions · lookup aks · lookup m365)");
}
