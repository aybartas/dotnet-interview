namespace Certification.AzureFundamentals.CloudServiceTypes;

public enum ServiceType { IaaS, PaaS, Serverless, SaaS }

/// <summary>The products the exam most often asks "which service type is this?" about. Serverless is a sub-type of PaaS.</summary>
public static class ServiceCatalog
{
    public sealed record AzureService(string Name, ServiceType Type, string Note, params string[] Aliases);

    public static IReadOnlyList<AzureService> All { get; } =
    [
        new("Azure Virtual Machines", ServiceType.IaaS, "Full access to the OS; patching, antivirus, backup are yours", "vm", "virtual machine", "virtual machines"),
        new("Virtual Machine Scale Sets", ServiceType.IaaS, "Many identical VMs, with autoscale", "vmss", "scale set"),
        new("SQL Server on Azure VMs", ServiceType.IaaS, "SQL Server installed on a VM: OS and SQL patching are yours", "sql vm", "sql server"),
        new("Azure Virtual Network", ServiceType.IaaS, "Network infrastructure: subnet, NSG, peering", "vnet", "network"),
        new("Azure Managed Disks", ServiceType.IaaS, "Block storage attached to VMs", "disk"),
        new("Azure Storage (Blob, Files, Queue, Table)", ServiceType.IaaS, "Storage infrastructure; in exam scenarios storage/backup = IaaS", "storage", "blob", "files"),
        new("Azure App Service", ServiceType.PaaS, "Hosts web apps/APIs; OS and runtime are Microsoft's", "app service", "web app"),
        new("Azure SQL Database", ServiceType.PaaS, "Managed database; patching, backup, HA are Microsoft's", "sql database", "sql db", "sql"),
        new("Azure SQL Managed Instance", ServiceType.PaaS, "Managed instance almost fully compatible with SQL Server (for migrations)", "managed instance", "sql mi", "sql"),
        new("Azure Cosmos DB", ServiceType.PaaS, "Managed NoSQL, global distribution; also has a serverless capacity mode", "cosmos"),
        new("Azure Kubernetes Service (AKS)", ServiceType.PaaS, "Managed Kubernetes: control plane is Microsoft's, nodes are in your subscription", "aks", "kubernetes"),
        new("Azure Container Instances", ServiceType.PaaS, "Run a single container without managing VMs", "aci", "container instances", "container"),
        new("Azure Functions", ServiceType.Serverless, "Event-triggered code; billed per execution time/call", "functions", "function"),
        new("Azure Logic Apps", ServiceType.Serverless, "Visual (low-code) workflows and integration", "logic apps", "logic app", "workflow"),
        new("Azure Container Apps", ServiceType.Serverless, "Serverless container platform; scales to zero", "container apps", "container"),
        new("Azure Event Grid", ServiceType.Serverless, "Event routing; billed per event", "event grid"),
        new("Microsoft 365", ServiceType.SaaS, "Office apps, Exchange Online, SharePoint, Teams", "m365", "office 365", "office", "teams", "outlook"),
        new("Dynamics 365", ServiceType.SaaS, "CRM and ERP applications", "dynamics", "crm", "erp"),
        new("Outlook.com", ServiceType.SaaS, "Ready-made email service", "mail", "email", "outlook"),
    ];

    public static string Label(ServiceType type) => type == ServiceType.Serverless ? "PaaS (serverless)" : type.ToString();

    /// <summary>`dotnet run -- C1.3 lookup sql` → all products whose name or alias matches (deliberately more than one: the three forms of SQL).</summary>
    public static int Lookup(string query)
    {
        var q = query.Trim().ToLowerInvariant();
        var matches = All.Where(s => s.Name.Contains(q, StringComparison.OrdinalIgnoreCase) || s.Aliases.Any(a => a.Contains(q))).ToList();
        if (q.Length == 0 || matches.Count == 0)
        {
            Console.Error.WriteLine($"Not found in the catalog: '{query}'. Examples: lookup sql · lookup functions · lookup m365");
            return 1;
        }

        foreach (var s in matches) Console.WriteLine($"{s.Name,-42} → {Label(s.Type),-18} {s.Note}");
        if (matches.Select(m => m.Type).Distinct().Count() > 1)
            Console.WriteLine("\n   The same technology can be offered in different service types: what decides is not the PRODUCT but the LEVEL OF MANAGEMENT.");
        return 0;
    }
}
