namespace Junior.CSharpFundamentals.Oop.Relationships;

// ════════════════ COMPOSITION (güçlü "has-a") ════════════════
// Order, satırlarının SAHİBİDİR. OrderLine, Order olmadan var olamaz:
//   • OrderLine'ın constructor'ı internal → dışarıda kimse tek başına satır üretemez; yalnızca Order.AddLine ile doğar.
//   • Satır tek bir siparişe aittir ve başka siparişe taşınamaz.
//   • Sipariş iptal edilince/yok olunca satırlar da yok olur (yaşam süresi bütüne bağlı).
// DDD'de bu sınır "aggregate root + içindeki parçalar" olarak karşımıza çıkar (→ S5.1).

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

    /// <summary>Sipariş iptal edilince satır "yetim" kalır: artık hiçbir anlamı/sahibi yoktur.</summary>
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
        if (IsCancelled) throw new InvalidOperationException("İptal edilmiş siparişe satır eklenemez.");
        var line = new OrderLine(this, product, quantity, unitPrice);   // satırı SİPARİŞ yaratır
        _lines.Add(line);
        return line;
    }

    public void Cancel()
    {
        foreach (var l in _lines) l.IsOrphaned = true;                   // parçalar bütünle birlikte "ölür"
        _lines.Clear();
        IsCancelled = true;
    }
}
