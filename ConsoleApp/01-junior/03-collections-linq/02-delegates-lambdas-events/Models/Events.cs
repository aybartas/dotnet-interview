namespace Junior.CollectionsLinq.DelegatesLambdasEvents.Models;

public sealed record Order(int Id, string Customer, decimal Amount, int ItemCount = 1, bool FirstOrder = false);

public sealed class OrderCreatedEventArgs(Order order) : EventArgs
{
    public Order Order { get; } = order;
}

/// <summary>
/// CORRECT: `event` + EventHandler&lt;TEventArgs&gt;.
///   • From the outside only += and -= are possible (subscribe / unsubscribe).
///   • ONLY the publisher class can raise the event (Raise).
///   • Clearing ALL subscribers with `= null` or calling `Invoke` from the outside DOES NOT COMPILE.
/// </summary>
public class OrderService
{
    private int _nextId = 1;

    public event EventHandler<OrderCreatedEventArgs>? OrderCreated;

    public Order Create(string customer, decimal amount)
    {
        var order = new Order(_nextId++, customer, amount);
        OnOrderCreated(new OrderCreatedEventArgs(order));
        return order;
    }

    // Standard pattern: protected virtual raiser + `?.Invoke` (null when there are no subscribers → `?.` does a thread-safe null check via a local copy)
    protected virtual void OnOrderCreated(OrderCreatedEventArgs e) => OrderCreated?.Invoke(this, e);

    public int SubscriberCount => OrderCreated?.GetInvocationList().Length ?? 0;
}

/// <summary>
/// BAD: a bare public delegate field. Anyone on the outside can:
///   • `service.OnCreated = null;`  → SILENTLY remove the other subscribers
///   • `service.OnCreated?.Invoke(...)` → raise a fake event in place of the publisher
///   • `service.OnCreated = single;`   → overwrite the existing subscribers
/// </summary>
public sealed class BadOrderService
{
    public Action<Order>? OnCreated;

    public Order Create(string customer, decimal amount)
    {
        var order = new Order(1, customer, amount);
        OnCreated?.Invoke(order);
        return order;
    }
}

/// <summary>Long-lived publisher (e.g. an application-wide singleton / static event): keeps its subscribers alive for as long as it lives.</summary>
public sealed class LongLivedPublisher
{
    public event EventHandler? Tick;
    public void RaiseTick() => Tick?.Invoke(this, EventArgs.Empty);
    public int SubscriberCount => Tick?.GetInvocationList().Length ?? 0;
}

/// <summary>A subscriber EXPECTED to be short-lived. Once it subscribes the publisher holds a reference to it → if it never unsubscribes, the GC cannot collect it.</summary>
public sealed class ShortLivedSubscriber : IDisposable
{
    private readonly LongLivedPublisher _publisher;
    public int TickCount { get; private set; }

    public ShortLivedSubscriber(LongLivedPublisher publisher)
    {
        _publisher = publisher;
        _publisher.Tick += OnTick;                    // publisher → subscriber reference (the event delegate's Target = this)
    }

    private void OnTick(object? sender, EventArgs e) => TickCount++;

    public void Dispose() => _publisher.Tick -= OnTick;   // CORRECT: unsubscribe
}
