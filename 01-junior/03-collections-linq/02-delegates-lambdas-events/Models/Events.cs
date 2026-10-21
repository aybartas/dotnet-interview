namespace Junior.CollectionsLinq.DelegatesLambdasEvents.Models;

public sealed record Order(int Id, string Customer, decimal Amount, int ItemCount = 1, bool FirstOrder = false);

public sealed class OrderCreatedEventArgs(Order order) : EventArgs
{
    public Order Order { get; } = order;
}

/// <summary>
/// DOĞRU: `event` + EventHandler&lt;TEventArgs&gt;.
///   • Dışarıdan yalnızca += ve -= yapılabilir (abone ol / ayrıl).
///   • Olayı YALNIZCA yayıncı sınıf tetikleyebilir (Raise).
///   • Dışarıdan `= null` ile TÜM aboneleri silmek ya da `Invoke` etmek DERLENMEZ.
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

    // Standart desen: korumalı sanal tetikleyici + `?.Invoke` (abone yoksa null → thread-safe null kontrolü için yerel kopya)
    protected virtual void OnOrderCreated(OrderCreatedEventArgs e) => OrderCreated?.Invoke(this, e);

    public int SubscriberCount => OrderCreated?.GetInvocationList().Length ?? 0;
}

/// <summary>
/// KÖTÜ: çıplak public delegate alanı. Dışarıdaki herkes:
///   • `service.OnCreated = null;`  → diğer aboneleri SESSİZCE siler
///   • `service.OnCreated?.Invoke(...)` → yayıncı yerine sahte olay üretir
///   • `service.OnCreated = tek;`   → mevcut abonelerin üzerine yazar
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

/// <summary>Uzun ömürlü yayıncı (ör. uygulama genelinde tek örnek / static olay): aboneleri yaşadığı sürece canlı tutar.</summary>
public sealed class LongLivedPublisher
{
    public event EventHandler? Tick;
    public void RaiseTick() => Tick?.Invoke(this, EventArgs.Empty);
    public int SubscriberCount => Tick?.GetInvocationList().Length ?? 0;
}

/// <summary>Kısa ömürlü olması BEKLENEN abone. Abone olunca yayıncı ona referans tutar → abonelikten çıkmazsa GC toplayamaz.</summary>
public sealed class ShortLivedSubscriber : IDisposable
{
    private readonly LongLivedPublisher _publisher;
    public int TickCount { get; private set; }

    public ShortLivedSubscriber(LongLivedPublisher publisher)
    {
        _publisher = publisher;
        _publisher.Tick += OnTick;                    // yayıncı → abone referansı (event delegate'in Target'ı = this)
    }

    private void OnTick(object? sender, EventArgs e) => TickCount++;

    public void Dispose() => _publisher.Tick -= OnTick;   // DOĞRU: abonelikten çık
}
