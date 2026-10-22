using Junior.CSharpFundamentals.ClassMembers.Models;
using Trace = Junior.CSharpFundamentals.ClassMembers.Models.Trace;   // the alias resolves the clash with Trace from the project's global using System.Diagnostics

namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section02_InitializationOrder
{
    public static void Run()
    {
        Console.WriteLine("1) Steps that run the first time 'new DerivedClass()' is called (static + instance):\n");
        Trace.Reset();
        _ = new DerivedClass();
        foreach (var step in Trace.Steps) Console.WriteLine($"   {step}");

        Console.WriteLine("\n2) Second 'new DerivedClass()' — static steps are NOT repeated (once per type):\n");
        Trace.Reset();
        _ = new DerivedClass();
        foreach (var step in Trace.Steps) Console.WriteLine($"   {step}");

        Console.WriteLine("\n3) Constructor chain this(...)/base(...) — new DerivedClass(\"x\"):\n");
        Trace.Reset();
        _ = new DerivedClass("x");
        foreach (var step in Trace.Steps) Console.WriteLine($"   {step}");

        Console.WriteLine("""

        SUMMARY (instance):  fields are zeroed → field initializers from MOST DERIVED TO BASE → ctor bodies from BASE TO MOST DERIVED
        SUMMARY (static)  :  each type's static field initializers + static ctor run ONCE, before that type is FIRST used (thread-safe guaranteed).
                    Note: Base's static initialization is triggered AFTER Derived's instance fields are set up, on first entry into the base ctor.

        Kinds of constructors:
          • default (compiler-generated)  — if you write no ctor a parameterless public ctor is generated; if you write one it is NOT generated
          • parameterized                 • static (type initialization)   • private (singleton / static factory pattern)
          • copy constructor (no special C# syntax; a ctor taking a parameter of the same type)
          • this(...) chains to another ctor of the same class, base(...) chains to the base ctor
        """);

        var a = Connection.Create("db");
        var copy = new Connection(a);
        // var c = new Connection("x", 1);   // ✗ CS0122: private ctor
        Console.WriteLine($"factory: {a.Name} (retries {a.Retries});  copy ctor: {copy.Name};  Is Shared the same instance? {ReferenceEquals(Connection.Shared, Connection.Shared)}");
    }
}
