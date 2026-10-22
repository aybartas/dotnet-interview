using Junior.CollectionsLinq.LinqBasics.Data;

namespace Junior.CollectionsLinq.LinqBasics;

public static class Section03_Operators
{
    public static void Run()
    {
        var d = new ShopData();
        string J<T>(IEnumerable<T> xs) => string.Join(", ", xs);

        Console.WriteLine("— Filtering and projection —");
        Console.WriteLine($"   Where(Price > 200)            : {J(d.Products.Where(p => p.Price > 200).Select(p => p.Name))}");
        Console.WriteLine($"   Select(name uppercased)       : {J(d.Products.Take(3).Select(p => p.Name.ToUpperInvariant()))}");
        Console.WriteLine($"   Select((p, index) => …)       : {J(d.Products.Take(3).Select((p, i) => $"{i + 1}.{p.Name}"))}   (indexed overload)");
        Console.WriteLine($"   SelectMany (flattening)       : {J(d.Orders.Take(2).SelectMany(o => d.Items.Where(i => i.OrderId == o.Id), (o, i) => $"#{o.Id}:{i.ProductId}"))}   (order → items → ONE flat list; Select would have given a list of lists)");

        Console.WriteLine("\n— Sorting —");
        Console.WriteLine($"   OrderBy(Category).ThenByDescending(Price): {J(d.Products.OrderBy(p => p.Category, StringComparer.Ordinal).ThenByDescending(p => p.Price).Select(p => $"{p.Category}:{p.Name}"))}");
        Console.WriteLine("   OrderBy is STABLE: elements with equal keys keep their original order. Use ThenBy for a secondary key; a second OrderBy OVERRIDES the first.");

        Console.WriteLine("\n— Grouping —");
        foreach (var g in d.Products.GroupBy(p => p.Category).OrderBy(g => g.Key, StringComparer.Ordinal))
            Console.WriteLine(FormattableString.Invariant($"   GroupBy(Category) → {g.Key,-11}: {J(g.Select(p => p.Name))}  (Count={g.Count()}, Average price={g.Average(p => p.Price):N1})"));
        Console.WriteLine("   GroupBy returns a sequence of IGrouping<TKey,TElement>: a group = key (Key) + elements. Unlike SQL's GROUP BY, you have access to ALL the elements of the group.");

        Console.WriteLine("\n— Joining (Join) —");
        var joined = d.Orders.Join(d.Customers, o => o.CustomerId, c => c.Id, (o, c) => $"#{o.Id}→{c.Name.Split(' ')[0]}").Take(4);
        Console.WriteLine($"   Join (inner)                  : {J(joined)}   (non-matching rows are NOT in the result)");
        var groupJoin = d.Customers.GroupJoin(d.Orders, c => c.Id, o => o.CustomerId, (c, os) => $"{c.Name.Split(' ')[0]}={os.Count()}");
        Console.WriteLine($"   GroupJoin (like a left outer) : {J(groupJoin)}   (a customer with no match shows up with 0)");

        Console.WriteLine("\n— Removing duplicates —");
        Console.WriteLine($"   Select(City).Distinct()       : {J(d.Customers.Select(c => c.City).Distinct())}");
        Console.WriteLine($"   DistinctBy(c => c.City)       : {J(d.Customers.DistinctBy(c => c.City).Select(c => c.Name.Split(' ')[0]))}   (the FIRST customer per city)");

        Console.WriteLine("\n— Aggregate operators —");
        var prices = d.Products.Select(p => p.Price);
        Console.WriteLine(FormattableString.Invariant($"   Count={prices.Count()}  Sum={prices.Sum():N2}  Average={prices.Average():N2}  Min={prices.Min():N2}  Max={prices.Max():N2}"));
        Console.WriteLine($"   MinBy/MaxBy (returns the object): {d.Products.MinBy(p => p.Price)!.Name} / {d.Products.MaxBy(p => p.Price)!.Name}   (Min/Max give the value, MinBy/MaxBy give the element)");
        Console.WriteLine($"   Any(Price > 900)={d.Products.Any(p => p.Price > 900)}  All(Price > 10)={d.Products.All(p => p.Price > 10)}  Contains(40m)={prices.Contains(40m)}");
        Console.WriteLine($"   Aggregate (manual fold)       : {d.Products.Take(4).Select(p => p.Name).Aggregate((acc, n) => acc + " | " + n)}");

        Console.WriteLine("\n— Partitioning and conversion —");
        Console.WriteLine($"   Take(3) / Skip(8)             : {J(d.Products.Take(3).Select(p => p.Id))} / {J(d.Products.Skip(8).Select(p => p.Id))}   ·  TakeWhile/SkipWhile/Take(^2..) exist too");
        Console.WriteLine($"   Chunk(4)                      : {J(d.Products.Select(p => p.Id).Chunk(4).Select(c => $"[{J(c)}]"))}");
        Console.WriteLine(FormattableString.Invariant($"   Zip (pair up two sequences)   : {J(d.Products.Take(3).Select(p => p.Name).Zip(d.Products.Take(3).Select(p => p.Price), (n, p) => $"{n}={p}"))}"));
        var dict = d.Products.ToDictionary(p => p.Id, p => p.Name);
        Console.WriteLine($"   ToDictionary(Id → Name)       : dict[3] = {dict[3]}   (the key must be UNIQUE; a duplicate → ArgumentException)");
        var lookup = d.Products.ToLookup(p => p.Category, p => p.Name);
        Console.WriteLine($"   ToLookup(Category → Name)     : lookup[\"Home\"] = [{J(lookup["Home"])}]; lookup[\"Missing\"] is empty = {!lookup["Missing"].Any()}   (unlike Dictionary: duplicate keys are allowed, a missing key does NOT throw, it returns an empty sequence)");

        Console.WriteLine("\n— Sets —");
        int[] a = [1, 2, 3, 4], b = [3, 4, 5];
        Console.WriteLine($"   Union={J(a.Union(b))} · Intersect={J(a.Intersect(b))} · Except={J(a.Except(b))} · Concat={J(a.Concat(b))} (Concat keeps duplicates, Union drops them)");
    }
}
