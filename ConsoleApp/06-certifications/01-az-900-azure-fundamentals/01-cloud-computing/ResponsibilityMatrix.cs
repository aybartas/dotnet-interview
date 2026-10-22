namespace Certification.AzureFundamentals.CloudComputing;

public enum Owner { Musteri, Paylasimli, Microsoft }

public enum DeploymentModel { OnPrem, IaaS, PaaS, SaaS }

/// <summary>Shared responsibility model — Microsoft'un yayımladığı katman tablosunun bu repodaki karşılığı (README 3.2).</summary>
public static class ResponsibilityMatrix
{
    public sealed record Layer(string Key, string Name, Owner OnPrem, Owner IaaS, Owner PaaS, Owner SaaS)
    {
        public Owner For(DeploymentModel model) => model switch
        {
            DeploymentModel.OnPrem => OnPrem,
            DeploymentModel.IaaS => IaaS,
            DeploymentModel.PaaS => PaaS,
            _ => SaaS,
        };
    }

    public static IReadOnlyList<Layer> Layers { get; } =
    [
        new("veri",              "Bilgi ve veri",                    Owner.Musteri, Owner.Musteri,  Owner.Musteri,    Owner.Musteri),
        new("cihaz",             "Cihazlar (mobil ve PC)",           Owner.Musteri, Owner.Musteri,  Owner.Musteri,    Owner.Musteri),
        new("hesap",             "Hesaplar ve kimlikler",            Owner.Musteri, Owner.Musteri,  Owner.Musteri,    Owner.Musteri),
        new("kimlik-altyapisi",  "Kimlik ve directory altyapısı",    Owner.Musteri, Owner.Musteri,  Owner.Paylasimli, Owner.Paylasimli),
        new("uygulama",          "Uygulamalar",                      Owner.Musteri, Owner.Musteri,  Owner.Paylasimli, Owner.Microsoft),
        new("ag",                "Ağ kontrolleri",                   Owner.Musteri, Owner.Musteri,  Owner.Paylasimli, Owner.Microsoft),
        new("os",                "İşletim sistemi",                  Owner.Musteri, Owner.Musteri,  Owner.Microsoft,  Owner.Microsoft),
        new("fiziksel-host",     "Fiziksel host'lar",                Owner.Musteri, Owner.Microsoft, Owner.Microsoft, Owner.Microsoft),
        new("fiziksel-ag",       "Fiziksel ağ",                      Owner.Musteri, Owner.Microsoft, Owner.Microsoft, Owner.Microsoft),
        new("datacenter",        "Fiziksel datacenter",              Owner.Musteri, Owner.Microsoft, Owner.Microsoft, Owner.Microsoft),
    ];

    public static string Label(Owner owner) => owner switch
    {
        Owner.Musteri => "Müşteri",
        Owner.Paylasimli => "Paylaşımlı",
        _ => "Microsoft",
    };

    public static bool TryParseModel(string text, out DeploymentModel model)
    {
        model = text.ToLowerInvariant() switch
        {
            "onprem" or "on-prem" => DeploymentModel.OnPrem,
            "iaas" => DeploymentModel.IaaS,
            "paas" => DeploymentModel.PaaS,
            "saas" => DeploymentModel.SaaS,
            _ => (DeploymentModel)(-1),
        };
        return Enum.IsDefined(model);
    }

    /// <summary>`dotnet run -- C1.1 kim os paas` → "İşletim sistemi · PaaS → Microsoft".</summary>
    public static int Answer(string layerKey, string modelText)
    {
        var layer = Layers.FirstOrDefault(l => l.Key == layerKey.ToLowerInvariant());
        if (layer is null || !TryParseModel(modelText, out var model))
        {
            Console.Error.WriteLine("Bilinmeyen katman veya model. Katmanlar: " + string.Join(", ", Layers.Select(l => l.Key)) + " · modeller: onprem, iaas, paas, saas");
            return 1;
        }

        var owner = layer.For(model);
        Console.WriteLine($"{layer.Name} · {model} → {Label(owner)}");
        Console.WriteLine(owner switch
        {
            Owner.Musteri when layer.Key is "veri" or "cihaz" or "hesap" => "   Bu katman HER modelde müşteridedir — sınavın en sevdiği kural.",
            Owner.Microsoft when layer.Key.StartsWith("fiziksel") || layer.Key == "datacenter" => "   Fiziksel katmanlar bulutta (IaaS/PaaS/SaaS) her zaman Microsoft'tadır.",
            Owner.Paylasimli => "   Paylaşımlı: platformu Microsoft sağlar, yapılandırmasını ve doğru kullanımını müşteri üstlenir.",
            _ => "   Sorumluluk, modelin yönetim düzeyine göre kayar (IaaS → PaaS → SaaS geçtikçe Microsoft'a geçer).",
        });
        return 0;
    }
}
