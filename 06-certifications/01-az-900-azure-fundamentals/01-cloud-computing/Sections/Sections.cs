using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudComputing;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Cloud computing nedir? (NIST'in 5 niteliği)",         Section01_WhatIsCloud.Run),
        new(2, "Shared responsibility model ⭐",                       Section02_SharedResponsibility.Run),
        new(3, "Public, private, hybrid cloud — senaryo eşleştirme",   Section03_CloudModels.Run),
        new(4, "Consumption-based model: CapEx vs OpEx ⭐",            Section04_ConsumptionModel.Run),
        new(5, "Cloud fiyatlandırma modelleri: pay-as-you-go, reserved, spot", Section05_PricingModels.Run),
    ];
}
