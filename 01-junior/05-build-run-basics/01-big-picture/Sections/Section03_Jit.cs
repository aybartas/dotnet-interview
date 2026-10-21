using System.Diagnostics;
using System.Runtime;

namespace Junior.BuildRunBasics.BigPicture;

public static class Section03_Jit
{
    public static void Run()
    {
        Console.WriteLine("JIT (Just-In-Time compiler): IL'i, bir metot İLK ÇAĞRILDIĞINDA makine koduna çevirir; sonraki çağrılar çevrilmiş kodu kullanır.\n");

        var methodsBefore = JitInfo.GetCompiledMethodCount();
        var timeBefore = JitInfo.GetCompilationTime();

        var t0 = Stopwatch.GetTimestamp();
        var r1 = Samples.JitMeOnce(7);
        var firstCall = Stopwatch.GetElapsedTime(t0);

        var methodsAfterFirst = JitInfo.GetCompiledMethodCount();

        var t1 = Stopwatch.GetTimestamp();
        var r2 = Samples.JitMeOnce(8);
        var secondCall = Stopwatch.GetElapsedTime(t1);

        var methodsAfterSecond = JitInfo.GetCompiledMethodCount();

        Console.WriteLine($"JitMeOnce(7) ilk çağrı   : {firstCall.TotalMicroseconds,8:N1} µs   JIT'lenen metot sayısı +{methodsAfterFirst - methodsBefore}  (sonuç {r1})");
        Console.WriteLine($"JitMeOnce(8) ikinci çağrı: {secondCall.TotalMicroseconds,8:N1} µs   JIT'lenen metot sayısı +{methodsAfterSecond - methodsAfterFirst}  (sonuç {r2})");
        Console.WriteLine($"Süreç başından beri: {JitInfo.GetCompiledMethodCount()} metot JIT'lendi, toplam JIT süresi {JitInfo.GetCompilationTime().TotalMilliseconds:N1} ms (bu bölüm öncesi {timeBefore.TotalMilliseconds:N1} ms)");

        Console.WriteLine("""

        Neden JIT?
          • IL platformdan bağımsızdır; AYNI .dll x64, ARM64, Windows, Linux'ta çalışır. JIT, çalıştığı makinenin işlemcisi için en iyi kodu üretir.
          • Maliyet: uygulamanın İLK çağrılarında JIT süresi (startup gecikmesi). Çözümler: ReadyToRun / Native AOT (önceden derleme, → M2.5, S1).
          • Tiered compilation (varsayılan): önce hızlı/az optimize kod (tier-0), sık çağrılan metotlar sonra yeniden ve daha iyi derlenir (tier-1).
        Bir cümleyle: "JIT, IL'i çalışma zamanında, metot ilk çağrıldığında native koda çevirir."
        """);
    }
}
