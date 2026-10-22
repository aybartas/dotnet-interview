namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section07_PrimaryConstructors
{
    public static void Run()
    {
        // ESKİ: alan + ctor + atama boilerplate'i
        var oldStyle = new GreeterOld(new Logger(), "Merhaba");
        // YENİ: primary constructor — parametreler sınıfın İÇİNDE kullanılabilir (yakalanır)
        var newStyle = new Greeter(new Logger(), "Merhaba");
        Console.WriteLine($"eski  : {oldStyle.Greet("Ayşe")}");
        Console.WriteLine($"yeni  : {newStyle.Greet("Ayşe")}");

        // Class'ta primary ctor parametresi PROPERTY DEĞİLDİR (yakalanan bir gizli alan); record'da ise property'dir
        var prop = typeof(Greeter).GetProperty("Prefix");
        Console.WriteLine($"\nclass primary ctor parametresi property mi?  typeof(Greeter).GetProperty(\"Prefix\") = {(prop is null ? "null (HAYIR)" : "var")}");
        Console.WriteLine($"record primary ctor parametresi property mi? typeof(Point).GetProperty(\"X\")      = {(typeof(Point).GetProperty("X") is null ? "null" : "VAR (otomatik init property)")}");

        // Parametreyi property'ye açmak ve doğrulamak
        var account = new Account("ACC-1", 100m);
        Console.WriteLine($"\ndoğrulama: {account.Id} bakiye {account.Balance}");
        try { _ = new Account("", 1m); }
        catch (ArgumentException ex) { Console.WriteLine($"   boş id → {ex.GetType().Name}"); }

        // Primary ctor + yakalanan parametrenin mutasyonu = sürpriz
        var counter = new Counter(10);
        counter.Increment(); counter.Increment();
        Console.WriteLine($"\nyakalanan parametre DEĞİŞKENDİR: Counter(10) iki artış sonrası = {counter.Value}  (parametre state olarak kullanılıyor — dikkat)");

        Console.WriteLine("""

        Kural özeti:
          • class/struct primary constructor (C# 12): parametre SINIF GÖVDESİNDE görünür; otomatik property/alan ÜRETİLMEZ. Gerekirse `public string X { get; } = x;`
          • record primary constructor: parametre → init-only PROPERTY + Deconstruct + Equals (otomatik)
          • Parametreyi alanlara ATMADAN doğrudan kullanmak, parametrenin "yakalanıp" gizli alan olmasına yol açar (derleyici uyarır: CS9124 aynı parametre hem yakalanıp hem property başlatıcıda kullanılırsa)
          • Doğrulama için parametreyi property başlatıcısında kontrol edin: `public string Id { get; } = !string.IsNullOrEmpty(id) ? id : throw new ArgumentException(…)`
          • DI ile en yaygın kullanım: `class OrderService(IRepo repo, ILogger<OrderService> logger)` — boilerplate yok (→ J6.2)
          • Yakalanan parametre mutable'dır: `count++` gibi değiştirmek state'i gizler; immutable tutun
        """);
    }

    private sealed class Logger { public void Log(string message) { } }

    private sealed class GreeterOld
    {
        private readonly Logger _logger;
        private readonly string _prefix;

        public GreeterOld(Logger logger, string prefix)
        {
            _logger = logger;
            _prefix = prefix;
        }

        public string Greet(string name)
        {
            _logger.Log($"greet {name}");
            return $"{_prefix}, {name}!";
        }
    }

    private sealed class Greeter(Logger logger, string prefix)
    {
        public string Greet(string name)
        {
            logger.Log($"greet {name}");
            return $"{prefix}, {name}!";
        }
    }

    private sealed record Point(int X, int Y);

    private sealed class Account(string id, decimal balance)
    {
        public string Id { get; } = !string.IsNullOrWhiteSpace(id) ? id : throw new ArgumentException("Id boş olamaz", nameof(id));
        public decimal Balance { get; } = balance;
    }

    private sealed class Counter(int start)
    {
        public int Value => start;
        public void Increment() => start++;                // yakalanan parametreyi DEĞİŞTİRİYOR
    }
}
