namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section01_Records
{
    public static void Run()
    {
        // positional record: parametre listesi = init-only property'ler + ctor + Deconstruct + Equals/GetHashCode/ToString/==/!= + with
        var p1 = new Person("Ayşe", "Yılmaz", 30);
        var p2 = new Person("Ayşe", "Yılmaz", 30);

        Console.WriteLine($"ToString (otomatik)  : {p1}");
        Console.WriteLine($"p1 == p2             : {p1 == p2}   (DEĞER eşitliği — farklı nesneler, aynı içerik)   ReferenceEquals: {ReferenceEquals(p1, p2)}");
        Console.WriteLine($"GetHashCode eşit mi? : {p1.GetHashCode() == p2.GetHashCode()}");

        var older = p1 with { Age = 31 };                      // non-destructive mutation: kopya + değişiklik
        Console.WriteLine($"with                 : {p1}  →  {older}   (p1 değişmedi)");

        var (first, last, age) = p1;                           // Deconstruct otomatik
        Console.WriteLine($"deconstruct          : {first} / {last} / {age}");

        // record struct: değer tipi + değer eşitliği; readonly record struct → tamamen immutable
        var pt1 = new Point(1, 2);
        var pt2 = pt1 with { Y = 5 };
        Console.WriteLine($"readonly record struct: {pt1} → {pt2};  boyut sınıf değil değer tipi: {typeof(Point).IsValueType}");

        // record kalıtımı: türeyen record eşitliğe kendi alanlarını katar; farklı tipler EŞİT değildir
        Employee e = new("Ali", "Veli", 40, "Yazılım");
        Person asPerson = e;
        Console.WriteLine($"türeyen record       : {e};  aynı değerli Person ile eşit mi? {asPerson.Equals(new Person("Ali", "Veli", 40))} (tip farklı → false)");

        // DİKKAT: with ve eşitlik SHALLOW/referans — koleksiyon içeren record (→ J2.4)
        var rec1 = new Basket("sepet", [1, 2]);
        var rec2 = new Basket("sepet", [1, 2]);
        Console.WriteLine($"koleksiyon içeren record: rec1 == rec2 → {rec1 == rec2}   ← listeler REFERANS karşılaştırılır (içerik aynı olsa da false!)");

        Console.WriteLine("""

        record (class)    : referans tipi + değer eşitliği — DTO, domain value object, immutable veri taşıyıcı (varsayılan tercih)
        record struct     : değer tipi + değer eşitliği — küçük, kısa ömürlü değer nesneleri (Point, Money)
        class             : kimlik + davranış + mutable durum (Entity: Id ile ayırt edilir, içerik değişir)
        Positional record'un property'leri varsayılan olarak `init`-only'dir; `record struct` positional ise varsayılan MUTABLE'dır → `readonly record struct` yazın.
        EF Core entity'leri için record önerilmez (kimlik/izleme değişen nesne bekler); DTO/query sonuçları için idealdir (→ J9, M7).
        """);
    }

    private record Person(string First, string Last, int Age);
    private sealed record Employee(string First, string Last, int Age, string Department) : Person(First, Last, Age);
    private readonly record struct Point(int X, int Y);
    private sealed record Basket(string Name, List<int> Items);
}
