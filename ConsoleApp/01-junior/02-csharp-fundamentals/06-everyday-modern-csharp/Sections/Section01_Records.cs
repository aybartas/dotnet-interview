namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section01_Records
{
    public static void Run()
    {
        // positional record: parameter list = init-only properties + ctor + Deconstruct + Equals/GetHashCode/ToString/==/!= + with
        var p1 = new Person("Alice", "Smith", 30);
        var p2 = new Person("Alice", "Smith", 30);

        Console.WriteLine($"ToString (automatic) : {p1}");
        Console.WriteLine($"p1 == p2             : {p1 == p2}   (VALUE equality — different objects, same content)   ReferenceEquals: {ReferenceEquals(p1, p2)}");
        Console.WriteLine($"Equal hash codes?    : {p1.GetHashCode() == p2.GetHashCode()}");

        var older = p1 with { Age = 31 };                      // non-destructive mutation: copy + change
        Console.WriteLine($"with                 : {p1}  →  {older}   (p1 did not change)");

        var (first, last, age) = p1;                           // Deconstruct is automatic
        Console.WriteLine($"deconstruct          : {first} / {last} / {age}");

        // record struct: value type + value equality; readonly record struct → fully immutable
        var pt1 = new Point(1, 2);
        var pt2 = pt1 with { Y = 5 };
        Console.WriteLine($"readonly record struct: {pt1} → {pt2};  it's a value type, not a class: {typeof(Point).IsValueType}");

        // record inheritance: a derived record adds its own fields to equality; different types are NOT equal
        Employee e = new("Bob", "Jones", 40, "Engineering");
        Person asPerson = e;
        Console.WriteLine($"derived record       : {e};  equal to a Person with the same values? {asPerson.Equals(new Person("Bob", "Jones", 40))} (different type → false)");

        // CAUTION: with and equality are SHALLOW/reference-based — a record that holds a collection (→ J2.4)
        var rec1 = new Basket("basket", [1, 2]);
        var rec2 = new Basket("basket", [1, 2]);
        Console.WriteLine($"record with a collection: rec1 == rec2 → {rec1 == rec2}   ← the lists are compared by REFERENCE (false even though the content is the same!)");

        Console.WriteLine("""

        record (class)    : reference type + value equality — DTO, domain value object, immutable data carrier (the default choice)
        record struct     : value type + value equality — small, short-lived value objects (Point, Money)
        class             : identity + behaviour + mutable state (Entity: distinguished by Id, content changes)
        A positional record's properties are `init`-only by default; a positional `record struct` is MUTABLE by default → write `readonly record struct`.
        Records are not recommended for EF Core entities (identity/tracking expects changing objects); ideal for DTOs/query results (→ J9, M7).
        """);
    }

    private record Person(string First, string Last, int Age);
    private sealed record Employee(string First, string Last, int Age, string Department) : Person(First, Last, Age);
    private readonly record struct Point(int X, int Y);
    private sealed record Basket(string Name, List<int> Items);
}
