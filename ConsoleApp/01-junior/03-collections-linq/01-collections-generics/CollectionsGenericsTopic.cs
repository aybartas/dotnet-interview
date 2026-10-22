namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class CollectionsGenericsTopic
{
    public static int Run(string[] args)
    {
        // J3.1 — Collections & Generics.
        //   dotnet run -- J3.1          → section list
        //   dotnet run -- J3.1 5     → List.Contains vs HashSet vs Dictionary lookup time
        //   dotnet run -- J3.1 all   → everything
        // The THEORY of data structures and Big-O proofs live in the DSA track (→ D2, D3); here: correct use of the .NET APIs.

        if (args.Length == 0)
        {
            Console.WriteLine("J3.1 Collections & Generics — sections:\n");
            foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine("\nTo run: dotnet run -- J3.1 <number> | all");
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
