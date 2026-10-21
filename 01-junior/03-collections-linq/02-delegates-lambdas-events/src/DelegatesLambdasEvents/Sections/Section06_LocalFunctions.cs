namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class Section06_LocalFunctions
{
    public static void Run()
    {
        Console.WriteLine("Local function: bir metodun İÇİNDE tanımlanan, yalnızca orada kullanılan adlı fonksiyon.\n");

        Console.WriteLine("1) Özyineleme (recursion): local function kendini doğrudan çağırır; lambda için önce delegate'i bildirmek gerekir");
        Console.WriteLine($"   Factorial(5) local function = {Factorial(5)}");
        Func<int, int>? factLambda = null;                           // lambda özyinelemesi: önce null atanmış değişken, sonra tanım (çirkin)
        factLambda = n => n <= 1 ? 1 : n * factLambda!(n - 1);
        Console.WriteLine($"   Factorial(5) lambda           = {factLambda(5)}   (iş görür ama okunaksız)");

        Console.WriteLine("\n2) Tanımdan ÖNCE kullanılabilir (lambda değişkene atanmadan kullanılamaz):");
        Console.WriteLine($"   {Twice(21)}");
        int Twice(int x) => x * 2;                                    // aşağıda tanımlı olsa da yukarıda çağrılabildi

        Console.WriteLine("\n3) Delegate tahsisi: local function bir delegate'e DÖNÜŞTÜRÜLMEDİKÇE tahsis üretmez (doğrudan çağrılır). Lambda her zaman delegate nesnesidir.");
        long Alloc(Action a) { var b = GC.GetAllocatedBytesForCurrentThread(); a(); return GC.GetAllocatedBytesForCurrentThread() - b; }
        _ = Alloc(() => { });
        var viaLocal = Alloc(() => { var captured = 5; var sum = 0; for (var i = 0; i < 1000; i++) sum += AddCaptured(i); _ = sum; int AddCaptured(int x) => x + captured; });
        var viaLambda = Alloc(() => { var captured = 5; var sum = 0; Func<int, int> add = x => x + captured; for (var i = 0; i < 1000; i++) sum += add(i); _ = sum; });
        Console.WriteLine($"   yakalama içeren local function (doğrudan çağrı): {viaLocal} B · lambda (Func nesnesi + closure): {viaLambda} B");

        Console.WriteLine("\n4) `static` local function: dış değişkeni yakalayamaz → kazara closure yok:");
        Console.WriteLine($"   Square(9) static local = {Square(9)}");
        static int Square(int x) => x * x;

        Console.WriteLine("\n5) Klasik kullanım: iterator + ERKEN argüman doğrulaması");
        try { _ = BadRange(-1); Console.WriteLine("   BadRange(-1) → hata YOK (iterator ertelendi; doğrulama ilk MoveNext'te!)"); }
        catch (ArgumentOutOfRangeException) { Console.WriteLine("   BadRange(-1) → fırlattı"); }
        try { _ = GoodRange(-1); }
        catch (ArgumentOutOfRangeException) { Console.WriteLine("   GoodRange(-1) → ANINDA fırlattı (doğrulama dış metotta, iterator local function'da)"); }

        Console.WriteLine("""

        Local function ne zaman?  Yalnızca tek bir metotta kullanılan yardımcı mantık, özyineleme, iterator/async için erken doğrulama, tahsis kaçınma (static).
        Lambda ne zaman?          Bir delegate/Expression değeri gerektiğinde: parametre olarak geçmek, LINQ, olay aboneliği, saklamak.
        Ortak: ikisi de dış değişkeni yakalar (closure) — static modifier ile yasaklanır.
        """);

        static int Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);
    }

    private static IEnumerable<int> BadRange(int count)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));   // iterator → gövde ERTELENİR
        for (var i = 0; i < count; i++) yield return i;
    }

    private static IEnumerable<int> GoodRange(int count)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));   // dış metot: hemen çalışır
        return Iterator();

        IEnumerable<int> Iterator()
        {
            for (var i = 0; i < count; i++) yield return i;
        }
    }
}
