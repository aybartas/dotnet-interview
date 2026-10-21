using Junior.CSharpFundamentals.ExceptionHandling.Domain;

namespace Junior.CSharpFundamentals.ExceptionHandling.Tests;

/// <summary>`throw;` vs `throw ex;` — stack trace karşılaştırması TESTLE kanıtlanır.</summary>
public class RethrowTests
{
    [Fact]
    public void Throw_keyword_preserves_the_original_stack_trace()
    {
        var ex = Rethrow.Capture(Rethrow.Mode.ThrowKeyword);

        Assert.Contains("ThrowSite", ex.StackTrace);          // hatanın GERÇEK doğduğu yer görünür
        Assert.Contains("Middle", ex.StackTrace);
        Assert.Contains("Entry", ex.StackTrace);
    }

    [Fact]
    public void Throw_ex_resets_the_stack_trace_to_the_catch_site()
    {
        var ex = Rethrow.Capture(Rethrow.Mode.ThrowEx);

        Assert.DoesNotContain("ThrowSite", ex.StackTrace);    // özgün fırlatma noktası KAYBOLDU
        Assert.DoesNotContain("Middle", ex.StackTrace);
        Assert.Contains("Entry", ex.StackTrace);              // sanki hata Entry'de doğmuş gibi
    }

    [Fact]
    public void Wrapping_with_inner_exception_keeps_the_original_stack_in_InnerException()
    {
        var ex = Rethrow.Capture(Rethrow.Mode.Wrap);

        Assert.Equal("Entry sarmaladı", ex.Message);
        Assert.NotNull(ex.InnerException);
        Assert.Contains("ThrowSite", ex.InnerException.StackTrace);
        Assert.Equal("özgün hata", ex.InnerException.Message);
    }

    [Fact]
    public void ExceptionDispatchInfo_keeps_the_original_frames_and_marks_the_boundary()
    {
        var ex = Rethrow.Capture(Rethrow.Mode.DispatchInfo);

        Assert.Contains("ThrowSite", ex.StackTrace);
        Assert.Contains("End of stack trace from previous location", ex.StackTrace);
    }
}
