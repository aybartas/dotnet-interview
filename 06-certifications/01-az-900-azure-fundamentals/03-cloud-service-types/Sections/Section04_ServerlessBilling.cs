using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudServiceTypes;

/// <summary>
/// Serverless faturalandırma mantığı: (çağrı sayısı) + (çalışma süresi × bellek = GB-saniye), aylık ücretsiz kotayla.
/// Oranlar Azure Functions Consumption planının uzun süre yayımlanan oranlarına dayalı ÖRNEKLERDİR (USD);
/// güncel fiyat ve Flex Consumption planı için Pricing Calculator'a bakılır.
/// </summary>
public static class Section04_ServerlessBilling
{
    private const decimal PricePerMillionExecutions = 0.20m;
    private const decimal PricePerGbSecond = 0.000016m;
    private const decimal FreeExecutions = 1_000_000m;
    private const decimal FreeGbSeconds = 400_000m;

    private const decimal AlwaysOnMonthly = 60m;   // 7/24 açık küçük bir VM/App Service planı (örnek)
    private const double DurationSeconds = 0.3;    // çağrı başına çalışma süresi
    private const double MemoryGb = 0.5;           // ayrılan bellek

    public static void Run()
    {
        Console.WriteLine($"Fonksiyon: çağrı başına {DurationSeconds * 1000} ms, {MemoryGb * 1024} MB bellek. Karşılaştırma: 7/24 açık sunucu ~{AlwaysOnMonthly:0} $/ay (örnek).\n");

        long[] monthlyCalls = [100_000, 2_000_000, 20_000_000, 200_000_000];
        Table.Print(["Aylık çağrı", "GB-saniye", "Çağrı ücreti", "Süre ücreti", "Serverless toplam", "7/24 açık", "Ucuz olan"],
            monthlyCalls.Select(calls =>
            {
                var gbSeconds = (decimal)(calls * DurationSeconds * MemoryGb);
                var execCost = Math.Max(0, calls - FreeExecutions) / 1_000_000m * PricePerMillionExecutions;
                var durationCost = Math.Max(0, gbSeconds - FreeGbSeconds) * PricePerGbSecond;
                var total = execCost + durationCost;
                return (IReadOnlyList<string>)
                [
                    calls.ToString("N0"),
                    gbSeconds.ToString("N0"),
                    $"{execCost:0.00} $",
                    $"{durationCost:0.00} $",
                    $"{total:0.00} $",
                    $"{AlwaysOnMonthly:0} $",
                    total < AlwaysOnMonthly ? "Serverless" : "7/24 açık*",
                ];
            }));

        Console.WriteLine("""

        * Çok yüksek ve SÜREKLİ yükte 7/24 açık kaynak ucuzlaşır — ama aylık 200 milyon çağrıyı tek küçük sunucu taşıyamaz;
          gerçek karşılaştırmada gereken instance sayısı da hesaba katılır (→ C1.11, Pricing Calculator).

        Serverless'in nitelikleri (sınavın aradığı ifadeler):
          • Sunucu YOK değil — sunucuları sağlamak, ölçeklemek ve yönetmek tamamen sağlayıcıda.
          • Olay tetiklemeli (HTTP isteği, kuyruk mesajı, dosya yükleme, zamanlayıcı).
          • Otomatik ölçekleme, boşta SIFIRA kadar; boşta ücret yok (yalnızca çalışma süresi/çağrı başına).
          • Trade-off: sıfırdan uyanırken "cold start" gecikmesi; uzun süren işler için çalışma süresi sınırları.
        """);
    }
}
