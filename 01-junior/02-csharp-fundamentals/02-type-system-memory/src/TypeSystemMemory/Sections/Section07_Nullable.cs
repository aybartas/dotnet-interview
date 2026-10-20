using System.Reflection;

namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section07_Nullable
{
    public static void Run()
    {
        Console.WriteLine("A) NULLABLE VALUE TYPES: int? = Nullable<int> (GERÇEK bir tip; struct)");
        int? maybe = null;
        int? some = 7;
        Console.WriteLine($"   maybe.HasValue={maybe.HasValue}, some.Value={some.Value}, maybe.GetValueOrDefault(-1)={maybe.GetValueOrDefault(-1)}, typeof(int?)={typeof(int?)}");
#pragma warning disable CS8629   // derleyici de uyarıyor: bilerek null.Value okuyoruz
        try { _ = maybe.Value; }
        catch (InvalidOperationException) { Console.WriteLine("   null.Value → InvalidOperationException ('Nullable object must have a value')"); }
#pragma warning restore CS8629

        // Lifted operators: null içeren işlem null döner; karşılaştırmalar false döner
        Console.WriteLine($"   null + 1 = {(maybe + 1)?.ToString() ?? "null"},  null < 5 = {maybe < 5},  null == null = {maybe == null},  null > 5 || null <= 5 = {maybe > 5 || maybe <= 5}  (!)");

        // Boxing: null Nullable → null referans; dolu Nullable → boxed T (Nullable<T> kutulanmaz!)
        object? boxedNull = maybe;
        object? boxedSome = some;
        Console.WriteLine($"   boxing: (object)null int? → {(boxedNull is null ? "null referans" : "?")}, (object)7 int? → {boxedSome!.GetType()} (Nullable<int> değil, Int32)");

        Console.WriteLine("\nB) NULLABLE REFERENCE TYPES (string?): SADECE derleme zamanı ipucu — çalışma zamanında İZ YOK");
        Console.WriteLine("   typeof(string?) yazılamaz bile (CS8639): çalışma zamanında 'string?' diye ayrı bir tip YOK; ? yalnızca derleyici analizi için.");
        var prop = typeof(Sample).GetProperty(nameof(Sample.Name))!;
        var info = new NullabilityInfoContext().Create(prop);
        Console.WriteLine($"   Yine de metadata'da [Nullable] attribute'u olarak saklanır: Sample.Name okuma nullability = {info.ReadState} (reflection ile okunabilir; çalışma zamanı ENFORCE ETMEZ)");
        string notNullable = null!;                                       // '!' derleyiciyi susturur; çalışma zamanında hiçbir şey yakalanmaz
        Console.WriteLine($"   string x = null! → derleme uyarısı yok, x is null: {notNullable is null}   (null-forgiving yalnızca sözdür)");

        Console.WriteLine("\nC) NULL OPERATÖRLERİ");
        string? name = null;
        int[]? numbers = null;
        Console.WriteLine($"   name?.Length            = {name?.Length.ToString() ?? "null"}   (null-conditional: null ise zinciri keser, sonuç null)");
        Console.WriteLine($"   numbers?[0]             = {numbers?[0].ToString() ?? "null"}   (null-conditional indexer)");
        Console.WriteLine($"   name ?? \"anonim\"        = {name ?? "anonim"}   (null-coalescing)");
        name ??= "varsayılan";                                            // null-coalescing assignment: yalnızca null ise ata
        Console.WriteLine($"   name ??= \"varsayılan\"   → {name}");
        name ??= "bunu görmeyeceksin";
        Console.WriteLine($"   ikinci ??=              → {name} (zaten doluydu, atanmadı)");
        Console.WriteLine($"   ?. zinciri ve ??        : {Describe(null)} / {Describe(new Order(new Customer("Zeynep", null)))} / {Describe(new Order(new Customer("Ali", new Address("Ankara"))))}");
    }

    private static string Describe(Order? order) =>
        order?.Customer?.Address?.City ?? "adres yok";                    // 3 seviye null kontrolünü tek satıra indirir

    private sealed class Sample { public string? Name { get; set; } }
    private sealed record Order(Customer? Customer);
    private sealed record Customer(string Name, Address? Address);
    private sealed record Address(string City);
}
