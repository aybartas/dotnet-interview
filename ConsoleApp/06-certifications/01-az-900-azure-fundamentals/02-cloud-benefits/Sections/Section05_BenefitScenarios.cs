using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

/// <summary>Sınavda fayda soruları çoğunlukla "bu senaryo hangi faydayı anlatıyor?" kalıbındadır. Anahtar kelimeyi yakala.</summary>
public static class Section05_BenefitScenarios
{
    public static void Run()
    {
        Console.WriteLine("Senaryodaki anahtar ifade → doğru fayda:\n");
        Table.Print(["Senaryo", "Anahtar ifade", "Fayda"],
        [
            ["Bir datacenter'da elektrik kesildi, site başka zone'dan yayında kaldı", "kesintiye rağmen çalışmaya devam", "High availability"],
            ["Kampanya için 2 VM'den 20 VM'e çıkılabildi", "kapasite eklenebilir", "Scalability"],
            ["CPU %70'i geçince instance sayısı kendiliğinden arttı", "otomatik, yüke göre", "Elasticity"],
            ["Bölgesel felaketten sonra sistem eşleşik bölgede ayağa kalktı", "felaketten toparlanma", "Reliability"],
            ["Önümüzdeki çeyreğin bulut faturası tahmin edilebildi", "maliyeti önceden kestirmek", "Predictability (maliyet)"],
            ["Trafik artsa da yanıt süresi hedefte kaldı (autoscale + LB)", "performansı önceden kestirmek", "Predictability (performans)"],
            ["PaaS'ta OS yamalarını Microsoft otomatik uyguladı", "yamalar/koruma sağlayıcıda", "Security"],
            ["Yalnızca AB bölgelerinde kaynak açılmasına izin veriliyor", "standart/regülasyona uyum", "Governance"],
            ["Kaynaklar şablonla dağıtılıyor, metriklere göre uyarı geliyor", "bulutun yönetimi (of)", "Manageability"],
            ["Ekip kaynakları portal, CLI ve REST API ile yönetiyor", "bulutta yönetim (in)", "Manageability"],
        ]);

        Console.WriteLine("""

        Taktik: önce fiile bak — "devam etti" (HA), "toparlandı" (reliability), "eklenebildi" (scalability),
        "kendiliğinden/otomatik" (elasticity), "tahmin edildi" (predictability), "uyum/kural" (governance),
        "koruma/yama/tehdit" (security), "araç/izleme/şablon" (manageability).
        """);
    }
}
