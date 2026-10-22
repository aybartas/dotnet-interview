namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section12_Enums
{
    public static void Run()
    {
        Console.WriteLine("enum = adlandırılmış tamsayı sabitleri (varsayılan taban tip int; byte/short/long da olabilir)");
        var status = OrderStatus.Shipped;
        Console.WriteLine($"   {status} = {(int)status}; sizeof underlying = {sizeof(int)}; Enum.GetUnderlyingType = {Enum.GetUnderlyingType(typeof(OrderStatus)).Name}");

        // TUZAK: Enum her tamsayıdan cast edilebilir — GEÇERSİZ değer üretebilirsiniz!
        var bogus = (OrderStatus)99;
        Console.WriteLine($"\n(OrderStatus)99 → \"{bogus}\"; Enum.IsDefined = {Enum.IsDefined(bogus)}   (cast exception ATMAZ)");

        // Parse: TryParse'ı tercih edin; sayısal metin ("99") de kabul edilir → IsDefined ile doğrulayın
        Console.WriteLine($"\nEnum.TryParse(\"shipped\", ignoreCase: true) → {Enum.TryParse<OrderStatus>("shipped", true, out var parsed)} ({parsed})");
        Console.WriteLine($"Enum.TryParse(\"99\")                         → {Enum.TryParse<OrderStatus>("99", out var numeric)} ({numeric}) ← ✗ geçersiz ama 'başarılı'!  IsDefined: {Enum.IsDefined(numeric)}");
        Console.WriteLine($"Enum.TryParse(\"Bilinmeyen\")                  → {Enum.TryParse<OrderStatus>("Bilinmeyen", out _)}");

        // switch expression + default: yeni enum değeri eklenince/geçersiz değerde çalışma zamanında patlayan dal bırakın
        Console.WriteLine($"\nswitch: {Label(OrderStatus.Pending)}, {Label(OrderStatus.Shipped)}");
        try { Label((OrderStatus)99); }
        catch (ArgumentOutOfRangeException ex) { Console.WriteLine($"switch default dalı: {ex.GetType().Name} (geçersiz değeri sessizce yutmak yerine ÖNE ÇIKARIN)"); }

        Console.WriteLine("\n[Flags]: bit alanı olarak birleştirilebilir enum — değerler 2'nin kuvveti olmalı");
        var perm = Permissions.Read | Permissions.Write;
        Console.WriteLine($"   Read|Write = {(int)perm} → ToString: {perm}   (FlagsAttribute olmasaydı '3' yazardı)");
        Console.WriteLine($"   HasFlag(Write)={perm.HasFlag(Permissions.Write)}  (perm & Execute) != 0 → {(perm & Permissions.Execute) != 0}");
        perm |= Permissions.Execute;                       // bayrak ekle
        perm &= ~Permissions.Write;                        // bayrak kaldır
        Console.WriteLine($"   |= Execute, &= ~Write → {perm}");
        Console.WriteLine($"   Permissions.All = {Permissions.All} ({(int)Permissions.All}); None = {Permissions.None}  (0 değeri için 'None' tanımlayın)");
        Console.WriteLine($"   Enum.TryParse(\"Read, Execute\") → {Enum.TryParse<Permissions>("Read, Execute", out var multi)} ({multi})");
    }

    private static string Label(OrderStatus s) => s switch
    {
        OrderStatus.Pending => "beklemede",
        OrderStatus.Shipped => "kargoda",
        OrderStatus.Delivered => "teslim edildi",
        _ => throw new ArgumentOutOfRangeException(nameof(s), s, "tanımsız durum"),
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
