namespace Junior.CollectionsLinq.CollectionsGenerics.Models;

/// <summary>Contract for an entity that has an identity. TId is used as a key, so it must be notnull.</summary>
public interface IEntity<out TId> where TId : notnull
{
    TId Id { get; }
}

/// <summary>
/// Generic Repository sketch: a single class, type-safe for EVERY entity type (no casts, no boxing).
/// Constraints:
///   where T : class, IEntity&lt;TId&gt;   → T is a reference type and an entity that has an Id
///   where TId : notnull             → a Dictionary key cannot be null
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
            throw new InvalidOperationException($"{typeof(T).Name} already exists: {entity.Id}");
    }

    public T? Find(TId id) => _store.GetValueOrDefault(id);

    public T Get(TId id) => Find(id) ?? throw new KeyNotFoundException($"{typeof(T).Name} not found: {id}");

    public bool Remove(TId id) => _store.Remove(id);

    public void Update(T entity)
    {
        if (!_store.ContainsKey(entity.Id)) throw new KeyNotFoundException($"{typeof(T).Name} not found: {entity.Id}");
        _store[entity.Id] = entity;
    }

    /// <summary>A NON-MODIFIABLE view to the outside (snapshot).</summary>
    public IReadOnlyList<T> GetAll() => [.. _store.Values];

    public IReadOnlyList<T> Where(Func<T, bool> predicate) => [.. _store.Values.Where(predicate)];
}

public sealed record Product(int Id, string Name, decimal Price) : IEntity<int>;
public sealed record Customer(Guid Id, string Name) : IEntity<Guid>;

/// <summary>Generic method and constraint examples.</summary>
public static class GenericUtils
{
    /// <summary>where T : IComparable&lt;T&gt; → works for any comparable type; no boxing (int/decimal/string…).</summary>
    public static T Max<T>(IEnumerable<T> items) where T : IComparable<T>
    {
        using var e = items.GetEnumerator();
        if (!e.MoveNext()) throw new InvalidOperationException("Empty sequence");
        var max = e.Current;
        while (e.MoveNext())
            if (e.Current.CompareTo(max) > 0) max = e.Current;
        return max;
    }

    /// <summary>where T : new() → a parameterless ctor is required; new T() can be used.</summary>
    public static T CreateDefault<T>() where T : new() => new();

    /// <summary>where T : struct → only (non-nullable) value types; T? here means Nullable&lt;T&gt;.</summary>
    public static T? FirstOrNull<T>(IEnumerable<T> items) where T : struct
    {
        foreach (var item in items) return item;
        return null;
    }

    /// <summary>where T : class → only reference types (can be assigned null).</summary>
    public static T? OrNull<T>(bool condition, T value) where T : class => condition ? value : null;

    /// <summary>where T : notnull → a type argument that may be null produces a warning.</summary>
    public static Dictionary<T, int> CountBy<T>(IEnumerable<T> items) where T : notnull
    {
        var result = new Dictionary<T, int>();
        foreach (var item in items) result[item] = result.GetValueOrDefault(item) + 1;
        return result;
    }

    public static void Swap<T>(ref T a, ref T b) => (a, b) = (b, a);
}
