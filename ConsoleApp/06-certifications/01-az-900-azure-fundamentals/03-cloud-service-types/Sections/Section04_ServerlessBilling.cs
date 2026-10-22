using System.Globalization;
using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudServiceTypes;

/// <summary>
/// Serverless billing logic: (number of executions) + (execution time × memory = GB-seconds), with a monthly free grant.
/// The rates are EXAMPLES based on the long-published rates of the Azure Functions Consumption plan (USD);
/// see the Pricing Calculator for current prices and the Flex Consumption plan.
/// </summary>
public static class Section04_ServerlessBilling
{
    private const decimal PricePerMillionExecutions = 0.20m;
    private const decimal PricePerGbSecond = 0.000016m;
    private const decimal FreeExecutions = 1_000_000m;
    private const decimal FreeGbSeconds = 400_000m;

    private const decimal AlwaysOnMonthly = 60m;   // a small always-on (24/7) VM/App Service plan (example)
    private const double DurationSeconds = 0.3;    // execution time per call
    private const double MemoryGb = 0.5;           // allocated memory

    public static void Run()
    {
        var inv = CultureInfo.InvariantCulture;
        Console.WriteLine(string.Create(inv, $"Function: {DurationSeconds * 1000} ms per call, {MemoryGb * 1024} MB memory. Comparison: 24/7 always-on server ~{AlwaysOnMonthly:0} $/month (example).\n"));

        long[] monthlyCalls = [100_000, 2_000_000, 20_000_000, 200_000_000];
        Table.Print(["Monthly calls", "GB-seconds", "Execution charge", "Duration charge", "Serverless total", "24/7 always-on", "Cheaper"],
            monthlyCalls.Select(calls =>
            {
                var gbSeconds = (decimal)(calls * DurationSeconds * MemoryGb);
                var execCost = Math.Max(0, calls - FreeExecutions) / 1_000_000m * PricePerMillionExecutions;
                var durationCost = Math.Max(0, gbSeconds - FreeGbSeconds) * PricePerGbSecond;
                var total = execCost + durationCost;
                return (IReadOnlyList<string>)
                [
                    calls.ToString("N0", inv),
                    gbSeconds.ToString("N0", inv),
                    execCost.ToString("0.00", inv) + " $",
                    durationCost.ToString("0.00", inv) + " $",
                    total.ToString("0.00", inv) + " $",
                    AlwaysOnMonthly.ToString("0", inv) + " $",
                    total < AlwaysOnMonthly ? "Serverless" : "24/7 always-on*",
                ];
            }));

        Console.WriteLine("""

        * At very high and CONSTANT load an always-on resource gets cheaper — but a single small server cannot carry 200 million calls a month;
          a real comparison also accounts for the number of instances needed (→ C1.11, Pricing Calculator).

        Characteristics of serverless (the phrases the exam looks for):
          • It does NOT mean no servers — provisioning, scaling and managing them is entirely the provider's job.
          • Event-triggered (HTTP request, queue message, file upload, timer).
          • Automatic scaling, down to ZERO when idle; no charge when idle (only per execution time/call).
          • Trade-off: "cold start" latency when waking from zero; execution time limits for long-running jobs.
        """);
    }
}
