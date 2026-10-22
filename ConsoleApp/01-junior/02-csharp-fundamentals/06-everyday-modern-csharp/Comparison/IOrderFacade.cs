namespace Junior.CSharpFundamentals.ModernCSharp.Comparison;

/// <summary>
/// A neutral contract proving that the two styles (C# 7 and C# 14) produce the SAME behaviour: inputs/outputs are primitive types only,
/// so two different domain models are tested with the same scenarios (Section10).
/// </summary>
public interface IOrderFacade
{
    string StyleName { get; }

    decimal DiscountRate(string tier, decimal subtotal);
    decimal Total(string tier, IReadOnlyList<(string Product, int Qty, decimal Price)> lines);
    string Receipt(string tier, string name, string rawEmail, string? city, IReadOnlyList<(string Product, int Qty, decimal Price)> lines);
    string Advance(string status, string action);
    string ShippingLabel(string? city);
    string NormalizeEmail(string raw);
    bool IdenticalOrdersAreEqual();
    string RenameIfPresent(bool customerExists, string newName);
}
