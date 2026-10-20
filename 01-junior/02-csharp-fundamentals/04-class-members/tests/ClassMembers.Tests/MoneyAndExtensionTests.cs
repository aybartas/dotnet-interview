using Junior.CSharpFundamentals.ClassMembers.Extensions;
using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers.Tests;

public class MoneyTests
{
    private static readonly Money Ten = new(10m, "TRY");

    [Fact]
    public void Arithmetic_operators_work_for_the_same_currency()
    {
        Assert.Equal(new Money(15m, "TRY"), Ten + new Money(5m, "TRY"));
        Assert.Equal(new Money(5m, "TRY"), Ten - new Money(5m, "TRY"));
        Assert.Equal(new Money(25m, "TRY"), Ten * 2.5m);
        Assert.Equal(new Money(25m, "TRY"), 2.5m * Ten);
        Assert.Equal(new Money(-10m, "TRY"), -Ten);
    }

    [Fact]
    public void Mixing_currencies_throws()
        => Assert.Throws<InvalidOperationException>(() => Ten + new Money(1m, "USD"));

    [Fact]
    public void Equality_operators_are_consistent_with_Equals_and_GetHashCode()
    {
        var a = new Money(10m, "try");              // para birimi büyük harfe normalize edilir
        var b = new Money(10m, "TRY");

        Assert.True(a == b);
        Assert.False(a != b);
        Assert.True(a.Equals(b));
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
        Assert.Contains(b, new HashSet<Money> { a });
    }

    [Fact]
    public void Comparison_operators_come_in_pairs()
    {
        Assert.True(Ten > new Money(5m, "TRY"));
        Assert.True(Ten >= new Money(10m, "TRY"));
        Assert.True(new Money(5m, "TRY") < Ten);
        Assert.True(new Money(10m, "TRY") <= Ten);
        Assert.Throws<InvalidOperationException>(() => Ten < new Money(1m, "USD"));
    }

    [Fact]
    public void Implicit_conversion_from_decimal_and_explicit_back()
    {
        Money m = 50m;                              // implicit
        Assert.Equal(new Money(50m, "TRY"), m);
        Assert.Equal(50m, (decimal)m);              // explicit (para birimi kaybolur)
    }

    [Fact]
    public void Indexers_support_position_currency_and_multiple_parameters()
    {
        var bag = new MoneyBag { new Money(100m, "TRY"), new Money(25.5m, "TRY"), new Money(10m, "USD"), new Money(4m, "USD") };

        Assert.Equal(new Money(100m, "TRY"), bag[0]);
        Assert.Equal(new Money(4m, "USD"), bag[^1]);                       // Index (^1): Count + int indexer yeterli
        Assert.Equal(new Money(125.5m, "TRY"), bag["TRY"]);                // string indexer
        Assert.Equal(new Money(4m, "USD"), bag["usd", 1]);                 // çok parametreli indexer

        bag[0] = new Money(1m, "TRY");                                     // set
        Assert.Equal(new Money(1m, "TRY"), bag[0]);
        Assert.Throws<ArgumentOutOfRangeException>(() => bag[99]);
    }
}

public class ExtensionMethodTests
{
    [Fact]
    public void Extension_is_a_static_call_in_disguise_and_works_on_null_receivers()
    {
        string? s = null;
        Assert.True(s.IsBlank());                                          // NullReferenceException YOK
        Assert.True(StringExtensions.IsBlank(s));                           // eşdeğer statik çağrı
    }

    [Fact]
    public void Instance_method_always_wins_over_an_extension_with_the_same_signature()
    {
        var w = new Widget();
        Assert.Equal("örnek metot (instance)", w.Describe());
        Assert.Equal("extension metot", WidgetExtensions.Describe(w));      // extension'a yalnızca statik çağrıyla ulaşılır
        Assert.Equal("önek: extension metot", w.Describe("önek"));          // farklı imza → extension çağrılabilir
    }

    [Fact]
    public void Generic_extension_on_IEnumerable_is_how_LINQ_works()
    {
        string?[] mixed = ["a", null, "b"];
        Assert.Equal(["a", "b"], mixed.WhereNotNull());
        Assert.True(typeof(Enumerable).IsAbstract && typeof(Enumerable).IsSealed);   // static class
    }

    [Theory]
    [InlineData("kısa", 10, "kısa")]
    [InlineData("çok uzun metin", 4, "çok …")]
    public void Truncate_shortens_long_text(string input, int max, string expected)
        => Assert.Equal(expected, input.Truncate(max));
}
