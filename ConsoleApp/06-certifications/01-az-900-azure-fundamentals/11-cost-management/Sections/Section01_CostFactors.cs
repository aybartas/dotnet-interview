using System.Globalization;
using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CostManagement;

/// <summary>Cost factors; an example effect for each. The numbers are EXAMPLES.</summary>
public static class Section01_CostFactors
{
    public static void Run()
    {
        var inv = CultureInfo.InvariantCulture;
        Table.Print(["Factor", "What it changes", "Example effect (example prices)"],
        [
            ["Resource type and settings", "SKU, size, tier, redundancy", "In the same VM family 2 → 8 vCPU ≈ 4x the price; LRS → GZRS storage ≈ 2x"],
            ["Consumption model", "Pay-as-you-go ↔ reservation/savings plan", "24/7 VM: ~50-60% cheaper with a 3-year reservation"],
            ["Maintenance (forgotten things)", "Unused resources stay on the bill", "VM deleted but disk + public IP remain → charged every month"],
            ["Geography / region", "The price of the same service by region", "The same VM size can differ 10-20% between two regions"],
            ["Network traffic", "Inbound (ingress) mostly free; outbound (egress) charged", "Traffic between regions/zones and out to the internet is charged per GB"],
            ["Subscription type", "Free/credit, pay-as-you-go, EA, CSP", "Free account credit; discounts under an enterprise agreement"],
            ["Azure Marketplace", "Third-party products", "Marketplace image: Azure charge + the vendor's software charge"],
        ]);

        // Egress calculation: serving the same data to users
        const decimal egressPerGb = 0.08m;   // example: egress to the internet, first tier
        const int freeGb = 100;              // example: monthly free egress allowance
        int[] monthlyGb = [50, 1_000, 20_000];
        Console.WriteLine("\nNetwork egress example — data served to the internet:\n");
        Table.Print(["Monthly egress", "Billable part", "Monthly egress cost (example)"],
            monthlyGb.Select(gb => (IReadOnlyList<string>)
            [
                $"{gb.ToString("N0", inv)} GB", $"{Math.Max(0, gb - freeGb).ToString("N0", inv)} GB", $"{(Math.Max(0, gb - freeGb) * egressPerGb).ToString("N2", inv)} $",
            ]));

        Console.WriteLine("""

        Exam rules: inbound data (ingress) is generally free, outbound data (egress) is charged; the price varies by region;
        deleting a VM may not automatically delete the attached disk/IP — deleting the resource group cleans up everything (→ C1.4).
        """);
    }
}
