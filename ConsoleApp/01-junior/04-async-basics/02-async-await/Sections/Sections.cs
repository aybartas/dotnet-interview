using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.AsyncAwait;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Task, Task<T>, the async signature and await",                    Section01_TaskBasics.Run),
        new(2, "🛠 Converting a synchronous app to async step by step (async all the way)", Section02_Conversion.Run),
        new(3, "Why is async void bad? ⭐ (crashing child process)",              Section03_AsyncVoid.Run),
        new(4, "The .Result / .Wait() deadlock ⭐",                               Section04_ResultDeadlock.Run),
        new(5, "Task.WhenAll / WhenAny / WhenEach and exception handling",       Section05_WhenAllAny.Run),
        new(6, "Thread.Sleep vs await Task.Delay",                                  Section06_SleepVsDelay.Run),
        new(7, "IAsyncEnumerable<T>, await foreach, await using",                   Section07_AsyncStreams.Run),
    ];
}
