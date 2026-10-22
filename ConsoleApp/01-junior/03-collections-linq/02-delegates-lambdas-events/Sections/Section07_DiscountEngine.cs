using Junior.CollectionsLinq.DelegatesLambdasEvents.Models;

namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class Section07_DiscountEngine
{
    public static void Run()
    {
        // The rules are passed as LAMBDAS — the engine doesn't know the rule types
        var engine = new DiscountEngine()
            .AddRule("10% general",         Rules.Percentage(10))
            .AddRule("5+ items 15%",        Rules.BulkOver(5, 15))
            .AddRule("first order $50",     Rules.FirstOrderFlat(50))
            .AddRule("VIP (inline lambda)", o => o.Customer == "Alice" ? 30m : 0m)                 // rule written in place
            .AddRule("large order (cap 200)", Rules.Capped(Rules.When(o => o.Amount > 1000, Rules.Percentage(20)), 200));

        var orders = new[]
        {
            new Order(1, "Bob",   300m,  ItemCount: 2),
            new Order(2, "Bob",   400m,  ItemCount: 8,  FirstOrder: true),
            new Order(3, "Alice", 2000m, ItemCount: 3),
        };

        foreach (var o in orders)
        {
            var best = engine.Calculate(o);                                  // default strategy: the single best discount
            var sum = engine.Calculate(o, Combiners.Sum);                    // another strategy: the total
            Console.WriteLine(FormattableString.Invariant($"   Order {o.Id} ({o.Customer}, ${o.Amount:N0}, {o.ItemCount} items{(o.FirstOrder ? ", first order" : "")}):"));
            Console.WriteLine(FormattableString.Invariant($"      applicable rules: {(best.Applied.Count == 0 ? "none" : string.Join(", ", best.Applied.Select(a => $"{a.Rule}={a.Amount:N0}")))}"));
            Console.WriteLine(FormattableString.Invariant($"      best single discount = ${best.Total:N0} · sum strategy = ${sum.Total:N0}"));
        }

        Console.WriteLine("\nCustom combination strategy (lambda): 'sum of the best two discounts':");
        var topTwo = engine.Calculate(orders[1], ds => ds.OrderByDescending(d => d).Take(2).Sum());
        Console.WriteLine(FormattableString.Invariant($"   Order 2 → ${topTwo.Total:N0}"));

        Console.WriteLine("""

        Why a strategy with delegates/lambdas?
          • Behavior becomes a VALUE: passed as a parameter, kept in a list, composed (When, Capped = higher-order functions).
          • No class hierarchy/interface ceremony: `Func<Order, decimal>` is enough for single-method behavior.
          • A new rule = a new lambda; DiscountEngine does NOT CHANGE (Open/Closed).
        When to use an interface/class?  If the strategy carries state, has multiple methods, will be named and registered with DI, or is complex enough to be tested on its own.
        Watch the closures: Rules.Percentage(10) → the lambda CAPTURES the 'percent' parameter (closure); producing lots of rules on a hot path means allocations.
        """);
    }
}
