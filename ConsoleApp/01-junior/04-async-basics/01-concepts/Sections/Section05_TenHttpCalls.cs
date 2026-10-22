using System.Collections.Concurrent;
using System.Diagnostics;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.Concepts;

/// <summary>🛠 J4.1: Aynı 10 HTTP çağrısını sıralı, Task.WhenAll ve Parallel.For ile yap; süreyi ve kullanılan thread sayısını karşılaştır.</summary>
public static class Section05_TenHttpCalls
{
    private const int Calls = 10;
    private const int DelayMs = 200;

    public static async Task Run()
    {
        await using var api = await LocalApi.StartAsync();
        using var client = api.CreateClient();
        await client.GetStringAsync("/delay/1");                                  // ısınma: bağlantı + JIT maliyetini ölçümden çıkar
        Console.WriteLine($"Yerel API: {api.BaseUrl}  ·  {Calls} çağrı × {DelayMs} ms gecikme  ·  {Environment.ProcessorCount} çekirdek\n");

        // 1) SIRALI: her çağrı bir öncekinin bitmesini bekler → toplam ≈ 10 × 200
        var seen = new ConcurrentDictionary<int, bool>();
        var sw = Stopwatch.StartNew();
        for (var i = 0; i < Calls; i++)
        {
            seen[Environment.CurrentManagedThreadId] = true;
            await client.GetStringAsync($"/delay/{DelayMs}");
            seen[Environment.CurrentManagedThreadId] = true;
        }
        Report("1) Sıralı await (for)", sw, seen);

        // 2) Task.WhenAll: 10 çağrı AYNI ANDA başlar, hepsi bitince devam → toplam ≈ 200 (en yavaşı kadar)
        seen.Clear();
        sw.Restart();
        await Task.WhenAll(Enumerable.Range(0, Calls).Select(async _ =>
        {
            seen[Environment.CurrentManagedThreadId] = true;
            await client.GetStringAsync($"/delay/{DelayMs}");
            seen[Environment.CurrentManagedThreadId] = true;
        }));
        Report("2) Task.WhenAll (I/O için doğru araç)", sw, seen);

        // 3) Parallel.For + bloklayan çağrı: Parallel.For CPU işi içindir; çağrıyı BLOKLAYARAK bekler → her thread bir çağrıda takılı
        seen.Clear();
        sw.Restart();
        Parallel.For(0, Calls, _ =>
        {
            seen[Environment.CurrentManagedThreadId] = true;
            client.GetStringAsync($"/delay/{DelayMs}").GetAwaiter().GetResult();    // sync-over-async: thread bloklanır
        });
        Report("3) Parallel.For + .GetResult() (YANLIŞ araç)", sw, seen);

        // 4) Parallel.ForEachAsync (.NET 6+): async gövdeyi destekler, eşzamanlılık sınırı verilebilir
        seen.Clear();
        sw.Restart();
        await Parallel.ForEachAsync(Enumerable.Range(0, Calls), new ParallelOptions { MaxDegreeOfParallelism = 5 }, async (_, ct) =>
        {
            seen[Environment.CurrentManagedThreadId] = true;
            await client.GetStringAsync($"/delay/{DelayMs}", ct);
            seen[Environment.CurrentManagedThreadId] = true;
        });
        Report("4) Parallel.ForEachAsync (maks. 5 eşzamanlı)", sw, seen);

        Console.WriteLine("""

        Sonuç:
          • I/O-bound iş için WhenAll (veya sınırlı eşzamanlılık gerekiyorsa Parallel.ForEachAsync) kullan.
          • Parallel.For/PLINQ CPU-bound içindir; içine bloklayan I/O koymak thread'leri UYUTUR: süre kısa görünse de 10 thread bloklandı (havuz yetmeyince süre uzar, başka istekler aç kalır).
          • WhenAll'da "kaç thread?" sorusu yanlış sorudur: beklerken thread yok; devam kodları havuzdan gelen birkaç thread'de çalışır.
        """);
    }

    private static void Report(string title, Stopwatch sw, ConcurrentDictionary<int, bool> seen) =>
        Console.WriteLine($"{title,-48} {sw.ElapsedMilliseconds,5} ms   farklı thread sayısı: {seen.Count}");
}
