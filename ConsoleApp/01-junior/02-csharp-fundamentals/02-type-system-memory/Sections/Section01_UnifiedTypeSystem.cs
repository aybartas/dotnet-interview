namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section01_UnifiedTypeSystem
{
    public static void Run()
    {
        // In C# EVERYTHING derives from System.Object — value types included. 'int' is just an alias for System.Int32.
        Console.WriteLine($"typeof(int) == typeof(Int32): {typeof(int) == typeof(Int32)}");
        Console.WriteLine($"Method call on 5           : 5.ToString() = \"{5.ToString()}\", 5.GetType() = {5.GetType()}");

        // Inheritance chains
        Console.WriteLine($"\nint    chain: {Chain(typeof(int))}");
        Console.WriteLine($"string chain: {Chain(typeof(string))}");
        Console.WriteLine($"enum   chain: {Chain(typeof(DayOfWeek))}");
        Console.WriteLine($"int[]  chain: {Chain(typeof(int[]))}");
        Console.WriteLine($"delegate chain: {Chain(typeof(Action))}");

        // Result: object is a "common denominator" → you can assign anything to object (but value types need BOXING → Section 5)
        object[] bag = [42, "text", 3.14, new DateTime(2026, 1, 1), DayOfWeek.Monday, (1, 2)];
        foreach (var item in bag)
            Console.WriteLine($"  {item,-22} → {item.GetType().Name,-14} value type? {item.GetType().IsValueType}");
    }

    private static string Chain(Type? t)
    {
        var parts = new List<string>();
        for (; t is not null; t = t.BaseType) parts.Add(t.Name);
        return string.Join(" → ", parts);
    }
}
