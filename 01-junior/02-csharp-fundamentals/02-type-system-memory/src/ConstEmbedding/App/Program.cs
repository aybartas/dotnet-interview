using Junior.CSharpFundamentals.TypeSystemMemory.ConstEmbedding.Lib;

// "Uygulama" assembly'si: Lib'i kullanır. Lib'in const değeri DERLEME anında bu assembly'nin IL'ine yazılır.
Console.WriteLine($"Lib sürümü (çalışırken okunan) : {Config.Version}");
Console.WriteLine($"Config.MaxRetries (const)       : {Config.MaxRetries}   ← derleme zamanında App'in IL'ine gömüldü");
Console.WriteLine($"Config.Timeout (static readonly): {Config.Timeout}   ← çalışırken Lib.dll'den okunur");
