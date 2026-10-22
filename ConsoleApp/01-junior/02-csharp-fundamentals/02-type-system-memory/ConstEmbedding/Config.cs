namespace Junior.CSharpFundamentals.TypeSystemMemory.ConstEmbedding;

/// <summary>"Kütüphane" sabitleri: const çağıranlara GÖMÜLÜR, static readonly çalışırken OKUNUR.</summary>
public static class Config
{
    public const int MaxRetries = 3;                   // const: derleme zamanında çağıranların IL'ine kopyalanır
    public static readonly int Timeout = 30;           // static readonly: çağıran çalışırken alanı okur
}
