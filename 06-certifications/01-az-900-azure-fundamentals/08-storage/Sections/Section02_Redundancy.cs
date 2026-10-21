using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.Storage;

/// <summary>Redundancy seçenekleri ↔ arıza türleri. Sınavın en sevdiği storage tablosu.</summary>
public static class Section02_Redundancy
{
    private sealed record Option(string Name, string Copies, bool Server, bool Datacenter, bool Region, bool ReadSecondaryAlways);

    private static readonly Option[] Options =
    [
        new("LRS",     "Birincil bölgede, TEK datacenter içinde 3 kopya",            true, false, false, false),
        new("ZRS",     "Birincil bölgede, 3 availability zone'a 3 kopya",           true, true,  false, false),
        new("GRS",     "LRS (birincil) + eşleşik bölgede LRS (toplam 6 kopya)",      true, false, true,  false),
        new("GZRS",    "ZRS (birincil) + eşleşik bölgede LRS",                       true, true,  true,  false),
        new("RA-GRS",  "GRS + ikincil bölgeden HER ZAMAN okuma",                     true, false, true,  true),
        new("RA-GZRS", "GZRS + ikincil bölgeden HER ZAMAN okuma",                    true, true,  true,  true),
    ];

    public static void Run()
    {
        Table.Print(["Seçenek", "Kopyalar", "Disk/raf arızası", "Datacenter/zone arızası", "Bölgesel felaket", "Felakette okuma"],
            Options.Select(o => (IReadOnlyList<string>)
            [
                o.Name, o.Copies, Mark(o.Server), Mark(o.Datacenter),
                o.Region ? "✔ (failover ile)" : "✘",
                o.ReadSecondaryAlways ? "✔ ikincilden" : o.Region ? "failover sonrası" : "✘",
            ]));

        Console.WriteLine("""

        Not: GRS/GZRS'te birincil bölgedeki datacenter arızasında veri kaybolmaz (ikincilde kopya var) ama hizmet,
        failover yapılana kadar okunamaz; tabloda "datacenter/zone arızası" sütunu kesintisiz erişimi gösterir.

        Senaryo → seçenek:
          • "En ucuz; datacenter felaketi kabul edilebilir"                         → LRS
          • "Tek bölgede kalmalı (veri ikametgâhı), ama bir zone düşerse erişim sürsün" → ZRS
          • "Bölgesel felakete dayan"                                                → GRS (veya GZRS)
          • "Bölgesel felaket sırasında da veri OKUNABİLSİN"                         → RA-GRS / RA-GZRS
          • "Hem zone arızasında kesintisiz erişim hem bölgesel felaket koruması"   → GZRS (okuma da gerekiyorsa RA-GZRS)
        İkincil bölge her zaman birincilin region pair'idir (→ C1.4); ikincil bölgeye replikasyon ASENKRONDUR.
        """);
    }

    private static string Mark(bool ok) => ok ? "✔" : "✘";
}
