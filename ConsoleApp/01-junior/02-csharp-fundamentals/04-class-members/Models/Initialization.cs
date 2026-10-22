namespace Junior.CSharpFundamentals.ClassMembers.Models;

/// <summary>Başlatma adımlarını sırayla kaydeden ortak iz (test edilebilir olsun diye static liste).</summary>
public static class Trace
{
    public static List<string> Steps { get; } = [];
    public static string Log(string step) { Steps.Add($"{Steps.Count + 1,2}. {step}"); return step; }
    public static void Reset() => Steps.Clear();
}

// ═════════ BAŞLATMA SIRASI ═════════
// new Derived() çağrıldığında (instance):
//   1) Tüm alanlar varsayılan değere (0/null) ayarlanır (bellek sıfırlanır)
//   2) FIELD INITIALIZER'lar çalışır — EN TÜRETİLMİŞ sınıftan TABANA doğru
//   3) Taban constructor GÖVDESİ — TABANDAN türetilmişe doğru
//   4) Derived constructor GÖVDESİ
// (Neden initializer'lar ters? Derleyici initializer'ları constructor'ın BAŞINA, base(...) çağrısından ÖNCE yerleştirir.)
// static: her tipin static başlatması KENDİ ilk kullanımında, bir kez çalışır. Base'in static başlatması, Derived'ın
// instance alan başlatıcılarından SONRA, base constructor'ına ilk girişte tetiklenir (çıktıda görün).

public class BaseClass
{
    public static readonly string StaticBaseField = Trace.Log("Base  static field initializer");
    static BaseClass() => Trace.Log("Base  static constructor");

    private readonly string _baseField = Trace.Log("Base  instance field initializer");

    public BaseClass()
    {
        Trace.Log("Base  constructor GÖVDESİ");
    }

    public BaseClass(string marker) : this()
    {
        Trace.Log($"Base  constructor(string) GÖVDESİ ({marker}) — this() zinciri önce çalıştı");
    }
}

public class DerivedClass : BaseClass
{
    public static readonly string StaticDerivedField = Trace.Log("Derived static field initializer");
    static DerivedClass() => Trace.Log("Derived static constructor");

    private readonly string _derivedField = Trace.Log("Derived instance field initializer");

    public DerivedClass() : base()
    {
        Trace.Log("Derived constructor GÖVDESİ");
    }

    public DerivedClass(string marker) : base(marker)
    {
        Trace.Log($"Derived constructor(string) GÖVDESİ ({marker})");
    }
}

// ═════════ Diğer constructor çeşitleri ═════════
public sealed class Connection
{
    private static Connection? _shared;
    public string Name { get; }
    public int Retries { get; }

    private Connection(string name, int retries) { Name = name; Retries = retries; }   // PRIVATE ctor → yalnızca bu sınıf üretebilir

    public static Connection Create(string name) => new(name, retries: 3);               // static factory method
    public static Connection Shared => _shared ??= new Connection("shared", 1);           // basit (thread-safe OLMAYAN) singleton

    public Connection(Connection other) : this(other.Name, other.Retries) { }             // COPY constructor + this(...) zinciri
}

public class Money2(decimal amount, string currency)                                      // primary constructor (C# 12; → J2.6)
{
    public decimal Amount { get; } = amount;
    public string Currency { get; } = currency;
}
