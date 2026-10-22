namespace Junior.BuildRunBasics.Shared;

/// <summary>A runnable section of a topic.</summary>
public sealed record Section(int Number, string Title, Action Run);

/// <summary>Shared logic of the topic entry points: no args → list, &lt;number&gt; → that section, all → every section.</summary>
public static class SectionRunner
{
    public static int Run(string topicId, string topicTitle, IReadOnlyList<Section> sections, string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine($"J{topicId} {topicTitle} — sections:\n");
            foreach (var s in sections) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine($"\nRun with: dotnet run -- J{topicId} <number> | all");
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
