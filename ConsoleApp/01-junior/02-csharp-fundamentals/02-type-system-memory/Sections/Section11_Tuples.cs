namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section11_Tuples
{
    public static void Run()
    {
        // ValueTuple: lightweight, nameable, VALUE TYPE (struct) multi-value return
        (int Min, int Max) range = MinMax([4, 9, 2, 7]);
        Console.WriteLine($"named elements: Min={range.Min}, Max={range.Max}; behind the scenes Item1/Item2: {range.Item1}/{range.Item2}");
        Console.WriteLine($"type: {range.GetType()}   → System.ValueTuple<int,int> (a struct; the old System.Tuple<> was a CLASS)");

        // Deconstruction: splitting into parts
        var (min, max) = MinMax([4, 9, 2, 7]);
        var (name, _, city) = ("Alice", 30, "London");               // _ = discard
        Console.WriteLine($"deconstruct: min={min}, max={max}; name={name}, city={city}");

        // Swapping variables
        var (x, y) = (1, 2);
        (x, y) = (y, x);
        Console.WriteLine($"swap: x={x}, y={y}");

        // Equality: element by element (==, != C# 7.3+)
        Console.WriteLine($"(1, \"a\") == (1, \"a\") → {(1, "a") == (1, "a")}");

        // Make your own type deconstructable
        var p = new Person("John", "Smith", 25);
        var (first, last, age) = p;
        Console.WriteLine($"Deconstruct method: {first} {last} {age}");

        // Tuple vs record: tuple names are COMPILE-time decoration (not in the run-time metadata); a record/class is better at an API boundary
        var t1 = (Name: "A", Age: 1);
        (string Title, int Height) t2 = t1;                          // assignable if the structure matches even if the names don't (the names may produce a warning)
        Console.WriteLine($"names are compile-time: t1.Name → t2.Title = {t2.Title}");

        // Mutable: ValueTuple fields are a mutable struct → the Section04 pitfalls apply
        Console.WriteLine("\nWhen? Private/local multi-value return → tuple. Public API boundary, meaningful domain concept → record/class (named type, documentation, easier evolution).");
    }

    private static (int Min, int Max) MinMax(int[] values) => (values.Min(), values.Max());

    private sealed record Person(string First, string Last, int Age)
    {
        public void Deconstruct(out string first, out string last, out int age) => (first, last, age) = (First, Last, Age);
    }
}
