using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.Storage;

public static class Section05_MovingData
{
    public static void Run()
    {
        Table.Print(["İhtiyaç (senaryodaki ifade)", "Araç", "Nasıl?"],
        [
            ["\"Komut satırından blob/dosya kopyala, betikle\"", "AzCopy", "azcopy copy / sync; hesaplar ve bulutlar arası kopya"],
            ["\"Grafik arayüzle blob/dosya/kuyruk yönet\"", "Azure Storage Explorer", "Masaüstü uygulaması (arka planda AzCopy)"],
            ["\"On-prem Windows dosya sunucusu Azure Files ile senkron, sık dosyalar yerelde\"", "Azure File Sync", "Sync agent + cloud tiering"],
            ["\"Bulutta yönetilen SMB/NFS paylaşımı\"", "Azure Files", "Paylaşımı bağla (net use / mount)"],
            ["\"Sunucuları keşfet, boyutlandır, maliyetini çıkar, taşı\"", "Azure Migrate", "Merkezi hub: discovery & assessment, migration"],
            ["\"Terabaytlarca veri, ağ yavaş/pahalı/yok\"", "Azure Data Box", "Microsoft fiziksel cihaz gönderir, veriyi yükleyip geri yollarsın"],
        ]);

        Console.WriteLine("""

        Ağdan mı, cihazla mı? Kaba hesap: 100 Mbps hat ile 40 TB ≈ 40e12 × 8 / 100e6 sn ≈ 37 gün (hattın tamamı bu işe ayrılsa bile).
        Böyle bir aktarımda Data Box birkaç günlük kargo süresiyle çok daha hızlıdır; Data Box dışa aktarmayı (Azure → on-prem) da destekler.

        Karıştırılan çiftler:
          • AzCopy (komut satırı, tek seferlik/betikli kopya) ↔ Storage Explorer (grafik araç) ↔ File Sync (sürekli, iki yönlü senkron).
          • Azure Migrate (taşıma PLATFORMU: keşif + değerlendirme + taşıma) ↔ Data Box (veriyi FİZİKSEL taşıma cihazı).
        """);
    }
}
