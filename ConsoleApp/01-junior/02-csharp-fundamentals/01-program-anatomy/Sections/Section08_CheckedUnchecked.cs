namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section08_CheckedUnchecked
{
    public static void Run()
    {
        // DEFAULT = UNCHECKED: overflow SILENTLY wraps around. No error, just a wrong result!
        int max = int.MaxValue;                           // 2,147,483,647
        int wrapped = unchecked(max + 1);
        Console.WriteLine($"unchecked: int.MaxValue + 1 = {wrapped}   (negative! silent overflow)");

        // checked: overflow throws OverflowException
        try
        {
            int boom = checked(max + 1);
            Console.WriteLine(boom);
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"checked  : int.MaxValue + 1 → {ex.GetType().Name}");
        }

        // checked block for multiple operations
        try
        {
            checked
            {
                var a = int.MaxValue;
                var b = a * 2;
                Console.WriteLine(b);
            }
        }
        catch (OverflowException) { Console.WriteLine("checked block: a * 2 → OverflowException"); }

        // Applies to conversions as well
        long big = 5_000_000_000;
        Console.WriteLine($"unchecked cast: (int){big} = {unchecked((int)big)}");
        try { _ = checked((int)big); }
        catch (OverflowException) { Console.WriteLine("checked   cast: (int)5_000_000_000 → OverflowException"); }

        // MISSING an overflow is a real source of bugs: code that computes totals/products
        int itemCount = 70_000;
        int pricePerItemInCents = 40_000;
        Console.WriteLine($"\n70,000 × 40,000 (int) = {itemCount * pricePerItemInCents}   (expected 2,800,000,000 → overflowed, negative!)");
        Console.WriteLine($"Fix 1: a wider type → {(long)itemCount * pricePerItemInCents}");
        Console.WriteLine($"Fix 2: checked compilation/block → visible IMMEDIATELY via OverflowException");

        // The Math.Abs(int.MinValue) trap: +2,147,483,648 doesn't fit in an int
        try { _ = Math.Abs(int.MinValue); }
        catch (OverflowException) { Console.WriteLine("Math.Abs(int.MinValue) → OverflowException"); }

        Console.WriteLine("\nProject-wide: <CheckForOverflowUnderflow>true</CheckForOverflowUnderflow> in the csproj (default false; the performance cost is small).");
        Console.WriteLine("Note: decimal ALWAYS behaves as checked; float/double produce Infinity on overflow, they don't throw.");
    }
}
