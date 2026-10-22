namespace Junior.CollectionsLinq.CollectionsGenerics.Models;

// Dictionary/HashSet key CONTRACT:
//   1) if a.Equals(b) then a.GetHashCode() == b.GetHashCode() (the reverse is not required)
//   2) The hash must NOT CHANGE while the object is in the collection (immutable key)
//   3) Equals and GetHashCode must rely on the SAME fields
// If broken, Dictionary "lies": it cannot find an existing key, or becomes O(n) instead of O(1).

/// <summary>CORRECT: immutable + consistent Equals/GetHashCode.</summary>
public sealed class GoodKey(int id) : IEquatable<GoodKey>
{
    public int Id { get; } = id;
    public bool Equals(GoodKey? other) => other is not null && other.Id == Id;
    public override bool Equals(object? obj) => Equals(obj as GoodKey);
    public override int GetHashCode() => Id.GetHashCode();
}

/// <summary>WRONG #1: Equals is overridden, GetHashCode is NOT → equal keys go to different buckets; lookup fails.</summary>
#pragma warning disable CS0659
public sealed class EqualsOnlyKey(int id)
{
    public int Id { get; } = id;
    public override bool Equals(object? obj) => obj is EqualsOnlyKey k && k.Id == Id;
}
#pragma warning restore CS0659

/// <summary>WRONG #2: GetHashCode is overridden, Equals is NOT → lands in the same bucket but Equals compares references; lookup fails.</summary>
#pragma warning disable CS0661, CS0660
public sealed class HashOnlyKey(int id)
{
    public int Id { get; } = id;
    public override int GetHashCode() => Id.GetHashCode();
}
#pragma warning restore CS0661, CS0660

/// <summary>WRONG #3 (works but SLOW): always the same hash → all keys in one bucket → lookup is O(n) instead of O(1) (chain scan).</summary>
public sealed class ConstantHashKey(int id) : IEquatable<ConstantHashKey>
{
    public int Id { get; } = id;
    public bool Equals(ConstantHashKey? other) => other is not null && other.Id == Id;
    public override bool Equals(object? obj) => Equals(obj as ConstantHashKey);
    public override int GetHashCode() => 1;
}

/// <summary>WRONG #4: the hash is derived from a mutable field (mutable key trap, → J2.4).</summary>
public sealed class MutableKey(string name)
{
    public string Name { get; set; } = name;
    public override bool Equals(object? obj) => obj is MutableKey k && k.Name == Name;
    public override int GetHashCode() => Name.GetHashCode();
}

/// <summary>When custom comparison is needed, an IEqualityComparer&lt;T&gt; is supplied without changing the type (e.g. case-insensitive).</summary>
public sealed class CaseInsensitiveComparer : IEqualityComparer<string>
{
    public bool Equals(string? x, string? y) => string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
    public int GetHashCode(string obj) => StringComparer.OrdinalIgnoreCase.GetHashCode(obj);
}
