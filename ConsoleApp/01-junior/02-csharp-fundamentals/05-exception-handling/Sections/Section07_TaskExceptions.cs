namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section07_TaskExceptions
{
    public static async Task RunAsync()
    {
        Console.WriteLine("1) async metottaki exception Task'ın İÇİNE yazılır; metot çağrısı FIRLATMAZ, await/Wait/Result fırlatır:");
        var task = FailAsync("A", delayMs: 10);
        Console.WriteLine($"   FailAsync çağrıldı, henüz fırlatılmadı. Status = {await WaitForStatus(task)}");
        try { await task; }
        catch (InvalidOperationException ex) { Console.WriteLine($"   await → orijinal tip fırlatıldı: {ex.GetType().Name}: {ex.Message}"); }

        Console.WriteLine("\n2) .Wait()/.Result AggregateException ile SARAR; await SARMAZ (ilk iç exception'ı açar):");
        try { FailAsync("B", 5).Wait(); }
        catch (AggregateException ex) { Console.WriteLine($"   .Wait() → {ex.GetType().Name} → InnerExceptions[0]: {ex.InnerExceptions[0].Message}"); }
        Console.WriteLine("   (.Wait()/.Result sync-over-async: ayrıca deadlock riski → J4. await tercih edin.)");

        Console.WriteLine("\n3) Task.WhenAll: birden çok hata olursa await YALNIZCA İLKİNİ fırlatır; TÜMÜ task.Exception.InnerExceptions içindedir:");
        var all = Task.WhenAll(FailAsync("C1", 5), FailAsync("C2", 10), FailAsync("C3", 15));
        try { await all; }
        catch (Exception ex) { Console.WriteLine($"   await fırlattı: {ex.Message}   (diğerlerini görmek için all.Exception!.InnerExceptions)"); }
        Console.WriteLine($"   all.Exception.InnerExceptions: [{string.Join(", ", all.Exception!.InnerExceptions.Select(e => e.Message))}]");

        Console.WriteLine("\n4) AggregateException.Flatten() / Handle(): iç içe aggregate'leri düzleştirir, bilinenleri işler");
        var nested = new AggregateException(new AggregateException(new TimeoutException("t1")), new InvalidOperationException("i1"));
        Console.WriteLine($"   Flatten: [{string.Join(", ", nested.Flatten().InnerExceptions.Select(e => e.GetType().Name))}]");

        Console.WriteLine("\n5) İptal (CancellationToken): OperationCanceledException HATA değil, normal akıştır — ayrı yakalanır");
        using var cts = new CancellationTokenSource(20);
        try { await Task.Delay(1000, cts.Token); }
        catch (OperationCanceledException) { Console.WriteLine("   iptal edildi (TaskCanceledException da OperationCanceledException'dan türer)"); }

        Console.WriteLine("""

        ⚠ `async void` metotta fırlatılan exception YAKALANAMAZ (çağıran Task alamaz) ve süreci çökertebilir → async void yalnızca event handler'lar için.
        ⚠ Bekleyen/unutulan Task'taki exception görünmez kalır (UnobservedTaskException; .NET Core'da süreci çökertmez ama hata KAYBOLUR) → her Task'ı await edin.
        Derin hali (async iç mekanizma, WhenAll/WhenAny, AggregateException ayrıntısı) → J4, M4.
        """);
    }

    private static async Task FailAsync(string name, int delayMs)
    {
        await Task.Delay(delayMs);
        throw new InvalidOperationException($"{name} başarısız");
    }

    private static async Task<TaskStatus> WaitForStatus(Task task)
    {
        _ = await Task.WhenAny(task, Task.Delay(100));
        return task.Status;                                          // Faulted: exception Task'ın içinde saklandı
    }
}
