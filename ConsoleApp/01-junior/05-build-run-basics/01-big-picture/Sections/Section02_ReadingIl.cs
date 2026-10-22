using System.Reflection;

namespace Junior.BuildRunBasics.BigPicture;

public static class Section02_ReadingIl
{
    public static void Run()
    {
        Console.WriteLine("IL = a stack-based, platform-independent intermediate language. For 'a + b': push two values onto the stack, add, return the result.\n");
        Show(nameof(Samples.Add), "public static int Add(int a, int b) => a + b;");
        Show(nameof(Samples.SumArray), "foreach (var item in items) sum += item;   // over an array: there is NO foreach in IL, just an indexed loop");
        Show(nameof(Samples.SumList), "foreach (var item in items) sum += item;   // over a List<T>: GetEnumerator/MoveNext/Current + try/finally");
        Show(nameof(Samples.ReadFirstLine), "using var reader = new StreamReader(path); ...  // using: try/finally + Dispose");
        Show(nameof(Samples.Greet), "$\"Hello {name}, {count}\"   // interpolation: DefaultInterpolatedStringHandler calls");

        Console.WriteLine("""

        Takeaways:
          • First/last lines: ldarg = push argument onto the stack, ldloc/stloc = local variable, add/ret = add/return, call/callvirt = method call,
            br/blt/brtrue = branching (if/for/while all lower to these).
          • 'foreach' and 'using' are a LANGUAGE feature — IL has an equivalent, but no 'foreach'/'using' instruction (→ section 6).
          • Real tools: ILSpy (opens a .dll as C#/IL), ildasm, dotnet-ildasm, sharplab.io (shows the IL and the 'lowered' C# of your code live).
            Try it: open bin/Debug/net10.0/ConsoleApp.dll in ILSpy and look at BigPicture → the Samples class.
        """);
    }

    private static void Show(string methodName, string source)
    {
        var method = typeof(Samples).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static)!;
        Console.WriteLine($"── {source}");
        foreach (var line in IlDisassembler.Disassemble(method)) Console.WriteLine($"     {line}");
        Console.WriteLine();
    }
}
