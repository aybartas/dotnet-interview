namespace Junior.CSharpFundamentals.ProgramAnatomy.Library;

/// <summary>Her erişim düzeyinden BİRER üye taşıyan tip. Hangi koddan hangisine erişilebildiğini derleyici denetler.</summary>
public class Vault
{
    private string PrivateMember => "private";                     // yalnızca Vault'un kendisi
    private protected string PrivateProtectedMember => "private protected";  // Vault + AYNI assembly'deki türeyenler
    protected string ProtectedMember => "protected";                // Vault + türeyenler (HERHANGİ bir assembly)
    internal string InternalMember => "internal";                   // aynı assembly'deki HERKES (türeyen olmak gerekmez)
    protected internal string ProtectedInternalMember => "protected internal";  // aynı assembly'deki herkes VEYA (başka assembly'de) türeyenler
    public string PublicMember => "public";                         // herkes

    /// <summary>Aynı sınıf içinden altısına da erişilebilir.</summary>
    public string ReadAllFromSameClass() =>
        string.Join(", ", PrivateMember, PrivateProtectedMember, ProtectedMember, InternalMember, ProtectedInternalMember, PublicMember);
}

/// <summary>AYNI assembly'de, Vault'tan TÜREYEN sınıf: private HARİÇ hepsine erişir.</summary>
public class SameAssemblyDerived : Vault
{
    public string Peek() => string.Join(", ",
        PrivateProtectedMember,      // ✅ aynı assembly + türeyen
        ProtectedMember,             // ✅ türeyen
        InternalMember,              // ✅ aynı assembly
        ProtectedInternalMember,     // ✅
        PublicMember);               // ✅
    // PrivateMember                 // ✗ CS0122: private yalnızca Vault içinde
}

/// <summary>AYNI assembly'de, Vault'tan TÜREMEYEN sınıf: protected ve private protected'a erişemez.</summary>
public class SameAssemblyOther
{
    public static string Peek(Vault v) => string.Join(", ",
        v.InternalMember,            // ✅ aynı assembly
        v.ProtectedInternalMember,   // ✅ protected internal'ın "internal" yarısı
        v.PublicMember);             // ✅
    // v.ProtectedMember             // ✗ CS0122: türeyen değiliz
    // v.PrivateProtectedMember      // ✗ CS0122
}

// Varsayılan erişim: tip için 'internal' (aşağıda yazılmadı), üye için 'private'.
class DefaultAccessType            // → internal class
{
    int field = 7;                 // → private
    public int ReadField() => field;
}

/// <summary>Bu tip public; ama içerdiği DefaultAccessType internal → dışarıya sızdıramaz (CS0050/CS0051 tutarlılık kuralı).</summary>
public static class Factory
{
    public static string DescribeDefaults() =>
        $"tip varsayılanı: {(typeof(DefaultAccessType).IsNotPublic ? "internal" : "public")}, " +
        $"üye varsayılanı: {(typeof(DefaultAccessType).GetField("field", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!.IsPrivate ? "private" : "?")}";
}

/// <summary>InternalsVisibleTo ile yalnızca "ProgramAnatomy.Tests" assembly'sinden görünür.</summary>
internal static class PriceRules
{
    internal static decimal AddVat(decimal net) => Math.Round(net * 1.20m, 2);
}
