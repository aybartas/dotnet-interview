namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class Section03_Closures
{
    public static void Run()
    {
        Console.WriteLine("CLOSURE: a lambda captures the VARIABLES of the scope it is defined in (the variable itself, NOT its value).\n");

        Console.WriteLine("1) Basics: the lambda reads the captured variable AT THE MOMENT IT RUNS");
        var counter = 0;
        Action increment = () => counter++;                            // CAPTURED the outer 'counter' variable
        increment(); increment(); increment();
        Console.WriteLine($"   increment was called 3 times → counter = {counter}   (the lambda modified the outer variable)");

        var message = "first";
        Func<string> read = () => message;
        message = "changed later";
        Console.WriteLine($"   message changed AFTER the lambda was created → read() = \"{read()}\"   (the value was not copied; the variable was captured)");

        Console.WriteLine("\n2) ⭐ CLASSIC TRAP: for loop — there is a single 'i' variable and they all capture it");
        var wrong = new List<Func<int>>();
        for (var i = 0; i < 3; i++)
            wrong.Add(() => i);                                        // all 3 lambdas capture the SAME 'i'
        Console.WriteLine($"   expected: 0,1,2 — actual: {string.Join(",", wrong.Select(f => f()))}   (when the loop ends i == 3)");

        Console.WriteLine("\n   FIX: take a loop-local COPY in each iteration");
        var right = new List<Func<int>>();
        for (var i = 0; i < 3; i++)
        {
            var copy = i;                                              // a NEW variable in each iteration
            right.Add(() => copy);
        }
        Console.WriteLine($"   result: {string.Join(",", right.Select(f => f()))}");

        Console.WriteLine("\n3) The foreach variable is NEW in each iteration (C# 5+) → no trap:");
        var fromForeach = new List<Func<int>>();
        foreach (var n in new[] { 10, 20, 30 })
            fromForeach.Add(() => n);
        Console.WriteLine($"   {string.Join(",", fromForeach.Select(f => f()))}   (difference from for: the for variable is SINGLE, the foreach variable is PER ITERATION)");

        Console.WriteLine("\n4) What does the compiler generate behind the scenes?  Captured variables are moved into a 'display class' object on the HEAP:");
        var captured = 5;
        Func<int> f = () => captured;
        Console.WriteLine($"   f.Target type = {f.Target!.GetType().Name}  (a compiler-generated class; 'captured' is now a field of that object)");
        Func<int, int> noCapture = static x => x * 2;
        Console.WriteLine($"   non-capturing static lambda: Target = {(noCapture.Target is null ? "null" : noCapture.Target.GetType().Name)} (a single cached instance; no extra allocation)");

        Console.WriteLine("\n5) The cost of closures: a captured variable LIVES as long as the lambda does (capturing a big object keeps it in memory) + every closure allocates");
        long Alloc(Action a) { var b = GC.GetAllocatedBytesForCurrentThread(); a(); return GC.GetAllocatedBytesForCurrentThread() - b; }
        var factor = 2;
        _ = Alloc(() => { });
        var withClosure = Alloc(() => { for (var k = 0; k < 1000; k++) { Func<int> c = () => k + factor; _ = c(); } });
        var withoutClosure = Alloc(() => { for (var k = 0; k < 1000; k++) { Func<int, int> c = static x => x + 2; _ = c(k); } });
        Console.WriteLine(FormattableString.Invariant($"   1000 iterations: capturing lambda = {withClosure:N0} B · non-capturing static lambda = {withoutClosure:N0} B"));

        Console.WriteLine("""

        Interview summary:
          • Closure = lambda + the outer variables it captures. The variable is CAPTURED (not a copy of the value).
          • In a for loop all lambdas share the same counter variable → results are always the last value. Fix: a loop-local copy (not needed in foreach).
          • Cost: display class allocation + the captured object lives longer. On hot paths, use a `static` lambda to forbid capturing.
          • In concurrent code, if multiple threads modify a captured variable you get a race condition.
        """);
    }
}
