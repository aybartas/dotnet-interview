using System.Collections;
using System.Diagnostics;

namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section05_Boxing
{
    public static void Run()
    {
        Console.WriteLine("BOXING: putting a value type on the heap, inside an object (or interface) box. UNBOXING: taking it back out of the box.\n");

        int i = 42;
        object boxed = i;                 // BOXING: allocates a new object on the heap + copies the value
        int back = (int)boxed;            // UNBOXING: type check + copy
        Console.WriteLine($"1) int → object → int: {back}");

        // The box is INDEPENDENT: changing the original doesn't affect the box
        i = 99;
        Console.WriteLine($"   i = 99 was set → the box still contains {boxed}");

        try { _ = (long)boxed; }          // unboxing requires the EXACT type: the box holds an 'int', 'long' was requested
        catch (InvalidCastException) { Console.WriteLine("   (long)boxed → InvalidCastException (unboxing works only to the exact type; you should have written (long)(int)boxed)"); }

        Console.WriteLine("\n2) Where boxing happens SILENTLY:");
        long Alloc(Action a) { var b = GC.GetAllocatedBytesForCurrentThread(); a(); return GC.GetAllocatedBytesForCurrentThread() - b; }
        const int n = 1000;

        Console.WriteLine($"   object parameter            : {Alloc(() => { for (var k = 0; k < n; k++) TakeObject(k); }),8:N0} B   (TakeObject(object) ← int)");
        Console.WriteLine($"   assignment to an interface  : {Alloc(() => { for (var k = 0; k < n; k++) { IComparable c = k; _ = c; } }),8:N0} B   (IComparable c = int)");
        Console.WriteLine($"   non-generic ArrayList.Add   : {Alloc(() => { var l = new ArrayList(); for (var k = 0; k < n; k++) l.Add(k); }),8:N0} B   (+ internal array growth)");
        Console.WriteLine($"   generic List<int>.Add       : {Alloc(() => { var l = new List<int>(n); for (var k = 0; k < n; k++) l.Add(k); }),8:N0} B   (only the internal array, NO boxes)");
        Console.WriteLine($"   string.Format({{0}}, int)     : {Alloc(() => { for (var k = 0; k < n; k++) _ = string.Format("{0}", (object)k); }),8:N0} B   (params object[] → box + array + string)");
        Console.WriteLine($"   struct, via an interface    : {Alloc(() => { for (var k = 0; k < n; k++) { IShape s = new Square(k); _ = s.Area; } }),8:N0} B   (struct → interface = boxing)");
        Console.WriteLine($"   generic constraint (where T:IShape): {Alloc(() => { for (var k = 0; k < n; k++) _ = AreaOf(new Square(k)); }),8:N0} B   (generic + constraint → NO boxing)");

        Console.WriteLine("\n3) Cost: ArrayList (boxing) vs List<int> — add 5 million elements, sum them");
        Compare();

        Console.WriteLine("\n4) MODIFYING a boxed struct through an interface works on a copy:");
        var counter = new Counter();
        IIncrementable inc = counter;     // BOXING: a copy of counter was boxed
        inc.Increment(); inc.Increment();
        Console.WriteLine($"   Increment twice via the interface → box: {((Counter)inc).Value}, original counter: {counter.Value}");

        Console.WriteLine("\nAvoidance: generic collections (List<T>), generic methods, string interpolation (the modern compiler uses a handler), generics instead of an 'object' parameter.");
    }

    private static void Compare()
    {
        const int n = 5_000_000;
        GC.Collect(); GC.WaitForPendingFinalizers(); GC.Collect();

        var gc0 = GC.CollectionCount(0);
        var sw = Stopwatch.StartNew();
        var arrayList = new ArrayList();
        for (var i = 0; i < n; i++) arrayList.Add(i);              // every Add: boxing
        long sum1 = 0;
        foreach (var o in arrayList) sum1 += (int)o!;              // every read: unboxing
        sw.Stop();
        Console.WriteLine($"   ArrayList : {sw.ElapsedMilliseconds,5} ms · sum {sum1:N0} · Gen0 GC: {GC.CollectionCount(0) - gc0}");

        gc0 = GC.CollectionCount(0);
        sw.Restart();
        var list = new List<int>();
        for (var i = 0; i < n; i++) list.Add(i);
        long sum2 = 0;
        foreach (var v in list) sum2 += v;
        sw.Stop();
        Console.WriteLine($"   List<int> : {sw.ElapsedMilliseconds,5} ms · sum {sum2:N0} · Gen0 GC: {GC.CollectionCount(0) - gc0}");
        Console.WriteLine("   (Rough measurement: Stopwatch, single run, no warm-up. For precise results use BenchmarkDotNet → S2.1)");
    }

    private static void TakeObject(object o) { }
    private static double AreaOf<T>(T shape) where T : IShape => shape.Area;

    private interface IShape { double Area { get; } }
    private readonly struct Square(double side) : IShape { public double Area => side * side; }

    private interface IIncrementable { void Increment(); }
    private struct Counter : IIncrementable { public int Value; public void Increment() => Value++; }
}
