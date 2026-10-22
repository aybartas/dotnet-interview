using System.Collections.Immutable;
using Junior.CSharpFundamentals.ModernCSharp.Csharp14Style;

namespace Junior.CSharpFundamentals.ModernCSharp.Comparison;

public sealed class Facade14 : IOrderFacade
{
    public string StyleName => "C# 14 style";

    private static Order Build(string tier, string name, string email, string? city, IReadOnlyList<(string Product, int Qty, decimal Price)> lines, OrderStatus status = OrderStatus.Pending)
        => new("ORD-1",
               new Customer(name, Enum.Parse<CustomerTier>(tier)) { Email = email, Address = city is null ? null : new(city) },
               [.. lines.Select(l => new OrderLine(l.Product, l.Qty, l.Price))],
               status);

    public decimal DiscountRate(string tier, decimal subtotal) => Build(tier, "x", "x", null, [("x", 1, subtotal)]).DiscountRate;

    public decimal Total(string tier, IReadOnlyList<(string Product, int Qty, decimal Price)> lines) => Build(tier, "x", "x", null, lines).Total;

    public string Receipt(string tier, string name, string rawEmail, string? city, IReadOnlyList<(string Product, int Qty, decimal Price)> lines)
        => Build(tier, name, rawEmail, city, lines).Receipt().ReplaceLineEndings("\n");

    public string Advance(string status, string action)
        => Build("Standard", "x", "x", null, [("x", 1, 1m)], Enum.Parse<OrderStatus>(status)).Advance(action).Status.ToString();

    public string ShippingLabel(string? city) => Build("Standard", "x", "x", city, [("x", 1, 1m)]).ShippingLabel();

    public string NormalizeEmail(string raw) => new Customer("x", CustomerTier.Standard) { Email = raw }.Email;

    public bool IdenticalOrdersAreEqual()
    {
        (string, int, decimal)[] lines = [("Pen", 2, 12.5m)];
        return Build("Gold", "Alice", "a@x.com", "London", lines, OrderStatus.Paid)
            .Equals(Build("Gold", "Alice", "a@x.com", "London", lines, OrderStatus.Paid));
    }

    public string RenameIfPresent(bool customerExists, string newName)
    {
        Customer? customer = customerExists ? new("old", CustomerTier.Standard) : null;
        customer?.Name = newName;                                   // C# 14: null-conditional ASSIGNMENT (if customer is null the assignment is never performed)
        return customer?.Name ?? "(no customer)";
    }
}
