using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CostManagement;

/// <summary>Aynı aylık VM faturasına farklı tasarruf yolları. İndirim oranları ÖRNEKTİR.</summary>
public static class Section03_Savings
{
    public static void Run()
    {
        const decimal monthlyBill = 10_000m;  // 7/24 çalışan Windows VM filosunun pay-as-you-go faturası (örnek)
        Console.WriteLine($"Başlangıç: aylık {monthlyBill:N0} $ pay-as-you-go VM faturası (7/24, Windows, üretim).\n");

        Table.Print(["Yöntem", "Nasıl?", "Örnek yeni fatura", "Uygun iş yükü / şart"],
        [
            ["Reservation (1/3 yıl)", "Belirli VM boyutu + bölge için taahhüt", Bill(monthlyBill, 0.55m), "Sabit, uzun süreli; kullanılmasa da ödenir"],
            ["Savings plan for compute", "Saatlik harcama tutarına 1/3 yıl taahhüt", Bill(monthlyBill, 0.45m), "Boyut/bölge değişebilen ama sürekli compute"],
            ["Azure Hybrid Benefit", "Mevcut Windows Server/SQL Server lisansını kullan", Bill(monthlyBill, 0.30m), "Software Assurance'lı lisanslar"],
            ["Spot VM", "Boşta kapasite; Azure geri alabilir", Bill(monthlyBill, 0.80m), "Yalnızca kesintiye dayanıklı işler"],
            ["Dev/test fiyatlandırması", "Dev/test aboneliklerinde indirimli oranlar", Bill(monthlyBill, 0.40m), "Yalnızca üretim DIŞI ortamlar"],
            ["Otomatik kapatma", "Mesai dışı VM'i deallocate et (12 sa/gün, hafta içi)", Bill(monthlyBill, 1 - (12m * 5) / (24m * 7)), "Geliştirme/test ortamları"],
            ["Doğru boyutlandırma", "Advisor'ın az kullanılan VM önerisi (→ C1.14)", Bill(monthlyBill, 0.50m), "CPU'su sürekli düşük VM'ler (bir boy küçült)"],
        ]);

        Console.WriteLine("""

        Birleştirme: reservation + Hybrid Benefit birlikte kullanılabilir (indirimler çarpılarak uygulanır).
        Spot, üretimdeki 7/24 kritik iş için doğru cevap DEĞİLDİR; dev/test fiyatı üretimde kullanılamaz.
        """);
    }

    private static string Bill(decimal bill, decimal discount) => $"{bill * (1 - discount):N0} $  (−%{discount * 100:0})";
}
