using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudComputing;

public static class Section02_SharedResponsibility
{
    public static void Run()
    {
        Console.WriteLine("Which layer is whose responsibility? (Microsoft's shared responsibility table)\n");
        Table.Print(["Layer", "On-prem", "IaaS", "PaaS", "SaaS"],
            ResponsibilityMatrix.Layers.Select(l => (IReadOnlyList<string>)
            [
                l.Name,
                ResponsibilityMatrix.Label(l.OnPrem),
                ResponsibilityMatrix.Label(l.IaaS),
                ResponsibilityMatrix.Label(l.PaaS),
                ResponsibilityMatrix.Label(l.SaaS),
            ]));

        Console.WriteLine("""

        Three rules (remember these instead of memorizing the table):
          1. Data, devices, accounts and identities are ALWAYS the customer's — even in SaaS.
          2. Physical host, physical network and datacenter are ALWAYS Microsoft's in the cloud.
          3. The ones in between (OS, network controls, applications, identity infrastructure) shift to Microsoft going IaaS → PaaS → SaaS.

        Exam-style examples:
        """);

        foreach (var (layer, model) in new[] { ("account", "saas"), ("os", "iaas"), ("os", "paas"), ("physical-network", "iaas"), ("app", "paas"), ("data", "paas") })
        {
            Console.Write("   ");
            ResponsibilityMatrix.Answer(layer, model);
        }
    }
}
