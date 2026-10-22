namespace Junior.CollectionsLinq.LinqBasics;

public static class LinqBasicsTopic
{
    public static int Run(string[] args)
    {
        // J3.3 — LINQ Basics.
        //   dotnet run -- J3.3          → section list
        //   dotnet run -- J3.3 2     → deferred vs immediate execution ⭐
        //   dotnet run -- J3.3 8     → 20 report exercises (outputs)
        //   dotnet run -- J3.3 all   → everything
        // Exercise: solve the questions in Reports/Reports.cs YOURSELF first, then compare with the solution.

        if (args.Length == 0)
        {
            Console.WriteLine("J3.3 LINQ Basics — sections:\n");
            foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine("\nTo run: dotnet run -- J3.3 <number> | all");
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
