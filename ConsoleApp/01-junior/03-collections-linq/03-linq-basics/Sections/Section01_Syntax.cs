using Junior.CollectionsLinq.LinqBasics.Data;

namespace Junior.CollectionsLinq.LinqBasics;

public static class Section01_Syntax
{
    public static void Run()
    {
        var d = new ShopData();

        Console.WriteLine("The SAME question, two syntaxes — \"Electronics products, by price descending, name and price\":\n");

        // QUERY syntax (SQL-like): from … where … orderby … select
        var query =
            from p in d.Products
            where p.Category == "Electronics"
            orderby p.Price descending
            select new { p.Name, p.Price };

        // METHOD syntax (fluent): a chain of extension methods + lambdas
        var method = d.Products
            .Where(p => p.Category == "Electronics")
            .OrderByDescending(p => p.Price)
            .Select(p => new { p.Name, p.Price });

        Console.WriteLine(FormattableString.Invariant($"   query  : {string.Join(", ", query.Select(x => $"{x.Name}={x.Price:N0}"))}"));
        Console.WriteLine(FormattableString.Invariant($"   method : {string.Join(", ", method.Select(x => $"{x.Name}={x.Price:N0}"))}"));
        Console.WriteLine("   → The compiler TRANSLATES query syntax into method syntax; both compile to the SAME IL. The only difference is readability.");

        Console.WriteLine("""

        Which one when?
          • Method syntax: always possible; ALL operators (Chunk, DistinctBy, MaxBy, Take, Skip, ToList, …) exist only in method syntax. Dominant in practice.
          • Query syntax: can be more readable for complex queries with `join`, `let` and multiple `from` (SelectMany). Mixed use is common:
              (from x in … select …).ToList()
          • In query syntax `let` defines an intermediate variable:  from o in orders let total = o.Items.Sum(i => i.Price) where total > 100 select …
        Both follow the same deferred execution rules (next section).
        """);

        var withLet =
            from o in d.Orders
            let total = d.Items.Where(i => i.OrderId == o.Id).Sum(i => i.Total)
            where total > 800
            select (o.Id, total);
        Console.WriteLine(FormattableString.Invariant($"   `let` example — orders over $800: {string.Join(", ", withLet.Select(x => $"#{x.Id}={x.total:N0}"))}"));
    }
}
