namespace Junior.CSharpFundamentals.ProgramAnatomy.AccessModifiers;

/// <summary>A type carrying ONE member of each access level (J2.1 access modifiers experiment). The compiler checks which code can reach which member.</summary>
public class Vault
{
    private string PrivateMember => "private";                     // only Vault itself
    private protected string PrivateProtectedMember => "private protected";  // Vault + derived types in the SAME assembly
    protected string ProtectedMember => "protected";                // Vault + derived types (in ANY assembly)
    internal string InternalMember => "internal";                   // EVERYONE in the same assembly (no need to be derived)
    protected internal string ProtectedInternalMember => "protected internal";  // everyone in the same assembly OR (in another assembly) derived types
    public string PublicMember => "public";                         // everyone

    /// <summary>All six can be reached from inside the same class.</summary>
    public string ReadAllFromSameClass() =>
        string.Join(", ", PrivateMember, PrivateProtectedMember, ProtectedMember, InternalMember, ProtectedInternalMember, PublicMember);
}

/// <summary>A class in the SAME assembly that DERIVES from Vault: reaches everything EXCEPT private.</summary>
public class SameAssemblyDerived : Vault
{
    public string Peek() => string.Join(", ",
        PrivateProtectedMember,      // ✅ same assembly + derived
        ProtectedMember,             // ✅ derived
        InternalMember,              // ✅ same assembly
        ProtectedInternalMember,     // ✅
        PublicMember);               // ✅
#if SHOW_ERRORS
    public string PeekError() => PrivateMember;   // ✗ CS0122: private is only inside Vault
#endif
}

/// <summary>A class in the SAME assembly that does NOT derive from Vault: can't reach protected or private protected.</summary>
public class SameAssemblyOther
{
    public static string Peek(Vault v) => string.Join(", ",
        v.InternalMember,            // ✅ same assembly
        v.ProtectedInternalMember,   // ✅ the "internal" half of protected internal
        v.PublicMember);             // ✅
#if SHOW_ERRORS
    public static string PeekErrors(Vault v) => v.ProtectedMember + v.PrivateProtectedMember;   // ✗ CS1540 / CS0122: we are not derived
#endif
}

// Default access: 'internal' for a type (not written below), 'private' for a member.
class DefaultAccessType            // → internal class
{
    int field = 7;                 // → private
    public int ReadField() => field;
}

/// <summary>This type is public; but the DefaultAccessType it contains is internal → it can't leak it outside (CS0050/CS0051 consistency rule).</summary>
public static class Factory
{
    public static string DescribeDefaults() =>
        $"type default: {(typeof(DefaultAccessType).IsNotPublic ? "internal" : "public")}, " +
        $"member default: {(typeof(DefaultAccessType).GetField("field", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!.IsPrivate ? "private" : "?")}";
}

/// <summary>With InternalsVisibleTo it can be opened ONLY to specific friend assemblies (typically the test project). Accessing this type from any other assembly gives CS0122.</summary>
internal static class PriceRules
{
    internal static decimal AddVat(decimal net) => Math.Round(net * 1.20m, 2);
}
