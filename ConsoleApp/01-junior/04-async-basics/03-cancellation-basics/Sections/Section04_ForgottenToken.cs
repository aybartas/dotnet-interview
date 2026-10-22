using System.Diagnostics;

namespace Junior.AsyncBasics.CancellationBasics;

public static class Section04_ForgottenToken
{
    private const int StageMs = 300;

    // CORRECT: the token is passed to every stage
    private static async Task PipelineAsync(CancellationToken ct)
    {
        await Task.Delay(StageMs, ct);       // stage 1
        await Task.Delay(StageMs, ct);       // stage 2
        await Task.Delay(StageMs, ct);       // stage 3
    }

    // WRONG: it accepts the token but FORGETS to pass it to the sub-calls
    private static async Task ForgetfulPipelineAsync(CancellationToken ct)
    {
        await Task.Delay(StageMs);           // ← NO ct: cancellation is not heard
        await Task.Delay(StageMs);
        await Task.Delay(StageMs);
    }

    private static async Task<long> TimeToCancel(Func<CancellationToken, Task> pipeline)
    {
        using var cts = new CancellationTokenSource();
        var sw = Stopwatch.StartNew();
        var task = pipeline(cts.Token);
        await Task.Delay(100);
        cts.Cancel();                                         // cancel at 100 ms
        try { await task; } catch (OperationCanceledException) { }
        return sw.ElapsedMilliseconds;
    }

    public static async Task Run()
    {
        Console.WriteLine($"3-stage job ({3 * StageMs} ms). Cancelled at 100 ms. We measure 'how many ms after the start did it stop?':\n");

        var good = await TimeToCancel(PipelineAsync);
        Console.WriteLine($"   token is passed to the sub-calls → stopped at {good,4} ms     ✓ reacts to cancellation immediately");

        var bad = await TimeToCancel(ForgetfulPipelineAsync);
        Console.WriteLine($"   forgot to pass the token         → finished at {bad,4} ms      ✗ cancellation completely ignored (all of the work was done)");

        Console.WriteLine("""

        Forgetting to pass the token is a silent bug: no compiler warning, the test passes, but when the user cancels the work continues.
        Defence: the CA2016 analyzer rule ("Forward the CancellationToken parameter") — its default severity is low (suggestion); make it a warning via .editorconfig. Look for 'ct' in code reviews.

        Typical APIs that accept a token:
          • HttpClient        : GetAsync(url, ct), GetStringAsync(url, ct), SendAsync(request, ct)
          • EF Core           : ToListAsync(ct), FirstOrDefaultAsync(ct), SaveChangesAsync(ct)
          • Stream / File     : ReadAsync(buffer, ct), File.ReadAllTextAsync(path, ct)
          • Task / sync       : Task.Delay(ms, ct), SemaphoreSlim.WaitAsync(ct), Task.Run(work, ct)
          • Channel / stream  : reader.ReadAsync(ct), IAsyncEnumerable.WithCancellation(ct)
        In a CPU loop (no API that takes a token) check it yourself: ct.ThrowIfCancellationRequested() — once every N iterations.
        """);
    }
}
