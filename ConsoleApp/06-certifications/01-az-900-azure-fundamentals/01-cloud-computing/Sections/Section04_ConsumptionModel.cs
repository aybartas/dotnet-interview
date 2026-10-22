using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudComputing;

/// <summary>
/// CapEx vs OpEx: aynı 12 aylık talebi kendi donanımınla (zirveye göre boyutlanmış, peşin ödenmiş) ve
/// tüketim bazlı bulutla (yalnızca kullanılan kapasite kadar) karşılamak. Fiyatlar ÖRNEKTİR, gerçek fiyat değildir.
/// </summary>
public static class Section04_ConsumptionModel
{
    // Aylık ihtiyaç duyulan sunucu sayısı (ör. bir e-ticaret sitesi: kasım-aralık zirvesi)
    private static readonly int[] DemandPerMonth = [4, 4, 5, 4, 4, 5, 5, 4, 6, 8, 16, 20];
    private static readonly string[] Months = ["Oca", "Şub", "Mar", "Nis", "May", "Haz", "Tem", "Ağu", "Eyl", "Eki", "Kas", "Ara"];

    private const decimal ServerPurchasePrice = 120_000m;      // bir sunucunun peşin bedeli (örnek, TL)
    private const int DepreciationYears = 4;                     // donanımın kullanım ömrü
    private const decimal OnPremMonthlyRunCostPerServer = 900m;  // enerji, soğutma, bakım (örnek)
    private const decimal CloudMonthlyPricePerServer = 4_500m;   // eşdeğer bulut VM'inin aylık pay-as-you-go bedeli (örnek)

    public static void Run()
    {
        var peak = DemandPerMonth.Max();

        // On-prem: zirve kadar sunucu peşin alınır; yıllık amortisman + her ay tüm sunucuların işletme maliyeti
        var upfront = peak * ServerPurchasePrice;
        var onPremYear = upfront / DepreciationYears + peak * OnPremMonthlyRunCostPerServer * 12;
        var idleServerMonths = DemandPerMonth.Sum(d => peak - d);

        // Bulut: her ay yalnızca o ayın ihtiyacı kadar
        var cloudYear = DemandPerMonth.Sum(d => d * CloudMonthlyPricePerServer);

        Console.WriteLine("Aylık sunucu ihtiyacı: " + string.Join(" ", Months.Zip(DemandPerMonth, (m, d) => $"{m}:{d}")));
        Console.WriteLine($"Zirve: {peak} sunucu (Aralık)\n");

        Table.Print(["", "Kendi datacenter'ın (CapEx)", "Bulut — tüketim bazlı (OpEx)"],
        [
            ["Baştaki ödeme", upfront.ToString("C0"), "0 ₺"],
            ["Kapasite", $"Her ay {peak} sunucu (zirveye göre)", "Her ay o ayın ihtiyacı kadar"],
            ["Boşta bekleyen sunucu-ay", idleServerMonths.ToString(), "0"],
            ["Yıllık maliyet (örnek)", $"{onPremYear:C0}  (amortisman + işletme)", $"{cloudYear:C0}"],
            ["Muhasebe", "Varlık; yıllara yayılan amortisman", "Dönem gideri"],
        ]);

        Console.WriteLine($"""

        Gözlem: yükün {peak / (double)DemandPerMonth.Min():0}× dalgalandığı bu iş yükünde on-prem kapasitenin %{100.0 * idleServerMonths / (peak * 12):0}'i yıl boyunca boşta.
        Tüketim bazlı modelde boşta kapasite için ödeme yapılmaz; tahmin hatası riski (az ya da fazla alım) ortadan kalkar.
        Ama dikkat: yük SABİT ve 7/24 ise fark kapanır — o zaman reserved instance gibi taahhütlü fiyatlar devreye girer (5. bölüm).

        CapEx (capital expenditure): fiziksel altyapıya baştan yapılan, yıllara yayılan sabit yatırım.
        OpEx (operational expenditure): hizmet/ürün için dönem dönem ödenen, kullanıma göre değişen gider.
        Bulut, harcamayı CapEx'ten OpEx'e kaydırır. (Bu hesaplamadaki tüm fiyatlar örnektir.)
        """);
    }
}
