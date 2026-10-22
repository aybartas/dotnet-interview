using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudServiceTypes;

public static class Section02_ProductCatalog
{
    public static void Run()
    {
        foreach (var group in ServiceCatalog.All.GroupBy(s => s.Type))
        {
            Console.WriteLine($"\n{ServiceCatalog.Label(group.Key)}");
            Table.Print(["Product", "Why this type?"], group.Select(s => (IReadOnlyList<string>)[s.Name, s.Note]));
        }

        Console.WriteLine("""

        Exam traps:
          • "SQL" alone is not a type: SQL Server on a VM = IaaS · Azure SQL Database / Managed Instance = PaaS.
          • Azure Functions and Logic Apps fit both the "PaaS" and the "serverless" option; if the question says "serverless", pick that.
          • Microsoft 365 is not an Azure service but often appears on the exam as a SaaS example.
          • In AKS the node VMs run in your subscription; it is still counted as PaaS because it is a managed service.

        Ask about your own product: dotnet run -- C1.3 lookup sql
        """);
    }
}
