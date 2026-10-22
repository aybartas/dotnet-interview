using System.Diagnostics;

namespace Junior.AsyncBasics.AsyncAwait;

public static class Section05_WhenAllAny
{
    private static async Task<int> Ok(string name, int ms, int value)
    {
        await Task.Delay(ms);
        Console.WriteLine($"   [{name}] bitti");
        return value;
    }

    private static async Task<int> Fail(string name, int ms)
    {
        await Task.Delay(ms);
        Console.WriteLine($"   [{name}] HATA fırlatıyor");
        throw new InvalidOperationException($"{name} hatası");
    }

    public static async Task Run()
    {
        // 1) Başarı: WhenAll sonuçları dizi olarak verir, süre = en yavaş
        var sw = Stopwatch.StartNew();
        int[] values = await Task.WhenAll(Ok("A", 100, 1), Ok("B", 200, 2), Ok("C", 150, 3));
        Console.WriteLine($"1) WhenAll sonucu: [{string.Join(", ", values)}]  süre ≈ {sw.ElapsedMilliseconds} ms (en yavaşı kadar; sıralı 450)\n");

        // 2) Hata: await yalnızca İLK exception'ı fırlatır; hepsi Task.Exception.InnerExceptions içindedir
        Console.WriteLine("2) İki task hata veriyor, biri başarılı:");
        var all = Task.WhenAll(Ok("A", 100, 1), Fail("B", 50), Fail("C", 150));
        try
        {
            await all;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"   await'in fırlattığı: {ex.GetType().Name}: {ex.Message}   (yalnızca İLK hata!)");
        }
        Console.WriteLine($"   all.Exception.InnerExceptions: {all.Exception!.InnerExceptions.Count} hata → {string.Join(" · ", all.Exception.InnerExceptions.Select(e => e.Message))}");
        Console.WriteLine($"   all.IsFaulted = {all.IsFaulted}; WhenAll DİĞER task'ları İPTAL ETMEZ — hepsi bitene kadar bekler (A de tamamlandı).\n");

        // 3) WhenAny: ilk biteni al; zaman aşımı kalıbı
        var work = Ok("yavaş iş", 500, 99);
        var first = await Task.WhenAny(work, Task.Delay(150));
        Console.WriteLine($"3) WhenAny zaman aşımı kalıbı: iş mi bitti? {first == work}  → zaman aşımı kazandı; ama 'yavaş iş' ARKADA ÇALIŞMAYA DEVAM EDER (iptal için CancellationToken gerekir, → J4.3)");
        await work;                                                                      // log karışmasın diye bekle

        // 4) Daha temiz zaman aşımı: WaitAsync (.NET 6+)
        try { await Ok("yavaş iş 2", 400, 1).WaitAsync(TimeSpan.FromMilliseconds(100)); }
        catch (TimeoutException) { Console.WriteLine("4) task.WaitAsync(timeout) → TimeoutException (WhenAny + Delay kalıbından okunaklı)\n"); }
        await Task.Delay(400);

        // 5) WhenEach (.NET 9): task'ları BİTİŞ SIRASINA göre işle
        Console.WriteLine("5) Task.WhenEach (.NET 9): bitiş sırasıyla işle");
        Task<int>[] tasks = [Ok("X", 300, 10), Ok("Y", 100, 20), Ok("Z", 200, 30)];
        await foreach (var done in Task.WhenEach(tasks))
            Console.WriteLine($"   → sonuç geldi: {await done}");

        Console.WriteLine("""

        Hatırlatma:
          • await Task.WhenAll(...) → try/catch tek (ilk) exception'ı görür; hepsini loglamak için Task.Exception.InnerExceptions'a bak.
          • WhenAll'a verilen task'lar ZATEN çalışıyordur (hot); WhenAll sadece bekler. Task'ları await etmeden başlatıp sonra WhenAll yap.
          • Çok sayıda task'ta eşzamanlılığı sınırla: Parallel.ForEachAsync veya SemaphoreSlim (sınırsız WhenAll = sunucuya 10.000 çağrı).
        """);
    }
}
