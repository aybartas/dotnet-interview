using System.Diagnostics;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.Concepts;

public static class Section03_CpuVsIoBound
{
#pragma warning disable CS1998 // This "async" method has no await — on purpose: the async keyword does NOT move the work to another thread
    private static async Task<int> FakeAsyncCpu(int size) => Cpu.CountPrimes(size);
#pragma warning restore CS1998

    public static async Task Run()
    {
        // ── CPU-bound: the processor works; solution = more cores (Task.Run / Parallel) ──
        const int size = 200_000;
        var cores = Environment.ProcessorCount;

        var sw = Stopwatch.StartNew();
        for (var i = 0; i < cores; i++) Cpu.CountPrimes(size);
        var sequential = sw.ElapsedMilliseconds;

        sw.Restart();
        await Task.WhenAll(Enumerable.Range(0, cores).Select(_ => Task.Run(() => Cpu.CountPrimes(size))));
        var parallel = sw.ElapsedMilliseconds;
        Console.WriteLine($"CPU-bound ({cores} jobs, prime counting): sequential {sequential} ms → Task.Run + WhenAll {parallel} ms   (CPU is busy; adding cores speeds it up)");

        // ── I/O-bound: the work consists of WAITING (network, disk, database); the CPU is idle; solution = wait without holding a thread (async) ──
        var threadsBefore = ThreadPool.ThreadCount;
        sw.Restart();
        await Task.WhenAll(Enumerable.Range(0, 200).Select(_ => Task.Delay(300)));      // 200 "network calls"
        Console.WriteLine($"I/O-bound (200 concurrent 300 ms waits): {sw.ElapsedMilliseconds} ms total, thread count {threadsBefore} → {ThreadPool.ThreadCount}");
        Console.WriteLine("   (waiting sequentially would take 60 s; 200 threads would cost ~200 MB of stack — async: no threads, just waiting task objects)");

        // ── Misconception: "if I make it async, the CPU work gets faster" ──
        sw.Restart();
        var task = FakeAsyncCpu(size * 2);                  // NO await: the method runs SYNCHRONOUSLY on the calling thread
        var returnedAfter = sw.ElapsedMilliseconds;
        await task;

        sw.Restart();
        var offloaded = Task.Run(() => Cpu.CountPrimes(size * 2));
        var offloadReturnedAfter = sw.ElapsedMilliseconds;
        await offloaded;

        Console.WriteLine($"\nMisconception: 'async' does not move CPU work to the background. The async method call returned after {returnedAfter} ms (the caller was blocked); Task.Run returned in {offloadReturnedAfter} ms.");

        Console.WriteLine("""

        Which work, which tool?
        ┌────────────────┬───────────────────────────────┬─────────────────────────────────────────────────┐
        │ Work type      │ Bottleneck                    │ Tool                                            │
        ├────────────────┼───────────────────────────────┼─────────────────────────────────────────────────┤
        │ I/O-bound      │ network, disk, DB (waiting)   │ async/await (GetStringAsync, ReadAsync…)        │
        │ CPU-bound      │ processor (compute, compress) │ Parallel.For / PLINQ / Task.Run (UI/background) │
        └────────────────┴───────────────────────────────┴─────────────────────────────────────────────────┘
        In ASP.NET Core, pushing CPU-bound work from the request thread to another pool thread with Task.Run GAINS NOTHING
        (same pool); only moving it to a background service/queue (→ M12) makes sense.
        """);
    }
}
