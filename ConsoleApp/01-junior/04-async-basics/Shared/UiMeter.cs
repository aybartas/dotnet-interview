using System.Diagnostics;

namespace Junior.AsyncBasics.Shared;

/// <summary>
/// "UI thread duyarlılığı" ölçer: tek thread'li bağlamda bir zamanlayıcı (20 ms'de bir "tık") çalışırken verilen iş de AYNI thread'de koşar.
/// İş thread'i bloklarsa tıklar duraklar (donmuş UI); bloklamazsa tıklar akmaya devam eder.
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

        await Task.Delay(60);                                 // zamanlayıcı ısınsın
        var before = ticks;
        var sw = Stopwatch.StartNew();
        await ui.InvokeAsync(work);                           // iş, "UI thread"inde başlar
        var ms = sw.ElapsedMilliseconds;
        var during = ticks - before;

        cts.Cancel();
        await ticker;
        return (during, ms);
    }
}
