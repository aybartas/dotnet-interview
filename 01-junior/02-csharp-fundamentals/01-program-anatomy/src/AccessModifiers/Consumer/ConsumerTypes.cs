using Junior.CSharpFundamentals.ProgramAnatomy.Library;

namespace Junior.CSharpFundamentals.ProgramAnatomy.Consumer;

/// <summary>BAŞKA assembly'de, Vault'tan TÜREYEN sınıf.</summary>
public class ConsumerDerived : Vault
{
    public string Peek()
    {
        var ok = string.Join(", ",
            ProtectedMember,          // ✅ protected: türeyen olduğumuz için (assembly farkı önemsiz)
            ProtectedInternalMember,  // ✅ protected internal: türeyen olduğumuz için
            PublicMember);            // ✅

#if SHOW_ERRORS
        _ = PrivateMember;            // ✗ CS0103: private üye türeyen sınıfta hiç görünmez ("adı yok" gibi davranır)
        _ = PrivateProtectedMember;   // ✗ CS0122: private protected → türeyen OLSAK da assembly farklı
        _ = InternalMember;           // ✗ CS0103: internal → farklı assembly (görünmez)
#endif
        return ok;
    }
}

/// <summary>BAŞKA assembly'de, Vault'tan TÜREMEYEN sınıf: yalnızca public.</summary>
public static class ConsumerOther
{
    public static string Peek(Vault v)
    {
        var ok = v.PublicMember;      // ✅ yalnızca public

#if SHOW_ERRORS
        _ = v.ProtectedMember;        // ✗ CS0122: türeyen değiliz
        _ = v.ProtectedInternalMember;// ✗ CS0122: farklı assembly VE türeyen değiliz → ikisi de sağlanmıyor
        _ = v.InternalMember;         // ✗ CS1061: dışarıdan bakınca üye hiç yok gibi
        _ = Library.PriceRules.AddVat(1m); // ✗ CS0122: internal TİP
#endif
        return ok;
    }
}
