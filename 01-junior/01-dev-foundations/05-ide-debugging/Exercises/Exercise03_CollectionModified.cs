namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise03_CollectionModified : Exercise
{
    public override int Number => 3;
    public override string Title => "foreach sırasında koleksiyonu değiştirmek";
    public override string Skill => "Exception Settings + stack trace okuma (hatanın kaynağı ile belirtisi farklı yerde)";
    public override string Hint => "İstisna foreach'te mi yoksa Remove'da mı fırlıyor? Call Stack'e bakın: enumerator'ın MoveNext'i neyi kontrol ediyor? (List<T>._version)";

    private sealed record Order(int Id, bool IsCancelled);

    public override void Run()
    {
        var orders = new List<Order> { new(1, false), new(2, true), new(3, true), new(4, false) };

        foreach (var order in orders)
        {
            if (order.IsCancelled)
                orders.Remove(order);
        }

        Report("kalan sipariş sayısı", expected: 2, actual: orders.Count);
    }
}
