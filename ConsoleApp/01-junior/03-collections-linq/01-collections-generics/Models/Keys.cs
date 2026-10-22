namespace Junior.CollectionsLinq.CollectionsGenerics.Models;

// Dictionary/HashSet anahtarı SÖZLEŞMESİ:
//   1) a.Equals(b) ise a.GetHashCode() == b.GetHashCode() (tersi şart değil)
//   2) Hash, nesne koleksiyondayken DEĞİŞMEMELİ (immutable key)
//   3) Equals ve GetHashCode AYNI alanlara dayanmalı
// Bozulursa Dictionary "yalan söyler": var olan anahtarı bulamaz, ya da O(1) yerine O(n) olur.

/// <summary>DOĞRU: değişmez + tutarlı Equals/GetHashCode.</summary>
public sealed class GoodKey(int id) : IEquatable<GoodKey>
{
    public int Id { get; } = id;
    public bool Equals(GoodKey? other) => other is not null && other.Id == Id;
    public override bool Equals(object? obj) => Equals(obj as GoodKey);
    public override int GetHashCode() => Id.GetHashCode();
}

/// <summary>YANLIŞ #1: Equals override edildi, GetHashCode EDİLMEDİ → eşit anahtarlar farklı kovaya gider; arama başarısız.</summary>
#pragma warning disable CS0659
public sealed class EqualsOnlyKey(int id)
{
    public int Id { get; } = id;
    public override bool Equals(object? obj) => obj is EqualsOnlyKey k && k.Id == Id;
}
#pragma warning restore CS0659

/// <summary>YANLIŞ #2: GetHashCode override edildi, Equals EDİLMEDİ → aynı kovaya düşer ama Equals referans karşılaştırır; arama başarısız.</summary>
#pragma warning disable CS0661, CS0660
public sealed class HashOnlyKey(int id)
{
    public int Id { get; } = id;
    public override int GetHashCode() => Id.GetHashCode();
}
#pragma warning restore CS0661, CS0660

/// <summary>YANLIŞ #3 (çalışır ama YAVAŞ): hep aynı hash → tüm anahtarlar tek kovada → arama O(1) yerine O(n) (zincir taraması).</summary>
public sealed class ConstantHashKey(int id) : IEquatable<ConstantHashKey>
{
    public int Id { get; } = id;
    public bool Equals(ConstantHashKey? other) => other is not null && other.Id == Id;
    public override bool Equals(object? obj) => Equals(obj as ConstantHashKey);
    public override int GetHashCode() => 1;
}

/// <summary>YANLIŞ #4: hash'i değişebilir bir alandan türüyor (mutable key tuzağı, → J2.4).</summary>
public sealed class MutableKey(string name)
{
    public string Name { get; set; } = name;
    public override bool Equals(object? obj) => obj is MutableKey k && k.Name == Name;
    public override int GetHashCode() => Name.GetHashCode();
}

/// <summary>Özel karşılaştırma gerekiyorsa tipi değiştirmeden IEqualityComparer&lt;T&gt; verilir (ör. büyük/küçük harf duyarsız).</summary>
public sealed class CaseInsensitiveComparer : IEqualityComparer<string>
{
    public bool Equals(string? x, string? y) => string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
    public int GetHashCode(string obj) => StringComparer.OrdinalIgnoreCase.GetHashCode(obj);
}
