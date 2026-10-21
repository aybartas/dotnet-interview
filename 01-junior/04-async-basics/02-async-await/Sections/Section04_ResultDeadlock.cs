using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.AsyncAwait;

public static class Section04_ResultDeadlock
{
    private static async Task<string> GetDataAsync()
    {
        await Task.Delay(100);                                    // devam kodu YAKALANAN context'e (UI thread'ine) geri gönderilmek ister
        return "veri";
    }

    private static async Task<string> GetDataNoContextAsync()
    {
        await Task.Delay(100).ConfigureAwait(false);              // "context'e dönmek zorunda değilim" → havuz thread'inde devam eder
        return "veri";
    }

    private static async Task Show(string title, Task<string> task, int timeoutMs = 1500)
    {
        var winner = await Task.WhenAny(task, Task.Delay(timeoutMs));
        Console.WriteLine(winner == task
            ? $"   {title,-44} → TAMAMLANDI: {await task}"
            : $"   {title,-44} → {timeoutMs} ms içinde BİTMEDİ: DEADLOCK ☠");
    }

    public static async Task Run()
    {
        Console.WriteLine("Sahne: tek thread'li 'UI' bağlamı (WinForms/WPF/eski ASP.NET gibi). await sonrası devam kodu bu thread'e GERİ gönderilir.\n");

        // 1) Klasik deadlock: UI thread'i .Result ile bloklanır; devam kodu UI thread'ine dönmek ister ama thread bloklu → kimse kimseyi bekletmez
        var ui1 = new SingleThreadContext("UI-1");
        await Show("1) UI thread'inde GetDataAsync().Result", ui1.InvokeAsync(() => Task.FromResult(GetDataAsync().Result)));

        // 2) ConfigureAwait(false) ile kütüphane kodu context'e dönmez → .Result bitebilir (ama thread yine bloklandı: çözüm DEĞİL, kırılgan)
        var ui2 = new SingleThreadContext("UI-2");
        await Show("2) .Result + ConfigureAwait(false)", ui2.InvokeAsync(() => Task.FromResult(GetDataNoContextAsync().Result)));

        // 3) Doğru çözüm: await
        var ui3 = new SingleThreadContext("UI-3");
        await Show("3) await GetDataAsync()  (async all the way)", ui3.InvokeAsync(async () => await GetDataAsync()));

        // 4) Hata sarmalama farkı
        Console.WriteLine("\nHata şekli: .Result/.Wait() hatayı AggregateException içine sarar; GetAwaiter().GetResult() ve await orijinal exception'ı verir:");
        static async Task<int> Boom() { await Task.Yield(); throw new InvalidOperationException("özgün hata"); }
        try { _ = Boom().Result; } catch (Exception ex) { Console.WriteLine($"   .Result                  → {ex.GetType().Name} → iç: {ex.InnerException?.GetType().Name}"); }
        try { _ = Boom().GetAwaiter().GetResult(); } catch (Exception ex) { Console.WriteLine($"   GetAwaiter().GetResult() → {ex.GetType().Name}"); }

        Console.WriteLine("""

        ⭐ .Result / .Wait() neden tehlikeli?
          1) DEADLOCK: SynchronizationContext olan ortamlarda (UI, klasik ASP.NET) yukarıdaki durum. ASP.NET Core'da context YOKTUR → bu deadlock olmaz...
          2) ...AMA thread pool STARVATION olur: her istek bir havuz thread'ini bloklar; yük arttıkça havuz yetişemez, sunucu kilitlenir (→ 4.1 Bölüm 4, M4.3).
          3) Hata sarmalama (AggregateException) ve zayıf iptal/zaman aşımı yönetimi.
        Kural: ASYNC ALL THE WAY. Senkron bir giriş noktası zorunluysa (nadir): tek yerde GetAwaiter().GetResult() ve bunun neden güvenli olduğunu yorumla.
        Not: Konsol Main'i async Task Main olabilir; ASP.NET Core controller/endpoint'leri async olabilir → çoğu zaman bloklamak ZORUNDA değilsiniz.
        """);
    }
}
