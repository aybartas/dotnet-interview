using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.Storage;

public static class Section01_StorageServices
{
    public static void Run()
    {
        Console.WriteLine("Bir storage account = benzersiz ad alanı + her servis için bir endpoint:\n");
        Table.Print(["Servis", "Ne saklar?", "Endpoint", "Tipik kullanım"],
        [
            ["Blob", "Nesne: yapılandırılmamış veri", "https://<ad>.blob.core.windows.net", "Görsel, video, yedek, log, statik site"],
            ["Azure Files", "SMB/NFS dosya paylaşımı", "https://<ad>.file.core.windows.net", "Paylaşılan klasör, lift-and-shift uygulama"],
            ["Queue", "Basit mesaj kuyruğu (mesaj ≤ 64 KB)", "https://<ad>.queue.core.windows.net", "Bileşenler arası asenkron iş"],
            ["Table", "NoSQL anahtar-değer (şemasız)", "https://<ad>.table.core.windows.net", "Basit yapılandırılmış veri, meta veri"],
            ["Disks", "VM'e bağlanan blok depolama (managed disk)", "— (VM'e bağlı kaynak)", "VM işletim sistemi ve veri diskleri"],
        ]);

        Console.WriteLine("\nStorage account türleri:\n");
        Table.Print(["Tür", "Desteklediği", "Redundancy", "Ne zaman?"],
        [
            ["Standard general-purpose v2", "Blob, Files, Queue, Table", "LRS, ZRS, GRS, RA-GRS, GZRS, RA-GZRS", "Çoğu senaryo (varsayılan seçim)"],
            ["Premium block blobs", "Block/append blob", "LRS, ZRS", "Düşük gecikme, yüksek işlem hızı"],
            ["Premium file shares", "Azure Files", "LRS, ZRS", "Kurumsal, yüksek performanslı dosya paylaşımı"],
            ["Premium page blobs", "Page blob", "LRS, ZRS", "Yüksek performanslı page blob senaryoları"],
        ]);

        Console.WriteLine("""

        Kurallar:
          • Storage account adı Azure GENELİNDE benzersizdir (URL'nin parçası): 3-24 karakter, yalnızca küçük harf ve rakam.
          • Blob hiyerarşisi: storage account → container → blob. Blob türleri: block (dosyalar), append (log), page (VM diskleri).
          • Premium seçenekler SSD tabanlıdır ve coğrafi (GRS/GZRS) redundancy sunmaz.
        """);
    }
}
