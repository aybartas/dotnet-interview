using System.Diagnostics;
using System.Reflection;

namespace Junior.BuildRunBasics.BigPicture;

public static class Section04_DebugVsRelease
{
    public static void Run()
    {
        var assembly = typeof(Samples).Assembly;
        var debuggable = assembly.GetCustomAttribute<DebuggableAttribute>();
        var configuration = assembly.GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration ?? "?";
        var optimizerDisabled = debuggable?.IsJITOptimizerDisabled ?? false;

        Console.WriteLine($"Build configuration of this .dll      : {configuration}");
        Console.WriteLine($"Is JIT optimization disabled?         : {optimizerDisabled}   (DebuggableAttribute.IsJITOptimizerDisabled)");

#if DEBUG
        Console.WriteLine("Is the DEBUG symbol defined?          : YES (the #if DEBUG block was compiled)");
#else
        Console.WriteLine("Is the DEBUG symbol defined?          : NO (the #if DEBUG block was REMOVED at compile time)");
#endif

        var assertRan = false;
        Debug.Assert(Mark(ref assertRan), "Debug.Assert argument");         // [Conditional("DEBUG")]: in Release both the call AND the argument expression are removed
        Console.WriteLine($"Did the expression inside Debug.Assert run: {assertRan}   (false in Release → don't put side-effecting expressions in Assert!)\n");

        var addIl = IlDisassembler.Disassemble(typeof(Samples).GetMethod(nameof(Samples.AddWithLocal))!);
        Console.WriteLine($"IL of AddWithLocal ('var result = a + b; return result;') — {addIl.Count} instructions, {addIl.Count(l => l.Contains(" nop"))} 'nop':");
        foreach (var line in addIl) Console.WriteLine($"     {line}");

        Console.WriteLine("""

        Run it twice to compare:
            dotnet run -- J5.1 4                 (Debug: nops + the redundant local stay in the IL, optimization is off)
            dotnet run -c Release -- J5.1 4      (Release: IL gets simpler, no DEBUG symbol, JIT optimization is on)

        Debug vs Release:
          • Compiler optimization  : Debug OFF (nops, temporaries) · Release ON (IL gets simpler)
          • JIT optimization       : Debug off (tier-0 / minopts) · Release on (tiered → tier-1)
          • DEBUG symbol           : defined in Debug (#if DEBUG works) · not defined in Release
          • Debug.Assert, [Conditional("DEBUG")] : run in Debug · call AND argument expressions are REMOVED in Release
          • Debugger experience    : Debug: line by line, every variable visible · Release: variables "optimized away", lines jump around
          • Speed / size           : Debug slow and large · Release fast and small
        • `dotnet build` / `dotnet run` default to Debug; `dotnet publish` defaults to Release (.NET 8+). Debug output must not go to production.
        • "Works in Debug, fails in Release" is a real class of problems (side-effecting asserts, timing/races, #if DEBUG differences). Test both.
        """);
    }

    private static bool Mark(ref bool flag)
    {
        flag = true;
        return true;
    }
}
