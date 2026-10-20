using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Junior.DevFoundations.NetworkFundamentals;

/// <summary>
/// Satır tabanlı (her mesaj '\n' ile biter) TCP echo sunucusu ve istemcisi.
///
/// Sunucu akışı:  bind → listen → accept      (TcpListener bunların hepsini sarar)
/// İstemci akışı: connect                      (TcpClient.ConnectAsync → three-way handshake burada olur)
///
/// TCP bir BYTE AKIŞI'dır, "mesaj" kavramı yoktur. "\n" ile bitirmemiz bizim uygulama katmanı
/// protokolümüzdür (L7). TCP'nin kendisi sınır korumaz: iki Write tek Read'de gelebilir, bir Write iki Read'e bölünebilir.
/// </summary>
public static class TcpEcho
{
    public static async Task RunServerAsync(int port, CancellationToken ct)
    {
        // IPAddress.Loopback = 127.0.0.1 → sadece bu makineden erişilir.
        // IPAddress.Any (0.0.0.0) olsaydı tüm ağ arayüzlerinden erişilirdi (firewall/security group gündeme gelir).
        var listener = new TcpListener(IPAddress.Loopback, port);
        listener.Start();   // bind + listen
        Console.WriteLine($"[TCP] Dinleniyor: {listener.LocalEndpoint}  (durdurmak için Ctrl+C)");
        Console.WriteLine($"      Başka terminalden:  nc 127.0.0.1 {port}   ya da   dotnet run -- tcp-client");

        // İpucu: aynı portu iki process dinleyemez. İkinci bir sunucuyu başlatırsanız:
        //   SocketException: Address already in use  → "bir portu neden iki process dinleyemez?" sorusunun cevabı.

        try
        {
            while (!ct.IsCancellationRequested)
            {
                var client = await listener.AcceptTcpClientAsync(ct);   // accept: bağlantı kuyruğundan bir bağlantı al
                _ = HandleClientAsync(client, ct);                      // her istemci kendi Task'ında → diğerlerini bloklamaz
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
        Console.WriteLine($"[TCP] + bağlandı: {remote}");

        try
        {
            using (client)
            {
                await using var stream = client.GetStream();   // NetworkStream: socket üzerinde Stream soyutlaması
                using var reader = new StreamReader(stream, Encoding.UTF8);
                await using var writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true, NewLine = "\n" };

                while (await reader.ReadLineAsync(ct) is { } line)   // null = karşı taraf bağlantıyı kapattı (FIN geldi)
                {
                    Console.WriteLine($"[TCP]   {remote} → \"{line}\"");
                    if (line.Equals("quit", StringComparison.OrdinalIgnoreCase))
                    {
                        await writer.WriteLineAsync("görüşürüz");
                        break;
                    }
                    await writer.WriteLineAsync($"ECHO: {line}");
                }
            }
        }
        catch (Exception ex) when (ex is IOException or SocketException or OperationCanceledException)
        {
            // İstemci aniden koptu (RST) → normal bir ağ olayı; sunucuyu çökertmemeli.
            Console.WriteLine($"[TCP]   {remote} bağlantı hatası: {ex.GetType().Name}");
        }

        Console.WriteLine($"[TCP] - ayrıldı: {remote}");
    }

    public static async Task RunClientAsync(string host, int port, string[] messages, CancellationToken ct)
    {
        using var client = new TcpClient();

        // connect → SYN, SYN-ACK, ACK (three-way handshake) burada gerçekleşir; maliyeti 1 RTT.
        // Sunucu yoksa: SocketException "Connection refused" (RST döner) — port kapalı demektir.
        // Güvenlik duvarı paketi sessizce düşürürse hata değil TIMEOUT alırsınız. İkisinin farkı klasik bir teşhis sorusudur.
        await client.ConnectAsync(host, port, ct);
        Console.WriteLine($"[TCP] Bağlandı: yerel {client.Client.LocalEndPoint}  →  uzak {client.Client.RemoteEndPoint}");
        Console.WriteLine("      (yerel port = işletim sisteminin verdiği EPHEMERAL port)");

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

        Console.WriteLine("Mesaj yazın ('quit' ile çıkın):");
        while (!ct.IsCancellationRequested && Console.ReadLine() is { } input)
        {
            await writer.WriteLineAsync(input);
            Console.WriteLine($"  ← {await reader.ReadLineAsync(ct)}");
            if (input.Equals("quit", StringComparison.OrdinalIgnoreCase)) break;
        }
    }
}
