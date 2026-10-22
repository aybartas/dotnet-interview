using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudComputing;

public static class Section03_CloudModels
{
    private sealed record Scenario(string Description, string Answer, string Why);

    public static void Run()
    {
        Table.Print(["Model", "Who owns the infrastructure?", "Pro", "Con"],
        [
            ["Public cloud", "The provider; many customers share it", "No CapEx, fast, nearly unlimited scale, pay as you go", "No physical control or hardware choice"],
            ["Private cloud", "A single organization (own DC or dedicated)", "Full control, special compliance/regulation", "Hardware, maintenance, capacity still yours (CapEx)"],
            ["Hybrid cloud", "Both together, with a connection between them", "Flexibility: sensitive/legacy systems on-site, variable load in the cloud", "Management complexity, security of two environments"],
            ["Multicloud (context)", "More than one public provider", "Less vendor lock-in, pick the best service", "Different tools/skills; common management with Azure Arc"],
        ]);

        Scenario[] scenarios =
        [
            new("Regulation requires customer data to stay in the organization's own datacenter; the website gets 10x load during holiday campaigns.",
                "Hybrid", "Data stays on the private/on-prem side, sudden load goes to the cloud (cloud bursting)."),
            new("A startup has no upfront budget and wants to launch its product worldwide within weeks.",
                "Public", "No upfront investment, fast start, global regions."),
            new("A defense agency wants full physical control over hardware and network, and has restricted internet connectivity.",
                "Private", "Full control and isolation; cost and maintenance stay with the organization."),
            new("A company runs workloads on two different cloud providers and wants to manage everything from one place.",
                "Multicloud (+ Azure Arc)", "Arc manages non-Azure resources from Azure Resource Manager (→ C1.13)."),
        ];

        Console.WriteLine("\nScenario → model:");
        foreach (var s in scenarios)
        {
            Console.WriteLine($"   • {s.Description}");
            Console.WriteLine($"     → {s.Answer}: {s.Why}");
        }
    }
}
