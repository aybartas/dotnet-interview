using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

public static class Section02_SlaDowntime
{
    public static void Run()
    {
        Console.WriteLine("SLA (Service Level Agreement) yüzdesi, bir ayda izin verilen en fazla kesinti demektir (ay = 730 saat):\n");

        double[] slas = [0.99, 0.999, 0.9995, 0.9999, 0.99999];
        Table.Print(SlaCalculator.DowntimeHeaders, slas.Select(SlaCalculator.DowntimeRow));

        Console.WriteLine("""

        Okuma pratiği:
          • Her "9" kesinti bütçesini 10'a böler: %99,9 → ~43 dk/ay, %99,99 → ~4 dk/ay.
          • %99,9 → %99,95 farkı küçük görünür ama aylık kesinti bütçesi yarıya iner (~43 → ~22 dk).

        SLA hakkında sınavın sorduğu kurallar:
          • SLA, Microsoft'un çalışma süresi TAAHHÜDÜDÜR; tutturulamazsa müşteri SERVİS KREDİSİ (faturada indirim) talep edebilir.
          • SLA'lar ürüne ve yapılandırmaya göre değişir (ör. tek VM < availability set < availability zone'a yayılmış VM'ler).
          • Preview (önizleme) servislerin SLA'sı yoktur; ücretsiz katmanlarda da genellikle SLA yoktur.
          • SLA'yı yükseltmenin yolu yedeklilik: zone'lara/bölgelere yaymak — karşılığında maliyet ve karmaşıklık artar.

        Kendi değerin için: dotnet run -- C1.2 kesinti 99.95
        """);
    }
}
