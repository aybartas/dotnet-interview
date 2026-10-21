using System.Diagnostics;

namespace Junior.AsyncBasics.CancellationBasics;

public static class Section01_CooperativeModel
{
    // Token'ı İZLEYEN işçi: her adımda iptal edilip edilmediğine bakar
    private static async Task<int> CooperativeWorker(CancellationToken ct)
    {
        var steps = 0;
        for (var i = 0; i < 20; i++)
        {
            ct.ThrowIfCancellationRequested();            // iptal istendiyse OperationCanceledException fırlat
            await Task.Delay(50);                         // (token geçmiyoruz; kontrol yukarıda, adım başında)
            steps++;
        }
        return steps;
    }

    // Token'ı SAYMAYAN işçi: iptal istense de işi bitirir
    private static async Task<int> StubbornWorker(CancellationToken ct)
    {
        var steps = 0;
        for (var i = 0; i < 20; i++)
        {
            await Task.Delay(50);
            steps++;                                      // ct hiç kontrol edilmiyor
        }
        return steps;
    }

    public static async Task Run()
    {
        Console.WriteLine("İptal bir KOOPERATİF sözleşmedir: Cancel() yalnızca 'iptal istendi' bayrağını kaldırır; işi KİMSE zorla durdurmaz.");
        Console.WriteLine("Token'ı izleyen kod durur; izlemeyen kod çalışmaya devam eder. (Thread.Abort yoktur — .NET Core'da desteklenmez.)\n");

        using var cts = new CancellationTokenSource();
        cts.Token.Register(() => Console.WriteLine("   [callback] iptal istendi — kayıtlı geri çağrılar Cancel() sırasında çalışır (kaynak temizliği için)"));

        var sw = Stopwatch.StartNew();
        var cooperative = CooperativeWorker(cts.Token);
        var stubborn = StubbornWorker(cts.Token);

        await Task.Delay(200);
        Console.WriteLine($"{sw.ElapsedMilliseconds} ms: Cancel() çağrılıyor (iki işçi de ~4 adım yapmış durumda)...");
        cts.Cancel();

        try { await cooperative; }
        catch (OperationCanceledException) { Console.WriteLine($"{sw.ElapsedMilliseconds,4} ms: kooperatif işçi DURDU  → Task.IsCanceled = {cooperative.IsCanceled}"); }

        Console.WriteLine($"{sw.ElapsedMilliseconds,4} ms: inatçı işçi hâlâ çalışıyor mu? {!stubborn.IsCompleted}");
        var steps = await stubborn;
        Console.WriteLine($"{sw.ElapsedMilliseconds,4} ms: inatçı işçi bitti — {steps} adımın HEPSİNİ yaptı (iptali görmezden geldi)\n");

        Console.WriteLine("Bayrak okuma yolları:");
        Console.WriteLine($"   token.IsCancellationRequested = {cts.Token.IsCancellationRequested}   → bayrağa bak, kendi karar ver (ör. temizlik yapıp return)");
        try { cts.Token.ThrowIfCancellationRequested(); }
        catch (OperationCanceledException ex) { Console.WriteLine($"   token.ThrowIfCancellationRequested() → {ex.GetType().Name}   → standart yol: işi kes, yukarı bildir"); }

        Console.WriteLine("""

        Model:
          • CancellationTokenSource (CTS) iptali TETİKLER (sahibi: isteği başlatan taraf); CancellationToken iptali GÖZLER (alan taraf, struct).
          • Bir CTS TEK KULLANIMLIDIR: iptal edilen CTS sıfırlanamaz; yeni iş = yeni CTS. CTS IDisposable'dır (zamanlayıcı/kayıt tutar) → using.
          • CancellationToken.None: "iptal edilemez" — token parametresi isteğe bağlı olsun diye varsayılan değer.
          • token.Register(callback): iptal anında çalışacak temizlik; kayıt dönen nesnesi dispose edilerek kaldırılır.
          • İptal edilebilir bir async API Task'ı Canceled durumuna getirir; await eden OperationCanceledException görür (hata DEĞİL).
        """);
    }
}
