using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Junior.DevFoundations.NetworkFundamentals;

/// <summary>
/// Opens and closes many SHORT-LIVED TCP connections and shows the operating system's TIME_WAIT counter growing.
///
/// Why? The side that closes the connection FIRST (active close) enters TIME_WAIT and holds that
/// (local IP, local port, remote IP, remote port) quadruple for ~2×MSL (60 s on Linux) — so delayed packets don't leak into a new connection.
/// If the client opens a new connection per request and closes it first, TIME_WAIT sockets pile up and the EPHEMERAL PORT pool is exhausted:
///     "Cannot assign requested address" / SocketException (AddressNotAvailable)
/// The classic .NET equivalent: `new HttpClient()` on every request → "HttpClient socket exhaustion" (→ J7.5, M5.9).
/// The fix: reuse the connection (keep-alive / connection pooling) — see the difference with `--reuse`.
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

        Console.WriteLine($"Local test server: 127.0.0.1:{port}");
        PrintEphemeralRange();

        int before = CountTimeWait(port);
        Console.WriteLine($"TIME_WAIT count for this port at the start: {FormatCount(before)}\n");

        var sw = Stopwatch.StartNew();
        if (reuse)
        {
            Console.WriteLine($"MODE: REUSE the connection — 1 connection, {connections:N0} requests");
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
            Console.WriteLine($"MODE: a NEW connection per request — {connections:N0} short-lived connections (the client closes first)");
            for (int i = 1; i <= connections; i++)
            {
                using var client = new TcpClient();
                await client.ConnectAsync(IPAddress.Loopback, port);
                await using var stream = client.GetStream();
                var ping = Encoding.ASCII.GetBytes("ping\n");
                await stream.WriteAsync(ping);
                var buf = new byte[5];                      // server reply: "pong\n" = exactly 5 bytes
                await stream.ReadExactlyAsync(buf);         // TCP is a byte stream: wait for exactly 5 bytes to guard against partial reads
                // the using block ends → client.Dispose() → we are the FIRST to send FIN = TIME_WAIT is left to us.

                if (i % 100 == 0)
                    Console.WriteLine($"  TIME_WAIT after {i,6:N0} connections: {FormatCount(CountTimeWait(port))}");
            }
        }
        sw.Stop();

        int after = CountTimeWait(port);
        Console.WriteLine($"\nDuration: {sw.ElapsedMilliseconds} ms · TIME_WAIT: {FormatCount(before)} → {FormatCount(after)}");
        PrintExplanation(reuse, after - before, connections, sw.Elapsed);

        cts.Cancel();
        listener.Stop();
        await server;
    }

    /// <summary>Simple server: read a line, write "pong"; when the client closes (EOF) close its own end.</summary>
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

    // ── Reading the operating system counter ─────────────────────────────────────────────

    /// <summary>Number of lines in the output of `ss -tan state time-wait dport = :PORT`. Returns -1 if ss is missing.</summary>
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
            return -1;   // `ss` doesn't exist on this machine (Windows/macOS)
        }
    }

    private static string FormatCount(int n) => n < 0 ? "(could not be measured)" : n.ToString("N0");

    private static void PrintEphemeralRange()
    {
        const string path = "/proc/sys/net/ipv4/ip_local_port_range";
        if (!File.Exists(path))
        {
            Console.WriteLine("Ephemeral port range: (non-Linux system; Windows: `netsh int ipv4 show dynamicport tcp`)");
            return;
        }
        var parts = File.ReadAllText(path).Split(['\t', ' '], StringSplitOptions.RemoveEmptyEntries);
        var (lo, hi) = (int.Parse(parts[0]), int.Parse(parts[1]));
        Console.WriteLine($"Ephemeral port range: {lo}–{hi}  ({hi - lo + 1:N0} ports)");
    }

    private static void PrintExplanation(bool reuse, int delta, int connections, TimeSpan elapsed)
    {
        if (delta < 0 && !reuse)
        {
            Console.WriteLine("`ss` not found. To measure by hand, in another terminal:  netstat -an | grep TIME_WAIT | wc -l   (Windows: netstat -an | find /c \"TIME_WAIT\")");
            return;
        }

        if (reuse)
        {
            Console.WriteLine($"→ {connections:N0} requests went over a SINGLE connection; TIME_WAIT is at most 1 (at close). This is what keep-alive / connection pooling is.");
            return;
        }

        Console.WriteLine($"→ {connections:N0} connections = ~{delta:N0} TIME_WAIT sockets. Each holds an ephemeral port hostage for ~60 s (Linux).");
        var perSec = connections / Math.Max(elapsed.TotalSeconds, 0.001);
        Console.WriteLine($"→ At this rate ({perSec:N0} connections/s) the port pool runs out in about {28_232 / perSec:F0} s (for the default ~28k ports).");
        Console.WriteLine("  Once the pool is exhausted new connections fail with 'Cannot assign requested address'. The same logic is the root of the `new HttpClient()` misuse.");
        Console.WriteLine("  See for yourself:  `ss -tan state time-wait | wc -l`  and look again after 60 s; compare with `dotnet run -- J1.2 time-wait 500 --reuse`.");
    }
}
