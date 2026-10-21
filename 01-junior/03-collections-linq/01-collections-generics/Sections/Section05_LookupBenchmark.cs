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
        var sortedArray = data;                                   // zaten sıralı

        // Aranacak değerler: yarısı VAR, yarısı YOK (en kötü durum: tam tarama)
        var rnd = new Random(42);
        var keys = Enumerable.Range(0, lookups).Select(i => i % 2 == 0 ? rnd.Next(size) : size + rnd.Next(size)).ToArray();

        // ısınma (JIT)
        _ = list.Contains(keys[0]); _ = set.Contains(keys[0]); _ = dict.ContainsKey(keys[0]); _ = Array.BinarySearch(sortedArray, keys[0]);

        Console.WriteLine($"{size:N0} elemanlı koleksiyonda {lookups:N0} arama (yarısı var, yarısı yok):\n");
        Report("List<int>.Contains            O(n)    ", () => { var c = 0; foreach (var k in keys) if (list.Contains(k)) c++; return c; });
        Report("Array.BinarySearch (sıralı)   O(log n)", () => { var c = 0; foreach (var k in keys) if (Array.BinarySearch(sortedArray, k) >= 0) c++; return c; });
        Report("HashSet<int>.Contains         O(1)    ", () => { var c = 0; foreach (var k in keys) if (set.Contains(k)) c++; return c; });
        Report("Dictionary<int,int>.ContainsKey O(1)  ", () => { var c = 0; foreach (var k in keys) if (dict.ContainsKey(k)) c++; return c; });
        Report("Dictionary<int,int>.TryGetValue O(1)  ", () => { var c = 0; foreach (var k in keys) if (dict.TryGetValue(k, out _)) c++; return c; });

        Console.WriteLine("""

        Yorum: aynı doğru sonuç, ama List.Contains her aramada listeyi baştan tarar (n büyüdükçe ÇOK yavaşlar); hash tabanlı yapılar n'den bağımsız yaklaşık sabit sürelidir.
        • Sadece "var mı?" → HashSet<T>    • Anahtarla DEĞER bul → Dictionary<K,V>    • Az sayıda eleman (≈ <20) veya tek seferlik arama → List yeterli (hash maliyeti kazanç getirmez)
        • Sıralı veri + aralık sorgusu → Array.BinarySearch / SortedSet
        • Hash yapısını kurma maliyeti vardır (bellek + O(n)); çok sayıda arama yapacaksanız amorti olur.
        • Kaba ölçüm (Stopwatch, tek tur); kesin sonuç için BenchmarkDotNet → S2.1. Big-O teorisi ve hash tablosu iç yapısı → D2.
        """);
    }

    private static void Report(string label, Func<int> body)
    {
        var sw = Stopwatch.StartNew();
        var found = body();
        sw.Stop();
        Console.WriteLine($"   {label}: {sw.Elapsed.TotalMilliseconds,9:F3} ms   (bulunan: {found})");
    }
}
