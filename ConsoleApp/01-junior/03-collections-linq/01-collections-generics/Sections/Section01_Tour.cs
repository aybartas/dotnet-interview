namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class Section01_Tour
{
    public static void Run()
    {
        Console.WriteLine("List<T> — dynamic array: O(1) index access, amortized O(1) append, O(n) insert/remove in the middle");
        var list = new List<int> { 5, 3, 8 };
        list.Add(1); list.Insert(1, 99); list.Remove(3);
        Console.WriteLine($"   {string.Join(",", list)}  ·  list[1] = {list[1]}  ·  IndexOf(8) = {list.IndexOf(8)}  ·  Contains(8) = {list.Contains(8)} (O(n) scan)");

        Console.WriteLine("\nDictionary<TKey,TValue> — hash table: O(1) average lookup by key; NO ordering GUARANTEE");
        var dict = new Dictionary<string, int> { ["apple"] = 3, ["pear"] = 5 };
        dict["banana"] = 7; dict["apple"]++;
        Console.WriteLine($"   apple={dict["apple"]}  ·  TryGetValue(cherry) = {dict.TryGetValue("cherry", out var v)} (v={v})  ·  GetValueOrDefault(cherry, -1) = {dict.GetValueOrDefault("cherry", -1)}");
        Console.WriteLine($"   TryAdd(apple,1) = {dict.TryAdd("apple", 1)} (already exists)  ·  dict[\"missing\"] → KeyNotFoundException (indexer): {Throws(() => _ = dict["missing"])}");

        Console.WriteLine("\nHashSet<T> — set without duplicates: Add/Contains O(1); set operations");
        var a = new HashSet<int> { 1, 2, 3, 4 };
        var b = new HashSet<int> { 3, 4, 5 };
        Console.WriteLine($"   Add(2) = {a.Add(2)} (already exists)  ·  Union = {{{string.Join(",", a.Union(b).OrderBy(x => x))}}}  ·  Intersect = {{{string.Join(",", a.Intersect(b))}}}  ·  Except = {{{string.Join(",", a.Except(b))}}}  ·  IsSubsetOf = {new HashSet<int> { 3, 4 }.IsSubsetOf(a)}");

        Console.WriteLine("\nQueue<T> — FIFO (first in, first out): job queue, BFS");
        var q = new Queue<string>(["a", "b", "c"]);
        Console.WriteLine($"   Dequeue = {q.Dequeue()}  ·  Peek = {q.Peek()}  ·  Count = {q.Count}");

        Console.WriteLine("\nStack<T> — LIFO (last in, first out): undo, expression parsing, DFS");
        var s = new Stack<string>(["a", "b", "c"]);
        Console.WriteLine($"   Pop = {s.Pop()}  ·  Peek = {s.Peek()}  ·  Count = {s.Count}");

        Console.WriteLine("\nLinkedList<T> — doubly linked list: O(1) insert/remove before/after a known node; NO index access, expensive in memory and cache terms");
        var ll = new LinkedList<int>([1, 2, 4]);
        var node = ll.Find(2)!;
        ll.AddAfter(node, 3);
        Console.WriteLine($"   {string.Join("→", ll)}  ·  First = {ll.First!.Value}, Last = {ll.Last!.Value}  (rarely the right choice in practice; List<T> is usually faster)");

        Console.WriteLine("\nSortedDictionary<K,V> — SORTED by key (red-black tree): O(log n) lookup; sorted iteration comes for free");
        var sd = new SortedDictionary<string, int> { ["banana"] = 1, ["pear"] = 2, ["apple"] = 3 };
        Console.WriteLine($"   {string.Join(", ", sd.Select(kv => $"{kv.Key}={kv.Value}"))}");

        Console.WriteLine("\nSortedSet<T> — sorted set without duplicates; range queries with GetViewBetween");
        var ss = new SortedSet<int> { 50, 10, 30, 20, 40 };
        Console.WriteLine($"   {string.Join(",", ss)}  ·  Min = {ss.Min}, Max = {ss.Max}  ·  [20..40] = {string.Join(",", ss.GetViewBetween(20, 40))}");

        Console.WriteLine("\nPriorityQueue<TElement,TPriority> — the SMALLEST priority value comes out first (min-heap; .NET 6+): task scheduling, Dijkstra");
        var pq = new PriorityQueue<string, int>();
        pq.Enqueue("low priority", 5); pq.Enqueue("URGENT", 1); pq.Enqueue("normal", 3);
        Console.WriteLine($"   dequeue order: {string.Join(" → ", Drain(pq))}   (order for equal priorities is NOT guaranteed — not stable)");

        Console.WriteLine("\nConcurrent collections (ConcurrentDictionary, ConcurrentQueue…) are for multi-threaded access → M4. Immutable collections → M1.");
    }

    private static string Throws(Action a)
    {
        try { a(); return "did not throw"; }
        catch (Exception ex) { return ex.GetType().Name; }
    }

    private static IEnumerable<string> Drain(PriorityQueue<string, int> pq)
    {
        while (pq.TryDequeue(out var item, out _)) yield return item;
    }
}
