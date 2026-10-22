using System.Reflection;
using Junior.CollectionsLinq.CollectionsGenerics.Models;

namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class Section07_Generics
{
    public static void Run()
    {
        Console.WriteLine("WHY do generics exist?  (1) TYPE SAFETY — at compile time: you cannot add a string to a List<int> (you can to an ArrayList, and it blows up at runtime)");
        Console.WriteLine("                        (2) PERFORMANCE without BOXING — List<int> stores ints directly (ArrayList boxes them, → J2.2)");
        Console.WriteLine("                        (3) Avoids CODE DUPLICATION — one algorithm for every type (the JIT generates specialized code for each value type)\n");

        Console.WriteLine("Generic methods and constraints:");
        Console.WriteLine($"   Max([3,9,2])             = {GenericUtils.Max([3, 9, 2])}          (where T : IComparable<T>)");
        Console.WriteLine($"   Max([\"b\",\"z\",\"a\"])       = {GenericUtils.Max(["b", "z", "a"])}          (same method, for string)");
        Console.WriteLine($"   CreateDefault<List<int>> = {GenericUtils.CreateDefault<List<int>>().GetType().Name}   (where T : new())");
        Console.WriteLine($"   FirstOrNull<int>([])     = {GenericUtils.FirstOrNull(Array.Empty<int>())?.ToString() ?? "null"}          (where T : struct → returns int?)");
        Console.WriteLine($"   CountBy(\"a b a\")         = {string.Join(", ", GenericUtils.CountBy("a b a".Split(' ')).Select(kv => $"{kv.Key}:{kv.Value}"))}   (where T : notnull)");
        int x = 1, y = 2; GenericUtils.Swap(ref x, ref y);
        Console.WriteLine($"   Swap(1,2)                → {x},{y}");

        Console.WriteLine("\nReading the constraints via reflection:");
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

        Console.WriteLine("\nGeneric Repository<T> sketch (same class, different entities, different key types):");
        var products = new InMemoryRepository<Product, int>();
        products.Add(new Product(1, "Pen", 12.5m));
        products.Add(new Product(2, "Notebook", 40m));
        var customers = new InMemoryRepository<Customer, Guid>();
        var alice = new Customer(Guid.NewGuid(), "Alice");
        customers.Add(alice);
        Console.WriteLine(FormattableString.Invariant($"   products.Get(2) = {products.Get(2)}   ·   products.Where(Price > 20) = [{string.Join(", ", products.Where(p => p.Price > 20).Select(p => p.Name))}]"));
        Console.WriteLine($"   customers.Find(alice.Id) = {customers.Find(alice.Id)?.Name}   ·   products.Find(99) = {(products.Find(99) is null ? "null" : "?")}");
        try { products.Add(new Product(1, "Duplicate", 1m)); }
        catch (InvalidOperationException ex) { Console.WriteLine($"   duplicate Id → {ex.Message}"); }
        // products.Add(alice);                       // ✗ compile error: Customer is not valid for InMemoryRepository<Product,int> (type safety)

        Console.WriteLine("""

        Core constraints:  where T : class | struct | notnull | new() | BaseClass | IInterface | T2 (another type parameter) | unmanaged
        What does a constraint do?  It gives the compiler a GUARANTEE about T → you can use the members that guarantee allows (CompareTo, new T(), assigning null…).
        Nothing is known about an unconstrained T (only object members). Over-constraining reduces reusability.
        Generic variance (in/out) and generic math (INumber<T>) → M1.2.
        """);
    }
}
