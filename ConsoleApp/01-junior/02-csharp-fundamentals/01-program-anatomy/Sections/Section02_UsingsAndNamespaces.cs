// 1) Normal using
using System.Text;

// 2) using static: bir tipin static üyelerini tip adı yazmadan kullanmak
using static System.Math;

// 3) Alias: uzun/çakışan adlara kısa ad (C# 12+: tuple vb. HER tipe alias verilebilir)
using Json = System.Text.Json.JsonSerializer;
using Point = (int X, int Y);

// FILE-SCOPED namespace (C# 10): tüm dosya bu namespace'te; bir seviye girinti tasarrufu. Dosyada TEK olabilir.
namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section02_UsingsAndNamespaces
{
    public static void Run()
    {
        var sb = new StringBuilder("normal using → StringBuilder");
        Console.WriteLine(sb);

        Console.WriteLine($"using static → Sqrt(16) = {Sqrt(16)}, PI = {PI:F4}   (Math. yazmadık)");

        Console.WriteLine($"alias        → {Json.Serialize(new { Ad = "Ayşe" })}   (System.Text.Json.JsonSerializer yerine Json)");

        Point p = (3, 4);
        Console.WriteLine($"alias (tuple)→ {p}");

        // global using (GlobalUsings.cs) → Stopwatch için bu dosyada 'using System.Diagnostics;' yazmadık:
        var sw = Stopwatch.StartNew();
        Console.WriteLine($"global using → Stopwatch kullanıldı ({sw.ElapsedTicks} tick)");

        // implicit usings (csproj: <ImplicitUsings>enable</ImplicitUsings>): System, System.IO, System.Linq,
        // System.Collections.Generic, System.Threading, System.Threading.Tasks, System.Net.Http global olarak eklenir.
        var evens = Enumerable.Range(1, 6).Where(n => n % 2 == 0);     // System.Linq için using yazmadık
        Console.WriteLine($"implicit     → LINQ: {string.Join(",", evens)}");

        // Tam nitelikli ad (fully qualified) çakışmaları çözer:
        Console.WriteLine($"Namespace    → {typeof(Section02_UsingsAndNamespaces).Namespace}");
    }
}
