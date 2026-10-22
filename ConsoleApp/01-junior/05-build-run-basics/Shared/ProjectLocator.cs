namespace Junior.BuildRunBasics.Shared;

public static class ProjectLocator
{
    public const string ProjectFile = "ConsoleApp.csproj";

    /// <summary>Walks up from the output folder (bin/Debug/net10.0) to find the project folder; returns null in a published copy.</summary>
    public static string? FindProjectDirectory()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null)
        {
            if (File.Exists(Path.Combine(dir.FullName, ProjectFile))) return dir.FullName;
            dir = dir.Parent;
        }
        return null;
    }

    public static string HumanSize(long bytes) => bytes switch
    {
        < 1024 => $"{bytes} B",
        < 1024 * 1024 => $"{bytes / 1024.0:N1} KB",
        _ => $"{bytes / 1024.0 / 1024.0:N1} MB",
    };
}
