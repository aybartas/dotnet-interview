using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section01_Properties
{
    public static void Run()
    {
        Console.WriteLine("A) BEFORE (public field): falling into an invalid state is allowed");
        var before = new ProductBefore { Name = "Pen", Price = -5, Stock = -100 };
        Console.WriteLine($"   Price = {before.Price}, Stock = {before.Stock}   ← negative price/stock; nobody could prevent it");

        Console.WriteLine("\nB) AFTER (property): the invariant is protected");
        var product = new ProductAfter { Name = "Pen", Price = 12.5m };              // required + init
        try { product.Price = -1; }
        catch (ArgumentOutOfRangeException ex) { Console.WriteLine($"   Price = -1 → {ex.GetType().Name}: {ex.ParamName}"); }
        // product.Name = "x";                  // ✗ CS8852: init-only — cannot be assigned after the object is created
        // product.Stock = 10;                  // ✗ CS0272: private setter
        // var p = new ProductAfter();          // ✗ CS9035: required 'Name' was not assigned
        product.AddStock(5);
        Console.WriteLine($"   AddStock(5) → Stock={product.Stock}, InStock={product.InStock} (computed property)");
        try { product.Remove(99); }
        catch (InvalidOperationException ex) { Console.WriteLine($"   Remove(99) → {ex.Message}"); }

        Console.WriteLine("\nC) Changing a field to a property is a BINARY BREAKING CHANGE");
        var fieldInfo = typeof(ProductBefore).GetField("Name");
        var propertyInfo = typeof(ProductAfter).GetProperty("Name");
        Console.WriteLine($"   ProductBefore.Name: FIELD (IL: ldfld)       → {fieldInfo is not null}");
        Console.WriteLine($"   ProductAfter.Name : PROPERTY (IL: call get_Name) → {propertyInfo is not null}");
        Console.WriteLine("   The source code LOOKS the same (product.Name) but the IL differs: an old assembly compiled against the field");
        Console.WriteLine("   gets MissingFieldException when it runs against the new version. When writing a library, use properties from the start (auto-property: { get; set; } is free).");
        Console.WriteLine("   Also, a field can be passed by ref/out (int.TryParse(s, out obj.Count)), a property cannot (CS0206).");

        Console.WriteLine("\nD) Behind an auto-property the compiler generates a backing field (lowering → M2.1)");
        var backing = typeof(ProductAfter).GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                                          .Select(f => f.Name);
        Console.WriteLine($"   ProductAfter private fields: {string.Join(", ", backing)}");

        Console.WriteLine("\nE) readonly field vs { get; }");
        Console.WriteLine($"   CreatedAtField (field) → {typeof(ProductAfter).GetField(nameof(ProductAfter.CreatedAtField))!.IsInitOnly}; CreatedAtProperty (property) → get_CreatedAtProperty method: {typeof(ProductAfter).GetProperty(nameof(ProductAfter.CreatedAtProperty))!.GetMethod!.Name}");
        Console.WriteLine($"   static property: {nameof(ProductAfter.CreatedCount)} = {ProductAfter.CreatedCount}");

        Console.WriteLine("""

        Property vs method: a property should behave like CHEAP, side-effect-free "state" that gives the same result when read again.
        Things that call the DB/network, take a long time, or give a different result on every call should be METHODS (GetOrders(), ComputeTotal()).
        get / set / init / private set / computed (=>) / required — all are doses of the same idea: how much mutation do you allow?
        """);
    }
}
