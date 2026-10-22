using System.Reflection;

namespace Junior.CSharpFundamentals.ProgramAnatomy.AccessModifiers;

/// <summary>Access modifiers experiment: Vault carries one member of each of the 6 access levels; this class is the 'outside' code.</summary>
public static class AccessModifiersDemo
{
    public static void Run()
    {
        // Access modifiers.
        //
        //   dotnet run -- J2.1 access-modifiers           → runs (all of it is valid code)
        //   dotnet build -p:DefineConstants=SHOW_ERRORS    → shows the COMPILE ERRORS (CS0122 ...)  — run in the ConsoleApp folder
        //
        // Rule summary (⭐ interview):
        //   private             → only the same CLASS
        //   private protected   → same class + derived types in the SAME assembly       (protected ∩ internal)
        //   protected           → same class + derived types (including other assemblies)
        //   internal            → everyone in the same ASSEMBLY
        //   protected internal  → internal ∪ protected                                  (protected ∪ internal)
        //   public              → everyone
        //
        // We can't compile the "other assembly" rows in this project (there is only one assembly); the README exercise shows them with a second class library.

        Console.WriteLine("── Within the same assembly ──");
        Console.WriteLine($"Same class          : {new Vault().ReadAllFromSameClass()}");
        Console.WriteLine($"Derived             : {new SameAssemblyDerived().Peek()}");
        Console.WriteLine($"Not derived         : {SameAssemblyOther.Peek(new Vault())}");

        Console.WriteLine($"\n{Factory.DescribeDefaults()}");

        Console.WriteLine("\n── Actual access levels via reflection (what the CLR sees) ──");
        foreach (var p in typeof(Vault).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).OrderBy(p => p.Name))
        {
            var m = p.GetMethod!;
            var level = m.IsPublic ? "public"
                      : m.IsFamilyOrAssembly ? "protected internal"
                      : m.IsFamilyAndAssembly ? "private protected"
                      : m.IsFamily ? "protected"
                      : m.IsAssembly ? "internal"
                      : "private";
            Console.WriteLine($"  {p.Name,-28} → {level}");
        }

        // Internal types of another assembly: internal is 'ours' for our own assembly; the BCL's internal types are closed to us.
        var bcl = typeof(object).Assembly;
        var all = bcl.GetTypes();
        var internals = all.Where(t => t.IsNotPublic).ToList();
        Console.WriteLine($"\n── Internal types of another assembly ({bcl.GetName().Name}) ──");
        Console.WriteLine($"  {all.Length} types in total: {all.Length - internals.Count} public, {internals.Count} internal/private");
        Console.WriteLine($"  e.g. internal types (you can't use them with 'new' or by name in your code, CS0122): {string.Join(", ", internals.OrderBy(t => t.FullName).Take(3).Select(t => t.FullName))}");
        Console.WriteLine("  Reflection can find them; the compiler won't allow access.");
    }
}
