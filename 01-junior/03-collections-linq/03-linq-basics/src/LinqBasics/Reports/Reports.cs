using Junior.CollectionsLinq.LinqBasics.Data;

namespace Junior.CollectionsLinq.LinqBasics.Reports;

/// <summary>
/// 20 rapor alıştırması. Her metot BİR soruyu cevaplar ve hangi LINQ operatörlerini gerektirdiğini gösterir.
/// Önce soruyu OKUYUN, kendi sorgunuzu yazmayı deneyin, sonra buradaki çözümle karşılaştırın.
/// (Ciro = yalnızca TESLİM EDİLEN siparişler.)
/// </summary>
public static class Reports
{
    // Ortak yardımcı: teslim edilen siparişlerin kalemleri (tekrar eden mantığı tek yerde tutmak için)
    private static IEnumerable<OrderItem> DeliveredItems(ShopData d) =>
        d.Items.Join(d.Orders.Where(o => o.Status == OrderStatus.Delivered), i => i.OrderId, o => o.Id, (i, _) => i);

    // 1) Where + OrderBy — "Teslim edilen siparişler tarihe göre"
    public static IReadOnlyList<int> DeliveredOrderIds(ShopData d) =>
        d.Orders.Where(o => o.Status == OrderStatus.Delivered).OrderBy(o => o.Date).Select(o => o.Id).ToList();

    // 2) Select (projeksiyon) — "Ürün adı ve KDV'li (%20) fiyatı"
    public static IReadOnlyList<(string Name, decimal PriceWithVat)> PricesWithVat(ShopData d) =>
        d.Products.Select(p => (p.Name, PriceWithVat: p.Price * 1.20m)).ToList();

    // 3) Select + Distinct + OrderBy — "Siparişi olan müşterilerin şehirleri (tekrarsız, sıralı)"
    public static IReadOnlyList<string> CitiesWithOrders(ShopData d) =>
        d.Orders.Select(o => d.Customers.First(c => c.Id == o.CustomerId).City).Distinct().OrderBy(c => c, StringComparer.Create(new System.Globalization.CultureInfo("tr-TR"), false)).ToList();

    // 4) SelectMany/Sum — "Toplam ciro"
    public static decimal TotalRevenue(ShopData d) => DeliveredItems(d).Sum(i => i.Total);

    // 5) Average — "Teslim edilen siparişlerin ortalama tutarı"
    public static decimal AverageOrderValue(ShopData d) =>
        DeliveredItems(d).GroupBy(i => i.OrderId).Average(g => g.Sum(i => i.Total));

    // 6) Join + GroupBy + Sum (SORGU sözdizimiyle) — "Kategori bazında ciro (azalan)"
    public static IReadOnlyList<(string Category, decimal Revenue)> RevenueByCategory(ShopData d)
    {
        var query =
            from i in DeliveredItems(d)
            join p in d.Products on i.ProductId equals p.Id
            group i by p.Category into g
            orderby g.Sum(x => x.Total) descending
            select (g.Key, g.Sum(x => x.Total));
        return query.ToList();
    }

    // 7) GroupBy + OrderByDescending + ThenBy — "Müşteri bazında ciro (azalan), eşitlikte ada göre"
    public static IReadOnlyList<(string Customer, decimal Revenue)> RevenueByCustomer(ShopData d) =>
        d.Orders.Where(o => o.Status == OrderStatus.Delivered)
            .Join(d.Items, o => o.Id, i => i.OrderId, (o, i) => (o.CustomerId, i.Total))
            .GroupBy(x => x.CustomerId)
            .Select(g => (Customer: d.Customers.First(c => c.Id == g.Key).Name, Revenue: g.Sum(x => x.Total)))
            .OrderByDescending(x => x.Revenue).ThenBy(x => x.Customer)
            .ToList();

    // 8) MaxBy — "En yüksek tutarlı (teslim edilen) sipariş"
    public static (int OrderId, decimal Total) LargestOrder(ShopData d) =>
        DeliveredItems(d).GroupBy(i => i.OrderId).Select(g => (OrderId: g.Key, Total: g.Sum(i => i.Total))).MaxBy(x => x.Total);

    // 9) GroupBy + Sum + MaxBy — "En çok satan ürün (adet; teslim edilenler)"
    public static (string Product, int Quantity) BestSellingProduct(ShopData d) =>
        DeliveredItems(d).GroupBy(i => i.ProductId)
            .Select(g => (Product: d.Products.First(p => p.Id == g.Key).Name, Quantity: g.Sum(i => i.Quantity)))
            .MaxBy(x => x.Quantity);

