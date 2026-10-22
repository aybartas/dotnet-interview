namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section09_Lifecycle
{
    public static void Run()
    {
        Console.WriteLine("Object lifecycle: creation (ctor) → use → becomes unreachable → the GC collects it.");
        Console.WriteLine("The GC manages ONLY MEMORY. RELEASING UNMANAGED resources such as file handles, sockets, DB connections is YOUR responsibility → IDisposable.\n");

        Console.WriteLine("1) using block: Dispose() is GUARANTEED to be called on exit (even with an exception → try/finally)");
        using (var r = new Resource("A"))
        {
            r.Use();
        }

        Console.WriteLine("\n2) using declaration (C# 8): Dispose at the end of the scope; multiple resources are released in REVERSE order");
        UseTwo();

        Console.WriteLine("\n3) Dispose is called even with an exception:");
        try
        {
            using var r = new Resource("C");
            throw new InvalidOperationException("error during use");
        }
        catch (InvalidOperationException ex) { Console.WriteLine($"   caught: {ex.Message}"); }

        Console.WriteLine("\n4) WITHOUT using: Dispose isn't called → the resource is released at an indeterminate GC time (if there is a finalizer; otherwise a LEAK):");
        var leaked = new Resource("D");
        leaked.Use();
        Console.WriteLine("   (D's Dispose was not called — there is NO 'released' line in the output)");

        Console.WriteLine("\nThe full Dispose pattern (finalizer, SuppressFinalize, protected Dispose(bool), async dispose) → M1.5");
    }

    private static void UseTwo()
    {
        using var first = new Resource("B1");
        using var second = new Resource("B2");
        first.Use(); second.Use();
        Console.WriteLine("   reached the end of the method...");
    }                                                                       // ← B2, then B1 is disposed

    private sealed class Resource : IDisposable
    {
        private readonly string _name;
        public Resource(string name) { _name = name; Console.WriteLine($"   [{_name}] opened"); }
        public void Use() => Console.WriteLine($"   [{_name}] in use");
        public void Dispose() => Console.WriteLine($"   [{_name}] released (Dispose)");
    }
}
