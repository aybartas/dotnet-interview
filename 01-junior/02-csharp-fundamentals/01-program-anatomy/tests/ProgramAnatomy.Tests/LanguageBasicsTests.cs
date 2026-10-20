using System.Runtime.CompilerServices;

namespace Junior.CSharpFundamentals.ProgramAnatomy.Tests;

public class LanguageBasicsTests
{
    [Fact]
    public void Unchecked_overflow_wraps_silently()
    {
        var max = int.MaxValue;
        Assert.Equal(int.MinValue, unchecked(max + 1));
    }

    [Fact]
    public void Checked_overflow_throws()
    {
        var max = int.MaxValue;
        Assert.Throws<OverflowException>(() => checked(max + 1));
    }

    [Fact]
    public void Convert_uses_bankers_rounding_but_Math_Round_can_round_away_from_zero()
    {
        Assert.Equal(2, Convert.ToInt32(2.5));
        Assert.Equal(4, Convert.ToInt32(3.5));
        Assert.Equal(3, (int)Math.Round(2.5, MidpointRounding.AwayFromZero));
    }

    [Fact]
    public void TryParse_does_not_throw_on_bad_input()
    {
        Assert.False(int.TryParse("12a", out var value));
        Assert.Equal(0, value);
        Assert.Throws<FormatException>(() => int.Parse("12a"));
    }

    [Fact]
    public void Short_circuit_and_skips_right_side_but_bitwise_and_does_not()
    {
        var calls = 0;
        bool Right() { calls++; return true; }

        _ = false && Right();
        Assert.Equal(0, calls);

        _ = false & Right();
        Assert.Equal(1, calls);
    }

    [Fact]
    public void CallerArgumentExpression_captures_the_source_text()
    {
        string? name = null;
        var ex = Assert.Throws<ArgumentNullException>(() => Guard.NotNull(name));
        Assert.Equal("name", ex.ParamName);
    }

    [Fact]
    public void Partial_class_combines_both_files()
    {
        var invoice = new Invoice("INV-1", 100m);
        Assert.Equal(120.00m, invoice.Gross);              // Invoice.Generated.cs'ten
        Assert.Contains("INV-1", invoice.Describe());      // Invoice.cs'ten
    }

    [Fact]
    public void Generated_regex_partial_method_works()
        => Assert.Equal("4521", Generated.Digits().Match("no: 4521").Value);

    [Fact]
    public void Top_level_statements_produce_a_compiler_generated_entry_point()
    {
        var entry = typeof(Section01_EntryPoint).Assembly.EntryPoint!;
        Assert.Equal("<Main>$", entry.Name);
    }
}
