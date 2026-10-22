using System.Diagnostics;
using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.CancellationBasics;

public static class Section02_Conventions
{
    // KONVANSİYON: CancellationToken SON parametredir ve çoğu zaman 'default' varsayılanı vardır; ismi 'cancellationToken' ya da 'ct'
    private static async Task<string> LoadReportAsync(string name, int pageSize = 100, CancellationToken cancellationToken = default)
    {
        await Task.Delay(300, cancellationToken);              // aşağı doğru GEÇİR: token'ı alan her çağrıya ver
        return $"{name}:{pageSize}";
    }

    public static async Task Run()
    {
        Console.WriteLine("1) İmza konvansiyonu: token SON parametre, isteğe bağlı:");
        Console.WriteLine("   Task<string> LoadReportAsync(string name, int pageSize = 100, CancellationToken cancellationToken = default)");
        Console.WriteLine($"   çağrı: await LoadReportAsync(\"satis\") → {await LoadReportAsync("satis")}\n");

        // 2) CancelAfter: zaman aşımı
        Console.WriteLine("2) CancelAfter(timeout): bir süre sonra otomatik iptal:");
        using (var timeout = new CancellationTokenSource())
        {
            timeout.CancelAfter(100);
            var sw = Stopwatch.StartNew();
            try { await LoadReportAsync("yavaş", cancellationToken: timeout.Token); }
            catch (OperationCanceledException ex) { Console.WriteLine($"   {sw.ElapsedMilliseconds} ms sonra {ex.GetType().Name} (iş 300 ms sürecekti)"); }
        }

        // 3) OperationCanceledException iyi yakalanır: iptal HATA değildir
        Console.WriteLine("\n3) OperationCanceledException'ı doğru yakalamak:");
        using var userCts = new CancellationTokenSource();
        userCts.Cancel();
        try { await LoadReportAsync("x", cancellationToken: userCts.Token); }
        catch (OperationCanceledException) when (userCts.IsCancellationRequested)
        {
            Console.WriteLine("   when (token.IsCancellationRequested) → bu bizim istediğimiz iptal: logla (Information), HATA olarak raporlama, devam etme");
        }
        Console.WriteLine("   (Filtre olmazsa başka kaynaklı iptaller — ör. HttpClient zaman aşımı — de yutulur.)");

        // 4) Kullanıcı iptali + zaman aşımı birlikte: linked token
        Console.WriteLine("\n4) Linked token: kullanıcı iptali + zaman aşımı birleşik:");
        using var user = new CancellationTokenSource();
        using var limit = new CancellationTokenSource(TimeSpan.FromMilliseconds(80));
        using var linked = CancellationTokenSource.CreateLinkedTokenSource(user.Token, limit.Token);
        try { await LoadReportAsync("birleşik", cancellationToken: linked.Token); }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"   iptal edildi. Kullanıcı mı? {user.IsCancellationRequested}  Zaman aşımı mı? {limit.IsCancellationRequested}  → nedeni ayırt edebilirsin (408/504 vs sessiz çıkış)");
        }

        // 5) HttpClient: iptal vs zaman aşımı
        Console.WriteLine("\n5) HttpClient'ta iptal vs zaman aşımı (ikisi de TaskCanceledException — OperationCanceledException'ın alt sınıfı):");
        await using var api = await LocalApi.StartAsync();
        using var client = api.CreateClient();

        using var cancel = new CancellationTokenSource(100);
        try { await client.GetStringAsync("/delay/2000", cancel.Token); }
        catch (OperationCanceledException ex) { Console.WriteLine($"   token iptali      → {ex.GetType().Name}, iç exception: {ex.InnerException?.GetType().Name ?? "yok"}"); }

        using var timeoutClient = new HttpClient { BaseAddress = new Uri(api.BaseUrl), Timeout = TimeSpan.FromMilliseconds(100) };
        try { await timeoutClient.GetStringAsync("/delay/2000"); }
        catch (OperationCanceledException ex) { Console.WriteLine($"   HttpClient.Timeout → {ex.GetType().Name}, iç exception: {ex.InnerException?.GetType().Name ?? "yok"}  ← TimeoutException ile ayırt edilir"); }

        Console.WriteLine("""

        Kurallar:
          • CancellationToken'ı metot imzasında SON parametre yap; ctor/özellik olarak SAKLAMA (istek başına yaşar).
          • İptal edilebilir her async metot token alsın ve bunu çağırdığı HER async API'ye geçirsin.
          • OperationCanceledException iptal edilen işi temsil eder → hata logu/500 üretme; yakalarsan when (token.IsCancellationRequested) ile sınırla.
          • İptal sonrası kısmi durumu temizle/geri al (yarım yazılmış dosya, açık transaction).
        """);
    }
}
