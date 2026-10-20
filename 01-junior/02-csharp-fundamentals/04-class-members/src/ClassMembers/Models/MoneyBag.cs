using System.Collections;

namespace Junior.CSharpFundamentals.ClassMembers.Models;

/// <summary>Indexer'lı küçük koleksiyon tipi: bag[0] (sıra), bag["TRY"] (para birimi toplamı), bag[0..2] (aralık).</summary>
public sealed class MoneyBag : IEnumerable<Money>
{
    private readonly List<Money> _items = [];

    public int Count => _items.Count;
    public void Add(Money money) => _items.Add(money);

    /// <summary>Konum indexer'ı — get/set. Geçersiz indeks ArgumentOutOfRangeException fırlatır.</summary>
    public Money this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }

    /// <summary>İndexer parametre tipi int olmak zorunda değil: para birimine göre toplam (salt okunur).</summary>
    public Money this[string currency]
        => new(_items.Where(m => m.Currency == currency.ToUpperInvariant()).Sum(m => m.Amount), currency);

    /// <summary>Çok parametreli indexer: bag["TRY", 0] → o para biriminin 0. kalemi.</summary>
    public Money this[string currency, int nth]
        => _items.Where(m => m.Currency == currency.ToUpperInvariant()).ElementAt(nth);

    /// <summary>Index/Range desteği (C# 8): bag[^1] son eleman, bag[1..] dilim — Count + int indexer varsa derleyici otomatik destekler.</summary>
    public IEnumerable<Money> Slice(int start, int length) => _items.Skip(start).Take(length);

    public IEnumerator<Money> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
