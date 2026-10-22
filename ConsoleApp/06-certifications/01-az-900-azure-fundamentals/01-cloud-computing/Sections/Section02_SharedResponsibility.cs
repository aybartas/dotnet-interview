using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudComputing;

public static class Section02_SharedResponsibility
{
    public static void Run()
    {
        Console.WriteLine("Hangi katman kimin sorumluluğunda? (Microsoft'un shared responsibility tablosu)\n");
        Table.Print(["Katman", "On-prem", "IaaS", "PaaS", "SaaS"],
            ResponsibilityMatrix.Layers.Select(l => (IReadOnlyList<string>)
            [
                l.Name,
                ResponsibilityMatrix.Label(l.OnPrem),
                ResponsibilityMatrix.Label(l.IaaS),
                ResponsibilityMatrix.Label(l.PaaS),
                ResponsibilityMatrix.Label(l.SaaS),
            ]));

        Console.WriteLine("""

        Üç kural (tabloyu ezberlemek yerine bunları hatırla):
          1. Veri, cihazlar, hesaplar ve kimlikler HER ZAMAN müşteride — SaaS'ta bile.
          2. Fiziksel host, fiziksel ağ ve datacenter bulutta HER ZAMAN Microsoft'ta.
          3. Arada kalanlar (OS, ağ kontrolleri, uygulama, kimlik altyapısı) IaaS → PaaS → SaaS geçtikçe Microsoft'a kayar.

        Sınav tarzı örnekler:
        """);

        foreach (var (layer, model) in new[] { ("hesap", "saas"), ("os", "iaas"), ("os", "paas"), ("fiziksel-ag", "iaas"), ("uygulama", "paas"), ("veri", "paas") })
        {
            Console.Write("   ");
            ResponsibilityMatrix.Answer(layer, model);
        }
    }
}
