namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section03_StructClassRecord
{
    public static void Run()
    {
        // Four different types carrying the same two values:
        var c1 = new PersonClass("Alice", 30);        var c2 = new PersonClass("Alice", 30);
        var r1 = new PersonRecord("Alice", 30);       var r2 = new PersonRecord("Alice", 30);
        var s1 = new PersonStruct("Alice", 30);       var s2 = new PersonStruct("Alice", 30);
        var rs1 = new PersonRecordStruct("Alice", 30); var rs2 = new PersonRecordStruct("Alice", 30);

        Console.WriteLine("                    ==        Equals    ToString()");
        Console.WriteLine($"class            : {c1 == c2,-9} {c1.Equals(c2),-9} {c1}");
        Console.WriteLine($"record (class)   : {r1 == r2,-9} {r1.Equals(r2),-9} {r1}");
        Console.WriteLine($"struct           : (no ==)   {s1.Equals(s2),-9} {s1}");
        Console.WriteLine($"record struct    : {rs1 == rs2,-9} {rs1.Equals(rs2),-9} {rs1}");

        // 'with' expression: non-destructive mutation (copy + change)
        var older = r1 with { Age = 31 };
        Console.WriteLine($"\nwith: {r1} → {older}   (r1 unchanged; ReferenceEquals = {ReferenceEquals(r1, older)})");

        Console.WriteLine("""

        DECISION TABLE
        ┌──────────────┬───────────┬──────────┬──────────────────────┬───────────────────────────────────────┐
        │              │ Meaning   │ Equality │ Memory               │ When?                                 │
        ├──────────────┼───────────┼──────────┼──────────────────────┼───────────────────────────────────────┤
        │ class        │ reference │ reference│ heap, GC-managed     │ Default. Identity/behavior/big objs   │
        │ record       │ reference │ VALUE    │ heap                 │ Immutable data carrier, DTO, value obj│
        │ struct       │ value     │ manual   │ container (stack/..) │ Small (≤16B), immutable, short-lived  │
        │ record struct│ value     │ VALUE    │ container            │ Small value object (Money, Point)     │
        └──────────────┴───────────┴──────────┴──────────────────────┴───────────────────────────────────────┘
        Struct rules (Microsoft guidance): represents a single logical value, instance size < 16 bytes,
        immutable, not boxed frequently. Otherwise class/record.
        """);
    }

    private sealed class PersonClass(string name, int age)
    {
        public string Name { get; } = name;
        public int Age { get; } = age;
        public override string ToString() => nameof(PersonClass);        // the default ToString would give the type name
    }

    private sealed record PersonRecord(string Name, int Age);             // the compiler generates Equals/GetHashCode/ToString/==/with
    private readonly struct PersonStruct(string name, int age)
    {
        public string Name { get; } = name;
        public int Age { get; } = age;
        public override string ToString() => nameof(PersonStruct);
    }
    private readonly record struct PersonRecordStruct(string Name, int Age);
}
