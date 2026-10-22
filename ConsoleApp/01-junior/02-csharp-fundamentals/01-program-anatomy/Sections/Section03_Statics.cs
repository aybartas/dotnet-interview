namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section03_Statics
{
    public static void Run()
    {
        Console.WriteLine("static class: CANNOT be instantiated, carries only static members (like Math, Path, Enumerable).");
        Console.WriteLine($"static member: {Counter.Next()}, {Counter.Next()}, {Counter.Next()}   (all callers share the SAME state → testability problem)");

        Console.WriteLine("\nWhen does the static constructor run?");
        Console.WriteLine("1) Accessing a const → does NOT trigger the static ctor (const is embedded at compile time):");
        Console.WriteLine($"   Settings.Version = {Settings.Version}");
        Console.WriteLine("2) On the first access to a static field/method/instance — NOT triggered yet, we call it now:");
        Console.WriteLine($"   Settings.StartedAt = {Settings.StartedAt:HH:mm:ss}");
        Console.WriteLine("3) It doesn't run again on the second access (ONCE per type, the runtime guarantees thread safety):");
        Console.WriteLine($"   Settings.StartedAt = {Settings.StartedAt:HH:mm:ss}");

        Console.WriteLine("\nIf the static ctor throws an exception the type becomes UNUSABLE (TypeInitializationException, on every access):");
        for (var i = 1; i <= 2; i++)
        {
            try { _ = Broken.Value; }
            catch (TypeInitializationException ex) { Console.WriteLine($"   attempt {i}: {ex.GetType().Name} → inner cause: {ex.InnerException!.Message}"); }
        }
        Console.WriteLine("(the deep version of when/why it runs — beforefieldinit — → S1.2)");
    }

    private static class Counter
    {
        private static int _value;                       // static field: ONE copy per type
        public static int Next() => ++_value;
    }

    private static class Settings
    {
        public const string Version = "1.0";             // const → compile-time constant, doesn't trigger the ctor
        public static readonly DateTime StartedAt;

        static Settings()                                // static constructor: parameterless, no access modifier, can't be called by hand
        {
            Console.WriteLine("   >> Settings static constructor RAN");
            StartedAt = DateTime.Now;
        }
    }

    private static class Broken
    {
        public static readonly int Value = 1;
        static Broken() => throw new InvalidOperationException("initialization failed");
    }
}
