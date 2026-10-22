namespace Certification.AzureFundamentals.CloudComputing;

public enum Owner { Customer, Shared, Microsoft }

public enum DeploymentModel { OnPrem, IaaS, PaaS, SaaS }

/// <summary>Shared responsibility model — this repo's version of the layer table Microsoft publishes (README 3.2).</summary>
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
        new("data",              "Information and data",                  Owner.Customer, Owner.Customer,  Owner.Customer,  Owner.Customer),
        new("device",            "Devices (mobile and PC)",               Owner.Customer, Owner.Customer,  Owner.Customer,  Owner.Customer),
        new("account",           "Accounts and identities",               Owner.Customer, Owner.Customer,  Owner.Customer,  Owner.Customer),
        new("identity-infra",    "Identity and directory infrastructure", Owner.Customer, Owner.Customer,  Owner.Shared,    Owner.Shared),
        new("app",               "Applications",                          Owner.Customer, Owner.Customer,  Owner.Shared,    Owner.Microsoft),
        new("network",           "Network controls",                      Owner.Customer, Owner.Customer,  Owner.Shared,    Owner.Microsoft),
        new("os",                "Operating system",                      Owner.Customer, Owner.Customer,  Owner.Microsoft, Owner.Microsoft),
        new("physical-host",     "Physical hosts",                        Owner.Customer, Owner.Microsoft, Owner.Microsoft, Owner.Microsoft),
        new("physical-network",  "Physical network",                      Owner.Customer, Owner.Microsoft, Owner.Microsoft, Owner.Microsoft),
        new("datacenter",        "Physical datacenter",                   Owner.Customer, Owner.Microsoft, Owner.Microsoft, Owner.Microsoft),
    ];

    public static string Label(Owner owner) => owner switch
    {
        Owner.Customer => "Customer",
        Owner.Shared => "Shared",
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

    /// <summary>`dotnet run -- C1.1 who os paas` → "Operating system · PaaS → Microsoft".</summary>
    public static int Answer(string layerKey, string modelText)
    {
        var layer = Layers.FirstOrDefault(l => l.Key == layerKey.ToLowerInvariant());
        if (layer is null || !TryParseModel(modelText, out var model))
        {
            Console.Error.WriteLine("Unknown layer or model. Layers: " + string.Join(", ", Layers.Select(l => l.Key)) + " · models: onprem, iaas, paas, saas");
            return 1;
        }

        var owner = layer.For(model);
        Console.WriteLine($"{layer.Name} · {model} → {Label(owner)}");
        Console.WriteLine(owner switch
        {
            Owner.Customer when layer.Key is "data" or "device" or "account" => "   This layer belongs to the customer in EVERY model — the exam's favorite rule.",
            Owner.Microsoft when layer.Key.StartsWith("physical") || layer.Key == "datacenter" => "   Physical layers always belong to Microsoft in the cloud (IaaS/PaaS/SaaS).",
            Owner.Shared => "   Shared: Microsoft provides the platform, the customer owns its configuration and correct use.",
            _ => "   Responsibility shifts with the management level of the model (moves to Microsoft as you go IaaS → PaaS → SaaS).",
        });
        return 0;
    }
}
