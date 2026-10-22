namespace Junior.BuildRunBasics.BinObj;

public static class Section04_CopyAndClean
{
    public static void Run()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        Console.WriteLine($"1) Is appsettings.json in bin/? {File.Exists(path)}   (in the source: 02-bin-obj/appsettings.json)");
        if (File.Exists(path)) Console.WriteLine($"   content: {File.ReadAllText(path).ReplaceLineEndings(" ").Trim()}");

        Console.WriteLine("""

           In the csproj:
             <None Include="02-bin-obj/appsettings.json" Link="appsettings.json" CopyToOutputDirectory="PreserveNewest" />

           CopyToOutputDirectory values:
             Never           → not copied (default)
             PreserveNewest  → copies if the source is newer (usually the right choice)
             Always          → copies on every build
           In web projects appsettings.json gets this automatically from the SDK; for your own content files (template, certificate, SQL script) you write it yourself.
           If the file doesn't reach bin/, the app says "file not found" at run time — the most common cause is forgetting this line.

        2) Why are bin/ and obj/ in .gitignore?
             • Both can be GENERATED from source (build output) → in the repo they mean noise, merge conflicts, machine-specific paths, bloat.
             • Standard .NET .gitignore: bin/ obj/  (+ .vs/, *.user). Same in this repo.

        3) When is dotnet clean needed?
             dotnet clean      → deletes generated files in bin/<config>/ and obj/ (does not delete obj/project.assets.json)
             Reasons: the "it builds but the old .dll runs" suspicion, odd errors after a package/SDK change, freeing disk space.
             Thorough cleanup: delete the bin/ and obj/ folders by hand and run `dotnet build` again (restore included). First step of troubleshooting: delete bin/obj → rebuild.
        """);
    }
}
