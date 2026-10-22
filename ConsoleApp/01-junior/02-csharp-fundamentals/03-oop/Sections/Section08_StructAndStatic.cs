namespace Junior.CSharpFundamentals.Oop;

public static class Section08_StructAndStatic
{
    public static void Run()
    {
        Console.WriteLine("A) OOP limits of struct");
        Console.WriteLine($"   typeof(Point).BaseType = {typeof(Point).BaseType} (implicitly derives from ValueType)   IsSealed = {typeof(Point).IsSealed} (CANNOT be inherited)");
        // struct Point3D : Point { }          // ✗ CS0527: structs cannot derive from a class/struct
        IHasLength shape = new Point(3, 4);   // but it CAN implement an interface (→ boxing!)
        Console.WriteLine($"   struct implements interface: Length = {shape.Length}");

        Console.WriteLine("\nB) static and testability — a static member = a hidden global dependency");
        var tightDiscount = new HappyHourTight();
        Console.WriteLine($"   class bound to DateTime.Now: IsActive = {tightDiscount.IsActive()}   ← the result depends on WHEN the test runs; no deterministic test is possible");

        var happyHour = new HappyHour(new FixedTime(new DateTimeOffset(2026, 1, 1, 17, 30, 0, TimeSpan.Zero)));
        var offHour = new HappyHour(new FixedTime(new DateTimeOffset(2026, 1, 1, 9, 0, 0, TimeSpan.Zero)));
        Console.WriteLine($"   class with an injected TimeProvider: 17:30 → {happyHour.IsActive()}, 09:00 → {offHour.IsActive()}   (time is UNDER CONTROL: deterministic)");

        Console.WriteLine("""

        When is static appropriate? Stateless, pure helper functions (Math.Max, string.IsNullOrEmpty, extension methods, factories).
        The "don't make everything static" critique: static members (a) are hidden dependencies (not visible in the signature), (b) cannot be mocked, (c) shared state breaks parallel tests/threads,
        (d) cannot take part in polymorphism. Time, randomness, file system, environment (DateTime.Now, Random, File, Environment) → abstract and inject (TimeProvider, IFileSystem…).
        """);
    }

    private readonly struct Point(double x, double y) : IHasLength
    {
        public double Length => Math.Sqrt(x * x + y * y);
    }
    private interface IHasLength { double Length { get; } }

    private sealed class HappyHourTight
    {
        public bool IsActive() => DateTime.Now.Hour is >= 17 and < 19;      // static dependency
    }

    private sealed class HappyHour(TimeProvider time)                       // .NET 8+: time abstraction
    {
        public bool IsActive() => time.GetUtcNow().Hour is >= 17 and < 19;
    }

    private sealed class FixedTime(DateTimeOffset now) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => now;
    }
}
