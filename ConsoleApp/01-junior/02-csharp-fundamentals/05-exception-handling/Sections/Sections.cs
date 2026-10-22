namespace Junior.CSharpFundamentals.ExceptionHandling;

public sealed record Section(int Number, string Title, Func<Task> Run)
{
    public Section(int number, string title, Action run) : this(number, title, () => { run(); return Task.CompletedTask; }) { }
}

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "try / catch / finally, using / await using",          Section01_TryCatchFinally.Run),
        new(2, "Exception hierarchy and custom domain exceptions",    Section02_CustomExceptions.Run),
        new(3, "Exception filter: catch (X ex) when (...)",           Section03_Filters.Run),
        new(4, "throw vs throw ex — losing the stack trace ⭐",       Section04_ThrowVsThrowEx.Run),
        new(5, "Exception cost: don't use it for control flow",       Section05_Cost.Run),
        new(6, "What to catch, what not to catch?",                   Section06_WhatToCatch.Run),
        new(7, "Exceptions inside a Task and AggregateException",     Section07_TaskExceptions.RunAsync),
        new(8, "Exception vs Result return (conceptual intro)",       Section08_ExceptionVsResult.Run),
    ];
}
