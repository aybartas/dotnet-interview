using System.Runtime.CompilerServices;
using Junior.CollectionsLinq.DelegatesLambdasEvents.Models;

namespace Junior.CollectionsLinq.DelegatesLambdasEvents.Tests;

public class EventTests
{
    [Fact]
    public void Subscribers_are_notified_with_the_publisher_and_event_data()
    {
        var service = new OrderService();
        object? seenSender = null;
        Order? seenOrder = null;
        service.OrderCreated += (s, e) => { seenSender = s; seenOrder = e.Order; };

        var order = service.Create("Ayşe", 100m);

        Assert.Same(service, seenSender);
        Assert.Same(order, seenOrder);
    }

    [Fact]
    public void Unsubscribed_handlers_are_not_called_and_no_subscribers_is_safe()
    {
        var service = new OrderService();
        var calls = 0;
        EventHandler<OrderCreatedEventArgs> handler = (_, _) => calls++;
        service.Create("a", 1m);                                       // abone yok → NullReference yok

        service.OrderCreated += handler;
        service.Create("b", 1m);
        service.OrderCreated -= handler;
        service.Create("c", 1m);

        Assert.Equal(1, calls);
        Assert.Equal(0, service.SubscriberCount);
    }

    [Fact]
    public void Naked_delegate_field_can_be_wiped_or_invoked_by_outsiders()
    {
        var bad = new BadOrderService();
        var emailSent = false;
        bad.OnCreated += _ => emailSent = true;

        bad.OnCreated = _ => { };                                      // başka kod TÜM aboneleri ezdi
        bad.Create("x", 1m);
        Assert.False(emailSent);

        var fakeSeen = (Order?)null;
        bad.OnCreated = o => fakeSeen = o;
        bad.OnCreated?.Invoke(new Order(999, "SAHTE", 1m));            // dışarıdan sahte olay
        Assert.Equal(999, fakeSeen!.Id);
    }

    [Fact]
    public void Event_exposes_only_add_and_remove_not_assignment_or_invocation()
    {
        var eventInfo = typeof(OrderService).GetEvent(nameof(OrderService.OrderCreated))!;
        Assert.NotNull(eventInfo.AddMethod);
        Assert.NotNull(eventInfo.RemoveMethod);
        Assert.Null(eventInfo.RaiseMethod);                            // C#'ta dışarıya Raise açılmaz
    }

    [Fact]
    public void An_exception_in_one_subscriber_stops_the_remaining_subscribers()
    {
        var service = new OrderService();
        var secondRan = false;
        service.OrderCreated += (_, _) => throw new InvalidOperationException("abone patladı");
        service.OrderCreated += (_, _) => secondRan = true;

        Assert.Throws<InvalidOperationException>(() => service.Create("x", 1m));
        Assert.False(secondRan);                                       // zincir KESİLDİ
    }

    [Fact]
    public void Invoking_each_handler_separately_isolates_failures()
    {
        EventHandler<OrderCreatedEventArgs>? ev = null;
        var ran = new List<string>();
        ev += (_, _) => throw new InvalidOperationException();
        ev += (_, _) => ran.Add("ikinci");

        var errors = 0;
        foreach (var handler in ev.GetInvocationList().Cast<EventHandler<OrderCreatedEventArgs>>())
        {
            try { handler(this, new OrderCreatedEventArgs(new Order(1, "x", 1m))); }
            catch (InvalidOperationException) { errors++; }
        }

        Assert.Equal(1, errors);
        Assert.Equal(["ikinci"], ran);
    }

    // ───────── Abonelik sızıntısı ─────────

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static (WeakReference Leaked, WeakReference Disposed) Subscribe(LongLivedPublisher publisher)
    {
        var leaked = new ShortLivedSubscriber(publisher);
        var disposed = new ShortLivedSubscriber(publisher);
        disposed.Dispose();
        return (new WeakReference(leaked), new WeakReference(disposed));
    }

    private static void ForceGc()
    {
        for (var i = 0; i < 3; i++) { GC.Collect(); GC.WaitForPendingFinalizers(); }
    }

