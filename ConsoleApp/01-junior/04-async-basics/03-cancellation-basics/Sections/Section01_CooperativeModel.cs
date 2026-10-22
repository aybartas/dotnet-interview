using System.Diagnostics;

namespace Junior.AsyncBasics.CancellationBasics;

public static class Section01_CooperativeModel
{
    // A worker that WATCHES the token: checks at every step whether cancellation was requested
    private static async Task<int> CooperativeWorker(CancellationToken ct)
    {
        var steps = 0;
        for (var i = 0; i < 20; i++)
        {
            ct.ThrowIfCancellationRequested();            // throw OperationCanceledException if cancellation was requested
            await Task.Delay(50);                         // (we do not pass the token; the check is above, at the start of the step)
            steps++;
        }
        return steps;
    }

    // A worker that IGNORES the token: finishes the job even if cancellation is requested
    private static async Task<int> StubbornWorker(CancellationToken ct)
    {
        var steps = 0;
        for (var i = 0; i < 20; i++)
        {
            await Task.Delay(50);
            steps++;                                      // ct is never checked
        }
        return steps;
    }

    public static async Task Run()
    {
        Console.WriteLine("Cancellation is a COOPERATIVE contract: Cancel() merely raises the 'cancellation requested' flag; NOBODY forcibly stops the work.");
        Console.WriteLine("Code that watches the token stops; code that does not keep running. (There is no Thread.Abort — it is not supported in .NET Core.)\n");

        using var cts = new CancellationTokenSource();
        cts.Token.Register(() => Console.WriteLine("   [callback] cancellation requested — registered callbacks run during Cancel() (for resource cleanup)"));

        var sw = Stopwatch.StartNew();
        var cooperative = CooperativeWorker(cts.Token);
        var stubborn = StubbornWorker(cts.Token);

        await Task.Delay(200);
        Console.WriteLine($"{sw.ElapsedMilliseconds} ms: calling Cancel() (both workers have done ~4 steps so far)...");
        cts.Cancel();

        try { await cooperative; }
        catch (OperationCanceledException) { Console.WriteLine($"{sw.ElapsedMilliseconds,4} ms: the cooperative worker STOPPED  → Task.IsCanceled = {cooperative.IsCanceled}"); }

        Console.WriteLine($"{sw.ElapsedMilliseconds,4} ms: is the stubborn worker still running? {!stubborn.IsCompleted}");
        var steps = await stubborn;
        Console.WriteLine($"{sw.ElapsedMilliseconds,4} ms: the stubborn worker finished — it did ALL {steps} steps (ignored the cancellation)\n");

        Console.WriteLine("Ways of reading the flag:");
        Console.WriteLine($"   token.IsCancellationRequested = {cts.Token.IsCancellationRequested}   → look at the flag, decide yourself (e.g. clean up and return)");
        try { cts.Token.ThrowIfCancellationRequested(); }
        catch (OperationCanceledException ex) { Console.WriteLine($"   token.ThrowIfCancellationRequested() → {ex.GetType().Name}   → the standard way: cut the work short, report upwards"); }

        Console.WriteLine("""

        Model:
          • CancellationTokenSource (CTS) TRIGGERS cancellation (owned by the party that started the request); CancellationToken OBSERVES it (the receiving party, a struct).
          • A CTS is SINGLE-USE: a cancelled CTS cannot be reset; new work = new CTS. CTS is IDisposable (holds a timer/registrations) → using.
          • CancellationToken.None: "cannot be cancelled" — the default value so that the token parameter can be optional.
          • token.Register(callback): cleanup that runs at the moment of cancellation; the registration is removed by disposing the returned object.
          • A cancellable async API puts the Task into the Canceled state; whoever awaits it sees OperationCanceledException (NOT an error).
        """);
    }
}
