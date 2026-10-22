using System.Globalization;
using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudComputing;

/// <summary>
/// CapEx vs OpEx: meeting the same 12 months of demand with your own hardware (sized for the peak, paid upfront) and with
/// the consumption-based cloud (only as much capacity as is used). Prices are EXAMPLES, not real prices.
/// </summary>
public static class Section04_ConsumptionModel
{
    // Servers needed per month (e.g. an e-commerce site: November-December peak)
    private static readonly int[] DemandPerMonth = [4, 4, 5, 4, 4, 5, 5, 4, 6, 8, 16, 20];
    private static readonly string[] Months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

    private const decimal ServerPurchasePrice = 120_000m;      // upfront price of one server (example, USD)
    private const int DepreciationYears = 4;                     // useful life of the hardware
    private const decimal OnPremMonthlyRunCostPerServer = 900m;  // power, cooling, maintenance (example)
    private const decimal CloudMonthlyPricePerServer = 4_500m;   // monthly pay-as-you-go price of an equivalent cloud VM (example)

    private static string Usd(decimal value) => "$" + value.ToString("N0", CultureInfo.InvariantCulture);

    public static void Run()
    {
        var peak = DemandPerMonth.Max();

        // On-prem: servers for the peak are bought upfront; yearly depreciation + running cost of ALL servers every month
        var upfront = peak * ServerPurchasePrice;
        var onPremYear = upfront / DepreciationYears + peak * OnPremMonthlyRunCostPerServer * 12;
        var idleServerMonths = DemandPerMonth.Sum(d => peak - d);

        // Cloud: each month only as much as that month needs
        var cloudYear = DemandPerMonth.Sum(d => d * CloudMonthlyPricePerServer);

        Console.WriteLine("Servers needed per month: " + string.Join(" ", Months.Zip(DemandPerMonth, (m, d) => $"{m}:{d}")));
        Console.WriteLine($"Peak: {peak} servers (December)\n");

        Table.Print(["", "Your own datacenter (CapEx)", "Cloud — consumption-based (OpEx)"],
        [
            ["Upfront payment", Usd(upfront), "$0"],
            ["Capacity", $"{peak} servers every month (sized for the peak)", "Exactly what each month needs"],
            ["Idle server-months", idleServerMonths.ToString(CultureInfo.InvariantCulture), "0"],
            ["Yearly cost (example)", $"{Usd(onPremYear)}  (depreciation + running)", Usd(cloudYear)],
            ["Accounting", "Asset; depreciation spread over the years", "Period expense"],
        ]);

        Console.WriteLine(FormattableString.Invariant($"""

        Observation: with a workload that fluctuates {peak / (double)DemandPerMonth.Min():0}x, {100.0 * idleServerMonths / (peak * 12):0}% of the on-prem capacity sits idle all year.
        The consumption-based model pays nothing for idle capacity; the risk of forecasting errors (buying too little or too much) disappears.
        But note: if the load is CONSTANT and 24/7, the gap closes — then committed prices such as reserved instances come into play (section 5).

        CapEx (capital expenditure): a fixed investment made upfront in physical infrastructure, spread over the years.
        OpEx (operational expenditure): an expense paid period by period for a service/product, varying with usage.
        The cloud shifts spending from CapEx to OpEx. (All prices in this calculation are examples.)
        """));
    }
}
