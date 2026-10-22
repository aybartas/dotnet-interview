namespace Junior.CSharpFundamentals.TypeSystemMemory.ConstEmbedding;

/// <summary>"Library" constants: const is EMBEDDED into callers, static readonly is READ at run time.</summary>
public static class Config
{
    public const int MaxRetries = 3;                   // const: copied into the callers' IL at compile time
    public static readonly int Timeout = 30;           // static readonly: the caller reads the field at run time
}
