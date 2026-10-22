using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

public static class Section01_Benefits
{
    public static void Run()
    {
        Console.WriteLine("The eight benefits in the official objective, as four pairs:\n");
        Table.Print(["Benefit", "One-sentence definition", "Azure example"],
        [
            ["High availability", "The service stays within the target uptime even when failures occur", "SLA, availability zone, scale set"],
            ["Scalability",       "Capacity can be increased or decreased with demand (up/out)",        "VM size, VM Scale Sets, App Service plan"],
            ["Reliability",       "Recovers from failures/disasters and keeps running",                 "Region pair, GRS storage, Site Recovery"],
            ["Predictability",    "BOTH performance AND cost can be forecast in advance",              "Autoscale + LB / Pricing Calculator, Cost Mgmt"],
            ["Security",          "The desired level of control over security controls",                "Defender for Cloud, Key Vault, DDoS Protection"],
            ["Governance",        "Compliance with corporate standards and regulations",                "Azure Policy, management group, Purview"],
            ["Manageability",     "Management of the cloud (of) and management in the cloud (in)",      "Monitor, ARM/Bicep / portal, CLI, PowerShell"],
            ["Elasticity",        "Scaling done AUTOMATICALLY according to load",                       "Autoscale rules, Functions"],
        ]);

        Console.WriteLine("""

        Commonly confused pairs (the exam offers these as options against each other):
          • High availability ↔ Reliability
              HA = KEEPING running (shrinking downtime, SLA).
              Reliability = RECOVERING from failure (distributed design, disaster recovery).
          • Scalability ↔ Elasticity
              Scalability = ABILITY (resources can be added/removed; can be manual too).
              Elasticity  = using that ability AUTOMATICALLY according to load.
          • Security ↔ Governance
              Security = protection against attack and the level of control (a lot in IaaS, little in SaaS).
              Governance = compliance with rules/standards and auditing (Policy, templates, compliance report).
          • Predictability has TWO faces: performance predictability AND cost predictability.
          • Manageability has TWO faces:
              management OF the cloud = managing resources: autoscaling, monitoring/alerts, template deployment.
              management IN the cloud = management interfaces: portal, CLI, PowerShell, REST API.
        """);
    }
}
