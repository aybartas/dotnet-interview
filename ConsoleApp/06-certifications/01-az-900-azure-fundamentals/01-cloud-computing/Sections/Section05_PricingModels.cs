using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudComputing;

/// <summary>Aynı VM boyutu için fiyatlandırma modelleri. Saatlik fiyatlar ÖRNEKTİR (oranlar gerçeğe yakın, rakamlar değil).</summary>
public static class Section05_PricingModels
{
    private const decimal PayAsYouGoHourly = 4.00m;     // örnek ₺/saat
    private const decimal Reserved1YearHourly = 2.40m;  // ~%40 indirim (örnek)
    private const decimal Reserved3YearHourly = 1.60m;  // ~%60 indirim (örnek)
    private const decimal SpotHourly = 0.80m;           // boşta kapasite, kesilebilir (örnek)
    private const int HoursPerMonth = 730;

    public static void Run()
    {
        Console.WriteLine("Aynı VM'in aylık maliyeti, kullanım saatine göre (örnek fiyatlar):\n");

        int[] usageHours = [730, 300, 100];
        Table.Print(["Model", "Taahhüt", .. usageHours.Select(h => $"{h} sa/ay")],
        [
            ["Pay-as-you-go", "Yok", .. usageHours.Select(h => (h * PayAsYouGoHourly).ToString("C0"))],
            ["Reserved 1 yıl", "1 yıl, saat kullanılmasa da ödenir", .. usageHours.Select(_ => (HoursPerMonth * Reserved1YearHourly).ToString("C0"))],
            ["Reserved 3 yıl", "3 yıl", .. usageHours.Select(_ => (HoursPerMonth * Reserved3YearHourly).ToString("C0"))],
            ["Spot", "Yok; Azure kapasite isteyince VM kesilir", .. usageHours.Select(h => (h * SpotHourly).ToString("C0"))],
        ]);

        var breakEven = HoursPerMonth * Reserved1YearHourly / PayAsYouGoHourly;
        Console.WriteLine($"""

        Başabaş: ayda ~{breakEven:0} saatten fazla çalışan VM için 1 yıllık reservation pay-as-you-go'dan ucuz.

        Hangi model ne zaman?
          • Pay-as-you-go   : değişken/öngörülemeyen yük, deneme, kısa süreli proje — esneklik.
          • Reservation / savings plan : 7/24 çalışan, yıllarca sürecek SABİT iş yükü — taahhüt karşılığı indirim.
          • Spot            : kesintiye dayanıklı toplu iş, render, test — en ucuz, ama her an geri alınabilir.
          • Free tier / ücretsiz hesap kredisi, dev/test fiyatlandırması: öğrenme ve geliştirme ortamları.
          Lisans tasarrufu (Azure Hybrid Benefit) ve diğer maliyet araçları → C1.11.
        """);
    }
}
