using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.Storage;

/// <summary>Redundancy options ↔ failure types. The exam's favorite storage table.</summary>
public static class Section02_Redundancy
{
    private sealed record Option(string Name, string Copies, bool Server, bool Datacenter, bool Region, bool ReadSecondaryAlways);

    private static readonly Option[] Options =
    [
        new("LRS",     "3 copies in the primary region, inside a SINGLE datacenter",        true, false, false, false),
        new("ZRS",     "3 copies across 3 availability zones in the primary region",        true, true,  false, false),
        new("GRS",     "LRS (primary) + LRS in the paired region (6 copies in total)",      true, false, true,  false),
        new("GZRS",    "ZRS (primary) + LRS in the paired region",                          true, true,  true,  false),
        new("RA-GRS",  "GRS + read access from the secondary region ALWAYS",                true, false, true,  true),
        new("RA-GZRS", "GZRS + read access from the secondary region ALWAYS",               true, true,  true,  true),
    ];

    public static void Run()
    {
        Table.Print(["Option", "Copies", "Disk/rack failure", "Datacenter/zone failure", "Regional disaster", "Read during disaster"],
            Options.Select(o => (IReadOnlyList<string>)
            [
                o.Name, o.Copies, Mark(o.Server), Mark(o.Datacenter),
                o.Region ? "✔ (with failover)" : "✘",
                o.ReadSecondaryAlways ? "✔ from secondary" : o.Region ? "after failover" : "✘",
            ]));

        Console.WriteLine("""

        Note: with GRS/GZRS, a datacenter failure in the primary region does not lose data (there is a copy in the secondary) but the service
        cannot be read until failover; the "datacenter/zone failure" column in the table shows uninterrupted access.

        Scenario → option:
          • "Cheapest; a datacenter disaster is acceptable"                                  → LRS
          • "Must stay in one region (data residency), but access continues if a zone fails" → ZRS
          • "Survive a regional disaster"                                                    → GRS (or GZRS)
          • "Data must be READABLE even during a regional disaster"                          → RA-GRS / RA-GZRS
          • "Both uninterrupted access on a zone failure and regional disaster protection"  → GZRS (RA-GZRS if reads are needed too)
        The secondary region is always the primary's region pair (→ C1.4); replication to the secondary region is ASYNCHRONOUS.
        """);
    }

    private static string Mark(bool ok) => ok ? "✔" : "✘";
}
