namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class ExceptionHandlingTopic
{
    public static int Run(string[] args) => RunAsync(args).GetAwaiter().GetResult();

    private static async Task<int> RunAsync(string[] args)
    {
        // J2.5 — Exception Handling.
        //   dotnet run -- 2.5          → bölüm listesi
        //   dotnet run -- 2.5 4     → throw vs throw ex ⭐
        //   dotnet run -- 2.5 all   → hepsi

        if (args.Length == 0)
        {
            Console.WriteLine("J2.5 Exception Handling — bölümler:\n");
            foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine("\nÇalıştırmak için: dotnet run -- 2.5 <numara> | all");
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
