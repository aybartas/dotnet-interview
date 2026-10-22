using System.Collections.Concurrent;
using System.Diagnostics;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.Concepts;

/// <summary>🛠 J4.1: Make the same 10 HTTP calls sequentially, with Task.WhenAll and with Parallel.For; compare the duration and the number of threads used.</summary>
public static class Section05_TenHttpCalls
{
    private const int Calls = 10;
    private const int DelayMs = 200;

    public static async Task Run()
    {
        await using var api = await LocalApi.StartAsync();
        using var client = api.CreateClient();
        await client.GetStringAsync("/delay/1");                                  // warm-up: keep the connection + JIT cost out of the measurement
        Console.WriteLine($"Local API: {api.BaseUrl}  ·  {Calls} calls × {DelayMs} ms delay  ·  {Environment.ProcessorCount} cores\n");

        // 1) SEQUENTIAL: each call waits for the previous one to finish → total ≈ 10 × 200
        var seen = new ConcurrentDictionary<int, bool>();
        var sw = Stopwatch.StartNew();
        for (var i = 0; i < Calls; i++)
        {
            seen[Environment.CurrentManagedThreadId] = true;
            await client.GetStringAsync($"/delay/{DelayMs}");
            seen[Environment.CurrentManagedThreadId] = true;
        }
        Report("1) Sequential await (for)", sw, seen);

        // 2) Task.WhenAll: 10 calls start AT THE SAME TIME, continue when all finish → total ≈ 200 (as slow as the slowest)
        seen.Clear();
        sw.Restart();
        await Task.WhenAll(Enumerable.Range(0, Calls).Select(async _ =>
        {
            seen[Environment.CurrentManagedThreadId] = true;
            await client.GetStringAsync($"/delay/{DelayMs}");
            seen[Environment.CurrentManagedThreadId] = true;
        }));
        Report("2) Task.WhenAll (the right tool for I/O)", sw, seen);

        // 3) Parallel.For + blocking call: Parallel.For is meant for CPU work; it waits for the call by BLOCKING → each thread is stuck on one call
        seen.Clear();
        sw.Restart();
        Parallel.For(0, Calls, _ =>
        {
            seen[Environment.CurrentManagedThreadId] = true;
            client.GetStringAsync($"/delay/{DelayMs}").GetAwaiter().GetResult();    // sync-over-async: the thread is blocked
        });
        Report("3) Parallel.For + .GetResult() (WRONG tool)", sw, seen);

        // 4) Parallel.ForEachAsync (.NET 6+): supports an async body, a concurrency limit can be given
        seen.Clear();
        sw.Restart();
        await Parallel.ForEachAsync(Enumerable.Range(0, Calls), new ParallelOptions { MaxDegreeOfParallelism = 5 }, async (_, ct) =>
        {
            seen[Environment.CurrentManagedThreadId] = true;
            await client.GetStringAsync($"/delay/{DelayMs}", ct);
            seen[Environment.CurrentManagedThreadId] = true;
        });
        Report("4) Parallel.ForEachAsync (max. 5 concurrent)", sw, seen);

        Console.WriteLine("""

        Conclusion:
          • For I/O-bound work use WhenAll (or Parallel.ForEachAsync if you need a concurrency limit).
          • Parallel.For/PLINQ is for CPU-bound work; putting blocking I/O inside it PUTS THREADS TO SLEEP: even if the time looks short, 10 threads were blocked (when the pool runs out, the time grows and other requests starve).
          • With WhenAll, "how many threads?" is the wrong question: there is no thread while waiting; the continuations run on a few threads from the pool.
        """);
    }

    private static void Report(string title, Stopwatch sw, ConcurrentDictionary<int, bool> seen) =>
        Console.WriteLine($"{title,-48} {sw.ElapsedMilliseconds,5} ms   distinct thread count: {seen.Count}");
}
