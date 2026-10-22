using System.Diagnostics;

namespace Junior.AsyncBasics.Concepts;

/// <summary>🛠 J4.1: Race condition demo on a shared counter.</summary>
public static class Section06_RaceCondition
{
    private const int Workers = 4;
    private const int PerWorker = 1_000_000;

    public static Task Run()
    {
        var expected = Workers * PerWorker;
        Console.WriteLine($"{Workers} threads × {PerWorker:N0} increments → expected result = {expected:N0}  ({Environment.ProcessorCount} cores)\n");
        Console.WriteLine("counter++ is a single line but THREE steps: read → add 1 → write. If two threads read the same value and write the same result, one increment is LOST.\n");

        // 1) Unsafe
        var counter = 0;
        var sw = Stopwatch.StartNew();
        RunWorkers(() => { for (var i = 0; i < PerWorker; i++) counter++; });
        Print("1) counter++ (no lock, RACE CONDITION)", counter, expected, sw);

        // 2) Interlocked: a single atomic CPU instruction
        var atomic = 0;
        sw.Restart();
        RunWorkers(() => { for (var i = 0; i < PerWorker; i++) Interlocked.Increment(ref atomic); });
        Print("2) Interlocked.Increment (atomic)", atomic, expected, sw);

        // 3) lock: critical section; one thread at a time
        var locked = 0;
        var gate = new object();
        sw.Restart();
        RunWorkers(() => { for (var i = 0; i < PerWorker; i++) lock (gate) locked++; });
        Print("3) lock (mutual exclusion)", locked, expected, sw);

        // 4) Eliminate the sharing: each thread keeps its own total, merged at the end (the fastest)
        var partials = new int[Workers];
        sw.Restart();
        Parallel.For(0, Workers, w => { var local = 0; for (var i = 0; i < PerWorker; i++) local++; partials[w] = local; });
        Print("4) per-thread local total + merge", partials.Sum(), expected, sw);

        Console.WriteLine("""

        Notes:
          • The number of lost increments VARIES from run to run (timing-dependent); if nothing was lost, run it again (can be rare on a single-core machine).
          • A race condition happens "sometimes" → it is the most dangerous kind of bug: passes in testing, blows up in production under load.
          • Hierarchy: first reduce sharing (immutable, per-thread data) → atomic operation (Interlocked) → lock → (rarely) more advanced primitives (→ M4, S3).
          • async/await alone does NOT provide thread safety: the code after an await may run on another thread and touch the same state.
        """);
        return Task.CompletedTask;
    }

    private static void RunWorkers(Action work) =>
        Task.WaitAll(Enumerable.Range(0, Workers).Select(_ => Task.Run(work)).ToArray());

    private static void Print(string title, int actual, int expected, Stopwatch sw) =>
        Console.WriteLine($"{title,-46} result = {actual,10:N0}   {(actual == expected ? "✓ correct" : $"✗ {expected - actual:N0} increments LOST"),-26} {sw.ElapsedMilliseconds} ms");
}
