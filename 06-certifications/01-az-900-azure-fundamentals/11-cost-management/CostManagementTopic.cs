using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CostManagement;

public static class CostManagementTopic
{
    public static int Run(string[] args) => args switch
    {
        ["tco", var servers] when int.TryParse(servers, out var n) && n is > 0 and <= 10_000 => TcoCalculator.Print(n),
        ["tco", ..] => Fail(),
        _ => SectionRunner.Run("1.11", "Azure'da Maliyet Yönetimi", Sections.All, args,
            "TCO hesaplayıcısı: dotnet run -- 1.11 tco <sunucu sayısı>   (ör. tco 20; fiyatlar örnektir)"),
    };

    private static int Fail()
    {
        Console.Error.WriteLine("Kullanım: dotnet run -- 1.11 tco <sunucu sayısı>  (1–10000)");
        return 1;
    }
}
