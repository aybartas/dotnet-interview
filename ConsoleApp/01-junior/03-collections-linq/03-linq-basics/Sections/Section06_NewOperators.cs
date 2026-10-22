using Junior.CollectionsLinq.LinqBasics.Data;

namespace Junior.CollectionsLinq.LinqBasics;

public static class Section06_NewOperators
{
    public static void Run()
    {
        var d = new ShopData();

        Console.WriteLine("— .NET 9 —");
        Console.WriteLine("CountBy: count by key (instead of GroupBy + Count; does not create intermediate IGrouping objects)");
        Console.WriteLine($"   old : {string.Join(", ", d.Customers.GroupBy(c => c.City).Select(g => $"{g.Key}={g.Count()}"))}");
        Console.WriteLine($"   new : {string.Join(", ", d.Customers.CountBy(c => c.City).Select(kv => $"{kv.Key}={kv.Value}"))}");

        Console.WriteLine("\nAggregateBy: aggregate by key (instead of GroupBy + Aggregate). Example — total number of orders per city:");
        var ordersByCity = d.Orders.AggregateBy(
            keySelector: o => d.Customers.First(c => c.Id == o.CustomerId).City,
            seed: 0,
            func: (acc, _) => acc + 1);
        Console.WriteLine($"   {string.Join(", ", ordersByCity.Select(kv => $"{kv.Key}={kv.Value}"))}");
        var revenueByCategory = d.Items.AggregateBy(
            i => d.Products.First(p => p.Id == i.ProductId).Category,
            0m,
            (sum, i) => sum + i.Total);
        Console.WriteLine(FormattableString.Invariant($"   item total per category: {string.Join(", ", revenueByCategory.Select(kv => $"{kv.Key}={kv.Value:N0}"))}"));

        Console.WriteLine("\nIndex: gives the element together with its index — instead of `Select((x, i) => (i, x))`:");
        Console.WriteLine($"   {string.Join(", ", d.Products.Take(3).Select(p => p.Name).Index().Select(t => $"{t.Index}:{t.Item}"))}");
        foreach (var (index, product) in d.Products.Take(2).Index())
            Console.WriteLine($"   foreach (var (index, item) in source.Index()) → {index}: {product.Name}");

        Console.WriteLine("\n— .NET 10 —");
        Console.WriteLine("LeftJoin: ALL elements of the left side + the matches (the unmatched right side is default/null) — previously you needed the GroupJoin + SelectMany + DefaultIfEmpty trio:");
        var left = d.Customers.LeftJoin(d.Orders, c => c.Id, o => o.CustomerId, (c, o) => (c.Name, OrderId: o?.Id));
        Console.WriteLine($"   LeftJoin: Frank Miller (no orders) → {left.Single(x => x.Name == "Frank Miller")}");
        var legacy = d.Customers.GroupJoin(d.Orders, c => c.Id, o => o.CustomerId, (c, os) => (c, os))
                                .SelectMany(x => x.os.DefaultIfEmpty(), (x, o) => (x.c.Name, OrderId: (int?)o?.Id));
        Console.WriteLine($"   does the old form (GroupJoin+SelectMany+DefaultIfEmpty) give the same result? {left.SequenceEqual(legacy)}");

        Console.WriteLine("RightJoin: ALL elements of the right side + the matching left (symmetric):");
        var right = d.Orders.RightJoin(d.Customers, o => o.CustomerId, c => c.Id, (o, c) => (c.Name, OrderId: o?.Id));
        Console.WriteLine($"   RightJoin row count = {right.Count()} (12 order matches + 1 customer with no orders)");

        Console.WriteLine("""

        Note: These operators depend on the target framework — they need the .NET 9/10 SDK + the net9.0/net10.0 TFM (they don't compile on older TFMs; with multi-targeting use #if NET10_0_OR_GREATER).
        In EF Core the SQL translation of LeftJoin/RightJoin depends on the version (→ J9).
        """);
    }
}
