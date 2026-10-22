using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudComputing;

public static class Section03_CloudModels
{
    private sealed record Scenario(string Description, string Answer, string Why);

    public static void Run()
    {
        Table.Print(["Model", "Altyapı kimin?", "Artı", "Eksi"],
        [
            ["Public cloud", "Sağlayıcının; birçok müşteri paylaşır", "CapEx yok, hızlı, neredeyse sınırsız ölçek, kullandıkça öde", "Fiziksel kontrol ve donanım seçimi yok"],
            ["Private cloud", "Tek kuruluşun (kendi DC'si veya ayrılmış)", "Tam kontrol, özel uyumluluk/regülasyon", "Donanım, bakım, kapasite yine sende (CapEx)"],
            ["Hybrid cloud", "İkisi birlikte, aralarında bağlantı", "Esneklik: hassas/eski sistem içeride, değişken yük bulutta", "Yönetim karmaşıklığı, iki ortamın güvenliği"],
            ["Multicloud (bağlam)", "Birden çok public sağlayıcı", "Sağlayıcı bağımlılığını azaltma, en iyi servisi seçme", "Farklı araçlar/beceriler; Azure Arc ile ortak yönetim"],
        ]);

        Scenario[] scenarios =
        [
            new("Regülasyon gereği müşteri verisi kurum içindeki veri merkezinde kalmalı; web sitesi ise bayram kampanyalarında 10 kat yük alıyor.",
                "Hybrid", "Veri private/on-prem tarafta, ani yük bulutta (cloud bursting)."),
            new("Girişim; ön yatırım bütçesi yok, ürünü haftalar içinde dünyaya açmak istiyor.",
                "Public", "Ön yatırım yok, hızlı başlangıç, küresel bölgeler."),
            new("Savunma kurumu; donanım ve ağ üzerinde tam fiziksel kontrol istiyor, internet bağlantısı kısıtlı.",
                "Private", "Tam kontrol ve izolasyon; maliyet ve bakım kuruluşta."),
            new("Şirket iki farklı bulut sağlayıcısında iş yükü çalıştırıyor ve hepsini tek yerden yönetmek istiyor.",
                "Multicloud (+ Azure Arc)", "Arc, Azure dışındaki kaynakları Azure Resource Manager'dan yönetir (→ C1.13)."),
        ];

        Console.WriteLine("\nSenaryo → model:");
        foreach (var s in scenarios)
        {
            Console.WriteLine($"   • {s.Description}");
            Console.WriteLine($"     → {s.Answer}: {s.Why}");
        }
    }
}
