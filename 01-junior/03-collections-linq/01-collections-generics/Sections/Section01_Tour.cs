namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class Section01_Tour
{
    public static void Run()
    {
        Console.WriteLine("List<T> — dinamik dizi: indeksle O(1) erişim, sona ekleme amortize O(1), ortaya ekleme/silme O(n)");
        var list = new List<int> { 5, 3, 8 };
        list.Add(1); list.Insert(1, 99); list.Remove(3);
        Console.WriteLine($"   {string.Join(",", list)}  ·  list[1] = {list[1]}  ·  IndexOf(8) = {list.IndexOf(8)}  ·  Contains(8) = {list.Contains(8)} (O(n) tarama)");

        Console.WriteLine("\nDictionary<TKey,TValue> — hash tablosu: anahtarla O(1) ortalama arama; sıralama GARANTİSİ YOK");
        var dict = new Dictionary<string, int> { ["elma"] = 3, ["armut"] = 5 };
        dict["muz"] = 7; dict["elma"]++;
        Console.WriteLine($"   elma={dict["elma"]}  ·  TryGetValue(kiraz) = {dict.TryGetValue("kiraz", out var v)} (v={v})  ·  GetValueOrDefault(kiraz, -1) = {dict.GetValueOrDefault("kiraz", -1)}");
        Console.WriteLine($"   TryAdd(elma,1) = {dict.TryAdd("elma", 1)} (zaten var)  ·  dict[\"yok\"] → KeyNotFoundException (indexer): {Throws(() => _ = dict["yok"])}");

        Console.WriteLine("\nHashSet<T> — tekrarsız küme: Add/Contains O(1); küme işlemleri");
        var a = new HashSet<int> { 1, 2, 3, 4 };
        var b = new HashSet<int> { 3, 4, 5 };
        Console.WriteLine($"   Add(2) = {a.Add(2)} (zaten var)  ·  Union = {{{string.Join(",", a.Union(b).OrderBy(x => x))}}}  ·  Intersect = {{{string.Join(",", a.Intersect(b))}}}  ·  Except = {{{string.Join(",", a.Except(b))}}}  ·  IsSubsetOf = {new HashSet<int> { 3, 4 }.IsSubsetOf(a)}");

        Console.WriteLine("\nQueue<T> — FIFO (ilk giren ilk çıkar): iş kuyruğu, BFS");
        var q = new Queue<string>(["a", "b", "c"]);
        Console.WriteLine($"   Dequeue = {q.Dequeue()}  ·  Peek = {q.Peek()}  ·  Count = {q.Count}");

        Console.WriteLine("\nStack<T> — LIFO (son giren ilk çıkar): geri al (undo), ifade ayrıştırma, DFS");
        var s = new Stack<string>(["a", "b", "c"]);
        Console.WriteLine($"   Pop = {s.Pop()}  ·  Peek = {s.Peek()}  ·  Count = {s.Count}");

        Console.WriteLine("\nLinkedList<T> — çift bağlı liste: bilinen düğümün önüne/arkasına ekleme/silme O(1); indeksle erişim YOK, bellek ve cache açısından pahalı");
        var ll = new LinkedList<int>([1, 2, 4]);
        var node = ll.Find(2)!;
        ll.AddAfter(node, 3);
        Console.WriteLine($"   {string.Join("→", ll)}  ·  First = {ll.First!.Value}, Last = {ll.Last!.Value}  (pratikte nadiren doğru seçim; çoğu zaman List<T> daha hızlı)");

        Console.WriteLine("\nSortedDictionary<K,V> — anahtara göre SIRALI (kırmızı-siyah ağaç): arama O(log n); sıralı gezinme ücretsiz");
        var sd = new SortedDictionary<string, int> { ["muz"] = 1, ["armut"] = 2, ["elma"] = 3 };
        Console.WriteLine($"   {string.Join(", ", sd.Select(kv => $"{kv.Key}={kv.Value}"))}");

        Console.WriteLine("\nSortedSet<T> — sıralı tekrarsız küme; GetViewBetween ile aralık sorgusu");
        var ss = new SortedSet<int> { 50, 10, 30, 20, 40 };
        Console.WriteLine($"   {string.Join(",", ss)}  ·  Min = {ss.Min}, Max = {ss.Max}  ·  [20..40] = {string.Join(",", ss.GetViewBetween(20, 40))}");

        Console.WriteLine("\nPriorityQueue<TElement,TPriority> — en KÜÇÜK öncelik değeri önce çıkar (min-heap; .NET 6+): görev zamanlama, Dijkstra");
        var pq = new PriorityQueue<string, int>();
        pq.Enqueue("düşük öncelik", 5); pq.Enqueue("ACİL", 1); pq.Enqueue("normal", 3);
        Console.WriteLine($"   çıkış sırası: {string.Join(" → ", Drain(pq))}   (aynı öncelikte sıra GARANTİ EDİLMEZ — kararlı değil)");

        Console.WriteLine("\nConcurrent koleksiyonlar (ConcurrentDictionary, ConcurrentQueue…) çok thread'li erişim için → M4. Immutable koleksiyonlar → M1.");
    }

    private static string Throws(Action a)
    {
        try { a(); return "fırlatmadı"; }
        catch (Exception ex) { return ex.GetType().Name; }
    }

    private static IEnumerable<string> Drain(PriorityQueue<string, int> pq)
    {
        while (pq.TryDequeue(out var item, out _)) yield return item;
    }
}
