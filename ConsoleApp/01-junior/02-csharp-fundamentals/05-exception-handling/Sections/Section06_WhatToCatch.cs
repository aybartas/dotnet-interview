namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section06_WhatToCatch
{
    public static void Run()
    {
        Console.WriteLine("""
        SHOULD CATCH (an error you KNOW what to do about):
          • An expected, recoverable situation:  catch (FileNotFoundException) → load the default configuration
          • Transient error + retry:             catch (HttpRequestException ex) when (ex.StatusCode is >= 500) → try again
          • TRANSLATE at a layer boundary:       catch (SqlException ex) → new OrderRepositoryException(…, ex)   (keep the inner)
          • When cleanup/compensation is needed: catch { roll back; throw; }   (or finally / using)

        SHOULD NOT CATCH:
          • Errors that are your own BUGs: NullReferenceException, IndexOutOfRangeException, InvalidCastException, ArgumentNullException (the caller's fault).
            Catching and silencing them hides the bug → fix them, don't catch them (guard clause / null check).
          • An error you don't know what to do about: catching, logging and SWALLOWING it = the worst. If you don't know, let it propagate.
          • Unrecoverable situations: OutOfMemoryException (mostly), StackOverflowException (CANNOT be caught, the process dies), ThreadAbort/AccessViolation.
          • Empty catch:  catch { }  → the "no error" lie. At least log it.

        WHEN IS catch (Exception) LEGITIMATE?  Only at BOUNDARIES, when you know what to do:
          1) The TOP level of the application / global handler (ASP.NET Core exception middleware → 500 + log; the last-resort handler in Main)
          2) A background loop / worker: a single job failure must not kill the WHOLE service (log it, move on to the next job)
          3) A plugin/third-party call boundary: an error in external code must not break your process
          4) catch + (cleanup) + `throw;` : passing the Exception through WITHOUT ever swallowing it
          5) Test/CLI "last resort": a meaningful message for the user, the right exit code
        """);

        Console.WriteLine("Live example — worker loop (a boundary): one job blows up, the service stays up:");
        string[] jobs = ["job-1", "FAIL", "job-3"];
        foreach (var job in jobs)
        {
            try
            {
                if (job == "FAIL") throw new InvalidOperationException("job could not be processed");
                Console.WriteLine($"   {job} processed");
            }
            catch (Exception ex)                                       // legitimate: job boundary
            {
                Console.WriteLine($"   [LOG] {job} failed → {ex.GetType().Name}: {ex.Message}  (continuing)");
            }
        }

        Console.WriteLine("\nGuard clause: instead of CATCHING the caller's mistake, reject it EARLY and CLEARLY:");
        try { Process(null!); }
        catch (ArgumentNullException ex) { Console.WriteLine($"   Process(null) → {ex.GetType().Name} (ParamName={ex.ParamName}): the caller must be fixed, no handler should be written"); }

        Console.WriteLine("\nStackOverflow can't be caught → limit recursion (this experiment is deliberately NOT run).");
    }

    private static void Process(string input)
    {
        ArgumentNullException.ThrowIfNull(input);
    }
}
