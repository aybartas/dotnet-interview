using System.Runtime.CompilerServices;

namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section13_CallerInfo
{
    public static void Run()
    {
        // Caller attributes: the caller's info is INJECTED into the default parameter value AT COMPILE TIME
        // (no reflection/StackTrace → zero runtime cost, refactor-proof).
        Log("application started");
        DoWork();

        // CallerArgumentExpression: the argument's TEXT in the SOURCE CODE — great for guard clauses
        var items = new List<int>();
        string? name = null;
        try { Guard.NotNull(name); }
        catch (ArgumentNullException ex) { Console.WriteLine($"\nGuard.NotNull(name)             → paramName = \"{ex.ParamName}\""); }
        try { Guard.That(items.Count > 0); }
        catch (ArgumentException ex) { Console.WriteLine($"Guard.That(items.Count > 0)      → {ex.Message}"); }
        // .NET itself uses it too: ArgumentNullException.ThrowIfNull(name) finds paramName this way.
    }

    private static void DoWork() => Log("doing work");     // [CallerMemberName] → "DoWork"

    private static void Log(
        string message,
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
        => Console.WriteLine($"[{member}] {message}   ({Path.GetFileName(file)}:{line})");
}

public static class Guard
{
    public static void NotNull(object? value, [CallerArgumentExpression(nameof(value))] string? expr = null)
    {
        if (value is null) throw new ArgumentNullException(expr);
    }

    public static void That(bool condition, [CallerArgumentExpression(nameof(condition))] string? expr = null)
    {
        if (!condition) throw new ArgumentException($"Condition not met: {expr}");
    }
}
