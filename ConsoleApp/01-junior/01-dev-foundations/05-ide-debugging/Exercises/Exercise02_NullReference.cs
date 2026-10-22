namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise02_NullReference : Exercise
{
    public override int Number => 2;
    public override string Title => "NullReferenceException chain";
    public override string Skill => "Exception Settings (first-chance) + Call Stack + Locals";
    public override string Hint => "Tick NullReferenceException with Ctrl+Alt+E; stop while the exception is being thrown. Switch to the previous frame in the Call Stack and see which reference is null.";

    private sealed record Address(string City);
    private sealed record Customer(string Name, Address? Address);

    private static readonly Dictionary<string, Customer> Customers = new()
    {
        ["alice"] = new Customer("Alice", new Address("Boston")),
        ["bob"] = new Customer("Bob", new Address("Denver")),
        ["carol"] = new Customer("Carol", Address: null),          // no address entered
    };

    public override void Run()
    {
        foreach (var key in new[] { "alice", "bob", "carol" })
            Console.WriteLine($"  {key} → {ShippingLabel(key)}");
    }

    private static string ShippingLabel(string key) => $"{FindCustomer(key).Name} / {CityOf(FindCustomer(key))}";

    private static Customer FindCustomer(string key) => Customers[key];

    // '!' (null-forgiving) silences the compiler, it doesn't remove the null — it defers the bug to run time.
    private static string CityOf(Customer c) => c.Address!.City.ToUpperInvariant();
}
