namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section09_Csharp14
{
    public static void Run()
    {
        Console.WriteLine("A) `field` keyword (C# 14): backing field'ı elle yazmadan property doğrulaması");
        var p = new Product { Name = "  Kalem  ", Price = 12.5m };
        Console.WriteLine($"   Name = \"{p.Name}\" (setter Trim etti)");
        try { p.Price = -1m; }
        catch (ArgumentOutOfRangeException ex) { Console.WriteLine($"   Price = -1 → {ex.GetType().Name}"); }
        Console.WriteLine($"   ESKİ yazım için: private string _name; public string Name {{ get => _name; set => _name = value.Trim(); }} — alan + property + atama (3 parça)");
        Console.WriteLine($"   field keyword: tek property içinde `field` derleyicinin ürettiği gizli backing field'dır; bildirim yok.");
        Console.WriteLine("   ⚠ Sınıfınızda zaten 'field' adlı bir alan/değişken varsa anlam değişir: `@field` veya `this.field` ile ayırın (breaking change uyarısı derleyiciden gelir).");

        Console.WriteLine("\nB) Null-conditional assignment (C# 14): `x?.Prop = value` — x null ise atama HİÇ yapılmaz (sağ taraf da değerlendirilmez)");
        Customer? customer = new() { Name = "eski" };
        customer?.Name = "yeni";                                    // customer null olsaydı: atama sessizce atlanırdı
        Console.WriteLine($"   customer?.Name = \"yeni\" → {customer?.Name}");

        Customer? none = null;
        var evaluated = false;
        none?.Name = Expensive(ref evaluated);                      // none null → sağ taraf (Expensive) ÇAĞRILMAZ
        Console.WriteLine($"   none?.Name = Expensive() → null olduğu için atama atlandı; sağ taraf çağrıldı mı? {evaluated}");
        Console.WriteLine("   Eski yazım: if (customer != null) customer.Name = \"yeni\";  (hem satır hem gürültü)");
        Console.WriteLine("   Bileşik atama da çalışır: customer?.Count += 1;  ·  ++/-- desteklenmez.");

        Console.WriteLine("""

        Diğer C# 14 yenilikleri (extension members, partial constructors/events, lambda parametre değiştiricileri…) ve C# 8 → 15 sürüm sürüm tarihçe → M1.1.
        C# 15 / .NET 11 özellikleri bu repoda yalnızca **(.NET 11)** işaretiyle anılır.
        """);
    }

    private static string Expensive(ref bool evaluated) { evaluated = true; return "x"; }

    private sealed class Customer
    {
        public string Name { get; set; } = "";
        public int Count { get; set; }
    }

    private sealed class Product
    {
        public string Name
        {
            get;
            set => field = value?.Trim() ?? throw new ArgumentNullException(nameof(value));
        } = "";

        public decimal Price
        {
            get;
            set => field = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value));
        }
    }
}
