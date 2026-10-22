using System.Globalization;
using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CostManagement;

/// <summary>
/// Reporting cost by department/environment using tags (key-value metadata) — a small imitation of Cost analysis' "group by tag" view.
/// Also shows that a resource group's tags are NOT passed to its resources BY DEFAULT. Amounts are EXAMPLES.
/// </summary>
public static class Section04_Tags
{
    private sealed record Resource(string Name, string ResourceGroup, decimal MonthlyCost, Dictionary<string, string> Tags);

    private static readonly Dictionary<string, Dictionary<string, string>> GroupTags = new()
    {
        ["rg-ecommerce-prod"] = new() { ["env"] = "prod", ["costCenter"] = "1001" },
        ["rg-ecommerce-dev"] = new() { ["env"] = "dev", ["costCenter"] = "1001" },
        ["rg-hr"] = new() { ["costCenter"] = "2002" },
    };

    private static readonly Resource[] Resources =
    [
        new("app-ecommerce",       "rg-ecommerce-prod", 420m, new() { ["env"] = "prod", ["costCenter"] = "1001", ["owner"] = "alice" }),
        new("sql-ecommerce",       "rg-ecommerce-prod", 690m, new() { ["env"] = "prod", ["costCenter"] = "1001" }),
        new("st-ecommerce-media",  "rg-ecommerce-prod", 85m,  new()),  // tag forgotten — the group's tags do not carry over!
        new("vm-ecommerce-test",   "rg-ecommerce-dev",  140m, new() { ["env"] = "dev", ["costCenter"] = "1001" }),
        new("app-hr-portal",       "rg-hr",             110m, new() { ["env"] = "prod", ["costCenter"] = "2002", ["owner"] = "bob" }),
        new("sql-hr",              "rg-hr",             95m,  new()),  // tag forgotten
    ];

    public static void Run()
    {
        Console.WriteLine("Resources and their tags:\n");
        Table.Print(["Resource", "Resource group", "Monthly (example)", "Tags"],
            Resources.Select(r => (IReadOnlyList<string>)[r.Name, r.ResourceGroup, Usd(r.MonthlyCost), Format(r.Tags)]));

        Report("costCenter", Resources.Select(r => (r.MonthlyCost, r.Tags)));

        Console.WriteLine("\nThe resource groups have tags too — but Azure does NOT copy them to the resources inside on its own:");
        foreach (var (rg, tags) in GroupTags) Console.WriteLine($"   {rg,-18} {Format(tags)}");

        Console.WriteLine("\nAfter Azure Policy 'Inherit a tag from the resource group if missing' (Modify effect) + remediation:");
        Report("costCenter", Resources.Select(r => (r.MonthlyCost, Inherit(r))));

        Console.WriteLine("""

        Rules:
          • A tag = a key-value pair attached to a resource (env=prod, costCenter=1001, owner=alice); a resource can have many tags.
          • Uses: cost reporting (Cost analysis → group by tag), finding/managing resources, automation (e.g. shut down env=dev at night),
            security/data classification, compliance.
          • Tags are NOT INHERITED by default: a resource group's tag does not pass to the resources inside.
          • To enforce them or copy them from the group, use Azure Policy (Deny: "Require a tag…", Modify: "Inherit a tag…") → C1.12.
          • (context) Cost Management's "tag inheritance" setting applies group tags only to COST RECORDS; the resource does not change.
        """);
    }

    private static Dictionary<string, string> Inherit(Resource r)
    {
        var merged = new Dictionary<string, string>(r.Tags);
        foreach (var (key, value) in GroupTags[r.ResourceGroup]) merged.TryAdd(key, value);
        return merged;
    }

    private static void Report(string tagKey, IEnumerable<(decimal Cost, Dictionary<string, string> Tags)> items)
    {
        Console.WriteLine($"\nCost analysis — group by tag '{tagKey}':");
        Table.Print([tagKey, "Monthly cost"],
            items.GroupBy(i => i.Tags.GetValueOrDefault(tagKey) ?? "(no tag)")
                 .OrderBy(g => g.Key, StringComparer.Ordinal)
                 .Select(g => (IReadOnlyList<string>)[g.Key, Usd(g.Sum(i => i.Cost))]));
    }

    private static string Usd(decimal value) => value.ToString("N0", CultureInfo.InvariantCulture) + " $";

    private static string Format(Dictionary<string, string> tags) =>
        tags.Count == 0 ? "—" : string.Join(", ", tags.Select(t => $"{t.Key}={t.Value}"));
}
