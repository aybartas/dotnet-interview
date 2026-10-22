namespace Junior.CSharpFundamentals.ModernCSharp;

public static class ModernCSharpTopic
{
    public static int Run(string[] args)
    {
        // J2.6 — Everyday Modern C#.
        //   dotnet run -- J2.6          → section list
        //   dotnet run -- J2.6 10    → the SAME order model, C# 7 style vs C# 14 style (line count + behavioural equality)
        //   dotnet run -- J2.6 all   → all sections

        if (args.Length == 0)
        {
            Console.WriteLine("J2.6 Everyday Modern C# — sections:\n");
            foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine("\nTo run: dotnet run -- J2.6 <number> | all");
            return 0;
        }

        foreach (var section in args[0] == "all" ? Sections.All : Sections.All.Where(s => s.Number.ToString() == args[0]))
        {
            Console.WriteLine($"\n━━━ {section.Number}. {section.Title} ━━━");
            section.Run();
        }
        return 0;
    }
}
