namespace Junior.DevFoundations.DotnetEcosystemCli.Vat;

public static class VatMath
{
    /// <summary>Net tutara KDV ekler; kuruş yuvarlaması "0,5 yukarı" (AwayFromZero) kuralıyla yapılır.</summary>
    /// <exception cref="ArgumentOutOfRangeException">Net tutar veya oran negatifse.</exception>
    public static decimal WithVat(decimal net, decimal ratePercent)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(net);
        ArgumentOutOfRangeException.ThrowIfNegative(ratePercent);

        return Math.Round(net * (1 + ratePercent / 100m), 2, MidpointRounding.AwayFromZero);
    }
}
