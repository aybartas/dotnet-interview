namespace Junior.CSharpFundamentals.ClassMembers.Models;

// ═════════ RULE: Equals and GetHashCode are overridden TOGETHER ═════════
// Contract: if a.Equals(b) then a.GetHashCode() == b.GetHashCode() MUST hold (the reverse isn't required: different objects may share a hash).
// Hash-based collections (Dictionary, HashSet) look at the hash first, then call Equals. If the hash is inconsistent the object "gets lost".

/// <summary>WRONG: only Equals is overridden. Two equal objects produce DIFFERENT hashes → HashSet/Dictionary can't see the equality.</summary>
#pragma warning disable CS0659 // 'Equals' is overridden but 'GetHashCode' is not — INTENTIONAL (to show the bug)
#pragma warning disable CS0661 // '==' is not defined; only CS0659 was suppressed
public sealed class BadPoint(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;
    public override bool Equals(object? obj) => obj is BadPoint p && p.X == X && p.Y == Y;
}
#pragma warning restore CS0661
#pragma warning restore CS0659

/// <summary>RIGHT: IEquatable&lt;T&gt; (typed equality without boxing) + Equals(object) + GetHashCode + == / !=.</summary>
public sealed class GoodPoint(int x, int y) : IEquatable<GoodPoint>
{
    public int X { get; } = x;
    public int Y { get; } = y;

    public bool Equals(GoodPoint? other) => other is not null && other.X == X && other.Y == Y;
    public override bool Equals(object? obj) => Equals(obj as GoodPoint);
    public override int GetHashCode() => HashCode.Combine(X, Y);                 // derive from the FIELDS used in equality
    public static bool operator ==(GoodPoint? a, GoodPoint? b) => a is null ? b is null : a.Equals(b);
    public static bool operator !=(GoodPoint? a, GoodPoint? b) => !(a == b);
    public override string ToString() => $"({X}, {Y})";
}

/// <summary>MUTABLE KEY TRAP: the hash derives from a field that can CHANGE. If it is changed after being added to a collection, the object can no longer be found.</summary>
public sealed class MutableKey(string id)
{
    public string Id { get; set; } = id;
    public override bool Equals(object? obj) => obj is MutableKey k && k.Id == Id;
    public override int GetHashCode() => Id.GetHashCode();
}

/// <summary>Safe key: immutable and value-equal.</summary>
public sealed record SafeKey(string Id);

/// <summary>IComparable&lt;T&gt; (natural order) and IComparer&lt;T&gt; (alternative order).</summary>
public sealed class Version2(int major, int minor) : IComparable<Version2>, IEquatable<Version2>
{
    public int Major { get; } = major;
    public int Minor { get; } = minor;

    public int CompareTo(Version2? other)
    {
        if (other is null) return 1;
        var c = Major.CompareTo(other.Major);
        return c != 0 ? c : Minor.CompareTo(other.Minor);
    }
    public bool Equals(Version2? other) => other is not null && Major == other.Major && Minor == other.Minor;
    public override bool Equals(object? obj) => Equals(obj as Version2);
    public override int GetHashCode() => HashCode.Combine(Major, Minor);
    public override string ToString() => $"{Major}.{Minor}";
}

public sealed class DescendingVersionComparer : IComparer<Version2>
{
    public int Compare(Version2? x, Version2? y) => Comparer<Version2>.Default.Compare(y, x);
}
