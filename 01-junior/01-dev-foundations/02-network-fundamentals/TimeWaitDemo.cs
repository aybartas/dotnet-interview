using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Junior.DevFoundations.NetworkFundamentals;

/// <summary>
/// Çok sayıda KISA ÖMÜRLÜ TCP bağlantısı açıp kapatır ve işletim sisteminin TIME_WAIT sayacının büyümesini gösterir.
///
/// Neden? Bağlantıyı İLK kapatan taraf (active close) TIME_WAIT durumuna girer ve ~2×MSL (Linux'ta 60 sn) boyunca
/// o (yerel IP, yerel port, uzak IP, uzak port) dörtlüsünü tutar — gecikmiş paketlerin yeni bağlantıya karışmaması için.
/// İstemci her istekte yeni bağlantı açıp ilk o kapatırsa, TIME_WAIT soketleri birikir ve EPHEMERAL PORT havuzu tükenir:
///     "Cannot assign requested address" / SocketException (AddressNotAvailable)
/// .NET'te klasik karşılığı: her istekte `new HttpClient()` → "HttpClient socket exhaustion" (→ J7.5, M5.9).
/// Çözüm: bağlantıyı yeniden kullan (keep-alive / connection pooling) — `--reuse` ile farkı görün.
/// </summary>
public static class TimeWaitDemo
{
    public static async Task RunAsync(int connections, bool reuse)
    {
        using var cts = new CancellationTokenSource();
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;
        var server = Task.Run(() => ServeAsync(listener, cts.Token));

        Console.WriteLine($"Yerel test sunucusu: 127.0.0.1:{port}");
        PrintEphemeralRange();

        int before = CountTimeWait(port);
        Console.WriteLine($"Başlangıçta bu porta ait TIME_WAIT sayısı: {FormatCount(before)}\n");

        var sw = Stopwatch.StartNew();
        if (reuse)
        {
            Console.WriteLine($"MOD: bağlantıyı YENİDEN KULLAN — 1 bağlantı, {connections:N0} istek");
            using var client = new TcpClient();
            await client.ConnectAsync(IPAddress.Loopback, port);
            await using var stream = client.GetStream();
            using var reader = new StreamReader(stream);
            await using var writer = new StreamWriter(stream) { AutoFlush = true, NewLine = "\n" };
            for (int i = 1; i <= connections; i++)
            {
                await writer.WriteLineAsync("ping");
                await reader.ReadLineAsync();
            }
        }
        else
        {
            Console.WriteLine($"MOD: her istekte YENİ bağlantı — {connections:N0} kısa ömürlü bağlantı (istemci ilk kapatır)");
            for (int i = 1; i <= connections; i++)
            {
                using var client = new TcpClient();
                await client.ConnectAsync(IPAddress.Loopback, port);
                await using var stream = client.GetStream();
                var ping = Encoding.ASCII.GetBytes("ping\n");
                await stream.WriteAsync(ping);
                var buf = new byte[5];                      // sunucu cevabı: "pong\n" = tam 5 byte
                await stream.ReadExactlyAsync(buf);         // TCP bayt akışı: kısmi okumaya karşı tam 5 byte bekle
                // using bloğu biter → client.Dispose() → FIN gönderen İLK taraf biz = TIME_WAIT bize kalır.

                if (i % 100 == 0)
                    Console.WriteLine($"  {i,6:N0} bağlantı sonrası TIME_WAIT: {FormatCount(CountTimeWait(port))}");
            }
        }
        sw.Stop();

        int after = CountTimeWait(port);
        Console.WriteLine($"\nSüre: {sw.ElapsedMilliseconds} ms · TIME_WAIT: {FormatCount(before)} → {FormatCount(after)}");
        PrintExplanation(reuse, after - before, connections, sw.Elapsed);

        cts.Cancel();
        listener.Stop();
        await server;
    }

