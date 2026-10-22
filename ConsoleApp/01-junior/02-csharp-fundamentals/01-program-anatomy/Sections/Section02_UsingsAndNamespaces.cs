// 1) Normal using
using System.Text;

// 2) using static: use a type's static members without writing the type name
using static System.Math;

// 3) Alias: a short name for long/conflicting names (C# 12+: ANY type, such as tuples, can be aliased)
using Json = System.Text.Json.JsonSerializer;
using Point = (int X, int Y);

// FILE-SCOPED namespace (C# 10): the whole file is in this namespace; saves one indentation level. Only ONE per file.
namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section02_UsingsAndNamespaces
{
    public static void Run()
    {
        var sb = new StringBuilder("normal using → StringBuilder");
        Console.WriteLine(sb);

        Console.WriteLine($"using static → Sqrt(16) = {Sqrt(16)}, PI = {PI:F4}   (we didn't write Math.)");

        Console.WriteLine($"alias        → {Json.Serialize(new { Name = "Alice" })}   (Json instead of System.Text.Json.JsonSerializer)");

        Point p = (3, 4);
        Console.WriteLine($"alias (tuple)→ {p}");

        // global using (GlobalUsings.cs) → we didn't write 'using System.Diagnostics;' in this file for Stopwatch:
        var sw = Stopwatch.StartNew();
        Console.WriteLine($"global using → Stopwatch used ({sw.ElapsedTicks} ticks)");

        // implicit usings (csproj: <ImplicitUsings>enable</ImplicitUsings>): System, System.IO, System.Linq,
        // System.Collections.Generic, System.Threading, System.Threading.Tasks, System.Net.Http are added globally.
        var evens = Enumerable.Range(1, 6).Where(n => n % 2 == 0);     // we didn't write a using for System.Linq
        Console.WriteLine($"implicit     → LINQ: {string.Join(",", evens)}");

        // The fully qualified name resolves conflicts:
        Console.WriteLine($"Namespace    → {typeof(Section02_UsingsAndNamespaces).Namespace}");
    }
}
