using System.Diagnostics;
using Junior.CollectionsLinq.CollectionsGenerics.Models;

namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class Section06_KeyContract
{
    public static void Run()
    {
        Console.WriteLine("In each experiment, key 1 → \"value\" is added to a Dictionary, then looked up with the SAME logical key (a new object):\n");

        Console.WriteLine($"   CORRECT     (Equals + GetHashCode consistent)       : found? {Lookup(() => new GoodKey(1), () => new GoodKey(1))}");
        Console.WriteLine($"   WRONG #1    (only Equals overridden)                : found? {Lookup(() => new EqualsOnlyKey(1), () => new EqualsOnlyKey(1))}   ← the hashes pointed to different buckets");
        Console.WriteLine($"   WRONG #2    (only GetHashCode overridden)           : found? {Lookup(() => new HashOnlyKey(1), () => new HashOnlyKey(1))}   ← same bucket but Equals compared references");

        Console.WriteLine("\nWRONG #3 — Constant hash (works but O(n)):");
        const int n = 5_000;
        var good = Enumerable.Range(0, n).ToDictionary(i => new GoodKey(i), i => i);
        var bad = Enumerable.Range(0, n).ToDictionary(i => new ConstantHashKey(i), i => i);
        var keysGood = Enumerable.Range(0, n).Select(i => new GoodKey(i)).ToArray();
        var keysBad = Enumerable.Range(0, n).Select(i => new ConstantHashKey(i)).ToArray();
        var sw = Stopwatch.StartNew(); foreach (var k in keysGood) _ = good[k]; var tGood = sw.Elapsed.TotalMilliseconds;
        sw.Restart(); foreach (var k in keysBad) _ = bad[k]; var tBad = sw.Elapsed.TotalMilliseconds;
        Console.WriteLine(FormattableString.Invariant($"   {n:N0} lookups: consistent hash = {tGood:F2} ms · constant hash = {tBad:F2} ms  (≈ {tBad / Math.Max(tGood, 0.001):N0}x slower — all keys in one bucket, the chain is scanned)"));

        Console.WriteLine("\nWRONG #4 — Mutable key (the hash changes while it is in the collection):");
        var key = new MutableKey("a");
        var map = new Dictionary<MutableKey, int> { [key] = 1 };
        key.Name = "b";
        Console.WriteLine($"   key.Name was changed → ContainsKey(key) = {map.ContainsKey(key)} (false!), Count = {map.Count}, Remove(key) = {map.Remove(key)} (cannot be removed → ghost entry)");

        Console.WriteLine("\nCustom comparison: IEqualityComparer<T> without changing the type:");
        var ci = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { ["Apple"] = 1 };
        Console.WriteLine($"   OrdinalIgnoreCase: ci[\"APPLE\"] = {ci["APPLE"]}   ·   custom IEqualityComparer: {new Dictionary<string, int>(new CaseInsensitiveComparer()) { ["Apple"] = 2 }["aPPle"]}");
        Console.WriteLine("   Note: the default comparison for string keys is ORDINAL (case-sensitive); do not pick a culture-aware comparer for keys (→ J2.2).");

        Console.WriteLine("""

        CONTRACT ⭐
          1) a.Equals(b) ⇒ a.GetHashCode() == b.GetHashCode()           (violation → lookup fails)
          2) Hash and Equals must rely on the SAME (and IMMUTABLE) fields  (mutable key → lost entry)
          3) A well-distributed hash                                       (constant/weak hash → O(n))
        In practice: use string, int, Guid, enum, record, readonly record struct as keys. HashCode.Combine(a, b, …) is a good starting point.
        """);
    }

    private static bool Lookup<T>(Func<T> store, Func<T> find) where T : notnull
    {
        var dict = new Dictionary<T, string> { [store()] = "value" };
        return dict.ContainsKey(find());
    }
}
