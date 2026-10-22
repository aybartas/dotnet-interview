using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.Concepts;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Process vs thread vs task",                                   Section01_ProcessThreadTask.Run),
        new(2, "Concurrency vs parallelism ⭐",                               Section02_ConcurrencyVsParallelism.Run),
        new(3, "CPU-bound vs I/O-bound",                                      Section03_CpuVsIoBound.Run),
        new(4, "Blocking vs non-blocking: why is blocking a thread expensive?", Section04_BlockingVsNonBlocking.Run),
        new(5, "🛠 The same 10 HTTP calls: sequential / WhenAll / Parallel.For", Section05_TenHttpCalls.Run),
        new(6, "🛠 Thread safety: race condition on a shared counter",          Section06_RaceCondition.Run),
    ];
}
