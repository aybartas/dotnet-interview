using System.Collections.Immutable;
using System.Text;

// ═══════════════════════════════════════════════════════════════════════════════════════
// The SAME order model in "C# 14 STYLE": file-scoped namespace, implicit/global using, record + primary constructor,
// automatic value equality/ToString/with, switch expression + relational/logical pattern, `field` keyword, null-conditional,
// raw string literal, collection expression. Comparison: ../Csharp7Style/Domain.cs
// ═══════════════════════════════════════════════════════════════════════════════════════
namespace Junior.CSharpFundamentals.ModernCSharp.Csharp14Style;

public enum CustomerTier { Standard, Gold, Platinum }

public enum OrderStatus { Pending, Paid, Shipped, Delivered, Cancelled }

public sealed record Address(string City, string? Country = null);

public sealed class Customer(string name, CustomerTier tier)
{
    public string Name { get; set; } = name ?? throw new ArgumentNullException(nameof(name));

    // C# 14 `field` keyword: normalize + validate in the setter without writing the backing field by hand
    public string Email
    {
        get;
        set => field = value?.Trim().ToLowerInvariant() ?? throw new ArgumentNullException(nameof(value));
    } = "";

    public CustomerTier Tier { get; } = tier;

    public Address? Address { get; set; }
}

public sealed record OrderLine(string Product, int Quantity, decimal UnitPrice)
{
    public decimal Total => Quantity * UnitPrice;
}

public sealed record Order(string Id, Customer Customer, ImmutableArray<OrderLine> Lines, OrderStatus Status = OrderStatus.Pending)
{
    public decimal Subtotal => Lines.Sum(l => l.Total);

    // relational (> 1000) + constant + logical pattern; evaluating two inputs together with a tuple pattern
    public decimal DiscountRate => (Customer.Tier, Subtotal) switch
    {
        (CustomerTier.Platinum, > 1000m) => 0.20m,
        (CustomerTier.Platinum, _) => 0.15m,
        (CustomerTier.Gold, > 1000m) => 0.15m,
        (CustomerTier.Gold, _) => 0.10m,
        (CustomerTier.Standard, > 1000m) => 0.05m,
        _ => 0m,
    };

    public decimal Total => Subtotal * (1 - DiscountRate);

    public Order Advance(string action) => (Status, action) switch
    {
        (OrderStatus.Pending, "pay") => this with { Status = OrderStatus.Paid },
        (OrderStatus.Paid, "ship") => this with { Status = OrderStatus.Shipped },
        (OrderStatus.Shipped, "deliver") => this with { Status = OrderStatus.Delivered },
        (OrderStatus.Pending or OrderStatus.Paid, "cancel") => this with { Status = OrderStatus.Cancelled },
        _ => throw new InvalidOperationException($"Action '{action}' is invalid for an order in '{Status}' state"),
    };

    public string ShippingLabel() => Customer.Address?.City ?? "no address";

    // The record's generated Equals compares collections by REFERENCE; if we want content equality we write it by hand.
    public bool Equals(Order? other) => other is not null && Id == other.Id && Status == other.Status && Lines.SequenceEqual(other.Lines);
    public override int GetHashCode() => HashCode.Combine(Id, Status, Lines.Length);

    public string Receipt()
    {
        var lines = string.Join('\n', Lines.Select(l => FormattableString.Invariant($"  {l.Quantity} × {l.Product} = {l.Total:F2}")));
        return FormattableString.Invariant($$"""
            ORDER {{Id}} — {{Customer.Name}} <{{Customer.Email}}>
            {{Customer.Tier}} customer · {{ShippingLabel()}}
            {{lines}}
            Subtotal: {{Subtotal:F2}} · Discount {{DiscountRate * 100:0}}% → {{Total:F2}}
            """);
    }
}
