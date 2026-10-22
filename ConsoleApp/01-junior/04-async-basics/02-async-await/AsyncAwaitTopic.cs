namespace Junior.AsyncBasics.AsyncAwait;

public static class AsyncAwaitTopic
{
    public static Task<int> Run(string[] args) => args is ["async-void-child"]
        ? Section03_AsyncVoid.RunChildAsync()             // the child process of section 3 that will crash (NOT called from the main menu)
        : Shared.SectionRunner.RunAsync("4.2", "async/await in Practice", Sections.All, args);
}
