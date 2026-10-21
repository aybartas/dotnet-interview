using System.Collections;
using System.Diagnostics;

namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section05_Boxing
{
    public static void Run()
    {
        Console.WriteLine("BOXING: value type'ı heap'e, bir object (veya interface) kutusunun içine koymak. UNBOXING: kutudan geri çıkarmak.\n");

        int i = 42;
        object boxed = i;                 // BOXING: heap'te yeni nesne tahsisi + değer kopyası
        int back = (int)boxed;            // UNBOXING: tip kontrolü + kopya
        Console.WriteLine($"1) int → object → int: {back}");

        // Kutu BAĞIMSIZDIR: orijinali değiştirmek kutuyu etkilemez
        i = 99;
        Console.WriteLine($"   i = 99 yapıldı → kutunun içi hâlâ {boxed}");

        try { _ = (long)boxed; }          // unboxing TAM tipi ister: kutu 'int' içeriyor, 'long' isteniyor
        catch (InvalidCastException) { Console.WriteLine("   (long)boxed → InvalidCastException (unboxing yalnızca tam tipe yapılır; (long)(int)boxed yazmalıydı)"); }

        Console.WriteLine("\n2) Boxing'in GİZLİCE olduğu yerler:");
        long Alloc(Action a) { var b = GC.GetAllocatedBytesForCurrentThread(); a(); return GC.GetAllocatedBytesForCurrentThread() - b; }
        const int n = 1000;

        Console.WriteLine($"   object parametre            : {Alloc(() => { for (var k = 0; k < n; k++) TakeObject(k); }),8:N0} B   (TakeObject(object) ← int)");
        Console.WriteLine($"   interface'e atama           : {Alloc(() => { for (var k = 0; k < n; k++) { IComparable c = k; _ = c; } }),8:N0} B   (IComparable c = int)");
        Console.WriteLine($"   non-generic ArrayList.Add   : {Alloc(() => { var l = new ArrayList(); for (var k = 0; k < n; k++) l.Add(k); }),8:N0} B   (+ iç dizi büyümeleri)");
        Console.WriteLine($"   generic List<int>.Add       : {Alloc(() => { var l = new List<int>(n); for (var k = 0; k < n; k++) l.Add(k); }),8:N0} B   (sadece iç dizi, kutu YOK)");
        Console.WriteLine($"   string.Format({{0}}, int)     : {Alloc(() => { for (var k = 0; k < n; k++) _ = string.Format("{0}", (object)k); }),8:N0} B   (params object[] → kutu + dizi + string)");
        Console.WriteLine($"   struct, interface üzerinden  : {Alloc(() => { for (var k = 0; k < n; k++) { IShape s = new Square(k); _ = s.Area; } }),8:N0} B   (struct → interface = boxing)");
        Console.WriteLine($"   generic kısıt (where T:IShape): {Alloc(() => { for (var k = 0; k < n; k++) _ = AreaOf(new Square(k)); }),8:N0} B   (generic + constraint → boxing YOK)");

        Console.WriteLine("\n3) Maliyet: ArrayList (boxing) vs List<int> — 5 milyon eleman ekle, topla");
        Compare();

        Console.WriteLine("\n4) Kutulanmış struct'ı interface üzerinden DEĞİŞTİRMEK kopya üzerinde çalışır:");
        var counter = new Counter();
        IIncrementable inc = counter;     // BOXING: counter'ın kopyası kutulandı
        inc.Increment(); inc.Increment();
        Console.WriteLine($"   interface üzerinden 2 kez Increment → kutu: {((Counter)inc).Value}, orijinal counter: {counter.Value}");

        Console.WriteLine("\nKaçınma: generic koleksiyonlar (List<T>), generic metotlar, string interpolation (modern derleyici handler kullanır), 'object' parametre yerine generic.");
    }

    private static void Compare()
    {
        const int n = 5_000_000;
        GC.Collect(); GC.WaitForPendingFinalizers(); GC.Collect();

        var gc0 = GC.CollectionCount(0);
        var sw = Stopwatch.StartNew();
        var arrayList = new ArrayList();
        for (var i = 0; i < n; i++) arrayList.Add(i);              // her Add: boxing
        long sum1 = 0;
        foreach (var o in arrayList) sum1 += (int)o!;              // her okuma: unboxing
        sw.Stop();
        Console.WriteLine($"   ArrayList : {sw.ElapsedMilliseconds,5} ms · toplam {sum1:N0} · Gen0 GC: {GC.CollectionCount(0) - gc0}");

        gc0 = GC.CollectionCount(0);
        sw.Restart();
        var list = new List<int>();
        for (var i = 0; i < n; i++) list.Add(i);
        long sum2 = 0;
        foreach (var v in list) sum2 += v;
        sw.Stop();
        Console.WriteLine($"   List<int> : {sw.ElapsedMilliseconds,5} ms · toplam {sum2:N0} · Gen0 GC: {GC.CollectionCount(0) - gc0}");
        Console.WriteLine("   (Kaba ölçüm: Stopwatch, tek çalıştırma, ısınma yok. Kesin sonuç için BenchmarkDotNet → S2.1)");
    }

    private static void TakeObject(object o) { }
    private static double AreaOf<T>(T shape) where T : IShape => shape.Area;

    private interface IShape { double Area { get; } }
    private readonly struct Square(double side) : IShape { public double Area => side * side; }

    private interface IIncrementable { void Increment(); }
    private struct Counter : IIncrementable { public int Value; public void Increment() => Value++; }
}
