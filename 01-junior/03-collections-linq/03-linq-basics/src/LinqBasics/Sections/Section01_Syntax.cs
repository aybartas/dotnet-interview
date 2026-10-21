using Junior.CollectionsLinq.LinqBasics.Data;

namespace Junior.CollectionsLinq.LinqBasics;

public static class Section01_Syntax
{
    public static void Run()
    {
        var d = new ShopData();

        Console.WriteLine("AYNI soru, iki sözdizimi — \"Elektronik ürünler, fiyata göre azalan, ad ve fiyat\":\n");

        // QUERY syntax (SQL'e benzer): from … where … orderby … select
        var query =
            from p in d.Products
            where p.Category == "Elektronik"
            orderby p.Price descending
            select new { p.Name, p.Price };

        // METHOD syntax (fluent): extension method zinciri + lambda
        var method = d.Products
            .Where(p => p.Category == "Elektronik")
            .OrderByDescending(p => p.Price)
            .Select(p => new { p.Name, p.Price });

        Console.WriteLine($"   query  : {string.Join(", ", query.Select(x => $"{x.Name}={x.Price:N0}"))}");
        Console.WriteLine($"   method : {string.Join(", ", method.Select(x => $"{x.Name}={x.Price:N0}"))}");
        Console.WriteLine("   → Derleyici query syntax'ı method syntax'a ÇEVİRİR; ikisi AYNI IL'e iner. Fark yalnızca okunabilirliktir.");

        Console.WriteLine("""

        Hangisi ne zaman?
          • Method syntax: her zaman mümkün; TÜM operatörler (Chunk, DistinctBy, MaxBy, Take, Skip, ToList, …) yalnızca method syntax'ta vardır. Pratikte baskın.
          • Query syntax: `join`, `let` ve birden çok `from` (SelectMany) içeren karmaşık sorgularda okunaklı olabilir. Karma kullanım yaygın:
              (from x in … select …).ToList()
          • Query syntax'ta `let` ara değişken tanımlar:  from o in orders let total = o.Items.Sum(i => i.Price) where total > 100 select …
        Her ikisi de aynı ertelenmiş yürütme (deferred execution) kurallarına tabidir (sıradaki bölüm).
        """);

        var withLet =
            from o in d.Orders
            let total = d.Items.Where(i => i.OrderId == o.Id).Sum(i => i.Total)
            where total > 800
            select (o.Id, total);
        Console.WriteLine($"   `let` örneği — tutarı 800 TL'den büyük siparişler: {string.Join(", ", withLet.Select(x => $"#{x.Id}={x.total:N0}"))}");
    }
}
