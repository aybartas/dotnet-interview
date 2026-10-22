using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Junior.DevFoundations.NetworkFundamentals;

/// <summary>
/// Line-based (every message ends with '\n') TCP echo server and client.
///
/// Server flow: bind → listen → accept      (TcpListener wraps all of these)
/// Client flow: connect                      (TcpClient.ConnectAsync → the three-way handshake happens here)
///
/// TCP is a BYTE STREAM, there is no notion of a "message". Ending with "\n" is our own application-layer
/// protocol (L7). TCP itself preserves no boundaries: two Writes can arrive in one Read, one Write can be split across two Reads.
/// </summary>
public static class TcpEcho
{
    public static async Task RunServerAsync(int port, CancellationToken ct)
    {
        // IPAddress.Loopback = 127.0.0.1 → reachable only from this machine.
        // With IPAddress.Any (0.0.0.0) it would be reachable from all network interfaces (firewall/security group come into play).
        var listener = new TcpListener(IPAddress.Loopback, port);
        listener.Start();   // bind + listen
        Console.WriteLine($"[TCP] Listening: {listener.LocalEndpoint}  (Ctrl+C to stop)");
        Console.WriteLine($"      From another terminal:  nc 127.0.0.1 {port}   or   dotnet run -- J1.2 tcp-client");

        // Tip: two processes cannot listen on the same port. If you start a second server:
        //   SocketException: Address already in use  → the answer to "why can't two processes listen on one port?"

        try
        {
            while (!ct.IsCancellationRequested)
            {
                var client = await listener.AcceptTcpClientAsync(ct);   // accept: take one connection from the backlog queue
                _ = HandleClientAsync(client, ct);                      // each client gets its own Task → doesn't block the others
            }
        }
        finally
        {
            listener.Stop();
        }
    }

    private static async Task HandleClientAsync(TcpClient client, CancellationToken ct)
    {
        var remote = client.Client.RemoteEndPoint;
        Console.WriteLine($"[TCP] + connected: {remote}");

        try
        {
            using (client)
            {
                await using var stream = client.GetStream();   // NetworkStream: a Stream abstraction over the socket
                using var reader = new StreamReader(stream, Encoding.UTF8);
                await using var writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true, NewLine = "\n" };

                while (await reader.ReadLineAsync(ct) is { } line)   // null = the other side closed the connection (FIN received)
                {
                    Console.WriteLine($"[TCP]   {remote} → \"{line}\"");
                    if (line.Equals("quit", StringComparison.OrdinalIgnoreCase))
                    {
                        await writer.WriteLineAsync("goodbye");
                        break;
                    }
                    await writer.WriteLineAsync($"ECHO: {line}");
                }
            }
        }
        catch (Exception ex) when (ex is IOException or SocketException or OperationCanceledException)
        {
            // The client dropped abruptly (RST) → a normal network event; it must not crash the server.
            Console.WriteLine($"[TCP]   {remote} connection error: {ex.GetType().Name}");
        }

        Console.WriteLine($"[TCP] - disconnected: {remote}");
    }

    public static async Task RunClientAsync(string host, int port, string[] messages, CancellationToken ct)
    {
        using var client = new TcpClient();

        // connect → SYN, SYN-ACK, ACK (three-way handshake) happens here; costs 1 RTT.
        // If there is no server: SocketException "Connection refused" (an RST comes back) — the port is closed.
        // If a firewall silently drops the packet you get a TIMEOUT instead of an error. The difference between the two is a classic diagnosis question.
        await client.ConnectAsync(host, port, ct);
        Console.WriteLine($"[TCP] Connected: local {client.Client.LocalEndPoint}  →  remote {client.Client.RemoteEndPoint}");
        Console.WriteLine("      (local port = the EPHEMERAL port assigned by the operating system)");

        await using var stream = client.GetStream();
        using var reader = new StreamReader(stream, Encoding.UTF8);
        await using var writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true, NewLine = "\n" };

        if (messages.Length > 0)
        {
            foreach (var m in messages)
            {
                await writer.WriteLineAsync(m);
                Console.WriteLine($"  → {m}\n  ← {await reader.ReadLineAsync(ct)}");
            }
            await writer.WriteLineAsync("quit");
            Console.WriteLine($"  ← {await reader.ReadLineAsync(ct)}");
            return;
        }

        Console.WriteLine("Type a message ('quit' to exit):");
        while (!ct.IsCancellationRequested && Console.ReadLine() is { } input)
        {
            await writer.WriteLineAsync(input);
            Console.WriteLine($"  ← {await reader.ReadLineAsync(ct)}");
            if (input.Equals("quit", StringComparison.OrdinalIgnoreCase)) break;
        }
    }
}
