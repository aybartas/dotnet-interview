namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section03_Filters
{
    public static void Run()
    {
        Console.WriteLine("1) Filter: catch is engaged only if the CONDITION is true; otherwise the exception carries on as if it had never been caught");
        foreach (var status in new[] { 404, 503 })
        {
            try { throw new HttpFailure(status); }
            catch (HttpFailure ex) when (ex.StatusCode == 404) { Console.WriteLine($"   {status} → the 404 filter caught it (not-found handling)"); }
            catch (HttpFailure ex) when (ex.StatusCode >= 500) { Console.WriteLine($"   {status} → the 5xx filter caught it (can be retried)"); }
        }

        Console.WriteLine("\n2) The filter runs BEFORE the stack unwinds → the stack and locals at the throw point are PRESERVED in a debugger/dump.");
        Console.WriteLine("   Ordering experiment (which comes first, the inner finally or the filter?):");
        try { Outer(); }
        catch (Exception ex) when (Log("filter ran", ex)) { Console.WriteLine("   catch body ran"); }
        Console.WriteLine("   → FILTER first, then the inner finally, then the catch body. (Had we checked the condition with an `if` INSIDE the catch, it would have been evaluated AFTER the inner finally —after the stack unwound.)");

        Console.WriteLine("\n3) The 'log without catching' pattern: the filter always returns false, the exception FLOW is not disturbed and the stack is intact");
        try
        {
            try { throw new InvalidOperationException("inner error"); }
            catch (Exception ex) when (LogAndContinue(ex)) { /* never enters */ }
        }
        catch (InvalidOperationException) { Console.WriteLine("   the outer catch caught it (thanks to the exception filter, neither stack nor behaviour changed)"); }

        Console.WriteLine("""

        Why a filter instead of if + throw; inside catch?
          • evaluated before the stack unwinds → better diagnostics (dump, debugger "first chance")
          • leaves an exception that won't be handled as if it had never been caught (no rethrow cost, no stack risk)
          • typical uses: HTTP status, error code, InnerException type, telling retryable errors apart, CancellationToken checks
        Don't throw exceptions inside a filter (they are swallowed and treated as false); a filter must be fast and side-effect free (a log side effect is acceptable).
        """);
    }

    private static void Outer()
    {
        try { throw new InvalidOperationException("root"); }
        finally { Console.WriteLine("   inner finally ran"); }
    }

    private static bool Log(string message, Exception ex)
    {
        Console.WriteLine($"   {message} ({ex.GetType().Name})");
        return true;
    }

    private static bool LogAndContinue(Exception ex)
    {
        Console.WriteLine($"   [log] {ex.Message}");
        return false;                                  // false → this catch does NOT MATCH, the exception carries on
    }

    private sealed class HttpFailure(int statusCode) : Exception($"HTTP {statusCode}")
    {
        public int StatusCode { get; } = statusCode;
    }
}
