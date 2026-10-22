using System.Runtime.CompilerServices;

namespace Junior.CSharpFundamentals.ModernCSharp.Comparison;

/// <summary>"Meaningful line" counter: blank lines and comment-only lines are not counted. (Brace-only lines ARE counted: they are real lines of code.)</summary>
public static class LineCounter
{
    public static int Count(string path)
        => File.ReadLines(path)
               .Select(l => l.Trim())
               .Count(l => l.Length > 0 && !l.StartsWith("//", StringComparison.Ordinal));

    /// <summary>Source folder: derived from the path of this file (LineCounter.cs) — not the caller's file.</summary>
    public static string StyleDirectory(string folder)
        => Path.Combine(Path.GetDirectoryName(ThisFile())!, "..", folder);

    private static string ThisFile([CallerFilePath] string file = "") => file;
}
