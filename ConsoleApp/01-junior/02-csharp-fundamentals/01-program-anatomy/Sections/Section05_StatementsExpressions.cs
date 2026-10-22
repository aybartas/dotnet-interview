namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section05_StatementsExpressions
{
    public static void Run()
    {
        // EXPRESSION: bir DEĞER üretir. STATEMENT: bir iş yapar, (genelde) değer üretmez; ';' ile biter veya blok'tur.
        var x = 5;                                       // statement (declaration)  — sağdaki '5' expression
        var sign = x > 0 ? "pozitif" : "negatif";        // ?: bir expression → bir değişkene atanabilir
        // var y = if (x > 0) { ... }                    // ✗ 'if' bir STATEMENT; değer üretmez
        var size = x switch                              // switch EXPRESSION (C# 8): if/else zincirinin değer üreten hali
        {
            < 3 => "küçük",
            < 10 => "orta",
            _ => "büyük",
        };
        Console.WriteLine($"{x} → {sign}, {size}");

        // Atama da expression'dır (değeri atanan değer): zincirleme atama
        int a, b;
        a = b = 7;
        Console.WriteLine($"a = b = 7 → a={a}, b={b}");

        // Expression-bodied member: gövdesi TEK expression olan üyeyi '=>' ile yaz
        var c = new Circle(3);
        Console.WriteLine($"{c}  alan={c.Area:F2}  çevre={c.Perimeter():F2}  büyük mü={c.IsLarge}");

        // Lambda de expression-bodied olabilir
        Func<int, int> square = n => n * n;
        Console.WriteLine($"square(9) = {square(9)}");

        // throw expression (C# 7): ?? ile null kontrolü + fırlatma tek ifadede
        try { _ = Require(null); }
        catch (ArgumentNullException ex) { Console.WriteLine($"throw expression → {ex.ParamName} null"); }
    }

    private static string Require(string? value) => value ?? throw new ArgumentNullException(nameof(value));

    private sealed class Circle(double radius)
    {
        public double Area => Math.PI * radius * radius;                 // expression-bodied property (get-only)
        public double Perimeter() => 2 * Math.PI * radius;               // expression-bodied metot
        public bool IsLarge => radius > 1;
        public override string ToString() => $"Circle(r={radius})";
    }
}
