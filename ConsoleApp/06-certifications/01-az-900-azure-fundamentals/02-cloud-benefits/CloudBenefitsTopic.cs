using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

public static class CloudBenefitsTopic
{
    public static int Run(string[] args) => args switch
    {
        ["downtime", var sla] => SlaCalculator.Downtime(sla),
        [var mode and ("serial" or "parallel"), .. var slas] => SlaCalculator.Composite(mode, slas),
        _ => SectionRunner.Run("1.2", "Benefits of the Cloud (HA, scale, SLA…)", Sections.All, args,
            """
            Calculators:
              dotnet run -- C1.2 downtime 99.95              → daily/weekly/monthly/yearly downtime budget of an SLA
              dotnet run -- C1.2 serial 99.95 99.99          → composite SLA of components chained in series
              dotnet run -- C1.2 parallel 99.94 99.94        → composite SLA of redundant (parallel) components
            """),
    };
}
