using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudComputing;

public static class Section01_WhatIsCloud
{
    public static void Run()
    {
        Console.WriteLine("Cloud computing: bilgi işlem hizmetlerinin (sunucu, depolama, ağ, veritabanı, yazılım, analitik, AI)");
        Console.WriteLine("İNTERNET ÜZERİNDEN, İHTİYAÇ ANINDA ve KULLANIM KADAR ÜCRETLE sunulması.\n");

        Console.WriteLine("NIST'in beş temel niteliği:");
        Table.Print(["Nitelik", "Anlamı", "Azure'da örnek"],
        [
            ["On-demand self-service", "Kimseye sormadan, kendin kaynak açarsın", "Portal/CLI ile 2 dakikada VM"],
            ["Broad network access", "Ağ üzerinden, her cihazdan erişim", "Portal, REST API, mobil uygulama"],
            ["Resource pooling", "Fiziksel kaynaklar müşteriler arasında paylaşılır", "Çok kiracılı (multi-tenant) datacenter'lar"],
            ["Rapid elasticity", "Kapasite hızla (otomatik) büyür/küçülür", "Autoscale, VM Scale Sets"],
            ["Measured service", "Kullanım ölçülür, ona göre ödenir", "Saniye/saat/işlem bazlı faturalama"],
        ]);

        Console.WriteLine("\nGeleneksel datacenter ile karşılaştırma (yeni bir web sunucusu ihtiyacı):");
        Table.Print(["", "Kendi datacenter'ın", "Bulut"],
        [
            ["Kaynağı hazır etme süresi", "Haftalar (satın alma, kurulum)", "Dakikalar"],
            ["Ön yatırım", "Donanım + lisans (CapEx)", "Yok — kullandıkça öde (OpEx)"],
            ["Kapasite planlama", "Zirveye göre tahmin; fazlası boşa", "İhtiyaç kadar; artır/azalt"],
            ["Bakım, enerji, fiziksel güvenlik", "Sende", "Sağlayıcıda"],
            ["Küresel erişim", "Yeni datacenter gerekir", "Başka bölgede dakikalar içinde"],
        ]);
    }
}
