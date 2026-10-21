using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

/// <summary>
/// Aynı 24 saatlik yükü dört stratejiyle karşılamak: sabit küçük, sabit büyük (zirveye göre),
/// elle/zamanlanmış ölçekleme (scalability) ve otomatik ölçekleme (elasticity). Birim: "instance" (ör. VM veya App Service instance'ı).
/// İkinci gün aynı yük + beklenmedik bir kampanya zirvesi: elasticity'nin asıl farkı orada görünür.
/// </summary>
public static class Section04_ScalingSimulation
{
    // Saat başına gereken instance sayısı (gündüz yoğun bir iş uygulaması)
    private static readonly int[] NormalDay = [2, 2, 2, 2, 2, 3, 4, 6, 8, 9, 10, 10, 9, 9, 10, 10, 9, 8, 7, 6, 5, 4, 3, 2];
    // Aynı gün, 13:00-16:00 arası habersiz başlayan bir kampanya
    private static readonly int[] SpikeDay = [2, 2, 2, 2, 2, 3, 4, 6, 8, 9, 10, 10, 9, 15, 18, 18, 12, 8, 7, 6, 5, 4, 3, 2];

    /// <summary>Plan: o günün ihtiyacına göre 24 saatlik kapasite dizisini üretir.</summary>
    private sealed record Strategy(string Name, string Kind, Func<int[], int[]> Plan);

    private static int[] EveryHour(Func<int, int> capacity) => [.. Enumerable.Range(0, 24).Select(capacity)];

    /// <summary>
    /// Autoscale kuralı metriğe birkaç dakikada bir bakar: kapasiteyi ihtiyaç + 1 (pay) yapar, ama cool-down nedeniyle
    /// bir saatte en fazla 4 instance ekleyebilir; azaltmada sınır yok. min 2, max 20.
    /// </summary>
    private static int[] Autoscale(int[] need)
    {
        var caps = new int[24];
        var previous = 2;
        for (var h = 0; h < 24; h++)
            previous = caps[h] = Math.Clamp(Math.Min(need[h] + 1, previous + 4), 2, 20);
        return caps;
    }

    private static readonly Strategy[] Strategies =
    [
        new("Sabit 4", "ölçekleme yok", _ => EveryHour(_ => 4)),
        new("Sabit 10", "zirveye göre boyutlama", _ => EveryHour(_ => 10)),
        new("Elle", "scalability: 08:00'de 10'a çıkar, 20:00'de 4'e in", _ => EveryHour(h => h is >= 8 and < 20 ? 10 : 4)),
        new("Autoscale", "elasticity: ihtiyaç + 1, saatte en fazla +4 (min 2, max 20)", Autoscale),
    ];

    public static void Run()
    {
        Simulate("1. gün — her günkü yük", NormalDay);
        Simulate("2. gün — 13:00'te habersiz kampanya", SpikeDay);

        Console.WriteLine("""

        Gözlemler:
          • Sabit küçük kapasite ucuz ama gündüz yetersiz; sabit büyük kapasite paranın büyük kısmını boşa harcar
            ve ÖNGÖRÜLMEYEN zirvede o da yetmez.
          • Yük her gün aynıysa elle/zamanlanmış ölçekleme (scalability) autoscale'a yaklaşır; yalnızca rampa saatlerinde kaçırır.
          • Elasticity'nin asıl farkı BEKLENMEDİK yükte: autoscale kampanya zirvesini insan müdahalesi olmadan karşılar
            ve zirve bitince kendiliğinden küçülür. Ani sıçramanın ilk saati (cool-down sınırı) eksik kalabilir —
            gerçekte bunu min instance sayısı, eşikler ve öngörülen günler için zamanlanmış kurallarla kapatırsın.

        Vertical vs horizontal:
          • Scale UP/DOWN (dikey): aynı makineyi büyütmek/küçültmek (daha çok CPU/RAM). Basit; ama bir ÜST SINIRI vardır
            (en büyük VM boyutu) ve VM'de boyut değişimi genellikle yeniden başlatma gerektirir; tek makine = tek arıza noktası.
          • Scale OUT/IN (yatay): instance SAYISINI artırmak/azaltmak (VM Scale Sets, App Service instance sayısı).
            Pratikte sınırsıza yakın büyür ve HA'ya da katkı verir; uygulamanın durumsuz (stateless) tasarlanması gerekir.
        """);
    }

    private static void Simulate(string title, int[] need)
    {
        Console.WriteLine($"\n{title} — saatlik instance kapasitesi (! = ihtiyacın altında → yavaşlama/hata):\n");
        var hours = Enumerable.Range(0, 24).ToArray();
        Table.Print(["", .. hours.Select(h => h.ToString("00"))],
        [
            ["İhtiyaç", .. need.Select(n => n.ToString())],
            .. Strategies.Select(s => (IReadOnlyList<string>)[s.Name, .. s.Plan(need).Select((c, h) => Cell(c, need[h]))]),
        ], indent: " ");

        Console.WriteLine();
        Table.Print(["Strateji", "Instance-saat (maliyet)", "Yetersiz saat", "Boşta instance-saat", "Ne gösteriyor?"],
            Strategies.Select(s =>
            {
                var caps = s.Plan(need);
                return (IReadOnlyList<string>)
                [
                    s.Name,
                    caps.Sum().ToString(),
                    hours.Count(h => caps[h] < need[h]).ToString(),
                    hours.Sum(h => Math.Max(0, caps[h] - need[h])).ToString(),
                    s.Kind,
                ];
            }));
        Console.WriteLine($"   Gerçek ihtiyaç: {need.Sum()} instance-saat.");
    }

    private static string Cell(int capacity, int needed) => capacity < needed ? $"{capacity}!" : capacity.ToString();
}
