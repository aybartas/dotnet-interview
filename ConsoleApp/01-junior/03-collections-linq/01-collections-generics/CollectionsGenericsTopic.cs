namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class CollectionsGenericsTopic
{
    public static int Run(string[] args)
    {
        // J3.1 — Collections & Generics.
        //   dotnet run -- J3.1          → bölüm listesi
        //   dotnet run -- J3.1 5     → List.Contains vs HashSet vs Dictionary arama süresi
        //   dotnet run -- J3.1 all   → hepsi
        // Veri yapılarının TEORİSİ ve Big-O ispatları DSA pistinde (→ D2, D3); burada .NET API'lerinin doğru kullanımı.

        if (args.Length == 0)
        {
            Console.WriteLine("J3.1 Collections & Generics — bölümler:\n");
            foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine("\nÇalıştırmak için: dotnet run -- J3.1 <numara> | all");
            return 0;
        }

        foreach (var section in args[0] == "all" ? Sections.All : Sections.All.Where(s => s.Number.ToString() == args[0]))
        {
            Console.WriteLine($"\n━━━ {section.Number}. {section.Title} ━━━");
            section.Run();
        }
        return 0;
    }
}
