namespace Junior.CSharpFundamentals.ClassMembers.Models;

// ═════════ ÖNCE: public field'lı model ═════════
// Sorunlar: (1) doğrulama yapılamaz — Price = -5 serbest, (2) salt okunur/yalnızca-ctor-set yapılamaz,
// (3) sonradan property'ye çevirmek BINARY BREAKING CHANGE (aşağıda), (4) data binding/serileştirme/EF çoğu araç property bekler.
public class ProductBefore
{
    public string Name = "";
    public decimal Price;
    public int Stock;
}

// ═════════ SONRA: property + doğrulama + invariant koruma ═════════
public class ProductAfter
{
    // required (C# 11): nesne oluşturulurken bu üyenin ATANMASI ZORUNLU (object initializer ile) — "yarım nesne" oluşmaz
    public required string Name { get; init; }            // init: yalnızca oluşturma sırasında atanır, sonra salt okunur

    // C# 14 'field' keyword: backing field'ı elle yazmadan setter'da doğrulama
    public decimal Price
    {
        get;
        set => field = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value), "Fiyat negatif olamaz");
    }

    // private setter: dışarıdan değiştirilemez, durumu yalnızca sınıfın kendi metotları değiştirir
    public int Stock { get; private set; }

    // computed (hesaplanan) property: depolama yok; ucuz ve yan etkisiz olmalı
    public bool InStock => Stock > 0;

    public void AddStock(int quantity)                    // PAHALI/yan etkili iş → metot (property değil)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        Stock += quantity;
    }

    public void Remove(int quantity)
    {
        if (quantity > Stock) throw new InvalidOperationException("Yetersiz stok");
        Stock -= quantity;
    }

    // static property: tip düzeyinde paylaşılan değer
    public static int CreatedCount { get; private set; }
    public ProductAfter() => CreatedCount++;

    // readonly alan ile { get; } farkı: alan bellekte bir slot; property bir METOT çifti (get_X). Public API'de property tercih edilir.
    public readonly DateTime CreatedAtField = DateTime.UtcNow;
    public DateTime CreatedAtProperty { get; } = DateTime.UtcNow;
}
