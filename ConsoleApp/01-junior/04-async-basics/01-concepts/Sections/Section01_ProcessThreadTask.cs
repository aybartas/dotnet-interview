using System.Diagnostics;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.Concepts;

public static class Section01_ProcessThreadTask
{
    public static async Task Run()
    {
        var p = Process.GetCurrentProcess();
        Console.WriteLine($"PROCESS : Id={p.Id}, name={p.ProcessName}, OS thread count={p.Threads.Count}, logical cores={Environment.ProcessorCount}");
        Console.WriteLine("          → isolated memory space; the threads inside it share the SAME memory (this is where thread safety problems come from)\n");

        Console.WriteLine($"THREAD  : current thread = {Cpu.Tid} (thread pool thread? {Thread.CurrentThread.IsThreadPoolThread})");
        var dedicated = new Thread(() =>
            Console.WriteLine($"          Thread we created ourselves → {Cpu.Tid}, pool? {Thread.CurrentThread.IsThreadPoolThread}, background? {Thread.CurrentThread.IsBackground}"));
        dedicated.Start();
        dedicated.Join();
        Console.WriteLine("          Thread = an execution flow at the operating system level: ~1 MB stack, expensive to create/switch.\n");

        ThreadPool.GetMinThreads(out var minWorker, out _);
        Console.WriteLine($"THREAD POOL: ready-made threads are reused. Currently {ThreadPool.ThreadCount} threads; at least {minWorker} worker threads are spun up immediately.");
        var onPool = await Task.Run(() => $"Task.Run → {Cpu.Tid}, pool? {Thread.CurrentThread.IsThreadPoolThread}");
        Console.WriteLine($"          {onPool}\n");

        Console.WriteLine("TASK    : an object representing the FUTURE result of a piece of work (a promise). Task ≠ thread:");
        var done = Task.FromResult(42);
        Console.WriteLine($"          Task.FromResult(42) → completed? {done.IsCompleted}, result={done.Result}  (no thread at all!)");

        var tcs = new TaskCompletionSource<string>();
        var waiting = tcs.Task;
        Console.WriteLine($"          TaskCompletionSource.Task → completed? {waiting.IsCompleted}  (waits for an external event, holds no thread)");
        tcs.SetResult("event arrived");
        Console.WriteLine($"          after SetResult → {await waiting}");

        var before = ThreadPool.ThreadCount;
        await Task.WhenAll(Enumerable.Range(0, 1000).Select(_ => Task.Delay(300)));
        Console.WriteLine($"          1000 Task.Delay(300) awaited together; thread count {before} → {ThreadPool.ThreadCount} (1000 threads were NOT created for 1000 waits: a waiting task holds no thread, it uses a timer)");

        Console.WriteLine("""

        Summary:
          • Process  : isolated resource container (memory, handles). When it exits, everything ends.
          • Thread   : an execution flow inside a process; works on shared memory; expensive and limited in number.
          • Task     : a promise that "this work will finish some day". It can run on a thread (Task.Run) OR with no thread at all (I/O wait).
        """);
    }
}
