namespace Junior.CSharpFundamentals.ClassMembers.Models;

/// <summary>A shared trace that records initialization steps in order (a static list so it is testable).</summary>
public static class Trace
{
    public static List<string> Steps { get; } = [];
    public static string Log(string step) { Steps.Add($"{Steps.Count + 1,2}. {step}"); return step; }
    public static void Reset() => Steps.Clear();
}

// ═════════ INITIALIZATION ORDER ═════════
// When new Derived() is called (instance):
//   1) All fields are set to their default value (0/null) (memory is zeroed)
//   2) FIELD INITIALIZERS run — from the MOST DERIVED class toward the BASE
//   3) The base constructor BODY — from the BASE toward the derived
//   4) The Derived constructor BODY
// (Why are the initializers reversed? The compiler places initializers at the START of the constructor, BEFORE the base(...) call.)
// static: each type's static initialization runs once, on that type's OWN first use. Base's static initialization is triggered
// AFTER Derived's instance field initializers, on first entry into the base constructor (see it in the output).

public class BaseClass
{
    public static readonly string StaticBaseField = Trace.Log("Base  static field initializer");
    static BaseClass() => Trace.Log("Base  static constructor");

    private readonly string _baseField = Trace.Log("Base  instance field initializer");

    public BaseClass()
    {
        Trace.Log("Base  constructor BODY");
    }

    public BaseClass(string marker) : this()
    {
        Trace.Log($"Base  constructor(string) BODY ({marker}) — the this() chain ran first");
    }
}

public class DerivedClass : BaseClass
{
    public static readonly string StaticDerivedField = Trace.Log("Derived static field initializer");
    static DerivedClass() => Trace.Log("Derived static constructor");

    private readonly string _derivedField = Trace.Log("Derived instance field initializer");

    public DerivedClass() : base()
    {
        Trace.Log("Derived constructor BODY");
    }

    public DerivedClass(string marker) : base(marker)
    {
        Trace.Log($"Derived constructor(string) BODY ({marker})");
    }
}

// ═════════ Other kinds of constructors ═════════
public sealed class Connection
{
    private static Connection? _shared;
    public string Name { get; }
    public int Retries { get; }

    private Connection(string name, int retries) { Name = name; Retries = retries; }   // PRIVATE ctor → only this class can create instances

    public static Connection Create(string name) => new(name, retries: 3);               // static factory method
    public static Connection Shared => _shared ??= new Connection("shared", 1);           // a simple singleton (NOT thread-safe)

    public Connection(Connection other) : this(other.Name, other.Retries) { }             // COPY constructor + this(...) chain
}

public class Money2(decimal amount, string currency)                                      // primary constructor (C# 12; → J2.6)
{
    public decimal Amount { get; } = amount;
    public string Currency { get; } = currency;
}
