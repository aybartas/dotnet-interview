namespace Junior.AspNetCoreFundamentals.DiBasics;

// The deliberately broken case: a SINGLETON that depends on a SCOPED service (captive dependency).
// These types are NOT registered in the app's container — CaptiveDemo registers them in its own ServiceProviders,
// otherwise the app itself would refuse to start in Development (ValidateOnBuild).

/// <summary>Scoped on purpose (think: a DbContext). Implements IDisposable so we can see WHEN the container disposes it.</summary>
public sealed class ScopedDependency : IDisposable
{
    public Guid Id { get; } = Guid.NewGuid();
    public bool IsDisposed { get; private set; }

    public void Dispose() => IsDisposed = true;
}

/// <summary>BROKEN: a singleton lives for the whole application but takes a scoped service in its constructor.</summary>
public sealed class CaptiveSingleton(ScopedDependency scoped)
{
    public ScopedDependency Scoped { get; } = scoped;   // "captured" at construction time and kept for the rest of the app's life
}

/// <summary>FIXED: the singleton asks for a short-lived scope each time it needs a scoped service.</summary>
public sealed class FixedSingleton(IServiceScopeFactory scopeFactory)
{
    public Guid UseScopedService()
    {
        using var scope = scopeFactory.CreateScope();                       // a fresh scope per operation
        var dependency = scope.ServiceProvider.GetRequiredService<ScopedDependency>();
        return dependency.Id;                                               // (the scope — and the dependency — are disposed on return)
    }
}
