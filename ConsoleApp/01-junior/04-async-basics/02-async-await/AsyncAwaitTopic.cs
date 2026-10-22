namespace Junior.AsyncBasics.AsyncAwait;

public static class AsyncAwaitTopic
{
    public static Task<int> Run(string[] args) => args is ["async-void-child"]
        ? Section03_AsyncVoid.RunChildAsync()             // 3. bölümün çökecek alt process'i (ana menüden ÇAĞRILMAZ)
        : Shared.SectionRunner.RunAsync("4.2", "async/await Kullanımı", Sections.All, args);
}
