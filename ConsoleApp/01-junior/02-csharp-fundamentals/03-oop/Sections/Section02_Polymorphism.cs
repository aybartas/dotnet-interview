using System.Numerics;

namespace Junior.CSharpFundamentals.Oop;

public static class Section02_Polymorphism
{
    public static void Run()
    {
        Console.WriteLine("A) COMPILE-TIME (static / early binding) — the COMPILER decides which method is called");

        // Overloading: same name, different parameter list; the choice is made by the COMPILE-TIME type of the ARGUMENTS
        Console.WriteLine($"   Print(5) → {Print(5)}   Print(\"x\") → {Print("x")}   Print(3.5) → {Print(3.5)}");
        object o = "a string";                                     // compile-time type: object!
        Console.WriteLine($"   object o = \"a string\"; Print(o) → {Print(o)}   ← even though the runtime type is string, the overload goes to object");
        Console.WriteLine($"   Print((dynamic)o) → {Print((dynamic)o)}   ← with dynamic the choice is deferred to runtime");

        // Generics: compile-time type parameter
        Console.WriteLine($"   Sum<int>(1,2) = {Sum(1, 2)}, Sum<double>(1.5,2.5) = {Sum(1.5, 2.5)}   (generic math, specialized code for each type)");

        // Operator overloading
        var total = new Money(10m) + new Money(5m);
        Console.WriteLine($"   Money + Money = {total}   (the + operator overload is also a compile-time resolution)");

        Console.WriteLine("\nB) RUNTIME (dynamic / late binding) — the RUNTIME TYPE decides which method runs");
        Animal[] zoo = [new Animal(), new Dog(), new Cat()];
        foreach (var a in zoo)
            Console.WriteLine($"   Animal reference → {a.GetType().Name,-6}.Speak() = {a.Speak()}");
        Console.WriteLine("   virtual/abstract + override: every object carries a vtable (virtual method table); the call is resolved from the table of the object's ACTUAL type.");
        Console.WriteLine("   Cost: a virtual call = 1 extra indirection; sealed/non-virtual calls can be inlined by the JIT.");
    }

    private static string Print(int x) => $"int({x})";
    private static string Print(string x) => $"string({x})";
    private static string Print(double x) => $"double({x})";
    private static string Print(object x) => $"object({x})";

    private static T Sum<T>(T a, T b) where T : IAdditionOperators<T, T, T> => a + b;

    private readonly record struct Money(decimal Amount)
    {
        public static Money operator +(Money a, Money b) => new(a.Amount + b.Amount);
        public override string ToString() => $"{Amount:N2}";
    }

    private class Animal { public virtual string Speak() => "..."; }
    private sealed class Dog : Animal { public override string Speak() => "Woof"; }
    private sealed class Cat : Animal { public override string Speak() => "Meow"; }
}
