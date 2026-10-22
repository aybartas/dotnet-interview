using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Junior.AsyncBasics.AsyncAwait;

public static class Section07_AsyncStreams
{
    // An async stream that produces data arriving page by page (API paging, DB reader, file lines) as it arrives
    private static async IAsyncEnumerable<int> ReadPagesAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        for (var page = 1; page <= 5; page++)
        {
            await Task.Delay(100, ct);              // each page arrives "from the network" in 100 ms
            yield return page;
        }
    }

    private static async Task<List<int>> ReadAllPagesAsync()                  // the same job, but it waits for everything and returns a list
    {
        var list = new List<int>();
        await foreach (var p in ReadPagesAsync()) list.Add(p);
        return list;
    }

    private sealed class Connection : IAsyncDisposable
    {
        public async ValueTask DisposeAsync()
        {
            await Task.Delay(50);                                              // close the resource ASYNCHRONOUSLY (e.g. buffer flush, network shutdown)
            Console.WriteLine("   [Connection] DisposeAsync completed (async cleanup)");
        }
    }

    public static async Task Run()
    {
        var sw = Stopwatch.StartNew();

        Console.WriteLine("1) await foreach: the first result is processed without waiting (streaming):");
        await foreach (var page in ReadPagesAsync())
            Console.WriteLine($"   {sw.ElapsedMilliseconds,4} ms → page {page} processed");

        sw.Restart();
        var all = await ReadAllPagesAsync();
        Console.WriteLine($"\n2) Task<List<int>>: all at once when every page is done: after {sw.ElapsedMilliseconds} ms [{string.Join(", ", all)}]  (even the first data at 500 ms!)");

        Console.WriteLine("\n3) Cancellation: WithCancellation(token) — cancelled at 250 ms:");
        using var cts = new CancellationTokenSource(250);
        try
        {
            await foreach (var page in ReadPagesAsync().WithCancellation(cts.Token))
                Console.WriteLine($"   page {page}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("   OperationCanceledException → the stream stopped (the producer receives the token via [EnumeratorCancellation])");
        }

        Console.WriteLine("\n4) await using: cleans up an IAsyncDisposable resource ASYNCHRONOUSLY:");
        await using (var connection = new Connection())
        {
            Console.WriteLine("   [using block] the resource is in use");
        }                                                                      // DisposeAsync is awaited when the block ends
        Console.WriteLine("   after the block");

        Console.WriteLine("""

        • IAsyncEnumerable<T> = a stream you can "async foreach": for processing large/slow data as it arrives, without accumulating it in memory.
          Returning IAsyncEnumerable<T> from an ASP.NET Core Minimal API streams the response; EF Core: AsQueryable().AsAsyncEnumerable().
        • await using / IAsyncDisposable: when Dispose itself does I/O (Stream.FlushAsync, DbConnection, closing a channel). Wrapping it in a synchronous Dispose blocks.
        • When writing one: yield return + await in the same method; for cancellation, [EnumeratorCancellation] CancellationToken.
        """);
    }
}
