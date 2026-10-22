using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CostManagement;

/// <summary>
/// Tag'ler (anahtar-değer meta veri) ile maliyeti departmana/ortama göre raporlamak — Cost analysis'in "group by tag" görünümünün
/// küçük bir taklidi. Resource group tag'inin kaynaklara VARSAYILAN OLARAK geçmediğini de gösterir. Tutarlar ÖRNEKTİR.
/// </summary>
public static class Section04_Tags
{
    private sealed record Resource(string Name, string ResourceGroup, decimal MonthlyCost, Dictionary<string, string> Tags);

    private static readonly Dictionary<string, Dictionary<string, string>> GroupTags = new()
    {
        ["rg-eticaret-prod"] = new() { ["env"] = "prod", ["costCenter"] = "1001" },
        ["rg-eticaret-dev"] = new() { ["env"] = "dev", ["costCenter"] = "1001" },
        ["rg-ik"] = new() { ["costCenter"] = "2002" },
    };

    private static readonly Resource[] Resources =
    [
        new("app-eticaret",      "rg-eticaret-prod", 420m, new() { ["env"] = "prod", ["costCenter"] = "1001", ["owner"] = "ayse" }),
        new("sql-eticaret",      "rg-eticaret-prod", 690m, new() { ["env"] = "prod", ["costCenter"] = "1001" }),
        new("st-eticaret-media", "rg-eticaret-prod", 85m,  new()),  // tag unutulmuş — grubun tag'leri geçmez!
        new("vm-eticaret-test",  "rg-eticaret-dev",  140m, new() { ["env"] = "dev", ["costCenter"] = "1001" }),
        new("app-ik-portal",     "rg-ik",            110m, new() { ["env"] = "prod", ["costCenter"] = "2002", ["owner"] = "mehmet" }),
        new("sql-ik",            "rg-ik",            95m,  new()),  // tag unutulmuş
    ];

    public static void Run()
    {
        Console.WriteLine("Kaynaklar ve tag'leri:\n");
        Table.Print(["Kaynak", "Resource group", "Aylık (örnek)", "Tag'ler"],
            Resources.Select(r => (IReadOnlyList<string>)[r.Name, r.ResourceGroup, $"{r.MonthlyCost:N0} $", Format(r.Tags)]));

        Report("costCenter", Resources.Select(r => (r.MonthlyCost, r.Tags)));

        Console.WriteLine("\nResource group'ların da tag'i var — ama Azure bunları içerideki kaynaklara KENDİLİĞİNDEN kopyalamaz:");
        foreach (var (rg, tags) in GroupTags) Console.WriteLine($"   {rg,-18} {Format(tags)}");

        Console.WriteLine("\nAzure Policy 'Inherit a tag from the resource group if missing' (Modify etkisi) + remediation sonrası:");
        Report("costCenter", Resources.Select(r => (r.MonthlyCost, Inherit(r))));

        Console.WriteLine("""

        Kurallar:
          • Tag = kaynağa eklenen anahtar-değer çifti (env=prod, costCenter=1001, owner=ayse); kaynak başına çok sayıda tag olabilir.
          • Kullanım: maliyet raporlama (Cost analysis → group by tag), kaynak arama/yönetim, otomasyon (ör. env=dev olanları gece kapat),
            güvenlik/veri sınıflandırma, uyumluluk.
          • Tag'ler varsayılan olarak MİRAS ALINMAZ: resource group'un tag'i içerideki kaynağa geçmez.
          • Zorunlu kılmak veya gruptan kopyalamak için Azure Policy (Deny: "Require a tag…", Modify: "Inherit a tag…") → C1.12.
          • (bağlam) Cost Management'ın "tag inheritance" ayarı grup tag'lerini yalnızca MALİYET KAYITLARINA uygular; kaynak değişmez.
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
        Table.Print([tagKey, "Aylık maliyet"],
            items.GroupBy(i => i.Tags.GetValueOrDefault(tagKey) ?? "(tag yok)")
                 .OrderBy(g => g.Key)
                 .Select(g => (IReadOnlyList<string>)[g.Key, $"{g.Sum(i => i.Cost):N0} $"]));
    }

    private static string Format(Dictionary<string, string> tags) =>
        tags.Count == 0 ? "—" : string.Join(", ", tags.Select(t => $"{t.Key}={t.Value}"));
}
