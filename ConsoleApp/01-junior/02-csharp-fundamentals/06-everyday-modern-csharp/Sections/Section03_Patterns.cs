namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section03_Patterns
{
    public static void Run()
    {
        object[] samples = [42, -7, 3.5, "hello", "", null!, new Circle(2), new Rectangle(3, 4), new Order("A-1", new Customer("Alice", new Address("London")), 1500m), new int[] { 1, 2, 3 }];

        foreach (var s in samples)
            Console.WriteLine($"   {Format(s),-34} → {Describe(s)}");

        Console.WriteLine("""

        Kinds of patterns (all work with `is` and `switch`):
          type pattern       : x is string s                    (check + cast + variable)
          constant pattern   : x is 0, x is null, x is "a"
          relational         : x is > 5, x is >= 0 and < 100    (C# 9)
          logical            : and / or / not                   (x is not null, c is 'a' or 'b')
          property pattern   : o is { Total: > 1000 }           (nested: o is { Customer.Address.City: "London" } — C# 10 extended)
          positional/tuple   : (a, b) switch { (0, 0) => … }    (via Deconstruct or a tuple)
          var / discard      : x is var y,  _                   (list pattern: is [1, .., 3]  — C# 11)
        Before: `if (x is string) { var s = (string)x; … }` or as + a null check. Now a single line.
        """);

        int[] arr = [1, 2, 3];
        Console.WriteLine($"list pattern: [1,2,3] is [1, .., 3] → {arr is [1, .., 3]};   [1,2,3] is [_, _] → {arr is [_, _]}");
        string? text = Environment.GetEnvironmentVariable("J2_6_NOT_SET");
        Console.WriteLine($"null check: text is not null → {text is not null}   (instead of `!= null`: the != operator can mislead on types that overload it; `is` doesn't call the overload)");
    }

    private static string Format(object? o) => o switch
    {
        null => "null",
        string { Length: 0 } => "\"\" (empty string)",
        string s => $"\"{s}\"",
        Circle c => $"Circle({c.Radius})",
        Rectangle r => $"Rectangle({r.Width}x{r.Height})",
        Order => "Order(...)",
        int[] a => $"int[{a.Length}]",
        _ => o.ToString()!,
    };

    private static string Describe(object? o) => o switch
    {
        null => "null value",
        int n and > 0 => "positive integer",                                     // type + relational + logical
        int n and < 0 => "negative integer",
        int => "zero",
        double d when double.IsFinite(d) => $"decimal ({d})",
        string { Length: 0 } => "empty text",                                    // property pattern
        string => "non-empty text",
        Circle { Radius: > 1 } => "big circle",
        Circle => "small circle",
        Rectangle { Width: var w, Height: var h } when w == h => "square",
        Rectangle => "rectangle",
        Order { Total: > 1000m, Customer.Address.City: "London" } => "big order going to London",   // nested property pattern
        Order => "order",
        int[] and [var first, .., var last] => $"array: first {first}, last {last}",      // list pattern
        _ => "unknown",
    };

    private sealed record Circle(double Radius);
    private sealed record Rectangle(double Width, double Height);
    private sealed record Address(string City);
    private sealed record Customer(string Name, Address? Address);
    private sealed record Order(string Id, Customer Customer, decimal Total);
}
