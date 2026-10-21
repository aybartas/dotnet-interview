namespace Certification.AzureFundamentals.CloudServiceTypes;

public enum ServiceType { IaaS, PaaS, Serverless, SaaS }

/// <summary>Sınavda en sık "bu ürün hangi hizmet türü?" diye sorulan ürünler. Serverless, PaaS'ın bir alt türüdür.</summary>
public static class ServiceCatalog
{
    public sealed record AzureService(string Name, ServiceType Type, string Note, params string[] Aliases);

    public static IReadOnlyList<AzureService> All { get; } =
    [
        new("Azure Virtual Machines", ServiceType.IaaS, "OS'a tam erişim; yama, antivirüs, yedekleme sende", "vm", "virtual machine", "sanal makine"),
        new("Virtual Machine Scale Sets", ServiceType.IaaS, "Aynı VM'den çok sayıda, autoscale ile", "vmss", "scale set"),
        new("SQL Server on Azure VMs", ServiceType.IaaS, "VM'e kurulu SQL Server: OS ve SQL yaması sende", "sql vm", "sql server"),
        new("Azure Virtual Network", ServiceType.IaaS, "Ağ altyapısı: subnet, NSG, peering", "vnet", "network"),
        new("Azure Managed Disks", ServiceType.IaaS, "VM'lere bağlanan blok depolama", "disk"),
        new("Azure Storage (Blob, Files, Queue, Table)", ServiceType.IaaS, "Depolama altyapısı; sınav senaryolarında depolama/yedekleme = IaaS", "storage", "blob", "files"),
        new("Azure App Service", ServiceType.PaaS, "Web uygulaması/API barındırma; OS ve runtime Microsoft'ta", "app service", "web app"),
        new("Azure SQL Database", ServiceType.PaaS, "Yönetilen veritabanı; yama, yedek, HA Microsoft'ta", "sql database", "sql db", "sql"),
        new("Azure SQL Managed Instance", ServiceType.PaaS, "SQL Server'a neredeyse tam uyumlu yönetilen instance (taşımalar için)", "managed instance", "sql mi", "sql"),
        new("Azure Cosmos DB", ServiceType.PaaS, "Yönetilen NoSQL, global dağıtım; serverless kapasite modu da var", "cosmos"),
        new("Azure Kubernetes Service (AKS)", ServiceType.PaaS, "Yönetilen Kubernetes: control plane Microsoft'ta, node'lar senin aboneliğinde", "aks", "kubernetes"),
        new("Azure Container Instances", ServiceType.PaaS, "Tek container'ı VM yönetmeden çalıştırma", "aci", "container instances", "container"),
        new("Azure Functions", ServiceType.Serverless, "Olayla tetiklenen kod; çalışma süresi/çağrı başına ücret", "functions", "function"),
        new("Azure Logic Apps", ServiceType.Serverless, "Görsel (low-code) iş akışı ve entegrasyon", "logic apps", "logic app", "workflow"),
        new("Azure Container Apps", ServiceType.Serverless, "Serverless container platformu; sıfıra kadar ölçeklenir", "container apps", "container"),
        new("Azure Event Grid", ServiceType.Serverless, "Olay yönlendirme; olay başına ücret", "event grid"),
        new("Microsoft 365", ServiceType.SaaS, "Office uygulamaları, Exchange Online, SharePoint, Teams", "m365", "office 365", "office", "teams", "outlook"),
        new("Dynamics 365", ServiceType.SaaS, "CRM ve ERP uygulamaları", "dynamics", "crm", "erp"),
        new("Outlook.com", ServiceType.SaaS, "Hazır e-posta hizmeti", "e-posta", "email", "outlook"),
    ];

    public static string Label(ServiceType type) => type == ServiceType.Serverless ? "PaaS (serverless)" : type.ToString();

    /// <summary>`dotnet run -- 1.3 tur sql` → adı veya takma adı eşleşen tüm ürünler (bilerek birden fazla: SQL'in üç biçimi).</summary>
    public static int Lookup(string query)
    {
        var q = query.Trim().ToLowerInvariant();
        var matches = All.Where(s => s.Name.Contains(q, StringComparison.OrdinalIgnoreCase) || s.Aliases.Any(a => a.Contains(q))).ToList();
        if (q.Length == 0 || matches.Count == 0)
        {
            Console.Error.WriteLine($"Katalogda bulunamadı: '{query}'. Örnek: tur sql · tur functions · tur m365");
            return 1;
        }

        foreach (var s in matches) Console.WriteLine($"{s.Name,-42} → {Label(s.Type),-18} {s.Note}");
        if (matches.Select(m => m.Type).Distinct().Count() > 1)
            Console.WriteLine("\n   Aynı teknoloji farklı hizmet türlerinde sunulabilir: belirleyici olan ÜRÜN değil, YÖNETİM DÜZEYİ.");
        return 0;
    }
}
