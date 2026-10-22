using Junior.CSharpFundamentals.ProgramAnatomy.AccessModifiers;

namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class ProgramAnatomyTopic
{
    public static int Run(string[] args)
    {
        // ═══════════════════════════════════════════════════════════════════════════════════════
        // J2.1 — Program Anatomy. This file ITSELF is a "top-level statements" example:
        // we wrote no class or Main; the compiler generated a Program class and a <Main>$ method (Section01 shows this).
        //
        //   dotnet run -- J2.1                      → section list
        //   dotnet run -- J2.1 5                 → run section 5
        //   dotnet run -- J2.1 all               → all of them
        //   dotnet run -- J2.1 exit-code 42      → ends the process with exit code 42 (then:  echo $?)
        //
        // `args`: command-line arguments (the program name is NOT included, unlike C).
        // `return <int>`: the process exit code. 0 = success, non-zero = failure (CI/scripts look at this).
        // If you use `await`, the compiler turns Main into `async Task<int> Main`.
        // ═══════════════════════════════════════════════════════════════════════════════════════

        if (args is ["exit-code", var code] && int.TryParse(code, out var exitCode))
        {
            Console.WriteLine($"Exiting with exit code {exitCode}. Check: Linux/macOS `echo $?`, PowerShell `$LASTEXITCODE`, cmd `echo %ERRORLEVEL%`");
            return exitCode;                      // return = Main's return value = exit code
        }

        if (args is ["access-modifiers"])        // access modifiers experiment (Vault)
        {
            AccessModifiersDemo.Run();
            return 0;
        }

        var sections = Sections.All;

        if (args.Length == 0)
        {
            Console.WriteLine("J2.1 Program Anatomy — sections:\n");
            foreach (var s in sections) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine("\nTo run: dotnet run -- J2.1 <number>  |  all  |  exit-code <n>  |  access-modifiers");
            return 0;
        }

        foreach (var section in args[0] == "all" ? sections : sections.Where(s => s.Number.ToString() == args[0]))
        {
            Console.WriteLine($"\n━━━ {section.Number}. {section.Title} ━━━");
            section.Run();
        }

        return 0;
    }
}
