namespace Junior.CSharpFundamentals.Oop;

public static class Section07_CouplingCohesion
{
    public static void Run()
    {
        Console.WriteLine("COUPLING: how much one class KNOWS about another. Goal: loose coupling.");
        Console.WriteLine("COHESION: how much the contents of a class serve the SAME purpose. Goal: high cohesion.\n");

        Console.WriteLine("1) TIGHTLY coupled: 'new SmtpSender()' inside OrderService — bound to a concrete type");
        var tight = new TightOrderService();
        tight.Place("book");                                  // when testing, a REAL e-mail is sent (side effect, slow, fragile)

        Console.WriteLine("\n2) LOOSELY coupled: OrderService knows only the INotifier abstraction (the dependency is given FROM OUTSIDE)");
        var fake = new FakeNotifier();
        var loose = new LooseOrderService(fake);
        loose.Place("book");
        Console.WriteLine($"   test double captured: [{string.Join(" | ", fake.Messages)}]  (no real e-mail)");
        new LooseOrderService(new SmsNotifier()).Place("pen");     // the delivery channel changed; OrderService was NOT TOUCHED

        Console.WriteLine("""

        Measuring coupling: "How many places are affected when I change this class?" and "How many concrete things must be stood up to test this class?"
          • When SmtpSender's signature changes: Tight → OrderService's code changes (+ everything that uses it). Loose → only SmtpSender and the DI registration.
        Measuring cohesion: "How many different REASONS TO CHANGE does the class have?" (SRP). OrderManager doing e-mail + PDF + tax + DB together = low cohesion:
          a tax rule and an e-mail template both change the same file → independent changes collide.
        Cost of binding to a concrete type with 'new': untestability, inability to swap, hidden dependency. Fix: interface + constructor injection (→ J6.2 DI).
        """);
    }

    private interface INotifier { void Notify(string message); }
    private sealed class SmtpSender { public void Send(string to, string body) => Console.WriteLine($"   [SMTP] real e-mail sent → {to}: {body}"); }
    private sealed class SmsNotifier : INotifier { public void Notify(string message) => Console.WriteLine($"   [SMS] {message}"); }
    private sealed class FakeNotifier : INotifier { public List<string> Messages { get; } = []; public void Notify(string message) => Messages.Add(message); }

    private sealed class TightOrderService
    {
        private readonly SmtpSender _sender = new();                   // ← a concrete type, 'new' INSIDE itself
        public void Place(string product) => _sender.Send("customer@example.com", $"order for {product} received");
    }

    private sealed class LooseOrderService(INotifier notifier)         // ← an abstraction, given from outside
    {
        public void Place(string product) => notifier.Notify($"order for {product} received");
    }
}
