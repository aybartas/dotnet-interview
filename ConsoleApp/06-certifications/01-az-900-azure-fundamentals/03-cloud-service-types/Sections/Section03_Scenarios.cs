using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudServiceTypes;

/// <summary>Senaryo sorularında hizmet türünü seçtiren ifadeler.</summary>
public static class Section03_Scenarios
{
    public static void Run()
    {
        Table.Print(["Senaryodaki ifade", "Tür", "Neden"],
        [
            ["\"İşletim sistemine tam erişim / özel sürücü kurulacak\"", "IaaS", "OS'u yalnızca IaaS'ta sen yönetirsin"],
            ["\"Mevcut sunucuları değiştirmeden buluta taşı\" (lift-and-shift)", "IaaS", "Uygulama aynen VM'e taşınır"],
            ["\"Geçici test ortamı hızla açılıp kapatılsın\"", "IaaS", "VM'ler dakikada açılır, silinince ücret biter"],
            ["\"Geliştiriciler yalnızca kod yazsın, OS yaması dert olmasın\"", "PaaS", "Runtime ve OS Microsoft'ta"],
            ["\"Yönetilen veritabanı; yedek ve yama otomatik\"", "PaaS", "Azure SQL Database, Cosmos DB"],
            ["\"Kod yalnızca bir dosya yüklendiğinde çalışsın, boşta ücret olmasın\"", "Serverless", "Olay tetiklemeli, çağrı başına ücret"],
            ["\"Kod yazmadan iki SaaS arasında iş akışı kur\"", "Serverless", "Logic Apps (low-code)"],
            ["\"Kullanıcılar e-posta ve ofis uygulamalarını hemen kullansın\"", "SaaS", "Hazır uygulama: Microsoft 365"],
            ["\"En az yönetim yükü, hazır CRM\"", "SaaS", "Dynamics 365"],
        ]);

        Console.WriteLine("""

        Karar sırası (yukarıdan aşağı ilk "evet"te dur):
          1. İhtiyacı karşılayan HAZIR bir uygulama var mı?             → SaaS
          2. Kendi kodun var, olayla/aralıklı mı çalışıyor?              → Serverless (Functions, Logic Apps)
          3. Kendi kodun var, OS'a erişim gerekmiyor mu?                 → PaaS (App Service, SQL Database)
          4. OS/ağ üzerinde tam kontrol veya aynen taşıma mı gerekiyor?  → IaaS (VM)
        "En az yönetim yükü" sorusunda dikkat: hazır uygulama varsa SaaS; kendi kodunu çalıştıracaksan PaaS/serverless.
        """);
    }
}
