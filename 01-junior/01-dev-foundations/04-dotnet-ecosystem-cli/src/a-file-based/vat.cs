// (a) FILE-BASED APP — .csproj YOK, tek dosya.  Çalıştırma:  dotnet run vat.cs -- 100 20
//
// Dosyanın başındaki #: direktifleri MSBuild ayarlarının yerini tutar:
//   #:property  → bir MSBuild property'si (csproj'daki <PropertyGroup> karşılığı)
//   #:package   → NuGet paketi        (csproj'daki <PackageReference>)
//   #:sdk       → kullanılacak SDK    (varsayılan Microsoft.NET.Sdk; web için Microsoft.NET.Sdk.Web)
//   #:project   → başka bir projeye referans
#:property Nullable=enable

using System.Globalization;

// Top-level statements: derleyici bunu gizli bir Program sınıfı + Main'e çevirir (→ J2.1).
// args = "--" işaretinden sonrakiler:  dotnet run vat.cs -- 100 20
if (args.Length < 1 || !decimal.TryParse(args[0], CultureInfo.InvariantCulture, out var net))
{
    Console.Error.WriteLine("Kullanım: dotnet run vat.cs -- <net tutar> [KDV yüzdesi=20]");
    return 1;
}

var rate = args.Length > 1 && decimal.TryParse(args[1], CultureInfo.InvariantCulture, out var r) ? r : 20m;
var result = VatMath.WithVat(net, rate);

Console.WriteLine($"{net:N2} + %{rate:N0} KDV = {result:N2}");
Console.WriteLine($"[bilgi] Çalışan runtime: {System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription}");
return 0;

// File-based app'te tipler dosyanın SONUNDA (top-level statement'lardan sonra) tanımlanır.
static class VatMath
{
    public static decimal WithVat(decimal net, decimal ratePercent)
        => Math.Round(net * (1 + ratePercent / 100m), 2, MidpointRounding.AwayFromZero);
}
