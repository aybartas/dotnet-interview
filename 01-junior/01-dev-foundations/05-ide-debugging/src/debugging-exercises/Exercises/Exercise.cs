namespace Junior.DevFoundations.IdeDebugging.Exercises;

/// <summary>Tek bir debugger alıştırması: ne öğretir, nasıl çalışır, ipucu.</summary>
public abstract class Exercise
{
    public abstract int Number { get; }
    public abstract string Title { get; }
    /// <summary>Bu egzersizde pratik edilen debugger becerisi.</summary>
    public abstract string Skill { get; }
    public abstract string Hint { get; }
    public abstract void Run();

    /// <summary>Beklenen/gerçek karşılaştırmasını standart biçimde yazar.</summary>
    protected static void Report(string what, object? expected, object? actual)
    {
        var ok = Equals(expected, actual);
        Console.WriteLine($"  {(ok ? "✓" : "✗")} {what}: beklenen = {expected}, gerçek = {actual}");
    }
}

public static class Exercises
{
    public static IReadOnlyList<Exercise> All { get; } =
    [
        new Exercise01_OffByOne(),
        new Exercise02_NullReference(),
        new Exercise03_CollectionModified(),
        new Exercise04_Closure(),
        new Exercise05_NumericTraps(),
        new Exercise06_NeedleInHaystack(),
        new Exercise07_ForgottenAwait(),
        new Exercise08_SharedReference(),
        new Exercise09_DebugVsRelease(),
    ];
}
