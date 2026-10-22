using System.Globalization;
using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudComputing;

/// <summary>Pricing models for the same VM size. Hourly prices are EXAMPLES (the ratios are close to reality, the numbers are not).</summary>
public static class Section05_PricingModels
{
    private const decimal PayAsYouGoHourly = 4.00m;     // example $/hour
    private const decimal Reserved1YearHourly = 2.40m;  // ~40% discount (example)
    private const decimal Reserved3YearHourly = 1.60m;  // ~60% discount (example)
    private const decimal SpotHourly = 0.80m;           // spare capacity, can be evicted (example)
    private const int HoursPerMonth = 730;

    private static string Usd(decimal value) => "$" + value.ToString("N0", CultureInfo.InvariantCulture);

    public static void Run()
    {
        Console.WriteLine("Monthly cost of the same VM, by hours of use (example prices):\n");

        int[] usageHours = [730, 300, 100];
        Table.Print(["Model", "Commitment", .. usageHours.Select(h => $"{h} h/month")],
        [
            ["Pay-as-you-go", "None", .. usageHours.Select(h => Usd(h * PayAsYouGoHourly))],
            ["Reserved 1 year", "1 year, paid even if the hours are not used", .. usageHours.Select(_ => Usd(HoursPerMonth * Reserved1YearHourly))],
            ["Reserved 3 years", "3 years", .. usageHours.Select(_ => Usd(HoursPerMonth * Reserved3YearHourly))],
            ["Spot", "None; the VM is evicted when Azure needs the capacity", .. usageHours.Select(h => Usd(h * SpotHourly))],
        ]);

        var breakEven = HoursPerMonth * Reserved1YearHourly / PayAsYouGoHourly;
        Console.WriteLine(FormattableString.Invariant($"""

        Break-even: for a VM running more than ~{breakEven:0} hours a month, a 1-year reservation is cheaper than pay-as-you-go.

        Which model when?
          • Pay-as-you-go   : variable/unpredictable load, trials, short projects — flexibility.
          • Reservation / savings plan : 24/7 workloads that will run for years at a STEADY level — discount in return for a commitment.
          • Spot            : interruption-tolerant batch jobs, rendering, testing — cheapest, but can be reclaimed at any time.
          • Free tier / free account credit, dev/test pricing: learning and development environments.
          License savings (Azure Hybrid Benefit) and other cost tools → C1.11.
        """));
    }
}
