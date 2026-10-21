namespace Junior.CSharpFundamentals.TypeSystemMemory.ConstEmbedding.Lib;

/// <summary>"Kütüphane" sürümü. V2 sembolü tanımlıysa (dotnet build -p:DefineConstants=V2) değerler değişir.</summary>
public static class Config
{
#if V2
    public const int MaxRetries = 5;                   // const: çağıranlara GÖMÜLÜR
    public static readonly int Timeout = 60;           // static readonly: çağıran çalışırken OKUR
    public static string Version => "v2";
#else
    public const int MaxRetries = 3;
    public static readonly int Timeout = 30;
    public static string Version => "v1";
#endif
}
