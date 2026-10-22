using System.Globalization;

namespace Junior.CSharpFundamentals.ClassMembers;

public static class ClassMembersTopic
{
    public static int Run(string[] args)
    {
        // J2.4 — Class Members & Object Semantics.
        //   dotnet run -- J2.4          → section list
        //   dotnet run -- J2.4 2     → initialization order ⭐
        //   dotnet run -- J2.4 all   → all sections

        // Culture-independent output (N2, decimals); restored on exit so other topics are unaffected.
        var previousCulture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        try
        {
            if (args.Length == 0)
            {
                Console.WriteLine("J2.4 Class Members & Object Semantics — sections:\n");
                foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
                Console.WriteLine("\nTo run: dotnet run -- J2.4 <number> | all");
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
