namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section01_TryCatchFinally
{
    public static void Run()
    {
        Console.WriteLine("1) Execution order: try → (if an error occurs) the FIRST matching catch → finally (ALWAYS)");
        Flow(throwIt: false);
        Flow(throwIt: true);

        Console.WriteLine("\n2) catch blocks are tried IN ORDER: write them from specific to general (putting the general one first is a compile error: CS0160)");
        foreach (var f in new Func<int>[] { () => int.Parse("x"), () => 1 / int.Parse("0"), () => ((int[])null!)[0] })
        {
            try { f(); }
            catch (FormatException) { Console.WriteLine("   FormatException caught"); }
            catch (DivideByZeroException) { Console.WriteLine("   DivideByZeroException caught"); }
            catch (Exception ex) { Console.WriteLine($"   general catch: {ex.GetType().Name}"); }
        }

        Console.WriteLine("\n3) finally runs even AFTER return (after the return value is computed, before control goes back to the caller)");
        Console.WriteLine($"   ReturnInTry() = {ReturnInTry()}");

        Console.WriteLine("\n4) using / await using: syntactic sugar over try/finally for IDisposable (the resource is released in every case)");
        try
        {
            using var res = new Res("R1");
            Console.WriteLine("   in use...");
            throw new InvalidOperationException("error");
        }
        catch (InvalidOperationException) { Console.WriteLine("   catch ran (Dispose ran BEFORE it: see the 'released' line above)"); }

        Console.WriteLine("""

        WARNINGS
          • Don't write `return`/`throw` inside finally: it silently SWALLOWS the pending exception or overwrites the original.
          • If an exception is thrown inside finally, the original exception is LOST → keep finally simple (cleanup only).
          • catch (Exception) { } (empty catch) SWALLOWS the error → bugs that are impossible to find (→ Section 6).
          • Use `using` instead of try/finally to release resources.
        """);
    }

    private static void Flow(bool throwIt)
    {
        Console.WriteLine($"   --- throwIt = {throwIt} ---");
        try
        {
            Console.WriteLine("   try: started");
            if (throwIt) throw new InvalidOperationException("blew up");
            Console.WriteLine("   try: finished");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"   catch: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("   finally: ALWAYS runs");
        }
    }

    private static int ReturnInTry()
    {
        try { return 1; }
        finally { Console.WriteLine("   (finally ran after return 1)"); }
    }

    private sealed class Res(string name) : IDisposable
    {
        public void Dispose() => Console.WriteLine($"   [{name}] released (Dispose)");
    }
}
