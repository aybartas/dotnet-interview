using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers.Tests;

public class EqualityTests
{
    [Fact]
    public void Overriding_only_Equals_breaks_hash_based_collections()
    {
        var a = new BadPoint(1, 2);
        var b = new BadPoint(1, 2);

        Assert.True(a.Equals(b));                           // Equals eşit diyor...
        Assert.NotEqual(a.GetHashCode(), b.GetHashCode());  // ...ama hash'ler farklı (sözleşme ihlali)

        var set = new HashSet<BadPoint> { a };
        Assert.DoesNotContain(b, set);                      // HashSet eşitliği GÖREMİYOR
        Assert.True(set.Add(b));                            // "aynı" nesne ikinci kez eklenebiliyor

        var dict = new Dictionary<BadPoint, string> { [a] = "x" };
        Assert.False(dict.ContainsKey(b));
    }

    [Fact]
    public void Overriding_Equals_and_GetHashCode_together_works_in_collections()
    {
        var a = new GoodPoint(1, 2);
        var b = new GoodPoint(1, 2);

        Assert.True(a == b);
        Assert.False(a != b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());

        var set = new HashSet<GoodPoint> { a };
        Assert.Contains(b, set);
        Assert.False(set.Add(b));
    }

    [Fact]
    public void Equality_operators_handle_null_without_throwing()
    {
        GoodPoint? n = null;
        Assert.True(n == null);
        Assert.False(new GoodPoint(1, 1) == n);
        Assert.True(new GoodPoint(1, 1) != n);
    }

    [Fact]
    public void Mutable_key_that_changes_after_insertion_cannot_be_found_again()
    {
        var key = new MutableKey("a");
        var set = new HashSet<MutableKey> { key };

        key.Id = "b";                                       // hash değişti, nesne eski kovada kaldı

        Assert.DoesNotContain(key, set);                    // aranamıyor...
        Assert.Single(set);                                 // ...ama sette DURUYOR
        Assert.False(set.Remove(key));                      // silinemiyor → "hayalet" kayıt
        Assert.True(set.Add(key));                          // aynı nesne tekrar eklenebiliyor
        Assert.Equal(2, set.Count);
    }

    [Fact]
    public void Immutable_record_key_is_safe()
    {
        var set = new HashSet<SafeKey> { new("a") };
        Assert.Contains(new SafeKey("a"), set);
    }

    [Fact]
    public void IComparable_gives_natural_order_and_IComparer_gives_alternative_order()
    {
        var versions = new List<Version2> { new(1, 10), new(1, 2), new(2, 0) };

        versions.Sort();
        Assert.Equal(["1.2", "1.10", "2.0"], versions.Select(v => v.ToString()));

        versions.Sort(new DescendingVersionComparer());
        Assert.Equal(["2.0", "1.10", "1.2"], versions.Select(v => v.ToString()));
    }
}
