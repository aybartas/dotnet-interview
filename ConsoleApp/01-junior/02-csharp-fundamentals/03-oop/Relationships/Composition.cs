namespace Junior.CSharpFundamentals.Oop.Relationships;

// ════════════════ COMPOSITION (strong "has-a") ════════════════
// An Order OWNS its lines. An OrderLine cannot exist without an Order:
//   • OrderLine's constructor is internal → nobody outside can create a line on its own; it is born only via Order.AddLine.
//   • A line belongs to a single order and cannot be moved to another order.
//   • When the order is cancelled/destroyed its lines are gone too (lifetime is bound to the whole).
// In DDD this boundary shows up as "aggregate root + the parts inside it" (→ S5.1).

public sealed class OrderLine
{
    internal OrderLine(Order owner, string product, int quantity, decimal unitPrice)
    {
        Owner = owner;
        Product = product;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public Order Owner { get; }
    public string Product { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }
    public decimal Total => Quantity * UnitPrice;

    /// <summary>When the order is cancelled the line is left "orphaned": it no longer has any meaning/owner.</summary>
    public bool IsOrphaned { get; internal set; }
}

public sealed class Order
{
    private readonly List<OrderLine> _lines = [];

    public IReadOnlyList<OrderLine> Lines => _lines;
    public decimal Total => _lines.Sum(l => l.Total);
    public bool IsCancelled { get; private set; }

    public OrderLine AddLine(string product, int quantity, decimal unitPrice)
    {
        if (IsCancelled) throw new InvalidOperationException("Cannot add a line to a cancelled order.");
        var line = new OrderLine(this, product, quantity, unitPrice);   // the ORDER creates the line
        _lines.Add(line);
        return line;
    }

    public void Cancel()
    {
        foreach (var l in _lines) l.IsOrphaned = true;                   // the parts "die" together with the whole
        _lines.Clear();
        IsCancelled = true;
    }
}
