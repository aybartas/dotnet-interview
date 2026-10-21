namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section03_StructClassRecord
{
    public static void Run()
    {
        // Aynı iki değeri taşıyan dört farklı tip:
        var c1 = new PersonClass("Ayşe", 30);        var c2 = new PersonClass("Ayşe", 30);
        var r1 = new PersonRecord("Ayşe", 30);       var r2 = new PersonRecord("Ayşe", 30);
        var s1 = new PersonStruct("Ayşe", 30);       var s2 = new PersonStruct("Ayşe", 30);
        var rs1 = new PersonRecordStruct("Ayşe", 30); var rs2 = new PersonRecordStruct("Ayşe", 30);

        Console.WriteLine("                    ==        Equals    ToString()");
        Console.WriteLine($"class            : {c1 == c2,-9} {c1.Equals(c2),-9} {c1}");
        Console.WriteLine($"record (class)   : {r1 == r2,-9} {r1.Equals(r2),-9} {r1}");
        Console.WriteLine($"struct           : (== yok)  {s1.Equals(s2),-9} {s1}");
        Console.WriteLine($"record struct    : {rs1 == rs2,-9} {rs1.Equals(rs2),-9} {rs1}");

        // 'with' ifadesi: non-destructive mutation (kopya + değişiklik)
        var older = r1 with { Age = 31 };
        Console.WriteLine($"\nwith: {r1} → {older}   (r1 değişmedi; ReferenceEquals = {ReferenceEquals(r1, older)})");

        Console.WriteLine("""

        KARAR TABLOSU
        ┌──────────────┬───────────┬──────────┬──────────────────────┬───────────────────────────────────────┐
        │              │ Anlamı    │ Eşitlik  │ Bellek               │ Ne zaman?                             │
        ├──────────────┼───────────┼──────────┼──────────────────────┼───────────────────────────────────────┤
        │ class        │ referans  │ referans │ heap, GC yönetir     │ Varsayılan. Kimlik/davranış/büyük nesne│
        │ record       │ referans  │ DEĞER    │ heap                 │ Değişmez veri taşıyıcı, DTO, value obj.│
        │ struct       │ değer     │ elle     │ içeren yer (stack/..)│ Küçük (≤16B), immutable, kısa ömürlü   │
        │ record struct│ değer     │ DEĞER    │ içeren yer           │ Küçük değer nesnesi (Money, Point)    │
        └──────────────┴───────────┴──────────┴──────────────────────┴───────────────────────────────────────┘
        Struct kuralları (Microsoft önerisi): tek bir mantıksal değeri temsil eder, örnek boyutu < 16 byte,
        immutable, sık boxing'e uğramaz. Aksi halde class/record.
        """);
    }

    private sealed class PersonClass(string name, int age)
    {
        public string Name { get; } = name;
        public int Age { get; } = age;
        public override string ToString() => nameof(PersonClass);        // varsayılan ToString tip adını verirdi
    }

    private sealed record PersonRecord(string Name, int Age);             // derleyici Equals/GetHashCode/ToString/==/with üretir
    private readonly struct PersonStruct(string name, int age)
    {
        public string Name { get; } = name;
        public int Age { get; } = age;
        public override string ToString() => nameof(PersonStruct);
    }
    private readonly record struct PersonRecordStruct(string Name, int Age);
}
