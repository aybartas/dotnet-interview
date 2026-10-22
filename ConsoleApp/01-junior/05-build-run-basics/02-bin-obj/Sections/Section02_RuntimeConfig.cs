using System.Text.Json;

namespace Junior.BuildRunBasics.BinObj;

public static class Section02_RuntimeConfig
{
    public static void Run()
    {
        var asm = typeof(Section02_RuntimeConfig).Assembly.GetName().Name!;
        var dir = AppContext.BaseDirectory;

        var runtimeConfigPath = Path.Combine(dir, asm + ".runtimeconfig.json");
        Console.WriteLine($"── {Path.GetFileName(runtimeConfigPath)} (tells which runtime to run on) ──");
        var runtimeConfig = File.ReadAllText(runtimeConfigPath);
        Console.WriteLine(runtimeConfig);

        using (var doc = JsonDocument.Parse(runtimeConfig))
        {
            var options = doc.RootElement.GetProperty("runtimeOptions");
            // Single framework → "framework" object; multiple frameworks (Microsoft.NETCore.App + Microsoft.AspNetCore.App) → "frameworks" array
            var frameworks = options.TryGetProperty("frameworks", out var many) ? many.EnumerateArray().ToList() : [options.GetProperty("framework")];
            Console.WriteLine($"Read: tfm = {options.GetProperty("tfm").GetString()}; frameworks = {string.Join(", ", frameworks.Select(f => $"{f.GetProperty("name").GetString()} {f.GetProperty("version").GetString()}"))}");
            Console.WriteLine("  → 'framework-dependent' app: the runtime does NOT ship with the app, the .NET installed on the machine is used (if it were self-contained we'd see 'includedFrameworks' → M2.5).");
            Console.WriteLine("  → This project has a FrameworkReference to Microsoft.AspNetCore.App (for J4's local Kestrel API); that is why a 'frameworks' array is written instead of 'framework'.");
            if (options.TryGetProperty("configProperties", out var props))
                Console.WriteLine($"  configProperties (GC/globalization/...): {string.Join(", ", props.EnumerateObject().Select(p => $"{p.Name}={p.Value}"))}");
        }

        var depsPath = Path.Combine(dir, asm + ".deps.json");
        Console.WriteLine($"\n── {Path.GetFileName(depsPath)} (dependency manifest) ──");
        using var deps = JsonDocument.Parse(File.ReadAllText(depsPath));
        var target = deps.RootElement.GetProperty("runtimeTarget").GetProperty("name").GetString();
        var libraries = deps.RootElement.GetProperty("libraries").EnumerateObject().Select(l => l.Name).ToList();
        Console.WriteLine($"runtimeTarget : {target}");
        Console.WriteLine($"libraries     : {libraries.Count} entries → {string.Join(", ", libraries)}");
        Console.WriteLine("  (Only our own assembly in this project; if a NuGet package is added it shows up here and in bin/. Framework assemblies are not listed here.)");

        Console.WriteLine("""

        Who reads them?
          • apphost/hostfxr → runtimeconfig.json: "Microsoft.NETCore.App 10.0.0 is required" → picks a suitable version on the machine (roll-forward).
          • hostpolicy      → deps.json: resolves the paths of the assemblies to load.
        Missing runtime → "You must install or update .NET to run this application" / "Framework ... was not found" error (→ J1.4 question 18).
        """);
    }
}
