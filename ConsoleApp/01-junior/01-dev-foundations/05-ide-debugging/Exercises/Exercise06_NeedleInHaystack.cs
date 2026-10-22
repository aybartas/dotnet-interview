namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise06_NeedleInHaystack : Exercise
{
    public override int Number => 6;
    public override string Title => "Corrupt data in one of 10,000 records";
    public override string Skill => "Conditional breakpoint and tracepoint (logging without stopping at the breakpoint)";
    public override string Hint => "A normal breakpoint in the loop stops 10,000 times. Conditional breakpoint: 'order.Quantity < 0'. Or a tracepoint: print '{order.Id} {order.Quantity}' without stopping.";

    private sealed record Order(int Id, decimal UnitPrice, int Quantity);

    public override void Run()
    {
        var orders = LoadOrders();

        // The correct formula: quantity is always (Id % 5) + 1.
        var expected = orders.Sum(o => o.UnitPrice * ((o.Id % 5) + 1));
        var actual = 0m;
        foreach (var order in orders)
            actual += order.UnitPrice * order.Quantity;      // ← why doesn't the total match? Which record?

        Report("invoice total", expected: expected, actual: actual);
        Console.WriteLine("  Question: what is the Id of the record that breaks the total? (You don't want to read 10,000 lines of log without a debugger.)");
    }

    private static List<Order> LoadOrders()
    {
        var list = new List<Order>(10_000);
        for (var id = 1; id <= 10_000; id++)
        {
            var quantity = id == 7345 ? -3 : (id % 5) + 1;    // a glitch in the data source: negative quantity (find it with the debugger, not by reading the code)
            list.Add(new Order(id, UnitPrice: 10m + (id % 7), quantity));
        }
        return list;
    }
}
