using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.RunBuildPublish;

public static class RunBuildPublishTopic
{
    public static int Run(string[] args) => args is ["compare", var a, var b]
        ? Section02_BuildVsPublish.CompareDirectories(a, b)
        : SectionRunner.Run("5.3", "run vs build vs publish", Sections.All, args);
}
