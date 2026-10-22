using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudComputing;

public static class CloudComputingTopic
{
    public static int Run(string[] args) => args is ["who", var layer, var model]
        ? ResponsibilityMatrix.Answer(layer, model)
        : SectionRunner.Run("1.1", "Cloud Computing, Cloud Models and Pricing", Sections.All, args,
            "Ask a question: dotnet run -- C1.1 who <layer> <model>   (e.g. who os paas · layers: data, device, account, identity-infra, app, network, os, physical-host, physical-network, datacenter · models: onprem, iaas, paas, saas)");
}
