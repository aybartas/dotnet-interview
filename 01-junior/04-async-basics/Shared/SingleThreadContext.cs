using System.Collections.Concurrent;

namespace Junior.AsyncBasics.Shared;

/// <summary>
/// Klasik UI iş parçacığının (WinForms/WPF) ve eski ASP.NET'in davranışını taklit eden tek thread'li SynchronizationContext.
/// <c>await</c>'ten sonra devam kodu bu thread'e geri gönderilir (Post). Thread bloklanırsa devam kodları çalışamaz → deadlock / "donmuş UI".
/// ASP.NET Core'un böyle bir context'i YOKTUR; bu sınıf yalnızca etkiyi güvenle göstermek içindir.
/// </summary>
public sealed class SingleThreadContext : SynchronizationContext, IDisposable
{
    private readonly BlockingCollection<(SendOrPostCallback Callback, object? State)> _queue = [];
    private readonly Thread _thread;

    public int ThreadId => _thread.ManagedThreadId;

    public SingleThreadContext(string name = "UI")
    {
        _thread = new Thread(Loop) { IsBackground = true, Name = name };   // background: deadlock olsa bile process kapanabilsin
        _thread.Start();
    }

    public override void Post(SendOrPostCallback d, object? state) => _queue.Add((d, state));

    private void Loop()
    {
        SetSynchronizationContext(this);
        foreach (var (callback, state) in _queue.GetConsumingEnumerable())
            callback(state);
    }

    /// <summary>İşi bu context'in thread'inde başlatır; dönen Task işin tamamlanmasını temsil eder.</summary>
    public Task<T> InvokeAsync<T>(Func<Task<T>> work)
    {
        var tcs = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
        Post(async _ =>
        {
            try { tcs.SetResult(await work()); }
            catch (Exception ex) { tcs.SetException(ex); }
        }, null);
        return tcs.Task;
    }

    public Task InvokeAsync(Func<Task> work) => InvokeAsync(async () => { await work(); return 0; });

    public void Dispose() => _queue.CompleteAdding();
}
