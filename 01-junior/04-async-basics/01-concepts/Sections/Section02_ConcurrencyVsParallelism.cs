using System.Collections.Concurrent;
using System.Diagnostics;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.Concepts;

public static class Section02_ConcurrencyVsParallelism
{
    public static async Task Run()
    {
        Console.WriteLine("CONCURRENCY = aynı anda BİRDEN ÇOK işin ilerleme halinde olması (sırayla dönüşümlü olabilir; tek thread yeter).");
        Console.WriteLine("PARALLELISM = birden çok işin GERÇEKTEN aynı anda çalışması (birden çok çekirdek gerekir).\n");

        // ── 1) Concurrency, parallelism OLMADAN: tek thread'li bağlamda 3 aşçı dönüşümlü çalışır ──
        using var ui = new SingleThreadContext("tek-thread");
        var log = new ConcurrentQueue<(long Ms, string Text)>();
        var sw = Stopwatch.StartNew();

        async Task Cook(string name)
        {
            for (var step = 1; step <= 3; step++)
            {
                log.Enqueue((sw.ElapsedMilliseconds, $"{name} adım {step} ({Cpu.Tid})"));
                await Task.Delay(100);                         // beklerken thread SERBEST kalır; diğer aşçı ilerler
            }
        }

        await ui.InvokeAsync(() => Task.WhenAll(Cook("A"), Cook("B"), Cook("C")));
        Console.WriteLine($"1) Tek thread'de 3 asenkron iş (ardışık yapılsa ~900 ms, burada {sw.ElapsedMilliseconds} ms):");
        foreach (var (ms, text) in log.OrderBy(l => l.Ms)) Console.WriteLine($"   {ms,4} ms  {text}");
        Console.WriteLine($"   → hepsi aynı thread'de ({ui.ThreadId}): CONCURRENCY var, PARALLELISM yok.\n");

        // ── 2) Parallelism: CPU işi birden çok çekirdeğe bölünür ──
        const int jobs = 8, size = 150_000;
        var seq = Stopwatch.StartNew();
        for (var i = 0; i < jobs; i++) Cpu.CountPrimes(size);
        seq.Stop();

        var threads = new ConcurrentDictionary<int, bool>();
        var par = Stopwatch.StartNew();
        Parallel.For(0, jobs, _ =>
        {
            threads[Environment.CurrentManagedThreadId] = true;
            Cpu.CountPrimes(size);
        });
        par.Stop();

        Console.WriteLine($"2) {jobs} adet CPU işi ({Environment.ProcessorCount} çekirdek): sıralı {seq.ElapsedMilliseconds} ms · Parallel.For {par.ElapsedMilliseconds} ms ({threads.Count} farklı thread)");
        Console.WriteLine("   → PARALLELISM: gerçekten aynı anda çalıştı. Kazanç çekirdek sayısıyla sınırlı; tek çekirdekte kazanç yoktur.");

        Console.WriteLine("""

        ⭐ Mülakat cümlesi:
          "Concurrency işleri YÖNETMEKle (aynı anda birçok iş ilerleyebilir), parallelism işleri AYNI ANDA ÇALIŞTIRMAKla ilgilidir.
           async/await I/O için concurrency sağlar (thread tutmadan bekler); Parallel.For/PLINQ CPU işi için parallelism sağlar."
        """);
    }
}
