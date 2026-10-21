namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section03_Patterns
{
    public static void Run()
    {
        object[] samples = [42, -7, 3.5, "merhaba", "", null!, new Circle(2), new Rectangle(3, 4), new Order("A-1", new Customer("Ayşe", new Address("Ankara")), 1500m), new int[] { 1, 2, 3 }];

        foreach (var s in samples)
            Console.WriteLine($"   {Format(s),-34} → {Describe(s)}");

        Console.WriteLine("""

        Pattern çeşitleri (hepsi `is` ve `switch` ile çalışır):
          type pattern       : x is string s                    (kontrol + cast + değişken)
          constant pattern   : x is 0, x is null, x is "a"
          relational         : x is > 5, x is >= 0 and < 100    (C# 9)
          logical            : and / or / not                   (x is not null, c is 'a' or 'b')
          property pattern   : o is { Total: > 1000 }           (iç içe: o is { Customer.Address.City: "Ankara" } — C# 10 extended)
          positional/tuple   : (a, b) switch { (0, 0) => … }    (Deconstruct veya tuple üzerinden)
          var / discard      : x is var y,  _                   (list pattern: is [1, .., 3]  — C# 11)
        Eskiden: `if (x is string) { var s = (string)x; … }` ya da as + null kontrolü. Şimdi tek satır.
        """);

        int[] arr = [1, 2, 3];
        Console.WriteLine($"list pattern: [1,2,3] is [1, .., 3] → {arr is [1, .., 3]};   [1,2,3] is [_, _] → {arr is [_, _]}");
        string? text = Environment.GetEnvironmentVariable("J2_6_NOT_SET");
        Console.WriteLine($"null kontrolü: text is not null → {text is not null}   (`!= null` yerine: != operatörü overload edilmiş tiplerde yanıltabilir; `is` overload'ı çağırmaz)");
    }

    private static string Format(object? o) => o switch
    {
        null => "null",
        string { Length: 0 } => "\"\" (boş string)",
        string s => $"\"{s}\"",
        Circle c => $"Circle({c.Radius})",
        Rectangle r => $"Rectangle({r.Width}x{r.Height})",
        Order => "Order(...)",
        int[] a => $"int[{a.Length}]",
        _ => o.ToString()!,
    };

    private static string Describe(object? o) => o switch
    {
        null => "null değer",
        int n and > 0 => "pozitif tamsayı",                                      // type + relational + logical
        int n and < 0 => "negatif tamsayı",
        int => "sıfır",
        double d when double.IsFinite(d) => $"ondalık ({d})",
        string { Length: 0 } => "boş metin",                                     // property pattern
        string => "dolu metin",
        Circle { Radius: > 1 } => "büyük daire",
        Circle => "küçük daire",
        Rectangle { Width: var w, Height: var h } when w == h => "kare",
        Rectangle => "dikdörtgen",
        Order { Total: > 1000m, Customer.Address.City: "Ankara" } => "Ankara'ya giden büyük sipariş",   // iç içe property pattern
        Order => "sipariş",
        int[] and [var first, .., var last] => $"dizi: ilk {first}, son {last}",      // list pattern
        _ => "bilinmiyor",
    };

    private sealed record Circle(double Radius);
    private sealed record Rectangle(double Width, double Height);
    private sealed record Address(string City);
    private sealed record Customer(string Name, Address? Address);
    private sealed record Order(string Id, Customer Customer, decimal Total);
}
