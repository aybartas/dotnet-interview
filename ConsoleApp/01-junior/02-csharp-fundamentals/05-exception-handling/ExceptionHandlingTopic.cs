namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class ExceptionHandlingTopic
{
    public static int Run(string[] args) => RunAsync(args).GetAwaiter().GetResult();

    private static async Task<int> RunAsync(string[] args)
    {
        // J2.5 — Exception Handling.
        //   dotnet run -- J2.5          → section list
        //   dotnet run -- J2.5 4     → throw vs throw ex ⭐
        //   dotnet run -- J2.5 all   → all sections

        if (args.Length == 0)
        {
            Console.WriteLine("J2.5 Exception Handling — sections:\n");
            foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine("\nTo run: dotnet run -- J2.5 <number> | all");
            return 0;
        }

        foreach (var section in args[0] == "all" ? Sections.All : Sections.All.Where(s => s.Number.ToString() == args[0]))
        {
            Console.WriteLine($"\n━━━ {section.Number}. {section.Title} ━━━");
            await section.Run();
        }
        return 0;
    }
}
