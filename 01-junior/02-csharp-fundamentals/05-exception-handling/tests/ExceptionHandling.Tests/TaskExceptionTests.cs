using Junior.CSharpFundamentals.ExceptionHandling.Domain;

namespace Junior.CSharpFundamentals.ExceptionHandling.Tests;

public class TaskExceptionTests
{
    private static async Task FailAsync(string name, int delayMs = 1)
    {
        await Task.Delay(delayMs, TestContext.Current.CancellationToken);
        throw new InvalidOperationException($"{name} başarısız");
    }

    [Fact]
    public async Task Calling_an_async_method_does_not_throw_the_exception_is_stored_in_the_task()
    {
        var task = FailAsync("A");                    // çağrı FIRLATMADI

        await Assert.ThrowsAsync<InvalidOperationException>(() => task);
        Assert.True(task.IsFaulted);
    }

    [Fact]
    public void Wait_wraps_in_AggregateException_while_await_unwraps()
    {
        var ex = Assert.Throws<AggregateException>(() => FailAsync("B").Wait(TestContext.Current.CancellationToken));
        Assert.IsType<InvalidOperationException>(ex.InnerExceptions.Single());
    }

    [Fact]
    public async Task WhenAll_await_throws_the_first_but_task_Exception_holds_all()
    {
        var all = Task.WhenAll(FailAsync("C1", 1), FailAsync("C2", 5), FailAsync("C3", 10));

        var thrown = await Assert.ThrowsAsync<InvalidOperationException>(() => all);

        Assert.Equal("C1 başarısız", thrown.Message);
        Assert.Equal(3, all.Exception!.InnerExceptions.Count);
    }

    [Fact]
    public void AggregateException_Flatten_unnests_inner_aggregates()
    {
        var nested = new AggregateException(new AggregateException(new TimeoutException()), new InvalidOperationException());
        Assert.Equal(2, nested.Flatten().InnerExceptions.Count);
        var types = nested.Flatten().InnerExceptions.Select(e => e.GetType()).ToHashSet();
        Assert.Contains(typeof(TimeoutException), types);
        Assert.Contains(typeof(InvalidOperationException), types);
    }

    [Fact]
    public async Task Cancellation_surfaces_as_OperationCanceledException()
    {
        using var cts = new CancellationTokenSource(10);
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => Task.Delay(5000, cts.Token));
    }
}

public class ResultTests
{
    [Fact]
    public void Successful_result_exposes_the_value()
    {
        var r = AccountService.TryWithdraw(100m, 40m);
        Assert.True(r.IsSuccess);
        Assert.Equal(60m, r.Value);
    }

    [Fact]
    public void Failed_result_carries_an_error_instead_of_throwing()
    {
        var r = AccountService.TryWithdraw(100m, 250m);
        Assert.False(r.IsSuccess);
        Assert.Contains("yetersiz", r.Error);
        Assert.Throws<InvalidOperationException>(() => r.Value);       // başarısız sonucun değerine erişmek bug'dır
    }

    [Fact]
    public void Match_forces_the_caller_to_handle_both_outcomes()
    {
        Assert.Equal("ok:60", AccountService.TryWithdraw(100m, 40m).Match(v => $"ok:{v}", e => $"err:{e}"));
        Assert.StartsWith("err:", AccountService.TryWithdraw(100m, 250m).Match(v => $"ok:{v}", e => $"err:{e}"));
    }

    [Fact]
    public void Exception_version_throws_the_same_business_failure()
        => Assert.Throws<InsufficientFundsException>(() => AccountService.WithdrawOrThrow(100m, 250m));
}
