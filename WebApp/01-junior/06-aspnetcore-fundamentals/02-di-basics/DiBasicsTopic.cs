using System.Text;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Junior.AspNetCoreFundamentals.DiBasics;

/// <summary>
/// J6.2 — Dependency Injection basics. Runs its OWN host on http://localhost:5080 (WebApp/Program.cs calls <see cref="Run"/>):
///
///   cd WebApp
///   dotnet run -- J6.2                              (default environment: Production → the app's container does NOT validate scopes)
///   dotnet run -- J6.2 --environment Development    (Development → ValidateScopes + ValidateOnBuild are ON for the app's container)
///
/// Endpoints (curl examples and real output: README.md):
///   GET /            list of endpoints
///   GET /lifetimes   Transient / Scoped / Singleton resolved twice in ONE request; call it repeatedly to see which Ids change
///   GET /greeters    several IGreeter registrations: IEnumerable&lt;IGreeter&gt; (all) vs IGreeter (the last one) + TryAdd*
///   GET /captive     captive dependency (singleton → scoped), built in separate ServiceProviders with and without validation
/// </summary>
public static class DiBasicsTopic
{
    // Request counter for the /lifetimes output (a plain static field + Interlocked: just a demo counter, not a DI service).
    private static int _requestNumber;

    public static int Run(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);          // args are forwarded: --environment Development, --urls … (UseUrls below wins)
        builder.WebHost.UseUrls("http://localhost:5080");

        var registrationLog = AddServices(builder.Services);

        var app = builder.Build();
        MapEndpoints(app, registrationLog);

        app.Run();
        return 0;
    }

    /// <summary>Everything this topic registers. Returns a log of the TryAdd* calls for the /greeters endpoint.</summary>
    private static List<string> AddServices(IServiceCollection services)
    {
        // ── Lifetimes (/lifetimes) ─────────────────────────────────────────────────────────────
        services.AddTransient<ITransientService, TransientService>();   // a NEW instance on every resolve
        services.AddScoped<IScopedService, ScopedService>();            // one instance per scope (= per HTTP request)
        services.AddSingleton<ISingletonService, SingletonService>();   // one instance for the whole app
        services.AddTransient<LifetimeConsumer>();                      // concrete type, no interface: allowed; its constructor parameters are injected

        // ── Several implementations of one interface (/greeters) ──────────────────────────────
        var log = new List<string>();
        int Count() => services.Count(d => d.ServiceType == typeof(IGreeter));

        services.AddSingleton<IGreeter, EnglishGreeter>();
        services.AddSingleton<IGreeter, SpanishGreeter>();
        services.AddSingleton<IGreeter, FrenchGreeter>();
        log.Add($"Add x3 (English, Spanish, French)               → {Count()} IGreeter registrations");

        // TryAdd: "register only if this SERVICE TYPE has no registration yet" → skipped, IGreeter is already registered.
        var before = Count();
        services.TryAddSingleton<IGreeter, GermanGreeter>();
        log.Add($"TryAddSingleton<IGreeter, GermanGreeter>        → {(Count() == before ? "skipped" : "added")} ({Count()} registrations)");

        // TryAddEnumerable: "register only if this (service type, IMPLEMENTATION type) pair is missing" → the way to add to a list safely.
        before = Count();
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IGreeter, SpanishGreeter>());
        log.Add($"TryAddEnumerable<IGreeter, SpanishGreeter>      → {(Count() == before ? "skipped (that pair already exists)" : "added")} ({Count()} registrations)");

        before = Count();
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IGreeter, GermanGreeter>());
        log.Add($"TryAddEnumerable<IGreeter, GermanGreeter>       → {(Count() == before ? "skipped" : "added (new pair)")} ({Count()} registrations)");

        services.AddTransient<GreetingService>();                        // takes IEnumerable<IGreeter> in its constructor

        // NOT registered here on purpose: the captive-dependency types (see CaptiveDemo). In Development ValidateOnBuild would
        // make the app fail at startup if we registered a singleton that depends on a scoped service in THIS container.
        return log;
    }

    private static void MapEndpoints(WebApplication app, List<string> registrationLog)
    {
        app.MapGet("/", () => Results.Text("""
            J6.2 Dependency Injection basics

              GET /lifetimes      Transient / Scoped / Singleton: which Id changes when?
              GET /greeters       several IGreeter registrations (?name=Ada)
              GET /captive        captive dependency: singleton -> scoped, with and without validation
            """));

        // ── /lifetimes ─────────────────────────────────────────────────────────────────────────
        // The same three services are resolved TWICE inside one request:
        //   (A) directly from ctx.RequestServices  (this is the "service locator" way — fine for a demo, avoid it in real code)
        //   (B) through LifetimeConsumer, which got them by constructor injection (the normal way)
        // `LifetimeConsumer consumer` as a handler parameter is itself resolved from the request's service provider.
        app.MapGet("/lifetimes", (HttpContext ctx, LifetimeConsumer consumer) =>
        {
            var sp = ctx.RequestServices;
            var rows = new (string Lifetime, Guid A, Guid B)[]
            {
                ("Transient", sp.GetRequiredService<ITransientService>().Id, consumer.Transient.Id),
                ("Scoped",    sp.GetRequiredService<IScopedService>().Id,    consumer.Scoped.Id),
                ("Singleton", sp.GetRequiredService<ISingletonService>().Id, consumer.Singleton.Id),
            };

            var sb = new StringBuilder();
            sb.AppendLine($"Request #{Interlocked.Increment(ref _requestNumber)}   (environment: {app.Environment.EnvironmentName})");
            sb.AppendLine();
            sb.AppendLine("Lifetime    (A) ctx.RequestServices   (B) via constructor   same instance in one request?");
            sb.AppendLine("----------  ------------------------  --------------------  ------------------------------");
            foreach (var (lifetime, a, b) in rows)
                sb.AppendLine($"{lifetime,-10}  {a.Short(),-24}  {b.Short(),-20}  {(a == b ? "yes" : "no")}");
            sb.AppendLine();
            sb.AppendLine("Call it again: Transient changes on every resolve, Scoped changes per request, Singleton never changes.");
            return Results.Text(sb.ToString());
        });

        // ── /greeters ──────────────────────────────────────────────────────────────────────────
        app.MapGet("/greeters", (string? name, HttpContext ctx, GreetingService service) =>
        {
            name ??= "world";
            var sp = ctx.RequestServices;

            var sb = new StringBuilder();
            sb.AppendLine("Registration log (startup):");
            foreach (var line in registrationLog) sb.AppendLine($"  {line}");

            sb.AppendLine().AppendLine("IEnumerable<IGreeter>  → ALL registrations, in registration order (injected into GreetingService's constructor):");
            foreach (var line in service.GreetEveryone(name)) sb.AppendLine($"  {line}");

            var last = sp.GetRequiredService<IGreeter>();
            sb.AppendLine().AppendLine("IGreeter (single)      → the LAST registration wins:");
            sb.AppendLine($"  {last.Language,-8} → {last.Greet(name)}   ({last.GetType().Name})");
            return Results.Text(sb.ToString());
        });

        // ── /captive ───────────────────────────────────────────────────────────────────────────
        // The deliberately broken case. Everything happens in ServiceProviders built inside CaptiveDemo, not in the app's container.
        app.MapGet("/captive", () => Results.Text(CaptiveDemo.Run()));
    }
}
