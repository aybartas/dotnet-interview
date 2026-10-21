using System.Reflection;
using Junior.CSharpFundamentals.ProgramAnatomy.Library;

namespace Junior.CSharpFundamentals.ProgramAnatomy.Consumer;

/// <summary>Erişim belirleyicileri deneyi: Vault başka bir assembly'de (J02.Lib), bu sınıf 'dışarıdaki' koddur.</summary>
public static class AccessModifiersDemo
{
    public static void Run()
    {
        // Erişim belirleyicileri — ASSEMBLY SINIRI deneyi.
        //
        //   dotnet run -- 2.1 access-modifiers           → çalışır (hepsi geçerli kod)
        //   dotnet build -p:DefineConstants=SHOW_ERRORS   → DERLEME HATALARINI görün (CS0122, CS1540 ...)  — modül klasöründe
        //
        // Kural özeti (⭐ mülakat):
        //   private             → sadece aynı SINIF
        //   private protected   → aynı sınıf + AYNI assembly'deki türeyenler          (protected ∩ internal)
        //   protected           → aynı sınıf + türeyenler (başka assembly dahil)
        //   internal            → aynı ASSEMBLY'deki herkes
        //   protected internal  → internal ∪ protected                               (protected ∪ internal)
        //   public              → herkes

        Console.WriteLine("── Library içinde (aynı assembly) ──");
        Console.WriteLine($"Aynı sınıf          : {new Vault().ReadAllFromSameClass()}");
        Console.WriteLine($"Türeyen (aynı asm.) : {new SameAssemblyDerived().Peek()}");
        Console.WriteLine($"Türemeyen (aynı asm): {SameAssemblyOther.Peek(new Vault())}");

        Console.WriteLine("\n── Consumer içinde (BAŞKA assembly) ──");
        Console.WriteLine($"Türeyen (başka asm.): {new ConsumerDerived().Peek()}");
        Console.WriteLine($"Türemeyen (başka asm): {ConsumerOther.Peek(new Vault())}");

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

        Console.WriteLine("\ninternal bir tipe dışarıdan erişim denemesi (Library.PriceRules):");
        var hidden = typeof(Vault).Assembly.GetType("Junior.CSharpFundamentals.ProgramAnatomy.Library.PriceRules");
        Console.WriteLine($"  reflection ile bulunabilir ama derleyici izin vermez: tip bulundu = {hidden is not null}, IsPublic = {hidden?.IsPublic}");
    }
}
