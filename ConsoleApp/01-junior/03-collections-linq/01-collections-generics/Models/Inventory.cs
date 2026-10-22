namespace Junior.CollectionsLinq.CollectionsGenerics.Models;

/// <summary>
/// BAD API: exposes the internal List directly → the caller can modify the collection ANY WAY THEY LIKE (encapsulation is broken).
/// </summary>
public sealed class LeakyInventory
{
    private readonly List<string> _items = ["pen", "notebook"];
    public List<string> Items => _items;
}

/// <summary>
/// CORRECT API: returns a non-modifiable view (IReadOnlyList / AsReadOnly); changes are made only through the class's own methods
/// and the class can protect its own invariant (e.g. uniqueness).
/// </summary>
public sealed class SafeInventory
{
    private readonly List<string> _items = ["pen", "notebook"];

    /// <summary>Thin contract: read-only. The caller can still modify it if they CAST it to List&lt;string&gt; (see below).</summary>
    public IReadOnlyList<string> ItemsView => _items;

    /// <summary>Real protection: a ReadOnlyCollection wrapper — cannot be modified even when cast.</summary>
    public IReadOnlyList<string> ItemsProtected => _items.AsReadOnly();

    /// <summary>Snapshot (copy): does not reflect later changes; the caller can use the copy freely.</summary>
    public IReadOnlyList<string> Snapshot() => [.. _items];

    public bool Add(string item)
    {
        if (_items.Contains(item)) return false;      // invariant: no duplicates
        _items.Add(item);
        return true;
    }
}
