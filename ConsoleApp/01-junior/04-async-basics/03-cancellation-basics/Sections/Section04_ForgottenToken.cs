using System.Diagnostics;

namespace Junior.AsyncBasics.CancellationBasics;

public static class Section04_ForgottenToken
{
    private const int StageMs = 300;

    // DOĞRU: token her aşamaya geçiriliyor
    private static async Task PipelineAsync(CancellationToken ct)
    {
        await Task.Delay(StageMs, ct);       // aşama 1
        await Task.Delay(StageMs, ct);       // aşama 2
        await Task.Delay(StageMs, ct);       // aşama 3
    }

    // HATALI: token'ı alıyor ama alt çağrılara geçirmeyi UNUTUYOR
    private static async Task ForgetfulPipelineAsync(CancellationToken ct)
    {
        await Task.Delay(StageMs);           // ← ct YOK: iptal duyulmaz
        await Task.Delay(StageMs);
        await Task.Delay(StageMs);
    }

    private static async Task<long> TimeToCancel(Func<CancellationToken, Task> pipeline)
    {
        using var cts = new CancellationTokenSource();
        var sw = Stopwatch.StartNew();
        var task = pipeline(cts.Token);
        await Task.Delay(100);
        cts.Cancel();                                         // 100 ms'de iptal
        try { await task; } catch (OperationCanceledException) { }
        return sw.ElapsedMilliseconds;
    }

    public static async Task Run()
    {
        Console.WriteLine($"3 aşamalı iş ({3 * StageMs} ms). 100 ms'de iptal ediliyor. 'İptalden kaç ms sonra durdu?' ölçülür:\n");

        var good = await TimeToCancel(PipelineAsync);
        Console.WriteLine($"   token alt çağrılara geçiriliyor → {good,4} ms'de durdu     ✓ iptale hemen tepki");

        var bad = await TimeToCancel(ForgetfulPipelineAsync);
        Console.WriteLine($"   token geçirmeyi UNUTMUŞ         → {bad,4} ms'de bitti      ✗ iptal tamamen görmezden gelindi (işin tamamı yapıldı)");

        Console.WriteLine("""

        Token'ı geçirmeyi unutmak sessiz bir hatadır: derleme uyarısı vermez, test geçer, ama kullanıcı iptal edince iş sürer.
        Savunma: CA2016 ("CancellationToken'ı ileri geçir") analyzer kuralı — varsayılan şiddeti düşüktür (öneri); .editorconfig ile warning yapın. Kod incelemesinde 'ct' arayın.

        Token kabul eden tipik API'ler:
          • HttpClient        : GetAsync(url, ct), GetStringAsync(url, ct), SendAsync(request, ct)
          • EF Core           : ToListAsync(ct), FirstOrDefaultAsync(ct), SaveChangesAsync(ct)
          • Stream / File     : ReadAsync(buffer, ct), File.ReadAllTextAsync(path, ct)
          • Task / eşitleme   : Task.Delay(ms, ct), SemaphoreSlim.WaitAsync(ct), Task.Run(work, ct)
          • Channel / akış    : reader.ReadAsync(ct), IAsyncEnumerable.WithCancellation(ct)
        CPU döngüsünde (token alan API yok) kendin kontrol et: ct.ThrowIfCancellationRequested() — her N iterasyonda bir.
        """);
    }
}
