namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section03_Filters
{
    public static void Run()
    {
        Console.WriteLine("1) Filter: catch yalnızca KOŞUL doğruysa devreye girer; değilse exception hiç yakalanmamış gibi yoluna devam eder");
        foreach (var status in new[] { 404, 503 })
        {
            try { throw new HttpFailure(status); }
            catch (HttpFailure ex) when (ex.StatusCode == 404) { Console.WriteLine($"   {status} → 404 filtresi yakaladı (bulunamadı işlemi)"); }
            catch (HttpFailure ex) when (ex.StatusCode >= 500) { Console.WriteLine($"   {status} → 5xx filtresi yakaladı (tekrar denenebilir)"); }
        }

        Console.WriteLine("\n2) Filter, stack çözülmeden ÖNCE çalışır → debugger/dump'ta fırlatma noktasının stack'i ve yerel değişkenleri KORUNUR.");
        Console.WriteLine("   Sıralama deneyi (iç finally ile filter hangisi önce?):");
        try { Outer(); }
        catch (Exception ex) when (Log("filter çalıştı", ex)) { Console.WriteLine("   catch gövdesi çalıştı"); }
        Console.WriteLine("   → Önce FILTER, sonra iç finally, sonra catch gövdesi. (Koşulu catch İÇİNDE `if` ile kontrol etseydik, koşul iç finally'den SONRA —stack çözüldükten sonra— değerlendirilirdi.)");

        Console.WriteLine("\n3) 'Loglayıp yakalamadan geçir' deseni: filter her zaman false döner, exception AKIŞI BOZULMAZ ve stack bozulmaz");
        try
        {
            try { throw new InvalidOperationException("iç hata"); }
            catch (Exception ex) when (LogAndContinue(ex)) { /* asla girmez */ }
        }
        catch (InvalidOperationException) { Console.WriteLine("   dış catch yakaladı (exception filter sayesinde stack ve davranış değişmedi)"); }

        Console.WriteLine("""

        Neden filter, catch içinde if + throw; yerine?
          • stack çözülmeden değerlendirilir → daha iyi tanılama (dump, debugger "first chance")
          • yakalanmayacak exception'ı hiç yakalamamış gibi bırakır (rethrow maliyeti ve stack riski yok)
          • tipik kullanım: HTTP status, hata kodu, InnerException tipi, retry'lanabilir hata ayrımı, CancellationToken kontrolü
        Filter içinde exception fırlatmayın (yutulur ve false sayılır); filter hızlı ve yan etkisiz olmalı (log yan etkisi kabul edilebilir).
        """);
    }

    private static void Outer()
    {
        try { throw new InvalidOperationException("kök"); }
        finally { Console.WriteLine("   iç finally çalıştı"); }
    }

    private static bool Log(string message, Exception ex)
    {
        Console.WriteLine($"   {message} ({ex.GetType().Name})");
        return true;
    }

    private static bool LogAndContinue(Exception ex)
    {
        Console.WriteLine($"   [log] {ex.Message}");
        return false;                                  // false → bu catch EŞLEŞMEZ, exception yoluna devam eder
    }

    private sealed class HttpFailure(int statusCode) : Exception($"HTTP {statusCode}")
    {
        public int StatusCode { get; } = statusCode;
    }
}
