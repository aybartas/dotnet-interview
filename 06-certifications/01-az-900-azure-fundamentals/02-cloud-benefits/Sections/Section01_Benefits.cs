using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

public static class Section01_Benefits
{
    public static void Run()
    {
        Console.WriteLine("Resmî hedefteki sekiz fayda, dört çift halinde:\n");
        Table.Print(["Fayda", "Tek cümlelik tanım", "Azure örneği"],
        [
            ["High availability", "Arıza olsa da hizmet hedeflenen çalışma süresinde kalır", "SLA, availability zone, scale set"],
            ["Scalability",       "Kapasite talebe göre artırılıp azaltılabilir (up/out)",   "VM boyutu, VM Scale Sets, App Service plan"],
            ["Reliability",       "Arızadan/felaketten toparlanıp çalışmaya devam eder",     "Region pair, GRS storage, Site Recovery"],
            ["Predictability",    "Performans VE maliyet önceden kestirilebilir",           "Autoscale + LB / Pricing Calculator, Cost Mgmt"],
            ["Security",          "Güvenlik kontrolleri üzerinde istenen düzeyde kontrol",  "Defender for Cloud, Key Vault, DDoS Protection"],
            ["Governance",        "Kurumsal standartlara ve regülasyonlara uyum",            "Azure Policy, management group, Purview"],
            ["Manageability",     "Bulutun yönetimi (of) ve bulutta yönetim (in)",           "Monitor, ARM/Bicep / portal, CLI, PowerShell"],
            ["Elasticity",        "Ölçeklemenin yüke göre OTOMATİK yapılması",               "Autoscale kuralları, Functions"],
        ]);

        Console.WriteLine("""

        Karıştırılan çiftler (sınav bunları birbirine şık yapar):
          • High availability ↔ Reliability
              HA = çalışmayı SÜRDÜRMEK (kesinti süresini küçültmek, SLA).
              Reliability = arızadan TOPARLANMAK (dağıtık tasarım, felaket kurtarma).
          • Scalability ↔ Elasticity
              Scalability = YETENEK (kaynak eklenebilir/çıkarılabilir; elle de olabilir).
              Elasticity  = bu yeteneğin yüke göre OTOMATİK kullanılması.
          • Security ↔ Governance
              Security = saldırıya karşı koruma ve kontrol düzeyi (IaaS'ta çok, SaaS'ta az kontrol).
              Governance = kurallara/standartlara uyum ve denetim (Policy, şablon, uyumluluk raporu).
          • Predictability'nin İKİ yüzü vardır: performans öngörülebilirliği VE maliyet öngörülebilirliği.
          • Manageability'nin İKİ yüzü vardır:
              management OF the cloud = kaynakları yönetmek: otomatik ölçek, izleme/uyarı, şablonla dağıtım.
              management IN the cloud = yönetim arayüzleri: portal, CLI, PowerShell, REST API.
        """);
    }
}
