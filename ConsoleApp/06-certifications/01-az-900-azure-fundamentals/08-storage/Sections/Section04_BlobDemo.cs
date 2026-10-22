using System.Text;
using Azure;
using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Certification.AzureFundamentals.Storage;

/// <summary>
/// Blob Storage temel işlemleri. Varsayılan hedef yerel Azurite emülatörüdür (Azure hesabı gerekmez);
/// AZURE_STORAGE_CONNECTION_STRING tanımlıysa gerçek bir storage account kullanılır.
/// </summary>
public static class Section04_BlobDemo
{
    private const string ContainerName = "c18-demo";

    /// <summary>Emülatöre/hesaba ulaşılamadıysa true — konu giriş noktası çıkış kodunu buna göre verir.</summary>
    public static bool Failed { get; private set; }

    public static void Run()
    {
        var connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING") ?? "UseDevelopmentStorage=true";
        var target = connectionString == "UseDevelopmentStorage=true" ? "Azurite (127.0.0.1:10000)" : "AZURE_STORAGE_CONNECTION_STRING";

        // Emülatör kapalıysa SDK'nın varsayılan yeniden deneme politikası dakikalarca bekler; demo hızlı hata versin.
        var options = new BlobClientOptions { Retry = { MaxRetries = 0, NetworkTimeout = TimeSpan.FromSeconds(5) } };
        var service = new BlobServiceClient(connectionString, options);
        Console.WriteLine($"Hedef: {target} · hesap: {service.AccountName}\n");

        try
        {
            // 1) Container (hiyerarşi: account → container → blob)
            var container = service.GetBlobContainerClient(ContainerName);
            container.CreateIfNotExists();
            Console.WriteLine($"1) Container hazır: {container.Uri}");

            // 2) Upload: üç küçük blob, "klasör" görünümü için ada '/' koyarak
            (string Name, string Content)[] files =
            [
                ("raporlar/2026-10.csv", "ay,gelir\n2026-10,1250\n"),
                ("yedekler/db-2026-09.bak", new string('x', 2048)),
                ("log/uygulama.log", "INFO uygulama basladi\n"),
            ];
            foreach (var (name, content) in files)
                container.GetBlobClient(name).Upload(BinaryData.FromString(content), overwrite: true);
            Console.WriteLine($"2) {files.Length} blob yüklendi.");

            // 3) Listeleme (her blob'un boyutu ve access tier'ı)
            Console.WriteLine("3) Container içeriği:");
            foreach (var blob in container.GetBlobs())
                Console.WriteLine($"   {blob.Name,-26} {blob.Properties.ContentLength,6} B  tier: {blob.Properties.AccessTier?.ToString() ?? "(varsayılan)"}");

            // 4) Download
            var downloaded = container.GetBlobClient("raporlar/2026-10.csv").DownloadContent().Value.Content.ToString();
            Console.WriteLine($"4) İndirilen raporlar/2026-10.csv: {downloaded.ReplaceLineEndings(" | ").TrimEnd(' ', '|')}");

            // 5) Access tier değiştirme: eski yedeği Cool'a, sonra Archive'a
            ChangeTier(container.GetBlobClient("yedekler/db-2026-09.bak"));

            // 6) Temizlik
            container.DeleteIfExists();
            Console.WriteLine("6) Container silindi (laboratuvarın temizlik adımı).");
        }
        catch (Exception ex) when (ex is RequestFailedException { Status: 0 } or AggregateException or HttpRequestException or TimeoutException or OperationCanceledException)
        {
            Failed = true;
            Console.Error.WriteLine($"""
            Blob endpoint'ine ulaşılamadı ({ex.GetType().Name}: {ex.Message.Split('\n')[0]})
            Azurite'i başlat: repo kökünde  docker compose up -d
            Ardından tekrar: dotnet run -- C1.8 4
            """);
        }
    }

    private static void ChangeTier(BlobClient blob)
    {
        foreach (var tier in new[] { AccessTier.Cool, AccessTier.Archive })
        {
            try
            {
                blob.SetAccessTier(tier);
                Console.WriteLine($"5) {blob.Name} → {blob.GetProperties().Value.AccessTier}");
            }
            catch (RequestFailedException ex) when (ex.Status is not 0)
            {
                // Azurite bazı tier işlemlerini Azure'daki gibi desteklemeyebilir; gerçek hesapta ne olacağını açıkla.
                Console.WriteLine($"5) {tier}: bu hedef işlemi desteklemedi ({ex.ErrorCode ?? ex.Status.ToString()}). Azure'da blob {tier} tier'ına geçer.");
            }
        }

        Console.WriteLine("""
           Archive'daki blob ÇEVRİMDIŞIDIR: Azure'da okumak (download) 409 BlobArchived hatası verir; önce
           SetAccessTier(Hot/Cool/Cold, rehydratePriority: Standard|High) ile rehydrate edilmesi gerekir (saatler sürebilir).
        """);
        try
        {
            blob.DownloadContent();
            Console.WriteLine("   Bu hedef Archive'daki blob'u okumaya izin verdi (emülatör davranışı; Azure'da izin vermez).");
        }
        catch (RequestFailedException ex) when (ex.Status is not 0)
        {
            Console.WriteLine($"   Archive'daki blob'u okuma denemesi reddedildi: {ex.Status} {ex.ErrorCode}");
        }
    }
}
