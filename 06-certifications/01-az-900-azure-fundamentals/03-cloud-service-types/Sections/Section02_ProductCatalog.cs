using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudServiceTypes;

public static class Section02_ProductCatalog
{
    public static void Run()
    {
        foreach (var group in ServiceCatalog.All.GroupBy(s => s.Type))
        {
            Console.WriteLine($"\n{ServiceCatalog.Label(group.Key)}");
            Table.Print(["Ürün", "Neden bu tür?"], group.Select(s => (IReadOnlyList<string>)[s.Name, s.Note]));
        }

        Console.WriteLine("""

        Sınav tuzakları:
          • "SQL" tek başına bir tür değildir: VM üzerinde SQL Server = IaaS · Azure SQL Database / Managed Instance = PaaS.
          • Azure Functions ve Logic Apps hem "PaaS" hem "serverless" şıkkına uyar; soru "serverless" diyorsa onu seç.
          • Microsoft 365 bir Azure hizmeti değil ama SaaS örneği olarak sınavda sık geçer.
          • AKS'de node VM'leri senin aboneliğinde çalışır; yine de yönetilen hizmet olduğu için PaaS kabul edilir.

        Kendi ürününü sor: dotnet run -- 1.3 tur sql
        """);
    }
}
