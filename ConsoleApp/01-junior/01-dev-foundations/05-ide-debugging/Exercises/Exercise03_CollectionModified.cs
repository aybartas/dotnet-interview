namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise03_CollectionModified : Exercise
{
    public override int Number => 3;
    public override string Title => "Modifying a collection during foreach";
    public override string Skill => "Exception Settings + reading the stack trace (the bug's source and its symptom are in different places)";
    public override string Hint => "Is the exception thrown in the foreach or in Remove? Look at the Call Stack: what does the enumerator's MoveNext check? (List<T>._version)";

    private sealed record Order(int Id, bool IsCancelled);

    public override void Run()
    {
        var orders = new List<Order> { new(1, false), new(2, true), new(3, true), new(4, false) };

        foreach (var order in orders)
        {
            if (order.IsCancelled)
                orders.Remove(order);
        }

        Report("remaining order count", expected: 2, actual: orders.Count);
    }
}
