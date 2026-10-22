using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Junior.AsyncBasics.Shared;

/// <summary>
/// Demolar için yerel, internetsiz bir Kestrel API'si (rastgele boş port). HTTP çağrılarının "dışarıdaki yavaş servis"i bu sunucudur:
///   GET /delay/{ms}  → ms kadar bekleyip cevap verir (bekleme istemci kopunca iptal olur).
/// Ek uç noktalar için <c>map</c> verilir.
/// </summary>
public sealed class LocalApi : IAsyncDisposable
{
    private readonly WebApplication _app;

    public string BaseUrl { get; }
    public HttpClient CreateClient() => new() { BaseAddress = new Uri(BaseUrl) };

    private LocalApi(WebApplication app, string baseUrl) => (_app, BaseUrl) = (app, baseUrl);

    public static async Task<LocalApi> StartAsync(Action<WebApplication>? map = null)
    {
        var builder = WebApplication.CreateSlimBuilder();
        builder.Logging.ClearProviders();                       // demo çıktısını sunucu loglarıyla kirletme
        builder.WebHost.UseUrls("http://127.0.0.1:0");          // 0 = işletim sistemi boş bir port seçsin
        var app = builder.Build();

        app.MapGet("/delay/{ms:int}", async (int ms, CancellationToken ct) =>
        {
            await Task.Delay(ms, ct);
            return $"{ms} ms sonra cevap";
        });

        map?.Invoke(app);

        await app.StartAsync();
        var url = app.Services.GetRequiredService<IServer>().Features.Get<IServerAddressesFeature>()!.Addresses.First();
        return new LocalApi(app, url);
    }

    public async ValueTask DisposeAsync()
    {
        await _app.StopAsync();
        await _app.DisposeAsync();
    }
}
