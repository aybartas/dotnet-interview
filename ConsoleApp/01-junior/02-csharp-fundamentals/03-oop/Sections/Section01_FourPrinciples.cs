namespace Junior.CSharpFundamentals.Oop;

public static class Section01_FourPrinciples
{
    public static void Run()
    {
        // 1) ENCAPSULATION — keep data and the behavior that changes it together; prevent invalid state
        var account = new BankAccount(100m);
        account.Withdraw(30m);
        try { account.Withdraw(500m); }
        catch (InvalidOperationException ex) { Console.WriteLine($"Encapsulation : Withdraw(500) rejected → {ex.Message}; balance is still {account.Balance}"); }
        // account.Balance = -1000;        // ✗ CS0272: private setter → outside code cannot change the balance arbitrarily (invariant preserved)

        // 2) ABSTRACTION — show "what it does", hide "how it does it"
        IReportExporter exporter = new CsvExporter();
        Console.WriteLine($"Abstraction   : {exporter.Export(["a", "b"])}   (the caller doesn't know CSV; only the IReportExporter contract)");

        // 3) INHERITANCE — "is-a" relationship
        Account savings = new SavingsAccount(1000m, 0.10m);
        savings.Deposit(100m);
        Console.WriteLine($"Inheritance   : SavingsAccount is-a Account → balance {savings.Balance}, shared Deposit used; extra behavior: interest {((SavingsAccount)savings).MonthlyInterest():F2}");

        // 4) POLYMORPHISM — same interface, different behavior
        Shape[] shapes = [new Circle(1), new Rectangle(2, 3)];
        foreach (var shape in shapes)
            Console.WriteLine($"Polymorphism  : {shape.GetType().Name,-10} Area() = {shape.Area():F2}   (the right method runs without the caller knowing the type)");

        Console.WriteLine("""

        When is inheritance the RIGHT tool? When there is a real "is-a", the base class's WHOLE contract also holds for the derived class (Liskov), and it is about a conceptual subtype relationship rather than code sharing.
        Trap: inheritance just to reuse code (→ Section 9, "composition over inheritance").
        """);
    }

    private sealed class BankAccount(decimal opening)
    {
        public decimal Balance { get; private set; } = opening;           // readable from outside, written only by the class
        public void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
            if (amount > Balance) throw new InvalidOperationException("insufficient balance");   // invariant: balance cannot go negative
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
