namespace Junior.CSharpFundamentals.ClassMembers.Models;

// ═════════ CONSTRUCTOR'DAN VIRTUAL METOT ÇAĞIRMAK TEHLİKELİDİR (CA2214) ═════════
// Taban constructor çalışırken türetilmiş sınıfın constructor GÖVDESİ henüz çalışmamıştır.
// Override edilen metot "yarım kurulmuş" nesneyi görür: ctor gövdesinde atanan alanlar hâlâ varsayılan (null/0) değerdedir.

public class BaseWithVirtualCall
{
    public List<string> Seen { get; } = [];

    public BaseWithVirtualCall()
    {
        Seen.Add(Describe());               // ← constructor içinde virtual çağrı (CA2214 uyarısı)
    }

    public virtual string Describe() => "Base.Describe";
}

public class DerivedWithState : BaseWithVirtualCall
{
    private readonly string _fromInitializer = "başlatıcıdan";   // field initializer → base ctor'dan ÖNCE çalışır
    private readonly string _fromConstructor;                      // ctor GÖVDESİNDE atanır → base ctor sırasında HENÜZ null
    private readonly int _count;

    public DerivedWithState()
    {
        _fromConstructor = "ctor gövdesinden";
        _count = 42;
    }

    public override string Describe()
        => $"initializer: {_fromInitializer ?? "null"}, ctor: {_fromConstructor ?? "null"}, count: {_count}";
}

/// <summary>Güvenli alternatif: virtual çağrıyı constructor'da değil, nesne tamamen kurulduktan SONRA (ör. Initialize()/factory) yapın.</summary>
public class SafeBase
{
    public static T Create<T>() where T : SafeBase, new()
    {
        var instance = new T();             // önce nesne TAMAMEN kurulur
        instance.Initialize();              // sonra virtual çağrı güvenle yapılır
        return instance;
    }

    public string Result { get; private set; } = "";
    protected virtual string Describe() => "Safe.Describe";
    private void Initialize() => Result = Describe();
}

public sealed class SafeDerived : SafeBase
{
    private readonly string _name = "hazır";
    protected override string Describe() => $"SafeDerived görüyor: {_name}";
}
