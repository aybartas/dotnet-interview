using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudServiceTypes;

/// <summary>Phrases in scenario questions that point to the service type.</summary>
public static class Section03_Scenarios
{
    public static void Run()
    {
        Table.Print(["Phrase in the scenario", "Type", "Why"],
        [
            ["\"Full access to the operating system / a custom driver will be installed\"", "IaaS", "You manage the OS only in IaaS"],
            ["\"Move existing servers to the cloud unchanged\" (lift-and-shift)", "IaaS", "The application moves to a VM as is"],
            ["\"A temporary test environment should be spun up and torn down quickly\"", "IaaS", "VMs start in minutes, billing stops when deleted"],
            ["\"Developers should only write code, OS patching should not be their concern\"", "PaaS", "Runtime and OS are Microsoft's"],
            ["\"Managed database; backup and patching are automatic\"", "PaaS", "Azure SQL Database, Cosmos DB"],
            ["\"Code should run only when a file is uploaded, no charge when idle\"", "Serverless", "Event-triggered, billed per call"],
            ["\"Build a workflow between two SaaS apps without writing code\"", "Serverless", "Logic Apps (low-code)"],
            ["\"Users should start using email and office apps right away\"", "SaaS", "Ready-made application: Microsoft 365"],
            ["\"Least management burden, ready-made CRM\"", "SaaS", "Dynamics 365"],
        ]);

        Console.WriteLine("""

        Decision order (stop at the first "yes" from top to bottom):
          1. Is there a READY-MADE application that meets the need?             → SaaS
          2. Do you have your own code, and does it run on events/intermittently? → Serverless (Functions, Logic Apps)
          3. Do you have your own code, and no OS access is needed?             → PaaS (App Service, SQL Database)
          4. Do you need full control over OS/network or an as-is migration?    → IaaS (VM)
        Watch out for "least management burden": if a ready-made application exists, SaaS; if you will run your own code, PaaS/serverless.
        """);
    }
}
