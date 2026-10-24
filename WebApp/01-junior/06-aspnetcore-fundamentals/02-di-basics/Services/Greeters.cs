namespace Junior.AspNetCoreFundamentals.DiBasics;

// Several implementations of ONE interface. What the container returns depends on HOW the consumer asks:
//   IEnumerable<IGreeter>  → ALL registrations, in registration order
//   IGreeter               → the LAST registration ("last one wins")

public interface IGreeter
{
    string Language { get; }
    string Greet(string name);
}

public sealed class EnglishGreeter : IGreeter
{
    public string Language => "English";
    public string Greet(string name) => $"Hello, {name}!";
}

public sealed class SpanishGreeter : IGreeter
{
    public string Language => "Spanish";
    public string Greet(string name) => $"Hola, {name}!";
}

public sealed class FrenchGreeter : IGreeter
{
    public string Language => "French";
    public string Greet(string name) => $"Bonjour, {name} !";
}

/// <summary>Registered with TryAdd* only: see DiBasicsTopic.AddServices for which of those registrations are skipped.</summary>
public sealed class GermanGreeter : IGreeter
{
    public string Language => "German";
    public string Greet(string name) => $"Hallo, {name}!";
}

/// <summary>
/// Takes <c>IEnumerable&lt;IGreeter&gt;</c> in its constructor: the container injects EVERY registered IGreeter.
/// This is the usual way to build "plug-in" style code (validators, handlers, strategies) without an if/switch.
/// </summary>
public sealed class GreetingService(IEnumerable<IGreeter> greeters)
{
    public IReadOnlyList<string> GreetEveryone(string name) =>
        greeters.Select(g => $"{g.Language,-8} → {g.Greet(name)}").ToList();
}
