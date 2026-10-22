namespace Junior.BuildRunBasics.BigPicture;

public static class Section05_CompileVsRuntime
{
    public static void Run()
    {
        Console.WriteLine("COMPILE-TIME ERRORS (Roslyn): the code doesn't compile, NO .dll is produced, the program never runs. Examples (commented out — uncomment them and the build breaks):");
        Console.WriteLine("""
              // int x = "hello";               → error CS0029: Cannot implicitly convert type 'string' to 'int'
              // Console.WriteLIne("a");        → error CS0117: 'Console' does not contain a definition for 'WriteLIne'   (typo)
              // int y;  Console.WriteLine(y);  → error CS0165: Use of unassigned local variable
              // string s = null;               → warning CS8600 (with nullable enabled): converting null to a non-nullable type (compiles, warns)
              // missing return / semicolon     → error CS1002, CS0161 ...
        """);

        Console.WriteLine("RUNTIME ERRORS (exceptions): the code COMPILES, the .dll is produced; the error is thrown when that line runs. Examples:\n");
        Try("InvalidCastException  ", () => { object o = "text"; _ = (int)o; });
        Try("IndexOutOfRange       ", () => { int[] a = [1, 2, 3]; var i = 5; _ = a[i]; });
        Try("NullReference         ", () => { string? s = Environment.GetEnvironmentVariable("NO_SUCH_VARIABLE"); _ = s!.Length; });
        Try("DivideByZero          ", () => { var zero = int.Parse("0"); _ = 10 / zero; });
        Try("FormatException       ", () => _ = int.Parse("abc"));
        Try("DirectoryNotFound    ", () => File.ReadAllText("/no/such/file.txt"));

        Console.WriteLine("""

        The difference:
          • When?        : compile-time → while the compiler analyzes the code · runtime → while the program runs, when that code path is executed
          • Result       : compile-time → no .dll, nothing runs · runtime → the .dll exists, the error reaches the user/log
          • Cost         : compile-time → cheap, the developer sees it immediately · runtime → expensive: in production, maybe only with specific data
          • Who catches? : compile-time → compiler, analyzers, nullable analysis · runtime → tests, logs, exception handling
        Strategy: move errors as EARLY as possible (to compile time) — types, nullable reference types (<Nullable>enable</Nullable>),
        analyzers, `required`, making invalid states unrepresentable with enums/records. Catch the rest with tests (→ J10).
        """);
    }

    private static void Try(string name, Action action)
    {
        try { action(); }
        catch (Exception ex) { Console.WriteLine($"   {name}: {ex.GetType().Name}: {ex.Message}"); }
    }
}
