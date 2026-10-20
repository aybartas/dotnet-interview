namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section09_Operators
{
    public static void Run()
    {
        // ÖNCELİK (yüksek → düşük, kısaltılmış): () · x++ · ! ~ ++x · * / % · + - · << >> · < > <= >= is as · == != · & · ^ · | · && · || · ?? · ?: · = (atama)
        Console.WriteLine($"2 + 3 * 4 = {2 + 3 * 4}      (2 + 3) * 4 = {(2 + 3) * 4}");
        Console.WriteLine($"true || false && false = {true || (false && false)}   (&& , || 'dan önce: şüphede PARANTEZ kullanın)");

        // Tamsayı bölme ve mod
        Console.WriteLine($"7 / 2 = {7 / 2}   7 / 2.0 = {7 / 2.0}   7 % 3 = {7 % 3}   -7 % 3 = {-7 % 3}   (mod işareti SOL operandı izler)");

        // SHORT-CIRCUIT: && ve || sağ tarafı gerekmedikçe DEĞERLENDİRMEZ. & ve | (mantıksal hali) İKİ tarafı da değerlendirir.
        Console.WriteLine("\n&& (short-circuit):");
        var r1 = Left(false) && Right(true);
        Console.WriteLine($"  sonuç = {r1}   → Right çağrılmadı\n& (non-short-circuit):");
        var r2 = Left(false) & Right(true);
        Console.WriteLine($"  sonuç = {r2}   → Right yine de çağrıldı");

        // Short-circuit'in asıl faydası: null-guard
        string? text = null;
        if (text != null && text.Length > 0)              // text null ise Length'e hiç dokunulmaz → NullReferenceException yok
            Console.WriteLine("dolu");
        else
            Console.WriteLine("\nnull-guard: text != null && text.Length > 0 → güvenli (null için false)");
        // if (text != null & text.Length > 0)            // ✗ & ile text.Length yine değerlendirilir → NullReferenceException

        // Bitwise & | ^ ~ << >> (tamsayılarda): bayrak (flags) işlemlerinin temeli
        const int Read = 0b001, Write = 0b010, Exec = 0b100;
        var perms = Read | Write;
        Console.WriteLine($"\nperms = {Convert.ToString(perms, 2).PadLeft(3, '0')}  Write var mı? {(perms & Write) != 0}  Exec var mı? {(perms & Exec) != 0}  Write kaldır: {Convert.ToString(perms & ~Write, 2).PadLeft(3, '0')}");

        // ++ önek/sonek farkı
        var n = 5;
        Console.WriteLine($"n=5: n++ → {n++} (sonra n={n}),  ++n → {++n}");

        // null birleştirme
        string? name = null;
        Console.WriteLine($"name ?? \"anonim\" = {name ?? "anonim"}");
    }

    private static bool Left(bool v) { Console.WriteLine($"  Left({v}) çağrıldı"); return v; }
    private static bool Right(bool v) { Console.WriteLine($"  Right({v}) çağrıldı"); return v; }
}
