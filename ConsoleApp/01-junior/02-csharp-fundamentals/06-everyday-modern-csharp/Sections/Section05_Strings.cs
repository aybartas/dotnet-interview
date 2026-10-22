using System.Globalization;

namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section05_Strings
{
    public static void Run()
    {
        var name = "Alice";
        var price = 1234.5m;
        var date = new DateTime(2026, 3, 9);
        var inv = CultureInfo.InvariantCulture;

        Console.WriteLine("1) Interpolation: expression + format + alignment");
        Console.WriteLine($"   name={name,-8}| price={price,10:N2}| date={date:yyyy-MM-dd}| hex={255:X4}| percent={0.256:P1}");
        Console.WriteLine($"   inside an expression: {(price > 1000 ? "expensive" : "cheap")}  ·  {name.ToUpperInvariant()}  ·  Invariant: {FormattableString.Invariant($"{price:F2}")}");
        Console.WriteLine($"   old: string.Format(\"{{0}} - {{1:N2}}\", ...) → {string.Format(inv, "{0} - {1:N2}", name, price)}");

        Console.WriteLine("\n2) Raw string literal (C# 11): with \"\"\" — NO escape characters needed; ideal for JSON/SQL/regex/HTML");
        var json = """
            {
              "name": "Alice",
              "path": "C:\temp\new",
              "quote": "she said \"hello\""
            }
            """;
        Console.WriteLine(json);
        Console.WriteLine("   (the indentation of the closing \"\"\" determines the whitespace that is STRIPPED → the content keeps its indentation as you wrote it)");

        Console.WriteLine("\n3) Interpolated raw string: $$\"\"\" — expressions go in {{ }}, a single { } stays part of the JSON");
        var body = $$"""
            {
              "user": "{{name}}",
              "total": {{price.ToString("F2", inv)}}
            }
            """;
        Console.WriteLine(body);

        Console.WriteLine("\n4) Compared with the old style (the same JSON):");
        var legacy = "{\n  \"user\": \"" + name + "\",\n  \"total\": " + price.ToString("F2", inv) + "\n}";
        Console.WriteLine($"   the same? {legacy == body.ReplaceLineEndings("\n")}   (raw string: no escaping, readable, no wrong-escape bugs)");

        Console.WriteLine("""

        Verbatim (@"…"): lets you write only backslashes and line breaks unescaped; quotes still need "". Raw strings replace it.
        Security: don't embed user input into SQL/command text via interpolation → use parameters (→ J8, J11.4). Interpolation is only formatting.
        Culture: {price:N2} uses CurrentCulture (for machine-to-machine text use FormattableString.Invariant / ToString(inv)).
        The modern compiler builds interpolated strings via a 'handler' without boxing/cheaply (an extra optimization in loggers: LoggerMessage → J6.5).
        """);
    }
}
