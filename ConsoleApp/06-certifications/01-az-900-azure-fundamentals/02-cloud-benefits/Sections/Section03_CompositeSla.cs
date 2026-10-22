using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

/// <summary>
/// Composite SLA. The SLA values used are typical values published by Microsoft and are for ILLUSTRATION only;
/// for the current value see the "Service Level Agreements (SLA) for Online Services" document.
/// </summary>
public static class Section03_CompositeSla
{
    private const double SingleVmPremiumSsd = 0.999;     // single-instance VM, all disks Premium SSD
    private const double VmsInAvailabilitySet = 0.9995;  // 2+ VMs in the same availability set
    private const double VmsAcrossZones = 0.9999;        // 2+ VMs spread across 2+ availability zones
    private const double AppService = 0.9995;           // App Service (paid tier)
    private const double SqlDatabase = 0.9999;           // Azure SQL Database

    public static void Run()
    {
        Console.WriteLine("A) Same application, different placement — SLA of the VM tier:\n");
        Table.Print(["Placement", ..SlaCalculator.DowntimeHeaders],
        [
            ["Single VM (Premium SSD)", ..SlaCalculator.DowntimeRow(SingleVmPremiumSsd)],
            ["2 VMs, availability set", ..SlaCalculator.DowntimeRow(VmsInAvailabilitySet)],
            ["2 VMs, two availability zones", ..SlaCalculator.DowntimeRow(VmsAcrossZones)],
        ]);
        Console.WriteLine("   Going from a single VM to two zones cuts the monthly downtime budget from ~43 min to ~4 min — the price: twice the VMs.\n");

        var web = SlaCalculator.Serial([AppService, SqlDatabase]);
        Console.WriteLine($"""
        B) Components in series — web application (App Service {SlaCalculator.Percent(AppService)}) + database (SQL Database {SlaCalculator.Percent(SqlDatabase)}):
           BOTH must be up for the application to work → the SLAs are MULTIPLIED:
           {SlaCalculator.Percent(AppService)} × {SlaCalculator.Percent(SqlDatabase)} = {SlaCalculator.Percent(web)} ≈ 99.94%  (~{SlaCalculator.Human(SlaCalculator.DowntimePerMonth(web))} downtime per month)
           The result is LOWER than the SLA of either component: every link added to the chain pulls the total SLA down.

        """);

        var twoRegions = SlaCalculator.Parallel([web, web]);
        Console.WriteLine($"""
        C) Redundant (parallel) design — the same web + DB stack in two regions, the other takes over if one fails:
           1 − (1 − {SlaCalculator.Percent(web)}) × (1 − {SlaCalculator.Percent(web)}) = {SlaCalculator.Percent(twoRegions)}  (monthly downtime budget: {SlaCalculator.Human(SlaCalculator.DowntimePerMonth(twoRegions))})
           Assumptions: failures of the two regions are independent and the layer that routes traffic (e.g. Front Door,
           Traffic Manager) works. That router is also in series; its SLA must be multiplied into the result as well.

        Two rules to remember:
          • Serial (all required)    → SLAs are multiplied, the total SLA drops.
          • Parallel (one is enough) → 1 − (1−a)(1−b), the total SLA rises; so does the cost.
        AZ-900 rarely asks composite SLA calculations in detail; but the answer to "how do I raise the SLA?" is always redundancy.
        """);
    }
}
