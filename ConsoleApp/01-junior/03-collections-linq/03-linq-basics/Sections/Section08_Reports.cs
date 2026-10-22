using Junior.CollectionsLinq.LinqBasics.Data;
using Junior.CollectionsLinq.LinqBasics.Reports;

namespace Junior.CollectionsLinq.LinqBasics;

public static class Section08_Reports
{
    public static void Run()
    {
        var d = new ShopData();
        Console.WriteLine("Data: 6 customers · 10 products · 12 orders (9 delivered, 2 cancelled, 1 pending). Revenue = delivered orders only.");
        Console.WriteLine("First solve the question yourself in Reports/Reports.cs; the output below is the expected answer.\n");

        foreach (var r in ReportCatalog.All)
        {
            Console.WriteLine($"{r.No,2}. {r.Question}");
            Console.WriteLine($"    operators: {r.Operators}");
            Console.WriteLine($"    → {r.Render(d)}\n");
        }
    }
}
