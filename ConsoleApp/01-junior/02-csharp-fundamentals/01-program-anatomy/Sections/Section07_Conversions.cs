using System.Globalization;

namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section07_Conversions
{
    public static void Run()
    {
        // 1) IMPLICIT: no data loss → automatic
        int i = 123;
        long l = i;                                       // int → long
        double d = i;                                     // int → double
        Console.WriteLine($"implicit: int {i} → long {l} → double {d}");

        // 2) EXPLICIT (cast): data may be lost → you MUST write it, the responsibility is yours
        double pi = 3.99;
        int truncated = (int)pi;                          // does not round, it TRUNCATES
        Console.WriteLine($"explicit cast: (int)3.99 = {truncated}   (truncation, not rounding)");
        long big = 3_000_000_000;
        Console.WriteLine($"explicit cast: (int)3_000_000_000 = {unchecked((int)big)}   (overflow happens silently → Section08)");

        // 3) is / as: REFERENCE conversions
        object o = "hello";
        Console.WriteLine($"is: o is string → {o is string}");
        var asString = o as string;                       // returns null if it fails (does NOT throw)
        var asInt = o as int?;                            // with nullable for value types
        Console.WriteLine($"as: o as string → \"{asString}\", o as int? → {(asInt is null ? "null" : asInt)}");
        try { _ = (int)o; }                               // cast: InvalidCastException if it fails
        catch (InvalidCastException ex) { Console.WriteLine($"(int)o → {ex.GetType().Name}"); }
        if (o is string s && s.Length > 3)                // pattern matching: check + cast + variable in one step
            Console.WriteLine($"pattern: o is string s → s.Length = {s.Length}");

        // 4) Convert: tolerant of null/format, ROUNDS (banker's rounding: .5 goes to the nearest EVEN number)
        Console.WriteLine($"\nConvert.ToInt32(2.5) = {Convert.ToInt32(2.5)}, (3.5) = {Convert.ToInt32(3.5)}   (banker's rounding!)  ·  Math.Round(2.5, MidpointRounding.AwayFromZero) = {Math.Round(2.5, MidpointRounding.AwayFromZero)}");
        Console.WriteLine($"Convert.ToInt32(null as string) = {Convert.ToInt32(null as string)}   (null → 0, no exception)");
        // int.Parse(null) → ArgumentNullException

        // 5) Parse vs TryParse ⭐
        try { _ = int.Parse("12a"); }
        catch (FormatException ex) { Console.WriteLine($"int.Parse(\"12a\") → {ex.GetType().Name}  (failure = exception: expensive and the wrong tool for control flow)"); }

        var ok = int.TryParse("12a", out var value);      // NO exception; returns bool, value = 0
        Console.WriteLine($"int.TryParse(\"12a\") → {ok}, value = {value}   (the right tool for user input)");

        // Culture trap: the same text is a different number in a different culture
        var tr = new CultureInfo("tr-TR");
        Console.WriteLine($"decimal.Parse(\"1.234,5\", tr-TR)  = {decimal.Parse("1.234,5", tr)}");
        Console.WriteLine($"decimal.Parse(\"1.234,5\", invariant) → {(decimal.TryParse("1.234,5", CultureInfo.InvariantCulture, out var inv) ? inv : "FAILED")}");
        Console.WriteLine("Rule: machine-to-machine data (JSON, files, protocols) → InvariantCulture; display/input for the user → the user's culture.");
    }
}
