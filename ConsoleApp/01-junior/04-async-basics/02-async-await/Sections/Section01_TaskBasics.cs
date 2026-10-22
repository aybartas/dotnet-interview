using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.AsyncAwait;

public static class Section01_TaskBasics
{
    // the signature of an async method: return type Task (no value) / Task<T> (has a value) / ValueTask<T> (hot paths that often complete synchronously)
    private static async Task<int> ComputeAsync(int x)
    {
        Console.WriteLine($"   [ComputeAsync] started ({Cpu.Tid}) — runs SYNCHRONOUSLY on the caller's thread until the first await");
        await Task.Delay(100);
        Console.WriteLine($"   [ComputeAsync] continuing after await ({Cpu.Tid}) — no SynchronizationContext in a console app → any thread from the pool");
        return x * 2;
    }

    private static async Task DoWorkAsync() => await Task.Delay(50);                 // Task: returns no value (the async counterpart of void)

    private static Task<int> PassThrough(int x) => ComputeAsync(x);                  // without async/await: return the Task directly (only if you are just passing it through)

    public static async Task Run()
    {
        Console.WriteLine($"1) Calling thread: {Cpu.Tid}");
        var task = ComputeAsync(21);                                                 // NO await: we only started it (the task is "hot" — already running)
        Console.WriteLine($"   ComputeAsync returned but has not finished: IsCompleted={task.IsCompleted}  (a Task is a PROMISE; there is no result yet)");
        var result = await task;                                                     // await: wait for the result (WITHOUT blocking the thread)
        Console.WriteLine($"   await result = {result}\n");

        Console.WriteLine("2) What does await do?");
        Console.WriteLine("   • If the Task has already completed: it continues (synchronously, no extra cost).");
        Console.WriteLine("   • If it has not completed: it registers the REST of the method as a 'continuation' and returns to the caller IMMEDIATELY. The thread is freed.");
        Console.WriteLine("   • When the Task completes, the continuation runs (in UI/legacy ASP.NET it returns to the same context; in ASP.NET Core/console it runs on a pool thread).");
        Console.WriteLine("   The compiler turns the method into a STATE MACHINE (internals → M4.2).\n");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        await DoWorkAsync();
        Console.WriteLine($"3) async Task (no value): completed after {sw.ElapsedMilliseconds} ms");

        Console.WriteLine($"4) Pass-through without async/await: PassThrough(5) → {await PassThrough(5)}   ('async' is unnecessary only in methods that return the Task as-is; but inside using/try, AWAIT IS REQUIRED)\n");

        Console.WriteLine("5) The difference between 'Task Result' and 'Task' + ready-made tasks:");
        Task<int> ready = Task.FromResult(7);                                        // no thread, minimal memory allocation
        Console.WriteLine($"   Task.FromResult(7).IsCompletedSuccessfully = {ready.IsCompletedSuccessfully}; Task.CompletedTask serves the same purpose (early exit in a Task-returning method)");

        Console.WriteLine("""

        Rules:
          • Give asynchronous methods the 'Async' suffix (GetUserAsync); return type Task/Task<T> (ValueTask<T> only for measured hot paths).
          • 'async' does NOT move a method to another thread; it is await that waits without a thread.
          • Do not leave a Task un-awaited (fire-and-forget): errors and completion are lost (CS4014 warning; make the analyzer a warning/error).
        """);
    }
}
