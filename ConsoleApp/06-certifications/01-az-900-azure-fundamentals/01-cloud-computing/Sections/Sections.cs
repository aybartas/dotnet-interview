using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudComputing;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "What is cloud computing? (NIST's 5 characteristics)",   Section01_WhatIsCloud.Run),
        new(2, "Shared responsibility model ⭐",                         Section02_SharedResponsibility.Run),
        new(3, "Public, private, hybrid cloud — scenario matching",     Section03_CloudModels.Run),
        new(4, "Consumption-based model: CapEx vs OpEx ⭐",              Section04_ConsumptionModel.Run),
        new(5, "Cloud pricing models: pay-as-you-go, reserved, spot",   Section05_PricingModels.Run),
    ];
}
