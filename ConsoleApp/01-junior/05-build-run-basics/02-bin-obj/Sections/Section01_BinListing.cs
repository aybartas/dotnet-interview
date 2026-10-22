using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.BinObj;

public static class Section01_BinListing
{
    public static void Run()
    {
        var dir = AppContext.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);
        var asm = typeof(Section01_BinListing).Assembly.GetName().Name!;
        Console.WriteLine($"Output folder (bin): {dir}\n");

        Console.WriteLine($"{"File",-42} {"Size",10}  What is it for?");
        Console.WriteLine(new string('─', 110));
        foreach (var file in new DirectoryInfo(dir).EnumerateFiles().OrderBy(f => f.Name, StringComparer.Ordinal))
            Console.WriteLine($"{file.Name,-42} {ProjectLocator.HumanSize(file.Length),10}  {Describe(file.Name, asm)}");

        foreach (var sub in new DirectoryInfo(dir).EnumerateDirectories())
            Console.WriteLine($"{sub.Name + "/",-42} {"",10}  subfolder ({sub.EnumerateFiles("*", SearchOption.AllDirectories).Count()} files): may hold localized/runtime-specific resources");

        Console.WriteLine("""

        Mapping table (README 3.2): .dll = your code · apphost = native launcher · .pdb = debug symbols · .deps.json = dependency manifest
        · .runtimeconfig.json = runtime/framework settings · other .dll = NuGet dependencies · appsettings.json = content brought by CopyToOutputDirectory.
        This project has no NuGet packages, so there is no other .dll; when you add a package (e.g. Newtonsoft.Json), its .dll is copied to bin/ too.
        """);
    }

    private static string Describe(string name, string asm)
    {
        if (name == asm + ".dll") return "★ YOUR CODE — IL + metadata (assembly)";
        if (name == asm || name == asm + ".exe") return "apphost — native launcher that starts the .dll (.exe on Windows, no extension on Linux/macOS)";
        if (name == asm + ".pdb") return "debug symbols: IL ↔ source line mapping (what makes breakpoints and stack-trace line numbers work)";
        if (name == asm + ".deps.json") return "dependency manifest: which assemblies/packages; hostpolicy reads it";
        if (name == asm + ".runtimeconfig.json") return "which runtime/framework version, roll-forward, GC/settings; apphost/hostfxr reads it";
        if (name == "appsettings.json") return "content file copied via CopyToOutputDirectory (<None ... CopyToOutputDirectory> in the csproj)";
        if (name.EndsWith(".dll", StringComparison.Ordinal)) return "NuGet/project dependency (library)";
        if (name.EndsWith(".xml", StringComparison.Ordinal)) return "XML documentation (GenerateDocumentationFile)";
        return "?";
    }
}
