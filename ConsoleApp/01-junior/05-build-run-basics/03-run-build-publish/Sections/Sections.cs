using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.RunBuildPublish;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Komutlar ve launchSettings.json: yalnızca geliştirme içindir ⭐", Section01_CommandsAndLaunchSettings.Run),
        new(2, "🛠 Aynı projeyi build ve publish ile çıkar, klasörleri karşılaştır ⭐", Section02_BuildVsPublish.Run),
        new(3, "Deployment modelleri: framework-dependent, self-contained, AOT",  Section03_DeploymentModels.Run),
    ];
}
