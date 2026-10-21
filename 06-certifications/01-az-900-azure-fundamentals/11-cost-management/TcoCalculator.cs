using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CostManagement;

/// <summary>
/// 3 yıllık toplam sahip olma maliyeti (TCO): kendi datacenter'ın (CapEx ağırlıklı) vs Azure (OpEx).
/// TÜM fiyatlar ÖRNEKTİR (USD); amaç kalemlerin yapısını görmek, gerçek teklif değil.
/// </summary>
public static class TcoCalculator
{
    private const int Years = 3;

    // On-prem (sunucu başına)
    private const decimal ServerHardware = 9_000m;          // peşin: sunucu (CapEx)
    private const decimal StorageNetworkPerServer = 3_000m; // peşin: depolama + ağ payı (CapEx)
    private const decimal LicensePerServer = 2_000m;        // peşin: OS/sanallaştırma lisansı (CapEx)
    private const decimal PowerCoolingPerYear = 1_100m;     // enerji + soğutma
    private const decimal SpacePerYear = 600m;              // raf/bina alanı
    private const decimal MaintenancePerYear = 900m;        // donanım bakım sözleşmesi
    private const decimal LaborPerYear = 2_500m;            // donanım/OS işletme emeği payı

    // Azure (sunucu başına eşdeğer VM)
    private const decimal VmPayAsYouGoPerMonth = 520m;    // ~8 vCPU / 32 GB Windows VM (örnek)
    private const decimal Reserved3YearDiscount = 0.55m;    // 3 yıllık reservation indirimi (örnek)
    private const decimal HybridBenefitDiscount = 0.30m;    // mevcut Windows Server lisansıyla (örnek)
    private const decimal StorageNetworkPerMonth = 45m;     // managed disk + egress payı
    private const decimal CloudLaborPerYear = 1_000m;       // donanım işi yok; OS/yapılandırma emeği kalır

    public sealed record Line(string Item, string Kind, decimal[] PerYear)
    {
        public decimal Total => PerYear.Sum();
    }

    public static IReadOnlyList<Line> OnPrem(int servers) =>
    [
        new("Donanım (sunucu)", "CapEx", Upfront(servers * ServerHardware)),
        new("Depolama + ağ ekipmanı", "CapEx", Upfront(servers * StorageNetworkPerServer)),
        new("Yazılım lisansları", "CapEx", Upfront(servers * LicensePerServer)),
        new("Enerji + soğutma", "OpEx", Yearly(servers * PowerCoolingPerYear)),
        new("Datacenter alanı", "OpEx", Yearly(servers * SpacePerYear)),
        new("Donanım bakımı", "OpEx", Yearly(servers * MaintenancePerYear)),
        new("BT işletme emeği", "OpEx", Yearly(servers * LaborPerYear)),
    ];

    public static IReadOnlyList<Line> Cloud(int servers, bool reserved, bool hybridBenefit)
    {
        var vm = VmPayAsYouGoPerMonth * (reserved ? 1 - Reserved3YearDiscount : 1) * (hybridBenefit ? 1 - HybridBenefitDiscount : 1);
        var label = (reserved ? "3 yıl reservation" : "pay-as-you-go") + (hybridBenefit ? " + Hybrid Benefit" : "");
        return
        [
            new($"VM'ler ({label})", "OpEx", Yearly(servers * vm * 12)),
            new("Disk + ağ çıkışı (egress)", "OpEx", Yearly(servers * StorageNetworkPerMonth * 12)),
            new("BT işletme emeği", "OpEx", Yearly(servers * CloudLaborPerYear)),
        ];
    }

    public static int Print(int servers)
    {
        var onPrem = OnPrem(servers);
        Console.WriteLine($"{servers} sunuculuk iş yükü, {Years} yıl — kalem kalem (örnek fiyatlar, USD):\n");
        PrintLines("Kendi datacenter'ın", onPrem);

        (string Name, IReadOnlyList<Line> Lines)[] cloudOptions =
        [
            ("Azure — pay-as-you-go", Cloud(servers, reserved: false, hybridBenefit: false)),
            ("Azure — 3 yıl reservation", Cloud(servers, reserved: true, hybridBenefit: false)),
            ("Azure — reservation + Azure Hybrid Benefit", Cloud(servers, reserved: true, hybridBenefit: true)),
        ];
        foreach (var (name, lines) in cloudOptions) PrintLines(name, lines);

        Console.WriteLine("Kümülatif maliyet:\n");
        Table.Print(["Seçenek", .. Enumerable.Range(1, Years).Select(y => $"{y}. yıl sonu"), "1. yılın toplamdaki payı"],
        [
            Cumulative("Kendi datacenter'ın", onPrem),
            .. cloudOptions.Select(o => Cumulative(o.Name, o.Lines)),
        ]);

        var onPremTotal = onPrem.Sum(l => l.Total);
        var best = cloudOptions.MinBy(o => o.Lines.Sum(l => l.Total));
        var bestTotal = best.Lines.Sum(l => l.Total);
        Console.WriteLine($"""

        En düşük bulut seçeneği: {best.Name} → {bestTotal:N0} $ ({(onPremTotal - bestTotal) / onPremTotal:P0} {(bestTotal < onPremTotal ? "daha ucuz" : "daha pahalı")}).
        Gözlemler:
          • On-prem'in büyük kısmı 1. yılda PEŞİN ödenir (CapEx); bulutta harcama aylara yayılır (OpEx).
          • Sabit 7/24 yükte pay-as-you-go en pahalı bulut seçeneğidir; reservation ve Hybrid Benefit farkı kapatır.
          • TCO'da yalnızca sunucu fiyatı değil enerji, alan, bakım ve EMEK de vardır — on-prem kıyasında en çok unutulan kalemler.
          • Donanım 3 yıldan uzun kullanılırsa (amortisman 5 yıl gibi) on-prem'in yıllık maliyeti düşer ve fark daralır;
            süre, kullanım oranı ve emek varsayımları sonucu belirler.
          • Bu örnek fiyatlarla yapılmış bir yapı gösterimidir; gerçek karşılaştırma için Pricing Calculator ve
            Azure Migrate business case kullanılır (README 3.2).
        """);
        return 0;
    }

    private static void PrintLines(string title, IReadOnlyList<Line> lines)
    {
        Console.WriteLine(title);
        Table.Print(["Kalem", "Tür", .. Enumerable.Range(1, Years).Select(y => $"{y}. yıl"), "Toplam"],
        [
            .. lines.Select(l => (IReadOnlyList<string>)[l.Item, l.Kind, .. l.PerYear.Select(Money), Money(l.Total)]),
            ["TOPLAM", "", .. Enumerable.Range(0, Years).Select(y => Money(lines.Sum(l => l.PerYear[y]))), Money(lines.Sum(l => l.Total))],
        ]);
        Console.WriteLine();
    }

    private static IReadOnlyList<string> Cumulative(string name, IReadOnlyList<Line> lines)
    {
        var running = 0m;
        var cells = Enumerable.Range(0, Years).Select(y => Money(running += lines.Sum(l => l.PerYear[y]))).ToList();
        var total = lines.Sum(l => l.Total);
        var firstYearShare = lines.Sum(l => l.PerYear[0]) / total;
        return [name, .. cells, firstYearShare.ToString("P0")];
    }

    private static decimal[] Upfront(decimal amount) => [amount, .. Enumerable.Repeat(0m, Years - 1)];
    private static decimal[] Yearly(decimal amount) => [.. Enumerable.Repeat(amount, Years)];
    private static string Money(decimal value) => value.ToString("N0") + " $";
}
