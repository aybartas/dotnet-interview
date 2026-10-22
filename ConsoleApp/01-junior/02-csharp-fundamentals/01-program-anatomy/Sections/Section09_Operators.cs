namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section09_Operators
{
    public static void Run()
    {
        // PRECEDENCE (high → low, abbreviated): () · x++ · ! ~ ++x · * / % · + - · << >> · < > <= >= is as · == != · & · ^ · | · && · || · ?? · ?: · = (assignment)
        Console.WriteLine($"2 + 3 * 4 = {2 + 3 * 4}      (2 + 3) * 4 = {(2 + 3) * 4}");
        Console.WriteLine($"true || false && false = {true || (false && false)}   (&& binds tighter than ||: when in doubt use PARENTHESES)");

        // Integer division and modulo
        Console.WriteLine($"7 / 2 = {7 / 2}   7 / 2.0 = {7 / 2.0}   7 % 3 = {7 % 3}   -7 % 3 = {-7 % 3}   (the sign of the modulo follows the LEFT operand)");

        // SHORT-CIRCUIT: && and || DO NOT evaluate the right side unless needed. & and | (the logical form) evaluate BOTH sides.
        Console.WriteLine("\n&& (short-circuit):");
        var r1 = Left(false) && Right(true);
        Console.WriteLine($"  result = {r1}   → Right was not called\n& (non-short-circuit):");
        var r2 = Left(false) & Right(true);
        Console.WriteLine($"  result = {r2}   → Right was called anyway");

        // The real benefit of short-circuit: the null guard
        string? text = null;
        if (text != null && text.Length > 0)              // if text is null, Length is never touched → no NullReferenceException
            Console.WriteLine("non-empty");
        else
            Console.WriteLine("\nnull guard: text != null && text.Length > 0 → safe (false for null)");
        // if (text != null & text.Length > 0)            // ✗ with & text.Length is still evaluated → NullReferenceException

        // Bitwise & | ^ ~ << >> (on integers): the basis of flag operations
        const int Read = 0b001, Write = 0b010, Exec = 0b100;
        var perms = Read | Write;
        Console.WriteLine($"\nperms = {Convert.ToString(perms, 2).PadLeft(3, '0')}  has Write? {(perms & Write) != 0}  has Exec? {(perms & Exec) != 0}  remove Write: {Convert.ToString(perms & ~Write, 2).PadLeft(3, '0')}");

        // prefix/postfix ++ difference
        var n = 5;
        Console.WriteLine($"n=5: n++ → {n++} (then n={n}),  ++n → {++n}");

        // null coalescing
        string? name = null;
        Console.WriteLine($"name ?? \"anonymous\" = {name ?? "anonymous"}");
    }

    private static bool Left(bool v) { Console.WriteLine($"  Left({v}) called"); return v; }
    private static bool Right(bool v) { Console.WriteLine($"  Right({v}) called"); return v; }
}
