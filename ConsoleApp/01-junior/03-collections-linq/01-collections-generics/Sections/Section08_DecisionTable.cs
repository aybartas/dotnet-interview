namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class Section08_DecisionTable
{
    public static void Run() => Console.WriteLine("""
        WHICH COLLECTION WHEN?  (Big-O average; theory and proofs → D2, D3)

        ┌───────────────────────────┬──────────────────────┬────────────┬────────────┬───────────────────────────────────────────────┐
        │ Need                      │ Choice               │ Lookup     │ Insert     │ Note                                          │
        ├───────────────────────────┼──────────────────────┼────────────┼────────────┼───────────────────────────────────────────────┤
        │ Ordered list, by index    │ List<T>              │ O(n)       │ O(1)* end  │ DEFAULT choice. Set capacity if size is known │
        │ Fixed size, raw speed     │ T[]  (Array)         │ O(n) / [i] │ —          │ Works with Span<T>; size does not change      │
        │ "Exists?" / no duplicates │ HashSet<T>           │ O(1)       │ O(1)       │ NO ordering guarantee; set operations         │
        │ Key → value               │ Dictionary<K,V>      │ O(1)       │ O(1)       │ Key contract (Equals/GetHashCode)             │
        │ SORTED iteration by key   │ SortedDictionary<K,V>│ O(log n)   │ O(log n)   │ Many inserts/removes; SortedList: fast lookup │
        │ Sorted + range query      │ SortedSet<T>         │ O(log n)   │ O(log n)   │ GetViewBetween, Min/Max                       │
        │ First in, first out       │ Queue<T>             │ —          │ O(1)       │ Job queue, BFS                                │
        │ Last in, first out        │ Stack<T>             │ —          │ O(1)       │ Undo, DFS, expression parsing                 │
        │ Priority processing       │ PriorityQueue<E,P>   │ —          │ O(log n)   │ Min-heap; scheduler, Dijkstra                 │
        │ Frequent mid-list inserts │ LinkedList<T>        │ O(n)       │ O(1)**     │ ** if node is known; List often wins anyway   │
        │ Multi-threaded access     │ Concurrent*          │ …          │ …          │ → M4                                          │
        │ Immutable                 │ Immutable* / Frozen* │ …          │ —          │ → M1; FrozenDictionary is read-heavy          │
        └───────────────────────────┴──────────────────────┴────────────┴────────────┴───────────────────────────────────────────────┘

        QUICK RULES
          • When in doubt, List<T>. Looking up by key? Dictionary. Asking "does it exist?" HashSet.
          • Public API: parameter IEnumerable<T> (or the narrowest interface), return IReadOnlyList<T>/IReadOnlyDictionary<K,V>.
          • Many items and many lookups → hash-based; a few items → List is enough.
          • MEASURE performance claims (Section 5): because of cache locality, List is usually faster than LinkedList for small n, and sometimes faster than Dictionary.
        """);
}
