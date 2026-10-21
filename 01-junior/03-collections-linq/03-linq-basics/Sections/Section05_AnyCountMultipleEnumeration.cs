using Junior.CollectionsLinq.LinqBasics.Data;

namespace Junior.CollectionsLinq.LinqBasics;

public static class Section05_AnyCountMultipleEnumeration
{
    public static void Run()
    {
        Console.WriteLine("A) Any() vs Count() > 0");
        var list = new List<int> { 1, 2, 3 };
        long enumerated = 0;
        IEnumerable<int> Counting(int n) { for (var i = 0; i < n; i++) { enumerated++; yield return i; } }

        enumerated = 0; _ = Counting(1_000_000).Any();
        Console.WriteLine($"   üretici (yield) 1.000.000 eleman → Any()         ilk elemanda durur: {enumerated:N0} eleman üretildi");
        enumerated = 0; _ = Counting(1_000_000).Count() > 0;
        Console.WriteLine($"   üretici (yield) 1.000.000 eleman → Count() > 0   TÜMÜNÜ sayar:       {enumerated:N0} eleman üretildi");

        Console.WriteLine($"   List<T>/ICollection üzerinde Count() O(1)'dir (Count özelliğine bakar): TryGetNonEnumeratedCount = {list.TryGetNonEnumeratedCount(out var c)} (c={c})");
        Console.WriteLine("   Kural: \"var mı?\" sorusu için HER ZAMAN Any(); gerçekten sayı gerekiyorsa Count(). (`Count() > 0` hem yavaş hem niyeti gizler)");
        Console.WriteLine("   EF Core'da: Any() → `EXISTS(...)` (ilk eşleşmede durur), Count() > 0 → `COUNT(*)` (tüm eşleşmeleri sayar; genelde daha pahalı) (→ J9).");

        Console.WriteLine("\nB) ⭐ ÇOKLU ENUMERATION: ertelenmiş bir IEnumerable<T>'yi birden çok kez gezmek");
        var d = new ShopData();
        var dbHits = 0;
        IEnumerable<Order> LoadFromDatabase()                           // "veritabanından okuma" simülasyonu (her gezinmede YENİDEN çalışır)
        {
            dbHits++;
            foreach (var o in d.Orders) yield return o;
        }

        var orders = LoadFromDatabase();                                // ertelenmiş!
        if (orders.Any())                                               // 1. enumeration
        {
            var count = orders.Count();                                 // 2. enumeration
            var last = orders.Last();                                   // 3. enumeration
            Console.WriteLine($"   Any + Count + Last → 'veritabanı' {dbHits} kez okundu (sonuç: {count} sipariş, son #{last.Id})");
        }

        dbHits = 0;
        var materialized = LoadFromDatabase().ToList();                 // bir kez materialize
        if (materialized.Any()) { _ = materialized.Count; _ = materialized.Last(); }
        Console.WriteLine($"   ToList() ile bir kez materialize → {dbHits} okuma. Analyzer: CA1851 ('IEnumerable'ın olası çoklu enumeration'ı'), Rider/ReSharper uyarısı.");

        Console.WriteLine("\n   Başka bir sürpriz: çoklu enumeration sonuçları DEĞİŞTİREBİLİR (rastgele/zamana bağlı kaynak):");
        var rnd = new Random(1);
        var randoms = Enumerable.Range(0, 3).Select(_ => rnd.Next(100));
        Console.WriteLine($"   aynı sorgu iki kez: [{string.Join(",", randoms)}] ve [{string.Join(",", randoms)}]   (farklı!)");

        Console.WriteLine("""

        Kural: IEnumerable<T> parametresini/değişkenini BİRDEN ÇOK kez gezecekseniz önce ToList()/ToArray() yapın (ya da parametre tipini IReadOnlyList<T> yapın).
        Tek geçişte yetiyorsa (foreach, tek terminal operatör) ertelenmiş bırakın.
        """);
    }
}
