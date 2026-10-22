namespace Junior.CSharpFundamentals.Oop;

public static class Section03_AbstractVsInterface
{
    public static void Run()
    {
        Console.WriteLine("A) abstract class: durum (alan), constructor, protected üye, gövdeli+gövdesiz üye taşıyabilir; TEK kalıtım");
        Report report = new SalesReport();
        Console.WriteLine($"   {report.Render()}");
        // var r = new Report("x");        // ✗ CS0144: abstract sınıf ÖRNEKLENEMEZ — ama constructor'ı vardır (türeyen sınıf çağırır)

        Console.WriteLine("\nB) interface: sözleşme; çoklu uygulama; (C# 8+) default implementation var, ama ALAN/constructor YOK");
        ILogger logger = new ConsoleLogger();
        logger.Log("normal mesaj");
        logger.LogWarning("uyarı (default implementation — ConsoleLogger yazmadı)");

        Console.WriteLine("\nC) EXPLICIT interface implementation — aynı imzalı iki interface üyesi (diamond benzeri çakışma)");
        var duck = new RobotDuck();
        Console.WriteLine($"   duck.Fly()               → {duck.Fly()}");
        Console.WriteLine($"   ((IBird)duck).Move()     → {((IBird)duck).Move()}");
        Console.WriteLine($"   ((IRobot)duck).Move()    → {((IRobot)duck).Move()}");
        Console.WriteLine("   duck.Move() → ✗ derlenmez: explicit uygulanan üyelere yalnızca ilgili interface referansı üzerinden erişilir");

        Console.WriteLine("""

        KARAR TABLOSU
        ┌──────────────────────────────┬──────────────────────────────┬───────────────────────────────┐
        │                              │ abstract class               │ interface                     │
        ├──────────────────────────────┼──────────────────────────────┼───────────────────────────────┤
        │ İlişki                       │ "is-a" + ortak KOD/DURUM     │ "can-do" / rol / sözleşme     │
        │ Birden çok uygulama          │ ✗ tek kalıtım                │ ✓ istediğiniz kadar           │
        │ Alan (field) / durum         │ ✓                            │ ✗ (yalnızca static üye)       │
        │ Constructor                  │ ✓                            │ ✗                             │
        │ Erişim belirleyicileri       │ protected, private, internal…│ üyeler varsayılan public      │
        │ Gövdeli üye                  │ ✓ (her zaman)                │ ✓ default implementation (C# 8)│
        │ Sonradan üye eklemek         │ Türeyenleri kırmaz (virtual) │ Default impl. ile kırmaz      │
        │ Mocking/test                 │ Zor                          │ Kolay                         │
        └──────────────────────────────┴──────────────────────────────┴───────────────────────────────┘
        C# 8 default implementation sonrası KALAN FARKLAR: interface hâlâ durum (instance field) tutamaz, constructor'ı yoktur,
        bir sınıf TEK abstract class'tan türer ama İSTEDİĞİ KADAR interface uygulayabilir; ve default implementation'a yalnızca
        interface referansı üzerinden erişilir (uygulayan sınıf örneği üzerinden değil).
        Pratik kural: önce interface (bağımlılığı soyutlamak, test edilebilirlik); ortak KOD/DURUM paylaşımı gerekiyorsa abstract class.
        """);
    }

    // ── A) abstract class ──
    private abstract class Report
    {
        private readonly string _title;                                // durum
        protected Report(string title) => _title = title;              // constructor (yalnızca türeyen çağırabilir)
        public string Render() => $"[{_title}] {Body()}";             // şablon: sabit iskelet (template method)
        protected abstract string Body();                              // türeyen UYGULAMAK ZORUNDA
    }
    private sealed class SalesReport() : Report("Satış") { protected override string Body() => "toplam 42"; }

    // ── B) interface + default implementation ──
    private interface ILogger
    {
        void Log(string message);
        void LogWarning(string message) => Log($"UYARI: {message}");   // default implementation
    }
    private sealed class ConsoleLogger : ILogger { public void Log(string message) => Console.WriteLine($"   {message}"); }

    // ── C) explicit implementation ──
    private interface IBird { string Move(); }
    private interface IRobot { string Move(); }
    private sealed class RobotDuck : IBird, IRobot
    {
        public string Fly() => "uçuyor";
        string IBird.Move() => "kanat çırpıyor";
        string IRobot.Move() => "motor çalışıyor";
    }
}
