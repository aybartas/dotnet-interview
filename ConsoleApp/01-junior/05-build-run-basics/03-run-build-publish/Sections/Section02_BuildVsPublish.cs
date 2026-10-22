using System.Diagnostics;
using System.Runtime.InteropServices;
using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.RunBuildPublish;

/// <summary>
/// 🛠 J5.3: Produce the same project with `dotnet build` and `dotnet publish`; compare the two folders and list the differences.
/// This section runs the dotnet CLI itself (the SDK is required; each command takes a few seconds). To compare two folders BY HAND:
///   dotnet run -- J5.3 compare &lt;folderA&gt; &lt;folderB&gt;
/// </summary>
public static class Section02_BuildVsPublish
{
    public static void Run()
    {
        var projectDir = ProjectLocator.FindProjectDirectory();
        if (projectDir is null)
        {
            Console.WriteLine("Project folder not found (you are running from a published copy): this section builds/publishes the source project.");
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
            Console.WriteLine($"Working folder: {work}  (deleted at the end of the section)\n");
            Console.WriteLine("1) dotnet build   -c Release -o <A>");
            Dotnet($"build \"{csproj}\" -c Release -o \"{build}\"");

            Console.WriteLine("2) dotnet publish -c Release -o <B>              (framework-dependent, default)");
            Dotnet($"publish \"{csproj}\" -c Release -o \"{publish}\"");

            Console.WriteLine($"3) dotnet publish -c Release -r {rid} --self-contained false -p:PublishSingleFile=true -o <C>");
            var singleOk = Dotnet($"publish \"{csproj}\" -c Release -r {rid} --self-contained false -p:PublishSingleFile=true -o \"{single}\"");

            Console.WriteLine("\n── A (build) ↔ B (publish) ──");
            CompareDirectories(build, publish, "build", "publish");

            if (singleOk && Directory.Exists(single))
            {
                Console.WriteLine("\n── B (publish) ↔ C (publish, single file) ──");
                CompareDirectories(publish, single, "publish", "single file");
            }
        }
        finally
        {
            if (Directory.Exists(work)) Directory.Delete(work, recursive: true);
        }

        Console.WriteLine("""

        Takeaways:
          • For a simple framework-dependent console app the build and publish outputs are SIMILAR: both are .dll + apphost + .pdb + .deps.json + .runtimeconfig.json.
            What makes the difference is the extra things publish can do:
          • Location and default configuration: build → bin/Debug/net10.0/ (Debug); publish → bin/Release/net10.0/publish/ (Release, optimized IL).
          • Publish is the "unit of deployment": it collects only what is needed to run (in web projects wwwroot/static files, web.config); development files
            (launchSettings.json, obj/, source code) are left out. The build folder is for the IDE/running tests.
          • Options only publish offers: single file (PublishSingleFile), self-contained (-r + runtime included), trimming, ReadyToRun, Native AOT.
            Step 3 above is a small example: the .dll, .deps.json and .runtimeconfig.json are embedded into a single executable (they don't show up as separate files).
            The size grows to ~65-70 MB: this SDK seems to bundle the .NET runtime too when single file is requested — state the single-file vs self-contained
            distinction EXPLICITLY with -p:SelfContained=true/false (details → M2.5).
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
        Console.WriteLine($"   → {(ok ? "ok" : $"FAILED (exit code {process.ExitCode})")} ({sw.Elapsed.TotalSeconds:N1} s)");
        if (!ok)
        {
            var lines = (stdout.Result + stderr.Result).Split('\n', StringSplitOptions.RemoveEmptyEntries).Take(4);
            foreach (var l in lines) Console.WriteLine($"     {l.Trim()}");
        }
        return ok;
    }

    /// <summary>Compares the file lists (name + size) of two folders. `dotnet run -- J5.3 compare A B` ends up here.</summary>
    public static int CompareDirectories(string a, string b, string labelA = "A", string labelB = "B")
    {
        if (!Directory.Exists(a) || !Directory.Exists(b))
        {
            Console.Error.WriteLine("One of the folders does not exist: " + (Directory.Exists(a) ? b : a));
            return 1;
        }

        var filesA = List(a);
        var filesB = List(b);
        Console.WriteLine($"   {labelA}: {filesA.Count} files, {ProjectLocator.HumanSize(filesA.Values.Sum())}   ·   {labelB}: {filesB.Count} files, {ProjectLocator.HumanSize(filesB.Values.Sum())}");

        var onlyA = filesA.Keys.Except(filesB.Keys).Order().ToList();
        var onlyB = filesB.Keys.Except(filesA.Keys).Order().ToList();
        var different = filesA.Keys.Intersect(filesB.Keys).Where(k => filesA[k] != filesB[k]).Order().ToList();
        var same = filesA.Keys.Intersect(filesB.Keys).Count(k => filesA[k] == filesB[k]);

        Console.WriteLine($"   Same size in both: {same} files");
        foreach (var f in onlyA) Console.WriteLine($"   only in {labelA} : {f,-40} {ProjectLocator.HumanSize(filesA[f])}");
        foreach (var f in onlyB) Console.WriteLine($"   only in {labelB} : {f,-40} {ProjectLocator.HumanSize(filesB[f])}");
        foreach (var f in different) Console.WriteLine($"   size differs   : {f,-40} {labelA}={ProjectLocator.HumanSize(filesA[f])}  {labelB}={ProjectLocator.HumanSize(filesB[f])}");
        return 0;
    }

    private static Dictionary<string, long> List(string dir) =>
        new DirectoryInfo(dir).EnumerateFiles("*", SearchOption.AllDirectories)
            .ToDictionary(f => Path.GetRelativePath(dir, f.FullName), f => f.Length);
}
