namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class Section03_Capacity
{
    public static void Run()
    {
        Console.WriteLine("Array: sabit boyut. List<T>: arkada bir dizi tutar; dolunca ~2 KATI yeni dizi ayırıp kopyalar (amortize O(1) Add).\n");

        var list = new List<int>();
        var lastCapacity = list.Capacity;
        Console.WriteLine($"   başlangıç Capacity = {lastCapacity}");
        for (var i = 0; i < 1_000; i++)
        {
            list.Add(i);
            if (list.Capacity != lastCapacity)
            {
                Console.WriteLine($"   Count={list.Count,4} eklenirken Capacity {lastCapacity,4} → {list.Capacity,4}  (yeni dizi + kopya)");
                lastCapacity = list.Capacity;
            }
        }

        const int n = 1_000_000;
        long Alloc(Action a) { var b = GC.GetAllocatedBytesForCurrentThread(); a(); return GC.GetAllocatedBytesForCurrentThread() - b; }
        var growing = Alloc(() => { var l = new List<int>(); for (var i = 0; i < n; i++) l.Add(i); });
        var presized = Alloc(() => { var l = new List<int>(n); for (var i = 0; i < n; i++) l.Add(i); });
        Console.WriteLine($"\n   {n:N0} eleman ekleme: kapasitesiz = {growing / 1024.0 / 1024:F1} MB tahsis · new List<int>({n:N0}) = {presized / 1024.0 / 1024:F1} MB tahsis");
        Console.WriteLine("   → Boyutu önceden biliyorsanız `new List<T>(capacity)`: ara diziler üretilmez, kopya yok, GC baskısı düşer.");

        var trimmed = new List<int>(1000) { 1, 2, 3 };
        trimmed.TrimExcess();
        Console.WriteLine($"\n   TrimExcess(): Capacity 1000 → {trimmed.Capacity} (uzun yaşayacak küçük listede bellek geri verilir)");
        Console.WriteLine("   Capacity ≠ Count: Count eleman sayısı, Capacity ayrılan alan. Clear() Capacity'yi küçültmez.");

        Console.WriteLine("""

        Array vs List<T>:
          • Boyut sabit ve bilinen, performans kritik (Span/stackalloc, buffer) → Array
          • Boyut değişken, ekle/çıkar → List<T>
          • API dönüşü: IReadOnlyList<T>/IEnumerable<T> (iç temsili sızdırma)
        """);
    }
}
