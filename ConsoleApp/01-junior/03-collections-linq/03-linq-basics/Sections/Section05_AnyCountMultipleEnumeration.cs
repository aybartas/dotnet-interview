using Junior.CollectionsLinq.LinqBasics.Data;

namespace Junior.CollectionsLinq.LinqBasics;

public static class Section05_AnyCountMultipleEnumeration
{
    public static void Run()
    {
        Console.WriteLine("A) Any() vs Count() > 0");
        var list = new List<int> { 1, 2, 3 };
        long enumerated = 0;
        IEnumerable<int> Counting(int n) { for (var i = 0; i < n; i++) { enumerated++; yield return i; } }

        enumerated = 0; _ = Counting(1_000_000).Any();
        Console.WriteLine(FormattableString.Invariant($"   generator (yield) of 1,000,000 elements → Any()         stops at the first element: {enumerated:N0} elements produced"));
        enumerated = 0; _ = Counting(1_000_000).Count() > 0;
        Console.WriteLine(FormattableString.Invariant($"   generator (yield) of 1,000,000 elements → Count() > 0   counts ALL of them:         {enumerated:N0} elements produced"));

        Console.WriteLine($"   On List<T>/ICollection, Count() is O(1) (it reads the Count property): TryGetNonEnumeratedCount = {list.TryGetNonEnumeratedCount(out var c)} (c={c})");
        Console.WriteLine("   Rule: for an \"is there any?\" question ALWAYS use Any(); use Count() only when you really need the number. (`Count() > 0` is both slow and hides the intent)");
        Console.WriteLine("   In EF Core: Any() → `EXISTS(...)` (stops at the first match), Count() > 0 → `COUNT(*)` (counts all matches; usually more expensive) (→ J9).");

        Console.WriteLine("\nB) ⭐ MULTIPLE ENUMERATION: iterating a deferred IEnumerable<T> more than once");
        var d = new ShopData();
        var dbHits = 0;
        IEnumerable<Order> LoadFromDatabase()                           // simulates "reading from the database" (runs AGAIN on every iteration)
        {
            dbHits++;
            foreach (var o in d.Orders) yield return o;
        }

        var orders = LoadFromDatabase();                                // deferred!
        if (orders.Any())                                               // 1st enumeration
        {
            var count = orders.Count();                                 // 2nd enumeration
            var last = orders.Last();                                   // 3rd enumeration
            Console.WriteLine($"   Any + Count + Last → the 'database' was read {dbHits} times (result: {count} orders, last #{last.Id})");
        }

        dbHits = 0;
        var materialized = LoadFromDatabase().ToList();                 // materialized once
        if (materialized.Any()) { _ = materialized.Count; _ = materialized.Last(); }
        Console.WriteLine($"   materialized once with ToList() → {dbHits} read. Analyzer: CA1851 ('possible multiple enumerations of IEnumerable'), Rider/ReSharper warning.");

        Console.WriteLine("\n   Another surprise: multiple enumeration can CHANGE the results (random/time-dependent source):");
        var rnd = new Random(1);
        var randoms = Enumerable.Range(0, 3).Select(_ => rnd.Next(100));
        Console.WriteLine($"   the same query twice: [{string.Join(",", randoms)}] and [{string.Join(",", randoms)}]   (different!)");

        Console.WriteLine("""

        Rule: if you will iterate an IEnumerable<T> parameter/variable MORE THAN ONCE, call ToList()/ToArray() first (or make the parameter type IReadOnlyList<T>).
        If a single pass is enough (foreach, one terminal operator), leave it deferred.
        """);
    }
}
