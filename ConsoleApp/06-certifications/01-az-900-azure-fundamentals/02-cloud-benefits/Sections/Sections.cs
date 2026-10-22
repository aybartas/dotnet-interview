using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "The eight benefits: definition, how achieved, Azure example ⭐", Section01_Benefits.Run),
        new(2, "SLA → allowed downtime ⭐",                                      Section02_SlaDowntime.Run),
        new(3, "Composite SLA: single VM vs two zones, serial vs redundant",     Section03_CompositeSla.Run),
        new(4, "Scale up, scale out and elasticity — 24-hour simulation ⭐",     Section04_ScalingSimulation.Run),
        new(5, "Scenario → benefit matching (exam pattern)",                     Section05_BenefitScenarios.Run),
    ];
}
