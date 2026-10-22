using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.AsyncAwait;

public static class Section04_ResultDeadlock
{
    private static async Task<string> GetDataAsync()
    {
        await Task.Delay(100);                                    // the continuation wants to be posted back to the CAPTURED context (the UI thread)
        return "data";
    }

    private static async Task<string> GetDataNoContextAsync()
    {
        await Task.Delay(100).ConfigureAwait(false);              // "I do not have to return to the context" → continues on a pool thread
        return "data";
    }

    private static async Task Show(string title, Task<string> task, int timeoutMs = 1500)
    {
        var winner = await Task.WhenAny(task, Task.Delay(timeoutMs));
        Console.WriteLine(winner == task
            ? $"   {title,-44} → COMPLETED: {await task}"
            : $"   {title,-44} → did NOT finish within {timeoutMs} ms: DEADLOCK ☠");
    }

    public static async Task Run()
    {
        Console.WriteLine("Scene: a single-threaded 'UI' context (like WinForms/WPF/legacy ASP.NET). After an await, the continuation is posted BACK to this thread.\n");

        // 1) Classic deadlock: the UI thread is blocked with .Result; the continuation wants to return to the UI thread but the thread is blocked → each waits for the other
        var ui1 = new SingleThreadContext("UI-1");
        await Show("1) GetDataAsync().Result on the UI thread", ui1.InvokeAsync(() => Task.FromResult(GetDataAsync().Result)));

        // 2) With ConfigureAwait(false) library code does not return to the context → .Result can finish (but the thread was still blocked: NOT a solution, fragile)
        var ui2 = new SingleThreadContext("UI-2");
        await Show("2) .Result + ConfigureAwait(false)", ui2.InvokeAsync(() => Task.FromResult(GetDataNoContextAsync().Result)));

        // 3) The right solution: await
        var ui3 = new SingleThreadContext("UI-3");
        await Show("3) await GetDataAsync()  (async all the way)", ui3.InvokeAsync(async () => await GetDataAsync()));

        // 4) Difference in error wrapping
        Console.WriteLine("\nError shape: .Result/.Wait() wrap the error in an AggregateException; GetAwaiter().GetResult() and await give the original exception:");
        static async Task<int> Boom() { await Task.Yield(); throw new InvalidOperationException("original error"); }
        try { _ = Boom().Result; } catch (Exception ex) { Console.WriteLine($"   .Result                  → {ex.GetType().Name} → inner: {ex.InnerException?.GetType().Name}"); }
        try { _ = Boom().GetAwaiter().GetResult(); } catch (Exception ex) { Console.WriteLine($"   GetAwaiter().GetResult() → {ex.GetType().Name}"); }

        Console.WriteLine("""

        ⭐ Why are .Result / .Wait() dangerous?
          1) DEADLOCK: the situation above, in environments that have a SynchronizationContext (UI, classic ASP.NET). ASP.NET Core has NO context → this deadlock does not happen...
          2) ...BUT thread pool STARVATION does: every request blocks a pool thread; as load grows the pool cannot keep up and the server locks up (→ 4.1 Section 4, M4.3).
          3) Error wrapping (AggregateException) and weak cancellation/timeout handling.
        Rule: ASYNC ALL THE WAY. If a synchronous entry point is unavoidable (rare): GetAwaiter().GetResult() in a single place, with a comment explaining why it is safe.
        Note: a console Main can be async Task Main; ASP.NET Core controllers/endpoints can be async → most of the time you do NOT have to block.
        """);
    }
}
