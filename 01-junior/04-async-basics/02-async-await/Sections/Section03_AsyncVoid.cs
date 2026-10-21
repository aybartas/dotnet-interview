using System.Diagnostics;

namespace Junior.AsyncBasics.AsyncAwait;

/// <summary>🛠 J4.2: async void içindeki exception'ın uygulamayı nasıl çökerttiğini gösteren demo (çökme ayrı bir alt process'te yaşanır).</summary>
public static class Section03_AsyncVoid
{
    private static async Task FailsAsync()
    {
        await Task.Delay(50);
        throw new InvalidOperationException("async Task içinden fırlatıldı");
    }

    private static async void FailsVoid()                       // ⚠ async void: çağıran bir Task ALAMAZ → hatayı yakalamanın yolu yok
    {
        await Task.Delay(100);
        throw new InvalidOperationException("async void içinden fırlatıldı");
    }

    /// <summary>Alt process'in giriş noktası: async void'in hatası try/catch'e takılmaz ve process'i çökertir.</summary>
    public static async Task<int> RunChildAsync()
    {
        Console.WriteLine("[alt process] try/catch içinde async void metodu çağrılıyor...");
        try
        {
            FailsVoid();
            Console.WriteLine("[alt process] çağrı hatasız DÖNDÜ (exception henüz fırlatılmadı: await'ten sonra, başka bir thread'de fırlayacak)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[alt process] YAKALANDI: {ex.Message}");        // asla çalışmaz
        }

        await Task.Delay(1000);
        Console.WriteLine("[alt process] BU SATIRA ULAŞILMAZ — process çoktan çöktü");
        return 0;
    }

    public static async Task Run()
    {
        // 1) async Task: hata Task'ın içinde taşınır; await eden yakalar
        try
        {
            await FailsAsync();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"1) async Task  → await sırasında YAKALANDI: {ex.Message}\n");
        }

        // 2) async void: hata SynchronizationContext'e/ThreadPool'a fırlatılır → yakalanmamış exception → process ölür
        Console.WriteLine("2) async void  → aynı hata bu kez ayrı bir process'te çalıştırılıyor (çökmesi bu demoyu öldürmesin):\n");

        var psi = new ProcessStartInfo(Environment.ProcessPath!)
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        if (Path.GetFileNameWithoutExtension(Environment.ProcessPath!).Equals("dotnet", StringComparison.OrdinalIgnoreCase))
            psi.ArgumentList.Add(typeof(Section03_AsyncVoid).Assembly.Location);          // dotnet <dll> ... ile çalıştırıldıysa dll yolu gerekir
        psi.ArgumentList.Add("4.2");
        psi.ArgumentList.Add("async-void-child");

        using var child = Process.Start(psi)!;
        var stdout = child.StandardOutput.ReadToEndAsync();
        var stderr = child.StandardError.ReadToEndAsync();
        if (!child.WaitForExit(15_000)) { child.Kill(); Console.WriteLine("   (alt process zaman aşımına uğradı)"); }

        foreach (var line in (await stdout).Split('\n', StringSplitOptions.RemoveEmptyEntries)) Console.WriteLine($"   {line.TrimEnd()}");
        foreach (var line in (await stderr).Split('\n', StringSplitOptions.RemoveEmptyEntries).Take(2)) Console.WriteLine($"   stderr> {line.TrimEnd()}");
        Console.WriteLine($"   alt process çıkış kodu: {child.ExitCode}   (0 değil: process ÇÖKTÜ)\n");

        Console.WriteLine("""
        Neden?
          • async void metodun çağıranı bir Task alamaz → ne await edebilir ne try/catch ile yakalayabilir; tamamlanmasını da izleyemez.
          • Exception, await'ten sonra (başka bir thread'de) fırlar; yakalayan olmadığı için "unhandled exception" → .NET Core'da process kapanır.
          • UI uygulamalarında hata SynchronizationContext'e gönderilir → uygulama çöker. ASP.NET Core'da: tüm sunucu process'i ölür.
          • Test edilemez (xUnit/NUnit bitişini bekleyemez).

        Tek meşru yer: EVENT HANDLER (imza void zorunlu). O zaman gövde try/catch içine alınır:
            button.Click += async (s, e) => { try { await SaveAsync(); } catch (Exception ex) { ShowError(ex); } };
        Diğer her yerde: async Task. Fire-and-forget gerekiyorsa: hatayı loglayan sarmalayıcı veya arka plan kuyruğu (→ M12).
        Analyzer: VSTHRD100 (Microsoft.VisualStudio.Threading.Analyzers paketi) async void metotları işaretler — warning/error yapın.
        """);
    }
}
