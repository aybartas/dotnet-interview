using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.BinObj;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "🛠 bin/ içeriği: her dosya ne işe yarar?",                  Section01_BinListing.Run),
        new(2, "🛠 runtimeconfig.json ve deps.json'ı okumak",               Section02_RuntimeConfig.Run),
        new(3, "obj/ = ara çıktılar: assets, GlobalUsings, AssemblyInfo",   Section03_ObjFolder.Run),
        new(4, "CopyToOutputDirectory, .gitignore ve dotnet clean",         Section04_CopyAndClean.Run),
    ];
}
