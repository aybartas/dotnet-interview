using System.Reflection;

namespace Junior.CSharpFundamentals.ProgramAnatomy;

/// <summary>
/// Entry point (giriş noktası) — geçerli imzalar:
/// <code>
/// static void Main()                      static void Main(string[] args)
/// static int  Main()                      static int  Main(string[] args)         // dönüş değeri = exit code
/// static async Task Main()                static async Task Main(string[] args)
/// static async Task&lt;int&gt; Main()           static async Task&lt;int&gt; Main(string[] args)
/// </code>
/// Hepsi <c>static</c>'tir; erişim belirleyicisi önemli değildir (private olabilir). Bir assembly'de TEK giriş noktası olabilir.
/// </summary>
public static class Section01_EntryPoint
{
    public static void Run()
    {
        // Derleyicinin Program.cs (top-level statements) için ÜRETTİĞİ giriş noktasını reflection ile görelim:
        var entry = Assembly.GetEntryAssembly()!.EntryPoint!;
        Console.WriteLine($"Giriş noktası tipi   : {entry.DeclaringType!.Name}   (biz yazmadık, derleyici üretti)");
        Console.WriteLine($"Giriş noktası metodu : {entry.Name}   (geçerli bir C# adı değil → elle çağrılamaz)");
        Console.WriteLine($"Parametreler         : {string.Join(", ", entry.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"))}");
        Console.WriteLine($"Dönüş tipi           : {entry.ReturnType.Name}   (Program.cs'te 'await' ve 'return <int>' olduğu için Task<int>)");

        // Komut satırı argümanları (ilk eleman program adı değildir)
        Console.WriteLine($"args                 : [{string.Join(", ", Environment.GetCommandLineArgs().Skip(1))}]");

        // Exit code: process bittiğinde işletim sistemine bildirilen sayı. Yakalanmayan exception → genelde sıfırdan farklı kod (Linux'ta 134 = SIGABRT).
        Console.WriteLine("\nDeneyin:  dotnet run -- exit-code 42   sonra   echo $?   →  42");
    }
}
