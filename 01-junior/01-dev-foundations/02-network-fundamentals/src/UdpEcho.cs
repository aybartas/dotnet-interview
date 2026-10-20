using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Junior.DevFoundations.NetworkFundamentals;

/// <summary>
/// TCP echo ile AYNI protokolün UDP versiyonu. Farklara dikkat:
///  • Bağlantı yok: handshake, accept, Close yok. Her SendAsync bağımsız bir datagram'dır.
///  • Mesaj sınırı korunur (TCP'nin aksine): bir Send = bir Receive.
///  • Teslim garantisi yok: sunucu kapalıysa istemci HATA ALMAZ, sadece cevap gelmez → zaman aşımını biz yönetiriz.
/// </summary>
public static class UdpEcho
{
    public static async Task RunServerAsync(int port, CancellationToken ct)
    {
        using var udp = new UdpClient(new IPEndPoint(IPAddress.Loopback, port));   // bind
        Console.WriteLine($"[UDP] Dinleniyor: {udp.Client.LocalEndPoint}  (durdurmak için Ctrl+C)");

        while (!ct.IsCancellationRequested)
        {
            // Accept yok: gelen her datagram, kimden geldiği bilgisiyle birlikte alınır.
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
        // Connect() UDP'de "bağlantı kurmaz"; sadece varsayılan hedefi ayarlar. Handshake yoktur, ağa tek paket gitmez.
        udp.Connect(host, port);
        Console.WriteLine($"[UDP] Hedef: {host}:{port} (yerel {udp.Client.LocalEndPoint})");

        var toSend = messages.Length > 0 ? messages : ["merhaba udp"];
        foreach (var m in toSend)
        {
            await udp.SendAsync(Encoding.UTF8.GetBytes(m), ct);

            // Cevabı en fazla 2 sn bekle. UDP'de "kayıp" ile "yavaş" ayırt edilemez; karar uygulamanındır.
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(ct);
            timeout.CancelAfter(TimeSpan.FromSeconds(2));
            try
            {
                var reply = await udp.ReceiveAsync(timeout.Token);
                Console.WriteLine($"  → {m}\n  ← {Encoding.UTF8.GetString(reply.Buffer)}");
            }
            catch (OperationCanceledException) when (!ct.IsCancellationRequested)
            {
                Console.WriteLine($"  → {m}\n  ← (2 sn içinde cevap yok — paket kaybolmuş ya da sunucu kapalı olabilir; UDP bunu söylemez)");
            }
            catch (SocketException ex) when (ex.SocketErrorCode is SocketError.ConnectionReset or SocketError.ConnectionRefused)
            {
                // Kapalı porta giden datagram'a hedef makine ICMP "port unreachable" döner; Windows'ta ConnectionReset,
                // Linux'ta ConnectionRefused olarak (connect() yapılmış socket'te) bir sonraki Receive'de yüzeye çıkar.
                // Yine de garanti DEĞİLDİR: firewall ICMP'yi düşürürse yukarıdaki zaman aşımı yolu çalışır.
                Console.WriteLine($"  → {m}\n  ← ICMP port unreachable (sunucu bu portu dinlemiyor)");
            }
        }
    }
}
