using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CostManagement;

/// <summary>Maliyet faktörleri; her birine örnek bir etki. Rakamlar ÖRNEKTİR.</summary>
public static class Section01_CostFactors
{
    public static void Run()
    {
        Table.Print(["Faktör", "Ne değiştirir?", "Örnek etki (örnek fiyatlar)"],
        [
            ["Kaynak türü ve ayarları", "SKU, boyut, tier, redundancy", "Aynı VM ailesinde 2 → 8 vCPU ≈ 4× fiyat; LRS → GZRS depolama ≈ 2×"],
            ["Tüketim modeli", "Pay-as-you-go ↔ reservation/savings plan", "7/24 VM: 3 yıl reservation ile ~%50-60 daha ucuz"],
            ["Bakım (unutulanlar)", "Kullanılmayan kaynaklar faturada kalır", "VM silindi ama disk + public IP kaldı → her ay ücret"],
            ["Coğrafya / bölge", "Aynı servisin bölgeye göre fiyatı", "Aynı VM boyutu iki bölgede %10-20 farklı olabilir"],
            ["Ağ trafiği", "Gelen (ingress) çoğunlukla ücretsiz; giden (egress) ücretli", "Bölgeler/zone'lar arası ve internete çıkan trafik GB başına ücretli"],
            ["Subscription türü", "Free/kredi, pay-as-you-go, EA, CSP", "Ücretsiz hesap kredisi; kurumsal sözleşmede indirim"],
            ["Azure Marketplace", "Üçüncü taraf ürünler", "Marketplace imajı: Azure ücreti + satıcının yazılım ücreti"],
        ]);

        // Egress hesabı: aynı veriyi kullanıcılara servis etmek
        const decimal egressPerGb = 0.08m;   // örnek: internete çıkış, ilk kademe
        const int freeGb = 100;              // örnek: aylık ücretsiz çıkış kotası
        int[] monthlyGb = [50, 1_000, 20_000];
        Console.WriteLine("\nAğ çıkışı (egress) örneği — internete servis edilen veri:\n");
        Table.Print(["Aylık çıkış", "Ücretli kısım", "Aylık egress (örnek)"],
            monthlyGb.Select(gb => (IReadOnlyList<string>)
            [
                $"{gb:N0} GB", $"{Math.Max(0, gb - freeGb):N0} GB", $"{Math.Max(0, gb - freeGb) * egressPerGb:N2} $",
            ]));

        Console.WriteLine("""

        Sınav kuralları: gelen veri (ingress) genellikle ücretsiz, giden veri (egress) ücretli; fiyat bölgeye göre değişir;
        VM'i silmek bağlı disk/IP'yi otomatik silmeyebilir — resource group'u silmek hepsini temizler (→ C1.4).
        """);
    }
}
