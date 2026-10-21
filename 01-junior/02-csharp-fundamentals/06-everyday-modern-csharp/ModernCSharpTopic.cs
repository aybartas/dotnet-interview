namespace Junior.CSharpFundamentals.ModernCSharp;

public static class ModernCSharpTopic
{
    public static int Run(string[] args)
    {
        // J2.6 — Günlük Modern C#.
        //   dotnet run -- 2.6          → bölüm listesi
        //   dotnet run -- 2.6 10    → AYNI sipariş modeli, C# 7 tarzı vs C# 14 tarzı (satır sayısı + davranış eşitliği)
        //   dotnet run -- 2.6 all   → hepsi

        if (args.Length == 0)
        {
            Console.WriteLine("J2.6 Günlük Modern C# — bölümler:\n");
            foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine("\nÇalıştırmak için: dotnet run -- 2.6 <numara> | all");
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
