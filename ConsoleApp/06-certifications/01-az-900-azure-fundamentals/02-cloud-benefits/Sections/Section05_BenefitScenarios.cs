using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

/// <summary>Benefit questions on the exam are mostly of the form "which benefit does this scenario describe?". Catch the keyword.</summary>
public static class Section05_BenefitScenarios
{
    public static void Run()
    {
        Console.WriteLine("Key phrase in the scenario → correct benefit:\n");
        Table.Print(["Scenario", "Key phrase", "Benefit"],
        [
            ["Power failed in one datacenter, the site stayed up from another zone", "keeps running despite the outage", "High availability"],
            ["For a campaign, capacity could be raised from 2 VMs to 20 VMs", "capacity can be added", "Scalability"],
            ["The instance count rose by itself when CPU exceeded 70%", "automatic, according to load", "Elasticity"],
            ["After a regional disaster the system came back up in the paired region", "recovering from disaster", "Reliability"],
            ["Next quarter's cloud bill could be forecast", "forecasting cost in advance", "Predictability (cost)"],
            ["Response time stayed on target even as traffic grew (autoscale + LB)", "forecasting performance in advance", "Predictability (performance)"],
            ["In PaaS, Microsoft applied the OS patches automatically", "patches/protection on the provider", "Security"],
            ["Resources may only be created in EU regions", "compliance with standards/regulation", "Governance"],
            ["Resources are deployed from templates, alerts fire on metrics", "management of the cloud (of)", "Manageability"],
            ["The team manages resources through the portal, CLI and REST API", "management in the cloud (in)", "Manageability"],
        ]);

        Console.WriteLine("""

        Tactic: look at the verb first — "kept running" (HA), "recovered" (reliability), "could be added" (scalability),
        "by itself/automatic" (elasticity), "was forecast" (predictability), "compliance/rule" (governance),
        "protection/patch/threat" (security), "tool/monitoring/template" (manageability).
        """);
    }
}
