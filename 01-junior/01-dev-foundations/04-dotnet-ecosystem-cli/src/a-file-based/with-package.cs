// (a-3) #:package ile NuGet paketi — Newtonsoft.Json ile KDV sonucunu JSON'a çevirme.
//   Çalıştırma:  dotnet run with-package.cs
// Paket ve sürüm direktifte yazılır:  #:package Paket@sürüm   (csproj'a geçerken <PackageReference> olur)
#:package Newtonsoft.Json@13.0.3
// Bu repo kökte Central Package Management (Directory.Packages.props) kullanır; CPM, #:package@sürüm ile çakışır (NU1008).
// File-based app CPM'in dışına alınır. Gerçek projeye dönüştürürken sürüm Directory.Packages.props'a taşınır.
#:property ManagePackageVersionsCentrally=false

using Newtonsoft.Json;

var result = new { Net = 100m, Rate = 20m, Total = 120m };
Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
