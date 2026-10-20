using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section06_OperatorsAndIndexers
{
    public static void Run()
    {
        var a = new Money(100m, "try");
        var b = new Money(25.5m, "TRY");
        var usd = new Money(10m, "usd");

        Console.WriteLine($"a + b   = {a + b};  a - b = {a - b};  a * 1.5m = {a * 1.5m};  2m * a = {2m * a};  -a = {-a}");
        Console.WriteLine($"a == b  = {a == b};  a != b = {a != b};  a > b = {a > b};  new Money(100,\"TRY\") == a → {new Money(100m, "TRY") == a}");
        try { _ = a + usd; }
        catch (InvalidOperationException ex) { Console.WriteLine($"a + usd → {ex.Message}"); }

        Money implicitMoney = 50m;                                  // implicit: decimal → Money (TRY)
        var raw = (decimal)a;                                        // explicit: para birimi kaybolur
        Console.WriteLine($"implicit: Money m = 50m → {implicitMoney};  explicit: (decimal)a → {raw}");
        Console.WriteLine($"Eşitlik tutarlılığı: a.Equals(copy) = {a.Equals(new Money(100m, "TRY"))}, hash'ler eşit = {a.GetHashCode() == new Money(100m, "TRY").GetHashCode()}");

        Console.WriteLine("\nINDEXER — MoneyBag");
        var bag = new MoneyBag { a, b, usd, new Money(4m, "usd") };
        Console.WriteLine($"bag[0] = {bag[0]};  bag[^1] (Index) = {bag[^1]};  bag.Count = {bag.Count}");
        Console.WriteLine($"bag[\"TRY\"] = {bag["TRY"]} (string indexer: para birimi toplamı);  bag[\"USD\", 1] = {bag["USD", 1]} (çok parametreli)");
        bag[0] = new Money(1m, "TRY");                                // set accessor
        Console.WriteLine($"bag[0] = ... atandı → {bag[0]};  foreach: {string.Join(" | ", bag)}");
        try { _ = bag[99]; }
        catch (ArgumentOutOfRangeException) { Console.WriteLine("bag[99] → ArgumentOutOfRangeException"); }

        Console.WriteLine("""

        Operator overloading ne zaman? Gerçek bir MATEMATİKSEL/doğal anlam varsa (Money, Vector, TimeSpan, DateTime - DateTime).
        Anlamsız overload (örn. Order + Order) okunabilirliği bozar. Kural: ==/!= ve </> çiftleri birlikte; Equals/GetHashCode/IEquatable tutarlı.
        Indexer: koleksiyon benzeri API (this[...]); int dışında tip ve birden çok parametre alabilir; get-only olabilir. İsim yok → 'Item' property'si olarak derlenir.
        """);
    }
}
