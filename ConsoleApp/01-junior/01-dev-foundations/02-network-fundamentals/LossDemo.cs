using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace Junior.DevFoundations.NetworkFundamentals;

/// <summary>
/// Sends the same load (N numbered messages of 512 bytes) to a SLOW receiver over both UDP and TCP.
///
/// Expected result:
///  • UDP: when the receiver's socket buffer fills up, the operating system SILENTLY drops datagrams → "sent ≠ received".
///         The sender gets no error; the loss is only noticed by counting on the receiving side.
///  • TCP: when the receive window fills up the sender is SLOWED DOWN (flow control) → nothing is lost, order is preserved;
///         the price: total time is bound to the receiver's speed.
/// </summary>
public static class LossDemo
{
    private const int PayloadSize = 512;
    private const int SpinPerMessage = 3_000;   // simulates the receiver's "work" time (slow consumer)

    public static async Task RunAsync(int count)
    {
        Console.WriteLine($"Load: {count:N0} messages × {PayloadSize} bytes, receiver is deliberately slow.\n");
        await RunUdpAsync(count);
        await RunTcpAsync(count);

        Console.WriteLine("""

            Conclusion: UDP gives up the delivery guarantee for speed — the application must handle loss and ordering itself
            (DNS, games, live video; QUIC adds its own reliability on top of UDP for this reason). TCP gives the guarantee,
            but the price is the handshake, the ACKs, and slowing down to the pace of a slow receiver.
            """);
    }

    // ───────────────────────────── UDP ─────────────────────────────

    private static async Task RunUdpAsync(int count)
    {
        using var receiver = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        receiver.Bind(new IPEndPoint(IPAddress.Loopback, 0));
        receiver.ReceiveBufferSize = 4 * 1024;             // deliberately small buffer (the OS may raise it to a lower bound)
        receiver.ReceiveTimeout = 500;                      // 500 ms of silence = sending is finished
        var target = receiver.LocalEndPoint!;

        int received = 0, outOfOrder = 0, lastSeq = -1;
        var recvTask = Task.Run(() =>
        {
            var buf = new byte[PayloadSize];
            while (true)
            {
                try { receiver.Receive(buf); }
                catch (SocketException ex) when (ex.SocketErrorCode == SocketError.TimedOut) { break; }

                int seq = BitConverter.ToInt32(buf, 0);
                if (seq < lastSeq) outOfOrder++;
                lastSeq = Math.Max(lastSeq, seq);
                received++;
                Thread.SpinWait(SpinPerMessage);            // slow consumer
            }
        });

        var sw = Stopwatch.StartNew();
        using (var sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
        {
            var payload = new byte[PayloadSize];
            for (int i = 0; i < count; i++)
            {
                BitConverter.TryWriteBytes(payload, i);
                sender.SendTo(payload, target);             // doesn't throw: the sender doesn't know even if it gets lost
            }
        }
        var sendTime = sw.Elapsed;
        await recvTask;

        int lost = count - received;
        Console.WriteLine($"[UDP] sent: {count:N0}   received: {received:N0}   LOST: {lost:N0} ({100.0 * lost / count:F1}%)   out of order: {outOfOrder}   send time: {sendTime.TotalMilliseconds:F0} ms");
        if (outOfOrder == 0)
            Console.WriteLine("      (reordering isn't expected on loopback; on a real network, datagrams taking different paths can arrive out of order)");
    }

    // ───────────────────────────── TCP ─────────────────────────────

    private static async Task RunTcpAsync(int count)
    {
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;

        int received = 0, outOfOrder = 0, lastSeq = -1;
        var serverTask = Task.Run(async () =>
        {
            using var conn = await listener.AcceptTcpClientAsync();
            await using var stream = conn.GetStream();
            var buf = new byte[PayloadSize];
            while (true)
            {
                // TCP is a byte stream: WE draw the "512-byte message" boundary → wait until exactly 512 bytes are read.
                try { await stream.ReadExactlyAsync(buf); }
                catch (EndOfStreamException) { break; }     // the sender closed

                int seq = BitConverter.ToInt32(buf, 0);
                if (seq < lastSeq) outOfOrder++;
                lastSeq = Math.Max(lastSeq, seq);
                received++;
                Thread.SpinWait(SpinPerMessage);
            }
        });

        var sw = Stopwatch.StartNew();
        using (var client = new TcpClient())
        {
            await client.ConnectAsync(IPAddress.Loopback, port);
            await using var stream = client.GetStream();
            var payload = new byte[PayloadSize];
            for (int i = 0; i < count; i++)
            {
                BitConverter.TryWriteBytes(payload, i);
                await stream.WriteAsync(payload);           // waits HERE when the buffer is full: flow control
            }
        }
        await serverTask;
        listener.Stop();

        int lost = count - received;
        Console.WriteLine($"[TCP] sent: {count:N0}   received: {received:N0}   LOST: {lost:N0}              out of order: {outOfOrder}   total time: {sw.Elapsed.TotalMilliseconds:F0} ms");
    }
}
