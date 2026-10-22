using System.Diagnostics;

namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise09_DebugVsRelease : Exercise
{
    public override int Number => 9;
    public override string Title => "Correct in Debug, different result in Release";
    public override string Skill => "Configurations (Debug vs Release), [Conditional], '#if DEBUG', effect of optimization";
    public override string Hint => "Run the same exercise with 'dotnet run -- J1.5 9' and 'dotnet run -c Release -- J1.5 9'. What happens in Release to the side effect of the expression inside Debug.Assert?";

    private static int _nextId;

    public override void Run()
    {
        _nextId = 0;

        Debug.Assert(NextId() > 0, "Id must be positive");   // the Debug.Assert call is REMOVED ENTIRELY from a Release build
        var id = NextId();

        Report("value of the second NextId()", expected: 2, actual: id);
#if DEBUG
        Console.WriteLine("  (Build: DEBUG — Debug.Assert and the expression inside it ran)");
#else
        Console.WriteLine("  (Build: RELEASE — the Debug.Assert call was eliminated)");
#endif
    }

    private static int NextId() => ++_nextId;
}
