using Junior.CollectionsLinq.LinqBasics.Data;
using Junior.CollectionsLinq.LinqBasics.Reports;

namespace Junior.CollectionsLinq.LinqBasics.Tests;

/// <summary>
/// 20 raporun beklenen sonuçları veri setinden ELLE hesaplanmıştır (bkz. ShopData.cs yorumları).
/// </summary>
public class ReportTests
{
    private static readonly ShopData D = new();

    [Fact] // 1
    public void DeliveredOrderIds_are_sorted_by_date()
        => Assert.Equal([101, 102, 103, 104, 106, 107, 109, 110, 111], Reports.Reports.DeliveredOrderIds(D));

    [Fact] // 2
    public void PricesWithVat_adds_twenty_percent()
    {
        var prices = Reports.Reports.PricesWithVat(D);
        Assert.Equal(10, prices.Count);
        Assert.Equal(("Kalem", 15.00m), prices[0]);
        Assert.Equal(("Çanta", 1176m), prices[2]);
    }

    [Fact] // 3
    public void CitiesWithOrders_are_distinct_and_sorted_by_turkish_culture()
        => Assert.Equal(["Ankara", "İstanbul", "İzmir"], Reports.Reports.CitiesWithOrders(D));

    [Fact] // 4
    public void TotalRevenue_counts_only_delivered_orders()
        => Assert.Equal(5030m, Reports.Reports.TotalRevenue(D));

    [Fact] // 5
    public void AverageOrderValue_is_total_over_delivered_order_count()
        => Assert.Equal(5030m / 9, Reports.Reports.AverageOrderValue(D));

    [Fact] // 6
    public void RevenueByCategory_is_descending()
        => Assert.Equal(
            [("Elektronik", 2710m), ("Aksesuar", 980m), ("Ev", 960m), ("Kırtasiye", 380m)],
            Reports.Reports.RevenueByCategory(D));

    [Fact] // 7
    public void RevenueByCustomer_is_descending_with_name_tiebreak()
        => Assert.Equal(
            [("Zeynep Kaya", 2230m), ("Ayşe Yılmaz", 1460m), ("Ali Demir", 670m), ("Elif Şahin", 520m), ("Mehmet Çelik", 150m)],
            Reports.Reports.RevenueByCustomer(D));

    [Fact] // 8
    public void LargestOrder_is_103()
        => Assert.Equal((103, 1330m), Reports.Reports.LargestOrder(D));

    [Fact] // 9
    public void BestSellingProduct_is_Kalem()
        => Assert.Equal(("Kalem", 24), Reports.Reports.BestSellingProduct(D));

    [Fact] // 10
    public void OrdersWithCustomer_lists_every_order_with_its_total()
    {
        var rows = Reports.Reports.OrdersWithCustomer(D);
        Assert.Equal(12, rows.Count);
        Assert.Contains((103, "Zeynep Kaya", 1330m), rows);
        Assert.Contains((108, "Ali Demir", 325m), rows);
    }

    [Fact] // 11
    public void TotalUnitsSold_excludes_cancelled_orders()
        => Assert.Equal(65, Reports.Reports.TotalUnitsSold(D));

    [Fact] // 12
    public void NeverOrderedProducts_is_Ajanda()
        => Assert.Equal(["Ajanda"], Reports.Reports.NeverOrderedProducts(D));

    [Fact] // 13
    public void CustomersWithoutOrders_uses_LeftJoin()
        => Assert.Equal(["Can Öztürk"], Reports.Reports.CustomersWithoutOrders(D));

    [Fact] // 14
    public void CustomersPerCity_counts_two_each()
        => Assert.Equal([("Ankara", 2), ("İstanbul", 2), ("İzmir", 2)], Reports.Reports.CustomersPerCity(D));

    [Fact] // 15
    public void MonthlyRevenue_is_chronological()
        => Assert.Equal([("2026-01", 2130m), ("2026-02", 1710m), ("2026-03", 1190m)], Reports.Reports.MonthlyRevenue(D));

    [Fact] // 16
    public void OrderHealth_reports_rate_pending_and_items()
    {
        var (rate, anyPending, allHaveItems) = Reports.Reports.OrderHealth(D);
        Assert.Equal(100.0 * 2 / 12, rate, 6);
        Assert.True(anyPending);
        Assert.True(allHaveItems);
    }

    [Fact] // 17
    public void OrdersPage_returns_the_requested_page()
    {
        Assert.Equal([101, 102, 103, 104, 105], Reports.Reports.OrdersPage(D, 1, 5));
        Assert.Equal([106, 107, 108, 109, 110], Reports.Reports.OrdersPage(D, 2, 5));
        Assert.Equal([111, 112], Reports.Reports.OrdersPage(D, 3, 5));
        Assert.Empty(Reports.Reports.OrdersPage(D, 4, 5));
    }

    [Fact] // 18
    public void OrderBatches_splits_into_chunks_of_five()
    {
        var batches = Reports.Reports.OrderBatches(D, 5);
        Assert.Equal(3, batches.Count);
        Assert.Equal([101, 102, 103, 104, 105], batches[0]);
        Assert.Equal([111, 112], batches[2]);
    }

    [Fact] // 19
    public void ProductNamesByCategory_is_a_lookup_that_returns_empty_for_missing_keys()
    {
        var lookup = Reports.Reports.ProductNamesByCategory(D);
        Assert.Equal(["Kalem", "Defter", "Ajanda"], lookup["Kırtasiye"]);
        Assert.Equal(3, lookup["Ev"].Count());
        Assert.Empty(lookup["Yok"]);
        Assert.False(lookup.Contains("Yok"));
    }

    [Fact] // 20
    public void MostExpensivePerCategory_has_one_row_per_category()
        => Assert.Equal(
            [("Aksesuar", "Çanta", 980m), ("Elektronik", "Kulaklık", 450m), ("Ev", "Lamba", 150m), ("Kırtasiye", "Ajanda", 90m)],
            Reports.Reports.MostExpensivePerCategory(D));

    // ───────── Veri seti tutarlılığı ─────────

    [Fact]
    public void Report_totals_agree_with_each_other()
    {
        Assert.Equal(Reports.Reports.TotalRevenue(D), Reports.Reports.RevenueByCategory(D).Sum(x => x.Revenue));
        Assert.Equal(Reports.Reports.TotalRevenue(D), Reports.Reports.RevenueByCustomer(D).Sum(x => x.Revenue));
        Assert.Equal(Reports.Reports.TotalRevenue(D), Reports.Reports.MonthlyRevenue(D).Sum(x => x.Revenue));
    }
}
