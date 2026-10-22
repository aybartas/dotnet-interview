using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.CancellationBasics;

public static class CancellationBasicsTopic
{
    public static Task<int> Run(string[] args) => SectionRunner.RunAsync("4.3", "Cancellation Basics", Sections.All, args);
}
