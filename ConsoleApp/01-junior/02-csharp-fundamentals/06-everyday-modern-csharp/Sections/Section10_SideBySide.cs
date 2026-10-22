using Junior.CSharpFundamentals.ModernCSharp.Comparison;

namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section10_SideBySide
{
    public static void Run()
    {
        IOrderFacade[] styles = [new Facade7(), new Facade14()];
        IReadOnlyList<(string Product, int Qty, decimal Price)> lines = [("Pen", 2, 12.5m), ("Notebook", 1, 40m), ("Bag", 1, 980m)];

        Console.WriteLine("1) Is the BEHAVIOUR the same? (two styles, same inputs)\n");
        foreach (var s in styles)
        {
            Console.WriteLine($"── {s.StyleName} ──");
            Console.WriteLine(s.Receipt("Gold", "Alice Smith", "  ALICE@Example.com ", "London", lines));
            Console.WriteLine($"   discount rates: Platinum/1500 = {s.DiscountRate("Platinum", 1500m):P0}, Gold/500 = {s.DiscountRate("Gold", 500m):P0}, Standard/1500 = {s.DiscountRate("Standard", 1500m):P0}");
            Console.WriteLine($"   status transitions: Pending+pay = {s.Advance("Pending", "pay")}, Paid+cancel = {s.Advance("Paid", "cancel")}");
            try { s.Advance("Delivered", "pay"); }
            catch (InvalidOperationException ex) { Console.WriteLine($"   invalid transition: {ex.Message}"); }
            Console.WriteLine($"   ShippingLabel(null) = {s.ShippingLabel(null)} · NormalizeEmail = {s.NormalizeEmail("  A@B.COM ")} · are two identical orders equal = {s.IdenticalOrdersAreEqual()}");
            Console.WriteLine($"   RenameIfPresent(true) = {s.RenameIfPresent(true, "new")} · (false) = {s.RenameIfPresent(false, "new")}\n");
        }

        var same = styles[0].Receipt("Gold", "Alice Smith", "  ALICE@Example.com ", "London", lines)
                == styles[1].Receipt("Gold", "Alice Smith", "  ALICE@Example.com ", "London", lines);
        Console.WriteLine($"Are the receipt texts IDENTICAL? {same}\n");

        Console.WriteLine("2) LINE COUNT (excluding blank and comment-only lines) — domain model files");
        var loc7 = LineCounter.Count(Path.Combine(LineCounter.StyleDirectory("Csharp7Style"), "Domain.cs"));
        var loc14 = LineCounter.Count(Path.Combine(LineCounter.StyleDirectory("Csharp14Style"), "Domain.cs"));
        Console.WriteLine($"   C# 7 style  : {loc7,4} lines");
        Console.WriteLine($"   C# 14 style : {loc14,4} lines   → {100 - 100.0 * loc14 / loc7:F0}% fewer");

        Console.WriteLine("""

        Why did it shrink?
          Address / OrderLine      : hand-written Equals/GetHashCode/ToString/WithXxx  →  a one-line `record`
          Customer.Email           : field + property + validation                    →  `field` keyword
          Order (state machine)    : switch statement + break + variable               →  switch expression + `with`
          DiscountRate             : nested if/switch + temporary variable             →  tuple pattern + relational pattern
          Receipt                  : StringBuilder + string.Format                     →  interpolated raw string literal
          Namespace + using        : block indentation + 6 usings                      →  file-scoped namespace + implicit/global using
          Building collections     : new List<>/ReadOnlyCollection                     →  collection expression `[.. xs]`
        READABILITY: the new style says "WHAT should happen" (declarative); the old style spells out "HOW to do it" step by step.
        But short ≠ always good: a chain of advanced patterns/primary constructors can look foreign to an inexperienced reader; team standards and readability come first.
        """);
    }
}
