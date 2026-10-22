using System.Runtime.CompilerServices;
using Junior.CSharpFundamentals.Oop.Relationships;

namespace Junior.CSharpFundamentals.Oop;

public static class Section06_Relationships
{
    public static void Run()
    {
        Console.WriteLine("""
        ┌──────────────┬────────────────┬──────────────────────────────┬──────────────────────┐
        │ Relationship │ Meaning        │ Lifetime                     │ Example              │
        ├──────────────┼────────────────┼──────────────────────────────┼──────────────────────┤
        │ Inheritance  │ "is-a"         │ —                            │ SavingsAccount:Account│
        │ Association  │ "uses-a"       │ Independent, no ownership    │ Order ↔ Courier      │
        │ Aggregation  │ weak "has-a"   │ Part OUTLIVES the whole      │ Team ↔ Employee      │
        │ Composition  │ strong "has-a" │ Part dies WITH the whole     │ Order ↔ OrderLine    │
        └──────────────┴────────────────┴──────────────────────────────┴──────────────────────┘
        """);

        Console.WriteLine("AGGREGATION — Team / Employee");
        var alice = new Employee("Alice");                   // the employee was created INDEPENDENTLY of the team
        var bob = new Employee("Bob");
        var backend = new Team("Backend");
        var platform = new Team("Platform");
        backend.Add(alice); backend.Add(bob);
        platform.Add(bob);                                   // Bob was MOVED to another team
        Console.WriteLine($"   backend: [{string.Join(", ", backend.Members.Select(m => m.Name))}]  platform: [{string.Join(", ", platform.Members.Select(m => m.Name))}]");
        backend.Disband();
        Console.WriteLine($"   backend disbanded → does Alice still exist? Yes (Team = {(alice.Team is null ? "none" : alice.Team.Name)}); Bob: Team = {bob.Team?.Name}");

        Console.WriteLine("\nCOMPOSITION — Order / OrderLine");
        var order = new Order();
        var line = order.AddLine("Pen", 3, 12.5m);            // the ORDER creates the line
        order.AddLine("Notebook", 1, 40m);
        Console.WriteLine($"   order total: {order.Total:N2}; is the line's owner the order? {ReferenceEquals(line.Owner, order)}");
        Console.WriteLine($"   is the OrderLine ctor public? {typeof(OrderLine).GetConstructors().Length > 0} (cannot be created standalone from outside)");
        order.Cancel();
        Console.WriteLine($"   order cancelled → line count: {order.Lines.Count}; is the old line an orphan? {line.IsOrphaned}");

        // ── Proving the lifetime difference with the GC (WeakReference: doesn't keep the object alive, only asks whether it was collected) ──
        Console.WriteLine("\nPROOF WITH THE GC (WeakReference)");
        var (company, weakTeam) = CreateCompanyAndTeam();        // the employees are held in the 'company' list (outside)
        var (weakOrder, weakLines) = CreateOrderWithLines();
        var (keptLine, weakOwner) = CreateOrderAndKeepLine();
        ForceGc();
        Console.WriteLine($"   Aggregation: was the team collected? {!weakTeam.TryGetTarget(out _)}; are the employees still reachable? {company.All(e => e.Name.Length > 0)}");
        Console.WriteLine($"   Composition: was the order collected? {!weakOrder.TryGetTarget(out _)}; were its lines collected too? {weakLines.All(w => !w.TryGetTarget(out _))}");
        Console.WriteLine($"   If a line is still held, does the order live too? {weakOwner.TryGetTarget(out var o) && ReferenceEquals(o, keptLine.Owner)} (line → Owner → order chain)");
        GC.KeepAlive(company);
    }

    // NoInlining: prevents the JIT from keeping locals alive for long; the objects become unreachable BEFORE GC.Collect.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static (List<Employee> Company, WeakReference<Team> Team) CreateCompanyAndTeam()
    {
        var company = new List<Employee> { new("Alice"), new("Bob") };
        var team = new Team("Temporary");
        foreach (var e in company) team.Add(e);
        team.Disband();                                          // the relationship is severed → employees hold no reference to the team
        return (company, new WeakReference<Team>(team));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static (WeakReference<Order>, List<WeakReference<OrderLine>>) CreateOrderWithLines()
    {
        var order = new Order();
        List<WeakReference<OrderLine>> lines = [new(order.AddLine("Pen", 1, 1m)), new(order.AddLine("Notebook", 2, 2m))];
        return (new WeakReference<Order>(order), lines);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static (OrderLine, WeakReference<Order>) CreateOrderAndKeepLine()
    {
        var order = new Order();
        var line = order.AddLine("Pen", 1, 1m);
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
