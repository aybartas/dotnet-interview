using System.Globalization;
using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.Storage;

/// <summary>
/// Access tiers: the cheaper the storage, the more expensive the access. Prices are EXAMPLES (the ratios are close to reality, the numbers are not);
/// see the Pricing Calculator for current prices.
/// </summary>
public static class Section03_AccessTiers
{
    private sealed record Tier(string Name, string Use, int MinDays, string FirstByte, decimal StoragePerGbMonth, decimal ReadPer10k);

    private static readonly Tier[] Tiers =
    [
        new("Hot",     "Frequently accessed data",              0,   "milliseconds", 0.020m, 0.005m),
        new("Cool",    "Infrequent access, at least 30 days",   30,  "milliseconds", 0.010m, 0.010m),
        new("Cold",    "Rare access, at least 90 days",         90,  "milliseconds", 0.004m, 0.100m),
        new("Archive", "Very rare, at least 180 days, OFFLINE", 180, "hours (rehydrate)", 0.001m, 5.000m),
    ];

    public static void Run()
    {
        var inv = CultureInfo.InvariantCulture;
        Table.Print(["Tier", "Use", "Recommended minimum duration", "First byte", "Storage $/GB-month*", "10,000 reads $*"],
            Tiers.Select(t => (IReadOnlyList<string>)
            [
                t.Name, t.Use, t.MinDays == 0 ? "—" : $"{t.MinDays} days", t.FirstByte,
                t.StoragePerGbMonth.ToString("0.000", inv), t.ReadPer10k.ToString("0.000", inv),
            ]));

        const int sizeGb = 1_000;            // 1 TB of backups
        long[] readsPerMonth = [1_000, 1_000_000, 100_000_000];
        Console.WriteLine($"\nExample monthly cost for {sizeGb.ToString("N0", inv)} GB of data (storage + read operations; data egress/rehydrate charges excluded):\n");
        Table.Print(["Monthly reads", .. Tiers.Select(t => t.Name), "Cheapest"],
            readsPerMonth.Select(reads =>
            {
                var costs = Tiers.Select(t => sizeGb * t.StoragePerGbMonth + reads / 10_000m * t.ReadPer10k).ToArray();
                var cheapest = Tiers[Array.IndexOf(costs, costs.Min())].Name;
                return (IReadOnlyList<string>)[reads.ToString("N0", inv), .. costs.Select(c => c.ToString("0.00", inv) + " $"), cheapest];
            }));

        Console.WriteLine("""

        * Example prices. The real lesson: the cheaper the storage, the more expensive the ACCESS — putting frequently read data in Archive is expensive.
          (In Archive every read also needs a rehydrate; the read cost in the table simplifies this.)

        Rules:
          • Archive is OFFLINE: a blob must be "rehydrated" to Hot/Cool/Cold before it can be read — with standard priority
            it can take hours (high priority is faster, more expensive).
          • Deleting or changing the tier of Cool/Cold/Archive data before the minimum duration is up incurs an "early deletion" charge.
          • The tier is set at the blob level; a default tier is chosen at the account level — Archive cannot be the default.
          • Lifecycle management rules: automatic transitions such as "blob untouched for 30 days → Cool, 180 days → Archive".
        """);
    }
}
