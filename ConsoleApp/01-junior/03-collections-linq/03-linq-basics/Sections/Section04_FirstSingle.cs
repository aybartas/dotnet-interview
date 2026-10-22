namespace Junior.CollectionsLinq.LinqBasics;

public static class Section04_FirstSingle
{
    public static void Run()
    {
        int[] empty = [];
        int[] one = [7];
        int[] many = [7, 8, 9];

        Console.WriteLine("                       empty sequence             one element   many elements");
        Console.WriteLine($"First()               : {Try(() => empty.First()),-26} {Try(() => one.First()),-13} {Try(() => many.First())}");
        Console.WriteLine($"FirstOrDefault()      : {Try(() => empty.FirstOrDefault()),-26} {Try(() => one.FirstOrDefault()),-13} {Try(() => many.FirstOrDefault())}");
        Console.WriteLine($"Single()              : {Try(() => empty.Single()),-26} {Try(() => one.Single()),-13} {Try(() => many.Single())}");
        Console.WriteLine($"SingleOrDefault()     : {Try(() => empty.SingleOrDefault()),-26} {Try(() => one.SingleOrDefault()),-13} {Try(() => many.SingleOrDefault())}");

        Console.WriteLine("\nTraps:");
        string[] words = [];
        Console.WriteLine($"   FirstOrDefault() returns null for a reference type: words.FirstOrDefault() is null → {words.FirstOrDefault() is null}  (a null check is mandatory)");
        Console.WriteLine($"   it returns default for a value type (int → 0): [].FirstOrDefault() = {empty.FirstOrDefault()}  ← you can't tell 'not found' from 'the value is 0'!");
        Console.WriteLine($"   safe way: FirstOrDefault(defaultValue) → {empty.FirstOrDefault(-1)}  ·  or nullable: empty.Select(x => (int?)x).FirstOrDefault() = {(empty.Select(x => (int?)x).FirstOrDefault() is null ? "null" : "?")}");

        Console.WriteLine("""

        ⭐ Meaning:
          First            "there is at least one, give me the FIRST"     — if none, InvalidOperationException ("Sequence contains no elements")
          FirstOrDefault   "give me the first if any, otherwise default"  — no error; if none, default(T)
          Single           "there must be EXACTLY ONE, give me that"      — 0 or 2+ → InvalidOperationException (VERIFIES the contract)
          SingleOrDefault  "there must be 0 or 1"                         — 0 → default; 2+ → InvalidOperationException

        Which one to choose?
          • If uniqueness is EXPECTED (Id, e-mail, a singleton configuration record) → Single/SingleOrDefault: if your expectation is broken (a data error) you get an error IMMEDIATELY instead of a silently wrong result.
          • "Any one will do" / the first element of a sorted sequence → First/FirstOrDefault (usually together with OrderBy; First without ordering means "an arbitrary first").
          • "It may not exist" → ...OrDefault + a null/default check.
          • Performance: First STOPS at the first match; Single must look for a 2nd match to verify uniqueness (if there is only one match it scans the whole sequence). In EF Core: FirstOrDefault → `TOP 1`, SingleOrDefault → `TOP 2` (→ J9).
          • For the last element use Last/LastOrDefault; by index use ElementAt/ElementAtOrDefault.
        """);
    }

    private static string Try<T>(Func<T> f)
    {
        try { return f()?.ToString() ?? "null"; }
        catch (InvalidOperationException ex) { return ex.Message.Contains("more than one", StringComparison.OrdinalIgnoreCase) ? "InvalidOp(more than one)" : "InvalidOp(empty)"; }
    }
}
