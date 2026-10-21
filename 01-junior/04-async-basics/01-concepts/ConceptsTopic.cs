using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.Concepts;

public static class ConceptsTopic
{
    public static Task<int> Run(string[] args) => SectionRunner.RunAsync("4.1", "Temel Kavramlar", Sections.All, args);
}
