using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Sekiz fayda: tanım, nasıl sağlanır, Azure örneği ⭐",     Section01_Benefits.Run),
        new(2, "SLA → izin verilen kesinti süresi ⭐",                    Section02_SlaDowntime.Run),
        new(3, "Composite SLA: tek VM vs iki zone, seri vs yedekli",      Section03_CompositeSla.Run),
        new(4, "Scale up, scale out ve elasticity — 24 saatlik simülasyon ⭐", Section04_ScalingSimulation.Run),
        new(5, "Senaryo → fayda eşleştirme (sınav kalıbı)",               Section05_BenefitScenarios.Run),
    ];
}
