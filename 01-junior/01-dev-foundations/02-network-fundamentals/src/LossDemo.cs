using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace Junior.DevFoundations.NetworkFundamentals;

/// <summary>
/// Aynı yükü (N adet, 512 byte'lık, sıra numaralı mesaj) hem UDP hem TCP ile YAVAŞ bir alıcıya gönderir.
///
/// Beklenen sonuç:
///  • UDP: alıcının socket tamponu dolunca işletim sistemi datagram'ları SESSİZCE düşürür → "gönderilen ≠ alınan".
///         Gönderen hata almaz; kayıp sadece alıcı tarafında sayarak fark edilir.
///  • TCP: receive window dolunca gönderen YAVAŞLATILIR (flow control) → hiçbir şey kaybolmaz, sıra korunur;
///         bedeli: toplam süre alıcının hızına bağlanır.
/// </summary>
public static class LossDemo
{
    private const int PayloadSize = 512;
    private const int SpinPerMessage = 3_000;   // alıcının "iş yapma" süresini simüle eder (yavaş tüketici)

    public static async Task RunAsync(int count)
    {
        Console.WriteLine($"Yük: {count:N0} mesaj × {PayloadSize} byte, alıcı bilerek yavaş.\n");
        await RunUdpAsync(count);
        await RunTcpAsync(count);

        Console.WriteLine("""

            Sonuç: UDP hız için teslim garantisinden vazgeçer — kaybı ve sırayı uygulama yönetmek zorundadır
            (DNS, oyun, canlı video, QUIC bu yüzden UDP üstüne kendi güvenilirliğini ekler). TCP ise garanti verir
            ama bunun bedeli handshake, ACK'ler ve yavaş alıcıya göre yavaşlamaktır.
            """);
    }

    // ───────────────────────────── UDP ─────────────────────────────

    private static async Task RunUdpAsync(int count)
    {
        using var receiver = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        receiver.Bind(new IPEndPoint(IPAddress.Loopback, 0));
        receiver.ReceiveBufferSize = 4 * 1024;             // bilerek küçük tampon (işletim sistemi alt sınıra yükseltebilir)
        receiver.ReceiveTimeout = 500;                      // 500 ms sessizlik = gönderim bitti
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
                Thread.SpinWait(SpinPerMessage);            // yavaş tüketici
            }
        });

        var sw = Stopwatch.StartNew();
        using (var sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
        {
            var payload = new byte[PayloadSize];
            for (int i = 0; i < count; i++)
            {
                BitConverter.TryWriteBytes(payload, i);
                sender.SendTo(payload, target);             // hata vermez: kaybolsa bile gönderen bilmez
            }
        }
        var sendTime = sw.Elapsed;
        await recvTask;

        int lost = count - received;
        Console.WriteLine($"[UDP] gönderilen: {count:N0}   alınan: {received:N0}   KAYIP: {lost:N0} (%{100.0 * lost / count:F1})   sırasız: {outOfOrder}   gönderme süresi: {sendTime.TotalMilliseconds:F0} ms");
        if (outOfOrder == 0)
            Console.WriteLine("      (loopback'te sıra bozulması beklenmez; gerçek ağda farklı yollardan giden datagramlar sırasız gelebilir)");
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
                // TCP bayt akışıdır: "512 byte'lık mesaj" sınırını BİZ çiziyoruz → tam 512 byte okuyana kadar bekle.
                try { await stream.ReadExactlyAsync(buf); }
                catch (EndOfStreamException) { break; }     // gönderen kapattı

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
                await stream.WriteAsync(payload);           // tampon dolunca BURADA bekler: flow control
            }
        }
        await serverTask;
        listener.Stop();

        int lost = count - received;
        Console.WriteLine($"[TCP] gönderilen: {count:N0}   alınan: {received:N0}   KAYIP: {lost:N0}              sırasız: {outOfOrder}   toplam süre: {sw.Elapsed.TotalMilliseconds:F0} ms");
    }
}
