using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section06_Strings
{
    public static void Run()
    {
        Console.WriteLine("1) string is IMMUTABLE: every 'modification' produces a NEW string.");
        var s = "hello";
        var upper = s.ToUpperInvariant();
        Console.WriteLine($"   s = \"{s}\", s.ToUpperInvariant() = \"{upper}\" → s unchanged; ReferenceEquals(s, upper) = {ReferenceEquals(s, upper)}");
        Console.WriteLine("   Why immutable? Thread-safe sharing, safe Dictionary key (hash never changes), security (a path/connection string can't change afterwards), interning becomes possible.");

        Console.WriteLine("\n2) Concatenation in a loop: string += is O(n²), StringBuilder is O(n)");
        const int n = 20_000;
        var sw = Stopwatch.StartNew();
        var bytesBefore = GC.GetAllocatedBytesForCurrentThread();
        var text = "";
        for (var i = 0; i < n; i++) text += "x";
        var concatBytes = GC.GetAllocatedBytesForCurrentThread() - bytesBefore;
        Console.WriteLine($"   {n:N0} times with +=     : {sw.ElapsedMilliseconds,4} ms, {concatBytes / 1024.0 / 1024.0,8:F1} MB allocated");

        sw.Restart();
        bytesBefore = GC.GetAllocatedBytesForCurrentThread();
        var sb = new StringBuilder();
        for (var i = 0; i < n; i++) sb.Append('x');
        var built = sb.ToString();
        Console.WriteLine($"   with StringBuilder       : {sw.ElapsedMilliseconds,4} ms, {(GC.GetAllocatedBytesForCurrentThread() - bytesBefore) / 1024.0 / 1024.0,8:F1} MB allocated   (same result: {built == text})");
        Console.WriteLine("   (For a one-off concatenation 'a + b + c' StringBuilder is NOT needed: the compiler turns it into string.Concat.)");

        Console.WriteLine("\n3) == on string compares VALUES (operator overload), not references:");
        var a = new string(['a', 'b', 'c']);
        var b = new string(['a', 'b', 'c']);
        Console.WriteLine($"   a == b: {a == b}   ReferenceEquals(a, b): {ReferenceEquals(a, b)}   (different objects, same content)");
        object oa = a, ob = b;
        Console.WriteLine($"   (object)a == (object)b: {oa == ob}   ← == on type object compares REFERENCES (operator overload depends on the compile-time type)");

        Console.WriteLine("\n4) String interning: identical literals share a single object");
        var l1 = "interned"; var l2 = "interned";
        Console.WriteLine($"   two identical literals: ReferenceEquals = {ReferenceEquals(l1, l2)}");
        var runtime = new string("interned".ToCharArray());
        Console.WriteLine($"   created at run time: ReferenceEquals(l1, runtime) = {ReferenceEquals(l1, runtime)};  with string.Intern(runtime): {ReferenceEquals(l1, string.Intern(runtime))}");

        Console.WriteLine("\n5) THE TURKISH I PROBLEM ⭐ — culture-sensitive upper/lower-case conversion");
        var tr = new CultureInfo("tr-TR");
        var inv = CultureInfo.InvariantCulture;
        Console.WriteLine($"   \"title\".ToUpper(tr-TR)   = {"title".ToUpper(tr)}   ← i → İ (dotted capital I)");
        Console.WriteLine($"   \"title\".ToUpper(invariant) = {"title".ToUpper(inv)}");
        Console.WriteLine($"   \"FILE\".ToLower(tr-TR)    = {"FILE".ToLower(tr)}    ← I → ı (dotless lowercase ı) → NOT EQUAL to \"file\"!");

        var old = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = tr;
        try
        {
            var command = "FILE";
            Console.WriteLine($"\n   When the user culture is tr-TR:");
            Console.WriteLine($"   command.ToLower() == \"file\"                          → {command.ToLower() == "file"}   ✗ BUGGY (in production it breaks only on Turkish devices!)");
            Console.WriteLine($"   command.Equals(\"file\", StringComparison.OrdinalIgnoreCase) → {command.Equals("file", StringComparison.OrdinalIgnoreCase)}   ✓ CORRECT");
            Console.WriteLine($"   string.Compare(\"i\", \"I\", ignoreCase: true) (culture-sensitive) = {string.Compare("i", "I", ignoreCase: true)}   (in tr-TR i ≠ I)");
            Console.WriteLine($"   string.Equals(\"i\", \"I\", OrdinalIgnoreCase)            = {string.Equals("i", "I", StringComparison.OrdinalIgnoreCase)}");
        }
        finally { CultureInfo.CurrentCulture = old; }

        Console.WriteLine("""

           RULE:
           • Language-independent identifiers (command name, file path, URL, HTTP header, config key, enum name) → StringComparison.Ordinal / OrdinalIgnoreCase,
             case conversion with ToUpperInvariant()/ToLowerInvariant().
           • Text shown to / sorted for the user → culture-sensitive (CurrentCulture).
           • Analyzers: CA1304/CA1305/CA1307/CA1309/CA1310 catch culture/comparison pitfalls.
        """);
    }
}
