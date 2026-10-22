namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class DelegatesLambdasEventsTopic
{
    public static int Run(string[] args)
    {
        // J3.2 — Delegates, Lambdas & Events.
        //   dotnet run -- J3.2          → section list
        //   dotnet run -- J3.2 3     → closure trap ⭐
        //   dotnet run -- J3.2 all   → everything

        if (args.Length == 0)
        {
            Console.WriteLine("J3.2 Delegates, Lambdas & Events — sections:\n");
            foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine("\nTo run: dotnet run -- J3.2 <number> | all");
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
