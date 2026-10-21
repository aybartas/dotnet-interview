using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Junior.AsyncBasics.AsyncAwait;

public static class Section07_AsyncStreams
{
    // Sayfa sayfa gelen veriyi (API sayfalama, DB okuyucu, dosya satırları) geldikçe üreten async akış
    private static async IAsyncEnumerable<int> ReadPagesAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        for (var page = 1; page <= 5; page++)
        {
            await Task.Delay(100, ct);              // her sayfa 100 ms'de "ağdan" gelir
            yield return page;
        }
    }

    private static async Task<List<int>> ReadAllPagesAsync()                  // aynı iş, ama hepsini bekleyip liste döndürür
    {
        var list = new List<int>();
        await foreach (var p in ReadPagesAsync()) list.Add(p);
        return list;
    }

    private sealed class Connection : IAsyncDisposable
    {
        public async ValueTask DisposeAsync()
        {
            await Task.Delay(50);                                              // kaynağı ASYNC kapat (ör. buffer flush, ağ kapanışı)
            Console.WriteLine("   [Connection] DisposeAsync tamamlandı (async temizlik)");
        }
    }

    public static async Task Run()
    {
        var sw = Stopwatch.StartNew();

        Console.WriteLine("1) await foreach: ilk sonuç beklemeden işlenir (streaming):");
        await foreach (var page in ReadPagesAsync())
            Console.WriteLine($"   {sw.ElapsedMilliseconds,4} ms → sayfa {page} işlendi");

        sw.Restart();
        var all = await ReadAllPagesAsync();
        Console.WriteLine($"\n2) Task<List<int>>: tüm sayfalar bitince birden: {sw.ElapsedMilliseconds} ms sonra [{string.Join(", ", all)}]  (ilk veri de 500 ms'de!)");

        Console.WriteLine("\n3) İptal: WithCancellation(token) — 250 ms'de iptal:");
        using var cts = new CancellationTokenSource(250);
        try
        {
            await foreach (var page in ReadPagesAsync().WithCancellation(cts.Token))
                Console.WriteLine($"   sayfa {page}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("   OperationCanceledException → akış durdu (üretici [EnumeratorCancellation] ile token'ı alır)");
        }

        Console.WriteLine("\n4) await using: IAsyncDisposable kaynağını ASYNC temizler:");
        await using (var connection = new Connection())
        {
            Console.WriteLine("   [using bloğu] kaynak kullanılıyor");
        }                                                                      // blok bitince DisposeAsync await edilir
        Console.WriteLine("   blok sonrası");

        Console.WriteLine("""

        • IAsyncEnumerable<T> = "async foreach edilebilen" akış: büyük/yavaş veriyi bellekte biriktirmeden, geldikçe işlemek için.
          ASP.NET Core Minimal API'de IAsyncEnumerable<T> döndürmek yanıtı akıtır; EF Core: AsQueryable().AsAsyncEnumerable().
        • await using / IAsyncDisposable: Dispose'ın kendisi I/O yapıyorsa (Stream.FlushAsync, DbConnection, kanal kapatma). Senkron Dispose'a sarmak bloklar.
        • Yazarken: yield return + await aynı metotta; iptal için [EnumeratorCancellation] CancellationToken.
        """);
    }
}
