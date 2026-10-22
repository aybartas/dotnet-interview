namespace Junior.CollectionsLinq.DelegatesLambdasEvents.Models;

public sealed record DiscountResult(decimal Total, IReadOnlyList<(string Rule, decimal Amount)> Applied);

/// <summary>
/// The STRATEGY pattern with delegates/lambdas: the rules and the way they are combined are passed as VALUES (Func), not as a CLASS HIERARCHY.
/// A new rule = a new lambda; the engine does not change (Open/Closed). Compare with the IFeePolicy/IPaymentRule classes in J2.3 — same idea, less ceremony.
/// </summary>
public sealed class DiscountEngine
{
    private readonly List<(string Name, Func<Order, decimal> Rule)> _rules = [];

    public DiscountEngine AddRule(string name, Func<Order, decimal> rule)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(rule);
        _rules.Add((name, rule));
        return this;                                                // fluent usage
    }

    public int RuleCount => _rules.Count;

    /// <param name="order">The order to calculate the discount for.</param>
    /// <param name="combine">How we combine the discounts of multiple rules is also a strategy (Func).</param>
    public DiscountResult Calculate(Order order, Func<IEnumerable<decimal>, decimal>? combine = null)
    {
        combine ??= Combiners.Best;
        var applied = _rules
            .Select(r => (r.Name, Amount: Math.Max(0m, r.Rule(order))))
            .Where(x => x.Amount > 0)
            .ToList();
        var total = Math.Min(combine(applied.Select(a => a.Amount)), order.Amount);   // the discount cannot exceed the order amount
        return new DiscountResult(total, applied);
    }
}

/// <summary>Ready-made rule factories: each one RETURNS a lambda (which captures the parameter via a closure).</summary>
public static class Rules
{
    public static Func<Order, decimal> Percentage(decimal percent) => o => o.Amount * percent / 100m;

    public static Func<Order, decimal> BulkOver(int itemCount, decimal percent) => o => o.ItemCount > itemCount ? o.Amount * percent / 100m : 0m;

    public static Func<Order, decimal> FirstOrderFlat(decimal amount) => o => o.FirstOrder ? amount : 0m;

    /// <summary>Rule combinator (higher-order function): applies the given rule if the condition holds.</summary>
    public static Func<Order, decimal> When(Func<Order, bool> condition, Func<Order, decimal> rule) => o => condition(o) ? rule(o) : 0m;

    public static Func<Order, decimal> Capped(Func<Order, decimal> rule, decimal max) => o => Math.Min(rule(o), max);
}

public static class Combiners
{
    /// <summary>The single best discount.</summary>
    public static readonly Func<IEnumerable<decimal>, decimal> Best = ds => ds.DefaultIfEmpty(0m).Max();

    /// <summary>The sum of all of them.</summary>
    public static readonly Func<IEnumerable<decimal>, decimal> Sum = ds => ds.Sum();
}
