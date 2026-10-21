using Junior.CSharpFundamentals.ExceptionHandling.Domain;

namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section04_ThrowVsThrowEx
{
    public static void Run()
    {
        Console.WriteLine("Zincir:  Entry() → Middle() → ThrowSite()  ← hata GERÇEKTE burada fırlatılıyor. Entry catch'te yeniden fırlatıyor.\n");

        Show("catch { throw; }                       (DOĞRU)", Rethrow.Capture(Rethrow.Mode.ThrowKeyword));
        Show("catch (Exception ex) { throw ex; }     (YANLIŞ — stack trace KAYBOLUR)", Rethrow.Capture(Rethrow.Mode.ThrowEx));

        var wrapped = Rethrow.Capture(Rethrow.Mode.Wrap);
        Show("catch (...) { throw new X(\"..\", ex); } (sarmalama — inner KORUNUR)", wrapped);
        Console.WriteLine("      └─ InnerException stack'i:");
        PrintFrames(wrapped.InnerException!, indent: "         ");

        Show("ExceptionDispatchInfo.Capture(ex).Throw()", Rethrow.Capture(Rethrow.Mode.DispatchInfo));

        Console.WriteLine("""

        SONUÇ
          • `throw;`   → orijinal stack trace korunur. Yeniden fırlatmanın DOĞRU yolu.
          • `throw ex;` → stack trace catch noktasından yeniden başlar; ThrowSite/Middle kaybolur → hatanın NEREDE doğduğunu bulamazsınız.
          • Bağlam eklemek istiyorsanız yeni exception + `innerException` parametresi (hiçbir bilgi kaybolmaz).
          • ExceptionDispatchInfo: catch'te yakalanan exception'ı başka bir yerden/thread'den orijinal stack'iyle fırlatmak için
            (async altyapısı Task.Exception'ı `await`'te bu mekanizmayla yeniden fırlatır).
          • Hiç yakalamayacaksanız catch yazmayın; yalnızca bir şey YAPACAKSANIZ (log, temizlik, dönüştürme) yakalayın.
          • `throw;` yalnızca catch bloğunun içinde geçerlidir.
        """);
    }

    private static void Show(string title, Exception ex)
    {
        Console.WriteLine($"▶ {title}");
        Console.WriteLine($"   {ex.GetType().Name}: {ex.Message}");
        PrintFrames(ex, indent: "   ");
        Console.WriteLine($"   ThrowSite stack'te var mı? → {ex.StackTrace?.Contains("ThrowSite") == true}\n");
    }

    private static void PrintFrames(Exception ex, string indent)
    {
        foreach (var line in (ex.StackTrace ?? "").Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            var trimmed = line.Trim();
            var at = trimmed.IndexOf(" in ", StringComparison.Ordinal);
            Console.WriteLine($"{indent}{(at > 0 ? trimmed[..at] : trimmed).Replace("Junior.CSharpFundamentals.ExceptionHandling.Domain.", "")}");
        }
    }
}
