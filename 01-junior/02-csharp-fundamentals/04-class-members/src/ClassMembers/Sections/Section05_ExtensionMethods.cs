using Junior.CSharpFundamentals.ClassMembers.Extensions;      // ← extension'ların görünmesi için GEREKLİ using

namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section05_ExtensionMethods
{
    public static void Run()
    {
        var text = "merhaba dünya";
        Console.WriteLine($"text.Shout()          → {text.Shout()}          (derleyici: StringExtensions.Shout(text))");
        Console.WriteLine($"text.Truncate(7)      → {text.Truncate(7)}");

        string? nothing = null;
        Console.WriteLine($"nothing.IsBlank()     → {nothing.IsBlank()}     (null receiver: NullReferenceException YOK — çünkü gerçekte statik çağrı)");

        string?[] mixed = ["a", null, "b"];
        Console.WriteLine($"WhereNotNull          → [{string.Join(", ", mixed.WhereNotNull())}]");

        var w = new Widget();
        Console.WriteLine($"\nw.Describe()          → {w.Describe()}         ← instance metot kazanır; aynı imzalı extension HİÇ çağrılmaz");
        Console.WriteLine($"w.Describe(\"önek\")    → {w.Describe("önek")}  ← farklı imzalı extension çağrılabilir");
        Console.WriteLine($"WidgetExtensions.Describe(w) → {WidgetExtensions.Describe(w)}  (statik çağrı şekliyle extension'a ulaşılabilir)");

        Console.WriteLine($"\nLINQ'in tamamı extension method'dur: Enumerable.Range(1,5).Where(...) → System.Linq.Enumerable.Where(source, predicate)");
        Console.WriteLine($"   typeof(Enumerable).IsAbstract && IsSealed (static class) = {typeof(Enumerable).IsAbstract && typeof(Enumerable).IsSealed}");

        Console.WriteLine("""

        Çözümleme (resolution) sırası: (1) örnek metot → (2) çağıran namespace/using'lerdeki extension'lar → bulunamazsa derleme hatası.
        Ne zaman extension?  Sahibi olmadığınız tipe (string, IEnumerable<T>, framework) yardımcı davranış eklemek; akıcı (fluent) API; interface'e davranış eklemek (taban sınıf değiştirmeden).
        Ne zaman inheritance / wrapper / kompozisyon?  Durum (state) eklemeniz gerekiyorsa, davranışı override etmek istiyorsanız (extension override edemez), polimorfizm gerekiyorsa.
        Tuzaklar: aşırı kullanım (her yere extension, keşfedilebilirlik düşer), namespace kirliliği (System.* namespace'ine extension koymayın), null davranışı.
        (C# 14 extension members → M1.1)
        """);
    }
}
