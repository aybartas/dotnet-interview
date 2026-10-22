using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Junior.AsyncBasics.Shared;

/// <summary>
/// A local, internet-free Kestrel API for the demos (random free port). This server is the "slow external service" behind the HTTP calls:
///   GET /delay/{ms}  → waits ms milliseconds and then responds (the wait is cancelled when the client disconnects).
/// Pass <c>map</c> to add extra endpoints.
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
        builder.Logging.ClearProviders();                       // do not pollute the demo output with server logs
        builder.WebHost.UseUrls("http://127.0.0.1:0");          // 0 = let the operating system pick a free port
        var app = builder.Build();

        app.MapGet("/delay/{ms:int}", async (int ms, CancellationToken ct) =>
        {
            await Task.Delay(ms, ct);
            return $"response after {ms} ms";
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
