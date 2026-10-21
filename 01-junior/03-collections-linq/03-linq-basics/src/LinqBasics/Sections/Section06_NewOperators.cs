using Junior.CollectionsLinq.LinqBasics.Data;

namespace Junior.CollectionsLinq.LinqBasics;

public static class Section06_NewOperators
{
    public static void Run()
    {
        var d = new ShopData();

        Console.WriteLine("— .NET 9 —");
        Console.WriteLine("CountBy: anahtara göre sayım (GroupBy + Count yerine; ara IGrouping nesneleri üretmez)");
        Console.WriteLine($"   eski : {string.Join(", ", d.Customers.GroupBy(c => c.City).Select(g => $"{g.Key}={g.Count()}"))}");
        Console.WriteLine($"   yeni : {string.Join(", ", d.Customers.CountBy(c => c.City).Select(kv => $"{kv.Key}={kv.Value}"))}");

        Console.WriteLine("\nAggregateBy: anahtara göre toplama (GroupBy + Aggregate yerine). Örnek — şehir başına toplam sipariş sayısı:");
        var ordersByCity = d.Orders.AggregateBy(
            keySelector: o => d.Customers.First(c => c.Id == o.CustomerId).City,
            seed: 0,
            func: (acc, _) => acc + 1);
        Console.WriteLine($"   {string.Join(", ", ordersByCity.Select(kv => $"{kv.Key}={kv.Value}"))}");
        var revenueByCategory = d.Items.AggregateBy(
            i => d.Products.First(p => p.Id == i.ProductId).Category,
            0m,
            (sum, i) => sum + i.Total);
        Console.WriteLine($"   kategori başına kalem toplamı: {string.Join(", ", revenueByCategory.Select(kv => $"{kv.Key}={kv.Value:N0}"))}");

        Console.WriteLine("\nIndex: elemanı indeksiyle birlikte ver — `Select((x, i) => (i, x))` yerine:");
        Console.WriteLine($"   {string.Join(", ", d.Products.Take(3).Select(p => p.Name).Index().Select(t => $"{t.Index}:{t.Item}"))}");
        foreach (var (index, product) in d.Products.Take(2).Index())
            Console.WriteLine($"   foreach (var (index, item) in source.Index()) → {index}: {product.Name}");

        Console.WriteLine("\n— .NET 10 —");
        Console.WriteLine("LeftJoin: sol tarafın TÜM elemanları + eşleşenler (eşleşmeyen sağ taraf default/null) — eskiden GroupJoin + SelectMany + DefaultIfEmpty üçlüsü gerekirdi:");
        var left = d.Customers.LeftJoin(d.Orders, c => c.Id, o => o.CustomerId, (c, o) => (c.Name, OrderId: o?.Id));
        Console.WriteLine($"   LeftJoin: Can Öztürk (siparişsiz) → {left.Single(x => x.Name == "Can Öztürk")}");
        var legacy = d.Customers.GroupJoin(d.Orders, c => c.Id, o => o.CustomerId, (c, os) => (c, os))
                                .SelectMany(x => x.os.DefaultIfEmpty(), (x, o) => (x.c.Name, OrderId: (int?)o?.Id));
        Console.WriteLine($"   eski yazım (GroupJoin+SelectMany+DefaultIfEmpty) aynı sonucu verir mi? {left.SequenceEqual(legacy)}");

        Console.WriteLine("RightJoin: sağ tarafın TÜM elemanları + eşleşen sol (simetrik):");
        var right = d.Orders.RightJoin(d.Customers, o => o.CustomerId, c => c.Id, (o, c) => (c.Name, OrderId: o?.Id));
        Console.WriteLine($"   RightJoin satır sayısı = {right.Count()} (12 sipariş eşleşmesi + siparişsiz müşteri 1)");

        Console.WriteLine("""

        Not: Bu operatörler hedef framework'e bağlıdır — .NET 9/10 SDK + net9.0/net10.0 TFM gerekir (eski TFM'de derlenmez; çoklu hedefte #if NET10_0_OR_GREATER).
        EF Core tarafında LeftJoin/RightJoin'in SQL çevirisi sürüme bağlıdır (→ J9).
        """);
    }
}
