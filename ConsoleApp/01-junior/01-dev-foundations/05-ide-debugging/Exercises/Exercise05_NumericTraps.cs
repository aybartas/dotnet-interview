namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise05_NumericTraps : Exercise
{
    public override int Number => 5;
    public override string Title => "Integer division and floating-point traps";
    public override string Skill => "Immediate window: evaluating an expression while running (e.g. 'percent / 100')";
    public override string Hint => "Open the Immediate window at the breakpoint: type 'percent / 100', '(double)percent / 100', '0.1 + 0.2 == 0.3' and look at the results. Which types are being mixed?";

    public override void Run()
    {
        Report("10% discount on 100", expected: 90m, actual: ApplyDiscount(price: 100, percent: 10));
        Report("average of {1,2}", expected: 1.5, actual: Average([1, 2]));
        Report("0.1 + 0.2 == 0.3", expected: true, actual: IsTotalCorrect(0.1, 0.2, 0.3));
    }

    private static decimal ApplyDiscount(int price, int percent)
        => price - price * (percent / 100);

    private static double Average(int[] values)
        => values.Sum() / values.Length;

    private static bool IsTotalCorrect(double a, double b, double expected)
        => a + b == expected;
}
