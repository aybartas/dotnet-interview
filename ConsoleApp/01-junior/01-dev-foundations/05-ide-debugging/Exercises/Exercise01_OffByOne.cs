namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise01_OffByOne : Exercise
{
    public override int Number => 1;
    public override string Title => "Loop: sum from 1 to n";
    public override string Skill => "Step over / watch / locals — tracking the loop variable";
    public override string Hint => "Put a breakpoint on the loop in SumUpTo and step with F10; add 'i' and 'sum' to Watch. What is i in the last iteration?";

    public override void Run()
    {
        Report("sum of 1..10", expected: 55, actual: SumUpTo(10));
        Report("sum of 1..1", expected: 1, actual: SumUpTo(1));
    }

    private static int SumUpTo(int n)
    {
        var sum = 0;
        for (var i = 1; i < n; i++)
            sum += i;
        return sum;
    }
}
