namespace Junior.BuildRunBasics.RunBuildPublish;

public static class Section03_DeploymentModels
{
    public static void Run()
    {
        Console.WriteLine("""
        A one-paragraph intro to the deployment models (in depth → M2.5):

        1) Framework-dependent (default)     Runtime: .NET installed ON THE MACHINE · Size: small (app only)
              dotnet publish -c Release
        2) Self-contained                    Runtime: ships WITH THE APP            · Size: large (~70+ MB)
              dotnet publish -c Release -r linux-x64 --self-contained
        3) Native AOT                        Runtime: none, code is compiled to native ahead of time · Size: small, fast startup
              dotnet publish -c Release -r linux-x64 -p:PublishAot=true

        • Framework-dependent: the .NET runtime must be installed on the server/in the container (mcr.microsoft.com/dotnet/aspnet); security patches come with runtime updates.
          If it is missing: "You must install or update .NET..." (the framework version in runtimeconfig.json can't be found → 5.2 Section 2).
        • Self-contained: no .NET needed on the target machine; the runtime version is locked to the app (you must republish to patch).
          `includedFrameworks` shows up in runtimeconfig.json; the output becomes hundreds of files (single file: -p:PublishSingleFile=true).
        • Native AOT: IL is compiled to machine code at publish time; there is no JIT → very fast startup, low memory; it has restrictions (reflection, dynamic code).
        • Extra options: -p:PublishTrimmed=true (drop unused code), -p:PublishReadyToRun=true (reduce JIT cost).
        Choice: corporate server/container + .NET installed → framework-dependent (default); isolated/client machine → self-contained; serverless/CLI/cold start → AOT.
        """);

        Console.WriteLine($"This program: {(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, typeof(Section03_DeploymentModels).Assembly.GetName().Name + ".runtimeconfig.json")).Contains("includedFrameworks") ? "self-contained" : "framework-dependent")} (we tell by checking whether runtimeconfig.json contains 'includedFrameworks')");
    }
}
