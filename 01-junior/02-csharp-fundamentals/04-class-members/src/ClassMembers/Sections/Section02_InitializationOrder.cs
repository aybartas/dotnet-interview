using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section02_InitializationOrder
{
    public static void Run()
    {
        Console.WriteLine("1) 'new DerivedClass()' çağrısında ilk kez çalışan adımlar (static + instance):\n");
        Trace.Reset();
        _ = new DerivedClass();
        foreach (var step in Trace.Steps) Console.WriteLine($"   {step}");

        Console.WriteLine("\n2) İkinci 'new DerivedClass()' — static adımlar TEKRARLANMAZ (tip başına bir kez):\n");
        Trace.Reset();
        _ = new DerivedClass();
        foreach (var step in Trace.Steps) Console.WriteLine($"   {step}");

        Console.WriteLine("\n3) Constructor zinciri this(...)/base(...) — new DerivedClass(\"x\"):\n");
        Trace.Reset();
        _ = new DerivedClass("x");
        foreach (var step in Trace.Steps) Console.WriteLine($"   {step}");

        Console.WriteLine("""

        ÖZET (instance):  alanlar sıfırlanır → field initializer'lar TÜRETİLMİŞTEN TABANA → ctor gövdeleri TABANDAN TÜRETİLMİŞE
        ÖZET (static)  :  her tipin static field initializer + static ctor'u, o tip İLK kullanılmadan önce BİR KEZ çalışır (thread-safe garantili).
                    Dikkat: Base'in static başlatması, Derived'ın instance alanları kurulduktan SONRA, base ctor'a ilk girişte tetiklenir.

        Constructor çeşitleri:
          • default (derleyicinin ürettiği)  — hiç ctor yazmazsanız parametresiz public ctor üretilir; bir tane yazarsanız ÜRETİLMEZ
          • parameterized                    • static (tip başlatma)         • private (singleton / static factory deseni)
          • copy constructor (C# özel sözdizimi yok; aynı tipten parametre alan ctor)
          • this(...) aynı sınıfın başka ctor'una zincir, base(...) taban ctor'una zincir
        """);

        var a = Connection.Create("db");
        var copy = new Connection(a);
        // var c = new Connection("x", 1);   // ✗ CS0122: private ctor
        Console.WriteLine($"factory: {a.Name} (retries {a.Retries});  copy ctor: {copy.Name};  Shared aynı mı? {ReferenceEquals(Connection.Shared, Connection.Shared)}");
    }
}
