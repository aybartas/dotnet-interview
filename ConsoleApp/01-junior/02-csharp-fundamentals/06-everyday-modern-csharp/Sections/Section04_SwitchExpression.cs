namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section04_SwitchExpression
{
    public static void Run()
    {
        foreach (var day in new[] { DayOfWeek.Monday, DayOfWeek.Saturday })
            Console.WriteLine($"   {day,-9} → old switch: {OldStyle(day),-8} new switch expression: {Describe(day)}");

        Console.WriteLine($"\n   Score 95 → {Grade(95)}, 72 → {Grade(72)}, 10 → {Grade(10)}   (relational pattern ranges)");
        Console.WriteLine($"   (IsWeekend, Hour) = (true, 11)  → {Greeting(true, 11)}");
        Console.WriteLine($"   (IsWeekend, Hour) = (false, 8)  → {Greeting(false, 8)}");

        try { _ = Parse("x"); }
        catch (ArgumentOutOfRangeException ex) { Console.WriteLine($"   no match → throw expression: {ex.GetType().Name}"); }

        Console.WriteLine("""

        switch STATEMENT (old): every branch needs `break;`, you declare the variable beforehand, you can forget the assignment, they're statements not expressions
        switch EXPRESSION     : produces a VALUE → assigned/returned directly; `=>`; `_` discard (default); `when` guard;
                                since branches are expressions there's no "forgotten assignment"; the compiler gives an **exhaustiveness** warning (CS8509):
                                a warning if an enum value is left out. If nothing matches, SwitchExpressionException is thrown at run time.
        Branch order MATTERS: specific first, then general (a branch that shadows an earlier one is a compile error: CS8510).
        """);
    }

    private static string OldStyle(DayOfWeek day)
    {
        string result;
        switch (day)
        {
            case DayOfWeek.Saturday:
            case DayOfWeek.Sunday:
                result = "weekend";
                break;
            default:
                result = "weekday";
                break;
        }
        return result;
    }

    private static string Describe(DayOfWeek day) => day switch
    {
        DayOfWeek.Saturday or DayOfWeek.Sunday => "weekend",
        _ => "weekday",
    };

    private static string Grade(int score) => score switch
    {
        >= 90 => "A",
        >= 80 => "B",
        >= 70 => "C",
        >= 0 => "F",
        _ => throw new ArgumentOutOfRangeException(nameof(score)),
    };

    private static string Greeting(bool isWeekend, int hour) => (isWeekend, hour) switch
    {
        (true, < 12) => "Good morning, enjoy your weekend!",
        (true, _) => "Enjoy your weekend!",
        (false, < 9) => "The early bird catches the worm",
        (false, _) => "Have a good day at work",
    };

    private static int Parse(string s) => s switch
    {
        "one" => 1,
        "two" => 2,
        _ => throw new ArgumentOutOfRangeException(nameof(s), s, "unknown number"),
    };
}
