using System.Diagnostics;
using System.Runtime;

namespace Junior.BuildRunBasics.BigPicture;

public static class Section03_Jit
{
    public static void Run()
    {
        Console.WriteLine("JIT (Just-In-Time compiler): turns IL into machine code the FIRST TIME a method is called; later calls use the compiled code.\n");

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

        Console.WriteLine($"JitMeOnce(7) first call  : {firstCall.TotalMicroseconds,8:N1} µs   JIT-compiled method count +{methodsAfterFirst - methodsBefore}  (result {r1})");
        Console.WriteLine($"JitMeOnce(8) second call : {secondCall.TotalMicroseconds,8:N1} µs   JIT-compiled method count +{methodsAfterSecond - methodsAfterFirst}  (result {r2})");
        Console.WriteLine($"Since process start: {JitInfo.GetCompiledMethodCount()} methods JIT-compiled, total JIT time {JitInfo.GetCompilationTime().TotalMilliseconds:N1} ms (before this section {timeBefore.TotalMilliseconds:N1} ms)");

        Console.WriteLine("""

        Why a JIT?
          • IL is platform independent; the SAME .dll runs on x64, ARM64, Windows, Linux. The JIT produces the best code for the CPU it runs on.
          • Cost: JIT time during the application's FIRST calls (startup delay). Fixes: ReadyToRun / Native AOT (ahead-of-time compilation, → M2.5, S1).
          • Tiered compilation (default): fast/lightly optimized code first (tier-0); frequently called methods are recompiled later with better optimization (tier-1).
        In one sentence: "The JIT turns IL into native code at run time, when a method is first called."
        """);
    }
}
