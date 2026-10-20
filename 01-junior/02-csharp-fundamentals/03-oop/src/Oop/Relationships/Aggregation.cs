namespace Junior.CSharpFundamentals.Oop.Relationships;

// ════════════════ AGGREGATION (zayıf "has-a") ════════════════
// Team çalışanları İÇERİR ama ONLARA SAHİP DEĞİLDİR. Employee, Team'den bağımsız yaşar:
//   • Team dağıtılınca (Disband) çalışanlar yok olmaz; başka takıma geçebilir.
//   • Employee, Team'in dışında (ör. şirket kaydında) oluşturulur ve ona verilir.

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

    /// <summary>Çalışan DIŞARIDA oluşturulmuş olarak gelir; takıma "katılır". Başka takımdaysa oradan ayrılır (taşınır).</summary>
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

    /// <summary>Takım dağıtılır; çalışanlar SAĞ KALIR (yalnızca ilişki kopar).</summary>
    public void Disband()
    {
        foreach (var e in _members) e.Team = null;
        _members.Clear();
    }
}
