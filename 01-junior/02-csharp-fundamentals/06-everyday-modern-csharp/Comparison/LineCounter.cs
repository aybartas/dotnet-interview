using System.Runtime.CompilerServices;

namespace Junior.CSharpFundamentals.ModernCSharp.Comparison;

/// <summary>"Anlamlı satır" sayacı: boş satırlar ve yalnızca yorum olan satırlar sayılmaz. (Süslü parantez-only satırlar SAYILIR: gerçek bir kod satırıdır.)</summary>
public static class LineCounter
{
    public static int Count(string path)
        => File.ReadLines(path)
               .Select(l => l.Trim())
               .Count(l => l.Length > 0 && !l.StartsWith("//", StringComparison.Ordinal));

    /// <summary>Kaynak klasörü: bu dosyanın (LineCounter.cs) yolundan türetilir — çağıranın dosyası değil.</summary>
    public static string StyleDirectory(string folder)
        => Path.Combine(Path.GetDirectoryName(ThisFile())!, "..", folder);

    private static string ThisFile([CallerFilePath] string file = "") => file;
}
