using System.Globalization;
using Junior.DevFoundations.IdeDebugging.AnalyzersNullable;

// Note: we have to live in the same-named namespace as the Exercises class (to resolve Exercises.All).
namespace Junior.DevFoundations.IdeDebugging.Exercises;

public static class IdeDebuggingTopic
{
    public static int Run(string[] args)
    {
        // Culture-independent number formatting (1.5 instead of 1,5) regardless of the machine culture.
        var previous = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        try
        {
            return RunCore(args);
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }

    private static int RunCore(string[] args)
    {
        // J1.5 — Debugger exercises. Every exercise is DELIBERATELY buggy.
        //
        //   dotnet run -- J1.5          → exercise list
        //   dotnet run -- J1.5 3        → run exercise 3 (you will see an error / a wrong result)
        //   dotnet run -- J1.5 3 --hint → hint (which debugger feature should you use?)
        //   dotnet run -- J1.5 all      → run all of them in order (bugs that go uncaught are summarized)
        //   dotnet run -- J1.5 analyzers → code with nullable/analyzer warnings (AnalyzersNullable/) — for the warnings: dotnet build
        //
        // Goal: find the bugs with the DEBUGGER, not by READING the code line by line (F5 → breakpoint → inspect).
        // In the IDE: make the ConsoleApp project the startup project (debug arguments: J1.5 3), put a breakpoint in RunOne, step in with F11.

        if (args is ["analyzers"])
        {
            // The types are enough to produce the warnings; running it also prints a short output.
            // Note: GetUserName(2) returns null at run time — exactly what the compiler warns about with CS8603.
            var service = new UserService { Name = "demo" };
            Console.WriteLine($"1 → {service.GetUserName(1)}");
            Console.WriteLine($"2 → {service.GetUserName(2) ?? "(returned null)"}");
            Console.WriteLine($"Style demo: IsAllowed(admin) = {new StyleViolations().IsAllowed("admin")}");
            return 0;
        }

        var exercises = Exercises.All;

        if (args.Length == 0)
        {
            Console.WriteLine("Debugger exercises:\n");
            foreach (var e in exercises)
                Console.WriteLine($"  {e.Number}. {e.Title,-40} → {e.Skill}");
            Console.WriteLine("\nTo run:  dotnet run -- J1.5 <number>   (add --hint for a hint)  |  analyzers");
            return 0;
        }

        if (args[0] == "all")
        {
            foreach (var e in exercises)
                RunOne(e);
            return 0;
        }

        if (!int.TryParse(args[0], out var number) || exercises.FirstOrDefault(e => e.Number == number) is not { } selected)
        {
            Console.Error.WriteLine($"Invalid exercise: {args[0]}");
            return 1;
        }

        if (args.Contains("--hint"))
        {
            Console.WriteLine($"[{selected.Number}] {selected.Title}\nSkill: {selected.Skill}\nHint: {selected.Hint}");
            return 0;
        }

        RunOne(selected);
        return 0;

        static void RunOne(Exercise e)
        {
            Console.WriteLine($"\n━━ {e.Number}. {e.Title} ━━");
            try
            {
                e.Run();
            }
            catch (Exception ex)
            {
                // The exercise crashed. Without a debugger you only see the message; with the "first-chance exception" setting
                // you should stop on the line where the exception was THROWN. (Exception Settings / Ctrl+Alt+E)
                Console.WriteLine($"  ✗ EXCEPTION: {ex.GetType().Name}: {ex.Message}");
                Console.WriteLine($"    (thrown at: {ex.StackTrace?.Split('\n').FirstOrDefault()?.Trim()})");
            }
        }
    }
}
