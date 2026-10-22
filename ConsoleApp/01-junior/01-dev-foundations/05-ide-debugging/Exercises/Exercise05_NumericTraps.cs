namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise05_NumericTraps : Exercise
{
    public override int Number => 5;
    public override string Title => "Tamsayı bölme ve kayan nokta tuzakları";
    public override string Skill => "Immediate window: ifadeyi çalışırken değerlendirmek (ör. 'percent / 100')";
    public override string Hint => "Breakpoint'te Immediate window açın: 'percent / 100', '(double)percent / 100', '0.1 + 0.2 == 0.3' yazıp sonuçlara bakın. Hangi tipler karışıyor?";

    public override void Run()
    {
        Report("100 TL'ye %10 indirim", expected: 90m, actual: ApplyDiscount(price: 100, percent: 10));
        Report("{1,2} ortalaması", expected: 1.5, actual: Average([1, 2]));
        Report("0.1 + 0.2 == 0.3", expected: true, actual: IsTotalCorrect(0.1, 0.2, 0.3));
    }

    private static decimal ApplyDiscount(int price, int percent)
        => price - price * (percent / 100);

    private static double Average(int[] values)
        => values.Sum() / values.Length;

    private static bool IsTotalCorrect(double a, double b, double expected)
        => a + b == expected;
}
