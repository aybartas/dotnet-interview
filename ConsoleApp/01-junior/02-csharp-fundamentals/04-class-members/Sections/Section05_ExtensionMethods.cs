using Junior.CSharpFundamentals.ClassMembers.Extensions;      // ← the using REQUIRED for the extensions to be visible

namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section05_ExtensionMethods
{
    public static void Run()
    {
        var text = "hello world";
        Console.WriteLine($"text.Shout()          → {text.Shout()}          (compiler: StringExtensions.Shout(text))");
        Console.WriteLine($"text.Truncate(7)      → {text.Truncate(7)}");

        string? nothing = null;
        Console.WriteLine($"nothing.IsBlank()     → {nothing.IsBlank()}     (null receiver: NO NullReferenceException — because it is really a static call)");

        string?[] mixed = ["a", null, "b"];
        Console.WriteLine($"WhereNotNull          → [{string.Join(", ", mixed.WhereNotNull())}]");

        var w = new Widget();
        Console.WriteLine($"\nw.Describe()          → {w.Describe()}         ← the instance method wins; an extension with the same signature is NEVER called");
        Console.WriteLine($"w.Describe(\"prefix\")  → {w.Describe("prefix")}  ← an extension with a different signature can be called");
        Console.WriteLine($"WidgetExtensions.Describe(w) → {WidgetExtensions.Describe(w)}  (the extension is reachable through the static call form)");

        Console.WriteLine($"\nLINQ is entirely extension methods: Enumerable.Range(1,5).Where(...) → System.Linq.Enumerable.Where(source, predicate)");
        Console.WriteLine($"   typeof(Enumerable).IsAbstract && IsSealed (static class) = {typeof(Enumerable).IsAbstract && typeof(Enumerable).IsSealed}");

        Console.WriteLine("""

        Resolution order: (1) instance method → (2) extensions in the caller's namespace/usings → compile error if none found.
        When to use an extension?  Adding helper behavior to a type you don't own (string, IEnumerable<T>, the framework); fluent APIs; adding behavior to an interface (without changing a base class).
        When inheritance / wrapper / composition?  When you need to add state, want to override behavior (an extension can't override), or need polymorphism.
        Pitfalls: overuse (extensions everywhere, discoverability drops), namespace pollution (don't put extensions in System.* namespaces), null behavior.
        (C# 14 extension members → M1.1)
        """);
    }
}
