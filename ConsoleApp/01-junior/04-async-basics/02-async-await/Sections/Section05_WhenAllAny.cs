using System.Diagnostics;

namespace Junior.AsyncBasics.AsyncAwait;

public static class Section05_WhenAllAny
{
    private static async Task<int> Ok(string name, int ms, int value)
    {
        await Task.Delay(ms);
        Console.WriteLine($"   [{name}] finished");
        return value;
    }

    private static async Task<int> Fail(string name, int ms)
    {
        await Task.Delay(ms);
        Console.WriteLine($"   [{name}] throwing an ERROR");
        throw new InvalidOperationException($"{name} error");
    }

    public static async Task Run()
    {
        // 1) Success: WhenAll returns the results as an array, duration = the slowest
        var sw = Stopwatch.StartNew();
        int[] values = await Task.WhenAll(Ok("A", 100, 1), Ok("B", 200, 2), Ok("C", 150, 3));
        Console.WriteLine($"1) WhenAll result: [{string.Join(", ", values)}]  duration ≈ {sw.ElapsedMilliseconds} ms (as long as the slowest; 450 if sequential)\n");

        // 2) Error: await throws only the FIRST exception; all of them are in Task.Exception.InnerExceptions
        Console.WriteLine("2) Two tasks fail, one succeeds:");
        var all = Task.WhenAll(Ok("A", 100, 1), Fail("B", 50), Fail("C", 150));
        try
        {
            await all;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"   thrown by await: {ex.GetType().Name}: {ex.Message}   (only the FIRST error!)");
        }
        Console.WriteLine($"   all.Exception.InnerExceptions: {all.Exception!.InnerExceptions.Count} errors → {string.Join(" · ", all.Exception.InnerExceptions.Select(e => e.Message))}");
        Console.WriteLine($"   all.IsFaulted = {all.IsFaulted}; WhenAll does NOT CANCEL the OTHER tasks — it waits until all of them finish (A completed too).\n");

        // 3) WhenAny: take the first one that finishes; the timeout pattern
        var work = Ok("slow job", 500, 99);
        var first = await Task.WhenAny(work, Task.Delay(150));
        Console.WriteLine($"3) WhenAny timeout pattern: did the job finish? {first == work}  → the timeout won; but 'slow job' KEEPS RUNNING IN THE BACKGROUND (cancelling needs a CancellationToken, → J4.3)");
        await work;                                                                      // await it so the log does not get mixed up

        // 4) A cleaner timeout: WaitAsync (.NET 6+)
        try { await Ok("slow job 2", 400, 1).WaitAsync(TimeSpan.FromMilliseconds(100)); }
        catch (TimeoutException) { Console.WriteLine("4) task.WaitAsync(timeout) → TimeoutException (more readable than the WhenAny + Delay pattern)\n"); }
        await Task.Delay(400);

        // 5) WhenEach (.NET 9): process the tasks in COMPLETION ORDER
        Console.WriteLine("5) Task.WhenEach (.NET 9): process in completion order");
        Task<int>[] tasks = [Ok("X", 300, 10), Ok("Y", 100, 20), Ok("Z", 200, 30)];
        await foreach (var done in Task.WhenEach(tasks))
            Console.WriteLine($"   → result arrived: {await done}");

        Console.WriteLine("""

        Reminder:
          • await Task.WhenAll(...) → try/catch sees a single (the first) exception; to log all of them, look at Task.Exception.InnerExceptions.
          • The tasks passed to WhenAll are ALREADY running (hot); WhenAll only waits. Start the tasks without awaiting them, then call WhenAll.
          • With many tasks, limit the concurrency: Parallel.ForEachAsync or SemaphoreSlim (unbounded WhenAll = 10,000 calls to the server).
        """);
    }
}
