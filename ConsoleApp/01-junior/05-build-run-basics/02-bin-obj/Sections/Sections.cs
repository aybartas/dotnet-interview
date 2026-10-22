using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.BinObj;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "🛠 bin/ contents: what is each file for?",                   Section01_BinListing.Run),
        new(2, "🛠 Reading runtimeconfig.json and deps.json",                Section02_RuntimeConfig.Run),
        new(3, "obj/ = intermediate output: assets, GlobalUsings, AssemblyInfo", Section03_ObjFolder.Run),
        new(4, "CopyToOutputDirectory, .gitignore and dotnet clean",        Section04_CopyAndClean.Run),
    ];
}
