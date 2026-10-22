using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.Storage;

/// <summary>
/// Access tier'ları: depolama ucuzladıkça erişim pahalılaşır. Fiyatlar ÖRNEKTİR (oranlar gerçeğe yakın, rakamlar değil);
/// güncel fiyat için Pricing Calculator.
/// </summary>
public static class Section03_AccessTiers
{
    private sealed record Tier(string Name, string Use, int MinDays, string FirstByte, decimal StoragePerGbMonth, decimal ReadPer10k);

    private static readonly Tier[] Tiers =
    [
        new("Hot",     "Sık erişilen veri",                  0,   "milisaniye", 0.020m, 0.005m),
        new("Cool",    "Seyrek erişim, en az 30 gün",        30,  "milisaniye", 0.010m, 0.010m),
        new("Cold",    "Nadir erişim, en az 90 gün",         90,  "milisaniye", 0.004m, 0.100m),
        new("Archive", "Çok nadir, en az 180 gün, ÇEVRİMDIŞI", 180, "saatler (rehydrate)", 0.001m, 5.000m),
    ];

    public static void Run()
    {
        Table.Print(["Tier", "Kullanım", "Önerilen en az süre", "İlk byte", "Depolama $/GB-ay*", "10.000 okuma $*"],
            Tiers.Select(t => (IReadOnlyList<string>)
            [
                t.Name, t.Use, t.MinDays == 0 ? "—" : $"{t.MinDays} gün", t.FirstByte,
                t.StoragePerGbMonth.ToString("0.000"), t.ReadPer10k.ToString("0.000"),
            ]));

        const int sizeGb = 1_000;            // 1 TB yedek
        long[] readsPerMonth = [1_000, 1_000_000, 100_000_000];
        Console.WriteLine($"\n{sizeGb:N0} GB veri için aylık örnek maliyet (depolama + okuma işlemi; veri çıkışı/rehydrate ücreti hariç):\n");
        Table.Print(["Aylık okuma", .. Tiers.Select(t => t.Name), "En ucuz"],
            readsPerMonth.Select(reads =>
            {
                var costs = Tiers.Select(t => sizeGb * t.StoragePerGbMonth + reads / 10_000m * t.ReadPer10k).ToArray();
                var cheapest = Tiers[Array.IndexOf(costs, costs.Min())].Name;
                return (IReadOnlyList<string>)[reads.ToString("N0"), .. costs.Select(c => $"{c:0.00} $"), cheapest];
            }));

        Console.WriteLine("""

        * Örnek fiyatlar. Asıl ders: depolama ucuzladıkça ERİŞİM pahalılaşır — sık okunan veriyi Archive'a koymak pahalıdır.
          (Archive'da her okuma ayrıca rehydrate gerektirir; tablodaki okuma maliyeti bunu basitleştirir.)

        Kurallar:
          • Archive ÇEVRİMDIŞIDIR: blob okunmadan önce Hot/Cool/Cold'a "rehydrate" edilmelidir — standart öncelikte
            saatler sürebilir (yüksek öncelik daha hızlı, daha pahalı).
          • Cool/Cold/Archive'da en az süre dolmadan silmek veya tier değiştirmek "erken silme" ücreti doğurur.
          • Tier blob düzeyinde ayarlanır; hesap düzeyinde bir varsayılan tier seçilir — Archive varsayılan olamaz.
          • Lifecycle management kuralları: "30 gün dokunulmayan blob → Cool, 180 gün → Archive" gibi otomatik geçişler.
        """);
    }
}
