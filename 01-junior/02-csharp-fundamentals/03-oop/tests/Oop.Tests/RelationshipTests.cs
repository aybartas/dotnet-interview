using System.Runtime.CompilerServices;
using Junior.CSharpFundamentals.Oop.Relationships;

namespace Junior.CSharpFundamentals.Oop.Tests;

/// <summary>Aggregation (zayıf has-a) ile composition (güçlü has-a) arasındaki YAŞAM SÜRESİ farkının kanıtı.</summary>
public class RelationshipTests
{
    // ───────────── Aggregation: Team ↔ Employee ─────────────

    [Fact]
    public void Aggregation_employee_outlives_the_team()
    {
        var ali = new Employee("Ali");
        var team = new Team("Backend");
        team.Add(ali);

        team.Disband();

        Assert.Empty(team.Members);
        Assert.Null(ali.Team);                 // çalışan hâlâ kullanılabilir durumda; sadece ilişki koptu
        Assert.Equal("Ali", ali.Name);
    }

    [Fact]
    public void Aggregation_employee_can_move_between_teams()
    {
        var veli = new Employee("Veli");
        var a = new Team("A");
        var b = new Team("B");
        a.Add(veli);
        b.Add(veli);

        Assert.Empty(a.Members);
        Assert.Same(b, veli.Team);
    }

    [Fact]
    public void Aggregation_employee_stays_alive_in_memory_when_the_team_is_collected()
    {
        var (company, weakTeam) = CreateCompanyAndTeam();       // çalışanlar 'company' listesinde (dışarıda) tutuluyor

        ForceGc();

        Assert.False(weakTeam.TryGetTarget(out _), "Team toplanmalıydı (kimse tutmuyor).");
        Assert.All(company, e => Assert.NotNull(e.Name));        // Employee'ler HÂLÂ erişilebilir
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static (List<Employee> Company, WeakReference<Team> Team) CreateCompanyAndTeam()
    {
        var company = new List<Employee> { new("Ali"), new("Veli") };
        var team = new Team("Geçici");
        foreach (var e in company) team.Add(e);
        team.Disband();                                          // ilişki koparılır → çalışanlar takıma referans tutmaz
        return (company, new WeakReference<Team>(team));
    }

    // ───────────── Composition: Order ↔ OrderLine ─────────────

    [Fact]
    public void Composition_order_line_cannot_be_constructed_outside_its_order()
    {
        Assert.Empty(typeof(OrderLine).GetConstructors());       // public ctor yok; yalnızca Order.AddLine yaratır
    }

    [Fact]
    public void Composition_cancelling_the_order_orphans_its_lines()
    {
        var order = new Order();
        var line = order.AddLine("Kalem", 2, 10m);
        order.Cancel();

        Assert.Empty(order.Lines);
        Assert.True(line.IsOrphaned);
        Assert.Throws<InvalidOperationException>(() => order.AddLine("Defter", 1, 5m));
    }

    [Fact]
    public void Composition_lines_die_together_with_the_order()
    {
        var (weakOrder, weakLines) = CreateOrderWithLines();

        ForceGc();

        Assert.False(weakOrder.TryGetTarget(out _), "Order toplanmalıydı.");
        Assert.All(weakLines, w => Assert.False(w.TryGetTarget(out _), "Satırlar siparişle BİRLİKTE toplanmalıydı."));
    }

    [Fact]
    public void Composition_a_line_that_is_still_referenced_keeps_its_order_alive()
    {
        var (line, weakOrder) = CreateOrderAndKeepLine();

        ForceGc();

        Assert.True(weakOrder.TryGetTarget(out var order));       // satır → Owner → sipariş zinciri: bütün ve parça aynı kaderi paylaşır
        Assert.Same(order, line.Owner);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static (WeakReference<Order>, List<WeakReference<OrderLine>>) CreateOrderWithLines()
    {
        var order = new Order();
        var lines = new List<WeakReference<OrderLine>>
        {
            new(order.AddLine("Kalem", 1, 1m)),
            new(order.AddLine("Defter", 2, 2m)),
        };
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
