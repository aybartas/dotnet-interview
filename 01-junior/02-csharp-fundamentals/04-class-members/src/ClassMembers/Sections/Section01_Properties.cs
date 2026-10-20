using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section01_Properties
{
    public static void Run()
    {
        Console.WriteLine("A) ÖNCE (public field): geçersiz duruma düşmek serbest");
        var before = new ProductBefore { Name = "Kalem", Price = -5, Stock = -100 };
        Console.WriteLine($"   Price = {before.Price}, Stock = {before.Stock}   ← negatif fiyat/stok; kimse engelleyemedi");

        Console.WriteLine("\nB) SONRA (property): invariant korunuyor");
        var product = new ProductAfter { Name = "Kalem", Price = 12.5m };            // required + init
        try { product.Price = -1; }
        catch (ArgumentOutOfRangeException ex) { Console.WriteLine($"   Price = -1 → {ex.GetType().Name}: {ex.ParamName}"); }
        // product.Name = "x";                  // ✗ CS8852: init-only — nesne oluşturulduktan sonra atanamaz
        // product.Stock = 10;                  // ✗ CS0272: private setter
        // var p = new ProductAfter();          // ✗ CS9035: required 'Name' atanmadı
        product.AddStock(5);
        Console.WriteLine($"   AddStock(5) → Stock={product.Stock}, InStock={product.InStock} (computed property)");
        try { product.Remove(99); }
        catch (InvalidOperationException ex) { Console.WriteLine($"   Remove(99) → {ex.Message}"); }

        Console.WriteLine("\nC) Field → property değişikliği BINARY BREAKING CHANGE'dir");
        var fieldInfo = typeof(ProductBefore).GetField("Name");
        var propertyInfo = typeof(ProductAfter).GetProperty("Name");
        Console.WriteLine($"   ProductBefore.Name: FIELD (IL: ldfld)       → {fieldInfo is not null}");
        Console.WriteLine($"   ProductAfter.Name : PROPERTY (IL: call get_Name) → {propertyInfo is not null}");
        Console.WriteLine("   Kaynak kodu AYNI görünür (product.Name) ama IL farklıdır: field'a derlenmiş eski bir assembly yeni sürümle çalışırken");
        Console.WriteLine("   MissingFieldException alır. Kütüphane yazarken baştan property kullanın (auto-property: { get; set; } ücretsizdir).");
        Console.WriteLine("   Ayrıca field'a ref/out geçilebilir (int.TryParse(s, out obj.Count)), property'ye geçilemez (CS0206).");

        Console.WriteLine("\nD) Auto-property'nin arkasında derleyici bir backing field üretir (lowering → M2.1)");
        var backing = typeof(ProductAfter).GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                                          .Select(f => f.Name);
        Console.WriteLine($"   ProductAfter özel alanları: {string.Join(", ", backing)}");

        Console.WriteLine("\nE) readonly alan vs { get; }");
        Console.WriteLine($"   CreatedAtField (alan) → {typeof(ProductAfter).GetField(nameof(ProductAfter.CreatedAtField))!.IsInitOnly}; CreatedAtProperty (property) → get_CreatedAtProperty metodu: {typeof(ProductAfter).GetProperty(nameof(ProductAfter.CreatedAtProperty))!.GetMethod!.Name}");
        Console.WriteLine($"   static property: {nameof(ProductAfter.CreatedCount)} = {ProductAfter.CreatedCount}");

        Console.WriteLine("""

        Property vs metot: property UCUZ, yan etkisiz ve tekrar okunduğunda aynı sonucu veren bir "durum" gibi davranmalıdır.
        DB/ağ çağrısı yapan, uzun süren, her çağrıda farklı sonuç veren şeyler METOT olmalıdır (GetOrders(), ComputeTotal()).
        get / set / init / private set / computed (=>) / required — hepsi aynı kavramın dozlarıdır: ne kadar mutasyona izin veriyorsun?
        """);
    }
}
