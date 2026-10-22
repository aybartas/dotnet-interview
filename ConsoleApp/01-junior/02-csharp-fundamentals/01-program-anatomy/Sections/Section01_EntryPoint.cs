using System.Reflection;

namespace Junior.CSharpFundamentals.ProgramAnatomy;

/// <summary>
/// Entry point — valid signatures:
/// <code>
/// static void Main()                      static void Main(string[] args)
/// static int  Main()                      static int  Main(string[] args)         // return value = exit code
/// static async Task Main()                static async Task Main(string[] args)
/// static async Task&lt;int&gt; Main()           static async Task&lt;int&gt; Main(string[] args)
/// </code>
/// All of them are <c>static</c>; the access modifier doesn't matter (it can be private). An assembly can have only ONE entry point.
/// </summary>
public static class Section01_EntryPoint
{
    public static void Run()
    {
        // Let's use reflection to see the entry point the compiler GENERATED for Program.cs (top-level statements):
        var entry = Assembly.GetEntryAssembly()!.EntryPoint!;
        Console.WriteLine($"Entry point type     : {entry.DeclaringType!.Name}   (we didn't write it, the compiler generated it)");
        Console.WriteLine($"Entry point method   : {entry.Name}   (not a valid C# name → can't be called by hand)");
        Console.WriteLine($"Parameters           : {string.Join(", ", entry.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"))}");
        Console.WriteLine($"Return type          : {entry.ReturnType.Name}   (Task<int> because Program.cs has 'await' and 'return <int>')");

        // Command-line arguments (the first element is not the program name)
        Console.WriteLine($"args                 : [{string.Join(", ", Environment.GetCommandLineArgs().Skip(1))}]");

        // Exit code: the number reported to the operating system when the process ends. Unhandled exception → usually a non-zero code (134 = SIGABRT on Linux).
        Console.WriteLine("\nTry:  dotnet run -- J2.1 exit-code 42   then   echo $?   →  42");
    }
}