    [Fact]
    public void A_subscriber_that_never_unsubscribes_is_kept_alive_by_a_long_lived_publisher()
    {
        var publisher = new LongLivedPublisher();
        var (leaked, disposed) = Subscribe(publisher);

        ForceGc();

        Assert.True(leaked.IsAlive, "abonelikten çıkmayan abone yayıncı tarafından canlı tutulmalı (sızıntı)");
        Assert.False(disposed.IsAlive, "Dispose ile ayrılan abone toplanabilmeli");
        Assert.Equal(1, publisher.SubscriberCount);
        GC.KeepAlive(publisher);
    }

    [Fact]
    public void Leaked_subscriber_keeps_reacting_to_events()
    {
        var publisher = new LongLivedPublisher();
        var subscriber = new ShortLivedSubscriber(publisher);
        publisher.RaiseTick(); publisher.RaiseTick();
        Assert.Equal(2, subscriber.TickCount);

        subscriber.Dispose();
        publisher.RaiseTick();
        Assert.Equal(2, subscriber.TickCount);                         // ayrıldıktan sonra tepki vermez
    }
}

public class DiscountEngineTests
{
    private static DiscountEngine Engine() => new DiscountEngine()
        .AddRule("%10", Rules.Percentage(10))
        .AddRule("5+ ürün %15", Rules.BulkOver(5, 15))
        .AddRule("ilk sipariş 50", Rules.FirstOrderFlat(50))
        .AddRule("VIP", o => o.Customer == "Ayşe" ? 30m : 0m)
        .AddRule("büyük sipariş (max 200)", Rules.Capped(Rules.When(o => o.Amount > 1000, Rules.Percentage(20)), 200));

    [Fact]
    public void Best_strategy_picks_the_largest_single_discount()
    {
        var r = Engine().Calculate(new Order(1, "Ali", 400m, ItemCount: 8, FirstOrder: true));
        Assert.Equal(60m, r.Total);                                    // 400×%15
        Assert.Equal(3, r.Applied.Count);
    }

    [Fact]
    public void Sum_strategy_adds_all_applicable_discounts()
    {
        var r = Engine().Calculate(new Order(1, "Ali", 400m, ItemCount: 8, FirstOrder: true), Combiners.Sum);
        Assert.Equal(40m + 60m + 50m, r.Total);
    }

    [Fact]
    public void Custom_lambda_can_be_the_combination_strategy()
    {
        var r = Engine().Calculate(new Order(1, "Ali", 400m, ItemCount: 8, FirstOrder: true),
            ds => ds.OrderByDescending(d => d).Take(2).Sum());
        Assert.Equal(110m, r.Total);
    }

    [Fact]
    public void Inline_lambda_rules_and_capped_rules_work()
    {
        var r = Engine().Calculate(new Order(3, "Ayşe", 2000m, ItemCount: 3));
        Assert.Contains(r.Applied, a => a.Rule == "VIP" && a.Amount == 30m);
        Assert.Contains(r.Applied, a => a.Rule.StartsWith("büyük") && a.Amount == 200m);     // %20 = 400 → üst sınır 200
    }

    [Fact]
    public void Discount_never_exceeds_the_order_amount()
    {
        var engine = new DiscountEngine().AddRule("dev", _ => 1_000_000m);
        Assert.Equal(100m, engine.Calculate(new Order(1, "x", 100m)).Total);
    }

    [Fact]
    public void No_applicable_rule_gives_zero()
    {
        var engine = new DiscountEngine().AddRule("hiç", _ => 0m);
        var r = engine.Calculate(new Order(1, "x", 100m));
        Assert.Equal(0m, r.Total);
        Assert.Empty(r.Applied);
    }

    [Fact]
    public void Rules_validate_their_arguments()
    {
        var engine = new DiscountEngine();
        Assert.Throws<ArgumentNullException>(() => engine.AddRule("x", null!));
        Assert.Throws<ArgumentException>(() => engine.AddRule("", _ => 1m));
    }

    [Fact]
    public void Each_rule_factory_call_captures_its_own_parameter()
    {
        var five = Rules.Percentage(5);
        var twenty = Rules.Percentage(20);
        var order = new Order(1, "x", 100m);
        Assert.Equal((5m, 20m), (five(order), twenty(order)));
    }
}
