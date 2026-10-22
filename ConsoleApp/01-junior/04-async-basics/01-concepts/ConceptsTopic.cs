using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.Concepts;

public static class ConceptsTopic
{
    public static Task<int> Run(string[] args) => SectionRunner.RunAsync("4.1", "Core Concepts (concurrency, parallelism, I/O vs CPU)", Sections.All, args);
}
