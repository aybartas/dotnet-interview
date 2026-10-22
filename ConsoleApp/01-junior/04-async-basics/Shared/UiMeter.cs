using System.Diagnostics;

namespace Junior.AsyncBasics.Shared;

/// <summary>
/// Measures "UI thread responsiveness": while a timer (a "tick" every 20 ms) runs on a single-threaded context, the given work runs on the SAME thread.
/// If the work blocks the thread, the ticks stall (frozen UI); if it does not block, the ticks keep flowing.
/// </summary>
public static class UiMeter
{
    public static async Task<(int Ticks, long Ms)> MeasureAsync(Func<Task> work)
    {
        using var ui = new SingleThreadContext("UI");
        using var cts = new CancellationTokenSource();
        var ticks = 0;

        var ticker = ui.InvokeAsync(async () =>
        {
            try { while (true) { await Task.Delay(20, cts.Token); ticks++; } }
            catch (OperationCanceledException) { }
        });

        await Task.Delay(60);                                 // let the timer warm up
        var before = ticks;
        var sw = Stopwatch.StartNew();
        await ui.InvokeAsync(work);                           // the work starts on the "UI thread"
        var ms = sw.ElapsedMilliseconds;
        var during = ticks - before;

        cts.Cancel();
        await ticker;
        return (during, ms);
    }
}