    /// <summary>Basit sunucu: satır oku, "pong" yaz; istemci kapatınca (EOF) kendi ucunu kapat.</summary>
    private static async Task ServeAsync(TcpListener listener, CancellationToken ct)
    {
        try
        {
            while (!ct.IsCancellationRequested)
            {
                var c = await listener.AcceptTcpClientAsync(ct);
                _ = Task.Run(async () =>
                {
                    using (c)
                    {
                        await using var s = c.GetStream();
                        using var r = new StreamReader(s);
                        await using var w = new StreamWriter(s) { AutoFlush = true, NewLine = "\n" };
                        while (await r.ReadLineAsync() is not null)
                            await w.WriteLineAsync("pong");
                    }
                }, ct);
            }
        }
        catch (Exception ex) when (ex is OperationCanceledException or SocketException or ObjectDisposedException) { }
    }

    // ── İşletim sistemi sayacını okuma ─────────────────────────────────────────────

    /// <summary>`ss -tan state time-wait dport = :PORT` çıktısındaki satır sayısı. ss yoksa -1 döner.</summary>
    private static int CountTimeWait(int port)
    {
        try
        {
            var psi = new ProcessStartInfo("ss")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };
            foreach (var a in new[] { "-H", "-t", "-a", "-n", "state", "time-wait", "dport", "=", $":{port}" })
                psi.ArgumentList.Add(a);

            using var p = Process.Start(psi)!;
            var output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output.Split('\n', StringSplitOptions.RemoveEmptyEntries).Length;
        }
        catch (Win32Exception)
        {
            return -1;   // `ss` bu makinede yok (Windows/macOS)
        }
    }

    private static string FormatCount(int n) => n < 0 ? "(ölçülemedi)" : n.ToString("N0");

    private static void PrintEphemeralRange()
    {
        const string path = "/proc/sys/net/ipv4/ip_local_port_range";
        if (!File.Exists(path))
        {
            Console.WriteLine("Ephemeral port aralığı: (Linux dışı sistem; Windows: `netsh int ipv4 show dynamicport tcp`)");
            return;
        }
        var parts = File.ReadAllText(path).Split(['\t', ' '], StringSplitOptions.RemoveEmptyEntries);
        var (lo, hi) = (int.Parse(parts[0]), int.Parse(parts[1]));
        Console.WriteLine($"Ephemeral port aralığı: {lo}–{hi}  ({hi - lo + 1:N0} port)");
    }

    private static void PrintExplanation(bool reuse, int delta, int connections, TimeSpan elapsed)
    {
        if (delta < 0 && !reuse)
        {
            Console.WriteLine("`ss` bulunamadı. Elle ölçmek için başka bir terminalde:  netstat -an | grep TIME_WAIT | wc -l   (Windows: netstat -an | find /c \"TIME_WAIT\")");
            return;
        }

        if (reuse)
        {
            Console.WriteLine($"→ {connections:N0} istek TEK bağlantıda gitti; TIME_WAIT en fazla 1 (kapanışta). Keep-alive / connection pooling budur.");
            return;
        }

        Console.WriteLine($"→ {connections:N0} bağlantı = ~{delta:N0} TIME_WAIT soketi. Her biri ~60 sn (Linux) bir ephemeral portu rehin tutar.");
        var perSec = connections / Math.Max(elapsed.TotalSeconds, 0.001);
        Console.WriteLine($"→ Bu hızda ({perSec:N0} bağlantı/sn) port havuzu yaklaşık {28_232 / perSec:F0} sn'de biter (varsayılan ~28k port için).");
        Console.WriteLine("  Havuz bitince yeni bağlantılar 'Cannot assign requested address' ile düşer. Aynı mantık `new HttpClient()` kullanım hatasının kökenidir.");
        Console.WriteLine("  Kendiniz görün:  `ss -tan state time-wait | wc -l`  ve 60 sn sonra tekrar bakın; `dotnet run -- time-wait 500 --reuse` ile karşılaştırın.");
    }
}
