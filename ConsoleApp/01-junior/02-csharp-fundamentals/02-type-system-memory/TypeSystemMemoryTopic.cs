using Junior.CSharpFundamentals.TypeSystemMemory.ConstEmbedding;

namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class TypeSystemMemoryTopic
{
    public static int Run(string[] args)
    {
        // J2.2 — Type System & Memory Model: experiments.
        //   dotnet run -- J2.2            → section list
        //   dotnet run -- J2.2 5       → section 5 (boxing)
        //   dotnet run -- J2.2 all     → all of them
        //
        // Each experiment answers the "what is happening?" question with output. PREDICT the output first, then run it.

        if (args is ["const-embedding"])        // const embedding experiment (steps are in the README)
        {
            ConstEmbeddingDemo.Run();
            return 0;
        }

        if (args.Length == 0)
        {
            Console.WriteLine("J2.2 Type System & Memory Model — sections:\n");
            foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine("\nTo run: dotnet run -- J2.2 <number> | all | const-embedding");
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
