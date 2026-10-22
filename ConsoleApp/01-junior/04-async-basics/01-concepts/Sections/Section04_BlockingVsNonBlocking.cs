using System.Diagnostics;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.Concepts;

public static class Section04_BlockingVsNonBlocking
{
    public static async Task Run()
    {
        ThreadPool.GetMinThreads(out var min, out _);
        var requests = min * 4;                              // number of "concurrent users"
        const int ioMs = 250;

        Console.WriteLine($"Scenario: {requests} concurrent requests, each waiting for a {ioMs} ms I/O (a database call). The pool has {min} threads immediately available.\n");

        // BLOCKING: each request waits by putting a thread to sleep (simulates synchronous I/O)
        var sw = Stopwatch.StartNew();
        await Task.WhenAll(Enumerable.Range(0, requests).Select(_ => Task.Run(() => Thread.Sleep(ioMs))));
        var blocking = sw.ElapsedMilliseconds;

        // NON-BLOCKING: the thread is free while waiting
        sw.Restart();
        await Task.WhenAll(Enumerable.Range(0, requests).Select(async _ => await Task.Delay(ioMs)));
        var nonBlocking = sw.ElapsedMilliseconds;

        Console.WriteLine($"   blocking     (Thread.Sleep)  : {blocking,5} ms   ← each thread is stuck on one request; the rest waited in the queue");
        Console.WriteLine($"   non-blocking (await Delay)   : {nonBlocking,5} ms   ← all requests waited at the same time, the threads were idle");

        Console.WriteLine($"\n   Expected: blocking ≈ {Math.Ceiling(requests / (double)min)} waves × {ioMs} ms (the pool adds threads slowly: one per ~500 ms), non-blocking ≈ {ioMs} ms.");

        Console.WriteLine("""

        Why is blocking a thread expensive on a web server?
          • Each thread needs ~1 MB of stack; thousands of threads = gigabytes of memory + context switch cost.
          • The pool adds threads SLOWLY when needed (1 per ~500 ms beyond the minimum). Under a sudden load spike, blocking requests
            wait in the queue → latency explodes → THREAD POOL STARVATION. The server shows the "got slow but CPU is at 5%" symptom.
          • async: a waiting request holds no thread → thousands of concurrent requests with the same number of threads (→ M4.3 in depth).
        Rule: never do I/O synchronously (File.ReadAllText, HttpClient.Send, .Result, .Wait()) — `await` the async counterpart.
        """);
    }
}
