using System.Globalization;

// (b) Console projesi — a-file-based/vat.cs ile AYNI uygulama.  Çalıştırma:  dotnet run -- 100 20
// Fark: artık bir .csproj var → derleme ayarları, paket referansları, birden çok dosya, IDE desteği, test edilebilirlik.

if (args.Length < 1 || !decimal.TryParse(args[0], CultureInfo.InvariantCulture, out var net))
{
    Console.Error.WriteLine("Kullanım: dotnet run -- <net tutar> [KDV yüzdesi=20]");
    return 1;
}

var rate = args.Length > 1 && decimal.TryParse(args[1], CultureInfo.InvariantCulture, out var r) ? r : 20m;

Console.WriteLine($"{net:N2} + %{rate:N0} KDV = {VatMath.WithVat(net, rate):N2}");
return 0;

static class VatMath
{
    public static decimal WithVat(decimal net, decimal ratePercent)
        => Math.Round(net * (1 + ratePercent / 100m), 2, MidpointRounding.AwayFromZero);
}
