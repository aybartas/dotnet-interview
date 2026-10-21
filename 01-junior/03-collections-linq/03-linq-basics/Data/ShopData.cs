namespace Junior.CollectionsLinq.LinqBasics.Data;

public enum Tier { Standard, Gold, Platinum }
public enum OrderStatus { Pending, Delivered, Cancelled }

public sealed record Customer(int Id, string Name, string City, Tier Tier);
public sealed record Product(int Id, string Name, string Category, decimal Price);
public sealed record OrderItem(int OrderId, int ProductId, int Quantity, decimal UnitPrice)
{
    public decimal Total => Quantity * UnitPrice;
}
public sealed record Order(int Id, int CustomerId, DateOnly Date, OrderStatus Status);

/// <summary>
/// Küçük, ELLE doğrulanabilir e-ticaret veri seti (6 müşteri, 10 ürün, 12 sipariş).
/// Teslim edilen siparişlerin toplam cirosu = 5030 TL (9 sipariş) — raporların beklenen sonuçları buna göre elle doğrulanmıştır (Section08).
/// </summary>
public sealed class ShopData
{
    public IReadOnlyList<Customer> Customers { get; } =
    [
        new(1, "Ayşe Yılmaz", "Ankara", Tier.Gold),
        new(2, "Ali Demir", "İstanbul", Tier.Standard),
        new(3, "Zeynep Kaya", "İzmir", Tier.Platinum),
        new(4, "Mehmet Çelik", "Ankara", Tier.Standard),
        new(5, "Elif Şahin", "İstanbul", Tier.Gold),
        new(6, "Can Öztürk", "İzmir", Tier.Standard),          // hiç siparişi YOK
    ];

    public IReadOnlyList<Product> Products { get; } =
    [
        new(1, "Kalem", "Kırtasiye", 12.50m),
        new(2, "Defter", "Kırtasiye", 40m),
        new(3, "Çanta", "Aksesuar", 980m),
        new(4, "Kulaklık", "Elektronik", 450m),
        new(5, "Mouse", "Elektronik", 220m),
        new(6, "Klavye", "Elektronik", 350m),
        new(7, "Lamba", "Ev", 150m),
        new(8, "Kupa", "Ev", 60m),
        new(9, "Bardak", "Ev", 25m),
        new(10, "Ajanda", "Kırtasiye", 90m),                    // hiç SATILMADI
    ];

    public IReadOnlyList<Order> Orders { get; } =
    [
        new(101, 1, new(2026, 1, 5),  OrderStatus.Delivered),
        new(102, 2, new(2026, 1, 12), OrderStatus.Delivered),
        new(103, 3, new(2026, 1, 20), OrderStatus.Delivered),
        new(104, 1, new(2026, 2, 2),  OrderStatus.Delivered),
        new(105, 4, new(2026, 2, 9),  OrderStatus.Cancelled),
        new(106, 5, new(2026, 2, 14), OrderStatus.Delivered),
        new(107, 3, new(2026, 2, 20), OrderStatus.Delivered),
        new(108, 2, new(2026, 3, 1),  OrderStatus.Pending),
        new(109, 1, new(2026, 3, 4),  OrderStatus.Delivered),
        new(110, 5, new(2026, 3, 10), OrderStatus.Delivered),
        new(111, 4, new(2026, 3, 15), OrderStatus.Delivered),
        new(112, 3, new(2026, 3, 22), OrderStatus.Cancelled),
    ];

    public IReadOnlyList<OrderItem> Items { get; } =
    [
        new(101, 1, 4, 12.50m), new(101, 2, 2, 40m),                        // 130
        new(102, 4, 1, 450m),   new(102, 5, 1, 220m),                       // 670
        new(103, 3, 1, 980m),   new(103, 6, 1, 350m),                       // 1330
        new(104, 7, 2, 150m),   new(104, 8, 4, 60m),                        // 540
        new(105, 6, 1, 350m),                                               // 350 (iptal)
        new(106, 9, 6, 25m),    new(106, 8, 2, 60m),                        // 270
        new(107, 4, 2, 450m),                                               // 900
        new(108, 2, 5, 40m),    new(108, 1, 10, 12.50m),                    // 325 (bekliyor)
        new(109, 5, 2, 220m),   new(109, 6, 1, 350m),                       // 790
        new(110, 1, 20, 12.50m),                                            // 250
        new(111, 7, 1, 150m),                                               // 150
        new(112, 3, 1, 980m),                                               // 980 (iptal)
    ];
}
