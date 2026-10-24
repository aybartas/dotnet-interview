using System.Text;

namespace Junior.AspNetCoreFundamentals.DiBasics;

/// <summary>
/// Captive dependency, shown WITHOUT touching the app's own container: every case builds a separate ServiceProvider.
/// The same registrations (scoped ScopedDependency + singleton CaptiveSingleton) are used three times:
///   1) ValidateScopes = true               → resolving the singleton throws InvalidOperationException
///   2) ValidateScopes + ValidateOnBuild    → BuildServiceProvider already throws (fail fast, at startup)
///   3) no validation (the production default) → everything "works" and the bug stays silent
/// then the fix (IServiceScopeFactory) is run on a validating provider.
/// </summary>
public static class CaptiveDemo
{
    public static string Run()
    {
        var sb = new StringBuilder();

        // ── 1) ValidateScopes = true: caught when the singleton is resolved ─────────────────────────
        sb.AppendLine("1) ValidateScopes = true");
        using (var provider = CaptiveServices().BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true }))
        {
            using var scope = provider.CreateScope();
            try
            {
                scope.ServiceProvider.GetRequiredService<CaptiveSingleton>();
                sb.AppendLine("   no error (unexpected)");
            }
            catch (Exception ex)
            {
                sb.AppendLine($"   {ex.GetType().Name}: {ex.Message}");
            }
        }

        // ── 2) ValidateScopes + ValidateOnBuild: caught even earlier, before anything is resolved ─────
        sb.AppendLine().AppendLine("2) ValidateScopes = true + ValidateOnBuild = true");
        try
        {
            using var provider = CaptiveServices().BuildServiceProvider(
                new ServiceProviderOptions { ValidateScopes = true, ValidateOnBuild = true });
            sb.AppendLine("   no error (unexpected)");
        }
        catch (Exception ex)
        {
            sb.AppendLine($"   {ex.GetType().Name}: {ex.Message}");
            // ValidateOnBuild wraps every problem it finds into one AggregateException; the root cause is the same InvalidOperationException.
            if (ex is AggregateException agg)
                foreach (var inner in agg.InnerExceptions)
                    sb.AppendLine($"   inner -> {inner.GetType().Name}: {inner.Message}");
        }

        // ── 3) No validation (default outside Development): the bug is silent ──────────────────────
        sb.AppendLine().AppendLine("3) No validation (production default)");
        using (var provider = CaptiveServices().BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = false }))
        {
            CaptiveSingleton captiveFromScope1;
            ScopedDependency scoped1;
            using (var scope1 = provider.CreateScope())    // "request 1"
            {
                captiveFromScope1 = scope1.ServiceProvider.GetRequiredService<CaptiveSingleton>();
                scoped1 = scope1.ServiceProvider.GetRequiredService<ScopedDependency>();
                sb.AppendLine($"   scope 1: scoped service Id = {scoped1.Id.Short()}   singleton's captured Id = {captiveFromScope1.Scoped.Id.Short()}");
            }                                              // scope 1 is disposed here → scoped1 is disposed

            using (var scope2 = provider.CreateScope())    // "request 2"
            {
                var captiveFromScope2 = scope2.ServiceProvider.GetRequiredService<CaptiveSingleton>();
                var scoped2 = scope2.ServiceProvider.GetRequiredService<ScopedDependency>();
                sb.AppendLine($"   scope 2: scoped service Id = {scoped2.Id.Short()}   singleton's captured Id = {captiveFromScope2.Scoped.Id.Short()}");
                sb.AppendLine($"   same singleton instance in both scopes: {ReferenceEquals(captiveFromScope1, captiveFromScope2)}");
                sb.AppendLine($"   captured Id the same in both scopes (\"scoped\" now behaves like a singleton): {captiveFromScope1.Scoped.Id == captiveFromScope2.Scoped.Id}");
            }

            sb.AppendLine($"   scope 1's own scoped instance disposed with its scope: {scoped1.IsDisposed}");
            sb.AppendLine($"   captured instance disposed with the scope: {captiveFromScope1.Scoped.IsDisposed}  (it lives until the root provider/app shuts down)");
        }

        // ── 4) The fix: the singleton takes IServiceScopeFactory and opens a scope per operation ─────
        sb.AppendLine().AppendLine("4) Fix: IServiceScopeFactory inside the singleton (validation ON, no error)");
        var fixedServices = new ServiceCollection();
        fixedServices.AddScoped<ScopedDependency>();
        fixedServices.AddSingleton<FixedSingleton>();
        using (var provider = fixedServices.BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true, ValidateOnBuild = true }))
        {
            var singleton = provider.GetRequiredService<FixedSingleton>();
            sb.AppendLine($"   call 1: scoped Id = {singleton.UseScopedService().Short()}");
            sb.AppendLine($"   call 2: scoped Id = {singleton.UseScopedService().Short()}   (a new scoped instance each call)");
        }

        return sb.ToString();
    }

    private static ServiceCollection CaptiveServices()
    {
        var services = new ServiceCollection();
        services.AddScoped<ScopedDependency>();
        services.AddSingleton<CaptiveSingleton>();   // BUG: a singleton depends on a scoped service
        return services;
    }
}
