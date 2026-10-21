namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section01_UnifiedTypeSystem
{
    public static void Run()
    {
        // C#'ta HER ŞEY System.Object'ten türer — value type'lar dahil. 'int' sadece System.Int32'nin takma adıdır (alias).
        Console.WriteLine($"typeof(int) == typeof(Int32): {typeof(int) == typeof(Int32)}");
        Console.WriteLine($"5 üzerinde metot çağrısı   : 5.ToString() = \"{5.ToString()}\", 5.GetType() = {5.GetType()}");

        // Kalıtım zincirleri
        Console.WriteLine($"\nint    zinciri: {Chain(typeof(int))}");
        Console.WriteLine($"string zinciri: {Chain(typeof(string))}");
        Console.WriteLine($"enum   zinciri: {Chain(typeof(DayOfWeek))}");
        Console.WriteLine($"int[]  zinciri: {Chain(typeof(int[]))}");
        Console.WriteLine($"delegate zinciri: {Chain(typeof(Action))}");

        // Sonuç: object bir "ortak payda"dır → her şeyi object'e atayabilirsiniz (ama value type'lar için BOXING gerekir → Bölüm 5)
        object[] bag = [42, "metin", 3.14, new DateTime(2026, 1, 1), DayOfWeek.Monday, (1, 2)];
        foreach (var item in bag)
            Console.WriteLine($"  {item,-22} → {item.GetType().Name,-14} value type mı? {item.GetType().IsValueType}");
    }

    private static string Chain(Type? t)
    {
        var parts = new List<string>();
        for (; t is not null; t = t.BaseType) parts.Add(t.Name);
        return string.Join(" → ", parts);
    }
}
