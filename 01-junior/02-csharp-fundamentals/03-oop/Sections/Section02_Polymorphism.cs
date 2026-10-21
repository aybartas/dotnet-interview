using System.Numerics;

namespace Junior.CSharpFundamentals.Oop;

public static class Section02_Polymorphism
{
    public static void Run()
    {
        Console.WriteLine("A) COMPILE-TIME (static / early binding) — hangi metodun çağrılacağına DERLEYİCİ karar verir");

        // Overloading: aynı ad, farklı parametre listesi; seçim ARGÜMANLARIN DERLEME ZAMANI tipine göre yapılır
        Console.WriteLine($"   Print(5) → {Print(5)}   Print(\"x\") → {Print("x")}   Print(3.5) → {Print(3.5)}");
        object o = "bir string";                                   // derleme zamanı tipi: object!
        Console.WriteLine($"   object o = \"bir string\"; Print(o) → {Print(o)}   ← çalışma zamanı tipi string olsa da overload object'e gider");
        Console.WriteLine($"   Print((dynamic)o) → {Print((dynamic)o)}   ← dynamic ile seçim çalışma zamanına ertelenir");

        // Generics: derleme zamanı tip parametresi
        Console.WriteLine($"   Sum<int>(1,2) = {Sum(1, 2)}, Sum<double>(1.5,2.5) = {Sum(1.5, 2.5)}   (generic math, her tip için özelleştirilmiş kod)");

        // Operator overloading
        var total = new Money(10m) + new Money(5m);
        Console.WriteLine($"   Money + Money = {total}   (operator + overload'ı da derleme zamanı çözümüdür)");

        Console.WriteLine("\nB) RUNTIME (dynamic / late binding) — hangi metodun çalışacağına ÇALIŞMA ZAMANI TİPİ karar verir");
        Animal[] zoo = [new Animal(), new Dog(), new Cat()];
        foreach (var a in zoo)
            Console.WriteLine($"   Animal referansı → {a.GetType().Name,-6}.Speak() = {a.Speak()}");
        Console.WriteLine("   virtual/abstract + override: her nesne vtable (sanal metot tablosu) taşır; çağrı, nesnenin GERÇEK tipinin tablosundan çözülür.");
        Console.WriteLine("   Maliyet: sanal çağrı = 1 ek dolaylama (indirection); sealed/non-virtual çağrılar JIT tarafından inline edilebilir.");
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
    private sealed class Dog : Animal { public override string Speak() => "Hav"; }
    private sealed class Cat : Animal { public override string Speak() => "Miyav"; }
}
