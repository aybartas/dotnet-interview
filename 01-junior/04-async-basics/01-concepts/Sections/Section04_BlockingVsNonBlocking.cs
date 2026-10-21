using System.Diagnostics;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.Concepts;

public static class Section04_BlockingVsNonBlocking
{
    public static async Task Run()
    {
        ThreadPool.GetMinThreads(out var min, out _);
        var requests = min * 4;                              // "eşzamanlı kullanıcı" sayısı
        const int ioMs = 250;

        Console.WriteLine($"Senaryo: {requests} eşzamanlı istek, her biri {ioMs} ms'lik bir I/O (veritabanı çağrısı) bekliyor. Havuzda hemen kullanılabilir {min} thread var.\n");

        // BLOKLAYAN: her istek bir thread'i uyutarak bekler (senkron I/O taklidi)
        var sw = Stopwatch.StartNew();
        await Task.WhenAll(Enumerable.Range(0, requests).Select(_ => Task.Run(() => Thread.Sleep(ioMs))));
        var blocking = sw.ElapsedMilliseconds;

        // BLOKLAMAYAN: bekleme sırasında thread serbest
        sw.Restart();
        await Task.WhenAll(Enumerable.Range(0, requests).Select(async _ => await Task.Delay(ioMs)));
        var nonBlocking = sw.ElapsedMilliseconds;

        Console.WriteLine($"   bloklayan  (Thread.Sleep)  : {blocking,5} ms   ← her thread bir istekte takılı; kalanlar kuyrukta sıra bekledi");
        Console.WriteLine($"   bloklamayan (await Delay)  : {nonBlocking,5} ms   ← tüm istekler aynı anda bekledi, thread'ler boştaydı");

        Console.WriteLine($"\n   Beklenen: bloklayan ≈ {Math.Ceiling(requests / (double)min)} dalga × {ioMs} ms (havuz yavaşça thread ekler: ~500 ms'de bir), bloklamayan ≈ {ioMs} ms.");

        Console.WriteLine("""

        Neden web sunucusunda thread bloklamak pahalı?
          • Her thread ~1 MB stack ister; binlerce thread = GB'larca bellek + context switch maliyeti.
          • Havuz, ihtiyaç olunca thread'i YAVAŞ ekler (min sayıdan sonra ~500 ms'de 1). Ani yük artışında bloklayan istekler
            kuyrukta bekler → gecikme patlar → THREAD POOL STARVATION (aç kalma). Sunucu "yavaşladı ama CPU %5" belirtisi verir.
          • async: bekleyen istek thread tutmaz → aynı thread sayısıyla binlerce eşzamanlı istek (→ M4.3 derinlemesine).
        Kural: I/O'yu asla senkron yapma (File.ReadAllText, HttpClient.Send, .Result, .Wait()) — async karşılığını `await` et.
        """);
    }
}
