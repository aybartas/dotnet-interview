using Junior.DevFoundations.HowTheWebWorks;

// Topics that need an HTTP server run in this app; each topic adds its own services and endpoints from its own class.
//
//   dotnet run                  → http://localhost:5080
//   dotnet run -- --https       → also https://localhost:5443  (first: dotnet dev-certs https --trust)

var builder = WebApplication.CreateBuilder(args);
HowTheWebWorksTopic.AddServices(builder, useHttps: args.Contains("--https"));   // J1.3 How the web works

var app = builder.Build();
HowTheWebWorksTopic.Map(app);                                                    // J1.3 (uses global middleware + MapFallback; when another web topic is added, move it into a route group)
// Later web topics (J7 Web API, M5 ASP.NET Core …) are added here the same way.

app.Run();
