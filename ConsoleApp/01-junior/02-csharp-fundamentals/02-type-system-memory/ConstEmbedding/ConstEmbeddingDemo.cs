namespace Junior.CSharpFundamentals.TypeSystemMemory.ConstEmbedding;

/// <summary>
/// const gömülmesini IL'de görün: iki metot aynı biçimde Config'e bakar, ama derleyici birine DEĞERİ yazar, diğerine ALAN OKUMASI.
/// (Gerçek senaryoda Config başka bir assembly'dedir; ikinci assembly'li deney README'de.)
/// </summary>
public static class ConstEmbeddingDemo
{
    public static int ReadConst() => Config.MaxRetries;       // IL: ldc.i4.3 → 3 literal olarak gömüldü
    public static int ReadReadonly() => Config.Timeout;       // IL: ldsfld Timeout → çalışırken alan okunur

    public static void Run()
    {
        Console.WriteLine($"Config.MaxRetries (const)        : {Config.MaxRetries}");
        Console.WriteLine($"Config.Timeout (static readonly) : {Config.Timeout}\n");

        Show(nameof(ReadConst));
        Show(nameof(ReadReadonly));

        Console.WriteLine("""

        const → çağıranın IL'ine DEĞER yazılır; Config başka bir assembly'de olsaydı ve yalnızca o güncellenseydi,
                çağıran yeniden derlenmediği sürece ESKİ değeri kullanmaya devam ederdi.
        static readonly → çağıran her seferinde alanı okur; güncellenen assembly'nin değerini görür.
        """);
    }

    private static void Show(string methodName)
    {
        var il = typeof(ConstEmbeddingDemo).GetMethod(methodName)!.GetMethodBody()!.GetILAsByteArray()!;
        var meaning = il[0] switch
        {
            >= 0x16 and <= 0x1E => $"ldc.i4.{il[0] - 0x16}  → {il[0] - 0x16} IL'in İÇİNE yazılı (gömülü literal)",
            0x1F => $"ldc.i4.s {il[1]}  → {il[1]} IL'in İÇİNE yazılı (gömülü literal)",
            0x7E => "ldsfld  → static alan çalışırken okunur",
            _ => "?",
        };
        Console.WriteLine($"{methodName,-14} IL: {Convert.ToHexString(il),-14} {meaning}");
    }
}
