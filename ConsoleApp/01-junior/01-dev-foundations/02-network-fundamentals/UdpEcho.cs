using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Junior.DevFoundations.NetworkFundamentals;

/// <summary>
/// The UDP version of the SAME protocol as the TCP echo. Note the differences:
///  • No connection: no handshake, no accept, no Close. Every SendAsync is an independent datagram.
///  • Message boundaries are preserved (unlike TCP): one Send = one Receive.
///  • No delivery guarantee: if the server is down the client gets NO ERROR, there is just no reply → we handle the timeout ourselves.
/// </summary>
public static class UdpEcho
{
    public static async Task RunServerAsync(int port, CancellationToken ct)
    {
        using var udp = new UdpClient(new IPEndPoint(IPAddress.Loopback, port));   // bind
        Console.WriteLine($"[UDP] Listening: {udp.Client.LocalEndPoint}  (Ctrl+C to stop)");

        while (!ct.IsCancellationRequested)
        {
            // No accept: every incoming datagram is received together with the sender's address.
            var result = await udp.ReceiveAsync(ct);
            var text = Encoding.UTF8.GetString(result.Buffer);
            Console.WriteLine($"[UDP]   {result.RemoteEndPoint} → \"{text}\"");

            var reply = Encoding.UTF8.GetBytes($"ECHO: {text}");
            await udp.SendAsync(reply, result.RemoteEndPoint, ct);
        }
    }

    public static async Task RunClientAsync(string host, int port, string[] messages, CancellationToken ct)
    {
        using var udp = new UdpClient();
        // Connect() on UDP does not "establish a connection"; it only sets the default target. There is no handshake, not a single packet goes on the wire.
        udp.Connect(host, port);
        Console.WriteLine($"[UDP] Target: {host}:{port} (local {udp.Client.LocalEndPoint})");

        var toSend = messages.Length > 0 ? messages : ["hello udp"];
        foreach (var m in toSend)
        {
            await udp.SendAsync(Encoding.UTF8.GetBytes(m), ct);

            // Wait at most 2 s for the reply. With UDP "lost" and "slow" are indistinguishable; the decision is the application's.
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(ct);
            timeout.CancelAfter(TimeSpan.FromSeconds(2));
            try
            {
                var reply = await udp.ReceiveAsync(timeout.Token);
                Console.WriteLine($"  → {m}\n  ← {Encoding.UTF8.GetString(reply.Buffer)}");
            }
            catch (OperationCanceledException) when (!ct.IsCancellationRequested)
            {
                Console.WriteLine($"  → {m}\n  ← (no reply within 2 s — the packet may have been lost or the server may be down; UDP won't tell you)");
            }
            catch (SocketException ex) when (ex.SocketErrorCode is SocketError.ConnectionReset or SocketError.ConnectionRefused)
            {
                // For a datagram sent to a closed port, the target machine returns an ICMP "port unreachable"; on a socket that has called connect()
                // it surfaces on the next Receive as ConnectionReset on Windows and ConnectionRefused on Linux.
                // Still NOT guaranteed: if a firewall drops the ICMP, the timeout path above kicks in.
                Console.WriteLine($"  → {m}\n  ← ICMP port unreachable (the server is not listening on this port)");
            }
        }
    }
}
