

namespace Junior.DevFoundations.NetworkFundamentals;

public static class NetworkFundamentalsTopic
{
    public static int Run(string[] args) => RunAsync(args).GetAwaiter().GetResult();

    private static async Task<int> RunAsync(string[] args)
    {
        // J1.2 — Ağ Temelleri: TCP / UDP / Socket uygulamalı örnekleri.
        // Kullanım için:  dotnet run -- 1.2 help
        //
        // Tek konu, birden çok "komut": her komut ağın farklı bir yönünü gösterir.

        if (args.Length == 0 || args[0] is "help" or "-h" or "--help")
        {
            PrintHelp();
            return 0;
        }

        // Ctrl+C ile sunucuları temiz kapatmak için
        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;      // process'i hemen öldürme; token'ı iptal et
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
                    Console.Error.WriteLine($"Bilinmeyen komut: {args[0]}\n");
                    PrintHelp();
                    return 1;
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("\nKapatıldı.");
        }
        catch (System.Net.Sockets.SocketException ex)
        {
            // ConnectionRefused = hedefe ulaşıldı ama o portu kimse dinlemiyor (RST döndü).
            // Zaman aşımı (TimedOut) olsaydı paket sessizce düşüyor demekti → firewall / security group şüphesi.
            // AddressAlreadyInUse = o portu başka bir process zaten dinliyor.
            Console.Error.WriteLine($"Ağ hatası: {ex.SocketErrorCode} — {ex.Message}");
            return 1;
        }

        return 0;

        static string ArgStr(string[] a, int i, string fallback) => i < a.Length && !a[i].StartsWith("--") ? a[i] : fallback;
        static int ArgInt(string[] a, int i, int fallback) => i < a.Length && int.TryParse(a[i], out var v) ? v : fallback;

        static void PrintHelp() => Console.WriteLine("""
            J1.2 Ağ Temelleri — komutlar  (dotnet run -- 1.2 <komut>)

              tcp-server [port=5000]                   Satır tabanlı TCP echo sunucusu (her istemci için ayrı Task)
              tcp-client [host] [port] [mesaj...]      TCP istemcisi. Mesaj verilmezse klavyeden satır okur ("quit" çıkar)
              udp-server [port=5001]                   UDP echo sunucusu (bağlantı YOK: her datagram bağımsız)
              udp-client [host] [port] [mesaj...]      UDP istemcisi. Sunucu kapalıysa cevap gelmez → zaman aşımı
              loss [adet=20000]                        Aynı yük TCP ve UDP ile: UDP'de paket kaybını gözlemle
              time-wait [adet=500] [--reuse]           Kısa ömürlü bağlantılar → TIME_WAIT birikimi (--reuse: tek bağlantı)

            Örnekler (iki terminal):
              dotnet run -- 1.2 tcp-server
              dotnet run -- 1.2 tcp-client 127.0.0.1 5000 merhaba dunya
            """);
    }
}
