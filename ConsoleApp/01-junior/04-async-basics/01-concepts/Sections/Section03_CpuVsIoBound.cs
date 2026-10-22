using System.Diagnostics;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.Concepts;

public static class Section03_CpuVsIoBound
{
#pragma warning disable CS1998 // Bu "async" metotta await yok — bilerek: async anahtar kelimesi işi başka thread'e TAŞIMAZ
    private static async Task<int> FakeAsyncCpu(int size) => Cpu.CountPrimes(size);
#pragma warning restore CS1998

    public static async Task Run()
    {
        // ── CPU-bound: işlemci çalışır; çözüm = daha çok çekirdek (Task.Run / Parallel) ──
        const int size = 200_000;
        var cores = Environment.ProcessorCount;

        var sw = Stopwatch.StartNew();
        for (var i = 0; i < cores; i++) Cpu.CountPrimes(size);
        var sequential = sw.ElapsedMilliseconds;

        sw.Restart();
        await Task.WhenAll(Enumerable.Range(0, cores).Select(_ => Task.Run(() => Cpu.CountPrimes(size))));
        var parallel = sw.ElapsedMilliseconds;
        Console.WriteLine($"CPU-bound ({cores} iş, asal sayma): sıralı {sequential} ms → Task.Run + WhenAll {parallel} ms   (işlemci meşgul; çekirdek eklemek hızlandırır)");

        // ── I/O-bound: iş BEKLEMEKten ibaret (ağ, disk, veritabanı); işlemci boş; çözüm = thread tutmadan beklemek (async) ──
        var threadsBefore = ThreadPool.ThreadCount;
        sw.Restart();
        await Task.WhenAll(Enumerable.Range(0, 200).Select(_ => Task.Delay(300)));      // 200 "ağ çağrısı"
        Console.WriteLine($"I/O-bound (200 eşzamanlı 300 ms bekleme): {sw.ElapsedMilliseconds} ms toplam, thread sayısı {threadsBefore} → {ThreadPool.ThreadCount}");
        Console.WriteLine("   (sıralı bekleseydik 60 sn; 200 thread açsaydık ~200 MB stack — async: thread yok, sadece bekleyen task nesneleri)");

        // ── Yanılgı: "async yaparsam CPU işi hızlanır" ──
        sw.Restart();
        var task = FakeAsyncCpu(size * 2);                  // await YOK: metot çağıran thread'de SENKRON çalışır
        var returnedAfter = sw.ElapsedMilliseconds;
        await task;

        sw.Restart();
        var offloaded = Task.Run(() => Cpu.CountPrimes(size * 2));
        var offloadReturnedAfter = sw.ElapsedMilliseconds;
        await offloaded;

        Console.WriteLine($"\nYanılgı: 'async' CPU işini arka plana almaz. async metot çağrısı {returnedAfter} ms sonra döndü (çağıran bloklandı); Task.Run {offloadReturnedAfter} ms'de döndü.");

        Console.WriteLine("""

        Hangi iş, hangi araç?
        ┌────────────────┬───────────────────────────────┬──────────────────────────────────────────────┐
        │ İş türü        │ Darboğaz                      │ Araç                                         │
        ├────────────────┼───────────────────────────────┼──────────────────────────────────────────────┤
        │ I/O-bound      │ ağ, disk, DB (bekleme)        │ async/await (GetStringAsync, ReadAsync…)     │
        │ CPU-bound      │ işlemci (hesap, sıkıştırma)   │ Parallel.For / PLINQ / Task.Run (UI/arka plan)│
        └────────────────┴───────────────────────────────┴──────────────────────────────────────────────┘
        ASP.NET Core'da CPU-bound işi Task.Run ile istek thread'inden başka havuz thread'ine atmak KAZANÇ SAĞLAMAZ
        (aynı havuz); yalnızca arka plan servisine/kuyruğa taşımak (→ M12) anlamlıdır.
        """);
    }
}
