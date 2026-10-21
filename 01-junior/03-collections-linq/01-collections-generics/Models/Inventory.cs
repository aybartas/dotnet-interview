namespace Junior.CollectionsLinq.CollectionsGenerics.Models;

/// <summary>
/// KÖTÜ API: dahili List'i doğrudan dışarı verir → çağıran koleksiyonu İSTEDİĞİ GİBİ değiştirebilir (encapsulation delinir).
/// </summary>
public sealed class LeakyInventory
{
    private readonly List<string> _items = ["kalem", "defter"];
    public List<string> Items => _items;
}

/// <summary>
/// DOĞRU API: değiştirilemeyen görünüm (IReadOnlyList / AsReadOnly) döndürür; değişiklik yalnızca sınıfın kendi metotlarıyla yapılır
/// ve sınıf kendi invariant'ını (örn. tekrarsızlık) koruyabilir.
/// </summary>
public sealed class SafeInventory
{
    private readonly List<string> _items = ["kalem", "defter"];

    /// <summary>İnce kontrat: yalnızca okuma. Çağıran bunu List&lt;string&gt;'e CAST EDERSE yine değiştirebilir (aşağıya bakın).</summary>
    public IReadOnlyList<string> ItemsView => _items;

    /// <summary>Gerçek koruma: ReadOnlyCollection sarmalayıcısı — cast edilse bile değiştirilemez.</summary>
    public IReadOnlyList<string> ItemsProtected => _items.AsReadOnly();

    /// <summary>Anlık görüntü (kopya): sonradan oluşan değişiklikleri yansıtmaz; çağıran kopyayı serbestçe kullanır.</summary>
    public IReadOnlyList<string> Snapshot() => [.. _items];

    public bool Add(string item)
    {
        if (_items.Contains(item)) return false;      // invariant: tekrarsız
        _items.Add(item);
        return true;
    }
}
