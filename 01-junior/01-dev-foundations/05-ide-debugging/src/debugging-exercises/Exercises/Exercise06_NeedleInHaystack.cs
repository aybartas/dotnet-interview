namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise06_NeedleInHaystack : Exercise
{
    public override int Number => 6;
    public override string Title => "10.000 kayıttan birindeki bozuk veri";
    public override string Skill => "Conditional breakpoint ve tracepoint (breakpoint'te durmadan log)";
    public override string Hint => "Döngüye normal breakpoint koymak 10.000 kez durdurur. Koşullu breakpoint: 'order.Quantity < 0'. Ya da tracepoint: durmadan '{order.Id} {order.Quantity}' yazdırın.";

    private sealed record Order(int Id, decimal UnitPrice, int Quantity);

    public override void Run()
    {
        var orders = LoadOrders();

        // Doğru formül: miktar her zaman (Id % 5) + 1'dir.
        var expected = orders.Sum(o => o.UnitPrice * ((o.Id % 5) + 1));
        var actual = 0m;
        foreach (var order in orders)
            actual += order.UnitPrice * order.Quantity;      // ← toplam neden tutmuyor? Hangi kayıt?

        Report("fatura toplamı", expected: expected, actual: actual);
        Console.WriteLine("  Soru: toplamı bozan kaydın Id'si nedir? (Debugger kullanmadan 10.000 satır log okumak istemezsiniz.)");
    }

    private static List<Order> LoadOrders()
    {
        var list = new List<Order>(10_000);
        for (var id = 1; id <= 10_000; id++)
        {
            var quantity = id == 7345 ? -3 : (id % 5) + 1;    // veri kaynağında bir aksaklık: eksi miktar (kodu okuyarak değil, debugger ile bulun)
            list.Add(new Order(id, UnitPrice: 10m + (id % 7), quantity));
        }
        return list;
    }
}
