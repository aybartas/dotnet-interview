namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section07_TaskExceptions
{
    public static async Task RunAsync()
    {
        Console.WriteLine("1) An exception in an async method is stored INSIDE the Task; calling the method does NOT throw, await/Wait/Result does:");
        var task = FailAsync("A", delayMs: 10);
        Console.WriteLine($"   FailAsync was called, nothing thrown yet. Status = {await WaitForStatus(task)}");
        try { await task; }
        catch (InvalidOperationException ex) { Console.WriteLine($"   await → the original type was thrown: {ex.GetType().Name}: {ex.Message}"); }

        Console.WriteLine("\n2) .Wait()/.Result WRAPS in AggregateException; await does NOT wrap (it unwraps the first inner exception):");
        try { FailAsync("B", 5).Wait(); }
        catch (AggregateException ex) { Console.WriteLine($"   .Wait() → {ex.GetType().Name} → InnerExceptions[0]: {ex.InnerExceptions[0].Message}"); }
        Console.WriteLine("   (.Wait()/.Result is sync-over-async: it also risks a deadlock → J4. Prefer await.)");

        Console.WriteLine("\n3) Task.WhenAll: if several fail, await throws ONLY THE FIRST; ALL of them are in task.Exception.InnerExceptions:");
        var all = Task.WhenAll(FailAsync("C1", 5), FailAsync("C2", 10), FailAsync("C3", 15));
        try { await all; }
        catch (Exception ex) { Console.WriteLine($"   await threw: {ex.Message}   (to see the others use all.Exception!.InnerExceptions)"); }
        Console.WriteLine($"   all.Exception.InnerExceptions: [{string.Join(", ", all.Exception!.InnerExceptions.Select(e => e.Message))}]");

        Console.WriteLine("\n4) AggregateException.Flatten() / Handle(): flattens nested aggregates, handles the known ones");
        var nested = new AggregateException(new AggregateException(new TimeoutException("t1")), new InvalidOperationException("i1"));
        Console.WriteLine($"   Flatten: [{string.Join(", ", nested.Flatten().InnerExceptions.Select(e => e.GetType().Name))}]");

        Console.WriteLine("\n5) Cancellation (CancellationToken): OperationCanceledException is NOT an error, it's normal flow — caught separately");
        using var cts = new CancellationTokenSource(20);
        try { await Task.Delay(1000, cts.Token); }
        catch (OperationCanceledException) { Console.WriteLine("   cancelled (TaskCanceledException also derives from OperationCanceledException)"); }

        Console.WriteLine("""

        ⚠ An exception thrown in an `async void` method CANNOT be caught (the caller gets no Task) and can crash the process → async void only for event handlers.
        ⚠ An exception in a Task that is never awaited/forgotten stays invisible (UnobservedTaskException; in .NET Core it doesn't crash the process but the error is LOST) → await every Task.
        The deep version (async internals, WhenAll/WhenAny, AggregateException details) → J4, M4.
        """);
    }

    private static async Task FailAsync(string name, int delayMs)
    {
        await Task.Delay(delayMs);
        throw new InvalidOperationException($"{name} failed");
    }

    private static async Task<TaskStatus> WaitForStatus(Task task)
    {
        _ = await Task.WhenAny(task, Task.Delay(100));
        return task.Status;                                          // Faulted: the exception is stored inside the Task
    }
}
