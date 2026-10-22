namespace Certification.AzureFundamentals.Shared;

/// <summary>Konsola hizalı tablo yazan küçük yardımcı (sütun genişlikleri içerikten hesaplanır).</summary>
public static class Table
{
    public static void Print(IReadOnlyList<string> headers, IEnumerable<IReadOnlyList<string>> rows, string indent = "   ")
    {
        var all = rows.ToList();
        var widths = headers.Select((h, i) => Math.Max(h.Length, all.Count == 0 ? 0 : all.Max(r => r[i].Length))).ToArray();

        string Line(IReadOnlyList<string> cells) => indent + string.Join("  ", cells.Select((c, i) => c.PadRight(widths[i]))).TrimEnd();

        Console.WriteLine(Line(headers));
        Console.WriteLine(indent + string.Join("  ", widths.Select(w => new string('─', w))));
        foreach (var row in all) Console.WriteLine(Line(row));
    }
}
