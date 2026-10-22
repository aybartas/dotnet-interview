using System.Collections.Immutable;

namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section06_CollectionExpressions
{
    public static void Run()
    {
        // OLD syntaxes (all different)
        int[] oldArray = new int[] { 1, 2, 3 };
        var oldList = new List<int> { 1, 2, 3 };
        var oldImmutable = ImmutableArray.Create(1, 2, 3);
        Console.WriteLine($"old: array={oldArray.Length}, list={oldList.Count}, immutable={oldImmutable.Length}   (three types, three different syntaxes)");

        // NEW: a single syntax — the target type decides
        int[] array = [1, 2, 3];
        List<int> list = [1, 2, 3];
        ImmutableArray<int> immutable = [1, 2, 3];
        HashSet<int> set = [1, 2, 2, 3];
        Span<int> span = [1, 2, 3];
        IEnumerable<int> sequence = [1, 2, 3];
        IReadOnlyList<int> readOnly = [1, 2, 3];
        Console.WriteLine($"new: array={array.Length}, list={list.Count}, immutable={immutable.Length}, set={set.Count} (duplicate dropped), span={span.Length}, IEnumerable={sequence.Count()}, IReadOnlyList={readOnly.Count}");

        // Empty collection
        List<string> empty = [];
        Console.WriteLine($"empty: [] → {empty.Count} elements (List<string>); for an Array, `[]` → a shared empty array similar to Array.Empty");

        // Spread (..): spread the elements of a collection INTO another — instead of Concat/AddRange for merging
        int[] first = [1, 2], second = [4, 5];
        int[] merged = [.. first, 3, .. second];
        Console.WriteLine($"spread: [.. [1,2], 3, .. [4,5]] → [{string.Join(",", merged)}]");

        // Realistic use: conditionally adding an element
        var isAdmin = true;
        string[] baseRoles = ["user"];
        string[] roles = [.. baseRoles, .. isAdmin ? new[] { "admin" } : []];
        Console.WriteLine($"conditional: roles = [{string.Join(",", roles)}]");

        // As a method argument
        Console.WriteLine($"method argument: Sum([1,2,3]) = {Sum([1, 2, 3])}   (target type IEnumerable<int>)");

        Console.WriteLine("""

        Target-typed: `[...]` is NOT a type on its own; it is built according to the type it is assigned/passed to (array, List<T>, Span<T>, ImmutableArray<T>, IEnumerable<T>…).
        `var x = [1,2,3];` does NOT COMPILE (CS9176: no target type) — write an explicit type.
        The compiler generates the most efficient code for the target type (stack for Span, directly sized allocation for arrays, with capacity for List<T>) — better than an AddRange loop.
        Collection expressions can't be used for key-value collections like Dictionary; use an index initializer: new() { ["a"] = 1 }.
        """);
    }

    private static int Sum(IEnumerable<int> items) => items.Sum();
}
