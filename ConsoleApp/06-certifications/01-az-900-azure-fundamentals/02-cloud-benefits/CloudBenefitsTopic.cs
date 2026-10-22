using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

public static class CloudBenefitsTopic
{
    public static int Run(string[] args) => args switch
    {
        ["kesinti", var sla] => SlaCalculator.Downtime(sla),
        [var mode and ("seri" or "paralel"), .. var slas] => SlaCalculator.Composite(mode, slas),
        _ => SectionRunner.Run("1.2", "Cloud'un Faydaları", Sections.All, args,
            """
            Hesaplayıcılar:
              dotnet run -- C1.2 kesinti 99.95               → SLA'nın günlük/haftalık/aylık/yıllık kesinti bütçesi
              dotnet run -- C1.2 seri 99.95 99.99            → seri bağlı bileşenlerin composite SLA'sı
              dotnet run -- C1.2 paralel 99.94 99.94         → yedekli (paralel) bileşenlerin composite SLA'sı
            """),
    };
}
