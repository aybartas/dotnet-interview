using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudServiceTypes;

public static class Section01_ServiceModels
{
    // Klasik "uygulama yığını": üstten alta. Sayı = o modelde müşterinin yönettiği en alt katmana kadar olan katman sayısı.
    private static readonly string[] Stack = ["Uygulama", "Veri", "Runtime", "Middleware", "İşletim sistemi", "Sanallaştırma", "Sunucular", "Depolama", "Ağ (fiziksel)"];
    private static readonly (string Model, int CustomerLayers)[] Models = [("On-prem", 9), ("IaaS", 5), ("PaaS", 2), ("SaaS", 0)];

    public static void Run()
    {
        Console.WriteLine("Uygulama yığınında kimin neyi yönettiği (Sen = müşteri, MS = Microsoft):\n");
        Table.Print(["Katman", .. Models.Select(m => m.Model)],
            Stack.Select((layer, i) => (IReadOnlyList<string>)[layer, .. Models.Select(m => i < m.CustomerLayers ? "Sen" : "MS")]));

        Console.WriteLine("""

        Not: Bu klasik yığın gösterimi bir sadeleştirmedir. SaaS'ta bile verinin, hesapların ve erişimin doğru
        yapılandırılması müşteridedir (→ C1.1 shared responsibility tablosu). Sınav C1.1'deki tabloyu temel alır.
        """);

        Table.Print(["", "IaaS", "PaaS", "SaaS"],
        [
            ["Ne kiralarsın", "Altyapı: VM, ağ, depolama", "Platform: runtime, DB, ara katman", "Hazır uygulama"],
            ["Kontrol/esneklik", "En fazla", "Orta", "En az"],
            ["Yönetim yükü", "En fazla", "Orta", "En az"],
            ["Tipik senaryo", "Lift-and-shift, dev/test, HPC, özel yazılım", "Web/API, analitik/BI, IoT backend", "E-posta, ofis, CRM/ERP, iş birliği"],
            ["Azure örneği", "Virtual Machines, VNet, Managed Disks", "App Service, Azure SQL Database, Functions", "Microsoft 365, Dynamics 365"],
        ]);

        Console.WriteLine("""

        Pizza benzetmesi:
          • On-prem  = evde yap      (fırın, malzeme, masa — hepsi senin)
          • IaaS     = hazır hamur al, evde pişir (mutfak/altyapı hazır; tarif, pişirme, servis senin)
          • PaaS     = eve sipariş   (pizza hazır gelir; masa, içecek, sunum senin — yani kodun ve verin)
          • SaaS     = restorana git (yalnızca ye — uygulamayı kullan)
        """);
    }
}
