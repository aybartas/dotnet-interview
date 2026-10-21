namespace Junior.BuildRunBasics.Shared;

/// <summary>Bir konunun çalıştırılabilir bölümü.</summary>
public sealed record Section(int Number, string Title, Action Run);

/// <summary>Konu giriş noktalarının ortak mantığı: argümansız → liste, &lt;numara&gt; → o bölüm, all → hepsi.</summary>
public static class SectionRunner
{
    public static int Run(string topicId, string topicTitle, IReadOnlyList<Section> sections, string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine($"J{topicId} {topicTitle} — bölümler:\n");
            foreach (var s in sections) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine($"\nÇalıştırmak için: dotnet run -- {topicId} <numara> | all");
            return 0;
        }

        foreach (var section in args[0] == "all" ? sections : sections.Where(s => s.Number.ToString() == args[0]))
        {
            Console.WriteLine($"\n━━━ {section.Number}. {section.Title} ━━━");
            section.Run();
        }
        return 0;
    }
}
