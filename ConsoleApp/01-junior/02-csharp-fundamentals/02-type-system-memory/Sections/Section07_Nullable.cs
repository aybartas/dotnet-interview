using System.Reflection;

namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section07_Nullable
{
    public static void Run()
    {
        Console.WriteLine("A) NULLABLE VALUE TYPES: int? = Nullable<int> (a REAL type; a struct)");
        int? maybe = null;
        int? some = 7;
        Console.WriteLine($"   maybe.HasValue={maybe.HasValue}, some.Value={some.Value}, maybe.GetValueOrDefault(-1)={maybe.GetValueOrDefault(-1)}, typeof(int?)={typeof(int?)}");
#pragma warning disable CS8629   // the compiler warns too: we deliberately read null.Value
        try { _ = maybe.Value; }
        catch (InvalidOperationException) { Console.WriteLine("   null.Value → InvalidOperationException ('Nullable object must have a value')"); }
#pragma warning restore CS8629

        // Lifted operators: an operation involving null returns null; comparisons return false
        Console.WriteLine($"   null + 1 = {(maybe + 1)?.ToString() ?? "null"},  null < 5 = {maybe < 5},  null == null = {maybe == null},  null > 5 || null <= 5 = {maybe > 5 || maybe <= 5}  (!)");

        // Boxing: a null Nullable → null reference; a non-null Nullable → boxed T (Nullable<T> is not boxed!)
        object? boxedNull = maybe;
        object? boxedSome = some;
        Console.WriteLine($"   boxing: (object)null int? → {(boxedNull is null ? "null reference" : "?")}, (object)7 int? → {boxedSome!.GetType()} (Int32, not Nullable<int>)");

        Console.WriteLine("\nB) NULLABLE REFERENCE TYPES (string?): ONLY a compile-time hint — NO trace at run time");
        Console.WriteLine("   typeof(string?) can't even be written (CS8639): there is NO separate 'string?' type at run time; ? is only for compiler analysis.");
        var prop = typeof(Sample).GetProperty(nameof(Sample.Name))!;
        var info = new NullabilityInfoContext().Create(prop);
        Console.WriteLine($"   Still, it is stored in the metadata as a [Nullable] attribute: Sample.Name read nullability = {info.ReadState} (readable via reflection; the runtime does NOT ENFORCE it)");
        string notNullable = null!;                                       // '!' silences the compiler; nothing is caught at run time
        Console.WriteLine($"   string x = null! → no compiler warning, x is null: {notNullable is null}   (null-forgiving is just talk)");

        Console.WriteLine("\nC) NULL OPERATORS");
        string? name = null;
        int[]? numbers = null;
        Console.WriteLine($"   name?.Length            = {name?.Length.ToString() ?? "null"}   (null-conditional: if null, short-circuits the chain, the result is null)");
        Console.WriteLine($"   numbers?[0]             = {numbers?[0].ToString() ?? "null"}   (null-conditional indexer)");
        Console.WriteLine($"   name ?? \"anonymous\"     = {name ?? "anonymous"}   (null-coalescing)");
        name ??= "default";                                               // null-coalescing assignment: assigns only if null
        Console.WriteLine($"   name ??= \"default\"      → {name}");
        name ??= "you won't see this";
        Console.WriteLine($"   second ??=              → {name} (it was already set, not assigned)");
        Console.WriteLine($"   ?. chain and ??         : {Describe(null)} / {Describe(new Order(new Customer("Alice", null)))} / {Describe(new Order(new Customer("Bob", new Address("London"))))}");
    }

    private static string Describe(Order? order) =>
        order?.Customer?.Address?.City ?? "no address";                   // reduces 3 levels of null checks to one line

    private sealed class Sample { public string? Name { get; set; } }
    private sealed record Order(Customer? Customer);
    private sealed record Customer(string Name, Address? Address);
    private sealed record Address(string City);
}
