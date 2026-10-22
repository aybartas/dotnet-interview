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
/// A small e-commerce data set that can be verified BY HAND (6 customers, 10 products, 12 orders).
/// Total revenue of delivered orders = $5030 (9 orders) — the expected results of the reports were verified by hand against this (Section08).
/// The city names are deliberately Turkish: report 3 sorts them with the tr-TR culture (İ / I ordering).
/// </summary>
public sealed class ShopData
{
    public IReadOnlyList<Customer> Customers { get; } =
    [
        new(1, "Alice Johnson", "Ankara", Tier.Gold),
        new(2, "Bob Smith", "İstanbul", Tier.Standard),
        new(3, "Carol White", "İzmir", Tier.Platinum),
        new(4, "David Brown", "Ankara", Tier.Standard),
        new(5, "Emma Davis", "İstanbul", Tier.Gold),
        new(6, "Frank Miller", "İzmir", Tier.Standard),         // has NO orders
    ];

    public IReadOnlyList<Product> Products { get; } =
    [
        new(1, "Pen", "Stationery", 12.50m),
        new(2, "Notebook", "Stationery", 40m),
        new(3, "Bag", "Accessories", 980m),
        new(4, "Headphones", "Electronics", 450m),
        new(5, "Mouse", "Electronics", 220m),
        new(6, "Keyboard", "Electronics", 350m),
        new(7, "Lamp", "Home", 150m),
        new(8, "Mug", "Home", 60m),
        new(9, "Glass", "Home", 25m),
        new(10, "Planner", "Stationery", 90m),                  // NEVER SOLD
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
        new(105, 6, 1, 350m),                                               // 350 (cancelled)
        new(106, 9, 6, 25m),    new(106, 8, 2, 60m),                        // 270
        new(107, 4, 2, 450m),                                               // 900
        new(108, 2, 5, 40m),    new(108, 1, 10, 12.50m),                    // 325 (pending)
        new(109, 5, 2, 220m),   new(109, 6, 1, 350m),                       // 790
        new(110, 1, 20, 12.50m),                                            // 250
        new(111, 7, 1, 150m),                                               // 150
        new(112, 3, 1, 980m),                                               // 980 (cancelled)
    ];
}
