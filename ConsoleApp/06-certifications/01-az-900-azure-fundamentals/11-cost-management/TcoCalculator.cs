using System.Globalization;
using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CostManagement;

/// <summary>
/// 3-year total cost of ownership (TCO): your own datacenter (CapEx-heavy) vs Azure (OpEx).
/// ALL prices are EXAMPLES (USD); the goal is to see the structure of the line items, not a real quote.
/// </summary>
public static class TcoCalculator
{
    private const int Years = 3;

    // On-prem (per server)
    private const decimal ServerHardware = 9_000m;          // upfront: server (CapEx)
    private const decimal StorageNetworkPerServer = 3_000m; // upfront: share of storage + network (CapEx)
    private const decimal LicensePerServer = 2_000m;        // upfront: OS/virtualization license (CapEx)
    private const decimal PowerCoolingPerYear = 1_100m;     // power + cooling
    private const decimal SpacePerYear = 600m;              // rack/building space
    private const decimal MaintenancePerYear = 900m;        // hardware maintenance contract
    private const decimal LaborPerYear = 2_500m;            // share of hardware/OS operations labor

    // Azure (equivalent VM per server)
    private const decimal VmPayAsYouGoPerMonth = 520m;    // ~8 vCPU / 32 GB Windows VM (example)
    private const decimal Reserved3YearDiscount = 0.55m;    // 3-year reservation discount (example)
    private const decimal HybridBenefitDiscount = 0.30m;    // with an existing Windows Server license (example)
    private const decimal StorageNetworkPerMonth = 45m;     // managed disk + egress share
    private const decimal CloudLaborPerYear = 1_000m;       // no hardware work; OS/configuration labor remains

    public sealed record Line(string Item, string Kind, decimal[] PerYear)
    {
        public decimal Total => PerYear.Sum();
    }

    public static IReadOnlyList<Line> OnPrem(int servers) =>
    [
        new("Hardware (servers)", "CapEx", Upfront(servers * ServerHardware)),
        new("Storage + network equipment", "CapEx", Upfront(servers * StorageNetworkPerServer)),
        new("Software licenses", "CapEx", Upfront(servers * LicensePerServer)),
        new("Power + cooling", "OpEx", Yearly(servers * PowerCoolingPerYear)),
        new("Datacenter space", "OpEx", Yearly(servers * SpacePerYear)),
        new("Hardware maintenance", "OpEx", Yearly(servers * MaintenancePerYear)),
        new("IT operations labor", "OpEx", Yearly(servers * LaborPerYear)),
    ];

    public static IReadOnlyList<Line> Cloud(int servers, bool reserved, bool hybridBenefit)
    {
        var vm = VmPayAsYouGoPerMonth * (reserved ? 1 - Reserved3YearDiscount : 1) * (hybridBenefit ? 1 - HybridBenefitDiscount : 1);
        var label = (reserved ? "3-year reservation" : "pay-as-you-go") + (hybridBenefit ? " + Hybrid Benefit" : "");
        return
        [
            new($"VMs ({label})", "OpEx", Yearly(servers * vm * 12)),
            new("Disk + network egress", "OpEx", Yearly(servers * StorageNetworkPerMonth * 12)),
            new("IT operations labor", "OpEx", Yearly(servers * CloudLaborPerYear)),
        ];
    }

    public static int Print(int servers)
    {
        var onPrem = OnPrem(servers);
        Console.WriteLine($"Workload of {servers} servers, {Years} years — line by line (example prices, USD):\n");
        PrintLines("Your own datacenter", onPrem);

        (string Name, IReadOnlyList<Line> Lines)[] cloudOptions =
        [
            ("Azure — pay-as-you-go", Cloud(servers, reserved: false, hybridBenefit: false)),
            ("Azure — 3-year reservation", Cloud(servers, reserved: true, hybridBenefit: false)),
            ("Azure — reservation + Azure Hybrid Benefit", Cloud(servers, reserved: true, hybridBenefit: true)),
        ];
        foreach (var (name, lines) in cloudOptions) PrintLines(name, lines);

        Console.WriteLine("Cumulative cost:\n");
        Table.Print(["Option", .. Enumerable.Range(1, Years).Select(y => $"End of year {y}"), "Year 1 share of total"],
        [
            Cumulative("Your own datacenter", onPrem),
            .. cloudOptions.Select(o => Cumulative(o.Name, o.Lines)),
        ]);

        var onPremTotal = onPrem.Sum(l => l.Total);
        var best = cloudOptions.MinBy(o => o.Lines.Sum(l => l.Total));
        var bestTotal = best.Lines.Sum(l => l.Total);
        Console.WriteLine(FormattableString.Invariant($"""

        Cheapest cloud option: {best.Name} → {bestTotal:N0} $ ({(onPremTotal - bestTotal) / onPremTotal:0%} {(bestTotal < onPremTotal ? "cheaper" : "more expensive")}).
        Observations:
          • Most of the on-prem spend is paid UPFRONT in year 1 (CapEx); in the cloud, spending is spread over the months (OpEx).
          • For a constant 24/7 load, pay-as-you-go is the most expensive cloud option; reservation and Hybrid Benefit close the gap.
          • TCO includes not just the server price but power, space, maintenance and LABOR — the items most often forgotten in on-prem comparisons.
          • If the hardware is used longer than 3 years (e.g. 5-year depreciation), the on-prem yearly cost drops and the gap narrows;
            the duration, utilization and labor assumptions determine the result.
          • This is a demonstration of the structure with example prices; for a real comparison use the Pricing Calculator and the
            Azure Migrate business case (README 3.2).
        """));
        return 0;
    }

    private static void PrintLines(string title, IReadOnlyList<Line> lines)
    {
        Console.WriteLine(title);
        Table.Print(["Item", "Type", .. Enumerable.Range(1, Years).Select(y => $"Year {y}"), "Total"],
        [
            .. lines.Select(l => (IReadOnlyList<string>)[l.Item, l.Kind, .. l.PerYear.Select(Money), Money(l.Total)]),
            ["TOTAL", "", .. Enumerable.Range(0, Years).Select(y => Money(lines.Sum(l => l.PerYear[y]))), Money(lines.Sum(l => l.Total))],
        ]);
        Console.WriteLine();
    }

    private static IReadOnlyList<string> Cumulative(string name, IReadOnlyList<Line> lines)
    {
        var running = 0m;
        var cells = Enumerable.Range(0, Years).Select(y => Money(running += lines.Sum(l => l.PerYear[y]))).ToList();
        var total = lines.Sum(l => l.Total);
        var firstYearShare = lines.Sum(l => l.PerYear[0]) / total;
        return [name, .. cells, firstYearShare.ToString("0%", CultureInfo.InvariantCulture)];
    }

    private static decimal[] Upfront(decimal amount) => [amount, .. Enumerable.Repeat(0m, Years - 1)];
    private static decimal[] Yearly(decimal amount) => [.. Enumerable.Repeat(amount, Years)];
    private static string Money(decimal value) => value.ToString("N0", CultureInfo.InvariantCulture) + " $";
}
