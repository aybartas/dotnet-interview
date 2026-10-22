namespace Junior.CSharpFundamentals.Oop;

public static class Section03_AbstractVsInterface
{
    public static void Run()
    {
        Console.WriteLine("A) abstract class: can carry state (fields), a constructor, protected members, members with and without a body; SINGLE inheritance");
        Report report = new SalesReport();
        Console.WriteLine($"   {report.Render()}");
        // var r = new Report("x");        // ✗ CS0144: an abstract class CANNOT BE INSTANTIATED — but it has a constructor (the derived class calls it)

        Console.WriteLine("\nB) interface: a contract; multiple implementation; (C# 8+) default implementation exists, but NO fields/constructor");
        ILogger logger = new ConsoleLogger();
        logger.Log("normal message");
        logger.LogWarning("warning (default implementation — ConsoleLogger didn't write it)");

        Console.WriteLine("\nC) EXPLICIT interface implementation — two interface members with the same signature (diamond-like conflict)");
        var duck = new RobotDuck();
        Console.WriteLine($"   duck.Fly()               → {duck.Fly()}");
        Console.WriteLine($"   ((IBird)duck).Move()     → {((IBird)duck).Move()}");
        Console.WriteLine($"   ((IRobot)duck).Move()    → {((IRobot)duck).Move()}");
        Console.WriteLine("   duck.Move() → ✗ does not compile: explicitly implemented members are reachable only through the relevant interface reference");

        Console.WriteLine("""

        DECISION TABLE
        ┌──────────────────────────────┬──────────────────────────────┬───────────────────────────────┐
        │                              │ abstract class               │ interface                     │
        ├──────────────────────────────┼──────────────────────────────┼───────────────────────────────┤
        │ Relationship                 │ "is-a" + shared CODE/STATE   │ "can-do" / role / contract    │
        │ Multiple implementation      │ ✗ single inheritance         │ ✓ as many as you like         │
        │ Field / state                │ ✓                            │ ✗ (static members only)       │
        │ Constructor                  │ ✓                            │ ✗                             │
        │ Access modifiers             │ protected, private, internal…│ members public by default     │
        │ Members with a body          │ ✓ (always)                   │ ✓ default impl. (C# 8)        │
        │ Adding a member later        │ Safe if virtual              │ Safe with default impl.       │
        │ Mocking/testing              │ Hard                         │ Easy                          │
        └──────────────────────────────┴──────────────────────────────┴───────────────────────────────┘
        DIFFERENCES THAT REMAIN after C# 8 default implementation: an interface still cannot hold state (instance fields), has no constructor,
        a class derives from ONE abstract class but can implement AS MANY interfaces as it wants; and a default implementation is reachable
        only through the interface reference (not through an instance of the implementing class).
        Rule of thumb: interface first (abstracting a dependency, testability); abstract class if you need to share CODE/STATE.
        """);
    }

    // ── A) abstract class ──
    private abstract class Report
    {
        private readonly string _title;                                // state
        protected Report(string title) => _title = title;              // constructor (only a derived class can call it)
        public string Render() => $"[{_title}] {Body()}";             // template: fixed skeleton (template method)
        protected abstract string Body();                              // the derived class MUST implement
    }
    private sealed class SalesReport() : Report("Sales") { protected override string Body() => "total 42"; }

    // ── B) interface + default implementation ──
    private interface ILogger
    {
        void Log(string message);
        void LogWarning(string message) => Log($"WARNING: {message}");   // default implementation
    }
    private sealed class ConsoleLogger : ILogger { public void Log(string message) => Console.WriteLine($"   {message}"); }

    // ── C) explicit implementation ──
    private interface IBird { string Move(); }
    private interface IRobot { string Move(); }
    private sealed class RobotDuck : IBird, IRobot
    {
        public string Fly() => "flying";
        string IBird.Move() => "flapping wings";
        string IRobot.Move() => "motor running";
    }
}