    // 10) Join — "Sipariş numarası + müşteri adı + sipariş toplamı (tüm siparişler)"
    public static IReadOnlyList<(int OrderId, string Customer, decimal Total)> OrdersWithCustomer(ShopData d) =>
        d.Orders.Join(d.Customers, o => o.CustomerId, c => c.Id, (o, c) => (o, c))
                .Select(x => (x.o.Id, x.c.Name, Total: d.Items.Where(i => i.OrderId == x.o.Id).Sum(i => i.Total)))
                .ToList();

    // 11) SelectMany — "Satılan toplam adet (iptaller hariç)"
    public static int TotalUnitsSold(ShopData d) =>
        d.Orders.Where(o => o.Status != OrderStatus.Cancelled)
                .SelectMany(o => d.Items.Where(i => i.OrderId == o.Id))
                .Sum(i => i.Quantity);

    // 12) Except / GroupJoin — "Hiç satılmamış (teslim edilmemiş dahil hiçbir siparişte yer almamış) ürünler"
    public static IReadOnlyList<string> NeverOrderedProducts(ShopData d) =>
        d.Products.ExceptBy(d.Items.Select(i => i.ProductId), p => p.Id).Select(p => p.Name).ToList();

    // 13) LeftJoin (.NET 10) — "Hiç siparişi olmayan müşteriler"
    public static IReadOnlyList<string> CustomersWithoutOrders(ShopData d) =>
        d.Customers.LeftJoin(d.Orders, c => c.Id, o => o.CustomerId, (c, o) => (Customer: c, Order: o))
                   .Where(x => x.Order is null)
                   .Select(x => x.Customer.Name)
                   .ToList();

    // 14) CountBy (.NET 9) — "Şehir bazında müşteri sayısı"
    public static IReadOnlyList<(string City, int Count)> CustomersPerCity(ShopData d) =>
        d.Customers.CountBy(c => c.City).OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key, StringComparer.Ordinal)
                   .Select(kv => (kv.Key, kv.Value)).ToList();

    // 15) GroupBy (tarih parçası) — "Aylık ciro (yıl-ay sırasıyla)"
    public static IReadOnlyList<(string Month, decimal Revenue)> MonthlyRevenue(ShopData d) =>
        d.Orders.Where(o => o.Status == OrderStatus.Delivered)
                .Join(d.Items, o => o.Id, i => i.OrderId, (o, i) => (o.Date, i.Total))
                .GroupBy(x => (x.Date.Year, x.Date.Month))
                .OrderBy(g => g.Key)
                .Select(g => ($"{g.Key.Year}-{g.Key.Month:00}", g.Sum(x => x.Total)))
                .ToList();

    // 16) Count + Any/All — "İptal oranı (%) ve bekleyen sipariş var mı?"
    public static (double CancelRatePercent, bool AnyPending, bool AllHaveItems) OrderHealth(ShopData d) =>
        (100.0 * d.Orders.Count(o => o.Status == OrderStatus.Cancelled) / d.Orders.Count,
         d.Orders.Any(o => o.Status == OrderStatus.Pending),
         d.Orders.All(o => d.Items.Any(i => i.OrderId == o.Id)));

    // 17) Skip/Take — "Siparişleri tarihe göre sırala, sayfa boyutu 5, 2. sayfa"
    public static IReadOnlyList<int> OrdersPage(ShopData d, int page, int pageSize) =>
        d.Orders.OrderBy(o => o.Date).Skip((page - 1) * pageSize).Take(pageSize).Select(o => o.Id).ToList();

    // 18) Chunk — "Siparişleri 5'erli işleme gruplarına böl"
    public static IReadOnlyList<int[]> OrderBatches(ShopData d, int size) =>
        d.Orders.Select(o => o.Id).Chunk(size).ToList();

    // 19) ToLookup / ToDictionary — "Kategori → ürün adları (ToLookup) ve Id → ürün (ToDictionary)"
    public static ILookup<string, string> ProductNamesByCategory(ShopData d) =>
        d.Products.ToLookup(p => p.Category, p => p.Name);

    // 20) GroupBy + MaxBy (top-1 per group) — "Her kategorinin en pahalı ürünü"
    public static IReadOnlyList<(string Category, string Product, decimal Price)> MostExpensivePerCategory(ShopData d) =>
        d.Products.GroupBy(p => p.Category)
                  .Select(g => g.MaxBy(p => p.Price)!)
                  .OrderBy(p => p.Category, StringComparer.Ordinal)
                  .Select(p => (p.Category, p.Name, p.Price))
                  .ToList();
}
