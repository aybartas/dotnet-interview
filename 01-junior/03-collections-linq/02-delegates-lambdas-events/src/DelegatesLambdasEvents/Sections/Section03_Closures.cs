namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class Section03_Closures
{
    public static void Run()
    {
        Console.WriteLine("CLOSURE: bir lambda, tanımlandığı kapsamdaki DEĞİŞKENLERİ yakalar (değerlerini DEĞİL, değişkenin kendisini).\n");

        Console.WriteLine("1) Temel: yakalanan değişkeni lambda ÇALIŞTIĞI ANDA okur");
        var counter = 0;
        Action increment = () => counter++;                            // dış 'counter' değişkenini YAKALADI
        increment(); increment(); increment();
        Console.WriteLine($"   increment 3 kez çağrıldı → counter = {counter}   (lambda dış değişkeni değiştirdi)");

        var message = "ilk";
        Func<string> read = () => message;
        message = "sonra değişti";
        Console.WriteLine($"   lambda oluşturulduktan SONRA message değişti → read() = \"{read()}\"   (değer kopyalanmadı; değişken yakalandı)");

        Console.WriteLine("\n2) ⭐ KLASİK TUZAK: for döngüsü — tek bir 'i' değişkeni var, hepsi onu yakalar");
        var wrong = new List<Func<int>>();
        for (var i = 0; i < 3; i++)
            wrong.Add(() => i);                                        // 3 lambda da AYNI 'i'yi yakalıyor
        Console.WriteLine($"   beklenen: 0,1,2 — gerçek: {string.Join(",", wrong.Select(f => f()))}   (döngü bitince i == 3)");

        Console.WriteLine("\n   ÇÖZÜM: her iterasyonda döngü-yerel BİR KOPYA al");
        var right = new List<Func<int>>();
        for (var i = 0; i < 3; i++)
        {
            var copy = i;                                              // her iterasyonda YENİ değişken
            right.Add(() => copy);
        }
        Console.WriteLine($"   sonuç: {string.Join(",", right.Select(f => f()))}");

        Console.WriteLine("\n3) foreach değişkeni her iterasyonda YENİDİR (C# 5+) → tuzak yok:");
        var fromForeach = new List<Func<int>>();
        foreach (var n in new[] { 10, 20, 30 })
            fromForeach.Add(() => n);
        Console.WriteLine($"   {string.Join(",", fromForeach.Select(f => f()))}   (for ile fark: for değişkeni TEK, foreach değişkeni İTERASYON BAŞINA)");

        Console.WriteLine("\n4) Derleyici arkada ne üretir?  Yakalanan değişkenler HEAP'teki bir 'display class' nesnesine taşınır:");
        var captured = 5;
        Func<int> f = () => captured;
        Console.WriteLine($"   f.Target tipi = {f.Target!.GetType().Name}  (derleyicinin ürettiği sınıf; 'captured' artık o nesnenin alanı)");
        Func<int, int> noCapture = static x => x * 2;
        Console.WriteLine($"   yakalamayan static lambda: Target = {(noCapture.Target is null ? "null" : noCapture.Target.GetType().Name)} (önbelleğe alınmış tek örnek; ek tahsis yok)");

        Console.WriteLine("\n5) Closure'ın bedeli: yakalanan değişken, lambda yaşadığı sürece YAŞAR (büyük nesneyi yakalamak onu bellekte tutar) + her closure tahsis üretir");
        long Alloc(Action a) { var b = GC.GetAllocatedBytesForCurrentThread(); a(); return GC.GetAllocatedBytesForCurrentThread() - b; }
        var factor = 2;
        _ = Alloc(() => { });
        var withClosure = Alloc(() => { for (var k = 0; k < 1000; k++) { Func<int> c = () => k + factor; _ = c(); } });
        var withoutClosure = Alloc(() => { for (var k = 0; k < 1000; k++) { Func<int, int> c = static x => x + 2; _ = c(k); } });
        Console.WriteLine($"   1000 iterasyon: yakalayan lambda = {withClosure:N0} B · yakalamayan static lambda = {withoutClosure:N0} B");

        Console.WriteLine("""

        Mülakat özeti:
          • Closure = lambda + yakaladığı dış değişkenler. Değişken YAKALANIR (değer kopyası değil).
          • for döngüsünde tüm lambda'lar aynı sayaç değişkenini paylaşır → sonuçlar hep son değer. Çözüm: döngü-yerel kopya (foreach'te gerekmez).
          • Maliyet: display class tahsisi + yakalanan nesnenin ömrü uzar. Sıcak yolda `static` lambda ile yakalamayı yasaklayın.
          • Eşzamanlı kodda yakalanan değişkeni birden çok thread değiştirirse yarış durumu oluşur.
        """);
    }
}
