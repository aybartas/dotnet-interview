using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CostManagement;

public static class CostManagementTopic
{
    public static int Run(string[] args) => args switch
    {
        ["tco", var servers] when int.TryParse(servers, out var n) && n is > 0 and <= 10_000 => TcoCalculator.Print(n),
        ["tco", ..] => Fail(),
        _ => SectionRunner.Run("1.11", "Cost Management in Azure", Sections.All, args,
            "TCO calculator: dotnet run -- C1.11 tco <server count>   (e.g. tco 20; prices are examples)"),
    };

    private static int Fail()
    {
        Console.Error.WriteLine("Usage: dotnet run -- C1.11 tco <server count>  (1–10000)");
        return 1;
    }
}
