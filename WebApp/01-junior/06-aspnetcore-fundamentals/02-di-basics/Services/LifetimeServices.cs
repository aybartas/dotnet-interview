namespace Junior.AspNetCoreFundamentals.DiBasics;

// Three services, one per lifetime. They are identical on purpose: the only difference is HOW they are registered
// (AddTransient / AddScoped / AddSingleton). Each instance gets a Guid in its constructor, so "which Id changed?"
// tells us whether the container created a NEW instance or handed out an existing one.

public interface ITransientService { Guid Id { get; } }
public interface IScopedService { Guid Id { get; } }
public interface ISingletonService { Guid Id { get; } }

public sealed class TransientService : ITransientService
{
    public Guid Id { get; } = Guid.NewGuid();   // runs once per instance → a new Id means a new instance
}

public sealed class ScopedService : IScopedService
{
    public Guid Id { get; } = Guid.NewGuid();
}

public sealed class SingletonService : ISingletonService
{
    public Guid Id { get; } = Guid.NewGuid();
}

/// <summary>
/// A consumer that receives all three services through its CONSTRUCTOR (constructor injection).
/// The consumer itself is registered as Transient; that does not matter here — what matters is which instances of the three
/// dependencies the container passes in. Every dependency is an interface, so this class never calls <c>new</c> on them.
/// </summary>
public sealed class LifetimeConsumer(
    ITransientService transient,
    IScopedService scoped,
    ISingletonService singleton)
{
    public ITransientService Transient { get; } = transient;
    public IScopedService Scoped { get; } = scoped;
    public ISingletonService Singleton { get; } = singleton;
}

static class GuidExtensions
{
    /// <summary>First 8 hex characters: enough to tell instances apart and short enough for a table.</summary>
    public static string Short(this Guid id) => id.ToString("N")[..8];
}
