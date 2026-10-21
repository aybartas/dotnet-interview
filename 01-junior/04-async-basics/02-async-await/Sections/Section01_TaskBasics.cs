using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.AsyncAwait;

public static class Section01_TaskBasics
{
    // async metodun imzası: dönüş tipi Task (değer yok) / Task<T> (değer var) / ValueTask<T> (sık senkron biten sıcak yollar)
    private static async Task<int> ComputeAsync(int x)
    {
        Console.WriteLine($"   [ComputeAsync] başladı ({Cpu.Tid}) — ilk await'e kadar çağıranın thread'inde SENKRON çalışır");
        await Task.Delay(100);
        Console.WriteLine($"   [ComputeAsync] await sonrası devam ediyor ({Cpu.Tid}) — konsolda SynchronizationContext yok → havuzdan herhangi bir thread");
        return x * 2;
    }

    private static async Task DoWorkAsync() => await Task.Delay(50);                 // Task: değer dönmez (void'in async karşılığı)

    private static Task<int> PassThrough(int x) => ComputeAsync(x);                  // async/await'siz: doğrudan Task döndür (sadece geçiş yapıyorsan)

    public static async Task Run()
    {
        Console.WriteLine($"1) Çağıran thread: {Cpu.Tid}");
        var task = ComputeAsync(21);                                                 // await YOK: sadece başlattık (task "sıcak" — zaten çalışıyor)
        Console.WriteLine($"   ComputeAsync döndü ama bitmedi: IsCompleted={task.IsCompleted}  (Task bir SÖZDÜR; sonuç henüz yok)");
        var result = await task;                                                     // await: sonucu bekle (thread'i BLOKLAMADAN)
        Console.WriteLine($"   await sonucu = {result}\n");

        Console.WriteLine("2) await ne yapar?");
        Console.WriteLine("   • Task zaten bittiyse: devam eder (senkron, ek maliyet yok).");
        Console.WriteLine("   • Bitmediyse: metodun GERİ KALANINI 'devam kodu' (continuation) olarak kaydeder ve çağırana HEMEN döner. Thread serbest kalır.");
        Console.WriteLine("   • Task bitince devam kodu çalışır (UI/eski ASP.NET'te aynı context'e geri döner; ASP.NET Core/konsolda havuz thread'inde).");
        Console.WriteLine("   Derleyici metodu bir STATE MACHINE'e çevirir (iç yapısı → M4.2).\n");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        await DoWorkAsync();
        Console.WriteLine($"3) async Task (değer yok): {sw.ElapsedMilliseconds} ms sonra tamamlandı");

        Console.WriteLine($"4) async/await'siz geçiş: PassThrough(5) → {await PassThrough(5)}   (yalnızca Task'ı aynen döndüren metotlarda 'async' gereksizdir; ama using/try içindeyse AWAIT ŞART)\n");

        Console.WriteLine("5) 'Task Result' ve 'Task' farkı + hazır task'lar:");
        Task<int> ready = Task.FromResult(7);                                        // thread yok, bellek tahsisi minimum
        Console.WriteLine($"   Task.FromResult(7).IsCompletedSuccessfully = {ready.IsCompletedSuccessfully}; Task.CompletedTask de aynı amaçla (Task döndüren metotta erken çıkış)");

        Console.WriteLine("""

        Kurallar:
          • Asenkron metoda 'Async' son eki ver (GetUserAsync); dönüş tipi Task/Task<T> (ValueTask<T> yalnızca ölçülmüş sıcak yollar için).
          • 'async' bir metodu başka thread'e TAŞIMAZ; işi thread'siz bekleten await'tir.
          • Task'ı await etmeden bırakma (fire-and-forget): hata ve tamamlanma kaybolur (CS4014 uyarısı; analyzer'ı warning/error yap).
        """);
    }
}
