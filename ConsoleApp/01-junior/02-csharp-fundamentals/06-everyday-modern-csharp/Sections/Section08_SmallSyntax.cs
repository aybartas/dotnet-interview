namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section08_SmallSyntax
{
    public static void Run()
    {
        Console.WriteLine("1) File-scoped namespace (C# 10): `namespace X;` — the whole file is in that namespace, saving one indentation level (all files in this repo use it).");

        Console.WriteLine("\n2) Global using (C# 10) + implicit usings: the `global using System.Globalization;` in GlobalUsings.cs is used in this file WITHOUT writing a using:");
        Console.WriteLine($"   CultureInfo.InvariantCulture.Name = \"{CultureInfo.InvariantCulture.Name}\" (empty = invariant)");

        Console.WriteLine("\n3) Target-typed new (C# 9): `new()` when the type is known from the left-hand side:");
        List<Dictionary<string, int>> old = new List<Dictionary<string, int>>();
        List<Dictionary<string, int>> modern = new();
        Dictionary<string, List<int>> map = new() { ["a"] = [1, 2] };
        Console.WriteLine($"   old: new List<Dictionary<string,int>>() · new: new() → {modern.Count}/{old.Count}; map[a].Count = {map["a"].Count}");
        Console.WriteLine("   ⚠ `var x = new();` is an ERROR — no target type; only when the type is EXPLICITLY known (field, parameter, return type).");

        Console.WriteLine("\n4) `using` declaration (C# 8): without braces — Dispose happens at the END of the scope (method):");
        UsingDeclaration();

        Console.WriteLine("""

        5) Other everyday shorthands:
           • expression-bodied member `=>`       • null-coalescing `??`, `??=`        • `is not null`         • discard `_`
           • tuple deconstruction `var (a, b) =` • default literal `default`           • static local function / static lambda
           • default parameters in lambdas (C# 12): `var f = (int x, int y = 10) => x + y;`
        """);
        var f = (int x, int y = 10) => x + y;
        Console.WriteLine($"   lambda default parameter: f(1) = {f(1)}, f(1, 2) = {f(1, 2)}");
    }

    private static void UsingDeclaration()
    {
        // OLD: using (var r = new Res("A")) { … }   ← an indentation pyramid with nested resources
        using var a = new Res("A");
        using var b = new Res("B");
        Console.WriteLine("   method body running...");
    }                                                      // ← B is disposed, then A

    private sealed class Res(string name) : IDisposable
    {
        public void Dispose() => Console.WriteLine($"   [{name}] Dispose");
    }
}
