namespace Junior.CSharpFundamentals.Oop.Relationships;

// ════════════════ AGGREGATION (weak "has-a") ════════════════
// A Team CONTAINS employees but does NOT OWN them. An Employee lives independently of the Team:
//   • When a Team is disbanded (Disband) the employees don't disappear; they can move to another team.
//   • An Employee is created outside the Team (e.g. in the company registry) and given to it.

public sealed class Employee(string name)
{
    public string Name { get; } = name;
    public Team? Team { get; internal set; }
}

public sealed class Team(string name)
{
    private readonly List<Employee> _members = [];

    public string Name { get; } = name;
    public IReadOnlyList<Employee> Members => _members;

    /// <summary>The employee arrives already created OUTSIDE; it "joins" the team. If it is in another team it leaves that one (is moved).</summary>
    public void Add(Employee employee)
    {
        employee.Team?.Remove(employee);
        _members.Add(employee);
        employee.Team = this;
    }

    public void Remove(Employee employee)
    {
        if (_members.Remove(employee)) employee.Team = null;
    }

    /// <summary>The team is disbanded; the employees SURVIVE (only the relationship is severed).</summary>
    public void Disband()
    {
        foreach (var e in _members) e.Team = null;
        _members.Clear();
    }
}
