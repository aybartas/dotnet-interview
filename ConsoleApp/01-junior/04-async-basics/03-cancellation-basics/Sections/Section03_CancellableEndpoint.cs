using Microsoft.AspNetCore.Builder;
using System.Collections.Concurrent;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.CancellationBasics;

/// <summary>
/// 🛠 J4.3: An API endpoint that makes a long-running report generation cancellable; shows that the work stops when the client disconnects.
/// The same report on two endpoints: one watches the RequestAborted token, the other does not.
/// </summary>
public static class Section03_CancellableEndpoint
{
    private const int Steps = 10, StepMs = 150;

    public static async Task Run()
    {
        var log = new ConcurrentQueue<string>();

        async Task<string> GenerateReportAsync(string endpoint, CancellationToken ct)
        {
            for (var step = 1; step <= Steps; step++)
            {
                log.Enqueue($"[{endpoint}] step {step}/{Steps}");
                await Task.Delay(StepMs, ct);                  // watch the token at every step: OperationCanceledException if cancelled
            }
            log.Enqueue($"[{endpoint}] report COMPLETED (even though nobody is waiting)");
            return "report ready";
        }

        await using var api = await LocalApi.StartAsync(app =>
        {
            // Minimal API: a 'CancellationToken' parameter is automatically bound to HttpContext.RequestAborted (cancelled when the client disconnects).
            app.MapGet("/report", async (CancellationToken requestAborted) =>
            {
                try { return await GenerateReportAsync("/report", requestAborted); }
                catch (OperationCanceledException)
                {
                    log.Enqueue("[/report] CLIENT DISCONNECTED → RequestAborted fired, report generation STOPPED");
                    throw;
                }
            });

            // The version that does not pass the token: even if the client is gone, the server does the work to the end (wasted CPU/DB)
            app.MapGet("/report-ignores-token", (CancellationToken requestAborted) =>
                GenerateReportAsync("/report-ignores-token", CancellationToken.None));
        });
        using var client = api.CreateClient();

        Console.WriteLine($"Report: {Steps} steps × {StepMs} ms = {Steps * StepMs} ms. The client gives up after 400 ms and disconnects.\n");

        foreach (var endpoint in new[] { "/report", "/report-ignores-token" })
        {
            using var cts = new CancellationTokenSource(400);
            try { await client.GetStringAsync(endpoint, cts.Token); }
            catch (OperationCanceledException) { Console.WriteLine($"CLIENT: {endpoint} → cancelled (connection dropped)"); }
        }

        await Task.Delay(Steps * StepMs + 300);                // wait for the normal duration of the work to see what the server did

        Console.WriteLine("\nSERVER LOG:");
        foreach (var line in log) Console.WriteLine($"   {line}");

        Console.WriteLine("""

        Observation:
          • /report            : when the client leaves, Kestrel cancels HttpContext.RequestAborted → the loop stops at ~step 3 (no resources wasted).
          • /report-ignores-token : the client is long gone but the server finished all 10 steps — every abandoned request costs the server the full price.
        In ASP.NET Core:
          • Take a CancellationToken as an endpoint/controller action parameter → the framework binds RequestAborted.
          • Optionally: HttpContext.RequestAborted can also be accessed directly.
          • Pass the token to the database (EF Core: ToListAsync(ct)), to HttpClient and to other I/O (see section 4).
        Proof: for the cancelled request the server log ends with the "STOPPED" line; the version that ignores the token does all of the work.
        """);
    }
}
