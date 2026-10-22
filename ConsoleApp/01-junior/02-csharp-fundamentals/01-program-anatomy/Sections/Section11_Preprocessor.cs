#define DEMO_SYMBOL                  // Defines a compilation symbol for this file (must come BEFORE the usings; it is file-scoped)

#nullable enable
namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section11_Preprocessor
{
    public static void Run()
    {
        // #if / #elif / #else / #endif: CONDITIONAL COMPILATION — the unselected code never enters the compilation (isn't written to IL in Release)
#if DEBUG
        Console.WriteLine("Build: DEBUG configuration (dotnet run). For Release:  dotnet run -c Release");
#else
        Console.WriteLine("Build: RELEASE configuration");
#endif

#if DEMO_SYMBOL
        Console.WriteLine("DEMO_SYMBOL was #define'd in this file.");
#endif

#if NET10_0_OR_GREATER
        Console.WriteLine("Target framework: net10.0 or later  (NETxx_0_OR_GREATER symbols are defined automatically by the SDK; useful with multi-targeting)");
#endif

        // #region / #endregion: a region that is foldable ONLY in the editor. NO effect on compilation. (Large #regions are often a code smell showing the class should be split.)
        #region Foldable region
        Console.WriteLine("#region folds only in the IDE.");
        #endregion

        // #nullable: turn nullable analysis on/off per file/region
#nullable disable
        string legacy = null;                             // NO warning (nullable context is off: interop with legacy code)
#nullable restore
        Console.WriteLine($"'string legacy = null' in the #nullable disable region produced no warning → legacy is null: {legacy is null}");

        // #pragma warning: silence a specific warning regionally (write the REASON!)
#pragma warning disable CS0219                           // assigned-but-never-used variable warning
        var unused = 42;
#pragma warning restore CS0219
        Console.WriteLine("CS0219 was silenced only between the two lines with #pragma warning disable/restore.");

        // #error / #warning produce your own error/warning at compile time (e.g. an invalid symbol combination):
        // #error This configuration is not supported
    }
}
