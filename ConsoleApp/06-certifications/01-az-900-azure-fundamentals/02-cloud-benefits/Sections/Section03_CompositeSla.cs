using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

/// <summary>
/// Composite SLA. Kullanılan SLA değerleri, Microsoft'un yayımladığı tipik değerlerdir ve ÖRNEK amaçlıdır;
/// güncel değer için "Service Level Agreements (SLA) for Online Services" belgesine bakılır.
/// </summary>
public static class Section03_CompositeSla
{
    private const double SingleVmPremiumSsd = 0.999;     // tek instance VM, tüm diskler Premium SSD
    private const double VmsInAvailabilitySet = 0.9995;  // aynı availability set'te 2+ VM
    private const double VmsAcrossZones = 0.9999;        // 2+ availability zone'a yayılmış 2+ VM
    private const double AppService = 0.9995;           // App Service (ücretli katman)
    private const double SqlDatabase = 0.9999;           // Azure SQL Database

    public static void Run()
    {
        Console.WriteLine("A) Aynı uygulama, farklı yerleşim — VM katmanının SLA'sı:\n");
        Table.Print(["Yerleşim", ..SlaCalculator.DowntimeHeaders],
        [
            ["Tek VM (Premium SSD)", ..SlaCalculator.DowntimeRow(SingleVmPremiumSsd)],
            ["2 VM, availability set", ..SlaCalculator.DowntimeRow(VmsInAvailabilitySet)],
            ["2 VM, iki availability zone", ..SlaCalculator.DowntimeRow(VmsAcrossZones)],
        ]);
        Console.WriteLine("   Tek VM'den iki zone'a geçmek aylık kesinti bütçesini ~43 dk'dan ~4 dk'ya indirir — bedeli: iki kat VM.\n");

        var web = SlaCalculator.Serial([AppService, SqlDatabase]);
        Console.WriteLine($"""
        B) Seri bağlı bileşenler — web uygulaması (App Service {SlaCalculator.Percent(AppService)}) + veritabanı (SQL Database {SlaCalculator.Percent(SqlDatabase)}):
           Uygulamanın çalışması için İKİSİ de ayakta olmalı → SLA'lar ÇARPILIR:
           {SlaCalculator.Percent(AppService)} × {SlaCalculator.Percent(SqlDatabase)} = {SlaCalculator.Percent(web)} ≈ %99,94  (aylık ~{SlaCalculator.Human(SlaCalculator.DowntimePerMonth(web))} kesinti)
           Sonuç her iki bileşenin SLA'sından da DÜŞÜK: zincire eklenen her halka toplam SLA'yı aşağı çeker.

        """);

        var twoRegions = SlaCalculator.Parallel([web, web]);
        Console.WriteLine($"""
        C) Yedekli (paralel) yapı — aynı web + DB yığını iki bölgede, biri düşerse diğeri devralır:
           1 − (1 − {SlaCalculator.Percent(web)}) × (1 − {SlaCalculator.Percent(web)}) = {SlaCalculator.Percent(twoRegions)}  (aylık kesinti bütçesi: {SlaCalculator.Human(SlaCalculator.DowntimePerMonth(twoRegions))})
           Varsayımlar: iki bölgenin arızası birbirinden bağımsız ve trafiği yönlendiren katman (ör. Front Door,
           Traffic Manager) çalışıyor. O yönlendirici de seri bağlıdır; onun SLA'sı da sonuçla çarpılmalıdır.

        Akılda kalacak iki kural:
          • Seri (hepsi gerekli)    → SLA'lar çarpılır, toplam SLA düşer.
          • Paralel (biri yeterli)  → 1 − (1−a)(1−b), toplam SLA yükselir; maliyet de yükselir.
        AZ-900 composite SLA hesabını nadiren ayrıntılı sorar; ama "SLA'yı nasıl yükseltirim?" sorusunun cevabı her zaman yedekliliktir.
        """);
    }
}
