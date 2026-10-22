using System.Diagnostics;

namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section05_Cost
{
    public static void Run()
    {
        const int n = 20_000;
        var inputs = Enumerable.Range(0, n).Select(i => i % 2 == 0 ? "123" : "abc").ToArray();       // half of them are invalid
        // warm-up
        _ = ParseWithTry("x"); _ = ParseWithTryParse("x");

        var sw = Stopwatch.StartNew();
        var okA = inputs.Count(s => ParseWithTryParse(s) is not null);
        var tryParseMs = sw.Elapsed.TotalMilliseconds;

        sw.Restart();
        var okB = inputs.Count(s => ParseWithTry(s) is not null);
        var exceptionMs = sw.Elapsed.TotalMilliseconds;

        Console.WriteLine($"{n:N0} inputs, half of them invalid:");
        Console.WriteLine($"   int.TryParse           : {tryParseMs,8:F2} ms   (succeeded: {okA:N0})");
        Console.WriteLine($"   int.Parse + try/catch  : {exceptionMs,8:F2} ms   (succeeded: {okB:N0})   → roughly {exceptionMs / Math.Max(tryParseMs, 0.001):N0}× SLOWER");
        Console.WriteLine($"   (each throw: stack trace capture + stack unwinding + running (any) filters/finally blocks → thousands of CPU cycles;");
        Console.WriteLine($"    on the path without an exception the try block is almost free — the cost shows up when it is THROWN)");
        Console.WriteLine("   Rough measurement (Stopwatch); for precise results use BenchmarkDotNet → S2.1");

        Console.WriteLine("""

        RULE: Exceptions are for "EXCEPTIONAL" situations, not for "EXPECTED" control flow.
          ✗ Validating user input with Parse + catch FormatException                → ✓ TryParse
          ✗ Checking a key with dictionary[key] + catch KeyNotFoundException       → ✓ TryGetValue
          ✗ Checking a file with catch FileNotFoundException (except race conditions) → ✓ File.Exists / TryOpen or return a Result
          ✗ Ending a loop with an exception                                         → ✓ a normal condition
        Reasons: (1) performance (the measurement above), (2) readability — the "normal" path and the "error" path get mixed up, (3) first-chance exception noise in the debugger,
        (4) monitoring/alerting systems count exceptions; normal flow produces false alarms.
        Rule: a failure the caller EXPECTS (parse error, no record) → Try pattern / Result / null; a violation the caller does NOT EXPECT (bug, setup error, outage) → exception.
        """);
    }

    private static int? ParseWithTryParse(string s) => int.TryParse(s, out var v) ? v : null;

    private static int? ParseWithTry(string s)
    {
        try { return int.Parse(s); }
        catch (FormatException) { return null; }
    }
}
