namespace Junior.CSharpFundamentals.ClassMembers.Extensions;

// EXTENSION METHOD kuralları:
//   • static bir sınıfta, static metot, İLK parametre 'this' ile işaretli → o tipin sanki bir örnek metoduymuş gibi çağrılır.
//   • Gerçekte hiçbir şey eklenmez: derleyici 'x.Shout()' çağrısını 'StringExtensions.Shout(x)' statik çağrısına çevirir.
//   • Çağıran dosyada bu namespace'in 'using'i OLMALI (aksi halde görünmez).
//   • Örnek (instance) metot HER ZAMAN önceliklidir; aynı imzalı extension asla çağrılmaz.
//   • null receiver ile çalışır (çünkü gerçekte static çağrı): x.IsBlank() x null olsa bile NullReferenceException atmaz.
public static class StringExtensions
{
    public static string Shout(this string text) => text.ToUpperInvariant() + "!";

    public static bool IsBlank(this string? text) => string.IsNullOrWhiteSpace(text);

    public static string Truncate(this string text, int max) => text.Length <= max ? text : text[..max] + "…";

    // Generic extension: IEnumerable<T> üzerinde — LINQ'in tamamı bu mantıkla yazılmıştır (Where, Select, OrderBy…)
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : class
    {
        foreach (var item in source)
            if (item is not null) yield return item;
    }
}

public sealed class Widget
{
    public string Describe() => "örnek metot (instance)";           // ← instance HER ZAMAN kazanır
}

public static class WidgetExtensions
{
    public static string Describe(this Widget w) => "extension metot";                          // aynı imza → ASLA çağrılmaz (gölgelenir)
    public static string Describe(this Widget w, string prefix) => $"{prefix}: extension metot"; // farklı imza → çağrılabilir
}
