using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace Junior.CSharpFundamentals.ExceptionHandling.Domain;

/// <summary>
/// A minimal chain showing the difference between `throw;` and `throw ex;`:  Entry → Middle → ThrowSite (where the error is ACTUALLY thrown).
/// [MethodImpl(NoInlining)]: if the JIT inlines the methods they vanish from the stack trace; it is turned off so the experiment is accurate.
/// </summary>
public static class Rethrow
{
    public enum Mode { ThrowKeyword, ThrowEx, Wrap, DispatchInfo }

    public static Exception Capture(Mode mode)
    {
        try { Entry(mode); }
        catch (Exception ex) { return ex; }
        throw new InvalidOperationException("the expected exception was not thrown");
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void Entry(Mode mode)
    {
        try
        {
            Middle();
        }
        catch (Exception ex)
        {
            switch (mode)
            {
                case Mode.ThrowKeyword:
                    throw;                                              // ✓ stack trace is PRESERVED (the original throw site stays)
                case Mode.ThrowEx:
#pragma warning disable CA2200 // ON PURPOSE: analyzer CA2200 catches exactly this mistake (compiler warning: 'Re-throwing caught exception changes stack information')
                    throw ex;                                           // ✗ stack trace is RESET here; the error looks as if it originated in Entry
#pragma warning restore CA2200
                case Mode.Wrap:
                    throw new InvalidOperationException("Entry wrapped it", ex);   // ✓ new exception + keep the original as InnerException
                case Mode.DispatchInfo:
                    ExceptionDispatchInfo.Capture(ex).Throw();          // ✓ capture the original stack and rethrow from ELSEWHERE (e.g. another thread)
                    break;
            }
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void Middle() => ThrowSite();

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowSite() => throw new InvalidOperationException("original error");
}
