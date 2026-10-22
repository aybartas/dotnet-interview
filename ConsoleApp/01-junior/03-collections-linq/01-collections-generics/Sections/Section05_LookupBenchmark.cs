using System.Diagnostics;

namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class Section05_LookupBenchmark
{
    public static void Run()
    {
        const int size = 100_000;
        const int lookups = 1_000;
        var data = Enumerable.Range(0, size).ToArray();
        var list = new List<int>(data);
        var set = new HashSet<int>(data);
        var dict = data.ToDictionary(x => x, x => x * 2);
        var sortedArray = data;                                   // already sorted

        // Values to look up: half EXIST, half DON'T (worst case: full scan)
        var rnd = new Random(42);
        var keys = Enumerable.Range(0, lookups).Select(i => i % 2 == 0 ? rnd.Next(size) : size + rnd.Next(size)).ToArray();

        // warm-up (JIT)
        _ = list.Contains(keys[0]); _ = set.Contains(keys[0]); _ = dict.ContainsKey(keys[0]); _ = Array.BinarySearch(sortedArray, keys[0]);

        Console.WriteLine(FormattableString.Invariant($"{lookups:N0} lookups in a collection of {size:N0} items (half present, half absent):\n"));
        Report("List<int>.Contains            O(n)    ", () => { var c = 0; foreach (var k in keys) if (list.Contains(k)) c++; return c; });
        Report("Array.BinarySearch (sorted)   O(log n)", () => { var c = 0; foreach (var k in keys) if (Array.BinarySearch(sortedArray, k) >= 0) c++; return c; });
        Report("HashSet<int>.Contains         O(1)    ", () => { var c = 0; foreach (var k in keys) if (set.Contains(k)) c++; return c; });
        Report("Dictionary<int,int>.ContainsKey O(1)  ", () => { var c = 0; foreach (var k in keys) if (dict.ContainsKey(k)) c++; return c; });
        Report("Dictionary<int,int>.TryGetValue O(1)  ", () => { var c = 0; foreach (var k in keys) if (dict.TryGetValue(k, out _)) c++; return c; });

        Console.WriteLine("""

        Takeaway: same correct result, but List.Contains scans the list from the start on every lookup (gets MUCH slower as n grows); hash-based structures take roughly constant time independent of n.
        • Only "does it exist?" → HashSet<T>    • Find a VALUE by key → Dictionary<K,V>    • Few items (≈ <20) or a one-off lookup → List is enough (hashing cost brings no gain)
        • Sorted data + range query → Array.BinarySearch / SortedSet
        • Building a hash structure has a cost (memory + O(n)); it pays off if you do many lookups.
        • Rough measurement (Stopwatch, single pass); for accurate results use BenchmarkDotNet → S2.1. Big-O theory and the internals of a hash table → D2.
        """);
    }

    private static void Report(string label, Func<int> body)
    {
        var sw = Stopwatch.StartNew();
        var found = body();
        sw.Stop();
        Console.WriteLine(FormattableString.Invariant($"   {label}: {sw.Elapsed.TotalMilliseconds,9:F3} ms   (found: {found})"));
    }
}
