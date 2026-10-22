namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section01_TryCatchFinally
{
    public static void Run()
    {
        Console.WriteLine("1) Çalışma sırası: try → (hata varsa) eşleşen İLK catch → finally (HER ZAMAN)");
        Flow(throwIt: false);
        Flow(throwIt: true);

        Console.WriteLine("\n2) catch blokları SIRAYLA denenir: özelden genele yazılır (genel olan önce yazılırsa derleyici hata verir: CS0160)");
        foreach (var f in new Func<int>[] { () => int.Parse("x"), () => 1 / int.Parse("0"), () => ((int[])null!)[0] })
        {
            try { f(); }
            catch (FormatException) { Console.WriteLine("   FormatException yakalandı"); }
            catch (DivideByZeroException) { Console.WriteLine("   DivideByZeroException yakalandı"); }
            catch (Exception ex) { Console.WriteLine($"   genel catch: {ex.GetType().Name}"); }
        }

        Console.WriteLine("\n3) finally, return'den SONRA bile çalışır (dönüş değeri hesaplandıktan sonra, çağırana dönmeden önce)");
        Console.WriteLine($"   ReturnInTry() = {ReturnInTry()}");

        Console.WriteLine("\n4) using / await using: IDisposable için try/finally şekerlemesi (kaynak her durumda bırakılır)");
        try
        {
            using var res = new Res("R1");
            Console.WriteLine("   kullanılıyor...");
            throw new InvalidOperationException("hata");
        }
        catch (InvalidOperationException) { Console.WriteLine("   catch çalıştı (Dispose ondan ÖNCE çalıştı: yukarıdaki 'bırakıldı' satırına bakın)"); }

        Console.WriteLine("""

        UYARILAR
          • finally içinde `return`/`throw` yazmayın: bekleyen exception'ı sessizce YUTAR veya orijinalini ezer.
          • finally içinde exception fırlarsa orijinal exception KAYBOLUR → finally'yi basit tutun (yalnızca temizlik).
          • catch (Exception) { } (boş catch) hatayı YUTAR → bulması imkânsız hatalar (→ Section 6).
          • Kaynakları bırakmak için try/finally yerine using kullanın.
        """);
    }

    private static void Flow(bool throwIt)
    {
        Console.WriteLine($"   --- throwIt = {throwIt} ---");
        try
        {
            Console.WriteLine("   try: başladı");
            if (throwIt) throw new InvalidOperationException("patladı");
            Console.WriteLine("   try: bitti");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"   catch: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("   finally: HER ZAMAN çalışır");
        }
    }

    private static int ReturnInTry()
    {
        try { return 1; }
        finally { Console.WriteLine("   (finally, return 1'den sonra çalıştı)"); }
    }

    private sealed class Res(string name) : IDisposable
    {
        public void Dispose() => Console.WriteLine($"   [{name}] bırakıldı (Dispose)");
    }
}
