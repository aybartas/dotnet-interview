namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section12_Enums
{
    public static void Run()
    {
        Console.WriteLine("enum = named integer constants (the default underlying type is int; byte/short/long are possible too)");
        var status = OrderStatus.Shipped;
        Console.WriteLine($"   {status} = {(int)status}; sizeof underlying = {sizeof(int)}; Enum.GetUnderlyingType = {Enum.GetUnderlyingType(typeof(OrderStatus)).Name}");

        // PITFALL: an enum can be cast from any integer — you can produce an INVALID value!
        var bogus = (OrderStatus)99;
        Console.WriteLine($"\n(OrderStatus)99 → \"{bogus}\"; Enum.IsDefined = {Enum.IsDefined(bogus)}   (the cast does NOT throw)");

        // Parse: prefer TryParse; numeric text ("99") is also accepted → validate with IsDefined
        Console.WriteLine($"\nEnum.TryParse(\"shipped\", ignoreCase: true) → {Enum.TryParse<OrderStatus>("shipped", true, out var parsed)} ({parsed})");
        Console.WriteLine($"Enum.TryParse(\"99\")                         → {Enum.TryParse<OrderStatus>("99", out var numeric)} ({numeric}) ← ✗ invalid but 'successful'!  IsDefined: {Enum.IsDefined(numeric)}");
        Console.WriteLine($"Enum.TryParse(\"Unknown\")                    → {Enum.TryParse<OrderStatus>("Unknown", out _)}");

        // switch expression + default: leave a branch that blows up at run time when a new enum value is added / on an invalid value
        Console.WriteLine($"\nswitch: {Label(OrderStatus.Pending)}, {Label(OrderStatus.Shipped)}");
        try { Label((OrderStatus)99); }
        catch (ArgumentOutOfRangeException ex) { Console.WriteLine($"switch default branch: {ex.GetType().Name} (SURFACE the invalid value instead of silently swallowing it)"); }

        Console.WriteLine("\n[Flags]: an enum that can be combined as a bit field — values must be powers of 2");
        var perm = Permissions.Read | Permissions.Write;
        Console.WriteLine($"   Read|Write = {(int)perm} → ToString: {perm}   (without FlagsAttribute it would print '3')");
        Console.WriteLine($"   HasFlag(Write)={perm.HasFlag(Permissions.Write)}  (perm & Execute) != 0 → {(perm & Permissions.Execute) != 0}");
        perm |= Permissions.Execute;                       // add a flag
        perm &= ~Permissions.Write;                        // remove a flag
        Console.WriteLine($"   |= Execute, &= ~Write → {perm}");
        Console.WriteLine($"   Permissions.All = {Permissions.All} ({(int)Permissions.All}); None = {Permissions.None}  (define 'None' for the value 0)");
        Console.WriteLine($"   Enum.TryParse(\"Read, Execute\") → {Enum.TryParse<Permissions>("Read, Execute", out var multi)} ({multi})");
    }

    private static string Label(OrderStatus s) => s switch
    {
        OrderStatus.Pending => "pending",
        OrderStatus.Shipped => "shipped",
        OrderStatus.Delivered => "delivered",
        _ => throw new ArgumentOutOfRangeException(nameof(s), s, "undefined status"),
    };

    private enum OrderStatus { Pending = 1, Shipped = 2, Delivered = 3 }

    [Flags]
    private enum Permissions
    {
        None = 0,
        Read = 1,          // 0001
        Write = 2,         // 0010
        Execute = 4,       // 0100
        All = Read | Write | Execute,
    }
}
