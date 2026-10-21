using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.BigPicture;

public static class BigPictureTopic
{
    public static int Run(string[] args) => SectionRunner.Run("5.1", "C# → IL → Native: Büyük Resim", Sections.All, args);
}
