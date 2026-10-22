using System.Collections;

namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class Section04_Enumeration
{
    public static void Run()
    {
        var numbers = new List<int> { 1, 2, 3 };

        Console.WriteLine("foreach is translated by the compiler into this (conceptually):");
        Console.WriteLine("   var e = numbers.GetEnumerator();  while (e.MoveNext()) { var x = e.Current; … }  (finally: e.Dispose())\n");

        Console.WriteLine("Stepping manually:");
        using (var e = numbers.GetEnumerator())
        {
            while (e.MoveNext()) Console.WriteLine($"   MoveNext() → true, Current = {e.Current}");
            Console.WriteLine($"   MoveNext() → false (done)");
        }

        Console.WriteLine("\nIEnumerable<T> is only the 'give me an enumerator' contract; the iteration state lives in the ENUMERATOR:");
        IEnumerable<int> seq = numbers;
        Console.WriteLine($"   two separate foreach loops run independently: {string.Join(",", seq)} / {string.Join(",", seq)}");

        Console.WriteLine("\nList<T>.Enumerator is a STRUCT (no boxing/allocation in foreach); if you hold it as IEnumerable<T> it gets boxed:");
        long Alloc(Action a) { var b = GC.GetAllocatedBytesForCurrentThread(); a(); return GC.GetAllocatedBytesForCurrentThread() - b; }
        _ = Alloc(() => { foreach (var _ in numbers) { } });                                    // warm-up
        var direct = Alloc(() => { foreach (var _ in numbers) { } });
        var viaInterface = Alloc(() => { foreach (var _ in (IEnumerable<int>)numbers) { } });
        Console.WriteLine($"   List<int> direct foreach: {direct} B allocated · via IEnumerable<int>: {viaInterface} B allocated");

        Console.WriteLine("\nMODIFYING the collection while iterating (version check):");
        try { foreach (var x in numbers) if (x == 2) numbers.Remove(x); }
        catch (InvalidOperationException ex) { Console.WriteLine($"   Remove inside foreach → {ex.GetType().Name}: {ex.Message}"); }
        numbers.RemoveAll(x => x == 2);
        Console.WriteLine($"   Fix: RemoveAll(x => …) or iterate over a ToList() copy → [{string.Join(",", numbers)}]");

        Console.WriteLine("\nMaking your own collection usable in foreach: GetEnumerator() is enough (duck typing), or IEnumerable<T> + yield return:");
        var countdown = new Countdown(3);
        Console.WriteLine($"   foreach (var i in new Countdown(3)) → {string.Join(",", countdown)}");
        Console.WriteLine("   The deep version (iterator state machine, yield, async stream) → M1.4.");
    }

    private sealed class Countdown(int from) : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            for (var i = from; i > 0; i--) yield return i;       // the compiler generates a state machine
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
