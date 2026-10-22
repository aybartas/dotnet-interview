namespace Junior.CSharpFundamentals.Oop;

public static class Section05_Casting
{
    public static void Run()
    {
        // UPCAST: derived to base — always safe, implicit
        Dog dog = new();
        Animal animal = dog;                                              // upcast (implicit)
        Console.WriteLine($"upcast   : Dog → Animal; animal.GetType() = {animal.GetType().Name} (the object didn't change, only the VIEW narrowed)");
        // animal.Fetch();                                                // ✗ compile error: Animal has no Fetch

        // DOWNCAST: base to derived — verified at runtime, may fail
        Animal real = new Dog();
        var d1 = (Dog)real;                                               // cast: InvalidCastException if it fails
        Console.WriteLine($"downcast : (Dog)real → {d1.GetType().Name}");
        Animal cat = new Cat();
        try { _ = (Dog)cat; }
        catch (InvalidCastException) { Console.WriteLine("downcast : (Dog)cat → InvalidCastException"); }

        // as: null if it fails
        var d2 = cat as Dog;
        Console.WriteLine($"as       : cat as Dog → {(d2 is null ? "null" : "Dog")}");

        // is + pattern matching: check + cast + variable in ONE step (safest and most readable)
        foreach (Animal a in new Animal[] { new Dog(), new Cat(), new Animal() })
        {
            var description = a switch
            {
                Dog { Name: var n } => $"dog ({n}) → Fetch: {((Dog)a).Fetch()}",
                Cat c => $"cat → {c.Purr()}",
                _ => "unknown animal",
            };
            Console.WriteLine($"pattern  : {description}");
        }

        Console.WriteLine("""

        If you need many downcasts, the design smells: move the behavior onto the type with polymorphism (virtual method) or visitor/pattern matching,
        don't make the caller do type checks (Open/Closed violation).
        """);
    }

    private class Animal { }
    private sealed class Dog : Animal { public string Name => "Rex"; public string Fetch() => "fetched"; }
    private sealed class Cat : Animal { public string Purr() => "purr"; }
}
