using Junior.CSharpFundamentals.ModernCSharp.Comparison;

namespace Junior.CSharpFundamentals.ModernCSharp.Tests;

/// <summary>
/// "C# 7 tarzı" ve "C# 14 tarzı" iki domain modelinin AYNI davranışı verdiğinin kanıtı:
/// abstract taban sınıf davranışı yazar, iki alt sınıf iki stili bağlar (refactor güvenliği — J2.3'teki yöntemle aynı).
/// </summary>
public abstract class OrderBehaviorContract
{
    protected abstract IOrderFacade Sut { get; }

    private static readonly IReadOnlyList<(string Product, int Qty, decimal Price)> Lines = [("Kalem", 2, 12.5m), ("Defter", 1, 40m), ("Çanta", 1, 980m)];

    [Theory]
    [InlineData("Platinum", 1500, 0.20)]
    [InlineData("Platinum", 500, 0.15)]
    [InlineData("Gold", 1500, 0.15)]
    [InlineData("Gold", 1000, 0.10)]        // sınır: 1000 "bulk" DEĞİL (> 1000)
    [InlineData("Gold", 1000.01, 0.15)]
    [InlineData("Standard", 1500, 0.05)]
    [InlineData("Standard", 500, 0.00)]
    public void Discount_rate_depends_on_tier_and_bulk_threshold(string tier, double subtotal, double expected)
        => Assert.Equal((decimal)expected, Sut.DiscountRate(tier, (decimal)subtotal));

    [Fact]
    public void Total_applies_the_discount_to_the_subtotal()
        => Assert.Equal(888.25m, Sut.Total("Gold", Lines));          // 1045 × 0.85

    [Fact]
    public void Receipt_is_formatted_identically()
    {
        var receipt = Sut.Receipt("Gold", "Ayşe Yılmaz", "  AYSE@Example.com ", "Ankara", Lines);

        Assert.Equal(
            "SİPARİŞ ORD-1 — Ayşe Yılmaz <ayse@example.com>\n" +
            "Gold müşteri · Ankara\n" +
            "  2 × Kalem = 25.00\n" +
            "  1 × Defter = 40.00\n" +
            "  1 × Çanta = 980.00\n" +
            "Ara toplam: 1045.00 · İndirim %15 → 888.25", receipt);
    }

    [Fact]
    public void Receipt_shows_a_placeholder_when_there_is_no_address()
        => Assert.Contains("adres yok", Sut.Receipt("Standard", "Ali", "a@b.c", null, Lines));

    [Theory]
    [InlineData("Pending", "pay", "Paid")]
    [InlineData("Paid", "ship", "Shipped")]
    [InlineData("Shipped", "deliver", "Delivered")]
    [InlineData("Pending", "cancel", "Cancelled")]
    [InlineData("Paid", "cancel", "Cancelled")]
    public void Valid_status_transitions(string from, string action, string expected)
        => Assert.Equal(expected, Sut.Advance(from, action));

    [Theory]
    [InlineData("Delivered", "pay")]
    [InlineData("Shipped", "cancel")]
    [InlineData("Pending", "ship")]
    [InlineData("Cancelled", "pay")]
    public void Invalid_status_transitions_throw(string from, string action)
    {
        var ex = Assert.Throws<InvalidOperationException>(() => Sut.Advance(from, action));
        Assert.Contains(from, ex.Message);
        Assert.Contains(action, ex.Message);
    }

    [Theory]
    [InlineData(null, "adres yok")]
    [InlineData("İzmir", "İzmir")]
    public void Shipping_label(string? city, string expected)
        => Assert.Equal(expected, Sut.ShippingLabel(city));

    [Theory]
    [InlineData("  A@B.COM ", "a@b.com")]
    [InlineData("x@y.z", "x@y.z")]
    public void Email_is_trimmed_and_lowercased(string raw, string expected)
        => Assert.Equal(expected, Sut.NormalizeEmail(raw));

    [Fact]
    public void Identically_built_orders_are_equal()
        => Assert.True(Sut.IdenticalOrdersAreEqual());

    [Fact]
    public void Null_conditional_rename_only_assigns_when_the_customer_exists()
    {
        Assert.Equal("yeni", Sut.RenameIfPresent(true, "yeni"));
        Assert.Equal("(müşteri yok)", Sut.RenameIfPresent(false, "yeni"));
    }
}

public sealed class Csharp7Style_Tests : OrderBehaviorContract { protected override IOrderFacade Sut { get; } = new Facade7(); }
public sealed class Csharp14Style_Tests : OrderBehaviorContract { protected override IOrderFacade Sut { get; } = new Facade14(); }

public class StyleComparisonTests
{
    [Fact]
    public void Both_styles_produce_byte_for_byte_identical_receipts()
    {
        IReadOnlyList<(string, int, decimal)> lines = [("Kalem", 2, 12.5m), ("Çanta", 1, 980m)];
        Assert.Equal(
            new Facade7().Receipt("Platinum", "A", "a@b.c", "Ankara", lines),
            new Facade14().Receipt("Platinum", "A", "a@b.c", "Ankara", lines));
    }

    [Fact]
    public void The_modern_style_needs_far_fewer_lines_for_the_same_model()
    {
        var loc7 = LineCounter.Count(Path.Combine(LineCounter.StyleDirectory("Csharp7Style"), "Domain.cs"));
        var loc14 = LineCounter.Count(Path.Combine(LineCounter.StyleDirectory("Csharp14Style"), "Domain.cs"));

        Assert.True(loc14 < loc7 * 0.4, $"C# 14 tarzı ({loc14}) C# 7 tarzının ({loc7}) %40'ından az olmalı");
    }
}
