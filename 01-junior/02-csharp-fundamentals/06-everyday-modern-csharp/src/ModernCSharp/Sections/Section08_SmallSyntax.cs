namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section08_SmallSyntax
{
    public static void Run()
    {
        Console.WriteLine("1) File-scoped namespace (C# 10): `namespace X;` — tüm dosya o namespace'te, bir girinti seviyesi kazanılır (bu repo dosyalarının hepsi böyle).");

        Console.WriteLine("\n2) Global using (C# 10) + implicit usings: GlobalUsings.cs'teki `global using System.Globalization;` bu dosyada using YAZMADAN kullanılıyor:");
        Console.WriteLine($"   CultureInfo.InvariantCulture.Name = \"{CultureInfo.InvariantCulture.Name}\" (boş = invariant)");

        Console.WriteLine("\n3) Target-typed new (C# 9): tip sol taraftan biliniyorsa `new()`:");
        List<Dictionary<string, int>> old = new List<Dictionary<string, int>>();
        List<Dictionary<string, int>> modern = new();
        Dictionary<string, List<int>> map = new() { ["a"] = [1, 2] };
        Console.WriteLine($"   eski: new List<Dictionary<string,int>>() · yeni: new() → {modern.Count}/{old.Count}; map[a].Count = {map["a"].Count}");
        Console.WriteLine("   ⚠ `var x = new();` HATA — hedef tip yok; yalnızca tip AÇIKÇA belliyse (alan, parametre, dönüş tipi).");

        Console.WriteLine("\n4) `using` declaration (C# 8): süslü parantezsiz — Dispose, kapsam (metot) SONUNDA:");
        UsingDeclaration();

        Console.WriteLine("""

        5) Diğer günlük kısaltmalar:
           • expression-bodied member `=>`       • null-coalescing `??`, `??=`        • `is not null`         • discard `_`
           • tuple deconstruction `var (a, b) =` • default literal `default`           • static local function / static lambda
           • lambda'da varsayılan parametre (C# 12): `var f = (int x, int y = 10) => x + y;`
        """);
        var f = (int x, int y = 10) => x + y;
        Console.WriteLine($"   lambda varsayılan parametre: f(1) = {f(1)}, f(1, 2) = {f(1, 2)}");
    }

    private static void UsingDeclaration()
    {
        // ESKİ: using (var r = new Res("A")) { … }   ← iç içe kaynaklarda girinti piramidi
        using var a = new Res("A");
        using var b = new Res("B");
        Console.WriteLine("   metot gövdesi çalışıyor...");
    }                                                      // ← B sonra A Dispose edilir

    private sealed class Res(string name) : IDisposable
    {
        public void Dispose() => Console.WriteLine($"   [{name}] Dispose");
    }
}
