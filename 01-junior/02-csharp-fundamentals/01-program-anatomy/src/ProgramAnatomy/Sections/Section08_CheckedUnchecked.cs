namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section08_CheckedUnchecked
{
    public static void Run()
    {
        // VARSAYILAN = UNCHECKED: taşma SESSİZCE sarar (wrap-around). Hata vermez, yanlış sonuç üretir!
        int max = int.MaxValue;                           // 2.147.483.647
        int wrapped = unchecked(max + 1);
        Console.WriteLine($"unchecked: int.MaxValue + 1 = {wrapped}   (negatif! sessiz taşma)");

        // checked: taşma OverflowException fırlatır
        try
        {
            int boom = checked(max + 1);
            Console.WriteLine(boom);
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"checked  : int.MaxValue + 1 → {ex.GetType().Name}");
        }

        // checked bloğu birden çok işlem için
        try
        {
            checked
            {
                var a = int.MaxValue;
                var b = a * 2;
                Console.WriteLine(b);
            }
        }
        catch (OverflowException) { Console.WriteLine("checked blok: a * 2 → OverflowException"); }

        // Dönüşümlerde de geçerli
        long big = 5_000_000_000;
        Console.WriteLine($"unchecked cast: (int){big} = {unchecked((int)big)}");
        try { _ = checked((int)big); }
        catch (OverflowException) { Console.WriteLine("checked   cast: (int)5_000_000_000 → OverflowException"); }

        // Taşmayı KAÇIRMAK gerçek hata sebebidir: toplam/çarpım hesaplayan kod
        int itemCount = 70_000;
        int pricePerItemInCents = 40_000;
        Console.WriteLine($"\n70.000 × 40.000 (int) = {itemCount * pricePerItemInCents}   (beklenen 2.800.000.000 → taştı, negatif!)");
        Console.WriteLine($"Çözüm 1: daha geniş tip → {(long)itemCount * pricePerItemInCents}");
        Console.WriteLine($"Çözüm 2: checked derleme/blok → OverflowException ile ANINDA görünür");

        // Math.Abs(int.MinValue) tuzağı: +2.147.483.648 int'e sığmaz
        try { _ = Math.Abs(int.MinValue); }
        catch (OverflowException) { Console.WriteLine("Math.Abs(int.MinValue) → OverflowException"); }

        Console.WriteLine("\nProje geneli: csproj'a <CheckForOverflowUnderflow>true</CheckForOverflowUnderflow> (varsayılan false; performans maliyeti küçük).");
        Console.WriteLine("Not: decimal HER ZAMAN checked davranır; float/double taşmada Infinity üretir, exception atmaz.");
    }
}
