namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise02_NullReference : Exercise
{
    public override int Number => 2;
    public override string Title => "NullReferenceException zinciri";
    public override string Skill => "Exception Settings (first-chance) + Call Stack + Locals";
    public override string Hint => "Ctrl+Alt+E ile NullReferenceException'ı işaretleyin; istisna fırlatılırken durun. Call Stack'te bir önceki çerçeveye geçip hangi referansın null olduğuna bakın.";

    private sealed record Address(string City);
    private sealed record Customer(string Name, Address? Address);

    private static readonly Dictionary<string, Customer> Customers = new()
    {
        ["ayse"] = new Customer("Ayşe", new Address("Bursa")),
        ["mehmet"] = new Customer("Mehmet", new Address("Ankara")),
        ["zeynep"] = new Customer("Zeynep", Address: null),          // adres girilmemiş
    };

    public override void Run()
    {
        foreach (var key in new[] { "ayse", "mehmet", "zeynep" })
            Console.WriteLine($"  {key} → {ShippingLabel(key)}");
    }

    private static string ShippingLabel(string key) => $"{FindCustomer(key).Name} / {CityOf(FindCustomer(key))}";

    private static Customer FindCustomer(string key) => Customers[key];

    // '!' (null-forgiving) derleyiciyi susturur, null'ı ortadan kaldırmaz — hatayı çalışma zamanına erteler.
    private static string CityOf(Customer c) => c.Address!.City.ToUpperInvariant();
}
