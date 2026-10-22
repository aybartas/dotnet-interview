using Junior.CollectionsLinq.DelegatesLambdasEvents.Models;

namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class Section04_Events
{
    public static void Run()
    {
        Console.WriteLine("A) BAD: a bare public delegate field (BadOrderService.OnCreated)");
        var bad = new BadOrderService();
        bad.OnCreated += o => Console.WriteLine($"   [email service] order {o.Id} → email the customer");
        bad.OnCreated += o => Console.WriteLine($"   [stock service] order {o.Id} → decrease stock");
        bad.OnCreated = o => Console.WriteLine("   [malicious/buggy code] OVERWROTE all subscribers");   // assignment: REMOVES the previous ones
        Console.WriteLine("   Calling Create — the email and stock services are EXPECTED to run, but:");
        bad.Create("Alice", 100m);
        Console.WriteLine("   Raising a fake event from the outside:");
        bad.OnCreated?.Invoke(new Order(999, "FAKE", 1m));                                         // raising a fake event from the outside
        Console.WriteLine("   → The subscribers were silently removed and an event could be raised from the outside. The publisher doesn't even own its own event.\n");

        Console.WriteLine("B) CORRECT: the `event` keyword + EventHandler<TEventArgs> (OrderService.OrderCreated)");
        var service = new OrderService();
        service.OrderCreated += (sender, e) => Console.WriteLine($"   [email service] order {e.Order.Id} ({e.Order.Customer}) → email");
        service.OrderCreated += OnStock;
        // service.OrderCreated = null;                              // ✗ CS0070: no assignment to an event from the outside
        // service.OrderCreated(service, null!);                     // ✗ CS0070: no raising from the outside
        Console.WriteLine($"   subscriber count: {service.SubscriberCount}");
        service.Create("Alice", 250m);

        service.OrderCreated -= OnStock;                            // unsubscribe
        Console.WriteLine($"   OnStock subscription removed → subscriber count: {service.SubscriberCount}");
        service.Create("Bob", 80m);

        Console.WriteLine("""

        What does `event` provide?  ENCAPSULATION over a delegate: only += / -= (subscribe / unsubscribe) is exposed; raising and clearing stay with the publisher.
        This is the "publisher–subscriber" (publish/subscribe, Observer) pattern: the publisher doesn't know who the subscribers are → loose coupling (→ J2.3).

        Standard pattern:
          public event EventHandler<OrderCreatedEventArgs>? OrderCreated;     // EventHandler<TEventArgs>: (object? sender, TEventArgs e)
          protected virtual void OnOrderCreated(OrderCreatedEventArgs e) => OrderCreated?.Invoke(this, e);
          A class derived from EventArgs carries the event's DATA (sender = the publisher).
        Watch out:
          • If one of the subscribers throws while the event is being raised, the remaining subscribers DO NOT RUN (the chain is broken). For resilience, invoke them one by one via GetInvocationList() and catch.
          • Subscribers run on the publisher's thread, SYNCHRONOUSLY (a slow subscriber blocks the publisher).
          • `?.Invoke` makes the null check + the call atomic (no NullReference even if a subscriber unsubscribes right at the moment of the call).
          • For async workflows with `event` you need async event patterns/message queues (→ J4, M12); cross-process events = messaging (RabbitMQ/Kafka).
        """);
    }

    private static void OnStock(object? sender, OrderCreatedEventArgs e) =>
        Console.WriteLine($"   [stock service] order {e.Order.Id} → decrease stock");
}
