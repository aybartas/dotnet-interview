namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class Section03_Capacity
{
    public static void Run()
    {
        Console.WriteLine("Array: fixed size. List<T>: keeps an array behind the scenes; when full it allocates a new array ~2x as large and copies (amortized O(1) Add).\n");

        var list = new List<int>();
        var lastCapacity = list.Capacity;
        Console.WriteLine($"   initial Capacity = {lastCapacity}");
        for (var i = 0; i < 1_000; i++)
        {
            list.Add(i);
            if (list.Capacity != lastCapacity)
            {
                Console.WriteLine($"   Capacity {lastCapacity,4} → {list.Capacity,4} while adding Count={list.Count,4}  (new array + copy)");
                lastCapacity = list.Capacity;
            }
        }

        const int n = 1_000_000;
        long Alloc(Action a) { var b = GC.GetAllocatedBytesForCurrentThread(); a(); return GC.GetAllocatedBytesForCurrentThread() - b; }
        var growing = Alloc(() => { var l = new List<int>(); for (var i = 0; i < n; i++) l.Add(i); });
        var presized = Alloc(() => { var l = new List<int>(n); for (var i = 0; i < n; i++) l.Add(i); });
        Console.WriteLine(FormattableString.Invariant($"\n   adding {n:N0} items: without capacity = {growing / 1024.0 / 1024:F1} MB allocated · new List<int>({n:N0}) = {presized / 1024.0 / 1024:F1} MB allocated"));
        Console.WriteLine("   → If you know the size up front, use `new List<T>(capacity)`: no intermediate arrays, no copies, less GC pressure.");

        var trimmed = new List<int>(1000) { 1, 2, 3 };
        trimmed.TrimExcess();
        Console.WriteLine($"\n   TrimExcess(): Capacity 1000 → {trimmed.Capacity} (gives memory back for a small, long-lived list)");
        Console.WriteLine("   Capacity ≠ Count: Count is the number of items, Capacity is the space reserved. Clear() does not shrink Capacity.");

        Console.WriteLine("""

        Array vs List<T>:
          • Fixed, known size, performance-critical (Span/stackalloc, buffer) → Array
          • Variable size, add/remove → List<T>
          • API return: IReadOnlyList<T>/IEnumerable<T> (do not leak the internal representation)
        """);
    }
}
