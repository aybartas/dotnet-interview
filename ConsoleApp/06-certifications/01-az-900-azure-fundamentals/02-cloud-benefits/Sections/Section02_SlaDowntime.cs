using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

public static class Section02_SlaDowntime
{
    public static void Run()
    {
        Console.WriteLine("An SLA (Service Level Agreement) percentage means the maximum downtime allowed in a month (month = 730 hours):\n");

        double[] slas = [0.99, 0.999, 0.9995, 0.9999, 0.99999];
        Table.Print(SlaCalculator.DowntimeHeaders, slas.Select(SlaCalculator.DowntimeRow));

        Console.WriteLine("""

        Reading practice:
          • Every "9" divides the downtime budget by 10: 99.9% → ~43 min/month, 99.99% → ~4 min/month.
          • The 99.9% → 99.95% difference looks small but halves the monthly downtime budget (~43 → ~22 min).

        Rules the exam asks about SLAs:
          • An SLA is Microsoft's uptime COMMITMENT; if it is missed, the customer can claim a SERVICE CREDIT (a discount on the bill).
          • SLAs vary by product and configuration (e.g. single VM < availability set < VMs spread across availability zones).
          • Preview services have no SLA; free tiers generally have none either.
          • The way to raise an SLA is redundancy: spread across zones/regions — at the price of more cost and complexity.

        With your own value: dotnet run -- C1.2 downtime 99.95
        """);
    }
}
