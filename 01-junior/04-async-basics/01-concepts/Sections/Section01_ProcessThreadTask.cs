using System.Diagnostics;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.Concepts;

public static class Section01_ProcessThreadTask
{
    public static async Task Run()
    {
        var p = Process.GetCurrentProcess();
        Console.WriteLine($"PROCESS : Id={p.Id}, ad={p.ProcessName}, işletim sistemi thread sayısı={p.Threads.Count}, mantıksal çekirdek={Environment.ProcessorCount}");
        Console.WriteLine("          → izole bellek alanı; içindeki thread'ler AYNI belleği paylaşır (thread safety sorunu buradan gelir)\n");

        Console.WriteLine($"THREAD  : şu anki thread = {Cpu.Tid} (havuz thread'i mi? {Thread.CurrentThread.IsThreadPoolThread})");
        var dedicated = new Thread(() =>
            Console.WriteLine($"          kendi açtığımız Thread → {Cpu.Tid}, havuz? {Thread.CurrentThread.IsThreadPoolThread}, background? {Thread.CurrentThread.IsBackground}"));
        dedicated.Start();
        dedicated.Join();
        Console.WriteLine("          Thread = işletim sistemi seviyesinde bir yürütme akışı: ~1 MB stack, oluşturması/değiştirilmesi pahalı.\n");

        ThreadPool.GetMinThreads(out var minWorker, out _);
        Console.WriteLine($"THREAD POOL: hazır thread'ler yeniden kullanılır. Şu an {ThreadPool.ThreadCount} thread, en az {minWorker} worker thread hemen açılır.");
        var onPool = await Task.Run(() => $"Task.Run → {Cpu.Tid}, havuz? {Thread.CurrentThread.IsThreadPoolThread}");
        Console.WriteLine($"          {onPool}\n");

        Console.WriteLine("TASK    : bir işin GELECEKTEKİ sonucunu temsil eden nesne (promise). Task ≠ thread:");
        var done = Task.FromResult(42);
        Console.WriteLine($"          Task.FromResult(42) → tamamlandı mı? {done.IsCompleted}, sonuç={done.Result}  (hiçbir thread yok!)");

        var tcs = new TaskCompletionSource<string>();
        var waiting = tcs.Task;
        Console.WriteLine($"          TaskCompletionSource.Task → tamamlandı mı? {waiting.IsCompleted}  (dış bir olayı bekler, thread tutmaz)");
        tcs.SetResult("olay geldi");
        Console.WriteLine($"          SetResult sonrası → {await waiting}");

        var before = ThreadPool.ThreadCount;
        await Task.WhenAll(Enumerable.Range(0, 1000).Select(_ => Task.Delay(300)));
        Console.WriteLine($"          1000 adet Task.Delay(300) birlikte beklendi; thread sayısı {before} → {ThreadPool.ThreadCount} (1000 bekleme için 1000 thread AÇILMADI: bekleyen task thread tutmaz, zamanlayıcı kullanır)");

        Console.WriteLine("""

        Özet:
          • Process  : izole kaynak kabı (bellek, handle'lar). Kapanması = hepsi biter.
          • Thread   : bir process içindeki yürütme akışı; paylaşılan bellekte çalışır; pahalı ve sayısı sınırlı.
          • Task     : "bu iş bir gün bitecek" sözü. Bir thread üzerinde (Task.Run) YA DA hiçbir thread'siz (I/O beklemesi) çalışabilir.
        """);
    }
}
