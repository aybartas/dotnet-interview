namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section06_WhatToCatch
{
    public static void Run()
    {
        Console.WriteLine("""
        YAKALAMALI (ne yapacağını BİLDİĞİN hata):
          • Beklenen, kurtarılabilir durum:   catch (FileNotFoundException) → varsayılan yapılandırmayı yükle
          • Geçici (transient) hata + retry:  catch (HttpRequestException ex) when (ex.StatusCode is >= 500) → tekrar dene
          • Katman sınırında DÖNÜŞTÜR:        catch (SqlException ex) → new OrderRepositoryException(…, ex)   (inner'ı koru)
          • Temizlik/telafi gerekiyorsa:      catch { geri al; throw; }   (ya da finally / using)

        YAKALAMAMALI:
          • Kendi BUG'ın olan hatalar: NullReferenceException, IndexOutOfRangeException, InvalidCastException, ArgumentNullException (çağıranın hatası).
            Bunları yakalayıp susturmak hatayı gizler → düzeltin, yakalamayın (guard clause / null kontrolü).
          • Ne yapacağını bilmediğin hata: yakalayıp loglayıp YUTMAK = en kötüsü. Bilmiyorsan yukarıya bırak.
          • Kurtarılamaz durumlar: OutOfMemoryException (çoğunlukla), StackOverflowException (YAKALANAMAZ, process ölür), ThreadAbort/AccessViolation.
          • Boş catch:  catch { }  → "hata yok" yalanı. En azından logla.

        catch (Exception) NE ZAMAN MEŞRU?  Yalnızca SINIRLARDA, ne yapacağınız belli olduğunda:
          1) Uygulamanın EN ÜST seviyesi / global handler (ASP.NET Core exception middleware → 500 + log; Main'de son çare handler)
          2) Arka plan döngüsü / worker: tek bir iş hatası TÜM servisi öldürmesin (logla, sıradaki işe geç)
          3) Plugin/üçüncü parti çağrı sınırı: dış kodun hatası sizin sürecinizi bozmasın
          4) catch + (temizlik) + `throw;` : Exception'ı ASLA yutmadan geçirmek
          5) Test/CLI "son çare": kullanıcıya anlamlı mesaj, doğru exit code
        """);

        Console.WriteLine("Canlı örnek — worker döngüsü (sınır): bir iş patlar, servis ayakta kalır:");
        string[] jobs = ["iş-1", "HATA", "iş-3"];
        foreach (var job in jobs)
        {
            try
            {
                if (job == "HATA") throw new InvalidOperationException("iş işlenemedi");
                Console.WriteLine($"   {job} işlendi");
            }
            catch (Exception ex)                                       // meşru: iş sınırı
            {
                Console.WriteLine($"   [LOG] {job} başarısız → {ex.GetType().Name}: {ex.Message}  (devam ediliyor)");
            }
        }

        Console.WriteLine("\nGuard clause: çağıranın hatasını YAKALAMAK yerine ERKEN ve NET reddet:");
        try { Process(null!); }
        catch (ArgumentNullException ex) { Console.WriteLine($"   Process(null) → {ex.GetType().Name} (ParamName={ex.ParamName}): çağıran düzeltilmeli, handler yazılmamalı"); }

        Console.WriteLine("\nStackOverflow yakalanamaz → özyinelemeyi sınırla (bu deney bilerek ÇALIŞTIRILMAZ).");
    }

    private static void Process(string input)
    {
        ArgumentNullException.ThrowIfNull(input);
    }
}
