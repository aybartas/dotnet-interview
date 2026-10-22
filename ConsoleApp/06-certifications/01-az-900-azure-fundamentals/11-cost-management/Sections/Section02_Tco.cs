namespace Certification.AzureFundamentals.CostManagement;

public static class Section02_Tco
{
    public static void Run()
    {
        TcoCalculator.Print(servers: 20);
        Console.WriteLine("Kendi sayınla: dotnet run -- C1.11 tco <sunucu sayısı>");
    }
}
