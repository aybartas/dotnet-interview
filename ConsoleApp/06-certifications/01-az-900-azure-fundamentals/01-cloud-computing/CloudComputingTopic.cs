using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudComputing;

public static class CloudComputingTopic
{
    public static int Run(string[] args) => args is ["kim", var layer, var model]
        ? ResponsibilityMatrix.Answer(layer, model)
        : SectionRunner.Run("1.1", "Cloud Computing, Cloud Modelleri ve Fiyatlandırma", Sections.All, args,
            "Soru sor: dotnet run -- C1.1 kim <katman> <model>   (ör. kim os paas · katmanlar: veri, cihaz, hesap, kimlik-altyapisi, uygulama, ag, os, fiziksel-host, fiziksel-ag, datacenter · modeller: onprem, iaas, paas, saas)");
}
