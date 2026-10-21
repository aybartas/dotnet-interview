using Junior.CollectionsLinq.DelegatesLambdasEvents.Models;

namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class Section07_DiscountEngine
{
    public static void Run()
    {
        // Kurallar LAMBDA olarak verilir — motor kural türlerini bilmez
        var engine = new DiscountEngine()
            .AddRule("%10 genel",         Rules.Percentage(10))
            .AddRule("5+ ürün %15",       Rules.BulkOver(5, 15))
            .AddRule("ilk sipariş 50 TL", Rules.FirstOrderFlat(50))
            .AddRule("VIP (inline lambda)", o => o.Customer == "Ayşe" ? 30m : 0m)                 // yerinde yazılmış kural
            .AddRule("büyük sipariş (üst sınır 200)", Rules.Capped(Rules.When(o => o.Amount > 1000, Rules.Percentage(20)), 200));

        var orders = new[]
        {
            new Order(1, "Ali",  300m,  ItemCount: 2),
            new Order(2, "Ali",  400m,  ItemCount: 8,  FirstOrder: true),
            new Order(3, "Ayşe", 2000m, ItemCount: 3),
        };

        foreach (var o in orders)
        {
            var best = engine.Calculate(o);                                  // varsayılan strateji: en iyi tek indirim
            var sum = engine.Calculate(o, Combiners.Sum);                    // başka strateji: toplam
            Console.WriteLine($"   Sipariş {o.Id} ({o.Customer}, {o.Amount:N0} TL, {o.ItemCount} ürün{(o.FirstOrder ? ", ilk sipariş" : "")}):");
            Console.WriteLine($"      uygulanabilen kurallar: {(best.Applied.Count == 0 ? "yok" : string.Join(", ", best.Applied.Select(a => $"{a.Rule}={a.Amount:N0}")))}");
            Console.WriteLine($"      en iyi tek indirim = {best.Total:N0} TL · toplam strateji = {sum.Total:N0} TL");
        }

        Console.WriteLine("\nÖzel birleştirme stratejisi (lambda): 'en iyi iki indirimin toplamı':");
        var topTwo = engine.Calculate(orders[1], ds => ds.OrderByDescending(d => d).Take(2).Sum());
        Console.WriteLine($"   Sipariş 2 → {topTwo.Total:N0} TL");

        Console.WriteLine("""

        Neden delegate/lambda ile strateji?
          • Davranış DEĞER olur: parametre olarak geçer, listede tutulur, birleştirilir (When, Capped = higher-order function).
          • Sınıf hiyerarşisi/arayüz tören yükü yok: tek metotluk davranış için `Func<Order, decimal>` yeterli.
          • Yeni kural = yeni lambda; DiscountEngine DEĞİŞMEZ (Open/Closed).
        Ne zaman interface/sınıf?  Strateji durum taşıyorsa, birden çok metodu varsa, adlandırılıp DI ile kaydedilecekse, ya da ayrı ayrı test edilecek karmaşıklıktaysa.
        Kapanışlara dikkat: Rules.Percentage(10) → lambda 'percent' parametresini YAKALAR (closure); sıcak yolda çok sayıda kural üretmek tahsis demektir.
        """);
    }
}
