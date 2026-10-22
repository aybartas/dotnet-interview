using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.RunBuildPublish;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Commands and launchSettings.json: for development only ⭐", Section01_CommandsAndLaunchSettings.Run),
        new(2, "🛠 Produce the same project with build and publish, compare the folders ⭐", Section02_BuildVsPublish.Run),
        new(3, "Deployment models: framework-dependent, self-contained, AOT",  Section03_DeploymentModels.Run),
    ];
}
