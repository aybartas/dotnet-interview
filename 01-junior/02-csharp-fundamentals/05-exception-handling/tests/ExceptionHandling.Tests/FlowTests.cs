using Junior.CSharpFundamentals.ExceptionHandling.Domain;

namespace Junior.CSharpFundamentals.ExceptionHandling.Tests;

public class FlowTests
{
    [Fact]
    public void Finally_always_runs_and_runs_before_the_catch_of_an_outer_frame()
    {
        var log = new List<string>();
        try
        {
            try { log.Add("try"); throw new InvalidOperationException(); }
            finally { log.Add("finally"); }
        }
        catch (InvalidOperationException) { log.Add("outer catch"); }

        Assert.Equal(["try", "finally", "outer catch"], log);
    }

    [Fact]
    public void Finally_runs_after_return_value_is_computed()
    {
        var log = new List<string>();
        int Compute()
        {
            try { log.Add("return"); return 1; }
            finally { log.Add("finally"); }
        }

        Assert.Equal(1, Compute());
        Assert.Equal(["return", "finally"], log);
    }

    [Fact]
    public void Exception_filter_runs_before_inner_finally_and_catch_body_runs_after()
    {
        var order = new List<string>();
        try
        {
            try { throw new InvalidOperationException(); }
            finally { order.Add("inner finally"); }
        }
        catch (Exception) when (Record(order, "filter")) { order.Add("catch body"); }

        Assert.Equal(["filter", "inner finally", "catch body"], order);
    }

    [Fact]
    public void Filter_returning_false_lets_the_exception_continue_to_outer_handlers()
    {
        var caught = "";
        var never = false;                       // sabit 'false' derleyici uyarısı üretir; değişken kullan
        try
        {
            try { throw new InvalidOperationException("x"); }
            catch (InvalidOperationException) when (never) { caught = "inner"; }
        }
        catch (InvalidOperationException) { caught = "outer"; }

        Assert.Equal("outer", caught);
    }

    [Fact]
    public void Using_disposes_the_resource_even_when_an_exception_is_thrown()
    {
        var res = new Tracking();
        void UseAndFail() { using (res) throw new InvalidOperationException(); }
        Assert.Throws<InvalidOperationException>(UseAndFail);
        Assert.True(res.Disposed);
    }

    [Fact]
    public void Catching_the_base_type_catches_derived_domain_exceptions_with_their_data()
    {
        DomainException? caught = null;
        try { throw new InsufficientFundsException("ACC-1", 100m, 250m); }
        catch (DomainException ex) { caught = ex; }

        var insufficient = Assert.IsType<InsufficientFundsException>(caught);
        Assert.Equal(150m, insufficient.Shortfall);
        Assert.Equal("INSUFFICIENT_FUNDS", insufficient.Code);
        Assert.Equal("ACC-1", insufficient.AccountId);
    }

    [Fact]
    public void Wrapping_infrastructure_errors_keeps_the_original_as_InnerException()
    {
        var inner = new TimeoutException("db");
        var wrapped = new OrderRepositoryException("Sipariş yüklenemedi", inner);
        Assert.Same(inner, wrapped.InnerException);
    }

    [Fact]
    public void Validation_exception_carries_all_errors()
    {
        var ex = new ValidationFailedException(new Dictionary<string, string> { ["email"] = "geçersiz", ["yas"] = "negatif" });
        Assert.Equal(2, ex.Errors.Count);
        Assert.Contains("email", ex.Message);
    }

    [Fact]
    public void Guard_clause_reports_the_parameter_name()
    {
        string? input = null;
        var ex = Assert.Throws<ArgumentNullException>(() => ArgumentNullException.ThrowIfNull(input));
        Assert.Equal("input", ex.ParamName);
    }

    [Fact]
    public void TryParse_never_throws_while_Parse_does()
    {
        Assert.False(int.TryParse("abc", out _));
        Assert.Throws<FormatException>(() => int.Parse("abc"));
    }

    private static bool Record(List<string> log, string entry) { log.Add(entry); return true; }

    private sealed class Tracking : IDisposable
    {
        public bool Disposed { get; private set; }
        public void Dispose() => Disposed = true;
    }
}
