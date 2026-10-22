namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise04_Closure : Exercise
{
    public override int Number => 4;
    public override string Title => "Variable captured in a loop (closure)";
    public override string Skill => "Seeing a variable's lifetime with Watch / Locals / Immediate window";
    public override string Hint => "Add 'i' to Watch while the lambdas run. How many 'i' variables are there? (Make Object ID / look at the closure class in Locals)";

    public override void Run()
    {
        var actions = new List<Func<int>>();

        for (var i = 0; i < 3; i++)
            actions.Add(() => i * 10);

        var results = actions.Select(a => a()).ToArray();
        Report("lambda results", expected: "0,10,20", actual: string.Join(",", results));
    }
}
