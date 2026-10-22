namespace Junior.CSharpFundamentals.Oop;

public static class Section08_StructAndStatic
{
    public static void Run()
    {
        Console.WriteLine("A) struct'ın OOP kısıtları");
        Console.WriteLine($"   typeof(Point).BaseType = {typeof(Point).BaseType} (örtük olarak ValueType'tan türer)   IsSealed = {typeof(Point).IsSealed} (kalıtım ALINAMAZ)");
        // struct Point3D : Point { }          // ✗ CS0527: struct'lar sınıf/struct'tan türeyemez
        IHasLength shape = new Point(3, 4);   // ama interface UYGULAYABİLİR (→ boxing!)
        Console.WriteLine($"   struct interface uygular: Length = {shape.Length}");

        Console.WriteLine("\nB) static ve test edilebilirlik — static üye = gizli global bağımlılık");
        var tightDiscount = new HappyHourTight();
        Console.WriteLine($"   DateTime.Now'a bağlı sınıf: IsActive = {tightDiscount.IsActive()}   ← sonuç testin KOŞTUĞU ZAMANA bağlı; deterministik test yazılamaz");

        var happyHour = new HappyHour(new FixedTime(new DateTimeOffset(2026, 1, 1, 17, 30, 0, TimeSpan.Zero)));
        var offHour = new HappyHour(new FixedTime(new DateTimeOffset(2026, 1, 1, 9, 0, 0, TimeSpan.Zero)));
        Console.WriteLine($"   TimeProvider enjekte edilen sınıf: 17:30 → {happyHour.IsActive()}, 09:00 → {offHour.IsActive()}   (zaman KONTROL altında: deterministik)");

        Console.WriteLine("""

        static ne zaman uygun? Durumsuz (stateless), saf yardımcı fonksiyonlar (Math.Max, string.IsNullOrEmpty, extension method'lar, factory'ler).
        "Her şeyi static yapma" eleştirisi: static üyeler (a) gizli bağımlılıktır (imzada görünmez), (b) mock'lanamaz, (c) paylaşılan durum paralel testleri/thread'leri bozar,
        (d) polimorfizme katılamaz. Zaman, rastgelelik, dosya sistemi, ortam (DateTime.Now, Random, File, Environment) → soyutla ve enjekte et (TimeProvider, IFileSystem…).
        """);
    }

    private readonly struct Point(double x, double y) : IHasLength
    {
        public double Length => Math.Sqrt(x * x + y * y);
    }
    private interface IHasLength { double Length { get; } }

    private sealed class HappyHourTight
    {
        public bool IsActive() => DateTime.Now.Hour is >= 17 and < 19;      // static bağımlılık
    }

    private sealed class HappyHour(TimeProvider time)                       // .NET 8+: zaman soyutlaması
    {
        public bool IsActive() => time.GetUtcNow().Hour is >= 17 and < 19;
    }

    private sealed class FixedTime(DateTimeOffset now) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => now;
    }
}
