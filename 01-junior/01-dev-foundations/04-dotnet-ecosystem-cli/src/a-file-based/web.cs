// (a-2) FILE-BASED WEB APP — aynı hesap, bu sefer HTTP üzerinden.  Çalıştırma:  dotnet run web.cs
//   → http://localhost:5090/vat?net=100&rate=20
//
// #:sdk ile Web SDK seçilir (ASP.NET Core Minimal API); csproj gerekmez.
#:sdk Microsoft.NET.Sdk.Web
// .NET 10'da file-based app'ler varsayılan olarak PublishAot=true ile gelir. Native AOT, yansıma (reflection) tabanlı
// JSON serileştirmeyi kapatır → anonim nesne döndüren endpoint 500 verir. AOT'a hazır değilsek kapatırız:
#:property PublishAot=false

var app = WebApplication.CreateBuilder(args).Build();

app.MapGet("/vat", (decimal net, decimal? rate) =>
{
    var r = rate ?? 20m;
    return new { net, rate = r, total = Math.Round(net * (1 + r / 100m), 2, MidpointRounding.AwayFromZero) };
});

app.Run("http://localhost:5090");
