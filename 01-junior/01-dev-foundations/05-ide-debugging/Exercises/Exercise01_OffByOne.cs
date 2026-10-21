namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise01_OffByOne : Exercise
{
    public override int Number => 1;
    public override string Title => "Döngü: 1'den n'e toplam";
    public override string Skill => "Step over / watch / locals — döngü değişkenini izleme";
    public override string Hint => "SumUpTo içindeki döngüye breakpoint koyup F10 ile ilerleyin; Watch'a 'i' ve 'sum' ekleyin. Son iterasyonda i kaç?";

    public override void Run()
    {
        Report("1..10 toplamı", expected: 55, actual: SumUpTo(10));
        Report("1..1 toplamı", expected: 1, actual: SumUpTo(1));
    }

    private static int SumUpTo(int n)
    {
        var sum = 0;
        for (var i = 1; i < n; i++)
            sum += i;
        return sum;
    }
}
