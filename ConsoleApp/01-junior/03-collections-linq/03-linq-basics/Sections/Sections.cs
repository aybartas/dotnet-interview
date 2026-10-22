namespace Junior.CollectionsLinq.LinqBasics;

public sealed record Section(int Number, string Title, Action Run);

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Query syntax vs method syntax",                              Section01_Syntax.Run),
        new(2, "Deferred vs immediate execution ⭐",                         Section02_DeferredExecution.Run),
        new(3, "Tour of the core operators",                                 Section03_Operators.Run),
        new(4, "First / FirstOrDefault / Single / SingleOrDefault ⭐",       Section04_FirstSingle.Run),
        new(5, "Any() vs Count() > 0 and multiple enumeration",              Section05_AnyCountMultipleEnumeration.Run),
        new(6, "New operators: CountBy, AggregateBy, Index (.NET 9); LeftJoin, RightJoin (.NET 10)", Section06_NewOperators.Run),
        new(7, "Introduction to IEnumerable<T> vs IQueryable<T> ⭐",         Section07_EnumerableVsQueryable.Run),
        new(8, "20 report exercises (e-commerce data)",                      Section08_Reports.Run),
    ];
}
