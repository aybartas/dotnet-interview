namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise07_ForgottenAwait : Exercise
{
    public override int Number => 7;
    public override string Title => "Unutulan await";
    public override string Skill => "Debugger'da async akış: Tasks penceresi, F10'un 'await'te davranışı";
    public override string Hint => "SaveAsync çağrısından sonraki satıra gelindiğinde Task'ın durumu ne? Watch'a 'task.Status' ekleyin. (Neden derleyici uyarısı yok? Sonucu bir değişkene atamak CS4014'ü susturur.)";

    public override void Run()
    {
        RunAsync().GetAwaiter().GetResult();
    }

    private static async Task RunAsync()
    {
        var repo = new SlowRepository();

        var task = repo.SaveAsync("kayıt-1");        // ← await yok!

        Report("kaydedilen kayıt sayısı", expected: 1, actual: repo.Count);
        await task;                                   // sonradan beklenir, ama kontrol çoktan yapıldı
        Console.WriteLine($"  (bekledikten sonra Count = {repo.Count})");
    }

    private sealed class SlowRepository
    {
        public int Count { get; private set; }

        public async Task SaveAsync(string item)
        {
            await Task.Delay(100);                   // veritabanı gecikmesi simülasyonu
            Count++;
        }
    }
}
