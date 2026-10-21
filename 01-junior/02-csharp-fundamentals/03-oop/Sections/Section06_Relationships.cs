using System.Runtime.CompilerServices;
using Junior.CSharpFundamentals.Oop.Relationships;

namespace Junior.CSharpFundamentals.Oop;

public static class Section06_Relationships
{
    public static void Run()
    {
        Console.WriteLine("""
        ┌──────────────┬────────────────┬──────────────────────────────┬──────────────────────┐
        │ İlişki       │ Anlam          │ Yaşam süresi                 │ Örnek                │
        ├──────────────┼────────────────┼──────────────────────────────┼──────────────────────┤
        │ Inheritance  │ "is-a"         │ —                            │ SavingsAccount:Account│
        │ Association  │ "uses-a"       │ Bağımsız, sahiplik yok       │ Order ↔ Courier      │
        │ Aggregation  │ zayıf "has-a"  │ Parça bütünden UZUN yaşar    │ Team ↔ Employee      │
        │ Composition  │ güçlü "has-a"  │ Parça bütünle BİRLİKTE ölür  │ Order ↔ OrderLine    │
        └──────────────┴────────────────┴──────────────────────────────┴──────────────────────┘
        """);

        Console.WriteLine("AGGREGATION — Team / Employee");
        var ali = new Employee("Ali");                       // çalışan takımdan BAĞIMSIZ oluşturuldu
        var veli = new Employee("Veli");
        var backend = new Team("Backend");
        var platform = new Team("Platform");
        backend.Add(ali); backend.Add(veli);
        platform.Add(veli);                                  // Veli başka takıma TAŞINDI
        Console.WriteLine($"   backend: [{string.Join(", ", backend.Members.Select(m => m.Name))}]  platform: [{string.Join(", ", platform.Members.Select(m => m.Name))}]");
        backend.Disband();
        Console.WriteLine($"   backend dağıtıldı → Ali hâlâ var mı? Evet (Team = {(ali.Team is null ? "yok" : ali.Team.Name)}); Veli: Team = {veli.Team?.Name}");

        Console.WriteLine("\nCOMPOSITION — Order / OrderLine");
        var order = new Order();
        var line = order.AddLine("Kalem", 3, 12.5m);          // satırı SİPARİŞ yaratır
        order.AddLine("Defter", 1, 40m);
        Console.WriteLine($"   sipariş toplamı: {order.Total:N2}; satır sahibi sipariş mi? {ReferenceEquals(line.Owner, order)}");
        Console.WriteLine($"   OrderLine ctor public mi? {typeof(OrderLine).GetConstructors().Length > 0} (dışarıdan tek başına üretilemez)");
        order.Cancel();
        Console.WriteLine($"   sipariş iptal → satır sayısı: {order.Lines.Count}; eski satır yetim mi? {line.IsOrphaned}");

        // ── Yaşam süresi farkının GC ile kanıtı (WeakReference: nesneyi hayatta tutmaz, toplandı mı diye sorar) ──
        Console.WriteLine("\nGC İLE KANIT (WeakReference)");
        var (company, weakTeam) = CreateCompanyAndTeam();        // çalışanlar 'company' listesinde (dışarıda) tutuluyor
        var (weakOrder, weakLines) = CreateOrderWithLines();
        var (keptLine, weakOwner) = CreateOrderAndKeepLine();
        ForceGc();
        Console.WriteLine($"   Aggregation: takım toplandı mı? {!weakTeam.TryGetTarget(out _)}; çalışanlar hâlâ erişilebilir mi? {company.All(e => e.Name.Length > 0)}");
        Console.WriteLine($"   Composition: sipariş toplandı mı? {!weakOrder.TryGetTarget(out _)}; satırları da toplandı mı? {weakLines.All(w => !w.TryGetTarget(out _))}");
        Console.WriteLine($"   Satır hâlâ tutuluyorsa sipariş de yaşar mı? {weakOwner.TryGetTarget(out var o) && ReferenceEquals(o, keptLine.Owner)} (satır → Owner → sipariş zinciri)");
        GC.KeepAlive(company);
    }

    // NoInlining: yerel değişkenlerin JIT tarafından uzun süre canlı tutulmasını engeller; GC.Collect'ten ÖNCE nesneler erişilemez olur.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static (List<Employee> Company, WeakReference<Team> Team) CreateCompanyAndTeam()
    {
        var company = new List<Employee> { new("Ali"), new("Veli") };
        var team = new Team("Geçici");
        foreach (var e in company) team.Add(e);
        team.Disband();                                          // ilişki koparılır → çalışanlar takıma referans tutmaz
        return (company, new WeakReference<Team>(team));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static (WeakReference<Order>, List<WeakReference<OrderLine>>) CreateOrderWithLines()
    {
        var order = new Order();
        List<WeakReference<OrderLine>> lines = [new(order.AddLine("Kalem", 1, 1m)), new(order.AddLine("Defter", 2, 2m))];
        return (new WeakReference<Order>(order), lines);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static (OrderLine, WeakReference<Order>) CreateOrderAndKeepLine()
    {
        var order = new Order();
        var line = order.AddLine("Kalem", 1, 1m);
        return (line, new WeakReference<Order>(order));
    }

    private static void ForceGc()
    {
        for (var i = 0; i < 3; i++)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
