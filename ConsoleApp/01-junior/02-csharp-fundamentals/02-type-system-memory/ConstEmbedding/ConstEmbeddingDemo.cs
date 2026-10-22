namespace Junior.CSharpFundamentals.TypeSystemMemory.ConstEmbedding;

/// <summary>
/// See const embedding in the IL: two methods look at Config the same way, but the compiler writes the VALUE into one and a FIELD READ into the other.
/// (In a real scenario Config lives in another assembly; the experiment with a second assembly is in the README.)
/// </summary>
public static class ConstEmbeddingDemo
{
    public static int ReadConst() => Config.MaxRetries;       // IL: ldc.i4.3 → 3 is embedded as a literal
    public static int ReadReadonly() => Config.Timeout;       // IL: ldsfld Timeout → the field is read at run time

    public static void Run()
    {
        Console.WriteLine($"Config.MaxRetries (const)        : {Config.MaxRetries}");
        Console.WriteLine($"Config.Timeout (static readonly) : {Config.Timeout}\n");

        Show(nameof(ReadConst));
        Show(nameof(ReadReadonly));

        Console.WriteLine("""

        const → the VALUE is written into the caller's IL; if Config were in another assembly and only that one were updated,
                the caller would keep using the OLD value until it is recompiled.
        static readonly → the caller reads the field every time; it sees the updated assembly's value.
        """);
    }

    private static void Show(string methodName)
    {
        var il = typeof(ConstEmbeddingDemo).GetMethod(methodName)!.GetMethodBody()!.GetILAsByteArray()!;
        var meaning = il[0] switch
        {
            >= 0x16 and <= 0x1E => $"ldc.i4.{il[0] - 0x16}  → {il[0] - 0x16} is written INTO the IL (embedded literal)",
            0x1F => $"ldc.i4.s {il[1]}  → {il[1]} is written INTO the IL (embedded literal)",
            0x7E => "ldsfld  → the static field is read at run time",
            _ => "?",
        };
        Console.WriteLine($"{methodName,-14} IL: {Convert.ToHexString(il),-14} {meaning}");
    }
}
