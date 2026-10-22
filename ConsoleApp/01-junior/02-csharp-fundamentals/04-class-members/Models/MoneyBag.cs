using System.Collections;

namespace Junior.CSharpFundamentals.ClassMembers.Models;

/// <summary>A small collection type with indexers: bag[0] (position), bag["TRY"] (total per currency), bag[0..2] (range).</summary>
public sealed class MoneyBag : IEnumerable<Money>
{
    private readonly List<Money> _items = [];

    public int Count => _items.Count;
    public void Add(Money money) => _items.Add(money);

    /// <summary>Positional indexer — get/set. An invalid index throws ArgumentOutOfRangeException.</summary>
    public Money this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }

    /// <summary>The indexer parameter type doesn't have to be int: total by currency (read-only).</summary>
    public Money this[string currency]
        => new(_items.Where(m => m.Currency == currency.ToUpperInvariant()).Sum(m => m.Amount), currency);

    /// <summary>Multi-parameter indexer: bag["TRY", 0] → the 0th item of that currency.</summary>
    public Money this[string currency, int nth]
        => _items.Where(m => m.Currency == currency.ToUpperInvariant()).ElementAt(nth);

    /// <summary>Index/Range support (C# 8): bag[^1] last element, bag[1..] slice — the compiler supports these automatically if there is a Count + int indexer.</summary>
    public IEnumerable<Money> Slice(int start, int length) => _items.Skip(start).Take(length);

    public IEnumerator<Money> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
