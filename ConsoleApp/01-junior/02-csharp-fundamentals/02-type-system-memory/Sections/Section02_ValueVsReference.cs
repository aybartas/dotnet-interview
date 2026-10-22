namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section02_ValueVsReference
{
    public static void Run()
    {
        // THE REAL DIFFERENCE: COPY SEMANTICS. (The stack/heap distinction is an implementation detail; shown below.)
        Console.WriteLine("1) Assignment = copying. What gets copied changes:");
        var p1 = new PointStruct { X = 1 };
        var p2 = p1;                       // the VALUE is copied → two independent objects
        p2.X = 99;
        Console.WriteLine($"   struct : p1.X = {p1.X}, p2.X = {p2.X}   (p2 changed, p1 was not affected)");

        var c1 = new PointClass { X = 1 };
        var c2 = c1;                       // the REFERENCE is copied → both point to the SAME object
        c2.X = 99;
        Console.WriteLine($"   class  : c1.X = {c1.X}, c2.X = {c2.X}   (same object! ReferenceEquals = {ReferenceEquals(c1, c2)})");

        Console.WriteLine("\n2) Passing to a method follows the SAME rule (default: pass by value):");
        var s = new PointStruct { X = 1 };
        var c = new PointClass { X = 1 };
        MutateStruct(s); MutateClass(c); ReassignClass(c);
        Console.WriteLine($"   struct: {s.X} (unchanged: the method worked on a copy)");
        Console.WriteLine($"   class : {c.X} (contents changed: the copy of the reference points to the same object; reassignment did NOT affect the caller)");

        Console.WriteLine("\n3) Equality: default behavior of ==");
        Console.WriteLine($"   class  == → reference equality: new PointClass{{X=1}} == new PointClass{{X=1}} → {new PointClass { X = 1 } == new PointClass { X = 1 }}");
        Console.WriteLine($"   struct Equals → value of the fields: {new PointStruct { X = 1 }.Equals(new PointStruct { X = 1 })}   (the == operator is NOT defined for a struct; Equals is)");

        Console.WriteLine("\n4) MISCONCEPTION: 'value types are always on the stack'. The truth: it is wherever the variable LIVES:");
        Console.WriteLine("   • local variable (not captured by a closure)   → stack (usually)");
        Console.WriteLine("   • struct that is a FIELD of a class            → on the HEAP together with that class object");
        Console.WriteLine("   • struct that is an array/List element         → in the array's memory, i.e. on the HEAP");
        Console.WriteLine("   • local variable captured by a closure         → in the compiler-generated class, on the HEAP");
        Console.WriteLine("   • struct that gets boxed                       → a new object on the HEAP");
        Measure();
    }

    private static void Measure()
    {
        // Proof: let's measure the number of bytes the GC allocated (placement on the stack doesn't affect the GC heap)
        const int n = 1_000;
        long Alloc(Action a) { var before = GC.GetAllocatedBytesForCurrentThread(); a(); return GC.GetAllocatedBytesForCurrentThread() - before; }

        var locals = Alloc(() => { for (var i = 0; i < n; i++) { var p = new PointStruct { X = i }; _ = p.X; } });
        var array = Alloc(() => { var arr = new PointStruct[n]; for (var i = 0; i < n; i++) arr[i].X = i; });
        var holders = Alloc(() => { for (var i = 0; i < n; i++) { var h = new Holder { Point = new PointStruct { X = i } }; _ = h.Point; } });
        Console.WriteLine($"\n   GC heap allocation (for {n} items):  local struct = {locals} B · struct array = {array:N0} B · class with struct field = {holders:N0} B");
    }

    private static void MutateStruct(PointStruct p) => p.X = 100;
    private static void MutateClass(PointClass p) => p.X = 100;
    private static void ReassignClass(PointClass p) { p = new PointClass { X = -1 }; }   // the local copy pointed to a new object; the caller doesn't see it

    private struct PointStruct { public int X; }
    private sealed class PointClass { public int X; }
    private sealed class Holder { public PointStruct Point; }
}
