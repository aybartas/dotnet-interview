namespace Junior.CSharpFundamentals.Oop;

public static class Section01_FourPrinciples
{
    public static void Run()
    {
        // 1) ENCAPSULATION — veriyi ve onu değiştiren davranışı birlikte tut; geçersiz duruma düşmeyi engelle
        var account = new BankAccount(100m);
        account.Withdraw(30m);
        try { account.Withdraw(500m); }
        catch (InvalidOperationException ex) { Console.WriteLine($"Encapsulation : Withdraw(500) reddedildi → {ex.Message}; bakiye hâlâ {account.Balance}"); }
        // account.Balance = -1000;        // ✗ CS0272: setter private → dışarıdan bakiyeyi keyfi değiştiremezsiniz (invariant korunur)

        // 2) ABSTRACTION — "ne yaptığı"nı göster, "nasıl yaptığı"nı sakla
        IReportExporter exporter = new CsvExporter();
        Console.WriteLine($"Abstraction   : {exporter.Export(["a", "b"])}   (çağıran CSV'yi bilmiyor; yalnızca IReportExporter sözleşmesini)");

        // 3) INHERITANCE — "is-a" ilişkisi
        Account savings = new SavingsAccount(1000m, 0.10m);
        savings.Deposit(100m);
        Console.WriteLine($"Inheritance   : SavingsAccount is-a Account → bakiye {savings.Balance}, ortak Deposit kullanıldı; ek davranış: faiz {((SavingsAccount)savings).MonthlyInterest():F2}");

        // 4) POLYMORPHISM — aynı arayüz, farklı davranış
        Shape[] shapes = [new Circle(1), new Rectangle(2, 3)];
        foreach (var shape in shapes)
            Console.WriteLine($"Polymorphism  : {shape.GetType().Name,-10} Area() = {shape.Area():F2}   (çağıran tipi bilmeden doğru metot çalışır)");

        Console.WriteLine("""

        Ne zaman inheritance DOĞRU araç? Gerçek bir "is-a" varsa, taban sınıfın TÜM sözleşmesi türeyen için de geçerliyse (Liskov), ve kod paylaşımından çok kavramsal alt-tip ilişkisi varsa.
        Tuzak: sırf kod tekrar kullanmak için kalıtım (→ Section 9, "composition over inheritance").
        """);
    }

    private sealed class BankAccount(decimal opening)
    {
        public decimal Balance { get; private set; } = opening;           // dışarıdan okunur, sadece sınıf yazar
        public void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
            if (amount > Balance) throw new InvalidOperationException("yetersiz bakiye");   // invariant: bakiye negatif olamaz
            Balance -= amount;
        }
    }

    private interface IReportExporter { string Export(IEnumerable<string> rows); }
    private sealed class CsvExporter : IReportExporter { public string Export(IEnumerable<string> rows) => string.Join(",", rows); }

    private class Account(decimal opening)
    {
        public decimal Balance { get; protected set; } = opening;
        public void Deposit(decimal amount) => Balance += amount;
    }
    private sealed class SavingsAccount(decimal opening, decimal rate) : Account(opening)
    {
        public decimal MonthlyInterest() => Balance * rate / 12;
    }

    private abstract class Shape { public abstract double Area(); }
    private sealed class Circle(double r) : Shape { public override double Area() => Math.PI * r * r; }
    private sealed class Rectangle(double w, double h) : Shape { public override double Area() => w * h; }
}
