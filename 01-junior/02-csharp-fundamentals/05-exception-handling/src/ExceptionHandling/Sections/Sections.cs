namespace Junior.CSharpFundamentals.ExceptionHandling;

public sealed record Section(int Number, string Title, Func<Task> Run)
{
    public Section(int number, string title, Action run) : this(number, title, () => { run(); return Task.CompletedTask; }) { }
}

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "try / catch / finally, using / await using",       Section01_TryCatchFinally.Run),
        new(2, "Exception hiyerarşisi ve custom domain exception'lar", Section02_CustomExceptions.Run),
        new(3, "Exception filter: catch (X ex) when (...)",         Section03_Filters.Run),
        new(4, "throw vs throw ex — stack trace kaybı ⭐",          Section04_ThrowVsThrowEx.Run),
        new(5, "Exception maliyeti: kontrol akışı için kullanma",   Section05_Cost.Run),
        new(6, "Neyi yakalamalı, neyi yakalamamalı?",               Section06_WhatToCatch.Run),
        new(7, "Task içindeki exception'lar ve AggregateException", Section07_TaskExceptions.RunAsync),
        new(8, "Exception vs Result dönüşü (kavramsal giriş)",      Section08_ExceptionVsResult.Run),
    ];
}
