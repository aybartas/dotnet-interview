using System.Diagnostics;

namespace Junior.AsyncBasics.Concepts;

/// <summary>🛠 J4.1: Paylaşılan sayaçta race condition demosu.</summary>
public static class Section06_RaceCondition
{
    private const int Workers = 4;
    private const int PerWorker = 1_000_000;

    public static Task Run()
    {
        var expected = Workers * PerWorker;
        Console.WriteLine($"{Workers} thread × {PerWorker:N0} artırma → beklenen sonuç = {expected:N0}  ({Environment.ProcessorCount} çekirdek)\n");
        Console.WriteLine("counter++ tek satırdır ama ÜÇ adımdır: oku → 1 ekle → yaz. İki thread aynı değeri okuyup aynı sonucu yazarsa bir artırma KAYBOLUR.\n");

        // 1) Güvensiz
        var counter = 0;
        var sw = Stopwatch.StartNew();
        RunWorkers(() => { for (var i = 0; i < PerWorker; i++) counter++; });
        Print("1) counter++ (kilitsiz, RACE CONDITION)", counter, expected, sw);

        // 2) Interlocked: tek atomik CPU komutu
        var atomic = 0;
        sw.Restart();
        RunWorkers(() => { for (var i = 0; i < PerWorker; i++) Interlocked.Increment(ref atomic); });
        Print("2) Interlocked.Increment (atomik)", atomic, expected, sw);

        // 3) lock: kritik bölge; aynı anda tek thread
        var locked = 0;
        var gate = new object();
        sw.Restart();
        RunWorkers(() => { for (var i = 0; i < PerWorker; i++) lock (gate) locked++; });
        Print("3) lock (karşılıklı dışlama)", locked, expected, sw);

        // 4) Paylaşımı ortadan kaldır: her thread kendi toplamını tutar, sonda birleştirilir (en hızlısı)
        var partials = new int[Workers];
        sw.Restart();
        Parallel.For(0, Workers, w => { var local = 0; for (var i = 0; i < PerWorker; i++) local++; partials[w] = local; });
        Print("4) thread başına yerel toplam + birleştirme", partials.Sum(), expected, sw);

        Console.WriteLine("""

        Notlar:
          • Kayıp sayısı çalıştırmadan çalıştırmaya DEĞİŞİR (zamanlamaya bağlı); hiç kayıp çıkmadıysa tekrar çalıştırın (tek çekirdekli makinede nadir olabilir).
          • race condition "bazen" olur → en tehlikeli hata türüdür: testte geçer, production'da yük altında patlar.
          • Hiyerarşi: önce paylaşımı azalt (immutable, thread başına veri) → atomik işlem (Interlocked) → lock → (nadiren) daha gelişmiş primitifler (→ M4, S3).
          • async/await tek başına thread safety SAĞLAMAZ: await sonrası devam kodu başka thread'de çalışabilir ve aynı state'e dokunabilir.
        """);
        return Task.CompletedTask;
    }

    private static void RunWorkers(Action work) =>
        Task.WaitAll(Enumerable.Range(0, Workers).Select(_ => Task.Run(work)).ToArray());

    private static void Print(string title, int actual, int expected, Stopwatch sw) =>
        Console.WriteLine($"{title,-46} sonuç = {actual,10:N0}   {(actual == expected ? "✓ doğru" : $"✗ {expected - actual:N0} artırma KAYBOLDU"),-26} {sw.ElapsedMilliseconds} ms");
}
