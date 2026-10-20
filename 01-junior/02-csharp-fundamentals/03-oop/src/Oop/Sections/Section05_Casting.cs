namespace Junior.CSharpFundamentals.Oop;

public static class Section05_Casting
{
    public static void Run()
    {
        // UPCAST: türeyenden tabana — daima güvenli, örtük
        Dog dog = new();
        Animal animal = dog;                                              // upcast (implicit)
        Console.WriteLine($"upcast   : Dog → Animal; animal.GetType() = {animal.GetType().Name} (nesne değişmedi, sadece GÖRÜNÜM daraldı)");
        // animal.Fetch();                                                // ✗ derleme hatası: Animal'ın Fetch'i yok

        // DOWNCAST: tabandan türeyene — çalışma zamanında doğrulanır, başarısız olabilir
        Animal real = new Dog();
        var d1 = (Dog)real;                                               // cast: başarısızsa InvalidCastException
        Console.WriteLine($"downcast : (Dog)real → {d1.GetType().Name}");
        Animal cat = new Cat();
        try { _ = (Dog)cat; }
        catch (InvalidCastException) { Console.WriteLine("downcast : (Dog)cat → InvalidCastException"); }

        // as: başarısızsa null
        var d2 = cat as Dog;
        Console.WriteLine($"as       : cat as Dog → {(d2 is null ? "null" : "Dog")}");

        // is + pattern matching: kontrol + cast + değişken TEK adımda (en güvenli ve okunaklı)
        foreach (Animal a in new Animal[] { new Dog(), new Cat(), new Animal() })
        {
            var description = a switch
            {
                Dog { Name: var n } => $"köpek ({n}) → Fetch: {((Dog)a).Fetch()}",
                Cat c => $"kedi → {c.Purr()}",
                _ => "bilinmeyen hayvan",
            };
            Console.WriteLine($"pattern  : {description}");
        }

        Console.WriteLine("""

        Çok downcast gerekiyorsa tasarım kokuyor: polimorfizm (virtual metot) veya visitor/pattern matching ile davranışı tipe taşıyın,
        çağıranın tip kontrolü yapmasını gerektirmeyin (Open/Closed ihlali).
        """);
    }

    private class Animal { }
    private sealed class Dog : Animal { public string Name => "Karabaş"; public string Fetch() => "getirdi"; }
    private sealed class Cat : Animal { public string Purr() => "mırr"; }
}
