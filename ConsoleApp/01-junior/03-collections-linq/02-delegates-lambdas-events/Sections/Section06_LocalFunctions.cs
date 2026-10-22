namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class Section06_LocalFunctions
{
    public static void Run()
    {
        Console.WriteLine("Local function: a named function defined INSIDE a method and used only there.\n");

        Console.WriteLine("1) Recursion: a local function calls itself directly; for a lambda you must declare the delegate first");
        Console.WriteLine($"   Factorial(5) local function = {Factorial(5)}");
        Func<int, int>? factLambda = null;                           // lambda recursion: first a variable assigned null, then the definition (ugly)
        factLambda = n => n <= 1 ? 1 : n * factLambda!(n - 1);
        Console.WriteLine($"   Factorial(5) lambda           = {factLambda(5)}   (works but is hard to read)");

        Console.WriteLine("\n2) Can be used BEFORE its definition (a lambda cannot be used before it is assigned to a variable):");
        Console.WriteLine($"   {Twice(21)}");
        int Twice(int x) => x * 2;                                    // although defined below, it could be called above

        Console.WriteLine("\n3) Delegate allocation: a local function does not allocate unless it is CONVERTED to a delegate (it is called directly). A lambda is always a delegate object.");
        long Alloc(Action a) { var b = GC.GetAllocatedBytesForCurrentThread(); a(); return GC.GetAllocatedBytesForCurrentThread() - b; }
        _ = Alloc(() => { });
        var viaLocal = Alloc(() => { var captured = 5; var sum = 0; for (var i = 0; i < 1000; i++) sum += AddCaptured(i); _ = sum; int AddCaptured(int x) => x + captured; });
        var viaLambda = Alloc(() => { var captured = 5; var sum = 0; Func<int, int> add = x => x + captured; for (var i = 0; i < 1000; i++) sum += add(i); _ = sum; });
        Console.WriteLine($"   capturing local function (direct call): {viaLocal} B · lambda (Func object + closure): {viaLambda} B");

        Console.WriteLine("\n4) `static` local function: cannot capture an outer variable → no accidental closure:");
        Console.WriteLine($"   Square(9) static local = {Square(9)}");
        static int Square(int x) => x * x;

        Console.WriteLine("\n5) Classic use: iterator + EARLY argument validation");
        try { _ = BadRange(-1); Console.WriteLine("   BadRange(-1) → NO error (the iterator is deferred; validation happens on the first MoveNext!)"); }
        catch (ArgumentOutOfRangeException) { Console.WriteLine("   BadRange(-1) → threw"); }
        try { _ = GoodRange(-1); }
        catch (ArgumentOutOfRangeException) { Console.WriteLine("   GoodRange(-1) → threw IMMEDIATELY (validation in the outer method, iterator in the local function)"); }

        Console.WriteLine("""

        When to use a local function?  Helper logic used in only one method, recursion, early validation for iterators/async, avoiding allocation (static).
        When to use a lambda?          When you need a delegate/Expression value: passing as a parameter, LINQ, event subscription, storing.
        Common: both capture outer variables (closure) — forbidden with the static modifier.
        """);

        static int Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);
    }

    private static IEnumerable<int> BadRange(int count)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));   // iterator → the body is DEFERRED
        for (var i = 0; i < count; i++) yield return i;
    }

    private static IEnumerable<int> GoodRange(int count)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));   // outer method: runs immediately
        return Iterator();

        IEnumerable<int> Iterator()
        {
            for (var i = 0; i < count; i++) yield return i;
        }
    }
}
