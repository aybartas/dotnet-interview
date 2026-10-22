namespace Junior.CSharpFundamentals.ClassMembers.Models;

// ═════════ KURAL: Equals ve GetHashCode BİRLİKTE override edilir ═════════
// Sözleşme: a.Equals(b) ise a.GetHashCode() == b.GetHashCode() olmak ZORUNDA (tersi şart değil: farklı nesneler aynı hash'i verebilir).
// Hash tabanlı koleksiyonlar (Dictionary, HashSet) önce hash'e bakar, sonra Equals çağırır. Hash tutarsızsa nesne "kaybolur".

/// <summary>YANLIŞ: yalnızca Equals override edildi. Eşit iki nesne FARKLI hash üretir → HashSet/Dictionary eşitliği göremez.</summary>
#pragma warning disable CS0659 // 'Equals' override edildi ama 'GetHashCode' edilmedi — BİLEREK (hatayı göstermek için)
#pragma warning disable CS0661 // '==' tanımlı değil; yalnızca CS0659 bastırıldı
public sealed class BadPoint(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;
    public override bool Equals(object? obj) => obj is BadPoint p && p.X == X && p.Y == Y;
}
#pragma warning restore CS0661
#pragma warning restore CS0659

/// <summary>DOĞRU: IEquatable&lt;T&gt; (boxing'siz tipli eşitlik) + Equals(object) + GetHashCode + == / !=.</summary>
public sealed class GoodPoint(int x, int y) : IEquatable<GoodPoint>
{
    public int X { get; } = x;
    public int Y { get; } = y;

    public bool Equals(GoodPoint? other) => other is not null && other.X == X && other.Y == Y;
    public override bool Equals(object? obj) => Equals(obj as GoodPoint);
    public override int GetHashCode() => HashCode.Combine(X, Y);                 // eşitlikte kullanılan ALANLARDAN türet
    public static bool operator ==(GoodPoint? a, GoodPoint? b) => a is null ? b is null : a.Equals(b);
    public static bool operator !=(GoodPoint? a, GoodPoint? b) => !(a == b);
    public override string ToString() => $"({X}, {Y})";
}

/// <summary>MUTABLE KEY TUZAĞI: hash'i DEĞİŞEBİLEN bir alandan türüyor. Koleksiyona eklendikten sonra değiştirilirse nesne bulunamaz hale gelir.</summary>
public sealed class MutableKey(string id)
{
    public string Id { get; set; } = id;
    public override bool Equals(object? obj) => obj is MutableKey k && k.Id == Id;
    public override int GetHashCode() => Id.GetHashCode();
}

/// <summary>Güvenli anahtar: değişmez (immutable) ve değer eşitlikli.</summary>
public sealed record SafeKey(string Id);

/// <summary>IComparable&lt;T&gt; (doğal sıralama) ve IComparer&lt;T&gt; (alternatif sıralama).</summary>
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
