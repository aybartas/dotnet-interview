namespace Junior.CSharpFundamentals.ModernCSharp.Comparison;

/// <summary>
/// İki stilin (C# 7 ve C# 14) AYNI davranışı verdiğini kanıtlayan nötr sözleşme: girdi/çıktı yalnızca ilkel tiplerdir,
/// böylece iki farklı domain modeli aynı testlerle sınanır.
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
