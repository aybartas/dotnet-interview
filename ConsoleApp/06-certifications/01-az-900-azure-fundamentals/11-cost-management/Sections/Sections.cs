using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CostManagement;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Factors that affect cost ⭐",                                    Section01_CostFactors.Run),
        new(2, "3-year TCO: CapEx (on-prem) vs OpEx (Azure)",                    Section02_Tco.Run),
        new(3, "Ways to save: reservation, savings plan, spot, Hybrid Benefit…", Section03_Savings.Run),
        new(4, "Tags and a tag-based cost report (not inherited!) ⭐",           Section04_Tags.Run),
    ];
}
