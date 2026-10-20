namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section02_ValueVsReference
{
    public static void Run()
    {
        // ASIL FARK: KOPYALAMA SEMANTİĞİ. (Stack/heap ayrımı bir uygulama detayıdır; aşağıda gösterilecek.)
        Console.WriteLine("1) Atama = kopyalama. Neyin kopyalandığı değişir:");
        var p1 = new PointStruct { X = 1 };
        var p2 = p1;                       // DEĞER kopyalanır → iki bağımsız nesne
        p2.X = 99;
        Console.WriteLine($"   struct : p1.X = {p1.X}, p2.X = {p2.X}   (p2 değişti, p1 etkilenmedi)");

        var c1 = new PointClass { X = 1 };
        var c2 = c1;                       // REFERANS kopyalanır → ikisi AYNI nesneyi gösterir
        c2.X = 99;
        Console.WriteLine($"   class  : c1.X = {c1.X}, c2.X = {c2.X}   (aynı nesne! ReferenceEquals = {ReferenceEquals(c1, c2)})");

        Console.WriteLine("\n2) Metoda geçirme de AYNI kural (varsayılan: değer ile geçirme = 'pass by value'):");
        var s = new PointStruct { X = 1 };
        var c = new PointClass { X = 1 };
        MutateStruct(s); MutateClass(c); ReassignClass(c);
        Console.WriteLine($"   struct: {s.X} (değişmedi: kopya üzerinde çalışıldı)");
        Console.WriteLine($"   class : {c.X} (içeriği değişti: referansın kopyası aynı nesneyi gösteriyor; yeniden atama ise çağıranı ETKİLEMEDİ)");

        Console.WriteLine("\n3) Eşitlik: == varsayılan davranışı");
        Console.WriteLine($"   class  == → referans eşitliği: new PointClass{{X=1}} == new PointClass{{X=1}} → {new PointClass { X = 1 } == new PointClass { X = 1 }}");
        Console.WriteLine($"   struct Equals → alanların değeri: {new PointStruct { X = 1 }.Equals(new PointStruct { X = 1 })}   (struct için == operatörü tanımlı DEĞİLDİR; Equals vardır)");

        Console.WriteLine("\n4) YANILGI: 'value type her zaman stack'te'. Doğrusu: değişkenin YAŞADIĞI yer neyse oradadır:");
        Console.WriteLine("   • yerel değişken (closure'a yakalanmamış)      → stack (genelde)");
        Console.WriteLine("   • sınıfın ALANI olan struct                    → o sınıf nesnesiyle birlikte HEAP'te");
        Console.WriteLine("   • dizi/List elemanı olan struct                → dizinin belleğinde, yani HEAP'te");
        Console.WriteLine("   • closure'a yakalanan yerel değişken           → derleyicinin ürettiği sınıfta, HEAP'te");
        Console.WriteLine("   • boxing'e uğrayan struct                      → HEAP'te yeni bir nesne");
        Measure();
    }

    private static void Measure()
    {
        // Kanıt: GC'nin tahsis ettiği bayt sayısını ölçelim (stack'e yapılan yerleşim GC heap'i etkilemez)
        const int n = 1_000;
        long Alloc(Action a) { var before = GC.GetAllocatedBytesForCurrentThread(); a(); return GC.GetAllocatedBytesForCurrentThread() - before; }

        var locals = Alloc(() => { for (var i = 0; i < n; i++) { var p = new PointStruct { X = i }; _ = p.X; } });
        var array = Alloc(() => { var arr = new PointStruct[n]; for (var i = 0; i < n; i++) arr[i].X = i; });
        var holders = Alloc(() => { for (var i = 0; i < n; i++) { var h = new Holder { Point = new PointStruct { X = i } }; _ = h.Point; } });
        Console.WriteLine($"\n   GC heap tahsisi ({n} adet için):  yerel struct = {locals} B · struct dizisi = {array:N0} B · struct alanlı sınıf = {holders:N0} B");
    }

    private static void MutateStruct(PointStruct p) => p.X = 100;
    private static void MutateClass(PointClass p) => p.X = 100;
    private static void ReassignClass(PointClass p) { p = new PointClass { X = -1 }; }   // yerel kopya yeni nesneye baktı; çağıran görmez

    private struct PointStruct { public int X; }
    private sealed class PointClass { public int X; }
    private sealed class Holder { public PointStruct Point; }
}
