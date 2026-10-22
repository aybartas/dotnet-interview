using System.Runtime.CompilerServices;

namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section13_CallerInfo
{
    public static void Run()
    {
        // Caller attribute'ları: DERLEME ZAMANINDA çağıran yerin bilgisi varsayılan parametre değerine ENJEKTE EDİLİR
        // (reflection/StackTrace yok → sıfır çalışma zamanı maliyeti, refactor'a dayanıklı).
        Log("uygulama başladı");
        DoWork();

        // CallerArgumentExpression: argümanın KAYNAK KODDAKİ METNİ — guard clause'larda şahane
        var items = new List<int>();
        string? name = null;
        try { Guard.NotNull(name); }
        catch (ArgumentNullException ex) { Console.WriteLine($"\nGuard.NotNull(name)             → paramName = \"{ex.ParamName}\""); }
        try { Guard.That(items.Count > 0); }
        catch (ArgumentException ex) { Console.WriteLine($"Guard.That(items.Count > 0)      → {ex.Message}"); }
        // .NET'in kendisi de kullanır: ArgumentNullException.ThrowIfNull(name) paramName'i böyle bulur.
    }

    private static void DoWork() => Log("iş yapılıyor");     // [CallerMemberName] → "DoWork"

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
        if (!condition) throw new ArgumentException($"Koşul sağlanmadı: {expr}");
    }
}
