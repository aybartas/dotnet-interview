namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section07_PrimaryConstructors
{
    public static void Run()
    {
        // OLD: field + ctor + assignment boilerplate
        var oldStyle = new GreeterOld(new Logger(), "Hello");
        // NEW: primary constructor — the parameters can be used INSIDE the class (they are captured)
        var newStyle = new Greeter(new Logger(), "Hello");
        Console.WriteLine($"old   : {oldStyle.Greet("Alice")}");
        Console.WriteLine($"new   : {newStyle.Greet("Alice")}");

        // In a class a primary ctor parameter is NOT A PROPERTY (it's a captured hidden field); in a record it IS a property
        var prop = typeof(Greeter).GetProperty("Prefix");
        Console.WriteLine($"\nis a class primary ctor parameter a property?  typeof(Greeter).GetProperty(\"Prefix\") = {(prop is null ? "null (NO)" : "exists")}");
        Console.WriteLine($"is a record primary ctor parameter a property? typeof(Point).GetProperty(\"X\")      = {(typeof(Point).GetProperty("X") is null ? "null" : "EXISTS (automatic init property)")}");

        // Exposing a parameter as a property and validating it
        var account = new Account("ACC-1", 100m);
        Console.WriteLine($"\nvalidation: {account.Id} balance {account.Balance}");
        try { _ = new Account("", 1m); }
        catch (ArgumentException ex) { Console.WriteLine($"   empty id → {ex.GetType().Name}"); }

        // Primary ctor + mutating the captured parameter = surprise
        var counter = new Counter(10);
        counter.Increment(); counter.Increment();
        Console.WriteLine($"\nthe captured parameter is a VARIABLE: Counter(10) after two increments = {counter.Value}  (the parameter is used as state — careful)");

        Console.WriteLine("""

        Summary of the rules:
          • class/struct primary constructor (C# 12): the parameter is visible in the CLASS BODY; no automatic property/field is GENERATED. If needed: `public string X { get; } = x;`
          • record primary constructor: parameter → init-only PROPERTY + Deconstruct + Equals (automatic)
          • Using a parameter directly WITHOUT copying it into fields makes the parameter be "captured" into a hidden field (the compiler warns: CS9124 if the same parameter is both captured and used in a property initializer)
          • For validation, check the parameter in the property initializer: `public string Id { get; } = !string.IsNullOrEmpty(id) ? id : throw new ArgumentException(…)`
          • The most common use with DI: `class OrderService(IRepo repo, ILogger<OrderService> logger)` — no boilerplate (→ J6.2)
          • A captured parameter is mutable: changing it like `count++` hides state; keep it immutable
        """);
    }

    private sealed class Logger { public void Log(string message) { } }

    private sealed class GreeterOld
    {
        private readonly Logger _logger;
        private readonly string _prefix;

        public GreeterOld(Logger logger, string prefix)
        {
            _logger = logger;
            _prefix = prefix;
        }

        public string Greet(string name)
        {
            _logger.Log($"greet {name}");
            return $"{_prefix}, {name}!";
        }
    }

    private sealed class Greeter(Logger logger, string prefix)
    {
        public string Greet(string name)
        {
            logger.Log($"greet {name}");
            return $"{prefix}, {name}!";
        }
    }

    private sealed record Point(int X, int Y);

    private sealed class Account(string id, decimal balance)
    {
        public string Id { get; } = !string.IsNullOrWhiteSpace(id) ? id : throw new ArgumentException("Id cannot be empty", nameof(id));
        public decimal Balance { get; } = balance;
    }

    private sealed class Counter(int start)
    {
        public int Value => start;
        public void Increment() => start++;                // MUTATES the captured parameter
    }
}
