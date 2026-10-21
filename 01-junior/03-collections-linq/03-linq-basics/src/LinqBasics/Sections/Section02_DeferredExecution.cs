namespace Junior.CollectionsLinq.LinqBasics;

public static class Section02_DeferredExecution
{
    public static void Run()
    {
        Console.WriteLine("1) Sorguyu TANIMLAMAK çalıştırmak değildir (deferred execution):");
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
        var query = numbers.Where(n => { Console.WriteLine($"      Where çalıştı: {n}"); return n % 2 == 0; });
        Console.WriteLine("   sorgu tanımlandı → hiç 'Where çalıştı' satırı yok; henüz hiçbir şey yürütülmedi.");
        Console.WriteLine("   ToList() çağrılıyor (immediate):");
        var snapshot = query.ToList();
        Console.WriteLine($"   sonuç: [{string.Join(",", snapshot)}]");

        Console.WriteLine("\n2) ⭐ TUZAK: sorgu her ENUMERATION'da YENİDEN çalışır ve kaynağın O ANKİ halini görür:");
        var data = new List<int> { 1, 2, 3 };
        var evens = data.Where(n => n % 2 == 0);                      // henüz çalışmadı
        data.Add(4); data.Add(6);                                      // tanımdan SONRA kaynak değişti
        Console.WriteLine($"   tanımdan sonra 4 ve 6 eklendi → evens = [{string.Join(",", evens)}]   (2 değil, 2,4,6: sorgu şimdi çalıştı)");
        var frozen = data.Where(n => n % 2 == 0).ToList();             // immediate: anlık görüntü
        data.Add(8);
        Console.WriteLine($"   ToList() anlık görüntüsü = [{string.Join(",", frozen)}] · canlı sorgu evens = [{string.Join(",", evens)}]");

        Console.WriteLine("\n3) Her enumeration kaynağı + lambda'yı TEKRAR çalıştırır (pahalıysa iki kez ödersiniz, yan etki iki kez olur):");
        var calls = 0;
        var expensive = Enumerable.Range(1, 3).Select(n => { calls++; return n * n; });
        _ = expensive.Count();     // 1. enumeration
        _ = expensive.Sum();       // 2. enumeration
        _ = expensive.Max();       // 3. enumeration
        Console.WriteLine($"   3 farklı terminal operatör → lambda toplam {calls} kez çalıştı (1 kez değil, 3 × 3 eleman)");
        calls = 0;
        var materialized = expensive.ToList();
        _ = materialized.Count; _ = materialized.Sum(); _ = materialized.Max();
        Console.WriteLine($"   ToList() ile bir kez materialize edince → {calls} çalışma (yalnızca 3 eleman için bir kez)");

        Console.WriteLine("\n4) Streaming: ardışık ertelenmiş operatörler elemanı TEK TEK ilerletir (aralarında ara liste yok):");
        var pipeline = Enumerable.Range(1, 4)
            .Where(n => { Console.WriteLine($"      Where({n})"); return n % 2 == 0; })
            .Select(n => { Console.WriteLine($"      Select({n})"); return n * 10; });
        Console.WriteLine("   foreach ile gezinirken sıralama (her eleman Where → Select'ten geçerek yield edilir):");
        foreach (var x in pipeline) Console.WriteLine($"   → sonuç {x}");

        Console.WriteLine("\n5) Buffering operatörler (OrderBy, GroupBy, Reverse, Distinct kısmen…): İLK sonuç istenince TÜM kaynağı okur:");
        var ordered = Enumerable.Range(1, 3).Select(n => { Console.WriteLine($"      kaynak okundu: {n}"); return 4 - n; }).OrderBy(n => n);
        Console.WriteLine("   OrderBy tanımlandı (henüz okunmadı). İlk eleman isteniyor (First):");
        Console.WriteLine($"   First() = {ordered.First()}  ← ilk elemanı vermek için BÜTÜN kaynak okunup sıralandı");

        Console.WriteLine("\n6) Hatalar da ertelenir (ve kaynak null ise tanım anında yakalanır):");
        var lazyBug = new List<int> { 1, 0 }.Select(n => 10 / n);      // 0'a bölme sorgu TANIMINDA değil, çalışırken patlar
        Console.WriteLine("   Select(10 / n) tanımlandı: hata yok");
        try { _ = lazyBug.ToList(); }
        catch (DivideByZeroException) { Console.WriteLine("   ToList() → DivideByZeroException (hata enumeration anında çıktı; stack trace tanım noktasını göstermez)"); }

        Console.WriteLine("""

        ÖZET ⭐
          ERTELENMİŞ (deferred):  Where, Select, SelectMany, Take, Skip, Zip, Concat, OrderBy*, GroupBy*, Join*, Distinct, Chunk, …   (* tamamen buffer'lar, ama yine de enumeration ANINDA)
          ANINDA (immediate):     ToList, ToArray, ToDictionary, ToLookup, ToHashSet · Count, Sum, Average, Min, Max, Aggregate · First, Single, Last, ElementAt · Any, All, Contains
        Yan etkiler:  sorgu içine yan etkili kod (log, sayaç, DB yazma) koymayın; kaç kez ve ne zaman çalışacağı enumeration'a bağlıdır.
        Kural:  sonucu birden çok kez kullanacaksanız, kaynak değişebilecekse ya da hesap pahalıysa → ToList()/ToArray() ile MATERIALIZE edin.
                aksi halde ertelenmiş bırakmak bellek ve gereksiz ara koleksiyon tasarrufu sağlar.
        """);
    }
}
