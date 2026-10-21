namespace Certification.AzureFundamentals.Shared;

/// <summary>Bir konunun çalıştırılabilir bölümü.</summary>
public sealed record Section(int Number, string Title, Action Run);

/// <summary>Konu giriş noktalarının ortak mantığı: argümansız → liste, &lt;numara&gt; → o bölüm, all → hepsi.</summary>
public static class SectionRunner
{
    public static int Run(string topicId, string topicTitle, IReadOnlyList<Section> sections, string[] args, string? extraUsage = null)
    {
        if (args.Length == 0)
        {
            Console.WriteLine($"C{topicId} {topicTitle} — bölümler:\n");
            foreach (var s in sections) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine($"\nÇalıştırmak için: dotnet run -- {topicId} <numara> | all");
            if (extraUsage is not null) Console.WriteLine(extraUsage);
            return 0;
        }

        var selected = args[0] == "all" ? sections : sections.Where(s => s.Number.ToString() == args[0]).ToList();
        if (selected.Count == 0)
        {
            Console.Error.WriteLine($"Bilinmeyen bölüm: {args[0]}");
            return 1;
        }

        foreach (var section in selected)
        {
            Console.WriteLine($"\n━━━ {section.Number}. {section.Title} ━━━");
            section.Run();
        }
        return 0;
    }
}
