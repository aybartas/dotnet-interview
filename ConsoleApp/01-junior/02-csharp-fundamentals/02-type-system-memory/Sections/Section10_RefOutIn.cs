namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section10_RefOutIn
{
    public static void Run()
    {
        // ref: can be read and WRITTEN, must be assigned in advance by the caller
        int a = 1, b = 2;
        Swap(ref a, ref b);
        Console.WriteLine($"ref : Swap(ref a, ref b) → a={a}, b={b}");

        // out: MUST be assigned INSIDE the method, the caller doesn't need to assign it in advance (returning multiple values / the TryXxx pattern)
        if (TryDivide(10, 4, out var quotient, out var remainder))
            Console.WriteLine($"out : TryDivide(10, 4) → quotient={quotient}, remainder={remainder}");
        _ = int.TryParse("x", out _);                              // discard: don't care about the out value

        // in: pass by reference (NO copying) but the method CANNOT MODIFY it → for large structs
        var big = new BigStruct { A = 1, B = 2, C = 3, D = 4 };
        Console.WriteLine($"in  : Sum(in big) = {Sum(in big)}   (the 32-byte struct was not copied; read-only reference)");

        // When a reference type is passed BY VALUE: a COPY of the reference is passed
        var names = new List<string> { "a" };
        Reassign(names);
        Console.WriteLine($"List reassigned without ref → the caller still has [{string.Join(",", names)}]");
        ReassignWithRef(ref names);
        Console.WriteLine($"List reassigned with ref    → the caller has [{string.Join(",", names)}]");

        // ref local & ref return: access data IN PLACE (no copy) — a reference to an array element
        var array = new[] { 10, 20, 30 };
        ref var second = ref array[1];
        second = 99;
        Console.WriteLine($"ref local   : second = 99 → array [{string.Join(",", array)}]");
        ref var found = ref Find(array, 30);
        found++;
        Console.WriteLine($"ref return  : Find(array, 30)++ → array [{string.Join(",", array)}]");

        Console.WriteLine("""

        ref  → read + write, must be assigned beforehand       out → the method MUST write, the caller need not assign       in → read-only reference (no copy)
        Note: ref/out/in parameters can't be used in async methods and iterators. In modern code, returning a tuple/record is often more readable than `out`.
        """);
    }

    private static void Swap(ref int x, ref int y) => (x, y) = (y, x);

    private static bool TryDivide(int dividend, int divisor, out int quotient, out int remainder)
    {
        if (divisor == 0) { quotient = remainder = 0; return false; }   // outs must be assigned on EVERY path
        quotient = dividend / divisor;
        remainder = dividend % divisor;
        return true;
    }

    private static long Sum(in BigStruct s) => s.A + s.B + s.C + s.D;   // s.A = 5; → ✗ CS8332: an in parameter can't be modified

    private static void Reassign(List<string> list) => list = ["new"];
    private static void ReassignWithRef(ref List<string> list) => list = ["new"];

    private static ref int Find(int[] items, int value)
    {
        for (var i = 0; i < items.Length; i++)
            if (items[i] == value) return ref items[i];
        throw new KeyNotFoundException();
    }

    private struct BigStruct { public long A, B, C, D; }
}
