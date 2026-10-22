using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.BinObj;

public static class BinObjTopic
{
    public static int Run(string[] args) => SectionRunner.Run("5.2", "bin/ and obj/", Sections.All, args);
}
