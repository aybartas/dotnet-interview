namespace Junior.CollectionsLinq.DelegatesLambdasEvents.Models;

public sealed record DiscountResult(decimal Total, IReadOnlyList<(string Rule, decimal Amount)> Applied);

/// <summary>
/// STRATEJİ deseni delegate/lambda ile: kurallar ve birleştirme biçimi bir SINIF HİYERARŞİSİ değil, DEĞER olarak (Func) verilir.
/// Yeni kural = yeni lambda; motor değişmez (Open/Closed). Karşılaştırma: J2.3'teki IFeePolicy/IPaymentRule sınıfları — aynı fikir, daha az tören.
/// </summary>
public sealed class DiscountEngine
{
    private readonly List<(string Name, Func<Order, decimal> Rule)> _rules = [];

    public DiscountEngine AddRule(string name, Func<Order, decimal> rule)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(rule);
        _rules.Add((name, rule));
        return this;                                                // akıcı (fluent) kullanım
    }

    public int RuleCount => _rules.Count;

    /// <param name="order">İndirimi hesaplanacak sipariş.</param>
    /// <param name="combine">Birden çok kuralın indirimlerini nasıl birleştireceğimiz de bir strateji (Func).</param>
    public DiscountResult Calculate(Order order, Func<IEnumerable<decimal>, decimal>? combine = null)
    {
        combine ??= Combiners.Best;
        var applied = _rules
            .Select(r => (r.Name, Amount: Math.Max(0m, r.Rule(order))))
            .Where(x => x.Amount > 0)
            .ToList();
        var total = Math.Min(combine(applied.Select(a => a.Amount)), order.Amount);   // indirim tutarı siparişi aşamaz
        return new DiscountResult(total, applied);
    }
}

/// <summary>Hazır kural fabrikaları: her biri bir lambda DÖNDÜRÜR (closure ile parametreyi yakalar).</summary>
public static class Rules
{
    public static Func<Order, decimal> Percentage(decimal percent) => o => o.Amount * percent / 100m;

    public static Func<Order, decimal> BulkOver(int itemCount, decimal percent) => o => o.ItemCount > itemCount ? o.Amount * percent / 100m : 0m;

    public static Func<Order, decimal> FirstOrderFlat(decimal amount) => o => o.FirstOrder ? amount : 0m;

    /// <summary>Kural birleştirici (higher-order function): koşul sağlanırsa verilen kuralı uygular.</summary>
    public static Func<Order, decimal> When(Func<Order, bool> condition, Func<Order, decimal> rule) => o => condition(o) ? rule(o) : 0m;

    public static Func<Order, decimal> Capped(Func<Order, decimal> rule, decimal max) => o => Math.Min(rule(o), max);
}

public static class Combiners
{
    /// <summary>En iyi tek indirim.</summary>
    public static readonly Func<IEnumerable<decimal>, decimal> Best = ds => ds.DefaultIfEmpty(0m).Max();

    /// <summary>Hepsinin toplamı.</summary>
    public static readonly Func<IEnumerable<decimal>, decimal> Sum = ds => ds.Sum();
}
