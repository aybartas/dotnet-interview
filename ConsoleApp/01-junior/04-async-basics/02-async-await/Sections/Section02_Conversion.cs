using System.Diagnostics;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.AsyncAwait;

public static class Section02_Conversion
{
    public static async Task Run()
    {
        await using var api = await LocalApi.StartAsync();
        using var client = api.CreateClient();
        var path = Path.Combine(Path.GetTempPath(), $"j4-async-{Environment.ProcessId}.txt");
        await File.WriteAllTextAsync(path, "file content");

        try
        {
            await client.GetStringAsync("/delay/1");                                       // warm-up
            Console.WriteLine("Each step is started on the 'UI thread'; a 20 ms timer ticks on the same thread. Tick count = how RESPONSIVE the interface stayed.\n");

            var s1 = await UiMeter.MeasureAsync(() => { _ = ConversionSteps.Step1_Sync(client, path); return Task.CompletedTask; });
            Console.WriteLine($"Step 1 — synchronous            : {s1.Ms,4} ms, UI ticks: {s1.Ticks,2}   ← the UI thread was blocked (frozen interface)");

            var s2 = await UiMeter.MeasureAsync(() => ConversionSteps.Step2_FakeAsync(client, path));
            Console.WriteLine($"Step 2 — wrapped in Task.Run    : {s2.Ms,4} ms, UI ticks: {s2.Ticks,2}   ← UI is free but a pool thread was blocked anyway; same duration");

            var s3 = await UiMeter.MeasureAsync(() => ConversionSteps.Step3_AsyncAllTheWay(client, path));
            Console.WriteLine($"Step 3 — async all the way      : {s3.Ms,4} ms, UI ticks: {s3.Ticks,2}   ← no thread was blocked + independent calls run in parallel → faster");

            Console.WriteLine($"\nContent check: {await ConversionSteps.Step3_AsyncAllTheWay(client, path)}");
        }
        finally
        {
            File.Delete(path);
        }

        Console.WriteLine("""

        Conversion recipe (code: 02-async-await/Conversion/ConversionSteps.cs):
          1. Replace the blocking API with its async counterpart: File.ReadAllText → ReadAllTextAsync, HttpClient.Send/.Result → await GetStringAsync.
          2. Make the method async, make the return type Task/Task<T>, add Async to its name.
          3. Await the CALLERS too → the chain grows upwards: ASYNC ALL THE WAY (up to the entry point: controller, Main, event handler).
          4. Start independent work without awaiting it, then await it together with Task.WhenAll.
          5. Add a CancellationToken and pass it down (→ J4.3).
        Do not mix: .Result/.Wait() in the middle of synchronous code (deadlock/starvation, → section 4) and a blocking call in the middle of async code.
        """);
    }
}
