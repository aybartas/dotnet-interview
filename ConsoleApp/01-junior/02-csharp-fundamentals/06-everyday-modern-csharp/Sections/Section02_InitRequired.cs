namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section02_InitRequired
{
    public static void Run()
    {
        // init-only: can be assigned only WHILE THE OBJECT IS BEING CREATED (object initializer / ctor / with), read-only afterwards
        var settings = new ConnectionSettings { Host = "localhost", Port = 5432 };
        // settings.Port = 1;                                   // ✗ CS8852: an init-only property can only be assigned in an object initializer
        var changed = new ConnectionSettings { Host = settings.Host, Port = 6000 };
        Console.WriteLine($"init-only      : {settings.Host}:{settings.Port} → new instance {changed.Host}:{changed.Port}");

        // required: assigning this member when creating the object is MANDATORY (otherwise a COMPILE error); no 'half-built' object
        // var bad = new ConnectionSettings { Port = 1 };       // ✗ CS9035: required 'Host' was not assigned
        Console.WriteLine($"required       : omitting Host is a compile error (CS9035); Port has a default → can be omitted: {new ConnectionSettings { Host = "x" }.Port}");

        // required + init together: the most practical pattern for a validated, immutable, complete object (without writing a constructor)
        var order = new OrderDto { Id = "ORD-1", Total = 10m };
        Console.WriteLine($"DTO            : {order.Id} / {order.Total}");

        // the with expression 'changes' init properties on records (actually produces a copy)
        Console.WriteLine($"required + record: {new Contact { Email = "a@x.com" } with { Email = "b@x.com" }}");

        Console.WriteLine("""

        Why? Before: either a parameterized ctor (the parameter count explodes) or set; (the object can change later, stays half-built).
        init → "assign while constructing, immutable afterwards"; required → "you can't forget to assign". Combined, they let you write an immutable DTO without a ctor.
        Serializers (System.Text.Json) support init-only and required; in EF Core entities usually use set;.
        Inside a constructor, `[SetsRequiredMembers]` can skip the required check (use with care).
        """);
    }

    private sealed class ConnectionSettings
    {
        public required string Host { get; init; }
        public int Port { get; init; } = 5432;
    }

    private sealed class OrderDto
    {
        public required string Id { get; init; }
        public required decimal Total { get; init; }
    }

    private sealed record Contact
    {
        public required string Email { get; init; }
    }
}
