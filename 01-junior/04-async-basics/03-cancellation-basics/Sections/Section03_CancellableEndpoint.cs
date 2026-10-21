using System.Collections.Concurrent;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.CancellationBasics;

/// <summary>
/// 🛠 J4.3: Uzun süren bir rapor üretimini iptal edilebilir yapan API endpoint'i; istemci bağlantıyı kestiğinde işin durduğunu gösterir.
/// Aynı rapor iki endpoint'te: biri RequestAborted token'ını izler, diğeri izlemez.
/// </summary>
public static class Section03_CancellableEndpoint
{
    private const int Steps = 10, StepMs = 150;

    public static async Task Run()
    {
        var log = new ConcurrentQueue<string>();

        async Task<string> GenerateReportAsync(string endpoint, CancellationToken ct)
        {
            for (var step = 1; step <= Steps; step++)
            {
                log.Enqueue($"[{endpoint}] adım {step}/{Steps}");
                await Task.Delay(StepMs, ct);                  // her adımda token'ı gözle: iptal edilirse OperationCanceledException
            }
            log.Enqueue($"[{endpoint}] rapor TAMAMLANDI (kimse beklemiyor olsa bile)");
            return "rapor hazır";
        }

        await using var api = await LocalApi.StartAsync(app =>
        {
            // Minimal API: 'CancellationToken' parametresi otomatik olarak HttpContext.RequestAborted'a bağlanır (istemci kopunca iptal olur).
            app.MapGet("/report", async (CancellationToken requestAborted) =>
            {
                try { return await GenerateReportAsync("/report", requestAborted); }
                catch (OperationCanceledException)
                {
                    log.Enqueue("[/report] İSTEMCİ KOPTU → RequestAborted tetiklendi, rapor üretimi DURDU");
                    throw;
                }
            });

            // Token'ı geçirmeyen sürüm: istemci gitse de sunucu işi sonuna kadar yapar (boşa harcanan CPU/DB)
            app.MapGet("/report-ignores-token", (CancellationToken requestAborted) =>
                GenerateReportAsync("/report-ignores-token", CancellationToken.None));
        });
        using var client = api.CreateClient();

        Console.WriteLine($"Rapor: {Steps} adım × {StepMs} ms = {Steps * StepMs} ms. İstemci 400 ms sonra vazgeçip bağlantıyı kesiyor.\n");

        foreach (var endpoint in new[] { "/report", "/report-ignores-token" })
        {
            using var cts = new CancellationTokenSource(400);
            try { await client.GetStringAsync(endpoint, cts.Token); }
            catch (OperationCanceledException) { Console.WriteLine($"İSTEMCİ: {endpoint} → iptal edildi (bağlantı kesildi)"); }
        }

        await Task.Delay(Steps * StepMs + 300);                // sunucunun ne yaptığını görmek için işin normal süresi kadar bekle

        Console.WriteLine("\nSUNUCU GÜNLÜĞÜ:");
        foreach (var line in log) Console.WriteLine($"   {line}");

        Console.WriteLine("""

        Gözlem:
          • /report            : istemci gidince Kestrel HttpContext.RequestAborted'ı iptal eder → döngü ~3. adımda durur (kaynak boşa gitmez).
          • /report-ignores-token : istemci çoktan gitti ama sunucu 10 adımı bitirdi — her terk edilen istek sunucuya tam maliyetli kalır.
        ASP.NET Core'da:
          • Endpoint/controller action parametresi olarak CancellationToken al → framework RequestAborted'ı bağlar.
          • İsteğe bağlı: HttpContext.RequestAborted'a doğrudan da erişilir.
          • Token'ı veritabanına (EF Core: ToListAsync(ct)), HttpClient'a ve diğer I/O'ya geçir (bkz. 4. bölüm).
        Kanıt: iptal edilen istekte sunucu günlüğü "DURDU" satırıyla biter; token'ı izlemeyen sürümde işin tamamı yapılır.
        """);
    }
}
