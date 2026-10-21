using System.Collections;

namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class Section04_Enumeration
{
    public static void Run()
    {
        var numbers = new List<int> { 1, 2, 3 };

        Console.WriteLine("foreach, derleyici tarafından şuna çevrilir (kavramsal):");
        Console.WriteLine("   var e = numbers.GetEnumerator();  while (e.MoveNext()) { var x = e.Current; … }  (finally: e.Dispose())\n");

        Console.WriteLine("Elle yürütme:");
        using (var e = numbers.GetEnumerator())
        {
            while (e.MoveNext()) Console.WriteLine($"   MoveNext() → true, Current = {e.Current}");
            Console.WriteLine($"   MoveNext() → false (bitti)");
        }

        Console.WriteLine("\nIEnumerable<T> yalnızca 'bana bir enumerator ver' sözleşmesidir; gezinme durumu ENUMERATOR'dadır:");
        IEnumerable<int> seq = numbers;
        Console.WriteLine($"   iki ayrı foreach bağımsız çalışır: {string.Join(",", seq)} / {string.Join(",", seq)}");

        Console.WriteLine("\nList<T>.Enumerator bir STRUCT'tır (foreach'te boxing/allocation yok); IEnumerable<T> olarak tutarsanız kutulanır:");
        long Alloc(Action a) { var b = GC.GetAllocatedBytesForCurrentThread(); a(); return GC.GetAllocatedBytesForCurrentThread() - b; }
        _ = Alloc(() => { foreach (var _ in numbers) { } });                                    // ısınma
        var direct = Alloc(() => { foreach (var _ in numbers) { } });
        var viaInterface = Alloc(() => { foreach (var _ in (IEnumerable<int>)numbers) { } });
        Console.WriteLine($"   List<int> doğrudan foreach: {direct} B tahsis · IEnumerable<int> üzerinden: {viaInterface} B tahsis");

        Console.WriteLine("\nGezinirken koleksiyonu DEĞİŞTİRMEK (version kontrolü):");
        try { foreach (var x in numbers) if (x == 2) numbers.Remove(x); }
        catch (InvalidOperationException ex) { Console.WriteLine($"   foreach içinde Remove → {ex.GetType().Name}: {ex.Message}"); }
        numbers.RemoveAll(x => x == 2);
        Console.WriteLine($"   Çözüm: RemoveAll(x => …) veya ToList() kopyası üzerinde gezin → [{string.Join(",", numbers)}]");

        Console.WriteLine("\nKendi koleksiyonunuzu foreach'e açmak: GetEnumerator() yeterli (duck typing) veya IEnumerable<T> + yield return:");
        var countdown = new Countdown(3);
        Console.WriteLine($"   foreach (var i in new Countdown(3)) → {string.Join(",", countdown)}");
        Console.WriteLine("   Derin hali (iterator state machine, yield, async stream) → M1.4.");
    }

    private sealed class Countdown(int from) : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            for (var i = from; i > 0; i--) yield return i;       // derleyici bir state machine üretir
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
