using Microsoft.CSharp.RuntimeBinder;

namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section08_VarDynamicObject
{
    public static void Run()
    {
        Console.WriteLine("var     → COMPILE-TIME inference. Still strongly typed! (not dynamic)");
        var x = 5;                       // the compiler inferred 'int'; there is no such thing as 'var' in IL
        var list = new List<string>();
        Console.WriteLine($"   var x = 5 → {x.GetType().Name};   var list → {list.GetType().Name}");
        // x = "text";                   // ✗ CS0029: no conversion from string to int (the type of a var variable is FIXED)
        var anon = new { Name = "Alice", Age = 30 };
        Console.WriteLine($"   'var' is REQUIRED for an anonymous type: {anon}  → {anon.GetType().Name}");

        Console.WriteLine("\nobject  → EVERYTHING; but accessing members needs a CAST, and value types get BOXED");
        object o = "hello";
        // Console.WriteLine(o.Length);  // ✗ CS1061: object has no Length
        Console.WriteLine($"   ((string)o).Length = {((string)o).Length}   — compile-time safety is preserved, a wrong cast blows up at run time");

        Console.WriteLine("\ndynamic → binding at RUN TIME; the compiler doesn't check, errors appear while running");
        dynamic d = "hello";
        Console.WriteLine($"   d.Length = {d.Length}  (resolved at run time)");
        try { Console.WriteLine(d.Missing); }
        catch (RuntimeBinderException ex) { Console.WriteLine($"   d.Missing → {ex.GetType().Name}: {ex.Message}"); }
        d = 42;                          // a dynamic variable can change type
        Console.WriteLine($"   d = 42 → d + 1 = {d + 1}");

        Console.WriteLine("""

        ┌─────────┬────────────────────────┬────────────────────────────┬────────────────────────────────────────────────────────┐
        │         │ Type known when?       │ Error appears when?        │ Usage                                                  │
        ├─────────┼────────────────────────┼────────────────────────────┼────────────────────────────────────────────────────────┤
        │ var     │ compile time           │ compile time               │ Everyday code (type obvious on the right)              │
        │ object  │ compile time = object  │ at cast time (run time)    │ Truly "anything" — generics are usually better         │
        │ dynamic │ RUN TIME               │ while running              │ COM/Office interop, schemaless data like JSON, rarely  │
        └─────────┴────────────────────────┴────────────────────────────┴────────────────────────────────────────────────────────┘
        dynamic: no IntelliSense, no compile-time safety, binder cost on the first call. Avoid it in new code.
        """);
    }
}
