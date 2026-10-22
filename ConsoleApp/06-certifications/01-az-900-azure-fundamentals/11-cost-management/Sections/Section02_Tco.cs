namespace Certification.AzureFundamentals.CostManagement;

public static class Section02_Tco
{
    public static void Run()
    {
        TcoCalculator.Print(servers: 20);
        Console.WriteLine("With your own number: dotnet run -- C1.11 tco <server count>");
    }
}
