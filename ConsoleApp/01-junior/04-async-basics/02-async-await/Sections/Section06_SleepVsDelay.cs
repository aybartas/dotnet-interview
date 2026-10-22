using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.AsyncAwait;

public static class Section06_SleepVsDelay
{
    public static async Task Run()
    {
        Console.WriteLine("Aynı 'UI thread'inde 400 ms bekleme: Thread.Sleep ile await Task.Delay karşılaştırması (tık = arayüz duyarlılığı).\n");

        var sleep = await UiMeter.MeasureAsync(() => { Thread.Sleep(400); return Task.CompletedTask; });
        Console.WriteLine($"Thread.Sleep(400)        : {sleep.Ms,4} ms, UI tık sayısı: {sleep.Ticks,2}   ← thread UYUDU: hiçbir şey çalışamadı (donmuş arayüz / boşa harcanan havuz thread'i)");

        var delay = await UiMeter.MeasureAsync(() => Task.Delay(400));
        Console.WriteLine($"await Task.Delay(400)    : {delay.Ms,4} ms, UI tık sayısı: {delay.Ticks,2}   ← thread SERBEST: bekleme zamanlayıcıyla yapıldı");

        Console.WriteLine("""

        • Thread.Sleep: bulunduğu thread'i bloklar. async metotta kullanmak, async'in tüm amacını bozar.
        • await Task.Delay: bir zamanlayıcı kurar, thread'i serbest bırakır, süre dolunca devam kodunu çalıştırır.
        • Kullanım: "bekle" ihtiyacı (retry arası, polling, simülasyon) → await Task.Delay. Test için gerçek beklemek yerine TimeProvider/fake zaman (→ M10).
        • Task.Delay(Timeout.Infinite, token) ile "iptal edilene kadar bekle" da yazılır.
        """);
    }
}
