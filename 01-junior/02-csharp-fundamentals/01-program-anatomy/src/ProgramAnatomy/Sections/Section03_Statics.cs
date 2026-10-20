namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section03_Statics
{
    public static void Run()
    {
        Console.WriteLine("static class: örneği OLUŞTURULAMAZ, yalnızca static üye taşır (Math, Path, Enumerable gibi).");
        Console.WriteLine($"static üye   : {Counter.Next()}, {Counter.Next()}, {Counter.Next()}   (tüm çağıranlar AYNI durumu paylaşır → test edilebilirlik sorunu)");

        Console.WriteLine("\nstatic constructor ne zaman çalışır?");
        Console.WriteLine("1) const'a erişim → static ctor TETİKLEMEZ (const derleme zamanında gömülür):");
        Console.WriteLine($"   Settings.Version = {Settings.Version}");
        Console.WriteLine("2) İlk static alan/metot/örnek erişiminde — HENÜZ tetiklenmedi, şimdi çağırıyoruz:");
        Console.WriteLine($"   Settings.StartedAt = {Settings.StartedAt:HH:mm:ss}");
        Console.WriteLine("3) İkinci erişimde tekrar çalışmaz (tip başına BİR KEZ, runtime thread-safe garanti eder):");
        Console.WriteLine($"   Settings.StartedAt = {Settings.StartedAt:HH:mm:ss}");

        Console.WriteLine("\nstatic ctor exception fırlatırsa tip KULLANILAMAZ olur (TypeInitializationException, her erişimde):");
        for (var i = 1; i <= 2; i++)
        {
            try { _ = Broken.Value; }
            catch (TypeInitializationException ex) { Console.WriteLine($"   deneme {i}: {ex.GetType().Name} → iç neden: {ex.InnerException!.Message}"); }
        }
        Console.WriteLine("(ne zaman/neden çalıştığının derin hali — beforefieldinit — → S1.2)");
    }

    private static class Counter
    {
        private static int _value;                       // static alan: tip başına TEK kopya
        public static int Next() => ++_value;
    }

    private static class Settings
    {
        public const string Version = "1.0";             // const → derleme zamanı sabiti, ctor tetiklemez
        public static readonly DateTime StartedAt;

        static Settings()                                // static constructor: parametresiz, erişim belirleyicisiz, elle çağrılamaz
        {
            Console.WriteLine("   >> Settings static constructor ÇALIŞTI");
            StartedAt = DateTime.Now;
        }
    }

    private static class Broken
    {
        public static readonly int Value = 1;
        static Broken() => throw new InvalidOperationException("başlatma başarısız");
    }
}
