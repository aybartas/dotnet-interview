namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section05_StatementsExpressions
{
    public static void Run()
    {
        // EXPRESSION: produces a VALUE. STATEMENT: does a job, (usually) produces no value; ends with ';' or is a block.
        var x = 5;                                       // statement (declaration)  — the '5' on the right is an expression
        var sign = x > 0 ? "positive" : "negative";      // ?: is an expression → can be assigned to a variable
        // var y = if (x > 0) { ... }                    // ✗ 'if' is a STATEMENT; it produces no value
        var size = x switch                              // switch EXPRESSION (C# 8): the value-producing form of an if/else chain
        {
            < 3 => "small",
            < 10 => "medium",
            _ => "large",
        };
        Console.WriteLine($"{x} → {sign}, {size}");

        // Assignment is an expression too (its value is the assigned value): chained assignment
        int a, b;
        a = b = 7;
        Console.WriteLine($"a = b = 7 → a={a}, b={b}");

        // Expression-bodied member: write a member whose body is a SINGLE expression with '=>'
        var c = new Circle(3);
        Console.WriteLine($"{c}  area={c.Area:F2}  perimeter={c.Perimeter():F2}  isLarge={c.IsLarge}");

        // A lambda can be expression-bodied too
        Func<int, int> square = n => n * n;
        Console.WriteLine($"square(9) = {square(9)}");

        // throw expression (C# 7): null check + throw in a single expression with ??
        try { _ = Require(null); }
        catch (ArgumentNullException ex) { Console.WriteLine($"throw expression → {ex.ParamName} null"); }
    }

    private static string Require(string? value) => value ?? throw new ArgumentNullException(nameof(value));

    private sealed class Circle(double radius)
    {
        public double Area => Math.PI * radius * radius;                 // expression-bodied property (get-only)
        public double Perimeter() => 2 * Math.PI * radius;               // expression-bodied method
        public bool IsLarge => radius > 1;
        public override string ToString() => $"Circle(r={radius})";
    }
}
