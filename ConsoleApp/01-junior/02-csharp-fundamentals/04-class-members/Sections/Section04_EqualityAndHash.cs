using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section04_EqualityAndHash
{
    public static void Run()
    {
        Console.WriteLine("A) Default: class → REFERENCE equality");
        var a1 = new Plain(); var a2 = new Plain();
        Console.WriteLine($"   new Plain() == new Plain()      → {a1 == a2}   Equals → {a1.Equals(a2)}   ReferenceEquals → {ReferenceEquals(a1, a2)}");

        Console.WriteLine("\nB) WRONG: only Equals was overridden (GetHashCode was not)");
        var b1 = new BadPoint(1, 2); var b2 = new BadPoint(1, 2);
        Console.WriteLine($"   b1.Equals(b2) = {b1.Equals(b2)}   but the hashes: {b1.GetHashCode()} vs {b2.GetHashCode()} (different!)");
        var badSet = new HashSet<BadPoint> { b1 };
        Console.WriteLine($"   Is an equal object in HashSet<BadPoint>? Contains(b2) = {badSet.Contains(b2)}   ← false! (the hash looked in another bucket)");
        var badDict = new Dictionary<BadPoint, string> { [b1] = "value" };
        Console.WriteLine($"   Dictionary lookup: {(badDict.ContainsKey(b2) ? "found" : "NOT FOUND")}; the same logical key can be added twice: {badDict.TryAdd(b2, "second")}");

        Console.WriteLine("\nC) RIGHT: Equals + GetHashCode + IEquatable<T> + ==/!=");
        var g1 = new GoodPoint(1, 2); var g2 = new GoodPoint(1, 2);
        var goodSet = new HashSet<GoodPoint> { g1 };
        Console.WriteLine($"   g1 == g2 → {g1 == g2};  hashes equal → {g1.GetHashCode() == g2.GetHashCode()};  Contains(g2) → {goodSet.Contains(g2)};  Add(g2) → {goodSet.Add(g2)} (already there)");

        Console.WriteLine("\nD) MUTABLE KEY trap: if the hash of an object sitting in a collection CHANGES");
        var key = new MutableKey("a");
        var set = new HashSet<MutableKey> { key };
        key.Id = "b";                                                   // the hash changed, but the object STAYS in the OLD bucket
        Console.WriteLine($"   after key.Id = \"b\" → Contains(key) = {set.Contains(key)} (false!), Count = {set.Count}, found by enumeration: {set.Any(k => ReferenceEquals(k, key))}");
        Console.WriteLine("   The object is IN the set but CAN'T BE LOOKED UP by hash → can't be removed, can be added again → 'ghost' entry, memory leak.");
        Console.WriteLine("   Rule: the fields that feed the hash/equality must be IMMUTABLE (immutable key: record, readonly struct, string, int).");
        var safe = new HashSet<SafeKey> { new("a") };
        Console.WriteLine($"   SafeKey (record, immutable): Contains(new SafeKey(\"a\")) = {safe.Contains(new SafeKey("a"))}");

        Console.WriteLine("\nE) IComparable<T> (natural order) and IComparer<T> (alternative order)");
        var versions = new List<Version2> { new(1, 10), new(1, 2), new(2, 0) };
        versions.Sort();
        Console.WriteLine($"   Sort() (IComparable): {string.Join(", ", versions)}");
        versions.Sort(new DescendingVersionComparer());
        Console.WriteLine($"   Sort(IComparer, descending): {string.Join(", ", versions)}");

        Console.WriteLine("""

        RULES
          • Equals + GetHashCode + (==, !=) + IEquatable<T> TOGETHER: the FIELDS that define equality also feed the hash.
          • a.Equals(b) ⇒ same hash (the reverse isn't required: collisions are allowed, they only hurt performance).
          • IEquatable<T>: typed Equals(T) → avoids boxing for structs, collections prefer it.
          • GetHashCode must be stable: it must NOT change during the object's lifetime (or at least while it is in a collection) → don't derive it from mutable fields.
          • record / record struct generate all of these automatically (→ J2.6) — the easiest way for value objects.
          • ToString(): return meaningful text for debugging and logging (the default is the type name).
        """);
    }

    private sealed class Plain;
}
