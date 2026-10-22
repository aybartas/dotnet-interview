using System.Text.Json;

namespace Junior.BuildRunBasics.BinObj;

public static class Section02_RuntimeConfig
{
    public static void Run()
    {
        var asm = typeof(Section02_RuntimeConfig).Assembly.GetName().Name!;
        var dir = AppContext.BaseDirectory;

        var runtimeConfigPath = Path.Combine(dir, asm + ".runtimeconfig.json");
        Console.WriteLine($"── {Path.GetFileName(runtimeConfigPath)} (hangi runtime ile çalışacağını söyler) ──");
        var runtimeConfig = File.ReadAllText(runtimeConfigPath);
        Console.WriteLine(runtimeConfig);

        using (var doc = JsonDocument.Parse(runtimeConfig))
        {
            var options = doc.RootElement.GetProperty("runtimeOptions");
            // Tek framework → "framework" nesnesi; birden çok framework (Microsoft.NETCore.App + Microsoft.AspNetCore.App) → "frameworks" dizisi
            var frameworks = options.TryGetProperty("frameworks", out var many) ? many.EnumerateArray().ToList() : [options.GetProperty("framework")];
            Console.WriteLine($"Okunan: tfm = {options.GetProperty("tfm").GetString()}; frameworks = {string.Join(", ", frameworks.Select(f => $"{f.GetProperty("name").GetString()} {f.GetProperty("version").GetString()}"))}");
            Console.WriteLine("  → 'framework-dependent' uygulama: runtime UYGULAMAYLA GELMEZ, makinede kurulu .NET kullanılır (self-contained olsaydı 'includedFrameworks' görürdük → M2.5).");
            Console.WriteLine("  → Bu projede FrameworkReference Microsoft.AspNetCore.App var (J4'ün yerel Kestrel API'si için); o yüzden 'framework' yerine 'frameworks' dizisi yazıldı.");
            if (options.TryGetProperty("configProperties", out var props))
                Console.WriteLine($"  configProperties (GC/globalization/…): {string.Join(", ", props.EnumerateObject().Select(p => $"{p.Name}={p.Value}"))}");
        }

        var depsPath = Path.Combine(dir, asm + ".deps.json");
        Console.WriteLine($"\n── {Path.GetFileName(depsPath)} (bağımlılık manifesti) ──");
        using var deps = JsonDocument.Parse(File.ReadAllText(depsPath));
        var target = deps.RootElement.GetProperty("runtimeTarget").GetProperty("name").GetString();
        var libraries = deps.RootElement.GetProperty("libraries").EnumerateObject().Select(l => l.Name).ToList();
        Console.WriteLine($"runtimeTarget : {target}");
        Console.WriteLine($"libraries     : {libraries.Count} adet → {string.Join(", ", libraries)}");
        Console.WriteLine("  (Bu projede yalnızca kendi assembly'miz; NuGet paketi eklenirse burada ve bin/'de görünür. Framework assembly'leri burada listelenmez.)");

        Console.WriteLine("""

        Kim okur?
          • apphost/hostfxr → runtimeconfig.json: "Microsoft.NETCore.App 10.0.0 gerekiyor" → makinedeki uygun sürümü seçer (roll-forward).
          • hostpolicy      → deps.json: yüklenecek assembly'lerin yollarını çözer.
        Eksik runtime → "You must install or update .NET to run this application" / "Framework ... was not found" hatası (→ J1.4 soru 18).
        """);
    }
}
