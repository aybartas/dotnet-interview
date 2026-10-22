using System.Diagnostics;
using System.Runtime.InteropServices;
using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.RunBuildPublish;

/// <summary>
/// 🛠 J5.3: Aynı projeyi `dotnet build` ve `dotnet publish` ile çıkar; iki klasörü karşılaştırıp farkları listele.
/// Bu bölüm dotnet CLI'ı kendisi çalıştırır (SDK gerekir; her komut birkaç saniye sürer). İki klasörü ELLE karşılaştırmak için:
///   dotnet run -- J5.3 compare &lt;klasörA&gt; &lt;klasörB&gt;
/// </summary>
public static class Section02_BuildVsPublish
{
    public static void Run()
    {
        var projectDir = ProjectLocator.FindProjectDirectory();
        if (projectDir is null)
        {
            Console.WriteLine("Proje klasörü bulunamadı (publish edilmiş kopyadan çalışıyorsunuz): bu bölüm kaynak projeyi build/publish eder.");
            return;
        }

        var csproj = Path.Combine(projectDir, ProjectLocator.ProjectFile);
        var work = Path.Combine(Path.GetTempPath(), $"j5-build-publish-{Environment.ProcessId}");
        var build = Path.Combine(work, "build");
        var publish = Path.Combine(work, "publish");
        var single = Path.Combine(work, "publish-single-file");
        var rid = RuntimeInformation.RuntimeIdentifier;

        try
        {
            Console.WriteLine($"Çalışma klasörü: {work}  (bölüm sonunda silinir)\n");
            Console.WriteLine("1) dotnet build   -c Release -o <A>");
            Dotnet($"build \"{csproj}\" -c Release -o \"{build}\"");

            Console.WriteLine("2) dotnet publish -c Release -o <B>              (framework-dependent, varsayılan)");
            Dotnet($"publish \"{csproj}\" -c Release -o \"{publish}\"");

            Console.WriteLine($"3) dotnet publish -c Release -r {rid} --self-contained false -p:PublishSingleFile=true -o <C>");
            var singleOk = Dotnet($"publish \"{csproj}\" -c Release -r {rid} --self-contained false -p:PublishSingleFile=true -o \"{single}\"");

            Console.WriteLine("\n── A (build) ↔ B (publish) ──");
            CompareDirectories(build, publish, "build", "publish");

            if (singleOk && Directory.Exists(single))
            {
                Console.WriteLine("\n── B (publish) ↔ C (publish, tek dosya) ──");
                CompareDirectories(publish, single, "publish", "tek dosya");
            }
        }
        finally
        {
            if (Directory.Exists(work)) Directory.Delete(work, recursive: true);
        }

        Console.WriteLine("""

        Çıkarımlar:
          • Basit bir framework-dependent console uygulamasında build ve publish çıktıları BENZERDİR: ikisi de .dll + apphost + .pdb + .deps.json + .runtimeconfig.json.
            Farkı yaratan, publish'in ekstra yapabildikleridir:
          • Konum ve varsayılan yapılandırma: build → bin/Debug/net10.0/ (Debug); publish → bin/Release/net10.0/publish/ (Release, optimizasyonlu IL).
          • Publish "dağıtım birimi"dir: yalnızca çalışmak için gerekenleri toplar (web projelerinde wwwroot/statik dosyalar, web.config); geliştirme dosyaları
            (launchSettings.json, obj/, kaynak kod) girmez. Build klasörü ise IDE/test çalıştırma içindir.
          • Yalnızca publish'in sunduğu seçenekler: tek dosya (PublishSingleFile), self-contained (-r + runtime dahil), trimming, ReadyToRun, Native AOT.
            Yukarıdaki 3. adım bunun küçük bir örneğidir: .dll, .deps.json ve .runtimeconfig.json tek çalıştırılabilir dosyaya gömülür (ayrı dosya olarak görünmezler).
            Boyut ~65-70 MB'a çıkar: bu SDK tek dosya isteğinde .NET runtime'ını da pakete katmış görünüyor — tek dosya + self-contained ayrımını
            -p:SelfContained=true/false ile AÇIKÇA belirtin (ayrıntı → M2.5).
        """);
    }

    private static bool Dotnet(string arguments)
    {
        var sw = Stopwatch.StartNew();
        var psi = new ProcessStartInfo("dotnet", arguments + " --nologo -v q")
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        using var process = Process.Start(psi)!;
        var stdout = process.StandardOutput.ReadToEndAsync();
        var stderr = process.StandardError.ReadToEndAsync();
        process.WaitForExit();
        var ok = process.ExitCode == 0;
        Console.WriteLine($"   → {(ok ? "tamam" : $"BAŞARISIZ (çıkış kodu {process.ExitCode})")} ({sw.Elapsed.TotalSeconds:N1} sn)");
        if (!ok)
        {
            var lines = (stdout.Result + stderr.Result).Split('\n', StringSplitOptions.RemoveEmptyEntries).Take(4);
            foreach (var l in lines) Console.WriteLine($"     {l.Trim()}");
        }
        return ok;
    }

    /// <summary>İki klasörün dosya listesini (ad + boyut) karşılaştırır. `dotnet run -- J5.3 compare A B` buraya gelir.</summary>
    public static int CompareDirectories(string a, string b, string labelA = "A", string labelB = "B")
    {
        if (!Directory.Exists(a) || !Directory.Exists(b))
        {
            Console.Error.WriteLine("Klasörlerden biri yok: " + (Directory.Exists(a) ? b : a));
            return 1;
        }

        var filesA = List(a);
        var filesB = List(b);
        Console.WriteLine($"   {labelA}: {filesA.Count} dosya, {ProjectLocator.HumanSize(filesA.Values.Sum())}   ·   {labelB}: {filesB.Count} dosya, {ProjectLocator.HumanSize(filesB.Values.Sum())}");

        var onlyA = filesA.Keys.Except(filesB.Keys).Order().ToList();
        var onlyB = filesB.Keys.Except(filesA.Keys).Order().ToList();
        var different = filesA.Keys.Intersect(filesB.Keys).Where(k => filesA[k] != filesB[k]).Order().ToList();
        var same = filesA.Keys.Intersect(filesB.Keys).Count(k => filesA[k] == filesB[k]);

        Console.WriteLine($"   Her ikisinde de aynı boyutta: {same} dosya");
        foreach (var f in onlyA) Console.WriteLine($"   yalnızca {labelA} içinde : {f,-40} {ProjectLocator.HumanSize(filesA[f])}");
        foreach (var f in onlyB) Console.WriteLine($"   yalnızca {labelB} içinde : {f,-40} {ProjectLocator.HumanSize(filesB[f])}");
        foreach (var f in different) Console.WriteLine($"   boyut farklı  : {f,-40} {labelA}={ProjectLocator.HumanSize(filesA[f])}  {labelB}={ProjectLocator.HumanSize(filesB[f])}");
        return 0;
    }

    private static Dictionary<string, long> List(string dir) =>
        new DirectoryInfo(dir).EnumerateFiles("*", SearchOption.AllDirectories)
            .ToDictionary(f => Path.GetRelativePath(dir, f.FullName), f => f.Length);
}
