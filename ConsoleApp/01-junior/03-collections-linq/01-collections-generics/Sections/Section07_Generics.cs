using System.Reflection;
using Junior.CollectionsLinq.CollectionsGenerics.Models;

namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class Section07_Generics
{
    public static void Run()
    {
        Console.WriteLine("Generics NEDEN var?  (1) TİP GÜVENLİĞİ — derleme zamanında: List<int>'e string eklenemez (ArrayList'e eklenir, patlar çalışırken)");
        Console.WriteLine("                     (2) BOXING'siz PERFORMANS — List<int> int'leri doğrudan tutar (ArrayList kutular, → J2.2)");
        Console.WriteLine("                     (3) KOD TEKRARINI önler — tek algoritma, her tip için (JIT, her değer tipi için ayrı özelleşmiş kod üretir)\n");

        Console.WriteLine("Generic metotlar ve constraint'ler:");
        Console.WriteLine($"   Max([3,9,2])             = {GenericUtils.Max([3, 9, 2])}          (where T : IComparable<T>)");
        Console.WriteLine($"   Max([\"b\",\"z\",\"a\"])       = {GenericUtils.Max(["b", "z", "a"])}          (aynı metot, string için)");
        Console.WriteLine($"   CreateDefault<List<int>> = {GenericUtils.CreateDefault<List<int>>().GetType().Name}   (where T : new())");
        Console.WriteLine($"   FirstOrNull<int>([])     = {GenericUtils.FirstOrNull(Array.Empty<int>())?.ToString() ?? "null"}          (where T : struct → int? döner)");
        Console.WriteLine($"   CountBy(\"a b a\")         = {string.Join(", ", GenericUtils.CountBy("a b a".Split(' ')).Select(kv => $"{kv.Key}:{kv.Value}"))}   (where T : notnull)");
        int x = 1, y = 2; GenericUtils.Swap(ref x, ref y);
        Console.WriteLine($"   Swap(1,2)                → {x},{y}");

        Console.WriteLine("\nConstraint'leri reflection ile okumak:");
        foreach (var m in typeof(GenericUtils).GetMethods(BindingFlags.Public | BindingFlags.Static).Where(m => m.IsGenericMethodDefinition && m.Name != "Swap").OrderBy(m => m.Name))
        {
            var p = m.GetGenericArguments()[0];
            var parts = new List<string>();
            var attrs = p.GenericParameterAttributes;
            if (attrs.HasFlag(GenericParameterAttributes.ReferenceTypeConstraint)) parts.Add("class");
            if (attrs.HasFlag(GenericParameterAttributes.NotNullableValueTypeConstraint)) parts.Add("struct");
            if (attrs.HasFlag(GenericParameterAttributes.DefaultConstructorConstraint) && !attrs.HasFlag(GenericParameterAttributes.NotNullableValueTypeConstraint)) parts.Add("new()");
            parts.AddRange(p.GetGenericParameterConstraints().Where(c => c != typeof(ValueType)).Select(c => c.Name.Replace("`1", "<T>")));
            Console.WriteLine($"   {m.Name,-14} where T : {(parts.Count == 0 ? "(notnull — nullable annotation)" : string.Join(", ", parts))}");
        }

        Console.WriteLine("\nGeneric Repository<T> taslağı (aynı sınıf, farklı varlıklar, farklı anahtar tipleri):");
        var products = new InMemoryRepository<Product, int>();
        products.Add(new Product(1, "Kalem", 12.5m));
        products.Add(new Product(2, "Defter", 40m));
        var customers = new InMemoryRepository<Customer, Guid>();
        var ayse = new Customer(Guid.NewGuid(), "Ayşe");
        customers.Add(ayse);
        Console.WriteLine($"   products.Get(2) = {products.Get(2)}   ·   products.Where(Price > 20) = [{string.Join(", ", products.Where(p => p.Price > 20).Select(p => p.Name))}]");
        Console.WriteLine($"   customers.Find(ayse.Id) = {customers.Find(ayse.Id)?.Name}   ·   products.Find(99) = {(products.Find(99) is null ? "null" : "?")}");
        try { products.Add(new Product(1, "Kopya", 1m)); }
        catch (InvalidOperationException ex) { Console.WriteLine($"   tekrar Id → {ex.Message}"); }
        // products.Add(ayse);                       // ✗ derleme hatası: Customer, InMemoryRepository<Product,int> için geçersiz (tip güvenliği)

        Console.WriteLine("""

        Temel constraint'ler:  where T : class | struct | notnull | new() | BaseClass | IInterface | T2 (başka tip parametresi) | unmanaged
        Constraint ne işe yarar?  T hakkında derleyiciye GARANTİ verir → o garantinin izin verdiği üyeleri (CompareTo, new T(), null atama…) kullanabilirsiniz.
        Kısıtsız T hakkında hiçbir şey bilinmez (yalnızca object üyeleri). Aşırı kısıtlamak yeniden kullanılabilirliği düşürür.
        Generic variance (in/out) ve generic math (INumber<T>) → M1.2.
        """);
    }
}
