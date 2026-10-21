namespace Junior.CollectionsLinq.CollectionsGenerics.Models;

/// <summary>Kimliği olan varlık sözleşmesi. TId anahtar olarak kullanılacağından notnull olmalı.</summary>
public interface IEntity<out TId> where TId : notnull
{
    TId Id { get; }
}

/// <summary>
/// Generic Repository taslağı: tek bir sınıf, HER varlık tipi için tip güvenli (cast yok, boxing yok).
/// Constraint'ler:
///   where T : class, IEntity&lt;TId&gt;   → T referans tip ve Id'si olan bir varlık
///   where TId : notnull             → Dictionary anahtarı null olamaz
/// </summary>
public class InMemoryRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : notnull
{
    private readonly Dictionary<TId, T> _store = [];

    public int Count => _store.Count;

    public void Add(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        if (!_store.TryAdd(entity.Id, entity))
            throw new InvalidOperationException($"{typeof(T).Name} zaten var: {entity.Id}");
    }

    public T? Find(TId id) => _store.GetValueOrDefault(id);

    public T Get(TId id) => Find(id) ?? throw new KeyNotFoundException($"{typeof(T).Name} bulunamadı: {id}");

    public bool Remove(TId id) => _store.Remove(id);

    public void Update(T entity)
    {
        if (!_store.ContainsKey(entity.Id)) throw new KeyNotFoundException($"{typeof(T).Name} bulunamadı: {entity.Id}");
        _store[entity.Id] = entity;
    }

    /// <summary>Dışarıya DEĞİŞTİRİLEMEZ görünüm (anlık görüntü).</summary>
    public IReadOnlyList<T> GetAll() => [.. _store.Values];

    public IReadOnlyList<T> Where(Func<T, bool> predicate) => [.. _store.Values.Where(predicate)];
}

public sealed record Product(int Id, string Name, decimal Price) : IEntity<int>;
public sealed record Customer(Guid Id, string Name) : IEntity<Guid>;

/// <summary>Generic metot ve constraint örnekleri.</summary>
public static class GenericUtils
{
    /// <summary>where T : IComparable&lt;T&gt; → karşılaştırılabilir her tip için çalışır; boxing yok (int/decimal/string…).</summary>
    public static T Max<T>(IEnumerable<T> items) where T : IComparable<T>
    {
        using var e = items.GetEnumerator();
        if (!e.MoveNext()) throw new InvalidOperationException("Boş dizi");
        var max = e.Current;
        while (e.MoveNext())
            if (e.Current.CompareTo(max) > 0) max = e.Current;
        return max;
    }

    /// <summary>where T : new() → parametresiz ctor şart; new T() kullanılabilir.</summary>
    public static T CreateDefault<T>() where T : new() => new();

    /// <summary>where T : struct → yalnızca (nullable olmayan) değer tipleri; T? burada Nullable&lt;T&gt; demektir.</summary>
    public static T? FirstOrNull<T>(IEnumerable<T> items) where T : struct
    {
        foreach (var item in items) return item;
        return null;
    }

    /// <summary>where T : class → yalnızca referans tipleri (null atanabilir).</summary>
    public static T? OrNull<T>(bool condition, T value) where T : class => condition ? value : null;

    /// <summary>where T : notnull → null olabilen tip argümanı uyarı üretir.</summary>
    public static Dictionary<T, int> CountBy<T>(IEnumerable<T> items) where T : notnull
    {
        var result = new Dictionary<T, int>();
        foreach (var item in items) result[item] = result.GetValueOrDefault(item) + 1;
        return result;
    }

    public static void Swap<T>(ref T a, ref T b) => (a, b) = (b, a);
}
