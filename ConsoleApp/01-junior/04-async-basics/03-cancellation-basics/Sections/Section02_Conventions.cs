using System.Diagnostics;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.CancellationBasics;

public static class Section02_Conventions
{
    // CONVENTION: CancellationToken is the LAST parameter and usually has a 'default' value; it is named 'cancellationToken' or 'ct'
    private static async Task<string> LoadReportAsync(string name, int pageSize = 100, CancellationToken cancellationToken = default)
    {
        await Task.Delay(300, cancellationToken);              // PASS it down: give it to every call that accepts a token
        return $"{name}:{pageSize}";
    }

    public static async Task Run()
    {
        Console.WriteLine("1) Signature convention: the token is the LAST parameter, optional:");
        Console.WriteLine("   Task<string> LoadReportAsync(string name, int pageSize = 100, CancellationToken cancellationToken = default)");
        Console.WriteLine($"   call: await LoadReportAsync(\"sales\") → {await LoadReportAsync("sales")}\n");

        // 2) CancelAfter: timeout
        Console.WriteLine("2) CancelAfter(timeout): automatic cancellation after a while:");
        using (var timeout = new CancellationTokenSource())
        {
            timeout.CancelAfter(100);
            var sw = Stopwatch.StartNew();
            try { await LoadReportAsync("slow", cancellationToken: timeout.Token); }
            catch (OperationCanceledException ex) { Console.WriteLine($"   {ex.GetType().Name} after {sw.ElapsedMilliseconds} ms (the work would have taken 300 ms)"); }
        }

        // 3) Catching OperationCanceledException well: cancellation is NOT an error
        Console.WriteLine("\n3) Catching OperationCanceledException correctly:");
        using var userCts = new CancellationTokenSource();
        userCts.Cancel();
        try { await LoadReportAsync("x", cancellationToken: userCts.Token); }
        catch (OperationCanceledException) when (userCts.IsCancellationRequested)
        {
            Console.WriteLine("   when (token.IsCancellationRequested) → this is the cancellation we asked for: log it (Information), do not report it as an ERROR, do not continue");
        }
        Console.WriteLine("   (Without the filter, cancellations from other sources — e.g. HttpClient timeout — would be swallowed too.)");

        // 4) User cancellation + timeout together: linked token
        Console.WriteLine("\n4) Linked token: user cancellation + timeout combined:");
        using var user = new CancellationTokenSource();
        using var limit = new CancellationTokenSource(TimeSpan.FromMilliseconds(80));
        using var linked = CancellationTokenSource.CreateLinkedTokenSource(user.Token, limit.Token);
        try { await LoadReportAsync("combined", cancellationToken: linked.Token); }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"   cancelled. By the user? {user.IsCancellationRequested}  By the timeout? {limit.IsCancellationRequested}  → you can tell the cause apart (408/504 vs silent exit)");
        }

        // 5) HttpClient: cancellation vs timeout
        Console.WriteLine("\n5) Cancellation vs timeout in HttpClient (both are TaskCanceledException — a subclass of OperationCanceledException):");
        await using var api = await LocalApi.StartAsync();
        using var client = api.CreateClient();

        using var cancel = new CancellationTokenSource(100);
        try { await client.GetStringAsync("/delay/2000", cancel.Token); }
        catch (OperationCanceledException ex) { Console.WriteLine($"   token cancellation → {ex.GetType().Name}, inner exception: {ex.InnerException?.GetType().Name ?? "none"}"); }

        using var timeoutClient = new HttpClient { BaseAddress = new Uri(api.BaseUrl), Timeout = TimeSpan.FromMilliseconds(100) };
        try { await timeoutClient.GetStringAsync("/delay/2000"); }
        catch (OperationCanceledException ex) { Console.WriteLine($"   HttpClient.Timeout  → {ex.GetType().Name}, inner exception: {ex.InnerException?.GetType().Name ?? "none"}  ← told apart by TimeoutException"); }

        Console.WriteLine("""

        Rules:
          • Make CancellationToken the LAST parameter in the method signature; do NOT store it as a ctor parameter/property (it lives per request).
          • Every cancellable async method should accept a token and pass it to EVERY async API it calls.
          • OperationCanceledException represents cancelled work → do not produce an error log/500; if you catch it, narrow it with when (token.IsCancellationRequested).
          • After cancellation, clean up/roll back partial state (half-written file, open transaction).
        """);
    }
}
