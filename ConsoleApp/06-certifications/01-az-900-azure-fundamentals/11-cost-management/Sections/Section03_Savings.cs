using System.Globalization;
using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CostManagement;

/// <summary>Different ways to save on the same monthly VM bill. The discount rates are EXAMPLES.</summary>
public static class Section03_Savings
{
    public static void Run()
    {
        const decimal monthlyBill = 10_000m;  // pay-as-you-go bill of a 24/7 Windows VM fleet (example)
        Console.WriteLine(string.Create(CultureInfo.InvariantCulture, $"Starting point: a monthly pay-as-you-go VM bill of {monthlyBill:N0} $ (24/7, Windows, production).\n"));

        Table.Print(["Method", "How?", "Example new bill", "Suitable workload / condition"],
        [
            ["Reservation (1/3 years)", "Commit to a specific VM size + region", Bill(monthlyBill, 0.55m), "Steady, long-lived; paid even if unused"],
            ["Savings plan for compute", "1/3-year commitment to an hourly spend amount", Bill(monthlyBill, 0.45m), "Size/region may change but compute is continuous"],
            ["Azure Hybrid Benefit", "Use your existing Windows Server/SQL Server license", Bill(monthlyBill, 0.30m), "Licenses with Software Assurance"],
            ["Spot VM", "Spare capacity; Azure can reclaim it", Bill(monthlyBill, 0.80m), "Only interruption-tolerant jobs"],
            ["Dev/test pricing", "Discounted rates on dev/test subscriptions", Bill(monthlyBill, 0.40m), "Only NON-production environments"],
            ["Automatic shutdown", "Deallocate the VM outside business hours (12 h/day, weekdays)", Bill(monthlyBill, 1 - (12m * 5) / (24m * 7)), "Development/test environments"],
            ["Right-sizing", "Advisor's recommendation for underused VMs (→ C1.14)", Bill(monthlyBill, 0.50m), "VMs with constantly low CPU (go one size down)"],
        ]);

        Console.WriteLine("""

        Combining: reservation + Hybrid Benefit can be used together (the discounts are applied multiplicatively).
        Spot is NOT the right answer for a 24/7 critical production job; dev/test pricing cannot be used in production.
        """);
    }

    private static string Bill(decimal bill, decimal discount) =>
        string.Create(CultureInfo.InvariantCulture, $"{bill * (1 - discount):N0} $  (−{discount * 100:0}%)");
}
