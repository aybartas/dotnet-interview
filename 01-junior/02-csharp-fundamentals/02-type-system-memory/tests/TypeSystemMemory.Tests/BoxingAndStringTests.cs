using System.Collections;
using System.Globalization;

namespace Junior.CSharpFundamentals.TypeSystemMemory.Tests;

public class BoxingAndStringTests
{
    private static long Alloc(Action a)
    {
        var before = GC.GetAllocatedBytesForCurrentThread();
        a();
        return GC.GetAllocatedBytesForCurrentThread() - before;
    }

    private interface IShape { double Area { get; } }
    private readonly struct Square(double side) : IShape { public double Area => side * side; }
    private static double AreaOf<T>(T shape) where T : IShape => shape.Area;

    [Fact]
    public void Boxing_allocates_on_the_heap_but_constrained_generics_do_not()
    {
        // ısınma (JIT/tiered compilation tahsislerini ölçümden çıkarmak için)
        _ = AreaOf(new Square(1)); IShape w = new Square(1); _ = w.Area;

        var boxing = Alloc(() => { for (var i = 0; i < 100; i++) { IShape s = new Square(i); _ = s.Area; } });
        var generic = Alloc(() => { for (var i = 0; i < 100; i++) _ = AreaOf(new Square(i)); });

        Assert.True(boxing >= 100 * 24, $"boxing tahsisi beklenenden az: {boxing}");
        Assert.Equal(0, generic);
    }

    [Fact]
    public void ArrayList_boxes_but_List_of_int_does_not()
    {
        var arrayList = Alloc(() => { var l = new ArrayList(); for (var i = 0; i < 1000; i++) l.Add(i); });
        var list = Alloc(() => { var l = new List<int>(1000); for (var i = 0; i < 1000; i++) l.Add(i); });
        Assert.True(arrayList > list * 5);
    }

    [Fact]
    public void Unboxing_requires_the_exact_type()
    {
        object boxed = 42;
        Assert.Throws<InvalidCastException>(() => (long)boxed);
        Assert.Equal(42L, (long)(int)boxed);
    }

    [Fact]
    public void Boxed_copy_is_independent_of_the_original()
    {
        var i = 42;
        object boxed = i;
        i = 99;
        Assert.Equal(42, (int)boxed);
    }

    [Fact]
    public void Boxing_a_nullable_gives_null_or_the_underlying_value_type()
    {
        int? none = null, some = 7;
        Assert.Null((object?)none);
        Assert.IsType<int>((object)some);
    }

    [Fact]
    public void String_equality_is_by_value_but_object_equality_is_by_reference()
    {
        var a = new string(['a', 'b']);
        var b = new string(['a', 'b']);
        var valueEqual = a == b;
        Assert.True(valueEqual);
        Assert.False(ReferenceEquals(a, b));
        object oa = a, ob = b;
        var refEqual = oa == ob;
        Assert.False(refEqual);
    }

    [Fact]
    public void Interning_makes_equal_strings_share_one_instance()
    {
        var literal = "interned-test";
        var runtime = new string("interned-test".ToCharArray());
        Assert.False(ReferenceEquals(literal, runtime));
        Assert.True(ReferenceEquals(literal, string.Intern(runtime)));
    }

    [Fact]
    public void Turkish_culture_changes_casing_of_I()
    {
        var tr = new CultureInfo("tr-TR");
        Assert.Equal("TİTLE", "title".ToUpper(tr));
        Assert.Equal("TITLE", "title".ToUpperInvariant());
        Assert.Equal("fıle", "FILE".ToLower(tr));
        Assert.NotEqual("file", "FILE".ToLower(tr));
    }

    [Fact]
    public void OrdinalIgnoreCase_is_culture_independent()
    {
        var original = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo("tr-TR");
            var cultureEqual = "FILE".ToLower() == "file";
            Assert.False(cultureEqual);
            var ignoreCaseEqual = "FILE".Equals("file", StringComparison.OrdinalIgnoreCase);
            Assert.True(ignoreCaseEqual);
        }
        finally { CultureInfo.CurrentCulture = original; }
    }

    [Fact]
    public void StringBuilder_allocates_far_less_than_repeated_concatenation()
    {
        var concat = Alloc(() => { var s = ""; for (var i = 0; i < 2000; i++) s += "x"; });
        var builder = Alloc(() => { var sb = new System.Text.StringBuilder(); for (var i = 0; i < 2000; i++) sb.Append('x'); _ = sb.ToString(); });
        Assert.True(concat > builder * 20);
    }
}
