using Junior.DevFoundations.DotnetEcosystemCli.Vat;

namespace Junior.DevFoundations.DotnetEcosystemCli.Vat.Tests;

public class VatMathTests
{
    [Theory]
    [InlineData(100, 20, 120.00)]
    [InlineData(100, 0, 100.00)]
    [InlineData(19.99, 18, 23.59)]      // 23,5882 → 23,59
    [InlineData(0.05, 10, 0.06)]        // 0,055 → 0,06 (0,5 yukarı yuvarlanır; banker's rounding olsaydı 0,05 olurdu)
    public void WithVat_AddsRateAndRoundsToTwoDecimals(decimal net, decimal rate, decimal expected)
        => Assert.Equal(expected, VatMath.WithVat(net, rate));

    [Fact]
    public void WithVat_NegativeNet_Throws()
        => Assert.Throws<ArgumentOutOfRangeException>(() => VatMath.WithVat(-1, 20));
}
