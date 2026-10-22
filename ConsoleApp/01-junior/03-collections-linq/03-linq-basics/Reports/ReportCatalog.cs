using Junior.CollectionsLinq.LinqBasics.Data;

namespace Junior.CollectionsLinq.LinqBasics.Reports;

/// <summary>Turns the reports into readable text (for console output). The computation lives in the Reports class.</summary>
public static class ReportCatalog
{
    public sealed record Entry(int No, string Question, string Operators, Func<ShopData, string> Render);

    private static string Join<T>(IEnumerable<T> items, Func<T, string>? fmt = null) => string.Join(" · ", items.Select(i => fmt?.Invoke(i) ?? i?.ToString() ?? ""));

    public static IReadOnlyList<Entry> All { get; } =
    [
        new(1,  "Delivered orders (by date)",                           "Where, OrderBy, Select",
            d => Join(Reports.DeliveredOrderIds(d))),
        new(2,  "Product name + price with VAT (20%)",                   "Select (tuple projection)",
            d => Join(Reports.PricesWithVat(d), x => FormattableString.Invariant($"{x.Name}={x.PriceWithVat:N2}"))),
        new(3,  "Cities that have orders (distinct, sorted)",            "Select, Distinct, OrderBy (culture)",
            d => Join(Reports.CitiesWithOrders(d))),
        new(4,  "Total revenue (delivered)",                             "Join, Sum",
            d => FormattableString.Invariant($"${Reports.TotalRevenue(d):N2}")),
        new(5,  "Average order value",                                   "GroupBy, Average",
            d => FormattableString.Invariant($"${Reports.AverageOrderValue(d):N2}")),
        new(6,  "Revenue by category (QUERY syntax)",                    "from/join/group/orderby/select",
            d => Join(Reports.RevenueByCategory(d), x => FormattableString.Invariant($"{x.Category}={x.Revenue:N0}"))),
        new(7,  "Revenue by customer (descending)",                      "Join, GroupBy, OrderByDescending, ThenBy",
            d => Join(Reports.RevenueByCustomer(d), x => FormattableString.Invariant($"{x.Customer}={x.Revenue:N0}"))),
        new(8,  "Largest order",                                         "GroupBy, MaxBy",
            d => { var r = Reports.LargestOrder(d); return FormattableString.Invariant($"#{r.OrderId} = ${r.Total:N0}"); }),
        new(9,  "Best-selling product (units)",                          "GroupBy, Sum, MaxBy",
            d => { var r = Reports.BestSellingProduct(d); return $"{r.Product} ({r.Quantity} units)"; }),
        new(10, "Order + customer name + total",                         "Join, Select (subquery)",
            d => Join(Reports.OrdersWithCustomer(d), x => FormattableString.Invariant($"#{x.OrderId} {x.Customer} {x.Total:N0}"))),
        new(11, "Total units sold (excluding cancelled)",                "Where, SelectMany, Sum",
            d => $"{Reports.TotalUnitsSold(d)} units"),
        new(12, "Products that appear in no order",                      "ExceptBy",
            d => Join(Reports.NeverOrderedProducts(d))),
        new(13, "Customers with no orders",                              "LeftJoin (.NET 10)",
            d => Join(Reports.CustomersWithoutOrders(d))),
        new(14, "Customer count by city",                                "CountBy (.NET 9)",
            d => Join(Reports.CustomersPerCity(d), x => $"{x.City}={x.Count}")),
        new(15, "Monthly revenue",                                       "GroupBy (composite key), OrderBy",
            d => Join(Reports.MonthlyRevenue(d), x => FormattableString.Invariant($"{x.Month}={x.Revenue:N0}"))),
        new(16, "Cancellation rate, any pending, does every order have items?", "Count, Any, All",
            d => { var r = Reports.OrderHealth(d); return FormattableString.Invariant($"cancelled {r.CancelRatePercent:F1}% · any pending: {r.AnyPending} · all have items: {r.AllHaveItems}"); }),
        new(17, "Paging: 5 per page, page 2",                            "OrderBy, Skip, Take",
            d => Join(Reports.OrdersPage(d, 2, 5))),
        new(18, "Split the orders into batches of 5",                    "Chunk",
            d => Join(Reports.OrderBatches(d, 5), b => $"[{string.Join(",", b)}]")),
        new(19, "Category → product names",                              "ToLookup",
            d => Join(Reports.ProductNamesByCategory(d).OrderBy(g => g.Key, StringComparer.Ordinal), g => $"{g.Key}: {string.Join("/", g)}")),
        new(20, "The most expensive product in each category",           "GroupBy, MaxBy",
            d => Join(Reports.MostExpensivePerCategory(d), x => FormattableString.Invariant($"{x.Category}: {x.Product} ({x.Price:N0})"))),
    ];
}
