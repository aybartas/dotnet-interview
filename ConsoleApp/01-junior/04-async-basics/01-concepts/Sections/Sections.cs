using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.Concepts;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Process vs thread vs task",                                   Section01_ProcessThreadTask.Run),
        new(2, "Concurrency vs parallelism ⭐",                               Section02_ConcurrencyVsParallelism.Run),
        new(3, "CPU-bound vs I/O-bound",                                      Section03_CpuVsIoBound.Run),
        new(4, "Blocking vs non-blocking: thread bloklamak neden pahalı?",    Section04_BlockingVsNonBlocking.Run),
        new(5, "🛠 Aynı 10 HTTP çağrısı: sıralı / WhenAll / Parallel.For",    Section05_TenHttpCalls.Run),
        new(6, "🛠 Thread safety: paylaşılan sayaçta race condition",          Section06_RaceCondition.Run),
    ];
}
