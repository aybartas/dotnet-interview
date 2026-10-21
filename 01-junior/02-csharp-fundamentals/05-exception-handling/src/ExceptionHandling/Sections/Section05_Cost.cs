using System.Diagnostics;

namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section05_Cost
{
    public static void Run()
    {
        const int n = 20_000;
        var inputs = Enumerable.Range(0, n).Select(i => i % 2 == 0 ? "123" : "abc").ToArray();       // yarısı geçersiz
        // ısınma
        _ = ParseWithTry("x"); _ = ParseWithTryParse("x");

        var sw = Stopwatch.StartNew();
        var okA = inputs.Count(s => ParseWithTryParse(s) is not null);
        var tryParseMs = sw.Elapsed.TotalMilliseconds;

        sw.Restart();
        var okB = inputs.Count(s => ParseWithTry(s) is not null);
        var exceptionMs = sw.Elapsed.TotalMilliseconds;

        Console.WriteLine($"{n:N0} girdi, yarısı geçersiz:");
        Console.WriteLine($"   int.TryParse           : {tryParseMs,8:F2} ms   (başarılı: {okA:N0})");
        Console.WriteLine($"   int.Parse + try/catch  : {exceptionMs,8:F2} ms   (başarılı: {okB:N0})   → yaklaşık {exceptionMs / Math.Max(tryParseMs, 0.001):N0}× daha YAVAŞ");
        Console.WriteLine($"   (her fırlatma: stack trace yakalama + stack çözme + (varsa) filter/finally işletme → binlerce CPU döngüsü;");
        Console.WriteLine($"    exception olmayan yolda try bloğu neredeyse ücretsizdir — maliyet FIRLATILDIĞINDA ortaya çıkar)");
        Console.WriteLine("   Kaba ölçüm (Stopwatch); kesin sonuç için BenchmarkDotNet → S2.1");

        Console.WriteLine("""

        KURAL: Exception, "BEKLENEN" kontrol akışı için değil, "İSTİSNAİ" durumlar için kullanılır.
          ✗ Kullanıcı girdisini doğrulamak için Parse + catch FormatException      → ✓ TryParse
          ✗ Anahtar var mı diye dictionary[key] + catch KeyNotFoundException       → ✓ TryGetValue
          ✗ Dosya var mı diye catch FileNotFoundException (yarış koşulu hariç)      → ✓ File.Exists / TryOpen ya da Result döndür
          ✗ Döngüyü exception ile bitirmek                                         → ✓ normal koşul
        Gerekçeler: (1) performans (yukarıdaki ölçüm), (2) okunabilirlik — "normal" yol ile "hata" yolu karışır, (3) debugger'da first-chance exception gürültüsü,
        (4) izleme/alarm sistemleri exception sayısına bakar; normal akış yanlış alarm üretir.
        Kural: çağıranın BEKLEDİĞİ başarısızlık (parse hatası, kayıt yok) → Try-deseni / Result / null; çağıranın BEKLEMEDİĞİ ihlal (bug, kurulum hatası, kesinti) → exception.
        """);
    }

    private static int? ParseWithTryParse(string s) => int.TryParse(s, out var v) ? v : null;

    private static int? ParseWithTry(string s)
    {
        try { return int.Parse(s); }
        catch (FormatException) { return null; }
    }
}
