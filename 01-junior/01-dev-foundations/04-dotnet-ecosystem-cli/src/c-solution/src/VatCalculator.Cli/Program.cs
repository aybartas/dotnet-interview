using System.Globalization;
using Junior.DevFoundations.DotnetEcosystemCli.Vat;

// (c) Solution içindeki ince CLI: sadece girdi/çıktı. Hesap mantığı Core'da (test edilebilir) durur.
if (args.Length < 1 || !decimal.TryParse(args[0], CultureInfo.InvariantCulture, out var net))
{
    Console.Error.WriteLine("Kullanım: dotnet run --project src/VatCalculator.Cli -- <net tutar> [KDV yüzdesi=20]");
    return 1;
}

var rate = args.Length > 1 && decimal.TryParse(args[1], CultureInfo.InvariantCulture, out var r) ? r : 20m;

Console.WriteLine($"{net:N2} + %{rate:N0} KDV = {VatMath.WithVat(net, rate):N2}");
return 0;
