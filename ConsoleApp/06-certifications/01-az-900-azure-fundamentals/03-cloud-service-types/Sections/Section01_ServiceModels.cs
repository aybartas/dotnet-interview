using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudServiceTypes;

public static class Section01_ServiceModels
{
    // The classic "application stack", top to bottom. The number = how many layers, down to the lowest one, the customer manages in that model.
    private static readonly string[] Stack = ["Application", "Data", "Runtime", "Middleware", "Operating system", "Virtualization", "Servers", "Storage", "Networking (physical)"];
    private static readonly (string Model, int CustomerLayers)[] Models = [("On-prem", 9), ("IaaS", 5), ("PaaS", 2), ("SaaS", 0)];

    public static void Run()
    {
        Console.WriteLine("Who manages what in the application stack (You = customer, MS = Microsoft):\n");
        Table.Print(["Layer", .. Models.Select(m => m.Model)],
            Stack.Select((layer, i) => (IReadOnlyList<string>)[layer, .. Models.Select(m => i < m.CustomerLayers ? "You" : "MS")]));

        Console.WriteLine("""

        Note: this classic stack view is a simplification. Even in SaaS, configuring data, accounts and access correctly
        is the customer's job (→ C1.1 shared responsibility table). The exam is based on the table in C1.1.
        """);

        Table.Print(["", "IaaS", "PaaS", "SaaS"],
        [
            ["What you rent", "Infrastructure: VM, network, storage", "Platform: runtime, DB, middleware", "A ready-made application"],
            ["Control/flexibility", "Most", "Medium", "Least"],
            ["Management burden", "Most", "Medium", "Least"],
            ["Typical scenario", "Lift-and-shift, dev/test, HPC, custom software", "Web/API, analytics/BI, IoT backend", "Email, office, CRM/ERP, collaboration"],
            ["Azure example", "Virtual Machines, VNet, Managed Disks", "App Service, Azure SQL Database, Functions", "Microsoft 365, Dynamics 365"],
        ]);

        Console.WriteLine("""

        Pizza analogy:
          • On-prem  = make it at home      (oven, ingredients, table — all yours)
          • IaaS     = buy ready dough, bake at home (kitchen/infrastructure is ready; recipe, baking, serving are yours)
          • PaaS     = order delivery       (the pizza arrives ready; table, drinks, presentation are yours — i.e. your code and data)
          • SaaS     = go to the restaurant (just eat — use the application)
        """);
    }
}
