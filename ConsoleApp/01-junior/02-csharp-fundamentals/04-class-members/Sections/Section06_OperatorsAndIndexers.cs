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
        var raw = (decimal)a;                                        // explicit: the currency is lost
        Console.WriteLine($"implicit: Money m = 50m → {implicitMoney};  explicit: (decimal)a → {raw}");
        Console.WriteLine($"Equality consistency: a.Equals(copy) = {a.Equals(new Money(100m, "TRY"))}, hashes equal = {a.GetHashCode() == new Money(100m, "TRY").GetHashCode()}");

        Console.WriteLine("\nINDEXER — MoneyBag");
        var bag = new MoneyBag { a, b, usd, new Money(4m, "usd") };
        Console.WriteLine($"bag[0] = {bag[0]};  bag[^1] (Index) = {bag[^1]};  bag.Count = {bag.Count}");
        Console.WriteLine($"bag[\"TRY\"] = {bag["TRY"]} (string indexer: total per currency);  bag[\"USD\", 1] = {bag["USD", 1]} (multi-parameter)");
        bag[0] = new Money(1m, "TRY");                                // set accessor
        Console.WriteLine($"bag[0] = ... assigned → {bag[0]};  foreach: {string.Join(" | ", bag)}");
        try { _ = bag[99]; }
        catch (ArgumentOutOfRangeException) { Console.WriteLine("bag[99] → ArgumentOutOfRangeException"); }

        Console.WriteLine("""

        When to use operator overloading? When there is a real MATHEMATICAL/natural meaning (Money, Vector, TimeSpan, DateTime - DateTime).
        A meaningless overload (e.g. Order + Order) hurts readability. Rules: ==/!= and </> come in pairs; Equals/GetHashCode/IEquatable consistent.
        Indexer: a collection-like API (this[...]); can take types other than int and multiple parameters; can be get-only. No name → compiled as an 'Item' property.
        """);
    }
}
