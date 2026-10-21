using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.AsyncAwait;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Task, Task<T>, async imzası ve await",                              Section01_TaskBasics.Run),
        new(2, "🛠 Senkron uygulamayı adım adım async'e dönüştürme (async all the way)", Section02_Conversion.Run),
        new(3, "async void neden kötü? ⭐ (çöken alt process)",                      Section03_AsyncVoid.Run),
        new(4, ".Result / .Wait() deadlock'u ⭐",                                    Section04_ResultDeadlock.Run),
        new(5, "Task.WhenAll / WhenAny / WhenEach ve exception yakalama",           Section05_WhenAllAny.Run),
        new(6, "Thread.Sleep vs await Task.Delay",                                  Section06_SleepVsDelay.Run),
        new(7, "IAsyncEnumerable<T>, await foreach, await using",                   Section07_AsyncStreams.Run),
    ];
}
