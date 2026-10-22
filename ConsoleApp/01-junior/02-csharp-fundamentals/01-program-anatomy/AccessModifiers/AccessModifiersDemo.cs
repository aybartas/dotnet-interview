using System.Reflection;

namespace Junior.CSharpFundamentals.ProgramAnatomy.AccessModifiers;

/// <summary>Erişim belirleyicileri deneyi: Vault 6 erişim düzeyinden birer üye taşır; bu sınıf 'dışarıdaki' koddur.</summary>
public static class AccessModifiersDemo
{
    public static void Run()
    {
        // Erişim belirleyicileri.
        //
        //   dotnet run -- J2.1 access-modifiers           → çalışır (hepsi geçerli kod)
        //   dotnet build -p:DefineConstants=SHOW_ERRORS    → DERLEME HATALARINI görün (CS0122 ...)  — ConsoleApp klasöründe
        //
        // Kural özeti (⭐ mülakat):
        //   private             → sadece aynı SINIF
        //   private protected   → aynı sınıf + AYNI assembly'deki türeyenler          (protected ∩ internal)
        //   protected           → aynı sınıf + türeyenler (başka assembly dahil)
        //   internal            → aynı ASSEMBLY'deki herkes
        //   protected internal  → internal ∪ protected                               (protected ∪ internal)
        //   public              → herkes
        //
        // "Başka assembly" satırlarını bu projede derleyemeyiz (tek assembly var); README'deki alıştırma ikinci bir class library ile gösterir.

        Console.WriteLine("── Aynı assembly içinde ──");
        Console.WriteLine($"Aynı sınıf          : {new Vault().ReadAllFromSameClass()}");
        Console.WriteLine($"Türeyen             : {new SameAssemblyDerived().Peek()}");
        Console.WriteLine($"Türemeyen           : {SameAssemblyOther.Peek(new Vault())}");

        Console.WriteLine($"\n{Factory.DescribeDefaults()}");

        Console.WriteLine("\n── Reflection ile gerçek erişim düzeyleri (CLR'ın gördüğü) ──");
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

        // Başka assembly'nin internal tipleri: kendi assembly'miz için internal 'bizimdir'; BCL'in internal tipleri ise bize kapalıdır.
        var bcl = typeof(object).Assembly;
        var all = bcl.GetTypes();
        var internals = all.Where(t => t.IsNotPublic).ToList();
        Console.WriteLine($"\n── Başka assembly'nin internal tipleri ({bcl.GetName().Name}) ──");
        Console.WriteLine($"  toplam {all.Length} tip: {all.Length - internals.Count} public, {internals.Count} internal/private");
        Console.WriteLine($"  ör. internal tipler (kodunuzda 'new' veya adıyla kullanamazsınız, CS0122): {string.Join(", ", internals.OrderBy(t => t.FullName).Take(3).Select(t => t.FullName))}");
        Console.WriteLine("  Reflection bunları bulabilir; derleyici erişime izin vermez.");
    }
}
