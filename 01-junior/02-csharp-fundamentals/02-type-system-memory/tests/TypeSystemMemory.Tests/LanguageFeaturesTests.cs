namespace Junior.CSharpFundamentals.TypeSystemMemory.Tests;

public class LanguageFeaturesTests
{
    private enum Status { Pending = 1, Shipped = 2 }

    [Flags] private enum Perm { None = 0, Read = 1, Write = 2, Execute = 4 }

    [Fact]
    public void Enum_cast_accepts_undefined_values_and_IsDefined_detects_them()
    {
        var bogus = (Status)99;
        Assert.False(Enum.IsDefined(bogus));
    }

    [Fact]
    public void Enum_TryParse_accepts_numeric_text_even_if_undefined()
    {
        Assert.True(Enum.TryParse<Status>("99", out var value));
        Assert.False(Enum.IsDefined(value));
        Assert.False(Enum.TryParse<Status>("Nope", out _));
    }

    [Fact]
    public void Flags_enum_combines_and_removes_bits()
    {
        var p = Perm.Read | Perm.Write;
        Assert.True(p.HasFlag(Perm.Write));
        p &= ~Perm.Write;
        Assert.Equal(Perm.Read, p);
        Assert.Equal("Read, Execute", (Perm.Read | Perm.Execute).ToString());
    }

    [Fact]
    public void Nullable_lifted_comparisons_with_null_are_false()
    {
        int? n = null;
        Assert.False(n < 5);
        Assert.False(n >= 5);
        Assert.Null(n + 1);
    }

    [Fact]
    public void Null_conditional_and_coalescing_operators()
    {
        string? s = null;
        Assert.Null(s?.Length);
        Assert.Equal("x", s ?? "x");
        s ??= "a";
        s ??= "b";
        Assert.Equal("a", s);
    }

    [Fact]
    public void ValueTuples_compare_element_wise_and_deconstruct()
    {
        Assert.True((1, "a") == (1, "a"));
        var (min, max) = (2, 9);
        (min, max) = (max, min);
        Assert.Equal((9, 2), (min, max));
    }

    [Fact]
    public void Dynamic_binds_at_runtime_and_fails_at_runtime()
    {
        dynamic d = "text";
        Assert.Equal(4, (int)d.Length);
        Assert.Throws<Microsoft.CSharp.RuntimeBinder.RuntimeBinderException>(() => d.Missing);
    }

    [Fact]
    public void ref_parameter_lets_the_callee_reassign_the_callers_variable()
    {
        var list = new List<string> { "a" };
        static void Replace(ref List<string> l) => l = ["new"];
        Replace(ref list);
        Assert.Equal(["new"], list);
    }

    [Fact]
    public void ref_return_gives_in_place_access_to_array_elements()
    {
        var arr = new[] { 1, 2, 3 };
        static ref int Find(int[] a, int v) { for (var i = 0; i < a.Length; i++) if (a[i] == v) return ref a[i]; throw new KeyNotFoundException(); }
        Find(arr, 2) = 20;
        Assert.Equal([1, 20, 3], arr);
    }

    [Fact]
    public void Const_is_a_literal_field_and_static_readonly_is_not()
    {
        var literal = typeof(Consts).GetField(nameof(Consts.Max))!;
        var ro = typeof(Consts).GetField(nameof(Consts.Started))!;
        Assert.True(literal.IsLiteral);
        Assert.False(ro.IsLiteral);
        Assert.True(ro.IsInitOnly);
    }

    private static class Consts { public const int Max = 1; public static readonly DateTime Started = DateTime.Now; }
}
