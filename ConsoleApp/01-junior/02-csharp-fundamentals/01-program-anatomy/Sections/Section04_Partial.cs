using System.Text.RegularExpressions;

namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section04_Partial
{
    public static void Run()
    {
        // partial class: a single type across multiple files. Invoice.cs (hand-written) + Invoice.Generated.cs (like code generator output).
        var invoice = new Invoice("INV-001", 100m);
        Console.WriteLine(invoice.Describe());

        // partial METHOD: the declaration is in one file, the implementation in another. Source generators use exactly this:
        //   [GeneratedRegex(...)] static partial Regex Digits();   → the compiler generates the method BODY at compile time.
        var m = Generated.Digits().Match("Order no: 4521");
        Console.WriteLine($"\n[GeneratedRegex] partial method → match: {m.Value}");
        Console.WriteLine("You can see this method's generated source in the IDE with F12 (Analyzers → ...RegexGenerator).");
    }
}

public static partial class Generated
{
    [GeneratedRegex(@"\d+")]
    public static partial Regex Digits();
}
