using Junior.CollectionsLinq.LinqBasics.Data;
using Junior.CollectionsLinq.LinqBasics.Reports;

namespace Junior.CollectionsLinq.LinqBasics;

public static class Section08_Reports
{
    public static void Run()
    {
        var d = new ShopData();
        Console.WriteLine("Veri: 6 müşteri · 10 ürün · 12 sipariş (9 teslim edildi, 2 iptal, 1 bekliyor). Ciro = yalnızca teslim edilenler.");
        Console.WriteLine("Önce Reports/Reports.cs'te soruyu kendiniz çözün; aşağıdaki çıktı beklenen cevaptır.\n");

        foreach (var r in ReportCatalog.All)
        {
            Console.WriteLine($"{r.No,2}. {r.Question}");
            Console.WriteLine($"    operatörler: {r.Operators}");
            Console.WriteLine($"    → {r.Render(d)}\n");
        }
    }
}
