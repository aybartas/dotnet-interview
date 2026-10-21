using Junior.CollectionsLinq.LinqBasics.Data;

namespace Junior.CollectionsLinq.LinqBasics.Reports;

/// <summary>Raporları okunur metne çevirir (konsol çıktısı için). Hesap Reports sınıfındadır.</summary>
public static class ReportCatalog
{
    public sealed record Entry(int No, string Question, string Operators, Func<ShopData, string> Render);

    private static string Join<T>(IEnumerable<T> items, Func<T, string>? fmt = null) => string.Join(" · ", items.Select(i => fmt?.Invoke(i) ?? i?.ToString() ?? ""));

    public static IReadOnlyList<Entry> All { get; } =
    [
        new(1,  "Teslim edilen siparişler (tarihe göre)",               "Where, OrderBy, Select",
            d => Join(Reports.DeliveredOrderIds(d))),
        new(2,  "Ürün adı + KDV'li fiyat (%20)",                         "Select (tuple projeksiyon)",
            d => Join(Reports.PricesWithVat(d), x => $"{x.Name}={x.PriceWithVat:N2}")),
        new(3,  "Siparişi olan şehirler (tekrarsız, sıralı)",            "Select, Distinct, OrderBy (kültür)",
            d => Join(Reports.CitiesWithOrders(d))),
        new(4,  "Toplam ciro (teslim edilenler)",                        "Join, Sum",
            d => $"{Reports.TotalRevenue(d):N2} TL"),
        new(5,  "Ortalama sipariş tutarı",                               "GroupBy, Average",
            d => $"{Reports.AverageOrderValue(d):N2} TL"),
        new(6,  "Kategori bazında ciro (SORGU sözdizimi)",               "from/join/group/orderby/select",
            d => Join(Reports.RevenueByCategory(d), x => $"{x.Category}={x.Revenue:N0}")),
        new(7,  "Müşteri bazında ciro (azalan)",                         "Join, GroupBy, OrderByDescending, ThenBy",
            d => Join(Reports.RevenueByCustomer(d), x => $"{x.Customer}={x.Revenue:N0}")),
        new(8,  "En yüksek tutarlı sipariş",                             "GroupBy, MaxBy",
            d => { var r = Reports.LargestOrder(d); return $"#{r.OrderId} = {r.Total:N0} TL"; }),
        new(9,  "En çok satan ürün (adet)",                              "GroupBy, Sum, MaxBy",
            d => { var r = Reports.BestSellingProduct(d); return $"{r.Product} ({r.Quantity} adet)"; }),
        new(10, "Sipariş + müşteri adı + tutar",                         "Join, Select (alt sorgu)",
            d => Join(Reports.OrdersWithCustomer(d), x => $"#{x.OrderId} {x.Customer} {x.Total:N0}")),
        new(11, "Satılan toplam adet (iptaller hariç)",                  "Where, SelectMany, Sum",
            d => $"{Reports.TotalUnitsSold(d)} adet"),
        new(12, "Hiç siparişte yer almayan ürünler",                     "ExceptBy",
            d => Join(Reports.NeverOrderedProducts(d))),
        new(13, "Hiç siparişi olmayan müşteriler",                       "LeftJoin (.NET 10)",
            d => Join(Reports.CustomersWithoutOrders(d))),
        new(14, "Şehir bazında müşteri sayısı",                          "CountBy (.NET 9)",
            d => Join(Reports.CustomersPerCity(d), x => $"{x.City}={x.Count}")),
        new(15, "Aylık ciro",                                            "GroupBy (bileşik anahtar), OrderBy",
            d => Join(Reports.MonthlyRevenue(d), x => $"{x.Month}={x.Revenue:N0}")),
        new(16, "İptal oranı, bekleyen var mı, her siparişin kalemi var mı?", "Count, Any, All",
            d => { var r = Reports.OrderHealth(d); return $"iptal %{r.CancelRatePercent:F1} · bekleyen var: {r.AnyPending} · hepsinin kalemi var: {r.AllHaveItems}"; }),
        new(17, "Sayfalama: 5'erli, 2. sayfa",                           "OrderBy, Skip, Take",
            d => Join(Reports.OrdersPage(d, 2, 5))),
        new(18, "Siparişleri 5'erli gruplara böl",                       "Chunk",
            d => Join(Reports.OrderBatches(d, 5), b => $"[{string.Join(",", b)}]")),
        new(19, "Kategori → ürün adları",                                "ToLookup",
            d => Join(Reports.ProductNamesByCategory(d).OrderBy(g => g.Key, StringComparer.Ordinal), g => $"{g.Key}: {string.Join("/", g)}")),
        new(20, "Her kategorinin en pahalı ürünü",                       "GroupBy, MaxBy",
            d => Join(Reports.MostExpensivePerCategory(d), x => $"{x.Category}: {x.Product} ({x.Price:N0})")),
    ];
}
