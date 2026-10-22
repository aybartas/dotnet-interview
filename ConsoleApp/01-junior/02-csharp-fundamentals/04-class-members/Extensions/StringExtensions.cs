namespace Junior.CSharpFundamentals.ClassMembers.Extensions;

// EXTENSION METHOD rules:
//   • In a static class, a static method whose FIRST parameter is marked with 'this' → it is called as if it were an instance method of that type.
//   • Nothing is actually added: the compiler turns the call 'x.Shout()' into the static call 'StringExtensions.Shout(x)'.
//   • The calling file MUST have a 'using' for this namespace (otherwise it is invisible).
//   • An instance method ALWAYS takes precedence; an extension with the same signature is never called.
//   • Works with a null receiver (because it is really a static call): x.IsBlank() doesn't throw NullReferenceException even if x is null.
public static class StringExtensions
{
    public static string Shout(this string text) => text.ToUpperInvariant() + "!";

    public static bool IsBlank(this string? text) => string.IsNullOrWhiteSpace(text);

    public static string Truncate(this string text, int max) => text.Length <= max ? text : text[..max] + "…";

    // Generic extension: on IEnumerable<T> — all of LINQ is written this way (Where, Select, OrderBy…)
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : class
    {
        foreach (var item in source)
            if (item is not null) yield return item;
    }
}

public sealed class Widget
{
    public string Describe() => "instance method";           // ← the instance ALWAYS wins
}

public static class WidgetExtensions
{
    public static string Describe(this Widget w) => "extension method";                          // same signature → NEVER called (shadowed)
    public static string Describe(this Widget w, string prefix) => $"{prefix}: extension method"; // different signature → can be called
}
