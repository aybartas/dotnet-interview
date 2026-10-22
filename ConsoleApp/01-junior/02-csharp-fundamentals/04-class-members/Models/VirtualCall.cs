namespace Junior.CSharpFundamentals.ClassMembers.Models;

// ═════════ CALLING A VIRTUAL METHOD FROM A CONSTRUCTOR IS DANGEROUS (CA2214) ═════════
// While the base constructor runs, the derived class's constructor BODY hasn't run yet.
// The overridden method sees a "half-built" object: fields assigned in the ctor body are still at their default (null/0) values.

public class BaseWithVirtualCall
{
    public List<string> Seen { get; } = [];

    public BaseWithVirtualCall()
    {
        Seen.Add(Describe());               // ← virtual call inside the constructor (CA2214 warning)
    }

    public virtual string Describe() => "Base.Describe";
}

public class DerivedWithState : BaseWithVirtualCall
{
    private readonly string _fromInitializer = "from initializer";   // field initializer → runs BEFORE the base ctor
    private readonly string _fromConstructor;                          // assigned in the ctor BODY → still null during the base ctor
    private readonly int _count;

    public DerivedWithState()
    {
        _fromConstructor = "from ctor body";
        _count = 42;
    }

    public override string Describe()
        => $"initializer: {_fromInitializer ?? "null"}, ctor: {_fromConstructor ?? "null"}, count: {_count}";
}

/// <summary>Safe alternative: make the virtual call not in the constructor but AFTER the object is fully constructed (e.g. Initialize()/factory).</summary>
public class SafeBase
{
    public static T Create<T>() where T : SafeBase, new()
    {
        var instance = new T();             // first the object is FULLY constructed
        instance.Initialize();              // then the virtual call is made safely
        return instance;
    }

    public string Result { get; private set; } = "";
    protected virtual string Describe() => "Safe.Describe";
    private void Initialize() => Result = Describe();
}

public sealed class SafeDerived : SafeBase
{
    private readonly string _name = "ready";
    protected override string Describe() => $"SafeDerived sees: {_name}";
}
