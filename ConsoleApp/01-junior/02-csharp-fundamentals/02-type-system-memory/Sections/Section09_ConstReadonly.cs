using System.Reflection;

namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section09_ConstReadonly
{
    public sealed class Settings
    {
        public const int MaxItems = 100;                              // const: compile-time constant, EMBEDDED INTO CALLERS
        public static readonly DateTime StartedAt = DateTime.Now;     // static readonly: assigned once at run time
        public readonly Guid InstanceId = Guid.NewGuid();             // readonly (instance field): assigned in the constructor/declaration
    }

    public static void Run()
    {
        var t = typeof(Settings);
        Console.WriteLine("Difference via reflection:");
        foreach (var name in new[] { nameof(Settings.MaxItems), nameof(Settings.StartedAt), nameof(Settings.InstanceId) })
        {
            var f = t.GetField(name)!;
            Console.WriteLine($"   {name,-10} → IsLiteral(const)={f.IsLiteral,-5} IsInitOnly(readonly)={f.IsInitOnly,-5} IsStatic={f.IsStatic}");
        }

        Console.WriteLine("""

        const                : only primitive/string/enum/null values known at compile time; implicitly static; the VALUE is COPIED to every place it is used (even in other assemblies) (written into the IL as a literal).
        static readonly      : can be computed at run time (DateTime.Now, new List<>()); assigned once; other assemblies READ the VALUE at run time.
        readonly (instance)  : each object has its own value; assigned only in the constructor/initializer.

        ⚠ VERSIONING TRAP: If you change the `public const int MaxRetries = 3` in a library to 5 and recompile and ship ONLY the library,
          the application that uses it STILL uses 3 until it is recompiled (the value is embedded in its IL).
          To prove it: dotnet run -- J2.2 const-embedding (steps in README 3.9)
          Rule: for a public value that may change use `public static readonly` (or a property); reserve const for things that truly never change (Math.PI, fixed number of days of the week).
        (readonly ≠ immutable: a `readonly List<int> L` field can't be reassigned but the INSIDE of the list can be modified.)
        """);

        var readonlyList = new List<int> { 1 };
        var holder = new ReadonlyHolder(readonlyList);
        holder.Items.Add(2);                                      // allowed: the reference is fixed, the object is mutable
        Console.WriteLine($"item added to the readonly List → [{string.Join(",", holder.Items)}]");
    }

    private sealed class ReadonlyHolder(List<int> items) { public readonly List<int> Items = items; }
}
