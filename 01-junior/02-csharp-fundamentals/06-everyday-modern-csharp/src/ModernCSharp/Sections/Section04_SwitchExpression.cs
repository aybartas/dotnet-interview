namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section04_SwitchExpression
{
    public static void Run()
    {
        foreach (var day in new[] { DayOfWeek.Monday, DayOfWeek.Saturday })
            Console.WriteLine($"   {day,-9} → eski switch: {OldStyle(day),-8} yeni switch expression: {Describe(day)}");

        Console.WriteLine($"\n   Puan 95 → {Grade(95)}, 72 → {Grade(72)}, 10 → {Grade(10)}   (relational pattern aralıkları)");
        Console.WriteLine($"   (IsWeekend, Hour) = (true, 11)  → {Greeting(true, 11)}");
        Console.WriteLine($"   (IsWeekend, Hour) = (false, 8)  → {Greeting(false, 8)}");

        try { _ = Parse("x"); }
        catch (ArgumentOutOfRangeException ex) { Console.WriteLine($"   eşleşme yok → throw expression: {ex.GetType().Name}"); }

        Console.WriteLine("""

        switch STATEMENT (eski): her dal `break;`, değişkeni önceden tanımla, değer atamayı unutabilirsin, hepsi ifade değil deyim
        switch EXPRESSION     : bir DEĞER üretir → doğrudan atanır/döndürülür; `=>`; `_` discard (varsayılan); `when` guard;
                                dallar ifade olduğundan "unutulan atama" yok; derleyici **kapsamlılık (exhaustiveness)** uyarısı verir (CS8509):
                                enum'un bir değeri atlanırsa uyarı. Eşleşme yoksa çalışma zamanında SwitchExpressionException fırlar.
        Dal sırası ÖNEMLİDİR: önce özel, sonra genel (öncekini gölgeleyen dal derleme hatası: CS8510).
        """);
    }

    private static string OldStyle(DayOfWeek day)
    {
        string result;
        switch (day)
        {
            case DayOfWeek.Saturday:
            case DayOfWeek.Sunday:
                result = "hafta sonu";
                break;
            default:
                result = "hafta içi";
                break;
        }
        return result;
    }

    private static string Describe(DayOfWeek day) => day switch
    {
        DayOfWeek.Saturday or DayOfWeek.Sunday => "hafta sonu",
        _ => "hafta içi",
    };

    private static string Grade(int score) => score switch
    {
        >= 90 => "A",
        >= 80 => "B",
        >= 70 => "C",
        >= 0 => "F",
        _ => throw new ArgumentOutOfRangeException(nameof(score)),
    };

    private static string Greeting(bool isWeekend, int hour) => (isWeekend, hour) switch
    {
        (true, < 12) => "Günaydın, iyi hafta sonları!",
        (true, _) => "İyi hafta sonları!",
        (false, < 9) => "Erken kalkan yol alır",
        (false, _) => "İyi çalışmalar",
    };

    private static int Parse(string s) => s switch
    {
        "bir" => 1,
        "iki" => 2,
        _ => throw new ArgumentOutOfRangeException(nameof(s), s, "bilinmeyen sayı"),
    };
}
