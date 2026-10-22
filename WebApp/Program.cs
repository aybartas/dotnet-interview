using Junior.DevFoundations.HowTheWebWorks;

// HTTP sunucusu gerektiren konular bu uygulamada çalışır; her konu kendi servislerini ve uç noktalarını kendi sınıfından ekler.
//
//   dotnet run                  → http://localhost:5080
//   dotnet run -- --https       → ayrıca https://localhost:5443  (önce: dotnet dev-certs https --trust)

var builder = WebApplication.CreateBuilder(args);
HowTheWebWorksTopic.AddServices(builder, useHttps: args.Contains("--https"));   // J1.3 Web nasıl çalışır?

var app = builder.Build();
HowTheWebWorksTopic.Map(app);                                                    // J1.3 (global middleware + MapFallback kullanır; yeni bir web konusu eklenirken rota grubuna alınmalı)
// Sonraki web konuları (J7 Web API, M5 ASP.NET Core …) buraya aynı biçimde eklenir.

app.Run();
