using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CostManagement;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Maliyeti etkileyen faktörler ⭐",                         Section01_CostFactors.Run),
        new(2, "3 yıllık TCO: CapEx (on-prem) vs OpEx (Azure)",          Section02_Tco.Run),
        new(3, "Tasarruf yolları: reservation, savings plan, spot, Hybrid Benefit…", Section03_Savings.Run),
        new(4, "Tag'ler ve tag bazlı maliyet raporu (miras alınmaz!) ⭐", Section04_Tags.Run),
    ];
}
