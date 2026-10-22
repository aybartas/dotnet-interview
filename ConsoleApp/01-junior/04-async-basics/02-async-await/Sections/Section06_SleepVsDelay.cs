using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.AsyncAwait;

public static class Section06_SleepVsDelay
{
    public static async Task Run()
    {
        Console.WriteLine("Waiting 400 ms on the same 'UI thread': Thread.Sleep vs await Task.Delay (tick = interface responsiveness).\n");

        var sleep = await UiMeter.MeasureAsync(() => { Thread.Sleep(400); return Task.CompletedTask; });
        Console.WriteLine($"Thread.Sleep(400)        : {sleep.Ms,4} ms, UI ticks: {sleep.Ticks,2}   ← the thread SLEPT: nothing could run (frozen interface / wasted pool thread)");

        var delay = await UiMeter.MeasureAsync(() => Task.Delay(400));
        Console.WriteLine($"await Task.Delay(400)    : {delay.Ms,4} ms, UI ticks: {delay.Ticks,2}   ← the thread is FREE: the wait was done with a timer");

        Console.WriteLine("""

        • Thread.Sleep: blocks the thread it is on. Using it in an async method defeats the whole purpose of async.
        • await Task.Delay: sets up a timer, frees the thread, and runs the continuation when the time is up.
        • Usage: whenever you need to "wait" (between retries, polling, simulation) → await Task.Delay. In tests, use TimeProvider/fake time instead of really waiting (→ M10).
        • "Wait until cancelled" is also written as Task.Delay(Timeout.Infinite, token).
        """);
    }
}
