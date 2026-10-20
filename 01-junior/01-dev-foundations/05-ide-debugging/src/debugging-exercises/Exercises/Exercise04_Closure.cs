namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise04_Closure : Exercise
{
    public override int Number => 4;
    public override string Title => "Döngüde yakalanan değişken (closure)";
    public override string Skill => "Watch / Locals / Immediate window ile değişkenin ömrünü görmek";
    public override string Hint => "Lambda'lar çalışırken Watch'a 'i' ekleyin. Kaç tane 'i' değişkeni var? (Make Object ID / Locals'ta closure sınıfına bakın)";

    public override void Run()
    {
        var actions = new List<Func<int>>();

        for (var i = 0; i < 3; i++)
            actions.Add(() => i * 10);

        var results = actions.Select(a => a()).ToArray();
        Report("lambda sonuçları", expected: "0,10,20", actual: string.Join(",", results));
    }
}
