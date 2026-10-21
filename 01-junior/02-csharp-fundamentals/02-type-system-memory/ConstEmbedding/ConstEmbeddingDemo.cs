using Junior.CSharpFundamentals.TypeSystemMemory.ConstEmbedding.Lib;

namespace Junior.CSharpFundamentals.TypeSystemMemory.ConstEmbedding;

/// <summary>"Uygulama" assembly'si: J02.Lib'i kullanır. Lib'in const değeri DERLEME anında bu assembly'nin IL'ine yazılır. Deney adımları README'de.</summary>
public static class ConstEmbeddingDemo
{
    public static void Run()
    {
        Console.WriteLine($"Lib sürümü (çalışırken okunan) : {Config.Version}");
        Console.WriteLine($"Config.MaxRetries (const)       : {Config.MaxRetries}   ← derleme zamanında bu assembly'nin IL'ine gömüldü");
        Console.WriteLine($"Config.Timeout (static readonly): {Config.Timeout}   ← çalışırken Lib.dll'den okunur");
    }
}
