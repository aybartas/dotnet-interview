namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class DelegatesLambdasEventsTopic
{
    public static int Run(string[] args)
    {
        // J3.2 — Delegates, Lambdas & Events.
        //   dotnet run -- J3.2          → bölüm listesi
        //   dotnet run -- J3.2 3     → closure tuzağı ⭐
        //   dotnet run -- J3.2 all   → hepsi

        if (args.Length == 0)
        {
            Console.WriteLine("J3.2 Delegates, Lambdas & Events — bölümler:\n");
            foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine("\nÇalıştırmak için: dotnet run -- J3.2 <numara> | all");
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
