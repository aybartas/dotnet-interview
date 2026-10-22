using System.Diagnostics;

namespace Junior.AsyncBasics.AsyncAwait;

/// <summary>🛠 J4.2: Demo showing how an exception inside async void crashes the application (the crash happens in a separate child process).</summary>
public static class Section03_AsyncVoid
{
    private static async Task FailsAsync()
    {
        await Task.Delay(50);
        throw new InvalidOperationException("thrown from inside async Task");
    }

    private static async void FailsVoid()                       // ⚠ async void: the caller CANNOT get a Task → there is no way to catch the error
    {
        await Task.Delay(100);
        throw new InvalidOperationException("thrown from inside async void");
    }

    /// <summary>Entry point of the child process: the error from async void is not caught by try/catch and crashes the process.</summary>
    public static async Task<int> RunChildAsync()
    {
        Console.WriteLine("[child process] calling the async void method inside try/catch...");
        try
        {
            FailsVoid();
            Console.WriteLine("[child process] the call RETURNED without error (the exception has not been thrown yet: it will be thrown after the await, on another thread)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[child process] CAUGHT: {ex.Message}");        // never runs
        }

        await Task.Delay(1000);
        Console.WriteLine("[child process] THIS LINE IS NEVER REACHED — the process has already crashed");
        return 0;
    }

    public static async Task Run()
    {
        // 1) async Task: the error is carried inside the Task; whoever awaits it catches it
        try
        {
            await FailsAsync();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"1) async Task  → CAUGHT during await: {ex.Message}\n");
        }

        // 2) async void: the error is thrown onto the SynchronizationContext/ThreadPool → unhandled exception → the process dies
        Console.WriteLine("2) async void  → the same error, this time run in a separate process (so that the crash does not kill this demo):\n");

        var psi = new ProcessStartInfo(Environment.ProcessPath!)
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        if (Path.GetFileNameWithoutExtension(Environment.ProcessPath!).Equals("dotnet", StringComparison.OrdinalIgnoreCase))
            psi.ArgumentList.Add(typeof(Section03_AsyncVoid).Assembly.Location);          // if run as dotnet <dll> ..., the dll path is required
        psi.ArgumentList.Add("J4.2");
        psi.ArgumentList.Add("async-void-child");

        using var child = Process.Start(psi)!;
        var stdout = child.StandardOutput.ReadToEndAsync();
        var stderr = child.StandardError.ReadToEndAsync();
        if (!child.WaitForExit(15_000)) { child.Kill(); Console.WriteLine("   (child process timed out)"); }

        foreach (var line in (await stdout).Split('\n', StringSplitOptions.RemoveEmptyEntries)) Console.WriteLine($"   {line.TrimEnd()}");
        foreach (var line in (await stderr).Split('\n', StringSplitOptions.RemoveEmptyEntries).Take(2)) Console.WriteLine($"   stderr> {line.TrimEnd()}");
        Console.WriteLine($"   child process exit code: {child.ExitCode}   (not 0: the process CRASHED)\n");

        Console.WriteLine("""
        Why?
          • The caller of an async void method gets no Task → it can neither await it nor catch the error with try/catch; it cannot track its completion either.
          • The exception is thrown after the await (on another thread); since nobody catches it, it is an "unhandled exception" → in .NET Core the process shuts down.
          • In UI applications the error is posted to the SynchronizationContext → the app crashes. In ASP.NET Core: the whole server process dies.
          • It cannot be tested (xUnit/NUnit cannot wait for it to finish).

        The only legitimate place: EVENT HANDLERS (a void signature is mandatory). Then the body is wrapped in try/catch:
            button.Click += async (s, e) => { try { await SaveAsync(); } catch (Exception ex) { ShowError(ex); } };
        Everywhere else: async Task. If you need fire-and-forget: a wrapper that logs the error, or a background queue (→ M12).
        Analyzer: VSTHRD100 (Microsoft.VisualStudio.Threading.Analyzers package) flags async void methods — make it a warning/error.
        """);
    }
}
