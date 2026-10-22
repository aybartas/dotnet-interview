namespace Junior.BuildRunBasics.BigPicture;

/// <summary>
/// 🛠 J5.1: the "lowered" (compiler-expanded) form of 'foreach' and 'using'. We write the same logic by hand and compare the results;
/// the real thing can be seen on sharplab.io (right pane: C# / IL / "Lowered").
/// </summary>
public static class Section06_Lowering
{
    private sealed class Resource(string name) : IDisposable
    {
        public void Dispose() => Console.WriteLine($"   [{name}] Dispose() called");
    }

    public static void Run()
    {
        // ─── foreach ───
        var list = new List<int> { 1, 2, 3 };

        var sum1 = 0;
        foreach (var item in list) sum1 += item;                       // what we write

        var sum2 = 0;
        {                                                              // what the compiler expands it to (roughly):
            List<int>.Enumerator e = list.GetEnumerator();             //   struct enumerator (not boxed)
            try
            {
                while (e.MoveNext())
                {
                    var item = e.Current;
                    sum2 += item;
                }
            }
            finally
            {
                e.Dispose();                                           //   foreach ALWAYS disposes the enumerator
            }
        }
        Console.WriteLine($"foreach        → {sum1}\nlowered while  → {sum2}   (same result)\n");

        // ─── using ───
        Console.WriteLine("using var r = new Resource(\"A\");  // what we write");
        {
            using var r = new Resource("A");
            Console.WriteLine("   [A] in use");
        }

        Console.WriteLine("\nlowered: try/finally");
        {
            var r = new Resource("B");
            try
            {
                Console.WriteLine("   [B] in use");
            }
            finally
            {
                if (r != null) r.Dispose();                            // null check: using silently skips a null resource
            }
        }

        // ─── array foreach ───
        int[] array = [4, 5, 6];
        var sum3 = 0;
        for (var i = 0; i < array.Length; i++) sum3 += array[i];       // on an array, foreach lowers to an indexed for in IL (no enumerator)
        Console.WriteLine($"\narray foreach → indexed for: {sum3}");

        Console.WriteLine("""

        Try it on sharplab.io (paste into the code box, pick "Results: Lowered C#" / "IL" at the top right):
            using System; using System.Collections.Generic;
            public class C { public int M(List<int> l) { var s = 0; foreach (var x in l) s += x; return s; } }
        What you'll see: GetEnumerator → try { while (MoveNext) { x = Current; ... } } finally { Dispose }.

        More "syntactic sugar" examples — all expanded into other code by the compiler:
          • using / await using  → try/finally
          • foreach              → GetEnumerator + MoveNext/Current + try/finally (for on arrays)
          • lock                 → Monitor.Enter/Exit (or .NET 9+ System.Threading.Lock.EnterScope)
          • async/await          → state machine class (→ M4.2)
          • string interpolation → DefaultInterpolatedStringHandler
          • record, primary ctor, pattern matching, switch expression → ordinary class/if-else code
          • lambda / closure     → compiler-generated 'display class' (→ J3.2)
        """);
    }
}
