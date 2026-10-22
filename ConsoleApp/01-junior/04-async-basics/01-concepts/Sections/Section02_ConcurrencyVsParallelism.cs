using System.Collections.Concurrent;
using System.Diagnostics;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.Concepts;

public static class Section02_ConcurrencyVsParallelism
{
    public static async Task Run()
    {
        Console.WriteLine("CONCURRENCY = MULTIPLE jobs being in progress at the same time (they may take turns; a single thread is enough).");
        Console.WriteLine("PARALLELISM = multiple jobs REALLY running at the same instant (needs multiple cores).\n");

        // ── 1) Concurrency WITHOUT parallelism: 3 cooks take turns on a single-threaded context ──
        using var ui = new SingleThreadContext("single-thread");
        var log = new ConcurrentQueue<(long Ms, string Text)>();
        var sw = Stopwatch.StartNew();

        async Task Cook(string name)
        {
            for (var step = 1; step <= 3; step++)
            {
                log.Enqueue((sw.ElapsedMilliseconds, $"{name} step {step} ({Cpu.Tid})"));
                await Task.Delay(100);                         // while waiting, the thread stays FREE; the other cook makes progress
            }
        }

        await ui.InvokeAsync(() => Task.WhenAll(Cook("A"), Cook("B"), Cook("C")));
        Console.WriteLine($"1) 3 async jobs on a single thread (~900 ms if done sequentially, here {sw.ElapsedMilliseconds} ms):");
        foreach (var (ms, text) in log.OrderBy(l => l.Ms)) Console.WriteLine($"   {ms,4} ms  {text}");
        Console.WriteLine($"   → all on the same thread ({ui.ThreadId}): there is CONCURRENCY, no PARALLELISM.\n");

        // ── 2) Parallelism: the CPU work is split across multiple cores ──
        const int jobs = 8, size = 150_000;
        var seq = Stopwatch.StartNew();
        for (var i = 0; i < jobs; i++) Cpu.CountPrimes(size);
        seq.Stop();

        var threads = new ConcurrentDictionary<int, bool>();
        var par = Stopwatch.StartNew();
        Parallel.For(0, jobs, _ =>
        {
            threads[Environment.CurrentManagedThreadId] = true;
            Cpu.CountPrimes(size);
        });
        par.Stop();

        Console.WriteLine($"2) {jobs} CPU jobs ({Environment.ProcessorCount} cores): sequential {seq.ElapsedMilliseconds} ms · Parallel.For {par.ElapsedMilliseconds} ms ({threads.Count} distinct threads)");
        Console.WriteLine("   → PARALLELISM: they really ran at the same time. The gain is limited by the core count; on a single core there is no gain.");

        Console.WriteLine("""

        ⭐ Interview sentence:
          "Concurrency is about MANAGING jobs (many jobs can be in progress at once), parallelism is about RUNNING jobs AT THE SAME TIME.
           async/await gives concurrency for I/O (it waits without holding a thread); Parallel.For/PLINQ gives parallelism for CPU work."
        """);
    }
}
