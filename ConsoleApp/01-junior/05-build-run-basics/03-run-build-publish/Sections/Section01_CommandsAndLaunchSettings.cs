using System.Diagnostics;
using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.RunBuildPublish;

public static class Section01_CommandsAndLaunchSettings
{
    public static void Run()
    {
        Console.WriteLine("""
        ┌──────────────────┬───────────────────────────────────┬──────────────────────────────────┬──────────────────────────────┐
        │ Command          │ What it does                      │ Output                           │ When                         │
        ├──────────────────┼───────────────────────────────────┼──────────────────────────────────┼──────────────────────────────┤
        │ dotnet restore   │ Resolves/downloads dependencies   │ obj/project.assets.json          │ Explicit step in CI          │
        │ dotnet build     │ Compiles (implicit restore)       │ bin/<config>/<tfm>/              │ Development, build validation│
        │ dotnet run       │ Builds AND runs                   │ A running process                │ Development ONLY             │
        │ dotnet watch     │ Re-runs on change                 │ —                                │ Development loop             │
        │ dotnet publish   │ Produces a DEPLOYMENT-READY folder│ bin/<config>/<tfm>/publish/      │ DEPLOYMENT                   │
        │ dotnet test      │ Builds and runs the tests         │ Test results                     │ CI                           │
        │ dotnet pack      │ Produces a NuGet package          │ .nupkg                           │ Publishing a library         │
        └──────────────────┴───────────────────────────────────┴──────────────────────────────────┴──────────────────────────────┘
        Default configuration: build/run = Debug, publish = Release (.NET 8+).
        """);

        var demoEnv = Environment.GetEnvironmentVariable("DEMO_ENV");
        var processName = Path.GetFileNameWithoutExtension(Environment.ProcessPath!);
        Console.WriteLine($"How was this program started?  ProcessPath = {Environment.ProcessPath}");
        Console.WriteLine($"DEMO_ENV environment variable  = {(demoEnv is null ? "(not set)" : $"\"{demoEnv}\"")}");

        var launchSettingsInBin = File.Exists(Path.Combine(AppContext.BaseDirectory, "launchSettings.json"));
        var projectDir = ProjectLocator.FindProjectDirectory();
        var launchSettingsInSource = projectDir is not null && File.Exists(Path.Combine(projectDir, "Properties", "launchSettings.json"));
        Console.WriteLine($"Is launchSettings.json in the source (Properties/)? {launchSettingsInSource}  ·  in the output folder (bin/)? {launchSettingsInBin}");

        Console.WriteLine($"""

        Observations:
          • `dotnet run` reads the profile in Properties/launchSettings.json and GIVES DEMO_ENV to the app → above {(demoEnv is null ? "it shows as not set, which means the program was started directly, not with dotnet run" : "a value showed up: dotnet run applied the profile")}.
          • When run directly (bin/Debug/net10.0/{typeof(Section01_CommandsAndLaunchSettings).Assembly.GetName().Name} or `dotnet <dll>`) launchSettings.json is NEVER read → DEMO_ENV is not set.
          • launchSettings.json is only for Visual Studio / `dotnet run`; it does NOT go into bin/ or the publish output.
            Production settings come from environment variables (ASPNETCORE_ENVIRONMENT, connection string…), appsettings.<Environment>.json or a secret store (Key Vault).

        Try it:
            dotnet run -- J5.3 1                                                    → DEMO_ENV is set (launchSettings)
            dotnet bin/Debug/net10.0/{typeof(Section01_CommandsAndLaunchSettings).Assembly.GetName().Name}.dll J5.3 1   → DEMO_ENV is not set
            dotnet run --no-launch-profile -- J5.3 1                                → don't use launchSettings at all

        ⭐ Why isn't `dotnet run` used in production?
          1. It needs the SDK (build tools on the server/in the container = big image, big attack surface).
          2. It does a build/up-to-date check on every run (slow startup, unexpected rebuilds).
          3. Source code and build tools sit on the server.
          4. launchSettings.json (a development profile) kicks in — it is NOT a production setting.
          Production: the `dotnet publish` output + only the runtime (framework-dependent) or self-contained/AOT.
        """);
    }
}
