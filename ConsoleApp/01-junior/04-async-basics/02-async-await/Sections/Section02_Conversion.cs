using System.Diagnostics;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.AsyncAwait;

public static class Section02_Conversion
{
    public static async Task Run()
    {
        await using var api = await LocalApi.StartAsync();
        using var client = api.CreateClient();
        var path = Path.Combine(Path.GetTempPath(), $"j4-async-{Environment.ProcessId}.txt");
        await File.WriteAllTextAsync(path, "dosya içeriği");

        try
        {
            await client.GetStringAsync("/delay/1");                                       // ısınma
            Console.WriteLine("Her adım 'UI thread'inde başlatılır; 20 ms'lik bir zamanlayıcı aynı thread'de tık atar. Tık sayısı = arayüzün ne kadar DUYARLI kaldığı.\n");

            var s1 = await UiMeter.MeasureAsync(() => { _ = ConversionSteps.Step1_Sync(client, path); return Task.CompletedTask; });
            Console.WriteLine($"Adım 1 — senkron                : {s1.Ms,4} ms, UI tık sayısı: {s1.Ticks,2}   ← UI thread'i bloklandı (donmuş arayüz)");

            var s2 = await UiMeter.MeasureAsync(() => ConversionSteps.Step2_FakeAsync(client, path));
            Console.WriteLine($"Adım 2 — Task.Run ile sarmak    : {s2.Ms,4} ms, UI tık sayısı: {s2.Ticks,2}   ← UI serbest ama bir havuz thread'i yine bloklandı; süre aynı");

            var s3 = await UiMeter.MeasureAsync(() => ConversionSteps.Step3_AsyncAllTheWay(client, path));
            Console.WriteLine($"Adım 3 — async all the way      : {s3.Ms,4} ms, UI tık sayısı: {s3.Ticks,2}   ← hiçbir thread bloklanmadı + bağımsız çağrılar paralel → daha hızlı");

            Console.WriteLine($"\nİçerik doğrulaması: {await ConversionSteps.Step3_AsyncAllTheWay(client, path)}");
        }
        finally
        {
            File.Delete(path);
        }

        Console.WriteLine("""

        Dönüşüm reçetesi (kod: 02-async-await/Conversion/ConversionSteps.cs):
          1. Bloklayan API'yi async karşılığıyla değiştir: File.ReadAllText → ReadAllTextAsync, HttpClient.Send/.Result → await GetStringAsync.
          2. Metodu async yap, dönüş tipini Task/Task<T> yap, adına Async ekle.
          3. ÇAĞIRANLARI da await et → zincir yukarı doğru uzar: ASYNC ALL THE WAY (giriş noktasına kadar: controller, Main, event handler).
          4. Bağımsız işleri await etmeden başlat, Task.WhenAll ile birlikte bekle.
          5. CancellationToken ekle ve aşağı geçir (→ J4.3).
        Karıştırma: senkron kodun ortasında .Result/.Wait() (deadlock/starvation, → 4. bölüm) ve async kodun ortasında bloklayan çağrı.
        """);
    }
}
