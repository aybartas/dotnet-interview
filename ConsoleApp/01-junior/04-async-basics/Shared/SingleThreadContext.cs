using System.Collections.Concurrent;

namespace Junior.AsyncBasics.Shared;

/// <summary>
/// A single-threaded SynchronizationContext that mimics the behaviour of the classic UI thread (WinForms/WPF) and legacy ASP.NET.
/// After an <c>await</c>, the continuation is posted back to this thread (Post). If the thread is blocked, continuations cannot run → deadlock / "frozen UI".
/// ASP.NET Core has NO such context; this class exists only to demonstrate the effect safely.
/// </summary>
public sealed class SingleThreadContext : SynchronizationContext, IDisposable
{
    private readonly BlockingCollection<(SendOrPostCallback Callback, object? State)> _queue = [];
    private readonly Thread _thread;

    public int ThreadId => _thread.ManagedThreadId;

    public SingleThreadContext(string name = "UI")
    {
        _thread = new Thread(Loop) { IsBackground = true, Name = name };   // background: so the process can still exit even if there is a deadlock
        _thread.Start();
    }

    public override void Post(SendOrPostCallback d, object? state) => _queue.Add((d, state));

    private void Loop()
    {
        SetSynchronizationContext(this);
        foreach (var (callback, state) in _queue.GetConsumingEnumerable())
            callback(state);
    }

    /// <summary>Starts the work on this context's thread; the returned Task represents the completion of the work.</summary>
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
