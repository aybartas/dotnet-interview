using System.Globalization;

namespace Junior.CSharpFundamentals.Oop;

public static class OopTopic
{
    public static int Run(string[] args)
    {
        // J2.3 — OOP Fundamentals.
        //   dotnet run -- J2.3          → section list
        //   dotnet run -- J2.3 4     → section 4 (new vs override ⭐)
        //   dotnet run -- J2.3 all   → all sections
        //   Section06: aggregation vs composition lifetime difference (proved with WeakReference + GC)
        //   Section09: at EVERY step of the inheritance → composition refactor the same scenarios give the same result

        // Culture-independent output (N2, F2, decimals); restored on exit so other topics are unaffected.
        var previousCulture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        try
        {
            if (args.Length == 0)
            {
                Console.WriteLine("J2.3 OOP Fundamentals — sections:\n");
                foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
                Console.WriteLine("\nTo run: dotnet run -- J2.3 <number> | all");
                return 0;
            }

            foreach (var section in args[0] == "all" ? Sections.All : Sections.All.Where(s => s.Number.ToString() == args[0]))
            {
                Console.WriteLine($"\n━━━ {section.Number}. {section.Title} ━━━");
                section.Run();
            }
            return 0;
        }
        finally
        {
            CultureInfo.CurrentCulture = previousCulture;
        }
    }
}
