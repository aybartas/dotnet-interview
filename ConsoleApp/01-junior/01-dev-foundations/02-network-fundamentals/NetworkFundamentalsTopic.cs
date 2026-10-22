using System.Globalization;

namespace Junior.DevFoundations.NetworkFundamentals;

public static class NetworkFundamentalsTopic
{
    public static int Run(string[] args)
    {
        // Culture-independent number formatting (1,234 / 12.5) regardless of the machine culture.
        var previous = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        try
        {
            return RunAsync(args).GetAwaiter().GetResult();
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }

    private static async Task<int> RunAsync(string[] args)
    {
        // J1.2 — Network Fundamentals: hands-on TCP / UDP / Socket examples.
        // For usage:  dotnet run -- J1.2 help
        //
        // One topic, several "commands": each command shows a different aspect of the network.

        if (args.Length == 0 || args[0] is "help" or "-h" or "--help")
        {
            PrintHelp();
            return 0;
        }

        // To shut the servers down cleanly with Ctrl+C
        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;      // don't kill the process immediately; cancel the token instead
            cts.Cancel();
        };

        var rest = args[1..];

        try
        {
            switch (args[0])
            {
                case "tcp-server":
                    await TcpEcho.RunServerAsync(port: ArgInt(rest, 0, 5000), cts.Token);
                    break;
                case "tcp-client":
                    await TcpEcho.RunClientAsync(host: ArgStr(rest, 0, "127.0.0.1"), port: ArgInt(rest, 1, 5000),
                                                 messages: rest.Skip(2).ToArray(), cts.Token);
                    break;
                case "udp-server":
                    await UdpEcho.RunServerAsync(port: ArgInt(rest, 0, 5001), cts.Token);
                    break;
                case "udp-client":
                    await UdpEcho.RunClientAsync(host: ArgStr(rest, 0, "127.0.0.1"), port: ArgInt(rest, 1, 5001),
                                                 messages: rest.Skip(2).ToArray(), cts.Token);
                    break;
                case "loss":
                    await LossDemo.RunAsync(count: ArgInt(rest, 0, 20_000));
                    break;
                case "time-wait":
                    await TimeWaitDemo.RunAsync(connections: ArgInt(rest, 0, 500), reuse: rest.Contains("--reuse"));
                    break;
                default:
                    Console.Error.WriteLine($"Unknown command: {args[0]}\n");
                    PrintHelp();
                    return 1;
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("\nShut down.");
        }
        catch (System.Net.Sockets.SocketException ex)
        {
            // ConnectionRefused = the target was reached but nobody is listening on that port (an RST came back).
            // A timeout (TimedOut) would have meant the packet is silently dropped → suspect a firewall / security group.
            // AddressAlreadyInUse = another process is already listening on that port.
            Console.Error.WriteLine($"Network error: {ex.SocketErrorCode} — {ex.Message}");
            return 1;
        }

        return 0;

        static string ArgStr(string[] a, int i, string fallback) => i < a.Length && !a[i].StartsWith("--") ? a[i] : fallback;
        static int ArgInt(string[] a, int i, int fallback) => i < a.Length && int.TryParse(a[i], out var v) ? v : fallback;

        static void PrintHelp() => Console.WriteLine("""
            J1.2 Network Fundamentals — commands  (dotnet run -- J1.2 <command>)

              tcp-server [port=5000]                   Line-based TCP echo server (a separate Task per client)
              tcp-client [host] [port] [message...]    TCP client. If no message is given, reads lines from the keyboard ("quit" exits)
              udp-server [port=5001]                   UDP echo server (NO connection: every datagram is independent)
              udp-client [host] [port] [message...]    UDP client. If the server is down, no reply comes → timeout
              loss [count=20000]                       Same load over TCP and UDP: observe packet loss with UDP
              time-wait [count=500] [--reuse]          Short-lived connections → TIME_WAIT buildup (--reuse: single connection)

            Examples (two terminals):
              dotnet run -- J1.2 tcp-server
              dotnet run -- J1.2 tcp-client 127.0.0.1 5000 hello world
            """);
    }
}
