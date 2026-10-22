using Junior.CSharpFundamentals.ExceptionHandling.Domain;

namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section04_ThrowVsThrowEx
{
    public static void Run()
    {
        Console.WriteLine("Chain:  Entry() → Middle() → ThrowSite()  ← this is where the error is ACTUALLY thrown. Entry rethrows in its catch.\n");

        Show("catch { throw; }                       (CORRECT)", Rethrow.Capture(Rethrow.Mode.ThrowKeyword));
        Show("catch (Exception ex) { throw ex; }     (WRONG — the stack trace is LOST)", Rethrow.Capture(Rethrow.Mode.ThrowEx));

        var wrapped = Rethrow.Capture(Rethrow.Mode.Wrap);
        Show("catch (...) { throw new X(\"..\", ex); } (wrapping — inner is PRESERVED)", wrapped);
        Console.WriteLine("      └─ InnerException stack:");
        PrintFrames(wrapped.InnerException!, indent: "         ");

        Show("ExceptionDispatchInfo.Capture(ex).Throw()", Rethrow.Capture(Rethrow.Mode.DispatchInfo));

        Console.WriteLine("""

        CONCLUSION
          • `throw;`   → the original stack trace is preserved. The CORRECT way to rethrow.
          • `throw ex;` → the stack trace restarts at the catch point; ThrowSite/Middle disappear → you can't tell WHERE the error originated.
          • If you want to add context: a new exception + the `innerException` parameter (no information is lost).
          • ExceptionDispatchInfo: to throw an exception caught in a catch from somewhere else/another thread with its original stack
            (the async infrastructure rethrows Task.Exception at `await` with this mechanism).
          • If you won't handle it at all, don't write a catch; catch only if you will DO something (log, clean up, translate).
          • `throw;` is valid only inside a catch block.
        """);
    }

    private static void Show(string title, Exception ex)
    {
        Console.WriteLine($"▶ {title}");
        Console.WriteLine($"   {ex.GetType().Name}: {ex.Message}");
        PrintFrames(ex, indent: "   ");
        Console.WriteLine($"   Is ThrowSite in the stack? → {ex.StackTrace?.Contains("ThrowSite") == true}\n");
    }

    private static void PrintFrames(Exception ex, string indent)
    {
        foreach (var line in (ex.StackTrace ?? "").Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            var trimmed = line.Trim();
            var at = trimmed.IndexOf(" in ", StringComparison.Ordinal);
            Console.WriteLine($"{indent}{(at > 0 ? trimmed[..at] : trimmed).Replace("Junior.CSharpFundamentals.ExceptionHandling.Domain.", "")}");
        }
    }
}
