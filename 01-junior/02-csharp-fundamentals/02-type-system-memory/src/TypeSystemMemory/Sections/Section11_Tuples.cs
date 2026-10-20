namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section11_Tuples
{
    public static void Run()
    {
        // ValueTuple: hafif, adlandırılabilir, VALUE TYPE (struct) çok değerli dönüş
        (int Min, int Max) range = MinMax([4, 9, 2, 7]);
        Console.WriteLine($"adlandırılmış eleman: Min={range.Min}, Max={range.Max}; arka planda Item1/Item2: {range.Item1}/{range.Item2}");
        Console.WriteLine($"tip: {range.GetType()}   → System.ValueTuple<int,int> (struct; eski System.Tuple<> bir CLASS idi)");

        // Deconstruction: parçalara ayırma
        var (min, max) = MinMax([4, 9, 2, 7]);
        var (name, _, city) = ("Ayşe", 30, "Ankara");                // _ = discard
        Console.WriteLine($"deconstruct: min={min}, max={max}; name={name}, city={city}");

        // Değişken takası
        var (x, y) = (1, 2);
        (x, y) = (y, x);
        Console.WriteLine($"swap: x={x}, y={y}");

        // Eşitlik: eleman eleman (==, != C# 7.3+)
        Console.WriteLine($"(1, \"a\") == (1, \"a\") → {(1, "a") == (1, "a")}");

        // Kendi tipinizi deconstruct edilebilir yapın
        var p = new Person("Ali", "Veli", 25);
        var (first, last, age) = p;
        Console.WriteLine($"Deconstruct metodu: {first} {last} {age}");

        // Tuple vs record: tuple isimleri DERLEME zamanı süsüdür (çalışma zamanı metadata'sında yok), API sınırında record/class daha iyi
        var t1 = (Ad: "A", Yas: 1);
        (string Isim, int Boy) t2 = t1;                              // isimler uyuşmasa da yapı uyuşuyorsa atanır (isimler uyarı verebilir)
        Console.WriteLine($"isimler derleme zamanı: t1.Ad → t2.Isim = {t2.Isim}");

        // Mutable: ValueTuple alanları değiştirilebilir bir struct → Section04 tuzakları geçerli
        Console.WriteLine("\nNe zaman? Özel/yerel çok değerli dönüş → tuple. Public API sınırı, anlamlı domain kavramı → record/class (isimli tip, belgeleme, evrim kolaylığı).");
    }

    private static (int Min, int Max) MinMax(int[] values) => (values.Min(), values.Max());

    private sealed record Person(string First, string Last, int Age)
    {
        public void Deconstruct(out string first, out string last, out int age) => (first, last, age) = (First, Last, Age);
    }
}
